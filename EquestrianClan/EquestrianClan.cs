using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;
using UnityEngine;
using Equestrian.MonsterCards;
using System.Text;
using HarmonyLib;
using Trainworks.Enums;
using Equestrian.Init;
using Equestrian.Champions;

namespace Equestrian
{
	internal class EquestrianClan
	{
		public static readonly string ID = "Equestrian_Clan";

		public static ClassData Buildclan()
		{
			return new ClassDataBuilder
			{
				ClassID = ID,
				DraftIconPath = "ClanAssets/Icon_CardBack_Equestrian.png",
				Name = "Equestrian",
				TitleLoc = "Equestrian_Clan_Name",
				Description = "Small, cute and friendly, the Equestrians are unlikely champions of Hell. But when Tarturus froze over, they realized the errors of Heaven's ways, and will fight to restore balance to the world.",
				DescriptionLoc = "Clan_desc",
				SubclassDescription = "Ally yourself with the tiny and timid Equestrians.",
				SubclassDescriptionLoc = "Clan_subdesc",
				IconAssetPaths = new List<string>
				{
					"ClanAssets/EquestrianLogo_32.png",
					"ClanAssets/EquestrianLogo_89.png",
					"ClanAssets/EquestrianLogo_89.png",
					"ClanAssets/EquestrianLogo_Silhouette.png"
				},
				CardFrameUnitPath = "ClanAssets/unit-cardframe-Equestrian.png",
				CardFrameSpellPath = "ClanAssets/spell-cardframe-Equestrian.png",
				UiColor = new Color(0.757f, 0.780f, 0.780f, 1.0f),
				UiColorDark = new Color(0.484f, 0.506f, 0.506f, 1.0f)
				
			}.BuildAndRegister();
		}
	}

	public class EquestrianBanner 
	{
		public static readonly string BannerID = EquestrianClan.ID + "_Banner";
		public static readonly string RewardID = EquestrianClan.ID + "_BannerReward";
		public static CardPool draftPool = UnityEngine.ScriptableObject.CreateInstance<CardPool>();
		public static RewardNodeData data;

		public static void buildbanner()
		{
			var cardDataList = (Malee.ReorderableArray<CardData>)AccessTools.Field(typeof(CardPool), "cardDataList").GetValue(draftPool);
			//cardDataList.Add(CustomCardManager.GetCardDataByID(PreenySnuggle.ID));
			cardDataList.Add(CustomCardManager.GetCardDataByID(StaticJoy.ID));
			cardDataList.Add(CustomCardManager.GetCardDataByID(Snackasmacky.ID));
			//cardDataList.Add(CustomCardManager.GetCardDataByID(MareYouKnow.ID));
			cardDataList.Add(CustomCardManager.GetCardDataByID(MistyStep.ID));
			cardDataList.Add(CustomCardManager.GetCardDataByID(SqueakyBooBoo.ID));
			//cardDataList.Add(CustomCardManager.GetCardDataByID(TavernAce.ID));
			//cardDataList.Add(CustomCardManager.GetCardDataByID(LordOfEmber.ID));
			cardDataList.Add(CustomCardManager.GetCardDataByID(GuardianOfTheGates.ID));

			Ponies.Log("Banner Card Pool");

			data = new RewardNodeDataBuilder
			{
				RewardNodeID = BannerID,
				MapNodePoolIDs = new List<string>
				{
					VanillaMapNodePoolIDs.RandomChosenMainClassUnit,
					VanillaMapNodePoolIDs.RandomChosenSubClassUnit
				},
				TooltipTitleKey = "name_equestrian_banner",
				TooltipBodyKey = "desc_equestrian_banner",
				RequiredClass = Ponies.EquestrianClanData,
				FrozenSpritePath = "ClanAssets/Equestrian_Frozen.png",
				EnabledSpritePath = "ClanAssets/Equestrian_Enabled.png",
				DisabledSpritePath = "ClanAssets/Equestrian_Disabled.png",
				DisabledVisitedSpritePath = "ClanAssets/Equestrian_VisitedDisabled.png",
				GlowSpritePath = "ClanAssets/MSK_Map_Clan_CEquestrian_01.png",
				MapIconPath = "ClanAssets/Equestrian_Enabled.png",
				MinimapIconPath = "ClanAssets/EquestrianLogo_Silhouette.png",
				ControllerSelectedOutline = "ClanAssets/selection_outlines.png",
				SkipCheckInBattleMode = true,
				OverrideTooltipTitleBody = false,
				NodeSelectedSfxCue = "Node_Banner",
				RewardBuilders = new List<IRewardDataBuilder>
				{
					new DraftRewardDataBuilder()
					{
						DraftRewardID = RewardID,
						_RewardSpritePath = "ClanAssets/Icon_CardBack_Equestrian.png",
						_RewardTitleKey = "Equestrian_Banner",
						_RewardDescriptionKey = "Choose a card!",
						Costs = new int[]
						{
							100
						},
						_IsServiceMerchantReward = false,
						DraftPool = draftPool,
						ClassType = RunState.ClassType.MainClass | RunState.ClassType.SubClass | RunState.ClassType.None,
						DraftOptionsCount = 2,
						RarityFloorOverride = CollectableRarity.Uncommon,
					}
				}
			}.BuildAndRegister();
		}
	}
}
