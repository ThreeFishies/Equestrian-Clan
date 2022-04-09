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
    class Shenanigans
    {
        public static readonly string ID = Ponies.GUID + "_Shenanigans";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                //Name = "Shenanigans",
                NameKey = "Pony_Spell_Shenanigans_Name_Key",
                //Description = "Deal {[effect0.power]} damage to a target unit and apply <nobr><b>Emberdrain</b> {[effect1.status0.power]}</nobr>.",
                OverrideDescriptionKey = "Pony_Spell_Shenanigans_Description_Key",
                Cost = 0,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = false,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/Shenanigans.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Spell_Shenanigans_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                //Attuned
                TraitBuilders = new List<CardTraitDataBuilder> 
                { 
                    new CardTraitDataBuilder
                    {
                        TraitStateType = VanillaCardTraitTypes.CardTraitStrongerMagicPower,
                        ParamInt = 5
                    }
                },

                EffectBuilders = new List<CardEffectDataBuilder>
                { 
                    
                    // Deal 25 damage
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectDamage",
                        //EffectStateType = VanillaCardEffectTypes.CardEffectDamage,
                        TargetMode = TargetMode.DropTargetCharacter,
                        TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
                        ParamInt = 25
                    },

                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectAddStatusEffect",
                        //EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
                        TargetMode = TargetMode.DropTargetCharacter,
                        TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                            new StatusEffectStackData
                            {
                                statusId = VanillaStatusEffectIDs.Emberdrain,
                                count = 2
                            }
                        }
                    }
                }
            }.BuildAndRegister();
        }
    }
}