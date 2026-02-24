using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	//Homepage
	public class Setlist : TrackerPage {

		//Constructor
		public Setlist(TrackerForm form) : base(form) {

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
			for (int i = 0; i < Catalog.Sets.Count; ++i) {

				//Important values
				Set set = Catalog.Sets[i];
				List<Printing> setPrints = Catalog.Printings.Where(print => print.Set == set).ToList();
				int setCount = setPrints.Count;
				int setOwned = setPrints.Count(print => print.IsOwned);
				bool missingCardref = setPrints.Count(print => !print.TryGetField("name", out string value) || value.Equals("_")) > 0;

				//Set info box
				Panel setPanel = Utils.GeneratePanel(new Rectangle(5, 5 + (i * 65), 820, 60));

				//Filter button
				Button filter = Utils.GenerateButton(new Rectangle(5, 5, 350, 50), set.Name, set.ImgPath);
				//filter.Click += new EventHandler((sender, e) => FilterBySet(set));
				filter.MouseUp += new MouseEventHandler((sender, e) => {
					if (e.Button == MouseButtons.Right) {
						if (Catalog.Game == Game.MTG)
							Process.Start("https://scryfall.com/sets/" + set.Code);
						else if (Catalog.Game == Game.YGO)
							Process.Start("https://yugipedia.com/wiki/" + set.Code);
						//else if (Catalog.Game == Game.PKMN)
						//	Process.Start("https://yugipedia.com/wiki/" + set.Code);
					}
				});
				setPanel.Controls.Add(filter);

				//Progress label
				Label progressLabel = Utils.GenerateLabel(new Rectangle(360, 20, 100, TEXT_HEIGHT), $"{setOwned}/{setCount}");
				progressLabel.TextAlign = ContentAlignment.MiddleCenter;
				setPanel.Controls.Add(progressLabel);

				//Progress bar
				ProgressBar progressBar = Utils.GenerateProgressBar(new Rectangle(470, 15, 265, 30), setOwned > 0 ? (int)(((float)setOwned / setCount) * 100) : 0);
				setPanel.Controls.Add(progressBar);

				//Missing cardref label
				if (missingCardref) {
					Label cardrefLabel = Utils.GenerateLabel(new Rectangle(780, 20, 35, TEXT_HEIGHT), "*");
					cardrefLabel.TextAlign = ContentAlignment.MiddleCenter;
					setPanel.Controls.Add(cardrefLabel);
				}

				//No cards logged but folder exists
				else if (setPrints.Count == 0 && Directory.Exists(Catalog.ResourcePath + set.Code)) {
					Label cardrefLabel = Utils.GenerateLabel(new Rectangle(780, 20, 35, TEXT_HEIGHT), "&");
					cardrefLabel.TextAlign = ContentAlignment.MiddleCenter;
					setPanel.Controls.Add(cardrefLabel);
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

		//Filter sets by text
		//private void OnFilterChanged(object sender, EventArgs e) => YGO_UpdateSets();

		//Filter catalog by set ID
		private void FilterBySet(Set set) => parent.SetPage(new Printlist(parent, $"s={set.Code}"));

	}

}
