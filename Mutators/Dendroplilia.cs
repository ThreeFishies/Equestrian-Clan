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
using Equestrian.MonsterCards;
using UnityEngine;
using CustomEffects;

namespace Equestrian.Mutators
{
    public static class Dendrophilia
    {
        public static string ID = Ponies.GUID + "_Dendrophilia";
        public static MutatorData mutatorData;

        public static void BuildAndRegister()
        {
            mutatorData = new MutatorDataBuilder
            {
                ID = ID,
                NameKey = "Pony_Mutator_Dendrophilia_Name_Key",
                DescriptionKey = "Pony_Mutator_Dendrophilia_Description_Key",
                RelicActivatedKey = "Pony_Mutator_Dendrophilia_Activated_Key",
                RelicLoreTooltipKeys = new List<string>()
                {
                    "Pony_Mutator_Dendrophilia_Lore_Key"
                },
                DisableInDailyChallenges = false,
                DivineVariant = false,
                BoonValue = 2,
                RequiredDLC = DLC.None,
                IconPath = "Mutators/Sprite/MTR_Dendrophilia.png",
                Tags = new List<string>
                {
                },
                Effects = new List<RelicEffectData>
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassName = "RelicEffectAddRelicStartOfRun",
                        //Bloomberg
                        ParamRelic = CustomCollectableRelicManager.GetRelicDataByID(Bloomberg.ID),
                    }.Build(),
                }
            }.BuildAndRegister();
        }
    }
}
