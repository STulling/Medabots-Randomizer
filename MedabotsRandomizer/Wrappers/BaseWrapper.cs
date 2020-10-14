using System;
using System.Collections.Generic;
using System.Text;

namespace MedabotsRandomizer
{
    public abstract class Wrapper<T>
    {
        protected int id;
        protected int memory_location;
        public T content;

        public Wrapper(int id, int memory_location, byte[] data)
        {
            this.id = id;
            this.memory_location = memory_location;
            this.content = StructUtils.FromBytes<T>(data);
        }
    }
}
