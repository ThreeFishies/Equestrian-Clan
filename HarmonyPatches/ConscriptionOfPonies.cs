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
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using Malee;

namespace Equestrian.MonsterCards
{
    public static class ConscriptionOfPonies 
    {
        public static bool isInit = false;

        public static void ConscriptThemPonies() 
        {
            if (isInit) { return; }

            CardData snackasmacky = CustomCardManager.GetCardDataByID(Snackasmacky.ID);
            CardData mistystep = CustomCardManager.GetCardDataByID(MistyStep.ID);
            if (snackasmacky == null || mistystep == null) { return; }

            SaveManager saveManager = ProviderManager.SaveManager;
            if (saveManager == null) { return; }

            if (!saveManager.IsDlcInstalled(ShinyShoe.DLC.Hellforged)) { return; }

            CollectableRelicData conscriptionnotice = saveManager.GetAllGameData().FindCollectableRelicData("2b7ee5f7-cf89-40b0-ae47-c73c5c879751"); //U3SappedAttack (Conscription Notice)
            CardPool cardPool = conscriptionnotice.GetEffects()[0].GetParamCardPool();

            ReorderableArray<CardData> cards = AccessTools.Field(typeof(CardPool), "cardDataList").GetValue(cardPool) as ReorderableArray<CardData>;
            cards.Add(snackasmacky);
            cards.Add(mistystep);

            AccessTools.Field(typeof(CardPool), "cardDataList").SetValue(cardPool, cards);

            isInit = true;
        }
    }
}