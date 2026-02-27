using System;
using System.Drawing;
using System.Windows.Forms;

namespace CollectionTracker {

	public class Printentry : TrackerPage {

		//Properties
		private Printing printref;
		private Fieldlist fields;
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

			//Fields
			fields = new Fieldlist(this, "Fields", print.Fields, 0);
			listPanel.Controls.Add(fields.Panel);
			int yPos = fields.Panel.Height + 5;

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
