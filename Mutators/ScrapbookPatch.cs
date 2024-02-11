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
using Equestrian.Metagame;
using Equestrian.Mutators;
using UnityEngine;
using CustomEffects;

namespace Equestrian.HarmonyPatches
{
    [HarmonyPatch(typeof(DraftScreenBase), "SetUpSkipButton")]
    public static class ActivateScrapbook
    {
        public static bool Prefix(GameUISelectableButton ___skipButton) 
        {
            if (!Ponies.EquestrianClanIsInit) { return true; }

            if (Scrapbook.HasScrapbook()) 
            { 
                ___skipButton.gameObject.SetActive(false);
                return false; 
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(RewardScreen), "StartRewardDisplaySequence")]
    public static class ActivateScrapbook2 
    { 
        public static void Postfix(GameUISelectableButton ___skipButton) 
        {
            if (!Ponies.EquestrianClanIsInit) { return; }

            if (Scrapbook.HasScrapbook())
            {
                ___skipButton.gameObject.SetActive(false);
            }
        }
    }

    [HarmonyPatch(typeof(RewardScreen), "DoSkip")]
    public static class ActivateScrapbook3 
    {
        public static bool Prefix(GameUISelectableButton ___skipButton)
        {
            if (!Ponies.EquestrianClanIsInit) { return true; }

            if (Scrapbook.HasScrapbook())
            {
                ___skipButton.gameObject.SetActive(false);
                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(RewardScreen), "StartGrantSequence")]
    public static class ActivateScrapbook4 
    {
        public static bool Prefix(ref bool skipReward, RewardScreen __instance)
        {
            if (!Ponies.EquestrianClanIsInit) { return true; }

            if (Scrapbook.HasScrapbook())
            {
                if (skipReward)
                {
                    AccessTools.Method(typeof(RewardScreen), "DoSkip", null, null).Invoke(__instance, new object[0]);
                    return false;
                }
            }
            return true;
        }
    }
}