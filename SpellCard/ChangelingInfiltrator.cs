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
using Equestrian.CardPools;

namespace Equestrian.SpellCards
{
    class ChangelingInfiltrator
    {
        public static readonly string ID = Ponies.GUID + "_ChangelingInfiltrator";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_ChangelingInfiltrator_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_ChangelingInfiltrator_Description_Key",
                Cost = 3,
                Rarity = CollectableRarity.Rare,
                TargetsRoom = true,
                Targetless = false,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/ChangelingInfiltrator.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                //CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Spell_ChangelingInfiltrator_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,
                UnlockLevel = 6,

                TraitBuilders = new List<CardTraitDataBuilder> 
                { 
                    new CardTraitDataBuilder 
                    {
                        TraitStateName = "CardTraitExhaustState",
                    }
                },

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder 
                    {
                        EffectStateName = "CardEffectAddStatusEffect",
                        TargetMode = TargetMode.DropTargetCharacter,
                        TargetTeamType = Team.Type.Monsters,
                        TargetIgnoreBosses = true,
                        TargetCharacterSubtype = "SubtypesData_None",
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                            new StatusEffectStackData
                            {
                                statusId = VanillaStatusEffectIDs.HealImmunity,
                                count = 1
                            }
                        }
                    },
                    new CardEffectDataBuilder
                    {
                        //EffectStateType = VanillaCardEffectTypes.CardEffectCopyUnits,
                        EffectStateName = "CardEffectCopyUnits",
                        TargetMode = TargetMode.LastTargetedCharacters,
                        TargetTeamType = Team.Type.Monsters,
                        TargetIgnoreBosses = true,
                        TargetCharacterSubtype = "SubtypesData_None",
                        //TargetCardType = CardType.Spell,
                        ParamInt = 1,
                        IgnoreTemporaryModifiersFromSource = false,
                        CopyModifiersFromSource = true,
                    },
                }
            }.BuildAndRegister();
        }
    }
}