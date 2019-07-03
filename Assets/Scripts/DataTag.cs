﻿using UnityEngine;

namespace AxeMan.GameSystem.GameDataTag
{
    public enum ActionTag { INVALID, Skip, Move, }

    public enum BlueprintTag { INVALID, Altar, Floor, Trap, Actor, ProgressBar, }

    public enum CanvasTag { Canvas_World, Canvas_PCStatus_Left, }

    public enum CommandTag
    {
        INVALID, Test, Reload, PrintSchedule, RemoveFromSchedule, NextInSchedule,
        ChangeHP,
        Left, Right, Up, Down,
    }

    public enum MainTag { INVALID, Building, Trap, Actor, Floor, Marker, }

    public enum SearchEventTag { Position, MainTag, SubTag, }

    public enum SubTag
    {
        INVALID, Dummy, LifeAltar, ShieldAltar, Floor, ProgressBar,
        FireTrap, IceTrap, LightningTrap, EarthTrap,
        PC,
    }

    public enum UITag { Modeline, HPText, HPData, }

    public class DataTag : MonoBehaviour
    {
    }
}
