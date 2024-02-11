using System;
using System.Collections.Generic;
using System.Collections;
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
using ShinyShoe.Loading;

namespace Equestrian.HarmonyPatches
{
    /*
    public static class WaitForArt 
    { 
        public static IEnumerator Wait() 
        {
            if (LoadingScreen.HasTask<LoadAdditionalCards>())
            {
                yield return new UnityEngine.WaitUntil( (() => LoadingScreen.HasTask<LoadAdditionalCards>() == false) );
            }
            yield break;
        }
    }
    */

    [HarmonyPatch(typeof(CombatManager), "StartCombat")]
    public static class FixArtForBlankPages 
    {
        public static string currentRunID = "new run";
        public static string blankPagesID = "6bb39014-9b03-4620-b088-f618a7e680b7";
        public static List<CardData> champsForArt = new List<CardData>() { };

        public static void Prefix() 
        { 
            if (!Ponies.EquestrianClanIsInit) { return; }

            if (!ProviderManager.SaveManager.GetHasRelic(ProviderManager.SaveManager.GetAllGameData().FindCollectableRelicData(blankPagesID))) 
            {
                return;
            }

            SaveData activeSave = (SaveData)AccessTools.Method(typeof(SaveManager), "get_ActiveSaveData").Invoke(ProviderManager.SaveManager, new object[] {});

            if (activeSave == null) { return; }

            if (currentRunID != activeSave.GetID()) 
            { 
                currentRunID = activeSave.GetID();
                champsForArt.Clear();
                RandomChampionPoolPatch.GetAllValidChamps(out champsForArt);
            }

            FixArt.TryYetAnotherFix(champsForArt, ShinyShoe.Loading.LoadingScreen.DisplayStyle.FullScreen);
            //while (WaitForArt.Wait().MoveNext()) { };
        }
    }

    [HarmonyPatch(typeof(RandomChampionPool), "GetAllChampions")]
    public static class RandomChampionPoolPatch 
    { 
        //public static bool ChampsAdded = false;
        //public static CardPool myChamps = UnityEngine.ScriptableObject.CreateInstance<CardPool>();

        public static List<RandomChampionPool.GrantedChampionInfo> GetAllValidChamps(out List<CardData> champsForArt)
        {
            List<RandomChampionPool.GrantedChampionInfo> newChamps = new List<RandomChampionPool.GrantedChampionInfo> { };
            champsForArt = new List<CardData>();

            List<ClassData> unfilteredClasses = ProviderManager.SaveManager.GetAllGameData().GetAllClassDatas();
            foreach (ClassData classData in unfilteredClasses)
            {
                if (ProviderManager.SaveManager.IsUnlockedAndAvailableWhenStartingRun(classData))
                {
                    int champCount = classData.GetAllChampionCards().Count;
                    if (ProviderManager.SaveManager.GetMetagameSave().GetLevel(classData.GetID()) < 5) 
                    { 
                        if (champCount > 1) 
                        {
                            champCount--;
                        }
                    }

                    for (int ii = 0; ii < champCount; ii++)
                    {
                        if (IsCustomClan(classData))
                        {
                            champsForArt.Add(classData.GetChampionCard(ii));
                        }
                        CardUpgradeTreeData baseChampUpgradeTree = classData.GetChampionData(ii).upgradeTree;
                        for (int jj = 0; jj < baseChampUpgradeTree.GetUpgradeTrees().Count; jj++) 
                        {
                            int index = baseChampUpgradeTree.GetUpgradeTrees()[jj].GetCardUpgrades().Count - 1;

                            if (ProviderManager.SaveManager.GetCurrentDistance() < 6)
                            {
                                index = (index > 1) ? 1 : index;
                            }
                            if (ProviderManager.SaveManager.GetCurrentDistance() < 3) 
                            {
                                index = (index > 0) ? 0 : index;
                            }
                            index = (index < 0) ? 0 : index;

                            RandomChampionPool.GrantedChampionInfo newChamp = new RandomChampionPool.GrantedChampionInfo()
                            {
                                championCard = classData.GetChampionCard(ii),
                                upgrades = new List<CardUpgradeData>
                                {
                                    baseChampUpgradeTree.GetUpgradeTrees()[jj].GetCardUpgrades()[index]
                                }
                            };
                            //Ponies.Log("Champ: " + classData.GetChampionCard(ii).name + " Upgrade: " + baseChampUpgradeTree.GetUpgradeTrees()[jj].GetCardUpgrades()[index].GetUpgradeTitleKey().Localize());
                            newChamps.Add(newChamp);
                        }
                    }
                }
            }

            return newChamps;
        }

        public static bool IsCustomClan(ClassData clan) 
        {
            List<string> AllVanillaClanIDs = new List<string>()
            {
                VanillaClanIDs.Clanless,
                VanillaClanIDs.Awoken,
                VanillaClanIDs.Hellhorned,
                VanillaClanIDs.Stygian,
                VanillaClanIDs.Umbra,
                VanillaClanIDs.MeltingRemnant,
                "46ae87db-d92e-4fcb-a3bc-67c723d7bebd" //Wurmkin
            };

            if (AllVanillaClanIDs.Contains( clan.GetID()) )
            {
                return false;
            }
            return true;
        }

        public static void Postfix(ref List<RandomChampionPool.GrantedChampionInfo> __result)
        {
            if (!Ponies.EquestrianClanIsInit) { return; }

            //List<CardData> champsForArt = new List<CardData> { };

            __result = GetAllValidChamps(out _);

            /*
            if (!ChampsAdded)
            {
                myChamps = new CardPoolBuilder
                {
                    CardPoolID = "myChampsForArt",
                    //CardIDs = new List<string>
                    //{
                    //    CustomCardManager.GetCardDataByID(MareaLee.ID).GetID(),
                    //    CustomCardManager.GetCardDataByID(Tantabus.ID).GetID(),
                    //},
                    Cards = champsForArt
                }.BuildAndRegister();

                ChampsAdded = true;
            }

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

            */
        }
    }
}