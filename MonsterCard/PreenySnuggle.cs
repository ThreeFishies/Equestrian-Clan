using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Trainworks.Builders;
using Trainworks.Managers;
using Trainworks.Constants;
using System.Linq;
using UnityEngine;
using Trainworks.Utilities;
using Equestrian.Init;
using CustomEffects;
using Equestrian;

namespace Equestrian.MonsterCards
{
    class PreenySnuggle
    {
        public static readonly string ID = Ponies.GUID + "_PreenySnugglesCard";
        public static readonly string CharID = Ponies.GUID + "_PreenySnugglesCharacter";
        public static CardUpgradeData PreenySnugglesSynthesis;

        //I mean, I just assumed this would work
        //public static CharacterData PreenySnuggleCharacterData = new CharacterData { };
        //public static CardData PreenySnuggleCardData = new CardData { };

        public static void BuildAndRegister()
        {
            CharacterData PreenySnuggleCharacterData = new CharacterDataBuilder
            {
                CharacterID = CharID,
                NameKey = "Pony_Unit_PreenySnuggle_Name_Key",
                Size = 1,
                Health = 8,
                AttackDamage = 8,
                PriorityDraw = true,
                AssetPath = "MonsterAssets/PreenySnuggleUnit.png",
                SubtypeKeys = new List<string> { SubtypePony.Key },
                CharacterChatterData = new CharacterChatterDataBuilder() 
                {
                    name = "Preeny Snuggles chatter data",
                    gender = CharacterChatterData.Gender.Female,
                    characterAddedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_PreenySnuggle_Chatter_Added_1_Key",
                    },
                    characterAttackingExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_PreenySnuggle_Chatter_Attacking_1_Key",
                    },
                    characterSlayedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_PreenySnuggle_Chatter_Slayed_1_Key",
                    },
                    characterIdleExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_PreenySnuggle_Chatter_Idle_1_Key",
                        "Pony_Unit_PreenySnuggle_Chatter_Idle_2_Key",
                        "Pony_Unit_PreenySnuggle_Chatter_Idle_3_Key",
                        "Pony_Unit_PreenySnuggle_Chatter_Idle_4_Key",
                        "Pony_Unit_PreenySnuggle_Chatter_Idle_5_Key",
                        "Pony_Unit_PreenySnuggle_Chatter_Idle_6_Key",
                        "Pony_Unit_PreenySnuggle_Chatter_Idle_7_Key",
                        "Pony_Unit_PreenySnuggle_Chatter_Idle_8_Key",
                        "Pony_Unit_PreenySnuggle_Chatter_Idle_9_Key"
                    }
                }.Build(),

                StartingStatusEffects = new StatusEffectStackData[]
                {
                    new StatusEffectStackData()
                    {
                        statusId = "social",
                        count = 1
                    }
                },

                TriggerBuilders = new List<CharacterTriggerDataBuilder>
                {
                    new CharacterTriggerDataBuilder
                    {
                        trigger = CharacterTriggerData.Trigger.OnUnscaledSpawn,
                        HideTriggerTooltip = true,
                        DescriptionKey = "Pony_Unit_PreenySnuggle_Description_Key",
                        
                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateName = typeof(CustomEffectSocial).AssemblyQualifiedName,
                                HideTooltip = true,
                                TargetMode = TargetMode.Self,
                            },

                            new CardEffectDataBuilder 
                            {
                                EffectStateName = "CardEffectAddTempCardUpgradeToUnits",
                                TargetMode = TargetMode.Self,
                                ParamCardUpgradeData = new CardUpgradeDataBuilder
                                {
                                    UpgradeTitle = "PreenySnuggles_title",
                                    BonusDamage = 0,
                                    BonusHP = 0,
                                    CostReduction = 0,
                                    XCostReduction = 0,
                                    BonusHeal = 0,
                                    BonusSize = 0,
                                }.Build(),
                            },
                        },
                    },
                },
                
            }.BuildAndRegister();

            CardData PreenySnuggleCardData = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Unit_PreenySnuggle_Name_Key",
                Cost = 0,
                CostType = CardData.CostType.ConsumeRemainingEnergy,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Uncommon,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "MonsterAssets/PreenySnuggleCard.png",
                ClanID = EquestrianClan.ID,
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.UnitsAllBanner },
                //CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string>
                {
                    "Pony_Unit_PreenySnuggle_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,
                UnlockLevel = 5,

                TraitBuilders = new List<CardTraitDataBuilder>
                {
                    new CardTraitDataBuilder
                    {
                        TraitStateName = "CardTraitScalingUpgradeUnitAttack",
                        ParamTrackedValue = CardStatistics.TrackedValueType.PlayedCost,
                        ParamEntryDuration = CardStatistics.EntryDuration.ThisTurn,
                        ParamUseScalingParams = true,
                        ParamInt = 8,
                        ParamFloat = 1.0f,
                        ParamTeamType = Team.Type.Monsters,
                    },

                    new CardTraitDataBuilder
                    {
                        TraitStateName = "CardTraitScalingUpgradeUnitHealth",
                        ParamTrackedValue = CardStatistics.TrackedValueType.PlayedCost,
                        ParamEntryDuration = CardStatistics.EntryDuration.ThisTurn,
                        ParamUseScalingParams = true,
                        ParamInt = 8,
                        ParamFloat = 1.0f,
                        ParamTeamType = Team.Type.Monsters
                    },

                    new CardTraitDataBuilder
                    {
                        //What is this? Why does Kinhost Carapace have it?
                        TraitStateName = "CardTraitLevelMonsterState",
                    }
                },

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterData = PreenySnuggleCharacterData,
                        EffectStateName = "CardEffectSpawnMonster"
                    }
                }
            }.BuildAndRegister();

            //Note: This only creates the essence but doesn't register it. That must be done seperately.
            PreenySnugglesSynthesis = new CardUpgradeDataBuilder
            {
                UpgradeTitle = "PreenySnuggleEssence",
                SourceSynthesisUnit = PreenySnuggleCharacterData,
                UpgradeDescriptionKey = "Pony_Unit_PreenySnuggle_Essence_Key",
                BonusHP = 8,
                BonusDamage = 8,
                CostReduction = -1,
                //XCostReduction = -1,

                TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
                {
                    new CharacterTriggerDataBuilder
                    {
                        Trigger = CharacterTriggerData.Trigger.OnUnscaledSpawn,
                        HideTriggerTooltip = true,

                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateName = typeof(CustomEffectSocial).AssemblyQualifiedName,
                                HideTooltip = true,
                                TargetMode = TargetMode.Self,
                            },
                        }
                    },
                    new CharacterTriggerDataBuilder
                    {
                        Trigger = CharacterTriggerData.Trigger.OnSpawn,

                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateName = "CardEffectAddTempCardUpgradeToUnits",
                                TargetMode = TargetMode.Self,
                                TargetTeamType = Team.Type.Monsters,
                                ParamCardUpgradeData = new CardUpgradeDataBuilder
                                {
                                    UpgradeTitle = "PrennySnuggles_Essence_Summon_trigger",
                                    BonusDamage = 8,
                                    BonusHP = 8,
                                    SourceSynthesisUnit = ScriptableObject.CreateInstance<CharacterData>(),
                                }.Build(),
                            }
                        }
                    }
                },

                StatusEffectUpgrades = new List<StatusEffectStackData>
                {
                    new StatusEffectStackData()
                    {
                        statusId = "social",
                        count = 1
                    }
                }

            }.Build();
        }
    }
}