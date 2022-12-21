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
    public static class IJustDontKnowWhatWentWrong_spChallenge
    {
        public static SpChallengeData data;
        public static string ID = Ponies.GUID + "_IJustDontKnowWhatWentWrong_spChallenge";
        public static string iconPath = Path.Combine(Path.GetDirectoryName(Ponies.Instance.Info.Location), "Mutators/Sprite", "SPC_PONIESTAKEOVER.png");

        public static void BuildAndRegister()
        {
            data = new spChallengeBuilder
            {
                ID = ID,
                DescriptionKey = "Pony_spChallenge_IJustDontKnowWhatWentWrong_Description_Key",
                NameKey = "Pony_spChallenge_IJustDontKnowWhatWentWrong_Name_Key",
                IconPath = iconPath,
                RequiredDLC = DLC.Hellforged,
                Mutators = new List<MutatorData>
                {
                    WorthIt.mutatorData,
                    Bubbles.mutatorData,
                    YouveGotMail.mutatorData
                }
            }.BuildAndRegister();
        }
    }
}