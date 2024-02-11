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
    class ImaginaryFriends
    {
        public static readonly string ID = Ponies.GUID + "_ImaginaryFriends";
        public static RelicData relicData;

        public static void BuildAndRegister()
        {
            relicData = new CollectableRelicDataBuilder
            {
                CollectableRelicID = ID,
                //Name = "ImaginaryFriends",
                NameKey = "Pony_Relic_ImaginaryFriends_Name_Key",
                //Description = "Herd spells require two fewer friendly units to cast.",
                DescriptionKey = "Pony_Relic_ImaginaryFriends_Description_Key",
                RelicPoolIDs = new List<string> { VanillaRelicPoolIDs.MegaRelicPool },
                IconPath = "RelicAssets/ImaginaryFriends.png",
                ClanID = CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID(),
                EffectBuilders = new List<RelicEffectDataBuilder>
                {
                    //This relic works just by existing.
                    //See the CardTraitHerd custom effect.
                    new RelicEffectDataBuilder
                    {
                        //We still need to have an effect so that tooltips can be added.
                        RelicEffectClassType = typeof(RelicEffectOnlyTooltips),
                        ParamSourceTeam = Team.Type.Monsters,
                        ParamInt = 0, 
                        ParamCardType = CardType.Invalid,
                        ParamFloat = 0.0f,
                        AdditionalTooltips = new AdditionalTooltipData[]
                        {
                            new AdditionalTooltipData
                            {
                                //Custom tooltip
                                titleKey = "CardTraitHerd_TooltipTitle",
                                descriptionKey = "CardTraitHerd_TooltipText",
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
                    "Pony_Relic_ImaginaryFriends_Lore_Key"
                },
                UnlockLevel = 7,
            }.BuildAndRegister();

            AccessTools.Field(typeof(RelicData), "relicLoreTooltipStyle").SetValue(relicData, Ponies.PonyRelicTooltip.GetEnum());
        }
    }
}