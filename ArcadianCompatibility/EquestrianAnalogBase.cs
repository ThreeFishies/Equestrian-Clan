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
using HarmonyLib;

namespace Equestrian.Arcadian
{
    class EquestrianAnalogBase
    {
        public static readonly string ID = Ponies.GUID + "_EquestrianAnalogBase";

        public static void BuildAndRegister()
        { 
            CardData cardData = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Arcadian_Spell_EquestrianAnalog_Name_Key",
                OverrideDescriptionKey = "Arcadian_Spell_EquestrianAnalogBase_Description_Key",
                Cost = 1,
                Rarity = CollectableRarity.Starter,
                CardType = CardType.Spell,
                TargetsRoom = true,
                Targetless = false,
                ClanID = ArcadianCompatibility.ArcadianClan.GetID(),
                AssetPath = "ArcadianCompatibility/Assets/EquestrianAnalog.png",
                CardPoolIDs = new List<string> {  },
                CardLoreTooltipKeys = new List<string>
                {
                    "Arcadian_Spell_EquestrianAnalog_Lore_Key"
                },
                LinkedClass = ArcadianCompatibility.ArcadianClan,
                IgnoreWhenCountingMastery = true,
                LinkedMasteryCard = ArcadianCompatibility.Analog,

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
                                count = 2
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
                                count = 1
                            },
                        }
                    }
                }
            }.BuildAndRegister();

            //AccessTools.Field(typeof(CardData), "ignoreWhenCountingMastery").SetValue(cardData, Cost);
        }
    }
}