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
using Trainworks.Constants;
using Trainworks.Builders;
using Equestrian.CardPools;
using Equestrian.Champions;
using ShinyShoe.Loading;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;


namespace Equestrian.HarmonyPatches
{
    //Finally Glaukopis will tell you what 'Rebate' actually does.
    [HarmonyPatch(typeof(RoomModifierData), "GetParamStatusEffects")]
    public static class FixRoomModifierTooltipsI
    {
        public static void Postfix(ref StatusEffectStackData[] __result)
        {
            if (__result == null)
            {
                __result = new StatusEffectStackData[0] { };
            }
        }
    }

    [HarmonyPatch(typeof(RoomModifierData), "GetExtraTooltipTitleKey")]
    public static class FixRoomModifierTooltipsII
    {
        public static void Postfix(ref string __result)
        {
            if (__result == null)
            {
                __result = string.Empty;
            }
        }
    }

    [HarmonyPatch(typeof(CardState), "GenerateCardBodyText")]
    public static class PreenyAndYoLoColonectomy
    {
        public static void Postfix(ref string generatedText, ref CardState __instance)
        {
            if (!Ponies.EquestrianClanIsInit) { return; }

            if (__instance.GetCardDataID() == CustomCardManager.GetCardDataByID(PreenySnuggle.ID).GetID() || __instance.GetCardDataID() == CustomCardManager.GetCardDataByID(YoLo.ID).GetID())
            {
                generatedText = generatedText.Replace(":", "");
            }
        }
    }


    [HarmonyPatch(typeof(CardTooltipContainer), "GetTooltipContentForGeneratedCard")]
    public static class YoLoNeedsExtraColonectomy
    {
        public static void Postfix(ref string __result, ref CardData cardData)
        {
            if (!Ponies.EquestrianClanIsInit) { return; }

            if (cardData.GetID() == CustomCardManager.GetCardDataByID(YoLo.ID).GetID())
            {
                __result = __result.Replace(": ", "");
            }
        }
    }

    [HarmonyPatch(typeof(TooltipContainer), "InstantiateTooltip")]
    public static class OnUnscaledSummonEmptyTriggerTooltipRemoval 
    { 
        public static bool Prefix(ref string tooltipId, ref TooltipUI __result)
        {
            if (tooltipId == CharacterTriggerData.Trigger.OnUnscaledSpawn.ToString()) 
            {
                __result = null;
                return false;
            }

            return true;
        }
    }
}