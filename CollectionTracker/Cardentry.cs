using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CollectionTracker {

	public class Cardentry : TrackerPage {

		//Properties
		private Card cardref;
		private Printing printref;
		private Entrylist<FieldEntry> fields;
		private List<Entrylist<FieldEntry>> faces;

		//Controls
		private TrackerPanel listPanel;
		private Button addButton;
		private Button saveButton;
		private Button returnButton;

		//Constructor
		public Cardentry(Card card, Printing print) : base() {

			//References
			cardref = card;
			printref = print;

			//Context
			TrackerForm.FieldContext = FieldContext.CARD;

			//Panel
			listPanel = Utils.GenerateTrackerPanel(Utils.CenterRect(new Size(800, panel.Height - 25), panel.Size, new Point(0, 0)));
			listPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			listPanel.AutoScroll = true;

			//Suspend
			listPanel.SuspendLayout();

			//Fields
			Dictionary<string, string> fieldDict;
			if (cardref != null)
				fieldDict = cardref.Fields;
			else {
				fieldDict = new Dictionary<string, string>();
				if (Utils.DefaultCardFields.ContainsKey(TrackerForm.Catalog.Game)) {
					foreach (string fieldName in Utils.DefaultCardFields[TrackerForm.Catalog.Game])
						fieldDict.Add(fieldName, "");
				}
			}
			fields = new Entrylist<FieldEntry>(this, "Fields", FieldEntry.GenerateEntries(fieldDict));
			fields.OnListResize += OnFieldsResized;
			listPanel.Controls.Add(fields.Panel);

			//Faces
			faces = new List<Entrylist<FieldEntry>>();
			if (cardref != null) {
				foreach (Face face in cardref.Faces) {
					Entrylist<FieldEntry> fieldList = new Entrylist<FieldEntry>(this, "Face", FieldEntry.GenerateEntries(face.Fields));
					fieldList.OnListEmpty += OnFaceEmpty;
					fieldList.OnListResize += OnFieldsResized;
					faces.Add(fieldList);
					listPanel.Controls.Add(fieldList.Panel);
				}
			}

			//Add face button
			addButton = Utils.GenerateButton(new Rectangle(0, 0, 30, 30), "+");
			addButton.Click += AddFace;
			listPanel.Controls.Add(addButton);

			//Save button
			saveButton = Utils.GenerateButton(new Rectangle(35, 0, 100, 30), "Save");
			saveButton.Click += SaveCard;
			listPanel.Controls.Add(saveButton);

			//Return button
			returnButton = Utils.GenerateButton(new Rectangle(140, 0, 100, 30), "Return");
			returnButton.Click += ReturnToDetails;
			listPanel.Controls.Add(returnButton);

			//Resize
			OnFieldsResized();

			//Resume
			listPanel.ResumeLayout();

			//Add to panel
			panel.Controls.Add(listPanel);

		}

		//Add face
		private void AddFace(object sender, EventArgs e) {
			Dictionary<string, string> fieldDict = new Dictionary<string, string>();
			if (Utils.DefaultCardFields.ContainsKey(TrackerForm.Catalog.Game)) {
				foreach (string fieldName in Utils.DefaultCardFields[TrackerForm.Catalog.Game])
					fieldDict.Add(fieldName, "");
			}
			Entrylist<FieldEntry> fieldList = new Entrylist<FieldEntry>(this, "Face", FieldEntry.GenerateEntries(fieldDict));
			fieldList.OnListEmpty += OnFaceEmpty;
			fieldList.OnListResize += OnFieldsResized;
			faces.Add(fieldList);
			listPanel.Controls.Add(fieldList.Panel);
			OnFieldsResized();
		}

		//On empty
		private void OnFaceEmpty(Entrylist<FieldEntry> list) {
			if (!faces.Contains(list))
				return;
			faces.Remove(list);
			listPanel.Controls.Remove(list.Panel);
			list.Panel.Dispose();
		}

		//On resize
		private void OnFieldsResized() {
			Point pos = new Point(0, 0);
			fields.Panel.Location = pos;
			pos.Y += fields.Panel.Height + 5;
			foreach (Entrylist<FieldEntry> face in faces) {
				face.Panel.Location = pos;
				pos.Y += face.Panel.Height + 5;
			}
			addButton.Location = pos;
			saveButton.Location = new Point(35, pos.Y);
			returnButton.Location = new Point(140, pos.Y);
		}

		//Save
		private void SaveCard(object sender, EventArgs e) {

			//Faces
			List<Dictionary<string, string>> faceDicts = new List<Dictionary<string, string>>();
			foreach (Entrylist<FieldEntry> face in faces)
				faceDicts.Add(FieldEntry.GetEntryDict(face.Entries));

			//Copy to existing card ref
			if (cardref != null) {
				string name = printref.GetField("name");
				cardref.CopyFields(FieldEntry.GetEntryDict(fields.Entries), faceDicts);
				if (!printref.GetField("name").Equals(name))
					Printentry.CardsAltered = true;
			}

			//Generate new card
			else {
				Card card = new Card();
				card.CopyFields(FieldEntry.GetEntryDict(fields.Entries), faceDicts);
				TrackerForm.Catalog.Cards.Add(card);
				Printentry.CardsAltered = true;
			}

			//Return to details
			if (printref != null)
				ReturnToDetails();

			//Generate new
			else
				TrackerForm.Instance.SetPage<Cardentry>();

		}

		//Return to details
		private void ReturnToDetails(object sender = null, EventArgs e = null) {
			if (printref != null)
				TrackerForm.Instance.SetPage<Detailpage>(printref: printref);
		}

	}

}
