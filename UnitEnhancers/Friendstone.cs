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
using HarmonyLib;

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
                EnhancerID = EnhancerID,
                IconPath = "ClanAssets/Friendstone.png",
                ClanID = EquestrianClan.ID,
                //LinkedClass = Ponies.EquestrianClanData,
                NameKey = "Pony_Enhancer_Friendstone_Name_Key",
                DescriptionKey = "Pony_Enhancer_Friendstone_Description_Key",
                EnhancerPoolIDs = { VanillaEnhancerPoolIDs.UnitUpgradePoolCommon }, //Adds this to the pool irregardless of clan. Needs a fix.
                Rarity = CollectableRarity.Common,
                CardType = CardType.Monster,

                Upgrade = new CardUpgradeDataBuilder
                {
                    UpgradeID = "Pony_Enhancer_Friendstone_Name_Key",
                    UpgradeTitleKey = "Pony_Enhancer_Friendstone_Name_Key",
                    UpgradeDescriptionKey = "Pony_Enhancer_Friendstone_Description_Key",
                    AssetPath = "ClanAssets/Friendstone.png",
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
                            TriggerID = "FriendStoneSocialUnitPlayedTrigger",

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

            //AccessTools.Field(typeof(GameData),"id").SetValue(FriendstoneData,EnhancerID);

            return FriendstoneData;
        }
    }
}