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
    class DressedToKill
    {
        public static readonly string ID = Ponies.GUID + "_DressedToKill";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_DressedToKill_Name_Key",
                //Name = "Dressed To Kill",
                //Armor value not updating in description with stackstone. Still doubles applied value, though.
                //Description = "Apply <nobr>+{[trait0.power]}[x][attack]</nobr> and <nobr>{[trait1.power]}[x]<b>Armor</b></nobr> to friendly units.",
                OverrideDescriptionKey = "Pony_Spell_DressedToKill_Description_Key",
                Cost = 0,
                CostType = CardData.CostType.ConsumeRemainingEnergy,
                CardType = CardType.Spell,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = true,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/dressedtokill.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                CardLoreTooltipKeys = new List<string>
                {
                    "Pony_Spell_DressedToKill_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                //Scale via X
                TraitBuilders = new List<CardTraitDataBuilder>
                {
                    /*
                    new CardTraitDataBuilder 
                    {
                        TraitStateName = VanillaCardTraitTypes.CardTraitScalingBuffDamage.AssemblyQualifiedName,
                        ParamTrackedValue = CardStatistics.TrackedValueType.PlayedCost,
                        ParamEntryDuration = CardStatistics.EntryDuration.ThisBattle,
                        ParamUseScalingParams = true,
                        ParamInt = 1,
                        ParamTeamType = Team.Type.Monsters,
                    },*/

                    new CardTraitDataBuilder
                    {
                        TraitStateName = "CardTraitScalingAddStatusEffect",
                        ParamTrackedValue = CardStatistics.TrackedValueType.PlayedCost,
                        ParamEntryDuration = CardStatistics.EntryDuration.ThisBattle,
                        ParamUseScalingParams = true,
                        ParamInt = 2,
                        ParamFloat = 1.0f,
                        ParamTeamType = Team.Type.Monsters,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                            new StatusEffectStackData()
                            {
                                statusId = "armor",
                                count = 0
                            }
                        }
                    }

                },

                //Add 1 Damage and X*2 Armor
                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        //EffectStateType = VanillaCardEffectTypes.CardEffectAddTempCardUpgradeToUnits,
                        EffectStateName = "CardEffectAddTempCardUpgradeToUnits",
                        //EffectStateName = typeof(CustomCardEffectAddTempCardUpgradeToUnitsByTrigger).AssemblyQualifiedName,
                        //ParamTrigger = CharacterTriggerData.Trigger.PostCombat,
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Monsters,
                        ParamCardUpgradeData = new CardUpgradeDataBuilder 
                        { 
                            UpgradeTitle = "DressedToKill",
                            BonusDamage = 1
                        }.Build()
                    },
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
                                statusId = VanillaStatusEffectIDs.Armor,
                                count = 0
                            }
                        }
                    }
                }
            }.BuildAndRegister();
        }
    }
}