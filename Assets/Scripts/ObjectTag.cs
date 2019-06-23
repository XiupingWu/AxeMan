﻿using UnityEngine;

namespace AxeMan.GameSystem
{
    public enum BlueprintTag { INVALID, Altar, Floor, Trap, Actor, }

    public enum CommandTag
    {
        INVALID, Test,
        Left, Right, Up, Down,
    }

    public enum MainTag { INVALID, Building, Trap, Actor, Floor, }

    public enum SearchEventTag { Position, MainTag, SubTag, }

    public enum SubTag
    {
        INVALID, Dummy, LifeAltar, ShieldAltar, Floor,
        FireTrap, IceTrap, LightningTrap, EarthTrap,
        PC,
    }

    public class ObjectTag : MonoBehaviour
    {
    }
}
