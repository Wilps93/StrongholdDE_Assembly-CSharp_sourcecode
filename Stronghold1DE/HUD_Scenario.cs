using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Noesis;

namespace Stronghold1DE;

public class HUD_Scenario : UserControl
{
	private WGT_Heading RefHeading;

	private Grid RefViewRoot;

	private Grid RefViewMain;

	private Grid RefViewStartingGoods;

	private Grid RefViewTradedGoods;

	private Grid RefViewBuildingAvailibity;

	private Grid RefViewInvasions;

	private Grid RefViewMessages;

	private Grid RefViewEvents;

	private Grid RefViewEventsConditions;

	private Grid RefViewEventsActions;

	private Grid RefViewAttackingForce;

	private Grid RefViewEditMessage;

	private Grid RefScenarioViewEditTeams;

	private Grid RefScenarioViewAdjustDates;

	private ListView RefScenarioActionList;

	private ListView RefScenarioBuildingList;

	private TextBox RefStartingYear;

	private TextBox RefEditMessage;

	private Slider RefStartingPop;

	private Slider RefStartingSpecialGold;

	private Slider RefStartingGold;

	private Slider RefStartingPitch;

	public Slider RefStartingGoods;

	public Slider RefAttackingForce;

	private RadioButton RefStartingGoodsGoldDefault;

	private RadioButton RefStartingAttackingForceArcherDefault;

	private CheckBox RefFasterGoodsCheck;

	private StackPanel RefLeftPanel;

	private TextBox RefMessageYear;

	private Button RefButtonMessage_Taunt;

	private Button RefButtonMessage_Anger;

	private Button RefButtonMessage_Pleading;

	private Button RefButtonMessage_Victory;

	private Button RefButtonMessage_Civil;

	private Button RefButtonMessage_Military;

	private Button RefButtonMessage_New1;

	private Button RefButtonMessage_New2;

	private Button RefButtonMessage_New3;

	private Button RefButtonMessage_New4;

	private Image RefMessageImage_Rat;

	private Image RefMessageImage_Snake;

	private Image RefMessageImage_Pig;

	private Image RefMessageImage_Wolf;

	private ListView RefScenarioMessageList;

	private TextBox RefInvasionYear;

	public Slider RefInvasionRepeatSlider;

	private Button RefSignpost;

	public Slider RefInvasionSize;

	private RadioButton RefInvasionSizeArcherDefault;

	private TextBox RefEventYear;

	private ListView RefScenarioEventActionList;

	public Slider RefActionRepeatMonthsSlider;

	public Slider RefActionRepeatSlider;

	public Slider RefActionValueSlider;

	public Slider RefActionValue2Slider;

	private ListView RefScenarioEventConditionList;

	public Slider RefConditionValueSlider;

	public Slider RefSliderEditTeam1;

	public Slider RefSliderEditTeam2;

	public Slider RefSliderEditTeam3;

	public Slider RefSliderEditTeam4;

	public Slider RefSliderEditTeam5;

	public Slider RefSliderEditTeam6;

	public Slider RefSliderEditTeam7;

	public Slider RefSliderEditTeam8;

	private TextBox RefAdjustStartingYear;

	private Button RefButtonEditText;

	private Button RefButtonStartingGoodsPresetLow;

	private Button RefButtonStartingGoodsPresetMedium;

	private Button RefButtonStartingGoodsPresetHigh;

	private SolidColorBrush[] lordColours = new SolidColorBrush[6]
	{
		new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
		new SolidColorBrush(Color.FromArgb(byte.MaxValue, 240, 121, 30)),
		new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 209, 35)),
		new SolidColorBrush(Color.FromArgb(byte.MaxValue, 204, 27, 56)),
		new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 0, 0)),
		new SolidColorBrush(Color.FromArgb(0, 0, 0, 0))
	};

	public int newStartingMonthValue;

	private ObservableCollection<ScenarioEditorRow> BuildingItems = new ObservableCollection<ScenarioEditorRow>();

	private ObservableCollection<ScenarioEditorRow> EventConditionItems = new ObservableCollection<ScenarioEditorRow>();

	private Dictionary<int, ScenarioEditorRow> BuildingItemsDict = new Dictionary<int, ScenarioEditorRow>();

	private bool barracksItemsShowing;

	private int barracksWoodRow = -1;

	private int barracksStoneRow = -1;

	private bool fletcherItemsShowing;

	private bool poleturnerItemsShowing;

	private bool blacksmithItemsShowing;

	private EngineInterface.tl_message scenarioCurrentMessage;

	private EngineInterface.tl_event scenarioCurrentEvent;

	private EngineInterface.tl_invasion scenarioCurrentInvasion;

	private int scenarioCurrentLine = -1;

	private int lastMessageCat1 = -1;

	private int lastMessageCat2 = -1;

	private int lastMessageCat3 = -1;

	private ScenarioEditorRow activeEventActionRow;

	private ObservableCollection<ScenarioEditorRow> MessageListItems = new ObservableCollection<ScenarioEditorRow>();

	private ScenarioEditorRow activeEventConditionRow;

	private int[] SliderCurve500 = new int[101]
	{
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
		10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
		20, 22, 24, 26, 28, 30, 32, 34, 36, 38,
		40, 42, 44, 46, 48, 50, 52, 54, 56, 58,
		60, 62, 64, 66, 68, 70, 72, 74, 76, 78,
		80, 82, 84, 86, 88, 90, 92, 94, 96, 98,
		100, 110, 120, 130, 140, 150, 160, 170, 180, 190,
		200, 210, 220, 230, 240, 250, 260, 270, 280, 290,
		300, 310, 320, 330, 340, 350, 360, 370, 380, 390,
		400, 410, 420, 430, 440, 450, 460, 470, 480, 490,
		500
	};

	public static int[] SliderCurve1000 = new int[101]
	{
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
		10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
		20, 21, 22, 24, 26, 28, 30, 32, 34, 36,
		38, 40, 42, 44, 46, 48, 50, 54, 58, 62,
		66, 70, 74, 78, 82, 86, 90, 94, 98, 100,
		105, 110, 120, 130, 140, 150, 160, 170, 180, 190,
		200, 220, 240, 260, 280, 300, 320, 340, 360, 380,
		400, 420, 440, 460, 480, 500, 520, 540, 560, 580,
		600, 620, 640, 660, 680, 700, 720, 740, 760, 780,
		800, 820, 840, 860, 880, 900, 920, 940, 960, 980,
		1000
	};

	private int[] SliderCurve10000 = new int[146]
	{
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
		10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
		20, 21, 22, 24, 26, 28, 30, 32, 34, 36,
		38, 40, 42, 44, 46, 48, 50, 54, 58, 62,
		66, 70, 74, 78, 82, 86, 90, 94, 98, 100,
		105, 110, 120, 130, 140, 150, 160, 170, 180, 190,
		200, 220, 240, 260, 280, 300, 320, 340, 360, 380,
		400, 420, 440, 460, 480, 500, 520, 540, 560, 580,
		600, 620, 640, 660, 680, 700, 720, 740, 760, 780,
		800, 820, 840, 860, 880, 900, 920, 940, 960, 980,
		1000, 1200, 1400, 1600, 1800, 2000, 2200, 2400, 2600, 2800,
		3000, 3200, 3400, 3600, 3800, 4000, 4200, 4400, 4600, 4800,
		5000, 5200, 5400, 5600, 5800, 6000, 6200, 6400, 6600, 6800,
		7000, 7200, 7400, 7600, 7800, 8000, 8200, 8400, 8600, 8800,
		9000, 9200, 9400, 9600, 9800, 10000
	};

	private int[] SliderCurve25000 = new int[161]
	{
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
		10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
		20, 21, 22, 24, 26, 28, 30, 32, 34, 36,
		38, 40, 42, 44, 46, 48, 50, 54, 58, 62,
		66, 70, 74, 78, 82, 86, 90, 94, 98, 100,
		105, 110, 120, 130, 140, 150, 160, 170, 180, 190,
		200, 220, 240, 260, 280, 300, 320, 340, 360, 380,
		400, 420, 440, 460, 480, 500, 520, 540, 560, 580,
		600, 620, 640, 660, 680, 700, 720, 740, 760, 780,
		800, 820, 840, 860, 880, 900, 920, 940, 960, 980,
		1000, 1200, 1400, 1600, 1800, 2000, 2200, 2400, 2600, 2800,
		3000, 3200, 3400, 3600, 3800, 4000, 4200, 4400, 4600, 4800,
		5000, 5200, 5400, 5600, 5800, 6000, 6200, 6400, 6600, 6800,
		7000, 7200, 7400, 7600, 7800, 8000, 8200, 8400, 8600, 8800,
		9000, 9200, 9400, 9600, 9800, 10000, 11000, 12000, 13000, 14000,
		15000, 16000, 17000, 18000, 19000, 20000, 21000, 22000, 23000, 24000,
		25000
	};

	public HUD_Scenario()
	{
		InitializeComponent();
		MainViewModel.Instance.HUDScenario = this;
		RefHeading = (WGT_Heading)FindName("ScenarioHeader");
		RefViewRoot = (Grid)FindName("LayoutRoot");
		RefViewRoot.Visibility = Visibility.Hidden;
		RefLeftPanel = (StackPanel)FindName("LeftPanel");
		RefViewMain = (Grid)FindName("ScenarioViewMain");
		RefViewStartingGoods = (Grid)FindName("ScenarioViewStartingGoods");
		RefViewTradedGoods = (Grid)FindName("ScenarioViewTradedGoods");
		RefViewBuildingAvailibity = (Grid)FindName("ScenarioViewBuildingAvailibility");
		RefViewInvasions = (Grid)FindName("ScenarioViewInvasions");
		RefViewMessages = (Grid)FindName("ScenarioViewMessages");
		RefViewEvents = (Grid)FindName("ScenarioViewEvents");
		RefViewEventsConditions = (Grid)FindName("ScenarioViewEventsConditions");
		RefViewEventsActions = (Grid)FindName("ScenarioViewEventsActions");
		RefViewAttackingForce = (Grid)FindName("ScenarioViewAttackingForce");
		RefViewEditMessage = (Grid)FindName("ScenarioEditMessage");
		RefScenarioViewEditTeams = (Grid)FindName("ScenarioViewEditTeams");
		RefScenarioViewAdjustDates = (Grid)FindName("ScenarioViewAdjustDates");
		RefStartingGoodsGoldDefault = (RadioButton)FindName("StartingGoodsGoldDefault");
		RefStartingAttackingForceArcherDefault = (RadioButton)FindName("StartingAttackingForceArcherDefault");
		RefFasterGoodsCheck = (CheckBox)FindName("FasterGoodsCheck");
		RefFasterGoodsCheck.Checked += FasterGoodsCheck_ValueChanged;
		RefFasterGoodsCheck.Unchecked += FasterGoodsCheck_ValueChanged;
		RefScenarioActionList = (ListView)FindName("ScenarioActionList");
		RefScenarioBuildingList = (ListView)FindName("ScenarioBuildingList");
		RefScenarioActionList.SelectionChanged += delegate
		{
			if (RefScenarioActionList.SelectedItem != null)
			{
				SelectActionRow(int.Parse(((ScenarioEditorRow)RefScenarioActionList.SelectedItem).DataValue, Director.defaultCulture));
				RefScenarioActionList.SelectedItem = null;
			}
		};
		RefStartingYear = (TextBox)FindName("TextBoxStartingYear");
		RefStartingYear.PreviewTextInput += NumberValidationTextBox;
		RefStartingYear.IsKeyboardFocusedChanged += TextInputFocus;
		RefAdjustStartingYear = (TextBox)FindName("TextBoxAdjustStartingYear");
		RefAdjustStartingYear.PreviewTextInput += NumberValidationTextBox;
		RefAdjustStartingYear.IsKeyboardFocusedChanged += TextInputFocus;
		RefEditMessage = (TextBox)FindName("TextBoxScenarioMessage");
		RefEditMessage.IsKeyboardFocusedChanged += TextInputFocus;
		RefEditMessage.TextChanged += EditMessageTextChanged;
		RefStartingPop = (Slider)FindName("PopSlider");
		RefStartingPop.ValueChanged += ScenarioPopularitySlider_ValueChanged;
		RefStartingSpecialGold = (Slider)FindName("SpecialGoldSlider");
		RefStartingSpecialGold.ValueChanged += ScenarioSpecialGoldSlider_ValueChanged;
		RefStartingGold = (Slider)FindName("GoldSlider");
		RefStartingGold.ValueChanged += ScenarioGoldSlider_ValueChanged;
		RefStartingPitch = (Slider)FindName("PitchSlider");
		RefStartingPitch.ValueChanged += ScenarioPitchSlider_ValueChanged;
		RefStartingGoods = (Slider)FindName("StartingGoodsSlider");
		RefStartingGoods.ValueChanged += ScenarioStartingGoodsSlider_ValueChanged;
		RefAttackingForce = (Slider)FindName("AttackingForcesSlider");
		RefAttackingForce.ValueChanged += ScenarioAttackingForcesSlider_ValueChanged;
		((Storyboard)base.Resources["Outtro"]).Completed += delegate
		{
			RefViewRoot.Visibility = Visibility.Hidden;
		};
		RefMessageYear = (TextBox)FindName("TextBoxStartingYearMessage");
		RefMessageYear.PreviewTextInput += NumberValidationTextBox;
		RefMessageYear.IsKeyboardFocusedChanged += TextInputFocus;
		RefButtonMessage_Anger = (Button)FindName("ButtonMessage_Anger");
		RefButtonMessage_Taunt = (Button)FindName("ButtonMessage_Taunt");
		RefButtonMessage_Pleading = (Button)FindName("ButtonMessage_Pleading");
		RefButtonMessage_Victory = (Button)FindName("ButtonMessage_Victory");
		RefButtonMessage_Civil = (Button)FindName("ButtonMessage_Civil");
		RefButtonMessage_Military = (Button)FindName("ButtonMessage_Military");
		RefButtonMessage_New1 = (Button)FindName("ButtonMessage_New1");
		RefButtonMessage_New2 = (Button)FindName("ButtonMessage_New2");
		RefButtonMessage_New3 = (Button)FindName("ButtonMessage_New3");
		RefButtonMessage_New4 = (Button)FindName("ButtonMessage_New4");
		RefSignpost = (Button)FindName("Signpost");
		RefMessageImage_Rat = (Image)FindName("MessageImage_Rat");
		RefMessageImage_Snake = (Image)FindName("MessageImage_Snake");
		RefMessageImage_Pig = (Image)FindName("MessageImage_Pig");
		RefMessageImage_Wolf = (Image)FindName("MessageImage_Wolf");
		RefScenarioMessageList = (ListView)FindName("ScenarioMessageList");
		RefScenarioMessageList.SelectionChanged += delegate
		{
			if (RefScenarioMessageList.SelectedItem != null)
			{
				MessageSelected(int.Parse(((ScenarioEditorRow)RefScenarioMessageList.SelectedItem).DataValue, Director.defaultCulture));
			}
		};
		RefInvasionYear = (TextBox)FindName("TextBoxStartingYearInvasion");
		RefInvasionYear.PreviewTextInput += NumberValidationTextBox;
		RefInvasionYear.IsKeyboardFocusedChanged += TextInputFocus;
		RefInvasionRepeatSlider = (Slider)FindName("InvasionRepeatSlider");
		RefInvasionRepeatSlider.ValueChanged += ScenarioInvasionRepeatSlider_ValueChanged;
		RefInvasionSize = (Slider)FindName("InvasionSlider");
		RefInvasionSize.ValueChanged += ScenarioInvasionSizeSlider_ValueChanged;
		RefInvasionSizeArcherDefault = (RadioButton)FindName("InvasionSizeArcherDefault");
		RefEventYear = (TextBox)FindName("TextBoxStartingYearEvent");
		RefEventYear.PreviewTextInput += NumberValidationTextBox;
		RefEventYear.IsKeyboardFocusedChanged += TextInputFocus;
		RefScenarioEventActionList = (ListView)FindName("ScenarioEventActionList");
		RefScenarioEventActionList.SelectionChanged += delegate
		{
			if (RefScenarioEventActionList.SelectedItem != null)
			{
				RefScenarioEventActionList.SelectedItem = null;
			}
		};
		RefActionRepeatMonthsSlider = (Slider)FindName("ActionRepeatMonthsSlider");
		RefActionRepeatMonthsSlider.ValueChanged += ActionRepeatMonthsSlider_ValueChanged;
		RefActionRepeatSlider = (Slider)FindName("ActionRepeatSlider");
		RefActionRepeatSlider.ValueChanged += ActionRepeatSlider_ValueChanged;
		RefActionValueSlider = (Slider)FindName("ActionValueSlider");
		RefActionValueSlider.ValueChanged += ActionValueSlider_ValueChanged;
		RefActionValue2Slider = (Slider)FindName("ActionValue2Slider");
		RefActionValue2Slider.ValueChanged += ActionValue2Slider_ValueChanged;
		RefScenarioEventConditionList = (ListView)FindName("ScenarioEventConditionList");
		RefScenarioEventConditionList.SelectionChanged += delegate
		{
			if (RefScenarioEventConditionList.SelectedItem != null)
			{
				RefScenarioEventConditionList.SelectedItem = null;
			}
		};
		RefConditionValueSlider = (Slider)FindName("ConditionValueSlider");
		RefConditionValueSlider.ValueChanged += ConditionValueSlider_ValueChanged;
		RefSliderEditTeam1 = (Slider)FindName("SliderEditTeam1");
		RefSliderEditTeam1.ValueChanged += ScenarioRefSliderEditTeam_ValueChanged;
		RefSliderEditTeam2 = (Slider)FindName("SliderEditTeam2");
		RefSliderEditTeam2.ValueChanged += ScenarioRefSliderEditTeam_ValueChanged;
		RefSliderEditTeam3 = (Slider)FindName("SliderEditTeam3");
		RefSliderEditTeam3.ValueChanged += ScenarioRefSliderEditTeam_ValueChanged;
		RefSliderEditTeam4 = (Slider)FindName("SliderEditTeam4");
		RefSliderEditTeam4.ValueChanged += ScenarioRefSliderEditTeam_ValueChanged;
		RefSliderEditTeam5 = (Slider)FindName("SliderEditTeam5");
		RefSliderEditTeam5.ValueChanged += ScenarioRefSliderEditTeam_ValueChanged;
		RefSliderEditTeam6 = (Slider)FindName("SliderEditTeam6");
		RefSliderEditTeam6.ValueChanged += ScenarioRefSliderEditTeam_ValueChanged;
		RefSliderEditTeam7 = (Slider)FindName("SliderEditTeam7");
		RefSliderEditTeam7.ValueChanged += ScenarioRefSliderEditTeam_ValueChanged;
		RefSliderEditTeam8 = (Slider)FindName("SliderEditTeam8");
		RefSliderEditTeam8.ValueChanged += ScenarioRefSliderEditTeam_ValueChanged;
		RefButtonEditText = (Button)FindName("ButtonEditText");
		RefButtonStartingGoodsPresetLow = (Button)FindName("ButtonStartingGoodsPresetLow");
		RefButtonStartingGoodsPresetMedium = (Button)FindName("ButtonStartingGoodsPresetMedium");
		RefButtonStartingGoodsPresetHigh = (Button)FindName("ButtonStartingGoodsPresetHigh");
		if (FatControler.italian)
		{
			PropEx.SetGlowButtonFontSize(RefButtonEditText, 14);
			PropEx.SetGlowButtonTextHeight(RefButtonEditText, 20);
			PropEx.SetGlowButtonFontSize(RefButtonStartingGoodsPresetLow, 14);
			PropEx.SetGlowButtonTextHeight(RefButtonStartingGoodsPresetLow, 20);
			PropEx.SetGlowButtonFontSize(RefButtonStartingGoodsPresetMedium, 14);
			PropEx.SetGlowButtonTextHeight(RefButtonStartingGoodsPresetMedium, 20);
			PropEx.SetGlowButtonFontSize(RefButtonStartingGoodsPresetHigh, 12);
			PropEx.SetGlowButtonTextHeight(RefButtonStartingGoodsPresetHigh, 18);
		}
		if (FatControler.portuguese)
		{
			PropEx.SetGlowButtonFontSize(RefButtonStartingGoodsPresetLow, 12);
			PropEx.SetGlowButtonTextHeight(RefButtonStartingGoodsPresetLow, 18);
			PropEx.SetGlowButtonFontSize(RefButtonStartingGoodsPresetMedium, 12);
			PropEx.SetGlowButtonTextHeight(RefButtonStartingGoodsPresetMedium, 18);
			PropEx.SetGlowButtonFontSize(RefButtonStartingGoodsPresetHigh, 12);
			PropEx.SetGlowButtonTextHeight(RefButtonStartingGoodsPresetHigh, 18);
			MainViewModel.Instance.ScenarioTradeTextSize = "12";
			MainViewModel.Instance.ScenarioTradeTextHeight = "18";
			PropEx.SetGlowButtonFontSize(RefSignpost, 12);
			PropEx.SetGlowButtonTextHeight(RefSignpost, 18);
		}
		if (FatControler.japanese)
		{
			PropEx.SetGlowButtonFontSize(RefButtonEditText, 12);
			PropEx.SetGlowButtonTextHeight(RefButtonEditText, 20);
		}
		if (FatControler.french)
		{
			PropEx.SetGlowButtonFontSize(RefButtonEditText, 14);
			PropEx.SetGlowButtonTextHeight(RefButtonEditText, 20);
			MainViewModel.Instance.ScenarioTradeTextSize = "12";
			MainViewModel.Instance.ScenarioTradeTextHeight = "18";
		}
		if (FatControler.ukrainian)
		{
			MainViewModel.Instance.ScenarioTradeTextSize = "12";
			MainViewModel.Instance.ScenarioTradeTextHeight = "20";
			PropEx.SetGlowButtonFontSize(RefButtonMessage_Pleading, 12);
			PropEx.SetGlowButtonTextHeight(RefButtonMessage_Pleading, 20);
		}
		if (FatControler.czech)
		{
			MainViewModel.Instance.ScenarioTradeTextSize = "12";
			MainViewModel.Instance.ScenarioTradeTextHeight = "20";
		}
	}

	private void InitializeComponent()
	{
		NoesisUnity.LoadComponent(this, "Assets/GUI/XAMLResources/HUD_Scenario.xaml");
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

	public void StartEntryAnim()
	{
		MainViewModel.Instance.ShowingScenario = true;
		MainViewModel.Instance.HUD_Markers_Vis = false;
		MainViewModel.Instance.HUDScenario.IsEnabled = true;
		MainViewModel.Instance.HUDScenarioPopup.IsEnabled = true;
		RefViewRoot.Visibility = Visibility.Visible;
		((Storyboard)base.Resources["Intro"]).Begin(this);
		MainViewModel.Instance.HUDScenarioPopup.StartEntryAnim();
		MainViewModel.Instance.ScenarioEditorButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 90);
	}

	public void StartExitAnim()
	{
		MainViewModel.Instance.ShowingScenario = false;
		((Storyboard)base.Resources["Outtro"]).Begin(this);
		MainViewModel.Instance.HUDScenarioPopup.StartExitAnim();
		MainViewModel.Instance.ScenarioEditorButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_SCENARIO_EDITOR);
	}

	public void initScenarioControls()
	{
		int currentValue = int.Parse(MainViewModel.Instance.ScenarioStartingGoldText, Director.defaultCulture);
		RefStartingGoods.Value = MainViewModel.Instance.initSliderScenarioStartingGoods();
		FatControler.instance.InitScenarioEditorValues();
		RefStartingPop.Value = int.Parse(MainViewModel.Instance.ScenarioStartingPopText, Director.defaultCulture);
		int currentValue2 = int.Parse(MainViewModel.Instance.ScenarioStartingSpecialGoldText, Director.defaultCulture);
		int maxValue = 10000;
		int freq = 1;
		currentValue2 = getLogSliderValue(currentValue2, 1, ref maxValue, ref freq);
		RefStartingSpecialGold.Value = currentValue2;
		if (GameData.Instance.mapType == Enums.GameModes.SIEGE)
		{
			maxValue = 10000;
			freq = 1;
			currentValue = getLogSliderValue(currentValue, 1, ref maxValue, ref freq);
			RefStartingGold.Value = currentValue;
		}
		RefStartingPitch.Value = int.Parse(MainViewModel.Instance.ScenarioStartingPitchText, Director.defaultCulture);
		changeScenarioView(Enums.ScenarioViews.Main);
		RefFasterGoodsCheck.IsChecked = GameData.Instance.scenarioOverview.fast_goods_feedin > 0;
		SetupScenarioActionsList();
	}

	public void changeScenarioView(Enums.ScenarioViews newView, bool fromButton = true)
	{
		RefViewMain.Visibility = Visibility.Hidden;
		RefViewStartingGoods.Visibility = Visibility.Hidden;
		RefViewTradedGoods.Visibility = Visibility.Hidden;
		RefViewBuildingAvailibity.Visibility = Visibility.Hidden;
		RefViewInvasions.Visibility = Visibility.Hidden;
		RefViewMessages.Visibility = Visibility.Hidden;
		RefViewEvents.Visibility = Visibility.Hidden;
		RefViewEventsConditions.Visibility = Visibility.Hidden;
		RefViewEventsActions.Visibility = Visibility.Hidden;
		RefViewAttackingForce.Visibility = Visibility.Hidden;
		RefViewEditMessage.Visibility = Visibility.Hidden;
		RefScenarioViewEditTeams.Visibility = Visibility.Hidden;
		RefScenarioViewAdjustDates.Visibility = Visibility.Hidden;
		RefLeftPanel.Visibility = Visibility.Visible;
		MainViewModel.Instance.ScenarioBuildingTogglesVis = false;
		string text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_SCENARIO_EDITOR);
		switch (newView)
		{
		case Enums.ScenarioViews.Main:
			RefreshScenarioActions();
			RefScenarioActionList.SelectedItem = null;
			ResetListViewPosition(RefScenarioActionList);
			RefViewMain.Visibility = Visibility.Visible;
			break;
		case Enums.ScenarioViews.StartingGoods:
			RefStartingGoodsGoldDefault.IsChecked = true;
			MainViewModel.Instance.ButtonScenarioStartingGoodSelect("15");
			RefViewStartingGoods.Visibility = Visibility.Visible;
			break;
		case Enums.ScenarioViews.TradedGoods:
			RefViewTradedGoods.Visibility = Visibility.Visible;
			break;
		case Enums.ScenarioViews.BuildingAvailibilty:
			ResetListViewPosition(RefScenarioBuildingList);
			RefViewBuildingAvailibity.Visibility = Visibility.Visible;
			MainViewModel.Instance.ScenarioBuildingTogglesVis = true;
			break;
		case Enums.ScenarioViews.Invasions:
			text = text + " - " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_INVASION);
			if (fromButton)
			{
				NewInvasion();
			}
			RefInvasionSizeArcherDefault.IsChecked = true;
			MainViewModel.Instance.ButtonSelectInvasionSize("0");
			RefLeftPanel.Visibility = Visibility.Hidden;
			RefViewInvasions.Visibility = Visibility.Visible;
			break;
		case Enums.ScenarioViews.Messages:
			text = text + " - " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_MESSAGE);
			if (fromButton)
			{
				NewMessage();
			}
			RefLeftPanel.Visibility = Visibility.Hidden;
			RefViewMessages.Visibility = Visibility.Visible;
			break;
		case Enums.ScenarioViews.Events:
			text = text + " - " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_EVENT);
			if (fromButton)
			{
				NewEvent();
			}
			RefLeftPanel.Visibility = Visibility.Hidden;
			RefViewEvents.Visibility = Visibility.Visible;
			break;
		case Enums.ScenarioViews.EventsConditions:
			text = text + " - " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_EVENT_CONDITIONS);
			RefLeftPanel.Visibility = Visibility.Hidden;
			RefViewEventsConditions.Visibility = Visibility.Visible;
			PopulateEventConditions();
			break;
		case Enums.ScenarioViews.EventsActions:
			text = text + " - " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_EVENT_ACTIONS);
			RefLeftPanel.Visibility = Visibility.Hidden;
			RefViewEventsActions.Visibility = Visibility.Visible;
			PopulateEventActions();
			break;
		case Enums.ScenarioViews.AttackingForce:
			RefStartingAttackingForceArcherDefault.IsChecked = true;
			MainViewModel.Instance.ButtonScenarioAttackingForcesSelect("0");
			RefViewAttackingForce.Visibility = Visibility.Visible;
			break;
		case Enums.ScenarioViews.EditMessage:
			RefViewEditMessage.Visibility = Visibility.Visible;
			MainViewModel.Instance.ScenarioEditMessageText = GameData.Instance.utf8MissionText;
			if (GameData.Instance.showAlternateMissionTextForBriefing)
			{
				RefEditMessage.Height = 200f;
				MainViewModel.Instance.ScenarioMessageAltTextIVisibilityBool = true;
				MainViewModel.Instance.ScenarioAltANSIMessage = GameData.Instance.ansiMissionText;
				MainViewModel.Instance.ScenarioAltUNICODEMessage = GameData.Instance.unicodeMissionText;
				GameData.Instance.showAlternateMissionTextForBriefing = false;
				GameData.Instance.ansiMissionText = "";
				GameData.Instance.unicodeMissionText = "";
			}
			else
			{
				RefEditMessage.Height = 346f;
				MainViewModel.Instance.ScenarioMessageAltTextIVisibilityBool = false;
			}
			RefEditMessage.Focus();
			break;
		case Enums.ScenarioViews.EditTeams:
			RefScenarioViewEditTeams.Visibility = Visibility.Visible;
			if (GameData.Instance.lastGameState != null)
			{
				if (GameData.Instance.lastGameState.starting_teams[1] == 0)
				{
					EngineInterface.GameAction(Enums.GameActionCommand.SetStartingTeam, 1, 1);
					EngineInterface.GameAction(Enums.GameActionCommand.SetStartingTeam, 2, 9);
					EngineInterface.GameAction(Enums.GameActionCommand.SetStartingTeam, 3, 9);
					EngineInterface.GameAction(Enums.GameActionCommand.SetStartingTeam, 4, 9);
					EngineInterface.GameAction(Enums.GameActionCommand.SetStartingTeam, 5, 9);
					EngineInterface.GameAction(Enums.GameActionCommand.SetStartingTeam, 6, 6);
					EngineInterface.GameAction(Enums.GameActionCommand.SetStartingTeam, 7, 7);
					EngineInterface.GameAction(Enums.GameActionCommand.SetStartingTeam, 8, 8);
					GameData.Instance.lastGameState.starting_teams[1] = 1;
					GameData.Instance.lastGameState.starting_teams[2] = 9;
					GameData.Instance.lastGameState.starting_teams[3] = 9;
					GameData.Instance.lastGameState.starting_teams[4] = 9;
					GameData.Instance.lastGameState.starting_teams[5] = 9;
					GameData.Instance.lastGameState.starting_teams[6] = 6;
					GameData.Instance.lastGameState.starting_teams[7] = 7;
					GameData.Instance.lastGameState.starting_teams[8] = 8;
				}
				RefSliderEditTeam1.Value = (int)GameData.Instance.lastGameState.starting_teams[1];
				RefSliderEditTeam2.Value = (int)GameData.Instance.lastGameState.starting_teams[2];
				RefSliderEditTeam3.Value = (int)GameData.Instance.lastGameState.starting_teams[3];
				RefSliderEditTeam4.Value = (int)GameData.Instance.lastGameState.starting_teams[4];
				RefSliderEditTeam5.Value = (int)GameData.Instance.lastGameState.starting_teams[5];
				RefSliderEditTeam6.Value = (int)GameData.Instance.lastGameState.starting_teams[6];
				RefSliderEditTeam7.Value = (int)GameData.Instance.lastGameState.starting_teams[7];
				RefSliderEditTeam8.Value = (int)GameData.Instance.lastGameState.starting_teams[8];
				for (int i = 1; i < 9; i++)
				{
					MainViewModel.Instance.ScenarioEditTeams[i] = GameData.Instance.lastGameState.starting_teams[i].ToString();
				}
			}
			break;
		case Enums.ScenarioViews.AdjustDates:
			RefScenarioViewAdjustDates.Visibility = Visibility.Visible;
			MainViewModel.Instance.ScenarioAdjustStartingYearText = MainViewModel.Instance.ScenarioStartingYearText;
			newStartingMonthValue = GameData.Instance.scenarioOverview.startMonth;
			MainViewModel.Instance.ScenarioAdjustStartingMonthText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MONTHS, newStartingMonthValue);
			break;
		}
		MainViewModel.Instance.ScenarioEditorMode = newView;
		RefHeading.HeadingText = text;
	}

	private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
	{
		Regex regex = new Regex("[^0-9]+");
		e.Handled = regex.IsMatch(e.Text);
	}

	private void TextInputFocus(object sender, DependencyPropertyChangedEventArgs e)
	{
		MainViewModel.Instance.SetNoesisKeyboardState((bool)e.NewValue);
	}

	private void EditMessageTextChanged(object sender, RoutedEventArgs e)
	{
		string text = RefEditMessage.Text;
		GameData.Instance.utf8MissionText = text;
		EngineInterface.SetUTF8MissionText(text);
	}

	private void ScenarioPopularitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		int newStartingPop = (int)RefStartingPop.Value;
		MainViewModel.Instance.SliderScenarioStartPop(newStartingPop);
	}

	private void ScenarioSpecialGoldSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		int sliderPos = (int)RefStartingSpecialGold.Value;
		sliderPos = getLogSliderDislayValue(sliderPos, 10000);
		MainViewModel.Instance.SliderScenarioStartSpecialGold(sliderPos);
	}

	private void ScenarioGoldSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		int sliderPos = (int)RefStartingGold.Value;
		sliderPos = getLogSliderDislayValue(sliderPos, 10000);
		MainViewModel.Instance.SliderScenarioStartGold(sliderPos);
	}

	private void ScenarioPitchSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		int newStartingPitch = (int)RefStartingPitch.Value;
		MainViewModel.Instance.SliderScenarioStartPitch(newStartingPitch);
	}

	private void ScenarioStartingGoodsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		int newStartingAmount = (int)RefStartingGoods.Value;
		MainViewModel.Instance.SliderScenarioStartingGoods(newStartingAmount);
	}

	private void ScenarioAttackingForcesSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		int newStartingAmount = (int)RefAttackingForce.Value;
		MainViewModel.Instance.SliderScenarioAttackingForces(newStartingAmount);
	}

	private void RefreshScenarioActions()
	{
		EngineInterface.ScenarioOverview scenarioOverview = GameData.Instance.scenarioOverview;
		List<ScenarioEditorRow> list = new List<ScenarioEditorRow>();
		for (int i = 0; i < scenarioOverview.entries.Count; i++)
		{
			string date = "";
			string body = "";
			string repeat = "";
			int entryType = 0;
			GameData.Instance.getScenarioEntryOverviewText(i, ref date, ref body, ref repeat, ref entryType);
			list.Add(new ScenarioEditorRow(this)
			{
				Text1 = date,
				Text2 = body,
				Text3 = repeat,
				DataValue = i.ToString()
			});
		}
		RefScenarioActionList.ItemsSource = list;
		ResetListViewPosition(RefScenarioActionList);
	}

	public void ApplyAltMissionText(bool ansi)
	{
		if (ansi)
		{
			MainViewModel.Instance.ScenarioEditMessageText = MainViewModel.Instance.ScenarioAltANSIMessage;
		}
		else
		{
			MainViewModel.Instance.ScenarioEditMessageText = MainViewModel.Instance.ScenarioAltUNICODEMessage;
		}
		RefEditMessage.Height = 346f;
		MainViewModel.Instance.ScenarioMessageAltTextIVisibilityBool = false;
	}

	public void SetupScenarioActionsList()
	{
		BuildingItems.Clear();
		BuildingItemsDict.Clear();
		RefreshScenarioActions();
		EngineInterface.ScenarioOverview scenarioOverview = GameData.Instance.scenarioOverview;
		barracksItemsShowing = false;
		fletcherItemsShowing = false;
		poleturnerItemsShowing = false;
		blacksmithItemsShowing = false;
		for (int i = 0; i < GameData.buildingAvailbleOrder.Length; i++)
		{
			string text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, GameData.buildingAvailbleOrder[i]) + " ";
			ScenarioEditorRow scenarioEditorRow = new ScenarioEditorRow(this);
			scenarioEditorRow.DataValue = i.ToString();
			if (scenarioOverview.scenario_buildings_available[i] > 0)
			{
				scenarioEditorRow.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_ON);
				scenarioEditorRow.Text1HL = text;
				scenarioEditorRow.Text1 = "";
			}
			else
			{
				scenarioEditorRow.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_OFF);
				scenarioEditorRow.Text1 = text;
				scenarioEditorRow.Text1HL = "";
			}
			BuildingItems.Add(scenarioEditorRow);
			BuildingItemsDict[i] = scenarioEditorRow;
			if (GameData.buildingAvailbleOrder[i] == 9)
			{
				barracksWoodRow = i - 1;
				barracksStoneRow = i;
				if (scenarioOverview.scenario_buildings_available[i] <= 0 && scenarioOverview.scenario_buildings_available[i - 1] <= 0)
				{
					continue;
				}
				barracksItemsShowing = true;
				for (int j = 0; j < 7; j++)
				{
					Enums.eChimps index = GameData.scenarioBarracksTroopsAvailableTypes[j];
					string text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, (int)index);
					ScenarioEditorRow scenarioEditorRow2 = new ScenarioEditorRow(this);
					scenarioEditorRow2.DataValue = (-1 - j).ToString();
					if (GameData.Instance.scenarioOverview.sa_troop_availability[j] > 0)
					{
						scenarioEditorRow2.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_ON);
						scenarioEditorRow2.Text3HL = text2;
						scenarioEditorRow2.Text3 = "";
					}
					else
					{
						scenarioEditorRow2.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_OFF);
						scenarioEditorRow2.Text3 = text2;
						scenarioEditorRow2.Text3HL = "";
					}
					BuildingItems.Add(scenarioEditorRow2);
					BuildingItemsDict[-1 - j] = scenarioEditorRow2;
				}
			}
			else if (GameData.buildingAvailbleOrder[i] == 54)
			{
				if (scenarioOverview.scenario_buildings_available[i] > 0)
				{
					fletcherItemsShowing = true;
					string text3 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, 18);
					ScenarioEditorRow scenarioEditorRow3 = new ScenarioEditorRow(this);
					scenarioEditorRow3.DataValue = "-20";
					if (scenarioOverview.sa_fletcher > 0)
					{
						scenarioEditorRow3.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_ON);
						scenarioEditorRow3.Text3HL = text3;
						scenarioEditorRow3.Text3 = "";
					}
					else
					{
						scenarioEditorRow3.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_OFF);
						scenarioEditorRow3.Text3 = text3;
						scenarioEditorRow3.Text3HL = "";
					}
					BuildingItems.Add(scenarioEditorRow3);
					BuildingItemsDict[-20] = scenarioEditorRow3;
				}
			}
			else if (GameData.buildingAvailbleOrder[i] == 55)
			{
				if (scenarioOverview.scenario_buildings_available[i] > 0)
				{
					poleturnerItemsShowing = true;
					string text4 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, 20);
					ScenarioEditorRow scenarioEditorRow4 = new ScenarioEditorRow(this);
					scenarioEditorRow4.DataValue = "-30";
					if (scenarioOverview.sa_poleturner > 0)
					{
						scenarioEditorRow4.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_ON);
						scenarioEditorRow4.Text3HL = text4;
						scenarioEditorRow4.Text3 = "";
					}
					else
					{
						scenarioEditorRow4.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_OFF);
						scenarioEditorRow4.Text3 = text4;
						scenarioEditorRow4.Text3HL = "";
					}
					BuildingItems.Add(scenarioEditorRow4);
					BuildingItemsDict[-30] = scenarioEditorRow4;
				}
			}
			else if (GameData.buildingAvailbleOrder[i] == 51 && scenarioOverview.scenario_buildings_available[i] > 0)
			{
				blacksmithItemsShowing = true;
				string text5 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, 22);
				ScenarioEditorRow scenarioEditorRow5 = new ScenarioEditorRow(this);
				scenarioEditorRow5.DataValue = "-40";
				if (scenarioOverview.sa_blacksmith > 0)
				{
					scenarioEditorRow5.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_ON);
					scenarioEditorRow5.Text3HL = text5;
					scenarioEditorRow5.Text3 = "";
				}
				else
				{
					scenarioEditorRow5.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_OFF);
					scenarioEditorRow5.Text3 = text5;
					scenarioEditorRow5.Text3HL = "";
				}
				BuildingItems.Add(scenarioEditorRow5);
				BuildingItemsDict[-40] = scenarioEditorRow5;
			}
		}
		RefScenarioBuildingList.ItemsSource = BuildingItems;
		ResetListViewPosition(RefScenarioBuildingList);
	}

	public void ButtonScenarioBuildingAvailToggle(object parameter)
	{
		EngineInterface.ScenarioOverview scenarioOverview = GameData.Instance.scenarioOverview;
		int num = Convert.ToInt32(parameter as string);
		if (num >= 1000)
		{
			int num2 = 0;
			if (num == 1000)
			{
				num2 = 1;
			}
			for (int i = 0; i < GameData.buildingAvailbleOrder.Length; i++)
			{
				scenarioOverview.scenario_buildings_available[i] = num2;
				EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_BuildingAvailable, i, scenarioOverview.scenario_buildings_available[i]);
			}
			scenarioOverview.sa_poleturner = num2;
			scenarioOverview.sa_fletcher = num2;
			scenarioOverview.sa_blacksmith = num2;
			for (int j = 0; j < 7; j++)
			{
				scenarioOverview.sa_troop_availability[j] = num2;
				EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_TroopAvailable, j, scenarioOverview.sa_troop_availability[j]);
			}
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_MaceAvailable, 0, scenarioOverview.sa_poleturner);
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_XbowAvailable, 0, scenarioOverview.sa_fletcher);
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_SwordAvailable, 0, scenarioOverview.sa_blacksmith);
			SetupScenarioActionsList();
			return;
		}
		if (num >= 0)
		{
			if (scenarioOverview.scenario_buildings_available[num] > 0)
			{
				scenarioOverview.scenario_buildings_available[num] = 0;
				SetBuildingAvailRow(num, state: false);
			}
			else
			{
				scenarioOverview.scenario_buildings_available[num] = 1;
				SetBuildingAvailRow(num, state: true);
			}
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_BuildingAvailable, num, scenarioOverview.scenario_buildings_available[num]);
			if (GameData.buildingAvailbleOrder[num] == 9 || GameData.buildingAvailbleOrder[num] == 10)
			{
				bool flag = scenarioOverview.scenario_buildings_available[barracksStoneRow] > 0 || scenarioOverview.scenario_buildings_available[barracksWoodRow] > 0;
				if (flag == barracksItemsShowing)
				{
					return;
				}
				barracksItemsShowing = flag;
				if (barracksItemsShowing)
				{
					for (int k = 0; k < 7; k++)
					{
						Enums.eChimps index = GameData.scenarioBarracksTroopsAvailableTypes[k];
						string text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, (int)index) + " ";
						ScenarioEditorRow scenarioEditorRow = new ScenarioEditorRow(this);
						scenarioEditorRow.DataValue = (-1 - k).ToString();
						if (GameData.Instance.scenarioOverview.sa_troop_availability[k] > 0)
						{
							scenarioEditorRow.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_ON);
							scenarioEditorRow.Text3HL = text;
							scenarioEditorRow.Text3 = "";
						}
						else
						{
							scenarioEditorRow.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_OFF);
							scenarioEditorRow.Text3 = text;
							scenarioEditorRow.Text3HL = "";
						}
						BuildingItems.Insert(barracksStoneRow + 1 + k, scenarioEditorRow);
						BuildingItemsDict[-1 - k] = scenarioEditorRow;
					}
				}
				else
				{
					for (int l = 0; l < 7; l++)
					{
						BuildingItems.RemoveAt(barracksStoneRow + 1);
					}
				}
			}
			else if (GameData.buildingAvailbleOrder[num] == 54)
			{
				int num3 = findBuildingItemRow(num);
				if (scenarioOverview.scenario_buildings_available[num] > 0)
				{
					fletcherItemsShowing = true;
					string text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, 18);
					ScenarioEditorRow scenarioEditorRow2 = new ScenarioEditorRow(this);
					scenarioEditorRow2.DataValue = "-20";
					if (scenarioOverview.sa_fletcher > 0)
					{
						scenarioEditorRow2.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_ON);
						scenarioEditorRow2.Text3HL = text2;
						scenarioEditorRow2.Text3 = "";
					}
					else
					{
						scenarioEditorRow2.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_OFF);
						scenarioEditorRow2.Text3 = text2;
						scenarioEditorRow2.Text3HL = "";
					}
					BuildingItems.Insert(num3 + 1, scenarioEditorRow2);
					BuildingItemsDict[-20] = scenarioEditorRow2;
				}
				else
				{
					fletcherItemsShowing = false;
					BuildingItems.RemoveAt(num3 + 1);
				}
			}
			else if (GameData.buildingAvailbleOrder[num] == 55)
			{
				int num4 = findBuildingItemRow(num);
				if (scenarioOverview.scenario_buildings_available[num] > 0)
				{
					poleturnerItemsShowing = true;
					string text3 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, 21);
					ScenarioEditorRow scenarioEditorRow3 = new ScenarioEditorRow(this);
					scenarioEditorRow3.DataValue = "-30";
					if (scenarioOverview.sa_poleturner > 0)
					{
						scenarioEditorRow3.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_ON);
						scenarioEditorRow3.Text3HL = text3;
						scenarioEditorRow3.Text3 = "";
					}
					else
					{
						scenarioEditorRow3.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_OFF);
						scenarioEditorRow3.Text3 = text3;
						scenarioEditorRow3.Text3HL = "";
					}
					BuildingItems.Insert(num4 + 1, scenarioEditorRow3);
					BuildingItemsDict[-30] = scenarioEditorRow3;
				}
				else
				{
					poleturnerItemsShowing = false;
					BuildingItems.RemoveAt(num4 + 1);
				}
			}
			else
			{
				if (GameData.buildingAvailbleOrder[num] != 51)
				{
					return;
				}
				int num5 = findBuildingItemRow(num);
				if (scenarioOverview.scenario_buildings_available[num] > 0)
				{
					blacksmithItemsShowing = true;
					string text4 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, 22);
					ScenarioEditorRow scenarioEditorRow4 = new ScenarioEditorRow(this);
					scenarioEditorRow4.DataValue = "-40";
					if (scenarioOverview.sa_blacksmith > 0)
					{
						scenarioEditorRow4.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_ON);
						scenarioEditorRow4.Text3HL = text4;
						scenarioEditorRow4.Text3 = "";
					}
					else
					{
						scenarioEditorRow4.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_OFF);
						scenarioEditorRow4.Text3 = text4;
						scenarioEditorRow4.Text3HL = "";
					}
					BuildingItems.Insert(num5 + 1, scenarioEditorRow4);
					BuildingItemsDict[-40] = scenarioEditorRow4;
				}
				else
				{
					blacksmithItemsShowing = false;
					BuildingItems.RemoveAt(num5 + 1);
				}
			}
			return;
		}
		if (num >= -10)
		{
			int num6 = -1 - num;
			if (scenarioOverview.sa_troop_availability[num6] > 0)
			{
				scenarioOverview.sa_troop_availability[num6] = 0;
				SetTroopAvailRow(num, state: false);
			}
			else
			{
				scenarioOverview.sa_troop_availability[num6] = 1;
				SetTroopAvailRow(num, state: true);
			}
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_TroopAvailable, num6, scenarioOverview.sa_troop_availability[num6]);
			return;
		}
		switch (num)
		{
		case -20:
			if (scenarioOverview.sa_fletcher > 0)
			{
				scenarioOverview.sa_fletcher = 0;
				SetTroopAvailRow(num, state: false);
			}
			else
			{
				scenarioOverview.sa_fletcher = 1;
				SetTroopAvailRow(num, state: true);
			}
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_XbowAvailable, 0, scenarioOverview.sa_fletcher);
			break;
		case -30:
			if (scenarioOverview.sa_poleturner > 0)
			{
				scenarioOverview.sa_poleturner = 0;
				SetTroopAvailRow(num, state: false);
			}
			else
			{
				scenarioOverview.sa_poleturner = 1;
				SetTroopAvailRow(num, state: true);
			}
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_MaceAvailable, 0, scenarioOverview.sa_poleturner);
			break;
		case -40:
			if (scenarioOverview.sa_blacksmith > 0)
			{
				scenarioOverview.sa_blacksmith = 0;
				SetTroopAvailRow(num, state: false);
			}
			else
			{
				scenarioOverview.sa_blacksmith = 1;
				SetTroopAvailRow(num, state: true);
			}
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_SwordAvailable, 0, scenarioOverview.sa_blacksmith);
			break;
		}
	}

	public void SetBuildingAvailRow(int row, bool state)
	{
		if (row >= BuildingItems.Count || !BuildingItemsDict.TryGetValue(row, out var value))
		{
			return;
		}
		if (state)
		{
			value.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_ON);
			if (value.Text1HL == "")
			{
				value.Text1HL = value.Text1;
				value.Text1 = "";
			}
		}
		else
		{
			value.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_OFF);
			if (value.Text1 == "")
			{
				value.Text1 = value.Text1HL;
				value.Text1HL = "";
			}
		}
	}

	public void SetTroopAvailRow(int row, bool state)
	{
		if (!BuildingItemsDict.TryGetValue(row, out var value))
		{
			return;
		}
		if (state)
		{
			value.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_ON);
			if (value.Text3HL == "")
			{
				value.Text3HL = value.Text3;
				value.Text3 = "";
			}
		}
		else
		{
			value.Text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_OFF);
			if (value.Text3 == "")
			{
				value.Text3 = value.Text3HL;
				value.Text3HL = "";
			}
		}
	}

	private int findBuildingItemRow(int ident)
	{
		string text = ident.ToString();
		for (int i = 0; i < BuildingItems.Count; i++)
		{
			if (BuildingItems[i].DataValue == text)
			{
				return i;
			}
		}
		return -1;
	}

	private void ResetListViewPosition(ListView lv)
	{
		if (lv.Items.Count > 0)
		{
			lv.ScrollIntoView(lv.Items[0]);
		}
	}

	public bool isEventPanelOpen()
	{
		return scenarioCurrentEvent != null;
	}

	private void SelectActionRow(int row)
	{
		string date = "";
		string body = "";
		string repeat = "";
		int entryType = 0;
		GameData.Instance.getScenarioEntryOverviewText(row, ref date, ref body, ref repeat, ref entryType);
		switch (entryType)
		{
		case 3:
			scenarioCurrentEvent = EngineInterface.GetScenarioEvent(row);
			scenarioCurrentInvasion = null;
			scenarioCurrentMessage = null;
			scenarioCurrentLine = row;
			if (scenarioCurrentEvent != null && (scenarioCurrentEvent.action == 2 || scenarioCurrentEvent.action == 27 || scenarioCurrentEvent.action == 26))
			{
				GameData.Instance.setMessagesFromMessageID(scenarioCurrentEvent.action_data);
			}
			PopulateEvent();
			MainViewModel.Instance.ButtonScenarioView("7", fromButton: false);
			break;
		case 1:
			scenarioCurrentInvasion = EngineInterface.GetScenarioInvasion(row);
			scenarioCurrentEvent = null;
			scenarioCurrentMessage = null;
			scenarioCurrentLine = row;
			MainViewModel.Instance.ButtonScenarioView("5", fromButton: false);
			PopulateInvasion();
			break;
		case 2:
			scenarioCurrentMessage = EngineInterface.GetScenarioMessage(row);
			scenarioCurrentInvasion = null;
			scenarioCurrentEvent = null;
			scenarioCurrentLine = row;
			if (scenarioCurrentMessage != null)
			{
				GameData.Instance.setMessagesFromMessageID(scenarioCurrentMessage.message_id);
			}
			MainViewModel.Instance.ButtonScenarioView("6", fromButton: false);
			PopulateMessage();
			break;
		}
	}

	private void NewInvasion()
	{
		scenarioCurrentEvent = null;
		scenarioCurrentMessage = null;
		scenarioCurrentInvasion = EngineInterface.CreateNewScenarioInvasion(ref scenarioCurrentLine);
		PopulateInvasion();
	}

	private void NewMessage()
	{
		scenarioCurrentInvasion = null;
		scenarioCurrentEvent = null;
		scenarioCurrentMessage = EngineInterface.CreateNewScenarioMessage(ref scenarioCurrentLine);
		GameData.Instance.message_catagory1 = 3;
		GameData.Instance.message_catagory2 = 1;
		GameData.Instance.message_catagory3 = 0;
		PopulateMessage();
	}

	private void NewEvent()
	{
		scenarioCurrentInvasion = null;
		scenarioCurrentMessage = null;
		scenarioCurrentEvent = EngineInterface.CreateNewScenarioEvent(ref scenarioCurrentLine);
		GameData.Instance.message_catagory1 = 3;
		GameData.Instance.message_catagory2 = 1;
		GameData.Instance.message_catagory3 = 0;
		PopulateEvent();
	}

	public void PopulateInvasion(bool fromUpdate = false)
	{
		if (scenarioCurrentInvasion == null)
		{
			return;
		}
		if (!fromUpdate)
		{
			MainViewModel.Instance.ScenarioInvasionYearText = scenarioCurrentInvasion.year.ToString();
			for (int i = 1; i < 5; i++)
			{
				MainViewModel.Instance.InvasionButtonHighlight[i] = false;
			}
			MainViewModel.Instance.InvasionButtonHighlight[scenarioCurrentInvasion.from + 1] = true;
			RefInvasionRepeatSlider.Value = scenarioCurrentInvasion.repeat;
			for (int j = 0; j < 11; j++)
			{
				MainViewModel.Instance.InvasionSpawnMarkersHighlight[j] = false;
			}
			if (scenarioCurrentInvasion.markerID < 0)
			{
				scenarioCurrentInvasion.markerID = 0;
			}
			else if (scenarioCurrentInvasion.markerID > 10)
			{
				scenarioCurrentInvasion.markerID = 10;
			}
			MainViewModel.Instance.InvasionSpawnMarkersHighlight[scenarioCurrentInvasion.markerID] = true;
		}
		MainViewModel.Instance.ScenarioInvasionMonthText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MONTHS, scenarioCurrentInvasion.month);
		int num = int.Parse(MainViewModel.Instance.ScenarioInvasionYearText, Director.defaultCulture);
		if (num != scenarioCurrentInvasion.year)
		{
			scenarioCurrentInvasion.year = num;
		}
		int num2 = 0;
		for (int k = 0; k < scenarioCurrentInvasion._size.Length; k++)
		{
			num2 += scenarioCurrentInvasion._size[k];
		}
		scenarioCurrentInvasion.total = num2;
		MainViewModel.Instance.ScenarioInvasionTotalTroopsText = num2.ToString();
		MainViewModel.Instance.ScenarioInvasionRepeatText = scenarioCurrentInvasion.repeat.ToString();
		int invasionSizeTroopTypeFromIndex = GameData.getInvasionSizeTroopTypeFromIndex(MainViewModel.Instance.ScenarioInvasionSizeType);
		MainViewModel.Instance.ScenarioInvasionSizeText = scenarioCurrentInvasion._size[MainViewModel.Instance.ScenarioInvasionSizeType] + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, invasionSizeTroopTypeFromIndex);
		for (int l = 0; l < 16; l++)
		{
			if (scenarioCurrentInvasion._size[l] > 0)
			{
				MainViewModel.Instance.InvasionSize[l] = scenarioCurrentInvasion._size[l].ToString();
			}
			else
			{
				MainViewModel.Instance.InvasionSize[l] = "";
			}
		}
	}

	public void PopulateMessage(bool fromUpdate = false)
	{
		if (scenarioCurrentMessage == null && scenarioCurrentEvent == null)
		{
			return;
		}
		if (!fromUpdate)
		{
			if (scenarioCurrentMessage != null)
			{
				MainViewModel.Instance.ScenarioMessageYearText = scenarioCurrentMessage.year.ToString();
			}
			lastMessageCat1 = -1;
			lastMessageCat2 = -1;
			lastMessageCat3 = -1;
			GameData.Instance.rescan_current_list();
			DrawMessageList();
		}
		if (scenarioCurrentMessage != null)
		{
			MainViewModel.Instance.ScenarioMessageMonthText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MONTHS, scenarioCurrentMessage.month);
			int num = int.Parse(MainViewModel.Instance.ScenarioMessageYearText, Director.defaultCulture);
			if (num != scenarioCurrentMessage.year)
			{
				scenarioCurrentMessage.year = num;
			}
		}
		if (GameData.Instance.message_catagory1 == lastMessageCat1 && GameData.Instance.message_catagory2 == lastMessageCat2 && GameData.Instance.message_catagory3 == lastMessageCat3)
		{
			return;
		}
		lastMessageCat1 = GameData.Instance.message_catagory1;
		lastMessageCat2 = GameData.Instance.message_catagory2;
		lastMessageCat3 = GameData.Instance.message_catagory3;
		for (int i = 0; i < 20; i++)
		{
			MainViewModel.Instance.MessageButtonHighlight[i] = false;
		}
		if (GameData.Instance.message_catagory1 == 5)
		{
			MainViewModel.Instance.MessageButtonHighlight[6] = true;
			RefButtonMessage_Taunt.Visibility = Visibility.Hidden;
			RefButtonMessage_Anger.Visibility = Visibility.Hidden;
			RefButtonMessage_Pleading.Visibility = Visibility.Hidden;
			RefButtonMessage_Victory.Visibility = Visibility.Hidden;
			RefButtonMessage_Civil.Visibility = Visibility.Hidden;
			RefButtonMessage_Military.Visibility = Visibility.Hidden;
			RefMessageImage_Rat.Visibility = Visibility.Hidden;
			RefMessageImage_Snake.Visibility = Visibility.Hidden;
			RefMessageImage_Pig.Visibility = Visibility.Hidden;
			RefMessageImage_Wolf.Visibility = Visibility.Hidden;
			RefButtonMessage_New1.Visibility = Visibility.Visible;
			RefButtonMessage_New2.Visibility = Visibility.Visible;
			RefButtonMessage_New3.Visibility = Visibility.Visible;
			RefButtonMessage_New4.Visibility = Visibility.Visible;
			MainViewModel.Instance.MessageButtonHighlight[16 + GameData.Instance.message_catagory2] = true;
		}
		else if (GameData.Instance.message_catagory1 == 3)
		{
			MainViewModel.Instance.MessageButtonHighlight[5] = true;
			RefButtonMessage_Taunt.Visibility = Visibility.Hidden;
			RefButtonMessage_Anger.Visibility = Visibility.Hidden;
			RefButtonMessage_Pleading.Visibility = Visibility.Hidden;
			RefButtonMessage_Victory.Visibility = Visibility.Hidden;
			RefButtonMessage_Civil.Visibility = Visibility.Visible;
			RefButtonMessage_Military.Visibility = Visibility.Visible;
			RefMessageImage_Rat.Visibility = Visibility.Hidden;
			RefMessageImage_Snake.Visibility = Visibility.Hidden;
			RefMessageImage_Pig.Visibility = Visibility.Hidden;
			RefMessageImage_Wolf.Visibility = Visibility.Hidden;
			RefButtonMessage_New1.Visibility = Visibility.Hidden;
			RefButtonMessage_New2.Visibility = Visibility.Hidden;
			RefButtonMessage_New3.Visibility = Visibility.Hidden;
			RefButtonMessage_New4.Visibility = Visibility.Hidden;
			if (GameData.Instance.message_catagory2 == 1)
			{
				MainViewModel.Instance.MessageButtonHighlight[14] = true;
				MainViewModel.Instance.MessageButtonHighlight[15] = false;
			}
			else
			{
				MainViewModel.Instance.MessageButtonHighlight[14] = false;
				MainViewModel.Instance.MessageButtonHighlight[15] = true;
			}
		}
		else if (GameData.Instance.message_catagory1 == 4)
		{
			RefMessageImage_Rat.Visibility = Visibility.Hidden;
			RefMessageImage_Snake.Visibility = Visibility.Hidden;
			RefMessageImage_Pig.Visibility = Visibility.Hidden;
			RefMessageImage_Wolf.Visibility = Visibility.Hidden;
			switch (GameData.Instance.message_catagory2)
			{
			case 0:
				MainViewModel.Instance.MessageButtonHighlight[1] = true;
				RefMessageImage_Rat.Visibility = Visibility.Visible;
				break;
			case 1:
				MainViewModel.Instance.MessageButtonHighlight[2] = true;
				RefMessageImage_Snake.Visibility = Visibility.Visible;
				break;
			case 2:
				MainViewModel.Instance.MessageButtonHighlight[3] = true;
				RefMessageImage_Pig.Visibility = Visibility.Visible;
				break;
			case 3:
				MainViewModel.Instance.MessageButtonHighlight[4] = true;
				RefMessageImage_Wolf.Visibility = Visibility.Visible;
				break;
			}
			RefButtonMessage_Taunt.Visibility = Visibility.Visible;
			RefButtonMessage_Anger.Visibility = Visibility.Visible;
			RefButtonMessage_Pleading.Visibility = Visibility.Visible;
			RefButtonMessage_Victory.Visibility = Visibility.Visible;
			RefButtonMessage_Civil.Visibility = Visibility.Hidden;
			RefButtonMessage_Military.Visibility = Visibility.Hidden;
			RefButtonMessage_New1.Visibility = Visibility.Hidden;
			RefButtonMessage_New2.Visibility = Visibility.Hidden;
			RefButtonMessage_New3.Visibility = Visibility.Hidden;
			RefButtonMessage_New4.Visibility = Visibility.Hidden;
			switch (GameData.Instance.message_catagory3)
			{
			case 0:
				MainViewModel.Instance.MessageButtonHighlight[10] = true;
				break;
			case 1:
				MainViewModel.Instance.MessageButtonHighlight[11] = true;
				break;
			case 2:
				MainViewModel.Instance.MessageButtonHighlight[12] = true;
				break;
			case 3:
				MainViewModel.Instance.MessageButtonHighlight[13] = true;
				break;
			}
		}
	}

	public void PopulateEvent(bool fromUpdate = false)
	{
		if (scenarioCurrentEvent == null)
		{
			return;
		}
		if (!fromUpdate)
		{
			MainViewModel.Instance.ScenarioEventYearText = scenarioCurrentEvent.year.ToString();
			int num = 0;
			for (int i = 0; i < 40; i++)
			{
				if (scenarioCurrentEvent.event_value[i].onoff <= 0)
				{
					continue;
				}
				string value = "";
				int num2 = 106 + i;
				if (num2 > 125)
				{
					num2 += 64;
				}
				if (num2 == 111 || num2 == 112 || num2 == 113 || num2 == 123)
				{
					num2 = 113;
				}
				string text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, num2);
				if (num2 == 114)
				{
					text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 8);
				}
				if (GameData.start_event_types[i] == 1)
				{
					text = text + " (" + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, GameData.start_event_goods_text[scenarioCurrentEvent.event_value[i].type]) + ")";
				}
				else if (GameData.start_event_types[i] == 2)
				{
					value = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, GameData.lord_killed_list[scenarioCurrentEvent.event_value[i].type]);
				}
				else if (GameData.start_event_types[i] == 3)
				{
					if (scenarioCurrentEvent.event_value[i].type < 0)
					{
						scenarioCurrentEvent.event_value[i].type = 0;
					}
					value = ((scenarioCurrentEvent.event_value[i].type == 31) ? Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 166) : Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 132 + scenarioCurrentEvent.event_value[i].type));
				}
				if (GameData.start_event_max[i] != 0)
				{
					value = (scenarioCurrentEvent.event_value[i].value * GameData.start_event_multiplier[i]).ToString();
				}
				MainViewModel.Instance.EventTextList[num + 1] = text;
				MainViewModel.Instance.EventTextList[num + 11] = value;
				num++;
				if (num >= 8)
				{
					break;
				}
			}
			if (num < 8)
			{
				for (int j = num; j < 8; j++)
				{
					MainViewModel.Instance.EventTextList[j + 1] = "";
					MainViewModel.Instance.EventTextList[j + 11] = "";
				}
			}
			if (scenarioCurrentEvent.action >= 0)
			{
				int num2 = 128 + scenarioCurrentEvent.action;
				if (num2 > 155)
				{
					num2 += 21;
				}
				if (num2 == 148)
				{
					num2 = 146;
				}
				else
				{
					_ = 146;
				}
				string text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, num2);
				if (scenarioCurrentEvent.action == 2 || scenarioCurrentEvent.action == 27 || scenarioCurrentEvent.action == 26)
				{
					if (scenarioCurrentEvent.action_data != -1)
					{
						text2 = text2 + " " + Translate.Instance.getMessageLibraryText(scenarioCurrentEvent.action_data);
					}
				}
				else if (scenarioCurrentEvent.action == 33)
				{
					text2 = ((scenarioCurrentEvent.action_data_marker != 0) ? (text2 + " : " + scenarioCurrentEvent.action_data_marker) : (text2 + " : " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 14)));
				}
				else if (scenarioCurrentEvent.action == 11 || scenarioCurrentEvent.action == 17 || scenarioCurrentEvent.action == 4 || scenarioCurrentEvent.action == 18 || scenarioCurrentEvent.action == 20 || scenarioCurrentEvent.action == 30)
				{
					text2 = text2 + " : " + scenarioCurrentEvent.action_data;
				}
				else if (scenarioCurrentEvent.action == 32)
				{
					text2 = text2 + " : " + scenarioCurrentEvent.action_data_marker + " / " + getReinforcementsName(scenarioCurrentEvent.action_data_reinforcement);
				}
				else if (scenarioCurrentEvent.action == 31)
				{
					text2 = text2 + " : " + scenarioCurrentEvent.action_data_marker + "  " + getAllegianceTeamName(scenarioCurrentEvent.action_data_reinforcement);
				}
				MainViewModel.Instance.EventTextList[0] = text2;
			}
			else
			{
				MainViewModel.Instance.EventTextList[0] = "";
			}
		}
		MainViewModel.Instance.ScenarioEventMonthText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MONTHS, scenarioCurrentEvent.month);
		int num3 = int.Parse(MainViewModel.Instance.ScenarioEventYearText, Director.defaultCulture);
		if (num3 != scenarioCurrentEvent.year)
		{
			scenarioCurrentEvent.year = num3;
		}
	}

	public void PopulateEventConditions(bool fromUpdate = false)
	{
		if (scenarioCurrentEvent == null)
		{
			return;
		}
		if (!fromUpdate)
		{
			MainViewModel.Instance.ScenarioEventConditionTitle = "";
			ObservableCollection<ScenarioEditorRow> observableCollection = new ObservableCollection<ScenarioEditorRow>();
			for (int i = 0; i < GameData.scenarioEventsOrder.Length; i++)
			{
				int num = GameData.scenarioEventsOrder[i];
				int num2 = num;
				if (num2 != 209)
				{
					if (num >= 190)
					{
						num -= 64;
					}
					num -= 106;
					ScenarioEditorRow scenarioEditorRow = new ScenarioEditorRow(this);
					scenarioEditorRow.DataValue = num.ToString();
					scenarioEditorRow.DataValue2 = num2;
					activeEventConditionRow = scenarioEditorRow;
					UpdateEventConditionRow();
					observableCollection.Add(scenarioEditorRow);
				}
			}
			activeEventConditionRow = null;
			RefScenarioEventConditionList.ItemsSource = observableCollection;
			MainViewModel.Instance.ConditionValueNameText = "";
			EventConditionItems = observableCollection;
			ManageEventConditionWinTimer();
			PopulateEventConditionLeftSide();
		}
		if (scenarioCurrentEvent.and_or > 0)
		{
			MainViewModel.Instance.ScenarionEventConditionAndOrText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_ALL_OF_THESE);
		}
		else
		{
			MainViewModel.Instance.ScenarionEventConditionAndOrText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_ANY_OF_THESE);
		}
	}

	private void ManageEventConditionWinTimer()
	{
		int num = 209;
		int num2 = num;
		if (num >= 190)
		{
			num -= 64;
		}
		num -= 106;
		bool flag = true;
		if (num2 == 209)
		{
			flag = false;
			if (scenarioCurrentEvent.event_value[1].onoff > 0)
			{
				flag = true;
			}
			if (scenarioCurrentEvent.event_value[4].onoff > 0)
			{
				flag = true;
			}
			if (scenarioCurrentEvent.event_value[5].onoff > 0)
			{
				flag = true;
			}
			if (scenarioCurrentEvent.event_value[6].onoff > 0)
			{
				flag = true;
			}
			if (scenarioCurrentEvent.event_value[7].onoff > 0)
			{
				flag = true;
			}
			if (scenarioCurrentEvent.event_value[17].onoff > 0)
			{
				flag = true;
			}
			if (scenarioCurrentEvent.event_value[20].onoff > 0)
			{
				flag = true;
			}
			if (scenarioCurrentEvent.event_value[21].onoff > 0)
			{
				flag = true;
			}
			if (scenarioCurrentEvent.event_value[22].onoff > 0)
			{
				flag = true;
			}
			if (scenarioCurrentEvent.event_value[23].onoff > 0)
			{
				flag = true;
			}
		}
		if (flag)
		{
			ScenarioEditorRow scenarioEditorRow = null;
			foreach (ScenarioEditorRow eventConditionItem in EventConditionItems)
			{
				if (eventConditionItem.DataValue2 == num2)
				{
					scenarioEditorRow = eventConditionItem;
					break;
				}
			}
			if (scenarioEditorRow == null)
			{
				scenarioEditorRow = new ScenarioEditorRow(this);
				EventConditionItems.Add(scenarioEditorRow);
			}
			scenarioEditorRow.DataValue = num.ToString();
			scenarioEditorRow.DataValue2 = num2;
			ScenarioEditorRow scenarioEditorRow2 = activeEventConditionRow;
			activeEventConditionRow = scenarioEditorRow;
			UpdateEventConditionRow();
			activeEventConditionRow = scenarioEditorRow2;
			return;
		}
		ScenarioEditorRow scenarioEditorRow3 = null;
		foreach (ScenarioEditorRow eventConditionItem2 in EventConditionItems)
		{
			if (eventConditionItem2.DataValue2 == num2)
			{
				scenarioEditorRow3 = eventConditionItem2;
				break;
			}
		}
		if (scenarioEditorRow3 != null)
		{
			EventConditionItems.Remove(scenarioEditorRow3);
		}
	}

	private void PopulateEventConditionLeftSide()
	{
		if (activeEventConditionRow == null)
		{
			MainViewModel.Instance.ScenarioEventConditionTitle = "";
			MainViewModel.Instance.ScenarionEventConditionToggleVisibleBool = false;
			MainViewModel.Instance.ScenarionEventConditionToggleColourVisibleBool = false;
			MainViewModel.Instance.ScenarioEventConditionValueVisibleBool = false;
			MainViewModel.Instance.ScenarionEventConditionOnOffVisibleBool = false;
			return;
		}
		int num = activeEventConditionRow.DataValue2;
		if (num == 111 || num == 112 || num == 113 || num == 123)
		{
			num = 113;
		}
		string scenarioEventConditionTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, num);
		if (num == 114)
		{
			scenarioEventConditionTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 8);
		}
		MainViewModel.Instance.ScenarioEventConditionTitle = scenarioEventConditionTitle;
		int num2 = int.Parse(activeEventConditionRow.DataValue, Director.defaultCulture);
		if (GameData.start_event_max[num2] != 0)
		{
			MainViewModel.Instance.ScenarioEventConditionValueVisibleBool = true;
			int maxValue = GameData.start_event_max[num2];
			int num3 = scenarioCurrentEvent.event_value[num2].value;
			int num4 = GameData.start_event_min[num2];
			int freq = maxValue / 10;
			if (freq < 1)
			{
				freq = 1;
			}
			if (num3 < num4)
			{
				num3 = num4;
			}
			else if (num3 > maxValue)
			{
				num3 = maxValue;
			}
			RefConditionValueSlider.Value = getLogSliderValue(num3, num4, ref maxValue, ref freq);
			MainViewModel.Instance.ConditionValueMax = maxValue;
			MainViewModel.Instance.ConditionValueFreq = freq;
			MainViewModel.Instance.ConditionValueText = num3.ToString();
		}
		else
		{
			MainViewModel.Instance.ScenarioEventConditionValueVisibleBool = false;
		}
		if (GameData.start_event_types[num2] == 1)
		{
			string scenarionEventConditionToggleText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, GameData.start_event_goods_text[scenarioCurrentEvent.event_value[num2].type]);
			MainViewModel.Instance.ScenarionEventConditionToggleText = scenarionEventConditionToggleText;
			MainViewModel.Instance.ScenarionEventConditionToggleVisibleBool = true;
			MainViewModel.Instance.ScenarionEventConditionToggleColourVisibleBool = false;
		}
		else if (GameData.start_event_types[num2] == 2)
		{
			string scenarionEventConditionToggleText2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, GameData.lord_killed_list[scenarioCurrentEvent.event_value[num2].type]);
			MainViewModel.Instance.ScenarionEventConditionToggleText = scenarionEventConditionToggleText2;
			MainViewModel.Instance.ScenarionEventConditionToggleVisibleBool = true;
			MainViewModel.Instance.ScenarionEventConditionToggleColourVisibleBool = true;
			MainViewModel.Instance.ScenarionEventConditionToggleColour = lordColours[scenarioCurrentEvent.event_value[num2].type];
		}
		else if (GameData.start_event_types[num2] == 3)
		{
			if (scenarioCurrentEvent.event_value[num2].type < 0)
			{
				scenarioCurrentEvent.event_value[num2].type = 0;
			}
			string scenarionEventConditionToggleText3 = ((scenarioCurrentEvent.event_value[num2].type == 31) ? Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 166) : Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 132 + scenarioCurrentEvent.event_value[num2].type));
			MainViewModel.Instance.ScenarionEventConditionToggleText = scenarionEventConditionToggleText3;
			MainViewModel.Instance.ScenarionEventConditionToggleVisibleBool = true;
			MainViewModel.Instance.ScenarionEventConditionToggleColourVisibleBool = false;
		}
		else
		{
			MainViewModel.Instance.ScenarionEventConditionToggleVisibleBool = false;
			MainViewModel.Instance.ScenarionEventConditionToggleColourVisibleBool = false;
		}
		MainViewModel.Instance.ScenarionEventConditionOnOffVisibleBool = true;
		if (scenarioCurrentEvent.event_value[num2].onoff > 0)
		{
			MainViewModel.Instance.ScenarionEventConditionOnOffText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_ACTIVE);
		}
		else
		{
			MainViewModel.Instance.ScenarionEventConditionOnOffText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_INACTIVE);
		}
		UpdateEventConditionRow();
	}

	public void EventConditionToggle()
	{
		if (activeEventConditionRow == null)
		{
			return;
		}
		int num = int.Parse(activeEventConditionRow.DataValue, Director.defaultCulture);
		if (GameData.start_event_types[num] == 1)
		{
			int num2 = scenarioCurrentEvent.event_value[num].type;
			if (num2 == 0)
			{
				num2 = (scenarioCurrentEvent.event_value[num].type = 10);
			}
			int i;
			for (i = 0; GameData.start_event_goods[num][i] != num2; i++)
			{
			}
			i++;
			if (GameData.start_event_goods[num][i] == -1)
			{
				scenarioCurrentEvent.event_value[num].type = (byte)GameData.start_event_goods[num][0];
			}
			else
			{
				scenarioCurrentEvent.event_value[num].type = (byte)GameData.start_event_goods[num][i];
			}
			string scenarionEventConditionToggleText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, GameData.start_event_goods_text[scenarioCurrentEvent.event_value[num].type]);
			MainViewModel.Instance.ScenarionEventConditionToggleText = scenarionEventConditionToggleText;
			UpdateEventConditionRow();
		}
		else if (GameData.start_event_types[num] == 2)
		{
			scenarioCurrentEvent.event_value[num].type++;
			if (scenarioCurrentEvent.event_value[num].type >= GameData.lord_killed_list.Length)
			{
				scenarioCurrentEvent.event_value[num].type = 0;
			}
			string scenarionEventConditionToggleText2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, GameData.lord_killed_list[scenarioCurrentEvent.event_value[num].type]);
			MainViewModel.Instance.ScenarionEventConditionToggleText = scenarionEventConditionToggleText2;
			MainViewModel.Instance.ScenarionEventConditionToggleColour = lordColours[scenarioCurrentEvent.event_value[num].type];
			UpdateEventConditionRow();
		}
		else if (GameData.start_event_types[num] == 3)
		{
			if (scenarioCurrentEvent.event_value[num].type < 0)
			{
				scenarioCurrentEvent.event_value[num].type = 0;
			}
			scenarioCurrentEvent.event_value[num].type++;
			if (scenarioCurrentEvent.event_value[num].type >= 32)
			{
				scenarioCurrentEvent.event_value[num].type = 0;
			}
			string scenarionEventConditionToggleText3 = ((scenarioCurrentEvent.event_value[num].type == 31) ? Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 166) : Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 132 + scenarioCurrentEvent.event_value[num].type));
			MainViewModel.Instance.ScenarionEventConditionToggleText = scenarionEventConditionToggleText3;
			UpdateEventConditionRow();
		}
	}

	public void EventConditionOnOff()
	{
		if (activeEventConditionRow != null)
		{
			int num = int.Parse(activeEventConditionRow.DataValue, Director.defaultCulture);
			if (scenarioCurrentEvent.event_value[num].onoff > 0)
			{
				scenarioCurrentEvent.event_value[num].onoff = 0;
			}
			else
			{
				scenarioCurrentEvent.event_value[num].onoff = 1;
			}
			ManageEventConditionWinTimer();
			PopulateEventConditionLeftSide();
			UpdateEventConditionRow();
		}
	}

	private void UpdateEventConditionRow()
	{
		if (activeEventConditionRow == null)
		{
			return;
		}
		int num = int.Parse(activeEventConditionRow.DataValue, Director.defaultCulture);
		int num2 = activeEventConditionRow.DataValue2;
		if (num2 == 111 || num2 == 112 || num2 == 113 || num2 == 123)
		{
			num2 = 113;
		}
		string text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, num2);
		if (num2 == 114)
		{
			text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 8);
		}
		if (scenarioCurrentEvent.event_value[num].onoff > 0)
		{
			activeEventConditionRow.BorderVisibility = Visibility.Visible;
			string text2 = "";
			if (GameData.start_event_max[num] != 0)
			{
				if (GameData.start_event_types[num] == 1)
				{
					text = text + " (" + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, GameData.start_event_goods_text[scenarioCurrentEvent.event_value[num].type]) + ")";
				}
				text2 = (scenarioCurrentEvent.event_value[num].value * GameData.start_event_multiplier[num]).ToString();
			}
			if (GameData.start_event_types[num] == 2)
			{
				text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, GameData.lord_killed_list[scenarioCurrentEvent.event_value[num].type]);
			}
			activeEventConditionRow.Text2 = text2;
		}
		else
		{
			activeEventConditionRow.BorderVisibility = Visibility.Hidden;
			activeEventConditionRow.Text2 = "";
		}
		if (num2 == 194 || num2 == 196 || num2 == 197 || num2 == 198 || num2 == 200)
		{
			MainViewModel.Instance.ConditionValueNameText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 24);
		}
		else
		{
			MainViewModel.Instance.ConditionValueNameText = "";
		}
		activeEventConditionRow.Text1 = text;
	}

	public void EventAndOr()
	{
		if (scenarioCurrentEvent != null)
		{
			if (scenarioCurrentEvent.and_or > 0)
			{
				scenarioCurrentEvent.and_or = 0;
			}
			else
			{
				scenarioCurrentEvent.and_or = 1;
			}
		}
	}

	public void PopulateEventActions(bool fromUpdate = false)
	{
		if (scenarioCurrentEvent == null)
		{
			return;
		}
		if (!fromUpdate)
		{
			List<ScenarioEditorRow> list = new List<ScenarioEditorRow>();
			activeEventActionRow = null;
			for (int i = 0; i < GameData.scenarioActionsOrder.Length; i++)
			{
				ScenarioEditorRow scenarioEditorRow = new ScenarioEditorRow(this);
				int num = GameData.scenarioActionsOrder[i];
				if (num > 155)
				{
					num -= 21;
				}
				num -= 128;
				scenarioEditorRow.DataValue2 = i;
				int num2 = GameData.scenarioActionsOrder[i];
				string text = "";
				switch (num2)
				{
				case 148:
					num2 = 146;
					text = text + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 192);
					break;
				case 146:
					text = text + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 193);
					break;
				}
				string text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, num2) + text;
				if (num == scenarioCurrentEvent.action)
				{
					activeEventActionRow = scenarioEditorRow;
					UpdateEventActionRow();
					scenarioEditorRow.BorderVisibility = Visibility.Visible;
				}
				else
				{
					scenarioEditorRow.BorderVisibility = Visibility.Hidden;
				}
				scenarioEditorRow.DataValue = num.ToString();
				scenarioEditorRow.Text1 = text2;
				list.Add(scenarioEditorRow);
			}
			PopulateEventActionLeftSide();
			RefScenarioEventActionList.ItemsSource = list;
			if (activeEventActionRow != null)
			{
				RefScenarioEventActionList.ScrollIntoView(activeEventActionRow);
			}
		}
		MainViewModel.Instance.ActionRepeatMonthsText = scenarioCurrentEvent.repeat.ToString();
		if (scenarioCurrentEvent.repeat_count != 10)
		{
			MainViewModel.Instance.ActionRepeatText = scenarioCurrentEvent.repeat_count.ToString();
		}
		else
		{
			MainViewModel.Instance.ActionRepeatText = "∞";
		}
		if (GameData.scenarioActionsOrder[activeEventActionRow.DataValue2] == 178)
		{
			if (!FatControler.turkish)
			{
				MainViewModel.Instance.ActionValueText = scenarioCurrentEvent.action_data + "%";
			}
			else
			{
				MainViewModel.Instance.ActionValueText = "%" + scenarioCurrentEvent.action_data;
			}
		}
		else if (GameData.scenarioActionsOrder[activeEventActionRow.DataValue2] == 181)
		{
			MainViewModel.Instance.ActionValueText = scenarioCurrentEvent.action_data_marker.ToString();
			MainViewModel.Instance.ActionValue2Text = getReinforcementsName(scenarioCurrentEvent.action_data_reinforcement);
		}
		else if (GameData.scenarioActionsOrder[activeEventActionRow.DataValue2] == 180)
		{
			MainViewModel.Instance.ActionValueText = scenarioCurrentEvent.action_data_marker.ToString();
			MainViewModel.Instance.ActionValue2Text = getAllegianceTeamName(scenarioCurrentEvent.action_data_reinforcement);
		}
		else if (GameData.scenarioActionsOrder[activeEventActionRow.DataValue2] == 182)
		{
			if (scenarioCurrentEvent.action_data_marker == 0)
			{
				MainViewModel.Instance.ActionValueText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 14);
			}
			else
			{
				MainViewModel.Instance.ActionValueText = scenarioCurrentEvent.action_data_marker.ToString();
			}
		}
		else
		{
			MainViewModel.Instance.ActionValueText = scenarioCurrentEvent.action_data.ToString();
		}
	}

	private void PopulateEventActionLeftSide()
	{
		int dataValue = activeEventActionRow.DataValue2;
		int num = GameData.scenarioActionsOrder[dataValue];
		string text = "";
		switch (num)
		{
		case 148:
			num = 146;
			text = text + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 192);
			break;
		case 146:
			text = text + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 193);
			break;
		}
		string scenarioEventActionTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, num) + text;
		MainViewModel.Instance.ScenarioEventActionTitle = scenarioEventActionTitle;
		MainViewModel.Instance.ActionValueNameText = "";
		MainViewModel.Instance.ActionValue2NameText = "";
		switch (scenarioCurrentEvent.action)
		{
		case 5:
		case 11:
		case 12:
		case 13:
		case 14:
		case 15:
		case 16:
		case 17:
		case 18:
		case 19:
		case 20:
		case 29:
		case 30:
			MainViewModel.Instance.ScenarioEventActionRepeatVisibleBool = true;
			break;
		case 31:
			MainViewModel.Instance.ActionValueNameText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 24);
			MainViewModel.Instance.ActionValue2NameText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, 30);
			MainViewModel.Instance.ScenarioEventActionRepeatVisibleBool = false;
			break;
		case 32:
			MainViewModel.Instance.ActionValueNameText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 24);
			MainViewModel.Instance.ActionValue2NameText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 26);
			MainViewModel.Instance.ScenarioEventActionRepeatVisibleBool = false;
			break;
		case 33:
			MainViewModel.Instance.ActionValueNameText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 24);
			break;
		default:
			MainViewModel.Instance.ScenarioEventActionRepeatVisibleBool = false;
			break;
		}
		MainViewModel.Instance.ActionValueMin = 1;
		switch (GameData.scenarioActionsOrder[dataValue])
		{
		case 132:
		case 139:
		case 145:
		case 146:
		case 148:
		case 178:
		case 179:
		{
			int num2 = 0;
			switch (GameData.scenarioActionsOrder[dataValue])
			{
			case 132:
				num2 = 10;
				break;
			case 139:
				num2 = 10;
				break;
			case 145:
				num2 = 10;
				break;
			case 146:
				num2 = 50;
				break;
			case 148:
				num2 = 50;
				break;
			case 178:
				num2 = 100;
				break;
			case 179:
				num2 = 10;
				break;
			}
			MainViewModel.Instance.ScenarioEventActionMessageVisibleBool = false;
			MainViewModel.Instance.ScenarioEventActionValueVisibleBool = true;
			MainViewModel.Instance.ScenarioEventActionValue2VisibleBool = false;
			MainViewModel.Instance.ActionValueMax = num2;
			int num3 = num2 / 10;
			if (num3 < 1)
			{
				num3 = 1;
			}
			MainViewModel.Instance.ActionValueFreq = num3;
			if (scenarioCurrentEvent.action_data <= 0)
			{
				scenarioCurrentEvent.action_data = 1;
			}
			MainViewModel.Instance.ScenarioEventActionMessage = "";
			RefActionValueSlider.Value = scenarioCurrentEvent.action_data;
			break;
		}
		case 130:
		case 154:
		case 155:
			MainViewModel.Instance.ScenarioEventActionMessageVisibleBool = true;
			MainViewModel.Instance.ScenarioEventActionValueVisibleBool = false;
			MainViewModel.Instance.ScenarioEventActionValue2VisibleBool = false;
			if (scenarioCurrentEvent.action_data > 0)
			{
				MainViewModel.Instance.ScenarioEventActionMessage = Translate.Instance.getMessageLibraryText(scenarioCurrentEvent.action_data);
			}
			else
			{
				MainViewModel.Instance.ScenarioEventActionMessage = "";
			}
			break;
		case 180:
		case 181:
		{
			int actionValueMax2 = 10;
			MainViewModel.Instance.ScenarioEventActionMessageVisibleBool = false;
			MainViewModel.Instance.ScenarioEventActionValueVisibleBool = true;
			MainViewModel.Instance.ScenarioEventActionValue2VisibleBool = true;
			MainViewModel.Instance.ActionValueMax = actionValueMax2;
			MainViewModel.Instance.ActionValueFreq = 1;
			if (scenarioCurrentEvent.action_data_marker <= 0)
			{
				scenarioCurrentEvent.action_data_marker = 1;
			}
			RefActionValueSlider.Value = scenarioCurrentEvent.action_data_marker;
			if (GameData.scenarioActionsOrder[dataValue] == 180)
			{
				MainViewModel.Instance.ActionValue2Min = 2;
				MainViewModel.Instance.ActionValue2Max = 8;
			}
			else
			{
				MainViewModel.Instance.ActionValue2Min = 1;
				MainViewModel.Instance.ActionValue2Max = 34;
			}
			MainViewModel.Instance.ActionValue2Freq = 1;
			if (scenarioCurrentEvent.action_data_reinforcement <= 0)
			{
				scenarioCurrentEvent.action_data_reinforcement = 1;
			}
			RefActionValue2Slider.Value = scenarioCurrentEvent.action_data_reinforcement;
			MainViewModel.Instance.ScenarioEventActionMessage = "";
			break;
		}
		case 182:
		{
			int actionValueMax = 10;
			MainViewModel.Instance.ScenarioEventActionMessageVisibleBool = false;
			MainViewModel.Instance.ScenarioEventActionValueVisibleBool = true;
			MainViewModel.Instance.ScenarioEventActionValue2VisibleBool = false;
			MainViewModel.Instance.ActionValueMax = actionValueMax;
			MainViewModel.Instance.ActionValueMin = 0;
			MainViewModel.Instance.ActionValueFreq = 1;
			if (scenarioCurrentEvent.action_data_marker < 0)
			{
				scenarioCurrentEvent.action_data_marker = 0;
			}
			RefActionValueSlider.Value = scenarioCurrentEvent.action_data_marker;
			MainViewModel.Instance.ScenarioEventActionMessage = "";
			break;
		}
		default:
			MainViewModel.Instance.ScenarioEventActionMessageVisibleBool = false;
			MainViewModel.Instance.ScenarioEventActionValueVisibleBool = false;
			MainViewModel.Instance.ScenarioEventActionValue2VisibleBool = false;
			MainViewModel.Instance.ScenarioEventActionMessage = "";
			break;
		}
	}

	private void UpdateEventActionRow()
	{
		if (activeEventActionRow == null)
		{
			return;
		}
		int dataValue = activeEventActionRow.DataValue2;
		switch (GameData.scenarioActionsOrder[dataValue])
		{
		case 132:
		case 139:
		case 145:
		case 146:
		case 148:
		case 179:
			activeEventActionRow.Text2 = scenarioCurrentEvent.action_data.ToString();
			break;
		case 180:
			activeEventActionRow.Text2 = scenarioCurrentEvent.action_data_marker + "  " + getAllegianceTeamName(scenarioCurrentEvent.action_data_reinforcement);
			break;
		case 181:
			activeEventActionRow.Text2 = scenarioCurrentEvent.action_data_marker + " / " + getReinforcementsName(scenarioCurrentEvent.action_data_reinforcement);
			break;
		case 182:
			activeEventActionRow.Text2 = scenarioCurrentEvent.action_data_marker.ToString();
			break;
		case 178:
			if (!FatControler.turkish)
			{
				activeEventActionRow.Text2 = scenarioCurrentEvent.action_data + "%";
			}
			else
			{
				activeEventActionRow.Text2 = "%" + scenarioCurrentEvent.action_data;
			}
			break;
		}
	}

	public void MessageMonthBtn()
	{
		if (scenarioCurrentMessage != null && ++scenarioCurrentMessage.month >= 12)
		{
			scenarioCurrentMessage.month = 0;
		}
	}

	public void ActionOKButton(ref int mode)
	{
		if (scenarioCurrentMessage != null)
		{
			if (scenarioCurrentMessage.message_id >= 0)
			{
				EngineInterface.ApplyScenarioMessage(scenarioCurrentLine, scenarioCurrentMessage);
			}
			else
			{
				EngineInterface.DeleteScenarioEntry(scenarioCurrentLine);
			}
		}
		if (scenarioCurrentInvasion != null)
		{
			EngineInterface.ApplyScenarioInvasion(scenarioCurrentLine, scenarioCurrentInvasion);
		}
		if (scenarioCurrentEvent != null)
		{
			if (MainViewModel.Instance.ScenarioEditorMode == Enums.ScenarioViews.Messages)
			{
				mode = 10;
				return;
			}
			EngineInterface.ApplyScenarioEvent(scenarioCurrentLine, scenarioCurrentEvent);
		}
		GameData.Instance.SetScenarioOverview(EngineInterface.GetScenarioOverview());
	}

	public void ActionDeleteButton()
	{
		EngineInterface.DeleteScenarioEntry(scenarioCurrentLine);
		GameData.Instance.SetScenarioOverview(EngineInterface.GetScenarioOverview());
	}

	private void MessageSelected(int messageID)
	{
		if (scenarioCurrentMessage != null)
		{
			scenarioCurrentMessage.message_id = messageID;
		}
		else if (scenarioCurrentEvent != null)
		{
			scenarioCurrentEvent.action_data = messageID;
		}
	}

	public void DrawMessageList()
	{
		MessageListItems.Clear();
		int[] current_list = GameData.Instance.current_list;
		for (int i = 0; i < GameData.Instance.num_of_current_type; i++)
		{
			string messageLibraryText = Translate.Instance.getMessageLibraryText(current_list[i]);
			ScenarioEditorRow scenarioEditorRow = new ScenarioEditorRow(this);
			scenarioEditorRow.Text1 = messageLibraryText;
			scenarioEditorRow.DataValue = current_list[i].ToString();
			MessageListItems.Add(scenarioEditorRow);
			if (scenarioCurrentMessage != null)
			{
				if (current_list[i] == scenarioCurrentMessage.message_id)
				{
					RefScenarioMessageList.SelectedItem = scenarioEditorRow;
				}
			}
			else if (scenarioCurrentEvent != null && current_list[i] == scenarioCurrentEvent.action_data)
			{
				RefScenarioMessageList.SelectedItem = scenarioEditorRow;
			}
		}
		RefScenarioMessageList.ItemsSource = MessageListItems;
	}

	public void SetInvasionFrom(int from)
	{
		if (scenarioCurrentInvasion != null)
		{
			scenarioCurrentInvasion.from = from - 1;
			for (int i = 1; i < 5; i++)
			{
				MainViewModel.Instance.InvasionButtonHighlight[i] = false;
			}
			MainViewModel.Instance.InvasionButtonHighlight[from] = true;
		}
	}

	public void InvasionMonthBtn()
	{
		if (scenarioCurrentInvasion != null && ++scenarioCurrentInvasion.month >= 12)
		{
			scenarioCurrentInvasion.month = 0;
		}
	}

	public void SetInvasionMarkerID(int markerID)
	{
		if (scenarioCurrentInvasion != null)
		{
			scenarioCurrentInvasion.markerID = markerID;
			for (int i = 0; i < 11; i++)
			{
				MainViewModel.Instance.InvasionSpawnMarkersHighlight[i] = false;
			}
			MainViewModel.Instance.InvasionSpawnMarkersHighlight[markerID] = true;
		}
	}

	public int GetInvasionSize(int index)
	{
		if (scenarioCurrentInvasion != null)
		{
			return scenarioCurrentInvasion._size[index];
		}
		return 0;
	}

	public void EventMonthBtn()
	{
		if (scenarioCurrentEvent != null && ++scenarioCurrentEvent.month >= 12)
		{
			scenarioCurrentEvent.month = 0;
		}
	}

	public void EventActionSelected(int actionID)
	{
		if (scenarioCurrentEvent.action == actionID)
		{
			return;
		}
		if (actionID == 2 || (uint)(actionID - 26) <= 1u)
		{
			int action = scenarioCurrentEvent.action;
			if (action != 2 && (uint)(action - 26) > 1u)
			{
				scenarioCurrentEvent.action_data = -1;
			}
		}
		else
		{
			scenarioCurrentEvent.action_data = 0;
		}
		scenarioCurrentEvent.action = actionID;
		string text = actionID.ToString();
		foreach (ScenarioEditorRow item in RefScenarioEventActionList.ItemsSource)
		{
			item.Text2 = "";
			if (item.DataValue == text)
			{
				item.BorderVisibility = Visibility.Visible;
				activeEventActionRow = item;
			}
			else
			{
				item.BorderVisibility = Visibility.Hidden;
			}
		}
		PopulateEventActionLeftSide();
		UpdateEventActionRow();
	}

	public void EventConditionSelected(int conditionID)
	{
		string text = conditionID.ToString();
		foreach (ScenarioEditorRow item in RefScenarioEventConditionList.ItemsSource)
		{
			if (item.DataValue == text)
			{
				activeEventConditionRow = item;
			}
		}
		PopulateEventConditionLeftSide();
	}

	public void EventActionMessageOpen()
	{
		if (scenarioCurrentEvent != null)
		{
			GameData.Instance.setMessagesFromMessageID(scenarioCurrentEvent.action_data);
			PopulateMessage();
		}
	}

	private void ScenarioInvasionRepeatSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		int repeat = (int)RefInvasionRepeatSlider.Value;
		if (scenarioCurrentInvasion != null)
		{
			scenarioCurrentInvasion.repeat = repeat;
		}
	}

	private void ScenarioInvasionSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		int num = (int)RefInvasionSize.Value;
		if (scenarioCurrentInvasion != null)
		{
			scenarioCurrentInvasion._size[MainViewModel.Instance.ScenarioInvasionSizeType] = num;
		}
	}

	private void ActionRepeatMonthsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		int num = (int)RefActionRepeatMonthsSlider.Value;
		if (scenarioCurrentEvent != null)
		{
			scenarioCurrentEvent.repeat = (byte)num;
		}
	}

	private void ActionRepeatSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		int num = (int)RefActionRepeatSlider.Value;
		if (scenarioCurrentEvent != null)
		{
			scenarioCurrentEvent.repeat_count = (byte)num;
		}
	}

	private void ActionValueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		int num = (int)RefActionValueSlider.Value;
		string actionValueText = num.ToString();
		if (scenarioCurrentEvent != null)
		{
			switch (scenarioCurrentEvent.action)
			{
			case 31:
			case 32:
				scenarioCurrentEvent.action_data_marker = num;
				break;
			case 33:
				scenarioCurrentEvent.action_data_marker = num;
				if (num == 0)
				{
					actionValueText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 14);
				}
				break;
			default:
				scenarioCurrentEvent.action_data = num;
				break;
			}
		}
		UpdateEventActionRow();
		MainViewModel.Instance.ActionValueText = actionValueText;
	}

	private void ActionValue2Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		int num = (int)RefActionValue2Slider.Value;
		if (scenarioCurrentEvent != null)
		{
			switch (scenarioCurrentEvent.action)
			{
			case 31:
				scenarioCurrentEvent.action_data_reinforcement = num;
				MainViewModel.Instance.ActionValue2Text = getAllegianceTeamName(num);
				break;
			case 32:
				scenarioCurrentEvent.action_data_reinforcement = num;
				MainViewModel.Instance.ActionValue2Text = getReinforcementsName(num);
				break;
			default:
				scenarioCurrentEvent.action_data = num;
				break;
			}
		}
		UpdateEventActionRow();
	}

	private void ConditionValueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		int sliderPos = (int)RefConditionValueSlider.Value;
		if (scenarioCurrentEvent != null && activeEventConditionRow != null)
		{
			int num = int.Parse(activeEventConditionRow.DataValue, Director.defaultCulture);
			int maxValue = GameData.start_event_max[num];
			sliderPos = getLogSliderDislayValue(sliderPos, maxValue);
			scenarioCurrentEvent.event_value[num].value = (short)sliderPos;
			MainViewModel.Instance.ConditionValueText = sliderPos.ToString();
		}
		UpdateEventConditionRow();
	}

	private void ScenarioRefSliderEditTeam_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		int num = 0;
		switch (((Slider)e.Source).Name)
		{
		case "SliderEditTeam1":
			num = 1;
			break;
		case "SliderEditTeam2":
			num = 2;
			break;
		case "SliderEditTeam3":
			num = 3;
			break;
		case "SliderEditTeam4":
			num = 4;
			break;
		case "SliderEditTeam5":
			num = 5;
			break;
		case "SliderEditTeam6":
			num = 6;
			break;
		case "SliderEditTeam7":
			num = 7;
			break;
		case "SliderEditTeam8":
			num = 8;
			break;
		}
		if (num != 0)
		{
			int state = (int)e.NewValue;
			MainViewModel.Instance.ScenarioEditTeams[num] = state.ToString();
			EngineInterface.GameAction(Enums.GameActionCommand.SetStartingTeam, num, state);
		}
	}

	private void FasterGoodsCheck_ValueChanged(object sender, RoutedEventArgs e)
	{
		if (RefFasterGoodsCheck.IsChecked.Value)
		{
			GameData.Instance.scenarioOverview.fast_goods_feedin = 1;
		}
		else
		{
			GameData.Instance.scenarioOverview.fast_goods_feedin = 0;
		}
		EngineInterface.GameAction(Enums.GameActionCommand.FastGoodsFeedin, GameData.Instance.scenarioOverview.fast_goods_feedin, GameData.Instance.scenarioOverview.fast_goods_feedin);
	}

	public static string getAllegianceTeamName(int data)
	{
		if (data < 1)
		{
			data = 1;
		}
		else if (data > 8)
		{
			data = 9;
		}
		return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 27 + data - 1);
	}

	public static string getReinforcementsName(int data)
	{
		return data switch
		{
			2 => "20 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 22), 
			3 => "50 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 22), 
			4 => "100 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 22), 
			5 => "10 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 23), 
			6 => "20 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 23), 
			7 => "50 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 23), 
			8 => "100 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 23), 
			9 => "10 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 24), 
			10 => "20 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 24), 
			11 => "50 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 24), 
			12 => "100 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 24), 
			13 => "10 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 26), 
			14 => "20 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 26), 
			15 => "50 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 26), 
			16 => "100 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 26), 
			17 => "10 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 25), 
			18 => "20 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 25), 
			19 => "50 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 25), 
			20 => "100 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 25), 
			21 => "10 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 27), 
			22 => "20 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 27), 
			23 => "50 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 27), 
			24 => "100 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 27), 
			25 => "5 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 28), 
			26 => "10 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 28), 
			27 => "20 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 28), 
			28 => "50 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 28), 
			29 => Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 53), 
			30 => Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 54), 
			31 => Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 55), 
			32 => Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 35), 
			33 => Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 36), 
			34 => Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 37), 
			_ => "10 " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 22), 
		};
	}

	public int getLogSliderValue(int currentValue, int minValue, ref int maxValue, ref int freq)
	{
		if (maxValue <= 100)
		{
			return currentValue;
		}
		int[] array;
		switch (maxValue)
		{
		case 500:
			freq = 10;
			array = SliderCurve500;
			maxValue = 100;
			break;
		case 1000:
			freq = 10;
			array = SliderCurve1000;
			maxValue = 100;
			break;
		case 10000:
			freq = 15;
			array = SliderCurve10000;
			maxValue = 145;
			break;
		case 25000:
			freq = 16;
			array = SliderCurve25000;
			maxValue = 160;
			break;
		default:
			freq = maxValue / 10;
			if (freq < 1)
			{
				freq = 1;
			}
			return currentValue;
		}
		for (int i = 0; i <= maxValue; i++)
		{
			if (currentValue <= array[i])
			{
				return i;
			}
		}
		return currentValue;
	}

	public int getLogSliderDislayValue(int sliderPos, int maxValue)
	{
		if (maxValue <= 100)
		{
			return sliderPos;
		}
		int[] array;
		switch (maxValue)
		{
		case 500:
			array = SliderCurve500;
			break;
		case 1000:
			array = SliderCurve1000;
			break;
		case 10000:
			array = SliderCurve10000;
			break;
		case 25000:
			array = SliderCurve25000;
			break;
		default:
			return sliderPos;
		}
		return array[sliderPos];
	}
}
