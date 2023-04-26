using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedabotsRandomizer.Data
{
	public class RomData
	{
		public string romHash { get; set; }
		public string romName { get; set; }
		public int battlesOffset { get; set; }
		public int starterOffset { get; set; }
		public int textOffset { get; set; }

		public RomData() { }

		public RomData(string romHash, string romName, int battlesOffset, int starterOffset, int textOffset)
		{
			this.romHash = romHash;
			this.romName = romName;
			this.battlesOffset = battlesOffset;
			this.starterOffset = starterOffset;
			this.textOffset = textOffset;
		}

		public Dictionary<OffsetEnum, int> GetOffsetDictionary()
		{
			Dictionary<OffsetEnum, int> dictionary = new Dictionary<OffsetEnum, int>();
			dictionary.Add(OffsetEnum.Battles, this.battlesOffset);
			dictionary.Add(OffsetEnum.Starter, this.starterOffset);
			dictionary.Add(OffsetEnum.Text, this.textOffset);

			return dictionary;
		}
	}
}
