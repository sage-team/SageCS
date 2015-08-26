using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace SageCS.Audio
{
    class Sound
    {
        private int buf;
        private int source;

        public Sound()
        {
            buf = AL.GenBuffer();
            source = AL.GenSource();
        }

        public void Play()
        {
            AL.SourcePlay(source);
        }

        public void Pause()
        {
            AL.SourcePause(source);
        }

        public void Stop()
        {
            AL.SourceStop(source);
        }

        public ALSourceState GetStatus()
        {
            return AL.GetSourceState(source);
        }

        ~Sound()
        {
            AL.DeleteSource(source);
            AL.DeleteBuffer(buf);
        }
    }
}
