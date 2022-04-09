using HarmonyLib;
using Ink.Runtime;
using Equestrian.Init;
using Trainworks.Managers;

namespace Equestrian.PonyStory
{

    [HarmonyPatch(typeof(InkWriter), "BeginStory")]
    public static class FixStorySoftlockOnGameReload
    {
        public static void Prefix(ref Story ____currentStory, ref StoryEventData storyData) 
        {
            //We are already softlocked before we reach 'Postfix'
            //No errors present at 'Prefix'
            //Ponies.Log("Checking on status of story for: " + storyData.name);

            if(storyData.name == "FlowerPonies") 
            {
                //Test resetting story
                //This works.
                ____currentStory = new Story(ProviderManager.SaveManager.GetBalanceData().MasterStoryFile.text);
            }

            /*
            if(____currentStory == null) 
            {
                Ponies.Log("Story is NULL");
            }
            if (____currentStory.hasError) 
            {
                Ponies.Log("Story has error(s): ");
                foreach(string error in ____currentStory.currentErrors) 
                {
                    Ponies.Log(error);
                }
            }
            if (____currentStory.hasWarning)
            {
                Ponies.Log("Story has warning(s): ");
                foreach (string warning in ____currentStory.currentWarnings)
                {
                    Ponies.Log(warning);
                }
            }
            */
        }
    }

    /*
    [HarmonyPatch(typeof(Story), "ChoosePathString")]
    public static class DebugStorySoftlockOnGameReloadDigDeeper
    {
        public static void Prefix(ref string path)
        {
            //Okay
            Ponies.Log("Story.ChoosePathString() prefix called.");
            Ponies.Log($"Path: {path}");
        }
        public static void Postfix()
        {
            //Softlocked :(
            Ponies.Log("Story.ChoosePathString() postfix called.");
        }
    }

    [HarmonyPatch(typeof(Story), "ChoosePath")]
    public static class DebugStorySoftlockOnGameReloadDigDeeperAndDeeper
    {
        public static void Prefix()
        {
            //Okay
            Ponies.Log("Story.ChoosePath() prefix called.");
        }
        public static void Postfix()
        {
            //Softlocked :(
            Ponies.Log("Story.ChoosePath() postfix called.");
        }
    }
    */
}