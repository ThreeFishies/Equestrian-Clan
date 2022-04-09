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
    class PartyInvitation 
    {
        public static readonly string ID = Ponies.GUID + "_PartyInvitation";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_PartyInvitation_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_PartyInvitation_Description_Key",
                Cost = 0,
                Rarity = CollectableRarity.Common,
                CardType = CardType.Spell,
                TargetsRoom = true,
                Targetless = true,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/PartyInvitation.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                CardLoreTooltipKeys = new List<string>
                {
                    "Pony_Spell_PartyInvitation_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                TraitBuilders = new List<CardTraitDataBuilder>
                {
                    new CardTraitDataBuilder
                    {
                        TraitStateName = "CardTraitExhaustState"
                    },

                    new CardTraitDataBuilder
                    {
                        TraitStateName = "CardTraitShowCardTargets"
                    }
                },

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        //EffectStateType = VanillaCardEffectTypes.CardEffectAddTempCardUpgradeToNextDrawnCard,
                        EffectStateName = "CardEffectAddTempCardUpgradeToNextDrawnCard",

                        ParamCardUpgradeData = new CardUpgradeDataBuilder
                        {
                            BonusDamage = 5,
                            BonusHP = 5,
                            //UpgradeDescription = "PartyInvitation_desc",
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
                    new CardEffectDataBuilder
                    { 
                        //EffectStateType = VanillaCardEffectTypes.CardEffectDrawType,
                        EffectStateName = "CardEffectDrawType",
                        TargetMode = TargetMode.DrawPile,
                        TargetCardType = CardType.Monster,
                        ParamInt = 1
                    }
                }
            }.BuildAndRegister();
        }
    }
}