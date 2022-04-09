using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomEffects
{

	// Token: 0x0200008A RID: 138
	public sealed class CustomCardEffectAddTempCardUpgradeToCardsInDeck : CardEffectBase, ICardEffectTipTooltip
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0000C623 File Offset: 0x0000A823
		public override bool CanPlayAfterBossDead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0000C623 File Offset: 0x0000A823
		public override bool CanApplyInPreviewMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00020E99 File Offset: 0x0001F099
		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			//Note: adding cards from your deck and discard pile to your handpile can break the game if the added cards are not removed later.
			//Yes, the list passed is your actual hand and not just a reference to it. If another effect later asks for your handpile, any changes you make here will also be passed.
			//That's bad, because if a card is not, acutally, in your hand, then effects that reference them can throw an index out-of-bounds error.
			//That softlocks the game.
			CardUpgradeState cardUpgradeState = new CardUpgradeState();
			cardUpgradeState.Setup(cardEffectState.GetParamCardUpgradeData(), false);
			List<CardState> unplayedCards = cardEffectParams.cardManager.GetHand(false);
			List<CardState> notHandCards = new List<CardState> { };
			int handSize = unplayedCards.Count;
			int ii = 0;
			foreach (CardState drawCard in cardEffectParams.cardManager.GetDrawPile(false)) 
			{ 
				unplayedCards.Add(drawCard);
			}
			foreach (CardState discardCard in cardEffectParams.cardManager.GetDiscardPile(false))
			{
				unplayedCards.Add(discardCard);
			}

			if (unplayedCards.Count == 0) { yield break; }

			using (List<CardState>.Enumerator enumerator = unplayedCards.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					CardState cardState = enumerator.Current;
					bool flag = false;
					using (List<CardUpgradeMaskData>.Enumerator enumerator2 = cardUpgradeState.GetFilters().GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (!enumerator2.Current.FilterCard<CardState>(cardState, cardEffectParams.relicManager))
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						bool flag2 = true;
						foreach (CardTraitState cardTraitState in cardEffectState.GetParentCardState().GetTraitStates())
						{
							flag2 = (flag2 && cardTraitState.OnCardBeingUpgraded(cardState, cardEffectState.GetParentCardState(), cardEffectParams.cardManager, cardUpgradeState));
						}
						if (flag2)
						{
							CardUpgradeState cardUpgradeState2 = new CardUpgradeState();
							cardUpgradeState2.Setup(cardUpgradeState);
							cardUpgradeState2.SetAttackDamage(cardUpgradeState2.GetAttackDamage() * cardState.GetMagicPowerMultiplierFromTraits());
							cardUpgradeState2.SetAdditionalHeal(cardUpgradeState2.GetAdditionalHeal() * cardState.GetMagicPowerMultiplierFromTraits());
							cardState.GetTemporaryCardStateModifiers().AddUpgrade(cardUpgradeState2, null);
							cardState.UpdateCardBodyText(null);
							CardManager cardManager = cardEffectParams.cardManager;
							if (ii < handSize)
							{
								if (cardManager != null)
								{
									cardManager.RefreshCardInHand(cardState, false);
								}
								CardUI cardInHand = cardEffectParams.cardManager.GetCardInHand(cardState);
								if (cardInHand != null)
								{
									cardInHand.ShowEnhanceFX();
								}
							}
						}
					}

					if (!(ii < handSize))
					{
						notHandCards.Add(cardState);
					}
					ii++;
				}
			}

			if (notHandCards.Count > 0) 
			{ 
				foreach (CardState notHandCard in notHandCards) 
				{ 
					unplayedCards.Remove(notHandCard);
				}
			}
			yield break;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00020EAF File Offset: 0x0001F0AF
		public override void GetTooltipsStatusList(CardEffectState cardEffectState, ref List<string> outStatusIdList)
		{
			CardEffectAddTempCardUpgradeToCardsInHand.GetTooltipsStatusList(cardEffectState.GetSourceCardEffectData(), ref outStatusIdList);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00020EC0 File Offset: 0x0001F0C0
		public static void GetTooltipsStatusList(CardEffectData cardEffectData, ref List<string> outStatusIdList)
		{
			foreach (StatusEffectStackData statusEffectStackData in cardEffectData.GetParamCardUpgradeData().GetStatusEffectUpgrades())
			{
				outStatusIdList.Add(statusEffectStackData.statusId);
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00020F20 File Offset: 0x0001F120
		public string GetTipTooltipKey(CardEffectState cardEffectState)
		{
			if (cardEffectState.GetParamCardUpgradeData() != null && cardEffectState.GetParamCardUpgradeData().HasUnitStatUpgrade())
			{
				return "TipTooltip_StatChangesStick";
			}
			return null;
		}
	}
}