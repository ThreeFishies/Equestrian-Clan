using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;
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
using UnityEngine;
using Malee;
using Equestrian.HarmonyPatches;
using Equestrian.Sprites;

namespace Equestrian.PonyStory
{
    /*
    [HarmonyPatch(typeof(InkWriter), "Initialize")]
    public static class GetMasterStoryFile
    {
        public static bool GotIt = true;
        public const string StoryPath = "PonyStory/MasterStoryFile.txt";

        public static void Prefix(ref TextAsset masterStoryFile)
        {
            if (!GotIt)
            {
                string directoryName = Path.GetDirectoryName(Ponies.Instance.Info.Location);
                File.WriteAllText(Path.Combine(directoryName, StoryPath), masterStoryFile.text);
            }
            GotIt = true;
        }
    }
    */

    [HarmonyPatch(typeof(StoryEventScreen), "SetupStory")]
    public static class FixArtForStories
    {
        public static void Postfix(ref StoryEventData storyData) 
        {
            //Load custom image for FLowerPonies event
            if (storyData != null && storyData.KnotName == "FlowerPonies") 
            {
                FixArt.TryYetAnotherFix(MyCardPools.FlowerPoniesPool);

                SpriteRenderer seeFlowerPonies = (SpriteRenderer)storyData.EventPrefab.KeyArtParent.GetComponent(typeof(SpriteRenderer));
                string imgPath = Path.Combine(Path.Combine(Path.GetDirectoryName(Ponies.Instance.Info.Location), "ClanAssets"), "FlowerPonies.png");

                if (File.Exists(imgPath))
                {
                    byte[] data = File.ReadAllBytes(imgPath);

                    seeFlowerPonies.sprite.texture.LoadImage(data);
                }
            }

            //Reset to default image for base event
            if (storyData != null && storyData.KnotName == "GimmeGold") 
            {
                SpriteRenderer frozenChests = (SpriteRenderer)storyData.EventPrefab.KeyArtParent.GetComponent(typeof(SpriteRenderer));
                string imgPath = Path.Combine(Path.Combine(Path.GetDirectoryName(Ponies.Instance.Info.Location), "ClanAssets"), "Event_GimmeGold_01.png");

                if (File.Exists(imgPath))
                {
                    byte[] data = File.ReadAllBytes(imgPath);

                    frozenChests.sprite.texture.LoadImage(data);
                }
            }
        }
    }

    //If this mod ever needs to be translated, localazation should be handled properly instead of doing this:
    [HarmonyPatch(typeof(LocalizationExtensions), "LocalizeInk")]
    public static class RemoveKEYTag
    {
        public static void Postfix(ref string __result)
        {
            if (__result.StartsWith("KEY>>") && __result.Length >= 7)
            { 
                __result = __result.Substring(5,__result.Length - 7);
            }
        }
    }

    /*
    //RunData seems to hangle the high/low shard divinity events. Not what we want here.
    public static class InitStoriesForRun
    {
        public static void AddPonyStoiesToRunDataPool(SaveManager saveManager)
        {
            StoryEventPoolData poolData = saveManager.GetRunData().GetRequiredStartingEventPool();

            ReorderableArray<StoryEventData> storyEventDatas = (ReorderableArray<StoryEventData>)AccessTools.Field(typeof(StoryEventPoolData), "storyEvents").GetValue(poolData);

            if (!storyEventDatas.Contains(FlowerPonies.flowerPoniesEventData))
            {
                storyEventDatas.Add(FlowerPonies.flowerPoniesEventData);
                Ponies.Log("Added FlowerPonies story to RunData");

                Ponies.Log("____________RunData_RequiredStartingEventPool_______________");
                foreach (StoryEventData story in storyEventDatas) 
                {
                    Ponies.Log(story.name);
                }
                Ponies.Log("____________________________________________________________");
            }
        }
    }
    */
}

//StoryManager "GetAuthoredReward"
//Inkwriter "CreateStory"
//AllGameData->BalanceData->MasterStoryFile

//Localization keys:
//EventChoice_FlowerPonies_TakePonyTome
//EventChoice_FlowerPonies_TakePonyTome_Optional