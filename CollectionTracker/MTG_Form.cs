using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CollectionTracker {
	public partial class Form1 : Form {

		//Properties
		public MTG_Catalog mtgCatalog;
		public List<string> mtgTreatments;
		public List<int> mtgPrintFilter;
		public MTG_Card mtgUpdateCard = null;
		public MTG_Printing mtgUpdatePrint = null;

		#region Init

		//Initialize
		public void MTG_Initialize() {
			mtgTreatments = new List<string>();
			mtgCatalog = new MTG_Catalog();
			mtgPrintFilter = new List<int>();
			detailBoxes = new List<GroupBox>();
			mtgDetailImgbox.Click += mtgOnFlipCard;
			mtgEditCardButton.Click += mtgOnEditCard;
			mtgEditPrintButton.Click += mtgOnEditPrint;
			mtgNameField.LostFocus += new EventHandler((sender, e) => MTG_CheckCardNameExists());
			mtgIdentityImgW.Load("resources/mtg/_icons/w.png");
			mtgIdentityImgU.Load("resources/mtg/_icons/u.png");
			mtgIdentityImgB.Load("resources/mtg/_icons/b.png");
			mtgIdentityImgR.Load("resources/mtg/_icons/r.png");
			mtgIdentityImgG.Load("resources/mtg/_icons/g.png");
			mtgColourImgW.Load("resources/mtg/_icons/w.png");
			mtgColourImgU.Load("resources/mtg/_icons/u.png");
			mtgColourImgB.Load("resources/mtg/_icons/b.png");
			mtgColourImgR.Load("resources/mtg/_icons/r.png");
			mtgColourImgG.Load("resources/mtg/_icons/g.png");
			mtgPrintImgboxBack.Load("resources/mtg/back.png");
			MTG_LoadCatalog();
		}

		//Re-initialize lists
		public void MTG_InitLists() {
			mtgSetField.Items.Clear();
			mtgRarityField.Items.Clear();
			mtgTreatmentField.Items.Clear();
			mtgMoveField.Items.Clear();
			foreach (MTG_Set set in mtgCatalog.sets) { mtgSetField.Items.Add(set); }
			foreach (string name in mtgCatalog.printings.Select(p => p.rarity).Distinct()) { mtgRarityField.Items.Add(name); }
			foreach (string name in mtgCatalog.printings.SelectMany(p => p.treatments).Select(t => t.name).Distinct()) { mtgTreatmentField.Items.Add(name); }
			foreach (string name in mtgCatalog.printings.SelectMany(p => p.treatments).SelectMany(t => t.locations).Distinct()) { mtgMoveField.Items.Add(name); }
		}

		#endregion

		#region Set List

		//Clear set list and update data
		private void MTG_UpdateSets() {
			mtgSetLayout.Controls.Clear();
			foreach (MTG_Set set in mtgCatalog.sets) {

				//Horizontal indent
				int indent = set.order * 35;

				//Important values
				List<MTG_Printing> cardsInSet = mtgCatalog.printings.Where(print => print.set == set).ToList();
				int setCount = cardsInSet.Count;
				int setOwned = cardsInSet.Count(print => print.AnyOwned());

				//Set info box
				GroupBox box = new GroupBox();
				mtgSetLayout.Controls.Add(box);
				box.Location = new Point(3, 3);
				box.Size = new Size(700, 70);

				//Filter button
				Button filter = new Button();
				box.Controls.Add(filter);
				filter.Location = new Point(5 + indent, 15);
				filter.Size = new Size(350 - indent, 50);
				filter.Text = set.name;
				filter.UseVisualStyleBackColor = true;
				filter.Image = new Bitmap(Image.FromFile(set.imgPath), new Size(35, 35));
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
				label.Size = new Size(60, 50);
				label.Text = setOwned.ToString() + '/' + setCount.ToString();
				label.TextAlign = ContentAlignment.MiddleCenter;

				//Progress bar
				ProgressBar bar = new ProgressBar();
				box.Controls.Add(bar);
				bar.Location = new Point(430, 25);
				bar.Size = new Size(265, 30);
				if (setCount > 0) { bar.Value = (int)(((float)setOwned / setCount) * 100); }

			}
		}

		#endregion

		#region Catalog

		//Properties
		public int mtgCatalogPagenum = 0;
		public int mtgCardsPerPage = 50;

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
				int index = mtgPrintFilter[i];
				MTG_Printing print = mtgCatalog.printings[mtgPrintFilter[i]];

				//Card box
				GroupBox box = new GroupBox();
				mtgCatalogLayout.Controls.Add(box);
				box.Location = new Point(3, 3);
				box.Size = new Size(250, 360 + (30 * print.treatments.Count));
				if (!print.AnyOwned()) { box.BackColor = SystemColors.ControlDarkDark; }
				box.SuspendLayout();

				//Image box
				PictureBox img = new PictureBox();
				box.Controls.Add(img);
				img.BorderStyle = BorderStyle.Fixed3D;
				img.SizeMode = PictureBoxSizeMode.StretchImage;
				img.Location = new Point(0, 0);
				img.Size = new Size(250, 350);
				img.Load(print.imgPath);
				img.Click += new EventHandler((sender, e) => MTG_LoadCardDetails(print));

				//Loop treatments
				for (int j = 0; j < print.treatments.Count; ++j) {

					//Get treatment at index
					MTG_Treatment treatment = print.treatments[j];

					//Treatment label
					Label treatmentLabel = new Label();
					box.Controls.Add(treatmentLabel);
					treatmentLabel.Location = new Point(60, 355 + (j * 30));
					treatmentLabel.Size = new Size(100, 30);
					treatmentLabel.Text = treatment.name;
					treatmentLabel.AutoEllipsis = true;
					treatmentLabel.TextAlign = ContentAlignment.MiddleRight;

					//Count label
					Label label = new Label();
					box.Controls.Add(label);
					label.Location = new Point(160, 355 + (j * 30));
					label.Size = new Size(30, 30);
					label.Text = print.OwnedCountOfTreatment(treatment.name).ToString();
					label.TextAlign = ContentAlignment.MiddleLeft;

					//Decrement
					Button leftButton = new Button();
					box.Controls.Add(leftButton);
					leftButton.Location = new Point(5, 355 + (j * 30));
					leftButton.Size = new Size(55, 29);
					leftButton.Text = "<";
					leftButton.UseVisualStyleBackColor = true;
					leftButton.Click += new EventHandler((sender, e) => MTG_DecrementCardCount(box, label, index, treatment.name));

					//Increment
					Button rightButton = new Button();
					box.Controls.Add(rightButton);
					rightButton.Location = new Point(190, 355 + (j * 30));
					rightButton.Size = new Size(55, 29);
					rightButton.Text = ">";
					rightButton.UseVisualStyleBackColor = true;
					rightButton.Click += new EventHandler((sender, e) => MTG_IncrementCardCount(box, label, index, treatment.name));

				}

				//Resume
				box.ResumeLayout();

			}
			mtgCatalogPage.Controls.Add(mtgCatalogLayout);
		}

		//Increment card quantity
		private void MTG_IncrementCardCount(GroupBox box, Label label, int index, string treatment) {
			if (index < mtgCatalog.printings.Count) {
				MTG_Printing print = mtgCatalog.printings[index];
				print.Increment(treatment);
				label.Text = print.OwnedCountOfTreatment(treatment).ToString();
				if (!print.AnyOwned()) { box.BackColor = SystemColors.ControlDarkDark; }
				else { box.BackColor = SystemColors.ControlDark; }
			}
			else { label.Text = $"IOOB: {index} | {mtgCatalog.printings.Count}"; }
		}

		//Decrement card quantity
		private void MTG_DecrementCardCount(GroupBox box, Label label, int index, string treatment) {
			if (index < mtgCatalog.printings.Count) {
				MTG_Printing print = mtgCatalog.printings[index];
				print.Decrement(treatment);
				label.Text = print.OwnedCountOfTreatment(treatment).ToString();
				if (!print.AnyOwned()) { box.BackColor = SystemColors.ControlDarkDark; }
				else { box.BackColor = SystemColors.ControlDark; }
			}
			else { label.Text = $"IOOB: {index} | {mtgCatalog.printings.Count}"; }
		}

		#endregion

		#region Card Details

		//Properties
		public const int TEXT_HEIGHT = 21;
		public bool mtgDetailFlipped = false;
		public List<GroupBox> detailBoxes;
		public EventHandler mtgOnFlipCard = null;
		public EventHandler mtgOnEditCard = null;
		public EventHandler mtgOnEditPrint = null;

		//Load card data into details tab
		private void MTG_LoadCardDetails(MTG_Printing print) {

			//Get printing and set image
			mtgDetailImgbox.Load(print.imgPath);
			mtgDetailFlipped = false;

			//Get card reference
			MTG_Card card = print.card;

			//Clear old boxes
			foreach (GroupBox box in detailBoxes) { mtgDetailPage.Controls.Remove(box); }
			detailBoxes.Clear();

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
					flavor.Location = new Point(5, y2);
					flavor.AutoSize = true;
					flavor.MaximumSize = new Size(mtgDetailBox.Size.Width - 10, 0);
					flavor.Text = print.flavorText;
					flavor.TextAlign = ContentAlignment.MiddleLeft;
					flavor.Font = new Font(flavor.Font, FontStyle.Italic);
					y2 += 5 + flavor.Size.Height;
				}

				//Size box and set position for next
				y2 += 5;
				box.Location = new Point(mtgDetailBox.Location.X, y);
				box.Size = new Size(mtgDetailBox.Size.Width, y2);
				detailBoxes.Add(box);
				y += 10 + y2;

			}

			//Load location table
			mtgDetailBox.Location = new Point(mtgDetailBox.Location.X, y);
			MTG_LoadLocationTable(print);

			//Set button events
			mtgDetailImgbox.Click -= mtgOnFlipCard;
			mtgEditCardButton.Click -= mtgOnEditCard;
			mtgEditPrintButton.Click -= mtgOnEditPrint;
			mtgOnFlipCard = new EventHandler((sender, e) => MTG_FlipCard(print));
			mtgOnEditCard = new EventHandler((sender, e) => MTG_EditCard(print.card));
			mtgOnEditPrint = new EventHandler((sender, e) => MTG_EditPrint(print));
			mtgDetailImgbox.Click += mtgOnFlipCard;
			mtgEditCardButton.Click += mtgOnEditCard;
			mtgEditPrintButton.Click += mtgOnEditPrint;

			//Hide tooltip
			mtgTooltipBox.Hide();

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
				int i = IndexOfMany(desc, new List<char>() { '{', '[', '<' });

				//We are at an object, generate it
				if (i == 0) {

					//Symbol
					if (desc[0] == '{') {
						int i2 = desc.IndexOf('}');
						if (i2 > 0) {
							location = MTG_InsertSymbol(desc.Substring(0, i2 + 1), box, location, TEXT_HEIGHT);
							desc = desc.Substring(i2 + 1);
						}
					}

					//Tooltip
					else if (desc[0] == '[') {
						int i2 = desc.IndexOf("|");
						int i3 = desc.IndexOf("]");
						if (i2 > 0 && i3 > 0) {
							location = MTG_InsertTooltip(desc.Substring(1, i2 - 1), desc.Substring(i2 + 1, (i3 - i2) - 1), box, location);
							desc = desc.Substring(i3 + 1);
						}
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
			label.Font = new Font(Font, FontStyle.Underline);
			label.ForeColor = Color.Blue;
			int textWidth = TextRenderer.MeasureText(str, label.Font).Width;
			if (textWidth > control.Width - (location.X + 5)) { location = new Point(5, location.Y + TEXT_HEIGHT); }
			control.Controls.Add(label);
			label.Location = location;
			label.Size = new Size(textWidth, TEXT_HEIGHT);
			label.Text = str;
			label.TextAlign = ContentAlignment.MiddleLeft;
			label.Click += new EventHandler((sender, e) => MTG_ShowTooltip(label, tooltip));
			label.MouseLeave += new EventHandler((sender, e) => mtgTooltipBox.Hide());
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
			icon.Load(symbol.imgPath);
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
			foreach (string name in mtgCatalog.printings.SelectMany(p => p.treatments).SelectMany(t => t.locations).Distinct()) { mtgMoveField.Items.Add(name); }
		}

		//Flip card image
		private void MTG_FlipCard(MTG_Printing print) {

			//Only flip if card has a second face
			if (!print.card.name.Contains(" // ")) { return; }

			//Get printing and flip
			mtgDetailFlipped = !mtgDetailFlipped;

			//Swap image if there's a back image reference
			if (print.backImgPath.Length > 1) {
				if (mtgDetailFlipped) { mtgDetailImgbox.Load(print.backImgPath); }
				else { mtgDetailImgbox.Load(print.imgPath); }
			}

			//Otherwise rotate 180
			else { mtgDetailImgbox.Image.RotateFlip(RotateFlipType.Rotate180FlipNone); }
			
		}

		//Edit card data
		private void MTG_EditCard(MTG_Card card) {
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
		private void MTG_EditPrint(MTG_Printing print) {
			mtgSetField.SelectedItem = print.set;
			mtgNumberField.Value = print.cardNumber;
			mtgRarityField.Text = print.rarity;
			mtgTreatments.Clear();
			foreach (MTG_Treatment treatment in print.treatments) { mtgTreatments.Add(treatment.name); }
			MTG_UpdateTreatmentList();
			mtgFlavorTextField.Text = print.flavorText;
			mtgImgpathLabel.Text = print.imgPath;
			mtgPrintImgbox.Load(print.imgPath);
			mtgImgpathBackLabel.Text = print.backImgPath;
			if (print.backImgPath.Length > 1) { mtgPrintImgboxBack.Load(print.backImgPath); }
			else { /*Load default*/ }
			mtgCardrefField.SelectedItem = print.card;
			mtgScryfallField.Text = print.scryfallID;
			mtgUpdatePrint = print;
			mtgAddPrintButton.Text = "Update Printing";
			mtgTabControl.SelectedTab = mtgPrintPage;
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
		private void MTG_OnClickAddCard(object sender, EventArgs e) {

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
				if (mtgCatalog.cards.Select(c => c.name).Contains(mtgNameField.Text)) {
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
				mtgPrintImgbox.Load(filePath);
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
				mtgPrintImgboxBack.Load(filePath);
			}
		}

		//Add printing to catalog
		private void MTG_OnClickAddPrint(object sender, EventArgs e) {

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
			mtgPrintImgboxBack.Load("resources/mtg/back.png");
			mtgUpdatePrint = null;
			mtgAddPrintButton.Text = "Add To Catalog";

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
		private void RegenerateSymbols() {
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
				icon.Load(refSymbol.imgPath);
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
				mtgFormSymbols[index].iconBox.Load(filePath);
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
		private List<MTG_FormSet> mtgFormSets = new List<MTG_FormSet>();

		//Symbol form class
		private class MTG_FormSet {
			public GroupBox box;
			public TextBox nameBox;
			public TextBox codeBox;
			public DateTimePicker dateBox;
			public NumericUpDown orderBox;
			public Label pathLabel;
			public PictureBox iconBox;
			public MTG_FormSet() : this(null, null, null, null, null, null, null) { }
			public MTG_FormSet(GroupBox box, TextBox nameBox, TextBox codeBox, DateTimePicker dateBox, NumericUpDown orderBox, Label pathLabel, PictureBox iconBox) {
				this.box = box;
				this.nameBox = nameBox;
				this.codeBox = codeBox;
				this.dateBox = dateBox;
				this.orderBox = orderBox;
				this.pathLabel = pathLabel;
				this.iconBox = iconBox;
			}
		}

		//Regenerate symbol controls
		private void RegenerateSets() {
			foreach (MTG_FormSet set in mtgFormSets) { mtgSetGeneratorLayout.Controls.Remove(set.box); }
			mtgFormSets.Clear();
			if (mtgCatalog.sets == null) { mtgCatalog.sets = new List<MTG_Set>(); }
			foreach (MTG_Set set in mtgCatalog.sets) { MTG_CreateSetGeneratorBox(set); }
		}

		//Add new symbol
		private void MTG_OnClickAddSet(object sender, EventArgs e) => MTG_CreateSetGeneratorBox();
		private void MTG_CreateSetGeneratorBox() => MTG_CreateSetGeneratorBox(new MTG_Set(), false);
		private void MTG_CreateSetGeneratorBox(MTG_Set refSet, bool useRef = true) {

			//Index
			int i = mtgFormSets.Count;

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

			//Order box
			NumericUpDown order = new NumericUpDown();
			box.Controls.Add(order);
			order.Location = new Point(5, 120);
			order.Size = new Size(50, 30);
			order.Increment = 1;
			order.Minimum = 0;
			order.Value = useRef ? refSet.order : 0;

			//Path label
			Label path = new Label();
			box.Controls.Add(path);
			path.Location = new Point(55, 120);
			path.Size = new Size(285, 30);
			path.TextAlign = ContentAlignment.MiddleLeft;

			//Icon box
			PictureBox icon = new PictureBox();
			box.Controls.Add(icon);
			icon.Location = new Point(280, 15);
			icon.Size = new Size(65, 65);
			icon.SizeMode = PictureBoxSizeMode.StretchImage;
			icon.Cursor = Cursors.Hand;
			icon.Click += new EventHandler((sender, e) => MTG_OnClickSearchSetIcon(i));

			//Load image if one is referenced
			if (refSet.imgPath.Length > 0) {
				path.Text = refSet.imgPath;
				icon.Load(refSet.imgPath);
			}

			//Create object and add box to layout
			mtgFormSets.Add(new MTG_FormSet(box, name, code, date, order, path, icon));
			mtgSetGeneratorLayout.Controls.Add(box);

		}

		//Get image path and display card
		private void MTG_OnClickSearchSetIcon(int index) {
			if (imageFileDialog.ShowDialog() == DialogResult.OK) {
				string curDir = Directory.GetCurrentDirectory();
				string filePath = imageFileDialog.FileName;
				if (!filePath.Contains(curDir)) { return; }
				filePath = filePath.Remove(filePath.IndexOf(curDir), curDir.Length + 1);
				mtgFormSets[index].pathLabel.Text = filePath;
				mtgFormSets[index].iconBox.Load(filePath);
			}
		}

		//Save current symbol list
		private void MTG_OnClickSaveSets(object sender, EventArgs e) {
			for (int i = 0; i < mtgFormSets.Count; ++i) {
				MTG_Set set = new MTG_Set(
					mtgFormSets[i].nameBox.Text,
					mtgFormSets[i].codeBox.Text,
					mtgFormSets[i].pathLabel.Text,
					mtgFormSets[i].dateBox.Value,
					(int)mtgFormSets[i].orderBox.Value
				);
				if (i < mtgCatalog.sets.Count) { mtgCatalog.sets[i].Copy(set); }
				else { mtgCatalog.sets.Add(set); }
			}
			MTG_UpdateSets();
		}

		#endregion

		#region Filters

		//Filter catalog by set ID
		private void MTG_FilterCatalogBySet(MTG_Set set) {
			mtgPrintFilter.Clear();
			for (int i = 0; i < mtgCatalog.printings.Count; ++i) {
				if (mtgCatalog.printings[i].set == set) {
					mtgPrintFilter.Add(i);
				}
			}
			mtgCatalogPagenum = 0;
			MTG_UpdateCatalog();
			mtgTabControl.SelectedTab = mtgCatalogPage;
		}

		#endregion

		#region I/O

		//Save catalog data
		private void MTG_OnClickSave(object sender, EventArgs e) => MTG_SaveCatalog();
		private void MTG_SaveCatalog() {

			//Serialize catalog to file
			using (Stream stream = File.Open("resources/mtg/catalog.bin", FileMode.Create)) {
				var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				binaryFormatter.Serialize(stream, mtgCatalog);
			}

			//Log
			mtgIODialog.Text = "Catalog saved";

		}

		//Load catalog from file
		private void MTG_LoadCatalog() {

			//Deserialize catalog from file
			try {
				using (Stream stream = File.Open("resources/mtg/catalog.bin", FileMode.Open)) {
					var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
					mtgCatalog = (MTG_Catalog)binaryFormatter.Deserialize(stream);
				}
			}
			catch (Exception ex) {
				mtgIODialog.Text = "Error: " + ex.Message;
				return;
			}

			//Generate symbol controls
			RegenerateSymbols();
			RegenerateSets();

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
