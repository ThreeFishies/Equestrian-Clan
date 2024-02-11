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
using ShinyShoe.Logging;
using Equestrian.Arcadian;

namespace Equestrian.HarmonyPatches
{
    //This will allow the MasteryFrameType enum to be expanded, allowing us to add an additional type.
    public class PonyLoreTooltipType : ExtendedEnum<PonyLoreTooltipType, TooltipDesigner.TooltipDesignType>
    {
        // Token: 0x06000106 RID: 262 RVA: 0x00005CDC File Offset: 0x00003EDC
        public PonyLoreTooltipType(string localizationKey, int? ID = null) : base(localizationKey, ID ?? PonyLoreTooltipType.GetNewTooltipTypeGUID())
        {
        }

        // Token: 0x06000107 RID: 263 RVA: 0x00005D20 File Offset: 0x00003F20
        public static int GetNewTooltipTypeGUID()
        {
            PonyLoreTooltipType.NumTypes++;
            return PonyLoreTooltipType.NumTypes;
        }

        // Token: 0x06000108 RID: 264 RVA: 0x00005D44 File Offset: 0x00003F44
        public static implicit operator PonyLoreTooltipType(TooltipDesigner.TooltipDesignType tooltipType)
        {
            return ExtendedEnum<PonyLoreTooltipType, TooltipDesigner.TooltipDesignType>.Convert(tooltipType);
        }

        // Token: 0x0400016E RID: 366
        // Twelve is next in the list.
        private static int NumTypes = 12;

        public static Sprite PonyLoreTooltipBb;

        public static TooltipDesigner.TooltipDesignData ponyTooltipDesignData;

        public static void Initialize()
        {
            //Ponies.Log("Line 55");
            string localPath = Path.GetDirectoryName(Ponies.Instance.Info.Location);
            //Ponies.Log("Line 57");

            if (PonyLoreTooltipBb == null)
            {
                //Ponies.Log("Line 61");
                PonyLoreTooltipBb = CustomAssetManager.LoadSpriteFromPath(Path.Combine(localPath, "ClanAssets/loreTooltipEquestrianBg.png"));
                //Ponies.Log("Line 63");

                ponyTooltipDesignData = new TooltipDesigner.TooltipDesignData();
                //Ponies.Log("Line 66");

                AccessTools.Field(typeof(TooltipDesigner.TooltipDesignData), "_tooltipDesignType").SetValue(ponyTooltipDesignData, Ponies.PonyLoreTooltip.GetEnum());
                AccessTools.Field(typeof(TooltipDesigner.TooltipDesignData), "_fontStyle").SetValue(ponyTooltipDesignData, TMPro.FontStyles.Italic);
                AccessTools.Field(typeof(TooltipDesigner.TooltipDesignData), "_fontColor").SetValue(ponyTooltipDesignData, Color.black);
                AccessTools.Field(typeof(TooltipDesigner.TooltipDesignData), "_backgroundSprite").SetValue(ponyTooltipDesignData, PonyLoreTooltipBb);
                AccessTools.Field(typeof(TooltipDesigner.TooltipDesignData), "_width").SetValue(ponyTooltipDesignData, TooltipDesigner.TooltipWidth.Default);
                UInt32 zzz = 100;
                AccessTools.Field(typeof(TooltipDesigner.TooltipDesignData), "_additionalTextRelativeSize").SetValue(ponyTooltipDesignData, zzz);
                Vector2Int contentPadding = new Vector2Int() { x = 1, y = 1 };
                AccessTools.Field(typeof(TooltipDesigner.TooltipDesignData), "_contentPadding").SetValue(ponyTooltipDesignData, contentPadding);
                //Ponies.Log("Line 76");
            }
        }
    }

    public class PonyRelicTooltipType : ExtendedEnum<PonyRelicTooltipType, RelicData.RelicLoreTooltipStyle>
    {
        // Token: 0x06000106 RID: 262 RVA: 0x00005CDC File Offset: 0x00003EDC
        public PonyRelicTooltipType(string localizationKey, int? ID = null) : base(localizationKey, ID ?? PonyRelicTooltipType.GetNewTooltipTypeGUID())
        {
        }

        // Token: 0x06000107 RID: 263 RVA: 0x00005D20 File Offset: 0x00003F20
        public static int GetNewTooltipTypeGUID()
        {
            PonyRelicTooltipType.NumTypes++;
            return PonyRelicTooltipType.NumTypes;
        }

        // Token: 0x06000108 RID: 264 RVA: 0x00005D44 File Offset: 0x00003F44
        public static implicit operator PonyRelicTooltipType(RelicData.RelicLoreTooltipStyle tooltipType)
        {
            return ExtendedEnum<PonyRelicTooltipType, RelicData.RelicLoreTooltipStyle>.Convert(tooltipType);
        }

        //Herzal, Malicka, Heph, and now Pony
        public static int NumTypes = 3;
    }


    [HarmonyPatch(typeof(TooltipDesigner), "GetTooltipDesignData")]
    public static class ICanCallThisClassAnythingIWantToAndNobodyCanStopMeButSomebodyReallyShould
    {
        public static bool Prefix(ref TooltipDesigner.TooltipDesignType tooltipDesignType, ref TooltipDesigner.TooltipDesignData __result)
        {
            if (!Ponies.EquestrianClanIsInit) { return true; }

            if (!(tooltipDesignType == Ponies.PonyLoreTooltip.GetEnum())) { return true; }

            __result = PonyLoreTooltipType.ponyTooltipDesignData;

            return false;
        }
    }

    [HarmonyPatch(typeof(TooltipGenerator), "GetTooltipDesignType")]
    public static class SeriouslyIAmLikeMadWithPowerHereAndItIsSuchATripYouHaveNoIdea
    {
        public static void Postfix(ref RelicData.RelicLoreTooltipStyle tooltipStyle, ref TooltipDesigner.TooltipDesignType __result)
        { 
            if (!Ponies.EquestrianClanIsInit) { return; }

            if (tooltipStyle == Ponies.PonyRelicTooltip.GetEnum()) 
            {
                __result = Ponies.PonyLoreTooltip.GetEnum();
            }
        }
    }

    [HarmonyPatch(typeof(CardTooltipContainer), "AddCardLoreTooltips")]
    public static class Muahahahahahahahahahahahahahahahahahahahahahahahahahahahaha 
    { 
        public static bool Prefix(ref CardState cardState, ref CardTooltipContainer __instance, ref CardUI ___parentCardUI) 
        {
            if (!Ponies.EquestrianClanIsInit) { return true; }

            //Ponies.Log(cardState.GetLinkedClassID());
            //Ponies.Log(EquestrianClan.ID);
            //Ponies.Log(CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID());

            //filter for Equestrian Cards
            if (!(cardState.GetLinkedClassID() == CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID()))
            { 
                if (!(ArcadianCompatibility.ArcadianExists)) 
                {
                    return true;
                }
                else if (cardState.GetCardDataID() != CustomCardManager.GetCardDataByID(EquestrianAnalogBase.ID).GetID() && cardState.GetCardDataID() != CustomCardManager.GetCardDataByID(EquestrianAnalogExile.ID).GetID() && cardState.GetCardDataID() != CustomCardManager.GetCardDataByID(VampireFruitBat.ID).GetID()) 
                {
                    return true;
                }
            }

            //Ponies.Log("Pony card detected. Altering lore tooltip style.");

            if (___parentCardUI.GetUIState() != CardUI.CardUIState.Hand)
            {
                foreach (string text in cardState.GetCardLoreTooltipKeys())
                {
                    if (!text.HasTranslation())
                    {
                        Log.Warning(LogGroups.Localization, "Card lore tooltip mising localization. Card = " + cardState.GetAssetName());
                    }
                    else
                    {
                        TooltipContent tooltipContent = new TooltipContent(null, text.Localize(null), Ponies.PonyLoreTooltip.GetEnum(), text);
                        __instance.ShowTooltip(tooltipContent);
                    }
                }
                CharacterData spawnCharacterData = cardState.GetSpawnCharacterData();
                if (spawnCharacterData != null)
                {
                    foreach (string text2 in spawnCharacterData.GetCharacterLoreTooltipKeys())
                    {
                        if (!text2.HasTranslation())
                        {
                            Log.Warning(LogGroups.Localization, "Character lore tooltip mising localization. Character = " + spawnCharacterData.name);
                        }
                        else
                        {
                            TooltipContent tooltipContent2 = new TooltipContent(null, text2.Localize(null), Ponies.PonyLoreTooltip.GetEnum(), text2);
                            __instance.ShowTooltip(tooltipContent2);
                        }
                    }
                }

                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(CharacterTooltipContainer),"SetCharacter")]
    public static class AndHereIAmOnToOfTheWorldNamingMyClassesAnythingIWantToAndItIsAwesome 
    { 
        //While you can add lore tooltips directly to CharacterData, the style defaults to Herzal.
        //Leaving them undefined and 'borrowing' the lore from the spawner card makes it easier to alter the tooltip design style.
        public static void Postfix(ref CharacterState characterState, ref bool enableTooltips, ref CharacterTooltipContainer __instance)
        {
            if (!Ponies.EquestrianClanIsInit) { return; }

            if (PreferencesManager.Instance.LoreTooltipsEnabled)
            {
                List<string> loreTooltips = new List<string> { };

                if (characterState.GetSpawnerCard() != null) 
                { 
                    if (characterState.GetSpawnerCard().GetLinkedClassID() == CustomClassManager.GetClassDataByID(EquestrianClan.ID).GetID()) 
                    {
                        loreTooltips = characterState.GetSpawnerCard().GetCardLoreTooltipKeys();
                    }

                    if (ArcadianCompatibility.ArcadianExists)
                    {
                        if (characterState.GetSpawnerCard().GetCardDataID() == CustomCardManager.GetCardDataByID(VampireFruitBat.ID).GetID())
                        {
                            loreTooltips = characterState.GetSpawnerCard().GetCardLoreTooltipKeys();
                        }
                    }
                }

                foreach (string key in loreTooltips)
                {
                    __instance.ShowTooltip(new TooltipContent(null, key.Localize(null), Ponies.PonyLoreTooltip.GetEnum(), null));
                }
            }
        }
    }
}