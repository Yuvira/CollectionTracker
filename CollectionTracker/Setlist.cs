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

			//Controls
			private Panel panel;
			private Button filter;
			private Label progressLabel;
			private ProgressBar progressBar;
			private Label cardRefLabel = null;

			//Accessors
			public Panel Panel => panel;

			//Generator
			public Setrow(Setlist parent, Set set) {

				//Initial values
				this.parent = parent;
				this.set = set;
				if (Utils.SetURLs.ContainsKey(TrackerForm.Catalog.Game))
					setURL = Utils.SetURLs[TrackerForm.Catalog.Game] + set.Code;
				List<Printing> setPrints = TrackerForm.Catalog.Printings.Where(print => print.Set == set).ToList();
				int setCount = setPrints.Count;
				int setOwned = setPrints.Count(print => print.IsOwned);
				bool missingCardref = setPrints.Count(print => !print.TryGetField("name", out string value) || value.Equals("_")) > 0;

				//Panel
				panel = Utils.GeneratePanel(new Rectangle(5, 5, 820, 60));

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
				else if (setPrints.Count == 0 && Utils.ResourcePaths.ContainsKey(TrackerForm.Catalog.Game) && Directory.Exists(Utils.ResourcePaths[TrackerForm.Catalog.Game] + set.Code)) {
					cardRefLabel = Utils.GenerateLabel(new Rectangle(780, 20, 35, TEXT_HEIGHT), "&");
					cardRefLabel.TextAlign = ContentAlignment.MiddleCenter;
					panel.Controls.Add(cardRefLabel);
				}

			}

			//Filter
			private void FilterBySet(object sender, EventArgs e) => parent.FilterBySet(set);

			//Load set URL
			private void LoadSetURL(object sender, MouseEventArgs e) {
				if (e.Button == MouseButtons.Right)
					Process.Start(setURL);
			}

		}

		#endregion

		//Properties
		private List<Setrow> rows;
		private Label headerLabel;
		private Panel listPanel;
		private int lastWidth;

		//Constructor
		public Setlist(TrackerForm form) : base(form) {

			//Width
			lastWidth = panel.Width;

			//Sets
			List<Set> sets = new List<Set>(TrackerForm.Catalog.Sets);
			sets.Sort(Set.SortNewest);

			//Header
			string str = $"Setlist: {TrackerForm.Catalog.Sets.Count} | {TrackerForm.Catalog.Cards.Count} | {TrackerForm.Catalog.Printings.Count} | {TrackerForm.Catalog.Symbols.Count}";
			headerLabel = Utils.GenerateLabel(new Rectangle(Math.Max((panel.Width - 850) / 2, 5), 5, Utils.MeasureWidth(str), TEXT_HEIGHT), str);
			headerLabel.Anchor = AnchorStyles.Top;

			//Panel
			listPanel = Utils.GeneratePanel(Utils.CenterRect(new Size(Math.Min(panel.Width - 10, 850), panel.Height - 40), panel.Size, new Point(0, -15)));
			listPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			listPanel.AutoScroll = true;

			//Loop sets
			Point pos = new Point(5, 5);
			rows = new List<Setrow>();
			listPanel.SuspendLayout();
			foreach (Set set in sets) {
				Setrow row = new Setrow(this, set);
				row.Panel.Location = pos;
				pos.Y += row.Panel.Height + 5;
				rows.Add(row);
				listPanel.Controls.Add(row.Panel);
			}
			listPanel.ResumeLayout();

			//Add to panel
			panel.Controls.Add(headerLabel);
			panel.Controls.Add(listPanel);

		}

		//Filter catalog by set ID
		private void FilterBySet(Set set) => parent.SetPage(new Printlist(parent, $"s={set.Code}"));

		//Resize event
		protected override void OnFormResize(object sender, EventArgs e) {
			base.OnFormResize(sender, e);
			if (panel.Width >= 860 && lastWidth >= 860)
				return;
			headerLabel.Location = new Point(Math.Max((panel.Width - 850) / 2, 5), 5);
			Rectangle rect = Utils.CenterRect(new Size(Math.Min(panel.Width - 10, 850), panel.Height - 40), panel.Size, new Point(0, -15));
			listPanel.Location = rect.Location;
			listPanel.Size = rect.Size;
			lastWidth = panel.Width;
		}

	}

}
