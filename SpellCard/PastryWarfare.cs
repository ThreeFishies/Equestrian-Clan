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
    class PastryWarfare
    {
        public static readonly string ID = Ponies.GUID + "_PastryWarfare";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                //Name = "Pastry Warfare",
                NameKey = "Pony_Spell_PastryWarfare_Name_Key",
                //Description = "Apply <nobr><b>Dazed</b> {[effect0.status0.power]}</nobr> to a random enemy unit three times.",
                OverrideDescriptionKey = "Pony_Spell_PastryWarfare_Description_Key",
                Cost = 1,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = true,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/PastryWarfare.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Spell_PastryWarfare_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    // Apply Dazed 1 
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectAddStatusEffect",
                        TargetMode = TargetMode.RandomInRoom,
                        TargetTeamType = Team.Type.Heroes,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                            new StatusEffectStackData
                            {
                                statusId = VanillaStatusEffectIDs.Dazed,
                                count = 1
                            }
                        }
                    },
                                        
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectAddStatusEffect",
                        TargetMode = TargetMode.RandomInRoom,
                        TargetTeamType = Team.Type.Heroes,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                            new StatusEffectStackData
                            {
                                statusId = VanillaStatusEffectIDs.Dazed,
                                count = 1
                            }
                        }
                    },
                                      
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectAddStatusEffect",
                        TargetMode = TargetMode.RandomInRoom,
                        TargetTeamType = Team.Type.Heroes,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                            new StatusEffectStackData
                            {
                                statusId = VanillaStatusEffectIDs.Dazed,
                                count = 1
                            }
                        }
                    }
                }
            }.BuildAndRegister();
        }
    }
}