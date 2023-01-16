using Trainworks.Managers;
using Trainworks.Builders;
using Trainworks.Constants;
using Equestrian.Init;
using Equestrian.Relic;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HarmonyLib;
using ShinyShoe;
using Equestrian.Sprites;
using Equestrian.MonsterCards;
using UnityEngine;
using CustomEffects;
using System.Runtime.CompilerServices;
using static UnityEngine.UI.CanvasScaler;
using Equestrian.Chatter;

namespace Equestrian.Chatter
{
    public static class MakeThemChatter
    {
        public static void DoIt() 
        {
            CharacterData unit;
            List<string> list;

            //Hellhorned Champions
            unit = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.HornbreakerPrince);
            list = new List<string>()
            {
                "Idle_Chatter_HornbreakerPrince_Key1",
                "Idle_Chatter_HornbreakerPrince_Key2",
                "Idle_Chatter_HornbreakerPrince_Key3"
            };
            CharacterChatterUpdater.AddIdleChatterKey(unit, list);
            unit = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.ShardtailQueen);
            list = new List<string>()
            {
                "Idle_Chatter_ShardtailQueen_Key1",
                "Idle_Chatter_ShardtailQueen_Key2",
                "Idle_Chatter_ShardtailQueen_Key3"
            };
            CharacterChatterUpdater.AddIdleChatterKey(unit, list);

            //Awoken Champions
            unit = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.TheSentient);
            list = new List<string>()
            {
                "Idle_Chatter_TheSentient_Key1",
                "Idle_Chatter_TheSentient_Key2",
                "Idle_Chatter_TheSentient_Key3"
            };
            CharacterChatterUpdater.AddIdleChatterKey(unit, list);
            unit = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.Wyldenten);
            list = new List<string>()
            {
                "Idle_Chatter_Wyldenten_Key1",
                "Idle_Chatter_Wyldenten_Key2",
                "Idle_Chatter_Wyldenten_Key3"
            };
            CharacterChatterUpdater.AddIdleChatterKey(unit, list);

            //Stygian Champions
            unit = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.TethysTitansbane);
            list = new List<string>()
            {
                "Idle_Chatter_TethysTitansbane_Key1",
                "Idle_Chatter_TethysTitansbane_Key2",
                "Idle_Chatter_TethysTitansbane_Key3"
            };
            CharacterChatterUpdater.AddIdleChatterKey(unit, list);
            unit = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.SolgardtheMartyr);
            list = new List<string>()
            {
                "Idle_Chatter_SolgardtheMartyr_Key1",
                "Idle_Chatter_SolgardtheMartyr_Key2",
                "Idle_Chatter_SolgardtheMartyr_Key3"
            };
            CharacterChatterUpdater.AddIdleChatterKey(unit, list);

            //Umbra champions
            unit = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.Penumbra);
            list = new List<string>()
            {
                "Idle_Chatter_Penumbra_Key1",
                "Idle_Chatter_Penumbra_Key2",
                "Idle_Chatter_Penumbra_Key3"
            };
            CharacterChatterUpdater.AddIdleChatterKey(unit, list);
            unit = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.Primordium);
            list = new List<string>()
            {
                "Idle_Chatter_Primordium_Key1",
                "Idle_Chatter_Primordium_Key2",
                "Idle_Chatter_Primordium_Key3",
                "Idle_Chatter_Primordium_Key4"
            };
            CharacterChatterUpdater.AddIdleChatterKey(unit, list);

            //Melting Remnant champions
            unit = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.RectorFlicker);
            list = new List<string>()
            {
                "Idle_Chatter_RectorFlicker_Key1",
                "Idle_Chatter_RectorFlicker_Key2",
                "Idle_Chatter_RectorFlicker_Key3"
            };
            CharacterChatterUpdater.AddIdleChatterKey(unit, list);
            unit = CustomCharacterManager.GetCharacterDataByID(VanillaCharacterIDs.LittleFade);
            list = new List<string>()
            {
                "Idle_Chatter_LittleFade_Key1",
                "Idle_Chatter_LittleFade_Key2",
                "Idle_Chatter_LittleFade_Key3"
            };
            CharacterChatterUpdater.AddIdleChatterKey(unit, list);

            //Wurmkin champions
            //MonsterWurmChampion1
            unit = CustomCharacterManager.GetCharacterDataByID("43ac682e-92da-4518-b2ee-c64b0c77a1f5");
            list = new List<string>()
            {
                "Idle_Chatter_SpineChief_Key1"
            };
            CharacterChatterUpdater.AddIdleChatterKey(unit, list);
            //MonsterWurmChampion2
            unit = CustomCharacterManager.GetCharacterDataByID("5c18728d-27e2-4b9b-835c-a69b1689eb9c");
            list = new List<string>()
            {
                "Idle_Chatter_Echowright_Key1",
                "Idle_Chatter_Echowright_Key2",
                "Idle_Chatter_Echowright_Key3"
            };
            CharacterChatterUpdater.AddIdleChatterKey(unit, list);
        }
    }

    public static class MakeMoreChatter 
    { 
        public static bool DoArcadian() 
        { 
            if (!Arcadian.ArcadianCompatibility.ArcadianExists) 
            {
                return false;
            }

            CharacterData unit;
            List<string> list;
            ChampionData champion;

            //Arcadian Champions
            //First Disciple
            champion = Arcadian.ArcadianCompatibility.ArcadianClan.GetChampionData(0);
            unit = champion.championCardData.GetSpawnCharacterData();
            list = new List<string>()
            {
                "Idle_Chatter_Disciple_Key1",
                "Idle_Chatter_Disciple_Key2",
                "Idle_Chatter_Disciple_Key3",
                "Idle_Chatter_Disciple_Key4",
                "Idle_Chatter_Disciple_Key5",
                "Idle_Chatter_Disciple_Key6",
                "Idle_Chatter_Disciple_Key7",
                "Idle_Chatter_Disciple_Key8",
                "Idle_Chatter_Disciple_Key9",
                "Idle_Chatter_Disciple_Key10",
                "Idle_Chatter_Disciple_Key11"
            };
            CharacterChatterUpdater.AddIdleChatterKey(unit, list, CharacterChatterData.Gender.Male);
            //Second Disciple
            champion = Arcadian.ArcadianCompatibility.ArcadianClan.GetChampionData(1);
            unit = champion.championCardData.GetSpawnCharacterData();
            list = new List<string>()
            {
                "Idle_Chatter_Glaukopis_Key1",
                "Idle_Chatter_Glaukopis_Key2",
                "Idle_Chatter_Glaukopis_Key3",
                "Idle_Chatter_Glaukopis_Key4",
                "Idle_Chatter_Glaukopis_Key5",
                "Idle_Chatter_Glaukopis_Key6",
                "Idle_Chatter_Glaukopis_Key7",
                "Idle_Chatter_Glaukopis_Key8",
                "Idle_Chatter_Glaukopis_Key9",
                "Idle_Chatter_Glaukopis_Key10",
                "Idle_Chatter_Glaukopis_Key11"
            };
            CharacterChatterUpdater.AddIdleChatterKey(unit, list, CharacterChatterData.Gender.Male);

            return true;
        }

        public static bool DoSweetkin() 
        {
            if (!Trainworks.Managers.PluginManager.GetAllPluginGUIDs().Contains("Exas4000"))
            {
                //Ponies.Log("Failed to find Sweetkin Clan.");
                return false;
            }

            ClassData clan = CustomClassManager.GetClassDataByID("Sweetkin_Clan");

            if (clan == null) 
            {
                Ponies.LogError("Sweetkin detected but failed to find class data. Can't add battle quotes.");
                return false;
            }

            ChampionData champion;
            CharacterData unit;
            List<string> list;

            //Sweetkin Champions
            //Rosette
            champion = clan.GetChampionData(0);
            unit = champion.championCardData.GetSpawnCharacterData();
            list = new List<string>()
            {
                "Idle_Chatter_Rosette_Key1",
                "Idle_Chatter_Rosette_Key2",
                "Idle_Chatter_Rosette_Key3",
                "Idle_Chatter_Rosette_Key4",
                "Idle_Chatter_Rosette_Key5",
                "Idle_Chatter_Rosette_Key6",
                "Idle_Chatter_Rosette_Key7",
                "Idle_Chatter_Rosette_Key8",
                "Idle_Chatter_Rosette_Key9",
                "Idle_Chatter_Rosette_Key10",
                "Idle_Chatter_Rosette_Key11"
            };
            CharacterChatterUpdater.AddIdleChatterKey(unit, list, CharacterChatterData.Gender.Female);
            //Sweet Cherry Sally
            champion = clan.GetChampionData(1);
            unit = champion.championCardData.GetSpawnCharacterData();
            list = new List<string>()
            {
                "Idle_Chatter_SweetCherrySally_Key1",
                "Idle_Chatter_SweetCherrySally_Key2",
                "Idle_Chatter_SweetCherrySally_Key3",
                "Idle_Chatter_SweetCherrySally_Key4",
                "Idle_Chatter_SweetCherrySally_Key5",
                "Idle_Chatter_SweetCherrySally_Key6",
                "Idle_Chatter_SweetCherrySally_Key7",
                "Idle_Chatter_SweetCherrySally_Key8",
                "Idle_Chatter_SweetCherrySally_Key9",
                "Idle_Chatter_SweetCherrySally_Key10",
                "Idle_Chatter_SweetCherrySally_Key11",
            };
            CharacterChatterUpdater.AddIdleChatterKey(unit, list, CharacterChatterData.Gender.Female);

            return true;
        }

        public static bool DoSucc()
        {
            if (!Trainworks.Managers.PluginManager.GetAllPluginGUIDs().Contains("com.name.package.succclan-mod"))
            {
                //Ponies.Log("Failed to find Succubus Clan.");
                return false;
            }

            ClassData clan = CustomClassManager.GetClassDataByID("Succubus");

            if (clan == null)
            {
                Ponies.LogError("Succubus detected but failed to find class data. Can't add battle quotes.");
                return false;
            }

            ChampionData champion;
            CharacterData unit;
            List<string> list;

            //Succubus Champions
            //Shadow Lady
            champion = clan.GetChampionData(0);
            unit = champion.championCardData.GetSpawnCharacterData();
            list = new List<string>()
            {
                "Idle_Chatter_ShadowLady_Key1",
                "Idle_Chatter_ShadowLady_Key2",
                "Idle_Chatter_ShadowLady_Key3",
                "Idle_Chatter_ShadowLady_Key4",
                "Idle_Chatter_ShadowLady_Key5",
                "Idle_Chatter_ShadowLady_Key6",
                "Idle_Chatter_ShadowLady_Key7",
                "Idle_Chatter_ShadowLady_Key8",
                "Idle_Chatter_ShadowLady_Key9",
                "Idle_Chatter_ShadowLady_Key10",
                "Idle_Chatter_ShadowLady_Key11",
            };
            CharacterChatterUpdater.AddIdleChatterKey(unit, list, CharacterChatterData.Gender.Female);
            //Shadow Lady
            champion = clan.GetChampionData(1);
            unit = champion.championCardData.GetSpawnCharacterData();
            list = new List<string>()
            {
                "Idle_Chatter_Knightmare_Key1",
                "Idle_Chatter_Knightmare_Key2",
                "Idle_Chatter_Knightmare_Key3",
                "Idle_Chatter_Knightmare_Key4",
                "Idle_Chatter_Knightmare_Key5",
                "Idle_Chatter_Knightmare_Key6",
                "Idle_Chatter_Knightmare_Key7",
                "Idle_Chatter_Knightmare_Key8",
                "Idle_Chatter_Knightmare_Key9",
                "Idle_Chatter_Knightmare_Key10",
                "Idle_Chatter_Knightmare_Key11",
            };
            CharacterChatterUpdater.AddIdleChatterKey(unit, list, CharacterChatterData.Gender.Male);

            return true;
        }
    }
}

namespace Equestrian.HarmonyPatches 
{
    [HarmonyPatch(typeof(CombatManager), "StartCombat")]
    public static class InitCustomClanBattleQuotes 
    {
        public static bool isSetup = false;

        public static void Prefix() 
        { 
            if (isSetup) { return; }
            if (!Ponies.EquestrianClanIsInit) { return; }

            Ponies.Log("Initializing character chatter data update for custom clans.");

            MakeMoreChatter.DoArcadian();
            MakeMoreChatter.DoSweetkin();
            MakeMoreChatter.DoSucc();

            isSetup = true;
        }
    }
}