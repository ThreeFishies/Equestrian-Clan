using System;
using System.Collections;
using System.Collections.Generic;
using Trainworks.Managers;
using Equestrian.Relic;

// Token: 0x0200029C RID: 668
namespace CustomEffects
{
	public sealed class CustomRelicEffectCopyCardOnCardPlay : RelicEffectBase, ICardPlayedRelicEffect, IRelicEffect
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
			CardData cardData = relicEffectParams.saveManager.GetAllGameData().FindCardData(relicEffectParams.cardState.GetCardDataID());
			CardManager.AddCardUpgradingInfo addCardUpgradingInfo = new CardManager.AddCardUpgradingInfo();
			addCardUpgradingInfo.tempCardUpgrade = true;
			addCardUpgradingInfo.upgradingCardSource = relicEffectParams.cardState;
			addCardUpgradingInfo.ignoreTempUpgrades = false;
			addCardUpgradingInfo.copyModifiersFromCard = relicEffectParams.cardState;
			//List <CardState> NewCards = new List<CardState> { };

			for (int i = 0; i < this.numCopies; i++)
			{
				//Copy everything to change "HandPile" to "DiscardPile" in one line. Very efficient.
				
				CardState copiedCard = relicEffectParams.cardManager.AddCard(cardData, CardPile.DiscardPile, 1, 1, false, false, addCardUpgradingInfo);

				//CardManager.DiscardCardParams discardCard = new CardManager.DiscardCardParams()
				//{
				//	discardCard = copiedCard,
				//	handDiscarded = false,
				//	wasPlayed = false,
				//	triggeredByCard = false
				//};
				//
				//relicEffectParams.relicManager.ApplyOnDiscardRelicEffects(discardCard);
			}

			//Do a sanity check on the Royal Scroll
			/*
			if (relicEffectParams.saveManager.GetHasRelic(CustomCollectableRelicManager.GetRelicDataByID(RoyalScroll.ID))) 
			{ 
				RelicData royalScrll = CustomCollectableRelicManager.GetRelicDataByID(RoyalScroll.ID);
				foreach (CardState copiedCard in NewCards) 
				{ 
					if (copiedCard.GetTemporaryCardStateModifiers() != null)
					{
						CardManager.DiscardCardParams discardCardParams = new CardManager.DiscardCardParams();
						discardCardParams.discardCard = copiedCard;
						discardCardParams.wasPlayed = false;
						discardCardParams.handDiscarded = false;
						discardCardParams.triggeredByCard = false;
						relicEffectParams.relicManager.ApplyOnDiscardRelicEffects(discardCardParams);
					}
				}
			}*/

			yield break;
		}

		// Token: 0x04000C09 RID: 3081
		private CardType cardType;

		// Token: 0x04000C0A RID: 3082
		private List<string> requiredTraits;

		// Token: 0x04000C0B RID: 3083
		private int numCopies;
	}
}