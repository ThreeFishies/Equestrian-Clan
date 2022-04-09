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
    class Carrot
    {
        public static readonly string ID = Ponies.GUID + "_CarrotCard";
        public static readonly string CharID = Ponies.GUID + "_CarrotCharacter";
        public static CardUpgradeData CarrotSynthesis;

        public static void BuildAndRegister()
        {
            CharacterData CarrotCharacterData = new CharacterDataBuilder
            {
                CharacterID = CharID,
                NameKey = "Pony_Unit_Carrot_Name_Key",
                Size = 1,
                Health = 5,
                AttackDamage = 0,
                PriorityDraw = false,
                AssetPath = "MonsterAssets/CarrotUnit.png",
                SubtypeKeys = new List<string> { SubtypeHerb.Key, "SubtypesData_Snack" },

                TriggerBuilders = new List<CharacterTriggerDataBuilder> 
                { 
                    new CharacterTriggerDataBuilder
                    {
                        Trigger = CharacterTriggerData.Trigger.OnEaten,
                        DescriptionKey = "Pony_Unit_Carrot_Trigger_Description_Key",
                        EffectBuilders = new List<CardEffectDataBuilder>
                        { 
                            new CardEffectDataBuilder
                            {
                                //EffectStateType = VanillaCardEffectTypes.CardEffectBuffMaxHealth,
                                EffectStateName = "CardEffectBuffMaxHealth",
                                TargetMode = TargetMode.LastFeederCharacter,
                                ParamInt = 5
                            },
                            new CardEffectDataBuilder
                            {
                                //EffectStateType = VanillaCardEffectTypes.CardEffectHeal,
                                EffectStateName = "CardEffectHeal",
                                TargetMode = TargetMode.LastFeederCharacter,
                                ParamInt = 5
                            }
                        }
                    }
                }
            }.BuildAndRegister();

            CardData CarrotCardData = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Unit_Carrot_Name_Key",
                Cost = 0,
                CostType = CardData.CostType.NonPlayable,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "MonsterAssets/CarrotCard.png",
                ClanID = EquestrianClan.ID,
                CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string>
                {
                    "Pony_Unit_Carrot_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,
                IgnoreWhenCountingMastery = true,

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterData = CarrotCharacterData,
                        EffectStateName = "CardEffectSpawnMonster"
                    }
                }
            }.BuildAndRegister();

            //Note: This only creates the essence but doesn't register it. That must be done seperately.
            CarrotSynthesis = new CardUpgradeDataBuilder
            {
                UpgradeTitle = "CarrotEssence",
                SourceSynthesisUnit = CarrotCharacterData,
                UpgradeDescriptionKey = "Pony_Unit_Carrot_Essence_Key",
                BonusHP = 0,
                BonusDamage = 0,
            }.Build();
        }
    }
}