using System;
using System.Drawing;
using System.Windows.Forms;

namespace CollectionTracker {

	//Homepage
	public class Homepage : TrackerPage {

		//Properties
		TrackerPanel searchPanel;
		TextBox searchBox;

		//Constructor
		public Homepage(TrackerForm form) : base(form) { }

		//Initializer
		public override void Init() {

			//Catalog selectors
			TrackerPanel catalogPanel = Utils.GenerateTrackerPanel(Utils.CenterRect(new Size(382, 42), panel.Size, new Point(0, 32)));
			catalogPanel.Anchor = AnchorStyles.None;

			//Magic button
			RadioButton mtgButton = Utils.GenerateRadioButton(new Rectangle(5, 5, 120, 30), "Magic");
			mtgButton.CheckedChanged += new EventHandler(SetCatalogMTG);
			catalogPanel.Controls.Add(mtgButton);

			//Yu-Gi-Oh! button
			RadioButton ygoButton = Utils.GenerateRadioButton(new Rectangle(130, 5, 120, 30), "Yu-Gi-Oh!");
			ygoButton.CheckedChanged += new EventHandler(SetCatalogYGO);
			catalogPanel.Controls.Add(ygoButton);

			//Pokémon button
			RadioButton pkmnButton = Utils.GenerateRadioButton(new Rectangle(255, 5, 120, 30), "Pokémon");
			pkmnButton.CheckedChanged += new EventHandler(SetCatalogPKMN);
			catalogPanel.Controls.Add(pkmnButton);

			//Search tools
			searchPanel = Utils.GenerateTrackerPanel(Utils.CenterRect(new Size(382, 77), panel.Size, new Point(0, -32)));
			searchPanel.Anchor = AnchorStyles.None;

			//Search bar
			searchBox = Utils.GenerateTextBox(new Rectangle(5, 5, 370, 30), "Search");
			searchBox.Size = new Size(370, 30);
			searchBox.KeyPress += new KeyPressEventHandler(SearchEnterPressed);
			searchPanel.Controls.Add(searchBox);

			//Search button
			Button searchButton = Utils.GenerateButton(new Rectangle(5, 35, 183, 30), "Search");
			searchButton.Click += new EventHandler(SearchButtonPressed);
			searchPanel.Controls.Add(searchButton);

			//Setlist button
			Button setlistButton = Utils.GenerateButton(new Rectangle(192, 35, 183, 30), "All Sets");
			setlistButton.Click += new EventHandler(OpenSetlist);
			searchPanel.Controls.Add(setlistButton);

			//Add to panel
			searchPanel.Visible = false;
			panel.Controls.Add(catalogPanel);
			panel.Controls.Add(searchPanel);

		}

		//Set current catalog
		private void SetCatalogMTG(object sender, EventArgs e) => SetCatalog(sender, parent.SetCatalogMTG);
		private void SetCatalogYGO(object sender, EventArgs e) => SetCatalog(sender, parent.SetCatalogYGO);
		private void SetCatalogPKMN(object sender, EventArgs e) => SetCatalog(sender, parent.SetCatalogPKMN);
		private void SetCatalog(object sender, Action action) {
			if (sender is RadioButton rb && rb.Checked) {
				action.Invoke();
				if (!searchPanel.Visible)
					searchPanel.Visible = true;
			}
		}

		//Show setlist
		private void OpenSetlist(object sender, EventArgs e) {

		}

		//Search event handlers
		private void SearchButtonPressed(object sender, EventArgs e) => Search();
		private void SearchEnterPressed(object sender, KeyPressEventArgs e) {
			if (e.KeyChar == (char)Keys.Return)
				Search();
		}
		private void Search() => MessageBox.Show(searchBox.Text);

	}

}
