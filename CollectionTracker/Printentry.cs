using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	public class Printentry : TrackerPage {

		//Properties
		private Printing printref;
		private ComboBox setBox;
		private ComboBox cardBox;
		private Treatmentlist treatments;
		private Fieldlist fields;
		private Imagelist images;
		private TrackerPanel listPanel;

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

			//Y Position
			int yPos = 70;

			//Treatments
			treatments = new Treatmentlist(this, "Treatments", printref.Treatments.Select(t => t.Name).ToList(), yPos);
			listPanel.Controls.Add(treatments.Panel);
			yPos += treatments.Panel.Height + 5;

			//Fields
			fields = new Fieldlist(this, "Fields", print.Fields, yPos);
			listPanel.Controls.Add(fields.Panel);
			yPos += fields.Panel.Height + 5;

			//Images
			images = new Imagelist(this, "Images", print.ImagePaths, yPos);
			listPanel.Controls.Add(images.Panel);
			yPos += images.Panel.Height + 5;

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
			if (printref != null) {
				printref.CopyFields(fields.GetFieldDict());
				parent.SetPage(new Detailpage(parent, printref));
			}
		}

	}

}
