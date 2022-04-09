using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Equestrian.Init;
using Trainworks.Managers;
using Equestrian.SpellCards;
using Equestrian.MonsterCards;
using ShinyShoe.Loading;
using Equestrian.CardPools;
using System.Collections;
using ShinyShoe;
using ShinyShoe.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Trainworks.Constants;


namespace Equestrian.HarmonyPatches
{
    //The art has been fixed.
    //This function is called when the "Give Pony" button is added to the UI.
    //A short "loading" circle appears at the start of battle while this runs.
	public static class FixArt
	{
		public static void TryYetAnotherFix(CardPool cardPool) 
		{
            var saveManager = Trainworks.Managers.ProviderManager.SaveManager;
            CardState card = new CardState();

            Ponies.Log("Loading art and character assets for ponies.");
            Ponies.Log("Loading from Card Pool: " + cardPool.name);
            Ponies.Log(cardPool.GetAllChoices().Count + " cards to load.");

            //foreach (CardData pony in MyCardPools.GivePonyCardPool.GetAllChoices())
            foreach (CardData pony in cardPool.GetAllChoices())
            {
                LoadingScreen.AddTask
                (
                    new LoadAdditionalCards
                    (
                        CustomCardManager.GetCardDataByID(pony.GetID()), loadSpawnedCharacters: true, LoadingScreen.DisplayStyle.Spinner,
                        delegate
                        {
                            card = saveManager.AddCardToDeck
                            (
                                CustomCardManager.GetCardDataByID(pony.GetID()), null, applyExistingRelicModifiers: true, applyExtraCopiesMutator: false, showAnimation: false, true, false
                            );
                            saveManager.RemoveCardFromDeck(card);
                        }
                    )
                );
            }
        }
	}
}