using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MedabotsRandomizer
{
    public class BattleWrapper : Wrapper<Battle>
    {
        public BattleWrapper(int id, int memory_location, byte[] data) : base(id, memory_location, data)
        {

        }
        public string FightId => ((byte)id).ToString("X2");
        public string Memory_Location => (memory_location + 0x8000000).ToString("X8");
        public string Character => IdTranslator.IdToCharacter(content.characterId);
        public string Bot_1 => IdTranslator.IdToBot(content.bots[0].head);
        public string Bot_2 => IdTranslator.IdToBot(content.bots[1].head);
        public string Bot_3 => IdTranslator.IdToBot(content.bots[2].head);
    }
}
