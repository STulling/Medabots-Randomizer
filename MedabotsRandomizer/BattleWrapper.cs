using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MedabotsRandomizer
{
    public class BattleWrapper
    {
        int id;
        int memory_location;
        public Battle battle;

        public BattleWrapper(int id, int memory_location, Battle battle)
        {
            this.id = id;
            this.memory_location = memory_location;
            this.battle = battle;
        }

        public string FightId => ((byte)id).ToString("X2");
        public string Memory_Location => (memory_location + 0x8000000).ToString("X8");

        public string Character => IdTranslator.IdToCharacter(battle.characterId);
        public string Bot_1 => IdTranslator.IdToBot(battle.bots[0].head);
        public string Bot_2 => IdTranslator.IdToBot(battle.bots[1].head);
        public string Bot_3 => IdTranslator.IdToBot(battle.bots[2].head);
    }
}
