﻿using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectFactory
{
    public class NPCComponent : MonoBehaviour
    {
        private void NPCComponent_AddingComponent(object sender,
            AddingComponentEventArgs e)
        {
            if ((e.Data.GetComponent<MetaInfo>()?.MainTag != MainTag.Actor)
                || (e.Data.GetComponent<MetaInfo>()?.SubTag == SubTag.PC))
            {
                return;
            }
            e.Data.AddComponent<DummyAI>();
        }

        private void Start()
        {
            GetComponent<CreateObject>().AddingComponent
                += NPCComponent_AddingComponent;
        }
    }
}
