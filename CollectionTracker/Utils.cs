using System.Drawing;

namespace CollectionTracker {

	//Global utilities
	public static class Utils {

		//Font style references
		public static readonly Font FONT_DEFAULT = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
		public static readonly Font FONT_BOLD = new Font(FONT_DEFAULT, FontStyle.Bold);
		public static readonly Font FONT_ITALIC = new Font(FONT_DEFAULT, FontStyle.Italic);
		public static readonly Font FONT_UNDERLINE = new Font(FONT_DEFAULT, FontStyle.Underline);

	}

}
