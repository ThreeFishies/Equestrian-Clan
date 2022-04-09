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
    class SpaTreatment
    {
        public static readonly string ID = Ponies.GUID + "_SpaTreatment";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_SpaTreatment_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_SpaTreatment_Description_Key",
                Cost = 2,
                Rarity = CollectableRarity.Uncommon,
                TargetsRoom = true,
                Targetless = false,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/SpaTreatment.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Spell_SpaTreatment_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    // Apply Armor 10
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectAddStatusEffect",
                        TargetMode = TargetMode.DropTargetCharacter,
                        TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                            new StatusEffectStackData
                            {
                                statusId = VanillaStatusEffectIDs.Armor,
                                count = 10
                            }
                        }
                    },
                    // Heal for 3
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectHeal",
                        TargetMode = TargetMode.LastTargetedCharacters,
                        TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
                        ParamInt = 3
                    },
                    // Heal for 3
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectHeal",
                        TargetMode = TargetMode.LastTargetedCharacters,
                        TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
                        ParamInt = 3
                    },
                    // Heal for 3
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectHeal",
                        TargetMode = TargetMode.LastTargetedCharacters,
                        TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
                        ParamInt = 3
                    }
                }
            }.BuildAndRegister();
        }
    }
}