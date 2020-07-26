using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MedabotsRandomizer
{
    public static class CompressionUtils
    {
        private static byte[] OtherMalias2Decompression(ArrayPtr bytes, out byte[] data)
        {
            short magic = (short)bytes.readshort();
            if (magic != 0x654c) throw new InvalidGraphicsException("Wrong magic.");
            int total = bytes.readint();
            if (total > 0x10000) throw new InvalidGraphicsException("Insane size: " + total.ToString("X8"));
            data = new byte[total];
            int head = 0;
            bool first = true;

            if (total > 0)
            {
                while (head < total)
                {
                    byte modes = (byte)bytes.readbyte();
                    for (byte i = 0; i < 4; i++)
                    {
                        if (head >= total) break;
                        byte mode = Convert.ToByte(((modes >> (i * 2 + 1)) % 2 << 1) + ((modes >> i * 2) % 2));
                        if (first && (mode == 0 || mode == 1)) throw new InvalidGraphicsException("Begins with mode " + mode.ToString("X2"));
                        first = false;
                        if (mode == 0)
                        {
                            int lz = bytes.readshort();
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
                            int lz = bytes.readbyte();
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
                                data[tmp_head] = (byte)bytes.readbyte();
                            }
                            else
                            {
                                data[tmp_head + 1] = (byte)bytes.readbyte();
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
                                    data[tmp_head] = (byte)bytes.readbyte();
                                }
                                else
                                {
                                    data[tmp_head+1] = (byte)bytes.readbyte();
                                }
                                head++;
                            }
                        }
                    }
                }
            }

            return data.Take(total).ToArray();
        }

        private static bool RegularMalias2Decompression(ArrayPtr bytes, out byte[] data)
        {
            short magic = (short)bytes.readshort();
            if (magic != 0x654c) throw new InvalidGraphicsException("Wrong magic.");
            int total = bytes.readint();
            if (total > 0x10000) throw new InvalidGraphicsException("Insane size: " + total.ToString("X8"));
            data = new byte[total];
            int head = 0;
            bool first = true;

            if (total > 0)
            {
                while (head < total)
                {
                    byte modes = (byte)bytes.readbyte();
                    for (int i = 0; i < 4; i++)
                    {
                        byte mode = Convert.ToByte(((modes >> (i * 2 + 1)) % 2 << 1) + ((modes >> i * 2) % 2));
                        if (first && (mode == 0 || mode == 1)) throw new InvalidGraphicsException("Begins with mode " + mode.ToString("X2"));
                        first = false;
                        if (mode == 0)
                        {
                            int lz = bytes.readshort();
                            int loc = (lz & 0b111111111111) + 5;
                            int num = (lz >> 12) + 3;
                            if (head - loc < 0 || head >= total) return false;
                            for (int j = 0; j < num; j++)
                            {
                                data[head] = data[head - loc];
                                head++;
                            }
                        }
                        else if (mode == 1)
                        {
                            int lz = bytes.readbyte();
                            int loc = (lz & 0b11) + 1;
                            int num = (lz >> 2) + 2;
                            if (head - loc < 0 || head >= total) return false;
                            for (int j = 0; j < num; j++)
                            {
                                data[head] = data[head - loc];
                                head++;
                            }
                        }
                        else if (mode == 2)
                        {
                            data[head] = (byte)bytes.readbyte();
                            head++;
                        }
                        else if (mode == 3)
                        {
                            for (int j = 0; j < mode; j++)
                            {
                                data[head] = (byte)bytes.readbyte();
                                head++;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public static ImageWrapper Decompress(ArrayPtr bytes, int index, int memory_address)
        {
            //
            //  Stolen from: https://github.com/Sanqui/romhacking/blob/master/telefang/puneedle.py
            //  and converted to C#.
            //
            ArrayPtr bytes2 = bytes.Copy();
            string compression_method;
            if (RegularMalias2Decompression(bytes, out byte[] res))
            {
                compression_method = "Malias2";
            }
            else
            {
                OtherMalias2Decompression(bytes2, out res);
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
