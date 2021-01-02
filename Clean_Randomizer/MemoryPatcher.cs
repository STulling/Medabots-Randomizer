using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Randomizer
{
    public class MemoryPatcher
    {
        uint offset;
        byte[] file;
        public MemoryPatcher(byte[] file, uint offset)
        {
            this.file = file;
            this.offset = offset;
        }

        public uint PatchMemory(byte[] data)
        {
            uint memoryOffset = this.offset;

            Utils.WritePayload(this.file, (uint)memoryOffset - 0x8000000, data);
            this.offset += (uint)data.Length;

            return memoryOffset;
        }

        public void PatchMemoryAndStoreAddress(byte[] data, uint address)
        {
            uint stored_offset = PatchMemory(data);
            Utils.WriteInt(this.file, address, stored_offset);
        }
    }
}
