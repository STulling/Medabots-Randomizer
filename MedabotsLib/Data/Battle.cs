using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MedabotsLib.Data
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Battle
    {
        public byte characterId;
        public byte unknown_1;
        public byte number_of_bots;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public Medabot[] bots = new Medabot[3];
        public byte always_0;
    }
}
