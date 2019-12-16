﻿using UnityEngine;

namespace AxeMan.GameSystem.GameDataTag
{
    public enum ActionTag
    {
        INVALID, Skip, Move, UseSkillQ, UseSkillW, UseSkillE, UseSkillR,
        ActiveAltar, BumpAttack, NPCAttack, NPCFindPath,
    }

    public enum ActorDataTag
    {
        Name, Cooldown, HP, PowerDuration, BuildingEffect, MoveDistance,
        AttackRange, Damage, CurseEffect, CurseData, MaxLevel, MaxDistance,
        AddCooldown, AddPowerDuration, BumpDamage, RestoreHP, SetTrap,
    }

    public enum BlueprintTag
    {
        INVALID, Altar, Floor, Trap, Actor, ProgressBar,
        AimMarker, ExamineMarker, StartScreenCursor, LogMarker,
    }

    public enum CanvasTag
    {
        Canvas_Main, Canvas_Start, Canvas_World, Canvas_Log,
        Canvas_Message, Canvas_ExamineMode, Canvas_Help, Canvas_PCStatus,
        Canvas_PCStatus_HPSkill, Canvas_PCStatus_CurrentStatus,
        Canvas_PCStatus_SkillData, Canvas_PCStatus_SkillFlawEffect,
    }

    public enum ColorTag
    {
        INVALID, White, Black, Grey, Orange, Green,
    }

    public enum CommandTag
    {
        INVALID, Confirm, Cancel, ExamineMode, LogMode,
        Test, ForceReload, Reload, PrintSchedule, RemoveFromSchedule,
        NextInSchedule,
        ChangeHP, PrintSkill,
        Left, Right, Up, Down,
        SkillQ, SkillW, SkillE, SkillR,
    }

    public enum GameModeTag
    {
        INVALID, NormalMode, ExamineMode, AimMode, LogMode, DeadMode, StartMode,
    }

    public enum LanguageTag { English, Chinese, }

    public enum LogCategoryTag
    {
        INVALID, Combat, Altar, Trap, GameProgress,
    }

    public enum LogMessageTag
    {
        INVALID, NewTurn, PCDeath,
        PCHit, PCCurse, NPCHit, NPCCurse,
        PCTriggerTrap, NPCTriggerTrap,
        PCBuff, PCTeleport,
        UpgradeAltar, ActivateAltar,
    }

    public enum MainTag { INVALID, Altar, Trap, Actor, Floor, Marker, }

    public enum SearchEventTag { Position, MainTag, SubTag, }

    public enum SettingDataTag { ShowStartMenu, Language, }

    public enum SkillComponentTag
    {
        INVALID, Life,
        FireMerit, WaterMerit, AirMerit, EarthMerit,
        FireFlaw, WaterFlaw, AirFlaw, EarthFlaw,
        FireCurse, WaterCurse, AirCurse, EarthCurse,
    };

    public enum SkillNameTag { INVALID, SkillQ, SkillW, SkillE, SkillR }

    public enum SkillSlotTag
    {
        SkillType, Merit1, Merit2, Merit3, Flaw1, Flaw2, Flaw3,
    }

    public enum SkillTypeTag { INVALID, Move, Attack, Buff, Curse, }

    public enum SubTag
    {
        INVALID, DEFAULT, Dummy, Floor, ProgressBar,
        FireTrap, WaterTrap, AirTrap, EarthTrap,
        FireAltar, WaterAltar, AirAltar, EarthAltar, LifeAltar,
        PC, AimMarker, ExamineMarker, LogMarker,
    }

    public enum UITag
    {
        Modeline, UIText,

        HPText, SkillText, RangeText, CooldownText,
        HPData, SkillData, RangeData, CooldownData,

        QText, WText, EText, RText,
        QData, WData, EData, RData,
        QType, WType, EType, RType,

        Status1Text, Status2Text, Status3Text, Status4Text,
        Status1Data, Status2Data, Status3Data, Status4Data,

        MoveText, AttackText, DamageText, CurseText,
        MoveData, AttackData, DamageData, CurseData,

        Line1, Line2, Line3, Line4, Line5,
        Line6, Line7, Line8, Line9, Line10,
        Line11, Line12, Line13, Line14, Line15,
        Line16, Line17, Line18, Line19, Line20,
    }

    public enum UITextCategoryTag
    {
        ActorStatus, Log, Help, World,
    }

    public enum UITextDataTag
    {
        Cooldown, HP, MoveDistance, AttackRange, Damage, Hint,
        NormalMode, AimMode, ExamineMode, MovePC, EnterExamine, EnterAim,
        ViewLog, Save, MoveCursor, UseSkill, SwitchSkill, ExitMode,
        Version, Seed, Difficulty, GameProgress, AltarLevel, AltarCooldown,
    }

    public class DataTag : MonoBehaviour
    {
    }
}
