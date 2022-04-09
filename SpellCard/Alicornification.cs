using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Equestrian.Init;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Enums;
using Equestrian;
using ShinyShoe;
using CustomEffects;

namespace Equestrian.SpellCards
{
    class Alicornification
    {
        public static readonly string ID = Ponies.GUID + "_Alicornification";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_Alicornification_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_Alicornification_Description_Key",
                Cost = 5,
                Rarity = CollectableRarity.Rare,
                TargetsRoom = true,
                Targetless = false,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/Alicornification.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                //CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Spell_Alicornification_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,
                UnlockLevel = 10,

                TraitBuilders = new List<CardTraitDataBuilder> 
                { 
                    new CardTraitDataBuilder
                    { 
                        TraitStateName = typeof(CardTraitHerd).AssemblyQualifiedName,
                        ParamInt = 7,
                    },
                    new CardTraitDataBuilder 
                    {
                        TraitStateName = "CardTraitExhaustState"
                    }
                },

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateName = typeof(CustomCardEffectDoubleCurrentStats).AssemblyQualifiedName,
                        TargetMode = TargetMode.DropTargetCharacter,
                        TargetTeamType = Team.Type.Monsters,
                        ParamInt = 0,
                    },
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectAddTempCardUpgradeToUnits",
                        TargetMode = TargetMode.LastTargetedCharacters,
                        TargetTeamType = Team.Type.Monsters,
                        ParamInt = 0,
                        ParamCardUpgradeData = new CardUpgradeDataBuilder 
                        {
                            BonusDamage = 0,
                            BonusHP = 0,
                            UpgradeTitle = "Alicornification_Trample_Title",
                            UseUpgradeHighlightTextTags = true,

                            StatusEffectUpgrades = new List<StatusEffectStackData>
                            { 
                                new StatusEffectStackData
                                { 
                                    statusId = VanillaStatusEffectIDs.Trample,
                                    count = 1,
                                },
                            }
                        }.Build(),
                    },
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectAddTempCardUpgradeToUnits",
                        TargetMode = TargetMode.LastTargetedCharacters,
                        TargetTeamType = Team.Type.Monsters,
                        ParamInt = 0,
                        ParamCardUpgradeData = new CardUpgradeDataBuilder
                        {
                            BonusDamage = 0,
                            BonusHP = 0,
                            UpgradeTitle = "Alicornification_Quick_Title",
                            UseUpgradeHighlightTextTags = true,

                            StatusEffectUpgrades = new List<StatusEffectStackData>
                            {
                                new StatusEffectStackData
                                {
                                    statusId = VanillaStatusEffectIDs.Quick,
                                    count = 1,
                                },
                            }
                        }.Build(),
                    },

                    new CardEffectDataBuilder
                    { 
                        EffectStateName = typeof(CardEffectHerdTooltip).AssemblyQualifiedName,
                    }
                }
            }.BuildAndRegister();
        }
    }
}