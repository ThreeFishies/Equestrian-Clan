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
using Equestrian.Champions;

namespace Equestrian.MonsterCards
{
    class LordOfEmber
    {
        public static readonly string ID = Ponies.GUID + "_LordOfEmberCard";
        public static readonly string CharID = Ponies.GUID + "_LordOfEmberCharacter";
        public static CardUpgradeData LordOfEmberSynthesis;

        public static void BuildAndRegister()
        {
            CharacterData LordOfEmberCharacterData = new CharacterDataBuilder
            {
                CharacterID = CharID,
                NameKey = "Pony_Unit_LordOfEmber_Name_Key",
                Size = 3,
                Health = 35,
                AttackDamage = 50,
                PriorityDraw = true,
                AssetPath = "MonsterAssets/LordOfEmberUnit.png",
                SubtypeKeys = new List<string> { SubtypeDragon.Key },
                CharacterChatterData = new CharacterChatterDataBuilder() 
                {
                    name = "Jestember chatter data",

                    gender = CharacterChatterData.Gender.Male,

                    characterAddedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_LordOfEmber_Chatter_Added_1_Key"
                    },

                    characterAttackingExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_LordOfEmber_Chatter_Attacking_1_Key"
                    },

                    characterSlayedExpressionKeys = new List<string>()
                    {
                        "Pony_Unit_LordOfEmber_Chatter_Slayed_1_Key"
                    },

                    characterIdleExpressionKeys = new List<string>()
                    {
                        "Pony_Unit_LordOfEmber_Chatter_Idle_1_Key",
                        "Pony_Unit_LordOfEmber_Chatter_Idle_2_Key",
                        "Pony_Unit_LordOfEmber_Chatter_Idle_3_Key",
                        "Pony_Unit_LordOfEmber_Chatter_Idle_4_Key",
                    },
                    
                    characterTriggerExpressionKeys = new List<CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys>() 
                    {
                        new CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys()
                        {
                            Key = "Pony_Unit_LordOfEmber_Chatter_Trigger_OnKill_1_Key",
                            Trigger = CharacterTriggerData.Trigger.OnKill
                        }
                    }
                }.Build(),
                
                StartingStatusEffects = new StatusEffectStackData[]
                { 
                    new StatusEffectStackData
                    { 
                        statusId = VanillaStatusEffectIDs.Sweep,
                        count = 1
                    }
                },

                TriggerBuilders = new List<CharacterTriggerDataBuilder> 
                { 
                    new CharacterTriggerDataBuilder
                    {
                        Trigger = CharacterTriggerData.Trigger.OnKill,
                        DescriptionKey = "Pony_Unit_LordOfEmber_Description_Key",
                        EffectBuilders = new List<CardEffectDataBuilder>
                        { 
                            new CardEffectDataBuilder
                            {
                                EffectStateName = "CardEffectGainEnergy",
                                ParamInt = 1
                            }
                        }
                    }
                }
            }.BuildAndRegister();

            CardData LordOfEmberCardData = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Unit_LordOfEmber_Name_Key",
                Cost = 4,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Rare,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "MonsterAssets/LordOfEmberCard.png",
                ClanID = EquestrianClan.ID,
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.UnitsAllBanner },
                //CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string>
                {
                    "Pony_Unit_LordOfEmber_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,
                UnlockLevel = 4,

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterData = LordOfEmberCharacterData,
                        EffectStateName = "CardEffectSpawnMonster"
                    }
                }
            }.BuildAndRegister();

            //Note: This only creates the essence but doesn't register it. That must be done seperately.
            LordOfEmberSynthesis = new CardUpgradeDataBuilder
            {
                UpgradeTitle = "LordOfEmberEssence",
                SourceSynthesisUnit = LordOfEmberCharacterData,
                UpgradeDescriptionKey = "Pony_Unit_LordOfEmber_Essence_Key",
                BonusHP = 0,
                BonusDamage = 10,
                CostReduction = 0,
                BonusSize = 0,

                TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder> 
                { 
                    new CharacterTriggerDataBuilder
                    { 
                        Trigger = CharacterTriggerData.Trigger.OnUnscaledSpawn,
                        DescriptionKey = "Pony_Unit_LordOfEmber_Description_Key",
                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateName = typeof(CustomCardEffectAddStatusImmunity).AssemblyQualifiedName,
                                TargetMode = TargetMode.Self,
                                TargetTeamType = Team.Type.Monsters,
                                ParamStatusEffects = new StatusEffectStackData[]
                                {
                                    new StatusEffectStackData
                                    {
                                        statusId = VanillaStatusEffectIDs.Sap,
                                        count = 1
                                    }
                                }
                            },
                            new CardEffectDataBuilder
                            {
                                EffectStateName = typeof(CustomCardEffectAddStatusImmunity).AssemblyQualifiedName,
                                TargetMode = TargetMode.Self,
                                TargetTeamType = Team.Type.Monsters,
                                ParamStatusEffects = new StatusEffectStackData[]
                                {
                                    new StatusEffectStackData
                                    {
                                        statusId = VanillaStatusEffectIDs.Emberdrain,
                                        count = 1
                                    }
                                }
                            },
                            new CardEffectDataBuilder
                            {
                                EffectStateName = "CardEffectRemoveStatusEffect",
                                TargetMode = TargetMode.Self,
                                TargetTeamType = Team.Type.Monsters,
                                ParamStatusEffects = new StatusEffectStackData[]
                                {
                                    new StatusEffectStackData
                                    {
                                        statusId = VanillaStatusEffectIDs.Emberdrain,
                                        count = 0
                                    }
                                }
                            },
                            new CardEffectDataBuilder
                            {
                                EffectStateName = "CardEffectRemoveStatusEffect",
                                TargetMode = TargetMode.Self,
                                TargetTeamType = Team.Type.Monsters,
                                ParamStatusEffects = new StatusEffectStackData[]
                                {
                                    new StatusEffectStackData
                                    {
                                        statusId = VanillaStatusEffectIDs.Sap,
                                        count = 0
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