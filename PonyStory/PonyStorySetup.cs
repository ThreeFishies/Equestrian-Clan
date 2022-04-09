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
using ShinyShoe;
using Equestrian.Sprites;

namespace Equestrian.PonyStory
{
    public static class FlowerPonies
    {
        public static bool Registered = false;
        public const string FlowerPoniesStoryPath = "PonyStory/FlowerPonies.txt";
        public static StoryEventData flowerPoniesEventData;
        //public static float X_Transform;
        //public static float Y_Transform;
        //public static float Z_Transform;

        public static void EditMasterStoryFile() 
        { 
            if (Registered) { return; }

            TextAsset masterStoryFile = ProviderManager.SaveManager.GetBalanceData().MasterStoryFile;

            if (masterStoryFile == null) 
            {
                Ponies.Log("Unable to retrieve master story file.");
                return;
            }

            string directoryName = Path.GetDirectoryName(Ponies.Instance.Info.Location);
            string[] flowerPoniesStoryRaw = File.ReadAllLines(Path.Combine(directoryName, FlowerPoniesStoryPath));

            if (flowerPoniesStoryRaw.Length == 0) 
            {
                Ponies.Log("Failed to read file: " + Path.Combine(directoryName, FlowerPoniesStoryPath));
                return;
            }

            string fullFlowerPoniesStory = ",";

            foreach (string flowerPonyLine in flowerPoniesStoryRaw) 
            {
                fullFlowerPoniesStory += flowerPonyLine.Trim();
            }

            string masterStoryString = masterStoryFile.text;

            int insertLocation = masterStoryString.IndexOf(",\"HephRecruit\"");

            masterStoryString = masterStoryString.Insert(insertLocation, fullFlowerPoniesStory);

            AccessTools.Field(typeof(BalanceData), "masterStoryFile").SetValue(ProviderManager.SaveManager.GetBalanceData(), new TextAsset(masterStoryString));

            //Ponies.Log("__________________________");
            //Ponies.Log(ProviderManager.SaveManager.GetBalanceData().MasterStoryFile.text);
            //Ponies.Log("__________________________");

            //new CardDataBuilder() { }.BuildAndRegister();

            Registered = true;
        }

        public static StoryEventData BuildEventData()
        {
            CardRewardData tomeReward = new CardRewardDataBuilder()
            {
                overrideID = Ponies.GUID + "_PonyTomeReward",
                name = "PonyTomeReward",
                _rewardTitleKey = "PonyTomeReward_RewardTitleKey",
                _rewardDescriptionKey = "",
                _cardData = CustomCardManager.GetCardDataByID(TheElementsOfHarmony.ID),
                _cardDataId = CustomCardManager.GetCardDataByID(TheElementsOfHarmony.ID).GetID()
            }.BuildAndRegister();

            CardRewardData spikeReward = new CardRewardDataBuilder()
            {
                overrideID = Ponies.GUID + "_PonyRailReward",
                name = "PonyRailReward",
                _rewardTitleKey = "PonyRailReward_RewardTitleKey",
                _rewardDescriptionKey = "",
                _cardData = CustomCardManager.GetCardDataByID(EquestrianRailspike.ID),
                _cardDataId = CustomCardManager.GetCardDataByID(EquestrianRailspike.ID).GetID()
            }.BuildAndRegister();

            CardRewardData secretReward = new CardRewardDataBuilder()
            {
                overrideID = Ponies.GUID + "_SecretPonyReward",
                name = "SecretPonyReward",
                _rewardTitleKey = "SecretPonyReward_RewardTitleKey",
                _rewardDescriptionKey = "",
                _cardData = CustomCardManager.GetCardDataByID(MissingMare.ID),
                _cardDataId = CustomCardManager.GetCardDataByID(MissingMare.ID).GetID()
            }.BuildAndRegister();

            StoryEventAssetFrame flowerPoniesFrame = new StoryEventAssetFrame();
            flowerPoniesFrame = ProviderManager.SaveManager.GetAllGameData().FindStoryEventDataByName("GimmeGold").EventPrefab;

            //Ponies.Log("Is same object as GimmeGold: " + UnityEngine.Object.ReferenceEquals(flowerPoniesFrame, ProviderManager.SaveManager.GetAllGameData().FindStoryEventDataByName("GimmeGold").EventPrefab));
            //Result: true
            //This is too complex to bother duplicating, so I'll just swap sprites as needed.

            StoryEventData flowerPoniesEventData = new StoryEventDataBuilder()
            {
                overrideID = Ponies.GUID + "_FlowerPonies",
                name = "FlowerPonies",
                storyId = "FlowerPonies",
                knotName = "FlowerPonies",
                priorityTicketCount = 25,
                eventPrefab = flowerPoniesFrame,
                possibleRewards = new List<RewardData>
                {
                    ProviderManager.SaveManager.GetAllGameData().FindRewardDataByName("GoldReward100"),
                    tomeReward,
                    spikeReward,
                    secretReward
                }
            }.BuildAndRegister();

            //Already seems to be in the list.
            //if (!ProviderManager.SaveManager.GetAllGameData().GetStoryRewardList().Rewards.Contains(ProviderManager.SaveManager.GetAllGameData().FindRewardDataByName("GoldReward100")))
            //{
            //    Ponies.Log("Adding 100 gold to story reward list.");
            //    RewardDataList dataList = (RewardDataList)AccessTools.Field(typeof(AllGameData), "storyRewardList").GetValue(ProviderManager.SaveManager.GetAllGameData());
            //    dataList.Rewards.Add(ProviderManager.SaveManager.GetAllGameData().FindRewardDataByName("GoldReward100"));
            //}

            //List<StoryEventData> datas = (List<StoryEventData>)AccessTools.Field(typeof(AllGameData), "storyEventDatas").GetValue(ProviderManager.SaveManager.GetAllGameData());
            //
            //Ponies.Log("___________Staritng_Stories_Dump______________");
            //foreach (StoryEventData data in datas)
            //{
            //    Ponies.Log(data.KnotName);
            //}
            //Ponies.Log("______________________________________________");

            //List<StoryEventData> datas = (List<StoryEventData>)AccessTools.Field(typeof(StoryEventPoolData).GetNestedType("StoryEventDataList", BindingFlags.NonPublic | BindingFlags.Instance), "storyEvents").GetValue(ProviderManager.SaveManager.GetRunData().GetStartingEventPoolData());
            //datas.Add(flowerPoniesEventData);

            return flowerPoniesEventData;
        }
    }
}