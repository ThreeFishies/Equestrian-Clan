using System;
using System.Collections;
using Equestrian.Init;
using Equestrian.HarmonyPatches;
using Equestrian.Mutators;
using ShinyShoe.Loading;
using UnityEngine;
using System.Collections.Generic;
using Trainworks.Managers;

namespace CustomEffects
{
    // Token: 0x020002B3 RID: 691
    public sealed class CustomRelicEffectFixArtForPreSpawnExtraWave : RelicEffectBase, IStartOfCombatRelicEffect
    {
        public IEnumerator ApplyEffect(RelicEffectParams effectParams)
        {
            //NoRespite.preloadCharacters.Clear();
            NoRespite.artLoaded = false;
            //NoRespite.currentRing = -2;

            List<CharacterData> list = NoRespite.GatherCharacters();

            //if (NoRespite.artLoaded) 
            //{
                //Ponies.Log("Art is loaded. Skipping request.");
                //yield break; 
            //}

            //Ponies.Log("Attempting to load assets for new wave pattern.");

            if (list.Count != 4) 
            {
                //Ponies.Log("Failed: No characters in list yet.");
                yield break; 
            }

            FixArt.TryYetAnotherFix(list, ShinyShoe.Loading.LoadingScreen.DisplayStyle.Background,
               delegate
               {
                   if (!ProviderManager.SaveManager.ShowPactCrystals())
                   {
                       NoRespite.artLoaded = true;
                   }
               }
            );

            if (!ProviderManager.SaveManager.ShowPactCrystals()) { yield break; }

            List<CharacterData> PactVariants = new List<CharacterData>();

            foreach ( CharacterData c in list ) 
            {
                //Even though the units are identical, the hellforged variants still need to be loaded as well.
                c.GetHellforgedCrystalsVariantStats(out _, out CharacterData sharded);
                PactVariants.Add(sharded);
            }

            FixArt.TryYetAnotherFix(PactVariants, ShinyShoe.Loading.LoadingScreen.DisplayStyle.Background,
               delegate
               {
                   NoRespite.artLoaded = true;
                   //Ponies.Log("Loading complete.");
               }
            );

            //Shouldn't need to explicitly wait for this. Loading should happen early in combat.
            //do
            //{
            //    yield return new WaitForSeconds(0.1f);
            //} while ( LoadingScreen.HasTask<LoadAdditionalCharacters>(true) );

            //if (NoRespite.artLoaded ) { Ponies.Log("Loading successful."); }

            /*
            foreach (CharacterData character in list) 
            { 
                if (character.HasCharacterPrefabVariant()) 
                {
                    Ponies.Log(character.GetNameEnglish() + " is loaded.");
                }
                else
                { 
                    Ponies.Log(character.GetNameEnglish() + " failed to load. Retrying.");

                    LoadingScreen.AddTask(new LoadAdditionalCharacters(character, LoadingScreen.DisplayStyle.Spinner, null));

                    do
                    {
                        yield return new WaitForSeconds(0.1f);
                    } while (LoadingScreen.HasTask<LoadAdditionalCharacters>(true));

                    if (!character.HasCharacterPrefabVariant()) 
                    {
                        Ponies.Log(character.GetNameEnglish() + " failed to load again.");
                    }
                    else 
                    {
                        Ponies.Log(character.GetNameEnglish() + " is now loaded on second attempt.");
                    }
                }
            }
            */

            yield break;
        }

        public bool TestEffect(RelicEffectParams effectParams) 
        {
            return true;
        }
    }
}