﻿using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.PlayerInput;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class PCMove : MonoBehaviour
    {
        private int[] GetNewPosition(int[] source, CommandTag command)
        {
            int[] target;

            switch (command)
            {
                case CommandTag.Left:
                    target = new int[] { source[0] - 1, source[1] };
                    break;

                case CommandTag.Right:
                    target = new int[] { source[0] + 1, source[1] };
                    break;

                case CommandTag.Up:
                    target = new int[] { source[0], source[1] - 1 };
                    break;

                case CommandTag.Down:
                    target = new int[] { source[0], source[1] + 1 };
                    break;

                default:
                    target = source;
                    break;
            }
            return target;
        }

        private bool IsValidCommand(CommandTag command)
        {
            return (command == CommandTag.Left)
                || (command == CommandTag.Right)
                || (command == CommandTag.Up)
                || (command == CommandTag.Down);
        }

        private void PCMove_PlayerCommanding(object sender,
            PlayerCommandingEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }
            if (!IsValidCommand(e.Command))
            {
                return;
            }

            int[] source = GetComponent<MetaInfo>().Position;
            int[] target = GetNewPosition(source, e.Command);

            if (!GetComponent<LocalManager>().IsPassable(target))
            {
                return;
            }

            ActionTag action = ActionTag.Move;
            MainTag mainTag = GetComponent<MetaInfo>().MainTag;
            SubTag subTag = GetComponent<MetaInfo>().SubTag;
            int id = GetComponent<MetaInfo>().ObjectID;

            GetComponent<LocalManager>().SetPosition(target);
            GetComponent<LocalManager>().TakenAction(
                new TakenActionEventArgs(action, mainTag, subTag, id));
            GameCore.AxeManCore.GetComponent<TileOverlay>().TryHideTile(source);
            GameCore.AxeManCore.GetComponent<TileOverlay>().TryHideTile(target);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<InputManager>().PlayerCommanding
                += PCMove_PlayerCommanding;
        }
    }
}
