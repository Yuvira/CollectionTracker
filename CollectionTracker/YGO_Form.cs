using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CollectionTracker {
	public partial class Form1 : Form {

		//Properties
		public YGO_Catalog ygoCatalog;
		public List<string> ygoRarities;
		public List<YGO_Printing> ygoPrintFilter;
		public YGO_Card ygoUpdateCard = null;
		public YGO_Printing ygoUpdatePrint = null;

		#region Init

		//Initialize
		public void YGO_Initialize() {
			ygoRarities = new List<string>();
			ygoCatalog = new YGO_Catalog();
			ygoPrintFilter = new List<YGO_Printing>();
			ygoDetailPanels = new List<Panel>();
			ygoNameField.LostFocus += new EventHandler((sender, e) => YGO_CheckCardNameExists());
			YGO_Utils.TryLoadCardImage(ygoPrintImgboxBack, YGO_Utils.CARD_BACK_PATH);
			ygoClient.DefaultRequestHeaders.Add("User-Agent", "CollectionTracker");
			ygoClient.DefaultRequestHeaders.Add("Accept", "application/json");
			ygoTooltipPanel.Paint += Utils.PanelPaintDefault;
			ygoDetailPanel.Paint += Utils.PanelPaintDefault;
			ygoPrintingsPanel.Paint += Utils.PanelPaintDefault;
			YGO_LoadCatalog();
		}

		//Re-initialize lists
		public void YGO_InitLists() {
			ygoSetField.Items.Clear();
			ygoRaritiesField.Items.Clear();
			ygoMoveField.Items.Clear();
			ygoSearchSetField.Items.Clear();
			ygoSearchSetField.Items.Add("--");
			ygoSearchLocationField.Items.Clear();
			ygoSearchLocationField.Items.Add("--");
			foreach (YGO_Set set in ygoCatalog.sets) {
				ygoSetField.Items.Add(set);
				ygoSearchSetField.Items.Add(set);
			}
			foreach (string name in ygoCatalog.printings.SelectMany(p => p.rarities).Select(t => t.name).Distinct()) { ygoRaritiesField.Items.Add(name); }
			foreach (string name in ygoCatalog.printings.SelectMany(p => p.rarities).SelectMany(t => t.locations).Distinct()) { ygoMoveField.Items.Add(name); }
			foreach (string name in ygoCatalog.printings.SelectMany(p => p.rarities).SelectMany(t => t.locations).Distinct()) { ygoSearchLocationField.Items.Add(name); }
		}

		#endregion

		#region Set List

		//Properties
		private List<(YGO_Set set, Panel panel, bool expanded)> ygoSetlist = new List<(YGO_Set, Panel, bool)>();
		public int ygoSetPagenum = 0;
		public int ygoSetsPerPage = 1000;

		//Paging
		private void YGO_OnClickPrevSet(object sender, EventArgs e) {
			--ygoSetPagenum;
			YGO_UpdateSets();
		}
		private void YGO_OnClickNextSet(object sender, EventArgs e) {
			++ygoSetPagenum;
			YGO_UpdateSets();
		}

		//Clear set list and update data
		private void YGO_UpdateSets() {

			//Suspend
			ygoSetlistLayout.SuspendLayout();

			//Clear controls and sort sets
			foreach ((YGO_Set set, Panel panel, bool expanded) listSet in ygoSetlist)
				listSet.panel.Dispose();
			ygoSetlist.Clear();
			ygoSetlistLayout.Controls.Clear();
			ygoCatalog.sets.Sort(new YGO_SetComparer().Compare);

			//Pagination
			int maxPage = ygoCatalog.sets.Count / ygoSetsPerPage;
			if (ygoSetPagenum > maxPage)
				ygoSetPagenum = 0;
			if (ygoSetPagenum < 0)
				ygoSetPagenum = maxPage;
			int startIndex = ygoSetPagenum * ygoSetsPerPage;
			int maxIndex = ygoSetsPerPage;
			if (ygoSetPagenum == maxPage)
				maxIndex = ygoCatalog.sets.Count % ygoSetsPerPage;
			maxIndex += startIndex;
			ygoSetPageLabel.Text = (ygoSetPagenum + 1) + " / " + (maxPage + 1);

			//Create set rows
			for (int i = startIndex; i < maxIndex; ++i)
				YGO_CreateSetRow(ygoCatalog.sets[i]);

			//Resume
			ygoSetlistLayout.ResumeLayout();

		}

		//Generate set info
		private void YGO_CreateSetRow(YGO_Set set) {

			//Important values
			List<YGO_Printing> cardsInSet = ygoCatalog.printings.Where(print => print.set == set).ToList();
			int setCount = cardsInSet.Count;
			int setOwned = cardsInSet.Count(print => print.AnyOwned());
			bool missingCardref = cardsInSet.Count(print => print.card.name.Equals("") || print.card.name.Equals("_")) > 0;

			//Set info box
			Panel panel = Utils.GeneratePanel(Point.Empty, new Size(820, 60));
			ygoSetlistLayout.Controls.Add(panel);

			//Filter button
			Button filter = Utils.GenerateButton(new Point(5, 5), new Size(350, 50), set.name, set.imgPath);
			filter.Click += new EventHandler((sender, e) => YGO_FilterCatalogBySet(set));
			filter.MouseUp += new MouseEventHandler((sender, e) => {
				if (e.Button == MouseButtons.Right)
					Process.Start("https://yugipedia.com/wiki/" + set.code);
			});
			panel.Controls.Add(filter);

			//Progress label
			Label label = Utils.GenerateLabel(new Point(360, 20), new Size(100, TEXT_HEIGHT), setOwned.ToString() + '/' + setCount.ToString());
			label.TextAlign = ContentAlignment.MiddleCenter;
			panel.Controls.Add(label);

			//Progress bar
			ProgressBar bar = Utils.GenerateProgressBar(new Point(470, 15), new Size(265, 30), setOwned > 0 ? (int)(((float)setOwned / setCount) * 100) : 0);
			panel.Controls.Add(bar);

			//Missing cardref label
			if (missingCardref) {
				Label cardrefLabel = Utils.GenerateLabel(new Point(780, 20), new Size(35, TEXT_HEIGHT), "*");
				cardrefLabel.TextAlign = ContentAlignment.MiddleCenter;
				panel.Controls.Add(cardrefLabel);
			}

			//No cards logged but folder exists
			else if (cardsInSet.Count == 0 && Directory.Exists("resources/ygo/" + set.code)) {
				Label cardrefLabel = Utils.GenerateLabel(new Point(780, 20), new Size(35, TEXT_HEIGHT), "&");
				cardrefLabel.TextAlign = ContentAlignment.MiddleCenter;
				panel.Controls.Add(cardrefLabel);
			}

			//Add to list
			ygoSetlist.Add((set, panel, false));

		}

		//Filter catalog by set ID
		private void YGO_FilterCatalogBySet(YGO_Set set) {
			ygoPrintFilter.Clear();
			foreach (YGO_Printing print in ygoCatalog.printings) {
				if (print.set == set) {
					ygoPrintFilter.Add(print);
				}
			}
			if (ygoSortMode) { ygoPrintFilter.Sort(new YGO_PrintComparerNumeric().Compare); }
			else { ygoPrintFilter.Sort(new YGO_PrintComparerAlphabetical().Compare); }
			ygoCatalogPagenum = 0;
			YGO_UpdateCatalog();
			ygoTabControl.SelectedTab = ygoCatalogPage;
		}

		#endregion

		#region Search

		//Check if string contains all in a given array of substrings
		private bool YGO_CheckSubstring(string s, string[] arr) {
			foreach (string ss in arr) {
				if (!s.ToLower().Contains(ss.ToLower())) {
					return false;
				}
			}
			return true;
		}

		//Search for card matching given criteria
		private void YGO_Search(object sender, EventArgs e) {

			//Clear print filter
			ygoPrintFilter.Clear();
			ygoPrintFilter = new List<YGO_Printing>();

			//Parameters
			bool searchName = ygoSearchNameField.Text.Length > 0;
			bool searchCardType = ygoSearchCardTypeField.Text.Length > 0;
			bool searchAttribute = ygoSearchAttributeField.Text.Length > 0;
			bool searchProperty = ygoSearchPropertyField.Text.Length > 0;
			bool searchTypes = ygoSearchTypesField.Text.Length > 0;
			bool searchOracle = ygoSearchOracleField.Text.Length > 0;
			string set = ygoSearchSetField.SelectedItem?.ToString() ?? "";
			string loc = ygoSearchLocationField.SelectedItem?.ToString() ?? "";
			bool searchSet = !set.Equals("") && !set.Equals("--");
			bool searchLoc = !loc.Equals("") && !loc.Equals("--");

			//Search
			foreach (YGO_Printing print in ygoCatalog.printings) {
				if (searchName && !YGO_CheckSubstring(print.card.name, ygoSearchNameField.Text.Split('|'))) { continue; }
				if (searchCardType && !YGO_CheckSubstring(print.card.cardType, ygoSearchCardTypeField.Text.Split('|'))) { continue; }
				if (searchAttribute && !YGO_CheckSubstring(print.card.attribute, ygoSearchAttributeField.Text.Split('|'))) { continue; }
				if (searchProperty && !YGO_CheckSubstring(print.card.property, ygoSearchPropertyField.Text.Split('|'))) { continue; }
				if (searchTypes && !YGO_CheckSubstring(print.card.types, ygoSearchTypesField.Text.Split('|'))) { continue; }
				if (searchOracle && !YGO_CheckSubstring(print.card.oracleText, ygoSearchOracleField.Text.Split('|'))) { continue; }
				if (searchSet && print.set != ygoSearchSetField.SelectedItem) { continue; }
				if (searchLoc) {
					bool add = false;
					foreach (YGO_Rarity rarity in print.rarities) {
						if (rarity.locations.Contains(loc)) {
							add = true;
						}
					}
					if (!add) { continue; }
				}
				ygoPrintFilter.Add(print);
			}

			//Show catalog
			if (ygoSortMode) { ygoPrintFilter.Sort(new YGO_PrintComparerNumeric().Compare); }
			else { ygoPrintFilter.Sort(new YGO_PrintComparerAlphabetical().Compare); }
			ygoCatalogPagenum = 0;
			YGO_UpdateCatalog();
			ygoTabControl.SelectedTab = ygoCatalogPage;

		}

		#endregion

		#region Catalog

		//Properties
		public int ygoCatalogPagenum = 0;
		public int ygoCardsPerPage = 50;
		public bool ygoSortMode = true;

		//Sort buttons
		private void YGO_OnClickSortAlphabetical(object sender, EventArgs e) => YGO_UpdateSortMode(false);
		private void YGO_OnClickSortNumeric(object sender, EventArgs e) => YGO_UpdateSortMode(true);
		private void YGO_UpdateSortMode(bool mode) {
			ygoSortMode = mode;
			if (ygoSortMode) { ygoPrintFilter.Sort(new YGO_PrintComparerNumeric().Compare); }
			else { ygoPrintFilter.Sort(new YGO_PrintComparerAlphabetical().Compare); }
			ygoCatalogPagenum = 0;
			YGO_UpdateCatalog();
			ygoTabControl.SelectedTab = ygoCatalogPage;
		}

		//Paging
		private void YGO_OnClickCatalogPrev(object sender, EventArgs e) {
			--ygoCatalogPagenum;
			YGO_UpdateCatalog();
		}
		private void YGO_OnClickCatalogNext(object sender, EventArgs e) {
			++ygoCatalogPagenum;
			YGO_UpdateCatalog();
		}

		//Clear catalog and generate new cards
		private void YGO_UpdateCatalog() {

			//Remove and clear layout controls
			ygoCatalogPage.Controls.Remove(ygoCatalogLayout);
			ygoCatalogLayout.Controls.Clear();

			//Pagination
			int maxPage = ygoPrintFilter.Count / ygoCardsPerPage;
			if (ygoCatalogPagenum > maxPage) { ygoCatalogPagenum = 0; }
			if (ygoCatalogPagenum < 0) { ygoCatalogPagenum = maxPage; }
			int startIndex = ygoCatalogPagenum * ygoCardsPerPage;
			int maxIndex = ygoCardsPerPage;
			if (ygoCatalogPagenum == maxPage) { maxIndex = ygoPrintFilter.Count % ygoCardsPerPage; }
			maxIndex += startIndex;

			//Header
			ygoCatalogIndex.Text = $"Showing {startIndex + 1} - {maxIndex} of {ygoPrintFilter.Count}";

			//Loop printings
			for (int i = startIndex; i < maxIndex; ++i) {

				//Get index/printing
				YGO_Printing print = ygoPrintFilter[i];

				//Card box
				Panel cardPanel = Utils.GeneratePanel(new Point(3, 3), new Size(300, 430 + (30 * print.rarities.Count)));
				if (!print.AnyOwned())
					cardPanel.BackColor = SystemColors.ControlDarkDark;
				ygoCatalogLayout.Controls.Add(cardPanel);

				//Suspend
				cardPanel.SuspendLayout();

				//Image box
				PictureBox img = Utils.GeneratePictureBox(Point.Empty, new Size(300, 420));
				img.Click += new EventHandler((sender, e) => YGO_LoadCardDetails(print));
				YGO_Utils.TryLoadCardImage(img, print.imgPath);
				cardPanel.Controls.Add(img);

				//Loop treatments
				for (int j = 0; j < print.rarities.Count; ++j) {

					//Get treatment at index
					YGO_Rarity treatment = print.rarities[j];

					//Rarity label
					Label treatmentLabel = Utils.GenerateLabel(new Point(60, 430 + (j * 30)), new Size(115, TEXT_HEIGHT), treatment.name);
					treatmentLabel.TextAlign = ContentAlignment.MiddleRight;
					cardPanel.Controls.Add(treatmentLabel);

					//Count label
					Label label = Utils.GenerateLabel(new Point(185, 430 + (j * 30)), new Size(55, TEXT_HEIGHT), print.OwnedCountOfTreatment(treatment.name).ToString());
					cardPanel.Controls.Add(label);

					//Decrement
					Button leftButton = Utils.GenerateButton(new Point(5, 425 + (j * 30)), new Size(55, 29), "<");
					leftButton.Click += new EventHandler((sender, e) => YGO_DecrementCardCount(cardPanel, label, print, treatment.name));
					cardPanel.Controls.Add(leftButton);

					//Increment
					Button rightButton = Utils.GenerateButton(new Point(240, 425 + (j * 30)), new Size(55, 29), ">");
					rightButton.Click += new EventHandler((sender, e) => YGO_IncrementCardCount(cardPanel, label, print, treatment.name));
					cardPanel.Controls.Add(rightButton);

				}

				//Resume
				cardPanel.ResumeLayout();

			}
			ygoCatalogPage.Controls.Add(ygoCatalogLayout);
		}

		//Increment card quantity
		private void YGO_IncrementCardCount(Panel panel, Label label, YGO_Printing print, string treatment) {
			if (print != null) {
				print.Increment(treatment);
				label.Text = print.OwnedCountOfTreatment(treatment).ToString();
				if (!print.AnyOwned()) { panel.BackColor = SystemColors.ControlDarkDark; }
				else { panel.BackColor = SystemColors.ControlDark; }
			}
			else { label.Text = "Print is null!"; }
		}

		//Decrement card quantity
		private void YGO_DecrementCardCount(Panel panel, Label label, YGO_Printing print, string treatment) {
			if (print != null) {
				print.Decrement(treatment);
				label.Text = print.OwnedCountOfTreatment(treatment).ToString();
				if (!print.AnyOwned()) { panel.BackColor = SystemColors.ControlDarkDark; }
				else { panel.BackColor = SystemColors.ControlDark; }
			}
			else { label.Text = "Print is null!"; }
		}

		#endregion

		#region Card Details

		//Properties
		public bool ygoDetailFlipped = false;
		public List<Panel> ygoDetailPanels;
		public YGO_Printing ygoDetailPrint = null;
		public YGO_Printing ygoDetailPrev = null;
		public YGO_Printing ygoDetailNext = null;

		//Load card data into details tab
		private void YGO_LoadCardDetails(YGO_Printing print) {

			//Set persistent reference
			ygoDetailPrint = print;

			//Set image
			YGO_Utils.TryLoadCardImage(ygoDetailImgbox, print.imgPath);
			ygoDetailFlipped = false;

			//Set number in filter
			ygoDetailFilterCountLabel.Text = (ygoPrintFilter.IndexOf(ygoDetailPrint) + 1).ToString() + " / " + ygoPrintFilter.Count.ToString();

			//Get card reference
			YGO_Card card = print.card;

			//Clear old boxes
			foreach (Panel panel in ygoDetailPanels) {
				//Utils.RemovePanelPaintEvent(panel);
				ygoDetailPage.Controls.Remove(panel);
			}
			ygoDetailPanels.Clear();

			//Y position to create elements at
			int y = 5;

			//Box-relative Y position
			int y2 = TOP_PAD;

			//Header
			Panel headerPanel = Utils.GeneratePanel(new Point(ygoDetailPanel.Location.X, y), new Size(ygoDetailPanel.Size.Width, 100));
			//YGO_AddDetailPanelPaintEvent(headerPanel, borderColour, borderColour2);
			ygoDetailPanels.Add(headerPanel);

			//Name
			YGO_WriteLine(card.name, headerPanel, new Point(LEFT_PAD, y2), Utils.FONT_BOLD);
			y2 += TEXT_HEIGHT;

			//Card Type
			if (card.cardType.Length > 0) {
				y2 += LINE_SPACING;
				string s = card.cardType;
				if (card.attribute.Length > 0)
					s = card.attribute + " " + s;
				if (card.property.Length > 0)
					s = card.property + " " + s;
				YGO_WriteLine(s, headerPanel, new Point(LEFT_PAD, y2), Utils.FONT_DEFAULT);
				y2 += TEXT_HEIGHT;
			}

			//Level
			if (card.types.Length > 0) {
				y2 += LINE_SPACING;
				string str = "Level ";
				if (card.types.Contains("Xyz"))
					str = "Rank ";
				else if (card.types.Contains("Link"))
					str = "Link-";
				YGO_WriteLine(str + card.level.ToString(), headerPanel, new Point(LEFT_PAD, y2), Utils.FONT_DEFAULT);
				y2 += TEXT_HEIGHT;
			}

			//Pendulum Scale
			if (card.types.Contains("Pendulum")) {
				y2 += LINE_SPACING;
				YGO_WriteLine("Scale " + card.pendulumScale.ToString(), headerPanel, new Point(LEFT_PAD, y2), Utils.FONT_DEFAULT);
				y2 += TEXT_HEIGHT;
			}

			//Size box and set position for next
			y2 += BOTTOM_PAD;
			headerPanel.Height = y2;
			ygoDetailPage.Controls.Add(headerPanel);
			y += y2 + 5;

			//Oracle panel
			y2 = TOP_PAD;
			Panel oraclePanel = Utils.GeneratePanel(new Point(ygoDetailPanel.Location.X, y), new Size(ygoDetailPanel.Size.Width, 100));
			//YGO_AddDetailPanelPaintEvent(oraclePanel, borderColour, borderColour2);
			ygoDetailPanels.Add(oraclePanel);

			//Monster Types
			if (card.types.Length > 0) {
				YGO_WriteLine(card.types, oraclePanel, new Point(LEFT_PAD, y2), Utils.FONT_BOLD);
				y2 += TEXT_HEIGHT + LINE_SPACING;
			}

			//Oracle Text
			if (card.oracleText.Length > 0)
				y2 += YGO_GenerateDescription(card.oracleText, oraclePanel, new Point(LEFT_PAD, y2));

			//ATK/DEF
			if (card.types.Length > 0) {
				y2 += LINE_SPACING;
				string s = card.atk.ToString() + " ATK";
				if (!card.types.Contains("Link"))
					s += " / " + card.def.ToString() + " DEF";
				int width = TextRenderer.MeasureText(s.Replace("&", "&&"), Utils.FONT_BOLD).Width - TEXT_MARGIN;
				Label atkdef = Utils.GenerateLabel(new Point(ygoDetailPanel.Size.Width - (width + LEFT_PAD), y2), new Size(width, TEXT_HEIGHT), s, Utils.FONT_BOLD);
				oraclePanel.Controls.Add(atkdef);
				y2 += TEXT_HEIGHT;
			}

			//Size box and set position for next
			y2 += BOTTOM_PAD;
			oraclePanel.Height = y2;
			ygoDetailPage.Controls.Add(oraclePanel);
			y += y2 + 5;

			//Load location table
			ygoDetailPanel.Location = new Point(ygoDetailPanel.Location.X, y);
			YGO_LoadLocationTable(print);

			//Load printings
			YGO_LoadPrintingsList(print.card, print);

			//Nav buttons
			int idx = ygoPrintFilter.IndexOf(print);
			if (idx == -1) {
				ygoDetailPrevButton.Hide();
				ygoDetailNextButton.Hide();
				return;
			}
			if (idx > 0)
				ygoDetailPrev = ygoPrintFilter[idx - 1];
			else
				ygoDetailPrev = ygoPrintFilter[ygoPrintFilter.Count - 1];
			if (idx < ygoPrintFilter.Count - 1)
				ygoDetailNext = ygoPrintFilter[idx + 1];
			else
				ygoDetailNext = ygoPrintFilter[0];
			ygoDetailPrevButton.Show();
			ygoDetailPrevButton.Text = ygoDetailPrev.card.name;
			ygoDetailNextButton.Show();
			ygoDetailNextButton.Text = ygoDetailNext.card.name;

			//Hide tooltip
			ygoTooltipPanel.Hide();
			ygoCardtipBox.Hide();

			//Set tab
			ygoTabControl.SelectedTab = ygoDetailPage;

		}

		#region Description Generators

		//Generate description box. Returns total height of the description field
		private int YGO_GenerateDescription(string desc, Panel panel, Point location) {

			//Set initial y position and loop fields
			int y = location.Y;
			while (true) {

				//Clear leading spaces
				if (desc.StartsWith(" ")) { desc = desc.Substring(1); }

				//Get index of next object
				int i = YGO_Utils.IndexOfMany(desc, new List<char>() { '{', '[', '<', '`', '~' });

				//We are at an object, generate it
				if (i == 0) {

					//Symbol
					if (desc[0] == '{') {
						int i2 = desc.IndexOf('}');
						if (i2 > 0) {
							location = YGO_InsertSymbol(desc.Substring(0, i2 + 1), panel, location, TEXT_HEIGHT);
							desc = desc.Substring(i2 + 1);
						}
						else { desc = desc.Substring(1); }
					}

					//Tooltip
					else if (desc[0] == '[') {
						int i2 = desc.IndexOf("|");
						int i3 = desc.IndexOf("]");
						if (i2 > 0 && i3 > 0) {
							location = YGO_InsertTooltip(desc.Substring(1, i2 - 1), desc.Substring(i2 + 1, (i3 - i2) - 1), panel, location);
							desc = desc.Substring(i3 + 1);
						}
						else { desc = desc.Substring(1); }
					}

					//Cardtip
					else if (desc[0] == '<') {
						int i2 = desc.IndexOf("|");
						int i3 = desc.IndexOf(">");
						if (i2 > 0 && i3 > 0) {
							location = YGO_InsertCardtip(desc.Substring(1, i2 - 1), desc.Substring(i2 + 1, (i3 - i2) - 1), panel, location);
							desc = desc.Substring(i3 + 1);
						}
						else { desc = desc.Substring(1); }
					}

					//Bold
					else if (desc[0] == '`') {
						int i2 = desc.IndexOf('`', 1);
						if (i2 > 0) {
							location = YGO_InsertBold(desc.Substring(1, i2 - 1), panel, location);
							desc = desc.Substring(i2 + 1);
						}
						else { desc = desc.Substring(1); }
					}

					//Italic
					else if (desc[0] == '~') {
						int i2 = desc.IndexOf('~', 1);
						if (i2 > 0) {
							location = YGO_InsertItalic(desc.Substring(1, i2 - 1), panel, location);
							desc = desc.Substring(i2 + 1);
						}
						else { desc = desc.Substring(1); }
					}

					//Skip anything else for now
					else { desc = desc.Substring(1); }

				}

				//Otherwise write text
				else {

					//There are no more objects, write remaining text and exit
					if (i == -1) {
						location = YGO_WriteDescription(desc, panel, location);
						break;
					}

					//Write until next object, clear written text, and continue
					else {
						string substr = desc.Substring(0, i);
						if (substr.EndsWith(" ")) { substr = substr.Substring(0, substr.Length - 1); }
						location = YGO_WriteDescription(substr, panel, location);
						desc = desc.Substring(i);
					}

				}

			}

			//Return y delta
			return location.Y + TEXT_HEIGHT - y;

		}

		//Write description text. Returns new text position
		private Point YGO_WriteDescription(string desc, Panel panel, Point location) {

			//Loop line breaks
			string[] lines = desc.Split(new string[] { "\r\n" }, StringSplitOptions.None);
			for (int i = 0; i < lines.Length; ++i) {

				//Jump location
				if (i > 0)
					location = new Point(LEFT_PAD, location.Y + TEXT_HEIGHT + 5);

				//Get space indices
				lines[i] = new Regex("[ ]{2,}", RegexOptions.None).Replace(lines[i], " ");
				if (lines[i].Length == 0)
					continue;
				List<int> spaces = new List<int> { 0 };
				for (int j = lines[i].IndexOf(' '); j > -1; j = lines[i].IndexOf(' ', j + 1))
					spaces.Add(j);

				//Split into words
				List<string> words = new List<string>();
				for (int j = 1; j <= spaces.Count; ++j) {
					if (j == spaces.Count)
						words.Add(lines[i].Substring(spaces[j - 1]));
					else
						words.Add(lines[i].Substring(spaces[j - 1], spaces[j] - spaces[j - 1]));
				}

				//Loop labels
				int idx = 0;
				while (true) {

					//Get available width
					int maxWidth = panel.Width - (location.X + 5);

					//Loop words in line
					string str = "";
					while (true) {

						//If we're done with our text or the next word would exceed available width, generate the label and break
						if (idx == words.Count || TextRenderer.MeasureText((str + words[idx]).Replace("&", "&&"), Utils.FONT_DEFAULT).Width > maxWidth) {

							//First word is exceeding max width, add it if it fills the entire line or skip to next
							if (idx < words.Count && str.Length == 0 && location.X == 5) {
								str += words[idx];
								++idx;
							}

							//Generate label and break
							int textWidth = TextRenderer.MeasureText(str.Replace("&", "&&"), Utils.FONT_DEFAULT).Width - TEXT_MARGIN;
							Label label = Utils.GenerateLabel(location, new Size(textWidth, TEXT_HEIGHT), str, Utils.FONT_DEFAULT);
							panel.Controls.Add(label);
							if (idx == words.Count)
								location = new Point(location.X + textWidth, location.Y);
							else {
								location = new Point(LEFT_PAD, location.Y + TEXT_HEIGHT);
								if (words[idx].StartsWith(" ")) {
									if (words[idx].Equals(" ") && idx == words.Count - 1) {
										++idx;
										break;
									}
									words[idx] = words[idx].Substring(1);
								}
							}
							break;

						}

						//We can keep going, add word to string and continues
						str += words[idx];
						++idx;

					}

					//Break if we're done with this line
					if (idx >= words.Count)
						break;

				}

			}

			//Return end of text
			return location;

		}
		
		//Write simple text
		private Point YGO_WriteLine(string str, Panel panel, Point location, Font font) {
			int width = TextRenderer.MeasureText(str.Replace("&", "&&"), font).Width - TEXT_MARGIN;
			Label label = Utils.GenerateLabel(location, new Size(width, TEXT_HEIGHT), str, font);
			panel.Controls.Add(label);
			return new Point(location.X + width, location.Y);
		}

		//Get list of symbols from string
		private List<string> YGO_GetSymbols(string str) {
			List<string> symbols = new List<string>();
			int i = 0;
			while (i != -1) {
				i = str.IndexOf('{', i);
				if (i != -1) {
					int i2 = str.IndexOf('}', i);
					if (i2 == -1) { return symbols; }
					symbols.Add(str.Substring(i, i2 + 1 - i));
					i = i2;
				}
			}
			return symbols;
		}

		//Insert clickable tooltip text at position. Returns position at end of added text
		private Point YGO_InsertTooltip(string str, string tooltip, Control control, Point location) {
			Label label = new Label();
			label.Font = Utils.FONT_UNDERLINE;
			label.ForeColor = Color.Blue;
			int textWidth = TextRenderer.MeasureText(str, label.Font).Width;
			if (textWidth > control.Width - (location.X + 5)) { location = new Point(5, location.Y + TEXT_HEIGHT); }
			control.Controls.Add(label);
			label.Location = location;
			label.Size = new Size(textWidth, TEXT_HEIGHT);
			label.Text = str;
			label.TextAlign = ContentAlignment.MiddleLeft;
			label.MouseEnter += new EventHandler((sender, e) => YGO_ShowTooltip(label, tooltip));
			label.MouseLeave += new EventHandler((sender, e) => ygoTooltipPanel.Hide());
			location = new Point(location.X + textWidth, location.Y);
			return location;
		}

		//Insert clickable cardtip text at position. Returns position at end of added text
		private Point YGO_InsertCardtip(string str, string cardtip, Control control, Point location) {
			Label label = new Label();
			label.Font = Utils.FONT_UNDERLINE;
			label.ForeColor = Color.Green;
			int textWidth = TextRenderer.MeasureText(str, label.Font).Width;
			if (textWidth > control.Width - (location.X + 5)) { location = new Point(5, location.Y + TEXT_HEIGHT); }
			control.Controls.Add(label);
			label.Location = location;
			label.Size = new Size(textWidth, TEXT_HEIGHT);
			label.Text = str;
			label.TextAlign = ContentAlignment.MiddleLeft;
			label.Click += new EventHandler((sender, e) => YGO_LoadCardtip(cardtip));
			label.MouseEnter += new EventHandler((sender, e) => YGO_ShowCardtip(label, cardtip));
			label.MouseLeave += new EventHandler((sender, e) => ygoCardtipBox.Hide());
			location = new Point(location.X + textWidth, location.Y);
			return location;
		}

		//Insert bold text
		private Point YGO_InsertBold(string str, Control control, Point location) {
			Label label = new Label();
			label.Font = Utils.FONT_BOLD;
			int textWidth = TextRenderer.MeasureText(str, label.Font).Width;
			if (textWidth > control.Width - (location.X + 5)) { location = new Point(5, location.Y + TEXT_HEIGHT); }
			control.Controls.Add(label);
			label.Location = location;
			label.Size = new Size(textWidth, TEXT_HEIGHT);
			label.Text = str;
			label.TextAlign = ContentAlignment.MiddleLeft;
			location = new Point(location.X + textWidth, location.Y);
			return location;
		}

		//Insert italicized text
		private Point YGO_InsertItalic(string str, Control control, Point location) {
			Label label = new Label();
			label.Font = Utils.FONT_ITALIC;
			int textWidth = TextRenderer.MeasureText(str, label.Font).Width;
			if (textWidth > control.Width - (location.X + 5)) { location = new Point(5, location.Y + TEXT_HEIGHT); }
			control.Controls.Add(label);
			label.Location = location;
			label.Size = new Size(textWidth, TEXT_HEIGHT);
			label.Text = str;
			label.TextAlign = ContentAlignment.MiddleLeft;
			location = new Point(location.X + textWidth, location.Y);
			return location;
		}

		//Insert symbol into control at position. Returns position at end of symbol
		private Point YGO_InsertSymbol(string str, Control control, Point location, int height) {

			/*
			//Find symbol object for given symbol string
			YGO_Symbol symbol = ygoCatalog.symbols.FirstOrDefault(s => s.symbol == str);

			//No symbol found, insert text box
			if (symbol == null) {
				Label label = new Label();
				int textWidth = TextRenderer.MeasureText(str, label.Font).Width;
				if (textWidth > control.Width - (location.X + 5)) { location = new Point(5, location.Y + TEXT_HEIGHT); }
				control.Controls.Add(label);
				label.Location = location;
				label.Size = new Size(textWidth, height);
				label.Text = str;
				label.BackColor = Color.White;
				label.TextAlign = ContentAlignment.MiddleLeft;
				location = new Point(location.X + textWidth, location.Y);
				return location;
			}

			//Insert image
			PictureBox icon = new PictureBox();
			int width = (int)(height * symbol.aspect);
			if (width > control.Width - (location.X + 5)) { location = new Point(5, location.Y + TEXT_HEIGHT); }
			control.Controls.Add(icon);
			icon.Location = location;
			icon.Size = new Size(width, height);
			icon.SizeMode = PictureBoxSizeMode.StretchImage;
			YGO_Utils.TryLoadImage(icon, symbol.imgPath);
			location = new Point(location.X + width, location.Y);
			return location;
			*/

			return location;

		}

		#endregion

		#region Detail Utils

		//Show tooltip window relative to given control with given text
		private void YGO_ShowTooltip(Control control, string str) {
			int posX = control.Parent.Location.X + control.Location.X + (control.Width / 2) - (ygoTooltipPanel.Width / 2);
			int posY = control.Parent.Location.Y + control.Location.Y + TEXT_HEIGHT;
			ygoTooltipPanel.Show();
			ygoTooltipPanel.BringToFront();
			ygoTooltipPanel.Location = new Point(posX, posY);
			ygoTooltipPanel.Controls.Clear();
			int height = YGO_GenerateDescription(str, ygoTooltipPanel, new Point(5, 15));
			ygoTooltipPanel.Size = new Size(ygoTooltipPanel.Width, height + 20);
		}

		//Show tooltip window relative to given control with given text
		private void YGO_ShowCardtip(Control control, string str) {

			//Get modifier
			char mod = ' ';
			if (str.Contains('|')) {
				mod = str[str.Length - 1];
				str = str.Substring(0, str.Length - 2);
			}

			//Load printing
			YGO_Printing print = ygoCatalog.printings.FirstOrDefault(p => p.printID.Equals(str));
			if (print != null) {

				//Size
				if (mod == 's' || mod == 'S') {
					ygoCardtipBox.Size = new Size(350, 250);
					ygoCardtipImage.Size = new Size(350, 250);
				}
				else {
					ygoCardtipBox.Size = new Size(250, 350);
					ygoCardtipImage.Size = new Size(250, 350);
				}

				//Position
				int posX = control.Parent.Location.X + control.Location.X + (control.Width / 2) - (ygoCardtipBox.Width / 2);
				int posY = control.Parent.Location.Y + control.Location.Y + TEXT_HEIGHT;

				//Show
				ygoCardtipBox.Show();
				ygoCardtipBox.BringToFront();
				ygoCardtipBox.Location = new Point(posX, posY);

				//Load image
				if (mod == 'b' || mod == 'B') { YGO_Utils.TryLoadCardImage(ygoCardtipImage, print.backImgPath); }
				else { YGO_Utils.TryLoadCardImage(ygoCardtipImage, print.imgPath); }

				//Rotation
				Image img = ygoCardtipImage.Image;
				if (mod == 'u' || mod == 'U') { img.RotateFlip(RotateFlipType.Rotate180FlipNone); }
				if (mod == 's' || mod == 'S') { img.RotateFlip(RotateFlipType.Rotate90FlipNone); }

			}

		}

		//Show tooltip window relative to given control with given text
		private void YGO_LoadCardtip(string str) {
			YGO_Printing print = ygoCatalog.printings.FirstOrDefault(p => p.printID.Equals(str));
			if (print != null) { YGO_LoadCardDetails(print); }
		}

		//Highlight desk rows
		private List<int> highlightRows = new List<int>();
		private void YGO_PaintLocationCell(object sender, TableLayoutCellPaintEventArgs args) {
			if (highlightRows.Contains(args.Row))
				args.Graphics.FillRectangle(Brushes.CornflowerBlue, args.CellBounds);
		}

		//Load location table
		private void YGO_LoadLocationTable(YGO_Printing print) {

			//Clear
			ygoDetailPanel.Controls.Remove(ygoLocationTable);
			ygoLocationTable.Controls.Clear();
			ygoLocationTable.RowCount = 0;
			ygoLocationTable.RowStyles.Clear();
			ygoLocationTable.Size = new Size(ygoLocationTable.Size.Width, 10);
			highlightRows.Clear();

			//Loop rarities and locations
			foreach (YGO_Rarity rarity in print.rarities) {
				for (int i = 0; i < rarity.locations.Count; ++i) {

					//Get row data
					string rar = rarity.name;
					string loc = rarity.locations[i];

					//Add row and resize
					++ygoLocationTable.RowCount;
					ygoLocationTable.Size = new Size(ygoLocationTable.Size.Width, ygoLocationTable.Size.Height + 35);
					ygoLocationTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));

					//Rarity label
					Label rarityLabel = new Label();
					ygoLocationTable.Controls.Add(rarityLabel, 0, ygoLocationTable.RowCount - 1);
					rarityLabel.Dock = DockStyle.Fill;
					rarityLabel.TextAlign = ContentAlignment.MiddleLeft;
					rarityLabel.Text = rar;
					rarityLabel.AutoEllipsis = true;

					//Location label
					Label locationLabel = new Label();
					ygoLocationTable.Controls.Add(locationLabel, 1, ygoLocationTable.RowCount - 1);
					locationLabel.Dock = DockStyle.Fill;
					locationLabel.TextAlign = ContentAlignment.MiddleLeft;
					locationLabel.Text = loc;
					locationLabel.AutoEllipsis = true;

					//Count label
					Label countLabel = new Label();
					ygoLocationTable.Controls.Add(countLabel, 2, ygoLocationTable.RowCount - 1);
					countLabel.Dock = DockStyle.Fill;
					countLabel.TextAlign = ContentAlignment.MiddleLeft;
					countLabel.Text = rarity.quantities[i].ToString();

					//Move button
					Button moveButton = new Button();
					ygoLocationTable.Controls.Add(moveButton, 3, ygoLocationTable.RowCount - 1);
					moveButton.Dock = DockStyle.Fill;
					moveButton.Text = "Move 1";
					moveButton.UseVisualStyleBackColor = true;
					moveButton.Click += new EventHandler((sender, e) => YGO_MoveOne(print, loc, rar));

					//Highlight row
					if (loc.Equals("Desk")) {
						highlightRows.Add(i);
						rarityLabel.BackColor = Color.CornflowerBlue;
						locationLabel.BackColor = Color.CornflowerBlue;
						countLabel.BackColor = Color.CornflowerBlue;
					}

				}
			}

			//Resume
			ygoDetailPanel.Controls.Add(ygoLocationTable);
			ygoDetailPanel.Size = new Size(ygoDetailPanel.Size.Width, ygoLocationTable.Size.Height + 110);

		}

		//Move one card of a given rarity from one location to another
		private void YGO_MoveOne(YGO_Printing print, string from, string rarity) {
			if (ygoMoveField.Text.Length > 0) {
				print.MoveOne(from, ygoMoveField.Text, rarity);
				YGO_LoadLocationTable(print);
			}
		}

		//Reload location data
		private void YGO_OnClickReloadLocations(object sender, EventArgs e) {
			ygoMoveField.Items.Clear();
			ygoSearchLocationField.Items.Clear();
			ygoSearchLocationField.Items.Add("--");
			foreach (string name in ygoCatalog.printings.SelectMany(p => p.rarities).SelectMany(t => t.locations).Distinct()) { ygoMoveField.Items.Add(name); }
			foreach (string name in ygoCatalog.printings.SelectMany(p => p.rarities).SelectMany(t => t.locations).Distinct()) { ygoSearchLocationField.Items.Add(name); }
		}

		//Load printings list
		private void YGO_LoadPrintingsList(YGO_Card card, YGO_Printing curPrint) {
			ygoPrintingsPanel.Controls.Clear();
			if (card.name.Equals("_"))
				return;
			List<YGO_Printing> prints = ygoCatalog.printings.Where(p => p.card == card).ToList();
			prints.Sort(new YGO_PrintComparerNumericReverse().Compare);
			for (int i = 0; i < prints.Count; ++i) {
				Label label = Utils.GenerateLabel(
					new Point(LEFT_PAD, TOP_PAD + (i * TEXT_HEIGHT)),
					new Size(pkmnPrintingsPanel.Width - 10, TEXT_HEIGHT),
					prints[i].printID.ToUpper() + " - " + prints[i].set.name,
					Utils.FONT_UNDERLINE,
					prints[i] != curPrint ? Color.Blue : default
				);
				string id = prints[i].printID;
				label.MouseEnter += new EventHandler((sender, e) => YGO_ShowCardtip(label, id));
				label.MouseLeave += new EventHandler((sender, e) => ygoCardtipBox.Hide());
				if (prints[i] != curPrint)
					label.Click += new EventHandler((sender, e) => YGO_LoadCardtip(id));
				ygoPrintingsPanel.Controls.Add(label);
			}
			ygoPrintingsPanel.Height = TOP_PAD + BOTTOM_PAD + (prints.Count * TEXT_HEIGHT);
		}

		//Flip card image
		private void YGO_ClickDetailCard(object sender, MouseEventArgs e) {
			if (ygoDetailPrint == null)
				return;
			if (e.Button == MouseButtons.Right)
				Process.Start("https://yugipedia.com/wiki/" + ygoDetailPrint.printID.Substring(0, 10));
		}

		//Edit card data
		private void YGO_EditCard(object sender, EventArgs e) => YGO_EditCard();
		private void YGO_EditCard() {

			//Return if print or reference card are invalid
			if (ygoDetailPrint == null) { return; }
			YGO_Card card = ygoDetailPrint.card;
			if (card == null) { return; }

			//Setup edit page
			ygoNameField.Text = card.name;
			ygoCardTypeField.Text = card.cardType;
			ygoAttributeField.Text = card.attribute;
			ygoPropertyField.Text = card.property;
			ygoTypesField.Text = card.types;
			ygoOracleField.Text = card.oracleText;
			ygoLevelField.Value = card.level;
			ygoScaleField.Value = card.pendulumScale;
			ygoAtkField.Value = card.atk;
			ygoDefField.Value = card.def;
			ygoUpdateCard = card;
			ygoAddCardButton.Text = "Update Card";
			ygoTabControl.SelectedTab = ygoCardPage;

		}

		//Edit printing data
		private void YGO_EditPrint(object sender, EventArgs e) => YGO_EditPrint();
		private void YGO_EditPrint() {

			//Return if print is invalid
			if (ygoDetailPrint == null) { return; }
			YGO_Printing print = ygoDetailPrint;

			//Setup edit page
			ygoSetField.SelectedItem = print.set;
			ygoNumberField.Value = print.cardNumber;
			ygoRarities.Clear();
			foreach (YGO_Rarity rarity in print.rarities) { ygoRarities.Add(rarity.name); }
			YGO_UpdateRarityList();
			ygoFlavorField.Text = print.flavorText;
			ygoImgpathLabel.Text = print.imgPath;
			YGO_Utils.TryLoadCardImage(ygoPrintImgbox, print.imgPath);
			ygoImgpathBackLabel.Text = print.backImgPath;
			if (print.backImgPath.Length > 1) { YGO_Utils.TryLoadCardImage(ygoPrintImgboxBack, print.backImgPath); }
			else { /*Load default*/ }
			ygoCardrefField.SelectedItem = print.card;
			ygoPrintIDField.Text = print.printID;
			ygoUpdatePrint = print;
			ygoAddPrintButton.Text = "Update Printing";
			ygoTabControl.SelectedTab = ygoPrintPage;

		}

		//Navigation
		private void YGO_LoadPreviousInSelection(object sender, EventArgs e) => YGO_LoadPreviousInSelection();
		private void YGO_LoadPreviousInSelection() {
			if (ygoDetailPrev != null)
				YGO_LoadCardDetails(ygoDetailPrev);
		}
		private void YGO_LoadNextInSelection(object sender, EventArgs e) => YGO_LoadNextInSelection();
		private void YGO_LoadNextInSelection() {
			if (ygoDetailNext != null)
				YGO_LoadCardDetails(ygoDetailNext);
		}

		//Autogen card details, then move to the next card in the set every two seconds
		private async void YGO_AutogenDetailRef(object sender, EventArgs e) {
			/*
			await YGO_AutogenDetailRef();
			while (ygoDetailNext != null) {
				Thread.Sleep(250);
				YGO_LoadCardDetails(ygoDetailNext);
				Thread.Sleep(250);
				await YGO_AutogenDetailRef();
			}
			*/
		}

		//Get card name from Scryfall, generate card object if it doesn't exist, then set printing reference
		private async Task YGO_AutogenDetailRef() {
			/*
			//Set up result and get card page
			string name = "";
			string result = await YGO_GetWebpage("https://api.scryfall.com/cards/" + ygoDetailPrint.tcgcID);

			//Return if error
			if (result.Contains("Error: ")) {
				ygoDetailDialog.Text = result;
				return;
			}

			//Find name
			if (result.Contains("\"name\":")) {
				int idx = result.IndexOf("\"name\":");
				name = result.Substring(idx + 8);
				idx = name.IndexOf("\",\"");
				name = name.Substring(0, idx);
				ygoNameField.Text = name;
			}

			//Find type line
			if (result.Contains("\"type_line\":")) {
				int idx = result.IndexOf("\"type_line\":");
				string type = result.Substring(idx + 13);
				idx = type.IndexOf("\",\"");
				type = type.Substring(0, idx);
				ygoCardTypeField.Text = type;
			}

			//Find mana cost(s)
			if (result.Contains("\"mana_cost\":")) {
				int idx = result.IndexOf("\"mana_cost\":");
				string costs = result.Substring(idx + 13);
				idx = costs.IndexOf("\",\"");
				string cost = costs.Substring(0, idx);
				if (costs.Contains("\"mana_cost\":")) {
					idx = costs.IndexOf("\"mana_cost\":");
					string cost2 = costs.Substring(idx + 13);
					idx = cost2.IndexOf("\",\"");
					cost2 = cost2.Substring(0, idx);
					cost += " // " + cost2;
				}
				ygoCostField.Text = cost;
			}

			//Find oracle text
			if (result.Contains("\"oracle_text\":")) {
				int idx = result.IndexOf("\"oracle_text\":");
				string oracles = result.Substring(idx + 15);
				idx = oracles.IndexOf("\",\"");
				string oracle = oracles.Substring(0, idx);
				if (oracles.Contains("\"oracle_text\":")) {
					idx = oracles.IndexOf("\"oracle_text\":");
					string oracle2 = oracles.Substring(idx + 15);
					idx = oracle2.IndexOf("\",\"");
					oracle2 = oracle2.Substring(0, idx);
					oracle += "\r\n//\r\n" + oracle2;
				}
				oracle = oracle.Replace("\\n", "\r\n\r\n");
				ygoOracleTextField.Text = oracle;
			}

			//Find powers
			if (result.Contains("\"power\":")) {
				int idx = result.IndexOf("\"power\":");
				string powers = result.Substring(idx + 9);
				idx = powers.IndexOf("\",\"");
				string power = powers.Substring(0, idx);
				try { ygoPowerField.Value = decimal.Parse(power); }
				catch { }
				if (powers.Contains("\"power\":")) {
					idx = powers.IndexOf("\"power\":");
					string power2 = powers.Substring(idx + 9);
					idx = power2.IndexOf("\",\"");
					power2 = power2.Substring(0, idx);
					try { ygoPowerBackField.Value = decimal.Parse(power2); }
					catch { }
				}
			}

			//Find toughnesses
			if (result.Contains("\"toughness\":")) {
				int idx = result.IndexOf("\"toughness\":");
				string toughnesses = result.Substring(idx + 13);
				idx = toughnesses.IndexOf("\",\"");
				string toughness = toughnesses.Substring(0, idx);
				try { ygoToughnessField.Value = decimal.Parse(toughness); }
				catch { }
				if (toughnesses.Contains("\"toughness\":")) {
					idx = toughnesses.IndexOf("\"toughness\":");
					string toughness2 = toughnesses.Substring(idx + 13);
					idx = toughness2.IndexOf("\",\"");
					toughness2 = toughness2.Substring(0, idx);
					try { ygoToughnessBackField.Value = decimal.Parse(toughness2); }
					catch { }
				}
			}

			//Generate card
			ygoIgnoreDuplicateEntryBox.Checked = false;
			YGO_AddCard();
			YGO_Card card = ygoCatalog.cards.FirstOrDefault(c => c.name.Equals(name));
			if (card != null) { ygoDetailPrint.card = card; }
			YGO_LoadCardDetails(ygoDetailPrint);
			ygoDetailDialog.Text = result;
			*/
		}

		//HTTP Client
		static readonly HttpClient ygoClient = new HttpClient();
		static async Task<string> YGO_GetWebpage(string url) {
			try {
				string data = await ygoClient.GetStringAsync(url);
				return data;
			}
			catch (Exception e) {
				string error = "Error: " + e.ToString();
				return error;
			}
		}

		//Delete printing from catalog
		private void YGO_DeleteCurrentPrinting(object sender, EventArgs e) {
			int index = ygoCatalog.printings.IndexOf(ygoDetailPrint);
			ygoCatalog.printings.RemoveAt(index);
			YGO_UpdateSets();
			ygoDetailPrint = null;
		}

		#endregion

		#endregion

		#region Card Entry

		//Check if card exists with name
		private void YGO_CheckCardNameExists() {
			if (ygoCatalog.cards.Select(card => card.name).Contains(ygoNameField.Text))
				ygoCardDialog.Text = ygoNameField.Text + " already exists";
			else
				ygoCardDialog.Text = "-";
		}

		//Import card text
		private void YGO_OnClickImport(object sender, EventArgs e) {

			//Reset fields
			ygoCardTypeField.Text = "";
			ygoAttributeField.Text = "";
			ygoPropertyField.Text = "";
			ygoTypesField.Text = "";
			ygoOracleField.Text = "";
			ygoLevelField.Value = 0;
			ygoScaleField.Value = 0;
			ygoAtkField.Value = 0;
			ygoDefField.Value = 0;

			//Split and set values
			string[] lines = ygoImportField.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
			for (int i = 0; i < lines.Length; ++i) {
				if (lines[i].StartsWith("Card type"))
					ygoCardTypeField.Text = lines[i].Replace("Card type \t", "");
				if (lines[i].StartsWith("Attribute"))
					ygoAttributeField.Text = lines[i].Replace("Attribute \t", "");
				if (lines[i].StartsWith("Property"))
					ygoPropertyField.Text = lines[i].Replace("Property \t", "");
				if (lines[i].StartsWith("Types"))
					ygoTypesField.Text = lines[i].Replace("Types \t", "");
				if (lines[i].StartsWith("Level \t"))
					ygoLevelField.Value = decimal.Parse(lines[i].Replace("Level \t", "").Replace("CG Star.svg", "").Replace(" ", ""));
				if (lines[i].StartsWith("Rank \t"))
					ygoLevelField.Value = decimal.Parse(lines[i].Replace("Rank \t", "").Replace("Rank Star.svg", "").Replace(" ", ""));
				if (lines[i].StartsWith("ATK / DEF")) {
					string[] values = lines[i].Replace("ATK / DEF \t", "").Replace(" ", "").Split('/');
					if (values[0].Equals("?"))
						values[0] = "0";
					if (values[1].Equals("?"))
						values[1] = "0";
					ygoAtkField.Value = decimal.Parse(values[0]);
					ygoDefField.Value = decimal.Parse(values[1]);
				}
				if (lines[i].StartsWith("ATK / LINK")) {
					string[] values = lines[i].Replace("ATK / LINK \t", "").Replace(" ", "").Split('/');
					if (values[0].Equals("?"))
						values[0] = "0";
					if (values[1].Equals("?"))
						values[1] = "0";
					ygoAtkField.Value = decimal.Parse(values[0]);
					ygoDefField.Value = decimal.Parse(values[1]);
				}
				if (lines[i].StartsWith("Pendulum Scale"))
					ygoScaleField.Value = decimal.Parse(lines[i].Replace("Pendulum Scale \tPendulum Scale.png ", ""));
				if (i == lines.Length - 1) {
					if (ygoTypesField.Text.Contains("Fusion") || ygoTypesField.Text.Contains("Xyz") || ygoTypesField.Text.Contains("Link") || ygoTypesField.Text.Contains("Synchro"))
						ygoOracleField.Text = lines[i - 1] + "\r\n" + lines[i];
					else if (ygoTypesField.Text.Contains("Pendulum"))
						ygoOracleField.Text = "`Pendulum Effect`\r\n" + lines[i - 2].Substring(4) + "\r\n`Monster Effect`\r\n" + lines[i].Substring(4);
					else
						ygoOracleField.Text = lines[i];
				}
			}

		}

		//Add card to catalog
		private void YGO_OnClickAddCard(object sender, EventArgs e) => YGO_AddCard();
		private void YGO_AddCard() {

			//Generate card object
			YGO_Card card = new YGO_Card(
				ygoNameField.Text,
				ygoCardTypeField.Text,
				ygoAttributeField.Text,
				ygoPropertyField.Text,
				ygoTypesField.Text,
				ygoOracleField.Text,
				(int)ygoLevelField.Value,
				(int)ygoScaleField.Value,
				(int)ygoAtkField.Value,
				(int)ygoDefField.Value
			);

			//Create or update card in catalog
			if (ygoUpdateCard == null) {
				if (ygoCatalog.cards.Select(c => c.name).Contains(ygoNameField.Text) && !ygoIgnoreDuplicateEntryBox.Checked) {
					ygoCardDialog.Text = ygoNameField.Text + " already exists";
					return;
				}
				ygoCatalog.cards.Add(card);
				YGO_UpdateCardList();
				ygoCardDialog.Text = "-";
			}
			else {
				ygoUpdateCard.Copy(card);
				ygoCardDialog.Text = "Card Data Updated";
				if (ygoDetailPrint.card == ygoUpdateCard) { YGO_LoadCardDetails(ygoDetailPrint); }
			}

			//Reset fields
			ygoNameField.Text = "";
			ygoCardTypeField.Text = "";
			ygoAttributeField.Text = "";
			ygoPropertyField.Text = "";
			ygoTypesField.Text = "";
			ygoOracleField.Text = "";
			ygoLevelField.Value = 0;
			ygoScaleField.Value = 0;
			ygoAtkField.Value = 0;
			ygoDefField.Value = 0;
			ygoIgnoreDuplicateEntryBox.Checked = false;
			ygoUpdateCard = null;
			ygoAddCardButton.Text = "Add To Catalog";

		}

		#endregion

		#region Printing Entry

		//Clear and refresh list of card names
		private void YGO_UpdateCardList() {
			ygoCardrefField.Items.Clear();
			foreach (YGO_Card card in ygoCatalog.cards) { ygoCardrefField.Items.Add(card); }
		}

		//Update monster list label
		private void YGO_UpdateRarityList() {
			if (ygoRarities.Count == 0) {
				ygoRaritiesValue.Text = "-";
				return;
			}
			string s = "";
			for (int i = 0; i < ygoRarities.Count; ++i) {
				if (i > 0) { s += ", "; }
				s += ygoRarities[i];
			}
			ygoRaritiesValue.Text = s;
		}

		//Add rarity to list
		private void YGO_OnClickAddRarity(object sender, EventArgs e) {
			if (ygoRaritiesField.Text.Length <= 0) { return; }
			if (ygoRarities.Contains(ygoRaritiesField.Text)) { return; }
			ygoRarities.Add(ygoRaritiesField.Text);
			YGO_UpdateRarityList();
		}

		//Remove rarity from list
		private void YGO_OnClickSubRarity(object sender, EventArgs e) {
			if (ygoRarities.Count == 0) { return; }
			ygoRarities.RemoveAt(ygoRarities.Count - 1);
			YGO_UpdateRarityList();
		}

		//Get image path and display card
		private void YGO_OnClickSearchImg(object sender, EventArgs e) {
			if (imageFileDialog.ShowDialog() == DialogResult.OK) {
				string curDir = Directory.GetCurrentDirectory();
				string filePath = imageFileDialog.FileName;
				if (!filePath.Contains(curDir)) {
					ygoImgpathLabel.Text = "Selected file is not locally referrable";
					return;
				}
				filePath = filePath.Remove(filePath.IndexOf(curDir), curDir.Length + 1);
				ygoImgpathLabel.Text = filePath;
				YGO_Utils.TryLoadCardImage(ygoPrintImgbox, filePath);
			}
		}

		//Get image path and display card back
		private void YGO_OnClickSearchImgBack(object sender, EventArgs e) {
			if (imageFileDialog.ShowDialog() == DialogResult.OK) {
				string curDir = Directory.GetCurrentDirectory();
				string filePath = imageFileDialog.FileName;
				if (!filePath.Contains(curDir)) {
					ygoImgpathBackLabel.Text = "Selected file is not locally referrable";
					return;
				}
				filePath = filePath.Remove(filePath.IndexOf(curDir), curDir.Length + 1);
				ygoImgpathBackLabel.Text = filePath;
				YGO_Utils.TryLoadCardImage(ygoPrintImgboxBack, filePath);
			}
		}

		//Autofill image
		private void YGO_OnClickPrintAutofill(object sender, EventArgs e) => YGO_OnClickPrintAutofill();
		private bool YGO_OnClickPrintAutofill() {
			YGO_Set set = (YGO_Set)ygoSetField.SelectedItem;
			if (set != null) {
				ygoCardrefField.SelectedItem = ygoCatalog.cards.FirstOrDefault(c => c.name.Equals("_"));
				string code = set.code;
				string num = ygoNumberField.Value.ToString().PadLeft(3, '0');
				ygoPrintIDField.Text = code + "-EN" + num;
				string path = "resources/ygo/" + code + "/EN" + num;
				ygoCardrefDescriptor.Text = path;
				if (YGO_Utils.TryLoadCardImage(ygoPrintImgbox, path + ".png", ygoImgpathLabel))
					return true;
			}
			return false;
		}

		//Populate descriptor when card reference is selected
		private void YGO_OnSelectCardref(object sender, EventArgs e) {

			//Return if index or card is invalid
			if (ygoCardrefField.SelectedIndex < 0) { return; }
			YGO_Card card = (YGO_Card)ygoCardrefField.SelectedItem;
			if (card == null) { return; }

			//Build description string
			string s = "";
			if (card.types.Length > 0) {
				s += card.atk.ToString() + " ATK / ";
				if (card.types.Contains("Link"))
					s += "LINK " + card.def.ToString() + " ";
				else
					s += card.def.ToString() + " DEF ";
			}
			if (card.attribute.Length > 0)
				s += card.attribute + " ";
			if (card.property.Length > 0)
				s += card.property + " ";
			s += card.cardType + " ";
			s += card.name;
			if (card.types.Length > 0)
				s += " | " + card.types;
			if (card.oracleText.Length > 0)
				s += " | " + card.oracleText;

			//Populate descriptor
			ygoCardrefDescriptor.Text = s;

		}

		//Add printing to catalog
		private void YGO_OnClickAddPrint(object sender, EventArgs e) => YGO_OnClickAddPrint();
		private void YGO_OnClickAddPrint() {

			//Return if no set, treatments, images, or card reference set
			if (ygoSetField.SelectedIndex < 0) { return; }
			if (ygoRarities.Count == 0) { return; }
			if (ygoImgpathLabel.Text.Length <= 1) { return; }
			if (ygoCardrefField.SelectedIndex < 0) { return; }

			//Generate printing object
			YGO_Printing print = new YGO_Printing(
				(YGO_Set)ygoSetField.SelectedItem,
				(int)ygoNumberField.Value,
				ygoFlavorField.Text,
				ygoImgpathLabel.Text,
				ygoImgpathBackLabel.Text,
				YGO_Rarity.GenerateTreatments(ygoRarities),
				(YGO_Card)ygoCardrefField.SelectedItem,
				ygoPrintIDField.Text
			);

			//Create or update printing in catalog
			if (ygoUpdatePrint == null) {
				ygoCatalog.printings.Add(print);
				YGO_UpdateSets();
				ygoImgpathLabel.Text = "-";
				ygoImgpathBackLabel.Text = "";
			}
			else {
				ygoUpdatePrint.Copy(print);
				ygoImgpathLabel.Text = "Printing Data Updated";
				if (ygoDetailPrint == ygoUpdatePrint) { YGO_LoadCardDetails(ygoDetailPrint); }
			}

			//Reset fields
			ygoNumberField.Value += 1;
			ygoFlavorField.Text = "";
			ygoCardrefField.SelectedIndex = -1;
			int index = ygoPrintIDField.Text.IndexOf("/");
			if (index > 0) {
				ygoPrintIDField.Text = ygoPrintIDField.Text.Substring(0, index + 1);
				ygoPrintIDField.Text += ((int)ygoNumberField.Value).ToString();
			}
			else { ygoPrintIDField.Text = ""; }
			ygoPrintImgbox.Image = null;
			YGO_Utils.TryLoadCardImage(ygoPrintImgboxBack, YGO_Utils.CARD_BACK_PATH);
			ygoCardrefDescriptor.Text = "-";
			ygoUpdatePrint = null;
			ygoAddPrintButton.Text = "Add To Catalog";

		}

		//Add card to catalog and autofill next
		private void YGO_OnClickAddAndFill(object sender, EventArgs e) {
			int lastIndex = -1;
			while (true) {
				YGO_OnClickAddPrint();
				if (ygoNumberField.Value == lastIndex) { break; }
				lastIndex = (int)ygoNumberField.Value;
				if (!YGO_OnClickPrintAutofill()) { break; }
				if (lastIndex > ygoPrintAutoLimit.Value) { break; }
			}
		}

		#endregion

		#region Symbols

		/*

		//Properties
		private List<YGO_FormSymbol> ygoFormSymbols = new List<YGO_FormSymbol>();

		//Symbol form class
		private class YGO_FormSymbol {
			public GroupBox box;
			public TextBox nameBox;
			public TextBox symbolBox;
			public NumericUpDown aspectBox;
			public Label pathLabel;
			public PictureBox iconBox;
			public YGO_FormSymbol() : this(null, null, null, null, null, null) { }
			public YGO_FormSymbol(GroupBox box, TextBox nameBox, TextBox symbolBox, NumericUpDown aspectBox, Label pathLabel, PictureBox iconBox) {
				this.box = box;
				this.nameBox = nameBox;
				this.symbolBox = symbolBox;
				this.aspectBox = aspectBox;
				this.pathLabel = pathLabel;
				this.iconBox = iconBox;
			}
		}

		//Regenerate symbol controls
		private void YGO_RegenerateSymbols() {
			foreach (YGO_FormSymbol symbol in ygoFormSymbols) { ygoSymbolLayout.Controls.Remove(symbol.box); }
			ygoFormSymbols.Clear();
			if (ygoCatalog.symbols == null) { ygoCatalog.symbols = new List<YGO_Symbol>(); }
			foreach (YGO_Symbol symbol in ygoCatalog.symbols) {
				if (symbol.aspect < 0.01m) { symbol.aspect = 0.01m; }
				if (symbol.aspect > 100.0m) { symbol.aspect = 100.0m; }
				YGO_CreateSymbolBox(symbol);
			}
		}

		//Add new symbol
		private void YGO_OnClickAddSymbol(object sender, EventArgs e) => YGO_CreateSymbolBox();
		private void YGO_CreateSymbolBox() => YGO_CreateSymbolBox(new YGO_Symbol(), false);
		private void YGO_CreateSymbolBox(YGO_Symbol refSymbol, bool useRef = true) {

			//Index
			int i = ygoFormSymbols.Count;

			//Group box
			GroupBox box = new GroupBox();
			box.Size = new Size(325, 120);

			//Symbol box
			TextBox symbol = new TextBox();
			box.Controls.Add(symbol);
			symbol.Location = new Point(5, 15);
			symbol.Size = new Size(245, 30);
			symbol.Text = useRef ? refSymbol.symbol : "Symbol";

			//Name box
			TextBox name = new TextBox();
			box.Controls.Add(name);
			name.Location = new Point(5, 50);
			name.Size = new Size(245, 30);
			name.Text = useRef ? refSymbol.name : "Name";

			//Aspect box
			NumericUpDown aspect = new NumericUpDown();
			box.Controls.Add(aspect);
			aspect.Location = new Point(5, 85);
			aspect.Size = new Size(55, 30);
			aspect.DecimalPlaces = 2;
			aspect.Increment = 0.01m;
			aspect.Minimum = 0.01m;
			aspect.Value = useRef ? refSymbol.aspect : 1.00m;

			//Path label
			Label path = new Label();
			box.Controls.Add(path);
			path.Location = new Point(65, 85);
			path.Size = new Size(255, 30);
			path.TextAlign = ContentAlignment.MiddleLeft;

			//Icon box
			PictureBox icon = new PictureBox();
			box.Controls.Add(icon);
			icon.Location = new Point(255, 15);
			icon.Size = new Size(65, 65);
			icon.SizeMode = PictureBoxSizeMode.StretchImage;
			icon.Cursor = Cursors.Hand;
			icon.Click += new EventHandler((sender, e) => YGO_OnClickSearchSymbol(i));

			//Load image if one is referenced
			if (refSymbol.imgPath.Length > 0) {
				path.Text = refSymbol.imgPath;
				YGO_Utils.TryLoadImage(icon, refSymbol.imgPath);
			}

			//Create object and add box to layout
			ygoFormSymbols.Add(new YGO_FormSymbol(box, name, symbol, aspect, path, icon));
			ygoSymbolLayout.Controls.Add(box);

		}

		//Get image path and display card
		private void YGO_OnClickSearchSymbol(int index) {
			if (imageFileDialog.ShowDialog() == DialogResult.OK) {
				string curDir = Directory.GetCurrentDirectory();
				string filePath = imageFileDialog.FileName;
				if (!filePath.Contains(curDir)) { return; }
				filePath = filePath.Remove(filePath.IndexOf(curDir), curDir.Length + 1);
				ygoFormSymbols[index].pathLabel.Text = filePath;
				YGO_Utils.TryLoadImage(ygoFormSymbols[index].iconBox, filePath);
			}
		}

		//Save current symbol list
		private void YGO_OnClickSaveSymbols(object sender, EventArgs e) {
			ygoCatalog.symbols.Clear();
			foreach (YGO_FormSymbol symbol in ygoFormSymbols) {
				ygoCatalog.symbols.Add(
					new YGO_Symbol(
						symbol.nameBox.Text,
						symbol.symbolBox.Text,
						symbol.pathLabel.Text,
						symbol.aspectBox.Value
					)
				);
			}
		}

		*/

		#endregion

		#region Set Generator

		//Properties
		public int ygoSetGeneratorPagenum = 0;
		public int ygoSetGeneratorSetsPage = 25;
		private List<YGO_FormSet> ygoFormSets = new List<YGO_FormSet>();

		//Paging
		private void YGO_OnClickSetGeneratorPrev(object sender, EventArgs e) {
			--ygoSetGeneratorPagenum;
			YGO_RegenerateSets();
		}
		private void YGO_OnClickSetGeneratorNext(object sender, EventArgs e) {
			++ygoSetGeneratorPagenum;
			YGO_RegenerateSets();
		}

		//Symbol form class
		private class YGO_FormSet {
			public YGO_Set set;
			public GroupBox box;
			public TextBox nameBox;
			public TextBox codeBox;
			public DateTimePicker dateBox;
			public Label pathLabel;
			public PictureBox iconBox;
			public YGO_FormSet() : this(null, null, null, null, null, null, null) { }
			public YGO_FormSet(YGO_Set set, GroupBox box, TextBox nameBox, TextBox codeBox, DateTimePicker dateBox, Label pathLabel, PictureBox iconBox) {
				this.set = set;
				this.box = box;
				this.nameBox = nameBox;
				this.codeBox = codeBox;
				this.dateBox = dateBox;
				this.pathLabel = pathLabel;
				this.iconBox = iconBox;
			}
		}

		//Regenerate symbol controls
		private void YGO_RegenerateSets(object sender, EventArgs e) => YGO_RegenerateSets();
		private void YGO_RegenerateSets() {

			//Quick null check
			if (ygoCatalog.sets == null) { ygoCatalog.sets = new List<YGO_Set>(); }

			//Pagination
			int maxPage = ygoCatalog.sets.Count / ygoSetGeneratorSetsPage;
			if (ygoSetGeneratorPagenum > maxPage) { ygoSetGeneratorPagenum = 0; }
			if (ygoSetGeneratorPagenum < 0) { ygoSetGeneratorPagenum = maxPage; }
			int startIndex = ygoSetGeneratorPagenum * ygoSetGeneratorSetsPage;
			int maxIndex = ygoSetGeneratorSetsPage;
			if (ygoSetGeneratorPagenum == maxPage) { maxIndex = ygoCatalog.sets.Count % ygoSetGeneratorSetsPage; }
			maxIndex += startIndex;
			ygoSetGeneratorPageLabel.Text = (ygoSetGeneratorPagenum + 1) + " / " + (maxPage + 1);

			//Remove old
			ygoSetGeneratorLayout.SuspendLayout();
			foreach (YGO_FormSet set in ygoFormSets) { ygoSetGeneratorLayout.Controls.Remove(set.box); }
			ygoFormSets.Clear();

			//Add new
			for (int i = startIndex; i < maxIndex; ++i) {
				YGO_Set set = ygoCatalog.sets[i];
				YGO_CreateSetGeneratorBox(set);
			}
			ygoSetGeneratorLayout.ResumeLayout();

		}

		//Add new symbol
		private void YGO_OnClickAddSet(object sender, EventArgs e) => YGO_CreateSetGeneratorBox();
		private void YGO_CreateSetGeneratorBox() => YGO_CreateSetGeneratorBox(new YGO_Set(), false);
		private void YGO_CreateSetGeneratorBox(YGO_Set refSet, bool useRef = true) {

			//Index
			int i = ygoFormSets.Count;

			//Group box
			GroupBox box = new GroupBox();
			box.Size = new Size(350, 155);

			//Symbol box
			TextBox name = new TextBox();
			box.Controls.Add(name);
			name.Location = new Point(5, 15);
			name.Size = new Size(270, 30);
			name.Text = useRef ? refSet.name : "Name";

			//Name box
			TextBox code = new TextBox();
			box.Controls.Add(code);
			code.Location = new Point(5, 50);
			code.Size = new Size(270, 30);
			code.Text = useRef ? refSet.code : "Code";

			//Date box
			DateTimePicker date = new DateTimePicker();
			box.Controls.Add(date);
			date.Location = new Point(5, 85);
			date.Size = new Size(340, 30);
			date.Value = useRef ? refSet.date : DateTime.Now;

			//Path label
			Label path = new Label();
			box.Controls.Add(path);
			path.Location = new Point(5, 120);
			path.Size = new Size(285, 30);
			path.TextAlign = ContentAlignment.MiddleLeft;

			//Icon box
			PictureBox icon = new PictureBox();
			box.Controls.Add(icon);
			icon.Location = new Point(280, 15);
			icon.Size = new Size(65, 65);
			icon.SizeMode = PictureBoxSizeMode.StretchImage;
			icon.Cursor = Cursors.Hand;

			//Load image if one is referenced
			if (refSet.imgPath.Length > 0) {
				path.Text = refSet.imgPath;
				YGO_Utils.TryLoadImage(icon, refSet.imgPath);
			}

			//Create object and set up search event
			YGO_FormSet set = new YGO_FormSet(useRef ? refSet : null, box, name, code, date, path, icon);
			icon.Click += new EventHandler((sender, e) => YGO_OnClickSearchSetIcon(set));

			//Add to list and layout
			ygoFormSets.Add(set);
			ygoSetGeneratorLayout.Controls.Add(box);

		}

		//Get image path and display card
		private void YGO_OnClickSearchSetIcon(YGO_FormSet set) {
			if (imageFileDialog.ShowDialog() == DialogResult.OK) {
				string curDir = Directory.GetCurrentDirectory();
				string filePath = imageFileDialog.FileName;
				if (!filePath.Contains(curDir)) { return; }
				filePath = filePath.Remove(filePath.IndexOf(curDir), curDir.Length + 1);
				set.pathLabel.Text = filePath;
				YGO_Utils.TryLoadImage(set.iconBox, filePath);
			}
			else {
				set.pathLabel.Text = "";
				set.iconBox.Image = null;
			}
		}

		//Save current symbol list
		private void YGO_OnClickSaveSets(object sender, EventArgs e) {
			foreach (YGO_FormSet formSet in ygoFormSets) {
				YGO_Set set = new YGO_Set(
					formSet.nameBox.Text,
					formSet.codeBox.Text,
					formSet.pathLabel.Text,
					formSet.dateBox.Value
				);
				if (formSet.set == null) { ygoCatalog.sets.Add(set); }
				else { formSet.set.Copy(set); }
			}
			YGO_UpdateSets();
			ygoSetField.Items.Clear();
			ygoSearchSetField.Items.Clear();
			ygoSearchSetField.Items.Add("--");
			foreach (YGO_Set set in ygoCatalog.sets) {
				ygoSetField.Items.Add(set);
				ygoSearchSetField.Items.Add(set);
			}
		}

		#endregion

		#region I/O

		//Save catalog data
		private void YGO_OnClickSave(object sender, EventArgs e) => YGO_SaveCatalog();
		private void YGO_SaveCatalog() {

			//Serialize catalog to file
			ygoCatalog.SavePrintRefs();
			using (Stream stream = File.Open("resources/ygo/catalog.bin", FileMode.Create))
				Serializer.Serialize(stream, ygoCatalog);

			//Log
			ygoIODialog.Text = "Catalog saved";

		}

		//Load catalog from file
		private void YGO_LoadCatalog() {

			//Deserialize catalog from file
			try {
				using (Stream stream = File.Open("resources/ygo/catalog.bin", FileMode.Open))
					ygoCatalog = Serializer.Deserialize<YGO_Catalog>(stream);
				ygoCatalog.LoadPrintRefs();
			}
			catch (Exception ex) {
				ygoIODialog.Text = "Error: " + ex.Message;
				return;
			}

			//Generate symbol controls
			//YGO_RegenerateSymbols();
			YGO_RegenerateSets();

			//Update lists
			YGO_InitLists();
			YGO_UpdateSets();
			YGO_UpdateCardList();

			//Log
			ygoIODialog.Text = "Catalog loaded";

		}

		#endregion

	}
}
