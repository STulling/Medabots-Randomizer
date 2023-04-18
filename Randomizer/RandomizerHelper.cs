using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MedabotsRandomizer
{
    public class RandomizerHelper
    {
        List<BattleWrapper> battles;
        List<EncountersWrapper> encounters;
        List<PartWrapper> parts;
        Random rng;

        List<int> randomizedMedals;
        public Dictionary<byte, byte> medalExchanges;
        public int starterMedal;

        public RandomizerHelper(List<BattleWrapper> battles, List<EncountersWrapper> encounters, List<PartWrapper> parts, Random rng)
        {
            this.battles = battles;
            this.encounters = encounters;
            this.parts = parts;
            this.rng = rng;
            this.randomizedMedals = new List<int>();
            this.medalExchanges = new Dictionary<byte, byte>();
        }

        private Dictionary<byte, List<int>> findUniques(BattleBot[] bots, int num_bots)
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

                            for (int botIndex = 0; botIndex < battle.content.number_of_bots; botIndex++)
                            {
                                BattleBot bot = battle.content.bots[botIndex];

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

                            for (int botIndex = 0; botIndex < battle.content.number_of_bots; botIndex++)
                            {
                                BattleBot bot = battle.content.bots[botIndex];
                                BattleBot newBot = newBots[diffBots.IndexOf(bot.head)];

                                if (balanced_medal_level)
                                    newBot.medal_level = bot.medal_level;

                                battle.content.bots[botIndex] = newBot;
                            }
                        }
                    }
                }
                else
                {
                    foreach (BattleWrapper battle in battles)
                    {
                        Dictionary<byte, List<int>> uniques = findUniques(battle.content.bots, battle.content.number_of_bots);

                        foreach (KeyValuePair<byte, List<int>> entry in uniques)
                        {
                            BattleBot newBot = GenerateRandomBot(mixedchance);

                            foreach (int i in entry.Value)
                            {
                                if (balanced_medal_level)
                                    newBot.medal_level = battle.content.bots[i].medal_level;

                                battle.content.bots[i] = newBot;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (BattleWrapper battle in battles)
                {
                    for (int i = 0; i < battle.content.number_of_bots; i++)
                    {
                        BattleBot newBot = GenerateRandomBot(mixedchance);

                        if (balanced_medal_level)
                            newBot.medal_level = battle.content.bots[i].medal_level;

                        battle.content.bots[i] = newBot;
                    }
                }
            }
        }

        private Dictionary<byte, List<int>> findCharacterOccurences(List<BattleWrapper> battlelist)
        {
            Dictionary<byte, List<int>> uniques = new Dictionary<byte, List<int>>();

            foreach (BattleWrapper battle in battlelist)
            {
                if (uniques.ContainsKey(battle.content.characterId))
                {
                    uniques[battle.content.characterId].Add(Convert.ToByte(battle.FightId, 16));
                }
                else
                {
                    uniques.Add(battle.content.characterId, new List<int>() { Convert.ToByte(battle.FightId, 16) });
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
                        battles[id].content.characterId = character;
                    }

                    possibleChars.RemoveAt(index);
                }
            }
            else
            {
                foreach (BattleWrapper battle in battles)
                {
                    battle.content.characterId = (byte)rng.Next(1, 0x60);
                }
            }
        }

        private byte getBestMedal(BattleBot bot)
        {
            return IdTranslator.botMedal(bot.head);
        }

        public void fixSoftlock()
        {
            BattleWrapper odoroBattle = battles[0xA4];

            byte firstFixedPart = 0x0;
            if (odoroBattle.content.bots[0].isComplete())
            {
                odoroBattle.content.bots[0].head = firstFixedPart;
                odoroBattle.content.bots[0].right_arm = firstFixedPart;
                odoroBattle.content.bots[0].left_arm = firstFixedPart;
                odoroBattle.content.bots[0].legs = firstFixedPart;
                odoroBattle.content.bots[0].medal = IdTranslator.botMedal(firstFixedPart);
            }
            else
            {
                odoroBattle.content.bots[0].head = firstFixedPart;
            }

            byte secondFixedPart = 0x4;
            if (odoroBattle.content.bots[1].isComplete())
            {
                odoroBattle.content.bots[1].head = secondFixedPart;
                odoroBattle.content.bots[1].right_arm = secondFixedPart;
                odoroBattle.content.bots[1].left_arm = secondFixedPart;
                odoroBattle.content.bots[1].legs = secondFixedPart;
                odoroBattle.content.bots[1].medal = IdTranslator.botMedal(secondFixedPart);
            }
            else
            {
                odoroBattle.content.bots[1].right_arm = secondFixedPart;
            }

            BattleWrapper kappaBattle = battles[0x39];
            int i = rng.Next(0, 3);

            byte kappaFixedPart = 0x6C;
            if (kappaBattle.content.bots[i].isComplete())
            {
                kappaBattle.content.bots[i].head = kappaFixedPart;
                kappaBattle.content.bots[i].right_arm = kappaFixedPart;
                kappaBattle.content.bots[i].left_arm = kappaFixedPart;
                kappaBattle.content.bots[i].legs = kappaFixedPart;
                kappaBattle.content.bots[i].medal = IdTranslator.botMedal(kappaFixedPart);
            }
            else
            {
                kappaBattle.content.bots[i].head = kappaFixedPart;
            }
        }

        private BattleBot GenerateRandomBot(float mixedchance)
        {
            BattleBot bot = new BattleBot();
            bot.unknown = 1;

            if (mixedchance != 0 && mixedchance >= rng.NextDouble())
            {
                bot.head = (byte)rng.Next(0, 0x78);
                bot.left_arm = (byte)rng.Next(0, 0x78);
                bot.right_arm = (byte)rng.Next(0, 0x78);
                bot.legs = (byte)rng.Next(0, 0x78);
                bot.medal_level = (byte)rng.Next(1, 100);
            }
            else
            {
                byte set = (byte)rng.Next(0, 0x78);
                bot.head = set;
                bot.left_arm = set;
                bot.right_arm = set;
                bot.legs = set;
                bot.medal_level = (byte)rng.Next(1, 100);
            }

            bot.medal = getBestMedal(bot);
            return bot;
        }

        public byte GetRandomMedal(byte medal)
        {
            var medalId = (byte)rng.Next(0, 0x1E);

            while (true)
            {
                //new medal
                if (!randomizedMedals.Contains(medalId) && medalId != starterMedal)
                {
                    randomizedMedals.Add(medalId);
                    medalExchanges.Add(medal, medalId);
                    return medalId;
                }

                //pick next closest medal to randomly selected, otherwise we would have to wait for rng to pick a new one itself
                                //29 - max id of medal
                if (medalId == 0x1D)
                {
                    medalId = 0x00;
                }
                else
                {
                    medalId++;
                }
            }
        }
    }
}
