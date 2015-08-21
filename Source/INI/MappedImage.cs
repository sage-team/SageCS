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

        public string Texture;
        public int TextureWidth;
        public int TextureHeight;
        public Coords coords;
        public string Status;

        public static void Parse(INIParser ip, string name)
        {
            MappedImage mi = new MappedImage();
            string s;

            Dictionary<string, FieldInfo> fields = new Dictionary<string, FieldInfo>();
            //get all class variables
            foreach (var prop in mi.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                fields.Add(prop.Name, prop);
            }
            do
            {
                ip.ParseLine();
                s = ip.getString();

                if (fields.ContainsKey(s))
                {
                    ip.SetValue(mi, fields[s]);
                }
                else
                {
                    if (!s.Equals("End"))
                        ip.PrintError("no such variable in MappedImage class: " + s);
                }
            }
            while (!s.Equals("End")); //also test END ?

            INIManager.AddMappedImage(name, mi);
        }
    }
}
