using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CollectionTracker {
	public partial class Form1 : Form {

		//Constants
		public const int TEXT_HEIGHT = 21;
		public const int TEXT_MARGIN = 8;
		public const int LINE_SPACING = 9;
		public const int LEFT_PAD = 8;
		public const int TOP_PAD = 8;
		public const int BOTTOM_PAD = 8;

		//Initialize
		public Form1() {
			InitializeComponent();
			ResizeBegin += new EventHandler((sender, e) => OnResizeBegin());
			ResizeEnd += new EventHandler((sender, e) => OnResizeEnd());
			KeyDown += OnKeyDown;
			YGO_Initialize();
			MTG_Initialize();
			PKMN_Initialize();
			Dictionary<int, int> dict = new Dictionary<int, int>();
			KeyPreview = true;
		}

		//Suspend layouts during resize
		private void OnResizeBegin() { Controls.Remove(formTabControl); }
		private void OnResizeEnd() { Controls.Add(formTabControl); }

		//Detail page key inputs
		private void OnKeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
				return;
			if (formTabControl.SelectedTab == mtgPage && mtgTabControl.SelectedTab == mtgDetailPage) {
				if (e.KeyCode == Keys.Left)
					MTG_LoadPreviousInSelection();
				else if (e.KeyCode == Keys.Right)
					MTG_LoadNextInSelection();
				else if (e.KeyCode == Keys.Up && mtgMoveField.SelectedIndex > 0)
					--mtgMoveField.SelectedIndex;
				else if (e.KeyCode == Keys.Down && mtgMoveField.SelectedIndex < mtgMoveField.Items.Count - 1)
					++mtgMoveField.SelectedIndex;
				e.SuppressKeyPress = true;
			}
			else if (formTabControl.SelectedTab == ygoPage && ygoTabControl.SelectedTab == ygoDetailPage) {
				if (e.KeyCode == Keys.Left)
					YGO_LoadPreviousInSelection();
				else if (e.KeyCode == Keys.Right)
					YGO_LoadNextInSelection();
				else if (e.KeyCode == Keys.Up && ygoMoveField.SelectedIndex > 0)
					--ygoMoveField.SelectedIndex;
				else if (e.KeyCode == Keys.Down && ygoMoveField.SelectedIndex < ygoMoveField.Items.Count - 1)
					++ygoMoveField.SelectedIndex;
				e.SuppressKeyPress = true;
			}
			else if (formTabControl.SelectedTab == pkmnPage && pkmnTabControl.SelectedTab == pkmnDetailPage) {
				if (e.KeyCode == Keys.Left)
					PKMN_LoadPreviousInSelection();
				else if (e.KeyCode == Keys.Right)
					PKMN_LoadNextInSelection();
				else if (e.KeyCode == Keys.Up && pkmnMoveField.SelectedIndex > 0)
					--pkmnMoveField.SelectedIndex;
				else if (e.KeyCode == Keys.Down && pkmnMoveField.SelectedIndex < pkmnMoveField.Items.Count - 1)
					++pkmnMoveField.SelectedIndex;
				e.SuppressKeyPress = true;
			}
		}

	}
}
