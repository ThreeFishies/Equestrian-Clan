using System;
using System.Collections;
using Equestrian.MonsterCards;
using Trainworks.Managers;
using Equestrian.Relic;
using Equestrian.Enhancers;

namespace CustomEffects
{
	// Token: 0x020000BA RID: 186

	//Here, I shamelessly copied "CardEffectRecruit" from the decompiler
	//Both "CardEffectRecruit" and "ParamCharacterDataPool" are missing from Trainworks, otherwise this would not be needed.
	public sealed class CardEffectRecruitBackgroundPony : CardEffectBase
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0000C623 File Offset: 0x0000A823
		public override bool CanApplyInPreviewMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x0000C623 File Offset: 0x0000A823
		public override bool CanPlayInEngineRoom
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00022CBA File Offset: 0x00020EBA
		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			RoomState roomState = cardEffectParams.GetSelectedRoom();
			RelicManager relicManager = cardEffectParams.relicManager;
			

			//Snip this line from the souce code and replace it with one that specifies a background pony.
			//CharacterData monsterData = cardEffectState.GetParamCharacterDataPool().RandomElement(RngId.Battle);
			
			CharacterData monsterData = CustomCharacterManager.GetCharacterDataByID(BackgroundPony.CharID);

			CharacterState newMonster = null;

			yield return cardEffectParams.monsterManager.CreateMonsterState(monsterData, null, cardEffectParams.selectedRoom, delegate (CharacterState character)
			{
				newMonster = character;

			}, SpawnMode.SelectedSlot, roomState.GetMonsterPoint(0), null, false, null, null, true); //swap the 'recruited' flag to prevent character art from being flipped and set 'cardless' to 'true'.

			//Hopefully apply relic effects
			relicManager.CharacterAdded(newMonster, cardEffectState.GetParentCardState());
			
			//Imaginary Friends needs a nudge.
			//swapped effects: "Bottled Cutie Mark" now does this.
			if (cardEffectParams.saveManager.GetHasRelic(CustomCollectableRelicManager.GetRelicDataByID(BottledCutieMark.ID)))
			{
				//Equestrian.Init.Ponies.Log("Adding Imaginary Friends to spawned Background Pony.");
				CardUpgradeData cardUpgradeData = BottledCutieMark.BottledCutieMarkUpgrade;
				CardUpgradeState cardUpgradeState = new CardUpgradeState();
				cardUpgradeState.Setup(cardUpgradeData);
				yield return newMonster.ApplyCardUpgrade(cardUpgradeState);
				yield return newMonster.FireTriggers(CharacterTriggerData.Trigger.OnUnscaledSpawn);
			}

			if (!cardEffectParams.saveManager.PreviewMode)
			{
				yield return cardEffectParams.roomManager.GetRoomUI().CenterCharacters(roomState, true, false, true);
			}
			yield break;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00022CD0 File Offset: 0x00020ED0
		public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			RoomState selectedRoom = cardEffectParams.GetSelectedRoom();
			return !selectedRoom.GetIsPyreRoom() && selectedRoom.GetFirstEmptyMonsterPoint() != null;
		}
	}
}