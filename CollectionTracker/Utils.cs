using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace CollectionTracker {

	#region Themes

	//Color references
	public class Theme {
		public readonly Color BackColor;
		public readonly Color ForeColor;
		public readonly Color Button;
		public readonly Color ButtonText;
		public readonly Color CardOwned;
		public readonly Color CardUnowned;
		public readonly Color CardOwnedFavorite;
		public readonly Color CardUnownedFavorite;
		public readonly Color TextSearchLink;
		public readonly Color TextCardtip;
		public Theme(Color backColor, Color foreColor, Color button, Color buttonText, Color cardOwned, Color cardUnowned, Color cardOwnedFavorite, Color cardUnownedFavorite, Color textTooltip, Color textCardtip) {
			BackColor = backColor;
			ForeColor = foreColor;
			Button = button;
			ButtonText = buttonText;
			CardOwned = cardOwned;
			CardUnowned = cardUnowned;
			CardOwnedFavorite = cardOwnedFavorite;
			CardUnownedFavorite = cardUnownedFavorite;
			TextSearchLink = textTooltip;
			TextCardtip = textCardtip;
		}
	}

	#endregion

	#region Custom Controls

	//Custom panel class
	public class TrackerPanel : Panel {

		//Border subclass
		private class PanelBorder {
			protected Pen pen;
			public PanelBorder(Color color, int width) => pen = new Pen(color, width);
			public virtual void DrawBorder(Graphics g, int width, int height) => g.DrawRectangle(pen, 0, 0, width, height);
		}
		private class HorizontalGradientPanelBorder : PanelBorder {
			Brush gradientBrush;
			private Pen pen2, gradientPen;
			public HorizontalGradientPanelBorder(Color color1, Color color2, int width, int left, int right) : base(color1, width) {
				pen2 = new Pen(color2, width);
				gradientBrush = new LinearGradientBrush(new Point(left, 0), new Point(right, 0), color1, color2);
				gradientPen = new Pen(gradientBrush, width);
			}
			public override void DrawBorder(Graphics g, int width, int height) {
				g.DrawLine(gradientPen, 0, 0, width, 0);
				g.DrawLine(gradientPen, 0, height, width, height);
				g.DrawLine(pen, 0, 0, 0, height);
				g.DrawLine(pen2, width, 0, width, height);
			}
		}

		//Properties / Constructors
		private PanelBorder border;
		public TrackerPanel() : base() {
			border = null;
			SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
		}
		public TrackerPanel(Color color, int width) : this() => SetBorder(color, width);
		public TrackerPanel(Color color1, Color color2, int width) : this() => SetBorder(color1, color2, width);

		//Border modifiers
		public void SetBorder(Color color, int width) => border = new PanelBorder(color, width);
		public void SetBorder(Color color1, Color color2, int width) => border = new HorizontalGradientPanelBorder(color1, color2, width, ClientRectangle.Left, ClientRectangle.Right);
		public void ClearBorder() => border = null;

		//Paint event
		protected override void OnPaint(PaintEventArgs e) {
			if (border != null) {
				e.Graphics.FillRectangle(Utils.BRUSH_BACK, ClientRectangle);
				border.DrawBorder(e.Graphics, ClientSize.Width - 1, ClientSize.Height - 1);
			}
			else
				base.OnPaint(e);
		}

	}

	//Custom text underline class
	public class DottedUnderline : Panel {

		//Properties / Constructors
		private Pen pen;
		public DottedUnderline() : base() => SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
		public DottedUnderline(Color color, int width) : this() => SetUnderline(color, width);

		//Underline modifiers
		public void SetUnderline(Color color, int width) {
			pen = new Pen(color, 1);
			pen.DashStyle = DashStyle.Dot;
		}
		public void ClearUnderline() => pen = null;

		//Paint event
		protected override void OnPaint(PaintEventArgs e) {
			if (pen != null)
				e.Graphics.DrawLine(pen, 0, 0, ClientSize.Width - 1, 0);
		}

	}

	#endregion

	//Global utilities
	public static class Utils {

		#region Static References

		//Themes
		public static readonly Theme THEME_DEFAULT = new Theme(
			backColor:           SystemColors.ControlDark,
			foreColor:           SystemColors.ControlText,
			button:              SystemColors.ControlLight,
			buttonText:          SystemColors.ControlText,
			cardOwned:           SystemColors.ControlDark,
			cardUnowned:         SystemColors.ControlDarkDark,
			cardOwnedFavorite:   Color.Red,
			cardUnownedFavorite: Color.DarkRed,
			textTooltip:         Color.Blue,
			textCardtip:         Color.Green
		);
		public static readonly Theme THEME_DARK = new Theme(
			backColor:           BlendColours(Color.Black, Color.DarkSlateGray),
			foreColor:           Color.White,
			button:              SystemColors.ControlLight,
			buttonText:          SystemColors.ControlText,
			cardOwned:           SystemColors.ControlDark,
			cardUnowned:         SystemColors.ControlDarkDark,
			cardOwnedFavorite:   Color.Red,
			cardUnownedFavorite: Color.DarkRed,
			textTooltip:         BlendColours(Color.White, Color.Blue),
			textCardtip:         Color.LightGreen
		);
		public static readonly Theme THEME = THEME_DARK;

		//Font style references
		public static readonly Font FONT_DEFAULT = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
		public static readonly Font FONT_BOLD = new Font(FONT_DEFAULT, FontStyle.Bold);
		public static readonly Font FONT_ITALIC = new Font(FONT_DEFAULT, FontStyle.Italic);
		public static readonly Font FONT_UNDERLINE = new Font(FONT_DEFAULT, FontStyle.Underline);

		//Brush references
		public static readonly SolidBrush BRUSH_BACK = new SolidBrush(THEME.BackColor);

		//Color references
		public static readonly Color COLOR_DARK_ORANGE = BlendColours(Color.Orange, Color.Black);

		//Formatting characters
		public static readonly char[] FORMAT_CHARS = { '{', '<' };

		//Resource paths
		public static readonly Dictionary<Game, string> ResourcePaths = new Dictionary<Game, string> {
			{ Game.MTG  , "resources/mtg/"  },
			{ Game.YGO  , "resources/ygo/"  },
			{ Game.PKMN , "resources/pkmn/" },
		};

		//Set URL paths
		public static readonly Dictionary<Game, string> SetURLs = new Dictionary<Game, string> {
			{ Game.MTG , "https://scryfall.com/sets/"  },
			{ Game.YGO , "https://yugipedia.com/wiki/" },
		};

		//Card URL paths
		public static readonly Dictionary<Game, string> CardURLs = new Dictionary<Game, string> {
			{ Game.MTG , "https://scryfall.com/card/"  },
			{ Game.YGO , "https://yugipedia.com/wiki/" },
		};

		//Default card back imagepaths
		public static readonly Dictionary<Game, string> CardBackPaths = new Dictionary<Game, string> {
			{ Game.MTG  , "resources/mtg/back.png"  },
			{ Game.YGO  , "resources/ygo/back.png"  },
			{ Game.PKMN , "resources/pkmn/back.png" },
		};

		//Default treatments
		public static readonly Dictionary<Game, List<string>> DefaultTreatments = new Dictionary<Game, List<string>> {
			{ Game.MTG  , new List<string> { "Normal", "Foil" } },
			{ Game.YGO  , new List<string> { "Common" } },
			{ Game.PKMN , new List<string> { "Normal", "Reverse Holo" } },
		};

		//Default card fields
		public static readonly Dictionary<Game, List<string>> DefaultCardFields = new Dictionary<Game, List<string>> {
			{ Game.MTG  , new List<string> { "name", "identity", "color", "cost", "type", "oracle", "power", "toughness" } },
			{ Game.YGO  , new List<string> { "name", "cardtype", "attribute", "property", "level", "type", "oracle", "attack", "defense" } },
			{ Game.PKMN , new List<string> { "name", "energy", "type", "stage", "hp", "oracle", "weak", "resist", "retreat" } },
		};

		//Default print fields
		public static readonly Dictionary<Game, List<string>> DefaultPrintFields = new Dictionary<Game, List<string>> {
			{ Game.MTG  , new List<string> { "cn", "printid", "rarity", "artist" } },
			{ Game.YGO  , new List<string> { "cn", "printid" } },
			{ Game.PKMN , new List<string> { "cn", "printid", "rarity", "artist", "flavor", "regulation" } },
		};

		//Tag shortcuts
		public static readonly Dictionary<Keys, string> TagShortcuts = new Dictionary<Keys, string> {
			{ Keys.L , "i" },
			{ Keys.B , "b" },
			{ Keys.U , "u" },
			{ Keys.O , "c|" },
			{ Keys.R , "ct|" },
			{ Keys.T , "tt|" },
			{ Keys.S , "s|" },
		};

		//String-defined colors
		public static readonly Dictionary<string, Color> ColorDefinitions = new Dictionary<string, Color> {
			{ "red"   , THEME.CardOwnedFavorite },
			{ "green" , THEME.TextCardtip       },
			{ "blue"  , THEME.TextSearchLink    },
			{ "white" , Color.White             },
			{ "text"  , THEME.ForeColor         },
		};

		//Fields that can list all values
		public static readonly List<string> ListableFields = new List<string> {
			"rarity",
			"artist",
			"regulation",
			"cardtype",
			"attribute",
			"property",
			"layout",
		};

		//Fields that can be kept on list regeneration
		public static readonly List<string> KeepableFields = new List<string> {
			"cn",
			"printid",
			"rarity",
			"regulation",
		};

		//Fields that contain multiline text
		public static readonly List<string> MultilineFields = new List<string> {
			"oracle",
			"flavor",
		};

		//List of characters to exclude for tag text selection
		public static readonly List<string> TagExcludedChars = new List<string> {
			" ",
			".",
			",",
			"'",
			"\"",
			"‘",
			"’",
			"“",
			"”",
		};

		#endregion

		#region Control Generators

		//Panel generator
		public static Panel GeneratePanel() => GeneratePanel(Rectangle.Empty);
		public static Panel GeneratePanel(Rectangle rect) {
			Panel panel = new Panel();
			panel.Location = rect.Location;
			panel.Size = rect.Size;
			panel.BorderStyle = BorderStyle.FixedSingle;
			return panel;
		}

		//Custom panel generator
		public static TrackerPanel GenerateTrackerPanel(bool useDefaultBorder = false) => GenerateTrackerPanel(Rectangle.Empty, useDefaultBorder);
		public static TrackerPanel GenerateTrackerPanel(Rectangle rect, bool useDefaultBorder = false) {
			TrackerPanel panel = new TrackerPanel();
			panel.Location = rect.Location;
			panel.Size = rect.Size;
			if (useDefaultBorder)
				panel.SetBorder(THEME.ForeColor, 1);
			return panel;
		}

		//Button generator
		public static Button GenerateButton(string text, string imgPath = "") => GenerateButton(Rectangle.Empty, text , imgPath);
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
			button.ForeColor = SystemColors.ControlText;
			button.UseVisualStyleBackColor = true;
			return button;
		}

		//Button generator
		public static RadioButton GenerateRadioButton(string text) => GenerateRadioButton(Rectangle.Empty, text);
		public static RadioButton GenerateRadioButton(Rectangle rect, string text) {
			RadioButton button = new RadioButton();
			button.Location = rect.Location;
			button.Size = rect.Size;
			button.Text = text.Replace("&", "&&");
			button.TextAlign = ContentAlignment.MiddleCenter;
			button.Appearance = Appearance.Button;
			button.FlatStyle = FlatStyle.Popup;
			button.BackColor = THEME.Button;
			button.ForeColor = SystemColors.ControlText;
			return button;
		}

		//ComboBox generator
		public static ComboBox GenerateComboBox(ComboBoxStyle style, bool sorted) => GenerateComboBox(Rectangle.Empty, style, sorted);
		public static ComboBox GenerateComboBox(Rectangle rect, ComboBoxStyle style, bool sorted) {
			ComboBox comboBox = new ComboBox();
			comboBox.Location = rect.Location;
			comboBox.Size = rect.Size;
			comboBox.DropDownStyle = style;
			comboBox.Sorted = sorted;
			comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			comboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
			return comboBox;
		}

		//Label Generator
		public static Label GenerateAutoSizeLabel(string text, Font font = null, Color? color = null) => GenerateAutoSizeLabel(Point.Empty, text, font, color);
		public static Label GenerateAutoSizeLabel(Point pos, string text, Font font = null, Color? color = null) => GenerateLabel(new Rectangle(pos, new Size(MeasureWidth(text, font), TrackerPage.TEXT_HEIGHT)), text, font, color);
		public static Label GenerateLabel(string text, Font font = null, Color? color = null) => GenerateLabel(Rectangle.Empty, text, font, color);
		public static Label GenerateLabel(Rectangle rect, string text, Font font = null, Color? color = null) {
			Label label = new Label();
			label.Location = rect.Location;
			label.Size = rect.Size;
			label.Text = text.Replace("&", "&&");
			label.Font = font != null ? font : FONT_DEFAULT;
			label.ForeColor = color ?? THEME.ForeColor;
			label.TextAlign = ContentAlignment.MiddleLeft;
			label.FlatStyle = FlatStyle.System;
			return label;
		}

		//Underline generator
		public static DottedUnderline GenerateUnderline(Label label, Color? color = null, int width = 1) {
			DottedUnderline underline = new DottedUnderline(color ?? label.ForeColor, width);
			underline.Location = new Point(label.Location.X, label.Location.Y + TrackerPage.UNDERLINE_OFFSET);
			underline.Size = new Size(label.Size.Width, 1);
			return underline;
		}

		//Progress bar generator
		public static ProgressBar GenerateProgressBar(int value = 0) => GenerateProgressBar(Rectangle.Empty, value);
		public static ProgressBar GenerateProgressBar(Rectangle rect, int value = 0) {
			ProgressBar bar = new ProgressBar();
			bar.Location = rect.Location;
			bar.Size = rect.Size;
			bar.Value = value;
			return bar;
		}

		//Picture box generator
		public static PictureBox GeneratePictureBox() => GeneratePictureBox(Rectangle.Empty);
		public static PictureBox GeneratePictureBox(Rectangle rect) {
			PictureBox box = new PictureBox();
			box.Location = rect.Location;
			box.Size = rect.Size;
			box.BorderStyle = BorderStyle.None;
			box.SizeMode = PictureBoxSizeMode.StretchImage;
			return box;
		}

		//Text box generator
		public static TextBox GenerateTextBox(string text, bool multiline = false) => GenerateTextBox(Rectangle.Empty, text, multiline);
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
		public static NumericUpDown GenerateNumericUpDown(decimal value) => GenerateNumericUpDown(Rectangle.Empty, value);
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
		public static CheckBox GenerateCheckbox(bool check, string text = "") => GenerateCheckbox(Rectangle.Empty, check, text);
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
		public static DateTimePicker GenerateDateTimePicker(DateTime? value = null) => GenerateDateTimePicker(Rectangle.Empty, value);
		public static DateTimePicker GenerateDateTimePicker(Rectangle rect, DateTime? value = null) {
			DateTimePicker dtp = new DateTimePicker();
			dtp.Location = rect.Location;
			dtp.Size = rect.Size;
			dtp.Value = value ?? DateTime.Now;
			return dtp;
		}

		//Toolstrip button
		public static ToolStripButton GenerateTSButton(string text, EventHandler eventHandler, bool enabled = true) {
			ToolStripButton button = new ToolStripButton();
			button.Text = text;
			button.Click += eventHandler;
			button.Enabled = enabled;
			button.ForeColor = SystemColors.ControlText;
			return button;
		}

		//Toolstrip dropdown button
		public static ToolStripDropDownButton GenerateTSDDButton(string text, ToolStripDropDown dropDown, bool enabled = true) {
			ToolStripDropDownButton button = new ToolStripDropDownButton();
			button.Text = text;
			button.DropDown = dropDown;
			button.Enabled = enabled;
			button.ForeColor = SystemColors.ControlText;
			return button;
		}

		//Toolstrip label
		public static ToolStripLabel GenerateTSLabel(string text, bool rightAlign = false, int rightPad = 0) {
			ToolStripLabel label = new ToolStripLabel();
			label.Text = text;
			if (rightAlign)
				label.Alignment = ToolStripItemAlignment.Right;
			if (rightPad > 0)
				label.Padding = new Padding(0, 0, rightPad, 0);
			label.ForeColor = SystemColors.ControlText;
			return label;
		}

		#endregion

		#region Formatting Utilities

		//Split by string delimiter
		public static string[] SplitString(string str, string delim) => str.Split(new string[] { delim }, StringSplitOptions.None);

		//Get unescaped formatting marker
		public static int FindFormatMarkerIndex(string str, int idx = 0, char formatChar = ' ') {
			while (idx < str.Length) {
				if (formatChar == ' ')
					idx = str.IndexOfAny(FORMAT_CHARS, idx);
				else
					idx = str.IndexOf(formatChar, idx);
				if (idx <= 0 || str[idx = 1] != '\\')
					return idx;
				++idx;
			}
			return -1;
		}

		//Measure width of text
		public static int MeasureWidth(string str, Font font = null) {
			if (font == null)
				font = FONT_DEFAULT;
			return TextRenderer.MeasureText(str.Replace("&", "&&"), font).Width - TrackerPage.TEXT_MARGIN;
		}

		//Get color from string
		public static Color? GetColorFromString(string str) {
			if (ColorDefinitions.ContainsKey(str.ToLower()))
				return ColorDefinitions[str.ToLower()];
			string[] values = str.Split(',');
			if (values.Length == 3 && int.TryParse(values[0], out int R) && int.TryParse(values[1], out int G) && int.TryParse(values[2], out int B))
				return Color.FromArgb(255, R, G, B);
			if (values.Length == 4 && int.TryParse(values[0], out int A) && int.TryParse(values[1], out R) && int.TryParse(values[2], out G) && int.TryParse(values[3], out B))
				return Color.FromArgb(A, R, G, B);
			return null;
		}

		#endregion
		
		#region Misc. Utilities

		//Center rect of given size within container
		public static Rectangle CenterRect(Size controlSize, Size containerSize, Point offset = default) {
			return new Rectangle(
				((containerSize.Width - controlSize.Width) / 2) + offset.X,
				((containerSize.Height - controlSize.Height) / 2) + offset.Y,
				controlSize.Width,
				controlSize.Height
			);
		}

		//Horizontally center rect of given size within width
		public static Rectangle CenterRect(Size controlSize, Size containerSize, int yPos) => CenterRect(controlSize.Width, controlSize.Height, containerSize, yPos);
		public static Rectangle CenterRect(int width, int height, Size containerSize, int yPos) {
			return new Rectangle(
				(containerSize.Width - width) / 2,
				yPos,
				width,
				height
			);
		}

		//Rectangle from size only
		public static Rectangle RectFromSize(int width, int height) {
			Rectangle rect = Rectangle.Empty;
			rect.Width = width;
			rect.Height = height;
			return rect;
		}

		//Blend list of colours
		public static Color BlendColours(Color col1, Color col2) =>
			Color.FromArgb((col1.A + col2.A) / 2, (col1.R + col2.R) / 2, (col1.G + col2.G) / 2, (col1.B + col2.B) / 2);
		public static Color BlendColours(Color col1, Color col2, Color col3) =>
			Color.FromArgb((col1.A + col2.A + col3.A) / 3, (col1.R + col2.R + col3.R) / 3, (col1.G + col2.G + col3.G) / 3, (col1.B + col2.B + col3.B) / 3);
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

		//Load image. Load nothing if it doesn't exist
		public static bool TryLoadImage(PictureBox box, string path) {
			try {
				box.Load(path);
				return true;
			}
			catch (Exception) { return false; }
		}

		//Try to load card image and return default path if failed
		public static bool TryLoadCardImage(PictureBox imgBox, string path, Game game, Label label = null) {
			try {
				imgBox.Load(path);
				if (label != null)
					label.Text = path;
				return true;
			}
			catch (Exception) {
				if (CardBackPaths.ContainsKey(game)) {
					try { imgBox.Load(CardBackPaths[game]); }
					catch (Exception) { }
				}
				if (label != null)
					label.Text = "Failed!";
				return false;
			}
		}

		//Check if image exists at path
		public static bool ImageExistsAtPath(string path, out string newPath) {
			newPath = path;
			if (File.Exists(path + ".png")) {
				newPath += ".png";
				return true;
			}
			else if (File.Exists(path + ".jpg")) {
				newPath += ".jpg";
				return true;
			}
			return false;
		}

		#endregion

	}

	//Extensions
	public static class Extensions {

		#region String Extensions

		//Clean formatting tags from a string
		public static string CleanFormatMarkers(this string str) {
			int idx = str.IndexOf('<');
			int idx2;
			while (idx != -1) {
				idx2 = str.IndexOf('>', idx);
				if (idx2 != -1)
					str = str.Remove(idx, idx2 + 1 - idx);
				else
					str = str.Remove(idx, 1);
				idx = str.IndexOf('<');
			}
			return str;
		}

		//Check if string ends with any listed strings
		public static bool EndsWithAny(this string str, List<string> ends) {
			foreach (string end in ends)
				if (str.EndsWith(end))
					return true;
			return false;
		}

		#endregion

		#region Point Extensions

		//Add co-ordinates
		public static Point Add(this Point point, int x, int y) {
			point.X += x;
			point.Y += y;
			return point;
		}
		public static Point AddX(this Point point, int x) {
			point.X += x;
			return point;
		}
		public static Point AddY(this Point point, int y) {
			point.Y += y;
			return point;
		}

		//Add point
		public static Point Add(this Point point, Point add) {
			point.X += add.X;
			point.Y += add.Y;
			return point;
		}

		//Center within container
		public static Point CenterX(this Point point, int controlWidth, int containerWidth) {
			point.X = (containerWidth - controlWidth) / 2;
			return point;
		}
		public static Point CenterY(this Point point, int controlHeight, int containerHeight) {
			point.Y = (containerHeight - controlHeight) / 2;
			return point;
		}

		#endregion

	}

}
