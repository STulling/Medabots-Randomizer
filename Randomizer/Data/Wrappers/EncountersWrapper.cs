using MedabotsRandomizer.Util;

namespace MedabotsRandomizer.Data.Wrappers
{
    public class EncountersWrapper : Wrapper<Encounters>
    {
        public EncountersWrapper(int id, int memory_location, byte[] data) : base(id, memory_location, data)
        {

        }

        public string Map => IdTranslator.IdToMap((byte)id);
        public string Enc_1 => content.battle[0].ToString("X2");
        public string Enc_2 => content.battle[1].ToString("X2");
        public string Enc_3 => content.battle[2].ToString("X2");
        public string Enc_4 => content.battle[3].ToString("X2");
    }
}
