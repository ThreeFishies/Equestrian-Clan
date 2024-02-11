using System;
using System.Collections;
using System.Collections.Generic;
using HarmonyLib;
using ShinyShoe;
using ShinyShoe.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Trainworks.Managers;
using Trainworks.Constants;
using Equestrian.MonsterCards;
using Equestrian.HarmonyPatches;
using Equestrian.Init;
using Equestrian.CardPools;
using Equestrian.Enhancers;
using GivePony;

namespace Equestrian.Util
{ 
    public static class CustomClassSelectionUIManager 
    {
        public static int MaxButtons = 6; //Not including the arrow buttons. With them, the total is 8.
        public static int MainOffset = 0;
        public static int SubOffset = 0;
        public static int TotalButtons = 0;

        public static GameUISelectableButton mainRightButton;
        public static GameUISelectableButton mainLeftButton;
        public static GameUISelectableButton subRightButton;
        public static GameUISelectableButton subLeftButton;

        public static void InitButtons(ClassSelectionScreen classSelectionScreen)
        {
            if (mainRightButton != null) { return; }
            TotalButtons = ProviderManager.SaveManager.GetBalanceData().GetClassDatas().Count + 1;
            if (TotalButtons <= MaxButtons + 2) { return; }

            CovenantProgressUI covenantProgress = AccessTools.Field(typeof(ClassSelectionScreen), "covenantProgressUI").GetValue(classSelectionScreen) as CovenantProgressUI;
            GameUISelectableButton RightBase = AccessTools.Field(typeof(CovenantSelectionUI), "levelUpButton").GetValue(covenantProgress.SelectionUI) as GameUISelectableButton;
            GameUISelectableButton LeftBase = AccessTools.Field(typeof(CovenantSelectionUI), "levelDownButton").GetValue(covenantProgress.SelectionUI) as GameUISelectableButton;

            mainRightButton = ClassSelectionArrowButton.Create(classSelectionScreen, RightBase, "Main Class Right Button", true, true, true);
            mainLeftButton = ClassSelectionArrowButton.Create(classSelectionScreen, LeftBase, "Main Class Left Button", false, true, false);
            subRightButton = ClassSelectionArrowButton.Create(classSelectionScreen, RightBase, "Sub Class Right Button", true, false, true);
            subLeftButton = ClassSelectionArrowButton.Create(classSelectionScreen, LeftBase, "Sub Class Left Button", false, false, false);
        }

        public static void RefreshUI(ClassSelectionScreen classSelectionScreen, bool isMain) 
        {
            ClassSelectionIconUI classSelectionUI;
            int currentOffset;

            if (isMain)
            {
                classSelectionUI = AccessTools.Field(typeof(ClassSelectionScreen), "mainClassSelectionUI").GetValue(classSelectionScreen) as ClassSelectionIconUI;
                currentOffset = MainOffset;
            }
            else 
            {
                classSelectionUI = AccessTools.Field(typeof(ClassSelectionScreen), "subClassSelectionUI").GetValue(classSelectionScreen) as ClassSelectionIconUI;
                currentOffset = SubOffset;
            }

            if (currentOffset <= 0) 
            { 
                if (isMain) 
                { 
                    mainLeftButton.SetState(GameUISelectableButton.State.Disabled);
                    mainRightButton.SetState(GameUISelectableButton.State.Enabled);
                }
                else 
                {
                    subLeftButton.SetState(GameUISelectableButton.State.Disabled);
                    subRightButton.SetState(GameUISelectableButton.State.Enabled);
                }
            }
            else if (currentOffset >= TotalButtons - MaxButtons) 
            {
                if (isMain)
                {
                    mainLeftButton.SetState(GameUISelectableButton.State.Enabled);
                    mainRightButton.SetState(GameUISelectableButton.State.Disabled);
                }
                else
                {
                    subLeftButton.SetState(GameUISelectableButton.State.Enabled);
                    subRightButton.SetState(GameUISelectableButton.State.Disabled);
                }
            }
            else 
            {
                if (isMain)
                {
                    mainLeftButton.SetState(GameUISelectableButton.State.Enabled);
                    mainRightButton.SetState(GameUISelectableButton.State.Enabled);
                }
                else
                {
                    subLeftButton.SetState(GameUISelectableButton.State.Enabled);
                    subRightButton.SetState(GameUISelectableButton.State.Enabled);
                }
            }

            List<ClassSelectionIcon> classIcons = AccessTools.Field(typeof(ClassSelectionIconUI), "classIcons").GetValue(classSelectionUI) as List<ClassSelectionIcon>;

            if (classIcons.Count != TotalButtons) { TotalButtons = classIcons.Count; }

            for (int ii = 0; ii < TotalButtons; ii++) 
            { 
                if (ii < currentOffset || ii >= currentOffset + MaxButtons)
                {
                    classIcons[ii].gameObject.SetActive(false);
                }
                else
                {
                    classIcons[ii].gameObject.SetActive(true);
                }
            }
        }

        /*
        public static void LogSoundManagerCues(SoundManager soundManager) 
        {
            Ponies.Log("========== Starting dump of SoundManager Cues ===========");

            if (soundManager != null) 
            {
                CoreAudioSystem audioSystem;
                CoreAudioSystemData AudioSystemData;
                CoreSoundEffectData GlobalSoundEffectData;

                //List<string> list = AccessTools.Field(typeof(SoundManager), "playedSoundCues").GetValue(soundManager) as List<string>;

                audioSystem = AccessTools.Field(typeof(SoundManager), "audioSystem").GetValue(soundManager) as CoreAudioSystem;

                if (audioSystem != null)
                {
                    AudioSystemData = AccessTools.Field(typeof(CoreAudioSystem), "AudioSystemData").GetValue(audioSystem) as CoreAudioSystemData;

                    if (AudioSystemData != null)
                    {
                        GlobalSoundEffectData = AudioSystemData.GlobalSoundEffectData;

                        if (GlobalSoundEffectData != null)
                        {
                            foreach (CoreSoundEffectData.SoundCueDefinition soundCueDefinition in GlobalSoundEffectData.Sounds)
                            {
                                Ponies.Log(soundCueDefinition.Name);
                            }
                        }
                        else
                        {
                            Ponies.Log("Attempt to retrieve Sound Effect Data failed.");
                        }
                    }
                    else
                    {
                        Ponies.Log("Attempt to retrieve Core Auio System Data failed.");
                    }
                }
                else 
                {
                    Ponies.Log("Attempt to retrieve Core Audio System failed.");
                }
            }
            else 
            {
                Ponies.Log("Failed to locate SoundManager");
            }

            Ponies.Log("============= End dump of SoundManager Cues =============");
        }
        */
    }    

    [HarmonyPatch(typeof(ClassSelectionScreen), "Initialize")]
    public static class SetupButtonsForClassSelectionScreen
    {
        private static void Postfix(ClassSelectionScreen __instance, SoundManager ___soundManager)
        {
            CustomClassSelectionUIManager.InitButtons(__instance);
            CustomClassSelectionUIManager.RefreshUI(__instance, true);
            CustomClassSelectionUIManager.RefreshUI(__instance, false);

            //CustomClassSelectionUIManager.LogSoundManagerCues(___soundManager);
            //Output:

            // ========== Starting dump of SoundManager Cues ===========
            // UI_PlayButtonSelected_Normal
            // BattleWinStinger
            // BattleLoseStinger
            // Combat_Spawn
            // Combat_Death
            // Combat_Attack
            // UI_PlayCard
            // UI_PlayCardDeal
            // UI_PlayCardShuffle
            // UI_EndTurn
            // UI_StartGame
            // UI_HighlightNormal
            // UI_Cancel
            // Combat_Heal
            // Combat_Ascend
            // Combat_Buff
            // Combat_Debuff
            // Combat_TrainTakeDamage
            // TrainIdleLoop
            // UI_BattleIntroCharacterAppear
            // UI_BattleIntroFightButton
            // UI_BattleIntroStart
            // UI_PlayButtonSelected_HUD
            // UI_PlayButtonSelected_Normal
            // UI_PlayCardDeselect
            // UI_PlayInvalidCard
            // UI_Purchase
            // UI_RoomScroll
            // UI_CardTargeting
            // UI_PlayCardSelect
            // Status_Rage
            // Status_Armor
            // Status_Multistrike
            // Status_Spikes
            // Status_Regen
            // Credits_Start
            // UI_BattleMode_TimerWarning
            // Multiplayer_Emote_Angry
            // Multiplayer_Emote_Sad
            // Multiplayer_Emote_Hmm
            // Multiplayer_Emote_Love
            // Multiplayer_Emote_Lol
            // Status_Rooted
            // Status_Stealth
            // Status_Dazed
            // Status_Quick
            // Status_Burnout
            // Status_Endless
            // Status_Frostbite
            // Status_Sap
            // Status_Lifesteal
            // Status_DamageShield
            // Status_Emberdrain
            // UI_Logbook_Open
            // UI_Logbook_Close
            // UI_Logbook_PageTurn
            // UI_Logbook_Tab
            // UI_Event_TrainStart
            // UI_Event_TrainStop
            // UI_Event_WriteScribble
            // UI_MultiplayerNotif
            // Node_Banner
            // Node_Chest
            // Node_ChooseSide
            // Node_Forge
            // Node_Merchant
            // Node_Temple
            // Node_Vortex
            // UI_CoinsLarge
            // UI_CrystalLevel_Cracked_1
            // UI_CrystalLevel_Cracked_2
            // UI_CrystalLevel_Cracked_3
            // UI_CrystalLevel_Cracked_4
            // UI_CrystalLevel_Cracked_5
            // UI_CrystalLevel_Cracked_6
            // UI_CrystalLevel_Cracked_7
            // UI_CrystalLevel_Cracked_8
            // UI_CrystalLevel_Perfect_1
            // UI_CrystalLevel_Perfect_2
            // UI_CrystalLevel_Perfect_3
            // UI_CrystalLevel_Perfect_4
            // UI_CrystalLevel_Perfect_5
            // UI_CrystalLevel_Perfect_6
            // UI_CrystalLevel_Perfect_7
            // UI_CrystalLevel_Perfect_8
            // UI_PointsTicker
            // UI_PointsTickerFinal
            // Node_ChooseSide
            // Node_Battle
            // Node_BossBattle
            // Node_Coins
            // Node_Event
            // Node_PyreRepair
            // UI_MultiplayerJoin
            // UI_Skip
            // Forge_Upgrade
            // Merchant_ChatterAnnoyed
            // Merchant_ChatterShort
            // Merchant_ChatterLong
            // Merchant_Leave
            // Merchant_Prepurchase
            // Temple_Brand
            // UI_LogbookClanTab
            // UI_LogbookFilterOn
            // UI_LogbookFilterOff
            // UI_WorldThaw
            // Collect_Health
            // UI_CoinsSmall
            // UI_CoinsMedium
            // UI_CancelLight
            // UI_TrialOn
            // Flame_Ambience
            // Collect_Blessing
            // Collect_CardRare
            // Collect_Card
            // UI_Click
            // UI_ClickHeavy
            // Collect_Purge
            // Collect_Duplicate
            // UI_CardHoverLoop
            // ClanSelect_Hellhorned
            // ClanSelect_Awoken
            // ClanSelect_Remnant
            // ClanSelect_Stygian
            // ClanSelect_Umbra
            // UI_HighlightVeryLight
            // UI_HighlightLight
            // UI_HighlightHeavy
            // Combat_RoomDestroy
            // Combat_SpawnBoss
            // Combat_SpawnMiniboss
            // UI_HellRoyale
            // UI_ClickLight
            // UI_ClickVeryLight
            // UI_ClickVeryHeavy
            // UI_CancelVeryLight
            // UI_CancelHeavy
            // UI_StatBoom
            // Combat_RoomTargetLoop
            // Status_SpellWeakness
            // Unlock_Stinger
            // Blacksmith_ChatterShort
            // Blacksmith_ChatterAnnoyed
            // Blacksmith_ChatterLong
            // Blacksmith_ChatterConfirm
            // Train_Whistle
            // Status_Silence
            // Upgrade_Stat_0
            // Upgrade_Stat_1
            // Upgrade_Stat_2
            // Upgrade_Stat_3
            // Upgrade_Stat_Attack
            // Upgrade_Stat_Capacity
            // Upgrade_Stat_Health
            // Upgrade_Stat_Cost
            // UI_PyreIgnite
            // PyreDestroy
            // UI_BattleMessage
            // CameraZoom
            // UI_EmberGain
            // Seraph_Intro
            // Merchant_Purchase
            // UI_Prepurchase
            // HUD_PyreDamage
            // HUD_PyreHeal
            // UI_MainMenu_Select
            // Node_Hellvent
            // Card_Enhance_Faster
            // ClanSelect_Hellhorned_Exile
            // ClanSelect_Awoken_Exile
            // ClanSelect_Remnant_Exile
            // ClanSelect_Stygian_Exile
            // ClanSelect_Umbra_Exile
            // UI_PreHellforgedBoss
            // UI_PostHellforgedBoss
            // ClanSelect_Wurmkin_Exile
            // ClanSelect_Wurmkin
            // Divinity_Intro
            // Node_CoinsPact
            // Node_ChestPact
            // Temple_Purchase
            // Temple_PrePurchase
            // UI_TLD_On
            // ============= End dump of SoundManager Cues =============
        }
    }
}