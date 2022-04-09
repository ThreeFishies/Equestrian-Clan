using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Equestrian.Init;
using Trainworks.Managers;
using Equestrian.SpellCards;
using Equestrian.MonsterCards;
using Equestrian.Relic;
using Trainworks.Constants;
using Trainworks.Builders;
using Equestrian.CardPools;
using Equestrian.Champions;

namespace Equestrian.HarmonyPatches
{
    [HarmonyPatch(typeof(RandomChampionPool), "GetAllChampions")]
    public static class RandomChampionPoolPatch 
    { 
        public static bool ChampsAdded = false;
        public static CardPool myChamps = UnityEngine.ScriptableObject.CreateInstance<CardPool>();
        public static void Postfix(ref List<RandomChampionPool.GrantedChampionInfo> __result)
        {
            if (!ChampsAdded) 
            {
                myChamps = new CardPoolBuilder
                {
                    CardPoolID = "myChampsForArt",
                    CardIDs = new List<string>
                    {
                        CustomCardManager.GetCardDataByID(MareaLee.ID).GetID(),
                        CustomCardManager.GetCardDataByID(Tantabus.ID).GetID(),
                    }
                }.BuildAndRegister();

                ChampsAdded = true;
            }

            FixArt.TryYetAnotherFix(myChamps);

            //Ponies.Log("Setting up Blank Pages");
            //Ponies.Log("Currect Champion List Count" + __result.Count);

            //__result.Clear();

            __result.Add(new RandomChampionPool.GrantedChampionInfo()
            {
                championCard = CustomCardManager.GetCardDataByID(MareaLee.ID),
                upgrades = new List<CardUpgradeData>()
                {
                    Mentor_III.Make(),
                }
            });
            __result.Add(new RandomChampionPool.GrantedChampionInfo()
            {
                championCard = CustomCardManager.GetCardDataByID(MareaLee.ID),
                upgrades = new List<CardUpgradeData>()
                {
                    Coach_III.Make(),
                }
            });
            __result.Add(new RandomChampionPool.GrantedChampionInfo()
            {
                championCard = CustomCardManager.GetCardDataByID(MareaLee.ID),
                upgrades = new List<CardUpgradeData>()
                {
                    Tutor_III.Make(),
                }
            });
            __result.Add(new RandomChampionPool.GrantedChampionInfo()
            {
                championCard = CustomCardManager.GetCardDataByID(Tantabus.ID),
                upgrades = new List<CardUpgradeData>()
                {
                    Psychosis_III.Make(),
                }
            });
            __result.Add(new RandomChampionPool.GrantedChampionInfo()
            {
                championCard = CustomCardManager.GetCardDataByID(Tantabus.ID),
                upgrades = new List<CardUpgradeData>()
                {
                    Nightmare_III.Make(),
                }
            });
            __result.Add(new RandomChampionPool.GrantedChampionInfo()
            {
                championCard = CustomCardManager.GetCardDataByID(Tantabus.ID),
                upgrades = new List<CardUpgradeData>()
                {
                    Lucid_III.Make(),
                }
            });

            //Ponies.Log("Setup Complete.");
            //Ponies.Log("Currect Champion List Count" + __result.Count);

            //ChampsAdded = true;
        }
    }
}