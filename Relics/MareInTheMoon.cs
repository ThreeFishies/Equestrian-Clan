using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Trainworks.Builders;
using Trainworks.Managers;
using Trainworks.Enums;
using Trainworks.Constants;
using Equestrian.Init;
using Equestrian;
using Equestrian.CardPools;

namespace Equestrian.Relic
{
    class MareInTheMoon
    {
        public static readonly string ID = Ponies.GUID + "_MareInTheMoonRelic";

        public static void BuildAndRegister()
        {
            new CollectableRelicDataBuilder
            {
                CollectableRelicID = ID,
                //Name = "BottledCutieMark",
                NameKey = "Pony_Relic_MareInTheMoon_Name_Key",
                //Description = "Herd spells require two fewer friendly units to cast.",
                DescriptionKey = "Pony_Relic_MareInTheMoon_Description_Key",
                RelicPoolIDs = new List<string> { VanillaRelicPoolIDs.MegaRelicPool },
                IconPath = "RelicAssets/MareInTheMoon.png",
                ClanID = CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID(),
                EffectBuilders = new List<RelicEffectDataBuilder>
                {
                    new RelicEffectDataBuilder
                    { 
                        RelicEffectClassName = "RelicEffectAddTempUpgrade",
                        ParamSourceTeam = Team.Type.Monsters,
                        ParamCardUpgradeData = new CardUpgradeDataBuilder
                        { 
                            BonusDamage = 0,
                            BonusHP = 0,
                            TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
                            { 
                                new CharacterTriggerDataBuilder
                                {
                                    Trigger = CharacterTriggerData.Trigger.OnKill,
                                    DescriptionKey = "Pony_Relic_MareInTheMoon_Trigger_Key",
                                    EffectBuilders = new List<CardEffectDataBuilder>
                                    { 
                                        new CardEffectDataBuilder
                                        { 
                                            //EffectStateType = VanillaCardEffectTypes.CardEffectHealTrain,
                                            EffectStateName = "CardEffectHealTrain",
                                            TargetMode = TargetMode.Pyre,
                                            TargetTeamType = Team.Type.Monsters,
                                            ParamInt = 5,
                                        }
                                    }
                                }
                            },
                            Filters = new List<CardUpgradeMaskData>
                            {
                                new CardUpgradeMaskDataBuilderFixed
                                {
                                    CardType = CardType.Monster,
                                    RequiredSubtypes = new List<string>
                                    {
                                        "SubtypesData_Champion_83f21cbe-9d9b-4566-a2c3-ca559ab8ff34"
                                    }
                                }.Build(),
                            }
                        }.Build(),
                    }
                },
                FromStoryEvent = false,
                IsBossGivenRelic = false,
                LinkedClass = Ponies.EquestrianClanData,
                Rarity = CollectableRarity.Common,
                RelicLoreTooltipKeys = new List<string>
                {
                    "Pony_Relic_MareInTheMoon_Lore_Key"
                },
                UnlockLevel = 6,
            }.BuildAndRegister();
        }
    }
}