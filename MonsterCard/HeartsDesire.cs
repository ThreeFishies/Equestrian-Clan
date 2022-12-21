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
    class HeartsDesire
    {
        public static readonly string ID = Ponies.GUID + "_HeartsDesireCard";
        public static readonly string CharID = Ponies.GUID + "_HeartsDesireCharacter";
        public static CardUpgradeData HeartsDesireSynthesis;

        public static void BuildAndRegister()
        {
            CharacterData HeartsDesireCharacterData = new CharacterDataBuilder
            {
                CharacterID = CharID,
                NameKey = "Pony_Unit_HeartsDesire_Name_Key",
                Size = 1,
                Health = 1,
                AttackDamage = 0,
                PriorityDraw = false,
                AssetPath = "MonsterAssets/HeartsDesireUnit.png",
                SubtypeKeys = new List<string> { SubtypeHerb.Key },

                StartingStatusEffects = new StatusEffectStackData[]
                {
                    new StatusEffectStackData
                    {
                        statusId = VanillaStatusEffectIDs.Immobile,
                        count = 1
                    }
                },

                RoomModifierBuilders = new List<RoomModifierDataBuilder> 
                { 
                    new RoomModifierDataBuilder
                    {
                        DescriptionKey = "Pony_Unit_HeartsDesire_Room_Description_Key",
                        RoomStateModifierClassType = typeof(RoomStateCostModifierAll),
                        ParamInt = -2,
                        //These three fields can't be left undefined or your tooltips will break:
                        ExtraTooltipBodyKey = "Pony_Unit_HeartsDesire_Room_TooltipBody_Key",
                        ExtraTooltipTitleKey = "Pony_Unit_HeartsDesire_Room_TooltipTitle_Key",
                        ParamStatusEffects = new StatusEffectStackData[] {}
                    }
                },

                TriggerBuilders = new List<CharacterTriggerDataBuilder> 
                { 
                    new CharacterTriggerDataBuilder
                    {
                        Trigger = CharacterTriggerData.Trigger.OnEaten,
                        DescriptionKey = "Pony_Unit_HeartsDesire_Trigger_Description_Key",
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
                                        statusId = VanillaStatusEffectIDs.Emberdrain,
                                        count = 3
                                    }
                                }
                            }
                        }
                    }
                }
            }.BuildAndRegister();

            CardData HeartsDesireCardData = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Unit_HeartsDesire_Name_Key",
                Cost = 0,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Rare,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "MonsterAssets/HeartsDesireCard.png",
                ClanID = EquestrianClan.ID,
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                //CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string>
                {
                    "Pony_Unit_HeartsDesire_Lore_Key"
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
                        ParamCharacterData = HeartsDesireCharacterData,
                        EffectStateName = "CardEffectSpawnMonster"
                    }
                }
            }.BuildAndRegister();

            //Note: This only creates the essence but doesn't register it. That must be done seperately.
            HeartsDesireSynthesis = new CardUpgradeDataBuilder
            {
                UpgradeTitle = "HeartsDesireEssence",
                SourceSynthesisUnit = HeartsDesireCharacterData,
                UpgradeDescriptionKey = "Pony_Unit_HeartsDesire_Essence_Key",
                BonusHP = 0,
                BonusDamage = 0,
                CostReduction = 99,
                XCostReduction = 3,
                BonusSize = -5,
                StatusEffectUpgrades = new List<StatusEffectStackData> 
                { 
                    new StatusEffectStackData
                    {
                        statusId = VanillaStatusEffectIDs.Emberdrain,
                        count = 2
                    }
                }
            }.Build();
        }
    }
}