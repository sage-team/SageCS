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
            //hardcoded: (on start)
            new INIParser(FileSystem.Open("data\\ini\\default\\subsystemlegend.ini"));
            /*
            data\ini\default\SubsystemLegend.ini
            ->most of this stuff is defined here, but why not all files??

            data\ini\default\gamedata.ini 
            data\ini\gamedata.ini
            data\ini\GameLOD.ini
            data\ini\GameLODPresets.ini
            data\ini\default\water.ini
            data\ini\water.ini
            data\ini\default\fire.ini
            data\ini\fire.ini
            data\ini\default\Environment.ini
            data\ini\environment.ini
            data\ini\AudioSettings.ini
            data\ini\default\music.ini
            data\ini\default\speech.ini
            data\ini\default\soundeffects.ini
            data\ini\default\ambientstream.ini
            data\ini\music.ini
            data\ini\soundeffects.ini
            data\ini\speech.ini
            data\ini\voice.ini
            data\ini\ambientstream.ini
            data\ini\miscAudio.ini
            data\ini\default\eva.ini
            data\ini\eva.ini
            data\ini\default\science.ini
            data\ini\science.ini
            data\ini\default\upgrade.ini
            data\ini\upgrade.ini
            data\ini\default\multiplayer.ini
            data\ini\multiplayer.ini
            data\ini\default\Terrain.ini
            data\ini\Terrain.ini
            data\ini\default\roads.ini
            data\ini\roads.ini
            data\ini\default\weather.ini
            data\ini\weather.ini
            data\ini\rank.ini
            data\ini\PlayerAITypes.ini
            data\ini\default\PlayerTemplate.ini
            data\ini\PlayerTemplate.ini
            data\ini\FXParticleSystem.ini
            data\ini\default\fxlist.ini
            data\ini\fxList.ini
            data\ini\weapon.ini
            data\ini\default\ObjectCreationList.ini
            data\ini\ObjectCreationList.ini
            data\ini\locomotor.ini
            data\ini\default\specialpower.ini
            data\ini\specialpower.ini
            data\ini\damagefx.ini
            data\ini\armor.ini
            data\ini\CrowdResponse.ini
            data\ini\LivingWorldAutoResolveArmor.ini
            data\ini\LivingWorldAutoResolveWeapon.ini
            data\ini\LivingWorldAutoResolveBody.ini
            data\ini\LivingWorldAutoResolveLeadership.ini
            data\ini\LivingWorldAutoResolveCombatChain.ini
            data\ini\LivingWorldAutoResolveHandicaps.ini
            data\ini\MissionObjectives.ini
            data\ini\Emotions.ini
            data\ini\default\object.ini
            data\ini\stances.ini
            data\ini\formationassistant.ini
            data\ini\Lightpoints.ini
            data\ini\experienceLevels.ini
            data\ini\aptButtonTooltipMap.ini
            data\ini\livingWorldPlayers.ini
            data\ini\livingworldAITemplate.ini
            data\ini\livingworldRegionEffects.ini
            data\ini\livingworld.ini
            data\ini\livingWorldBuildingIcons.ini
            data\ini\livingworldbuildploticons.ini
            data\ini\DrawGroupInfo.ini
            data\ini\fontsubstitution.ini
            data\ini\fontsettings.ini
            data\ini\mouse.ini

            //game starts, but shows nothing

            
            #####
            with empty subsystemlegend ini

            data\ini\default\SubsystemLegend.ini
            data\ini\GameLOD.ini
            data\ini\GameLODPresets.ini
            data\ini\default\water.ini
            data\ini\water.ini
            data\ini\default\fire.ini
            data\ini\fire.ini
            data\ini\default\Environment.ini
            data\ini\environment.ini
            data\ini\AudioSettings.ini
            data\ini\default\music.ini
            data\ini\default\speech.ini
            data\ini\default\soundeffects.ini
            data\ini\default\ambientstream.ini
            data\ini\music.ini
            data\ini\soundeffects.ini
            data\ini\speech.ini
            data\ini\voice.ini
            data\ini\ambientstream.ini
            data\ini\miscAudio.ini
            data\ini\default\eva.ini
            data\ini\eva.ini
            data\ini\stances.ini
            data\ini\formationassistant.ini
            data\ini\DrawGroupInfo.ini
            data\ini\fontsubstitution.ini
            data\ini\mouse.ini
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

        public static bool TryGetObject(string name, out Object obj)
        {
            if (objects.ContainsKey(name))
            {
                obj = objects[name];
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

        public static bool TryGetWeapon(string name, out Weapon weapon)
        {
            if (weapons.ContainsKey(name))
            {
                weapon = weapons[name];
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

        public static bool TryGetUpgrade(string name, out Upgrade upgrade)
        {
            if (upgrades.ContainsKey(name))
            {
                upgrade = upgrades[name];
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

        public static bool TryGetArmor(string name, out Armor armor)
        {
            if (armors.ContainsKey(name))
            {
                armor = armors[name];
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

        public static bool TryGetMappedImage(string name, out MappedImage mi)
        {
            if (mappedImages.ContainsKey(name))
            {
                mi = mappedImages[name];
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

        public static bool TryGetAmbientStream(string name, out AmbientStream ast)
        {
            if (ambientStreams.ContainsKey(name))
            {
                ast = ambientStreams[name];
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

        public static bool TryGetCommandButton(string name, out CommandButton cb)
        {
            if (commandButtons.ContainsKey(name))
            {
                cb = commandButtons[name];
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

        public static bool TryGetModifierList(string name, out ModifierList ml)
        {
            if(modifierLists.ContainsKey(name))
            {
                ml = modifierLists[name];
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
