using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Equestrian.Init;
using Trainworks.Managers;
using Equestrian.SpellCards;
using Equestrian.MonsterCards;
using ShinyShoe.Loading;

//This allows Mare You Know to reduce the cost of cards drawn on resolve trigger without effecting your entire hand.
//The player action phase check ensures that it won't break cards played during your turn, such as the Awoken RailSpike.
namespace Equestrian.HarmonyPatches
{
	[HarmonyPatch(typeof(CardManager), "DrawCards")]
	public static class RemoveTempUpgradeToCardsAfterDrawingCards
	{
		private static void Postfix(ref List<CardUpgradeState> ___nextDrawnTempCardUpgrades)
		{
			if (!Trainworks.Managers.ProviderManager.CombatManager.IsPlayerActionPhase()) 
			{
				___nextDrawnTempCardUpgrades.Clear();
			}
		}
	}
} 
