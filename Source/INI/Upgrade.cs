using SageCS.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.INI
{
    class Upgrade
    {
        public string DisplayName;
        public string Type;         //PLAYER or OBJECT
        public int BuildTime;
        public int BuildCost;
        public string ButtonImage;
        public string ResearchSound;
        public string UnitSpecificSound;

        private static void Parse(INIParser ip, string name)
        {
            Upgrade up = new Upgrade();
            string[] data;
            do
            {
                data = ip.ParseLine();
                switch (data[0])
                {
                    case "DispayName":
                        up.DisplayName = data[2];
                        break;
                }
            }
            while (!data[0].Equals("End")); //also test END ?

            INIManager.AddUpgrade(name, up);
        }
    }
}
