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
    class RainbowPower
    {
        public static readonly string ID = Ponies.GUID + "_RainbowPower";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_RainbowPower_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_RainbowPower_Description_Key",
                Cost = 3,
                Rarity = CollectableRarity.Rare,
                TargetsRoom = true,
                Targetless = true,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/RainbowPower.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                //CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string>
                {
                    "Pony_Spell_RainbowPower_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,
                UnlockLevel = 9,

                TraitBuilders = new List<CardTraitDataBuilder>
                { 
                    //Herd 6
                    new CardTraitDataBuilder
                    {
                        TraitStateName = typeof(CardTraitHerd).AssemblyQualifiedName,
                        ParamInt = 6,
                    },
                    new CardTraitDataBuilder
                    {
                        //TraitStateType = VanillaCardTraitTypes.CardTraitStrongerMagicPower,
                        TraitStateName = "CardTraitStrongerMagicPower",
                        ParamInt = 5
                    },
                    new CardTraitDataBuilder
                    {
                        //TraitStateType = VanillaCardTraitTypes.CardTraitSelfPurge,
                        TraitStateName = "CardTraitSelfPurge"
                    }
                },

                EffectBuilders = new List<CardEffectDataBuilder>
                { 
                    //Kill the front enemy unit
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectDamage",
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Heroes,
                        TargetIgnoreBosses = false,
                        ParamInt = 6
                    },
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectDamage",
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Heroes,
                        TargetIgnoreBosses = false,
                        ParamInt = 6
                    },
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectDamage",
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Heroes,
                        TargetIgnoreBosses = false,
                        ParamInt = 6
                    },
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectDamage",
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Heroes,
                        TargetIgnoreBosses = false,
                        ParamInt = 6
                    },
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectDamage",
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Heroes,
                        TargetIgnoreBosses = false,
                        ParamInt = 6
                    },
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectDamage",
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Heroes,
                        TargetIgnoreBosses = false,
                        ParamInt = 6
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