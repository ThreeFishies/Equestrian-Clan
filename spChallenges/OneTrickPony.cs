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
    public static class OneTrickPony_spChallenge
    {
        public static SpChallengeData data;
        public static string ID = Ponies.GUID + "_OneTrickPony_spChallenge";
        public static string iconPath = Path.Combine(Path.GetDirectoryName(Ponies.Instance.Info.Location), "Mutators/Sprite", "SPC_PONIESTAKEOVER.png");

        public static void BuildAndRegister()
        {
            data = new spChallengeBuilder
            {
                ID = ID,
                DescriptionKey = "Pony_spChallenge_OneTrickPony_Description_Key",
                NameKey = "Pony_spChallenge_OneTrickPony_Name_Key",
                IconPath = iconPath,
                RequiredDLC = DLC.None,
                Mutators = new List<MutatorData>
                {
                    WantItNeedIt.mutatorData,
                    //ProviderManager.SaveManager.GetAllGameData().FindMutatorData("2dd0e035-96a9-46f3-8c52-eacbe82e871b"), //Hand doesn't discard.
                    Equalizer.mutatorData,
                    NoRespite.mutatorData
                }
            }.BuildAndRegister();
        }
    }
}