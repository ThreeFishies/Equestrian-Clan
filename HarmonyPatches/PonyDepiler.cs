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
    //Hopefully this version of the fix won't remove anything that won't be recreated.
    //No more facing off against armies of missing character art!
    [HarmonyPatch(typeof(AssetLoadingManager), "UnloadAsset")]
    public static class RemovePonyPile
    {
        public static void Prefix(ref AssetLoadingManager.AssetInfo info)
        {
            if (info.asset.name.StartsWith("Character_"))
            {
                if (!AccessTools.StaticFieldRefAccess<Dictionary<object, KeyValuePair<IResourceLocation, int>>>(typeof(Addressables), "s_AssetToLocationMap").TryGetValue(info.asset, out _))
                {
                    Ponies.Log("Destroying object: " + info.asset.name);
                    UnityEngine.Object.Destroy(info.asset);
                }
            }
        }
    }
}