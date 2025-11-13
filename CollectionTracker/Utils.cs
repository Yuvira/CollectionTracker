using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	//Global utilities
	public static class Utils {

		//Font style references
		public static readonly Font FONT_DEFAULT = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
		public static readonly Font FONT_BOLD = new Font(FONT_DEFAULT, FontStyle.Bold);
		public static readonly Font FONT_ITALIC = new Font(FONT_DEFAULT, FontStyle.Italic);
		public static readonly Font FONT_UNDERLINE = new Font(FONT_DEFAULT, FontStyle.Underline);

		//Custom colours
		public static readonly Color COLOR_DARK_ORANGE = BlendColours(new List<Color> { Color.Orange, Color.Black });

		#region Control Generators

		//Panel paint event managers
		private static List<(Panel, Color, int)> paintPanels = new List<(Panel, Color, int)>();
		public static void AddPanelPaintEvent(Panel panel, Color colour, int width, bool removeExisting = true) {
			if (removeExisting)
				RemovePanelPaintEvent(panel);
			paintPanels.Add((panel, colour, width));
		}
		public static void RemovePanelPaintEvent(Panel panel) => paintPanels.RemoveAll(pp => pp.Item1 == panel);

		//Panel generator
		public static Panel GeneratePanel(Point position, Size size) {
			Panel panel = new Panel();
			panel.Location = position;
			panel.Size = size;
			panel.Paint += PanelPaintDefault;
			return panel;
		}
		public static void PanelPaintDefault(object sender, PaintEventArgs e) {
			Panel panel = sender as Panel;
			if (!paintPanels.Select(pp => pp.Item1).Contains(panel)) {
				PanelPaint(e.Graphics, panel, 1, SystemColors.ControlLight, ButtonBorderStyle.Solid);
				return;
			}
			foreach ((Panel panel, Color colour, int width) pp in paintPanels)
				if (panel == pp.panel)
					PanelPaint(e.Graphics, panel, pp.width, pp.colour, ButtonBorderStyle.Solid);
		}
		public static void PanelPaint(Graphics graphics, Panel panel, int width, Color color, ButtonBorderStyle style) =>
			ControlPaint.DrawBorder(graphics, panel.DisplayRectangle, color, width, style, color, width, style, color, width, style, color, width, style);

		//Button generator
		public static Button GenerateButton(Point position, Size size, string text, string imgPath = "") {
			Button button = new Button();
			button.Location = position;
			button.Size = size;
			button.Text = text.Replace("&", "&&");
			button.TextAlign = ContentAlignment.MiddleCenter;
			if (imgPath.Length > 0) {
				button.Image = new Bitmap(Image.FromFile(imgPath), new Size(35, 35));
				button.TextImageRelation = TextImageRelation.ImageBeforeText;
				button.ImageAlign = ContentAlignment.MiddleRight;
			}
			button.UseVisualStyleBackColor = true;
			return button;
		}

		//Label Generator
		public static Label GenerateLabel(Point position, Size size, string text, Font font = null, Color? color = null) {
			Label label = new Label();
			label.Location = position;
			label.Size = size;
			label.Text = text.Replace("&", "&&");
			label.Font = font != null ? font : FONT_DEFAULT;
			label.ForeColor = color ?? SystemColors.ControlText;
			label.TextAlign = ContentAlignment.MiddleLeft;
			label.FlatStyle = FlatStyle.System;
			return label;
		}

		//Progress bar generator
		public static ProgressBar GenerateProgressBar(Point position, Size size, int value = 0) {
			ProgressBar bar = new ProgressBar();
			bar.Location = position;
			bar.Size = size;
			bar.Value = value;
			return bar;
		}

		//Picture box generator
		public static PictureBox GeneratePictureBox(Point position, Size size) {
			PictureBox box = new PictureBox();
			box.Location = position;
			box.Size = size;
			box.BorderStyle = BorderStyle.None;
			box.SizeMode = PictureBoxSizeMode.StretchImage;
			return box;
		}

		//Text box generator
		public static TextBox GenerateTextBox(Point position, Size size, string text) {
			TextBox box = new TextBox();
			box.Location = position;
			box.Size = size;
			box.Text = text;
			return box;
		}

		//Numeric up down generator
		public static NumericUpDown GenerateNumericUpDown(Point position, Size size, decimal value) {
			NumericUpDown nud = new NumericUpDown();
			nud.Location = position;
			nud.Size = size;
			nud.Minimum = decimal.MinValue;
			nud.Maximum = decimal.MaxValue;
			nud.Value = value;
			return nud;
		}

		//Checkbox generator
		public static CheckBox GenerateCheckbox(Point position, Size size, bool check, string text = "") {
			CheckBox box = new CheckBox();
			box.Location = position;
			box.Size = size;
			box.Checked = check;
			box.Text = text.Replace("&", "&&");
			box.TextAlign = ContentAlignment.MiddleLeft;
			return box;
		}

		//Date time picker
		public static DateTimePicker GenerateDateTimePicker(Point position, Size size, DateTime? value = null) {
			DateTimePicker dtp = new DateTimePicker();
			dtp.Location = position;
			dtp.Size = size;
			dtp.Value = value ?? DateTime.Now;
			return dtp;
		}

		#endregion

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

		#region Misc. Utilities

		//Blend list of colours
		public static Color BlendColours(List<Color> cols) {
			int A = 0, R = 0, G = 0, B = 0;
			foreach (Color c in cols) {
				A += c.A;
				R += c.R;
				G += c.G;
				B += c.B;
			}
			return Color.FromArgb(A / cols.Count, R / cols.Count, G / cols.Count, B / cols.Count);
		}

		#endregion

	}

}
