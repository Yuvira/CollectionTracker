using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	public class Setentrylist : TrackerPage {

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
			private TextBox prefixBox;
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

				//Fields
				panel = Utils.GeneratePanel(new Rectangle(5, 5, 515, 145));
				nameBox = Utils.GenerateTextBox(new Rectangle(5, 5, 400, 30), this.set.Name);
				panel.Controls.Add(nameBox);
				codeBox = Utils.GenerateTextBox(new Rectangle(5, 40, 120, 30), this.set.Code);
				panel.Controls.Add(codeBox);
				typeBox = Utils.GenerateTextBox(new Rectangle(130, 40, 135, 30), this.set.Type);
				panel.Controls.Add(typeBox);
				dateOrder = Utils.GenerateNumericUpDown(new Rectangle(270, 40, 135, 30), this.set.DateOrder);
				panel.Controls.Add(dateOrder);
				dateBox = Utils.GenerateTextBox(new Rectangle(5, 75, 120, 30), this.set.Date);
				panel.Controls.Add(dateBox);
				prefixBox = Utils.GenerateTextBox(new Rectangle(130, 75, 135, 30), this.set.PrefixOrder);
				panel.Controls.Add(prefixBox);
				mainCount = Utils.GenerateNumericUpDown(new Rectangle(270, 75, 135, 30), this.set.MainCount);
				panel.Controls.Add(mainCount);
				pathLabel = Utils.GenerateLabel(new Rectangle(135, 110, 375, TEXT_HEIGHT), this.set.ImgPath);
				panel.Controls.Add(pathLabel);
				iconBox = Utils.GeneratePictureBox(new Rectangle(410, 5, 100, 100));
				if (this.set.ImgPath.Length > 0)
					Utils.TryLoadImage(iconBox, this.set.ImgPath);
				iconBox.Cursor = Cursors.Hand;
				iconBox.Click += SearchIcon;
				panel.Controls.Add(iconBox);

				//Buttons
				Button deleteButton = Utils.GenerateButton(new Rectangle(5, 110, 30, 30), "-");
				deleteButton.Click += DeleteEntry;
				panel.Controls.Add(deleteButton);
				Button saveButton = Utils.GenerateButton(new Rectangle(40, 110, 90, 30), "Save");
				saveButton.Click += SaveEntry;
				panel.Controls.Add(saveButton);

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
				if (!set.Name.Equals(nameBox.Text))
					Printentry.SetsAltered = true;
				set.Copy(new Set(
					nameBox.Text,
					codeBox.Text,
					typeBox.Text,
					dateBox.Text,
					pathLabel.Text,
					(int)mainCount.Value,
					(int)dateOrder.Value,
					prefixBox.Text
				));
			}

			//Compare
			public static int SortNewest(Setentry entry1, Setentry entry2) =>
				Set.SortNewest(entry1.set, entry2.set);

		}

		#endregion

		//Properties
		private List<Setentry> entries;

		//Controls
		private Panel listPanel;

		//Constructor
		public Setentrylist() : base() {

			//Set list
			List<Set> sets = new List<Set>(TrackerForm.Catalog.Sets);
			sets.Sort(Set.SortNewest);

			//Panel
			listPanel = Utils.GeneratePanel(Utils.CenterRect(new Size(545, panel.Height - 25), panel.Size));
			listPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			listPanel.AutoScroll = true;

			//Generate sets
			Point pos = new Point(5, 5);
			entries = new List<Setentry>();
			listPanel.SuspendLayout();
			foreach (Set set in sets) {
				Setentry entry = new Setentry(this, set);
				entry.Panel.Location = pos;
				pos.Y += entry.Panel.Height + 5;
				entries.Add(entry);
				listPanel.Controls.Add(entry.Panel);
			}
			listPanel.ResumeLayout();

			//Add button
			Button addButton = Utils.GenerateButton(new Rectangle(listPanel.Location.X + listPanel.Width + 5, 5, 30, 30), "+");
			addButton.Click += AddSet;
			panel.Controls.Add(addButton);

			//Add to panel
			panel.Controls.Add(listPanel);

		}

		//Update layout
		private void Update() {
			listPanel.AutoScrollPosition = Point.Empty;
			entries.Sort(Setentry.SortNewest);
			Point pos = new Point(5, 5);
			listPanel.SuspendLayout();
			foreach (Setentry entry in entries) {
				entry.Panel.Location = pos;
				pos.Y += entry.Panel.Height + 5;
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
			int height = entry.Panel.Height + 5;
			entries.Remove(entry);
			listPanel.Controls.Remove(entry.Panel);
			entry.Panel.Dispose();
			TrackerForm.Catalog.Sets.Remove(set);
			Printentry.SetsAltered = true;
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
			Printentry.SetsAltered = true;
			Update();
		}

	}

}
