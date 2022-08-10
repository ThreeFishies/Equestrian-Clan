using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using HarmonyLib;
using ShinyShoe;
using ShinyShoe.Audio;
using ShinyShoe.Loading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Trainworks.Managers;
using Trainworks.Constants;
using Equestrian.MonsterCards;
using Equestrian.HarmonyPatches;
using Equestrian.Init;
using Equestrian.CardPools;
using Equestrian.Enhancers;
using Equestrian.Metagame;

namespace Equestrian.Init
{
    [HarmonyPatch(typeof(LoadingScreen), "FadeOutFullScreen")]
    public static class PonyStart
    {
        public static void Prefix()
        {
            if (Ponies.EquestrianClanIsInit) { return; }

            //if (style == LoadingScreen.DisplayStyle.FullScreen)
            //{
            Ponies.Instance.InitializeHereInstead();
            //}
        }
    }

    [HarmonyPatch(typeof(LoadingScreen), "Initialize")]
    public static class PonyPreStart
    {
        public static void Postfix()
        {
            PonyLoader.ShowImage();
        }
    }
}