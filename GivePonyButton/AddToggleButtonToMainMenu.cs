using System;
using ShinyShoe;
using UnityEngine;
using Equestrian.Metagame;
using Equestrian.Init;
using HarmonyLib;

namespace GivePony
{
    [HarmonyPatch(typeof(MainMenuScreen), "Initialize")]
    public static class AddGivePonyToggleButtonToMainMenuScreen 
    { 
        public static void Postfix(ref MainMenuScreen __instance) 
        { 
            if (!Ponies.EquestrianClanIsInit) { return; }

            GivePonyToggle.Create(__instance);

            //Ponies.givePonyToggle.Initialize();
            //Ponies.givePonyToggle.SetInteractable(true);
            //Ponies.givePonyToggle.SetVisible(true);
        }
    }
}