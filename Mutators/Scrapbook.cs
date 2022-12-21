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
    public static class Scrapbook
    {
        public static string ID = Ponies.GUID + "_Scrapbook";
        public static MutatorData mutatorData;

        public static bool HasScrapbook() 
        {
            if (ProviderManager.SaveManager.GetMutatorCount() > 0)
            {
                List<MutatorState> mutators = ProviderManager.SaveManager.GetMutators();
                foreach (MutatorState mutator in mutators)
                {
                    if (mutator.GetRelicDataID() == ID)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void BuildAndRegister()
        {
            mutatorData = new MutatorDataBuilder
            {
                ID = ID,
                NameKey = "Pony_Mutator_Scrapbook_Name_Key",
                DescriptionKey = "Pony_Mutator_Scrapbook_Description_Key",
                RelicActivatedKey = "Pony_Mutator_Scrapbook_Activated_Key",
                RelicLoreTooltipKeys = new List<string>()
                {
                    "Pony_Mutator_Scrapbook_Lore_Key"
                },
                DisableInDailyChallenges = false,
                DivineVariant = false,
                BoonValue = -8,
                RequiredDLC = DLC.None,
                IconPath = "Mutators/Sprite/MTR_Scrapbook.png",
                Tags = new List<string>
                {
                },
                Effects = new List<RelicEffectData>
                {
                    new RelicEffectDataBuilder
                    {
                        //See ScrapbookPatch.cs
                        RelicEffectClassName = "RelicEffectNull",
                    }.Build(),
                }
            }.BuildAndRegister();
        }
    }
}
