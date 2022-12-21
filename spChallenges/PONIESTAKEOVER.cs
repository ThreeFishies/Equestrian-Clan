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
using UnityEngine;
using CustomEffects;
using Equestrian.Metagame;

namespace Equestrian.Mutators
{
    public static class PONIESTAKEOVER_spChallenge
    {
        public static SpChallengeData data;
        public static string ID = Ponies.GUID + "_PONIESTAKEOVER_spChallenge";
        public static string iconPath = Path.Combine(Path.GetDirectoryName(Ponies.Instance.Info.Location), "Mutators/Sprite", "SPC_PONIESTAKEOVER.png");

        public static void BuildAndRegister() 
        {
            data = UnityEngine.ScriptableObject.CreateInstance<SpChallengeData>();
            List<MutatorData> mutators = new List<MutatorData>()
            {
                DesertionMutator.desertionMutatorData,
                ProviderManager.SaveManager.GetAllGameData().FindMutatorData("4bca64b3-2976-45b9-b49b-9f6368d5dab3"), //Purge champion
                CallTheCavalryMutator.callTheCavalryMutatorData
            };

            AccessTools.Field(typeof(GameData), "id").SetValue(data, ID);
            AccessTools.Field(typeof(SpChallengeData), "nameKey").SetValue(data, "Pony_spChallenge_PONIESTAKEOVER_Name_Key");
            AccessTools.Field(typeof(SpChallengeData), "descriptionKey").SetValue(data, "Pony_spChallenge_PONIESTAKEOVER_Description_Key");
            Sprite icon = CustomAssetManager.LoadSpriteFromPath(iconPath);
            AccessTools.Field(typeof(SpChallengeData), "icon").SetValue(data, icon);
            AccessTools.Field(typeof(SpChallengeData), "requiredDLC").SetValue(data, DLC.None);
            AccessTools.Field(typeof(SpChallengeData), "mutators").SetValue(data, mutators);

            List<SpChallengeData> allChallenges = (List<SpChallengeData>)AccessTools.Field(typeof(AllGameData), "spChallengeDatas").GetValue(ProviderManager.SaveManager.GetAllGameData());
            
            if (!allChallenges.Contains(data)) 
            { 
                allChallenges.Add(data);
            }

            List<SpChallengeData> allChallenges2 = (List<SpChallengeData>)AccessTools.Field(typeof(BalanceData), "spChallenges").GetValue(ProviderManager.SaveManager.GetBalanceData());

            if (!allChallenges2.Contains(data))
            {
                allChallenges2.Add(data);
            }

            PonyMetagame.RegisterChallenge(ID);
        }
    }
}