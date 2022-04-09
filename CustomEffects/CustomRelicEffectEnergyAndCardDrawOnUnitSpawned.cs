using System;
using System.Collections;
using Equestrian.Init;

namespace CustomEffects
{
	// Token: 0x020002B3 RID: 691
	public sealed class CustomRelicEffectEnergyAndCardDrawOnUnitSpawned : RelicEffectBase
	{
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x0000C623 File Offset: 0x0000A823
		public override bool CanApplyInPreviewMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0005BE4B File Offset: 0x0005A04B
		public override string GetActivatedDescription()
		{
			return string.Format(base.GetActivatedDescription(), this.energy);
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0005BE63 File Offset: 0x0005A063
		public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
		{
			base.Initialize(relicState, relicData, relicEffectData);
			this.energy = relicEffectData.GetParamInt();
			this.teamFilter = relicEffectData.GetParamSourceTeam();
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0005BE86 File Offset: 0x0005A086
		public override IEnumerator OnCharacterAdded(CharacterState character, CardState fromCard, RelicManager relicManager, SaveManager saveManager, PlayerManager playerManager, RoomManager roomManager, CombatManager combatManager, CardManager cardManager)
		{
			bool isChampion = false;
			foreach (SubtypeData subtype in character.GetSubtypes())
			{
				isChampion |= subtype.IsChampion;
			}
			if (!isChampion) 
			{
				yield break;
			}

			if (saveManager.PreviewMode)
			{
				yield break;
			}
			if (this.teamFilter == Team.Type.None || character.GetTeamType() == this.teamFilter)
			{
				base.NotifyRoomRelicTriggered(new RelicEffectParams
				{
					relicManager = relicManager,
					playerManager = playerManager,
					roomManager = roomManager,
					saveManager = saveManager
				});
				playerManager.AddEnergy(this.energy);

				//Draw three cards, and filter by type monster.
				cardManager.DrawCards(3, null, CardType.Monster);
				yield break;
			}
			yield break;
		}

		// Token: 0x04000C6D RID: 3181
		private int energy;

		// Token: 0x04000C6E RID: 3182
		private Team.Type teamFilter;
	}
}