/*
using HarmonyLib;
using Trainworks.Managers;

namespace EditAwokenHollow
{
    public class EditAwokenHollowStuff
	{
		public static void ChangeCultivate()
		{
			//unit
			CharacterData spawnCharacterData = ProviderManager.SaveManager.GetAllGameData().FindCardDataByName("AwokenHollow").GetSpawnCharacterData();
			Traverse.Create(spawnCharacterData.GetTriggers()[1].GetEffects()[0]).Field("targetMode").SetValue(TargetMode.Room);
			Traverse.Create(spawnCharacterData.GetTriggers()[1].GetEffects()[0]).Field("targetTeamType").SetValue(Team.Type.Monsters);

			//essence
			CardUpgradeData upgradeData = ProviderManager.SaveManager.GetBalanceData().SynthesisMapping.GetUpgradeData(spawnCharacterData);
			Traverse.Create(upgradeData.GetTriggerUpgrades()[0].GetEffects()[0]).Field("targetMode").SetValue(TargetMode.Room);
			Traverse.Create(upgradeData.GetTriggerUpgrades()[0].GetEffects()[0]).Field("targetTeamType").SetValue(Team.Type.Monsters);
		}
	}
}*/