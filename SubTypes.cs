using Equestrian.Init;
using Trainworks.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equestrian.MonsterCards
{
    class SubtypePony
    {
        public static readonly string Key = Ponies.GUID + "_Subtype_Pony";

        public static void BuildAndRegister()
        {
            CustomCharacterManager.RegisterSubtype(Key, "Pony");
        }
    }
    class SubtypeHerb
    {
        public static readonly string Key = Ponies.GUID + "_Subtype_Herb";

        public static void BuildAndRegister()
        {
            CustomCharacterManager.RegisterSubtype(Key, "Herb");
        }
    }
    class SubtypePet
    {
        public static readonly string Key = Ponies.GUID + "_Subtype_Pet";

        public static void BuildAndRegister()
        {
            CustomCharacterManager.RegisterSubtype(Key, "Pet");
        }
    }
    class SubtypeDragon
    {
        public static readonly string Key = Ponies.GUID + "_Subtype_Dragon";

        public static void BuildAndRegister()
        {
            CustomCharacterManager.RegisterSubtype(Key, "Dragon");
        }
    }
    class SubtypeTrap
    {
        public static readonly string Key = Ponies.GUID + "_Subtype_Trap";

        public static void BuildAndRegister()
        {
            CustomCharacterManager.RegisterSubtype(Key, "Trap");
        }
    }

}