using Trainworks.Managers;
using Trainworks.Builders;
using Trainworks.Constants;
using Equestrian.Init;
using Equestrian.Relic;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HarmonyLib;
using ShinyShoe;
using Equestrian.Sprites;
using Equestrian.MonsterCards;
using UnityEngine;
using CustomEffects;
using System.Runtime.CompilerServices;

namespace Equestrian.Chatter
{ 
    public static class CharacterChatterUpdater 
    {
        /// <summary>
        /// Adds localization keys to an existing character's idle chatter data.
        /// </summary>
        /// <param name="character">
        /// The character data to be updated.<br></br>
        /// If the character has no chatter data, new chatter data will be created.
        /// </param>
        /// <param name="chatterKeys">
        /// The list of localization keys to be added.
        /// </param>
        /// <param name="gender">
        /// The characterer's gender. This field is only used if the character's base chatter data is undefined.
        /// </param>
        /// <returns>
        /// Returns true on success, or false if the character is null or the list is null or empty.<br></br>
        /// </returns>
        public static bool AddIdleChatterKey(CharacterData character, List<string> chatterKeys, CharacterChatterData.Gender gender = CharacterChatterData.Gender.Neutral)
        {
            if (character == null) return false;
            if (chatterKeys == null) return false;
            if (chatterKeys.Count == 0) return false;

            bool keyAdded = false;

            CharacterChatterData chatterData = AccessTools.Field(typeof(CharacterData), "characterChatterData").GetValue(character) as CharacterChatterData;
            if (chatterData == null) 
            {
                Ponies.Log("Creating new chatter data for unit: " + character.GetNameEnglish());

                chatterData = new CharacterChatterDataBuilder() 
                {
                    gender = gender,
                    characterIdleExpressionKeys = chatterKeys,
                }.Build();

                AccessTools.Field(typeof(CharacterData), "characterChatterData").SetValue(character, chatterData);
            }
            else 
            {
                List<CharacterChatterData.ChatterExpressionData> idleKeys = AccessTools.Field(typeof(CharacterChatterData), "characterIdleExpressions").GetValue(chatterData) as List<CharacterChatterData.ChatterExpressionData>;
                foreach (string newIdleKey in chatterKeys) 
                {
                    CharacterChatterData.ChatterExpressionData idleKey = new CharacterChatterData.ChatterExpressionData() { locKey= newIdleKey };

                    //if (!idleKeys.Contains(idleKey)) 
                    //{ 
                        idleKeys.Add(idleKey);
                        keyAdded= true;
                    //}
                }
            }

            return keyAdded;
        }

        public static bool AddIdleChatterKey(CharacterData character, string chatterKey, CharacterChatterData.Gender gender = CharacterChatterData.Gender.Neutral)
        { 
            return AddIdleChatterKey(character, new List<string> { chatterKey });
        }
    }
}