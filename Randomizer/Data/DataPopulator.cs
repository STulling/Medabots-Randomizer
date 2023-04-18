using System;
using System.Collections.Generic;

namespace MedabotsRandomizer.Data
{
	public static class DataPopulator
    {
        public static List<T> Populate_Data<T>(byte[] file, int amount, int size, int offset, bool is_ptr)
        {
            List<T> result = new List<T>();
            for (int i = 0; i <= amount; i++)
            {
                int address;
                if (is_ptr)
                {
                    address = Utils.GetAdressAtPosition(file, offset + 4 * i);
                }
                else
                {
                    address = offset + size * i;
                }
                byte[] slice = new byte[size];
                Array.Copy(file, address, slice, 0, size);
                result.Add((T)Activator.CreateInstance(typeof(T), new object[] { i, address, slice }));
            }
            return result;
        }
    }
}
