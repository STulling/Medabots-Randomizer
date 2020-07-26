using System;
using System.Collections.Generic;
using System.Text;

namespace MedabotsRandomizer
{
    public class EncountersWrapper
    {
        public int id;
        public int memory_location;
        public Encounters encounters;

        public EncountersWrapper(int id, int memory_location, Encounters encounters)
        {
            this.id = id;
            this.memory_location = memory_location;
            this.encounters = encounters;
        }

        public string MapId => ((byte)id).ToString("X2");
        public string Memory_Location => (memory_location + 0x8000000).ToString("X8");

        public string Map => IdTranslator.IdToMap((byte)id);

        public string Enc_1 => encounters.battle[0].ToString("X2");
        public string Enc_2 => encounters.battle[1].ToString("X2");
        public string Enc_3 => encounters.battle[2].ToString("X2");
        public string Enc_4 => encounters.battle[3].ToString("X2");
    }
}
