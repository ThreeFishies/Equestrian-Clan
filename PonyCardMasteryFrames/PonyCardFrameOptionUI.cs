using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;
using UnityEngine;
using UnityEngine.UI;
using Equestrian.MonsterCards;
using System.Text;
using HarmonyLib;
using Trainworks.Enums;
using Equestrian.Init;

namespace Equestrian.CardMasteryFrame 
{
    /*
    //This seems unnecessary. It looks like only one CardFrameOptionUI exists. The MasterySpriteSelector sorts through the data to build each frame as needed.
    public static class PonyCardFrameOptionUI 
    {
        public static MasteryFrameType ponyFrame = Ponies.PonyFrame.GetEnum();
        public static int index = (int)Ponies.PonyFrame.GetEnum(); //Not sure what this should be.

        public static CardFrameOptionUI Build() 
        {
            CardFrameOptionUI cardFrame = new CardFrameOptionUI();

            cardFrame.Set(ponyFrame, ProviderManager.SaveManager.GetMetagameSave(), index);
            AccessTools.Field(typeof(CardFrameOptionUI),"locked").SetValue(cardFrame, MissingMareUnlockedMasteryCriteria.timesMissingMareSeen > 0);

            return cardFrame;
        }
    }
    */

    [HarmonyPatch(typeof(CompendiumSectionCardFrames), "Initialize")]
    public static class AddPonyMasteryFrameToList 
    { 
        public static void Postfix(ref List<MasteryFrameType> ___frameTypes, ref GridLayoutGroup ___optionsLayout) 
        { 
            if (!Ponies.EquestrianClanIsInit) { return; }

            if (!___frameTypes.Contains(Ponies.PonyFrame.GetEnum())) 
            {
                ___frameTypes.Add(Ponies.PonyFrame.GetEnum());
                Ponies.Log("Pony Frame Added.");

                //CardFrameOptionUI[] cardFrames = ___optionsLayout.GetComponentsInChildren<CardFrameOptionUI>();
                //foreach (CardFrameOptionUI cardFrameOptionUI in cardFrames) 
                //{
                    //Ponies.Log("CardFrameOptionUI: " + cardFrameOptionUI.FrameType.ToString());
                    //Output: None
                //}
            }
            else 
            {
                Ponies.Log("Pony Frame Present.");
            }
        }
    }

    /*
    [HarmonyPatch(typeof(CardFrameOptionUI),"Set")]
    public static class PonyCardFrameOptionUICheck 
    { 
        public static void Prefix(ref MasteryFrameType frameType, ref MetagameSaveData metagameSave, ref int index) 
        { 
            if (!Ponies.EquestrianClanIsInit) { return; }

            Ponies.Log("Frame Type: " + frameType.ToString() + " at index: " + index);
        }
    }
    */

    [HarmonyPatch(typeof(MetagameSaveData), "HasUnlockedMasteryFrameType")]
    public static class HasUnlockedPonyMasteryFrameType 
    { 
        public static void Postfix(ref bool __result, ref MasteryFrameType masteryType)
        { 
            if (!Ponies.EquestrianClanIsInit) { return; }

            if (masteryType == Ponies.PonyFrame.GetEnum()) 
            {
                __result = MissingMareUnlockedMasteryCriteria.timesMissingMareSeen > 0;
            }
        }
    }
}