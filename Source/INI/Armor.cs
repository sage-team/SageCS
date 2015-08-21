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

        public Armor(INIParser ip, string name)
        {
            string s;
            do
            {
                ip.ParseLine();
                s = ip.getString();
                switch (s)
                {
                    case "DEFAULT":
                        types.Add(s, ip.getInt());
                        break;
                    case "FORCE":
                        types.Add(s, ip.getInt());
                        break;
                    case "CRUSH":
                        types.Add(s, ip.getInt());
                        break;
                    case "SLASH":
                        types.Add(s, ip.getInt());
                        break;
                    case "PIERCE":
                        types.Add(s, ip.getInt());
                        break;
                    case "SIEGE":
                        types.Add(s, ip.getInt());
                        break;
                    case "STRUCTURAL":
                        types.Add(s, ip.getInt());
                        break;
                    case "FLAME":
                        types.Add(s, ip.getInt());
                        break;
                    case "HEALING":
                        types.Add(s, ip.getInt());
                        break;
                    case "UNRESISTABLE":
                        types.Add(s, ip.getInt());
                        break;
                    case "WATER":
                        types.Add(s, ip.getInt());
                        break;
                    case "PENALTY":
                        types.Add(s, ip.getInt());
                        break;
                    case "FALLING":
                        types.Add(s, ip.getInt());
                        break;
                    case "TOPPLING":
                        types.Add(s, ip.getInt());
                        break;
                    case "REFLECTED":
                        types.Add(s, ip.getInt());
                        break;
                    case "PASSENGER":
                        types.Add(s, ip.getInt());
                        break;
                    case "MAGIC":
                        types.Add(s, ip.getInt());
                        break;
                    case "CHOP":
                        types.Add(s, ip.getInt());
                        break;
                    case "HERO":
                        types.Add(s, ip.getInt());
                        break;
                    case "SPECIALIST":
                        types.Add(s, ip.getInt());
                        break;
                    case "URUK":
                        types.Add(s, ip.getInt());
                        break;
                    case "HERO_RANGED":
                        types.Add(s, ip.getInt());
                        break;
                    case "FLY_INTO":
                        types.Add(s, ip.getInt());
                        break;
                    case "UNDEFINED":
                        types.Add(s, ip.getInt());
                        break;
                    case "LOGICAL_FIRE":
                        types.Add(s, ip.getInt());
                        break;
                    case "CAVALRY":
                        types.Add(s, ip.getInt());
                        break;
                    case "CAVALRY_RANGED":
                        types.Add(s, ip.getInt());
                        break;
                    case "POISON":
                        types.Add(s, ip.getInt());
                        break;
                    default:
                        ip.PrintError("invalid type: " + s);
                        break;
                }
            }
            while (!s.Equals("End")); //also test END ?

            INIManager.AddArmor(name, this);
        }

        public int getType(string key)
        {
            if (types.ContainsKey(key))
                return types[key];
            return types["DEFAULT"];
        }
    }
}
