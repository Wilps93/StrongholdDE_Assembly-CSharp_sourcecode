using Noesis;

namespace Stronghold1DE;

public class HUD_Buildings : UserControl
{
	public Button RefRecruitArcherButton;

	public Button RefRecruitSpearmanButton;

	public Button RefRecruitMacemanButton;

	public Button RefRecruitXBowmanButton;

	public Button RefRecruitPikemanButton;

	public Button RefRecruitSwordsmanButton;

	public Button RefRecruitKnightButton;

	public Button RefRecruitEngineerButton;

	public Button RefRecruitEngineerButtonX;

	public Button RefRecruitLaddermanButton;

	public Button RefRecruitLaddermanButtonX;

	public Button RefRecruitTunellerButton;

	public Button RefButtonReleaseDogs;

	public Grid RefHUDBuildingFullClickMask;

	public Grid RefBuildingPanel;

	public Grid RefBarracksPanel;

	public Grid RefWorkerPanel;

	public Grid RefKeepPanel;

	public Grid RefGranaryPanel;

	public Grid RefArmouryPanel;

	public Grid RefStockpilePanel;

	public Grid RefInnPanel;

	public Grid RefFletchersPanel;

	public Grid RefPoleturnersPanel;

	public Grid RefBlacksmithsPanel;

	public Grid RefChurchPanel;

	public Grid RefTradepostPanel;

	public Grid RefTradingFoodPanel;

	public Grid RefTradingResourcesPanel;

	public Grid RefTradingWeaponsPanel;

	public Grid RefTradingPricesPanel;

	public Grid RefTradingTradePanel;

	public Button RefTradeBuyButton;

	public Button RefTradeSellButton;

	public Storyboard RefTradeErrorAnination;

	public Grid RefTradePost_Trade_Normal;

	public Grid RefTradePost_Trade_Auto;

	public Slider RefAutotrade_Sell_Slider;

	public Slider RefAutotrade_Buy_Slider;

	public Button RefTrade_AutoToggle;

	public Button RefTrade_GoTo_Auto;

	public Grid RefReportsPanel;

	public Grid RefReportsPopularityPanel;

	public Grid RefReportsPopEventsPanel;

	public Grid RefShowEventsButton;

	public Grid RefReportsFearFactorPanel;

	public Grid RefReportsPopulationPanel;

	public Grid RefReportsArmy1Panel;

	public Grid RefReportsArmy2Panel;

	public Grid RefReportsStoresPanel;

	public Grid RefReportsWeaponsPanel;

	public Grid RefReportsReligionPanel;

	public TextBlock RefWGT_PopReportAleText;

	public Grid RefChimpPanel;

	public Grid RefReportsFoodPanel;

	public Button RefHelpButton;

	public Grid RefShowWorkersPanel;

	public Grid RefShowInfoPanel;

	public Grid RefShowRepairPanel;

	public Grid RefShowGatePanel;

	public Grid RefShowDogsPanel;

	public Grid RefShowDrawbridgePanel;

	public Grid RefShowEngineersGuildPanel;

	public Grid RefShowTunellersGuildPanel;

	public Button RefButtonRepair;

	public TextBlock RefTroopCostsText;

	public TextBlock RefTroopNameText;

	public TextBlock RefTroopHelpText;

	public TextBlock RefEngineersCostsText;

	public TextBlock RefEngineersHelpText;

	public TextBlock RefTunellersCostsText;

	public TextBlock RefTunellersHelpText;

	public TextBlock RefDogsReleasedText;

	public TextBlock RefEngineersGuildNoGoldMessage;

	public TextBlock RefTunnllersGuildNoGoldMessage;

	public WGT_Popularity RefWGTFoodTypePop;

	public WGT_Popularity RefWGTRationsPop;

	public WGT_Popularity RefWGTInnPop;

	public WGT_Popularity RefWGTTaxPop;

	public WGT_Popularity RefWGTPopReportFoodPop;

	public WGT_Popularity RefWGTPopReportTaxPop;

	public WGT_Popularity RefWGTPopReportCrowdingPop;

	public WGT_Popularity RefWGTPopReportFearFactorPop;

	public WGT_Popularity RefWGTPopReportReligionPop;

	public WGT_Popularity RefWGTPopReportAlePop;

	public WGT_Popularity RefWGTPopReportEventsPop;

	public WGT_Popularity RefWGTPopReportTotalPop;

	public WGT_Popularity RefWGTPopReportTotal2Pop;

	public WGT_Popularity RefWGTPopReportFairsPop;

	public WGT_Popularity RefWGTPopReportMarriagePop;

	public WGT_Popularity RefWGTPopReportJesterPop;

	public WGT_Popularity RefWGTPopReportPlaguePop;

	public WGT_Popularity RefWGTPopReportWolvesPop;

	public WGT_Popularity RefWGTPopReportBanditsPop;

	public WGT_Popularity RefWGTPopReportFirePop;

	public WGT_Popularity RefWGTFFReportFearFactorPop;

	public WGT_Popularity RefWGTRelReport;

	public WGT_Popularity RefWGTRelReport2;

	public Image RefRationHandNone;

	public Image RefRationHandHalf;

	public Image RefRationHandFull;

	public Image RefRationHandExtra;

	public Image RefRationHandDouble;

	public Image RefStopMeatConsumption;

	public Image RefStopCheeseConsumption;

	public Image RefStopBreadConsumption;

	public Image RefStopApplesConsumption;

	public Button RefButtonOpenGate;

	public Button RefButtonCloseGate;

	public Button RefButtonOpenBridge;

	public Button RefButtonCloseBridge;

	public RadioButton RefProducingBows;

	public RadioButton RefProducingXBows;

	public RadioButton RefProducingSpears;

	public RadioButton RefProducingPikes;

	public RadioButton RefProducingSwords;

	public RadioButton RefProducingMaces;

	public TextBlock RefRelReportPopEffectLabel;

	public TextBlock RefWGT_RelReportLabel;

	public Button RefBuildingZZZButtonOn;

	public Button RefBuildingZZZButtonOff;

	public Button RefButtonArmyReportBack;

	public Button RefButtonArmyReportBack2;

	public TextBlock RefAutotrade_Buy_Text;

	public TextBlock RefAutotrade_Sell_Text;

	public TextBlock RefTradeErrorMessage;

	private bool currentAutoTradeOn;

	private bool sliderSetup;

	private bool insideValueChanged;

	private TranslateTransform[] SelTroopPositions = new TranslateTransform[8]
	{
		new TranslateTransform(-181f, 0f),
		new TranslateTransform(-130f, 0f),
		new TranslateTransform(-78f, 0f),
		new TranslateTransform(-27f, 0f),
		new TranslateTransform(25f, 0f),
		new TranslateTransform(76f, 0f),
		new TranslateTransform(128f, 0f),
		new TranslateTransform(180f, 0f)
	};

	private int[] sketchList = new int[110]
	{
		0, 176, 176, 177, 178, 179, 180, 181, 0, 0,
		0, 0, 182, 183, 184, 185, 186, 187, 188, 0,
		189, 189, 190, 191, 0, 0, 0, 192, 193, 0,
		194, 195, 196, 197, 198, 199, 200, 200, 200, 0,
		201, 201, 201, 201, 201, 203, 203, 203, 203, 203,
		0, 0, 0, 0, 0, 202, 0, 0, 0, 0,
		0, 203, 204, 205, 0, 206, 207, 208, 0, 0,
		0, 201, 201, 201, 203, 203, 203, 203, 203, 203,
		0, 0, 0, 0, 0, 0, 203, 203, 203, 203,
		0, 209, 210, 211, 212, 213, 0, 214, 215, 216,
		217, 245, 0, 218, 219, 0, 0, 0, 0, 0
	};

	private string[] BuildingTitles = new string[110]
	{
		"", "TEXT_IN_HOUSE_001", "TEXT_IN_HOUSE_001", "TEXT_IN_WOODCUTTERS_HUT_001", "TEXT_IN_OXEN_BASE_001", "TEXT_IN_IRON_MINE_001", "TEXT_IN_PITCH_DIGGER_001", "TEXT_IN_HUNTERS_HUT_001", "TEXT_IN_BARRACKS_001", "TEXT_IN_BARRACKS_001",
		"TEXT_IN_GOODS_YARD_001", "TEXT_IN_ARMOURY_001", "TEXT_IN_FLETCHERS_WORKSHOP_001", "TEXT_IN_BLACKSMITHS_WORKSHOP_001", "TEXT_IN_POLETURNERS_WORKSHOP_001", "TEXT_IN_ARMOURERS_WORKSHOP_001", "TEXT_IN_TANNERS_WORKSHOP_001", "TEXT_IN_BAKERS_WORKSHOP_001", "TEXT_IN_BREWERS_WORKSHOP_001", "TEXT_IN_GRANARY_001",
		"TEXT_IN_QUARRY_001", "TEXT_IN_QUARRYPILE_001", "TEXT_IN_INN_001", "TEXT_IN_HEALERS_001", "TEXT_IN_ENGINEERS_GUILD_001", "TEXT_IN_TUNNELLERS_GUILD_001", "TEXT_IN_TRADEPOST_001", "TEXT_IN_WELL_001", "TEXT_IN_OIL_SMELTER_001", "TEXT_IN_SIEGE_TENT_001",
		"TEXT_IN_WHEATFARM_001", "TEXT_IN_HOPSFARM_001", "TEXT_IN_APPLEFARM_001", "TEXT_IN_CATTLEFARM_001", "TEXT_IN_MILL_001", "TEXT_IN_STABLES_001", "TEXT_IN_CHURCH_001", "TEXT_IN_CHURCH_004", "TEXT_IN_CHURCH_005", "TEXT_BUBBLE_HELP_TEXT_232",
		"TEXT_IN_KEEP_001", "TEXT_IN_KEEP_002", "TEXT_IN_KEEP_003", "TEXT_IN_KEEP_004", "TEXT_IN_KEEP_005", "TEXT_IN_GATEHOUSE_001", "TEXT_IN_GATEHOUSE_001", "TEXT_IN_GATEHOUSE_001", "TEXT_IN_POSTERN_GATE_001", "TEXT_IN_DRAWBRIDGE_001",
		"TEXT_IN_TUNNEL_ENTERANCE_001", "TEXT_IN_TRAINING_GROUND_001", "TEXT_IN_SIGNPOST_001", "TEXT_IN_TRAINING_GROUND_001", "", "TEXT_IN_CAMP_FIRE_001", "TEXT_IN_TRAINING_GROUND_001", "TEXT_IN_TRAINING_GROUND_001", "TEXT_IN_TRAINING_GROUND_001", "TEXT_IN_TRAINING_GROUND_001",
		"TEXT_IN_GATEHOUSE_001", "TEXT_IN_TOWER_001", "TEXT_IN_GALLOWS_001", "TEXT_IN_STOCKS_001", "TEXT_IN_WITCH_HOIST_001", "TEXT_IN_MAYPOLE_001", "TEXT_IN_GARDEN_001", "TEXT_IN_KILLING_PIT_001", "", "",
		"", "TEXT_IN_KEEP_001", "TEXT_IN_KEEP_001", "TEXT_IN_KEEP_001", "TEXT_IN_TOWER_001", "TEXT_IN_TOWER_001", "TEXT_IN_TOWER_001", "TEXT_IN_TOWER_001", "TEXT_IN_TOWER_001", "TEXT_IN_TOWER_001",
		"TEXT_IN_CATAPULT_001", "TEXT_IN_TREBUCHET_001", "", "", "", "TEXT_IN_TUNNEL_ENTERANCE_001", "TEXT_IN_TOWER_001", "TEXT_IN_TOWER_001", "TEXT_IN_TOWER_001", "TEXT_IN_TOWER_001",
		"", "TEXT_IN_CESS_PIT_001", "TEXT_IN_BURNING_STAKE_001", "TEXT_IN_GIBBET_001", "TEXT_IN_DUNGEON_001", "TEXT_IN_STRETCHING_RACK_001", "TEXT_IN_FLOGGING_RACK_001", "TEXT_IN_CHOPPING_BLOCK_001", "TEXT_IN_DUNKING_STOOL_001", "TEXT_IN_DOG_CAGE_001",
		"TEXT_IN_STATUE_001", "TEXT_IN_SHRINE_001", "TEXT_IN_BEEHIVE_001", "TEXT_IN_DANCING_BEAR_001", "TEXT_IN_POND_001", "TEXT_IN_BEAR_CAVE_001", "", "", "", ""
	};

	private int[] IsWorkerBuilding = new int[110]
	{
		0, 0, 0, 1, 1, 1, 1, 1, 0, 0,
		0, 0, 1, 1, 1, 1, 1, 1, 1, 0,
		1, 0, 1, 1, 0, 0, 0, 1, 0, 0,
		1, 1, 1, 1, 1, 0, 1, 1, 1, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0
	};

	private int[] ShowWorkersBuilding = new int[110]
	{
		0, 0, 0, 1, 1, 1, 1, 1, 0, 0,
		0, 0, 1, 1, 1, 1, 1, 1, 1, 0,
		1, 0, 1, 1, 0, 0, 0, 1, 0, 0,
		1, 1, 1, 1, 1, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0
	};

	public HUD_Buildings()
	{
		InitializeComponent();
		MainViewModel.Instance.HUDBuildingPanel = this;
		RefRecruitArcherButton = (Button)FindName("BarracksArcher");
		RefRecruitSpearmanButton = (Button)FindName("BarracksSpearman");
		RefRecruitMacemanButton = (Button)FindName("BarracksMaceman");
		RefRecruitXBowmanButton = (Button)FindName("BarracksXBowman");
		RefRecruitPikemanButton = (Button)FindName("BarracksPikeman");
		RefRecruitSwordsmanButton = (Button)FindName("BarracksSwordsman");
		RefRecruitKnightButton = (Button)FindName("BarracksKnight");
		RefRecruitEngineerButton = (Button)FindName("BarracksEngineer");
		RefRecruitEngineerButtonX = (Button)FindName("BarracksEngineerX");
		RefRecruitLaddermanButton = (Button)FindName("BarracksLadderman");
		RefRecruitLaddermanButtonX = (Button)FindName("BarracksLaddermanX");
		RefRecruitTunellerButton = (Button)FindName("BarracksTuneller");
		RefButtonReleaseDogs = (Button)FindName("ButtonReleaseDogs");
		RefHUDBuildingFullClickMask = (Grid)FindName("HUDBuildingFullClickMask");
		RefBuildingPanel = (Grid)FindName("BuildingPanel");
		RefBarracksPanel = (Grid)FindName("BarracksPanel");
		RefWorkerPanel = (Grid)FindName("WorkerPanel");
		RefKeepPanel = (Grid)FindName("KeepPanel");
		RefGranaryPanel = (Grid)FindName("GranaryPanel");
		RefArmouryPanel = (Grid)FindName("ArmouryPanel");
		RefStockpilePanel = (Grid)FindName("StockpilePanel");
		RefInnPanel = (Grid)FindName("InnPanel");
		RefFletchersPanel = (Grid)FindName("FletchersPanel");
		RefPoleturnersPanel = (Grid)FindName("PoleturnersPanel");
		RefBlacksmithsPanel = (Grid)FindName("BlacksmithsPanel");
		RefChurchPanel = (Grid)FindName("ChurchPanel");
		RefTradepostPanel = (Grid)FindName("TradepostPanel");
		RefTradingFoodPanel = (Grid)FindName("TradingFoodPanel");
		RefTradingResourcesPanel = (Grid)FindName("TradingResourcesPanel");
		RefTradingWeaponsPanel = (Grid)FindName("TradingWeaponsPanel");
		RefTradingPricesPanel = (Grid)FindName("TradingPricesPanel");
		RefTradingTradePanel = (Grid)FindName("TradingTradePanel");
		RefTradeBuyButton = (Button)FindName("TradeBuy");
		RefTradeSellButton = (Button)FindName("TradeSell");
		RefTradeErrorAnination = (Storyboard)TryFindResource("TradeErrorFadeOut");
		RefTradePost_Trade_Normal = (Grid)FindName("TradePost_Trade_Normal");
		RefTradePost_Trade_Auto = (Grid)FindName("TradePost_Trade_Auto");
		RefTradePost_Trade_Auto.Visibility = Visibility.Hidden;
		RefTrade_AutoToggle = (Button)FindName("Trade_AutoToggle");
		RefTrade_GoTo_Auto = (Button)FindName("Trade_GoTo_Auto");
		RefAutotrade_Sell_Slider = (Slider)FindName("Autotrade_Sell_Slider");
		RefAutotrade_Buy_Slider = (Slider)FindName("Autotrade_Buy_Slider");
		RefAutotrade_Sell_Slider.ValueChanged += Autotrade_Sell_Slider_ValueChanged;
		RefAutotrade_Buy_Slider.ValueChanged += Autotrade_Buy_Slider_ValueChanged;
		RefReportsPanel = (Grid)FindName("ReportsPanel");
		RefReportsPopularityPanel = (Grid)FindName("ReportsPopularityPanel");
		RefReportsPopEventsPanel = (Grid)FindName("ReportsPopEventsPanel");
		RefShowEventsButton = (Grid)FindName("ShowEventsPanelControl");
		RefReportsFearFactorPanel = (Grid)FindName("ReportsFearFactorPanel");
		RefReportsPopulationPanel = (Grid)FindName("ReportsPopulationPanel");
		RefReportsArmy1Panel = (Grid)FindName("ReportsArmy1Panel");
		RefReportsArmy2Panel = (Grid)FindName("ReportsArmy2Panel");
		RefReportsStoresPanel = (Grid)FindName("ReportsStoresPanel");
		RefReportsWeaponsPanel = (Grid)FindName("ReportsWeaponsPanel");
		RefReportsReligionPanel = (Grid)FindName("ReportsReligionPanel");
		RefWGT_PopReportAleText = (TextBlock)FindName("WGT_PopReportAleText");
		RefChimpPanel = (Grid)FindName("ChimpPanel");
		RefShowGatePanel = (Grid)FindName("ShowGate");
		RefShowDrawbridgePanel = (Grid)FindName("ShowDrawbridge");
		RefShowEngineersGuildPanel = (Grid)FindName("EngineersGuildPanel");
		RefShowTunellersGuildPanel = (Grid)FindName("TunellersGuildPanel");
		RefShowDogsPanel = (Grid)FindName("ShowDogs");
		RefReportsFoodPanel = (Grid)FindName("ReportsFoodPanel");
		RefShowWorkersPanel = (Grid)FindName("ShowWorkersPanel");
		RefShowInfoPanel = (Grid)FindName("ShowInfoPanel");
		RefShowRepairPanel = (Grid)FindName("ShowRepairPanel");
		RefHelpButton = (Button)FindName("BuildingHelpButton");
		RefButtonRepair = (Button)FindName("ButtonRepair");
		RefTroopCostsText = (TextBlock)FindName("TroopCostsText");
		RefTroopNameText = (TextBlock)FindName("TroopNameText");
		RefTroopHelpText = (TextBlock)FindName("TroopHelpText");
		RefEngineersCostsText = (TextBlock)FindName("EngineersCostsText");
		RefEngineersHelpText = (TextBlock)FindName("EngineersHelpText");
		RefTunellersCostsText = (TextBlock)FindName("TunellersCostsText");
		RefTunellersHelpText = (TextBlock)FindName("TunellersHelpText");
		RefDogsReleasedText = (TextBlock)FindName("DogsReleasedText");
		RefEngineersGuildNoGoldMessage = (TextBlock)FindName("EngineersGuildNoGoldMessage");
		RefTunnllersGuildNoGoldMessage = (TextBlock)FindName("TunnllersGuildNoGoldMessage");
		RefWGTFoodTypePop = (WGT_Popularity)FindName("WGT_FoodTypePop");
		RefWGTRationsPop = (WGT_Popularity)FindName("WGT_RationsPop");
		RefWGTInnPop = (WGT_Popularity)FindName("WGT_InnPop");
		RefWGTTaxPop = (WGT_Popularity)FindName("WGT_TaxPop");
		RefWGTPopReportFoodPop = (WGT_Popularity)FindName("WGT_PopReportFood");
		RefWGTPopReportTaxPop = (WGT_Popularity)FindName("WGT_PopReportTax");
		RefWGTPopReportCrowdingPop = (WGT_Popularity)FindName("WGT_PopReportCrowding");
		RefWGTPopReportFearFactorPop = (WGT_Popularity)FindName("WGT_PopReportFearFactor");
		RefWGTPopReportReligionPop = (WGT_Popularity)FindName("WGT_PopReportReligion");
		RefWGTPopReportAlePop = (WGT_Popularity)FindName("WGT_PopReportAle");
		RefWGTPopReportEventsPop = (WGT_Popularity)FindName("WGT_PopReportEvents");
		RefWGTPopReportTotalPop = (WGT_Popularity)FindName("WGT_PopReportTotal");
		RefWGTPopReportTotal2Pop = (WGT_Popularity)FindName("WGT_PopReportTotal2");
		RefWGTPopReportFairsPop = (WGT_Popularity)FindName("WGT_PopReportFairs");
		RefWGTPopReportMarriagePop = (WGT_Popularity)FindName("WGT_PopReportMarriage");
		RefWGTPopReportJesterPop = (WGT_Popularity)FindName("WGT_PopReportJester");
		RefWGTPopReportPlaguePop = (WGT_Popularity)FindName("WGT_PopReportPlague");
		RefWGTPopReportWolvesPop = (WGT_Popularity)FindName("WGT_PopReportWolves");
		RefWGTPopReportBanditsPop = (WGT_Popularity)FindName("WGT_PopReportBandits");
		RefWGTPopReportFirePop = (WGT_Popularity)FindName("WGT_PopReportFire");
		RefWGTFFReportFearFactorPop = (WGT_Popularity)FindName("WGT_FFReportFearFactor");
		RefWGTRelReport = (WGT_Popularity)FindName("WGT_RelReport");
		RefWGTRelReport2 = (WGT_Popularity)FindName("WGT_RelReport2");
		RefRationHandNone = (Image)FindName("RationHandNone");
		RefRationHandHalf = (Image)FindName("RationHandHalf");
		RefRationHandFull = (Image)FindName("RationHandFull");
		RefRationHandExtra = (Image)FindName("RationHandExtra");
		RefRationHandDouble = (Image)FindName("RationHandDouble");
		RefStopMeatConsumption = (Image)FindName("StopMeatConsumption");
		RefStopCheeseConsumption = (Image)FindName("StopCheeseConsumption");
		RefStopBreadConsumption = (Image)FindName("StopBreadConsumption");
		RefStopApplesConsumption = (Image)FindName("StopApplesConsumption");
		RefButtonOpenGate = (Button)FindName("ButtonOpenGate");
		RefButtonCloseGate = (Button)FindName("ButtonCloseGate");
		RefButtonOpenBridge = (Button)FindName("ButtonOpenBridge");
		RefButtonCloseBridge = (Button)FindName("ButtonCloseBridge");
		RefProducingBows = (RadioButton)FindName("ProducingBows");
		RefProducingXBows = (RadioButton)FindName("ProducingXBows");
		RefProducingSpears = (RadioButton)FindName("ProducingSpears");
		RefProducingPikes = (RadioButton)FindName("ProducingPikes");
		RefProducingSwords = (RadioButton)FindName("ProducingSwords");
		RefProducingMaces = (RadioButton)FindName("ProducingMaces");
		RefRelReportPopEffectLabel = (TextBlock)FindName("RelReportPopEffectLabel");
		RefWGT_RelReportLabel = (TextBlock)FindName("WGT_RelReportLabel");
		RefBuildingZZZButtonOn = (Button)FindName("BuildingZZZButtonOn");
		RefBuildingZZZButtonOff = (Button)FindName("BuildingZZZButtonOff");
		RefButtonArmyReportBack = (Button)FindName("ButtonArmyReportBack");
		RefButtonArmyReportBack2 = (Button)FindName("ButtonArmyReportBack2");
		MainViewModel.Instance.HUDmain.RefTutorialArrow3 = (Image)FindName("TutorialArrow3");
		MainViewModel.Instance.HUDmain.RefTutorialArrow4 = (Image)FindName("TutorialArrow4");
		MainViewModel.Instance.HUDmain.RefTutorialArrow6 = (Image)FindName("TutorialArrow6");
		RefAutotrade_Buy_Text = (TextBlock)FindName("Autotrade_Buy_Text");
		RefAutotrade_Sell_Text = (TextBlock)FindName("Autotrade_Sell_Text");
		RefTradeErrorMessage = (TextBlock)FindName("TradeErrorMessage");
		if (FatControler.thai)
		{
			RefWGT_PopReportAleText.FontSize = 14f;
		}
		if (FatControler.french || FatControler.czech)
		{
			RefAutotrade_Buy_Text.FontSize = 18f;
			RefAutotrade_Sell_Text.FontSize = 18f;
		}
		if (FatControler.polish)
		{
			RefAutotrade_Buy_Text.FontSize = 16f;
			RefAutotrade_Sell_Text.FontSize = 16f;
		}
		if (FatControler.italian)
		{
			RefTradeErrorMessage.FontSize = 16f;
			MainViewModel.Instance.BuySellFontSize = "20";
		}
		if (FatControler.portuguese)
		{
			MainViewModel.Instance.BuySellFontSize = "19";
		}
		if (FatControler.german)
		{
			MainViewModel.Instance.BuySellFontSize = "19";
			RefTradeBuyButton.Width = 226f;
			RefTradeSellButton.Width = 226f;
		}
		if (FatControler.turkish)
		{
			RefTradeErrorMessage.FontSize = 14f;
			MainViewModel.Instance.BuySellFontSize = "20";
		}
		if (FatControler.hungarian)
		{
			RefAutotrade_Buy_Text.FontSize = 18f;
			RefAutotrade_Sell_Text.FontSize = 18f;
			MainViewModel.Instance.BuySellFontSize = "20";
		}
		if (FatControler.ukrainian)
		{
			RefTradeErrorMessage.FontSize = 16f;
		}
	}

	public void initAutoTrade(int goods = -1)
	{
		sliderSetup = true;
		int num = GameData.Instance.lastGameState.trading_current_goods;
		if (goods != -1)
		{
			num = goods;
		}
		int num2 = GameData.Instance.lastGameState.autotrade_buy_amount[num];
		int num3 = GameData.Instance.lastGameState.autotrade_sell_amount[num];
		currentAutoTradeOn = GameData.Instance.lastGameState.autotrade_onoff[num] > 0;
		EngineInterface.GameAction(Enums.GameActionCommand.Autotrade_OnOff, num, GameData.Instance.lastGameState.autotrade_onoff[num]);
		EngineInterface.GameAction(Enums.GameActionCommand.Autotrade_SetBuy, -1, num2);
		EngineInterface.GameAction(Enums.GameActionCommand.Autotrade_SetSell, -1, num3);
		MainViewModel.Instance.TradeAutoBuy = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 143) + " < " + num2;
		MainViewModel.Instance.TradeAutoSell = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 144) + " > " + num3;
		UpdateAutoTradeButton(currentAutoTradeOn);
		int[] sliderCurve = HUD_Scenario.SliderCurve1000;
		for (int i = 0; i <= 100; i++)
		{
			if (num2 <= sliderCurve[i])
			{
				RefAutotrade_Buy_Slider.Value = i;
				break;
			}
		}
		for (int j = 0; j <= 100; j++)
		{
			if (num3 <= sliderCurve[j])
			{
				RefAutotrade_Sell_Slider.Value = j;
				break;
			}
		}
		sliderSetup = false;
	}

	public void toggleAutoTrade()
	{
		currentAutoTradeOn = !currentAutoTradeOn;
		if (currentAutoTradeOn)
		{
			EngineInterface.GameAction(Enums.GameActionCommand.Autotrade_OnOff, -1, 1);
		}
		else
		{
			EngineInterface.GameAction(Enums.GameActionCommand.Autotrade_OnOff, -1, 0);
		}
		UpdateAutoTradeButton(currentAutoTradeOn);
	}

	private void UpdateAutoTradeButton(bool tradingOn)
	{
		if (tradingOn)
		{
			PropEx.SetSprite1(RefTrade_AutoToggle, MainViewModel.Instance.GameSprites[269]);
			PropEx.SetSprite2(RefTrade_AutoToggle, MainViewModel.Instance.GameSprites[270]);
			PropEx.SetSprite3(RefTrade_AutoToggle, MainViewModel.Instance.GameSprites[270]);
			PropEx.SetSprite4(RefTrade_AutoToggle, MainViewModel.Instance.GameSprites[270]);
		}
		else
		{
			PropEx.SetSprite1(RefTrade_AutoToggle, MainViewModel.Instance.GameSprites[271]);
			PropEx.SetSprite2(RefTrade_AutoToggle, MainViewModel.Instance.GameSprites[272]);
			PropEx.SetSprite3(RefTrade_AutoToggle, MainViewModel.Instance.GameSprites[272]);
			PropEx.SetSprite4(RefTrade_AutoToggle, MainViewModel.Instance.GameSprites[272]);
		}
	}

	public void UpdateAutoTradeSelectButton(bool lastState = false)
	{
		if ((!lastState) ? (GameData.Instance.lastGameState.autotrade_onoff[GameData.Instance.lastGameState.trading_current_goods] > 0) : currentAutoTradeOn)
		{
			PropEx.SetSprite1(RefTrade_GoTo_Auto, MainViewModel.Instance.GameSprites[273]);
			PropEx.SetSprite2(RefTrade_GoTo_Auto, MainViewModel.Instance.GameSprites[274]);
			PropEx.SetSprite3(RefTrade_GoTo_Auto, MainViewModel.Instance.GameSprites[274]);
			PropEx.SetSprite4(RefTrade_GoTo_Auto, MainViewModel.Instance.GameSprites[274]);
		}
		else
		{
			PropEx.SetSprite1(RefTrade_GoTo_Auto, MainViewModel.Instance.GameSprites[275]);
			PropEx.SetSprite2(RefTrade_GoTo_Auto, MainViewModel.Instance.GameSprites[276]);
			PropEx.SetSprite3(RefTrade_GoTo_Auto, MainViewModel.Instance.GameSprites[276]);
			PropEx.SetSprite4(RefTrade_GoTo_Auto, MainViewModel.Instance.GameSprites[276]);
		}
	}

	private void Autotrade_Sell_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		int num = (int)RefAutotrade_Sell_Slider.Value;
		if (!insideValueChanged)
		{
			insideValueChanged = true;
			if (num <= (int)RefAutotrade_Buy_Slider.Value)
			{
				RefAutotrade_Buy_Slider.Value = num - 1;
			}
			insideValueChanged = false;
		}
		MainViewModel.Instance.TradeAutoSell = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 144) + " > " + HUD_Scenario.SliderCurve1000[num];
		EngineInterface.GameAction(Enums.GameActionCommand.Autotrade_SetSell, 0, HUD_Scenario.SliderCurve1000[num]);
		if (!sliderSetup && !currentAutoTradeOn)
		{
			toggleAutoTrade();
		}
	}

	private void Autotrade_Buy_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		int num = (int)RefAutotrade_Buy_Slider.Value;
		if (!insideValueChanged)
		{
			insideValueChanged = true;
			if (num >= (int)RefAutotrade_Sell_Slider.Value)
			{
				RefAutotrade_Sell_Slider.Value = num + 1;
			}
			insideValueChanged = false;
		}
		MainViewModel.Instance.TradeAutoBuy = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 143) + " < " + HUD_Scenario.SliderCurve1000[num];
		EngineInterface.GameAction(Enums.GameActionCommand.Autotrade_SetBuy, 0, HUD_Scenario.SliderCurve1000[num]);
		if (!sliderSetup && !currentAutoTradeOn)
		{
			toggleAutoTrade();
		}
	}

	private void InitializeComponent()
	{
		GUI.LoadComponent(this, "Assets/GUI/XAMLResources/HUD_Buildings.xaml");
	}

	protected override bool ConnectEvent(object source, string eventName, string handlerName)
	{
		if (eventName == "MouseEnter" && handlerName == "CommonRedButtonEnter")
		{
			if (source is Button)
			{
				((Button)source).MouseEnter += MainViewModel.Instance.CommonRedButtonEnter;
			}
			else if (source is RadioButton)
			{
				((RadioButton)source).MouseEnter += MainViewModel.Instance.CommonRedButtonEnter;
			}
			return true;
		}
		return false;
	}

	public void OpenFletchers()
	{
		if (GameData.Instance.lastGameState != null)
		{
			if (GameData.Instance.lastGameState.weapon_being_made_next == 17)
			{
				RefProducingBows.IsChecked = true;
			}
			else
			{
				RefProducingXBows.IsChecked = true;
			}
			if (GameData.Instance.lastGameState.can_make_xbows > 0)
			{
				RefProducingXBows.Visibility = Visibility.Visible;
			}
			else
			{
				RefProducingXBows.Visibility = Visibility.Hidden;
			}
		}
	}

	public void OpenPoleturners()
	{
		if (GameData.Instance.lastGameState != null)
		{
			if (GameData.Instance.lastGameState.weapon_being_made_next == 19)
			{
				RefProducingSpears.IsChecked = true;
			}
			else
			{
				RefProducingPikes.IsChecked = true;
			}
			if (GameData.Instance.lastGameState.can_make_pike > 0)
			{
				RefProducingPikes.Visibility = Visibility.Visible;
			}
			else
			{
				RefProducingPikes.Visibility = Visibility.Hidden;
			}
		}
	}

	public void OpenBlacksmiths()
	{
		if (GameData.Instance.lastGameState != null)
		{
			if (GameData.Instance.lastGameState.weapon_being_made_next == 22)
			{
				RefProducingSwords.IsChecked = true;
			}
			else
			{
				RefProducingMaces.IsChecked = true;
			}
			if (GameData.Instance.lastGameState.can_make_sword > 0)
			{
				RefProducingSwords.Visibility = Visibility.Visible;
			}
			else
			{
				RefProducingSwords.Visibility = Visibility.Hidden;
			}
		}
	}

	public string GetBuildingTitle(int thisType, int thisPanel)
	{
		switch (thisPanel)
		{
		case 70:
			return "";
		case 54:
			return "TEXT_IN_TRADEPOST_003";
		case 55:
			return "TEXT_IN_TRADEPOST_004";
		case 56:
			return "TEXT_IN_TRADEPOST_005";
		case 53:
			return "";
		case 57:
			return "";
		case 71:
			return "";
		case 72:
			return "TEXT_REPORT_BUTTONS_002";
		case 69:
			return "TEXT_REPORT_BUTTONS_002";
		case 73:
			return "TEXT_REPORT_BUTTONS_003";
		case 74:
			return "TEXT_REPORT_BUTTONS_004";
		case 75:
			return "TEXT_REPORT_BUTTONS_005";
		case 76:
			return "TEXT_REPORT_BUTTONS_006";
		case 68:
			return "TEXT_REPORT_BUTTONS_006";
		case 77:
			return "TEXT_REPORT_BUTTONS_007";
		case 78:
			return "TEXT_REPORT_BUTTONS_008";
		case 79:
			return "TEXT_REPORT_BUTTONS_009";
		case 93:
			return "TEXT_IN_POND_001";
		default:
			if (thisType >= 118 && thisType <= 120)
			{
				return "TEXT_IN_GARDEN_001";
			}
			if (thisType <= 0)
			{
				return "";
			}
			if (thisType >= 110)
			{
				return "";
			}
			return BuildingTitles[thisType];
		}
	}

	public int GetBuildingSketchImage(int thisType, int thisPanel)
	{
		switch (thisPanel)
		{
		case 71:
			return 0;
		case 72:
			return 242;
		case 69:
			return 242;
		case 73:
			return 228;
		case 74:
			return 243;
		case 75:
			return 230;
		case 76:
			return 225;
		case 68:
			return 225;
		case 77:
			return 247;
		case 78:
			return 252;
		case 79:
			return 245;
		case 93:
			return 219;
		default:
			if (thisType >= 118 && thisType <= 120)
			{
				return 207;
			}
			if (thisPanel == 70)
			{
				return thisType switch
				{
					3 => 258, 
					62 => 223, 
					4 => 260, 
					6 => 234, 
					8 => 249, 
					7 => 249, 
					10 => 240, 
					13 => 227, 
					14 => 227, 
					12 => 227, 
					11 => 227, 
					15 => 224, 
					16 => 221, 
					18 => 241, 
					19 => 256, 
					20 => 185, 
					31 => 236, 
					32 => 236, 
					33 => 244, 
					34 => 233, 
					35 => 226, 
					36 => 235, 
					42 => 250, 
					43 => 199, 
					57 => 237, 
					17 => 259, 
					21 => 257, 
					63 => 238, 
					64 => 224, 
					1 => 209, 
					47 => 218, 
					45 => 216, 
					67 => 216, 
					53 => 229, 
					54 => 231, 
					9 => 178, 
					51 => 178, 
					56 => 255, 
					_ => 0, 
				};
			}
			if (thisType <= 0)
			{
				return 0;
			}
			if (thisType >= 110)
			{
				return 0;
			}
			return sketchList[thisType];
		}
	}

	public int GetPartnerImage(int thisType)
	{
		return thisType switch
		{
			3 => 151, 
			4 => 152, 
			5 => 153, 
			6 => 157, 
			8 => 158, 
			7 => 158, 
			10 => 159, 
			13 => 160, 
			14 => 160, 
			12 => 174, 
			11 => 173, 
			15 => 161, 
			16 => 162, 
			18 => 163, 
			19 => 164, 
			20 => 165, 
			31 => 166, 
			32 => 166, 
			33 => 167, 
			34 => 168, 
			35 => 169, 
			36 => 170, 
			42 => 171, 
			57 => 172, 
			17 => 154, 
			21 => 155, 
			63 => 156, 
			_ => 174, 
		};
	}

	public bool GetBuildingSleepable(int thisType, int thisPanel)
	{
		switch (thisPanel)
		{
		case 70:
			return false;
		case 71:
			return false;
		case 72:
			return false;
		case 69:
			return false;
		case 73:
			return false;
		case 74:
			return false;
		case 75:
			return false;
		case 76:
			return false;
		case 68:
			return false;
		case 77:
			return false;
		case 78:
			return false;
		case 79:
			return false;
		default:
			if (thisType <= 0)
			{
				return false;
			}
			if (thisType >= 110)
			{
				return false;
			}
			if (IsWorkerBuilding[thisType] == 1)
			{
				return true;
			}
			return false;
		}
	}

	public bool GetBuildingShowWorkers(int thisType, int thisPanel)
	{
		switch (thisPanel)
		{
		case 70:
			return false;
		case 71:
			return false;
		case 72:
			return false;
		case 69:
			return false;
		case 73:
			return false;
		case 74:
			return false;
		case 75:
			return false;
		case 76:
			return false;
		case 68:
			return false;
		case 77:
			return false;
		case 78:
			return false;
		case 79:
			return false;
		default:
			if (thisType <= 0)
			{
				return false;
			}
			if (thisType >= 110)
			{
				return false;
			}
			if (ShowWorkersBuilding[thisType] == 1)
			{
				return true;
			}
			return false;
		}
	}

	public bool GetBuildingShowInfo(int thisType, int thisPanel)
	{
		if (thisPanel == 70)
		{
			return false;
		}
		switch (thisType)
		{
		case 1:
		case 4:
		case 21:
		case 28:
		case 35:
		case 62:
		case 63:
		case 65:
		case 66:
		case 74:
		case 75:
		case 76:
		case 77:
		case 78:
		case 80:
		case 81:
		case 82:
		case 83:
		case 84:
		case 91:
		case 92:
		case 93:
		case 94:
		case 95:
		case 97:
		case 98:
		case 101:
		case 103:
		case 104:
		case 118:
		case 119:
		case 120:
		case 121:
		case 122:
			return true;
		default:
			return false;
		}
	}

	public string GetBuildingInfo(int thisType, int thisPanel)
	{
		switch (thisPanel)
		{
		case 70:
			return "";
		case 71:
		case 72:
		case 73:
		case 74:
		case 75:
		case 76:
		case 77:
		case 78:
		case 79:
			return "";
		default:
			return thisType switch
			{
				1 => "TEXT_IN_HOUSE_002", 
				21 => "TEXT_IN_QUARRYPILE_002", 
				4 => "TEXT_IN_OXEN_BASE_004", 
				28 => "TEXT_IN_OIL_SMELTER_002", 
				74 => "TEXT_IN_TOWER_002", 
				75 => "TEXT_IN_TOWER_003", 
				76 => "TEXT_IN_TOWER_004", 
				77 => "TEXT_IN_TOWER_005", 
				78 => "TEXT_IN_TOWER_006", 
				62 => "TEXT_IN_GALLOWS_002", 
				91 => "TEXT_IN_GALLOWS_002", 
				63 => "TEXT_IN_GALLOWS_002", 
				92 => "TEXT_IN_GALLOWS_002", 
				94 => "TEXT_IN_GALLOWS_002", 
				95 => "TEXT_IN_GALLOWS_002", 
				93 => "TEXT_IN_GALLOWS_002", 
				97 => "TEXT_IN_GALLOWS_002", 
				98 => "TEXT_IN_GALLOWS_002", 
				65 => "TEXT_IN_MAYPOLE_002", 
				103 => "TEXT_IN_DANCING_BEAR_002", 
				66 => "TEXT_IN_GARDEN_002", 
				118 => "TEXT_IN_GARDEN_002", 
				119 => "TEXT_IN_GARDEN_002", 
				120 => "TEXT_IN_GARDEN_002", 
				101 => "TEXT_IN_MAYPOLE_002", 
				104 => "TEXT_IN_POND_002", 
				121 => "TEXT_IN_POND_002", 
				122 => "TEXT_IN_POND_002", 
				_ => "", 
			};
		}
	}

	public bool GetBuildingShowRepair(int thisType, int thisPanel)
	{
		return thisPanel switch
		{
			70 => false, 
			53 => false, 
			71 => false, 
			72 => false, 
			69 => false, 
			73 => false, 
			74 => false, 
			75 => false, 
			76 => false, 
			68 => false, 
			77 => false, 
			78 => false, 
			79 => false, 
			_ => thisType switch
			{
				74 => true, 
				75 => true, 
				76 => true, 
				77 => true, 
				78 => true, 
				47 => true, 
				46 => true, 
				45 => true, 
				48 => true, 
				_ => false, 
			}, 
		};
	}

	public bool GetBuildingShowHelp(int thisType, int thisPanel)
	{
		return thisPanel switch
		{
			70 => false, 
			53 => false, 
			71 => false, 
			72 => false, 
			69 => false, 
			73 => false, 
			74 => false, 
			75 => false, 
			76 => false, 
			68 => false, 
			77 => false, 
			78 => false, 
			79 => false, 
			_ => HUD_Help.doesBuildingHelpExist(thisType), 
		};
	}

	public void UpdateButtonState()
	{
		if (GameData.Instance.lastGameState == null)
		{
			return;
		}
		switch (GameData.Instance.app_sub_mode)
		{
		case 36:
			if (GameData.Instance.lastGameState.gatehouse_state == 10)
			{
				PropEx.SetSprite1(RefButtonCloseGate, MainViewModel.Instance.GameSprites[85]);
				PropEx.SetSprite2(RefButtonCloseGate, MainViewModel.Instance.GameSprites[85]);
				PropEx.SetSprite1(RefButtonOpenGate, MainViewModel.Instance.GameSprites[84]);
				PropEx.SetSprite2(RefButtonOpenGate, MainViewModel.Instance.GameSprites[84]);
			}
			else
			{
				PropEx.SetSprite1(RefButtonCloseGate, MainViewModel.Instance.GameSprites[86]);
				PropEx.SetSprite2(RefButtonCloseGate, MainViewModel.Instance.GameSprites[86]);
				PropEx.SetSprite1(RefButtonOpenGate, MainViewModel.Instance.GameSprites[83]);
				PropEx.SetSprite2(RefButtonOpenGate, MainViewModel.Instance.GameSprites[83]);
			}
			break;
		case 37:
			if (GameData.Instance.lastGameState.gatehouse_state == 10)
			{
				PropEx.SetSprite1(RefButtonCloseBridge, MainViewModel.Instance.GameSprites[85]);
				PropEx.SetSprite2(RefButtonCloseBridge, MainViewModel.Instance.GameSprites[85]);
				PropEx.SetSprite1(RefButtonOpenBridge, MainViewModel.Instance.GameSprites[84]);
				PropEx.SetSprite2(RefButtonOpenBridge, MainViewModel.Instance.GameSprites[84]);
			}
			else
			{
				PropEx.SetSprite1(RefButtonCloseBridge, MainViewModel.Instance.GameSprites[86]);
				PropEx.SetSprite2(RefButtonCloseBridge, MainViewModel.Instance.GameSprites[86]);
				PropEx.SetSprite1(RefButtonOpenBridge, MainViewModel.Instance.GameSprites[83]);
				PropEx.SetSprite2(RefButtonOpenBridge, MainViewModel.Instance.GameSprites[83]);
			}
			break;
		case 88:
			if (GameData.Instance.lastGameState.dog_cage_state == 0 && (!OnScreenText.Instance.inPeaceTime || !Director.instance.MultiplayerGame))
			{
				RefButtonReleaseDogs.IsEnabled = true;
				RefDogsReleasedText.Visibility = Visibility.Hidden;
				break;
			}
			RefButtonReleaseDogs.IsEnabled = false;
			if (GameData.Instance.lastGameState.dog_cage_state != 0)
			{
				RefDogsReleasedText.Visibility = Visibility.Visible;
			}
			else
			{
				RefDogsReleasedText.Visibility = Visibility.Hidden;
			}
			break;
		}
	}
}
