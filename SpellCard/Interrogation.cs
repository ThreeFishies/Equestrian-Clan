using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Equestrian.Init;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Enums;
using Equestrian.MonsterCards;
using Equestrian;

namespace Equestrian.SpellCards
{
    class Interrogation
    {
        public static readonly string ID = Ponies.GUID + "_Interrogation";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_Interrogation_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_Interrogation_Description_Key",
                Cost = 1,
                Rarity = CollectableRarity.Uncommon,
                TargetsRoom = true,
                Targetless = true,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/Interrogation.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                //CardPoolIDs = new List<string> {  },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Spell_Interrogation_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,
                UnlockLevel = 2,

                TraitBuilders = new List<CardTraitDataBuilder> 
                { 
                    //Scale by number of ponies
                    new CardTraitDataBuilder
                    {
                        TraitStateName = typeof(CardTraitScalingAddDamage).AssemblyQualifiedName,
                        ParamInt = 20,
                        ParamCardType = CardStatistics.CardTypeTarget.Any,
                        ParamSubtype = SubtypePony.Key,
                        ParamEntryDuration = CardStatistics.EntryDuration.ThisTurn,
                        ParamTeamType = Team.Type.Heroes,
                        ParamFloat = 1.0f,
                        ParamTrackedValue = CardStatistics.TrackedValueType.SubtypeInDeck,
                    }
                },

                EffectBuilders = new List<CardEffectDataBuilder>
                { 
                    //Damage front enemy unit
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectDamage",
                        TargetMode = TargetMode.FrontInRoom,
                        TargetTeamType = Team.Type.Heroes,
                        TargetIgnoreBosses = false,
                        ParamInt = 0,
                        ParamMultiplier = 1.0f
                    }
                }
            }.BuildAndRegister();
        }
    }
}