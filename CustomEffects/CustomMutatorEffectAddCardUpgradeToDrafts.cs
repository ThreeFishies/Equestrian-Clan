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
	public sealed class CustomMutatorEffectAddUpgradeToCardDrafts : RelicEffectBase, IAddStartingUpgradeToCardDrafts, IRelicEffect
	{
		// Token: 0x06001551 RID: 5457 RVA: 0x000561F4 File Offset: 0x000543F4
		public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
		{
			base.Initialize(relicState, relicData, relicEffectData);
			this.upgradeData = relicEffectData.GetParamCardUpgradeData();
		}

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