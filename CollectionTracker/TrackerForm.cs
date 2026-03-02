using ProtoBuf;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CollectionTracker {

	//Form class
	public partial class TrackerForm : Form {

		//Load legacy catalogs
		private const bool LOAD_LEGACY_CATALOGS = false;

		//Properties
		private MenuStrip toolbar;
		private ToolStripLabel catalogLabel;
		private ToolStripButton saveButton;
		private Catalog mtgCatalog;
		private Catalog ygoCatalog;
		private Catalog pkmnCatalog;
		private Catalog curCatalog = null;
		private TrackerPage page = null;

		//Accessors
		public Catalog Catalog => curCatalog;
		public TrackerPage Page => page;
		public int ToolbarHeight => toolbar.Height;

		//Constructor
		public TrackerForm() {

			//Initialize form
			BackColor = Utils.COLOR_BACK;
			ClientSize = new Size(1600, 900);
			Font = Utils.FONT_DEFAULT;
			Text = "Collection Tracker";
			KeyPreview = true;

			//Generate toolbar and add home button / catalog label
			toolbar = new MenuStrip();
			ToolStripDropDown dropDown = new ToolStripDropDown();
			dropDown.Items.Add(Utils.GenerateTSButton("Home", OpenHomePage));
			saveButton = Utils.GenerateTSButton("Save", SaveCatalog);
			saveButton.Enabled = false;
			dropDown.Items.Add(saveButton);
			toolbar.Items.Add(Utils.GenerateTSDDButton("File", dropDown));
			catalogLabel = Utils.GenerateTSLabel("", true, 10);
			toolbar.Items.Add(catalogLabel);
			Controls.Add(toolbar);

			//Initialize catalogs
			if (LOAD_LEGACY_CATALOGS) {
				if (TryLoadCatalogFromFile("resources/mtg/catalog.bin", out MTG_Catalog mtg))
					mtgCatalog = new Catalog(mtg);
				if (TryLoadCatalogFromFile("resources/ygo/catalog.bin", out YGO_Catalog ygo))
					ygoCatalog = new Catalog(ygo);
				if (TryLoadCatalogFromFile("resources/pkmn/catalog.bin", out PKMN_Catalog pkmn))
					pkmnCatalog = new Catalog(pkmn);
			}
			else {
				mtgCatalog = Catalog.LoadFromFile("resources/mtg/catalog2.bin");
				ygoCatalog = Catalog.LoadFromFile("resources/ygo/catalog2.bin");
				pkmnCatalog = Catalog.LoadFromFile("resources/pkmn/catalog2.bin");
			}

			//Open homepage
			OpenHomePage();

		}

		//Open default page
		public void OpenHomePage(object sender = null, EventArgs e = null) => SetPage(new Homepage(this));

		//Save current catalog
		public void SaveCatalog(object sender, EventArgs e) {
			if (!Utils.ResourcePaths.ContainsKey(Catalog.Game))
				return;
			Catalog.SaveToFile(Utils.ResourcePaths[Catalog.Game] + "catalog2.bin");
		}

		//Set current catalog
		public void SetCatalogMTG() {
			curCatalog = mtgCatalog;
			catalogLabel.Text = "Magic";
			saveButton.Enabled = true;
		}
		public void SetCatalogYGO() {
			curCatalog = ygoCatalog;
			catalogLabel.Text = "Yu-Gi-Oh!";
			saveButton.Enabled = true;
		}
		public void SetCatalogPKMN() {
			curCatalog = pkmnCatalog;
			catalogLabel.Text = "Pokémon";
			saveButton.Enabled = true;
		}

		//Replace current page
		public void SetPage(TrackerPage page) {
			if (this.page != null) {
				this.page.Dispose();
				Controls.Remove(this.page.Panel);
			}
			Controls.Add(page.Panel);
			this.page = page;
		}

		//Catalog loaders
		public static bool TryLoadCatalogFromFile<T>(string path, out T catalog) {
			try {
				using (Stream stream = File.Open(path, FileMode.Open))
					catalog = Serializer.Deserialize<T>(stream);
				return true;
			}
			catch (Exception ex) {
				MessageBox.Show($"Error: {ex.Message}");
				catalog = default;
				return false;
			}
		}

	}

}