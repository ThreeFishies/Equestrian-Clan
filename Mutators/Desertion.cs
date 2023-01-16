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
    public static class DesertionMutator
    {
        public static MutatorData desertionMutatorData = ScriptableObject.CreateInstance<MutatorData>();
        public static string iconPath = Path.Combine(Path.GetDirectoryName(Ponies.Instance.Info.Location), "Mutators/Sprite", "MTR_Desertion.png");
        public static string ID = Ponies.GUID + "_Desertion";
        public static bool gotAllBannerPool = false;
        public static CardPool betterAllBannerPool;

        public static CardPool GetUnitsAllBannerPool() 
        {
            if (gotAllBannerPool) 
            {
                return betterAllBannerPool;
            }

            //The "UnitsAllBanner" card pool
            CardPool unitsAllBannerPool = (CardPool)AccessTools.Field(typeof(DraftRewardData), "draftPool").GetValue(ProviderManager.SaveManager.GetAllGameData().FindRewardData("8a8782bf-3faf-4dd7-8f5d-413c1b269c3d"));

            List<CardData> betterList = new List<CardData>();

            //We do this step to sniff out the custom cards added by clan mods because Trainworks does not allow us to access the custom card pool manager.
            if (!CardEffectState.GetFilteredCardListFromPool(unitsAllBannerPool, null, null, ref betterList)) 
            {
                Ponies.Log("Failed to update UnitsAllBanner with custom cards.");
                return unitsAllBannerPool;
            }

            //foreach (CardData better in betterList) 
            //{
            //    //This correctly outputs all banner units. Error must be elsewhere.
            //    Ponies.Log($"Pool item: {better.GetName()}");
            //}

            betterAllBannerPool = new CardPoolBuilder
            {
                CardPoolID = Ponies.GUID + "_BetterUnitsAllBannerPool",
                Cards = betterList,
            }.BuildAndRegister();

            gotAllBannerPool = true;

            return betterAllBannerPool;
        }
        public static void BuildAndRegister()
        {
            AccessTools.Field(typeof(GameData), "id").SetValue(desertionMutatorData, ID);
            AccessTools.Field(typeof(MutatorData), "nameKey").SetValue(desertionMutatorData, "Pony_Mutator_Desertion_Name_Key");
            AccessTools.Field(typeof(MutatorData), "descriptionKey").SetValue(desertionMutatorData, "Pony_Mutator_Desertion_Description_Key");
            Sprite icon = CustomAssetManager.LoadSpriteFromPath(iconPath);
            AccessTools.Field(typeof(MutatorData), "icon").SetValue(desertionMutatorData, icon);
            List<RelicEffectData> effects = new List<RelicEffectData>
            {
                new RelicEffectDataBuilder
                {
                    RelicEffectClassName = typeof(CustomMutatorEffectAddUpgradeToCardDrafts).AssemblyQualifiedName,
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
                        UpgradeTitle = "Desertion_Mutator_Effect_Purge",
                        BonusDamage = 0,
                        BonusHeal = 0,
                        TraitDataUpgradeBuilders = new List<CardTraitDataBuilder>
                        {
                            new CardTraitDataBuilder
                            {
                                TraitStateName = "CardTraitSelfPurge",
                            }
                        },
                        Filters = new List<CardUpgradeMaskData>
                        {
                            new CardUpgradeMaskDataBuilderFixed
                            {
                                CardType = CardType.Monster,
                                AllowedCardPools = new List<CardPool>
                                {
                                    //The "UnitsAllBanner" card pool
                                    GetUnitsAllBannerPool()
                                }
                            }.Build(),
                        },
                    }.Build(),
                }.Build(),
            };
            AccessTools.Field(typeof(MutatorData), "effects").SetValue(desertionMutatorData, effects);
            List<string> relicLoreTooltipKeys = new List<string>
            {
                "Pony_Mutator_Desertion_Lore_Key"
            };
            AccessTools.Field(typeof(MutatorData), "relicLoreTooltipKeys").SetValue(desertionMutatorData, relicLoreTooltipKeys);
            AccessTools.Field(typeof(MutatorData), "relicLoreTooltipStyle").SetValue(desertionMutatorData, Ponies.PonyRelicTooltip.GetEnum());
            AccessTools.Field(typeof(MutatorData), "relicActivatedKey").SetValue(desertionMutatorData, "Pony_Mutator_Desertion_Activated_Key");
            AccessTools.Field(typeof(MutatorData), "divineVariant").SetValue(desertionMutatorData, false);
            AccessTools.Field(typeof(MutatorData), "boonValue").SetValue(desertionMutatorData, -10);
            AccessTools.Field(typeof(MutatorData), "disableInDailyChallenges").SetValue(desertionMutatorData, false);
            AccessTools.Field(typeof(MutatorData), "requiredDLC").SetValue(desertionMutatorData, DLC.None);
            List<String> tags = new List<String>
            {
                "friendlyeffect"
            };
            AccessTools.Field(typeof(MutatorData), "tags").SetValue(desertionMutatorData, tags);

            AllGameData allGameData = ProviderManager.SaveManager.GetAllGameData();
            List<MutatorData> mutatorDatas = (List<MutatorData>)AccessTools.Field(typeof(AllGameData), "mutatorDatas").GetValue(allGameData);
            
            if (!mutatorDatas.Contains(desertionMutatorData)) 
            {
                mutatorDatas.Add(desertionMutatorData);
            }
        }
    }
}