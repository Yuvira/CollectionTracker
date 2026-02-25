using System;
using System.Collections.Generic;
using System.Diagnostics;
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
				private Label nameLabel;
				private Label countLabel;
				private Button decrementButton;
				private Button incrementButton;

				//Generator
				public Treatmentrow(Printentry parent, Printing print, int idx) {

					//Initial values
					this.parent = parent;
					treatment = print.Treatments[idx];

					//Name label
					nameLabel = Utils.GenerateLabel(new Rectangle(60, 430 + (idx * 30), 115, TEXT_HEIGHT), treatment.Name);
					nameLabel.TextAlign = ContentAlignment.MiddleRight;
					parent.Panel.Controls.Add(nameLabel);

					//Count label
					countLabel = Utils.GenerateLabel(new Rectangle(185, 430 + (idx * 30), 55, TEXT_HEIGHT), treatment.OwnedCount.ToString());
					parent.Panel.Controls.Add(countLabel);

					//Decrement
					decrementButton = Utils.GenerateButton(new Rectangle(5, 425 + (idx * 30), 55, 29), "<");
					decrementButton.Click += DecrementCount;
					parent.Panel.Controls.Add(decrementButton);

					//Increment
					incrementButton = Utils.GenerateButton(new Rectangle(240, 425 + (idx * 30), 55, 29), ">");
					incrementButton.Click += IncrementCount;
					parent.Panel.Controls.Add(incrementButton);

				}

				//Dispose
				public void Dispose() {
					parent.panel.Controls.Remove(nameLabel);
					parent.panel.Controls.Remove(countLabel);
					parent.panel.Controls.Remove(decrementButton);
					parent.panel.Controls.Remove(incrementButton);
					nameLabel.Dispose();
					countLabel.Dispose();
					decrementButton.Dispose();
					incrementButton.Dispose();
				}

				//Count modifiers
				private void IncrementCount(object sender, EventArgs e) {
					treatment.Increment();
					countLabel.Text = treatment.OwnedCount.ToString();
				}
				private void DecrementCount(object sender, EventArgs e) {
					if (treatment.Decrement())
						countLabel.Text = treatment.OwnedCount.ToString();
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
				panel = Utils.GeneratePanel(new Rectangle(5, 5, 300, 430));

				//Image box
				imgBox = Utils.GeneratePictureBox(new Rectangle(0, 0, 300, 420));
				imgBox.MouseUp += OnClick;
				panel.Controls.Add(imgBox);

				//Initialize rows
				rows = new List<Treatmentrow>();

			}

			//Set print reference
			public int SetPrinting(List<Printing> printings, int idx) {

				//Set print
				print = printings[idx];

				//Resize panel
				panel.Height = 430 + (30 * print.Treatments.Count);

				//Load image
				if (print.ImagePaths.Count > 0)
					Utils.TryLoadCardImage(imgBox, print.ImagePaths[0], parent.Catalog.Game);

				//Add treatments
				foreach (Treatmentrow row in rows)
					row.Dispose();
				rows.Clear();
				for (int i = 0; i < print.Treatments.Count; ++i)
					rows.Add(new Treatmentrow(this, print, i));

				//Return height
				return panel.Height;

			}

			//Handle click event
			private void OnClick(object sender, MouseEventArgs e) {
				//if (e.Button == MouseButtons.Left)
				//	YGO_LoadCardDetails(print);
				//else if (e.Button == MouseButtons.Right)
				//	YGO_ToggleFavorite(print);
			}

		}

		#endregion

		//Properties
		private int page = 0;
		private int rowsPerPage = 15;
		private int entriesPerRow = 4;
		private List<Printing> printings;
		private List<Printentry> entries;

		//Controls
		private Label headerLabel;
		private Panel listPanel;

		//Accessors
		public int EntriesPerPage => rowsPerPage * entriesPerRow;

		//Constructor
		public Printlist(TrackerForm form, string searchTerms) : base(form) {

			//Get print list
			printings = SearchUtils.SearchPrintings(Catalog, searchTerms);

			//Header
			string str = $"Setlist: {Catalog.Sets.Count} | {Catalog.Cards.Count} | {Catalog.Printings.Count} | {Catalog.Symbols.Count}";
			headerLabel = Utils.GenerateLabel(new Rectangle((panel.Width - 1245) / 2, 5, Utils.MeasureWidth(str), TEXT_HEIGHT), str);
			headerLabel.Anchor = AnchorStyles.Top;

			//Panel
			listPanel = Utils.GeneratePanel(Utils.CenterRect(new Size(1245, panel.Height - 40), panel.Size, new Point(0, -15)));
			listPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			listPanel.AutoScroll = true;

			//Generate entries
			listPanel.SuspendLayout();
			entries = new List<Printentry>();
			Printentry entry;
			for (int i = 0; i < EntriesPerPage; ++i) {
				entry = new Printentry(this);
				entry.Panel.Visible = false;
				entries.Add(entry);
				listPanel.Controls.Add(entry.Panel);
			}
			listPanel.ResumeLayout();

			//Add to panel
			panel.Controls.Add(headerLabel);
			panel.Controls.Add(listPanel);

			UpdateEntries();

		}

		//Update entries
		public void UpdateEntries() {

			//Paging
			int maxPage = 0;
			while (((maxPage + 1) * EntriesPerPage) < printings.Count)
				++maxPage;
			if (page > maxPage)
				page = 0;
			else if (page < 0)
				page = maxPage;
			int startIdx = page * EntriesPerPage;

			//Header
			headerLabel.Text = printings.Count.ToString();

			//Loop entries
			int idx;
			int yPos = 5;
			int maxHeight;
			for (int y = 0; y < rowsPerPage; ++y) {
				maxHeight = 0;
				for (int x = 0; x < entriesPerRow; ++x) {
					idx = x + (y * entriesPerRow);
					if (idx < printings.Count) {
						maxHeight = Math.Max(maxHeight, entries[idx].SetPrinting(printings, startIdx + idx));
						entries[idx].Panel.Location = new Point(5 + (x * 305), yPos);
						entries[idx].Panel.Visible = true;
					}
					else
						entries[idx].Panel.Visible = false;
				}
				yPos += maxHeight + 5;
			}

		}

	}

}
