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
    /*
    public static class I_CAN_CALL_THIS_CLASS_ANYTHING_I_WANT_TO_AND_NOBODY_CAN_STOP_ME 
    {
    }
    */

    [HarmonyPatch(typeof(MetagameSaveData), "HasCompletedSpChallenge")]
    public static class spChallengeVictoryPatch
    { 
        public static void Postfix(ref bool __result, ref string id) 
        {
            if (!Ponies.EquestrianClanIsInit) { return; }

            if (PonyMetagame.IsPonyChallenge(id))
            {
                __result = PonyMetagame.HasSpChallengeWin(id);
            }
        }
    }

    [HarmonyPatch(typeof(MetagameSaveData), "HasCompletedSpChallengeWithDivineVictory")]
    public static class spChallengeDivineVictoryPatch
    {
        public static void Postfix(ref bool __result, ref string id)
        {
            if (!Ponies.EquestrianClanIsInit) { return; }

            if (PonyMetagame.IsPonyChallenge(id))
            {
                __result = PonyMetagame.HasSpChallengeWinDivine(id);
            }
        }
    }

    [HarmonyPatch(typeof(SaveManager), "TrackRunResults")]
    public static class CatchChallengeWins
    {
        [HarmonyBefore(new string[] { "mod.clan.helper.monstertrain" })]
        public static void Prefix(ref SaveManager __instance)
        {
            if (!Ponies.EquestrianClanIsInit) { return; }

            if (PonyMetagame.IsPonyChallenge(__instance.GetSpChallengeId()))
            {
                SaveManager.VictoryType victoryType = __instance.GetVictoryType();
                bool victory = false;
                bool divine = false;

                if (victoryType > SaveManager.VictoryType.None)
                {
                    victory = true;

                    if (victoryType >= SaveManager.VictoryType.Hellforged)
                    {
                        divine = true;
                    }
                }

                PonyMetagame.UpdateChallenge(__instance.GetSpChallengeId(), victory, divine);
                PonyMetagame.SavePonyMetaFile();
            }
        }
    }
}