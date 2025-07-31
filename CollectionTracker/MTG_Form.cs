using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CollectionTracker {
	public partial class Form1 : Form {

		//Properties
		public MTG_Catalog mtgCatalog;
		public List<string> mtgTreatments;
		public List<MTG_Printing> mtgPrintFilter;
		public MTG_Card mtgUpdateCard = null;
		public MTG_Printing mtgUpdatePrint = null;

		#region Init

		//Initialize
		public void MTG_Initialize() {
			mtgTreatments = new List<string>();
			mtgCatalog = new MTG_Catalog();
			mtgPrintFilter = new List<MTG_Printing>();
			mtgDetailBoxes = new List<GroupBox>();
			mtgNameField.LostFocus += new EventHandler((sender, e) => MTG_CheckCardNameExists());
			MTG_Utils.TryLoadImage(mtgIdentityImgW, "resources/mtg/_icons/w.png");
			MTG_Utils.TryLoadImage(mtgIdentityImgU, "resources/mtg/_icons/u.png");
			MTG_Utils.TryLoadImage(mtgIdentityImgB, "resources/mtg/_icons/b.png");
			MTG_Utils.TryLoadImage(mtgIdentityImgR, "resources/mtg/_icons/r.png");
			MTG_Utils.TryLoadImage(mtgIdentityImgG, "resources/mtg/_icons/g.png");
			MTG_Utils.TryLoadImage(mtgColourImgW, "resources/mtg/_icons/w.png");
			MTG_Utils.TryLoadImage(mtgColourImgU, "resources/mtg/_icons/u.png");
			MTG_Utils.TryLoadImage(mtgColourImgB, "resources/mtg/_icons/b.png");
			MTG_Utils.TryLoadImage(mtgColourImgR, "resources/mtg/_icons/r.png");
			MTG_Utils.TryLoadImage(mtgColourImgG, "resources/mtg/_icons/g.png");
			MTG_Utils.TryLoadImage(mtgSearchIDImgW, "resources/mtg/_icons/w.png");
			MTG_Utils.TryLoadImage(mtgSearchIDImgU, "resources/mtg/_icons/u.png");
			MTG_Utils.TryLoadImage(mtgSearchIDImgB, "resources/mtg/_icons/b.png");
			MTG_Utils.TryLoadImage(mtgSearchIDImgR, "resources/mtg/_icons/r.png");
			MTG_Utils.TryLoadImage(mtgSearchIDImgG, "resources/mtg/_icons/g.png");
			MTG_Utils.TryLoadImage(mtgSearchColImgW, "resources/mtg/_icons/w.png");
			MTG_Utils.TryLoadImage(mtgSearchColImgU, "resources/mtg/_icons/u.png");
			MTG_Utils.TryLoadImage(mtgSearchColImgB, "resources/mtg/_icons/b.png");
			MTG_Utils.TryLoadImage(mtgSearchColImgR, "resources/mtg/_icons/r.png");
			MTG_Utils.TryLoadImage(mtgSearchColImgG, "resources/mtg/_icons/g.png");
			MTG_Utils.TryLoadCardImage(mtgPrintImgboxBack, MTG_Utils.CARD_BACK_PATH);
			mtgClient.DefaultRequestHeaders.Add("User-Agent", "CollectionTracker");
			mtgClient.DefaultRequestHeaders.Add("Accept", "application/json");
			MTG_LoadCatalog();
		}

		//Re-initialize lists
		public void MTG_InitLists() {
			mtgSetField.Items.Clear();
			mtgRarityField.Items.Clear();
			mtgTreatmentField.Items.Clear();
			mtgMoveField.Items.Clear();
			mtgSearchSetField.Items.Clear();
			mtgSearchSetField.Items.Add("--");
			mtgSearchLocationField.Items.Clear();
			mtgSearchLocationField.Items.Add("--");
			foreach (MTG_Set set in mtgCatalog.sets) {
				mtgSetField.Items.Add(set);
				mtgSearchSetField.Items.Add(set);
			}
			foreach (string name in mtgCatalog.printings.Select(p => p.rarity).Distinct()) { mtgRarityField.Items.Add(name); }
			foreach (string name in mtgCatalog.printings.SelectMany(p => p.treatments).Select(t => t.name).Distinct()) { mtgTreatmentField.Items.Add(name); }
			foreach (string name in mtgCatalog.printings.SelectMany(p => p.treatments).SelectMany(t => t.locations).Distinct()) { mtgMoveField.Items.Add(name); }
			foreach (string name in mtgCatalog.printings.SelectMany(p => p.treatments).SelectMany(t => t.locations).Distinct()) { mtgSearchLocationField.Items.Add(name); }
		}

		#endregion

		#region Set List

		//Properties
		private List<(MTG_Set set, GroupBox box, bool expanded)> mtgSetlist = new List<(MTG_Set, GroupBox, bool)>();
		public int mtgSetPagenum = 0;
		public int mtgSetsPerPage = 15;

		//Paging
		private void MTG_OnClickPrevSet(object sender, EventArgs e) {
			--mtgSetPagenum;
			MTG_UpdateSets();
		}
		private void MTG_OnClickNextSet(object sender, EventArgs e) {
			++mtgSetPagenum;
			MTG_UpdateSets();
		}

		//Clear set list and update data
		private void MTG_UpdateSets() {

			//Clear controls and sort sets
			mtgSetlist.Clear();
			mtgSetLayout.Controls.Clear();
			mtgCatalog.sets.Sort(new SetComparer().Compare);

			//Get non-subsets
			List<MTG_Set> sets = mtgCatalog.sets.Where(s => s.indent == 0).ToList();

			//Pagination
			int maxPage = sets.Count / mtgSetsPerPage;
			if (mtgSetPagenum > maxPage) { mtgSetPagenum = 0; }
			if (mtgSetPagenum < 0) { mtgSetPagenum = maxPage; }
			int startIndex = mtgSetPagenum * mtgSetsPerPage;
			int maxIndex = mtgSetsPerPage;
			if (mtgSetPagenum == maxPage) { maxIndex = sets.Count % mtgSetsPerPage; }
			maxIndex += startIndex;
			mtgSetPageLabel.Text = (mtgSetPagenum + 1) + " / " + (maxPage + 1);

			//Create set rows
			for (int i = startIndex; i < maxIndex; ++i) {
				MTG_CreateSetRow(sets[i]);
				foreach (MTG_Set set in mtgCatalog.sets) {
					if (set.indent > 0 && set.date.Date == sets[i].date.Date) {
						MTG_CreateSetRow(set);
					}
				}
			}

		}

		//Generate set info
		private void MTG_CreateSetRow(MTG_Set set) {

			//Horizontal indent
			int indent = set.indent * 35;

			//Important values
			List<MTG_Printing> cardsInSet = mtgCatalog.printings.Where(print => print.set == set).ToList();
			int setCount = cardsInSet.Count;
			int setOwned = cardsInSet.Count(print => print.AnyOwned());
			bool missingCardref = cardsInSet.Count(print => print.card.name.Equals("") || print.card.name.Equals("_")) > 0;

			//Set info box
			GroupBox box = new GroupBox();
			mtgSetLayout.Controls.Add(box);
			box.Size = new Size(820, 70);

			//Filter button
			Button filter = new Button();
			box.Controls.Add(filter);
			filter.Location = new Point(5 + indent, 15);
			filter.Size = new Size(350 - indent, 50);
			filter.Text = set.name;
			filter.UseVisualStyleBackColor = true;
			if (set.imgPath.Length > 0) { filter.Image = new Bitmap(Image.FromFile(set.imgPath), new Size(35, 35)); }
			filter.TextImageRelation = TextImageRelation.ImageBeforeText;
			filter.ImageAlign = ContentAlignment.MiddleRight;
			filter.TextAlign = ContentAlignment.MiddleCenter;
			filter.Click += new EventHandler((sender, e) => MTG_FilterCatalogBySet(set));

			//Arrow
			if (indent > 0) {
				Label arrow = new Label();
				box.Controls.Add(arrow);
				arrow.Location = new Point(indent - 25, 15);
				arrow.Size = new Size(20, 50);
				arrow.Text = "↳";
				arrow.TextAlign = ContentAlignment.MiddleLeft;
				arrow.Font = new Font(arrow.Font.Name, 20, arrow.Font.Style);
			}

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

			//Expand button
			if (set.indent == 0) {
				Button expand = new Button();
				box.Controls.Add(expand);
				expand.Location = new Point(740, 15);
				expand.Size = new Size(35, 50);
				expand.Text = "V";
				expand.TextAlign = ContentAlignment.MiddleCenter;
				expand.Click += new EventHandler((sender, e) => MTG_ExpandCollapseSet(set));
			}

			//Hide if not main set
			else { box.Hide(); }

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
			mtgSetlist.Add((set, box, false));

		}

		//Expand or collapse set box
		private void MTG_ExpandCollapseSet(MTG_Set set) {
			for (int i = 0; i < mtgSetlist.Count; ++i) {
				(MTG_Set set, GroupBox box, bool expanded) listSet = mtgSetlist[i];
				if (listSet.set == set) {
					foreach ((MTG_Set set, GroupBox box, bool expanded) listSet2 in mtgSetlist) {
						if (listSet2.set != listSet.set && listSet2.set.date.Date == set.date.Date) {
							if (listSet.expanded) { listSet2.box.Hide(); }
							else { listSet2.box.Show(); }
						}
					}
					mtgSetlist[i] = (listSet.set, listSet.box, !listSet.expanded);
					break;
				}
			}
		}

		//Filter catalog by set ID
		private void MTG_FilterCatalogBySet(MTG_Set set) {
			mtgPrintFilter.Clear();
			foreach (MTG_Printing print in mtgCatalog.printings) {
				if (print.set == set) {
					mtgPrintFilter.Add(print);
				}
			}
			if (mtgSortMode) { mtgPrintFilter.Sort(new PrintComparerNumeric().Compare); }
			else { mtgPrintFilter.Sort(new PrintComparerAlphabetical().Compare); }
			mtgCatalogPagenum = 0;
			MTG_UpdateCatalog();
			mtgTabControl.SelectedTab = mtgCatalogPage;
		}

		#endregion

		#region Search

		//Check if string contains all in a given array of substrings
		private bool MTG_CheckSubstring(string s, string[] arr) {
			foreach (string ss in arr) {
				if (!s.ToLower().Contains(ss.ToLower())) {
					return false;
				}
			}
			return true;
		}

		//Search for card matching given criteria
		private void MTG_Search(object sender, EventArgs e) {

			//Clear print filter
			mtgPrintFilter.Clear();
			mtgPrintFilter = new List<MTG_Printing>();

			//Parameters
			bool searchName = mtgSearchNameField.Text.Length > 0;
			bool searchTypes = mtgSearchTypeField.Text.Length > 0;
			bool searchOracle = mtgSearchOracleField.Text.Length > 0;
			string set = mtgSearchSetField.SelectedItem?.ToString() ?? "";
			string loc = mtgSearchLocationField.SelectedItem?.ToString() ?? "";
			bool searchSet = !set.Equals("") && !set.Equals("--");
			bool searchLoc = !loc.Equals("") && !loc.Equals("--");

			//Colour and identity
			MTG_Colour colour = MTG_Colour.None;
			if (mtgSearchColW.Checked) { colour |= MTG_Colour.White; }
			if (mtgSearchColU.Checked) { colour |= MTG_Colour.Blue; }
			if (mtgSearchColB.Checked) { colour |= MTG_Colour.Black; }
			if (mtgSearchColR.Checked) { colour |= MTG_Colour.Red; }
			if (mtgSearchColG.Checked) { colour |= MTG_Colour.Green; }
			MTG_Colour identity = MTG_Colour.None;
			if (mtgSearchIDW.Checked) { identity |= MTG_Colour.White; }
			if (mtgSearchIDU.Checked) { identity |= MTG_Colour.Blue; }
			if (mtgSearchIDB.Checked) { identity |= MTG_Colour.Black; }
			if (mtgSearchIDR.Checked) { identity |= MTG_Colour.Red; }
			if (mtgSearchIDG.Checked) { identity |= MTG_Colour.Green; }

			//Search
			foreach(MTG_Printing print in mtgCatalog.printings) {
				if (colour != MTG_Colour.None && print.card.colour != colour) { continue; }
				if (identity != MTG_Colour.None && !identity.HasFlag(print.card.identity)) { continue; }
				if (searchName && !MTG_CheckSubstring(print.card.name, mtgSearchNameField.Text.Split('|'))) { continue; }
				if (searchTypes && !MTG_CheckSubstring(print.card.cardTypes, mtgSearchTypeField.Text.Split('|'))) { continue; }
				if (searchOracle && !MTG_CheckSubstring(print.card.oracleText, mtgSearchOracleField.Text.Split('|'))) { continue; }
				if (searchSet && print.set != mtgSearchSetField.SelectedItem) { continue; }
				if (searchLoc) {
					bool add = false;
					foreach (MTG_Treatment treatment in print.treatments) {
						if (treatment.locations.Contains(loc)) {
							add = true;
						}
					}
					if (!add) { continue; }
				}
				mtgPrintFilter.Add(print);
			}

			//Show catalog
			if (mtgSortMode) { mtgPrintFilter.Sort(new PrintComparerNumeric().Compare); }
			else { mtgPrintFilter.Sort(new PrintComparerAlphabetical().Compare); }
			mtgCatalogPagenum = 0;
			MTG_UpdateCatalog();
			mtgTabControl.SelectedTab = mtgCatalogPage;

		}

		#endregion

		#region Catalog

		//Properties
		public int mtgCatalogPagenum = 0;
		public int mtgCardsPerPage = 50;
		public bool mtgSortMode = true;

		//Sort buttons
		private void MTG_OnClickSortAlphabetical(object sender, EventArgs e) => MTG_UpdateSortMode(false);
		private void MTG_OnClickSortNumeric(object sender, EventArgs e) => MTG_UpdateSortMode(true);
		private void MTG_UpdateSortMode(bool mode) {
			mtgSortMode = mode;
			if (mtgSortMode) { mtgPrintFilter.Sort(new PrintComparerNumeric().Compare); }
			else { mtgPrintFilter.Sort(new PrintComparerAlphabetical().Compare); }
			mtgCatalogPagenum = 0;
			MTG_UpdateCatalog();
			mtgTabControl.SelectedTab = mtgCatalogPage;
		}

		//Paging
		private void MTG_OnClickCatalogPrev(object sender, EventArgs e) {
			--mtgCatalogPagenum;
			MTG_UpdateCatalog();
		}
		private void MTG_OnClickCatalogNext(object sender, EventArgs e) {
			++mtgCatalogPagenum;
			MTG_UpdateCatalog();
		}

		//Clear catalog and generate new cards
		private void MTG_UpdateCatalog() {

			//Remove and clear layout controls
			mtgCatalogPage.Controls.Remove(mtgCatalogLayout);
			mtgCatalogLayout.Controls.Clear();

			//Pagination
			int maxPage = mtgPrintFilter.Count / mtgCardsPerPage;
			if (mtgCatalogPagenum > maxPage) { mtgCatalogPagenum = 0; }
			if (mtgCatalogPagenum < 0) { mtgCatalogPagenum = maxPage; }
			int startIndex = mtgCatalogPagenum * mtgCardsPerPage;
			int maxIndex = mtgCardsPerPage;
			if (mtgCatalogPagenum == maxPage) { maxIndex = mtgPrintFilter.Count % mtgCardsPerPage; }
			maxIndex += startIndex;

			//Header
			mtgCatalogIndex.Text = $"Showing {startIndex + 1} - {maxIndex} of {mtgPrintFilter.Count}";

			//Loop printings
			for (int i = startIndex; i < maxIndex; ++i) {

				//Get index/printing
				MTG_Printing print = mtgPrintFilter[i];

				//Card box
				GroupBox box = new GroupBox();
				mtgCatalogLayout.Controls.Add(box);
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
				MTG_Utils.TryLoadCardImage(img, print.imgPath);
				img.Click += new EventHandler((sender, e) => MTG_LoadCardDetails(print));

				//Loop treatments
				for (int j = 0; j < print.treatments.Count; ++j) {

					//Get treatment at index
					MTG_Treatment treatment = print.treatments[j];

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
					leftButton.Click += new EventHandler((sender, e) => MTG_DecrementCardCount(box, label, print, treatment.name));

					//Increment
					Button rightButton = new Button();
					box.Controls.Add(rightButton);
					rightButton.Location = new Point(240, 425 + (j * 30));
					rightButton.Size = new Size(55, 29);
					rightButton.Text = ">";
					rightButton.UseVisualStyleBackColor = true;
					rightButton.Click += new EventHandler((sender, e) => MTG_IncrementCardCount(box, label, print, treatment.name));

				}

				//Resume
				box.ResumeLayout();

			}
			mtgCatalogPage.Controls.Add(mtgCatalogLayout);
		}

		//Increment card quantity
		private void MTG_IncrementCardCount(GroupBox box, Label label, MTG_Printing print, string treatment) {
			if (print != null) {
				print.Increment(treatment);
				label.Text = print.OwnedCountOfTreatment(treatment).ToString();
				if (!print.AnyOwned()) { box.BackColor = SystemColors.ControlDarkDark; }
				else { box.BackColor = SystemColors.ControlDark; }
			}
			else { label.Text = "Print is null!"; }
		}

		//Decrement card quantity
		private void MTG_DecrementCardCount(GroupBox box, Label label, MTG_Printing print, string treatment) {
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
		public const int TEXT_HEIGHT = 21;
		public bool mtgDetailFlipped = false;
		public List<GroupBox> mtgDetailBoxes;
		public MTG_Printing mtgDetailPrint = null;
		public MTG_Printing mtgDetailPrev = null;
		public MTG_Printing mtgDetailNext = null;

		//Load card data into details tab
		private void MTG_LoadCardDetails(MTG_Printing print) {

			//Set persistent reference
			mtgDetailPrint = print;

			//Set image
			MTG_Utils.TryLoadCardImage(mtgDetailImgbox, print.imgPath);
			mtgDetailFlipped = false;

			//Get card reference
			MTG_Card card = print.card;

			//Clear old boxes
			foreach (GroupBox box in mtgDetailBoxes) { mtgDetailPage.Controls.Remove(box); }
			mtgDetailBoxes.Clear();

			//Y position to create elements at
			int y = 5;

			//Split fields
			string[] names = card.name.Split(new string[] { " // " }, StringSplitOptions.None);
			string[] costs = card.cost.Split(new string[] { " // " }, StringSplitOptions.None);
			string[] types = card.cardTypes.Split(new string[] { " // " }, StringSplitOptions.None);
			string[] texts = card.oracleText.Split(new string[] { "\r\n//\r\n" }, StringSplitOptions.None);

			//Loop faces
			for (int i = 0; i < names.Length; ++i) {

				//Box-relative Y position
				int y2 = 20;

				//Face box
				GroupBox box = new GroupBox();
				mtgDetailPage.Controls.Add(box);
				box.Size = new Size(mtgDetailBox.Size.Width, 100);

				//Name
				Label name = new Label();
				box.Controls.Add(name);
				name.Location = new Point(5, y2);
				name.Size = new Size(TextRenderer.MeasureText(names[i], name.Font).Width, TEXT_HEIGHT);
				name.Text = names[i];
				name.TextAlign = ContentAlignment.MiddleLeft;

				//Cost
				if (costs.Length > i) {
					Point location = new Point(5 + TextRenderer.MeasureText(name.Text, name.Font).Width, y2);
					foreach (string symbol in MTG_GetSymbols(costs[i]))
						location = MTG_InsertSymbol(symbol, box, location, TEXT_HEIGHT);
				}
				y2 += TEXT_HEIGHT + 10;

				//Card Type
				if (types.Length > i) {
					Label type = new Label();
					box.Controls.Add(type);
					type.Location = new Point(5, y2);
					type.Size = new Size(mtgDetailBox.Size.Width - 10, TEXT_HEIGHT);
					type.Text = types[i];
					type.TextAlign = ContentAlignment.MiddleLeft;
					y2 += TEXT_HEIGHT + 10;
				}

				//Oracle Text
				if (texts.Length > i)
					if (texts[i].Length > 0)
						y2 += 10 + MTG_GenerateDescription(texts[i], box, new Point(5, y2));

				//Statline
				if (types[i].Contains("Creature") || types[i].Contains("Vehicle") || types[i].Contains("Planeswalker")) {
					Label stats = new Label();
					box.Controls.Add(stats);
					stats.Location = new Point(5, y2);
					stats.Size = new Size(mtgDetailBox.Size.Width - 10, 30);
					if (types[i].Contains("Planeswalker"))
						stats.Text = (i == 0 ? card.toughness : card.toughness2) + " Loyalty";
					else {
						stats.Text = i == 0
								   ? card.power + " / " + card.toughness
								   : card.power2 + " / " + card.toughness2;
					}
					stats.TextAlign = ContentAlignment.MiddleLeft;
					y2 += 35;
				}

				//Flavor Text (apply to last face box)
				if (i == names.Length - 1 && print.flavorText.Length > 0) {
					Label flavor = new Label();
					box.Controls.Add(flavor);
					flavor.Font = Utils.FONT_ITALIC;
					flavor.Location = new Point(5, y2);
					flavor.AutoSize = true;
					flavor.MaximumSize = new Size(mtgDetailBox.Size.Width - 10, 0);
					flavor.Text = print.flavorText;
					flavor.TextAlign = ContentAlignment.MiddleLeft;
					y2 += 5 + flavor.Size.Height;
				}

				//Size box and set position for next
				y2 += 5;
				box.Location = new Point(mtgDetailBox.Location.X, y);
				box.Size = new Size(mtgDetailBox.Size.Width, y2);
				mtgDetailBoxes.Add(box);
				y += 10 + y2;

			}

			//Load location table
			mtgDetailBox.Location = new Point(mtgDetailBox.Location.X, y);
			MTG_LoadLocationTable(print);

			//Load printings
			MTG_LoadPrintingsList(print.card, print);

			//Nav buttons
			int idx = mtgPrintFilter.IndexOf(print);
			if (idx == -1) {
				mtgDetailPrevButton.Hide();
				mtgDetailNextButton.Hide();
				return;
			}
			if (idx > 0)
				mtgDetailPrev = mtgPrintFilter[idx - 1];
			else
				mtgDetailPrev = mtgPrintFilter[mtgPrintFilter.Count - 1];
			if (idx < mtgPrintFilter.Count - 1)
				mtgDetailNext = mtgPrintFilter[idx + 1];
			else
				mtgDetailNext = mtgPrintFilter[0];
			mtgDetailPrevButton.Show();
			mtgDetailPrevButton.Text = mtgDetailPrev.card.name;
			mtgDetailNextButton.Show();
			mtgDetailNextButton.Text = mtgDetailNext.card.name;

			//Hide tooltip
			mtgTooltipBox.Hide();
			mtgCardtipBox.Hide();

			//Set tab
			mtgTabControl.SelectedTab = mtgDetailPage;

		}

		#region Description Generators

		//Generate description box. Returns total height of the description field
		private int MTG_GenerateDescription(string desc, GroupBox box, Point location) {

			//Set initial y position and loop fields
			int y = location.Y;
			while (true) {

				//Clear leading spaces
				if (desc.StartsWith(" ")) { desc = desc.Substring(1); }

				//Get index of next object
				int i = MTG_Utils.IndexOfMany(desc, new List<char>() { '{', '[', '<' });

				//We are at an object, generate it
				if (i == 0) {

					//Symbol
					if (desc[0] == '{') {
						int i2 = desc.IndexOf('}');
						if (i2 > 0) {
							location = MTG_InsertSymbol(desc.Substring(0, i2 + 1), box, location, TEXT_HEIGHT);
							desc = desc.Substring(i2 + 1);
						}
						else { desc = desc.Substring(1); }
					}

					//Tooltip
					else if (desc[0] == '[') {
						int i2 = desc.IndexOf("|");
						int i3 = desc.IndexOf("]");
						if (i2 > 0 && i3 > 0) {
							location = MTG_InsertTooltip(desc.Substring(1, i2 - 1), desc.Substring(i2 + 1, (i3 - i2) - 1), box, location);
							desc = desc.Substring(i3 + 1);
						}
						else { desc = desc.Substring(1); }
					}

					//Cardtip
					else if (desc[0] == '<') {
						int i2 = desc.IndexOf("|");
						int i3 = desc.IndexOf(">");
						if (i2 > 0 && i3 > 0) {
							location = MTG_InsertCardtip(desc.Substring(1, i2 - 1), desc.Substring(i2 + 1, (i3 - i2) - 1), box, location);
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
						location = MTG_WriteDescription(desc, box, location);
						break;
					}

					//Write until next object, clear written text, and continue
					else {
						string substr = desc.Substring(0, i);
						if (substr.EndsWith(" ")) { substr = substr.Substring(0, substr.Length - 1); }
						location = MTG_WriteDescription(substr, box, location);
						desc = desc.Substring(i);
					}

				}

			}

			//Return y delta
			return location.Y + TEXT_HEIGHT - y;

		}

		//Write description text. Returns new text position
		private Point MTG_WriteDescription(string desc, GroupBox box, Point location) {

			//Loop line breaks
			string[] lines = desc.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);
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
		private List<string> MTG_GetSymbols(string str) {
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
		private Point MTG_InsertTooltip(string str, string tooltip, Control control, Point location) {
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
			label.MouseEnter += new EventHandler((sender, e) => MTG_ShowTooltip(label, tooltip));
			label.MouseLeave += new EventHandler((sender, e) => mtgTooltipBox.Hide());
			location = new Point(location.X + textWidth, location.Y);
			return location;
		}

		//Insert clickable cardtip text at position. Returns position at end of added text
		private Point MTG_InsertCardtip(string str, string cardtip, Control control, Point location) {
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
			label.Click += new EventHandler((sender, e) => MTG_LoadCardtip(cardtip));
			label.MouseEnter += new EventHandler((sender, e) => MTG_ShowCardtip(label, cardtip));
			label.MouseLeave += new EventHandler((sender, e) => mtgCardtipBox.Hide());
			location = new Point(location.X + textWidth, location.Y);
			return location;
		}

		//Insert symbol into control at position. Returns position at end of symbol
		private Point MTG_InsertSymbol(string str, Control control, Point location, int height) {

			//Find symbol object for given symbol string
			MTG_Symbol symbol = mtgCatalog.symbols.FirstOrDefault(s => s.symbol == str);

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
			MTG_Utils.TryLoadImage(icon, symbol.imgPath);
			location = new Point(location.X + width, location.Y);
			return location;

		}

		#endregion

		#region Detail Utils

		//Show tooltip window relative to given control with given text
		private void MTG_ShowTooltip(Control control, string str) {
			int posX = control.Parent.Location.X + control.Location.X + (control.Width / 2) - (mtgTooltipBox.Width / 2);
			int posY = control.Parent.Location.Y + control.Location.Y + TEXT_HEIGHT;
			mtgTooltipBox.Show();
			mtgTooltipBox.BringToFront();
			mtgTooltipBox.Location = new Point(posX, posY);
			mtgTooltipBox.Controls.Clear();
			int height = MTG_GenerateDescription(str, mtgTooltipBox, new Point(5, 15));
			mtgTooltipBox.Size = new Size(mtgTooltipBox.Width, height + 20);
		}

		//Show tooltip window relative to given control with given text
		private void MTG_ShowCardtip(Control control, string str) {

			//Get modifier
			char mod = ' ';
			if (str.Contains('|')) {
				mod = str[str.Length - 1];
				str = str.Substring(0, str.Length - 2);
			}

			//Load printing
			MTG_Printing print = mtgCatalog.printings.FirstOrDefault(p => p.scryfallID.Equals(str));
			if (print != null) {

				//Size
				if (mod == 's' || mod == 'S') {
					mtgCardtipBox.Size = new Size(350, 250);
					mtgCardtipImage.Size = new Size(350, 250);
				}
				else {
					mtgCardtipBox.Size = new Size(250, 350);
					mtgCardtipImage.Size = new Size(250, 350);
				}

				//Position
				int posX = control.Parent.Location.X + control.Location.X + (control.Width / 2) - (mtgCardtipBox.Width / 2);
				int posY = control.Parent.Location.Y + control.Location.Y + TEXT_HEIGHT;

				//Show
				mtgCardtipBox.Show();
				mtgCardtipBox.BringToFront();
				mtgCardtipBox.Location = new Point(posX, posY);

				//Load image
				if (mod == 'b' || mod == 'B') { MTG_Utils.TryLoadCardImage(mtgCardtipImage, print.backImgPath); }
				else { MTG_Utils.TryLoadCardImage(mtgCardtipImage, print.imgPath); }

				//Rotation
				Image img = mtgCardtipImage.Image;
				if (mod == 'u' || mod == 'U') { img.RotateFlip(RotateFlipType.Rotate180FlipNone); }
				if (mod == 's' || mod == 'S') { img.RotateFlip(RotateFlipType.Rotate90FlipNone); }

			}

		}

		//Show tooltip window relative to given control with given text
		private void MTG_LoadCardtip(string str) {
			MTG_Printing print = mtgCatalog.printings.FirstOrDefault(p => p.scryfallID.Equals(str));
			if (print != null) { MTG_LoadCardDetails(print); }
		}

		//Load location table
		private void MTG_LoadLocationTable(MTG_Printing print) {

			//Clear
			mtgDetailBox.Controls.Remove(mtgLocationTable);
			mtgLocationTable.Controls.Clear();
			mtgLocationTable.RowCount = 0;
			mtgLocationTable.RowStyles.Clear();
			mtgLocationTable.Size = new Size(mtgLocationTable.Size.Width, 10);

			//Loop rarities and locations
			foreach (MTG_Treatment treatment in print.treatments) {
				for (int i = 0; i < treatment.locations.Count; ++i) {
					string rar = treatment.name;
					string loc = treatment.locations[i];
					mtgLocationTable.Size = new Size(mtgLocationTable.Size.Width, mtgLocationTable.Size.Height + 35);

					//Add row
					++mtgLocationTable.RowCount;
					mtgLocationTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));

					//Rarity label
					Label treatmentLabel = new Label();
					mtgLocationTable.Controls.Add(treatmentLabel, 0, mtgLocationTable.RowCount - 1);
					treatmentLabel.Dock = DockStyle.Fill;
					treatmentLabel.TextAlign = ContentAlignment.MiddleLeft;
					treatmentLabel.Text = rar;
					treatmentLabel.AutoEllipsis = true;

					//Location label
					Label locationLabel = new Label();
					mtgLocationTable.Controls.Add(locationLabel, 1, mtgLocationTable.RowCount - 1);
					locationLabel.Dock = DockStyle.Fill;
					locationLabel.TextAlign = ContentAlignment.MiddleLeft;
					locationLabel.Text = loc;
					locationLabel.AutoEllipsis = true;

					//Count label
					Label countLabel = new Label();
					mtgLocationTable.Controls.Add(countLabel, 2, mtgLocationTable.RowCount - 1);
					countLabel.Dock = DockStyle.Fill;
					countLabel.TextAlign = ContentAlignment.MiddleLeft;
					countLabel.Text = treatment.quantities[i].ToString();

					//Move button
					Button moveButton = new Button();
					mtgLocationTable.Controls.Add(moveButton, 3, mtgLocationTable.RowCount - 1);
					moveButton.Dock = DockStyle.Fill;
					moveButton.Text = "Move 1";
					moveButton.UseVisualStyleBackColor = true;
					moveButton.Click += new EventHandler((sender, e) => MTG_MoveOne(print, loc, rar));

				}
			}

			//Resume
			mtgDetailBox.Controls.Add(mtgLocationTable);
			mtgDetailBox.Size = new Size(mtgDetailBox.Size.Width, mtgLocationTable.Size.Height + 100);

		}

		//Move one card of a given rarity from one location to another
		private void MTG_MoveOne(MTG_Printing print, string from, string treatment) {
			if (mtgMoveField.Text.Length > 0) {
				print.MoveOne(from, mtgMoveField.Text, treatment);
				MTG_LoadLocationTable(print);
			}
		}

		//Reload location data
		private void MTG_OnClickReloadLocations(object sender, EventArgs e) {
			mtgMoveField.Items.Clear();
			mtgSearchLocationField.Items.Clear();
			mtgSearchLocationField.Items.Add("--");
			foreach (string name in mtgCatalog.printings.SelectMany(p => p.treatments).SelectMany(t => t.locations).Distinct()) { mtgMoveField.Items.Add(name); }
			foreach (string name in mtgCatalog.printings.SelectMany(p => p.treatments).SelectMany(t => t.locations).Distinct()) { mtgSearchLocationField.Items.Add(name); }
		}

		//Load printings list
		private void MTG_LoadPrintingsList(MTG_Card card, MTG_Printing curPrint) {
			mtgPrintingsBox.Controls.Clear();
			if (card.name.Equals("_"))
				return;
			List<MTG_Printing> prints = mtgCatalog.printings.Where(p => p.card == card).ToList();
			prints.Sort(new PrintComparerNumeric().Compare);
			for (int i = 0; i < prints.Count; ++i) {
				Label label = new Label();
				label.Font = Utils.FONT_UNDERLINE;
				if (prints[i] != curPrint)
					label.ForeColor = Color.Blue;
				mtgPrintingsBox.Controls.Add(label);
				label.Location = new Point(5, 20 + (i * 30));
				label.Size = new Size(mtgPrintingsBox.Width - 10, TEXT_HEIGHT);
				label.Text = prints[i].scryfallID.ToUpper() + " - " + prints[i].set.name;
				label.TextAlign = ContentAlignment.MiddleLeft;
				string id = prints[i].scryfallID;
				if (prints[i] != curPrint)
					label.Click += new EventHandler((sender, e) => MTG_LoadCardtip(id));
				label.MouseEnter += new EventHandler((sender, e) => MTG_ShowCardtip(label, id));
				label.MouseLeave += new EventHandler((sender, e) => mtgCardtipBox.Hide());
			}
			mtgPrintingsBox.Height = 20 + (prints.Count * 30);
		}

		//Flip card image
		private void MTG_FlipCard(object sender, EventArgs e) => MTG_FlipCard();
		private void MTG_FlipCard() {

			//Return if no reference set
			if (mtgDetailPrint == null) { return; }

			//Only flip if card has a second face
			if (!mtgDetailPrint.card.name.Contains(" // ")) { return; }

			//Get printing and flip
			mtgDetailFlipped = !mtgDetailFlipped;

			//Swap image if there's a back image reference
			if (mtgDetailPrint.backImgPath.Length > 1) {
				if (mtgDetailFlipped) { MTG_Utils.TryLoadCardImage(mtgDetailImgbox, mtgDetailPrint.backImgPath); }
				else { MTG_Utils.TryLoadImage(mtgDetailImgbox, mtgDetailPrint.imgPath); }
			}

			//Otherwise rotate 180
			else { mtgDetailImgbox.Image.RotateFlip(RotateFlipType.Rotate180FlipNone); }

		}

		//Edit card data
		private void MTG_EditCard(object sender, EventArgs e) => MTG_EditCard();
		private void MTG_EditCard() {

			//Return if print or reference card are invalid
			if (mtgDetailPrint == null) { return; }
			MTG_Card card = mtgDetailPrint.card;
			if (card == null) { return; }

			//Setup edit page
			mtgNameField.Text = card.name;
			mtgIdentityW.Checked = card.identity.HasFlag(MTG_Colour.White);
			mtgIdentityU.Checked = card.identity.HasFlag(MTG_Colour.Blue);
			mtgIdentityB.Checked = card.identity.HasFlag(MTG_Colour.Black);
			mtgIdentityR.Checked = card.identity.HasFlag(MTG_Colour.Red);
			mtgIdentityG.Checked = card.identity.HasFlag(MTG_Colour.Green);
			mtgColourW.Checked = card.colour.HasFlag(MTG_Colour.White);
			mtgColourU.Checked = card.colour.HasFlag(MTG_Colour.Blue);
			mtgColourB.Checked = card.colour.HasFlag(MTG_Colour.Black);
			mtgColourR.Checked = card.colour.HasFlag(MTG_Colour.Red);
			mtgColourG.Checked = card.colour.HasFlag(MTG_Colour.Green);
			mtgCostField.Text = card.cost;
			mtgCardTypeField.Text = card.cardTypes;
			mtgOracleTextField.Text = card.oracleText;
			mtgPowerField.Value = card.power;
			mtgToughnessField.Value = card.toughness;
			mtgPowerBackField.Value = card.power2;
			mtgToughnessBackField.Value = card.toughness2;
			mtgUpdateCard = card;
			mtgAddCardButton.Text = "Update Card";
			mtgTabControl.SelectedTab = mtgCardPage;

		}

		//Edit printing data
		private void MTG_EditPrint(object sender, EventArgs e) => MTG_EditPrint();
		private void MTG_EditPrint() {

			//Return if print is invalid
			if (mtgDetailPrint == null) { return; }
			MTG_Printing print = mtgDetailPrint;

			//Setup edit page
			mtgSetField.SelectedItem = print.set;
			mtgNumberField.Value = print.cardNumber;
			mtgRarityField.Text = print.rarity;
			mtgTreatments.Clear();
			foreach (MTG_Treatment treatment in print.treatments) { mtgTreatments.Add(treatment.name); }
			MTG_UpdateTreatmentList();
			mtgFlavorTextField.Text = print.flavorText;
			mtgImgpathLabel.Text = print.imgPath;
			MTG_Utils.TryLoadCardImage(mtgPrintImgbox, print.imgPath);
			mtgImgpathBackLabel.Text = print.backImgPath;
			if (print.backImgPath.Length > 1) { MTG_Utils.TryLoadCardImage(mtgPrintImgboxBack, print.backImgPath); }
			else { /*Load default*/ }
			mtgCardrefField.SelectedItem = print.card;
			mtgScryfallField.Text = print.scryfallID;
			mtgUpdatePrint = print;
			mtgAddPrintButton.Text = "Update Printing";
			mtgTabControl.SelectedTab = mtgPrintPage;

		}

		//Navigation
		private void MTG_LoadPreviousInSelection(object sender, EventArgs e) => MTG_LoadPreviousInSelection();
		private void MTG_LoadPreviousInSelection() {
			if (mtgDetailPrev != null)
				MTG_LoadCardDetails(mtgDetailPrev);
		}
		private void MTG_LoadNextInSelection(object sender, EventArgs e) => MTG_LoadNextInSelection();
		private void MTG_LoadNextInSelection() {
			if (mtgDetailNext != null)
				MTG_LoadCardDetails(mtgDetailNext);
		}

		//Autogen card details, then move to the next card in the set every two seconds
		private async void MTG_AutogenDetailRef(object sender, EventArgs e) {
			await MTG_AutogenDetailRef();
			while (mtgDetailNext != null) {
				Thread.Sleep(250);
				MTG_LoadCardDetails(mtgDetailNext);
				Thread.Sleep(250);
				await MTG_AutogenDetailRef();
			}
		}

		//Get card name from Scryfall, generate card object if it doesn't exist, then set printing reference
		private async Task MTG_AutogenDetailRef() { 

			//Set up result and get card page
			string name = "";
			string result = await MTG_GetWebpage("https://api.scryfall.com/cards/" + mtgDetailPrint.scryfallID);

			//Return if error
			if (result.Contains("Error: ")) {
				mtgDetailDialog.Text = result;
				return;
			}

			//Find name
			if (result.Contains("\"name\":")) {
				int idx = result.IndexOf("\"name\":");
				name = result.Substring(idx + 8);
				idx = name.IndexOf("\",\"");
				name = name.Substring(0, idx);
				mtgNameField.Text = name;
			}

			//Find identity
			if (result.Contains("\"color_identity\":")) {
				int idx = result.IndexOf("\"color_identity\":");
				string identity = result.Substring(idx + 18);
				idx = identity.IndexOf("],");
				identity = identity.Substring(0, idx);
				mtgIdentityW.Checked = identity.Contains("W");
				mtgIdentityU.Checked = identity.Contains("U");
				mtgIdentityB.Checked = identity.Contains("B");
				mtgIdentityR.Checked = identity.Contains("R");
				mtgIdentityG.Checked = identity.Contains("G");
			}

			//Find colors
			if (result.Contains("\"colors\":")) {
				int idx = result.IndexOf("\"colors\":");
				string color = result.Substring(idx + 10);
				idx = color.IndexOf("],");
				color = color.Substring(0, idx);
				mtgColourW.Checked = color.Contains("W");
				mtgColourU.Checked = color.Contains("U");
				mtgColourB.Checked = color.Contains("B");
				mtgColourR.Checked = color.Contains("R");
				mtgColourG.Checked = color.Contains("G");
			}

			//Find type line
			if (result.Contains("\"type_line\":")) {
				int idx = result.IndexOf("\"type_line\":");
				string type = result.Substring(idx + 13);
				idx = type.IndexOf("\",\"");
				type = type.Substring(0, idx);
				mtgCardTypeField.Text = type;
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
				mtgCostField.Text = cost;
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
				mtgOracleTextField.Text = oracle;
			}

			//Find powers
			if (result.Contains("\"power\":")) {
				int idx = result.IndexOf("\"power\":");
				string powers = result.Substring(idx + 9);
				idx = powers.IndexOf("\",\"");
				string power = powers.Substring(0, idx);
				try { mtgPowerField.Value = decimal.Parse(power); }
				catch { }
				if (powers.Contains("\"power\":")) {
					idx = powers.IndexOf("\"power\":");
					string power2 = powers.Substring(idx + 9);
					idx = power2.IndexOf("\",\"");
					power2 = power2.Substring(0, idx);
					try { mtgPowerBackField.Value = decimal.Parse(power2); }
					catch { }
				}
			}

			//Find toughnesses
			if (result.Contains("\"toughness\":")) {
				int idx = result.IndexOf("\"toughness\":");
				string toughnesses = result.Substring(idx + 13);
				idx = toughnesses.IndexOf("\",\"");
				string toughness = toughnesses.Substring(0, idx);
				try { mtgToughnessField.Value = decimal.Parse(toughness); }
				catch { }
				if (toughnesses.Contains("\"toughness\":")) {
					idx = toughnesses.IndexOf("\"toughness\":");
					string toughness2 = toughnesses.Substring(idx + 13);
					idx = toughness2.IndexOf("\",\"");
					toughness2 = toughness2.Substring(0, idx);
					try { mtgToughnessBackField.Value = decimal.Parse(toughness2); }
					catch { }
				}
			}

			//Generate card
			mtgIgnoreDuplicateEntryBox.Checked = false;
			MTG_AddCard();
			MTG_Card card = mtgCatalog.cards.FirstOrDefault(c => c.name.Equals(name));
			if (card != null) { mtgDetailPrint.card = card; }
			MTG_LoadCardDetails(mtgDetailPrint);
			mtgDetailDialog.Text = result;

		}

		//HTTP Client
		static readonly HttpClient mtgClient = new HttpClient();
		static async Task<string> MTG_GetWebpage(string url) {
			try {
				string data = await mtgClient.GetStringAsync(url);
				return data;
			}
			catch (Exception e) {
				string error = "Error: " + e.ToString();
				return error;
			}
		}

		//Delete printing from catalog
		private void MTG_DeleteCurrentPrinting(object sender, EventArgs e) {
			int index = mtgCatalog.printings.IndexOf(mtgDetailPrint);
			mtgCatalog.printings.RemoveAt(index);
			MTG_UpdateSets();
			mtgDetailPrint = null;
		}

		#endregion

		#endregion

		#region Card Entry

		//Check if card exists with name
		private void MTG_CheckCardNameExists() {
			if (mtgCatalog.cards.Select(card => card.name).Contains(mtgNameField.Text)) {
				mtgCardDialog.Text = mtgNameField.Text + " already exists";
			}
		}

		//Add card to catalog
		private void MTG_OnClickAddCard(object sender, EventArgs e) => MTG_AddCard();
		private void MTG_AddCard() {

			//Get colour identity
			MTG_Colour identity = MTG_Colour.None;
			MTG_Colour colour = MTG_Colour.None;
			if (mtgIdentityW.Checked) { identity |= MTG_Colour.White; }
			if (mtgIdentityU.Checked) { identity |= MTG_Colour.Blue; }
			if (mtgIdentityB.Checked) { identity |= MTG_Colour.Black; }
			if (mtgIdentityR.Checked) { identity |= MTG_Colour.Red; }
			if (mtgIdentityG.Checked) { identity |= MTG_Colour.Green; }
			if (mtgColourW.Checked) { colour |= MTG_Colour.White; }
			if (mtgColourU.Checked) { colour |= MTG_Colour.Blue; }
			if (mtgColourB.Checked) { colour |= MTG_Colour.Black; }
			if (mtgColourR.Checked) { colour |= MTG_Colour.Red; }
			if (mtgColourG.Checked) { colour |= MTG_Colour.Green; }

			//Generate card object
			MTG_Card card = new MTG_Card(
				mtgNameField.Text,
				identity,
				colour,
				mtgCostField.Text,
				mtgCardTypeField.Text,
				mtgOracleTextField.Text,
				(int)mtgPowerField.Value,
				(int)mtgToughnessField.Value,
				(int)mtgPowerBackField.Value,
				(int)mtgToughnessBackField.Value
			);

			//Create or update card in catalog
			if (mtgUpdateCard == null) {
				if (mtgCatalog.cards.Select(c => c.name).Contains(mtgNameField.Text) && !mtgIgnoreDuplicateEntryBox.Checked) {
					mtgCardDialog.Text = mtgNameField.Text + " already exists";
					return;
				}
				mtgCatalog.cards.Add(card);
				MTG_UpdateCardList();
				mtgCardDialog.Text = "-";
			}
			else {
				mtgUpdateCard.Copy(card);
				mtgCardDialog.Text = "Card Data Updated";
				if (mtgDetailPrint.card == mtgUpdateCard) { MTG_LoadCardDetails(mtgDetailPrint); }
			}

			//Reset fields
			mtgNameField.Text = "";
			mtgIdentityW.Checked = false;
			mtgIdentityU.Checked = false;
			mtgIdentityB.Checked = false;
			mtgIdentityR.Checked = false;
			mtgIdentityG.Checked = false;
			mtgColourW.Checked = false;
			mtgColourU.Checked = false;
			mtgColourB.Checked = false;
			mtgColourR.Checked = false;
			mtgColourG.Checked = false;
			mtgCostField.Text = "";
			mtgCardTypeField.Text = "";
			mtgOracleTextField.Text = "";
			mtgPowerField.Value = 0;
			mtgToughnessField.Value = 0;
			mtgPowerBackField.Value = 0;
			mtgToughnessBackField.Value = 0;
			mtgIgnoreDuplicateEntryBox.Checked = false;
			mtgUpdateCard = null;
			mtgAddCardButton.Text = "Add To Catalog";

		}

		#endregion

		#region Printing Entry

		//Clear and refresh list of card names
		private void MTG_UpdateCardList() {
			mtgCardrefField.Items.Clear();
			foreach (MTG_Card card in mtgCatalog.cards) { mtgCardrefField.Items.Add(card); }
		}

		//Update monster list label
		private void MTG_UpdateTreatmentList() {
			if (mtgTreatments.Count == 0) {
				mtgTreatmentsValue.Text = "-";
				return;
			}
			string s = "";
			for (int i = 0; i < mtgTreatments.Count; ++i) {
				if (i > 0) { s += ", "; }
				s += mtgTreatments[i];
			}
			mtgTreatmentsValue.Text = s;
		}

		//Add rarity to list
		private void MTG_OnClickAddTreatment(object sender, EventArgs e) {
			if (mtgTreatmentField.Text.Length <= 0) { return; }
			if (mtgTreatments.Contains(mtgTreatmentField.Text)) { return; }
			mtgTreatments.Add(mtgTreatmentField.Text);
			MTG_UpdateTreatmentList();
		}

		//Remove rarity from list
		private void MTG_OnClickSubTreatment(object sender, EventArgs e) {
			if (mtgTreatments.Count == 0) { return; }
			mtgTreatments.RemoveAt(mtgTreatments.Count - 1);
			MTG_UpdateTreatmentList();
		}

		//Get image path and display card
		private void MTG_OnClickSearchImg(object sender, EventArgs e) {
			if (imageFileDialog.ShowDialog() == DialogResult.OK) {
				string curDir = Directory.GetCurrentDirectory();
				string filePath = imageFileDialog.FileName;
				if (!filePath.Contains(curDir)) {
					mtgImgpathLabel.Text = "Selected file is not locally referrable";
					return;
				}
				filePath = filePath.Remove(filePath.IndexOf(curDir), curDir.Length + 1);
				mtgImgpathLabel.Text = filePath;
				MTG_Utils.TryLoadCardImage(mtgPrintImgbox, filePath);
			}
		}

		//Get image path and display card back
		private void MTG_OnClickSearchImgBack(object sender, EventArgs e) {
			if (imageFileDialog.ShowDialog() == DialogResult.OK) {
				string curDir = Directory.GetCurrentDirectory();
				string filePath = imageFileDialog.FileName;
				if (!filePath.Contains(curDir)) {
					mtgImgpathBackLabel.Text = "Selected file is not locally referrable";
					return;
				}
				filePath = filePath.Remove(filePath.IndexOf(curDir), curDir.Length + 1);
				mtgImgpathBackLabel.Text = filePath;
				MTG_Utils.TryLoadCardImage(mtgPrintImgboxBack, filePath);
			}
		}

		//Autofill image
		private void MTG_OnClickPrintAutofill(object sender, EventArgs e) => MTG_OnClickPrintAutofill();
		private bool MTG_OnClickPrintAutofill() {
			MTG_Set set = (MTG_Set)mtgSetField.SelectedItem;
			if (set != null) {
				mtgCardrefField.SelectedIndex = 1;
				string code = set.code;
				string num = mtgNumberField.Value.ToString().PadLeft(4, '0');
				mtgScryfallField.Text = code.ToLower() + "/" + mtgNumberField.Value.ToString();
				if (mtgPrintTokenCheck.Checked) {
					num = code[0] + num;
					code = code.Substring(1);
				}
				string path = "resources/mtg/" + code + "/" + num;
				mtgCardrefDescriptor.Text = path;
				if (MTG_Utils.TryLoadCardImage(mtgPrintImgbox, path + ".png", mtgImgpathLabel)) { return true; }
				else {
					if (MTG_Utils.TryLoadCardImage(mtgPrintImgbox, path + "a.png", mtgImgpathLabel)) {
						if (MTG_Utils.TryLoadCardImage(mtgPrintImgboxBack, path + "b.png", mtgImgpathBackLabel)) {
							return true;
						}
					}
				}
			}
			return false;
		}

		//Populate descriptor when card reference is selected
		private void MTG_OnSelectCardref(object sender, EventArgs e) {

			//Return if index or card is invalid
			if (mtgCardrefField.SelectedIndex < 0) { return; }
			MTG_Card card = (MTG_Card)mtgCardrefField.SelectedItem;
			if (card == null) { return; }

			//Build description string
			string s = "";
			string o = MTG_Utils.CleanOracleText(card.oracleText);
			if (card.cardTypes.Contains("Planeswalker"))
				s += card.toughness.ToString() + " Loyalty ";
			if (card.cardTypes.Contains("Creature") || card.cardTypes.Contains("Vehicle"))
				s += card.power.ToString() + '/' + card.toughness.ToString() + ' ';
			s += MTG_Utils.colourNames[card.colour] + ' ';
			s += card.cardTypes;
			if (o.Length > 0) { s += " with " + o; }

			//Populate descriptor
			mtgCardrefDescriptor.Text = s;

		}

		//Add printing to catalog
		private void MTG_OnClickAddPrint(object sender, EventArgs e) => MTG_OnClickAddPrint();
		private void MTG_OnClickAddPrint() {

			//Return if no set, treatments, images, or card reference set
			if (mtgSetField.SelectedIndex < 0) { return; }
			if (mtgTreatments.Count == 0) { return; }
			if (mtgImgpathLabel.Text.Length <= 1) { return; }
			if (mtgCardrefField.SelectedIndex < 0) { return; }

			//Generate printing object
			MTG_Printing print = new MTG_Printing(
				(MTG_Set)mtgSetField.SelectedItem,
				(int)mtgNumberField.Value,
				mtgFlavorTextField.Text,
				mtgImgpathLabel.Text,
				mtgImgpathBackLabel.Text,
				mtgRarityField.Text,
				MTG_Treatment.GenerateTreatments(mtgTreatments),
				(MTG_Card)mtgCardrefField.SelectedItem,
				mtgScryfallField.Text
			);

			//Create or update printing in catalog
			if (mtgUpdatePrint == null) {
				mtgCatalog.printings.Add(print);
				MTG_UpdateSets();
				mtgImgpathLabel.Text = "-";
				mtgImgpathBackLabel.Text = "";
			}
			else {
				mtgUpdatePrint.Copy(print);
				mtgImgpathLabel.Text = "Printing Data Updated";
				if (mtgDetailPrint == mtgUpdatePrint) { MTG_LoadCardDetails(mtgDetailPrint); }
			}

			//Reset fields
			mtgNumberField.Value += 1;
			mtgRarityField.SelectedIndex = -1;
			mtgFlavorTextField.Text = "";
			mtgCardrefField.SelectedIndex = -1;
			int index = mtgScryfallField.Text.IndexOf("/");
			if (index > 0) {
				mtgScryfallField.Text = mtgScryfallField.Text.Substring(0, index + 1);
				mtgScryfallField.Text += ((int)mtgNumberField.Value).ToString();
			}
			else { mtgScryfallField.Text = ""; }
			mtgPrintImgbox.Image = null;
			MTG_Utils.TryLoadCardImage(mtgPrintImgboxBack, MTG_Utils.CARD_BACK_PATH);
			mtgCardrefDescriptor.Text = "-";
			mtgUpdatePrint = null;
			mtgAddPrintButton.Text = "Add To Catalog";

		}

		//Add card to catalog and autofill next
		private void MTG_OnClickAddAndFill(object sender, EventArgs e) {
			int lastIndex = -1;
			while (true) {
				MTG_OnClickAddPrint();
				if (mtgNumberField.Value == lastIndex) { break; }
				lastIndex = (int)mtgNumberField.Value;
				if (!MTG_OnClickPrintAutofill()) { break; }
				if (lastIndex > mtgPrintAutoLimit.Value) { break; }
			}
		}

		#endregion

		#region Symbols

		//Properties
		private List<MTG_FormSymbol> mtgFormSymbols = new List<MTG_FormSymbol>();

		//Symbol form class
		private class MTG_FormSymbol {
			public GroupBox box;
			public TextBox nameBox;
			public TextBox symbolBox;
			public NumericUpDown aspectBox;
			public Label pathLabel;
			public PictureBox iconBox;
			public MTG_FormSymbol() : this(null, null, null, null, null, null) { }
			public MTG_FormSymbol(GroupBox box, TextBox nameBox, TextBox symbolBox, NumericUpDown aspectBox, Label pathLabel, PictureBox iconBox) {
				this.box = box;
				this.nameBox = nameBox;
				this.symbolBox = symbolBox;
				this.aspectBox = aspectBox;
				this.pathLabel = pathLabel;
				this.iconBox = iconBox;
			}
		}

		//Regenerate symbol controls
		private void MTG_RegenerateSymbols() {
			foreach (MTG_FormSymbol symbol in mtgFormSymbols) { mtgSymbolLayout.Controls.Remove(symbol.box); }
			mtgFormSymbols.Clear();
			if (mtgCatalog.symbols == null) { mtgCatalog.symbols = new List<MTG_Symbol>(); }
			foreach (MTG_Symbol symbol in mtgCatalog.symbols) {
				if (symbol.aspect < 0.01m) { symbol.aspect = 0.01m; }
				if (symbol.aspect > 100.0m) { symbol.aspect = 100.0m; }
				MTG_CreateSymbolBox(symbol);
			}
		}

		//Add new symbol
		private void MTG_OnClickAddSymbol(object sender, EventArgs e) => MTG_CreateSymbolBox();
		private void MTG_CreateSymbolBox() => MTG_CreateSymbolBox(new MTG_Symbol(), false);
		private void MTG_CreateSymbolBox(MTG_Symbol refSymbol, bool useRef = true) {

			//Index
			int i = mtgFormSymbols.Count;

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
			icon.Click += new EventHandler((sender, e) => MTG_OnClickSearchSymbol(i));

			//Load image if one is referenced
			if (refSymbol.imgPath.Length > 0) {
				path.Text = refSymbol.imgPath;
				MTG_Utils.TryLoadImage(icon, refSymbol.imgPath);
			}

			//Create object and add box to layout
			mtgFormSymbols.Add(new MTG_FormSymbol(box, name, symbol, aspect, path, icon));
			mtgSymbolLayout.Controls.Add(box);

		}

		//Get image path and display card
		private void MTG_OnClickSearchSymbol(int index) {
			if (imageFileDialog.ShowDialog() == DialogResult.OK) {
				string curDir = Directory.GetCurrentDirectory();
				string filePath = imageFileDialog.FileName;
				if (!filePath.Contains(curDir)) { return; }
				filePath = filePath.Remove(filePath.IndexOf(curDir), curDir.Length + 1);
				mtgFormSymbols[index].pathLabel.Text = filePath;
				MTG_Utils.TryLoadImage(mtgFormSymbols[index].iconBox, filePath);
			}
		}

		//Save current symbol list
		private void MTG_OnClickSaveSymbols(object sender, EventArgs e) {
			mtgCatalog.symbols.Clear();
			foreach(MTG_FormSymbol symbol in mtgFormSymbols) {
				mtgCatalog.symbols.Add(
					new MTG_Symbol(
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
		public int mtgSetGeneratorPagenum = 0;
		public int mtgSetGeneratorSetsPage = 25;
		private List<MTG_FormSet> mtgFormSets = new List<MTG_FormSet>();

		//Paging
		private void MTG_OnClickSetGeneratorPrev(object sender, EventArgs e) {
			--mtgSetGeneratorPagenum;
			MTG_RegenerateSets();
		}
		private void MTG_OnClickSetGeneratorNext(object sender, EventArgs e) {
			++mtgSetGeneratorPagenum;
			MTG_RegenerateSets();
		}

		//Symbol form class
		private class MTG_FormSet {
			public MTG_Set set;
			public GroupBox box;
			public TextBox nameBox;
			public TextBox codeBox;
			public DateTimePicker dateBox;
			public NumericUpDown orderBox;
			public NumericUpDown indentBox;
			public Label pathLabel;
			public PictureBox iconBox;
			public MTG_FormSet() : this(null, null, null, null, null, null, null, null, null) { }
			public MTG_FormSet(MTG_Set set, GroupBox box, TextBox nameBox, TextBox codeBox, DateTimePicker dateBox, NumericUpDown orderBox, NumericUpDown indentBox, Label pathLabel, PictureBox iconBox) {
				this.set = set;
				this.box = box;
				this.nameBox = nameBox;
				this.codeBox = codeBox;
				this.dateBox = dateBox;
				this.orderBox = orderBox;
				this.indentBox = indentBox;
				this.pathLabel = pathLabel;
				this.iconBox = iconBox;
			}
		}

		//Regenerate symbol controls
		private void MTG_RegenerateSets(object sender, EventArgs e) => MTG_RegenerateSets();
		private void MTG_RegenerateSets() {

			//Quick null check
			if (mtgCatalog.sets == null) { mtgCatalog.sets = new List<MTG_Set>(); }

			//Pagination
			int maxPage = mtgCatalog.sets.Count / mtgSetGeneratorSetsPage;
			if (mtgSetGeneratorPagenum > maxPage) { mtgSetGeneratorPagenum = 0; }
			if (mtgSetGeneratorPagenum < 0) { mtgSetGeneratorPagenum = maxPage; }
			int startIndex = mtgSetGeneratorPagenum * mtgSetGeneratorSetsPage;
			int maxIndex = mtgSetGeneratorSetsPage;
			if (mtgSetGeneratorPagenum == maxPage) { maxIndex = mtgCatalog.sets.Count % mtgSetGeneratorSetsPage; }
			maxIndex += startIndex;
			mtgSetGeneratorPageLabel.Text = (mtgSetGeneratorPagenum + 1) + " / " + (maxPage + 1);

			//Remove old
			mtgSetGeneratorLayout.SuspendLayout();
			foreach (MTG_FormSet set in mtgFormSets) { mtgSetGeneratorLayout.Controls.Remove(set.box); }
			mtgFormSets.Clear();

			//Add new
			for (int i = startIndex; i < maxIndex; ++i) {
				MTG_Set set = mtgCatalog.sets[i];
				MTG_CreateSetGeneratorBox(set);
			}
			mtgSetGeneratorLayout.ResumeLayout();

		}

		//Add new symbol
		private void MTG_OnClickAddSet(object sender, EventArgs e) => MTG_CreateSetGeneratorBox();
		private void MTG_CreateSetGeneratorBox() => MTG_CreateSetGeneratorBox(new MTG_Set(), false);
		private void MTG_CreateSetGeneratorBox(MTG_Set refSet, bool useRef = true) {

			//Index
			int i = mtgFormSets.Count;

			//Group box
			GroupBox box = new GroupBox();
			box.Size = new Size(350, 190);

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

			//Order box
			NumericUpDown order = new NumericUpDown();
			box.Controls.Add(order);
			order.Location = new Point(5, 120);
			order.Size = new Size(168, 30);
			order.Increment = 1;
			order.Minimum = 0;
			order.Value = useRef ? refSet.order : 0;

			//Order box
			NumericUpDown indent = new NumericUpDown();
			box.Controls.Add(indent);
			indent.Location = new Point(177, 120);
			indent.Size = new Size(168, 30);
			indent.Increment = 1;
			indent.Minimum = 0;
			indent.Value = useRef ? refSet.indent : 0;

			//Path label
			Label path = new Label();
			box.Controls.Add(path);
			path.Location = new Point(5, 155);
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
				MTG_Utils.TryLoadImage(icon, refSet.imgPath);
			}

			//Create object and set up search event
			MTG_FormSet set = new MTG_FormSet(useRef ? refSet : null, box, name, code, date, order, indent, path, icon);
			icon.Click += new EventHandler((sender, e) => MTG_OnClickSearchSetIcon(set));

			//Add to list and layout
			mtgFormSets.Add(set);
			mtgSetGeneratorLayout.Controls.Add(box);

		}

		//Get image path and display card
		private void MTG_OnClickSearchSetIcon(MTG_FormSet set) {
			if (imageFileDialog.ShowDialog() == DialogResult.OK) {
				string curDir = Directory.GetCurrentDirectory();
				string filePath = imageFileDialog.FileName;
				if (!filePath.Contains(curDir)) { return; }
				filePath = filePath.Remove(filePath.IndexOf(curDir), curDir.Length + 1);
				set.pathLabel.Text = filePath;
				MTG_Utils.TryLoadImage(set.iconBox, filePath);
			}
			else {
				set.pathLabel.Text = "";
				set.iconBox.Image = null;
			}
		}

		//Save current symbol list
		private void MTG_OnClickSaveSets(object sender, EventArgs e) {
			foreach (MTG_FormSet formSet in mtgFormSets) {
				MTG_Set set = new MTG_Set(
					formSet.nameBox.Text,
					formSet.codeBox.Text,
					formSet.pathLabel.Text,
					formSet.dateBox.Value,
					(int)formSet.orderBox.Value,
					(int)formSet.indentBox.Value
				);
				if (formSet.set == null) { mtgCatalog.sets.Add(set); }
				else { formSet.set.Copy(set); }
			}
			MTG_UpdateSets();
			mtgSetField.Items.Clear();
			mtgSearchSetField.Items.Clear();
			mtgSearchSetField.Items.Add("--");
			foreach (MTG_Set set in mtgCatalog.sets) {
				mtgSetField.Items.Add(set);
				mtgSearchSetField.Items.Add(set);
			}
		}

		#endregion

		#region I/O

		//Save catalog data
		private void MTG_OnClickSave(object sender, EventArgs e) => MTG_SaveCatalog();
		private void MTG_SaveCatalog() {

			//Serialize catalog to file
			mtgCatalog.SavePrintRefs();
			using (Stream stream = File.Open("resources/mtg/catalog.bin", FileMode.Create))
				Serializer.Serialize(stream, mtgCatalog);

			//Log
			mtgIODialog.Text = "Catalog saved";

		}

		//Load catalog from file
		private void MTG_LoadCatalog() {

			//Deserialize catalog from file
			try {
				using (Stream stream = File.Open("resources/mtg/catalog.bin", FileMode.Open))
					mtgCatalog = Serializer.Deserialize<MTG_Catalog>(stream);
				mtgCatalog.LoadPrintRefs();
			}
			catch (Exception ex) {
				mtgIODialog.Text = "Error: " + ex.Message;
				return;
			}

			//Generate symbol controls
			MTG_RegenerateSymbols();
			MTG_RegenerateSets();

			//Update lists
			MTG_InitLists();
			MTG_UpdateSets();
			MTG_UpdateCardList();

			//Log
			mtgIODialog.Text = "Catalog loaded";

		}

		#endregion

	}
}
