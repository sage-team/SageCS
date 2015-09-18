using SageCS.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.INI
{
    class MappedImage
    {
        public struct Coords
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public Coords coords;
        public string Status;
        public string Texture;
        public int TextureWidth;
        public int TextureHeight;
    }
}
