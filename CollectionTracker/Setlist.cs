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
			private Label warningLabel = null;

			//Accessors
			public Panel Panel => panel;
			public Set Set => set;

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
				bool missingCardref = setPrints.Count(print => MissingCardRef(print)) > 0;
				bool missingPrintData = TrackerForm.Catalog.Game != Game.YGO && setPrints.Count(print => MissingPrintData(print)) > 0;
				bool noCardsWithFolder = setPrints.Count == 0 && Utils.ResourcePaths.ContainsKey(TrackerForm.Catalog.Game) && Directory.Exists(Utils.ResourcePaths[TrackerForm.Catalog.Game] + "sets/" + set.Code);

				//Panel
				panel = Utils.GeneratePanel(new Rectangle(5, 5, 820, 60));

				//Filter button
				filter = Utils.GenerateButton(new Rectangle(5, 5, 350, 50), set.Name, set.ImgPath);
				filter.Click += FilterBySet;
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
				if (missingCardref || missingPrintData || noCardsWithFolder) {
					warningLabel = Utils.GenerateLabel(new Rectangle(780, 20, 35, TEXT_HEIGHT), "");
					if (missingCardref)
						warningLabel.Text = "*";
					else if (missingPrintData)
						warningLabel.Text = "!";
					else if (noCardsWithFolder)
						warningLabel.Text = "&";
					warningLabel.TextAlign = ContentAlignment.MiddleCenter;
					panel.Controls.Add(warningLabel);
				}

			}

			//Filter
			private void FilterBySet(object sender, EventArgs e) => parent.FilterBySet(set);

			//Load set URL
			private void LoadSetURL(object sender, MouseEventArgs e) {
				if (e.Button == MouseButtons.Right && !string.IsNullOrWhiteSpace(setURL))
					Process.Start(setURL);
				else if (e.Button == MouseButtons.Middle) {
					List<Printing> printlist = TrackerForm.Catalog.Printings.Where(print => print.Set == set && (MissingCardRef(print) || (TrackerForm.Catalog.Game != Game.YGO && MissingPrintData(print)))).ToList();
					if (printlist.Count > 0)
						parent.FilterByList(printlist);
				}
			}

			//Check if print is missing data
			private bool MissingCardRef(Printing print) => !print.TryGetField("name", out string name) || name.Equals("_");
			private bool MissingPrintData(Printing print) => !print.HasField("rarity") && !print.HasField("artist") && print.TryGetField("name", out string name) && !name.Equals("Punchcard") && !print.GetField("type").Equals("Basic Energy") && !print.GetField("type").Equals("Minigame");

		}

		#endregion

		//Constants
		private const int MAX_WIDTH = 860;
		private const int FILTER_WIDTH = 200;

		//Properties
		private List<Setrow> rows;
		private Label headerLabel;
		private Label filterLabel;
		private TextBox filterBox;
		private Panel listPanel;

		//Constructor
		public Setlist() : base() {

			//Sets
			List<Set> sets = new List<Set>(TrackerForm.Catalog.Sets);
			sets.Sort(Set.SortNewest);

			//Header
			headerLabel = Utils.GenerateAutoSizeLabel(new Point(0, PANEL_MARGIN + 5), $"Sets: {TrackerForm.Catalog.Sets.Count} | Cards: {TrackerForm.Catalog.Cards.Count} | Prints: {TrackerForm.Catalog.Printings.Count} | Symbols: {TrackerForm.Catalog.Symbols.Count}");
			headerLabel.Anchor = AnchorStyles.Top;

			//Filter box
			filterLabel = Utils.GenerateAutoSizeLabel(new Point(0, PANEL_MARGIN + 5), "Filter");
			filterLabel.Anchor = AnchorStyles.Top;
			filterBox = Utils.GenerateTextBox(new Rectangle(0, PANEL_MARGIN, FILTER_WIDTH, BUTTON_HEIGHT), "");
			filterBox.TextChanged += OnFilterChanged;
			filterBox.Anchor = AnchorStyles.Top;

			//Panel
			int yPos = BUTTON_HEIGHT + (PANEL_MARGIN * 2);
			listPanel = Utils.GeneratePanel(new Rectangle(0, yPos, 0, panel.Height - (yPos + PANEL_MARGIN)));
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
			panel.Controls.Add(filterLabel);
			panel.Controls.Add(filterBox);
			panel.Controls.Add(listPanel);

			//Size and position
			oldWidth = 0;
			OnFormResizeEnd();

		}

		//Filter catalog by set ID
		private void FilterBySet(Set set) => TrackerForm.Instance.SetPage<Printlist>(searchTerms: $"s={set.Code}");
		private void FilterByList(List<Printing> printlist) => TrackerForm.Instance.SetPage<Printlist>(printlist: printlist);

		//Set filter changed
		private void OnFilterChanged(object sender, EventArgs e) {
			int top = PANEL_MARGIN;
			listPanel.SuspendLayout();
			foreach (Setrow row in rows) {
				row.Panel.Visible = row.Set.Name.ToLower().Contains(filterBox.Text.ToLower());
				if (row.Panel.Visible) {
					row.Panel.Top = top;
					top += row.Panel.Height + PANEL_MARGIN;
				}
			}
			listPanel.ResumeLayout();
		}

		//Resize event
		protected override void OnFormResizeEnd(object sender = null, EventArgs e = null) {
			if (panel.Width >= MAX_WIDTH && oldWidth >= MAX_WIDTH)
				return;
			listPanel.Width = Math.Min(panel.Width - (PANEL_MARGIN * 2), MAX_WIDTH - (PANEL_MARGIN * 2));
			listPanel.Left = Math.Max((panel.Width - listPanel.Width) / 2, PANEL_MARGIN);
			headerLabel.Left = listPanel.Left;
			filterLabel.Left = listPanel.Right - (filterLabel.Width + filterBox.Width + PANEL_MARGIN);
			filterBox.Left = listPanel.Right - filterBox.Width;
			oldWidth = panel.Width;
		}

	}

}
