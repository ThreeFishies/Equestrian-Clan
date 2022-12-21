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
using Equestrian.Mutators;

namespace Equestrian.Unused
{

    public static class AllOutAssault_spChallenge
    {
        public static SpChallengeData data;
        public static string ID = Ponies.GUID + "_AllOutAssault_spChallenge";
        public static string iconPath = Path.Combine(Path.GetDirectoryName(Ponies.Instance.Info.Location), "Mutators/Sprite", "SPC_PONIESTAKEOVER.png");

        public static void BuildAndRegister()
        {
            data = new spChallengeBuilder
            {
                ID = ID,
                DescriptionKey = "Pony_spChallenge_AllOutAssault_Description_Key",
                NameKey = "Pony_spChallenge_AllOutAssault_Name_Key",
                IconPath = iconPath,
                RequiredDLC = DLC.None,
                Mutators = new List<MutatorData>
                {
                    ProviderManager.SaveManager.GetAllGameData().FindMutatorData("b75d5726-62ef-47ce-9378-ac3721594bab"), //Brawl
                    AdaptiveFoes.mutatorData,
                    ProviderManager.SaveManager.GetAllGameData().FindMutatorData("387f73cb-cdbb-44a4-8708-6d42cf5f7455"), //Enemies Get Damage shield                
                }
            }.BuildAndRegister();
        }
    }
}
