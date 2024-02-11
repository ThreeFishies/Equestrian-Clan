using System;
using System.Collections;
using System.Collections.Generic;
using Trainworks.Builders;
using System.Text;
using HarmonyLib;
using Trainworks.Managers;
using Trainworks.Constants;
using System.Linq;
using UnityEngine;
using Trainworks.Utilities;
using Equestrian.Init;
using Equestrian.MonsterCards;
using ShinyShoe.Loading;
using Equestrian.Relic;
using UnityEngine.Animations;
using System.Reflection;
using System.Globalization;
using System.Dynamic;

namespace CustomEffects
{
    // Token: 0x02000072 RID: 114
    public class CustomCardTraitDummy : CardTraitState
    {
        //public override string GetCardText() 
        //{
        //    return "CustomCardTraitDummy";
        //}
    }

    public class CustomCardTraitHerb : CardTraitState
    {
        public override string GetCardText()
        {
            return "CardTraitHerb_CardText".Localize();
        }

        public override string GetCardTooltipText()
        {
            return "CardTraitHerb_TooltipText".Localize();
        }

        public override string GetCardTooltipId()
        {
            return "CardTraitHerb";
        }

        public override string GetCardTooltipTitle()
        {
            return "CardTraitHerb_TooltipTitle".Localize();
        }

        public static void RegisterTooltip()
        {
            if (!TooltipContainer.TraitSupportedInTooltips("CardTraitHerb"))
            {
                TooltipContainer tooltipContainer = new TooltipContainer();
                List<String> supportedTraits = AccessTools.Field(typeof(TooltipContainer), "TraitsSupportedInTooltips").GetValue(tooltipContainer) as List<string>;
                supportedTraits.Add(typeof(CustomCardTraitHerb).Name);
                AccessTools.Field(typeof(TooltipContainer), "TraitsSupportedInTooltips").SetValue(tooltipContainer, supportedTraits);
            }

            if (!TooltipContainer.TraitSupportedInTooltips("CardTraitHerb"))
            {
                //Ponies.Log("Attempt to register CardTraitHerb failed.");
            }
        }
    }
}