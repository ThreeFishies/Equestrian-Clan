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

namespace Equestrian.Relic
{
    class AChildsDrawing
    {
        public static readonly string ID = Ponies.GUID + "_AChildsDrawingRelic";

        public static void BuildAndRegister()
        {
            new CollectableRelicDataBuilder
            {
                CollectableRelicID = ID,
                NameKey = "Pony_Relic_AChildsDrawing_Name_Key",
                DescriptionKey = "Pony_Relic_AChildsDrawing_Description_Key",
                RelicPoolIDs = new List<string> { VanillaRelicPoolIDs.MegaRelicPool },
                IconPath = "RelicAssets/AChildsDrawing.png",
                ClanID = CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID(),

                EffectBuilders = new List<RelicEffectDataBuilder>
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassType = typeof(CustomRelicEffectEnergyAndCardDrawOnUnitSpawned), 
                        ParamSourceTeam = Team.Type.Monsters,
                        ParamInt = 4, //Energy Gained

                                        
                        ParamFloat = 0.0f,
                        ParamUseIntRange = false,
                        ParamMinInt = 0,
                        ParamMaxInt = 0,
                        ParamString = "",
                        ParamCharacterSubtype = "SubtypesData_None",
                        ParamExcludeCharacterSubtypes = new string[]{ },
                        ParamSpecialCharacterType = 0,
                        ParamBool = false,
                        ParamStatusEffects = new StatusEffectStackData[]{},
                        ParamCardEffects = new List<CardEffectData> { },
                        ParamTrigger = CharacterTriggerData.Trigger.OnDeath,
                        ParamTargetMode = TargetMode.Room,
                        ParamCardType = CardType.Spell,

                        EffectConditionBuilders = new List<RelicEffectConditionBuilder>
                        {
                            new RelicEffectConditionBuilder
                            {
                                paramCardType = CardStatistics.CardTypeTarget.Monster,
                                allowMultipleTriggersPerDuration = false,
                                paramInt = 1,
                                paramTrackTriggerCount = true,
                                paramTrackedValue = CardStatistics.TrackedValueType.MonsterSubtypePlayed,
                                paramEntryDuration = CardStatistics.EntryDuration.ThisBattle,
                                paramSubtype = "SubtypesData_None",
                                paramComparator = RelicEffectCondition.Comparator.Equal,
                            }
                        },
                    }
                },
                FromStoryEvent = false,
                IsBossGivenRelic = false,
                LinkedClass = Ponies.EquestrianClanData,
                Rarity = CollectableRarity.Common,
                RelicLoreTooltipKeys = new List<string>
                {
                    "Pony_Relic_AChildsDrawing_Lore_Key"
                },
                RelicActivatedKey = "Pony_Relic_AChildsDrawing_Activated_Key",
                UnlockLevel = 3,
            }.BuildAndRegister();
        }
    }
}