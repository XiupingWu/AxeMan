﻿using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.GameMode
{
    public class StartScreen : MonoBehaviour
    {
        public event EventHandler<EventArgs> LeavingStartScreen;

        protected virtual void OnLeavingStartScreen(EventArgs e)
        {
            LeavingStartScreen?.Invoke(this, e);
        }

        private bool LeaveStartScreen(PlayerCommandingEventArgs e)
        {
            if (e.SubTag != SubTag.StartScreenCursor)
            {
                return false;
            }
            return e.Command == CommandTag.Confirm;
        }

        private void Start()
        {
            GetComponent<InputManager>().PlayerCommanding
                += StartScreen_PlayerCommanding;
        }

        private void StartScreen_PlayerCommanding(object sender,
            PlayerCommandingEventArgs e)
        {
            if (LeaveStartScreen(e))
            {
                OnLeavingStartScreen(EventArgs.Empty);
            }
        }
    }
}
