using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.INI
{
    class Multisound
    {
        public string Control;
        private List<MusicTrack> subsounds = new List<MusicTrack>();

        public void AddMusicTrack(MusicTrack mt)
        {
            subsounds.Add(mt);
        }
    }
}
