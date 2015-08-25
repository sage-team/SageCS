using SageCS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.INI
{
    class AmbientStream
    {
        public string Filename;
        public float Volume;
        public string Type;
        public string Control;
        public float MinRange;
        public float MaxRange;
        public int DryLevel;
        public int ReverbEffectLevel;
        public string SubmixSlider;

        public static void Parse(INIParser ip, string name)
        {
            AmbientStream ast = new AmbientStream();
            string s;

            Dictionary<string, FieldInfo> fields = new Dictionary<string, FieldInfo>();
            //get all class variables
            foreach (var prop in ast.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                fields.Add(prop.Name, prop);
            }
            do
            {
                ip.ParseLine();
                s = ip.getString();

                if (fields.ContainsKey(s))
                {
                    ip.SetValue(ast, fields[s]);
                }
                else
                {
                    if (!s.Equals("End"))
                        ip.PrintError("no such variable in AmbientStream class: " + s);
                }
            }
            while (!s.Equals("End")); //also test END ?

            INIManager.AddAmbientStream(name, ast);
        }
    }
}
