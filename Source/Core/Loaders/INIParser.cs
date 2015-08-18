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
        private static Dictionary<string, string> macros = new Dictionary<string, string>();

        public static void Parse(Stream s)
        {
            long filesize = s.Length;
            StreamReader sr = new StreamReader(s);

            while (s.Position < filesize)
            {
                string[] data = ReadLine(sr);
                if (data != null)
                {
                    switch (data[0])
                    {
                        case "#define":
                            string value = "";
                            for (int i = 2; i < data.Length; i++)
                            {
                                value += data[i];
                            }
                            macros.Add(data[1], value);
                            break;
                        case "Object":
                            break;
                        case "SelectPortrait":
                            break;
                        case "ButtonImage":
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
            string line = sr.ReadLine().Trim();
            if (line.Contains(";"))
            {
                line = line.Remove(line.IndexOf(";"));
            }
            if (line.Contains("//"))
            {
                line = line.Remove(line.IndexOf("//"));
            }
            string[] data = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length != 0 && !data[0].StartsWith(";") && !data[0].StartsWith("//"))
            {
                return data;
            }
            return null;
        }
    }
}
