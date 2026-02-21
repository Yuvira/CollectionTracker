using System.Drawing;
using System.Windows.Forms;

namespace CollectionTracker {

	//Page class
	public abstract class TrackerPage {

		//Properties
		protected TrackerForm parent;
		protected TrackerPanel panel;

		//Accessors
		public TrackerPanel Panel => panel;

		//Constructor
		public TrackerPage(TrackerForm form) {
			parent = form;
			panel = Utils.GenerateTrackerPanel(new Rectangle(Point.Empty, form.ClientSize));
			panel.Dock = DockStyle.Fill;
			Init();
		}

		//Initialize
		public abstract void Init();

	}

}
