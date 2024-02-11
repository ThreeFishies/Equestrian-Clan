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
    public static class Hoarding_spChallenge
    {
        public static SpChallengeData data;
        public static string ID = Ponies.GUID + "_Hoarding_spChallenge";
        public static string iconPath = Path.Combine(Path.GetDirectoryName(Ponies.Instance.Info.Location), "Mutators/Sprite", "SPC_PONIESTAKEOVER.png");

        public static void BuildAndRegister()
        {
            data = new spChallengeBuilder
            {
                ID = ID,
                DescriptionKey = "Pony_spChallenge_Hoarding_Description_Key",
                NameKey = "Pony_spChallenge_Hoarding_Name_Key",
                IconPath = iconPath,
                RequiredDLC = DLC.None,
                Mutators = new List<MutatorData>
                {
                    Scrapbook.mutatorData,
                    Dendrophilia.mutatorData,
                    ProviderManager.SaveManager.GetAllGameData().FindMutatorData("ee32b5c9-c46d-4146-8cb3-c8b811e555dc"), //Two Cards For One
                }
            }.BuildAndRegister();
        }
    }
}