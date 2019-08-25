﻿using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.ObjectFactory;
using AxeMan.GameSystem.PrototypeFactory;
using AxeMan.GameSystem.SearchGameObject;
using AxeMan.GameSystem.UserInterface;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.InitializeGameWorld
{
    public class InitializeStartScreen : MonoBehaviour
    {
        private bool hideCanvas;
        private bool skipStart;

        private void Awake()
        {
            //skipStart = true;
            skipStart = false;
        }

        private void EnterMainScreen()
        {
            GetComponent<UIManager>().SwitchCanvasVisibility(
                CanvasTag.Canvas_Main, true);
            GetComponent<UIManager>().SwitchCanvasVisibility(
                CanvasTag.Canvas_Start, false);

            GetComponent<InitializeMainGame>().enabled = true;
            enabled = false;
        }

        private void EnterStartScreen()
        {
            GetComponent<UIManager>().SwitchCanvasVisibility(
                CanvasTag.Canvas_Main, false);

            GameObject ui = GetComponent<SearchUI>().Search(
                CanvasTag.Canvas_Start, UITag.UIText);
            ui.GetComponent<Text>().text
                = "Start screen: Press Space to continue.";

            IPrototype[] proto = GetComponent<Blueprint>().GetBlueprint(
                BlueprintTag.StartScreenCursor);
            GetComponent<CreateObject>().Create(proto);

            hideCanvas = true;
            enabled = false;
        }

        private void InitializeStartScreen_LeavingStartScreen(object sender,
            EventArgs e)
        {
            EnterMainScreen();
        }

        private void Start()
        {
            GetComponent<StartScreen>().LeavingStartScreen
                += InitializeStartScreen_LeavingStartScreen;
        }

        private void Update()
        {
            if (skipStart)
            {
                EnterMainScreen();
            }
            else if (!hideCanvas)
            {
                EnterStartScreen();
            }
        }
    }
}
