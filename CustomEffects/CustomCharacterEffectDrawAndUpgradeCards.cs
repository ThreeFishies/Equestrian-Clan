using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;

namespace CustomEffects
{
	// Token: 0x020000A3 RID: 163
	public sealed class CustomCharacterEffectDrawAndUpgradeCards : CardEffectBase
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x0000C623 File Offset: 0x0000A823
		public override bool CanPlayAfterBossDead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0000C623 File Offset: 0x0000A823
		public override bool CanApplyInPreviewMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x0001C727 File Offset: 0x0001A927
		public override bool IsTriggerStackable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00021F28 File Offset: 0x00020128
		private bool CanDrawToMaxStartingHandCards(CardEffectParams cardEffectParams, CustomCharacterEffectDrawAndUpgradeCards.DrawMode drawMode, out int numCardsToDraw)
		{
			int num = cardEffectParams.cardManager.GetNumCardsInHand() - 1;
			int maxHandSize = cardEffectParams.cardManager.GetMaxHandSize();
			int startingHandSize = cardEffectParams.cardManager.GetStartingHandSize();
			numCardsToDraw = Mathf.Max(maxHandSize - num, 0);
			return numCardsToDraw > 0;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00021F6F File Offset: 0x0002016F
		private CustomCharacterEffectDrawAndUpgradeCards.DrawMode GetDrawMode(int cards)
		{
			if (cards != -1)
			{
				return CustomCharacterEffectDrawAndUpgradeCards.DrawMode.MaxHand;
			}
			return CustomCharacterEffectDrawAndUpgradeCards.DrawMode.StartingHand;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00021F78 File Offset: 0x00020178
		public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			CustomCharacterEffectDrawAndUpgradeCards.DrawMode drawMode = this.GetDrawMode(cardEffectState.GetIntInRange());
			int num;
			return this.CanDrawToMaxStartingHandCards(cardEffectParams, drawMode, out num);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00021F9C File Offset: 0x0002019C
		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			int num = cardEffectState.GetIntInRange();
			if (num == -1)
			{
				CustomCharacterEffectDrawAndUpgradeCards.DrawMode drawMode = this.GetDrawMode(num);
				this.CanDrawToMaxStartingHandCards(cardEffectParams, drawMode, out num);
			}
			else
			{
				for (int i = 1; i < cardEffectParams.fireCount; i++)
				{
					num += cardEffectState.GetIntInRange();
				}
			}

			CardUpgradeState cardUpgradeState = new CardUpgradeState();
			cardUpgradeState.Setup(cardEffectState.GetParamCardUpgradeData(), false);

			cardEffectParams.cardManager.AddTempCardUpgradeToNextDrawnCard(cardUpgradeState);
			cardEffectParams.cardManager.DrawCards(num, cardEffectParams.playedCard, CardType.Invalid);

			List<CardUpgradeState> tempUpgradesOnNextDraw = (List<CardUpgradeState>)AccessTools.Field(typeof(CardManager), "nextDrawnTempCardUpgrades").GetValue(cardEffectParams.cardManager);
			tempUpgradesOnNextDraw.Clear();

			yield break;
		}

		// Token: 0x0400044D RID: 1101
		private const int DRAW_TO_MAX_STARTING_HAND = -1;

		// Token: 0x02000BFD RID: 3069
		private enum DrawMode : byte
		{
			// Token: 0x04003F91 RID: 16273
			StartingHand,
			// Token: 0x04003F92 RID: 16274
			MaxHand
		}
	}
}