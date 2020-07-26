using System;
using System.Collections.Generic;
using System.Text;

namespace MedabotsRandomizer
{
    public class ImageWrapper
    {
        int id;
        int memory_location;
        public byte[] data;
        string method;

        public ImageWrapper(int id, int memory_location, byte[] data, string method)
        {
            this.id = id;
            this.memory_location = memory_location;
            this.data = data;
            this.method = method;
        }

        public string ImageNo => id.ToString();
        public string Memory_Location => (memory_location + 0x8000000).ToString("X8");

        public string Size => data.Length.ToString("X8");

        public string Compression_Method => method;

        public List<byte[]> getTiles()
        {
            List<byte[]> result = new List<byte[]>();
            for (int i = 0; i < this.data.Length / 0x20; i++)
            {
                byte[] tile = new byte[0x20];
                Array.Copy(this.data, i * 0x20, tile, 0, 0x20);
                result.Add(tile);
            }
            return result;
        }
    }
}
