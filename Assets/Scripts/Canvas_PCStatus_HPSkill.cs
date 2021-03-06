﻿using AxeMan.DungeonObject;
using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_PCStatus_HPSkill : MonoBehaviour
    {
        private CanvasTag canvasTag;
        private HP hp;
        private PCSkillManager skillManager;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_PCStatus_HPSkill;
        }

        private void Canvas_PCStatus_HPSkill_ChangedHP(object sender,
            ChangeHPEventArgs e)
        {
            if (e.SubTag != SubTag.PC)
            {
                return;
            }
            HPData();
        }

        private void Canvas_PCStatus_HPSkill_ChangedSkillCooldown(object sender,
            ChangedSkillCooldownEventArgs e)
        {
            SkillCooldown();
        }

        private void Canvas_PCStatus_HPSkill_CreatedWorld(object sender,
            EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);

            HPText();
            HPData();

            SkillName();
            SkillCooldown();
            SkillType();
        }

        private void Canvas_PCStatus_HPSkill_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            GameObject pc = e.PC;
            skillManager = pc.GetComponent<PCSkillManager>();
            hp = pc.GetComponent<HP>();
        }

        private void Canvas_PCStatus_HPSkill_SwitchingGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            if (e.EnterMode == GameModeTag.AimMode)
            {
                GetComponent<UIManager>().SwitchCanvasVisibility(canvasTag, false);
            }
            else if (e.LeaveMode == GameModeTag.AimMode)
            {
                GetComponent<UIManager>().SwitchCanvasVisibility(canvasTag, true);
            }
        }

        private void HPData()
        {
            SearchText(UITag.HPData).text = hp.Current + "/" + hp.Max;
        }

        private void HPText()
        {
            SearchText(UITag.HPText).text = GetComponent<UITextData>()
                .GetStringData(UITextCategoryTag.ActorStatus, UITextDataTag.HP);
        }

        private Text SearchText(UITag uiTag)
        {
            return GetComponent<SearchUI>().SearchText(uiObjects, uiTag);
        }

        private void SkillCooldown()
        {
            UITag[] ui = new UITag[]
            {
                UITag.QData, UITag.WData, UITag.EData, UITag.RData
            };

            for (int i = 0; i < ui.Length; i++)
            {
                SearchText(ui[i]).text
                    = skillManager.GetCurrentCooldown(ui[i]).ToString();
            }
        }

        private void SkillName()
        {
            UITag[] ui = new UITag[]
            {
                UITag.QText, UITag.WText, UITag.EText, UITag.RText
            };

            for (int i = 0; i < ui.Length; i++)
            {
                SkillNameTag tag = skillManager.GetSkillNameTag(ui[i]);
                SearchText(ui[i]).text = skillManager.GetSkillName(tag);
            }
        }

        private void SkillType()
        {
            UITag[] ui = new UITag[]
            {
                UITag.QType, UITag.WType, UITag.EType, UITag.RType
            };
            SkillTypeTag typeTag;
            string typeName;

            for (int i = 0; i < ui.Length; i++)
            {
                typeTag = skillManager.GetSkillTypeTag(ui[i]);
                typeName = skillManager.GetShortSkillTypeName(typeTag);
                SearchText(ui[i]).text = typeName;
            }
        }

        private void Start()
        {
            GetComponent<InitializeMainGame>().SettingReference
                += Canvas_PCStatus_HPSkill_SettingReference;
            GetComponent<InitializeMainGame>().CreatedWorld
                += Canvas_PCStatus_HPSkill_CreatedWorld;
            GetComponent<PublishActorHP>().ChangedHP
                += Canvas_PCStatus_HPSkill_ChangedHP;
            GetComponent<PublishSkill>().ChangedSkillCooldown
                += Canvas_PCStatus_HPSkill_ChangedSkillCooldown;
            GetComponent<GameModeManager>().SwitchingGameMode
                += Canvas_PCStatus_HPSkill_SwitchingGameMode;
        }
    }
}
