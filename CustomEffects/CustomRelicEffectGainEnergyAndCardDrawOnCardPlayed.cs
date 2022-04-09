using System;
using System.Collections;

// Token: 0x0200028F RID: 655
namespace CustomEffects
{
	public sealed class CustomRelicEffectGainEnergyAndCardDrawOnCardTypePlayed : RelicEffectBase, ICardPlayedRelicEffect, IRelicEffect
	{
		// Token: 0x0600169E RID: 5790 RVA: 0x00059816 File Offset: 0x00057A16
		public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
		{
			base.Initialize(relicState, relicData, relicEffectData);
			this.energyToGain = relicEffectData.GetParamInt();
			this.cardsToDraw = relicEffectData.GetParamMinInt();
			this.subtypeData = relicEffectData.GetParamCharacterSubtype();
			this.cardType = relicEffectData.GetParamCardType();
			this.cardType2 = (CardType)relicEffectData.GetParamMaxInt();
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x00059845 File Offset: 0x00057A45
		public bool TestCardPlayed(CardPlayedRelicEffectParams relicEffectParams)
		{
			return (this.cardType == CardType.Invalid || relicEffectParams.cardState.GetCardType() == this.cardType || relicEffectParams.cardState.GetCardType() == this.cardType2) && CardManager.DoesCardStatePassSubtypeCheck(relicEffectParams.cardState, this.subtypeData, relicEffectParams.relicManager);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0005987C File Offset: 0x00057A7C
		public IEnumerator OnCardPlayed(CardPlayedRelicEffectParams relicEffectParams)
		{
			base.NotifyRoomRelicTriggered(relicEffectParams);
			relicEffectParams.cardManager.DrawCards(this.cardsToDraw, null, CardType.Invalid);
			relicEffectParams.playerManager.AddEnergy(this.energyToGain);
			yield break;
		}

		// Token: 0x04000BF6 RID: 3062
		private int energyToGain;
		private int cardsToDraw;

		// Token: 0x04000BF7 RID: 3063
		private SubtypeData subtypeData;

		// Token: 0x04000BF8 RID: 3064
		private CardType cardType;

		private CardType cardType2;
	}
}
