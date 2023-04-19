using System;

namespace MedabotsRandomizer
{
	public class RandomizerOptions
	{
		public string gameId { get; set; }
		public string romLabel { get; set; }
		public string seedInput { get; set; }
		public bool randomizerEnabled { get; set; } = false;
		public bool characterRandomizationEnabled { get; set; } = false;
		public bool characterContinuityEnabled { get; set; } = false;
		public bool battleRandomizationEnabled { get; set; } = false;
		public bool battleContinuityEnabled { get; set; } = false;
		public bool battleStructureEnabled { get; set; } = false;
		public bool mixedBotsEnabled { get; set; } = false;
		public double mixedBotPartsPercentage { get; set; } = 50;
		public bool balancedBotLevelsEnabled { get; set; } = false;
		public bool shopRandomizationEnabled { get; set; } = false;
		public bool starterRandomizationEnabled { get; set; } = false;
		public bool starterMedalRandomizationEnabled { get; set; } = false;
		public bool medalRandomizationEnabled { get; set; } = false;
		public bool codePatchingEnabled { get; set; } = false;
		public bool instantTextEnabled { get; set; } = false;
		public bool extraEncountersEnabled { get; set; } = false;

		public byte starterBot { get; set; } = 0xF8;
		public byte starterMedal { get; set; } = 0xF8;

		public bool shopPatchingEnabled { get; set; } = false;
		public bool genderlessBotsEnabled { get; set; } = false;
	}
}
