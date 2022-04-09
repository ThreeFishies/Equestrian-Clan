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
	public sealed class CustomRelicEffectAddStartingUpgradeToCardDrafts : RelicEffectBase, IAddStartingUpgradeToCardDrafts, IRelicEffect
	{
		// Token: 0x06001551 RID: 5457 RVA: 0x000561F4 File Offset: 0x000543F4
		public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
		{
			base.Initialize(relicState, relicData, relicEffectData);
			this.largeStoneData = ProviderManager.SaveManager.GetAllGameData().FindEnhancerData(VanillaUnitEnhancers.Largestone);
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0005620C File Offset: 0x0005440C
		public void UpgradeCard(CardDrawnRelicEffectParams relicEffectParams)
		{
			CardState cardState = relicEffectParams.cardState;
			List<EnhancerData> allChoices = new List<EnhancerData> { largeStoneData };
			for (int i = allChoices.Count - 1; i >= 0; i--)
			{
				foreach (RelicEffectData relicEffectData in allChoices[i].GetEffects())
				{
					if (relicEffectData.GetParamCardType() != relicEffectParams.cardState.GetCardType())
					{
						allChoices.RemoveAt(i);
						break;
					}
					CardUpgradeData paramCardUpgradeData = relicEffectData.GetParamCardUpgradeData();
					if (paramCardUpgradeData != null)
					{
						using (List<CardUpgradeMaskData>.Enumerator enumerator2 = paramCardUpgradeData.GetFilters().GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								if (!enumerator2.Current.FilterCard<CardState>(cardState, relicEffectParams.relicManager))
								{
									allChoices.RemoveAt(i);
									break;
								}
							}
						}
					}
				}
			}
			if (allChoices.Count <= 0)
			{
				return;
			}
			CardUpgradeData paramCardUpgradeData2 = allChoices.RandomElement(RngId.Rewards).GetEffects()[0].GetParamCardUpgradeData();
			CardUpgradeState cardUpgradeState = Activator.CreateInstance<CardUpgradeState>();
			cardUpgradeState.Setup(paramCardUpgradeData2, false);
			relicEffectParams.cardState.Upgrade(cardUpgradeState, relicEffectParams.saveManager, true);
		}

		// Token: 0x04000B5E RID: 2910
		//private EnhancerPool enhancerPool;

		public EnhancerData largeStoneData;
	}
}