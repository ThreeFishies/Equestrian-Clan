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
using Equestrian.Arcadian;
using Equestrian.Mutators;

namespace Equestrian.HarmonyPatches
{
    [HarmonyPatch(typeof(AssetLoadingManager),"Unload")]
    public static class PreUnloadGatherAssets 
    { 
        public static void Prefix() 
        {
            RemovePonyPile.CollectArcadianUnits();
        }
    }

    //Hopefully this version of the fix won't remove anything that won't be recreated.
    //No more facing off against armies of missing character art!
    //Animated Arcadian units were also improperly being destroyed. Added a fix for that.
    [HarmonyPatch(typeof(AssetLoadingManager), "UnloadAsset")]
    public static class RemovePonyPile
    {
        public static List<string> ArcadianUnits = new List<string>() { };

        public static void CollectArcadianUnits() 
        {
            if (!ArcadianCompatibility.ArcadianExists) 
            {
                return;
            }

            ClassData classData = ArcadianCompatibility.ArcadianClan;
            SaveManager saveManager = ProviderManager.SaveManager;

            //We just need a fake card pool with the name "Chrono" so that we can indirectly pull data from it by invoking a method that Trainworks has patched.
            //Unfortunately, we can't access the data directly because the CustomCardPoolManager was flagged as 'internal.'
            CardPool cardPool = new CardPoolBuilder()
            {
                CardIDs = new List<string>() { },
                Cards = new List<CardData>() { },
                CardPoolID = "Chrono"
            }.Build();

            if (cardPool == null) 
            {
                Ponies.LogError("Failed to create dummy CardPool 'Chrono'.");
            }

            List<CardData> possibleCards = CardPoolHelper.GetCardsForClass(cardPool, classData, saveManager.GetClassLevel(classData.GetID()), CollectableRarity.Common, saveManager, null, false);

            foreach (CardData card in possibleCards) 
            { 
                if (card.IsSpawnerCard()) 
                {
                    if (card.GetSpawnCharacterData().characterPrefabVariantRef == null || card.GetSpawnCharacterData().characterPrefabVariantRef.Asset == null)
                    {
                        //Ponies.Log("Null asset ignored for unit " + card.GetSpawnCharacterData().name + ".");
                    }
                    else
                    {
                        if (!ArcadianUnits.Contains(card.GetSpawnCharacterData().characterPrefabVariantRef.Asset.name))
                        {
                            ArcadianUnits.Add(card.GetSpawnCharacterData().characterPrefabVariantRef.Asset.name);
                            //Ponies.Log(card.GetSpawnCharacterData().name);
                            //Ponies.Log(card.GetSpawnCharacterData().characterPrefabVariantRef.Asset.name);
                        }
                    }
                }
            }
        }

        public static bool IsArcadianUnit(string unitToDestroy)
        {
            if (!ArcadianCompatibility.ArcadianExists) 
            {
                return false;
            }

            //ArcadianUnits.Clear();
            //CollectArcadianUnits();

            foreach (string unit in ArcadianUnits) 
            {
                if (unitToDestroy == unit) 
                {
                    return true;
                }
            }

            return false;
        }

        public static void Prefix(ref AssetLoadingManager.AssetInfo info)
        {
            if (info == null || info.asset == null) { return; }

            if (info.asset.name.StartsWith("Character_"))
            {
                if (!AccessTools.StaticFieldRefAccess<Dictionary<object, KeyValuePair<IResourceLocation, int>>>(typeof(Addressables), "s_AssetToLocationMap").TryGetValue(info.asset, out _))
                {
                    if (ArcadianCompatibility.ArcadianExists) 
                    {
                        if (IsArcadianUnit(info.asset.name)) 
                        {
                            Ponies.Log("Skipping Arcadian unit: " + info.asset.name);
                            return;
                        }
                    }

                    Ponies.Log("Destroying object: " + info.asset.name);
                    UnityEngine.Object.Destroy(info.asset);
                }
            }
        }
    }
}