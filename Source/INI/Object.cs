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
    class Object
    {
        private List<ArmorSet> armorSets = new List<ArmorSet>(); //object

        private Dictionary<string, Behaviour> behaviours = new Dictionary<string, Behaviour>();
        private Dictionary<string, Body> bodys = new Dictionary<string, Body>();
        public bool Buildable;
        public string BuildCompletion;
        public int BuildCost;
        public int BuildTime;
        public string ButtonImage;

        public string CamouflageDetectionMultiplayer;
        public int CampnessValue;
        public int CampnessValueRadius;

        public string Deploy;
        public string DisguiseRevealedFailure;
        public string DisguiseRevealedSuccess;
        public string DisguiseStarted;
        public string DisplayColor;
        private Dictionary<string, Draw> draws = new Dictionary<string, Draw>();

        //public string editorSorting; //only used in worldBuilder
        public int EnergyProduction;
        public string ExperienceScalarTable;

        public string Geometry; //Collision geometry
        public float GeometryHeight;
        public bool GeometryIsSmall;
        public float GeometryMajorRadius;
        public float GeometryMinorRadius;

        private List<InheritableModule> inheritableModules = new List<InheritableModule>();
        public bool IsBridge;
        public bool IsForbidden;
        public bool IsPrerequisite;

        public string KindOf;

        public float PlacementViewAngle;

        public string RadarPriority;
        public int RefundValue;

        public float Scale;
        public string SelectPortrait;
        public string Shadow;
        //public string side; //only used in worldBuilder
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

        public int TransportSlotCount; //how many "slots" we take in a transport (0 == not transportable)
        public string TurretMoveLoop;

        public string Undeploy;
        public string UnderConstruction;
        public string UnderRepairFromDamage;
        public string UnderRepairFromRubble;
        public string UpgradeCameo1;
        public string UpgradeCameo2;
        public string UpgradeCameo3;
        public string UpgradeCameo4;
        public string UpgradeCameo5;

        public float VisionRange;
        public string VoiceAlert;
        public string VoiceAttack;
        public string VoiceAttackAir;
        public string VoiceAttackCharge;
        public string VoiceAttackMachine;
        public string VoiceAttackStructure;
        private Dictionary<string, string> voiceAttackUnit = new Dictionary<string, string>(); // Played when ordered to attack a specific type of object e.g. VoiceAttackUnitRohanEntFir
        public string VoiceBombard;
        public string VoiceBuildResponse;
        public string VoiceCaptureBuildingComplete;
        public string VoiceCombatDrop;
        public string VoiceCombineWithHorde;
        public string VoiceCreated;
        public string VoiceCrush;
        public string VoiceDefect;
        public string VoiceDeliverRing;
        public string VoiceEnter;
        public string VoiceEnterHostile;
        public string VoiceEnterStateAttack;
        public string VoiceEnterStateAttackAir;
        public string VoiceEnterStateAttackCharge;
        public string VoiceEnterStateAttackMachine;
        public string VoiceEnterStateAttackStructure;
        public string VoiceEnterStateInitiateCaptureBuilding;
        public string VoiceEnterStateMove;
        public string VoiceEnterStateMoveToHigherGround;
        public string VoiceEnterStateMoveToCamp;
        public string VoiceEnterStateMoveWhileAttacking;
        public string VoiceEnterStateRetreatToCastle;
        private Dictionary<string, string> voiceEnterUnit = new Dictionary<string, string>();//<ObjectName>
        public string VoiceFullyCreated;
        public string VoiceFear;
        public string VoiceGarrison;
        public string VoiceGetHealed;
        public string VoiceGuard;
        public string VoiceInitiateCaptureBuilding;
        public string VoiceMove;
        public string VoiceMoveOverWalls;
        public string VoiceMoveToCamp;
        public string VoiceMoveToHigherGround;
        public string VoiceMoveWhileAttacking;
        public string VoiceNoBuild;
        public string VoicePrimaryWeaponMode;
        public int VoicePriority;
        public string VoiceRapidFire;
        public string VoiceRepair;
        public string VoiceRetreatToCastle;
        public string VoiceSalvage;
        public string VoiceSecondaryWeaponMode;
        public string VoiceSelect;
        public string VoiceSelectBattle;
        public string VoiceSendToSlaughterhouse;
        public string VoiceSupply;
        public string VoiceTaskComplete;
        public string VoiceTertiaryWeaponMode;
        public string VoiceUnload;

        private List<WeaponSet> weaponSets = new List<WeaponSet>(); //object
        

        public void AddArmorSet(ArmorSet set)
        {
            armorSets.Add(set);
        }

        public void AddBehaviour(string name, Behaviour b)
        {
            if (!behaviours.ContainsKey(name))
                behaviours.Add(name, b);
            else
                behaviours[name] = b;
        }

        public void AddBody(string name, Body b)
        {
            if (!bodys.ContainsKey(name))
                bodys.Add(name, b);
            else
                bodys[name] = b;
        }

        public void AddDraw(string name, Draw d)
        {
            if (!draws.ContainsKey(name))
                draws.Add(name, d);
            else
                draws[name] = d;
        }
    }


    public class Animation
    {
        public int AnimationBlendTime;
        public string AnimationMode;
        public bool AnimationMustCompleteBlend;
        public string AnimationName;
        public int AnimationPriority;
        public float[] AnimationSpeedFactorRange = new float[2];
        public float Distance;
        public bool UseWeaponTiming;
    }

    public class AnimationState
    {
        private List<Animation> animations = new List<Animation>();
        public string Flags;
        public int FrameForPristineBonePositions;
        private Dictionary<string, ParticleSysBone> particleSysBones = new Dictionary<string, ParticleSysBone>();
        private List<string> script = new List<string>();
        public bool ShareAnimation;
        public bool SimilarRestart;
        public string StateName;

        public void AddAnimation(Animation a)
        {
            animations.Add(a);
        }

        public void AddParticleSysBone(string name, ParticleSysBone bone)
        {
            if (!particleSysBones.ContainsKey(name))
                particleSysBones.Add(name, bone);
            else
                particleSysBones[name] = bone;
        }

        public void AddScriptLine(string line)
        {
            script.Add(line);
        }
    }

    public class ArmorSet
    {
        public string Armor;
        public string Conditions;
        public string DamageFX;
    }

    class Behaviour
    {
        public int DestructionDelay;
    }

    class Body
    {
        public string DamageCreationList;
        public string GrabFX;
        public int GrabDamage;
        public string GrabObject;
        public int[] GrabOffset = new int[2];
        public float InitialHealth;
        public float MaxHealth;
        public float MaxHealthDamaged;
        public float MaxHealthReallyDamaged;
        public float RecoveryTime;
    }

    public class Draw
    {
        private Dictionary<string, AnimationState> animationStates = new Dictionary<string, AnimationState>();
        public ModelConditionState DefaultModelConditionState;
        public string ExtraPublicBone;
        public AnimationState IdleAnimationState;
        private Dictionary<string, ModelConditionState> modelConditionStates = new Dictionary<string, ModelConditionState>();     
        public bool OkToChangeModelColor;
        public bool StaticModelLODMode;
        public string WadingParticleSys;

        public void AddAnimationState(string name, AnimationState ast)
        {
            if (!animationStates.ContainsKey(name))
                animationStates.Add(name, ast);
            else
                animationStates[name] = ast;
        }

        public void AddModelConditionState(string name, ModelConditionState mcs)
        {
            if (!modelConditionStates.ContainsKey(name))
                modelConditionStates.Add(name, mcs);
            else
                modelConditionStates[name] = mcs;
        }
    }

    class InheritableModule
    {
        public Behaviour Behaviour;
    }

    public class ModelConditionState
    {
        public string Model;
        public Dictionary<string, ParticleSysBone> particleSysBones = new Dictionary<string, ParticleSysBone>();
        public string Shadow;
        public int ShadowMaxHeight;
        public int ShadowSizeX;
        public int ShadowSizeY;
        public string ShadowTexture;
        public bool ShadowOverrideLODVisibility;
        public string Skeleton;

        public void AddParticleSysBone(string name, ParticleSysBone bone)
        {
            if (!particleSysBones.ContainsKey(name))
                particleSysBones.Add(name, bone);
            else
                particleSysBones[name] = bone;
        }
    }

    public class ParticleSysBone
    {
        //these values are set because not all of them have to be defined in the ini file
        public bool FollowBone = false;
        public bool OnlyIfOnLand = false;
        public bool OnlyIfOnWater = false;
        public string Type;
    }

    public class TransitionState
    {
        public Animation animation;
    }

    public class WeaponSet
    {
        public string AutoChooseSources;
        public string Conditions;
        public string PRIMARY;
        public string SECONDARY;
        public string TERTIARY;
    }
}
