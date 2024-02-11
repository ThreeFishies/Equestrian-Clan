using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Trainworks.Builders;
using Trainworks.Managers;
using Trainworks.Enums;
using Trainworks.Constants;
using Equestrian.Init;
using Equestrian;
using Equestrian.CardPools;
using CustomEffects;
using Equestrian.Enhancers;
using System.Collections;

namespace CustomEffects
{ 
    public class CustomMutatorEffectReplaceHandOnSpellPlay : RelicEffectBase, ICardPlayedRelicEffect, IRelicEffect
    {
        // Token: 0x060016D5 RID: 5845 RVA: 0x00059F58 File Offset: 0x00058158
        public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
        {
            base.Initialize(relicState, relicData, relicEffectData);
            this.cardType = relicEffectData.GetParamCardType();
            this.requiredTraits = new List<string>();
            foreach (CardTraitData cardTraitData in relicEffectData.GetTraits())
            {
                this.requiredTraits.Add(cardTraitData.GetTraitStateName());
            }
            this.numCopies = relicEffectData.GetParamInt();
        }

        // Token: 0x060016D6 RID: 5846 RVA: 0x00059FE4 File Offset: 0x000581E4
        public bool TestCardPlayed(CardPlayedRelicEffectParams relicEffectParams)
        {
            if (this.cardType != CardType.Invalid && relicEffectParams.cardState.GetCardType() != this.cardType)
            {
                return false;
            }
            foreach (string a in this.requiredTraits)
            {
                bool flag = false;
                foreach (CardTraitData cardTraitData in relicEffectParams.cardState.GetTraits())
                {
                    if (a == cardTraitData.GetTraitStateName())
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    return false;
                }
            }
            return true;
        }

        // Token: 0x060016D7 RID: 5847 RVA: 0x0005A0B0 File Offset: 0x000582B0
        public IEnumerator OnCardPlayed(CardPlayedRelicEffectParams relicEffectParams)
        {
            base.NotifyRoomRelicTriggered(relicEffectParams);
            yield return ConsumeHand(relicEffectParams);
            CardData cardData = relicEffectParams.saveManager.GetAllGameData().FindCardData(relicEffectParams.cardState.GetCardDataID());
            CardManager.AddCardUpgradingInfo addCardUpgradingInfo = new CardManager.AddCardUpgradingInfo
            {
                tempCardUpgrade = true,
                upgradingCardSource = relicEffectParams.cardState,
                ignoreTempUpgrades = false,
                copyModifiersFromCard = relicEffectParams.cardState
            };
            for (int i = 0; i < this.numCopies; i++)
            {
                relicEffectParams.cardManager.AddCard(cardData, CardPile.HandPile, 1, 1, false, false, addCardUpgradingInfo);
            }
            yield break;
        }

        public IEnumerator ConsumeHand(CardPlayedRelicEffectParams relicEffectParams) 
        {
            this.numCopies = 0;

            List<CardState> hand = relicEffectParams.cardManager.GetHand(true);
            if (relicEffectParams.cardState != null)
            {
                hand.Remove(relicEffectParams.cardState);
            }
            float effectDelay = 0f;
            CardManager.DiscardCardParams discardCardParams = new CardManager.DiscardCardParams();

            foreach (CardState cardToDiscard in hand)
            {
                if (cardToDiscard.GetCardType() == CardType.Spell && cardToDiscard.GetCardDataID() != relicEffectParams.cardState.GetCardDataID())
                {
                    yield return CoreUtil.WaitForSecondsOrBreak(effectDelay);

                    relicEffectParams.cardManager.MoveToStandByPile(cardToDiscard, false, true, new RemoveFromStandByCondition(() => CardPile.ExhaustedPile, null), discardCardParams, HandUI.DiscardEffect.Exhausted);
                    this.numCopies++;

                    effectDelay += ProviderManager.SaveManager.GetAllGameData().GetBalanceData().GetAnimationTimingData().handDiscardAnimationDelay;
                    //cardToDiscard = null;
                }
            }

            //List<CardState>.Enumerator enumerator = default(List<CardState>.Enumerator);
            yield break;
            //yield break;
        }

        // Token: 0x04000C09 RID: 3081
        private CardType cardType;

        // Token: 0x04000C0A RID: 3082
        private List<string> requiredTraits;

        // Token: 0x04000C0B RID: 3083
        private int numCopies;
    }

}