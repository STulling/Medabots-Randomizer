using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MedabotsRandomizer.Exceptions;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace MetroApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
    {
        private MedabotsRandomizer.Randomizer randomizer = new MedabotsRandomizer.Randomizer();

        public MainWindow()
        {
            InitializeComponent();
            this.randomizer.PopulateLists();

            cmb_starter.ItemsSource = new List<string>(){ "Random Bot" }.Concat(this.randomizer.bots.ToList());
            cmb_starter.SelectedItem = "Random Bot";

			cmb_starter_medal.ItemsSource = new List<string>() { "Random Medal" }.Concat(this.randomizer.medals.ToList());
			cmb_starter_medal.SelectedItem = "Random Medal";            
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

        private void LoadROM(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                string chosenFile = openFileDialog.FileName;

                try
                {
					this.randomizer.LoadROM(chosenFile);
				}
                catch (InvalidRomException exception)
                {
					ShowNotification("Error!", exception.Message);
				}
                finally
				{
					romLabel.Content = this.randomizer.options.romLabel;
				}
			}
        }

        private void Randomize(object sender, RoutedEventArgs e)
        {
            this.randomizer.options.seedInput = (seed_input.Text != "") ? seed_input.Text : MedabotsRandomizer.Util.Utils.RandomString(12);
			this.randomizer.options.randomizerEnabled = chk_enable_randomizer.IsOn;
			this.randomizer.options.characterRandomizationEnabled = chk_randomize_characters.IsOn;
			this.randomizer.options.characterContinuityEnabled = chk_character_continuity.IsOn;
            this.randomizer.options.battleRandomizationEnabled = chk_randomize_battles.IsOn;
			this.randomizer.options.battleContinuityEnabled = chk_battle_continuity.IsOn;
			this.randomizer.options.battleStructureEnabled = chk_keep_battle_structure.IsOn;
			this.randomizer.options.mixedBotsEnabled = chk_allow_mixed_bots.IsOn;
            this.randomizer.options.mixedBotPartsPercentage = sl_mixed_bots.Value;
			this.randomizer.options.balancedBotLevelsEnabled = chk_balanced_bot_levels.IsOn;
			this.randomizer.options.shopRandomizationEnabled = chk_random_shops.IsOn;
			this.randomizer.options.starterRandomizationEnabled = chk_randomize_starter.IsOn;
            this.randomizer.options.starterMedalRandomizationEnabled = chk_randomize_starter_medal.IsOn;
			this.randomizer.options.medalRandomizationEnabled = chk_random_medal.IsOn;
			this.randomizer.options.codePatchingEnabled = chk_code_patches.IsOn;
			this.randomizer.options.instantTextEnabled = chk_instant_text.IsOn;
			this.randomizer.options.extraEncountersEnabled = chk_encounters.IsOn;

			byte part;
			if (this.randomizer.botDictionary.TryGetValue(cmb_starter.SelectedItem.ToString(), out part))
            {
				this.randomizer.options.starterBot = part;
			}

			byte medal;
            if (this.randomizer.medalDictionary.TryGetValue(cmb_starter_medal.SelectedItem.ToString(), out medal))
			{
				this.randomizer.options.starterMedal = medal;
			}

            this.randomizer.options.shopPatchingEnabled = chk_shop_patching.IsOn;
            this.randomizer.options.genderlessBotsEnabled = chk_genderless_bots.IsOn;

            try
            {
				this.randomizer.Randomize();
				ShowNotification("Done!", "The ROM has been converted and is saved with seed: \"" + this.randomizer.options.seedInput + "\" as \"" + this.randomizer.options.romLabel + " - " + this.randomizer.options.seedInput + ".gba\"");
			}
            catch (FileNotFoundException exception)
            {
                ShowNotification("Error", exception.Message);
            }
		}
	}
}
