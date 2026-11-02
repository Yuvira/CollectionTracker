using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	//Deck editor
	public class Deckpage : TrackerPage {

		#region Deck Group Class

		//Deck group
		private class DeckGroup {

			//Properties
			private Deckpage parent;
			private string name;
			private List<DeckPrint> prints;

			//Controls
			private Label label;

			//Accessors
			public Deckpage Parent => parent;
			public string Name => name;
			public List<DeckPrint> Prints => prints;
			public Label Label => label;

			//Constructor
			public DeckGroup(Deckpage parent, string name) {
				this.parent = parent;
				this.name = name;
				label = Utils.GenerateAutoSizeLabel(name);
				prints = new List<DeckPrint>();
				this.parent.layoutPanel.Controls.Add(label);
			}

			//Add print
			public void AddPrint(Printing printing) {
				DeckPrint print = new DeckPrint(this, printing);
				prints.Add(print);
				parent.layoutPanel.Controls.Add(print.ImgBox);
			}

			//Remove print
			public void RemovePrint(DeckPrint print) {
				if (!prints.Contains(print))
					return;
				prints.Remove(print);
				parent.layoutPanel.Controls.Remove(print.ImgBox);
				print.ImgBox.Dispose();
				parent.DoLayout();
			}

			//Layout objects
			public int Layout(int width, int xPos, int yPos) {
				if (prints.Count == 0) {
					label.Hide();
					return yPos;
				}
				string labelText = $"{name} ({prints.Count})";
				label.Width = Utils.MeasureWidth(labelText);
				label.Text = labelText;
				label.Show();
				Point pos = new Point(xPos, yPos);
				label.Location = pos;
				pos.Y += TEXT_HEIGHT + PANEL_MARGIN;
				prints.Sort(DeckPrint.CompareCards);
				if (parent.groupToggle.Checked) {
					IEnumerable<IGrouping<string, DeckPrint>> groups = prints.GroupBy(p => p.Printing.GetField("name"));
					foreach (IGrouping<string, DeckPrint> group in groups) {
						if (pos.X > PANEL_MARGIN) {
							int groupWidth = DECK_CARD_WIDTH + ((group.Count() - 1) * CARD_GAP_SHARED);
							if (pos.X + groupWidth > width) {
								pos.X = xPos;
								pos.Y += DECK_CARD_HEIGHT + PANEL_MARGIN;
							}
						}
						foreach (DeckPrint print in group) {
							print.ImgBox.Location = pos;
							pos.X += CARD_GAP_SHARED;
						}
						pos.X += CARD_GAP_UNSHARED - CARD_GAP_SHARED;
					}
				}
				else {
					foreach (DeckPrint print in prints) {
						if (pos.X > PANEL_MARGIN && pos.X + CARD_GAP_UNSHARED > width) {
							pos.X = xPos;
							pos.Y += DECK_CARD_HEIGHT + PANEL_MARGIN;
						}
						print.ImgBox.Location = pos;
						pos.X += CARD_GAP_UNSHARED;
					}
				}
				for (int i = prints.Count - 1; i >= 0; --i)
					prints[i].ImgBox.BringToFront();
				return pos.Y + DECK_CARD_HEIGHT + PANEL_MARGIN;
			}

		}

		#endregion

		#region Deck Print Class

		//Deck printing
		private class DeckPrint {

			//Properties
			private DeckGroup parent;
			private Printing printing;
			private string path;

			//Controls
			private PictureBox imgBox;

			//Accessors
			public Printing Printing => printing;
			public PictureBox ImgBox => imgBox;

			//Constructor
			public DeckPrint(DeckGroup parent, Printing printing) {
				this.parent = parent;
				this.printing = printing;
				imgBox = Utils.GeneratePictureBox(new Rectangle(0, 0, DECK_CARD_WIDTH, DECK_CARD_HEIGHT));
				imgBox.MouseEnter += GetFocus;
				imgBox.MouseUp += ProcessClick;
				imgBox.MouseWheel += ChangePrinting;
				UpdateImage();
			}

			//Load image
			private void UpdateImage() {
				path = "";
				if (printing != null && printing.ImagePaths.Count > 0)
					path = printing.ImagePaths[0];
				Utils.TryLoadCardImage(imgBox, path, TrackerForm.Catalog.Game);
			}

			//Sorter
			public static int CompareCards(DeckPrint print1, DeckPrint print2) {
				if (TrackerForm.Catalog.Game == Game.MTG)
					return Card.SortTypeMTG(print1.printing.Card, print2.printing.Card);
				if (TrackerForm.Catalog.Game == Game.YGO)
					return Card.SortTypeYGO(print1.printing.Card, print2.printing.Card);
				return Printing.SortAlphabetical(print1.printing, print2.printing);
			}

			//Focus on hover
			private void GetFocus(object sender, EventArgs e) {
				imgBox.Focus();
				Utils.TryLoadCardImage(parent.Parent.previewImage, path, TrackerForm.Catalog.Game);
			}

			//Remove on right click
			private void ProcessClick(object sender, MouseEventArgs e) {
				if (e.Button == MouseButtons.Right)
					parent.RemovePrint(this);
			}

			//Change printing on mouse wheel
			private void ChangePrinting(object sender, MouseEventArgs e) {
				List<Printing> prints = TrackerForm.Catalog.Printings.Where(p => p.Card == printing.Card).ToList();
				if (prints.Count <= 1)
					return;
				int idx = prints.IndexOf(printing);
				if (idx == -1)
					return;
				if (e.Delta < 0)
					--idx;
				else if (e.Delta > 0)
					++idx;
				printing = prints[(idx + prints.Count) % prints.Count];
				UpdateImage();
				Utils.TryLoadCardImage(parent.Parent.previewImage, path, TrackerForm.Catalog.Game);
				if (e is HandledMouseEventArgs args)
					args.Handled = true;
			}

		}

		#endregion

		//Constants
		private const int PREVIEW_WIDTH = 400;
		private const int PREVIEW_HEIGHT = 540;
		private const int DECK_CARD_WIDTH = 200;
		private const int DECK_CARD_HEIGHT = 270;
		private const int CARD_GAP_SHARED = DECK_CARD_WIDTH / 4;
		private const int CARD_GAP_UNSHARED = DECK_CARD_WIDTH + PANEL_MARGIN;
		private readonly List<string> GROUP_TARGETS = new List<string> {
			"Commander",
			"Main Deck",
			"Extra Deck",
			"Sideboard",
			"Maybeboard"
		};

		//Properties
		private Deck refDeck;
		private List<DeckGroup> groups;

		//Controls
		private TextBox nameBox;
		private ComboBox cardBox;
		private ComboBox targetBox;
		private Button addCardButton;
		private CheckBox previewToggle;
		private CheckBox groupToggle;
		private Button saveDeckButton;
		private PictureBox previewImage;
		private Panel layoutPanel;
		private TrackerPanel topLeft;

		//Constructor
		public Deckpage(Deck deck) {

			//Set reference
			refDeck = deck;

			//Name box
			nameBox = Utils.GenerateTextBox(new Rectangle(0, PANEL_MARGIN, 250, BUTTON_HEIGHT), refDeck != null ? refDeck.Name : "New Deck");
			nameBox.Anchor = AnchorStyles.Top;

			//Card box
			cardBox = Utils.GenerateComboBox(new Rectangle(0, PANEL_MARGIN, 400, BUTTON_HEIGHT), ComboBoxStyle.DropDownList, true);
			cardBox.Anchor = AnchorStyles.Top;
			cardBox.Items.AddRange(TrackerForm.Catalog.Cards.ToArray());

			//Target box
			targetBox = Utils.GenerateComboBox(new Rectangle(0, PANEL_MARGIN, 150, BUTTON_HEIGHT), ComboBoxStyle.DropDownList, false);
			targetBox.Anchor = AnchorStyles.Top;
			targetBox.Items.AddRange(GROUP_TARGETS.ToArray());
			targetBox.SelectedIndex = 1;

			//Add button
			addCardButton = Utils.GenerateButton(new Rectangle(0, PANEL_MARGIN, 150, BUTTON_HEIGHT), "Add Card");
			addCardButton.Anchor = AnchorStyles.Top;
			addCardButton.Click += AddCard;

			//Preview toggle
			previewToggle = Utils.GenerateToggleButton(new Rectangle(0, PANEL_MARGIN, 150, BUTTON_HEIGHT), true, "Show Preview");
			previewToggle.Anchor = AnchorStyles.Top;
			previewToggle.CheckedChanged += OnFormResizeEnd;

			//Group toggle
			groupToggle = Utils.GenerateToggleButton(new Rectangle(0, PANEL_MARGIN, 150, BUTTON_HEIGHT), true, "Group Cards");
			groupToggle.Anchor = AnchorStyles.Top;
			groupToggle.CheckedChanged += OnFormResizeEnd;

			//Save button
			saveDeckButton = Utils.GenerateButton(new Rectangle(0, PANEL_MARGIN, 150, BUTTON_HEIGHT), "Save Deck");
			saveDeckButton.Anchor = AnchorStyles.Top;
			saveDeckButton.Click += SaveDeck;

			//Y Position for body
			int yPos = BUTTON_HEIGHT + (PANEL_MARGIN * 2);

			//Preview image
			previewImage = Utils.GeneratePictureBox(new Rectangle(PANEL_MARGIN, yPos, PREVIEW_WIDTH, PREVIEW_HEIGHT));
			previewImage.Anchor = AnchorStyles.Top | AnchorStyles.Left;

			//Panel
			layoutPanel = Utils.GeneratePanel(new Rectangle(0, yPos, 0, panel.Height - (yPos + PANEL_MARGIN)));
			layoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			layoutPanel.AutoScroll = true;

			//Top left object for relative position
			topLeft = Utils.GenerateTrackerPanel(new Rectangle(PANEL_MARGIN, PANEL_MARGIN, 1, 1));
			layoutPanel.Controls.Add(topLeft);

			//Add to panel
			panel.Controls.Add(nameBox);
			panel.Controls.Add(cardBox);
			panel.Controls.Add(targetBox);
			panel.Controls.Add(addCardButton);
			panel.Controls.Add(previewToggle);
			panel.Controls.Add(groupToggle);
			panel.Controls.Add(saveDeckButton);
			panel.Controls.Add(previewImage);
			panel.Controls.Add(layoutPanel);

			//Clear lists
			groups = new List<DeckGroup>();
			foreach (string target in GROUP_TARGETS)
				groups.Add(new DeckGroup(this, target));

			//Load existing deck
			if (refDeck != null) {
				foreach (KeyValuePair<string, List<string>> kvp in refDeck.IDMap)
					foreach (string id in kvp.Value)
						AddToGroup(kvp.Key, id);
			}

			//Force layout
			OnFormResizeEnd();

		}

		//Add card to group
		private void AddCard(object sender, EventArgs e) {
			if (cardBox.SelectedItem == null || !(cardBox.SelectedItem is Card card) || !card.TryGetField("name", out string name) || string.IsNullOrWhiteSpace(name) || name.Equals("_")) {
				MessageBox.Show("Invalid card reference!");
				return;
			}
			Printing print = TrackerForm.Catalog.Printings.FirstOrDefault(p => p.Card == card);
			AddToGroup(targetBox.SelectedItem.ToString(), print);
		}
		private void AddToGroup(string groupName, string id) => AddToGroup(groupName, TrackerForm.Catalog.Printings.FirstOrDefault(p => p.GetField("printid").Equals(id)));
		private void AddToGroup(string groupName, Printing printing) {
			DeckGroup group = groups.FirstOrDefault(g => g.Name.Equals(groupName));
			if (group != null)
				group.AddPrint(printing);
			else {
				group = new DeckGroup(this, groupName);
				group.AddPrint(printing);
				groups.Add(group);
			}
			DoLayout();
		}

		//Save deck to catalog
		private void SaveDeck(object sender, EventArgs e) {

			//Collect IDs
			Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
			foreach (DeckGroup group in groups) {
				if (group.Prints.Count == 0)
					continue;
				List<string> list = group.Prints.Select(p => p.Printing.GetField("printid")).ToList();
				map.Add(group.Name, list);
			}

			//Generate Deck object and add to catalog
			Deck deck = new Deck(nameBox.Text, map);
			if (refDeck == null)
				TrackerForm.Catalog.Decks.Add(deck);
			else
				refDeck.Copy(deck);

		}

		//Layout cards
		private void DoLayout() {
			int y = topLeft.Location.Y;
			int maxWidth = layoutPanel.Width - ((PANEL_MARGIN * 2) + SCROLL_MARGIN);
			layoutPanel.SuspendLayout();
			foreach (DeckGroup group in groups)
				y = group.Layout(maxWidth, topLeft.Location.X, y);
			layoutPanel.ResumeLayout();
		}

		//Resize event
		protected override void OnFormResizeEnd(object sender = null, EventArgs e = null) {
			previewImage.Visible = previewToggle.Checked;
			layoutPanel.Left = previewToggle.Checked ? PREVIEW_WIDTH + (PANEL_MARGIN * 2) : PANEL_MARGIN;
			layoutPanel.Width = Math.Max(panel.Width - (layoutPanel.Left + PANEL_MARGIN), DECK_CARD_WIDTH + (PANEL_MARGIN * 2) + SCROLL_MARGIN);
			nameBox.Left = PANEL_MARGIN;
			cardBox.Left = nameBox.Right + PANEL_MARGIN;
			targetBox.Left = cardBox.Right + PANEL_MARGIN;
			addCardButton.Left = targetBox.Right + PANEL_MARGIN;
			saveDeckButton.Left = layoutPanel.Right - saveDeckButton.Width;
			previewToggle.Left = saveDeckButton.Left - (previewToggle.Width + PANEL_MARGIN);
			groupToggle.Left = previewToggle.Left - (groupToggle.Width + PANEL_MARGIN);
			DoLayout();
			oldWidth = panel.Width;
		}

	}

}
