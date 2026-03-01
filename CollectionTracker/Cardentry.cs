using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CollectionTracker {

	public class Cardentry : TrackerPage {

		//Properties
		private Card cardref;
		private Printing printref;
		private Fieldlist fields;
		private List<Fieldlist> faces;

		//Controls
		private TrackerPanel listPanel;
		private Button saveButton;

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
			fields.OnListResize += OnFieldsResized;
			listPanel.Controls.Add(fields.Panel);

			//Faces
			faces = new List<Fieldlist>();
			foreach (Dictionary<string, string> faceFields in card.Faces) {
				Fieldlist face = new Fieldlist(this, "Face", faceFields, 0);
				face.OnListResize += OnFieldsResized;
				listPanel.Controls.Add(face.Panel);
				faces.Add(face);
			}

			//Save button
			saveButton = Utils.GenerateButton(new Rectangle(0, 0, 100, BUTTON_HEIGHT), "Save");
			saveButton.Click += SaveCard;
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
			int yPos = fields.Panel.Height + 5;
			foreach (Fieldlist face in faces) {
				face.Panel.Location = new Point(0, yPos);
				yPos += face.Panel.Height + 5;
			}
			saveButton.Location = new Point(0, yPos);
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
