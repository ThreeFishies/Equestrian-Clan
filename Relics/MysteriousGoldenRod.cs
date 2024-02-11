using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Trainworks.Builders;
using Trainworks.Managers;
using Trainworks.Enums;
using Trainworks.Constants;
using Equestrian.Init;
using Equestrian;
using Equestrian.CardPools;
using CustomEffects;

namespace Equestrian.Relic
{
    class MysteriousGoldenRod
    {
        public static readonly string ID = Ponies.GUID + "_MysteriousGoldenRodRelic";
        public static RelicData relicData;

        public static void BuildAndRegister()
        {
            relicData = new CollectableRelicDataBuilder
            {
                CollectableRelicID = ID,
                NameKey = "Pony_Relic_MysteriousGoldenRod_Name_Key",
                DescriptionKey = "Pony_Relic_MysteriousGoldenRod_Description_Key",
                RelicPoolIDs = new List<string> { VanillaRelicPoolIDs.MegaRelicPool },
                IconPath = "RelicAssets/MysteriousGoldenRod.png",
                ClanID = CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID(),
                EffectBuilders = new List<RelicEffectDataBuilder>
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassType = typeof(CustomRelicEffectCopyCardOnCardPlay),
                        ParamSourceTeam = Team.Type.Monsters,
                        ParamInt = 1, //number of copies
                        ParamCardType = CardType.Spell,
                        Traits = new List<CardTraitData>
                        {
                            new CardTraitDataBuilder
                            {
                                //TraitStateType = VanillaCardTraitTypes.CardTraitExhaustState,
                                TraitStateName = "CardTraitExhaustState"
                            }.Build()
                        },
                        EffectConditionBuilders = new List<RelicEffectConditionBuilder>
                        {
                            new RelicEffectConditionBuilder
                            {
                                paramCardType = CardStatistics.CardTypeTarget.Any,
                                allowMultipleTriggersPerDuration = false,
                                paramInt = 1,
                                paramTrackTriggerCount = true,
                                paramTrackedValue = CardStatistics.TrackedValueType.SubtypeInDeck,
                                paramEntryDuration = CardStatistics.EntryDuration.ThisTurn,
                                paramSubtype = "SubtypesData_None",
                                paramComparator = RelicEffectCondition.Comparator.Equal | RelicEffectCondition.Comparator.GreaterThan,
                            }
                        },
                        AdditionalTooltips = new AdditionalTooltipData[]
                        {
                            new AdditionalTooltipData
                            {
                                //Bult-in tooltip
                                titleKey = "CardTraitExhaustState_CardText",
                                descriptionKey = "CardTraitExhaustState_TooltipText",
                                style = TooltipDesigner.TooltipDesignType.Keyword,
                                isStatusTooltip = false,
                                statusId = "",
                                isTriggerTooltip = false,
                                trigger = 0,
                                isTipTooltip = false
                            }
                        }
                    }
                },
                FromStoryEvent = false,
                IsBossGivenRelic = false,
                LinkedClass = Ponies.EquestrianClanData,
                Rarity = CollectableRarity.Common,
                RelicLoreTooltipKeys = new List<string>
                {
                    "Pony_Relic_MysteriousGoldenRod_Lore_Key"
                },
                //UnlockLevel = 6,
                RelicActivatedKey = "Pony_Relic_MysteriousGoldenRod_Activated_Key",
            }.BuildAndRegister();

            AccessTools.Field(typeof(RelicData), "relicLoreTooltipStyle").SetValue(relicData, Ponies.PonyRelicTooltip.GetEnum());
        }
    }
}