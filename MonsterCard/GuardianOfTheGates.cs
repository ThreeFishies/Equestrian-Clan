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
    class GuardianOfTheGates
    {
        public static readonly string ID = Ponies.GUID + "_GuardianOfTheGatesCard";
        public static readonly string CharID = Ponies.GUID + "_GuardianOfTheGatesCharacter";
        public static CardUpgradeData GuardianOfTheGatesSynthesis;

        public static void BuildAndRegister()
        {
            CharacterData GuardianOfTheGatesCharacterData = new CharacterDataBuilder
            {
                CharacterID = CharID,
                NameKey = "Pony_Unit_GuardianOfTheGates_Name_Key",
                Size = 6,
                Health = 150,
                AttackDamage = 50,
                PriorityDraw = true,
                AssetPath = "MonsterAssets/GuardianOfTheGatesUnit.png",
                SubtypeKeys = new List<string> { SubtypePet.Key },
                CharacterChatterData = new CharacterChatterDataBuilder() 
                {
                    name = "Guardian of the Gates chatter data",

                    gender = CharacterChatterData.Gender.Male,

                    characterAddedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_GuardianOfTheGates_Chatter_Added_1_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Added_2_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Added_3_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Added_4_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Added_5_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Added_6_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Added_7_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Added_8_Key"
                    },

                    characterAttackingExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_GuardianOfTheGates_Chatter_Attacking_1_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Attacking_2_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Attacking_3_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Attacking_4_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Attacking_5_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Attacking_6_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Attacking_7_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Attacking_8_Key"
                    },

                    characterSlayedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_GuardianOfTheGates_Chatter_Slayed_1_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Slayed_2_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Slayed_3_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Slayed_4_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Slayed_5_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Slayed_6_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Slayed_7_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Slayed_8_Key"
                    },

                    characterIdleExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_1_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_2_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_3_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_4_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_5_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_6_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_7_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_8_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_9_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_10_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_11_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_12_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_13_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_14_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_15_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_16_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_17_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_18_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_19_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_20_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_21_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_22_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_23_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_24_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_25_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_26_Key",
                        "Pony_Unit_GuardianOfTheGates_Chatter_Idle_27_Key"
                    }

                }.Build(),
                
                StartingStatusEffects = new StatusEffectStackData[] 
                { 
                    new StatusEffectStackData
                    { 
                        statusId = VanillaStatusEffectIDs.Multistrike,
                        count = 2
                    }
                }
            }.BuildAndRegister();

            CardData GuardianOfTheGatesCardData = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Unit_GuardianOfTheGates_Name_Key",
                Cost = 6,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Rare,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "MonsterAssets/GuardianOfTheGatesCard.png",
                ClanID = EquestrianClan.ID,
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.UnitsAllBanner },
                //CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string>
                {
                    "Pony_Unit_GuardianOfTheGates_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterData = GuardianOfTheGatesCharacterData,
                        EffectStateName = "CardEffectSpawnMonster"
                    }
                }
            }.BuildAndRegister();

            //Note: This only creates the essence but doesn't register it. That must be done seperately.
            GuardianOfTheGatesSynthesis = new CardUpgradeDataBuilder
            {
                UpgradeTitle = "GuardianOfTheGatesEssence",
                SourceSynthesisUnit = GuardianOfTheGatesCharacterData,
                UpgradeDescriptionKey = "Pony_Unit_GuardianOfTheGates_Essence_Key",
                BonusHP = 60,
                BonusDamage = 20,
                BonusSize = 3,
                StatusEffectUpgrades = new List<StatusEffectStackData> 
                { 
                    new StatusEffectStackData
                    { 
                        statusId = VanillaStatusEffectIDs.Multistrike,
                        count = 1
                    }
                }
            }.Build();
        }
    }
}