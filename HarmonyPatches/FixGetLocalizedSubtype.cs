using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;
using HarmonyLib;
using Equestrian.Init;
using Trainworks.Managers;
using Equestrian.SpellCards;
using Equestrian.MonsterCards;
using Equestrian.Relic;
using Equestrian;
using Trainworks.Constants;
using Trainworks.Builders;
using Equestrian.CardPools;
using Equestrian.Champions;
using ShinyShoe.Loading;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;


namespace Equestrian.HarmonyPatches
{
    [HarmonyPatch(typeof(CharacterData), "GetLocalizedSubtype")]
    public static class GetAllSubtypesNotJustTheFirst
    {
        public static void Postfix(ref CharacterData __instance, ref string __result)
        {
            List<SubtypeData> subtypes = __instance.GetSubtypes();
            if (subtypes.Count > 1) 
            { 
                for (int ii = 1; ii < subtypes.Count; ii++) 
                {
                    if (subtypes[ii].Key.HasTranslation()) 
                    {
                        //I'm not really sure why an erroneous 'Chosen' subtype was being added to the referenced unit tooltip for the Sweetkin unit 'Lime.'
                        //Just don't add it, I guess.
                        if (subtypes[ii] != SubtypeManager.GetSubtypeData(new List<string> { "SubtypesData_Chosen" })[0])
                        {
                            __result = __result + ", " + subtypes[ii].LocalizedName;
                        }
                    }
                }
            }
        }
    }
}