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
		private Catalog mtgCatalog;
		private Catalog ygoCatalog;
		private Catalog pkmnCatalog;
		private Catalog curCatalog = null;
		private TrackerPage page = null;
		private Printlist savedPrintlist = null;
		private Printentry savedPrintentry = null;

		//Controls
		private MenuStrip toolbar;
		private ToolStripLabel catalogLabel;
		private ToolStripButton saveButton;
		private ToolStripButton setButton;
		private ToolStripButton printListButton;

		//Accessors
		public Catalog Catalog => curCatalog;
		public TrackerPage Page => page;
		public int ToolbarHeight => toolbar.Height;
		public Printlist Printlist => savedPrintlist;

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
			setButton = Utils.GenerateTSButton("Sets", OpenSetentries);
			setButton.Enabled = false;
			dropDown.Items.Add(setButton);
			printListButton = Utils.GenerateTSButton("Prints", ShowPrintlist);
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

		#region Catalog Setters

		//Set current catalog
		public void SetCatalogMTG() {
			curCatalog = mtgCatalog;
			catalogLabel.Text = "Magic";
			EnableCatalogButtons();
		}
		public void SetCatalogYGO() {
			curCatalog = ygoCatalog;
			catalogLabel.Text = "Yu-Gi-Oh!";
			EnableCatalogButtons();
		}
		public void SetCatalogPKMN() {
			curCatalog = pkmnCatalog;
			catalogLabel.Text = "Pokémon";
			EnableCatalogButtons();
		}
		private void EnableCatalogButtons() {
			saveButton.Enabled = true;
			setButton.Enabled = true;
		}

		#endregion

		#region Page Setters

		//Open pages
		public void OpenHomePage(object sender = null, EventArgs e = null) => SetPage(new Homepage(this));
		public void OpenSetentries(object sender = null, EventArgs e = null) => SetPage(new Setentrylist(this));

		//Replace current page
		public void SetPage(TrackerPage page) {
			if (savedPrintlist != null && !(page is Detailpage dp || page is Cardentry ce || page is Printentry pe)) {
				toolbar.Items.Remove(printListButton);
				savedPrintlist.Dispose();
				savedPrintlist = null;
			}
			if (this.page != null && this.page == savedPrintentry)
				savedPrintentry.Panel.Hide();
			else if (this.page != null) {
				Controls.Remove(this.page.Panel);
				this.page.Dispose();
			}
			Controls.Add(page.Panel);
			this.page = page;
		}

		//Save printlist and load detail page
		public void ShowDetails(Printlist pl, Detailpage dp) {
			if (page != pl)
				return;
			toolbar.Items.Add(printListButton);
			Controls.Remove(pl.Panel);
			Controls.Add(dp.Panel);
			savedPrintlist = pl;
			page = dp;
			dp.UpdateNavButtons();
		}
		public void ShowPrintlist(object sender, EventArgs e) {
			toolbar.Items.Remove(printListButton);
			Controls.Remove(page.Panel);
			page.Dispose();
			Controls.Add(savedPrintlist.Panel);
			savedPrintlist.UpdateEntries();
			page = savedPrintlist;
			savedPrintlist = null;
		}

		//Save printentry
		public void ShowPrintentry(Printing printing) {
			if (page != null) {
				Controls.Remove(page.Panel);
				page.Dispose();
			}
			if (savedPrintentry == null) {
				savedPrintentry = new Printentry(this, printing);
				Controls.Add(savedPrintentry.Panel);
			}
			else {
				savedPrintentry.LoadPrinting(printing);
				savedPrintentry.Panel.Show();
			}
			page = savedPrintentry;
		}

		#endregion

		#region I/O

		//Save current catalog
		public void SaveCatalog(object sender, EventArgs e) {
			if (!Utils.ResourcePaths.ContainsKey(Catalog.Game))
				return;
			Catalog.SaveToFile(Utils.ResourcePaths[Catalog.Game] + "catalog2.bin");
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

		#endregion

	}

}