using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	#region Custom Controls

	//Custom panel class
	public class TrackerPanel : Panel {
		private List<Pen> pens;
		public TrackerPanel() : base() {
			pens = new List<Pen>();
			SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
		}
		public void AddBorder(Color color, int width) => pens.Add(new Pen(color, width));
		public void ClearBorders() => pens.Clear();
		protected override void OnPaint(PaintEventArgs e) {
			if (pens.Count > 0) {
				e.Graphics.FillRectangle(Utils.BRUSH_BACK, ClientRectangle);
				foreach (Pen pen in pens)
					e.Graphics.DrawRectangle(pen, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
			}
			else
				base.OnPaint(e);
		}
	}

	#endregion

	//Global utilities
	public static class Utils {

		//Formatting constants
		public const int TEXT_HEIGHT = 21;
		public const int TEXT_MARGIN = 8;
		public const int LINE_SPACING = 9;
		public const int LEFT_PAD = 8;
		public const int TOP_PAD = 8;
		public const int BOTTOM_PAD = 8;

		//Font style references
		public static readonly Font FONT_DEFAULT = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
		public static readonly Font FONT_BOLD = new Font(FONT_DEFAULT, FontStyle.Bold);
		public static readonly Font FONT_ITALIC = new Font(FONT_DEFAULT, FontStyle.Italic);
		public static readonly Font FONT_UNDERLINE = new Font(FONT_DEFAULT, FontStyle.Underline);

		//Brush references
		public static readonly SolidBrush BRUSH_BACK = new SolidBrush(COLOR_BACK);

		//Color references
		public static readonly Color COLOR_BACK = SystemColors.ControlDark;
		public static readonly Color COLOR_FRONT = Color.Black;
		public static readonly Color COLOR_BUTTON = SystemColors.ControlLight;
		public static readonly Color COLOR_DARK_ORANGE = BlendColours(new List<Color> { Color.Orange, Color.Black });

		#region Control Generators

		//Panel generator
		public static Panel GeneratePanel(Rectangle rect) {
			Panel panel = new Panel();
			panel.Location = rect.Location;
			panel.Size = rect.Size;
			panel.BorderStyle = BorderStyle.FixedSingle;
			return panel;
		}

		//Custom panel generator
		public static TrackerPanel GenerateTrackerPanel(Rectangle rect, bool useDefaultBorder = false) {
			TrackerPanel panel = new TrackerPanel();
			panel.Location = rect.Location;
			panel.Size = rect.Size;
			if (useDefaultBorder)
				panel.AddBorder(COLOR_FRONT, 1);
			return panel;
		}

		//Button generator
		public static Button GenerateButton(Rectangle rect, string text, string imgPath = "") {
			Button button = new Button();
			button.Location = rect.Location;
			button.Size = rect.Size;
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

		//Button generator
		public static RadioButton GenerateRadioButton(Rectangle rect, string text) {
			RadioButton button = new RadioButton();
			button.Location = rect.Location;
			button.Size = rect.Size;
			button.Text = text.Replace("&", "&&");
			button.TextAlign = ContentAlignment.MiddleCenter;
			button.Appearance = Appearance.Button;
			button.FlatStyle = FlatStyle.Popup;
			button.BackColor = COLOR_BUTTON;
			return button;
		}

		//Label Generator
		public static Label GenerateLabel(Rectangle rect, string text, Font font = null, Color? color = null) {
			Label label = new Label();
			label.Location = rect.Location;
			label.Size = rect.Size;
			label.Text = text.Replace("&", "&&");
			label.Font = font != null ? font : FONT_DEFAULT;
			label.ForeColor = color ?? SystemColors.ControlText;
			label.TextAlign = ContentAlignment.MiddleLeft;
			label.FlatStyle = FlatStyle.System;
			return label;
		}

		//Progress bar generator
		public static ProgressBar GenerateProgressBar(Rectangle rect, int value = 0) {
			ProgressBar bar = new ProgressBar();
			bar.Location = rect.Location;
			bar.Size = rect.Size;
			bar.Value = value;
			return bar;
		}

		//Picture box generator
		public static PictureBox GeneratePictureBox(Rectangle rect) {
			PictureBox box = new PictureBox();
			box.Location = rect.Location;
			box.Size = rect.Size;
			box.BorderStyle = BorderStyle.None;
			box.SizeMode = PictureBoxSizeMode.StretchImage;
			return box;
		}

		//Text box generator
		public static TextBox GenerateTextBox(Rectangle rect, string text, bool multiline = false) {
			TextBox box = new TextBox();
			box.Location = rect.Location;
			box.Size = rect.Size;
			box.Text = text;
			if (multiline) {
				box.Multiline = true;
				box.ScrollBars = ScrollBars.Vertical;
			}
			return box;
		}

		//Numeric up down generator
		public static NumericUpDown GenerateNumericUpDown(Rectangle rect, decimal value) {
			NumericUpDown nud = new NumericUpDown();
			nud.Location = rect.Location;
			nud.Size = rect.Size;
			nud.Minimum = decimal.MinValue;
			nud.Maximum = decimal.MaxValue;
			nud.Value = value;
			return nud;
		}

		//Checkbox generator
		public static CheckBox GenerateCheckbox(Rectangle rect, bool check, string text = "") {
			CheckBox box = new CheckBox();
			box.Location = rect.Location;
			box.Size = rect.Size;
			box.Checked = check;
			box.Text = text.Replace("&", "&&");
			box.TextAlign = ContentAlignment.MiddleLeft;
			return box;
		}

		//Date time picker
		public static DateTimePicker GenerateDateTimePicker(Rectangle rect, DateTime? value = null) {
			DateTimePicker dtp = new DateTimePicker();
			dtp.Location = rect.Location;
			dtp.Size = rect.Size;
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

		//Measure width of text
		public static int MeasureWidth(string str, Font font, bool useMargin = true) {
			if (useMargin)
				return TextRenderer.MeasureText(str.Replace("&", "&&"), font).Width - TEXT_MARGIN;
			else
				return TextRenderer.MeasureText(str.Replace("&", "&&"), font).Width;
		}

		//Center rect of given size within width
		public static Rectangle CenterRect(Size controlSize, Size containerSize, Point offset = default) {
			return new Rectangle(
				((containerSize.Width - controlSize.Width) / 2) - offset.X,
				((containerSize.Height - controlSize.Height) / 2) - offset.Y,
				controlSize.Width,
				controlSize.Height
			);
		}

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
