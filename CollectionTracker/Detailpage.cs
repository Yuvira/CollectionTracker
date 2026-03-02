using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	//Detail page
	public class Detailpage : TrackerPage {

		#region Print List Row Class

		//Print list row
		private class DetailPrintrow {

			//Properties
			private Detailpage parent;
			private Label label;
			private string printid;

			//Accessors
			public Label Label => label;

			//Constructor
			public DetailPrintrow(Detailpage parent, string printid, string setname, int yPos, int width, bool isCurrent) {
				this.parent = parent;
				this.printid = printid;
				label = Utils.GenerateLabel(
					new Rectangle(LEFT_PAD, yPos, width, TEXT_HEIGHT),
					printid.ToUpper() + " - " + setname,
					Utils.FONT_UNDERLINE,
					isCurrent ? default : Color.Blue
				);
				label.MouseEnter += ShowCardtip;
				label.MouseLeave += HideCardtip;
				if (!isCurrent)
					label.Click += LoadCardtip;
			}

			//Cardtip functions
			private void ShowCardtip(object sender, EventArgs e) => parent.ShowCardtip(label, printid);
			private void HideCardtip(object sender, EventArgs e) => parent.HideCardtip();
			private void LoadCardtip(object sender, EventArgs e) => parent.LoadCardtip(printid);

		}

		#endregion

		#region Tooltip & Cardtip Classes

		//Tooltip object
		public class Tooltip {

			//Properties
			private Detailpage parent;
			private Control control;
			private string text;

			//Constructor
			public Tooltip(Detailpage parent, Control control, string text) {
				this.parent = parent;
				this.control = control;
				this.text = text;
				this.control.MouseEnter += ShowTooltip;
				this.control.MouseLeave += HideTooltip;
			}

			//Tooltip functions
			private void ShowTooltip(object sender, EventArgs e) => parent.ShowTooltip(control, text);
			private void HideTooltip(object sender, EventArgs e) => parent.HideTooltip();

		}

		//Cardtip object
		public class Cardtip {

			//Properties
			private Detailpage parent;
			private Control control;
			private string printid;

			//Constructor
			public Cardtip(Detailpage parent, Control control, string printid) {
				this.parent = parent;
				this.control = control;
				this.printid = printid;
				this.control.MouseEnter += ShowCardtip;
				this.control.MouseLeave += HideCardtip;
				this.control.Click += LoadCardtip;
			}

			//Cardtip functions
			private void ShowCardtip(object sender, EventArgs e) => parent.ShowCardtip(control, printid);
			private void HideCardtip(object sender, EventArgs e) => parent.HideCardtip();
			private void LoadCardtip(object sender, EventArgs e) => parent.LoadCardtip(printid);

		}

		#endregion

		//Properties
		private Printing printing;
		private List<Tooltip> tooltips;
		private List<Cardtip> cardtips;

		//Controls
		private Panel contentPanel;
		private TrackerPanel printPanel;
		private Panel tooltipPanel;
		private PictureBox cardtipBox;

		//Accessors
		public List<Tooltip> Tooltips => tooltips;
		public List<Cardtip> Cardtips => cardtips;

		//Constructor
		public Detailpage(TrackerForm form, Printing printing) : base(form) {

			//Set print reference
			this.printing = printing;

			//Generate content panel
			contentPanel = Utils.GeneratePanel(Utils.CenterRect(new Size(1290, panel.Height - 10), panel.Size));
			contentPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			contentPanel.AutoScroll = true;

			//Tooltips
			tooltips = new List<Tooltip>();
			cardtips = new List<Cardtip>();
			tooltipPanel = Utils.GeneratePanel(new Rectangle(0, 0, 300, TEXT_HEIGHT));
			tooltipPanel.Hide();
			cardtipBox = Utils.GeneratePictureBox(new Rectangle(0, 0, 250, 350));
			cardtipBox.Hide();
			contentPanel.Controls.Add(tooltipPanel);
			contentPanel.Controls.Add(cardtipBox);

			//Image box
			PictureBox imgBox = Utils.GeneratePictureBox(new Rectangle(5, 5, 400, 540));
			if (this.printing.ImagePaths.Count > 0)
				Utils.TryLoadCardImage(imgBox, this.printing.ImagePaths[0], Catalog.Game);
			contentPanel.Controls.Add(imgBox);

			//Edit card
			Button editCardButton = Utils.GenerateButton(new Rectangle(5, 550, 100, BUTTON_HEIGHT), "Edit Card");
			editCardButton.Click += EditCard;
			contentPanel.Controls.Add(editCardButton);

			//Edit print
			Button editPrintButton = Utils.GenerateButton(new Rectangle(110, 550, 100, BUTTON_HEIGHT), "Edit Print");
			editPrintButton.Click += EditPrint;
			contentPanel.Controls.Add(editPrintButton);

			//Print panel
			printPanel = Utils.GenerateTrackerPanel(new Rectangle(865, 5, 400, TEXT_HEIGHT));
			if (!printing.GetField("name").Equals("_") && !printing.GetField("name").Equals("")) {
				List<Printing> prints = Catalog.Printings.Where(p => p.Card == printing.Card).ToList();
				prints.Sort(new PrintComparerInverseNewest().Compare);
				for (int i = 0; i < prints.Count; ++i) {
					DetailPrintrow row = new DetailPrintrow(
						this,
						prints[i].GetField("printid"),
						prints[i].Set.Name,
						TOP_PAD + (i * TEXT_HEIGHT),
						printPanel.Width - 10,
						prints[i] == printing
					);
					printPanel.Controls.Add(row.Label);
				}
				printPanel.Height = TOP_PAD + BOTTOM_PAD + (prints.Count * TEXT_HEIGHT);
			}
			contentPanel.Controls.Add(printPanel);

			//Card data
			if (Catalog.Game == Game.MTG)
				LayoutDataMTG();

			//Add to panel
			panel.Controls.Add(contentPanel);

		}

		//Modify card data
		private void EditCard(object sender, EventArgs e) => parent.SetPage(new Cardentry(parent, printing.Card, printing));
		private void EditPrint(object sender, EventArgs e) => parent.SetPage(new Printentry(parent, printing));

		#region MTG Layout

		//Color dictionary
		private static readonly Dictionary<char, Color> MTGTypeColors = new Dictionary<char, Color> {
			{ 'w', Color.White },
			{ 'u', Color.Blue  },
			{ 'b', Color.Black },
			{ 'r', Color.Red   },
			{ 'g', Color.Green },
		};

		//Data layout
		private void LayoutDataMTG() {

			//Y Position
			int yPos;
			int yPosPanel = 5;

			//Print faces
			for (int i = 0; i < Math.Max(printing.Card.Faces.Count, 1); ++i) {

				//Panel
				TrackerPanel facePanel = Utils.GenerateTrackerPanel(new Rectangle(410, yPosPanel, 450, 0));

				//Position
				yPos = TOP_PAD;

				//Name and cost
				bool hasName = printing.Card.TryGetField("name", i, out string name);
				bool hasCost = printing.Card.TryGetField("cost", i, out string cost);
				if (hasName)
					GenerateDescription($"<b>{name}", facePanel, new Point(LEFT_PAD, yPos), false);
				if (hasCost)
					GenerateDescription(cost, facePanel, new Point(facePanel.Width - LEFT_PAD, yPos), false, true);
				if (hasName || hasCost)
					yPos += TEXT_HEIGHT;

				//Type line
				if (printing.Card.TryGetField("type", i, out string type)) {
					yPos += BLOCK_SPACING;
					yPos += GenerateDescription($"<b>{type}", facePanel, new Point(LEFT_PAD, yPos), false);
				}

				//Oracle text
				if (printing.Card.TryGetField("oracle", i, out string oracle)) {
					yPos += BLOCK_SPACING;
					yPos += GenerateDescription(oracle, facePanel, new Point(LEFT_PAD, yPos));
				}

				//Power and toughness
				bool hasPower = printing.Card.TryGetField("power", i, out string power);
				bool hasToughness = printing.Card.TryGetField("toughness", i, out string toughness);
				bool hasLoyalty = printing.Card.TryGetField("loyalty", i, out string loyalty);
				string pt = "";
				if (hasPower && hasToughness)
					pt = $"{power} / {toughness}";
				else if (hasPower)
					pt = $"{power} Power";
				else if (hasToughness)
					pt = $"{toughness} Toughness";
				if (hasLoyalty && (hasPower || hasToughness))
					pt = $"{loyalty} Loyalty, {pt}";
				else if (hasLoyalty)
					pt = $"{loyalty} Loyalty";
				if (hasPower || hasToughness || hasLoyalty) {
					yPos += BLOCK_SPACING;
					yPos += GenerateDescription($"<b>{pt}", facePanel, new Point(facePanel.Width - LEFT_PAD, yPos), false, true);
				}

				//Panel height
				facePanel.Height = yPos + BOTTOM_PAD;
				yPosPanel += yPos + BOTTOM_PAD + 5;

				//Panel colors
				if (printing.Card.TryGetField("color", i, out string color))
					SetPanelColorsMTG(facePanel, color);
				else
					facePanel.AddBorder(Utils.COLOR_FRONT, 1);

				//Add to content
				contentPanel.Controls.Add(facePanel);

			}

			//Colour panels
			if (printing.Card.TryGetField("color", 0, out string color2))
				SetPanelColorsMTG(printPanel, color2);
			else
				printPanel.AddBorder(Utils.COLOR_FRONT, 1);

		}

		//Set panel color
		private void SetPanelColorsMTG(TrackerPanel panel, string color) {
			color = color.ToLower();
			if (color.Length == 0)
				panel.AddBorder(SystemColors.ControlDarkDark, 3);
			else if (color.Length == 1 && MTGTypeColors.ContainsKey(color[0]))
				panel.AddBorder(MTGTypeColors[color[0]], 3);
			else if (color.Length == 2 && MTGTypeColors.ContainsKey(color[0]) && MTGTypeColors.ContainsKey(color[1]))
				panel.AddDoubleBorder(MTGTypeColors[color[0]], MTGTypeColors[color[1]], 3);
			else if (color.Length > 2)
				panel.AddBorder(Color.Yellow, 3);
			else
				panel.AddBorder(Utils.COLOR_FRONT, 1);
		}

		#endregion

		#region Tooltips / Cardtips

		//Show tooltip window relative to given control with given text
		public void ShowTooltip(Control control, string text) {
			int posX = control.Parent.Location.X + control.Location.X + (control.Width / 2) - (tooltipPanel.Width / 2);
			int posY = control.Parent.Location.Y + control.Location.Y + TEXT_HEIGHT;
			tooltipPanel.Show();
			tooltipPanel.BringToFront();
			tooltipPanel.Location = new Point(posX, posY);
			tooltipPanel.Controls.Clear();
			int height = GenerateDescription(text, tooltipPanel, new Point(LEFT_PAD, TOP_PAD));
			tooltipPanel.Height = height + TOP_PAD + BOTTOM_PAD;
		}

		//Show tooltip window relative to given control with given text
		public void ShowCardtip(Control control, string printid) {

			//Get modifier
			char mod = ' ';
			if (printid[printid.Length - 2] == '-') {
				mod = printid.ToLower()[printid.Length - 1];
				printid = printid.Substring(0, printid.Length - 2);
			}

			//Load printing
			Printing print = Catalog.Printings.FirstOrDefault(p => p.GetField("printid").Equals(printid));
			if (print != null) {

				//Sideways cards
				if (mod == 's')
					cardtipBox.Size = new Size(350, 250);
				else
					cardtipBox.Size = new Size(250, 350);

				//Position
				int posX = control.Parent.Location.X + control.Location.X + (control.Width / 2) - (cardtipBox.Width / 2);
				int posY = control.Parent.Location.Y + control.Location.Y + TEXT_HEIGHT;

				//Show
				cardtipBox.Show();
				cardtipBox.BringToFront();
				cardtipBox.Location = new Point(posX, posY);

				//Load image
				Utils.TryLoadCardImage(cardtipBox, print.GetImagePath(mod == 'b' ? 1 : 0), Catalog.Game);

				//Rotation
				Image image = cardtipBox.Image;
				if (mod == 'u')
					image.RotateFlip(RotateFlipType.Rotate180FlipNone);
				if (mod == 's')
					image.RotateFlip(RotateFlipType.Rotate90FlipNone);

			}

		}

		//Show tooltip window relative to given control with given text
		public void LoadCardtip(string printid) {
			Printing print = Catalog.Printings.FirstOrDefault(p => p.GetField("printid").Equals(printid));
			if (print != null)
				parent.SetPage(new Detailpage(parent, print));
		}

		//Hide
		public void HideTooltip() => tooltipPanel.Hide();
		public void HideCardtip() => cardtipBox.Hide();

		#endregion

		#region Description Generators

		#region Description Object Classes

		//Formatting settings
		private struct FormatSettings {
			public string tooltip;
			public string printid;
			public FontStyle style;
			public Color? color;
			public int id;
		}

		//Description object class
		private abstract class DescriptionObject {
			public abstract int GetWidth();
			public abstract Point GenerateControl(Detailpage parent, Panel panel, Point location, bool rightAlign = false);
		}

		//Description text
		private class DescriptionText : DescriptionObject {

			//Properties
			public string text;
			public FormatSettings settings;
			public DescriptionText(string text, FormatSettings settings) {
				this.text = text;
				this.settings = settings;
			}

			//Overrides
			public override int GetWidth() => Utils.MeasureWidth(text, new Font(Utils.FONT_DEFAULT, settings.style));
			public override Point GenerateControl(Detailpage parent, Panel panel, Point location, bool rightAlign = false) {
				int width = Utils.MeasureWidth(text, new Font(Utils.FONT_DEFAULT, settings.style));
				if (rightAlign)
					location.X -= width;
				if (settings.tooltip != null || settings.printid != null)
					settings.style |= FontStyle.Underline;
				if (settings.tooltip != null && settings.printid != null)
					settings.color = Color.Teal;
				else if (settings.tooltip != null)
					settings.color = Color.Blue;
				else if (settings.printid != null)
					settings.color = Color.Green;
				Label label = Utils.GenerateLabel(new Rectangle(location, new Size(width, TEXT_HEIGHT)), text, new Font(Utils.FONT_DEFAULT, settings.style), settings.color);
				if (settings.tooltip != null)
					parent.Tooltips.Add(new Tooltip(parent, label, settings.tooltip));
				if (settings.printid != null)
					parent.Cardtips.Add(new Cardtip(parent, label, settings.printid));
				panel.Controls.Add(label);
				if (rightAlign)
					return location;
				return new Point(location.X + width, location.Y);
			}

		}

		//Description symbol
		private class DescriptionSymbol : DescriptionObject {

			//Properties
			public Symbol symbol;
			public DescriptionSymbol(Catalog catalog, string text) {
				symbol = catalog.Symbols.FirstOrDefault(s => s.Text.Equals(text));
			}

			//Overrides
			public override int GetWidth() {
				if (symbol == null)
					return 0;
				return (int)(symbol.Aspect * TEXT_HEIGHT);
			}
			public override Point GenerateControl(Detailpage parent, Panel panel, Point location, bool rightAlign = false) {
				int width = (int)(symbol.Aspect * TEXT_HEIGHT);
				if (rightAlign)
					location.X -= width;
				PictureBox imgBox = Utils.GeneratePictureBox(new Rectangle(location, new Size(width, TEXT_HEIGHT)));
				Utils.TryLoadImage(imgBox, symbol.ImgPath);
				panel.Controls.Add(imgBox);
				if (rightAlign)
					return location;
				return new Point(location.X + width, location.Y);
			}

		}

		//Description group
		private class DescriptionGroup {

			//Properties
			public List<DescriptionObject> objects;
			public DescriptionGroup() {
				objects = new List<DescriptionObject>();
			}

			//Utils
			public void Add(DescriptionObject obj) => objects.Add(obj);
			public void Clean() {
				for (int i = 0; i < objects.Count - 1; ++i) {
					if (objects[i] is DescriptionText t1 && objects[i + 1] is DescriptionText t2 && t1.settings.id == t2.settings.id) {
						t1.text += t2.text;
						objects.RemoveAt(i + 1);
					}
				}
			}
			public int GetWidth() {
				int width = 0;
				foreach (DescriptionObject obj in objects)
					width += obj.GetWidth();
				return width;
			}
			public void Merge(DescriptionGroup group) {
				if (group.objects.Count == 0)
					return;
				if (objects.Count == 0) {
					for (int i = 0; i < group.objects.Count; ++i) {
						if (group.objects[i] is DescriptionText t) {
							if (string.IsNullOrWhiteSpace(t.text))
								continue;
							while (t.text.StartsWith(" "))
								t.text = t.text.Substring(1);
							for (int j = i; j < group.objects.Count; ++j)
								objects.Add(group.objects[j]);
							break;
						}
						else {
							for (int j = i; j < group.objects.Count; ++j)
								objects.Add(group.objects[j]);
							break;
						}
					}
				}
				else {
					if (objects.Last() is DescriptionText t1 && group.objects.First() is DescriptionText t2 && t1.settings.id == t2.settings.id) {
						t1.text += t2.text;
						for (int i = 1; i < group.objects.Count; ++i)
							objects.Add(group.objects[i]);
					}
					else {
						foreach (DescriptionObject obj in group.objects)
							objects.Add(obj);
					}
				}
			}
			public void GenerateControls(Detailpage parent, Panel panel, Point location, bool rightAlign = false) {
				if (!rightAlign)
					foreach (DescriptionObject obj in objects)
						location = obj.GenerateControl(parent, panel, location);
				else
					for (int i = objects.Count - 1; i >= 0; --i)
						location = objects[i].GenerateControl(parent, panel, location, rightAlign);
			}

		}

		#endregion

		//Process formatting marker
		private FormatSettings ProcessFormatMarker(string text, FormatSettings settings) {
			if (string.IsNullOrWhiteSpace(text))
				return settings;
			string[] splits = text.Split('|');
			if (splits.Length == 1) {
				if (splits[0].ToLower().Equals("b"))
					settings.style |= FontStyle.Bold;
				else if (splits[0].ToLower().Equals("/b"))
					settings.style &= ~FontStyle.Bold;
				else if (splits[0].ToLower().Equals("i"))
					settings.style |= FontStyle.Italic;
				else if (splits[0].ToLower().Equals("/i"))
					settings.style &= ~FontStyle.Italic;
				else if (splits[0].ToLower().Equals("u"))
					settings.style |= FontStyle.Underline;
				else if (splits[0].ToLower().Equals("/u"))
					settings.style &= ~FontStyle.Underline;
				else if (splits[0].ToLower().Equals("/c"))
					settings.color = null;
				else if (splits[0].ToLower().Equals("/tt"))
					settings.tooltip = null;
				else if (splits[0].ToLower().Equals("/ct"))
					settings.printid = null;
			}
			else if (splits.Length == 2) {
				if (splits[0].ToLower().Equals("c")) {
					Color? color = Utils.GetColorFromString(splits[1]);
					if (color != null)
						settings.color = color;
				}
				else if (splits[0].ToLower().Equals("tt"))
					settings.tooltip = splits[1];
				else if (splits[0].ToLower().Equals("ct"))
					settings.printid = splits[1];
			}
			return settings;
		}

		//Generate description text in panel at location. Returns total height of the description field
		private int GenerateDescription(string description, Panel panel, Point location, bool allowLineBreaks = true, bool rightAlign = false) {

			//Remove line breaks
			if (!allowLineBreaks)
				description = description.Replace("\r\n", "");

			//Return if nothing to display
			if (string.IsNullOrWhiteSpace(description))
				return 0;

			//Max two linebreaks in a row
			while (description.Contains("\r\n\r\n\r\n"))
				description = description.Replace("\r\n\r\n\r\n", "\r\n\r\n");

			//Starting height
			int startHeight = location.Y;

			//Format settings
			FormatSettings settings = new FormatSettings();

			//Loop blocks
			int curIdx, searchIdx;
			bool firstBlock = true, firstLine = true;
			foreach (string block in Utils.SplitString(description, "\r\n\r\n")) {

				//Spacing
				if (firstBlock)
					firstBlock = false;
				else {
					firstLine = true;
					location.Y += BLOCK_SPACING;
				}

				//Loop lines
				foreach (string line in Utils.SplitString(block, "\r\n")) {

					//Get text
					string text = line;

					//Clear whitespace
					if (string.IsNullOrWhiteSpace(line))
						continue;
					while (text.StartsWith(" "))
						text = text.Substring(1);
					while (text.EndsWith(" "))
						text = text.Substring(0, text.Length - 1);

					//Spacing
					if (firstLine)
						firstLine = false;
					else
						location.Y += LINE_SPACING;

					//Break line into words
					curIdx = 0;
					bool objectFound = false;
					List<DescriptionGroup> groups = new List<DescriptionGroup>();
					DescriptionGroup group = new DescriptionGroup();
					while (curIdx < text.Length) {

						//Add symbols
						if (text[curIdx] == '{') {
							objectFound = true;
							searchIdx = text.IndexOf('}', curIdx);
							if (searchIdx >= 0) {
								group.Add(new DescriptionSymbol(Catalog, text.Substring(curIdx, searchIdx + 1 - curIdx)));
								curIdx = searchIdx + 1;
							}
							else
								group.Add(new DescriptionText("{", settings));
						}

						//Process format markers
						else if (text[curIdx] == '<') {
							searchIdx = text.IndexOf('>', curIdx);
							if (searchIdx >= 0) {
								settings = ProcessFormatMarker(text.Substring(curIdx + 1, searchIdx - 1 - curIdx), settings);
								++settings.id;
								curIdx = searchIdx + 1;
							}
							else
								group.Add(new DescriptionText("<", settings));
						}

						//Process text
						else if (text[curIdx] != ' ') {
							objectFound = true;
							searchIdx = text.IndexOfAny(new char[] { '{', '<', ' ' }, curIdx);
							if (searchIdx < 0)
								searchIdx = text.Length;
							group.Add(new DescriptionText(text.Substring(curIdx, searchIdx - curIdx), settings));
							curIdx = searchIdx;
						}

						//Process spaces
						else {
							if (objectFound) {
								group.Clean();
								groups.Add(group);
								group = new DescriptionGroup();
								objectFound = false;
							}
							else {
								group.Add(new DescriptionText(" ", settings));
								++curIdx;
							}
						}

					}
					group.Clean();
					groups.Add(group);

					//Generate controls
					curIdx = 0;
					int maxWidth = panel.Width - (location.X + 5);
					group = new DescriptionGroup();
					for (int i = 0; i < groups.Count; ++i) {
						if (group.objects.Count == 0 || group.GetWidth() + groups[i].GetWidth() <= maxWidth || !allowLineBreaks)
							group.Merge(groups[i]);
						else {
							group.GenerateControls(this, panel, location, rightAlign);
							location.Y += TEXT_HEIGHT;
							group = new DescriptionGroup();
							group.Merge(groups[i]);
						}
					}
					group.GenerateControls(this, panel, location, rightAlign);
					location.Y += TEXT_HEIGHT;

				}

			}

			//Return height
			return location.Y - startHeight;

		}

		#endregion

	}

}
