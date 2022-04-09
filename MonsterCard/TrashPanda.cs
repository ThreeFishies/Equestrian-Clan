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
    class TrashPanda
    {
        public static readonly string ID = Ponies.GUID + "_TrashPandaCard";
        public static readonly string CharID = Ponies.GUID + "_TrashPandaCharacter";
        public static CardUpgradeData TrashPandaSynthesis;

        //Despite Unity throwing tons of errors, this actually seemed to work as intended.
        //public static CharacterData TrashPandaCharacterData = new CharacterData { };
        //public static CardData TrashPandaCardData = new CardData { };

        public static void BuildAndRegister()
        {
            CharacterData TrashPandaCharacterData = new CharacterDataBuilder
            {
                CharacterID = CharID,
                NameKey = "Pony_Unit_TrashPanda_Name_Key",
                Size = 1,
                Health = 3,
                AttackDamage = 5,
                PriorityDraw = false,
                AssetPath = "MonsterAssets/TrashPandaUnit.png",
                SubtypeKeys = new List<string> { SubtypePet.Key },
                CharacterChatterData = new CharacterChatterDataBuilder() 
                {
                    name = "Trash Panda chatter data",

                    gender = CharacterChatterData.Gender.Male,

                    characterAddedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_TrashPanda_Chatter_Added_1_Key"
                    },

                    characterAttackingExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_TrashPanda_Chatter_Attacking_1_Key"
                    },

                    characterSlayedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_TrashPanda_Chatter_Slayed_1_Key"
                    },

                    characterIdleExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_TrashPanda_Chatter_Idle_1_Key",
                        "Pony_Unit_TrashPanda_Chatter_Idle_2_Key",
                        "Pony_Unit_TrashPanda_Chatter_Idle_3_Key"
                    }
                }.Build(),
                
                StartingStatusEffects = new StatusEffectStackData[] 
                { 
                    new StatusEffectStackData
                    { 
                        statusId = VanillaStatusEffectIDs.Sweep,
                        count = 1
                    }
                }
            }.BuildAndRegister();

            CardData TrashPandaCardData = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Unit_TrashPanda_Name_Key",
                Cost = 0,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "MonsterAssets/TrashPandaCard.png",
                ClanID = EquestrianClan.ID,
                CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string>
                {
                    "Pony_Unit_TrashPanda_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,
                IgnoreWhenCountingMastery = true,

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterData = TrashPandaCharacterData,
                        EffectStateName = "CardEffectSpawnMonster"
                    }
                }
            }.BuildAndRegister();

            //Note: This only creates the essence but doesn't register it. That must be done seperately.
            TrashPandaSynthesis = new CardUpgradeDataBuilder
            {
                UpgradeTitle = "TrashPandaEssence",
                SourceSynthesisUnit = TrashPandaCharacterData,
                UpgradeDescriptionKey = "Pony_Unit_TrashPanda_Essence_Key",
                BonusHP = 3,
                BonusDamage = 5
            }.Build();
        }
    }

    class TrashPandaCardPool 
    {
        public static CardPool GetPool()
        {
            return new CardPoolBuilder
            {
                CardPoolID = Ponies.GUID + "_TrashPandaCardPool",
                CardIDs = new List<string>
                {
                    CustomCardManager.GetCardDataByID(TrashPanda.ID).GetID(),
                }
            }.BuildAndRegister();
        }
    }
}