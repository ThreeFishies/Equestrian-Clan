using System;
using System.Collections;
using Equestrian.MonsterCards;
using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using Trainworks.Managers;
using System.Text;
using UnityEngine;
using StateMechanic;
using Trainworks.AssetConstructors;
using Trainworks.Builders;
using System.Runtime.CompilerServices;
using UnityEngine.AddressableAssets;
using System.Text.RegularExpressions;
using Trainworks.Interfaces;
using Trainworks.Constants;
using Equestrian.SpellCards;
using Equestrian.HarmonyPatches;
//using SynthesisFix;
using ShinyShoe;
using Equestrian;
using Equestrian.Champions;
using Equestrian.Init;

//The base function (CardEffectAddTempUpgradeToCardsInHand) softlocks the game when triggered by Mare a Lee. Therefore, copy the original code from the decompiler and mess with it until it maybe works?
//Found it. The function referenced the parent card, which didn't exist because the code was triggered by a character.
namespace CustomEffects
{
	// Token: 0x0200008A RID: 138
	//Tweak the name to clarify use
	public sealed class CharacterEffectAddTempCardUpgradeToCardsInHand: CardEffectBase, ICardEffectTipTooltip
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
			//Ponies.Log("Tutor I Activated.");
			//Ponies.Log("Bonus HP: " + cardEffectState.GetParamCardUpgradeData().GetBonusHP().ToString());
			//Ponies.Log("Bonus Damage: " + cardEffectState.GetParamCardUpgradeData().GetBonusDamage().ToString());
			//for (int ii = 0; ii < cardEffectState.GetParamCardUpgradeData().GetTraitDataUpgrades().Count; ii++) 
			//{
			//	Ponies.Log($"Bonus Trait[{ii}]: " + cardEffectState.GetParamCardUpgradeData().GetTraitDataUpgrades()[ii].GetTraitStateName());
			//}
			//for (int jj = 0; jj < cardEffectState.GetParamCardUpgradeData().GetFilters().Count; jj++) 
			//{
			//	Ponies.Log($"Filter[{jj}]: " + cardEffectState.GetParamCardUpgradeData().GetFilters()[jj].GetType());
			//}

			CardUpgradeState cardUpgradeState = new CardUpgradeState();
			cardUpgradeState.Setup(cardEffectState.GetParamCardUpgradeData(), false);
			using (List<CardState>.Enumerator enumerator = cardEffectParams.cardManager.GetHand(false).GetEnumerator())
			{
				//Ponies.Log("Line 71.");

				while (enumerator.MoveNext())
				{
					CardState cardState = enumerator.Current;
					bool flag = false;

					//Ponies.Log("Line 78.");

					using (List<CardUpgradeMaskData>.Enumerator enumerator2 = cardUpgradeState.GetFilters().GetEnumerator())
					{
						//Ponies.Log("Line 82.");

						while (enumerator2.MoveNext())
						{
							//Ponies.Log("Line 86.");

							if (!enumerator2.Current.FilterCard<CardState>(cardState, cardEffectParams.relicManager))
							{
								//Ponies.Log("Line 90.");

								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						//Ponies.Log("Line 99.");

						bool flag2 = true;
						//The error occurs here because the trigger is invoked from a unit and not a parent card
						//foreach (CardTraitState cardTraitState in cardEffectState.GetParentCardState().GetTraitStates())
						//{
						//	Ponies.Log("Line 104.");
						//
						//	flag2 = (flag2 && cardTraitState.OnCardBeingUpgraded(cardState, cardEffectState.GetParentCardState(), cardEffectParams.cardManager, cardUpgradeState));
						//}
						if (flag2)
						{
							//Ponies.Log("Line 110.");

							CardUpgradeState cardUpgradeState2 = new CardUpgradeState();
							cardUpgradeState2.Setup(cardUpgradeState);
							cardUpgradeState2.SetAttackDamage(cardUpgradeState2.GetAttackDamage() * cardState.GetMagicPowerMultiplierFromTraits());
							cardUpgradeState2.SetAdditionalHeal(cardUpgradeState2.GetAdditionalHeal() * cardState.GetMagicPowerMultiplierFromTraits());
							cardState.GetTemporaryCardStateModifiers().AddUpgrade(cardUpgradeState2, null);
							cardState.UpdateCardBodyText(null);
							CardManager cardManager = cardEffectParams.cardManager;
							//Ponies.Log("Line 119.");

							if (cardManager != null)
							{
								//Ponies.Log("Line 123.");

								cardManager.RefreshCardInHand(cardState, false);
							}
							//Ponies.Log("Line 127.");

							CardUI cardInHand = cardEffectParams.cardManager.GetCardInHand(cardState);
							if (cardInHand != null)
							{
								//Ponies.Log("Line 132.");

								cardInHand.ShowEnhanceFX();
							}
						}
					}
				}
				//Ponies.Log("Line 139.");

				//yield break;
			}
			//Ponies.Log("Line 143.");

			yield break;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00020EAF File Offset: 0x0001F0AF
		public override void GetTooltipsStatusList(CardEffectState cardEffectState, ref List<string> outStatusIdList)
		{
			//Ponies.Log("Line 151.");

			CardEffectAddTempCardUpgradeToCardsInHand.GetTooltipsStatusList(cardEffectState.GetSourceCardEffectData(), ref outStatusIdList);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00020EC0 File Offset: 0x0001F0C0
		public static void GetTooltipsStatusList(CardEffectData cardEffectData, ref List<string> outStatusIdList)
		{
			//Ponies.Log("Line 159.");

			foreach (StatusEffectStackData statusEffectStackData in cardEffectData.GetParamCardUpgradeData().GetStatusEffectUpgrades())
			{
				//Ponies.Log("Line 163.");

				outStatusIdList.Add(statusEffectStackData.statusId);
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00020F20 File Offset: 0x0001F120
		public string GetTipTooltipKey(CardEffectState cardEffectState)
		{
			//Ponies.Log("Line 172.");

			if (cardEffectState.GetParamCardUpgradeData() != null && cardEffectState.GetParamCardUpgradeData().HasUnitStatUpgrade())
			{
				//Ponies.Log("Line 176.");

				return "TipTooltip_StatChangesStick";
			}
			//Ponies.Log("Line 180.");

			return null;
		}
	}
}