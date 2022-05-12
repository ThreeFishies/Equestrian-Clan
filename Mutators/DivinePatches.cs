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
    [HarmonyPatch(typeof(SaveManager), "GetVictorySectionState")]
    public static class DivineDetermination
    {
        public static void Postfix(ref SaveManager __instance, ref SaveManager.VictorySectionState __result)
        {
            if (!Ponies.EquestrianClanIsInit) { return; }

            if (__instance.GetMutators().Count > 0)
            {
                foreach (MutatorState mutator in __instance.GetMutators())
                {
                    if (mutator.GetRelicDataID() == DivineOmnipresence.ID)
                    {
                        if (__instance.GetCurrentDistance() == __instance.GetRunLength() - 1)
                        {
                            if (__instance.GetDlcSaveData<HellforgedSaveData>(DLC.Hellforged) != null)
                            {
                                if (__result != SaveManager.VictorySectionState.NotReached)
                                {
                                    __result = SaveManager.VictorySectionState.PreHellforgedBoss;
                                }
                            }
                        }
                    }

                    if (mutator.GetRelicDataID() == DivineVoid.ID)
                    {
                        if (__result == SaveManager.VictorySectionState.PreHellforgedBoss)
                        {
                            __result = SaveManager.VictorySectionState.Victory;
                        }
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(SaveData), "GetVictoryType")]
    public static class FixVictoryType
    {
        public static void Postfix(ref SaveManager.VictoryType __result, ref SaveData __instance, ref SaveManager saveManager) 
        {
            if (__instance.GetMutatorIDs().Count > 0) 
            { 
                foreach (string id in __instance.GetMutatorIDs()) 
                {
                    if (id == DivineVoid.ID)
                    {
                        if (__result < SaveManager.VictoryType.Standard)
                        {
                            if (__instance.GetNumBattlesWon() >= 8) 
                            {
                                __result = SaveManager.VictoryType.Standard;
                            }
                        }
                    }

                    if (id == DivineOmnipresence.ID) 
                    {
                        if (__result == SaveManager.VictoryType.Standard)
                        {
                            __result = SaveManager.VictoryType.Hellforged;
                        }
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(SpMutatorOptionsUI), "HandleMutatorsChosen")]
    public static class DivineExclusion
    {
        public static void Prefix(ref List<string> chosenMutators)
        {
            if (!Ponies.EquestrianClanIsInit) { return; }

            bool divineConflict = false;
            int conflictID = -1;

            for (int ii = 0; ii < chosenMutators.Count; ii++)
            {
                if (chosenMutators[ii] == DivineOmnipresence.ID || chosenMutators[ii] == DivineVoid.ID)
                {
                    if (conflictID == -1)
                    {
                        conflictID = ii;
                    }
                    else
                    {
                        divineConflict = true;
                    }
                }
            }

            if (divineConflict)
            {
                chosenMutators.RemoveAt(conflictID);
            }
        }
    }
}