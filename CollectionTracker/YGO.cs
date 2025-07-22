using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	#region Catalog

	//Catalog object containing all card references and printing information
	[ProtoContract]
	public class YGO_Catalog {

		//Properties
		[ProtoMember(1)] public List<YGO_Card> cards;
		[ProtoMember(2)] public List<YGO_Printing> printings;
		[ProtoMember(3)] public List<YGO_Set> sets;

		//Constructor
		public YGO_Catalog() {
			cards = new List<YGO_Card>();
			printings = new List<YGO_Printing>();
			sets = new List<YGO_Set>();
		}

		//Save and load print references
		public void SavePrintRefs() {
			foreach (YGO_Printing print in printings)
				print.SaveRefs(this);
		}
		public void LoadPrintRefs() {
			foreach (YGO_Printing print in printings)
				print.LoadRefs(this);
		}

	}

	#endregion

	#region Card Data

	//Unique card data
	[ProtoContract]
	public class YGO_Card {

		//Properties
		[ProtoMember(1)]  public string name;
		[ProtoMember(2)]  public string cardType;
		[ProtoMember(3)]  public string attribute;
		[ProtoMember(4)]  public string property;
		[ProtoMember(5)]  public string types;
		[ProtoMember(6)]  public string oracleText;
		[ProtoMember(7)]  public int level;
		[ProtoMember(8)]  public int pendulumScale;
		[ProtoMember(9)]  public int atk;
		[ProtoMember(10)] public int def;

		//Constructor
		public YGO_Card() : this("", "", "", "", "", "", 0, 0, 0, 0) { }
		public YGO_Card(string name, string cardType, string attribute, string property, string types, string oracleText, int level, int pendulumScale, int atk, int def) {
			this.name = name;
			this.cardType = cardType;
			this.attribute = attribute;
			this.property = property;
			this.types = types;
			this.oracleText = oracleText;
			this.level = level;
			this.pendulumScale = pendulumScale;
			this.atk = atk;
			this.def = def;
		}

		//Copy function
		public void Copy(YGO_Card card) {
			name = card.name;
			cardType = card.cardType;
			attribute = card.attribute;
			property = card.property;
			types = card.types;
			oracleText = card.oracleText;
			level = card.level;
			pendulumScale = card.pendulumScale;
			atk = card.atk;
			def = card.def;
		}

		//ToString
		public override string ToString() => name;

	}

	#endregion

	#region Printing Data

	//Unique information for each printing of a card, as well as collection status
	[ProtoContract]
	public class YGO_Printing {

		//Properties
		[ProtoMember(1)] private int setIndex; public YGO_Set set;
		[ProtoMember(2)] public int cardNumber;
		[ProtoMember(3)] public string flavorText;
		[ProtoMember(4)] public string imgPath;
		[ProtoMember(5)] public string backImgPath;
		[ProtoMember(6)] public List<YGO_Rarity> rarities;
		[ProtoMember(7)] private int cardIndex; public YGO_Card card;
		[ProtoMember(8)] public string printID;

		//Constructor
		public YGO_Printing() : this(null, 0, "", "", "", new List<YGO_Rarity>(), new YGO_Card(), "") { }
		public YGO_Printing(YGO_Set set, int cardNumber, string flavorText, string imgPath, string backImgPath, List<YGO_Rarity> rarities, YGO_Card card, string printID) {
			this.set = set;
			this.cardNumber = cardNumber;
			this.flavorText = flavorText;
			this.imgPath = imgPath;
			this.backImgPath = backImgPath;
			this.rarities = new List<YGO_Rarity>(rarities);
			this.card = card;
			this.printID = printID;
		}

		//Copy function
		public void Copy(YGO_Printing print) {
			set = print.set;
			cardNumber = print.cardNumber;
			flavorText = print.flavorText;
			imgPath = print.imgPath;
			backImgPath = print.backImgPath;
			foreach (YGO_Rarity rarity in print.rarities) {
				foreach (YGO_Rarity rarity2 in rarities) {
					if (rarity.name.Equals(rarity2.name)) {
						rarity.locations = new List<string>(rarity2.locations);
						rarity.quantities = new List<int>(rarity2.quantities);
					}
				}
			}
			rarities = new List<YGO_Rarity>();
			foreach (YGO_Rarity rarity in print.rarities) { rarities.Add(new YGO_Rarity(rarity)); }
			card = print.card;
			printID = print.printID;
		}

		//Increment count at location
		public void Increment(string name) => Increment(name, "Desk");
		public void Increment(string name, string location) {
			YGO_Rarity rarity = rarities.FirstOrDefault(t => t.name.Equals(name));
			if (rarity != null) {
				int i = rarity.locations.IndexOf(location);
				if (i >= 0) { ++rarity.quantities[i]; }
				else {
					rarity.locations.Add(location);
					rarity.quantities.Add(1);
				}
			}
		}

		//Decrement count at location, return false if no cards were removed
		public bool Decrement(string name) {
			YGO_Rarity rarity = rarities.FirstOrDefault(t => t.name.Equals(name));
			if (rarity != null) {
				if (rarity.locations.Count > 0) {
					return Decrement(rarity, 0);
				}
			}
			return false;
		}
		public bool Decrement(string name, string location) {
			YGO_Rarity rarity = rarities.FirstOrDefault(t => t.name.Equals(name));
			if (rarity != null) {
				int i = rarity.locations.IndexOf(location);
				if (i >= 0) { return Decrement(rarity, i); }
				return false;
			}
			return false;
		}
		public bool Decrement(YGO_Rarity rarity, int index) {
			if (rarity.quantities[index] <= 0) {
				rarity.locations.RemoveAt(index);
				rarity.quantities.RemoveAt(index);
				return false;
			}
			--rarity.quantities[index];
			if (rarity.quantities[index] <= 0) {
				rarity.locations.RemoveAt(index);
				rarity.quantities.RemoveAt(index);
			}
			return true;
		}

		//Move one copy of the specified rarity from one location to another
		public void MoveOne(string from, string to, string rarity) {
			if (Decrement(rarity, from)) {
				Increment(rarity, to);
			}
		}

		//Get total count owned at any rarity
		public int OwnedCount() {
			int count = 0;
			foreach (YGO_Rarity rarity in rarities) {
				foreach (int quantity in rarity.quantities) {
					count += quantity;
				}
			}
			return count;
		}

		//Get count owned of a specified rarity
		public int OwnedCountOfTreatment(string name) {
			YGO_Rarity rarity = rarities.FirstOrDefault(t => t.name.Equals(name));
			if (rarity != null) {
				int count = 0;
				foreach (int quantity in rarity.quantities) {
					count += quantity;
				}
				return count;
			}
			return 0;
		}

		//Return true if any amount is owned at any rarity
		public bool AnyOwned() {
			foreach (YGO_Rarity rarity in rarities) {
				foreach (int quantity in rarity.quantities) {
					if (quantity > 0) { return true; };
				}
			}
			return false;
		}

		//Save and load reference objects
		public void SaveRefs(YGO_Catalog catalog) {
			setIndex = catalog.sets.IndexOf(set);
			cardIndex = catalog.cards.IndexOf(card);
		}
		public void LoadRefs(YGO_Catalog catalog) {
			set = catalog.sets[setIndex];
			card = catalog.cards[cardIndex];
		}

	}

	//Numeric comparer
	public class YGO_PrintComparerNumeric : IComparer<YGO_Printing> {
		public int Compare(YGO_Printing print1, YGO_Printing print2) {
			int setCompare = new YGO_SetComparer().Compare(print1.set, print2.set);
			if (setCompare != 0) { return setCompare; }
			if (print1.cardNumber < print2.cardNumber) { return -1; }
			if (print2.cardNumber < print1.cardNumber) { return 1; }
			return 0;
		}
	}

	//Alphabetical comparer
	public class YGO_PrintComparerAlphabetical : IComparer<YGO_Printing> {
		public int Compare(YGO_Printing print1, YGO_Printing print2) {
			return print1.card.name.CompareTo(print2.card.name);
		}
	}

	#endregion

	#region Set Data

	[ProtoContract]
	public class YGO_Set {

		//Properties
		[ProtoMember(1)] public string name;
		[ProtoMember(2)] public string code;
		[ProtoMember(3)] public string imgPath;
		[ProtoMember(4)] public DateTime date;

		//Constructor
		public YGO_Set() : this("", "", "", DateTime.Now) { }
		public YGO_Set(string name, string code, string imgPath, DateTime date) {
			this.name = name;
			this.code = code;
			this.imgPath = imgPath;
			this.date = date;
		}

		//Copy function
		public void Copy(YGO_Set set) {
			name = set.name;
			code = set.code;
			imgPath = set.imgPath;
			date = set.date;
		}

		//ToString
		public override string ToString() => name;

	}

	//Comparer
	public class YGO_SetComparer : IComparer<YGO_Set> {
		public int Compare(YGO_Set set1, YGO_Set set2) {
			if (set1.date.Date > set2.date.Date) { return -1; }
			if (set2.date.Date > set1.date.Date) { return 1; }
			return 0;
		}
	}

	#endregion

	#region Rarity Data

	//Class containing the name of a treatment along with its owned quantities and their locations
	[ProtoContract]
	public class YGO_Rarity {

		//Properties
		[ProtoMember(1)] public string name;
		[ProtoMember(2)] public List<string> locations;
		[ProtoMember(3)] public List<int> quantities;

		//Constructor
		public YGO_Rarity() : this("") { }
		public YGO_Rarity(string name) {
			this.name = name;
			locations = new List<string>();
			quantities = new List<int>();
		}

		//Copy constructor
		public YGO_Rarity(YGO_Rarity rarity) {
			name = rarity.name;
			locations = new List<string>(rarity.locations);
			quantities = new List<int>(rarity.quantities);
		}

		//Static default list generator
		public static List<YGO_Rarity> GenerateTreatments(List<string> rarities) {
			List<YGO_Rarity> list = new List<YGO_Rarity>();
			foreach (string rarity in rarities) { list.Add(new YGO_Rarity(rarity)); }
			return list;
		}

	}

	#endregion

	#region Utils

	//Utilities class
	public static class YGO_Utils {

		//Card back image path
		public static readonly string CARD_BACK_PATH = "resources/ygo/back.png";

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
