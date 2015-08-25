using SageCS.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.INI
{
    class Armor
    {
        private Dictionary<string, int> types = new Dictionary<string, int>();

        public static void Parse(INIParser ip, string name)
        {
            Armor ar = new Armor();
            string s;
            do
            {
                ip.ParseLine();
                s = ip.getString();
                switch (s)
                {
                    case "DEFAULT":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "FORCE":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "CRUSH":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "SLASH":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "PIERCE":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "SIEGE":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "STRUCTURAL":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "FLAME":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "HEALING":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "UNRESISTABLE":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "WATER":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "PENALTY":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "FALLING":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "TOPPLING":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "REFLECTED":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "PASSENGER":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "MAGIC":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "CHOP":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "HERO":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "SPECIALIST":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "URUK":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "HERO_RANGED":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "FLY_INTO":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "UNDEFINED":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "LOGICAL_FIRE":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "CAVALRY":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "CAVALRY_RANGED":
                        ar.types.Add(s, ip.getInt());
                        break;
                    case "POISON":
                        ar.types.Add(s, ip.getInt());
                        break;
                    default:
                        ip.PrintError("invalid type: " + s);
                        break;
                }
            }
            while (!s.Equals("End")); //also test END ?

            INIManager.AddArmor(name, ar);
        }

        public int getType(string key)
        {
            if (types.ContainsKey(key))
                return types[key];
            return types["DEFAULT"];
        }
    }
}
