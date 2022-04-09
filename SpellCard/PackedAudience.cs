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
using Equestrian.Enhancers;

namespace Equestrian.SpellCards
{
    class PackedAudience
    {
        public static readonly string ID = Ponies.GUID + "_PackedAudience";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_PackedAudience_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_PackedAudience_Description_Key",
                Cost = 0,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = true,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/PackedAudience.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Spell_PackedAudience_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                //Consume
                TraitBuilders = new List<CardTraitDataBuilder> 
                { 
                    new CardTraitDataBuilder
                    {
                        //TraitStateType = VanillaCardTraitTypes.CardTraitExhaustState,
                        TraitStateName = "CardTraitExhaustState"
                    }
                },

                EffectBuilders = new List<CardEffectDataBuilder>
                { 
                    new CardEffectDataBuilder
                    {
                        //A custom effect is needed because "CardEffectRecruit" is not implemented in Trainworks.
                        EffectStateName = typeof(CardEffectRecruitBackgroundPony).AssemblyQualifiedName,
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Monsters,
                    },
                    new CardEffectDataBuilder
                    {
                        EffectStateName = typeof(CardEffectRecruitBackgroundPony).AssemblyQualifiedName,
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Monsters,
                    },
                    new CardEffectDataBuilder
                    {
                        EffectStateName = typeof(CardEffectRecruitBackgroundPony).AssemblyQualifiedName,
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Monsters,
                    }
                }
            }.BuildAndRegister();
        }
    }
}