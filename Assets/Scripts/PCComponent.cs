﻿using AxeMan.Actor;
using AxeMan.Actor.PlayerInput;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectFactory
{
    public class PCComponent : MonoBehaviour
    {
        private void PCComponent_AddingComponent(object sender,
            AddingComponentEventArgs e)
        {
            if (e.Data.GetComponent<MetaInfo>()?.STag != SubTag.PC)
            {
                return;
            }
            e.Data.AddComponent<MovementInput>();
            e.Data.AddComponent<PCInput>();
            e.Data.AddComponent<PCMove>();
            e.Data.AddComponent<WizardInput>();
        }

        private void Start()
        {
            GetComponent<CreateObject>().AddingComponent
                += PCComponent_AddingComponent;
        }
    }
}
