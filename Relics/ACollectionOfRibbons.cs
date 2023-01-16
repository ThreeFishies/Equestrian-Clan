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

namespace Equestrian.Unused
{
    class ACollectionOfRibbons
    {
        public static readonly string ID = Ponies.GUID + "_ACollectionOfRibbonsRelic";
        public static RelicData relicData;

        public static void BuildAndRegister()
        {
            relicData = new CollectableRelicDataBuilder
            {
                CollectableRelicID = ID,
                NameKey = "Pony_Relic_ACollectionOfRibbons_Name_Key",
                DescriptionKey = "Pony_Relic_ACollectionOfRibbons_Description_Key",
                RelicPoolIDs = new List<string> { VanillaRelicPoolIDs.MegaRelicPool },
                IconPath = "RelicAssets/ACollectionOfRibbons.png",
                ClanID = CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID(),
                EffectBuilders = new List<RelicEffectDataBuilder>
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassType = typeof(CustomRelicEffectGainEnergyAndCardDrawOnCardTypePlayed), 
                        ParamSourceTeam = Team.Type.Monsters,
                        ParamInt = 2, //Energy gain
                        ParamMinInt = 1, //Cards to draw
                        ParamCardType = CardType.Junk,
                        ParamMaxInt = (int)CardType.Blight,

                        EffectConditionBuilders = new List<RelicEffectConditionBuilder>
                        {
                            new RelicEffectConditionBuilder
                            {
                                paramCardType = CardStatistics.CardTypeTarget.Any,
                                allowMultipleTriggersPerDuration = false,
                                paramInt = 1,
                                paramTrackTriggerCount = true,
                                paramTrackedValue = CardStatistics.TrackedValueType.AnyCardPlayed,
                                paramEntryDuration = CardStatistics.EntryDuration.ThisTurn,
                                paramSubtype = "SubtypesData_None",
                                paramComparator = RelicEffectCondition.Comparator.Equal | RelicEffectCondition.Comparator.GreaterThan,
                            }
                        },
                    },
                },
                FromStoryEvent = false,
                IsBossGivenRelic = false,
                LinkedClass = Ponies.EquestrianClanData,
                Rarity = CollectableRarity.Common,
                RelicLoreTooltipKeys = new List<string>
                {
                    "Pony_Relic_ACollectionOfRibbons_Lore_Key"
                },
                //UnlockLevel = 6,
                RelicActivatedKey = "Pony_Relic_ACollectionOfRibbons_Activated_Key",
            }.BuildAndRegister();

            AccessTools.Field(typeof(RelicData), "relicLoreTooltipStyle").SetValue(relicData, Ponies.PonyRelicTooltip.GetEnum());
        }
    }
}