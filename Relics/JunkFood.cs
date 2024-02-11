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
    class JunkFood
    {
        public static readonly string ID = Ponies.GUID + "_JunkFoodRelic";
        public static RelicData relicData;

        public static void BuildAndRegister()
        {
            relicData = new CollectableRelicDataBuilder
            {
                CollectableRelicID = ID,
                NameKey = "Pony_Relic_JunkFood_Name_Key",
                DescriptionKey = "Pony_Relic_JunkFood_Description_Key",
                //Description = "At the start of your turn, add a random pony to your hand.",
                RelicPoolIDs = new List<string> { VanillaRelicPoolIDs.MegaRelicPool },
                IconPath = "RelicAssets/JunkFood.png",
                ClanID = CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID(),
                EffectBuilders = new List<RelicEffectDataBuilder>
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassName = "RelicEffectAddBattleCardToHand",
                        ParamInt = 1,
                        ParamCardPool = MyCardPools.GivePonyCardPool,
                        ParamTrigger = CharacterTriggerData.Trigger.PreCombat,
                        ParamTargetMode = TargetMode.FrontInRoom
                    }
                },
                Rarity = CollectableRarity.Common,
                RelicLoreTooltipKeys = new List<string>
                {
                    "Pony_Relic_JunkFood_Lore_Key"
                },
                IsBossGivenRelic = false,
                FromStoryEvent = false,
                LinkedClass = Ponies.EquestrianClanData,
                RelicActivatedKey = "Pony_Relic_JunkFood_Activated_Key",
                UnlockLevel = 2,
            }.BuildAndRegister();

            AccessTools.Field(typeof(RelicData), "relicLoreTooltipStyle").SetValue(relicData, Ponies.PonyRelicTooltip.GetEnum());
        }
    }
}