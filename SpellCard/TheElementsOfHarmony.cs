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
    class TheElementsOfHarmony
    {
        public static readonly string ID = Ponies.GUID + "_TheElementsOfHarmony";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_TheElementsOfHarmony_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_TheElementsOfHarmony_Description_Key",
                Cost = 1,
                Rarity = CollectableRarity.Rare,
                CardType = CardType.Spell,
                TargetsRoom = false,
                Targetless = true,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/TheElementsOfHarmony.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                CardLoreTooltipKeys = new List<string>
                {
                    "Pony_Spell_TheElementsOfHarmony_Lore_Key"
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
                        //EffectStateType = VanillaCardEffectTypes.CardEffectAddTempCardUpgradeToCardsInHand,
                        //EffectStateName = "CardEffectAddTempCardUpgradeToNextDrawnCard",
                        EffectStateName = typeof(CustomCardEffectAddTempCardUpgradeToCardsInDeck).AssemblyQualifiedName,

                        ParamCardUpgradeData = new CardUpgradeDataBuilder
                        {
                            BonusDamage = 0,
                            BonusHP = 0,
                            UpgradeTitle = "TheElementsOfHarmony_title",

                            Filters = new List<CardUpgradeMaskData>
                            {
                                new CardUpgradeMaskDataBuilderFixed
                                {
                                    CardType = CardType.Monster,
                                }.Build(),
                            },

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