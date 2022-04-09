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

//This prevents the price of purchasing multiple purges or duplications from further increasing the cost.
//Requires the Torn Lapel Pin relic.
namespace Equestrian.HarmonyPatches
{
	[HarmonyPatch(typeof(MerchantGoodState), "ClaimReward")]
	public static class StopTrackingCosts
	{
		private static void Prefix(ref bool increment)
		{
			SaveManager saveManager = ProviderManager.SaveManager;
			if (saveManager != null) 
			{
				List<RelicState> relicDataList = saveManager.GetAllRelics();
				if (relicDataList.Count > 0)
				{
					foreach (RelicState state in relicDataList)
					{
						if (state.GetRelicDataID() == TornLapelPin.ID) 
						{
							increment = false;
						}
					}
				}
			}
		}
	}
}