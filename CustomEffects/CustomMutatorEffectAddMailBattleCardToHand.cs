using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Equestrian.Mutators;
using Trainworks.Managers;
using Equestrian.Init;
using ShinyShoe.Loading;
using Equestrian.HarmonyPatches;

// Token: 0x0200026B RID: 619

namespace CustomEffects
{
	public sealed class CustomMutatorEffectAddMailBattleCardToHand : RelicEffectBase, IStartOfPlayerTurnAfterDrawRelicEffect, ITurnTimingRelicEffect, IRelicEffect
	{
		// Token: 0x060015CE RID: 5582 RVA: 0x000574B4 File Offset: 0x000556B4
		public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
		{
			base.Initialize(relicState, relicData, relicEffectData);
			this.cardUpgradeData = relicEffectData.GetParamCardUpgradeData();
			YouveGotMail.SetupMail(ProviderManager.SaveManager);
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x000574CB File Offset: 0x000556CB
		public bool TestEffect(RelicEffectParams relicEffectParams)
		{
			this.cardToAdd = null;
			if (YouveGotMail.mailSpells.Count > 0)
			{
				YouveGotMail.mailSpells.Shuffle<CardData>(RngId.Battle);
				this.cardToAdd = YouveGotMail.mailSpells[0];
			}
			return true;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x000574F5 File Offset: 0x000556F5
		public IEnumerator ApplyEffect(RelicEffectParams relicEffectParams)
		{
			//Testing shows that Antumbra Assault and Plink both fail to pre-load morsel card and character art.
			//There are no issues with any of the other 'container' spells that generate cards.
			//pre-loading "Packed Morsels" if the picked card is "Antumbra Assult" should fix this.
			//Plink is a starter card and not part of the MegaPool so it will never be chosen.
			//this.cardToAdd = YouveGotMail.GetTestCard();

			if (this.cardToAdd)
			{ 
				FixArt.TryYetAnotherFix(new List<CardData> { this.cardToAdd }, LoadingScreen.DisplayStyle.Background);
			}

			if (this.cardToAdd.GetID() == "fe73da55-8250-4f96-b3e0-91c19b3e60de") 
			{
				//Also load Packed morsels if Antumbra Assault is chosen
				FixArt.TryYetAnotherFix(new List<CardData> { CustomCardManager.GetCardDataByID("fab34a33-f0c3-41b2-8e7a-d099bb870fc4") }, LoadingScreen.DisplayStyle.Background);
			}

			if (LoadingScreen.HasTask<LoadAdditionalCards>(true))
			{
				//Ponies.Log("Mail delayed due to cards still loading.");

				int ii = 0;
				int loads = FixArt.cardsToLoad;

                do 
				{
					ii++;

					if (loads > FixArt.cardsToLoad)
					{
						ii = 0;
						loads = FixArt.cardsToLoad;
					}

					if (ii > 100 || FixArt.cardsToLoad == 0)
					{
						Ponies.Log("ii: " + ii);
						Ponies.Log("Card to Load: " + FixArt.cardsToLoad);
						break;
					}

					yield return new WaitForSeconds(0.1f);
				}
				while (LoadingScreen.HasTask<LoadAdditionalCards>(true));

				//Ponies.Log("Loading complete");
			}

			if (this.cardToAdd)
			{
				CardManager cardManager = relicEffectParams.cardManager;
				bool pileUpdated = false;
				cardManager.cardPilesChangedSignal.AddOnce(delegate (CardManager.CardPileInformation _)
				{
					pileUpdated = true;
				});
				CardManager.AddCardUpgradingInfo addCardUpgradingInfo = new CardManager.AddCardUpgradingInfo();
				if (this.cardUpgradeData != null)
				{
					addCardUpgradingInfo.upgradeDatas.Add(this.cardUpgradeData);
				}
				addCardUpgradingInfo.tempCardUpgrade = true;
				addCardUpgradingInfo.upgradingCardSource = null;
				if (cardManager.AddCard(this.cardToAdd, CardPile.HandPile, 1, 1, true, false, addCardUpgradingInfo) == null)
				{
					pileUpdated = true;
				}
				yield return new WaitUntil(() => pileUpdated);
			}
			yield break;
		}

		// Token: 0x04000B87 RID: 2951
		private CardData cardToAdd;

		// Token: 0x04000B88 RID: 2952
		private CardUpgradeData cardUpgradeData;
	}
}