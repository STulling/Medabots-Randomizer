using System;
using System.Collections.Generic;
using System.Text;

namespace MedabotsRandomizer
{
    public class TextWrapper
    {
        int id1;
        int id2;
        int memory_location;
        public byte[] data;

        public TextWrapper(int id1, int id2, int memory_location, byte[] data)
        {
            this.id1 = id1;
            this.id2 = id2;
            this.memory_location = memory_location;
            this.data = data;
        }

        public string Id1 => id1.ToString();
        public string Id2 => id2.ToString();
        public string Memory_Location => (memory_location + 0x8000000).ToString("X8");
        public string Size => data.Length.ToString("X8");
    }
}
