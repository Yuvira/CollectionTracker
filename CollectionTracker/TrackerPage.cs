using System.Drawing;
using System.Windows.Forms;

namespace CollectionTracker {

	//Page class
	public abstract class TrackerPage {

		//Properties
		protected TrackerForm parent;
		protected TrackerPanel panel;

		//Constructor
		public TrackerPage(TrackerForm form) {

			//Generate panel and initialize
			parent = form;
			panel = Utils.GenerateTrackerPanel(new Rectangle(Point.Empty, form.Size));
			panel.Dock = DockStyle.Fill;
			Init();

			//Replace active panel
			parent.Page?.RemovePanel();
			parent.Controls.Add(panel);
			parent.SetPage(this);

		}

		//Initialize
		public abstract void Init();

		//Remove panel from form controls
		public void RemovePanel() => parent.Controls.Remove(panel);

	}

}
