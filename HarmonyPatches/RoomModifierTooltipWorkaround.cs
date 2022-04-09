using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Equestrian.Init;
using Trainworks.Managers;
using Equestrian.SpellCards;
using Equestrian.MonsterCards;
using ShinyShoe.Loading;

//Fixed erroes by defining data and removing nulls.
//No need for fixes if it already works.
//This was helpful for degubbing, though.

/*
namespace MonsterTrainTestMod.HarmonyPatches
{
	[HarmonyPatch(typeof(CardTooltipContainer), "AddRoomModifierDataTooltips")]
	public static class CheckForNull
	{
		private static bool Prefix(List<RoomModifierData> roomModifiersData)
		{
			if (roomModifiersData == null)
			{
				return false;
			}
			else if (roomModifiersData.Count == 0) 
			{
				return false;
			}

			Ponies.Log("Room Modifier data exists, looking for problem.");
			Ponies.Log("Description key: " + roomModifiersData[0].GetDescriptionKey());
			//Ponies.Log("Description keyinplay: " + roomModifiersData[0].GetDescriptionKeyInPlay());
			foreach (StatusEffectStackData statusEffectStackData in roomModifiersData[0].GetParamStatusEffects())
			{
				Ponies.Log("StatusEffect Type: " + statusEffectStackData.GetType());
			}
			Ponies.Log("ExtraTooltipBodyKey: " + roomModifiersData[0].GetExtraTooltipBodyKey());
			Ponies.Log("RoomStateMdifierClassName: " + roomModifiersData[0].GetRoomStateModifierClassName());
			if (CardTooltipContainer.RoomModsWithStatusEffectTooltip.ContainsKey(roomModifiersData[0].GetRoomStateModifierClassName()))
				Ponies.Log("StatusID: " + CardTooltipContainer.RoomModsWithStatusEffectTooltip[roomModifiersData[0].GetRoomStateModifierClassName()]);
			Ponies.Log("End Room Modifier Data dump.");

			return true;
		}
	}
}
*/