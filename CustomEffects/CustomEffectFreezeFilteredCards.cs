using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomEffects
{
	public sealed class CustomEffectFreezeFilteredCards : CardEffectBase, ICardEffectStatuslessTooltip
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x0000C623 File Offset: 0x0000A823
		public override bool CanPlayAfterBossDead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0000C623 File Offset: 0x0000A823
		public override bool CanApplyInPreviewMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x000227A7 File Offset: 0x000209A7
		public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			List<CardState> hand = cardEffectParams.cardManager.GetHand(true);
			CustomEffectFreezeFilteredCards.FilterCards(hand, cardEffectState, cardEffectParams);
			return hand.Count > 0;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000227C3 File Offset: 0x000209C3
		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			List<CardState> hand = cardEffectParams.cardManager.GetHand(true);
			CustomEffectFreezeFilteredCards.FilterCards(hand, cardEffectState, cardEffectParams);
			//int index = RandomManager.Range(0, hand.Count, RngId.Battle);
			foreach (CardState cardState in hand)
			{
				CardTraitData cardTraitData = new CardTraitData();
				cardTraitData.Setup("CardTraitFreeze");
				cardEffectParams.cardManager.AddTemporaryTraitToCard(cardState, cardTraitData, true, false);
			}
			yield break;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00022724 File Offset: 0x00020924
		public string GetTooltipBaseKey(CardEffectState cardEffectState)
		{
			return "CardEffectFreezeCard";
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x000227D4 File Offset: 0x000209D4
		public static void FilterCards(List<CardState> listOfCards, CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			if (listOfCards == null || listOfCards.Count == 0)
			{
				return;
			}
			for (int i = listOfCards.Count - 1; i >= 0; i--)
			{
				bool flag = listOfCards[i].HasTrait(typeof(CardTraitFreeze));
				bool flag2 = false;
				if (cardEffectState.GetParamCardFilter().FilterCard<CardState>(listOfCards[i], cardEffectParams.relicManager))
				{
					flag2 = true;
				}
				CardType cardType = listOfCards[i].GetCardType();
				if (cardType == CardType.Blight || cardType == CardType.Junk)
				{
					flag = true;
				}
				if (flag || !flag2)
				{
					listOfCards.RemoveAt(i);
				}
			}
		}
	}
}