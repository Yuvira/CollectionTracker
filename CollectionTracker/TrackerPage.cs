using System;
using System.Drawing;
using System.Windows.Forms;

namespace CollectionTracker {

	//Page class
	public abstract class TrackerPage {

		//Formatting constants
		public const int TEXT_HEIGHT = 21;
		public const int TEXT_MARGIN = 9;
		public const int LINE_SPACING = 9;
		public const int BLOCK_SPACING = 17;
		public const int LEFT_PAD = 8;
		public const int TOP_PAD = 8;
		public const int BOTTOM_PAD = 8;
		public const int BUTTON_HEIGHT = 30;

		//Properties
		protected TrackerForm parent;
		protected TrackerPanel panel;

		//Accessors
		public TrackerPanel Panel => panel;

		//Constructor
		public TrackerPage(TrackerForm form) {
			parent = form;
			panel = Utils.GenerateTrackerPanel(new Rectangle(0, parent.ToolbarHeight, form.ClientSize.Width, form.ClientSize.Height - parent.ToolbarHeight));
			panel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			parent.Resize += OnFormResize;
		}

		//Dispose
		public virtual void Dispose() {
			parent.Resize -= OnFormResize;
			panel.Dispose();
		}

		//Resize
		protected virtual void OnFormResize(object sender, EventArgs e) { }

	}

}
