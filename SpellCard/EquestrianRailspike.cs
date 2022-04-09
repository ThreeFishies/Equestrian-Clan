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

namespace Equestrian.SpellCards
{
    class EquestrianRailspike
    {
        public static readonly string ID = Ponies.GUID + "_EquestrianRailspike";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_EquestrianRailspike_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_EquestrianRailspike_Description_Key",
                Cost = 0,
                CostType = CardData.CostType.ConsumeRemainingEnergy,
                CardType = CardType.Spell,
                Rarity = CollectableRarity.Rare,
                TargetsRoom = true,
                Targetless = true,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/EquestrianRailspike.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Spell_EquestrianRailspike_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                //Scale via X
                TraitBuilders = new List<CardTraitDataBuilder> 
                {
                    new CardTraitDataBuilder 
                    {
                        //Need to make custom effect here
                        TraitStateName = typeof(CustomCardTraitScalingAdjustRoomSize).AssemblyQualifiedName,
                        ParamTrackedValue = CardStatistics.TrackedValueType.PlayedCost,
                        ParamEntryDuration = CardStatistics.EntryDuration.ThisBattle,
                        ParamUseScalingParams = true,
                        ParamInt = 1,
                        ParamTeamType = Team.Type.Monsters,
                    },
  
                    new CardTraitDataBuilder
                    {
                        TraitStateName = typeof(CustomCardTraitScalingAddEnergyNextTurn).AssemblyQualifiedName,
                        ParamTrackedValue = CardStatistics.TrackedValueType.PlayedCost,
                        ParamEntryDuration = CardStatistics.EntryDuration.ThisBattle,
                        ParamUseScalingParams = true,
                        ParamInt = 2,
                        ParamFloat = 1.0f,
                        ParamTeamType = Team.Type.Monsters,
                    },

                    new CardTraitDataBuilder
                    {
                       TraitStateName = "CardTraitExhaustState"
                    }
                },

                //The trait builders seem to do all the work, using being played as the trigger.
                //These effect builders exist for tooltips, I guess...
                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        //EffectStateType = VanillaCardEffectTypes.CardEffectAdjustRoomCapacity,
                        EffectStateName = "CardEffectAdjustRoomCapacity",
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Monsters,
                        ParamInt = 0,
                    },
                    new CardEffectDataBuilder
                    {
                        //EffectStateType = VanillaCardEffectTypes.CardEffectGainEnergyNextTurn,
                        EffectStateName = "CardEffectGainEnergyNextTurn",
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Monsters,
                        ParamInt = 0,
                    }
                }
            }.BuildAndRegister();
        }
    }
}