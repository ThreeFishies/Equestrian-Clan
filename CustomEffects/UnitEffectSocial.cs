using System;
using System.Collections;
using System.Collections.Generic;
using Trainworks.Builders;
using System.Text;
using HarmonyLib;
using Trainworks.Managers;
using Trainworks.Constants;
using System.Linq;
using UnityEngine;
using Trainworks.Utilities;
using Equestrian.Init;
using Equestrian.MonsterCards;
using ShinyShoe.Loading;
using CustomEffects;

namespace CustomEffects
{
    //Social units will attempt to move to the floor with the highest number of other friendly units, preferring closer floors or the bottom floor if there's a choice. Full/invalid rooms are ignored, and if the middle floor is full, they won't be able to move past it.
	//Expects a target:self and OnSpawn or OnUnscaledSpawn Trigger
	public class CustomEffectSocial : CardEffectBase //, ICardEffectStatuslessTooltip
	{
		//This is what we really want to do.
		private CardEffectBump bumper;

		public override bool CanPlayAfterBossDead => false;

		public override bool CanApplyInPreviewMode => true;

		public override bool CanPlayInEngineRoom => false;

		public override bool CanRandomizeTargetMode => false;

		public override bool IsTriggerStackable => false;

		public override void Setup(CardEffectState cardEffectState)
		{
			base.Setup(cardEffectState);
			this.bumper = new CardEffectBump();
		}

		//public string GetTooltipBaseKey(CardEffectState cardEffectState)
		//{
		//	return "CardEffectSocial";
		//}
		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			//			yield break;
			//Ponies.Log("ApplyEffect() Started.");
			if(cardEffectParams.selfTarget == null || !cardEffectParams.selfTarget.IsAlive) 
			{ 
				yield break;
			}

			if(cardEffectParams.selfTarget.GetStatusEffect("social") == null)
			{
				//If the social status was removed, don't do anyhting.
				yield break;
			}

			int MaxOccupancy = cardEffectState.SaveManager.GetBalanceData().GetNumSpawnPointsPerFloor();
			int Highest_Occupancy = 0;
			int Target_Room = -1;

			int StartingFloor = cardEffectParams.selfTarget.GetCurrentRoomIndex();
			int Current_Occupancy_Excluding_Self = cardEffectParams.roomManager.GetRoom(StartingFloor).GetNumCharacters(Team.Type.Monsters) - 1;

			//Ponies.Log("Max Occupancy: " + MaxOccupancy.ToString());
			//Ponies.Log("Targeted Floor: " +StartingFloor.ToString());

			if (cardEffectParams.selfTarget.GetTeamType() != Team.Type.Monsters) 
			{
				Ponies.Log("Applying social to a non-monster?");
			}

			RoomManager rooms = cardEffectParams.roomManager;
			for (int ii = 0; ii < rooms.GetNumRooms(); ii++)
			{
				bool IsValidRoom = true;

				//Ponies.Log("Floor(ii): " +ii.ToString());

				if (rooms.GetRoom(ii) == null)
				{
					IsValidRoom = false;
				}
				else if (rooms.GetRoom(ii).IsDestroyedOrInactive())
				{
					IsValidRoom = false;
				}
				else if (!rooms.GetRoom(ii).IsRoomEnabled())
				{
					IsValidRoom = false;
				}
				else if (rooms.GetRoom(ii).GetIsPyreRoom())
				{
					IsValidRoom = false;
				}
				else if (rooms.GetRoom(ii).GetRemainingSpawnPointCount(Team.Type.Monsters) == 0 & ii == StartingFloor) 
				{
					//Don't move if placed on a full floor
					//Ponies.Log("Targeted Floor: " + StartingFloor.ToString() + " is full.");
					yield break;
				}

				//Ponies.Log("FLoor(" + ii.ToString() + ") is valid: " + IsValidRoom.ToString());

				if (IsValidRoom) 
				{
					int SpawnsLeft = rooms.GetRoom(ii).GetRemainingSpawnPointCount(Team.Type.Monsters);
					//Ponies.Log("Floor("+ ii.ToString() +") Spawns Left: " + SpawnsLeft.ToString());

					//check if room is full
					if (SpawnsLeft > 0) 
					{
						//check if room is not empty
						if (SpawnsLeft < MaxOccupancy)
						{
							//ignore self
							if (ii == StartingFloor) 
							{
								SpawnsLeft++;
							}

							//check if room has more friendly units than the spawn point
							if (MaxOccupancy - SpawnsLeft > Current_Occupancy_Excluding_Self)
							{
								//now check for higest occupancy
								if (MaxOccupancy - SpawnsLeft > Highest_Occupancy)
								{
									Highest_Occupancy = MaxOccupancy - SpawnsLeft;
									Target_Room = ii;
									//Ponies.Log("Floor(" + ii.ToString() + ") Highest Occupancy: " + Highest_Occupancy.ToString());
								}
								else if (MaxOccupancy - SpawnsLeft == Highest_Occupancy) 
								{
									//if tied, aim for the closest floor. The also picks the bottom floor is spawned in the middle.
									if (System.Math.Abs(ii - StartingFloor) < System.Math.Abs(Target_Room - StartingFloor)) 
									{
										Target_Room = ii;
									}
								}
							}
						}
					}
				}
			}

			//Ponies.Log("Newly Targeted Room: " + Target_Room.ToString());

			//Do nothing if no valid target was found or if it's the current floor
			if (Target_Room == -1 | Target_Room == StartingFloor)
			{
				yield break;
			}
			else 
			{
				//bump self to back of target room
				//Ponies.Log("DoBump: " + Target_Room.ToString() + " - " + StartingFloor.ToString());
				//Ponies.Log("Unit: " + cardEffectParams.selfTarget.GetName());
				//Ponies.Log("From floor: " + Target_Room);
				//Ponies.Log("To floor: " + StartingFloor);
				//Ponies.Log("Distance: " + (Target_Room - StartingFloor).ToString());

				//Ponies.Log("Social effect starting spawn point: room: " + cardEffectParams.selfTarget.GetSpawnPoint().GetRoomOwner().GetRoomIndex() + " index: " + cardEffectParams.selfTarget.GetSpawnPoint().GetIndexInRoom());
				//int startRoom = cardEffectParams.selfTarget.GetSpawnPoint().GetRoomOwner().GetRoomIndex();
				//int startIndex = cardEffectParams.selfTarget.GetSpawnPoint().GetIndexInRoom();

				yield return bumper.Bump(cardEffectParams, Target_Room - StartingFloor);

				/*
				if (startRoom == cardEffectParams.selfTarget.GetCurrentRoomIndex() && startIndex == cardEffectParams.selfTarget.GetSpawnPoint().GetIndexInRoom()) 
				{ 
				}
                else 
				{
					Ponies.LastMovedUnit = cardEffectParams.selfTarget.GetSpawnPoint();
				}*/

				if (Ponies.HallowedHallsInEffect && StartingFloor != cardEffectParams.selfTarget.GetCurrentRoomIndex())
				{
					yield return cardEffectParams.relicManager.ApplyOnDiscardRelicEffects(new CardManager.DiscardCardParams
					{
						discardCard = cardEffectParams.selfTarget.GetSpawnerCard(),
						wasPlayed = true,
						characterSummoned = cardEffectParams.selfTarget
					});
					cardEffectParams.monsterManager.SetOverrideIgnoreCapacity(false);
					CardManager cardManager = cardEffectParams.cardManager;						
					cardManager.MoveToStandByPile(cardEffectParams.selfTarget.GetSpawnerCard(), false, false, new RemoveFromStandByCondition(() => cardManager.CheckMonsterRemoveFromStandByCondition(cardEffectParams.selfTarget), null), null, HandUI.DiscardEffect.None);
					Ponies.Log("Fixing iteration for " + cardEffectParams.selfTarget.GetName());
					//CS$<> 8__locals2 = null;
					//spawnPoint = null;
					//card = null;
				}

				//Ponies.Log("Social effect end spawn point: room: " + cardEffectParams.selfTarget.GetSpawnPoint().GetRoomOwner().GetRoomIndex() + " index: " + cardEffectParams.selfTarget.GetSpawnPoint().GetIndexInRoom());
			}

			yield break;
		}
    }


	// Token: 0x02000045 RID: 69
	internal class StatusEffectSocial : StatusEffectState
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x000068E4 File Offset: 0x00004AE4
		//Associated Text keys:
		//StatusEffect_social_CardText
		//StatusEffect_social_CharacterTooltipText
		//StatusEffect_social_CardTooltipText
		//StatusEffect_social_NotificationText
		//StatusEffect_social_Stack_CardText

		public static void Make()
		{
			new StatusEffectDataBuilder
			{
				StatusEffectStateName = typeof(StatusEffectSocial).AssemblyQualifiedName,
				StatusId = "social",
				DisplayCategory = StatusEffectData.DisplayCategory.Persistent,
				IconPath = "ClanAssets/social.png",
				ShowStackCount = false,
				IsStackable = false,
				TriggerStage = StatusEffectData.TriggerStage.None
			}.Build();
		}

		public const string statusId = "social";
	}
}

/*
//Making Social non-removable isn't necessary and goes counter to expectations. Adding the social effect after a unit's been played won't activate it anyway, and death clears added status effects so nothing is actually needed there either.
//While it's possible to make a card that activates a unit's non-scaled summon abilities, no existing cards do this, and it would also only apply to the edge case where social is transferred in some way like Superfood Primordium.
namespace Equestrian.HarmonyPatches
{
	[HarmonyPatch(typeof(CardEffectRemoveAllStatusEffects), "ApplyEffect")]
	public static class SocialIsNotRemovable
	{
		public static void Prefix(out List<bool> __isSocial, ref CardEffectState cardEffectState, ref CardEffectParams cardEffectParams)
		{
			__isSocial = new List<bool>() { };

			List<CharacterState.StatusEffectStack> list = new List<CharacterState.StatusEffectStack>();
			for (int ii = 0; ii < cardEffectParams.targets.Count; ii++)
			{
				CharacterState characterState = cardEffectParams.targets[ii];
				characterState.GetStatusEffects(out list, false);

				bool isSocial = false;

				for (int jj = 0; jj < list.Count; jj++)
				{
					if (list[jj].State.GetStatusId() == "social")
					{
						isSocial = true;
					}
				}

				if (isSocial)
				{
					__isSocial.Add(true);
				}
				else
				{
					__isSocial.Add(false);
				}

				list.Clear();
			}
		}

		/// <summary>
		/// This should prevent Eel Gorgon from removing the 'social' tag.
		/// </summary>
		public static void Postfix(List<bool> __isSocial, ref CardEffectState cardEffectState, ref CardEffectParams cardEffectParams)
		{
			if (cardEffectParams.targets.Count != __isSocial.Count)
			{
				Ponies.Log("Something went wrong when trying to prevent Social from being removed. Aborting.");
				return;
			}

			for (int kk = 0; kk < cardEffectParams.targets.Count; kk++)
			{
				if (__isSocial[kk])
				{
					//Even if it wasn't removed, adding another stack won't make a difference.
					cardEffectParams.targets[kk].AddStatusEffect("social", 1);
				}
			}

			__isSocial.Clear();
		}
	}

	[HarmonyPatch(typeof(CardEffectAddStatsAndStatusEffectsFromSelf), "ApplyEffect")]
	public static class FixSocialTransfer
	{
		public static void Prefix(out List<bool> __isSocial, ref CardEffectState cardEffectState, ref CardEffectParams cardEffectParams)
		{
			__isSocial = new List<bool>() { };

			if (cardEffectParams.selfTarget.GetStatusEffect("social") == null) { return; }

			List<CharacterState.StatusEffectStack> list = new List<CharacterState.StatusEffectStack>();
			for (int ii = 0; ii < cardEffectParams.targets.Count; ii++)
			{
				CharacterState characterState = cardEffectParams.targets[ii];
				characterState.GetStatusEffects(out list, false);

				bool isSocial = false;

				for (int jj = 0; jj < list.Count; jj++)
				{
					if (list[jj].State.GetStatusId() == "social")
					{
						isSocial = true;
					}
				}

				if (isSocial)
				{
					__isSocial.Add(true);
				}
				else
				{
					__isSocial.Add(false);
				}

				list.Clear();
			}
		}

		public static void Postfix(List<bool> __isSocial, ref CardEffectState cardEffectState, ref CardEffectParams cardEffectParams)
		{
			__isSocial = new List<bool>() { };

			if (cardEffectParams.selfTarget.GetStatusEffect("social") == null) { return; }

			CardUpgradeData upgrade = new CardUpgradeDataBuilder()
			{
				UpgradeTitle = "Spreading_Social",
				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>()
				{
					new CharacterTriggerDataBuilder()
					{
						Trigger = CharacterTriggerData.Trigger.OnUnscaledSpawn,
						HideTriggerTooltip = true,
						EffectBuilders = new List<CardEffectDataBuilder>() 
						{
							new CardEffectDataBuilder()
							{
								EffectStateName = typeof(CustomEffectSocial).AssemblyQualifiedName,
								TargetMode = TargetMode.Self
							}
						}
					}
				}
			}.Build();

			//upgrade.
		}
	}
}
*/