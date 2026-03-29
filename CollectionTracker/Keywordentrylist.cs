using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
			List<string> sortedKeys = TrackerForm.Catalog.Keywords.Keys.ToList();
			sortedKeys.Sort();
			Dictionary<string, string> sortedDict = new Dictionary<string, string>();
			foreach (string key in sortedKeys)
				sortedDict.Add(key, TrackerForm.Catalog.Keywords[key]);
			keywords = new Entrylist<FieldEntry>(this, "Keywords", FieldEntry.GenerateEntries(sortedDict));
			keywords.OnListResize += OnFieldsResized;
			listPanel.Controls.Add(keywords.Panel);

			//Save button
			saveButton = Utils.GenerateButton(new Rectangle(0, 0, 100, 30), "Save");
			saveButton.Click += SaveKeywords;
			listPanel.Controls.Add(saveButton);

			//Resume
			listPanel.ResumeLayout();

			//Resize
			OnFieldsResized();

			//Add to panel
			panel.Controls.Add(listPanel);

		}

		//On resize
		private void OnFieldsResized() {
			listPanel.SuspendLayout();
			Point pos = new Point(0, 0);
			listPanel.AutoScrollPosition = pos;
			keywords.Panel.Location = pos;
			pos.Y += keywords.Panel.Height + 5;
			saveButton.Location = pos;
			listPanel.ResumeLayout();
		}

		//Save
		private void SaveKeywords(object sender, EventArgs e) => TrackerForm.Catalog.CopyKeywords(FieldEntry.GetEntryDict(keywords.Entries));

	}

}
