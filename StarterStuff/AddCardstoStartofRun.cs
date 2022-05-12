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
using Equestrian;
using Equestrian.Arcadian;
using Equestrian.PonyStory;
using Equestrian.Mutators;
using CustomEffects;

namespace Equestrian.StarterStuff
{
    [HarmonyPatch(typeof(SaveManager), "SetupRun")]
    class DoStarterStuff
    {
        // Postfix is for adding cards for testing
        static void Postfix(ref SaveManager __instance)
        {
            ArcadianCompatibility.InitRun(__instance);
            CustomMutatorEffectAddUpgradeToUnitsForPactShardsAtStartOfRun.SetupCheckNeeded = true;

            //Generate a list of all map nodes for the current run
            //Ponies.Log("____________Map_Node_Explorer____________");
            //List<NodeState> nodes = (List<NodeState>)AccessTools.Field(typeof(RunState),"nodes").GetValue(__instance.GetRunState());
            //foreach(NodeState node in nodes) 
            //{
            //    foreach(NodeState.BranchGroup branch in node.mapNodes) 
            //    { 
            //        foreach(MapNodeData data in branch.Nodes) 
            //        {
            //            Ponies.Log("ID: " + data.GetID() + " Name: " + data.name);
            //        }
            //    }
            //}
            //Ponies.Log("_________________________________________");

            //Just one at a time. Don't be greedy.

            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(DragonCostume.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(BackgroundPony.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(Reserves.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(TimeToShine.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(PastryWarfare.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(BuckOff.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(Snackasmacky.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(MareYouKnow.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(MistyStep.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(PreenySnuggle.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID("8fcb523c-690f-44cb-887d-7112795a50a1")); //kinhost carapace
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID("0c68b12a-0779-4c3c-bf64-6bc5bcf83e9f")); //awoken hollow
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(PartyInvitation.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(PackedAudience.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(VIPList.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(VanillaCardIDs.AwokensRailSpike));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(VanillaCardIDs.AwokenHollow));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(SqueakyBooBoo.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(TavernAce.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(LordOfEmber.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(GuardianOfTheGates.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(PartyCannon.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(BlankFlank.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(SpaTreatment.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(HeartsDesire.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(PoisonJoke.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(YoLo.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(Finchy.ID));
            //__instance.AddRelic(CustomCollectableRelicManager.GetRelicDataByID(BottledCutieMark.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(CrunchieMunchie.ID));
            //__instance.AddRelic(CustomCollectableRelicManager.GetRelicDataByID(MareInTheMoon.ID));
            //__instance.AddRelic(CustomCollectableRelicManager.GetRelicDataByID(TinyMouseCrutches.ID));
            //__instance.AddRelic(CustomCollectableRelicManager.GetRelicDataByID(MysteriousGoldenRod.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(VanillaCardIDs.FormlessChild));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID("c6484604-b077-43ce-84a4-0179d2f36352")); //Hallowed Halls
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID("6f770340-a37d-4e25-9a93-6b2e46f9a252")); //Heph
            //__instance.AddRelic(CustomCollectableRelicManager.GetRelicDataByID(AChildsDrawing.ID));
            //__instance.AddRelic(CustomCollectableRelicManager.GetRelicDataByID(ACollectionOfRibbons.ID));
            //__instance.AddRelic(CustomCollectableRelicManager.GetRelicDataByID(ImaginaryFriends.ID));
            //__instance.AddRelic(CustomCollectableRelicManager.GetRelicDataByID(RoyalScroll.ID));
            //__instance.AddRelic(CustomCollectableRelicManager.GetRelicDataByID(TheSeventhElement.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(SpontaneousSongAndDance.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(Interrogation.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(FanClub.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(WinterWrapUp.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(FirstAid.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(Checklist.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(ChangelingInfiltrator.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(RainbowPower.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(EquestrianRailspike.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(SecondChance.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(TheElementsOfHarmony.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(Tom.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(Alicornification.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(DressedToKill.ID));
            //__instance.AddRelic(CustomCollectableRelicManager.GetRelicDataByID("6bb39014-9b03-4620-b088-f618a7e680b7")); //blank pages
            //__instance.AddRelic(CustomCollectableRelicManager.GetRelicDataByID("2b7ee5f7-cf89-40b0-ae47-c73c5c879751")); //conscription notice
            //__instance.AddRelic(CustomCollectableRelicManager.GetRelicDataByID(Bloomberg.ID));
            //__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(MissingMare.ID));
        }

        public static int loadedClanLevel = 0;

        //Filter for unlocked cards
        static void Prefix(ref SaveManager __instance)
        {
            //This didn't do what I wanted it to.
            //InitStoriesForRun.AddPonyStoiesToRunDataPool(__instance);

            Ponies.Log("Loaded Clan Level: " + loadedClanLevel);
            int currentClanLevel = ProviderManager.SaveManager.GetClassLevel(CustomClassManager.GetClassDataByID(Equestrian.EquestrianClan.ID).GetID());
            Ponies.Log("Current Clan Level: " + currentClanLevel);

            if (currentClanLevel == loadedClanLevel) { return; }

            //var megaPoolDataList = (Malee.ReorderableArray<CardData>)AccessTools.Field(typeof(CardPool), "cardDataList").GetValue(MyCardPools.RefMegaPool);
            //var allBannersDataList = (Malee.ReorderableArray<CardData>)AccessTools.Field(typeof(CardPool), "cardDataList").GetValue(MyCardPools.RefAllBannersPool);

            //Ponies.Log("MegaPoolAddedItemsStart: " + megaPoolDataList.Count);
            //Ponies.Log("AllBannersAddedItemsStart: "+ allBannersDataList.Count); 

            //if (megaPoolDataList.Count > 0) 
            //{
            //    for (int ii = 0; ii < megaPoolDataList.Count; ii++)
            //    {
            //        Ponies.Log("Item in MegaPool: " + megaPoolDataList[ii].name);
            //    }
            //};

            //if (allBannersDataList.Count > 0)
            //{
            //    for (int ii = 0; ii < allBannersDataList.Count; ii++)
            //    {
            //        Ponies.Log("Item in AllBanners: " + allBannersDataList[ii].name);
            //    }
            //};

            if (currentClanLevel > loadedClanLevel) 
            { 
                if (currentClanLevel >= 1 && loadedClanLevel < 1) 
                {
                    //Level 1 appears to be unlocked by default.

                    //level 1 unlocks: "Reserves" and "Tavern Ace"
                    var cardDataList = (Malee.ReorderableArray<CardData>)AccessTools.Field(typeof(CardPool), "cardDataList").GetValue(EquestrianBanner.draftPool);
                    cardDataList.Add(CustomCardManager.GetCardDataByID(TavernAce.ID));
                    //megaPoolDataList.Add(CustomCardManager.GetCardDataByID(Reserves.ID));
                    //allBannersDataList.Add(CustomCardManager.GetCardDataByID(TavernAce.ID));
                }
                if (currentClanLevel >= 2 && loadedClanLevel < 2)
                {
                    //level 2 unlocks: “Junk Food” and “Interrogation”
                    //megaPoolDataList.Add(CustomCardManager.GetCardDataByID(Interrogation.ID));
                }
                if (currentClanLevel >= 3 && loadedClanLevel < 3)
                {
                    //Level 3: “Fan Club” and “A Child's Drawing”
                    //megaPoolDataList.Add(CustomCardManager.GetCardDataByID(FanClub.ID));
                }
                if (currentClanLevel >= 4 && loadedClanLevel < 4)
                {
                    //Level 4: “Bottled Cutie Mark” and “Jestember”
                    var cardDataList = (Malee.ReorderableArray<CardData>)AccessTools.Field(typeof(CardPool), "cardDataList").GetValue(EquestrianBanner.draftPool);
                    cardDataList.Add(CustomCardManager.GetCardDataByID(LordOfEmber.ID));
                    //allBannersDataList.Add(CustomCardManager.GetCardDataByID(LordOfEmber.ID));
                }
                if (currentClanLevel >= 5 && loadedClanLevel < 5)
                {
                    //Level 5: "Mare You Know", "Preeny Snuggles", and Exile Champion
                    var cardDataList = (Malee.ReorderableArray<CardData>)AccessTools.Field(typeof(CardPool), "cardDataList").GetValue(EquestrianBanner.draftPool);
                    cardDataList.Add(CustomCardManager.GetCardDataByID(PreenySnuggle.ID));
                    cardDataList.Add(CustomCardManager.GetCardDataByID(MareYouKnow.ID));
                    var givePonyDataList = (Malee.ReorderableArray<CardData>)AccessTools.Field(typeof(CardPool), "cardDataList").GetValue(MyCardPools.GivePonyCardPool);
                    givePonyDataList.Add(CustomCardManager.GetCardDataByID(PreenySnuggle.ID));
                    givePonyDataList.Add(CustomCardManager.GetCardDataByID(MareYouKnow.ID));
                    var mareALeeDataList = (Malee.ReorderableArray<CardData>)AccessTools.Field(typeof(CardPool), "cardDataList").GetValue(MyCardPools.MareALeeMentorIIICardPool);
                    mareALeeDataList.Add(CustomCardManager.GetCardDataByID(PreenySnuggle.ID));
                    mareALeeDataList.Add(CustomCardManager.GetCardDataByID(MareYouKnow.ID));
                    var fixArtDataList = (Malee.ReorderableArray<CardData>)AccessTools.Field(typeof(CardPool), "cardDataList").GetValue(MyCardPools.FixArtCardPool);
                    fixArtDataList.Add(CustomCardManager.GetCardDataByID(PreenySnuggle.ID));
                    fixArtDataList.Add(CustomCardManager.GetCardDataByID(MareYouKnow.ID));
                    //allBannersDataList.Add(CustomCardManager.GetCardDataByID(PreenySnuggle.ID));
                    //allBannersDataList.Add(CustomCardManager.GetCardDataByID(MareYouKnow.ID));
                }
                if (currentClanLevel >= 6 && loadedClanLevel < 6)
                {
                    //Level 6: “Moon Rock” and “Changeling Infiltrator”
                    //megaPoolDataList.Add(CustomCardManager.GetCardDataByID(ChangelingInfiltrator.ID));
                }
                if (currentClanLevel >= 7 && loadedClanLevel < 7)
                {
                    //Level 7: “Buck Off” and “Imaginary Friends”
                    //megaPoolDataList.Add(CustomCardManager.GetCardDataByID(BuckOff.ID));
                }
                if (currentClanLevel >= 8 && loadedClanLevel < 8)
                {
                    //Level 8: “Poison Joke” and “Heart's Desire”
                    //megaPoolDataList.Add(CustomCardManager.GetCardDataByID(PoisonJoke.ID));
                    //megaPoolDataList.Add(CustomCardManager.GetCardDataByID(HeartsDesire.ID));
                }
                if (currentClanLevel >= 9 && loadedClanLevel < 9)
                {
                    //Level 9: “Royal Scroll” and “Rainbow Power”
                    //megaPoolDataList.Add(CustomCardManager.GetCardDataByID(RainbowPower.ID));
                }
                if (currentClanLevel >= 10 && loadedClanLevel < 10)
                {
                    //Level 10: “Alicornification”
                    //megaPoolDataList.Add(CustomCardManager.GetCardDataByID(Alicornification.ID));
                }

                loadedClanLevel = currentClanLevel;

                //Ponies.Log("MegaPoolAddedItemsEnd: " + megaPoolDataList.Count);
                //Ponies.Log("AllBannersAddedItemsEnd: " + allBannersDataList.Count);

                Ponies.Log("Loading complete");
            }
        }
    }
}