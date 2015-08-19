using SageCS.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.INI
{
    //subclasses of the object class

    public class ParticleSysBone
    {
        public string Type;
        public bool FollowBone = false;
        public bool OnlyIfOnLand = false;
        public bool OnlyIfOnWater = false;

        public static ParticleSysBone Parse(string[] data)
        {
            ParticleSysBone bone = new ParticleSysBone();
            switch (data[4])
            {
                case "FollowBone:Yes":
                    bone.OnlyIfOnLand = true;
                    break;
            }
            switch (data[5])
            {
                case "OnlyIfOnLand:Yes":
                    bone.OnlyIfOnLand = true;
                    break;
                case "OnlyIfOnWater:Yes":
                    bone.OnlyIfOnWater = true;
                    break;
            }
            return bone;
        }
    }

    public class ModelConditionState
    {
        public string Model;
        public string Skeleton;
        public Dictionary<string, ParticleSysBone> ParticleSysBones;

        public static ModelConditionState Parse(StreamReader sr)
        {
            ModelConditionState state = new ModelConditionState();
            string[] data;
            do
            {
                data = INIParser.ReadLine(sr);
                switch (data[0])
                {
                    case "Model":
                        state.Model = data[2];
                        break;
                    case "Skeleton":
                        state.Skeleton = data[2];
                        break;
                    case "ParticleSysBone":
                        state.ParticleSysBones.Add(data[3], ParticleSysBone.Parse(data));
                        break;
                    default:
                        //Console.WriteLine("##INI: unhandled entry in ModelConditionState: " + data[0]);
                        break;
                }
            }
            while (!data[0].Equals("End")); //also test END ?

            return state;
        }
    }

    public class Draw
    {
        public string ExtraPublicBone;
        public bool OkToChangeModelColor;
        public ModelConditionState DefaultModelConditionState;

        public static Draw Parse(StreamReader sr)
        {
            Draw draw = new Draw();
            string[] data;
            do
            {
                data = INIParser.ReadLine(sr);
                switch (data[0])
                {
                    case "ExtraPublicBone":
                        draw.ExtraPublicBone = data[2];
                        break;
                    case "DefaultModelConditionState":
                        draw.DefaultModelConditionState = ModelConditionState.Parse(sr);
                        break;
                    case "OkToChangeModelColor":
                        if (data[2].Equals("Yes"))
                            draw.OkToChangeModelColor = true;
                        else
                            draw.OkToChangeModelColor = false;
                        break;
                    default:
                        //Console.WriteLine("##INI: unhandled entry in Draw: " + data[0]);
                        break;
                }
            }
            while (!data[0].Equals("End")); //also test END ?

            return draw;
        }
    }

    public class WeaponSet
    {
        public string Conditions;
        public string PRIMARY;
        public string SECONDARY;
        public string TERTIARY;
        public string AutoChooseSources;

        public static WeaponSet Parse(StreamReader sr)
        {
            WeaponSet set = new WeaponSet();
            string[] data;
            do
            {
                data = INIParser.ReadLine(sr);
                switch (data[0])
                {
                    case "Conditions":
                        set.Conditions = data[2];
                        break;
                    case "Weapon":
                        switch (data[2])
                        {
                            case "PRIMARY":
                                set.PRIMARY = data[3];
                                break;
                            case "SECONDARY":
                                set.SECONDARY = data[3];
                                break;
                            case "TERTIARY":
                                set.TERTIARY = data[3];
                                break;
                            default:
                                Console.WriteLine("##INI: unhandled entry in WeaponSet: " + data[2]);
                                break;
                        }
                        break;
                    case "AutoChooseSources":
                        set.AutoChooseSources = data[2];
                        break;
                    default:
                        //Console.WriteLine("##INI: unhandled entry in WeaponSet: " + data[0]);
                        break;
                }
            }
            while (!data[0].Equals("End")); //also test END ?

            return set;
        }
    }

    public class ArmorSet
    {
        public string Conditions;
        public string Armor;
        public string DamageFX;

        public static ArmorSet Parse(StreamReader sr)
        {
            ArmorSet set = new ArmorSet();
            string[] data;
            do
            {
                data = INIParser.ReadLine(sr);
                switch (data[0])
                {
                    case "Conditions":
                        set.Conditions = data[2];
                        break;
                    case "Armor":
                        set.Armor = data[2];
                        break;
                    case "DamageFX":
                        set.DamageFX = data[2];
                        break;
                    default:
                        //Console.WriteLine("##INI: unhandled entry in ArmorSet: " + data[0]);
                        break;
                }
            }
            while (!data[0].Equals("End")); //also test END ?

            return set;
        }
    }

    public class Body
    {
        public float MaxHealth;

        public static Body Parse(StreamReader sr)
        {
            Body body = new Body();
            string[] data;
            do
            {
                data = INIParser.ReadLine(sr);
                switch (data[0])
                {
                    case "MaxHealth":
                        body.MaxHealth = float.Parse(data[2]);
                        break;
                    default:
                        //Console.WriteLine("##INI: unhandled entry in Body: " + data[0]);
                        break;
                }
            }
            while (!data[0].Equals("End")); //also test END ?

            return body;
        }
    }

    public class Behaviour
    {
        public int DestructionDelay;

        public static Behaviour Parse(StreamReader sr)
        {
            Behaviour behav = new Behaviour();
            string[] data;
            do
            {
                data = INIParser.ReadLine(sr);
                switch (data[0])
                {
                    case "DestructionDelay":
                        behav.DestructionDelay = int.Parse(data[2]);
                        break;
                    default:
                        //Console.WriteLine("##INI: unhandled entry in Behaviour: " + data[0]);
                        break;
                }
            }
            while (!data[0].Equals("End")); //also test END ?

            return behav;
        }
    }

    public class InheritableModule
    {
        public Behaviour Behaviour;

        public static InheritableModule Parse(StreamReader sr)
        {
            InheritableModule module = new InheritableModule();
            string[] data;
            do
            {
                data = INIParser.ReadLine(sr);
                switch (data[0])
                {
                    case "Behaviour":
                        module.Behaviour = Behaviour.Parse(sr);
                        break;
                    default:
                        //Console.WriteLine("##INI: unhandled entry in InheritableModule: " + data[0]);
                        break;
                }
            }
            while (!data[0].Equals("End")); //also test END ?

            return module;
        }
    }

    public class Object
    {
        public string ButtonImage;
        public string SelectPortrait;

        //The placement view angle is the initial view angle you see a building at when it's floating at the cursor for placement.  
        //Values are 0-360 with 0 being down the X axis(right side of screen) and counterclockwise from there
        public float PlacementViewAngle;

        //For the upgrade cameo's pass in the name of the Upgrade you want the unit to display when selected.
        public string UpgradeCameo1;
        public string UpgradeCameo2;
        public string UpgradeCameo3;
        public string UpgradeCameo4;
        public string UpgradeCameo5;

        public bool Buildable;
        //public string side; //only used in worldBuilder
        //public string editorSorting; //only used in worldBuilder
        public int TransportSlotCount; //how many "slots" we take in a transport (0 == not transportable)
        public List<WeaponSet> WeaponSets = new List<WeaponSet>(); //object
        public List<ArmorSet> ArmorSets = new List<ArmorSet>(); //object
        public float VisionRange;
        public int BuildCost;
        public int BuildTime;
        public int RefundValue;
        public int EnergyProduction;
        public bool IsForbidden;
        public bool IsBridge;
        public bool IsPrerequisite;

        //sounds
        public string VoiceAlert;
        public string VoiceAttack;
        public string VoiceAttackAir;
        public string VoiceAttackCharge;
        public string VoiceAttackMachine;
        public string VoiceAttackStructure;
        public string VoiceCombineWithHorde;
        public string VoiceCreated;
        public string VoiceDefect;
        public string VoiceFullyCreated;
        public string VoiceFear;
        public string VoiceGuard;
        public string VoiceMove;
        public string VoiceMoveOverWalls;
        public string VoiceMoveToCamp;
        public string VoiceMoveToHigherGround;
        public string VoiceMoveWhileAttacking;
        public string VoiceRetreatToCastle;

        public string VoiceSelect;
        public string VoiceSelectBattle;
        public string VoiceTaskComplete;

        public string VoiceEnterStateAttack;
        public string VoiceEnterStateAttackCharge;
        public string VoiceEnterStateAttackAir;
        public string VoiceEnterStateAttackStructure;
        public string VoiceEnterStateAttackMachine;
        public string VoiceEnterStateMove;
        public string VoiceEnterStateMoveToHigherGround;
        public string VoiceEnterStateRetreatToCastle;
        public string VoiceEnterStateMoveToCamp;
        public string VoiceEnterStateMoveWhileAttacking;

        public string SoundAmbient;

        public string SoundAmbientBattle;
        public string SoundAmbientDamaged;
        public string SoundAmbientReallyDamaged;
        public string SoundAmbientRubble;
        public string SoundEnter;
        public string SoundExit;
        public string SoundCreated;
        public string SoundCrushing;
        public string SoundFallingFromPlane;
        public string SoundImpact;
        public string SoundImpactCyclonic;
        public string SoundMoveLoop;
        public string SoundMoveLoopDamaged;
        public string SoundMoveStart;
        public string SoundMoveStartDamaged;
        public string SoundOnDamaged;
        public string SoundOnReallyDamaged;
        public string SoundPromotedElite;
        public string SoundPromotedHero;
        public string SoundPromotedVeteran;
        public string SoundStealthOn;
        public string SoundStealthOff;

        //unit specific sounds
        public string Deploy;
        public string Undeploy;
        public string DisguiseStarted;
        public string DisguiseRevealedSuccess;
        public string DisguiseRevealedFailure;
        public string TurretMoveLoop;
        public string UnderConstruction;
        public string UnderRepairFromDamage;
        public string UnderRepairFromRubble;
        public Dictionary<string, string> VoiceAttackUnit = new Dictionary<string, string>(); // Played when ordered to attack a specific type of object e.g. VoiceAttackUnitRohanEntFir
        public string VoiceBombard;
        public string VoiceBuildResponse;
        public string VoiceCaptureBuildingComplete;
        public string VoiceCombatDrop;
        public string VoiceCrush;
        public string VoiceDeliverRing;
        public string VoiceEnter;
        public string VoiceEnterHostile;
        public Dictionary<string, string> VoiceEnterUnit = new Dictionary<string, string>();//<ObjectName>
        public string VoiceGetHealed;
        public string VoiceGarrison;
        public string VoiceNoBuild;
        public string VoicePrimaryWeaponMode;
        public string VoiceSecondaryWeaponMode;
        public string VoiceTertiaryWeaponMode;
        public string VoiceRapidFire;
        public string VoiceRepair;
        public string VoiceSalvage;
        public string VoiceSendToSlaughterhouse;
        public string VoiceSupply;
        public string VoiceUnload;
        public string VoiceInitiateCaptureBuilding;
        public string VoiceEnterStateInitiateCaptureBuilding;

        public int VoicePriority;
        public int CampnessValue;
        public int CampnessValueRadius;

        public string RadarPriority;
        public string DisplayColor;
        public string KindOf;
        public string CamouflageDetectionMultiplayer;

        public Dictionary<string, Body> Bodys = new Dictionary<string, Body>();
        public Dictionary<string, Draw> Draws = new Dictionary<string, Draw>();
        public Dictionary<string, Behaviour> Behaviours = new Dictionary<string, Behaviour>();

        public List<InheritableModule> InheritableModules = new List<InheritableModule>();

        public float Scale;
        public string Geometry; //Collision geometry
        public float GeometryMajorRadius;
        public float GeometryMinorRadius;
        public float GeometryHeight;
        public bool GeometryIsSmall;
        public string Shadow;
        public string BuildCompletion;

        public string ExperienceScalarTable;

        public static void Parse(StreamReader sr, string name)
        {
            Object obj = new Object();
            string[] data;
            do
            {
                data = INIParser.ReadLine(sr);
                switch (data[0])
                {
                    case "ButtonImage":
                        obj.ButtonImage = data[2];
                        break;
                    case "SelectPortrait":
                        obj.SelectPortrait = data[2];
                        break;
                    case "PlacementViewAngle":
                        obj.PlacementViewAngle = float.Parse(data[2]);
                        break;
                    case "UpgradeCameo1":
                        obj.UpgradeCameo1 = data[2];
                        break;
                    case "UpgradeCameo2":
                        obj.UpgradeCameo2 = data[2];
                        break;
                    case "UpgradeCameo3":
                        obj.UpgradeCameo3 = data[2];
                        break;
                    case "UpgradeCameo4":
                        obj.UpgradeCameo4 = data[2];
                        break;
                    case "UpgradeCameo5":
                        obj.UpgradeCameo5 = data[2];
                        break;
                    case "Buildable":
                        obj.Buildable = bool.Parse(data[2]);
                        break;
                    case "TransportSlotCount":
                        obj.TransportSlotCount = int.Parse(data[2]);
                        break;
                    case "WeaponSet":
                        obj.WeaponSets.Add(WeaponSet.Parse(sr));
                        break;
                    case "ArmorSet":
                        obj.ArmorSets.Add(ArmorSet.Parse(sr));
                        break;
                    case "VisionRange":
                        obj.VisionRange = float.Parse(data[2]);
                        break;
                    case "BuildCost":
                        obj.BuildCost = int.Parse(data[2]);
                        break;
                    case "BuildTime":
                        obj.BuildTime = int.Parse(data[2]);
                        break;
                    case "RefundValue":
                        obj.RefundValue = int.Parse(data[2]);
                        break;
                    case "EnergyProduction":
                        obj.EnergyProduction = int.Parse(data[2]);
                        break;
                    case "IsForbidden":
                        obj.IsForbidden = bool.Parse(data[2]);
                        break;
                    case "isBridge":
                        obj.IsBridge = bool.Parse(data[2]);
                        break;
                    case "IsPrerequisite":
                        obj.IsPrerequisite = bool.Parse(data[2]);
                        break;
                    case "VoiceAlert":
                        obj.VoiceAlert = data[2];
                        break;
                    case "VoiceAttack":
                        obj.VoiceAttack = data[2];
                        break;
                    case "VoiceAttackAir":
                        obj.VoiceAttackAir = data[2];
                        break;
                    case "VoiceAttackCharge":
                        obj.VoiceAttackCharge = data[2];
                        break;
                    case "VoiceAttackMachine":
                        obj.VoiceAttackMachine = data[2];
                        break;
                    case "VoiceAttackStructure":
                        obj.VoiceAttackStructure = data[2];
                        break;
                    case "VoiceCombineWithHorde":
                        obj.VoiceCombineWithHorde = data[2];
                        break;
                    case "VoiceCreated":
                        obj.VoiceCreated = data[2];
                        break;
                    case "VoiceDefect":
                        obj.VoiceDefect = data[2];
                        break;
                    case "VoiceFullyCreated":
                        obj.VoiceFullyCreated = data[2];
                        break;
                    case "VoiceFear":
                        obj.VoiceFear = data[2];
                        break;
                    case "VoiceGuard":
                        obj.VoiceGuard = data[2];
                        break;
                    case "VoiceMove":
                        obj.VoiceMove = data[2];
                        break;
                    case "VoiceMoveOverWalls":
                        obj.VoiceMoveOverWalls = data[2];
                        break;
                    case "VoiceMoveToCamp":
                        obj.VoiceMoveToCamp = data[2];
                        break;
                    case "VoiceMoveToHigherGround":
                        obj.VoiceMoveToHigherGround = data[2];
                        break;
                    case "VoiceMoveWhileAttacking":
                        obj.VoiceMoveWhileAttacking = data[2];
                        break;
                    case "VoiceRetreatToCastle":
                        obj.VoiceRetreatToCastle = data[2];
                        break;
                    case "VoiceSelect":
                        obj.VoiceSelect = data[2];
                        break;
                    case "VoiceSelectBattle":
                        obj.VoiceSelectBattle = data[2];
                        break;
                    case "VoiceTaskComplete":
                        obj.VoiceTaskComplete = data[2];
                        break;
                    case "VoiceEnterStateAttack":
                        obj.VoiceEnterStateAttack = data[2];
                        break;
                    case "VoiceEnterStateAttackCharge":
                        obj.VoiceEnterStateAttackCharge = data[2];
                        break;
                    case "VoiceEnterStateAttackAir":
                        obj.VoiceEnterStateAttackAir = data[2];
                        break;
                    case "VoiceEnterStateAttackStructure":
                        obj.VoiceEnterStateAttackStructure = data[2];
                        break;
                    case "VoiceEnterStateAttackMachine":
                        obj.VoiceEnterStateAttackMachine = data[2];
                        break;
                    case "VoiceEnterStateMove":
                        obj.VoiceEnterStateMove = data[2];
                        break;
                    case "VoiceEnterStateMoveToHigherGround":
                        obj.VoiceEnterStateMoveToHigherGround = data[2];
                        break;
                    case "VoiceEnterStateRetreatToCastle":
                        obj.VoiceEnterStateRetreatToCastle = data[2];
                        break;
                    case "VoiceEnterStateMoveToCamp":
                        obj.VoiceEnterStateMoveToCamp = data[2];
                        break;
                    case "VoiceEnterStateMoveWhileAttacking":
                        obj.VoiceEnterStateMoveWhileAttacking = data[2];
                        break;
                    case "SoundAmbient":
                        obj.SoundAmbient = data[2];
                        break;
                    case "SoundAmbientBattle":
                        obj.SoundAmbientBattle = data[2];
                        break;
                    case "SoundAmbientDamaged":
                        obj.SoundAmbientDamaged = data[2];
                        break;
                    case "SoundAmbientReallyDamaged":
                        obj.SoundAmbientReallyDamaged = data[2];
                        break;
                    case "SoundAmbientRubble":
                        obj.SoundAmbientRubble = data[2];
                        break;
                    case "SoundEnter":
                        obj.SoundEnter = data[2];
                        break;
                    case "SoundExit":
                        obj.SoundExit = data[2];
                        break;
                    case "SoundCreated":
                        obj.SoundCreated = data[2];
                        break;
                    case "SoundCrushing":
                        obj.SoundCrushing = data[2];
                        break;
                    case "SoundFallingFromPlane":
                        obj.SoundFallingFromPlane = data[2];
                        break;
                    case "SoundImpact":
                        obj.SoundImpact = data[2];
                        break;
                    case "SoundImpactCyclonic":
                        obj.SoundImpactCyclonic = data[2];
                        break;
                    case "SoundMoveLoop":
                        obj.SoundMoveLoop = data[2];
                        break;
                    case "SoundMoveLoopDamaged":
                        obj.SoundMoveLoopDamaged = data[2];
                        break;
                    case "SoundMoveStart":
                        obj.SoundMoveStart = data[2];
                        break;
                    case "SoundMoveStartDamaged":
                        obj.SoundMoveStartDamaged = data[2];
                        break;
                    case "SoundOnDamaged":
                        obj.SoundOnDamaged = data[2];
                        break;
                    case "SoundOnReallyDamaged":
                        obj.SoundOnReallyDamaged = data[2];
                        break;
                    case "SoundPromotedElite":
                        obj.SoundPromotedElite = data[2];
                        break;
                    case "SoundPromotedHero":
                        obj.SoundPromotedHero = data[2];
                        break;
                    case "SoundPromotedVeteran":
                        obj.SoundPromotedVeteran = data[2];
                        break;
                    case "SoundStealthOn":
                        obj.SoundStealthOn = data[2];
                        break;
                    case "SoundStealthOff":
                        obj.SoundStealthOff = data[2];
                        break;
                    case "Deploy":
                        obj.Deploy = data[2];
                        break;
                    case "Undeploy":
                        obj.Undeploy = data[2];
                        break;
                    case "DisguiseStarted":
                        obj.DisguiseStarted = data[2];
                        break;
                    case "DisguiseRevealedSuccess":
                        obj.DisguiseRevealedSuccess = data[2];
                        break;
                    case "DisguiseRevealedFailure":
                        obj.DisguiseRevealedFailure = data[2];
                        break;
                    case "TurretMoveLoop":
                        obj.TurretMoveLoop = data[2];
                        break;
                    case "UnderConstruction":
                        obj.UnderConstruction = data[2];
                        break;
                    case "UnderRepairFromDamage":
                        obj.UnderRepairFromDamage = data[2];
                        break;
                    case "UnderRepairFromRubble":
                        obj.UnderRepairFromRubble = data[2];
                        break;
                    //VoiceAttackUnit<ObjectName> is handled in default case
                    case "VoiceBombard":
                        obj.VoiceBombard = data[2];
                        break;
                    case "VoiceBuildResponse":
                        obj.VoiceBuildResponse = data[2];
                        break;
                    case "VoiceCaptureBuildingComplete":
                        obj.VoiceCaptureBuildingComplete = data[2];
                        break;
                    case "VoiceCombatDrop":
                        obj.VoiceCombatDrop = data[2];
                        break;
                    case "VoiceCrush":
                        obj.VoiceCrush = data[2];
                        break;
                    case "VoiceDeliverRing":
                        obj.VoiceDeliverRing = data[2];
                        break;
                    case "VoiceEnter":
                        obj.VoiceEnter = data[2];
                        break;
                    case "VoiceEnterHostile":
                        obj.VoiceEnterHostile = data[2];
                        break;
                    //VoiceEnterUnit<ObjectName> is handled in default case
                    case "VoiceGetHealed":
                        obj.VoiceGetHealed = data[2];
                        break;
                    case "VoiceGarrison":
                        obj.VoiceGarrison = data[2];
                        break;
                    case "VoiceNoBuild":
                        obj.VoiceNoBuild = data[2];
                        break;
                    case "VoicePrimaryWeaponMode":
                        obj.VoicePrimaryWeaponMode = data[2];
                        break;
                    case "VoiceSecondaryWeaponMode":
                        obj.VoiceSecondaryWeaponMode = data[2];
                        break;
                    case "VoiceTertiaryWeaponMode":
                        obj.VoiceTertiaryWeaponMode = data[2];
                        break;
                    case "VoiceRapidFire":
                        obj.VoiceRapidFire = data[2];
                        break;
                    case "VoiceRepair":
                        obj.VoiceRepair = data[2];
                        break;
                    case "VoiceSalvage":
                        obj.VoiceSalvage = data[2];
                        break;
                    case "VoiceSendToSlaughterhouse":
                        obj.VoiceSendToSlaughterhouse = data[2];
                        break;
                    case "VoiceSupply":
                        obj.VoiceSupply = data[2];
                        break;
                    case "VoiceUnload":
                        obj.VoiceUnload = data[2];
                        break;
                    case "VoiceInitiateCaptureBuilding":
                        obj.VoiceInitiateCaptureBuilding = data[2];
                        break;
                    case "VoiceEnterStateInitiateCaptureBuilding":
                        obj.VoiceEnterStateInitiateCaptureBuilding = data[2];
                        break;
                    case "VoicePriority":
                        obj.VoicePriority = int.Parse(data[2]);
                        break;
                    case "CampnessValue":
                        obj.CampnessValue = int.Parse(data[2]);
                        break;
                    case "CampnessValueRadius":
                        obj.CampnessValueRadius = int.Parse(data[2]);
                        break;
                    case "RadarPriority":
                        obj.RadarPriority = data[2];
                        break;
                    case "DisplayColor":
                        obj.DisplayColor = data[2];
                        break;
                    case "KindOf":
                        obj.KindOf = data[2];
                        break;
                    case "CamouflageDetectionMultiplayer":
                        obj.CamouflageDetectionMultiplayer = data[2];
                        break;
                    case "Body":
                        obj.Bodys.Add(data[2], Body.Parse(sr));
                        break;
                    case "Behaviour":
                        obj.Behaviours.Add(data[2], Behaviour.Parse(sr));
                        break;
                    case "Draw":
                        obj.Draws.Add(data[2], Draw.Parse(sr));
                        break;
                    case "InheritableModule":
                        obj.InheritableModules.Add(InheritableModule.Parse(sr));
                        break;
                    case "Scale":
                        obj.Scale = float.Parse(data[2]);
                        break;
                    case "Geometry":
                        obj.Geometry = data[2];
                        break;
                    case "GeometryMajorRadius":
                        obj.GeometryMajorRadius = float.Parse(data[2]);
                        break;
                    case "GeometryMinorRadius":
                        obj.GeometryMinorRadius = float.Parse(data[2]);
                        break;
                    case "GeometryHeight":
                        obj.GeometryHeight = float.Parse(data[2]);
                        break;
                    case "GeometryIsSmall":
                        obj.GeometryIsSmall = bool.Parse(data[2]);
                        break;
                    case "Shadow":
                        obj.Shadow = data[2];
                        break;
                    case "BuildCompletion":
                        obj.BuildCompletion = data[2];
                        break;
                    case "ExperienceScalarTable":
                        obj.ExperienceScalarTable = data[2];
                        break;
                    default:
                        if (data[0].StartsWith("VoiceAttackUnit"))
                            obj.VoiceAttackUnit.Add(data[0], data[2]);
                        else if (data[0].StartsWith("VoiceEnterUnit"))
                            obj.VoiceEnterUnit.Add(data[0], data[2]);
                        else
                            Console.WriteLine("##INI: unhandled entry in object: " + data[0]);
                        break;
                }
            }
            while (!data[0].Equals("End")); //also test END ?

            INIManager.AddObject(name, obj);
        }
    }
}
