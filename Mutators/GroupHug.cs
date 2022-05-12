using Trainworks.Managers;
using Trainworks.Builders;
using Trainworks.Constants;
using Equestrian.Init;
using Equestrian.Relic;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HarmonyLib;
using ShinyShoe;
using Equestrian.Sprites;
using UnityEngine;
using CustomEffects;

namespace Equestrian.Mutators
{
    public static class GroupHugMutator
    {
        public static MutatorData groupHugMutatorData = ScriptableObject.CreateInstance<MutatorData>();
        public static string iconPath = Path.Combine(Path.GetDirectoryName(Ponies.Instance.Info.Location), "Mutators/Sprite", "MTR_GroupHug.png");
        public static string ID = Ponies.GUID + "_GroupHug";

        public static void BuildAndRegister()
        {
            AccessTools.Field(typeof(GameData), "id").SetValue(groupHugMutatorData, ID);
            AccessTools.Field(typeof(MutatorData), "nameKey").SetValue(groupHugMutatorData, "Pony_Mutator_GroupHug_Name_Key");
            AccessTools.Field(typeof(MutatorData), "descriptionKey").SetValue(groupHugMutatorData, "Pony_Mutator_GroupHug_Description_Key");
            Sprite icon = CustomAssetManager.LoadSpriteFromPath(iconPath);
            AccessTools.Field(typeof(MutatorData), "icon").SetValue(groupHugMutatorData, icon);
            List<RelicEffectData> effects = new List<RelicEffectData>
            {
                new RelicEffectDataBuilder
                {
                    RelicEffectClassName = typeof(CustomMutatorEffectAddUpgradeToAllCards).AssemblyQualifiedName,
                    ParamSourceTeam = Team.Type.Monsters,
                    ParamInt = -1,
                    ParamFloat = 0.0f,
                    ParamTrigger = CharacterTriggerData.Trigger.OnDeath,
                    ParamTargetMode = TargetMode.FrontInRoom,
                    ParamStatusEffects = new StatusEffectStackData[]
                    {
                    },
                    ParamCardUpgradeData = new CardUpgradeDataBuilder
                    {
                        UpgradeTitle = "GroupHug_Mutator_Effect_Social",
                        BonusDamage = 0,
                        BonusHeal = 0,
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
                                        HideTooltip = true,
                                        TargetMode = TargetMode.Self
                                    }
                                }
                            }
                        },
                        StatusEffectUpgrades = new List<StatusEffectStackData>
                        {
                            new StatusEffectStackData
                            {
                                statusId = "social",
                                count = 1
                            }
                        },
                        Filters = new List<CardUpgradeMaskData>
                        {
                            new CardUpgradeMaskDataBuilderFixed
                            {
                                CardType = CardType.Monster,
                                ExcludedStatusEffects = new List<StatusEffectStackData>
                                {
                                    new StatusEffectStackData
                                    { 
                                        statusId = "social",
                                        count = 1
                                    }
                                }
                            }.Build(),
                        },
                    }.Build(),
                    AdditionalTooltips = new AdditionalTooltipData[]
                    {
                        new AdditionalTooltipData
                        {
                            titleKey = "CardEffectSocial_TooltipTitle",
                            descriptionKey = "CardEffectSocial_Description",
                            isStatusTooltip = true,
                            isTipTooltip = false,
                            isTriggerTooltip = false,
                            statusId = "social",
                            style = TooltipDesigner.TooltipDesignType.Persistent
                        }
                    }
                }.Build(),
            };
            AccessTools.Field(typeof(MutatorData), "effects").SetValue(groupHugMutatorData, effects);
            List<string> relicLoreTooltipKeys = new List<string>
            {
                "Pony_Mutator_GroupHug_Lore_Key"
            };
            AccessTools.Field(typeof(MutatorData), "relicLoreTooltipKeys").SetValue(groupHugMutatorData, relicLoreTooltipKeys);
            AccessTools.Field(typeof(MutatorData), "relicActivatedKey").SetValue(groupHugMutatorData, "Pony_Mutator_GroupHug_Activated_Key");
            AccessTools.Field(typeof(MutatorData), "divineVariant").SetValue(groupHugMutatorData, false);
            AccessTools.Field(typeof(MutatorData), "boonValue").SetValue(groupHugMutatorData, 6);
            AccessTools.Field(typeof(MutatorData), "disableInDailyChallenges").SetValue(groupHugMutatorData, false);
            AccessTools.Field(typeof(MutatorData), "requiredDLC").SetValue(groupHugMutatorData, DLC.None);
            List<String> tags = new List<String>
            {
                "friendlyeffect"
            };
            AccessTools.Field(typeof(MutatorData), "tags").SetValue(groupHugMutatorData, tags);

            AllGameData allGameData = ProviderManager.SaveManager.GetAllGameData();
            List<MutatorData> mutatorDatas = (List<MutatorData>)AccessTools.Field(typeof(AllGameData), "mutatorDatas").GetValue(allGameData);

            if (!mutatorDatas.Contains(groupHugMutatorData))
            {
                mutatorDatas.Add(groupHugMutatorData);
            }
        }
    }
}