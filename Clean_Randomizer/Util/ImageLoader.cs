using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean_Randomizer.Util
{
    public class ImageData
    {
        public byte[] data;
        public byte[] palette;
        public int[] metadata;

        public ImageData(int[] metadata, byte[] data, byte[] palette)
        {
            this.metadata = metadata;
            this.data = data;
            this.palette = palette;
        }
    }

    public static class ImageLoader
    {
        private static void swap(byte[] data, int one, int other)
        {
            byte tmp = data[one];
            data[one] = data[other];
            data[other] = tmp;
        }

        public static ImageData LoadImage(string file, string folder)
        {
            string[] parts = file.Split('_');
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\grit\\grit.exe",
                WorkingDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\" + folder + "\\",
                Arguments = file + ".bmp -gu16 -gt -ftbin -fh! -pn16"
            };
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            byte[] palette = File.ReadAllBytes(".\\" + folder + "\\" + file + ".pal.bin");
            byte[] data = File.ReadAllBytes(".\\" + folder + "\\" + file + ".img.bin");
            File.Delete(".\\" + folder + "\\" + file + ".pal.bin");
            File.Delete(".\\" + folder + "\\" + file + ".img.bin");
            
            if (data[0] != 0)
            {
                byte coolboy = data[0];
                swap(palette, 0, 2 * coolboy);
                swap(palette, 1, 2 * coolboy + 1);
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i] == 0)
                        data[i] = coolboy;
                    else if (data[i] == coolboy)
                        data[i] = 0;
                }
            }

            byte[] indexedColors = new byte[data.Length / 2];
            for (int i = 0; i < indexedColors.Length; i++)
            {
                indexedColors[i] = (byte)((data[2*i + 1] << 4) | data[2*i]);
            }

            return new ImageData(parts.Select(x => int.Parse(x)).ToArray(), indexedColors, palette);
        }
    }
}
