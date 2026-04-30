using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
		public Action<Entrylist<T>> OnListEmpty;

		//Constructor
		public Entrylist(TrackerPage parent, string headerText, List<T> entries) {

			//Initial values
			this.parent = parent;
			this.entries = new List<T>(entries);

			//Panel
			panel = Utils.GenerateTrackerPanel(new Rectangle(0, 0, 780, 0));

			//Suspend
			panel.SuspendLayout();

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

			//Resume
			panel.ResumeLayout();

		}

		//Add event handlers
		private void AddEventHandlers(T entry) {
			entry.OnClickRemove += RemoveRow;
			entry.OnClickUp += MoveRow;
			entry.OnClickDown += MoveRow;
			entry.OnResize += ResizeRows;
		}

		//Add row
		private void AddRow(object sender, EventArgs e) => AddRow(new T());
		public void AddRow(T entry) {
			entry.Panel.Location = add.Location;
			add.Location = add.Location.Add(0, entry.Panel.Height + 5);
			entries.Add(entry);
			panel.SuspendLayout();
			panel.Controls.Add(entry.Panel);
			panel.Height += entry.Panel.Height + 5;
			panel.ResumeLayout();
			AddEventHandlers(entry);
			OnListResize?.Invoke();
		}

		//Remove row
		private void RemoveRow(ItemEntry entry) {
			if (!(entry is T entryT) || !entries.Contains(entryT))
				return;
			panel.SuspendLayout();
			int height = entryT.Panel.Height + 5;
			for (int i = entries.IndexOf(entryT) + 1; i < entries.Count; ++i)
				entries[i].Panel.Location = entries[i].Panel.Location.Add(0, -height);
			add.Location = add.Location.Add(0, -height);
			panel.Height -= height;
			entries.Remove(entryT);
			panel.Controls.Remove(entryT.Panel);
			entryT.Panel.Dispose();
			panel.ResumeLayout();
			if (entries.Count == 0)
				OnListEmpty?.Invoke(this);
			OnListResize?.Invoke();
		}

		//Clear all rows
		public void ClearRows() {
			panel.SuspendLayout();
			foreach (T entry in entries) {
				add.Location = add.Location.Add(0, -(entry.Panel.Height + 5));
				panel.Height -= entry.Panel.Height + 5;
				panel.Controls.Remove(entry.Panel);
				entry.Panel.Dispose();
			}
			panel.ResumeLayout();
			entries.Clear();
			OnListEmpty?.Invoke(this);
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

		//Resize rows
		private void ResizeRows(ItemEntry entry, int delta) {
			if (!(entry is T entryT) || !entries.Contains(entryT))
				return;
			panel.SuspendLayout();
			int y = TrackerPage.TEXT_HEIGHT + TrackerPage.PANEL_MARGIN;
			for (int i = 0; i < entries.Count; ++i) {
				entries[i].Panel.Top = y;
				y += entries[i].Panel.Height + 5;
			}
			add.Top = y;
			panel.Height = y + add.Height;
			panel.ResumeLayout();
			OnListResize?.Invoke();
		}

	}

	#endregion

	#region Base Item Entry

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
		public Action<ItemEntry, int> OnResize;

		//Constructor
		public ItemEntry() {
			panel = Utils.GenerateTrackerPanel(new Rectangle(0, 0, 780, 30));
			remove = Utils.GenerateButton(new Rectangle(0, 0, 30, 30), "-");
			up = Utils.GenerateButton(new Rectangle(35, 0, 30, 30), "↑");
			down = Utils.GenerateButton(new Rectangle(70, 0, 30, 30), "↓");
			remove.TabStop = false;
			up.TabStop = false;
			down.TabStop = false;
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

	#endregion

	#region Field Entry

	//Field entry
	public class FieldEntry : ItemEntry {

		//Controls
		private ComboBox field;
		private TextBox textValue;
		private ComboBox listValue;

		//Accessors
		public string Field => field.Text;
		public string Value => textValue.Visible ? textValue.Text : listValue.Text;

		//Constructor
		public FieldEntry() : this("", "") { }
		public FieldEntry(string fieldString, string valueString) : base() {
			field = Utils.GenerateComboBox(new Rectangle(105, 0, 100, 30), ComboBoxStyle.DropDown, true);
			if (TrackerForm.FieldContext == FieldContext.CARD)
				field.Items.AddRange(TrackerForm.Catalog.Cards.SelectMany(c => c.Fields.Keys).Distinct().ToArray());
			else if (TrackerForm.FieldContext == FieldContext.PRINT)
				field.Items.AddRange(TrackerForm.Catalog.Printings.SelectMany(p => p.Fields.Keys).Distinct().ToArray());
			field.TextChanged += OnFieldChanged;
			textValue = Utils.GenerateTextBox(new Rectangle(210, 0, 570, 30), valueString);
			textValue.KeyDown += HandleControlInput;
			textValue.KeyUp += TextKeyPressed;
			listValue = Utils.GenerateComboBox(new Rectangle(210, 0, 570, 30), ComboBoxStyle.DropDown, true);
			listValue.Text = valueString;
			listValue.Hide();
			field.Text = fieldString;
			panel.Controls.Add(field);
			panel.Controls.Add(textValue);
			panel.Controls.Add(listValue);
		}

		//Detect if value field needs to be modified
		public void OnFieldChanged(object sender = null, EventArgs e = null) {
			if (Utils.ListableFields.Contains(field.Text)) {
				listValue.Items.Clear();
				listValue.Items.AddRange(TrackerForm.Catalog.Printings.Select(p => p.GetField(field.Text)).Distinct().ToArray());
				if (!listValue.Visible) {
					listValue.Text = textValue.Text;
					textValue.Hide();
					listValue.Show();
				}
			}
			else if (!textValue.Visible) {
				textValue.Text = listValue.Text;
				listValue.Hide();
				textValue.Show();
			}
			if (Utils.MultilineFields.Contains(field.Text)) {
				if (!textValue.Multiline) {
					int newHeight = field.Text.Equals("oracle") ? 210 : 120;
					int delta = newHeight - textValue.Height;
					textValue.Multiline = true;
					textValue.ScrollBars = ScrollBars.Both;
					textValue.Height = newHeight;
					panel.Height = newHeight;
					OnResize?.Invoke(this, delta);
				}
			}
			else if (textValue.Multiline) {
				int delta = 30 - textValue.Height;
				textValue.Multiline = false;
				textValue.ScrollBars = ScrollBars.None;
				textValue.Height = 30;
				panel.Height = 30;
				OnResize?.Invoke(this, delta);
			}
		}

		//Update list items
		public void RefreshListItems() {
			if (Utils.ListableFields.Contains(field.Text)) {
				listValue.Items.Clear();
				listValue.Items.AddRange(TrackerForm.Catalog.Printings.Select(p => p.GetField(field.Text)).Distinct().ToArray());
			}
		}

		//Tag shortcuts
		private void HandleControlInput(object sender, KeyEventArgs e) {
			if (!e.Control || string.IsNullOrEmpty(textValue.SelectedText))
				return;
			if (e.KeyCode == Keys.L || e.KeyCode == Keys.B || e.KeyCode == Keys.U || e.KeyCode == Keys.R || e.KeyCode == Keys.T)
				e.SuppressKeyPress = true;
		}

		//Tag shortcuts
		private void TextKeyPressed(object sender, KeyEventArgs e) {
			if (!e.Control || string.IsNullOrEmpty(textValue.SelectedText))
				return;
			string code = "";
			if (e.KeyCode == Keys.L)
				code = "i";
			else if (e.KeyCode == Keys.B)
				code = "b";
			else if (e.KeyCode == Keys.U)
				code = "u";
			else if (e.KeyCode == Keys.O)
				code = "c|";
			else if (e.KeyCode == Keys.R)
				code = "ct|";
			else if (e.KeyCode == Keys.T)
				code = "tt|";
			if (string.IsNullOrWhiteSpace(code))
				return;
			int index = textValue.SelectionStart;
			int length = textValue.SelectionLength;
			if (textValue.Text.Substring(index, length).EndsWith(" "))
				--length;
			if (textValue.Text.Substring(index, length).EndsWith(".") || textValue.Text.Substring(index, length).EndsWith(","))
				--length;
			textValue.Text = textValue.Text.Insert(index + length, "</" + (code.Contains('|') ? code.Substring(0, code.Length - 1) : code) + ">");
			textValue.Text = textValue.Text.Insert(index, "<" + code + ">");
			textValue.SelectionStart = index + code.Length + (code.Contains('|') ? 1 : 2);
			textValue.SelectionLength = code.Contains('|') ? 0 : length;
		}

		//Clear value
		public void SetValue(string value = "") {
			textValue.Text = value;
			listValue.Text = value;
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
				if (dict.ContainsKey(entry.Field))
					MessageBox.Show($"Duplicate field [{entry.Field}]");
				else
					dict.Add(entry.Field, entry.Value);
			}
			return dict;
		}

	}

	#endregion

	#region Treatment Entry

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

	#endregion

	#region Image Entry

	//Image path entry
	public class ImageEntry : ItemEntry {

		//Controls
		private Button search;
		private Label path;
		private static PictureBox cardtip;

		//Constructor
		public ImageEntry() : this("") { }
		public ImageEntry(string imagePath) : base() {
			search = Utils.GenerateButton(new Rectangle(105, 0, 100, 30), "Search");
			search.Click += SearchImage;
			path = Utils.GenerateLabel(new Rectangle(210, 0, 670, 30), imagePath);
			path.MouseEnter += ShowCardtip;
			path.MouseLeave += HideCardtip;
			path.MouseClick += OpenImage;
			if (cardtip == null) {
				cardtip = Utils.GeneratePictureBox(new Rectangle(0, 0, 250, 350));
				cardtip.Hide();
			}
			panel.Controls.Add(search);
			panel.Controls.Add(path);
		}

		//Search
		private void SearchImage(object sender, EventArgs e) {
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
				path.Text = filePath;
			}
			else
				path.Text = "";
		}

		//Show cardtip relative to given control
		private void ShowCardtip(object sender, EventArgs e) {
			if (cardtip.Parent == null)
				panel.Parent.Parent.Controls.Add(cardtip);
			Utils.TryLoadCardImage(cardtip, path.Text, TrackerForm.Catalog.Game);
			Point pos = panel.Parent.Location.Add(panel.Location).Add(path.Location);
			pos.X += (Utils.MeasureWidth(path.Text) / 2) - (cardtip.Width / 2);
			pos.Y += TrackerPage.TEXT_HEIGHT;
			cardtip.Show();
			cardtip.BringToFront();
			cardtip.Location = pos;
		}
		private void HideCardtip(object sender, EventArgs e) => cardtip.Hide();

		//Open image file
		private void OpenImage(object sender, EventArgs e) => Process.Start(Path.GetFullPath(path.Text));

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
