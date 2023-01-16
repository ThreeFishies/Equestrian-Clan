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
    [HarmonyPatch(typeof(CardEffectBump), "FindBumpSpawnPoint")]
    public static class MakeFlashFeatherNotFlashFryTheGame
    {
        public static bool Prefix(ref CharacterState target, ref RoomManager roomManager, ref CardEffectBump.BumpError bumpError)
        {
            bool isOkay = true;

            if (target == null) { isOkay = false; }
            if (roomManager == null) { isOkay = false; }
            if (isOkay && target.GetSpawnPoint(false) == null) { isOkay = false;}
            if (isOkay && target.GetSpawnPoint(false).GetRoomOwner() == null) { isOkay = false; }

            if (!isOkay) 
            { 
                bumpError = CardEffectBump.BumpError.NoRoom;
                Ponies.Log("Null detected. Handling bump error.");
            }

            return isOkay;
        }
    }
}