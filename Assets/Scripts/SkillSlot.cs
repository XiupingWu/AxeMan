﻿using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject.ActorSkill
{
    public interface ISkillSlot
    {
        Dictionary<SkillSlotTag, SkillComponentTag> GetSkillSlot(
            SkillNameTag skillNameTag);

        bool TrySetSkillSlot(SkillNameTag skillNameTag, SkillSlotTag skillSlotTag,
            SkillComponentTag skillComponentTag);
    }

    public class SkillSlot : MonoBehaviour, ISkillSlot
    {
        private Dictionary<SkillNameTag,
            Dictionary<SkillSlotTag, SkillComponentTag>> componentDict;

        private Dictionary<SkillTypeTag, SkillComponentTag[]> validMeritSlot;

        public Dictionary<SkillSlotTag, SkillComponentTag> GetSkillSlot(
            SkillNameTag skillNameTag)
        {
            if (componentDict.TryGetValue(skillNameTag, out var slotComp))
            {
                return new Dictionary<SkillSlotTag, SkillComponentTag>(slotComp);
            }
            return null;
        }

        public bool TrySetSkillSlot(SkillNameTag skillNameTag,
            SkillSlotTag skillSlotTag, SkillComponentTag skillComponentTag)
        {
            SkillTypeTag skillTypeTag = GetComponent<PCSkillManager>()
                .GetSkillTypeTag(skillNameTag);
            bool canSetSlot;

            switch (skillSlotTag)
            {
                case SkillSlotTag.Merit1:
                case SkillSlotTag.Merit2:
                case SkillSlotTag.Merit3:
                    canSetSlot = VerifyMeritSlot(skillTypeTag, skillComponentTag);
                    break;

                case SkillSlotTag.Flaw1:
                case SkillSlotTag.Flaw2:
                case SkillSlotTag.Flaw3:
                    canSetSlot = VerifyFlawSlot(skillComponentTag);
                    break;

                default:
                    canSetSlot = false;
                    break;
            }

            if (canSetSlot
                && componentDict.TryGetValue(skillNameTag, out var slotComp))
            {
                slotComp[skillSlotTag] = skillComponentTag;
                return true;
            }
            return false;
        }

        private void Awake()
        {
            componentDict = new Dictionary<SkillNameTag,
                Dictionary<SkillSlotTag, SkillComponentTag>>();
            SkillNameTag[] skillNameTags = new SkillNameTag[]
            {
                SkillNameTag.Q, SkillNameTag.W, SkillNameTag.E, SkillNameTag.R
            };

            foreach (SkillNameTag snt in skillNameTags)
            {
                componentDict[snt]
                    = new Dictionary<SkillSlotTag, SkillComponentTag>();
            }

            validMeritSlot
                = new Dictionary<SkillTypeTag, SkillComponentTag[]>()
                {
                    { SkillTypeTag.Move,
                        new SkillComponentTag[]
                        {
                            SkillComponentTag.AirMerit,
                        }
                    },
                    { SkillTypeTag.Attack,
                        new SkillComponentTag[]
                        {
                            SkillComponentTag.AirMerit,
                            SkillComponentTag.AirFlaw,
                        }
                    },
                    { SkillTypeTag.Enhance,
                        new SkillComponentTag[]
                        {
                            SkillComponentTag.FireMerit,
                            SkillComponentTag.WaterMerit,
                            SkillComponentTag.AirMerit,
                            SkillComponentTag.EarthMerit,
                        }
                    },
                    { SkillTypeTag.Curse,
                        new SkillComponentTag[]
                        {
                            SkillComponentTag.AirMerit,

                            SkillComponentTag.FireFlaw,
                            SkillComponentTag.WaterFlaw,
                            SkillComponentTag.AirFlaw,
                            SkillComponentTag.EarthFlaw,
                        }
                    },
                };
        }

        private void SkillSlot_CreatedWorld(object sender, EventArgs e)
        {
            TestSkillSlot();
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<Wizard>().CreatedWorld
                += SkillSlot_CreatedWorld;
        }

        private void TestSkillSlot()
        {
            TrySetSkillSlot(SkillNameTag.Q, SkillSlotTag.Merit1,
                SkillComponentTag.AirMerit);
            TrySetSkillSlot(SkillNameTag.Q, SkillSlotTag.Merit2,
                SkillComponentTag.AirFlaw);
            TrySetSkillSlot(SkillNameTag.Q, SkillSlotTag.Flaw1,
                SkillComponentTag.AirFlaw);

            TrySetSkillSlot(SkillNameTag.W, SkillSlotTag.Merit1,
                SkillComponentTag.AirMerit);
            TrySetSkillSlot(SkillNameTag.W, SkillSlotTag.Merit2,
                SkillComponentTag.AirMerit);
            TrySetSkillSlot(SkillNameTag.W, SkillSlotTag.Merit3,
                SkillComponentTag.AirMerit);
            TrySetSkillSlot(SkillNameTag.W, SkillSlotTag.Flaw1,
                SkillComponentTag.WaterFlaw);

            TrySetSkillSlot(SkillNameTag.E, SkillSlotTag.Merit3,
                SkillComponentTag.EarthMerit);

            TrySetSkillSlot(SkillNameTag.R, SkillSlotTag.Flaw2,
                SkillComponentTag.AirFlaw);
        }

        private bool VerifyFlawSlot(SkillComponentTag skillComponentTag)
        {
            switch (skillComponentTag)
            {
                case SkillComponentTag.FireFlaw:
                case SkillComponentTag.WaterFlaw:
                case SkillComponentTag.AirFlaw:
                case SkillComponentTag.EarthFlaw:
                    return true;

                default:
                    return false;
            }
        }

        private bool VerifyMeritSlot(SkillTypeTag skillTypeTag,
            SkillComponentTag skillComponentTag)
        {
            if (validMeritSlot.TryGetValue(skillTypeTag, out var comp))
            {
                return Array.Exists(comp, e => e == skillComponentTag);
            }
            return false;
        }
    }
}