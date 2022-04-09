using System;
using System.Collections;

namespace CustomEffects
{
	// Token: 0x020000DF RID: 223
	public sealed class CustomCardTraitScalingAddEnergyNextTurn : CardTraitState
	{
		// Token: 0x0600085E RID: 2142 RVA: 0x00024166 File Offset: 0x00022366
		public override IEnumerator OnCardDiscarded(CardManager.DiscardCardParams discardCardParams, CardManager cardManager, RelicManager relicManager, CombatManager combatManager, RoomManager roomManager, SaveManager saveManager)
		{
			if (!discardCardParams.wasPlayed)
			{
				yield break;
			}
			int additionalEnergy = this.GetAdditionalEnergy(cardManager.GetCardStatistics(), false);
			if (additionalEnergy > 0)
			{
				//cardManager.GetPlayerManager().AddEnergy(additionalEnergy);
				combatManager.ModifyEnergyForNextTurn(additionalEnergy);
			}
			yield break;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00024184 File Offset: 0x00022384
		private int GetAdditionalEnergy(CardStatistics cardStatistics, bool forPreviewText)
		{
			CardStatistics.StatValueData statValueData = base.StatValueData;
			statValueData.forPreviewText = forPreviewText;
			int statValue = cardStatistics.GetStatValue(statValueData);
			return base.GetParamInt() * statValue;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001C727 File Offset: 0x0001A927
		public override bool HasMultiWordDesc()
		{
			return true;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000241B0 File Offset: 0x000223B0
		public override string GetCurrentEffectText(CardStatistics cardStatistics, SaveManager saveManager, RelicManager relicManager)
		{
			if (cardStatistics != null && cardStatistics.GetStatValueShouldDisplayOnCardNow(base.StatValueData))
			{
				return string.Format("CardTraitScalingAddEnergy_CurrentScaling_CardText".Localize(null), this.GetAdditionalEnergy(cardStatistics, true));
			}
			return string.Empty;
		}
	}
}