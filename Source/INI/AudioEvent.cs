using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.INI
{
    class AudioEvent
    {
        public string Control;
        public int DryLevel;
        public int Limit;
        public float MaxRange;
        public float MinRange;
        public int MinVolume;
        public int[] PitchShift = new int[2];
        public int PlayPercent;
        public string Priority;
        public int ReverbEffectLevel;
        private List<MusicTrack> sounds = new List<MusicTrack>();
        public string SubmixSlider;
        public string Type;
        public float Volume;
        public int VolumeShift;
        public int ZoomedInOffscreenVolumePercent;
        public int ZoomedInOffscreenMinVolumePercent;
        public int ZoomedInOffscreenOcclusionPercent;

        public void AddMusicTrack(MusicTrack mt)
        {
            sounds.Add(mt);
        }
    }
}
