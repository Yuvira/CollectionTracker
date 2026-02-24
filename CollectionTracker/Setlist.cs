using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	//Setlist
	public class Setlist : TrackerPage {

		#region Set Row Class

		//Set row
		private class Setrow {

			//Properties
			private Setlist parent;
			private Set set;
			private string setURL;
			private Panel panel;
			private Button filter;
			private Label progressLabel;
			private ProgressBar progressBar;
			private Label cardRefLabel;

			//Accessors
			public Panel Panel => panel;

			//Generator
			public Setrow(Setlist parent, Catalog catalog, int idx) {

				//Initial values
				this.parent = parent;
				set = catalog.Sets[idx];
				if (Utils.SetURLs.ContainsKey(catalog.Game))
					setURL = Utils.SetURLs[catalog.Game] + set.Code;
				List<Printing> setPrints = catalog.Printings.Where(print => print.Set == set).ToList();
				int setCount = setPrints.Count;
				int setOwned = setPrints.Count(print => print.IsOwned);
				bool missingCardref = setPrints.Count(print => !print.TryGetField("name", out string value) || value.Equals("_")) > 0;

				//Set info box
				panel = Utils.GeneratePanel(new Rectangle(5, 5 + (idx * 65), 820, 60));

				//Filter button
				filter = Utils.GenerateButton(new Rectangle(5, 5, 350, 50), set.Name, set.ImgPath);
				filter.Click += FilterBySet;
				if (!string.IsNullOrWhiteSpace(setURL))
					filter.MouseUp += LoadSetURL;
				panel.Controls.Add(filter);

				//Progress label
				progressLabel = Utils.GenerateLabel(new Rectangle(360, 20, 100, TEXT_HEIGHT), $"{setOwned}/{setCount}");
				progressLabel.TextAlign = ContentAlignment.MiddleCenter;
				panel.Controls.Add(progressLabel);

				//Progress bar
				progressBar = Utils.GenerateProgressBar(new Rectangle(470, 15, 265, 30), setOwned > 0 ? (int)(((float)setOwned / setCount) * 100) : 0);
				panel.Controls.Add(progressBar);

				//Missing cardref label
				if (missingCardref) {
					cardRefLabel = Utils.GenerateLabel(new Rectangle(780, 20, 35, TEXT_HEIGHT), "*");
					cardRefLabel.TextAlign = ContentAlignment.MiddleCenter;
					panel.Controls.Add(cardRefLabel);
				}

				//No cards logged but folder exists
				else if (setPrints.Count == 0 && Utils.ResourcePaths.ContainsKey(catalog.Game) && Directory.Exists(Utils.ResourcePaths[catalog.Game] + set.Code)) {
					cardRefLabel = Utils.GenerateLabel(new Rectangle(780, 20, 35, TEXT_HEIGHT), "&");
					cardRefLabel.TextAlign = ContentAlignment.MiddleCenter;
					panel.Controls.Add(cardRefLabel);
				}
			}

			//Filter
			private void FilterBySet(object sender, EventArgs e) => parent.FilterBySet(set);
			private void LoadSetURL(object sender, MouseEventArgs e) {
				if (e.Button == MouseButtons.Right)
					Process.Start(setURL);
			}

		}

		#endregion

		//Properties
		private List<Setrow> rows;

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

			//Loop sets
			listPanel.SuspendLayout();
			rows = new List<Setrow>();
			for (int i = 0; i < Catalog.Sets.Count; ++i) {
				rows.Add(new Setrow(this, Catalog, i));
				listPanel.Controls.Add(rows[rows.Count - 1].Panel);
			}
			listPanel.ResumeLayout();

			//Add to panel
			panel.Controls.Add(headerLabel);
			panel.Controls.Add(listPanel);

		}

		//Filter catalog by set ID
		private void FilterBySet(Set set) => parent.SetPage(new Printlist(parent, $"s={set.Code}"));

	}

}
