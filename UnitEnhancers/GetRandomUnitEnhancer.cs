using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Equestrian.Init;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;
using Trainworks.Enums;
using Equestrian;
using CustomEffects;

namespace Equestrian.Enhancers
{
    public class VanillaUnitEnhancers
    {
        public static string Frenzystone = "0016b165-11a9-4a26-8837-3b2895bc39f8";
        public static string Largestone = "e1646fd5-9245-431a-a07f-580c3a033347";
        public static string Quickstone = "74d5a105-cc4a-4de4-8189-d346e2a6de53";
        public static string Eternalstone = "2ce018b5-2b43-4ecf-8567-696d4fd8c119";
        public static string Attackstone = "e8095d27-518c-4dd8-9f19-4306b42940f9";
        public static string Healthstone = "f0c610cd-0b6b-48af-8c61-9d23dbdfea34";
        public static string Battlestone = "247c99a2-1076-4cef-be8a-1b8441ea75d3";
        public static string Ragestone = "d00f4776-f11a-4c9c-a62f-bf9a95eaa52a";
        public static string Thornstone = "16beb502-1f53-42c0-b4fb-61da62401475";
        public static string Armorstone = "70dbb13a-a4f0-44bd-bcd5-9c5b94860cf5";
        public static string Shieldstone = "3ec7e45c-0820-45b7-910c-e50e817bcf23";
        public static string Burnoutstone = "ba976851-189e-4499-8e22-3fd76914c382";
        public static string Echostone = "9e2d80ca-4035-4610-bfa4-ccd22732dc5a";

        public static bool IsWyrm() 
        {
            SaveManager saveManager = ProviderManager.SaveManager;

            if (saveManager.HasMainClass()) 
            { 
                if (saveManager.GetMainClass().name == "ClassWurm") 
                { 
                    return true;
                }
                else if (saveManager.GetSubClass().name == "ClassWurm") 
                { 
                    return true;
                }
            }
            return false;
        }

        public static List<string> allEnhancers = new List<string> { };
        public static List<string> GetValidEnhancerList()
        {
            allEnhancers.Clear();

            allEnhancers.Add(Frenzystone);
            allEnhancers.Add(Largestone);
            allEnhancers.Add(Quickstone);
            allEnhancers.Add(Eternalstone);
            allEnhancers.Add(Ragestone);
            allEnhancers.Add(Thornstone);
            allEnhancers.Add(Armorstone);
            allEnhancers.Add(Shieldstone);
            allEnhancers.Add(Burnoutstone);
            allEnhancers.Add(Attackstone);
            allEnhancers.Add(Attackstone);
            allEnhancers.Add(Healthstone);
            allEnhancers.Add(Healthstone);
            allEnhancers.Add(Battlestone);
            allEnhancers.Add(Battlestone);
            if (IsWyrm()) 
            {
                allEnhancers.Add(Echostone);
            }
            allEnhancers.Add(Friendstone.FriendstoneData.GetID());

            return allEnhancers;
        }

        public static CardUpgradeData GetRandomEnhancer(RngId rngId) 
        {
            List<String> enhancerList = VanillaUnitEnhancers.GetValidEnhancerList();

            if (enhancerList == null)
                return null;
            if (enhancerList.Count == 0)
                return null;

            enhancerList.Shuffle(rngId);

            if (enhancerList[0] == Friendstone.FriendstoneData.GetID())
            { 
                return Friendstone.FriendstoneData.GetEffects()[0].GetParamCardUpgradeData();
            }
            else 
            {
                return ProviderManager.SaveManager.GetAllGameData().FindEnhancerData(enhancerList[0]).GetEffects()[0].GetParamCardUpgradeData();
            }
        }
    }
}