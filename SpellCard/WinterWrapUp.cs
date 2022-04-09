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
    class WinterWrapUp
    {
        public static readonly string ID = Ponies.GUID + "_WinterWrapUp";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_WinterWrapUp_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_WinterWrapUp_Description_Key",
                Cost = 0,
                CostType = CardData.CostType.ConsumeRemainingEnergy,
                CardType = CardType.Spell,
                Rarity = CollectableRarity.Uncommon,
                TargetsRoom = true,
                Targetless = true,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/WinterWrapUp.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Spell_WinterWrapUp_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                //Scale via X
                TraitBuilders = new List<CardTraitDataBuilder> 
                {
                    new CardTraitDataBuilder 
                    {
                        TraitStateName = "CardTraitScalingAddDamage",
                        ParamTrackedValue = CardStatistics.TrackedValueType.PlayedCost,
                        ParamEntryDuration = CardStatistics.EntryDuration.ThisBattle,
                        ParamUseScalingParams = true,
                        ParamInt = 1,
                        ParamFloat = 1.0f,
                        ParamTeamType = Team.Type.Heroes,
                    },
  
                    new CardTraitDataBuilder
                    {
                        TraitStateName = "CardTraitScalingAddStatusEffect",
                        ParamTrackedValue = CardStatistics.TrackedValueType.PlayedCost,
                        ParamEntryDuration = CardStatistics.EntryDuration.ThisBattle,
                        ParamUseScalingParams = true,
                        ParamInt = 2,
                        ParamFloat = 1.0f,
                        ParamTeamType = Team.Type.Heroes,
                        ParamStatusEffects = new StatusEffectStackData[] 
                        { 
                            new StatusEffectStackData() 
                            {
                                statusId = VanillaStatusEffectIDs.Frostbite,
                                count = 0
                            } 
                        }
                    },
                },

                //Add 1*X damage and X*2 Frostbite
                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        //EffectStateType = VanillaCardEffectTypes.CardEffectHeal,
                        EffectStateName = "CardEffectDamage",
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Heroes,
                        ParamMultiplier = 1.0f,
                        ParamInt = 0,
                    },
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectAddStatusEffect",
                        //EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Heroes,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                            new StatusEffectStackData
                            {
                                statusId = VanillaStatusEffectIDs.Frostbite,
                                count = 0
                            }
                        }
                    },
                }
            }.BuildAndRegister();
        }
    }
}