using System.Windows.Forms;

namespace CollectionTracker {

	//Form class
	public partial class TrackerForm : Form {

		//Constructor
		public TrackerForm() {
			BackColor = System.Drawing.SystemColors.ControlDark;
			ClientSize = new System.Drawing.Size(1600, 900);
			Name = "Collection Tracker";
			KeyPreview = true;
		}

	}

}