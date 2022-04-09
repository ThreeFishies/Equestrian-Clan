using System;
using System.Collections.Generic;
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
using Equestrian;
using CustomEffects;

namespace Equestrian.HarmonyPatches
{
    /*
    [HarmonyPatch(typeof(SpawnPoint), "GetCharacterState")]
    public static class FindMovedUnit
    {
        public static void Postfix(ref CharacterState __result, ref SpawnPoint __instance)
        {
            if (__result == null)
            {
                if (Ponies.HallowedHallsInEffect)
                {
                    if (Ponies.LastMovedUnit != null && !Ponies.PanicFlag)
                    {
                        Ponies.Log("Trying to find misplaced unit.");
                        Ponies.PanicFlag = true;
                        __result = Ponies.LastMovedUnit.GetCharacterState();
                        Ponies.PanicFlag = false;

                        if (__result != null)
                        {
                            Ponies.Log("Returning CharacterState: " + __result.GetName());
                        }
                    }
                    else if (Ponies.PanicFlag) 
                    {
                        Ponies.PanicFlag = false;
                    }
                }
            }
        }
    }*/

    [HarmonyPatch(typeof(CardEffectRecursion), "HandleRandomToRoomUntilCapacityFull")]
    public static class SetHallowedHallsStatus
    {
        public static void Prefix()
        {
            Ponies.HallowedHallsInEffect = true;
            Ponies.Log("Starting Hallowed Halls.");
        }

        /*
        public static void Postfix(ref System.Collections.IEnumerator __result)
        {
            Ponies.HallowedHallsInEffect = false;
            Ponies.Log("Ending Hallowed Halls.");
            Ponies.Log("Result: " + __result.ToString());
        }
        */
    }

    [HarmonyPatch(typeof(CardManager), "PlayCard")]
    public static class ResetHallowedHallsStatus
    {
        public static void Postfix()
        {
            if (Ponies.HallowedHallsInEffect)
            {
                Ponies.HallowedHallsInEffect = false;
                Ponies.Log("Ending Hallowed Halls.");
            }
        }
    }
}