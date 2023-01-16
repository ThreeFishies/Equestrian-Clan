using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Managers;
using Trainworks.Constants;
using Equestrian.MonsterCards;
using Equestrian;
using Equestrian.Init;
using CustomEffects;
using ShinyShoe;
using Equestrian.CardPools;
using Equestrian.Enhancers;

namespace Equestrian.Champions
{
	internal class MareaLee
	{
		public static readonly string ID = Ponies.GUID + "_MareaLee";
		
		//last one
		//public static CardData MareaLeeData = new CardData();
		//public static readonly string CharID = Ponies.GUID + "_MareaLeeCharacter";

		public static void BuildAndRegister()
		{
			CharacterChatterData mareALeeChatterData = UnityEngine.ScriptableObject.CreateInstance<CharacterChatterData>();

			mareALeeChatterData = new CharacterChatterDataBuilder()
			{
				gender = CharacterChatterData.Gender.Female,

				characterIdleExpressionKeys = new List<string>()
				{
					"Pony_Champion_MareaLee_Chatter_Idle_1_Key",
					"Pony_Champion_MareaLee_Chatter_Idle_2_Key",
					"Pony_Champion_MareaLee_Chatter_Idle_3_Key",
					"Pony_Champion_MareaLee_Chatter_Idle_4_Key",
					"Pony_Champion_MareaLee_Chatter_Idle_5_Key",
					"Pony_Champion_MareaLee_Chatter_Idle_6_Key",
					"Pony_Champion_MareaLee_Chatter_Idle_7_Key",
					"Pony_Champion_MareaLee_Chatter_Idle_8_Key",
					"Pony_Champion_MareaLee_Chatter_Idle_9_Key",
                    "Pony_Champion_MareaLee_Chatter_Idle_10_Key",
                },

				characterAddedExpressionKeys = new List<string>()
				{
					"Pony_Champion_MareaLee_Chatter_Added_1_Key",
                    "Pony_Champion_MareaLee_Chatter_Added_2_Key",
                    "Pony_Champion_MareaLee_Chatter_Added_3_Key",
                    "Pony_Champion_MareaLee_Chatter_Added_4_Key"
                },

				characterAttackingExpressionKeys = new List<string>()
				{
					"Pony_Champion_MareaLee_Chatter_Attacking_1_Key",
                    "Pony_Champion_MareaLee_Chatter_Attacking_2_Key",
                    "Pony_Champion_MareaLee_Chatter_Attacking_3_Key",
                    "Pony_Champion_MareaLee_Chatter_Attacking_4_Key",
                    "Pony_Champion_MareaLee_Chatter_Attacking_5_Key",
                    "Pony_Champion_MareaLee_Chatter_Attacking_6_Key",
                    "Pony_Champion_MareaLee_Chatter_Attacking_7_Key",
                    "Pony_Champion_MareaLee_Chatter_Attacking_8_Key"
                },

				characterSlayedExpressionKeys = new List<string>()
				{
                    "Pony_Champion_MareaLee_Chatter_Slayed_1_Key",
                    "Pony_Champion_MareaLee_Chatter_Slayed_2_Key",
                    "Pony_Champion_MareaLee_Chatter_Slayed_3_Key",
                    "Pony_Champion_MareaLee_Chatter_Slayed_4_Key"
                },

				characterTriggerExpressionKeys = new List<CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys>()
				{
					//Post Combat and Slayed happen at similar times, but Mare a Lee's not as likely to get kills so I'll leav it be.
					new CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys
					{
						Trigger = CharacterTriggerData.Trigger.PostCombat,
						Key = "Pony_Champion_MareaLee_Chatter_Trigger_PostCombat_1_Key",
					},
                    new CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys
                    {
                        Trigger = CharacterTriggerData.Trigger.PostCombat,
                        Key = "Pony_Champion_MareaLee_Chatter_Trigger_PostCombat_2_Key",
                    },
                    new CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys
                    {
                        Trigger = CharacterTriggerData.Trigger.PostCombat,
                        Key = "Pony_Champion_MareaLee_Chatter_Trigger_PostCombat_3_Key",
                    },
                    new CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys
                    {
                        Trigger = CharacterTriggerData.Trigger.PostCombat,
                        Key = "Pony_Champion_MareaLee_Chatter_Trigger_PostCombat_4_Key",
                    },

					//End of turn and attacking happen at similar times.
					//Moving to Attacking to reduce chatter spam.
					//new CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys
					//{
					//	Trigger = CharacterTriggerData.Trigger.EndTurnPreHandDiscard,
					//	Key = "Pony_Champion_MareaLee_Chatter_Trigger_EndOfTurn_1_Key",
					//},
                    //new CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys
                    //{
                    //    Trigger = CharacterTriggerData.Trigger.EndTurnPreHandDiscard,
                    //    Key = "Pony_Champion_MareaLee_Chatter_Trigger_EndOfTurn_2_Key",
                    //},
                    //new CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys
                    //{
                    //    Trigger = CharacterTriggerData.Trigger.EndTurnPreHandDiscard,
                    //    Key = "Pony_Champion_MareaLee_Chatter_Trigger_EndOfTurn_3_Key",
                    //},
                    //new CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys
                    //{
                    //    Trigger = CharacterTriggerData.Trigger.EndTurnPreHandDiscard,
                    //    Key = "Pony_Champion_MareaLee_Chatter_Trigger_EndOfTurn_4_Key",
                    //}
				},
			}.Build();

			CharacterDataBuilder champion = new CharacterDataBuilder
			{
				CharacterID = ID,
				Name = "Mare a Lee",
				NameKey = "Pony_Champion_MareaLee_Name_Key",
				Size = 1,
				Health = 5,
				AttackDamage = 0,

				CharacterChatterData = mareALeeChatterData,

				AssetPath = "MonsterAssets/MareaLeeUnit.png"
			};
			CardData MareaLeeData = new ChampionCardDataBuilder()
			{
				Champion = champion,
				ChampionIconPath = "ClanAssets/MareALee.png",
				ChampionSelectedCue = "",
				StarterCardData = CustomCardManager.GetCardDataByID(BackgroundPony.ID),
				LinkedClass = CustomClassManager.GetClassDataByID(EquestrianClan.ID),
				//Note: Shared mastery card assets are loaded as well, avoiding art issues when spawning cards.
				SharedMasteryCards = new List<CardData>() 
				{ 
					CustomCardManager.GetCardDataByID(TrashPanda.ID),
					CustomCardManager.GetCardDataByID(YoLo.ID),
					CustomCardManager.GetCardDataByID(Finchy.ID),
					CustomCardManager.GetCardDataByID(CrunchieMunchie.ID),
					CustomCardManager.GetCardDataByID(Carrot.ID)
				},

				UpgradeTree = new CardUpgradeTreeDataBuilder
				{
					UpgradeTrees = new List<List<CardUpgradeDataBuilder>>
					{
						new List<CardUpgradeDataBuilder>
						{
							Mentor_I.Builder(),
							Mentor_II.Builder(),
							Mentor_III.Builder()
						},
						new List<CardUpgradeDataBuilder>
						{
							Coach_I.Builder(),
							Coach_II.Builder(),
							Coach_III.Builder()
						},
						new List<CardUpgradeDataBuilder>
						{
							Tutor_I.Builder(),
							Tutor_II.Builder(),
							Tutor_III.Builder()
						}
					}
				},

				CardID = ID,
				NameKey = "Pony_Champion_MareaLee_Name_Key",
				ClanID = CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID(),
				CardPoolIDs = new List<string> { VanillaCardPoolIDs.ChampionPool },

				AssetPath = "MonsterAssets/MareaLeeCard.png",
				CardLoreTooltipKeys = new List<string>
				{
					"Pony_Champion_MareaLee_Lore_Key"
				}
			}.BuildAndRegister();
		}
	}

	internal class Mentor_I 
	{
		public static CardUpgradeDataBuilder Builder()
		{
			return new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = "Pony_Champion_MareaLee_MentorI_Title_Key",
				UpgradeDescriptionKey = "Pony_Champion_MareaLee_MentorI_Description_Key",
				UseUpgradeHighlightTextTags = true,
				BonusHP = 20,
				BonusDamage = 0,
				
				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.OnSpawn,
						DescriptionKey = "Pony_Champion_MareaLee_MentorI_Trigger0_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateName = "CardEffectAddBattleCard",
								ParamCardPool = TrashPandaCardPool.GetPool(),
								ParamInt = ((int)CardPile.HandPile),
								AdditionalParamInt = 1
							},
						}
					},
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.PostCombat,
						DescriptionKey = "Pony_Champion_MareaLee_MentorI_Trigger1_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								//EffectStateType = VanillaCardEffectTypes.CardEffectHeal,
								EffectStateName = "CardEffectHeal",
								ParamInt = 4,
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters
							}
						}
					}
				},
			};
		}

		public static CardUpgradeData Make()
		{
			return Mentor_I.Builder().Build();
		}
	}

	internal class Mentor_II
	{
		public static CardUpgradeDataBuilder Builder()
		{
			return new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = "Pony_Champion_MareaLee_MentorII_Title_Key",
				UpgradeDescriptionKey = "Pony_Champion_MareaLee_MentorII_Description_Key",
				UseUpgradeHighlightTextTags = true,
				BonusHP = 45,
				BonusDamage = 0,
				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.OnSpawn,
						DescriptionKey = "Pony_Champion_MareaLee_MentorII_Trigger0_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateName = "CardEffectAddBattleCard",
								ParamCardPool = MyCardPools.YoLoCardPool,
								ParamInt = ((int)CardPile.HandPile),
								AdditionalParamInt = 1
							},
							new CardEffectDataBuilder
							{
								EffectStateName = "CardEffectAddBattleCard",
								ParamCardPool = MyCardPools.FinchyCardPool,
								ParamInt = ((int)CardPile.HandPile),
								AdditionalParamInt = 1
							},
							new CardEffectDataBuilder
							{
								EffectStateName = "CardEffectAddBattleCard",
								ParamCardPool = MyCardPools.CrunchieMunchieCardPool,
								ParamInt = ((int)CardPile.HandPile),
								AdditionalParamInt = 1
							},

							//new CardEffectDataBuilder
							//{
							//	EffectStateName = typeof(AddThreeBackgroundPoniesArtFix).AssemblyQualifiedName,
							//	TargetMode = TargetMode.Self
							//}
						}
					},
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.PostCombat,
						DescriptionKey = "Pony_Champion_MareaLee_MentorII_Trigger1_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								//EffectStateType = VanillaCardEffectTypes.CardEffectHeal,
								EffectStateName = "CardEffectHeal",
								ParamInt = 8,
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters
							}
						}
					}
				},
			};
		}

		public static CardUpgradeData Make()
		{
			return Mentor_II.Builder().Build();
		}
	}

	internal class Mentor_III
	{
		public static CardUpgradeDataBuilder Builder()
		{
			return new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = "Pony_Champion_MareaLee_MentorIII_Title_Key",
				UpgradeDescriptionKey = "Pony_Champion_MareaLee_MentorIII_Description_Key",
				UseUpgradeHighlightTextTags = true,
				BonusHP = 95,
				BonusDamage = 0,
				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.PostCombat,
						DescriptionKey = "Pony_Champion_MareaLee_MentorIII_Trigger0_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateName = "CardEffectAddBattleCard",
								ParamCardPool = MyCardPools.MareALeeMentorIIICardPool,
								ParamInt = ((int)CardPile.HandPile),
								AdditionalParamInt = 1,

								ParamCardUpgradeData = Gradstone.GradstoneData.GetEffects()[0].GetParamCardUpgradeData(),
								/*
								ParamCardUpgradeData = new CardUpgradeDataBuilder
								{
									BonusHP = 15,
									BonusDamage = 15,
									CostReduction = 99,
									XCostReduction = 0,

									TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder> 
									{ 
										new CharacterTriggerDataBuilder
										{ 
											Trigger = CharacterTriggerData.Trigger.OnUnscaledSpawn,
											//DescriptionKey = "",
											EffectBuilders = new List<CardEffectDataBuilder>
											{
												new CardEffectDataBuilder
												{ 
													EffectStateName = typeof(CustomEffectSocial).AssemblyQualifiedName,
													TargetMode = TargetMode.Self,
												},
											}
										}
									},

									StatusEffectUpgrades = new List<StatusEffectStackData>
									{
										new StatusEffectStackData
										{ 
											statusId = "social",
											count = 1
										}
									}
								}.Build()*/
							},
						}
					},
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.PostCombat,
						DescriptionKey = "Pony_Champion_MareaLee_MentorIII_Trigger1_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								//EffectStateType = VanillaCardEffectTypes.CardEffectHeal,
								EffectStateName = "CardEffectHeal",
								ParamInt = 15,
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters
							}
						}
					}
				},
			};
		}

		public static CardUpgradeData Make()
		{
			return Mentor_III.Builder().Build();
		}
	}
	
	internal class Coach_I
	{
		public static CardUpgradeDataBuilder Builder()
		{
			return new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = "Pony_Champion_MareaLee_CoachI_Title_Key",
				UpgradeDescriptionKey = "Pony_Champion_MareaLee_CoachI_Description_Key",
				UseUpgradeHighlightTextTags = true,
				BonusHP = 0,
				BonusDamage = 15,

				StatusEffectUpgrades = new List<StatusEffectStackData>
				{
					new StatusEffectStackData
					{
						statusId = VanillaStatusEffectIDs.Armor,
						count = 5
					}
				},

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.AfterSpawnEnchant,
						DescriptionKey = "Pony_Champion_MareaLee_CoachI_Trigger_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateName = "CardEffectEnchant",
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters,

								ParamStatusEffects = new StatusEffectStackData[]
								{
									new StatusEffectStackData
									{
										statusId = VanillaStatusEffectIDs.Endless,
										count = 1
									}
								}
							}
						}
					},
				},
			};
		}

		public static CardUpgradeData Make()
		{
			return Coach_I.Builder().Build();
		}
	}

	internal class Coach_II
	{
		public static CardUpgradeDataBuilder Builder()
		{
			return new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = "Pony_Champion_MareaLee_CoachII_Title_Key",
				UpgradeDescriptionKey = "Pony_Champion_MareaLee_CoachII_Description_Key",
				UseUpgradeHighlightTextTags = true,
				BonusHP = 0,
				BonusDamage = 30,

				StatusEffectUpgrades = new List<StatusEffectStackData>
				{
					new StatusEffectStackData
					{
						statusId = VanillaStatusEffectIDs.Armor,
						count = 10
					}
				},

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.AfterSpawnEnchant,
						DescriptionKey = "Pony_Champion_MareaLee_CoachII_Trigger_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateName = "CardEffectEnchant",
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters,

								ParamStatusEffects = new StatusEffectStackData[]
								{
									new StatusEffectStackData
									{
										statusId = VanillaStatusEffectIDs.Endless,
										count = 1
									},
								}
							},
							new CardEffectDataBuilder
							{
								EffectStateName = "CardEffectEnchant",
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters,

								ParamStatusEffects = new StatusEffectStackData[]
								{
									new StatusEffectStackData
									{
										statusId = VanillaStatusEffectIDs.Quick,
										count = 1
									}
								}
							}
						}
					},
				},
			};
		}

		public static CardUpgradeData Make()
        {
            return Coach_II.Builder().Build();
        }
    }


	internal class Coach_III
	{
		public static CardUpgradeDataBuilder Builder()
		{
			return new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = "Pony_Champion_MareaLee_CoachIII_Title_Key",
				UpgradeDescriptionKey = "Pony_Champion_MareaLee_CoachIII_Description_Key",
				UseUpgradeHighlightTextTags = true,
				BonusHP = 0,
				BonusDamage = 60,

				StatusEffectUpgrades = new List<StatusEffectStackData>
				{
					new StatusEffectStackData
					{
						statusId = VanillaStatusEffectIDs.Armor,
						count = 20
					}
				},

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.AfterSpawnEnchant,
						DescriptionKey = "Pony_Champion_MareaLee_CoachIII_Trigger_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateName = "CardEffectEnchant",
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters,

								ParamStatusEffects = new StatusEffectStackData[]
								{
									new StatusEffectStackData
									{
										statusId = VanillaStatusEffectIDs.Endless,
										count = 1
									}
								}
							},
							new CardEffectDataBuilder
							{
								EffectStateName = "CardEffectEnchant",
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters,

								ParamStatusEffects = new StatusEffectStackData[]
								{
									new StatusEffectStackData
									{
										statusId = VanillaStatusEffectIDs.Quick,
										count = 1
									}
								}
							},
							new CardEffectDataBuilder
							{
								EffectStateName = "CardEffectEnchant",
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters,

								ParamStatusEffects = new StatusEffectStackData[]
								{
									new StatusEffectStackData
									{
										statusId = VanillaStatusEffectIDs.Trample,
										count = 1
									}
								}
							},
						}
					},
				},
			};
		}

		public static CardUpgradeData Make()
		{
			return Coach_III.Builder().Build();
		}
	}

	internal class Tutor_I
	{
		public static CardUpgradeDataBuilder Builder()
		{
			return new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = "Pony_Champion_MareaLee_TutorI_Title_Key",
				UpgradeDescriptionKey = "Pony_Champion_MareaLee_TutorI_Description_Key",
				UseUpgradeHighlightTextTags = true,
				BonusHP = 5,
				BonusDamage = 5,
				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.EndTurnPreHandDiscard,
						DescriptionKey = "Pony_Champion_MareaLee_TutorI_Trigger_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								//EffectStateType = VanillaCardEffectTypes.CardEffectAddTempCardUpgradeToCardsInHand,
								//EffectStateName = "CardEffectAddTempCardUpgradeToCardsInHand",
								EffectStateName = typeof(CharacterEffectAddTempCardUpgradeToCardsInHand).AssemblyQualifiedName,
								TargetCardType = CardType.Monster,
								TargetMode = TargetMode.Hand,
								ParamCardUpgradeData = new CardUpgradeDataBuilder
								{
									BonusHP = 3,
									BonusDamage = 3,

									Filters = new List<CardUpgradeMaskData>
									{
										new CardUpgradeMaskDataBuilderFixed
										{
											CardType = CardType.Monster,
										}.Build()
									},

									TraitDataUpgradeBuilders = new List<CardTraitDataBuilder>
									{
										new CardTraitDataBuilder
										{
											TraitStateType = VanillaCardTraitTypes.CardTraitFreeze,
										}
									}
								}.Build(),
							}
						}
					},
				},
			};
		}

		public static CardUpgradeData Make()
		{
			return Tutor_I.Builder().Build();
		}
	}

	internal class Tutor_II
	{
		public static CardUpgradeDataBuilder Builder()
		{
			return new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = "Pony_Champion_MareaLee_TutorII_Title_Key",
				UpgradeDescriptionKey = "Pony_Champion_MareaLee_TutorII_Description_Key",
				UseUpgradeHighlightTextTags = true,
				BonusHP = 15,
				BonusDamage = 10,
				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.EndTurnPreHandDiscard,
						DescriptionKey = "Pony_Champion_MareaLee_TutorII_Trigger_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								//EffectStateType = VanillaCardEffectTypes.CardEffectAddTempCardUpgradeToCardsInHand,
								EffectStateName = typeof(CharacterEffectAddTempCardUpgradeToCardsInHand).AssemblyQualifiedName,
								TargetCardType = CardType.Monster,
								TargetMode = TargetMode.Hand,
								ParamCardUpgradeData = new CardUpgradeDataBuilder
								{
									BonusHP = 6,
									BonusDamage = 6,
									CostReduction = 1,
									XCostReduction = 0,

									Filters = new List<CardUpgradeMaskData>
									{
										new CardUpgradeMaskDataBuilderFixed
										{
											CardType = CardType.Monster,
										}.Build()
									},

									TraitDataUpgradeBuilders = new List<CardTraitDataBuilder>
									{
										new CardTraitDataBuilder
										{
											TraitStateType = VanillaCardTraitTypes.CardTraitFreeze,
										}
									}
								}.Build(),
							}
						}
					},
				},
			};
		}

		public static CardUpgradeData Make()
		{
			return Tutor_II.Builder().Build();
		}
	}

	internal class Tutor_III
	{
		public static CardUpgradeDataBuilder Builder()
		{
			return new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = "Pony_Champion_MareaLee_TutorIII_Title_Key",
				UpgradeDescriptionKey = "Pony_Champion_MareaLee_TutorIII_Description_Key",
				UseUpgradeHighlightTextTags = true,
				BonusHP = 35,
				BonusDamage = 20,
				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.EndTurnPreHandDiscard,
						DescriptionKey = "Pony_Champion_MareaLee_TutorIII_Trigger_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								//EffectStateType = VanillaCardEffectTypes.CardEffectAddTempCardUpgradeToCardsInHand,
								EffectStateName = typeof(CharacterEffectAddTempCardUpgradeToCardsInHand).AssemblyQualifiedName,
								TargetCardType = CardType.Monster,
								TargetMode = TargetMode.Hand,
								ParamCardUpgradeData = new CardUpgradeDataBuilder
								{
									BonusHP = 10,
									BonusDamage = 10,
									CostReduction = 1,
									XCostReduction = 0,
									BonusSize = -1,

									Filters = new List<CardUpgradeMaskData>
									{
										new CardUpgradeMaskDataBuilderFixed
										{
											CardType = CardType.Monster,
										}.Build()
									},

									TraitDataUpgradeBuilders = new List<CardTraitDataBuilder>
									{
										new CardTraitDataBuilder
										{
											TraitStateType = VanillaCardTraitTypes.CardTraitFreeze,
										}
									}
								}.Build(),
							}
						}
					},
				},
			};
		}

		public static CardUpgradeData Make()
		{
			return Tutor_III.Builder().Build();
		}
	}
}
