/*
using System;
using Trainworks.Managers;
using HarmonyLib;

namespace Equestrian.Arcadian
{
    [HarmonyPatch(typeof(MetagameSaveData), "GetMastery", new Type[] { typeof(string) })]
    public static class EquestrianAnalogMastery
    {
        public static void PostFix(ref CardUI.MasteryType __result, ref string cardDataId) 
        {
            if (!ArcadianCompatibility.ArcadianExists) { return; }

            if (cardDataId == CustomCardManager.GetCardDataByID(EquestrianAnalogBase.ID).GetMasteryCardDataID()) 
            {
                __result = ArcadianCompatibility.AnalogMastery;
                return;
            }
            if (cardDataId == CustomCardManager.GetCardDataByID(EquestrianAnalogExile.ID).GetMasteryCardDataID())
            {
                __result = ArcadianCompatibility.AnalogMastery;
                return;
            }
            if (cardDataId == CustomCardManager.GetCardDataByID(VampireFruitBat.ID).GetMasteryCardDataID())
            {
                __result = ArcadianCompatibility.AnalogMastery;
                return;
            }
        }
    }
}
*/