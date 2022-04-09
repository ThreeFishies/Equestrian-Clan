using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Trainworks.Builders;
using Trainworks.Managers;
using Trainworks.Constants;
using System.Linq;
using UnityEngine;
using Trainworks.Utilities;
using Equestrian.Init;
using CustomEffects;
using Equestrian;
using Equestrian.MonsterCards;
using Equestrian.SpellCards;

namespace Equestrian.CardPools 
{
    public class MyCardPools
    {
        public static CardPool GivePonyCardPool = ScriptableObject.CreateInstance<CardPool>();
        public static CardPool MareALeeMentorIIICardPool = ScriptableObject.CreateInstance<CardPool>();
        public static CardPool BackgroundPonyCardPool = ScriptableObject.CreateInstance<CardPool>();
        public static CardPool FilliesCardPool = ScriptableObject.CreateInstance<CardPool>();
        public static CardPool YoLoCardPool = ScriptableObject.CreateInstance<CardPool>();
        public static CardPool FinchyCardPool = ScriptableObject.CreateInstance<CardPool>();
        public static CardPool CrunchieMunchieCardPool = ScriptableObject.CreateInstance<CardPool>();
        public static CardPool PsychosisFilteredCards = ScriptableObject.CreateInstance<CardPool>();
        public static CardPool DeadweightOnlyCardPool = ScriptableObject.CreateInstance<CardPool>();
        public static CardPool NightTerrorsPool = ScriptableObject.CreateInstance<CardPool>();
        public static CardPool FixArtCardPool = ScriptableObject.CreateInstance<CardPool>();
        public static CardPool FlowerPoniesPool = ScriptableObject.CreateInstance<CardPool>();
        //public static CardPool RefMegaPool = ScriptableObject.CreateInstance<CardPool>();
        //public static CardPool RefAllBannersPool = ScriptableObject.CreateInstance<CardPool>();
        public static void DoCardPoolStuff()
        {
            GivePonyCardPool = new CardPoolBuilder
            {
                CardPoolID = Ponies.GUID + "_GivePonyCardPool",
                CardIDs = new List<string>
                {
                    //CustomCardManager.GetCardDataByID(MareYouKnow.ID).GetID(),
                    CustomCardManager.GetCardDataByID(MistyStep.ID).GetID(),
                    //CustomCardManager.GetCardDataByID(PreenySnuggle.ID).GetID(),
                    CustomCardManager.GetCardDataByID(Snackasmacky.ID).GetID(),
                    CustomCardManager.GetCardDataByID(SqueakyBooBoo.ID).GetID(),
                    CustomCardManager.GetCardDataByID(StaticJoy.ID).GetID(),
                    CustomCardManager.GetCardDataByID(YoLo.ID).GetID(),
                    CustomCardManager.GetCardDataByID(Finchy.ID).GetID(),
                    CustomCardManager.GetCardDataByID(CrunchieMunchie.ID).GetID(),
                }
            }.BuildAndRegister();

            MareALeeMentorIIICardPool = new CardPoolBuilder
            {
                CardPoolID = Ponies.GUID + "_MareALeeMentorIIICardPool",
                CardIDs = new List<string>
                {
                    //CustomCardManager.GetCardDataByID(MareYouKnow.ID).GetID(),
                    CustomCardManager.GetCardDataByID(MistyStep.ID).GetID(),
                    //CustomCardManager.GetCardDataByID(PreenySnuggle.ID).GetID(),
                    CustomCardManager.GetCardDataByID(Snackasmacky.ID).GetID(),
                    CustomCardManager.GetCardDataByID(SqueakyBooBoo.ID).GetID(),
                    CustomCardManager.GetCardDataByID(StaticJoy.ID).GetID(),
                }
            }.BuildAndRegister();

            BackgroundPonyCardPool = new CardPoolBuilder
            {
                CardPoolID = Ponies.GUID + "_BaackgroundPonyCardPool",
                CardIDs = new List<string>
                {
                    CustomCardManager.GetCardDataByID(BackgroundPony.ID).GetID(),
                }
            }.BuildAndRegister();

            FilliesCardPool = new CardPoolBuilder
            {
                CardPoolID = Ponies.GUID + "_FilliesCardPool",
                CardIDs = new List<string>
                {
                    CustomCardManager.GetCardDataByID(YoLo.ID).GetID(),
                    CustomCardManager.GetCardDataByID(Finchy.ID).GetID(),
                    CustomCardManager.GetCardDataByID(CrunchieMunchie.ID).GetID(),
                }
            }.BuildAndRegister();

            YoLoCardPool = new CardPoolBuilder
            {
                CardPoolID = Ponies.GUID + "_YoLoCardPool",
                CardIDs = new List<string>
                {
                    CustomCardManager.GetCardDataByID(YoLo.ID).GetID(),
                }
            }.BuildAndRegister();

            FinchyCardPool = new CardPoolBuilder
            {
                CardPoolID = Ponies.GUID + "_FinchyCardPool",
                CardIDs = new List<string>
                {
                    CustomCardManager.GetCardDataByID(Finchy.ID).GetID(),
                }
            }.BuildAndRegister();

            CrunchieMunchieCardPool = new CardPoolBuilder
            {
                CardPoolID = Ponies.GUID + "_CrunchieMunchieCardPool",
                CardIDs = new List<string>
                {
                    CustomCardManager.GetCardDataByID(CrunchieMunchie.ID).GetID(),
                }
            }.BuildAndRegister();

            PsychosisFilteredCards = new CardPoolBuilder
            {
                CardPoolID = Ponies.GUID + "_PsychosisFilteredCards",
                CardIDs= new List<string> 
                {
                    VanillaCardIDs.UnleashtheWildwood,
                    VanillaCardIDs.AdaptiveMutation
                }
            }.BuildAndRegister();

            NightTerrorsPool = new CardPoolBuilder
            {
                CardPoolID = Ponies.GUID + "_NightTerrorsPool",
                CardIDs = new List<string>
                {
                    CustomCardManager.GetCardDataByID(NightTerrors.ID).GetID(),
                }
            }.BuildAndRegister();

            DeadweightOnlyCardPool = new CardPoolBuilder
            {
                CardPoolID = Ponies.GUID + "_DeadweightOnlyCardPool",
                CardIDs = new List<string>
                {
                    CustomCardManager.GetCardDataByID(VanillaCardIDs.Deadweight).GetID(),
                }
            }.BuildAndRegister();

            FixArtCardPool = new CardPoolBuilder()
            {
                CardPoolID = Ponies.GUID + "_FixArtCardPool",
                Cards = GivePonyCardPool.GetAllChoices(),
            }.BuildAndRegister();

            FlowerPoniesPool = new CardPoolBuilder()
            {
                CardPoolID = Ponies.GUID + "_FlowerPoniesPool",
                CardIDs = new List<string>
                {
                    CustomCardManager.GetCardDataByID(TheElementsOfHarmony.ID).GetID(),
                    CustomCardManager.GetCardDataByID(EquestrianRailspike.ID).GetID(),
                    CustomCardManager.GetCardDataByID(MissingMare.ID).GetID(),
                }
            }.BuildAndRegister();

            /*
            RefMegaPool = new CardPoolBuilder
            {
                CardPoolID = VanillaCardPoolIDs.MegaPool,
                CardIDs = new List<string>
                {
                }
            }.BuildAndRegister();

            RefAllBannersPool = new CardPoolBuilder
            {
                CardPoolID = VanillaCardPoolIDs.UnitsAllBanner,
                CardIDs = new List<string>
                {
                }
            }.BuildAndRegister();
            */
        }
    }
}
