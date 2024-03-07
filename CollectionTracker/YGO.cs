using System;
using System.Collections.Generic;

namespace CollectionTracker {

	//Enum dictionaries
	public class YGO_Enums {
		public Dictionary<int, string> sets = new Dictionary<int, string>();
		public Dictionary<int, string> cardTypes = new Dictionary<int, string>();
		public Dictionary<int, string> attributes = new Dictionary<int, string>();
		public Dictionary<int, string> monsterTypes = new Dictionary<int, string>();
		public Dictionary<int, string> rarities = new Dictionary<int, string>();
		public Dictionary<int, string> locations = new Dictionary<int, string>();
		public void Clear() {
			sets.Clear();
			cardTypes.Clear();
			attributes.Clear();
			monsterTypes.Clear();
			rarities.Clear();
			locations.Clear();
		}
	}

	//Catalog object containing all card references and printing information
	[Serializable]
	public class YGO_Catalog {

		//Card and printing lists
		public List<YGO_Card> cards;
		public List<YGO_Printing> printings;

		//Constructor
		public YGO_Catalog() {
			cards = new List<YGO_Card>();
			printings = new List<YGO_Printing>();
		}

		//Copy constructor
		public YGO_Catalog(YGO_Catalog catalog) {
			cards = new List<YGO_Card>();
			foreach (YGO_Card card in catalog.cards) { cards.Add(new YGO_Card(card)); }
			printings = new List<YGO_Printing>();
			foreach (YGO_Printing print in catalog.printings) { printings.Add(new YGO_Printing(print)); }
		}

	}

	//Unique card data
	[Serializable]
	public class YGO_Card {

		//Properties
		public string name;
		public int cardType;
		public int attribute;
		public List<int> monsterTypes;
		public string oracleText;
		public int pendulumLeft;
		public int pendulumRight;
		public int level;
		public int attack;
		public int defense;
		public List<int> printingIDs;

		//Constructor
		public YGO_Card() : this("", 0, 0, new List<int>(), "", 0, 0, 0, 0, 0) { }
		public YGO_Card(string name, int cardType, int attribute, List<int> monsterTypes, string oracleText, int pendulumLeft, int pendulumRight,  int level, int attack, int defense) {
			this.name = name;
			this.cardType = cardType;
			this.attribute = attribute;
			this.monsterTypes = new List<int>(monsterTypes);
			this.oracleText = oracleText;
			this.pendulumLeft = pendulumLeft;
			this.pendulumRight = pendulumRight;
			this.level = level;
			this.attack = attack;
			this.defense = defense;
			this.printingIDs = new List<int>();
		}

		//Copy constructor
		public YGO_Card(YGO_Card card) {
			name = card.name;
			cardType = card.cardType;
			attribute = card.attribute;
			monsterTypes = new List<int>();
			foreach (int type in card.monsterTypes) { monsterTypes.Add(type); }
			oracleText = card.oracleText;
			pendulumLeft = card.pendulumLeft;
			pendulumRight = card.pendulumRight;
			level = card.level;
			attack = card.attack;
			defense = card.defense;
			printingIDs = new List<int>();
			foreach (int print in card.printingIDs) { printingIDs.Add(print); }
		}

	}

	//Unique information for each printing of a card, as well as collection status
	[Serializable]
	public class YGO_Printing {

		//Properties
		public int set;
		public int cardNumber;
		public string flavorText;
		public string imgPath;
		public List<int> rarities;
		public int cardID;
		public Dictionary<int, Dictionary<int, int>> quantity;

		//Constructor
		public YGO_Printing() : this(0, 0, "", "", new List<int>(), 0) { }
		public YGO_Printing(int set, int cardNumber, string flavorText, string imgPath, List<int> rarities, int cardID) {
			this.set = set;
			this.cardNumber = cardNumber;
			this.flavorText = flavorText;
			this.imgPath = imgPath;
			this.rarities = new List<int>(rarities);
			this.cardID = cardID;
			this.quantity = new Dictionary<int, Dictionary<int, int>>();
			foreach (int rarity in rarities) { quantity.Add(rarity, new Dictionary<int, int>()); }
		}

		//Copy constructor
		public YGO_Printing(YGO_Printing print) {
			set = print.set;
			cardNumber = print.cardNumber;
			flavorText = print.flavorText;
			imgPath = print.imgPath;
			rarities = new List<int>();
			foreach (int rarity in print.rarities) { rarities.Add(rarity); }
			cardID = print.cardID;
			quantity = new Dictionary<int, Dictionary<int, int>>();
			foreach (int rarity in print.quantity.Keys) { 
				quantity.Add(rarity, new Dictionary<int, int>());
				foreach (int location in print.quantity[rarity].Keys) {
					quantity[rarity].Add(location, print.quantity[rarity][location]);
				}
			}
		}

		//Increment count at location
		public void Increment(int rarity) => Increment(rarity, 0);
		public void Increment(int rarity, int location) {
			if (quantity.ContainsKey(rarity)) {
				if (quantity[rarity].ContainsKey(location)) { ++quantity[rarity][location]; }
				else { quantity[rarity].Add(location, 1); }
			}
		}

		//Decrement count at location, return false if count was already zero
		public bool Decrement(int rarity) => Decrement(rarity, 0);
		public bool Decrement(int rarity, int location) {
			if (quantity.ContainsKey(rarity)) {
				if (quantity[rarity].ContainsKey(location)) {
					--quantity[rarity][location];
					if (quantity[rarity][location] <= 0) { quantity[rarity].Remove(location); }
					return true;
				}
			}
			return false;
		}

		//Move one copy of the specified rarity from one location to another
		public void MoveOne(int from, int to, int rarity) {
			if (Decrement(rarity, from)) {
				Increment(rarity, to);
			}
		}

		//Get total count owned at any rarity
		public int OwnedCount() {
			int count = 0;
			foreach (int rarity in quantity.Keys) {
				foreach (int location in quantity[rarity].Keys) {
					count += quantity[rarity][location];
				}
			}
			return count;
		}

		//Get count owned of a specified rarity
		public int OwnedCountOfRarity(int rarity) {
			if (quantity.ContainsKey(rarity)) {
				int count = 0;
				foreach (int location in quantity[rarity].Keys) {
					count += quantity[rarity][location];
				}
				return count;
			}
			return 0;
		}

		//Return true if any amount is owned at any rarity
		public bool AnyOwned() {
			foreach (int rarity in quantity.Keys) {
				foreach (int location in quantity[rarity].Keys) {
					if (quantity[rarity][location] > 0) { return true; };
				}
			}
			return false;
		}

	}

}
