using HarmonyLib;
using System;
using Trainworks.Managers;
using Equestrian.Init;

//So apparently all of this stuff is needed to fix unit merging at Divine Temples. Call FindUnitSynthesisMappingInstanceToStub() in your initialization setup after builing and registering your units to allow thier CardUpgradeDataBuilder data to be added to the game. Oh, and make sure your BepInEx logging is set up properly too, or this fix won't work.
namespace SynthesisFix
{
    [HarmonyPatch(typeof(UnitSynthesisMapping), "CollectMappingData", new Type[]
	{

	})]
	public class RecallingCollectMappingData
	{
		[HarmonyReversePatch(0)]
		public static void MyTest(object _instance)
		{
		}
	}

	internal class LiLyPatch
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00009CA8 File Offset: 0x00007EA8
		public static void FindUnitSynthesisMappingInstanceToStub()
		{
			AllGameData allGameData = ProviderManager.SaveManager.GetAllGameData();
			Ponies.Log("Got reference to AllGameData: " + allGameData.name);
			BalanceData balanceData = allGameData.GetBalanceData();
			Ponies.Log("Got reference to BalanceData: " + balanceData.name);
			UnitSynthesisMapping synthesisMapping = balanceData.SynthesisMapping;
			bool flag = synthesisMapping == null;
			if (flag)
			{
				Ponies.Log("Failed to create a mapping instance.");
			}
			else
			{
				Ponies.Log("Able to find mapping instance: " + synthesisMapping.GetID());
			}
			RecallingCollectMappingData.MyTest(synthesisMapping);
			Ponies.Log("Ran through full test of mapping synths.");
		}
	}
}
