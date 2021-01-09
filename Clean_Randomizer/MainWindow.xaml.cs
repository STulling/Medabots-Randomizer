using Clean_Randomizer.Util;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MedabotsRandomizer;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                    { "Starter", 0x7852f4},
                    { "Text", 0x47df44}
                }},
                { "MEDABOTSRKSVA9BEE9", new Dictionary<string, int>{
                    { "Battles", 0x3c1a00 },
                    { "Starter", 0x7840c0},
                    { "Text", 0x47e45c}
                }},
                { "MEDABOTSMTBVA8BEE9", new Dictionary<string, int>{
                    { "Battles", 0x3c19e0 },
                    { "Starter", 0x78409B},
                    { "Text", 0x47e43c}
                }},
                { "MEDABOTSMTBVA8BPE9", new Dictionary<string, int>{
                    { "Battles", 0x3c1b80 },
                    { "Starter", 0x7852cf},
                    { "Text", 0x47df24}

                }}
            };

            allBattles = new List<BattleWrapper>();
            allEncounters = new List<EncountersWrapper>();
            allParts = new List<PartWrapper>();
            List<string> bots = IdTranslator.bots.ToList();

            bots.Remove("");

            cmb_starter.ItemsSource = new List<string>(){ "Random" }.Concat(bots);
            cmb_starter.SelectedItem = "Random";
        }

        byte[] file;
        Dictionary<string, string> hashes;
        Dictionary<string, Dictionary<string, int>> memory_offsets;
        List<BattleWrapper> allBattles;
        List<EncountersWrapper> allEncounters;
        List<PartWrapper> allParts;
        Randomizer randomizer;
        MemoryPatcher memoryPatcher;
        Dictionary<string, Operation> operations;
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
            await this.ShowMessageAsync(big, error, MessageDialogStyle.Affirmative, mySettings);
        }

        private void addOffsets()
        {
            byte[] shopBytes  = new byte[] { 0x13, 0x00, 0xFF, 0xFF, 0x13, 0x00, 0x42, 0xFF, 0x13, 0x00 };
            byte[] eventBytes = new byte[] { 0x2F, 0x1B, 0x03, 0x11, 0x34, 0x00, 0x86, 0x01, 0x01, 0x0A };
            byte[] encounterBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x9B, 0xF5, 0x9B, 0xA7, 0x9B, 0xF5 };
            byte[] partBytes = new byte[] { 0x0F, 0x22, 0x02, 0x00, 0x23, 0x15, 0x08, 0x01, 0x08, 0x00 };
            byte[] startMedalBytes = new byte[] { 0x01, 0x02, 0x00, 0x56, 0x5D, 0x01, 0x62, 0x17, 0x01 };
            memory_offsets[game_id].Add("ShopContents", Utils.Search(file, shopBytes));
            memory_offsets[game_id].Add("Events", Utils.Search(file, eventBytes));
            memory_offsets[game_id].Add("Encounters", Utils.Search(file, encounterBytes));
            memory_offsets[game_id].Add("Parts", Utils.Search(file, partBytes));
            memory_offsets[game_id].Add("StartMedal", Utils.Search(file, startMedalBytes) - 1);
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
                Trace.WriteLine(file.Length);
                int newsize = 8388608 * 2;
                Array.Resize<byte>(ref file, newsize);
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
                    addOffsets();
                    PopulateData(game_id);
                    memoryPatcher = new MemoryPatcher(file, 0x08900000);
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
                ShowNotification("Error!", "Please select a ROM before applying.");
                return;
            }

            //////////////////////////////////////////////////////
            /// SETUP RANDOMIZER AND SEED
            //////////////////////////////////////////////////////
            string seedtext = (seed_input.Text != "") ? seed_input.Text : Utils.RandomString(12);
            seedtext = "Salty's-Quest";
            MD5 md5Hasher = MD5.Create();
            byte[] hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(seedtext));
            int ivalue = BitConverter.ToInt32(hashed, 0);

            Random rng = new Random(ivalue);
            randomizer = new Randomizer(allBattles, allEncounters, allParts, rng);

            if (chk_enable_randomizer.IsOn)
            {
                //////////////////////////////////////////////////////
                /// RANDOMIZE CHARACTERS
                //////////////////////////////////////////////////////
                if (chk_randomize_characters.IsOn)
                {
                    randomizer.RandomizeCharacters(chk_character_continuity.IsOn);
                }

                //////////////////////////////////////////////////////
                /// RANDOMIZE BATTLES
                //////////////////////////////////////////////////////
                if (chk_randomize_battles.IsOn)
                {
                    float mixedchance = chk_allow_mixed_bots.IsOn ? (float)(sl_mixed_bots.Value / 100) : 0;
                    bool keep_team_structure = chk_keep_battle_structure.IsOn;
                    bool balanced_medal_level = chk_balanced_bot_levels.IsOn;
                    bool keep_battle_continuity = chk_battle_continuity.IsOn;

                    randomizer.RandomizeBattles(keep_team_structure, balanced_medal_level, mixedchance, keep_battle_continuity);
                }

                randomizer.fixSoftlock();
                int amount_of_battles = 0xf5;
                int battle_size = 0x28;

                for (int i = 0; i <= amount_of_battles; i++)
                {
                    int battle_address = Utils.GetAdressAtPosition(file, memory_offsets[game_id]["Battles"] + 4 * i);
                    byte[] battle = StructUtils.getBytes(allBattles[i].content);
                    Array.Copy(battle, 0, file, battle_address, battle_size);
                }

                //////////////////////////////////////////////////////
                /// RANDOM STARTER
                //////////////////////////////////////////////////////
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
                    byte part;

                    if ((string)cmb_starter.SelectedItem == "Random")
                    {
                        part = (byte)rng.Next(0, 0x78);
                        while (blacklist.Contains(part))
                        {
                            part = (byte)rng.Next(0, 0x78);
                        }
                    }
                    else
                    {
                        part = (byte)(cmb_starter.SelectedIndex - 1);
                    }

                    byte medal = IdTranslator.botMedal(part);

                    randomizer.starterMedal = medal;

                    int offset = memory_offsets[game_id]["Starter"];
                    uint funcOffset = 0x044b58;

                    for (int i = 0; i < 4; i++)
                    {
                        file[offset + 4 * i] = part;
                    }

                    if (IdTranslator.isFemale(part))
                    {
                        file[offset + 16] = 1;
                    }

                    file[memory_offsets[game_id]["StartMedal"]] = medal;

                    ushort[] replacedFunction = new ushort[]
                    {
                    //Equip_parts 
                    0x4a0e,                         //ldr        r2,[PTR_DAT_]
                    0x7811,           				//ldrb       r1,[r2,#0x0 ]=>DAT_03000be0
                    0x20c0,                         //mov        r0,#0xc0
                    (ushort)(0x2300 + medal),        //mov        r3,#medal
                    0x4308,                         //orr        r0, r1
                    0x7010,                         //strb       r0,[r2,#0x0 ]=>DAT_03000be0
                    0x490c,                         //ldr        r1,[PTR_DAT_]
                    0x2003,           				//mov        r0,#0x3
                    0x7008,                         //strb       r0,[r1,#0x0 ]=>DAT_030017a0
                    0x708b,                         //strb       r3,[r1,#0x2 ]=>DAT_030017a2
                    (ushort)(0x2000 + part),         //mov        r0,#part
                    0x70c8,                         //strb       r0,[r1,#0x3 ]=>DAT_030017a3
                    0x7108,                         //strb       r0,[r1,#0x4 ]=>DAT_030017a4
                    0x7148,                         //strb       r0,[r1,#0x5 ]=>DAT_030017a5
                    0x7188,                         //strb       r0,[r1,#0x6 ]=>DAT_030017a6
                    0x4909,                         //ldr        r1,[->parts_in_inventory]
                    0x1c08,           				//add        r0, r1,#0x0
                    (ushort)(0x3000 + part * 2 + 1), //add        r0,#part_offset
                    0x2201,                         //mov        r2,#0x1
                    0x7002,           				//strb       r2,[r0,#0x0 ]=>DAT_03004c95
                    0x4b07,                         //ldr        r3,[DAT_]
                    0x18c8,           				//add        r0, r1, r3
                    0x7002,                         //strb       r2,[r0,#0x0 ]=>DAT_03004d85
                    0x33f0,                         //add        r3,#0xf0
                    0x18c8,                         //add        r0, r1, r3
                    0x7002,                         //strb       r2,[r0,#0x0 ]=>DAT_03004e75
                    0x4805,                         //ldr        r0,[DAT_]
                    0x1809,           				//add        r1, r1, r0
                    0x700a,                         //strb       r2,[r1,#0x0 ]=>DAT_03004f65
                    0x4770,                         //bx         lr
                    // Data and pointers
                    0x0be0,
                    0x0300,
                    0x17a0,
                    0x0300,
                    0x4c40,
                    0x0300,
                    (ushort)(part * 2 + 1 + 0xf0),
                    0x0000,
                    (ushort)(part * 2 + 1 + 3 * 0xf0),
                    0x0000
                    };

                    Utils.WritePayload(file, funcOffset, replacedFunction);
                }

                //////////////////////////////////////////////////////
                /// RANDOM MEDALS
                //////////////////////////////////////////////////////
                if (chk_random_medal.IsOn)
                {
                    for (int i = memory_offsets[game_id]["Events"]; i < memory_offsets[game_id]["Events"] + 0x18000;)
                    {
                        byte op = file[i];
                        if (op == 0x3C)
                        {
                            Trace.WriteLine("Get Medal: " + IdTranslator.IdToMedal(file[i + 1]));
                            if (i + 1 == memory_offsets[game_id]["StartMedal"])
                            {
                                Trace.WriteLine("Is random starter, skipping...");
                            }
                            else
                            {
                                var randomMedal = randomizer.GetRandomMedal(file[i + 1]);

                                file[i + 1] = randomMedal;
                                Trace.WriteLine("Set Medal to: " + IdTranslator.IdToMedal(file[i + 1]));
                            }
                        }

                        if (op == 0x2F)
                        {
                            //multiconditional jump
                            i += file[i + 1] + 1;
                        }
                        else
                        {
                            i += IdTranslator.operationBytes[op];
                        }
                    }
                    //////////////////////////////////////////////////////
                    /// FIX MESSAGES
                    //////////////////////////////////////////////////////
                    List<byte> origMedals = new List<byte> { 15, 16, 17, 18, 19, 20 };
                    List<byte> replacedMedals = origMedals.Select(x => randomizer.medalExchanges[x]).ToList();

                    List<Medal> medals = loadFile<List<Medal>>("./Medals.json");
                    List<((int, int), (int, int))> messages = new List<((int, int), (int, int))>();
                    messages.Add(((0x00, 0x6b), (0x00, 0x68)));
                    messages.Add(((0x00, 0x6f), (0x00, 0x6c)));
                    messages.Add(((0x00, 0x73), (0x00, 0x70)));
                    messages.Add(((0x00, 0x77), (0x00, 0x74)));
                    messages.Add(((0x00, 0x7b), (0x00, 0x78)));
                    messages.Add(((0x00, 0x7f), (0x00, 0x7c)));

                    TextParser textParser = new TextParser(file, memory_offsets[game_id]["Text"]);
                    for (int i = 0; i < replacedMedals.Count; i++)
                    {
                        textParser.addMessage(messages[i].Item1, medals[replacedMedals[i]].ikki_text);
                        textParser.addMessage(messages[i].Item2, medals[replacedMedals[i]].collect_text);
                    }
                }
            }
            //////////////////////////////////////////////////////
            /// CODE PATCHES
            //////////////////////////////////////////////////////
            if (chk_code_patches.IsOn)
            {
                uint jumpOffset = 0x104;
                uint hookOffset = 0x7f4500;
                uint trainerOffset = hookOffset + 0xD0;

                uint instr1 = (uint)Utils.GetIntAtPosition(file, (int)jumpOffset);
                uint instr2 = (uint)Utils.GetIntAtPosition(file, (int)jumpOffset + 4);
                uint instr3 = (uint)Utils.GetIntAtPosition(file, (int)jumpOffset + 8);

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

                List<uint> trainerPayloadList = new List<uint>();
                Dictionary<uint, ushort> codePatches = new Dictionary<uint, ushort>();

                if (chk_instant_text.IsOn)
                {
                    trainerPayloadList.AddRange(new uint[]{
                        // Set text_speed to instant
                        0xE3A01403,                         // mov r1, #0x3000000
                        0xE3A000FF,                         // mov r0, #0xFF
                        0xE5C1045A,                         // strb r0, [r1, #0x45A]
                    });
                    codePatches = codePatches.Union(new Dictionary<uint, ushort>
                    {
                        // Instant Character Popup
                        { 0x3F5F6, 0x3008 },
                        { 0x3F600, 0xDC08 }
                    }).ToDictionary(k => k.Key, v => v.Value);
                }

                if (chk_encounters.IsOn)
                {
                    trainerPayloadList.AddRange(new uint[]{
                        // Allow encounters
                        0xE3A01403,                         // mov r1, #0x3000000
                        0xE2811B19,                         // add r1, #0x6400
                        0xE3A00000,                         // mov r0, #0x0
                        0xE5C1000C,                         // strb r0, [r1, #0xc]
                    });
                }

                trainerPayloadList.Add(
                    // Return
                    0xE12FFF1E                          // bx r15
                );

                uint[] trainerPayload = trainerPayloadList.ToArray();

                Utils.WritePayload(file, jumpOffset, jumpPayload);
                Utils.WritePayload(file, hookOffset, hookPayload);
                Utils.WritePayload(file, trainerOffset, trainerPayload);
                Utils.WritePatches(file, codePatches);
            }

            //////////////////////////////////////////////////////
            /// ADD MESSAGES
            //////////////////////////////////////////////////////
            TextParser textParser2 = new TextParser(file, memory_offsets[game_id]["Text"]);
            List<Message> patchedMessages = loadFile<List<Message>>("./Patched_Messages.json");
            foreach (Message message in patchedMessages)
            {
                textParser2.addMessage((message.id[0], message.id[1]), message.message);
            }

            TextPatcher textPatcher = new TextPatcher(ref file, memory_offsets[game_id]["Text"], 0x7f5500, textParser2.getEncodedMessages());
            textPatcher.PatchText();

            //////////////////////////////////////////////////////
            /// Gender-Neutral Bots
            //////////////////////////////////////////////////////

            byte[] palette = new byte[] { 0x00, 0x00, 0xbc, 0xff, 0xf7, 0xee, 0x51, 0x56, 0x28, 
                                          0x2D, 0x7f, 0x8f, 0x7e, 0x92, 0x78, 0x95, 0xae, 0xfe,
                                          0xa9, 0x75, 0x68, 0x65, 0xc4, 0x4c, 0x83, 0x1c, 0xff, 
                                          0xff, 0xff, 0xff, 0xff, 0xff };

            byte[] newPalette = new byte[] {  0x00, 0x00, 0xbc, 0xff, 0xf7, 0xee, 0x51, 0x56, 0x28,
                                              0x2D, 0x7f, 0x8f, 0x7e, 0x92, 0x78, 0x95, 0xff, 0x7f,
                                              0x39, 0x67, 0x94, 0x52, 0x10, 0x42, 0x83, 0x1c, 0xff,
                                              0xff, 0xff, 0xff, 0xff, 0xff };

            foreach (uint loc in Utils.SearchAll(file, palette))
            {
                Utils.WritePayload(file, loc, newPalette);
            }

            foreach (PartWrapper part in allParts)
            {
                part.content.gender = 0;
            }

            for (int i = 0; i < allParts.Count; i++)
            {
                byte[] battle = StructUtils.getBytes(allParts[i].content);
                int part_address = memory_offsets[game_id]["Parts"] + battle.Length * i;
                Array.Copy(battle, 0, file, part_address, battle.Length);
            }

            for (int i = memory_offsets[game_id]["Events"]; i < memory_offsets[game_id]["Events"] + 0x18000;)
            {
                byte op = file[i];
                if (op == 0x3D)
                {
                    file[i + 1] = 0;
                }
                if (op == 0x2F)
                {
                    //multiconditional jump
                    i += file[i + 1] + 1;
                }
                else
                {
                    i += IdTranslator.operationBytes[op];
                }
            }

            //////////////////////////////////////////////////////
            /// PATCH SHOPS
            //////////////////////////////////////////////////////

            int shop_address = memory_offsets[game_id]["ShopContents"];

            List<ShopData> shops = loadFile<List<ShopData>>("shops.json");

            foreach (ShopData shop in shops)
            {
                int shop_id = shop.id;
                int[] json_shop_data = shop.shopContents;
                int shop_size = json_shop_data.Length;
                byte[] newShop = new byte[shop_size];
                for (int i = 0; i < shop_size; i++)
                {
                    newShop[i] = (byte)json_shop_data[i];
                }
                uint specific_shop_address = (uint)(shop_address + shop_size * shop_id);
                Utils.WritePayload(file, specific_shop_address, newShop);
            }


            //////////////////////////////////////////////////////
            /// DOG MODE
            //////////////////////////////////////////////////////

            byte[] salty = new byte[] { 0x13, 0x1b, 0x26, 0x2e, 0x33, 0xfe};

            uint ptr = 0x483714;
            Utils.WritePayload(file, ptr, salty);
            //Utils.WriteInt(file, 0x3f7ea0, 0x085D81c4);

            // Change sprite to salty.
            for (int i = 0; i <= 191; i++)
            {
                int address = Utils.GetAdressAtPosition(file, 0x413180 + 4 * i);
                int j = 0;
                while (true)
                {
                    if (file[address + j] != 0xff)
                    {
                        if (file[address + j] == 0x00)
                        {
                            file[address + j] = 0x05;
                        }
                        else if (file[address + j] == 0x05)
                        {
                            file[address + j] = 0x00;
                        }
                        j++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // New Sprite
            List<ImageData> patchedPortraits = new List<ImageData>();
            foreach (string file in Directory.GetFiles(".\\portraits\\", "*.bmp"))
            {
                patchedPortraits.Add(ImageLoader.LoadImage(Path.GetFileName(file).Split('.')[0], "portraits"));
            }
            for (int i = 0; i < patchedPortraits.Count; i++)
            {
                byte[] compressedPortrait = DataCompression.CompressPortrait(patchedPortraits[i].data);
                memoryPatcher.PatchMemoryAndStoreAddress(compressedPortrait, (uint)(0x3afea8 + (patchedPortraits[i].metadata[0] * 9 + patchedPortraits[i].metadata[1]) * 4));
                memoryPatcher.PatchMemoryAndStoreAddress(patchedPortraits[i].palette, (uint)(0x3b1768 + patchedPortraits[i].metadata[0] * 4));
            }

            // Overworld Sprites
            // 083f7ea0 sprites
            // 083f83e0 palettes
            List<ImageData> patchedSprites = new List<ImageData>();
            foreach (string file in Directory.GetFiles(".\\sprites\\", "*.bmp"))
            {
                patchedSprites.Add(ImageLoader.LoadImage(Path.GetFileName(file).Split('.')[0], "sprites"));
            }
            for (int i = 0; i < patchedSprites.Count; i++)
            {
                byte[] compressedSprite = DataCompression.CompressSprite(patchedSprites[i].data);
                memoryPatcher.PatchMemoryAndStoreAddress(compressedSprite, (uint)(0x3f7ea0 + patchedSprites[i].metadata[0] * 4));
                memoryPatcher.PatchMemoryAndStoreAddress(patchedSprites[i].palette, (uint)(0x3f83e0 + patchedSprites[i].metadata[0] * 4));
            }

            operations = loadFile<Dictionary<string, Operation>>("operations.json");
            uint event_table_offset = 0x00769EE4;
            List<Event> allEvents = new List<Event>();
            for (short event_id = 0; event_id < 0x8F0; event_id++)
            {
                //if (event_id == 0x20) continue;
                uint bank_offset = (uint)file[event_table_offset + 0x11E0 + event_id] * 0x4000;
                uint address_in_bank = ((uint)file[event_table_offset + event_id * 2 + 1] << 8) + (uint)file[event_table_offset + event_id * 2] - 0x4000;
                uint event_offset = (event_table_offset - 0x4000) + address_in_bank + bank_offset;
                uint i = event_offset;
                allEvents.Add(Event.fromData(file, event_offset, operations));
            }
            Trace.WriteLine("OH SHIT WADUP");

            //////////////////////////////////////////////////////
            /// WRITE TO FILE
            //////////////////////////////////////////////////////
            File.WriteAllBytes(seedtext + ".gba", file);
            ShowNotification("Done!", "The ROM has been converted and is saved with seed: \"" + seedtext + "\" as \"" + seedtext + ".gba\"");
        }

        class Event
        {
            public Dictionary<uint, string> event_operations;

            private Event(Dictionary<uint, string> event_operations)
            {
                this.event_operations = event_operations;
            }

            public static Event fromData(byte[] file, uint offset, Dictionary<string, Operation> operations)
            {
                Dictionary<uint, string> operationMap = new Dictionary<uint, string>();
                buildEvent(file, offset, operationMap, operations);
                return new Event(operationMap);
            }

            public static void buildEvent(byte[] file, uint offset, Dictionary<uint, string> operationMap, Dictionary<string, Operation> operations)
            {
                while (true)
                {
                    byte op = file[offset];
                    if (op == 0x2F)
                    {
                        //multiconditional jump
                        List<int> jumps = new List<int>();
                        jumps.Add(file[offset + 1]);
                        jumps.Add(file[offset + 2]);
                        int nArgs = 0;
                        for (int i = 2; i < 8; i++)
                        {
                            int minJump = jumps.Min();
                            if (minJump == jumps.Count)
                            {
                                nArgs = minJump;
                                break;
                            }
                            else
                            {
                                jumps.Add(file[offset + 1 + i]);
                            }
                        }
                        int[] args = new int[nArgs];
                        for (int i = 0; i < nArgs; i++)
                        {
                            args[i] = file[offset + 1 + i];
                        }
                        string argString = String.Join(",", args);
                        operationMap[offset] = $"<Conditional_Multijump: {argString}>";
                        foreach (uint jump in args)
                        {
                            buildEvent(file, offset + jump + 1, operationMap, operations);
                        }
                        break;
                    }
                    else if (op == 0x06)
                    {
                        operationMap[offset] = "<END>";
                        break;
                    }
                    else if (op == 0x19)
                    {
                        ushort goto_event = (ushort)((ushort)(file[offset + 1] << 8) + (ushort)file[offset + 2]);
                        operationMap[offset] = $"<GOTO_EVENT: {goto_event}>";
                        break;
                    }
                    else
                    {
                        if (!operations.TryGetValue(op.ToString("X2"), out Operation operation))
                        {
                            uint opLength = IdTranslator.operationBytes[op];
                            IdTranslator.opNames.TryGetValue(op, out string opName);
                            if (opName == "" || opName == null) opName = $"UNKNOWN({op.ToString("X2")})";
                            if (opLength > 1)
                            {
                                int[] args = new int[opLength - 1];
                                for (int i = 0; i < opLength - 1; i++)
                                {
                                    args[i] = file[offset + i + 1];
                                }
                                string argString = string.Join(",", args);
                                operationMap[offset] = $"<{opName}: {argString}>";
                            }
                            else
                            {
                                operationMap[offset] = $"<{opName}>";
                            }
                            offset += opLength;
                        }
                        else
                        {
                            uint opLength = (uint)operation.getArgLength() + 1;
                            operationMap[offset] = operation.getString(file, offset);
                            /*
                            if (operation.args.Any(x => x.name == "jump"))
                            {
                                buildEvent(file, offset + jump + 1, operationMap, operations);
                            }
                            */
                            offset += opLength;
                        }
                    }
                }
            }
        }

        struct OperationArgument
        {
            public string name;
            public string type;
            public object value;

            public int getByteCount()
            {
                switch (type)
                {
                    case "short":
                        return 2;
                    default:
                        return 1;
                }
            }

            public object readValue(byte[] file, uint offset)
            {
                switch (type)
                {
                    case "short":
                        value = (file[offset] << 8) + file[offset + 1];
                        return value;
                    case "medal":
                        value = file[offset];
                        return IdTranslator.IdToMedal(file[offset]);
                    case "direction":
                        value = file[offset];
                        switch (file[offset])
                        {
                            case 0:
                                return "north";
                            case 1:
                                return "south";
                            case 2:
                                return "west";
                            case 3:
                                return "east";
                            default:
                                return file[offset];
                        }
                    case "part":
                        value = file[offset];
                        switch (file[offset])
                        {
                            case 0:
                                return "head";
                            case 1:
                                return "right";
                            case 2:
                                return "left";
                            case 3:
                                return "legs";
                            default:
                                return "?";
                        }
                    case "bot":
                        value = file[offset];
                        return IdTranslator.IdToBot((byte)value);
                    case "move":
                        if (file[offset] == 0xFF) return "-";
                        string direction = "";
                        switch (file[offset] & 0xF0)
                        {
                            case 0x00:
                                direction = "north";
                                break;
                            case 0x10:
                                direction = "south";
                                break;
                            case 0x20:
                                direction = "west";
                                break;
                            case 0x30:
                                direction = "east";
                                break;
                            default:
                                direction = "?";
                                break;
                        }
                        value = file[offset];
                        string length = (file[offset] & 0x0F).ToString();
                        return $"({direction}, {length})";
                    case "event_bank":
                        value = file[offset] - 2;
                        break;
                    default:
                        value = file[offset];
                        break;
                }
                return value;
            }

            public Tuple<string, int, object> parseData(byte[] file, uint offset)
            {
                object value = readValue(file, offset);
                int bytes_read = getByteCount();
                return new Tuple<string, int, object>($"{name}: {value}", bytes_read, value);
            }
        }

        struct Operation
        {
            public string name;
            public List<OperationArgument> args;

            public bool hasJump()
            {
                return args.Any(x => x.name == "jump");
            }

            public int getJump(List<object> values)
            {
                int index = args.FindIndex(x => x.name == "jump");
                return Convert.ToInt32(values[index]);
            }

            public int getArgLength()
            {
                int result = 0;
                foreach (OperationArgument arg in args)
                {
                    result += arg.getByteCount();
                }
                return result;
            }

            public string getString(byte[] file, uint offset)
            {
                Tuple<string, List<object>> args = getArgString(file, offset + 1);
                string result = $"{this.name}({args.Item1})";
                string comment = "";
                if (name == "Show_Message_A" || name == "Show_Message_B")
                    comment = TextParser.instance.origMessages[((int)args.Item2[0], (int)args.Item2[1])];
                if (name == "Warp_A" || name == "Warp_B")
                    comment = IdTranslator.IdToMap((byte)args.Item2[0]);
                if (name == "Play_Music" || name == "Play_Persistent_Music")
                    if ((byte)args.Item2[0] <= 51)
                        comment = IdTranslator.song_names[(byte)args.Item2[0] - 1];
                if (this.hasJump())
                    comment = (offset + getJump(args.Item2) + 1).ToString("X2");
                if (comment != "")
                    return $"{result} #{comment}";
                return $"{result}";
            }

            public Tuple<string, List<object>> getArgString(byte[] file, uint offset)
            {
                List<string> result = new List<string>();
                List<object> argValues = new List<object>();
                foreach (OperationArgument arg in args)
                {
                    Tuple<string, int, object> res = arg.parseData(file, offset);
                    offset += (uint)res.Item2;
                    result.Add(res.Item1);
                    argValues.Add(res.Item3);
                }
                return new Tuple<string, List<object>>(string.Join(", ", result), argValues);
            }
        }

        struct Message
        {
            public int[] id;
            public string message;
        }

        struct Medal
        {
            public string name;
            public string ikki_text;
            public string collect_text;
        }

        struct ShopData
        {
            public int id;
            public int[] shopContents;
        }

        private T loadFile<T>(string fileName)
        {
            using (StreamReader file = File.OpenText(fileName))
                return JsonConvert.DeserializeObject<T>(file.ReadToEnd());
        }
    }
}
