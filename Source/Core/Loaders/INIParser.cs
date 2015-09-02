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

        private List<string> includedFiles = new List<string>();

        public INIParser(Stream str) : base(str)
        {
            //Console.WriteLine(((BigStream)str).Name);
            long filesize = str.Length;
            while (str.Position < filesize)
            {
                ParseLine();
                string s = getString();
                string name;
                switch (s)
                {
                    case "#include":
                        string file = getString().Replace("\"", "");
                        string dir = ((BigStream)str).Path;
                        if (file.StartsWith(".."))
                        {
                            string path = dir.Substring(0, dir.LastIndexOf("\\")) + file.Replace("..", "");
                            if (!includedFiles.Contains(path))
                            {
                                try
                                {
                                    new INIParser(FileSystem.Open(path));
                                    includedFiles.Add(path);
                                }
                                catch
                                {
                                    Console.WriteLine(path);
                                }
                            }
                        }
                        else
                        {
                            string path = dir + "\\" + file;
                            if (!includedFiles.Contains(path))
                            {
                                try
                                {
                                    new INIParser(FileSystem.Open(path));
                                    includedFiles.Add(path);
                                }
                                catch
                                {
                                    Console.WriteLine(path);
                                }
                            }
                        }
                        break;
                    case "#define":
                        macros.Add(getString().ToUpper(), getStrings());
                        break;
                    case "LoadSubsystem":
                        LoadSubsystem ls = new LoadSubsystem();
                        name = getString();
                        ParseObject(ls);
                        ls.LoadFiles();
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

                if (s.Equals("InitFile") && (o.GetType() == typeof(LoadSubsystem)))
                    ((LoadSubsystem)o).AddInitFile(getString());
                else if (s.Equals("InitFileDebug") && (o.GetType() == typeof(LoadSubsystem)))
                    ((LoadSubsystem)o).AddInitFileDebug(getString());
                else if (s.Equals("InitPath") && (o.GetType() == typeof(LoadSubsystem)))
                    ((LoadSubsystem)o).AddInitPath(getString());
                else if (s.Equals("IncludePathCinematics") && (o.GetType() == typeof(LoadSubsystem)))
                    ((LoadSubsystem)o).AddIncludePathCinematics(getString());
                else if (s.Equals("ExcludePath") && (o.GetType() == typeof(LoadSubsystem)))
                    ((LoadSubsystem)o).AddExcludePath(getString());

                else if (s.Equals("Armor") && (o.GetType() == typeof(Armor)))
                    ((Armor)o).AddType(getString(), getFloat());
                else if (s.Equals("Modifier") && (o.GetType() == typeof(Armor)))
                    ((ModifierList)o).AddModifier(getString(), getFloat());

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


        public void ParseLine()
        {
            char[] separators = new char[] { ' ', '\t' };
            line = base.ReadLine().Trim();
            if (line.Contains(";"))
                line = line.Remove(line.IndexOf(";"));
            if (line.Contains("//"))
                line = line.Remove(line.IndexOf("//"));
            lineNumber++;
            index = 0;
            List<string> dataList = line.Replace("=", "").Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList<string>();

            //insert the values from the macros
            for (int i = 0; i < dataList.Count; i++)
            {
                if (macros.ContainsKey(dataList[i].ToUpper()))
                    dataList[i] = macros[dataList[i].ToUpper()];
            }
            for (int i = 0; i < dataList.Count; i++)
            {
                if (dataList[i].Equals("#MULTIPLY("))
                {
                    dataList[i] = (float.Parse(dataList[i + 1].Replace(".", "0.").Replace("%", "")) * float.Parse(dataList[i + 2].Replace(".", "0.").Replace("%", ""))).ToString("0.000");
                    dataList.RemoveAt(i + 1);
                    dataList.RemoveAt(i + 1);
                    dataList.RemoveAt(i + 1);
                }
                if (dataList[i].Equals("#ADD("))
                {
                    dataList[i] = (float.Parse(dataList[i + 1].Replace(".", "0.").Replace("%", "")) + float.Parse(dataList[i + 2].Replace(".", "0.").Replace("%", ""))).ToString("0.000");
                    dataList.RemoveAt(i + 1);
                    dataList.RemoveAt(i + 1);
                    dataList.RemoveAt(i + 1);
                }
            }
            if (dataList.Count != 0 && !dataList[0].StartsWith(";") && !dataList[0].StartsWith("//"))
                data = dataList.ToArray<string>();
            else
                ParseLine();
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
            s = s.Replace(",", ".");
            s = s.Replace("%", "");
            s = s.Replace("f", "");
            s = s.Replace("X:", "").Replace("Y:", "").Replace("Z:", "");
            s = s.Replace("R:", "").Replace("G:", "").Replace("B:", "");
            s = s.Replace("MP1:", "").Replace("MP2:", "").Replace("MP3:", "").Replace("MP4:", "").Replace("MP5:", "").Replace("MP6:", "").Replace("MP7:", "").Replace("MP8:", "");
            if (float.TryParse(s, out result))
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
