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
using CustomEffects;

namespace Equestrian.Unused
{
    class TheOldSeventhElement
    {
        public static readonly string ID = Ponies.GUID + "_TheSeventhElementRelic";

        public static void BuildAndRegister()
        {
            new CollectableRelicDataBuilder
            {
                CollectableRelicID = ID,
                NameKey = "Pony_Relic_TheSeventhElement_Name_Key",
                DescriptionKey = "Pony_Relic_TheSeventhElement_Description_Key",
                RelicPoolIDs = new List<string> { VanillaRelicPoolIDs.MegaRelicPool },
                IconPath = "RelicAssets/TheSeventhElement.png",
                ClanID = CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID(),
                EffectBuilders = new List<RelicEffectDataBuilder>
                {
                    new RelicEffectDataBuilder
                    { 
                        //Based off of Penitent Remains
                        RelicEffectClassType = typeof(RelicEffectModifyCharacterAttackDamage),
                        ParamSourceTeam = Team.Type.Monsters,
                        ParamStatusEffects = new StatusEffectStackData[]{ },
                        ParamInt = 1, //Changed from 2
                        ParamUseIntRange = false,
                        ParamMinInt = 0,
                        ParamMaxInt = 0,
                        ParamString = "",
                        ParamCharacterSubtype = "SubtypesData_None",
                        ParamExcludeCharacterSubtypes = new string[]{ },
                        ParamSpecialCharacterType = 0,
                        ParamBool = true, //Changing this to false means the buffs are removed on death. We don't want that.
                        ParamCardEffects = new List<CardEffectData>{ },
                        ParamTrigger = CharacterTriggerData.Trigger.OnSpawn,
                        ParamTargetMode = TargetMode.FrontInRoom,
                        ParamCardType = CardType.Monster, //Changed from "Blight"
                        ParamCardUpgradeData = new CardUpgradeDataBuilder
                        { 
                            BonusDamage = 1,
                            UseUpgradeHighlightTextTags = true,
                            UpgradeTitle = "ScalingAttackPerUnit",

                        }.Build(),
                    }
                },
                FromStoryEvent = false,
                IsBossGivenRelic = false,
                LinkedClass = Ponies.EquestrianClanData,
                Rarity = CollectableRarity.Common,
                RelicLoreTooltipKeys = new List<string>
                {
                    "Pony_Relic_TheSeventhElement_Lore_Key"
                },
                RelicActivatedKey = "",
                //UnlockLevel = 6,
            }.BuildAndRegister();
        }
    }
}