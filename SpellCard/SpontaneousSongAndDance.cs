using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Equestrian.Init;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Enums;
using Equestrian;
using CustomEffects;
using ShinyShoe;

namespace Equestrian.SpellCards
{
    class SpontaneousSongAndDance
    {
        public static readonly string ID = Ponies.GUID + "_SpontaneousSongAndDance";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_SpontaneousSongAndDance_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_SpontaneousSongAndDance_Description_Key",
                Cost = 0,
                CostType = CardData.CostType.ConsumeRemainingEnergy,
                CardType = CardType.Spell,
                Rarity = CollectableRarity.Uncommon,
                TargetsRoom = true,
                Targetless = true,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/SpontaneousSongAndDance.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Spell_SpontaneousSongAndDance_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                //Scale via X
                TraitBuilders = new List<CardTraitDataBuilder> 
                {
                    new CardTraitDataBuilder 
                    {
                        TraitStateName = "CardTraitScalingHeal",
                        ParamTrackedValue = CardStatistics.TrackedValueType.PlayedCost,
                        ParamEntryDuration = CardStatistics.EntryDuration.ThisBattle,
                        ParamUseScalingParams = true,
                        ParamInt = 3,
                        ParamFloat = 1.0f,
                        ParamTeamType = Team.Type.Monsters,
                    },
  
                    new CardTraitDataBuilder
                    {
                        TraitStateName = "CardTraitScalingAddStatusEffect",
                        ParamTrackedValue = CardStatistics.TrackedValueType.PlayedCost,
                        ParamEntryDuration = CardStatistics.EntryDuration.ThisBattle,
                        ParamUseScalingParams = true,
                        ParamInt = 3,
                        ParamFloat = 1.0f,
                        ParamTeamType = Team.Type.Monsters,
                        ParamStatusEffects = new StatusEffectStackData[] 
                        { 
                            new StatusEffectStackData() 
                            {
                                statusId = VanillaStatusEffectIDs.Rage,
                                count = 0
                            } 
                        }
                    },
                    new CardTraitDataBuilder 
                    { 
                        TraitStateName = typeof(CardTraitHerd).AssemblyQualifiedName,
                        ParamInt = 4,
                    }
                },

                //Add X*3 healing and X*3 Rage
                //Note: "CardTraitScalingAddDamage_ExtraHealing_XCostOutsideBattle_CardText" needs to be added to your localization file because it was left undefined.
                //Use: "<nobr><i>(<{1}>*+{0}*</{1}> healing)</i></nobr>"
                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = VanillaCardEffectTypes.CardEffectHeal,
                        EffectStateName = "CardEffectHeal",
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Monsters,
                        ParamMultiplier = 1.0f,
                        ParamInt = 0,
                    },
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectAddStatusEffect",
                        //EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Monsters,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                            new StatusEffectStackData
                            {
                                statusId = VanillaStatusEffectIDs.Rage,
                                count = 0
                            }
                        }
                    },
                    new CardEffectDataBuilder 
                    {
                        EffectStateName = typeof(CardEffectHerdTooltip).AssemblyQualifiedName
                    }
                }
            }.BuildAndRegister();
        }
    }
}