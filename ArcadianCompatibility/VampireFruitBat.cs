using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Equestrian.Init;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Enums;
using Equestrian;
using CustomEffects;
using HarmonyLib;

namespace Equestrian.Arcadian
{
    class VampireFruitBat
    {
        public static readonly string ID = Ponies.GUID + "_VampireFruitBatCard";
        public static readonly string CharID = Ponies.GUID + "_VampireFruitBatCharacter";
        public static readonly string CardPoolID = Ponies.GUID + "_VampireFruitBatCardPool";
        public static CardUpgradeData VampireFruitBatSynthesis;
        public static CardData VampireFruitBatCardData;

        public static void BuildAndRegister()
        {
            CharacterData VampireFruitBatCharacter = new CharacterDataBuilder
            {
                CharacterID = CharID,
                NameKey = "Arcadian_Unit_VampireFruitBat_Name_Key",
                Size = 1,
                Health = 1,
                AttackDamage = 3,
                PriorityDraw = false,
                AssetPath = "ArcadianCompatibility/Assets/VampireFruitBatCharacter.png",
                SubtypeKeys = new List<string> { "ChronoSubtype_Seer" },
                CharacterChatterData = new CharacterChatterDataBuilder()
                {
                    name = "Vampire Fruit Bat chatter data",

                    gender = CharacterChatterData.Gender.Male,

                    characterAddedExpressionKeys = new List<string>()
                    {
                        "Arcadian_Unit_VampireFruitBat_Chatter_Added_1_Key"
                    },

                    characterAttackingExpressionKeys = new List<string>()
                    {
                        "Arcadian_Unit_VampireFruitBat_Chatter_Attacking_1_Key"
                    },

                    characterSlayedExpressionKeys = new List<string>()
                    {
                        "Arcadian_Unit_VampireFruitBat_Chatter_Slayed_1_Key"
                    },

                    characterIdleExpressionKeys = new List<string>()
                    {
                        "Arcadian_Unit_VampireFruitBat_Chatter_Idle_1_Key",
                        "Arcadian_Unit_VampireFruitBat_Chatter_Idle_2_Key",
                        "Arcadian_Unit_VampireFruitBat_Chatter_Idle_3_Key",
                    },

                    characterTriggerExpressionKeys = new List<CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys>()
                    {
                        new CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys()
                        {
                            Key = "Arcadian_Unit_VampireFruitBat_Chatter_Trigger_OnDeath_1_Key",
                            Trigger = CharacterTriggerData.Trigger.OnDeath
                        }
                    }
                }.Build(),

                StartingStatusEffects = new StatusEffectStackData[]
                {
                    new StatusEffectStackData
                    {
                        statusId = "icarian",
                        count = 1
                    }
                },

                TriggerBuilders = new List<CharacterTriggerDataBuilder>
                {
                    new CharacterTriggerDataBuilder
                    {
                        Trigger = CharacterTriggerData.Trigger.OnDeath,
                        DescriptionKey = "Arcadian_Unit_VampireFruitBat_Description_Key",
                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                //EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
                                EffectStateName = "CardEffectAddStatusEffect",
                                TargetMode = TargetMode.Room,
                                TargetTeamType = Team.Type.Monsters,
                                ParamStatusEffects = new StatusEffectStackData[]
                                {
                                    new StatusEffectStackData
                                    { 
                                        statusId = VanillaStatusEffectIDs.Lifesteal,
                                        count = 1
                                    }
                                }
                            }
                        }
                    }
                }

            }.BuildAndRegister();

            VampireFruitBatCardData = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Arcadian_Unit_VampireFruitBat_Name_Key",
                Cost = 0,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Starter,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "ArcadianCompatibility/Assets/VampireFruitBatCard.png",
                ClanID = EquestrianClan.ID,
                CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string>
                {
                    "Arcadian_Unit_VampireFruitBat_Lore_Key"
                },
                LinkedClass = ArcadianCompatibility.ArcadianClan,
                IgnoreWhenCountingMastery = true,
                LinkedMasteryCard = ArcadianCompatibility.Analog,
                
                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterData = VampireFruitBatCharacter,
                        EffectStateName = "CardEffectSpawnMonster"
                    }
                }
            }.BuildAndRegister();

            //Note: This only creates the essence but doesn't register it. That must be done seperately.
            VampireFruitBatSynthesis = new CardUpgradeDataBuilder
            {
                UpgradeTitle = "VampireFruitBatEssence",
                SourceSynthesisUnit = VampireFruitBatCharacter,
                UpgradeDescriptionKey = "Arcadian_Unit_VampireFruitBat_Essence_Key",
                BonusHP = 0,
                BonusDamage = 0
            }.Build();

            //AccessTools.Field(typeof(CardData), "ignoreWhenCountingMastery").SetValue(cardData, Cost);
        }
    }
}