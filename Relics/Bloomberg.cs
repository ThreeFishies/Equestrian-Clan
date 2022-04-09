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
using Equestrian.Enhancers;

namespace Equestrian.Relic
{
    class Bloomberg
    {
        public static readonly string ID = Ponies.GUID + "_Bloomberg";

        public static void BuildAndRegister()
        {
            RelicEffectData bloombergEffectData = new RelicEffectDataBuilder
            {
                //Based off of Capritious Reflection
                RelicEffectClassType = typeof(CustomRelicEffectAddStartingUpgradeToCardDrafts),
                ParamSourceTeam = Team.Type.None,
                ParamStatusEffects = new StatusEffectStackData[] { },
                ParamInt = 0,
                ParamUseIntRange = false,
                ParamMinInt = 0,
                ParamMaxInt = 0,
                ParamString = "",
                ParamCharacterSubtype = "SubtypesData_None",
                ParamExcludeCharacterSubtypes = new string[] { },
                ParamSpecialCharacterType = 0,
                ParamBool = false,
                ParamCardEffects = new List<CardEffectData> { },
                ParamTrigger = CharacterTriggerData.Trigger.OnDeath,
                ParamTargetMode = TargetMode.Room,
                ParamCardType = CardType.Monster,
                ParamCardFilter = new CardUpgradeMaskDataBuilderFixed
                {
                    CardType = CardType.Monster
                }.Build(),
                AdditionalTooltips = new AdditionalTooltipData[] 
                { 
                    new AdditionalTooltipData 
                    {
                        titleKey = "Pony_Relic_Bloomberg_Largestone_Title_Key",
                        descriptionKey = "Pony_Relic_Bloomberg_Largestone_Description_Key",
                        isStatusTooltip = false,
                        isTriggerTooltip = false,
                        isTipTooltip = true,
                        style = TooltipDesigner.TooltipDesignType.Default,
                    }
                },
            }.Build();

            /*
            EnhancerPool largeStonePool = UnityEngine.ScriptableObject.CreateInstance<EnhancerPool>();
            largeStonePool.name = "largeStonePool";

            var enhancerDataList = (Malee.ReorderableArray<EnhancerData>)AccessTools.Field(typeof(List<EnhancerData>), "relicDataList").GetValue(largeStonePool);
            enhancerDataList.Add(ProviderManager.SaveManager.GetAllGameData().FindEnhancerData(VanillaUnitEnhancers.Largestone));

            AccessTools.Field(typeof(RelicEffectData), "paramEnhancerPool").SetValue(bloombergEffectData, largeStonePool);
            */

            CollectableRelicData bloombergData = new CollectableRelicDataBuilder
            {
                CollectableRelicID = ID,
                NameKey = "Pony_Relic_Bloomberg_Name_Key",
                DescriptionKey = "Pony_Relic_Bloomberg_Description_Key",
                RelicPoolIDs = new List<string> { VanillaRelicPoolIDs.MegaRelicPool },
                IconPath = "RelicAssets/Bloomberg.png",
                ClanID = CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID(),
                //EffectBuilders = new List<RelicEffectDataBuilder>
                Effects = new List<RelicEffectData> 
                {
                    bloombergEffectData
                },
                FromStoryEvent = false,
                IsBossGivenRelic = false,
                LinkedClass = Ponies.EquestrianClanData,
                Rarity = CollectableRarity.Common,
                RelicLoreTooltipKeys = new List<string>
                {
                    "Pony_Relic_Bloomberg_Lore_Key"
                },
                RelicActivatedKey = "",
                UnlockLevel = 0,
                
            }.BuildAndRegister();
        }
    }
}