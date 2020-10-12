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

        public List<byte[]> getTiles(int tilesize)
        {
            List<byte[]> result = new List<byte[]>();
            for (int i = 0; i < this.data.Length / tilesize; i++)
            {
                byte[] tile = new byte[tilesize];
                Array.Copy(this.data, i * tilesize, tile, 0, tilesize);
                result.Add(tile);
            }
            return result;
        }
    }
}
