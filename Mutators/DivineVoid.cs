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

    public static class DivineVoid
    {
        public static MutatorData divineVoidMutatorData = ScriptableObject.CreateInstance<MutatorData>();
        public static string iconPath = Path.Combine(Path.GetDirectoryName(Ponies.Instance.Info.Location), "Mutators/Sprite", "MTR_DivineVoid.png");
        public static string ID = Ponies.GUID + "_DivineVoidMutator";
        public static void BuildAndRegister()
        {
            AccessTools.Field(typeof(GameData), "id").SetValue(divineVoidMutatorData, ID);
            AccessTools.Field(typeof(MutatorData), "nameKey").SetValue(divineVoidMutatorData, "Pony_Mutator_DivineVoid_Name_Key");
            AccessTools.Field(typeof(MutatorData), "descriptionKey").SetValue(divineVoidMutatorData, "Pony_Mutator_DivineVoid_Description_Key");
            Sprite icon = CustomAssetManager.LoadSpriteFromPath(iconPath);
            AccessTools.Field(typeof(MutatorData), "icon").SetValue(divineVoidMutatorData, icon);
            List<RelicEffectData> effects = new List<RelicEffectData>
            {
                new RelicEffectDataBuilder
                {
                    RelicEffectClassName = "RelicEffectNull"
                }.Build(),
            };
            AccessTools.Field(typeof(MutatorData), "effects").SetValue(divineVoidMutatorData, effects);
            List<string> relicLoreTooltipKeys = new List<string>
            {
                "Pony_Mutator_DivineVoid_Lore_Key"
            };
            AccessTools.Field(typeof(MutatorData), "relicLoreTooltipKeys").SetValue(divineVoidMutatorData, relicLoreTooltipKeys);
            AccessTools.Field(typeof(MutatorData), "relicLoreTooltipStyle").SetValue(divineVoidMutatorData, Ponies.PonyRelicTooltip.GetEnum());
            AccessTools.Field(typeof(MutatorData), "relicActivatedKey").SetValue(divineVoidMutatorData, "Pony_Mutator_DivineVoid_Activated_Key");
            AccessTools.Field(typeof(MutatorData), "divineVariant").SetValue(divineVoidMutatorData, false);
            AccessTools.Field(typeof(MutatorData), "boonValue").SetValue(divineVoidMutatorData, 2);
            AccessTools.Field(typeof(MutatorData), "disableInDailyChallenges").SetValue(divineVoidMutatorData, true);
            AccessTools.Field(typeof(MutatorData), "requiredDLC").SetValue(divineVoidMutatorData, DLC.Hellforged);
            List<String> tags = new List<String>
            {
                "Divinity"
            };
            AccessTools.Field(typeof(MutatorData), "tags").SetValue(divineVoidMutatorData, tags);

            AllGameData allGameData = ProviderManager.SaveManager.GetAllGameData();
            List<MutatorData> mutatorDatas = (List<MutatorData>)AccessTools.Field(typeof(AllGameData), "mutatorDatas").GetValue(allGameData);

            if (!mutatorDatas.Contains(divineVoidMutatorData))
            {
                mutatorDatas.Add(divineVoidMutatorData);
            }
        }
    }
}