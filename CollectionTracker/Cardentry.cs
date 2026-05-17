using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	public class Cardentry : TrackerPage {

		//Constants
		private const int PANEL_WIDTH = 800;
		private const int BUTTON_WIDTH = 100;

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
		private Button artButton;
		private Button frontButton;
		private ComboBox keywordBox;
		private Button copyKeywordButton;
		private Button defaultFieldsButton;

		//Constructor
		public Cardentry(Card card, Printing print) : base() {

			//References
			cardref = card;
			printref = print;

			//Context
			TrackerForm.FieldContext = FieldContext.CARD;

			//Panel
			listPanel = Utils.GenerateTrackerPanel(new Rectangle((panel.Width - PANEL_WIDTH) / 2, PANEL_MARGIN, PANEL_WIDTH, panel.Height - (PANEL_MARGIN * 2)));
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
			addButton = Utils.GenerateButton(new Rectangle(0, 0, BUTTON_HEIGHT, BUTTON_HEIGHT), "+");
			addButton.Click += AddFace;
			listPanel.Controls.Add(addButton);

			//Save button
			saveButton = Utils.GenerateButton(new Rectangle(addButton.Right + PANEL_MARGIN, 0, BUTTON_WIDTH, BUTTON_HEIGHT), "Save");
			saveButton.Click += SaveCard;
			listPanel.Controls.Add(saveButton);

			//Return button
			returnButton = Utils.GenerateButton(new Rectangle(saveButton.Right + PANEL_MARGIN, 0, BUTTON_WIDTH, BUTTON_HEIGHT), "Return");
			returnButton.Click += ReturnToDetails;
			listPanel.Controls.Add(returnButton);

			//Keyword box
			int keywordWidth = (BUTTON_WIDTH * 2) + PANEL_MARGIN;
			keywordBox = Utils.GenerateComboBox(new Rectangle(listPanel.Width - (keywordWidth + SCROLL_MARGIN), 0, keywordWidth, BUTTON_HEIGHT), ComboBoxStyle.DropDownList, true);
			keywordBox.Items.AddRange(TrackerForm.Catalog.Keywords.Keys.ToArray());
			keywordBox.SelectedIndexChanged += CopyKeyword;
			listPanel.Controls.Add(keywordBox);

			//Keyword button
			copyKeywordButton = Utils.GenerateButton(new Rectangle(keywordBox.Left - (BUTTON_WIDTH + PANEL_MARGIN), 0, BUTTON_WIDTH, BUTTON_HEIGHT), "Copy");
			copyKeywordButton.Click += CopyKeyword;
			listPanel.Controls.Add(copyKeywordButton);

			//Front card button
			frontButton = Utils.GenerateButton(new Rectangle(listPanel.Width - (BUTTON_WIDTH + SCROLL_MARGIN), 0, BUTTON_WIDTH, BUTTON_HEIGHT), "Front");
			frontButton.Click += SetTypeFront;
			listPanel.Controls.Add(frontButton);

			//Art card button
			artButton = Utils.GenerateButton(new Rectangle(frontButton.Left - (BUTTON_WIDTH + PANEL_MARGIN), 0, BUTTON_WIDTH, BUTTON_HEIGHT), "Art");
			artButton.Click += SetTypeArt;
			listPanel.Controls.Add(artButton);

			//Art card button
			defaultFieldsButton = Utils.GenerateButton(new Rectangle(artButton.Left - (BUTTON_HEIGHT + PANEL_MARGIN), 0, BUTTON_HEIGHT, BUTTON_HEIGHT), "+");
			defaultFieldsButton.Click += AddDefaultFields;
			listPanel.Controls.Add(defaultFieldsButton);

			//Resume
			listPanel.ResumeLayout();

			//Resize
			OnFieldsResized();

			//Add to panel
			panel.Controls.Add(listPanel);

		}

		//Add face
		private void AddFace(object sender, EventArgs e) {
			listPanel.SuspendLayout();
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
			listPanel.ResumeLayout();
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
			listPanel.SuspendLayout();
			int y = 0;
			fields.Panel.Top = y;
			y += fields.Panel.Height + PANEL_MARGIN;
			foreach (Entrylist<FieldEntry> face in faces) {
				face.Panel.Top = y;
				y += face.Panel.Height + PANEL_MARGIN;
			}
			addButton.Top = y;
			saveButton.Top = y;
			returnButton.Top = y;
			keywordBox.Top = y;
			copyKeywordButton.Top = y;
			artButton.Top = y + BUTTON_HEIGHT + PANEL_MARGIN;
			frontButton.Top = y + BUTTON_HEIGHT + PANEL_MARGIN;
			defaultFieldsButton.Top = y + BUTTON_HEIGHT + PANEL_MARGIN;
			listPanel.ResumeLayout();
		}

		//Type shortcuts
		private void SetTypeArt(object sender, EventArgs e) => SetType("Art Card");
		private void SetTypeFront(object sender, EventArgs e) => SetType("Front Card");
		private void SetType(string type) {
			foreach (FieldEntry entry in fields.Entries) {
				if (entry.Field.Equals("type"))
					entry.SetValue(type);
			}
		}

		//Copy keyword value
		private void CopyKeyword(object sender, EventArgs e) {
			if (TrackerForm.Catalog.Keywords.Keys.Contains(keywordBox.Text))
				Clipboard.SetText(TrackerForm.Catalog.Keywords[keywordBox.Text]);
			else
				MessageBox.Show("Catalog has no Keyword entry \"keywordBox.Text\"!");
		}

		//Add missing default fields
		private void AddDefaultFields(object sender, EventArgs e) {
			if (Utils.DefaultCardFields.ContainsKey(TrackerForm.Catalog.Game))
				foreach (string fieldName in Utils.DefaultCardFields[TrackerForm.Catalog.Game])
					if (!fields.Entries.Select(entry => entry.Field).Contains(fieldName))
						fields.AddRow(new FieldEntry(fieldName, ""));
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
