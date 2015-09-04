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
            Console.WriteLine(((BigStream)str).Name);
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
                                new INIParser(FileSystem.Open(path));
                                includedFiles.Add(path);
                            }
                        }
                        else
                        {
                            string path = dir + "\\" + file;
                            if (!includedFiles.Contains(path))
                            {
                                new INIParser(FileSystem.Open(path));
                                includedFiles.Add(path);
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
                    case "FXList":
                        FXList fl = new FXList();
                        name = getString();
                        ParseObject(fl);
                        INIManager.AddFXList(name, fl);
                        break;
                    case "GameData":
                        GameData data = new GameData();
                        ParseObject(data);
                        INIManager.SetGameData(data);
                        break;
                    case "MappedImage":
                        MappedImage mi = new MappedImage();
                        name = getString();
                        ParseObject(mi);
                        INIManager.AddMappedImage(name, mi);
                        break;
                    case "ModifierList":
                        ModifierList ml = new ModifierList();
                        name = getString();
                        ParseObject(ml);
                        INIManager.AddModifierList(name, ml);
                        break;
                    case "Object":
                        INI.Object o = new INI.Object();
                        name = getString();
                        //ParseObject(o);
                        INIManager.AddObject(name, o);
                        break;
                    case "Science":
                        Science sc = new Science();
                        name = getString();
                        ParseObject(sc);
                        INIManager.AddScience(name, sc);
                        break;
                    case "Upgrade":
                        Upgrade u = new Upgrade();
                        name = getString();
                        ParseObject(u);
                        INIManager.AddUpgrade(name, u);
                        break;
                    case "Weapon":
                        Weapon w = new Weapon();
                        name = getString();
                        //ParseObject(w);
                        INIManager.AddWeapon(name, w);
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

                if (o.GetType() == typeof(LoadSubsystem))
                {
                    LoadSubsystem ls = (LoadSubsystem)o;
                    if (s.Equals("InitFile"))
                        ls.AddInitFile(getString());
                    else if (s.Equals("InitFileDebug"))
                        ls.AddInitFileDebug(getString());
                    else if (s.Equals("InitPath"))
                        ls.AddInitPath(getString());
                    else if (s.Equals("IncludePathCinematics"))
                        ls.AddIncludePathCinematics(getString());
                    else if (s.Equals("ExcludePath") )
                        ls.AddExcludePath(getString());
                }
                else if (o.GetType() == typeof(GameData))
                {
                    GameData gd = (GameData)o;
                    if (s.Equals("WeaponBonus"))
                        gd.AddWeaponBonus(getString(), getString(), getInt());
                    else if (s.Equals("StandardPublicBone"))
                        gd.AddStandardPublicBone(getString());
                }

                //rewrite all down there

                else if (s.Equals("Armor") && (o.GetType() == typeof(Armor)))
                    ((Armor)o).AddType(getString(), getFloat());
                else if (s.Equals("Modifier") && (o.GetType() == typeof(ModifierList)))
                    ((ModifierList)o).AddModifier(getString(), getStrings());

                else if (s.Equals("DamageFieldNugget") && (o.GetType() == typeof(Weapon)))
                {
                    if (o.GetType() == typeof(Weapon))
                    { }
                    DamageFieldNugget dfn = new DamageFieldNugget();
                    ParseObject(dfn);
                    ((Weapon)o).damageFieldNugget = dfn;
                }
                else if (s.Equals("DamageNugget") && (o.GetType() == typeof(Weapon)))
                {
                    DamageNugget dn = new DamageNugget();
                    ParseObject(dn);
                    ((Weapon)o).damageNugget = dn;
                }
                else if (s.Equals("MetaImpactNugget") && (o.GetType() == typeof(Weapon)))
                {
                    MetaImpactNugget min = new MetaImpactNugget();
                    ParseObject(min);
                    ((Weapon)o).metaImpactNugget = min;
                }
                else if (s.Equals("ProjectileNugget") && (o.GetType() == typeof(Weapon)))
                {
                    ProjectileNugget pn = new ProjectileNugget();
                    ParseObject(pn);
                    ((Weapon)o).AddProjectileNugget(pn);
                }
                else if (s.Equals("WeaponOCLNugget") && (o.GetType() == typeof(Weapon)))
                {
                    WeaponOCLNugget won = new WeaponOCLNugget();
                    ParseObject(won);
                    ((Weapon)o).weaponOCLNugget = won;
                }
                else if (s.Equals("DOTNugget") && (o.GetType() == typeof(Weapon)))
                {
                    DOTNugget dn = new DOTNugget();
                    ParseObject(dn);
                    ((Weapon)o).dotNugget = dn;
                }
                else if (s.Equals("ParalyzeNugget") && (o.GetType() == typeof(Weapon)))
                {
                    ParalyzeNugget pn = new ParalyzeNugget();
                    ParseObject(pn);
                    ((Weapon)o).paralyzeNugget = pn;
                }
                else if (s.Equals("FireLogicNugget") && (o.GetType() == typeof(Weapon)))
                {
                    FireLogicNugget fln = new FireLogicNugget();
                    ParseObject(fln);
                    ((Weapon)o).fireLogicNugget = fln;
                }
                else if (s.Equals("HordeAttackNugget") && (o.GetType() == typeof(Weapon)))
                {
                    HordeAttackNugget han = new HordeAttackNugget();
                    ParseObject(han);
                    ((Weapon)o).hordeAttackNugget = han;
                }
                else if (s.Equals("DamageContainedNugget") && (o.GetType() == typeof(Weapon)))
                {
                    DamageContainedNugget dcn = new DamageContainedNugget();
                    ParseObject(dcn);
                    ((Weapon)o).damageContainedNugget = dcn;
                }
                else if (s.Equals("OpenGateNugget") && (o.GetType() == typeof(Weapon)))
                {
                    OpenGateNugget ogn = new OpenGateNugget();
                    ParseObject(ogn);
                    ((Weapon)o).openGateNugget = ogn;
                }
                else if (s.Equals("LuaEventNugget") && (o.GetType() == typeof(Weapon)))
                {
                    LuaEventNugget len = new LuaEventNugget();
                    ParseObject(len);
                    ((Weapon)o).luaEventNugget = len;
                }
                else if (s.Equals("SlaveAttackNugget") && (o.GetType() == typeof(Weapon)))
                {
                    SlaveAttackNugget san = new SlaveAttackNugget();
                    ParseObject(san);
                    ((Weapon)o).slaveAttackNugget = san;
                }
                else if (s.Equals("AttributeModifierNugget") && (o.GetType() == typeof(Weapon)))
                {
                    AttributeModifierNugget amn = new AttributeModifierNugget();
                    ParseObject(amn);
                    ((Weapon)o).attributeModifierNugget = amn;
                }
                else if (s.Equals("StealMoneyNugget") && (o.GetType() == typeof(Weapon)))
                {
                    StealMoneyNugget smn = new StealMoneyNugget();
                    ParseObject(smn);
                    ((Weapon)o).stealMoneyNugget = smn;
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


        public void ParseLine()
        {
            char[] separators = new char[] { ' ', '\t' };
            line = base.ReadLine().Trim();
            if (line.Contains(";"))
                line = line.Remove(line.IndexOf(";"));
            if (line.Contains("//"))
                line = line.Remove(line.IndexOf("//"));
            line = line.Replace("%", "");
            line = line.Replace("Left:", "");
            line = line.Replace("Top:", "");
            line = line.Replace("Right:", "");
            line = line.Replace("Bottom:", "");
            line = line.Replace("Min:", "");
            line = line.Replace("Max:", "");
            line = line.Replace(",", ".");
            line = line.Replace("X:", "").Replace("Y:", "").Replace("Z:", "");
            line = line.Replace("R:", "").Replace("G:", "").Replace("B:", "");
            line = line.Replace("MP1:", "").Replace("MP2:", "").Replace("MP3:", "").Replace("MP4:", "").Replace("MP5:", "").Replace("MP6:", "").Replace("MP7:", "").Replace("MP8:", "");
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
                if (dataList[i].ToLower().Contains("#multiply"))
                {
                    dataList[i] = (float.Parse(dataList[i + 1].Replace(".", "0.").Replace("%", "")) * float.Parse(dataList[i + 2].Replace(".", "0.").Replace("%", ""))).ToString("0.000");
                    dataList.RemoveAt(i + 1);
                    dataList.RemoveAt(i + 1);
                    dataList.RemoveAt(i + 1);
                }
                else if (dataList[i].ToLower().Contains("#add"))
                {
                    dataList[i] = (float.Parse(dataList[i + 1].Replace(".", "0.").Replace("%", "")) + float.Parse(dataList[i + 2].Replace(".", "0.").Replace("%", ""))).ToString("0.000");
                    dataList.RemoveAt(i + 1);
                    dataList.RemoveAt(i + 1);
                    dataList.RemoveAt(i + 1);
                }
                else if (dataList[i].ToLower().Contains("#subtract"))
                {
                    dataList[i] = (float.Parse(dataList[i + 1].Replace(".", "0.").Replace("%", "")) - float.Parse(dataList[i + 2].Replace(".", "0.").Replace("%", ""))).ToString("0.000");
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
            if (s.StartsWith("."))
                s = s.Replace(".", "0.");
            s = s.Replace("f", "");
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
            s = s.Replace("YES", "True");
            s = s.Replace("No", "False");
            s = s.Replace("NO", "False");
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
