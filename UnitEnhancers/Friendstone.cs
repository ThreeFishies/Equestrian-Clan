using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Equestrian.Init;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Enums;
using Equestrian;
using CustomEffects;
using UnityEngine;

namespace Equestrian.Enhancers
{
    class Friendstone
    {
        public static readonly string EnhancerID = Ponies.GUID + "_Friendstone";
        public static EnhancerData FriendstoneData = ScriptableObject.CreateInstance<EnhancerData>();

        public static EnhancerData BuildAndRegister()
        {
            FriendstoneData = new EnhancerDataBuilder
            {
                ID = EnhancerID,
                AssetPath = "ClanAssets/Friendstone.png",
                ClanID = EquestrianClan.ID,
                LinkedClass = Ponies.EquestrianClanData,
                NameKey = "Pony_Enhancer_Friendstone_Name_Key",
                DescriptionKey = "Pony_Enhancer_Friendstone_Description_Key",
                EnhancerPoolIDs = { VanillaEnhancerPoolIDs.UnitUpgradePoolCommon }, //Adds this to the pool irregardless of clan. Needs a fix.
                Rarity = CollectableRarity.Common,
                CardType = CardType.Monster,

                Upgrade = new CardUpgradeDataBuilder
                {
                    UpgradeTitleKey = "Pony_Enhancer_Friendstone_Name_Key",
                    UpgradeDescriptionKey = "Pony_Enhancer_Friendstone_Description_Key",
                    UpgradeIconPath = "ClanAssets/Friendstone.png",
                    BonusHP = 5,
                    BonusDamage = 5,
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
                            Trigger = CharacterTriggerData.Trigger.OnUnscaledSpawn,
                            HideTriggerTooltip = true,
                            EffectBuilders = new List<CardEffectDataBuilder>
                            { 
                                new CardEffectDataBuilder
                                { 
                                    EffectStateName = typeof(CustomEffectSocial).AssemblyQualifiedName,
                                    TargetMode = TargetMode.Self,
                                }
                            }
                        }
                    }
                }
            }.BuildAndRegister();

            return FriendstoneData;
        }
    }
}