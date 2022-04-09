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
using Equestrian.MonsterCards;

namespace Equestrian.Relic
{
    class TheSeventhElement
    {
        public static readonly string ID = Ponies.GUID + "_TheSeventhElementRelic";

        public static void BuildAndRegister()
        {
            new CollectableRelicDataBuilder
            {
                CollectableRelicID = ID,
                NameKey = "Pony_Relic_TheSeventhElement_Name_Key",
                DescriptionKey = "Pony_Relic_TheSecondSeventhElement_Description_Key",
                RelicPoolIDs = new List<string> { VanillaRelicPoolIDs.MegaRelicPool },
                IconPath = "RelicAssets/TheSeventhElement.png",
                ClanID = CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID(),
                EffectBuilders = new List<RelicEffectDataBuilder>
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassType = typeof(RelicEffectModifyTriggerCount),
                        ParamSourceTeam = Team.Type.Monsters,
                        ParamStatusEffects = new StatusEffectStackData[]{ },
                        ParamInt = 1, //add 1 additional trigger
                        ParamUseIntRange = false,
                        ParamMinInt = 0,
                        ParamMaxInt = 0,
                        ParamString = "",
                        ParamCharacterSubtype = "SubtypesData_None",
                        ParamExcludeCharacterSubtypes = new string[]{ },
                        ParamSpecialCharacterType = 0,
                        ParamBool = true, //sets to all triggers
                        ParamCardEffects = new List<CardEffectData>{ },
                        ParamTrigger = CharacterTriggerData.Trigger.OnDeath,
                        ParamTargetMode = TargetMode.FrontInRoom,
                        ParamCardType = CardType.Monster,

                        //This doesn't actually do anything. A patch does the filtering.
                        ParamCardFilter = new CardUpgradeMaskDataBuilderFixed
                        { 
                            RequiredStatusEffects = new List<StatusEffectStackData>
                            {
                                new StatusEffectStackData
                                {
                                    statusId = "social",
                                    count = 1,
                                }
                            }
                        }.Build(),

                        AdditionalTooltips = new AdditionalTooltipData[]
                        {
                            new AdditionalTooltipData
                            {
                                titleKey = "CardEffectSocial_TooltipTitle",
                                descriptionKey = "StatusEffect_social_CardTooltipText",
                                isStatusTooltip = true,
                                isTriggerTooltip = false,
                                isTipTooltip = false,
                                style = TooltipDesigner.TooltipDesignType.Persistent,
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
                    "Pony_Relic_TheSeventhElement_Lore_Key"
                },
                RelicActivatedKey = "",
                //UnlockLevel = 6,
            }.BuildAndRegister();
        }
    }
}