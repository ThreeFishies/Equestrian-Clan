using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Equestrian.Init;
using Trainworks.Managers;
using Equestrian.SpellCards;
using Equestrian.MonsterCards;
using ShinyShoe.Loading;

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
			//Ponies.Log($"Icon Removal Service: title={title}, text={text}" );
		}
	}
}