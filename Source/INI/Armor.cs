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
        private Dictionary<string, float> types = new Dictionary<string, float>();
        public int FlankedPenalty;
        public int DamageScalar;
        public string DamageFX;

        public void AddType(string key, float value)
        {
            if (!types.ContainsKey(key))
                types.Add(key, value);
            else
                types[key] = value;
        }

        public float getType(string key)
        {
            if (types.ContainsKey(key))
                return types[key];
            return types["DEFAULT"];
        }
    }
}
