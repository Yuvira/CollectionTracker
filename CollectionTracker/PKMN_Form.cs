using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CollectionTracker {
	public partial class Form1 : Form {

		//Properties
		public PKMN_Catalog pkmnCatalog;
		public List<string> pkmnTreatments;
		public List<PKMN_Printing> pkmnPrintFilter;
		public PKMN_Card pkmnUpdateCard = null;
		public PKMN_Printing pkmnUpdatePrint = null;

		#region Init

		//Initialize
		public void PKMN_Initialize() {
			pkmnTreatments = new List<string>();
			pkmnCatalog = new PKMN_Catalog();
			pkmnPrintFilter = new List<PKMN_Printing>();
			pkmnDetailBoxes = new List<GroupBox>();
			pkmnNameField.LostFocus += new EventHandler((sender, e) => PKMN_CheckCardNameExists());
			PKMN_Utils.TryLoadCardImage(pkmnPrintImgboxBack, PKMN_Utils.CARD_BACK_PATH);
			pkmnClient.DefaultRequestHeaders.Add("User-Agent", "CollectionTracker");
			pkmnClient.DefaultRequestHeaders.Add("Accept", "application/json");
			PKMN_LoadCatalog();
		}

		//Re-initialize lists
		public void PKMN_InitLists() {
			pkmnSetField.Items.Clear();
			pkmnRarityField.Items.Clear();
			pkmnTreatmentField.Items.Clear();
			pkmnMoveField.Items.Clear();
			foreach (PKMN_Set set in pkmnCatalog.sets)
				pkmnSetField.Items.Add(set);
			foreach (string name in pkmnCatalog.printings.Select(p => p.rarity).Distinct())
				pkmnRarityField.Items.Add(name);
			foreach (string name in pkmnCatalog.printings.SelectMany(p => p.treatments).Select(t => t.name).Distinct())
				pkmnTreatmentField.Items.Add(name);
			foreach (string name in pkmnCatalog.printings.SelectMany(p => p.treatments).SelectMany(t => t.locations).Distinct())
				pkmnMoveField.Items.Add(name);
			PKMN_ReloadSearchLists();
		}

		#endregion

		#region Set List

		//Properties
		private List<(PKMN_Set set, GroupBox box, bool expanded)> pkmnSetlist = new List<(PKMN_Set, GroupBox, bool)>();
		public int pkmnSetPagenum = 0;
		public int pkmnSetsPerPage = 15;

		//Paging
		private void PKMN_OnClickPrevSet(object sender, EventArgs e) {
			--pkmnSetPagenum;
			PKMN_UpdateSets();
		}
		private void PKMN_OnClickNextSet(object sender, EventArgs e) {
			++pkmnSetPagenum;
			PKMN_UpdateSets();
		}

		//Clear set list and update data
		private void PKMN_UpdateSets() {

			//Clear controls and sort sets
			pkmnSetlist.Clear();
			pkmnSetlistLayout.Controls.Clear();
			pkmnCatalog.sets.Sort(new PKMN_SetComparer().Compare);

			//Pagination
			int maxPage = pkmnCatalog.sets.Count / pkmnSetsPerPage;
			if (pkmnSetPagenum > maxPage) { pkmnSetPagenum = 0; }
			if (pkmnSetPagenum < 0) { pkmnSetPagenum = maxPage; }
			int startIndex = pkmnSetPagenum * pkmnSetsPerPage;
			int maxIndex = pkmnSetsPerPage;
			if (pkmnSetPagenum == maxPage) { maxIndex = pkmnCatalog.sets.Count % pkmnSetsPerPage; }
			maxIndex += startIndex;
			pkmnSetPageLabel.Text = (pkmnSetPagenum + 1) + " / " + (maxPage + 1);

			//Create set rows
			for (int i = startIndex; i < maxIndex; ++i) { PKMN_CreateSetRow(pkmnCatalog.sets[i]); }

		}

		//Generate set info
		private void PKMN_CreateSetRow(PKMN_Set set) {

			//Important values
			List<PKMN_Printing> cardsInSet = pkmnCatalog.printings.Where(print => print.set == set).ToList();
			int setCount = cardsInSet.Count;
			int setOwned = cardsInSet.Count(print => print.AnyOwned());
			bool missingCardref = cardsInSet.Count(print => print.card.name.Equals("") || print.card.name.Equals("_")) > 0;

			//Set info box
			GroupBox box = new GroupBox();
			pkmnSetlistLayout.Controls.Add(box);
			box.Size = new Size(820, 70);

			//Filter button
			Button filter = new Button();
			box.Controls.Add(filter);
			filter.Location = new Point(5, 15);
			filter.Size = new Size(350, 50);
			filter.Text = set.name;
			filter.UseVisualStyleBackColor = true;
			if (set.imgPath.Length > 0) { filter.Image = new Bitmap(Image.FromFile(set.imgPath), new Size(35, 35)); }
			filter.TextImageRelation = TextImageRelation.ImageBeforeText;
			filter.ImageAlign = ContentAlignment.MiddleRight;
			filter.TextAlign = ContentAlignment.MiddleCenter;
			filter.Click += new EventHandler((sender, e) => PKMN_FilterCatalogBySet(set));

			//Progress label
			Label label = new Label();
			box.Controls.Add(label);
			label.Location = new Point(360, 15);
			label.Size = new Size(100, 50);
			label.Text = setOwned.ToString() + '/' + setCount.ToString();
			label.TextAlign = ContentAlignment.MiddleCenter;

			//Progress bar
			ProgressBar bar = new ProgressBar();
			box.Controls.Add(bar);
			bar.Location = new Point(470, 25);
			bar.Size = new Size(265, 30);
			if (setCount > 0) { bar.Value = (int)(((float)setOwned / setCount) * 100); }

			//Missing cardref label
			if (missingCardref) {
				Label cardrefLabel = new Label();
				box.Controls.Add(cardrefLabel);
				cardrefLabel.Location = new Point(780, 15);
				cardrefLabel.Size = new Size(35, 50);
				cardrefLabel.Text = "*";
				cardrefLabel.TextAlign = ContentAlignment.MiddleCenter;
			}

			//Add to list
			pkmnSetlist.Add((set, box, false));

		}

		//Expand or collapse set box
		private void PKMN_ExpandCollapseSet(PKMN_Set set) {
			for (int i = 0; i < pkmnSetlist.Count; ++i) {
				(PKMN_Set set, GroupBox box, bool expanded) listSet = pkmnSetlist[i];
				if (listSet.set == set) {
					foreach ((PKMN_Set set, GroupBox box, bool expanded) listSet2 in pkmnSetlist) {
						if (listSet2.set != listSet.set && listSet2.set.date.Date == set.date.Date) {
							if (listSet.expanded) { listSet2.box.Hide(); }
							else { listSet2.box.Show(); }
						}
					}
					pkmnSetlist[i] = (listSet.set, listSet.box, !listSet.expanded);
					break;
				}
			}
		}

		//Filter catalog by set ID
		private void PKMN_FilterCatalogBySet(PKMN_Set set) {
			pkmnPrintFilter.Clear();
			foreach (PKMN_Printing print in pkmnCatalog.printings) {
				if (print.set == set) {
					pkmnPrintFilter.Add(print);
				}
			}
			if (pkmnSortMode) { pkmnPrintFilter.Sort(new PKMN_PrintComparerNumeric().Compare); }
			else { pkmnPrintFilter.Sort(new PKMN_PrintComparerAlphabetical().Compare); }
			pkmnCatalogPagenum = 0;
			PKMN_UpdateCatalog();
			pkmnTabControl.SelectedTab = pkmnCatalogPage;
		}

		#endregion

		#region Search

		//Refresh search lists
		private void PKMN_OnClickReloadSearchLists(object sender, EventArgs e) => PKMN_ReloadSearchLists();
		private void PKMN_ReloadSearchLists() {
			pkmnSearchTypeList.Items.Clear();
			pkmnSearchTypeList.Items.Add("--");
			pkmnSearchSetField.Items.Clear();
			pkmnSearchSetField.Items.Add("--");
			pkmnSearchLocationField.Items.Clear();
			pkmnSearchLocationField.Items.Add("--");
			foreach (string name in pkmnCatalog.cards.SelectMany(c => c.cardTypes.Split(new string[] { " / " }, StringSplitOptions.None)).Distinct())
				pkmnSearchTypeList.Items.Add(name);
			foreach (PKMN_Set set in pkmnCatalog.sets)
				pkmnSearchSetField.Items.Add(set);
			foreach (string name in pkmnCatalog.printings.SelectMany(p => p.treatments).SelectMany(t => t.locations).Distinct())
				pkmnSearchLocationField.Items.Add(name);
		}

		//Check if string contains all in a given array of substrings
		private bool PKMN_CheckSubstring(string s, string[] arr) {
			foreach (string ss in arr) {
				if (!s.ToLower().Contains(ss.ToLower())) {
					return false;
				}
			}
			return true;
		}

		//Add selected type to type field
		private void PKMN_OnSearchTypeChanged(object sender, EventArgs e) {
			string s = pkmnSearchTypeList.SelectedItem?.ToString() ?? "";
			if (s.Equals("") || s.Equals("--"))
				return;
			if (!pkmnSearchTypeField.Text.Equals(""))
				pkmnSearchTypeField.Text += '|';
			pkmnSearchTypeField.Text += s;
		}

		//Apply advanced search terms to search bar
		private void PKMN_OnClickApplySearchTerms(object sender, EventArgs e) {

			//Clear search
			pkmnSearchField.Text = "";

			//Apply terms
			if (pkmnSearchNameField.Text.Length > 0)
				PKMN_AddSearchTerm("n:" + pkmnSearchNameField.Text);
			if (pkmnSearchTypeField.Text.Length > 0)
				foreach (string term in pkmnSearchTypeField.Text.Split('|'))
					PKMN_AddSearchTerm("t=" + pkmnSearchTypeField.Text);
			if (pkmnSearchOracleField.Text.Length > 0)
				PKMN_AddSearchTerm("o:" + pkmnSearchOracleField.Text);
			if (pkmnSearchSetField.SelectedItem is PKMN_Set set)
				PKMN_AddSearchTerm("s=" + set.code);
			string loc = pkmnSearchLocationField.SelectedItem?.ToString() ?? "";
			if (!loc.Equals("") && !loc.Equals("--"))
				PKMN_AddSearchTerm("l=" + loc);

		}

		//Add term to search field
		private void PKMN_AddSearchTerm(string term) {
			if (pkmnSearchField.Text.Length > 0)
				pkmnSearchField.Text += '&';
			pkmnSearchField.Text += term;
		}

		//Search for card matching given criteria
		private void PKMN_OnClickSearch(object sender, EventArgs e) {

			//Check for any input
			if (pkmnSearchField.Text.Length <= 0) {
				pkmnSearchDialog.Text = "No search terms provided!";
				return;
			}

			//Get logical operation type
			char logicalOp = '|';
			if (pkmnSearchField.Text.Contains('&'))
				logicalOp = '&';
			if (pkmnSearchField.Text.Contains('|')) {
				if (logicalOp == '&') {
					pkmnSearchDialog.Text = "Mixed and/or operators provided!";
					return;
				}
			}

			//Clear print filter
			pkmnPrintFilter.Clear();
			pkmnPrintFilter = new List<PKMN_Printing>();

			//Split terms into array
			string[] termArray;
			if (logicalOp == '&')
				termArray = pkmnSearchField.Text.Split('&');
			else
				termArray = pkmnSearchField.Text.Split('|');

			//Convert array of terms into list of (field, operator, value)
			List<(string field, string op, string value)> terms = new List<(string, string, string)>();
			List<string> ops = new List<string> { "!:", "!=", "~:", "~=", "==", ":", "=" };
			foreach (string term in termArray) {
				string[] splitTerm;
				foreach (string op in ops) {
					splitTerm = term.Split(new string[] { op }, StringSplitOptions.None);
					if (splitTerm.Length != 2)
						continue;
					terms.Add((splitTerm[0], op, splitTerm[1]));
					break;
				}
			}

			//Search printings
			bool[] matches = new bool[terms.Count];
			string searchField;
			foreach (PKMN_Printing print in pkmnCatalog.printings) {
				for (int i = 0; i < terms.Count; ++i) {
					if (terms[i].field.ToLower().Equals("n"))
						searchField = print.card.name;
					else if (terms[i].field.ToLower().Equals("t"))
						searchField = print.card.cardTypes;
					else if (terms[i].field.ToLower().Equals("o"))
						searchField = print.card.oracleText;
					else if (terms[i].field.ToLower().Equals("s"))
						searchField = print.set.code;
					else if (terms[i].field.ToLower().Equals("l"))
						searchField = string.Join(" / ", print.treatments.SelectMany(t => t.locations).Distinct());
					else
						continue;
					bool fieldIsList = false;
					if (terms[i].field.ToLower().Equals("t") || terms[i].field.ToLower().Equals("l"))
						fieldIsList = true;
					matches[i] = Utils.EvaluateSearchOperation(searchField, terms[i].op, terms[i].value, fieldIsList);
				}
				if (logicalOp == '&' && !matches.Contains(false))
					pkmnPrintFilter.Add(print);
				else if (logicalOp == '|' && matches.Contains(true))
					pkmnPrintFilter.Add(print);
			}

			//Show catalog
			if (pkmnSortMode) { pkmnPrintFilter.Sort(new PKMN_PrintComparerNumeric().Compare); }
			else { pkmnPrintFilter.Sort(new PKMN_PrintComparerAlphabetical().Compare); }
			pkmnCatalogPagenum = 0;
			PKMN_UpdateCatalog();
			pkmnTabControl.SelectedTab = pkmnCatalogPage;

		}

		//Clipboard buttons
		private void PKMN_OnClickClipboardexButton(object sender, EventArgs e) => Clipboard.SetText("𝑒𝑥");
		private void PKMN_OnClickClipboardStarButton(object sender, EventArgs e) => Clipboard.SetText("☆");
		private void PKMN_OnClickClipboardDeltaButton(object sender, EventArgs e) => Clipboard.SetText("δ");
		private void PKMN_OnClickClipboardSPButton(object sender, EventArgs e) => Clipboard.SetText("𝘚𝘗");
		private void PKMN_OnClickClipboardPrismButton(object sender, EventArgs e) => Clipboard.SetText("◇");

		#endregion

		#region Catalog

		//Properties
		public int pkmnCatalogPagenum = 0;
		public int pkmnCardsPerPage = 50;
		public bool pkmnSortMode = true;

		//Sort buttons
		private void PKMN_OnClickSortAlphabetical(object sender, EventArgs e) => PKMN_UpdateSortMode(false);
		private void PKMN_OnClickSortNumeric(object sender, EventArgs e) => PKMN_UpdateSortMode(true);
		private void PKMN_UpdateSortMode(bool mode) {
			pkmnSortMode = mode;
			if (pkmnSortMode) { pkmnPrintFilter.Sort(new PKMN_PrintComparerNumeric().Compare); }
			else { pkmnPrintFilter.Sort(new PKMN_PrintComparerAlphabetical().Compare); }
			pkmnCatalogPagenum = 0;
			PKMN_UpdateCatalog();
			pkmnTabControl.SelectedTab = pkmnCatalogPage;
		}

		//Paging
		private void PKMN_OnClickCatalogPrev(object sender, EventArgs e) {
			--pkmnCatalogPagenum;
			PKMN_UpdateCatalog();
		}
		private void PKMN_OnClickCatalogNext(object sender, EventArgs e) {
			++pkmnCatalogPagenum;
			PKMN_UpdateCatalog();
		}

		//Clear catalog and generate new cards
		private void PKMN_UpdateCatalog() {

			//Remove and clear layout controls
			pkmnCatalogPage.Controls.Remove(pkmnCatalogLayout);
			pkmnCatalogLayout.Controls.Clear();

			//Pagination
			int maxPage = pkmnPrintFilter.Count / pkmnCardsPerPage;
			if (pkmnCatalogPagenum > maxPage) { pkmnCatalogPagenum = 0; }
			if (pkmnCatalogPagenum < 0) { pkmnCatalogPagenum = maxPage; }
			int startIndex = pkmnCatalogPagenum * pkmnCardsPerPage;
			int maxIndex = pkmnCardsPerPage;
			if (pkmnCatalogPagenum == maxPage) { maxIndex = pkmnPrintFilter.Count % pkmnCardsPerPage; }
			maxIndex += startIndex;

			//Header
			pkmnCatalogIndex.Text = $"Showing {startIndex + 1} - {maxIndex} of {pkmnPrintFilter.Count}";

			//Loop printings
			for (int i = startIndex; i < maxIndex; ++i) {

				//Get index/printing
				PKMN_Printing print = pkmnPrintFilter[i];

				//Card box
				GroupBox box = new GroupBox();
				pkmnCatalogLayout.Controls.Add(box);
				box.Location = new Point(3, 3);
				box.Size = new Size(300, 430 + (30 * print.treatments.Count));
				if (!print.AnyOwned()) { box.BackColor = SystemColors.ControlDarkDark; }
				box.SuspendLayout();

				//Image box
				PictureBox img = new PictureBox();
				box.Controls.Add(img);
				img.BorderStyle = BorderStyle.Fixed3D;
				img.SizeMode = PictureBoxSizeMode.StretchImage;
				img.Location = new Point(0, 0);
				img.Size = new Size(300, 420);
				PKMN_Utils.TryLoadCardImage(img, print.imgPath);
				img.Click += new EventHandler((sender, e) => PKMN_LoadCardDetails(print));

				//Loop treatments
				for (int j = 0; j < print.treatments.Count; ++j) {

					//Get treatment at index
					PKMN_Treatment treatment = print.treatments[j];

					//Treatment label
					Label treatmentLabel = new Label();
					box.Controls.Add(treatmentLabel);
					treatmentLabel.Location = new Point(60, 425 + (j * 30));
					treatmentLabel.Size = new Size(120, 30);
					treatmentLabel.Text = treatment.name;
					treatmentLabel.AutoEllipsis = true;
					treatmentLabel.TextAlign = ContentAlignment.MiddleRight;

					//Count label
					Label label = new Label();
					box.Controls.Add(label);
					label.Location = new Point(180, 425 + (j * 30));
					label.Size = new Size(60, 30);
					label.Text = print.OwnedCountOfTreatment(treatment.name).ToString();
					label.TextAlign = ContentAlignment.MiddleLeft;

					//Decrement
					Button leftButton = new Button();
					box.Controls.Add(leftButton);
					leftButton.Location = new Point(5, 425 + (j * 30));
					leftButton.Size = new Size(55, 29);
					leftButton.Text = "<";
					leftButton.UseVisualStyleBackColor = true;
					leftButton.Click += new EventHandler((sender, e) => PKMN_DecrementCardCount(box, label, print, treatment.name));

					//Increment
					Button rightButton = new Button();
					box.Controls.Add(rightButton);
					rightButton.Location = new Point(240, 425 + (j * 30));
					rightButton.Size = new Size(55, 29);
					rightButton.Text = ">";
					rightButton.UseVisualStyleBackColor = true;
					rightButton.Click += new EventHandler((sender, e) => PKMN_IncrementCardCount(box, label, print, treatment.name));

				}

				//Resume
				box.ResumeLayout();

			}
			pkmnCatalogPage.Controls.Add(pkmnCatalogLayout);
		}

		//Increment card quantity
		private void PKMN_IncrementCardCount(GroupBox box, Label label, PKMN_Printing print, string treatment) {
			if (print != null) {
				print.Increment(treatment);
				label.Text = print.OwnedCountOfTreatment(treatment).ToString();
				if (!print.AnyOwned()) { box.BackColor = SystemColors.ControlDarkDark; }
				else { box.BackColor = SystemColors.ControlDark; }
			}
			else { label.Text = "Print is null!"; }
		}

		//Decrement card quantity
		private void PKMN_DecrementCardCount(GroupBox box, Label label, PKMN_Printing print, string treatment) {
			if (print != null) {
				print.Decrement(treatment);
				label.Text = print.OwnedCountOfTreatment(treatment).ToString();
				if (!print.AnyOwned()) { box.BackColor = SystemColors.ControlDarkDark; }
				else { box.BackColor = SystemColors.ControlDark; }
			}
			else { label.Text = "Print is null!"; }
		}

		#endregion

		#region Card Details

		//Properties
		public bool pkmnDetailFlipped = false;
		public List<GroupBox> pkmnDetailBoxes;
		public PKMN_Printing pkmnDetailPrint = null;
		public PKMN_Printing pkmnDetailPrev = null;
		public PKMN_Printing pkmnDetailNext = null;

		//Load card data into details tab
		private void PKMN_LoadCardDetails(PKMN_Printing print) {

			//Set persistent reference
			pkmnDetailPrint = print;

			//Set image
			PKMN_Utils.TryLoadCardImage(pkmnDetailImgbox, print.imgPath);
			pkmnDetailFlipped = false;

			//Set number in filter
			pkmnDetailFilterCountLabel.Text = (pkmnPrintFilter.IndexOf(pkmnDetailPrint) + 1).ToString() + " / " + pkmnPrintFilter.Count.ToString();

			//Get card reference
			PKMN_Card card = print.card;

			//Clear old boxes
			foreach (GroupBox gbox in pkmnDetailBoxes) { pkmnDetailPage.Controls.Remove(gbox); }
			pkmnDetailBoxes.Clear();

			//Y position to create elements at
			int y = 5;

			//Box-relative Y position
			int y2 = 20;

			//Header box
			GroupBox headerBox = new GroupBox();
			pkmnDetailPage.Controls.Add(headerBox);
			headerBox.Size = new Size(pkmnDetailBox.Size.Width, 100);

			//Name
			PKMN_WriteLineWithSymbols(card.name, headerBox, new Point(5, y2), Utils.FONT_BOLD);

			//Energy type & HP
			if (card.energyType.Length > 0 || card.hp > 0) {

				//Value and width data
				List<string> symbols = PKMN_GetSymbols(card.energyType);
				string hpText = card.hp.ToString() + " HP";
				int symbolWidth = (symbols.Count * TEXT_HEIGHT) + 10;
				int hpWidth = TextRenderer.MeasureText(hpText, Utils.FONT_BOLD).Width;

				//Draw HP label
				Label hp = new Label();
				headerBox.Controls.Add(hp);
				hp.Font = Utils.FONT_BOLD;
				hp.Location = new Point(pkmnDetailBox.Size.Width - (symbolWidth + hpWidth), y2);
				hp.Size = new Size(hpWidth, TEXT_HEIGHT);
				hp.Text = hpText;
				hp.TextAlign = ContentAlignment.MiddleLeft;

				//Draw symbols
				Point location = new Point(pkmnDetailBox.Size.Width - symbolWidth, y2);
				foreach (string symbol in symbols)
					location = PKMN_InsertSymbol(symbol, headerBox, location, TEXT_HEIGHT);

			}

			//Next line
			y2 += TEXT_HEIGHT + 10;

			//Card Type
			if (card.cardTypes.Length > 0) {
				PKMN_WriteLineWithSymbols(card.cardTypes, headerBox, new Point(5, y2), Utils.FONT_DEFAULT);
				y2 += TEXT_HEIGHT + 10;
			}

			//Pokemon stage
			if (card.stage.Length > 0) {
				PKMN_WriteLineWithSymbols(card.stage, headerBox, new Point(5, y2), Utils.FONT_DEFAULT);
				y2 += TEXT_HEIGHT + 10;
			}

			//Size box and set position for next
			y2 += 5;
			headerBox.Location = new Point(pkmnDetailBox.Location.X, y);
			headerBox.Size = new Size(pkmnDetailBox.Size.Width, y2);
			pkmnDetailBoxes.Add(headerBox);
			y += y2;

			//Oracle box
			y2 = 20;
			GroupBox oracleBox = new GroupBox();
			pkmnDetailPage.Controls.Add(oracleBox);
			oracleBox.Size = new Size(pkmnDetailBox.Size.Width, 100);

			//Oracle Text
			if (card.oracleText.Length > 0)
				y2 += 10 + PKMN_GenerateDescription(PKMN_AddAbilityMarkers(card.oracleText), oracleBox, new Point(5, y2));

			//Size box and set position for next
			y2 += 5;
			oracleBox.Location = new Point(pkmnDetailBox.Location.X, y);
			oracleBox.Size = new Size(pkmnDetailBox.Size.Width, y2);
			pkmnDetailBoxes.Add(oracleBox);
			y += y2;

			//Generate footer only if we have data for it
			if (card.weakness.Length > 0 || card.resistance.Length > 0 || card.retreatCost.Length > 0) {

				//Footer box
				y2 = 20;
				GroupBox footerBox = new GroupBox();
				pkmnDetailPage.Controls.Add(footerBox);
				footerBox.Size = new Size(pkmnDetailBox.Size.Width, 100);

				//Weakness
				if (card.weakness.Length > 0)
					y2 += 10 + PKMN_GenerateDescription("Weakness: " + card.weakness, footerBox, new Point(5, y2));

				//Resistance
				if (card.resistance.Length > 0)
					y2 += 10 + PKMN_GenerateDescription("Resistance: " + card.resistance, footerBox, new Point(5, y2));

				//Retreat
				if (card.retreatCost.Length > 0)
					y2 += 10 + PKMN_GenerateDescription("Retreat: " + card.retreatCost, footerBox, new Point(5, y2));

				//Size box and set position for next
				y2 += 5;
				footerBox.Location = new Point(pkmnDetailBox.Location.X, y);
				footerBox.Size = new Size(pkmnDetailBox.Size.Width, y2);
				pkmnDetailBoxes.Add(footerBox);
				y += y2;

			}

			//Generate flavor text box only if we have flavor text
			if (print.flavorText.Length > 0) {

				//Flavor box
				y2 = 20;
				GroupBox flavorBox = new GroupBox();
				pkmnDetailPage.Controls.Add(flavorBox);
				flavorBox.Size = new Size(pkmnDetailBox.Size.Width, 100);

				Label flavor = new Label();
				flavorBox.Controls.Add(flavor);
				flavor.Font = Utils.FONT_ITALIC;
				flavor.Location = new Point(5, y2);
				flavor.AutoSize = true;
				flavor.MaximumSize = new Size(pkmnDetailBox.Size.Width - 10, 0);
				flavor.Text = print.flavorText;
				flavor.TextAlign = ContentAlignment.MiddleLeft;
				y2 += 5 + flavor.Size.Height;

				//Size box and set position for next
				y2 += 5;
				flavorBox.Location = new Point(pkmnDetailBox.Location.X, y);
				flavorBox.Size = new Size(pkmnDetailBox.Size.Width, y2);
				pkmnDetailBoxes.Add(flavorBox);
				y += y2;

			}

			//Load location table
			y += 10;
			pkmnDetailBox.Location = new Point(pkmnDetailBox.Location.X, y);
			PKMN_LoadLocationTable(print);

			//Load printing list
			PKMN_LoadPrintingsList(print.card, print);

			//Nav buttons
			int idx = pkmnPrintFilter.IndexOf(print);
			if (idx == -1) {
				pkmnDetailPrevButton.Hide();
				pkmnDetailNextButton.Hide();
				return;
			}
			if (idx > 0)
				pkmnDetailPrev = pkmnPrintFilter[idx - 1];
			else
				pkmnDetailPrev = pkmnPrintFilter[pkmnPrintFilter.Count - 1];
			if (idx < pkmnPrintFilter.Count - 1)
				pkmnDetailNext = pkmnPrintFilter[idx + 1];
			else
				pkmnDetailNext = pkmnPrintFilter[0];
			pkmnDetailPrevButton.Show();
			pkmnDetailPrevButton.Text = pkmnDetailPrev.card.name;
			pkmnDetailNextButton.Show();
			pkmnDetailNextButton.Text = pkmnDetailNext.card.name;

			//Hide tooltip
			pkmnTooltipBox.Hide();
			pkmnCardtipBox.Hide();

			//Set tab
			pkmnTabControl.SelectedTab = pkmnDetailPage;

		}

		#region Description Generators

		//Write string and replace symbols
		private void PKMN_WriteLineWithSymbols(string str, GroupBox box, Point location, Font font) {

			//Loop fields
			while (true) {

				//Clear leading spaces
				if (str.StartsWith(" ")) { str = str.Substring(1); }

				//Get index of next symbol
				int i = str.IndexOf('{');

				//Generate object
				if (i == 0) {
					int i2 = str.IndexOf('}');
					if (i2 > 0) {
						location = PKMN_InsertSymbol(str.Substring(0, i2 + 1), box, location, TEXT_HEIGHT);
						str = str.Substring(i2 + 1);
					}
					else { str = str.Substring(1); }
				}

				//Write text
				else {
					if (i == -1) {
						PKMN_WriteLine(str, box, location, font);
						break;
					}
					else {
						string substr = str.Substring(0, i);
						if (substr.EndsWith(" ")) { substr = substr.Substring(0, substr.Length - 1); }
						location = PKMN_WriteLine(substr, box, location, font);
						str = str.Substring(i);
					}
				}

			}

		}

		//Write simple text
		private Point PKMN_WriteLine(string str, GroupBox box, Point location, Font font) {
			Label label = new Label();
			box.Controls.Add(label);
			label.Font = font;
			label.Location = location;
			label.Size = new Size(TextRenderer.MeasureText(str, label.Font).Width, TEXT_HEIGHT);
			label.Text = str;
			label.TextAlign = ContentAlignment.MiddleLeft;
			return new Point(location.X + TextRenderer.MeasureText(str, label.Font).Width, location.Y);
		}

		//Add markers to abilities so the description generator knows where to bold/colour text
		private string PKMN_AddAbilityMarkers(string desc) {
			string[] lines = desc.Split(new string[] { "\r\n" }, StringSplitOptions.None);
			for (int i = 0; i < lines.Length; ++i)
				if (lines[i].StartsWith("{"))
					lines[i] = lines[i].Insert(lines[i].LastIndexOf('}') + 2, "`");
			return string.Join("\r\n", lines);
		}

		//Generate description box. Returns total height of the description field
		private int PKMN_GenerateDescription(string desc, GroupBox box, Point location) {

			//Set initial y position and loop fields
			int y = location.Y;
			while (true) {

				//Clear leading spaces
				if (desc.StartsWith(" ")) { desc = desc.Substring(1); }

				//Get index of next object
				int i = PKMN_Utils.IndexOfMany(desc, new List<char>() { '{', '[', '<' });

				//We are at an object, generate it
				if (i == 0) {

					//Symbol
					if (desc[0] == '{') {
						int i2 = desc.IndexOf('}');
						if (i2 > 0) {
							location = PKMN_InsertSymbol(desc.Substring(0, i2 + 1), box, location, TEXT_HEIGHT);
							desc = desc.Substring(i2 + 1);
						}
						else { desc = desc.Substring(1); }
					}

					//Tooltip
					else if (desc[0] == '[') {
						int i2 = desc.IndexOf("|");
						int i3 = desc.IndexOf("]");
						if (i2 > 0 && i3 > 0) {
							location = PKMN_InsertTooltip(desc.Substring(1, i2 - 1), desc.Substring(i2 + 1, (i3 - i2) - 1), box, location);
							desc = desc.Substring(i3 + 1);
						}
						else { desc = desc.Substring(1); }
					}

					//Cardtip
					else if (desc[0] == '<') {
						int i2 = desc.IndexOf("|");
						int i3 = desc.IndexOf(">");
						if (i2 > 0 && i3 > 0) {
							location = PKMN_InsertCardtip(desc.Substring(1, i2 - 1), desc.Substring(i2 + 1, (i3 - i2) - 1), box, location);
							desc = desc.Substring(i3 + 1);
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
						location = PKMN_WriteDescription(desc, box, location);
						break;
					}

					//Write until next object, clear written text, and continue
					else {
						string substr = desc.Substring(0, i);
						if (substr.EndsWith(" ")) { substr = substr.Substring(0, substr.Length - 1); }
						location = PKMN_WriteDescription(substr, box, location);
						desc = desc.Substring(i);
					}

				}

			}

			//Return y delta
			return location.Y + TEXT_HEIGHT - y;

		}

		//Write description text. Returns new text position
		private Point PKMN_WriteDescription(string desc, GroupBox box, Point location) {

			//Loop line breaks
			string[] lines = desc.Split(new string[] { "\r\n" }, StringSplitOptions.None);
			for (int i = 0; i < lines.Length; ++i) {

				//Jump location
				if (i > 0)
					location = new Point(5, location.Y + TEXT_HEIGHT + 5);

				//Split words
				string[] words = lines[i].Split(' ');
				if (words.Length == 0) { continue; }
				int idx = 0;

				//Loop lines
				while (true) {

					//Generate label
					Label label = new Label();
					box.Controls.Add(label);

					//Get available width
					int maxWidth = box.Width - (location.X + 5);

					//If first word doesn't fit, force if it's the only word in the line or skip to the next
					if (TextRenderer.MeasureText(words[idx], label.Font).Width > maxWidth) {
						if (location.X == 5) {
							label.Location = location;
							label.Size = new Size(TextRenderer.MeasureText(words[idx], label.Font).Width, 30);
							label.Text = words[idx];
							label.TextAlign = ContentAlignment.MiddleLeft;
							location = new Point(5, location.Y + TEXT_HEIGHT);
							++idx;
							continue;
						}
						location = new Point(5, location.Y + TEXT_HEIGHT);
						maxWidth = box.Width - (location.X + 5);
					}

					//Loop words in line
					string str = words[idx];
					while (true) {

						//If we're done with our text or the next word would exceed available width, generate the label and break
						if (idx + 1 >= words.Length || TextRenderer.MeasureText(str + " " + words[idx + 1], label.Font).Width > maxWidth) {
							label.Location = location;
							if (str.StartsWith("`")) {
								label.Font = Utils.FONT_BOLD;
								str = str.Substring(1);
							}
							foreach (KeyValuePair<string, Color> kvp in PKMN_Utils.abilityTerms) {
								if (str.StartsWith(kvp.Key)) {
									label.Font = Utils.FONT_BOLD;
									label.ForeColor = kvp.Value;
									break;
								}
							}
							label.Size = new Size(TextRenderer.MeasureText(str, label.Font).Width, TEXT_HEIGHT);
							label.Text = str;
							label.TextAlign = ContentAlignment.MiddleLeft;
							if (idx + 1 >= words.Length)
								location = new Point(location.X + TextRenderer.MeasureText(str, label.Font).Width, location.Y);
							else
								location = new Point(5, location.Y + TEXT_HEIGHT);
							++idx;
							break;
						}

						//Otherwise add to string and continue
						str += " " + words[idx + 1];
						++idx;
						continue;

					}

					//Break if we're done with this line
					if (idx >= words.Length)
						break;

				}

			}

			//Return end of text
			return location;

		}

		//Get list of symbols from string
		private List<string> PKMN_GetSymbols(string str) {
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
		private Point PKMN_InsertTooltip(string str, string tooltip, Control control, Point location) {
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
			label.MouseEnter += new EventHandler((sender, e) => PKMN_ShowTooltip(label, tooltip));
			label.MouseLeave += new EventHandler((sender, e) => pkmnTooltipBox.Hide());
			location = new Point(location.X + textWidth, location.Y);
			return location;
		}

		//Insert clickable cardtip text at position. Returns position at end of added text
		private Point PKMN_InsertCardtip(string str, string cardtip, Control control, Point location) {
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
			label.Click += new EventHandler((sender, e) => PKMN_LoadCardtip(cardtip));
			label.MouseEnter += new EventHandler((sender, e) => PKMN_ShowCardtip(label, cardtip));
			label.MouseLeave += new EventHandler((sender, e) => pkmnCardtipBox.Hide());
			location = new Point(location.X + textWidth, location.Y);
			return location;
		}

		//Insert symbol into control at position. Returns position at end of symbol
		private Point PKMN_InsertSymbol(string str, Control control, Point location, int height) {

			//Find symbol object for given symbol string
			PKMN_Symbol symbol = pkmnCatalog.symbols.FirstOrDefault(s => s.symbol == str);

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
			PKMN_Utils.TryLoadImage(icon, symbol.imgPath);
			location = new Point(location.X + width, location.Y);
			return location;

		}

		#endregion

		#region Detail Utils

		//Show tooltip window relative to given control with given text
		private void PKMN_ShowTooltip(Control control, string str) {
			int posX = control.Parent.Location.X + control.Location.X + (control.Width / 2) - (pkmnTooltipBox.Width / 2);
			int posY = control.Parent.Location.Y + control.Location.Y + TEXT_HEIGHT;
			pkmnTooltipBox.Show();
			pkmnTooltipBox.BringToFront();
			pkmnTooltipBox.Location = new Point(posX, posY);
			pkmnTooltipBox.Controls.Clear();
			int height = PKMN_GenerateDescription(str, pkmnTooltipBox, new Point(5, 15));
			pkmnTooltipBox.Size = new Size(pkmnTooltipBox.Width, height + 20);
		}

		//Show tooltip window relative to given control with given text
		private void PKMN_ShowCardtip(Control control, string str) {

			//Get modifier
			char mod = ' ';
			if (str.Contains('|')) {
				mod = str[str.Length - 1];
				str = str.Substring(0, str.Length - 2);
			}

			//Load printing
			PKMN_Printing print = pkmnCatalog.printings.FirstOrDefault(p => p.printID.Equals(str));
			if (print != null) {

				//Size
				if (mod == 's' || mod == 'S') {
					pkmnCardtipBox.Size = new Size(350, 250);
					pkmnCardtipImage.Size = new Size(350, 250);
				}
				else {
					pkmnCardtipBox.Size = new Size(250, 350);
					pkmnCardtipImage.Size = new Size(250, 350);
				}

				//Position
				int posX = control.Parent.Location.X + control.Location.X + (control.Width / 2) - (pkmnCardtipBox.Width / 2);
				int posY = control.Parent.Location.Y + control.Location.Y + TEXT_HEIGHT;

				//Show
				pkmnCardtipBox.Show();
				pkmnCardtipBox.BringToFront();
				pkmnCardtipBox.Location = new Point(posX, posY);

				//Load image
				if (mod == 'b' || mod == 'B') { PKMN_Utils.TryLoadCardImage(pkmnCardtipImage, print.backImgPath); }
				else { PKMN_Utils.TryLoadCardImage(pkmnCardtipImage, print.imgPath); }

				//Rotation
				Image img = pkmnCardtipImage.Image;
				if (mod == 'u' || mod == 'U') { img.RotateFlip(RotateFlipType.Rotate180FlipNone); }
				if (mod == 's' || mod == 'S') { img.RotateFlip(RotateFlipType.Rotate90FlipNone); }

			}

		}

		//Show tooltip window relative to given control with given text
		private void PKMN_LoadCardtip(string str) {
			PKMN_Printing print = pkmnCatalog.printings.FirstOrDefault(p => p.printID.Equals(str));
			if (print != null) { PKMN_LoadCardDetails(print); }
		}

		//Load location table
		private void PKMN_LoadLocationTable(PKMN_Printing print) {

			//Clear
			pkmnDetailBox.Controls.Remove(pkmnLocationTable);
			pkmnLocationTable.Controls.Clear();
			pkmnLocationTable.RowCount = 0;
			pkmnLocationTable.RowStyles.Clear();
			pkmnLocationTable.Size = new Size(pkmnLocationTable.Size.Width, 10);

			//Loop rarities and locations
			foreach (PKMN_Treatment treatment in print.treatments) {
				for (int i = 0; i < treatment.locations.Count; ++i) {
					string rar = treatment.name;
					string loc = treatment.locations[i];
					pkmnLocationTable.Size = new Size(pkmnLocationTable.Size.Width, pkmnLocationTable.Size.Height + 35);

					//Add row
					++pkmnLocationTable.RowCount;
					pkmnLocationTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));

					//Rarity label
					Label treatmentLabel = new Label();
					pkmnLocationTable.Controls.Add(treatmentLabel, 0, pkmnLocationTable.RowCount - 1);
					treatmentLabel.Dock = DockStyle.Fill;
					treatmentLabel.TextAlign = ContentAlignment.MiddleLeft;
					treatmentLabel.Text = rar;
					treatmentLabel.AutoEllipsis = true;

					//Location label
					Label locationLabel = new Label();
					pkmnLocationTable.Controls.Add(locationLabel, 1, pkmnLocationTable.RowCount - 1);
					locationLabel.Dock = DockStyle.Fill;
					locationLabel.TextAlign = ContentAlignment.MiddleLeft;
					locationLabel.Text = loc;
					locationLabel.AutoEllipsis = true;

					//Count label
					Label countLabel = new Label();
					pkmnLocationTable.Controls.Add(countLabel, 2, pkmnLocationTable.RowCount - 1);
					countLabel.Dock = DockStyle.Fill;
					countLabel.TextAlign = ContentAlignment.MiddleLeft;
					countLabel.Text = treatment.quantities[i].ToString();

					//Move button
					Button moveButton = new Button();
					pkmnLocationTable.Controls.Add(moveButton, 3, pkmnLocationTable.RowCount - 1);
					moveButton.Dock = DockStyle.Fill;
					moveButton.Text = "Move 1";
					moveButton.UseVisualStyleBackColor = true;
					moveButton.Click += new EventHandler((sender, e) => PKMN_MoveOne(print, loc, rar));

				}
			}

			//Resume
			pkmnDetailBox.Controls.Add(pkmnLocationTable);
			pkmnDetailBox.Size = new Size(pkmnDetailBox.Size.Width, pkmnLocationTable.Size.Height + 100);

		}

		//Move one card of a given rarity from one location to another
		private void PKMN_MoveOne(PKMN_Printing print, string from, string treatment) {
			if (pkmnMoveField.Text.Length > 0) {
				print.MoveOne(from, pkmnMoveField.Text, treatment);
				PKMN_LoadLocationTable(print);
			}
		}

		//Reload location data
		private void PKMN_OnClickReloadLocations(object sender, EventArgs e) {
			pkmnMoveField.Items.Clear();
			pkmnSearchLocationField.Items.Clear();
			pkmnSearchLocationField.Items.Add("--");
			foreach (string name in pkmnCatalog.printings.SelectMany(p => p.treatments).SelectMany(t => t.locations).Distinct()) { pkmnMoveField.Items.Add(name); }
			foreach (string name in pkmnCatalog.printings.SelectMany(p => p.treatments).SelectMany(t => t.locations).Distinct()) { pkmnSearchLocationField.Items.Add(name); }
		}

		//Load printings list
		private void PKMN_LoadPrintingsList(PKMN_Card card, PKMN_Printing curPrint) {
			pkmnPrintingsBox.Controls.Clear();
			if (card.name.Equals("_"))
				return;
			List<PKMN_Printing> prints = pkmnCatalog.printings.Where(p => p.card == card).ToList();
			prints.Sort(new PKMN_PrintComparerNumericReverse().Compare);
			for (int i = 0; i < prints.Count; ++i) {
				Label label = new Label();
				label.Font = Utils.FONT_UNDERLINE;
				if (prints[i] != curPrint)
					label.ForeColor = Color.Blue;
				pkmnPrintingsBox.Controls.Add(label);
				label.Location = new Point(5, 20 + (i * 30));
				label.Size = new Size(pkmnPrintingsBox.Width - 10, TEXT_HEIGHT);
				label.Text = prints[i].printID.ToUpper() + " - " + prints[i].set.name;
				label.TextAlign = ContentAlignment.MiddleLeft;
				string id = prints[i].printID;
				if (prints[i] != curPrint)
					label.Click += new EventHandler((sender, e) => PKMN_LoadCardtip(id));
				label.MouseEnter += new EventHandler((sender, e) => PKMN_ShowCardtip(label, id));
				label.MouseLeave += new EventHandler((sender, e) => pkmnCardtipBox.Hide());
			}
			pkmnPrintingsBox.Height = 20 + (prints.Count * 30);
		}

		//Flip card image
		private void PKMN_FlipCard(object sender, EventArgs e) => PKMN_FlipCard();
		private void PKMN_FlipCard() {

			//Return if no reference set
			if (pkmnDetailPrint == null) { return; }

			//Only flip if card has a second face
			if (!pkmnDetailPrint.card.name.Contains(" // ")) { return; }

			//Get printing and flip
			pkmnDetailFlipped = !pkmnDetailFlipped;

			//Swap image if there's a back image reference
			if (pkmnDetailPrint.backImgPath.Length > 1) {
				if (pkmnDetailFlipped) { PKMN_Utils.TryLoadCardImage(pkmnDetailImgbox, pkmnDetailPrint.backImgPath); }
				else { PKMN_Utils.TryLoadImage(pkmnDetailImgbox, pkmnDetailPrint.imgPath); }
			}

			//Otherwise rotate 180
			else { pkmnDetailImgbox.Image.RotateFlip(RotateFlipType.Rotate180FlipNone); }

		}

		//Edit card data
		private void PKMN_EditCard(object sender, EventArgs e) => PKMN_EditCard();
		private void PKMN_EditCard() {

			//Return if print or reference card are invalid
			if (pkmnDetailPrint == null) { return; }
			PKMN_Card card = pkmnDetailPrint.card;
			if (card == null) { return; }

			//Setup edit page
			pkmnNameField.Text = card.name;
			pkmnETypeField.Text = card.energyType;
			pkmnTypeField.Text = card.cardTypes;
			pkmnStageField.Text = card.stage;
			pkmnHPField.Value = card.hp;
			pkmnOracleField.Text = card.oracleText;
			pkmnWeakField.Text = card.weakness;
			pkmnResistField.Text = card.resistance;
			pkmnRetreatField.Text = card.retreatCost;
			pkmnUpdateCard = card;
			pkmnAddCardButton.Text = "Update Card";
			pkmnTabControl.SelectedTab = pkmnCardPage;

		}

		//Edit printing data
		private void PKMN_EditPrint(object sender, EventArgs e) => PKMN_EditPrint();
		private void PKMN_EditPrint() {

			//Return if print is invalid
			if (pkmnDetailPrint == null) { return; }
			PKMN_Printing print = pkmnDetailPrint;

			//Setup edit page
			pkmnSetField.SelectedItem = print.set;
			pkmnNumberField.Value = print.cardNumber;
			pkmnRarityField.Text = print.rarity;
			pkmnTreatments.Clear();
			foreach (PKMN_Treatment treatment in print.treatments) { pkmnTreatments.Add(treatment.name); }
			PKMN_UpdateTreatmentList();
			pkmnFlavorField.Text = print.flavorText;
			pkmnImgpathLabel.Text = print.imgPath;
			PKMN_Utils.TryLoadCardImage(pkmnPrintImgbox, print.imgPath);
			pkmnImgpathBackLabel.Text = print.backImgPath;
			if (print.backImgPath.Length > 1) { PKMN_Utils.TryLoadCardImage(pkmnPrintImgboxBack, print.backImgPath); }
			else { /*Load default*/ }
			pkmnCardrefField.SelectedItem = print.card;
			pkmnPrintIDField.Text = print.printID;
			pkmnUpdatePrint = print;
			pkmnAddPrintButton.Text = "Update Printing";
			pkmnTabControl.SelectedTab = pkmnPrintPage;

		}

		//Navigation
		private void PKMN_LoadPreviousInSelection(object sender, EventArgs e) => PKMN_LoadPreviousInSelection();
		private void PKMN_LoadPreviousInSelection() {
			if (pkmnDetailPrev != null)
				PKMN_LoadCardDetails(pkmnDetailPrev);
		}
		private void PKMN_LoadNextInSelection(object sender, EventArgs e) => PKMN_LoadNextInSelection();
		private void PKMN_LoadNextInSelection() {
			if (pkmnDetailNext != null)
				PKMN_LoadCardDetails(pkmnDetailNext);
		}

		//Autogen card details, then move to the next card in the set every two seconds
		private async void PKMN_AutogenDetailRef(object sender, EventArgs e) {
			/*
			await PKMN_AutogenDetailRef();
			while (pkmnDetailNext != null) {
				Thread.Sleep(250);
				PKMN_LoadCardDetails(pkmnDetailNext);
				Thread.Sleep(250);
				await PKMN_AutogenDetailRef();
			}
			*/
		}

		//Get card name from Scryfall, generate card object if it doesn't exist, then set printing reference
		private async Task PKMN_AutogenDetailRef() {
			/*
			//Set up result and get card page
			string name = "";
			string result = await PKMN_GetWebpage("https://api.scryfall.com/cards/" + pkmnDetailPrint.tcgcID);

			//Return if error
			if (result.Contains("Error: ")) {
				pkmnDetailDialog.Text = result;
				return;
			}

			//Find name
			if (result.Contains("\"name\":")) {
				int idx = result.IndexOf("\"name\":");
				name = result.Substring(idx + 8);
				idx = name.IndexOf("\",\"");
				name = name.Substring(0, idx);
				pkmnNameField.Text = name;
			}

			//Find type line
			if (result.Contains("\"type_line\":")) {
				int idx = result.IndexOf("\"type_line\":");
				string type = result.Substring(idx + 13);
				idx = type.IndexOf("\",\"");
				type = type.Substring(0, idx);
				pkmnCardTypeField.Text = type;
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
				pkmnCostField.Text = cost;
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
				pkmnOracleTextField.Text = oracle;
			}

			//Find powers
			if (result.Contains("\"power\":")) {
				int idx = result.IndexOf("\"power\":");
				string powers = result.Substring(idx + 9);
				idx = powers.IndexOf("\",\"");
				string power = powers.Substring(0, idx);
				try { pkmnPowerField.Value = decimal.Parse(power); }
				catch { }
				if (powers.Contains("\"power\":")) {
					idx = powers.IndexOf("\"power\":");
					string power2 = powers.Substring(idx + 9);
					idx = power2.IndexOf("\",\"");
					power2 = power2.Substring(0, idx);
					try { pkmnPowerBackField.Value = decimal.Parse(power2); }
					catch { }
				}
			}

			//Find toughnesses
			if (result.Contains("\"toughness\":")) {
				int idx = result.IndexOf("\"toughness\":");
				string toughnesses = result.Substring(idx + 13);
				idx = toughnesses.IndexOf("\",\"");
				string toughness = toughnesses.Substring(0, idx);
				try { pkmnToughnessField.Value = decimal.Parse(toughness); }
				catch { }
				if (toughnesses.Contains("\"toughness\":")) {
					idx = toughnesses.IndexOf("\"toughness\":");
					string toughness2 = toughnesses.Substring(idx + 13);
					idx = toughness2.IndexOf("\",\"");
					toughness2 = toughness2.Substring(0, idx);
					try { pkmnToughnessBackField.Value = decimal.Parse(toughness2); }
					catch { }
				}
			}

			//Generate card
			pkmnIgnoreDuplicateEntryBox.Checked = false;
			PKMN_AddCard();
			PKMN_Card card = pkmnCatalog.cards.FirstOrDefault(c => c.name.Equals(name));
			if (card != null) { pkmnDetailPrint.card = card; }
			PKMN_LoadCardDetails(pkmnDetailPrint);
			pkmnDetailDialog.Text = result;
			*/
		}

		//HTTP Client
		static readonly HttpClient pkmnClient = new HttpClient();
		static async Task<string> PKMN_GetWebpage(string url) {
			try {
				string data = await pkmnClient.GetStringAsync(url);
				return data;
			}
			catch (Exception e) {
				string error = "Error: " + e.ToString();
				return error;
			}
		}

		//Delete printing from catalog
		private void PKMN_DeleteCurrentPrinting(object sender, EventArgs e) {
			int index = pkmnCatalog.printings.IndexOf(pkmnDetailPrint);
			pkmnCatalog.printings.RemoveAt(index);
			PKMN_UpdateSets();
			pkmnDetailPrint = null;
		}

		#endregion

		#endregion

		#region Card Entry

		//Check if card exists with name
		private void PKMN_CheckCardNameExists() {
			if (pkmnCatalog.cards.Select(card => card.name).Contains(pkmnNameField.Text)) {
				pkmnCardDialog.Text = pkmnNameField.Text + " already exists";
			}
		}

		//Import card text
		private void PKMN_OnClickImport(object sender, EventArgs e) {

			//Reset fields
			PKMN_ResetEntryFields();

			//Split and set name
			string[] lines = pkmnImportField.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
			pkmnNameField.Text = lines[1];

			//Pokemon fields
			if (lines[2].StartsWith("HP ") && !lines[3].Contains("Trainer")) {

				//Header data
				pkmnHPField.Value = decimal.Parse(lines[2].Substring(3));
				pkmnETypeField.Text = PKMN_Utils.ReplaceTypeSymbols(lines[3], true);
				pkmnTypeField.Text = lines[4];
				pkmnStageField.Text = lines[5];

				//Oracle text
				int i = 6;
				string o = "";
				while (true) {

					//Break
					if (i >= lines.Length)
						break;

					//Skip card rules
					if (lines[i].Equals("Card rule")) {
						i += 3;
						continue;
					}

					//Footer text
					if (lines[i].Equals("Weakness")) {
						while (true) {
							++i;
							if (lines[i].Equals("Resistance"))
								break;
							if (pkmnWeakField.Text.Length > 0)
								pkmnWeakField.Text += " ";
							pkmnWeakField.Text += PKMN_Utils.ReplaceTypeSymbols(lines[i], true);
						}
						while (true) {
							++i;
							if (lines[i].Equals("Retreat Cost"))
								break;
							if (pkmnResistField.Text.Length > 0)
								pkmnResistField.Text += " ";
							pkmnResistField.Text += PKMN_Utils.ReplaceTypeSymbols(lines[i], true);
						}
						if (lines[i + 1].Equals("None"))
							pkmnRetreatField.Text = "—";
						else
							pkmnRetreatField.Text = PKMN_Utils.ReplaceTypeSymbols(lines[i + 1], true);
						break;
					}

					//Add double line break between abilities
					if (o.Length > 0)
						o += "\r\n\r\n";

					//Powers
					if (PKMN_Utils.abilityTerms.Keys.Any(t => lines[i].Contains(t))) { 
						o += lines[i] + " — " + lines[i + 1] + "\r\n" + PKMN_Utils.ReplaceTypeSymbols(lines[i + 3], false);
						i += 4;
					}

					//Attacks
					else {

						//Check line starts with cost
						string s = PKMN_Utils.ReplaceTypeSymbols(lines[i], true);
						if (!s.Contains("{"))
							break;

						//Fields
						else {

							//All fields
							if (lines[i + 3].Length == 0) {
								o += s + " " + lines[i + 1] + " — " + lines[i + 2] + "\r\n" + PKMN_Utils.ReplaceTypeSymbols(lines[i + 4], false);
								i += 5;
							}

							//No damage
							else if (lines[i + 2].Length == 0) {
								o += s + " " + lines[i + 1] + "\r\n" + PKMN_Utils.ReplaceTypeSymbols(lines[i + 3], false);
								i += 4;
							}

							//No effect
							else {
								o += s + " " + lines[i + 1] + " — " + lines[i + 2];
								i += 3;
							}

						}

					}

				}

				//Apply oracle text
				pkmnOracleField.Text = o;

			}

			//Fossils
			else if (lines[2].StartsWith("HP ") && lines[3].Equals("Trainer")) {
				pkmnHPField.Value = decimal.Parse(lines[2].Substring(3));
				pkmnTypeField.Text = lines[3];
				pkmnOracleField.Text = PKMN_Utils.ReplaceTypeSymbols(lines[5], false);
			}

			//Energy
			else if (lines[2].Contains("Energy")) {
				pkmnTypeField.Text = lines[2];
				if (!lines[2].Contains("Basic"))
					pkmnOracleField.Text = PKMN_Utils.ReplaceTypeSymbols(lines[4], false);
			}

			//Trainers
			else {
				pkmnTypeField.Text = lines[2];
				pkmnOracleField.Text = PKMN_Utils.ReplaceTypeSymbols(lines[4], false);
			}

		}

		//Quick add card types to type line
		private void PKMN_OnClickTrainer(object sender, EventArgs e) => pkmnTypeField.Text = "Trainer / " + pkmnTypeField.Text;
		private void PKMN_OnClickPokemon(object sender, EventArgs e) => pkmnTypeField.Text = "Pokémon / " + pkmnTypeField.Text;

		//Add card to catalog
		private void PKMN_OnClickAddCard(object sender, EventArgs e) => PKMN_AddCard();
		private void PKMN_AddCard() {

			//Generate card object
			PKMN_Card card = new PKMN_Card(
				pkmnNameField.Text,
				pkmnETypeField.Text,
				pkmnTypeField.Text,
				pkmnStageField.Text,
				pkmnOracleField.Text,
				pkmnWeakField.Text,
				pkmnResistField.Text,
				pkmnRetreatField.Text,
				(int)pkmnHPField.Value
			);

			//Create or update card in catalog
			if (pkmnUpdateCard == null) {
				PKMN_Card duplicate = pkmnCatalog.cards.FirstOrDefault(c => c.ToString().Equals(card.ToString()));
				if (duplicate != default && !pkmnIgnorDuplicateEntryBox.Checked) {
					pkmnCardDialog.Text = duplicate.ToString() + " already exists";
					return;
				}
				pkmnCatalog.cards.Add(card);
				PKMN_UpdateCardList();
				pkmnCardDialog.Text = "-";
			}
			else {
				pkmnUpdateCard.Copy(card);
				pkmnCardDialog.Text = "Card Data Updated";
				if (pkmnDetailPrint.card == pkmnUpdateCard) { PKMN_LoadCardDetails(pkmnDetailPrint); }
			}

			//Reset fields
			PKMN_ResetEntryFields();
			pkmnIgnorDuplicateEntryBox.Checked = false;
			pkmnUpdateCard = null;
			pkmnAddCardButton.Text = "Add To Catalog";

		}

		//Reset card entry fields
		private void PKMN_ResetEntryFields() {
			pkmnNameField.Text = "";
			pkmnETypeField.Text = "";
			pkmnTypeField.Text = "";
			pkmnStageField.Text = "";
			pkmnOracleField.Text = "";
			pkmnWeakField.Text = "";
			pkmnResistField.Text = "";
			pkmnRetreatField.Text = "";
			pkmnHPField.Value = 0;
		}

		#endregion

		#region Printing Entry

		//Clear and refresh list of card names
		private void PKMN_UpdateCardList() {
			pkmnCardrefField.Items.Clear();
			foreach (PKMN_Card card in pkmnCatalog.cards) { pkmnCardrefField.Items.Add(card); }
		}

		//Update monster list label
		private void PKMN_UpdateTreatmentList() {
			if (pkmnTreatments.Count == 0) {
				pkmnTreatmentsValue.Text = "-";
				return;
			}
			string s = "";
			for (int i = 0; i < pkmnTreatments.Count; ++i) {
				if (i > 0) { s += ", "; }
				s += pkmnTreatments[i];
			}
			pkmnTreatmentsValue.Text = s;
		}

		//Add rarity to list
		private void PKMN_OnClickAddTreatment(object sender, EventArgs e) {
			if (pkmnTreatmentField.Text.Length <= 0) { return; }
			if (pkmnTreatments.Contains(pkmnTreatmentField.Text)) { return; }
			pkmnTreatments.Add(pkmnTreatmentField.Text);
			PKMN_UpdateTreatmentList();
		}

		//Remove rarity from list
		private void PKMN_OnClickSubTreatment(object sender, EventArgs e) {
			if (pkmnTreatments.Count == 0) { return; }
			pkmnTreatments.RemoveAt(pkmnTreatments.Count - 1);
			PKMN_UpdateTreatmentList();
		}

		//Replace first treatment with "Normal"
		private void PKMN_OnClickNormalTreatment(object sender, EventArgs e) {
			if (pkmnTreatments.Count == 0)
				pkmnTreatments.Add("Normal");
			else if (!pkmnTreatments.Contains("Normal"))
				pkmnTreatments[0] = "Normal";
			PKMN_UpdateTreatmentList();
		}

		//Replace first treatment with "Holo"
		private void PKMN_OnClickHoloTreatment(object sender, EventArgs e) {
			if (pkmnTreatments.Count == 0)
				pkmnTreatments.Add("Holo");
			else if (!pkmnTreatments.Contains("Holo"))
				pkmnTreatments[0] = "Holo";
			PKMN_UpdateTreatmentList();
		}

		//Get image path and display card
		private void PKMN_OnClickSearchImg(object sender, EventArgs e) {
			if (imageFileDialog.ShowDialog() == DialogResult.OK) {
				string curDir = Directory.GetCurrentDirectory();
				string filePath = imageFileDialog.FileName;
				if (!filePath.Contains(curDir)) {
					pkmnImgpathLabel.Text = "Selected file is not locally referrable";
					return;
				}
				filePath = filePath.Remove(filePath.IndexOf(curDir), curDir.Length + 1);
				pkmnImgpathLabel.Text = filePath;
				PKMN_Utils.TryLoadCardImage(pkmnPrintImgbox, filePath);
			}
		}

		//Get image path and display card back
		private void PKMN_OnClickSearchImgBack(object sender, EventArgs e) {
			if (imageFileDialog.ShowDialog() == DialogResult.OK) {
				string curDir = Directory.GetCurrentDirectory();
				string filePath = imageFileDialog.FileName;
				if (!filePath.Contains(curDir)) {
					pkmnImgpathBackLabel.Text = "Selected file is not locally referrable";
					return;
				}
				filePath = filePath.Remove(filePath.IndexOf(curDir), curDir.Length + 1);
				pkmnImgpathBackLabel.Text = filePath;
				PKMN_Utils.TryLoadCardImage(pkmnPrintImgboxBack, filePath);
			}
		}

		//Autofill image
		private void PKMN_OnClickPrintAutofill(object sender, EventArgs e) => PKMN_OnClickPrintAutofill();
		private bool PKMN_OnClickPrintAutofill() {
			PKMN_Set set = (PKMN_Set)pkmnSetField.SelectedItem;
			if (set != null) {
				pkmnCardrefField.SelectedIndex = 1;
				string code = set.code;
				string num = pkmnNumberField.Value.ToString().PadLeft(4, '0');
				pkmnPrintIDField.Text = code.ToLower() + "/" + pkmnNumberField.Value.ToString();
				string path = "resources/pkmn/" + code + "/" + num;
				pkmnCardrefDescriptor.Text = path;
				if (PKMN_Utils.TryLoadCardImage(pkmnPrintImgbox, path + ".jpg", pkmnImgpathLabel))
					return true;
			}
			return false;
		}

		//Populate descriptor when card reference is selected
		private void PKMN_OnSelectCardref(object sender, EventArgs e) {

			//Return if index or card is invalid
			if (pkmnCardrefField.SelectedIndex < 0) { return; }
			PKMN_Card card = (PKMN_Card)pkmnCardrefField.SelectedItem;
			if (card == null) { return; }

			//Populate descriptor
			pkmnCardrefDescriptor.Text = card.ToString();

		}

		//Add printing to catalog
		private void PKMN_OnClickAddPrint(object sender, EventArgs e) => PKMN_OnClickAddPrint();
		private void PKMN_OnClickAddPrint() {

			//Return if no set, treatments, images, or card reference set
			if (pkmnSetField.SelectedIndex < 0) { return; }
			if (pkmnTreatments.Count == 0) { return; }
			if (pkmnImgpathLabel.Text.Length <= 1) { return; }
			if (pkmnCardrefField.SelectedIndex < 0) { return; }

			//Generate printing object
			PKMN_Printing print = new PKMN_Printing(
				(PKMN_Set)pkmnSetField.SelectedItem,
				(int)pkmnNumberField.Value,
				pkmnFlavorField.Text,
				pkmnImgpathLabel.Text,
				pkmnImgpathBackLabel.Text,
				pkmnRarityField.Text,
				PKMN_Treatment.GenerateTreatments(pkmnTreatments),
				(PKMN_Card)pkmnCardrefField.SelectedItem,
				pkmnPrintIDField.Text
			);

			//Create or update printing in catalog
			if (pkmnUpdatePrint == null) {
				pkmnCatalog.printings.Add(print);
				PKMN_UpdateSets();
				pkmnImgpathLabel.Text = "-";
				pkmnImgpathBackLabel.Text = "";
			}
			else {
				pkmnUpdatePrint.Copy(print);
				pkmnImgpathLabel.Text = "Printing Data Updated";
				if (pkmnDetailPrint == pkmnUpdatePrint) { PKMN_LoadCardDetails(pkmnDetailPrint); }
			}

			//Reset fields
			pkmnNumberField.Value += 1;
			pkmnRarityField.SelectedIndex = -1;
			pkmnFlavorField.Text = "";
			pkmnCardrefField.SelectedIndex = -1;
			int index = pkmnPrintIDField.Text.IndexOf("/");
			if (index > 0) {
				pkmnPrintIDField.Text = pkmnPrintIDField.Text.Substring(0, index + 1);
				pkmnPrintIDField.Text += ((int)pkmnNumberField.Value).ToString();
			}
			else { pkmnPrintIDField.Text = ""; }
			pkmnPrintImgbox.Image = null;
			PKMN_Utils.TryLoadCardImage(pkmnPrintImgboxBack, PKMN_Utils.CARD_BACK_PATH);
			pkmnCardrefDescriptor.Text = "-";
			pkmnUpdatePrint = null;
			pkmnAddPrintButton.Text = "Add To Catalog";

		}

		//Add card to catalog and autofill next
		private void PKMN_OnClickAddAndFill(object sender, EventArgs e) {
			int lastIndex = -1;
			while (true) {
				PKMN_OnClickAddPrint();
				if (pkmnNumberField.Value == lastIndex) { break; }
				lastIndex = (int)pkmnNumberField.Value;
				if (!PKMN_OnClickPrintAutofill()) { break; }
				if (lastIndex > pkmnPrintAutoLimit.Value) { break; }
			}
		}

		#endregion

		#region Symbols

		//Properties
		private List<PKMN_FormSymbol> pkmnFormSymbols = new List<PKMN_FormSymbol>();

		//Symbol form class
		private class PKMN_FormSymbol {
			public GroupBox box;
			public TextBox nameBox;
			public TextBox symbolBox;
			public NumericUpDown aspectBox;
			public Label pathLabel;
			public PictureBox iconBox;
			public PKMN_FormSymbol() : this(null, null, null, null, null, null) { }
			public PKMN_FormSymbol(GroupBox box, TextBox nameBox, TextBox symbolBox, NumericUpDown aspectBox, Label pathLabel, PictureBox iconBox) {
				this.box = box;
				this.nameBox = nameBox;
				this.symbolBox = symbolBox;
				this.aspectBox = aspectBox;
				this.pathLabel = pathLabel;
				this.iconBox = iconBox;
			}
		}

		//Regenerate symbol controls
		private void PKMN_RegenerateSymbols() {
			foreach (PKMN_FormSymbol symbol in pkmnFormSymbols) { pkmnSymbolLayout.Controls.Remove(symbol.box); }
			pkmnFormSymbols.Clear();
			if (pkmnCatalog.symbols == null) { pkmnCatalog.symbols = new List<PKMN_Symbol>(); }
			foreach (PKMN_Symbol symbol in pkmnCatalog.symbols) {
				if (symbol.aspect < 0.01m) { symbol.aspect = 0.01m; }
				if (symbol.aspect > 100.0m) { symbol.aspect = 100.0m; }
				PKMN_CreateSymbolBox(symbol);
			}
		}

		//Add new symbol
		private void PKMN_OnClickAddSymbol(object sender, EventArgs e) => PKMN_CreateSymbolBox();
		private void PKMN_CreateSymbolBox() => PKMN_CreateSymbolBox(new PKMN_Symbol(), false);
		private void PKMN_CreateSymbolBox(PKMN_Symbol refSymbol, bool useRef = true) {

			//Index
			int i = pkmnFormSymbols.Count;

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
			icon.Click += new EventHandler((sender, e) => PKMN_OnClickSearchSymbol(i));

			//Load image if one is referenced
			if (refSymbol.imgPath.Length > 0) {
				path.Text = refSymbol.imgPath;
				PKMN_Utils.TryLoadImage(icon, refSymbol.imgPath);
			}

			//Create object and add box to layout
			pkmnFormSymbols.Add(new PKMN_FormSymbol(box, name, symbol, aspect, path, icon));
			pkmnSymbolLayout.Controls.Add(box);

		}

		//Get image path and display card
		private void PKMN_OnClickSearchSymbol(int index) {
			if (imageFileDialog.ShowDialog() == DialogResult.OK) {
				string curDir = Directory.GetCurrentDirectory();
				string filePath = imageFileDialog.FileName;
				if (!filePath.Contains(curDir)) { return; }
				filePath = filePath.Remove(filePath.IndexOf(curDir), curDir.Length + 1);
				pkmnFormSymbols[index].pathLabel.Text = filePath;
				PKMN_Utils.TryLoadImage(pkmnFormSymbols[index].iconBox, filePath);
			}
		}

		//Save current symbol list
		private void PKMN_OnClickSaveSymbols(object sender, EventArgs e) {
			pkmnCatalog.symbols.Clear();
			foreach (PKMN_FormSymbol symbol in pkmnFormSymbols) {
				pkmnCatalog.symbols.Add(
					new PKMN_Symbol(
						symbol.nameBox.Text,
						symbol.symbolBox.Text,
						symbol.pathLabel.Text,
						symbol.aspectBox.Value
					)
				);
			}
		}

		#endregion

		#region Set Generator

		//Properties
		public int pkmnSetGeneratorPagenum = 0;
		public int pkmnSetGeneratorSetsPage = 25;
		private List<PKMN_FormSet> pkmnFormSets = new List<PKMN_FormSet>();

		//Paging
		private void PKMN_OnClickSetGeneratorPrev(object sender, EventArgs e) {
			--pkmnSetGeneratorPagenum;
			PKMN_RegenerateSets();
		}
		private void PKMN_OnClickSetGeneratorNext(object sender, EventArgs e) {
			++pkmnSetGeneratorPagenum;
			PKMN_RegenerateSets();
		}

		//Symbol form class
		private class PKMN_FormSet {
			public PKMN_Set set;
			public GroupBox box;
			public TextBox nameBox;
			public TextBox codeBox;
			public DateTimePicker dateBox;
			public Label pathLabel;
			public PictureBox iconBox;
			public CheckBox leadBonusSheetCheckbox;
			public PKMN_FormSet() : this(null, null, null, null, null, null, null, null) { }
			public PKMN_FormSet(PKMN_Set set, GroupBox box, TextBox nameBox, TextBox codeBox, DateTimePicker dateBox, Label pathLabel, PictureBox iconBox, CheckBox leadBonusSheetCheckbox) {
				this.set = set;
				this.box = box;
				this.nameBox = nameBox;
				this.codeBox = codeBox;
				this.dateBox = dateBox;
				this.pathLabel = pathLabel;
				this.iconBox = iconBox;
				this.leadBonusSheetCheckbox = leadBonusSheetCheckbox;
			}
		}

		//Regenerate symbol controls
		private void PKMN_RegenerateSets(object sender, EventArgs e) => PKMN_RegenerateSets();
		private void PKMN_RegenerateSets() {

			//Quick null check
			if (pkmnCatalog.sets == null) { pkmnCatalog.sets = new List<PKMN_Set>(); }

			//Pagination
			int maxPage = pkmnCatalog.sets.Count / pkmnSetGeneratorSetsPage;
			if (pkmnSetGeneratorPagenum > maxPage) { pkmnSetGeneratorPagenum = 0; }
			if (pkmnSetGeneratorPagenum < 0) { pkmnSetGeneratorPagenum = maxPage; }
			int startIndex = pkmnSetGeneratorPagenum * pkmnSetGeneratorSetsPage;
			int maxIndex = pkmnSetGeneratorSetsPage;
			if (pkmnSetGeneratorPagenum == maxPage) { maxIndex = pkmnCatalog.sets.Count % pkmnSetGeneratorSetsPage; }
			maxIndex += startIndex;
			pkmnSetGeneratorPageLabel.Text = (pkmnSetGeneratorPagenum + 1) + " / " + (maxPage + 1);

			//Remove old
			pkmnSetGeneratorLayout.SuspendLayout();
			foreach (PKMN_FormSet set in pkmnFormSets) { pkmnSetGeneratorLayout.Controls.Remove(set.box); }
			pkmnFormSets.Clear();

			//Add new
			for (int i = startIndex; i < maxIndex; ++i) {
				PKMN_Set set = pkmnCatalog.sets[i];
				PKMN_CreateSetGeneratorBox(set);
			}
			pkmnSetGeneratorLayout.ResumeLayout();

		}

		//Add new symbol
		private void PKMN_OnClickAddSet(object sender, EventArgs e) => PKMN_CreateSetGeneratorBox();
		private void PKMN_CreateSetGeneratorBox() => PKMN_CreateSetGeneratorBox(new PKMN_Set(), false);
		private void PKMN_CreateSetGeneratorBox(PKMN_Set refSet, bool useRef = true) {

			//Index
			int i = pkmnFormSets.Count;

			//Group box
			GroupBox box = new GroupBox();
			box.Size = new Size(350, 180);

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

			//Leading bonus sheet box
			CheckBox bsBox = new CheckBox();
			box.Controls.Add(bsBox);
			bsBox.Location = new Point(5, 115);
			bsBox.Size = new Size(285, 35);
			bsBox.Checked = useRef ? refSet.leadBonusSheet : false;
			bsBox.Text = "Has leading bonus sheet";

			//Path label
			Label path = new Label();
			box.Controls.Add(path);
			path.Location = new Point(5, 140);
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
				PKMN_Utils.TryLoadImage(icon, refSet.imgPath);
			}

			//Create object and set up search event
			PKMN_FormSet set = new PKMN_FormSet(useRef ? refSet : null, box, name, code, date, path, icon, bsBox);
			icon.Click += new EventHandler((sender, e) => PKMN_OnClickSearchSetIcon(set));

			//Add to list and layout
			pkmnFormSets.Add(set);
			pkmnSetGeneratorLayout.Controls.Add(box);

		}

		//Get image path and display card
		private void PKMN_OnClickSearchSetIcon(PKMN_FormSet set) {
			if (imageFileDialog.ShowDialog() == DialogResult.OK) {
				string curDir = Directory.GetCurrentDirectory();
				string filePath = imageFileDialog.FileName;
				if (!filePath.Contains(curDir)) { return; }
				filePath = filePath.Remove(filePath.IndexOf(curDir), curDir.Length + 1);
				set.pathLabel.Text = filePath;
				PKMN_Utils.TryLoadImage(set.iconBox, filePath);
			}
			else {
				set.pathLabel.Text = "";
				set.iconBox.Image = null;
			}
		}

		//Save current symbol list
		private void PKMN_OnClickSaveSets(object sender, EventArgs e) {
			foreach (PKMN_FormSet formSet in pkmnFormSets) {
				PKMN_Set set = new PKMN_Set(
					formSet.nameBox.Text,
					formSet.codeBox.Text,
					formSet.pathLabel.Text,
					formSet.dateBox.Value,
					formSet.leadBonusSheetCheckbox.Checked
				);
				if (formSet.set == null) { pkmnCatalog.sets.Add(set); }
				else { formSet.set.Copy(set); }
			}
			PKMN_UpdateSets();
			pkmnSetField.Items.Clear();
			pkmnSearchSetField.Items.Clear();
			pkmnSearchSetField.Items.Add("--");
			foreach (PKMN_Set set in pkmnCatalog.sets) {
				pkmnSetField.Items.Add(set);
				pkmnSearchSetField.Items.Add(set);
			}
		}

		#endregion

		#region I/O

		//Save catalog data
		private void PKMN_OnClickSave(object sender, EventArgs e) => PKMN_SaveCatalog();
		private void PKMN_SaveCatalog() {

			//Serialize catalog to file
			pkmnCatalog.SavePrintRefs();
			using (Stream stream = File.Open("resources/pkmn/catalog.bin", FileMode.Create))
				Serializer.Serialize(stream, pkmnCatalog);

			//Log
			pkmnIODialog.Text = "Catalog saved";

		}

		//Load catalog from file
		private void PKMN_LoadCatalog() {

			//Deserialize catalog from file
			try {
				using (Stream stream = File.Open("resources/pkmn/catalog.bin", FileMode.Open))
					pkmnCatalog = Serializer.Deserialize<PKMN_Catalog>(stream);
				pkmnCatalog.LoadPrintRefs();
			}
			catch (Exception ex) {
				pkmnIODialog.Text = "Error: " + ex.Message;
				return;
			}

			//Set static utils ref
			PKMN_Utils.catalog = pkmnCatalog;

			//Generate symbol controls
			PKMN_RegenerateSymbols();
			PKMN_RegenerateSets();

			//Update lists
			PKMN_InitLists();
			PKMN_UpdateSets();
			PKMN_UpdateCardList();

			//Log
			pkmnIODialog.Text = "Catalog loaded";

		}

		#endregion

	}
}
