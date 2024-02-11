using System;
using System.Collections.Generic;
using System.Collections;
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
using Equestrian.Champions;
using ShinyShoe.Loading;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using CustomEffects;

namespace Equestrian.HarmonyPatches
{
    //Add checks for null where appropriate.
    [HarmonyPatch(typeof(CharacterState), "CheckIfEnchanterSpawned")]
    public static class AddCustomCardEffectEnchantMareALeeCoachToListOfEnchanterEffects
    {
        public static void Postfix(ref CharacterState __instance)
        {
            if (!Ponies.EquestrianClanIsInit) { return; }
            if (ProviderManager.CombatManager == null) { return; }
            if (__instance == null) 
            {
                Ponies.Log("AddCustomCardEffectEnchantMareALeeCoachToListOfEnchanterEffects OOOOPS");
                return;
            }
            if (__instance.GetTriggers() == null)
            {
                Ponies.Log("AddCustomCardEffectEnchantMareALeeCoachToListOfEnchanterEffects OOOOPS #2");
                return;
            }

            foreach (CharacterTriggerState characterTriggerState in __instance.GetTriggers())
            {
                if (characterTriggerState == null || characterTriggerState.GetEffects() == null)
                {
                    Ponies.Log("AddCustomCardEffectEnchantMareALeeCoachToListOfEnchanterEffects OOOOPS #3");
                    continue;
                }

                foreach (CardEffectState effect in characterTriggerState.GetEffects())
                {
                    if (effect.GetCardEffect() is CustomCardEffectEnchantMareALeeCoach cardEffectEnchant)
                    {
                        AccessTools.Method(typeof(CharacterState), "set_IsEnchanter").Invoke(__instance, new object[] { true });
                        //Ponies.Log("Attempting to Enchant");
                        cardEffectEnchant.SetEnchanterCharacter(__instance, ProviderManager.CombatManager.GetSaveManager(), ProviderManager.CombatManager.GetMonsterManager(), ProviderManager.CombatManager.GetHeroManager());
                    }
                }
            }
        }
    }

    /*
    [HarmonyPatch(typeof(CardTooltipContainer), "AddTooltipsCardEffect", new Type[] { typeof(TooltipContainer), typeof(CardEffectData), typeof(SaveManager) })]
    public static class AddCustomCardEffectEnchantMareALeeCoachToListOfTypesSupportedByTooltips 
    {
        public static void Prefix(CardTooltipContainer __instance) 
        {
            if (!Ponies.EquestrianClanIsInit) { return; }

            TooltipsGetter tooltipsGetter;

            if (CardTooltipContainer.EffectsSupportedInTooltips.TryGetValue(typeof(CustomCardEffectEnchantMareALeeCoach), out _))
            {
                Ponies.Log("CustomCardEffectEnchantMareALeeCoach is alrready in list of types supported by tooltips.");
                return;
            }
            else 
            {
                tooltipsGetter = new TooltipsGetter(CustomCardEffectEnchantMareALeeCoach.GetTooltipsStatusList);
                CardTooltipContainer.EffectsSupportedInTooltips.Add(typeof(CustomCardEffectEnchantMareALeeCoach), tooltipsGetter);

                if (CardTooltipContainer.EffectsSupportedInTooltips.TryGetValue(typeof(CustomCardEffectEnchantMareALeeCoach), out TooltipsGetter _))
                {
                    Ponies.Log("Added CustomCardEffectEnchantMareALeeCoach to list of types supported by tooltips.");
                }
                else 
                {
                    Ponies.Log("Failed to add CustomCardEffectEnchantMareALeeCoach to list of types supported by tooltips.");
                }
            }
        }
    }*/
}