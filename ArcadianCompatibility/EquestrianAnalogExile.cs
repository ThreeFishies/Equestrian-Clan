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
    class EquestrianAnalogExile
    {
        public static readonly string ID = Ponies.GUID + "_EquestrianAnalogExile";

        public static void BuildAndRegister()
        {
            CardData cardData = new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Arcadian_Spell_EquestrianAnalog_Name_Key",
                OverrideDescriptionKey = "Arcadian_Spell_EquestrianAnalogExile_Description_Key",
                Cost = 1,
                Rarity = CollectableRarity.Starter,
                CardType = CardType.Spell,
                TargetsRoom = true,
                Targetless = true,
                ClanID = ArcadianCompatibility.ArcadianClan.GetID(),
                AssetPath = "ArcadianCompatibility/Assets/EquestrianAnalog.png",
                CardPoolIDs = new List<string> { },
                CardLoreTooltipKeys = new List<string>
                {
                    "Arcadian_Spell_EquestrianAnalog_Lore_Key"
                },
                LinkedClass = ArcadianCompatibility.ArcadianClan,
                IgnoreWhenCountingMastery = true,
                LinkedMasteryCard = ArcadianCompatibility.Analog,
                SharedMasteryCards = new List<CardData>() 
                { 
                    VampireFruitBat.VampireFruitBatCardData
                },

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectAddBattleCard",
                        //EffectStateType = VanillaCardEffectTypes.CardEffectAddBattleCard,
                        TargetMode = TargetMode.Hand,
                        ParamInt = (int)CardPile.HandPile,
                        AdditionalParamInt = 1,
                        ParamCardPool = ArcadianCompatibility.VampireFruitBatCardPool,
                    },
                }
            }.BuildAndRegister();

            //AccessTools.Field(typeof(CardData), "ignoreWhenCountingMastery").SetValue(cardData, Cost);
        }
    }
}