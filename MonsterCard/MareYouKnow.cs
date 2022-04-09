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
    class MareYouKnow
    {
        public static readonly string ID = Ponies.GUID + "_MareYouKnowCard";
        public static readonly string CharID = Ponies.GUID + "_MareYouKnowCharacter";
        public static CardUpgradeData MareYouKnowSynthesis;

        //Epic Fail
        //public static CharacterData MareYouKnowCharacterData = new CharacterData { };
        //public static CardData MareYouKnowCardData = new CardData { };

        public static void BuildAndRegister()
        {
            CharacterData MareYouKnowCharacterData = new CharacterDataBuilder
            {
                CharacterID = CharID,
                NameKey = "Pony_Unit_MareYouKnow_Name_Key",
                Size = 1,
                Health = 8,
                AttackDamage = 3,
                PriorityDraw = true,
                AssetPath = "MonsterAssets/MareYouKnowUnit.png",
                SubtypeKeys = new List<string> { SubtypePony.Key },
                CharacterChatterData = new CharacterChatterDataBuilder() 
                {
                    name = "Mare You Know chatter data",

                    gender = CharacterChatterData.Gender.Female,

                    characterAddedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_MareYouKnow_Chatter_Added_1_Key",
                    },

                    characterAttackingExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_MareYouKnow_Chatter_Attacking_1_Key",
                    },

                    characterSlayedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_MareYouKnow_Chatter_Slayed_1_Key",
                    },

                    characterIdleExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_MareYouKnow_Chatter_Idle_1_Key",
                        "Pony_Unit_MareYouKnow_Chatter_Idle_2_Key",
                        "Pony_Unit_MareYouKnow_Chatter_Idle_3_Key",
                        "Pony_Unit_MareYouKnow_Chatter_Idle_4_Key",
                        "Pony_Unit_MareYouKnow_Chatter_Idle_5_Key",
                        "Pony_Unit_MareYouKnow_Chatter_Idle_6_Key",
                        "Pony_Unit_MareYouKnow_Chatter_Idle_7_Key",
                        "Pony_Unit_MareYouKnow_Chatter_Idle_8_Key",
                        "Pony_Unit_MareYouKnow_Chatter_Idle_9_Key",
                        "Pony_Unit_MareYouKnow_Chatter_Idle_10_Key",
                        "Pony_Unit_MareYouKnow_Chatter_Idle_11_Key",
                        "Pony_Unit_MareYouKnow_Chatter_Idle_12_Key",
                        "Pony_Unit_MareYouKnow_Chatter_Idle_13_Key",
                        "Pony_Unit_MareYouKnow_Chatter_Idle_14_Key",
                    },

                    characterTriggerExpressionKeys = new List<CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys>() 
                    { 
                        new CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys()
                        {
                            Key = "Pony_Unit_MareYouKnow_Chatter_Trigger_PostCombat_1_Key",
                            Trigger = CharacterTriggerData.Trigger.PostCombat,
                        }
                    }
                }.Build(),

                TriggerBuilders = new List<CharacterTriggerDataBuilder>
                {
                    new CharacterTriggerDataBuilder
                    {
                        trigger = CharacterTriggerData.Trigger.PostCombat,
                        DescriptionKey = "Pony_Unit_MareYouKnow_Description_Key",

                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                //EffectStateType = VanillaCardEffectTypes.CardEffectAddTempCardUpgradeToNextDrawnCard,
                                EffectStateName = "CardEffectAddTempCardUpgradeToNextDrawnCard",
                                ParamCardUpgradeData = new CardUpgradeDataBuilder
                                { 
                                    CostReduction = 1,
                                    XCostReduction = 1
                                }.Build(),
                            },

                            new CardEffectDataBuilder 
                            {
                                EffectStateName = "CardEffectDraw",
                                ParamInt = 1
                            },

                            //Note: Temp upgrades are normally only removed when a card is played. To prevent this from effecting your entire hand, a Harmony patch is needed.
                            //See: RemoveTempUpgradeToCardsAfterDrawingCards
                        }
                    }
                },
                
                StartingStatusEffects = new StatusEffectStackData[] 
                { 
                    new StatusEffectStackData
                    { 
                        statusId = "armor",
                        count = 5
                    }
                }
            }.BuildAndRegister();

            CardData MareYouKnowCardData = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Unit_MareYouKnow_Name_Key",
                Cost = 1,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Uncommon,
                //Description = "<b>Action</b>: Apply {[effect0.status0.power]} <b>Armor</b> to all friendly units.",
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "MonsterAssets/MareYouKnowCard.png",
                ClanID = EquestrianClan.ID,
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.UnitsAllBanner },
                //CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Unit_MareYouKnow_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,
                UnlockLevel = 5,

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterData = MareYouKnowCharacterData,
                        EffectStateName = "CardEffectSpawnMonster"
                    }
                }
            }.BuildAndRegister();

            //Note: This only creates the essence but doesn't register it. That must be done seperately.
            MareYouKnowSynthesis = new CardUpgradeDataBuilder
            {
                UpgradeTitle = "MareYouKnowEssence",
                SourceSynthesisUnit = MareYouKnowCharacterData,
                UpgradeDescriptionKey = "Pony_Unit_MareYouKnow_Essence_Key",
                BonusHP = 8,
                BonusDamage = 3,

                TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
                {
                    new CharacterTriggerDataBuilder
                    {
                        trigger = CharacterTriggerData.Trigger.PostCombat,
                        DescriptionKey = "Pony_Unit_MareYouKnow_Description_Key",

                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                //EffectStateType = VanillaCardEffectTypes.CardEffectAddTempCardUpgradeToNextDrawnCard,
                                EffectStateName = "CardEffectAddTempCardUpgradeToNextDrawnCard",
                                TargetMode = TargetMode.Room,
                                TargetTeamType = Team.Type.None,
                                ParamCardUpgradeData = new CardUpgradeDataBuilder
                                {
                                    UpgradeTitle = "MareYouKnow_title",
                                    CostReduction = 1,
                                    XCostReduction = 1
                                }.Build(),
                            },

                            new CardEffectDataBuilder
                            {
                                EffectStateName = "CardEffectDraw",
                                ParamInt = 1
                            },

                            //Note: Temp upgrades are normally only removed when a card is played. To prevent this from effecting your entire hand, a Harmony patch is needed.
                            //See: RemoveTempUpgradeToCardsAfterDrawingCards
                        }
                    }
                },
            }.Build();
        }
    }
}