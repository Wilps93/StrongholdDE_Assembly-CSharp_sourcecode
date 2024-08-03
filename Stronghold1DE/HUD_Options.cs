using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Noesis;
using UnityEngine;

namespace Stronghold1DE;

public class HUD_Options : UserControl
{
	private class HotKeyEntry
	{
		public Enums.KeyFunctions function;

		public int textID;

		public HotKeyEntry(Enums.KeyFunctions f, int t)
		{
			function = f;
			textID = t;
		}
	}

	public static int from;

	private int menuSection;

	private bool panelActive;

	private Noesis.Grid RefVideoSettings;

	private Noesis.Grid RefSoundSettings;

	private Noesis.Grid RefKeySettings;

	private Noesis.Grid RefControlSettings;

	private Noesis.Grid RefNameSettings;

	private Noesis.Grid RefCheatSettings;

	private ComboBox RefResolutionCombo;

	private ComboBox RefScreenModeCombo;

	private Slider RefMasterVolumeSlider;

	private Slider RefMusicVolumeSlider;

	private Slider RefSpeechVolumeSlider;

	private Slider RefUnitSpeechVolumeSlider;

	private Slider RefSFXVolumeSlider;

	private Slider RefScrollSpeedSlider;

	private Slider RefGameSpeedSlider;

	private Slider RefUIScaleSlider;

	private TextBox RefTextBoxChangeName;

	private CheckBox RefVSyncCheck;

	private CheckBox RefLockCursorCheck;

	private CheckBox RefBuildingTooltipsCheck;

	private TextBlock RefBuildingTooltipsCheckText;

	private CheckBox RefSteamHelpCheck;

	private TextBlock RefSteamHelpCheckText;

	private CheckBox RefCompassCheck;

	private CheckBox RefRadarZoomCheck;

	private TextBlock RefRadarZoomCheckText;

	private CheckBox RefCustomIntros;

	private CheckBox RefUISoundsCheck;

	private CheckBox RefReduceSoundsCheck;

	private TextBlock RefReduceSoundsCheckText;

	private CheckBox RefCheatKeysCheck;

	private TextBlock RefPingsCheckText;

	private CheckBox RefPingsCheck;

	private TextBlock RefExtraZoomCheckText;

	private CheckBox RefExtraZoomCheck;

	private Button RefSFXDefaultsButton;

	private RadioButton RefPlayerColourShield1;

	private RadioButton RefPlayerColourShield2;

	private RadioButton RefPlayerColourShield3;

	private RadioButton RefPlayerColourShield4;

	private RadioButton RefPlayerColourShield5;

	private RadioButton RefPlayerColourShield6;

	private RadioButton RefPlayerColourShield7;

	private RadioButton RefPlayerColourShield8;

	private Noesis.Grid RefOptionsHotKeyPanel;

	private Noesis.Grid RefUIScaleGrid;

	private ListView RefHotKeyList;

	private Button RefOptionsHotKeyNewKeyApply;

	private Button RefCursorSystemButton;

	private Button RefCursorSwordButton;

	private Button RefCursorSwordXButton;

	private Button RefScribeClassicButton;

	private Button RefScribeModernButton;

	private Button RefOptionsKeys1;

	private Button RefOptionsKeys2;

	private static HUD_Options instance1;

	private static HUD_Options instance2;

	private ObservableCollection<HotKeyRow> hotKeyRows = new ObservableCollection<HotKeyRow>();

	private Enums.KeyFunctions selectedFunction = Enums.KeyFunctions.NumActions;

	private int selectedColumn = -1;

	private bool resChanged;

	private bool screenModeChanged;

	private DateTime lastDynamicChanged = DateTime.MaxValue;

	private HotKeyEntry[] hotKeyList = new HotKeyEntry[81]
	{
		new HotKeyEntry(Enums.KeyFunctions.Left, 1),
		new HotKeyEntry(Enums.KeyFunctions.Right, 2),
		new HotKeyEntry(Enums.KeyFunctions.Up, 3),
		new HotKeyEntry(Enums.KeyFunctions.Down, 4),
		new HotKeyEntry(Enums.KeyFunctions.Pause, 5),
		new HotKeyEntry(Enums.KeyFunctions.HomeKeep, 6),
		new HotKeyEntry(Enums.KeyFunctions.Market, 7),
		new HotKeyEntry(Enums.KeyFunctions.Signpost, 8),
		new HotKeyEntry(Enums.KeyFunctions.Barracks, 9),
		new HotKeyEntry(Enums.KeyFunctions.Granary, 10),
		new HotKeyEntry(Enums.KeyFunctions.Lord, 106),
		new HotKeyEntry(Enums.KeyFunctions.CycleLord, 107),
		new HotKeyEntry(Enums.KeyFunctions.MapRotateLeft, 11),
		new HotKeyEntry(Enums.KeyFunctions.MapRotateRight, 12),
		new HotKeyEntry(Enums.KeyFunctions.FlattenLandscape, 13),
		new HotKeyEntry(Enums.KeyFunctions.ZoomOut, 14),
		new HotKeyEntry(Enums.KeyFunctions.ZoomIn, 15),
		new HotKeyEntry(Enums.KeyFunctions.Patrol, 16),
		new HotKeyEntry(Enums.KeyFunctions.StanceStand, 112),
		new HotKeyEntry(Enums.KeyFunctions.StanceDefensive, 113),
		new HotKeyEntry(Enums.KeyFunctions.StanceAggressive, 114),
		new HotKeyEntry(Enums.KeyFunctions.RotateBuilding, 108),
		new HotKeyEntry(Enums.KeyFunctions.Load, 17),
		new HotKeyEntry(Enums.KeyFunctions.Save, 18),
		new HotKeyEntry(Enums.KeyFunctions.IncreaseEngineSpeed, 19),
		new HotKeyEntry(Enums.KeyFunctions.DecreaseEngineSpeed, 20),
		new HotKeyEntry(Enums.KeyFunctions.ToggleUI, 21),
		new HotKeyEntry(Enums.KeyFunctions.ToggleFrameRate, 22),
		new HotKeyEntry(Enums.KeyFunctions.RadarZoomIn, 101),
		new HotKeyEntry(Enums.KeyFunctions.RadarZoomOut, 102),
		new HotKeyEntry(Enums.KeyFunctions.FreeBuildEvents, 23),
		new HotKeyEntry(Enums.KeyFunctions.ToggleObjectives, 104),
		new HotKeyEntry(Enums.KeyFunctions.ToggleGoods, 103),
		new HotKeyEntry(Enums.KeyFunctions.QuickSave, 105),
		new HotKeyEntry(Enums.KeyFunctions.OpenChat, 24),
		new HotKeyEntry(Enums.KeyFunctions.ShowPings, 25),
		new HotKeyEntry(Enums.KeyFunctions.GroupTroops0, 26),
		new HotKeyEntry(Enums.KeyFunctions.GroupTroops1, 27),
		new HotKeyEntry(Enums.KeyFunctions.GroupTroops2, 28),
		new HotKeyEntry(Enums.KeyFunctions.GroupTroops3, 29),
		new HotKeyEntry(Enums.KeyFunctions.GroupTroops4, 30),
		new HotKeyEntry(Enums.KeyFunctions.GroupTroops5, 31),
		new HotKeyEntry(Enums.KeyFunctions.GroupTroops6, 32),
		new HotKeyEntry(Enums.KeyFunctions.GroupTroops7, 33),
		new HotKeyEntry(Enums.KeyFunctions.GroupTroops8, 34),
		new HotKeyEntry(Enums.KeyFunctions.GroupTroops9, 35),
		new HotKeyEntry(Enums.KeyFunctions.SelectClan0, 36),
		new HotKeyEntry(Enums.KeyFunctions.SelectClan1, 37),
		new HotKeyEntry(Enums.KeyFunctions.SelectClan2, 38),
		new HotKeyEntry(Enums.KeyFunctions.SelectClan3, 39),
		new HotKeyEntry(Enums.KeyFunctions.SelectClan4, 40),
		new HotKeyEntry(Enums.KeyFunctions.SelectClan5, 41),
		new HotKeyEntry(Enums.KeyFunctions.SelectClan6, 42),
		new HotKeyEntry(Enums.KeyFunctions.SelectClan7, 43),
		new HotKeyEntry(Enums.KeyFunctions.SelectClan8, 44),
		new HotKeyEntry(Enums.KeyFunctions.SelectClan9, 45),
		new HotKeyEntry(Enums.KeyFunctions.SetBookmark0, 46),
		new HotKeyEntry(Enums.KeyFunctions.SetBookmark1, 47),
		new HotKeyEntry(Enums.KeyFunctions.SetBookmark2, 48),
		new HotKeyEntry(Enums.KeyFunctions.SetBookmark3, 49),
		new HotKeyEntry(Enums.KeyFunctions.SetBookmark4, 50),
		new HotKeyEntry(Enums.KeyFunctions.SetBookmark5, 51),
		new HotKeyEntry(Enums.KeyFunctions.SetBookmark6, 52),
		new HotKeyEntry(Enums.KeyFunctions.SetBookmark7, 53),
		new HotKeyEntry(Enums.KeyFunctions.SetBookmark8, 54),
		new HotKeyEntry(Enums.KeyFunctions.SetBookmark9, 55),
		new HotKeyEntry(Enums.KeyFunctions.GotoBookmark0, 56),
		new HotKeyEntry(Enums.KeyFunctions.GotoBookmark1, 57),
		new HotKeyEntry(Enums.KeyFunctions.GotoBookmark2, 58),
		new HotKeyEntry(Enums.KeyFunctions.GotoBookmark3, 59),
		new HotKeyEntry(Enums.KeyFunctions.GotoBookmark4, 60),
		new HotKeyEntry(Enums.KeyFunctions.GotoBookmark5, 61),
		new HotKeyEntry(Enums.KeyFunctions.GotoBookmark6, 62),
		new HotKeyEntry(Enums.KeyFunctions.GotoBookmark7, 63),
		new HotKeyEntry(Enums.KeyFunctions.GotoBookmark8, 64),
		new HotKeyEntry(Enums.KeyFunctions.GotoBookmark9, 65),
		new HotKeyEntry(Enums.KeyFunctions.Cheat_gold, 110),
		new HotKeyEntry(Enums.KeyFunctions.Cheat_freestuff, 111),
		new HotKeyEntry(Enums.KeyFunctions.EditorHoldTime, 98),
		new HotKeyEntry(Enums.KeyFunctions.EditorRespawnLord, 99),
		new HotKeyEntry(Enums.KeyFunctions.EditorWipeAnimals, 100)
	};

	private Dictionary<Enums.KeyFunctions, int> hotKeyTextDict;

	public HUD_Options()
	{
		InitializeComponent();
		if (instance1 == null)
		{
			instance1 = this;
		}
		else if (instance2 == null)
		{
			instance2 = this;
		}
		RefVideoSettings = (Noesis.Grid)FindName("VideoSettings");
		RefSoundSettings = (Noesis.Grid)FindName("SoundSettings");
		RefKeySettings = (Noesis.Grid)FindName("KeySettings");
		RefControlSettings = (Noesis.Grid)FindName("ControlSettings");
		RefNameSettings = (Noesis.Grid)FindName("NameSettings");
		RefCheatSettings = (Noesis.Grid)FindName("CheatSettings");
		RefResolutionCombo = (ComboBox)FindName("ResolutionCombo");
		RefScreenModeCombo = (ComboBox)FindName("ScreenModeCombo");
		RefOptionsHotKeyPanel = (Noesis.Grid)FindName("OptionsHotKeyPanel");
		RefUIScaleGrid = (Noesis.Grid)FindName("UIScaleGrid");
		RefMasterVolumeSlider = (Slider)FindName("MasterVolumeSlider");
		RefMasterVolumeSlider.ValueChanged += MasterVolumeSlider_ValueChanged;
		RefMusicVolumeSlider = (Slider)FindName("MusicVolumeSlider");
		RefMusicVolumeSlider.ValueChanged += MusicVolumeSlider_ValueChanged;
		RefSpeechVolumeSlider = (Slider)FindName("SpeechVolumeSlider");
		RefSpeechVolumeSlider.ValueChanged += SpeechVolumeSlider_ValueChanged;
		RefUnitSpeechVolumeSlider = (Slider)FindName("UnitSpeechVolumeSlider");
		RefUnitSpeechVolumeSlider.ValueChanged += UnitSpeechVolumeSlider_ValueChanged;
		RefSFXVolumeSlider = (Slider)FindName("SFXVolumeSlider");
		RefSFXVolumeSlider.ValueChanged += SFXVolumeSlider_ValueChanged;
		RefScrollSpeedSlider = (Slider)FindName("ScrollSpeedSlider");
		RefScrollSpeedSlider.ValueChanged += ScrollSpeedSlider_ValueChanged;
		RefGameSpeedSlider = (Slider)FindName("GameSpeedSlider");
		RefGameSpeedSlider.ValueChanged += GameSpeedSlider_ValueChanged;
		RefUIScaleSlider = (Slider)FindName("UIScaleSlider");
		RefUIScaleSlider.ValueChanged += UIScaleSlider_ValueChanged;
		RefTextBoxChangeName = (TextBox)FindName("TextBoxChangeName");
		RefTextBoxChangeName.IsKeyboardFocusedChanged += TextInputFocus;
		RefVSyncCheck = (CheckBox)FindName("VSyncCheck");
		RefVSyncCheck.Checked += VSyncCheck_ValueChanged;
		RefVSyncCheck.Unchecked += VSyncCheck_ValueChanged;
		RefLockCursorCheck = (CheckBox)FindName("LockCursorCheck");
		RefLockCursorCheck.Checked += LockCursor_ValueChanged;
		RefLockCursorCheck.Unchecked += LockCursor_ValueChanged;
		RefBuildingTooltipsCheck = (CheckBox)FindName("BuildingTooltipsCheck");
		RefBuildingTooltipsCheck.Checked += BuildingTooltipsCheck_ValueChanged;
		RefBuildingTooltipsCheck.Unchecked += BuildingTooltipsCheck_ValueChanged;
		RefBuildingTooltipsCheckText = (TextBlock)FindName("BuildingTooltipsCheckText");
		RefSteamHelpCheck = (CheckBox)FindName("SteamHelpCheck");
		RefSteamHelpCheck.Checked += SteamHelp_ValueChanged;
		RefSteamHelpCheck.Unchecked += SteamHelp_ValueChanged;
		RefSteamHelpCheckText = (TextBlock)FindName("SteamHelpCheckText");
		RefCompassCheck = (CheckBox)FindName("CompassCheck");
		RefCompassCheck.Checked += CompassCheck_ValueChanged;
		RefCompassCheck.Unchecked += CompassCheck_ValueChanged;
		RefRadarZoomCheck = (CheckBox)FindName("RadarZoomCheck");
		RefRadarZoomCheck.Checked += RadarZoomCheck_ValueChanged;
		RefRadarZoomCheck.Unchecked += RadarZoomCheck_ValueChanged;
		RefRadarZoomCheckText = (TextBlock)FindName("RadarZoomCheckText");
		RefCustomIntros = (CheckBox)FindName("CustomIntros");
		RefCustomIntros.Checked += CustomIntros_ValueChanged;
		RefCustomIntros.Unchecked += CustomIntros_ValueChanged;
		RefUISoundsCheck = (CheckBox)FindName("UISoundsCheck");
		RefUISoundsCheck.Checked += UISounds_ValueChanged;
		RefUISoundsCheck.Unchecked += UISounds_ValueChanged;
		RefReduceSoundsCheck = (CheckBox)FindName("ReduceSoundsCheck");
		RefReduceSoundsCheck.Checked += ReduceSounds_ValueChanged;
		RefReduceSoundsCheck.Unchecked += ReduceSounds_ValueChanged;
		RefReduceSoundsCheckText = (TextBlock)FindName("ReduceSoundsCheckText");
		RefSFXDefaultsButton = (Button)FindName("SFXDefaultsButton");
		RefCheatKeysCheck = (CheckBox)FindName("CheatKeysCheck");
		RefCheatKeysCheck.Checked += CheatKeys_ValueChanged;
		RefCheatKeysCheck.Unchecked += CheatKeys_ValueChanged;
		RefPingsCheck = (CheckBox)FindName("PingsCheck");
		RefPingsCheck.Checked += Pings_ValueChanged;
		RefPingsCheck.Unchecked += Pings_ValueChanged;
		RefPingsCheckText = (TextBlock)FindName("PingsCheckText");
		RefExtraZoomCheck = (CheckBox)FindName("ExtraZoomCheck");
		RefExtraZoomCheck.Checked += ExtraZoom_ValueChanged;
		RefExtraZoomCheck.Unchecked += ExtraZoom_ValueChanged;
		RefExtraZoomCheckText = (TextBlock)FindName("ExtraZoomCheckText");
		RefPlayerColourShield1 = (RadioButton)FindName("PlayerColourShield1");
		RefPlayerColourShield2 = (RadioButton)FindName("PlayerColourShield2");
		RefPlayerColourShield3 = (RadioButton)FindName("PlayerColourShield3");
		RefPlayerColourShield4 = (RadioButton)FindName("PlayerColourShield4");
		RefPlayerColourShield5 = (RadioButton)FindName("PlayerColourShield5");
		RefPlayerColourShield6 = (RadioButton)FindName("PlayerColourShield6");
		RefPlayerColourShield7 = (RadioButton)FindName("PlayerColourShield7");
		RefPlayerColourShield8 = (RadioButton)FindName("PlayerColourShield8");
		Resolution[] resolutions = Screen.resolutions;
		for (int i = 0; i < resolutions.Length; i++)
		{
			Resolution resolution = resolutions[i];
			if (resolution.width >= 1280 && resolution.height >= 768)
			{
				ComboBoxItem item = new ComboBoxItem
				{
					Content = resolution.width + "x" + resolution.height + " (" + resolution.refreshRate + "hz)",
					Tag = resolution,
					Height = 25f,
					Padding = new Thickness(12f, 0f, 12f, 0f),
					VerticalAlignment = VerticalAlignment.Center
				};
				RefResolutionCombo.Items.Add(item);
			}
		}
		UpdateResListbox();
		RefResolutionCombo.SelectionChanged += RefResolutionCombo_SelectionChanged;
		ComboBoxItem item2 = new ComboBoxItem
		{
			Content = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 88),
			Tag = 0,
			Height = 25f,
			Padding = new Thickness(12f, 0f, 12f, 0f),
			VerticalAlignment = VerticalAlignment.Center
		};
		RefScreenModeCombo.Items.Add(item2);
		ComboBoxItem item3 = new ComboBoxItem
		{
			Content = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 100),
			Tag = 1,
			Height = 25f,
			Padding = new Thickness(12f, 0f, 12f, 0f),
			VerticalAlignment = VerticalAlignment.Center
		};
		RefScreenModeCombo.Items.Add(item3);
		ComboBoxItem item4 = new ComboBoxItem
		{
			Content = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 101),
			Tag = 2,
			Height = 25f,
			Padding = new Thickness(12f, 0f, 12f, 0f),
			VerticalAlignment = VerticalAlignment.Center
		};
		RefScreenModeCombo.Items.Add(item4);
		if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
		{
			RefScreenModeCombo.SelectedIndex = 1;
		}
		else if (Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen)
		{
			RefScreenModeCombo.SelectedIndex = 0;
		}
		else
		{
			RefScreenModeCombo.SelectedIndex = 2;
		}
		RefScreenModeCombo.SelectionChanged += RefScreenModeCombo_SelectionChanged;
		if (FatControler.polish || FatControler.ukrainian || FatControler.french || FatControler.spanish || FatControler.russian || FatControler.thai)
		{
			RefBuildingTooltipsCheck.Height = 43f;
			RefBuildingTooltipsCheckText.LineHeight = 20f;
			RefBuildingTooltipsCheckText.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
		}
		if (FatControler.japanese || FatControler.ukrainian)
		{
			RefRadarZoomCheck.Height = 43f;
			RefRadarZoomCheckText.LineHeight = 20f;
			RefRadarZoomCheckText.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
		}
		if (FatControler.spanish || FatControler.greek)
		{
			RefCustomIntros.Height = 43f;
			RefCustomIntros.Margin = new Thickness(0f, 100f, 0f, 0f);
		}
		if (FatControler.polish)
		{
			RefReduceSoundsCheck.Height = 43f;
			RefReduceSoundsCheckText.LineHeight = 20f;
			RefReduceSoundsCheckText.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
			RefSFXDefaultsButton.Margin = new Thickness(0f, 550f, 27f, 10f);
		}
		if (FatControler.german || FatControler.portuguese || FatControler.russian || FatControler.ukrainian || FatControler.czech || FatControler.french || FatControler.hungarian || FatControler.italian || FatControler.japanese || FatControler.polish || FatControler.spanish || FatControler.thai || FatControler.turkish || FatControler.greek)
		{
			RefPingsCheck.Height = 43f;
			RefPingsCheckText.LineHeight = 20f;
			RefPingsCheckText.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
		}
		if (FatControler.polish)
		{
			RefExtraZoomCheck.Height = 43f;
			RefExtraZoomCheckText.LineHeight = 20f;
			RefExtraZoomCheckText.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
		}
		if (FatControler.ukrainian)
		{
			RefSteamHelpCheck.Height = 43f;
			RefSteamHelpCheckText.LineHeight = 20f;
			RefSteamHelpCheckText.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
		}
		RefHotKeyList = (ListView)FindName("HotKeyList");
		RefHotKeyList.SelectionChanged += delegate
		{
			if (RefHotKeyList.SelectedItem != null)
			{
				MainViewModel.Instance.OptionsHotKeyTitle = ((HotKeyRow)RefHotKeyList.SelectedItem).Text1;
				selectedFunction = (Enums.KeyFunctions)((HotKeyRow)RefHotKeyList.SelectedItem).iDataValue;
				string text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 93);
				KeyCode keyCode = KeyManager.instance.GetKeyCode(selectedFunction, 0);
				KeyCode keyCode2 = KeyManager.instance.GetKeyCode(selectedFunction, 1);
				if (keyCode == KeyCode.None)
				{
					MainViewModel.Instance.OptionsHotKey1 = text;
				}
				else
				{
					MainViewModel.Instance.OptionsHotKey1 = GetKeyCodeString(keyCode);
				}
				if (keyCode2 == KeyCode.None)
				{
					MainViewModel.Instance.OptionsHotKey2 = text;
				}
				else
				{
					MainViewModel.Instance.OptionsHotKey2 = GetKeyCodeString(keyCode2);
				}
				RefOptionsHotKeyPanel.Visibility = Visibility.Visible;
				MainViewModel.Instance.OptionsHotKeySelectVis = Visibility.Visible;
				MainViewModel.Instance.OptionsHotKeyChangeVis = Visibility.Hidden;
			}
		};
		RefOptionsHotKeyNewKeyApply = (Button)FindName("OptionsHotKeyNewKeyApply");
		RefCursorSystemButton = (Button)FindName("CursorSystemButton");
		RefCursorSwordButton = (Button)FindName("CursorSwordButton");
		RefCursorSwordXButton = (Button)FindName("CursorSwordXButton");
		RefScribeClassicButton = (Button)FindName("ScribeClassicButton");
		RefScribeModernButton = (Button)FindName("ScribeModernButton");
		RefOptionsKeys1 = (Button)FindName("OptionsKeys1");
		RefOptionsKeys2 = (Button)FindName("OptionsKeys2");
		if (FatControler.hungarian)
		{
			RefOptionsKeys1.Width = 380f;
			RefOptionsKeys1.Margin = new Thickness(10f, 0f, 0f, 70f);
			RefOptionsKeys2.Width = 380f;
			RefOptionsKeys2.Margin = new Thickness(10f, 0f, 0f, 30f);
		}
		CreateHotkeyList();
	}

	public static void OpenOptions(bool fromIngameMenu)
	{
		MainViewModel.Instance.Show_HUD_Options = true;
		if (instance1.IsVisible)
		{
			MainViewModel.Instance.HUDOptions = instance1;
		}
		else if (instance2.IsVisible)
		{
			MainViewModel.Instance.HUDOptions = instance2;
		}
		if (fromIngameMenu)
		{
			from = 0;
		}
		else
		{
			from = 1;
		}
		MainViewModel.Instance.HUDOptions.Init();
	}

	private void Init()
	{
		if (Director.instance.SimRunning && MainViewModel.Instance.ShowingScenario)
		{
			MainViewModel.Instance.HUDScenario.StartExitAnim();
		}
		panelActive = true;
		menuSection = 0;
		UpdateMenus();
		UpdateControls();
		UpdateCursors();
		RefMasterVolumeSlider.Value = (int)(ConfigSettings.Settings_MasterVolume * 100f);
		RefMusicVolumeSlider.Value = (int)(ConfigSettings.Settings_MusicVolume * 100f);
		RefSpeechVolumeSlider.Value = (int)(ConfigSettings.Settings_SpeechVolume * 100f);
		RefUnitSpeechVolumeSlider.Value = (int)(ConfigSettings.Settings_UnitSpeechVolume * 100f);
		RefSFXVolumeSlider.Value = (int)(ConfigSettings.Settings_SFXVolume * 100f);
		MainViewModel.Instance.MasterVolumeValue = RefMasterVolumeSlider.Value.ToString();
		MainViewModel.Instance.MusicVolumeValue = RefMusicVolumeSlider.Value.ToString();
		MainViewModel.Instance.SpeechVolumeValue = RefSpeechVolumeSlider.Value.ToString();
		MainViewModel.Instance.UnitSpeechVolumeValue = RefUnitSpeechVolumeSlider.Value.ToString();
		MainViewModel.Instance.SfxVolumeValue = RefSFXVolumeSlider.Value.ToString();
		RefScrollSpeedSlider.Value = ConfigSettings.Settings_ScrollSpeed;
		RefGameSpeedSlider.Value = ConfigSettings.Settings_GameSpeed / 5;
		RefTextBoxChangeName.Text = ConfigSettings.Settings_UserName;
		RefUIScaleSlider.Value = (int)(ConfigSettings.Settings_UIScale * 100f);
		if (ConfigSettings.Settings_Vsync)
		{
			RefVSyncCheck.IsChecked = true;
		}
		else
		{
			RefVSyncCheck.IsChecked = false;
		}
		if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
		{
			RefScreenModeCombo.SelectedIndex = 1;
		}
		else if (Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen)
		{
			RefScreenModeCombo.SelectedIndex = 0;
		}
		else
		{
			RefScreenModeCombo.SelectedIndex = 2;
		}
		RefLockCursorCheck.IsChecked = ConfigSettings.Settings_LockCursor;
		RefBuildingTooltipsCheck.IsChecked = ConfigSettings.Settings_ShowBuildingTooltips;
		RefSteamHelpCheck.IsChecked = ConfigSettings.Settings_UseSteamOverlayForHelp;
		RefCompassCheck.IsChecked = ConfigSettings.Settings_Compass;
		RefRadarZoomCheck.IsChecked = ConfigSettings.Settings_RadarDefaultZoomedOut;
		RefCustomIntros.IsChecked = ConfigSettings.Settings_CustomIntros;
		RefUISoundsCheck.IsChecked = ConfigSettings.Settings_PlayUISFX;
		RefReduceSoundsCheck.IsChecked = ConfigSettings.Settings_ReduceMusicVolumeForSpeech;
		RefCheatKeysCheck.IsChecked = ConfigSettings.Settings_CheatKeysEnabled;
		RefPingsCheck.IsChecked = ConfigSettings.Settings_ShowPings;
		RefExtraZoomCheck.IsChecked = ConfigSettings.Settings_ExtraZoom;
		MainViewModel.Instance.OptionsScaleApplyVisible = Visibility.Hidden;
		if (Director.instance.MultiplayerGame)
		{
			MainViewModel.Instance.OptionsGameSpeedVis = Visibility.Collapsed;
		}
		else
		{
			MainViewModel.Instance.OptionsGameSpeedVis = Visibility.Visible;
		}
		if (ConfigSettings.AchievementsDisabled)
		{
			MainViewModel.Instance.OptionsAchievementsDisabledVis = Visibility.Visible;
		}
		else
		{
			MainViewModel.Instance.OptionsAchievementsDisabledVis = Visibility.Hidden;
		}
		if (Director.instance.SimRunning && GameData.Instance.game_type != 3)
		{
			MainViewModel.Instance.OptionsInGameCheatsVis = Visibility.Visible;
		}
		else
		{
			MainViewModel.Instance.OptionsInGameCheatsVis = Visibility.Hidden;
		}
		if (GameData.Instance.lastGameState == null || GameData.Instance.lastGameState.free_buildingCheat == 0)
		{
			MainViewModel.Instance.OptionsFreeBuildingsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 39);
		}
		else
		{
			MainViewModel.Instance.OptionsFreeBuildingsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 40);
		}
		if (Director.instance.SimRunning)
		{
			MainViewModel.Instance.OptionsPlayerColourVis = Visibility.Hidden;
		}
		else
		{
			MainViewModel.Instance.OptionsPlayerColourVis = Visibility.Visible;
		}
		switch (ConfigSettings.Settings_PlayerColour)
		{
		case 0:
			RefPlayerColourShield1.IsChecked = true;
			break;
		case 1:
			RefPlayerColourShield2.IsChecked = true;
			break;
		case 2:
			RefPlayerColourShield3.IsChecked = true;
			break;
		case 3:
			RefPlayerColourShield4.IsChecked = true;
			break;
		case 4:
			RefPlayerColourShield5.IsChecked = true;
			break;
		case 5:
			RefPlayerColourShield6.IsChecked = true;
			break;
		case 6:
			RefPlayerColourShield7.IsChecked = true;
			break;
		case 7:
			RefPlayerColourShield8.IsChecked = true;
			break;
		}
		UpdateUIScaleSliderVis();
		MainViewModel.Instance.OptionsApplyVisible = Visibility.Hidden;
		resChanged = false;
		screenModeChanged = false;
		CreateHotkeyList();
	}

	public void RefreshGameSpeed()
	{
		RefGameSpeedSlider.Value = ConfigSettings.Settings_GameSpeed / 5;
	}

	public void Update()
	{
		UpdateUIScaleSliderVis();
		if (KeyManager.instance.HotKeySelectorMode)
		{
			if (KeyManager.instance.HotKeyCurrentKey == 0)
			{
				MainViewModel.Instance.OptionsHotKeyNewKey = "";
				MainViewModel.Instance.OptionsHotKeyWarning = "";
			}
			else
			{
				MainViewModel.Instance.OptionsHotKeyNewKey = GetKeyCodeString((KeyCode)KeyManager.instance.HotKeyCurrentKey);
				RefOptionsHotKeyNewKeyApply.IsEnabled = true;
				Enums.KeyFunctions hotKeyFunction = KeyManager.instance.GetHotKeyFunction();
				if (hotKeyFunction == Enums.KeyFunctions.NumActions)
				{
					MainViewModel.Instance.OptionsHotKeyWarning = "";
				}
				else
				{
					string text = "";
					if (hotKeyTextDict.TryGetValue(hotKeyFunction, out var value))
					{
						text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, value);
						MainViewModel.Instance.OptionsHotKeyWarning = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 97) + " " + text;
					}
				}
			}
		}
		if (lastDynamicChanged < DateTime.UtcNow)
		{
			Save();
		}
	}

	public void ButtonClicked(int param)
	{
		switch (param)
		{
		case -1:
			switch (from)
			{
			case 0:
				MainViewModel.Instance.Show_HUD_Options = false;
				if (!MainViewModel.Instance.HUDIngameMenu.wasPaused)
				{
					Director.instance.SetPausedState(state: false);
				}
				MainViewModel.Instance.HUDmain.InGameOptions(null, null);
				break;
			case 1:
				MainViewModel.Instance.Show_HUD_Options = false;
				break;
			}
			break;
		case -2:
		{
			int targetFrameRate = 0;
			if (RefVSyncCheck.IsChecked.Value)
			{
				targetFrameRate = ((RefResolutionCombo.SelectedItem == null) ? Screen.currentResolution.refreshRate : ((Resolution)((ComboBoxItem)RefResolutionCombo.SelectedItem).Tag).refreshRate);
			}
			Application.targetFrameRate = targetFrameRate;
			if (screenModeChanged && !resChanged && RefScreenModeCombo.SelectedIndex == 2)
			{
				Screen.fullScreenMode = FullScreenMode.Windowed;
			}
			else
			{
				FullScreenMode fullScreenMode = FullScreenMode.Windowed;
				switch (RefScreenModeCombo.SelectedIndex)
				{
				case 0:
					fullScreenMode = FullScreenMode.ExclusiveFullScreen;
					ConfigSettings.Settings_LastFullscreenType = 0;
					break;
				case 1:
					fullScreenMode = FullScreenMode.FullScreenWindow;
					ConfigSettings.Settings_LastFullscreenType = 1;
					break;
				case 2:
					fullScreenMode = FullScreenMode.Windowed;
					break;
				}
				if (RefResolutionCombo.SelectedItem == null)
				{
					Screen.fullScreenMode = fullScreenMode;
				}
				else
				{
					Resolution resolution = (Resolution)((ComboBoxItem)RefResolutionCombo.SelectedItem).Tag;
					ConfigSettings.Settings_LastFullscreenWidth = resolution.width;
					ConfigSettings.Settings_LastFullscreenHeight = resolution.height;
					ConfigSettings.Settings_LastFullscreenRefresh = resolution.refreshRate;
					Screen.SetResolution(resolution.width, resolution.height, fullScreenMode, resolution.refreshRate);
				}
			}
			SetVSync(RefVSyncCheck.IsChecked.Value);
			UpdateResListbox(fromSettings: true);
			Save();
			MainViewModel.Instance.OptionsApplyVisible = Visibility.Hidden;
			screenModeChanged = false;
			resChanged = false;
			UpdateUIScaleSliderVis();
			break;
		}
		case -3:
		{
			float scaleFactor = (ConfigSettings.Settings_UIScale = (float)(int)RefUIScaleSlider.Value / 100f);
			if (MainViewModel.Instance.Show_InGame)
			{
				MainViewModel.Instance.ScaleIngameUI(scaleFactor);
			}
			else
			{
				MainViewModel.Instance.FrontEndMenu.UpdateFrontMenuPopupScale();
			}
			MainViewModel.Instance.OptionsScaleApplyVisible = Visibility.Hidden;
			Save();
			break;
		}
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 8:
			menuSection = param - 1;
			UpdateMenus();
			break;
		case 6:
			try
			{
				string persistentDataPath = Application.persistentDataPath;
				Application.OpenURL("file://" + persistentDataPath);
				break;
			}
			catch (Exception)
			{
				break;
			}
		case -10:
			ConfigSettings.Settings_CursorStyle = 1;
			Director.instance.resetCursor();
			UpdateCursors();
			Save();
			break;
		case -11:
			ConfigSettings.Settings_CursorStyle = 0;
			Director.instance.resetCursor();
			UpdateCursors();
			Save();
			break;
		case -12:
			ConfigSettings.Settings_CursorStyle = 2;
			Director.instance.resetCursor();
			UpdateCursors();
			Save();
			break;
		case -15:
			ConfigSettings.Settings_Scribe = 0;
			UpdateCursors();
			Save();
			break;
		case -16:
			ConfigSettings.Settings_Scribe = 1;
			UpdateCursors();
			Save();
			break;
		case -17:
			ConfigSettings.Settings_Scribe = 2;
			UpdateCursors();
			Save();
			break;
		case -20:
			ConfigSettings.Settings_MasterVolume = 0.8f;
			ConfigSettings.Settings_MusicVolume = 1f;
			ConfigSettings.Settings_SpeechVolume = 1f;
			ConfigSettings.Settings_UnitSpeechVolume = 1f;
			ConfigSettings.Settings_SFXVolume = 1f;
			RefMasterVolumeSlider.Value = (int)(ConfigSettings.Settings_MasterVolume * 100f);
			RefMusicVolumeSlider.Value = (int)(ConfigSettings.Settings_MusicVolume * 100f);
			RefSpeechVolumeSlider.Value = (int)(ConfigSettings.Settings_SpeechVolume * 100f);
			RefUnitSpeechVolumeSlider.Value = (int)(ConfigSettings.Settings_UnitSpeechVolume * 100f);
			RefSFXVolumeSlider.Value = (int)(ConfigSettings.Settings_SFXVolume * 100f);
			Save();
			break;
		case -40:
			RefScrollSpeedSlider.Value = (ConfigSettings.Settings_ScrollSpeed = 5);
			Save();
			break;
		case -41:
			RefGameSpeedSlider.Value = 8f;
			break;
		case 41:
			KeyManager.instance.SetDefaultFunctionsSH1();
			CreateHotkeyList();
			ConfigSettings.SetDirty();
			Save();
			break;
		case 42:
			KeyManager.instance.SetDefaultFunctionsNew();
			CreateHotkeyList();
			ConfigSettings.SetDirty();
			Save();
			break;
		case 43:
			ConfigSettings.Settings_PushMapScrolling = true;
			UpdateControls();
			Save();
			break;
		case 44:
			ConfigSettings.Settings_PushMapScrolling = false;
			UpdateControls();
			Save();
			break;
		case 45:
			ConfigSettings.Settings_SH1RTSControls = true;
			UpdateControls();
			Save();
			break;
		case 46:
			ConfigSettings.Settings_SH1RTSControls = false;
			UpdateControls();
			Save();
			break;
		case 47:
			ConfigSettings.Settings_SH1MouseWheel = true;
			UpdateControls();
			Save();
			break;
		case 48:
			ConfigSettings.Settings_SH1MouseWheel = false;
			UpdateControls();
			Save();
			break;
		case 49:
			ConfigSettings.Settings_SH1CentreControls = true;
			UpdateControls();
			Save();
			break;
		case 50:
			ConfigSettings.Settings_SH1CentreControls = false;
			UpdateControls();
			Save();
			break;
		case 101:
			ConfigSettings.TempMissionUnlock = true;
			ConfigSettings.AchievementsDisabled = true;
			MainViewModel.Instance.OptionsAchievementsDisabledVis = Visibility.Visible;
			break;
		case 102:
			HUD_ConfirmationPopup.ShowConfirmationMessage(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 33), delegate
			{
				ConfigSettings.Settings_Progress_Campaign = 21;
				ConfigSettings.Settings_Progress_EcoCampaign = 5;
				ConfigSettings.Settings_Progress_Extra1Campaign = 7;
				ConfigSettings.Settings_Progress_Extra2Campaign = 7;
				ConfigSettings.Settings_Progress_Extra3Campaign = 7;
				ConfigSettings.Settings_Progress_ExtraEcoCampaign = 7;
				ConfigSettings.Settings_Progress_Extra4Campaign = 7;
				ConfigSettings.Settings_Progress_Trail2 = 10;
				ConfigSettings.Settings_Progress_Trail = 10;
				ConfigSettings.SaveSettings();
			}, delegate
			{
			}, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 41));
			break;
		case 103:
			HUD_ConfirmationPopup.ShowConfirmationMessage(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 36), delegate
			{
				ConfigSettings.Settings_Progress_Campaign = 1;
				ConfigSettings.Settings_Progress_EcoCampaign = 1;
				ConfigSettings.Settings_Progress_Extra1Campaign = 1;
				ConfigSettings.Settings_Progress_Extra2Campaign = 1;
				ConfigSettings.Settings_Progress_Extra3Campaign = 1;
				ConfigSettings.Settings_Progress_Extra4Campaign = 1;
				ConfigSettings.Settings_Progress_Trail = 1;
				FrontendMenus.CurrentSelectedMission = 1;
				FrontendMenus.CurrentSelectedEcoMission = 1;
				FrontendMenus.CurrentSelectedExtra1Mission = 1;
				FrontendMenus.CurrentSelectedExtra2Mission = 1;
				FrontendMenus.CurrentSelectedExtra3Mission = 1;
				FrontendMenus.CurrentSelectedExtraEcoMission = 1;
				FrontendMenus.CurrentSelectedExtra4Mission = 1;
				ConfigSettings.Settings_Progress_Trail2 = 1;
				ConfigSettings.SaveSettings();
			}, delegate
			{
			}, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 42));
			break;
		case 104:
			EngineInterface.GameAction(Enums.GameActionCommand.SH1Cheats, 3, 0);
			ConfigSettings.AchievementsDisabled = true;
			MainViewModel.Instance.OptionsAchievementsDisabledVis = Visibility.Visible;
			break;
		case 105:
			EngineInterface.GameAction(Enums.GameActionCommand.SH1Cheats, 2, 0);
			ConfigSettings.AchievementsDisabled = true;
			MainViewModel.Instance.OptionsAchievementsDisabledVis = Visibility.Visible;
			break;
		case 106:
			if (GameData.Instance.lastGameState != null)
			{
				if (GameData.Instance.lastGameState.free_buildingCheat == 0)
				{
					GameData.Instance.lastGameState.free_buildingCheat = 1;
					MainViewModel.Instance.OptionsFreeBuildingsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 40);
					EngineInterface.GameAction(Enums.GameActionCommand.SH1Cheats, 1, 0);
					ConfigSettings.AchievementsDisabled = true;
					MainViewModel.Instance.OptionsAchievementsDisabledVis = Visibility.Visible;
				}
				else
				{
					GameData.Instance.lastGameState.free_buildingCheat = 0;
					MainViewModel.Instance.OptionsFreeBuildingsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 39);
					EngineInterface.GameAction(Enums.GameActionCommand.SH1Cheats, 0, 0);
				}
			}
			break;
		case 70:
		case 71:
		case 72:
		case 73:
		case 74:
		case 75:
		case 76:
		case 77:
			ConfigSettings.Settings_PlayerColour = param - 70;
			Save();
			break;
		case -101:
			RefOptionsHotKeyPanel.Visibility = Visibility.Hidden;
			CreateHotkeyList();
			RefHotKeyList.SelectedItem = null;
			break;
		case -102:
			MainViewModel.Instance.OptionsHotKeyCurrentKey = MainViewModel.Instance.OptionsHotKey1;
			MainViewModel.Instance.OptionsHotKeyNewKey = "";
			RefOptionsHotKeyNewKeyApply.IsEnabled = false;
			KeyManager.instance.HotKeySelectorMode = true;
			selectedColumn = 0;
			MainViewModel.Instance.OptionsHotKeySelectVis = Visibility.Hidden;
			MainViewModel.Instance.OptionsHotKeyChangeVis = Visibility.Visible;
			break;
		case -103:
			MainViewModel.Instance.OptionsHotKeyCurrentKey = MainViewModel.Instance.OptionsHotKey2;
			MainViewModel.Instance.OptionsHotKeyNewKey = "";
			RefOptionsHotKeyNewKeyApply.IsEnabled = false;
			KeyManager.instance.HotKeySelectorMode = true;
			selectedColumn = 1;
			MainViewModel.Instance.OptionsHotKeySelectVis = Visibility.Hidden;
			MainViewModel.Instance.OptionsHotKeyChangeVis = Visibility.Visible;
			break;
		case -104:
			KeyManager.instance.HotKeySelectorMode = false;
			MainViewModel.Instance.OptionsHotKeySelectVis = Visibility.Visible;
			MainViewModel.Instance.OptionsHotKeyChangeVis = Visibility.Hidden;
			break;
		case -105:
		{
			if (KeyManager.instance.HotKeyCurrentKey > 0)
			{
				KeyManager.instance.SetNewKey(selectedFunction, KeyManager.instance.HotKeyCurrentKey, selectedColumn);
			}
			string text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 93);
			KeyCode keyCode3 = KeyManager.instance.GetKeyCode(selectedFunction, 0);
			KeyCode keyCode4 = KeyManager.instance.GetKeyCode(selectedFunction, 1);
			if (keyCode3 == KeyCode.None)
			{
				MainViewModel.Instance.OptionsHotKey1 = text2;
			}
			else
			{
				MainViewModel.Instance.OptionsHotKey1 = GetKeyCodeString(keyCode3);
			}
			if (keyCode4 == KeyCode.None)
			{
				MainViewModel.Instance.OptionsHotKey2 = text2;
			}
			else
			{
				MainViewModel.Instance.OptionsHotKey2 = GetKeyCodeString(keyCode4);
			}
			ConfigSettings.SetDirty();
			Save();
			KeyManager.instance.HotKeySelectorMode = false;
			MainViewModel.Instance.OptionsHotKeySelectVis = Visibility.Visible;
			MainViewModel.Instance.OptionsHotKeyChangeVis = Visibility.Hidden;
			break;
		}
		case -106:
		{
			KeyManager.instance.SetNewKey(selectedFunction, -1, selectedColumn);
			string text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 93);
			KeyCode keyCode = KeyManager.instance.GetKeyCode(selectedFunction, 0);
			KeyCode keyCode2 = KeyManager.instance.GetKeyCode(selectedFunction, 1);
			if (keyCode == KeyCode.None)
			{
				MainViewModel.Instance.OptionsHotKey1 = text;
			}
			else
			{
				MainViewModel.Instance.OptionsHotKey1 = GetKeyCodeString(keyCode);
			}
			if (keyCode2 == KeyCode.None)
			{
				MainViewModel.Instance.OptionsHotKey2 = text;
			}
			else
			{
				MainViewModel.Instance.OptionsHotKey2 = GetKeyCodeString(keyCode2);
			}
			ConfigSettings.SetDirty();
			Save();
			KeyManager.instance.HotKeySelectorMode = false;
			MainViewModel.Instance.OptionsHotKeySelectVis = Visibility.Visible;
			MainViewModel.Instance.OptionsHotKeyChangeVis = Visibility.Hidden;
			break;
		}
		}
	}

	public void Save()
	{
		ConfigSettings.Settings_UserName = RefTextBoxChangeName.Text;
		ConfigSettings.SaveSettings();
		lastDynamicChanged = DateTime.MaxValue;
	}

	private void UpdateMenus()
	{
		for (int i = 0; i < 5; i++)
		{
			if (menuSection == i)
			{
				MainViewModel.Instance.OptionsSectionsBorders[i] = Visibility.Visible;
			}
			else
			{
				MainViewModel.Instance.OptionsSectionsBorders[i] = Visibility.Hidden;
			}
		}
		RefVideoSettings.Visibility = Visibility.Hidden;
		RefSoundSettings.Visibility = Visibility.Hidden;
		RefKeySettings.Visibility = Visibility.Hidden;
		RefControlSettings.Visibility = Visibility.Hidden;
		RefCheatSettings.Visibility = Visibility.Hidden;
		RefNameSettings.Visibility = Visibility.Hidden;
		switch (menuSection)
		{
		case 0:
			RefVideoSettings.Visibility = Visibility.Visible;
			UpdateResListbox();
			break;
		case 1:
			RefSoundSettings.Visibility = Visibility.Visible;
			break;
		case 2:
			RefKeySettings.Visibility = Visibility.Visible;
			break;
		case 3:
			RefControlSettings.Visibility = Visibility.Visible;
			break;
		case 4:
			RefNameSettings.Visibility = Visibility.Visible;
			break;
		case 7:
			RefCheatSettings.Visibility = Visibility.Visible;
			break;
		case 5:
		case 6:
			break;
		}
	}

	private void UpdateControls()
	{
		if (ConfigSettings.Settings_PushMapScrolling)
		{
			MainViewModel.Instance.OptionsPushEnabled = Visibility.Visible;
			MainViewModel.Instance.OptionsPushDisabled = Visibility.Hidden;
		}
		else
		{
			MainViewModel.Instance.OptionsPushEnabled = Visibility.Hidden;
			MainViewModel.Instance.OptionsPushDisabled = Visibility.Visible;
		}
		if (ConfigSettings.Settings_SH1RTSControls)
		{
			MainViewModel.Instance.OptionsSH1RTS = Visibility.Visible;
			MainViewModel.Instance.OptionsDERTS = Visibility.Hidden;
		}
		else
		{
			MainViewModel.Instance.OptionsSH1RTS = Visibility.Hidden;
			MainViewModel.Instance.OptionsDERTS = Visibility.Visible;
		}
		if (ConfigSettings.Settings_SH1MouseWheel)
		{
			MainViewModel.Instance.OptionsWheelSH1 = Visibility.Visible;
			MainViewModel.Instance.OptionsWheelZoom = Visibility.Hidden;
		}
		else
		{
			MainViewModel.Instance.OptionsWheelSH1 = Visibility.Hidden;
			MainViewModel.Instance.OptionsWheelZoom = Visibility.Visible;
		}
		if (ConfigSettings.Settings_SH1CentreControls)
		{
			MainViewModel.Instance.OptionsCenteringSH1 = Visibility.Visible;
			MainViewModel.Instance.OptionsCenteringModern = Visibility.Hidden;
		}
		else
		{
			MainViewModel.Instance.OptionsCenteringSH1 = Visibility.Hidden;
			MainViewModel.Instance.OptionsCenteringModern = Visibility.Visible;
		}
	}

	private void UpdateCursors()
	{
		if (ConfigSettings.Settings_CursorStyle == 0)
		{
			PropEx.SetSprite1(RefCursorSystemButton, MainViewModel.Instance.GameSprites[265]);
			PropEx.SetSprite2(RefCursorSystemButton, MainViewModel.Instance.GameSprites[263]);
			PropEx.SetSprite3(RefCursorSystemButton, MainViewModel.Instance.GameSprites[263]);
			PropEx.SetSprite4(RefCursorSystemButton, MainViewModel.Instance.GameSprites[265]);
			PropEx.SetSprite1(RefCursorSwordButton, MainViewModel.Instance.GameSprites[267]);
			PropEx.SetSprite2(RefCursorSwordButton, MainViewModel.Instance.GameSprites[267]);
			PropEx.SetSprite3(RefCursorSwordButton, MainViewModel.Instance.GameSprites[267]);
			PropEx.SetSprite4(RefCursorSwordButton, MainViewModel.Instance.GameSprites[267]);
			PropEx.SetSprite1(RefCursorSwordXButton, MainViewModel.Instance.GameSprites[268]);
			PropEx.SetSprite2(RefCursorSwordXButton, MainViewModel.Instance.GameSprites[266]);
			PropEx.SetSprite3(RefCursorSwordXButton, MainViewModel.Instance.GameSprites[266]);
			PropEx.SetSprite4(RefCursorSwordXButton, MainViewModel.Instance.GameSprites[268]);
		}
		else if (ConfigSettings.Settings_CursorStyle == 1)
		{
			PropEx.SetSprite1(RefCursorSystemButton, MainViewModel.Instance.GameSprites[264]);
			PropEx.SetSprite2(RefCursorSystemButton, MainViewModel.Instance.GameSprites[264]);
			PropEx.SetSprite3(RefCursorSystemButton, MainViewModel.Instance.GameSprites[264]);
			PropEx.SetSprite4(RefCursorSystemButton, MainViewModel.Instance.GameSprites[264]);
			PropEx.SetSprite1(RefCursorSwordButton, MainViewModel.Instance.GameSprites[268]);
			PropEx.SetSprite2(RefCursorSwordButton, MainViewModel.Instance.GameSprites[266]);
			PropEx.SetSprite3(RefCursorSwordButton, MainViewModel.Instance.GameSprites[266]);
			PropEx.SetSprite4(RefCursorSwordButton, MainViewModel.Instance.GameSprites[268]);
			PropEx.SetSprite1(RefCursorSwordXButton, MainViewModel.Instance.GameSprites[268]);
			PropEx.SetSprite2(RefCursorSwordXButton, MainViewModel.Instance.GameSprites[266]);
			PropEx.SetSprite3(RefCursorSwordXButton, MainViewModel.Instance.GameSprites[266]);
			PropEx.SetSprite4(RefCursorSwordXButton, MainViewModel.Instance.GameSprites[268]);
		}
		else if (ConfigSettings.Settings_CursorStyle == 2)
		{
			PropEx.SetSprite1(RefCursorSystemButton, MainViewModel.Instance.GameSprites[265]);
			PropEx.SetSprite2(RefCursorSystemButton, MainViewModel.Instance.GameSprites[263]);
			PropEx.SetSprite3(RefCursorSystemButton, MainViewModel.Instance.GameSprites[263]);
			PropEx.SetSprite4(RefCursorSystemButton, MainViewModel.Instance.GameSprites[265]);
			PropEx.SetSprite1(RefCursorSwordXButton, MainViewModel.Instance.GameSprites[267]);
			PropEx.SetSprite2(RefCursorSwordXButton, MainViewModel.Instance.GameSprites[267]);
			PropEx.SetSprite3(RefCursorSwordXButton, MainViewModel.Instance.GameSprites[267]);
			PropEx.SetSprite4(RefCursorSwordXButton, MainViewModel.Instance.GameSprites[267]);
			PropEx.SetSprite1(RefCursorSwordButton, MainViewModel.Instance.GameSprites[268]);
			PropEx.SetSprite2(RefCursorSwordButton, MainViewModel.Instance.GameSprites[266]);
			PropEx.SetSprite3(RefCursorSwordButton, MainViewModel.Instance.GameSprites[266]);
			PropEx.SetSprite4(RefCursorSwordButton, MainViewModel.Instance.GameSprites[268]);
		}
		if (ConfigSettings.Settings_Scribe == 0)
		{
			PropEx.SetSprite1(RefScribeClassicButton, MainViewModel.Instance.GameSprites[341]);
			PropEx.SetSprite2(RefScribeClassicButton, MainViewModel.Instance.GameSprites[341]);
			PropEx.SetSprite3(RefScribeClassicButton, MainViewModel.Instance.GameSprites[341]);
			PropEx.SetSprite4(RefScribeClassicButton, MainViewModel.Instance.GameSprites[341]);
			PropEx.SetSprite1(RefScribeModernButton, MainViewModel.Instance.GameSprites[345]);
			PropEx.SetSprite2(RefScribeModernButton, MainViewModel.Instance.GameSprites[343]);
			PropEx.SetSprite3(RefScribeModernButton, MainViewModel.Instance.GameSprites[343]);
			PropEx.SetSprite4(RefScribeModernButton, MainViewModel.Instance.GameSprites[345]);
		}
		else if (ConfigSettings.Settings_Scribe == 2)
		{
			PropEx.SetSprite1(RefScribeClassicButton, MainViewModel.Instance.GameSprites[342]);
			PropEx.SetSprite2(RefScribeClassicButton, MainViewModel.Instance.GameSprites[340]);
			PropEx.SetSprite3(RefScribeClassicButton, MainViewModel.Instance.GameSprites[340]);
			PropEx.SetSprite4(RefScribeClassicButton, MainViewModel.Instance.GameSprites[342]);
			PropEx.SetSprite1(RefScribeModernButton, MainViewModel.Instance.GameSprites[344]);
			PropEx.SetSprite2(RefScribeModernButton, MainViewModel.Instance.GameSprites[344]);
			PropEx.SetSprite3(RefScribeModernButton, MainViewModel.Instance.GameSprites[344]);
			PropEx.SetSprite4(RefScribeModernButton, MainViewModel.Instance.GameSprites[344]);
		}
	}

	private void RefResolutionCombo_SelectionChanged(object sender, SelectionChangedEventArgs args)
	{
		if (RefResolutionCombo.SelectedItem != null)
		{
			resChanged = true;
			MainViewModel.Instance.OptionsApplyVisible = Visibility.Visible;
		}
	}

	private void RefScreenModeCombo_SelectionChanged(object sender, SelectionChangedEventArgs args)
	{
		if (RefScreenModeCombo.SelectedItem != null)
		{
			screenModeChanged = true;
			MainViewModel.Instance.OptionsApplyVisible = Visibility.Visible;
		}
	}

	private void UpdateResListbox(bool fromSettings = false)
	{
		int num;
		int num2;
		int num3;
		if (!fromSettings)
		{
			num = Screen.width;
			num2 = Screen.height;
			num3 = Screen.currentResolution.refreshRate;
			if (ConfigSettings.Settings_Vsync)
			{
				num3 = Application.targetFrameRate;
			}
		}
		else
		{
			num = ConfigSettings.Settings_LastFullscreenWidth;
			num2 = ConfigSettings.Settings_LastFullscreenHeight;
			num3 = ConfigSettings.Settings_LastFullscreenRefresh;
		}
		RefResolutionCombo.SelectedItem = null;
		ItemCollection.Enumerator enumerator = RefResolutionCombo.Items.GetEnumerator();
		while (enumerator.MoveNext())
		{
			ComboBoxItem comboBoxItem = (ComboBoxItem)enumerator.Current;
			Resolution resolution = (Resolution)comboBoxItem.Tag;
			if (num == resolution.width && num2 == resolution.height && Math.Abs(resolution.refreshRate - num3) < 2)
			{
				RefResolutionCombo.SelectedItem = comboBoxItem;
				break;
			}
		}
	}

	private void MasterVolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		if (panelActive)
		{
			int num = (int)RefMasterVolumeSlider.Value;
			MainViewModel.Instance.MasterVolumeValue = num.ToString();
			ConfigSettings.Settings_MasterVolume = (float)num / 100f;
			MyAudioManager.Instance.updateSFXVolumeFromSettings();
			MyAudioManager.Instance.updateSpeechVolumeFromSettings();
			MyAudioManager.Instance.updateMusicVolumeFromSettings();
			lastDynamicChanged = DateTime.UtcNow.AddSeconds(2.0);
		}
	}

	private void MusicVolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		if (panelActive)
		{
			int num = (int)RefMusicVolumeSlider.Value;
			MainViewModel.Instance.MusicVolumeValue = num.ToString();
			ConfigSettings.Settings_MusicVolume = (float)num / 100f;
			MyAudioManager.Instance.updateMusicVolumeFromSettings();
			lastDynamicChanged = DateTime.UtcNow.AddSeconds(2.0);
		}
	}

	private void SpeechVolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		if (panelActive)
		{
			int num = (int)RefSpeechVolumeSlider.Value;
			MainViewModel.Instance.SpeechVolumeValue = num.ToString();
			ConfigSettings.Settings_SpeechVolume = (float)num / 100f;
			MyAudioManager.Instance.updateSpeechVolumeFromSettings();
			lastDynamicChanged = DateTime.UtcNow.AddSeconds(2.0);
		}
	}

	private void UnitSpeechVolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		if (panelActive)
		{
			int num = (int)RefUnitSpeechVolumeSlider.Value;
			MainViewModel.Instance.UnitSpeechVolumeValue = num.ToString();
			ConfigSettings.Settings_UnitSpeechVolume = (float)num / 100f;
			MyAudioManager.Instance.updateSpeechVolumeFromSettings();
			lastDynamicChanged = DateTime.UtcNow.AddSeconds(2.0);
		}
	}

	private void SFXVolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		if (panelActive)
		{
			int num = (int)RefSFXVolumeSlider.Value;
			MainViewModel.Instance.SfxVolumeValue = num.ToString();
			ConfigSettings.Settings_SFXVolume = (float)num / 100f;
			MyAudioManager.Instance.updateSFXVolumeFromSettings();
			lastDynamicChanged = DateTime.UtcNow.AddSeconds(2.0);
		}
	}

	private void ScrollSpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		if (panelActive)
		{
			int settings_ScrollSpeed = (int)RefScrollSpeedSlider.Value;
			MainViewModel.Instance.ScrollSpeedValue = settings_ScrollSpeed.ToString();
			ConfigSettings.Settings_ScrollSpeed = settings_ScrollSpeed;
			lastDynamicChanged = DateTime.UtcNow.AddSeconds(2.0);
		}
	}

	private void GameSpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		if (panelActive)
		{
			int num = (int)RefGameSpeedSlider.Value * 5;
			if (!Director.instance.MultiplayerGame)
			{
				MainViewModel.Instance.GameSpeedValue = num.ToString();
				Director.instance.SetEngineFrameRate(num);
				ConfigSettings.Settings_GameSpeed = num;
				lastDynamicChanged = DateTime.UtcNow.AddSeconds(2.0);
			}
		}
	}

	private void UIScaleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		if (panelActive)
		{
			MainViewModel.Instance.OptionsScaleApplyVisible = Visibility.Visible;
		}
	}

	private void LockCursor_ValueChanged(object sender, RoutedEventArgs e)
	{
		if (panelActive)
		{
			ConfigSettings.Settings_LockCursor = RefLockCursorCheck.IsChecked.Value;
			Save();
		}
	}

	private void BuildingTooltipsCheck_ValueChanged(object sender, RoutedEventArgs e)
	{
		if (panelActive)
		{
			ConfigSettings.Settings_ShowBuildingTooltips = RefBuildingTooltipsCheck.IsChecked.Value;
			Save();
		}
	}

	private void SteamHelp_ValueChanged(object sender, RoutedEventArgs e)
	{
		if (panelActive)
		{
			ConfigSettings.Settings_UseSteamOverlayForHelp = RefSteamHelpCheck.IsChecked.Value;
			Save();
		}
	}

	private void CompassCheck_ValueChanged(object sender, RoutedEventArgs e)
	{
		if (panelActive)
		{
			ConfigSettings.Settings_Compass = RefCompassCheck.IsChecked.Value;
			Save();
			if (Director.instance.SimRunning)
			{
				MainViewModel.Instance.IngameUI.setRotationImage(GameMap.instance.CurrentRotation());
				MainViewModel.Instance.Compass_Vis = ConfigSettings.Settings_Compass;
			}
		}
	}

	private void RadarZoomCheck_ValueChanged(object sender, RoutedEventArgs e)
	{
		if (panelActive)
		{
			ConfigSettings.Settings_RadarDefaultZoomedOut = RefRadarZoomCheck.IsChecked.Value;
			Save();
		}
	}

	private void CustomIntros_ValueChanged(object sender, RoutedEventArgs e)
	{
		if (panelActive)
		{
			ConfigSettings.Settings_CustomIntros = RefCustomIntros.IsChecked.Value;
			Save();
		}
	}

	private void UISounds_ValueChanged(object sender, RoutedEventArgs e)
	{
		if (panelActive)
		{
			ConfigSettings.Settings_PlayUISFX = RefUISoundsCheck.IsChecked.Value;
			Save();
		}
	}

	private void ReduceSounds_ValueChanged(object sender, RoutedEventArgs e)
	{
		if (panelActive)
		{
			ConfigSettings.Settings_ReduceMusicVolumeForSpeech = RefReduceSoundsCheck.IsChecked.Value;
			Save();
		}
	}

	private void CheatKeys_ValueChanged(object sender, RoutedEventArgs e)
	{
		if (panelActive)
		{
			ConfigSettings.Settings_CheatKeysEnabled = RefCheatKeysCheck.IsChecked.Value;
			Save();
		}
	}

	private void Pings_ValueChanged(object sender, RoutedEventArgs e)
	{
		if (panelActive)
		{
			ConfigSettings.Settings_ShowPings = RefPingsCheck.IsChecked.Value;
			Save();
		}
	}

	private void ExtraZoom_ValueChanged(object sender, RoutedEventArgs e)
	{
		if (panelActive)
		{
			ConfigSettings.Settings_ExtraZoom = RefExtraZoomCheck.IsChecked.Value;
			Save();
		}
	}

	private void VSyncCheck_ValueChanged(object sender, RoutedEventArgs e)
	{
		if (panelActive)
		{
			MainViewModel.Instance.OptionsApplyVisible = Visibility.Visible;
		}
	}

	public static void SetVSync(bool state)
	{
		if (state)
		{
			if ((Screen.fullScreenMode == FullScreenMode.FullScreenWindow || Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen) && ConfigSettings.Settings_LastFullscreenRefresh > 0)
			{
				Application.targetFrameRate = ConfigSettings.Settings_LastFullscreenRefresh;
			}
			else
			{
				Application.targetFrameRate = Screen.currentResolution.refreshRate;
			}
			QualitySettings.vSyncCount = 1;
			ConfigSettings.Settings_Vsync = true;
		}
		else
		{
			Application.targetFrameRate = 300;
			QualitySettings.vSyncCount = 0;
			ConfigSettings.Settings_Vsync = false;
		}
	}

	public void UpdateUIScaleSliderVis()
	{
		if (Screen.width <= 1366 || Screen.height <= 768)
		{
			RefUIScaleGrid.Visibility = Visibility.Hidden;
		}
		else
		{
			RefUIScaleGrid.Visibility = Visibility.Visible;
		}
	}

	private void InitializeComponent()
	{
		NoesisUnity.LoadComponent(this, "Assets/GUI/XAMLResources/HUD_Options.xaml");
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
		if (eventName == "MouseEnter" && handlerName == "ChickenButtonEnter")
		{
			if (source is Button)
			{
				((Button)source).MouseEnter += ChickenButtonEnter;
			}
			return true;
		}
		return false;
	}

	public void ChickenButtonEnter(object sender, MouseEventArgs e)
	{
		SFXManager.instance.playUISound(137);
	}

	private void TextInputFocus(object sender, DependencyPropertyChangedEventArgs e)
	{
		MainViewModel.Instance.SetNoesisKeyboardState((bool)e.NewValue);
	}

	private void CreateHotkeyList()
	{
		hotKeyRows.Clear();
		string text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 93);
		HotKeyEntry[] array;
		if (hotKeyTextDict == null)
		{
			hotKeyTextDict = new Dictionary<Enums.KeyFunctions, int>();
			array = hotKeyList;
			foreach (HotKeyEntry hotKeyEntry in array)
			{
				hotKeyTextDict[hotKeyEntry.function] = hotKeyEntry.textID;
			}
		}
		array = hotKeyList;
		foreach (HotKeyEntry hotKeyEntry2 in array)
		{
			HotKeyRow hotKeyRow = new HotKeyRow(this);
			hotKeyRow.Text1 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, hotKeyEntry2.textID);
			KeyCode keyCode = KeyManager.instance.GetKeyCode(hotKeyEntry2.function, 0);
			KeyCode keyCode2 = KeyManager.instance.GetKeyCode(hotKeyEntry2.function, 1);
			if (keyCode == KeyCode.None && keyCode2 == KeyCode.None)
			{
				hotKeyRow.Text2 = text;
			}
			else if (keyCode2 == KeyCode.None)
			{
				hotKeyRow.Text2 = GetKeyCodeString(keyCode);
			}
			else
			{
				hotKeyRow.Text2 = GetKeyCodeString(keyCode) + " / " + GetKeyCodeString(keyCode2);
			}
			int function = (int)hotKeyEntry2.function;
			hotKeyRow.DataValue = function.ToString();
			hotKeyRow.iDataValue = (int)hotKeyEntry2.function;
			hotKeyRows.Add(hotKeyRow);
		}
		RefHotKeyList.ItemsSource = hotKeyRows;
	}

	public static string GetKeyCodeString(KeyCode code)
	{
		int num = (int)(code & (KeyCode)65535);
		bool num2 = (code & (KeyCode)65536) > KeyCode.None;
		bool flag = (code & (KeyCode)131072) > KeyCode.None;
		bool flag2 = (code & (KeyCode)262144) > KeyCode.None;
		string text = "";
		if (num2)
		{
			text = text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 78) + " ";
		}
		if (flag)
		{
			text = text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 79) + " ";
		}
		if (flag2)
		{
			text = text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 80) + " ";
		}
		switch (num)
		{
		case 48:
			return text + "0";
		case 49:
			return text + "1";
		case 50:
			return text + "2";
		case 51:
			return text + "3";
		case 52:
			return text + "4";
		case 53:
			return text + "5";
		case 54:
			return text + "6";
		case 55:
			return text + "7";
		case 56:
			return text + "8";
		case 57:
			return text + "9";
		case 96:
			return text + "`";
		case 92:
			return text + "\\";
		case 45:
			return text + "-";
		case 61:
			return text + "=";
		case 91:
			return text + "[";
		case 93:
			return text + "]";
		case 59:
			return text + ";";
		case 39:
			return text + "'";
		case 44:
			return text + ",";
		case 46:
			return text + ".";
		case 47:
			return text + "/";
		case 32:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 109);
		case 256:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 81) + " 0";
		case 257:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 81) + " 1";
		case 258:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 81) + " 2";
		case 259:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 81) + " 3";
		case 260:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 81) + " 4";
		case 261:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 81) + " 5";
		case 262:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 81) + " 6";
		case 263:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 81) + " 7";
		case 264:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 81) + " 8";
		case 265:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 81) + " 9";
		case 325:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 115) + " 2";
		case 326:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 115) + " 3";
		case 327:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 115) + " 4";
		case 328:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 115) + " 5";
		case 329:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 115) + " 6";
		case 266:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 81) + " .";
		case 270:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 81) + " +";
		case 269:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 81) + " -";
		case 267:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 81) + " /";
		case 268:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 81) + " *";
		case 271:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 81) + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 82);
		case 272:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 81) + " =";
		case 9:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 83);
		case 301:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 84);
		case 13:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 85);
		case 8:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 86);
		case 273:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 87);
		case 274:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 88);
		case 275:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 89);
		case 276:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 90);
		case 277:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 91);
		case 278:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 92);
		case 279:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 93);
		case 280:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 94);
		case 281:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 95);
		case 127:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 96);
		case 300:
			return text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 97);
		default:
		{
			string text2 = text;
			KeyCode keyCode = (KeyCode)num;
			return text2 + keyCode;
		}
		}
	}
}
