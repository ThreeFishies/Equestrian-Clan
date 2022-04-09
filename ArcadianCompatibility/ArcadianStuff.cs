using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Equestrian.Init;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;
using Trainworks.Enums;
using Equestrian;
using CustomEffects;
using Equestrian.CardPools;
using HarmonyLib;

namespace Equestrian.Arcadian
{ 
    public static class ArcadianCompatibility
    {
        public static bool ArcadianExists = false;

        public static bool isArcadian = false;
        public static bool isArcadianExile = false;
        public static bool isEquestrian = false;
        public static bool isEquestrianExile = false;

        public static ClassData ArcadianClan = null;
        public static CardData Analog = null;
        public static CardPool VampireFruitBatCardPool = null;

        //public static CardUI.MasteryType AnalogMastery = CardUI.MasteryType.None;

        public static void Initialize()
        { 
            if (Trainworks.Managers.PluginManager.GetAllPluginGUIDs().Contains("ca.chronometry.disciple")) 
            {
                ArcadianExists = true;
                Ponies.Log("Arcadian Clan exists. Add compatibility.");
            }

            if (ArcadianExists) 
            {
                ArcadianClan = CustomClassManager.GetClassDataByID("Chrono");

                if (ArcadianClan == null) 
                {
                    Ponies.Log("Error: Unable to retrieve data for Arcadian Clan.");
                    ArcadianExists = false;
                    return;
                }

                Analog = CustomCardManager.GetCardDataByID("Analog");

                if (Analog == null)
                {
                    Ponies.Log("Error: Unable to retrieve data for Arcadian spell Analog.");
                    ArcadianExists = false;
                    return;
                }
                else
                {
                    //AnalogMastery = ProviderManager.SaveManager.GetMetagameSave().GetMastery(Analog);
                }
                VampireFruitBat.BuildAndRegister();
                Ponies.Log("Vampire Fruit Bat");

                VampireFruitBatCardPool = new CardPoolBuilder()
                {
                    CardPoolID = VampireFruitBat.CardPoolID,
                    Cards = new List<CardData>() 
                    { 
                        VampireFruitBat.VampireFruitBatCardData
                    }
                }.BuildAndRegister();
                Ponies.Log("Vampire Fruit Bat card pool");

                EquestrianAnalogBase.BuildAndRegister();
                Ponies.Log("Equestrian Analog Base");

                EquestrianAnalogExile.BuildAndRegister();
                Ponies.Log("Equestrian Analog Exile");
            }
        }

        public static bool CheckRunStatus() 
        {
            SaveManager saveManager = ProviderManager.SaveManager;

            if (!saveManager.IsInBattle() || !ArcadianExists) 
            {
                return false;
            }

            isArcadian = false;
            isArcadianExile = false;
            isEquestrian = false;
            isEquestrianExile = false;

            if (saveManager.GetMainClass() != null)
            {
                if ((saveManager.GetMainClass().GetID() == Ponies.EquestrianClanData.GetID()))
                {
                    isEquestrian = true;

                    if (saveManager.GetMainChampionIndex() > 0)
                    {
                        isEquestrianExile = true;
                    }
                }
                else if ((saveManager.GetSubClass().GetID() == Ponies.EquestrianClanData.GetID()))
                {
                    isEquestrian = true;

                    if (saveManager.GetSubChampionIndex() > 0)
                    {
                        isEquestrianExile = true;
                    }
                }

                if ((saveManager.GetMainClass().GetID() == ArcadianClan.GetID()))
                {
                    isArcadian = true;

                    if (saveManager.GetMainChampionIndex() > 0)
                    {
                        isArcadianExile = true;
                    }
                }
                else if ((saveManager.GetSubClass().GetID() == ArcadianClan.GetID()))
                {
                    isArcadian = true;

                    if (saveManager.GetSubChampionIndex() > 0)
                    {
                        isArcadianExile = true;
                    }
                }
            }
            else
            {
                Ponies.Log("Attempting to init a run without a main class. Aborting.");
                return false;
            }

            return true;
        }

        public static bool InitRun(SaveManager saveManager) 
        {
            if (!CheckRunStatus()) 
            {
                return false;
            }

            //replace Analog with Equestrian Analog
            if (isArcadianExile && isEquestrian) 
            {
                CardData EquestrianAnalogReplacement = null;
                List<CardState> CardsToReplace = new List<CardState>();

                if (!isEquestrianExile) 
                {
                    EquestrianAnalogReplacement = CustomCardManager.GetCardDataByID(EquestrianAnalogBase.ID);
                }
                else 
                {
                    EquestrianAnalogReplacement = CustomCardManager.GetCardDataByID(EquestrianAnalogExile.ID);
                    /*
                    if (!MyCardPools.FixArtCardPool.Contains(CustomCardManager.GetCardDataByID(VampireFruitBat.ID).GetID())) 
                    {
                        var cardDataList = (Malee.ReorderableArray<CardData>)AccessTools.Field(typeof(CardPool), "cardDataList").GetValue(MyCardPools.FixArtCardPool);
                        cardDataList.Add(CustomCardManager.GetCardDataByID(VampireFruitBat.ID));
                    }
                    */
                }

                foreach (CardState card in saveManager.GetDeckState())
                {
                    if (card.GetCardDataID() == Analog.GetID())
                    {
                        CardsToReplace.Add(card);
                    }
                }

                if (CardsToReplace.Count > 0)
                {
                    for (int ii = 0; ii < CardsToReplace.Count; ii++) 
                    {
                        saveManager.AddCardToDeck(EquestrianAnalogReplacement, CardsToReplace[ii].GetCardStateModifiers(), false, false, false, false, true);
                    }
                }

                foreach (CardState replaceableAnalog in CardsToReplace)
                {
                    saveManager.RemoveCardFromDeck(replaceableAnalog);
                }
            }

            return true;
        }
    }
}