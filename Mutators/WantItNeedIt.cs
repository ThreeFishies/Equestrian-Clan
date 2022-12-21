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
    public static class WantItNeedIt
    {
        public static string ID = Ponies.GUID + "_WantItNeedIt";
        public static MutatorData mutatorData;

        public static void BuildAndRegister()
        {
            mutatorData = new MutatorDataBuilder
            {
                ID = ID,
                NameKey = "Pony_Mutator_WantItNeedIt_Name_Key",
                DescriptionKey = "Pony_Mutator_WantItNeedIt_Description_Key",
                RelicActivatedKey = "Pony_Mutator_WantItNeedIt_Activated_Key",
                RelicLoreTooltipKeys = new List<string>()
                {
                    "Pony_Mutator_WantItNeedIt_Lore_Key"
                },
                DisableInDailyChallenges = false,
                DivineVariant = false,
                BoonValue = 9,
                RequiredDLC = DLC.None,
                IconPath = "Mutators/Sprite/MTR_WantItNeedIt.png",
                Tags = new List<string>
                {
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
                    ParamCardUpgradeData = new CardUpgradeDataBuilder
                    {
                        UpgradeTitle = "WantItNeedIt_Mutator_Effect_Holdover",
                        BonusDamage = 0,
                        BonusHeal = 0,
                        TraitDataUpgradeBuilders = new List<CardTraitDataBuilder>
                        {
                            new CardTraitDataBuilder
                            {
                                TraitStateName = "CardTraitRetain",
                            },
                        },
                        Filters = new List<CardUpgradeMaskData>
                        {
                            new CardUpgradeMaskDataBuilderFixed
                            {
                                CardType = CardType.Spell,
                                ExcludedCardTraits = new List<String>
                                {
                                    "CardTraitRetain",
                                    //"CardTraitExhaustState",
                                    //"CardTraitSelfPurge"
                                }
                            }.Build(),
                        },
                    }.Build(),
                    AdditionalTooltips = new AdditionalTooltipData[]
                    {
                        new AdditionalTooltipData
                        {
                            titleKey = "CardTraitRetain_CardText",
                            descriptionKey = "CardTraitRetain_TooltipText",
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
