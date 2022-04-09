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
using CustomEffects;

namespace Equestrian.SpellCards
{
    class FanClub
    {
        public static readonly string ID = Ponies.GUID + "_FanClub";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                NameKey = "Pony_Spell_FanClub_Name_Key",
                OverrideDescriptionKey = "Pony_Spell_FanClub_Description_Key",
                Cost = 1,
                Rarity = CollectableRarity.Uncommon,
                TargetsRoom = true,
                Targetless = true,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/FanClub.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                //CardPoolIDs = new List<string> {  },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Spell_FanClub_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,
                UnlockLevel = 3,

                TraitBuilders = new List<CardTraitDataBuilder> 
                { 
                    //Herd 3
                    new CardTraitDataBuilder
                    {
                        TraitStateName = typeof(CardTraitHerd).AssemblyQualifiedName,
                        ParamInt = 3,
                    },

                    new CardTraitDataBuilder 
                    {
                        TraitStateName = "CardTraitExhaustState",
                    }
                },

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        //EffectStateType = VanillaCardEffectTypes.CardEffectAddBattleCard,
                        EffectStateName = "CardEffectAddBattleCard",
                        TargetMode = TargetMode.Hand,
                        ParamInt = (int)CardPile.HandPile,
                        AdditionalParamInt = 1,
                        ParamCardPool = MyCardPools.FilliesCardPool,
                    },

                    new CardEffectDataBuilder
                    {
                        //EffectStateType = VanillaCardEffectTypes.CardEffectAdjustRoomCapacity
                        EffectStateName = "CardEffectAdjustRoomCapacity",
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Monsters,
                        ParamInt = 2
                    },

                    new CardEffectDataBuilder
                    { 
                        EffectStateName = typeof(CardEffectHerdTooltip).AssemblyQualifiedName
                    }
                }
            }.BuildAndRegister();
        }
    }
}