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
    class Playstone
    {
        public static readonly string EnhancerID = Ponies.GUID + "_Playstone";
        public static EnhancerData PlaystoneData = ScriptableObject.CreateInstance<EnhancerData>();

        public static void BuildAndRegister()
        {
            PlaystoneData = new EnhancerDataBuilder
            {
                EnhancerID = EnhancerID,
                IconPath = "ClanAssets/Playstone.png",
                ClanID = EquestrianClan.ID,
                //LinkedClass = Ponies.EquestrianClanData,
                NameKey = "Pony_Enhancer_Playstone_Name_Key",
                DescriptionKey = "Pony_Enhancer_Playstone_Description_Key",
                EnhancerPoolIDs = {  },
                Rarity = CollectableRarity.Starter,
                CardType = CardType.Monster,

                Upgrade = new CardUpgradeDataBuilder
                {
                    UpgradeID = "Pony_Enhancer_Playstone_Name_Key",
                    UpgradeTitleKey = "Pony_Enhancer_Playstone_Name_Key",
                    UpgradeDescriptionKey = "Pony_Enhancer_Playstone_Description_Key",
                    AssetPath = "ClanAssets/Playstone.png",
                    BonusHP = 0,
                    BonusDamage = 0,
                    HideUpgradeIconOnCard = false,
                    CostReduction = 2,
                    XCostReduction = 2
                }.Build()
            }.BuildAndRegister();
        }
    }
}