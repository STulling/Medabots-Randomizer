using System;
using System.Collections.Generic;
using System.Text;

namespace MedabotsRandomizer
{
    public class TextWrapper
    {
        int id;
        int memory_location;
        public byte[] data;

        public TextWrapper(int id, int memory_location, byte[] data)
        {
            this.id = id;
            this.memory_location = memory_location;
            this.data = data;
        }

        public string ImageNo => id.ToString();
        public string Memory_Location => (memory_location + 0x8000000).ToString("X8");
        public string Size => data.Length.ToString("X8");
    }
}
