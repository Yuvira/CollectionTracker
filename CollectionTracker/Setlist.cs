using System;
using System.Drawing;
using System.Windows.Forms;

namespace CollectionTracker {

	//Homepage
	public class Setlist : TrackerPage {

		//Constructor
		public Setlist(TrackerForm form) : base(form) { }

		//Initializer
		public override void Init() {

			//Catalog selectors
			string str = $"Setlist: {Catalog.Sets.Count} | {Catalog.Cards.Count} | {Catalog.Printings.Count} | {Catalog.Symbols.Count}";
			int textWidth = Utils.MeasureWidth(str, Utils.FONT_DEFAULT);
			Label label = Utils.GenerateLabel(new Rectangle((panel.Width - 850) / 2, 5, textWidth, Utils.TEXT_HEIGHT), str);
			label.Anchor = AnchorStyles.Top;

			//Panel
			Panel listPanel = Utils.GeneratePanel(Utils.CenterRect(new Size(850, panel.Height - 40), panel.Size, new Point(0, -15)));
			listPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			listPanel.AutoScroll = true;

			//Loop sets


			//Add to panel
			panel.Controls.Add(label);
			panel.Controls.Add(listPanel);

		}

	}

}
