using System;
using System.Collections.Generic;
using System.Text;

namespace MedabotsRandomizer
{
    public static class DataPopulator
    {
        public static void Populate_Battles(byte[] file, int battle_offset, List<BattleWrapper> battles)
        {
            int amount_of_battles = 0xf5;
            int battle_size = 0x28;
            for (int i = 0; i <= amount_of_battles; i++)
            {
                int battle_address = Utils.GetAdressAtPosition(file, battle_offset + 4 * i);
                byte[] slice = new byte[battle_size];
                Array.Copy(file, battle_address, slice, 0, battle_size);
                Battle battle = Battle.FromBytes(slice);
                battles.Add(new BattleWrapper(i, battle_address, battle));
            }
        }

        public static void Populate_Parts(byte[] file, int part_offset, List<PartWrapper> parts)
        {
            int amount_of_parts = 480;
            int part_size = 0x10;
            for (int i = 0; i < amount_of_parts; i++)
            {
                int part_address = part_offset + part_size * i;
                byte[] slice = new byte[part_size];
                Array.Copy(file, part_address, slice, 0, part_size);
                Part part = Part.FromBytes(slice);
                parts.Add(new PartWrapper(i, part_address, part));
            }
        }

        public static void Populate_Encounters(byte[] file, int encounter_offset, List<EncountersWrapper> encounterlist)
        {
            int amount_of_maps = 0xbf;
            int encounters_size = 0x10;
            for (int i = 0; i <= amount_of_maps; i++)
            {
                int encounters_address = encounter_offset + 4 * i;
                byte[] slice = new byte[encounters_size];
                Array.Copy(file, encounters_address, slice, 0, encounters_size);
                Encounters encounters = Encounters.FromBytes(slice);
                encounterlist.Add(new EncountersWrapper(i, encounters_address, encounters));
            }
        }
    }
}
