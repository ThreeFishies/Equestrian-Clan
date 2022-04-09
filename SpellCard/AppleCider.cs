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
    class AppleCider
    {
        public static readonly string ID = Ponies.GUID + "_AppleCider";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_AppleCider_Name_Key",
                //Name = "Apple Cider",
                //Description = "Heal friendly units for {[effect1.power]} and apply <nobr><b>Regen</b> {[effect0.status0.power]}</nobr>.",
                OverrideDescriptionKey = "Pony_Spell_AppleCider_Description_Key",
                Cost = 1,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = true,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/applecider.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                CardLoreTooltipKeys = new List<string> 
                { 
                    "Pony_Spell_AppleCider_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    // Apply Regen 1
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectAddStatusEffect",
                        //EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Monsters,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                            new StatusEffectStackData
                            {
                                statusId = VanillaStatusEffectIDs.Regen,
                                count = 1
                            }
                        }
                    },
                    
                    // Heal for 3
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectHeal",
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Monsters,
                        ParamInt = 3
                    }
                }
            }.BuildAndRegister();
        }
    }
}