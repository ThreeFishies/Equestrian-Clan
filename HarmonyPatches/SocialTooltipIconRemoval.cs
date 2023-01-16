using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Equestrian.Init;
using Trainworks.Managers;
using Equestrian.SpellCards;
using Equestrian.MonsterCards;
using ShinyShoe.Loading;
using UnityEngine.UI;
using CustomEffects;
using UnityEngine;
using TMPro;
using ExtendedTMPro;
using static System.Net.Mime.MediaTypeNames;
using I2.Loc;

//This prevents malformed icon text from cluttering up the "Social" status tooltips.
namespace Equestrian.HarmonyPatches
{
	[HarmonyPatch(typeof(TooltipUI), "FormatTitleWithIcon")]
	public static class IconRemovalService
	{
		private static void Prefix(ref string title, ref string icon)
		{
			string text = StatusEffectManager.GetLocalizedName("social", 1, false, true, false);
			if (title == text) { icon = ""; }
			text = StatusEffectManager.GetLocalizedName("male", 1, false, true, false);
			if (title == text) { icon = ""; }
			text = StatusEffectManager.GetLocalizedName("female", 1, false, true, false);
			if (title == text) { icon = ""; }
			text = StatusEffectManager.GetLocalizedName("genderless", 1, false, true, false);
			if (title == text) { icon = ""; }
			text = StatusEffectManager.GetLocalizedName("undefined", 1, false, true, false);
			if (title == text) { icon = ""; }
			//Ponies.Log($"Icon Removal Service: title={title}, text={text}" );
		}
	}

	/*
	//[HarmonyPatch(typeof(TooltipUI), "Set", new Type[] {typeof(string), typeof(string)})]
	public static class MakeIconsWorkHopefully 
	{
		public static bool isSetup = false;

		public static void LocalizeIcons(bool logAllAssets = false)
		{ 
			//if (!Ponies.EquestrianClanIsInit) { return; }

			if (!isSetup)
			{
                Ponies.Log("Attempting to add Icon for Social status effect.");
				//___titleText.GetLocalizeComponent().AddTranslatedObject(StatusEffectSocial.statusEffectData.GetIcon());
				LocalizationManager.Sources[0].AddAsset(StatusEffectSocial.statusEffectData.GetIcon());

				LocalizationManager.RegisterTarget(new LocalizeTargetDesc_Type<Sprite, LocalizeTarget_UnityUI_Sprite>
				{
				    Name = "Sprite",
				    Priority = 100
				});

                if (LocalizationManager.FindAsset(StatusEffectSocial.statusEffectData.GetIcon().name) == null) 
				{
					Ponies.Log("Unable to locate asset that was just added: " + StatusEffectSocial.statusEffectData.GetIcon().name);
				}
				else 
				{
					Ponies.Log("Added asset: " + StatusEffectSocial.statusEffectData.GetIcon().name);
				}

                isSetup = true;
            }

			if (logAllAssets) 
			{
				foreach (ILocalizeTargetDescriptor localizeTargetDescriptor in LocalizationManager.mLocalizeTargets)
				{
					Ponies.Log(localizeTargetDescriptor.Name);
					Ponies.Log(localizeTargetDescriptor.GetTargetType().ToString());
					Ponies.Log(string.Empty);
				}

                for (int ii = 0; ii < LocalizationManager.Sources.Count; ii++) 
				{
                    Ponies.Log("-------------------------------");
                    Ponies.Log("LanguageSourceData[" + ii + "]:");
					LanguageSourceData sourceData = LocalizationManager.Sources[ii];
					foreach (KeyValuePair<string, UnityEngine.Object> keyValuePair in sourceData.mAssetDictionary) 
					{
						Ponies.Log("Key: " + keyValuePair.Key);
						Ponies.Log("Object name: " + keyValuePair.Value.name);
						Ponies.Log("Object type: " + keyValuePair.Value.GetType());
						Ponies.Log(string.Empty);
					}
				}

                Ponies.Log("-------------------------------");
                Ponies.Log("****    Completed dump.    ****");
                Ponies.Log("-------------------------------");
            }

            /*
			Ponies.Log(tooltipId + " | " + StatusEffectSocial.statusEffectData.GetStatusId());

            if (tooltipId == StatusEffectSocial.statusEffectData.GetStatusId())
			{
				Ponies.Log("Attempting to add Icon for Social status effect.");
				//__result = StatusEffectSocial.statusEffectData.GetIcon();

				if (__result == null) 
				{ 
					Ponies.Log("TooltipUI is null. Whoops.");
					return;
				}

				TextMeshProUGUI text = AccessTools.Field(typeof(TooltipUI), "titleText").GetValue(__result) as ExtendedTextMeshProUGUI;

				if (text == null) 
				{ 
					Ponies.Log("Null title data field. Sad Face.");
					__result.Set("Test title", "Test body");

                    text = AccessTools.Field(typeof(TooltipUI), "titleText").GetValue(__result) as ExtendedTextMeshProUGUI;
                }

                if (text == null)
                {
                    Ponies.Log("Title data field is still null. Angry Face.");
					return;
                }

                text.SetText("Test");
				text.GetLocalizeComponent().AddTranslatedObject(StatusEffectSocial.statusEffectData.GetIcon());
            }
			*//*
        }
    }
	*/
}