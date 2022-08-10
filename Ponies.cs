using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using Trainworks.Managers;
using System.Text;
using System.IO;
using UnityEngine;
using StateMechanic;
using Trainworks.AssetConstructors;
using Trainworks.Builders;
using System.Runtime.CompilerServices;
using UnityEngine.AddressableAssets;
using System.Text.RegularExpressions;
using Trainworks.Interfaces;
using Trainworks.Constants;
using Equestrian.SpellCards;
using Equestrian.MonsterCards;
using Equestrian.HarmonyPatches;
//using SynthesisFix;
using ShinyShoe;
using Equestrian;
using Equestrian.Champions;
using Equestrian.Relic;
using Equestrian.CardPools;
using Equestrian.Enhancers;
using Equestrian.Sprites;
using CustomEffects;
using Equestrian.Arcadian;
using Equestrian.PonyStory;
using Equestrian.Mutators;
using Equestrian.Metagame;
using GivePony;

namespace Equestrian.Init
{
    // Credit to Rawsome, Stable Infery for the base of this method.
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess("MonsterTrain.exe")]
    [BepInProcess("MtLinkHandler.exe")]
    [BepInDependency("tools.modding.trainworks", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("ca.chronometry.disciple", BepInDependency.DependencyFlags.SoftDependency)]
    //[BepInIncompatibility("com.rising_dusk.unofficialbalancepatch")]

    public class Ponies : BaseUnityPlugin, IInitializable
    {
        public static Ponies Instance { get; private set; }

        public const string GUID = "mod.equestrian.clan.monstertrain";
        public const string NAME = "Equestrian Clan";
        public const string VERSION = "1.0.0";
        public static ClassData EquestrianClanData;
        public static CardPool BGPonyPool;
        public static EnhancerData FriendstoneData = ScriptableObject.CreateInstance<EnhancerData>();
        //public static bool UnitSynthesisMappingIsBorkedAgain = false;
        public static bool HallowedHallsInEffect = false; //Flag needed by the social mechanic to work around a Hallowed Halls bug.
        //public static SpawnPoint LastMovedUnit = null;
        //public static bool PanicFlag = false;
        public static bool EquestrianClanIsInit = false; //Very important! If a patched method is invoked by another mod before the Equestrian Clan is loaded, this can cause crashes. Always check first to ensure data is loaded first before referencing any pony-specific data.
        public static bool AttemptAResetFirst = true;
        public static PonyFrame PonyFrame; //This is for the clan's card mastery frame
        //public static GivePonyToggle givePonyToggle;

        //In order to make a loading screen work, the clan's loading process was moved from the default Trainworks location of AssetLoadingManager.Start().
        public void Initialize()
        {
            //Init the clan data here so the custom clan helper can find it.

            //Clan
            EquestrianClanData = EquestrianClan.Buildclan();
            Ponies.Log("Build Clan");
        }

        //To LoadingScreen.FadeOutFullScreen(). Note that the background image needs to be prepped earlier, at LoadingScreen.Initialize() or it won't show up.
        public void InitializeHereInstead() 
        {
            //Oh, and change the cursor too, because we can.
            PonyLoader.ChangeCursor();
            Ponies.Log("TempPonyCursor");

            //Character Subtypes
            SubtypeHerb.BuildAndRegister();
            Ponies.Log("RegisterHerbSubtype");
            SubtypePony.BuildAndRegister();
            Ponies.Log("RegisterPonySubtype");
            SubtypePet.BuildAndRegister();
            Ponies.Log("RegisterPetSubtype");
            SubtypeDragon.BuildAndRegister();
            Ponies.Log("RegisterDragonSubtype");
            SubtypeTrap.BuildAndRegister();
            Ponies.Log("RegisterTrapSubtype");

            //Status Effects
            StatusEffectSocial.Make();
            Ponies.Log("StatusEffectSocial");
            StatusEffectMale.Make();
            Ponies.Log("Male");
            StatusEffectFemale.Make();
            Ponies.Log("Female");
            StatusEffectGenderless.Make();
            Ponies.Log("Genderless");
            StatusEffectUndefined.Make();
            Ponies.Log("Undefined");

            //Starter Spell (exile)
            NightTerrors.BuildAndRegister();
            Ponies.Log("Night Terrors");

            //Starter Unit (base)
            BackgroundPony.BuildAndRegister();
            Ponies.Log("Background Pony");
            //BGPonyPool = BackgroundPonyCardPool.GetPool();
            //Ponies.Log("Background Pony Card Pool");

            //Special Units (common, undraftable)
            Carrot.BuildAndRegister();
            Ponies.Log("Carrot");
            TrashPanda.BuildAndRegister();
            Ponies.Log("Trash Panda");
            YoLo.BuildAndRegister();
            Ponies.Log("Yo Lo");
            Finchy.BuildAndRegister();
            Ponies.Log("Finchy");
            CrunchieMunchie.BuildAndRegister();
            Ponies.Log("Crunchie Munchie");

            //Common spells [8 total]
            TimeToShine.BuildAndRegister();
            Ponies.Log("Time To Shine");
            AppleCider.BuildAndRegister();
            Ponies.Log("Apple Cider");
            DressedToKill.BuildAndRegister();
            Ponies.Log("Dressed To Kill");
            Shenanigans.BuildAndRegister();
            Ponies.Log("Shenanigans");
            Reserves.BuildAndRegister();
            Ponies.Log("Reserves");
            PastryWarfare.BuildAndRegister();
            Ponies.Log("Pastry Warfare");
            PartyInvitation.BuildAndRegister();
            Ponies.Log("Party Invitation");
            PackedAudience.BuildAndRegister();
            Ponies.Log("Packed Audience");

            //Uncommon Spells [11 total, +1 unit]
            BuckOff.BuildAndRegister();
            Ponies.Log("Buck Off");
            VIPList.BuildAndRegister();
            Ponies.Log("VIP List");
            PartyCannon.BuildAndRegister();
            Ponies.Log("Party Cannon");
            BlankFlank.BuildAndRegister();
            Ponies.Log("Blank Flank");
            SpaTreatment.BuildAndRegister();
            Ponies.Log("Spa Treatment");
            SpontaneousSongAndDance.BuildAndRegister();
            Ponies.Log("Spontaneous Song and Dance");
            //One of the game's tooltip keys for X-scaling healing was left undefined.
            //Ponies.Log("CardTraitScalingAddDamage_ExtraDamage_XCostOutsideBattle_CardText".Localize(null));
            //Ponies.Log("CardTraitScalingAddDamage_ExtraHealing_XCostOutsideBattle_CardText".Localize(null));
            //fix: <nobr><i>(<{1}>*+{0}*</{1}> healing)</i></nobr>
            Interrogation.BuildAndRegister();
            Ponies.Log("Interrogation");
            //Uncommon spell "Fan Club" initializes after card pool initialization
            WinterWrapUp.BuildAndRegister();
            Ponies.Log("Winter Wrap-Up");
            FirstAid.BuildAndRegister();
            Ponies.Log("First Aid");
            Checklist.BuildAndRegister();
            Ponies.Log("Checklist");

            //Rare Spells [7 total] (one is unit)
            ChangelingInfiltrator.BuildAndRegister();
            Ponies.Log("Changeling Infiltrator");
            RainbowPower.BuildAndRegister();
            Ponies.Log("Rainbow Power");
            EquestrianRailspike.BuildAndRegister();
            Ponies.Log("Equestrian Railspike");
            SecondChance.BuildAndRegister();
            Ponies.Log("Second Chance");
            TheElementsOfHarmony.BuildAndRegister();
            Ponies.Log("The Elements of Harmony");
            //Rare pony spell "Tom" requires a _defined_ card pool and is initialized later.
            Alicornification.BuildAndRegister();
            Ponies.Log("Alicornification");

            //Uncommon units [6 total] +1
            StaticJoy.BuildAndRegister();
            Ponies.Log("Static Joy");
            PreenySnuggle.BuildAndRegister();
            Ponies.Log("Preeny Snuggles");
            Snackasmacky.BuildAndRegister();
            Ponies.Log("Snackasmacky");
            MareYouKnow.BuildAndRegister();
            Ponies.Log("Mare You Know");
            MistyStep.BuildAndRegister();
            Ponies.Log("Misty Step");
            SqueakyBooBoo.BuildAndRegister();
            Ponies.Log("Squeaky Boo-Boo");
            PoisonJoke.BuildAndRegister(); //Not banner unit
            Ponies.Log("Poison Joke");

            //Rare units [3] +1
            TavernAce.BuildAndRegister();
            Ponies.Log("Tavern Ace");
            LordOfEmber.BuildAndRegister();
            Ponies.Log("Jestember");
            GuardianOfTheGates.BuildAndRegister();
            Ponies.Log("Guardian of the Gates");
            HeartsDesire.BuildAndRegister(); //Not banner unit
            Ponies.Log("HeartsDesire");
            //Ponies.Log("RoomModifiersData[0].DescriptionKey: " + CustomCharacterManager.GetCharacterDataByID(HeartsDesire.CharID).GetRoomModifiersData()[0].GetDescriptionKey());

            //A hidden unit that provides a default essence to prevent crashes.
            //Check MissingMare.cs under the MonsterCard folder for tips on how to find her.
            MissingMare.BuildAndRegister();
            Ponies.Log("Missing Mare");

            //Card Pools
            MyCardPools.DoCardPoolStuff();
            Ponies.Log("Card Pools");

            //Need card pools for this Uncommon spell
            FanClub.BuildAndRegister();
            Ponies.Log("Fan Club");

            //And this rare spell
            Tom.BuildAndRegister();
            Ponies.Log("Tom");

            //Enhancers
            //This generates errors because Trainworks uses new EnhancerData(); instead of ScriptableObject.CreateInstance<EnhancerData>();
            //It still works, though.
            Ponies.FriendstoneData = Friendstone.BuildAndRegister();
            Ponies.Log("Friendstone");
            Playstone.BuildAndRegister();
            Ponies.Log("Playstone");
            Gradstone.BuildAndRegister();
            Ponies.Log("Gradstone");

            //Relics [11 total]
            //Your clan needs to have at least one relic or the game's logbook UI gets messed up starting with the champion upgrade trees.
            //Relics seem to appear in the log book in reverse order. The last one registered is on the left.
            RoyalScroll.BuildAndRegister(); //build this one first to see if order matters.
            Ponies.Log("Royal Scroll"); 
            JunkFood.BuildAndRegister();
            Ponies.Log("Junk Food");
            BottledCutieMark.BuildAndRegister();
            Ponies.Log("Bottled Cutie Mark");
            TornLapelPin.BuildAndRegister();
            Ponies.Log("Torn Lapel Pin");
            MareInTheMoon.BuildAndRegister();
            Ponies.Log("Mare in the Moon");
            TinyMouseCrutches.BuildAndRegister();
            Ponies.Log("Tiny Mouse Wheelchair");
            MysteriousGoldenRod.BuildAndRegister();
            Ponies.Log("Strange Golden Rod");
            AChildsDrawing.BuildAndRegister();
            Ponies.Log("A Child's Drawing");
            //ACollectionOfRibbons.BuildAndRegister();
            //Ponies.Log("A Collection of Ribbons");
            Bloomberg.BuildAndRegister();
            Ponies.Log("Bloomberg");
            ImaginaryFriends.BuildAndRegister();
            Ponies.Log("Imaginary Friends");
            TheSeventhElement.BuildAndRegister();
            Ponies.Log("The Seventh Element");

            //Register unit synthesis data (DLC essences)
            //LiLyPatch.FindUnitSynthesisMappingInstanceToStub();
            //Ponies.Log("Lily Patch");
            //Trainworks.Patches.AccessUnitSynthesisMapping.FindUnitSynthesisMappingInstanceToStub();
            //Ponies.Log("Trainworks Unit Synthesis Patch");
            //if (ProviderManager.SaveManager.GetBalanceData().SynthesisMapping.GetUpgradeData(CustomCardManager.GetCardDataByID(BackgroundPony.ID).GetSpawnCharacterData()) == null) 
            //{
            //    Ponies.Instance.Logger.LogError("Equestrian Clan: Unit synthesis mapping failed. Attempting fallback plan.");
            //    UnitSynthesisMappingIsBorkedAgain = true;
            //}

            //Champions
            MareaLee.BuildAndRegister();
            Ponies.Log("Mare a Lee");
            Tantabus.BuildAndRegister();
            Ponies.Log("Tantabus");

            //Ponies.Log("Champion[0]: " + EquestrianClanData.GetChampionCard(0).GetName());
            //Ponies.Log("Champion[1]: " + EquestrianClanData.GetChampionCard(1).GetName());

            //Banner
            EquestrianBanner.buildbanner();
            Ponies.Log("Banner");

            //Fix a bug with the revenge version of Heaven's aid so it won't trigger Preeny Snuggles' on played effect when triggered.
            FixHeavensAid.MakeItSo();
            Ponies.Log("Fix Heaven's Aid");

            //Change Heph's gender from Male to Female.
            HephIsAFineAndDandyLADYThankYouVeryMuch.Fix();
            Ponies.Log("Stop Misgendering Heph");

            //Add equestrian variants for the starter spell 'Analog'
            ArcadianCompatibility.Initialize();
            Ponies.Log("Arcadian Stuff Complete.");

            //The story file controls the text and defines the clickable buttons and logic of cavern events.
            FlowerPonies.EditMasterStoryFile();
            Ponies.Log("Added \"FlowerPonies\" cavern event script to the master story file.");

            //The event data determines any requirements and links the clickable buttons to their effects/rewards.
            FlowerPonies.BuildEventData();
            Ponies.Log("Build and register \"FlowerPonies\" event data.");

            //Mutators
            CallTheCavalryMutator.BuildAndRegister();
            Ponies.Log("Call the Cavalry Mutator");
            DesertionMutator.BuildAndRegister();
            Ponies.Log("Desertion Mutator");
            BureaucracyMutator.BuildAndRegister();
            Ponies.Log("Bureaucracy Mutator");
            GroupHugMutator.BuildAndRegister();
            Ponies.Log("Group Hug Mutator");
            DivineOmnipresence.BuildAndRegister();
            Ponies.Log("Divine Omnipresence Mutator");
            DivineVoid.BuildAndRegister();
            Ponies.Log("Divine Void Mutator");
            AdaptiveFoes.BuildAndRegister();
            Ponies.Log("Adaptive Foes Mutator");
            Bubbles.BuildAndRegister();
            Ponies.Log("Bubbles Mutator");
            WorthIt.BuildAndRegister();
            Ponies.Log("Worth It Mutator");
            YouveGotMail.BuildAndRegister();
            Ponies.Log("You've Got Mail Mutator");
            GenderReveal.BuildAndRegister();
            Ponies.Log("Gender Reveal Mutator");
            ReadyForAnything.BuildAndRegister();
            Ponies.Log("Ready for Anything Mutator");

            //Expert challenges
            PONIESTAKEOVER_spChallenge.BuildAndRegister();
            Ponies.Log("Expert Challenge: PONIES TAKE OVER");
            DesignedByCommittee_spChallenge.BuildAndRegister();
            Ponies.Log("Expert Challenge: Red Tape");
            SomeDivinity_spChallenge.BuildAndRegister();
            Ponies.Log("Expert Challenge: Some Divinity");
            IJustDontKnowWhatWentWrong_spChallenge.BuildAndRegister();
            Ponies.Log("Expert Challenge: I Just Don't Know What Went Wrong");
            GenderRevealParty_spChallenge.BuildAndRegister();
            Ponies.Log("Expert Challenge: Gender Reveal Party");

            //This challenge crossed the line between 'difficult' and 'unfair'.
            //It wasn't really fun re-rolling for specific cards/units to simply have a chance.
            //AllOutAssault_spChallenge.BuildAndRegister();
            //Ponies.Log("All Out Assault");

            //Load metagame items
            //This inculdes the state of the 'Give Pony' toggle, the number of times Missing Mare was recruited (for the card mastery frame), and the victory status (normal/divine) of completed expert challenges.
            PonyMetagame.LoadPonyMetaFile();
            Ponies.Log("Loaded Metagame File");

            //Zap this to reset it.
            AccessTools.Field(typeof(UnitSynthesisMapping), "_dictionaryMapping").SetValue(ProviderManager.SaveManager.GetBalanceData().SynthesisMapping,null);

            //Run unit synthesis mapping.
            Trainworks.Patches.AccessUnitSynthesisMapping.FindUnitSynthesisMappingInstanceToStub();
            Ponies.Log("Trainworks Unit Synthesis Patch");

            //The card mastery frame is unlocked by finding Missing Mare.
            //This just extends the enum type of MasteryFrameType.
            PonyFrame = new PonyFrame("ponyCardFrame");
            Ponies.Log("Pony Card Mastery Frame");

            //Clear the background 'Loading...' image and reset the game cursor back to normal.
            PonyLoader.HideImage();
            Ponies.Log("Ending Loading Image");

            //Event Data Mining
            //Ponies.Log("EventChoice_HephRecruit_TakeHammer".Localize());
            //Output: Fine-tuning is my middle name.
            //Ponies.Log("EventChoice_HephRecruit_TakeHammer_Optional".Localize());
            //Output: Get {Card: HephHammer}.
            //Ponies.Log("EventChoice_HephRecruit_RefuseHammer".Localize());
            //Output: That's a little outside my comfort zone.
            //Ponies.Log("EventChoice_HephRecruit_RefuseHammer_Optional".Localize());
            //Output: Do nothing.
            //Ponies.Log("EventChoice_HephRecruit_TakeUpgrade".Localize());
            //Output: Absolutely!
            //Ponies.Log("EventChoice_HephRecruit_TakeUpgrade_Optional".Localize());
            //Output: Get {Upgrade: HephRecruit_UpgradeAttack}.{DeckReward: HephRecruit_UpgradeAttack}
            //Ponies.Log("EventChoice_HephRecruit_OnlyUnitExcludingChamps".Localize());
            //No localization.
            //Ponies.Log("EventChoice_HephRecruit_OnlyUnitExcludingChamps_Optional".Localize());
            //No localization.
            //Ponies.Log("EventChoice_HephRecruit_Leave".Localize());
            //No localization.
            // Ponies.Log("EventChoice_HephRecruit_Leave_Optional".Localize());
            //No localization.
            //Ponies.Log("EventChoice_GimmeGold_Medium".Localize());
            //Output: Just a little.
            //Ponies.Log("EventChoice_GimmeGold_Medium_Optional".Localize());
            //Output: Lose 5 Pyre health. {Coin: 25}

            //EditAwokenHollowStuff.ChangeCultivate();
            //Ponies.Log("Spike of the Stygian description: ");
            //Ponies.Log("CardData_overrideDescriptionKey-e6d3068eb68b320e-7987deaa29ff16e4891e8f14aac2d45e-v2".Localize());
            //Ponies.Log("CardRewardData__rewardTitleKey-2867c0be63de9841-11b47ead542e454429c34e3423ff3bf4-v2".Localize()); //Spike of the stygian reward.
            //Output: Apply {[trait0.statusmultiplier]}<sprite name="Xcost"> <b>Sap</b> and {[trait1.statusmultiplier]}<sprite name="Xcost"> <b>Frostbite</b>.
            //Ponies.Log("MutatorData_descriptionKey-5905a445b67ceaee-82dc6963c668dd14d9bd45a460fd7173-v2".Localize()); //Shardless mutator description.
            //Output: Disable all map nodes and events that provide <sprite name="PactShards">.

            EquestrianClanIsInit = true;

            //RemovePonyPile.CollectArcadianUnits();

            //Ponies.Log("Enum: " + PonyFrame.GetEnum().ToString());
            //Ponies.Log("ID: " + (int)PonyFrame.GetEnum());
        }
        private void Awake()
        {
            Ponies.Instance = this;
            var harmony = new Harmony(GUID);
            harmony.PatchAll();
        }

        public static void Log(string message)
        {
            Ponies.Instance.Logger.LogInfo("Equestrian Clan: "+message);
        }

        public static void LogError(string message) 
        {
            Ponies.Instance.Logger.LogError("Equestrian Clan: " + message);
        }
    }
}