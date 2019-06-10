﻿using UnityEngine;

namespace AxeMan.GameSystem.Blueprint
{
    public interface IBlueprint
    {
        IPrototype[] GetBlueprint();
    }

    public interface IPrototype
    {
        MainTag MTag { get; }

        int[] Position { get; }

        SubTag STag { get; }
    }

    public class BlueprintInterface : MonoBehaviour
    {
    }

    public class Prototype : IPrototype
    {
        public Prototype(MainTag mTag, SubTag sTag, int[] position)
        {
            MTag = mTag;
            STag = sTag;
            Position = position;
        }

        public MainTag MTag { get; }

        public int[] Position { get; }

        public SubTag STag { get; }
    }
}
