using Trainworks.Managers;
using HarmonyLib;

namespace Equestrian.HarmonyPatches
{ 
    public static class HephIsAFineAndDandyLADYThankYouVeryMuch 
    { 
        public static void Fix() 
        {
            CardData HephCard = CustomCardManager.GetCardDataByID("6f770340-a37d-4e25-9a93-6b2e46f9a252"); //Heph
            CharacterChatterData HephTalk = HephCard.GetSpawnCharacterData().GetCharacterChatterData();

            AccessTools.Field(typeof(CharacterChatterData), "gender").SetValue(HephTalk, CharacterChatterData.Gender.Female);
        }
    }
}