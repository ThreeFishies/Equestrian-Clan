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
    class BottledCutieMark
    {
        public static readonly string ID = Ponies.GUID + "_BottledCutieMarkRelic";
        public static CardUpgradeData BottledCutieMarkUpgrade;
        public static RelicData relicData;

        public static void BuildAndRegister()
        {
            BottledCutieMark.BottledCutieMarkUpgrade = new CardUpgradeDataBuilder
            {
                BonusDamage = 5,
                BonusHP = 5,

                StatusEffectUpgrades = new List<StatusEffectStackData>
                {
                    new StatusEffectStackData
                    {
                        statusId = "social",
                        count = 1,
                    }
                },

                TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
                {
                    new CharacterTriggerDataBuilder
                    {
                        Trigger = CharacterTriggerData.Trigger.OnUnscaledSpawn,
                        DisplayEffectHintText = false,
                        HideTriggerTooltip = true,

                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateName = typeof(CustomEffectSocial).AssemblyQualifiedName,
                                TargetMode = TargetMode.Self,
                            }
                        }
                    }
                },

                Filters = new List<CardUpgradeMaskData>
                {
                    new CardUpgradeMaskDataBuilderFixed
                    {
                        CardType = CardType.Monster,
                        RequiredSubtypes = new List<string>
                        {
                            SubtypePony.Key
                        }
                    }.Build()
                }
            }.Build();

            relicData = new CollectableRelicDataBuilder
            {
                CollectableRelicID = ID,
                NameKey = "Pony_Relic_BottledCutieMark_Name_Key",
                DescriptionKey = "Pony_Relic_BottledCutieMark_Description_Key",
                RelicPoolIDs = new List<string> { VanillaRelicPoolIDs.MegaRelicPool },
                IconPath = "RelicAssets/BottledCutieMark.png",
                ClanID = CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID(),
                EffectBuilders = new List<RelicEffectDataBuilder>
                {
                    new RelicEffectDataBuilder
                    { 
                        RelicEffectClassType = typeof(RelicEffectAddTempUpgrade),
                        ParamInt = 0,
                        ParamSourceTeam = Team.Type.Monsters,
                        ParamCharacterSubtype = SubtypePony.Key,
                        ParamTargetMode = TargetMode.FrontInRoom,
                        
                        ParamCardUpgradeData = BottledCutieMarkUpgrade,
                    }
                },
                FromStoryEvent = false,
                IsBossGivenRelic = false,
                LinkedClass = Ponies.EquestrianClanData,
                Rarity = CollectableRarity.Common,
                RelicLoreTooltipKeys = new List<string>
                {
                    "Pony_Relic_BottledCutieMark_Lore_Key"
                },
                UnlockLevel = 4,
            }.BuildAndRegister();

            AccessTools.Field(typeof(RelicData), "relicLoreTooltipStyle").SetValue(relicData, Ponies.PonyRelicTooltip.GetEnum());
        }
    }
}