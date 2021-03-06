﻿using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SearchGameObject;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface IUpgradeAltar
    {
        int CurrentLevel { get; }

        int MaxLevel { get; }
    }

    public class UpgradeAltar : MonoBehaviour, IUpgradeAltar
    {
        private int[][] altarPositions;
        private int maxDistance;
        private MetaInfo pcMetaInfo;

        public event EventHandler<EventArgs> UpgradingAltar;

        public int CurrentLevel { get; private set; }

        public int MaxLevel { get; private set; }

        protected virtual void OnUpgradingAltar(EventArgs e)
        {
            UpgradingAltar?.Invoke(this, e);
        }

        private void Awake()
        {
            CurrentLevel = 1;
        }

        private bool CheckDistance(int[] position)
        {
            return GetComponent<Distance>().GetDistance(
                position, pcMetaInfo.Position)
                < maxDistance;
        }

        private bool CheckUpgrade()
        {
            return CurrentLevel < MaxLevel;
        }

        private void SetAltarPositions()
        {
            GameObject[] altars = GetComponent<SearchObject>().Search(
                MainTag.Altar);
            Stack<int[]> position = new Stack<int[]>();

            foreach (GameObject go in altars)
            {
                position.Push(go.GetComponent<MetaInfo>().Position);
            }
            altarPositions = position.ToArray();
        }

        private void SetMaximums()
        {
            MaxLevel = GetComponent<ActorData>().GetIntData(
                MainTag.Altar, SubTag.DEFAULT, ActorDataTag.MaxLevel);
            maxDistance = GetComponent<ActorData>().GetIntData(
                MainTag.Altar, SubTag.DEFAULT, ActorDataTag.MaxDistance);
        }

        private void Start()
        {
            GetComponent<InitializeMainGame>().SettingReference
                += UpgradeAltar_SettingReference;
            GetComponent<PublishActorHP>().ChangedHP += UpgradeAltar_ChangedHP;
        }

        private void UpgradeAltar_ChangedHP(object sender, ChangeHPEventArgs e)
        {
            if (e.IsAlive || (e.SubTag == SubTag.PC))
            {
                return;
            }

            foreach (int[] pos in altarPositions)
            {
                if (CheckDistance(pos) && CheckUpgrade())
                {
                    CurrentLevel++;
                    OnUpgradingAltar(EventArgs.Empty);

                    GetComponent<LogManager>().Add(
                        new LogMessage(LogCategoryTag.Altar,
                        LogMessageTag.UpgradeAltar));

                    break;
                }
            }
        }

        private void UpgradeAltar_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            pcMetaInfo = e.PC.GetComponent<MetaInfo>();

            SetAltarPositions();
            SetMaximums();
        }
    }
}
