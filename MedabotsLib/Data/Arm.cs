using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static MedabotsLib.IdTranslator;

namespace MedabotsLib.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Arm
    {
        public byte medal_compatibility;
        public Technique technique;
        public Speciality speciality;
        public Gender gender;
        public byte armor;
        public byte RoS;
        public byte power;
        public byte chain_reaction;
        public byte charge;
        public byte radiation;
        public byte unknown3;
        public byte unknown4;
        public byte unknown5;
        public byte unknown6;
        public byte unknown7;
        public byte unknown8;
    }
}
