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
using Equestrian.HarmonyPatches;

namespace Equestrian.Mutators
{
    public static class YouveGotMail
    {
        public static string ID = Ponies.GUID + "_YouveGotMail";
        public static MutatorData mutatorData;
        public static List<CardData> mailSpells = new List<CardData> { };
        public static CardPool refMegaPool = null;
        public static RelicEffectParams tempRelicEffectParams;
        public static bool waitForLoading = false;
        public static int TestPoolEnumerator = 2;

        public static CardData GetTestCard() 
        {
            TestPoolEnumerator++;
            if (TestPoolEnumerator > 10) { TestPoolEnumerator = 0; }

            switch (TestPoolEnumerator) 
            { 
                case 0: return CustomCardManager.GetCardDataByID("4cf1b860-5873-45ff-95aa-a583d355f344"); //preserved thorns
                case 1: return CustomCardManager.GetCardDataByID("61db7d04-3db6-4780-82e9-8f4d7fc2b56c"); //imp in a box
                case 2: return CustomCardManager.GetCardDataByID("a27a5954-d63a-4b7c-9cff-308aa6a91314"); //sacrificial ressurection
                case 3: return CustomCardManager.GetCardDataByID("fe73da55-8250-4f96-b3e0-91c19b3e60de"); //Antumbra Assault
                case 4: return CustomCardManager.GetCardDataByID("89605a1e-342e-401d-8f54-396ec12a7ede"); //Making of a Morsel
                case 5: return CustomCardManager.GetCardDataByID("fab34a33-f0c3-41b2-8e7a-d099bb870fc4"); //Packed Morsels
                case 6: return CustomCardManager.GetCardDataByID("379000d5-0658-40f3-be48-3bf5572330b2"); //plink
                case 7: return CustomCardManager.GetCardDataByID("d96af7f1-0efc-4859-9077-83618b9176dd"); //shadesplitter
                case 8: return CustomCardManager.GetCardDataByID("34d8ec74-19ff-498a-b51d-821e7f65cf27"); //ember cache
                case 9: return CustomCardManager.GetCardDataByID("cf85905c-6810-461a-a196-a73e51fbaf2f"); //Gem trove
                case 10: return CustomCardManager.GetCardDataByID("70dc3602-861d-4a41-96d6-d13880835dbb"); //Grovel
                default: return null;
            }
        }

        /*
        public static void NeedFixArt() 
        {
            if (HasMutator()) 
            { 
                FixArt.TryYetAnotherFix(mailSpells, ShinyShoe.Loading.LoadingScreen.DisplayStyle.FullScreen);
            }
        }
        */

        public static bool HasMutator() 
        {
            if (ProviderManager.SaveManager.GetMutatorCount() > 0)
            {
                foreach (MutatorState mutator in ProviderManager.SaveManager.GetMutators())
                {
                    if (mutator.GetRelicDataID() == ID)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static CardPool GetMegaPool()
        {
            if (refMegaPool != null)
            {
                return refMegaPool;
            }

            refMegaPool = UnityEngine.ScriptableObject.CreateInstance<CardPool>();

            StoryEventData _CardSwapper = ProviderManager.SaveManager.GetAllGameData().FindStoryEventData("907a7cfb-3547-4215-9d04-5c4f6465ba62");

            if (_CardSwapper == null)
            {
                Ponies.Log("Failed to retrieve CardSwapper event data.");
                return refMegaPool;
            }

            foreach (RewardData rewardData in _CardSwapper.GetPossibleRewards()) 
            {
                if (rewardData.GetID() == "ed7601f8-149e-4ec1-bcf3-662aac344106")
                {
                    refMegaPool = (CardPool)AccessTools.Field(typeof(CardPoolRewardData), "cardPool").GetValue((CardPoolRewardData)rewardData);
                    return refMegaPool;
                }
            }

            Ponies.Log("Failed to find MegaPool");
            return refMegaPool;
        }

        public static bool SetupMail(SaveManager saveManager)
        {
            //Ponies.Log("Has main class: " + saveManager.HasMainClass());

            if (!saveManager.HasMainClass()) 
            { 
                return false; 
            }

            //Ponies.Log("Main class: " + saveManager.GetMainClass().name);
            //Ponies.Log("Sub class: " + saveManager.GetSubClass().name);
            //Ponies.Log("Is DLC enabled when starting run: " + saveManager.IsDlcAvailableWhenStartingRun(DLC.Hellforged));
            //Ponies.Log("Megapool " + GetMegaPool().name);

            mailSpells.Clear();

            List<ClassData> allClasses = saveManager.GetAllGameData().GetAllClassDatas();

            foreach (ClassData classData in allClasses)
            {
                //Ponies.Log("Class " + classData.name);

                if (classData != saveManager.GetMainClass() && classData != saveManager.GetSubClass())
                {
                    //Ponies.Log("Class " + classData.name + " is neither main nor sub class.");
                    if (saveManager.IsUnlockedAndAvailableWhenStartingRun(classData))
                    {
                        //Ponies.Log("Class " + classData.name + " and is unlocked and available.");

                        //This specific method is patched by Trainworks to also return custom clan cards.
                        List<CardData> possibleCards = CardPoolHelper.GetCardsForClass(GetMegaPool(), classData, saveManager.GetClassLevel(classData.GetID()), CollectableRarity.Common, saveManager, null, false);

                        if (possibleCards != null && possibleCards.Count > 0)
                        {
                            //Ponies.Log("Got card list.");

                            foreach (CardData card in possibleCards)
                            {
                                if (card.GetCardType() == CardType.Spell)
                                {
                                    if (saveManager.GetClassLevel(classData.GetID()) >= card.GetUnlockLevel())
                                    {
                                        //A clan level check is needed because Trainworks skips the ulock level check when returning custom clan cards.
                                        //Ponies.Log("Card: " + card.name);

                                        mailSpells.Add(card);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //Ponies.Log(mailSpells.Count + " cards are in the mail pool.");
            return true;
        }

        public static void BuildAndRegister() 
        {
            mutatorData = new MutatorDataBuilder
            {
                ID = ID,
                NameKey = "Pony_Mutator_YouveGotMail_Name_Key",
                DescriptionKey = "Pony_Mutator_YouveGotMail_Description_Key",
                RelicActivatedKey = "Pony_Mutator_YouveGotMail_Activated_Key",
                RelicLoreTooltipKeys = new List<string>()
                {
                    "Pony_Mutator_YouveGotMail_Lore_Key"
                },
                DisableInDailyChallenges = false,
                DivineVariant = false,
                BoonValue = 2,
                RequiredDLC = DLC.None,
                IconPath = "Mutators/Sprite/MTR_YouveGotMail.png",
                Tags = new List<string>
                {
                },
                Effects = new List<RelicEffectData>
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassName = typeof(CustomMutatorEffectAddMailBattleCardToHand).AssemblyQualifiedName,
                        ParamSourceTeam = Team.Type.None,
                        ParamTrigger = CharacterTriggerData.Trigger.PreCombat,
                        ParamTargetMode = TargetMode.FrontInRoom,
                        ParamBool = false,
                        ParamInt = 0,
                        ParamCharacterSubtype = "SubtypesData_None",
                        ParamStatusEffects = new StatusEffectStackData[]{},
                        ParamCardPool = GetMegaPool(),

                        ParamCardUpgradeData = new CardUpgradeDataBuilder
                        {
                            UpgradeTitle = "YouveGotMailMutatorPurgeTrait",
                            TraitDataUpgradeBuilders = new List<CardTraitDataBuilder>
                            {
                                new CardTraitDataBuilder
                                {
                                    TraitStateName = "CardTraitSelfPurge",
                                }
                            },
                            Filters = new List<CardUpgradeMaskData>
                            {
                                new CardUpgradeMaskDataBuilderFixed
                                {
                                    CardType = CardType.Spell,
                                }.Build(),
                            }
                        }.Build(),

                        AdditionalTooltips = new AdditionalTooltipData[]
                        {
                            new AdditionalTooltipData
                            {
                                titleKey = "CardTraitSelfPurge_CardText",
                                descriptionKey = "CardTraitSelfPurge_TooltipText",
                                style = TooltipDesigner.TooltipDesignType.Keyword,
                                isStatusTooltip = false,
                                statusId = "",
                                isTriggerTooltip = false,
                                trigger = CharacterTriggerData.Trigger.OnDeath,
                                isTipTooltip = false
                            },
                            //new AdditionalTooltipData
                            //{
                            //    titleKey = "Pony_Mutator_YouveGotMail_Tip_Title_Key",
                            //    descriptionKey = "Pony_Mutator_YouveGotMail_Tip_Description_Key",
                            //    style = TooltipDesigner.TooltipDesignType.Default,
                            //    isStatusTooltip = false,
                            //    statusId = "",
                            //    isTriggerTooltip = false,
                            //    trigger = CharacterTriggerData.Trigger.OnDeath,
                            //    isTipTooltip = false
                            //}
                        }
                    }.Build(),
                }
            }.BuildAndRegister();
        }
    }
}