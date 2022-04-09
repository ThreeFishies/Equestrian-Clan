using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Equestrian.Init;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Enums;
using Equestrian;

namespace Equestrian.SpellCards
{
    class TimeToShine
    {
        public static readonly string ID = Ponies.GUID + "_TimeToShine";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                //Name = "Time To Shine",
                NameKey = "Pony_Spell_TimeToShine_Name_Key",
                //Description = "Pull tagert unit to the front and apply <nobr>+10<sprite name=\"Attack\"></nobr>.",
                OverrideDescriptionKey = "Pony_Spell_TimeToShine_Description_Key",
                Cost = 1,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = false,
                ClanID = EquestrianClan.ID,
                AssetPath = "SpellAssets/timetoshine.png",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool },
                CardLoreTooltipKeys = new List<string> 
                {
                    "Pony_Spell_TimeToShine_Lore_Key"
                },
                LinkedClass = Ponies.EquestrianClanData,

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        //While the type "CardEffectBuffDamage" is simpler and easier to use, it creates some oddities such as missing the status upgrade tooltip and being unable to target collectors.

                        //EffectStateType = VanillaCardEffectTypes.CardEffectBuffDamage,
                        EffectStateName = "CardEffectAddTempCardUpgradeToUnits",
                        TargetMode = TargetMode.DropTargetCharacter,
                        TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
                        ParamInt = 10,
                        ParamCardUpgradeData = new CardUpgradeDataBuilder 
                        {
                            BonusDamage = 10,
                            BonusHP = 0,
                            //UpgradeDescription = "TimeToShine_Desc",
                            UpgradeTitle = "TimeToShine_Title",
                            UseUpgradeHighlightTextTags = true,
                            //UpgradeIconPath = null,
                        }.Build(),
                    },

                    new CardEffectDataBuilder
                    { 
                        EffectStateType = VanillaCardEffectTypes.CardEffectFloorRearrange,
                        //EffectStateName = "CardEffectFloorRearrange",
                        TargetMode = TargetMode.DropTargetCharacter,
                        TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
                        ParamInt = 0
                    }
                }
            }.BuildAndRegister();
        }
    }
}