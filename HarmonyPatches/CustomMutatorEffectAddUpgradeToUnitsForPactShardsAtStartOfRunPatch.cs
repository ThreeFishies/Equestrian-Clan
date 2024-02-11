using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Equestrian.Init;
using Trainworks.Managers;
using Equestrian.SpellCards;
using Equestrian.MonsterCards;
using Equestrian.Mutators;
using ShinyShoe.Loading;

namespace Equestrian.HarmonyPatches
{
	[HarmonyPatch(typeof(MapScreen), "Initialize")]
	public static class CustomMutatorEffectAddUpgradeToUnitsForPactShardsAtStartOfRunPatch
	{
		public static void Postfix() 
		{
			if (!Ponies.EquestrianClanIsInit) { return; }

			WorthIt.Activate();
		}
	}
}