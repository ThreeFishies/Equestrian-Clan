using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Trainworks.Builders;
using Trainworks.Managers;
using Trainworks.Constants;
using Trainworks;
using System.Linq;
using UnityEngine;
using Trainworks.Utilities;
using Equestrian.Init;
using CustomEffects;
using Equestrian;
using Equestrian.Champions;
using BepInEx.Logging;
using System.IO;
using System.Reflection;
using UnityEngine.AddressableAssets;

namespace Equestrian.MonsterCards
{
    class CrunchieMunchie
    {
        public static readonly string ID = Ponies.GUID + "_v2_CrunchieMunchieCard";
        public static readonly string CharID = Ponies.GUID + "_v2_CrunchieMunchieCharacter";
        public static CardUpgradeData CrunchieMunchieSynthesis;

        public static void BuildAndRegister()
        {
            CharacterData CrunchieMunchieCharacterData = new CharacterDataBuilder
            {
                CharacterID = CharID,
                NameKey = "Pony_Unit_CrunchieMunchie_Name_Key",
                Size = 1,
                Health = 1,
                AttackDamage = 1,
                PriorityDraw = false,
                AssetPath = "MonsterAssets/CrunchieMunchieUnit.png",
                SubtypeKeys = new List<string> { SubtypePony.Key },
                CharacterChatterData = new CharacterChatterDataBuilder() 
                {
                    name = "Crunchie Munchie chatter data",

                    gender = CharacterChatterData.Gender.Female,

                    characterAddedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_CrunchieMunchie_Chatter_Added_1_Key"
                    },

                    characterAttackingExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_CrunchieMunchie_Chatter_Attacking_1_Key"
                    },

                    characterSlayedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_CrunchieMunchie_Chatter_Slayed_1_Key"
                    },

                    characterIdleExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_CrunchieMunchie_Chatter_Idle_1_Key",
                        "Pony_Unit_CrunchieMunchie_Chatter_Idle_2_Key",
                        "Pony_Unit_CrunchieMunchie_Chatter_Idle_3_Key",
                        "Pony_Unit_CrunchieMunchie_Chatter_Idle_4_Key",
                        "Pony_Unit_CrunchieMunchie_Chatter_Idle_5_Key",
                        "Pony_Unit_CrunchieMunchie_Chatter_Idle_6_Key",
                        "Pony_Unit_CrunchieMunchie_Chatter_Idle_7_Key",
                        "Pony_Unit_CrunchieMunchie_Chatter_Idle_8_Key",
                    },

                    characterTriggerExpressionKeys = new List<CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys>() 
                    {
                        new CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys()
                        {
                            Key = "Pony_Unit_CrunchieMunchie_Chatter_Trigger_PostCombat_1_Key",
                            Trigger = CharacterTriggerData.Trigger.PostCombat
                        }
                    }
                }.Build(),
                
                StartingStatusEffects = new StatusEffectStackData[] 
                { 
                    new StatusEffectStackData
                    { 
                        statusId = VanillaStatusEffectIDs.Armor,
                        count = 5
                    }
                },

                TriggerBuilders = new List<CharacterTriggerDataBuilder> 
                { 
                    new CharacterTriggerDataBuilder
                    {
                        Trigger = CharacterTriggerData.Trigger.PostCombat,
                        DescriptionKey = "Pony_Unit_CrunchieMunchie_Description_Key",
                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                //EffectStateType = VanillaCardEffectTypes.CardEffectSpawnMonster,
                                EffectStateName = "CardEffectSpawnMonster",
                                TargetMode = TargetMode.Room,
                                TargetTeamType = Team.Type.None,
                                ParamInt = 1,
                                ParamBool = true,
                                ParamCharacterData = CustomCharacterManager.GetCharacterDataByID(Carrot.CharID),
                            }
                        }
                    }
                }

            }.BuildAndRegister();

            CardData CrunchieMunchieCardData = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Unit_CrunchieMunchie_Name_Key",
                Cost = 1,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "MonsterAssets/CrunchieMunchieCard.png",
                ClanID = EquestrianClan.ID,
                CardPoolIDs = new List<string> { "ConscriptUnitPool" },
                CardLoreTooltipKeys = new List<string>
                {
                    "Pony_Unit_CrunchieMunchie_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,
                SharedMasteryCards = new List<CardData>
                {
                    CustomCardManager.GetCardDataByID(Carrot.ID)
                },
                IgnoreWhenCountingMastery = true,
                //No builder for this field :-/
                //SharedDiscoveryCards = new List<CardData> 
                //{
                //    CustomCardManager.GetCardDataByID(Carrot.ID)
                //},


                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterData = CrunchieMunchieCharacterData,
                        EffectStateName = "CardEffectSpawnMonster",
                        /*AdditionalTooltips = new AdditionalTooltipData[]
                        {
                            new AdditionalTooltipData
                            {
                                titleKey = "Pony_Unit_CrunchieMunchie_AdditionalTooltip_Title_Key",
                                descriptionKey = "Pony_Unit_CrunchieMunchie_AdditionalTooltip_Description_Key",
                                isStatusTooltip = false,
                                isTipTooltip = false,
                                isTriggerTooltip = false,
                                trigger = CharacterTriggerData.Trigger.OnEaten,
                                statusId = VanillaStatusEffectIDs.Cardless,
                                style = TooltipDesigner.TooltipDesignType.Default
                            }
                        }*/
                    }
                }
            }.Build();
            AccessTools.Field(typeof(CardData), "sharedDiscoveryCards").SetValue(CrunchieMunchieCardData, new List<CardData> { CustomCardManager.GetCardDataByID(Carrot.ID) });
            //Trainworks.Log(LogLevel.Debug, "Adding custom card: " + CrunchieMunchieCardData.GetName());
            CustomCardManager.RegisterCustomCard(CrunchieMunchieCardData, new List<string> { });

            //Note: This only creates the essence but doesn't register it. That must be done seperately.
            CrunchieMunchieSynthesis = new CardUpgradeDataBuilder
            {
                UpgradeTitle = "CrunchieMunchieEssence",
                SourceSynthesisUnit = CrunchieMunchieCharacterData,
                UpgradeDescriptionKey = "Pony_Unit_CrunchieMunchie_Essence_Key",
                BonusHP = 0,
                BonusDamage = 0
            }.Build();
        }
    }
}