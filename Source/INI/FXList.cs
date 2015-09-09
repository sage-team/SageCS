using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.INI
{
    class FXList
    {
        public bool PlayEvenIfShrouded;
        private List<ParticleSystem> particleSystems = new List<ParticleSystem>();
        private List<Sound> sounds = new List<Sound>();
        private List<DynamicDecal> dynamicDecals = new List<DynamicDecal>();
        private List<TerrainScorch> terrainScorches = new List<TerrainScorch>();
        public BuffNugget buffNugget;
        public ViewShake viewShake;
        public CameraShakerVolume cameraShakerVolume;
        public TintDrawable tintDrawable;

        public void AddParticleSystem(ParticleSystem ps)
        {
            particleSystems.Add(ps);
        }

        public void AddDynamicDecal(DynamicDecal dd)
        {
            dynamicDecals.Add(dd);
        }

        public void AddTerrainScorch(TerrainScorch ts)
        {
            terrainScorches.Add(ts);
        }

        public void AddSound(Sound s)
        {
            sounds.Add(s);
        }
    }

    class ParticleSystem
    {
        public string Name;
        public int Count;
        public Vector3 Offset;
        public int Radius;
        public float Height;
        public float InitialDelay;
        public float RotateX;
        public float RotateY;
        public float RotateZ;
        public bool OrientToObject;
        public bool Ricochet;
        public bool AttachToObject;
        public bool CreateAtGroundHeight;
        public string CreateBoneOverride;
        public string TargetBoneOverride;
        public bool UseTargetOffset;
        public Vector3 TargetOffset;
        public bool OnlyIfOnWater;
        public bool OnlyIfOnLand;
        public string AttachToBone;
        public string Weather;
        public string ObjectFilter;
        public bool SetTargetMatrix;
        public int SystemLife;
        public string TargetCoeff;
    }

    class BuffNugget
    {
        public string BuffType;
        public string BuffInfantryTemplate;
        public string BuffCavalryTemplate;
        public string BuffTrollTemplate;
        public string BuffShipTemplate;
        public string BuffOrcTemplate;
        public string BuffMonsterTemplate;
        public bool IsComplexBuff;
        public int BuffLifeTime;
        public string BuffThingTemplate;
    }

    class Sound
    {
        public string Name;
        public string RequiredSourceModelConditions;
        public string SourceObjectFilter;
        public string ObjectFilter;
        public bool StopIfNuggetPlayed;
        public string ExcludedSourceModelConditions;
    }

    class EvaEvent
    {
        public string EvaEventOwner;
        public string EvaEventAlly;
        public string EvaEventEnemy;
    }

    class RayEffect
    {
        public string Name;
        public Vector3 PrimaryOffset;
        public Vector3 SecondaryOffset;
    }

    class Tracer
    {
        public string TracerName;
        public string BoneName;
        public float Speed;
        public float DecayAt;
        public float Length;
        public float Width;
        public Vector3 Color;
        public float Probability;
    }

    class LightPulse
    {
        public Vector3 Color;
        public float Radius;
        public float RadiusAsPercentOfObjectSize;
        public int IncreaseTime;
        public int DecreaseTime;
    }

    class CameraShakerVolume
    {
        public float Radius;
        public float Duration_Seconds;
        public float Amplitude_Degrees;
    }

    class ViewShake
    {
        public string Type;
        public string ObjectFilter;
    }

    class TerrainScorch
    {
        public string Type;
        public float Radius;
        public string Weather;
        public int[] RandomRange = new int[2];
    }

    class FXListAtBonePos
    {
        public string FX;
        public string BoneName;
    }

    class FXParticleSysBoneNugget
    {
        public string ParticleSysBone;
    }

    class AttachedModel
    {
        public string Modelname;
        public bool RandomlyRotate;
        public int ExpireTimer;
    }

    class DynamicDecal
    {
        public string DecalName;
        public float Lifetime;
        public float Size;
        public Vector3 Color;
        public Vector2 Offset;
        public int OpacityStart;
        public float OpacityFadeTimeOne;
        public int OpacityPeak;
        public float OpacityPeakTime;
        public float OpacityFadeTimeTwo;
        public int OpacityEnd;
        public float StartingDelay;
        public string Shader;
    }

    class Laser
    {
        public string LaserName;
        public bool LaserBackwards;
        public Vector3 TargetPositionOffsetFallback;
    }

    class TintDrawable
    {
        public Vector3 Color;
        public int PreColorTime;
        public int PostColorTime;
        public int SustainedColorTime;
        public int Frequency;
        public int Amplitude;
    }
}
