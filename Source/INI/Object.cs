using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.INI
{
    public struct Object
    {
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
        public List<WeaponSet> WeaponSets; //object
        public List<ArmorSet> ArmorSets; //object
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
        //set this as List?
        public Dictionary<string, string> VoiceAttackUnit; // Played when ordered to attack a specific type of object e.g. VoiceAttackUnitRohanEntFir
        public string VoiceBombard;
        public string VoiceBuildResponse;
        public string VoiceCaptureBuildingComplete;
        public string VoiceCombatDrop;
        public string VoiceCrush;
        public string VoiceDeliverRing;
        public string VoiceEnter;
        public string VoiceEnterHostile;
        public Dictionary<string, string> VoiceEnterUnit;//<ObjectName>
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

        public string Body;
        public string Behavior;
        public string Draw;

        public string[] InheritableModule;

        public float Scale;
        public string Geometry; //Collision geometry
        public float GeometryMajorRadius;
        public float GeometryMinorRadius;
        public float GeometryHeight;
        public bool GeometryIsSmall;
        public string Shadow;
        public string BuildCompletion;

        public string ExperienceScalarTable;
    }

    public struct Draw
    {
        public bool OkToChangeModelColor;
        public ModelConditionState DefaultModelConditionState;
    }

    public struct ModelConditionState
    {
        public string Model;
        public string[] ParticleSysBone;
    }

    public struct WeaponSet
    {
        public string Conditions;
        public string PRIMARY;
        public string SECONDARY;
        public string TERTIARY;
        public string AutoChooseSources;
    }

    public struct ArmorSet
    {
        public string Conditions;
        public string Armor;
        public string DamageFX;
    }

    public struct Body
    {
        public float MaxHealth;
    }

    public struct Behaviour
    {
        public int DestructionDelay;
    }
}
