using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MedabotsRandomizer
{
    public static class CompressionUtils
    {
        private static int readint(List<byte> bytes) {
            int read_int = 0;
            read_int += bytes[0];
            bytes.RemoveAt(0);
            read_int += bytes[0] << 8;
            bytes.RemoveAt(0);
            read_int += bytes[0] << 16;
            bytes.RemoveAt(0);
            read_int += bytes[0] << 24;
            bytes.RemoveAt(0);
            return read_int;
        }

        private static int readshort(List<byte> bytes)
        {
            int read_short = 0;
            read_short += bytes[0];
            bytes.RemoveAt(0);
            read_short += bytes[0] << 8;
            bytes.RemoveAt(0);
            return read_short;
        }

        private static int readbyte(List<byte> bytes)
        {
            int read_byte = bytes[0];
            bytes.RemoveAt(0);
            return read_byte;
        }

        private static byte[] OtherMalias2Decompression(List<byte> bytes)
        {
            short magic = (short)readshort(bytes);
            if (magic != 0x654c) throw new InvalidGraphicsException("Wrong magic.");
            int total = readint(bytes);
            if (total > 0x10000) throw new InvalidGraphicsException("Insane size: " + total.ToString("X8"));
            byte[] data = new byte[total];
            int head = 0;
            bool first = true;

            if (total > 0)
            {
                while (head < total)
                {
                    byte modes = (byte)readbyte(bytes);
                    for (byte i = 0; i < 4; i++)
                    {
                        if (head >= total) break;
                        byte mode = Convert.ToByte(((modes >> (i * 2 + 1)) % 2 << 1) + ((modes >> i * 2) % 2));
                        if (first && (mode == 0 || mode == 1)) throw new InvalidGraphicsException("Begins with mode " + mode.ToString("X2"));
                        first = false;
                        if (mode == 0)
                        {
                            int lz = readshort(bytes);
                            int loc = (lz & 0b111111111111) + 5;
                            int num = (lz >> 12) + 3;
                            uint uVar4 = (uint)(head - loc);
                            for (int j = 0; j < num; j++)
                            {
                                int tmp_head = (int)(head & 0xfffffffe);
                                short uVar5 = (short)(((short)data[tmp_head] | (short)(data[tmp_head] << 8)) & 0xfffffffe);
                                short uVar2 = (short)(data[uVar4] | (short)(data[uVar4 + 1] << 8) & 0xfffffffe);
                                if ((uVar4 & 1) == 0)
                                {
                                    if ((head & 1) == 0)
                                    {
                                        data[tmp_head] = (byte)uVar5;
                                        data[tmp_head + 1] = ((byte)uVar2);
                                    }
                                    else
                                    {
                                        data[tmp_head] = (byte)(uVar2);
                                        data[tmp_head + 1] = (byte)(uVar5 >> 8);
                                    }
                                }
                                else
                                {
                                    if ((head & 1) == 0)
                                    {
                                        data[tmp_head] = (byte)(uVar2 >> 8);
                                        data[tmp_head + 1] = (byte)(uVar5 >> 8);
                                    }
                                    else
                                    {
                                        data[tmp_head] = (byte)(uVar5);
                                        data[tmp_head + 1] = (byte)(uVar2 >> 8);
                                    }
                                }
                                head++;
                            }
                        }
                        else if (mode == 1)
                        {
                            int lz = readbyte(bytes);
                            int loc = (lz & 3) + 1;
                            int num = (lz >> 2) + 2;
                            uint uVar4 = (uint)(head - loc);
                            for (int j = 0; j < num; j++)
                            {

                                int tmp_head = (int)(head & 0xfffffffe);
                                short uVar5 = (short)(((short)data[tmp_head] | (short)(data[tmp_head] << 8)) & 0xfffffffe);
                                short uVar2 = (short)(data[uVar4] | (short)(data[uVar4 + 1] << 8) & 0xfffffffe);
                                if ((uVar4 & 1) == 0)
                                {
                                    if ((head & 1) == 0)
                                    {
                                        data[tmp_head] = (byte)uVar5;
                                        data[tmp_head + 1] = ((byte)uVar2);
                                    }
                                    else
                                    {
                                        data[tmp_head] = (byte)(uVar2);
                                        data[tmp_head + 1] = (byte)(uVar5 >> 8);
                                    }
                                }
                                else
                                {
                                    if ((head & 1) == 0)
                                    {
                                        data[tmp_head] = (byte)(uVar2 >> 8);
                                        data[tmp_head + 1] = (byte)(uVar5 >> 8);
                                    }
                                    else
                                    {
                                        data[tmp_head] = (byte)(uVar5);
                                        data[tmp_head + 1] = (byte)(uVar2 >> 8);
                                    }
                                }
                                head++;
                            }
                        }
                        else if (mode == 2)
                        {
                            int tmp_head = (int)(head & 0xfffffffe);
                            if ((head & 1) == 0)
                            {
                                data[tmp_head] = (byte)readbyte(bytes);
                            }
                            else
                            {
                                data[tmp_head + 1] = (byte)readbyte(bytes);
                            }
                            head++;
                        }
                        else if (mode == 3)
                        {
                            for (int j = 0; j < mode; j++)
                            {
                                int tmp_head = (int)(head & 0xfffffffe);
                                if ((head & 1) == 0)
                                {
                                    data[tmp_head] = (byte)readbyte(bytes);
                                }
                                else
                                {
                                    data[tmp_head+1] = (byte)readbyte(bytes);
                                }
                                head++;
                            }
                        }
                    }
                }
            }

            return data.Take(total).ToArray();
        }

        private static byte[] RegularMalias2Decompression(List<byte> bytes)
        {
            short magic = (short)readshort(bytes);
            if (magic != 0x654c) throw new InvalidGraphicsException("Wrong magic.");
            int total = readint(bytes);
            if (total > 0x10000) throw new InvalidGraphicsException("Insane size: " + total.ToString("X8"));
            List<byte> data = new List<byte>();
            bool first = true;

            if (total > 0)
            {
                while (data.Count < total)
                {
                    byte modes = (byte)readbyte(bytes);
                    for (int i = 0; i < 4; i++)
                    {
                        byte mode = Convert.ToByte(((modes >> (i * 2 + 1)) % 2 << 1) + ((modes >> i * 2) % 2));
                        if (first && (mode == 0 || mode == 1)) throw new InvalidGraphicsException("Begins with mode " + mode.ToString("X2"));
                        first = false;
                        if (mode == 0)
                        {
                            int lz = readshort(bytes);
                            int loc = (lz & 0b111111111111) + 5;
                            int num = (lz >> 12) + 3;
                            for (int j = 0; j < num; j++)
                            {
                                data.Add(data[data.Count - loc]);
                            }
                        }
                        else if (mode == 1)
                        {
                            int lz = readbyte(bytes);
                            int loc = (lz & 0b11) + 1;
                            int num = (lz >> 2) + 2;
                            for (int j = 0; j < num; j++)
                            {
                                data.Add(data[data.Count - loc]);
                            }
                        }
                        else if (mode == 2)
                        {
                            data.Add((byte)readbyte(bytes));
                        }
                        else if (mode == 3)
                        {
                            for (int j = 0; j < mode; j++)
                            {
                                data.Add((byte)readbyte(bytes));
                            }
                        }
                    }
                }
            }

            return data.Take(total).ToArray();
        }

        public static ImageWrapper Decompress(byte[] start, int index, int memory_address)
        {
            //
            //  Stolen from: https://github.com/Sanqui/romhacking/blob/master/telefang/puneedle.py
            //  and converted to C#.
            //
            List<byte> bytes = start.ToList();
            List<byte> bytes2 = start.ToList();
            byte[] res;
            string compression_method;
            try
            {
                res = RegularMalias2Decompression(bytes);
                compression_method = "Malias2";
            }
            catch 
            {
                res = OtherMalias2Decompression(bytes2);
                compression_method = "Custom";
            }
            return new ImageWrapper(index, memory_address, res, compression_method);
        }
    }

    public class InvalidGraphicsException : Exception
    {
        public InvalidGraphicsException()
        {

        }

        public InvalidGraphicsException(string name) : base(name)
        {

        }
    }
}
