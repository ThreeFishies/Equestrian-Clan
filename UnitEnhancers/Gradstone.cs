using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Equestrian.Init;
using Trainworks.BuildersV2;
using Trainworks.Constants;
using Trainworks.Enums;
using Equestrian;
using CustomEffects;
using UnityEngine;

namespace Equestrian.Enhancers
{
    class Gradstone
    {
        public static readonly string EnhancerID = Ponies.GUID + "_Gradstone";
        public static EnhancerData GradstoneData = ScriptableObject.CreateInstance<EnhancerData>();

        public static void BuildAndRegister()
        {
            GradstoneData = new EnhancerDataBuilder
            {
                EnhancerID = EnhancerID,
                IconPath = "ClanAssets/Gradstone.png",
                ClanID = EquestrianClan.ID,
                //LinkedClass = Ponies.EquestrianClanData,
                NameKey = "Pony_Enhancer_Gradstone_Name_Key",
                DescriptionKey = "Pony_Enhancer_Gradstone_Description_Key",
                EnhancerPoolIDs = {  },
                Rarity = CollectableRarity.Common,
                CardType = CardType.Monster,

                Upgrade = new CardUpgradeDataBuilder
                {
                    UpgradeID = "Pony_Enhancer_Gradstone_Name_Key",
                    UpgradeTitleKey = "Pony_Enhancer_Gradstone_Name_Key",
                    UpgradeDescriptionKey = "Pony_Enhancer_Gradstone_Description_Key",
                    AssetPath = "ClanAssets/Gradstone.png",
                    BonusHP = 10,
                    BonusDamage = 10,
                    CostReduction = 99,
                    HideUpgradeIconOnCard = false,

                    StatusEffectUpgrades = new List<StatusEffectStackData>
                    {
                        new StatusEffectStackData()
                        {
                           statusId = "social",
                           count = 1
                        }
                    },
                    
                    TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
                    {
                        new CharacterTriggerDataBuilder
                        {
                            TriggerID = "GradstoneSocialTrigger",
                            Trigger = CharacterTriggerData.Trigger.OnUnscaledSpawn,
                            HideTriggerTooltip = true,
                            EffectBuilders = new List<CardEffectDataBuilder>
                            {
                                new CardEffectDataBuilder
                                { 
                                    EffectStateType = typeof(CustomEffectSocial),
                                    TargetMode = TargetMode.Self,
                                }
                            }
                        }
                    }
                }.Build()
            }.BuildAndRegister();
        }
    }
}