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
    //Add checks for null where appropriate.
    [HarmonyPatch(typeof(CardEffectFloorRearrange), "ApplyEffect")]
    public static class DidTheUnnamedRelicEverWorkOrWasItAlwaysBrokenEhWhoCaresLetsFixIt
    {
        public static bool Prefix(ref CardEffectParams cardEffectParams)
        {
            //Ponies.Log("line 28.");

            bool isOkay = true;

            if (cardEffectParams == null) { isOkay = false; }
            //Ponies.Log("line 33.");
            if (isOkay && cardEffectParams.targets == null) { isOkay = false; }
            //Ponies.Log("line 35.");
            if (isOkay && cardEffectParams.targets.Count == 0) { isOkay = false; }
            //Ponies.Log("line 37.");
            if (isOkay && cardEffectParams.targets[0] == null) { isOkay = false; }
            //Ponies.Log("line 39.");
            if (isOkay && cardEffectParams.targets[0].GetSpawnPoint(false) == null) { isOkay = false; }
            //Ponies.Log("line 41.");
            if (isOkay && cardEffectParams.targets[0].GetSpawnPoint(false).GetRoomOwner() == null) { isOkay = false; }
            //Ponies.Log("line 43.");

            if (!isOkay && Ponies.EquestrianClanIsInit)
            {
                //This error happens a LOT with the Unnamed Relic in use.
                //This fix seems to work, though.
                //Ponies.Log("Null detected. Handling unnamed error.");
            }

            return isOkay;
        }
    }
}