using System;

namespace MedabotsRandomizer
{

    public class PartWrapper : Wrapper<Part>
    {
        public PartWrapper(int id, int memory_location, byte[] data) : base(id, memory_location, data)
        {

        }
        public string PartId => ((byte)Math.Floor(((double)id / 4))).ToString("X2");
        public string Memory_Location => (memory_location + 0x8000000).ToString("X8");
        public string Name => IdTranslator.IdToPart((byte)Math.Floor(((double)id / 4)), id % 4);
        public string Type
        {
            get
            {
                switch (id % 4)
                {
                    case 0:
                        return "Head";
                    case 1:
                        return "Right Arm";
                    case 2:
                        return "Left Arm";
                    default:
                        return "Legs";
                }
            }
        }
    }
}
