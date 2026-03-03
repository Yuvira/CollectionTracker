using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	public class Printentry : TrackerPage {

		//Properties
		private Printing printref;
		private Treatmentlist treatments;
		private Fieldlist fields;
		private Imagelist images;

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
		public Printentry(TrackerForm form, Printing print) : base(form) {

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
		public void LoadPrinting(Printing print) {

			//References
			printref = print;

			//Suspend
			listPanel.SuspendLayout();

			//Sets
			if (SetsAltered) {
				setBox.SuspendLayout();
				setBox.Items.Clear();
				setBox.Items.AddRange(Catalog.Sets.ToArray());
				setBox.ResumeLayout();
				SetsAltered = false;
			}
			setBox.SelectedItem = printref.Set;

			//Cards
			if (CardsAltered) {
				cardBox.SuspendLayout();
				cardBox.Items.Clear();
				cardBox.Items.AddRange(Catalog.Cards.ToArray());
				cardBox.ResumeLayout();
				CardsAltered = false;
			}
			cardBox.SelectedItem = printref.Card;

			//Treatments
			if (treatments != null) {
				listPanel.Controls.Remove(treatments.Panel);
				treatments.Panel.Dispose();
			}
			treatments = new Treatmentlist(this, "Treatments", printref.Treatments.Select(t => t.Name).ToList(), 70);
			treatments.OnListResize += OnFieldsResized;
			listPanel.Controls.Add(treatments.Panel);

			//Fields
			if (fields != null) {
				listPanel.Controls.Remove(fields.Panel);
				fields.Panel.Dispose();
			}
			fields = new Fieldlist(this, "Fields", printref.Fields, 70);
			fields.OnListResize += OnFieldsResized;
			listPanel.Controls.Add(fields.Panel);

			//Images
			if (images != null) {
				listPanel.Controls.Remove(images.Panel);
				images.Panel.Dispose();
			}
			images = new Imagelist(this, "Images", printref.ImagePaths, 70);
			images.OnListResize += OnFieldsResized;
			listPanel.Controls.Add(images.Panel);

			//Resize
			OnFieldsResized();

			//Resume
			listPanel.ResumeLayout();

		}

		//On resize
		private void OnFieldsResized() {
			int yPos = 70;
			yPos += treatments.Panel.Height + 5;
			fields.Panel.Location = new Point(0, yPos);
			yPos += fields.Panel.Height + 5;
			images.Panel.Location = new Point(0, yPos);
			yPos += images.Panel.Height + 5;
			saveButton.Location = new Point(0, yPos);
			returnButton.Location = new Point(105, yPos);
		}

		//Save
		private void SavePrinting(object sender, EventArgs e) {
			if (printref != null) {
				if (setBox.SelectedItem != null && setBox.SelectedItem is Set set)
					printref.CopySet(set);
				if (cardBox.SelectedItem != null && cardBox.SelectedItem is Card card)
					printref.CopyCard(card);
				printref.CopyTreatments(treatments.GetTreatmentList());
				printref.CopyFields(fields.GetFieldDict());
				printref.CopyImgPaths(images.GetPathList());
				parent.SetPage(new Detailpage(parent, printref));
			}
		}

		//Return
		private void ReturnToDetails(object sender, EventArgs e) {
			if (printref != null)
				parent.SetPage(new Detailpage(parent, printref));
		}

	}

}
