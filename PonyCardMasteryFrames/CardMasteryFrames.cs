using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Reflection;
using HarmonyLib;
using Equestrian.Init;
using Trainworks.Managers;
using Equestrian.SpellCards;
using Equestrian.MonsterCards;
using Equestrian.Relic;
using Trainworks.Constants;
using Trainworks.Builders;
using Trainworks.Enums;
using Equestrian.CardPools;
using Equestrian.Champions;
using ShinyShoe.Loading;
using UnityEngine.AddressableAssets;
using UnityEngine;

namespace Equestrian.HarmonyPatches
{
    //This will allow the MasteryFrameType enum to be expanded, allowing us to add an additional type.
    public class PonyFrame : ExtendedEnum<PonyFrame, MasteryFrameType> 
    {
        // Token: 0x06000106 RID: 262 RVA: 0x00005CDC File Offset: 0x00003EDC
        public PonyFrame(string localizationKey, int? ID = null) : base(localizationKey, ID ?? PonyFrame.GetNewFrameGUID())
        {
            //CharacterTriggerData.TriggerToLocalizationExpression[this.GetEnum()] = localizationKey;

        }

        // Token: 0x06000107 RID: 263 RVA: 0x00005D20 File Offset: 0x00003F20
        public static int GetNewFrameGUID()
        {
            PonyFrame.NumFrames++;
            return PonyFrame.NumFrames;
        }

        // Token: 0x06000108 RID: 264 RVA: 0x00005D44 File Offset: 0x00003F44
        public static implicit operator PonyFrame(MasteryFrameType frame)
        {
            return ExtendedEnum<PonyFrame, MasteryFrameType>.Convert(frame);
        }

        // Token: 0x0400016E RID: 366
        // Some arbitrary number, I guess.
        private static int NumFrames = 576;
    }


    [HarmonyPatch(typeof(MasterySpriteSelector), "GetSprite")]
    public static class TestCardMastery
    {
        public static Sprite SpellFrame;
        public static Sprite UnitFrame;
        public static Sprite ChampionFrame;
        public static Sprite UnitNameplate;
        public static Sprite ChampionNameplate;
        public static Sprite SpellNameplate;
        public static Sprite Typeplate;
        public static Sprite UnitEmberbacking;
        public static Sprite SpellEmberbacking;
        public static Sprite Locked;

        
        public static void Prefix(ref MasteryFrameType masteryType, out bool __state) 
        {
            __state = false;

            if (!Ponies.EquestrianClanIsInit) { return; }

            //Ponies.Log("Line 66");

            if (masteryType == Ponies.PonyFrame.GetEnum()) 
            {
                //Ponies.Log("Pony frame type detected.");

                //Set __state to 'true' when the pony frame is detected. This will be passed to Postfix().
                __state = true;

                //Change the frame type to 'default' to prevent errors. This is needed because the mastery type defaults to 'none' if the type is unrecognized.
                //Unfortunately, frame type 'none' has no 'locked' display, which causes the card frame selection screen to glitch out if the frame has not been unlocked yet.
                masteryType = MasteryFrameType.Default;
            }
            else 
            {
                //Ponies.Log(masteryType.ToString() + ": " + (int)masteryType);
            }
        }
        

        public static void Postfix(ref Sprite __result, ref CardType cardType, ref bool isChampion, ref MasteryFrameType masteryType, bool __state)
        {
            if (!Ponies.EquestrianClanIsInit) { return; }

            //If pony frames are selected, display appropriate art.
            if (!__state) { return; }

            //Hmm...
            //Ponies.Log("Original Sprite name: " + __result.name);
            //Sniffed the default card mastery frames.
            //Results:

            // unit-backframe-mastered-champion
            // unit-nameplate-mastered-champion
            // all-typeplate-mastered
            // unit-emberbacking-mastered
            // diamond_frame

            // spell-backframe-mastered
            // spell-nameplate-mastered
            // all-typeplate-mastered
            // spell-emberbacking-mastered
            // diamond_frame

            // unit-backframe-mastered-nonchampion
            // unit-nameplate-mastered-nonchampion
            // all-typeplate-mastered
            // unit-emberbacking-mastered
            // diamond_frame

            //Ponies.Log("Display Pony frame type.");
            //Ponies.Log("Original sprite is null: " + (__result == null).ToString());
            //if (__result != null)
            //{
            //    Ponies.Log("Original Sprite name: " + __result.name);
            //}

            string localPath = Path.GetDirectoryName(Ponies.Instance.Info.Location);

            if (SpellFrame == null) 
            { 
                SpellFrame = CustomAssetManager.LoadSpriteFromPath(Path.Combine(localPath, "ClanAssets/spell-backframe-pony.png"));
                UnitFrame = CustomAssetManager.LoadSpriteFromPath(Path.Combine(localPath, "ClanAssets/unit-backframe-pony-nonchampion.png"));
                ChampionFrame = CustomAssetManager.LoadSpriteFromPath(Path.Combine(localPath, "ClanAssets/unit-backframe-pony-champion.png"));
                UnitNameplate = CustomAssetManager.LoadSpriteFromPath(Path.Combine(localPath, "ClanAssets/unit-nameplate-mastered-nonchampion.png"));
                ChampionNameplate = CustomAssetManager.LoadSpriteFromPath(Path.Combine(localPath, "ClanAssets/unit-nameplate-mastered-champion.png"));
                SpellNameplate = CustomAssetManager.LoadSpriteFromPath(Path.Combine(localPath, "ClanAssets/spell-nameplate-mastered.png"));
                Typeplate = CustomAssetManager.LoadSpriteFromPath(Path.Combine(localPath, "ClanAssets/all-typeplate-mastered.png"));
                UnitEmberbacking = CustomAssetManager.LoadSpriteFromPath(Path.Combine(localPath, "ClanAssets/unit-emberbacking-mastered.png"));
                SpellEmberbacking = CustomAssetManager.LoadSpriteFromPath(Path.Combine(localPath, "ClanAssets/spell-emberbacking-mastered.png"));
                Locked = CustomAssetManager.LoadSpriteFromPath(Path.Combine(localPath, "ClanAssets/MasteryFrames_Logbook_Pony_Locked.png"));
            }

            //Card frames revert to normal (unmastered) if the frame type is unrecognized.
            if (__result == null || __result.name == "MasteryFrames_Logbook_Default_Locked")
            {
                __result = Locked;
            }
            if (__result.name == "spell-backframe-mastered")
            {
                __result = SpellFrame;
            }
            if (__result.name == "unit-backframe-mastered-nonchampion")
            {
                __result = UnitFrame;
            }
            if (__result.name == "unit-backframe-mastered-champion")
            {
                __result = ChampionFrame;
            }
            if (__result.name == "unit-nameplate-mastered-nonchampion")
            {
                __result = UnitNameplate;
            }
            if (__result.name == "unit-nameplate-mastered-champion")
            {
                __result = ChampionNameplate;
            }
            if (__result.name == "spell-nameplate-mastered")
            {
                __result = SpellNameplate;
            }
            if (__result.name == "all-typeplate-mastered")
            {
                __result = Typeplate;
            }
            if (__result.name == "unit-emberbacking-mastered")
            {
                __result = UnitEmberbacking;
            }
            if (__result.name == "spell-emberbacking-mastered")
            {
                __result = SpellEmberbacking;
            }
        }
    }
}