using OpenTK;
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
                string name;
                switch (s)
                {
                    case "#include":
                        PrintError("include");
                        break;
                    case "#define":
                        macros.Add(getString().ToUpper(), getStrings());
                        break;
                    

                    case "GameData":
                        //GameData data = new GameData();
                        //ParseObject(data);
                        //INIManager.SetGameData(data);
                        break;
                    case "Object":
                        //INI.Object.Parse(this, getString());
                        break;
                    case "MappedImage":
                        //MappedImage.Parse(this, getString());
                        break;
                    case "Upgrade":
                        //Upgrade.Parse(this, getString());
                        break;
                    case "Weapon":
                        //Weapon.Parse(this, getString());
                        break;
                    case "ModifierList":
                        ModifierList ml = new ModifierList();
                        name = getString();
                        ParseObject(ml);
                        INIManager.AddModifierList(name, ml);
                        break;
                    case "Armor":
                        Armor ar = new Armor();
                        name = getString();
                        ParseObject(ar);
                        INIManager.AddArmor(name, ar);
                        break;
                    case "AmbientStream":
                        AmbientStream ast = new AmbientStream();
                        name = getString();
                        ParseObject(ast);
                        INIManager.AddAmbientStream(name, ast);
                        break;
                    case "CommandButton":
                        CommandButton cb = new CommandButton();
                        name = getString();
                        ParseObject(cb);
                        INIManager.AddCommandButton(name, cb);
                        break;
                    default:
                        //PrintError("unhandled entry: " + data[0]);
                        break;
                }
            }
        }

        private void ParseObject(object o)
        {
            string s;

            Dictionary<string, FieldInfo> fields = new Dictionary<string, FieldInfo>();
            //get all class variables
            foreach (var prop in o.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                fields.Add(prop.Name, prop);
            }
            do
            {
                ParseLine();
                s = getString();

                if (s.Equals("Armor"))
                {
                    ((Armor)o).AddType(getString(), getFloat());
                }
                else if (s.Equals("Modifier"))
                {
                    ((ModifierList)o).AddModifier(getString(), getFloat());
                }
                else if (fields.ContainsKey(s))
                {
                    Type type = fields[s].FieldType;
                    if (type == typeof(string))
                        fields[s].SetValue(o, getString());
                    else if (type == typeof(int))
                        fields[s].SetValue(o, getInt());
                    else if (type == typeof(float))
                        fields[s].SetValue(o, getFloat());
                    else if (type == typeof(float[]))
                        fields[s].SetValue(o, getFloats());
                    else if (type == typeof(bool))
                        fields[s].SetValue(o, getBool());
                    else if (type == typeof(OpenTK.Vector2))
                        fields[s].SetValue(o, getVec2());
                    else if (type == typeof(OpenTK.Vector3))
                        fields[s].SetValue(o, getVec3());
                    else
                        PrintError(" invalid type: " + type);
                }
                else
                {
                    if (!s.Equals("End") && !s.Equals("END"))
                        PrintError("invalid parameter in " + o.GetType() + " class: " + s);
                }
            }
            while (!s.Equals("End") && !s.Equals("END")); 
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
                if (macros.ContainsKey(data[i].ToUpper()))
                    data[i] = macros[data[i].ToUpper()];
            }
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Equals("#MULTIPLY("))
                    data[i] = data[i + 1] + "*" + data[i + 2];
            }
            if (data.Length != 0 && !data[0].StartsWith(";") && !data[0].StartsWith("//"))
                return data;
            return ParseLine();
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
            s = s.Replace("X:", "").Replace("Y:", "").Replace("Z:", "");
            s = s.Replace("R:", "").Replace("G:", "").Replace("B:", "");
            s = s.Replace("MP1:", "").Replace("MP2:", "").Replace("MP3:", "").Replace("MP4:", "").Replace("MP5:", "").Replace("MP6:", "").Replace("MP7:", "").Replace("MP8:", "");
            if (s.Contains("*"))
            {
                string[] vals = s.Split('*');
                float one, two;
                if (float.TryParse(vals[0], out one) && float.TryParse(vals[1], out two))
                    return one * two;
                else
                    PrintError(s + " could not be parsed as float value!!");
                    throw new FormatException();
            }
            else if (float.TryParse(s, out result))
                return result;
            else
            {
                PrintError(s + " could not be parsed as float value!!");
                throw new FormatException();
            }
        }

        public float[] getFloats()
        {
            List<float> f = new List<float>();
            while(HasNext())
            {
                f.Add(getFloat());
            }
            return f.ToArray<float>();
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

        public Vector2 getVec2()
        {
            return new Vector2(getFloat(), getFloat());
        }

        public Vector3 getVec3()
        {
            return new Vector3(getFloat(), getFloat(), getFloat());
        }

        public void PrintError(string message)
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
