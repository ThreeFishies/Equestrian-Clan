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
    //The herd trait acts as a requirement to stack a certain number of friendly units in a room to allow a spell to be played on that floor
    //Set "ParamInt" to equal the number of friendly units required.
    internal class CardTraitHerd : CardTraitState, ICardEffectStatuslessTooltip
    {
        public string GetTooltipBaseKey(CardEffectState cardEffectState)
        {
            return "CardTraitHerd";
        }

        public override string GetCardText()
        {
            int RelicReduction = 0;
            if (HasImaginaryFriends())
            {
                RelicReduction = 2;
            }

            string text = "CardTraitHerd_CardText".Localize(null);
            text = string.Format(text, (base.GetParamInt() - RelicReduction));
            text = string.Format("<b>{0}</b>", text);
            if (RelicReduction > 0)
            {
                //text = string.Format("<b><color=#007300FF>*{0}*</color></b>", text);
                text = text.Replace(": ", ": <color=#007300FF>*");
                text = text.Replace("</b>", "*</color=#007300FF></b>");
                //Ponies.Log("Herd trait card text: " + text);
                //text = "<color=#007300FF>" + text + "</color>";
            }
            text = text.Replace(":", "");
            return text;
        }

        public override bool GetIsPlayableFromHand(CardManager cardManager, RoomManager roomManager, out CardSelectionBehaviour.SelectionError selectionError)
        {
            return CheckIsPlayable(cardManager, roomManager, out selectionError);
        }
        public override bool GetIsPlayableFromPlay(CardManager cardManager, RoomManager roomManager, out CardSelectionBehaviour.SelectionError selectionError)
        {
            return CheckIsPlayable(cardManager, roomManager, out selectionError);
        }
        private bool HasImaginaryFriends() 
        {
            List<RelicState> yourRelics = ProviderManager.SaveManager.GetAllRelics();
            foreach (RelicState relic in yourRelics)
            {
                if (relic.GetRelicDataID() == ImaginaryFriends.ID) 
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckIsPlayable(CardManager cardManager, RoomManager roomManager, out CardSelectionBehaviour.SelectionError selectionError)
        {
            int selectedRoom = roomManager.GetSelectedRoom();
            int RelicReduction = 0;
            if (HasImaginaryFriends()) 
            {
                RelicReduction = 2;
            }

            //Herd spells can't be playe din the pyre room
            if (roomManager.GetRoom(selectedRoom).GetIsPyreRoom())
            {
                selectionError = CardSelectionBehaviour.SelectionError.RoomFurnace;
                return false;
            }

            int numFriendlyUnits = roomManager.GetRoom(selectedRoom).GetNumCharacters(Team.Type.Monsters);

            if (numFriendlyUnits < (base.GetParamInt() - RelicReduction) || numFriendlyUnits == 0)
            {
                selectionError = CardSelectionBehaviour.SelectionError.Unplayable;
                return false;
            }

            selectionError = CardSelectionBehaviour.SelectionError.None;
            return true;
        }
    }

    internal class CardEffectHerdTooltip : CardEffectBase, ICardEffectStatuslessTooltip
    {
        public string GetTooltipBaseKey(CardEffectState cardEffectState)
        {
            return "CardTraitHerd";
        }

        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
        {
            yield break;
        }
    }
}