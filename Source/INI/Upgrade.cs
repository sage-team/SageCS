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
        public string AlliedPlayerGainsUpgradeEvaEvent;
        public string AlliedPlayerLosesUpgradeEvaEvent;

        public float BuildCost;
        public float BuildTime;
        public string ButtonImage;

        public string Cursor;

        public string DisplayName;

        public string EnemyPlayerGainsUpgradeEvaEvent;
        public string EnemyPlayerLosesUpgradeEvaEvent;

        public string GroupName;
        public int GroupOrder;

        public string LocalPlayerGainsUpgradeEvaEvent;

        public bool NoUpgradeDiscount;

        public string ObjectFilter;

        public bool PersistsInCampaign;

        public string RequiredObjectFilter;
        public string ResearchCompleteEvaEvent;
        public string ResearchSound;

        public string SkirmishAIHeuristic;
        public string StrategicIcon;
        public string SubUpgradeTemplateNames;

        public string Tooltip;
        public string Type;         //PLAYER or OBJECT

        public string UnitSpecificSound;
        public float UpgradeBonusPercent;
        public string UpgradeFX;
        public string UpgradeMustBePresent;
        public string UseObjectTemplateForCostDiscount;

        public int WallUpgradeRadius;
    }
}
