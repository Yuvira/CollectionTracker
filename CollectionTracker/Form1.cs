using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CollectionTracker {
	public partial class Form1 : Form {

		//Initialize
		public Form1() {
			InitializeComponent();
			ResizeBegin += new EventHandler((sender, e) => OnResizeBegin());
			ResizeEnd += new EventHandler((sender, e) => OnResizeEnd());
			YGO_Initialize();
			MTG_Initialize();
			Dictionary<int, int> dict = new Dictionary<int, int>();
		}

		//Suspend layouts during resize
		private void OnResizeBegin() { Controls.Remove(formTabControl); }
		private void OnResizeEnd() { Controls.Add(formTabControl); }

		//Get first index of a subset of symbols. Returns -1 if there are no instances of any of the provided symbols
		private int IndexOfMany(string str, List<char> chars) {
			int index = -1;
			foreach (char c in chars) {
				int i = str.IndexOf(c);
				if (i != -1)
					if (index == -1 || i < index)
						index = i;
			}
			return index;
		}

	}
}
