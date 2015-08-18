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

        private static Dictionary<string, string> macros = new Dictionary<string, string>();

        private static Dictionary<string, INI.Object> objects = new Dictionary<string, INI.Object>();
        private static Dictionary<string, INI.Weapon> weapons = new Dictionary<string, INI.Weapon>();

        public static void Parse(Stream s)
        {
            long filesize = s.Length;
            sr = new StreamReader(s);

            while (s.Position < filesize)
            {
                string[] data = ReadLine(sr);
                switch (data[0])
                {
                    case "#include":
                        break;
                    case "#define":
                        string value = "";
                        for (int i = 2; i < data.Length; i++)
                        {
                            value += data[i];
                        }
                        macros.Add(data[1], value);
                        break;

                    case "Object":
                        ParseObject(data[1]);
                        break;
                    default:
                        //Console.WriteLine("##INI: unhandled entry in file " + data[0]);
                        break;
                }
            }
        }

        private static string[] ReadLine(StreamReader sr)
        {
            char[] separators = new char[] { ' ', '\t' };
            string line = sr.ReadLine().Trim();
            if (line.Contains(";"))
                line = line.Remove(line.IndexOf(";"));
            if (line.Contains("//"))
                line = line.Remove(line.IndexOf("//"));
            string[] data = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length != 0 && !data[0].StartsWith(";") && !data[0].StartsWith("//"))
                return data;
            return ReadLine(sr);
        }

        //##############################################################################################
        //# weapon struct
        //##############################################################################################

        private static void ParseWeapon(string name)
        {
            Weapon wep = new Weapon();
            string[] data;
            do
            {
                data = ReadLine(sr);
                switch (data[0])
                {
                    case "PrimaryDamage":
                        wep.PrimaryDamage = float.Parse(data[2]);
                        break;
                    case "PrimaryDamageRadius":
                        wep.PrimaryDamageRadius = float.Parse(data[2]);
                        break;
                }
            }
            while (!data[0].Equals("End")); //also test END ?

            if (!weapons.ContainsKey(name))
                weapons.Add(name, wep);
            else
                //overwrite old object
                weapons[name] = wep;
        }

        //##############################################################################################
        //# object struct
        //##############################################################################################

        private static WeaponSet ParseWeaponSet()
        {
            WeaponSet set = new WeaponSet();
            string[] data;
            do
            {
                data = ReadLine(sr);
                switch (data[0])
                {
                    case "Conditions":
                        set.Conditions = data[2];
                        break;
                    case "Weapon":
                        switch (data[2])
                        {
                            case "PRIMARY":
                                set.PRIMARY = data[3];
                                break;
                            case "SECONDARY":
                                set.SECONDARY = data[3];
                                break;
                            case "TERTIARY":
                                set.TERTIARY = data[3];
                                break;
                            default:
                                Console.WriteLine("##INI: new param " + data[2] + " in WeaponSet");
                                break;
                        }
                        break;
                    case "AutoChooseSources":
                        set.AutoChooseSources = data[2];
                        break;
                    default:
                        Console.WriteLine("##INI: new param " + data[0] + " in WeaponSet");
                        break;
                }
            }
            while (!data[0].Equals("End")); //also test END ?

            return set;
        }

        private static ArmorSet ParseArmorSet()
        {
            ArmorSet set = new ArmorSet();
            string[] data;
            do
            {
                data = ReadLine(sr);
                switch (data[0])
                {
                    case "Conditions":
                        set.Conditions = data[2];
                        break;
                    case "Armor":
                        set.Armor = data[2];
                        break;
                    case "DamageFX":
                        set.DamageFX = data[2];
                        break;
                    default:
                        Console.WriteLine("##INI: new param " + data[0] + " in ArmorSet");
                        break;
                }
            }
            while (!data[0].Equals("End")); //also test END ?

            return set;
        }

        private static void ParseObject(string name)
        {
            INI.Object obj = new INI.Object();
            string[] data;
            do
            {
                data = ReadLine(sr);
                switch (data[0])
                {
                    case "SelectPortrait":
                        obj.SelectPortrait = data[2];
                        break;
                    case "PlacementViewAngle":
                        obj.PlacementViewAngle = float.Parse(data[2]);
                        break;
                    case "UpgradeCameo1":
                        obj.UpgradeCameo1 = data[2];
                        break;
                    case "UpgradeCameo2":
                        obj.UpgradeCameo2 = data[2];
                        break;
                    case "UpgradeCameo3":
                        obj.UpgradeCameo3 = data[2];
                        break;
                    case "UpgradeCameo4":
                        obj.UpgradeCameo4 = data[2];
                        break;
                    case "UpgradeCameo5":
                        obj.UpgradeCameo5 = data[2];
                        break;
                    case "Buildable":
                        obj.Buildable = bool.Parse(data[2]);
                        break;
                    case "TransportSlotCount":
                        obj.TransportSlotCount = int.Parse(data[2]);
                        break;
                    case "WeaponSet":
                        obj.WeaponSets.Add(ParseWeaponSet());
                        break;
                    case "ArmorSet":
                        obj.ArmorSets.Add(ParseArmorSet());
                        break;
                    case "VisionRange":
                        obj.VisionRange = float.Parse(data[2]);
                        break;
                    case "BuildCost":
                        obj.BuildCost = int.Parse(data[2]);
                        break;
                    case "BuildTime":
                        obj.BuildTime = int.Parse(data[2]);
                        break;
                    case "RefundValue":
                        obj.RefundValue = int.Parse(data[2]);
                        break;
                    case "EnergyProduction":
                        obj.EnergyProduction = int.Parse(data[2]);
                        break;
                    case "IsForbidden":
                        obj.IsForbidden = bool.Parse(data[2]);
                        break;
                    case "isBridge":
                        obj.IsBridge = bool.Parse(data[2]);
                        break;
                    case "IsPrerequisite":
                        obj.IsPrerequisite = bool.Parse(data[2]);
                        break;
                    case "VoiceAlert":
                        obj.VoiceAlert = data[2];
                        break;
                    case "VoiceAttack":
                        obj.VoiceAttack = data[2];
                        break;
                    case "VoiceAttackAir":
                        obj.VoiceAttackAir = data[2];
                        break;
                    case "VoiceAttackCharge":
                        obj.VoiceAttackCharge = data[2];
                        break;
                    case "VoiceAttackMachine":
                        obj.VoiceAttackMachine = data[2];
                        break;
                    case "VoiceAttackStructure":
                        obj.VoiceAttackStructure = data[2];
                        break;
                    case "VoiceCombineWithHorde":
                        obj.VoiceCombineWithHorde = data[2];
                        break;
                    case "VoiceCreated":
                        obj.VoiceCreated = data[2];
                        break;
                    case "VoiceDefect":
                        obj.VoiceDefect = data[2];
                        break;
                    case "VoiceFullyCreated":
                        obj.VoiceFullyCreated = data[2];
                        break;
                    case "VoiceFear":
                        obj.VoiceFear = data[2];
                        break;
                    case "VoiceGuard":
                        obj.VoiceGuard = data[2];
                        break;
                    case "VoiceMove": 
                        obj.VoiceMove = data[2];
                        break;
                    case "VoiceMoveOverWalls": 
                        obj.VoiceMoveOverWalls = data[2];
                        break;
                    case "VoiceMoveToCamp":
                        obj.VoiceMoveToCamp = data[2];
                        break;
                    case "VoiceMoveToHigherGround":
                        obj.VoiceMoveToHigherGround = data[2];
                        break;
                    case "VoiceMoveWhileAttacking":
                        obj.VoiceMoveWhileAttacking = data[2];
                        break;
                    case "VoiceRetreatToCastle":
                        obj.VoiceRetreatToCastle = data[2];
                        break;
                    case "VoiceSelect":
                        obj.VoiceSelect = data[2];
                        break;
                    case "VoiceSelectBattle":
                        obj.VoiceSelectBattle = data[2];
                        break;
                    case "VoiceTaskComplete":
                        obj.VoiceTaskComplete = data[2];
                        break;
                    case "VoiceEnterStateAttack":
                        obj.VoiceEnterStateAttack = data[2];
                        break;
                    case "VoiceEnterStateAttackCharge":
                        obj.VoiceEnterStateAttackCharge = data[2];
                        break;
                    case "VoiceEnterStateAttackAir":
                        obj.VoiceEnterStateAttackAir = data[2];
                        break;
                    case "VoiceEnterStateAttackStructure":
                        obj.VoiceEnterStateAttackStructure = data[2];
                        break;
                    case "VoiceEnterStateAttackMachine":
                        obj.VoiceEnterStateAttackMachine = data[2];
                        break;
                    case "VoiceEnterStateMove":
                        obj.VoiceEnterStateMove = data[2];
                        break;
                    case "VoiceEnterStateMoveToHigherGround":
                        obj.VoiceEnterStateMoveToHigherGround = data[2];
                        break;
                    case "VoiceEnterStateRetreatToCastle":
                        obj.VoiceEnterStateRetreatToCastle = data[2];
                        break;
                    case "VoiceEnterStateMoveToCamp":
                        obj.VoiceEnterStateMoveToCamp = data[2];
                        break;
                    case "VoiceEnterStateMoveWhileAttacking":
                        obj.VoiceEnterStateMoveWhileAttacking = data[2];
                        break;
                    case "SoundAmbient":
                        obj.SoundAmbient = data[2];
                        break;
                    case "SoundAmbientBattle":
                        obj.SoundAmbientBattle = data[2];
                        break;
                    case "SoundAmbientDamaged":
                        obj.SoundAmbientDamaged = data[2];
                        break;
                    case "SoundAmbientReallyDamaged":
                        obj.SoundAmbientReallyDamaged = data[2];
                        break;
                    case "SoundAmbientRubble":
                        obj.SoundAmbientRubble = data[2];
                        break;
                    case "SoundEnter":
                        obj.SoundEnter = data[2];
                        break;
                    case "SoundExit":
                        obj.SoundExit = data[2];
                        break;
                    case "SoundCreated":
                        obj.SoundCreated = data[2];
                        break;
                    case "SoundCrushing":
                        obj.SoundCrushing = data[2];
                        break;
                    case "SoundFallingFromPlane":
                        obj.SoundFallingFromPlane = data[2];
                        break;
                    case "SoundImpact":
                        obj.SoundImpact = data[2];
                        break;
                    case "SoundImpactCyclonic":
                        obj.SoundImpactCyclonic = data[2];
                        break;
                    case "SoundMoveLoop":
                        obj.SoundMoveLoop = data[2];
                        break;
                    case "SoundMoveLoopDamaged":
                        obj.SoundMoveLoopDamaged = data[2];
                        break;
                    case "SoundMoveStart":
                        obj.SoundMoveStart = data[2];
                        break;
                    case "SoundMoveStartDamaged":
                        obj.SoundMoveStartDamaged = data[2];
                        break;
                    case "SoundOnDamaged":
                        obj.SoundOnDamaged = data[2];
                        break;
                    case "SoundOnReallyDamaged":
                        obj.SoundOnReallyDamaged = data[2];
                        break;
                    case "SoundPromotedElite":
                        obj.SoundPromotedElite = data[2];
                        break;
                    case "SoundPromotedHero":
                        obj.SoundPromotedHero = data[2];
                        break;
                    case "SoundPromotedVeteran":
                        obj.SoundPromotedVeteran = data[2];
                        break;
                    case "SoundStealthOn":
                        obj.SoundStealthOn = data[2];
                        break;
                    case "SoundStealthOff":
                        obj.SoundStealthOff = data[2];
                        break;

                    //continue with cases

                    default:
                        Console.WriteLine("##INI: unhandled entry in object: " + data[0]);
                        break;
                }
            }
            while (!data[0].Equals("End")); //also test END ?

            if (!objects.ContainsKey(name))
                objects.Add(name, obj);
            else
                //overwrite old object
                objects[name] = obj;
        }
    }
}
