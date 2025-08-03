using System;
using System.Collections.Generic;
using System.Drawing;

namespace CollectionTracker {

	//Global utilities
	public static class Utils {

		//Font style references
		public static readonly Font FONT_DEFAULT = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
		public static readonly Font FONT_BOLD = new Font(FONT_DEFAULT, FontStyle.Bold);
		public static readonly Font FONT_ITALIC = new Font(FONT_DEFAULT, FontStyle.Italic);
		public static readonly Font FONT_UNDERLINE = new Font(FONT_DEFAULT, FontStyle.Underline);

		#region Search Tools

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
		};
		public static readonly Dictionary<string, string> searchStrings = new Dictionary<string, string> {
			{ "𝑒", "e" },
			{ "𝑥", "x" },
			{ "𝘚", "S" },
			{ "𝘗", "P" },
		};

		//Convert input to searchable string by replacing non-standard characters and switching to lowercase
		public static Dictionary<string, string> searchSymbols;
		public static string SearchableString(string input) {
			foreach (KeyValuePair<char, char> kvp in searchChars)
				input = input.Replace(kvp.Key, kvp.Value);
			foreach (KeyValuePair<string, string> kvp in searchStrings)
				input = input.Replace(kvp.Key, kvp.Value);
			if (searchSymbols != null)
				foreach (KeyValuePair<string, string> kvp in searchSymbols)
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
