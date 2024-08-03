using System;
using System.IO;
using Noesis;
using UnityEngine;

namespace Stronghold1DE;

public class HUD_IngameMenu : UserControl
{
	private enum MenuMode
	{
		Normal,
		Multiplayer,
		SiegeThat,
		Editor,
		Tutorial
	}

	public class RestartMapInfo
	{
		public Enums.StartUpUIPanels missionType;

		public FileHeader selectedHeader;

		public bool siegeAttackMode;

		public bool siegeAdvanced;

		public Enums.GameDifficulty difficulty = Enums.GameDifficulty.DIFFICULTY_NORMAL;

		public int[] adjusted_start_troops = new int[11];

		public int trailType;

		public int trailID;

		public bool advancedFreebuild;

		public int freebuild_GoldLevel;

		public int freebuild_FoodLevel;

		public int freebuild_ResourcesLevel;

		public int freebuild_WeaponsLevel;

		public int freebuild_RandomEvents;

		public int freebuild_Invasions;

		public int freebuild_InvasionDifficulty;

		public int freebuild_Peacetime = 4;
	}

	private WGT_Heading RefHeading;

	private Button RefButtonLoad;

	private Button RefButtonWorkshop;

	private Button RefButtonSave;

	private Button RefButtonOptions;

	private Button RefButtonHelp;

	private Button RefButtonRestart;

	private Button RefButtonQuit;

	private Button RefButtonExit;

	private Button RefButtonResume;

	private Noesis.Grid RefLayoutRoot;

	public bool wasPaused;

	private MenuMode menuMode;

	public RestartMapInfo restartMapInfo;

	public HUD_IngameMenu()
	{
		InitializeComponent();
		MainViewModel.Instance.HUDIngameMenu = this;
		RefLayoutRoot = (Noesis.Grid)FindName("LayoutRoot");
		RefHeading = (WGT_Heading)FindName("ScenarioHeader");
		RefButtonLoad = (Button)FindName("ButtonLoad");
		RefButtonWorkshop = (Button)FindName("ButtonWorkshop");
		RefButtonSave = (Button)FindName("ButtonSave");
		RefButtonOptions = (Button)FindName("ButtonOptions");
		RefButtonHelp = (Button)FindName("ButtonHelp");
		RefButtonRestart = (Button)FindName("ButtonRestartMission");
		RefButtonQuit = (Button)FindName("ButtonQuitMission");
		RefButtonExit = (Button)FindName("ButtonExitStronghold");
		RefButtonResume = (Button)FindName("ButtonResume");
	}

	private void InitializeComponent()
	{
		NoesisUnity.LoadComponent(this, "Assets/GUI/XAMLResources/HUD_IngameMenu.xaml");
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

	public void Init()
	{
		RefLayoutRoot.Height = 400f;
		if (MainViewModel.Instance.IsMapEditorMode)
		{
			if (GameData.Instance.mapType == Enums.GameModes.SIEGE && GameData.Instance.siegeThat)
			{
				SetAsSiegeThatMapEditor();
			}
			else
			{
				SetAsMapEditor();
			}
		}
		else if (GameData.Instance.multiplayerMap)
		{
			SetAsMultiplayer();
		}
		else if (GameData.Instance.game_type == 4)
		{
			SetAsTutorial();
		}
		else
		{
			SetAsNormal();
		}
	}

	private void SetAsNormal()
	{
		menuMode = MenuMode.Normal;
		MainViewModel.Instance.IngameMessageLoadButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 2);
		MainViewModel.Instance.IngameMessageSaveButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 3);
		MainViewModel.Instance.IngameMessageRestartButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 44);
		MainViewModel.Instance.IngameMessageQuitButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 7);
		RefButtonLoad.IsEnabled = true;
		RefButtonSave.IsEnabled = Director.instance.SimRunning;
		RefButtonOptions.IsEnabled = true;
		RefButtonHelp.IsEnabled = true;
		if (GameData.Instance.game_type == 2)
		{
			RefButtonRestart.IsEnabled = restartMapInfo != null;
		}
		else
		{
			RefButtonRestart.IsEnabled = true;
		}
		RefButtonQuit.IsEnabled = true;
		RefButtonExit.IsEnabled = true;
		RefButtonResume.IsEnabled = true;
		RefButtonLoad.Visibility = Visibility.Visible;
		RefButtonWorkshop.Visibility = Visibility.Collapsed;
		wasPaused = Director.instance.Paused;
		if (!wasPaused)
		{
			Director.instance.SetPausedState(state: true);
		}
	}

	private void SetAsTutorial()
	{
		menuMode = MenuMode.Tutorial;
		MainViewModel.Instance.IngameMessageLoadButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 2);
		MainViewModel.Instance.IngameMessageSaveButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 3);
		MainViewModel.Instance.IngameMessageRestartButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 44);
		MainViewModel.Instance.IngameMessageQuitButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 7);
		RefButtonLoad.IsEnabled = false;
		RefButtonSave.IsEnabled = false;
		RefButtonOptions.IsEnabled = true;
		RefButtonHelp.IsEnabled = true;
		RefButtonRestart.IsEnabled = false;
		RefButtonQuit.IsEnabled = true;
		RefButtonExit.IsEnabled = true;
		RefButtonResume.IsEnabled = true;
		RefButtonLoad.Visibility = Visibility.Visible;
		RefButtonWorkshop.Visibility = Visibility.Collapsed;
		wasPaused = Director.instance.Paused;
		if (!wasPaused)
		{
			Director.instance.SetPausedState(state: true);
		}
	}

	private void SetAsMapEditor()
	{
		menuMode = MenuMode.Editor;
		MainViewModel.Instance.IngameMessageLoadButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 2);
		MainViewModel.Instance.IngameMessageSaveButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAPEDIT, 3);
		MainViewModel.Instance.IngameMessageRestartButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 44);
		if (!EditorDirector.instance.mapChanged)
		{
			MainViewModel.Instance.IngameMessageQuitButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 84);
		}
		else
		{
			MainViewModel.Instance.IngameMessageQuitButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 43);
		}
		RefButtonLoad.IsEnabled = false;
		RefButtonSave.IsEnabled = Director.instance.SimRunning;
		RefButtonOptions.IsEnabled = true;
		RefButtonHelp.IsEnabled = true;
		RefButtonRestart.IsEnabled = false;
		RefButtonQuit.IsEnabled = true;
		RefButtonExit.IsEnabled = true;
		RefButtonResume.IsEnabled = true;
		RefButtonLoad.Visibility = Visibility.Collapsed;
		RefButtonWorkshop.Visibility = Visibility.Visible;
		wasPaused = Director.instance.Paused;
		if (!wasPaused)
		{
			Director.instance.SetPausedState(state: true);
		}
	}

	private void SetAsSiegeThatMapEditor()
	{
		menuMode = MenuMode.SiegeThat;
		MainViewModel.Instance.IngameMessageLoadButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAPEDIT, 36);
		MainViewModel.Instance.IngameMessageSaveButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAPEDIT, 3);
		MainViewModel.Instance.IngameMessageRestartButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 12);
		if (!EditorDirector.instance.mapChanged)
		{
			MainViewModel.Instance.IngameMessageQuitButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 84);
		}
		else
		{
			MainViewModel.Instance.IngameMessageQuitButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 43);
		}
		if (GameData.Instance.lastGameState != null)
		{
			Button refButtonWorkshop = RefButtonWorkshop;
			Button refButtonLoad = RefButtonLoad;
			bool flag2 = (RefButtonSave.IsEnabled = GameData.Instance.lastGameState.siege_that_saveable > 0);
			bool isEnabled = (refButtonLoad.IsEnabled = flag2);
			refButtonWorkshop.IsEnabled = isEnabled;
		}
		else
		{
			RefButtonSave.IsEnabled = false;
			RefButtonLoad.IsEnabled = false;
			RefButtonWorkshop.IsEnabled = false;
		}
		RefButtonOptions.IsEnabled = true;
		RefButtonHelp.IsEnabled = true;
		RefButtonRestart.IsEnabled = true;
		RefButtonQuit.IsEnabled = true;
		RefButtonExit.IsEnabled = true;
		RefButtonResume.IsEnabled = true;
		RefButtonLoad.Visibility = Visibility.Collapsed;
		RefButtonWorkshop.Visibility = Visibility.Visible;
		wasPaused = Director.instance.Paused;
		if (!wasPaused)
		{
			Director.instance.SetPausedState(state: true);
		}
	}

	private void SetAsMultiplayer()
	{
		menuMode = MenuMode.Multiplayer;
		MainViewModel.Instance.IngameMessageLoadButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 2);
		MainViewModel.Instance.IngameMessageSaveButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 3);
		MainViewModel.Instance.IngameMessageRestartButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 44);
		MainViewModel.Instance.IngameMessageQuitButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 7);
		RefButtonLoad.IsEnabled = false;
		RefButtonSave.IsEnabled = Director.instance.SimRunning && Platform_Multiplayer.Instance.IsHost;
		RefButtonOptions.IsEnabled = true;
		RefButtonHelp.IsEnabled = true;
		RefButtonRestart.IsEnabled = false;
		RefButtonQuit.IsEnabled = true;
		RefButtonExit.IsEnabled = true;
		RefButtonResume.IsEnabled = true;
		RefButtonLoad.Visibility = Visibility.Visible;
		RefButtonWorkshop.Visibility = Visibility.Collapsed;
	}

	public static void SaveMapEditor(bool siegeThat, bool fromIngameMenu)
	{
		Enums.RequesterTypes reqType;
		Action<string, FileHeader> oKAction;
		if (siegeThat)
		{
			reqType = Enums.RequesterTypes.SaveSiegeThatTempMap;
			oKAction = delegate(string filename, FileHeader header)
			{
				MainViewModel.Instance.Show_HUD_IngameMenu = false;
				if (fromIngameMenu)
				{
					MainViewModel.Instance.HUDIngameMenu.Close();
				}
				string text2 = filename + ".tmp";
				string path2 = System.IO.Path.Combine(ConfigSettings.GetUserMapsPath(), text2);
				EditorDirector.instance.SaveSaveGameOrMap(path2, text2, lockMap: false, tempLockOnly: false, mapSave: true);
				if (HUD_LoadSaveRequester.siegeThatCreateLockedMap)
				{
					text2 = filename + ".map";
					path2 = System.IO.Path.Combine(ConfigSettings.GetUserMapsPath(), text2);
					EditorDirector.instance.SaveSaveGameOrMap(path2, text2, lockMap: true, tempLockOnly: false, mapSave: true);
					GameData.Instance.currentFileName = System.IO.Path.GetFileNameWithoutExtension(path2);
				}
			};
		}
		else
		{
			reqType = Enums.RequesterTypes.SaveEditorMap;
			oKAction = delegate(string filename, FileHeader header)
			{
				if (fromIngameMenu)
				{
					MainViewModel.Instance.HUDIngameMenu.Close();
				}
				string text = filename + ".map";
				string path = System.IO.Path.Combine(ConfigSettings.GetUserMapsPath(), text);
				EngineInterface.GameAction(Enums.GameActionCommand.Set_AI_Patrolling, 1, 1);
				EditorDirector.instance.SaveSaveGameOrMap(path, text, lockMap: false, tempLockOnly: false, mapSave: true);
			};
		}
		HUD_LoadSaveRequester.OpenLoadSaveRequester(reqType, oKAction, delegate
		{
			if (fromIngameMenu)
			{
				MainViewModel.Instance.Show_HUD_IngameMenu = true;
			}
		});
	}

	public void ButtonIngameMenuFunction(int function)
	{
		switch (function)
		{
		case 1:
		{
			Enums.RequesterTypes reqType;
			Action<string, FileHeader> oKAction;
			switch (menuMode)
			{
			default:
				reqType = Enums.RequesterTypes.SaveSinglePlayerGame;
				oKAction = delegate(string filename, FileHeader header)
				{
					Close();
					string path2 = filename + ".sav";
					string path3 = System.IO.Path.Combine(ConfigSettings.GetSavesPath(), path2);
					EditorDirector.instance.SaveSaveGameOrMap(path3, "");
				};
				break;
			case MenuMode.Multiplayer:
				reqType = Enums.RequesterTypes.SaveMultiplayerGame;
				oKAction = delegate(string filename, FileHeader header)
				{
					Close();
					EngineInterface.TriggerMPSave(filename + ".msv");
				};
				break;
			case MenuMode.SiegeThat:
				SaveMapEditor(siegeThat: true, fromIngameMenu: true);
				return;
			case MenuMode.Editor:
				SaveMapEditor(siegeThat: false, fromIngameMenu: true);
				return;
			}
			HUD_LoadSaveRequester.OpenLoadSaveRequester(reqType, oKAction, delegate
			{
				MainViewModel.Instance.Show_HUD_IngameMenu = true;
			});
			Hide();
			break;
		}
		case 2:
			switch (menuMode)
			{
			case MenuMode.Normal:
				HUD_LoadSaveRequester.OpenLoadSaveRequester(Enums.RequesterTypes.LoadSinglePlayerGame, delegate(string filename, FileHeader header)
				{
					Close();
					EditorDirector.instance.stopGameSim();
					EditorDirector.instance.loadSaveGame(header.filePath, header.standAlone_filename, header);
					MainViewModel.Instance.InitObjectiveGoodsPanelDelayed();
				}, delegate
				{
					MainViewModel.Instance.Show_HUD_IngameMenu = true;
				});
				break;
			case MenuMode.SiegeThat:
				HUD_LoadSaveRequester.OpenLoadSaveRequester(Enums.RequesterTypes.SaveSiegeThatLockedMap, delegate(string filename, FileHeader header)
				{
					Close();
					string text = filename + ".map";
					string path = System.IO.Path.Combine(ConfigSettings.GetUserMapsPath(), text);
					EditorDirector.instance.SaveSaveGameOrMap(path, text, lockMap: true, tempLockOnly: false, mapSave: true);
					GameData.Instance.currentFileName = System.IO.Path.GetFileNameWithoutExtension(path);
				}, delegate
				{
					MainViewModel.Instance.Show_HUD_IngameMenu = true;
				});
				break;
			}
			Hide();
			break;
		case 99:
			HUD_WorkshopUploader.Open();
			Hide();
			break;
		case 3:
			HUD_Options.OpenOptions(fromIngameMenu: true);
			Hide();
			break;
		case 4:
			if (!ConfigSettings.Settings_UseSteamOverlayForHelp)
			{
				Close();
			}
			HUD_Help.OpenHelp(fromMenu: true, "file://" + Application.dataPath + "/StreamingAssets/Help/help_main.html");
			break;
		case 5:
			Hide();
			HUD_ConfirmationPopup.ShowConfirmation(MainViewModel.Instance.IngameMessageRestartButtonText, delegate
			{
				if (menuMode == MenuMode.SiegeThat)
				{
					EditorDirector.instance.stopGameSim();
					EditorDirector.instance.createNewMap(GameMap.tilemapSize, Enums.GameModes.SIEGE, siege_that: true);
				}
				else if (GameData.Instance.game_type == 0)
				{
					EditorDirector.instance.stopGameSim();
					MainViewModel.Instance.StartCampaignMission(GameData.Instance.mission_level);
				}
				else if (GameData.Instance.game_type == 5)
				{
					EditorDirector.instance.stopGameSim();
					MainViewModel.Instance.StartEcoCampaignMission(GameData.Instance.mission_level - 32);
				}
				else if (GameData.Instance.game_type == 7)
				{
					EditorDirector.instance.stopGameSim();
					MainViewModel.Instance.StartExtraCampaignMission(1, GameData.Instance.mission_level - 40);
				}
				else if (GameData.Instance.game_type == 8)
				{
					EditorDirector.instance.stopGameSim();
					MainViewModel.Instance.StartExtraCampaignMission(2, GameData.Instance.mission_level - 50);
				}
				else if (GameData.Instance.game_type == 9)
				{
					EditorDirector.instance.stopGameSim();
					MainViewModel.Instance.StartExtraCampaignMission(3, GameData.Instance.mission_level - 60);
				}
				else if (GameData.Instance.game_type == 10)
				{
					EditorDirector.instance.stopGameSim();
					MainViewModel.Instance.StartExtraCampaignMission(4, GameData.Instance.mission_level - 70);
				}
				else if (GameData.Instance.game_type == 12)
				{
					EditorDirector.instance.stopGameSim();
					MainViewModel.Instance.StartExtraEcoCampaignMission(GameData.Instance.mission_level - 80);
				}
				else if (GameData.Instance.game_type == 11)
				{
					EditorDirector.instance.stopGameSim();
					MainViewModel.Instance.FrontEndMenu.StartTrailMission(GameData.Instance.mission_text_id, 0);
				}
				else if (GameData.Instance.game_type == 13)
				{
					EditorDirector.instance.stopGameSim();
					MainViewModel.Instance.FrontEndMenu.StartTrailMission(GameData.Instance.mission_text_id, 1);
				}
				else if (restartMapInfo != null)
				{
					EditorDirector.instance.stopGameSim();
					FRONT_StandaloneMission.StartMap(MainViewModel.Instance.HUDIngameMenu.restartMapInfo);
				}
			}, delegate
			{
				MainViewModel.Instance.Show_HUD_IngameMenu = true;
			});
			break;
		case 6:
		{
			Hide();
			string title = ((!MainViewModel.Instance.IsMapEditorMode) ? Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 7) : (EditorDirector.instance.mapChanged ? Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 43) : Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 84)));
			SFXManager.instance.playSpeech(1, "General_Quitgame.wav", 1f);
			HUD_ConfirmationPopup.ShowConfirmation(title, delegate
			{
				MainViewModel.Instance.InitNewScene(Enums.SceneIDS.FrontEnd);
			}, delegate
			{
				MainViewModel.Instance.Show_HUD_IngameMenu = true;
			});
			break;
		}
		case 7:
			Hide();
			SFXManager.instance.playSpeech(1, "General_Message10.wav", 1f);
			HUD_ConfirmationPopup.ShowConfirmation(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 9), delegate
			{
				if (Director.instance.MultiplayerGame)
				{
					Director.instance.ExitAppFromMP();
				}
				else
				{
					FatControler.instance.ExitApp();
				}
			}, delegate
			{
				MainViewModel.Instance.Show_HUD_IngameMenu = true;
			});
			break;
		case 8:
			Close();
			break;
		}
	}

	public void Hide()
	{
		MainViewModel.Instance.Show_HUD_IngameMenu = false;
	}

	public void Close()
	{
		MainViewModel.Instance.Show_HUD_IngameMenu = false;
		if (!wasPaused)
		{
			Director.instance.SetPausedState(state: false);
		}
	}
}
