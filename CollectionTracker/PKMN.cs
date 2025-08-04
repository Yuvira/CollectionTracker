using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	#region Catalog

	//Catalog object containing all card references and printing information
	[ProtoContract]
	public class PKMN_Catalog {

		//Properties
		[ProtoMember(1)] public List<PKMN_Card> cards;
		[ProtoMember(2)] public List<PKMN_Printing> printings;
		[ProtoMember(3)] public List<PKMN_Set> sets;
		[ProtoMember(4)] public List<PKMN_Symbol> symbols;

		//Constructor
		public PKMN_Catalog() {
			cards = new List<PKMN_Card>();
			printings = new List<PKMN_Printing>();
			sets = new List<PKMN_Set>();
			symbols = new List<PKMN_Symbol>();
		}

		//Save and load print references
		public void SavePrintRefs() {
			foreach (PKMN_Printing print in printings)
				print.SaveRefs(this);
		}
		public void LoadPrintRefs() {
			foreach (PKMN_Printing print in printings)
				print.LoadRefs(this);
		}

	}

	#endregion

	#region Card Data

	//Unique card data
	[ProtoContract]
	public class PKMN_Card {

		//Properties
		[ProtoMember(1)] public string name;
		[ProtoMember(2)] public string energyType;
		[ProtoMember(3)] public string cardTypes;
		[ProtoMember(4)] public string stage;
		[ProtoMember(5)] public string oracleText;
		[ProtoMember(6)] public string weakness;
		[ProtoMember(7)] public string resistance;
		[ProtoMember(8)] public string retreatCost;
		[ProtoMember(9)] public int hp;

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
				foreach (string line in lines) {
					if (line.StartsWith("{") || PKMN_Utils.abilityTerms.Keys.Any(t => line.StartsWith(t)))
						s += " | " + line;
				}
				s += " | " + weakness + " | " + resistance + " | " + retreatCost;
			}
			else if (hp != 0)
				s += " | " + hp.ToString();
			if (PKMN_Utils.catalog != null)
				s = PKMN_Utils.ReplaceSymbols(s, PKMN_Utils.catalog.symbols);
			return s;
		}

	}

	#endregion

	#region Printing Data

	//Unique information for each printing of a card, as well as collection status
	[ProtoContract]
	public class PKMN_Printing {

		//Properties
		[ProtoMember(1)] private int setIndex; public PKMN_Set set;
		[ProtoMember(2)] public int cardNumber;
		[ProtoMember(3)] public string flavorText;
		[ProtoMember(4)] public string imgPath;
		[ProtoMember(5)] public string backImgPath;
		[ProtoMember(6)] public string rarity;
		[ProtoMember(7)] public List<PKMN_Treatment> treatments;
		[ProtoMember(8)] private int cardIndex; public PKMN_Card card;
		[ProtoMember(9)] public string printID;

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
		public bool IsBonusSheet() {
			int idx = printID.IndexOf('/');
			if (idx >= 0 && idx < printID.Length - 1 && !char.IsDigit(printID[idx + 1]))
				return true;
			return false;
		}

		//Save and load reference objects
		public void SaveRefs(PKMN_Catalog catalog) {
			setIndex = catalog.sets.IndexOf(set);
			cardIndex = catalog.cards.IndexOf(card);
		}
		public void LoadRefs(PKMN_Catalog catalog) {
			set = catalog.sets[setIndex];
			card = catalog.cards[cardIndex];
		}

	}

	//Numeric comparer
	public class PKMN_PrintComparerNumeric : IComparer<PKMN_Printing> {
		public int Compare(PKMN_Printing print1, PKMN_Printing print2) {
			int setCompare = new PKMN_SetComparer().Compare(print1.set, print2.set);
			if (setCompare != 0) { return setCompare; }
			if (print1.IsBonusSheet() && !print2.IsBonusSheet()) { return print1.set.leadBonusSheet ? -1 : 1; }
			if (print2.IsBonusSheet() && !print1.IsBonusSheet()) { return print2.set.leadBonusSheet ? 1 : -1; }
			if (print1.cardNumber < print2.cardNumber) { return -1; }
			if (print2.cardNumber < print1.cardNumber) { return 1; }
			return string.Compare(print1.printID, print2.printID);
		}
	}

	//Reverse numeric comparer
	public class PKMN_PrintComparerNumericReverse : IComparer<PKMN_Printing> {
		public int Compare(PKMN_Printing print1, PKMN_Printing print2) {
			int setCompare = new PKMN_SetComparer().Compare(print1.set, print2.set);
			if (setCompare != 0) { return setCompare; }
			if (print1.IsBonusSheet() && !print2.IsBonusSheet()) { return print1.set.leadBonusSheet ? 1 : -1; }
			if (print2.IsBonusSheet() && !print1.IsBonusSheet()) { return print2.set.leadBonusSheet ? -1 : 1; }
			if (print1.cardNumber < print2.cardNumber) { return 1; }
			if (print2.cardNumber < print1.cardNumber) { return -1; }
			return string.Compare(print1.printID, print2.printID) * -1;
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

	[ProtoContract]
	public class PKMN_Set {

		//Properties
		[ProtoMember(1)] public string name;
		[ProtoMember(2)] public string code;
		[ProtoMember(6)] public string setType;
		[ProtoMember(3)] public string imgPath;
		[ProtoMember(4)] public DateTime date;
		[ProtoMember(5)] public bool leadBonusSheet;

		//Constructor
		public PKMN_Set() : this("", "", "", "", DateTime.Now, false) { }
		public PKMN_Set(string name, string code, string setType, string imgPath, DateTime date, bool leadBonusSheet) {
			this.name = name;
			this.code = code;
			this.setType = setType;
			this.imgPath = imgPath;
			this.date = date;
			this.leadBonusSheet = leadBonusSheet;
		}

		//Copy function
		public void Copy(PKMN_Set set) {
			name = set.name;
			code = set.code;
			setType = set.setType;
			imgPath = set.imgPath;
			date = set.date;
			leadBonusSheet = set.leadBonusSheet;
		}

		//ToString
		public override string ToString() => name;

	}

	//Comparer
	public class PKMN_SetComparer : IComparer<PKMN_Set> {
		public int Compare(PKMN_Set set1, PKMN_Set set2) {
			if (set1.date.Date > set2.date.Date) { return -1; }
			if (set2.date.Date > set1.date.Date) { return 1; }
			return string.Compare(set1.ToString(), set2.ToString());
		}
	}

	#endregion

	#region Symbol Data

	//Symbol data
	[ProtoContract]
	public class PKMN_Symbol {

		//Properties
		[ProtoMember(2)] public string symbol;
		[ProtoMember(1)] public string name;
		[ProtoMember(3)] public string imgPath;
		[ProtoMember(4)] public decimal aspect;

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
	[ProtoContract]
	public class PKMN_Treatment {

		//Properties
		[ProtoMember(1)] public string name;
		[ProtoMember(2)] public List<string> locations;
		[ProtoMember(3)] public List<int> quantities;

		//Constructor
		public PKMN_Treatment() : this("") { }
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

		//Catalog reference
		public static PKMN_Catalog catalog;

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
			{ "Typeless"  , "{A}" },
		};

		//Ability term to string dictionary
		public static readonly Dictionary<string, string> abilitySymbols = new Dictionary<string, string> {
			{ "Poké-POWER —" , "{PK-POW}"  },
			{ "Poké-BODY —"  , "{PK-BDY}"  },
			{ "Ability —"    , "{ABILITY}" },
		};

		//Ability term list
		public static readonly Dictionary<string, string> abilityTerms = new Dictionary<string, string> {
			{ "Pokémon Power", "`*" },
			{ "Poké-POWER",    "`*" },
			{ "Poké-BODY",     "`~" },
			{ "Card Effect",   "`%" },
			{ "Held Item",     "`%" },
		};

		//Ability symbol list
		public static readonly Dictionary<string, string> abilityMarkers = new Dictionary<string, string> {
			{ "{PK-POW}",  "*" },
			{ "{PK-BDY}",  "~" },
			{ "{ABILITY}", "*" },
		};

		//Marker colour list
		public static readonly Dictionary<string, Color> markerColours = new Dictionary<string, Color> {
			{ "*", Color.Red   },
			{ "~", Color.Green },
			{ "%", Color.White },
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

		//Replace all symbols from a given list with text
		public static string ReplaceSymbols(string str, List<PKMN_Symbol> symbols) {
			foreach (PKMN_Symbol symbol in symbols)
				str = str.Replace(symbol.symbol, symbol.name);
			return str;
		}

		//Replace all instances of energy types in a string with symbol indicators
		public static string ReplaceTypeSymbols(string str, bool clearSpaces) {
			foreach (KeyValuePair<string, string> kvp in typeSymbols)
				str = str.Replace(kvp.Key, kvp.Value);
			if (clearSpaces)
				str = str.Replace(" ", "");
			return str;
		}

		//Replace all instances of ability terms in a string with symbol indicators
		public static string ReplaceAbilitySymbols(string str) {
			foreach (KeyValuePair<string, string> kvp in abilitySymbols)
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

		//Convert symbol list to dictionary
		public static Dictionary<string, string> SymbolsToDict(List<PKMN_Symbol> symbols) {
			Dictionary<string, string> dict = new Dictionary<string, string>();
			foreach (PKMN_Symbol symbol in symbols)
				dict.Add(symbol.symbol, symbol.name);
			return dict;
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
