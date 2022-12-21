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
    public static class Bubbles
    {
        public static string ID = Ponies.GUID + "_Bubbles";
        public static MutatorData mutatorData;

        public static void BuildAndRegister() 
        {
            mutatorData = new MutatorDataBuilder
            {
                ID = ID,
                NameKey = "Pony_Mutator_Bubbles_Name_Key",
                DescriptionKey = "Pony_Mutator_Bubbles_Description_Key",
                RelicActivatedKey = "Pony_Mutator_Bubbles_Activated_Key",
                RelicLoreTooltipKeys = new List<string>()
                {
                    "Pony_Mutator_Bubbles_Lore_Key"
                },
                DisableInDailyChallenges = false,
                DivineVariant = false,
                BoonValue = -4,
                RequiredDLC = DLC.None,
                IconPath = "Mutators/Sprite/MTR_Bubbles.png",
                Tags = new List<string>
                {
                    "enemyeffect"
                },
                Effects = new List<RelicEffectData>
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassName = "RelicEffectAddTempUpgrade",
                        ParamSourceTeam = Team.Type.Monsters,
                        ParamTargetMode = TargetMode.Room,
                        ParamBool = false,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                            new StatusEffectStackData
                            {
                                statusId = VanillaStatusEffectIDs.Spellshield,
                                count = 1
                            }
                        },
                        ParamCardUpgradeData = new CardUpgradeDataBuilder
                        {
                            UpgradeTitle = "BubbleMutatorShield",
                            BonusHP = 0,
                            BonusDamage = 0,
                            StatusEffectUpgrades = new List<StatusEffectStackData>
                            {
                                new StatusEffectStackData
                                {
                                    statusId = VanillaStatusEffectIDs.Spellshield,
                                    count = 1
                                }
                            },
                            Filters = new List<CardUpgradeMaskData>
                            {
                                new CardUpgradeMaskDataBuilderFixed
                                {
                                    CardType = CardType.Monster
                                }.Build(),
                            }
                        }.Build(),
                    }.Build(),
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassName = "RelicEffectAddStatusEffectOnSpawn",
                        ParamSourceTeam = Team.Type.Heroes,
                        ParamTargetMode = TargetMode.Room,
                        ParamBool = false,
                        ParamInt = 0,
                        ParamString = "",
                        ParamExcludeCharacterSubtypes = new string[] { },
                        ParamCharacterSubtype = "SubtypesData_None",
                        ParamStatusEffects = new StatusEffectStackData[] 
                        {  
                            new StatusEffectStackData
                            {
                                statusId = VanillaStatusEffectIDs.Spellshield,
                                count = 1
                            }
                        },
                    }.Build()
                }
            }.BuildAndRegister();
        }
    }
}