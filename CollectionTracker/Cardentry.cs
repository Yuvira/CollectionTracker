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
		private Button returnButton;

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
			foreach (Face face in card.Faces) {
				Fieldlist fieldList = new Fieldlist(this, "Face", face.Fields, 0);
				fieldList.OnListResize += OnFieldsResized;
				listPanel.Controls.Add(fieldList.Panel);
				faces.Add(fieldList);
			}

			//Save button
			saveButton = Utils.GenerateButton(new Rectangle(0, 0, 100, BUTTON_HEIGHT), "Save");
			saveButton.Click += SaveCard;
			listPanel.Controls.Add(saveButton);

			//Return button
			returnButton = Utils.GenerateButton(new Rectangle(105, 0, 100, BUTTON_HEIGHT), "Return");
			returnButton.Click += ReturnToDetails;
			listPanel.Controls.Add(returnButton);

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
			returnButton.Location = new Point(105, yPos);
		}

		//Save
		private void SaveCard(object sender, EventArgs e) {
			if (cardref != null) {
				string name = printref.GetField("name");
				List<Dictionary<string, string>> faceDicts = new List<Dictionary<string, string>>();
				foreach (Fieldlist face in faces)
					faceDicts.Add(face.GetFieldDict());
				cardref.CopyFields(fields.GetFieldDict(), faceDicts);
				if (!printref.GetField("name").Equals(name))
					Printentry.CardsAltered = true;
			}
			if (printref != null)
				parent.SetPage(new Detailpage(parent, printref));
		}
		private void ReturnToDetails(object sender, EventArgs e) {
			if (printref != null)
				parent.SetPage(new Detailpage(parent, printref));
		}

	}

}
