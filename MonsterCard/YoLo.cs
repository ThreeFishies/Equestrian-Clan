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
    class YoLo
    {
        public static readonly string ID = Ponies.GUID + "_YoLoCard";
        public static readonly string CharID = Ponies.GUID + "_YoLoCharacter";
        public static CardUpgradeData YoLoSynthesis;

        public static void BuildAndRegister()
        {
            CharacterData YoLoCharacterData = new CharacterDataBuilder
            {
                CharacterID = CharID,
                NameKey = "Pony_Unit_YoLo_Name_Key",
                Size = 1,
                Health = 1,
                AttackDamage = 1,
                PriorityDraw = false,
                AssetPath = "MonsterAssets/YoLoUnit.png",
                SubtypeKeys = new List<string> { SubtypePony.Key },
                CharacterChatterData = new CharacterChatterDataBuilder() 
                {
                    name = "Yo Lo chatter data",

                    gender = CharacterChatterData.Gender.Female,

                    characterAddedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_YoLo_Chatter_Added_1_Key"
                    },

                    characterAttackingExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_YoLo_Chatter_Attacking_1_Key"
                    },

                    characterSlayedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_YoLo_Chatter_Slayed_1_Key"
                    },

                    characterIdleExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_YoLo_Chatter_Idle_1_Key",
                        "Pony_Unit_YoLo_Chatter_Idle_2_Key",
                        "Pony_Unit_YoLo_Chatter_Idle_3_Key",
                        "Pony_Unit_YoLo_Chatter_Idle_4_Key",
                        "Pony_Unit_YoLo_Chatter_Idle_5_Key",
                        "Pony_Unit_YoLo_Chatter_Idle_6_Key",
                        "Pony_Unit_YoLo_Chatter_Idle_7_Key",
                        "Pony_Unit_YoLo_Chatter_Idle_8_Key",
                        "Pony_Unit_YoLo_Chatter_Idle_9_Key"
                    }
                }.Build(),

                StartingStatusEffects = new StatusEffectStackData[] 
                {
                    new StatusEffectStackData
                    {
                        statusId = VanillaStatusEffectIDs.Armor,
                        count = 5
                    }
                },

                TriggerBuilders = new List<CharacterTriggerDataBuilder>
                {
                    new CharacterTriggerDataBuilder
                    {
                        trigger = CharacterTriggerData.Trigger.OnUnscaledSpawn,
                        HideTriggerTooltip = true,
                        DescriptionKey = "Pony_Unit_YoLo_Description_Key",
                        
                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder 
                            {
                                EffectStateName = "CardEffectAddStatusEffect",
                                TargetMode = TargetMode.Self,
                                TargetTeamType = Team.Type.Monsters,

                                ParamStatusEffects = new StatusEffectStackData[]
                                {
                                    new StatusEffectStackData
                                    {
                                        statusId = VanillaStatusEffectIDs.Multistrike,
                                        count = 0
                                    }
                                }
                            },
                        },
                    },
                },
                
            }.BuildAndRegister();

            CardData YoLoCardData = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Unit_YoLo_Name_Key",
                Cost = 0,
                CostType = CardData.CostType.ConsumeRemainingEnergy,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "MonsterAssets/YoLoCard.png",
                ClanID = EquestrianClan.ID,
                CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Unit_YoLo_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,
                IgnoreWhenCountingMastery = true,

                TraitBuilders = new List<CardTraitDataBuilder> 
                { 
                    new CardTraitDataBuilder
                    {
                        TraitStateName = "CardTraitScalingAddStatusEffect",
                        ParamTrackedValue = CardStatistics.TrackedValueType.PlayedCost,
                        ParamEntryDuration = CardStatistics.EntryDuration.ThisBattle,
                        ParamStr = "",
                        ParamUseScalingParams = true,
                        ParamInt = 1,
                        ParamFloat = 1.0f,
                        ParamTeamType = Team.Type.Monsters,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                            new StatusEffectStackData()
                            {
                                statusId = VanillaStatusEffectIDs.Multistrike,
                                count = 0
                            }
                        }
                    }
                },

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterData = YoLoCharacterData,
                        EffectStateName = "CardEffectSpawnMonster"
                    }
                }
            }.BuildAndRegister();

            //Note: This only creates the essence but doesn't register it. That must be done seperately.
            YoLoSynthesis = new CardUpgradeDataBuilder
            {
                UpgradeTitle = "YoLoEssence",
                SourceSynthesisUnit = YoLoCharacterData,
                UpgradeDescriptionKey = "Pony_Unit_YoLo_Essence_Key",
                BonusHP = 0,
                BonusDamage = 0,
                CostReduction = 0,

            }.Build();
        }
    }
}