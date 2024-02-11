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
    class VIPList
    {
        public static readonly string ID = Ponies.GUID + "_VIPList";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_VIPList_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_VIPList_Description_Key",
                Cost = 1,
                Rarity = CollectableRarity.Uncommon,
                CardType = CardType.Spell,
                TargetsRoom = false,
                Targetless = true,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/VIPList.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                CardLoreTooltipKeys = new List<string>
                {
                    "Pony_Spell_VIPList_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                TraitBuilders = new List<CardTraitDataBuilder>
                {
                    new CardTraitDataBuilder
                    {
                        TraitStateName = "CardTraitPermafrost"
                    },

                    new CardTraitDataBuilder
                    {
                        TraitStateName = "CardTraitExhaustState"
                    }
                },

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = VanillaCardEffectTypes.CardEffectAddTempCardUpgradeToCardsInHand,
                        TargetCardType = CardType.Monster,
                        TargetMode = TargetMode.Hand,
                        ParamCardUpgradeData = new CardUpgradeDataBuilder
                        {
                            BonusDamage = 10,
                            BonusHP = 10,
                            //UpgradeDescription = "VIPList_desc",
                            UpgradeTitle = "VIPList_title",
                            Filters = new List<CardUpgradeMaskData>
                            {
                                new CardUpgradeMaskDataBuilderFixed
                                {
                                    CardType = CardType.Monster,
                                }.Build()
                            },

                            TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
                            {
                                new CharacterTriggerDataBuilder
                                {
                                    Trigger = CharacterTriggerData.Trigger.OnUnscaledSpawn,
                                    HideTriggerTooltip = true,
                                    DisplayEffectHintText = false,
                                    //DescriptionKey = "CardEffectSocial_Description",

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