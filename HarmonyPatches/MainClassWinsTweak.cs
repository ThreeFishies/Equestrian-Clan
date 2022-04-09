using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Equestrian.Init;
using Trainworks.Managers;
using Equestrian.SpellCards;
using Equestrian.MonsterCards;
using ShinyShoe.Loading;
using Equestrian;

//Certain story events require a number of main class wins to activate.
//Set the floor to five to activate them all.
namespace Equestrian.HarmonyPatches
{
	/*
	[HarmonyPatch(typeof(MetagameSaveData), "GetMainClassWins")]
	public static class FloorEquestrianClassWins
	{
		private static void Postfix(ref int __result, ref string classId)
		{
			Ponies.Log("Main class wins for clanID " + classId + " : " + __result);

			if(classId == null) { return; }

			if(classId == CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID()) 
			{ 
				if (__result < 5) 
				{
					__result = 5;
					Ponies.Log("Setting wins to the minimum number for " + classId + " : " + __result);
				}
			}
		}
	}*/

	[HarmonyPatch(typeof(SaveManager), "IsStoryEventValid")]
	public class SeeStoryRun 
	{
		private static void Prefix(ref SaveManager __instance,ref StoryEventData storyEventData, ref int runsStartedModifier) 
		{
			string ponyClassID = CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID();

			if (__instance.GetMainClass().GetID() == ponyClassID) 
			{
				if (__instance.GetClassLevel(ponyClassID) > 9)
				{
					//Ponies.Log("Runs for Equestrian: " + __instance.GetTrackedValue(MetagameSaveData.TrackedValue.StartedRuns));

					runsStartedModifier += 10;

					//Ponies.Log("Tweaking started runs by 10 for Equestrian clan.");
					//Ponies.Log("Story event name: " + storyEventData.name);
				}
			}
		}

		/*
		public static void Postfix(ref bool __result, ref StoryEventData storyEventData) 
		{
			//This creates a list of the cavern events you'll encounter on your journey. Please note that the second event in the list is never used.
			if (__result)
			{
				Ponies.Log($"Story: {storyEventData.name}");
			}
		}
		*/
	}
}
