using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	//Global utilities
	public static class SearchUtils {

		#region Character Replacement Dictionaries

		//Character and string search replacements
		public static readonly Dictionary<char, char> searchChars = new Dictionary<char, char> {
			{ 'é', 'e' },
			{ 'É', 'E' },
			{ 'ᴀ', 'A' },
			{ 'ʙ', 'B' },
			{ 'ᴄ', 'C' },
			{ 'ᴅ', 'D' },
			{ 'ᴇ', 'E' },
			{ 'ꜰ', 'F' },
			{ 'ɢ', 'G' },
			{ 'ʜ', 'H' },
			{ 'ɪ', 'I' },
			{ 'ᴊ', 'J' },
			{ 'ᴋ', 'K' },
			{ 'ʟ', 'L' },
			{ 'ᴍ', 'M' },
			{ 'ɴ', 'N' },
			{ 'ᴏ', 'O' },
			{ 'ᴘ', 'P' },
			{ 'ǫ', 'Q' },
			{ 'ʀ', 'R' },
			{ 's', 'S' },
			{ 'ᴛ', 'T' },
			{ 'ᴜ', 'U' },
			{ 'ᴠ', 'V' },
			{ 'ᴡ', 'W' },
			{ 'x', 'X' },
			{ 'ʏ', 'Y' },
			{ 'ᴢ', 'Z' },
			{ '₀', '0' },
			{ '₁', '1' },
			{ '₂', '2' },
			{ '₃', '3' },
			{ '₄', '4' },
			{ '₅', '5' },
			{ '₆', '6' },
			{ '₇', '7' },
			{ '₈', '8' },
			{ '₉', '9' },
			{ '₋', '-' },
			{ '₍', '(' },
			{ '₎', ')' },
			{ '—', '-' },
			{ '×', 'x' },
			{ '“', '"' },
			{ '”', '"' },
			{ '‘', '\'' },
			{ '’', '\'' },
			{ ' ', ' ' },
		};
		public static readonly Dictionary<string, string> searchStrings = new Dictionary<string, string> {
			{ "𝑒", "e" },
			{ "𝑥", "x" },
			{ "𝘚", "S" },
			{ "𝘗", "P" },
			{ "𝘌", "E" },
			{ "𝘟", "X" },
			{ "𝘊", "C" },
			{ "𝘈", "A" },
			{ "𝘎", "G" },
		};

		#endregion

		#region Field Shortcut Dictionary

		//Field shortcuts
		public static readonly Dictionary<string, string> fieldShortcuts = new Dictionary<string, string> {
			{ "n"         , "name"      },
			{ "name"      , "name"      },
			{ "id"        , "identity"  },
			{ "identity"  , "identity"  },
			{ "c"         , "color"     },
			{ "color"     , "color"     },
			{ "cost"      , "cost"      },
			{ "ct"        , "cardtype"  },
			{ "cardtype"  , "cardtype"  },
			{ "att"       , "attribute" },
			{ "attribute" , "attribute" },
			{ "p"         , "property"  },
			{ "property"  , "property"  },
			{ "e"         , "energy"    },
			{ "energy"    , "energy"    },
			{ "pow"       , "power"     },
			{ "power"     , "power"     },
			{ "tough"     , "toughness" },
			{ "toughness" , "toughness" },
			{ "loy"       , "loyalty"   },
			{ "loyalty"   , "loyalty"   },
			{ "atk"       , "attack"    },
			{ "attack"    , "attack"    },
			{ "def"       , "defense"   },
			{ "defense"   , "defense"   },
			{ "hp"        , "hp"        },
			{ "lvl"       , "level"     },
			{ "level"     , "level"     },
			{ "stg"       , "stage"     },
			{ "stage"     , "stage"     },
			{ "pend"      , "pendulum"  },
			{ "pendulum"  , "pendulum"  },
			{ "t"         , "type"      },
			{ "type"      , "type"      },
			{ "o"         , "oracle"    },
			{ "oracle"    , "oracle"    },
			{ "weak"      , "weak"      },
			{ "resist"    , "resist"    },
			{ "retreat"   , "retreat"   },
			{ "a"         , "artist"    },
			{ "art"       , "artist"    },
			{ "artist"    , "artist"    },
		};

		#endregion

		//Return list of printings from catalog for search string
		public static List<Printing> SearchPrintings(string search) {

			//Default list
			List<Printing> printList = new List<Printing>();

			//Check search string exists
			if (string.IsNullOrWhiteSpace(search) || search.ToLower().Equals("search")) {
				printList.AddRange(TrackerForm.Catalog.Printings);
				return printList;
			}

			//Build search evaluator
			SearchNode root;
			try { root = BuildQuery(search); }
			catch (Exception e) {
				MessageBox.Show(e.Message);
				return printList;
			}

			//Evaluate printings
			foreach (Printing print in TrackerForm.Catalog.Printings)
				if (root.Evaluate(print))
					printList.Add(print);

			//Return
			return printList;

		}

		#region Query Node Chain

		//Query chain object
		private abstract class SearchNode {
			public abstract bool Evaluate(Printing print);
		}
		private sealed class AndNode : SearchNode {
			private SearchNode left, right;
			public AndNode(SearchNode left, SearchNode right) {
				this.left = left;
				this.right = right;
			}
			public override bool Evaluate(Printing print) => left.Evaluate(print) && right.Evaluate(print);
		}
		private sealed class OrNode : SearchNode {
			private SearchNode left, right;
			public OrNode(SearchNode left, SearchNode right) {
				this.left = left;
				this.right = right;
			}
			public override bool Evaluate(Printing print) => left.Evaluate(print) || right.Evaluate(print);
		}
		private sealed class TermNode : SearchNode {
			private readonly string field;
			private readonly string op;
			private readonly string value;
			public TermNode(string field, string op, string value) {
				this.field = field.ToLower();
				this.op = op;
				this.value = SearchableString(value);
			}
			public override bool Evaluate(Printing print) {
				if (!TryGetField(field, print, out string raw))
					return false;
				string left = SearchableString(raw);
				switch (op) {
					case ":": return left.Contains(value);
					case "!:": return !left.Contains(value);
					case "=": return left.Equals(value);
					case "!=": return !left.Equals(value);
					default: return false;
				}
			}
		}

		#endregion

		#region Query Construction

		//Build query
		private static int queryIdx = 0;
		private static string searchQuery = "";
		private static SearchNode BuildQuery(string text) {
			queryIdx = 0;
			searchQuery = text;
			SearchNode root = ParseOr();
			ParseWhitespace();
			if (queryIdx < searchQuery.Length)
				throw new Exception(searchQuery[queryIdx] == ')'
					? $"Expected token ')' not found! [{searchQuery}:{queryIdx}]"
					: $"Unexpected token {searchQuery[queryIdx]} found! [{searchQuery}:{queryIdx}]");
			return root;
		}

		//Parse operators
		private static SearchNode ParseOr() {
			SearchNode left = ParseAnd();
			while (TryConsume('|'))
				left = new OrNode(left, ParseAnd());
			return left;
		}
		private static SearchNode ParseAnd() {
			SearchNode left = ParseAtom();
			while (TryConsume('&'))
				left = new AndNode(left, ParseAtom());
			return left;
		}
		private static SearchNode ParseAtom() {
			ParseWhitespace();
			if (queryIdx >= searchQuery.Length)
				throw new Exception($"Expected search term! [{searchQuery}:{queryIdx}]");
			if (searchQuery[queryIdx] == '(') {
				int open = queryIdx++;
				SearchNode inner = ParseOr();
				if (!TryConsume(')'))
					throw new Exception($"Unmatched '('! [{searchQuery}:{open}]");
				return inner;
			}
			return ParseTerm();
		}

		//Parse search term
		private static SearchNode ParseTerm() {

			//Get field
			int start = queryIdx;
			while (queryIdx < searchQuery.Length && ":=!&|()\"".IndexOf(searchQuery[queryIdx]) < 0)
				++queryIdx;
			string field = searchQuery.Substring(start, queryIdx - start).Trim();
			if (field.Length == 0)
				throw new Exception($"Missing field name! [{searchQuery}:{start}]");

			//Get operator
			string op = "";
			if (TryConsume('!'))
				op += '!';
			if (TryConsume(':'))
				op += ':';
			else if (TryConsume('='))
				op += '=';
			if (op.Length == 0 || op.Equals("!"))
				throw new Exception($"Missing or invalid operator after '{field}'! [{searchQuery}:{queryIdx}]");

			//Read value and return term node
			return new TermNode(field, op, ReadValue());

		}

		//Read value
		private static string ReadValue() {

			//Skip whitespace and initialize value
			ParseWhitespace();
			string value = "";
			char c;

			//Read quoted value
			if (TryConsume('"')) {
				int open = queryIdx - 1;
				while (true) {
					if (queryIdx >= searchQuery.Length)
						throw new Exception($"Missing closing '\"'! [{searchQuery}:{open}]");
					c = searchQuery[queryIdx++];
					if (c == '"')
						break;
					if (c == '\\' && queryIdx < searchQuery.Length)
						c = searchQuery[queryIdx++];
					value += c;
				}
				ParseWhitespace();
				c = searchQuery[queryIdx];
				if (queryIdx < searchQuery.Length && c != '&' && c != '|' && c != ')')
					throw new Exception($"Unexpected text after quoted value! [{searchQuery}:{queryIdx}]");
				return value;
			}

			//Read unquoted value
			while (queryIdx < searchQuery.Length) {
				c = searchQuery[queryIdx];
				if (c == '\\' && queryIdx + 1 < searchQuery.Length) {
					value += searchQuery[queryIdx + 1];
					queryIdx += 2;
					continue;
				}
				if (c == '&' || c == '|' || c == '(' || c == ')')
					break;
				if (c == ':' || c == '=' || c == '!')
					throw new Exception($"Unexpected operator in value! [{searchQuery}:{queryIdx}]");
				value += c;
				++queryIdx;
			}

			//Return value
			return value.Trim();

		}

		//Skip whitespace characters
		private static void ParseWhitespace() {
			while (queryIdx < searchQuery.Length && char.IsWhiteSpace(searchQuery[queryIdx]))
				++queryIdx;
		}

		//Find character at current position in query
		private static bool TryConsume(char c) {
			ParseWhitespace();
			if (queryIdx < searchQuery.Length && searchQuery[queryIdx] == c) {
				++queryIdx;
				return true;
			}
			return false;
		}

		#endregion

		#region Utils

		//Convert input to searchable string by replacing non-standard characters and switching to lowercase
		public static string SearchableString(string input) {
			foreach (KeyValuePair<char, char> kvp in searchChars)
				input = input.Replace(kvp.Key, kvp.Value);
			foreach (KeyValuePair<string, string> kvp in searchStrings)
				input = input.Replace(kvp.Key, kvp.Value);
			return input.ToLower();
		}

		//Get the text a search field refers to on a printing. Returns false if no such field exists
		//Most of this logic can probably be merged into Printing.TryGetField()
		private static bool TryGetField(string field, Printing print, out string value) {
			if (fieldShortcuts.TryGetValue(field, out string shortcut)) {
				if (print.TryGetField(shortcut, out value)) {
					value = value.CleanFormatMarkers();
					return true;
				}
				return false;
			}
			else if (field.Equals("fo") || field.Equals("fulloracle")) {
				value = print.GetField("oracle");
				return true;
			}
			else if (field.Equals("s") || field.Equals("set")) {
				value = print.Set.Code;
				return true;
			}
			else if (field.Equals("l") || field.Equals("loc") || field.Equals("location")) {
				value = string.Join(" / ", print.Treatments.SelectMany(t => t.Locations.Select(l => l.Name)).Distinct());
				return true;
			}
			else if (field.Equals("f") || field.Equals("fav") || field.Equals("favorite")) {
				value = print.Card.Favorite.ToString();
				return true;
			}
			return print.TryGetField(field, out value);
		}

		#endregion

	}

}
