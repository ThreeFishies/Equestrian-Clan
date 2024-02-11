using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Trainworks.Builders;
using Trainworks.Managers;
using Trainworks.Enums;
using Trainworks.Constants;
using Equestrian.Init;
using Equestrian;
using Equestrian.CardPools;
using CustomEffects;
using Equestrian.Enhancers;

namespace CustomEffects
{
    public class CustomMutatorEffectAddUpgradeToUnitsForPactShardsAtStartOfRun : RelicEffectBase
    {
        public static int pactShards;
        public static List<CharacterData> targets;
        public static CardUpgradeData upgradeData;
        public static bool SetupCheckNeeded = false;

        public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData) 
        {
            pactShards = relicEffectData.GetParamInt();
            if (pactShards < 0)
            {
                pactShards = 0;
            }

            targets = new List<CharacterData>();
            if (relicEffectData.GetParamCharacters().Count > 0)
            {
                targets = relicEffectData.GetParamCharacters();
            }

            if (relicEffectData.GetParamCardUpgradeData() == null)
            {
                upgradeData = null;
            }
            else
            { 
                upgradeData = relicEffectData.GetParamCardUpgradeData();
            }

            base.Initialize(relicState, relicData, relicEffectData);
        }

        public static void ApplyEffectAtStartOfRun()
        {
            //Ponies.Log("Line 52");

            if (!SetupCheckNeeded)
            {
                //Ponies.Log("Line 56");
                return;
            }

            //Ponies.Log("Line 60");

            if (pactShards > 0)
            {
                //Ponies.Log("Line 64");
                AddPactShards(pactShards);
            }

            //Ponies.Log("Line 68");

            if (upgradeData == null)
            {
                Ponies.Log("Upgrade failed because it's null.");
                return;
            }

            //Ponies.Log("Line 76");

            List<CardState> deck = ProviderManager.SaveManager.GetDeckState();

            if (deck.Count <= 0)
            {
                Ponies.Log("Player has no cards.");
                return;
            }

            CardUpgradeState upgradeState = new CardUpgradeState();
            upgradeState.Setup(upgradeData);

            foreach (CardState card in deck)
            {
                if (card.IsSpawnerCard())
                {
                    if (targets.Contains(card.GetSpawnCharacterData()))
                    {
                        Ponies.Log("Upgrading card: " + card.GetCardDataID() + " with upgrade: " + upgradeState.GetCardUpgradeDataId());
                        card.Upgrade(upgradeState, ProviderManager.SaveManager, true);
                    }
                }
            }

            SetupCheckNeeded = false;
        }

        public static void AddPactShards(int pactShards)
        {
            ProviderManager.SaveManager.GetDlcSaveData<HellforgedSaveData>(ShinyShoe.DLC.Hellforged).AddCrystals(pactShards);
        }
    }
}