using MedabotsRandomizer.Util;

namespace MedabotsRandomizer.Data.Wrappers
{
    public abstract class Wrapper<T>
    {
        public int id;
        public int memory_location;
        public T content;

        public Wrapper(int id, int memory_location, byte[] data)
        {
            this.id = id;
            this.memory_location = memory_location;
            this.content = StructUtils.FromBytes<T>(data);
        }
    }
}
