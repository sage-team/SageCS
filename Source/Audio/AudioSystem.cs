using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace SageCS.Audio
{
    class AudioSystem
    {
        static AudioContext ac;
        static XRamExtension xram;
        static IList<string> devices;

        static bool mp3_sup;
        static bool eax_sup;
        public static void Init()
        {
            ac = new AudioContext();
            ac.CheckErrors();
            ac.MakeCurrent();
            eax_sup = ac.SupportsExtension("EAX3.0");
            if (eax_sup)
                xram = new XRamExtension();

            mp3_sup = ac.SupportsExtension("AL_EXT_mp3");
            devices = Alc.GetString(IntPtr.Zero, AlcGetStringList.AllDevicesSpecifier); 
        }
    }
}
