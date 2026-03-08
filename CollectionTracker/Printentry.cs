using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	public class Printentry : TrackerPage {

		//Properties
		private Printing printref;
		private Entrylist<TreatmentEntry> treatments;
		private Entrylist<FieldEntry> fields;
		private List<Entrylist<FieldEntry>> faces;
		private Entrylist<ImageEntry> images;

		//Controls
		private TrackerPanel listPanel;
		private ComboBox setBox;
		private ComboBox cardBox;
		private Button addFaceButton;
		private Button saveButton;
		private Button returnButton;

		//Static modified field identifiers
		public static bool SetsAltered = true;
		public static bool CardsAltered = true;

		//Constructor
		public Printentry(Printing print) : base() {

			//Panel
			listPanel = Utils.GenerateTrackerPanel(Utils.CenterRect(new Size(800, panel.Height - 25), panel.Size, new Point(0, 0)));
			listPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			listPanel.AutoScroll = true;

			//Set
			Label setLabel = Utils.GenerateLabel(new Rectangle(0, 0, 100, 30), "Set");
			setBox = Utils.GenerateComboBox(new Rectangle(105, 0, 675, 30), ComboBoxStyle.DropDownList, true);
			listPanel.Controls.Add(setLabel);
			listPanel.Controls.Add(setBox);

			//Card
			Label cardLabel = Utils.GenerateLabel(new Rectangle(0, 35, 100, 30), "Card");
			cardBox = Utils.GenerateComboBox(new Rectangle(105, 35, 675, 30), ComboBoxStyle.DropDownList, true);
			listPanel.Controls.Add(cardLabel);
			listPanel.Controls.Add(cardBox);

			//Field lists
			treatments = null;
			fields = null;
			images = null;
			faces = new List<Entrylist<FieldEntry>>();

			//Add face button
			addFaceButton = Utils.GenerateButton(new Rectangle(0, 70, 30, 30), "+");
			addFaceButton.Click += AddFace;
			listPanel.Controls.Add(addFaceButton);

			//Save button
			saveButton = Utils.GenerateButton(new Rectangle(35, 70, 100, 30), "Save");
			saveButton.Click += SavePrinting;
			listPanel.Controls.Add(saveButton);

			//Return button
			returnButton = Utils.GenerateButton(new Rectangle(140, 70, 100, 30), "Return");
			returnButton.Click += ReturnToDetails;
			listPanel.Controls.Add(returnButton);

			//Load
			LoadPrinting(print);

			//Add to panel
			panel.Controls.Add(listPanel);

		}

		//Load printing
		public void LoadPrinting(Printing print = null) {

			//References
			printref = print;

			//Context
			TrackerForm.FieldContext = FieldContext.PRINT;

			//Suspend
			listPanel.SuspendLayout();

			//Sets
			if (SetsAltered) {
				setBox.SuspendLayout();
				setBox.Items.Clear();
				setBox.Items.AddRange(TrackerForm.Catalog.Sets.ToArray());
				setBox.ResumeLayout();
				SetsAltered = false;
			}
			if (printref  != null)
				setBox.SelectedItem = printref.Set;

			//Cards
			if (CardsAltered) {
				cardBox.SuspendLayout();
				cardBox.Items.Clear();
				cardBox.Items.AddRange(TrackerForm.Catalog.Cards.ToArray());
				cardBox.ResumeLayout();
				CardsAltered = false;
			}
			if (printref != null)
				cardBox.SelectedItem = printref.Card;

			//Treatments
			if (treatments != null) {
				listPanel.Controls.Remove(treatments.Panel);
				treatments.Panel.Dispose();
			}
			List<string> treatmentList;
			if (printref != null)
				treatmentList = printref.Treatments.Select(t => t.Name).ToList();
			else if (Utils.DefaultTreatments.ContainsKey(TrackerForm.Catalog.Game))
				treatmentList = Utils.DefaultTreatments[TrackerForm.Catalog.Game];
			else
				treatmentList = new List<string>();
			treatments = new Entrylist<TreatmentEntry>(this, "Treatments", TreatmentEntry.GenerateEntries(treatmentList));
			treatments.OnListResize += OnFieldsResized;
			listPanel.Controls.Add(treatments.Panel);

			//Fields
			if (fields != null) {
				listPanel.Controls.Remove(fields.Panel);
				fields.Panel.Dispose();
			}
			Dictionary<string, string> fieldDict;
			if (printref != null)
				fieldDict = printref.Fields;
			else {
				fieldDict = new Dictionary<string, string>();
				if (Utils.DefaultPrintFields.ContainsKey(TrackerForm.Catalog.Game)) {
					foreach (string fieldName in Utils.DefaultPrintFields[TrackerForm.Catalog.Game])
						fieldDict.Add(fieldName, "");
				}
			}
			fields = new Entrylist<FieldEntry>(this, "Fields", FieldEntry.GenerateEntries(fieldDict));
			fields.OnListResize += OnFieldsResized;
			listPanel.Controls.Add(fields.Panel);

			//Faces
			for (int i = 0; i < faces.Count; ++i) {
				listPanel.Controls.Remove(faces[i].Panel);
				faces[i].Panel.Dispose();
			}
			faces.Clear();
			if (printref != null) {
				foreach (Face face in printref.Faces) {
					Entrylist<FieldEntry> fieldList = new Entrylist<FieldEntry>(this, "Face", FieldEntry.GenerateEntries(face.Fields));
					fieldList.OnListEmpty += OnFaceEmpty;
					fieldList.OnListResize += OnFieldsResized;
					faces.Add(fieldList);
					listPanel.Controls.Add(fieldList.Panel);
				}
			}

			//Images
			if (images != null) {
				listPanel.Controls.Remove(images.Panel);
				images.Panel.Dispose();
			}
			images = new Entrylist<ImageEntry>(this, "Images", printref == null ? new List<ImageEntry>() : ImageEntry.GenerateEntries(printref.ImagePaths));
			images.OnListResize += OnFieldsResized;
			listPanel.Controls.Add(images.Panel);

			//Resize
			OnFieldsResized();

			//Resume
			listPanel.ResumeLayout();

		}

		//Add face
		private void AddFace(object sender, EventArgs e) {
			Entrylist<FieldEntry> fieldList = new Entrylist<FieldEntry>(this, "Face", FieldEntry.GenerateEntries(new Dictionary<string, string>()));
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
			Point pos = new Point(0, 70);
			treatments.Panel.Location = pos;
			pos.Y += treatments.Panel.Height + 5;
			fields.Panel.Location = pos;
			pos.Y += fields.Panel.Height + 5;
			foreach (Entrylist<FieldEntry> face in faces) {
				face.Panel.Location = pos;
				pos.Y += face.Panel.Height + 5;
			}
			addFaceButton.Location = pos;
			pos.Y += 35;
			images.Panel.Location = pos;
			pos.Y += images.Panel.Height + 5;
			saveButton.Location = pos;
			returnButton.Location = pos.Add(105, 0);
		}

		//Save
		private void SavePrinting(object sender, EventArgs e) {

			//Skip if invalid references
			if (setBox.SelectedItem == null || !(setBox.SelectedItem is Set set)) {
				MessageBox.Show("Invalid set reference!");
				return;
			}
			if (cardBox.SelectedItem == null || !(cardBox.SelectedItem is Card card)) {
				MessageBox.Show("Invalid card reference!");
				return;
			}

			//Faces
			List<Dictionary<string, string>> faceDicts = new List<Dictionary<string, string>>();
			foreach (Entrylist<FieldEntry> face in faces)
				faceDicts.Add(FieldEntry.GetEntryDict(face.Entries));

			//Copy to existing print ref
			if (printref != null) {
				printref.CopySet(set);
				printref.CopyCard(card);
				printref.CopyTreatments(TreatmentEntry.GetEntryList(treatments.Entries));
				printref.CopyFields(FieldEntry.GetEntryDict(fields.Entries), faceDicts);
				printref.CopyImgPaths(ImageEntry.GetEntryList(images.Entries));
				ReturnToDetails();
			}

			//Generate new printing
			else {

				//Generate printing and add to catalog
				Printing print = new Printing();
				print.CopySet(set);
				print.CopyCard(card);
				print.CopyTreatments(TreatmentEntry.GetEntryList(treatments.Entries));
				print.CopyFields(FieldEntry.GetEntryDict(fields.Entries), faceDicts);
				print.CopyImgPaths(ImageEntry.GetEntryList(images.Entries));
				TrackerForm.Catalog.Printings.Add(print);

				//Autofill next fields
				printref = null;
				string cn = fields.Entries.FirstOrDefault(fe => fe.Field.Equals("cn"))?.Value;
				if (!string.IsNullOrWhiteSpace(cn)) {
					string prefix = "";
					int width = 3;
					while (cn.Length > 0 && !char.IsNumber(cn[0])) {
						prefix += cn[0];
						cn = cn.Substring(1);
					}
					while (cn.Length > 0 && !char.IsNumber(cn[cn.Length - 1]))
						cn = cn.Substring(0, cn.Length - 1);
					width = cn.Length;
					while (cn.Length > 0 && cn[0] == '0')
						cn = cn.Substring(1);
					if (!string.IsNullOrWhiteSpace(cn) && int.TryParse(cn, out int value)) {
						++value;
						cn = prefix + value.ToString().PadLeft(width, '0');
						fields.Entries.FirstOrDefault(fe => fe.Field.Equals("cn"))?.SetValue(cn);
						images.ClearRows();
						fields.Entries.FirstOrDefault(fe => fe.Field.Equals("printid"))?.SetValue(prefix.ToLower() + set.Code.ToLower() + '/' + value.ToString());
						if (Utils.ResourcePaths.ContainsKey(TrackerForm.Catalog.Game)) {
							string path = Utils.ResourcePaths[TrackerForm.Catalog.Game] + set.Code + '/' + cn;
							if (Utils.ImageExistsAtPath(path, out path))
								images.AddRow(new ImageEntry(path));
							else {
								char suffix = 'a';
								while (Utils.ImageExistsAtPath(path + suffix, out string newPath)) {
									images.AddRow(new ImageEntry(newPath));
									++suffix;
								}
							}
						}
					}
				}
				foreach (FieldEntry entry in fields.Entries) {
					if (!Utils.KeepableFields.Contains(entry.Field))
						entry.SetValue();
					entry.OnFieldChanged();
				}
				while (faces.Count > 0)
					faces[0].ClearRows();
				if (card.TryGetField("name", out string name) && !name.Equals("_"))
					cardBox.SelectedIndex = -1;

			}

		}

		//Return
		private void ReturnToDetails(object sender = null, EventArgs e = null) {
			if (printref != null)
				TrackerForm.Instance.SetPage<Detailpage>(printref: printref);
		}

	}

}
