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
    class PoisonJoke
    {
        public static readonly string ID = Ponies.GUID + "_PoisonJokeCard";
        public static readonly string CharID = Ponies.GUID + "_PoisonJokeCharacter";
        public static CardUpgradeData PoisonJokeSynthesis;

        public static void BuildAndRegister()
        {
            CharacterData PoisonJokeCharacterData = new CharacterDataBuilder
            {
                CharacterID = CharID,
                NameKey = "Pony_Unit_PoisonJoke_Name_Key",
                Size = 1,
                Health = 1,
                AttackDamage = 0,
                PriorityDraw = false,
                AssetPath = "MonsterAssets/PoisonJokeUnit.png",
                SubtypeKeys = new List<string> { SubtypeHerb.Key },

                StartingStatusEffects = new StatusEffectStackData[]
                {
                    new StatusEffectStackData
                    {
                        statusId = VanillaStatusEffectIDs.Immobile,
                        count = 1
                    }
                },

                TriggerBuilders = new List<CharacterTriggerDataBuilder> 
                { 
                    new CharacterTriggerDataBuilder
                    {
                        Trigger = CharacterTriggerData.Trigger.OnSpawn,
                        DescriptionKey = "Pony_Unit_PoisonJoke_Trigger0_Description_Key",
                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                //EffectStateType = VanillaCardEffectTypes.CardEffectBuffDamage,
                                EffectStateName = "CardEffectAddTempCardUpgradeToUnits",
                                TargetMode = TargetMode.Room,
                                TargetTeamType = Team.Type.Monsters,
                                ParamCardUpgradeData = new CardUpgradeDataBuilder
                                {
                                    UpgradeTitle = "PoisonJoke_Summon_trigger",
                                    BonusDamage = 20,
                                    BonusHP = 0
                                }.Build(),
                            },

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
                                        statusId = VanillaStatusEffectIDs.Sap,
                                        count = 10
                                    },
                                }
                            }
                        }
                    },
                    new CharacterTriggerDataBuilder
                    {
                        Trigger = CharacterTriggerData.Trigger.OnEaten,
                        DescriptionKey = "Pony_Unit_PoisonJoke_Trigger1_Description_Key",
                        EffectBuilders = new List<CardEffectDataBuilder>
                        { 
                            new CardEffectDataBuilder
                            {
                                //EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
                                EffectStateName = "CardEffectAddStatusEffect",
                                TargetMode = TargetMode.LastFeederCharacter,
                                ParamStatusEffects = new StatusEffectStackData[]
                                {
                                    new StatusEffectStackData
                                    {
                                        statusId = VanillaStatusEffectIDs.Dazed,
                                        count = 2
                                    }
                                }
                            }
                        }
                    }
                }
            }.BuildAndRegister();

            CardData PoisonJokeCardData = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Unit_PoisonJoke_Name_Key",
                Cost = 0,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Uncommon,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "MonsterAssets/PoisonJokeCard.png",
                ClanID = EquestrianClan.ID,
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                //CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string>
                {
                    "Pony_Unit_PoisonJoke_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,
                UnlockLevel = 8,
                TraitBuilders = new List<CardTraitDataBuilder> 
                { 
                    new CardTraitDataBuilder
                    {
                        TraitStateName = typeof(CustomCardTraitHerb).AssemblyQualifiedName
                    }
                },

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterData = PoisonJokeCharacterData,
                        EffectStateName = "CardEffectSpawnMonster"
                    }
                }
            }.BuildAndRegister();

            //Note: This only creates the essence but doesn't register it. That must be done seperately.
            PoisonJokeSynthesis = new CardUpgradeDataBuilder
            {
                UpgradeTitle = "PoisonJokeEssence",
                SourceSynthesisUnit = PoisonJokeCharacterData,
                UpgradeDescriptionKey = "Pony_Unit_PoisonJoke_Essence_Key",
                BonusHP = 0,
                BonusDamage = 40,
                StatusEffectUpgrades = new List<StatusEffectStackData> 
                { 
                    new StatusEffectStackData
                    {
                        statusId = VanillaStatusEffectIDs.Sap,
                        count = 20
                    },
                    new StatusEffectStackData
                    {
                        statusId = VanillaStatusEffectIDs.Dazed,
                        count = 1
                    }
                }
            }.Build();
        }
    }
}