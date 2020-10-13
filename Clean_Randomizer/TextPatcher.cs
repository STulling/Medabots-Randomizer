using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Randomizer
{
    public class TextPatcher
    {
        Dictionary<(int, int), byte[]> messages;
        Dictionary<(int, int), (byte[], int)> storedMessages;
        int offset;
        int dumpOffset;
        byte[] file;
        public TextPatcher(ref byte[] file, int offset, int dumpOffset, Dictionary<(int, int), byte[]> messages)
        {
            this.file = file;
            this.offset = offset;
            this.messages = messages;
            this.dumpOffset = dumpOffset;
            this.storeText();
        }

        private void storeText()
        {
            storedMessages = new Dictionary<(int, int), (byte[], int)>();
            foreach (KeyValuePair<(int, int), byte[]> entry in messages)
            {
                storedMessages.Add(entry.Key, (entry.Value, dumpOffset));
                Array.Copy(entry.Value, 0, file, dumpOffset, entry.Value.Length);
                this.dumpOffset += entry.Value.Length;
            }
        }

        public void PatchText()
        {
            foreach (KeyValuePair<(int, int), (byte[], int)> entry in storedMessages)
            {
                int originalTextPointer = findPointer(entry.Key);
                Utils.WriteInt(file, (uint)originalTextPointer, (uint)entry.Value.Item2 + 0x8000000);
            }
        }

        private int findPointer((int, int) id)
        {
            int subAdress = Utils.GetAdressAtPosition(file, this.offset + 4 * id.Item1);
            int actualAdress = subAdress + 4 * id.Item2;
            return actualAdress;
        }
    }
}
