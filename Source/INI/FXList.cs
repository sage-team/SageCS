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
        List<ParticleSystem> particleSystems;
        List<Sound> sounds;
        List<DynamicDecal> dynamicDecals;
        List<TerrainScorch> terrainScorches;
        BuffNugget buffNugget;
        ViewShake viewShake;
        CameraShakerVolume cameraShakerVolume;
        TintDrawable tintDrawable;

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
        public int Offset;
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
    }

    class BuffNugget
    {
        public string BuffType;
        public string BuffInfantryTemplate;
        public string BuffCavalryTemplate;
        public string BuffTrollTemplate;
        public string BuffShipTemplate;
        public string BuffMonsterTemplate;
        public bool IsComplexBuff;
        public int BuffLifeTime;
    }

    class Sound
    {
        public string Name;
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
        public Vector4 Color;
        public float Probability;
    }

    class LightPulse
    {
        public Vector4 Color;
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
    }

    class TerrainScorch
    {
        public string Type;
        public float Radius;
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
        public float Size;
        public Vector4 Color;
        public Vector2 Offset;
        public int OpacityStart;
        public float OpacityFadeTimeOne;
        public int OpacityPeak;
        public float OpacityPeakTime;
        public float OpacityFadeTimeTwo;
        public int OpacityEnd;
        public float StartingDelay;
        public float LifeTime;

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
