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
    public static class WorthIt
    {
        public static string ID = Ponies.GUID + "_WorthIt";
        public static MutatorData mutatorData;

        public static void Activate() 
        {
            if (ProviderManager.SaveManager.GetMutatorCount() > 0)
            {
                List<MutatorState> mutators = ProviderManager.SaveManager.GetMutators();
                foreach (MutatorState mutator in mutators)
                {
                    if (mutator.GetRelicDataID() == ID)
                    {
                        CustomMutatorEffectAddUpgradeToUnitsForPactShardsAtStartOfRun.ApplyEffectAtStartOfRun();
                    }
                }
            }
        }

        public static void BuildAndRegister()
        {
            CardData trainSteward = ProviderManager.SaveManager.GetAllGameData().FindCardData("d14a50f3-728d-43e1-87f0-ef1b013f6678");
            CardUpgradeData stewardSynthesis = ProviderManager.SaveManager.GetBalanceData().SynthesisMapping.GetUpgradeData(trainSteward.GetSpawnCharacterData());

            mutatorData = new MutatorDataBuilder
            {
                ID = ID,
                NameKey = "Pony_Mutator_WorthIt_Name_Key",
                DescriptionKey = "Pony_Mutator_WorthIt_Description_Key",
                RelicActivatedKey = "Pony_Mutator_WorthIt_Activated_Key",
                RelicLoreTooltipKeys = new List<string>()
                {
                    "Pony_Mutator_WorthIt_Lore_Key"
                },
                DisableInDailyChallenges = false,
                DivineVariant = false,
                BoonValue = -4,
                RequiredDLC = DLC.Hellforged,
                IconPath = "Mutators/Sprite/MTR_WorthIt.png",
                Tags = new List<string>
                {
                },
                Effects = new List<RelicEffectData>
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassName = typeof(CustomMutatorEffectAddUpgradeToUnitsForPactShardsAtStartOfRun).AssemblyQualifiedName,
                        ParamInt = 50,
                        ParamCharacters = new List<CharacterData>
                        {
                            trainSteward.GetSpawnCharacterData(),
                        },
                        ParamCardUpgradeData = stewardSynthesis
                    }.Build(),
                }
            }.BuildAndRegister();

            //Zap this to reset it.
            AccessTools.Field(typeof(UnitSynthesisMapping), "_dictionaryMapping").SetValue(ProviderManager.SaveManager.GetBalanceData().SynthesisMapping, null);
        }
    }
}
