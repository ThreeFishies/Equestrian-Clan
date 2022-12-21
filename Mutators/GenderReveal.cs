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
    public static class GenderReveal
    {
        public static string ID = Ponies.GUID + "_GenderReveal";
        public static MutatorData mutatorData;

        public enum Gender
        {
            Male = 0,
            Female = 1,
            Genderless = 2,
            Undefined = 3
        }

        public static Gender GetGender(CharacterState character)
        {
            if (character != null)
            {
                if (character.GetCharacterChatterData() != null)
                {
                    return GetGender(character.GetCharacterChatterData());
                }
            }

            return Gender.Undefined;
        }

        public static Gender GetGender(CharacterData character)
        {
            if (character != null)
            {
                if (character.GetCharacterChatterData() != null)
                {
                    return GetGender(character.GetCharacterChatterData());
                }
            }

            return Gender.Undefined;
        }

        private static Gender GetGender(CharacterChatterData chatterData)
        { 
            switch (chatterData.GetGender())
            {
                case CharacterChatterData.Gender.Male:
                {
                    return Gender.Male;
                }
                case CharacterChatterData.Gender.Female:
                {
                    return Gender.Female;
                }
                case CharacterChatterData.Gender.Neutral:
                {
                    return Gender.Genderless;
                }
                default:
                {
                    return Gender.Undefined;
                }
            }
        }

        public static void BuildAndRegister()
        {
            mutatorData = new MutatorDataBuilder
            {
                ID = ID,
                NameKey = "Pony_Mutator_GenderReveal_Name_Key",
                DescriptionKey = "Pony_Mutator_GenderReveal_Description_Key",
                RelicActivatedKey = "Pony_Mutator_GenderReveal_Activated_Key",
                RelicLoreTooltipKeys = new List<string>()
                {
                    "Pony_Mutator_GenderReveal_Lore_Key"
                },
                DisableInDailyChallenges = true,
                DivineVariant = false,
                BoonValue = 0,
                RequiredDLC = DLC.None,
                IconPath = "Mutators/Sprite/MTR_GenderReveal.png",
                Tags = new List<string>
                {
                },
                Effects = new List<RelicEffectData>
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassName = typeof(CustomMutatorEffectAddGenderOnSpawn).AssemblyQualifiedName,
                        ParamSourceTeam = Team.Type.Monsters,
                        ParamBool = false,
                        ParamInt = 0,
                        ParamString = String.Empty
                    }.Build(),
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassName = typeof(CustomMutatorEffectAddGenderOnSpawn).AssemblyQualifiedName,
                        ParamSourceTeam = Team.Type.Heroes,
                        ParamBool = false,
                        ParamInt = 0,
                        ParamString = String.Empty
                    }.Build(),
                    /*
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassName = "RelicEffectChanceAddEffectOnSpawn",
                        ParamSourceTeam = Team.Type.Monsters,
                        ParamTargetMode = TargetMode.Self,
                        ParamInt = 100,
                        ParamBool = false,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                        },
                        ParamExcludeCharacterSubtypes = new string[]
                        { 
                        },
                        ParamCardEffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateName = typeof(CustomCardEffectAddGenderTag).AssemblyQualifiedName,
                                TargetMode = TargetMode.Self,
                                TargetTeamType = Team.Type.Monsters,
                                ParamMaxInt = 0,
                                ParamMinInt = 0,
                                ParamBool = false,
                                ParamInt = 0,
                            }
                        }
                    }.Build(),
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassName = "RelicEffectChanceAddEffectOnSpawn",
                        ParamSourceTeam = Team.Type.Heroes,
                        ParamTargetMode = TargetMode.Self,
                        ParamInt = 100,
                        ParamBool = false,
                        ParamExcludeCharacterSubtypes = new string[]
                        {
                        },
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                        },
                        ParamCardEffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateName = typeof(CustomCardEffectAddGenderTag).AssemblyQualifiedName,
                                TargetMode = TargetMode.Self,
                                TargetTeamType = Team.Type.Heroes,
                                ParamMaxInt = 0,
                                ParamMinInt = 0,
                                ParamBool = false,
                                ParamInt = 0,
                            }
                        }
                    }.Build(),
                    */
                }
            }.BuildAndRegister();
        }
    }
}