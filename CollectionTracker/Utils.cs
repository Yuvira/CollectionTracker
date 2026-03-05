using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CollectionTracker {

	#region Custom Controls

	//Custom panel class
	public class TrackerPanel : Panel {
		private class PanelBorder {
			protected Pen pen;
			public PanelBorder(Color color, int width) => pen = new Pen(color, width);
			public virtual void DrawBorder(Graphics g, int width, int height) => g.DrawRectangle(pen, 0, 0, width, height);
		}
		private class DoublePanelBorder : PanelBorder {
			private Pen pen2;
			public DoublePanelBorder(Color color1, Color color2, int width) : base(color1, width) => pen2 = new Pen(color2, width);
			public override void DrawBorder(Graphics g, int width, int height) {
				g.DrawLine(pen, 0, 0, width, 0);
				g.DrawLine(pen, 0, 0, 0, height);
				g.DrawLine(pen2, width, 0, width, height);
				g.DrawLine(pen2, 0, height, width, height);
			}
		}
		private List<PanelBorder> borders;
		public TrackerPanel() : base() {
			borders = new List<PanelBorder>();
			SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
		}
		public void AddBorder(Color color, int width) => borders.Add(new PanelBorder(color, width));
		public void AddDoubleBorder(Color color1, Color color2, int width) => borders.Add(new DoublePanelBorder(color1, color2, width));
		public void ClearBorders() => borders.Clear();
		protected override void OnPaint(PaintEventArgs e) {
			if (borders.Count > 0) {
				e.Graphics.FillRectangle(Utils.BRUSH_BACK, ClientRectangle);
				foreach (PanelBorder border in borders)
					border.DrawBorder(e.Graphics, ClientSize.Width - 1, ClientSize.Height - 1);
			}
			else
				base.OnPaint(e);
		}
	}

	#endregion

	//Global utilities
	public static class Utils {

		#region Static References

		//Font style references
		public static readonly Font FONT_DEFAULT = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
		public static readonly Font FONT_BOLD = new Font(FONT_DEFAULT, FontStyle.Bold);
		public static readonly Font FONT_ITALIC = new Font(FONT_DEFAULT, FontStyle.Italic);
		public static readonly Font FONT_UNDERLINE = new Font(FONT_DEFAULT, FontStyle.Underline);

		//Brush references
		public static readonly SolidBrush BRUSH_BACK = new SolidBrush(COLOR_BACK);

		//Color references
		public static readonly Color COLOR_BACK = SystemColors.ControlDark;
		public static readonly Color COLOR_BACK_DARK = SystemColors.ControlDarkDark;
		public static readonly Color COLOR_FAVORITE = Color.Red;
		public static readonly Color COLOR_FAVORITE_DARK = Color.DarkRed;
		public static readonly Color COLOR_FRONT = Color.Black;
		public static readonly Color COLOR_BUTTON = SystemColors.ControlLight;
		public static readonly Color COLOR_DARK_ORANGE = BlendColours(new List<Color> { Color.Orange, Color.Black });

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
			{ Game.YGO  , new List<string> { "name", "cardtype", "attribute", "property", "type", "oracle", "level", "attack", "defense" } },
			{ Game.PKMN , new List<string> { "name", "energy", "type", "stage", "hp", "oracle", "weak", "resist", "retreat" } },
		};

		//Default print fields
		public static readonly Dictionary<Game, List<string>> DefaultPrintFields = new Dictionary<Game, List<string>> {
			{ Game.MTG  , new List<string> { "cn", "printid", "rarity", "artist" } },
			{ Game.YGO  , new List<string> { "cn", "printid" } },
			{ Game.PKMN , new List<string> { "cn", "printid", "rarity", "regulation", "artist", "flavor" } },
		};

		//Fields that can list all values
		public static readonly List<string> ListableFields = new List<string> {
			"rarity",
			"artist",
		};

		//Fields that can be kept on list regeneration
		public static readonly List<string> KeepableFields = new List<string> {
			"cn",
			"printid",
			"rarity",
		};

		//Fields that contain multiline text
		public static readonly List<string> MultilineFields = new List<string> {
			"oracle",
			"flavor",
		};

		//String-defined colors
		public static readonly Dictionary<string, Color> ColorDefinitions = new Dictionary<string, Color> {
			{ "red"   , Color.Red   },
			{ "green" , Color.Green },
			{ "blue"  , Color.Blue  },
			{ "white" , Color.White },
		};

		#endregion

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

		//ComboBox generator
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

		//Toolstrip button
		public static ToolStripButton GenerateTSButton(string text, EventHandler eventHandler, bool enabled = true) {
			ToolStripButton button = new ToolStripButton();
			button.Text = text;
			button.Click += eventHandler;
			button.Enabled = enabled;
			return button;
		}

		//Toolstrip dropdown button
		public static ToolStripDropDownButton GenerateTSDDButton(string text, ToolStripDropDown dropDown, bool enabled = true) {
			ToolStripDropDownButton button = new ToolStripDropDownButton();
			button.Text = text;
			button.DropDown = dropDown;
			button.Enabled = enabled;
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

		#region Point Extensions

		//Add co-ordinates
		public static Point Add(this Point point, int x, int y) {
			point.X += x;
			point.Y += y;
			return point;
		}

		//Add point
		public static Point Add(this Point point, Point add) {
			point.X += add.X;
			point.Y += add.Y;
			return point;
		}

		#endregion

	}

}
