using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	#region List Base

	//Entry list
	public abstract class Entrylist {

		//Properties
		protected TrackerPage parent;
		protected int yPos;

		//Controls
		protected TrackerPanel panel;
		protected Button addButton;
		protected List<Button> removeButtons;
		protected List<Button> upButtons;
		protected List<Button> downButtons;

		//Accessors
		public TrackerPanel Panel => panel;

		//Constructor
		public Entrylist(TrackerPage parent, string headerText, int startY) {

			//Initial values
			this.parent = parent;
			yPos = startY;

			//Lists
			removeButtons = new List<Button>();
			upButtons = new List<Button>();
			downButtons = new List<Button>();

			//Panel
			panel = Utils.GenerateTrackerPanel(new Rectangle(0, yPos, 780, 10));

			//Header label
			Label header = Utils.GenerateLabel(new Rectangle(0, 0, 780, TrackerPage.TEXT_HEIGHT), headerText);
			panel.Controls.Add(header);

			//Add button
			addButton = Utils.GenerateButton(new Rectangle(0, TrackerPage.TEXT_HEIGHT + 5, 30, 30), "+");
			addButton.Click += AddRow;
			panel.Controls.Add(addButton);

			//Position
			yPos = addButton.Location.Y;
			panel.Height = yPos + 30;

		}

		//Add row
		public virtual void AddRow(object sender, EventArgs e) {
			Button remove = Utils.GenerateButton(new Rectangle(0, yPos, 30, 30), "-");
			Button up = Utils.GenerateButton(new Rectangle(35, yPos, 30, 30), "↑");
			Button down = Utils.GenerateButton(new Rectangle(70, yPos, 30, 30), "↓");
			panel.Controls.Add(remove);
			panel.Controls.Add(up);
			panel.Controls.Add(down);
			removeButtons.Add(remove);
			upButtons.Add(up);
			downButtons.Add(down);
		}

	}

	#endregion

	#region Fields

	//Field entry
	public class Fieldlist : Entrylist {

		//Controls
		private List<TextBox> fields;
		private List<TextBox> values;

		//Constructor
		public Fieldlist(TrackerPage parent, string headerText, Dictionary<string, string> cardFields, int startY) : base(parent, headerText, startY) {

			//Lists
			fields = new List<TextBox>();
			values = new List<TextBox>();

			//Fields
			if (cardFields != null) {
				panel.SuspendLayout();
				foreach (string field in cardFields.Keys)
					AddRow(field, cardFields[field]);
				panel.ResumeLayout();
			}

		}

		//Add row
		public override void AddRow(object sender, EventArgs e) => AddRow("", "");
		public void AddRow(string fieldText, string valueText) {
			base.AddRow(null, null);
			TextBox field = Utils.GenerateTextBox(new Rectangle(105, yPos, 100, 30), fieldText);
			TextBox value;
			if (fieldText.ToLower().Equals("oracle") || fieldText.ToLower().Equals("flavor"))
				value = Utils.GenerateTextBox(new Rectangle(210, yPos, 570, 120), valueText, true);
			else
				value = Utils.GenerateTextBox(new Rectangle(210, yPos, 570, 30), valueText);
			panel.Controls.Add(field);
			panel.Controls.Add(value);
			fields.Add(field);
			values.Add(value);
			addButton.Location = new Point(0, addButton.Location.Y + (value.Multiline ? 125 : 35));
			yPos = addButton.Location.Y;
			panel.Height = yPos + 30;
		}

		//Get fields as dictionary
		public Dictionary<string, string> GetFieldDict() {
			Dictionary<string, string> dict = new Dictionary<string, string>();
			for (int i = 0; i < fields.Count; ++i)
				if (!dict.ContainsKey(fields[i].Text))
					dict.Add(fields[i].Text, values[i].Text);
			return dict;
		}

	}

	#endregion

	#region Treatments

	//Treatment entry
	public class Treatmentlist : Entrylist {

		//Controls
		private List<ComboBox> treatments;

		//Constructor
		public Treatmentlist(TrackerPage parent, string headerText, List<string> cardTreatments, int startY) : base(parent, headerText, startY) {

			//Lists
			treatments = new List<ComboBox>();

			//Fields
			if (cardTreatments != null) {
				panel.SuspendLayout();
				foreach (string treatment in cardTreatments)
					AddRow(treatment);
				panel.ResumeLayout();
			}

		}

		//Add row
		public override void AddRow(object sender, EventArgs e) => AddRow("");
		public void AddRow(string treatmentText) {
			base.AddRow(null, null);
			ComboBox treatment = Utils.GenerateComboBox(new Rectangle(105, yPos, 675, 30), ComboBoxStyle.DropDown, true);
			treatment.Items.AddRange(parent.Catalog.Printings.SelectMany(p => p.Treatments).Select(t => t.Name).Distinct().ToArray());
			treatment.Text = treatmentText;
			panel.Controls.Add(treatment);
			treatments.Add(treatment);
			addButton.Location = new Point(0, addButton.Location.Y + 35);
			yPos = addButton.Location.Y;
			panel.Height = yPos + 30;
		}

		//Get fields as dictionary
		public List<string> GetTreatmentList() {
			List<string> list = new List<string>();
			for (int i = 0; i < treatments.Count; ++i)
				if (!list.Contains(treatments[i].Text))
					list.Add(treatments[i].Text);
			return list;
		}

	}

	#endregion

	#region Images

	//Field entry
	public class Imagelist : Entrylist {

		//Controls
		private List<Button> searchButtons;
		private List<Label> imgPaths;

		//Constructor
		public Imagelist(TrackerPage parent, string headerText, List<string> cardImages, int startY) : base(parent, headerText, startY) {

			//Lists
			searchButtons = new List<Button>();
			imgPaths = new List<Label>();

			//Fields
			if (cardImages != null) {
				panel.SuspendLayout();
				foreach (string imgPath in cardImages)
					AddRow(imgPath);
				panel.ResumeLayout();
			}

		}

		//Add row
		public override void AddRow(object sender, EventArgs e) => AddRow("");
		public void AddRow(string pathText) {
			base.AddRow(null, null);
			Button search = Utils.GenerateButton(new Rectangle(105, yPos, 100, 30), "Search");
			Label path = Utils.GenerateLabel(new Rectangle(210, yPos, 670, 30), pathText);
			panel.Controls.Add(search);
			panel.Controls.Add(path);
			searchButtons.Add(search);
			imgPaths.Add(path);
			addButton.Location = new Point(0, addButton.Location.Y + 35);
			yPos = addButton.Location.Y;
			panel.Height = yPos + 30;
		}

		//Get fields as dictionary
		public List<string> GetPathList() {
			List<string> list = new List<string>();
			for (int i = 0; i < imgPaths.Count; ++i)
				list.Add(imgPaths[i].Text);
			return list;
		}

	}

	#endregion

}
