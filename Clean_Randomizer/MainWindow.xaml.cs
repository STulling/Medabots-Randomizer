using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MedabotsRandomizer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Clean_Randomizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            hashes = new Dictionary<string, string>
            {
                { "MEDABOTSRKSVA9BPE9", "Medabots Rokusho Version (E)" },
                { "MEDABOTSRKSVA9BEE9", "Medabots Rokusho Version (U)" },
                { "MEDABOTSMTBVA8BEE9", "Medabots Metabee Version (U)" },
                { "MEDABOTSMTBVA8BPE9", "Medabots Metabee Version (E)" }
            };
            memory_offsets = new Dictionary<string, Dictionary<string, int>>
            {
                { "MEDABOTSRKSVA9BPE9", new Dictionary<string, int>{
                    { "Battles", 0x3c1ba0 },
                    { "Encounters", 0x3bf230 },
                    { "Parts", 0x3b841c },
                    { "Starter", 0x7852f4},
                    { "StartMedal", 0x78549c}
                }},
                { "MEDABOTSRKSVA9BEE9", new Dictionary<string, int>{
                    { "Battles", 0x3c1a00 },
                    { "Encounters", 0x3bf090 },
                    { "Parts", 0x3b827c },
                    { "Starter", 0x7840c0},
                    { "StartMedal", 0x784268}
                }},
                { "MEDABOTSMTBVA8BEE9", new Dictionary<string, int>{
                    { "Battles", 0x3c19e0 },
                    { "Encounters", 0x3bf070 },
                    { "Parts", 0x3b825c },
                    { "Starter", 0x78409B},
                    { "StartMedal", 0x784243}
                }},
                { "MEDABOTSMTBVA8BPE9", new Dictionary<string, int>{
                    { "Battles", 0x3c1b80 },
                    { "Encounters", 0x3bf210 },
                    { "Parts", 0x3b83fc },
                    { "Starter", 0x7852cf},
                    { "StartMedal", 0x785477}
                }}
            };
            allBattles = new List<BattleWrapper>();
            allEncounters = new List<EncountersWrapper>();
            allParts = new List<PartWrapper>();
        }

        byte[] file;
        Dictionary<string, string> hashes;
        Dictionary<string, Dictionary<string, int>> memory_offsets;
        List<BattleWrapper> allBattles;
        List<EncountersWrapper> allEncounters;
        List<PartWrapper> allParts;
        Randomizer randomizer;
        string game_id;

        private void PopulateData(string id_string)
        {
            allBattles = DataPopulator.Populate_Data<BattleWrapper>(file, 0xf5, 0x28, memory_offsets[id_string]["Battles"], true);
            allEncounters = DataPopulator.Populate_Data<EncountersWrapper>(file, 0xbf, 4, memory_offsets[id_string]["Encounters"], false);
            allParts = DataPopulator.Populate_Data<PartWrapper>(file, 480, 0x10, memory_offsets[id_string]["Parts"], false);
        }

        private async void ShowNotification(string big, string error)
        {
            MetroDialogSettings mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "OK",
                ColorScheme = MetroDialogOptions.ColorScheme
            };

            MessageDialogResult result = await this.ShowMessageAsync(big, error, MessageDialogStyle.Affirmative, mySettings);
        }

        private void Load_ROM(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                allBattles.Clear();
                allEncounters.Clear();
                allParts.Clear();
                string chosenFile = openFileDialog.FileName;
                file = File.ReadAllBytes(chosenFile);
                byte[] id_bytes = new byte[0x12];
                Array.Copy(file, 0xa0, id_bytes, 0, 0x12);
                string id_string = Encoding.Default.GetString(id_bytes);
                game_id = id_string;
                if (id_string.Contains("MEDACORE"))
                {
                    file = null;
                    ShowNotification("Error!", "Please select an English Medabots ROM\nThe game id corresponds with a Japanese ROM, which is not supported.");
                    return;
                }
                if (!id_string.Contains("MEDABOTS"))
                {
                    file = null;
                    ShowNotification("Error!", "Please select a Medabots ROM\nThe game id does not correspond to any Medabots ROM.");
                    return;
                }
                if (hashes.TryGetValue(id_string, out string recognizedFile))
                {
                    romLabel.Content = recognizedFile;
                }
                else
                {
                    romLabel.Content = "Unknown ROM";
                }
            }
        }

        private void Randomize(object sender, RoutedEventArgs e)
        {
            if (file == null)
            {
                ShowNotification("Error!", "Please select a ROM befora applying.");
                return;
            }
            PopulateData(game_id);
            Random rng;
            if (seed_input.Text != "")
            {
                MD5 md5Hasher = MD5.Create();
                byte[] hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(seed_input.Text));
                int ivalue = BitConverter.ToInt32(hashed, 0);
                rng = new Random(ivalue);
            }
            else
            {
                rng = new Random();
            }
            randomizer = new Randomizer(allBattles, allEncounters, allParts, rng);
            if (chk_randomize_battles.IsOn)
            {
                float mixedchance = 0;
                if (chk_allow_mixed_bots.IsOn)
                    mixedchance = (float)(sl_mixed_bots.Value / 100);
                bool keep_team_structure = chk_keep_battle_structure.IsOn;
                bool balanced_medal_level = chk_balanced_bot_levels.IsOn;
                bool keep_battle_continuity = chk_battle_continuity.IsOn;
                randomizer.RandomizeBattles(keep_team_structure, balanced_medal_level, mixedchance, keep_battle_continuity);
            }
            if (chk_randomize_characters.IsOn)
            {
                bool continuity = chk_character_continuity.IsOn;
                randomizer.RandomizeCharacters(continuity);
            }
            int amount_of_battles = 0xf5;
            int battle_size = 0x28;
            for (int i = 0; i <= amount_of_battles; i++)
            {
                int battle_address = MedabotsRandomizer.Utils.GetAdressAtPosition(file, memory_offsets[game_id]["Battles"] + 4 * i);
                byte[] battle = StructUtils.getBytes(allBattles[i].content);
                Array.Copy(battle, 0, file, battle_address, battle_size);
            }
            if (chk_code_patches.IsOn)
            {
                uint jumpOffset = 0x104;
                uint hookOffset = 0x7f3530;
                uint trainerOffset = 0x7f3600;
                uint instr1 = (uint)MedabotsRandomizer.Utils.GetIntAtPosition(file, (int)jumpOffset);
                uint instr2 = (uint)MedabotsRandomizer.Utils.GetIntAtPosition(file, (int)jumpOffset + 4);
                uint instr3 = (uint)MedabotsRandomizer.Utils.GetIntAtPosition(file, (int)jumpOffset + 8);
                uint[] jumpPayload = new uint[]
                {
                    0xE92D8000,                         // push r15
                    0xE51FF004,                         // ldr r15, traineraddr
                    0x08000000 + hookOffset             // hookOffset
                };
                uint[] hookPayload = new uint[]
                {
                    0xE92D4000,                         // push r14
                    0xE3A0E402,                         // mov r14, #0x2000000
                    0xE28EE701,                         // add r14, #40000
                    0xE24EE004,                         // sub r14, #28
                    0xE90E08FF,                         // stmdb [r14], r0-r7, r11
                    0xEB00002D,                         // bl trainerfunc
                    0xE3A0E402,                         // mov r14, #0x2000000
                    0xE28EE701,                         // add r14, #40000
                    0xE24EE028,                         // sub r14, #28
                    0xE89E08FF,                         // ldmia [r14], r0-r7, r11
                    0xE8BD4000,                         // pop r14
                    instr1,                             // --- original instruction #1 ---
                    instr2,                             // --- original instruction #2 ---
                    instr3,                             // --- original instruction #3 ---
                    0xE8BD8000                          // pop r15
                };
                uint[] trainerPayload = new uint[]
                {
                    // Set text_speed to instant
                    0xE3A01403,                         // mov r1, #0x3000000
                    0xE3A000FF,                         // mov r0, #0xFF
                    0xE5C1045A,                         // strb r0, [r1, #0x45A]
                    // Return
                    0xE12FFF1E                          // bx r15
                };
                Dictionary<uint, ushort> codePatches = new Dictionary<uint, ushort>
                {
                    // Instant Character Popup
                    { 0x3F5F6, 0x3008 },
                    { 0x3F600, 0xDC08 }
                };
                MedabotsRandomizer.Utils.WritePayload(file, jumpOffset, jumpPayload);
                MedabotsRandomizer.Utils.WritePayload(file, hookOffset, hookPayload);
                MedabotsRandomizer.Utils.WritePayload(file, trainerOffset, trainerPayload);
                MedabotsRandomizer.Utils.WritePatches(file, codePatches);
            }

            byte[] blacklist = new byte[]{1, 3, 6, 7, 8,
                                            14, 15, 17, 18,
                                            19, 20, 22, 23,
                                            25, 26, 27, 28,
                                            39, 40, 45, 50,
                                            57, 66, 72, 75,
                                            77, 80, 81, 82,
                                            84, 90, 91, 92,
                                            96, 100, 101, 104,
                                            110, 115, 117, 118};

            if (chk_randomize_starter.IsOn)
            {
                byte randomBot = (byte)rng.Next(0, 0x78);
                while (blacklist.Contains(randomBot))
                {
                    randomBot = (byte)rng.Next(0, 0x78);
                }
                byte medal = IdTranslator.botMedal(randomBot);
                int offset = memory_offsets[game_id]["Starter"];
                uint funcOffset = 0x044b6c;
                for (int i = 0; i < 4; i++)
                {
                    file[offset + 4 * i] = randomBot;
                }
                if (IdTranslator.isFemale(randomBot))
                {
                    file[offset + 16] = 1;
                }
                file[memory_offsets[game_id]["StartMedal"]] = medal;
                file[funcOffset] = randomBot;
                if (game_id == "MEDABOTSRKSVA9BPE9" || game_id == "MEDABOTSRKSVA9BEE9")
                {
                    file[funcOffset + 0xE] = (byte)(randomBot * 2 + 1);
                    file[funcOffset - 0xE] = medal;
                    MedabotsRandomizer.Utils.WriteInt(file, funcOffset + 0x34, (uint)(randomBot * 2 + 1) + 0xf0);
                    MedabotsRandomizer.Utils.WriteInt(file, funcOffset + 0x38, (uint)(randomBot * 2 + 1) + 3 * 0xf0);
                }
                else
                {
                    file[funcOffset + 0xE] = (byte)(randomBot * 2 + 1);
                    file[funcOffset - 0x4] = medal;
                    MedabotsRandomizer.Utils.WriteInt(file, funcOffset + 0x34, (uint)(randomBot * 2 + 1) + 0xf0);
                    MedabotsRandomizer.Utils.WriteInt(file, funcOffset + 0x38, (uint)(randomBot * 2 + 1) + 3 * 0xf0);
                }
            }

            File.WriteAllBytes("Converted_Medabots.gba", file);
            ShowNotification("Done!", "The ROM has been converted and is saved as \"Converted_Medabots.gba\"");
        }
    }
}
