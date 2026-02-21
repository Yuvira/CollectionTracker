using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionTracker {

	#region Catalog

	[ProtoContract]
	public class Catalog {

		//Serialized properties
		[ProtoMember(1)] public List<Set> sets;
		[ProtoMember(2)] public List<Card> cards;
		[ProtoMember(3)] public List<Printing> printings;
		[ProtoMember(4)] public List<Symbol> symbols;

		//Constructor
		public Catalog() {
			sets = new List<Set>();
			cards = new List<Card>();
			printings = new List<Printing>();
			symbols = new List<Symbol>();
		}

		//Save and load print references
		public void SavePrintRefs() {
			foreach (Printing print in printings)
				print.SaveRefs(this);
		}
		public void LoadPrintRefs() {
			foreach (Printing print in printings)
				print.LoadRefs(this);
		}

	}

	#endregion

	#region Sets

	[ProtoContract]
	public class Set {

		//Serialized properties
		[ProtoMember(1)] private string name;
		[ProtoMember(2)] private string code;
		[ProtoMember(3)] private string imgPath;
		[ProtoMember(4)] private string date;
		[ProtoMember(5)] private string prefixOrder;

		//Accessors
		public string Name => name;
		public string Code => code;
		public string ImgPath => imgPath;
		public string Date => date;
		public DateTime DateTime => DateTime.ParseExact(date, "yyyy-MM-dd", null);
		public string PrefixOrder => prefixOrder;

		//Constructor
		public Set() : this("", "", "", DateTime.Now.ToString("yyyy-MM-dd"), "") { }
		public Set(string name, string code, string imgPath, string date, string prefixOrder) {
			this.name = name;
			this.code = code;
			this.imgPath = imgPath;
			this.date = date;
			this.prefixOrder = prefixOrder;
		}

		//Copy function
		public void Copy(Set set) {
			name = set.name;
			code = set.code;
			imgPath = set.imgPath;
			date = set.date;
			prefixOrder = set.prefixOrder;
		}

		//ToString
		public override string ToString() => name;

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

		//Constructor
		public Card() {
			fields = new Dictionary<string, string>();
			faces = new List<Dictionary<string, string>>();
		}

		//Copy function
		public void Copy(Card card) {
			fields = new Dictionary<string, string>(card.fields);
			faces = new List<Dictionary<string, string>>();
			foreach (Dictionary<string, string> face in card.faces)
				faces.Add(new Dictionary<string, string>(face));
		}

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

		//Private properties
		private Set set;
		private Card card;

		//Accessors
		public Set Set => set;
		public Card Card => card;
		public List<Treatment> Treatments => treatments;
		public bool IsOwned => OwnedCount > 0;
		public int OwnedCount => treatments.Sum(t => t.OwnedCount);

		//Constructor
		public Printing() : this(null, null, new List<Treatment>()) { }
		public Printing(Set set, Card card, List<Treatment> treatments) {
			this.set = set;
			this.card = card;
			this.treatments = new List<Treatment>(treatments);
			fields = new Dictionary<string, string>();
		}

		//Copy function
		public void Copy(Printing print) {
			set = print.set;
			card = print.card;
			treatments = new List<Treatment>();
			foreach (Treatment treatment in print.treatments)
				treatments.Add(new Treatment(treatment));
			fields = new Dictionary<string, string>(print.fields);
		}

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

		//Save and load reference objects
		public void SaveRefs(Catalog catalog) {
			setIndex = catalog.sets.IndexOf(set);
			cardIndex = catalog.cards.IndexOf(card);
		}
		public void LoadRefs(Catalog catalog) {
			set = catalog.sets[setIndex];
			card = catalog.cards[cardIndex];
		}


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

		//Copy constructor
		public Treatment(Treatment treatment) {
			name = treatment.name;
			locations = new List<Location>();
			foreach (Location location in treatment.locations)
				locations.Add(new Location(location));
		}

		//Count modifiers
		public void Increment(string locationName) {
			Location location = locations.FirstOrDefault(l => l.Name.Equals(locationName));
			if (location != null)
				location.Increment();
			else
				locations.Add(new Location(locationName, 1));
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

		//Static default list generator
		public static List<Treatment> GenerateTreatments(List<string> treatments) {
			List<Treatment> list = new List<Treatment>();
			foreach (string treatment in treatments)
				list.Add(new Treatment(treatment));
			return list;
		}

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

		//Copy constructor
		public Location(Location location) {
			name = location.name;
			count = location.count;
		}

		//Count modifiers
		public void Increment() => ++count;
		public void Decrement() => --count;

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

		//Copy constructor
		public Symbol(Symbol symbol) {
			name = symbol.name;
			text = symbol.text;
			imgPath = symbol.imgPath;
			aspect = symbol.aspect;
		}

	}

	#endregion

}
