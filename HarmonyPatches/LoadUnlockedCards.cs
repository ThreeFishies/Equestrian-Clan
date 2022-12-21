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

namespace Equestrian.HarmonyPatches
{
    [HarmonyPatch(typeof(UnlockScreen), "GetClanLevelUpUnlockItems")]
    public static class LoadCardsForLevelUP
    {
        static void Postfix(ref SaveManager saveManager, ref ClassData classData, ref int newLevel) 
        { 
            if (CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID() == classData.GetID()) 
            {
                CardPool cardsToShow = UnityEngine.ScriptableObject.CreateInstance<CardPool>();

                Ponies.Log("Level up to level: " + newLevel);

                CardPoolBuilder cardPoolBuilder = new CardPoolBuilder() 
                { 
                    CardPoolID = "EquestrianClanNewLevel_"+newLevel,
                    CardIDs = new List<string> { }
                };

                cardsToShow = cardPoolBuilder.BuildAndRegister();
                var cardDataList = (Malee.ReorderableArray<CardData>)AccessTools.Field(typeof(CardPool), "cardDataList").GetValue(cardsToShow);                

                if (newLevel == 1) 
                {
                    cardDataList.Add(CustomCardManager.GetCardDataByID(Reserves.ID));
                    cardDataList.Add(CustomCardManager.GetCardDataByID(TavernAce.ID));
                }
                if (newLevel == 2)
                {
                    cardDataList.Add(CustomCardManager.GetCardDataByID(Interrogation.ID));
                }
                if (newLevel == 3)
                {
                    cardDataList.Add(CustomCardManager.GetCardDataByID(FanClub.ID));
                }
                if (newLevel == 4)
                {
                    cardDataList.Add(CustomCardManager.GetCardDataByID(LordOfEmber.ID));
                }
                if (newLevel == 5)
                {
                    cardDataList.Add(CustomCardManager.GetCardDataByID(MareYouKnow.ID));
                    cardDataList.Add(CustomCardManager.GetCardDataByID(PreenySnuggle.ID));
                }
                if (newLevel == 6)
                {
                    cardDataList.Add(CustomCardManager.GetCardDataByID(ChangelingInfiltrator.ID));
                }
                if (newLevel == 7)
                {
                    cardDataList.Add(CustomCardManager.GetCardDataByID(BuckOff.ID));
                }
                if (newLevel == 8)
                {
                    cardDataList.Add(CustomCardManager.GetCardDataByID(PoisonJoke.ID));
                    cardDataList.Add(CustomCardManager.GetCardDataByID(HeartsDesire.ID));
                }
                if (newLevel == 9)
                {
                    cardDataList.Add(CustomCardManager.GetCardDataByID(RainbowPower.ID));
                }
                if (newLevel == 10)
                {
                    cardDataList.Add(CustomCardManager.GetCardDataByID(Alicornification.ID));
                }

                FixArt.TryYetAnotherFix(cardsToShow, ShinyShoe.Loading.LoadingScreen.DisplayStyle.Spinner);
            }
        }
    }
}