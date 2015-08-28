using SageCS.Core;
using SageCS.Core.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.INI
{
    class INIManager
    {
        private static GameData gameData;

        private static Dictionary<string, Object> objects = new Dictionary<string, Object>();
        private static Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();
        private static Dictionary<string, Upgrade> upgrades = new Dictionary<string, Upgrade>();
        private static Dictionary<string, Armor> armors = new Dictionary<string, Armor>();
        private static Dictionary<string, MappedImage> mappedImages = new Dictionary<string, MappedImage>();
        private static Dictionary<string, AmbientStream> ambientStreams = new Dictionary<string, AmbientStream>();
        private static Dictionary<string, CommandButton> commandButtons = new Dictionary<string, CommandButton>();
        private static Dictionary<string, ModifierList> modifierLists = new Dictionary<string, ModifierList>();

        public static void ParseINIs()
        {
            //map.ini files are loaded when the corresponding map is selected for the game

            new INIParser(FileSystem.Open("data\\ini\\gamedata.ini"));
            new INIParser(FileSystem.Open("data\\ini\\createaherogamedata.inc"));

            new INIParser(FileSystem.Open("data\\ini\\armor.ini"));
            new INIParser(FileSystem.Open("data\\ini\\ambientstream.ini"));
            //new INIParser(FileSystem.Open("data\\ini\\attributemodifier.ini")); //still some error

            new INIParser(FileSystem.Open("data\\ini\\commandbutton.ini"));


            /*
            List<Stream> streams = FileSystem.OpenAll(".ini");
            foreach (Stream s in streams)
            {
                try
                {
                    new INIParser(s);
                }
                catch
                {
                    try
                    {
                        Console.WriteLine("## ERROR: unable to parse ini file: " + ((BigStream)s).Name);
                    }
                    catch
                    {
                        Console.WriteLine("#######");
                    }
                }
            }
            Console.WriteLine("# finished parsing " + streams.Count + " ini files");  
            */
        }

        public static void SetGameData(GameData data)
        {
            gameData = data;
        }

        public static void AddObject(string name, INI.Object obj)
        {
            if (!objects.ContainsKey(name))
                objects.Add(name, obj);
            else
                //overwrite old object
                objects[name] = obj;
        }

        public static Object getObject(string name)
        {
            return objects[name];
        }

        public static bool TryGetObject(string name, out Object obj)
        {
            if (objects.ContainsKey(name))
            {
                obj = getObject(name);
                return true;
            }
            obj = null;
            return false;
        }

        public static void AddWeapon(string name, Weapon wep)
        {
            if (!weapons.ContainsKey(name))
                weapons.Add(name, wep);
            else
                //overwrite old object
                weapons[name] = wep;
        }

        public static Weapon GetWeapon(string name)
        {
            return weapons[name];
        }

        public static bool TryGetWeapon(string name, out Weapon weapon)
        {
            if (weapons.ContainsKey(name))
            {
                weapon = GetWeapon(name);
                return true;
            }
            weapon = null;
            return false;
        }

        public static void AddUpgrade(string name, Upgrade up)
        {
            if (!upgrades.ContainsKey(name))
                upgrades.Add(name, up);
            else
                //overwrite old object
                upgrades[name] = up;
        }

        public static Upgrade GetUpgrade(string name)
        {
            return upgrades[name];
        }

        public static bool TryGetUpgrade(string name, out Upgrade upgrade)
        {
            if (upgrades.ContainsKey(name))
            {
                upgrade = GetUpgrade(name);
                return true;
            }
            upgrade = null;
            return false;
        }

        public static void AddArmor(string name, Armor ar)
        {
            if (!armors.ContainsKey(name))
                armors.Add(name, ar);
            else
                //overwrite old object
                armors[name] = ar;
        }

        public static Armor GetArmor(string name)
        {
            return armors[name];
        }

        public static bool TryGetArmor(string name, out Armor armor)
        {
            if (armors.ContainsKey(name))
            {
                armor = GetArmor(name);
                return true;
            }
            armor = null;
            return false;
        }

        public static void AddMappedImage(string name, MappedImage mi)
        {
            if (!mappedImages.ContainsKey(name))
                mappedImages.Add(name, mi);
            else
                //overwrite old object
                mappedImages[name] = mi;
        }

        public static MappedImage GetMappedImage(string name)
        {
            return mappedImages[name];
        }

        public static bool TryGetMappedImage(string name, out MappedImage mi)
        {
            if (mappedImages.ContainsKey(name))
            {
                mi = GetMappedImage(name);
                return true;
            }
            mi = null;
            return false;
        }

        public static void AddAmbientStream(string name, AmbientStream ast)
        {
            if (!ambientStreams.ContainsKey(name))
                ambientStreams.Add(name, ast);
            else
                ambientStreams[name] = ast;
        }

        public static AmbientStream GetAmbientStream(string name)
        {
            return ambientStreams[name];
        }

        public static bool TryGetAmbientStream(string name, out AmbientStream ast)
        {
            if (ambientStreams.ContainsKey(name))
            {
                ast = GetAmbientStream(name);
                return true;
            }
            ast = null;
            return false;
        }

        public static void AddCommandButton(string name, CommandButton cb)
        {
            if (!commandButtons.ContainsKey(name))
                commandButtons.Add(name, cb);
            else
                commandButtons[name] = cb;
        }

        public static CommandButton GetCommandButton(string name)
        {
            return commandButtons[name];
        }

        public static bool TryGetCommandButton(string name, out CommandButton cb)
        {
            if (commandButtons.ContainsKey(name))
            {
                cb = GetCommandButton(name);
                return true;
            }
            cb = null;
            return false;
        }

        public static void AddModifierList(string name, ModifierList ml)
        {
            if (!modifierLists.ContainsKey(name))
                modifierLists.Add(name, ml);
            else
                modifierLists[name] = ml;
        }

        public static ModifierList GetModifierList(string name)
        {
            return modifierLists[name];
        }

        public static bool TryGetModifierList(string name, out ModifierList ml)
        {
            if(modifierLists.ContainsKey(name))
            {
                ml = GetModifierList(name);
                return true;
            }
            ml = null;
            return false;
        }

        //called after each match?
        public static void ClearAll()
        {
            objects.Clear();
            weapons.Clear();
        }
    }
}
