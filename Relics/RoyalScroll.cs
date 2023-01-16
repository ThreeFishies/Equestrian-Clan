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
    class RoyalScroll
    {
        public static readonly string ID = Ponies.GUID + "_RoyalScrollRelic";
        public static RelicData relicData;

        public static void BuildAndRegister()
        {
            relicData = new CollectableRelicDataBuilder
            {
                CollectableRelicID = ID,
                NameKey = "Pony_Relic_RoyalScroll_Name_Key",
                DescriptionKey = "Pony_Relic_RoyalScroll_Description_Key",
                RelicPoolIDs = new List<string> { VanillaRelicPoolIDs.MegaRelicPool },
                IconPath = "RelicAssets/RoyalScroll.png",
                ClanID = CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID(),

                EffectBuilders = new List<RelicEffectDataBuilder>
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassType = typeof(CustomRelicEffecyModifyCardCostByType), 
                        ParamSourceTeam = Team.Type.Monsters,
                        ParamInt = -99,
                        ParamFloat = 0.0f,
                        ParamUseIntRange = false,
                        ParamMinInt = 0,
                        ParamMaxInt = 0,
                        ParamString = "",
                        ParamCharacterSubtype = "SubtypesData_None",
                        ParamExcludeCharacterSubtypes = new string[]{ },
                        ParamSpecialCharacterType = 0,
                        ParamBool = false,
                        ParamStatusEffects = new StatusEffectStackData[]{},
                        ParamCardEffects = new List<CardEffectData> { },
                        ParamTrigger = CharacterTriggerData.Trigger.OnDeath,
                        ParamTargetMode = TargetMode.Room,
                        ParamCardType = CardType.Spell,
                        ParamCardUpgradeData = new CardUpgradeDataBuilder
                        { 
                            CostReduction = 99,
                            Filters = new List<CardUpgradeMaskData>
                            { 
                                new CardUpgradeMaskDataBuilderFixed
                                { 
                                    CardType = CardType.Spell,
                                }.Build()
                            }
                        }.Build(),

                        EffectConditionBuilders = new List<RelicEffectConditionBuilder>
                        {
                            new RelicEffectConditionBuilder
                            {
                                paramCardType = CardStatistics.CardTypeTarget.Spell,
                                allowMultipleTriggersPerDuration = false,
                                paramInt = 1,
                                paramTrackTriggerCount = true,
                                paramTrackedValue = CardStatistics.TrackedValueType.AnyCardPlayed,
                                paramEntryDuration = CardStatistics.EntryDuration.ThisTurn,
                                paramSubtype = "SubtypesData_None",
                                paramComparator = RelicEffectCondition.Comparator.Equal,
                            }
                        },
                    }
                },
                FromStoryEvent = false,
                IsBossGivenRelic = false,
                LinkedClass = Ponies.EquestrianClanData,
                Rarity = CollectableRarity.Common,
                RelicLoreTooltipKeys = new List<string>
                {
                    "Pony_Relic_RoyalScroll_Lore_Key"
                },
                //RelicActivatedKey = "Pony_Relic_RoyalScroll_Activated_Key",
                UnlockLevel = 0,
            }.BuildAndRegister();

            AccessTools.Field(typeof(RelicData), "relicLoreTooltipStyle").SetValue(relicData, Ponies.PonyRelicTooltip.GetEnum());
        }
    }
}