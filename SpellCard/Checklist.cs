using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Equestrian.Init;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Enums;
using Equestrian;

namespace Equestrian.SpellCards
{
    class Checklist
    {
        public static readonly string ID = Ponies.GUID + "_Checklist";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_Checklist_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_Checklist_Description_Key",
                Cost = 0,
                Rarity = CollectableRarity.Uncommon,
                TargetsRoom = true,
                Targetless = true,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/Checklist.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Spell_Checklist_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                TraitBuilders = new List<CardTraitDataBuilder> 
                {
                    new CardTraitDataBuilder
                    {
                        TraitStateName = "CardTraitScalingAddEnergy",
                        ParamTrackedValue = CardStatistics.TrackedValueType.TypeInExhaustPile,
                        ParamCardType = CardStatistics.CardTypeTarget.Spell,
                        ParamEntryDuration = CardStatistics.EntryDuration.ThisTurn,
                        ParamInt = 1,
                        ParamFloat = 1.0f,
                        ParamUseScalingParams = true,
                        ParamTeamType = Team.Type.Heroes,
                    },
                    new CardTraitDataBuilder
                    { 
                        TraitStateName = "CardTraitExhaustState",
                    }
                },
                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectAdjustEnergy",
                        ParamInt = 0
                    }
                },
            }.BuildAndRegister();
        }
    }
}