using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using ShinyShoe;
using Trainworks.Managers;
using Trainworks.Utilities;
using Trainworks.Builders;
using UnityEngine;
using Equestrian.Init;
using Equestrian.Metagame;
using System.IO;


namespace Trainworks.Builders
{
    /// <summary>
    /// Expert challenges ask the player to complete a run with up to three specifically-chosen mutators.
    /// They can be easy or hard, but should not be impossible.
    /// </summary>
    public class spChallengeBuilder
    {
        /// <summary>
        /// The ID overwrites the base GameObject ID. This needs to be unique.
        /// </summary>
        public string ID = "";

        /// <summary>
        /// Subdirectory and filename.
        /// ex: "Mutators/Sprite/SPC_PONIESTAKEOVER.png"
        /// <br></br>
        /// Icon should be 76x76. The icon is not currently used, and all in-game expert challenges use the Hornbreaker Prince emote as the icon.
        /// </summary>
        public string IconPath = "";

        /// <summary>
        /// These are the mutators that will be part of the challenge.
        /// Include up to three.
        /// <br></br>
        /// ex: ProviderManager.SaveManager.GetAllGameData().FindMutatorData("4bca64b3-2976-45b9-b49b-9f6368d5dab3"), //Purge champion
        /// </summary>
        public List<MutatorData> Mutators = new List<MutatorData>() { };

        /// <summary>
        /// Localization key. This determines the name of the expert challenge.
        /// </summary>
        public string NameKey = "";

        /// <summary>
        /// Localization key. A short blurb that tells the player something about this challenge.
        /// </summary>
        public string DescriptionKey = "";

        /// <summary>
        /// This determines whether the player needs the DLC to be active to take on this Expert Challenge.
        /// <br></br>
        /// Defaults to 'DLC.None'.
        /// </summary>
        public DLC RequiredDLC = DLC.None;

        /// <summary>
        /// Builds the spChallengeData.
        /// </summary>
        /// <returns>The newly-created spChallengeData.</returns>
        public SpChallengeData Build()
        {
            SpChallengeData data = UnityEngine.ScriptableObject.CreateInstance<SpChallengeData>();

            AccessTools.Field(typeof(GameData), "id").SetValue(data, ID);
            string fullIconPath = Path.Combine(Path.GetDirectoryName(Ponies.Instance.Info.Location), IconPath);
            Sprite icon = CustomAssetManager.LoadSpriteFromPath(fullIconPath);
            AccessTools.Field(typeof(SpChallengeData), "icon").SetValue(data, icon);
            AccessTools.Field(typeof(SpChallengeData), "mutators").SetValue(data, Mutators);
            AccessTools.Field(typeof(SpChallengeData), "nameKey").SetValue(data, NameKey);
            AccessTools.Field(typeof(SpChallengeData), "descriptionKey").SetValue(data, DescriptionKey);
            AccessTools.Field(typeof(SpChallengeData), "requiredDLC").SetValue(data, RequiredDLC);
            return data;
        }

        /// <summary>
        /// Adds the spChallengeData to AllGameData and BalanceData. Please note that expert challenges are not sorted, and new challenges will appear at the bottom of the list in the order that they've been added.
        /// <br></br><br></br>
        /// Also note that victory check marks are handled by the Equestrian metadata manager, and the challenge is registered there as well.
        /// </summary>
        /// <param name="spChallengeData"></param>
        /// <returns>False is data is null or has already been added. True otherwise.</returns>
        public bool Register(SpChallengeData spChallengeData) 
        { 
            if (spChallengeData == null) { return false; }

            List<SpChallengeData> allChallenges = (List<SpChallengeData>)AccessTools.Field(typeof(AllGameData), "spChallengeDatas").GetValue(ProviderManager.SaveManager.GetAllGameData());

            if (!allChallenges.Contains(spChallengeData))
            {
                allChallenges.Add(spChallengeData);
            }
            else 
            {
                return false;
            }

            List<SpChallengeData> allChallenges2 = (List<SpChallengeData>)AccessTools.Field(typeof(BalanceData), "spChallenges").GetValue(ProviderManager.SaveManager.GetBalanceData());

            if (!allChallenges2.Contains(spChallengeData))
            {
                allChallenges2.Add(spChallengeData);
            }

            PonyMetagame.RegisterChallenge(ID);

            return true;
        }

        /// <summary>
        /// Builds a new SpChallengeData and adds it to AllGameData, BalanceData, and the Equestrian metagame manager.
        /// </summary>
        /// <returns>The newly-created SpChallengeData.</returns>
        public SpChallengeData BuildAndRegister() 
        { 
            SpChallengeData data = this.Build();
            this.Register(data);
            return data;
        }
    }
}