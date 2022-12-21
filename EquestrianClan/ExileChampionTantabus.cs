using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Managers;
using Trainworks.Constants;
using Equestrian.SpellCards;
using Equestrian.MonsterCards;
using Equestrian;
using Equestrian.Init;
using CustomEffects;
using ShinyShoe;
using Equestrian.CardPools;

namespace Equestrian.Champions
{
	internal class Tantabus
	{
		public static readonly string ID = Ponies.GUID + "_Tantabus";
		//public static readonly string CharID = Ponies.GUID + "_TantabusCharacter";

		public static void BuildAndRegister()
		{
			CharacterDataBuilder champion = new CharacterDataBuilder
			{
				CharacterID = ID,
				Name = "Tantaabus",
				NameKey = "Pony_Champion_Tantabus_Name_Key",
				Size = 2,
				Health = 10,
				AttackDamage = 20,
				CharacterChatterData = new CharacterChatterDataBuilder()
				{
					gender = CharacterChatterData.Gender.Neutral,
					characterAddedExpressionKeys = new List<string> 
					{
						"Pony_Champion_Tantabus_Chatter_Added_1_Key"
					},
					characterAttackingExpressionKeys = new List<string> 
					{
						"Pony_Champion_Tantabus_Chatter_Attacking_1_Key"
					},
					characterSlayedExpressionKeys = new List<string> 
					{
						"Pony_Champion_Tantabus_Chatter_Slayed_1_Key"
					},
					characterIdleExpressionKeys = new List<string> 
					{
						"Pony_Champion_Tantabus_Chatter_Idle_1_Key",
						"Pony_Champion_Tantabus_Chatter_Idle_2_Key",
						"Pony_Champion_Tantabus_Chatter_Idle_3_Key",
						"Pony_Champion_Tantabus_Chatter_Idle_4_Key",
						"Pony_Champion_Tantabus_Chatter_Idle_5_Key",
						"Pony_Champion_Tantabus_Chatter_Idle_6_Key",
						"Pony_Champion_Tantabus_Chatter_Idle_7_Key",
						"Pony_Champion_Tantabus_Chatter_Idle_8_Key",
					},
					characterTriggerExpressionKeys = new List<CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys> 
					{ 
						new CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys()
						{
							Key = "Pony_Champion_Tantabus_Chatter_Trigger_PostCombat_1_Key",
							Trigger = CharacterTriggerData.Trigger.PostCombat
						},
						new CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys()
						{
							Key = "Pony_Champion_Tantabus_Chatter_Trigger_OnKill_1_Key",
							Trigger = CharacterTriggerData.Trigger.OnKill
						},
						new CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys()
						{
							Key = "Pony_Champion_Tantabus_Chatter_Trigger_OnTurnBegin_1_Key",
							Trigger = CharacterTriggerData.Trigger.OnTurnBegin
						}
					}
				}.Build(),

				AssetPath = "MonsterAssets/TantabusUnit.png"
			};
			new ChampionCardDataBuilder()
			{
				Champion = champion,
				ChampionIconPath = "ClanAssets/Tantabus.png",
				ChampionSelectedCue = "",
				StarterCardData = CustomCardManager.GetCardDataByID(NightTerrors.ID),
				LinkedClass = CustomClassManager.GetClassDataByID(EquestrianClan.ID),

				UpgradeTree = new CardUpgradeTreeDataBuilder
				{
					UpgradeTrees = new List<List<CardUpgradeDataBuilder>>
					{
						new List<CardUpgradeDataBuilder>
						{
							Psychosis_I.Builder(),
							Psychosis_II.Builder(),
							Psychosis_III.Builder()
						},
						new List<CardUpgradeDataBuilder>
						{
							Nightmare_I.Builder(),
							Nightmare_II.Builder(),
							Nightmare_III.Builder()
						},
						new List<CardUpgradeDataBuilder>
						{
							Lucid_I.Builder(),
							Lucid_II.Builder(),
							Lucid_III.Builder()
						}
					}
				},

				CardID = ID,
				NameKey = "Pony_Champion_Tantabus_Name_Key",
				ClanID = CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID(),
				CardPoolIDs = new List<string> { VanillaCardPoolIDs.ChampionPool },

				AssetPath = "MonsterAssets/TantabusCard.png",
				CardLoreTooltipKeys = new List<string>
				{
					"Pony_Champion_Tantabus_Lore_Key"
				}
			}.BuildAndRegister(1);
		}
	}

	internal class Psychosis_I 
	{
		public static CardUpgradeDataBuilder Builder()
		{
			return new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = "Pony_Champion_Tantabus_PsychosisI_Title_Key",
				UpgradeDescriptionKey = "Pony_Champion_Tantabus_PsychosisI_Description_Key",
				UseUpgradeHighlightTextTags = true,
				BonusHP = 0,
				BonusDamage = 10,

				StatusEffectUpgrades = new List<StatusEffectStackData>
				{
					new StatusEffectStackData
					{
						statusId = "armor",
						count = 10
					}
				},

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.CardSpellPlayed,
						DescriptionKey = "Pony_Champion_Tantabus_PsychosisI_Trigger0_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								//EffectStateType = VanillaCardEffectTypes.CardEffectAddTempCardUpgradeToCardsInHand,
								EffectStateName = typeof(CharacterEffectAddTempCardUpgradeToCardsInHand).AssemblyQualifiedName,
								TargetMode = TargetMode.Hand,
								TargetCardType = CardType.Spell,

								ParamCardUpgradeData = new CardUpgradeDataBuilder
								{
									BonusDamage = 1,
									BonusHeal = 1,

									Filters = new List<CardUpgradeMaskData>
									{
										new CardUpgradeMaskDataBuilderFixed
										{
											DisallowedCardPools = new List<CardPool>
											{
												MyCardPools.PsychosisFilteredCards,
											},
											CardType = CardType.Spell,
											RequiredCardEffectsOperator = CardUpgradeMaskDataBuilder.CompareOperator.Or,
											RequiredCardEffects = new List<String>
											{
												"CardEffectDamage",
												"CardEffectHeal",
												"CardEffectHealAndDamageRelative",

                                                "UnofficialBalancePatch.CardEffects.CardEffectDamageRandomWithPriority, UnofficialBalancePatch, Version=2.0.3.0, Culture=neutral, PublicKeyToken=null",
                                                "UnofficialBalancePatch.CardEffects.CardEffectSacrificeAndDamageRelative, UnofficialBalancePatch, Version=2.0.3.0, Culture=neutral, PublicKeyToken=null"
                                            },
										}.Build(),
									}
								}.Build(),
							},
						}
					},
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.EndTurnPreHandDiscard,
						//DisplayEffectHintText = false,
						DescriptionKey = "Pony_Champion_Tantabus_PsychosisI_Trigger1_Key",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateName = typeof(CustomEffectFreezeFilteredCards).AssemblyQualifiedName,
								TargetCardType = CardType.Spell,
								TargetMode = TargetMode.Room,
								ParamCardFilter = new CardUpgradeMaskDataBuilderFixed
								{
									//DisallowedCardPools = new List<CardPool>
									//{
									//	MyCardPools.PsychosisFilteredCards,
									//},
									//CardType = CardType.Spell,
									//RequiredCardEffectsOperator = CardUpgradeMaskDataBuilder.CompareOperator.Or,
									//RequiredCardEffects = new List<String> 
									//{
									//	"CardEffectDamage",
									//	"CardEffectHeal",
									//	"CardEffectHealAndDamageRelative"
									//},
									AllowedCardPools = new List<CardPool>
									{
										MyCardPools.NightTerrorsPool,
									}
								}.Build(),
							}
						}
					}
				},
			};
		}

		public static CardUpgradeData Make()
		{
			return Psychosis_I.Builder().Build();
		}
	}
	internal class Psychosis_II
	{
		public static CardUpgradeDataBuilder Builder()
		{
			return new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = "Pony_Champion_Tantabus_PsychosisII_Title_Key",
				UpgradeDescriptionKey = "Pony_Champion_Tantabus_PsychosisII_Description_Key",
				UseUpgradeHighlightTextTags = true,
				BonusHP = 0,
				BonusDamage = 40,

				StatusEffectUpgrades = new List<StatusEffectStackData>
				{
					new StatusEffectStackData
					{
						statusId = "armor",
						count = 20
					}
				},

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.CardSpellPlayed,
						DescriptionKey = "Pony_Champion_Tantabus_PsychosisII_Trigger0_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								//EffectStateType = VanillaCardEffectTypes.CardEffectAddTempCardUpgradeToCardsInHand,
								EffectStateName = typeof(CharacterEffectAddTempCardUpgradeToCardsInHand).AssemblyQualifiedName,
								TargetMode = TargetMode.Hand,
								TargetCardType = CardType.Spell,

								ParamCardUpgradeData = new CardUpgradeDataBuilder
								{
									BonusDamage = 2,
									BonusHeal = 2,

									Filters = new List<CardUpgradeMaskData>
									{
										new CardUpgradeMaskDataBuilderFixed
										{
											DisallowedCardPools = new List<CardPool>
											{
												MyCardPools.PsychosisFilteredCards,
											},
                                            CardType = CardType.Spell,
											RequiredCardEffectsOperator = CardUpgradeMaskDataBuilder.CompareOperator.Or,
											RequiredCardEffects = new List<String>
											{
												"CardEffectDamage",
												"CardEffectHeal",
												"CardEffectHealAndDamageRelative",

                                                "UnofficialBalancePatch.CardEffects.CardEffectDamageRandomWithPriority, UnofficialBalancePatch, Version=2.0.3.0, Culture=neutral, PublicKeyToken=null",
                                                "UnofficialBalancePatch.CardEffects.CardEffectSacrificeAndDamageRelative, UnofficialBalancePatch, Version=2.0.3.0, Culture=neutral, PublicKeyToken=null"
                                            },
										}.Build(),
									}
								}.Build(),
							},
						}
					},
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.EndTurnPreHandDiscard,
						//DisplayEffectHintText = false,
						DescriptionKey = "Pony_Champion_Tantabus_PsychosisI_Trigger1_Key",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateName = typeof(CustomEffectFreezeFilteredCards).AssemblyQualifiedName,
								TargetCardType = CardType.Spell,
								TargetMode = TargetMode.Room,
								ParamCardFilter = new CardUpgradeMaskDataBuilderFixed
								{
									//DisallowedCardPools = new List<CardPool>
									//{
									//	MyCardPools.PsychosisFilteredCards,
									//},
									//CardType = CardType.Spell,
									//RequiredCardEffectsOperator = CardUpgradeMaskDataBuilder.CompareOperator.Or,
									//RequiredCardEffects = new List<String>
									//{
									//	"CardEffectDamage",
									//	"CardEffectHeal",
									//	"CardEffectHealAndDamageRelative"
									//},
									AllowedCardPools = new List<CardPool>
									{
										MyCardPools.NightTerrorsPool,
									}
								}.Build(),
							}
						}
					}
				},
			};
		}

		public static CardUpgradeData Make()
		{
			return Psychosis_II.Builder().Build();
		}
	}

	internal class Psychosis_III
	{
		public static CardUpgradeDataBuilder Builder()
		{
			return new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = "Pony_Champion_Tantabus_PsychosisIII_Title_Key",
				UpgradeDescriptionKey = "Pony_Champion_Tantabus_PsychosisIII_Description_Key",
				UseUpgradeHighlightTextTags = true,
				BonusHP = 0,
				BonusDamage = 100,

				StatusEffectUpgrades = new List<StatusEffectStackData>
				{
					new StatusEffectStackData
					{
						statusId = "armor",
						count = 40
					}
				},

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.CardSpellPlayed,
						DescriptionKey = "Pony_Champion_Tantabus_PsychosisIII_Trigger0_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								//EffectStateType = VanillaCardEffectTypes.CardEffectAddTempCardUpgradeToCardsInHand,
								EffectStateName = typeof(CharacterEffectAddTempCardUpgradeToCardsInHand).AssemblyQualifiedName,
								TargetMode = TargetMode.Hand,
								TargetCardType = CardType.Spell,

								ParamCardUpgradeData = new CardUpgradeDataBuilder
								{
									BonusDamage = 3,
									BonusHeal = 3,

									Filters = new List<CardUpgradeMaskData>
									{
										new CardUpgradeMaskDataBuilderFixed
										{
											DisallowedCardPools = new List<CardPool>
											{
												MyCardPools.PsychosisFilteredCards,
											},
                                            CardType = CardType.Spell,
											RequiredCardEffectsOperator = CardUpgradeMaskDataBuilder.CompareOperator.Or,
											RequiredCardEffects = new List<String>
											{
												"CardEffectDamage",
												"CardEffectHeal",
												"CardEffectHealAndDamageRelative",

                                                "UnofficialBalancePatch.CardEffects.CardEffectDamageRandomWithPriority, UnofficialBalancePatch, Version=2.0.3.0, Culture=neutral, PublicKeyToken=null",
                                                "UnofficialBalancePatch.CardEffects.CardEffectSacrificeAndDamageRelative, UnofficialBalancePatch, Version=2.0.3.0, Culture=neutral, PublicKeyToken=null"
                                            },
										}.Build(),
									}
								}.Build(),
							},
						}
					},
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.EndTurnPreHandDiscard,
						//DisplayEffectHintText = false,
						DescriptionKey = "Pony_Champion_Tantabus_PsychosisI_Trigger1_Key",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateName = typeof(CustomEffectFreezeFilteredCards).AssemblyQualifiedName,
								TargetCardType = CardType.Spell,
								TargetMode = TargetMode.Room,
								ParamCardFilter = new CardUpgradeMaskDataBuilderFixed
								{
									//DisallowedCardPools = new List<CardPool>
									//{
									//	MyCardPools.PsychosisFilteredCards,
									//},
									//CardType = CardType.Spell,
									//RequiredCardEffectsOperator = CardUpgradeMaskDataBuilder.CompareOperator.Or,
									//RequiredCardEffects = new List<String> 
									//{
									//	"CardEffectDamage",
									//	"CardEffectHeal",
									//	"CardEffectHealAndDamageRelative"
									//},
									AllowedCardPools = new List<CardPool>
									{
										MyCardPools.NightTerrorsPool,
									}
								}.Build(),
							}
						}
					}
				},
			};
		}

		public static CardUpgradeData Make()
		{
			return Psychosis_III.Builder().Build();
		}
	}
	internal class Nightmare_I
	{
		public static CardUpgradeDataBuilder Builder()
		{
			return new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = "Pony_Champion_Tantabus_NightmareI_Title_Key",
				UpgradeDescriptionKey = "Pony_Champion_Tantabus_NightmareI_Description_Key",
				UseUpgradeHighlightTextTags = true,
				BonusHP = 5,
				BonusDamage = 0,

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.PostCombat,
						DescriptionKey = "Pony_Champion_Tantabus_NightmareI_Trigger0_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateName = "CardEffectKill",
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters,
								TargetCharacterSubtype = SubtypePony.Key
							}
						}
					}
				},
				
				//This line is currently broken as previous slay trigger is not being properly removed when the next upgrade is picked.
				//Exiting and re-loading will clear the erroneous upgrade. 
				CardTriggerUpgradeBuilders = new List<CardTriggerEffectDataBuilder> 
				{
					new CardTriggerEffectDataBuilder
					{ 
						Trigger = CardTriggerType.OnKill,
						DescriptionKey = "Pony_Champion_Tantabus_NightmareI_Trigger1_Key",
						CardTriggerEffects = new List<CardTriggerData>
						{
							new CardTriggerEffectDataBuilder{ }.AddCardTrigger
							(
								PersistenceMode.SingleRun,
								"CardTriggerEffectBuffCharacterDamage",
								"None",
								3
							)
						}
					}
				}
			};
		}

		public static CardUpgradeData Make()
		{
			return Nightmare_I.Builder().Build();
		}
	}
	internal class Nightmare_II
	{
		public static CardUpgradeDataBuilder Builder()
		{
			return new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = "Pony_Champion_Tantabus_NightmareII_Title_Key",
				UpgradeDescriptionKey = "Pony_Champion_Tantabus_NightmareII_Description_Key",
				UseUpgradeHighlightTextTags = true,
				BonusHP = 10,
				BonusDamage = 0,

				UpgradesToRemove = new List<CardUpgradeData> 
				{ 
					Nightmare_I.Make(),
				},

				StatusEffectUpgrades = new List<StatusEffectStackData>
				{
					new StatusEffectStackData
					{
						statusId = VanillaStatusEffectIDs.Multistrike,
						count = 1
					}
				},

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.PostCombat,
						DescriptionKey = "Pony_Champion_Tantabus_NightmareII_Trigger0_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								//EffectStateType = VanillaCardEffectTypes.CardEffectDamageByUnitsKilled,
								EffectStateName = "CardEffectKill",
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters,
								TargetCharacterSubtype = SubtypePony.Key
							}
						}
					}
				},

				CardTriggerUpgradeBuilders = new List<CardTriggerEffectDataBuilder>
				{
					new CardTriggerEffectDataBuilder
					{
						Trigger = CardTriggerType.OnKill,
						DescriptionKey = "Pony_Champion_Tantabus_NightmareII_Trigger1_Key",
						CardTriggerEffects = new List<CardTriggerData>
						{
							new CardTriggerEffectDataBuilder{ }.AddCardTrigger
							(
								PersistenceMode.SingleRun,
								"CardTriggerEffectBuffCharacterDamage",
								"None",
								4
							)
						}
					}
				}
			};
		}

		public static CardUpgradeData Make()
		{
			return Nightmare_II.Builder().Build();
		}
	}
	internal class Nightmare_III
	{
		public static CardUpgradeDataBuilder Builder()
		{
			return new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = "Pony_Champion_Tantabus_NightmareIII_Title_Key",
				UpgradeDescriptionKey = "Pony_Champion_Tantabus_NightmareIII_Description_Key",
				UseUpgradeHighlightTextTags = true,
				BonusHP = 15,
				BonusDamage = 0,

				UpgradesToRemove = new List<CardUpgradeData>
				{
					Nightmare_II.Make(),
				},

				StatusEffectUpgrades = new List<StatusEffectStackData>
				{
					new StatusEffectStackData
					{
						statusId = VanillaStatusEffectIDs.Multistrike,
						count = 2
					}
				},

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.PostCombat,
						DescriptionKey = "Pony_Champion_Tantabus_NightmareIII_Trigger0_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								//EffectStateType = VanillaCardEffectTypes.CardEffectDamageByUnitsKilled,
								EffectStateName = "CardEffectKill",
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters,
								TargetCharacterSubtype = SubtypePony.Key
							}
						}
					}
				},
				
				CardTriggerUpgradeBuilders = new List<CardTriggerEffectDataBuilder>
				{
					new CardTriggerEffectDataBuilder
					{
						Trigger = CardTriggerType.OnKill,
						DescriptionKey = "Pony_Champion_Tantabus_NightmareIII_Trigger1_Key",
						CardTriggerEffects = new List<CardTriggerData>
						{
							new CardTriggerEffectDataBuilder{ }.AddCardTrigger
							(
								PersistenceMode.SingleRun,
								"CardTriggerEffectBuffCharacterDamage",
								"None",
								5
							)
						}
					}
				}
			};
		}

		public static CardUpgradeData Make()
		{
			return Nightmare_III.Builder().Build();
		}
	}
	internal class Lucid_I
	{
		public static CardUpgradeDataBuilder Builder()
		{
			return new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = "Pony_Champion_Tantabus_LucidI_Title_Key",
				UpgradeDescriptionKey = "Pony_Champion_Tantabus_LucidI_Description_Key",
				UseUpgradeHighlightTextTags = true,
				BonusHP = 15,
				BonusDamage = 0,
				
				RoomModifierUpgradeBuilders = new List<RoomModifierDataBuilder> 
				{ 
					new RoomModifierDataBuilder
					{ 
						DescriptionKey = "Pony_Champion_Tantabus_LucidI_Room_Key",
						RoomStateModifierClassName = "RoomStateEnergyModifier",
						ParamInt = 1,
						ParamStatusEffects = new StatusEffectStackData[]{ },
						ExtraTooltipTitleKey = "Pony_Champion_Tantabus_LucidI_Room_Title_Key",
						ExtraTooltipBodyKey = "Pony_Champion_Tantabus_LucidI_Room_Key",
					}
				},

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.OnTurnBegin,
						DescriptionKey = "Pony_Champion_Tantabus_LucidI_Trigger_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								//EffectStateType = VanillaCardEffectTypes.CardEffectRemoveStatusEffect,
								EffectStateName = "CardEffectRemoveStatusEffect",
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters,
								ParamStatusEffects = new StatusEffectStackData[]
								{
									new StatusEffectStackData
									{ 
										statusId = VanillaStatusEffectIDs.Sap,
										count = 999,
									}
								}
							},
						}
					}
				}
			};
		}

		public static CardUpgradeData Make()
		{
			return Lucid_I.Builder().Build();
		}
	}
	internal class Lucid_II
	{
		public static CardUpgradeDataBuilder Builder()
		{
			return new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = "Pony_Champion_Tantabus_LucidII_Title_Key",
				UpgradeDescriptionKey = "Pony_Champion_Tantabus_LucidII_Description_Key",
				UseUpgradeHighlightTextTags = true,
				BonusHP = 40,
				BonusDamage = 20,

				RoomModifierUpgradeBuilders = new List<RoomModifierDataBuilder>
				{
					new RoomModifierDataBuilder
					{
						DescriptionKey = "Pony_Champion_Tantabus_LucidII_Room_Key",
						RoomStateModifierClassName = "RoomStateEnergyModifier",
						ParamInt = 2,
						ParamStatusEffects = new StatusEffectStackData[]{ },
						ExtraTooltipTitleKey = "Pony_Champion_Tantabus_LucidII_Room_Title_Key",
						ExtraTooltipBodyKey = "Pony_Champion_Tantabus_LucidII_Room_Key",
					}
				},

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.OnTurnBegin,
						DescriptionKey = "Pony_Champion_Tantabus_LucidII_Trigger_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								//EffectStateType = VanillaCardEffectTypes.CardEffectRemoveStatusEffect,
								EffectStateName = "CardEffectRemoveStatusEffect",
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters,
								ParamStatusEffects = new StatusEffectStackData[]
								{
									new StatusEffectStackData
									{
										statusId = VanillaStatusEffectIDs.Sap,
										count = 999,
									}
								}
							},
						}
					}
				}
			};
		}

		public static CardUpgradeData Make()
		{
			return Lucid_II.Builder().Build();
		}
	}
	internal class Lucid_III
	{
		public static CardUpgradeDataBuilder Builder()
		{
			return new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = "Pony_Champion_Tantabus_LucidIII_Title_Key",
				UpgradeDescriptionKey = "Pony_Champion_Tantabus_LucidIII_Description_Key",
				UseUpgradeHighlightTextTags = true,
				BonusHP = 90,
				BonusDamage = 60,

				RoomModifierUpgradeBuilders = new List<RoomModifierDataBuilder>
				{
					new RoomModifierDataBuilder
					{
						DescriptionKey = "Pony_Champion_Tantabus_LucidIII_Room_Key",
						RoomStateModifierClassName = "RoomStateEnergyModifier",
						ParamInt = 3,
						ParamStatusEffects = new StatusEffectStackData[]{ },
						ExtraTooltipTitleKey = "Pony_Champion_Tantabus_LucidIII_Room_Title_Key",
						ExtraTooltipBodyKey = "Pony_Champion_Tantabus_LucidIII_Room_Key",
					}
				},

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.OnTurnBegin,
						DescriptionKey = "Pony_Champion_Tantabus_LucidIII_Trigger_Key",
						HideTriggerTooltip = false,
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								//EffectStateType = VanillaCardEffectTypes.CardEffectRemoveStatusEffect,
								EffectStateName = "CardEffectRemoveStatusEffect",
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters,
								ParamStatusEffects = new StatusEffectStackData[]
								{
									new StatusEffectStackData
									{
										statusId = VanillaStatusEffectIDs.Sap,
										count = 999,
									}
								}
							},
						}
					}
				}
			};
		}

		public static CardUpgradeData Make()
		{
			return Lucid_III.Builder().Build();
		}
	}
}
