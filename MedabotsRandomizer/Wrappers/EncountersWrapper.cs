using System;
using System.Collections.Generic;
using System.Text;

namespace MedabotsRandomizer
{
    public class EncountersWrapper : Wrapper<Encounters>
    {
        public EncountersWrapper(int id, int memory_location, byte[] data) : base(id, memory_location, data)
        {

        }

        public string MapId => ((byte)id).ToString("X2");
        public string Memory_Location => (memory_location + 0x8000000).ToString("X8");
        public string Map => IdTranslator.IdToMap((byte)id);
        public string Enc_1 => content.battle[0].ToString("X2");
        public string Enc_2 => content.battle[1].ToString("X2");
        public string Enc_3 => content.battle[2].ToString("X2");
        public string Enc_4 => content.battle[3].ToString("X2");
    }
}
