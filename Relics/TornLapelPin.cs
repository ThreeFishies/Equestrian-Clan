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

namespace Equestrian.Relic
{
    class TornLapelPin
    {
        public static readonly string ID = Ponies.GUID + "_TornLapelPin";
        public static RelicData relicData;

        public static void BuildAndRegister()
        {
            relicData = new CollectableRelicDataBuilder
            {
                CollectableRelicID = ID,
                NameKey = "Pony_Relic_TornLapelPin_Name_Key",
                DescriptionKey = "Pony_Relic_TornLapelPin_Description_Key",
                RelicPoolIDs = new List<string> { VanillaRelicPoolIDs.MegaRelicPool },
                IconPath = "RelicAssets/TornLapelPin.png",
                ClanID = CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID(),
                EffectBuilders = new List<RelicEffectDataBuilder>
                {
                    //This relic works just by existing.
                    //See the StopTrackingCosts Harmony patch.
                },
                FromStoryEvent = false,
                IsBossGivenRelic = false,
                LinkedClass = Ponies.EquestrianClanData,
                Rarity = CollectableRarity.Common,
                RelicLoreTooltipKeys = new List<string> 
                {
                    "Pony_Relic_TornLapelPin_Lore_Key"
                }
            }.BuildAndRegister();

            AccessTools.Field(typeof(RelicData), "relicLoreTooltipStyle").SetValue(relicData, Ponies.PonyRelicTooltip.GetEnum());
        }
    }
}