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
    class TavernAce
    {
        public static readonly string ID = Ponies.GUID + "_TavernAceCard";
        public static readonly string CharID = Ponies.GUID + "_TavernAceCharacter";
        public static CardUpgradeData TavernAceSynthesis;

        public static void BuildAndRegister()
        {
            CharacterData TavernAceCharacterData = new CharacterDataBuilder
            {
                CharacterID = CharID,
                NameKey = "Pony_Unit_TavernAce_Name_Key",
                Size = 1,
                Health = 5,
                AttackDamage = 0,
                PriorityDraw = true,
                AssetPath = "MonsterAssets/TavernAceUnit.png",
                SubtypeKeys = new List<string> { SubtypePony.Key },
                CharacterChatterData = new CharacterChatterDataBuilder()
                {
                    name = "Tavern Ace chatter data",

                    gender = CharacterChatterData.Gender.Female,

                    characterAddedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_TavernAce_Chatter_Added_1_Key"
                    },

                    characterAttackingExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_TavernAce_Chatter_Attacking_1_Key"
                    },

                    characterSlayedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_TavernAce_Chatter_Slayed_1_Key"
                    },

                    characterIdleExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_TavernAce_Chatter_Idle_1_Key",
                        "Pony_Unit_TavernAce_Chatter_Idle_2_Key",
                        "Pony_Unit_TavernAce_Chatter_Idle_3_Key",
                        "Pony_Unit_TavernAce_Chatter_Idle_4_Key",
                        "Pony_Unit_TavernAce_Chatter_Idle_5_Key",
                        "Pony_Unit_TavernAce_Chatter_Idle_6_Key",
                        "Pony_Unit_TavernAce_Chatter_Idle_7_Key",
                        "Pony_Unit_TavernAce_Chatter_Idle_8_Key"
                    }
                }.Build(),

                TriggerBuilders = new List<CharacterTriggerDataBuilder>
                {

                },
            }.BuildAndRegister();

            CardData TavernAceCardData = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Unit_TavernAce_Name_Key",
                Cost = 1,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Rare,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "MonsterAssets/TavernAceCard.png",
                ClanID = EquestrianClan.ID,
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.UnitsAllBanner },
                //CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string>
                {
                    "Pony_Unit_TavernAce_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,
                UnlockLevel = 1,

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterData = TavernAceCharacterData,
                        EffectStateName = "CardEffectSpawnMonster"
                    }
                },

                TriggerBuilders = new List<CardTriggerEffectDataBuilder> 
                { 
                    new CardTriggerEffectDataBuilder 
                    { 
                        Trigger = CardTriggerType.OnUnplayed,
                        DescriptionKey = "Pony_Unit_TavernAce_Description_Key",
                        CardTriggerEffects = new List<CardTriggerData>
                        {
                            new CardTriggerEffectDataBuilder{}.AddCardTrigger
                            (
                                PersistenceMode.SingleRun, 
                                "CardTriggerEffectBuffCharacterDamage", 
                                "None", 
                                12
                            )
                        }
                    }
                }
            }.BuildAndRegister();

            //Note: This only creates the essence but doesn't register it. That must be done seperately.
            TavernAceSynthesis = new CardUpgradeDataBuilder
            {
                UpgradeTitle = "TavernAceEssence",
                SourceSynthesisUnit = TavernAceCharacterData,
                UpgradeDescriptionKey = "Pony_Unit_TavernAce_Essence_Key",
                BonusHP = 0,
                BonusDamage = 0,

                CardTriggerUpgradeBuilders = new List<CardTriggerEffectDataBuilder>
                {
                    new CardTriggerEffectDataBuilder
                    {
                        trigger = CardTriggerType.OnUnplayed,
                        DescriptionKey = "Pony_Unit_TavernAce_Essence_Description_Key",

                        CardTriggerEffects = new List<CardTriggerData>
                        {
                            new CardTriggerEffectDataBuilder{}.AddCardTrigger 
                            (
                                PersistenceMode.SingleRun, 
                                "CardTriggerEffectBuffCharacterDamage", 
                                "None", 
                                12
                            )
                        }
                    }
                }
            }.Build();
        }
    }
}