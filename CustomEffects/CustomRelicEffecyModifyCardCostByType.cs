using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomEffects
{

	// Token: 0x0200027F RID: 639
	public sealed class CustomRelicEffecyModifyCardCostByType : RelicEffectBase, IOnTurnStartRelicEffect, IRelicEffect, ICardPlayedRelicEffect, IOnDiscardNoConditionsRelicEffect, IOnCardAddedToHandRelicEffect
	{
		// Token: 0x0600163D RID: 5693 RVA: 0x00058580 File Offset: 0x00056780
		public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
		{
			base.Initialize(relicState, relicData, relicEffectData);
			this.upgradeState.Setup(relicEffectData.GetParamCardUpgradeData(), false);
			cardType = relicEffectData.GetParamCardType();
		}

		public bool TestCardPlayed(CardPlayedRelicEffectParams relicEffectParams) 
		{
			return this.upgradedCards.Contains(relicEffectParams.cardState);
		}

		public IEnumerator OnCardPlayed(CardPlayedRelicEffectParams relicEffectParams)
		{
            if (this.triggered) 
			{
				yield break;
			}

			relicEffectParams.cardState.GetTemporaryCardStateModifiers().RemoveUpgrade(this.upgradeState);
			this.upgradedCards.Remove(relicEffectParams.cardState);

			foreach (CardState cardState in this.upgradedCards)
			{
				cardState.GetTemporaryCardStateModifiers().RemoveUpgrade(this.upgradeState);
			}
			this.upgradedCards.Clear();

			this.triggered = true;
			base.NotifyRelicTriggered(relicEffectParams.relicManager);

			yield break;
		}

		public bool TestOnCardDiscarded(CardDiscardedRelicEffectParams relicEffectParams)
		{
			return this.upgradedCards.Contains(relicEffectParams.discardCardParams.discardCard);
			//return true;
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00058C0A File Offset: 0x00056E0A
		public IEnumerator OnCardDiscarded(CardDiscardedRelicEffectParams relicEffectParams)
		{
			relicEffectParams.discardCardParams.discardCard.GetTemporaryCardStateModifiers().RemoveUpgrade(this.upgradeState);
			if (this.upgradedCards.Contains(relicEffectParams.discardCardParams.discardCard))
			{
				this.upgradedCards.Remove(relicEffectParams.discardCardParams.discardCard);
			}
			yield break;
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x000585D2 File Offset: 0x000567D2
		public IEnumerator ApplyEffect(RelicEffectParams relicEffectParams)
		{
			if (!this.triggered && this.upgradedCards.Count > 0)
			{
				foreach (CardState cardState in this.upgradedCards)
				{
					cardState.GetTemporaryCardStateModifiers().RemoveUpgrade(this.upgradeState);
				}
			}

			this.upgradedCards.Clear();
			this.triggered = false;

			foreach (CardState cardState in relicEffectParams.cardManager.GetHand(false))
			{
				if (this.cardType == CardType.Invalid || cardState.GetCardType() == this.cardType)
				{
					cardState.GetTemporaryCardStateModifiers().AddUpgrade(this.upgradeState, null);
					this.upgradedCards.Add(cardState);
				}
			}

			yield break;
		}

		public bool OnCardAdded(CardAddedToHandRelicEffectParams relicEffectParams)
		{
			//relicEffectParams.cardState.GetTemporaryCardStateModifiers().RemoveUpgrade(this.upgradeState);

			if (this.triggered) { return true; }

			CardState cardState = relicEffectParams.cardState;

			if (this.cardType == CardType.Invalid || cardState.GetCardType() == this.cardType)
			{
				cardState.GetTemporaryCardStateModifiers().AddUpgrade(this.upgradeState, null);
				this.upgradedCards.Add(cardState);
			}			
			return false;
		}

		// Token: 0x04000BCF RID: 3023
		public CardUpgradeState upgradeState = new CardUpgradeState();

		// Token: 0x04000BD0 RID: 3024
		private CardType cardType = CardType.Invalid;

		// Token: 0x04000BD2 RID: 3026
		public List<CardState> upgradedCards = new List<CardState>();

		private bool triggered = false;
	}
}