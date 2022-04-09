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
    class TinyMouseCrutches
    {
        public static readonly string ID = Ponies.GUID + "_TinyMouseCrutchesRelic";

        public static void BuildAndRegister()
        {
            new CollectableRelicDataBuilder
            {
                CollectableRelicID = ID,
                NameKey = "Pony_Relic_TinyMouseCrutches_Name_Key",
                DescriptionKey = "Pony_Relic_TinyMouseCrutches_Description_Key",
                RelicPoolIDs = new List<string> { VanillaRelicPoolIDs.MegaRelicPool },
                IconPath = "RelicAssets/TinyMouseCrutches.png",
                ClanID = CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID(),
                EffectBuilders = new List<RelicEffectDataBuilder>
                {
                    new RelicEffectDataBuilder
                    { 
                        RelicEffectClassType = typeof(RelicEffectAddStatusEffectOnOtherStatusRemoved),
                        ParamSourceTeam = Team.Type.Monsters,
                        ParamString = VanillaStatusEffectIDs.Regen,
                        ParamStatusEffects = new StatusEffectStackData[]
                        {
                            new StatusEffectStackData
                            {
                                statusId = VanillaStatusEffectIDs.Armor,
                                count = 5
                            }
                        }
                    }
                },
                FromStoryEvent = false,
                IsBossGivenRelic = false,
                LinkedClass = Ponies.EquestrianClanData,
                Rarity = CollectableRarity.Common,
                RelicLoreTooltipKeys = new List<string>
                {
                    "Pony_Relic_TinyMouseCrutches_Lore_Key"
                },
                //UnlockLevel = 6,
            }.BuildAndRegister();
        }
    }
}