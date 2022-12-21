using HarmonyLib;
using Equestrian.CardPools;
using Trainworks.Managers;
using Equestrian.HarmonyPatches;
using Equestrian.Init;

namespace Equestrian.HarmonyPatches 
{
    public static class CollisionAvoider
    {
        public static bool hasRestartBattleButton = false;
        public static bool hasMoveBattleUI = false;
        private static bool doneInit = false;

        public static bool HasRestartBattleButton()
        {
            if (doneInit) { return hasRestartBattleButton; }

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
                        hasRestartBattleButton = true;
                    }
                    if (owner == "rawsome.modster-train.move-battle-ui") 
                    {
                        Ponies.Log("Battle UI has been moved.");
                        hasMoveBattleUI = true;
                    }

                    //Ponies.Log("Patch owner: " + owner);
                }
            }

            doneInit = true;

            return hasRestartBattleButton;
        }
    }
}