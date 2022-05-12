using Trainworks.Managers;
using Trainworks.Builders;
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

namespace Equestrian.Mutators
{
    //SaveManager "GetVictorySectionState"

    public static class DivineOmnipresence
    {
        public static MutatorData divineOmnipresenceMutatorData = ScriptableObject.CreateInstance<MutatorData>();
        public static string iconPath = Path.Combine(Path.GetDirectoryName(Ponies.Instance.Info.Location), "Mutators/Sprite", "MTR_DivineOmnipresence.png");
        public static string ID = Ponies.GUID + "_DivineOmnipresenceMutator";
        public static void BuildAndRegister()
        {
            AccessTools.Field(typeof(GameData), "id").SetValue(divineOmnipresenceMutatorData, ID);
            AccessTools.Field(typeof(MutatorData), "nameKey").SetValue(divineOmnipresenceMutatorData, "Pony_Mutator_DivineOmnipresence_Name_Key");
            AccessTools.Field(typeof(MutatorData), "descriptionKey").SetValue(divineOmnipresenceMutatorData, "Pony_Mutator_DivineOmnipresence_Description_Key");
            Sprite icon = CustomAssetManager.LoadSpriteFromPath(iconPath);
            AccessTools.Field(typeof(MutatorData), "icon").SetValue(divineOmnipresenceMutatorData, icon);
            List<RelicEffectData> effects = new List<RelicEffectData>
            {
                new RelicEffectDataBuilder
                {
                    RelicEffectClassName = "RelicEffectNull"
                }.Build(),
            };
            AccessTools.Field(typeof(MutatorData), "effects").SetValue(divineOmnipresenceMutatorData, effects);
            List<string> relicLoreTooltipKeys = new List<string>
            {
                "Pony_Mutator_DivineOmnipresence_Lore_Key"
            };
            AccessTools.Field(typeof(MutatorData), "relicLoreTooltipKeys").SetValue(divineOmnipresenceMutatorData, relicLoreTooltipKeys);
            AccessTools.Field(typeof(MutatorData), "relicActivatedKey").SetValue(divineOmnipresenceMutatorData, "Pony_Mutator_DivineOmnipresence_Activated_Key");
            AccessTools.Field(typeof(MutatorData), "divineVariant").SetValue(divineOmnipresenceMutatorData, false);
            AccessTools.Field(typeof(MutatorData), "boonValue").SetValue(divineOmnipresenceMutatorData, -2);
            AccessTools.Field(typeof(MutatorData), "disableInDailyChallenges").SetValue(divineOmnipresenceMutatorData, true);
            AccessTools.Field(typeof(MutatorData), "requiredDLC").SetValue(divineOmnipresenceMutatorData, DLC.Hellforged);
            List<String> tags = new List<String>
            {
                "Divinity"
            };
            AccessTools.Field(typeof(MutatorData), "tags").SetValue(divineOmnipresenceMutatorData, tags);

            AllGameData allGameData = ProviderManager.SaveManager.GetAllGameData();
            List<MutatorData> mutatorDatas = (List<MutatorData>)AccessTools.Field(typeof(AllGameData), "mutatorDatas").GetValue(allGameData);

            if (!mutatorDatas.Contains(divineOmnipresenceMutatorData))
            {
                mutatorDatas.Add(divineOmnipresenceMutatorData);
            }
        }
    }
}