using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Equestrian.Init;
using Trainworks.Managers;
using Equestrian.SpellCards;
using Equestrian.MonsterCards;
using ShinyShoe.Loading;
using Equestrian;


namespace Equestrian.Enhancers
{
    [HarmonyPatch(typeof(EnhancerPool), "GetFilteredChoices")]
    class FriendstoneIsEquestrianExclusive
    {
        //Trainworks replaces the original code with their own version that does not take class restrictions of custom enhancers into account.
        //This will edit the result to remove the friendstone from the list if Equestrian is not a primary or secondary clan.
        static public void Postfix(ref List<EnhancerData> __result) 
        {
            SaveManager saveManager = ProviderManager.SaveManager;

            if (saveManager.HasMainClass())
            {
                if (saveManager.GetMainClass().name == Ponies.EquestrianClanData.name || saveManager.GetSubClass().name == Ponies.EquestrianClanData.name)
                {
                    return;
                }
                else
                {
                    __result.Remove(Ponies.FriendstoneData);

                    //foreach (EnhancerData enhancer in __result) 
                    //{
                    //    Ponies.Log("Grantable Enhancer name: " + enhancer.GetName());
                    //    Ponies.Log("Grantable Enhancer ID: " + enhancer.GetID());
                    //}
                }
            }
        }
    }

    //This patch breaks the game, making the button for starting a new run do nothing. Not sure what went wrong, as the log file shows no errors.
    /*
    [HarmonyPatch]
    [HarmonyPatch(new Type[] { typeof(PoolRewardData.RandomChoiceParameters<EnhancerData>) })]
    public static class FriendstoneIsEquestrianExclusive
    {
        public static System.Reflection.MethodBase TargetMethod()
        {
            // refer to C# reflection documentation:
            return typeof(PoolRewardData).GetMethod("GetFilteredChoices").MakeGenericMethod(typeof(EnhancerData));
        }
        public static void Prefix(ref PoolRewardData.RandomChoiceParameters<EnhancerData> parameters)
        {
            SaveManager saveManager = ProviderManager.SaveManager;

            if (saveManager.GetMainClass().name == Ponies.EquestrianClanData.name || saveManager.GetSubClass().name == Ponies.EquestrianClanData.name)
            {
                //isPony
            }
            else
            {
                Ponies.Log("Non-pony. Attempting to exclude Friendstone from enhancer pool.");
                if (parameters.list.Contains(Ponies.FriendstoneData)) 
                {
                    Ponies.Log("Friendstone exists.");
                }
                else 
                {
                    Ponies.Log("Friendstone is not in pool.");
                }
            }
        }
    }*/
}