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

namespace CustomEffects
{
	// Token: 0x02000256 RID: 598
	public sealed class CustomMutatorEffectAddUpgradeToAllCards : RelicEffectBase, IAddStartingUpgradeToCardDrafts, IRelicEffect, ICardModifierRelicEffect
	{
		// Token: 0x06001551 RID: 5457 RVA: 0x000561F4 File Offset: 0x000543F4
		public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
		{
			base.Initialize(relicState, relicData, relicEffectData);
			this.upgradeData = relicEffectData.GetParamCardUpgradeData();
		}

		public bool ApplyCardStateModifiers(CardState cardState, SaveManager saveManager, CardManager cardManager, RelicManager relicManager) 
		{
			if (upgradeData == null)
			{
				return false;
			}
			CardUpgradeState cardUpgradeState = Activator.CreateInstance<CardUpgradeState>();
			cardUpgradeState.Setup(upgradeData, false);

			using (List<CardUpgradeMaskData>.Enumerator enumerator2 = upgradeData.GetFilters().GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (!enumerator2.Current.FilterCard<CardState>(cardState, relicManager))
					{
						return false;
					}
				}
			}

			cardState.Upgrade(cardUpgradeState, saveManager, true);
			return true;
		}

		public bool GetTooltip(out string title, out string body) 
		{
			title = string.Empty;
			body = string.Empty;
			return false;
		}

		/*
		public void ApplyEffect(RelicEffectParams relicEffectParams) 
		{
			List<CardState> deck = relicEffectParams.cardManager.GetAllCards();
			if (upgradeData == null)
			{
				return;
			}
			CardUpgradeState cardUpgradeState = Activator.CreateInstance<CardUpgradeState>();
			cardUpgradeState.Setup(upgradeData, false);

			foreach (CardState card in deck)
			{
				bool flag = true;

				using (List<CardUpgradeMaskData>.Enumerator enumerator2 = upgradeData.GetFilters().GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (!enumerator2.Current.FilterCard<CardState>(card, relicEffectParams.relicManager))
						{
							flag = false;
						}
					}
				}

				if (flag)
				{
					card.Upgrade(cardUpgradeState, relicEffectParams.saveManager, true);
				}
			}
		}
		*/

		// Token: 0x06001552 RID: 5458 RVA: 0x0005620C File Offset: 0x0005440C
		public void UpgradeCard(CardDrawnRelicEffectParams relicEffectParams)
		{
			CardState cardState = relicEffectParams.cardState;

			if (upgradeData != null)
			{
				using (List<CardUpgradeMaskData>.Enumerator enumerator2 = upgradeData.GetFilters().GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (!enumerator2.Current.FilterCard<CardState>(cardState, relicEffectParams.relicManager))
						{
							return;
						}
					}
				}
			}
			
			if (upgradeData == null)
			{
				return;
			}
			CardUpgradeState cardUpgradeState = Activator.CreateInstance<CardUpgradeState>();
			cardUpgradeState.Setup(upgradeData, false);
			relicEffectParams.cardState.Upgrade(cardUpgradeState, relicEffectParams.saveManager, true);
		}

		// Token: 0x04000B5E RID: 2910
		//private EnhancerPool enhancerPool;

		public CardUpgradeData upgradeData;
	}
}