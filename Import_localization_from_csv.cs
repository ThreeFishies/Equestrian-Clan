/*
using System;
using HarmonyLib;
using I2.Loc;
using Trainworks.Managers;

namespace PonyLocalization
{
	internal class localization
	{
		[HarmonyPatch(typeof(LocalizationManager), "UpdateSources")]
		private class RegisterLocalizationStrings
		{
			private static void Postfix()
			{
				CustomLocalizationManager.ImportCSV("Localization/PoniesEverywhere.csv", ',');
			}
		}
	}
}
*/