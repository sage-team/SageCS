using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.Source.INI
{
    public struct FXList
    {
        public string Sound;
        public string RayEffect;
        public ViewShake ViewShake;
        public Tracer Tracer;
        public ParticleSystem[] ParticelSystem;
    }

    public struct ViewShake
    {
        public string Type;
    }

    public struct LightPulse
    {
        public string Color;
        public int Radius;
        public int IncreaseTime;
        public int DecreaseTime;
    }

    public struct Tracer
    {
        public float DecayAt;
        public int Length;
        public int Width;
        public string Color;
    }

    public struct ParticleSystem
    {
        public string Name;
        public bool OrientToObject;
        public int RotateY;
    }
}
