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
        public Dictionary<string, string> modifiers = new Dictionary<string, string>();
        public int Duration;
        public string FX;
        public string FX2;
        public string FX3;
        public bool MultiLevelFX;
        public string EndFX;
        public string EndFX2;
        public string EndFX3;
        public bool ReplaceInCategoryIfLongest;
        public bool IgnoreIfAnticategoryActive;
        public string ModelCondition;
        public string ClearModelCondition;
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
