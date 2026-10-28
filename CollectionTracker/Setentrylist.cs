using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	public class Setentrylist : TrackerPage {

		//Formatting constants
		private const int SETBOX_WIDTH = 605;

		#region Set Entry

		//Set entry
		private class Setentry {

			//Properties
			private Setentrylist parent;
			private Set set;

			//Controls
			private Panel panel;
			private TextBox nameBox;
			private TextBox codeBox;
			private TextBox typeBox;
			private NumericUpDown dateOrder;
			private TextBox dateBox;
			private NumericUpDown mainCount;
			private Label pathLabel;
			private PictureBox iconBox;

			//Accessors
			public Panel Panel => panel;

			//Constructor
			public Setentry(Setentrylist parent, Set set) {

				//Parent
				this.parent = parent;
				this.set = set;

				//Row positions
				int row1Y = PANEL_MARGIN;
				int row2Y = BUTTON_HEIGHT + (PANEL_MARGIN * 2);
				int row3Y = (BUTTON_HEIGHT * 2) + (PANEL_MARGIN * 3);

				//Fields
				panel = Utils.GeneratePanel(new Rectangle(PANEL_MARGIN, row1Y, SETBOX_WIDTH, (BUTTON_HEIGHT * 3) + (PANEL_MARGIN * 4)));
				nameBox = Utils.GenerateTextBox(new Rectangle(PANEL_MARGIN, row1Y, 370, BUTTON_HEIGHT), this.set.Name);
				panel.Controls.Add(nameBox);
				codeBox = Utils.GenerateTextBox(new Rectangle(nameBox.Right + PANEL_MARGIN, row1Y, 120, BUTTON_HEIGHT), this.set.Code);
				panel.Controls.Add(codeBox);
				dateBox = Utils.GenerateTextBox(new Rectangle(nameBox.Left, row2Y, 120, BUTTON_HEIGHT), this.set.Date);
				panel.Controls.Add(dateBox);
				typeBox = Utils.GenerateTextBox(new Rectangle(dateBox.Right + PANEL_MARGIN, row2Y, 120, BUTTON_HEIGHT), this.set.Type);
				panel.Controls.Add(typeBox);
				dateOrder = Utils.GenerateNumericUpDown(new Rectangle(typeBox.Right + PANEL_MARGIN, row2Y, 120, BUTTON_HEIGHT), this.set.DateOrder);
				panel.Controls.Add(dateOrder);
				mainCount = Utils.GenerateNumericUpDown(new Rectangle(dateOrder.Right + PANEL_MARGIN, row2Y, 120, BUTTON_HEIGHT), this.set.MainCount);
				panel.Controls.Add(mainCount);

				//Buttons & Path
				Button deleteButton = Utils.GenerateButton(new Rectangle(dateBox.Left, row3Y, BUTTON_HEIGHT, BUTTON_HEIGHT), "-");
				deleteButton.Click += DeleteEntry;
				panel.Controls.Add(deleteButton);
				Button saveButton = Utils.GenerateButton(new Rectangle(deleteButton.Right + PANEL_MARGIN, row3Y, 120 - (deleteButton.Width + PANEL_MARGIN), BUTTON_HEIGHT), "Save");
				saveButton.Click += SaveEntry;
				panel.Controls.Add(saveButton);
				pathLabel = Utils.GenerateLabel(new Rectangle(saveButton.Right + PANEL_MARGIN, row3Y, mainCount.Right - (saveButton.Right + PANEL_MARGIN), TEXT_HEIGHT), this.set.ImgPath);
				panel.Controls.Add(pathLabel);

				//Icon
				iconBox = Utils.GeneratePictureBox(new Rectangle(codeBox.Right + PANEL_MARGIN, row1Y, 100, 100));
				if (this.set.ImgPath.Length > 0)
					Utils.TryLoadImage(iconBox, this.set.ImgPath);
				iconBox.Cursor = Cursors.Hand;
				iconBox.Click += SearchIcon;
				panel.Controls.Add(iconBox);

			}

			//Search for image
			private void SearchIcon(object sender, EventArgs e) {
				OpenFileDialog dialog = new OpenFileDialog();
				dialog.Title = "Select Image";
				dialog.Filter = "All files(*.*) | *.*";
				if (dialog.ShowDialog() == DialogResult.OK) {
					string curDir = Directory.GetCurrentDirectory();
					string filePath = dialog.FileName;
					if (!filePath.Contains(curDir)) {
						MessageBox.Show("Not a local path!");
						return;
					}
					filePath = filePath.Remove(filePath.IndexOf(curDir), curDir.Length + 1);
					pathLabel.Text = filePath;
					Utils.TryLoadImage(iconBox, filePath);
				}
				else {
					pathLabel.Text = "";
					iconBox.Image = null;
				}
			}

			//Delete entry
			private void DeleteEntry(object sender, EventArgs e) => parent.DeleteEntry(this, set);

			//Save entry fields
			private void SaveEntry(object sender, EventArgs e) {
				Printentry.RemoveSet(set);
				set.Copy(new Set(
					nameBox.Text,
					codeBox.Text,
					typeBox.Text,
					dateBox.Text,
					pathLabel.Text,
					(int)mainCount.Value,
					(int)dateOrder.Value
				));
				Printentry.AddSet(set);
			}

			//Compare
			public static int SortNewest(Setentry entry1, Setentry entry2) =>
				Set.SortNewest(entry1.set, entry2.set);

		}

		#endregion

		//Properties
		private List<Setentry> entries = null;

		//Controls
		private Panel listPanel;
		private TextBox dateFilter = null;

		//Constructor
		public Setentrylist() : base() {

			//Panel
			listPanel = Utils.GeneratePanel(Utils.CenterRect(new Size(SETBOX_WIDTH + (PANEL_MARGIN * 2) + SCROLL_MARGIN, panel.Height - 25), panel.Size));
			listPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			listPanel.AutoScroll = true;

			//Generate
			GenerateEntries();

			//Add button
			Button addButton = Utils.GenerateButton(new Rectangle(listPanel.Location.X + listPanel.Width + PANEL_MARGIN, PANEL_MARGIN, BUTTON_HEIGHT, BUTTON_HEIGHT), "+");
			addButton.Click += AddSet;
			panel.Controls.Add(addButton);

			//Filter
			Label filterLabel = Utils.GenerateAutoSizeLabel(new Point(addButton.Left, addButton.Bottom + PANEL_MARGIN), "Before:");
			dateFilter = Utils.GenerateTextBox(new Rectangle(addButton.Left, filterLabel.Bottom + PANEL_MARGIN, 120, BUTTON_HEIGHT), DateTime.Now.ToString("yyyy-MM-dd"));
			Button filterButton = Utils.GenerateButton(new Rectangle(addButton.Left, filterLabel.Bottom + (PANEL_MARGIN * 2) + BUTTON_HEIGHT, 120, BUTTON_HEIGHT), "Filter");
			filterButton.Click += GenerateEntries;
			panel.Controls.Add(filterLabel);
			panel.Controls.Add(dateFilter);
			panel.Controls.Add(filterButton);

			//Add to panel
			panel.Controls.Add(listPanel);

		}

		//Entry generator
		private void GenerateEntries(object sender, EventArgs e) => GenerateEntries();
		private void GenerateEntries() {

			//Set list
			List<Set> sets;
			if (dateFilter != null)
				sets = new List<Set>(TrackerForm.Catalog.Sets.Where(s => s.Date.CompareTo(dateFilter.Text) < 0).ToList());
			else
				sets = new List<Set>(TrackerForm.Catalog.Sets);
			sets.Sort(Set.SortNewest);

			//Clear entries and get start position
			Point pos = new Point(PANEL_MARGIN, PANEL_MARGIN);
			if (entries == null)
				entries = new List<Setentry>();
			else if (entries.Count > 0) {
				pos.Y = entries[0].Panel.Top;
				foreach (Setentry entry in entries) {
					listPanel.Controls.Remove(entry.Panel);
					entry.Panel.Dispose();
				}
				entries.Clear();
			}

			//Generate
			listPanel.SuspendLayout();
			foreach (Set set in sets) {
				Setentry entry = new Setentry(this, set);
				entry.Panel.Location = pos;
				pos.Y += entry.Panel.Height + PANEL_MARGIN;
				entries.Add(entry);
				listPanel.Controls.Add(entry.Panel);
			}
			listPanel.ResumeLayout();

		}

		//Update layout
		private void Update() {
			listPanel.AutoScrollPosition = Point.Empty;
			entries.Sort(Setentry.SortNewest);
			Point pos = new Point(PANEL_MARGIN, PANEL_MARGIN);
			listPanel.SuspendLayout();
			foreach (Setentry entry in entries) {
				entry.Panel.Location = pos;
				pos.Y += entry.Panel.Height + PANEL_MARGIN;
			}
		}

		//Delete set entry
		private void DeleteEntry(Setentry entry, Set set) {
			List<Printing> prints = TrackerForm.Catalog.Printings.Where(p => p.Set == set).ToList();
			if (prints.Count > 0) {
				MessageBox.Show($"Can't remove set, {prints.Count} prints still rely on it");
				return;
			}
			int idx = entries.IndexOf(entry);
			int height = entry.Panel.Height + PANEL_MARGIN;
			entries.Remove(entry);
			listPanel.Controls.Remove(entry.Panel);
			entry.Panel.Dispose();
			TrackerForm.Catalog.Sets.Remove(set);
			Printentry.RemoveSet(set);
			for (int i = idx; i < entries.Count; ++i)
				entries[i].Panel.Top -= height;
		}

		//Add new set
		private void AddSet(object sender, EventArgs e) {
			Set set = new Set();
			TrackerForm.Catalog.Sets.Add(set);
			Setentry entry = new Setentry(this, set);
			entries.Insert(0, entry);
			listPanel.Controls.Add(entry.Panel);
			Printentry.AddSet(set);
			Update();
		}

	}

}
