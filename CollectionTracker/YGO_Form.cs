using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {
	public partial class Form1 : Form {

		//Properties
		public List<int> ygoMonsterTypes;
		public List<int> ygoRarities;
		public YGO_Catalog ygoCatalog;
		public YGO_Enums ygoEnums;
		public List<int> ygoPrintFilter;
		Dictionary<int, string> ygoNames;
		EventHandler ygoOnEditCard = null;
		EventHandler ygoOnEditPrint = null;
		int ygoUpdateCard = -1;
		int ygoUpdatePrint = -1;

		#region Init

		//Initialize
		public void YGO_Initialize() {
			ygoEnums = new YGO_Enums();
			YGO_InitLists();
			ygoMonsterTypes = new List<int>();
			ygoRarities = new List<int>();
			ygoCatalog = new YGO_Catalog();
			ygoPrintFilter = new List<int>();
			ygoEditCardButton.Click += ygoOnEditCard;
			ygoEditPrintButton.Click += ygoOnEditPrint;
			ygoDetailImgbox.MouseClick += YGO_GrowShrinkImageBox;
			ygoNameField.LostFocus += new EventHandler((sender, e) => YGO_CheckCardNameExists());
			YGO_LoadCatalog();
		}

		//Re-initialize lists
		public void YGO_InitLists() {
			ygoCardTypeField.Items.Clear();
			ygoAttributeField.Items.Clear();
			ygoMonsterTypeField.Items.Clear();
			ygoSetField.Items.Clear();
			ygoRaritiesField.Items.Clear();
			ygoMoveField.Items.Clear();
			foreach (string name in ygoEnums.cardTypes.Values) { ygoCardTypeField.Items.Add(name); }
			foreach (string name in ygoEnums.attributes.Values) { ygoAttributeField.Items.Add(name); }
			foreach (string name in ygoEnums.monsterTypes.Values) { ygoMonsterTypeField.Items.Add(name); }
			foreach (string name in ygoEnums.sets.Values) { ygoSetField.Items.Add(name); }
			foreach (string name in ygoEnums.rarities.Values) { ygoRaritiesField.Items.Add(name); }
			foreach (string name in ygoEnums.locations.Values) { ygoMoveField.Items.Add(name); }
			ygoNames = new Dictionary<int, string>();
		}

		#endregion

		#region Set List

		//Clear set list and update data
		private void YGO_UpdateSets() {
			ygoSetLayout.Controls.Clear();
			for (int i = 0; i < ygoEnums.sets.Count; ++i) {
				int i2 = i;

				//Important values
				int key = ygoEnums.sets.ElementAt(i2).Key;
				List<YGO_Printing> cardsInSet = ygoCatalog.printings.Where(print => print.set == key).ToList();
				int setCount = cardsInSet.Count;
				int setOwned = cardsInSet.Count(print => print.AnyOwned());

				//Set info box
				GroupBox box = new GroupBox();
				ygoSetLayout.Controls.Add(box);
				box.Location = new Point(3, 3);
				box.Size = new Size(400, 55);

				//Filter button
				Button filter = new Button();
				box.Controls.Add(filter);
				filter.Location = new Point(5, 10);
				filter.Size = new Size(145, 40);
				filter.Text = ygoEnums.sets.ElementAt(i).Value;
				filter.UseVisualStyleBackColor = true;
				filter.Click += new EventHandler((sender, e) => YGO_FilterCatalogBySet(key));

				//Progress label
				Label label = new Label();
				box.Controls.Add(label);
				label.Location = new Point(150, 10);
				label.Size = new Size(60, 40);
				label.Text = setOwned.ToString() + '/' + setCount.ToString();
				label.TextAlign = ContentAlignment.MiddleCenter;

				//Progress bar
				ProgressBar bar = new ProgressBar();
				box.Controls.Add(bar);
				bar.Location = new Point(210, 20);
				bar.Size = new Size(185, 20);
				if (setCount > 0) { bar.Value = (int)(((float)setOwned / setCount) * 100); }

			}
		}

		#endregion

		#region Catalog

		//Clear catalog and generate new cards
		private void YGO_UpdateCatalog() {
			ygoCatalogLayout.Controls.Clear();
			ygoCatalogLayout.SuspendLayout();
			for (int i = 0; i < ygoPrintFilter.Count; ++i) {

				//Get index/printing
				int index = ygoPrintFilter[i];
				YGO_Printing print = ygoCatalog.printings[ygoPrintFilter[i]];

				//Card box
				GroupBox box = new GroupBox();
				ygoCatalogLayout.Controls.Add(box);
				box.Location = new Point(3, 3);
				box.Size = new Size(250, 360 + (20 * print.rarities.Count));
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
				img.Click += new EventHandler((sender, e) => YGO_LoadCardDetails(index));

				//Loop rarities
				for (int j = 0; j < print.rarities.Count; ++j) {
					int rarity = print.rarities[j];

					//Rarity label
					Label rarityLabel = new Label();
					box.Controls.Add(rarityLabel);
					rarityLabel.Location = new Point(60, 355 + (j * 20));
					rarityLabel.Size = new Size(100, 20);
					rarityLabel.Text = ygoEnums.rarities[rarity];
					rarityLabel.AutoEllipsis = true;
					rarityLabel.TextAlign = ContentAlignment.MiddleRight;

					//Count label
					Label label = new Label();
					box.Controls.Add(label);
					label.Location = new Point(160, 355 + (j * 20));
					label.Size = new Size(30, 20);
					label.Text = print.OwnedCountOfRarity(rarity).ToString();
					label.TextAlign = ContentAlignment.MiddleLeft;

					//Decrement
					Button leftButton = new Button();
					box.Controls.Add(leftButton);
					leftButton.Location = new Point(5, 355 + (j * 20));
					leftButton.Size = new Size(55, 20);
					leftButton.Text = "<";
					leftButton.UseVisualStyleBackColor = true;
					leftButton.Click += new EventHandler((sender, e) => YGO_DecrementCardCount(box, label, index, rarity));

					//Increment
					Button rightButton = new Button();
					box.Controls.Add(rightButton);
					rightButton.Location = new Point(190, 355 + (j * 20));
					rightButton.Size = new Size(55, 20);
					rightButton.Text = ">";
					rightButton.UseVisualStyleBackColor = true;
					rightButton.Click += new EventHandler((sender, e) => YGO_IncrementCardCount(box, label, index, rarity));

				}

				//Resume
				box.ResumeLayout();

			}
			ygoCatalogLayout.ResumeLayout();
		}

		//Increment card quantity
		private void YGO_IncrementCardCount(GroupBox box, Label label, int index, int rarity) {
			if (index < ygoCatalog.printings.Count) {
				YGO_Printing print = ygoCatalog.printings[index];
				print.Increment(rarity);
				label.Text = print.OwnedCountOfRarity(rarity).ToString();
				if (!print.AnyOwned()) { box.BackColor = SystemColors.ControlDarkDark; }
				else { box.BackColor = SystemColors.ControlDark; }
			}
			else { label.Text = $"IOOB: {index} | {ygoCatalog.printings.Count}"; }
		}

		//Decrement card quantity
		private void YGO_DecrementCardCount(GroupBox box, Label label, int index, int rarity) {
			if (index < ygoCatalog.printings.Count) {
				YGO_Printing print = ygoCatalog.printings[index];
				print.Decrement(rarity);
				label.Text = print.OwnedCountOfRarity(rarity).ToString();
				if (!print.AnyOwned()) { box.BackColor = SystemColors.ControlDarkDark; }
				else { box.BackColor = SystemColors.ControlDark; }
			}
			else { label.Text = $"IOOB: {index} | {ygoCatalog.printings.Count}"; }
		}

		#endregion

		#region Card Details

		//Load card data into details tab
		private void YGO_LoadCardDetails(int printIndex) {

			//Get printing and set image
			YGO_Printing print = ygoCatalog.printings[printIndex];
			ygoDetailImgbox.Load(print.imgPath);

			//Load card data
			YGO_Card card = ygoCatalog.cards[print.cardID];
			ygoDetailBox.Text = card.name;

			//Remove static controls and clear detail box
			ygoDetailBox.Controls.Remove(ygoMoveLabel);
			ygoDetailBox.Controls.Remove(ygoMoveField);
			ygoDetailBox.Controls.Remove(ygoLocationTable);
			ygoDetailBox.Controls.Remove(ygoEditCardButton);
			ygoDetailBox.Controls.Remove(ygoEditPrintButton);
			ygoDetailBox.Controls.Clear();

			//Y position to create elements at
			int y = 0;

			//Card Type
			Label cType = new Label();
			ygoDetailBox.Controls.Add(cType);
			cType.Location = new Point(5, 15 + y);
			cType.Size = new Size(200, 20);
			cType.Text = ygoEnums.cardTypes[card.cardType];
			cType.TextAlign = ContentAlignment.MiddleLeft;
			y += 20;

			//Attribute
			Label att = new Label();
			ygoDetailBox.Controls.Add(att);
			att.Location = new Point(5, 15 + y);
			att.Size = new Size(200, 20);
			att.Text = ygoEnums.attributes[card.attribute];
			att.TextAlign = ContentAlignment.MiddleLeft;
			y += 20;

			//Card Type
			if (card.monsterTypes.Count > 0) {
				string s = "[ ";
				for (int i = 0; i < card.monsterTypes.Count; ++i) {
					if (i > 0) { s += " / "; }
					s += ygoEnums.monsterTypes[card.monsterTypes[i]];
				}
				s += " ]";
				Label mTypes = new Label();
				ygoDetailBox.Controls.Add(mTypes);
				mTypes.Location = new Point(5, 15 + y);
				mTypes.Size = new Size(200, 20);
				mTypes.Text = s;
				mTypes.TextAlign = ContentAlignment.MiddleLeft;
				y += 20;
			}

			//Oracle Text
			if (card.oracleText.Length > 0) {
				Label oText = new Label();
				ygoDetailBox.Controls.Add(oText);
				oText.Location = new Point(5, 20 + y);
				oText.AutoSize = true;
				oText.MaximumSize = new Size(400, 0);
				oText.Text = card.oracleText;
				oText.TextAlign = ContentAlignment.MiddleLeft;
				y += 10 + oText.Size.Height;
			}

			//Flavor Text
			if (print.flavorText.Length > 0) {
				Label fText = new Label();
				ygoDetailBox.Controls.Add(fText);
				fText.Location = new Point(5, 20 + y);
				fText.AutoSize = true;
				fText.MaximumSize = new Size(400, 0);
				fText.Text = print.flavorText;
				fText.TextAlign = ContentAlignment.MiddleLeft;
				fText.Font = new Font(fText.Font, FontStyle.Italic);
				y += 10 + fText.Size.Height;
			}

			//Monster Data
			if (card.cardType == ygoEnums.cardTypes.FirstOrDefault(type => type.Value.Equals("Monster")).Key) {

				//Level
				Label lvl = new Label();
				ygoDetailBox.Controls.Add(lvl);
				lvl.Location = new Point(5, 15 + y);
				lvl.Size = new Size(200, 20);
				if (card.monsterTypes.Contains(ygoEnums.monsterTypes.FirstOrDefault(type => type.Value.Equals("Link")).Key))
					lvl.Text = "Link-" + card.level.ToString();
				else
					lvl.Text = "Level " + card.level.ToString();
				lvl.TextAlign = ContentAlignment.MiddleLeft;
				y += 20;

				//Pendulum Scale
				if (card.monsterTypes.Contains(ygoEnums.monsterTypes.FirstOrDefault(type => type.Value.Equals("Pendulum")).Key)) {
					Label pend = new Label();
					ygoDetailBox.Controls.Add(pend);
					pend.Location = new Point(5, 15 + y);
					pend.Size = new Size(200, 20);
					pend.Text = card.pendulumLeft.ToString() + " - " + card.pendulumRight.ToString();
					pend.TextAlign = ContentAlignment.MiddleLeft;
					y += 20;
				}

				//Attack/Defense
				Label atkdef = new Label();
				ygoDetailBox.Controls.Add(atkdef);
				atkdef.Location = new Point(5, 15 + y);
				atkdef.Size = new Size(200, 20);
				atkdef.Text = card.attack.ToString() + " ATK / " + card.defense.ToString() + " DEF";
				atkdef.TextAlign = ContentAlignment.MiddleLeft;
				y += 20;

			}

			//Re-add and position location controls
			ygoDetailBox.Controls.Add(ygoMoveLabel);
			ygoDetailBox.Controls.Add(ygoMoveField);
			ygoDetailBox.Controls.Add(ygoLocationTable);
			ygoDetailBox.Controls.Add(ygoEditCardButton);
			ygoDetailBox.Controls.Add(ygoEditPrintButton);
			ygoMoveLabel.Location = new Point(5, 20 + y);
			ygoMoveField.Location = new Point(60, 15 + y);
			ygoLocationTable.Location = new Point(9, 45 + y);

			//Load location table
			YGO_LoadLocationTable(print);

			//Set button events
			ygoEditCardButton.Click -= ygoOnEditCard;
			ygoEditPrintButton.Click -= ygoOnEditPrint;
			ygoOnEditCard = new EventHandler((sender, e) => YGO_EditCard(print.cardID));
			ygoOnEditPrint = new EventHandler((sender, e) => YGO_EditPrint(printIndex));
			ygoEditCardButton.Click += ygoOnEditCard;
			ygoEditPrintButton.Click += ygoOnEditPrint;

			//Set tab
			ygoTabControl.SelectedTab = ygoDetailPage;

		}

		//Load location table
		private void YGO_LoadLocationTable(YGO_Printing print) {

			//Clear
			ygoDetailBox.Controls.Remove(ygoLocationTable);
			ygoLocationTable.Controls.Clear();
			ygoLocationTable.RowCount = 0;
			ygoLocationTable.RowStyles.Clear();
			ygoLocationTable.Size = new Size(ygoLocationTable.Size.Width, 10);

			//Loop rarities and locations
			foreach (int rarity in print.quantity.Keys) {
				foreach (int location in print.quantity[rarity].Keys) {
					int rar = rarity;
					int loc = location;
					ygoLocationTable.Size = new Size(ygoLocationTable.Size.Width, ygoLocationTable.Size.Height + 35);

					//Add row
					++ygoLocationTable.RowCount;
					ygoLocationTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));

					//Rarity label
					Label rarityLabel = new Label();
					ygoLocationTable.Controls.Add(rarityLabel, 0, ygoLocationTable.RowCount - 1);
					rarityLabel.Dock = DockStyle.Fill;
					rarityLabel.TextAlign = ContentAlignment.MiddleLeft;
					rarityLabel.Text = ygoEnums.rarities[rarity];
					rarityLabel.AutoEllipsis = true;

					//Location label
					Label locationLabel = new Label();
					ygoLocationTable.Controls.Add(locationLabel, 1, ygoLocationTable.RowCount - 1);
					locationLabel.Dock = DockStyle.Fill;
					locationLabel.TextAlign = ContentAlignment.MiddleLeft;
					locationLabel.Text = ygoEnums.locations[location];
					locationLabel.AutoEllipsis = true;

					//Count label
					Label countLabel = new Label();
					ygoLocationTable.Controls.Add(countLabel, 2, ygoLocationTable.RowCount - 1);
					countLabel.Dock = DockStyle.Fill;
					countLabel.TextAlign = ContentAlignment.MiddleLeft;
					countLabel.Text = print.quantity[rarity][location].ToString();

					//Move button
					Button moveButton = new Button();
					ygoLocationTable.Controls.Add(moveButton, 3, ygoLocationTable.RowCount - 1);
					moveButton.Dock = DockStyle.Fill;
					moveButton.Text = "Move 1";
					moveButton.UseVisualStyleBackColor = true;
					moveButton.Click += new EventHandler((sender, e) => YGO_MoveOne(print, loc, rarity));

				}
			}

			//Resume
			ygoDetailBox.Controls.Add(ygoLocationTable);

		}

		//Move one card of a given rarity from one location to another
		private void YGO_MoveOne(YGO_Printing print, int from, int rarity) {
			if (ygoMoveField.SelectedIndex < 0) { return; }
			print.MoveOne(from, ygoEnums.locations.ElementAt(ygoMoveField.SelectedIndex).Key, rarity);
			YGO_LoadLocationTable(print);
		}

		//Grow or shrink image box
		private void YGO_GrowShrinkImageBox(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				ygoDetailImgbox.Size = new Size(ygoDetailImgbox.Size.Width + 25, ygoDetailImgbox.Size.Height + 35);
				ygoDetailBox.Location = new Point(ygoDetailBox.Location.X + 25, ygoDetailBox.Location.Y);
				ygoDetailBox.Size = new Size(ygoDetailBox.Size.Width, ygoDetailBox.Size.Height + 35);
			}
			else if (e.Button == MouseButtons.Right && ygoDetailImgbox.Size.Width > 300) {
				ygoDetailImgbox.Size = new Size(ygoDetailImgbox.Size.Width - 25, ygoDetailImgbox.Size.Height - 35);
				ygoDetailBox.Location = new Point(ygoDetailBox.Location.X - 25, ygoDetailBox.Location.Y);
				ygoDetailBox.Size = new Size(ygoDetailBox.Size.Width, ygoDetailBox.Size.Height - 35);
			}
		}

		//Edit card data
		private void YGO_EditCard(int index) {
			YGO_Card card = ygoCatalog.cards[index];
			ygoNameField.Text = card.name;
			ygoCardTypeField.SelectedIndex = ygoEnums.cardTypes.Keys.ToList().IndexOf(card.cardType);
			ygoAttributeField.SelectedIndex = ygoEnums.attributes.Keys.ToList().IndexOf(card.attribute);
			ygoMonsterTypes.Clear();
			foreach (int type in card.monsterTypes) { ygoMonsterTypes.Add(type); }
			YGO_UpdateMonsterTypeList();
			ygoOracleTextField.Text = card.oracleText;
			ygoLevelField.Value = card.level;
			ygoPendulumLeft.Value = card.pendulumLeft;
			ygoPendulumRight.Value = card.pendulumRight;
			ygoAttackField.Value = card.attack;
			ygoDefenseField.Value = card.defense;
			ygoUpdateCard = index;
			ygoAddCardButton.Text = "Update Card";
			ygoTabControl.SelectedTab = ygoCardPage;
		}

		//Edit printing data
		private void YGO_EditPrint(int index) {
			YGO_Printing print = ygoCatalog.printings[index];
			ygoSetField.SelectedIndex = ygoEnums.sets.Keys.ToList().IndexOf(print.set);
			ygoNumberField.Value = print.cardNumber;
			ygoRarities.Clear();
			foreach (int rarity in print.rarities) { ygoRarities.Add(rarity); }
			YGO_UpdateRarityList();
			ygoFlavorTextField.Text = print.flavorText;
			ygoImgpathLabel.Text = print.imgPath;
			ygoPrintImgbox.Load(print.imgPath);
			ygoCardrefField.SelectedIndex = ygoNames.Keys.ToList().IndexOf(print.cardID);
			ygoUpdatePrint = index;
			ygoAddPrintButton.Text = "Update Printing";
			ygoTabControl.SelectedTab = ygoPrintPage;
		}

		#endregion

		#region Card Entry

		//Update monster list label
		private void YGO_UpdateMonsterTypeList() {
			if (ygoMonsterTypes.Count == 0) {
				ygoMonsterTypeValue.Text = "-";
				return;
			}
			string s = "";
			for (int i = 0; i < ygoMonsterTypes.Count; ++i) {
				if (i > 0) { s += " / "; }
				s += ygoEnums.monsterTypes[ygoMonsterTypes[i]];
			}
			ygoMonsterTypeValue.Text = s;
		}

		//Add monster type to list
		private void YGO_OnClickAddMonsterType(object sender, EventArgs e) {
			if (ygoMonsterTypeField.SelectedIndex < 0) { return; }
			int key = ygoEnums.monsterTypes.ElementAt(ygoMonsterTypeField.SelectedIndex).Key;
			if (ygoMonsterTypes.Contains(key)) { return; }
			ygoMonsterTypes.Add(key);
			YGO_UpdateMonsterTypeList();
		}

		//Remove monster type from list
		private void YGO_OnClickSubMonsterType(object sender, EventArgs e) {
			if (ygoMonsterTypes.Count == 0) { return; }
			ygoMonsterTypes.RemoveAt(ygoMonsterTypes.Count - 1);
			YGO_UpdateMonsterTypeList();
		}

		//Check if card exists with name
		private void YGO_CheckCardNameExists() {
			if (ygoCatalog.cards.Select(card => card.name).Contains(ygoNameField.Text)) {
				ygoCardDialog.Text = ygoNameField.Text + " already exists";
			}
		}

		//Add card to catalog
		private void YGO_OnClickAddCard(object sender, EventArgs e) {
			if (ygoCardTypeField.SelectedIndex < 0) { return; }
			if (ygoAttributeField.SelectedIndex < 0) { return; }
			if (ygoUpdateCard < 0) {
				if (ygoCatalog.cards.Select(card => card.name).Contains(ygoNameField.Text)) {
					ygoCardDialog.Text = ygoNameField.Text + " already exists";
					return;
				}
				ygoCatalog.cards.Add(
					new YGO_Card(
						ygoNameField.Text,
						ygoEnums.cardTypes.ElementAt(ygoCardTypeField.SelectedIndex).Key,
						ygoEnums.attributes.ElementAt(ygoAttributeField.SelectedIndex).Key,
						ygoMonsterTypes,
						ygoOracleTextField.Text,
						(int)ygoPendulumLeft.Value,
						(int)ygoPendulumRight.Value,
						(int)ygoLevelField.Value,
						(int)ygoAttackField.Value,
						(int)ygoDefenseField.Value
					)
				);
				YGO_UpdateCardList();
				ygoCardDialog.Text = "-";
			}
			else {
				YGO_Card card = ygoCatalog.cards[ygoUpdateCard];
				card.name = ygoNameField.Text;
				card.cardType = ygoEnums.cardTypes.ElementAt(ygoCardTypeField.SelectedIndex).Key;
				card.attribute = ygoEnums.attributes.ElementAt(ygoAttributeField.SelectedIndex).Key;
				card.monsterTypes = new List<int>(ygoMonsterTypes);
				card.oracleText = ygoOracleTextField.Text;
				card.pendulumLeft = (int)ygoPendulumLeft.Value;
				card.pendulumRight = (int)ygoPendulumRight.Value;
				card.level = (int)ygoLevelField.Value;
				card.attack = (int)ygoAttackField.Value;
				card.defense = (int)ygoDefenseField.Value;
				ygoCardDialog.Text = "Card Data Updated";
			}
			ygoNameField.Text = "";
			ygoCardTypeField.SelectedIndex = -1;
			ygoAttributeField.SelectedIndex = -1;
			ygoOracleTextField.Text = "";
			ygoLevelField.Value = 0;
			ygoPendulumLeft.Value = 0;
			ygoPendulumRight.Value = 0;
			ygoAttackField.Value = 0;
			ygoDefenseField.Value = 0;
			ygoMonsterTypes.Clear();
			YGO_UpdateMonsterTypeList();
			ygoUpdateCard = -1;
			ygoAddCardButton.Text = "Add To Catalog";
		}

		#endregion

		#region Printing Entry

		//Clear and refresh list of card names
		private void YGO_UpdateCardList() {
			ygoCardrefField.Items.Clear();
			ygoNames.Clear();
			for (int i = 0; i < ygoCatalog.cards.Count; ++i) { ygoNames.Add(i, ygoCatalog.cards[i].name); }
			ygoNames = ygoNames.OrderBy(name => name.Value).ToDictionary(name => name.Key, name => name.Value);
			foreach (KeyValuePair<int, string> name in ygoNames) { ygoCardrefField.Items.Add(name.Value); }
		}

		//Update monster list label
		private void YGO_UpdateRarityList() {
			if (ygoRarities.Count == 0) {
				ygoTreatmentsValue.Text = "-";
				return;
			}
			string s = "";
			for (int i = 0; i < ygoRarities.Count; ++i) {
				if (i > 0) { s += ", "; }
				s += ygoEnums.rarities[ygoRarities[i]];
			}
			ygoTreatmentsValue.Text = s;
		}

		//Add rarity to list
		private void YGO_OnClickAddRarity(object sender, EventArgs e) {
			if (ygoRaritiesField.SelectedIndex < 0) { return; }
			int key = ygoEnums.rarities.ElementAt(ygoRaritiesField.SelectedIndex).Key;
			if (ygoRarities.Contains(key)) { return; }
			ygoRarities.Add(key);
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
				ygoPrintImgbox.Load(filePath);
			}
		}

		//Add printing to catalog
		private void YGO_OnClickAddPrint(object sender, EventArgs e) {
			if (ygoSetField.SelectedIndex < 0) { return; }
			if (ygoRarities.Count == 0) { return; }
			if (ygoImgpathLabel.Text.Length <= 1) { return; }
			if (ygoCardrefField.SelectedIndex < 0) { return; }
			if (ygoUpdatePrint < 0) {
				ygoCatalog.printings.Add(
					new YGO_Printing(
						ygoEnums.sets.ElementAt(ygoSetField.SelectedIndex).Key,
						(int)ygoNumberField.Value,
						ygoFlavorTextField.Text,
						ygoImgpathLabel.Text,
						ygoRarities,
						ygoNames.ElementAt(ygoCardrefField.SelectedIndex).Key
					)
				);
				YGO_UpdateSets();
				ygoImgpathLabel.Text = "-";
			}
			else {
				YGO_Printing print = ygoCatalog.printings[ygoUpdatePrint];
				print.set = ygoEnums.sets.ElementAt(ygoSetField.SelectedIndex).Key;
				print.cardNumber = (int)ygoNumberField.Value;
				print.flavorText = ygoFlavorTextField.Text;
				print.imgPath = ygoImgpathLabel.Text;
				print.rarities = new List<int>(ygoRarities);
				print.cardID = ygoNames.ElementAt(ygoCardrefField.SelectedIndex).Key;
				ygoImgpathLabel.Text = "Printing Data Updated";
			}
			ygoNumberField.Value += 1;
			ygoFlavorTextField.Text = "";
			ygoCardrefField.SelectedIndex = -1;
			ygoPrintImgbox.Image = null;
			ygoUpdatePrint = -1;
			ygoAddPrintButton.Text = "Add To Catalog";
		}

		#endregion

		#region Filters

		//Filter catalog by set ID
		private void YGO_FilterCatalogBySet(int setID) {
			ygoPrintFilter.Clear();
			for (int i = 0; i < ygoCatalog.printings.Count; ++i) {
				if (ygoCatalog.printings[i].set == setID) {
					ygoPrintFilter.Add(i);
				}
			}
			YGO_UpdateCatalog();
			ygoTabControl.SelectedTab = ygoCatalogPage;
		}

		#endregion

		#region I/O

		//Save catalog data
		private void YGO_OnClickSave(object sender, EventArgs e) => YGO_SaveCatalog();
		private void YGO_SaveCatalog() {

			//Serialize catalog to file
			using (Stream stream = File.Open("resources/ygo/catalog.bin", FileMode.Create)) {
				var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				binaryFormatter.Serialize(stream, ygoCatalog);
			}

			//Log
			ygoIODialog.Text = "Catalog saved";

		}

		//Load catalog from file
		private void YGO_LoadCatalog() {

			//Deserialize catalog from file
			try {
				using (Stream stream = File.Open("resources/ygo/catalog.bin", FileMode.Open)) {
					var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
					ygoCatalog = (YGO_Catalog)binaryFormatter.Deserialize(stream);
				}
			}
			catch (Exception ex) { ygoIODialog.Text = "Error: " + ex.Message; }

			//Load enum data from file
			ygoEnums.Clear();
			string line;
			int n = 0;
			try {
				StreamReader sr = new StreamReader("resources/ygo/data.txt");
				line = sr.ReadLine();
				while (line != null) {
					if (line.StartsWith("--")) { ++n; }
					else {
						string[] subs = line.Split('|');
						if (subs.Length == 2) {
							switch (n) {
								case 1:
									ygoEnums.sets.Add(int.Parse(subs[0]), subs[1]);
									break;
								case 2:
									ygoEnums.cardTypes.Add(int.Parse(subs[0]), subs[1]);
									break;
								case 3:
									ygoEnums.attributes.Add(int.Parse(subs[0]), subs[1]);
									break;
								case 4:
									ygoEnums.monsterTypes.Add(int.Parse(subs[0]), subs[1]);
									break;
								case 5:
									ygoEnums.rarities.Add(int.Parse(subs[0]), subs[1]);
									break;
								case 6:
									ygoEnums.locations.Add(int.Parse(subs[0]), subs[1]);
									break;
								default:
									break;
							}
						}
					}
					line = sr.ReadLine();
				}
				sr.Close();
			}
			catch (Exception ex) { ygoIODialog.Text = "Error: " + ex.Message; }

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
