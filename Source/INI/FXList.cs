using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.Source.INI
{
    [Serializable]
    struct FXList
    {
        public string Sound;
        public string RayEffect;
        public ViewShake ViewShake;
        public Tracer Tracer;
        public ParticleSystem[] ParticelSystem;
    }

    [Serializable]
    struct ViewShake
    {
        public string Type;
    }

    [Serializable]
    struct LightPulse
    {
        public string Color;
        public int Radius;
        public int IncreaseTime;
        public int DecreaseTime;
    }

    [Serializable]
    struct Tracer
    {
        public float DecayAt;
        public int Length;
        public int Width;
        public string Color;
    }

    [Serializable]
    struct ParticleSystem
    {
        public string Name;
        public bool OrientToObject;
        public int RotateY;
    }
}
