using System.Collections.Generic;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CollectionTracker {

	//Decklist
	public class Decklist : TrackerPage {

		#region Set Row Class

		//Deck row
		private class Deckrow {

			//Properties
			private Decklist parent;
			private Deck deck;

			//Controls
			private Button button;

			//Accessors
			public Button Button => button;
			public Deck Deck => deck;

			//Generator
			public Deckrow(Decklist parent, Deck deck) {

				//Values
				this.parent = parent;
				this.deck = deck;

				//Button
				button = Utils.GenerateButton(new Rectangle(5, 5, 500, 50), deck.Name);
				button.Click += LoadDeck;

			}

			//Load
			private void LoadDeck(object sender, EventArgs e) => parent.LoadDeck(deck);

		}

		#endregion

		//Constants
		private const int MAX_WIDTH = 540;

		//Properties
		private List<Deckrow> rows;
		private Label headerLabel;
		private Button newDeckButton;
		private Panel listPanel;

		//Constructor
		public Decklist() : base() {

			//Decks
			List<Deck> decks = new List<Deck>(TrackerForm.Catalog.Decks);

			//Header
			headerLabel = Utils.GenerateAutoSizeLabel(new Point(0, PANEL_MARGIN + 5), $"Decks: {TrackerForm.Catalog.Decks.Count}");
			headerLabel.Anchor = AnchorStyles.Top;

			//New button
			newDeckButton = Utils.GenerateButton(new Rectangle(0, PANEL_MARGIN, 200, BUTTON_HEIGHT), "New Deck");
			newDeckButton.Anchor = AnchorStyles.Top;
			newDeckButton.Click += NewDeck;

			//Panel
			int yPos = BUTTON_HEIGHT + (PANEL_MARGIN * 2);
			listPanel = Utils.GeneratePanel(new Rectangle(0, yPos, 0, panel.Height - (yPos + PANEL_MARGIN)));
			listPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			listPanel.AutoScroll = true;

			//Loop decks
			Point pos = new Point(5, 5);
			rows = new List<Deckrow>();
			listPanel.SuspendLayout();
			foreach (Deck deck in decks) {
				Deckrow row = new Deckrow(this, deck);
				row.Button.Location = pos;
				pos.Y += row.Button.Height + 5;
				rows.Add(row);
				listPanel.Controls.Add(row.Button);
			}
			listPanel.ResumeLayout();

			//Add to panel
			panel.Controls.Add(headerLabel);
			panel.Controls.Add(newDeckButton);
			panel.Controls.Add(listPanel);

			//Size and position
			oldWidth = 0;
			OnFormResizeEnd();

		}

		//Load deck
		private void NewDeck(object sender, EventArgs e) => TrackerForm.Instance.SetPage<Deckpage>();
		private void LoadDeck(Deck deck) => TrackerForm.Instance.SetPage<Deckpage>(deckref: deck);

		//Resize event
		protected override void OnFormResizeEnd(object sender = null, EventArgs e = null) {
			if (panel.Width >= MAX_WIDTH && oldWidth >= MAX_WIDTH)
				return;
			listPanel.Width = Math.Min(panel.Width - (PANEL_MARGIN * 2), MAX_WIDTH - (PANEL_MARGIN * 2));
			listPanel.Left = Math.Max((panel.Width - listPanel.Width) / 2, PANEL_MARGIN);
			headerLabel.Left = listPanel.Left;
			newDeckButton.Left = listPanel.Right - newDeckButton.Width;
			oldWidth = panel.Width;
		}

	}

}
