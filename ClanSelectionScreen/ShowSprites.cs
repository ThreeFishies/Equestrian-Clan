using System;
using System.IO;
using HarmonyLib;
using ShinyShoe;
using UnityEngine;
using BepInEx;
using Equestrian.Init;
using Trainworks.Managers;
using Equestrian.Champions;

namespace Equestrian.Sprites
{
	[HarmonyPatch(typeof(ClassSelectionScreen), "RefreshCharacters")]
	public static class SeeMeSomePonies
	{
		private static GameObject mareALee = new GameObject();
		private static GameObject tantabus = new GameObject();
		private static GameObject subClan = new GameObject();
		private static bool isSetup = false;
		private static void Postfix(ref ClassSelectionUI ___mainClassSelectionUI, ref ClassSelectionIconUI ___subClassSelectionUI, ref ChampionData ____currentlySelectedChampion) 
		{
			//Ponies.Log("Updating selected characters:");
            if (!isSetup) 
			{ 
				SetupSprites();
			}

			//Ponies.Log("Mare a Lee Instance ID: " + mareALee.GetInstanceID().ToString());

			if (mareALee.GetInstanceID() == 0) 
			{
				//Ponies.Log("Attempting to refresh clan sprites.");

				mareALee = null;
				tantabus = null;
				subClan = null;

				mareALee = new GameObject();
				tantabus = new GameObject();
				subClan = new GameObject();

				SetupSprites();
			}

			//Ponies.Log("Mare a Lee Instance ID: " + mareALee.GetInstanceID().ToString());

			if (___mainClassSelectionUI.currentClass.isRandom) 
			{
				//Ponies.Log("Random main clan selected.");
				mareALee.SetActive(false);
				tantabus.SetActive(false);
			}
			else if (____currentlySelectedChampion.championCardData.GetID() == CustomCardManager.GetCardDataByID(MareaLee.ID).GetID()) 
			{
				//Ponies.Log("Champion Mare a Lee selected.");
				mareALee.SetActive(true);
				tantabus.SetActive(false);
			}
			else if (____currentlySelectedChampion.championCardData.GetID() == CustomCardManager.GetCardDataByID(Tantabus.ID).GetID())
			{
				//Ponies.Log("Champion Tantabus selected.");
				mareALee.SetActive(false);
				tantabus.SetActive(true);
			}
			else 
			{
				//Ponies.Log("Non-Equestrian champion selected.");
				mareALee.SetActive(false);
				tantabus.SetActive(false);
			}

			if (___subClassSelectionUI.currentClass.isRandom) 
			{
				//Ponies.Log("Random sub-class selected.");
				subClan.SetActive(false);
			}
			else if (___subClassSelectionUI.currentClass.classData.GetID() == CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID()) 
			{
				//Ponies.Log("Equestrian sub-class selected.");
				subClan.SetActive(true);
			}
			else 
			{
				//Ponies.Log("Non-Equestrian sub-class selected.");
				subClan.SetActive(false);
			}
		}

		private static void SetupSprites() 
		{
			SpriteRenderer oohLaLa = mareALee.AddComponent<SpriteRenderer>();
			oohLaLa.sprite = Mod_Sprites_Setup.LoadNewSprite(Path.Combine(Path.GetDirectoryName(Ponies.Instance.Info.Location), "ClanAssets"), "MareALeeSprite.png");
			oohLaLa.sortingLayerID = 0;
			oohLaLa.transform.SetPosition(-7.0f, -5.0f, 0.0f);
			SpriteRenderer wakkaWakka = tantabus.AddComponent<SpriteRenderer>();
			wakkaWakka.sprite = Mod_Sprites_Setup.LoadNewSprite(Path.Combine(Path.GetDirectoryName(Ponies.Instance.Info.Location), "ClanAssets"), "TantabusSprite.png");
			wakkaWakka.sortingLayerID = 0;
			wakkaWakka.transform.SetPosition(-9.0f, -5.0f, 0.0f);
			SpriteRenderer oogaOoga = subClan.AddComponent<SpriteRenderer>();
			oogaOoga.sprite = Mod_Sprites_Setup.LoadNewSprite(Path.Combine(Path.GetDirectoryName(Ponies.Instance.Info.Location), "ClanAssets"), "EquestrianClanSprite.png");
			oogaOoga.sortingLayerID = 0;
			oogaOoga.transform.SetPosition(-12.0f,-2.0f,0.0f);

			mareALee.SetActive(false);
			tantabus.SetActive(false);
			subClan.SetActive(false);

			isSetup = true;
		}
	}
}