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
using Equestrian.Champions;

namespace Equestrian.MonsterCards
{
    class Finchy
    {
        public static readonly string ID = Ponies.GUID + "_FinchyCard";
        public static readonly string CharID = Ponies.GUID + "_FinchyCharacter";
        public static CardUpgradeData FinchySynthesis;

        public static void BuildAndRegister()
        {
            CharacterData FinchyCharacterData = new CharacterDataBuilder
            {
                CharacterID = CharID,
                NameKey = "Pony_Unit_Finchy_Name_Key",
                Size = 1,
                Health = 1,
                AttackDamage = 1,
                PriorityDraw = false,
                AssetPath = "MonsterAssets/FinchyUnit.png",
                SubtypeKeys = new List<string> { SubtypePony.Key },
                CharacterChatterData = new CharacterChatterDataBuilder() 
                {
                    name = "Finchy chatter data",

                    gender = CharacterChatterData.Gender.Female,

                    characterAddedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_Finchy_Chatter_Added_1_Key"
                    },

                    characterAttackingExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_Finchy_Chatter_Attacking_1_Key"
                    },

                    characterSlayedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_Finchy_Chatter_Slayed_1_Key"
                    },

                    characterIdleExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_Finchy_Chatter_Idle_1_Key",
                        "Pony_Unit_Finchy_Chatter_Idle_2_Key",
                        "Pony_Unit_Finchy_Chatter_Idle_3_Key",
                        "Pony_Unit_Finchy_Chatter_Idle_4_Key",
                        "Pony_Unit_Finchy_Chatter_Idle_5_Key",
                        "Pony_Unit_Finchy_Chatter_Idle_6_Key",
                        "Pony_Unit_Finchy_Chatter_Idle_7_Key",
                        "Pony_Unit_Finchy_Chatter_Idle_8_Key"
                    },

                    characterTriggerExpressionKeys = new List<CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys>() 
                    {
                        new CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys()
                        {
                            Key = "Pony_Unit_Finchy_Chatter_Trigger_OnAttacking_1_Key",
                            Trigger = CharacterTriggerData.Trigger.OnAttacking
                        }
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
                        Trigger = CharacterTriggerData.Trigger.OnAttacking,
                        DescriptionKey = "Pony_Unit_Finchy_Description_Key",
                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                //EffectStateType = VanillaCardEffectTypes.CardEffectRewardGold,
                                EffectStateName = "CardEffectRewardGold",
                                ParamInt = 5
                            }
                        }
                    }
                }

            }.BuildAndRegister();

            CardData FinchyCardData = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Unit_Finchy_Name_Key",
                Cost = 0,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "MonsterAssets/FinchyCard.png",
                ClanID = EquestrianClan.ID,
                CardPoolIDs = new List<string> {  },
                CardLoreTooltipKeys = new List<string>
                {
                    "Pony_Unit_Finchy_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,
                IgnoreWhenCountingMastery = true,

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterData = FinchyCharacterData,
                        EffectStateName = "CardEffectSpawnMonster"
                    }
                }
            }.BuildAndRegister();

            //Note: This only creates the essence but doesn't register it. That must be done seperately.
            FinchySynthesis = new CardUpgradeDataBuilder
            {
                UpgradeTitle = "FinchyEssence",
                SourceSynthesisUnit = FinchyCharacterData,
                UpgradeDescriptionKey = "Pony_Unit_Finchy_Essence_Key",
                BonusHP = 0,
                BonusDamage = 0
            }.Build();
        }
    }
}