using OpenTK;
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
    class Weapon
    {
        public string AcceptableAimDelta;
        public string AllowAttackGarrisonedBldgs;
        public string AntiAirborneInfantry;
        public bool AntiAirborneMonster;
        public string AntiAirborneVehicle;
        public string AntiBallisticMissile;
        public string AntiGround;
        public string AntiMine;
        public string AntiParachute;
        public string AntiProjectile;
        public string AntiSmallMissile;
        public float AttackRange;
        public AttributeModifierNugget attributeModifierNugget;
        public string AutoReloadsClip;
        public string AutoReloadWhenIdle;

        public string CapableOfFollowingWaypoints;
        public float ClipReloadTime;
        public int ClipSize;
        public string ContinueAttackRange;
        public string ContinuousFireCoast;
        public string ContinuousFireOne;
        public string ContinuousFireTwo;

        public DamageContainedNugget damageContainedNugget;
        public string DamageDealtAtSelfPosition;
        public DamageFieldNugget damageFieldNugget;
        public DamageNugget damageNugget;
        public string DamageType;
        public string DeathType;
        public float DelayBetweenShots;
        public DOTNugget dotNugget;

        public string FireFX;
        public FireLogicNugget fireLogicNugget;
        public string FireOCL;
        public string FireSound;
        public string FireSoundLoopTime;
        public int FiringDuration;
        public string FXTrigger;

        public string HistoricBonusCount;
        public string HistoricBonusRadius;
        public string HistoricBonusTime;
        public string HistoricBonusWeapon;
        public float HitPercentage;
        public bool HitStoredTarget;
        public HordeAttackNugget hordeAttackNugget;

        public float IdleAfterFiringDelay;
        public bool InstantLoadClipOnActivate;
        public bool IsAimingWeapon;

        public string LaserName;
        public string LeechRangeWeapon;
        public LuaEventNugget luaEventNugget;

        public int MaxTargetPitch;
        public float MaxWeaponSpeed;
        public MetaImpactNugget metaImpactNugget;
        public float MinimumAttackRange;
        public int MinTargetPitch;
        public float MinWeaponSpeed;

        public OpenGateNugget openGateNugget;

        public ParalyzeNugget paralyzeNugget;
        public string PlayFXWhenStealthed;
        public string PreAttackDelay;
        public string PreAttackFX;
        public int PreAttackRandomAmount;
        public string PreAttackType;
        public float PrimaryDamage;
        public float PrimaryDamageRadius;
        public string ProjectileCollidesWith;
        public string ProjectileDetonationFX;
        public string ProjectileDetonationOCL;
        public string ProjectileExhaust;
        private List<ProjectileNugget> projectileNuggets = new List<ProjectileNugget>();
        public string ProjectileObject;
        public string ProjectileStreamName; //Obsolete

        public string RadiusDamageAffects;
        public string RequestAssistRange;

        public bool ScaleWeaponSpeed;
        public float ScatterRadius;
        public float ScatterRadiusVsInfantry;
        public string ScatterTarget;
        public string ScatterTargetScalar;
        public float SecondaryDamage;
        public float SecondaryDamageRadius;
        public int ShotsPerBarrel;
        public string ShowsAmmoPips;
        public SlaveAttackNugget slaveAttackNugget;
        public StealMoneyNugget stealMoneyNugget;

        public string VeterancyFireFX;
        public string VeterancyFireOCL;
        public string VeterancyProjectileDetonationFX;
        public string VeterancyProjectileDetonationOCL;
        public string VeterancyProjectileExhaust;

        public string WeaponBonus;
        public WeaponOCLNugget weaponOCLNugget;
        public int WeaponRecoil;
        public float WeaponSpeed;    

        public void AddProjectileNugget(ProjectileNugget pn)
        {
            projectileNuggets.Add(pn);
        }
    }

    class AttributeModifierNugget
    {
        public string AntiCategories;
        public string AttributeModifier;
        public string DamageFXType;
        public float Radius;
        public string SpecialObjectFilter;
    }

    class DamageContainedNugget
    {
        public string DeathType;
        public int KillCount;
        public string KillKindof;
        public string KillKindofNot;
    }

    class DamageNugget
    {
        public bool AcceptDamageAdd;
        public bool CylinderAOE;
        public float Damage;
        public float DamageArc;
        public bool DamageArcInverted;
        public string DamageFXType;
        public int DamageMaxHeight;
        public int DamageMaxHeightAboveTerrain;
        public string DamageScalar;
        public float DamageSpeed;
        public string DamageSubType;
        public string DamageTaperOff;
        public string DamageType;
        public string DeathType;
        public float DelayTime;
        public bool DrainLife;
        public float DrainLifeMultiplier;
        public int FlankingBonus;
        public string ForbiddenUpgradeNames;
        public string ForceKillObjectFilter;
        public float Radius;
        public string RequiredUpgradeNames;
        public string SpecialObjectFilter;
    }

    class DamageFieldNugget
    {
        public int Duration;
        public string WeaponTemplateName;
    }

    class DOTNugget
    {
        public bool AcceptDamageAdd;
        public float Damage;
        public int DamageDuration;
        public string DamageFXType;
        public int DamageInterval;
        public float DamageScalar;
        public string DamageType;
        public string DeathType;
        public string DelayTime;
        public string ForbiddenUpgradeNames;
        public float Radius;
        public string RequiredUpgradeNames;
        public string SpecialObjectFilter;
    }

    class FireLogicNugget
    {
        public float Damage;
        public int DamageArc;
        public float DelayTime;
        public string LogicType;
        public float MaxResistance;
        public float MinDecay;
        public int MinMaxBurnRate;
        public float Radius; 
    }

    class HordeAttackNugget
    {
        public string LockWeaponSlot;
    }

    class LuaEventNugget
    {
        public string LuaEvent;
        public int Radius;
        public bool SendToAllies;
        public bool SendToEnemies;
        public bool SendToNeutral;
    }

    class MetaImpactNugget
    {
        public float CyclonicFactor;
        public int DelayTime;
        public bool FlipDirection;
        public float HeroResist;
        public bool InvertShockWave;
        public string KillObjectFilter;
        public bool OnlyWhenJustDied;
        public float ShockWaveAmount;
        public float ShockWaveArc;
        public bool ShockWaveArcInverted;
        public float ShockWaveClearFlingHeight;
        public float ShockWaveClearMult;
        public bool ShockWaveClearRadius;
        public float ShockWaveRadius;
        public float ShockWaveSpeed;
        public float ShockWaveTaperOff;
        public float ShockWaveZMult;
        public string SpecialObjectFilter;   
    }

    class OpenGateNugget
    {
        public float Radius;
    }

    class ParalyzeNugget
    {
        public float Duration;
        public string ParalyzeFX;
        public float Radius;
        public string SpecialObjectFilter;
    }

    class ProjectileNugget
    {
        public Vector3 AlwaysAttackHereOffset;
        public string ForbiddenUpgradeNames;
        public string ProjectileTemplateName;
        public string RequiredUpgradeNames;
        public string SpecialObjectFilter;
        public bool UseAlwaysAttackOffset;
        public string WarheadTemplateName;
        public string WeaponLaunchBoneSlotOverride;
    }

    class SlaveAttackNugget
    {

    }

    class StealMoneyNugget
    {
        public float AmountStolenPerAttack;
        public string ForbiddenUpgradeNames;
        public string RequiredUpgradeNames;
        public string SpecialObjectFilter;
    }

    class WeaponOCLNugget
    {
        public string ForbiddenUpgradeNames;
        public string RequiredUpgradeNames;
        public string WeaponOCLName;
    }
}
