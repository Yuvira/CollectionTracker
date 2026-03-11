using System;
using System.Collections.Generic;
using System.Diagnostics;
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
					isCurrent ? Utils.THEME.ForeColor : Utils.THEME.TextTooltip
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
		private Printing prevListPrint;
		private Printing nextListPrint;
		private List<Printing> refPrints;
		private int refIndex;
		private int imgIndex;
		private bool viewData;
		private List<DetailTreatmentPanel> treatmentPanels;
		private List<DetailPrintrow> printRows;
		private List<Tooltip> tooltips;
		private List<Cardtip> cardtips;

		//Controls
		private Button navButtonLeft;
		private Button navButtonRight;
		private Button refButtonBack;
		private Button refButtonForward;
		private PictureBox imgBox;
		private Panel contentPanel;
		private ComboBox moveToBox;
		private List<TrackerPanel> dataPanels;
		private List<TrackerPanel> borderPanels;
		private TrackerPanel viewPanel;
		private TrackerPanel printPanel;
		private Panel tooltipPanel;
		private PictureBox cardtipBox;

		//Accessors
		public List<Tooltip> Tooltips => tooltips;
		public List<Cardtip> Cardtips => cardtips;

		//Constructor
		public Detailpage(Printing printing) : base() {

			//Initial values
			refIndex = 0;
			refPrints = new List<Printing> { printing };
			dataPanels = new List<TrackerPanel>();
			borderPanels = new List<TrackerPanel>();
			treatmentPanels = new List<DetailTreatmentPanel>();
			printRows = new List<DetailPrintrow>();
			moveToBox = null;

			//Generate content panel
			contentPanel = Utils.GeneratePanel(Utils.CenterRect(new Size(1290, panel.Height - 40), panel.Size, new Point(0, -20)));
			contentPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			contentPanel.AutoScroll = true;

			//Nav buttons
			navButtonLeft = Utils.GenerateButton(new Rectangle(contentPanel.Location.X, 5, 200, 30), "");
			navButtonLeft.Anchor = AnchorStyles.Top;
			navButtonLeft.Click += NavLeft;
			navButtonRight = Utils.GenerateButton(new Rectangle(contentPanel.Location.X + contentPanel.Width - 200, 5, 200, 30), "");
			navButtonRight.Anchor = AnchorStyles.Top;
			navButtonRight.Click += NavRight;

			//Ref buttons
			refButtonBack = Utils.GenerateButton(new Rectangle(contentPanel.Location.X + 205, 5, 30, 30), "<");
			refButtonBack.Anchor = AnchorStyles.Top;
			refButtonBack.Click += RefBack;
			refButtonForward = Utils.GenerateButton(new Rectangle(contentPanel.Location.X + 240, 5, 30, 30), ">");
			refButtonForward.Anchor = AnchorStyles.Top;
			refButtonForward.Click += RefForward;

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
			imgBox = Utils.GeneratePictureBox(new Rectangle(5, 5, 400, 540));
			imgBox.MouseClick += OnClickImage;
			contentPanel.Controls.Add(imgBox);

			//Edit card
			Button editCardButton = Utils.GenerateButton(new Rectangle(5, 550, 100, BUTTON_HEIGHT), "Edit Card");
			editCardButton.Click += EditCard;
			contentPanel.Controls.Add(editCardButton);

			//Edit print
			Button editPrintButton = Utils.GenerateButton(new Rectangle(110, 550, 100, BUTTON_HEIGHT), "Edit Print");
			editPrintButton.Click += EditPrint;
			contentPanel.Controls.Add(editPrintButton);

			//Views
			viewPanel = Utils.GenerateTrackerPanel(new Rectangle(410, 5, 450, BUTTON_HEIGHT + 10));
			RadioButton cardViewButton = Utils.GenerateRadioButton(new Rectangle(5, 5, 217, BUTTON_HEIGHT), "Card Data");
			cardViewButton.Checked = true;
			cardViewButton.CheckedChanged += SetViewCardData;
			viewPanel.Controls.Add(cardViewButton);
			RadioButton printViewButton = Utils.GenerateRadioButton(new Rectangle(228, 5, 217, BUTTON_HEIGHT), "Owned Printings");
			printViewButton.CheckedChanged += SetViewOwnedPrintings;
			viewPanel.Controls.Add(printViewButton);
			contentPanel.Controls.Add(viewPanel);

			//Print panel
			printPanel = Utils.GenerateTrackerPanel(new Rectangle(865, 5, 400, TEXT_HEIGHT));
			contentPanel.Controls.Add(printPanel);

			//Add to panel
			panel.Controls.Add(navButtonLeft);
			panel.Controls.Add(navButtonRight);
			panel.Controls.Add(refButtonBack);
			panel.Controls.Add(refButtonForward);
			panel.Controls.Add(contentPanel);

			//Set printing
			viewData = true;
			SetPrinting(printing);

		}

		//Set printing
		private void SetPrinting(Printing printing) {

			//Set print reference
			this.printing = printing;

			//Image
			imgIndex = 0;
			if (this.printing.ImagePaths.Count > 0)
				Utils.TryLoadCardImage(imgBox, this.printing.ImagePaths[0], TrackerForm.Catalog.Game);

			//Hide tool/cardtips
			HideTooltip();
			HideCardtip();

			//Prints
			foreach (DetailPrintrow row in printRows) {
				printPanel.Controls.Remove(row.Label);
				row.Label.Dispose();
			}
			printRows.Clear();
			if (!printing.GetField("name").Equals("_") && !printing.GetField("name").Equals("")) {
				List<Printing> prints = TrackerForm.Catalog.Printings.Where(p => p.Card == printing.Card).ToList();
				prints.Sort(Printing.SortInverseNewest);
				for (int i = 0; i < prints.Count; ++i) {
					DetailPrintrow row = new DetailPrintrow(
						this,
						prints[i].GetField("printid"),
						prints[i].Set.Name,
						TOP_PAD + (i * TEXT_HEIGHT),
						printPanel.Width - 10,
						prints[i] == printing
					);
					printRows.Add(row);
					printPanel.Controls.Add(row.Label);
				}
				printPanel.Height = TOP_PAD + BOTTOM_PAD + (prints.Count * TEXT_HEIGHT);
			}

			//Nav buttons
			UpdateNavButtons();

			//View
			UpdateView();

		}

		//Increment image index
		private void OnClickImage(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				if (printing.ImagePaths.Count == 0)
					return;
				imgIndex = (imgIndex + 1) % printing.ImagePaths.Count;
				Utils.TryLoadCardImage(imgBox, printing.ImagePaths[imgIndex], TrackerForm.Catalog.Game);
			}
			else if (e.Button == MouseButtons.Right && printing.TryGetField("printid", out string printid)) {
				if (Utils.CardURLs.ContainsKey(TrackerForm.Catalog.Game))
					Process.Start(Utils.CardURLs[TrackerForm.Catalog.Game] + printid);
			}
		}

		//Modify card data
		private void EditCard(object sender, EventArgs e) => TrackerForm.Instance.SetPage<Cardentry>(cardref: printing.Card, printref: printing);
		private void EditPrint(object sender, EventArgs e) => TrackerForm.Instance.SetPage<Printentry>(printref: printing);

		#region Navigation

		//Update navigation
		public void UpdateNavButtons() {
			if (TrackerForm.Instance.FilteredPrints != null && TrackerForm.Instance.FilteredPrints.Contains(printing)) {
				int idx = TrackerForm.Instance.FilteredPrints.IndexOf(printing);
				prevListPrint = TrackerForm.Instance.FilteredPrints[idx == 0 ? TrackerForm.Instance.FilteredPrints.Count - 1 : idx - 1];
				nextListPrint = TrackerForm.Instance.FilteredPrints[(idx + 1) % TrackerForm.Instance.FilteredPrints.Count];
				navButtonLeft.Text = prevListPrint.GetField("name");
				navButtonRight.Text = nextListPrint.GetField("name");
				navButtonLeft.Show();
				navButtonRight.Show();
			}
			else {
				prevListPrint = null;
				nextListPrint = null;
				navButtonLeft.Hide();
				navButtonRight.Hide();
			}
			refButtonBack.Visible = refIndex - 1 >= 0;
			refButtonForward.Visible = refIndex + 1 < refPrints.Count;
		}

		//Navigate
		private void NavLeft(object sender, EventArgs e) {
			if (prevListPrint != null)
				SetPrinting(prevListPrint);
		}
		private void NavRight(object sender, EventArgs e) {
			if (nextListPrint != null)
				SetPrinting(nextListPrint);
		}

		//References
		private void RefBack(object sender, EventArgs e) {
			if (refIndex - 1 < 0)
				return;
			refPrints[refIndex] = printing;
			--refIndex;
			SetPrinting(refPrints[refIndex]);
		}
		private void RefForward(object sender, EventArgs e) {
			if (refIndex + 1 >= refPrints.Count)
				return;
			refPrints[refIndex] = printing;
			++refIndex;
			SetPrinting(refPrints[refIndex]);
		}

		#endregion

		#region Views

		//Views
		private void SetViewCardData(object sender = null, EventArgs e = null) {
			if (sender != null && sender is RadioButton rb && !rb.Checked)
				return;
			viewData = true;
			UpdateView();
		}
		private void SetViewOwnedPrintings(object sender = null, EventArgs e = null) {
			if (sender != null && sender is RadioButton rb && !rb.Checked)
				return;
			viewData = false;
			UpdateView();
		}

		//Clear data panels and set new view
		private void UpdateView() {

			//Locations
			moveToBox = null;
			treatmentPanels.Clear();

			//Clear borders
			foreach (TrackerPanel panel in borderPanels)
				panel.ClearBorders();
			borderPanels.Clear();
			borderPanels.Add(viewPanel);
			borderPanels.Add(printPanel);
			contentPanel.Controls.Remove(viewPanel);
			contentPanel.Controls.Remove(printPanel);
			contentPanel.Controls.Add(viewPanel);
			contentPanel.Controls.Add(printPanel);

			//Clear data panels
			foreach (TrackerPanel panel in dataPanels) {
				contentPanel.Controls.Remove(panel);
				panel.Dispose();
			}
			dataPanels.Clear();

			//Card data
			if (viewData) {
				if (TrackerForm.Catalog.Game == Game.MTG)
					LayoutDataMTG();
				else if (TrackerForm.Catalog.Game == Game.YGO)
					LayoutDataYGO();
				else if (TrackerForm.Catalog.Game == Game.PKMN)
					LayoutDataPKMN();
			}

			//Owned locations
			else
				LayoutOwnedPrintings();

		}

		#endregion

		#region MTG Layout

		#region Panel Coloring

		//Color dictionary
		private static readonly Dictionary<char, Color> MTGTypeColors = new Dictionary<char, Color> {
			{ 'w' , Color.White },
			{ 'u' , Color.Blue  },
			{ 'b' , Color.Black },
			{ 'r' , Color.Red   },
			{ 'g' , Color.Green },
		};

		//Set panel color
		private void SetPanelColorsMTG(List<TrackerPanel> panels) {
			string color = "";
			if (!printing.Card.TryGetFirstField("identity", out color) || string.IsNullOrWhiteSpace(color))
				printing.Card.TryGetFirstField("color", out color);
			color = color.ToLower();
			foreach (TrackerPanel panel in panels) {
				if (color.Length == 0)
					panel.AddBorder(SystemColors.ControlDarkDark, 3);
				else if (color.Length == 1 && MTGTypeColors.ContainsKey(color[0]))
					panel.AddBorder(MTGTypeColors[color[0]], 3);
				else if (color.Length == 2 && MTGTypeColors.ContainsKey(color[0]) && MTGTypeColors.ContainsKey(color[1]))
					panel.AddDoubleBorder(MTGTypeColors[color[0]], MTGTypeColors[color[1]], 3);
				else if (color.Length > 2)
					panel.AddBorder(Color.Yellow, 3);
				else
					panel.AddBorder(Utils.THEME.ForeColor, 1);
			}
		}

		#endregion

		//Data layout
		private void LayoutDataMTG() {

			//Y Position
			Point textPos = new Point(LEFT_PAD, TOP_PAD);
			Point panelPos = new Point(410, 5);
			Size panelSize = new Size(450, 0);

			//Print faces
			for (int i = -1; i < printing.Card.Faces.Count; ++i) {

				//Position
				textPos.Y = TOP_PAD;

				//Panel
				TrackerPanel facePanel = Utils.GenerateTrackerPanel(new Rectangle(panelPos, panelSize));

				//Name and cost
				int titleHeight = 0;
				if (printing.Card.TryGetFaceField("name", i, out string name))
					titleHeight = Math.Max(titleHeight, LINE_SPACING + GenerateDescription($"<b>{name}", facePanel, textPos, false));
				if (printing.Card.TryGetFaceField("cost", i, out string cost))
					titleHeight = Math.Max(titleHeight, LINE_SPACING + GenerateDescription(cost, facePanel, textPos, false, true));
				textPos.Y += titleHeight;

				//Flavor name
				if (printing.TryGetFaceField("name", i, out string flavorName)) {
					textPos.Y -= LINE_SPACING;
					textPos.Y += LINE_SPACING + GenerateDescription($"<i>{flavorName}", facePanel, textPos, false);
				}

				//Type line
				if (printing.Card.TryGetFaceField("type", i, out string type))
					textPos.Y += LINE_SPACING + GenerateDescription($"<b>{type}", facePanel, textPos, false);

				//Oracle text
				if (printing.Card.TryGetFaceField("oracle", i, out string oracle))
					textPos.Y += LINE_SPACING + GenerateDescription(oracle, facePanel, textPos);

				//Power and toughness
				bool hasPower = printing.Card.TryGetFaceField("power", i, out string power);
				bool hasToughness = printing.Card.TryGetFaceField("toughness", i, out string toughness);
				bool hasLoyalty = printing.Card.TryGetFaceField("loyalty", i, out string loyalty);
				if (hasPower || hasToughness || hasLoyalty) {
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
					textPos.Y += LINE_SPACING + GenerateDescription($"<b>{pt}", facePanel, textPos, false, true);
				}

				//Delete panel if no content
				if (textPos.Y == TOP_PAD) {
					facePanel.Dispose();
					continue;
				}

				//Panel height
				facePanel.Height = textPos.Y + BOTTOM_PAD - LINE_SPACING;
				panelPos.Y += facePanel.Height + 5;

				//Add to content
				borderPanels.Add(facePanel);
				dataPanels.Add(facePanel);
				contentPanel.Controls.Add(facePanel);

				//Flavor footer
				if (i >= 0) {
					bool faceHasFlavor = printing.TryGetFaceField("flavor", i, out string faceFlavor);
					bool faceHasArtist = printing.TryGetFaceField("artist", i, out string faceArtist);
					bool faceHasRarity = printing.TryGetFaceField("rarity", i, out string faceRarity);
					if (faceHasRarity || faceHasFlavor || faceHasArtist) {
						textPos.Y = TOP_PAD;
						TrackerPanel footerPanel = Utils.GenerateTrackerPanel(new Rectangle(panelPos, panelSize));
						if (faceHasFlavor)
							textPos.Y += LINE_SPACING + GenerateDescription($"<i>{faceFlavor}", footerPanel, textPos);
						int footerHeight = 0;
						if (faceHasArtist)
							footerHeight = Math.Max(footerHeight, LINE_SPACING + GenerateDescription($"🖌 {faceArtist}", footerPanel, textPos, false));
						if (faceHasRarity)
							footerHeight = Math.Max(footerHeight, LINE_SPACING + GenerateDescription(faceRarity, footerPanel, textPos, false, true));
						textPos.Y += footerHeight;
						footerPanel.Height = textPos.Y + BOTTOM_PAD - LINE_SPACING;
						panelPos.Y += footerPanel.Height + 5;
						borderPanels.Add(footerPanel);
						dataPanels.Add(footerPanel);
						contentPanel.Controls.Add(footerPanel);
					}
				}

			}

			//Print footer
			bool hasFlavor = printing.TryGetBaseField("flavor", out string flavor);
			bool hasArtist = printing.TryGetBaseField("artist", out string artist);
			bool hasRarity = printing.TryGetBaseField("rarity", out string rarity);
			if (hasRarity || hasFlavor || hasArtist) {
				textPos = new Point(LEFT_PAD, TOP_PAD);
				TrackerPanel footerPanel = Utils.GenerateTrackerPanel(new Rectangle(panelPos, panelSize));
				if (hasFlavor)
					textPos.Y += LINE_SPACING + GenerateDescription($"<i>{flavor}", footerPanel, textPos);
				int footerHeight = 0;
				if (hasArtist)
					footerHeight = Math.Max(footerHeight, LINE_SPACING + GenerateDescription($"🖌 {artist}", footerPanel, textPos, false));
				if (hasRarity)
					footerHeight = Math.Max(footerHeight, LINE_SPACING + GenerateDescription(rarity, footerPanel, textPos, false, true));
				textPos.Y += footerHeight;
				footerPanel.Height = textPos.Y + BOTTOM_PAD - LINE_SPACING;
				panelPos.Y += footerPanel.Height + 5;
				borderPanels.Add(footerPanel);
				dataPanels.Add(footerPanel);
				contentPanel.Controls.Add(footerPanel);
			}

			//View panel
			viewPanel.Location = panelPos;

			//Color panels
			SetPanelColorsMTG(borderPanels);

		}

		#endregion

		#region YGO Layout

		#region Panel Coloring

		//Set panel color
		private void SetPanelColorsYGO(List<TrackerPanel> panels) {
			foreach (TrackerPanel panel in panels)
				panel.AddBorder(SystemColors.ControlDarkDark, 3);
		}

		#endregion

		//Data layout
		private void LayoutDataYGO() {

			//Y Position
			Point textPos = new Point(LEFT_PAD, TOP_PAD);
			Point panelPos = new Point(410, 5);
			Size panelSize = new Size(450, 0);

			//Header
			TrackerPanel headerPanel = Utils.GenerateTrackerPanel(new Rectangle(panelPos, panelSize));
			if (printing.Card.TryGetField("name", out string name))
				textPos.Y += GenerateDescription($"<b>{name}", headerPanel, textPos, false);
			if (printing.Card.TryGetField("cardtype", out string cardtype)) {
				if (printing.Card.TryGetField("attribute", out string attribute))
					cardtype = $"{attribute} {cardtype}";
				if (printing.Card.TryGetField("property", out string property))
					cardtype = $"{property} {cardtype}";
				textPos.Y += LINE_SPACING + GenerateDescription(cardtype, headerPanel, textPos, false);
			}
			if (printing.Card.TryGetField("level", out string level)) {
				if (printing.GetField("type").ToLower().Contains("xyz"))
					textPos.Y += LINE_SPACING + GenerateDescription($"Rank {level}", headerPanel, textPos, false);
				else if (printing.GetField("type").ToLower().Contains("link"))
					textPos.Y += LINE_SPACING + GenerateDescription($"Link-{level}", headerPanel, textPos, false);
				else
					textPos.Y += LINE_SPACING + GenerateDescription($"Level {level}", headerPanel, textPos, false);
			}
			headerPanel.Height = textPos.Y + BOTTOM_PAD - LINE_SPACING;
			panelPos.Y += headerPanel.Height + 5;
			textPos.Y = TOP_PAD;
			borderPanels.Add(headerPanel);
			dataPanels.Add(headerPanel);
			contentPanel.Controls.Add(headerPanel);

			//Pendulum text
			string oracle;
			if (printing.Card.IsMultiface) {
				TrackerPanel pendulumPanel = Utils.GenerateTrackerPanel(new Rectangle(panelPos, panelSize));
				if (printing.Card.TryGetField("pendulum", out string pendulum))
					textPos.Y += LINE_SPACING + GenerateDescription($"<b>Scale {pendulum}", pendulumPanel, textPos, false);
				if (printing.Card.TryGetFaceField("oracle", 0, out oracle))
					textPos.Y += LINE_SPACING + GenerateDescription(oracle, pendulumPanel, textPos);
				pendulumPanel.Height = textPos.Y + BOTTOM_PAD - LINE_SPACING;
				panelPos.Y += pendulumPanel.Height + 5;
				textPos.Y = TOP_PAD;
				borderPanels.Add(pendulumPanel);
				dataPanels.Add(pendulumPanel);
				contentPanel.Controls.Add(pendulumPanel);
			}

			//Oracle text
			TrackerPanel oraclePanel = Utils.GenerateTrackerPanel(new Rectangle(panelPos, panelSize));
			if (printing.Card.TryGetField("type", out string type))
				textPos.Y += LINE_SPACING + GenerateDescription($"<b>{type}", oraclePanel, textPos, false);
			if ((printing.Card.IsMultiface && printing.Card.TryGetFaceField("oracle", 1, out oracle)) || printing.Card.TryGetField("oracle", out oracle))
				textPos.Y += LINE_SPACING + GenerateDescription(oracle, oraclePanel, textPos);
			bool hasAtk = printing.Card.TryGetField("attack", out string atk);
			bool hasDef = printing.Card.TryGetField("defense", out string def);
			if (hasAtk && hasDef)
				textPos.Y += LINE_SPACING + GenerateDescription($"<b>{atk} ATK / {def} DEF", oraclePanel, textPos, false, true);
			else if (hasAtk)
				textPos.Y += LINE_SPACING + GenerateDescription($"<b>{atk} ATK", oraclePanel, textPos, false, true);
			else if (hasDef)
				textPos.Y += LINE_SPACING + GenerateDescription($"<b>{def} DEF", oraclePanel, textPos, false, true);
			oraclePanel.Height = textPos.Y + BOTTOM_PAD - LINE_SPACING;
			panelPos.Y += oraclePanel.Height + 5;
			borderPanels.Add(oraclePanel);
			dataPanels.Add(oraclePanel);
			contentPanel.Controls.Add(oraclePanel);

			//View panel
			viewPanel.Location = panelPos;

			//Color panels
			SetPanelColorsYGO(borderPanels);

		}

		#endregion

		#region PKMN Layout

		#region Panel Coloring

		//Color dictionary
		private static readonly Dictionary<char, Color> PKMNTypeColors = new Dictionary<char, Color> {
			{ 'c' , Color.White             },
			{ 'd' , Color.Black             },
			{ 'f' , Color.Brown             },
			{ 'g' , Color.Green             },
			{ 'l' , Color.Yellow            },
			{ 'm' , Color.Gray              },
			{ 'n' , Utils.COLOR_DARK_ORANGE },
			{ 'p' , Color.DarkMagenta       },
			{ 'r' , Color.Red               },
			{ 'w' , Color.SlateBlue         },
			{ 'y' , Color.DeepPink          },
		};

		//Set panel color
		private void SetPanelColorsPKMN(List<TrackerPanel> panels) {
			string color = printing.GetField("energy").ToLower().Replace("{", "").Replace("}", "");
			foreach (TrackerPanel panel in panels) {
				if (color.Length == 0)
					panel.AddBorder(SystemColors.ControlDarkDark, 3);
				else if (color.Length == 1 && PKMNTypeColors.ContainsKey(color[0]))
					panel.AddBorder(PKMNTypeColors[color[0]], 3);
				else if (color.Length == 2 && PKMNTypeColors.ContainsKey(color[0]) && PKMNTypeColors.ContainsKey(color[1]))
					panel.AddDoubleBorder(PKMNTypeColors[color[0]], PKMNTypeColors[color[1]], 3);
				else if (color.Length > 2)
					panel.AddBorder(Color.Yellow, 3);
				else
					panel.AddBorder(Utils.THEME.ForeColor, 1);
			}
		}

		#endregion

		//Data layout
		private void LayoutDataPKMN() {

			//Y Position
			Point textPos = new Point(LEFT_PAD, TOP_PAD);
			Point panelPos = new Point(410, 5);
			Size panelSize = new Size(450, 0);

			//Header box
			TrackerPanel headerPanel = Utils.GenerateTrackerPanel(new Rectangle(panelPos, panelSize));
			int titleHeight = 0;
			if (printing.Card.TryGetField("name", out string name))
				titleHeight = Math.Max(titleHeight, LINE_SPACING + GenerateDescription($"<b>{name}", headerPanel, textPos, false));
			bool hasEnergyType = printing.Card.TryGetField("energy", out string energy);
			bool hasHP = printing.Card.TryGetField("hp", out string hp);
			if (hasEnergyType && hasHP)
				titleHeight = Math.Max(titleHeight, LINE_SPACING + GenerateDescription($"<b>{hp} HP {energy}", headerPanel, textPos, false, true));
			else if (hasEnergyType)
				titleHeight = Math.Max(titleHeight, LINE_SPACING + GenerateDescription(energy, headerPanel, textPos, false, true));
			else if (hasHP)
				titleHeight = Math.Max(titleHeight, LINE_SPACING + GenerateDescription($"<b>{hp} HP", headerPanel, textPos, false, true));
			textPos.Y += titleHeight;
			if (printing.Card.TryGetField("type", out string type))
				textPos.Y += LINE_SPACING + GenerateDescription(type, headerPanel, textPos);
			if (printing.Card.TryGetField("stage", out string stage))
				textPos.Y += LINE_SPACING + GenerateDescription(stage, headerPanel, textPos);
			headerPanel.Height = textPos.Y + BOTTOM_PAD - LINE_SPACING;
			panelPos.Y += headerPanel.Height + 5;
			textPos.Y = TOP_PAD;
			borderPanels.Add(headerPanel);
			dataPanels.Add(headerPanel);
			contentPanel.Controls.Add(headerPanel);

			//Oracle text
			if (printing.Card.TryGetField("oracle", out string oracle)) {
				TrackerPanel oraclePanel = Utils.GenerateTrackerPanel(new Rectangle(panelPos, panelSize));
				oraclePanel.Height = TOP_PAD + BOTTOM_PAD + GenerateDescription(oracle, oraclePanel, textPos, true, false, true);
				panelPos.Y += oraclePanel.Height + 5;
				borderPanels.Add(oraclePanel);
				dataPanels.Add(oraclePanel);
				contentPanel.Controls.Add(oraclePanel);
			}

			//Footer
			bool hasWeakness = printing.Card.TryGetField("weak", out string weak);
			bool hasResistance = printing.Card.TryGetField("resist", out string resist);
			bool hasRetreatCost = printing.Card.TryGetField("retreat", out string retreat);
			if (hasWeakness || hasResistance || hasRetreatCost) {
				TrackerPanel footerPanel = Utils.GenerateTrackerPanel(new Rectangle(panelPos, panelSize));
				if (hasWeakness)
					textPos.Y += LINE_SPACING + GenerateDescription($"Weakness: {weak}", footerPanel, textPos);
				if (hasResistance)
					textPos.Y += LINE_SPACING + GenerateDescription($"Resistance: {resist}", footerPanel, textPos);
				if (hasRetreatCost)
					textPos.Y += LINE_SPACING + GenerateDescription($"Retreat: {retreat}", footerPanel, textPos);
				footerPanel.Height = textPos.Y + BOTTOM_PAD - LINE_SPACING;
				panelPos.Y += footerPanel.Height + 5;
				textPos.Y = TOP_PAD;
				borderPanels.Add(footerPanel);
				dataPanels.Add(footerPanel);
				contentPanel.Controls.Add(footerPanel);
			}

			//Flavor
			if (printing.TryGetField("flavor", out string flavor)) {
				TrackerPanel flavorPanel = Utils.GenerateTrackerPanel(new Rectangle(panelPos, panelSize));
				flavorPanel.Height = TOP_PAD + BOTTOM_PAD + GenerateDescription($"<i>{flavor}", flavorPanel, textPos);
				panelPos.Y += flavorPanel.Height + 5;
				borderPanels.Add(flavorPanel);
				dataPanels.Add(flavorPanel);
				contentPanel.Controls.Add(flavorPanel);
			}

			//View panel
			viewPanel.Location = panelPos;

			//Color print panel
			SetPanelColorsPKMN(borderPanels);

		}

		#endregion

		#region Owned Printing Layout

		#region Treatment Panel

		//Treatment panel
		private class DetailTreatmentPanel {

			#region Treatment Row

			//Treatment row
			private class DetailTreatmentRow {

				//Properties
				private DetailTreatmentPanel parent;
				private Location location;
				private Panel panel;
				private Label nameLabel;
				private Label countLabel;

				//Accessors
				public Location Location => location;
				public Panel Panel => panel;

				//Constructor
				public DetailTreatmentRow(DetailTreatmentPanel parent, Location location) {

					//Panel header
					this.parent = parent;
					this.location = location;
					panel = Utils.GeneratePanel(new Rectangle(5, 0, 440, 40));
					nameLabel = Utils.GenerateLabel(new Rectangle(5, 10, 270, TEXT_HEIGHT), location.Name);
					panel.Controls.Add(nameLabel);
					countLabel = Utils.GenerateLabel(new Rectangle(280, 10, 50, TEXT_HEIGHT), location.Count.ToString());
					countLabel.TextAlign = ContentAlignment.MiddleRight;
					panel.Controls.Add(countLabel);

					//Buttons
					Button incButton = Utils.GenerateButton(new Rectangle(335, 5, 30, 30), "+");
					incButton.Click += Increment;
					panel.Controls.Add(incButton);
					Button decButton = Utils.GenerateButton(new Rectangle(370, 5, 30, 30), "-");
					decButton.Click += Decrement;
					panel.Controls.Add(decButton);
					Button moveButton = Utils.GenerateButton(new Rectangle(405, 5, 30, 30), "M");
					moveButton.Click += MoveOne;
					panel.Controls.Add(moveButton);

				}

				//Update count
				public void Update() {
					nameLabel.Text = location.Name;
					countLabel.Text = location.Count.ToString();
				}

				//Count modifiers
				private void Increment(object sender, EventArgs e) => parent.IncrementLocation(location.Name);
				private void Decrement(object sender, EventArgs e) => parent.DecrementLocation(location.Name);
				private void MoveOne(object sender, EventArgs e) => parent.MoveOne(location.Name);

			}

			#endregion

			//Properties
			private Detailpage parent;
			private Treatment treatment;
			private TrackerPanel panel;
			private List<DetailTreatmentRow> rows;

			//Accessors
			public Treatment Treatment => treatment;
			public TrackerPanel Panel => panel;

			//Constructor
			public DetailTreatmentPanel(Detailpage parent, Treatment treatment) {

				//Panel header
				this.parent = parent;
				this.treatment = treatment;
				panel = Utils.GenerateTrackerPanel(new Rectangle(410, 0, 450, 0));
				panel.Controls.Add(Utils.GenerateLabel(new Rectangle(5, 10, 350, TEXT_HEIGHT), this.treatment.Name));
				Button addButton = Utils.GenerateButton(new Rectangle(450 - (BUTTON_HEIGHT + 5), 5, BUTTON_HEIGHT, BUTTON_HEIGHT), "+");
				addButton.Click += AddToTreatment;
				panel.Controls.Add(addButton);

				//Rows
				int yPosRow = BUTTON_HEIGHT + 10;
				rows = new List<DetailTreatmentRow>();
				foreach (Location location in this.treatment.Locations) {
					DetailTreatmentRow row = new DetailTreatmentRow(this, location);
					row.Panel.Location = new Point(5, yPosRow);
					yPosRow += row.Panel.Height + 5;
					rows.Add(row);
					panel.Controls.Add(row.Panel);
				}

				//Panel height
				panel.Height = yPosRow;

			}

			//Update content
			public void Update() {

				//Remove locations that no longer exist
				for (int i = 0; i < rows.Count; ++i) {
					if (!treatment.Locations.Contains(rows[i].Location)) {
						rows[i].Panel.Dispose();
						rows.RemoveAt(i);
					}
				}

				//Update order
				for (int i = 0; i < treatment.Locations.Count; ++i) {
					if (i == rows.Count || !rows.Select(r => r.Location).Contains(treatment.Locations[i])) {
						rows.Insert(i, new DetailTreatmentRow(this, treatment.Locations[i]));
						panel.Controls.Add(rows[i].Panel);
					}
					else if (rows[i].Location != treatment.Locations[i]) {
						int index = rows.Select(r => r.Location).ToList().IndexOf(treatment.Locations[i]);
						DetailTreatmentRow row = rows[i];
						rows[i] = rows[index];
						rows[index] = row;
					}
				}

				//Positions
				int yPosRow = BUTTON_HEIGHT + 10;
				foreach (DetailTreatmentRow row in rows) {
					row.Update();
					row.Panel.Location = new Point(5, yPosRow);
					yPosRow += row.Panel.Height + 5;
				}
				panel.Height = yPosRow;

			}

			//Count modifiers
			private void AddToTreatment(object sender, EventArgs e) => parent.AddToTreatment(treatment);
			public void IncrementLocation(string location) => parent.IncrementLocation(treatment, location);
			public void DecrementLocation(string location) => parent.DecrementLocation(treatment, location);
			public void MoveOne(string fromLocation) => parent.MoveOne(treatment, fromLocation);

		}

		#endregion

		#region Count Modifiers

		//Count modifiers
		public void AddToTreatment(Treatment treatment) {
			if (string.IsNullOrEmpty(moveToBox.Text))
				return;
			treatment.Increment(moveToBox.Text);
			UpdateOwnedPrintings();
		}
		public void IncrementLocation(Treatment treatment, string location) {
			treatment.Increment(location);
			UpdateOwnedPrintings();
		}
		public void DecrementLocation(Treatment treatment, string location) {
			if (treatment.Decrement(location))
				UpdateOwnedPrintings();
		}
		public void MoveOne(Treatment treatment, string fromLocation) {
			if (!string.IsNullOrEmpty(moveToBox.Text) && treatment.MoveOne(fromLocation, moveToBox.Text))
				UpdateOwnedPrintings();
		}

		#endregion

		//Owned prints
		private void LayoutOwnedPrintings() {

			//Position
			int yPos = 5;

			//Move to
			TrackerPanel moveToPanel = Utils.GenerateTrackerPanel(new Rectangle(410, yPos, 450, BUTTON_HEIGHT + 10));
			int width = Utils.MeasureWidth("Move to");
			moveToPanel.Controls.Add(Utils.GenerateLabel(new Rectangle(5, 10, width, TEXT_HEIGHT), "Move to"));
			moveToBox = Utils.GenerateComboBox(new Rectangle(width + 10, 5, 450 - (width + 15), BUTTON_HEIGHT), ComboBoxStyle.DropDown, true);
			moveToBox.Items.AddRange(TrackerForm.Catalog.Printings.SelectMany(p => p.Treatments).SelectMany(t => t.Locations).Select(l => l.Name).Distinct().ToArray());
			moveToPanel.Controls.Add(moveToBox);
			yPos += moveToPanel.Height + 5;
			borderPanels.Add(moveToPanel);
			dataPanels.Add(moveToPanel);
			contentPanel.Controls.Add(moveToPanel);

			//Loop treatments
			foreach (Treatment treatment in printing.Treatments) {
				DetailTreatmentPanel panel = new DetailTreatmentPanel(this, treatment);
				panel.Panel.Location = new Point(410, yPos);
				yPos += panel.Panel.Height + 5;
				treatmentPanels.Add(panel);
				borderPanels.Add(panel.Panel);
				dataPanels.Add(panel.Panel);
				contentPanel.Controls.Add(panel.Panel);
			}

			//View panel
			viewPanel.Location = new Point(viewPanel.Location.X, yPos);

			//Panel colors
			if (TrackerForm.Catalog.Game == Game.MTG)
				SetPanelColorsMTG(borderPanels);
			else if (TrackerForm.Catalog.Game == Game.YGO)
				SetPanelColorsYGO(borderPanels);
			else if (TrackerForm.Catalog.Game == Game.PKMN)
				SetPanelColorsPKMN(borderPanels);

		}

		//Update panel positions
		private void UpdateOwnedPrintings() {
			if (treatmentPanels.Count == 0)
				return;
			int yPos = treatmentPanels[0].Panel.Location.Y;
			foreach (DetailTreatmentPanel panel in treatmentPanels) {
				panel.Update();
				panel.Panel.Location = new Point(viewPanel.Location.X, yPos);
				yPos += panel.Panel.Height + 5;
			}
			viewPanel.Location = new Point(viewPanel.Location.X, yPos);
			string location = moveToBox.Text;
			moveToBox.Items.Clear();
			moveToBox.Items.AddRange(TrackerForm.Catalog.Printings.SelectMany(p => p.Treatments).SelectMany(t => t.Locations).Select(l => l.Name).Distinct().ToArray());
			moveToBox.Text = location;
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
			Printing print = TrackerForm.Catalog.Printings.FirstOrDefault(p => p.GetField("printid").Equals(printid));
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
				Utils.TryLoadCardImage(cardtipBox, print.GetImagePath(mod == 'b' ? 1 : 0), TrackerForm.Catalog.Game);

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
			if (printid[printid.Length - 2] == '-')
				printid = printid.Substring(0, printid.Length - 2);
			Printing print = TrackerForm.Catalog.Printings.FirstOrDefault(p => p.GetField("printid").Equals(printid));
			if (print == null)
				return;
			for (int i = refIndex + 1; i < refPrints.Count; ++i) {
				refPrints.RemoveAt(i);
				--i;
			}
			refPrints[refIndex] = printing;
			refPrints.Add(print);
			++refIndex;
			SetPrinting(print);
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

			//Measure text
			public override int GetWidth() => Utils.MeasureWidth(text, new Font(Utils.FONT_DEFAULT, settings.style));

			//Generate
			public override Point GenerateControl(Detailpage parent, Panel panel, Point location, bool rightAlign = false) {
				int width = Utils.MeasureWidth(text, new Font(Utils.FONT_DEFAULT, settings.style));
				if (rightAlign)
					location.X -= width;
				if (settings.tooltip != null || settings.printid != null)
					settings.style |= FontStyle.Underline;
				if (settings.tooltip != null && settings.printid != null)
					settings.color = Utils.BlendColours(Utils.THEME.TextTooltip, Utils.THEME.TextCardtip);
				else if (settings.tooltip != null)
					settings.color = Utils.THEME.TextTooltip;
				else if (settings.printid != null)
					settings.color = Utils.THEME.TextCardtip;
				Label label = Utils.GenerateLabel(new Rectangle(location, new Size(width, TEXT_HEIGHT)), text, new Font(Utils.FONT_DEFAULT, settings.style), settings.color);
				if (settings.tooltip != null)
					parent.Tooltips.Add(new Tooltip(parent, label, settings.tooltip));
				if (settings.printid != null)
					parent.Cardtips.Add(new Cardtip(parent, label, settings.printid));
				panel.Controls.Add(label);
				if (rightAlign)
					return location;
				location.X += width;
				return location;
			}

		}

		//Description symbol
		private class DescriptionSymbol : DescriptionObject {

			//Properties
			public Symbol symbol;
			public DescriptionSymbol(string text) =>
				symbol = TrackerForm.Catalog.Symbols.FirstOrDefault(s => s.Text.Equals(text));

			//Symbol width
			public override int GetWidth() {
				if (symbol == null)
					return 0;
				return (int)(symbol.Aspect * TEXT_HEIGHT);
			}

			//Generate
			public override Point GenerateControl(Detailpage parent, Panel panel, Point location, bool rightAlign = false) {
				int width = (int)(symbol.Aspect * TEXT_HEIGHT);
				if (rightAlign)
					location.X -= width;
				PictureBox imgBox = Utils.GeneratePictureBox(new Rectangle(location.Add(new Point(1, 1)), new Size(width - 2, TEXT_HEIGHT - 2)));
				Utils.TryLoadImage(imgBox, symbol.ImgPath);
				panel.Controls.Add(imgBox);
				if (rightAlign)
					return location;
				location.X += width;
				return location;
			}

		}

		//Description symbol
		private class DescriptionDivider {

			//Properties
			public int width;
			public DescriptionDivider(int width) => this.width = width;

			//Generate
			public TrackerPanel GenerateControl(Detailpage parent, Panel panel, Point location) {
				location.X = (panel.Width / 2) - (width / 2);
				TrackerPanel divider = Utils.GenerateTrackerPanel(new Rectangle(location, new Size(width, 2)));
				panel.Controls.Add(divider);
				return divider;
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

			//Get combined width of objects
			public int GetWidth() {
				int width = 0;
				foreach (DescriptionObject obj in objects)
					width += obj.GetWidth();
				return width;
			}

			//Merge another group into this one, clearing whitespace at group start and merging strings with the same settings
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

			//Generate all controls
			public void GenerateControls(Detailpage parent, Panel panel, Point location, bool rightAlign = false) {
				if (!rightAlign)
					foreach (DescriptionObject obj in objects)
						location = obj.GenerateControl(parent, panel, location);
				else {
					location.X = panel.Width - location.X;
					for (int i = objects.Count - 1; i >= 0; --i)
						location = objects[i].GenerateControl(parent, panel, location, rightAlign);
				}
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
		private int GenerateDescription(string description, Panel panel, Point location, bool allowLineBreaks = true, bool rightAlign = false, bool reduceLineSpacing = false) {

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
					location.Y += reduceLineSpacing ? LINE_SPACING : BLOCK_SPACING;
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
						location.Y += reduceLineSpacing ? 0 : LINE_SPACING;

					//Detect dividers
					if (text.Equals("——")) {
						DescriptionDivider divider = new DescriptionDivider(panel.Width - (LEFT_PAD * 2));
						borderPanels.Add(divider.GenerateControl(this, panel, location));
						continue;
					}

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
								group.Add(new DescriptionSymbol(text.Substring(curIdx, searchIdx + 1 - curIdx)));
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
