using SageCS.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.INI
{
    class INIObject
    {
        public static Dictionary<string, Object> objects = new Dictionary<string, Object>();

        public string selectPortrait;
        public string buttonImage;

        public string model;
        public string hierarchy;

        public INIObject(string name)
        {
            objects.Add(name, this);
        }
    }
}
