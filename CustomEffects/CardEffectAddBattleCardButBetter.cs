using System;
using System.Collections;
using System.Collections.Generic;
using Trainworks.Builders;
using System.Text;
using HarmonyLib;
using Trainworks.Managers;
using Trainworks.Constants;
using System.Linq;
using UnityEngine;
using Trainworks.Utilities;
using Equestrian.Init;
using Equestrian.MonsterCards;
using ShinyShoe.Loading;
using Equestrian.CardPools;

namespace CustomEffects
{
    //There's probably a better way of doing this, but spawning invisible, hidden cards fixes art issues with the real ones.
    internal class AddThreeBackgroundPoniesArtFix : CardEffectBase
    {
        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
        {
            var saveManager = cardEffectState.SaveManager;
            CardState card = new CardState();

            LoadingScreen.AddTask
            (
                new LoadAdditionalCards
                (
                    CustomCardManager.GetCardDataByID(BackgroundPony.ID), loadSpawnedCharacters: true, LoadingScreen.DisplayStyle.Spinner,
                    delegate
                    {
                        card = saveManager.AddCardToDeck
                        (
                            CustomCardManager.GetCardDataByID(BackgroundPony.ID), null, applyExistingRelicModifiers: true, applyExtraCopiesMutator: false, showAnimation: false, true, false
                        );
                        saveManager.RemoveCardFromDeck(card);
                    }
                )
            );

            //No worky
            /*
            foreach (CardData addablePony in MyCardPools.GivePonyCardPool.GetAllChoices())
            {
                LoadingScreen.AddTask
                (
                    new LoadAdditionalCards
                    (
                        CustomCardManager.GetCardDataByID(BackgroundPony.ID), loadSpawnedCharacters: true, LoadingScreen.DisplayStyle.Spinner,
                        delegate
                        {
                            card = saveManager.AddCardToDeck
                            (
                               addablePony, null, applyExistingRelicModifiers: true, applyExtraCopiesMutator: false, showAnimation: false, true, false
                            );

                            Ponies.Log("Loading asstes for: " + card.GetID());
                            saveManager.RemoveCardFromDeck(card);
                        }
                    )
                );
            }*/

            yield break;
        }
        //        public bool isFixed = false;
    }
}
