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
using Equestrian.Arcadian;

namespace Equestrian.HarmonyPatches
{
    /// <summary>
    /// This patch bypasses the need for unit synthesis mapping. It tends to be more reliable. Just ensure that data is defined when this patch is called, because other mods may load first and interact with this first. (Unofficial Balance Patch being one.)
    /// </summary>
    [HarmonyPatch(typeof(UnitSynthesisMapping), "GetUpgradeData")]
    public static class UnitSynthesisMappingIsBorkedAgainPleaseFixIt
    {
        public static void Postfix(ref CardUpgradeData __result, ref CharacterData characterData)
        {
            //This prevents a crash that can occur if this function is called before the Equestrian Clan is initialized.
            if (!Ponies.EquestrianClanIsInit) { return; }

            if (characterData == null) { return; }

            if (characterData.IsChampion()) { return; }

            if (__result != null) { return; }

            if (Ponies.AttemptAResetFirst) 
            {
                Ponies.AttemptAResetFirst = false;
                AccessTools.Field(typeof(UnitSynthesisMapping), "_dictionaryMapping").SetValue(ProviderManager.SaveManager.GetBalanceData().SynthesisMapping, null);
                Trainworks.Patches.AccessUnitSynthesisMapping.FindUnitSynthesisMappingInstanceToStub();
                __result = ProviderManager.SaveManager.GetBalanceData().SynthesisMapping.GetUpgradeData(characterData);
                if (__result != null)
                {
                    Ponies.AttemptAResetFirst = true;
                    return;
                }
            }

            /*
            if (!Ponies.UnitSynthesisMappingIsBorkedAgain) 
            { 
                if (characterData.GetSubtypes().Count > 0) 
                { 
                    foreach (SubtypeData subtype in characterData.GetSubtypes()) 
                    { 
                        if (subtype.Key == Ponies.GUID + "_Subtype_Pony") 
                        {
                            //Ponies.Log("Synthesis mapping is borked. Attempting manual fix.");
                            Ponies.UnitSynthesisMappingIsBorkedAgain = true;
                        }
                        if (subtype.Key == Ponies.GUID + "_Subtype_Herb")
                        {
                            //Ponies.Log("Synthesis mapping is borked. Attempting manual fix.");
                            Ponies.UnitSynthesisMappingIsBorkedAgain = true;
                        }
                        if (subtype.Key == Ponies.GUID + "_Subtype_Pet")
                        {
                            //Ponies.Log("Synthesis mapping is borked. Attempting manual fix.");
                            Ponies.UnitSynthesisMappingIsBorkedAgain = true;
                        }
                        if (subtype.Key == Ponies.GUID + "_Subtype_Dragon")
                        {
                            //Ponies.Log("Synthesis mapping is borked. Attempting manual fix.");
                            Ponies.UnitSynthesisMappingIsBorkedAgain = true;
                        }
                    }
                }
            }*/

            //if (!Ponies.UnitSynthesisMappingIsBorkedAgain) { return; }

            //It probably would have made more sense to put this in a dictionary-style list instead of adding each check individually.
            //It'd be a bit silly to change it now, though.

            if (characterData.GetID() == CustomCharacterManager.GetCharacterDataByID(BackgroundPony.CharID).GetID())
            {
                __result = BackgroundPony.BackgroundPonySynthesis;
                return;
            }
            if (characterData.GetID() == CustomCharacterManager.GetCharacterDataByID(Carrot.CharID).GetID())
            {
                __result = Carrot.CarrotSynthesis;
                return;
            }
            if (characterData.GetID() == CustomCharacterManager.GetCharacterDataByID(CrunchieMunchie.CharID).GetID())
            {
                __result = CrunchieMunchie.CrunchieMunchieSynthesis;
                return;
            }
            if (characterData.GetID() == CustomCharacterManager.GetCharacterDataByID(Finchy.CharID).GetID())
            {
                __result = Finchy.FinchySynthesis;
                return;
            }
            if (characterData.GetID() == CustomCharacterManager.GetCharacterDataByID(GuardianOfTheGates.CharID).GetID())
            {
                __result = GuardianOfTheGates.GuardianOfTheGatesSynthesis;
                return;
            }
            if (characterData.GetID() == CustomCharacterManager.GetCharacterDataByID(HeartsDesire.CharID).GetID())
            {
                __result = HeartsDesire.HeartsDesireSynthesis;
                return;
            }
            if (characterData.GetID() == CustomCharacterManager.GetCharacterDataByID(LordOfEmber.CharID).GetID())
            {
                __result = LordOfEmber.LordOfEmberSynthesis;
                return;
            }
            if (characterData.GetID() == CustomCharacterManager.GetCharacterDataByID(MareYouKnow.CharID).GetID())
            {
                __result = MareYouKnow.MareYouKnowSynthesis;
                return;
            }
            if (characterData.GetID() == CustomCharacterManager.GetCharacterDataByID(MistyStep.CharID).GetID())
            {
                __result = MistyStep.MistyStepSynthesis;
                return;
            }
            if (characterData.GetID() == CustomCharacterManager.GetCharacterDataByID(PoisonJoke.CharID).GetID())
            {
                __result = PoisonJoke.PoisonJokeSynthesis;
                return;
            }
            if (characterData.GetID() == CustomCharacterManager.GetCharacterDataByID(PreenySnuggle.CharID).GetID())
            {
                __result = PreenySnuggle.PreenySnugglesSynthesis;
                return;
            }
            if (characterData.GetID() == CustomCharacterManager.GetCharacterDataByID(Snackasmacky.CharID).GetID())
            {
                __result = Snackasmacky.SnackasmackySynthesis;
                return;
            }
            if (characterData.GetID() == CustomCharacterManager.GetCharacterDataByID(SqueakyBooBoo.CharID).GetID())
            {
                __result = SqueakyBooBoo.SqueakyBooBooSynthesis;
                return;
            }
            if (characterData.GetID() == CustomCharacterManager.GetCharacterDataByID(StaticJoy.CharID).GetID())
            {
                __result = StaticJoy.StaticJoySynthesis;
                return;
            }
            if (characterData.GetID() == CustomCharacterManager.GetCharacterDataByID(TavernAce.CharID).GetID())
            {
                __result = TavernAce.TavernAceSynthesis;
                return;
            }
            if (characterData.GetID() == CustomCharacterManager.GetCharacterDataByID(TrashPanda.CharID).GetID())
            {
                __result = TrashPanda.TrashPandaSynthesis;
                return;
            }
            if (characterData.GetID() == CustomCharacterManager.GetCharacterDataByID(YoLo.CharID).GetID())
            {
                __result = YoLo.YoLoSynthesis;
                return;
            }
            if (characterData.GetID() == CustomCharacterManager.GetCharacterDataByID(MissingMare.CharID).GetID())
            {
                //So if Missing Mare is ever added for real, her essence will look like an error message.
                __result = MissingMare.MissingMareSynthesis;
                return;
            }
            if (ArcadianCompatibility.ArcadianExists) 
            { 
                if (characterData.GetID() == CustomCharacterManager.GetCharacterDataByID(VampireFruitBat.CharID).GetID()) 
                {
                    __result = VampireFruitBat.VampireFruitBatSynthesis;
                    return;
                }
            }

            if (__result == null) 
            {
                //+15 attack, +5 health, and 1 stealth isn't great for an essence, but it's not null and it won't crash the game if applied.
                Trainworks.Trainworks.Log(BepInEx.Logging.LogLevel.Error, "Equestrian Clan: Error! Unit synthesis data is missing for " + characterData.GetName() + ". A default substitute was provided to prevent crashes.");
                __result = MissingMare.MissingMareSynthesis;
            }
        }
    }
}