using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomEffects
{
	// Token: 0x0200008E RID: 142
	public sealed class CustomCardEffectDoubleCurrentStats : CardEffectBase
	{
		// Token: 0x060006C4 RID: 1732 RVA: 0x00021094 File Offset: 0x0001F294
		public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			return cardEffectParams.targets.Count > 0;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x000211BC File Offset: 0x0001F3BC
		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			foreach (CharacterState target in cardEffectParams.targets)
			{
				int attackDamage = target.GetAttackDamage();
				//int unbuffedAttackDamage = target.GetUnbuffedAttackDamage();
				CardEffectState healerEffect = target.GetHealerEffect();
				if (healerEffect != null)
				{
					attackDamage = healerEffect.GetParamInt();
				}
				int maxHP = target.GetMaxHP();
				int currentHP = target.GetHP();
				CardUpgradeState upgradeState = new CardUpgradeState();
				upgradeState.Setup();
				//upgradeState.SetAttackDamage(attackDamage);
				upgradeState.SetAttackDamageBuff(attackDamage);
				//upgradeState.SetAdditionalHP(maxHP);
				yield return target.ApplyCardUpgrade(upgradeState, false);
				target.SetHealth(currentHP*2, maxHP*2);
				yield return target.ApplyHeal(maxHP*2, true, null, null, false);
				CardState spawnerCard = target.GetSpawnerCard();
				bool flag = false;
				if (spawnerCard != null && (target.GetSourceCharacterData() == spawnerCard.GetSpawnCharacterData() || spawnerCard.GetSpawnCharacterData() == null))
				{
					flag = true;
				}
				if (spawnerCard != null && !cardEffectParams.saveManager.PreviewMode && flag)
				{
					CardAnimator.CardUpgradeAnimationInfo type = new CardAnimator.CardUpgradeAnimationInfo(spawnerCard, upgradeState, CardPile.None, CardPile.DeckPileTop, 1, 1);
					CardAnimator.DoAddRecentCardUpgrade.Dispatch(type);
					spawnerCard.GetTemporaryCardStateModifiers().AddUpgrade(upgradeState, null);
				}
				upgradeState = null;
				//target = null;
			}
			List<CharacterState>.Enumerator enumerator = default(List<CharacterState>.Enumerator);
			yield break;
			yield break;
		}
	}
}