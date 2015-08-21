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
    class Upgrade
    {
        public string DisplayName;
        public string Type;         //PLAYER or OBJECT
        public float BuildTime;
        public float BuildCost;
        public string ButtonImage;
        public string ResearchSound;
        public string UnitSpecificSound;
        public string Tooltip;
        public string Cursor;
        public string ReserarchSound;
        public bool PersistsInCampaign;
        public string LocalPlayerGainsUpgradeEvaEvent;
        public string AlliedPlayerGainsUpgradeEvaEvent;
        public string EnemyPlayerGainsUpgradeEvaEvent;
        public string AlliedPlayerLosesUpgradeEvaEvent;
        public string EnemyPlayerLosesUpgradeEvaEvent;
        public string StrategicIcon;
        public bool NoUpgradeDiscount;
        public string UseObjectTemplateForCostDiscount;
        public string ResearchCompleteEvaEvent;
        public string RequiredObjectFilter;
        public string UpgradeFX;
        public string SkirmishAIHeuristic;
        public string SubUpgradeTemplateNames;


        public static void Parse(INIParser ip, string name)
        {
            Upgrade up = new Upgrade();
            string s;

            Dictionary<string, FieldInfo> fields = new Dictionary<string, FieldInfo>();
            //get all class variables
            foreach (var prop in up.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                fields.Add(prop.Name, prop);
            }
            do
            {
                ip.ParseLine();
                s = ip.getString();

                if (fields.ContainsKey(s))
                {
                    ip.SetValue(up, fields[s]);
                }
                else 
                {
                    if (!s.Equals("End"))
                        ip.PrintError("no such variable in Upgrade class: " + s);
                }
            }
            while (!s.Equals("End")); //also test END ?

            INIManager.AddUpgrade(name, up);
        }
    }
}
