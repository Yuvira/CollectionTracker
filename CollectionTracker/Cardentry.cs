using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	public class Cardentry : TrackerPage {

		#region Field Entry Class

		//Field entry
		private class Fieldlist {

			//Properties
			private Cardentry parent;

			//Controls
			private TrackerPanel panel;
			private List<TextBox> fields;
			private List<TextBox> values;

			//Accessors
			public TrackerPanel Panel => panel;

			//Constructor
			public Fieldlist(Cardentry parent, string headerText, Dictionary<string, string> cardFields, int yPos) {

				//Initial values
				this.parent = parent;

				//Panel
				panel = Utils.GenerateTrackerPanel(new Rectangle(0, yPos, 780, 10));

				//Header label
				Label header = Utils.GenerateLabel(new Rectangle(0, 0, 780, TEXT_HEIGHT), headerText);
				panel.Controls.Add(header);

				//Fields
				yPos = TEXT_HEIGHT + 5;
				fields = new List<TextBox>();
				values = new List<TextBox>();
				panel.SuspendLayout();
				foreach (string field in cardFields.Keys) {
					fields.Add(Utils.GenerateTextBox(new Rectangle(0, yPos, 150, 30), field));
					if (field.ToLower().Equals("oracle")) {
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

			//Get fields as dictionary
			public Dictionary<string, string> GetFieldDict() {
				Dictionary<string, string> dict = new Dictionary<string, string>();
				for (int i = 0; i < fields.Count; ++i)
					if (!dict.ContainsKey(fields[i].Text))
						dict.Add(fields[i].Text, values[i].Text);
				return dict;
			}

		}

		#endregion

		//Properties
		private Card cardref;
		private Printing printref;
		private Fieldlist fields;
		private List<Fieldlist> faces;
		private TrackerPanel listPanel;

		//Constructor
		public Cardentry(TrackerForm form, Card card, Printing print = null) : base(form) {

			//References
			cardref = card;
			printref = print;

			//Panel
			listPanel = Utils.GenerateTrackerPanel(Utils.CenterRect(new Size(800, panel.Height - 25), panel.Size, new Point(0, 0)));
			listPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			listPanel.AutoScroll = true;

			//Suspend
			listPanel.SuspendLayout();

			//Fields
			fields = new Fieldlist(this, "Fields", card.Fields, 0);
			listPanel.Controls.Add(fields.Panel);

			//Faces
			faces = new List<Fieldlist>();
			int yPos = fields.Panel.Height + 5;
			foreach (Dictionary<string, string> face in card.Faces) {
				faces.Add(new Fieldlist(this, "Face", face, yPos));
				listPanel.Controls.Add(faces[faces.Count - 1].Panel);
				yPos += faces[faces.Count - 1].Panel.Height + 5;
			}

			//Save button
			Button saveButton = Utils.GenerateButton(new Rectangle(0, yPos, 100, BUTTON_HEIGHT), "Save");
			saveButton.Click += SaveCard;
			listPanel.Controls.Add(saveButton);

			//Resume
			listPanel.ResumeLayout();

			//Add to panel
			panel.Controls.Add(listPanel);

		}

		//Save
		private void SaveCard(object sender, EventArgs e) {
			if (cardref != null) {
				List<Dictionary<string, string>> faceDicts = new List<Dictionary<string, string>>();
				foreach (Fieldlist face in faces)
					faceDicts.Add(face.GetFieldDict());
				cardref.CopyFields(fields.GetFieldDict(), faceDicts);
			}
			if (printref != null)
				parent.SetPage(new Detailpage(parent, printref));
		}

	}

}
