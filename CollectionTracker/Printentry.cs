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

		//Constructor
		public Printentry(TrackerForm form, Printing print) : base(form) {

			//References
			printref = print;

			//Panel
			listPanel = Utils.GenerateTrackerPanel(Utils.CenterRect(new Size(800, panel.Height - 25), panel.Size, new Point(0, 0)));
			listPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			listPanel.AutoScroll = true;

			//Suspend
			listPanel.SuspendLayout();

			//Set
			Label setLabel = Utils.GenerateLabel(new Rectangle(0, 0, 150, 30), "Set");
			setBox = Utils.GenerateComboBox(new Rectangle(155, 0, 625, 30), ComboBoxStyle.DropDownList, true);
			setBox.Items.AddRange(Catalog.Sets.ToArray());
			setBox.SelectedItem = printref.Set;
			listPanel.Controls.Add(setLabel);
			listPanel.Controls.Add(setBox);

			//Card
			Label cardLabel = Utils.GenerateLabel(new Rectangle(0, 35, 150, 30), "Card");
			cardBox = Utils.GenerateComboBox(new Rectangle(155, 35, 625, 30), ComboBoxStyle.DropDownList, true);
			cardBox.Items.AddRange(Catalog.Cards.ToArray());
			cardBox.SelectedItem = printref.Card;
			listPanel.Controls.Add(cardLabel);
			listPanel.Controls.Add(cardBox);

			//Treatments
			treatments = new Treatmentlist(this, "Treatments", printref.Treatments.Select(t => t.Name).ToList(), 70);
			treatments.OnListResize += OnFieldsResized;
			listPanel.Controls.Add(treatments.Panel);

			//Fields
			fields = new Fieldlist(this, "Fields", print.Fields, 70);
			fields.OnListResize += OnFieldsResized;
			listPanel.Controls.Add(fields.Panel);

			//Images
			images = new Imagelist(this, "Images", print.ImagePaths, 70);
			images.OnListResize += OnFieldsResized;
			listPanel.Controls.Add(images.Panel);

			//Save button
			saveButton = Utils.GenerateButton(new Rectangle(0, 70, 100, BUTTON_HEIGHT), "Save");
			saveButton.Click += SavePrinting;
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
			int yPos = 70;
			yPos += treatments.Panel.Height + 5;
			fields.Panel.Location = new Point(0, yPos);
			yPos += fields.Panel.Height + 5;
			images.Panel.Location = new Point(0, yPos);
			yPos += images.Panel.Height + 5;
			saveButton.Location = new Point(0, yPos);
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

	}

}
