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

    public static class PonyLoader 
    {
        public static Sprite cursorIMG;
        public static Sprite backgroundIMG;
        //public static GameObject gameObject1;
        public static Image image;
        public static ScreenManager screenManager;

        public static void SetupSprites() 
        { 
            if (cursorIMG == null || cursorIMG.GetInstanceID() == 0) 
            {
                //if (gameObject1 != null) 
                //{
                //    UnityEngine.GameObject.Destroy(gameObject1);
                //}
                
                //gameObject1 = new GameObject();
                //gameObject1.name = "Background Loading Pony";

                if (cursorIMG != null) 
                {
                    GameObject.Destroy(cursorIMG);
                }

                string localPath = Path.GetDirectoryName(Ponies.Instance.Info.Location);
                string fullPath = Path.Combine(localPath, "ClanAssets/Loading.png");

                cursorIMG = CustomAssetManager.LoadSpriteFromPath(fullPath);

                fullPath = Path.Combine(localPath, "ClanAssets/BackgroundLoadingImage.png");

                backgroundIMG = CustomAssetManager.LoadSpriteFromPath(fullPath);

                //SpriteRenderer boohoohoo = gameObject1.AddComponent<SpriteRenderer>();
                //boohoohoo.sprite = backgroundIMG;
                //boohoohoo.sortingLayerID = 0;
                //boohoohoo.name = "Background Loading Pony Renderer";
                
            }
        }

        public static void ChangeCursor() 
        {
            SetupSprites();
            Cursor.SetCursor(cursorIMG.texture, Vector2.zero, CursorMode.Auto);
        }

        public static void ShowImage() 
        {
            if (!ProviderManager.TryGetProvider<ScreenManager>(out screenManager)) 
            {
                Ponies.Log("Error loading Asset Loading Manager");
                return; 
            }

            LoadingScreen loading = screenManager.GetScreen(ScreenName.Loading) as LoadingScreen;

            //GameObject screeen = AccessTools.Field(typeof(LoadingScreen),"screenArt").GetValue(loading) as GameObject;
            Image faaader = AccessTools.Field(typeof(LoadingScreen), "fader").GetValue(loading) as Image;

            SetupSprites();

            //Component[] components = screeen.GetComponentsInChildren<Component>();
            //foreach (Component component in components) 
            //{
            //    Ponies.Log(component.name);
            //}

            image = GameObject.Instantiate(faaader, faaader.transform);

            image.name = "testPony";
            image.sprite = backgroundIMG;
            image.color = Color.white;
            image.isMaskingGraphic = false;
            image.raycastTarget = false;
            image.type = Image.Type.Simple;

            image.gameObject.SetActive(true);

            //Ponies.Log(DepInjector.ListProviders());
            //Output: 14 [StoryManager (InkWriter)] [SteamClientHades (ShinyShoe.SteamClientHades)] [SaveManager (SaveManager)] [ScreenManager (ScreenManager)] [SoundManager (SoundManager)] [InputManager (InputManager)] [LanguageManager (LanguageManager)] [GameVFXManager (ShinyShoe.GameVFXManager)] [StoryManager (StoryManager)] [StatusEffectManager (StatusEffectManager)] [CardStatistics (CardStatistics)] [RelicManager (RelicManager)] [PlayerManager (PlayerManager)] [AssetLoadingManager (AssetLoadingManager)]
            
            //if (gameObject1 != null)
            //{
            //    Ponies.Log("Attempting to show a background loading image");
            //    gameObject1.SetActive(true);
            //}
            //else 
            //{
            //    Ponies.Log("An error occured when attempting to display a background loading image.");
            //}
            
        }

        public static void HideImage() 
        {
            //return;

            image.gameObject.SetActive(false);
            GameObject.Destroy(image);

            //gameObject1.SetActive(false);
            //UnityEngine.Object.Destroy(gameObject1);

            //loadingManager.gameObject.SetActive(true);

            if (ProviderManager.TryGetProvider<InputManager>(out InputManager inputManager)) 
            {
                inputManager.ApplyCursorStyle(InputManager.CursorStyle.Default);
            }
        }
    }
}