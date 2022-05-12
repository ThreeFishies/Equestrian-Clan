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
using Equestrian.Mutators;

namespace CustomEffects
{
	internal class StatusEffectMale : StatusEffectState
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x000068E4 File Offset: 0x00004AE4
		//Associated Text keys:
		//StatusEffect_male_CardText
		//StatusEffect_male_CharacterTooltipText
		//StatusEffect_male_CardTooltipText
		//StatusEffect_male_NotificationText
		//StatusEffect_male_Stack_CardText

		public static void Make()
		{
			new StatusEffectDataBuilder
			{
				StatusEffectStateName = typeof(StatusEffectMale).AssemblyQualifiedName,
				StatusId = "male",
				DisplayCategory = StatusEffectData.DisplayCategory.Persistent,
				IconPath = "ClanAssets/Male.png",
				ShowStackCount = false,
				IsStackable = false,
				TriggerStage = StatusEffectData.TriggerStage.None,
			}.Build();
		}

		public const string statusId = "male";
	}

	internal class StatusEffectFemale : StatusEffectState
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x000068E4 File Offset: 0x00004AE4
		//Associated Text keys:
		//StatusEffect_female_CardText
		//StatusEffect_female_CharacterTooltipText
		//StatusEffect_female_CardTooltipText
		//StatusEffect_female_NotificationText
		//StatusEffect_female_Stack_CardText

		public static void Make()
		{
			new StatusEffectDataBuilder
			{
				StatusEffectStateName = typeof(StatusEffectFemale).AssemblyQualifiedName,
				StatusId = "female",
				DisplayCategory = StatusEffectData.DisplayCategory.Persistent,
				IconPath = "ClanAssets/Female.png",
				ShowStackCount = false,
				IsStackable = false,
				TriggerStage = StatusEffectData.TriggerStage.None,
			}.Build();
		}

		public const string statusId = "female";
	}

	internal class StatusEffectGenderless : StatusEffectState
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x000068E4 File Offset: 0x00004AE4
		//Associated Text keys:
		//StatusEffect_genderless_CardText
		//StatusEffect_genderless_CharacterTooltipText
		//StatusEffect_genderless_CardTooltipText
		//StatusEffect_genderless_NotificationText
		//StatusEffect_genderless_Stack_CardText

		public static void Make()
		{
			new StatusEffectDataBuilder
			{
				StatusEffectStateName = typeof(StatusEffectGenderless).AssemblyQualifiedName,
				StatusId = "genderless",
				DisplayCategory = StatusEffectData.DisplayCategory.Persistent,
				IconPath = "ClanAssets/Genderless.png",
				ShowStackCount = false,
				IsStackable = false,
				TriggerStage = StatusEffectData.TriggerStage.None,
			}.Build();
		}

		public const string statusId = "genderless";
	}

	internal class StatusEffectUndefined : StatusEffectState
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x000068E4 File Offset: 0x00004AE4
		//Associated Text keys:
		//StatusEffect_undefined_CardText
		//StatusEffect_undefined_CharacterTooltipText
		//StatusEffect_undefined_CardTooltipText
		//StatusEffect_undefined_NotificationText
		//StatusEffect_undefined_Stack_CardText

		public static void Make()
		{
			new StatusEffectDataBuilder
			{
				StatusEffectStateName = typeof(StatusEffectUndefined).AssemblyQualifiedName,
				StatusId = "undefined",
				DisplayCategory = StatusEffectData.DisplayCategory.Persistent,
				IconPath = "ClanAssets/Undefined.png",
				ShowStackCount = false,
				IsStackable = false,
				TriggerStage = StatusEffectData.TriggerStage.None,
			}.Build();
		}

		public const string statusId = "undefined";
	}

	public class CustomMutatorEffectAddGenderOnSpawn : RelicEffectBase, IStatusEffectRelicEffect
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x0000C623 File Offset: 0x0000A823
		public override bool CanShowNotifications
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00058298 File Offset: 0x00056498
		public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
		{
			base.Initialize(relicState, relicData, relicEffectData);
			this.targetTeam = relicEffectData.GetParamSourceTeam();
			//this.statusEffects = relicEffectData.GetParamStatusEffects();
			this.characterSubtype = relicEffectData.GetParamCharacterSubtype();
			this.excludeCharacterSubtypes = relicEffectData.GetParamExcludeCharacterSubtypes();
			this.restrictToRoom = relicEffectData.GetParamBool();
			this.restrictedRoomIndex = relicEffectData.GetParamInt();
			this.allowFromCard = (relicEffectData.GetParamString() != "NoCard");
			this.onlyAllowedFromCard = (relicEffectData.GetParamString() == "OnlyFromCard");
			this.statusEffects = new StatusEffectStackData[4] 
			{ 
				new StatusEffectStackData
				{
					statusId = "male",
					count = 1,
				},
				new StatusEffectStackData
				{
					statusId = "female",
					count = 1,
				},
				new StatusEffectStackData
				{
					statusId = "genderless",
					count = 1,
				},
				new StatusEffectStackData
				{
					statusId = "undefined",
					count = 1,
				},
			};
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00058322 File Offset: 0x00056522
		public override IEnumerator OnCharacterAdded(CharacterState character, CardState fromCard, RelicManager relicManager, SaveManager saveManager, PlayerManager playerManager, RoomManager roomManager, CombatManager combatManager, CardManager cardManager)
		{
			//bool overrideImmunity = this.characterSubtype.IsPyre;
			bool overrideImmunity = true;
			bool flag = fromCard != null && fromCard.GetSpawnCharacterData() == character.GetSourceCharacterData();
			if (character.HasStatusEffect("cardless"))
			{
				flag = false;
			}
			if (flag && !this.allowFromCard)
			{
				yield break;
			}
			if (!flag && this.onlyAllowedFromCard)
			{
				yield break;
			}
			if (character.GetTeamType() != this.targetTeam || (!overrideImmunity && character.HasStatusEffect("immune")))
			{
				//yield break;
			}
			if (character.HasStatusEffect("untouchable"))
			{
				//yield break;
			}
			foreach (SubtypeData subtypeData in this.excludeCharacterSubtypes)
			{
				if (character.GetHasSubtype(subtypeData))
				{
					yield break;
				}
			}
			if (this.restrictToRoom && this.restrictedRoomIndex != character.GetSpawnPoint(false).GetRoomOwner().GetRoomIndex())
			{
				yield break;
			}
			if (this.statusEffects.Length == 0)
			{
				yield break;
			}
			//int index = RandomManager.Range(0, this.statusEffects.Length, RngId.Battle);
			int index = (int)GenderReveal.GetGender(character);
			int stacks = (this.statusEffects[index].count > 0) ? this.statusEffects[index].count : 1;
			if (character.GetCharacterManager().DoesCharacterPassSubtypeCheck(character, this.characterSubtype))
			{
				yield return base.TimingYieldIfNonCovenant(RelicEffectBase.TimingContext.PreFire, saveManager);
				CharacterState.AddStatusEffectParams addStatusEffectParams = new CharacterState.AddStatusEffectParams
				{
					overrideImmunity = overrideImmunity,
					sourceRelicState = this._srcRelicState
				};
				character.AddStatusEffect(this.statusEffects[index].statusId, stacks, addStatusEffectParams);
				base.NotifyRelicTriggered(relicManager, character);
				yield return base.TimingYieldIfNonCovenant(RelicEffectBase.TimingContext.PostFire, saveManager);
			}
			yield break;
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x0005834E File Offset: 0x0005654E
		public bool ImpactsPyre()
		{
			return this.characterSubtype.IsPyre && this.targetTeam == Team.Type.Monsters;
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00058368 File Offset: 0x00056568
		public StatusEffectStackData[] GetStatusEffects()
		{
			//return this.statusEffects;
			return new StatusEffectStackData[0] {};
		}

		// Token: 0x04000BAC RID: 2988
		private Team.Type targetTeam;

		// Token: 0x04000BAD RID: 2989
		private StatusEffectStackData[] statusEffects;

		// Token: 0x04000BAE RID: 2990
		private SubtypeData characterSubtype;

		// Token: 0x04000BAF RID: 2991
		private SubtypeData[] excludeCharacterSubtypes;

		// Token: 0x04000BB0 RID: 2992
		private bool restrictToRoom;

		// Token: 0x04000BB1 RID: 2993
		private int restrictedRoomIndex;

		// Token: 0x04000BB2 RID: 2994
		private bool allowFromCard;

		// Token: 0x04000BB3 RID: 2995
		private bool onlyAllowedFromCard;
	}


	/*
	public class CustomCardEffectAddGenderTag : CardEffectBase
	{
		public static CharacterState lastSniffedBoss;

		// Token: 0x0600069B RID: 1691 RVA: 0x00020CB4 File Offset: 0x0001EEB4
		public static StatusEffectStackData GetStatusEffectStack(CardEffectData cardEffectData, CardEffectState cardEffectState, CharacterState selfTarget, bool isTest = false)
		{ 

			if (selfTarget == null)
			{
				Ponies.Log("Character has no gender because they do not exist.");
				return null;
			}

			GenderReveal.Gender gender = GenderReveal.GetGender(selfTarget);

			switch (gender) 
			{ 
				case GenderReveal.Gender.Male: 
				{
					//Ponies.Log("Character" + selfTarget.name + "is male.");
					return new StatusEffectStackData
					{
						statusId = "male",
						count = 1
					};
				}
				case GenderReveal.Gender.Female:
				{
					//Ponies.Log("Character" + selfTarget.name + "is female.");
					return new StatusEffectStackData
					{
						statusId = "female",
						count = 1
					};
				}
				case GenderReveal.Gender.Genderless:
				{
					//Ponies.Log("Character" + selfTarget.name + "is genderless.");
					return new StatusEffectStackData
					{
						statusId = "genderless",
						count = 1
					};
				}
				case GenderReveal.Gender.Undefined:
				{
					//Ponies.Log("Character" + selfTarget.name + "is undefined.");
					return new StatusEffectStackData
					{
						statusId = "undefined",
						count = 1
					};
				}
			}

			return null;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00020D38 File Offset: 0x0001EF38
		public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			StatusEffectStackData statusEffectStack = CustomCardEffectAddGenderTag.GetStatusEffectStack(cardEffectState.GetSourceCardEffectData(), cardEffectState, cardEffectParams.selfTarget, true);
			if (statusEffectStack == null)
			{
				return false;
			}
			if (cardEffectState.GetTargetMode() != TargetMode.DropTargetCharacter)
			{
				return true;
			}
			if (cardEffectParams.targets.Count <= 0)
			{
				return false;
			}
			if (cardEffectParams.statusEffectManager.GetStatusEffectDataById(statusEffectStack.statusId).IsStackable())
			{
				return true;
			}
			foreach (CharacterState characterState in cardEffectParams.targets)
			{
				if (!characterState.HasStatusEffect(statusEffectStack.statusId) && base.IsTargetValid(cardEffectState, characterState, true))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00020DF4 File Offset: 0x0001EFF4
		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			StatusEffectStackData statusEffectStack = CustomCardEffectAddGenderTag.GetStatusEffectStack(cardEffectState.GetSourceCardEffectData(), cardEffectState, cardEffectParams.selfTarget, false);
			if (statusEffectStack == null)
			{
				yield break;
			}
			int intInRange = cardEffectState.GetIntInRange();
			CharacterState.AddStatusEffectParams addStatusEffectParams = new CharacterState.AddStatusEffectParams
			{
				sourceRelicState = cardEffectParams.sourceRelic,
				sourceCardState = cardEffectParams.playedCard,
				cardManager = cardEffectParams.cardManager,
				overrideImmunity = true,
				sourceIsHero = (cardEffectState.GetSourceTeamType() == Team.Type.Heroes)
			};

			CharacterState characterState = cardEffectParams.selfTarget;
			RngId rngId = cardEffectParams.saveManager.PreviewMode ? RngId.BattleTest : RngId.Battle;
			if (intInRange == 0 || RandomManager.Range(0, 100, rngId) < intInRange)
			{
				int count = statusEffectStack.count;
				characterState.AddStatusEffect(statusEffectStack.statusId, count, addStatusEffectParams);
			}

			CharacterState outerBoss = SniffOuterTrainBoss();

			if (outerBoss != null && outerBoss != lastSniffedBoss)
			{
				lastSniffedBoss = outerBoss;
				statusEffectStack = CustomCardEffectAddGenderTag.GetStatusEffectStack(cardEffectState.GetSourceCardEffectData(), cardEffectState, SniffOuterTrainBoss(), false);
				int count = statusEffectStack.count;
				addStatusEffectParams.sourceIsHero = true;
				SniffOuterTrainBoss().AddStatusEffect(statusEffectStack.statusId, count, addStatusEffectParams);
			}

			yield break;
		}

		public CharacterState SniffOuterTrainBoss() 
		{
			if (ProviderManager.TryGetProvider<HeroManager>(out HeroManager heroManager)) 
			{
				return heroManager.GetOuterTrainBossCharacter();
			}
			return null;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00020E0A File Offset: 0x0001F00A
		public override void GetTooltipsStatusList(CardEffectState cardEffectState, ref List<string> outStatusIdList)
		{
			if (cardEffectState != null)
			{
				if (cardEffectState.GetParentCardState() != null) 
				{
					GenderReveal.Gender gender = GenderReveal.GetGender(cardEffectState.GetParentCardState().GetSpawnCharacterData());

					switch (gender) 
					{
						case GenderReveal.Gender.Male:
						{
							outStatusIdList.Add("male");
							return;
						}
						case GenderReveal.Gender.Female:
						{
							outStatusIdList.Add("female");
							return;
						}
						case GenderReveal.Gender.Genderless: 
						{
							outStatusIdList.Add("genderless");
							return;
						}
						case GenderReveal.Gender.Undefined:
						{
							outStatusIdList.Add("undefined");
							return;
						}
					}
				}
			}
		}
	}*/
}