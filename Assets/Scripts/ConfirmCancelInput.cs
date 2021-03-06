﻿using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem.PlayerInput
{
    public class ConfirmCancelInput : MonoBehaviour, IConvertInput
    {
        public CommandTag ConvertInput()
        {
            bool confirm = Input.GetKeyDown(KeyCode.Space);
            bool cancel = Input.GetKeyDown(KeyCode.Escape);

            if (confirm)
            {
                return CommandTag.Confirm;
            }
            else if (cancel)
            {
                return CommandTag.Cancel;
            }
            return CommandTag.INVALID;
        }
    }
}
