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
    public static class GenderRevealParty_spChallenge
    {
        public static SpChallengeData data;
        public static string ID = Ponies.GUID + "_GenderRevealParty_spChallenge";
        public static string iconPath = Path.Combine(Path.GetDirectoryName(Ponies.Instance.Info.Location), "Mutators/Sprite", "SPC_PONIESTAKEOVER.png");

        public static void BuildAndRegister()
        {
            data = new spChallengeBuilder
            {
                ID = ID,
                DescriptionKey = "Pony_spChallenge_GenderRevealParty_Description_Key",
                NameKey = "Pony_spChallenge_GenderRevealParty_Name_Key",
                IconPath = iconPath,
                RequiredDLC = DLC.None,
                Mutators = new List<MutatorData>
                {
                    GenderReveal.mutatorData,
                    ProviderManager.SaveManager.GetAllGameData().FindMutatorData("95cd7890-fa1c-4f27-8f38-b2d27b1ea8ad"), //Slow and Steady
                    ProviderManager.SaveManager.GetAllGameData().FindMutatorData("e9895424-feb8-4440-8e95-da6da579b5d8"), //Wand of Cure Light Wounds
                }
            }.BuildAndRegister();
        }
    }
}