using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using HarmonyLib;
using Equestrian.Init;
using Trainworks.Managers;
using Equestrian.SpellCards;
using Equestrian.MonsterCards;
using Equestrian.Relic;
using Equestrian;
using Trainworks.Constants;
using Trainworks.Builders;
using Equestrian.CardPools;
using Equestrian.Champions;
using ShinyShoe.Loading;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;


namespace Equestrian.HarmonyPatches
{
    [HarmonyPatch(typeof(RewardNodeData), "TriggerNode")]
    public static class CacheRewardData
    {
        public static List<GrantableRewardData> cached_Data;
        public static RewardState.Location cached_location;
        
        public static void Prefix(ref RewardState.Location location, ref RewardNodeData __instance) 
        {
            if (!Ponies.EquestrianClanIsInit) { return; }

            CustomMapNodePoolManager.CustomRewardNodeData.TryGetValue(VanillaMapNodePoolIDs.RandomChosenMainClassUnit, out List<RewardNodeData> value);

            bool isCustomNode = false;

            foreach (RewardNodeData item in value) 
            {
                if(__instance.GetTooltipTitle() == item.GetTooltipTitle())
                {
                    isCustomNode = true;
                }
            }

            if (isCustomNode)
            {
                Ponies.Log("Custom banner detected. Caching data.");
                //cached_Data = __instance.GetRewards();
                //cached_Data = EquestrianBanner.data.GetRewards();

                cached_Data = __instance.GetGrantableRewards(ProviderManager.SaveManager);
                cached_location = location;
            }
            else 
            {
                cached_Data = null;
            }
        }
    }

    [HarmonyPatch(typeof(RewardScreen), "Show")]
    public static class AssertBannerReward
    {
        public static bool overrideSetVisited = false;
        public static void Prefix(ref List<RewardState> rewards)
        {
            if (!Ponies.EquestrianClanIsInit) { return; }

            if (rewards.Count == 0) 
            {
                Ponies.Log("No rewards to display.");
                return; 
            }

            bool FixRewards = false;

            for (int ii = 0; ii < rewards.Count; ii++)
            {
                IRewardDisplayable reward = rewards[ii];
                RewardState reward1 = reward as RewardState;
                if (reward == null || reward1 == null)
                {
                    Ponies.Log("Reward is null!");
                    return;
                }
                if (reward1.RewardData == null) 
                {
                    Ponies.Log("Reward Data is null!");
                    //return;

                    if (CacheRewardData.cached_Data != null) 
                    {
                        FixRewards = true;
                    }
                }
            }

            if (FixRewards) 
            {
                Ponies.Log("Attempting to fix broken banner.");

                List<RewardState> rewardStates = new List<RewardState> { };
                SaveData saveData = (SaveData)AccessTools.Method(typeof(SaveManager), "get_ActiveSaveData").Invoke(ProviderManager.SaveManager, new object[] { });

                foreach (GrantableRewardData state in CacheRewardData.cached_Data) 
                {
                    rewardStates.Add(new RewardState(state, ProviderManager.SaveManager));
                }

                foreach (RewardState rewardState in rewardStates) 
                {
                    rewardState.SetActive(CacheRewardData.cached_location.Distance, CacheRewardData.cached_location.Branch, CacheRewardData.cached_location.Index);
                    rewardState.MarkAsSeen();
                    saveData.AddReward(rewardState);
                }

                rewards = rewardStates;

                overrideSetVisited = true;
            }
        }
    }

    [HarmonyPatch(typeof(MapNodeUI),"SetVisited")]
    public static class WhyIsThisSoMessy_Sadface 
    {
        public static List<MapNodeUI> nodeUIs = new List<MapNodeUI> { };
        public static void Prefix(ref bool ___canActivate, ref MapNodeUI __instance)
        { 
            if (nodeUIs.Contains(__instance)) 
            {
                Ponies.Log("Seriously. Stop it.");

                ___canActivate = false;

                return;
            }

            if (AssertBannerReward.overrideSetVisited) 
            {
                Ponies.Log("Attempting to stop infinite rewards.");
                ___canActivate = false;
                AssertBannerReward.overrideSetVisited = false;

                if (!nodeUIs.Contains(__instance))
                {
                    nodeUIs.Add(__instance);
                }
            }
        }
    }


    [HarmonyPatch(typeof(MapNodeUI), "RefreshState")]
    public static class TheMoreMessyTheMerrier
    {
        public static bool Prefix(ref MapNodeUI __instance)
        {
            if (WhyIsThisSoMessy_Sadface.nodeUIs.Contains(__instance)) 
            {
                Ponies.Log("Please stop regenerating the stupid banner already!");
                return false;
            }

            return true;
        }
    }
}