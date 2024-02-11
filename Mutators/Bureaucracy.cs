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
    public static class BureaucracyMutator
    {
        public static MutatorData bureaucracyMutatorData = ScriptableObject.CreateInstance<MutatorData>();
        public static string iconPath = Path.Combine(Path.GetDirectoryName(Ponies.Instance.Info.Location), "Mutators/Sprite", "MTR_Bureaucracy.png");
        public static string ID = Ponies.GUID + "_Bureaucracy";

        public static void BuildAndRegister()
        {
            CardData trainSteward = ProviderManager.SaveManager.GetAllGameData().FindCardData("d14a50f3-728d-43e1-87f0-ef1b013f6678");

            AccessTools.Field(typeof(GameData), "id").SetValue(bureaucracyMutatorData, ID);
            AccessTools.Field(typeof(MutatorData), "nameKey").SetValue(bureaucracyMutatorData, "Pony_Mutator_Bureaucracy_Name_Key");
            AccessTools.Field(typeof(MutatorData), "descriptionKey").SetValue(bureaucracyMutatorData, "Pony_Mutator_Bureaucracy_Description_Key");
            Sprite icon = CustomAssetManager.LoadSpriteFromPath(iconPath);
            AccessTools.Field(typeof(MutatorData), "icon").SetValue(bureaucracyMutatorData, icon);
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
                        UpgradeTitle = "Bureaucracy_Mutator_Effect_Herd_3",
                        BonusDamage = 0,
                        BonusHeal = 0,
                        TraitDataUpgradeBuilders = new List<CardTraitDataBuilder>
                        {
                            new CardTraitDataBuilder
                            {
                                TraitStateName = typeof(CardTraitHerd).AssemblyQualifiedName,
                                ParamInt = 3,
                            },
                        },
                        CardTriggerUpgradeBuilders = new List<CardTriggerEffectDataBuilder>
                        {
                            new CardTriggerEffectDataBuilder
                            {
                                Trigger = CardTriggerType.OnCast,
                                //Failing to add a description key adds and ugly NO DESCRIPTION PROVIDED to the bottom of the card text.
                                //Trying to use the Null key also does this.
                                //If you don't want to add a description, you need to pass a non-empty string containing only whitespace.
                                DescriptionKey = "CardTraitHerd_EmptyText",
                                CardEffectBuilders = new List<CardEffectDataBuilder>
                                {
                                    new CardEffectDataBuilder
                                    {
                                        EffectStateName = typeof(CardEffectHerdTooltip).AssemblyQualifiedName
                                    } 
                                }
                            }
                        },
                        Filters = new List<CardUpgradeMaskData>
                        {
                            new CardUpgradeMaskDataBuilderFixed
                            {
                                CardType = CardType.Spell,
                                ExcludedCardTraits = new List<String>
                                {
                                    typeof(CardTraitHerd).AssemblyQualifiedName
                                }
                            }.Build(),
                        },
                    }.Build(),
                    AdditionalTooltips = new AdditionalTooltipData[]
                    {
                        new AdditionalTooltipData
                        {
                            titleKey = "CardTraitHerd_TooltipTitle",
                            descriptionKey = "CardTraitHerd_TooltipText",
                            isStatusTooltip = false,
                            isTipTooltip = false,
                            isTriggerTooltip = false,
                            style = TooltipDesigner.TooltipDesignType.Keyword
                        }
                    }
                }.Build(),

                new RelicEffectDataBuilder
                {
                    RelicEffectClassName = typeof(RelicEffectModifyMonsterCapacity).AssemblyQualifiedName,
                    ParamInt = -1, //Reduce size by 1
                    ParamBool = false, //Does not apply to all.
                    ParamCharacterSubtype = "SubtypesData_TrainSteward",
                    ParamString = trainSteward.GetSpawnCharacterData().GetAssetKey(),
                }.Build(),
            };
            AccessTools.Field(typeof(MutatorData), "effects").SetValue(bureaucracyMutatorData, effects);
            List<string> relicLoreTooltipKeys = new List<string>
            {
                "Pony_Mutator_Bureaucracy_Lore_Key"
            };
            AccessTools.Field(typeof(MutatorData), "relicLoreTooltipKeys").SetValue(bureaucracyMutatorData, relicLoreTooltipKeys);
            AccessTools.Field(typeof(MutatorData), "relicLoreTooltipStyle").SetValue(bureaucracyMutatorData, Ponies.PonyRelicTooltip.GetEnum());
            AccessTools.Field(typeof(MutatorData), "relicActivatedKey").SetValue(bureaucracyMutatorData, "Pony_Mutator_Bureaucracy_Activated_Key");
            AccessTools.Field(typeof(MutatorData), "divineVariant").SetValue(bureaucracyMutatorData, false);
            AccessTools.Field(typeof(MutatorData), "boonValue").SetValue(bureaucracyMutatorData, -8);
            AccessTools.Field(typeof(MutatorData), "disableInDailyChallenges").SetValue(bureaucracyMutatorData, false); //Allows boon to be picked by randomizer button if false.
            AccessTools.Field(typeof(MutatorData), "requiredDLC").SetValue(bureaucracyMutatorData, DLC.None);
            List<String> tags = new List<String>
            {
            };
            AccessTools.Field(typeof(MutatorData), "tags").SetValue(bureaucracyMutatorData, tags);

            AllGameData allGameData = ProviderManager.SaveManager.GetAllGameData();
            List<MutatorData> mutatorDatas = (List<MutatorData>)AccessTools.Field(typeof(AllGameData), "mutatorDatas").GetValue(allGameData);

            if (!mutatorDatas.Contains(bureaucracyMutatorData))
            {
                mutatorDatas.Add(bureaucracyMutatorData);
            }
        }
    }
}