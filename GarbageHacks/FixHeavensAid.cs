using HarmonyLib;
using Trainworks.Managers;

namespace Equestrian.HarmonyPatches
{
    public class FixHeavensAid
	{
		public static void MakeItSo()
		{
			//DelayedUnitUpgrade_Heal
			RewardData lameHeavensAid = ProviderManager.SaveManager.GetAllGameData().FindRewardData("75a0b506-e397-4b48-bb22-0ed0be66575d");
			//Ponies.Log("Reward: " + lameHeavensAid.name);
			CardUpgradeData upgradeData = (CardUpgradeData)Traverse.Create(lameHeavensAid).Field("cardUpgradeData").GetValue();
			//Ponies.Log("Enhancer Upgrade: " + upgradeData.name);
			CardUpgradeData moreUpgradeData = upgradeData.GetTriggerUpgrades()[0].GetEffects()[0].GetParamCardUpgradeData();
			//Ponies.Log("Enhancer Upgrade Upgrade: " + moreUpgradeData.name);
			Traverse.Create(moreUpgradeData).Field("isUnitSynthesisUpgrade").SetValue(true);

			//DelayedUnitUpgradeFinal_Heal
			RewardData betterHeavensAid = ProviderManager.SaveManager.GetAllGameData().FindRewardData("060f5e88-b88f-4c52-8288-f9084a3d9cba");
			//Ponies.Log("Reward: " + betterHeavensAid.name);
			CardUpgradeData upgradeData2 = (CardUpgradeData)Traverse.Create(betterHeavensAid).Field("cardUpgradeData").GetValue();
			//Ponies.Log("Enhancer Upgrade: " + upgradeData2.name);
			CardUpgradeData moreUpgradeData2 = upgradeData2.GetTriggerUpgrades()[0].GetEffects()[0].GetParamCardUpgradeData();
			//Ponies.Log("Enhancer Upgrade Upgrade: " + moreUpgradeData2.name);
			Traverse.Create(moreUpgradeData2).Field("isUnitSynthesisUpgrade").SetValue(true);
		}
	}
}