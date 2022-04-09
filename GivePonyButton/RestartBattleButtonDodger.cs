using HarmonyLib;
using Equestrian.CardPools;
using Trainworks.Managers;
using Equestrian.HarmonyPatches;
using Equestrian.Init;

namespace Equestrian.HarmonyPatches 
{
    public static class CollisionAvoider
    {
        public static bool HasRestartBattleButton()
        {
            var originalMethods = Harmony.GetAllPatchedMethods();
            foreach (var method in originalMethods) 
            {
                //Ponies.Log($"Patched Method: {method}");

                // retrieve all patches
                //Patches patches = Harmony.GetPatchInfo(typeof(BattleHud).GetMethod("Start"));
                Patches patches = Harmony.GetPatchInfo(method);
                if (patches is null)
                {
                    //Ponies.Log("No patches found.");
                    //return false; // not patched
                }

                // get a summary of all different Harmony ids involved
                //Ponies.Log("all owners: " + patches.Owners.Join<string>());

                foreach (string owner in patches.Owners)
                {
                    if (owner == "com.shinyshoe.restartbattle")
                    {
                        Ponies.Log("Restart Battle Button Exists.");
                        return true;
                    }
                }
            }
            return false;
        }
    }
}