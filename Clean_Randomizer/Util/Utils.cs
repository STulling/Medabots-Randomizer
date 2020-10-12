using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace MedabotsRandomizer
{
    public static class Utils
    {
        // Return a byte array as a sequence of hex values.
        public static string BytesToString(byte[] bytes)
        {
            string result = "";
            foreach (byte b in bytes) result += b.ToString("x2");
            return result;
        }

        // Compute the file's hash.
        public static byte[] GetHashSha256(string filename)
        {
            SHA256 Sha256 = SHA256.Create();
            using (FileStream stream = File.OpenRead(filename))
            {
                return Sha256.ComputeHash(stream);
            }
        }

        public static void WriteShort(byte[] bytes, uint offset, ushort opcode)
        {
            byte[] bytez = BitConverter.GetBytes(opcode);
            bytes[offset] = bytez[0];
            bytes[offset + 1] = bytez[1];
        }

        public static void WritePatches(byte[] bytes, Dictionary<uint, ushort> codePatches)
        {
            foreach (KeyValuePair<uint, ushort> entry in codePatches)
            {
                WriteShort(bytes, entry.Key, entry.Value);
            }
        }

        public static void WriteInt(byte[] bytes, uint offset, uint opcode)
        {
            byte[] bytez = BitConverter.GetBytes(opcode);
            bytes[offset] = bytez[0];
            bytes[offset + 1] = bytez[1];
            bytes[offset + 2] = bytez[2];
            bytes[offset + 3] = bytez[3];
        }

        public static void WritePayload(byte[] bytes, uint offset, uint[] payload)
        {
            for (uint i = 0; i < payload.Length; i++)
            {
                WriteInt(bytes, offset + 4 * i, payload[i]);
            }
        }

        public static string GetHash(string filename)
        {
            return BytesToString(GetHashSha256(filename));
        }

        public static int GetAdressAtPosition(byte[] bytes, int address)
        {
            int new_address = 0;
            new_address += (bytes[address + 3] << 24);
            new_address += (bytes[address + 2] << 16);
            new_address += (bytes[address + 1] << 8);
            new_address += bytes[address];
            return new_address - 0x08000000;
        }

        public static int GetIntAtPosition(byte[] bytes, int address)
        {
            int read_int = 0;
            read_int += (bytes[address + 3] << 24);
            read_int += (bytes[address + 2] << 16);
            read_int += (bytes[address + 1] << 8);
            read_int += bytes[address];
            return read_int;
        }

        public static void Shuffle<T>(IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static int Search(byte[] haystack, byte[] needle)
        {
            for (int i = 0; i <= haystack.Length - needle.Length; i++)
            {
                if (match(haystack, needle, i))
                {
                    return i;
                }
            }
            return -1;
        }

        private static bool match(byte[] haystack, byte[] needle, int start)
        {
            if (needle.Length + start > haystack.Length)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < needle.Length; i++)
                {
                    if (needle[i] != haystack[i + start])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public static string RandomString(int length)
        {
            Random random = new Random();

            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
