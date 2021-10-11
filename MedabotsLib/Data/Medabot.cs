using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static MedabotsLib.IdTranslator;

namespace MedabotsLib.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Medabot
    {
        public byte unknown;
        public byte head;
        public byte right_arm;
        public byte left_arm;
        public byte legs;
        public Medal_Id medal;
        public byte medal_level;
        public byte unknown1;
        public byte unknown2;
        public byte unknown3;
        public byte unknown4;
        public byte unknown5;

        public bool isComplete()
        {
            return head == right_arm && right_arm == left_arm && left_arm == legs;
        }
    }
}
