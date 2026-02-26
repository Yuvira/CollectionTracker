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
				panel = Utils.GenerateTrackerPanel(new Rectangle(0, yPos, 780, (cardFields.Keys.Count * 35) + TEXT_HEIGHT));

				//Header label
				Label header = Utils.GenerateLabel(new Rectangle(0, 0, 780, TEXT_HEIGHT), headerText);
				panel.Controls.Add(header);

				//Fields
				int idx = 0;
				fields = new List<TextBox>();
				values = new List<TextBox>();
				panel.SuspendLayout();
				foreach (string field in cardFields.Keys) {
					fields.Add(Utils.GenerateTextBox(new Rectangle(0, TEXT_HEIGHT + 5 + (idx * 35), 150, 30), field));
					values.Add(Utils.GenerateTextBox(new Rectangle(155, TEXT_HEIGHT + 5 + (idx * 35), 625, 30), cardFields[field]));
					panel.Controls.Add(fields[fields.Count - 1]);
					panel.Controls.Add(values[values.Count - 1]);
					++idx;
				}
				panel.ResumeLayout();

			}

		}

		#endregion

		//Properties
		private Fieldlist fields;
		private List<Fieldlist> faces;
		private TrackerPanel listPanel;

		//Constructor
		public Cardentry(TrackerForm form, Card card) : base(form) {

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

			//Resume
			listPanel.ResumeLayout();

			//Add to panel
			panel.Controls.Add(listPanel);

		}

	}
}
