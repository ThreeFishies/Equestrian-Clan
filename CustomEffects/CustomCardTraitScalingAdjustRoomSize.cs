using System;
using System.Collections;
using Trainworks.Managers;
using Equestrian.Init;

namespace CustomEffects
{
	// Token: 0x020000DF RID: 223
	public sealed class CustomCardTraitScalingAdjustRoomSize : CardTraitState
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
				//combatManager.ModifyEnergyForNextTurn(additionalEnergy);
				RoomUI roomUI = roomManager.GetRoomUI();
				RoomState room = roomManager.GetRoom(roomManager.GetSelectedRoom());
				Team.Type team = Team.Type.Monsters;
				int adjustAmount = additionalEnergy;
				roomUI.DisableScrolling();
				yield return roomUI.SetSelectedRoom(roomManager.GetSelectedRoom(), false);
				SpawnPointGroup.CapacityAdjustmentError capacityAdjustmentError;

				//Ponies.Log("Current Room Capacity: " + room.GetCapacityInfo(team).max);

				if (room.GetCapacityInfo(team).max + adjustAmount > 30) 
				{
					adjustAmount = 30 - room.GetCapacityInfo(team).max;
				}
				if (adjustAmount < 1) 
				{
					roomUI.EnableScrolling();
					yield break;
				}
				if (!room.CanAdjustCapacity(team, ref adjustAmount, out capacityAdjustmentError))
				{
					CardEffectParams cardEffectParams = new CardEffectParams();
					cardEffectParams.popupNotificationManager.ShowNotification(new PopupNotificationUI.NotificationData
					{
						text = string.Format("CardEffectAdjustRoomCapacity_{0}", capacityAdjustmentError).Localize(null),
						colorType = ColorDisplayData.ColorType.Negative,
						source = PopupNotificationUI.Source.Room
					}, room);
					roomUI.EnableScrolling();
					yield break;
				}
				yield return room.AdjustCapacity(Team.Type.Monsters, adjustAmount, true);
				yield return CoreUtil.WaitForSecondsOrBreak(saveManager.GetActiveTiming().CharacterTriggerPostFire);
				roomUI.EnableScrolling();
				yield break;
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
				return string.Format("CardTraitScalingUpgradeUnitSize_CurrentScaling_CardText".Localize(null), this.GetAdditionalEnergy(cardStatistics, true));
			}
			return string.Empty;
		}
	}
}