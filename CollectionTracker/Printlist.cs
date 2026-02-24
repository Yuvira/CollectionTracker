using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CollectionTracker {

	//Print list
	public class Printlist : TrackerPage {

		//Constructor
		public Printlist(TrackerForm form, string searchTerms) : base(form) {

			//Get print list
			List<Printing> prints = SearchUtils.SearchPrintings(Catalog, searchTerms);

			//Header
			string str = $"Setlist: {Catalog.Sets.Count} | {Catalog.Cards.Count} | {Catalog.Printings.Count} | {Catalog.Symbols.Count}";
			int textWidth = Utils.MeasureWidth(str, Utils.FONT_DEFAULT);
			Label headerLabel = Utils.GenerateLabel(new Rectangle((panel.Width - 850) / 2, 5, textWidth, TEXT_HEIGHT), str);
			headerLabel.Anchor = AnchorStyles.Top;

			//Panel
			Panel listPanel = Utils.GeneratePanel(Utils.CenterRect(new Size(850, panel.Height - 40), panel.Size, new Point(0, -15)));
			listPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			listPanel.AutoScroll = true;

			//Suspend
			listPanel.SuspendLayout();

			//Loop sets
			for (int i = 0; i < prints.Count; ++i) {

				//Important values
				Printing print = prints[i];

				//Set info box
				Panel setPanel = Utils.GeneratePanel(new Rectangle(5, 5 + (i * 65), 820, 60));

				//Progress label
				if (print.TryGetField("name", out string name)) {
					Label nameLabel = Utils.GenerateLabel(new Rectangle(5, 5, 810, 50), name);
					nameLabel.TextAlign = ContentAlignment.MiddleCenter;
					setPanel.Controls.Add(nameLabel);
				}

				//Add to list
				listPanel.Controls.Add(setPanel);

			}

			//Resume
			listPanel.ResumeLayout();

			//Add to panel
			panel.Controls.Add(headerLabel);
			panel.Controls.Add(listPanel);

		}

	}

}
