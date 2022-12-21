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
    public static class AdaptiveFoes
    {
        public static string ID = Ponies.GUID + "_AdaptiveFoes";
        public static MutatorData mutatorData;
        public static void BuildAndRegister()
        {
            Predicate<CharacterData> predicateA = delegate (CharacterData characterDataA)
            {
                return characterDataA.GetID() == "7feea299-a4fd-4635-898d-7c51995462fd";
            };
            CharacterData trapA = ProviderManager.SaveManager.GetAllGameData().GetAllCharacterData().Find(predicateA);
            AccessTools.Field(typeof(CharacterData), "subtypeKeys").SetValue(trapA, new List<String> { SubtypeTrap.Key });

            Predicate<CharacterData> predicateB = delegate (CharacterData characterDataB)
            {
                return characterDataB.GetID() == "86cf445a-382b-4b8a-bb2d-56b269afae93";
            };
            CharacterData trapB = ProviderManager.SaveManager.GetAllGameData().GetAllCharacterData().Find(predicateB);
            AccessTools.Field(typeof(CharacterData), "subtypeKeys").SetValue(trapB, new List<String> { SubtypeTrap.Key });

            Predicate<CharacterData> predicateC = delegate (CharacterData characterDataC)
            {
                return characterDataC.GetID() == "e8266980-3df5-4079-a3a6-f39521118e2c";
            };
            CharacterData trapC = ProviderManager.SaveManager.GetAllGameData().GetAllCharacterData().Find(predicateC);
            AccessTools.Field(typeof(CharacterData), "subtypeKeys").SetValue(trapC, new List<String> { SubtypeTrap.Key });

            mutatorData = new MutatorDataBuilder
            {
                ID = ID,
                NameKey = "Pony_Mutator_AdaptiveFoes_Name_Key",
                DescriptionKey = "Pony_Mutator_AdaptiveFoes_Description_Key",
                RelicActivatedKey = "Pony_Mutator_AdaptiveFoes_Activated_Key",
                RelicLoreTooltipKeys = new List<string>()
                {
                    "Pony_Mutator_AdaptiveFoes_Lore_Key"
                },
                DisableInDailyChallenges = false,
                DivineVariant = false,
                BoonValue = -6,
                RequiredDLC = DLC.None,
                IconPath = "Mutators/Sprite/MTR_AdaptiveFoes.png",
                Tags = new List<string>
                {
                    "enemyeffect"
                },
                Effects = new List<RelicEffectData>
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassName = "RelicEffectChanceAddEffectOnSpawn",
                        ParamSourceTeam = Team.Type.Heroes,
                        ParamInt = 50,
                        ParamTargetMode = TargetMode.Room,
                        ParamExcludeCharacterSubtypes = new string[]
                        {
                            "SubtypesData_Boss",
                            "SubtypesData_TreasureCollector",
                            "SubtypesData_ExplodingBarrel",
                            "SubtypesData_Statue",
                            SubtypeTrap.Key,
                        },

                        ParamCardEffects = new List<CardEffectData>
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateName = "CardEffectAddTempSwapStats",
                                TargetMode = TargetMode.Self,
                                TargetTeamType = Team.Type.Heroes,
                                TargetIgnoreBosses = true,
                            }.Build(),
                        },
                        AdditionalTooltips = new AdditionalTooltipData[]
                        {
                            new AdditionalTooltipData
                            {
                                titleKey = "",
                                descriptionKey = "TipTooltip_CanReduceHealthToZero",
                                isStatusTooltip = false,
                                isTriggerTooltip = false,
                                isTipTooltip = true,
                                trigger = CharacterTriggerData.Trigger.OnDeath,
                                statusId = "",
                                style = TooltipDesigner.TooltipDesignType.Default
                            }
                        }
                        
                    }.Build()
                }
            }.BuildAndRegister();
        }
    }
}