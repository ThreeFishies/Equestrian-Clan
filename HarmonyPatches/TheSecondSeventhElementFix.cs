using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Equestrian.Init;
using Trainworks.Managers;
using Equestrian.SpellCards;
using Equestrian.MonsterCards;
using Equestrian.Relic;
using Trainworks.Constants;
using Trainworks.Builders;
using Equestrian.CardPools;
using Equestrian;

namespace Equestrian.HarmonyPatches
{
    [HarmonyPatch(typeof(CharacterState), "GetTriggerFireCount")]
    public static class TheSecondSeventhElementFix
    {
        public static void Postfix(ref CharacterState __instance, ref int __result)
        {
            if (__instance.GetTeamType() != Team.Type.Monsters)
            {
                return;
            }
            if (ProviderManager.SaveManager.GetHasRelic(CustomCollectableRelicManager.GetRelicDataByID(TheSeventhElement.ID)))
            {
                if (__result > 1)
                {
                    bool isSocial = false;

                    if (__instance.GetStatusEffect("social") != null)
                    { 
                        isSocial = true;
                    }

                    if (!isSocial) 
                    { 
                        __result -= 1;
                    }
                }
            }
        }
    }
}