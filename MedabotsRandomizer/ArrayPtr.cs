using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MedabotsRandomizer
{
    public class ArrayPtr
    {
        byte[] array;
        int ptr;

        public ArrayPtr(byte[] array, int ptr)
        {
            this.array = array;
            this.ptr = ptr;
        }

        public ArrayPtr Copy()
        {
            return new ArrayPtr(array, ptr);
        }

        public int readint()
        {
            int result = 0;
            result += array[ptr];
            ptr++;
            result += array[ptr] << 8;
            ptr++;
            result += array[ptr] << 16;
            ptr++;
            result += array[ptr] << 24;
            ptr++;
            return result;
        }

        public int readshort()
        {
            int result = 0;
            result += array[ptr];
            ptr++;
            result += array[ptr] << 8;
            ptr++;
            return result;
        }

        public int readbyte()
        {
            int result = 0;
            result += array[ptr];
            ptr++;
            return result;
        }
    }
}
