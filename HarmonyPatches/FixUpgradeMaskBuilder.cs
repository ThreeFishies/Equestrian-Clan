using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

//The CardUpgradeMaskDataBuilder is borked. Copy the entire thing and replace one line to fix it.
namespace Trainworks.Builders
{
	// Token: 0x0200005D RID: 93
	public class CardUpgradeMaskDataBuilderFixed
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000E1E8 File Offset: 0x0000C3E8
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
		public CardType CardType { get; set; } = CardType.Invalid;

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000E1F9 File Offset: 0x0000C3F9
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x0000E201 File Offset: 0x0000C401
		public List<string> RequiredSubtypes { get; set; } = new List<string>();

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000E20A File Offset: 0x0000C40A
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x0000E212 File Offset: 0x0000C412
		public List<string> ExcludedSubtypes { get; set; } = new List<string>();

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0000E21B File Offset: 0x0000C41B
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x0000E223 File Offset: 0x0000C423
		public List<StatusEffectStackData> RequiredStatusEffects { get; set; } = new List<StatusEffectStackData>();

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000E22C File Offset: 0x0000C42C
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x0000E234 File Offset: 0x0000C434
		public List<StatusEffectStackData> ExcludedStatusEffects { get; set; } = new List<StatusEffectStackData>();

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0000E23D File Offset: 0x0000C43D
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0000E245 File Offset: 0x0000C445
		public List<string> RequiredCardTraits { get; set; } = new List<string>();

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000E24E File Offset: 0x0000C44E
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x0000E256 File Offset: 0x0000C456
		public List<string> ExcludedCardTraits { get; set; } = new List<string>();

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000E25F File Offset: 0x0000C45F
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x0000E267 File Offset: 0x0000C467
		public List<string> RequiredCardEffects { get; set; } = new List<string>();

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000E270 File Offset: 0x0000C470
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x0000E278 File Offset: 0x0000C478
		public List<string> ExcludedCardEffects { get; set; } = new List<string>();

		/// <summary>
		/// If there are any cards in this pool, then only the cards in this pool will be allowed
		/// </summary>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000E281 File Offset: 0x0000C481
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x0000E289 File Offset: 0x0000C489
		public List<CardPool> AllowedCardPools { get; set; } = new List<CardPool>();

		/// <summary>
		/// No cards in this pool will be allowed
		/// </summary>
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000E292 File Offset: 0x0000C492
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x0000E29A File Offset: 0x0000C49A
		public List<CardPool> DisallowedCardPools { get; set; } = new List<CardPool>();

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000E2A3 File Offset: 0x0000C4A3
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x0000E2AB File Offset: 0x0000C4AB
		public List<int> RequiredSizes { get; set; } = new List<int>();

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000E2B4 File Offset: 0x0000C4B4
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x0000E2BC File Offset: 0x0000C4BC
		public List<int> ExcludedSizes { get; set; } = new List<int>();

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000E2C5 File Offset: 0x0000C4C5
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x0000E2CD File Offset: 0x0000C4CD
		public Vector2 CostRange { get; set; } = new Vector2(0f, 99f);

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000E2D6 File Offset: 0x0000C4D6
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x0000E2DE File Offset: 0x0000C4DE
		public bool ExcludeNonAttackingMonsters { get; set; }

		/// <summary>
		/// Operator determines if we require all or at least one
		/// </summary>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000E2E7 File Offset: 0x0000C4E7
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0000E2EF File Offset: 0x0000C4EF
		public CardUpgradeMaskDataBuilder.CompareOperator RequiredSubtypesOperator { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0000E2F8 File Offset: 0x0000C4F8
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x0000E300 File Offset: 0x0000C500
		public CardUpgradeMaskDataBuilder.CompareOperator ExcludedSubtypesOperator { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000E309 File Offset: 0x0000C509
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x0000E311 File Offset: 0x0000C511
		public CardUpgradeMaskDataBuilder.CompareOperator RequiredStatusEffectsOperator { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000E31A File Offset: 0x0000C51A
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x0000E322 File Offset: 0x0000C522
		public CardUpgradeMaskDataBuilder.CompareOperator ExcludedStatusEffectsOperator { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000E32B File Offset: 0x0000C52B
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x0000E333 File Offset: 0x0000C533
		public CardUpgradeMaskDataBuilder.CompareOperator RequiredCardTraitsOperator { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0000E33C File Offset: 0x0000C53C
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x0000E344 File Offset: 0x0000C544
		public CardUpgradeMaskDataBuilder.CompareOperator ExcludedCardTraitsOperator { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000E34D File Offset: 0x0000C54D
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x0000E355 File Offset: 0x0000C555
		public CardUpgradeMaskDataBuilder.CompareOperator RequiredCardEffectsOperator { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000E35E File Offset: 0x0000C55E
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0000E366 File Offset: 0x0000C566
		public CardUpgradeMaskDataBuilder.CompareOperator ExcludedCardEffectsOperator { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000E36F File Offset: 0x0000C56F
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0000E377 File Offset: 0x0000C577
		public bool RequireXCost { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000E380 File Offset: 0x0000C580
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x0000E388 File Offset: 0x0000C588
		public bool ExcludeXCost { get; set; }

		/// <summary>
		/// This is the reason why a card is filtered away from having this upgrade applied to it
		/// </summary>
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000E391 File Offset: 0x0000C591
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0000E399 File Offset: 0x0000C599
		public CardState.UpgradeDisabledReason UpgradeDisabledReason { get; set; }

		/// <summary>
		/// Builds the RoomModifierData represented by this builders's parameters recursively;
		/// </summary>
		/// <returns>The newly created RoomModifierData</returns>
		// Token: 0x0600047F RID: 1151 RVA: 0x0000E3A4 File Offset: 0x0000C5A4
		public CardUpgradeMaskData Build()
		{
			//Here's the error:
			//CardUpgradeMaskData cardUpgradeMaskData = new CardUpgradeMaskData();

			//And the fix:
			CardUpgradeMaskData cardUpgradeMaskData = ScriptableObject.CreateInstance<CardUpgradeMaskData>();

			Type realEnumType = AccessTools.Inner(typeof(CardUpgradeMaskData), "CompareOperator");
			AccessTools.Field(typeof(CardUpgradeMaskData), "allowedCardPools").SetValue(cardUpgradeMaskData, this.AllowedCardPools);
			AccessTools.Field(typeof(CardUpgradeMaskData), "cardType").SetValue(cardUpgradeMaskData, this.CardType);
			AccessTools.Field(typeof(CardUpgradeMaskData), "costRange").SetValue(cardUpgradeMaskData, this.CostRange);
			AccessTools.Field(typeof(CardUpgradeMaskData), "disallowedCardPools").SetValue(cardUpgradeMaskData, this.DisallowedCardPools);
			AccessTools.Field(typeof(CardUpgradeMaskData), "excludedCardEffects").SetValue(cardUpgradeMaskData, this.ExcludedCardEffects);
			AccessTools.Field(typeof(CardUpgradeMaskData), "excludedCardEffectsOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.ExcludedCardEffectsOperator));
			AccessTools.Field(typeof(CardUpgradeMaskData), "excludedCardTraits").SetValue(cardUpgradeMaskData, this.ExcludedCardTraits);
			AccessTools.Field(typeof(CardUpgradeMaskData), "excludedCardTraitsOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.ExcludedCardTraitsOperator));
			AccessTools.Field(typeof(CardUpgradeMaskData), "excludedSizes").SetValue(cardUpgradeMaskData, this.ExcludedSizes);
			AccessTools.Field(typeof(CardUpgradeMaskData), "excludedStatusEffects").SetValue(cardUpgradeMaskData, this.ExcludedStatusEffects);
			AccessTools.Field(typeof(CardUpgradeMaskData), "excludedStatusEffectsOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.ExcludedStatusEffectsOperator));
			AccessTools.Field(typeof(CardUpgradeMaskData), "excludedSubtypes").SetValue(cardUpgradeMaskData, this.ExcludedSubtypes);
			AccessTools.Field(typeof(CardUpgradeMaskData), "excludedSubtypesOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.ExcludedSubtypesOperator));
			AccessTools.Field(typeof(CardUpgradeMaskData), "excludeNonAttackingMonsters").SetValue(cardUpgradeMaskData, this.ExcludeNonAttackingMonsters);
			AccessTools.Field(typeof(CardUpgradeMaskData), "excludeXCost").SetValue(cardUpgradeMaskData, this.ExcludeXCost);
			AccessTools.Field(typeof(CardUpgradeMaskData), "requiredCardEffects").SetValue(cardUpgradeMaskData, this.RequiredCardEffects);
			AccessTools.Field(typeof(CardUpgradeMaskData), "requiredCardEffectsOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.RequiredCardEffectsOperator));
			AccessTools.Field(typeof(CardUpgradeMaskData), "requiredCardTraits").SetValue(cardUpgradeMaskData, this.RequiredCardTraits);
			AccessTools.Field(typeof(CardUpgradeMaskData), "requiredCardTraitsOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.RequiredCardTraitsOperator));
			AccessTools.Field(typeof(CardUpgradeMaskData), "requiredSizes").SetValue(cardUpgradeMaskData, this.RequiredSizes);
			AccessTools.Field(typeof(CardUpgradeMaskData), "requiredStatusEffects").SetValue(cardUpgradeMaskData, this.RequiredStatusEffects);
			AccessTools.Field(typeof(CardUpgradeMaskData), "requiredStatusEffectsOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.RequiredStatusEffectsOperator));
			AccessTools.Field(typeof(CardUpgradeMaskData), "requiredSubtypes").SetValue(cardUpgradeMaskData, this.RequiredSubtypes);
			AccessTools.Field(typeof(CardUpgradeMaskData), "requiredSubtypesOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.RequiredSubtypesOperator));
			AccessTools.Field(typeof(CardUpgradeMaskData), "requireXCost").SetValue(cardUpgradeMaskData, this.RequireXCost);
			AccessTools.Field(typeof(CardUpgradeMaskData), "upgradeDisabledReason").SetValue(cardUpgradeMaskData, this.UpgradeDisabledReason);
			return cardUpgradeMaskData;
		}

		// Token: 0x0200006C RID: 108
		public enum CompareOperator
		{
			// Token: 0x040005DF RID: 1503
			And,
			// Token: 0x040005E0 RID: 1504
			Or
		}
	}
}
