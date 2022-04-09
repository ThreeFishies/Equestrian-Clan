using System;
using System.Collections;
using System.Collections.Generic;
using Trainworks.Builders;
using System.Text;
using HarmonyLib;
using Trainworks.Managers;
using Trainworks.Constants;
using System.Linq;
using UnityEngine;
using Trainworks.Utilities;
using Equestrian.Init;
using Equestrian.MonsterCards;
using ShinyShoe.Loading;

namespace CustomEffects
{
	public class RoomStateCostModifierAll : RoomStateCostModifierBase
	{
		// Token: 0x06001B7F RID: 7039 RVA: 0x00067BC4 File Offset: 0x00065DC4
		public override int GetModifiedCost(CardState cardState, CardStatistics cardStatistics, MonsterManager monsterManager)
		{
			return this.modifiedCost;
		}
	}
}