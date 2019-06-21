﻿using AxeMan.GameSystem;
using UnityEngine;

namespace AxeMan.Actor.PlayerInput
{
    public interface IInputManager
    {
        CommandTag ConvertInput();
    }

    public class PCInput : MonoBehaviour, IInputManager
    {
        private IInputManager[] input;

        public CommandTag ConvertInput()
        {
            CommandTag command;

            foreach (IInputManager i in input)
            {
                command = i.ConvertInput();
                if (command != CommandTag.INVALID)
                {
                    return command;
                }
            }
            return CommandTag.INVALID;
        }

        private void Start()
        {
            input = new IInputManager[] { GetComponent<MovementInput>(), };
        }

        private void Update()
        {
            ConvertInput();
        }
    }
}