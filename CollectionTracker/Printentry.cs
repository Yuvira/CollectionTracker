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
		private Entrylist<ImageEntry> images;

		//Controls
		private TrackerPanel listPanel;
		private ComboBox setBox;
		private ComboBox cardBox;
		private Button saveButton;
		private Button returnButton;

		//Static modified field identifiers
		public static bool SetsAltered = true;
		public static bool CardsAltered = true;

		//Constructor
		public Printentry(TrackerForm form, Printing print = null) : base(form) {

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

			//Save button
			saveButton = Utils.GenerateButton(new Rectangle(0, 70, 100, BUTTON_HEIGHT), "Save");
			saveButton.Click += SavePrinting;
			listPanel.Controls.Add(saveButton);

			//Return button
			returnButton = Utils.GenerateButton(new Rectangle(105, 70, 100, BUTTON_HEIGHT), "Return");
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
			treatments = new Entrylist<TreatmentEntry>(this, "Treatments", printref == null ? new List<TreatmentEntry>() : TreatmentEntry.GenerateEntries(printref.Treatments.Select(t => t.Name).ToList()));
			treatments.OnListResize += OnFieldsResized;
			listPanel.Controls.Add(treatments.Panel);

			//Fields
			if (fields != null) {
				listPanel.Controls.Remove(fields.Panel);
				fields.Panel.Dispose();
			}
			fields = new Entrylist<FieldEntry>(this, "Fields", printref == null ? new List<FieldEntry>() : FieldEntry.GenerateEntries(printref.Fields));
			fields.OnListResize += OnFieldsResized;
			listPanel.Controls.Add(fields.Panel);

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

		//On resize
		private void OnFieldsResized() {
			Point pos = new Point(0, 70);
			treatments.Panel.Location = pos;
			pos.Y += treatments.Panel.Height + 5;
			fields.Panel.Location = pos;
			pos.Y += fields.Panel.Height + 5;
			images.Panel.Location = pos;
			pos.Y += images.Panel.Height + 5;
			saveButton.Location = pos;
			returnButton.Location = new Point(105, pos.Y);
		}

		//Save
		private void SavePrinting(object sender, EventArgs e) {

			//Copy to existing print ref
			if (printref != null) {
				if (setBox.SelectedItem != null && setBox.SelectedItem is Set set)
					printref.CopySet(set);
				if (cardBox.SelectedItem != null && cardBox.SelectedItem is Card card)
					printref.CopyCard(card);
				printref.CopyTreatments(TreatmentEntry.GetEntryList(treatments.Entries));
				printref.CopyFields(FieldEntry.GetEntryDict(fields.Entries));
				printref.CopyImgPaths(ImageEntry.GetEntryList(images.Entries));
				ReturnToDetails();
			}

			//Generate new printing
			else {
				Printing print = new Printing();
				if (setBox.SelectedItem != null && setBox.SelectedItem is Set set)
					print.CopySet(set);
				if (cardBox.SelectedItem != null && cardBox.SelectedItem is Card card)
					print.CopyCard(card);
				print.CopyTreatments(TreatmentEntry.GetEntryList(treatments.Entries));
				print.CopyFields(FieldEntry.GetEntryDict(fields.Entries));
				print.CopyImgPaths(ImageEntry.GetEntryList(images.Entries));
				TrackerForm.Catalog.Printings.Add(print);
				LoadPrinting(null);
			}

		}

		//Return
		private void ReturnToDetails(object sender = null, EventArgs e = null) {
			if (printref != null)
				parent.SetPage(new Detailpage(parent, printref));
		}

	}

}
