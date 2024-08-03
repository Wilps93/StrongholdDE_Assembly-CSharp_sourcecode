using System;
using System.Collections.Generic;
using Noesis;
using NoesisApp;
using Steamworks;
using UnityEngine;

namespace Stronghold1DE;

public class FrontendMenus : UserControl
{
	public static int CurrentSelectedMission = -1;

	public static int CurrentSelectedEcoMission = -1;

	public static int CurrentSelectedTrailMission = -1;

	public static int CurrentSelectedTrail2Mission = -1;

	private int CurrentSelectedExtraMission = -1;

	public static int CurrentSelectedExtra1Mission = -1;

	public static int CurrentSelectedExtra2Mission = -1;

	public static int CurrentSelectedExtra3Mission = -1;

	public static int CurrentSelectedExtra4Mission = -1;

	public static int CurrentSelectedExtraEcoMission = -1;

	public static int CurrentSelectedTrail = 0;

	private static bool ControlSchemeOptionsShown = false;

	private bool ControlSchemeModernSelected = true;

	private static bool ForceShowTutorialHelp = false;

	public Storyboard refModernControlsEnlarge;

	public Storyboard refModernControlsShrink;

	public Storyboard refClassicControlsEnlarge;

	public Storyboard refClassicControlsShrink;

	public static Storyboard refHandButtonAnim;

	public static TextBlock RefDeselectText;

	public Storyboard refShowModernKeybinds;

	public Storyboard refShowClassicKeybinds;

	public Storyboard refMainMenu_ShowMainMenu;

	public Storyboard refMainMenu_ShowCombat;

	public Storyboard refMainMenu_ShowEco;

	public Storyboard refMainMenu_ShowDLC;

	public Storyboard refMainMenu_ShowTrails;

	public MediaElement refFrontendBackVideo;

	public Image refLogoMain;

	public Image refLogoLoading;

	private bool loopBackgroundVideo;

	private bool dlcsChecked;

	private bool DLC1Owned;

	private bool DLC2Owned;

	private DateTime lastRolloverSoundTime = DateTime.MinValue;

	private int[] trailLocations = new int[20]
	{
		1899, 604, 1965, 721, 1607, 997, 1949, 962, 2259, 1048,
		2200, 1069, 2365, 1221, 2562, 1210, 3112, 1107, 3380, 1261
	};

	private int[] trail2Locations = new int[20]
	{
		1824, 1688, 2344, 968, 1712, 1214, 2670, 746, 2658, 600,
		2850, 1206, 2210, 950, 3150, 838, 2496, 1186, 3012, 736
	};

	public FrontendMenus()
	{
		base.DataContext = MainViewModel.Instance;
		MainViewModel.Instance.FrontEndMenu = this;
		InitializeComponent();
		refFrontendBackVideo = (MediaElement)FindName("FrontendBackVideo");
		refFrontendBackVideo.MediaEnded += FrontendBackVideo_Ended;
		refFrontendBackVideo.MediaOpened += FrontendBackVideo_Opened;
		Uri source;
		if (Screen.width <= 1920 && Screen.height <= 1080)
		{
			source = new Uri("Assets/GUI/Video/front_end_background_low.webm", UriKind.Relative);
			loopBackgroundVideo = true;
		}
		else
		{
			source = new Uri("Assets/GUI/Video/front_end_background.webm", UriKind.Relative);
			loopBackgroundVideo = true;
		}
		refFrontendBackVideo.Source = source;
		refModernControlsEnlarge = (Storyboard)TryFindResource("EnlargeModern");
		refModernControlsShrink = (Storyboard)TryFindResource("ShrinkModern");
		refClassicControlsEnlarge = (Storyboard)TryFindResource("EnlargeClassic");
		refClassicControlsShrink = (Storyboard)TryFindResource("ShrinkClassic");
		refHandButtonAnim = (Storyboard)TryFindResource("HandButtonAnim");
		refMainMenu_ShowMainMenu = (Storyboard)TryFindResource("MainMenu_ShowMainMenu");
		refMainMenu_ShowCombat = (Storyboard)TryFindResource("MainMenu_ShowCombat");
		refMainMenu_ShowEco = (Storyboard)TryFindResource("MainMenu_ShowEco");
		refMainMenu_ShowDLC = (Storyboard)TryFindResource("MainMenu_ShowDLC");
		refMainMenu_ShowTrails = (Storyboard)TryFindResource("MainMenu_ShowTrails");
		refShowModernKeybinds = (Storyboard)TryFindResource("ShowModernKeybinds");
		refShowClassicKeybinds = (Storyboard)TryFindResource("ShowClassicKeybinds");
		RefDeselectText = (TextBlock)FindName("DeselectText");
		refLogoMain = (Image)FindName("LogoMain");
		refLogoLoading = (Image)FindName("LogoLoading");
		refLogoMain.Source = MainViewModel.Instance.GetImage(Enums.eImages.IMAGE_FRONTEND_LOGO);
		refLogoLoading.Source = MainViewModel.Instance.GetImage(Enums.eImages.IMAGE_FRONTEND_LOGO);
	}

	public static void ClearUIPanels(bool frontEndState = true)
	{
		MainViewModel.Instance.Show_Frontend = frontEndState;
		MainViewModel.Instance.Show_Frontend_MainMenu = false;
		MainViewModel.Instance.Show_Frontend_Combat = false;
		MainViewModel.Instance.Show_FrontEndCombat_Main_Help = false;
		MainViewModel.Instance.Show_FrontEndCombat_Jewel_Help = false;
		MainViewModel.Instance.Show_Frontend_Eco = false;
		MainViewModel.Instance.Show_Frontend_Jewel = false;
		MainViewModel.Instance.Show_Frontend_Trails = false;
		MainViewModel.Instance.Show_Frontend_Controls_Selection = false;
		MainViewModel.Instance.Show_FrontMenus = true;
		MainViewModel.Instance.Show_CampaignMenu = false;
		MainViewModel.Instance.Show_EcoCampaignMenu = false;
		MainViewModel.Instance.Show_Extra1CampaignMenu = false;
		MainViewModel.Instance.Show_Extra2CampaignMenu = false;
		MainViewModel.Instance.Show_Extra3CampaignMenu = false;
		MainViewModel.Instance.Show_Extra4CampaignMenu = false;
		MainViewModel.Instance.Show_ExtraEcoCampaignMenu = false;
		MainViewModel.Instance.Show_TrailCampaignMenu = false;
		MainViewModel.Instance.Show_Trail2CampaignMenu = false;
		MainViewModel.Instance.Show_MapScreenMenuMode = true;
		MainViewModel.Instance.Show_MapScreenStoryMode = false;
		MainViewModel.Instance.Show_StandaloneSetup = false;
		MainViewModel.Instance.Show_MultiplayerSetup = false;
		MainViewModel.Instance.Show_Credits = false;
		MainViewModel.Instance.Show_MapEditor = false;
		MainViewModel.Instance.ShowRat = false;
		MainViewModel.Instance.ShowSnake = false;
		MainViewModel.Instance.ShowPig = false;
		MainViewModel.Instance.ShowWolf = false;
		MainViewModel.Instance.Show_Frontend_Logo = frontEndState;
		refHandButtonAnim.Stop();
	}

	public static void OpenFrontEndMenus()
	{
		MainViewModel.Instance.FrontEndMenu.Init();
		ClearUIPanels();
		MainViewModel.Instance.Show_FrontMenus = true;
		switch (FatControler.locale)
		{
		case "ruru":
			MainViewModel.Instance.FrontEndButtonMargin = "107,0,0,32";
			MainViewModel.Instance.DemoFrontEndButtonFontSize = 43.0;
			break;
		case "frfr":
			MainViewModel.Instance.FrontEndButtonDLCMargin = "107,0,0,32";
			break;
		case "dede":
			MainViewModel.Instance.FrontEndButtonDLCMargin = "107,0,0,32";
			break;
		case "eses":
			MainViewModel.Instance.FrontEndButtonDLCMargin = "107,0,0,32";
			break;
		case "itit":
			MainViewModel.Instance.FrontEndButtonMargin = "92,0,0,32";
			MainViewModel.Instance.FrontEndButtonDLCMargin = "107,0,0,32";
			break;
		case "plpl":
			MainViewModel.Instance.FrontEndButtonMargin = "92,0,0,32";
			MainViewModel.Instance.FrontEndHomeFiresButtonMargin = "110,0,-8,32";
			MainViewModel.Instance.FrontEndHomeFiresButtonFontSize = 43.0;
			break;
		case "zhcn":
			MainViewModel.Instance.FrontEndButtonMargin = "92,0,0,32";
			break;
		case "zhhk":
			MainViewModel.Instance.FrontEndButtonMargin = "92,0,0,32";
			break;
		case "thth":
			MainViewModel.Instance.FrontEndButtonMargin = "102,0,0,32";
			break;
		case "ukua":
			MainViewModel.Instance.FrontEndHomeFiresButtonMargin = "110,0,-8,32";
			break;
		case "huhu":
			RefDeselectText.Width = 300f;
			break;
		case "elgr":
			MainViewModel.Instance.FrontEndButtonLineHeight = 35.0;
			break;
		}
		if (!ConfigSettings.SettingsFileExisted && !ControlSchemeOptionsShown)
		{
			ControlSchemeOptionsShown = true;
			ForceShowTutorialHelp = true;
			MainViewModel.Instance.Show_Frontend_Controls_Selection = true;
			MainViewModel.Instance.FrontEndMenu.refModernControlsEnlarge.Begin();
			MainViewModel.Instance.Show_Frontend_Logo = false;
			refHandButtonAnim.Begin();
		}
		else
		{
			MainViewModel.Instance.Show_Frontend_MainMenu = true;
			MainViewModel.Instance.FrontEndMenu.refMainMenu_ShowMainMenu.Begin();
		}
	}

	private void Init()
	{
		if (!dlcsChecked)
		{
			dlcsChecked = true;
			if (SteamManager.Initialized)
			{
				if (SteamApps.BIsDlcInstalled(new AppId_t(2727990u)))
				{
					DLC1Owned = true;
				}
				if (SteamApps.BIsDlcInstalled(new AppId_t(2728000u)))
				{
					DLC2Owned = true;
				}
			}
		}
		bool flag = Translate.Instance.ExtraText_3_Loaded && DLC1Owned;
		bool flag2 = Translate.Instance.ExtraText_4_Loaded && DLC2Owned;
		MainViewModel.Instance.Extra2Visible = Translate.Instance.ExtraText_2_Loaded;
		MainViewModel.Instance.Extra3Visible = flag;
		MainViewModel.Instance.Extra4Visible = flag2;
		MainViewModel.Instance.Extra3NOTVisible = !flag;
		MainViewModel.Instance.Extra4NOTVisible = !flag2;
		if (CurrentSelectedMission < 0)
		{
			CurrentSelectedMission = ConfigSettings.Settings_Progress_Campaign;
			CurrentSelectedEcoMission = ConfigSettings.Settings_Progress_EcoCampaign;
			CurrentSelectedTrailMission = ConfigSettings.Settings_Progress_Trail;
			CurrentSelectedTrail2Mission = ConfigSettings.Settings_Progress_Trail2;
			CurrentSelectedExtraMission = (CurrentSelectedExtra1Mission = ConfigSettings.Settings_Progress_Extra1Campaign + 10);
			CurrentSelectedExtra2Mission = ConfigSettings.Settings_Progress_Extra2Campaign + 20;
			CurrentSelectedExtra3Mission = ConfigSettings.Settings_Progress_Extra3Campaign + 30;
			CurrentSelectedExtraEcoMission = ConfigSettings.Settings_Progress_ExtraEcoCampaign;
			CurrentSelectedExtra4Mission = ConfigSettings.Settings_Progress_Extra4Campaign + 40;
			if (CurrentSelectedMission > 21)
			{
				CurrentSelectedMission = 21;
			}
			if (CurrentSelectedEcoMission > 5)
			{
				CurrentSelectedEcoMission = 5;
			}
			if (CurrentSelectedExtraEcoMission > 7)
			{
				CurrentSelectedExtraEcoMission = 7;
			}
			if (CurrentSelectedTrailMission > 10)
			{
				CurrentSelectedTrailMission = 10;
			}
			if (CurrentSelectedTrail2Mission > 10)
			{
				CurrentSelectedTrail2Mission = 10;
			}
			if (CurrentSelectedExtra1Mission > 17)
			{
				CurrentSelectedExtra1Mission = (CurrentSelectedExtraMission = 17);
			}
			if (CurrentSelectedExtra2Mission > 27)
			{
				CurrentSelectedExtra2Mission = 27;
			}
			if (CurrentSelectedExtra3Mission > 37)
			{
				CurrentSelectedExtra3Mission = 37;
			}
			if (CurrentSelectedExtra4Mission > 47)
			{
				CurrentSelectedExtra4Mission = 47;
			}
		}
		else
		{
			CurrentSelectedExtraMission = CurrentSelectedExtra1Mission;
		}
		ButtonCampaignClicked(CurrentSelectedMission);
		ButtonEcoCampaignClicked(CurrentSelectedEcoMission);
		ButtonExtraCampaignClicked(CurrentSelectedExtraMission);
		ButtonExtraEcoCampaignClicked(CurrentSelectedExtraEcoMission);
		UpdateTutorialHelpVisibility(state: false);
		Director.instance.setCursor(0, force: true);
	}

	public void ButtonClicked(string param)
	{
		switch (param)
		{
		case "Combat":
			ClearUIPanels();
			MainViewModel.Instance.Show_Frontend_Combat = true;
			MainViewModel.Instance.FrontEndMenu.refMainMenu_ShowCombat.Begin();
			break;
		case "Eco":
			ClearUIPanels();
			MainViewModel.Instance.Show_Frontend_Eco = true;
			MainViewModel.Instance.FrontEndMenu.refMainMenu_ShowEco.Begin();
			break;
		case "Jewel":
			ClearUIPanels();
			MainViewModel.Instance.Show_FrontMenus = true;
			MainViewModel.Instance.Show_Frontend_Jewel = true;
			MainViewModel.Instance.FrontEndMenu.refMainMenu_ShowDLC.Begin();
			break;
		case "Multiplayer":
			ClearUIPanels();
			FRONT_Multiplayer.Open();
			break;
		case "MapEditor":
			ClearUIPanels();
			FRONT_EditorSetup.Open();
			break;
		case "Tutorial":
			ForceShowTutorialHelp = false;
			MainViewModel.Instance.InitNewScene(Enums.SceneIDS.Tutorial);
			break;
		case "Options":
			UpdateFrontMenuPopupScale();
			MainViewModel.Instance.InitNewScene(Enums.SceneIDS.Options);
			break;
		case "Load":
			UpdateFrontMenuPopupScale();
			HUD_LoadSaveRequester.OpenLoadSaveRequester(Enums.RequesterTypes.LoadSinglePlayerGame, delegate(string filename, FileHeader header)
			{
				MainViewModel.Instance.InitNewScene(Enums.SceneIDS.MainGame);
				Director.instance.SetPausedState(state: false);
				EditorDirector.instance.stopGameSim();
				EditorDirector.instance.loadSaveGame(header.filePath, header.standAlone_filename, header);
				MainViewModel.Instance.InitObjectiveGoodsPanelDelayed();
			}, delegate
			{
			});
			break;
		case "Exit":
			UpdateFrontMenuPopupScale();
			SFXManager.instance.playSpeech(1, "General_Message10.wav", 1f);
			HUD_ConfirmationPopup.ShowConfirmation(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 9), delegate
			{
				FatControler.instance.ExitApp();
			}, delegate
			{
			});
			break;
		case "BackMain":
			ClearUIPanels();
			MainViewModel.Instance.Show_Frontend_MainMenu = true;
			MainViewModel.Instance.FrontEndMenu.refMainMenu_ShowMainMenu.Begin();
			break;
		case "LeaveCredits":
			SFXManager.instance.playMusic(3);
			ClearUIPanels();
			MainViewModel.Instance.Show_Frontend_MainMenu = true;
			MainViewModel.Instance.FrontEndMenu.refMainMenu_ShowMainMenu.Begin();
			break;
		case "LeaveControls":
			ClearUIPanels();
			if (ControlSchemeModernSelected)
			{
				ConfigSettings.Settings_PushMapScrolling = false;
				KeyManager.instance.SetDefaultFunctionsNew();
				ConfigSettings.Settings_SH1RTSControls = false;
				ConfigSettings.Settings_SH1MouseWheel = false;
				ConfigSettings.Settings_SH1CentreControls = false;
			}
			else
			{
				ConfigSettings.Settings_PushMapScrolling = true;
				KeyManager.instance.SetDefaultFunctionsSH1();
				ConfigSettings.Settings_SH1RTSControls = true;
				ConfigSettings.Settings_SH1MouseWheel = true;
				ConfigSettings.Settings_SH1CentreControls = true;
			}
			ConfigSettings.SaveSettings();
			MainViewModel.Instance.Show_Frontend_MainMenu = true;
			MainViewModel.Instance.FrontEndMenu.refMainMenu_ShowMainMenu.Begin();
			break;
		case "MainCampaign":
			ClearUIPanels();
			MainViewModel.Instance.Show_CampaignMenu = true;
			ButtonCampaignClicked(CurrentSelectedMission);
			UpdateCampaignListButtonVisibility(ConfigSettings.Settings_Progress_Campaign);
			break;
		case "DLC1":
			CurrentSelectedExtraMission = CurrentSelectedExtra1Mission;
			ClearUIPanels();
			MainViewModel.Instance.Show_Extra1CampaignMenu = true;
			ButtonExtraCampaignClicked(CurrentSelectedExtraMission);
			UpdateCampaignListButtonVisibility(ConfigSettings.Settings_Progress_Extra1Campaign);
			break;
		case "DLC2":
			CurrentSelectedExtraMission = CurrentSelectedExtra2Mission;
			ClearUIPanels();
			MainViewModel.Instance.Show_Extra2CampaignMenu = true;
			ButtonExtraCampaignClicked(CurrentSelectedExtraMission);
			UpdateCampaignListButtonVisibility(ConfigSettings.Settings_Progress_Extra2Campaign);
			break;
		case "DLC3":
			CurrentSelectedExtraMission = CurrentSelectedExtra3Mission;
			ClearUIPanels();
			MainViewModel.Instance.Show_Extra3CampaignMenu = true;
			ButtonExtraCampaignClicked(CurrentSelectedExtraMission);
			UpdateCampaignListButtonVisibility(ConfigSettings.Settings_Progress_Extra3Campaign);
			break;
		case "DLC3NOT":
			SteamFriends.ActivateGameOverlayToStore(new AppId_t(2727990u), EOverlayToStoreFlag.k_EOverlayToStoreFlag_None);
			break;
		case "DLCECO":
			ClearUIPanels();
			MainViewModel.Instance.Show_ExtraEcoCampaignMenu = true;
			ButtonExtraEcoCampaignClicked(CurrentSelectedExtraEcoMission);
			UpdateCampaignListButtonVisibility(ConfigSettings.Settings_Progress_ExtraEcoCampaign);
			break;
		case "DLC4":
			CurrentSelectedExtraMission = CurrentSelectedExtra4Mission;
			ClearUIPanels();
			MainViewModel.Instance.Show_Extra4CampaignMenu = true;
			ButtonExtraCampaignClicked(CurrentSelectedExtraMission);
			UpdateCampaignListButtonVisibility(ConfigSettings.Settings_Progress_Extra4Campaign);
			break;
		case "DLC4NOT":
			SteamFriends.ActivateGameOverlayToStore(new AppId_t(2728000u), EOverlayToStoreFlag.k_EOverlayToStoreFlag_None);
			break;
		case "Siege":
			ClearUIPanels();
			FRONT_StandaloneMission.Open(Enums.StartUpUIPanels.Siege);
			break;
		case "Invasion":
			ClearUIPanels();
			FRONT_StandaloneMission.Open(Enums.StartUpUIPanels.Invasion);
			break;
		case "Trails":
			ClearUIPanels();
			MainViewModel.Instance.Show_FrontMenus = true;
			MainViewModel.Instance.Show_Frontend_Trails = true;
			MainViewModel.Instance.FrontEndMenu.refMainMenu_ShowTrails.Begin();
			break;
		case "Trail":
			ClearUIPanels();
			MainViewModel.Instance.Show_TrailCampaignMenu = true;
			CurrentSelectedTrail = 0;
			ButtonTrailCampaignClicked(CurrentSelectedTrailMission);
			UpdateCampaignListButtonVisibility(ConfigSettings.Settings_Progress_Trail);
			break;
		case "Trail2":
			ClearUIPanels();
			MainViewModel.Instance.Show_Trail2CampaignMenu = true;
			CurrentSelectedTrail = 1;
			ButtonTrailCampaignClicked(CurrentSelectedTrail2Mission);
			UpdateCampaignListButtonVisibility(ConfigSettings.Settings_Progress_Trail2);
			break;
		case "EcoCampaign":
			ClearUIPanels();
			MainViewModel.Instance.Show_EcoCampaignMenu = true;
			ButtonEcoCampaignClicked(CurrentSelectedEcoMission);
			UpdateCampaignListButtonVisibility(ConfigSettings.Settings_Progress_EcoCampaign);
			break;
		case "EcoMission":
			ClearUIPanels();
			FRONT_StandaloneMission.Open(Enums.StartUpUIPanels.EcoMission);
			break;
		case "JustBuild":
			ClearUIPanels();
			FRONT_StandaloneMission.Open(Enums.StartUpUIPanels.FreeBuild);
			break;
		case "DEBUGSTORY":
			MainViewModel.Instance.Show_Story_DEBUG = true;
			MainViewModel.Instance.InitNewScene(Enums.SceneIDS.Story);
			break;
		case "Credits":
			ClearUIPanels();
			MainViewModel.Instance.Show_Credits = true;
			break;
		case "ExitControl":
			UpdateFrontMenuPopupScale();
			HUD_ConfirmationPopup.ShowConfirmation(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 9), delegate
			{
				FatControler.instance.ExitApp();
			}, delegate
			{
			});
			break;
		case "ControlsModern":
			if (!ControlSchemeModernSelected)
			{
				ControlSchemeModernSelected = true;
				refClassicControlsShrink.Begin();
			}
			break;
		case "ControlsClassic":
			if (ControlSchemeModernSelected)
			{
				ControlSchemeModernSelected = false;
				refModernControlsShrink.Begin();
			}
			break;
		}
	}

	private void MouseEnterMainButtonHandler(object sender, MouseEventArgs e)
	{
		if (!(e.Source is Button))
		{
			return;
		}
		switch ((string)((Button)e.Source).CommandParameter)
		{
		case "Exit":
			break;
		case "BackMain":
			break;
		case "Combat":
		case "Eco":
		case "Multiplayer":
		case "MapEditor":
			PlayShieldRollover();
			break;
		case "Tutorial":
			UpdateTutorialHelpVisibility(state: true);
			break;
		case "Options":
		case "Load":
			PlayShieldRollover(0.5f);
			break;
		case "MainCampaign":
			MainViewModel.Instance.Show_FrontEndCombat_Main_Help = true;
			PlayShieldRollover();
			break;
		case "Jewel":
			MainViewModel.Instance.Show_FrontEndCombat_Jewel_Help = true;
			PlayShieldRollover();
			break;
		case "EcoCampaign":
			MainViewModel.Instance.Show_FrontEndEco_Main_Help = true;
			PlayShieldRollover();
			break;
		case "DLCECO":
			MainViewModel.Instance.Show_FrontEndEco_DLC_Help = true;
			PlayShieldRollover();
			break;
		case "DLC3NOT":
			MainViewModel.Instance.Show_FrontEndCombat_DLC_3_Help = true;
			PlayShieldRollover();
			break;
		case "DLC4NOT":
			MainViewModel.Instance.Show_FrontEndCombat_DLC_4_Help = true;
			PlayShieldRollover();
			break;
		case "DLC1":
		case "DLC2":
		case "DLC3":
		case "DLC4":
		case "Siege":
		case "Invasion":
		case "Trail":
			PlayShieldRollover();
			break;
		case "EcoMission":
		case "JustBuild":
			PlayShieldRollover();
			break;
		case "ControlsModern":
			if (!ControlSchemeModernSelected)
			{
				refModernControlsEnlarge.Begin();
				refModernControlsShrink.Stop();
				refShowModernKeybinds.Begin();
				refShowClassicKeybinds.Stop();
			}
			break;
		case "ControlsClassic":
			if (ControlSchemeModernSelected)
			{
				refClassicControlsEnlarge.Begin();
				refClassicControlsShrink.Stop();
				refShowModernKeybinds.Stop();
				refShowClassicKeybinds.Begin();
			}
			break;
		}
	}

	private void MouseLeaveMainButtonHandler(object sender, MouseEventArgs e)
	{
		if (!(e.Source is Button))
		{
			return;
		}
		switch ((string)((Button)e.Source).CommandParameter)
		{
		case "ControlsModern":
			if (!ControlSchemeModernSelected)
			{
				refModernControlsEnlarge.Stop();
				refModernControlsShrink.Begin();
				refShowModernKeybinds.Stop();
				refShowClassicKeybinds.Begin();
			}
			break;
		case "ControlsClassic":
			if (ControlSchemeModernSelected)
			{
				refClassicControlsEnlarge.Stop();
				refClassicControlsShrink.Begin();
				refShowModernKeybinds.Begin();
				refShowClassicKeybinds.Stop();
			}
			break;
		case "Tutorial":
			UpdateTutorialHelpVisibility(state: false);
			break;
		case "MainCampaign":
			MainViewModel.Instance.Show_FrontEndCombat_Main_Help = false;
			break;
		case "Jewel":
			MainViewModel.Instance.Show_FrontEndCombat_Jewel_Help = false;
			break;
		case "EcoCampaign":
			MainViewModel.Instance.Show_FrontEndEco_Main_Help = false;
			break;
		case "DLCECO":
			MainViewModel.Instance.Show_FrontEndEco_DLC_Help = false;
			break;
		case "DLC3NOT":
			MainViewModel.Instance.Show_FrontEndCombat_DLC_3_Help = false;
			break;
		case "DLC4NOT":
			MainViewModel.Instance.Show_FrontEndCombat_DLC_4_Help = false;
			break;
		}
	}

	public void PlayShieldRollover(float volume = 1f)
	{
		if (lastRolloverSoundTime < DateTime.UtcNow)
		{
			SFXManager.instance.playUISound(146, volume);
			lastRolloverSoundTime = DateTime.UtcNow.AddMilliseconds(200.0);
		}
	}

	public void ButtonCampaignClicked(int value)
	{
		if (value < 0)
		{
			if (value == -1)
			{
				ButtonClicked("Combat");
				return;
			}
			MainViewModel.Instance.Show_Story_DEBUG = false;
			GameData.Instance.game_type = 0;
			GameData.Instance.mission_level = CurrentSelectedMission;
			MainViewModel.Instance.InitNewScene(Enums.SceneIDS.Story);
			MainViewModel.Instance.StartStory(CurrentSelectedMission, 1);
			MainViewModel.Instance.StoryAdvance();
		}
		else
		{
			if (value <= 2)
			{
				MainViewModel.Instance.setupMap(value - 1, "");
			}
			else
			{
				MainViewModel.Instance.setupMap(value - 1, "");
				StoryMap.Instance.lookUpFlagStartPos(value);
			}
			CurrentSelectedMission = value;
			UpdateCampaignSelectedButton(value);
		}
	}

	private void UpdateCampaignSelectedButton(int buttonID)
	{
		for (int i = 0; i < MainViewModel.Instance.CampaignMenuButtonBorders.Count; i++)
		{
			MainViewModel.Instance.CampaignMenuButtonBorders[i] = Visibility.Hidden;
		}
		if (buttonID >= MainViewModel.Instance.CampaignMenuButtonBorders.Count)
		{
			buttonID = MainViewModel.Instance.CampaignMenuButtonBorders.Count - 1;
		}
		MainViewModel.Instance.CampaignMenuButtonBorders[buttonID] = Visibility.Visible;
	}

	public void ButtonEcoCampaignClicked(int value)
	{
		if (value < 0)
		{
			if (value == -1)
			{
				ButtonClicked("Eco");
				return;
			}
			GameData.Instance.game_type = 5;
			MainViewModel.Instance.StartEcoCampaignMission(CurrentSelectedEcoMission);
		}
		else
		{
			CurrentSelectedEcoMission = value;
			UpdateEcoCampaignSelectedButton(value);
		}
	}

	private void UpdateEcoCampaignSelectedButton(int buttonID)
	{
		for (int i = 0; i < MainViewModel.Instance.EcoCampaignMenuButtonBorders.Count; i++)
		{
			MainViewModel.Instance.EcoCampaignMenuButtonBorders[i] = Visibility.Hidden;
		}
		if (buttonID >= MainViewModel.Instance.EcoCampaignMenuButtonBorders.Count)
		{
			buttonID = MainViewModel.Instance.EcoCampaignMenuButtonBorders.Count - 1;
		}
		MainViewModel.Instance.EcoCampaignMenuButtonBorders[buttonID] = Visibility.Visible;
	}

	public void ButtonExtraCampaignClicked(int value)
	{
		if (value < 0)
		{
			if (value == -1)
			{
				ButtonClicked("Jewel");
				return;
			}
			MainViewModel.Instance.Show_Story_DEBUG = false;
			GameData.Instance.game_type = 7;
			GameData.Instance.mission_level = CurrentSelectedExtraMission + 30;
			MainViewModel.Instance.InitNewScene(Enums.SceneIDS.Story);
			MainViewModel.Instance.StartStory(CurrentSelectedExtraMission + 30, 1);
			MainViewModel.Instance.StoryAdvance();
			return;
		}
		CurrentSelectedExtraMission = value;
		if (value >= 10 && value < 20)
		{
			CurrentSelectedExtra1Mission = value;
		}
		if (value >= 20 && value < 30)
		{
			CurrentSelectedExtra2Mission = value;
		}
		if (value >= 30 && value < 40)
		{
			CurrentSelectedExtra3Mission = value;
		}
		if (value >= 40 && value < 50)
		{
			CurrentSelectedExtra4Mission = value;
		}
		UpdateExtraCampaignSelectedButton(value);
	}

	private void UpdateExtraCampaignSelectedButton(int buttonID)
	{
		buttonID %= 10;
		for (int i = 0; i < MainViewModel.Instance.ExtraCampaignMenuButtonBorders.Count; i++)
		{
			MainViewModel.Instance.ExtraCampaignMenuButtonBorders[i] = Visibility.Hidden;
		}
		if (buttonID >= MainViewModel.Instance.ExtraCampaignMenuButtonBorders.Count)
		{
			buttonID = MainViewModel.Instance.ExtraCampaignMenuButtonBorders.Count - 1;
		}
		MainViewModel.Instance.ExtraCampaignMenuButtonBorders[buttonID] = Visibility.Visible;
	}

	public void ButtonExtraEcoCampaignClicked(int value)
	{
		if (value < 0)
		{
			if (value == -1)
			{
				ButtonClicked("Eco");
				return;
			}
			MainViewModel.Instance.Show_Story_DEBUG = false;
			GameData.Instance.game_type = 12;
			GameData.Instance.mission_level = CurrentSelectedExtraEcoMission + 80;
			MainViewModel.Instance.InitNewScene(Enums.SceneIDS.Story);
			MainViewModel.Instance.StartStory(CurrentSelectedExtraEcoMission + 80, 1);
			MainViewModel.Instance.StoryAdvance();
		}
		else
		{
			value %= 10;
			CurrentSelectedExtraEcoMission = value;
			UpdateExtraEcoCampaignSelectedButton(value);
		}
	}

	private void UpdateExtraEcoCampaignSelectedButton(int buttonID)
	{
		buttonID %= 10;
		for (int i = 0; i < MainViewModel.Instance.ExtraCampaignMenuButtonBorders.Count; i++)
		{
			MainViewModel.Instance.ExtraCampaignMenuButtonBorders[i] = Visibility.Hidden;
		}
		if (buttonID >= MainViewModel.Instance.ExtraCampaignMenuButtonBorders.Count)
		{
			buttonID = MainViewModel.Instance.ExtraCampaignMenuButtonBorders.Count - 1;
		}
		MainViewModel.Instance.ExtraCampaignMenuButtonBorders[buttonID] = Visibility.Visible;
	}

	public void ButtonTrailCampaignClicked(int value)
	{
		if (value < 0)
		{
			if (value == -1)
			{
				ButtonClicked("Trails");
			}
			else if (CurrentSelectedTrail == 0)
			{
				StartTrailMission(CurrentSelectedTrailMission, CurrentSelectedTrail);
			}
			else
			{
				StartTrailMission(CurrentSelectedTrail2Mission, CurrentSelectedTrail);
			}
		}
		else
		{
			if (CurrentSelectedTrail == 0)
			{
				CurrentSelectedTrailMission = value;
			}
			else
			{
				CurrentSelectedTrail2Mission = value;
			}
			UpdateTrailCampaignSelectedButton(value);
		}
	}

	private void UpdateTrailCampaignSelectedButton(int buttonID)
	{
		for (int i = 0; i < MainViewModel.Instance.TrailCampaignMenuButtonBorders.Count; i++)
		{
			MainViewModel.Instance.TrailCampaignMenuButtonBorders[i] = Visibility.Hidden;
		}
		if (buttonID >= MainViewModel.Instance.TrailCampaignMenuButtonBorders.Count)
		{
			buttonID = MainViewModel.Instance.TrailCampaignMenuButtonBorders.Count - 1;
		}
		MainViewModel.Instance.TrailCampaignMenuButtonBorders[buttonID] = Visibility.Visible;
		if (buttonID >= 1 && buttonID <= 10)
		{
			if (CurrentSelectedTrail == 0)
			{
				MainViewModel.Instance.FlagXPos = trailLocations[(buttonID - 1) * 2] / 2 - 13;
				MainViewModel.Instance.FlagYPos = trailLocations[(buttonID - 1) * 2 + 1] / 2 - 236;
			}
			else
			{
				MainViewModel.Instance.FlagXPos = trail2Locations[(buttonID - 1) * 2] / 2 - 13;
				MainViewModel.Instance.FlagYPos = trail2Locations[(buttonID - 1) * 2 + 1] / 2 - 236;
			}
		}
	}

	public void StartTrailMission(int missionID, int trailID)
	{
		MainViewModel.Instance.PreStartMapMission();
		List<FileHeader> siegeMaps = MapFileManager.Instance.GetSiegeMaps(sortByName: false, sortAscend: false, includeBuiltIn: true, includeUser: false, includeWorkshop: false);
		string text = "";
		bool siegeAttackMode = true;
		if (trailID == 0)
		{
			switch (missionID)
			{
			default:
				text = "dunnottar";
				siegeAttackMode = false;
				break;
			case 2:
				text = "warkworth";
				break;
			case 3:
				text = "pembroke";
				break;
			case 4:
				text = "warwick";
				siegeAttackMode = false;
				break;
			case 5:
				text = "bodiam";
				siegeAttackMode = false;
				break;
			case 6:
				text = "hastings";
				break;
			case 7:
				text = "chateau gaillard";
				break;
			case 8:
				text = "chateau de coucy";
				siegeAttackMode = false;
				break;
			case 9:
				text = "marksburg";
				break;
			case 10:
				text = "heuneburg";
				siegeAttackMode = false;
				break;
			}
		}
		else
		{
			switch (missionID)
			{
			default:
				text = "castillo de coca";
				siegeAttackMode = false;
				break;
			case 2:
				text = "heidelberg_nobletrail";
				break;
			case 3:
				text = "fougeres";
				siegeAttackMode = false;
				break;
			case 4:
				text = "biskupin";
				break;
			case 5:
				text = "malbork";
				break;
			case 6:
				text = "monteriggioni_nobletrail";
				siegeAttackMode = false;
				break;
			case 7:
				text = "koblenz stolzanfels";
				break;
			case 8:
				text = "diósgyőr";
				siegeAttackMode = false;
				break;
			case 9:
				text = "fenis";
				break;
			case 10:
				text = "niedzica";
				siegeAttackMode = false;
				break;
			}
		}
		FileHeader fileHeader = null;
		foreach (FileHeader item in siegeMaps)
		{
			if (item.fileName.ToLowerInvariant() == text)
			{
				fileHeader = item;
				break;
			}
		}
		if (fileHeader != null)
		{
			HUD_IngameMenu.RestartMapInfo restartMapInfo = new HUD_IngameMenu.RestartMapInfo();
			MainViewModel.Instance.HUDIngameMenu.restartMapInfo = restartMapInfo;
			restartMapInfo.missionType = Enums.StartUpUIPanels.Siege;
			restartMapInfo.difficulty = Enums.GameDifficulty.DIFFICULTY_NORMAL;
			restartMapInfo.siegeAttackMode = siegeAttackMode;
			restartMapInfo.siegeAdvanced = false;
			restartMapInfo.selectedHeader = fileHeader;
			restartMapInfo.trailType = trailID + 1;
			restartMapInfo.trailID = missionID;
			FRONT_StandaloneMission.StartMap(restartMapInfo);
		}
	}

	private void UpdateCampaignListButtonVisibility(int unlockedLevel)
	{
		for (int i = 1; i <= 21; i++)
		{
			if (i <= unlockedLevel)
			{
				MainViewModel.Instance.CampaignMenuButtonsVisible[i] = Visibility.Visible;
			}
			else
			{
				MainViewModel.Instance.CampaignMenuButtonsVisible[i] = Visibility.Hidden;
			}
		}
	}

	private void UpdateTutorialHelpVisibility(bool state)
	{
		if (ForceShowTutorialHelp)
		{
			MainViewModel.Instance.ShowTutorialHelpText = Visibility.Visible;
		}
		else if (state)
		{
			MainViewModel.Instance.ShowTutorialHelpText = Visibility.Visible;
		}
		else
		{
			MainViewModel.Instance.ShowTutorialHelpText = Visibility.Hidden;
		}
	}

	public void UpdateFrontMenuPopupScale()
	{
		float num = Screen.width;
		float num2 = Screen.height;
		float num3 = 1f;
		if (num < 1920f || num2 < 1080f)
		{
			int num4 = 1920;
			int num5 = 1080;
			float a = num / (float)num4;
			float b = num2 / (float)num5;
			num3 = 1f / Mathf.Min(a, b);
			if (num3 < 1f)
			{
				num3 = 1f;
			}
		}
		if (Screen.width > 1366 && Screen.height > 768)
		{
			num3 = (1.6f - num3) * ConfigSettings.Settings_UIScale + num3;
		}
		MainViewModel.Instance.FrontEndRequesterWidth = (int)(1036f * num3);
		MainViewModel.Instance.FrontEndRequesterHeight = (int)(636f * num3);
		MainViewModel.Instance.FrontEndOptionsWidth = (int)(836f * num3);
		MainViewModel.Instance.FrontEndOptionsHeight = (int)(636f * num3);
		MainViewModel.Instance.FrontEndHelpWidth = (int)(856f * num3);
		MainViewModel.Instance.FrontEndHelpHeight = (int)(536f * num3);
		MainViewModel.Instance.FrontEndConfirmationWidth = (int)((double)HUD_ConfirmationPopup.ConfirmationWidth * 1.2 * (double)num3);
		MainViewModel.Instance.FrontEndConfirmationHeight = (int)((double)HUD_ConfirmationPopup.ConfirmationHeight * 1.2 * (double)num3);
		UpdateVideoScale();
	}

	public void PlayBackgroundVideo(bool state)
	{
		if (state)
		{
			UpdateVideoScale();
			refFrontendBackVideo.Play();
		}
		else
		{
			refFrontendBackVideo.Stop();
		}
	}

	private void FrontendBackVideo_Opened(object sender, RoutedEventArgs args)
	{
		MainViewModel.Instance.PreLoadBlankVis = false;
		refFrontendBackVideo.Play();
		MainViewModel.Instance.FrontEndMenu.refMainMenu_ShowMainMenu.Begin();
	}

	private void FrontendBackVideo_Ended(object sender, RoutedEventArgs args)
	{
		if (loopBackgroundVideo)
		{
			refFrontendBackVideo.Stop();
			refFrontendBackVideo.Play();
		}
	}

	public static void UpdateVideoScale()
	{
		float num = Screen.width;
		float num2 = Screen.height;
		float num3 = 1.7777778f;
		float num4 = num / num2;
		if (num4 == num3)
		{
			MainViewModel.Instance.FrontEndVideoMargin = "0,0";
		}
		else if (num4 > num3)
		{
			MainViewModel.Instance.FrontEndVideoMargin = "0,-1079";
		}
		else
		{
			MainViewModel.Instance.FrontEndVideoMargin = "-1919,0";
		}
	}

	private void InitializeComponent()
	{
		Noesis.GUI.LoadComponent(this, "Assets/GUI/XAML/FrontendMenus.xaml");
	}

	protected override bool ConnectEvent(object source, string eventName, string handlerName)
	{
		if (eventName == "MouseEnter" && handlerName == "MouseEnterMainButtonHandler")
		{
			if (source is Button)
			{
				((Button)source).MouseEnter += MouseEnterMainButtonHandler;
			}
			return true;
		}
		if (eventName == "MouseLeave" && handlerName == "MouseLeaveMainButtonHandler")
		{
			if (source is Button)
			{
				((Button)source).MouseLeave += MouseLeaveMainButtonHandler;
			}
			return true;
		}
		return false;
	}
}
