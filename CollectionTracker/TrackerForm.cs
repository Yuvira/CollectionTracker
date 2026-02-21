using System.Drawing;
using System.Windows.Forms;

namespace CollectionTracker {

	//Form class
	public partial class TrackerForm : Form {

		//Properties
		private Catalog mtgCatalog;
		private Catalog ygoCatalog;
		private Catalog pkmnCatalog;
		private Catalog curCatalog = null;
		private TrackerPage page = null;

		//Accessors
		public Catalog Catalog => curCatalog;
		public TrackerPage Page => page;

		//Constructor
		public TrackerForm() {

			//Initialize form
			BackColor = Utils.COLOR_BACK;
			ClientSize = new Size(1600, 900);
			Name = "Collection Tracker";
			KeyPreview = true;

			//Initialize catalogs
			mtgCatalog = Catalog.LoadFromFile("resources/mtg/catalog2.bin");
			ygoCatalog = Catalog.LoadFromFile("resources/ygo/catalog2.bin");
			pkmnCatalog = Catalog.LoadFromFile("resources/pkmn/catalog2.bin");

			//Open homepage
			page = new Homepage(this);

		}

		//Replace current page
		public void SetPage(TrackerPage page) => this.page = page;

	}

}