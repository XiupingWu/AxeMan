﻿using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.SchedulingSystem;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class DummyAI : MonoBehaviour
    {
        private void DummyAI_EndingTurn(object sender,
            StartOrEndTurnEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }
        }

        private void DummyAI_StartingTurn(object sender,
            StartOrEndTurnEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }

            ActionTag action = ActionTag.Skip;

            GetComponent<LocalManager>().TakingAction(action);
            GetComponent<LocalManager>().TakenAction(action);
            GetComponent<LocalManager>().CheckingSchedule(action);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<TurnManager>().StartingTurn
                += DummyAI_StartingTurn;
            GameCore.AxeManCore.GetComponent<TurnManager>().EndingTurn
                += DummyAI_EndingTurn;
        }
    }
}
