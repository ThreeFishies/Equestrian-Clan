using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Equestrian.Init;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Enums;
using ShinyShoe;
using Equestrian;
using CustomEffects;

namespace Equestrian.SpellCards
{
    class BuckOff
    {
        public static readonly string ID = Ponies.GUID + "_BuckOff";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_BuckOff_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_BuckOff_Description_Key",
                Cost = 1,
                Rarity = CollectableRarity.Uncommon,
                TargetsRoom = true,
                Targetless = true,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/BuckOff.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                //CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Spell_BuckOff_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,
                UnlockLevel = 7,

                TraitBuilders = new List<CardTraitDataBuilder> 
                { 
                    //Herd 5
                    new CardTraitDataBuilder
                    {
                        TraitStateName = typeof(CardTraitHerd).AssemblyQualifiedName,
                        ParamInt = 5,
                    }
                },

                EffectBuilders = new List<CardEffectDataBuilder>
                { 
                    //Kill a target enemy unit
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectKill",
                        TargetMode = TargetMode.FrontInRoom,
                        TargetTeamType = Team.Type.Heroes,
                        TargetIgnoreBosses = true
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