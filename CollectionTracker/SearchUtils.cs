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
			{ "a"         , "attribute" },
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
		};

		#endregion

		//Search operation class
		private struct SearchExpression {
			private string field;
			private string operation;
			private string value;
			public SearchExpression(string field, string operation, string value) {
				this.field = field.ToLower();
				this.operation = operation;
				this.value = value.ToLower();
			}
			public string Field => field;
			public string Operation => operation;
			public string Value => value;
		}

		//Return list of printings from catalog for search string
		public static List<Printing> SearchPrintings(string search) {

			//Default list
			List<Printing> printList = new List<Printing>();

			//Check search string exists
			if (string.IsNullOrWhiteSpace(search) || search.ToLower().Equals("search")) {
				foreach (Printing print in TrackerForm.Catalog.Printings)
					printList.Add(print);
				return printList;
			}

			//Check all parentheses closed
			if (search.Count(c => c == '(') != search.Count(c => c == ')')) {
				MessageBox.Show($"Mismatched parentheses count! [{search.Count(c => c == '(')}:{search.Count(c => c == ')')}]");
				return printList;
			}

			//Split expressions
			List<SearchExpression> expressions = new List<SearchExpression>();
			string[] expressionStrings = search.Split(new char[] { '(', ')', '&', '|' }, StringSplitOptions.RemoveEmptyEntries);
			if (expressionStrings.Length > 10) {
				MessageBox.Show("Can't evaluate more than 10 expressions!");
				return printList;
			}
			foreach (string expressionString in expressionStrings) {
				string[] terms = expressionString.Split(new string[] { "!:", "!=", "~:", "~=", "==", ":", "=" }, StringSplitOptions.None);
				if (terms.Length != 2) {
					MessageBox.Show($"Invalid term parsed! [{expressionString}]");
					return printList;
				}
				expressions.Add(new SearchExpression(terms[0], expressionString.Substring(terms[0].Length, expressionString.Length - (terms[0].Length + terms[1].Length)), terms[1]));
				search = search.Replace(expressionString, (expressions.Count - 1).ToString());
			}

			//Search printings
			string evaluate;
			foreach (Printing print in TrackerForm.Catalog.Printings) {
				evaluate = string.Copy(search);
				for (int i = 0; i < expressions.Count; ++i)
					evaluate = evaluate.Replace(i.ToString(), EvaluateExpression(expressions[i], print) ? "t" : "f");
				try {
					if (EvaluateBoolean(evaluate))
						printList.Add(print);
				}
				catch (Exception e) {
					MessageBox.Show($"{e.Message} [{evalString}:{index}]");
					return printList;
				}
			}

			//Return
			return printList;

		}

		//Evaluate search expression on printing
		private static bool EvaluateExpression(SearchExpression expression, Printing print) {
			if (fieldShortcuts.ContainsKey(expression.Field) && print.TryGetField(fieldShortcuts[expression.Field], out string field))
				return EvaluateExpression(field, expression.Operation, expression.Value);
			else if (expression.Field.Equals("s") || expression.Field.Equals("set"))
				return EvaluateExpression(print.Set.Code, expression.Operation, expression.Value);
			else if (expression.Field.Equals("l") || expression.Field.Equals("loc") || expression.Field.Equals("location"))
				return EvaluateExpression(string.Join(" / ", print.Treatments.SelectMany(t => t.Locations.Select(l => l.Name)).Distinct()), expression.Operation, expression.Value);
			else
				return false;
		}

		//Evaluate strings with operation
		private static bool EvaluateExpression(string field, string op, string value) {
			if (op.Equals(":"))
				return SearchableString(field).Contains(SearchableString(value));
			if (op.Equals("!:"))
				return !SearchableString(field).Contains(SearchableString(value));
			if (op.Equals("="))
				return SearchableString(field).Equals(SearchableString(value));
			if (op.Equals("!="))
				return !SearchableString(field).Equals(SearchableString(value));
			return false;
		}

		//Evaluate
		private static int index = 0;
		private static string evalString = "";
		private static bool EvaluateBoolean(string str) {
			index = 0;
			evalString = str;
			return ParseOperand();
		}
		private static bool ParseOperand() {
			bool left = ParseValue();
			if (index >= evalString.Length)
				return left;
			if (evalString[index] == '&') {
				++index;
				return left && ParseValue();
			}
			if (evalString[index] == '|') {
				++index;
				return left || ParseValue();
			}
			return left;
		}
		private static bool ParseValue() {
			if (evalString[index] == '(') {
				++index;
				bool result = ParseOperand();
				if (evalString[index] != ')')
					throw new Exception($"Expected token ')' not found! [{evalString}:{index}]");
				++index;
				return result;
			}
			if (evalString[index] == 't') {
				++index;
				return true;
			}
			if (evalString[index] == 'f') {
				++index;
				return false;
			}
			throw new Exception($"Unexpected token {evalString[index]} found! [{evalString}:{index}]");
		}

		#region Legacy Code

		//Convert input to searchable string by replacing non-standard characters and switching to lowercase
		public static Dictionary<string, string> searchSymbols;
		public static string SearchableString(string input) {
			if (searchSymbols != null)
				foreach (KeyValuePair<string, string> kvp in searchSymbols)
					input = input.Replace(kvp.Key, kvp.Value);
			foreach (KeyValuePair<char, char> kvp in searchChars)
				input = input.Replace(kvp.Key, kvp.Value);
			foreach (KeyValuePair<string, string> kvp in searchStrings)
				input = input.Replace(kvp.Key, kvp.Value);
			return input.ToLower();
		}

		//Evaluate if a search operation is true or not
		public static bool EvaluateSearchOperation(string field, string op, string value, Dictionary<string, string> symbols, bool fieldIsList = false) {
			searchSymbols = symbols;
			if (op.Equals("!:")) {
				if (!SearchableString(field).Contains(SearchableString(value)))
					return true;
				return false;
			}
			else if (op.Equals("~:")) {
				if (field.Contains(value))
					return true;
				return false;
			}
			else if (op.Equals(":")) {
				if (SearchableString(field).Contains(SearchableString(value)))
					return true;
				return false;
			}
			else if (op.Equals("==")) {
				if (SearchableString(field).Equals(SearchableString(value)))
					return true;
			}
			else if (op.Equals("!=")) {
				if (!fieldIsList) {
					if (!SearchableString(field).Equals(SearchableString(value)))
						return true;
					return false;
				}
				foreach (string subfield in field.Split(new string[] { " / " }, StringSplitOptions.None))
					if (SearchableString(subfield).Equals(SearchableString(value)))
						return false;
				return true;
			}
			else if (op.Equals("~=")) {
				if (!fieldIsList) {
					if (field.Equals(value))
						return true;
					return false;
				}
				foreach (string subfield in field.Split(new string[] { " / " }, StringSplitOptions.None))
					if (subfield.Equals(value))
						return true;
				return false;
			}
			else if (op.Equals("=")) {
				if (!fieldIsList) {
					if (SearchableString(field).Equals(SearchableString(value)))
						return true;
					return false;
				}
				foreach (string subfield in field.Split(new string[] { " / " }, StringSplitOptions.None))
					if (SearchableString(subfield).Equals(SearchableString(value)))
						return true;
				return false;
			}
			return false;
		}

		//Check if list delimited by string contains another exact string
		public static bool ListContainsExact(string input, string delimiter, string search) {
			string[] terms = input.Split(new string[] { delimiter }, StringSplitOptions.None);
			foreach (string term in terms)
				if (term.Equals(search))
					return true;
			return false;
		}

		#endregion

	}

}
