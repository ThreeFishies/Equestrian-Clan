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
    class SqueakyBooBoo
    {
        public static readonly string ID = Ponies.GUID + "_SqueakyBooBooCard";
        public static readonly string CharID = Ponies.GUID + "_SqueakyBooBooCharacter";
        public static CardUpgradeData SqueakyBooBooSynthesis;

        public static void BuildAndRegister()
        {
            CharacterData SqueakyBooBooCharacterData = new CharacterDataBuilder
            {
                CharacterID = CharID,
                NameKey = "Pony_Unit_SqueakyBooBoo_Name_Key",
                Size = 1,
                Health = 35,
                AttackDamage = 0,
                PriorityDraw = true,
                AssetPath = "MonsterAssets/SqueakyBooBooUnit.png",
                SubtypeKeys = new List<string> { SubtypePony.Key },
                CharacterChatterData = new CharacterChatterDataBuilder() 
                {
                    name = "Squeaky Boo-Boo chatter data",

                    gender = CharacterChatterData.Gender.Female,

                    characterAddedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_SqueakyBooBoo_Chatter_Added_1_Key"
                    },

                    characterAttackingExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_SqueakyBooBoo_Chatter_Attacking_1_Key"
                    },

                    characterSlayedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_SqueakyBooBoo_Chatter_Slayed_1_Key"
                    },

                    characterIdleExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_SqueakyBooBoo_Chatter_Idle_1_Key",
                        "Pony_Unit_SqueakyBooBoo_Chatter_Idle_2_Key",
                        "Pony_Unit_SqueakyBooBoo_Chatter_Idle_3_Key",
                        "Pony_Unit_SqueakyBooBoo_Chatter_Idle_4_Key",
                        "Pony_Unit_SqueakyBooBoo_Chatter_Idle_5_Key",
                        "Pony_Unit_SqueakyBooBoo_Chatter_Idle_6_Key",
                        "Pony_Unit_SqueakyBooBoo_Chatter_Idle_7_Key",
                        "Pony_Unit_SqueakyBooBoo_Chatter_Idle_8_Key",
                    },

                    characterTriggerExpressionKeys = new List<CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys>() 
                    { 
                        new CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys()
                        {
                            Key = "Pony_Unit_SqueakyBooBoo_Chatter_Trigger_PostCombat_1_Key",
                            Trigger = CharacterTriggerData.Trigger.PostCombat
                        }
                    }
                }.Build(),

                StartingStatusEffects = new StatusEffectStackData[] 
                {
                    new StatusEffectStackData
                    {
                        statusId = VanillaStatusEffectIDs.Armor,
                        count = 10,
                    }
                },

                TriggerBuilders = new List<CharacterTriggerDataBuilder>
                {
                    new CharacterTriggerDataBuilder
                    {
                        trigger = CharacterTriggerData.Trigger.PostCombat,
                        DescriptionKey = "Pony_Unit_SqueakyBooBoo_Description_Key",

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
                                        statusId = VanillaStatusEffectIDs.Regen,
                                        count = 2
                                    }
                                }
                            },
                        }
                    }
                },
            }.BuildAndRegister();

            CardData SqueakyBooBooCardData = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Unit_SqueakyBooBoo_Name_Key",
                Cost = 2,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Uncommon,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "MonsterAssets/SqueakyBooBooCard.png",
                ClanID = EquestrianClan.ID,
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.UnitsAllBanner },
                //CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string>
                {
                    "Pony_Unit_SqueakyBooBoo_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterData = SqueakyBooBooCharacterData,
                        EffectStateName = "CardEffectSpawnMonster"
                    }
                },

                TraitBuilders = new List<CardTraitDataBuilder>
                {
                    new CardTraitDataBuilder
                    {
                        TraitStateType = VanillaCardTraitTypes.CardTraitPermafrost
                    }
                }
            }.BuildAndRegister();

            //Note: This only creates the essence but doesn't register it. That must be done seperately.
            SqueakyBooBooSynthesis = new CardUpgradeDataBuilder
            {
                UpgradeTitle = "SqueakyBooBooEssence",
                SourceSynthesisUnit = SqueakyBooBooCharacterData,
                UpgradeDescriptionKey = "Pony_Unit_SqueakyBooBoo_Essence_Key",
                BonusHP = 30,
                BonusDamage = 0,

                TraitDataUpgradeBuilders = new List<CardTraitDataBuilder>
                {
                    new CardTraitDataBuilder
                    {
                        TraitStateType = VanillaCardTraitTypes.CardTraitPermafrost
                    }
                }
            }.Build();
        }
    }
}