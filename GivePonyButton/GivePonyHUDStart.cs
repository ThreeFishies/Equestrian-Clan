using HarmonyLib;
using Equestrian.CardPools;
using Trainworks.Managers;
using Equestrian.HarmonyPatches;
using Equestrian.Mutators;

namespace GivePony
{
	// Token: 0x02000003 RID: 3
	[HarmonyPatch(typeof(BattleHud), "Start")]
	public static class Mod_BattleHud_Start
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002069 File Offset: 0x00000269
		private static void Postfix(BattleHud __instance)
		{
			if (GivePonyToggle.isGivePonyButtonEnabled)
			{
				Equestrian.Init.Ponies.Log("Loading 'Give Pony' button.");

				GivePonyButton.Create(__instance);
			}
			FixArt.TryYetAnotherFix(MyCardPools.FixArtCardPool); //Yay. It worked.
		}
	}
}