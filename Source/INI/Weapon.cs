using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.INI
{
    public struct Weapon
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
        public int WeaponSpeed;
        public int MinWeaponSpeed;
        public int ScaleWeaponSpeed;
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
        public string PorjectileExhause;
        public string VeterancyFireFX;
        public string VeterancyProjectileDetonationFX;
        public string VeterancyFireOCL;
        public string VeterancyProjectileDetonationOCL;
        public string VeterancyProjectileExhaust;
        public int ClipSize;
        public string ContinuousFireOne;
        public string ContinuousFireTwo;
        public string ContinuousireCoast;
        public string AutoReloadWhenIdle;
        public int ClipRelaodTime;
        public int DelayBetweenShots;
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
        public string SuspendFXDelay;
    }

    public struct DamageFieldNugget
    {
        public string WeaponTemplateName;
        public int Duration;
    }

    public struct DamageNugget
    {
        public int Damage;
        public float Radius;
        public int DelayTime;
        public string DamageType;
        public string DamageFXType;
        public string DeathType;
    }

    public struct MetaImpactNugget
    {
        public float HeroResist;
        public float ShockWaveAmount;
        public float ShockWaveRadius;
        public int ShockWaveArc; //in degrees to each side 180 is full circle
        public float ShockWaveTaperOff;
        public float ShockWaveSpeed;
        public float ShockWaveZMult;
        public bool InvertShockWave;
    }

    public struct ProjectileNugget
    {
        public string ProjectileTemplateName;
        public string WarheadTemplateName;
    }

    public struct WeaponOCLNugget
    {
        public string WeaponOCLName;
    }

    public struct AttributeModifierNugget
    {
        public string AttributeModifier;
        public string DamageFXType;
    }

    public struct StealMoneyNugget
    {
        public float AmountStolenPerAttack;
    }
}
