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
using Equestrian.Relic;

namespace CustomEffects
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    // Token: 0x020000BD RID: 189
    public sealed class CustomCardEffectAddStatusImmunity : CardEffectBase
    {
        // Token: 0x060007C6 RID: 1990 RVA: 0x00023240 File Offset: 0x00021440
        public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
        {
            SubtypeData targetCharacterSubtype = cardEffectState.GetTargetCharacterSubtype();
            if (targetCharacterSubtype != null && !targetCharacterSubtype.IsNone)
            {
                for (int i = 0; i < cardEffectParams.targets.Count; i++)
                {
                    if (cardEffectParams.targets[i].GetHasSubtype(targetCharacterSubtype))
                    {
                        return true;
                    }
                }
                return false;
            }
            return true;
        }

        // Token: 0x060007C7 RID: 1991 RVA: 0x00023294 File Offset: 0x00021494
        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
        {
            StatusEffectStackData paramStatusStack = cardEffectState.GetParamStatusStack();
            if (paramStatusStack == null)
            {
                yield break;
            }
            for (int i = 0; i < cardEffectParams.targets.Count; i++)
            {
                CharacterState characterState = cardEffectParams.targets[i];
                //int num = characterState.GetStatusEffectStacks(paramStatusStack.statusId);
                //if (num > 0)
                //{
                //    SubtypeData targetCharacterSubtype = cardEffectState.GetTargetCharacterSubtype();
                //    bool flag = targetCharacterSubtype != null && !targetCharacterSubtype.IsNone;
                //    if (!flag || (flag && characterState.GetHasSubtype(targetCharacterSubtype)))
                //    {
                //        characterState.RemoveStatusEffect(paramStatusStack.statusId, false, num, true, cardEffectParams.sourceRelic, null);
                //    }
                //}
                characterState.AddImmunity(paramStatusStack.statusId);
            }
            yield break;
        }

        // These tooltip functions are never invoked.
        // To get them to show up, use CardEffectAddStatusEffect or CardEffectRemoveStatusEffect with dummy data.

        // Token: 0x060007C8 RID: 1992 RVA: 0x000232AA File Offset: 0x000214AA
        public override void GetTooltipsStatusList(CardEffectState cardEffectState, ref List<string> outStatusIdList)
        {
            CustomCardEffectAddStatusImmunity.GetTooltipsStatusList(cardEffectState.GetSourceCardEffectData(), ref outStatusIdList);
        }

        // Token: 0x060007C9 RID: 1993 RVA: 0x000232B8 File Offset: 0x000214B8
        public static void GetTooltipsStatusList(CardEffectData cardEffectData, ref List<string> outStatusIdList)
        {
            //Ponies.Log("Sniffing Status IDs.");
            foreach (StatusEffectStackData status in cardEffectData.GetParamStatusEffects())
            {
                //Ponies.Log("StatusID: " + status.statusId);
                outStatusIdList.Add(status.statusId);
            }
        }
    }

}