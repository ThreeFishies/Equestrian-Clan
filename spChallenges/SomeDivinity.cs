//Brawl, Adaptive Foes, Multistrike
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
    public static class SomeDivinity_spChallenge
    {
        public static SpChallengeData data;
        public static string ID = Ponies.GUID + "_SomeDivinity_spChallenge";
        public static string iconPath = Path.Combine(Path.GetDirectoryName(Ponies.Instance.Info.Location), "Mutators/Sprite", "SPC_PONIESTAKEOVER.png");

        public static void BuildAndRegister()
        {
            data = new spChallengeBuilder
            {
                ID = ID,
                DescriptionKey = "Pony_spChallenge_SomeDivinity_Description_Key",
                NameKey = "Pony_spChallenge_SomeDivinity_Name_Key",
                IconPath = iconPath,
                RequiredDLC = DLC.Hellforged,
                Mutators = new List<MutatorData>
                {
                    DivineOmnipresence.divineOmnipresenceMutatorData,
                    ProviderManager.SaveManager.GetAllGameData().FindMutatorData("906e45aa-3aea-4fa1-8370-95877fc13318"), //No Pact Shards
                }
            }.BuildAndRegister();
        }
    }
}