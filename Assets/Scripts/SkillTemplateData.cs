﻿using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SaveLoadGameFile;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace AxeMan.GameSystem.GameDataHub
{
    public interface ISkillTemplateData
    {
        Dictionary<SkillSlotTag, SkillComponentTag> GetSkillSlot(
            SkillNameTag skillNameTag);

        SkillTypeTag GetSkillTypeTag(SkillNameTag skillNameTag);
    }

    public class SkillTemplateData : MonoBehaviour, ISkillTemplateData
    {
        private Dictionary<SkillNameTag, SkillTypeTag> nameTypeDict;
        private SkillNameTag[] skillNames;
        private XElement templateFile;

        public Dictionary<SkillSlotTag, SkillComponentTag> GetSkillSlot(
            SkillNameTag skillNameTag)
        {
            throw new System.NotImplementedException();
        }

        public SkillTypeTag GetSkillTypeTag(SkillNameTag skillNameTag)
        {
            if (nameTypeDict.TryGetValue(skillNameTag, out SkillTypeTag data))
            {
                return data;
            }
            return SkillTypeTag.INVALID;
        }

        private void Awake()
        {
            nameTypeDict = new Dictionary<SkillNameTag, SkillTypeTag>();
            skillNames = new SkillNameTag[]
            {
                SkillNameTag.SkillQ,
                SkillNameTag.SkillW,
                SkillNameTag.SkillE,
                SkillNameTag.SkillR,
            };
        }

        private void LoadFile()
        {
            string file = "skillTemplate.xml";
            string directory = "Data";

            templateFile = GetComponent<SaveLoadXML>().Load(file, directory);
        }

        private void SetSkillType()
        {
            foreach (SkillNameTag snt in skillNames)
            {
                if (TryGetData(snt, SkillSlotTag.SkillType,
                    out XElement xElement))
                {
                    Enum.TryParse((string)xElement, out SkillTypeTag data);
                    nameTypeDict[snt] = data;
                }
            }
        }

        private void SkillTemplateData_LoadingGameData(object sender,
            EventArgs e)
        {
            LoadFile();
            SetSkillType();
        }

        private void Start()
        {
            GetComponent<InitializeStartScreen>().LoadingGameData
                += SkillTemplateData_LoadingGameData;
        }

        private bool TryGetData(SkillNameTag skillName, SkillSlotTag skillSlot,
            out XElement xElement)
        {
            xElement = templateFile
                .Element(skillName.ToString())
                .Element(skillSlot.ToString());

            return xElement != null;
        }
    }
}
