using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace CollectionTracker {

	//Catalog object containing all card references and printing information
	[Serializable]
	public class PKMN_Catalog {

		//Card and printing lists
		public List<PKMN_Card> cards;
		public List<PKMN_Printing> printings;

		//Additional lists
		public List<PKMN_Set> sets;
		public List<PKMN_Symbol> symbols;

		//Constructor
		public PKMN_Catalog() {
			cards = new List<PKMN_Card>();
			printings = new List<PKMN_Printing>();
			sets = new List<PKMN_Set>();
			symbols = new List<PKMN_Symbol>();
		}

	}

	#region Card Data

	//Unique card data
	[Serializable]
	public class PKMN_Card {

		//Properties
		public string name;
		public string energyType;
		public string cardTypes;
		public string stage;
		public string oracleText;
		public string weakness;
		public string resistance;
		public string retreatCost;
		public int hp;

		//Constructor
		public PKMN_Card() : this("", "", "", "", "", "", "", "", 0) { }
		public PKMN_Card(string name, string energyType, string cardTypes, string stage, string oracleText, string weakness, string resistance, string retreatCost, int hp) {
			this.name = name;
			this.energyType = energyType;
			this.cardTypes = cardTypes;
			this.stage = stage;
			this.oracleText = oracleText;
			this.weakness = weakness;
			this.resistance = resistance;
			this.retreatCost = retreatCost;
			this.hp = hp;
		}

		//Copy function
		public void Copy(PKMN_Card card) {
			name = card.name;
			energyType = card.energyType;
			cardTypes = card.cardTypes;
			stage = card.stage;
			oracleText = card.oracleText;
			weakness = card.weakness;
			resistance = card.resistance;
			retreatCost = card.retreatCost;
			hp = card.hp;
		}

		//ToString
		public override string ToString() {
			string s = name + " | " + cardTypes;
			if (cardTypes.Contains("Pokémon")) {
				s += " | " + energyType + " " + hp.ToString();
				string[] lines = oracleText.Split(new string[] { "\r\n" }, StringSplitOptions.None);
				for (int i = 0; i < lines.Length; i += 3)
					s += " | " + lines[i];
			}
			else if (hp != 0)
				s += " | " + hp.ToString();
			return s;
		}

	}

	#endregion

	#region Printing Data

	//Unique information for each printing of a card, as well as collection status
	[Serializable]
	public class PKMN_Printing {

		//Properties
		public PKMN_Set set;
		public int cardNumber;
		public string flavorText;
		public string imgPath;
		public string backImgPath;
		public string rarity;
		public List<PKMN_Treatment> treatments;
		public PKMN_Card card;
		public string printID;

		//Constructor
		public PKMN_Printing() : this(null, 0, "", "", "", "", new List<PKMN_Treatment>(), new PKMN_Card(), "") { }
		public PKMN_Printing(PKMN_Set set, int cardNumber, string flavorText, string imgPath, string backImgPath, string rarity, List<PKMN_Treatment> treatments, PKMN_Card card, string printID) {
			this.set = set;
			this.cardNumber = cardNumber;
			this.flavorText = flavorText;
			this.imgPath = imgPath;
			this.backImgPath = backImgPath;
			this.rarity = rarity;
			this.treatments = new List<PKMN_Treatment>(treatments);
			this.card = card;
			this.printID = printID;
		}

		//Copy function
		public void Copy(PKMN_Printing print) {
			set = print.set;
			cardNumber = print.cardNumber;
			flavorText = print.flavorText;
			imgPath = print.imgPath;
			backImgPath = print.backImgPath;
			rarity = print.rarity;
			foreach (PKMN_Treatment treatment in print.treatments) {
				foreach (PKMN_Treatment treatment2 in treatments) {
					if (treatment.name.Equals(treatment2.name)) {
						treatment.locations = new List<string>(treatment2.locations);
						treatment.quantities = new List<int>(treatment2.quantities);
					}
				}
			}
			treatments = new List<PKMN_Treatment>();
			foreach (PKMN_Treatment treatment in print.treatments) { treatments.Add(new PKMN_Treatment(treatment)); }
			card = print.card;
			printID = print.printID;
		}

		//Increment count at location
		public void Increment(string name) => Increment(name, "Desk");
		public void Increment(string name, string location) {
			PKMN_Treatment treatment = treatments.FirstOrDefault(t => t.name.Equals(name));
			if (treatment != null) {
				int i = treatment.locations.IndexOf(location);
				if (i >= 0) { ++treatment.quantities[i]; }
				else {
					treatment.locations.Add(location);
					treatment.quantities.Add(1);
				}
			}
		}

		//Decrement count at location, return false if no cards were removed
		public bool Decrement(string name) {
			PKMN_Treatment treatment = treatments.FirstOrDefault(t => t.name.Equals(name));
			if (treatment != null) {
				if (treatment.locations.Count > 0) {
					return Decrement(treatment, 0);
				}
			}
			return false;
		}
		public bool Decrement(string name, string location) {
			PKMN_Treatment treatment = treatments.FirstOrDefault(t => t.name.Equals(name));
			if (treatment != null) {
				int i = treatment.locations.IndexOf(location);
				if (i >= 0) { return Decrement(treatment, i); }
				return false;
			}
			return false;
		}
		public bool Decrement(PKMN_Treatment treatment, int index) {
			if (treatment.quantities[index] <= 0) {
				treatment.locations.RemoveAt(index);
				treatment.quantities.RemoveAt(index);
				return false;
			}
			--treatment.quantities[index];
			if (treatment.quantities[index] <= 0) {
				treatment.locations.RemoveAt(index);
				treatment.quantities.RemoveAt(index);
			}
			return true;
		}

		//Move one copy of the specified rarity from one location to another
		public void MoveOne(string from, string to, string treatment) {
			if (Decrement(treatment, from)) {
				Increment(treatment, to);
			}
		}

		//Get total count owned at any rarity
		public int OwnedCount() {
			int count = 0;
			foreach (PKMN_Treatment treatment in treatments) {
				foreach (int quantity in treatment.quantities) {
					count += quantity;
				}
			}
			return count;
		}

		//Get count owned of a specified rarity
		public int OwnedCountOfTreatment(string name) {
			PKMN_Treatment treatment = treatments.FirstOrDefault(t => t.name.Equals(name));
			if (treatment != null) {
				int count = 0;
				foreach (int quantity in treatment.quantities) {
					count += quantity;
				}
				return count;
			}
			return 0;
		}

		//Return true if any amount is owned at any rarity
		public bool AnyOwned() {
			foreach (PKMN_Treatment treatment in treatments) {
				foreach (int quantity in treatment.quantities) {
					if (quantity > 0) { return true; };
				}
			}
			return false;
		}

		//Return true if collector's number starts with a character
		public bool HasSpecialCN() {
			string[] split = printID.Split('/');
			if (split.Length > 1 && split[1].Length > 0 && !char.IsDigit(split[1][0]))
				return true;
			return false;
		}

	}

	//Numeric comparer
	public class PKMN_PrintComparerNumeric : IComparer<PKMN_Printing> {
		public int Compare(PKMN_Printing print1, PKMN_Printing print2) {
			int setCompare = new PKMN_SetComparer().Compare(print1.set, print2.set);
			if (setCompare != 0) { return setCompare; }
			if (print1.HasSpecialCN() && !print2.HasSpecialCN()) { return -1; }
			if (print2.HasSpecialCN() && !print1.HasSpecialCN()) { return 1; }
			if (print1.cardNumber < print2.cardNumber) { return -1; }
			if (print2.cardNumber < print1.cardNumber) { return 1; }
			return 0;
		}
	}

	//Alphabetical comparer
	public class PKMN_PrintComparerAlphabetical : IComparer<PKMN_Printing> {
		public int Compare(PKMN_Printing print1, PKMN_Printing print2) {
			return print1.card.name.CompareTo(print2.card.name);
		}
	}

	#endregion

	#region Set Data

	[Serializable]
	public class PKMN_Set {

		//Properties
		public string name;
		public string code;
		public string imgPath;
		public DateTime date;

		//Constructor
		public PKMN_Set() : this("", "", "", DateTime.Now) { }
		public PKMN_Set(string name, string code, string imgPath, DateTime date) {
			this.name = name;
			this.code = code;
			this.imgPath = imgPath;
			this.date = date;
		}

		//Copy function
		public void Copy(PKMN_Set set) {
			name = set.name;
			code = set.code;
			imgPath = set.imgPath;
			date = set.date;
		}

		//ToString
		public override string ToString() => name;

	}

	//Comparer
	public class PKMN_SetComparer : IComparer<PKMN_Set> {
		public int Compare(PKMN_Set set1, PKMN_Set set2) {
			if (set1.date.Date > set2.date.Date) { return -1; }
			if (set2.date.Date > set1.date.Date) { return 1; }
			return 0;
		}
	}

	#endregion

	#region Symbol Data

	//Symbol data
	[Serializable]
	public class PKMN_Symbol {

		//Properties
		public string name;
		public string symbol;
		public string imgPath;
		public decimal aspect;

		//Constructor
		public PKMN_Symbol() : this("", "", "", 1.00m) { }
		public PKMN_Symbol(string name, string symbol, string imgPath, decimal aspect) {
			this.name = name;
			this.symbol = symbol;
			this.imgPath = imgPath;
			this.aspect = aspect;
		}

	}

	#endregion

	#region Treatment Data

	//Class containing the name of a treatment along with its owned quantities and their locations
	[Serializable]
	public class PKMN_Treatment {

		//Properties
		public string name;
		public List<string> locations;
		public List<int> quantities;

		//Constructor
		public PKMN_Treatment(string name) {
			this.name = name;
			locations = new List<string>();
			quantities = new List<int>();
		}

		//Copy constructor
		public PKMN_Treatment(PKMN_Treatment treatment) {
			name = treatment.name;
			locations = new List<string>(treatment.locations);
			quantities = new List<int>(treatment.quantities);
		}

		//Static default list generator
		public static List<PKMN_Treatment> GenerateTreatments(List<string> treatments) {
			List<PKMN_Treatment> list = new List<PKMN_Treatment>();
			foreach (string treatment in treatments) { list.Add(new PKMN_Treatment(treatment)); }
			return list;
		}

	}

	#endregion

	#region Utils

	//Utilities class
	public static class PKMN_Utils {

		//Card back image path
		public static readonly string CARD_BACK_PATH = "resources/pkmn/back.png";

		//Colour identity to string dictionary
		public static readonly Dictionary<string, string> typeSymbols = new Dictionary<string, string> {
			{ "Colorless" , "{C}" },
			{ "Darkness"  , "{D}" },
			{ "Fighting"  , "{F}" },
			{ "Grass"     , "{G}" },
			{ "Lightning" , "{L}" },
			{ "Metal"     , "{M}" },
			{ "Dragon"    , "{N}" },
			{ "Psychic"   , "{P}" },
			{ "Fire"      , "{R}" },
			{ "Water"     , "{W}" },
			{ "Fairy"     , "{Y}" },
		};

		//Returns string with tooltip text/markers removed
		public static string CleanOracleText(string str) {

			//Loop through text
			while (true) {

				//Get index of next object and return if we're done
				int i = str.IndexOf('[');
				if (i < 0) { return str.Replace("\r\n", " || "); }

				//Remove tooltip
				else if (str[i] == '[') {
					str = str.Remove(i, 1);
					int i2 = str.IndexOf("|", i);
					int i3 = str.IndexOf("]", i);
					if (i2 > 0 && i3 > 0) { str = str.Remove(i2, i3 + 1 - i2); }
				}

			}
		}

		//Replace all instances of energy types in a string with symbol indicators
		public static string ReplaceAllSymbols(string str) {
			foreach(KeyValuePair<string, string> kvp in typeSymbols)
				str = str.Replace(kvp.Key, kvp.Value);
			str = str.Replace(" ", "");
			return str;
		}

		//Replace all instances of energy types in a string with symbol indicators (without clearing spaces)
		public static string ReplaceSymbolsInText(string str) {
			foreach (KeyValuePair<string, string> kvp in typeSymbols)
				str = str.Replace(kvp.Key, kvp.Value);
			return str;
		}

		//Get first index of a subset of symbols. Returns -1 if there are no instances of any of the provided symbols
		public static int IndexOfMany(string str, List<char> chars) {
			int index = -1;
			foreach (char c in chars) {
				int i = str.IndexOf(c);
				if (i != -1)
					if (index == -1 || i < index)
						index = i;
			}
			return index;
		}

		//Load image. Load nothing if it doesn't exist
		public static bool TryLoadImage(PictureBox box, string path) {
			try { box.Load(path); return true; }
			catch (Exception) { return false; }
		}

		//Load card image. If it doesn't exist, load default image
		public static bool TryLoadCardImage(PictureBox box, string path, Label label = null) {
			try {
				box.Load(path);
				if (label != null) { label.Text = path; }
				return true;
			}
			catch (Exception) {
				try { box.Load(CARD_BACK_PATH); }
				catch (Exception) { }
				if (label != null) { label.Text = ""; }
				return false;
			}
		}

	}

	#endregion

}
