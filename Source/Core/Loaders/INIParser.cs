using SageCS.Core.Loaders;
using SageCS.Graphics;
using SageCS.INI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
                ParseLine();
                string s = getString();
                switch (s)
                {
                    case "#include":
                        PrintError("include");
                        break;
                    case "#define":
                        macros.Add(getString(), getStrings());
                        break;

                    case "GameData":
                        GameData.parse(this);
                        break;
                    case "Object":
                        //Object.Parse(this, getString());
                        break;
                    case "MappedImage":
                        MappedImage.Parse(this, getString());
                        break;
                    case "Upgrade":
                        Upgrade.Parse(this, getString());
                        break;
                    case "Weapon":
                        
                        break;
                    default:
                        //PrintError("unhandled entry: " + data[0]);
                        break;
                }
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
            data = line.Replace("=", "").Split(separators, StringSplitOptions.RemoveEmptyEntries);
            index = 0;

            //insert the values from the macros
            for (int i = 0; i < data.Length; i++)
            {
                //if the string contains a '_' it should be a macro -> cast to upper case
                if (data[i].Contains('_'))
                    data[i] = data[i].ToUpper();
                if (macros.ContainsKey(data[i]))
                    data[i] = macros[data[i]];
            }
            if (data.Length != 0 && !data[0].StartsWith(";") && !data[0].StartsWith("//"))
                return data;
            return ParseLine();
        }

        public void SetValue(object ob, FieldInfo info)
        {
            Type type = info.FieldType;
            if (type == typeof(string))
                info.SetValue(ob, getString());
            else if (type == typeof(int))
                info.SetValue(ob, getInt());
            else if (type == typeof(float))
                info.SetValue(ob, getFloat());
            else if (type == typeof(bool))
                info.SetValue(ob, getBool());
            else
                PrintError(" invalid type: " + type);
        }

        private bool HasNext()
        {
            return (index < data.Length);
        }

        public string getString()
        {
            if (!HasNext())
            {
                PrintError("insufficient amount of values!!");
                throw new IndexOutOfRangeException();
            }
            return data[index++];
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
            s = s.Replace("%", "");
            s = s.Replace("Left:", "");
            s = s.Replace("Top:", "");
            s = s.Replace("Right:", "");
            s = s.Replace("Bottom:", "");
            s = s.Replace("Min:", "");
            s = s.Replace("Max:", "");
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
            s = s.Replace("%", "");
            s = s.Replace("f", "");
            if (float.TryParse(s, out result))
                return result;
            else
            {
                PrintError(s + " could not be parsed as float value!!");
                throw new FormatException();
            }
        }

        public bool getBool()
        {
            bool result;
            string s = getString();
            s = s.Replace("Yes", "True");
            s = s.Replace("No", "False");
            if (bool.TryParse(s, out result))
                return result;
            else
            {
                PrintError(s + " could not be parsed as boolean value!!");
                throw new FormatException();
            }
        }

        public void PrintError(string message)
        {
            //Console.WriteLine("### INI ERROR ###");
            //Console.WriteLine("# in file: " + ((BigStream)this.BaseStream).Name);
            //Console.WriteLine("# at line: " + lineNumber + "     " + line);
            //Console.WriteLine("# " + message);
            //Console.WriteLine("#################");
            //Console.WriteLine(" ");
        }
    }
}
