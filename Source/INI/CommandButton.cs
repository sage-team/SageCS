using SageCS.Core;
using SageCS.INI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.INI
{
    class CommandButton
    {
        public string Command;
        public string Options;
        public string Object;
        public string CursorName;
        public string InvalidCursorName;
        public string TextLabel;
        public string ButtonImage;
        public string DescriptLabel;
        public string ButtonBorderType;
        public bool DoubleClick;
        public bool Radial;
        public bool InPalantir;
        public string UnitSpecificSound;
        public string Stances;

        public static void Parse(INIParser ip, string name)
        {
            CommandButton cb = new CommandButton();
            string s;

            Dictionary<string, FieldInfo> fields = new Dictionary<string, FieldInfo>();
            //get all class variables
            foreach (var prop in cb.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                fields.Add(prop.Name, prop);
            }
            do
            {
                ip.ParseLine();
                s = ip.getString();

                if (fields.ContainsKey(s))
                {
                    ip.SetValue(cb, fields[s]);
                }
                else
                {
                    if (!s.Equals("End"))
                        ip.PrintError("no such variable in CommandButton class: " + s);
                }
            }
            while (!s.Equals("End")); //also test END ?

            INIManager.AddCommandButton(name, cb);
        }
    }
}
