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
    public static class Equalizer
    {
        public static string ID = Ponies.GUID + "_Equalizer";
        public static MutatorData mutatorData;

        public static void BuildAndRegister()
        {
            mutatorData = new MutatorDataBuilder
            {
                ID = ID,
                NameKey = "Pony_Mutator_Equalizer_Name_Key",
                DescriptionKey = "Pony_Mutator_Equalizer_Description_Key",
                RelicActivatedKey = "Pony_Mutator_Equalizer_Activated_Key",
                RelicLoreTooltipKeys = new List<string>()
                {
                    "Pony_Mutator_Equalizer_Lore_Key"
                },
                DisableInDailyChallenges = false,
                DivineVariant = false,
                BoonValue = -1,
                RequiredDLC = DLC.None,
                IconPath = "Mutators/Sprite/MTR_Equalizer.png",
                Tags = new List<string>
                {
                },
                Effects = new List<RelicEffectData>
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassName = typeof(CustomMutatorEffectReplaceHandOnSpellPlay).AssemblyQualifiedName,
                        ParamCardType = CardType.Spell,
                        ParamInt= 0,
                        AdditionalTooltips = new AdditionalTooltipData[]
                        {
                            new AdditionalTooltipData
                            {
                                titleKey = "CardTraitExhaustState_CardText",
                                descriptionKey = "CardTraitExhaustState_TooltipText",
                                isStatusTooltip = false,
                                isTipTooltip = false,
                                isTriggerTooltip = false,
                                style = TooltipDesigner.TooltipDesignType.Keyword,
                            }
                        }
                    }.Build(),
                }
            }.BuildAndRegister();
        }
    }
}
