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
using Equestrian.Mutators;
using ShinyShoe.Loading;
using System.Collections;
using Steamworks;
using Equestrian.HarmonyPatches;
using PubNubAPI;

namespace Equestrian.Mutators
{
    public static class NoRespite
    {
        public static string ID = Ponies.GUID + "_NoRespite";
        public static string NoRespiteMessageKey = "Pony_Mutator_NoRespite_Activated_Key";
        public static MutatorData mutatorData;
        public static List<CharacterData> preloadCharacters = new List<CharacterData>();
        public static int currentRing = -2;
        public static bool artLoaded = false;

        public static List<CharacterData> GatherCharacters() 
        {
            if (currentRing != ProviderManager.SaveManager.GetCurrentDistance()) 
            {
                preloadCharacters.Clear();
                artLoaded = false;
                currentRing = ProviderManager.SaveManager.GetCurrentDistance();
                //Ponies.Log("Reset art flag for ring: " + currentRing);
            }

            if (!HasNoRespite()) 
            { 
                return preloadCharacters;
            }

            if (preloadCharacters.Count == 4) 
            {
                return preloadCharacters;
            }

            SaveManager saveManager = ProviderManager.SaveManager;
            int currentLevel = 0;
            if (saveManager.ShowPactCrystals())
            {
                int crystals = saveManager.GetDlcSaveData<HellforgedSaveData>(DLC.Hellforged).GetCrystals();
                HellforgedThreatLevel hellforgedThreatLevelAtDistance = saveManager.GetBalanceData().GetHellforgedThreatLevelAtDistance(saveManager.GetCurrentDistance());
                if (crystals >= hellforgedThreatLevelAtDistance.DangerAmount)
                {
                    currentLevel = 4;
                }
                else if (crystals >= hellforgedThreatLevelAtDistance.WarningAmount)
                {
                    currentLevel = 3;
                }
                else if (crystals >= hellforgedThreatLevelAtDistance.LowAmount)
                {
                    currentLevel = 2;
                }
                else if (crystals > 0)
                {
                    currentLevel = 1;
                }

            }

            NoRespitePatch2.currentLevel = currentLevel;

            //if (currentLevel > 0) { NoRespitePatch2.NewSpawnPattern = true; }

            for (int jj = 0; jj < 4; jj++)
            {
                NoRespitePatch2.HellforgedSpawnPattern[jj] = (jj < currentLevel);
            }

            NoRespitePatch2.HellforgedSpawnPattern.Shuffle<bool>(RngId.Battle);

            switch (currentRing)
            {
                case 0: //ring 1
                    {
                        //Notes on behavior and functionality:

                        //List is weighted to favor small guys.
                        //List consists of ALL units that can spawn at that ring, regardless of normal spawn pattern.
                        //Character art will need to be loaded. (see CustomRelicEffectFixArtForPreSpawnExtraWave)
                        //Four units will be chosen at random.
                        //Order is completely random.
                        //Number of shard enhaced units is based on threat level (none = 0, low = 1, medium = 2, high = 3, extreme = 4). (see NoRespitePatch2)
                        //Shard enhanced units are chosen at random. The crystal 'cost' system is ignored. (see NoRespitePatch3)

                        List<CharacterData> list = new List<CharacterData>
                        {
                            CustomCharacterManager.GetCharacterDataByID("5709fe14-007a-4900-84b3-c8d4d9f5f948"), //ClericT1
                            CustomCharacterManager.GetCharacterDataByID("5c46e946-e798-4b5a-8b91-10d9a5822234"), //AttackerT1
                            CustomCharacterManager.GetCharacterDataByID("5c46e946-e798-4b5a-8b91-10d9a5822234"), //AttackerT1
                            CustomCharacterManager.GetCharacterDataByID("d441a440-931c-48db-9633-bb761f503042"), //HordeT1
                            CustomCharacterManager.GetCharacterDataByID("d441a440-931c-48db-9633-bb761f503042"), //HordeT1
                            CustomCharacterManager.GetCharacterDataByID("d441a440-931c-48db-9633-bb761f503042"), //HordeT1
                            CustomCharacterManager.GetCharacterDataByID("d441a440-931c-48db-9633-bb761f503042"), //HordeT1
                            CustomCharacterManager.GetCharacterDataByID("bb48fc25-87a1-41cb-b951-e052dcb60d7e"), //HeavyT1ChainedBrute
                            CustomCharacterManager.GetCharacterDataByID("2e371b96-1f84-44e6-9ce5-a301c9b84763"), //HeavyT1Basic
                            CustomCharacterManager.GetCharacterDataByID("818e0817-a691-4248-bdbf-98d32982ad9b") //ShieldGuard1
                        };

                        list.Shuffle<CharacterData>(RngId.Battle);

                        preloadCharacters.Add(list[0]);
                        preloadCharacters.Add(list[1]);
                        preloadCharacters.Add(list[2]);
                        preloadCharacters.Add(list[3]);

                        return preloadCharacters;
                    }
                case 1: //ring 2
                    {
                        List<CharacterData> list2 = new List<CharacterData>
                        {
                            CustomCharacterManager.GetCharacterDataByID("febefdcb-ce46-4884-a2f0-3485494bc901"), //EnchanterT1Speed
                            CustomCharacterManager.GetCharacterDataByID("febefdcb-ce46-4884-a2f0-3485494bc901"), //EnchanterT1Speed
                            CustomCharacterManager.GetCharacterDataByID("d441a440-931c-48db-9633-bb761f503042"), //HordeT1
                            CustomCharacterManager.GetCharacterDataByID("d441a440-931c-48db-9633-bb761f503042"), //HordeT1
                            CustomCharacterManager.GetCharacterDataByID("5c46e946-e798-4b5a-8b91-10d9a5822234"), //AttackerT1
                            CustomCharacterManager.GetCharacterDataByID("5c46e946-e798-4b5a-8b91-10d9a5822234"), //AttackerT1
                            CustomCharacterManager.GetCharacterDataByID("5c46e946-e798-4b5a-8b91-10d9a5822234"), //AttackerT1
                            CustomCharacterManager.GetCharacterDataByID("5709fe14-007a-4900-84b3-c8d4d9f5f948"), //ClericT1
                            CustomCharacterManager.GetCharacterDataByID("5709fe14-007a-4900-84b3-c8d4d9f5f948"), //ClericT1
                            CustomCharacterManager.GetCharacterDataByID("feb71302-93eb-4d65-bfd5-c4afc072dd55"), //HeavyT1GainAttackOnSpellcast
                            CustomCharacterManager.GetCharacterDataByID("feb71302-93eb-4d65-bfd5-c4afc072dd55"), //HeavyT1GainAttackOnSpellcast
                            CustomCharacterManager.GetCharacterDataByID("85e6b58c-8bab-4390-9319-14291bc71f9c"), //MageT1Junker
                            CustomCharacterManager.GetCharacterDataByID("85e6b58c-8bab-4390-9319-14291bc71f9c"), //MageT1Junker
                            CustomCharacterManager.GetCharacterDataByID("85e6b58c-8bab-4390-9319-14291bc71f9c"), //MageT1Junker
                            CustomCharacterManager.GetCharacterDataByID("2e371b96-1f84-44e6-9ce5-a301c9b84763"), //HeavtT1Basic
                            CustomCharacterManager.GetCharacterDataByID("2e371b96-1f84-44e6-9ce5-a301c9b84763"), //HeavtT1Basic
                            CustomCharacterManager.GetCharacterDataByID("818e0817-a691-4248-bdbf-98d32982ad9b"), //ShieldGuard1
                            CustomCharacterManager.GetCharacterDataByID("818e0817-a691-4248-bdbf-98d32982ad9b"), //ShieldGuard1
                            CustomCharacterManager.GetCharacterDataByID("ba72de22-8c0e-478f-a6df-524929742977"), //EnchanterT1Spikes
                            CustomCharacterManager.GetCharacterDataByID("ba72de22-8c0e-478f-a6df-524929742977") //EnchanterT1Spikes
                        };

                        list2.Shuffle<CharacterData>(RngId.Battle);

                        preloadCharacters.Add(list2[0]);
                        preloadCharacters.Add(list2[1]);
                        preloadCharacters.Add(list2[2]);
                        preloadCharacters.Add(list2[3]);

                        return preloadCharacters;
                    }
                //Skip ring 3 - flying boss
                case 3: //ring 4
                    {
                        List<CharacterData> list4 = new List<CharacterData>
                        {
                            CustomCharacterManager.GetCharacterDataByID("f277aff2-eb7e-417a-9b27-ff3bd98975f3"), //AttackerT2
                            CustomCharacterManager.GetCharacterDataByID("f277aff2-eb7e-417a-9b27-ff3bd98975f3"), //AttackerT2
                            CustomCharacterManager.GetCharacterDataByID("f277aff2-eb7e-417a-9b27-ff3bd98975f3"), //AttackerT2
                            CustomCharacterManager.GetCharacterDataByID("5709fe14-007a-4900-84b3-c8d4d9f5f948"), //ClericT1
                            CustomCharacterManager.GetCharacterDataByID("d0f90c99-28b7-4700-9192-6d34efa5fe39"), //ClericT2
                            CustomCharacterManager.GetCharacterDataByID("d0f90c99-28b7-4700-9192-6d34efa5fe39"), //ClericT2
                            CustomCharacterManager.GetCharacterDataByID("530b1e0c-d405-49b5-a394-96bd39e04cb5"), //EnchanterT2Speed
                            CustomCharacterManager.GetCharacterDataByID("530b1e0c-d405-49b5-a394-96bd39e04cb5"), //EnchanterT2Speed
                            CustomCharacterManager.GetCharacterDataByID("fd1ed53c-a112-44f7-86df-aa0887b43acf"), //HeavyT2Basic
                            CustomCharacterManager.GetCharacterDataByID("fd1ed53c-a112-44f7-86df-aa0887b43acf"), //HeavyT2Basic
                            CustomCharacterManager.GetCharacterDataByID("44d76f20-d74c-42a6-83f6-253069a007ef"), //HeavyT2GrowOnAscend
                            CustomCharacterManager.GetCharacterDataByID("44d76f20-d74c-42a6-83f6-253069a007ef"), //HeavyT2GrowOnAscend
                            CustomCharacterManager.GetCharacterDataByID("630f33d3-51cb-4cc1-8bc7-caabfbf80193"), //HeavyT2SelfRepair (incant armor)
                            CustomCharacterManager.GetCharacterDataByID("630f33d3-51cb-4cc1-8bc7-caabfbf80193"), //HeavyT2SelfRepair (incant armor)
                            CustomCharacterManager.GetCharacterDataByID("2e440ab7-7786-422c-ba18-4c987ba1fd55"), //MageT2Junker
                            CustomCharacterManager.GetCharacterDataByID("2e440ab7-7786-422c-ba18-4c987ba1fd55"), //MageT2Junker
                            CustomCharacterManager.GetCharacterDataByID("2e440ab7-7786-422c-ba18-4c987ba1fd55"), //MageT2Junker
                            CustomCharacterManager.GetCharacterDataByID("2e440ab7-7786-422c-ba18-4c987ba1fd55"), //MageT2Junker
                            CustomCharacterManager.GetCharacterDataByID("9a79de7c-8005-4921-9f43-7f6da00fb380"), //ShieldGuardT2
                            CustomCharacterManager.GetCharacterDataByID("9a79de7c-8005-4921-9f43-7f6da00fb380"), //ShieldGuardT2
                        };

                        list4.Shuffle(RngId.Battle);

                        preloadCharacters.Add(list4[0]);
                        preloadCharacters.Add(list4[1]);
                        preloadCharacters.Add(list4[2]);
                        preloadCharacters.Add(list4[3]);

                        return preloadCharacters;
                    }
                case 4: //ring 5
                    {
                        List<CharacterData> list5 = new List<CharacterData>
                        {
                            CustomCharacterManager.GetCharacterDataByID("f277aff2-eb7e-417a-9b27-ff3bd98975f3"), //AttackerT2
                            CustomCharacterManager.GetCharacterDataByID("f277aff2-eb7e-417a-9b27-ff3bd98975f3"), //AttackerT2
                            CustomCharacterManager.GetCharacterDataByID("f277aff2-eb7e-417a-9b27-ff3bd98975f3"), //AttackerT2
                            CustomCharacterManager.GetCharacterDataByID("2255cfcc-e245-4619-b23c-9172fdfca965"), //AttackerT2Stealth
                            CustomCharacterManager.GetCharacterDataByID("2255cfcc-e245-4619-b23c-9172fdfca965"), //AttackerT2Stealth
                            CustomCharacterManager.GetCharacterDataByID("2255cfcc-e245-4619-b23c-9172fdfca965"), //AttackerT2Stealth
                            CustomCharacterManager.GetCharacterDataByID("2255cfcc-e245-4619-b23c-9172fdfca965"), //AttackerT2Stealth
                            CustomCharacterManager.GetCharacterDataByID("d0f90c99-28b7-4700-9192-6d34efa5fe39"), //ClericT2
                            CustomCharacterManager.GetCharacterDataByID("d0f90c99-28b7-4700-9192-6d34efa5fe39"), //ClericT2
                            CustomCharacterManager.GetCharacterDataByID("fd1ed53c-a112-44f7-86df-aa0887b43acf"), //HeavyT2Basic
                            CustomCharacterManager.GetCharacterDataByID("fd1ed53c-a112-44f7-86df-aa0887b43acf"), //HeavyT2Basic
                            CustomCharacterManager.GetCharacterDataByID("fd1ed53c-a112-44f7-86df-aa0887b43acf"), //HeavyT2Basic
                            CustomCharacterManager.GetCharacterDataByID("f7b7fc11-a063-4393-a39a-3124f0a5ea98"), //HeavyT2DeathHarvester
                            CustomCharacterManager.GetCharacterDataByID("f7b7fc11-a063-4393-a39a-3124f0a5ea98"), //HeavyT2DeathHarvester
                            CustomCharacterManager.GetCharacterDataByID("f7b7fc11-a063-4393-a39a-3124f0a5ea98"), //HeavyT2DeathHarvester
                            CustomCharacterManager.GetCharacterDataByID("abc1302d-81b5-4e08-8c33-7d9e4821f783"), //HeavyT2GrowOnKill (sweeper)
                            CustomCharacterManager.GetCharacterDataByID("abc1302d-81b5-4e08-8c33-7d9e4821f783"), //HeavyT2GrowOnKill (sweeper)
                            CustomCharacterManager.GetCharacterDataByID("abc1302d-81b5-4e08-8c33-7d9e4821f783"), //HeavyT2GrowOnKill (sweeper)
                            CustomCharacterManager.GetCharacterDataByID("6143e635-1e9b-43b7-a48b-40f35c11af0d"), //HordeT2DeathGivesAttack
                            CustomCharacterManager.GetCharacterDataByID("6143e635-1e9b-43b7-a48b-40f35c11af0d"), //HordeT2DeathGivesAttack
                            CustomCharacterManager.GetCharacterDataByID("6143e635-1e9b-43b7-a48b-40f35c11af0d"), //HordeT2DeathGivesAttack
                            CustomCharacterManager.GetCharacterDataByID("6143e635-1e9b-43b7-a48b-40f35c11af0d"), //HordeT2DeathGivesAttack
                            CustomCharacterManager.GetCharacterDataByID("9a79de7c-8005-4921-9f43-7f6da00fb380"), //ShieldGuardT2
                            CustomCharacterManager.GetCharacterDataByID("9a79de7c-8005-4921-9f43-7f6da00fb380"), //ShieldGuardT2
                            CustomCharacterManager.GetCharacterDataByID("9a79de7c-8005-4921-9f43-7f6da00fb380"), //ShieldGuardT2
                        };

                        list5.Shuffle(RngId.Battle);

                        preloadCharacters.Add(list5[0]);
                        preloadCharacters.Add(list5[1]);
                        preloadCharacters.Add(list5[2]);
                        preloadCharacters.Add(list5[3]);

                        return preloadCharacters;
                    }
                //skip ring 6 - flying boss
                case 6: //ring 7
                    {
                        List<CharacterData> list7 = new List<CharacterData>
                        {
                            CustomCharacterManager.GetCharacterDataByID("fdf6db4f-2e1a-4718-b36e-3d250cfac630"), //AttackerT3
                            CustomCharacterManager.GetCharacterDataByID("fdf6db4f-2e1a-4718-b36e-3d250cfac630"), //AttackerT3
                            CustomCharacterManager.GetCharacterDataByID("fdf6db4f-2e1a-4718-b36e-3d250cfac630"), //AttackerT3
                            CustomCharacterManager.GetCharacterDataByID("fdf6db4f-2e1a-4718-b36e-3d250cfac630"), //AttackerT3
                            CustomCharacterManager.GetCharacterDataByID("18eb55da-80bd-4a61-b95e-b816d3dd4ba8"), //AttackerT3Reaper
                            CustomCharacterManager.GetCharacterDataByID("18eb55da-80bd-4a61-b95e-b816d3dd4ba8"), //AttackerT3Reaper
                            CustomCharacterManager.GetCharacterDataByID("18eb55da-80bd-4a61-b95e-b816d3dd4ba8"), //AttackerT3Reaper
                            CustomCharacterManager.GetCharacterDataByID("18eb55da-80bd-4a61-b95e-b816d3dd4ba8"), //AttackerT3Reaper
                            CustomCharacterManager.GetCharacterDataByID("b58aa8c2-8ca8-4682-a25a-239e451733ca"), //EnchanterT3Multistrike
                            CustomCharacterManager.GetCharacterDataByID("b58aa8c2-8ca8-4682-a25a-239e451733ca"), //EnchanterT3Multistrike
                            CustomCharacterManager.GetCharacterDataByID("b58aa8c2-8ca8-4682-a25a-239e451733ca"), //EnchanterT3Multistrike
                            CustomCharacterManager.GetCharacterDataByID("b58aa8c2-8ca8-4682-a25a-239e451733ca"), //EnchanterT3Multistrike
                            CustomCharacterManager.GetCharacterDataByID("808d529c-87c4-40f2-9b1b-c0e4176e5ad6"), //HeavyT3Basic
                            CustomCharacterManager.GetCharacterDataByID("808d529c-87c4-40f2-9b1b-c0e4176e5ad6"), //HeavyT3Basic
                            CustomCharacterManager.GetCharacterDataByID("808d529c-87c4-40f2-9b1b-c0e4176e5ad6"), //HeavyT3Basic
                            CustomCharacterManager.GetCharacterDataByID("808d529c-87c4-40f2-9b1b-c0e4176e5ad6"), //HeavyT3Basic
                            CustomCharacterManager.GetCharacterDataByID("adfc88b5-9b20-44a1-9fdc-53725aa7b20d"), //HeavyT3DeathHarvester
                            CustomCharacterManager.GetCharacterDataByID("adfc88b5-9b20-44a1-9fdc-53725aa7b20d"), //HeavyT3DeathHarvester
                            CustomCharacterManager.GetCharacterDataByID("adfc88b5-9b20-44a1-9fdc-53725aa7b20d"), //HeavyT3DeathHarvester
                            CustomCharacterManager.GetCharacterDataByID("adfc88b5-9b20-44a1-9fdc-53725aa7b20d"), //HeavyT3DeathHarvester
                            CustomCharacterManager.GetCharacterDataByID("d45133e4-1049-4713-9780-2e124af68356"), //HeavyT3EmberDrain
                            CustomCharacterManager.GetCharacterDataByID("d45133e4-1049-4713-9780-2e124af68356"), //HeavyT3EmberDrain
                            CustomCharacterManager.GetCharacterDataByID("d45133e4-1049-4713-9780-2e124af68356"), //HeavyT3EmberDrain
                            CustomCharacterManager.GetCharacterDataByID("d45133e4-1049-4713-9780-2e124af68356"), //HeavyT3EmberDrain
                            CustomCharacterManager.GetCharacterDataByID("44d7eb10-9ea2-46b2-a513-0cba9cd16890"), //HordeT3DeathDoesDamage
                            CustomCharacterManager.GetCharacterDataByID("44d7eb10-9ea2-46b2-a513-0cba9cd16890"), //HordeT3DeathDoesDamage
                            CustomCharacterManager.GetCharacterDataByID("44d7eb10-9ea2-46b2-a513-0cba9cd16890"), //HordeT3DeathDoesDamage
                            CustomCharacterManager.GetCharacterDataByID("44d7eb10-9ea2-46b2-a513-0cba9cd16890"), //HordeT3DeathDoesDamage
                            CustomCharacterManager.GetCharacterDataByID("a0e49b3c-747a-4b08-8583-7ac7d73e791b"), //MageT3Junker
                            CustomCharacterManager.GetCharacterDataByID("a0e49b3c-747a-4b08-8583-7ac7d73e791b"), //MageT3Junker
                            CustomCharacterManager.GetCharacterDataByID("a0e49b3c-747a-4b08-8583-7ac7d73e791b"), //MageT3Junker
                            CustomCharacterManager.GetCharacterDataByID("a0e49b3c-747a-4b08-8583-7ac7d73e791b"), //MageT3Junker
                            CustomCharacterManager.GetCharacterDataByID("76b7a42c-837d-40b4-9408-87abcd079fb4"), //ShieldGuardT3
                            CustomCharacterManager.GetCharacterDataByID("76b7a42c-837d-40b4-9408-87abcd079fb4"), //ShieldGuardT3
                            CustomCharacterManager.GetCharacterDataByID("76b7a42c-837d-40b4-9408-87abcd079fb4"), //ShieldGuardT3
                            CustomCharacterManager.GetCharacterDataByID("76b7a42c-837d-40b4-9408-87abcd079fb4"), //ShieldGuardT3
                        };

                        list7.Shuffle(RngId.Battle);

                        preloadCharacters.Add(list7[0]);
                        preloadCharacters.Add(list7[1]);
                        preloadCharacters.Add(list7[2]);
                        preloadCharacters.Add(list7[3]);

                        return preloadCharacters;
                    }
                //skip ring 8 - Seraph
                //skip ring 9 - Divinity
                default:
                    {
                        return preloadCharacters;
                    }
            }
        }

        public static bool HasNoRespite() 
        {
            if (ProviderManager.SaveManager.GetMutatorCount() > 0)
            {
                List<MutatorState> mutators = ProviderManager.SaveManager.GetMutators();
                foreach (MutatorState mutator in mutators)
                {
                    if (mutator.GetRelicDataID() == ID)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void BuildAndRegister()
        {
            mutatorData = new MutatorDataBuilder
            {
                ID = ID,
                NameKey = "Pony_Mutator_NoRespite_Name_Key",
                DescriptionKey = "Pony_Mutator_NoRespite_Description_Key",
                RelicActivatedKey = "Pony_Mutator_NoRespite_Activated_Key",
                RelicLoreTooltipKeys = new List<string>()
                {
                    "Pony_Mutator_NoRespite_Lore_Key"
                },
                DisableInDailyChallenges = false,
                DivineVariant = false,
                BoonValue = -5,
                RequiredDLC = DLC.None,
                IconPath = "Mutators/Sprite/MTR_NoRespite.png",
                Tags = new List<string>
                {
                },
                Effects = new List<RelicEffectData>
                {
                    new RelicEffectDataBuilder
                    {
                        //See NoRespitePatch.cs
                        RelicEffectClassName = typeof(CustomRelicEffectFixArtForPreSpawnExtraWave).AssemblyQualifiedName,
                    }.Build(),
                }
            }.BuildAndRegister();
        }
    }
}

namespace Equestrian.HarmonyPatches
{
    [HarmonyPatch(typeof(SpawnGroupData), "GetWaveMessage")]
    public static class NoRespitePatch1
    {
        public static void Postfix(ref string __result, ref SpawnGroupData __instance)
        {
            if (!Ponies.EquestrianClanIsInit) { return; }

            if (NoRespite.HasNoRespite())
            {
                //Only TLD has other 'wave messages' in the data ('This is not the end.').
                if (ProviderManager.SaveManager.GetCurrentDistance() == 8) { return; }

                //For whatever reason, the "A Brief Respite" message has many different localization keys.
                //But just having a 'wave message' is indication enough.
                __result = NoRespite.NoRespiteMessageKey.Localize();
            }
        }
    }

    [HarmonyPatch(typeof(SpawnGroupData), "GetCharacters")]
    public static class NoRespitePatch2
    {
        public static bool NewSpawnPattern = false;
        public static List<bool> HellforgedSpawnPattern = new List<bool> { false, false, false, false };
        public static int spawnIndex = 0;
        public static int currentLevel = 0;

        public static void Postfix(ref List<CharacterData> __result, ref SpawnGroupData __instance)
        {
            //NewSpawnPattern = false;

            if (!Ponies.EquestrianClanIsInit) { return; }
            if (!NoRespite.HasNoRespite()) { return; }
            if (__result.Count > 0) { return; }
            if (!ProviderManager.CombatManager.GetIsRunningCombat()) { return; }

            SaveManager saveManager = ProviderManager.SaveManager;

            //HellforgedSpawnPattern = new List<bool> { false, false, false, false };
            spawnIndex = 0;

            __result = NoRespite.GatherCharacters();

            if (currentLevel > 0) { NewSpawnPattern = true; }

            if (__result.Count != 4) { NewSpawnPattern = false; }
        }
    }


    [HarmonyPatch(typeof(SpawnPatternData), "GetHellforgedVariant")]
    public static class NoRespitePatch3
    {
        public static void Postfix(ref CharacterData sourceData, ref CharacterData variantData, ref bool __result)
        {
            if (!Ponies.EquestrianClanIsInit) { return; }
            if (!NoRespitePatch2.NewSpawnPattern) { return; }

            if (NoRespitePatch2.HellforgedSpawnPattern[NoRespitePatch2.spawnIndex])
            {
                __result = true;
                sourceData.GetHellforgedCrystalsVariantStats(out _, out variantData);
            }
            else
            {
                variantData = null;
                __result = false;
            }

            NoRespitePatch2.spawnIndex++;

            if (NoRespitePatch2.spawnIndex == 4)
            {
                NoRespitePatch2.spawnIndex = 0;
                NoRespitePatch2.NewSpawnPattern = false;
            }
        }
    }


    [HarmonyPatch(typeof(HeroManager), "InstantiateCharacter")]
    public static class NoRespitePatch4
    {
        public static void Prefix(ref CharacterData characterData)
        {
            if (!characterData.HasCharacterPrefabVariant())
            {
                //Oddly, hellforged variants don't return a name.
                Ponies.Log("Warning: " + characterData.GetNameEnglish() + " is missing art.");
            }
        }
    }
}