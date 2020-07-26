using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;

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
                    { "Parts", 0x3b841c }
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
            randomizer = new Randomizer(allBattles, allEncounters, allParts);
        }

        byte[] file;
        Dictionary<string, string> hashes;
        Dictionary<string, Dictionary<string, int>> memory_offsets;
        List<BattleWrapper> allBattles;
        List<EncountersWrapper> allEncounters;
        List<PartWrapper> allParts;
        Randomizer randomizer;
        int battle_offset;
        int encounters_offset;
        int parts_offset;

        private void PopulateData(string id_string)
        {
            DataPopulator.Populate_Battles(file, memory_offsets[id_string]["Battles"], allBattles);
            battleList.ItemsSource = allBattles;
            battle_offset = memory_offsets[id_string]["Battles"];

            DataPopulator.Populate_Encounters(file, memory_offsets[id_string]["Encounters"], allEncounters);
            encounterList.ItemsSource = allEncounters;
            encounters_offset = memory_offsets[id_string]["Encounters"];

            DataPopulator.Populate_Parts(file, memory_offsets[id_string]["Parts"], allParts);
            partData.ItemsSource = allParts;
            parts_offset = memory_offsets[id_string]["Parts"];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
            encounters_1.Text = encounters.encounters.battle[0].ToString("X2");
            encounters_2.Text = encounters.encounters.battle[1].ToString("X2");
            encounters_3.Text = encounters.encounters.battle[2].ToString("X2");
            encounters_4.Text = encounters.encounters.battle[3].ToString("X2");
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
            encounters.encounters.battle[0] = enc1;
            encounters.encounters.battle[1] = enc2;
            encounters.encounters.battle[2] = enc3;
            encounters.encounters.battle[3] = enc4;
            encounterList.Items.Refresh();
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
                int battle_address = Utils.GetAdressAtPosition(file, battle_offset + 4 * i);
                byte[] battle = allBattles[i].battle.getBytes();
                //byte[] battle = randomizer.GenerateRandomBattle(false).getBytes();
                Array.Copy(battle, 0, file, battle_address, battle_size);
            }
            File.WriteAllBytes("randomized.gba", file);
        }

        private void battleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            BattleWrapper battle = (BattleWrapper)dataGrid.SelectedItem;
            characterLabel.Content = battle.Character;
            charId.Text = battle.battle.characterId.ToString("X2");
            numBots.Text = battle.battle.number_of_bots.ToString("X2");
            bot_1_grid.ItemsSource = battle.battle.bots[0].toDataSource();
            bot_2_grid.ItemsSource = battle.battle.bots[1].toDataSource();
            bot_3_grid.ItemsSource = battle.battle.bots[2].toDataSource();
        }

        private void partData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            PartWrapper part = (PartWrapper)dataGrid.SelectedItem;
            partDataView.ItemsSource = part.part.toDataSource(part.Type);
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
            List<int> offsets = Utils.findAllOffsets(file);
            //List<int> offsets = Utils.findAllLZ77(file);
            foreach (int offset in offsets)
            {
                int size = Utils.GetIntAtPosition(file, offset+2);
                if (size > 0 && size < 0x10000)
                {
                    byte[] slice = new byte[size*4];
                    Array.Copy(file, offset, slice, 0, size);
                    ImageWrapper result = CompressionUtils.Decompress(slice, i, offset);
                    images.Add(result);
                    i++;
                }
            }
            imagesList.ItemsSource = images;
        }
        private void AddPixel(double x, double y)
        {
            Rectangle rec = new Rectangle();
            Canvas.SetTop(rec, y);
            Canvas.SetLeft(rec, x);
            rec.Width = 1;
            rec.Height = 1;
            rec.Fill = new SolidColorBrush(Colors.Red);
            canvas.Children.Add(rec);
        }

        private void showImage(ImageWrapper imageWrapper, int width = 256)
        {
            canvas.Children.Clear();
            if (imageWrapper.data.Length % 0x20 == 0)
            {
                List<byte[]> tiles = imageWrapper.getTiles();
                int height = (65536 / width);
                WriteableBitmap wbm = new WriteableBitmap(width, height, 48, 48, PixelFormats.Gray4, null);
                for (int y = 0; y < height/8; y++)
                {
                    for (int x = 0; x < width/8; x++)
                    {
                        if (x + y * (width/8) >= tiles.Count) break;
                        wbm.WritePixels(new Int32Rect(x * 8, y * 8, 8, 8), tiles[x + y * (width / 8)], 4, 0);
                    }
                }
                Image tile = new Image();
                tile.Source = wbm;
                Canvas.SetTop(tile, 0);
                Canvas.SetLeft(tile, 0);
                canvas.Children.Add(tile);
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
            if (Int32.TryParse(widthImage.Text, out int result))
            {
                if (result > 0)
                {
                    showImage(imageWrapper, result * 8);
                }
            }
        }
    }
}