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

namespace Equestrian.SpellCards
{
    class FirstAid 
    {
        public static readonly string ID = Ponies.GUID + "_FirstAid";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_FirstAid_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_FirstAid_Description_Key",
                Cost = 0,
                Rarity = CollectableRarity.Uncommon,
                CardType = CardType.Spell,
                TargetsRoom = true,
                Targetless = false,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/FirstAid.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                CardLoreTooltipKeys = new List<string>
                {
                    "Pony_Spell_FirstAid_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                TraitBuilders = new List<CardTraitDataBuilder>
                {
                    new CardTraitDataBuilder
                    {
                        TraitStateName = "CardTraitExhaustState"
                    },
                },

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectAddStatusEffect",
                        //EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
                        TargetMode = TargetMode.DropTargetCharacter,
                        TargetTeamType = Team.Type.Heroes | Team.Type.Monsters,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                            new StatusEffectStackData
                            {
                                statusId = VanillaStatusEffectIDs.Armor,
                                count = 5
                            },
                        }
                    },
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectAddStatusEffect",
                        //EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
                        TargetMode = TargetMode.LastTargetedCharacters,
                        TargetTeamType = Team.Type.Heroes | Team.Type.Monsters,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                            new StatusEffectStackData
                            {
                                statusId = VanillaStatusEffectIDs.Regen,
                                count = 3
                            },
                        }
                    }
                }
            }.BuildAndRegister();
        }
    }
}