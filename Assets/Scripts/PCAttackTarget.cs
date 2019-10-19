﻿using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class PCAttackTarget : MonoBehaviour
    {
        private GameObject aimMarker;
        private int[] targetPosition;
        private int zeroDamage;

        private void Awake()
        {
            zeroDamage = 0;
        }

        private void PCAttackTarget_LeavingAimMode(object sender, EventArgs e)
        {
            targetPosition = aimMarker.GetComponent<MetaInfo>().Position;
        }

        private void PCAttackTarget_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            aimMarker = e.AimMarker;
        }

        private void PCAttackTarget_TakingAction(object sender,
            PublishActionEventArgs e)
        {
            if (e.SubTag != SubTag.PC)
            {
                return;
            }

            SkillNameTag skillName = GetComponent<PCSkillManager>()
               .GetSkillNameTag(e.Action);
            SkillTypeTag skillType = GetComponent<PCSkillManager>()
               .GetSkillTypeTag(skillName);
            if (skillType != SkillTypeTag.Attack)
            {
                return;
            }

            if (!GameCore.AxeManCore.GetComponent<SearchObject>()
                .Search(targetPosition[0], targetPosition[1], MainTag.Actor,
                out GameObject[] targets))
            {
                return;
            }

            int damage = GetComponent<PCSkillManager>().GetSkillDamage(skillName);
            damage += StatusMod(targets[0]);
            targets[0].GetComponent<HP>().Subtract(damage);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<InitializeMainGame>()
                .SettingReference += PCAttackTarget_SettingReference;
            GameCore.AxeManCore.GetComponent<PublishAction>().TakingAction
                += PCAttackTarget_TakingAction;
            GameCore.AxeManCore.GetComponent<AimMode>().LeavingAimMode
                += PCAttackTarget_LeavingAimMode;
        }

        private int StatusMod(GameObject target)
        {
            ActorStatus actorStatus = target.GetComponent<ActorStatus>();

            if (actorStatus.HasStatus(SkillComponentTag.AirFlaw,
                out EffectData effectData))
            {
                return effectData.Power;
            }
            return zeroDamage;
        }
    }
}
