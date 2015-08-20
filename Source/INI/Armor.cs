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
        public int DEFAULT;
        public int FORCE;
        public int CRUSH;
        public int SLASH;
        public int PIERCE;
        public int SIEGE;
        public int STRUCTURAL;
        public int FLAME;
        public int HEALING;
        public int UNRESISTABLE;
        public int WATER;
        public int PENALTY;
        public int FALLING;
        public int TOPPLING;
        public int REFLECTED;
        public int PASSENGER;
        public int MAGIC;
        public int CHOP;
        public int HERO;
        public int SPECIALIST;
        public int URUK;
        public int HERO_RANGED;
        public int FLY_INTO;
        public int UNDEFINED;
        public int LOGICAL_FIRE;
        public int CAVALRY;
        public int CAVALRY_RANGED;
        public int POISON;

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
                        ar.DEFAULT = ip.getInt();
                        break;
                }
            }
            while (!s.Equals("End")); //also test END ?

            INIManager.AddArmor(name, ar);
        }
    }
}
