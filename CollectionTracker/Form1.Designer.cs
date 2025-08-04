using System;
using System.Drawing;
using System.Windows.Forms;

namespace CollectionTracker {
	partial class Form1 {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.ygoNameLabel = new System.Windows.Forms.Label();
			this.ygoTabControl = new System.Windows.Forms.TabControl();
			this.ygoSetPage = new System.Windows.Forms.TabPage();
			this.ygoSetPageLabel = new System.Windows.Forms.Label();
			this.ygoNextSetButton = new System.Windows.Forms.Button();
			this.ygoPrevSetButton = new System.Windows.Forms.Button();
			this.ygoSetlistLabel = new System.Windows.Forms.Label();
			this.ygoSetlistLayout = new System.Windows.Forms.FlowLayoutPanel();
			this.ygoSearchPage = new System.Windows.Forms.TabPage();
			this.ygoSearchLocationField = new System.Windows.Forms.ComboBox();
			this.ygoSearchSetField = new System.Windows.Forms.ComboBox();
			this.ygoSearchOracleField = new System.Windows.Forms.TextBox();
			this.ygoSearchTypesField = new System.Windows.Forms.TextBox();
			this.ygoSearchPropertyField = new System.Windows.Forms.TextBox();
			this.ygoSearchAttributeField = new System.Windows.Forms.TextBox();
			this.ygoSearchCardTypeField = new System.Windows.Forms.TextBox();
			this.ygoSearchNameField = new System.Windows.Forms.TextBox();
			this.ygoSearchButton = new System.Windows.Forms.Button();
			this.ygoSearchSetLabel = new System.Windows.Forms.Label();
			this.ygoSearchLocationLabel = new System.Windows.Forms.Label();
			this.ygoSearchNameLabel = new System.Windows.Forms.Label();
			this.ygoSearchCardTypeLabel = new System.Windows.Forms.Label();
			this.ygoSearchTypesLabel = new System.Windows.Forms.Label();
			this.ygoSearchPropertyLabel = new System.Windows.Forms.Label();
			this.ygoSearchOracleLabel = new System.Windows.Forms.Label();
			this.ygoSearchAttributeLabel = new System.Windows.Forms.Label();
			this.ygoCatalogPage = new System.Windows.Forms.TabPage();
			this.ygoSortAlphabeticalButton = new System.Windows.Forms.RadioButton();
			this.ygoSortNumericButton = new System.Windows.Forms.RadioButton();
			this.ygoCatalogNextButton = new System.Windows.Forms.Button();
			this.ygoCatalogPrevButton = new System.Windows.Forms.Button();
			this.ygoCatalogLayout = new System.Windows.Forms.FlowLayoutPanel();
			this.ygoCatalogIndex = new System.Windows.Forms.Label();
			this.ygoDetailPage = new System.Windows.Forms.TabPage();
			this.ygoDetailFilterCountLabel = new System.Windows.Forms.Label();
			this.ygoPrintingsBox = new System.Windows.Forms.GroupBox();
			this.ygoDeletePrintingButton = new System.Windows.Forms.Button();
			this.ygoDetailDialog = new System.Windows.Forms.Label();
			this.ygoCardtipBox = new System.Windows.Forms.GroupBox();
			this.ygoCardtipImage = new System.Windows.Forms.PictureBox();
			this.ygoTooltipBox = new System.Windows.Forms.GroupBox();
			this.ygoDetailNextButton = new System.Windows.Forms.Button();
			this.ygoDetailAutogenButton = new System.Windows.Forms.Button();
			this.ygoDetailPrevButton = new System.Windows.Forms.Button();
			this.ygoDetailBox = new System.Windows.Forms.GroupBox();
			this.ygoReloadLocationsButton = new System.Windows.Forms.Button();
			this.ygoMoveField = new System.Windows.Forms.ComboBox();
			this.ygoEditPrintButton = new System.Windows.Forms.Button();
			this.ygoEditCardButton = new System.Windows.Forms.Button();
			this.ygoMoveLabel = new System.Windows.Forms.Label();
			this.ygoLocationTable = new System.Windows.Forms.TableLayoutPanel();
			this.ygoDetailImgbox = new System.Windows.Forms.PictureBox();
			this.ygoCardPage = new System.Windows.Forms.TabPage();
			this.ygoImportButton = new System.Windows.Forms.Button();
			this.ygoImportField = new System.Windows.Forms.TextBox();
			this.ygoScaleField = new System.Windows.Forms.NumericUpDown();
			this.ygoLevelField = new System.Windows.Forms.NumericUpDown();
			this.ygoDefField = new System.Windows.Forms.NumericUpDown();
			this.ygoAtkField = new System.Windows.Forms.NumericUpDown();
			this.ygoCardDialog = new System.Windows.Forms.Label();
			this.ygoIgnoreDuplicateEntryLabel = new System.Windows.Forms.Label();
			this.ygoIgnoreDuplicateEntryBox = new System.Windows.Forms.CheckBox();
			this.ygoAddCardButton = new System.Windows.Forms.Button();
			this.ygoScaleLabel = new System.Windows.Forms.Label();
			this.ygoAtkDefLabel = new System.Windows.Forms.Label();
			this.ygoLevelLabel = new System.Windows.Forms.Label();
			this.ygoOracleField = new System.Windows.Forms.TextBox();
			this.ygoCardTypeField = new System.Windows.Forms.TextBox();
			this.ygoAttributeField = new System.Windows.Forms.TextBox();
			this.ygoPropertyField = new System.Windows.Forms.TextBox();
			this.ygoTypesField = new System.Windows.Forms.TextBox();
			this.ygoNameField = new System.Windows.Forms.TextBox();
			this.ygoOracleLabel = new System.Windows.Forms.Label();
			this.ygoTypesLabel = new System.Windows.Forms.Label();
			this.ygoPropertyLabel = new System.Windows.Forms.Label();
			this.ygoAttributeLabel = new System.Windows.Forms.Label();
			this.ygoCardTypeLabel = new System.Windows.Forms.Label();
			this.ygoPrintPage = new System.Windows.Forms.TabPage();
			this.ygoImgpathBackLabel = new System.Windows.Forms.Label();
			this.ygoPrintAutoLimit = new System.Windows.Forms.NumericUpDown();
			this.ygoPrintAutofillRangeButton = new System.Windows.Forms.Button();
			this.ygoPrintAutofillButton = new System.Windows.Forms.Button();
			this.ygoCardrefDescriptor = new System.Windows.Forms.Label();
			this.ygoPrintIDLabel = new System.Windows.Forms.Label();
			this.ygoPrintIDField = new System.Windows.Forms.TextBox();
			this.ygoPrintImgboxBack = new System.Windows.Forms.PictureBox();
			this.ygoImgsearchBackButton = new System.Windows.Forms.Button();
			this.ygoImgBackLabel = new System.Windows.Forms.Label();
			this.ygoIODialog = new System.Windows.Forms.Label();
			this.ygoSaveButton = new System.Windows.Forms.Button();
			this.ygoImgpathLabel = new System.Windows.Forms.Label();
			this.ygoRemoveRarityButton = new System.Windows.Forms.Button();
			this.ygoAddRarityButton = new System.Windows.Forms.Button();
			this.ygoPrintImgbox = new System.Windows.Forms.PictureBox();
			this.ygoAddPrintButton = new System.Windows.Forms.Button();
			this.ygoCardrefField = new System.Windows.Forms.ComboBox();
			this.ygoCardrefLabel = new System.Windows.Forms.Label();
			this.ygoImgsearchButton = new System.Windows.Forms.Button();
			this.ygoImageLabel = new System.Windows.Forms.Label();
			this.ygoFlavorField = new System.Windows.Forms.TextBox();
			this.ygoFlavorLabel = new System.Windows.Forms.Label();
			this.ygoRaritiesValue = new System.Windows.Forms.Label();
			this.ygoRaritiesField = new System.Windows.Forms.ComboBox();
			this.ygoRaritiesLabel = new System.Windows.Forms.Label();
			this.ygoNumberField = new System.Windows.Forms.NumericUpDown();
			this.ygoNumberLabel = new System.Windows.Forms.Label();
			this.ygoSetField = new System.Windows.Forms.ComboBox();
			this.ygoSetLabel = new System.Windows.Forms.Label();
			this.ygoSetsPage = new System.Windows.Forms.TabPage();
			this.ygoSetGeneratorPageLabel = new System.Windows.Forms.Label();
			this.ygoSetGeneratorNext = new System.Windows.Forms.Button();
			this.ygoSetGeneratorPrev = new System.Windows.Forms.Button();
			this.ygoSaveSetsButton = new System.Windows.Forms.Button();
			this.ygoAddSetButton = new System.Windows.Forms.Button();
			this.ygoReloadSetsButton = new System.Windows.Forms.Button();
			this.ygoSetGeneratorLayout = new System.Windows.Forms.FlowLayoutPanel();
			this.ygoSetGeneratorLabel = new System.Windows.Forms.Label();
			this.imageFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.formTabControl = new System.Windows.Forms.TabControl();
			this.mtgPage = new System.Windows.Forms.TabPage();
			this.mtgTabControl = new System.Windows.Forms.TabControl();
			this.mtgSetPage = new System.Windows.Forms.TabPage();
			this.mtgPrevSetButton = new System.Windows.Forms.Button();
			this.mtgSetPageLabel = new System.Windows.Forms.Label();
			this.mtgNextSetButton = new System.Windows.Forms.Button();
			this.mtgSetlistLabel = new System.Windows.Forms.Label();
			this.mtgSetLayout = new System.Windows.Forms.FlowLayoutPanel();
			this.mtgSearchPage = new System.Windows.Forms.TabPage();
			this.mtgSearchSetField = new System.Windows.Forms.ComboBox();
			this.mtgSearchSetLabel = new System.Windows.Forms.Label();
			this.mtgSearchLocationField = new System.Windows.Forms.ComboBox();
			this.mtgSearchLocationLabel = new System.Windows.Forms.Label();
			this.mtgSearchButton = new System.Windows.Forms.Button();
			this.mtgSearchOracleField = new System.Windows.Forms.TextBox();
			this.mtgSearchTypeField = new System.Windows.Forms.TextBox();
			this.mtgSearchColImgG = new System.Windows.Forms.PictureBox();
			this.mtgSearchColImgR = new System.Windows.Forms.PictureBox();
			this.mtgSearchColImgB = new System.Windows.Forms.PictureBox();
			this.mtgSearchColImgU = new System.Windows.Forms.PictureBox();
			this.mtgSearchColImgW = new System.Windows.Forms.PictureBox();
			this.mtgSearchColG = new System.Windows.Forms.CheckBox();
			this.mtgSearchColR = new System.Windows.Forms.CheckBox();
			this.mtgSearchColB = new System.Windows.Forms.CheckBox();
			this.mtgSearchColU = new System.Windows.Forms.CheckBox();
			this.mtgSearchColW = new System.Windows.Forms.CheckBox();
			this.mtgSearchIDImgG = new System.Windows.Forms.PictureBox();
			this.mtgSearchIDImgR = new System.Windows.Forms.PictureBox();
			this.mtgSearchIDImgB = new System.Windows.Forms.PictureBox();
			this.mtgSearchIDImgU = new System.Windows.Forms.PictureBox();
			this.mtgSearchIDImgW = new System.Windows.Forms.PictureBox();
			this.mtgSearchIDG = new System.Windows.Forms.CheckBox();
			this.mtgSearchIDR = new System.Windows.Forms.CheckBox();
			this.mtgSearchIDB = new System.Windows.Forms.CheckBox();
			this.mtgSearchIDU = new System.Windows.Forms.CheckBox();
			this.mtgSearchIDW = new System.Windows.Forms.CheckBox();
			this.mtgSearchNameLabel = new System.Windows.Forms.Label();
			this.mtgSearchNameField = new System.Windows.Forms.TextBox();
			this.mtgSearchOracleLabel = new System.Windows.Forms.Label();
			this.mtgSearchIdentityLabel = new System.Windows.Forms.Label();
			this.mtgSearchColourLabel = new System.Windows.Forms.Label();
			this.mtgSearchTypeLabel = new System.Windows.Forms.Label();
			this.mtgCatalogPage = new System.Windows.Forms.TabPage();
			this.mtgSortAlphabeticalButton = new System.Windows.Forms.RadioButton();
			this.mtgSortNumericButton = new System.Windows.Forms.RadioButton();
			this.mtgCatalogNextButton = new System.Windows.Forms.Button();
			this.mtgCatalogPrevButton = new System.Windows.Forms.Button();
			this.mtgCatalogLayout = new System.Windows.Forms.FlowLayoutPanel();
			this.mtgCatalogIndex = new System.Windows.Forms.Label();
			this.mtgDetailPage = new System.Windows.Forms.TabPage();
			this.mtgDetailFilterCountLabel = new System.Windows.Forms.Label();
			this.mtgPrintingsBox = new System.Windows.Forms.GroupBox();
			this.mtgDetailDialog = new System.Windows.Forms.Label();
			this.mtgDetailAutogenButton = new System.Windows.Forms.Button();
			this.mtgDeletePrintingButton = new System.Windows.Forms.Button();
			this.mtgDetailNextButton = new System.Windows.Forms.Button();
			this.mtgDetailPrevButton = new System.Windows.Forms.Button();
			this.mtgCardtipBox = new System.Windows.Forms.GroupBox();
			this.mtgCardtipImage = new System.Windows.Forms.PictureBox();
			this.mtgTooltipBox = new System.Windows.Forms.GroupBox();
			this.mtgDetailBox = new System.Windows.Forms.GroupBox();
			this.mtgReloadLocationsButton = new System.Windows.Forms.Button();
			this.mtgEditPrintButton = new System.Windows.Forms.Button();
			this.mtgEditCardButton = new System.Windows.Forms.Button();
			this.mtgMoveLabel = new System.Windows.Forms.Label();
			this.mtgLocationTable = new System.Windows.Forms.TableLayoutPanel();
			this.mtgMoveField = new System.Windows.Forms.ComboBox();
			this.mtgDetailImgbox = new System.Windows.Forms.PictureBox();
			this.mtgCardPage = new System.Windows.Forms.TabPage();
			this.mtgIgnoreDuplicateEntryLabel = new System.Windows.Forms.Label();
			this.mtgIgnoreDuplicateEntryBox = new System.Windows.Forms.CheckBox();
			this.mtgCardTypeField = new System.Windows.Forms.TextBox();
			this.mtgToughnessBackField = new System.Windows.Forms.NumericUpDown();
			this.mtgPowerBackField = new System.Windows.Forms.NumericUpDown();
			this.mtgAtkDefBackLabel = new System.Windows.Forms.Label();
			this.mtgCostField = new System.Windows.Forms.TextBox();
			this.mtgCostLabel = new System.Windows.Forms.Label();
			this.mtgColourImgG = new System.Windows.Forms.PictureBox();
			this.mtgColourImgR = new System.Windows.Forms.PictureBox();
			this.mtgColourImgB = new System.Windows.Forms.PictureBox();
			this.mtgColourImgU = new System.Windows.Forms.PictureBox();
			this.mtgColourImgW = new System.Windows.Forms.PictureBox();
			this.mtgColourG = new System.Windows.Forms.CheckBox();
			this.mtgColourR = new System.Windows.Forms.CheckBox();
			this.mtgColourB = new System.Windows.Forms.CheckBox();
			this.mtgColourU = new System.Windows.Forms.CheckBox();
			this.mtgColourW = new System.Windows.Forms.CheckBox();
			this.mtgIdentityImgG = new System.Windows.Forms.PictureBox();
			this.mtgIdentityImgR = new System.Windows.Forms.PictureBox();
			this.mtgIdentityImgB = new System.Windows.Forms.PictureBox();
			this.mtgIdentityImgU = new System.Windows.Forms.PictureBox();
			this.mtgIdentityImgW = new System.Windows.Forms.PictureBox();
			this.mtgIdentityG = new System.Windows.Forms.CheckBox();
			this.mtgIdentityR = new System.Windows.Forms.CheckBox();
			this.mtgIdentityB = new System.Windows.Forms.CheckBox();
			this.mtgIdentityU = new System.Windows.Forms.CheckBox();
			this.mtgIdentityW = new System.Windows.Forms.CheckBox();
			this.mtgCardDialog = new System.Windows.Forms.Label();
			this.mtgToughnessField = new System.Windows.Forms.NumericUpDown();
			this.mtgPowerField = new System.Windows.Forms.NumericUpDown();
			this.mtgAtkDefLabel = new System.Windows.Forms.Label();
			this.mtgAddCardButton = new System.Windows.Forms.Button();
			this.mtgNameLabel = new System.Windows.Forms.Label();
			this.mtgNameField = new System.Windows.Forms.TextBox();
			this.mtgOracleTextLabel = new System.Windows.Forms.Label();
			this.mtgIdentityLabel = new System.Windows.Forms.Label();
			this.mtgOracleTextField = new System.Windows.Forms.TextBox();
			this.mtgColourLabel = new System.Windows.Forms.Label();
			this.mtgCardTypeLabel = new System.Windows.Forms.Label();
			this.mtgPrintPage = new System.Windows.Forms.TabPage();
			this.mtgPrintTokenCheck = new System.Windows.Forms.CheckBox();
			this.mtgPrintAutoLimit = new System.Windows.Forms.NumericUpDown();
			this.mtgPrintAutofillRangeButton = new System.Windows.Forms.Button();
			this.mtgPrintAutofillButton = new System.Windows.Forms.Button();
			this.mtgCardrefDescriptor = new System.Windows.Forms.Label();
			this.mtgRarityField = new System.Windows.Forms.ComboBox();
			this.mtgRarityLabel = new System.Windows.Forms.Label();
			this.mtgScryfallLabel = new System.Windows.Forms.Label();
			this.mtgScryfallField = new System.Windows.Forms.TextBox();
			this.mtgImgpathBackLabel = new System.Windows.Forms.Label();
			this.mtgPrintImgboxBack = new System.Windows.Forms.PictureBox();
			this.mtgImgsearchBackButton = new System.Windows.Forms.Button();
			this.mtgImgsearchBackLabel = new System.Windows.Forms.Label();
			this.mtgIODialog = new System.Windows.Forms.Label();
			this.mtgSaveButton = new System.Windows.Forms.Button();
			this.mtgImgpathLabel = new System.Windows.Forms.Label();
			this.mtgSubTreatmentButton = new System.Windows.Forms.Button();
			this.mtgAddTreatmentButton = new System.Windows.Forms.Button();
			this.mtgPrintImgbox = new System.Windows.Forms.PictureBox();
			this.mtgAddPrintButton = new System.Windows.Forms.Button();
			this.mtgCardrefField = new System.Windows.Forms.ComboBox();
			this.mtgCardrefLabel = new System.Windows.Forms.Label();
			this.mtgImgsearchButton = new System.Windows.Forms.Button();
			this.mtgImgsearchLabel = new System.Windows.Forms.Label();
			this.mtgFlavorTextField = new System.Windows.Forms.TextBox();
			this.mtgFlavorTextLabel = new System.Windows.Forms.Label();
			this.mtgTreatmentsValue = new System.Windows.Forms.Label();
			this.mtgTreatmentField = new System.Windows.Forms.ComboBox();
			this.mtgTreatmentLabel = new System.Windows.Forms.Label();
			this.mtgNumberField = new System.Windows.Forms.NumericUpDown();
			this.mtgNumberLabel = new System.Windows.Forms.Label();
			this.mtgSetField = new System.Windows.Forms.ComboBox();
			this.mtgSetLabel = new System.Windows.Forms.Label();
			this.mtgSymbolsPage = new System.Windows.Forms.TabPage();
			this.mtgSymbolLayout = new System.Windows.Forms.FlowLayoutPanel();
			this.mtgSymbolHeaderBox = new System.Windows.Forms.GroupBox();
			this.mtgSaveSymbolsButton = new System.Windows.Forms.Button();
			this.mtgAddSymbolButton = new System.Windows.Forms.Button();
			this.mtgSymbolLabel = new System.Windows.Forms.Label();
			this.mtgSetsPage = new System.Windows.Forms.TabPage();
			this.mtgReloadSetsButton = new System.Windows.Forms.Button();
			this.mtgSetGeneratorPrev = new System.Windows.Forms.Button();
			this.mtgAddSetButton = new System.Windows.Forms.Button();
			this.mtgSaveSetsButton = new System.Windows.Forms.Button();
			this.mtgSetGeneratorNext = new System.Windows.Forms.Button();
			this.mtgSetGeneratorPageLabel = new System.Windows.Forms.Label();
			this.mtgSetGeneratorLayout = new System.Windows.Forms.FlowLayoutPanel();
			this.mtgSetGeneratorLabel = new System.Windows.Forms.Label();
			this.ygoPage = new System.Windows.Forms.TabPage();
			this.pkmnPage = new System.Windows.Forms.TabPage();
			this.pkmnTabControl = new System.Windows.Forms.TabControl();
			this.pkmnSetlistPage = new System.Windows.Forms.TabPage();
			this.pkmnPrevSetButton = new System.Windows.Forms.Button();
			this.pkmnSetPageLabel = new System.Windows.Forms.Label();
			this.pkmnNextSetButton = new System.Windows.Forms.Button();
			this.pkmnSetlistLabel = new System.Windows.Forms.Label();
			this.pkmnSetlistLayout = new System.Windows.Forms.FlowLayoutPanel();
			this.pkmnSearchPage = new System.Windows.Forms.TabPage();
			this.pkmnClipboardLVXButton = new System.Windows.Forms.Button();
			this.pkmnClipboardSPButton = new System.Windows.Forms.Button();
			this.pkmnSearchDialog = new System.Windows.Forms.Label();
			this.pkmnApplySearchTermsButton = new System.Windows.Forms.Button();
			this.pkmnReloadSearchListsButton = new System.Windows.Forms.Button();
			this.pkmnSearchTypeList = new System.Windows.Forms.ComboBox();
			this.pkmnSearchField = new System.Windows.Forms.TextBox();
			this.pkmnSearchLabel = new System.Windows.Forms.Label();
			this.pkmnClipboardPrismButton = new System.Windows.Forms.Button();
			this.pkmnClipboardDeltaButton = new System.Windows.Forms.Button();
			this.pkmnClipboardStarButton = new System.Windows.Forms.Button();
			this.pkmnClipboardexButton = new System.Windows.Forms.Button();
			this.pkmnSearchSetField = new System.Windows.Forms.ComboBox();
			this.pkmnSearchSetLabel = new System.Windows.Forms.Label();
			this.pkmnSearchLocationField = new System.Windows.Forms.ComboBox();
			this.pkmnSearchLocationLabel = new System.Windows.Forms.Label();
			this.pkmnSearchButton = new System.Windows.Forms.Button();
			this.pkmnSearchOracleField = new System.Windows.Forms.TextBox();
			this.pkmnSearchTypeField = new System.Windows.Forms.TextBox();
			this.pkmnSearchNameLabel = new System.Windows.Forms.Label();
			this.pkmnSearchNameField = new System.Windows.Forms.TextBox();
			this.pkmnSearchOracleLabel = new System.Windows.Forms.Label();
			this.pkmnSearchTypeLabel = new System.Windows.Forms.Label();
			this.pkmnCatalogPage = new System.Windows.Forms.TabPage();
			this.pkmnSortAlphabeticalButton = new System.Windows.Forms.RadioButton();
			this.pkmnSortNumericButton = new System.Windows.Forms.RadioButton();
			this.pkmnCatalogNextButton = new System.Windows.Forms.Button();
			this.pkmnCatalogPrevButton = new System.Windows.Forms.Button();
			this.pkmnCatalogLayout = new System.Windows.Forms.FlowLayoutPanel();
			this.pkmnCatalogIndex = new System.Windows.Forms.Label();
			this.pkmnDetailPage = new System.Windows.Forms.TabPage();
			this.pkmnDetailFilterCountLabel = new System.Windows.Forms.Label();
			this.pkmnPrintingsPanel = new System.Windows.Forms.Panel();
			this.pkmnDetailDialog = new System.Windows.Forms.Label();
			this.pkmnDetailAutogenButton = new System.Windows.Forms.Button();
			this.pkmnDeletePrintingButton = new System.Windows.Forms.Button();
			this.pkmnDetailNextButton = new System.Windows.Forms.Button();
			this.pkmnDetailPrevButton = new System.Windows.Forms.Button();
			this.pkmnCardtipPanel = new System.Windows.Forms.Panel();
			this.pkmnCardtipImage = new System.Windows.Forms.PictureBox();
			this.pkmnTooltipPanel = new System.Windows.Forms.Panel();
			this.pkmnDetailPanel = new System.Windows.Forms.Panel();
			this.pkmnOwnedPrintingsLabel = new System.Windows.Forms.Label();
			this.pkmnReloadLocationsButton = new System.Windows.Forms.Button();
			this.pkmnEditPrintButton = new System.Windows.Forms.Button();
			this.pkmnEditCardButton = new System.Windows.Forms.Button();
			this.pkmnMoveLabel = new System.Windows.Forms.Label();
			this.pkmnLocationTable = new System.Windows.Forms.TableLayoutPanel();
			this.pkmnMoveField = new System.Windows.Forms.ComboBox();
			this.pkmnDetailImgbox = new System.Windows.Forms.PictureBox();
			this.pkmnCardPage = new System.Windows.Forms.TabPage();
			this.pkmnPokemonButton = new System.Windows.Forms.Button();
			this.pkmnTrainerButton = new System.Windows.Forms.Button();
			this.pkmnImportButton = new System.Windows.Forms.Button();
			this.pkmnImportField = new System.Windows.Forms.TextBox();
			this.pkmnRetreatField = new System.Windows.Forms.TextBox();
			this.pkmnResistField = new System.Windows.Forms.TextBox();
			this.pkmnWeakField = new System.Windows.Forms.TextBox();
			this.pkmnRetreatLabel = new System.Windows.Forms.Label();
			this.pkmnWeakLabel = new System.Windows.Forms.Label();
			this.pkmnStageLabel = new System.Windows.Forms.Label();
			this.pkmnStageField = new System.Windows.Forms.TextBox();
			this.pkmnIgnorDuplicateEntryLabel = new System.Windows.Forms.Label();
			this.pkmnIgnorDuplicateEntryBox = new System.Windows.Forms.CheckBox();
			this.pkmnTypeField = new System.Windows.Forms.TextBox();
			this.pkmnResistLabel = new System.Windows.Forms.Label();
			this.pkmnETypeField = new System.Windows.Forms.TextBox();
			this.pkmnETypeLabel = new System.Windows.Forms.Label();
			this.pkmnCardDialog = new System.Windows.Forms.Label();
			this.pkmnHPField = new System.Windows.Forms.NumericUpDown();
			this.pkmnHPLabel = new System.Windows.Forms.Label();
			this.pkmnAddCardButton = new System.Windows.Forms.Button();
			this.pkmnNameLabel = new System.Windows.Forms.Label();
			this.pkmnNameField = new System.Windows.Forms.TextBox();
			this.pkmnOracleLabel = new System.Windows.Forms.Label();
			this.pkmnOracleField = new System.Windows.Forms.TextBox();
			this.pkmnTypeLabel = new System.Windows.Forms.Label();
			this.pkmnPrintPage = new System.Windows.Forms.TabPage();
			this.pkmnPrintHoloButton = new System.Windows.Forms.Button();
			this.pkmnPrintNormalButton = new System.Windows.Forms.Button();
			this.pkmnPrintAutoLimit = new System.Windows.Forms.NumericUpDown();
			this.pkmnPrintAutofillRangeButton = new System.Windows.Forms.Button();
			this.pkmnPrintAutofillButton = new System.Windows.Forms.Button();
			this.pkmnCardrefDescriptor = new System.Windows.Forms.Label();
			this.pkmnRarityField = new System.Windows.Forms.ComboBox();
			this.pkmnRarityLabel = new System.Windows.Forms.Label();
			this.pkmnPrintIDLabel = new System.Windows.Forms.Label();
			this.pkmnPrintIDField = new System.Windows.Forms.TextBox();
			this.pkmnImgpathBackLabel = new System.Windows.Forms.Label();
			this.pkmnPrintImgboxBack = new System.Windows.Forms.PictureBox();
			this.pkmnImgsearchBackButton = new System.Windows.Forms.Button();
			this.pkmnImgBackLabel = new System.Windows.Forms.Label();
			this.pkmnIODialog = new System.Windows.Forms.Label();
			this.pkmnSaveButton = new System.Windows.Forms.Button();
			this.pkmnImgpathLabel = new System.Windows.Forms.Label();
			this.pkmnRemTreatmentButton = new System.Windows.Forms.Button();
			this.pkmnAddTreatmentButton = new System.Windows.Forms.Button();
			this.pkmnPrintImgbox = new System.Windows.Forms.PictureBox();
			this.pkmnAddPrintButton = new System.Windows.Forms.Button();
			this.pkmnCardrefField = new System.Windows.Forms.ComboBox();
			this.pkmnCardrefLabel = new System.Windows.Forms.Label();
			this.pkmnImgsearchButton = new System.Windows.Forms.Button();
			this.pkmnImgLabel = new System.Windows.Forms.Label();
			this.pkmnFlavorField = new System.Windows.Forms.TextBox();
			this.pkmnFlavorLabel = new System.Windows.Forms.Label();
			this.pkmnTreatmentsValue = new System.Windows.Forms.Label();
			this.pkmnTreatmentField = new System.Windows.Forms.ComboBox();
			this.pkmnTreatmentLabel = new System.Windows.Forms.Label();
			this.pkmnNumberField = new System.Windows.Forms.NumericUpDown();
			this.pkmnNumberLabel = new System.Windows.Forms.Label();
			this.pkmnSetField = new System.Windows.Forms.ComboBox();
			this.pkmnSetLabel = new System.Windows.Forms.Label();
			this.pkmnSymbolPage = new System.Windows.Forms.TabPage();
			this.pkmnSymbolLayout = new System.Windows.Forms.FlowLayoutPanel();
			this.pkmnSymbolHeaderPanel = new System.Windows.Forms.Panel();
			this.pkmnSaveSymbolsButton = new System.Windows.Forms.Button();
			this.pkmnAddSymbolButton = new System.Windows.Forms.Button();
			this.pkmnSymbolLabel = new System.Windows.Forms.Label();
			this.pkmnSetPage = new System.Windows.Forms.TabPage();
			this.pkmnReloadSetsButton = new System.Windows.Forms.Button();
			this.pkmnSetGeneratorPrev = new System.Windows.Forms.Button();
			this.pkmnAddSetButton = new System.Windows.Forms.Button();
			this.pkmnSaveSetsButton = new System.Windows.Forms.Button();
			this.pkmnSetGeneratorNext = new System.Windows.Forms.Button();
			this.pkmnSetGeneratorPageLabel = new System.Windows.Forms.Label();
			this.pkmnSetGeneratorLayout = new System.Windows.Forms.FlowLayoutPanel();
			this.pkmnSetGeneratorLabel = new System.Windows.Forms.Label();
			this.ygoTabControl.SuspendLayout();
			this.ygoSetPage.SuspendLayout();
			this.ygoSearchPage.SuspendLayout();
			this.ygoCatalogPage.SuspendLayout();
			this.ygoDetailPage.SuspendLayout();
			this.ygoCardtipBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ygoCardtipImage)).BeginInit();
			this.ygoDetailBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ygoDetailImgbox)).BeginInit();
			this.ygoCardPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ygoScaleField)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ygoLevelField)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ygoDefField)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ygoAtkField)).BeginInit();
			this.ygoPrintPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ygoPrintAutoLimit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ygoPrintImgboxBack)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ygoPrintImgbox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ygoNumberField)).BeginInit();
			this.ygoSetsPage.SuspendLayout();
			this.formTabControl.SuspendLayout();
			this.mtgPage.SuspendLayout();
			this.mtgTabControl.SuspendLayout();
			this.mtgSetPage.SuspendLayout();
			this.mtgSearchPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchColImgG)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchColImgR)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchColImgB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchColImgU)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchColImgW)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchIDImgG)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchIDImgR)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchIDImgB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchIDImgU)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchIDImgW)).BeginInit();
			this.mtgCatalogPage.SuspendLayout();
			this.mtgDetailPage.SuspendLayout();
			this.mtgCardtipBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mtgCardtipImage)).BeginInit();
			this.mtgDetailBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mtgDetailImgbox)).BeginInit();
			this.mtgCardPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mtgToughnessBackField)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgPowerBackField)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgColourImgG)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgColourImgR)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgColourImgB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgColourImgU)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgColourImgW)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgIdentityImgG)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgIdentityImgR)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgIdentityImgB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgIdentityImgU)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgIdentityImgW)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgToughnessField)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgPowerField)).BeginInit();
			this.mtgPrintPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mtgPrintAutoLimit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgPrintImgboxBack)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgPrintImgbox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgNumberField)).BeginInit();
			this.mtgSymbolsPage.SuspendLayout();
			this.mtgSymbolLayout.SuspendLayout();
			this.mtgSymbolHeaderBox.SuspendLayout();
			this.mtgSetsPage.SuspendLayout();
			this.ygoPage.SuspendLayout();
			this.pkmnPage.SuspendLayout();
			this.pkmnTabControl.SuspendLayout();
			this.pkmnSetlistPage.SuspendLayout();
			this.pkmnSearchPage.SuspendLayout();
			this.pkmnCatalogPage.SuspendLayout();
			this.pkmnDetailPage.SuspendLayout();
			this.pkmnCardtipPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pkmnCardtipImage)).BeginInit();
			this.pkmnDetailPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pkmnDetailImgbox)).BeginInit();
			this.pkmnCardPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pkmnHPField)).BeginInit();
			this.pkmnPrintPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pkmnPrintAutoLimit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pkmnPrintImgboxBack)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pkmnPrintImgbox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pkmnNumberField)).BeginInit();
			this.pkmnSymbolPage.SuspendLayout();
			this.pkmnSymbolLayout.SuspendLayout();
			this.pkmnSymbolHeaderPanel.SuspendLayout();
			this.pkmnSetPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// ygoNameLabel
			// 
			this.ygoNameLabel.Location = new System.Drawing.Point(5, 5);
			this.ygoNameLabel.Name = "ygoNameLabel";
			this.ygoNameLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoNameLabel.TabIndex = 1;
			this.ygoNameLabel.Text = "Name:";
			this.ygoNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoTabControl
			// 
			this.ygoTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.ygoTabControl.Controls.Add(this.ygoSetPage);
			this.ygoTabControl.Controls.Add(this.ygoSearchPage);
			this.ygoTabControl.Controls.Add(this.ygoCatalogPage);
			this.ygoTabControl.Controls.Add(this.ygoDetailPage);
			this.ygoTabControl.Controls.Add(this.ygoCardPage);
			this.ygoTabControl.Controls.Add(this.ygoPrintPage);
			this.ygoTabControl.Controls.Add(this.ygoSetsPage);
			this.ygoTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ygoTabControl.Location = new System.Drawing.Point(3, 3);
			this.ygoTabControl.Name = "ygoTabControl";
			this.ygoTabControl.SelectedIndex = 0;
			this.ygoTabControl.Size = new System.Drawing.Size(1270, 703);
			this.ygoTabControl.TabIndex = 0;
			// 
			// ygoSetPage
			// 
			this.ygoSetPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ygoSetPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ygoSetPage.Controls.Add(this.ygoSetPageLabel);
			this.ygoSetPage.Controls.Add(this.ygoNextSetButton);
			this.ygoSetPage.Controls.Add(this.ygoPrevSetButton);
			this.ygoSetPage.Controls.Add(this.ygoSetlistLabel);
			this.ygoSetPage.Controls.Add(this.ygoSetlistLayout);
			this.ygoSetPage.Location = new System.Drawing.Point(4, 33);
			this.ygoSetPage.Name = "ygoSetPage";
			this.ygoSetPage.Padding = new System.Windows.Forms.Padding(3);
			this.ygoSetPage.Size = new System.Drawing.Size(1262, 666);
			this.ygoSetPage.TabIndex = 3;
			this.ygoSetPage.Text = "Set List";
			// 
			// ygoSetPageLabel
			// 
			this.ygoSetPageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ygoSetPageLabel.Location = new System.Drawing.Point(1125, 5);
			this.ygoSetPageLabel.Name = "ygoSetPageLabel";
			this.ygoSetPageLabel.Size = new System.Drawing.Size(60, 30);
			this.ygoSetPageLabel.TabIndex = 10;
			this.ygoSetPageLabel.Text = "X / X";
			this.ygoSetPageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ygoNextSetButton
			// 
			this.ygoNextSetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ygoNextSetButton.Location = new System.Drawing.Point(1190, 5);
			this.ygoNextSetButton.Name = "ygoNextSetButton";
			this.ygoNextSetButton.Size = new System.Drawing.Size(60, 29);
			this.ygoNextSetButton.TabIndex = 9;
			this.ygoNextSetButton.Text = ">";
			this.ygoNextSetButton.UseVisualStyleBackColor = true;
			this.ygoNextSetButton.Click += new System.EventHandler(this.YGO_OnClickNextSet);
			// 
			// ygoPrevSetButton
			// 
			this.ygoPrevSetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ygoPrevSetButton.Location = new System.Drawing.Point(1060, 5);
			this.ygoPrevSetButton.Name = "ygoPrevSetButton";
			this.ygoPrevSetButton.Size = new System.Drawing.Size(60, 29);
			this.ygoPrevSetButton.TabIndex = 8;
			this.ygoPrevSetButton.Text = "<";
			this.ygoPrevSetButton.UseVisualStyleBackColor = true;
			this.ygoPrevSetButton.Click += new System.EventHandler(this.YGO_OnClickPrevSet);
			// 
			// ygoSetlistLabel
			// 
			this.ygoSetlistLabel.AutoSize = true;
			this.ygoSetlistLabel.Location = new System.Drawing.Point(5, 8);
			this.ygoSetlistLabel.Name = "ygoSetlistLabel";
			this.ygoSetlistLabel.Size = new System.Drawing.Size(60, 21);
			this.ygoSetlistLabel.TabIndex = 2;
			this.ygoSetlistLabel.Text = "Set List";
			this.ygoSetlistLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoSetlistLayout
			// 
			this.ygoSetlistLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ygoSetlistLayout.AutoScroll = true;
			this.ygoSetlistLayout.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ygoSetlistLayout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ygoSetlistLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.ygoSetlistLayout.Location = new System.Drawing.Point(3, 38);
			this.ygoSetlistLayout.Name = "ygoSetlistLayout";
			this.ygoSetlistLayout.Size = new System.Drawing.Size(1252, 620);
			this.ygoSetlistLayout.TabIndex = 1;
			this.ygoSetlistLayout.WrapContents = false;
			// 
			// ygoSearchPage
			// 
			this.ygoSearchPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ygoSearchPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ygoSearchPage.Controls.Add(this.ygoSearchLocationField);
			this.ygoSearchPage.Controls.Add(this.ygoSearchSetField);
			this.ygoSearchPage.Controls.Add(this.ygoSearchOracleField);
			this.ygoSearchPage.Controls.Add(this.ygoSearchTypesField);
			this.ygoSearchPage.Controls.Add(this.ygoSearchPropertyField);
			this.ygoSearchPage.Controls.Add(this.ygoSearchAttributeField);
			this.ygoSearchPage.Controls.Add(this.ygoSearchCardTypeField);
			this.ygoSearchPage.Controls.Add(this.ygoSearchNameField);
			this.ygoSearchPage.Controls.Add(this.ygoSearchButton);
			this.ygoSearchPage.Controls.Add(this.ygoSearchSetLabel);
			this.ygoSearchPage.Controls.Add(this.ygoSearchLocationLabel);
			this.ygoSearchPage.Controls.Add(this.ygoSearchNameLabel);
			this.ygoSearchPage.Controls.Add(this.ygoSearchCardTypeLabel);
			this.ygoSearchPage.Controls.Add(this.ygoSearchTypesLabel);
			this.ygoSearchPage.Controls.Add(this.ygoSearchPropertyLabel);
			this.ygoSearchPage.Controls.Add(this.ygoSearchOracleLabel);
			this.ygoSearchPage.Controls.Add(this.ygoSearchAttributeLabel);
			this.ygoSearchPage.Location = new System.Drawing.Point(4, 25);
			this.ygoSearchPage.Name = "ygoSearchPage";
			this.ygoSearchPage.Size = new System.Drawing.Size(1262, 674);
			this.ygoSearchPage.TabIndex = 6;
			this.ygoSearchPage.Text = "Search";
			// 
			// ygoSearchLocationField
			// 
			this.ygoSearchLocationField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.ygoSearchLocationField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.ygoSearchLocationField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ygoSearchLocationField.FormattingEnabled = true;
			this.ygoSearchLocationField.Location = new System.Drawing.Point(100, 250);
			this.ygoSearchLocationField.Name = "ygoSearchLocationField";
			this.ygoSearchLocationField.Size = new System.Drawing.Size(370, 29);
			this.ygoSearchLocationField.TabIndex = 82;
			// 
			// ygoSearchSetField
			// 
			this.ygoSearchSetField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.ygoSearchSetField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.ygoSearchSetField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ygoSearchSetField.FormattingEnabled = true;
			this.ygoSearchSetField.Location = new System.Drawing.Point(100, 215);
			this.ygoSearchSetField.Name = "ygoSearchSetField";
			this.ygoSearchSetField.Size = new System.Drawing.Size(370, 29);
			this.ygoSearchSetField.Sorted = true;
			this.ygoSearchSetField.TabIndex = 81;
			// 
			// ygoSearchOracleField
			// 
			this.ygoSearchOracleField.Location = new System.Drawing.Point(100, 180);
			this.ygoSearchOracleField.Name = "ygoSearchOracleField";
			this.ygoSearchOracleField.Size = new System.Drawing.Size(370, 29);
			this.ygoSearchOracleField.TabIndex = 80;
			// 
			// ygoSearchTypesField
			// 
			this.ygoSearchTypesField.Location = new System.Drawing.Point(100, 145);
			this.ygoSearchTypesField.Name = "ygoSearchTypesField";
			this.ygoSearchTypesField.Size = new System.Drawing.Size(370, 29);
			this.ygoSearchTypesField.TabIndex = 79;
			// 
			// ygoSearchPropertyField
			// 
			this.ygoSearchPropertyField.Location = new System.Drawing.Point(100, 110);
			this.ygoSearchPropertyField.Name = "ygoSearchPropertyField";
			this.ygoSearchPropertyField.Size = new System.Drawing.Size(370, 29);
			this.ygoSearchPropertyField.TabIndex = 78;
			// 
			// ygoSearchAttributeField
			// 
			this.ygoSearchAttributeField.Location = new System.Drawing.Point(100, 75);
			this.ygoSearchAttributeField.Name = "ygoSearchAttributeField";
			this.ygoSearchAttributeField.Size = new System.Drawing.Size(370, 29);
			this.ygoSearchAttributeField.TabIndex = 77;
			// 
			// ygoSearchCardTypeField
			// 
			this.ygoSearchCardTypeField.Location = new System.Drawing.Point(100, 40);
			this.ygoSearchCardTypeField.Name = "ygoSearchCardTypeField";
			this.ygoSearchCardTypeField.Size = new System.Drawing.Size(370, 29);
			this.ygoSearchCardTypeField.TabIndex = 76;
			// 
			// ygoSearchNameField
			// 
			this.ygoSearchNameField.Location = new System.Drawing.Point(100, 5);
			this.ygoSearchNameField.Name = "ygoSearchNameField";
			this.ygoSearchNameField.Size = new System.Drawing.Size(370, 29);
			this.ygoSearchNameField.TabIndex = 75;
			// 
			// ygoSearchButton
			// 
			this.ygoSearchButton.Location = new System.Drawing.Point(100, 285);
			this.ygoSearchButton.Name = "ygoSearchButton";
			this.ygoSearchButton.Size = new System.Drawing.Size(120, 30);
			this.ygoSearchButton.TabIndex = 74;
			this.ygoSearchButton.Text = "Search";
			this.ygoSearchButton.UseVisualStyleBackColor = true;
			this.ygoSearchButton.Click += new System.EventHandler(this.YGO_Search);
			// 
			// ygoSearchSetLabel
			// 
			this.ygoSearchSetLabel.Location = new System.Drawing.Point(5, 215);
			this.ygoSearchSetLabel.Name = "ygoSearchSetLabel";
			this.ygoSearchSetLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoSearchSetLabel.TabIndex = 52;
			this.ygoSearchSetLabel.Text = "Set:";
			this.ygoSearchSetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoSearchLocationLabel
			// 
			this.ygoSearchLocationLabel.Location = new System.Drawing.Point(5, 250);
			this.ygoSearchLocationLabel.Name = "ygoSearchLocationLabel";
			this.ygoSearchLocationLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoSearchLocationLabel.TabIndex = 51;
			this.ygoSearchLocationLabel.Text = "Location:";
			this.ygoSearchLocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoSearchNameLabel
			// 
			this.ygoSearchNameLabel.Location = new System.Drawing.Point(5, 5);
			this.ygoSearchNameLabel.Name = "ygoSearchNameLabel";
			this.ygoSearchNameLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoSearchNameLabel.TabIndex = 50;
			this.ygoSearchNameLabel.Text = "Name:";
			this.ygoSearchNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoSearchCardTypeLabel
			// 
			this.ygoSearchCardTypeLabel.Location = new System.Drawing.Point(5, 40);
			this.ygoSearchCardTypeLabel.Name = "ygoSearchCardTypeLabel";
			this.ygoSearchCardTypeLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoSearchCardTypeLabel.TabIndex = 49;
			this.ygoSearchCardTypeLabel.Text = "Card Type:";
			this.ygoSearchCardTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoSearchTypesLabel
			// 
			this.ygoSearchTypesLabel.Location = new System.Drawing.Point(5, 145);
			this.ygoSearchTypesLabel.Name = "ygoSearchTypesLabel";
			this.ygoSearchTypesLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoSearchTypesLabel.TabIndex = 48;
			this.ygoSearchTypesLabel.Text = "Types:";
			this.ygoSearchTypesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoSearchPropertyLabel
			// 
			this.ygoSearchPropertyLabel.Location = new System.Drawing.Point(5, 110);
			this.ygoSearchPropertyLabel.Name = "ygoSearchPropertyLabel";
			this.ygoSearchPropertyLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoSearchPropertyLabel.TabIndex = 47;
			this.ygoSearchPropertyLabel.Text = "Property:";
			this.ygoSearchPropertyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoSearchOracleLabel
			// 
			this.ygoSearchOracleLabel.Location = new System.Drawing.Point(5, 180);
			this.ygoSearchOracleLabel.Name = "ygoSearchOracleLabel";
			this.ygoSearchOracleLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoSearchOracleLabel.TabIndex = 46;
			this.ygoSearchOracleLabel.Text = "Oracle Text:";
			this.ygoSearchOracleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoSearchAttributeLabel
			// 
			this.ygoSearchAttributeLabel.Location = new System.Drawing.Point(5, 75);
			this.ygoSearchAttributeLabel.Name = "ygoSearchAttributeLabel";
			this.ygoSearchAttributeLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoSearchAttributeLabel.TabIndex = 45;
			this.ygoSearchAttributeLabel.Text = "Attribute:";
			this.ygoSearchAttributeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoCatalogPage
			// 
			this.ygoCatalogPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ygoCatalogPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ygoCatalogPage.Controls.Add(this.ygoSortAlphabeticalButton);
			this.ygoCatalogPage.Controls.Add(this.ygoSortNumericButton);
			this.ygoCatalogPage.Controls.Add(this.ygoCatalogNextButton);
			this.ygoCatalogPage.Controls.Add(this.ygoCatalogPrevButton);
			this.ygoCatalogPage.Controls.Add(this.ygoCatalogLayout);
			this.ygoCatalogPage.Controls.Add(this.ygoCatalogIndex);
			this.ygoCatalogPage.Location = new System.Drawing.Point(4, 25);
			this.ygoCatalogPage.Name = "ygoCatalogPage";
			this.ygoCatalogPage.Padding = new System.Windows.Forms.Padding(3);
			this.ygoCatalogPage.Size = new System.Drawing.Size(1262, 674);
			this.ygoCatalogPage.TabIndex = 2;
			this.ygoCatalogPage.Text = "Catalog";
			// 
			// ygoSortAlphabeticalButton
			// 
			this.ygoSortAlphabeticalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ygoSortAlphabeticalButton.AutoSize = true;
			this.ygoSortAlphabeticalButton.Location = new System.Drawing.Point(929, 8);
			this.ygoSortAlphabeticalButton.Name = "ygoSortAlphabeticalButton";
			this.ygoSortAlphabeticalButton.Size = new System.Drawing.Size(113, 25);
			this.ygoSortAlphabeticalButton.TabIndex = 6;
			this.ygoSortAlphabeticalButton.Text = "Alphabetical";
			this.ygoSortAlphabeticalButton.UseVisualStyleBackColor = true;
			this.ygoSortAlphabeticalButton.CheckedChanged += new System.EventHandler(this.YGO_OnClickSortAlphabetical);
			// 
			// ygoSortNumericButton
			// 
			this.ygoSortNumericButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ygoSortNumericButton.AutoSize = true;
			this.ygoSortNumericButton.Checked = true;
			this.ygoSortNumericButton.Location = new System.Drawing.Point(835, 7);
			this.ygoSortNumericButton.Name = "ygoSortNumericButton";
			this.ygoSortNumericButton.Size = new System.Drawing.Size(88, 25);
			this.ygoSortNumericButton.TabIndex = 5;
			this.ygoSortNumericButton.TabStop = true;
			this.ygoSortNumericButton.Text = "Numeric";
			this.ygoSortNumericButton.UseVisualStyleBackColor = true;
			this.ygoSortNumericButton.CheckedChanged += new System.EventHandler(this.YGO_OnClickSortNumeric);
			// 
			// ygoCatalogNextButton
			// 
			this.ygoCatalogNextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ygoCatalogNextButton.Location = new System.Drawing.Point(1178, 5);
			this.ygoCatalogNextButton.Name = "ygoCatalogNextButton";
			this.ygoCatalogNextButton.Size = new System.Drawing.Size(75, 29);
			this.ygoCatalogNextButton.TabIndex = 3;
			this.ygoCatalogNextButton.Text = ">";
			this.ygoCatalogNextButton.UseVisualStyleBackColor = true;
			this.ygoCatalogNextButton.Click += new System.EventHandler(this.YGO_OnClickCatalogNext);
			// 
			// ygoCatalogPrevButton
			// 
			this.ygoCatalogPrevButton.Location = new System.Drawing.Point(5, 5);
			this.ygoCatalogPrevButton.Name = "ygoCatalogPrevButton";
			this.ygoCatalogPrevButton.Size = new System.Drawing.Size(75, 29);
			this.ygoCatalogPrevButton.TabIndex = 2;
			this.ygoCatalogPrevButton.Text = "<";
			this.ygoCatalogPrevButton.UseVisualStyleBackColor = true;
			this.ygoCatalogPrevButton.Click += new System.EventHandler(this.YGO_OnClickCatalogPrev);
			// 
			// ygoCatalogLayout
			// 
			this.ygoCatalogLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ygoCatalogLayout.AutoScroll = true;
			this.ygoCatalogLayout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ygoCatalogLayout.Location = new System.Drawing.Point(3, 38);
			this.ygoCatalogLayout.Name = "ygoCatalogLayout";
			this.ygoCatalogLayout.Size = new System.Drawing.Size(1252, 628);
			this.ygoCatalogLayout.TabIndex = 1;
			// 
			// ygoCatalogIndex
			// 
			this.ygoCatalogIndex.Dock = System.Windows.Forms.DockStyle.Top;
			this.ygoCatalogIndex.Location = new System.Drawing.Point(3, 3);
			this.ygoCatalogIndex.Name = "ygoCatalogIndex";
			this.ygoCatalogIndex.Size = new System.Drawing.Size(1252, 30);
			this.ygoCatalogIndex.TabIndex = 4;
			this.ygoCatalogIndex.Text = "Showing 0 - 0 of 0";
			this.ygoCatalogIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ygoDetailPage
			// 
			this.ygoDetailPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ygoDetailPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ygoDetailPage.Controls.Add(this.ygoDetailFilterCountLabel);
			this.ygoDetailPage.Controls.Add(this.ygoPrintingsBox);
			this.ygoDetailPage.Controls.Add(this.ygoDeletePrintingButton);
			this.ygoDetailPage.Controls.Add(this.ygoDetailDialog);
			this.ygoDetailPage.Controls.Add(this.ygoCardtipBox);
			this.ygoDetailPage.Controls.Add(this.ygoTooltipBox);
			this.ygoDetailPage.Controls.Add(this.ygoDetailNextButton);
			this.ygoDetailPage.Controls.Add(this.ygoDetailAutogenButton);
			this.ygoDetailPage.Controls.Add(this.ygoDetailPrevButton);
			this.ygoDetailPage.Controls.Add(this.ygoDetailBox);
			this.ygoDetailPage.Controls.Add(this.ygoDetailImgbox);
			this.ygoDetailPage.Location = new System.Drawing.Point(4, 25);
			this.ygoDetailPage.Name = "ygoDetailPage";
			this.ygoDetailPage.Size = new System.Drawing.Size(1262, 674);
			this.ygoDetailPage.TabIndex = 4;
			this.ygoDetailPage.Text = "Card Details";
			// 
			// ygoDetailFilterCountLabel
			// 
			this.ygoDetailFilterCountLabel.Location = new System.Drawing.Point(10, 585);
			this.ygoDetailFilterCountLabel.MinimumSize = new System.Drawing.Size(0, 29);
			this.ygoDetailFilterCountLabel.Name = "ygoDetailFilterCountLabel";
			this.ygoDetailFilterCountLabel.Size = new System.Drawing.Size(390, 29);
			this.ygoDetailFilterCountLabel.TabIndex = 33;
			this.ygoDetailFilterCountLabel.Text = "X / X";
			this.ygoDetailFilterCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ygoPrintingsBox
			// 
			this.ygoPrintingsBox.Location = new System.Drawing.Point(865, 5);
			this.ygoPrintingsBox.Name = "ygoPrintingsBox";
			this.ygoPrintingsBox.Size = new System.Drawing.Size(380, 100);
			this.ygoPrintingsBox.TabIndex = 30;
			this.ygoPrintingsBox.TabStop = false;
			// 
			// ygoDeletePrintingButton
			// 
			this.ygoDeletePrintingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ygoDeletePrintingButton.Location = new System.Drawing.Point(5, 638);
			this.ygoDeletePrintingButton.Name = "ygoDeletePrintingButton";
			this.ygoDeletePrintingButton.Size = new System.Drawing.Size(250, 29);
			this.ygoDeletePrintingButton.TabIndex = 32;
			this.ygoDeletePrintingButton.Text = "Delete Printing!";
			this.ygoDeletePrintingButton.UseVisualStyleBackColor = true;
			this.ygoDeletePrintingButton.Click += new System.EventHandler(this.YGO_DeleteCurrentPrinting);
			// 
			// ygoDetailDialog
			// 
			this.ygoDetailDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ygoDetailDialog.AutoSize = true;
			this.ygoDetailDialog.Location = new System.Drawing.Point(261, 638);
			this.ygoDetailDialog.MinimumSize = new System.Drawing.Size(0, 29);
			this.ygoDetailDialog.Name = "ygoDetailDialog";
			this.ygoDetailDialog.Size = new System.Drawing.Size(16, 29);
			this.ygoDetailDialog.TabIndex = 31;
			this.ygoDetailDialog.Text = "-";
			this.ygoDetailDialog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoCardtipBox
			// 
			this.ygoCardtipBox.Controls.Add(this.ygoCardtipImage);
			this.ygoCardtipBox.Location = new System.Drawing.Point(865, 215);
			this.ygoCardtipBox.Name = "ygoCardtipBox";
			this.ygoCardtipBox.Size = new System.Drawing.Size(250, 350);
			this.ygoCardtipBox.TabIndex = 30;
			this.ygoCardtipBox.TabStop = false;
			// 
			// ygoCardtipImage
			// 
			this.ygoCardtipImage.Location = new System.Drawing.Point(0, 0);
			this.ygoCardtipImage.Name = "ygoCardtipImage";
			this.ygoCardtipImage.Size = new System.Drawing.Size(250, 350);
			this.ygoCardtipImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.ygoCardtipImage.TabIndex = 0;
			this.ygoCardtipImage.TabStop = false;
			// 
			// ygoTooltipBox
			// 
			this.ygoTooltipBox.Location = new System.Drawing.Point(865, 110);
			this.ygoTooltipBox.Name = "ygoTooltipBox";
			this.ygoTooltipBox.Size = new System.Drawing.Size(300, 100);
			this.ygoTooltipBox.TabIndex = 29;
			this.ygoTooltipBox.TabStop = false;
			// 
			// ygoDetailNextButton
			// 
			this.ygoDetailNextButton.Location = new System.Drawing.Point(285, 550);
			this.ygoDetailNextButton.Name = "ygoDetailNextButton";
			this.ygoDetailNextButton.Size = new System.Drawing.Size(120, 29);
			this.ygoDetailNextButton.TabIndex = 28;
			this.ygoDetailNextButton.Text = "Next Card";
			this.ygoDetailNextButton.UseVisualStyleBackColor = true;
			this.ygoDetailNextButton.Click += new System.EventHandler(this.YGO_LoadNextInSelection);
			// 
			// ygoDetailAutogenButton
			// 
			this.ygoDetailAutogenButton.Location = new System.Drawing.Point(130, 550);
			this.ygoDetailAutogenButton.Name = "ygoDetailAutogenButton";
			this.ygoDetailAutogenButton.Size = new System.Drawing.Size(150, 29);
			this.ygoDetailAutogenButton.TabIndex = 27;
			this.ygoDetailAutogenButton.Text = "Autogen";
			this.ygoDetailAutogenButton.UseVisualStyleBackColor = true;
			this.ygoDetailAutogenButton.Click += new System.EventHandler(this.YGO_AutogenDetailRef);
			// 
			// ygoDetailPrevButton
			// 
			this.ygoDetailPrevButton.Location = new System.Drawing.Point(5, 550);
			this.ygoDetailPrevButton.Name = "ygoDetailPrevButton";
			this.ygoDetailPrevButton.Size = new System.Drawing.Size(120, 29);
			this.ygoDetailPrevButton.TabIndex = 10;
			this.ygoDetailPrevButton.Text = "Previous Card";
			this.ygoDetailPrevButton.UseVisualStyleBackColor = true;
			this.ygoDetailPrevButton.Click += new System.EventHandler(this.YGO_LoadPreviousInSelection);
			// 
			// ygoDetailBox
			// 
			this.ygoDetailBox.Controls.Add(this.ygoReloadLocationsButton);
			this.ygoDetailBox.Controls.Add(this.ygoMoveField);
			this.ygoDetailBox.Controls.Add(this.ygoEditPrintButton);
			this.ygoDetailBox.Controls.Add(this.ygoEditCardButton);
			this.ygoDetailBox.Controls.Add(this.ygoMoveLabel);
			this.ygoDetailBox.Controls.Add(this.ygoLocationTable);
			this.ygoDetailBox.Location = new System.Drawing.Point(410, 5);
			this.ygoDetailBox.Name = "ygoDetailBox";
			this.ygoDetailBox.Size = new System.Drawing.Size(450, 540);
			this.ygoDetailBox.TabIndex = 4;
			this.ygoDetailBox.TabStop = false;
			this.ygoDetailBox.Text = "Owned Printings";
			// 
			// ygoReloadLocationsButton
			// 
			this.ygoReloadLocationsButton.Location = new System.Drawing.Point(295, 25);
			this.ygoReloadLocationsButton.Name = "ygoReloadLocationsButton";
			this.ygoReloadLocationsButton.Size = new System.Drawing.Size(150, 29);
			this.ygoReloadLocationsButton.TabIndex = 9;
			this.ygoReloadLocationsButton.Text = "Reload Locations";
			this.ygoReloadLocationsButton.UseVisualStyleBackColor = true;
			this.ygoReloadLocationsButton.Click += new System.EventHandler(this.YGO_OnClickReloadLocations);
			// 
			// ygoMoveField
			// 
			this.ygoMoveField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.ygoMoveField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.ygoMoveField.FormattingEnabled = true;
			this.ygoMoveField.Location = new System.Drawing.Point(75, 25);
			this.ygoMoveField.Name = "ygoMoveField";
			this.ygoMoveField.Size = new System.Drawing.Size(215, 29);
			this.ygoMoveField.TabIndex = 6;
			// 
			// ygoEditPrintButton
			// 
			this.ygoEditPrintButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ygoEditPrintButton.Location = new System.Drawing.Point(300, 505);
			this.ygoEditPrintButton.Name = "ygoEditPrintButton";
			this.ygoEditPrintButton.Size = new System.Drawing.Size(145, 29);
			this.ygoEditPrintButton.TabIndex = 5;
			this.ygoEditPrintButton.Text = "Edit Printing Data";
			this.ygoEditPrintButton.UseVisualStyleBackColor = true;
			this.ygoEditPrintButton.Click += new System.EventHandler(this.YGO_EditPrint);
			// 
			// ygoEditCardButton
			// 
			this.ygoEditCardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ygoEditCardButton.Location = new System.Drawing.Point(150, 505);
			this.ygoEditCardButton.Name = "ygoEditCardButton";
			this.ygoEditCardButton.Size = new System.Drawing.Size(145, 29);
			this.ygoEditCardButton.TabIndex = 4;
			this.ygoEditCardButton.Text = "Edit Card Data";
			this.ygoEditCardButton.UseVisualStyleBackColor = true;
			this.ygoEditCardButton.Click += new System.EventHandler(this.YGO_EditCard);
			// 
			// ygoMoveLabel
			// 
			this.ygoMoveLabel.Location = new System.Drawing.Point(5, 25);
			this.ygoMoveLabel.Name = "ygoMoveLabel";
			this.ygoMoveLabel.Size = new System.Drawing.Size(70, 30);
			this.ygoMoveLabel.TabIndex = 3;
			this.ygoMoveLabel.Text = "Move to:";
			this.ygoMoveLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoLocationTable
			// 
			this.ygoLocationTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
			this.ygoLocationTable.ColumnCount = 4;
			this.ygoLocationTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.ygoLocationTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.ygoLocationTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.ygoLocationTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.ygoLocationTable.Location = new System.Drawing.Point(9, 60);
			this.ygoLocationTable.Name = "ygoLocationTable";
			this.ygoLocationTable.RowCount = 1;
			this.ygoLocationTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.ygoLocationTable.Size = new System.Drawing.Size(435, 30);
			this.ygoLocationTable.TabIndex = 1;
			// 
			// ygoDetailImgbox
			// 
			this.ygoDetailImgbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ygoDetailImgbox.Location = new System.Drawing.Point(5, 5);
			this.ygoDetailImgbox.Name = "ygoDetailImgbox";
			this.ygoDetailImgbox.Size = new System.Drawing.Size(400, 540);
			this.ygoDetailImgbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.ygoDetailImgbox.TabIndex = 0;
			this.ygoDetailImgbox.TabStop = false;
			// 
			// ygoCardPage
			// 
			this.ygoCardPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ygoCardPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ygoCardPage.Controls.Add(this.ygoImportButton);
			this.ygoCardPage.Controls.Add(this.ygoImportField);
			this.ygoCardPage.Controls.Add(this.ygoScaleField);
			this.ygoCardPage.Controls.Add(this.ygoLevelField);
			this.ygoCardPage.Controls.Add(this.ygoDefField);
			this.ygoCardPage.Controls.Add(this.ygoAtkField);
			this.ygoCardPage.Controls.Add(this.ygoCardDialog);
			this.ygoCardPage.Controls.Add(this.ygoIgnoreDuplicateEntryLabel);
			this.ygoCardPage.Controls.Add(this.ygoIgnoreDuplicateEntryBox);
			this.ygoCardPage.Controls.Add(this.ygoAddCardButton);
			this.ygoCardPage.Controls.Add(this.ygoScaleLabel);
			this.ygoCardPage.Controls.Add(this.ygoAtkDefLabel);
			this.ygoCardPage.Controls.Add(this.ygoLevelLabel);
			this.ygoCardPage.Controls.Add(this.ygoOracleField);
			this.ygoCardPage.Controls.Add(this.ygoCardTypeField);
			this.ygoCardPage.Controls.Add(this.ygoAttributeField);
			this.ygoCardPage.Controls.Add(this.ygoPropertyField);
			this.ygoCardPage.Controls.Add(this.ygoTypesField);
			this.ygoCardPage.Controls.Add(this.ygoNameField);
			this.ygoCardPage.Controls.Add(this.ygoOracleLabel);
			this.ygoCardPage.Controls.Add(this.ygoTypesLabel);
			this.ygoCardPage.Controls.Add(this.ygoPropertyLabel);
			this.ygoCardPage.Controls.Add(this.ygoAttributeLabel);
			this.ygoCardPage.Controls.Add(this.ygoCardTypeLabel);
			this.ygoCardPage.Controls.Add(this.ygoNameLabel);
			this.ygoCardPage.Location = new System.Drawing.Point(4, 25);
			this.ygoCardPage.Name = "ygoCardPage";
			this.ygoCardPage.Padding = new System.Windows.Forms.Padding(3);
			this.ygoCardPage.Size = new System.Drawing.Size(1262, 674);
			this.ygoCardPage.TabIndex = 0;
			this.ygoCardPage.Text = "Card Entry";
			// 
			// ygoImportButton
			// 
			this.ygoImportButton.Location = new System.Drawing.Point(555, 355);
			this.ygoImportButton.Name = "ygoImportButton";
			this.ygoImportButton.Size = new System.Drawing.Size(250, 30);
			this.ygoImportButton.TabIndex = 55;
			this.ygoImportButton.Text = "Import!";
			this.ygoImportButton.UseVisualStyleBackColor = true;
			this.ygoImportButton.Click += new System.EventHandler(this.YGO_OnClickImport);
			// 
			// ygoImportField
			// 
			this.ygoImportField.Location = new System.Drawing.Point(555, 180);
			this.ygoImportField.Multiline = true;
			this.ygoImportField.Name = "ygoImportField";
			this.ygoImportField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.ygoImportField.Size = new System.Drawing.Size(250, 170);
			this.ygoImportField.TabIndex = 24;
			// 
			// ygoScaleField
			// 
			this.ygoScaleField.Location = new System.Drawing.Point(100, 460);
			this.ygoScaleField.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.ygoScaleField.Name = "ygoScaleField";
			this.ygoScaleField.Size = new System.Drawing.Size(250, 29);
			this.ygoScaleField.TabIndex = 16;
			// 
			// ygoLevelField
			// 
			this.ygoLevelField.Location = new System.Drawing.Point(100, 425);
			this.ygoLevelField.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.ygoLevelField.Name = "ygoLevelField";
			this.ygoLevelField.Size = new System.Drawing.Size(250, 29);
			this.ygoLevelField.TabIndex = 14;
			// 
			// ygoDefField
			// 
			this.ygoDefField.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.ygoDefField.Location = new System.Drawing.Point(227, 495);
			this.ygoDefField.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.ygoDefField.Name = "ygoDefField";
			this.ygoDefField.Size = new System.Drawing.Size(123, 29);
			this.ygoDefField.TabIndex = 19;
			// 
			// ygoAtkField
			// 
			this.ygoAtkField.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.ygoAtkField.Location = new System.Drawing.Point(100, 495);
			this.ygoAtkField.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.ygoAtkField.Name = "ygoAtkField";
			this.ygoAtkField.Size = new System.Drawing.Size(123, 29);
			this.ygoAtkField.TabIndex = 18;
			// 
			// ygoCardDialog
			// 
			this.ygoCardDialog.AutoSize = true;
			this.ygoCardDialog.Location = new System.Drawing.Point(355, 533);
			this.ygoCardDialog.Name = "ygoCardDialog";
			this.ygoCardDialog.Size = new System.Drawing.Size(16, 21);
			this.ygoCardDialog.TabIndex = 21;
			this.ygoCardDialog.Text = "-";
			this.ygoCardDialog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoIgnoreDuplicateEntryLabel
			// 
			this.ygoIgnoreDuplicateEntryLabel.Location = new System.Drawing.Point(115, 565);
			this.ygoIgnoreDuplicateEntryLabel.Name = "ygoIgnoreDuplicateEntryLabel";
			this.ygoIgnoreDuplicateEntryLabel.Size = new System.Drawing.Size(235, 30);
			this.ygoIgnoreDuplicateEntryLabel.TabIndex = 23;
			this.ygoIgnoreDuplicateEntryLabel.Text = "Ignore Duplicate Entry";
			this.ygoIgnoreDuplicateEntryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoIgnoreDuplicateEntryBox
			// 
			this.ygoIgnoreDuplicateEntryBox.Location = new System.Drawing.Point(100, 565);
			this.ygoIgnoreDuplicateEntryBox.Name = "ygoIgnoreDuplicateEntryBox";
			this.ygoIgnoreDuplicateEntryBox.Size = new System.Drawing.Size(14, 30);
			this.ygoIgnoreDuplicateEntryBox.TabIndex = 22;
			this.ygoIgnoreDuplicateEntryBox.UseVisualStyleBackColor = true;
			// 
			// ygoAddCardButton
			// 
			this.ygoAddCardButton.Location = new System.Drawing.Point(100, 530);
			this.ygoAddCardButton.Name = "ygoAddCardButton";
			this.ygoAddCardButton.Size = new System.Drawing.Size(250, 30);
			this.ygoAddCardButton.TabIndex = 20;
			this.ygoAddCardButton.Text = "Add To Catalog";
			this.ygoAddCardButton.UseVisualStyleBackColor = true;
			this.ygoAddCardButton.Click += new System.EventHandler(this.YGO_OnClickAddCard);
			// 
			// ygoScaleLabel
			// 
			this.ygoScaleLabel.Location = new System.Drawing.Point(5, 460);
			this.ygoScaleLabel.Name = "ygoScaleLabel";
			this.ygoScaleLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoScaleLabel.TabIndex = 15;
			this.ygoScaleLabel.Text = "Scale:";
			this.ygoScaleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoAtkDefLabel
			// 
			this.ygoAtkDefLabel.Location = new System.Drawing.Point(5, 495);
			this.ygoAtkDefLabel.Name = "ygoAtkDefLabel";
			this.ygoAtkDefLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoAtkDefLabel.TabIndex = 17;
			this.ygoAtkDefLabel.Text = "ATK/DEF:";
			this.ygoAtkDefLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoLevelLabel
			// 
			this.ygoLevelLabel.Location = new System.Drawing.Point(5, 425);
			this.ygoLevelLabel.Name = "ygoLevelLabel";
			this.ygoLevelLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoLevelLabel.TabIndex = 13;
			this.ygoLevelLabel.Text = "Level:";
			this.ygoLevelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoOracleField
			// 
			this.ygoOracleField.Location = new System.Drawing.Point(100, 180);
			this.ygoOracleField.Multiline = true;
			this.ygoOracleField.Name = "ygoOracleField";
			this.ygoOracleField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.ygoOracleField.Size = new System.Drawing.Size(450, 240);
			this.ygoOracleField.TabIndex = 12;
			// 
			// ygoCardTypeField
			// 
			this.ygoCardTypeField.Location = new System.Drawing.Point(100, 40);
			this.ygoCardTypeField.Name = "ygoCardTypeField";
			this.ygoCardTypeField.Size = new System.Drawing.Size(370, 29);
			this.ygoCardTypeField.TabIndex = 4;
			// 
			// ygoAttributeField
			// 
			this.ygoAttributeField.Location = new System.Drawing.Point(100, 75);
			this.ygoAttributeField.Name = "ygoAttributeField";
			this.ygoAttributeField.Size = new System.Drawing.Size(370, 29);
			this.ygoAttributeField.TabIndex = 6;
			// 
			// ygoPropertyField
			// 
			this.ygoPropertyField.Location = new System.Drawing.Point(100, 110);
			this.ygoPropertyField.Name = "ygoPropertyField";
			this.ygoPropertyField.Size = new System.Drawing.Size(370, 29);
			this.ygoPropertyField.TabIndex = 8;
			// 
			// ygoTypesField
			// 
			this.ygoTypesField.Location = new System.Drawing.Point(100, 145);
			this.ygoTypesField.Name = "ygoTypesField";
			this.ygoTypesField.Size = new System.Drawing.Size(370, 29);
			this.ygoTypesField.TabIndex = 10;
			// 
			// ygoNameField
			// 
			this.ygoNameField.Location = new System.Drawing.Point(100, 5);
			this.ygoNameField.Name = "ygoNameField";
			this.ygoNameField.Size = new System.Drawing.Size(370, 29);
			this.ygoNameField.TabIndex = 2;
			// 
			// ygoOracleLabel
			// 
			this.ygoOracleLabel.Location = new System.Drawing.Point(5, 180);
			this.ygoOracleLabel.Name = "ygoOracleLabel";
			this.ygoOracleLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoOracleLabel.TabIndex = 11;
			this.ygoOracleLabel.Text = "Oracle Text:";
			this.ygoOracleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoTypesLabel
			// 
			this.ygoTypesLabel.Location = new System.Drawing.Point(5, 145);
			this.ygoTypesLabel.Name = "ygoTypesLabel";
			this.ygoTypesLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoTypesLabel.TabIndex = 9;
			this.ygoTypesLabel.Text = "Types:";
			this.ygoTypesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoPropertyLabel
			// 
			this.ygoPropertyLabel.Location = new System.Drawing.Point(5, 110);
			this.ygoPropertyLabel.Name = "ygoPropertyLabel";
			this.ygoPropertyLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoPropertyLabel.TabIndex = 7;
			this.ygoPropertyLabel.Text = "Property:";
			this.ygoPropertyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoAttributeLabel
			// 
			this.ygoAttributeLabel.Location = new System.Drawing.Point(5, 75);
			this.ygoAttributeLabel.Name = "ygoAttributeLabel";
			this.ygoAttributeLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoAttributeLabel.TabIndex = 5;
			this.ygoAttributeLabel.Text = "Attribute:";
			this.ygoAttributeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoCardTypeLabel
			// 
			this.ygoCardTypeLabel.Location = new System.Drawing.Point(5, 40);
			this.ygoCardTypeLabel.Name = "ygoCardTypeLabel";
			this.ygoCardTypeLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoCardTypeLabel.TabIndex = 3;
			this.ygoCardTypeLabel.Text = "Card Type:";
			this.ygoCardTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoPrintPage
			// 
			this.ygoPrintPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ygoPrintPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ygoPrintPage.Controls.Add(this.ygoImgpathBackLabel);
			this.ygoPrintPage.Controls.Add(this.ygoPrintAutoLimit);
			this.ygoPrintPage.Controls.Add(this.ygoPrintAutofillRangeButton);
			this.ygoPrintPage.Controls.Add(this.ygoPrintAutofillButton);
			this.ygoPrintPage.Controls.Add(this.ygoCardrefDescriptor);
			this.ygoPrintPage.Controls.Add(this.ygoPrintIDLabel);
			this.ygoPrintPage.Controls.Add(this.ygoPrintIDField);
			this.ygoPrintPage.Controls.Add(this.ygoPrintImgboxBack);
			this.ygoPrintPage.Controls.Add(this.ygoImgsearchBackButton);
			this.ygoPrintPage.Controls.Add(this.ygoImgBackLabel);
			this.ygoPrintPage.Controls.Add(this.ygoIODialog);
			this.ygoPrintPage.Controls.Add(this.ygoSaveButton);
			this.ygoPrintPage.Controls.Add(this.ygoImgpathLabel);
			this.ygoPrintPage.Controls.Add(this.ygoRemoveRarityButton);
			this.ygoPrintPage.Controls.Add(this.ygoAddRarityButton);
			this.ygoPrintPage.Controls.Add(this.ygoPrintImgbox);
			this.ygoPrintPage.Controls.Add(this.ygoAddPrintButton);
			this.ygoPrintPage.Controls.Add(this.ygoCardrefField);
			this.ygoPrintPage.Controls.Add(this.ygoCardrefLabel);
			this.ygoPrintPage.Controls.Add(this.ygoImgsearchButton);
			this.ygoPrintPage.Controls.Add(this.ygoImageLabel);
			this.ygoPrintPage.Controls.Add(this.ygoFlavorField);
			this.ygoPrintPage.Controls.Add(this.ygoFlavorLabel);
			this.ygoPrintPage.Controls.Add(this.ygoRaritiesValue);
			this.ygoPrintPage.Controls.Add(this.ygoRaritiesField);
			this.ygoPrintPage.Controls.Add(this.ygoRaritiesLabel);
			this.ygoPrintPage.Controls.Add(this.ygoNumberField);
			this.ygoPrintPage.Controls.Add(this.ygoNumberLabel);
			this.ygoPrintPage.Controls.Add(this.ygoSetField);
			this.ygoPrintPage.Controls.Add(this.ygoSetLabel);
			this.ygoPrintPage.Location = new System.Drawing.Point(4, 25);
			this.ygoPrintPage.Name = "ygoPrintPage";
			this.ygoPrintPage.Padding = new System.Windows.Forms.Padding(3);
			this.ygoPrintPage.Size = new System.Drawing.Size(1262, 674);
			this.ygoPrintPage.TabIndex = 1;
			this.ygoPrintPage.Text = "Printing Entry";
			// 
			// ygoImgpathBackLabel
			// 
			this.ygoImgpathBackLabel.AutoSize = true;
			this.ygoImgpathBackLabel.Location = new System.Drawing.Point(855, 505);
			this.ygoImgpathBackLabel.Name = "ygoImgpathBackLabel";
			this.ygoImgpathBackLabel.Size = new System.Drawing.Size(0, 21);
			this.ygoImgpathBackLabel.TabIndex = 63;
			// 
			// ygoPrintAutoLimit
			// 
			this.ygoPrintAutoLimit.Location = new System.Drawing.Point(220, 475);
			this.ygoPrintAutoLimit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.ygoPrintAutoLimit.Name = "ygoPrintAutoLimit";
			this.ygoPrintAutoLimit.Size = new System.Drawing.Size(250, 29);
			this.ygoPrintAutoLimit.TabIndex = 62;
			// 
			// ygoPrintAutofillRangeButton
			// 
			this.ygoPrintAutofillRangeButton.Location = new System.Drawing.Point(100, 475);
			this.ygoPrintAutofillRangeButton.Name = "ygoPrintAutofillRangeButton";
			this.ygoPrintAutofillRangeButton.Size = new System.Drawing.Size(115, 29);
			this.ygoPrintAutofillRangeButton.TabIndex = 61;
			this.ygoPrintAutofillRangeButton.Text = "Autofill to";
			this.ygoPrintAutofillRangeButton.UseVisualStyleBackColor = true;
			this.ygoPrintAutofillRangeButton.Click += new System.EventHandler(this.YGO_OnClickAddAndFill);
			// 
			// ygoPrintAutofillButton
			// 
			this.ygoPrintAutofillButton.Location = new System.Drawing.Point(320, 300);
			this.ygoPrintAutofillButton.Name = "ygoPrintAutofillButton";
			this.ygoPrintAutofillButton.Size = new System.Drawing.Size(150, 29);
			this.ygoPrintAutofillButton.TabIndex = 60;
			this.ygoPrintAutofillButton.Text = "Autofill";
			this.ygoPrintAutofillButton.UseVisualStyleBackColor = true;
			this.ygoPrintAutofillButton.Click += new System.EventHandler(this.YGO_OnClickPrintAutofill);
			// 
			// ygoCardrefDescriptor
			// 
			this.ygoCardrefDescriptor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ygoCardrefDescriptor.AutoSize = true;
			this.ygoCardrefDescriptor.Location = new System.Drawing.Point(5, 611);
			this.ygoCardrefDescriptor.MaximumSize = new System.Drawing.Size(0, 21);
			this.ygoCardrefDescriptor.Name = "ygoCardrefDescriptor";
			this.ygoCardrefDescriptor.Size = new System.Drawing.Size(16, 21);
			this.ygoCardrefDescriptor.TabIndex = 59;
			this.ygoCardrefDescriptor.Text = "-";
			this.ygoCardrefDescriptor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoPrintIDLabel
			// 
			this.ygoPrintIDLabel.Location = new System.Drawing.Point(5, 405);
			this.ygoPrintIDLabel.Name = "ygoPrintIDLabel";
			this.ygoPrintIDLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoPrintIDLabel.TabIndex = 52;
			this.ygoPrintIDLabel.Text = "Print ID:";
			this.ygoPrintIDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoPrintIDField
			// 
			this.ygoPrintIDField.Location = new System.Drawing.Point(100, 405);
			this.ygoPrintIDField.Name = "ygoPrintIDField";
			this.ygoPrintIDField.Size = new System.Drawing.Size(370, 29);
			this.ygoPrintIDField.TabIndex = 53;
			// 
			// ygoPrintImgboxBack
			// 
			this.ygoPrintImgboxBack.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ygoPrintImgboxBack.InitialImage = null;
			this.ygoPrintImgboxBack.Location = new System.Drawing.Point(855, 5);
			this.ygoPrintImgboxBack.Name = "ygoPrintImgboxBack";
			this.ygoPrintImgboxBack.Size = new System.Drawing.Size(375, 495);
			this.ygoPrintImgboxBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.ygoPrintImgboxBack.TabIndex = 58;
			this.ygoPrintImgboxBack.TabStop = false;
			// 
			// ygoImgsearchBackButton
			// 
			this.ygoImgsearchBackButton.Location = new System.Drawing.Point(100, 335);
			this.ygoImgsearchBackButton.Name = "ygoImgsearchBackButton";
			this.ygoImgsearchBackButton.Size = new System.Drawing.Size(370, 29);
			this.ygoImgsearchBackButton.TabIndex = 48;
			this.ygoImgsearchBackButton.Text = "Search";
			this.ygoImgsearchBackButton.UseVisualStyleBackColor = true;
			this.ygoImgsearchBackButton.Click += new System.EventHandler(this.YGO_OnClickSearchImgBack);
			// 
			// ygoImgBackLabel
			// 
			this.ygoImgBackLabel.Location = new System.Drawing.Point(5, 335);
			this.ygoImgBackLabel.Name = "ygoImgBackLabel";
			this.ygoImgBackLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoImgBackLabel.TabIndex = 47;
			this.ygoImgBackLabel.Text = "Back:";
			this.ygoImgBackLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoIODialog
			// 
			this.ygoIODialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ygoIODialog.AutoSize = true;
			this.ygoIODialog.Location = new System.Drawing.Point(260, 638);
			this.ygoIODialog.Name = "ygoIODialog";
			this.ygoIODialog.Size = new System.Drawing.Size(16, 21);
			this.ygoIODialog.TabIndex = 56;
			this.ygoIODialog.Text = "-";
			this.ygoIODialog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoSaveButton
			// 
			this.ygoSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ygoSaveButton.Location = new System.Drawing.Point(5, 635);
			this.ygoSaveButton.Name = "ygoSaveButton";
			this.ygoSaveButton.Size = new System.Drawing.Size(250, 29);
			this.ygoSaveButton.TabIndex = 55;
			this.ygoSaveButton.Text = "Save Catalog";
			this.ygoSaveButton.UseVisualStyleBackColor = true;
			this.ygoSaveButton.Click += new System.EventHandler(this.YGO_OnClickSave);
			// 
			// ygoImgpathLabel
			// 
			this.ygoImgpathLabel.AutoSize = true;
			this.ygoImgpathLabel.Location = new System.Drawing.Point(475, 505);
			this.ygoImgpathLabel.Name = "ygoImgpathLabel";
			this.ygoImgpathLabel.Size = new System.Drawing.Size(16, 21);
			this.ygoImgpathLabel.TabIndex = 57;
			this.ygoImgpathLabel.Text = "-";
			// 
			// ygoRemoveRarityButton
			// 
			this.ygoRemoveRarityButton.Location = new System.Drawing.Point(440, 75);
			this.ygoRemoveRarityButton.Name = "ygoRemoveRarityButton";
			this.ygoRemoveRarityButton.Size = new System.Drawing.Size(30, 29);
			this.ygoRemoveRarityButton.TabIndex = 41;
			this.ygoRemoveRarityButton.Text = "-";
			this.ygoRemoveRarityButton.UseVisualStyleBackColor = true;
			this.ygoRemoveRarityButton.Click += new System.EventHandler(this.YGO_OnClickSubRarity);
			// 
			// ygoAddRarityButton
			// 
			this.ygoAddRarityButton.Location = new System.Drawing.Point(405, 75);
			this.ygoAddRarityButton.Name = "ygoAddRarityButton";
			this.ygoAddRarityButton.Size = new System.Drawing.Size(30, 29);
			this.ygoAddRarityButton.TabIndex = 40;
			this.ygoAddRarityButton.Text = "+";
			this.ygoAddRarityButton.UseVisualStyleBackColor = true;
			this.ygoAddRarityButton.Click += new System.EventHandler(this.YGO_OnClickAddRarity);
			// 
			// ygoPrintImgbox
			// 
			this.ygoPrintImgbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ygoPrintImgbox.InitialImage = null;
			this.ygoPrintImgbox.Location = new System.Drawing.Point(475, 5);
			this.ygoPrintImgbox.Name = "ygoPrintImgbox";
			this.ygoPrintImgbox.Size = new System.Drawing.Size(375, 495);
			this.ygoPrintImgbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.ygoPrintImgbox.TabIndex = 51;
			this.ygoPrintImgbox.TabStop = false;
			// 
			// ygoAddPrintButton
			// 
			this.ygoAddPrintButton.Location = new System.Drawing.Point(100, 440);
			this.ygoAddPrintButton.Name = "ygoAddPrintButton";
			this.ygoAddPrintButton.Size = new System.Drawing.Size(370, 29);
			this.ygoAddPrintButton.TabIndex = 54;
			this.ygoAddPrintButton.Text = "Add To Catalog";
			this.ygoAddPrintButton.UseVisualStyleBackColor = true;
			this.ygoAddPrintButton.Click += new System.EventHandler(this.YGO_OnClickAddPrint);
			// 
			// ygoCardrefField
			// 
			this.ygoCardrefField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.ygoCardrefField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.ygoCardrefField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ygoCardrefField.FormattingEnabled = true;
			this.ygoCardrefField.Location = new System.Drawing.Point(100, 370);
			this.ygoCardrefField.Name = "ygoCardrefField";
			this.ygoCardrefField.Size = new System.Drawing.Size(370, 29);
			this.ygoCardrefField.Sorted = true;
			this.ygoCardrefField.TabIndex = 50;
			this.ygoCardrefField.SelectedIndexChanged += new System.EventHandler(this.YGO_OnSelectCardref);
			// 
			// ygoCardrefLabel
			// 
			this.ygoCardrefLabel.Location = new System.Drawing.Point(5, 370);
			this.ygoCardrefLabel.Name = "ygoCardrefLabel";
			this.ygoCardrefLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoCardrefLabel.TabIndex = 49;
			this.ygoCardrefLabel.Text = "Card:";
			this.ygoCardrefLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoImgsearchButton
			// 
			this.ygoImgsearchButton.Location = new System.Drawing.Point(100, 300);
			this.ygoImgsearchButton.Name = "ygoImgsearchButton";
			this.ygoImgsearchButton.Size = new System.Drawing.Size(215, 29);
			this.ygoImgsearchButton.TabIndex = 46;
			this.ygoImgsearchButton.Text = "Search";
			this.ygoImgsearchButton.UseVisualStyleBackColor = true;
			this.ygoImgsearchButton.Click += new System.EventHandler(this.YGO_OnClickSearchImg);
			// 
			// ygoImageLabel
			// 
			this.ygoImageLabel.Location = new System.Drawing.Point(5, 300);
			this.ygoImageLabel.Name = "ygoImageLabel";
			this.ygoImageLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoImageLabel.TabIndex = 45;
			this.ygoImageLabel.Text = "Image:";
			this.ygoImageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoFlavorField
			// 
			this.ygoFlavorField.Location = new System.Drawing.Point(100, 145);
			this.ygoFlavorField.Multiline = true;
			this.ygoFlavorField.Name = "ygoFlavorField";
			this.ygoFlavorField.Size = new System.Drawing.Size(370, 150);
			this.ygoFlavorField.TabIndex = 44;
			// 
			// ygoFlavorLabel
			// 
			this.ygoFlavorLabel.Location = new System.Drawing.Point(5, 145);
			this.ygoFlavorLabel.Name = "ygoFlavorLabel";
			this.ygoFlavorLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoFlavorLabel.TabIndex = 43;
			this.ygoFlavorLabel.Text = "Flavor Text:";
			this.ygoFlavorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoRaritiesValue
			// 
			this.ygoRaritiesValue.Location = new System.Drawing.Point(100, 110);
			this.ygoRaritiesValue.Name = "ygoRaritiesValue";
			this.ygoRaritiesValue.Size = new System.Drawing.Size(350, 30);
			this.ygoRaritiesValue.TabIndex = 42;
			this.ygoRaritiesValue.Text = "-";
			this.ygoRaritiesValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoRaritiesField
			// 
			this.ygoRaritiesField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.ygoRaritiesField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.ygoRaritiesField.FormattingEnabled = true;
			this.ygoRaritiesField.Location = new System.Drawing.Point(100, 75);
			this.ygoRaritiesField.Name = "ygoRaritiesField";
			this.ygoRaritiesField.Size = new System.Drawing.Size(300, 29);
			this.ygoRaritiesField.Sorted = true;
			this.ygoRaritiesField.TabIndex = 39;
			// 
			// ygoRaritiesLabel
			// 
			this.ygoRaritiesLabel.Location = new System.Drawing.Point(5, 75);
			this.ygoRaritiesLabel.Name = "ygoRaritiesLabel";
			this.ygoRaritiesLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoRaritiesLabel.TabIndex = 38;
			this.ygoRaritiesLabel.Text = "Rarities:";
			this.ygoRaritiesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoNumberField
			// 
			this.ygoNumberField.Location = new System.Drawing.Point(100, 40);
			this.ygoNumberField.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.ygoNumberField.Name = "ygoNumberField";
			this.ygoNumberField.Size = new System.Drawing.Size(370, 29);
			this.ygoNumberField.TabIndex = 35;
			// 
			// ygoNumberLabel
			// 
			this.ygoNumberLabel.Location = new System.Drawing.Point(5, 40);
			this.ygoNumberLabel.Name = "ygoNumberLabel";
			this.ygoNumberLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoNumberLabel.TabIndex = 34;
			this.ygoNumberLabel.Text = "Number:";
			this.ygoNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoSetField
			// 
			this.ygoSetField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ygoSetField.FormattingEnabled = true;
			this.ygoSetField.Location = new System.Drawing.Point(100, 5);
			this.ygoSetField.Name = "ygoSetField";
			this.ygoSetField.Size = new System.Drawing.Size(370, 29);
			this.ygoSetField.Sorted = true;
			this.ygoSetField.TabIndex = 33;
			// 
			// ygoSetLabel
			// 
			this.ygoSetLabel.Location = new System.Drawing.Point(5, 5);
			this.ygoSetLabel.Name = "ygoSetLabel";
			this.ygoSetLabel.Size = new System.Drawing.Size(90, 30);
			this.ygoSetLabel.TabIndex = 32;
			this.ygoSetLabel.Text = "Set:";
			this.ygoSetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoSetsPage
			// 
			this.ygoSetsPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ygoSetsPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ygoSetsPage.Controls.Add(this.ygoSetGeneratorPageLabel);
			this.ygoSetsPage.Controls.Add(this.ygoSetGeneratorNext);
			this.ygoSetsPage.Controls.Add(this.ygoSetGeneratorPrev);
			this.ygoSetsPage.Controls.Add(this.ygoSaveSetsButton);
			this.ygoSetsPage.Controls.Add(this.ygoAddSetButton);
			this.ygoSetsPage.Controls.Add(this.ygoReloadSetsButton);
			this.ygoSetsPage.Controls.Add(this.ygoSetGeneratorLayout);
			this.ygoSetsPage.Controls.Add(this.ygoSetGeneratorLabel);
			this.ygoSetsPage.Location = new System.Drawing.Point(4, 25);
			this.ygoSetsPage.Name = "ygoSetsPage";
			this.ygoSetsPage.Padding = new System.Windows.Forms.Padding(3);
			this.ygoSetsPage.Size = new System.Drawing.Size(1262, 674);
			this.ygoSetsPage.TabIndex = 5;
			this.ygoSetsPage.Text = "Sets";
			// 
			// ygoSetGeneratorPageLabel
			// 
			this.ygoSetGeneratorPageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ygoSetGeneratorPageLabel.Location = new System.Drawing.Point(1125, 5);
			this.ygoSetGeneratorPageLabel.Name = "ygoSetGeneratorPageLabel";
			this.ygoSetGeneratorPageLabel.Size = new System.Drawing.Size(60, 30);
			this.ygoSetGeneratorPageLabel.TabIndex = 9;
			this.ygoSetGeneratorPageLabel.Text = "X / X";
			this.ygoSetGeneratorPageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ygoSetGeneratorNext
			// 
			this.ygoSetGeneratorNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ygoSetGeneratorNext.Location = new System.Drawing.Point(1190, 5);
			this.ygoSetGeneratorNext.Name = "ygoSetGeneratorNext";
			this.ygoSetGeneratorNext.Size = new System.Drawing.Size(60, 29);
			this.ygoSetGeneratorNext.TabIndex = 8;
			this.ygoSetGeneratorNext.Text = ">";
			this.ygoSetGeneratorNext.UseVisualStyleBackColor = true;
			this.ygoSetGeneratorNext.Click += new System.EventHandler(this.YGO_OnClickSetGeneratorNext);
			// 
			// ygoSetGeneratorPrev
			// 
			this.ygoSetGeneratorPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ygoSetGeneratorPrev.Location = new System.Drawing.Point(1060, 5);
			this.ygoSetGeneratorPrev.Name = "ygoSetGeneratorPrev";
			this.ygoSetGeneratorPrev.Size = new System.Drawing.Size(60, 29);
			this.ygoSetGeneratorPrev.TabIndex = 7;
			this.ygoSetGeneratorPrev.Text = "<";
			this.ygoSetGeneratorPrev.UseVisualStyleBackColor = true;
			this.ygoSetGeneratorPrev.Click += new System.EventHandler(this.YGO_OnClickSetGeneratorPrev);
			// 
			// ygoSaveSetsButton
			// 
			this.ygoSaveSetsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ygoSaveSetsButton.Location = new System.Drawing.Point(995, 5);
			this.ygoSaveSetsButton.Name = "ygoSaveSetsButton";
			this.ygoSaveSetsButton.Size = new System.Drawing.Size(60, 29);
			this.ygoSaveSetsButton.TabIndex = 6;
			this.ygoSaveSetsButton.Text = "Save";
			this.ygoSaveSetsButton.UseVisualStyleBackColor = true;
			this.ygoSaveSetsButton.Click += new System.EventHandler(this.YGO_OnClickSaveSets);
			// 
			// ygoAddSetButton
			// 
			this.ygoAddSetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ygoAddSetButton.Location = new System.Drawing.Point(960, 5);
			this.ygoAddSetButton.Name = "ygoAddSetButton";
			this.ygoAddSetButton.Size = new System.Drawing.Size(30, 29);
			this.ygoAddSetButton.TabIndex = 5;
			this.ygoAddSetButton.Text = "+";
			this.ygoAddSetButton.UseVisualStyleBackColor = true;
			this.ygoAddSetButton.Click += new System.EventHandler(this.YGO_OnClickAddSet);
			// 
			// ygoReloadSetsButton
			// 
			this.ygoReloadSetsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ygoReloadSetsButton.Location = new System.Drawing.Point(880, 5);
			this.ygoReloadSetsButton.Name = "ygoReloadSetsButton";
			this.ygoReloadSetsButton.Size = new System.Drawing.Size(75, 29);
			this.ygoReloadSetsButton.TabIndex = 4;
			this.ygoReloadSetsButton.Text = "Reload";
			this.ygoReloadSetsButton.UseVisualStyleBackColor = true;
			this.ygoReloadSetsButton.Click += new System.EventHandler(this.YGO_RegenerateSets);
			// 
			// ygoSetGeneratorLayout
			// 
			this.ygoSetGeneratorLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ygoSetGeneratorLayout.AutoScroll = true;
			this.ygoSetGeneratorLayout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ygoSetGeneratorLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.ygoSetGeneratorLayout.Location = new System.Drawing.Point(3, 38);
			this.ygoSetGeneratorLayout.Name = "ygoSetGeneratorLayout";
			this.ygoSetGeneratorLayout.Size = new System.Drawing.Size(1252, 628);
			this.ygoSetGeneratorLayout.TabIndex = 2;
			// 
			// ygoSetGeneratorLabel
			// 
			this.ygoSetGeneratorLabel.AutoSize = true;
			this.ygoSetGeneratorLabel.Location = new System.Drawing.Point(5, 8);
			this.ygoSetGeneratorLabel.Name = "ygoSetGeneratorLabel";
			this.ygoSetGeneratorLabel.Size = new System.Drawing.Size(69, 21);
			this.ygoSetGeneratorLabel.TabIndex = 1;
			this.ygoSetGeneratorLabel.Text = "Edit Sets";
			this.ygoSetGeneratorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// imageFileDialog
			// 
			this.imageFileDialog.Filter = "All files (*.*)|*.*";
			this.imageFileDialog.Title = "Select Image";
			// 
			// formTabControl
			// 
			this.formTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.formTabControl.Controls.Add(this.mtgPage);
			this.formTabControl.Controls.Add(this.ygoPage);
			this.formTabControl.Controls.Add(this.pkmnPage);
			this.formTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.formTabControl.Location = new System.Drawing.Point(0, 0);
			this.formTabControl.Name = "formTabControl";
			this.formTabControl.SelectedIndex = 0;
			this.formTabControl.Size = new System.Drawing.Size(1284, 738);
			this.formTabControl.TabIndex = 0;
			// 
			// mtgPage
			// 
			this.mtgPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.mtgPage.Controls.Add(this.mtgTabControl);
			this.mtgPage.Location = new System.Drawing.Point(4, 33);
			this.mtgPage.Name = "mtgPage";
			this.mtgPage.Padding = new System.Windows.Forms.Padding(3);
			this.mtgPage.Size = new System.Drawing.Size(1276, 701);
			this.mtgPage.TabIndex = 1;
			this.mtgPage.Text = "MTG";
			// 
			// mtgTabControl
			// 
			this.mtgTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.mtgTabControl.Controls.Add(this.mtgSetPage);
			this.mtgTabControl.Controls.Add(this.mtgSearchPage);
			this.mtgTabControl.Controls.Add(this.mtgCatalogPage);
			this.mtgTabControl.Controls.Add(this.mtgDetailPage);
			this.mtgTabControl.Controls.Add(this.mtgCardPage);
			this.mtgTabControl.Controls.Add(this.mtgPrintPage);
			this.mtgTabControl.Controls.Add(this.mtgSymbolsPage);
			this.mtgTabControl.Controls.Add(this.mtgSetsPage);
			this.mtgTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mtgTabControl.Location = new System.Drawing.Point(3, 3);
			this.mtgTabControl.Name = "mtgTabControl";
			this.mtgTabControl.SelectedIndex = 0;
			this.mtgTabControl.Size = new System.Drawing.Size(1270, 695);
			this.mtgTabControl.TabIndex = 0;
			// 
			// mtgSetPage
			// 
			this.mtgSetPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.mtgSetPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.mtgSetPage.Controls.Add(this.mtgPrevSetButton);
			this.mtgSetPage.Controls.Add(this.mtgSetPageLabel);
			this.mtgSetPage.Controls.Add(this.mtgNextSetButton);
			this.mtgSetPage.Controls.Add(this.mtgSetlistLabel);
			this.mtgSetPage.Controls.Add(this.mtgSetLayout);
			this.mtgSetPage.Location = new System.Drawing.Point(4, 33);
			this.mtgSetPage.Name = "mtgSetPage";
			this.mtgSetPage.Padding = new System.Windows.Forms.Padding(3);
			this.mtgSetPage.Size = new System.Drawing.Size(1262, 658);
			this.mtgSetPage.TabIndex = 3;
			this.mtgSetPage.Text = "Set List";
			// 
			// mtgPrevSetButton
			// 
			this.mtgPrevSetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.mtgPrevSetButton.Location = new System.Drawing.Point(1060, 5);
			this.mtgPrevSetButton.Name = "mtgPrevSetButton";
			this.mtgPrevSetButton.Size = new System.Drawing.Size(60, 29);
			this.mtgPrevSetButton.TabIndex = 7;
			this.mtgPrevSetButton.Text = "<";
			this.mtgPrevSetButton.UseVisualStyleBackColor = true;
			this.mtgPrevSetButton.Click += new System.EventHandler(this.MTG_OnClickPrevSet);
			// 
			// mtgSetPageLabel
			// 
			this.mtgSetPageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.mtgSetPageLabel.Location = new System.Drawing.Point(1125, 5);
			this.mtgSetPageLabel.Name = "mtgSetPageLabel";
			this.mtgSetPageLabel.Size = new System.Drawing.Size(60, 30);
			this.mtgSetPageLabel.TabIndex = 6;
			this.mtgSetPageLabel.Text = "X / X";
			this.mtgSetPageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// mtgNextSetButton
			// 
			this.mtgNextSetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.mtgNextSetButton.Location = new System.Drawing.Point(1190, 5);
			this.mtgNextSetButton.Name = "mtgNextSetButton";
			this.mtgNextSetButton.Size = new System.Drawing.Size(60, 29);
			this.mtgNextSetButton.TabIndex = 5;
			this.mtgNextSetButton.Text = ">";
			this.mtgNextSetButton.UseVisualStyleBackColor = true;
			this.mtgNextSetButton.Click += new System.EventHandler(this.MTG_OnClickNextSet);
			// 
			// mtgSetlistLabel
			// 
			this.mtgSetlistLabel.AutoSize = true;
			this.mtgSetlistLabel.Location = new System.Drawing.Point(5, 8);
			this.mtgSetlistLabel.Name = "mtgSetlistLabel";
			this.mtgSetlistLabel.Size = new System.Drawing.Size(60, 21);
			this.mtgSetlistLabel.TabIndex = 1;
			this.mtgSetlistLabel.Text = "Set List";
			this.mtgSetlistLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgSetLayout
			// 
			this.mtgSetLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mtgSetLayout.AutoScroll = true;
			this.mtgSetLayout.BackColor = System.Drawing.SystemColors.ControlDark;
			this.mtgSetLayout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.mtgSetLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.mtgSetLayout.Location = new System.Drawing.Point(3, 38);
			this.mtgSetLayout.Name = "mtgSetLayout";
			this.mtgSetLayout.Size = new System.Drawing.Size(1252, 612);
			this.mtgSetLayout.TabIndex = 0;
			this.mtgSetLayout.WrapContents = false;
			// 
			// mtgSearchPage
			// 
			this.mtgSearchPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.mtgSearchPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.mtgSearchPage.Controls.Add(this.mtgSearchSetField);
			this.mtgSearchPage.Controls.Add(this.mtgSearchSetLabel);
			this.mtgSearchPage.Controls.Add(this.mtgSearchLocationField);
			this.mtgSearchPage.Controls.Add(this.mtgSearchLocationLabel);
			this.mtgSearchPage.Controls.Add(this.mtgSearchButton);
			this.mtgSearchPage.Controls.Add(this.mtgSearchOracleField);
			this.mtgSearchPage.Controls.Add(this.mtgSearchTypeField);
			this.mtgSearchPage.Controls.Add(this.mtgSearchColImgG);
			this.mtgSearchPage.Controls.Add(this.mtgSearchColImgR);
			this.mtgSearchPage.Controls.Add(this.mtgSearchColImgB);
			this.mtgSearchPage.Controls.Add(this.mtgSearchColImgU);
			this.mtgSearchPage.Controls.Add(this.mtgSearchColImgW);
			this.mtgSearchPage.Controls.Add(this.mtgSearchColG);
			this.mtgSearchPage.Controls.Add(this.mtgSearchColR);
			this.mtgSearchPage.Controls.Add(this.mtgSearchColB);
			this.mtgSearchPage.Controls.Add(this.mtgSearchColU);
			this.mtgSearchPage.Controls.Add(this.mtgSearchColW);
			this.mtgSearchPage.Controls.Add(this.mtgSearchIDImgG);
			this.mtgSearchPage.Controls.Add(this.mtgSearchIDImgR);
			this.mtgSearchPage.Controls.Add(this.mtgSearchIDImgB);
			this.mtgSearchPage.Controls.Add(this.mtgSearchIDImgU);
			this.mtgSearchPage.Controls.Add(this.mtgSearchIDImgW);
			this.mtgSearchPage.Controls.Add(this.mtgSearchIDG);
			this.mtgSearchPage.Controls.Add(this.mtgSearchIDR);
			this.mtgSearchPage.Controls.Add(this.mtgSearchIDB);
			this.mtgSearchPage.Controls.Add(this.mtgSearchIDU);
			this.mtgSearchPage.Controls.Add(this.mtgSearchIDW);
			this.mtgSearchPage.Controls.Add(this.mtgSearchNameLabel);
			this.mtgSearchPage.Controls.Add(this.mtgSearchNameField);
			this.mtgSearchPage.Controls.Add(this.mtgSearchOracleLabel);
			this.mtgSearchPage.Controls.Add(this.mtgSearchIdentityLabel);
			this.mtgSearchPage.Controls.Add(this.mtgSearchColourLabel);
			this.mtgSearchPage.Controls.Add(this.mtgSearchTypeLabel);
			this.mtgSearchPage.Location = new System.Drawing.Point(4, 25);
			this.mtgSearchPage.Name = "mtgSearchPage";
			this.mtgSearchPage.Padding = new System.Windows.Forms.Padding(3);
			this.mtgSearchPage.Size = new System.Drawing.Size(1262, 666);
			this.mtgSearchPage.TabIndex = 7;
			this.mtgSearchPage.Text = "Search";
			// 
			// mtgSearchSetField
			// 
			this.mtgSearchSetField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.mtgSearchSetField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.mtgSearchSetField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mtgSearchSetField.FormattingEnabled = true;
			this.mtgSearchSetField.Location = new System.Drawing.Point(100, 180);
			this.mtgSearchSetField.Name = "mtgSearchSetField";
			this.mtgSearchSetField.Size = new System.Drawing.Size(370, 29);
			this.mtgSearchSetField.Sorted = true;
			this.mtgSearchSetField.TabIndex = 77;
			// 
			// mtgSearchSetLabel
			// 
			this.mtgSearchSetLabel.Location = new System.Drawing.Point(5, 180);
			this.mtgSearchSetLabel.Name = "mtgSearchSetLabel";
			this.mtgSearchSetLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgSearchSetLabel.TabIndex = 76;
			this.mtgSearchSetLabel.Text = "Set:";
			this.mtgSearchSetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgSearchLocationField
			// 
			this.mtgSearchLocationField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.mtgSearchLocationField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.mtgSearchLocationField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mtgSearchLocationField.FormattingEnabled = true;
			this.mtgSearchLocationField.Location = new System.Drawing.Point(100, 215);
			this.mtgSearchLocationField.Name = "mtgSearchLocationField";
			this.mtgSearchLocationField.Size = new System.Drawing.Size(370, 29);
			this.mtgSearchLocationField.TabIndex = 75;
			// 
			// mtgSearchLocationLabel
			// 
			this.mtgSearchLocationLabel.Location = new System.Drawing.Point(5, 215);
			this.mtgSearchLocationLabel.Name = "mtgSearchLocationLabel";
			this.mtgSearchLocationLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgSearchLocationLabel.TabIndex = 74;
			this.mtgSearchLocationLabel.Text = "Location:";
			this.mtgSearchLocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgSearchButton
			// 
			this.mtgSearchButton.Location = new System.Drawing.Point(100, 250);
			this.mtgSearchButton.Name = "mtgSearchButton";
			this.mtgSearchButton.Size = new System.Drawing.Size(120, 30);
			this.mtgSearchButton.TabIndex = 73;
			this.mtgSearchButton.Text = "Search";
			this.mtgSearchButton.UseVisualStyleBackColor = true;
			this.mtgSearchButton.Click += new System.EventHandler(this.MTG_Search);
			// 
			// mtgSearchOracleField
			// 
			this.mtgSearchOracleField.Location = new System.Drawing.Point(100, 145);
			this.mtgSearchOracleField.Name = "mtgSearchOracleField";
			this.mtgSearchOracleField.Size = new System.Drawing.Size(370, 29);
			this.mtgSearchOracleField.TabIndex = 72;
			// 
			// mtgSearchTypeField
			// 
			this.mtgSearchTypeField.Location = new System.Drawing.Point(100, 110);
			this.mtgSearchTypeField.Name = "mtgSearchTypeField";
			this.mtgSearchTypeField.Size = new System.Drawing.Size(370, 29);
			this.mtgSearchTypeField.TabIndex = 59;
			// 
			// mtgSearchColImgG
			// 
			this.mtgSearchColImgG.Location = new System.Drawing.Point(320, 75);
			this.mtgSearchColImgG.Name = "mtgSearchColImgG";
			this.mtgSearchColImgG.Size = new System.Drawing.Size(30, 30);
			this.mtgSearchColImgG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgSearchColImgG.TabIndex = 71;
			this.mtgSearchColImgG.TabStop = false;
			// 
			// mtgSearchColImgR
			// 
			this.mtgSearchColImgR.Location = new System.Drawing.Point(270, 75);
			this.mtgSearchColImgR.Name = "mtgSearchColImgR";
			this.mtgSearchColImgR.Size = new System.Drawing.Size(30, 30);
			this.mtgSearchColImgR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgSearchColImgR.TabIndex = 70;
			this.mtgSearchColImgR.TabStop = false;
			// 
			// mtgSearchColImgB
			// 
			this.mtgSearchColImgB.Location = new System.Drawing.Point(220, 75);
			this.mtgSearchColImgB.Name = "mtgSearchColImgB";
			this.mtgSearchColImgB.Size = new System.Drawing.Size(30, 30);
			this.mtgSearchColImgB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgSearchColImgB.TabIndex = 69;
			this.mtgSearchColImgB.TabStop = false;
			// 
			// mtgSearchColImgU
			// 
			this.mtgSearchColImgU.Location = new System.Drawing.Point(170, 75);
			this.mtgSearchColImgU.Name = "mtgSearchColImgU";
			this.mtgSearchColImgU.Size = new System.Drawing.Size(30, 30);
			this.mtgSearchColImgU.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgSearchColImgU.TabIndex = 68;
			this.mtgSearchColImgU.TabStop = false;
			// 
			// mtgSearchColImgW
			// 
			this.mtgSearchColImgW.Location = new System.Drawing.Point(120, 75);
			this.mtgSearchColImgW.Name = "mtgSearchColImgW";
			this.mtgSearchColImgW.Size = new System.Drawing.Size(30, 30);
			this.mtgSearchColImgW.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgSearchColImgW.TabIndex = 67;
			this.mtgSearchColImgW.TabStop = false;
			// 
			// mtgSearchColG
			// 
			this.mtgSearchColG.Location = new System.Drawing.Point(305, 75);
			this.mtgSearchColG.Name = "mtgSearchColG";
			this.mtgSearchColG.Size = new System.Drawing.Size(14, 30);
			this.mtgSearchColG.TabIndex = 57;
			this.mtgSearchColG.UseVisualStyleBackColor = true;
			// 
			// mtgSearchColR
			// 
			this.mtgSearchColR.Location = new System.Drawing.Point(255, 75);
			this.mtgSearchColR.Name = "mtgSearchColR";
			this.mtgSearchColR.Size = new System.Drawing.Size(14, 30);
			this.mtgSearchColR.TabIndex = 56;
			this.mtgSearchColR.UseVisualStyleBackColor = true;
			// 
			// mtgSearchColB
			// 
			this.mtgSearchColB.Location = new System.Drawing.Point(205, 75);
			this.mtgSearchColB.Name = "mtgSearchColB";
			this.mtgSearchColB.Size = new System.Drawing.Size(14, 30);
			this.mtgSearchColB.TabIndex = 55;
			this.mtgSearchColB.UseVisualStyleBackColor = true;
			// 
			// mtgSearchColU
			// 
			this.mtgSearchColU.Location = new System.Drawing.Point(155, 75);
			this.mtgSearchColU.Name = "mtgSearchColU";
			this.mtgSearchColU.Size = new System.Drawing.Size(14, 30);
			this.mtgSearchColU.TabIndex = 54;
			this.mtgSearchColU.UseVisualStyleBackColor = true;
			// 
			// mtgSearchColW
			// 
			this.mtgSearchColW.Location = new System.Drawing.Point(105, 75);
			this.mtgSearchColW.Name = "mtgSearchColW";
			this.mtgSearchColW.Size = new System.Drawing.Size(14, 30);
			this.mtgSearchColW.TabIndex = 53;
			this.mtgSearchColW.UseVisualStyleBackColor = true;
			// 
			// mtgSearchIDImgG
			// 
			this.mtgSearchIDImgG.Location = new System.Drawing.Point(320, 40);
			this.mtgSearchIDImgG.Name = "mtgSearchIDImgG";
			this.mtgSearchIDImgG.Size = new System.Drawing.Size(30, 30);
			this.mtgSearchIDImgG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgSearchIDImgG.TabIndex = 66;
			this.mtgSearchIDImgG.TabStop = false;
			// 
			// mtgSearchIDImgR
			// 
			this.mtgSearchIDImgR.Location = new System.Drawing.Point(270, 40);
			this.mtgSearchIDImgR.Name = "mtgSearchIDImgR";
			this.mtgSearchIDImgR.Size = new System.Drawing.Size(30, 30);
			this.mtgSearchIDImgR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgSearchIDImgR.TabIndex = 65;
			this.mtgSearchIDImgR.TabStop = false;
			// 
			// mtgSearchIDImgB
			// 
			this.mtgSearchIDImgB.Location = new System.Drawing.Point(220, 40);
			this.mtgSearchIDImgB.Name = "mtgSearchIDImgB";
			this.mtgSearchIDImgB.Size = new System.Drawing.Size(30, 30);
			this.mtgSearchIDImgB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgSearchIDImgB.TabIndex = 64;
			this.mtgSearchIDImgB.TabStop = false;
			// 
			// mtgSearchIDImgU
			// 
			this.mtgSearchIDImgU.Location = new System.Drawing.Point(170, 40);
			this.mtgSearchIDImgU.Name = "mtgSearchIDImgU";
			this.mtgSearchIDImgU.Size = new System.Drawing.Size(30, 30);
			this.mtgSearchIDImgU.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgSearchIDImgU.TabIndex = 63;
			this.mtgSearchIDImgU.TabStop = false;
			// 
			// mtgSearchIDImgW
			// 
			this.mtgSearchIDImgW.Location = new System.Drawing.Point(120, 40);
			this.mtgSearchIDImgW.Name = "mtgSearchIDImgW";
			this.mtgSearchIDImgW.Size = new System.Drawing.Size(30, 30);
			this.mtgSearchIDImgW.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgSearchIDImgW.TabIndex = 62;
			this.mtgSearchIDImgW.TabStop = false;
			// 
			// mtgSearchIDG
			// 
			this.mtgSearchIDG.Location = new System.Drawing.Point(305, 40);
			this.mtgSearchIDG.Name = "mtgSearchIDG";
			this.mtgSearchIDG.Size = new System.Drawing.Size(14, 30);
			this.mtgSearchIDG.TabIndex = 51;
			this.mtgSearchIDG.UseVisualStyleBackColor = true;
			// 
			// mtgSearchIDR
			// 
			this.mtgSearchIDR.Location = new System.Drawing.Point(255, 40);
			this.mtgSearchIDR.Name = "mtgSearchIDR";
			this.mtgSearchIDR.Size = new System.Drawing.Size(14, 30);
			this.mtgSearchIDR.TabIndex = 50;
			this.mtgSearchIDR.UseVisualStyleBackColor = true;
			// 
			// mtgSearchIDB
			// 
			this.mtgSearchIDB.Location = new System.Drawing.Point(205, 40);
			this.mtgSearchIDB.Name = "mtgSearchIDB";
			this.mtgSearchIDB.Size = new System.Drawing.Size(14, 30);
			this.mtgSearchIDB.TabIndex = 49;
			this.mtgSearchIDB.UseVisualStyleBackColor = true;
			// 
			// mtgSearchIDU
			// 
			this.mtgSearchIDU.Location = new System.Drawing.Point(155, 40);
			this.mtgSearchIDU.Name = "mtgSearchIDU";
			this.mtgSearchIDU.Size = new System.Drawing.Size(14, 30);
			this.mtgSearchIDU.TabIndex = 48;
			this.mtgSearchIDU.UseVisualStyleBackColor = true;
			// 
			// mtgSearchIDW
			// 
			this.mtgSearchIDW.Location = new System.Drawing.Point(105, 40);
			this.mtgSearchIDW.Name = "mtgSearchIDW";
			this.mtgSearchIDW.Size = new System.Drawing.Size(14, 30);
			this.mtgSearchIDW.TabIndex = 47;
			this.mtgSearchIDW.UseVisualStyleBackColor = true;
			// 
			// mtgSearchNameLabel
			// 
			this.mtgSearchNameLabel.Location = new System.Drawing.Point(5, 5);
			this.mtgSearchNameLabel.Name = "mtgSearchNameLabel";
			this.mtgSearchNameLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgSearchNameLabel.TabIndex = 44;
			this.mtgSearchNameLabel.Text = "Name:";
			this.mtgSearchNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgSearchNameField
			// 
			this.mtgSearchNameField.Location = new System.Drawing.Point(100, 5);
			this.mtgSearchNameField.Name = "mtgSearchNameField";
			this.mtgSearchNameField.Size = new System.Drawing.Size(370, 29);
			this.mtgSearchNameField.TabIndex = 45;
			// 
			// mtgSearchOracleLabel
			// 
			this.mtgSearchOracleLabel.Location = new System.Drawing.Point(5, 145);
			this.mtgSearchOracleLabel.Name = "mtgSearchOracleLabel";
			this.mtgSearchOracleLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgSearchOracleLabel.TabIndex = 60;
			this.mtgSearchOracleLabel.Text = "Oracle Text:";
			this.mtgSearchOracleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgSearchIdentityLabel
			// 
			this.mtgSearchIdentityLabel.Location = new System.Drawing.Point(5, 40);
			this.mtgSearchIdentityLabel.Name = "mtgSearchIdentityLabel";
			this.mtgSearchIdentityLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgSearchIdentityLabel.TabIndex = 46;
			this.mtgSearchIdentityLabel.Text = "Identity:";
			this.mtgSearchIdentityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgSearchColourLabel
			// 
			this.mtgSearchColourLabel.Location = new System.Drawing.Point(5, 75);
			this.mtgSearchColourLabel.Name = "mtgSearchColourLabel";
			this.mtgSearchColourLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgSearchColourLabel.TabIndex = 52;
			this.mtgSearchColourLabel.Text = "Colours:";
			this.mtgSearchColourLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgSearchTypeLabel
			// 
			this.mtgSearchTypeLabel.Location = new System.Drawing.Point(5, 110);
			this.mtgSearchTypeLabel.Name = "mtgSearchTypeLabel";
			this.mtgSearchTypeLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgSearchTypeLabel.TabIndex = 58;
			this.mtgSearchTypeLabel.Text = "Card Types:";
			this.mtgSearchTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgCatalogPage
			// 
			this.mtgCatalogPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.mtgCatalogPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.mtgCatalogPage.Controls.Add(this.mtgSortAlphabeticalButton);
			this.mtgCatalogPage.Controls.Add(this.mtgSortNumericButton);
			this.mtgCatalogPage.Controls.Add(this.mtgCatalogNextButton);
			this.mtgCatalogPage.Controls.Add(this.mtgCatalogPrevButton);
			this.mtgCatalogPage.Controls.Add(this.mtgCatalogLayout);
			this.mtgCatalogPage.Controls.Add(this.mtgCatalogIndex);
			this.mtgCatalogPage.Location = new System.Drawing.Point(4, 25);
			this.mtgCatalogPage.Name = "mtgCatalogPage";
			this.mtgCatalogPage.Padding = new System.Windows.Forms.Padding(3);
			this.mtgCatalogPage.Size = new System.Drawing.Size(1262, 666);
			this.mtgCatalogPage.TabIndex = 2;
			this.mtgCatalogPage.Text = "Catalog";
			// 
			// mtgSortAlphabeticalButton
			// 
			this.mtgSortAlphabeticalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.mtgSortAlphabeticalButton.AutoSize = true;
			this.mtgSortAlphabeticalButton.Location = new System.Drawing.Point(929, 8);
			this.mtgSortAlphabeticalButton.Name = "mtgSortAlphabeticalButton";
			this.mtgSortAlphabeticalButton.Size = new System.Drawing.Size(113, 25);
			this.mtgSortAlphabeticalButton.TabIndex = 5;
			this.mtgSortAlphabeticalButton.Text = "Alphabetical";
			this.mtgSortAlphabeticalButton.UseVisualStyleBackColor = true;
			this.mtgSortAlphabeticalButton.CheckedChanged += new System.EventHandler(this.MTG_OnClickSortAlphabetical);
			// 
			// mtgSortNumericButton
			// 
			this.mtgSortNumericButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.mtgSortNumericButton.AutoSize = true;
			this.mtgSortNumericButton.Checked = true;
			this.mtgSortNumericButton.Location = new System.Drawing.Point(835, 7);
			this.mtgSortNumericButton.Name = "mtgSortNumericButton";
			this.mtgSortNumericButton.Size = new System.Drawing.Size(88, 25);
			this.mtgSortNumericButton.TabIndex = 4;
			this.mtgSortNumericButton.TabStop = true;
			this.mtgSortNumericButton.Text = "Numeric";
			this.mtgSortNumericButton.UseVisualStyleBackColor = true;
			this.mtgSortNumericButton.CheckedChanged += new System.EventHandler(this.MTG_OnClickSortNumeric);
			// 
			// mtgCatalogNextButton
			// 
			this.mtgCatalogNextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.mtgCatalogNextButton.Location = new System.Drawing.Point(1178, 5);
			this.mtgCatalogNextButton.Name = "mtgCatalogNextButton";
			this.mtgCatalogNextButton.Size = new System.Drawing.Size(75, 29);
			this.mtgCatalogNextButton.TabIndex = 2;
			this.mtgCatalogNextButton.Text = ">";
			this.mtgCatalogNextButton.UseVisualStyleBackColor = true;
			this.mtgCatalogNextButton.Click += new System.EventHandler(this.MTG_OnClickCatalogNext);
			// 
			// mtgCatalogPrevButton
			// 
			this.mtgCatalogPrevButton.Location = new System.Drawing.Point(5, 5);
			this.mtgCatalogPrevButton.Name = "mtgCatalogPrevButton";
			this.mtgCatalogPrevButton.Size = new System.Drawing.Size(75, 29);
			this.mtgCatalogPrevButton.TabIndex = 1;
			this.mtgCatalogPrevButton.Text = "<";
			this.mtgCatalogPrevButton.UseVisualStyleBackColor = true;
			this.mtgCatalogPrevButton.Click += new System.EventHandler(this.MTG_OnClickCatalogPrev);
			// 
			// mtgCatalogLayout
			// 
			this.mtgCatalogLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mtgCatalogLayout.AutoScroll = true;
			this.mtgCatalogLayout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.mtgCatalogLayout.Location = new System.Drawing.Point(3, 38);
			this.mtgCatalogLayout.Name = "mtgCatalogLayout";
			this.mtgCatalogLayout.Size = new System.Drawing.Size(1252, 620);
			this.mtgCatalogLayout.TabIndex = 0;
			// 
			// mtgCatalogIndex
			// 
			this.mtgCatalogIndex.Dock = System.Windows.Forms.DockStyle.Top;
			this.mtgCatalogIndex.Location = new System.Drawing.Point(3, 3);
			this.mtgCatalogIndex.Name = "mtgCatalogIndex";
			this.mtgCatalogIndex.Size = new System.Drawing.Size(1252, 30);
			this.mtgCatalogIndex.TabIndex = 3;
			this.mtgCatalogIndex.Text = "Showing 0 - 0 of 0";
			this.mtgCatalogIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// mtgDetailPage
			// 
			this.mtgDetailPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.mtgDetailPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.mtgDetailPage.Controls.Add(this.mtgDetailFilterCountLabel);
			this.mtgDetailPage.Controls.Add(this.mtgPrintingsBox);
			this.mtgDetailPage.Controls.Add(this.mtgDetailDialog);
			this.mtgDetailPage.Controls.Add(this.mtgDetailAutogenButton);
			this.mtgDetailPage.Controls.Add(this.mtgDeletePrintingButton);
			this.mtgDetailPage.Controls.Add(this.mtgDetailNextButton);
			this.mtgDetailPage.Controls.Add(this.mtgDetailPrevButton);
			this.mtgDetailPage.Controls.Add(this.mtgCardtipBox);
			this.mtgDetailPage.Controls.Add(this.mtgTooltipBox);
			this.mtgDetailPage.Controls.Add(this.mtgDetailBox);
			this.mtgDetailPage.Controls.Add(this.mtgDetailImgbox);
			this.mtgDetailPage.Location = new System.Drawing.Point(4, 25);
			this.mtgDetailPage.Name = "mtgDetailPage";
			this.mtgDetailPage.Size = new System.Drawing.Size(1262, 666);
			this.mtgDetailPage.TabIndex = 4;
			this.mtgDetailPage.Text = "Card Details";
			// 
			// mtgDetailFilterCountLabel
			// 
			this.mtgDetailFilterCountLabel.Location = new System.Drawing.Point(10, 585);
			this.mtgDetailFilterCountLabel.MinimumSize = new System.Drawing.Size(0, 29);
			this.mtgDetailFilterCountLabel.Name = "mtgDetailFilterCountLabel";
			this.mtgDetailFilterCountLabel.Size = new System.Drawing.Size(390, 29);
			this.mtgDetailFilterCountLabel.TabIndex = 29;
			this.mtgDetailFilterCountLabel.Text = "X / X";
			this.mtgDetailFilterCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// mtgPrintingsBox
			// 
			this.mtgPrintingsBox.Location = new System.Drawing.Point(865, 5);
			this.mtgPrintingsBox.Name = "mtgPrintingsBox";
			this.mtgPrintingsBox.Size = new System.Drawing.Size(380, 100);
			this.mtgPrintingsBox.TabIndex = 6;
			this.mtgPrintingsBox.TabStop = false;
			// 
			// mtgDetailDialog
			// 
			this.mtgDetailDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.mtgDetailDialog.AutoSize = true;
			this.mtgDetailDialog.Location = new System.Drawing.Point(261, 630);
			this.mtgDetailDialog.MinimumSize = new System.Drawing.Size(0, 29);
			this.mtgDetailDialog.Name = "mtgDetailDialog";
			this.mtgDetailDialog.Size = new System.Drawing.Size(16, 29);
			this.mtgDetailDialog.TabIndex = 27;
			this.mtgDetailDialog.Text = "-";
			this.mtgDetailDialog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgDetailAutogenButton
			// 
			this.mtgDetailAutogenButton.Location = new System.Drawing.Point(130, 550);
			this.mtgDetailAutogenButton.Name = "mtgDetailAutogenButton";
			this.mtgDetailAutogenButton.Size = new System.Drawing.Size(150, 29);
			this.mtgDetailAutogenButton.TabIndex = 26;
			this.mtgDetailAutogenButton.Text = "Autogen";
			this.mtgDetailAutogenButton.UseVisualStyleBackColor = true;
			this.mtgDetailAutogenButton.Click += new System.EventHandler(this.MTG_AutogenDetailRef);
			// 
			// mtgDeletePrintingButton
			// 
			this.mtgDeletePrintingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.mtgDeletePrintingButton.Location = new System.Drawing.Point(5, 630);
			this.mtgDeletePrintingButton.Name = "mtgDeletePrintingButton";
			this.mtgDeletePrintingButton.Size = new System.Drawing.Size(250, 29);
			this.mtgDeletePrintingButton.TabIndex = 25;
			this.mtgDeletePrintingButton.Text = "Delete Printing!";
			this.mtgDeletePrintingButton.UseVisualStyleBackColor = true;
			this.mtgDeletePrintingButton.Click += new System.EventHandler(this.MTG_DeleteCurrentPrinting);
			// 
			// mtgDetailNextButton
			// 
			this.mtgDetailNextButton.Location = new System.Drawing.Point(285, 550);
			this.mtgDetailNextButton.Name = "mtgDetailNextButton";
			this.mtgDetailNextButton.Size = new System.Drawing.Size(120, 29);
			this.mtgDetailNextButton.TabIndex = 10;
			this.mtgDetailNextButton.Text = "Next Card";
			this.mtgDetailNextButton.UseVisualStyleBackColor = true;
			this.mtgDetailNextButton.Click += new System.EventHandler(this.MTG_LoadNextInSelection);
			// 
			// mtgDetailPrevButton
			// 
			this.mtgDetailPrevButton.Location = new System.Drawing.Point(5, 550);
			this.mtgDetailPrevButton.Name = "mtgDetailPrevButton";
			this.mtgDetailPrevButton.Size = new System.Drawing.Size(120, 29);
			this.mtgDetailPrevButton.TabIndex = 9;
			this.mtgDetailPrevButton.Text = "Previous Card";
			this.mtgDetailPrevButton.UseVisualStyleBackColor = true;
			this.mtgDetailPrevButton.Click += new System.EventHandler(this.MTG_LoadPreviousInSelection);
			// 
			// mtgCardtipBox
			// 
			this.mtgCardtipBox.Controls.Add(this.mtgCardtipImage);
			this.mtgCardtipBox.Location = new System.Drawing.Point(865, 215);
			this.mtgCardtipBox.Name = "mtgCardtipBox";
			this.mtgCardtipBox.Size = new System.Drawing.Size(250, 350);
			this.mtgCardtipBox.TabIndex = 6;
			this.mtgCardtipBox.TabStop = false;
			// 
			// mtgCardtipImage
			// 
			this.mtgCardtipImage.Location = new System.Drawing.Point(0, 0);
			this.mtgCardtipImage.Name = "mtgCardtipImage";
			this.mtgCardtipImage.Size = new System.Drawing.Size(250, 350);
			this.mtgCardtipImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgCardtipImage.TabIndex = 0;
			this.mtgCardtipImage.TabStop = false;
			// 
			// mtgTooltipBox
			// 
			this.mtgTooltipBox.Location = new System.Drawing.Point(865, 110);
			this.mtgTooltipBox.Name = "mtgTooltipBox";
			this.mtgTooltipBox.Size = new System.Drawing.Size(300, 100);
			this.mtgTooltipBox.TabIndex = 5;
			this.mtgTooltipBox.TabStop = false;
			// 
			// mtgDetailBox
			// 
			this.mtgDetailBox.Controls.Add(this.mtgReloadLocationsButton);
			this.mtgDetailBox.Controls.Add(this.mtgEditPrintButton);
			this.mtgDetailBox.Controls.Add(this.mtgEditCardButton);
			this.mtgDetailBox.Controls.Add(this.mtgMoveLabel);
			this.mtgDetailBox.Controls.Add(this.mtgLocationTable);
			this.mtgDetailBox.Controls.Add(this.mtgMoveField);
			this.mtgDetailBox.Location = new System.Drawing.Point(410, 5);
			this.mtgDetailBox.Name = "mtgDetailBox";
			this.mtgDetailBox.Size = new System.Drawing.Size(450, 540);
			this.mtgDetailBox.TabIndex = 4;
			this.mtgDetailBox.TabStop = false;
			this.mtgDetailBox.Text = "Owned Printings";
			// 
			// mtgReloadLocationsButton
			// 
			this.mtgReloadLocationsButton.Location = new System.Drawing.Point(295, 25);
			this.mtgReloadLocationsButton.Name = "mtgReloadLocationsButton";
			this.mtgReloadLocationsButton.Size = new System.Drawing.Size(150, 29);
			this.mtgReloadLocationsButton.TabIndex = 8;
			this.mtgReloadLocationsButton.Text = "Reload Locations";
			this.mtgReloadLocationsButton.UseVisualStyleBackColor = true;
			this.mtgReloadLocationsButton.Click += new System.EventHandler(this.MTG_OnClickReloadLocations);
			// 
			// mtgEditPrintButton
			// 
			this.mtgEditPrintButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.mtgEditPrintButton.Location = new System.Drawing.Point(300, 505);
			this.mtgEditPrintButton.Name = "mtgEditPrintButton";
			this.mtgEditPrintButton.Size = new System.Drawing.Size(145, 29);
			this.mtgEditPrintButton.TabIndex = 5;
			this.mtgEditPrintButton.Text = "Edit Printing Data";
			this.mtgEditPrintButton.UseVisualStyleBackColor = true;
			this.mtgEditPrintButton.Click += new System.EventHandler(this.MTG_EditPrint);
			// 
			// mtgEditCardButton
			// 
			this.mtgEditCardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.mtgEditCardButton.Location = new System.Drawing.Point(150, 505);
			this.mtgEditCardButton.Name = "mtgEditCardButton";
			this.mtgEditCardButton.Size = new System.Drawing.Size(145, 29);
			this.mtgEditCardButton.TabIndex = 4;
			this.mtgEditCardButton.Text = "Edit Card Data";
			this.mtgEditCardButton.UseVisualStyleBackColor = true;
			this.mtgEditCardButton.Click += new System.EventHandler(this.MTG_EditCard);
			// 
			// mtgMoveLabel
			// 
			this.mtgMoveLabel.Location = new System.Drawing.Point(5, 25);
			this.mtgMoveLabel.Name = "mtgMoveLabel";
			this.mtgMoveLabel.Size = new System.Drawing.Size(70, 30);
			this.mtgMoveLabel.TabIndex = 3;
			this.mtgMoveLabel.Text = "Move to:";
			this.mtgMoveLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgLocationTable
			// 
			this.mtgLocationTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
			this.mtgLocationTable.ColumnCount = 4;
			this.mtgLocationTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.mtgLocationTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.mtgLocationTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.mtgLocationTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.mtgLocationTable.Location = new System.Drawing.Point(9, 60);
			this.mtgLocationTable.Name = "mtgLocationTable";
			this.mtgLocationTable.RowCount = 1;
			this.mtgLocationTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.mtgLocationTable.Size = new System.Drawing.Size(435, 30);
			this.mtgLocationTable.TabIndex = 1;
			// 
			// mtgMoveField
			// 
			this.mtgMoveField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.mtgMoveField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.mtgMoveField.FormattingEnabled = true;
			this.mtgMoveField.Location = new System.Drawing.Point(75, 25);
			this.mtgMoveField.Name = "mtgMoveField";
			this.mtgMoveField.Size = new System.Drawing.Size(215, 29);
			this.mtgMoveField.TabIndex = 2;
			// 
			// mtgDetailImgbox
			// 
			this.mtgDetailImgbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.mtgDetailImgbox.Location = new System.Drawing.Point(5, 5);
			this.mtgDetailImgbox.Name = "mtgDetailImgbox";
			this.mtgDetailImgbox.Size = new System.Drawing.Size(400, 540);
			this.mtgDetailImgbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgDetailImgbox.TabIndex = 0;
			this.mtgDetailImgbox.TabStop = false;
			this.mtgDetailImgbox.Click += new System.EventHandler(this.MTG_FlipCard);
			// 
			// mtgCardPage
			// 
			this.mtgCardPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.mtgCardPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.mtgCardPage.Controls.Add(this.mtgIgnoreDuplicateEntryLabel);
			this.mtgCardPage.Controls.Add(this.mtgIgnoreDuplicateEntryBox);
			this.mtgCardPage.Controls.Add(this.mtgCardTypeField);
			this.mtgCardPage.Controls.Add(this.mtgToughnessBackField);
			this.mtgCardPage.Controls.Add(this.mtgPowerBackField);
			this.mtgCardPage.Controls.Add(this.mtgAtkDefBackLabel);
			this.mtgCardPage.Controls.Add(this.mtgCostField);
			this.mtgCardPage.Controls.Add(this.mtgCostLabel);
			this.mtgCardPage.Controls.Add(this.mtgColourImgG);
			this.mtgCardPage.Controls.Add(this.mtgColourImgR);
			this.mtgCardPage.Controls.Add(this.mtgColourImgB);
			this.mtgCardPage.Controls.Add(this.mtgColourImgU);
			this.mtgCardPage.Controls.Add(this.mtgColourImgW);
			this.mtgCardPage.Controls.Add(this.mtgColourG);
			this.mtgCardPage.Controls.Add(this.mtgColourR);
			this.mtgCardPage.Controls.Add(this.mtgColourB);
			this.mtgCardPage.Controls.Add(this.mtgColourU);
			this.mtgCardPage.Controls.Add(this.mtgColourW);
			this.mtgCardPage.Controls.Add(this.mtgIdentityImgG);
			this.mtgCardPage.Controls.Add(this.mtgIdentityImgR);
			this.mtgCardPage.Controls.Add(this.mtgIdentityImgB);
			this.mtgCardPage.Controls.Add(this.mtgIdentityImgU);
			this.mtgCardPage.Controls.Add(this.mtgIdentityImgW);
			this.mtgCardPage.Controls.Add(this.mtgIdentityG);
			this.mtgCardPage.Controls.Add(this.mtgIdentityR);
			this.mtgCardPage.Controls.Add(this.mtgIdentityB);
			this.mtgCardPage.Controls.Add(this.mtgIdentityU);
			this.mtgCardPage.Controls.Add(this.mtgIdentityW);
			this.mtgCardPage.Controls.Add(this.mtgCardDialog);
			this.mtgCardPage.Controls.Add(this.mtgToughnessField);
			this.mtgCardPage.Controls.Add(this.mtgPowerField);
			this.mtgCardPage.Controls.Add(this.mtgAtkDefLabel);
			this.mtgCardPage.Controls.Add(this.mtgAddCardButton);
			this.mtgCardPage.Controls.Add(this.mtgNameLabel);
			this.mtgCardPage.Controls.Add(this.mtgNameField);
			this.mtgCardPage.Controls.Add(this.mtgOracleTextLabel);
			this.mtgCardPage.Controls.Add(this.mtgIdentityLabel);
			this.mtgCardPage.Controls.Add(this.mtgOracleTextField);
			this.mtgCardPage.Controls.Add(this.mtgColourLabel);
			this.mtgCardPage.Controls.Add(this.mtgCardTypeLabel);
			this.mtgCardPage.Location = new System.Drawing.Point(4, 25);
			this.mtgCardPage.Name = "mtgCardPage";
			this.mtgCardPage.Padding = new System.Windows.Forms.Padding(3);
			this.mtgCardPage.Size = new System.Drawing.Size(1262, 666);
			this.mtgCardPage.TabIndex = 0;
			this.mtgCardPage.Text = "Card Entry";
			// 
			// mtgIgnoreDuplicateEntryLabel
			// 
			this.mtgIgnoreDuplicateEntryLabel.Location = new System.Drawing.Point(115, 530);
			this.mtgIgnoreDuplicateEntryLabel.Name = "mtgIgnoreDuplicateEntryLabel";
			this.mtgIgnoreDuplicateEntryLabel.Size = new System.Drawing.Size(235, 30);
			this.mtgIgnoreDuplicateEntryLabel.TabIndex = 45;
			this.mtgIgnoreDuplicateEntryLabel.Text = "Ignore Duplicate Entry";
			this.mtgIgnoreDuplicateEntryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgIgnoreDuplicateEntryBox
			// 
			this.mtgIgnoreDuplicateEntryBox.Location = new System.Drawing.Point(100, 530);
			this.mtgIgnoreDuplicateEntryBox.Name = "mtgIgnoreDuplicateEntryBox";
			this.mtgIgnoreDuplicateEntryBox.Size = new System.Drawing.Size(14, 30);
			this.mtgIgnoreDuplicateEntryBox.TabIndex = 44;
			this.mtgIgnoreDuplicateEntryBox.UseVisualStyleBackColor = true;
			// 
			// mtgCardTypeField
			// 
			this.mtgCardTypeField.Location = new System.Drawing.Point(100, 145);
			this.mtgCardTypeField.Name = "mtgCardTypeField";
			this.mtgCardTypeField.Size = new System.Drawing.Size(370, 29);
			this.mtgCardTypeField.TabIndex = 18;
			// 
			// mtgToughnessBackField
			// 
			this.mtgToughnessBackField.Location = new System.Drawing.Point(227, 460);
			this.mtgToughnessBackField.Name = "mtgToughnessBackField";
			this.mtgToughnessBackField.Size = new System.Drawing.Size(123, 29);
			this.mtgToughnessBackField.TabIndex = 29;
			// 
			// mtgPowerBackField
			// 
			this.mtgPowerBackField.Location = new System.Drawing.Point(100, 460);
			this.mtgPowerBackField.Name = "mtgPowerBackField";
			this.mtgPowerBackField.Size = new System.Drawing.Size(123, 29);
			this.mtgPowerBackField.TabIndex = 28;
			// 
			// mtgAtkDefBackLabel
			// 
			this.mtgAtkDefBackLabel.Location = new System.Drawing.Point(5, 460);
			this.mtgAtkDefBackLabel.Name = "mtgAtkDefBackLabel";
			this.mtgAtkDefBackLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgAtkDefBackLabel.TabIndex = 27;
			this.mtgAtkDefBackLabel.Text = "Back P/T:";
			this.mtgAtkDefBackLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgCostField
			// 
			this.mtgCostField.Location = new System.Drawing.Point(100, 110);
			this.mtgCostField.Name = "mtgCostField";
			this.mtgCostField.Size = new System.Drawing.Size(370, 29);
			this.mtgCostField.TabIndex = 16;
			// 
			// mtgCostLabel
			// 
			this.mtgCostLabel.Location = new System.Drawing.Point(5, 110);
			this.mtgCostLabel.Name = "mtgCostLabel";
			this.mtgCostLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgCostLabel.TabIndex = 15;
			this.mtgCostLabel.Text = "Cost:";
			this.mtgCostLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgColourImgG
			// 
			this.mtgColourImgG.Location = new System.Drawing.Point(320, 75);
			this.mtgColourImgG.Name = "mtgColourImgG";
			this.mtgColourImgG.Size = new System.Drawing.Size(30, 30);
			this.mtgColourImgG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgColourImgG.TabIndex = 43;
			this.mtgColourImgG.TabStop = false;
			// 
			// mtgColourImgR
			// 
			this.mtgColourImgR.Location = new System.Drawing.Point(270, 75);
			this.mtgColourImgR.Name = "mtgColourImgR";
			this.mtgColourImgR.Size = new System.Drawing.Size(30, 30);
			this.mtgColourImgR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgColourImgR.TabIndex = 42;
			this.mtgColourImgR.TabStop = false;
			// 
			// mtgColourImgB
			// 
			this.mtgColourImgB.Location = new System.Drawing.Point(220, 75);
			this.mtgColourImgB.Name = "mtgColourImgB";
			this.mtgColourImgB.Size = new System.Drawing.Size(30, 30);
			this.mtgColourImgB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgColourImgB.TabIndex = 41;
			this.mtgColourImgB.TabStop = false;
			// 
			// mtgColourImgU
			// 
			this.mtgColourImgU.Location = new System.Drawing.Point(170, 75);
			this.mtgColourImgU.Name = "mtgColourImgU";
			this.mtgColourImgU.Size = new System.Drawing.Size(30, 30);
			this.mtgColourImgU.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgColourImgU.TabIndex = 40;
			this.mtgColourImgU.TabStop = false;
			// 
			// mtgColourImgW
			// 
			this.mtgColourImgW.Location = new System.Drawing.Point(120, 75);
			this.mtgColourImgW.Name = "mtgColourImgW";
			this.mtgColourImgW.Size = new System.Drawing.Size(30, 30);
			this.mtgColourImgW.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgColourImgW.TabIndex = 39;
			this.mtgColourImgW.TabStop = false;
			// 
			// mtgColourG
			// 
			this.mtgColourG.Location = new System.Drawing.Point(305, 75);
			this.mtgColourG.Name = "mtgColourG";
			this.mtgColourG.Size = new System.Drawing.Size(14, 30);
			this.mtgColourG.TabIndex = 14;
			this.mtgColourG.UseVisualStyleBackColor = true;
			// 
			// mtgColourR
			// 
			this.mtgColourR.Location = new System.Drawing.Point(255, 75);
			this.mtgColourR.Name = "mtgColourR";
			this.mtgColourR.Size = new System.Drawing.Size(14, 30);
			this.mtgColourR.TabIndex = 13;
			this.mtgColourR.UseVisualStyleBackColor = true;
			// 
			// mtgColourB
			// 
			this.mtgColourB.Location = new System.Drawing.Point(205, 75);
			this.mtgColourB.Name = "mtgColourB";
			this.mtgColourB.Size = new System.Drawing.Size(14, 30);
			this.mtgColourB.TabIndex = 12;
			this.mtgColourB.UseVisualStyleBackColor = true;
			// 
			// mtgColourU
			// 
			this.mtgColourU.Location = new System.Drawing.Point(155, 75);
			this.mtgColourU.Name = "mtgColourU";
			this.mtgColourU.Size = new System.Drawing.Size(14, 30);
			this.mtgColourU.TabIndex = 11;
			this.mtgColourU.UseVisualStyleBackColor = true;
			// 
			// mtgColourW
			// 
			this.mtgColourW.Location = new System.Drawing.Point(105, 75);
			this.mtgColourW.Name = "mtgColourW";
			this.mtgColourW.Size = new System.Drawing.Size(14, 30);
			this.mtgColourW.TabIndex = 10;
			this.mtgColourW.UseVisualStyleBackColor = true;
			// 
			// mtgIdentityImgG
			// 
			this.mtgIdentityImgG.Location = new System.Drawing.Point(320, 40);
			this.mtgIdentityImgG.Name = "mtgIdentityImgG";
			this.mtgIdentityImgG.Size = new System.Drawing.Size(30, 30);
			this.mtgIdentityImgG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgIdentityImgG.TabIndex = 33;
			this.mtgIdentityImgG.TabStop = false;
			// 
			// mtgIdentityImgR
			// 
			this.mtgIdentityImgR.Location = new System.Drawing.Point(270, 40);
			this.mtgIdentityImgR.Name = "mtgIdentityImgR";
			this.mtgIdentityImgR.Size = new System.Drawing.Size(30, 30);
			this.mtgIdentityImgR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgIdentityImgR.TabIndex = 32;
			this.mtgIdentityImgR.TabStop = false;
			// 
			// mtgIdentityImgB
			// 
			this.mtgIdentityImgB.Location = new System.Drawing.Point(220, 40);
			this.mtgIdentityImgB.Name = "mtgIdentityImgB";
			this.mtgIdentityImgB.Size = new System.Drawing.Size(30, 30);
			this.mtgIdentityImgB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgIdentityImgB.TabIndex = 31;
			this.mtgIdentityImgB.TabStop = false;
			// 
			// mtgIdentityImgU
			// 
			this.mtgIdentityImgU.Location = new System.Drawing.Point(170, 40);
			this.mtgIdentityImgU.Name = "mtgIdentityImgU";
			this.mtgIdentityImgU.Size = new System.Drawing.Size(30, 30);
			this.mtgIdentityImgU.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgIdentityImgU.TabIndex = 30;
			this.mtgIdentityImgU.TabStop = false;
			// 
			// mtgIdentityImgW
			// 
			this.mtgIdentityImgW.Location = new System.Drawing.Point(120, 40);
			this.mtgIdentityImgW.Name = "mtgIdentityImgW";
			this.mtgIdentityImgW.Size = new System.Drawing.Size(30, 30);
			this.mtgIdentityImgW.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgIdentityImgW.TabIndex = 29;
			this.mtgIdentityImgW.TabStop = false;
			// 
			// mtgIdentityG
			// 
			this.mtgIdentityG.Location = new System.Drawing.Point(305, 40);
			this.mtgIdentityG.Name = "mtgIdentityG";
			this.mtgIdentityG.Size = new System.Drawing.Size(14, 30);
			this.mtgIdentityG.TabIndex = 8;
			this.mtgIdentityG.UseVisualStyleBackColor = true;
			// 
			// mtgIdentityR
			// 
			this.mtgIdentityR.Location = new System.Drawing.Point(255, 40);
			this.mtgIdentityR.Name = "mtgIdentityR";
			this.mtgIdentityR.Size = new System.Drawing.Size(14, 30);
			this.mtgIdentityR.TabIndex = 7;
			this.mtgIdentityR.UseVisualStyleBackColor = true;
			// 
			// mtgIdentityB
			// 
			this.mtgIdentityB.Location = new System.Drawing.Point(205, 40);
			this.mtgIdentityB.Name = "mtgIdentityB";
			this.mtgIdentityB.Size = new System.Drawing.Size(14, 30);
			this.mtgIdentityB.TabIndex = 6;
			this.mtgIdentityB.UseVisualStyleBackColor = true;
			// 
			// mtgIdentityU
			// 
			this.mtgIdentityU.Location = new System.Drawing.Point(155, 40);
			this.mtgIdentityU.Name = "mtgIdentityU";
			this.mtgIdentityU.Size = new System.Drawing.Size(14, 30);
			this.mtgIdentityU.TabIndex = 5;
			this.mtgIdentityU.UseVisualStyleBackColor = true;
			// 
			// mtgIdentityW
			// 
			this.mtgIdentityW.Location = new System.Drawing.Point(105, 40);
			this.mtgIdentityW.Name = "mtgIdentityW";
			this.mtgIdentityW.Size = new System.Drawing.Size(14, 30);
			this.mtgIdentityW.TabIndex = 4;
			this.mtgIdentityW.UseVisualStyleBackColor = true;
			// 
			// mtgCardDialog
			// 
			this.mtgCardDialog.AutoSize = true;
			this.mtgCardDialog.Location = new System.Drawing.Point(355, 498);
			this.mtgCardDialog.Name = "mtgCardDialog";
			this.mtgCardDialog.Size = new System.Drawing.Size(16, 21);
			this.mtgCardDialog.TabIndex = 31;
			this.mtgCardDialog.Text = "-";
			this.mtgCardDialog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgToughnessField
			// 
			this.mtgToughnessField.Location = new System.Drawing.Point(227, 425);
			this.mtgToughnessField.Name = "mtgToughnessField";
			this.mtgToughnessField.Size = new System.Drawing.Size(123, 29);
			this.mtgToughnessField.TabIndex = 26;
			// 
			// mtgPowerField
			// 
			this.mtgPowerField.Location = new System.Drawing.Point(100, 425);
			this.mtgPowerField.Name = "mtgPowerField";
			this.mtgPowerField.Size = new System.Drawing.Size(123, 29);
			this.mtgPowerField.TabIndex = 25;
			// 
			// mtgAtkDefLabel
			// 
			this.mtgAtkDefLabel.Location = new System.Drawing.Point(5, 425);
			this.mtgAtkDefLabel.Name = "mtgAtkDefLabel";
			this.mtgAtkDefLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgAtkDefLabel.TabIndex = 24;
			this.mtgAtkDefLabel.Text = "Pow/Tough:";
			this.mtgAtkDefLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgAddCardButton
			// 
			this.mtgAddCardButton.Location = new System.Drawing.Point(100, 495);
			this.mtgAddCardButton.Name = "mtgAddCardButton";
			this.mtgAddCardButton.Size = new System.Drawing.Size(250, 30);
			this.mtgAddCardButton.TabIndex = 30;
			this.mtgAddCardButton.Text = "Add To Catalog";
			this.mtgAddCardButton.UseVisualStyleBackColor = true;
			this.mtgAddCardButton.Click += new System.EventHandler(this.MTG_OnClickAddCard);
			// 
			// mtgNameLabel
			// 
			this.mtgNameLabel.Location = new System.Drawing.Point(5, 5);
			this.mtgNameLabel.Name = "mtgNameLabel";
			this.mtgNameLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgNameLabel.TabIndex = 1;
			this.mtgNameLabel.Text = "Name:";
			this.mtgNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgNameField
			// 
			this.mtgNameField.Location = new System.Drawing.Point(100, 5);
			this.mtgNameField.Name = "mtgNameField";
			this.mtgNameField.Size = new System.Drawing.Size(370, 29);
			this.mtgNameField.TabIndex = 2;
			// 
			// mtgOracleTextLabel
			// 
			this.mtgOracleTextLabel.Location = new System.Drawing.Point(5, 180);
			this.mtgOracleTextLabel.Name = "mtgOracleTextLabel";
			this.mtgOracleTextLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgOracleTextLabel.TabIndex = 22;
			this.mtgOracleTextLabel.Text = "Oracle Text:";
			this.mtgOracleTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgIdentityLabel
			// 
			this.mtgIdentityLabel.Location = new System.Drawing.Point(5, 40);
			this.mtgIdentityLabel.Name = "mtgIdentityLabel";
			this.mtgIdentityLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgIdentityLabel.TabIndex = 3;
			this.mtgIdentityLabel.Text = "Identity:";
			this.mtgIdentityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgOracleTextField
			// 
			this.mtgOracleTextField.Location = new System.Drawing.Point(100, 180);
			this.mtgOracleTextField.Multiline = true;
			this.mtgOracleTextField.Name = "mtgOracleTextField";
			this.mtgOracleTextField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.mtgOracleTextField.Size = new System.Drawing.Size(450, 240);
			this.mtgOracleTextField.TabIndex = 23;
			// 
			// mtgColourLabel
			// 
			this.mtgColourLabel.Location = new System.Drawing.Point(5, 75);
			this.mtgColourLabel.Name = "mtgColourLabel";
			this.mtgColourLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgColourLabel.TabIndex = 9;
			this.mtgColourLabel.Text = "Colours:";
			this.mtgColourLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgCardTypeLabel
			// 
			this.mtgCardTypeLabel.Location = new System.Drawing.Point(5, 145);
			this.mtgCardTypeLabel.Name = "mtgCardTypeLabel";
			this.mtgCardTypeLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgCardTypeLabel.TabIndex = 17;
			this.mtgCardTypeLabel.Text = "Card Types:";
			this.mtgCardTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgPrintPage
			// 
			this.mtgPrintPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.mtgPrintPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.mtgPrintPage.Controls.Add(this.mtgPrintTokenCheck);
			this.mtgPrintPage.Controls.Add(this.mtgPrintAutoLimit);
			this.mtgPrintPage.Controls.Add(this.mtgPrintAutofillRangeButton);
			this.mtgPrintPage.Controls.Add(this.mtgPrintAutofillButton);
			this.mtgPrintPage.Controls.Add(this.mtgCardrefDescriptor);
			this.mtgPrintPage.Controls.Add(this.mtgRarityField);
			this.mtgPrintPage.Controls.Add(this.mtgRarityLabel);
			this.mtgPrintPage.Controls.Add(this.mtgScryfallLabel);
			this.mtgPrintPage.Controls.Add(this.mtgScryfallField);
			this.mtgPrintPage.Controls.Add(this.mtgImgpathBackLabel);
			this.mtgPrintPage.Controls.Add(this.mtgPrintImgboxBack);
			this.mtgPrintPage.Controls.Add(this.mtgImgsearchBackButton);
			this.mtgPrintPage.Controls.Add(this.mtgImgsearchBackLabel);
			this.mtgPrintPage.Controls.Add(this.mtgIODialog);
			this.mtgPrintPage.Controls.Add(this.mtgSaveButton);
			this.mtgPrintPage.Controls.Add(this.mtgImgpathLabel);
			this.mtgPrintPage.Controls.Add(this.mtgSubTreatmentButton);
			this.mtgPrintPage.Controls.Add(this.mtgAddTreatmentButton);
			this.mtgPrintPage.Controls.Add(this.mtgPrintImgbox);
			this.mtgPrintPage.Controls.Add(this.mtgAddPrintButton);
			this.mtgPrintPage.Controls.Add(this.mtgCardrefField);
			this.mtgPrintPage.Controls.Add(this.mtgCardrefLabel);
			this.mtgPrintPage.Controls.Add(this.mtgImgsearchButton);
			this.mtgPrintPage.Controls.Add(this.mtgImgsearchLabel);
			this.mtgPrintPage.Controls.Add(this.mtgFlavorTextField);
			this.mtgPrintPage.Controls.Add(this.mtgFlavorTextLabel);
			this.mtgPrintPage.Controls.Add(this.mtgTreatmentsValue);
			this.mtgPrintPage.Controls.Add(this.mtgTreatmentField);
			this.mtgPrintPage.Controls.Add(this.mtgTreatmentLabel);
			this.mtgPrintPage.Controls.Add(this.mtgNumberField);
			this.mtgPrintPage.Controls.Add(this.mtgNumberLabel);
			this.mtgPrintPage.Controls.Add(this.mtgSetField);
			this.mtgPrintPage.Controls.Add(this.mtgSetLabel);
			this.mtgPrintPage.Location = new System.Drawing.Point(4, 25);
			this.mtgPrintPage.Name = "mtgPrintPage";
			this.mtgPrintPage.Padding = new System.Windows.Forms.Padding(3);
			this.mtgPrintPage.Size = new System.Drawing.Size(1262, 666);
			this.mtgPrintPage.TabIndex = 1;
			this.mtgPrintPage.Text = "Printing Entry";
			// 
			// mtgPrintTokenCheck
			// 
			this.mtgPrintTokenCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.mtgPrintTokenCheck.Location = new System.Drawing.Point(440, 335);
			this.mtgPrintTokenCheck.Name = "mtgPrintTokenCheck";
			this.mtgPrintTokenCheck.Size = new System.Drawing.Size(29, 29);
			this.mtgPrintTokenCheck.TabIndex = 32;
			this.mtgPrintTokenCheck.UseVisualStyleBackColor = true;
			// 
			// mtgPrintAutoLimit
			// 
			this.mtgPrintAutoLimit.Location = new System.Drawing.Point(220, 510);
			this.mtgPrintAutoLimit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.mtgPrintAutoLimit.Name = "mtgPrintAutoLimit";
			this.mtgPrintAutoLimit.Size = new System.Drawing.Size(250, 29);
			this.mtgPrintAutoLimit.TabIndex = 31;
			// 
			// mtgPrintAutofillRangeButton
			// 
			this.mtgPrintAutofillRangeButton.Location = new System.Drawing.Point(100, 510);
			this.mtgPrintAutofillRangeButton.Name = "mtgPrintAutofillRangeButton";
			this.mtgPrintAutofillRangeButton.Size = new System.Drawing.Size(115, 29);
			this.mtgPrintAutofillRangeButton.TabIndex = 30;
			this.mtgPrintAutofillRangeButton.Text = "Autofill to";
			this.mtgPrintAutofillRangeButton.UseVisualStyleBackColor = true;
			this.mtgPrintAutofillRangeButton.Click += new System.EventHandler(this.MTG_OnClickAddAndFill);
			// 
			// mtgPrintAutofillButton
			// 
			this.mtgPrintAutofillButton.Location = new System.Drawing.Point(320, 335);
			this.mtgPrintAutofillButton.Name = "mtgPrintAutofillButton";
			this.mtgPrintAutofillButton.Size = new System.Drawing.Size(115, 29);
			this.mtgPrintAutofillButton.TabIndex = 29;
			this.mtgPrintAutofillButton.Text = "Autofill";
			this.mtgPrintAutofillButton.UseVisualStyleBackColor = true;
			this.mtgPrintAutofillButton.Click += new System.EventHandler(this.MTG_OnClickPrintAutofill);
			// 
			// mtgCardrefDescriptor
			// 
			this.mtgCardrefDescriptor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.mtgCardrefDescriptor.AutoSize = true;
			this.mtgCardrefDescriptor.Location = new System.Drawing.Point(5, 603);
			this.mtgCardrefDescriptor.MaximumSize = new System.Drawing.Size(0, 21);
			this.mtgCardrefDescriptor.Name = "mtgCardrefDescriptor";
			this.mtgCardrefDescriptor.Size = new System.Drawing.Size(16, 21);
			this.mtgCardrefDescriptor.TabIndex = 28;
			this.mtgCardrefDescriptor.Text = "-";
			this.mtgCardrefDescriptor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgRarityField
			// 
			this.mtgRarityField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.mtgRarityField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.mtgRarityField.FormattingEnabled = true;
			this.mtgRarityField.Location = new System.Drawing.Point(100, 75);
			this.mtgRarityField.Name = "mtgRarityField";
			this.mtgRarityField.Size = new System.Drawing.Size(370, 29);
			this.mtgRarityField.TabIndex = 6;
			// 
			// mtgRarityLabel
			// 
			this.mtgRarityLabel.Location = new System.Drawing.Point(5, 75);
			this.mtgRarityLabel.Name = "mtgRarityLabel";
			this.mtgRarityLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgRarityLabel.TabIndex = 5;
			this.mtgRarityLabel.Text = "Rarity:";
			this.mtgRarityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgScryfallLabel
			// 
			this.mtgScryfallLabel.Location = new System.Drawing.Point(5, 440);
			this.mtgScryfallLabel.Name = "mtgScryfallLabel";
			this.mtgScryfallLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgScryfallLabel.TabIndex = 20;
			this.mtgScryfallLabel.Text = "Scryfall ID:";
			this.mtgScryfallLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgScryfallField
			// 
			this.mtgScryfallField.Location = new System.Drawing.Point(100, 440);
			this.mtgScryfallField.Name = "mtgScryfallField";
			this.mtgScryfallField.Size = new System.Drawing.Size(370, 29);
			this.mtgScryfallField.TabIndex = 21;
			// 
			// mtgImgpathBackLabel
			// 
			this.mtgImgpathBackLabel.AutoSize = true;
			this.mtgImgpathBackLabel.Location = new System.Drawing.Point(855, 505);
			this.mtgImgpathBackLabel.Name = "mtgImgpathBackLabel";
			this.mtgImgpathBackLabel.Size = new System.Drawing.Size(0, 21);
			this.mtgImgpathBackLabel.TabIndex = 27;
			// 
			// mtgPrintImgboxBack
			// 
			this.mtgPrintImgboxBack.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.mtgPrintImgboxBack.InitialImage = null;
			this.mtgPrintImgboxBack.Location = new System.Drawing.Point(855, 5);
			this.mtgPrintImgboxBack.Name = "mtgPrintImgboxBack";
			this.mtgPrintImgboxBack.Size = new System.Drawing.Size(375, 495);
			this.mtgPrintImgboxBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgPrintImgboxBack.TabIndex = 27;
			this.mtgPrintImgboxBack.TabStop = false;
			// 
			// mtgImgsearchBackButton
			// 
			this.mtgImgsearchBackButton.Location = new System.Drawing.Point(100, 370);
			this.mtgImgsearchBackButton.Name = "mtgImgsearchBackButton";
			this.mtgImgsearchBackButton.Size = new System.Drawing.Size(370, 29);
			this.mtgImgsearchBackButton.TabIndex = 17;
			this.mtgImgsearchBackButton.Text = "Search";
			this.mtgImgsearchBackButton.UseVisualStyleBackColor = true;
			this.mtgImgsearchBackButton.Click += new System.EventHandler(this.MTG_OnClickSearchImgBack);
			// 
			// mtgImgsearchBackLabel
			// 
			this.mtgImgsearchBackLabel.Location = new System.Drawing.Point(5, 370);
			this.mtgImgsearchBackLabel.Name = "mtgImgsearchBackLabel";
			this.mtgImgsearchBackLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgImgsearchBackLabel.TabIndex = 16;
			this.mtgImgsearchBackLabel.Text = "Back:";
			this.mtgImgsearchBackLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgIODialog
			// 
			this.mtgIODialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.mtgIODialog.AutoSize = true;
			this.mtgIODialog.Location = new System.Drawing.Point(260, 630);
			this.mtgIODialog.Name = "mtgIODialog";
			this.mtgIODialog.Size = new System.Drawing.Size(16, 21);
			this.mtgIODialog.TabIndex = 25;
			this.mtgIODialog.Text = "-";
			this.mtgIODialog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgSaveButton
			// 
			this.mtgSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.mtgSaveButton.Location = new System.Drawing.Point(5, 627);
			this.mtgSaveButton.Name = "mtgSaveButton";
			this.mtgSaveButton.Size = new System.Drawing.Size(250, 29);
			this.mtgSaveButton.TabIndex = 24;
			this.mtgSaveButton.Text = "Save Catalog";
			this.mtgSaveButton.UseVisualStyleBackColor = true;
			this.mtgSaveButton.Click += new System.EventHandler(this.MTG_OnClickSave);
			// 
			// mtgImgpathLabel
			// 
			this.mtgImgpathLabel.AutoSize = true;
			this.mtgImgpathLabel.Location = new System.Drawing.Point(475, 505);
			this.mtgImgpathLabel.Name = "mtgImgpathLabel";
			this.mtgImgpathLabel.Size = new System.Drawing.Size(16, 21);
			this.mtgImgpathLabel.TabIndex = 26;
			this.mtgImgpathLabel.Text = "-";
			// 
			// mtgSubTreatmentButton
			// 
			this.mtgSubTreatmentButton.Location = new System.Drawing.Point(440, 110);
			this.mtgSubTreatmentButton.Name = "mtgSubTreatmentButton";
			this.mtgSubTreatmentButton.Size = new System.Drawing.Size(30, 29);
			this.mtgSubTreatmentButton.TabIndex = 10;
			this.mtgSubTreatmentButton.Text = "-";
			this.mtgSubTreatmentButton.UseVisualStyleBackColor = true;
			this.mtgSubTreatmentButton.Click += new System.EventHandler(this.MTG_OnClickSubTreatment);
			// 
			// mtgAddTreatmentButton
			// 
			this.mtgAddTreatmentButton.Location = new System.Drawing.Point(405, 110);
			this.mtgAddTreatmentButton.Name = "mtgAddTreatmentButton";
			this.mtgAddTreatmentButton.Size = new System.Drawing.Size(30, 29);
			this.mtgAddTreatmentButton.TabIndex = 9;
			this.mtgAddTreatmentButton.Text = "+";
			this.mtgAddTreatmentButton.UseVisualStyleBackColor = true;
			this.mtgAddTreatmentButton.Click += new System.EventHandler(this.MTG_OnClickAddTreatment);
			// 
			// mtgPrintImgbox
			// 
			this.mtgPrintImgbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.mtgPrintImgbox.InitialImage = null;
			this.mtgPrintImgbox.Location = new System.Drawing.Point(475, 5);
			this.mtgPrintImgbox.Name = "mtgPrintImgbox";
			this.mtgPrintImgbox.Size = new System.Drawing.Size(375, 495);
			this.mtgPrintImgbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mtgPrintImgbox.TabIndex = 20;
			this.mtgPrintImgbox.TabStop = false;
			// 
			// mtgAddPrintButton
			// 
			this.mtgAddPrintButton.Location = new System.Drawing.Point(100, 475);
			this.mtgAddPrintButton.Name = "mtgAddPrintButton";
			this.mtgAddPrintButton.Size = new System.Drawing.Size(370, 29);
			this.mtgAddPrintButton.TabIndex = 22;
			this.mtgAddPrintButton.Text = "Add To Catalog";
			this.mtgAddPrintButton.UseVisualStyleBackColor = true;
			this.mtgAddPrintButton.Click += new System.EventHandler(this.MTG_OnClickAddPrint);
			// 
			// mtgCardrefField
			// 
			this.mtgCardrefField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.mtgCardrefField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.mtgCardrefField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mtgCardrefField.FormattingEnabled = true;
			this.mtgCardrefField.Location = new System.Drawing.Point(100, 405);
			this.mtgCardrefField.Name = "mtgCardrefField";
			this.mtgCardrefField.Size = new System.Drawing.Size(370, 29);
			this.mtgCardrefField.Sorted = true;
			this.mtgCardrefField.TabIndex = 19;
			this.mtgCardrefField.SelectedIndexChanged += new System.EventHandler(this.MTG_OnSelectCardref);
			// 
			// mtgCardrefLabel
			// 
			this.mtgCardrefLabel.Location = new System.Drawing.Point(5, 405);
			this.mtgCardrefLabel.Name = "mtgCardrefLabel";
			this.mtgCardrefLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgCardrefLabel.TabIndex = 18;
			this.mtgCardrefLabel.Text = "Card:";
			this.mtgCardrefLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgImgsearchButton
			// 
			this.mtgImgsearchButton.Location = new System.Drawing.Point(100, 335);
			this.mtgImgsearchButton.Name = "mtgImgsearchButton";
			this.mtgImgsearchButton.Size = new System.Drawing.Size(215, 29);
			this.mtgImgsearchButton.TabIndex = 15;
			this.mtgImgsearchButton.Text = "Search";
			this.mtgImgsearchButton.UseVisualStyleBackColor = true;
			this.mtgImgsearchButton.Click += new System.EventHandler(this.MTG_OnClickSearchImg);
			// 
			// mtgImgsearchLabel
			// 
			this.mtgImgsearchLabel.Location = new System.Drawing.Point(5, 335);
			this.mtgImgsearchLabel.Name = "mtgImgsearchLabel";
			this.mtgImgsearchLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgImgsearchLabel.TabIndex = 14;
			this.mtgImgsearchLabel.Text = "Image:";
			this.mtgImgsearchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgFlavorTextField
			// 
			this.mtgFlavorTextField.Location = new System.Drawing.Point(100, 180);
			this.mtgFlavorTextField.Multiline = true;
			this.mtgFlavorTextField.Name = "mtgFlavorTextField";
			this.mtgFlavorTextField.Size = new System.Drawing.Size(370, 150);
			this.mtgFlavorTextField.TabIndex = 13;
			// 
			// mtgFlavorTextLabel
			// 
			this.mtgFlavorTextLabel.Location = new System.Drawing.Point(5, 180);
			this.mtgFlavorTextLabel.Name = "mtgFlavorTextLabel";
			this.mtgFlavorTextLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgFlavorTextLabel.TabIndex = 12;
			this.mtgFlavorTextLabel.Text = "Flavor Text:";
			this.mtgFlavorTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgTreatmentsValue
			// 
			this.mtgTreatmentsValue.Location = new System.Drawing.Point(100, 145);
			this.mtgTreatmentsValue.Name = "mtgTreatmentsValue";
			this.mtgTreatmentsValue.Size = new System.Drawing.Size(350, 30);
			this.mtgTreatmentsValue.TabIndex = 11;
			this.mtgTreatmentsValue.Text = "-";
			this.mtgTreatmentsValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgTreatmentField
			// 
			this.mtgTreatmentField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.mtgTreatmentField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.mtgTreatmentField.FormattingEnabled = true;
			this.mtgTreatmentField.Location = new System.Drawing.Point(100, 110);
			this.mtgTreatmentField.Name = "mtgTreatmentField";
			this.mtgTreatmentField.Size = new System.Drawing.Size(300, 29);
			this.mtgTreatmentField.TabIndex = 8;
			// 
			// mtgTreatmentLabel
			// 
			this.mtgTreatmentLabel.Location = new System.Drawing.Point(5, 110);
			this.mtgTreatmentLabel.Name = "mtgTreatmentLabel";
			this.mtgTreatmentLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgTreatmentLabel.TabIndex = 7;
			this.mtgTreatmentLabel.Text = "Treatments:";
			this.mtgTreatmentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgNumberField
			// 
			this.mtgNumberField.Location = new System.Drawing.Point(100, 40);
			this.mtgNumberField.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.mtgNumberField.Name = "mtgNumberField";
			this.mtgNumberField.Size = new System.Drawing.Size(370, 29);
			this.mtgNumberField.TabIndex = 4;
			// 
			// mtgNumberLabel
			// 
			this.mtgNumberLabel.Location = new System.Drawing.Point(5, 40);
			this.mtgNumberLabel.Name = "mtgNumberLabel";
			this.mtgNumberLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgNumberLabel.TabIndex = 3;
			this.mtgNumberLabel.Text = "Number:";
			this.mtgNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgSetField
			// 
			this.mtgSetField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mtgSetField.FormattingEnabled = true;
			this.mtgSetField.Location = new System.Drawing.Point(100, 5);
			this.mtgSetField.Name = "mtgSetField";
			this.mtgSetField.Size = new System.Drawing.Size(370, 29);
			this.mtgSetField.Sorted = true;
			this.mtgSetField.TabIndex = 2;
			// 
			// mtgSetLabel
			// 
			this.mtgSetLabel.Location = new System.Drawing.Point(5, 5);
			this.mtgSetLabel.Name = "mtgSetLabel";
			this.mtgSetLabel.Size = new System.Drawing.Size(90, 30);
			this.mtgSetLabel.TabIndex = 1;
			this.mtgSetLabel.Text = "Set:";
			this.mtgSetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgSymbolsPage
			// 
			this.mtgSymbolsPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.mtgSymbolsPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.mtgSymbolsPage.Controls.Add(this.mtgSymbolLayout);
			this.mtgSymbolsPage.Location = new System.Drawing.Point(4, 25);
			this.mtgSymbolsPage.Name = "mtgSymbolsPage";
			this.mtgSymbolsPage.Padding = new System.Windows.Forms.Padding(3);
			this.mtgSymbolsPage.Size = new System.Drawing.Size(1262, 666);
			this.mtgSymbolsPage.TabIndex = 5;
			this.mtgSymbolsPage.Text = "Symbols";
			// 
			// mtgSymbolLayout
			// 
			this.mtgSymbolLayout.AutoScroll = true;
			this.mtgSymbolLayout.Controls.Add(this.mtgSymbolHeaderBox);
			this.mtgSymbolLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mtgSymbolLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.mtgSymbolLayout.Location = new System.Drawing.Point(3, 3);
			this.mtgSymbolLayout.Name = "mtgSymbolLayout";
			this.mtgSymbolLayout.Size = new System.Drawing.Size(1252, 656);
			this.mtgSymbolLayout.TabIndex = 0;
			// 
			// mtgSymbolHeaderBox
			// 
			this.mtgSymbolHeaderBox.Controls.Add(this.mtgSaveSymbolsButton);
			this.mtgSymbolHeaderBox.Controls.Add(this.mtgAddSymbolButton);
			this.mtgSymbolHeaderBox.Controls.Add(this.mtgSymbolLabel);
			this.mtgSymbolHeaderBox.Location = new System.Drawing.Point(3, 3);
			this.mtgSymbolHeaderBox.Name = "mtgSymbolHeaderBox";
			this.mtgSymbolHeaderBox.Size = new System.Drawing.Size(325, 50);
			this.mtgSymbolHeaderBox.TabIndex = 1;
			this.mtgSymbolHeaderBox.TabStop = false;
			// 
			// mtgSaveSymbolsButton
			// 
			this.mtgSaveSymbolsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.mtgSaveSymbolsButton.Location = new System.Drawing.Point(260, 15);
			this.mtgSaveSymbolsButton.Name = "mtgSaveSymbolsButton";
			this.mtgSaveSymbolsButton.Size = new System.Drawing.Size(60, 29);
			this.mtgSaveSymbolsButton.TabIndex = 2;
			this.mtgSaveSymbolsButton.Text = "Save";
			this.mtgSaveSymbolsButton.UseVisualStyleBackColor = true;
			this.mtgSaveSymbolsButton.Click += new System.EventHandler(this.MTG_OnClickSaveSymbols);
			// 
			// mtgAddSymbolButton
			// 
			this.mtgAddSymbolButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.mtgAddSymbolButton.Location = new System.Drawing.Point(225, 15);
			this.mtgAddSymbolButton.Name = "mtgAddSymbolButton";
			this.mtgAddSymbolButton.Size = new System.Drawing.Size(30, 29);
			this.mtgAddSymbolButton.TabIndex = 1;
			this.mtgAddSymbolButton.Text = "+";
			this.mtgAddSymbolButton.UseVisualStyleBackColor = true;
			this.mtgAddSymbolButton.Click += new System.EventHandler(this.MTG_OnClickAddSymbol);
			// 
			// mtgSymbolLabel
			// 
			this.mtgSymbolLabel.Location = new System.Drawing.Point(5, 15);
			this.mtgSymbolLabel.Name = "mtgSymbolLabel";
			this.mtgSymbolLabel.Size = new System.Drawing.Size(150, 30);
			this.mtgSymbolLabel.TabIndex = 0;
			this.mtgSymbolLabel.Text = "Symbols";
			this.mtgSymbolLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mtgSetsPage
			// 
			this.mtgSetsPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.mtgSetsPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.mtgSetsPage.Controls.Add(this.mtgReloadSetsButton);
			this.mtgSetsPage.Controls.Add(this.mtgSetGeneratorPrev);
			this.mtgSetsPage.Controls.Add(this.mtgAddSetButton);
			this.mtgSetsPage.Controls.Add(this.mtgSaveSetsButton);
			this.mtgSetsPage.Controls.Add(this.mtgSetGeneratorNext);
			this.mtgSetsPage.Controls.Add(this.mtgSetGeneratorPageLabel);
			this.mtgSetsPage.Controls.Add(this.mtgSetGeneratorLayout);
			this.mtgSetsPage.Controls.Add(this.mtgSetGeneratorLabel);
			this.mtgSetsPage.Location = new System.Drawing.Point(4, 25);
			this.mtgSetsPage.Name = "mtgSetsPage";
			this.mtgSetsPage.Padding = new System.Windows.Forms.Padding(3);
			this.mtgSetsPage.Size = new System.Drawing.Size(1262, 666);
			this.mtgSetsPage.TabIndex = 6;
			this.mtgSetsPage.Text = "Sets";
			// 
			// mtgReloadSetsButton
			// 
			this.mtgReloadSetsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.mtgReloadSetsButton.Location = new System.Drawing.Point(880, 5);
			this.mtgReloadSetsButton.Name = "mtgReloadSetsButton";
			this.mtgReloadSetsButton.Size = new System.Drawing.Size(75, 29);
			this.mtgReloadSetsButton.TabIndex = 3;
			this.mtgReloadSetsButton.Text = "Reload";
			this.mtgReloadSetsButton.UseVisualStyleBackColor = true;
			this.mtgReloadSetsButton.Click += new System.EventHandler(this.MTG_RegenerateSets);
			// 
			// mtgSetGeneratorPrev
			// 
			this.mtgSetGeneratorPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.mtgSetGeneratorPrev.Location = new System.Drawing.Point(1060, 5);
			this.mtgSetGeneratorPrev.Name = "mtgSetGeneratorPrev";
			this.mtgSetGeneratorPrev.Size = new System.Drawing.Size(60, 29);
			this.mtgSetGeneratorPrev.TabIndex = 5;
			this.mtgSetGeneratorPrev.Text = "<";
			this.mtgSetGeneratorPrev.UseVisualStyleBackColor = true;
			this.mtgSetGeneratorPrev.Click += new System.EventHandler(this.MTG_OnClickSetGeneratorPrev);
			// 
			// mtgAddSetButton
			// 
			this.mtgAddSetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.mtgAddSetButton.Location = new System.Drawing.Point(960, 5);
			this.mtgAddSetButton.Name = "mtgAddSetButton";
			this.mtgAddSetButton.Size = new System.Drawing.Size(30, 29);
			this.mtgAddSetButton.TabIndex = 1;
			this.mtgAddSetButton.Text = "+";
			this.mtgAddSetButton.UseVisualStyleBackColor = true;
			this.mtgAddSetButton.Click += new System.EventHandler(this.MTG_OnClickAddSet);
			// 
			// mtgSaveSetsButton
			// 
			this.mtgSaveSetsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.mtgSaveSetsButton.Location = new System.Drawing.Point(995, 5);
			this.mtgSaveSetsButton.Name = "mtgSaveSetsButton";
			this.mtgSaveSetsButton.Size = new System.Drawing.Size(60, 29);
			this.mtgSaveSetsButton.TabIndex = 2;
			this.mtgSaveSetsButton.Text = "Save";
			this.mtgSaveSetsButton.UseVisualStyleBackColor = true;
			this.mtgSaveSetsButton.Click += new System.EventHandler(this.MTG_OnClickSaveSets);
			// 
			// mtgSetGeneratorNext
			// 
			this.mtgSetGeneratorNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.mtgSetGeneratorNext.Location = new System.Drawing.Point(1190, 5);
			this.mtgSetGeneratorNext.Name = "mtgSetGeneratorNext";
			this.mtgSetGeneratorNext.Size = new System.Drawing.Size(60, 29);
			this.mtgSetGeneratorNext.TabIndex = 4;
			this.mtgSetGeneratorNext.Text = ">";
			this.mtgSetGeneratorNext.UseVisualStyleBackColor = true;
			this.mtgSetGeneratorNext.Click += new System.EventHandler(this.MTG_OnClickSetGeneratorNext);
			// 
			// mtgSetGeneratorPageLabel
			// 
			this.mtgSetGeneratorPageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.mtgSetGeneratorPageLabel.Location = new System.Drawing.Point(1125, 5);
			this.mtgSetGeneratorPageLabel.Name = "mtgSetGeneratorPageLabel";
			this.mtgSetGeneratorPageLabel.Size = new System.Drawing.Size(60, 30);
			this.mtgSetGeneratorPageLabel.TabIndex = 2;
			this.mtgSetGeneratorPageLabel.Text = "X / X";
			this.mtgSetGeneratorPageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// mtgSetGeneratorLayout
			// 
			this.mtgSetGeneratorLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mtgSetGeneratorLayout.AutoScroll = true;
			this.mtgSetGeneratorLayout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.mtgSetGeneratorLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.mtgSetGeneratorLayout.Location = new System.Drawing.Point(3, 38);
			this.mtgSetGeneratorLayout.Name = "mtgSetGeneratorLayout";
			this.mtgSetGeneratorLayout.Size = new System.Drawing.Size(1252, 620);
			this.mtgSetGeneratorLayout.TabIndex = 1;
			// 
			// mtgSetGeneratorLabel
			// 
			this.mtgSetGeneratorLabel.AutoSize = true;
			this.mtgSetGeneratorLabel.Location = new System.Drawing.Point(5, 8);
			this.mtgSetGeneratorLabel.Name = "mtgSetGeneratorLabel";
			this.mtgSetGeneratorLabel.Size = new System.Drawing.Size(69, 21);
			this.mtgSetGeneratorLabel.TabIndex = 0;
			this.mtgSetGeneratorLabel.Text = "Edit Sets";
			this.mtgSetGeneratorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ygoPage
			// 
			this.ygoPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ygoPage.Controls.Add(this.ygoTabControl);
			this.ygoPage.Location = new System.Drawing.Point(4, 25);
			this.ygoPage.Name = "ygoPage";
			this.ygoPage.Padding = new System.Windows.Forms.Padding(3);
			this.ygoPage.Size = new System.Drawing.Size(1276, 709);
			this.ygoPage.TabIndex = 0;
			this.ygoPage.Text = "YGO";
			// 
			// pkmnPage
			// 
			this.pkmnPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pkmnPage.Controls.Add(this.pkmnTabControl);
			this.pkmnPage.Location = new System.Drawing.Point(4, 33);
			this.pkmnPage.Name = "pkmnPage";
			this.pkmnPage.Padding = new System.Windows.Forms.Padding(3);
			this.pkmnPage.Size = new System.Drawing.Size(1276, 701);
			this.pkmnPage.TabIndex = 2;
			this.pkmnPage.Text = "PKMN";
			// 
			// pkmnTabControl
			// 
			this.pkmnTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.pkmnTabControl.Controls.Add(this.pkmnSetlistPage);
			this.pkmnTabControl.Controls.Add(this.pkmnSearchPage);
			this.pkmnTabControl.Controls.Add(this.pkmnCatalogPage);
			this.pkmnTabControl.Controls.Add(this.pkmnDetailPage);
			this.pkmnTabControl.Controls.Add(this.pkmnCardPage);
			this.pkmnTabControl.Controls.Add(this.pkmnPrintPage);
			this.pkmnTabControl.Controls.Add(this.pkmnSymbolPage);
			this.pkmnTabControl.Controls.Add(this.pkmnSetPage);
			this.pkmnTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pkmnTabControl.Location = new System.Drawing.Point(3, 3);
			this.pkmnTabControl.Name = "pkmnTabControl";
			this.pkmnTabControl.SelectedIndex = 0;
			this.pkmnTabControl.Size = new System.Drawing.Size(1270, 695);
			this.pkmnTabControl.TabIndex = 1;
			// 
			// pkmnSetlistPage
			// 
			this.pkmnSetlistPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pkmnSetlistPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pkmnSetlistPage.Controls.Add(this.pkmnPrevSetButton);
			this.pkmnSetlistPage.Controls.Add(this.pkmnSetPageLabel);
			this.pkmnSetlistPage.Controls.Add(this.pkmnNextSetButton);
			this.pkmnSetlistPage.Controls.Add(this.pkmnSetlistLabel);
			this.pkmnSetlistPage.Controls.Add(this.pkmnSetlistLayout);
			this.pkmnSetlistPage.Location = new System.Drawing.Point(4, 33);
			this.pkmnSetlistPage.Name = "pkmnSetlistPage";
			this.pkmnSetlistPage.Padding = new System.Windows.Forms.Padding(3);
			this.pkmnSetlistPage.Size = new System.Drawing.Size(1262, 658);
			this.pkmnSetlistPage.TabIndex = 3;
			this.pkmnSetlistPage.Text = "Set List";
			// 
			// pkmnPrevSetButton
			// 
			this.pkmnPrevSetButton.Location = new System.Drawing.Point(645, 5);
			this.pkmnPrevSetButton.Name = "pkmnPrevSetButton";
			this.pkmnPrevSetButton.Size = new System.Drawing.Size(60, 29);
			this.pkmnPrevSetButton.TabIndex = 7;
			this.pkmnPrevSetButton.Text = "<";
			this.pkmnPrevSetButton.UseVisualStyleBackColor = true;
			this.pkmnPrevSetButton.Click += new System.EventHandler(this.PKMN_OnClickPrevSet);
			// 
			// pkmnSetPageLabel
			// 
			this.pkmnSetPageLabel.Location = new System.Drawing.Point(710, 5);
			this.pkmnSetPageLabel.Name = "pkmnSetPageLabel";
			this.pkmnSetPageLabel.Size = new System.Drawing.Size(60, 30);
			this.pkmnSetPageLabel.TabIndex = 6;
			this.pkmnSetPageLabel.Text = "X / X";
			this.pkmnSetPageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pkmnNextSetButton
			// 
			this.pkmnNextSetButton.Location = new System.Drawing.Point(775, 5);
			this.pkmnNextSetButton.Name = "pkmnNextSetButton";
			this.pkmnNextSetButton.Size = new System.Drawing.Size(60, 29);
			this.pkmnNextSetButton.TabIndex = 5;
			this.pkmnNextSetButton.Text = ">";
			this.pkmnNextSetButton.UseVisualStyleBackColor = true;
			this.pkmnNextSetButton.Click += new System.EventHandler(this.PKMN_OnClickNextSet);
			// 
			// pkmnSetlistLabel
			// 
			this.pkmnSetlistLabel.AutoSize = true;
			this.pkmnSetlistLabel.Location = new System.Drawing.Point(5, 8);
			this.pkmnSetlistLabel.Name = "pkmnSetlistLabel";
			this.pkmnSetlistLabel.Size = new System.Drawing.Size(60, 21);
			this.pkmnSetlistLabel.TabIndex = 1;
			this.pkmnSetlistLabel.Text = "Set List";
			this.pkmnSetlistLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnSetlistLayout
			// 
			this.pkmnSetlistLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.pkmnSetlistLayout.AutoScroll = true;
			this.pkmnSetlistLayout.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pkmnSetlistLayout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pkmnSetlistLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.pkmnSetlistLayout.Location = new System.Drawing.Point(3, 38);
			this.pkmnSetlistLayout.Name = "pkmnSetlistLayout";
			this.pkmnSetlistLayout.Size = new System.Drawing.Size(850, 612);
			this.pkmnSetlistLayout.TabIndex = 0;
			this.pkmnSetlistLayout.WrapContents = false;
			// 
			// pkmnSearchPage
			// 
			this.pkmnSearchPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pkmnSearchPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pkmnSearchPage.Controls.Add(this.pkmnClipboardLVXButton);
			this.pkmnSearchPage.Controls.Add(this.pkmnClipboardSPButton);
			this.pkmnSearchPage.Controls.Add(this.pkmnSearchDialog);
			this.pkmnSearchPage.Controls.Add(this.pkmnApplySearchTermsButton);
			this.pkmnSearchPage.Controls.Add(this.pkmnReloadSearchListsButton);
			this.pkmnSearchPage.Controls.Add(this.pkmnSearchTypeList);
			this.pkmnSearchPage.Controls.Add(this.pkmnSearchField);
			this.pkmnSearchPage.Controls.Add(this.pkmnSearchLabel);
			this.pkmnSearchPage.Controls.Add(this.pkmnClipboardPrismButton);
			this.pkmnSearchPage.Controls.Add(this.pkmnClipboardDeltaButton);
			this.pkmnSearchPage.Controls.Add(this.pkmnClipboardStarButton);
			this.pkmnSearchPage.Controls.Add(this.pkmnClipboardexButton);
			this.pkmnSearchPage.Controls.Add(this.pkmnSearchSetField);
			this.pkmnSearchPage.Controls.Add(this.pkmnSearchSetLabel);
			this.pkmnSearchPage.Controls.Add(this.pkmnSearchLocationField);
			this.pkmnSearchPage.Controls.Add(this.pkmnSearchLocationLabel);
			this.pkmnSearchPage.Controls.Add(this.pkmnSearchButton);
			this.pkmnSearchPage.Controls.Add(this.pkmnSearchOracleField);
			this.pkmnSearchPage.Controls.Add(this.pkmnSearchTypeField);
			this.pkmnSearchPage.Controls.Add(this.pkmnSearchNameLabel);
			this.pkmnSearchPage.Controls.Add(this.pkmnSearchNameField);
			this.pkmnSearchPage.Controls.Add(this.pkmnSearchOracleLabel);
			this.pkmnSearchPage.Controls.Add(this.pkmnSearchTypeLabel);
			this.pkmnSearchPage.Location = new System.Drawing.Point(4, 25);
			this.pkmnSearchPage.Name = "pkmnSearchPage";
			this.pkmnSearchPage.Padding = new System.Windows.Forms.Padding(3);
			this.pkmnSearchPage.Size = new System.Drawing.Size(1262, 666);
			this.pkmnSearchPage.TabIndex = 7;
			this.pkmnSearchPage.Text = "Search";
			// 
			// pkmnClipboardLVXButton
			// 
			this.pkmnClipboardLVXButton.Location = new System.Drawing.Point(385, 40);
			this.pkmnClipboardLVXButton.Name = "pkmnClipboardLVXButton";
			this.pkmnClipboardLVXButton.Size = new System.Drawing.Size(45, 30);
			this.pkmnClipboardLVXButton.TabIndex = 89;
			this.pkmnClipboardLVXButton.Text = "ʟᴠ.𝘟";
			this.pkmnClipboardLVXButton.UseVisualStyleBackColor = true;
			this.pkmnClipboardLVXButton.Click += new System.EventHandler(this.PKMN_OnClickClipboardLVXButton);
			// 
			// pkmnClipboardSPButton
			// 
			this.pkmnClipboardSPButton.Location = new System.Drawing.Point(345, 40);
			this.pkmnClipboardSPButton.Name = "pkmnClipboardSPButton";
			this.pkmnClipboardSPButton.Size = new System.Drawing.Size(35, 30);
			this.pkmnClipboardSPButton.TabIndex = 88;
			this.pkmnClipboardSPButton.Text = "𝘚𝘗";
			this.pkmnClipboardSPButton.UseVisualStyleBackColor = true;
			this.pkmnClipboardSPButton.Click += new System.EventHandler(this.PKMN_OnClickClipboardSPButton);
			// 
			// pkmnSearchDialog
			// 
			this.pkmnSearchDialog.Location = new System.Drawing.Point(475, 5);
			this.pkmnSearchDialog.Name = "pkmnSearchDialog";
			this.pkmnSearchDialog.Size = new System.Drawing.Size(90, 30);
			this.pkmnSearchDialog.TabIndex = 87;
			this.pkmnSearchDialog.Text = "-";
			this.pkmnSearchDialog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnApplySearchTermsButton
			// 
			this.pkmnApplySearchTermsButton.Location = new System.Drawing.Point(100, 285);
			this.pkmnApplySearchTermsButton.Name = "pkmnApplySearchTermsButton";
			this.pkmnApplySearchTermsButton.Size = new System.Drawing.Size(120, 30);
			this.pkmnApplySearchTermsButton.TabIndex = 86;
			this.pkmnApplySearchTermsButton.Text = "Apply";
			this.pkmnApplySearchTermsButton.UseVisualStyleBackColor = true;
			this.pkmnApplySearchTermsButton.Click += new System.EventHandler(this.PKMN_OnClickApplySearchTerms);
			// 
			// pkmnReloadSearchListsButton
			// 
			this.pkmnReloadSearchListsButton.Location = new System.Drawing.Point(350, 285);
			this.pkmnReloadSearchListsButton.Name = "pkmnReloadSearchListsButton";
			this.pkmnReloadSearchListsButton.Size = new System.Drawing.Size(120, 30);
			this.pkmnReloadSearchListsButton.TabIndex = 85;
			this.pkmnReloadSearchListsButton.Text = "Reload Lists";
			this.pkmnReloadSearchListsButton.UseVisualStyleBackColor = true;
			this.pkmnReloadSearchListsButton.Click += new System.EventHandler(this.PKMN_OnClickReloadSearchLists);
			// 
			// pkmnSearchTypeList
			// 
			this.pkmnSearchTypeList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.pkmnSearchTypeList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.pkmnSearchTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pkmnSearchTypeList.FormattingEnabled = true;
			this.pkmnSearchTypeList.Location = new System.Drawing.Point(100, 110);
			this.pkmnSearchTypeList.Name = "pkmnSearchTypeList";
			this.pkmnSearchTypeList.Size = new System.Drawing.Size(370, 29);
			this.pkmnSearchTypeList.Sorted = true;
			this.pkmnSearchTypeList.TabIndex = 84;
			this.pkmnSearchTypeList.SelectedValueChanged += new System.EventHandler(this.PKMN_OnSearchTypeChanged);
			// 
			// pkmnSearchField
			// 
			this.pkmnSearchField.Location = new System.Drawing.Point(100, 5);
			this.pkmnSearchField.Name = "pkmnSearchField";
			this.pkmnSearchField.Size = new System.Drawing.Size(370, 29);
			this.pkmnSearchField.TabIndex = 83;
			// 
			// pkmnSearchLabel
			// 
			this.pkmnSearchLabel.Location = new System.Drawing.Point(5, 5);
			this.pkmnSearchLabel.Name = "pkmnSearchLabel";
			this.pkmnSearchLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnSearchLabel.TabIndex = 82;
			this.pkmnSearchLabel.Text = "Search:";
			this.pkmnSearchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnClipboardPrismButton
			// 
			this.pkmnClipboardPrismButton.Location = new System.Drawing.Point(435, 40);
			this.pkmnClipboardPrismButton.Name = "pkmnClipboardPrismButton";
			this.pkmnClipboardPrismButton.Size = new System.Drawing.Size(35, 30);
			this.pkmnClipboardPrismButton.TabIndex = 81;
			this.pkmnClipboardPrismButton.Text = "◇";
			this.pkmnClipboardPrismButton.UseVisualStyleBackColor = true;
			this.pkmnClipboardPrismButton.Click += new System.EventHandler(this.PKMN_OnClickClipboardPrismButton);
			// 
			// pkmnClipboardDeltaButton
			// 
			this.pkmnClipboardDeltaButton.Location = new System.Drawing.Point(305, 40);
			this.pkmnClipboardDeltaButton.Name = "pkmnClipboardDeltaButton";
			this.pkmnClipboardDeltaButton.Size = new System.Drawing.Size(35, 30);
			this.pkmnClipboardDeltaButton.TabIndex = 80;
			this.pkmnClipboardDeltaButton.Text = "δ";
			this.pkmnClipboardDeltaButton.UseVisualStyleBackColor = true;
			this.pkmnClipboardDeltaButton.Click += new System.EventHandler(this.PKMN_OnClickClipboardDeltaButton);
			// 
			// pkmnClipboardStarButton
			// 
			this.pkmnClipboardStarButton.Location = new System.Drawing.Point(265, 40);
			this.pkmnClipboardStarButton.Name = "pkmnClipboardStarButton";
			this.pkmnClipboardStarButton.Size = new System.Drawing.Size(35, 30);
			this.pkmnClipboardStarButton.TabIndex = 79;
			this.pkmnClipboardStarButton.Text = "☆";
			this.pkmnClipboardStarButton.UseVisualStyleBackColor = true;
			this.pkmnClipboardStarButton.Click += new System.EventHandler(this.PKMN_OnClickClipboardStarButton);
			// 
			// pkmnClipboardexButton
			// 
			this.pkmnClipboardexButton.Location = new System.Drawing.Point(225, 40);
			this.pkmnClipboardexButton.Name = "pkmnClipboardexButton";
			this.pkmnClipboardexButton.Size = new System.Drawing.Size(35, 30);
			this.pkmnClipboardexButton.TabIndex = 78;
			this.pkmnClipboardexButton.Text = "𝑒𝑥";
			this.pkmnClipboardexButton.UseVisualStyleBackColor = true;
			this.pkmnClipboardexButton.Click += new System.EventHandler(this.PKMN_OnClickClipboardexButton);
			// 
			// pkmnSearchSetField
			// 
			this.pkmnSearchSetField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.pkmnSearchSetField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.pkmnSearchSetField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pkmnSearchSetField.FormattingEnabled = true;
			this.pkmnSearchSetField.Location = new System.Drawing.Point(100, 215);
			this.pkmnSearchSetField.Name = "pkmnSearchSetField";
			this.pkmnSearchSetField.Size = new System.Drawing.Size(370, 29);
			this.pkmnSearchSetField.Sorted = true;
			this.pkmnSearchSetField.TabIndex = 77;
			// 
			// pkmnSearchSetLabel
			// 
			this.pkmnSearchSetLabel.Location = new System.Drawing.Point(5, 215);
			this.pkmnSearchSetLabel.Name = "pkmnSearchSetLabel";
			this.pkmnSearchSetLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnSearchSetLabel.TabIndex = 76;
			this.pkmnSearchSetLabel.Text = "Set:";
			this.pkmnSearchSetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnSearchLocationField
			// 
			this.pkmnSearchLocationField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.pkmnSearchLocationField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.pkmnSearchLocationField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pkmnSearchLocationField.FormattingEnabled = true;
			this.pkmnSearchLocationField.Location = new System.Drawing.Point(100, 250);
			this.pkmnSearchLocationField.Name = "pkmnSearchLocationField";
			this.pkmnSearchLocationField.Size = new System.Drawing.Size(370, 29);
			this.pkmnSearchLocationField.TabIndex = 75;
			// 
			// pkmnSearchLocationLabel
			// 
			this.pkmnSearchLocationLabel.Location = new System.Drawing.Point(5, 250);
			this.pkmnSearchLocationLabel.Name = "pkmnSearchLocationLabel";
			this.pkmnSearchLocationLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnSearchLocationLabel.TabIndex = 74;
			this.pkmnSearchLocationLabel.Text = "Location:";
			this.pkmnSearchLocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnSearchButton
			// 
			this.pkmnSearchButton.Location = new System.Drawing.Point(100, 40);
			this.pkmnSearchButton.Name = "pkmnSearchButton";
			this.pkmnSearchButton.Size = new System.Drawing.Size(120, 30);
			this.pkmnSearchButton.TabIndex = 73;
			this.pkmnSearchButton.Text = "Search";
			this.pkmnSearchButton.UseVisualStyleBackColor = true;
			this.pkmnSearchButton.Click += new System.EventHandler(this.PKMN_OnClickSearch);
			// 
			// pkmnSearchOracleField
			// 
			this.pkmnSearchOracleField.Location = new System.Drawing.Point(100, 180);
			this.pkmnSearchOracleField.Name = "pkmnSearchOracleField";
			this.pkmnSearchOracleField.Size = new System.Drawing.Size(370, 29);
			this.pkmnSearchOracleField.TabIndex = 72;
			// 
			// pkmnSearchTypeField
			// 
			this.pkmnSearchTypeField.Location = new System.Drawing.Point(100, 145);
			this.pkmnSearchTypeField.Name = "pkmnSearchTypeField";
			this.pkmnSearchTypeField.Size = new System.Drawing.Size(370, 29);
			this.pkmnSearchTypeField.TabIndex = 59;
			// 
			// pkmnSearchNameLabel
			// 
			this.pkmnSearchNameLabel.Location = new System.Drawing.Point(5, 75);
			this.pkmnSearchNameLabel.Name = "pkmnSearchNameLabel";
			this.pkmnSearchNameLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnSearchNameLabel.TabIndex = 44;
			this.pkmnSearchNameLabel.Text = "Name:";
			this.pkmnSearchNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnSearchNameField
			// 
			this.pkmnSearchNameField.Location = new System.Drawing.Point(100, 75);
			this.pkmnSearchNameField.Name = "pkmnSearchNameField";
			this.pkmnSearchNameField.Size = new System.Drawing.Size(370, 29);
			this.pkmnSearchNameField.TabIndex = 45;
			// 
			// pkmnSearchOracleLabel
			// 
			this.pkmnSearchOracleLabel.Location = new System.Drawing.Point(5, 180);
			this.pkmnSearchOracleLabel.Name = "pkmnSearchOracleLabel";
			this.pkmnSearchOracleLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnSearchOracleLabel.TabIndex = 60;
			this.pkmnSearchOracleLabel.Text = "Oracle Text:";
			this.pkmnSearchOracleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnSearchTypeLabel
			// 
			this.pkmnSearchTypeLabel.Location = new System.Drawing.Point(5, 110);
			this.pkmnSearchTypeLabel.Name = "pkmnSearchTypeLabel";
			this.pkmnSearchTypeLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnSearchTypeLabel.TabIndex = 58;
			this.pkmnSearchTypeLabel.Text = "Card Types:";
			this.pkmnSearchTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnCatalogPage
			// 
			this.pkmnCatalogPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pkmnCatalogPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pkmnCatalogPage.Controls.Add(this.pkmnSortAlphabeticalButton);
			this.pkmnCatalogPage.Controls.Add(this.pkmnSortNumericButton);
			this.pkmnCatalogPage.Controls.Add(this.pkmnCatalogNextButton);
			this.pkmnCatalogPage.Controls.Add(this.pkmnCatalogPrevButton);
			this.pkmnCatalogPage.Controls.Add(this.pkmnCatalogLayout);
			this.pkmnCatalogPage.Controls.Add(this.pkmnCatalogIndex);
			this.pkmnCatalogPage.Location = new System.Drawing.Point(4, 25);
			this.pkmnCatalogPage.Name = "pkmnCatalogPage";
			this.pkmnCatalogPage.Padding = new System.Windows.Forms.Padding(3);
			this.pkmnCatalogPage.Size = new System.Drawing.Size(1262, 666);
			this.pkmnCatalogPage.TabIndex = 2;
			this.pkmnCatalogPage.Text = "Catalog";
			// 
			// pkmnSortAlphabeticalButton
			// 
			this.pkmnSortAlphabeticalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pkmnSortAlphabeticalButton.AutoSize = true;
			this.pkmnSortAlphabeticalButton.Location = new System.Drawing.Point(929, 8);
			this.pkmnSortAlphabeticalButton.Name = "pkmnSortAlphabeticalButton";
			this.pkmnSortAlphabeticalButton.Size = new System.Drawing.Size(113, 25);
			this.pkmnSortAlphabeticalButton.TabIndex = 5;
			this.pkmnSortAlphabeticalButton.Text = "Alphabetical";
			this.pkmnSortAlphabeticalButton.UseVisualStyleBackColor = true;
			this.pkmnSortAlphabeticalButton.CheckedChanged += new System.EventHandler(this.PKMN_OnClickSortAlphabetical);
			// 
			// pkmnSortNumericButton
			// 
			this.pkmnSortNumericButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pkmnSortNumericButton.AutoSize = true;
			this.pkmnSortNumericButton.Checked = true;
			this.pkmnSortNumericButton.Location = new System.Drawing.Point(835, 7);
			this.pkmnSortNumericButton.Name = "pkmnSortNumericButton";
			this.pkmnSortNumericButton.Size = new System.Drawing.Size(88, 25);
			this.pkmnSortNumericButton.TabIndex = 4;
			this.pkmnSortNumericButton.TabStop = true;
			this.pkmnSortNumericButton.Text = "Numeric";
			this.pkmnSortNumericButton.UseVisualStyleBackColor = true;
			this.pkmnSortNumericButton.CheckedChanged += new System.EventHandler(this.PKMN_OnClickSortAlphabetical);
			// 
			// pkmnCatalogNextButton
			// 
			this.pkmnCatalogNextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pkmnCatalogNextButton.Location = new System.Drawing.Point(1178, 5);
			this.pkmnCatalogNextButton.Name = "pkmnCatalogNextButton";
			this.pkmnCatalogNextButton.Size = new System.Drawing.Size(75, 29);
			this.pkmnCatalogNextButton.TabIndex = 2;
			this.pkmnCatalogNextButton.Text = ">";
			this.pkmnCatalogNextButton.UseVisualStyleBackColor = true;
			this.pkmnCatalogNextButton.Click += new System.EventHandler(this.PKMN_OnClickCatalogNext);
			// 
			// pkmnCatalogPrevButton
			// 
			this.pkmnCatalogPrevButton.Location = new System.Drawing.Point(5, 5);
			this.pkmnCatalogPrevButton.Name = "pkmnCatalogPrevButton";
			this.pkmnCatalogPrevButton.Size = new System.Drawing.Size(75, 29);
			this.pkmnCatalogPrevButton.TabIndex = 1;
			this.pkmnCatalogPrevButton.Text = "<";
			this.pkmnCatalogPrevButton.UseVisualStyleBackColor = true;
			this.pkmnCatalogPrevButton.Click += new System.EventHandler(this.PKMN_OnClickCatalogPrev);
			// 
			// pkmnCatalogLayout
			// 
			this.pkmnCatalogLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pkmnCatalogLayout.AutoScroll = true;
			this.pkmnCatalogLayout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pkmnCatalogLayout.Location = new System.Drawing.Point(3, 38);
			this.pkmnCatalogLayout.Name = "pkmnCatalogLayout";
			this.pkmnCatalogLayout.Size = new System.Drawing.Size(1252, 620);
			this.pkmnCatalogLayout.TabIndex = 0;
			// 
			// pkmnCatalogIndex
			// 
			this.pkmnCatalogIndex.Dock = System.Windows.Forms.DockStyle.Top;
			this.pkmnCatalogIndex.Location = new System.Drawing.Point(3, 3);
			this.pkmnCatalogIndex.Name = "pkmnCatalogIndex";
			this.pkmnCatalogIndex.Size = new System.Drawing.Size(1252, 30);
			this.pkmnCatalogIndex.TabIndex = 3;
			this.pkmnCatalogIndex.Text = "Showing 0 - 0 of 0";
			this.pkmnCatalogIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pkmnDetailPage
			// 
			this.pkmnDetailPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pkmnDetailPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pkmnDetailPage.Controls.Add(this.pkmnDetailFilterCountLabel);
			this.pkmnDetailPage.Controls.Add(this.pkmnPrintingsPanel);
			this.pkmnDetailPage.Controls.Add(this.pkmnDetailDialog);
			this.pkmnDetailPage.Controls.Add(this.pkmnDetailAutogenButton);
			this.pkmnDetailPage.Controls.Add(this.pkmnDeletePrintingButton);
			this.pkmnDetailPage.Controls.Add(this.pkmnDetailNextButton);
			this.pkmnDetailPage.Controls.Add(this.pkmnDetailPrevButton);
			this.pkmnDetailPage.Controls.Add(this.pkmnCardtipPanel);
			this.pkmnDetailPage.Controls.Add(this.pkmnTooltipPanel);
			this.pkmnDetailPage.Controls.Add(this.pkmnDetailPanel);
			this.pkmnDetailPage.Controls.Add(this.pkmnDetailImgbox);
			this.pkmnDetailPage.Location = new System.Drawing.Point(4, 25);
			this.pkmnDetailPage.Name = "pkmnDetailPage";
			this.pkmnDetailPage.Size = new System.Drawing.Size(1262, 666);
			this.pkmnDetailPage.TabIndex = 4;
			this.pkmnDetailPage.Text = "Card Details";
			// 
			// pkmnDetailFilterCountLabel
			// 
			this.pkmnDetailFilterCountLabel.Location = new System.Drawing.Point(10, 585);
			this.pkmnDetailFilterCountLabel.MinimumSize = new System.Drawing.Size(0, 29);
			this.pkmnDetailFilterCountLabel.Name = "pkmnDetailFilterCountLabel";
			this.pkmnDetailFilterCountLabel.Size = new System.Drawing.Size(390, 29);
			this.pkmnDetailFilterCountLabel.TabIndex = 28;
			this.pkmnDetailFilterCountLabel.Text = "X / X";
			this.pkmnDetailFilterCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pkmnPrintingsPanel
			// 
			this.pkmnPrintingsPanel.Location = new System.Drawing.Point(865, 5);
			this.pkmnPrintingsPanel.Name = "pkmnPrintingsPanel";
			this.pkmnPrintingsPanel.Size = new System.Drawing.Size(380, 100);
			this.pkmnPrintingsPanel.TabIndex = 6;
			// 
			// pkmnDetailDialog
			// 
			this.pkmnDetailDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pkmnDetailDialog.AutoSize = true;
			this.pkmnDetailDialog.Location = new System.Drawing.Point(261, 630);
			this.pkmnDetailDialog.MinimumSize = new System.Drawing.Size(0, 29);
			this.pkmnDetailDialog.Name = "pkmnDetailDialog";
			this.pkmnDetailDialog.Size = new System.Drawing.Size(16, 29);
			this.pkmnDetailDialog.TabIndex = 27;
			this.pkmnDetailDialog.Text = "-";
			this.pkmnDetailDialog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnDetailAutogenButton
			// 
			this.pkmnDetailAutogenButton.Location = new System.Drawing.Point(130, 550);
			this.pkmnDetailAutogenButton.Name = "pkmnDetailAutogenButton";
			this.pkmnDetailAutogenButton.Size = new System.Drawing.Size(150, 29);
			this.pkmnDetailAutogenButton.TabIndex = 26;
			this.pkmnDetailAutogenButton.Text = "Autogen";
			this.pkmnDetailAutogenButton.UseVisualStyleBackColor = true;
			this.pkmnDetailAutogenButton.Click += new System.EventHandler(this.PKMN_AutogenDetailRef);
			// 
			// pkmnDeletePrintingButton
			// 
			this.pkmnDeletePrintingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pkmnDeletePrintingButton.Location = new System.Drawing.Point(5, 630);
			this.pkmnDeletePrintingButton.Name = "pkmnDeletePrintingButton";
			this.pkmnDeletePrintingButton.Size = new System.Drawing.Size(250, 29);
			this.pkmnDeletePrintingButton.TabIndex = 25;
			this.pkmnDeletePrintingButton.Text = "Delete Printing!";
			this.pkmnDeletePrintingButton.UseVisualStyleBackColor = true;
			this.pkmnDeletePrintingButton.Click += new System.EventHandler(this.PKMN_DeleteCurrentPrinting);
			// 
			// pkmnDetailNextButton
			// 
			this.pkmnDetailNextButton.Location = new System.Drawing.Point(285, 550);
			this.pkmnDetailNextButton.Name = "pkmnDetailNextButton";
			this.pkmnDetailNextButton.Size = new System.Drawing.Size(120, 29);
			this.pkmnDetailNextButton.TabIndex = 10;
			this.pkmnDetailNextButton.Text = "Next Card";
			this.pkmnDetailNextButton.UseVisualStyleBackColor = true;
			this.pkmnDetailNextButton.Click += new System.EventHandler(this.PKMN_LoadNextInSelection);
			// 
			// pkmnDetailPrevButton
			// 
			this.pkmnDetailPrevButton.Location = new System.Drawing.Point(5, 550);
			this.pkmnDetailPrevButton.Name = "pkmnDetailPrevButton";
			this.pkmnDetailPrevButton.Size = new System.Drawing.Size(120, 29);
			this.pkmnDetailPrevButton.TabIndex = 9;
			this.pkmnDetailPrevButton.Text = "Previous Card";
			this.pkmnDetailPrevButton.UseVisualStyleBackColor = true;
			this.pkmnDetailPrevButton.Click += new System.EventHandler(this.PKMN_LoadPreviousInSelection);
			// 
			// pkmnCardtipPanel
			// 
			this.pkmnCardtipPanel.Controls.Add(this.pkmnCardtipImage);
			this.pkmnCardtipPanel.Location = new System.Drawing.Point(865, 215);
			this.pkmnCardtipPanel.Name = "pkmnCardtipPanel";
			this.pkmnCardtipPanel.Size = new System.Drawing.Size(250, 350);
			this.pkmnCardtipPanel.TabIndex = 6;
			// 
			// pkmnCardtipImage
			// 
			this.pkmnCardtipImage.Location = new System.Drawing.Point(0, 0);
			this.pkmnCardtipImage.Name = "pkmnCardtipImage";
			this.pkmnCardtipImage.Size = new System.Drawing.Size(250, 350);
			this.pkmnCardtipImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pkmnCardtipImage.TabIndex = 0;
			this.pkmnCardtipImage.TabStop = false;
			// 
			// pkmnTooltipPanel
			// 
			this.pkmnTooltipPanel.Location = new System.Drawing.Point(865, 110);
			this.pkmnTooltipPanel.Name = "pkmnTooltipPanel";
			this.pkmnTooltipPanel.Size = new System.Drawing.Size(300, 100);
			this.pkmnTooltipPanel.TabIndex = 5;
			// 
			// pkmnDetailPanel
			// 
			this.pkmnDetailPanel.Controls.Add(this.pkmnOwnedPrintingsLabel);
			this.pkmnDetailPanel.Controls.Add(this.pkmnReloadLocationsButton);
			this.pkmnDetailPanel.Controls.Add(this.pkmnEditPrintButton);
			this.pkmnDetailPanel.Controls.Add(this.pkmnEditCardButton);
			this.pkmnDetailPanel.Controls.Add(this.pkmnMoveLabel);
			this.pkmnDetailPanel.Controls.Add(this.pkmnLocationTable);
			this.pkmnDetailPanel.Controls.Add(this.pkmnMoveField);
			this.pkmnDetailPanel.Location = new System.Drawing.Point(410, 5);
			this.pkmnDetailPanel.Name = "pkmnDetailPanel";
			this.pkmnDetailPanel.Size = new System.Drawing.Size(450, 540);
			this.pkmnDetailPanel.TabIndex = 4;
			this.pkmnDetailPanel.Text = "Owned Printings";
			// 
			// pkmnOwnedPrintingsLabel
			// 
			this.pkmnOwnedPrintingsLabel.Location = new System.Drawing.Point(5, 5);
			this.pkmnOwnedPrintingsLabel.Name = "pkmnOwnedPrintingsLabel";
			this.pkmnOwnedPrintingsLabel.Size = new System.Drawing.Size(440, 30);
			this.pkmnOwnedPrintingsLabel.TabIndex = 9;
			this.pkmnOwnedPrintingsLabel.Text = "Owned Printings";
			this.pkmnOwnedPrintingsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnReloadLocationsButton
			// 
			this.pkmnReloadLocationsButton.Location = new System.Drawing.Point(295, 35);
			this.pkmnReloadLocationsButton.Name = "pkmnReloadLocationsButton";
			this.pkmnReloadLocationsButton.Size = new System.Drawing.Size(150, 29);
			this.pkmnReloadLocationsButton.TabIndex = 8;
			this.pkmnReloadLocationsButton.Text = "Reload Locations";
			this.pkmnReloadLocationsButton.UseVisualStyleBackColor = true;
			this.pkmnReloadLocationsButton.Click += new System.EventHandler(this.PKMN_OnClickReloadLocations);
			// 
			// pkmnEditPrintButton
			// 
			this.pkmnEditPrintButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pkmnEditPrintButton.Location = new System.Drawing.Point(300, 505);
			this.pkmnEditPrintButton.Name = "pkmnEditPrintButton";
			this.pkmnEditPrintButton.Size = new System.Drawing.Size(145, 29);
			this.pkmnEditPrintButton.TabIndex = 5;
			this.pkmnEditPrintButton.Text = "Edit Printing Data";
			this.pkmnEditPrintButton.UseVisualStyleBackColor = true;
			this.pkmnEditPrintButton.Click += new System.EventHandler(this.PKMN_EditPrint);
			// 
			// pkmnEditCardButton
			// 
			this.pkmnEditCardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pkmnEditCardButton.Location = new System.Drawing.Point(150, 505);
			this.pkmnEditCardButton.Name = "pkmnEditCardButton";
			this.pkmnEditCardButton.Size = new System.Drawing.Size(145, 29);
			this.pkmnEditCardButton.TabIndex = 4;
			this.pkmnEditCardButton.Text = "Edit Card Data";
			this.pkmnEditCardButton.UseVisualStyleBackColor = true;
			this.pkmnEditCardButton.Click += new System.EventHandler(this.PKMN_EditCard);
			// 
			// pkmnMoveLabel
			// 
			this.pkmnMoveLabel.Location = new System.Drawing.Point(5, 35);
			this.pkmnMoveLabel.Name = "pkmnMoveLabel";
			this.pkmnMoveLabel.Size = new System.Drawing.Size(70, 30);
			this.pkmnMoveLabel.TabIndex = 3;
			this.pkmnMoveLabel.Text = "Move to:";
			this.pkmnMoveLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnLocationTable
			// 
			this.pkmnLocationTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
			this.pkmnLocationTable.ColumnCount = 4;
			this.pkmnLocationTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.pkmnLocationTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.pkmnLocationTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.pkmnLocationTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.pkmnLocationTable.Location = new System.Drawing.Point(9, 70);
			this.pkmnLocationTable.Name = "pkmnLocationTable";
			this.pkmnLocationTable.RowCount = 1;
			this.pkmnLocationTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.pkmnLocationTable.Size = new System.Drawing.Size(435, 30);
			this.pkmnLocationTable.TabIndex = 1;
			// 
			// pkmnMoveField
			// 
			this.pkmnMoveField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.pkmnMoveField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.pkmnMoveField.FormattingEnabled = true;
			this.pkmnMoveField.Location = new System.Drawing.Point(75, 35);
			this.pkmnMoveField.Name = "pkmnMoveField";
			this.pkmnMoveField.Size = new System.Drawing.Size(215, 29);
			this.pkmnMoveField.TabIndex = 2;
			// 
			// pkmnDetailImgbox
			// 
			this.pkmnDetailImgbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pkmnDetailImgbox.Location = new System.Drawing.Point(5, 5);
			this.pkmnDetailImgbox.Name = "pkmnDetailImgbox";
			this.pkmnDetailImgbox.Size = new System.Drawing.Size(400, 540);
			this.pkmnDetailImgbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pkmnDetailImgbox.TabIndex = 0;
			this.pkmnDetailImgbox.TabStop = false;
			// 
			// pkmnCardPage
			// 
			this.pkmnCardPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pkmnCardPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pkmnCardPage.Controls.Add(this.pkmnPokemonButton);
			this.pkmnCardPage.Controls.Add(this.pkmnTrainerButton);
			this.pkmnCardPage.Controls.Add(this.pkmnImportButton);
			this.pkmnCardPage.Controls.Add(this.pkmnImportField);
			this.pkmnCardPage.Controls.Add(this.pkmnRetreatField);
			this.pkmnCardPage.Controls.Add(this.pkmnResistField);
			this.pkmnCardPage.Controls.Add(this.pkmnWeakField);
			this.pkmnCardPage.Controls.Add(this.pkmnRetreatLabel);
			this.pkmnCardPage.Controls.Add(this.pkmnWeakLabel);
			this.pkmnCardPage.Controls.Add(this.pkmnStageLabel);
			this.pkmnCardPage.Controls.Add(this.pkmnStageField);
			this.pkmnCardPage.Controls.Add(this.pkmnIgnorDuplicateEntryLabel);
			this.pkmnCardPage.Controls.Add(this.pkmnIgnorDuplicateEntryBox);
			this.pkmnCardPage.Controls.Add(this.pkmnTypeField);
			this.pkmnCardPage.Controls.Add(this.pkmnResistLabel);
			this.pkmnCardPage.Controls.Add(this.pkmnETypeField);
			this.pkmnCardPage.Controls.Add(this.pkmnETypeLabel);
			this.pkmnCardPage.Controls.Add(this.pkmnCardDialog);
			this.pkmnCardPage.Controls.Add(this.pkmnHPField);
			this.pkmnCardPage.Controls.Add(this.pkmnHPLabel);
			this.pkmnCardPage.Controls.Add(this.pkmnAddCardButton);
			this.pkmnCardPage.Controls.Add(this.pkmnNameLabel);
			this.pkmnCardPage.Controls.Add(this.pkmnNameField);
			this.pkmnCardPage.Controls.Add(this.pkmnOracleLabel);
			this.pkmnCardPage.Controls.Add(this.pkmnOracleField);
			this.pkmnCardPage.Controls.Add(this.pkmnTypeLabel);
			this.pkmnCardPage.Location = new System.Drawing.Point(4, 25);
			this.pkmnCardPage.Name = "pkmnCardPage";
			this.pkmnCardPage.Padding = new System.Windows.Forms.Padding(3);
			this.pkmnCardPage.Size = new System.Drawing.Size(1262, 666);
			this.pkmnCardPage.TabIndex = 0;
			this.pkmnCardPage.Text = "Card Entry";
			// 
			// pkmnPokemonButton
			// 
			this.pkmnPokemonButton.Location = new System.Drawing.Point(600, 75);
			this.pkmnPokemonButton.Name = "pkmnPokemonButton";
			this.pkmnPokemonButton.Size = new System.Drawing.Size(120, 29);
			this.pkmnPokemonButton.TabIndex = 56;
			this.pkmnPokemonButton.Text = "Pokémon";
			this.pkmnPokemonButton.UseVisualStyleBackColor = true;
			this.pkmnPokemonButton.Click += new System.EventHandler(this.PKMN_OnClickPokemon);
			// 
			// pkmnTrainerButton
			// 
			this.pkmnTrainerButton.Location = new System.Drawing.Point(475, 75);
			this.pkmnTrainerButton.Name = "pkmnTrainerButton";
			this.pkmnTrainerButton.Size = new System.Drawing.Size(120, 29);
			this.pkmnTrainerButton.TabIndex = 55;
			this.pkmnTrainerButton.Text = "Trainer";
			this.pkmnTrainerButton.UseVisualStyleBackColor = true;
			this.pkmnTrainerButton.Click += new System.EventHandler(this.PKMN_OnClickTrainer);
			// 
			// pkmnImportButton
			// 
			this.pkmnImportButton.Location = new System.Drawing.Point(555, 355);
			this.pkmnImportButton.Name = "pkmnImportButton";
			this.pkmnImportButton.Size = new System.Drawing.Size(250, 30);
			this.pkmnImportButton.TabIndex = 54;
			this.pkmnImportButton.Text = "Import!";
			this.pkmnImportButton.UseVisualStyleBackColor = true;
			this.pkmnImportButton.Click += new System.EventHandler(this.PKMN_OnClickImport);
			// 
			// pkmnImportField
			// 
			this.pkmnImportField.Location = new System.Drawing.Point(555, 180);
			this.pkmnImportField.Multiline = true;
			this.pkmnImportField.Name = "pkmnImportField";
			this.pkmnImportField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.pkmnImportField.Size = new System.Drawing.Size(250, 170);
			this.pkmnImportField.TabIndex = 53;
			// 
			// pkmnRetreatField
			// 
			this.pkmnRetreatField.Location = new System.Drawing.Point(100, 495);
			this.pkmnRetreatField.Name = "pkmnRetreatField";
			this.pkmnRetreatField.Size = new System.Drawing.Size(370, 29);
			this.pkmnRetreatField.TabIndex = 52;
			// 
			// pkmnResistField
			// 
			this.pkmnResistField.Location = new System.Drawing.Point(100, 460);
			this.pkmnResistField.Name = "pkmnResistField";
			this.pkmnResistField.Size = new System.Drawing.Size(370, 29);
			this.pkmnResistField.TabIndex = 51;
			// 
			// pkmnWeakField
			// 
			this.pkmnWeakField.Location = new System.Drawing.Point(100, 425);
			this.pkmnWeakField.Name = "pkmnWeakField";
			this.pkmnWeakField.Size = new System.Drawing.Size(370, 29);
			this.pkmnWeakField.TabIndex = 50;
			// 
			// pkmnRetreatLabel
			// 
			this.pkmnRetreatLabel.Location = new System.Drawing.Point(5, 495);
			this.pkmnRetreatLabel.Name = "pkmnRetreatLabel";
			this.pkmnRetreatLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnRetreatLabel.TabIndex = 49;
			this.pkmnRetreatLabel.Text = "Retreat:";
			this.pkmnRetreatLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnWeakLabel
			// 
			this.pkmnWeakLabel.Location = new System.Drawing.Point(5, 425);
			this.pkmnWeakLabel.Name = "pkmnWeakLabel";
			this.pkmnWeakLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnWeakLabel.TabIndex = 48;
			this.pkmnWeakLabel.Text = "Weakness:";
			this.pkmnWeakLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnStageLabel
			// 
			this.pkmnStageLabel.Location = new System.Drawing.Point(5, 110);
			this.pkmnStageLabel.Name = "pkmnStageLabel";
			this.pkmnStageLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnStageLabel.TabIndex = 47;
			this.pkmnStageLabel.Text = "Stage:";
			this.pkmnStageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnStageField
			// 
			this.pkmnStageField.Location = new System.Drawing.Point(100, 110);
			this.pkmnStageField.Name = "pkmnStageField";
			this.pkmnStageField.Size = new System.Drawing.Size(370, 29);
			this.pkmnStageField.TabIndex = 46;
			// 
			// pkmnIgnorDuplicateEntryLabel
			// 
			this.pkmnIgnorDuplicateEntryLabel.Location = new System.Drawing.Point(115, 565);
			this.pkmnIgnorDuplicateEntryLabel.Name = "pkmnIgnorDuplicateEntryLabel";
			this.pkmnIgnorDuplicateEntryLabel.Size = new System.Drawing.Size(235, 30);
			this.pkmnIgnorDuplicateEntryLabel.TabIndex = 45;
			this.pkmnIgnorDuplicateEntryLabel.Text = "Ignore Duplicate Entry";
			this.pkmnIgnorDuplicateEntryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnIgnorDuplicateEntryBox
			// 
			this.pkmnIgnorDuplicateEntryBox.Location = new System.Drawing.Point(100, 565);
			this.pkmnIgnorDuplicateEntryBox.Name = "pkmnIgnorDuplicateEntryBox";
			this.pkmnIgnorDuplicateEntryBox.Size = new System.Drawing.Size(14, 30);
			this.pkmnIgnorDuplicateEntryBox.TabIndex = 44;
			this.pkmnIgnorDuplicateEntryBox.UseVisualStyleBackColor = true;
			// 
			// pkmnTypeField
			// 
			this.pkmnTypeField.Location = new System.Drawing.Point(100, 75);
			this.pkmnTypeField.Name = "pkmnTypeField";
			this.pkmnTypeField.Size = new System.Drawing.Size(370, 29);
			this.pkmnTypeField.TabIndex = 18;
			// 
			// pkmnResistLabel
			// 
			this.pkmnResistLabel.Location = new System.Drawing.Point(5, 460);
			this.pkmnResistLabel.Name = "pkmnResistLabel";
			this.pkmnResistLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnResistLabel.TabIndex = 27;
			this.pkmnResistLabel.Text = "Resistance:";
			this.pkmnResistLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnETypeField
			// 
			this.pkmnETypeField.Location = new System.Drawing.Point(100, 40);
			this.pkmnETypeField.Name = "pkmnETypeField";
			this.pkmnETypeField.Size = new System.Drawing.Size(370, 29);
			this.pkmnETypeField.TabIndex = 16;
			// 
			// pkmnETypeLabel
			// 
			this.pkmnETypeLabel.Location = new System.Drawing.Point(5, 40);
			this.pkmnETypeLabel.Name = "pkmnETypeLabel";
			this.pkmnETypeLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnETypeLabel.TabIndex = 15;
			this.pkmnETypeLabel.Text = "Energy:";
			this.pkmnETypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnCardDialog
			// 
			this.pkmnCardDialog.AutoSize = true;
			this.pkmnCardDialog.Location = new System.Drawing.Point(355, 533);
			this.pkmnCardDialog.Name = "pkmnCardDialog";
			this.pkmnCardDialog.Size = new System.Drawing.Size(16, 21);
			this.pkmnCardDialog.TabIndex = 31;
			this.pkmnCardDialog.Text = "-";
			this.pkmnCardDialog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnHPField
			// 
			this.pkmnHPField.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.pkmnHPField.Location = new System.Drawing.Point(100, 145);
			this.pkmnHPField.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.pkmnHPField.Name = "pkmnHPField";
			this.pkmnHPField.Size = new System.Drawing.Size(370, 29);
			this.pkmnHPField.TabIndex = 25;
			// 
			// pkmnHPLabel
			// 
			this.pkmnHPLabel.Location = new System.Drawing.Point(5, 145);
			this.pkmnHPLabel.Name = "pkmnHPLabel";
			this.pkmnHPLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnHPLabel.TabIndex = 24;
			this.pkmnHPLabel.Text = "HP:";
			this.pkmnHPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnAddCardButton
			// 
			this.pkmnAddCardButton.Location = new System.Drawing.Point(100, 530);
			this.pkmnAddCardButton.Name = "pkmnAddCardButton";
			this.pkmnAddCardButton.Size = new System.Drawing.Size(250, 30);
			this.pkmnAddCardButton.TabIndex = 30;
			this.pkmnAddCardButton.Text = "Add To Catalog";
			this.pkmnAddCardButton.UseVisualStyleBackColor = true;
			this.pkmnAddCardButton.Click += new System.EventHandler(this.PKMN_OnClickAddCard);
			// 
			// pkmnNameLabel
			// 
			this.pkmnNameLabel.Location = new System.Drawing.Point(5, 5);
			this.pkmnNameLabel.Name = "pkmnNameLabel";
			this.pkmnNameLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnNameLabel.TabIndex = 1;
			this.pkmnNameLabel.Text = "Name:";
			this.pkmnNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnNameField
			// 
			this.pkmnNameField.Location = new System.Drawing.Point(100, 5);
			this.pkmnNameField.Name = "pkmnNameField";
			this.pkmnNameField.Size = new System.Drawing.Size(370, 29);
			this.pkmnNameField.TabIndex = 2;
			// 
			// pkmnOracleLabel
			// 
			this.pkmnOracleLabel.Location = new System.Drawing.Point(5, 180);
			this.pkmnOracleLabel.Name = "pkmnOracleLabel";
			this.pkmnOracleLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnOracleLabel.TabIndex = 22;
			this.pkmnOracleLabel.Text = "Oracle Text:";
			this.pkmnOracleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnOracleField
			// 
			this.pkmnOracleField.Location = new System.Drawing.Point(100, 180);
			this.pkmnOracleField.Multiline = true;
			this.pkmnOracleField.Name = "pkmnOracleField";
			this.pkmnOracleField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.pkmnOracleField.Size = new System.Drawing.Size(450, 240);
			this.pkmnOracleField.TabIndex = 23;
			// 
			// pkmnTypeLabel
			// 
			this.pkmnTypeLabel.Location = new System.Drawing.Point(5, 75);
			this.pkmnTypeLabel.Name = "pkmnTypeLabel";
			this.pkmnTypeLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnTypeLabel.TabIndex = 17;
			this.pkmnTypeLabel.Text = "Card Types:";
			this.pkmnTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnPrintPage
			// 
			this.pkmnPrintPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pkmnPrintPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pkmnPrintPage.Controls.Add(this.pkmnPrintHoloButton);
			this.pkmnPrintPage.Controls.Add(this.pkmnPrintNormalButton);
			this.pkmnPrintPage.Controls.Add(this.pkmnPrintAutoLimit);
			this.pkmnPrintPage.Controls.Add(this.pkmnPrintAutofillRangeButton);
			this.pkmnPrintPage.Controls.Add(this.pkmnPrintAutofillButton);
			this.pkmnPrintPage.Controls.Add(this.pkmnCardrefDescriptor);
			this.pkmnPrintPage.Controls.Add(this.pkmnRarityField);
			this.pkmnPrintPage.Controls.Add(this.pkmnRarityLabel);
			this.pkmnPrintPage.Controls.Add(this.pkmnPrintIDLabel);
			this.pkmnPrintPage.Controls.Add(this.pkmnPrintIDField);
			this.pkmnPrintPage.Controls.Add(this.pkmnImgpathBackLabel);
			this.pkmnPrintPage.Controls.Add(this.pkmnPrintImgboxBack);
			this.pkmnPrintPage.Controls.Add(this.pkmnImgsearchBackButton);
			this.pkmnPrintPage.Controls.Add(this.pkmnImgBackLabel);
			this.pkmnPrintPage.Controls.Add(this.pkmnIODialog);
			this.pkmnPrintPage.Controls.Add(this.pkmnSaveButton);
			this.pkmnPrintPage.Controls.Add(this.pkmnImgpathLabel);
			this.pkmnPrintPage.Controls.Add(this.pkmnRemTreatmentButton);
			this.pkmnPrintPage.Controls.Add(this.pkmnAddTreatmentButton);
			this.pkmnPrintPage.Controls.Add(this.pkmnPrintImgbox);
			this.pkmnPrintPage.Controls.Add(this.pkmnAddPrintButton);
			this.pkmnPrintPage.Controls.Add(this.pkmnCardrefField);
			this.pkmnPrintPage.Controls.Add(this.pkmnCardrefLabel);
			this.pkmnPrintPage.Controls.Add(this.pkmnImgsearchButton);
			this.pkmnPrintPage.Controls.Add(this.pkmnImgLabel);
			this.pkmnPrintPage.Controls.Add(this.pkmnFlavorField);
			this.pkmnPrintPage.Controls.Add(this.pkmnFlavorLabel);
			this.pkmnPrintPage.Controls.Add(this.pkmnTreatmentsValue);
			this.pkmnPrintPage.Controls.Add(this.pkmnTreatmentField);
			this.pkmnPrintPage.Controls.Add(this.pkmnTreatmentLabel);
			this.pkmnPrintPage.Controls.Add(this.pkmnNumberField);
			this.pkmnPrintPage.Controls.Add(this.pkmnNumberLabel);
			this.pkmnPrintPage.Controls.Add(this.pkmnSetField);
			this.pkmnPrintPage.Controls.Add(this.pkmnSetLabel);
			this.pkmnPrintPage.Location = new System.Drawing.Point(4, 25);
			this.pkmnPrintPage.Name = "pkmnPrintPage";
			this.pkmnPrintPage.Padding = new System.Windows.Forms.Padding(3);
			this.pkmnPrintPage.Size = new System.Drawing.Size(1262, 666);
			this.pkmnPrintPage.TabIndex = 1;
			this.pkmnPrintPage.Text = "Printing Entry";
			// 
			// pkmnPrintHoloButton
			// 
			this.pkmnPrintHoloButton.Location = new System.Drawing.Point(440, 145);
			this.pkmnPrintHoloButton.Name = "pkmnPrintHoloButton";
			this.pkmnPrintHoloButton.Size = new System.Drawing.Size(30, 29);
			this.pkmnPrintHoloButton.TabIndex = 33;
			this.pkmnPrintHoloButton.Text = "H";
			this.pkmnPrintHoloButton.UseVisualStyleBackColor = true;
			this.pkmnPrintHoloButton.Click += new System.EventHandler(this.PKMN_OnClickHoloTreatment);
			// 
			// pkmnPrintNormalButton
			// 
			this.pkmnPrintNormalButton.Location = new System.Drawing.Point(405, 145);
			this.pkmnPrintNormalButton.Name = "pkmnPrintNormalButton";
			this.pkmnPrintNormalButton.Size = new System.Drawing.Size(30, 29);
			this.pkmnPrintNormalButton.TabIndex = 32;
			this.pkmnPrintNormalButton.Text = "N";
			this.pkmnPrintNormalButton.UseVisualStyleBackColor = true;
			this.pkmnPrintNormalButton.Click += new System.EventHandler(this.PKMN_OnClickNormalTreatment);
			// 
			// pkmnPrintAutoLimit
			// 
			this.pkmnPrintAutoLimit.Location = new System.Drawing.Point(220, 510);
			this.pkmnPrintAutoLimit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.pkmnPrintAutoLimit.Name = "pkmnPrintAutoLimit";
			this.pkmnPrintAutoLimit.Size = new System.Drawing.Size(250, 29);
			this.pkmnPrintAutoLimit.TabIndex = 31;
			// 
			// pkmnPrintAutofillRangeButton
			// 
			this.pkmnPrintAutofillRangeButton.Location = new System.Drawing.Point(100, 510);
			this.pkmnPrintAutofillRangeButton.Name = "pkmnPrintAutofillRangeButton";
			this.pkmnPrintAutofillRangeButton.Size = new System.Drawing.Size(115, 29);
			this.pkmnPrintAutofillRangeButton.TabIndex = 30;
			this.pkmnPrintAutofillRangeButton.Text = "Autofill to";
			this.pkmnPrintAutofillRangeButton.UseVisualStyleBackColor = true;
			this.pkmnPrintAutofillRangeButton.Click += new System.EventHandler(this.PKMN_OnClickAddAndFill);
			// 
			// pkmnPrintAutofillButton
			// 
			this.pkmnPrintAutofillButton.Location = new System.Drawing.Point(320, 335);
			this.pkmnPrintAutofillButton.Name = "pkmnPrintAutofillButton";
			this.pkmnPrintAutofillButton.Size = new System.Drawing.Size(150, 29);
			this.pkmnPrintAutofillButton.TabIndex = 29;
			this.pkmnPrintAutofillButton.Text = "Autofill";
			this.pkmnPrintAutofillButton.UseVisualStyleBackColor = true;
			this.pkmnPrintAutofillButton.Click += new System.EventHandler(this.PKMN_OnClickPrintAutofill);
			// 
			// pkmnCardrefDescriptor
			// 
			this.pkmnCardrefDescriptor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pkmnCardrefDescriptor.AutoSize = true;
			this.pkmnCardrefDescriptor.Location = new System.Drawing.Point(5, 603);
			this.pkmnCardrefDescriptor.MaximumSize = new System.Drawing.Size(0, 21);
			this.pkmnCardrefDescriptor.Name = "pkmnCardrefDescriptor";
			this.pkmnCardrefDescriptor.Size = new System.Drawing.Size(16, 21);
			this.pkmnCardrefDescriptor.TabIndex = 28;
			this.pkmnCardrefDescriptor.Text = "-";
			this.pkmnCardrefDescriptor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnRarityField
			// 
			this.pkmnRarityField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.pkmnRarityField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.pkmnRarityField.FormattingEnabled = true;
			this.pkmnRarityField.Location = new System.Drawing.Point(100, 75);
			this.pkmnRarityField.Name = "pkmnRarityField";
			this.pkmnRarityField.Size = new System.Drawing.Size(370, 29);
			this.pkmnRarityField.Sorted = true;
			this.pkmnRarityField.TabIndex = 6;
			// 
			// pkmnRarityLabel
			// 
			this.pkmnRarityLabel.Location = new System.Drawing.Point(5, 75);
			this.pkmnRarityLabel.Name = "pkmnRarityLabel";
			this.pkmnRarityLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnRarityLabel.TabIndex = 5;
			this.pkmnRarityLabel.Text = "Rarity:";
			this.pkmnRarityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnPrintIDLabel
			// 
			this.pkmnPrintIDLabel.Location = new System.Drawing.Point(5, 440);
			this.pkmnPrintIDLabel.Name = "pkmnPrintIDLabel";
			this.pkmnPrintIDLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnPrintIDLabel.TabIndex = 20;
			this.pkmnPrintIDLabel.Text = "Print ID:";
			this.pkmnPrintIDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnPrintIDField
			// 
			this.pkmnPrintIDField.Location = new System.Drawing.Point(100, 440);
			this.pkmnPrintIDField.Name = "pkmnPrintIDField";
			this.pkmnPrintIDField.Size = new System.Drawing.Size(370, 29);
			this.pkmnPrintIDField.TabIndex = 21;
			// 
			// pkmnImgpathBackLabel
			// 
			this.pkmnImgpathBackLabel.AutoSize = true;
			this.pkmnImgpathBackLabel.Location = new System.Drawing.Point(855, 505);
			this.pkmnImgpathBackLabel.Name = "pkmnImgpathBackLabel";
			this.pkmnImgpathBackLabel.Size = new System.Drawing.Size(0, 21);
			this.pkmnImgpathBackLabel.TabIndex = 27;
			// 
			// pkmnPrintImgboxBack
			// 
			this.pkmnPrintImgboxBack.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pkmnPrintImgboxBack.InitialImage = null;
			this.pkmnPrintImgboxBack.Location = new System.Drawing.Point(855, 5);
			this.pkmnPrintImgboxBack.Name = "pkmnPrintImgboxBack";
			this.pkmnPrintImgboxBack.Size = new System.Drawing.Size(375, 495);
			this.pkmnPrintImgboxBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pkmnPrintImgboxBack.TabIndex = 27;
			this.pkmnPrintImgboxBack.TabStop = false;
			// 
			// pkmnImgsearchBackButton
			// 
			this.pkmnImgsearchBackButton.Location = new System.Drawing.Point(100, 370);
			this.pkmnImgsearchBackButton.Name = "pkmnImgsearchBackButton";
			this.pkmnImgsearchBackButton.Size = new System.Drawing.Size(370, 29);
			this.pkmnImgsearchBackButton.TabIndex = 17;
			this.pkmnImgsearchBackButton.Text = "Search";
			this.pkmnImgsearchBackButton.UseVisualStyleBackColor = true;
			this.pkmnImgsearchBackButton.Click += new System.EventHandler(this.PKMN_OnClickSearchImgBack);
			// 
			// pkmnImgBackLabel
			// 
			this.pkmnImgBackLabel.Location = new System.Drawing.Point(5, 370);
			this.pkmnImgBackLabel.Name = "pkmnImgBackLabel";
			this.pkmnImgBackLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnImgBackLabel.TabIndex = 16;
			this.pkmnImgBackLabel.Text = "Back:";
			this.pkmnImgBackLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnIODialog
			// 
			this.pkmnIODialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pkmnIODialog.AutoSize = true;
			this.pkmnIODialog.Location = new System.Drawing.Point(260, 630);
			this.pkmnIODialog.Name = "pkmnIODialog";
			this.pkmnIODialog.Size = new System.Drawing.Size(16, 21);
			this.pkmnIODialog.TabIndex = 25;
			this.pkmnIODialog.Text = "-";
			this.pkmnIODialog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnSaveButton
			// 
			this.pkmnSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pkmnSaveButton.Location = new System.Drawing.Point(5, 627);
			this.pkmnSaveButton.Name = "pkmnSaveButton";
			this.pkmnSaveButton.Size = new System.Drawing.Size(250, 29);
			this.pkmnSaveButton.TabIndex = 24;
			this.pkmnSaveButton.Text = "Save Catalog";
			this.pkmnSaveButton.UseVisualStyleBackColor = true;
			this.pkmnSaveButton.Click += new System.EventHandler(this.PKMN_OnClickSave);
			// 
			// pkmnImgpathLabel
			// 
			this.pkmnImgpathLabel.AutoSize = true;
			this.pkmnImgpathLabel.Location = new System.Drawing.Point(475, 505);
			this.pkmnImgpathLabel.Name = "pkmnImgpathLabel";
			this.pkmnImgpathLabel.Size = new System.Drawing.Size(16, 21);
			this.pkmnImgpathLabel.TabIndex = 26;
			this.pkmnImgpathLabel.Text = "-";
			// 
			// pkmnRemTreatmentButton
			// 
			this.pkmnRemTreatmentButton.Location = new System.Drawing.Point(440, 110);
			this.pkmnRemTreatmentButton.Name = "pkmnRemTreatmentButton";
			this.pkmnRemTreatmentButton.Size = new System.Drawing.Size(30, 29);
			this.pkmnRemTreatmentButton.TabIndex = 10;
			this.pkmnRemTreatmentButton.Text = "-";
			this.pkmnRemTreatmentButton.UseVisualStyleBackColor = true;
			this.pkmnRemTreatmentButton.Click += new System.EventHandler(this.PKMN_OnClickSubTreatment);
			// 
			// pkmnAddTreatmentButton
			// 
			this.pkmnAddTreatmentButton.Location = new System.Drawing.Point(405, 110);
			this.pkmnAddTreatmentButton.Name = "pkmnAddTreatmentButton";
			this.pkmnAddTreatmentButton.Size = new System.Drawing.Size(30, 29);
			this.pkmnAddTreatmentButton.TabIndex = 9;
			this.pkmnAddTreatmentButton.Text = "+";
			this.pkmnAddTreatmentButton.UseVisualStyleBackColor = true;
			this.pkmnAddTreatmentButton.Click += new System.EventHandler(this.PKMN_OnClickAddTreatment);
			// 
			// pkmnPrintImgbox
			// 
			this.pkmnPrintImgbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pkmnPrintImgbox.InitialImage = null;
			this.pkmnPrintImgbox.Location = new System.Drawing.Point(475, 5);
			this.pkmnPrintImgbox.Name = "pkmnPrintImgbox";
			this.pkmnPrintImgbox.Size = new System.Drawing.Size(375, 495);
			this.pkmnPrintImgbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pkmnPrintImgbox.TabIndex = 20;
			this.pkmnPrintImgbox.TabStop = false;
			// 
			// pkmnAddPrintButton
			// 
			this.pkmnAddPrintButton.Location = new System.Drawing.Point(100, 475);
			this.pkmnAddPrintButton.Name = "pkmnAddPrintButton";
			this.pkmnAddPrintButton.Size = new System.Drawing.Size(370, 29);
			this.pkmnAddPrintButton.TabIndex = 22;
			this.pkmnAddPrintButton.Text = "Add To Catalog";
			this.pkmnAddPrintButton.UseVisualStyleBackColor = true;
			this.pkmnAddPrintButton.Click += new System.EventHandler(this.PKMN_OnClickAddPrint);
			// 
			// pkmnCardrefField
			// 
			this.pkmnCardrefField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.pkmnCardrefField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.pkmnCardrefField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pkmnCardrefField.FormattingEnabled = true;
			this.pkmnCardrefField.Location = new System.Drawing.Point(100, 405);
			this.pkmnCardrefField.Name = "pkmnCardrefField";
			this.pkmnCardrefField.Size = new System.Drawing.Size(750, 29);
			this.pkmnCardrefField.Sorted = true;
			this.pkmnCardrefField.TabIndex = 19;
			this.pkmnCardrefField.SelectedIndexChanged += new System.EventHandler(this.PKMN_OnSelectCardref);
			// 
			// pkmnCardrefLabel
			// 
			this.pkmnCardrefLabel.Location = new System.Drawing.Point(5, 405);
			this.pkmnCardrefLabel.Name = "pkmnCardrefLabel";
			this.pkmnCardrefLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnCardrefLabel.TabIndex = 18;
			this.pkmnCardrefLabel.Text = "Card:";
			this.pkmnCardrefLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnImgsearchButton
			// 
			this.pkmnImgsearchButton.Location = new System.Drawing.Point(100, 335);
			this.pkmnImgsearchButton.Name = "pkmnImgsearchButton";
			this.pkmnImgsearchButton.Size = new System.Drawing.Size(215, 29);
			this.pkmnImgsearchButton.TabIndex = 15;
			this.pkmnImgsearchButton.Text = "Search";
			this.pkmnImgsearchButton.UseVisualStyleBackColor = true;
			this.pkmnImgsearchButton.Click += new System.EventHandler(this.PKMN_OnClickSearchImg);
			// 
			// pkmnImgLabel
			// 
			this.pkmnImgLabel.Location = new System.Drawing.Point(5, 335);
			this.pkmnImgLabel.Name = "pkmnImgLabel";
			this.pkmnImgLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnImgLabel.TabIndex = 14;
			this.pkmnImgLabel.Text = "Image:";
			this.pkmnImgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnFlavorField
			// 
			this.pkmnFlavorField.Location = new System.Drawing.Point(100, 180);
			this.pkmnFlavorField.Multiline = true;
			this.pkmnFlavorField.Name = "pkmnFlavorField";
			this.pkmnFlavorField.Size = new System.Drawing.Size(370, 150);
			this.pkmnFlavorField.TabIndex = 13;
			// 
			// pkmnFlavorLabel
			// 
			this.pkmnFlavorLabel.Location = new System.Drawing.Point(5, 180);
			this.pkmnFlavorLabel.Name = "pkmnFlavorLabel";
			this.pkmnFlavorLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnFlavorLabel.TabIndex = 12;
			this.pkmnFlavorLabel.Text = "Flavor Text:";
			this.pkmnFlavorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnTreatmentsValue
			// 
			this.pkmnTreatmentsValue.Location = new System.Drawing.Point(100, 145);
			this.pkmnTreatmentsValue.Name = "pkmnTreatmentsValue";
			this.pkmnTreatmentsValue.Size = new System.Drawing.Size(350, 30);
			this.pkmnTreatmentsValue.TabIndex = 11;
			this.pkmnTreatmentsValue.Text = "-";
			this.pkmnTreatmentsValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnTreatmentField
			// 
			this.pkmnTreatmentField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.pkmnTreatmentField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.pkmnTreatmentField.FormattingEnabled = true;
			this.pkmnTreatmentField.Location = new System.Drawing.Point(100, 110);
			this.pkmnTreatmentField.Name = "pkmnTreatmentField";
			this.pkmnTreatmentField.Size = new System.Drawing.Size(300, 29);
			this.pkmnTreatmentField.TabIndex = 8;
			// 
			// pkmnTreatmentLabel
			// 
			this.pkmnTreatmentLabel.Location = new System.Drawing.Point(5, 110);
			this.pkmnTreatmentLabel.Name = "pkmnTreatmentLabel";
			this.pkmnTreatmentLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnTreatmentLabel.TabIndex = 7;
			this.pkmnTreatmentLabel.Text = "Treatments:";
			this.pkmnTreatmentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnNumberField
			// 
			this.pkmnNumberField.Location = new System.Drawing.Point(100, 40);
			this.pkmnNumberField.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.pkmnNumberField.Name = "pkmnNumberField";
			this.pkmnNumberField.Size = new System.Drawing.Size(370, 29);
			this.pkmnNumberField.TabIndex = 4;
			// 
			// pkmnNumberLabel
			// 
			this.pkmnNumberLabel.Location = new System.Drawing.Point(5, 40);
			this.pkmnNumberLabel.Name = "pkmnNumberLabel";
			this.pkmnNumberLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnNumberLabel.TabIndex = 3;
			this.pkmnNumberLabel.Text = "Number:";
			this.pkmnNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnSetField
			// 
			this.pkmnSetField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pkmnSetField.FormattingEnabled = true;
			this.pkmnSetField.Location = new System.Drawing.Point(100, 5);
			this.pkmnSetField.Name = "pkmnSetField";
			this.pkmnSetField.Size = new System.Drawing.Size(370, 29);
			this.pkmnSetField.Sorted = true;
			this.pkmnSetField.TabIndex = 2;
			// 
			// pkmnSetLabel
			// 
			this.pkmnSetLabel.Location = new System.Drawing.Point(5, 5);
			this.pkmnSetLabel.Name = "pkmnSetLabel";
			this.pkmnSetLabel.Size = new System.Drawing.Size(90, 30);
			this.pkmnSetLabel.TabIndex = 1;
			this.pkmnSetLabel.Text = "Set:";
			this.pkmnSetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnSymbolPage
			// 
			this.pkmnSymbolPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pkmnSymbolPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pkmnSymbolPage.Controls.Add(this.pkmnSymbolLayout);
			this.pkmnSymbolPage.Location = new System.Drawing.Point(4, 25);
			this.pkmnSymbolPage.Name = "pkmnSymbolPage";
			this.pkmnSymbolPage.Padding = new System.Windows.Forms.Padding(3);
			this.pkmnSymbolPage.Size = new System.Drawing.Size(1262, 666);
			this.pkmnSymbolPage.TabIndex = 5;
			this.pkmnSymbolPage.Text = "Symbols";
			// 
			// pkmnSymbolLayout
			// 
			this.pkmnSymbolLayout.AutoScroll = true;
			this.pkmnSymbolLayout.Controls.Add(this.pkmnSymbolHeaderPanel);
			this.pkmnSymbolLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pkmnSymbolLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.pkmnSymbolLayout.Location = new System.Drawing.Point(3, 3);
			this.pkmnSymbolLayout.Name = "pkmnSymbolLayout";
			this.pkmnSymbolLayout.Size = new System.Drawing.Size(1252, 656);
			this.pkmnSymbolLayout.TabIndex = 0;
			// 
			// pkmnSymbolHeaderPanel
			// 
			this.pkmnSymbolHeaderPanel.Controls.Add(this.pkmnSaveSymbolsButton);
			this.pkmnSymbolHeaderPanel.Controls.Add(this.pkmnAddSymbolButton);
			this.pkmnSymbolHeaderPanel.Controls.Add(this.pkmnSymbolLabel);
			this.pkmnSymbolHeaderPanel.Location = new System.Drawing.Point(3, 3);
			this.pkmnSymbolHeaderPanel.Name = "pkmnSymbolHeaderPanel";
			this.pkmnSymbolHeaderPanel.Size = new System.Drawing.Size(325, 40);
			this.pkmnSymbolHeaderPanel.TabIndex = 1;
			// 
			// pkmnSaveSymbolsButton
			// 
			this.pkmnSaveSymbolsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pkmnSaveSymbolsButton.Location = new System.Drawing.Point(258, 5);
			this.pkmnSaveSymbolsButton.Name = "pkmnSaveSymbolsButton";
			this.pkmnSaveSymbolsButton.Size = new System.Drawing.Size(60, 29);
			this.pkmnSaveSymbolsButton.TabIndex = 2;
			this.pkmnSaveSymbolsButton.Text = "Save";
			this.pkmnSaveSymbolsButton.UseVisualStyleBackColor = true;
			this.pkmnSaveSymbolsButton.Click += new System.EventHandler(this.PKMN_OnClickSaveSymbols);
			// 
			// pkmnAddSymbolButton
			// 
			this.pkmnAddSymbolButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pkmnAddSymbolButton.Location = new System.Drawing.Point(223, 5);
			this.pkmnAddSymbolButton.Name = "pkmnAddSymbolButton";
			this.pkmnAddSymbolButton.Size = new System.Drawing.Size(30, 29);
			this.pkmnAddSymbolButton.TabIndex = 1;
			this.pkmnAddSymbolButton.Text = "+";
			this.pkmnAddSymbolButton.UseVisualStyleBackColor = true;
			this.pkmnAddSymbolButton.Click += new System.EventHandler(this.PKMN_OnClickAddSymbol);
			// 
			// pkmnSymbolLabel
			// 
			this.pkmnSymbolLabel.Location = new System.Drawing.Point(5, 5);
			this.pkmnSymbolLabel.Name = "pkmnSymbolLabel";
			this.pkmnSymbolLabel.Size = new System.Drawing.Size(150, 30);
			this.pkmnSymbolLabel.TabIndex = 0;
			this.pkmnSymbolLabel.Text = "Symbols";
			this.pkmnSymbolLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pkmnSetPage
			// 
			this.pkmnSetPage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pkmnSetPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pkmnSetPage.Controls.Add(this.pkmnReloadSetsButton);
			this.pkmnSetPage.Controls.Add(this.pkmnSetGeneratorPrev);
			this.pkmnSetPage.Controls.Add(this.pkmnAddSetButton);
			this.pkmnSetPage.Controls.Add(this.pkmnSaveSetsButton);
			this.pkmnSetPage.Controls.Add(this.pkmnSetGeneratorNext);
			this.pkmnSetPage.Controls.Add(this.pkmnSetGeneratorPageLabel);
			this.pkmnSetPage.Controls.Add(this.pkmnSetGeneratorLayout);
			this.pkmnSetPage.Controls.Add(this.pkmnSetGeneratorLabel);
			this.pkmnSetPage.Location = new System.Drawing.Point(4, 33);
			this.pkmnSetPage.Name = "pkmnSetPage";
			this.pkmnSetPage.Padding = new System.Windows.Forms.Padding(3);
			this.pkmnSetPage.Size = new System.Drawing.Size(1262, 658);
			this.pkmnSetPage.TabIndex = 6;
			this.pkmnSetPage.Text = "Sets";
			// 
			// pkmnReloadSetsButton
			// 
			this.pkmnReloadSetsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pkmnReloadSetsButton.Location = new System.Drawing.Point(880, 5);
			this.pkmnReloadSetsButton.Name = "pkmnReloadSetsButton";
			this.pkmnReloadSetsButton.Size = new System.Drawing.Size(75, 29);
			this.pkmnReloadSetsButton.TabIndex = 3;
			this.pkmnReloadSetsButton.Text = "Reload";
			this.pkmnReloadSetsButton.UseVisualStyleBackColor = true;
			this.pkmnReloadSetsButton.Click += new System.EventHandler(this.PKMN_RegenerateSets);
			// 
			// pkmnSetGeneratorPrev
			// 
			this.pkmnSetGeneratorPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pkmnSetGeneratorPrev.Location = new System.Drawing.Point(1060, 5);
			this.pkmnSetGeneratorPrev.Name = "pkmnSetGeneratorPrev";
			this.pkmnSetGeneratorPrev.Size = new System.Drawing.Size(60, 29);
			this.pkmnSetGeneratorPrev.TabIndex = 5;
			this.pkmnSetGeneratorPrev.Text = "<";
			this.pkmnSetGeneratorPrev.UseVisualStyleBackColor = true;
			this.pkmnSetGeneratorPrev.Click += new System.EventHandler(this.PKMN_OnClickSetGeneratorPrev);
			// 
			// pkmnAddSetButton
			// 
			this.pkmnAddSetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pkmnAddSetButton.Location = new System.Drawing.Point(960, 5);
			this.pkmnAddSetButton.Name = "pkmnAddSetButton";
			this.pkmnAddSetButton.Size = new System.Drawing.Size(30, 29);
			this.pkmnAddSetButton.TabIndex = 1;
			this.pkmnAddSetButton.Text = "+";
			this.pkmnAddSetButton.UseVisualStyleBackColor = true;
			this.pkmnAddSetButton.Click += new System.EventHandler(this.PKMN_OnClickAddSet);
			// 
			// pkmnSaveSetsButton
			// 
			this.pkmnSaveSetsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pkmnSaveSetsButton.Location = new System.Drawing.Point(995, 5);
			this.pkmnSaveSetsButton.Name = "pkmnSaveSetsButton";
			this.pkmnSaveSetsButton.Size = new System.Drawing.Size(60, 29);
			this.pkmnSaveSetsButton.TabIndex = 2;
			this.pkmnSaveSetsButton.Text = "Save";
			this.pkmnSaveSetsButton.UseVisualStyleBackColor = true;
			this.pkmnSaveSetsButton.Click += new System.EventHandler(this.PKMN_OnClickSaveSets);
			// 
			// pkmnSetGeneratorNext
			// 
			this.pkmnSetGeneratorNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pkmnSetGeneratorNext.Location = new System.Drawing.Point(1190, 5);
			this.pkmnSetGeneratorNext.Name = "pkmnSetGeneratorNext";
			this.pkmnSetGeneratorNext.Size = new System.Drawing.Size(60, 29);
			this.pkmnSetGeneratorNext.TabIndex = 4;
			this.pkmnSetGeneratorNext.Text = ">";
			this.pkmnSetGeneratorNext.UseVisualStyleBackColor = true;
			this.pkmnSetGeneratorNext.Click += new System.EventHandler(this.PKMN_OnClickSetGeneratorNext);
			// 
			// pkmnSetGeneratorPageLabel
			// 
			this.pkmnSetGeneratorPageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pkmnSetGeneratorPageLabel.Location = new System.Drawing.Point(1125, 5);
			this.pkmnSetGeneratorPageLabel.Name = "pkmnSetGeneratorPageLabel";
			this.pkmnSetGeneratorPageLabel.Size = new System.Drawing.Size(60, 30);
			this.pkmnSetGeneratorPageLabel.TabIndex = 2;
			this.pkmnSetGeneratorPageLabel.Text = "X / X";
			this.pkmnSetGeneratorPageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pkmnSetGeneratorLayout
			// 
			this.pkmnSetGeneratorLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pkmnSetGeneratorLayout.AutoScroll = true;
			this.pkmnSetGeneratorLayout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pkmnSetGeneratorLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.pkmnSetGeneratorLayout.Location = new System.Drawing.Point(3, 38);
			this.pkmnSetGeneratorLayout.Name = "pkmnSetGeneratorLayout";
			this.pkmnSetGeneratorLayout.Size = new System.Drawing.Size(1252, 612);
			this.pkmnSetGeneratorLayout.TabIndex = 1;
			// 
			// pkmnSetGeneratorLabel
			// 
			this.pkmnSetGeneratorLabel.AutoSize = true;
			this.pkmnSetGeneratorLabel.Location = new System.Drawing.Point(5, 8);
			this.pkmnSetGeneratorLabel.Name = "pkmnSetGeneratorLabel";
			this.pkmnSetGeneratorLabel.Size = new System.Drawing.Size(69, 21);
			this.pkmnSetGeneratorLabel.TabIndex = 0;
			this.pkmnSetGeneratorLabel.Text = "Edit Sets";
			this.pkmnSetGeneratorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Form1
			// 
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ClientSize = new System.Drawing.Size(1284, 738);
			this.Controls.Add(this.formTabControl);
			this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "Form1";
			this.ygoTabControl.ResumeLayout(false);
			this.ygoSetPage.ResumeLayout(false);
			this.ygoSetPage.PerformLayout();
			this.ygoSearchPage.ResumeLayout(false);
			this.ygoSearchPage.PerformLayout();
			this.ygoCatalogPage.ResumeLayout(false);
			this.ygoCatalogPage.PerformLayout();
			this.ygoDetailPage.ResumeLayout(false);
			this.ygoDetailPage.PerformLayout();
			this.ygoCardtipBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ygoCardtipImage)).EndInit();
			this.ygoDetailBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ygoDetailImgbox)).EndInit();
			this.ygoCardPage.ResumeLayout(false);
			this.ygoCardPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ygoScaleField)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ygoLevelField)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ygoDefField)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ygoAtkField)).EndInit();
			this.ygoPrintPage.ResumeLayout(false);
			this.ygoPrintPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ygoPrintAutoLimit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ygoPrintImgboxBack)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ygoPrintImgbox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ygoNumberField)).EndInit();
			this.ygoSetsPage.ResumeLayout(false);
			this.ygoSetsPage.PerformLayout();
			this.formTabControl.ResumeLayout(false);
			this.mtgPage.ResumeLayout(false);
			this.mtgTabControl.ResumeLayout(false);
			this.mtgSetPage.ResumeLayout(false);
			this.mtgSetPage.PerformLayout();
			this.mtgSearchPage.ResumeLayout(false);
			this.mtgSearchPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchColImgG)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchColImgR)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchColImgB)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchColImgU)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchColImgW)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchIDImgG)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchIDImgR)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchIDImgB)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchIDImgU)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgSearchIDImgW)).EndInit();
			this.mtgCatalogPage.ResumeLayout(false);
			this.mtgCatalogPage.PerformLayout();
			this.mtgDetailPage.ResumeLayout(false);
			this.mtgDetailPage.PerformLayout();
			this.mtgCardtipBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mtgCardtipImage)).EndInit();
			this.mtgDetailBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mtgDetailImgbox)).EndInit();
			this.mtgCardPage.ResumeLayout(false);
			this.mtgCardPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.mtgToughnessBackField)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgPowerBackField)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgColourImgG)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgColourImgR)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgColourImgB)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgColourImgU)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgColourImgW)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgIdentityImgG)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgIdentityImgR)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgIdentityImgB)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgIdentityImgU)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgIdentityImgW)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgToughnessField)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgPowerField)).EndInit();
			this.mtgPrintPage.ResumeLayout(false);
			this.mtgPrintPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.mtgPrintAutoLimit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgPrintImgboxBack)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgPrintImgbox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mtgNumberField)).EndInit();
			this.mtgSymbolsPage.ResumeLayout(false);
			this.mtgSymbolLayout.ResumeLayout(false);
			this.mtgSymbolHeaderBox.ResumeLayout(false);
			this.mtgSetsPage.ResumeLayout(false);
			this.mtgSetsPage.PerformLayout();
			this.ygoPage.ResumeLayout(false);
			this.pkmnPage.ResumeLayout(false);
			this.pkmnTabControl.ResumeLayout(false);
			this.pkmnSetlistPage.ResumeLayout(false);
			this.pkmnSetlistPage.PerformLayout();
			this.pkmnSearchPage.ResumeLayout(false);
			this.pkmnSearchPage.PerformLayout();
			this.pkmnCatalogPage.ResumeLayout(false);
			this.pkmnCatalogPage.PerformLayout();
			this.pkmnDetailPage.ResumeLayout(false);
			this.pkmnDetailPage.PerformLayout();
			this.pkmnCardtipPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pkmnCardtipImage)).EndInit();
			this.pkmnDetailPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pkmnDetailImgbox)).EndInit();
			this.pkmnCardPage.ResumeLayout(false);
			this.pkmnCardPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pkmnHPField)).EndInit();
			this.pkmnPrintPage.ResumeLayout(false);
			this.pkmnPrintPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pkmnPrintAutoLimit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pkmnPrintImgboxBack)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pkmnPrintImgbox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pkmnNumberField)).EndInit();
			this.pkmnSymbolPage.ResumeLayout(false);
			this.pkmnSymbolLayout.ResumeLayout(false);
			this.pkmnSymbolHeaderPanel.ResumeLayout(false);
			this.pkmnSetPage.ResumeLayout(false);
			this.pkmnSetPage.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label ygoNameLabel;
		private System.Windows.Forms.TabControl ygoTabControl;
		private System.Windows.Forms.TabPage ygoCardPage;
		private System.Windows.Forms.TabPage ygoPrintPage;
		private System.Windows.Forms.TabPage ygoCatalogPage;
		private System.Windows.Forms.OpenFileDialog imageFileDialog;
		private System.Windows.Forms.TabPage ygoSetPage;
		private System.Windows.Forms.TabPage ygoDetailPage;
		private System.Windows.Forms.TableLayoutPanel ygoLocationTable;
		private System.Windows.Forms.PictureBox ygoDetailImgbox;
		private System.Windows.Forms.Label ygoMoveLabel;
		private System.Windows.Forms.GroupBox ygoDetailBox;
		private System.Windows.Forms.Button ygoEditPrintButton;
		private System.Windows.Forms.Button ygoEditCardButton;
		private System.Windows.Forms.TabControl formTabControl;
		private System.Windows.Forms.TabPage ygoPage;
		private System.Windows.Forms.TabPage mtgPage;
		private System.Windows.Forms.TabControl mtgTabControl;
		private System.Windows.Forms.TabPage mtgSetPage;
		private System.Windows.Forms.FlowLayoutPanel mtgSetLayout;
		private System.Windows.Forms.TabPage mtgCatalogPage;
		private System.Windows.Forms.FlowLayoutPanel mtgCatalogLayout;
		private System.Windows.Forms.TabPage mtgDetailPage;
		private System.Windows.Forms.GroupBox mtgDetailBox;
		private System.Windows.Forms.Button mtgEditPrintButton;
		private System.Windows.Forms.Button mtgEditCardButton;
		private System.Windows.Forms.Label mtgMoveLabel;
		private System.Windows.Forms.TableLayoutPanel mtgLocationTable;
		private System.Windows.Forms.ComboBox mtgMoveField;
		private System.Windows.Forms.PictureBox mtgDetailImgbox;
		private System.Windows.Forms.TabPage mtgCardPage;
		private System.Windows.Forms.Label mtgCardDialog;
		private System.Windows.Forms.NumericUpDown mtgToughnessField;
		private System.Windows.Forms.NumericUpDown mtgPowerField;
		private System.Windows.Forms.Label mtgAtkDefLabel;
		private System.Windows.Forms.Button mtgAddCardButton;
		private System.Windows.Forms.Label mtgNameLabel;
		private System.Windows.Forms.TextBox mtgNameField;
		private System.Windows.Forms.Label mtgOracleTextLabel;
		private System.Windows.Forms.Label mtgIdentityLabel;
		private System.Windows.Forms.TextBox mtgOracleTextField;
		private System.Windows.Forms.Label mtgColourLabel;
		private System.Windows.Forms.Label mtgCardTypeLabel;
		private System.Windows.Forms.TabPage mtgPrintPage;
		private System.Windows.Forms.Label mtgIODialog;
		private System.Windows.Forms.Button mtgSaveButton;
		private System.Windows.Forms.Label mtgImgpathLabel;
		private System.Windows.Forms.Button mtgSubTreatmentButton;
		private System.Windows.Forms.Button mtgAddTreatmentButton;
		private System.Windows.Forms.PictureBox mtgPrintImgbox;
		private System.Windows.Forms.Button mtgAddPrintButton;
		private System.Windows.Forms.ComboBox mtgCardrefField;
		private System.Windows.Forms.Label mtgCardrefLabel;
		private System.Windows.Forms.Button mtgImgsearchButton;
		private System.Windows.Forms.Label mtgImgsearchLabel;
		private System.Windows.Forms.TextBox mtgFlavorTextField;
		private System.Windows.Forms.Label mtgFlavorTextLabel;
		private System.Windows.Forms.Label mtgTreatmentsValue;
		private System.Windows.Forms.ComboBox mtgTreatmentField;
		private System.Windows.Forms.Label mtgTreatmentLabel;
		private System.Windows.Forms.NumericUpDown mtgNumberField;
		private System.Windows.Forms.Label mtgNumberLabel;
		private System.Windows.Forms.ComboBox mtgSetField;
		private System.Windows.Forms.Label mtgSetLabel;
		private System.Windows.Forms.CheckBox mtgIdentityG;
		private System.Windows.Forms.CheckBox mtgIdentityR;
		private System.Windows.Forms.CheckBox mtgIdentityB;
		private System.Windows.Forms.CheckBox mtgIdentityU;
		private System.Windows.Forms.CheckBox mtgIdentityW;
		private System.Windows.Forms.PictureBox mtgColourImgG;
		private System.Windows.Forms.PictureBox mtgColourImgR;
		private System.Windows.Forms.PictureBox mtgColourImgB;
		private System.Windows.Forms.PictureBox mtgColourImgU;
		private System.Windows.Forms.PictureBox mtgColourImgW;
		private System.Windows.Forms.CheckBox mtgColourG;
		private System.Windows.Forms.CheckBox mtgColourR;
		private System.Windows.Forms.CheckBox mtgColourB;
		private System.Windows.Forms.CheckBox mtgColourU;
		private System.Windows.Forms.CheckBox mtgColourW;
		private System.Windows.Forms.PictureBox mtgIdentityImgG;
		private System.Windows.Forms.PictureBox mtgIdentityImgR;
		private System.Windows.Forms.PictureBox mtgIdentityImgB;
		private System.Windows.Forms.PictureBox mtgIdentityImgU;
		private System.Windows.Forms.PictureBox mtgIdentityImgW;
		private System.Windows.Forms.TextBox mtgCostField;
		private System.Windows.Forms.Label mtgCostLabel;
		private System.Windows.Forms.Button mtgImgsearchBackButton;
		private System.Windows.Forms.Label mtgImgsearchBackLabel;
		private System.Windows.Forms.PictureBox mtgPrintImgboxBack;
		private System.Windows.Forms.Label mtgImgpathBackLabel;
		private System.Windows.Forms.Label mtgScryfallLabel;
		private System.Windows.Forms.TextBox mtgScryfallField;
		private System.Windows.Forms.ComboBox mtgRarityField;
		private System.Windows.Forms.Label mtgRarityLabel;
		private System.Windows.Forms.NumericUpDown mtgToughnessBackField;
		private System.Windows.Forms.NumericUpDown mtgPowerBackField;
		private System.Windows.Forms.Label mtgAtkDefBackLabel;
		private System.Windows.Forms.TextBox mtgCardTypeField;
		private System.Windows.Forms.Button mtgCatalogNextButton;
		private System.Windows.Forms.Button mtgCatalogPrevButton;
		private System.Windows.Forms.Label mtgCatalogIndex;
		private System.Windows.Forms.TabPage mtgSymbolsPage;
		private System.Windows.Forms.FlowLayoutPanel mtgSymbolLayout;
		private System.Windows.Forms.Label mtgSymbolLabel;
		private System.Windows.Forms.GroupBox mtgSymbolHeaderBox;
		private System.Windows.Forms.Button mtgAddSymbolButton;
		private System.Windows.Forms.Button mtgSaveSymbolsButton;
		private System.Windows.Forms.TabPage mtgSetsPage;
		private System.Windows.Forms.FlowLayoutPanel mtgSetGeneratorLayout;
		private System.Windows.Forms.Button mtgSaveSetsButton;
		private System.Windows.Forms.Button mtgAddSetButton;
		private System.Windows.Forms.Label mtgSetGeneratorLabel;
		private System.Windows.Forms.Button mtgReloadLocationsButton;
		private System.Windows.Forms.GroupBox mtgTooltipBox;
		private System.Windows.Forms.Button mtgReloadSetsButton;
		private System.Windows.Forms.Label mtgIgnoreDuplicateEntryLabel;
		private System.Windows.Forms.CheckBox mtgIgnoreDuplicateEntryBox;
		private System.Windows.Forms.GroupBox mtgCardtipBox;
		private System.Windows.Forms.PictureBox mtgCardtipImage;
		private System.Windows.Forms.Button mtgDetailNextButton;
		private System.Windows.Forms.Button mtgDetailPrevButton;
		private System.Windows.Forms.Label mtgCardrefDescriptor;
		private System.Windows.Forms.Button mtgSetGeneratorPrev;
		private System.Windows.Forms.Button mtgSetGeneratorNext;
		private System.Windows.Forms.Label mtgSetGeneratorPageLabel;
		private System.Windows.Forms.Button mtgPrevSetButton;
		private System.Windows.Forms.Label mtgSetPageLabel;
		private System.Windows.Forms.Button mtgNextSetButton;
		private System.Windows.Forms.Label mtgSetlistLabel;
		private System.Windows.Forms.Button mtgPrintAutofillButton;
		private System.Windows.Forms.Button mtgPrintAutofillRangeButton;
		private System.Windows.Forms.NumericUpDown mtgPrintAutoLimit;
		private System.Windows.Forms.Button mtgDeletePrintingButton;
		private System.Windows.Forms.TabPage mtgSearchPage;
		private System.Windows.Forms.Button mtgSearchButton;
		private System.Windows.Forms.TextBox mtgSearchOracleField;
		private System.Windows.Forms.TextBox mtgSearchTypeField;
		private System.Windows.Forms.PictureBox mtgSearchColImgG;
		private System.Windows.Forms.PictureBox mtgSearchColImgR;
		private System.Windows.Forms.PictureBox mtgSearchColImgB;
		private System.Windows.Forms.PictureBox mtgSearchColImgU;
		private System.Windows.Forms.PictureBox mtgSearchColImgW;
		private System.Windows.Forms.CheckBox mtgSearchColG;
		private System.Windows.Forms.CheckBox mtgSearchColR;
		private System.Windows.Forms.CheckBox mtgSearchColB;
		private System.Windows.Forms.CheckBox mtgSearchColU;
		private System.Windows.Forms.CheckBox mtgSearchColW;
		private System.Windows.Forms.PictureBox mtgSearchIDImgG;
		private System.Windows.Forms.PictureBox mtgSearchIDImgR;
		private System.Windows.Forms.PictureBox mtgSearchIDImgB;
		private System.Windows.Forms.PictureBox mtgSearchIDImgU;
		private System.Windows.Forms.PictureBox mtgSearchIDImgW;
		private System.Windows.Forms.CheckBox mtgSearchIDG;
		private System.Windows.Forms.CheckBox mtgSearchIDR;
		private System.Windows.Forms.CheckBox mtgSearchIDB;
		private System.Windows.Forms.CheckBox mtgSearchIDU;
		private System.Windows.Forms.CheckBox mtgSearchIDW;
		private System.Windows.Forms.Label mtgSearchNameLabel;
		private System.Windows.Forms.TextBox mtgSearchNameField;
		private System.Windows.Forms.Label mtgSearchOracleLabel;
		private System.Windows.Forms.Label mtgSearchIdentityLabel;
		private System.Windows.Forms.Label mtgSearchColourLabel;
		private System.Windows.Forms.Label mtgSearchTypeLabel;
		private System.Windows.Forms.Button mtgDetailAutogenButton;
		private System.Windows.Forms.Label mtgDetailDialog;
		private System.Windows.Forms.RadioButton mtgSortNumericButton;
		private System.Windows.Forms.RadioButton mtgSortAlphabeticalButton;
		private System.Windows.Forms.ComboBox mtgSearchLocationField;
		private System.Windows.Forms.Label mtgSearchLocationLabel;
		private System.Windows.Forms.ComboBox mtgSearchSetField;
		private System.Windows.Forms.Label mtgSearchSetLabel;
		private System.Windows.Forms.CheckBox mtgPrintTokenCheck;
		private System.Windows.Forms.TabPage pkmnPage;
		private System.Windows.Forms.TabControl pkmnTabControl;
		private System.Windows.Forms.TabPage pkmnSetlistPage;
		private System.Windows.Forms.Button pkmnPrevSetButton;
		private System.Windows.Forms.Label pkmnSetPageLabel;
		private System.Windows.Forms.Button pkmnNextSetButton;
		private System.Windows.Forms.Label pkmnSetlistLabel;
		private System.Windows.Forms.FlowLayoutPanel pkmnSetlistLayout;
		private System.Windows.Forms.TabPage pkmnSearchPage;
		private System.Windows.Forms.ComboBox pkmnSearchSetField;
		private System.Windows.Forms.Label pkmnSearchSetLabel;
		private System.Windows.Forms.ComboBox pkmnSearchLocationField;
		private System.Windows.Forms.Label pkmnSearchLocationLabel;
		private System.Windows.Forms.Button pkmnSearchButton;
		private System.Windows.Forms.TextBox pkmnSearchOracleField;
		private System.Windows.Forms.TextBox pkmnSearchTypeField;
		private System.Windows.Forms.Label pkmnSearchNameLabel;
		private System.Windows.Forms.TextBox pkmnSearchNameField;
		private System.Windows.Forms.Label pkmnSearchOracleLabel;
		private System.Windows.Forms.Label pkmnSearchTypeLabel;
		private System.Windows.Forms.TabPage pkmnCatalogPage;
		private System.Windows.Forms.RadioButton pkmnSortAlphabeticalButton;
		private System.Windows.Forms.RadioButton pkmnSortNumericButton;
		private System.Windows.Forms.Button pkmnCatalogNextButton;
		private System.Windows.Forms.Button pkmnCatalogPrevButton;
		private System.Windows.Forms.FlowLayoutPanel pkmnCatalogLayout;
		private System.Windows.Forms.Label pkmnCatalogIndex;
		private System.Windows.Forms.TabPage pkmnDetailPage;
		private System.Windows.Forms.Label pkmnDetailDialog;
		private System.Windows.Forms.Button pkmnDetailAutogenButton;
		private System.Windows.Forms.Button pkmnDeletePrintingButton;
		private System.Windows.Forms.Button pkmnDetailNextButton;
		private System.Windows.Forms.Button pkmnDetailPrevButton;
		private System.Windows.Forms.Panel pkmnCardtipPanel;
		private System.Windows.Forms.PictureBox pkmnCardtipImage;
		private System.Windows.Forms.Panel pkmnTooltipPanel;
		private System.Windows.Forms.Panel pkmnDetailPanel;
		private System.Windows.Forms.Button pkmnReloadLocationsButton;
		private System.Windows.Forms.Button pkmnEditPrintButton;
		private System.Windows.Forms.Button pkmnEditCardButton;
		private System.Windows.Forms.Label pkmnMoveLabel;
		private System.Windows.Forms.TableLayoutPanel pkmnLocationTable;
		private System.Windows.Forms.ComboBox pkmnMoveField;
		private System.Windows.Forms.PictureBox pkmnDetailImgbox;
		private System.Windows.Forms.TabPage pkmnCardPage;
		private System.Windows.Forms.Label pkmnIgnorDuplicateEntryLabel;
		private System.Windows.Forms.CheckBox pkmnIgnorDuplicateEntryBox;
		private System.Windows.Forms.TextBox pkmnTypeField;
		private System.Windows.Forms.Label pkmnResistLabel;
		private System.Windows.Forms.TextBox pkmnETypeField;
		private System.Windows.Forms.Label pkmnETypeLabel;
		private System.Windows.Forms.Label pkmnCardDialog;
		private System.Windows.Forms.NumericUpDown pkmnHPField;
		private System.Windows.Forms.Label pkmnHPLabel;
		private System.Windows.Forms.Button pkmnAddCardButton;
		private System.Windows.Forms.Label pkmnNameLabel;
		private System.Windows.Forms.TextBox pkmnNameField;
		private System.Windows.Forms.Label pkmnOracleLabel;
		private System.Windows.Forms.TextBox pkmnOracleField;
		private System.Windows.Forms.Label pkmnTypeLabel;
		private System.Windows.Forms.TabPage pkmnPrintPage;
		private System.Windows.Forms.NumericUpDown pkmnPrintAutoLimit;
		private System.Windows.Forms.Button pkmnPrintAutofillRangeButton;
		private System.Windows.Forms.Button pkmnPrintAutofillButton;
		private System.Windows.Forms.Label pkmnCardrefDescriptor;
		private System.Windows.Forms.Label pkmnPrintIDLabel;
		private System.Windows.Forms.TextBox pkmnPrintIDField;
		private System.Windows.Forms.Label pkmnImgpathBackLabel;
		private System.Windows.Forms.PictureBox pkmnPrintImgboxBack;
		private System.Windows.Forms.Button pkmnImgsearchBackButton;
		private System.Windows.Forms.Label pkmnImgBackLabel;
		private System.Windows.Forms.Label pkmnIODialog;
		private System.Windows.Forms.Button pkmnSaveButton;
		private System.Windows.Forms.Label pkmnImgpathLabel;
		private System.Windows.Forms.Button pkmnRemTreatmentButton;
		private System.Windows.Forms.Button pkmnAddTreatmentButton;
		private System.Windows.Forms.PictureBox pkmnPrintImgbox;
		private System.Windows.Forms.Button pkmnAddPrintButton;
		private System.Windows.Forms.ComboBox pkmnCardrefField;
		private System.Windows.Forms.Label pkmnCardrefLabel;
		private System.Windows.Forms.Button pkmnImgsearchButton;
		private System.Windows.Forms.Label pkmnImgLabel;
		private System.Windows.Forms.TextBox pkmnFlavorField;
		private System.Windows.Forms.Label pkmnFlavorLabel;
		private System.Windows.Forms.Label pkmnTreatmentsValue;
		private System.Windows.Forms.ComboBox pkmnTreatmentField;
		private System.Windows.Forms.Label pkmnTreatmentLabel;
		private System.Windows.Forms.NumericUpDown pkmnNumberField;
		private System.Windows.Forms.Label pkmnNumberLabel;
		private System.Windows.Forms.ComboBox pkmnSetField;
		private System.Windows.Forms.Label pkmnSetLabel;
		private System.Windows.Forms.TabPage pkmnSymbolPage;
		private System.Windows.Forms.FlowLayoutPanel pkmnSymbolLayout;
		private System.Windows.Forms.Panel pkmnSymbolHeaderPanel;
		private System.Windows.Forms.Button pkmnSaveSymbolsButton;
		private System.Windows.Forms.Button pkmnAddSymbolButton;
		private System.Windows.Forms.Label pkmnSymbolLabel;
		private System.Windows.Forms.TabPage pkmnSetPage;
		private System.Windows.Forms.Button pkmnReloadSetsButton;
		private System.Windows.Forms.Button pkmnSetGeneratorPrev;
		private System.Windows.Forms.Button pkmnAddSetButton;
		private System.Windows.Forms.Button pkmnSaveSetsButton;
		private System.Windows.Forms.Button pkmnSetGeneratorNext;
		private System.Windows.Forms.Label pkmnSetGeneratorPageLabel;
		private System.Windows.Forms.FlowLayoutPanel pkmnSetGeneratorLayout;
		private System.Windows.Forms.Label pkmnSetGeneratorLabel;
		private System.Windows.Forms.Label pkmnStageLabel;
		private System.Windows.Forms.TextBox pkmnStageField;
		private System.Windows.Forms.Label pkmnWeakLabel;
		private System.Windows.Forms.TextBox pkmnWeakField;
		private System.Windows.Forms.Label pkmnRetreatLabel;
		private System.Windows.Forms.TextBox pkmnRetreatField;
		private System.Windows.Forms.TextBox pkmnResistField;
		private System.Windows.Forms.Button pkmnImportButton;
		private System.Windows.Forms.TextBox pkmnImportField;
		private System.Windows.Forms.Button pkmnPrintHoloButton;
		private System.Windows.Forms.Button pkmnPrintNormalButton;
		private System.Windows.Forms.Label ygoCardTypeLabel;
		private System.Windows.Forms.NumericUpDown ygoScaleField;
		private System.Windows.Forms.NumericUpDown ygoLevelField;
		private System.Windows.Forms.NumericUpDown ygoDefField;
		private System.Windows.Forms.NumericUpDown ygoAtkField;
		private System.Windows.Forms.Label ygoCardDialog;
		private System.Windows.Forms.Label ygoIgnoreDuplicateEntryLabel;
		private System.Windows.Forms.CheckBox ygoIgnoreDuplicateEntryBox;
		private System.Windows.Forms.Button ygoAddCardButton;
		private System.Windows.Forms.Label ygoScaleLabel;
		private System.Windows.Forms.Label ygoAtkDefLabel;
		private System.Windows.Forms.Label ygoLevelLabel;
		private System.Windows.Forms.TextBox ygoOracleField;
		private System.Windows.Forms.TextBox ygoCardTypeField;
		private System.Windows.Forms.TextBox ygoAttributeField;
		private System.Windows.Forms.TextBox ygoPropertyField;
		private System.Windows.Forms.TextBox ygoTypesField;
		private System.Windows.Forms.TextBox ygoNameField;
		private System.Windows.Forms.Label ygoOracleLabel;
		private System.Windows.Forms.Label ygoTypesLabel;
		private System.Windows.Forms.Label ygoPropertyLabel;
		private System.Windows.Forms.Label ygoAttributeLabel;
		private System.Windows.Forms.NumericUpDown ygoPrintAutoLimit;
		private System.Windows.Forms.Button ygoPrintAutofillRangeButton;
		private System.Windows.Forms.Button ygoPrintAutofillButton;
		private System.Windows.Forms.Label ygoCardrefDescriptor;
		private System.Windows.Forms.Label ygoPrintIDLabel;
		private System.Windows.Forms.TextBox ygoPrintIDField;
		private System.Windows.Forms.PictureBox ygoPrintImgboxBack;
		private System.Windows.Forms.Button ygoImgsearchBackButton;
		private System.Windows.Forms.Label ygoImgBackLabel;
		private System.Windows.Forms.Label ygoIODialog;
		private System.Windows.Forms.Button ygoSaveButton;
		private System.Windows.Forms.Label ygoImgpathLabel;
		private System.Windows.Forms.Button ygoRemoveRarityButton;
		private System.Windows.Forms.Button ygoAddRarityButton;
		private System.Windows.Forms.PictureBox ygoPrintImgbox;
		private System.Windows.Forms.Button ygoAddPrintButton;
		private System.Windows.Forms.ComboBox ygoCardrefField;
		private System.Windows.Forms.Button ygoImgsearchButton;
		private System.Windows.Forms.Label ygoImageLabel;
		private System.Windows.Forms.TextBox ygoFlavorField;
		private System.Windows.Forms.Label ygoFlavorLabel;
		private System.Windows.Forms.Label ygoRaritiesValue;
		private System.Windows.Forms.ComboBox ygoRaritiesField;
		private System.Windows.Forms.Label ygoRaritiesLabel;
		private System.Windows.Forms.NumericUpDown ygoNumberField;
		private System.Windows.Forms.Label ygoNumberLabel;
		private System.Windows.Forms.ComboBox ygoSetField;
		private System.Windows.Forms.Label ygoSetLabel;
		private System.Windows.Forms.TabPage ygoSetsPage;
		private System.Windows.Forms.Label ygoSetGeneratorPageLabel;
		private System.Windows.Forms.Button ygoSetGeneratorNext;
		private System.Windows.Forms.Button ygoSetGeneratorPrev;
		private System.Windows.Forms.Button ygoSaveSetsButton;
		private System.Windows.Forms.Button ygoAddSetButton;
		private System.Windows.Forms.Button ygoReloadSetsButton;
		private System.Windows.Forms.FlowLayoutPanel ygoSetGeneratorLayout;
		private System.Windows.Forms.Label ygoSetGeneratorLabel;
		private System.Windows.Forms.ComboBox pkmnRarityField;
		private System.Windows.Forms.Label pkmnRarityLabel;
		private System.Windows.Forms.Label ygoCardrefLabel;
		private System.Windows.Forms.Label ygoSetPageLabel;
		private System.Windows.Forms.Button ygoNextSetButton;
		private System.Windows.Forms.Button ygoPrevSetButton;
		private System.Windows.Forms.Label ygoSetlistLabel;
		private System.Windows.Forms.FlowLayoutPanel ygoSetlistLayout;
		private System.Windows.Forms.TabPage ygoSearchPage;
		private System.Windows.Forms.Label ygoSearchNameLabel;
		private System.Windows.Forms.Label ygoSearchCardTypeLabel;
		private System.Windows.Forms.Label ygoSearchTypesLabel;
		private System.Windows.Forms.Label ygoSearchPropertyLabel;
		private System.Windows.Forms.Label ygoSearchOracleLabel;
		private System.Windows.Forms.Label ygoSearchAttributeLabel;
		private System.Windows.Forms.ComboBox ygoSearchLocationField;
		private System.Windows.Forms.ComboBox ygoSearchSetField;
		private System.Windows.Forms.TextBox ygoSearchOracleField;
		private System.Windows.Forms.TextBox ygoSearchTypesField;
		private System.Windows.Forms.TextBox ygoSearchPropertyField;
		private System.Windows.Forms.TextBox ygoSearchAttributeField;
		private System.Windows.Forms.TextBox ygoSearchCardTypeField;
		private System.Windows.Forms.TextBox ygoSearchNameField;
		private System.Windows.Forms.Button ygoSearchButton;
		private System.Windows.Forms.Label ygoSearchSetLabel;
		private System.Windows.Forms.Label ygoSearchLocationLabel;
		private System.Windows.Forms.GroupBox ygoCardtipBox;
		private System.Windows.Forms.PictureBox ygoCardtipImage;
		private System.Windows.Forms.GroupBox ygoTooltipBox;
		private System.Windows.Forms.Button ygoDetailNextButton;
		private System.Windows.Forms.Button ygoDetailAutogenButton;
		private System.Windows.Forms.Button ygoDetailPrevButton;
		private System.Windows.Forms.Button ygoReloadLocationsButton;
		private System.Windows.Forms.ComboBox ygoMoveField;
		private System.Windows.Forms.RadioButton ygoSortAlphabeticalButton;
		private System.Windows.Forms.RadioButton ygoSortNumericButton;
		private System.Windows.Forms.Button ygoCatalogNextButton;
		private System.Windows.Forms.Button ygoCatalogPrevButton;
		private System.Windows.Forms.FlowLayoutPanel ygoCatalogLayout;
		private System.Windows.Forms.Label ygoCatalogIndex;
		private System.Windows.Forms.Label ygoImgpathBackLabel;
		private System.Windows.Forms.Button ygoDeletePrintingButton;
		private System.Windows.Forms.Label ygoDetailDialog;
		private System.Windows.Forms.Button ygoImportButton;
		private System.Windows.Forms.TextBox ygoImportField;
		private System.Windows.Forms.GroupBox ygoPrintingsBox;
		private System.Windows.Forms.GroupBox mtgPrintingsBox;
		private System.Windows.Forms.Panel pkmnPrintingsPanel;
		private System.Windows.Forms.Button pkmnTrainerButton;
		private System.Windows.Forms.Button pkmnClipboardexButton;
		private System.Windows.Forms.Button pkmnClipboardPrismButton;
		private System.Windows.Forms.Button pkmnClipboardDeltaButton;
		private System.Windows.Forms.Button pkmnClipboardStarButton;
		private System.Windows.Forms.TextBox pkmnSearchField;
		private System.Windows.Forms.Label pkmnSearchLabel;
		private System.Windows.Forms.ComboBox pkmnSearchTypeList;
		private System.Windows.Forms.Button pkmnReloadSearchListsButton;
		private System.Windows.Forms.Button pkmnApplySearchTermsButton;
		private System.Windows.Forms.Label pkmnSearchDialog;
		private System.Windows.Forms.Label pkmnDetailFilterCountLabel;
		private System.Windows.Forms.Label ygoDetailFilterCountLabel;
		private System.Windows.Forms.Label mtgDetailFilterCountLabel;
		private System.Windows.Forms.Button pkmnPokemonButton;
		private System.Windows.Forms.Button pkmnClipboardSPButton;
		private System.Windows.Forms.Button pkmnClipboardLVXButton;
		private System.Windows.Forms.Label pkmnOwnedPrintingsLabel;
	}
}

