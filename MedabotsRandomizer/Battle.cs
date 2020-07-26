using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MedabotsRandomizer
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BattleBot
    {
        public byte unknown;
        public byte head;
        public byte right_arm;
        public byte left_arm;
        public byte legs;
        public byte medal;
        public byte medal_level;
        public byte unknown1;
        public byte unknown2;
        public byte unknown3;
        public byte unknown4;
        public byte unknown5;

        public IEnumerable<FieldWrapper> toDataSource()
        {
            List<FieldWrapper> fields = new List<FieldWrapper>();
            fields.Add(new PartIdWrapper(head, "Head", 0));
            fields.Add(new PartIdWrapper(right_arm, "Right Arm", 1));
            fields.Add(new PartIdWrapper(left_arm, "Left Arm", 2));
            fields.Add(new PartIdWrapper(legs, "Legs", 3));
            fields.Add(new MedalIdWrapper(medal, "Medal"));
            fields.Add(new ByteWrapper(medal_level, "Medal Level"));
            return fields;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Battle
    {
        public byte characterId;
        public byte unknown_1;
        public byte number_of_bots;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public BattleBot[] bots = new BattleBot[3];
        public byte always_0;

        public static Battle FromBytes(byte[] bytes)
        {
            GCHandle gcHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            Battle battle = (Battle)Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof(Battle));
            gcHandle.Free();
            return battle;
        }

        public byte[] getBytes()
        {
            int size = Marshal.SizeOf(this);
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(this, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }
    }
}
