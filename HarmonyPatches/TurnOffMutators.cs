using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Equestrian.Init;
using Trainworks.Managers;
using Equestrian.SpellCards;
using Equestrian.MonsterCards;
using ShinyShoe.Loading;
using Equestrian.Relic;
using ShinyShoe;

namespace Equestrian.HarmonyPatches
{
	//Unknown mutators appear as ugly white boxes. If the clan mod is turned off, reset these settings to avoid that.
	[HarmonyPatch(typeof(ModSettingsScreen), "HandleModToggled")]
	public static class TurnOffMutators
	{
		public static void Prefix(ref ModDefinition modDef, ref bool enabled)
		{ 
			if (!Ponies.EquestrianClanIsInit) { return; }

			if (!enabled && (modDef.ModName == "Equestrian Clan" || modDef.ModName == "Equestrian Dev"))
			{
				Ponies.Log("Clearing mutator selections.");

				MetagameSaveData metagameSaveData = ProviderManager.SaveManager.GetMetagameSave();

				metagameSaveData.lastSelectedMutatorIDs.Clear();
				metagameSaveData.SetSpChallengeId(null);

				if (metagameSaveData.GetActiveMasteryFrameType() == Ponies.PonyFrame.GetEnum()) 
				{
					Ponies.Log("Reset to default card mastery frame type.");
					metagameSaveData.SetActiveMasteryFrameType(MasteryFrameType.Default);
				}

				ProviderManager.SaveManager.StartSavingMetagame("metagameSave");
			}
		}
	}
}