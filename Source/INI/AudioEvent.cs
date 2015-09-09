using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.INI
{
    class AudioEvent
    {
        private List<MusicTrack> sounds = new List<MusicTrack>();
        public float Volume;
        public int MinVolume;
        public float MinRange;
        public float MaxRange;
        public string Priority;
        public int Limit;
        public string Type;
        public string SubmixSlider;
        public int[] PitchShift = new int[2];
        public int PlayPercent;
        public int DryLevel;
        public int ReverbEffectLevel;
        public int ZoomedInOffscreenVolumePercent;
        public int ZoomedInOffscreenMinVolumePercent;
        public int ZoomedInOffscreenOcclusionPercent;
        public string Control;
        public int VolumeShift;

        public void AddMusicTrack(MusicTrack mt)
        {
            sounds.Add(mt);
        }
    }
}
