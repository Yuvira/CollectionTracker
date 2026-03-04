using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	#region Entry List

	//Entry list
	public class Entrylist<T> where T : ItemEntry, new() {

		//Properties
		private TrackerPage parent;
		private List<T> entries;

		//Controls
		private TrackerPanel panel;
		private Button add;

		//Accessors
		public TrackerPage Parent => parent;
		public TrackerPanel Panel => panel;
		public List<T> Entries => entries;

		//Events
		public Action OnListResize;

		//Constructor
		public Entrylist(TrackerPage parent, string headerText, List<T> entries) {

			//Initial values
			this.parent = parent;
			this.entries = new List<T>(entries);

			//Panel
			panel = Utils.GenerateTrackerPanel(new Rectangle(0, 0, 780, 0));

			//Header label
			Label header = Utils.GenerateLabel(new Rectangle(0, 0, 780, TrackerPage.TEXT_HEIGHT), headerText);
			panel.Controls.Add(header);

			//Layout entries
			Point pos = new Point(0, TrackerPage.TEXT_HEIGHT + 5);
			foreach (T entry in entries) {
				entry.Panel.Location = pos;
				pos.Y += entry.Panel.Height + 5;
				panel.Controls.Add(entry.Panel);
				AddEventHandlers(entry);
			}

			//Add button
			add = Utils.GenerateButton(new Rectangle(0, pos.Y, 30, 30), "+");
			add.Click += AddRow;
			panel.Controls.Add(add);

			//Panel size
			panel.Height = pos.Y + 30;

		}

		//Add event handlers
		private void AddEventHandlers(T entry) {
			entry.OnClickRemove += RemoveRow;
			entry.OnClickUp += MoveRow;
			entry.OnClickDown += MoveRow;
		}

		//Add row
		private void AddRow(object sender, EventArgs e) {
			T entry = new T();
			entry.Panel.Location = add.Location;
			add.Location = add.Location.Add(0, entry.Panel.Height + 5);
			entries.Add(entry);
			panel.Controls.Add(entry.Panel);
			panel.Height += entry.Panel.Height + 5;
			AddEventHandlers(entry);
			OnListResize?.Invoke();
		}

		//Remove row
		private void RemoveRow(ItemEntry entry) {
			if (!(entry is T entryT) || !entries.Contains(entryT))
				return;
			int height = entryT.Panel.Height + 5;
			for (int i = entries.IndexOf(entryT) + 1; i < entries.Count; ++i)
				entries[i].Panel.Location = entries[i].Panel.Location.Add(0, -height);
			add.Location = add.Location.Add(0, -height);
			panel.Height -= height;
			entries.Remove(entryT);
			panel.Controls.Remove(entryT.Panel);
			entryT.Panel.Dispose();
			OnListResize?.Invoke();
		}

		//Move rows
		private void MoveRow(ItemEntry entry, int move) {
			if (!(entry is T entryT) || !entries.Contains(entryT))
				return;
			int i = entries.IndexOf(entryT);
			if (i + move < 0 || i + move >= entries.Count)
				return;
			T entryT2 = entries[i + move];
			entries[i + move] = entryT;
			entries[i] = entryT2;
			entryT.Panel.Location = entryT.Panel.Location.Add(0, (entryT2.Panel.Height + 5) * move);
			entryT2.Panel.Location = entryT2.Panel.Location.Add(0, (entryT.Panel.Height + 5) * -move);
		}

	}

	#endregion

	#region Entries

	//Item entry
	public abstract class ItemEntry {

		//Controls
		protected TrackerPanel panel;
		protected Button remove;
		protected Button up;
		protected Button down;

		//Accessors
		public TrackerPanel Panel => panel;

		//Events
		public Action<ItemEntry> OnClickRemove;
		public Action<ItemEntry, int> OnClickUp;
		public Action<ItemEntry, int> OnClickDown;

		//Constructor
		public ItemEntry() {
			panel = Utils.GenerateTrackerPanel(new Rectangle(0, 0, 780, 30));
			remove = Utils.GenerateButton(new Rectangle(0, 0, 30, 30), "-");
			up = Utils.GenerateButton(new Rectangle(35, 0, 30, 30), "↑");
			down = Utils.GenerateButton(new Rectangle(70, 0, 30, 30), "↓");
			remove.Click += Remove;
			up.Click += Up;
			down.Click += Down;
			panel.Controls.Add(remove);
			panel.Controls.Add(up);
			panel.Controls.Add(down);
		}

		//Invoke actions
		private void Remove(object sender, EventArgs e) => OnClickRemove?.Invoke(this);
		private void Up(object sender, EventArgs e) => OnClickUp?.Invoke(this, -1);
		private void Down(object sender, EventArgs e) => OnClickDown?.Invoke(this, 1);

	}

	//Field entry
	public class FieldEntry : ItemEntry {

		//Controls
		private TextBox field;
		private TextBox value;

		//Constructor
		public FieldEntry() : this("", "") { }
		public FieldEntry(string fieldString, string valueString) : base() {
			field = Utils.GenerateTextBox(new Rectangle(105, 0, 100, 30), fieldString);
			value = Utils.GenerateTextBox(new Rectangle(210, 0, 570, 30), valueString);
			panel.Controls.Add(field);
			panel.Controls.Add(value);
		}

		//List generator
		public static List<FieldEntry> GenerateEntries(Dictionary<string, string> fields) {
			List<FieldEntry> entries = new List<FieldEntry>();
			foreach (string field in fields.Keys)
				entries.Add(new FieldEntry(field, fields[field]));
			return entries;
		}

		//Data retriever
		public static Dictionary<string, string> GetEntryDict(List<FieldEntry> entries) {
			Dictionary<string, string> dict = new Dictionary<string, string>();
			foreach (FieldEntry entry in entries) {
				if (dict.ContainsKey(entry.field.Text))
					MessageBox.Show($"Duplicate field [{entry.field.Text}]");
				else
					dict.Add(entry.field.Text, entry.value.Text);
			}
			return dict;
		}

	}

	//Treatment entry
	public class TreatmentEntry : ItemEntry {

		//Controls
		private ComboBox treatment;

		//Constructor
		public TreatmentEntry() : this("") { }
		public TreatmentEntry(string treatmentString) : base() {
			treatment = Utils.GenerateComboBox(new Rectangle(105, 0, 675, 30), ComboBoxStyle.DropDown, true);
			treatment.Items.AddRange(TrackerForm.Catalog.Printings.SelectMany(p => p.Treatments).Select(t => t.Name).Distinct().ToArray());
			treatment.Text = treatmentString;
			panel.Controls.Add(treatment);
		}

		//List generator
		public static List<TreatmentEntry> GenerateEntries(List<string> treatments) {
			List<TreatmentEntry> entries = new List<TreatmentEntry>();
			foreach (string treatment in treatments)
				entries.Add(new TreatmentEntry(treatment));
			return entries;
		}

		//Data retriever
		public static List<string> GetEntryList(List<TreatmentEntry> entries) {
			List<string> list = new List<string>();
			foreach (TreatmentEntry entry in entries) {
				if (list.Contains(entry.treatment.Text))
					MessageBox.Show($"Duplicate treatment [{entry.treatment.Text}]");
				else
					list.Add(entry.treatment.Text);
			}
			return list;
		}

	}

	//Image path entry
	public class ImageEntry : ItemEntry {

		//Controls
		private Button search;
		private Label path;

		//Constructor
		public ImageEntry() : this("") { }
		public ImageEntry(string imagePath) : base() {
			search = Utils.GenerateButton(new Rectangle(105, 0, 100, 30), "Search");
			path = Utils.GenerateLabel(new Rectangle(210, 0, 670, 30), imagePath);
			panel.Controls.Add(search);
			panel.Controls.Add(path);
		}

		//List generator
		public static List<ImageEntry> GenerateEntries(List<string> paths) {
			List<ImageEntry> entries = new List<ImageEntry>();
			foreach (string path in paths)
				entries.Add(new ImageEntry(path));
			return entries;
		}

		//Data retriever
		public static List<string> GetEntryList(List<ImageEntry> entries) {
			List<string> list = new List<string>();
			foreach (ImageEntry entry in entries)
				list.Add(entry.path.Text);
			return list;
		}

	}

	#endregion

}
