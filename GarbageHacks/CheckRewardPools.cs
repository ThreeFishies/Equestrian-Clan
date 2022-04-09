using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Equestrian.Init;
using Trainworks.Managers;
using Equestrian.SpellCards;
using Equestrian.MonsterCards;
using Equestrian.Relic;
using Trainworks.Constants;
using Trainworks.Builders;
using Equestrian.CardPools;
using Equestrian;

namespace Equestrian.HarmonyPatches
{
    [HarmonyPatch(typeof(CardPoolHelper), "GetCardsForClass", new Type[] { typeof(CardPool), typeof(ClassData), typeof(int), typeof(CollectableRarity), typeof(SaveManager), typeof(CardPoolHelper.RarityCondition), typeof(bool) })]
    public static class PoolInspector 
    { 
        public static void Postfix(ref List<CardData> __result, ref CardPool cardPool, ref ClassData classData, ref int classLevel)
        {
            //Ponies.Log("INSPECTING A CARD POOL FILTERED BY CLASS");

            //Ponies.Log("Card Pool: " + cardPool.name);
            //Ponies.Log("Clan: " + classData.name);
            //Ponies.Log("Clan Level: " + classLevel);

            if (__result.Count > 0) 
            { 
                List<CardData> errorsToRemove = new List<CardData>();
                errorsToRemove.Clear();

                foreach (CardData card in __result) 
                {
                    //Ponies.Log("Card: " + card.GetNameEnglish());
                    //Ponies.Log("Unlock level: " + card.GetUnlockLevel());

                    if (card.GetUnlockLevel() > classLevel) 
                    {
                        //Ponies.Log("ERROR DETECTED!!!");
                        errorsToRemove.Add(card);
                    }
                }

                if (errorsToRemove.Count > 0)
                {
                    foreach (CardData removableError in errorsToRemove)
                    {
                        __result.Remove(removableError);
                    }
                }

                errorsToRemove.Clear();
            }

            //Ponies.Log("________________________________________");
        }
    }
}