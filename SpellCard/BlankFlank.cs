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
    class BlankFlank
    {
        public static readonly string ID = Ponies.GUID + "_BlankFlank";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_BlankFlank_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_BlankFlank_Description_Key",
                Cost = 0,
                Rarity = CollectableRarity.Uncommon,
                TargetsRoom = true,
                Targetless = false,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/BlankFlank.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Spell_BlankFlank_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                EffectBuilders = new List<CardEffectDataBuilder>
                { 
                    //Actually, do Armor first.
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectRemoveStatusEffect",
                        //EffectStateType = VanillaCardEffectTypes.CardEffectRemoveStatusEffect,
                        TargetMode = TargetMode.DropTargetCharacter,
                        TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                            new StatusEffectStackData
                            {
                                statusId = VanillaStatusEffectIDs.Armor,
                                count = 9999
                            }
                        }
                    },
                    // Remove buffs
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectRemoveAllStatusEffects",
                        //EffectStateType = VanillaCardEffectTypes.CardEffectRemoveAllStatusEffects,
                        TargetMode = TargetMode.LastTargetedCharacters,
                        TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
                        ParamInt = (int)StatusEffectData.DisplayCategory.Positive
                    },
                    // Remove debuffs
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectRemoveAllStatusEffects",
                        //EffectStateType = VanillaCardEffectTypes.CardEffectRemoveAllStatusEffects,
                        TargetMode = TargetMode.LastTargetedCharacters,
                        TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
                        ParamInt = (int)StatusEffectData.DisplayCategory.Negative
                    }
                }
            }.BuildAndRegister();
        }
    }
}