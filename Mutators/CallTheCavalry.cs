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
    public static class CallTheCavalryMutator 
    {
        public static MutatorData callTheCavalryMutatorData = ScriptableObject.CreateInstance<MutatorData>();
        public static string iconPath = Path.Combine(Path.GetDirectoryName(Ponies.Instance.Info.Location), "Mutators/Sprite", "MTR_CallTheCavalry.png");
        public static string ID = Ponies.GUID + "CallTheCavalyMutator";
        public static void BuildAndRegister() 
        {
            AccessTools.Field(typeof(GameData), "id").SetValue(callTheCavalryMutatorData, ID);
            AccessTools.Field(typeof(MutatorData), "nameKey").SetValue(callTheCavalryMutatorData, "Pony_Mutator_CallTheCavalry_Name_Key");
            AccessTools.Field(typeof(MutatorData), "descriptionKey").SetValue(callTheCavalryMutatorData, "Pony_Mutator_CallTheCavalry_Description_Key");
            Sprite icon = CustomAssetManager.LoadSpriteFromPath(iconPath);
            //Mod_Sprites_Setup.LoadNewSprite(iconPath,"MTR_CallTheCavalry.png");
            AccessTools.Field(typeof(MutatorData), "icon").SetValue(callTheCavalryMutatorData, icon);
            List<RelicEffectData> effects = new List<RelicEffectData>
            {
                new RelicEffectDataBuilder
                {
                    RelicEffectClassName = "RelicEffectAddRelicStartOfRun",
                    ParamRelic = CustomCollectableRelicManager.GetRelicDataByID(JunkFood.ID),
                }.Build(),
                new RelicEffectDataBuilder
                {
                    RelicEffectClassName = "RelicEffectAddRelicStartOfRun",
                    ParamRelic = CustomCollectableRelicManager.GetRelicDataByID(BottledCutieMark.ID),
                }.Build(),
                new RelicEffectDataBuilder
                { 
                    RelicEffectClassName = "RelicEffectAddRelicStartOfRun",
                    ParamRelic = CustomCollectableRelicManager.GetRelicDataByID(TheSeventhElement.ID),
                }.Build()
            };
            AccessTools.Field(typeof(MutatorData), "effects").SetValue(callTheCavalryMutatorData, effects);
            List<string> relicLoreTooltipKeys = new List<string>
            {
                "Pony_Mutator_CallTheCavalry_Lore_Key"
            };
            AccessTools.Field(typeof(MutatorData), "relicLoreTooltipKeys").SetValue(callTheCavalryMutatorData, relicLoreTooltipKeys);
            AccessTools.Field(typeof(MutatorData), "relicActivatedKey").SetValue(callTheCavalryMutatorData, "Pony_Mutator_CallTheCavalry_Activated_Key");
            AccessTools.Field(typeof(MutatorData), "divineVariant").SetValue(callTheCavalryMutatorData, false);
            AccessTools.Field(typeof(MutatorData), "boonValue").SetValue(callTheCavalryMutatorData, 10);
            AccessTools.Field(typeof(MutatorData), "disableInDailyChallenges").SetValue(callTheCavalryMutatorData, false);
            AccessTools.Field(typeof(MutatorData), "requiredDLC").SetValue(callTheCavalryMutatorData, DLC.None);
            List<String> tags = new List<String>
            {
            };
            AccessTools.Field(typeof(MutatorData), "tags").SetValue(callTheCavalryMutatorData, tags);

            AllGameData allGameData = ProviderManager.SaveManager.GetAllGameData();
            List<MutatorData> mutatorDatas = (List<MutatorData>)AccessTools.Field(typeof(AllGameData), "mutatorDatas").GetValue(allGameData);
            
            if (!mutatorDatas.Contains(callTheCavalryMutatorData)) 
            {
                mutatorDatas.Add(callTheCavalryMutatorData);
            }
        }
    }
}