using ProtoBuf;
using System;
using System.Collections.Generic;
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

		//Controls
		private MenuStrip toolbar;
		private ToolStripLabel catalogLabel;
		private ToolStripDropDownButton entryDDButton;
		private ToolStripButton saveButton;
		private ToolStripButton printListButton;

		//Saved pages
		private Printlist savedPrintlist = null;
		private Printentry savedPrintentry = null;

		//Accessors
		public int ToolbarHeight => toolbar.Height;
		public List<Printing> FilteredPrints => savedPrintlist?.FilteredPrints;

		//Static
		public static TrackerForm Instance = null;
		public static Catalog Catalog = null;
		public static TrackerPage Page = null;
		public static FieldContext FieldContext = FieldContext.NONE;

		//Constructor
		public TrackerForm() {

			//Initialize form
			BackColor = Utils.COLOR_BACK;
			ClientSize = new Size(1600, 900);
			Font = Utils.FONT_DEFAULT;
			Text = "Collection Tracker";
			KeyPreview = true;
			Instance = this;

			//Toolbar
			toolbar = new MenuStrip();

			//File - Home / Save
			ToolStripDropDown fileDropDown = new ToolStripDropDown();
			fileDropDown.Items.Add(Utils.GenerateTSButton("Home", OpenHomePage));
			saveButton = Utils.GenerateTSButton("Save", SaveCatalog, false);
			fileDropDown.Items.Add(saveButton);
			toolbar.Items.Add(Utils.GenerateTSDDButton("File", fileDropDown));

			//Entries - Sets / Card / Printing
			ToolStripDropDown entryDropDown = new ToolStripDropDown();
			entryDropDown.Items.Add(Utils.GenerateTSButton("Sets", OpenSetentries));
			entryDropDown.Items.Add(Utils.GenerateTSButton("Card", OpenCardEntry));
			entryDropDown.Items.Add(Utils.GenerateTSButton("Printing", OpenPrintEntry));
			entryDDButton = Utils.GenerateTSDDButton("Entries", entryDropDown, false);
			toolbar.Items.Add(entryDDButton);

			//Print list ref
			printListButton = Utils.GenerateTSButton("Prints", OpenPrintlist);

			//Label
			catalogLabel = Utils.GenerateTSLabel("", true, 10);
			toolbar.Items.Add(catalogLabel);

			//Add
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
			SetPage<Homepage>();

		}

		#region Catalog Setters

		//Set current catalog
		public void SetCatalogMTG() {
			Catalog = mtgCatalog;
			catalogLabel.Text = "Magic";
			EnableCatalogButtons();
		}
		public void SetCatalogYGO() {
			Catalog = ygoCatalog;
			catalogLabel.Text = "Yu-Gi-Oh!";
			EnableCatalogButtons();
		}
		public void SetCatalogPKMN() {
			Catalog = pkmnCatalog;
			catalogLabel.Text = "Pokémon";
			EnableCatalogButtons();
		}
		private void EnableCatalogButtons() {
			saveButton.Enabled = true;
			entryDDButton.Enabled = true;
			Printentry.SetsAltered = true;
			Printentry.CardsAltered = true;
		}

		#endregion

		#region Page Setters

		//Open pages
		public void OpenHomePage(object sender, EventArgs e) => SetPage<Homepage>();
		public void OpenPrintlist(object sender, EventArgs e) => SetPage<Printlist>();
		public void OpenSetentries(object sender, EventArgs e) => SetPage<Setentrylist>();
		public void OpenCardEntry(object sender, EventArgs e) => SetPage<Cardentry>();
		public void OpenPrintEntry(object sender, EventArgs e) => SetPage<Printentry>();

		//Set new page
		public void SetPage<T>(Card cardref = null, Printing printref = null, string searchTerms = "") where T : TrackerPage {

			//Clear print list if moving to unsupported page
			bool TKeepsPrintlist = typeof(T) == typeof(Printlist) || typeof(T) == typeof(Detailpage) || typeof(T) == typeof(Cardentry) || typeof(T) == typeof(Printentry);
			if (savedPrintlist != null && !TKeepsPrintlist) {
				toolbar.Items.Remove(printListButton);
				if (Page != savedPrintlist)
					savedPrintlist.Dispose();
				savedPrintlist = null;
			}

			//Clear current page
			if (Page != null) {
				if (Page is Printlist pl && TKeepsPrintlist) {
					toolbar.Items.Add(printListButton);
					savedPrintlist = pl;
					Page.Panel.Hide();
				}
				else if (Page is Printentry pe) {
					savedPrintentry = pe;
					Page.Panel.Hide();
				}
				else if (Page != null) {
					Controls.Remove(Page.Panel);
					Page.Dispose();
				}
			}

			//Show saved page
			if (typeof(T) == typeof(Printlist) && savedPrintlist != null) {
				toolbar.Items.Remove(printListButton);
				savedPrintlist.Panel.Show();
				savedPrintlist.UpdateEntries();
				Page = savedPrintlist;
				savedPrintlist = null;
			}
			else if (typeof(T) == typeof(Printentry) && savedPrintentry != null) {
				savedPrintentry.Panel.Show();
				savedPrintentry.LoadPrinting(printref);
				Page = savedPrintentry;
			}

			//Generate new page
			else {
				if (typeof(T) == typeof(Homepage))
					Page = new Homepage();
				else if (typeof(T) == typeof(Setlist))
					Page = new Setlist();
				else if (typeof(T) == typeof(Printlist))
					Page = new Printlist(searchTerms);
				else if (typeof(T) == typeof(Detailpage)) {
					if (printref != null) {
						Detailpage dp = new Detailpage(printref);
						dp.UpdateNavButtons();
						Page = dp;
					}
					else
						InvokePageChangeError("Detailpage printing can't be null!");
				}
				else if (typeof(T) == typeof(Cardentry))
					Page = new Cardentry(cardref, printref);
				else if (typeof(T) == typeof(Printentry))
					Page = new Printentry(printref);
				else if (typeof(T) == typeof(Setentrylist))
					Page = new Setentrylist();
				Controls.Add(Page.Panel);
			}

		}

		//Default to homepage if invalid parameters provided
		private void InvokePageChangeError(string error) {
			MessageBox.Show(error);
			Page = new Homepage();
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