using SageCS.Core.Loaders;
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
    public class INIParser : StreamReader
    {
        public static Dictionary<string, string> macros = new Dictionary<string, string>();

        private string[] data;
        private string line;
        private int index = 0;
        private int lineNumber = 0;

        public INIParser(Stream str) : base(str)
        {
            long filesize = str.Length;
            while (str.Position < filesize)
            {
                //ParseLine();
                //string s = getString();
                Console.WriteLine(base.ReadLine());
                /*
                switch (s)
                {
                    case "#define":
                        string s1 = getString();
                        string s2 = getStrings();
                        Console.WriteLine(s1);
                        Console.WriteLine(s2);
                        //macros.Add(s1, s2);
                        break;

                    case "GameData":
                        //INI.GameData.Parse(this);
                        break;
                    case "Object":
                        //INI.Object.Parse(this, getString());
                        break;
                    case "MappedImage":
                        //INI.MappedImage.Parse(this, getString());
                        break;
                    default:
                        //Console.WriteLine("##INI: unhandled entry: " + data[0] + " in file "); //+ sr.BaseStream.Name);
                        break;
                }
                */
            }
        }

        public string[] ParseLine()
        {
            char[] separators = new char[] { ' ', '\t' };
            line = base.ReadLine().Trim();
            if (line.Contains(";"))
                line = line.Remove(line.IndexOf(";"));
            if (line.Contains("//"))
                line = line.Remove(line.IndexOf("//"));
            lineNumber++;
            Console.WriteLine(line);
            data = line.Replace("=", "").Split(separators, StringSplitOptions.RemoveEmptyEntries);
            index = 0;

            //insert the values from the macros
            for (int i = 0; i < data.Length; i++)
            {
                if (macros.ContainsKey(data[i]))
                    data[i] = macros[data[i]];
                if (data[i].Equals("Yes"))
                    data[i] = "True";
                else if (data[i].Equals("No"))
                    data[i] = "False";
            }
            if (data.Length != 0 && !data[0].StartsWith(";") && !data[0].StartsWith("//"))
                return data;
            return ParseLine();
        }

        public bool HasNext()
        {
            index++;
            return (index < data.Length);
        }

        public string getString()
        {
            if (!HasNext())
            {
                PrintError("insufficient amount of values!!");
                throw new IndexOutOfRangeException();
            }
            return data[index];
        }

        public string getStrings()
        {
            string result = "";
            while(HasNext())
            {
                result += getString() + " ";
            }
            return result;
        }

        public int getInt()
        {
            int result;
            string s = getString();
            if (s.Contains("%"))
                s.Replace("%", "");
            if (int.TryParse(s, out result))
                return result;
            else
            {
                PrintError(s + " could not be parsed as integer value!!");
                throw new FormatException();
            }
        }
 
        public float getFloat()
        {
            float result;
            string s = getString();
            if (float.TryParse(s, out result))
                return result;
            else
            {
                PrintError(s + " could not be parsed as float value!!");
                throw new FormatException();
            }
        }

        private void PrintError(string message)
        {
            Console.WriteLine("### INI ERROR ###");
            Console.WriteLine("# in file: " + ((BigStream)this.BaseStream).Name);
            Console.WriteLine("# at line: " + lineNumber + "     " + line);
            Console.WriteLine("# " + message);
            Console.WriteLine("#################");
            Console.WriteLine(" ");
        }
    }
}
