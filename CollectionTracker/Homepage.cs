using System.Drawing;
using System.Windows.Forms;

namespace CollectionTracker {

	//Homepage
	public class Homepage : TrackerPage {

		//Constructor
		public Homepage(TrackerForm form) : base(form) { }

		//Initializer
		public override void Init() {

			//Catalog selectors
			Panel catalogPanel = Utils.GeneratePanel(Utils.CenterRect(new Size(382, 42), panel.Size));
			catalogPanel.Anchor = AnchorStyles.None;

			//Magic button
			RadioButton mtgButton = Utils.GenerateRadioButton(new Rectangle(5, 5, 120, 30), "Magic");
			catalogPanel.Controls.Add(mtgButton);

			//Yu-Gi-Oh! button
			RadioButton ygoButton = Utils.GenerateRadioButton(new Rectangle(130, 5, 120, 30), "Yu-Gi-Oh!");
			catalogPanel.Controls.Add(ygoButton);

			//Pokémon button
			RadioButton pkmnButton = Utils.GenerateRadioButton(new Rectangle(255, 5, 120, 30), "Pokémon");
			catalogPanel.Controls.Add(pkmnButton);

			//Add to panel
			panel.Controls.Add(catalogPanel);

		}

	}

}
