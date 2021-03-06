﻿using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using System;
using System.Linq;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface INPCMove
    {
        int Distance { get; }

        void Approach(int moveDistance);
    }

    public class NPCMove : MonoBehaviour, INPCMove
    {
        private int baseDistance;
        private int minDistance;

        public int Distance
        {
            get
            {
                if (GetComponent<ActorStatus>().HasStatus(
                    SkillComponentTag.EarthFlaw, out EffectData effect))
                {
                    return Math.Max(minDistance, baseDistance - effect.Power);
                }
                return baseDistance;
            }
        }

        public void Approach(int moveDistance)
        {
            int[] source = GetComponent<MetaInfo>().Position;
            int move = Math.Min(moveDistance, Distance);
            int[][] path = GetComponent<NPCFindPath>().GetPath(source, move);

            foreach (int[] p in path)
            {
                GetComponent<LocalManager>().SetPosition(p);
                GetComponent<LocalManager>().TakenAction(ActionTag.NPCFindPath);
            }

            TileOverlay to = GameCore.AxeManCore.GetComponent<TileOverlay>();
            to.TryHideTile(source);
            to.TryHideTile(path.Last());
        }

        private void Awake()
        {
            MainTag mainTag = GetComponent<MetaInfo>().MainTag;
            SubTag subTag = GetComponent<MetaInfo>().SubTag;
            ActorDataTag dataTag = ActorDataTag.MoveDistance;

            baseDistance = GameCore.AxeManCore.GetComponent<ActorData>()
                .GetIntData(mainTag, subTag, dataTag);
            minDistance = 1;
        }
    }
}
