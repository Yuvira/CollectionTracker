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
		public Catalog Catalog => parent.Catalog;

		//Constructor
		public TrackerPage(TrackerForm form) {
			parent = form;
			panel = Utils.GenerateTrackerPanel(new Rectangle(0, parent.ToolbarHeight, form.ClientSize.Width, form.ClientSize.Height - parent.ToolbarHeight));
			panel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			Init();
		}

		//Initialize
		public abstract void Init();

	}

}
