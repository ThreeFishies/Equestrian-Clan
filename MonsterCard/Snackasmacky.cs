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

namespace Equestrian.MonsterCards
{
    class Snackasmacky
    {
        public static readonly string ID = Ponies.GUID + "_SnackasmackyCard";
        public static readonly string CharID = Ponies.GUID + "_SnackasmackyCharacter";
        public static CardUpgradeData SnackasmackySynthesis;

        //If you want new variables, just make new ones, right? Lol wrong.
        //public static CharacterData SnackasmackyCharacterData = new CharacterData { };
        //public static CardData SnackasmackyCardData = new CardData { };

        public static void BuildAndRegister()
        {
            CharacterData SnackasmackyCharacterData = new CharacterDataBuilder
            {
                CharacterID = CharID,
                NameKey = "Pony_Unit_Snackasmacky_Name_Key",
                Size = 1,
                Health = 15,
                AttackDamage = 5,
                PriorityDraw = true,
                AssetPath = "MonsterAssets/SnackasmackyUnit.png",
                SubtypeKeys = new List<string> { SubtypePony.Key },
                CharacterChatterData = new CharacterChatterDataBuilder()
                {
                    name = "Snackasmacky chatter data",

                    gender = CharacterChatterData.Gender.Female,

                    characterAddedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_Snackasmacky_Chatter_Added_1_Key",
                    },

                    characterAttackingExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_Snackasmacky_Chatter_Attacking_1_Key",
                    },

                    characterSlayedExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_Snackasmacky_Chatter_Slayed_1_Key",
                    },

                    characterIdleExpressionKeys = new List<string>() 
                    {
                        "Pony_Unit_Snackasmacky_Chatter_Idle_1_Key",
                        "Pony_Unit_Snackasmacky_Chatter_Idle_2_Key",
                        "Pony_Unit_Snackasmacky_Chatter_Idle_3_Key",
                        "Pony_Unit_Snackasmacky_Chatter_Idle_4_Key",
                        "Pony_Unit_Snackasmacky_Chatter_Idle_5_Key",
                        "Pony_Unit_Snackasmacky_Chatter_Idle_6_Key",
                        "Pony_Unit_Snackasmacky_Chatter_Idle_7_Key",
                        "Pony_Unit_Snackasmacky_Chatter_Idle_8_Key",
                    },

                    characterTriggerExpressionKeys = new List<CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys>() 
                    { 
                        new CharacterChatterDataBuilder.CharacterTriggerDataChatterExpressionKeys()
                        {
                            Key = "Pony_Unit_Snackasmacky_Chatter_Trigger_OnHeal_1_Key",
                            Trigger = CharacterTriggerData.Trigger.OnHeal
                        }
                    }
                }.Build(),

                TriggerBuilders = new List<CharacterTriggerDataBuilder>
                {
                    new CharacterTriggerDataBuilder
                    {
                        trigger = CharacterTriggerData.Trigger.OnHeal,
                        DescriptionKey = "Pony_Unit_Snackasmacky_Description_Key",

                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                //EffectStateType = VanillaCardEffectTypes.CardEffectAddTempCardUpgradeToUnits,
                                EffectStateName = "CardEffectAddTempCardUpgradeToUnits",
                                TargetMode = TargetMode.Room,
                                TargetTeamType = Team.Type.Monsters,
                                ParamCardUpgradeData = new CardUpgradeDataBuilder
                                {
                                    UpgradeTitle = "Snackasmacky_Rejuvenate_trigger",
                                    BonusDamage = 1,
                                    BonusHP = 1
                                }.Build(),
                            }
                        }
                    }
                },
            }.BuildAndRegister();

            CardData SnackasmackyCardData = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Unit_Snackasmacky_Name_Key",
                Cost = 1,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Uncommon,
                //Description = "<b>Action</b>: Apply {[effect0.status0.power]} <b>Armor</b> to all friendly units.",
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "MonsterAssets/SnackasmackyCard.png",
                ClanID = EquestrianClan.ID,
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.UnitsAllBanner },
                //CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Unit_Snackasmacky_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterData = SnackasmackyCharacterData,
                        EffectStateName = "CardEffectSpawnMonster"
                    }
                }
            }.BuildAndRegister();

            //Note: This only creates the essence but doesn't register it. That must be done seperately.
            SnackasmackySynthesis = new CardUpgradeDataBuilder
            {
                UpgradeTitle = "SnackasmackyEssence",
                SourceSynthesisUnit = SnackasmackyCharacterData,
                UpgradeDescriptionKey = "Pony_Unit_Snackasmacky_Essence_Key",
                BonusHP = 10,
                BonusDamage = 0,
                CostReduction = 0,
                XCostReduction = 0,
                HideUpgradeIconOnCard = true,
                UseUpgradeHighlightTextTags = true,
                BonusHeal = 0,
                BonusSize = 0,

                TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
                {
                    new CharacterTriggerDataBuilder
                    {
                        trigger = CharacterTriggerData.Trigger.OnHeal,
                        DescriptionKey = "Pony_Unit_Snackasmacky_Description_Key",

                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                //EffectStateType = VanillaCardEffectTypes.CardEffectAddTempCardUpgradeToUnits,
                                EffectStateName = "CardEffectAddTempCardUpgradeToUnits",
                                TargetMode = TargetMode.Room,
                                TargetTeamType = Team.Type.Monsters,
                                CopyModifiersFromSource = false,
                                FilterBasedOnMainSubClass = false,
                                IgnoreTemporaryModifiersFromSource = false,
                                TargetCardSelectionMode = CardEffectData.CardSelectionMode.ChooseToHand,
                                ShouldTest = true,
                                UseIntRange = false,
                                ParamBool = false,
                                ParamInt = 0,
                                ParamMaxInt = 0,
                                ParamMinInt = 0,
                                ParamMultiplier = 0.0f,
                                UseStatusEffectStackMultiplier = false,
                                ParamStr = "",
                                ParamTrigger = CharacterTriggerData.Trigger.OnDeath,

                                ParamCardUpgradeData = new CardUpgradeDataBuilder
                                {
                                    UpgradeTitle = "Snackasmacky_Essence_Rejuvenate_trigger",
                                    BonusDamage = 1,
                                    BonusHP = 1,
                                    SourceSynthesisUnit = UnityEngine.ScriptableObject.CreateInstance<CharacterData>(),
                                    
                                }.Build(),
                            }
                        }
                    }
                },
            }.Build();

            //Traverse traverse = Traverse.Create(ProviderManager.SaveManager.GetAllGameData()).Field("cardUpgradeDatas");
            //AccessTools.Field(typeof(CardUpgradeData), "isUnitSynthesisUpgrade").SetValue(monsterSnackasmackySynthesis, true);
            //List<CardUpgradeData> value = traverse.GetValue<List<CardUpgradeData>>();
            //CardUpgradeData item = value.Where((CardUpgradeData u) => "SnackasmackyEssence" == (string)AccessTools.Field(typeof(CardUpgradeData), "upgradeTitleKey").GetValue(u)).FirstOrDefault();
            //value.Remove(item);
            //value.Add(monsterSnackasmackySynthesis);
        }
    }
}