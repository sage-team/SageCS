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
        private static StreamReader sr;

        public static Dictionary<string, string> macros = new Dictionary<string, string>();

        public static void Parse(Stream s)
        {
            long filesize = s.Length;
            sr = new StreamReader(s);

            while (s.Position < filesize)
            {
                string[] data = ReadLine(sr);
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

                    case "GameData":
                        INI.GameData.Parse(sr);
                        break;
                    case "Object":
                        INI.Object.Parse(sr, data[1]);
                        break;
                    case "MappedImage":
                        INI.MappedImage.Parse(sr, data[1]);
                        break;
                    default:
                        //Console.WriteLine("##INI: unhandled entry: " + data[0] + " in file "); //+ sr.BaseStream.Name);
                        break;
                }
            }
        }

        public static string[] ReadLine(StreamReader sr)
        {
            char[] separators = new char[] { ' ', '\t' };
            string line = sr.ReadLine().Trim();
            if (line.Contains(";"))
                line = line.Remove(line.IndexOf(";"));
            if (line.Contains("//"))
                line = line.Remove(line.IndexOf("//"));
            string[] data = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            //insert the values from the macros
            for (int i = 0; i < data.Length; i++)
            {
                if (macros.ContainsKey(data[i]))
                {
                    data[i] = macros[data[i]];
                }
                if (data[i].Equals("Yes"))
                    data[i] = "True";
                else if (data[i].Equals("No"))
                    data[i] = "False";
            }
            if (data.Length != 0 && !data[0].StartsWith(";") && !data[0].StartsWith("//"))
                return data;
            return ReadLine(sr);
        }
    }
}
