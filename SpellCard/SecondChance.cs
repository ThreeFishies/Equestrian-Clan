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
    class SecondChance
    {
        public static readonly string ID = Ponies.GUID + "_SecondChance";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_SecondChance_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_SecondChance_Description_Key",
                Cost = 2,
                Rarity = CollectableRarity.Rare,
                CardType = CardType.Spell,
                TargetsRoom = true,
                Targetless = true,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/SecondChance.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                CardLoreTooltipKeys = new List<string>
                {
                    "Pony_Spell_SecondChance_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                TraitBuilders = new List<CardTraitDataBuilder>
                {
                    new CardTraitDataBuilder
                    {
                        TraitStateName = "CardTraitShowCardTargets"
                    }
                },

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectRecursion",
                        TargetMode = TargetMode.Exhaust,
                        TargetTeamType = Team.Type.None,
                        TargetCardType = CardType.Monster,
                        TargetCharacterSubtype = "SubtypesData_None",
                        ParamInt = 1,

                        ParamCardUpgradeData = new CardUpgradeDataBuilder
                        {
                            BonusDamage = 5,
                            BonusHP = 5,
                            UpgradeTitle = "PartyInvitation_title",

                            TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
                            {
                                new CharacterTriggerDataBuilder
                                {
                                    Trigger = CharacterTriggerData.Trigger.OnUnscaledSpawn,
                                    DisplayEffectHintText = false,
                                    HideTriggerTooltip = true,

                                    EffectBuilders = new List<CardEffectDataBuilder>
                                    {
                                        new CardEffectDataBuilder
                                        {
                                            EffectStateName = typeof(CustomEffectSocial).AssemblyQualifiedName,
                                            HideTooltip = true,
                                            TargetMode = TargetMode.Self
                                        }
                                    }
                                }
                            },

                            StatusEffectUpgrades = new List<StatusEffectStackData>
                            {
                                new StatusEffectStackData()
                                {
                                    statusId = "social",
                                    count = 1
                                }
                            }
                        }.Build()
                    },
                }
            }.BuildAndRegister();
        }
    }
}