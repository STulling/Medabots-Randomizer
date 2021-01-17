using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

static unsafe class LZ77
{
    #region Don't edit this!!!

    private enum ScanDepth : byte
    {
        Byte = 1,
        HalfWord = 2,
        Word = 4
    };

    private const ScanDepth scanDepth = ScanDepth.Word;
    private const int sizeMultible = 0x20;
    private const int maxSize = 0x8000;

    #endregion Don't edit this!!!

    /// <summary>
    /// Decompresses Malias2 data
    /// </summary>
    /// <param name="data">Data where compressed data is</param>
    /// <param name="offset">Offset of the compressed data</param>
    /// <returns>Decompressed data or null if decompression fails</returns>
    static public byte[] Decompress(byte[] data, int offset)
    {
        fixed (byte* ptr = &data[offset])
        {
            return Decompress(ptr);
        }
    }

    /// <summary>
    /// Decompresses Malias2 data
    /// </summary>
    /// <param name="source">Pointer to data to decompress</param>
    /// <returns>Decompressed data or null if decompression fails</returns>
    static public byte[] Decompress(byte* source)
    {
        byte[] uncompressedData = null;
        if (*source == 0x10)
        {
            int length = *(source + 1) | *(source + 2) << 8 | *(source + 3) << 16;
            if (length > 0 && length < 0x100000)
            {
                uncompressedData = new byte[length];
                fixed (byte* destination = &uncompressedData[0])
                {
                    if (!DecompressLZ77(source, destination))
                        uncompressedData = null;
                }
            }
        }
        return uncompressedData;
    }

    /// <summary>
    /// Decompresses Malias2 data
    /// </summary>
    /// <param name="source">Pointer to compressed data</param>
    /// <param name="target">Pointer to where uncompressed data goes</param>
    /// <returns>True if successful, else false</returns>
    static public bool DecompressLZ77(byte* source, byte* target)
    {
        int length = *(source + 1) | *(source + 2) << 8 | *(source + 3) << 16;
        source += 4;
        int written = 0;
        while (length > 0)
        {
            byte command = *source++;
            for (int i = 0; i < 8 && length > 0; i++)
            {
                bool type = (command & (0x80 >> i)) > 0;
                if (type)
                {
                    ushort value = (ushort)(*source++ | *source++ << 8);
                    ushort disp = (ushort)(((value & 0xf) << 8) | (value >> 8));
                    byte n = (byte)((value >> 4) & 0xf);
                    if (disp > written) return false;
                    for (int j = 0; j < n + 3; j++)
                    {
                        *target = *(target - disp - 1);
                        target++;
                        length--;
                        written++;
                    }
                }
                else
                {
                    *target++ = *source++;
                    length--;
                    written++;
                }
            }
        }
        return true;
    }
}