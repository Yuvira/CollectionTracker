using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	//Print list
	public class Printlist : TrackerPage {

		#region Print Entry Class

		//Print entry
		private class Printentry {

			#region Treatment Row Class

			//Treatment row
			private class Treatmentrow {

				//Properties
				private Printentry parent;
				private Treatment treatment;

				//Controls
				private TrackerPanel panel;
				private Label nameLabel;
				private Label countLabel;
				private Button decrementButton;
				private Button incrementButton;

				//Accessors
				public Panel Panel => panel;

				//Generator
				public Treatmentrow(Printentry parent, Treatment treatment, int yPos) {

					//Initial values
					this.parent = parent;
					this.treatment = treatment;

					//Panel
					panel = Utils.GenerateTrackerPanel(new Rectangle(PANEL_MARGIN, yPos, ENTRY_WIDTH - (PANEL_MARGIN * 2), BUTTON_HEIGHT));

					//Buttons
					decrementButton = Utils.GenerateButton(new Rectangle(0, 0, BUTTON_HEIGHT, BUTTON_HEIGHT), "<");
					decrementButton.Click += DecrementCount;
					incrementButton = Utils.GenerateButton(new Rectangle(panel.Width - BUTTON_HEIGHT, 0, BUTTON_HEIGHT, BUTTON_HEIGHT), ">");
					incrementButton.Click += IncrementCount;

					//Labels
					int width = (incrementButton.Left - decrementButton.Right) - (PANEL_MARGIN * 3);
					nameLabel = Utils.GenerateLabel(new Rectangle(decrementButton.Right + PANEL_MARGIN, 5, (int)(width * 0.7f), TEXT_HEIGHT), treatment.Name);
					nameLabel.TextAlign = ContentAlignment.MiddleRight;
					countLabel = Utils.GenerateLabel(new Rectangle(nameLabel.Right + PANEL_MARGIN, 5, (int)(width * 0.3f), TEXT_HEIGHT), treatment.OwnedCount.ToString());

					//Add controls
					panel.Controls.Add(decrementButton);
					panel.Controls.Add(incrementButton);
					panel.Controls.Add(nameLabel);
					panel.Controls.Add(countLabel);

				}

				//Update reference
				public void UpdateTreatment(Treatment treatment) {
					this.treatment = treatment;
					nameLabel.Text = treatment.Name;
					countLabel.Text = treatment.OwnedCount.ToString();
				}

				//Count modifiers
				private void IncrementCount(object sender, EventArgs e) {
					treatment.Increment();
					countLabel.Text = treatment.OwnedCount.ToString();
					parent.UpdateColor();
				}
				private void DecrementCount(object sender, EventArgs e) {
					if (treatment.Decrement())
						countLabel.Text = treatment.OwnedCount.ToString();
					parent.UpdateColor();
				}

			}

			#endregion

			//Properties
			private Printlist parent;
			private Printing print;
			private List<Treatmentrow> rows;

			//Controls
			private Panel panel;
			private PictureBox imgBox;

			//Accessors
			public Panel Panel => panel;

			//Generator
			public Printentry(Printlist parent) {

				//Initial values
				this.parent = parent;
				print = null;

				//Panel
				panel = Utils.GeneratePanel(new Rectangle(0, 0, ENTRY_WIDTH, ENTRY_HEIGHT));

				//Image box
				imgBox = Utils.GeneratePictureBox(new Rectangle(0, 0, ENTRY_WIDTH, ENTRY_HEIGHT));
				imgBox.MouseUp += OnClick;
				panel.Controls.Add(imgBox);

				//Initialize rows
				rows = new List<Treatmentrow>();

			}

			//Set print reference
			public int SetPrinting(Printing printing) {

				//Set print
				print = printing;

				//Resize panel
				panel.Height = ENTRY_HEIGHT + ((PANEL_MARGIN + BUTTON_HEIGHT) * print.Treatments.Count) + PANEL_MARGIN;

				//Load image
				if (print.ImagePaths.Count > 0)
					Utils.TryLoadCardImage(imgBox, print.ImagePaths[0], TrackerForm.Catalog.Game);

				//Add treatments
				panel.SuspendLayout();
				while (rows.Count > print.Treatments.Count) {
					panel.Controls.Remove(rows[rows.Count - 1].Panel);
					rows[rows.Count - 1].Panel.Dispose();
					rows.RemoveAt(rows.Count - 1);
				}
				for (int i = 0; i < print.Treatments.Count; ++i) {
					Treatment treatment = print.Treatments[i];
					if (i < rows.Count)
						rows[i].UpdateTreatment(treatment);
					else {
						Treatmentrow row = new Treatmentrow(this, treatment, ENTRY_HEIGHT + PANEL_MARGIN + ((PANEL_MARGIN + BUTTON_HEIGHT) * i));
						rows.Add(row);
						panel.Controls.Add(row.Panel);
					}
				}
				panel.ResumeLayout();

				//Color
				UpdateColor();

				//Return height
				return panel.Height;

			}

			//Update panel color
			public void UpdateColor() {
				if (print == null || print.Card == null) {
					panel.BackColor = Color.White;
					return;
				}
				if (print.Card.Favorite) {
					if (print.IsOwned)
						panel.BackColor = Utils.THEME.CardOwnedFavorite;
					else
						panel.BackColor = Utils.THEME.CardUnownedFavorite;
				}
				else {
					if (print.IsOwned)
						panel.BackColor = Utils.THEME.CardOwned;
					else
						panel.BackColor = Utils.THEME.CardUnowned;
				}
			}

			//Handle click event
			private void OnClick(object sender, MouseEventArgs e) {
				if (e.Button == MouseButtons.Left)
					parent.ShowDetails(print);
				else if (e.Button == MouseButtons.Right) {
					print.Card.ToggleFavorite();
					UpdateColor();
				}
			}

		}

		#endregion

		//Constants
		private const int BUTTON_WIDTH = 120;
		private const int FILTER_WIDTH = 120;
		private const int ENTRY_WIDTH = 300;
		private const int ENTRY_HEIGHT = 420;
		private const int ENTRIES_PER_PAGE = 50;

		//Properties
		private int page = 0;
		private List<Printing> printings;
		private List<Printing> filteredPrints;
		private List<Printentry> entries;

		//Controls
		private Label headerLabel;
		private ComboBox sortBox;
		private ComboBox filterBox;
		private Label filterLabel;
		private Button prevPageButton;
		private Button nextPageButton;
		private Panel listPanel;

		//Accessors
		public List<Printing> FilteredPrints => filteredPrints;

		//Constructor
		public Printlist(string searchTerms) : this(SearchUtils.SearchPrintings(searchTerms)) { }
		public Printlist(List<Printing> printlist) : base() {

			//Get print list
			printings = new List<Printing>(printlist);
			filteredPrints = new List<Printing>(printings);
			filteredPrints.Sort(Printing.SortNewest);

			//Page buttons
			prevPageButton = Utils.GenerateButton(new Rectangle(0, PANEL_MARGIN, BUTTON_WIDTH, BUTTON_HEIGHT), "<");
			prevPageButton.Anchor = AnchorStyles.Top;
			prevPageButton.Click += PrevPage;
			nextPageButton = Utils.GenerateButton(new Rectangle(0, PANEL_MARGIN, BUTTON_WIDTH, BUTTON_HEIGHT), ">");
			nextPageButton.Anchor = AnchorStyles.Top;
			nextPageButton.Click += NextPage;

			//Header
			headerLabel = Utils.GenerateLabel(new Rectangle(0, PANEL_MARGIN + 5, 0, TEXT_HEIGHT), "");
			headerLabel.Anchor = AnchorStyles.Top;

			//Filter
			filterBox = Utils.GenerateComboBox(new Rectangle(0, PANEL_MARGIN, FILTER_WIDTH, BUTTON_HEIGHT), ComboBoxStyle.DropDownList, false);
			filterBox.Anchor = AnchorStyles.Top;
			filterBox.Items.AddRange(new string[] { "None", "Newest", "Oldest" });
			filterBox.SelectedIndex = 0;
			filterBox.SelectedValueChanged += FilterChanged;
			int width = Utils.MeasureWidth("Filter");
			filterLabel = Utils.GenerateLabel(new Rectangle(0, PANEL_MARGIN + 5, width, TEXT_HEIGHT), "Filter");
			filterLabel.Anchor = AnchorStyles.Top;

			//Panel
			int yPos = BUTTON_HEIGHT + (PANEL_MARGIN * 2);
			listPanel = Utils.GeneratePanel(new Rectangle(0, yPos, 0, panel.Height - (yPos + PANEL_MARGIN)));
			listPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			listPanel.AutoScroll = true;

			//Generate entries
			listPanel.SuspendLayout();
			entries = new List<Printentry>();
			Printentry entry;
			for (int i = 0; i < ENTRIES_PER_PAGE; ++i) {
				entry = new Printentry(this);
				entry.Panel.Visible = false;
				entries.Add(entry);
				listPanel.Controls.Add(entry.Panel);
			}
			listPanel.ResumeLayout();

			//Add to panel
			panel.Controls.Add(prevPageButton);
			panel.Controls.Add(nextPageButton);
			panel.Controls.Add(filterLabel);
			panel.Controls.Add(headerLabel);
			panel.Controls.Add(filterBox);
			panel.Controls.Add(listPanel);

			//Update entries and resize
			UpdateEntries(false);
			OnFormResizeEnd();

		}

		//Filter changed
		private void FilterChanged(object sender, EventArgs e) {
			filteredPrints = new List<Printing>(printings);
			if (filterBox.SelectedItem.ToString().ToLower().Equals("newest")) {
				filteredPrints.Sort(Printing.SortNewest);
				filteredPrints = filteredPrints.GroupBy(p => p.Card).Select(g => g.First()).ToList();
			}
			else if (filterBox.SelectedItem.ToString().ToLower().Equals("oldest")) {
				filteredPrints.Sort(Printing.SortOldest);
				filteredPrints = filteredPrints.GroupBy(p => p.Card).Select(g => g.First()).ToList();
			}
			filteredPrints.Sort(Printing.SortNewest);
			UpdateEntries();
		}

		//Update entry contents
		public void UpdateEntries(bool doLayout = true) {

			//Paging
			int maxPage = 0;
			while (((maxPage + 1) * ENTRIES_PER_PAGE) < filteredPrints.Count)
				++maxPage;
			if (page > maxPage)
				page = 0;
			else if (page < 0)
				page = maxPage;
			int startIdx = page * ENTRIES_PER_PAGE;

			//Header
			string str = $"Showing {startIdx + 1} - {Math.Min((page + 1) * ENTRIES_PER_PAGE, filteredPrints.Count)} of {filteredPrints.Count}";
			headerLabel.Width = Utils.MeasureWidth(str);
			headerLabel.Text = str;

			//Button visibility
			prevPageButton.Visible = filteredPrints.Count > ENTRIES_PER_PAGE;
			nextPageButton.Visible = filteredPrints.Count > ENTRIES_PER_PAGE;

			//Update contents
			for (int i = 0; i < ENTRIES_PER_PAGE; ++i) {
				if (startIdx + i < filteredPrints.Count) {
					Printing print = filteredPrints[startIdx + i];
					entries[i].SetPrinting(print);
					entries[i].Panel.Show();
				}
				else
					entries[i].Panel.Hide();
			}

			//Layout
			if (doLayout)
				LayoutEntries();

		}


		//Update entry positions
		private void LayoutEntries() {

			//Reset scroll
			listPanel.AutoScrollPosition = new Point(0, 0);

			//Determine max width
			int maxWidth = listPanel.Width - ((PANEL_MARGIN * 2) + SCROLL_MARGIN);
			int remainingWidth = maxWidth - ENTRY_WIDTH;
			int columns = 1;
			while (true) {
				if (remainingWidth - (ENTRY_WIDTH + PANEL_MARGIN) < 0)
					break;
				remainingWidth -= ENTRY_WIDTH + PANEL_MARGIN;
				++columns;
			}
			int left = PANEL_MARGIN + (remainingWidth / 2);

			//Top margin
			int top = PANEL_MARGIN;
			int rows = 0;
			while (rows * columns < ENTRIES_PER_PAGE)
				++rows;
			int rowHeight;

			//Loop entries
			listPanel.SuspendLayout();
			int idx;
			for (int y = 0; y < rows; ++y) {
				rowHeight = 0;
				for (int x = 0; x < columns; ++x) {
					idx = x + (y * columns);
					if (idx < entries.Count && entries[idx].Panel.Visible) {
						entries[idx].Panel.Left = left + ((ENTRY_WIDTH + PANEL_MARGIN) * x);
						entries[idx].Panel.Top = top;
						rowHeight = Math.Max(rowHeight, entries[idx].Panel.Height);
					}
				}
				top += rowHeight + PANEL_MARGIN;
			}
			listPanel.ResumeLayout();

		}

		//Paging
		private void PrevPage(object sender, EventArgs e) {
			--page;
			UpdateEntries();
		}
		private void NextPage(object sender, EventArgs e) {
			++page;
			UpdateEntries();
		}

		//Details
		public void ShowDetails(Printing print) => TrackerForm.Instance.SetPage<Detailpage>(printref: print);

		//Resize event
		protected override void OnFormResizeEnd(object sender = null, EventArgs e = null) {
			listPanel.Width = Math.Max(panel.Width - (PANEL_MARGIN * 2), ENTRY_WIDTH + (PANEL_MARGIN * 2) + SCROLL_MARGIN);
			listPanel.Left = PANEL_MARGIN;
			prevPageButton.Left = listPanel.Left;
			nextPageButton.Left = listPanel.Right - BUTTON_WIDTH;
			headerLabel.Left = prevPageButton.Right + PANEL_MARGIN;
			filterBox.Left = nextPageButton.Left - (FILTER_WIDTH + PANEL_MARGIN);
			filterLabel.Left = filterBox.Left - (filterLabel.Width + PANEL_MARGIN);
			LayoutEntries();
			oldWidth = panel.Width;
		}

	}

}
