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
    public static class ReadyForAnything
    {
        public static string ID = Ponies.GUID + "_ReadyForAnything";
        public static MutatorData mutatorData;

        public static void BuildAndRegister()
        {
            mutatorData = new MutatorDataBuilder
            {
                ID = ID,
                NameKey = "Pony_Mutator_ReadyForAnything_Name_Key",
                DescriptionKey = "Pony_Mutator_ReadyForAnything_Description_Key",
                RelicActivatedKey = "Pony_Mutator_ReadyForAnything_Activated_Key",
                RelicLoreTooltipKeys = new List<string>()
                {
                    "Pony_Mutator_ReadyForAnything_Lore_Key"
                },
                DisableInDailyChallenges = false,
                DivineVariant = false,
                BoonValue = 12,
                RequiredDLC = DLC.Hellforged,
                IconPath = "Mutators/Sprite/MTR_ReadyForAnything.png",
                Tags = new List<string>
                {
                },
                Effects = new List<RelicEffectData>
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassName = "RelicEffectAddRelicStartOfRun",
                        //Blank Pages
                        ParamRelic = ProviderManager.SaveManager.GetAllGameData().FindCollectableRelicData("6bb39014-9b03-4620-b088-f618a7e680b7")
                    }.Build(),
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassName = "RelicEffectAddRelicStartOfRun",
                        //Shard of Divinity
                        ParamRelic = ProviderManager.SaveManager.GetAllGameData().FindCollectableRelicData("b6282e63-9c8e-477f-9797-ecd43412e47a")
                    }.Build()
                }
            }.BuildAndRegister();

            //Edit the 150 pact shard Heph event to be excluded with this mutator.
            //This replaces the 'Shardless' mutator exclusion, but since you'll never reach 150 pact shards with that one, I don't see the point.
            StoryEventData Shards_HephArtifact = ProviderManager.SaveManager.GetAllGameData().FindStoryEventData("2997b92c-8b1c-4c62-8cc9-78c06ff0fe68");
            AccessTools.Field(typeof(StoryEventData), "excludedMutator").SetValue(Shards_HephArtifact, mutatorData);
        }
    }
}
