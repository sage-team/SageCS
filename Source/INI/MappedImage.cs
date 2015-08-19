using SageCS.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public string Texture;
        public int TextureWidth;
        public int TextureHeight;
        public Coords coords;
        public string Status;

        public static void Parse(StreamReader sr, string name)
        {
            MappedImage mi = new MappedImage();
            string[] data;
            do
            {
                data = INIParser.ReadLine(sr);
                switch (data[0])
                {
                    case "Texture":
                        mi.Texture = data[2];
                        break;
                    case "TextureWitdth":
                        mi.TextureWidth = int.Parse(data[2]);
                        break;
                    case "TextureHeight":
                        mi.TextureHeight = int.Parse(data[2]);
                        break;
                    case "Coords":
                        Coords co = new Coords();
                        co.Left = int.Parse(data[2].Replace("Left:", ""));
                        co.Top = int.Parse(data[3].Replace("Top:", ""));
                        co.Right = int.Parse(data[4].Replace("Right:", ""));
                        co.Bottom = int.Parse(data[5].Replace("Bottom:", ""));
                        mi.coords = co;
                        break;
                    case "Status":
                        mi.Status = data[2];
                        break;
                }
            }
            while (!data[0].Equals("End")); //also test END ?

            INIManager.AddMappedImage(name, mi);
        }
    }
}
