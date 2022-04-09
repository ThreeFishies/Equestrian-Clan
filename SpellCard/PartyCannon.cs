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
    class PartyCannon
    {
        public static readonly string ID = Ponies.GUID + "_PartyCannon";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_PartyCannon_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_PartyCannon_Description_Key",
                Cost = 3,
                Rarity = CollectableRarity.Uncommon,
                TargetsRoom = true,
                Targetless = true,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/PartyCannon.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Spell_PartyCannon_Lore_Key"
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
                    
                    // Deal 60 damage
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectDamage",
                        //EffectStateType = VanillaCardEffectTypes.CardEffectDamage,
                        TargetMode = TargetMode.FrontInRoom,
                        TargetTeamType = Team.Type.Heroes,
                        ParamInt = 60
                    },

                    //Dazed 3
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectAddStatusEffect",
                        //EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
                        TargetMode = TargetMode.LastTargetedCharacters,
                        TargetTeamType = Team.Type.Heroes,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                            new StatusEffectStackData
                            {
                                statusId = VanillaStatusEffectIDs.Dazed,
                                count = 3
                            }
                        }
                    }
                }
            }.BuildAndRegister();
        }
    }
}