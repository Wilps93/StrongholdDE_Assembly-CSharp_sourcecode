using System;
using System.Collections;
using System.IO;
using Noesis;
using Stronghold1DE;
using UnityEngine;

public class EditorDirector : MonoBehaviour
{
	public static EditorDirector instance;

	private static int DEBUG_UNIQUE;

	private Resolution lastRes;

	public GUISkin skin1;

	private const int panel1_Width = 300;

	private int panel1_Height = 580;

	private int panel1_XPos = Screen.width - 300 - 10;

	private int panel1_YPos = 40;

	private const int panel1_Vert_Ofset = 32;

	private const int panel1_UI_Width = 270;

	private const int panel1_UI_Half_Width = 135;

	private const int panel1_UI_XOff = 5;

	private const int panel1_UI_Half_XOff = 140;

	private const int panel1_UI_YOff = 5;

	private Vector2 scrollPosition = Vector2.zero;

	private int YCOUNT;

	private bool isActive = true;

	private bool guiChanged;

	private bool overGUI = true;

	private string lastErrorMessage = "";

	private float mousePosX;

	private float mousePosY;

	private float mouseTileX;

	private float mouseTileY;

	[HideInInspector]
	public bool shiftPressed;

	[HideInInspector]
	public bool ctrlPressed;

	[HideInInspector]
	public bool altPressed;

	[HideInInspector]
	public bool allowLockedMapEditing;

	private int editorPlayerID = 1;

	private int gameLocalPlayerID = 1;

	[HideInInspector]
	public bool mapChanged;

	private float constantEditorPlacementRate = 1f / 60f;

	private Vector2 lastConstantPlacementPos = Vector2.zero;

	private bool constantEditorLimitSameTileRepeat;

	private Vector2 troopSelectMouseStart = Vector2.zero;

	private Vector2 troopSelectMouseCurrent = Vector2.zero;

	private int[] selectedChimpList;

	private int[] underCursorChimpList;

	private int[] onScreenChimpsList;

	private int[] dllSelectedChimpList;

	private bool chimpListChanged;

	private bool scheduleTroopSelectionEnd;

	private bool troopSelectionBoxOn;

	public int lastTroopOverDepth = -1;

	private bool gotNewSelectionInfo;

	private int[] lastSelectedChimpList;

	private int MapEditor_CreateMapSize = 160;

	private Vector3Int constantMousePosTile;

	private double actionLimitingTime;

	private int leftMouseStateForEngine;

	private int mousePosXForEngine = -1;

	private int mousePosYForEngine = -1;

	private bool stateRead = true;

	private bool rightDownForEngine;

	private bool rightUpForEngine;

	private bool upPending;

	private bool overNoesisUI;

	private DateTime mouseStateSetTime = DateTime.MinValue;

	private bool ignoreNextMouseDown;

	private DateTime ignoreNextMouseDownTime = DateTime.MinValue;

	private Texture2D darkBoxTexture;

	private int loadingState = -1;

	private DateTime recentFlattenLandscape = DateTime.MinValue;

	private int editorMouseState;

	private float editorOverLandHeight;

	private int lastConstantX = -1;

	private int lastConstantY = -1;

	private int lastConstantState;

	private DateTime lastConstantTime = DateTime.MinValue;

	private int lockedRulerX = -1;

	private int lockedRulerY = -1;

	private bool allowContinuousNoMouseConstant;

	public double ftime;

	private int currentFrames;

	private int lastFrames;

	private const int mission_text_editor_width = 300;

	private int ScenarioWindowbaseX;

	private const int mission_scenario_editor_width = 800;

	private Vector2 scenarioScrollPosition = Vector2.zero;

	private const int scenerioWindowsScrollWidth = 750;

	public bool IsActive => isActive;

	public int ActivePlayerID
	{
		get
		{
			if (MainViewModel.Instance.IsMapEditorMode)
			{
				return editorPlayerID;
			}
			return gameLocalPlayerID;
		}
	}

	public int CurrentFPS => lastFrames;

	public void SetLocalPlayer(int playerID)
	{
		gameLocalPlayerID = playerID;
	}

	private void Awake()
	{
		instance = this;
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		lastRes = Screen.currentResolution;
		startFPS();
		FatControler.instance.SetNoesisKeyboardState(state: false);
	}

	private void Start()
	{
	}

	private void OnDestroy()
	{
		if (Director.instance != null)
		{
			Director.instance.stopSimThread();
		}
		if (MyAudioManager.Instance != null)
		{
			MyAudioManager.Instance.StopAllGameSounds();
		}
	}

	public void log(string str)
	{
		Debug.Log(DEBUG_UNIQUE + " " + str);
		DEBUG_UNIQUE++;
	}

	private void Update()
	{
		if (FatControler.currentScene != Enums.SceneIDS.ActualMainGame)
		{
			if (Input.GetMouseButtonDown(0))
			{
				FatControler.MouseIsDownStroke = true;
			}
			else if (Input.GetMouseButtonUp(0))
			{
				FatControler.MouseIsUpStroke = true;
			}
			return;
		}
		updateFPS();
		if (lastRes.width != Screen.currentResolution.width || lastRes.height != Screen.currentResolution.height)
		{
			lastRes = Screen.currentResolution;
		}
		MainControls.instance.getMouseTileCentrePosition(ref mousePosX, ref mousePosY);
		MainControls.instance.getMouseMapTilePosition(ref mouseTileX, ref mouseTileY);
		int num = (int)Input.mousePosition.x;
		int num2 = Screen.height - (int)Input.mousePosition.y;
		mousePosXForEngine = num;
		mousePosYForEngine = num2;
		bool flag = false;
		bool flag2 = false;
		if (FatControler.instance.overNoesisGUI())
		{
			flag = true;
			flag2 = true;
		}
		if (GameData.scenario.inGameoverSituation)
		{
			flag = true;
		}
		overNoesisUI = flag;
		overGUI = false;
		bool flag3 = false;
		int[] chimpsUnderCursor = null;
		if (Input.GetMouseButtonDown(0))
		{
			if (ignoreNextMouseDown && ignoreNextMouseDownTime > DateTime.UtcNow)
			{
				ignoreNextMouseDown = false;
			}
			else
			{
				FatControler.MouseIsDownStroke = true;
				if (!overNoesisUI)
				{
					updateLeftMouseStateForEngine(1);
				}
				if (MainControls.instance.CurrentAction == 9)
				{
					MainControls.instance.CurrentAction = 0;
				}
				if (MainControls.instance.CurrentAction == 0)
				{
					if (!overNoesisUI && (!MainViewModel.Instance.IsMapEditorMode || MainViewModel.Instance.MEMode != 0) && !HUD_Help.browserThumbHeld)
					{
						troopSelectMouseStart = Input.mousePosition;
					}
					else
					{
						troopSelectMouseStart = new Vector2(-1f, -1f);
					}
					int[] chimpsUnderCursor2 = null;
					int depthFromUnder = -1;
					onScreenChimpsList = GameMap.instance.grabTroopsOnScreen(Vector2.zero, new Vector2(Screen.width, Screen.height), ref chimpsUnderCursor2, Vector2.zero, ref depthFromUnder);
				}
				else
				{
					performEditorClick();
				}
				initConstantAction();
			}
		}
		else if (Input.GetMouseButton(0))
		{
			if (!overNoesisUI || MainControls.instance.CurrentAction == 8)
			{
				updateLeftMouseStateForEngine(2);
			}
			double timeAsDouble = Time.timeAsDouble;
			if (timeAsDouble - actionLimitingTime >= (double)constantEditorPlacementRate)
			{
				actionLimitingTime = timeAsDouble;
				GameMap.instance.getFixedHeightMouseOver(Input.mousePosition, out constantMousePosTile, editorOverLandHeight);
			}
			if (MainControls.instance.CurrentAction == 0 && Director.instance.SimRunning)
			{
				if (troopSelectMouseStart.x >= 0f && (Math.Abs(troopSelectMouseStart.x - Input.mousePosition.x) > 3f || Math.Abs(troopSelectMouseStart.y - Input.mousePosition.y) > 3f))
				{
					MainControls.instance.CurrentAction = 8;
					TroopSelector.instance.startSelection(troopSelectMouseStart, Input.mousePosition);
					troopSelectMouseCurrent = Input.mousePosition;
				}
			}
			else if (MainControls.instance.CurrentAction == 8)
			{
				TroopSelector.instance.updateSelection(Input.mousePosition);
				troopSelectMouseCurrent = Input.mousePosition;
				int depthFromUnder2 = lastTroopOverDepth;
				selectedChimpList = GameMap.instance.grabTroopsOnScreen(troopSelectMouseStart, troopSelectMouseCurrent, ref chimpsUnderCursor, Input.mousePosition, ref depthFromUnder2);
				lastTroopOverDepth = depthFromUnder2;
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			FatControler.MouseIsUpStroke = true;
			if (MainControls.instance.CurrentAction == 8)
			{
				TroopSelector.instance.updateSelection(Input.mousePosition);
				troopSelectMouseCurrent = Input.mousePosition;
				int depthFromUnder3 = lastTroopOverDepth;
				selectedChimpList = GameMap.instance.grabTroopsOnScreen(troopSelectMouseStart, troopSelectMouseCurrent, ref chimpsUnderCursor, Input.mousePosition, ref depthFromUnder3);
				lastTroopOverDepth = depthFromUnder3;
				if (TroopSelector.instance.selection_on)
				{
					troopSelectionBoxOn = true;
				}
				TroopSelector.instance.endSelection();
				MainControls.instance.CurrentAction = 9;
				updateLeftMouseStateForEngine(3);
			}
			else if (!overNoesisUI)
			{
				updateLeftMouseStateForEngine(3);
			}
			else
			{
				updateLeftMouseStateForEngine(0);
			}
		}
		else
		{
			flag3 = true;
		}
		if (Input.GetMouseButtonDown(2) && (MainControls.instance.CurrentAction == 5 || MainControls.instance.CurrentAction == 3))
		{
			EngineInterface.GameAction(Enums.GameActionCommand.RotateBuilding, 0, 0);
		}
		gotNewSelectionInfo = true;
		allowContinuousNoMouseConstant = flag3;
		if (chimpsUnderCursor == null)
		{
			int depthFromUnder4 = lastTroopOverDepth;
			GameMap.instance.grabTroopsOnScreen(Vector2.zero, Vector2.zero, ref chimpsUnderCursor, Input.mousePosition, ref depthFromUnder4);
			lastTroopOverDepth = depthFromUnder4;
		}
		underCursorChimpList = chimpsUnderCursor;
		bool flag4 = false;
		if (GameData.Instance.game_type == 4 && (MainViewModel.Instance.HUDmain.currentTutorialArrow == 3 || MainViewModel.Instance.HUDmain.currentTutorialArrow == 4))
		{
			flag4 = true;
		}
		if (!overNoesisUI && !flag4)
		{
			if (Input.GetMouseButtonUp(1))
			{
				rightUpForEngine = true;
			}
			else if (Input.GetMouseButtonDown(1))
			{
				bool flag5 = true;
				if (GameData.Instance.lastGameState != null && GameData.Instance.lastGameState.lordOnlySelected > 0 && !ConfigSettings.Settings_SH1RTSControls)
				{
					flag5 = false;
				}
				if (flag5)
				{
					MainViewModel.Instance.HUDRightClick.Open();
				}
				MainControls.instance.StopAllPlacement();
				rightDownForEngine = true;
			}
		}
		else if (flag2 && FatControler.instance.overBuildingMenu && Input.GetMouseButtonDown(1))
		{
			if (GameData.Instance.lastGameState != null && GameData.Instance.lastGameState.app_mode == 14)
			{
				switch (GameData.Instance.lastGameState.app_sub_mode)
				{
				case 33:
				case 34:
					MainViewModel.Instance.HUDmain.NewBuildScreenSubTownRtn(null, null);
					break;
				case 17:
				case 18:
				case 19:
					MainViewModel.Instance.HUDmain.NewBuildScreenGates(null, null);
					break;
				case 11:
				case 12:
				case 14:
					MainViewModel.Instance.HUDmain.NewBuildScreenCastle();
					break;
				default:
					if (MainViewModel.Instance.IsMapEditorMode && MainViewModel.Instance.SubMode == 13)
					{
						MainViewModel.Instance.HUDmain.NewBuildScreenCastle();
					}
					break;
				}
			}
			else if (GameData.Instance.lastGameState != null && GameData.Instance.lastGameState.app_mode == 16)
			{
				int app_sub_mode = GameData.Instance.lastGameState.app_sub_mode;
				if ((uint)(app_sub_mode - 53) <= 4u)
				{
					MainViewModel.Instance.ButtonNewTradeType("0");
				}
			}
		}
		if (scheduleTroopSelectionEnd)
		{
			TroopSelector.instance.endSelection();
			scheduleTroopSelectionEnd = false;
		}
		if (chimpListChanged)
		{
			dllSelectedChimpsChanged();
			chimpListChanged = false;
		}
		shiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
		ctrlPressed = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
		altPressed = Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) || Input.GetKey(KeyCode.AltGr);
	}

	public void GamePaused()
	{
		TroopSelector.instance.endSelection();
		scheduleTroopSelectionEnd = false;
		selectedChimpList = null;
		MainControls.instance.CurrentAction = 0;
	}

	public void IgnoreNextMouseDown()
	{
		ignoreNextMouseDown = true;
		ignoreNextMouseDownTime = DateTime.UtcNow.AddSeconds(0.5);
	}

	public void clearMouseStateForEngine()
	{
		leftMouseStateForEngine = 0;
		stateRead = true;
		upPending = false;
	}

	public bool overUI()
	{
		return overNoesisUI;
	}

	private int getMouseStateForEngine(ref bool rightDown, ref bool rightUp)
	{
		rightDown = rightDownForEngine;
		rightDownForEngine = false;
		rightUp = rightUpForEngine;
		rightUpForEngine = false;
		int result = leftMouseStateForEngine;
		stateRead = true;
		if (upPending)
		{
			upPending = false;
			leftMouseStateForEngine = 3;
		}
		else if (leftMouseStateForEngine == 1)
		{
			leftMouseStateForEngine = 2;
		}
		else if (leftMouseStateForEngine == 3)
		{
			leftMouseStateForEngine = 0;
		}
		if (overUI() && MainControls.instance.CurrentAction != 8 && MainControls.instance.CurrentAction != 9)
		{
			return 0;
		}
		return result;
	}

	public void updateLeftMouseStateForEngine(int state)
	{
		if ((state == 2 || state == 3) && leftMouseStateForEngine == 0)
		{
			return;
		}
		if (!stateRead && leftMouseStateForEngine > 0 && (DateTime.UtcNow - mouseStateSetTime).TotalMilliseconds < 500.0)
		{
			if (state == 3)
			{
				upPending = true;
			}
		}
		else
		{
			leftMouseStateForEngine = state;
			stateRead = false;
			mouseStateSetTime = DateTime.UtcNow;
		}
	}

	public Vector2Int GetMapMousePos()
	{
		GameMapTile mapTile = GameMap.instance.getMapTile((int)mouseTileX, (int)mouseTileY);
		if (mapTile != null)
		{
			return new Vector2Int(mapTile.gameMapX, mapTile.gameMapY);
		}
		return new Vector2Int(-1, -1);
	}

	private Texture2D MakeTex(int width, int height, UnityEngine.Color col)
	{
		UnityEngine.Color[] array = new UnityEngine.Color[width * height];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = col;
		}
		Texture2D texture2D = new Texture2D(width, height);
		texture2D.SetPixels(array);
		texture2D.Apply();
		return texture2D;
	}

	public void GUIDarkBox(UnityEngine.Rect rect, string text = "")
	{
		if (darkBoxTexture == null)
		{
			darkBoxTexture = MakeTex(2, 2, new UnityEngine.Color(0f, 0f, 0f, 0.65f));
		}
		Texture2D background = UnityEngine.GUI.skin.box.normal.background;
		UnityEngine.GUI.skin.box.normal.background = darkBoxTexture;
		UnityEngine.GUI.Box(rect, text);
		UnityEngine.GUI.skin.box.normal.background = background;
	}

	public bool isOverGUI()
	{
		return overGUI;
	}

	public void LoadCampaignMap(int loadMapFile, int difficulty, bool mission6PreStart = false)
	{
		EngineInterface.sendPath(Application.streamingAssetsPath, ConfigSettings.GetMpAutoSavePath(), ConfigSettings.GetSavesPath());
		EngineInterface.LoadMapReturnData retData = EngineInterface.loadCampaignMap(loadMapFile, difficulty, mission6PreStart);
		if (retData.errorCode == 1)
		{
			EngineInterface.SetUTF8MapName("mission" + loadMapFile);
			scrollPosition = Vector2.zero;
			postLoading(retData);
			AchievementsCommon.Instance.ResetOnMissionStart();
		}
	}

	public void LoadEcoCampaignMap(int loadMapFile, int difficulty = 1)
	{
		EngineInterface.sendPath(Application.streamingAssetsPath, ConfigSettings.GetMpAutoSavePath(), ConfigSettings.GetSavesPath());
		EngineInterface.LoadMapReturnData retData = EngineInterface.loadEcoCampaignMap(loadMapFile, difficulty);
		if (retData.errorCode == 1)
		{
			EngineInterface.SetUTF8MapName("mission" + loadMapFile);
			scrollPosition = Vector2.zero;
			postLoading(retData);
			AchievementsCommon.Instance.ResetOnMissionStart();
		}
	}

	public int LoadExtraCampaignMap(int dlc, int mission, int difficulty = 1)
	{
		int num = dlc * 10 + 30 + mission;
		EngineInterface.sendPath(Application.streamingAssetsPath, ConfigSettings.GetMpAutoSavePath(), ConfigSettings.GetSavesPath());
		EngineInterface.LoadMapReturnData retData = EngineInterface.loadExtraCampaignMap(num, difficulty);
		if (retData.errorCode == 1)
		{
			EngineInterface.SetUTF8MapName("mission" + num);
			scrollPosition = Vector2.zero;
			postLoading(retData);
			AchievementsCommon.Instance.ResetOnMissionStart();
		}
		return num;
	}

	public int LoadExtraEcoCampaignMap(int mission, int difficulty = 1)
	{
		int num = 80 + mission;
		EngineInterface.sendPath(Application.streamingAssetsPath, ConfigSettings.GetMpAutoSavePath(), ConfigSettings.GetSavesPath());
		EngineInterface.LoadMapReturnData retData = EngineInterface.loadExtraCampaignMap(num, difficulty);
		if (retData.errorCode == 1)
		{
			EngineInterface.SetUTF8MapName("mission" + num);
			scrollPosition = Vector2.zero;
			postLoading(retData);
			AchievementsCommon.Instance.ResetOnMissionStart();
		}
		return num;
	}

	public void createNewMap(int size, Enums.GameModes mode, bool siege_that, bool multiPlayerMap = false)
	{
		EngineInterface.sendPath(Application.streamingAssetsPath, ConfigSettings.GetMpAutoSavePath(), ConfigSettings.GetSavesPath());
		EngineInterface.LoadMapReturnData initData = EngineInterface.newMapEditor(size, (int)mode, siege_that, multiPlayerMap);
		if (!siege_that || initData.errorCode == 1)
		{
			MainViewModel.Instance.HUDmain.sheildButtons[0].IsChecked = true;
			EngineInterface.SetUTF8MapName("");
			GameData.Instance.currentFileName = "";
			GameData.Instance.lastGameState = null;
			GameData.Instance.InitGameInfo(initData);
			GameData.Instance.NewMapForEditor();
			GameMap.instance.newMapLoaded(initData.mapSize);
			SFXManager.instance.resetFreebuildMessages();
			MainViewModel.Instance.IngameUI.setRotationImage(Enums.Dircs.North);
			MainViewModel.Instance.DefaultMapEditorUIGameAction();
			OnScreenText.Instance.initOST();
			GameData.Instance.SetScenarioOverview(EngineInterface.GetScenarioOverview());
			FatControler.instance.InitScenarioEditorValues();
			clearFakeUI();
			Director.instance.startSimThread();
			SetEditorPlayerID(1);
			SetLocalPlayer(1);
			SpriteMapping.BuildPlayerColourMapping(1);
			PerfectPixelWithZoom.instance.ResetZoom();
			manageScenarioEditorButtons();
			InitEditorMode();
			if (siege_that)
			{
				MainViewModel.Instance.HUDmain.SetEditorModeButtonVisibilityForSiegeThatMode(visible: false);
			}
			else
			{
				MainViewModel.Instance.HUDmain.SetEditorModeButtonVisibilityForSiegeThatMode(visible: true);
			}
			mapChanged = false;
		}
	}

	public void changeMapSize(int newMapSize)
	{
		if (Director.instance.SafeToSave(wait: true))
		{
			if (EngineInterface.EditorChangeMap_MapSize(newMapSize) >= 0)
			{
				GameMap.instance.wipeRadar();
				Director.instance.stopSimThread();
				GameMap.instance.changeMapSize(newMapSize);
				clearFakeUI();
				Director.instance.startSimThread();
			}
			Director.instance.SetPausedState(state: false);
		}
	}

	public void stopGameSim(bool leavingScene = false)
	{
		if (Director.instance.SimRunning)
		{
			if (Director.instance.MultiplayerGame)
			{
				StartCoroutine(ExitMP());
			}
			GameData.Instance.NewMapForEditor();
			clearFakeUI();
			MainViewModel.Instance.Show_HUD_MPInviteWarning = false;
			GameMap.instance.wipeRadar();
			MainViewModel.Instance.TestRadarMap();
			SFXManager.instance.stopBink();
			MyAudioManager.Instance.StopAllGameSounds();
			Director.instance.stopSimThread();
			manageScenarioEditorButtons();
			SFXManager.instance.resetFreebuildMessages();
			if (HUD_MPInviteWarning.PendingMPInvite && !leavingScene)
			{
				HUD_MPInviteWarning.PendingMPInvite = false;
				Platform_Multiplayer.Instance.ResumeInvite();
			}
			MainViewModel.Instance.HUDScenario.StartExitAnim();
		}
	}

	private IEnumerator ExitMP()
	{
		Platform_Multiplayer.Instance.LeaveGame();
		yield return new WaitForSeconds(1f);
		Platform_Multiplayer.Instance.exitMP();
	}

	public void MPGameKilled()
	{
		StartCoroutine(KillMP());
	}

	private IEnumerator KillMP()
	{
		yield return new WaitForSeconds(1f);
		instance.stopGameSim();
		yield return new WaitForSeconds(1f);
		MainViewModel.Instance.InitNewScene(Enums.SceneIDS.FrontEnd);
	}

	public void clearFakeUI()
	{
		OnScreenText.Instance.initOST();
	}

	public void loadSaveGame(string filename, string mapName, FileHeader header)
	{
		MainViewModel.Instance.HUDIngameMenu.restartMapInfo = null;
		EngineInterface.sendPath(Application.streamingAssetsPath, ConfigSettings.GetMpAutoSavePath(), ConfigSettings.GetSavesPath());
		EngineInterface.LoadMapReturnData retData = EngineInterface.LoadSaveFile(filename);
		if (retData.errorCode == 1)
		{
			GameData.Instance.getCachedTrailBriefingFromSave(header);
			AchievementsCommon.Instance.UpdateAfterLoadingASave(header.achFood, header.achWood, header.achWeapons);
			EngineInterface.SetUTF8MapName(mapName);
			postLoading(retData);
		}
	}

	public bool loadMapIntoEditor(string filename, string mapName)
	{
		EngineInterface.sendPath(Application.streamingAssetsPath, ConfigSettings.GetMpAutoSavePath(), ConfigSettings.GetSavesPath());
		EngineInterface.LoadMapReturnData initData = EngineInterface.LoadMapFile(filename, editorMode: true);
		if (initData.errorCode == 1)
		{
			MainViewModel.Instance.HUDmain.sheildButtons[0].IsChecked = true;
			EngineInterface.SetUTF8MapName(mapName);
			GameData.Instance.SetScenarioOverview(EngineInterface.GetScenarioOverview());
			FatControler.instance.InitScenarioEditorValues();
			GameData.Instance.lastGameState = null;
			GameData.Instance.InitGameInfo(initData);
			GameData.Instance.currentFileName = System.IO.Path.GetFileNameWithoutExtension(filename);
			GameMap.instance.newMapLoaded(initData.mapSize);
			MainViewModel.Instance.DefaultMapEditorUIGameAction();
			OnScreenText.Instance.initOST();
			clearFakeUI();
			Director.instance.startSimThread();
			SetEditorPlayerID(1);
			SetLocalPlayer(1);
			SpriteMapping.BuildPlayerColourMapping(1);
			PerfectPixelWithZoom.instance.ResetZoom();
			GameMap.instance.setMapRotation((Enums.Dircs)initData.mapRotation, initData.mapRotationCentreX, initData.mapRotationCentreY, force: true);
			MainViewModel.Instance.IngameUI.setRotationImage((Enums.Dircs)initData.mapRotation);
			InitEditorMode();
			if (!GameData.Instance.showAlternateMissionTextForBriefing)
			{
				EngineInterface.SetUTF8MissionText(GameData.Instance.utf8MissionText);
			}
			manageScenarioEditorButtons();
			if (GameData.Instance.game_type == 6)
			{
				MainViewModel.Instance.HUDmain.SetEditorModeButtonVisibilityForSiegeThatMode(visible: false);
			}
			else
			{
				MainViewModel.Instance.HUDmain.SetEditorModeButtonVisibilityForSiegeThatMode(visible: true);
			}
			mapChanged = false;
			lastErrorMessage = "";
			return true;
		}
		return false;
	}

	public void postLoading(EngineInterface.LoadMapReturnData retData, bool startGameThread = true)
	{
		MainViewModel.Instance.HUDMPChatMessages.ClearMPChat();
		GameData.Instance.lastGameState = null;
		GameData.Instance.InitGameInfo(retData);
		GameMap.instance.newMapLoaded(retData.mapSize);
		MainViewModel.Instance.DefaultGameUIGameAction(force: true);
		MainViewModel.Instance.HUDRoot.RefRadarMapGrid.Visibility = Visibility.Visible;
		MainViewModel.Instance.HUDRoot.RefReportsControlGrid.Visibility = Visibility.Visible;
		MainViewModel.Instance.BriefingFromStory = false;
		MainControls.instance.forceUIState(state: true);
		MainViewModel.Instance.HUDmain.RefreshBuildScreen();
		OnScreenText.Instance.initOST();
		SFXManager.instance.resetFreebuildMessages();
		clearFakeUI();
		if (startGameThread)
		{
			Director.instance.startSimThread();
		}
		SpriteMapping.BuildPlayerColourMapping(ConfigSettings.Settings_PlayerColour + 1);
		PerfectPixelWithZoom.instance.ResetZoom();
		GameMap.instance.setMapRotation((Enums.Dircs)retData.mapRotation, retData.mapRotationCentreX, retData.mapRotationCentreY, force: true);
		MainViewModel.Instance.IngameUI.setRotationImage((Enums.Dircs)retData.mapRotation);
		if (GameData.Instance.game_type == 2 && GameData.Instance.mapType == Enums.GameModes.BUILD)
		{
			SFXManager.instance.startFreebuildMessages();
		}
		mapChanged = false;
	}

	public void SaveSaveGameOrMap(string path, string mapName, bool lockMap = false, bool tempLockOnly = false, bool mapSave = false)
	{
		if (mapName != "")
		{
			EngineInterface.SetUTF8MapName(mapName);
		}
		Vector2Int screenXYForSaveCentring = CameraControls2D.instance.getScreenXYForSaveCentring((int)((float)(Screen.width / 64) / PerfectPixelWithZoom.instance.getZoom()), (int)((float)(Screen.height / 16) / PerfectPixelWithZoom.instance.getZoom()));
		int centreX = -1;
		int centreY = -1;
		GameMap.instance.getRotationCentre(ref centreX, ref centreY);
		EngineInterface.SaveSaveGame(path, screenXYForSaveCentring.x, screenXYForSaveCentring.y, centreX, centreY, lockMap, tempLockOnly, mapSave);
		mapChanged = false;
	}

	public void manageScenarioEditorButtons()
	{
		if (Director.instance.SimRunning)
		{
			if (MainViewModel.Instance.IsMapEditorMode)
			{
				if (GameData.Instance.mapType == Enums.GameModes.SIEGE && GameData.Instance.siegeThat)
				{
					MainViewModel.Instance.Show_HUD_Scenario_Button = false;
					MainViewModel.Instance.Show_HUD_Scenario = false;
					return;
				}
				MainViewModel.Instance.Show_HUD_Scenario_Button = true;
				if (!GameData.Instance.multiplayerMap)
				{
					MainViewModel.Instance.Show_HUD_Scenario = true;
				}
				else
				{
					MainViewModel.Instance.Show_HUD_Scenario = false;
				}
			}
			else
			{
				MainViewModel.Instance.Show_HUD_Scenario = false;
				MainViewModel.Instance.Show_HUD_Scenario_Button = false;
			}
		}
		else
		{
			MainViewModel.Instance.Show_HUD_Scenario = false;
			MainViewModel.Instance.Show_HUD_Scenario_Button = false;
		}
	}

	public void setActive(bool mode)
	{
		isActive = mode;
		if (!isActive)
		{
			MainControls.instance.CurrentAction = 0;
		}
	}

	public void preDLLCallActions(ref int mouseOverX, ref int mouseOverY)
	{
		bool rightDown = false;
		bool rightUp = false;
		int num = getMouseStateForEngine(ref rightDown, ref rightUp);
		int x = (int)mouseTileX;
		int y = (int)mouseTileY;
		GameMapTile gameMapTile = GameMap.instance.getMapTile(x, y);
		if (gameMapTile != null && (!overNoesisUI || MainControls.instance.CurrentAction == 9))
		{
			mouseOverX = gameMapTile.gameMapX;
			mouseOverY = gameMapTile.gameMapY;
		}
		else
		{
			gameMapTile = null;
			mouseOverX = -1;
			mouseOverY = -1;
		}
		performContinuousNoMouse();
		if (Director.instance.Paused)
		{
			num = 0;
		}
		if (rightDown && TroopSelector.instance.selection_on)
		{
			troopSelectionBoxOn = false;
			scheduleTroopSelectionEnd = true;
			num = 0;
		}
		if (MainControls.instance.CurrentAction == 9 && num == 3)
		{
			MainControls.instance.CurrentAction = 0;
		}
		if (!gotNewSelectionInfo)
		{
			selectedChimpList = lastSelectedChimpList;
		}
		gotNewSelectionInfo = false;
		int[] array = ((selectedChimpList == null) ? new int[0] : selectedChimpList);
		int[] array2 = new int[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i];
		}
		int[] array3 = ((underCursorChimpList == null) ? new int[0] : underCursorChimpList);
		int[] array4 = new int[array3.Length];
		for (int j = 0; j < array3.Length; j++)
		{
			array4[j] = array3[j];
		}
		EngineInterface.TroopSelection(onScreenChimps: (num != 1) ? new int[0] : onScreenChimpsList, mouseState: num, rightDown: rightDown, rightUp: rightUp, selectedChimps: array2, selection_on: TroopSelector.instance.selection_on || troopSelectionBoxOn, selection_established: TroopSelector.instance.selection_established, underCursorChimps: array4, mousePosX: mousePosXForEngine, mousePosY: mousePosYForEngine, overTopHalf: GameMap.instance.overTopHalf);
		if (MainControls.instance.CurrentAction != 9)
		{
			lastSelectedChimpList = array;
			selectedChimpList = null;
		}
		if (MainControls.instance.CurrentAction == 8 || MainControls.instance.CurrentAction == 9)
		{
			return;
		}
		troopSelectionBoxOn = false;
		selectedChimpList = null;
		if (MainControls.instance.CurrentAction == 0)
		{
			MainViewModel.Instance.CrossThreadRollover(0, "");
		}
		else
		{
			if (MainControls.instance.CurrentAction == 5 && (GameMap.instance.pendingRotationtriggered || recentFlattenLandscape > DateTime.UtcNow))
			{
				return;
			}
			if (MainControls.instance.CurrentAction == 5)
			{
				int num2 = GameData.getStructFromMapper(MainControls.instance.CurrentSubAction);
				int mapperToHelp = MainViewModel.Instance.getMapperToHelp(MainControls.instance.CurrentSubAction);
				string structString = "";
				if (mapperToHelp > 0)
				{
					structString = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, mapperToHelp);
				}
				if (num2 == 0)
				{
					if (MainControls.instance.CurrentSubAction == 46)
					{
						num2 = 110;
						structString = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, 15);
					}
					else if (MainControls.instance.CurrentSubAction == 25)
					{
						num2 = 111;
						structString = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, 12);
					}
					else if (MainControls.instance.CurrentSubAction == 26)
					{
						num2 = 112;
						structString = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, 13);
					}
					else if (MainControls.instance.CurrentSubAction == 27)
					{
						num2 = 113;
						structString = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, 14);
					}
				}
				if (!MainViewModel.Instance.Show_HUD_Troops)
				{
					MainViewModel.Instance.CrossThreadRollover(num2, structString);
				}
				else
				{
					MainViewModel.Instance.CrossThreadSiegeEngineRollover(num2);
				}
				if (MainControls.instance.CurrentSubAction == 25 || MainControls.instance.CurrentSubAction == 46 || MainControls.instance.CurrentSubAction == 26 || MainControls.instance.CurrentSubAction == 27 || MainControls.instance.CurrentSubAction == 106 || MainControls.instance.CurrentSubAction == 107 || MainControls.instance.CurrentSubAction == 44 || MainControls.instance.CurrentSubAction == 45 || MainControls.instance.CurrentSubAction == 99)
				{
					if (gameMapTile != null)
					{
						EngineInterface.PlaceMapperItem(MainControls.instance.CurrentSubAction, gameMapTile.gameMapX, gameMapTile.gameMapY, 0, ActivePlayerID, !MainViewModel.Instance.IsMapEditorMode, constructingOnly: true, num);
					}
					return;
				}
			}
			switch (num)
			{
			case 0:
				if (MainControls.instance.CurrentAction == 5 && gameMapTile != null)
				{
					EngineInterface.PlaceMapperItem(MainControls.instance.CurrentSubAction, gameMapTile.gameMapX, gameMapTile.gameMapY, 0, ActivePlayerID, !MainViewModel.Instance.IsMapEditorMode, constructingOnly: true, num);
				}
				if (MainControls.instance.CurrentAction == 7 && gameMapTile != null)
				{
					EngineInterface.PlaceMapperItem(MainControls.instance.CurrentSubAction, gameMapTile.gameMapX, gameMapTile.gameMapY, 0, ActivePlayerID, !MainViewModel.Instance.IsMapEditorMode, constructingOnly: true, num);
				}
				break;
			case 1:
				if (MainControls.instance.CurrentAction == 5 && gameMapTile != null)
				{
					EngineInterface.PlaceMapperItem(MainControls.instance.CurrentSubAction, gameMapTile.gameMapX, gameMapTile.gameMapY, 0, ActivePlayerID, !MainViewModel.Instance.IsMapEditorMode, constructingOnly: false, num);
					clearMouseStateForEngine();
				}
				if (MainControls.instance.CurrentAction == 6 && gameMapTile != null)
				{
					EngineInterface.DeleteBuilding(gameMapTile.gameMapX, gameMapTile.gameMapY, ActivePlayerID, inGameNotEditor: true, 1);
				}
				if (MainControls.instance.CurrentAction == 3 && gameMapTile != null)
				{
					int num4 = 0;
					if (MainControls.instance.BrushSize == 9)
					{
						num4 = 1;
					}
					else if (MainControls.instance.BrushSize == 25)
					{
						num4 = 2;
					}
					else if (MainControls.instance.BrushSize == 49)
					{
						num4 = 3;
					}
					else if (MainControls.instance.BrushSize == 81)
					{
						num4 = 4;
					}
					else if (MainControls.instance.BrushSize == 121)
					{
						num4 = 5;
					}
					else if (MainControls.instance.BrushSize == 169)
					{
						num4 = 6;
					}
					EngineInterface.PlaceMapperItem(MainControls.instance.CurrentSubAction, gameMapTile.gameMapX, gameMapTile.gameMapY, num4 + 1, ActivePlayerID, !MainViewModel.Instance.IsMapEditorMode, constructingOnly: false, num);
				}
				break;
			case 2:
				if (MainControls.instance.CurrentAction == 6 && gameMapTile != null)
				{
					EngineInterface.DeleteBuilding(gameMapTile.gameMapX, gameMapTile.gameMapY, ActivePlayerID, inGameNotEditor: true, 2);
				}
				performEditorConstantAction();
				break;
			case 3:
				if (MainControls.instance.CurrentAction == 7 && gameMapTile != null)
				{
					EngineInterface.PlaceMapperItem(MainControls.instance.CurrentSubAction, gameMapTile.gameMapX, gameMapTile.gameMapY, 0, ActivePlayerID, !MainViewModel.Instance.IsMapEditorMode, constructingOnly: false, num);
				}
				if (MainControls.instance.CurrentAction == 3 && gameMapTile != null)
				{
					int num3 = 0;
					if (MainControls.instance.BrushSize == 9)
					{
						num3 = 1;
					}
					else if (MainControls.instance.BrushSize == 25)
					{
						num3 = 2;
					}
					else if (MainControls.instance.BrushSize == 49)
					{
						num3 = 3;
					}
					else if (MainControls.instance.BrushSize == 81)
					{
						num3 = 4;
					}
					else if (MainControls.instance.BrushSize == 121)
					{
						num3 = 5;
					}
					else if (MainControls.instance.BrushSize == 169)
					{
						num3 = 6;
					}
					EngineInterface.PlaceMapperItem(MainControls.instance.CurrentSubAction, gameMapTile.gameMapX, gameMapTile.gameMapY, num3 + 1, ActivePlayerID, !MainViewModel.Instance.IsMapEditorMode, constructingOnly: false, num);
				}
				break;
			}
		}
	}

	public void FlattenedLandscape()
	{
		recentFlattenLandscape = DateTime.UtcNow.AddMilliseconds(200.0);
	}

	public void EscapeCloseUIPanel()
	{
		if (MainControls.instance.CurrentAction != 8 && MainControls.instance.CurrentAction != 9)
		{
			EngineInterface.GameAction(Enums.GameActionCommand.CloseTroopsPanel, 0, 0);
			rightDownForEngine = true;
		}
	}

	public void CancelPlacement()
	{
		MainControls.instance.CurrentAction = 0;
	}

	public bool performEditorClick()
	{
		if (MainControls.instance.CurrentAction == 0)
		{
			return false;
		}
		if (MainControls.instance.CurrentAction == 3)
		{
			lastConstantX = -1;
			lastConstantState = 0;
			lastConstantTime = DateTime.UtcNow;
			return true;
		}
		_ = mouseTileX;
		_ = mouseTileY;
		return true;
	}

	private void initConstantAction()
	{
		if (MainControls.instance.CurrentAction != 3)
		{
			return;
		}
		int num = (int)mouseTileX;
		int num2 = (int)mouseTileY;
		if (editorMouseState == 0)
		{
			editorOverLandHeight = GameMap.instance.lastMouseLandscapeHeight;
			editorMouseState = 1;
			if (MainViewModel.Instance.MERulerMode == 0)
			{
				lockedRulerX = num;
				lockedRulerY = -1;
			}
			else if (MainViewModel.Instance.MERulerMode == 1)
			{
				lockedRulerX = -1;
				lockedRulerY = num2;
			}
			else
			{
				lockedRulerX = -1;
				lockedRulerY = -1;
			}
		}
	}

	public bool performEditorConstantAction()
	{
		int num = 0;
		if (MainControls.instance.CurrentAction == 0)
		{
			return false;
		}
		if (overUI())
		{
			return false;
		}
		if (MainControls.instance.CurrentAction == 3)
		{
			int num2 = (int)mouseTileX;
			int num3 = (int)mouseTileY;
			if (constantMousePosTile.y > 0 && constantMousePosTile.y > 0)
			{
				num2 = constantMousePosTile.x;
				num3 = constantMousePosTile.y;
				if (lockedRulerX >= 0)
				{
					num2 = lockedRulerX;
				}
				if (lockedRulerY >= 0)
				{
					num3 = lockedRulerY;
				}
			}
			if (num2 == lastConstantX && num3 == lastConstantY)
			{
				bool flag = true;
				if (lastConstantState == 0)
				{
					if ((DateTime.UtcNow - lastConstantTime).TotalSeconds > 1.0)
					{
						flag = false;
						lastConstantState = 1;
					}
				}
				else if (lastConstantState == 1 && (DateTime.UtcNow - lastConstantTime).TotalMilliseconds > 50.0)
				{
					flag = false;
				}
				if (flag && MainControls.instance.CurrentSubAction != 151)
				{
					return true;
				}
			}
			lastConstantTime = DateTime.UtcNow;
			lastConstantX = num2;
			lastConstantY = num3;
			if (MainControls.instance.BrushSize == 9)
			{
				num = 1;
			}
			else if (MainControls.instance.BrushSize == 25)
			{
				num = 2;
			}
			else if (MainControls.instance.BrushSize == 49)
			{
				num = 3;
			}
			else if (MainControls.instance.BrushSize == 81)
			{
				num = 4;
			}
			else if (MainControls.instance.BrushSize == 121)
			{
				num = 5;
			}
			else if (MainControls.instance.BrushSize == 169)
			{
				num = 6;
			}
			int currentSubAction = MainControls.instance.CurrentSubAction;
			bool flag2 = false;
			int currentSubAction2 = MainControls.instance.CurrentSubAction;
			if ((uint)(currentSubAction2 - 350) <= 9u && overNoesisUI)
			{
				flag2 = true;
			}
			if (!flag2)
			{
				GameMapTile mapTile = GameMap.instance.getMapTile(num2, num3);
				if (mapTile != null)
				{
					if (constantEditorLimitSameTileRepeat)
					{
						if (lastConstantPlacementPos.x == (float)num2 && lastConstantPlacementPos.y == (float)num3)
						{
							return true;
						}
						lastConstantPlacementPos = new Vector2(num2, num3);
					}
					EngineInterface.PlaceMapperItem(currentSubAction, mapTile.gameMapX, mapTile.gameMapY, num + 1, ActivePlayerID, !MainViewModel.Instance.IsMapEditorMode, constructingOnly: false, 2);
				}
			}
			return true;
		}
		return true;
	}

	public bool performContinuousNoMouse()
	{
		if (!allowContinuousNoMouseConstant)
		{
			return false;
		}
		if (MainControls.instance.CurrentAction == 0)
		{
			return false;
		}
		if (!MainViewModel.Instance.IsMapEditorMode)
		{
			return false;
		}
		int x = (int)mouseTileX;
		int y = (int)mouseTileY;
		if (MainControls.instance.CurrentAction == 3)
		{
			editorMouseState = 0;
			int num = 0;
			if (MainControls.instance.BrushSize == 9)
			{
				num = 1;
			}
			else if (MainControls.instance.BrushSize == 25)
			{
				num = 2;
			}
			else if (MainControls.instance.BrushSize == 49)
			{
				num = 3;
			}
			else if (MainControls.instance.BrushSize == 81)
			{
				num = 4;
			}
			else if (MainControls.instance.BrushSize == 121)
			{
				num = 5;
			}
			else if (MainControls.instance.BrushSize == 169)
			{
				num = 6;
			}
			GameMapTile mapTile = GameMap.instance.getMapTile(x, y);
			if (mapTile != null)
			{
				EngineInterface.PlaceMapperItem(MainControls.instance.CurrentSubAction, mapTile.gameMapX, mapTile.gameMapY, num + 1, ActivePlayerID, inGameNotEditor: false, constructingOnly: true, 0);
			}
		}
		return true;
	}

	public void InitEditorMode()
	{
		MainControls.instance.BrushSize = 169;
		MainViewModel.Instance.HUDmain.UpdateMEDrawingSize(6);
	}

	public void mapEditorInteraction(Enums.eMappers command)
	{
		if (Director.instance.Paused)
		{
			return;
		}
		constantEditorPlacementRate = 1f / 60f;
		lastConstantPlacementPos = Vector2.zero;
		constantEditorLimitSameTileRepeat = false;
		mapChanged = true;
		switch (command)
		{
		case Enums.eMappers.MAPPER_DELETE_EDITOR:
			if (MainControls.instance.CurrentAction == 3 && MainControls.instance.CurrentSubAction == (int)command)
			{
				MainControls.instance.CurrentAction = 0;
			}
			else
			{
				changeLand(command);
			}
			break;
		case Enums.eMappers.MAPPER_RAISE:
		case Enums.eMappers.MAPPER_LOWER:
		case Enums.eMappers.MAPPER_MIN:
		case Enums.eMappers.MAPPER_MAX:
		case Enums.eMappers.MAPPER_EQUALISE:
		case Enums.eMappers.MAPPER_MOUNTAIN:
		case Enums.eMappers.MAPPER_HILL:
		case Enums.eMappers.MAPPER_PLAIN1:
		case Enums.eMappers.MAPPER_PLAIN2:
			changeLand(command);
			break;
		case Enums.eMappers.MAPPER_DEER:
		case Enums.eMappers.MAPPER_WOLF:
		case Enums.eMappers.MAPPER_RABBIT:
		case Enums.eMappers.MAPPER_BEAR:
		case Enums.eMappers.MAPPER_CROW:
		case Enums.eMappers.MAPPER_SEAGULL:
			constantEditorPlacementRate = 0.1f;
			constantEditorLimitSameTileRepeat = true;
			changeLand(command);
			break;
		case Enums.eMappers.MAPPER_LAND:
		case Enums.eMappers.MAPPER_ROCKY:
		case Enums.eMappers.MAPPER_STONES:
		case Enums.eMappers.MAPPER_BOULDERS:
		case Enums.eMappers.MAPPER_PEBBLES:
		case Enums.eMappers.MAPPER_IRON:
		case Enums.eMappers.MAPPER_DIRT:
		case Enums.eMappers.MAPPER_GRASS:
			changeLand(command);
			break;
		case Enums.eMappers.MAPPER_SEA:
		case Enums.eMappers.MAPPER_BEACH:
		case Enums.eMappers.MAPPER_RIVER:
		case Enums.eMappers.MAPPER_FORD:
		case Enums.eMappers.MAPPER_MARSH:
		case Enums.eMappers.MAPPER_OIL:
		case Enums.eMappers.MAPPER_FOAM:
		case Enums.eMappers.MAPPER_RIPPLE:
			changeLand(command);
			break;
		case Enums.eMappers.MAPPER_CHESTNUT:
		case Enums.eMappers.MAPPER_OAK:
		case Enums.eMappers.MAPPER_PINE:
		case Enums.eMappers.MAPPER_BIRCH:
		case Enums.eMappers.MAPPER_SHRUB1A:
		case Enums.eMappers.MAPPER_SHRUB1B:
		case Enums.eMappers.MAPPER_SHRUB1C:
		case Enums.eMappers.MAPPER_SHRUB1D:
		case Enums.eMappers.MAPPER_SHRUB1E:
		case Enums.eMappers.MAPPER_SHRUB2A:
			constantEditorPlacementRate = 0.05f;
			constantEditorLimitSameTileRepeat = true;
			changeLand(command);
			break;
		case Enums.eMappers.MAPPER_SIGNPOST:
			changeLand(command);
			break;
		case Enums.eMappers.MAPPER_MARKER_POINT1:
		case Enums.eMappers.MAPPER_MARKER_POINT2:
		case Enums.eMappers.MAPPER_MARKER_POINT3:
		case Enums.eMappers.MAPPER_MARKER_POINT4:
		case Enums.eMappers.MAPPER_MARKER_POINT5:
		case Enums.eMappers.MAPPER_MARKER_POINT6:
		case Enums.eMappers.MAPPER_MARKER_POINT7:
		case Enums.eMappers.MAPPER_MARKER_POINT8:
		case Enums.eMappers.MAPPER_MARKER_POINT9:
		case Enums.eMappers.MAPPER_MARKER_POINT10:
		{
			changeLand(command);
			int num = (int)(command - 350);
			for (int i = 0; i < 10; i++)
			{
				if (i == num)
				{
					MainViewModel.Instance.MarkerSelected[i] = true;
				}
				else
				{
					MainViewModel.Instance.MarkerSelected[i] = false;
				}
			}
			MainViewModel.Instance.HUDMarkers.RefMarkerInvisible.Visibility = Visibility.Visible;
			MainViewModel.Instance.HUDMarkers.RefMarkerVisible.Visibility = Visibility.Visible;
			MainViewModel.Instance.HUDMarkers.RefMarkerDisappearing.Visibility = Visibility.Visible;
			MainViewModel.Instance.HUDMarkers.DisableRadios = true;
			switch (GameData.Instance.lastGameState.markers_start_points[num, 3])
			{
			case 0:
			case 1:
				MainViewModel.Instance.HUDMarkers.RefMarkerInvisible.IsChecked = true;
				break;
			case 2:
				MainViewModel.Instance.HUDMarkers.RefMarkerVisible.IsChecked = true;
				break;
			case 3:
				MainViewModel.Instance.HUDMarkers.RefMarkerDisappearing.IsChecked = true;
				break;
			}
			MainViewModel.Instance.HUDMarkers.DisableRadios = false;
			if (GameData.Instance.lastGameState.markers_start_points[num, 2] > 0)
			{
				EngineInterface.GameAction(Enums.GameActionCommand.CentreMarker, num, 0);
			}
			break;
		}
		case Enums.eMappers.MAPPER_BIGROCK1:
		case Enums.eMappers.MAPPER_BIGROCK2:
		case Enums.eMappers.MAPPER_BIGROCK3:
		case Enums.eMappers.MAPPER_BIGROCK4:
		case Enums.eMappers.MAPPER_BIGROCK5:
			changeLand(command);
			if (MainControls.instance.BrushSize == 9)
			{
				MainControls.instance.BrushSize = 25;
				MainViewModel.Instance.HUDmain.UpdateMEDrawingSize(2);
			}
			else if (MainControls.instance.BrushSize == 49)
			{
				MainControls.instance.BrushSize = 81;
				MainViewModel.Instance.HUDmain.UpdateMEDrawingSize(4);
			}
			else if (MainControls.instance.BrushSize == 121)
			{
				MainControls.instance.BrushSize = 169;
				MainViewModel.Instance.HUDmain.UpdateMEDrawingSize(6);
			}
			break;
		default:
			changeLand(Enums.eMappers.MAPPER_NULL);
			break;
		}
	}

	public void placeBuildingInteraction(Enums.eMappers command)
	{
		if (Director.instance.Paused)
		{
			return;
		}
		switch (command)
		{
		case Enums.eMappers.MAPPER_DELETE:
			if (EngineInterface.StartMapperItem(39) >= 0)
			{
				deleteBuilding();
			}
			break;
		case Enums.eMappers.MAPPER_PEOPLE_ARCHERS:
		case Enums.eMappers.MAPPER_PEOPLE_SPEARMEN:
		case Enums.eMappers.MAPPER_PEOPLE_PIKEMEN:
		case Enums.eMappers.MAPPER_PEOPLE_MACEMEN:
		case Enums.eMappers.MAPPER_PEOPLE_XBOWMEN:
		case Enums.eMappers.MAPPER_PEOPLE_SWORDSMEN:
		case Enums.eMappers.MAPPER_PEOPLE_KNIGHTS:
		case Enums.eMappers.MAPPER_PEOPLE_LADDERMEN:
		case Enums.eMappers.MAPPER_PEOPLE_ENGINEERS:
		case Enums.eMappers.MAPPER_PEOPLE_ENGINEERS_POTS:
		case Enums.eMappers.MAPPER_PEOPLE_MONKS:
		case Enums.eMappers.MAPPER_PEOPLE_CATAPULTS:
		case Enums.eMappers.MAPPER_PEOPLE_TREBUCHETS:
		case Enums.eMappers.MAPPER_PEOPLE_BATTERING_RAMS:
		case Enums.eMappers.MAPPER_PEOPLE_SIEGE_TOWERS:
		case Enums.eMappers.MAPPER_PEOPLE_PORTABLE_SHIELDS:
		case Enums.eMappers.MAPPER_PEOPLE_TUNNELERS:
			placeEditorTroop(command);
			break;
		default:
			placeBuilding(command);
			break;
		}
	}

	public void SetEditorPlayerID(int playerID)
	{
		if (!Director.instance.Paused)
		{
			editorPlayerID = playerID;
			if (MainViewModel.Instance.IsMapEditorMode)
			{
				EngineInterface.SetEditorPlayer(playerID);
			}
		}
	}

	public void SetMEBrushSize(int newSize)
	{
		if (MainControls.instance.CurrentAction == 3 && (MainControls.instance.CurrentSubAction == 205 || MainControls.instance.CurrentSubAction == 206 || MainControls.instance.CurrentSubAction == 207 || MainControls.instance.CurrentSubAction == 208 || MainControls.instance.CurrentSubAction == 209))
		{
			switch (newSize)
			{
			case 1:
				newSize = 2;
				MainViewModel.Instance.HUDmain.UpdateMEDrawingSize(newSize);
				break;
			case 3:
				newSize = 4;
				MainViewModel.Instance.HUDmain.UpdateMEDrawingSize(newSize);
				break;
			case 5:
				newSize = 6;
				MainViewModel.Instance.HUDmain.UpdateMEDrawingSize(newSize);
				break;
			}
		}
		switch (newSize)
		{
		case 0:
			MainControls.instance.BrushSize = 1;
			break;
		case 1:
			MainControls.instance.BrushSize = 9;
			break;
		case 2:
			MainControls.instance.BrushSize = 25;
			break;
		case 3:
			MainControls.instance.BrushSize = 49;
			break;
		case 4:
			MainControls.instance.BrushSize = 81;
			break;
		case 5:
			MainControls.instance.BrushSize = 121;
			break;
		case 6:
			MainControls.instance.BrushSize = 169;
			break;
		}
	}

	public int[] getSelectedChimpTypes()
	{
		if (dllSelectedChimpList == null || dllSelectedChimpList.Length == 0)
		{
			return new int[70];
		}
		return GameMap.instance.getChimpTypesFromChimpList(dllSelectedChimpList);
	}

	private void dllSelectedChimpsChanged()
	{
		MainViewModel.Instance.TroopsSelectedGameAction(fromInitialOpening: false);
	}

	public void setSelectedChimpTypes(int[] newTypes)
	{
		if (!Director.instance.Paused && dllSelectedChimpList != null && dllSelectedChimpList.Length != 0 && newTypes != null)
		{
			EngineInterface.TroopSelectionChanged(GameMap.instance.filterChimpTypes(dllSelectedChimpList, newTypes));
		}
	}

	public void triggerTroopsSelection()
	{
		troopSelectMouseStart = (troopSelectMouseCurrent = Input.mousePosition);
		int[] chimpsUnderCursor = null;
		int depthFromUnder = lastTroopOverDepth;
		selectedChimpList = GameMap.instance.grabTroopsOnScreen(troopSelectMouseStart, troopSelectMouseCurrent, ref chimpsUnderCursor, Input.mousePosition, ref depthFromUnder);
		lastTroopOverDepth = depthFromUnder;
		underCursorChimpList = chimpsUnderCursor;
	}

	public void updateDLLSelectedTroops(EngineInterface.PlayState gameState, bool inTroopUI)
	{
		if (gameState.numSelectedChimps > 0 && inTroopUI)
		{
			if (dllSelectedChimpList == null || dllSelectedChimpList.Length != gameState.numSelectedChimps)
			{
				chimpListChanged = true;
				dllSelectedChimpList = new int[gameState.numSelectedChimps];
				for (int i = 0; i < gameState.numSelectedChimps; i++)
				{
					dllSelectedChimpList[i] = gameState.selectedChimps[i];
				}
				return;
			}
			for (int j = 0; j < gameState.numSelectedChimps; j++)
			{
				if (dllSelectedChimpList[j] != gameState.selectedChimps[j])
				{
					chimpListChanged = true;
					dllSelectedChimpList[j] = gameState.selectedChimps[j];
				}
			}
		}
		else if (dllSelectedChimpList != null && dllSelectedChimpList.Length != 0)
		{
			chimpListChanged = false;
			dllSelectedChimpList = null;
		}
	}

	public void removeSelectedChimpTypes(Enums.eChimps type, int mode)
	{
		if (!Director.instance.Paused)
		{
			if (mode == 0)
			{
				EngineInterface.GameAction(Enums.GameActionCommand.Troops_DeSelectExceptType, 0, (int)type);
			}
			else
			{
				EngineInterface.GameAction(Enums.GameActionCommand.Troops_DeSelectType, 0, (int)type);
			}
		}
	}

	public void directSetAppSubMode(int newMode)
	{
		GameData.Instance.app_sub_mode = newMode;
		EngineInterface.SetAppMode(GameData.Instance.app_mode, GameData.Instance.app_sub_mode);
		tutorialAppModes();
	}

	public void directSetAppMode(int new_app_mode, int new_sub_mode)
	{
		EngineInterface.SetAppMode(new_app_mode, new_sub_mode);
		tutorialAppModes();
	}

	public void reportSubModeTabChange(int newMode)
	{
		GameData.Instance.app_sub_mode = newMode;
		EngineInterface.SetAppMode(GameData.Instance.app_mode, GameData.Instance.app_sub_mode);
		tutorialAppModes();
	}

	private void tutorialAppModes()
	{
		if (GameData.Instance.game_type == 4)
		{
			if (GameData.Instance.app_mode == 16 && GameData.Instance.app_sub_mode == 71)
			{
				EngineInterface.TutorialAction(14, 1);
			}
			if (GameData.Instance.app_mode == 16 && GameData.Instance.app_sub_mode == 72)
			{
				EngineInterface.TutorialAction(14, 72);
			}
		}
	}

	public void changeEditorMode()
	{
		MainControls.instance.StopAllPlacement();
		if (MainViewModel.Instance.MEMode == 0)
		{
			directSetAppMode(12, 0);
		}
		else
		{
			directSetAppMode(14, 10);
		}
	}

	public void placeBuilding(Enums.eMappers buildingType)
	{
		int num = EngineInterface.StartMapperItem((int)buildingType);
		if (num >= 0)
		{
			MainControls.instance.CurrentAction = 5;
			if (num > 0)
			{
				MainControls.instance.CurrentSubAction = num;
				mapChanged = true;
			}
		}
	}

	private void placeEditorTroop(Enums.eMappers buildingType)
	{
		if (EngineInterface.StartMapperItem((int)buildingType) >= 0)
		{
			MainControls.instance.CurrentAction = 7;
			MainControls.instance.CurrentSubAction = (int)buildingType;
		}
	}

	private void deleteBuilding()
	{
		MainControls.instance.CurrentAction = 6;
		mapChanged = true;
	}

	private void changeLand(Enums.eMappers mapperAction)
	{
		if (EngineInterface.StartMapperItem((int)mapperAction) >= 0)
		{
			MainControls.instance.CurrentAction = 3;
			MainControls.instance.CurrentSubAction = (int)mapperAction;
			editorMouseState = 0;
			mapChanged = true;
		}
	}

	public void toggleOSTFrameRate()
	{
		if (OnScreenText.Instance.isOSTActive(Enums.eOnScreenText.OST_FRAMERATE))
		{
			OnScreenText.Instance.removeOSTEntry(Enums.eOnScreenText.OST_FRAMERATE);
		}
		else
		{
			OnScreenText.Instance.addOSTEntry(Enums.eOnScreenText.OST_FRAMERATE, 0);
		}
	}

	private void startFPS()
	{
		currentFrames = 0;
		lastFrames = 0;
		StartCoroutine(fpsCounterSub());
	}

	private void updateFPS()
	{
		currentFrames++;
	}

	private IEnumerator fpsCounterSub()
	{
		yield return new WaitForSeconds(1f);
		lastFrames = currentFrames;
		currentFrames = 0;
		StartCoroutine(fpsCounterSub());
	}

	public static int getIntFromString(string str)
	{
		try
		{
			return int.Parse(str, Director.defaultCulture);
		}
		catch (Exception)
		{
		}
		return -1;
	}

	public static ulong getuLongFromString(string str)
	{
		try
		{
			return ulong.Parse(str, Director.defaultCulture);
		}
		catch (Exception)
		{
		}
		return 0uL;
	}

	private void SetButtonColouring(bool state)
	{
		if (state)
		{
			UnityEngine.GUI.skin.button.normal.textColor = UnityEngine.Color.yellow;
			UnityEngine.GUI.skin.button.hover.textColor = UnityEngine.Color.yellow;
		}
		else
		{
			UnityEngine.GUI.skin.button.normal.textColor = UnityEngine.Color.white;
			UnityEngine.GUI.skin.button.hover.textColor = UnityEngine.Color.white;
		}
	}
}
