using System;
using System.Collections.Generic;
using System.Drawing;

namespace Clean_Randomizer.Data
{
    public class ImageConverter
    {
        public static byte[] splitBytes(byte[] orig)
        {
            byte[] result = new byte[orig.Length * 2];
            int i = 0;
            foreach (byte val in orig)
            {
                result[i] = (byte)(val & 0x0f);
                i++;
                result[i] = (byte)((val & 0xf0) >> 4);
                i++;
            }
            return result;
        }

        public static byte[] getColors(short palette)
        {
            short red_mask = 0x7C00;
            short green_mask = 0x3E0;
            short blue_mask = 0x1F;

            byte red_value = (byte)((palette & red_mask) >> 10);
            byte green_value = (byte)((palette & green_mask) >> 5);
            byte blue_value = (byte)(palette & blue_mask);

            byte red = (byte)(red_value << 3);
            byte green = (byte)(green_value << 3);
            byte blue = (byte)(blue_value << 3);

            return new byte[] { blue, green, red };
        }
        public static List<byte[]> getTiles(byte[] data, int tilesize)
        {
            List<byte[]> result = new List<byte[]>();
            for (int i = 0; i < data.Length / tilesize; i++)
            {
                byte[] tile = new byte[tilesize];
                Array.Copy(data, i * tilesize, tile, 0, tilesize);
                result.Add(tile);
            }
            return result;
        }

        public static Bitmap GetPictureFromData(int w, int h, byte[] data, byte[] palette)
        {
            Bitmap pic = new Bitmap(w * 8, h * 8, PixelFormat.Format24bppRgb);
            Color c;

            List<byte[]> tiles = getTiles(data, 0x40);
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    byte[] tile = tiles[x + y * w];
                    for (int i = 0; i < tile.Length; i++)
                    {
                        short color_byte = (short)((palette[tile[i] * 2]) + (palette[tile[i] * 2 + 1] << 8));
                        byte[] color_bytes = getColors(color_byte);
                        c = Color.FromArgb(color_bytes[0], color_bytes[1], color_bytes[2]);
                        pic.SetPixel(i % 8 + x * 8, i / 8 + y * 8, c);
                    }
                }
            }

            return pic;
        }
    }
}
