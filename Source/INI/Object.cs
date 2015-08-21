using SageCS.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        public static ModelConditionState Parse(INIParser ip)
        {
            ModelConditionState state = new ModelConditionState();
            string[] data;
            do
            {
                data = ip.ParseLine();
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

        public static Draw Parse(INIParser ip)
        {
            Draw draw = new Draw();
            string[] data;
            do
            {
                data = ip.ParseLine();
                switch (data[0])
                {
                    case "ExtraPublicBone":
                        draw.ExtraPublicBone = data[2];
                        break;
                    case "DefaultModelConditionState":
                        draw.DefaultModelConditionState = ModelConditionState.Parse(ip);
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

        public static WeaponSet Parse(INIParser ip)
        {
            WeaponSet set = new WeaponSet();
            string[] data;
            do
            {
                data = ip.ParseLine();
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

        public static ArmorSet Parse(INIParser ip)
        {
            ArmorSet set = new ArmorSet();
            string[] data;
            do
            {
                data = ip.ParseLine();
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

    class Body
    {
        public float MaxHealth;

        public static Body Parse(INIParser ip)
        {
            Body body = new Body();
            string[] data;
            do
            {
                data = ip.ParseLine();
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

    class Behaviour
    {
        public int DestructionDelay;

        public static Behaviour Parse(INIParser ip)
        {
            Behaviour behav = new Behaviour();
            string[] data;
            do
            {
                data = ip.ParseLine();
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

    class InheritableModule
    {
        public Behaviour Behaviour;

        public static InheritableModule Parse(INIParser ip)
        {
            InheritableModule module = new InheritableModule();
            string[] data;
            do
            {
                data = ip.ParseLine();
                switch (data[0])
                {
                    case "Behaviour":
                        module.Behaviour = Behaviour.Parse(ip);
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

    class Object
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

        public static void Parse(INIParser ip, string name)
        {
            Object obj = new Object();
            string s;

            Dictionary<string, FieldInfo> fields = new Dictionary<string, FieldInfo>();
            //get all class variables
            foreach (var prop in obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                fields.Add(prop.Name, prop);
            }
            do
            {
                ip.ParseLine();
                s = ip.getString();

                if (fields.ContainsKey(s))
                {
                    ip.SetValue(obj, fields[s]);
                }
                else
                {
                    if (!s.Equals("End"))
                        ip.PrintError("no such variable in GameData class: " + s);
                }
            }
            while (!s.Equals("End")); //also test END ?

            INIManager.AddObject(name, obj);
        }
    }
}
