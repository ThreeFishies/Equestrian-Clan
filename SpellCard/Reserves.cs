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
    class Reserves
    {
        public static readonly string ID = Ponies.GUID + "_Reserves";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                //Name = "Reserves",
                NameKey = "Pony_Spell_Reserves_Name_Key",
                //Description = "<b>Freeze</b> all units in your hand and apply <nobr>+10<sprite name=\"Attack\"> and <sprite name=\"Health\"></nobr>.",
                OverrideDescriptionKey = "Pony_Spell_Reserves_Description_Key",
                Cost = 2,
                Rarity = CollectableRarity.Common,
                CardType = CardType.Spell,
                TargetsRoom = false,
                Targetless = true,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/reserves.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                //CardPoolIDs = new List<string> {  },
                CardLoreTooltipKeys = new List<string> 
                { 
                    "Pony_Spell_Reserves_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,
                UnlockLevel = 1,

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
                            //UpgradeDescription = "Reserves_desc",
                            UpgradeTitle = "Reserves_title",

                            Filters = new List<CardUpgradeMaskData>
                            {
                                new CardUpgradeMaskDataBuilderFixed
                                {
                                   CardType = CardType.Monster,
                                }.Build()
                            },

                            TraitDataUpgradeBuilders = new List<CardTraitDataBuilder> 
                            {
                                new CardTraitDataBuilder 
                                {
                                    TraitStateType = VanillaCardTraitTypes.CardTraitFreeze,
                                }
                            }
                        }.Build()
                    },
                }
            }.BuildAndRegister();
        }
    }
}