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
        public float PrimaryDamage;
        public float PrimaryDamageRadius;
        public float SecondaryDamage;
        public float SecondaryDamageRadius;
        public float AttackRange;
        public float MinimumAttackRange;
        public string RequestAssistRange;
        public string AcceptableAimDelta;
        public float ScatterRadius;
        public string ScatterTargetScalar;
        public float ScatterRadiusVsInfantry;
        public string DamageType;
        public string DeathType;
        public float WeaponSpeed;
        public int MinWeaponSpeed;
        public bool ScaleWeaponSpeed;
        public int WeaponRecoil;
        public int MinTargetPitch;
        public int MaxTargetPitch;
        public string ProjectileObject;
        public string FireSound;
        public string FireSoundLoopTime;
        public string FireFX;
        public string ProjectileDetonationFX;
        public string FireOCL;
        public string ProjectileDetonationOCL;
        public string ProjectileExhaust;
        public string VeterancyFireFX;
        public string VeterancyProjectileDetonationFX;
        public string VeterancyFireOCL;
        public string VeterancyProjectileDetonationOCL;
        public string VeterancyProjectileExhaust;
        public int ClipSize;
        public string ContinuousFireOne;
        public string ContinuousFireTwo;
        public string ContinuousFireCoast;
        public string AutoReloadWhenIdle;
        public int[] ClipRelaodTime;
        public float DelayBetweenShots;
        public int ShotsPerBarrel;
        public string DamageDealtAtSelfPosition;
        public string RadiusDamageAffects;
        public string ProjectileCollidesWith;
        public string AntiAirborneVehicle;
        public string AntiGround;
        public string AntiProjectile;
        public string AntiSmallMissile;
        public string AntiMine;
        public string AntiParachute;
        public string AntiAirborneInfantry;
        public string AntiBallisticMissile;
        public string AutoReloadsClip;
        public string ProjectileStreamName; //Obsolete
        public string LaserName;
        public string WeaponBonus;
        public string HistoricBonusTime;
        public string HistoricBonusRadius;
        public string HistoricBonusCount;
        public string HistoricBonusWeapon;
        public string LeechRangeWeapon;
        public string ScatterTarget;
        public string CapableOfFollowingWaypoints;
        public string ShowsAmmoPips;
        public string AllowAttackGarrisonedBldgs;
        public string PlayFXWhenStealthed;
        public string PreAttackDelay;
        public string PreAttackType;
        public string ContinueAttackRange;
        public string FXTrigger;
        public bool AntiAirborneMonster;
        public float MaxWeaponSpeed;
        public float HitPercentage;
        public int PreAttackRandomAmount;
        public int FiringDuration;
        public bool IsAimingWeapon;
        public float IdleAfterFiringDelay;
        public bool HitStoredTarget;
        public float ClipReloadTime;
        public bool InstantLoadClipOnActivate;
        public string PreAttackFX;

        public DamageFieldNugget damageFieldNugget;
        public DamageNugget damageNugget;
        public MetaImpactNugget metaImpactNugget;
        private List<ProjectileNugget> ProjectileNuggets = new List<ProjectileNugget>();
        public WeaponOCLNugget weaponOCLNugget;
        public DOTNugget dotNugget;
        public ParalyzeNugget paralyzeNugget;
        public FireLogicNugget fireLogicNugget;
        public OpenGateNugget openGateNugget;
        public DamageContainedNugget damageContainedNugget;
        public HordeAttackNugget hordeAttackNugget;
        public LuaEventNugget luaEventNugget;
        public SlaveAttackNugget slaveAttackNugget;
        public AttributeModifierNugget attributeModifierNugget;
        public StealMoneyNugget stealMoneyNugget;

        public void AddProjectileNugget(ProjectileNugget pn)
        {
            ProjectileNuggets.Add(pn);
        }
    }

    class DamageFieldNugget
    {
        public string WeaponTemplateName;
        public int Duration;
    }

    class DamageNugget
    {
        public bool DamageArcInverted;
        public bool CylinderAOE;
        public string ForceKillObjectFilter;
        public int DamageMaxHeightAboveTerrain;
        public string DamageSubType;
        public string DamageTaperOff;
        public bool DrainLife;
        public float DrainLifeMultiplier;
        public int FlankingBonus;
        public string DamageScalar;
        public string RequiredUpgradeNames;
        public string ForbiddenUpgradeNames;
        public float Damage;
        public int DamageMaxHeight;
        public float DamageSpeed;
        public float Radius;
        public float DelayTime;
        public float DamageArc;
        public string DamageType;
        public string DamageFXType;
        public string DeathType;
        public string SpecialObjectFilter;
        public bool AcceptDamageAdd;
    }

    class MetaImpactNugget
    {
        public bool FlipDirection;
        public bool ShockWaveArcInverted;
        public float CyclonicFactor;
        public bool OnlyWhenJustDied;
        public float HeroResist;
        public int DelayTime;
        public float ShockWaveAmount;
        public float ShockWaveRadius;
        public float ShockWaveArc; //in degrees to each side 180 is full circle
        public float ShockWaveTaperOff;
        public float ShockWaveClearMult;
        public float ShockWaveClearFlingHeight;
        public float ShockWaveSpeed;
        public float ShockWaveZMult;
        public bool InvertShockWave;
        public bool ShockWaveClearRadius;
        public string SpecialObjectFilter;
        public string KillObjectFilter;
    }

    class ProjectileNugget
    {
        public string WeaponLaunchBoneSlotOverride;
        public string RequiredUpgradeNames;
        public string ForbiddenUpgradeNames;
        public string ProjectileTemplateName;
        public string WarheadTemplateName;
        public string SpecialObjectFilter;
        public bool UseAlwaysAttackOffset;
        public Vector3 AlwaysAttackHereOffset;
    }

    class WeaponOCLNugget
    {
        public string WeaponOCLName;
        public string ForbiddenUpgradeNames;
        public string RequiredUpgradeNames;
    }

    class DOTNugget
    {
        public string RequiredUpgradeNames;
        public string ForbiddenUpgradeNames;
        public bool AcceptDamageAdd;
        public float Damage;
        public float DamageScalar;
        public float Radius;
        public string DelayTime;
        public string DamageType;
        public string DamageFXType;
        public string DeathType;
        public int DamageInterval;
        public int DamageDuration;
        public string SpecialObjectFilter;
    }

    class ParalyzeNugget
    {
        public float Duration;
        public float Radius;
        public string ParalyzeFX;
        public string SpecialObjectFilter;
    }

    class FireLogicNugget
    {
        public string LogicType;
        public float Damage;
        public float Radius;
        public float MaxResistance;
        public float MinDecay;
        public int MinMaxBurnRate;
        public float DelayTime;
        public int DamageArc;
    }

    class OpenGateNugget
    {
        public float Radius;
    }

    class HordeAttackNugget
    {
        public string LockWeaponSlot;
    }

    class DamageContainedNugget
    {
        public string KillKindof;
        public string KillKindofNot;
        public string DeathType;
        public int KillCount;
    }

    class LuaEventNugget
    {
        public bool SendToNeutral;
        public bool SendToAllies;
        public bool SendToEnemies;
        public int Radius;
        public string LuaEvent;
    }

    class SlaveAttackNugget
    {

    }

    class AttributeModifierNugget
    {
        public float Radius;
        public string AntiCategories;
        public string AttributeModifier;
        public string DamageFXType;
        public string SpecialObjectFilter;
    }

    class StealMoneyNugget
    {
        public float AmountStolenPerAttack;
        public string RequiredUpgradeNames;
        public string SpecialObjectFilter;
        public string ForbiddenUpgradeNames;
    }
}
