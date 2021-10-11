using System.Runtime.InteropServices;

namespace MedabotsLib.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Encounters
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] battle;

        public static Encounters FromBytes(byte[] bytes)
        {
            GCHandle gcHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            Encounters encounters = (Encounters)Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof(Encounters));
            gcHandle.Free();
            return encounters;
        }
    }
}
