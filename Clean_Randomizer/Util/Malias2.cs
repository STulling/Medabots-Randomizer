using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Randomizer.Util
{
    public static class Malias2
    {
        public static byte[] Compress(byte[] data)
        {
            byte[] padded = new byte[data.Length + 20];
            Array.Copy(data, 0, padded, 0, data.Length);
            List<byte> compressed = new List<byte>();
            compressed.Add((byte)'L');
            compressed.Add((byte)'e');
            compressed.Add(0x00);
            compressed.Add(0x08);
            compressed.Add(0x00);
            compressed.Add(0x00);
            int i = 0;
            while (i < data.Length)
            {
                compressed.Add(0xAA);
                compressed.Add(padded[i++]);
                compressed.Add(padded[i++]);
                compressed.Add(padded[i++]);
                compressed.Add(padded[i++]);
            }
            return compressed.ToArray();
        }
    }
}
