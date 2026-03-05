using System;
using System.Drawing;
using System.Windows.Forms;

namespace CollectionTracker {

	public class Keywordentrylist : TrackerPage {

		//Properties
		private Entrylist<FieldEntry> keywords;

		//Controls
		private TrackerPanel listPanel;
		private Button saveButton;

		//Constructor
		public Keywordentrylist() : base() {

			//Panel
			listPanel = Utils.GenerateTrackerPanel(Utils.CenterRect(new Size(800, panel.Height - 25), panel.Size, new Point(0, 0)));
			listPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			listPanel.AutoScroll = true;

			//Suspend
			listPanel.SuspendLayout();

			//Fields
			keywords = new Entrylist<FieldEntry>(this, "Keywords", FieldEntry.GenerateEntries(TrackerForm.Catalog.Keywords));
			keywords.OnListResize += OnFieldsResized;
			listPanel.Controls.Add(keywords.Panel);

			//Save button
			saveButton = Utils.GenerateButton(new Rectangle(0, 0, 100, 30), "Save");
			saveButton.Click += SaveKeywords;
			listPanel.Controls.Add(saveButton);

			//Resize
			OnFieldsResized();

			//Resume
			listPanel.ResumeLayout();

			//Add to panel
			panel.Controls.Add(listPanel);

		}

		//On resize
		private void OnFieldsResized() {
			Point pos = new Point(0, 0);
			keywords.Panel.Location = pos;
			pos.Y += keywords.Panel.Height + 5;
			saveButton.Location = pos;
		}

		//Save
		private void SaveKeywords(object sender, EventArgs e) {
			TrackerForm.Catalog.CopyKeywords(FieldEntry.GetEntryDict(keywords.Entries));
			TrackerForm.Instance.UpdateKeywords();
		}

	}

}
