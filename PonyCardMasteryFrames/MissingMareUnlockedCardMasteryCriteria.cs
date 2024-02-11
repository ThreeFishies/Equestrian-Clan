using System;
using UnityEngine;
using Equestrian.Init;
using Equestrian.MonsterCards;
using Equestrian.Metagame;
using Trainworks.Managers;
using HarmonyLib;

namespace Equestrian.CardMasteryFrame
{

	// Token: 0x02000572 RID: 1394
	[Serializable]
	public class MissingMareUnlockedMasteryCriteria : UnlockedMasteryCriteriaBase
	{
		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600306D RID: 12397 RVA: 0x000BD0DF File Offset: 0x000BB2DF
		protected override string instructionsKey
		{
			get
			{
				return "MasteryFrameUnlockCriteria_MissingMare";
			}
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x0001C727 File Offset: 0x0001A927
		protected override int GetTargetUnlockCount()
		{
			return 1;
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000BD0E6 File Offset: 0x000BB2E6
		protected override int GetCurrentCount()
		{
			return timesMissingMareSeen;
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000BD0EE File Offset: 0x000BB2EE
		public override void UpdateStatus(RunAggregateData runAggregateData, DateTime challengeStartTime = default(DateTime))
		{
			return;
		}

		public static void SpotMissingMare() 
		{ 
			timesMissingMareSeen++;
			PonyMetagame.dirty = true;
			PonyMetagame.SavePonyMetaFile();

			if (timesMissingMareSeen == 1) 
			{
				if (ProviderManager.TryGetProvider<ScreenManager>(out ScreenManager screenManager))
				{
					screenManager.ShowDialog(new Dialog.Data
					{
						style = Dialog.Style.Normal,
						content = "FTUE_EquestrianCardMasteryFrameUnlock".Localize(),
						button1Text = "FTUE_BuckYeah_ButtonLabel".Localize(null),
						callbackClose = null
					}, null);
				}
			}
		}

		// Token: 0x04001D32 RID: 7474
		[SerializeField]
		public static int timesMissingMareSeen;
	}

    [HarmonyPatch (typeof(MetagameSaveData), "GetCriteriaForMasteryFrameType")]
	public static class GetMissingMareCriteriaForMasteryFrameType 
	{
		public static void Postfix(ref MasteryFrameType frameType, ref IUnlockedMasteryCriteria __result) 
		{
			if (!Ponies.EquestrianClanIsInit) { return; }

			if (frameType == Ponies.PonyFrame.GetEnum()) 
			{
				__result = new MissingMareUnlockedMasteryCriteria();
			}
		}
	}

    [HarmonyPatch (typeof(CardRewardData),"GrantReward")]
	public static class CheckForMissingMare 
	{ 
		public static void Prefix(ref CardRewardData __instance) 
		{
			if (!Ponies.EquestrianClanIsInit) { return; }

			Ponies.Log("Card Reward Data ID: " + __instance.GetCardData().GetID());

			if (__instance.GetCardData().GetID() == CustomCardManager.GetCardDataByID(MissingMare.ID).GetID()) 
			{
				MissingMareUnlockedMasteryCriteria.SpotMissingMare();
			}
		}
	}
}