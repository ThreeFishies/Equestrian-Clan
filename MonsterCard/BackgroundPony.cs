using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Trainworks.Builders;
using Trainworks.Managers;
using Trainworks.Constants;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting;
using Trainworks.Utilities;
using Equestrian.Init;
using CustomEffects;
using Equestrian;
using Equestrian.CardPools;

namespace Equestrian.MonsterCards
{
    class BackgroundPony
    {
        public static readonly string ID = Ponies.GUID + "_BackgroundPonyCard";
        public static readonly string CharID = Ponies.GUID + "_BackgroundPonyCharacter";
        public static readonly string CardPoolID = Ponies.GUID + "_BackgroundPonyCardPool";
        public static CardUpgradeData BackgroundPonySynthesis;

        //public static CharacterData SocialBackGroundPony = ScriptableObject.CreateInstance<CharacterData>();

        //So I've been doing this wrong :(
        //public static CardPool BackgroundPonyCardPool = new CardPool { };
        //public static CharacterData BackgroundPonyCharacterData = new CharacterData { };
        //public static CardData BackgroundPonyCardData = new CardData { };

        public static void BuildAndRegister()
        {
            CharacterData _BGPonyData = new CharacterDataBuilder
            {
                CharacterID = CharID,
                NameKey = "Pony_Unit_BackgroundPony_Name_Key",
                Size = 1,
                Health = 12,
                AttackDamage = 0,
                PriorityDraw = false,
                AssetPath = "MonsterAssets/BackgroundPonyUnit.png",
                SubtypeKeys = new List<string> { SubtypePony.Key },
                CharacterChatterData = new CharacterChatterDataBuilder()
                {
                    name = "Background Pony chatter data",

                    gender = CharacterChatterData.Gender.Female,

                    characterAddedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_BackgroundPony_Chatter_Added_1_Key",
                    },

                    characterAttackingExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_BackgroundPony_Chatter_Attacking_1_Key",
                    },

                    characterSlayedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_BackgroundPony_Chatter_Slayed_1_Key",
                    },

                    characterIdleExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_BackgroundPony_Chatter_Idle_1_Key",
                        "Pony_Unit_BackgroundPony_Chatter_Idle_2_Key",
                        "Pony_Unit_BackgroundPony_Chatter_Idle_3_Key",
                        "Pony_Unit_BackgroundPony_Chatter_Idle_4_Key",
                        "Pony_Unit_BackgroundPony_Chatter_Idle_5_Key",
                        "Pony_Unit_BackgroundPony_Chatter_Idle_6_Key",
                        "Pony_Unit_BackgroundPony_Chatter_Idle_7_Key",
                        "Pony_Unit_BackgroundPony_Chatter_Idle_8_Key",
                    }
                }.Build(),

                TriggerBuilders = new List<CharacterTriggerDataBuilder>
                {
                    new CharacterTriggerDataBuilder
                    {
                        trigger = CharacterTriggerData.Trigger.OnSpawn,
                        //DescriptionKey = "Pony_Unit_BackgroundPony_Description_Key",
                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateType = VanillaCardEffectTypes.CardEffectFloorRearrange,
                                TargetMode = TargetMode.Self,
                                TargetTeamType = Team.Type.Monsters,
                                ParamInt = 1
                            }
                        }
                    }
                }
            }.BuildAndRegister();

            CardData _BGPonyCard = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Unit_BackgroundPony_Name_Key",
                Cost = 0,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Starter,
                Description = "<b>Summon:</b> Move to the back.",
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "MonsterAssets/BackgroundPonyCard.png",
                ClanID = EquestrianClan.ID,
                CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Unit_BackgroundPony_Lore_Key",
                },
                LinkedClass = Ponies.EquestrianClanData,

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterData = _BGPonyData,
                        EffectStateName = "CardEffectSpawnMonster"
                    }
                }
            }.BuildAndRegister();

            CardPool testBGCardPool = new CardPoolBuilder { CardIDs = new List<string>{ _BGPonyCard.GetID() } }.BuildAndRegister();

            //Note: This only creates the essence but doesn't register it. That must be done seperately.
            BackgroundPonySynthesis = new CardUpgradeDataBuilder
            {
                UpgradeTitle = "Background Pony",
                //UpgradeTitleKey = "BackgroundPony_TitleKey",
                SourceSynthesisUnit = _BGPonyData,
                //UpgradeDescription = "+<nobr>15[health]</nobr> and <b>Summon</b>: Add three Background Ponies with missing artwork to your hand.",
                UpgradeDescriptionKey = "Pony_Unit_BackgroundPony_Essence_Key",

                BonusHP = 15,

                TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
                {
                    new CharacterTriggerDataBuilder
                    {
                        Trigger = CharacterTriggerData.Trigger.OnSpawn,
                        //Description = "Add three Background Ponies to your hand.",
                        DescriptionKey = "Pony_Unit_BackgroundPony_Essence_Summon_Key",
                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            //Cards will spawn without artwork if you don't have any Background Ponies in your deck. A fix is needed to force the art assets to load.
                            new CardEffectDataBuilder
                            {
                                EffectStateName = typeof(AddThreeBackgroundPoniesArtFix).AssemblyQualifiedName,
                                TargetMode = TargetMode.Self
                            },

                            new CardEffectDataBuilder
                            {
                                EffectStateType = VanillaCardEffectTypes.CardEffectAddBattleCard,
                                ParamCardPool = testBGCardPool, //testpool works. Keep it.
                                ParamInt = 3,
                                AdditionalParamInt = 3
                            }
                		}
                	}
                }
            }.Build();
        }
    }
}