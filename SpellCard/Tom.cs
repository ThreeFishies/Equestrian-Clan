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
    class Tom
    {
        public static readonly string ID = Ponies.GUID + "_Tom";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_Tom_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_Tom_Description_Key",
                Cost = 0,
                Rarity = CollectableRarity.Rare,
                TargetsRoom = true,
                Targetless = true,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/Tom.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Spell_Tom_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                /*
                TraitBuilders = new List<CardTraitDataBuilder> 
                { 
                    new CardTraitDataBuilder
                    { 
                        //TraitStateType = VanillaCardTraitTypes.CardTraitIgnoreArmor,
                        TraitStateName = "CardTraitIgnoreArmor",
                    }
                },
                */

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        //EffectStateType = VanillaCardEffectTypes.CardEffectAddBattleCard,
                        EffectStateName = "CardEffectAddBattleCard",
                        TargetMode = TargetMode.Hand,
                        ParamInt = (int)CardPile.DeckPileTop,
                        AdditionalParamInt = 1,
                        ParamCardPool = MyCardPools.DeadweightOnlyCardPool,
                    },

                    new CardEffectDataBuilder
                    {
                        //EffectStateType = VanillaCardEffectTypes.CardEffectDamage,
                        EffectStateName = "CardEffectDamage",
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Heroes,
                        ParamInt = 12
                    },

                    new CardEffectDataBuilder 
                    { 
                        EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
                        EffectStateName = "CardEffectAddStatusEffect",
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Heroes,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                            new StatusEffectStackData
                            { 
                                statusId = VanillaStatusEffectIDs.Dazed,
                                count = 1
                            }
                        }
                    }
                }
            }.BuildAndRegister();
        }
    }
}