using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedabotsRandomizer
{
    public class Randomizer
    {
        List<BattleWrapper> battles;
        List<EncountersWrapper> encounters;
        List<PartWrapper> parts;
        Random rng;

        public Randomizer(List<BattleWrapper> battles, List<EncountersWrapper> encounters, List<PartWrapper> parts, int seed = 0)
        {
            this.battles = battles;
            this.encounters = encounters;
            this.parts = parts;
            if (seed != 0)
                rng = new Random(seed);
            else
                rng = new Random();
        }

        public Dictionary<byte, List<int>> findUniques(BattleBot[] bots, int num_bots)
        {
            Dictionary<byte, List<int>> uniques = new Dictionary<byte, List<int>>();
            for (int i = 0; i < num_bots; i++)
            {
                if (uniques.ContainsKey(bots[i].head))
                {
                    uniques[bots[i].head].Add(i);
                }
                else
                {
                    uniques.Add(bots[i].head, new List<int>() { i });
                }
            }
            return uniques;
        }

        public void RandomizeBattles(bool keep_team_structure, bool balanced_medal_level, float mixedchance, bool continuity)
        {
            if (keep_team_structure)
            {
                if (continuity)
                {
                    Dictionary<byte, List<int>> uniques = findCharacterOccurences(battles);
                    foreach (KeyValuePair<byte, List<int>> entry in uniques)
                    {
                        List<byte> diffBots = new List<byte>();
                        foreach (int i in entry.Value)
                        {
                            BattleWrapper battle = battles[i];
                            for (int botIndex = 0; botIndex < battle.battle.number_of_bots; botIndex++)
                            {
                                BattleBot bot = battle.battle.bots[botIndex];
                                if (!diffBots.Contains(bot.head))
                                    diffBots.Add(bot.head);
                            }
                        }
                        List<BattleBot> newBots = new List<BattleBot>();
                        foreach (byte i in diffBots)
                        {
                            newBots.Add(GenerateRandomBot(mixedchance));
                        }
                        foreach (int i in entry.Value)
                        {
                            BattleWrapper battle = battles[i];
                            for (int botIndex = 0; botIndex < battle.battle.number_of_bots; botIndex++)
                            {
                                BattleBot bot = battle.battle.bots[botIndex];
                                BattleBot newBot = newBots[diffBots.IndexOf(bot.head)];
                                if (balanced_medal_level)
                                    newBot.medal_level = bot.medal_level;
                                battle.battle.bots[botIndex] = newBot;
                            }
                        }
                    }
                }
                else
                {
                    foreach (BattleWrapper battle in battles)
                    {
                        Dictionary<byte, List<int>> uniques = findUniques(battle.battle.bots, battle.battle.number_of_bots);
                        foreach (KeyValuePair<byte, List<int>> entry in uniques)
                        {
                            BattleBot newBot = GenerateRandomBot(mixedchance);
                            foreach (int i in entry.Value)
                            {
                                if (balanced_medal_level)
                                    newBot.medal_level = battle.battle.bots[i].medal_level;
                                battle.battle.bots[i] = newBot;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (BattleWrapper battle in battles)
                {
                    for (int i = 0; i < battle.battle.number_of_bots; i++)
                    {
                        BattleBot newBot = GenerateRandomBot(mixedchance);
                        if (balanced_medal_level)
                            newBot.medal_level = battle.battle.bots[i].medal_level;
                        battle.battle.bots[i] = newBot;
                    }
                }
            }
        }

        public Dictionary<byte, List<int>> findCharacterOccurences(List<BattleWrapper> battlelist)
        {
            Dictionary<byte, List<int>> uniques = new Dictionary<byte, List<int>>();
            foreach (BattleWrapper battle in battlelist)
            {
                if (uniques.ContainsKey(battle.battle.characterId))
                {
                    uniques[battle.battle.characterId].Add(Convert.ToByte(battle.FightId, 16));
                }
                else
                {
                    uniques.Add(battle.battle.characterId, new List<int>() { Convert.ToByte(battle.FightId, 16) });
                }
            }
            return uniques;
        }

        public void RandomizeCharacters(bool continuity)
        {
            if (continuity)
            {
                Dictionary<byte, List<int>> uniques = findCharacterOccurences(battles);
                List<int> possibleChars = Enumerable.Range(1, 0x5f).ToList();
                foreach (KeyValuePair<byte, List<int>> entry in uniques)
                {
                    int index = rng.Next(possibleChars.Count);
                    byte character = (byte)possibleChars[index];
                    foreach (int id in entry.Value)
                    {
                        battles[id].battle.characterId = character;
                    }
                    possibleChars.RemoveAt(index);
                }
            }
            else
            {
                foreach (BattleWrapper battle in battles)
                {
                    battle.battle.characterId = (byte)rng.Next(1, 0x60);
                }
            }
        }

        public BattleBot GenerateRandomBot(float mixedchance)
        {
            BattleBot bot = new BattleBot();
            bot.unknown = 1;
            if (mixedchance != 0 && mixedchance >= rng.NextDouble())
            {
                bot.head = (byte)rng.Next(0, 0x78);
                bot.left_arm = (byte)rng.Next(0, 0x78);
                bot.right_arm = (byte)rng.Next(0, 0x78);
                bot.legs = (byte)rng.Next(0, 0x78);
                bot.medal = parts[bot.head * 4].part.medal_compatibility;
                bot.medal_level = (byte)rng.Next(1, 101);
            }
            else
            {
                byte set = (byte)rng.Next(0, 0x78);
                byte medal = parts[set * 4].part.medal_compatibility;
                bot.head = set;
                bot.left_arm = set;
                bot.right_arm = set;
                bot.legs = set;
                bot.medal = medal;
                bot.medal_level = (byte)rng.Next(1, 101);
            }
            return bot;
        }
    }
}
