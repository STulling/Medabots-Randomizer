using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace MedabotsRandomizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
                    { "Equipped", 0x044b6c},
                    { "StartMedal", 0x78549c}
                }},
                { "MEDABOTSRKSVA9BEE9", new Dictionary<string, int>{
                    { "Battles", 0x3c1a00 },
                    { "Encounters", 0x3bf090 },
                    { "Parts", 0x3b827c }
                }},
                { "MEDABOTSMTBVA8BEE9", new Dictionary<string, int>{
                    { "Battles", 0x3c19e0 },
                    { "Encounters", 0x3bf070 },
                    { "Parts", 0x3b825c }
                }},
                { "MEDABOTSMTBVA8BPE9", new Dictionary<string, int>{
                    { "Battles", 0x3c1b80 },
                    { "Encounters", 0x3bf210 },
                    { "Parts", 0x3b83fc }
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
            battleList.ItemsSource = allBattles;
            allEncounters = DataPopulator.Populate_Data<EncountersWrapper>(file, 0xbf, 4, memory_offsets[id_string]["Encounters"], false);
            encounterList.ItemsSource = allEncounters;
            allParts = DataPopulator.Populate_Data<PartWrapper>(file, 480, 0x10, memory_offsets[id_string]["Parts"], false);
            partData.ItemsSource = allParts;
            randomizer = new Randomizer(allBattles, allEncounters, allParts);
        }

        private void Load_ROM_event(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                allBattles.Clear();
                allEncounters.Clear();
                allParts.Clear();
                string chosenFile = openFileDialog.FileName;
                file = File.ReadAllBytes(chosenFile);
                //file = new byte[tmp_file.Length * 2];
                //tmp_file.CopyTo(file, 0);
                byte[] id_bytes = new byte[0x12];
                Array.Copy(file, 0xa0, id_bytes, 0, 0x12);
                string id_string = Encoding.Default.GetString(id_bytes);
                game_id = id_string;
                Trace.WriteLine(id_string);
                if (hashes.TryGetValue(id_string, out string recognizedFile))
                {
                    romLabel.Foreground = Brushes.Green;
                    Trace.WriteLine(recognizedFile);
                    romLabel.Content = recognizedFile;
                    PopulateData(id_string);
                }
                else
                {
                    romLabel.Content = "Unknown ROM";
                    romLabel.Foreground = Brushes.Red;
                }
            }
        }

        private void encounterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            EncountersWrapper encounters = (EncountersWrapper)dataGrid.SelectedItem;
            encounters_1.Text = encounters.content.battle[0].ToString("X2");
            encounters_2.Text = encounters.content.battle[1].ToString("X2");
            encounters_3.Text = encounters.content.battle[2].ToString("X2");
            encounters_4.Text = encounters.content.battle[3].ToString("X2");
        }

        private void save_encounters(object sender, RoutedEventArgs e)
        {
            EncountersWrapper encounters = (EncountersWrapper)encounterList.SelectedItem;
            byte enc1;
            byte enc2;
            byte enc3;
            byte enc4;
            try
            {
                enc1 = Convert.ToByte(encounters_1.Text, 16);
                enc2 = Convert.ToByte(encounters_2.Text, 16);
                enc3 = Convert.ToByte(encounters_3.Text, 16);
                enc4 = Convert.ToByte(encounters_4.Text, 16);
            }
            catch
            {
                MessageBox.Show("Invalid encounters");
                return;
            }
            encounters.content.battle[0] = enc1;
            encounters.content.battle[1] = enc2;
            encounters.content.battle[2] = enc3;
            encounters.content.battle[3] = enc4;
            encounterList.Items.Refresh();
        }

        private void WriteInt(byte[] bytes, uint offset, uint opcode)
        {
            byte[] bytez = BitConverter.GetBytes(opcode);
            bytes[offset] = bytez[0];
            bytes[offset + 1] = bytez[1];
            bytes[offset + 2] = bytez[2];
            bytes[offset + 3] = bytez[3];
        }

        private void WritePayload(byte[] bytes, uint offset, uint[] payload)
        {
            for (uint i = 0; i < payload.Length; i++)
            {
                WriteInt(bytes, offset + 4 * i, payload[i]);
            }
        }

        private void WriteShort(byte[] bytes, uint offset, ushort opcode)
        {
            byte[] bytez = BitConverter.GetBytes(opcode);
            bytes[offset] = bytez[0];
            bytes[offset + 1] = bytez[1];
        }

        private void WritePatches(byte[] bytes, Dictionary<uint, ushort> codePatches)
        {
            foreach (KeyValuePair<uint, ushort> entry in codePatches)
            {
                WriteShort(bytes, entry.Key, entry.Value);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Utils.Shuffle<BattleWrapper>(allBattles);
            if (randomizebattles.IsChecked.Value)
            {
                float mixedchance = 0;
                if (mixedbots.IsChecked.Value)
                    mixedchance = (float)(mixedslider.Value / 100);
                bool keep_team_structure = keepstructure.IsChecked.Value;
                bool balanced_medal_level = balancedlevels.IsChecked.Value;
                bool keep_battle_continuity = battlecontinuity.IsChecked.Value;
                randomizer.RandomizeBattles(keep_team_structure, balanced_medal_level, mixedchance, keep_battle_continuity);
            }
            if (randomizecharacters.IsChecked.Value)
            {
                bool continuity = continuitycheck.IsChecked.Value;
                randomizer.RandomizeCharacters(continuity);
            }
            int amount_of_battles = 0xf5;
            int battle_size = 0x28;
            for (int i = 0; i <= amount_of_battles; i++)
            {
                int battle_address = Utils.GetAdressAtPosition(file, memory_offsets[game_id]["Battles"] + 4 * i);
                byte[] battle = StructUtils.getBytes(allBattles[i].content);
                //byte[] battle = randomizer.GenerateRandomBattle(false).getBytes();
                Array.Copy(battle, 0, file, battle_address, battle_size);
            }
            if (shouldPatch.IsChecked.Value)
            {
                uint jumpOffset = 0x104;
                uint hookOffset = 0x7f3530;
                uint trainerOffset = 0x7f3600;
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
                WritePayload(file, jumpOffset, jumpPayload);
                WritePayload(file, hookOffset, hookPayload);
                WritePayload(file, trainerOffset, trainerPayload);
                WritePatches(file, codePatches);
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

            if (randomizeStarter.IsChecked.Value)
            {
                byte randomBot = (byte) new Random().Next(0, 0x78);
                while (blacklist.Contains(randomBot))
                {
                    randomBot = (byte)new Random().Next(0, 0x78);
                }
                int offset = memory_offsets[game_id]["Starter"];
                for (int i = 0; i < 4; i++)
                {
                    file[offset + 4 * i] = randomBot;
                }
                if (IdTranslator.isFemale(randomBot))
                {
                    file[offset + 16] = 1;
                }
                file[memory_offsets[game_id]["Equipped"]] = randomBot;
                file[memory_offsets[game_id]["Equipped"] + 0xE] = (byte)(randomBot * 2 + 1);
            }

            File.WriteAllBytes("randomized.gba", file);
        }

        private void battleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            BattleWrapper battle = (BattleWrapper)dataGrid.SelectedItem;
            characterLabel.Content = battle.Character;
            charId.Text = battle.content.characterId.ToString("X2");
            numBots.Text = battle.content.number_of_bots.ToString("X2");
            bot_1_grid.ItemsSource = battle.content.bots[0].toDataSource();
            bot_2_grid.ItemsSource = battle.content.bots[1].toDataSource();
            bot_3_grid.ItemsSource = battle.content.bots[2].toDataSource();
        }

        private void partData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            PartWrapper part = (PartWrapper)dataGrid.SelectedItem;
            partDataView.ItemsSource = part.content.toDataSource(part.Type);
        }

        private void ContinuityCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            continuitycheck.IsEnabled = (bool)((CheckBox)sender).IsChecked;
        }

        private void MixedCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            mixedslider.IsEnabled = (bool)((CheckBox)sender).IsChecked;
        }

        private void mixedslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mixedpercentage.Content = ((int)((Slider)sender).Value).ToString() + "%";
        }

        private void RandomizeBattlesCheckBox_checked(object sender, RoutedEventArgs e)
        {
            bool ischecked = (bool)((CheckBox)sender).IsChecked;
            if (!ischecked)
            {
                mixedslider.IsEnabled = false;
                battlecontinuity.IsEnabled = false;
            }
            keepstructure.IsEnabled = ischecked;
            balancedlevels.IsEnabled = ischecked;
            mixedbots.IsEnabled = ischecked;
        }

        private void keepstructure_Checked(object sender, RoutedEventArgs e)
        {
            battlecontinuity.IsEnabled = (bool)((CheckBox)sender).IsChecked;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            List<ImageWrapper> images = new List<ImageWrapper>();
            int i = 0;
            for (int offset = 0; offset < (file.Length - 1); offset++)
            {
                if (file[offset] == 'L' && file[offset + 1] == 'e')
                {
                    int size = Utils.GetIntAtPosition(file, offset + 2);
                    if (size > 0 && size < 0x8000)
                    {
                        //ArrayPtr arrayPtr = new ArrayPtr(file, offset);
                        byte[] data = Malias2.Decompress(file, offset);
                        ImageWrapper result = new ImageWrapper(i, offset, data, "Malias2");
                        images.Add(result);
                        i++;
                    }
                }
                else if (file[offset] == 0x10)
                {
                    int size = file[offset + 1] | file[offset + 2] << 8 | file[offset + 3] << 16;
                    if (size > 0 && size < 0x8000 && size % 0x20 == 0)
                    {
                        byte[] data = LZ77.Decompress(file, offset);
                        if (data != null)
                        {
                            ImageWrapper result = new ImageWrapper(i, offset, data, "LZ77");
                            images.Add(result);
                            i++;
                        }
                    }
                }
            }
            imagesList.ItemsSource = images;
        }

        private byte[] flip(byte[] tile)
        {
            byte[] result = new byte[tile.Length];
            for (int i = 0; i < tile.Length; i++)
            {
                result[i] = (byte)((byte)(tile[i] << 4) + (byte)(tile[i] >> 4));
            }
            return result;
        }

        private void showImage(ImageWrapper imageWrapper)
        {
            canvas.Children.Clear();
            int width = 256;
            int offset = 0;
            if (Int32.TryParse(widthImage.Text, out int result))
            {
                if (result > 0)
                {
                    width = result * 8;
                }
            }
            if (Int32.TryParse(offsetImage.Text, out result))
            {
                if (result > 0)
                {
                    offset = result;
                }
            }
            if (bit_mode.IsChecked.Value)
            {
                if (imageWrapper.data.Length % 0x40 == 0)
                {
                    List<byte[]> tiles = imageWrapper.getTiles(0x40);
                    tiles.RemoveRange(0, offset);
                    int height = (int)Math.Ceiling((double)tiles.Count / (width / 8)) * 8;
                    WriteableBitmap wbm = new WriteableBitmap(width, height, 48, 48, PixelFormats.Gray8, null);
                    for (int y = 0; y < height / 8; y++)
                    {
                        for (int x = 0; x < width / 8; x++)
                        {
                            if (x + y * (width / 8) >= tiles.Count) break;
                            wbm.WritePixels(new Int32Rect(x * 8, y * 8, 8, 8), tiles[x + y * (width / 8)], 8, 0);
                        }
                    }
                    Image tile = new Image();
                    tile.Source = wbm;
                    Canvas.SetTop(tile, 0);
                    Canvas.SetLeft(tile, 0);
                    canvas.Children.Add(tile);
                }
            }
            else
            {
                if (imageWrapper.data.Length % 0x20 == 0)
                {
                    List<byte[]> tiles = imageWrapper.getTiles(0x20);
                    tiles.RemoveRange(0, offset);
                    int height = (int)Math.Ceiling((double)tiles.Count / (width / 8)) * 8;
                    WriteableBitmap wbm = new WriteableBitmap(width, height, 48, 48, PixelFormats.Gray4, null);
                    for (int y = 0; y < height / 8; y++)
                    {
                        for (int x = 0; x < width / 8; x++)
                        {
                            if (x + y * (width / 8) >= tiles.Count) break;
                            wbm.WritePixels(new Int32Rect(x * 8, y * 8, 8, 8), flip(tiles[x + y * (width / 8)]), 4, 0);
                        }
                    }
                    Image tile = new Image();
                    tile.Source = wbm;
                    Canvas.SetTop(tile, 0);
                    Canvas.SetLeft(tile, 0);
                    canvas.Children.Add(tile);
                }
            }
        }

        private void imagesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            ImageWrapper imageWrapper = (ImageWrapper)dataGrid.SelectedItem;
            showImage(imageWrapper);
        }

        private void widthImage_TextChanged(object sender, TextChangedEventArgs e)
        {
            ImageWrapper imageWrapper = (ImageWrapper)imagesList.SelectedItem;
            showImage(imageWrapper);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ImageWrapper imageWrapper = (ImageWrapper)imagesList.SelectedItem;
            showImage(imageWrapper);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int textPtrPtrOffset = 0x47df44;
            //int textPtrPtrOffset = 0x44f3d0;
            HashSet<(int, int, int)> textAdresses = new HashSet<(int, int, int)>();
            List<TextWrapper> texts = new List<TextWrapper>();
            int amount_of_ptrs = 15;
            for (int i = 0; i <= amount_of_ptrs; i++)
            {
                int textPtrOffset = Utils.GetAdressAtPosition(file, textPtrPtrOffset + 4 * i);
                int j = 0;
                while(true)
                {
                    int textOffset = Utils.GetAdressAtPosition(file, textPtrOffset + 4 * j);
                    if (textOffset == -0x08000000) break;
                    textAdresses.Add((textOffset, i, j));
                    j++;
                }
            }
            /*
            int b = 0;
            while (true)
            {
                int textOffset = Utils.GetAdressAtPosition(file, 0x411b00 + 4 * b);
                b++;
                if (textOffset > 0x500000 || textOffset < 0) break;
                textAdresses.Add(textOffset);
            }
            int x = 0;
            */
            foreach ((int, int, int) textData in textAdresses)
            {
                int textAddress = textData.Item1;
                List<byte> data = new List<byte>();
                int i = 0;
                while (true)
                {
                    byte currByte = file[textAddress + i];
                    if(currByte == 0xFF || currByte == 0xFE)
                    {
                        data.Add(currByte);
                        i++;
                        data.Add(file[textAddress + i]);
                        break;
                    }
                    else
                    {
                        data.Add(currByte);
                        i++;
                    }
                }
                texts.Add(new TextWrapper(textData.Item2, textData.Item3, textAddress, data.ToArray()));
            }
            textList.ItemsSource = texts;
            List<Message> messages = new List<Message>();
            foreach (TextWrapper text in texts)
            {
                messages.Add(new Message(new int[] { int.Parse(text.Id1), int.Parse(text.Id2) }, decode(text.data)));
            }
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.WriteIndented = true;
            jsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            string jsonString = JsonSerializer.Serialize(messages, options: jsonSerializerOptions);
            File.WriteAllText("./messages.json", jsonString);
        }

        struct Message
        {
            public Message(int[] id, string message)
            {
                this.id = id;
                this.message = message;
            }

            public int[] id { get; }
            public string message { get; }
        }

        private char[] encoding = new char[]
        {
            ' ', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
            'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
            'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W',
            'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e',
            'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
            'v', 'w', 'x', 'y', 'z', '0', '1', '2',
            '3', '4', '5', '6', '7', '8', '9', '·',
            '.', ',', '\'', '-', '/', ':', '?', '!',
            '"', '(', ')', '♥', '£', '&', '%'

        };

        private string decode(byte[] data)
        {
            string result = "";
            for (int i = 0; i < data.Length; i++)
            {
                byte chr = data[i];
                if (chr < 0x4f)
                {
                    result += encoding[data[i]];
                }
                else if (chr == 0xf7)
                {
                    result += "<SPEED:" + data[i + 1] + ">";
                    i++;
                }
                else if (chr == 0xf8)
                {
                    result += "<I>";
                }
                else if (chr == 0xf9)
                {
                    result += "<MEM:" + data[i + 1] + ">";
                    i++;
                }
                else if (chr == 0xfa)
                {
                    result += "<?>";
                }
                else if (chr == 0xfb)
                {
                    result += "<PORTRAIT:" + data[i + 1] + ", " + data[i + 2] + ", " + data[i+3] + ">";
                    i += 3;
                }
                else if (chr == 0xfc)
                {
                    result += "<NB>";
                }
                else if (chr == 0xfD)
                {
                    result += "<NL>";
                }
                else if (chr == 0xfe)
                {
                    result += "<ENDLST>";
                }
                else if (chr == 0xff)
                {
                    result += "<END:"+ data[i + 1] + ">";
                }
            }
            return result;
        }

        private void textList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            TextWrapper textWrapper = (TextWrapper)dataGrid.SelectedItem;
            string text = decode(textWrapper.data);
            textBlock.Text = text;
        }
    }
}