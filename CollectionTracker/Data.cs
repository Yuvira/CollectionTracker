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

	//Field context
	public enum FieldContext {
		NONE,
		CARD,
		PRINT
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
		[ProtoMember(6)] private Dictionary<string, string> keywords;
		[ProtoMember(7)] private string name;
		[ProtoMember(5)] private Game game;

		//Accessors
		public List<Set> Sets => sets;
		public List<Card> Cards => cards;
		public List<Printing> Printings => printings;
		public List<Symbol> Symbols => symbols;
		public Dictionary<string, string> Keywords => keywords;
		public string Name => name;
		public Game Game => game;

		//Constructor
		public Catalog() {
			sets = new List<Set>();
			cards = new List<Card>();
			printings = new List<Printing>();
			symbols = new List<Symbol>();
			keywords = new Dictionary<string, string>();
			name = "";
			game = Game.NONE;
		}

		#region Field Copy

		//Copy keywords
		public void CopyKeywords(Dictionary<string, string> keywords) =>
			this.keywords = new Dictionary<string, string>(keywords);

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
		[ProtoMember(8)] private int dateOrder;
		[ProtoMember(7)] private string prefixOrder;

		//Accessors
		public string Name => name;
		public string Code => code;
		public string Type => type;
		public string Date => date;
		public DateTime DateTime => DateTime.ParseExact(date, "yyyy-MM-dd", null);
		public string ImgPath => imgPath;
		public int MainCount => mainCount;
		public int DateOrder => dateOrder;
		public string PrefixOrder => prefixOrder;

		//Constructor
		public Set() : this("Name", "Code", "Type", DateTime.Now.ToString("yyyy-MM-dd"), "", 0, 0, "") { }
		public Set(string name, string code, string type, string date, string imgPath, int mainCount, int dateOrder, string prefixOrder) {
			this.name = name;
			this.code = code;
			this.type = type;
			this.date = date;
			this.imgPath = imgPath;
			this.mainCount = mainCount;
			this.dateOrder = dateOrder;
			this.prefixOrder = prefixOrder;
		}

		#region Copy

		//Copy function
		public void Copy(Set set) {
			name = set.name;
			code = set.code;
			type = set.type;
			date = set.date;
			imgPath = set.imgPath;
			mainCount = set.mainCount;
			dateOrder = set.dateOrder;
			prefixOrder = set.prefixOrder;
		}

		#endregion

		#region Utils

		//Comparers
		public static int SortNewest(Set set1, Set set2) {
			int compare = set1.Date.CompareTo(set2.Date);
			if (compare != 0)
				return -compare;
			compare = set1.dateOrder.CompareTo(set2.dateOrder);
			if (compare != 0)
				return -compare;
			return set1.Name.CompareTo(set2.Name);
		}

		//ToString
		public override string ToString() => name;

		#endregion

	}

	#endregion

	#region Faces

	[ProtoContract]
	public class Face {

		//Serialized properties
		[ProtoMember(1)] private Dictionary<string, string> fields;

		//Accessors
		public Dictionary<string, string> Fields => fields;

		//Contructors
		public Face() => fields = new Dictionary<string, string>();
		public Face(Face face) => fields = new Dictionary<string, string>(face.fields);
		public Face(Dictionary<string, string> fields) => this.fields = new Dictionary<string, string>(fields);

		//Copy function
		public void CopyFields(Dictionary<string, string> fields) => this.fields = new Dictionary<string, string>(fields);

		//Field accessor
		public bool TryGetField(string field, out string value) {
			if (fields.ContainsKey(field)) {
				value = fields[field];
				return true;
			}
			value = "";
			return false;
		}

	}

	#endregion

	#region Cards

	[ProtoContract]
	public class Card {

		//Serialized properties
		[ProtoMember(1)] private Dictionary<string, string> fields;
		[ProtoMember(2)] private List<Face> faces;
		[ProtoMember(3)] private bool favorite;

		//Accessors
		public Dictionary<string, string> Fields => fields;
		public List<Face> Faces => faces;
		public bool IsMultiface => faces.Count > 0;
		public bool Favorite => favorite;

		//Constructor
		public Card() {
			fields = new Dictionary<string, string>();
			faces = new List<Face>();
			favorite = false;
		}

		#region Copy

		//Copy function
		public void Copy(Card card) {
			fields = new Dictionary<string, string>(card.fields);
			faces = new List<Face>();
			foreach (Face face in card.faces)
				faces.Add(new Face(face));
			favorite = card.favorite;
		}
		public void CopyFields(Dictionary<string, string> fields, List<Dictionary<string, string>> faces) {
			this.fields = new Dictionary<string, string>(fields);
			this.faces = new List<Face>();
			foreach (Dictionary<string, string> face in faces)
				this.faces.Add(new Face(face));
		}

		#endregion

		#region Field Accessors

		//Get field from base card
		public bool TryGetBaseField(string field, out string value) {
			if (fields.ContainsKey(field)) {
				value = fields[field];
				return true;
			}
			value = "";
			return false;
		}

		//Get field from specific face
		public bool TryGetFaceField(string field, int face, out string value) {
			if (face < 0)
				return TryGetBaseField(field, out value);
			if (face >= 0 && face < faces.Count && faces[face].TryGetField(field, out value))
				return true;
			value = "";
			return false;
		}

		//Get field from specific face, but treat base fields as face 0
		public bool TryGetBaseOrFaceField(string field, int face, out string value) {
			if (face == 0 && TryGetBaseField(field, out value))
				return true;
			if (face >= 0 && face < faces.Count && faces[face].TryGetField(field, out value))
				return true;
			value = "";
			return false;
		}

		//Get first instance of field
		public bool TryGetFirstField(string field, out string value) {
			for (int i = -1; i < faces.Count; ++i)
				if (TryGetFaceField(field, i, out value))
					return true;
			value = "";
			return false;
		}

		//Get field from all faces
		public bool TryGetField(string field, out string value) {
			value = null;
			for (int i = -1; i < faces.Count; ++i) {
				if (TryGetFaceField(field, i, out string faceValue)) {
					if (string.IsNullOrEmpty(value))
						value = faceValue;
					else
						value += " // " + faceValue;
				}
			}
			if (value == null) {
				value = "";
				return false;
			}
			return true;
		}

		//Has field
		public bool HasField(string field) {
			if (fields.ContainsKey(field))
				return true;
			foreach (Face face in faces)
				if (face.Fields.ContainsKey(field))
					return true;
			return false;
		}

		#endregion

		#region Utils

		//Toggle favorite status
		public void ToggleFavorite() => favorite = !favorite;

		//ToString
		public override string ToString() {

			//Get name
			if (TryGetField("name", out string name)) {

				//Clean formatting
				name = name.CleanFormatMarkers();

				//Add clarifiers to card types that need them
				if (TryGetField("type", out string type)) {
					if (type.Contains("Token")) {
						name += " (Token";
						if (TryGetField("power", out string power))
							name += " | " + power;
						if (TryGetField("toughness", out string toughness))
							name += " / " + toughness;
						if (TryGetField("color", out string color))
							name += " | " + color;
						if (TryGetField("oracle", out string oracle))
							name += " | " + oracle.Replace("\r\n", " / ").CleanFormatMarkers();
						return name + ")";
					}
					if (type.Contains("Pokémon") && !type.Contains("Pokémon Tool")) {
						name += " | ";
						if (TryGetField("energy", out string energy))
							name += energy + " ";
						if (TryGetField("hp", out string hp))
							name += hp + " ";
						name += "| ";
						if (TryGetField("oracle", out string oracle))
							name += oracle.Replace("\r\n", " / ").CleanFormatMarkers();
						return name;
					}
					if (type.Contains("Art Card"))
						return name + " (Art Card)";
					if (type.Contains("Front Card"))
						return name + " (Front Card)";
					if (type.Contains("Emblem"))
						return name + " (Emblem)";
					if (type.Contains("Minigame"))
						return name + " (Minigame)";
				}

				//Return text
				return name;

			}

			//No name field
			return "MISSING NAME FIELD";

		}

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
		[ProtoMember(6)] private List<Face> faces;
		[ProtoMember(5)] private List<string> imagePaths;

		//Private properties
		private Set set;
		private Card card;

		//Accessors
		public Set Set => set;
		public Card Card => card;
		public List<Treatment> Treatments => treatments;
		public Dictionary<string, string> Fields => fields;
		public List<Face> Faces => faces;
		public string Date => TryGetField("date", out string value) ? value : Set.Date;
		public bool IsOwned => OwnedCount > 0;
		public int OwnedCount => treatments.Sum(t => t.OwnedCount);
		public List<string> ImagePaths => imagePaths;

		//Constructor
		public Printing() : this(null, null) { }
		public Printing(Set set, Card card) {
			this.set = set;
			this.card = card;
			treatments = new List<Treatment>();
			fields = new Dictionary<string, string>();
			faces = new List<Face>();
			imagePaths = new List<string>();
		}

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
		public void CopySet(Set set) => this.set = set;
		public void CopyCard(Card card) => this.card = card;
		public void CopyTreatments(List<string> names) {
			List<Treatment> treatments = new List<Treatment>();
			foreach (string name in names) {
				Treatment treatment = this.treatments.FirstOrDefault(t => t.Name.Equals(name));
				if (treatment != null)
					treatments.Add(treatment);
				else
					treatments.Add(new Treatment(name));
			}
			this.treatments = new List<Treatment>(treatments);
		}
		public void CopyFields(Dictionary<string, string> fields, List<Dictionary<string, string>> faces) {
			this.fields = new Dictionary<string, string>(fields);
			this.faces = new List<Face>();
			foreach (Dictionary<string, string> face in faces)
				this.faces.Add(new Face(face));
		}
		public void CopyImgPaths(List<string> paths) => this.imagePaths = new List<string>(paths);

		#endregion

		#region Field Accessors

		//Get field from base printing
		public bool TryGetBaseField(string field, out string value) {
			if (fields.ContainsKey(field)) {
				value = fields[field];
				return true;
			}
			value = "";
			return false;
		}

		//Get field from specific face
		public bool TryGetFaceField(string field, int face, out string value) {
			if (face < 0)
				return TryGetBaseField(field, out value);
			if (face >= 0 && face < faces.Count && faces[face].TryGetField(field, out value))
				return true;
			value = "";
			return false;
		}

		//Get field
		public bool TryGetField(string field, out string value) {
			if (card.TryGetField(field, out value))
				return true;
			value = null;
			for (int i = -1; i < faces.Count; ++i) {
				if (TryGetFaceField(field, i, out string faceValue)) {
					if (string.IsNullOrEmpty(value))
						value = faceValue;
					else
						value += " // " + faceValue;
				}
			}
			if (value == null) {
				value = "";
				return false;
			}
			return true;
		}
		public string GetField(string field) {
			if (card.TryGetField(field, out string value))
				return value;
			for (int i = -1; i < faces.Count; ++i) {
				if (TryGetFaceField(field, i, out string faceValue)) {
					if (string.IsNullOrEmpty(value))
						value = faceValue;
					else
						value += " // " + faceValue;
				}
			}
			if (value == null)
				return "";
			return value;
		}

		//Has field
		public bool HasField(string field) {
			if (fields.ContainsKey(field))
				return true;
			foreach (Face face in faces)
				if (face.Fields.ContainsKey(field))
					return true;
			return false;
		}

		//Get image
		public string GetImagePath(int idx) {
			if (idx < 0 || idx >= imagePaths.Count)
				return "";
			return imagePaths[idx];
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
		public bool MoveOne(string treatmentName, string fromLocation, string toLocation) {
			Treatment treatment = treatments.FirstOrDefault(t => t.Name.Equals(treatmentName));
			if (treatment != null)
				return treatment.MoveOne(fromLocation, toLocation);
			return false;
		}

		#endregion

		#region Utils

		//Comparers
		public static int SortNewest(Printing print1, Printing print2) {
			int compare = SortDateNewest(print1, print2);
			if (compare != 0)
				return compare;
			return print1.GetField("cn").CompareTo(print2.GetField("cn"));
		}
		public static int SortInverseNewest(Printing print1, Printing print2) {
			int compare = SortDateNewest(print1, print2);
			if (compare != 0)
				return compare;
			return -print1.GetField("cn").CompareTo(print2.GetField("cn"));
		}
		public static int SortOldest(Printing print1, Printing print2) {
			int compare = SortDateNewest(print1, print2);
			if (compare != 0)
				return -compare;
			return print1.GetField("cn").CompareTo(print2.GetField("cn"));
		}
		public static int SortAlphabetical(Printing print1, Printing print2) =>
			print1.GetField("name").CompareTo(print2.GetField("name"));

		//Comparison Utils
		private static int SortDateNewest(Printing print1, Printing print2) {
			int compare = print1.Date.CompareTo(print2.Date);
			if (compare != 0)
				return -compare;
			if (print1.HasField("date") && !print2.HasField("date"))
				return -1;
			else if (print2.HasField("date") && !print1.HasField("date"))
				return 1;
			return Set.SortNewest(print1.set, print2.set);
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
		public bool MoveOne(string from, string to) {
			if (Decrement(from)) {
				Increment(to);
				return true;
			}
			return false;
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
