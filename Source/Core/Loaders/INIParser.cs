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
                    case "AudioEvent":
                        AudioEvent e = new AudioEvent();
                        name = getString();
                        ParseObject(e);
                        INIManager.AddAudioEvent(name, e);
                        break;
                    case "CommandButton":
                        CommandButton cb = new CommandButton();
                        name = getString();
                        ParseObject(cb);
                        INIManager.AddCommandButton(name, cb);
                        break;
                    case "DialogEvent":
                        DialogEvent de = new DialogEvent();
                        name = getString();
                        ParseObject(de);
                        INIManager.AddDialogEvent(name, de);
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
                    case "LoadSubsystem":
                        LoadSubsystem ls = new LoadSubsystem();
                        name = getString();
                        ParseObject(ls);
                        ls.LoadFiles();
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
                    case "Multisound":
                        Multisound ms = new Multisound();
                        name = getString();
                        ParseObject(ms);
                        INIManager.AddMultisound(name, ms);
                        break;
                    case "MusicTrack":
                        MusicTrack mt = new MusicTrack();
                        name = getString();
                        ParseObject(mt);
                        INIManager.AddMusicTrack(name, mt);
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
                        ParseObject(w);
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
                    switch(s)
                    {
                        case "InitFile":
                            ls.AddInitFile(getString());
                            break;
                        case "InitFileDebug":
                            ls.AddInitFileDebug(getString());
                            break;
                        case "InitPath":
                            ls.AddInitPath(getString());
                            break;
                        case "IncludePathCinematics":
                            ls.AddIncludePathCinematics(getString());
                            break;
                        case "ExcludePath":
                            ls.AddExcludePath(getString());
                            break;
                    }
                }
                else if (o.GetType() == typeof(Armor))
                {
                    Armor ar = (Armor)o;
                    switch(s)
                    {
                        case "Armor":
                            ar.AddType(getString(), getFloat());
                            break;
                    }
                }
                else if (o.GetType() == typeof(AudioEvent))
                {
                    AudioEvent ae = (AudioEvent)o;
                    switch(s)
                    {
                        case "Sounds":
                            string val = getStrings();
                            string[] sounds = val.Split(' ');
                            foreach (string sound in sounds)
                            {
                                MusicTrack mt;
                                if (INIManager.TryGetMusicTrack(sound, out mt))
                                    ae.AddMusicTrack(mt);
                                //else
                                //    PrintError(" no such MusicTrack found: " + sound);
                            }
                            break;
                    }
                }
                else if (o.GetType() == typeof(FXList))
                {
                    FXList fx = (FXList)o;
                    switch(s)
                    {
                        case "ParticleSystem":
                            ParticleSystem ps = new ParticleSystem();
                            ParseObject(ps);
                            fx.AddParticleSystem(ps);
                            break;
                        case "Sound":
                            Sound so = new Sound();
                            ParseObject(so);
                            fx.AddSound(so);
                            break;
                        case "DynamicDecal":
                            DynamicDecal dc = new DynamicDecal();
                            ParseObject(dc);
                            fx.AddDynamicDecal(dc);
                            break;
                        case "TerrainScorch":
                            TerrainScorch ts = new TerrainScorch();
                            ParseObject(ts);
                            fx.AddTerrainScorch(ts);
                            break;
                        case "BuffNugget":
                            BuffNugget bn = new BuffNugget();
                            ParseObject(bn);
                            fx.buffNugget = bn;
                            break;
                        case "ViewShake":
                            ViewShake vs = new ViewShake();
                            ParseObject(vs);
                            fx.viewShake = vs;
                            break;
                        case "CameraShakerVolume":
                            CameraShakerVolume cs = new CameraShakerVolume();
                            ParseObject(cs);
                            fx.cameraShakerVolume = cs;
                            break;
                        case "TintDrawable":
                            TintDrawable td = new TintDrawable();
                            ParseObject(td);
                            fx.tintDrawable = td;
                            break;
                    }
                }
                else if (o.GetType() == typeof(GameData))
                {
                    GameData gd = (GameData)o;
                    switch (s)
                    {
                        case "WeaponBonus":
                            gd.AddWeaponBonus(getString(), getString(), getInt());
                            break;
                        case "StandardPublicBone":
                            gd.AddStandardPublicBone(getString());
                            break;
                    }
                }
                else if (o.GetType() == typeof(ModifierList))
                {
                    ModifierList ml = (ModifierList)o;
                    switch(s)
                    {
                        case "Modifier":
                            ml.AddModifier(getString(), getStrings());
                            break;
                    }
                }
                else if (o.GetType() == typeof(Multisound))
                {
                    Multisound ms = (Multisound)o;
                    switch (s)
                    {
                        case "Subsounds":
                            string val = getStrings();
                            string[] sounds = val.Split(' ');
                            foreach (string sound in sounds)
                            {
                                MusicTrack mt;
                                if (INIManager.TryGetMusicTrack(sound, out mt))
                                    ms.AddMusicTrack(mt);
                                //else
                                 //   PrintError(" no such MusicTrack found: " + sound);
                            }
                            break;
                    }
                }
                else if (o.GetType() == typeof(INI.Object))
                {
                    INI.Object obj = (INI.Object)o;
                    switch (s)
                    {
                        case "Draw":
                            Draw d = new Draw();
                            string name = getString();
                            ParseObject(d);
                            obj.AddDraw(name, d);
                            break;
                    }
                }
                else if (o.GetType() == typeof(Weapon))
                {
                    Weapon w = (Weapon)o;
                    switch(s)
                    {
                        case "DamageFieldNugget":
                            DamageFieldNugget dfn = new DamageFieldNugget();
                            ParseObject(dfn);
                            w.damageFieldNugget = dfn;
                            break;
                        case "DamageNugget":
                            DamageNugget dn = new DamageNugget();
                            ParseObject(dn);
                            w.damageNugget = dn;
                            break;
                        case "MetaImpactNugget":
                            MetaImpactNugget min = new MetaImpactNugget();
                            ParseObject(min);
                            w.metaImpactNugget = min;
                            break;
                        case "ProjectileNugget":
                            ProjectileNugget pn = new ProjectileNugget();
                            ParseObject(pn);
                            w.AddProjectileNugget(pn);
                            break;
                        case "WeaponOCLNugget":
                            WeaponOCLNugget won = new WeaponOCLNugget();
                            ParseObject(won);
                            w.weaponOCLNugget = won;
                            break;
                        case "DOTNugget":
                            DOTNugget don = new DOTNugget();
                            ParseObject(don);
                            w.dotNugget = don;
                            break;
                        case "ParalyzeNugget":
                            ParalyzeNugget pan = new ParalyzeNugget();
                            ParseObject(pan);
                            w.paralyzeNugget = pan;
                            break;
                        case "FireLogicNugget":
                            FireLogicNugget fln = new FireLogicNugget();
                            ParseObject(fln);
                            w.fireLogicNugget = fln;
                            break;
                        case "HordeAttackNugget":
                            HordeAttackNugget han = new HordeAttackNugget();
                            ParseObject(han);
                            w.hordeAttackNugget = han;
                            break;
                        case "DamageContainedNugget":
                            DamageContainedNugget dcn = new DamageContainedNugget();
                            ParseObject(dcn);
                            w.damageContainedNugget = dcn;
                            break;
                        case "OpenGateNugget":
                            OpenGateNugget ogn = new OpenGateNugget();
                            ParseObject(ogn);
                            w.openGateNugget = ogn;
                            break;
                        case "LuaEventNugget":
                            LuaEventNugget len = new LuaEventNugget();
                            ParseObject(len);
                            w.luaEventNugget = len;
                            break;
                        case "SlaveAttackNugget":
                            SlaveAttackNugget san = new SlaveAttackNugget();
                            ParseObject(san);
                            w.slaveAttackNugget = san;
                            break;
                        case "AttributeModifierNugget":
                            AttributeModifierNugget amn = new AttributeModifierNugget();
                            ParseObject(amn);
                            w.attributeModifierNugget = amn;
                            break;
                        case "StealMoneyNugget":
                            StealMoneyNugget smn = new StealMoneyNugget();
                            ParseObject(smn);
                            w.stealMoneyNugget = smn;
                            break;
                    }
                }

                else if (fields.ContainsKey(s))
                {
                    Type type = fields[s].FieldType;
                    if (type == typeof(string))
                        fields[s].SetValue(o, getString());
                    else if (type == typeof(int))
                        fields[s].SetValue(o, getInt());
                    else if (type == typeof(int[]))
                        fields[s].SetValue(o, getInts());
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
                        PrintError("invalid type: " + type);
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
                //throw new IndexOutOfRangeException(); //sometimes values are outcommented
                return "";
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

        public int[] getInts()
        {
            List<int> i = new List<int>();
            while (HasNext())
            {
                i.Add(getInt());
            }
            return i.ToArray<int>();
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
            s = s.Replace("yes", "True");
            s = s.Replace("YES", "True");
            s = s.Replace("No", "False");
            s = s.Replace("no", "False");
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
