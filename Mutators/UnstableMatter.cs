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
    public static class UnstableMatter
    {
        public static string ID = Ponies.GUID + "_UnstableMatter";
        public static MutatorData mutatorData;

        public static void BuildAndRegister() 
        {
            mutatorData = new MutatorDataBuilder
            {
                ID = ID,
                NameKey = "Pony_Mutator_UnstableMatter_Name_Key",
                DescriptionKey = "Pony_Mutator_UnstableMatter_Description_Key",
                RelicActivatedKey = "Pony_Mutator_UnstableMatter_Activated_Key",
                RelicLoreTooltipKeys = new List<string>()
                {
                    "Pony_Mutator_UnstableMatter_Lore_Key"
                },
                DisableInDailyChallenges = false,
                DivineVariant = false,
                BoonValue = -4,
                RequiredDLC = DLC.None,
                IconPath = "Mutators/Sprite/MTR_UnstableMatter.png",
                Tags = new List<string>
                {
                    "friendlyeffect"
                },
                Effects = new List<RelicEffectData>
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassName = typeof(CustomMutatorEffectAddUpgradeToAllCards).AssemblyQualifiedName,
                        ParamSourceTeam = Team.Type.Monsters,
                        ParamInt = -1,
                        ParamFloat = 0.0f,
                        ParamTrigger = CharacterTriggerData.Trigger.OnDeath,
                        ParamTargetMode = TargetMode.FrontInRoom,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                        },
                        ExcludedTraits = new List<CardTraitData>
                        {
                            new CardTraitDataBuilder
                            {
                                TraitStateName = "CustomCardTraitDummy"
                            }.Build(),
                        },
                        ParamCardUpgradeData = new CardUpgradeDataBuilder
                        {
                            UpgradeTitle = "UnstableMatter_Mutator_Effect_KaBlooie",
                            BonusDamage = 0,
                            BonusHeal = 0,

                            Filters = new List<CardUpgradeMaskData>
                            {
                                new CardUpgradeMaskDataBuilderFixed
                                {
                                    CardType = CardType.Monster,
                                    ExcludedCardTraits = new List<string>
                                    {
                                        "CustomCardTraitDummy",
                                        typeof(CustomCardTraitDummy).Name,
                                        typeof(CustomCardTraitDummy).AssemblyQualifiedName
                                    }
                                }.Build(),
                            },

                            TraitDataUpgradeBuilders = new List<CardTraitDataBuilder>
                            {
                                new CardTraitDataBuilder
                                {
                                    TraitStateName = typeof(CustomCardTraitDummy).AssemblyQualifiedName,
                                }
                            },

                            TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
                            {
                                new CharacterTriggerDataBuilder
                                {
                                    Trigger = CharacterTriggerData.Trigger.OnUnscaledSpawn,
                                    DisplayEffectHintText = false,
                                    HideTriggerTooltip = true,

                                    EffectBuilders = new List<CardEffectDataBuilder>
                                    {
                                        new CardEffectDataBuilder
                                        {
                                            EffectStateName = typeof(CustomEffectKablooie).AssemblyQualifiedName,
                                            ParamInt = 50,
                                            TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
                                            ParamMinInt = 33,
                                            ParamMaxInt = 33,
                                            HideTooltip = true,
                                            TargetMode = TargetMode.Self,
                                            ParamStr = "SubtypesData_Champion_83f21cbe-9d9b-4566-a2c3-ca559ab8ff34",
                                            
                                        }
                                    }
                                }
                            },
                        }.Build(),
                    }.Build(),
                }
            }.BuildAndRegister();
        }
    }
}