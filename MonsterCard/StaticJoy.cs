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
//using MonsterTrainTestMod.Clans;
using Equestrian;

namespace Equestrian.MonsterCards
{
    class StaticJoy
    {
        public static readonly string ID = Ponies.GUID + "_StaticJoyCard";
        public static readonly string CharID = Ponies.GUID + "_StaticJoyCharacter";
        public static CardUpgradeData StaticJoySynthesis;

        //If things are messed up it helps to check the logs every once in a while
        //public static CharacterData StaticJoyCharacterData = new CharacterData { };
        //public static CardData StaticJoyCardData = new CardData { };

        public static void BuildAndRegister()
        {
            CharacterData StaticJoyCharacterData = new CharacterDataBuilder
            {
                CharacterID = CharID,
                NameKey = "Pony_Unit_StaticJoy_Name_Key",
                Size = 1,
                Health = 5,
                AttackDamage = 5,
                PriorityDraw = true,
                AssetPath = "MonsterAssets/StaticJoyUnit.png",
                SubtypeKeys = new List<string> { SubtypePony.Key },
                CharacterChatterData = new CharacterChatterDataBuilder() 
                {
                    name = "Static Joy chatter data",

                    gender = CharacterChatterData.Gender.Female,

                    characterAddedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_StaticJoy_Chatter_Added_1_Key",
                    },

                    characterAttackingExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_StaticJoy_Chatter_Attacking_1_Key",
                    },

                    characterSlayedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_StaticJoy_Chatter_Slayed_1_Key",
                    },

                    characterIdleExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_StaticJoy_Chatter_Idle_1_Key",
                        "Pony_Unit_StaticJoy_Chatter_Idle_2_Key",
                        "Pony_Unit_StaticJoy_Chatter_Idle_3_Key",
                        "Pony_Unit_StaticJoy_Chatter_Idle_4_Key",
                        "Pony_Unit_StaticJoy_Chatter_Idle_5_Key",
                        "Pony_Unit_StaticJoy_Chatter_Idle_6_Key",
                        "Pony_Unit_StaticJoy_Chatter_Idle_7_Key",
                        "Pony_Unit_StaticJoy_Chatter_Idle_8_Key",
                    },

                    characterTriggerExpressionKeys = new List<CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys>()
                    { 
                        new CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys()
                        {
                            Key = "Pony_Unit_StaticJoy_Chatter_Trigger_OnAttacking_1_Key",
                            Trigger = CharacterTriggerData.Trigger.OnAttacking,
                        }
                    }
                }.Build(),

                TriggerBuilders = new List<CharacterTriggerDataBuilder>
                {
                    new CharacterTriggerDataBuilder
                    {
                        trigger = CharacterTriggerData.Trigger.OnAttacking,
                        DescriptionKey = "Pony_Unit_StaticJoy_Description_Key",

                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateName = "CardEffectAddStatusEffect",
                                TargetMode = TargetMode.Room,
                                TargetTeamType = Team.Type.Monsters,
                                ParamStatusEffects = new StatusEffectStackData[]
                                {
                                    new StatusEffectStackData
                                    {
                                        statusId = "armor",
                                        count = 2
                                    }
                                }
                            }
                        }
                    }
                },
                
                StartingStatusEffects = new StatusEffectStackData[] 
                { 
                    new StatusEffectStackData
                    { 
                        statusId = "armor",
                        count = 10
                    },
                    new StatusEffectStackData
                    {
                        statusId = VanillaStatusEffectIDs.Trample,
                        count = 1
                    }
                }
            }.BuildAndRegister();

            CardData StaticJoyCardData = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Unit_StaticJoy_Name_Key",
                Cost = 2,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Uncommon,
                //Description = "<b>Action</b>: Apply {[effect0.status0.power]} <b>Armor</b> to all friendly units.",
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "MonsterAssets/StaticJoyCard.png",
                ClanID = EquestrianClan.ID,
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.UnitsAllBanner },
                //CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string> 
                { 
                    "Pony_Unit_StaticJoy_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterData = StaticJoyCharacterData,
                        EffectStateName = "CardEffectSpawnMonster"
                    }
                }
            }.BuildAndRegister();

            //Note: This only creates the essence but doesn't register it. That must be done seperately.
            StaticJoySynthesis = new CardUpgradeDataBuilder
            {
                UpgradeTitle = "StaticJoyEssence",
                SourceSynthesisUnit = StaticJoyCharacterData,
                UpgradeDescriptionKey = "Pony_Unit_StaticJoy_Essence_Key",
                BonusHP = 0,
                BonusDamage = 0,

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
                        trigger = CharacterTriggerData.Trigger.OnAttacking,
                        DescriptionKey = "Pony_Unit_StaticJoy_Description_Key",
                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateName = "CardEffectAddStatusEffect",
                                TargetMode = TargetMode.Room,
                                TargetTeamType = Team.Type.Monsters,
                                ParamStatusEffects = new StatusEffectStackData[]
                                {
                                    new StatusEffectStackData
                                    {
                                        statusId = "armor",
                                        count = 2
                                    }
                                }
                            }
                        }
                    }
                }
            }.Build();
        }
    }
}