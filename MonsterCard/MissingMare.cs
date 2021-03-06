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
    /// <summary>
    /// This secret unit can be obtained by skipping the first set of options in the FlowerPonies event.
    /// </summary>
    class MissingMare
    {
        public static readonly string ID = Ponies.GUID + "_MissingMareCard";
        public static readonly string CharID = Ponies.GUID + "_MissingMareCharacter";
        public static CardUpgradeData MissingMareSynthesis;

        public static void BuildAndRegister()
        {
            CharacterData MissingMareCharacterData = new CharacterDataBuilder
            {
                CharacterID = CharID,
                NameKey = "Pony_Unit_MissingMare_Name_Key",
                Size = 1,
                Health = 3,
                AttackDamage = 25,
                PriorityDraw = false,
                AssetPath = "MonsterAssets/MissingMareUnit.png",
                SubtypeKeys = new List<string> { SubtypePony.Key },
                CharacterChatterData = new CharacterChatterDataBuilder() 
                {
                    name = "Missing Mare chatter data",

                    gender = CharacterChatterData.Gender.Female,

                    characterAddedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_MissingMare_Chatter_Added_1_Key"
                    },

                    characterAttackingExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_MissingMare_Chatter_Attacking_1_Key"
                    },

                    characterSlayedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_MissingMare_Chatter_Slayed_1_Key"
                    },

                    characterIdleExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_MissingMare_Chatter_Idle_1_Key",
                        "Pony_Unit_MissingMare_Chatter_Idle_2_Key",
                        "Pony_Unit_MissingMare_Chatter_Idle_3_Key",
                        "Pony_Unit_MissingMare_Chatter_Idle_4_Key",
                        "Pony_Unit_MissingMare_Chatter_Idle_5_Key",
                        "Pony_Unit_MissingMare_Chatter_Idle_6_Key",
                        "Pony_Unit_MissingMare_Chatter_Idle_7_Key",
                        "Pony_Unit_MissingMare_Chatter_Idle_8_Key"
                    },
                }.Build(),
                
                StartingStatusEffects = new StatusEffectStackData[] 
                { 
                    new StatusEffectStackData
                    { 
                        statusId = VanillaStatusEffectIDs.Stealth,
                        count = 3
                    },
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
                        Trigger = CharacterTriggerData.Trigger.CardSpellPlayed,
                        DescriptionKey = "Pony_Unit_MissingMare_Description_Key",
                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                //EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
                                EffectStateName = "CardEffectAddStatusEffect",
                                TargetMode = TargetMode.RandomInRoom,
                                TargetTeamType = Team.Type.Monsters,
                                ParamStatusEffects = new StatusEffectStackData[]
                                {
                                    new StatusEffectStackData
                                    {
                                        statusId = VanillaStatusEffectIDs.Stealth,
                                        count = 1
                                    }
                                }
                            }
                        }
                    },
                    new CharacterTriggerDataBuilder
                    {
                        trigger = CharacterTriggerData.Trigger.OnUnscaledSpawn,
                        HideTriggerTooltip = false,

                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateName = typeof(CustomEffectSocial).AssemblyQualifiedName,
                                HideTooltip = true,
                                TargetMode = TargetMode.Self,
                            },
                        }
                    }
                }

            }.BuildAndRegister();

            CardData MissingMareCardData = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Unit_MissingMare_Name_Key",
                Cost = 1,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Starter,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "MonsterAssets/MissingMareCard.png",
                ClanID = EquestrianClan.ID,
                CardPoolIDs = new List<string> {  },
                CardLoreTooltipKeys = new List<string>
                {
                    "Pony_Unit_MissingMare_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,
                LinkedMasteryCard = CustomCardManager.GetCardDataByID(BackgroundPony.ID),
                IgnoreWhenCountingMastery = true,

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterData = MissingMareCharacterData,
                        EffectStateName = "CardEffectSpawnMonster"
                    }
                }
            }.BuildAndRegister();

            //Note: This only creates the essence but doesn't register it. That must be done seperately.
            MissingMareSynthesis = new CardUpgradeDataBuilder
            {
                UpgradeTitle = "MissingMareEssence",
                SourceSynthesisUnit = MissingMareCharacterData,
                UpgradeDescriptionKey = "Pony_Unit_MissingMare_Essence_Key",
                BonusHP = 5,
                BonusDamage = 15,

                StatusEffectUpgrades = new List<StatusEffectStackData>() 
                { 
                    new StatusEffectStackData
                    {
                        statusId = VanillaStatusEffectIDs.Stealth,
                        count = 1
                    }
                }
            }.Build();
        }
    }
}