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
        public BuffNugget buffNugget;
        public CameraShakerVolume cameraShakerVolume;
        private List<DynamicDecal> dynamicDecals = new List<DynamicDecal>();
        private List<ParticleSystem> particleSystems = new List<ParticleSystem>();
        public bool PlayEvenIfShrouded;
        private List<Sound> sounds = new List<Sound>();
        private List<TerrainScorch> terrainScorches = new List<TerrainScorch>();
        public TintDrawable tintDrawable;
        public ViewShake viewShake;

        public void AddDynamicDecal(DynamicDecal dd)
        {
            dynamicDecals.Add(dd);
        }

        public void AddParticleSystem(ParticleSystem ps)
        {
            particleSystems.Add(ps);
        }

        public void AddSound(Sound s)
        {
            sounds.Add(s);
        }

        public void AddTerrainScorch(TerrainScorch ts)
        {
            terrainScorches.Add(ts);
        }
    }


    class AttachedModel
    {
        public int ExpireTimer;
        public string Modelname;
        public bool RandomlyRotate;
    }

    class BuffNugget
    {
        public string BuffCavalryTemplate;
        public string BuffInfantryTemplate;
        public int BuffLifeTime;
        public string BuffMonsterTemplate;
        public string BuffOrcTemplate;
        public string BuffShipTemplate;
        public string BuffThingTemplate;
        public string BuffTrollTemplate;
        public string BuffType;
        public bool IsComplexBuff;
    }

    class CameraShakerVolume
    {
        public float Amplitude_Degrees;
        public float Duration_Seconds;
        public float Radius;
    }

    class DynamicDecal
    {
        public Vector3 Color;
        public string DecalName;
        public float Lifetime;
        public Vector2 Offset;
        public int OpacityEnd;
        public float OpacityFadeTimeOne;
        public float OpacityFadeTimeTwo;
        public int OpacityPeak;
        public float OpacityPeakTime;
        public int OpacityStart;
        public string Shader;
        public float Size;
        public float StartingDelay;
    }

    class EvaEvent
    {
        public string EvaEventAlly;
        public string EvaEventEnemy;
        public string EvaEventOwner;
    }

    class FXListAtBonePos
    {
        public string BoneName;
        public string FX;
    }

    class FXParticleSysBoneNugget
    {
        public string ParticleSysBone;
    }

    class Laser
    {
        public bool LaserBackwards;
        public string LaserName;
        public Vector3 TargetPositionOffsetFallback;
    }

    class LightPulse
    {
        public Vector3 Color;
        public int DecreaseTime;
        public int IncreaseTime;
        public float Radius;
        public float RadiusAsPercentOfObjectSize;
    }

    class ParticleSystem
    {
        public string AttachToBone;
        public bool AttachToObject;
        public int Count;
        public bool CreateAtGroundHeight;
        public string CreateBoneOverride;
        public float Height;
        public float InitialDelay;
        public string Name;
        public string ObjectFilter;
        public Vector3 Offset;
        public bool OnlyIfOnLand;
        public bool OnlyIfOnWater;
        public bool OrientToObject;
        public int Radius;
        public bool Ricochet;
        public float RotateX;
        public float RotateY;
        public float RotateZ;
        public bool SetTargetMatrix;
        public int SystemLife;
        public string TargetBoneOverride;
        public string TargetCoeff;
        public Vector3 TargetOffset;
        public bool UseTargetOffset;
        public string Weather;    
    }

    class RayEffect
    {
        public string Name;
        public Vector3 PrimaryOffset;
        public Vector3 SecondaryOffset;
    }

    class Sound
    {
        public string ExcludedSourceModelConditions;
        public string Name;
        public string ObjectFilter;
        public string RequiredSourceModelConditions;
        public string SourceObjectFilter;
        public bool StopIfNuggetPlayed;
    }

    class TerrainScorch
    {
        public int[] RandomRange = new int[2];
        public float Radius;
        public string Type;
        public string Weather;
    }

    class TintDrawable
    {
        public int Amplitude;
        public Vector3 Color;
        public int Frequency;
        public int PostColorTime;
        public int PreColorTime;
        public int SustainedColorTime;
    }

    class Tracer
    {
        public string BoneName;
        public Vector3 Color;
        public float DecayAt;
        public float Length;
        public float Probability;
        public float Speed;
        public string TracerName;
        public float Width;
    }

    class ViewShake
    {
        public string ObjectFilter;
        public string Type;
    }
}
