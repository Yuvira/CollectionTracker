using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CollectionTracker {

	//Detail page
	public class Detailpage : TrackerPage {

		//Properties
		Printing printing;

		//Controls
		Panel contentPanel;

		//Constructor
		public Detailpage(TrackerForm form, Printing printing) : base(form) {

			//Set print reference
			this.printing = printing;

			//Generate content panel
			contentPanel = Utils.GeneratePanel(Utils.CenterRect(new Size(1270, panel.Height - 10), panel.Size));
			contentPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			contentPanel.AutoScroll = true;

			//Image box
			PictureBox imgBox = Utils.GeneratePictureBox(new Rectangle(5, 5, 400, 540));
			if (this.printing.ImagePaths.Count > 0)
				Utils.TryLoadCardImage(imgBox, this.printing.ImagePaths[0], Catalog.Game);
			contentPanel.Controls.Add(imgBox);

			//Edit card
			Button editCardButton = Utils.GenerateButton(new Rectangle(5, 550, 100, BUTTON_HEIGHT), "Edit Card");
			editCardButton.Click += EditCard;
			contentPanel.Controls.Add(editCardButton);

			//Edit print
			Button editPrintButton = Utils.GenerateButton(new Rectangle(110, 550, 100, BUTTON_HEIGHT), "Edit Print");
			editPrintButton.Click += EditPrint;
			contentPanel.Controls.Add(editPrintButton);

			//Card data
			if (Catalog.Game == Game.MTG)
				LayoutDataMTG(contentPanel);

			//Add to panel
			panel.Controls.Add(contentPanel);

		}

		//Modify card data
		private void EditCard(object sender, EventArgs e) => parent.SetPage(new Cardentry(parent, printing.Card, printing));
		private void EditPrint(object sender, EventArgs e) => parent.SetPage(new Printentry(parent, printing));

		#region MTG Layout

		//Data layout
		private void LayoutDataMTG(Panel contentPanel) {

			//Header panel
			Panel headerPanel = Utils.GeneratePanel(new Rectangle(410, 5, 450, 0));
			if (printing.TryGetField("oracle", out string oracle)) {
				int height = GenerateDescription(oracle, headerPanel, new Point(LEFT_PAD, TOP_PAD));
				headerPanel.Height = height + TOP_PAD + BOTTOM_PAD;
			}

			//Add to content
			contentPanel.Controls.Add(headerPanel);

		}

		#endregion

		#region Description Generators

		//Formatting settings
		private struct FormatSettings {
			public string tooltip;
			public string printid;
			public FontStyle style;
			public Color? color;
			public int id;
		}

		//Description object class
		private abstract class DescriptionObject {
			public abstract int GetWidth();
			public abstract Point GenerateControl(Panel panel, Point location);
		}

		//Description text
		private class DescriptionText : DescriptionObject {

			//Properties
			public string text;
			public FormatSettings settings;
			public DescriptionText(string text, FormatSettings settings) {
				this.text = text;
				this.settings = settings;
			}

			//Overrides
			public override int GetWidth() => Utils.MeasureWidth(text, new Font(Utils.FONT_DEFAULT, settings.style));
			public override Point GenerateControl(Panel panel, Point location) {
				int width = Utils.MeasureWidth(text, new Font(Utils.FONT_DEFAULT, settings.style));
				panel.Controls.Add(Utils.GenerateLabel(new Rectangle(location, new Size(width, TEXT_HEIGHT)), text, new Font(Utils.FONT_DEFAULT, settings.style), settings.color));
				return new Point(location.X + width, location.Y);
			}

		}

		//Description symbol
		private class DescriptionSymbol : DescriptionObject {

			//Properties
			public Symbol symbol;
			public DescriptionSymbol(Catalog catalog, string text) {
				symbol = catalog.Symbols.FirstOrDefault(s => s.Text.Equals(text));
			}

			//Overrides
			public override int GetWidth() {
				if (symbol == null)
					return 0;
				return (int)(symbol.Aspect * TEXT_HEIGHT);
			}
			public override Point GenerateControl(Panel panel, Point location) {
				int width = (int)(symbol.Aspect * TEXT_HEIGHT);
				PictureBox imgBox = Utils.GeneratePictureBox(new Rectangle(location, new Size(width, TEXT_HEIGHT)));
				Utils.TryLoadImage(imgBox, symbol.ImgPath);
				panel.Controls.Add(imgBox);
				return new Point(location.X + width, location.Y);
			}

		}

		//Description group
		private class DescriptionGroup {

			//Properties
			public List<DescriptionObject> objects;
			public DescriptionGroup() {
				objects = new List<DescriptionObject>();
			}

			//Utils
			public void Add(DescriptionObject obj) => objects.Add(obj);
			public void Clean() {
				for (int i = 0; i < objects.Count - 1; ++i) {
					if (objects[i] is DescriptionText t1 && objects[i + 1] is DescriptionText t2 && t1.settings.id == t2.settings.id) {
						t1.text += t2.text;
						objects.RemoveAt(i + 1);
					}
				}
			}
			public int GetWidth() {
				int width = 0;
				foreach (DescriptionObject obj in objects)
					width += obj.GetWidth();
				return width;
			}
			public void Merge(DescriptionGroup group) {
				if (group.objects.Count == 0)
					return;
				if (objects.Count == 0) {
					for (int i = 0; i < group.objects.Count; ++i) {
						if (group.objects[i] is DescriptionText t) {
							if (string.IsNullOrWhiteSpace(t.text))
								continue;
							while (t.text.StartsWith(" "))
								t.text = t.text.Substring(1);
							for (int j = i; j < group.objects.Count; ++j)
								objects.Add(group.objects[j]);
							break;
						}
						else {
							for (int j = i; j < group.objects.Count; ++j)
								objects.Add(group.objects[j]);
							break;
						}
					}
				}
				else {
					if (objects.Last() is DescriptionText t1 && group.objects.First() is DescriptionText t2 && t1.settings.id == t2.settings.id) {
						t1.text += t2.text;
						for (int i = 1; i < group.objects.Count; ++i)
							objects.Add(group.objects[i]);
					}
					else {
						foreach (DescriptionObject obj in group.objects)
							objects.Add(obj);
					}
				}
			}
			public void GenerateControls(Panel panel, Point location) {
				foreach (DescriptionObject obj in objects)
					location = obj.GenerateControl(panel, location);
			}

		}

		//Process formatting marker
		private FormatSettings ProcessFormatMarker(string text, FormatSettings settings) {
			if (string.IsNullOrWhiteSpace(text))
				return settings;
			string[] splits = text.Split('|');
			if (splits.Length < 3) {
				if (splits.Length == 1) {
					if (splits[0].ToLower().Equals("b"))
						settings.style |= FontStyle.Bold;
					else if (splits[0].ToLower().Equals("/b"))
						settings.style &= ~FontStyle.Bold;
					else if (splits[0].ToLower().Equals("i"))
						settings.style |= FontStyle.Italic;
					else if (splits[0].ToLower().Equals("/i"))
						settings.style &= ~FontStyle.Italic;
					else if (splits[0].ToLower().Equals("u"))
						settings.style |= FontStyle.Underline;
					else if (splits[0].ToLower().Equals("/u"))
						settings.style &= ~FontStyle.Underline;
					else if (splits[0].ToLower().Equals("/c"))
						settings.color = null;
					else if (splits[0].ToLower().Equals("/tt"))
						settings.tooltip = null;
					else if (splits[0].ToLower().Equals("/ct"))
						settings.printid = null;
				}
				else if (splits.Length == 2) {
					if (splits[0].ToLower().Equals("c")) {
						Color? color = Utils.GetColorFromString(splits[1]);
						if (color != null)
							settings.color = color;
					}
					else if (splits[0].ToLower().Equals("tt"))
						settings.tooltip = splits[1];
					else if (splits[0].ToLower().Equals("ct"))
						settings.printid = splits[1];
				}
			}
			return settings;
		}

		//Generate description text in panel at location. Returns total height of the description field
		private int GenerateDescription(string description, Panel panel, Point location) {

			//Return if nothing to display
			if (string.IsNullOrWhiteSpace(description))
				return 0;

			//Max two linebreaks in a row
			while (description.Contains("\r\n\r\n\r\n"))
				description = description.Replace("\r\n\r\n\r\n", "\r\n\r\n");

			//Starting height
			int startHeight = location.Y;

			//Format settings
			FormatSettings settings = new FormatSettings();

			//Loop blocks
			int curIdx, searchIdx;
			bool firstBlock = true, firstLine = true;
			foreach (string block in Utils.SplitString(description, "\r\n\r\n")) {

				//Spacing
				if (firstBlock)
					firstBlock = false;
				else {
					firstLine = true;
					location.Y += BLOCK_SPACING;
				}

				//Loop lines
				foreach (string line in Utils.SplitString(block, "\r\n")) {

					//Get text
					string text = line;

					//Clear whitespace
					if (string.IsNullOrWhiteSpace(line))
						continue;
					while (text.StartsWith(" "))
						text = text.Substring(1);
					while (text.EndsWith(" "))
						text = text.Substring(0, text.Length - 1);

					//Spacing
					if (firstLine)
						firstLine = false;
					else
						location.Y += LINE_SPACING;

					//Break line into words
					curIdx = 0;
					bool objectFound = false;
					List<DescriptionGroup> groups = new List<DescriptionGroup>();
					DescriptionGroup group = new DescriptionGroup();
					while (curIdx < text.Length) {

						//Add symbols
						if (text[curIdx] == '{') {
							objectFound = true;
							searchIdx = text.IndexOf('}', curIdx);
							if (searchIdx >= 0) {
								group.Add(new DescriptionSymbol(Catalog, text.Substring(curIdx, searchIdx + 1 - curIdx)));
								curIdx = searchIdx + 1;
							}
							else
								group.Add(new DescriptionText("{", settings));
						}

						//Process format markers
						else if (text[curIdx] == '<') {
							searchIdx = text.IndexOf('>', curIdx);
							if (searchIdx >= 0) {
								settings = ProcessFormatMarker(text.Substring(curIdx + 1, searchIdx - 1 - curIdx), settings);
								++settings.id;
								curIdx = searchIdx + 1;
							}
							else
								group.Add(new DescriptionText("<", settings));
						}

						//Process text
						else if (text[curIdx] != ' ') {
							objectFound = true;
							searchIdx = text.IndexOfAny(new char[] { '{', '<', ' ' }, curIdx);
							if (searchIdx < 0)
								searchIdx = text.Length;
							group.Add(new DescriptionText(text.Substring(curIdx, searchIdx - curIdx), settings));
							curIdx = searchIdx;
						}

						//Process spaces
						else {
							if (objectFound) {
								group.Clean();
								groups.Add(group);
								group = new DescriptionGroup();
								objectFound = false;
							}
							else {
								group.Add(new DescriptionText(" ", settings));
								++curIdx;
							}
						}

					}
					group.Clean();
					groups.Add(group);

					//Generate controls
					curIdx = 0;
					int maxWidth = panel.Width - (location.X + 5);
					group = new DescriptionGroup();
					for (int i = 0; i < groups.Count; ++i) {
						if (group.objects.Count == 0 || group.GetWidth() + groups[i].GetWidth() <= maxWidth)
							group.Merge(groups[i]);
						else {
							group.GenerateControls(panel, location);
							location.Y += TEXT_HEIGHT;
							group = new DescriptionGroup();
							group.Merge(groups[i]);
						}
					}
					group.GenerateControls(panel, location);
					location.Y += TEXT_HEIGHT;

				}

			}

			//Return height
			return location.Y - startHeight;

		}

		#endregion

	}

}
