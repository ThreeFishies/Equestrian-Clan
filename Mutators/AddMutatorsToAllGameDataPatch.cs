/*
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
using Equestrian.Champions;
using Equestrian.Mutators;

namespace Equestrian.HarmonyPatches
{
    
    [HarmonyPatch(typeof(AllGameData), "GetAllMutatorData")]
    public static class AddMutatorsToAllGameDataPatch
    { 
        public static void Postfix(ref List<MutatorData> __result) 
        {
            if (!Ponies.EquestrianClanIsInit) { return; }

            __result.Add(CallTheCavalryMutator.callTheCavalryMutatorData);
        }
    }
    

    [HarmonyPatch(typeof(AllGameData), "FindMutatorData")]
    public static class AddMutatorsToAllGameDataPatch2
    {
        public static void Postfix(ref MutatorData __result, ref string id)
        {
            if (!Ponies.EquestrianClanIsInit) { return; }
            if (__result != null) { return; }

            if (CallTheCavalryMutator.callTheCavalryMutatorData.GetID() == id) 
            { 
                __result = CallTheCavalryMutator.callTheCavalryMutatorData;
            }

            if (DesertionMutator.desertionMutatorData.GetID() == id)
            {
                __result = DesertionMutator.desertionMutatorData;
            }
        }
    }
}
*/