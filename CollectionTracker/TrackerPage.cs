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
		public const int PANEL_MARGIN = 5;
		public const int SCROLL_MARGIN = 20;

		//Properties
		protected TrackerPanel panel;
		protected int oldWidth = 0;

		//Accessors
		public TrackerPanel Panel => panel;

		//Constructor
		public TrackerPage() {
			panel = Utils.GenerateTrackerPanel(
				new Rectangle(
					0,
					TrackerForm.Instance.ToolbarHeight,
					TrackerForm.Instance.ClientSize.Width,
					TrackerForm.Instance.ClientSize.Height - TrackerForm.Instance.ToolbarHeight
				)
			);
			panel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			TrackerForm.Instance.Resize += OnFormResize;
			TrackerForm.Instance.ResizeBegin += OnFormResizeBegin;
			TrackerForm.Instance.ResizeEnd += OnFormResizeEnd;
			oldWidth = panel.Width;
		}

		//Dispose
		public virtual void Dispose() {
			TrackerForm.Instance.Resize -= OnFormResize;
			TrackerForm.Instance.ResizeBegin -= OnFormResizeBegin;
			TrackerForm.Instance.ResizeEnd -= OnFormResizeEnd;
			panel.Dispose();
		}

		//Resize
		protected virtual void OnFormResize(object sender, EventArgs e) { }
		protected virtual void OnFormResizeBegin(object sender, EventArgs e) { }
		protected virtual void OnFormResizeEnd(object sender, EventArgs e) { }

	}

}
