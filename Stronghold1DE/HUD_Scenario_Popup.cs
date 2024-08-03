using Noesis;

namespace Stronghold1DE;

public class HUD_Scenario_Popup : UserControl
{
	private Grid RefViewRoot;

	private Button RefBorder;

	private TextBlock RefTextType;

	private TextBlock RefTextName;

	private TextBlock RefTextSize;

	private TextBlock RefMapSizeLabel;

	private Button RefButtonEditMapPlayer;

	private Button RefButtonEditSettings;

	private Button RefButtonEditMapSize160;

	private Button RefButtonEditMapSize200;

	private Button RefButtonEditMapSize300;

	private Grid RefScenarioPopupNormalButtons;

	private bool settingsOpened;

	public HUD_Scenario_Popup()
	{
		InitializeComponent();
		MainViewModel.Instance.HUDScenarioPopup = this;
		RefViewRoot = (Grid)FindName("LayoutRoot");
		RefBorder = (Button)FindName("ScenarioBorder");
		RefTextType = (TextBlock)FindName("MapType");
		RefTextName = (TextBlock)FindName("MapName");
		RefTextSize = (TextBlock)FindName("MapSize");
		RefMapSizeLabel = (TextBlock)FindName("MapSizeLabel");
		RefButtonEditMapPlayer = (Button)FindName("ButtonEditMapPlayer");
		((Storyboard)base.Resources["Outtro"]).Completed += delegate
		{
			RefViewRoot.Visibility = Visibility.Hidden;
		};
		RefScenarioPopupNormalButtons = (Grid)FindName("ScenarioPopupNormalButtons");
		RefButtonEditMapSize160 = (Button)FindName("ButtonEditMapSize160");
		RefButtonEditMapSize200 = (Button)FindName("ButtonEditMapSize200");
		RefButtonEditMapSize300 = (Button)FindName("ButtonEditMapSize300");
		RefButtonEditSettings = (Button)FindName("ButtonEditSettings");
		if (FatControler.italian || FatControler.french)
		{
			PropEx.SetGlowButtonFontSize(RefButtonEditSettings, 14);
			PropEx.SetGlowButtonTextHeight(RefButtonEditSettings, 20);
		}
		if (FatControler.ukrainian)
		{
			PropEx.SetGlowButtonFontSize(RefButtonEditSettings, 15);
			PropEx.SetGlowButtonTextHeight(RefButtonEditSettings, 22);
		}
	}

	private void InitializeComponent()
	{
		NoesisUnity.LoadComponent(this, "Assets/GUI/XAMLResources/HUD_Scenario_Popup.xaml");
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

	public void Update()
	{
		if (!settingsOpened)
		{
			UpdateEditorTimeButton();
		}
	}

	public void StartEntryAnim()
	{
		RefViewRoot.Visibility = Visibility.Visible;
		((Storyboard)base.Resources["Intro"]).Begin(this);
		UpdateText();
		SetState(opened: false);
	}

	public void StartExitAnim()
	{
		((Storyboard)base.Resources["Outtro"]).Begin(this);
	}

	public void UpdateText()
	{
		RefTextName.Text = GameData.Instance.currentFileName;
		switch (GameMap.tilemapSize)
		{
		case 160:
			RefTextSize.Text = "160 x 160";
			break;
		case 200:
			RefTextSize.Text = "200 x 200";
			break;
		case 300:
			RefTextSize.Text = "300 x 300";
			break;
		case 400:
			RefTextSize.Text = "400 x 400";
			break;
		}
		string text = "";
		if (GameData.Instance.multiplayerMap)
		{
			text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 9);
		}
		else
		{
			switch (GameData.Instance.mapType)
			{
			case Enums.GameModes.BUILD:
				text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 4);
				break;
			case Enums.GameModes.ECO:
				text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 5);
				break;
			case Enums.GameModes.INVASION:
				text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 6);
				break;
			case Enums.GameModes.SIEGE:
				text = ((!GameData.Instance.siegeThat) ? Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 7) : Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 8));
				break;
			}
		}
		RefTextType.Text = text;
	}

	public void EditSettingsClicked()
	{
		SetState(!settingsOpened);
	}

	public void Refresh()
	{
		SetState(settingsOpened);
	}

	private void SetState(bool opened)
	{
		settingsOpened = opened;
		if (!opened)
		{
			RefScenarioPopupNormalButtons.Visibility = Visibility.Visible;
			Grid refViewRoot = RefViewRoot;
			Button refBorder = RefBorder;
			float num2 = (base.Height = 300f);
			float height = (refBorder.Height = num2);
			refViewRoot.Height = height;
			RefMapSizeLabel.Visibility = Visibility.Hidden;
			RefScenarioPopupNormalButtons.Visibility = Visibility.Visible;
			RefButtonEditMapSize160.Visibility = Visibility.Hidden;
			RefButtonEditMapSize200.Visibility = Visibility.Hidden;
			RefButtonEditMapSize300.Visibility = Visibility.Hidden;
			MainViewModel.Instance.ButtonMapSettingsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 3);
			UpdateEditorTimeButton();
			return;
		}
		RefScenarioPopupNormalButtons.Visibility = Visibility.Collapsed;
		MainViewModel.Instance.ButtonMapSettingsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 11);
		RefMapSizeLabel.Visibility = Visibility.Visible;
		RefButtonEditMapSize160.Visibility = Visibility.Visible;
		RefButtonEditMapSize200.Visibility = Visibility.Visible;
		RefButtonEditMapSize300.Visibility = Visibility.Visible;
		MainViewModel.Instance.ButBordScenEdit160HL = GameMap.tilemapSize == 160;
		MainViewModel.Instance.ButBordScenEdit200HL = GameMap.tilemapSize == 200;
		MainViewModel.Instance.ButBordScenEdit300HL = GameMap.tilemapSize == 300;
		MainViewModel.Instance.ButBordScenEdit400HL = GameMap.tilemapSize == 400;
		if (GameData.Instance.multiplayerMap)
		{
			Grid refViewRoot2 = RefViewRoot;
			Button refBorder2 = RefBorder;
			float num2 = (base.Height = 475f);
			float height = (refBorder2.Height = num2);
			refViewRoot2.Height = height;
			MainViewModel.Instance.ButtonScenarioEditSPMP = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAPEDIT, Enums.eTextValues.TEXT_SCN_TAUNT);
			MainViewModel.Instance.ButtonScenarioEditSinglePlayerVis = false;
			MainViewModel.Instance.ButtonScenarioEditMultiPlayerVis = true;
			MainViewModel.Instance.ButBordScenEditMultiPlayerKoth = GameData.Instance.multiplayerKOTHMap;
		}
		else
		{
			Grid refViewRoot3 = RefViewRoot;
			Button refBorder3 = RefBorder;
			float num2 = (base.Height = 590f);
			float height = (refBorder3.Height = num2);
			refViewRoot3.Height = height;
			MainViewModel.Instance.ButtonScenarioEditSPMP = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAPEDIT, Enums.eTextValues.TEXT_SCN_WOLF);
			MainViewModel.Instance.ButtonScenarioEditSinglePlayerVis = true;
			MainViewModel.Instance.ButtonScenarioEditMultiPlayerVis = false;
			MainViewModel.Instance.ButBordScenEditSPSiege = GameData.Instance.mapType == Enums.GameModes.SIEGE;
			MainViewModel.Instance.ButBordScenEditSPInvasion = GameData.Instance.mapType == Enums.GameModes.INVASION;
			MainViewModel.Instance.ButBordScenEditSPEco = GameData.Instance.mapType == Enums.GameModes.ECO;
			MainViewModel.Instance.ButBordScenEditSPFreeBuild = GameData.Instance.mapType == Enums.GameModes.BUILD;
		}
		UpdateText();
	}

	public void UpdateEditorTimeButton()
	{
		if (GameData.Instance.lastGameState != null)
		{
			if (GameData.Instance.lastGameState.editor_time_paused == 0)
			{
				MainViewModel.Instance.ScenarioPopup_GameTimeText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 82);
			}
			else
			{
				MainViewModel.Instance.ScenarioPopup_GameTimeText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 83);
			}
		}
	}
}
