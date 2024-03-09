using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionTracker {

	#region Enums

	//Colour enum
	[Flags]
	public enum MTG_Colour {
		None = 0,
		White = 1,
		Blue = 2,
		Black = 4,
		Red = 8,
		Green = 16
	}

	#endregion

	//Catalog object containing all card references and printing information
	[Serializable]
	public class MTG_Catalog {

		//Card and printing lists
		public List<MTG_Card> cards;
		public List<MTG_Printing> printings;

		//Additional lists
		public List<MTG_Set> sets;
		public List<MTG_Symbol> symbols;

		//Constructor
		public MTG_Catalog() {
			cards = new List<MTG_Card>();
			printings = new List<MTG_Printing>();
			symbols = new List<MTG_Symbol>();
		}

	}

	#region Card Data

	//Unique card data
	[Serializable]
	public class MTG_Card {

		//Properties
		public string name;
		public MTG_Colour identity;
		public MTG_Colour colour;
		public string cost;
		public string cardTypes;
		public string oracleText;
		public int power;
		public int toughness;
		public int power2;
		public int toughness2;

		//Constructor
		public MTG_Card() : this("", 0, 0, "", "", "", 0, 0, 0, 0) { }
		public MTG_Card(string name, MTG_Colour identity, MTG_Colour colour, string cost, string cardTypes, string oracleText, int power, int toughness, int power2, int toughness2) {
			this.name = name;
			this.identity = identity;
			this.colour = colour;
			this.cost = cost;
			this.cardTypes = cardTypes;
			this.oracleText = oracleText;
			this.power = power;
			this.toughness = toughness;
			this.power2 = power2;
			this.toughness2 = toughness2;
		}

		//Copy function
		public void Copy(MTG_Card card) {
			name = card.name;
			identity = card.identity;
			colour = card.colour;
			cost = card.cost;
			cardTypes = card.cardTypes;
			oracleText = card.oracleText;
			power = card.power;
			toughness = card.toughness;
			power2 = card.power2;
			toughness2 = card.toughness2;
		}

		//ToString
		public override string ToString() => name;

	}

	#endregion

	#region Printing Data

	//Unique information for each printing of a card, as well as collection status
	[Serializable]
	public class MTG_Printing {

		//Properties
		public MTG_Set set;
		public int cardNumber;
		public string flavorText;
		public string imgPath;
		public string backImgPath;
		public string rarity;
		public List<MTG_Treatment> treatments;
		public MTG_Card card;
		public string scryfallID;

		//Constructor
		public MTG_Printing() : this(null, 0, "", "", "", "", new List<MTG_Treatment>(), new MTG_Card(), "") { }
		public MTG_Printing(MTG_Set set, int cardNumber, string flavorText, string imgPath, string backImgPath, string rarity, List<MTG_Treatment> treatments, MTG_Card card, string scryfallID) {
			this.set = set;
			this.cardNumber = cardNumber;
			this.flavorText = flavorText;
			this.imgPath = imgPath;
			this.backImgPath = backImgPath;
			this.rarity = rarity;
			this.treatments = new List<MTG_Treatment>(treatments);
			this.card = card;
			this.scryfallID = scryfallID;
		}

		//Copy function
		public void Copy(MTG_Printing print) {
			set = print.set;
			cardNumber = print.cardNumber;
			flavorText = print.flavorText;
			imgPath = print.imgPath;
			backImgPath = print.backImgPath;
			rarity = print.rarity;
			treatments = new List<MTG_Treatment>();
			foreach (MTG_Treatment treatment in print.treatments) { treatments.Add(new MTG_Treatment(treatment)); }
			card = print.card;
			scryfallID = print.scryfallID;
		}

		//Increment count at location
		public void Increment(string name) => Increment(name, "Desk");
		public void Increment(string name, string location) {
			MTG_Treatment treatment = treatments.FirstOrDefault(t => t.name.Equals(name));
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
			MTG_Treatment treatment = treatments.FirstOrDefault(t => t.name.Equals(name));
			if (treatment != null) {
				if (treatment.locations.Count > 0) {
					return Decrement(treatment, 0);
				}
			}
			return false;
		}
		public bool Decrement(string name, string location) {
			MTG_Treatment treatment = treatments.FirstOrDefault(t => t.name.Equals(name));
			if (treatment != null) {
				int i = treatment.locations.IndexOf(location);
				if (i >= 0) { return Decrement(treatment, i); }
				return false;
			}
			return false;
		}
		public bool Decrement(MTG_Treatment treatment, int index) {
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
			foreach (MTG_Treatment treatment in treatments) {
				foreach (int quantity in treatment.quantities) {
					count += quantity;
				}
			}
			return count;
		}

		//Get count owned of a specified rarity
		public int OwnedCountOfTreatment(string name) {
			MTG_Treatment treatment = treatments.FirstOrDefault(t => t.name.Equals(name));
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
			foreach (MTG_Treatment treatment in treatments) {
				foreach (int quantity in treatment.quantities) {
					if (quantity > 0) { return true; };
				}
			}
			return false;
		}

	}

	#endregion

	#region Set Data

	[Serializable]
	public class MTG_Set {

		//Properties
		public string name;
		public string code;
		public string imgPath;
		public DateTime date;
		public int order;
		public int indent;

		//Constructor
		public MTG_Set() : this("", "", "", DateTime.Now, 0, 0) { }
		public MTG_Set(string name, string code, string imgPath, DateTime date, int order, int indent) {
			this.name = name;
			this.code = code;
			this.imgPath = imgPath;
			this.date = date;
			this.order = order;
			this.indent = indent;
		}

		//Copy function
		public void Copy(MTG_Set set) {
			name = set.name;
			code = set.code;
			imgPath = set.imgPath;
			date = set.date;
			order = set.order;
			indent = set.indent;
		}

		//ToString
		public override string ToString() => name;

	}

	//Comparer
	public class SetComparer : IComparer<MTG_Set> {
		public int Compare(MTG_Set set1, MTG_Set set2) {
			if (set1.date.Date > set2.date.Date) { return -1; }
			if (set2.date.Date > set1.date.Date) { return 1; }
			if (set1.order < set2.order) { return -1; }
			if (set2.order < set1.order) { return 1; }
			return 0;
		}
	}

	#endregion

	#region Symbol Data

	//Symbol data
	[Serializable]
	public class MTG_Symbol {

		//Properties
		public string name;
		public string symbol;
		public string imgPath;
		public decimal aspect;

		//Constructor
		public MTG_Symbol() : this("", "", "", 1.00m) { }
		public MTG_Symbol(string name, string symbol, string imgPath, decimal aspect) {
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
	public class MTG_Treatment {

		//Properties
		public string name;
		public List<string> locations;
		public List<int> quantities;

		//Constructor
		public MTG_Treatment(string name) {
			this.name = name;
			locations = new List<string>();
			quantities = new List<int>();
		}

		//Copy constructor
		public MTG_Treatment(MTG_Treatment treatment) {
			name = treatment.name;
			locations = new List<string>(treatment.locations);
			quantities = new List<int>(treatment.quantities);
		}

		//Static default list generator
		public static List<MTG_Treatment> GenerateTreatments(List<string> treatments) {
			List<MTG_Treatment> list = new List<MTG_Treatment>();
			foreach (string treatment in treatments) { list.Add(new MTG_Treatment(treatment)); }
			return list;
		}

	}

	#endregion

	#region Utils

	//Utilities class
	public static class MTG_Utils { 

		//Colour identity to string dictionary
		public static readonly Dictionary<MTG_Colour, string> colourNames = new Dictionary<MTG_Colour, string> {
			{ MTG_Colour.None, "Colourless" },
			{ MTG_Colour.White, "White" },
			{ MTG_Colour.Blue, "Blue" },
			{ MTG_Colour.Black, "Black" },
			{ MTG_Colour.Red, "Red" },
			{ MTG_Colour.Green, "Green" },
			{ MTG_Colour.White | MTG_Colour.Blue, "Azorius" },
			{ MTG_Colour.White | MTG_Colour.Black, "Orzhov" },
			{ MTG_Colour.White | MTG_Colour.Red, "Boros" },
			{ MTG_Colour.White | MTG_Colour.Green, "Selesnya" },
			{ MTG_Colour.Blue | MTG_Colour.Black, "Dimir" },
			{ MTG_Colour.Blue | MTG_Colour.Red, "Izzet" },
			{ MTG_Colour.Blue | MTG_Colour.Green, "Simic" },
			{ MTG_Colour.Black | MTG_Colour.Red, "Rakdos" },
			{ MTG_Colour.Black | MTG_Colour.Green, "Golgari" },
			{ MTG_Colour.Red | MTG_Colour.Green, "Gruul" },
			{ MTG_Colour.White | MTG_Colour.Blue | MTG_Colour.Black, "Esper" },
			{ MTG_Colour.White | MTG_Colour.Blue | MTG_Colour.Red, "Jeskai" },
			{ MTG_Colour.White | MTG_Colour.Blue | MTG_Colour.Green, "Bant" },
			{ MTG_Colour.White | MTG_Colour.Black | MTG_Colour.Red, "Mardu" },
			{ MTG_Colour.White | MTG_Colour.Black | MTG_Colour.Green, "Abzan" },
			{ MTG_Colour.White | MTG_Colour.Red | MTG_Colour.Green, "Naya" },
			{ MTG_Colour.Blue | MTG_Colour.Black | MTG_Colour.Red, "Grixis" },
			{ MTG_Colour.Blue | MTG_Colour.Black | MTG_Colour.Green, "Sultai" },
			{ MTG_Colour.Blue | MTG_Colour.Red | MTG_Colour.Green, "Temur" },
			{ MTG_Colour.Black | MTG_Colour.Red | MTG_Colour.Green, "Jund" },
			{ MTG_Colour.White | MTG_Colour.Blue | MTG_Colour.Black | MTG_Colour.Red, "Yore-Tiller" },
			{ MTG_Colour.White | MTG_Colour.Blue | MTG_Colour.Black | MTG_Colour.Green, "Witch-Maw" },
			{ MTG_Colour.White | MTG_Colour.Blue | MTG_Colour.Red | MTG_Colour.Green, "Ink-Treader" },
			{ MTG_Colour.White | MTG_Colour.Black | MTG_Colour.Red | MTG_Colour.Green, "Dune-Brood" },
			{ MTG_Colour.Blue | MTG_Colour.Black | MTG_Colour.Red | MTG_Colour.Green, "Glint-Eye" },
			{ MTG_Colour.White | MTG_Colour.Blue | MTG_Colour.Black | MTG_Colour.Red | MTG_Colour.Green, "WUBRG" },
		};

	}

	#endregion

}
