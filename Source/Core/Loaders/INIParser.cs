using SageCS.Graphics;
using SageCS.INI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.Core
{
    class INIParser
    {
        public static void Parse(Stream s)
        {
            long filesize = s.Length;
            StreamReader sr = new StreamReader(s);

            INIObject obj = null;

            while (s.Position < filesize)
            {
                string[] data = ReadLine(sr);
                if (data != null)
                {
                    switch (data[0])
                    {
                        case "Object":
                            obj = new INIObject(data[1]);
                            break;
                        case "SelectPortrait":
                            obj.selectPortrait = data[2];
                            break;
                        case "ButtonImage":
                            obj.buttonImage = data[2];
                            break;
                        case "DefaultModelConditionState":
                            do
                            {
                                data = ReadLine(sr);
                                switch (data[0])
                                {
                                    case "Model":
                                        obj.model = data[2];
                                        break;
                                    case "Skeleton":
                                        obj.hierarchy = data[2];
                                        break;
                                }
                            }
                            while (!data[0].Equals("End"));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static string[] ReadLine(StreamReader sr)
        {
            char[] separators = new char[] { ' ', '\t' };
            string[] data = sr.ReadLine().Trim().Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length != 0 && !data[0].StartsWith(";") && !data[0].StartsWith("//"))
            {
                return data;
            }
            return null;
        }
    }
}
