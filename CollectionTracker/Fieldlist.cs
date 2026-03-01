using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CollectionTracker {

	//Field entry
	public class Fieldlist {

		//Properties
		private TrackerPage parent;

		//Controls
		private TrackerPanel panel;
		private List<TextBox> fields;
		private List<TextBox> values;

		//Accessors
		public TrackerPanel Panel => panel;

		//Constructor
		public Fieldlist(TrackerPage parent, string headerText, Dictionary<string, string> cardFields, int yPos) {

			//Initial values
			this.parent = parent;

			//Panel
			panel = Utils.GenerateTrackerPanel(new Rectangle(0, yPos, 780, 10));

			//Header label
			Label header = Utils.GenerateLabel(new Rectangle(0, 0, 780, TrackerPage.TEXT_HEIGHT), headerText);
			panel.Controls.Add(header);

			//Fields
			if (cardFields != null) {
				yPos = TrackerPage.TEXT_HEIGHT + 5;
				fields = new List<TextBox>();
				values = new List<TextBox>();
				panel.SuspendLayout();
				foreach (string field in cardFields.Keys) {
					fields.Add(Utils.GenerateTextBox(new Rectangle(0, yPos, 150, 30), field));
					if (field.ToLower().Equals("oracle") || field.ToLower().Equals("flavor")) {
						values.Add(Utils.GenerateTextBox(new Rectangle(155, yPos, 625, 120), cardFields[field], true));
						yPos += 125;
					}
					else {
						values.Add(Utils.GenerateTextBox(new Rectangle(155, yPos, 625, 30), cardFields[field]));
						yPos += 35;
					}
					panel.Controls.Add(fields[fields.Count - 1]);
					panel.Controls.Add(values[values.Count - 1]);
				}
				panel.Height = yPos - 5;
				panel.ResumeLayout();
			}
			else
				panel.Height = TrackerPage.TEXT_HEIGHT;

		}

		//Get fields as dictionary
		public Dictionary<string, string> GetFieldDict() {
			Dictionary<string, string> dict = new Dictionary<string, string>();
			for (int i = 0; i < fields.Count; ++i)
				if (!dict.ContainsKey(fields[i].Text))
					dict.Add(fields[i].Text, values[i].Text);
			return dict;
		}

	}
}
