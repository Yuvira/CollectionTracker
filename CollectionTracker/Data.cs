using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	#region Enums

	//Game type
	public enum Game {
		NONE,
		MTG,
		YGO,
		PKMN
	}

	#endregion

	#region Catalog

	[ProtoContract]
	public class Catalog {

		//Serialized properties
		[ProtoMember(1)] private List<Set> sets;
		[ProtoMember(2)] private List<Card> cards;
		[ProtoMember(3)] private List<Printing> printings;
		[ProtoMember(4)] private List<Symbol> symbols;
		[ProtoMember(5)] private Game game;

		//Accessors
		public List<Set> Sets => sets;
		public List<Card> Cards => cards;
		public List<Printing> Printings => printings;
		public List<Symbol> Symbols => symbols;
		public Game Game => game;

		//Constructor
		public Catalog() {
			sets = new List<Set>();
			cards = new List<Card>();
			printings = new List<Printing>();
			symbols = new List<Symbol>();
			game = Game.NONE;
		}

		#region Conversions

		//Conversion
		public Catalog(MTG_Catalog catalog) {
			sets = new List<Set>();
			cards = new List<Card>();
			printings = new List<Printing>();
			symbols = new List<Symbol>();
			for (int i = 0; i < catalog.sets.Count; ++i)
				sets.Add(new Set(catalog.sets[i]));
			for (int i = 0; i < catalog.cards.Count; ++i)
				cards.Add(new Card(catalog.cards[i]));
			for (int i = 0; i < catalog.printings.Count; ++i)
				printings.Add(new Printing(catalog.printings[i]));
			for (int i = 0; i < catalog.symbols.Count; ++i)
				symbols.Add(new Symbol(catalog.symbols[i]));
			foreach (Printing print in printings)
				print.LoadRefs(this);
			game = Game.MTG;
		}
		public Catalog(YGO_Catalog catalog) {
			sets = new List<Set>();
			cards = new List<Card>();
			printings = new List<Printing>();
			symbols = new List<Symbol>();
			for (int i = 0; i < catalog.sets.Count; ++i)
				sets.Add(new Set(catalog.sets[i]));
			for (int i = 0; i < catalog.cards.Count; ++i)
				cards.Add(new Card(catalog.cards[i]));
			for (int i = 0; i < catalog.printings.Count; ++i)
				printings.Add(new Printing(catalog.printings[i]));
			foreach (Printing print in printings)
				print.LoadRefs(this);
			game = Game.YGO;
		}
		public Catalog(PKMN_Catalog catalog) {
			sets = new List<Set>();
			cards = new List<Card>();
			printings = new List<Printing>();
			symbols = new List<Symbol>();
			for (int i = 0; i < catalog.sets.Count; ++i)
				sets.Add(new Set(catalog.sets[i]));
			for (int i = 0; i < catalog.cards.Count; ++i)
				cards.Add(new Card(catalog.cards[i]));
			for (int i = 0; i < catalog.printings.Count; ++i)
				printings.Add(new Printing(catalog.printings[i]));
			for (int i = 0; i < catalog.symbols.Count; ++i)
				symbols.Add(new Symbol(catalog.symbols[i]));
			foreach (Printing print in printings)
				print.LoadRefs(this);
			game = Game.PKMN;
		}

		#endregion

		#region I/O

		//Load
		public static Catalog LoadFromFile(string path) {
			try {
				Catalog catalog;
				using (Stream stream = File.Open(path, FileMode.OpenOrCreate))
					catalog = Serializer.Deserialize<Catalog>(stream);
				foreach (Printing print in catalog.printings)
					print.LoadRefs(catalog);
				return catalog;
			}
			catch (Exception ex) {
				MessageBox.Show($"Error: {ex.Message}");
				return null;
			}
		}

		//Save
		public void SaveToFile(string path) {
			foreach (Printing print in printings)
				print.SaveRefs(this);
			using (Stream stream = File.Open(path, FileMode.Create))
				Serializer.Serialize(stream, this);
		}

		#endregion

	}

	#endregion

	#region Sets

	[ProtoContract]
	public class Set {

		//Serialized properties
		[ProtoMember(1)] private string name;
		[ProtoMember(2)] private string code;
		[ProtoMember(3)] private string type;
		[ProtoMember(4)] private string date;
		[ProtoMember(5)] private string imgPath;
		[ProtoMember(6)] private int mainCount;
		[ProtoMember(7)] private string prefixOrder;

		//Accessors
		public string Name => name;
		public string Code => code;
		public string ImgPath => imgPath;
		public string Date => date;
		public DateTime DateTime => DateTime.ParseExact(date, "yyyy-MM-dd", null);
		public string PrefixOrder => prefixOrder;

		//Constructor
		public Set() : this("", "", "", DateTime.Now.ToString("yyyy-MM-dd"), "", 0, "") { }
		public Set(string name, string code, string type, string date, string imgPath, int mainCount, string prefixOrder) {
			this.name = name;
			this.code = code;
			this.type = type;
			this.date = date;
			this.imgPath = imgPath;
			this.mainCount = mainCount;
			this.prefixOrder = prefixOrder;
		}

		#region Conversions

		//Conversions
		public Set(MTG_Set set) {
			name = set.name;
			code = set.code;
			type = "";
			date = set.date.ToString("yyyy-MM-dd");
			imgPath = set.imgPath;
			mainCount = 0;
			prefixOrder = "";
		}
		public Set(YGO_Set set) {
			name = set.name;
			code = set.code;
			type = "";
			date = set.date.ToString("yyyy-MM-dd");
			imgPath = set.imgPath;
			mainCount = 0;
			prefixOrder = "";
		}
		public Set(PKMN_Set set) {
			name = set.name;
			code = set.code;
			type = set.setType;
			date = set.date.ToString("yyyy-MM-dd");
			imgPath = set.imgPath;
			mainCount = set.mainSetCount;
			prefixOrder = "";
		}

		#endregion

		#region Copy

		//Copy function
		public void Copy(Set set) {
			name = set.name;
			code = set.code;
			type = set.type;
			date = set.date;
			imgPath = set.imgPath;
			mainCount = set.mainCount;
			prefixOrder = set.prefixOrder;
		}

		#endregion

		#region Utils

		//ToString
		public override string ToString() => name;

		#endregion

	}

	//Comparer
	public class SetComparer : IComparer<Set> {
		public int Compare(Set set1, Set set2) => set1.Date.CompareTo(set2.Date);
	}

	#endregion

	#region Cards

	[ProtoContract]
	public class Card {

		//Serialized properties
		[ProtoMember(1)] private Dictionary<string, string> fields;
		[ProtoMember(2)] private List<Dictionary<string, string>> faces;
		[ProtoMember(3)] private bool favorite;

		//Accessors
		public Dictionary<string, string> Fields => fields;
		public List<Dictionary<string, string>> Faces => faces;
		public bool IsMultiface => faces.Count > 0;

		//Constructor
		public Card() {
			fields = new Dictionary<string, string>();
			faces = new List<Dictionary<string, string>>();
			favorite = false;
		}

		#region Conversions

		//Conversions
		private static string MTGColorToString(MTG_Colour color) {
			string s = "";
			if (color.HasFlag(MTG_Colour.White))
				s += "W";
			if (color.HasFlag(MTG_Colour.Blue))
				s += "U";
			if (color.HasFlag(MTG_Colour.Black))
				s += "B";
			if (color.HasFlag(MTG_Colour.Red))
				s += "R";
			if (color.HasFlag(MTG_Colour.Green))
				s += "G";
			return s;
		}
		public Card(MTG_Card card) {
			fields = new Dictionary<string, string>();
			faces = new List<Dictionary<string, string>>();
			if (!card.name.Contains(" // ")) {
				if (!string.IsNullOrEmpty(card.name))
					fields.Add("name", card.name);
				fields.Add("identity", MTGColorToString(card.identity));
				fields.Add("color", MTGColorToString(card.colour));
				if (!string.IsNullOrEmpty(card.cost))
					fields.Add("cost", card.cost);
				if (!string.IsNullOrEmpty(card.cardTypes)) {
					fields.Add("type", card.cardTypes);
					if (card.cardTypes.Contains("Creature") || card.cardTypes.Contains("Vehicle")) {
						fields.Add("power", card.power.ToString());
						fields.Add("toughness", card.toughness.ToString());
					}
					if (card.cardTypes.Contains("Planeswalker"))
						fields.Add("loyalty", card.toughness.ToString());
				}
				if (!string.IsNullOrEmpty(card.oracleText))
					fields.Add("oracle", card.oracleText);
			}
			else {
				fields.Add("identity", MTGColorToString(card.identity));
				Dictionary<string, string> front = new Dictionary<string, string>();
				Dictionary<string, string> back = new Dictionary<string, string>();
				string[] names = Utils.SplitString(card.name, " // ");
				if (names.Length > 0 && !string.IsNullOrEmpty(names[0]))
					front.Add("name", names[0]);
				if (names.Length > 1 && !string.IsNullOrEmpty(names[1]))
					back.Add("name", names[1]);
				front.Add("color", MTGColorToString(card.colour));
				if (!string.IsNullOrEmpty(card.cost)) {
					if (!card.cost.Contains(" // "))
						front.Add("cost", card.cost);
					else {
						string[] costs = Utils.SplitString(card.cost, " // ");
						if (costs.Length > 0 && !string.IsNullOrEmpty(costs[0]))
							front.Add("cost", costs[0]);
						if (costs.Length > 1 && !string.IsNullOrEmpty(costs[1]))
							back.Add("cost", costs[1]);
					}
				}
				if (!string.IsNullOrEmpty(card.cardTypes)) {
					if (!card.cardTypes.Contains(" // ")) {
						front.Add("type", card.cardTypes);
						if (card.cardTypes.Contains("Creature") || card.cardTypes.Contains("Vehicle")) {
							front.Add("power", card.power.ToString());
							front.Add("toughness", card.toughness.ToString());
						}
						if (card.cardTypes.Contains("Planeswalker"))
							front.Add("loyalty", card.toughness.ToString());
					}
					else {
						string[] types = Utils.SplitString(card.cardTypes, " // ");
						if (types.Length > 0 && !string.IsNullOrEmpty(types[0])) {
							front.Add("type", types[0]);
							if (types[0].Contains("Creature") || types[0].Contains("Vehicle")) {
								front.Add("power", card.power.ToString());
								front.Add("toughness", card.toughness.ToString());
							}
							if (types[0].Contains("Planeswalker"))
								front.Add("loyalty", card.toughness.ToString());
						}
						if (types.Length > 1 && !string.IsNullOrEmpty(types[1])) {
							back.Add("type", types[1]);
							if (types[1].Contains("Creature") || types[1].Contains("Vehicle")) {
								back.Add("power", card.power2.ToString());
								back.Add("toughness", card.toughness2.ToString());
							}
							if (types[1].Contains("Planeswalker"))
								back.Add("loyalty", card.toughness2.ToString());
						}
					}
				}
				if (!string.IsNullOrEmpty(card.oracleText)) {
					if (!card.oracleText.Contains("\r\n//\r\n")) {
						string o = card.oracleText;
						while (o.Contains("\r\n\r\n"))
							o = o.Replace("\r\n\r\n", "\r\n");
						front.Add("oracle", o);
					}
					else {
						string[] texts = Utils.SplitString(card.oracleText, "\r\n//\r\n");
						string o;
						if (texts.Length > 0 && !string.IsNullOrEmpty(texts[0])) {
							o = texts[0];
							while (o.Contains("\r\n\r\n"))
								o = o.Replace("\r\n\r\n", "\r\n");
							front.Add("oracle", o);
						}
						if (texts.Length > 1 && !string.IsNullOrEmpty(texts[1])) {
							o = texts[1];
							while (o.Contains("\r\n\r\n"))
								o = o.Replace("\r\n\r\n", "\r\n");
							back.Add("oracle", o);
						}
					}
				}
				faces.Add(front);
				faces.Add(back);
			}
			favorite = false;
		}
		public Card(YGO_Card card) {
			fields = new Dictionary<string, string>();
			faces = new List<Dictionary<string, string>>();
			if (!string.IsNullOrEmpty(card.name))
				fields.Add("name", card.name);
			if (!string.IsNullOrEmpty(card.cardType))
				fields.Add("cardtype", card.cardType);
			if (!string.IsNullOrEmpty(card.attribute))
				fields.Add("attribute", card.attribute);
			if (!string.IsNullOrEmpty(card.property))
				fields.Add("property", card.property);
			if (!string.IsNullOrEmpty(card.types)) {
				fields.Add("type", card.types);
				fields.Add("level", card.level.ToString());
				fields.Add("attack", card.atk.ToString());
				fields.Add("defense", card.def.ToString());
				if (card.types.Contains("Pendulum"))
					fields.Add("pendulum", card.pendulumScale.ToString());
			}
			if (!string.IsNullOrEmpty(card.oracleText)) {
				if (!card.oracleText.Contains("\r\n\r\n//\r\n\r\n"))
					fields.Add("oracle", card.oracleText);
				else {
					Dictionary<string, string> front = new Dictionary<string, string>();
					Dictionary<string, string> back = new Dictionary<string, string>();
					string[] texts = Utils.SplitString(card.oracleText, "\r\n\r\n//\r\n\r\n");
					if (texts.Length > 0 && !string.IsNullOrEmpty(texts[0]))
						front.Add("oracle", texts[0]);
					if (texts.Length > 1 && !string.IsNullOrEmpty(texts[1]))
						back.Add("oracle", texts[1]);
					faces.Add(front);
					faces.Add(back);
				}
			}
			favorite = card.favorite;
		}
		public Card(PKMN_Card card) {
			fields = new Dictionary<string, string>();
			faces = new List<Dictionary<string, string>>();
			if (!string.IsNullOrEmpty(card.name))
				fields.Add("name", card.name);
			if (!string.IsNullOrEmpty(card.energyType))
				fields.Add("energy", card.energyType);
			if (!string.IsNullOrEmpty(card.cardTypes))
				fields.Add("type", card.cardTypes);
			if (!string.IsNullOrEmpty(card.stage))
				fields.Add("stage", card.stage);
			if (card.hp > 0)
				fields.Add("hp", card.hp.ToString());
			if (!string.IsNullOrEmpty(card.oracleText))
				fields.Add("oracle", card.oracleText);
			if (!string.IsNullOrEmpty(card.weakness))
				fields.Add("weak", card.weakness);
			if (!string.IsNullOrEmpty(card.resistance))
				fields.Add("resist", card.resistance);
			if (!string.IsNullOrEmpty(card.retreatCost))
				fields.Add("retreat", card.retreatCost);
			favorite = false;
		}

		#endregion

		#region Copy

		//Copy function
		public void Copy(Card card) {
			fields = new Dictionary<string, string>(card.fields);
			faces = new List<Dictionary<string, string>>();
			foreach (Dictionary<string, string> face in card.faces)
				faces.Add(new Dictionary<string, string>(face));
			favorite = card.favorite;
		}
		public void CopyFields(Dictionary<string, string> fields, List<Dictionary<string, string>> faces) {
			this.fields = new Dictionary<string, string>(fields);
			this.faces = new List<Dictionary<string, string>>();
			foreach (Dictionary<string, string> face in faces)
				this.faces.Add(new Dictionary<string, string>(face));
		}

		#endregion

		#region Field Accessors

		//Get field
		public bool TryGetField(string field, out string value) {
			if (fields.ContainsKey(field)) {
				value = fields[field];
				return true;
			}
			value = "";
			foreach (Dictionary<string, string> face in faces) {
				if (face.ContainsKey(field)) {
					if (string.IsNullOrEmpty(value))
						value = face[field];
					else
						value += " // " + face[field];
				}
			}
			return !string.IsNullOrEmpty(value);
		}

		//Get field from specific face
		public bool TryGetField(string field, int face, out string value) {
			if (face >= 0 && face < faces.Count && faces[face].ContainsKey(field)) {
				value = faces[face][field];
				return true;
			}
			if (face == 0 && fields.ContainsKey(field)) {
				value = fields[field];
				return true;
			}
			value = "";
			return false;
		}


		#endregion

		#region Utils

		//ToString
		public override string ToString() => TryGetField("name", out string name) ? name : "MISSING NAME FIELD";

		#endregion

	}

	#endregion

	#region Printings

	[ProtoContract]
	public class Printing {

		//Serialized properties
		[ProtoMember(1)] private int setIndex;
		[ProtoMember(2)] private int cardIndex;
		[ProtoMember(3)] private List<Treatment> treatments;
		[ProtoMember(4)] private Dictionary<string, string> fields;
		[ProtoMember(4)] private List<string> imagePaths;

		//Private properties
		private Set set;
		private Card card;

		//Accessors
		public Set Set => set;
		public Card Card => card;
		public List<Treatment> Treatments => treatments;
		public Dictionary<string, string> Fields => fields;
		public string Date => TryGetField("date", out string value) ? value : Set.Date;
		public bool IsOwned => OwnedCount > 0;
		public int OwnedCount => treatments.Sum(t => t.OwnedCount);
		public List<string> ImagePaths => imagePaths;

		//Constructor
		public Printing() : this(null, null, new List<Treatment>()) { }
		public Printing(Set set, Card card, List<Treatment> treatments) {
			this.set = set;
			this.card = card;
			this.treatments = new List<Treatment>(treatments);
			fields = new Dictionary<string, string>();
			imagePaths = new List<string>();
		}

		#region Conversions

		//Conversions
		public Printing(MTG_Printing print) {
			setIndex = print.SetIndex;
			cardIndex = print.CardIndex;
			treatments = new List<Treatment>();
			foreach (MTG_Treatment treatment in print.treatments)
				treatments.Add(new Treatment(treatment));
			fields = new Dictionary<string, string>();
			fields.Add("cn", print.cardNumber.ToString());
			if (!string.IsNullOrEmpty(print.rarity))
				fields.Add("rarity", print.rarity);
			if (!string.IsNullOrEmpty(print.flavorText))
				fields.Add("flavor", print.flavorText);
			if (!string.IsNullOrEmpty(print.scryfallID))
				fields.Add("printid", print.scryfallID);
			imagePaths = new List<string>();
			if (!string.IsNullOrEmpty(print.imgPath))
				imagePaths.Add(print.imgPath);
			if (!string.IsNullOrEmpty(print.backImgPath))
				imagePaths.Add(print.backImgPath);
		}
		public Printing(YGO_Printing print) {
			setIndex = print.SetIndex;
			cardIndex = print.CardIndex;
			treatments = new List<Treatment>();
			foreach (YGO_Rarity treatment in print.rarities)
				treatments.Add(new Treatment(treatment));
			fields = new Dictionary<string, string>();
			fields.Add("cn", print.cardNumber.ToString());
			if (!string.IsNullOrEmpty(print.printID))
				fields.Add("printid", print.printID);
			imagePaths = new List<string>();
			if (!string.IsNullOrEmpty(print.imgPath))
				imagePaths.Add(print.imgPath);
			if (!string.IsNullOrEmpty(print.backImgPath))
				imagePaths.Add(print.backImgPath);
		}
		public Printing(PKMN_Printing print) {
			setIndex = print.SetIndex;
			cardIndex = print.CardIndex;
			treatments = new List<Treatment>();
			foreach (PKMN_Treatment treatment in print.treatments)
				treatments.Add(new Treatment(treatment));
			fields = new Dictionary<string, string>();
			fields.Add("cn", print.cardNumber.ToString());
			if (!string.IsNullOrEmpty(print.rarity))
				fields.Add("rarity", print.rarity);
			if (!string.IsNullOrEmpty(print.flavorText))
				fields.Add("flavor", print.flavorText);
			if (!string.IsNullOrEmpty(print.printID))
				fields.Add("printid", print.printID);
			imagePaths = new List<string>();
			if (!string.IsNullOrEmpty(print.imgPath))
				imagePaths.Add(print.imgPath);
			if (!string.IsNullOrEmpty(print.backImgPath))
				imagePaths.Add(print.backImgPath);
		}

		#endregion

		#region Copy

		//Copy function
		public void Copy(Printing print) {
			set = print.set;
			card = print.card;
			treatments = new List<Treatment>();
			foreach (Treatment treatment in print.treatments)
				treatments.Add(new Treatment(treatment));
			fields = new Dictionary<string, string>(print.fields);
			imagePaths = new List<string>(print.imagePaths);
		}
		public void CopyFields(Dictionary<string, string> fields) => this.fields = new Dictionary<string, string>(fields);

		#endregion

		#region Field Accessors

		//Get field
		public bool TryGetField(string field, out string value) {
			if (card.TryGetField(field, out value))
				return true;
			if (!fields.ContainsKey(field))
				return false;
			value = fields[field];
			return true;
		}

		#endregion

		#region Count Modifiers

		//Count modifiers
		public void Increment(string treatmentName) => Increment(treatmentName, "Desk");
		public void Increment(string treatmentName, string locationName) {
			Treatment treatment = treatments.FirstOrDefault(t => t.Name.Equals(treatmentName));
			if (treatment != null)
				treatment.Increment(locationName);
		}
		public bool Decrement(string treatmentName) {
			Treatment treatment = treatments.FirstOrDefault(t => t.Name.Equals(treatmentName));
			if (treatment != null && treatment.Locations.Count > 0)
				return treatment.Decrement(treatment.Locations.Count - 1);
			return false;
		}
		public bool Decrement(string treatmentName, string locationName) {
			Treatment treatment = treatments.FirstOrDefault(t => t.Name.Equals(treatmentName));
			if (treatment != null)
				return treatment.Decrement(locationName);
			return false;
		}
		public void MoveOne(string treatmentName, string fromLocation, string toLocation) {
			if (Decrement(treatmentName, fromLocation))
				Increment(treatmentName, toLocation);
		}

		#endregion

		#region I/O

		//Save and load reference objects
		public void SaveRefs(Catalog catalog) {
			setIndex = catalog.Sets.IndexOf(set);
			cardIndex = catalog.Cards.IndexOf(card);
		}
		public void LoadRefs(Catalog catalog) {
			set = catalog.Sets[setIndex];
			card = catalog.Cards[cardIndex];
		}

		#endregion

	}

	#endregion

	#region Treatments / Locations

	[ProtoContract]
	public class Treatment {

		//Serialized properties
		[ProtoMember(1)] private string name;
		[ProtoMember(2)] private List<Location> locations;

		//Accessors
		public string Name => name;
		public List<Location> Locations => locations;
		public bool IsOwned => OwnedCount > 0;
		public int OwnedCount => locations.Sum(l => l.Count);

		//Constructor
		public Treatment() : this("") { }
		public Treatment(string name) {
			this.name = name;
			locations = new List<Location>();
		}

		#region Conversions

		//Conversions
		public Treatment(MTG_Treatment treatment) {
			name = treatment.name;
			locations = new List<Location>();
			for (int i = 0; i < treatment.locations.Count; ++i)
				locations.Add(new Location(treatment.locations[i], treatment.quantities[i]));
		}
		public Treatment(YGO_Rarity treatment) {
			name = treatment.name;
			locations = new List<Location>();
			for (int i = 0; i < treatment.locations.Count; ++i)
				locations.Add(new Location(treatment.locations[i], treatment.quantities[i]));
		}
		public Treatment(PKMN_Treatment treatment) {
			name = treatment.name;
			locations = new List<Location>();
			for (int i = 0; i < treatment.locations.Count; ++i)
				locations.Add(new Location(treatment.locations[i], treatment.quantities[i]));
		}

		#endregion

		#region Copy

		//Copy constructor
		public Treatment(Treatment treatment) {
			name = treatment.name;
			locations = new List<Location>();
			foreach (Location location in treatment.locations)
				locations.Add(new Location(location));
		}

		#endregion

		#region Count Modifiers

		//Count modifiers
		public void Increment() => Increment("Desk");
		public void Increment(string locationName) {
			Location location = locations.FirstOrDefault(l => l.Name.Equals(locationName));
			if (location != null)
				location.Increment();
			else
				locations.Add(new Location(locationName, 1));
		}
		public bool Decrement() {
			if (locations.Count > 0)
				return Decrement(locations.Count - 1);
			return false;
		}
		public bool Decrement(string locationName) => Decrement(locations.FindIndex(l => l.Name.Equals(locationName)));
		public bool Decrement(int index) {
			if (index < 0 || index >= locations.Count)
				return false;
			locations[index].Decrement();
			if (locations[index].Count <= 0)
				locations.RemoveAt(index);
			return true;
		}

		#endregion

		#region Utils

		//Static default list generator
		public static List<Treatment> GenerateTreatments(List<string> treatments) {
			List<Treatment> list = new List<Treatment>();
			foreach (string treatment in treatments)
				list.Add(new Treatment(treatment));
			return list;
		}

		#endregion

	}

	[ProtoContract]
	public class Location {

		//Serialized properties
		[ProtoMember(1)] private string name;
		[ProtoMember(2)] private int count;

		//Accessors
		public string Name => name;
		public int Count => count;

		//Constructor
		public Location() : this("", 0) { }
		public Location(string name, int count) {
			this.name = name;
			this.count = count;
		}

		#region Copy

		//Copy constructor
		public Location(Location location) {
			name = location.name;
			count = location.count;
		}

		#endregion

		#region Count Modifiers

		//Count modifiers
		public void Increment() => ++count;
		public void Decrement() => --count;

		#endregion

	}

	#endregion

	#region Symbol Data

	[ProtoContract]
	public class Symbol {

		//Serialized properties
		[ProtoMember(1)] private string name;
		[ProtoMember(2)] private string text;
		[ProtoMember(3)] private string imgPath;
		[ProtoMember(4)] private decimal aspect;

		//Accessors
		public string Name => name;
		public string Text => text;
		public string ImgPath => imgPath;
		public decimal Aspect => aspect;

		//Constructor
		public Symbol() : this("", "", "", 1.00m) { }
		public Symbol(string name, string text, string imgPath, decimal aspect) {
			this.name = name;
			this.text = text;
			this.imgPath = imgPath;
			this.aspect = aspect;
		}

		#region Conversions

		//Conversions
		public Symbol(MTG_Symbol symbol) {
			name = symbol.name;
			text = symbol.symbol;
			imgPath = symbol.imgPath;
			aspect = symbol.aspect;
		}
		public Symbol(PKMN_Symbol symbol) {
			name = symbol.name;
			text = symbol.symbol;
			imgPath = symbol.imgPath;
			aspect = symbol.aspect;
		}

		#endregion

		#region Copy

		//Copy constructor
		public Symbol(Symbol symbol) {
			name = symbol.name;
			text = symbol.text;
			imgPath = symbol.imgPath;
			aspect = symbol.aspect;
		}

		#endregion

	}

	#endregion

}
