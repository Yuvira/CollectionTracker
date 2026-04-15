using System;
using System.Drawing;
using System.Windows.Forms;

namespace CollectionTracker {

	//Homepage
	public class Homepage : TrackerPage {

		//Static search string
		static string searchString = null;

		//Properties
		TrackerPanel searchPanel;
		TextBox searchBox;

		//Constructor
		public Homepage() : base() {

			//Catalog selectors
			TrackerPanel catalogPanel = Utils.GenerateTrackerPanel(Utils.CenterRect(new Size(382, 42), panel.Size, new Point(0, -32)));
			catalogPanel.Anchor = AnchorStyles.None;

			//Magic button
			RadioButton mtgButton = Utils.GenerateRadioButton(new Rectangle(5, 5, 120, BUTTON_HEIGHT), "Magic");
			mtgButton.CheckedChanged += SetCatalogMTG;
			catalogPanel.Controls.Add(mtgButton);

			//Yu-Gi-Oh! button
			RadioButton ygoButton = Utils.GenerateRadioButton(new Rectangle(130, 5, 120, BUTTON_HEIGHT), "Yu-Gi-Oh!");
			ygoButton.CheckedChanged += SetCatalogYGO;
			catalogPanel.Controls.Add(ygoButton);

			//Pokémon button
			RadioButton pkmnButton = Utils.GenerateRadioButton(new Rectangle(255, 5, 120, BUTTON_HEIGHT), "Pokémon");
			pkmnButton.CheckedChanged += SetCatalogPKMN;
			catalogPanel.Controls.Add(pkmnButton);

			//Search tools
			searchPanel = Utils.GenerateTrackerPanel(Utils.CenterRect(new Size(382, 77), panel.Size, new Point(0, 32)));
			searchPanel.Anchor = AnchorStyles.None;

			//Search bar
			searchBox = Utils.GenerateTextBox(new Rectangle(5, 5, 370, BUTTON_HEIGHT), searchString != null ? searchString : "Search");
			searchBox.Size = new Size(370, 30);
			searchBox.Click += SearchBoxClicked;
			searchBox.KeyPress += SearchEnterPressed;
			searchPanel.Controls.Add(searchBox);

			//Search button
			Button searchButton = Utils.GenerateButton(new Rectangle(5, 35, 183, BUTTON_HEIGHT), "Search");
			searchButton.Click += SearchButtonClicked;
			searchPanel.Controls.Add(searchButton);

			//Setlist button
			Button setlistButton = Utils.GenerateButton(new Rectangle(192, 35, 183, BUTTON_HEIGHT), "All Sets");
			setlistButton.Click += OpenSetlist;
			searchPanel.Controls.Add(setlistButton);

			//Add to panel
			searchPanel.Visible = false;
			panel.Controls.Add(catalogPanel);
			panel.Controls.Add(searchPanel);

			//Check current catalog
			if (TrackerForm.Catalog != null) {
				if (TrackerForm.Catalog.Game == Game.MTG)
					mtgButton.Checked = true;
				else if (TrackerForm.Catalog.Game == Game.YGO)
					ygoButton.Checked = true;
				else if (TrackerForm.Catalog.Game == Game.PKMN)
					pkmnButton.Checked = true;
			}

		}

		//Set current catalog
		private void SetCatalogMTG(object sender, EventArgs e) => SetCatalog(sender, TrackerForm.Instance.SetCatalogMTG);
		private void SetCatalogYGO(object sender, EventArgs e) => SetCatalog(sender, TrackerForm.Instance.SetCatalogYGO);
		private void SetCatalogPKMN(object sender, EventArgs e) => SetCatalog(sender, TrackerForm.Instance.SetCatalogPKMN);
		private void SetCatalog(object sender, Action action) {
			if (sender is RadioButton rb && rb.Checked) {
				action.Invoke();
				if (!searchPanel.Visible)
					searchPanel.Visible = true;
			}
		}

		//Show setlist
		private void OpenSetlist(object sender, EventArgs e) => TrackerForm.Instance.SetPage<Setlist>();

		//Search event handlers
		private void SearchBoxClicked(object sender, EventArgs e) {
			if (searchBox.Text.Equals("Search"))
				searchBox.SelectAll();
		}
		private void SearchButtonClicked(object sender, EventArgs e) => Search();
		private void SearchEnterPressed(object sender, KeyPressEventArgs e) {
			if (e.KeyChar == (char)Keys.Return)
				Search();
		}
		private void Search() {
			searchString = searchBox.Text;
			TrackerForm.Instance.SetPage<Printlist>(searchTerms: searchBox.Text);
		}

	}

}
