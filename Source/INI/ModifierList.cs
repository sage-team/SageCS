using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.INI
{
    class ModifierList
    {
        public string Category;
        public string ClearModelCondition;
        public int Duration;
        public string EndFX;
        public string EndFX2;
        public string EndFX3;
        public string FX;
        public string FX2;
        public string FX3;
        public bool IgnoreIfAnticategoryActive;
        public string ModelCondition;
        public Dictionary<string, string> modifiers = new Dictionary<string, string>();
        public bool MultiLevelFX;
        public bool ReplaceInCategoryIfLongest;
        public string Upgrade;

        public void AddModifier(string key, string value)
        {
            if (!modifiers.ContainsKey(key))
                modifiers.Add(key, value);
            else
                modifiers[key] = value;
        }
    }
}
