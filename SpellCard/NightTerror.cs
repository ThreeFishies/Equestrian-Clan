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
    class NightTerrors
    {
        public static readonly string ID = Ponies.GUID + "_NightTerrors";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                //Name = "Night Terrors",
                NameKey = "Pony_Spell_NightTerrors_Name_Key",
                //Description = "Deal <nobr>{[effect0.power]} damage</nobr> to a target unit twice and gain <nobr>1[ember] on <b>Slay</b></nobr>.",
                OverrideDescriptionKey = "Pony_Spell_NightTerrors_Description_Key",
                Cost = 2,
                Rarity = CollectableRarity.Starter,
                TargetsRoom = true,
                Targetless = false,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/NightTerrors.png",
                CardPoolIDs = new List<string> {  },
                CardLoreTooltipKeys = new List<string> 
                { 
                    "Pony_Spell_NightTerrors_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectDamage",
                        TargetMode = TargetMode.DropTargetCharacter,
                        TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
                        ParamInt = 2
                    },
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectDamage",
                        TargetMode = TargetMode.DropTargetCharacter,
                        TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
                        ParamInt = 2
                    },
                },
                TriggerBuilders = new List<CardTriggerEffectDataBuilder>
                {
                    new CardTriggerEffectDataBuilder
                    {
                        Trigger = CardTriggerType.OnKill,
                        CardEffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateName = "CardEffectGainEnergy",
                                ParamInt = 1
                            }
                        }
                    }
                }
            }.BuildAndRegister();
        }
    }
}