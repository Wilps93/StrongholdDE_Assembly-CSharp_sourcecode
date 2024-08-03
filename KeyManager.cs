using System;
using System.Collections.Generic;
using System.IO;
using Noesis;
using Stronghold1DE;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
	public enum KeyState
	{
		Off = 0,
		Down = 1,
		Held = 2,
		Up = 3,
		Shift = 65536,
		Ctrl = 131072,
		Alt = 262144,
		MP = 524288,
		Mask = 65535
	}

	public static KeyManager instance;

	private int[] values;

	private int[] keyCodeMap;

	private int[] keys;

	private int leftShiftMap = -1;

	private int rightShiftMap = -1;

	private int leftCtrlMap = -1;

	private int rightCtrlMap = -1;

	private int altMap = -1;

	private int altGrMap = -1;

	private bool hotKeySelectorMode;

	private int hotKeyCurrentKey;

	private int hotKeyCurrentKeyPressed;

	private DateTime lastQuickSaveTime = DateTime.MinValue;

	private bool cursorUpHeld;

	private bool cursorDownHeld;

	private int[,] functionMap = new int[97, 2];

	private Enums.KeyFunctions[] directSentFunctions = new Enums.KeyFunctions[47]
	{
		Enums.KeyFunctions.HomeKeep,
		Enums.KeyFunctions.Market,
		Enums.KeyFunctions.Signpost,
		Enums.KeyFunctions.Barracks,
		Enums.KeyFunctions.Granary,
		Enums.KeyFunctions.Lord,
		Enums.KeyFunctions.CycleLord,
		Enums.KeyFunctions.GroupTroops0,
		Enums.KeyFunctions.GroupTroops1,
		Enums.KeyFunctions.GroupTroops2,
		Enums.KeyFunctions.GroupTroops3,
		Enums.KeyFunctions.GroupTroops4,
		Enums.KeyFunctions.GroupTroops5,
		Enums.KeyFunctions.GroupTroops6,
		Enums.KeyFunctions.GroupTroops7,
		Enums.KeyFunctions.GroupTroops8,
		Enums.KeyFunctions.GroupTroops9,
		Enums.KeyFunctions.SelectClan0,
		Enums.KeyFunctions.SelectClan1,
		Enums.KeyFunctions.SelectClan2,
		Enums.KeyFunctions.SelectClan3,
		Enums.KeyFunctions.SelectClan4,
		Enums.KeyFunctions.SelectClan5,
		Enums.KeyFunctions.SelectClan6,
		Enums.KeyFunctions.SelectClan7,
		Enums.KeyFunctions.SelectClan8,
		Enums.KeyFunctions.SelectClan9,
		Enums.KeyFunctions.SetBookmark0,
		Enums.KeyFunctions.SetBookmark1,
		Enums.KeyFunctions.SetBookmark2,
		Enums.KeyFunctions.SetBookmark3,
		Enums.KeyFunctions.SetBookmark4,
		Enums.KeyFunctions.SetBookmark5,
		Enums.KeyFunctions.SetBookmark6,
		Enums.KeyFunctions.SetBookmark7,
		Enums.KeyFunctions.SetBookmark8,
		Enums.KeyFunctions.SetBookmark9,
		Enums.KeyFunctions.GotoBookmark0,
		Enums.KeyFunctions.GotoBookmark1,
		Enums.KeyFunctions.GotoBookmark2,
		Enums.KeyFunctions.GotoBookmark3,
		Enums.KeyFunctions.GotoBookmark4,
		Enums.KeyFunctions.GotoBookmark5,
		Enums.KeyFunctions.GotoBookmark6,
		Enums.KeyFunctions.GotoBookmark7,
		Enums.KeyFunctions.GotoBookmark8,
		Enums.KeyFunctions.GotoBookmark9
	};

	private DateTime ignoreNextEscape = DateTime.MinValue;

	public float RadarHeldX;

	public float RadarHeldY;

	public bool CursorUpHeld => cursorUpHeld;

	public bool CursorDownHeld => cursorDownHeld;

	public bool HotKeySelectorMode
	{
		get
		{
			return hotKeySelectorMode;
		}
		set
		{
			hotKeySelectorMode = value;
			if (value)
			{
				hotKeyCurrentKeyPressed = 0;
				hotKeyCurrentKey = 0;
			}
		}
	}

	public int HotKeyCurrentKey => hotKeyCurrentKey;

	private void Awake()
	{
		instance = this;
		values = (int[])Enum.GetValues(typeof(KeyCode));
		keys = new int[values.Length];
		int num = 0;
		for (int i = 0; i < values.Length; i++)
		{
			if (values[i] > num)
			{
				num = values[i];
			}
		}
		keyCodeMap = new int[num + 1];
		for (int j = 0; j < values.Length; j++)
		{
			keyCodeMap[j] = -1;
		}
		for (int k = 0; k < values.Length; k++)
		{
			keyCodeMap[values[k]] = k;
			if (values[k] == 304)
			{
				leftShiftMap = k;
			}
			if (values[k] == 303)
			{
				rightShiftMap = k;
			}
			if (values[k] == 306)
			{
				leftCtrlMap = k;
			}
			if (values[k] == 305)
			{
				rightCtrlMap = k;
			}
			if (values[k] == 308)
			{
				altMap = k;
			}
			if (values[k] == 307)
			{
				altGrMap = k;
			}
		}
		SetDefaultFunctionsNew();
	}

	public void SetDefaultFunctionsNew()
	{
		for (int i = 0; i < 97; i++)
		{
			functionMap[i, 0] = -1;
			functionMap[i, 1] = -1;
		}
		functionMap[1, 0] = 97;
		functionMap[1, 1] = 276;
		functionMap[2, 0] = 100;
		functionMap[2, 1] = 275;
		functionMap[3, 0] = 119;
		functionMap[3, 1] = 273;
		functionMap[4, 0] = 115;
		functionMap[4, 1] = 274;
		functionMap[5, 0] = 112;
		functionMap[6, 0] = 104;
		functionMap[7, 0] = 109;
		functionMap[8, 0] = 111;
		functionMap[9, 0] = 98;
		functionMap[10, 0] = 103;
		functionMap[92, 0] = 108;
		functionMap[93, 0] = 65644;
		functionMap[94, 0] = 9;
		functionMap[11, 0] = 113;
		functionMap[12, 0] = 101;
		functionMap[13, 0] = 32;
		functionMap[15, 0] = 122;
		functionMap[14, 0] = 120;
		functionMap[16, 0] = 114;
		functionMap[17, 0] = 116;
		functionMap[18, 0] = 121;
		functionMap[19, 0] = 131120;
		functionMap[20, 0] = 131121;
		functionMap[21, 0] = 131122;
		functionMap[22, 0] = 131123;
		functionMap[23, 0] = 131124;
		functionMap[24, 0] = 131125;
		functionMap[25, 0] = 131126;
		functionMap[26, 0] = 131127;
		functionMap[27, 0] = 131128;
		functionMap[28, 0] = 131129;
		functionMap[29, 0] = 48;
		functionMap[30, 0] = 49;
		functionMap[31, 0] = 50;
		functionMap[32, 0] = 51;
		functionMap[33, 0] = 52;
		functionMap[34, 0] = 53;
		functionMap[35, 0] = 54;
		functionMap[36, 0] = 55;
		functionMap[37, 0] = 56;
		functionMap[38, 0] = 57;
		functionMap[39, 0] = 393264;
		functionMap[40, 0] = 393265;
		functionMap[41, 0] = 393266;
		functionMap[42, 0] = 393267;
		functionMap[43, 0] = 393268;
		functionMap[44, 0] = 393269;
		functionMap[45, 0] = 393270;
		functionMap[46, 0] = 393271;
		functionMap[47, 0] = 393272;
		functionMap[48, 0] = 393273;
		functionMap[49, 0] = 262192;
		functionMap[50, 0] = 262193;
		functionMap[51, 0] = 262194;
		functionMap[52, 0] = 262195;
		functionMap[53, 0] = 262196;
		functionMap[54, 0] = 262197;
		functionMap[55, 0] = 262198;
		functionMap[56, 0] = 262199;
		functionMap[57, 0] = 262200;
		functionMap[58, 0] = 262201;
		functionMap[59, 0] = 65824;
		functionMap[60, 0] = 65819;
		functionMap[61, 0] = 65818;
		functionMap[62, 0] = 270;
		functionMap[63, 0] = 269;
		functionMap[62, 1] = 61;
		functionMap[63, 1] = 45;
		functionMap[64, 0] = 262266;
		functionMap[65, 0] = 102;
		functionMap[67, 0] = 262183;
		functionMap[68, 0] = 282;
		functionMap[69, 0] = 524301;
		functionMap[69, 1] = 524559;
		functionMap[70, 0] = 786548;
		functionMap[71, 0] = 27;
		functionMap[72, 0] = 524570;
		functionMap[73, 0] = 524571;
		functionMap[74, 0] = 524572;
		functionMap[75, 0] = 524573;
		functionMap[76, 0] = 524574;
		functionMap[77, 0] = 524575;
		functionMap[78, 0] = 524576;
		functionMap[79, 0] = 524577;
		functionMap[80, 0] = 524578;
		functionMap[81, 0] = 524579;
		functionMap[82, 0] = 524580;
		functionMap[83, 0] = 524581;
		functionMap[84, 0] = 262248;
		functionMap[85, 0] = 262262;
		functionMap[86, 0] = 262245;
		functionMap[87, 0] = 281;
		functionMap[88, 0] = 280;
		functionMap[89, 0] = 107;
		functionMap[90, 0] = 106;
		functionMap[91, 0] = 278;
		functionMap[96, 0] = 262251;
		functionMap[95, 0] = 262264;
	}

	public void SetDefaultFunctionsSH1()
	{
		for (int i = 0; i < 97; i++)
		{
			functionMap[i, 0] = -1;
			functionMap[i, 1] = -1;
		}
		functionMap[1, 0] = 276;
		functionMap[2, 0] = 275;
		functionMap[3, 0] = 273;
		functionMap[4, 0] = 274;
		functionMap[5, 0] = 112;
		functionMap[6, 0] = 104;
		functionMap[7, 0] = 109;
		functionMap[8, 0] = 115;
		functionMap[9, 0] = 98;
		functionMap[10, 0] = 103;
		functionMap[92, 0] = 108;
		functionMap[93, 0] = 65644;
		functionMap[94, 0] = 9;
		functionMap[11, 0] = 99;
		functionMap[12, 0] = 120;
		functionMap[13, 0] = 32;
		functionMap[15, 0] = 122;
		functionMap[16, 0] = 113;
		functionMap[17, 0] = 119;
		functionMap[18, 0] = 101;
		functionMap[19, 0] = 131120;
		functionMap[20, 0] = 131121;
		functionMap[21, 0] = 131122;
		functionMap[22, 0] = 131123;
		functionMap[23, 0] = 131124;
		functionMap[24, 0] = 131125;
		functionMap[25, 0] = 131126;
		functionMap[26, 0] = 131127;
		functionMap[27, 0] = 131128;
		functionMap[28, 0] = 131129;
		functionMap[29, 0] = 48;
		functionMap[30, 0] = 49;
		functionMap[31, 0] = 50;
		functionMap[32, 0] = 51;
		functionMap[33, 0] = 52;
		functionMap[34, 0] = 53;
		functionMap[35, 0] = 54;
		functionMap[36, 0] = 55;
		functionMap[37, 0] = 56;
		functionMap[38, 0] = 57;
		functionMap[39, 0] = 393264;
		functionMap[40, 0] = 393265;
		functionMap[41, 0] = 393266;
		functionMap[42, 0] = 393267;
		functionMap[43, 0] = 393268;
		functionMap[44, 0] = 393269;
		functionMap[45, 0] = 393270;
		functionMap[46, 0] = 393271;
		functionMap[47, 0] = 393272;
		functionMap[48, 0] = 393273;
		functionMap[49, 0] = 262192;
		functionMap[50, 0] = 262193;
		functionMap[51, 0] = 262194;
		functionMap[52, 0] = 262195;
		functionMap[53, 0] = 262196;
		functionMap[54, 0] = 262197;
		functionMap[55, 0] = 262198;
		functionMap[56, 0] = 262199;
		functionMap[57, 0] = 262200;
		functionMap[58, 0] = 262201;
		functionMap[59, 0] = 65824;
		functionMap[60, 0] = 65819;
		functionMap[61, 0] = 65818;
		functionMap[62, 0] = 270;
		functionMap[63, 0] = 269;
		functionMap[64, 0] = 262266;
		functionMap[65, 0] = 102;
		functionMap[67, 0] = 262183;
		functionMap[68, 0] = 282;
		functionMap[69, 0] = 524301;
		functionMap[69, 1] = 524559;
		functionMap[70, 0] = 786548;
		functionMap[71, 0] = 27;
		functionMap[72, 0] = 524570;
		functionMap[73, 0] = 524571;
		functionMap[74, 0] = 524572;
		functionMap[75, 0] = 524573;
		functionMap[76, 0] = 524574;
		functionMap[77, 0] = 524575;
		functionMap[78, 0] = 524576;
		functionMap[79, 0] = 524577;
		functionMap[80, 0] = 524578;
		functionMap[81, 0] = 524579;
		functionMap[82, 0] = 524580;
		functionMap[83, 0] = 524581;
		functionMap[84, 0] = 262248;
		functionMap[85, 0] = 262262;
		functionMap[86, 0] = 262245;
		functionMap[87, 0] = 281;
		functionMap[88, 0] = 280;
		functionMap[89, 0] = 107;
		functionMap[90, 0] = 106;
		functionMap[91, 0] = 278;
		functionMap[96, 0] = 262251;
		functionMap[95, 0] = 262264;
	}

	private void Update()
	{
		if (hotKeySelectorMode)
		{
			for (int i = 0; i < values.Length; i++)
			{
				if (Input.GetKey((KeyCode)values[i]))
				{
					if (keys[i] == 0 || keys[i] == 3)
					{
						if (i != leftShiftMap && i != rightShiftMap && i != leftCtrlMap && i != rightCtrlMap && i != altMap && i != altGrMap && values[i] < 330 && values[i] != 323 && values[i] != 324 && values[i] != 27 && values[i] != 316 && values[i] != 302 && values[i] != 310 && values[i] != 311 && values[i] != 312 && values[i] != 319 && values[i] != 19)
						{
							hotKeyCurrentKeyPressed = values[i];
							hotKeyCurrentKey = hotKeyCurrentKeyPressed;
							if (isShiftDown())
							{
								hotKeyCurrentKey |= 65536;
							}
							if (isCtrlDown())
							{
								hotKeyCurrentKey |= 131072;
							}
							if (isAltDown())
							{
								hotKeyCurrentKey |= 262144;
							}
						}
						keys[i] = 1;
					}
					else if (keys[i] == 1)
					{
						keys[i] = 2;
					}
				}
				else if (keys[i] == 1 || keys[i] == 2)
				{
					keys[i] = 3;
				}
				else if (keys[i] == 3)
				{
					keys[i] = 0;
				}
			}
			return;
		}
		bool noesisHasKeyboard = FatControler.instance.NoesisHasKeyboard;
		for (int j = 0; j < values.Length; j++)
		{
			if (!noesisHasKeyboard && Input.GetKey((KeyCode)values[j]))
			{
				if (keys[j] == 0 || keys[j] == 3)
				{
					keys[j] = 1;
				}
				else if (keys[j] == 1)
				{
					keys[j] = 2;
				}
			}
			else if (keys[j] == 1 || keys[j] == 2)
			{
				keys[j] = 3;
			}
			else if (keys[j] == 3)
			{
				keys[j] = 0;
			}
		}
		cursorUpHeld = IsKeyHeldDown(KeyCode.UpArrow, ignoreModifiers: true);
		cursorDownHeld = IsKeyHeldDown(KeyCode.DownArrow, ignoreModifiers: true);
		if (Director.instance != null && Director.instance.SimRunning)
		{
			Enums.KeyFunctions[] array = directSentFunctions;
			foreach (Enums.KeyFunctions keyFunctions in array)
			{
				if (!IsActionPressed(keyFunctions) || GameData.Instance.lastGameState == null)
				{
					continue;
				}
				switch (keyFunctions)
				{
				case Enums.KeyFunctions.SetBookmark0:
				case Enums.KeyFunctions.SetBookmark1:
				case Enums.KeyFunctions.SetBookmark2:
				case Enums.KeyFunctions.SetBookmark3:
				case Enums.KeyFunctions.SetBookmark4:
				case Enums.KeyFunctions.SetBookmark5:
				case Enums.KeyFunctions.SetBookmark6:
				case Enums.KeyFunctions.SetBookmark7:
				case Enums.KeyFunctions.SetBookmark8:
				case Enums.KeyFunctions.SetBookmark9:
				{
					Vector3 mouseMapVector = Vector3.zero;
					Vector3Int mouseTileMapVector = Vector3Int.zero;
					int clickDepth = -1;
					GameMap.instance.CalcMapTileFromMousePos(new Vector3(Screen.width / 2, Screen.height / 2, 0f), ref mouseMapVector, ref mouseTileMapVector, ref clickDepth, useBuildingHeight: false);
					int value = -1;
					int value2 = -1;
					GameMapTile mapTile = GameMap.instance.getMapTile(mouseTileMapVector.x, mouseTileMapVector.y);
					if (mapTile != null)
					{
						value = mapTile.gameMapX;
						value2 = mapTile.gameMapY;
					}
					EngineInterface.GameAction(keyFunctions, value, value2);
					break;
				}
				case Enums.KeyFunctions.Barracks:
					if (ConfigSettings.Settings_SH1CentreControls || (GameData.Instance.lastGameState.app_mode == 16 && GameData.Instance.lastGameState.app_sub_mode == 1))
					{
						EngineInterface.GameAction(keyFunctions);
					}
					else
					{
						EngineInterface.GameAction(Enums.GameActionCommand.SelectBuildingType, 9, 8);
					}
					break;
				case Enums.KeyFunctions.HomeKeep:
					if (ConfigSettings.Settings_SH1CentreControls || (GameData.Instance.lastGameState.app_mode == 16 && GameData.Instance.lastGameState.app_sub_mode == 2))
					{
						EngineInterface.GameAction(keyFunctions);
					}
					else
					{
						EngineInterface.GameAction(Enums.GameActionCommand.SelectBuildingType, 40, -1);
					}
					break;
				case Enums.KeyFunctions.Market:
					if (ConfigSettings.Settings_SH1CentreControls || (GameData.Instance.lastGameState.app_mode == 16 && (GameData.Instance.lastGameState.app_sub_mode == 25 || GameData.Instance.lastGameState.app_sub_mode == 56 || GameData.Instance.lastGameState.app_sub_mode == 55 || GameData.Instance.lastGameState.app_sub_mode == 57 || GameData.Instance.lastGameState.app_sub_mode == 54 || GameData.Instance.lastGameState.app_sub_mode == 53)))
					{
						EngineInterface.GameAction(keyFunctions);
					}
					else
					{
						EngineInterface.GameAction(Enums.GameActionCommand.SelectBuildingType, 26, -1);
					}
					break;
				case Enums.KeyFunctions.Granary:
					if (ConfigSettings.Settings_SH1CentreControls || (GameData.Instance.lastGameState.app_mode == 16 && GameData.Instance.lastGameState.app_sub_mode == 4))
					{
						EngineInterface.GameAction(keyFunctions);
					}
					else
					{
						EngineInterface.GameAction(Enums.GameActionCommand.SelectBuildingType, 19, -1);
					}
					break;
				default:
					EngineInterface.GameAction(keyFunctions);
					break;
				}
			}
			if (IsActionPressed(Enums.KeyFunctions.FlattenLandscape) && !Director.instance.Paused)
			{
				EngineInterface.toggleFlattenedLandscapeMode();
			}
			if (IsActionPressed(Enums.KeyFunctions.MapRotateLeft) && !Director.instance.Paused)
			{
				GameMap.instance.RotateMapLeft();
			}
			if (IsActionPressed(Enums.KeyFunctions.MapRotateRight) && !Director.instance.Paused)
			{
				GameMap.instance.RotateMapRight();
			}
			if (IsActionPressed(Enums.KeyFunctions.RotateBuilding) && !Director.instance.Paused && (MainControls.instance.CurrentAction == 5 || MainControls.instance.CurrentAction == 3))
			{
				EngineInterface.GameAction(Enums.GameActionCommand.RotateBuilding, 0, 0);
			}
			if (IsActionPressed(Enums.KeyFunctions.ZoomOut))
			{
				if (doesActionKeyFunctionExist(Enums.KeyFunctions.ZoomIn))
				{
					if (ConfigSettings.Settings_ExtraZoom)
					{
						PerfectPixelWithZoom.instance.adjustZoom(-0.5f);
					}
					else
					{
						PerfectPixelWithZoom.instance.adjustZoom(-1f);
					}
				}
				else if (ConfigSettings.Settings_ExtraZoom)
				{
					PerfectPixelWithZoom.instance.adjustZoom(-0.5f, loop: true);
				}
				else
				{
					PerfectPixelWithZoom.instance.adjustZoom(-1f, loop: true);
				}
			}
			if (IsActionPressed(Enums.KeyFunctions.ZoomIn))
			{
				if (ConfigSettings.Settings_ExtraZoom)
				{
					PerfectPixelWithZoom.instance.adjustZoom(0.5f);
				}
				else
				{
					PerfectPixelWithZoom.instance.adjustZoom(1f);
				}
			}
			if (IsActionPressed(Enums.KeyFunctions.Patrol))
			{
				EditorDirector.instance.placeBuilding(Enums.eMappers.MAPPER_PATROL);
			}
			if (IsActionPressed(Enums.KeyFunctions.StanceStand))
			{
				int app_mode = GameData.Instance.lastGameState.app_mode;
				int app_sub_mode = GameData.Instance.lastGameState.app_sub_mode;
				if (app_mode == 14 && (app_sub_mode == 61 || app_sub_mode == 62))
				{
					EngineInterface.GameAction(Enums.GameActionCommand.Troops_ChangeStance, -1, 287);
				}
			}
			if (IsActionPressed(Enums.KeyFunctions.StanceDefensive))
			{
				int app_mode2 = GameData.Instance.lastGameState.app_mode;
				int app_sub_mode2 = GameData.Instance.lastGameState.app_sub_mode;
				if (app_mode2 == 14 && (app_sub_mode2 == 61 || app_sub_mode2 == 62))
				{
					EngineInterface.GameAction(Enums.GameActionCommand.Troops_ChangeStance, -1, 288);
				}
			}
			if (IsActionPressed(Enums.KeyFunctions.StanceAggressive))
			{
				int app_mode3 = GameData.Instance.lastGameState.app_mode;
				int app_sub_mode3 = GameData.Instance.lastGameState.app_sub_mode;
				if (app_mode3 == 14 && (app_sub_mode3 == 61 || app_sub_mode3 == 62))
				{
					EngineInterface.GameAction(Enums.GameActionCommand.Troops_ChangeStance, -1, 289);
				}
			}
			if (IsActionPressed(Enums.KeyFunctions.Load) && !MainViewModel.Instance.IsMapEditorMode && !Director.instance.MultiplayerGame)
			{
				bool wasPaused = Director.instance.Paused;
				if (!wasPaused)
				{
					Director.instance.SetPausedState(state: true);
				}
				HUD_LoadSaveRequester.OpenLoadSaveRequester(Enums.RequesterTypes.LoadSinglePlayerGame, delegate(string filename, FileHeader header)
				{
					Director.instance.SetPausedState(state: false);
					EditorDirector.instance.stopGameSim();
					EditorDirector.instance.loadSaveGame(header.filePath, header.standAlone_filename, header);
				}, delegate
				{
					if (!wasPaused)
					{
						Director.instance.SetPausedState(state: false);
					}
				});
			}
			if (IsActionPressed(Enums.KeyFunctions.Save) && !MainViewModel.Instance.IsMapEditorMode && Director.instance.SimRunning)
			{
				if (!Director.instance.MultiplayerGame)
				{
					bool wasPaused = Director.instance.Paused;
					if (!wasPaused)
					{
						Director.instance.SetPausedState(state: true);
					}
					HUD_LoadSaveRequester.OpenLoadSaveRequester(Enums.RequesterTypes.SaveSinglePlayerGame, delegate(string filename, FileHeader header)
					{
						string path3 = filename + ".sav";
						string path4 = System.IO.Path.Combine(ConfigSettings.GetSavesPath(), path3);
						EditorDirector.instance.SaveSaveGameOrMap(path4, "");
						if (!wasPaused)
						{
							Director.instance.SetPausedState(state: false);
						}
					}, delegate
					{
						if (!wasPaused)
						{
							Director.instance.SetPausedState(state: false);
						}
					});
				}
				else
				{
					HUD_LoadSaveRequester.OpenLoadSaveRequester(Enums.RequesterTypes.SaveSinglePlayerGame, delegate(string filename, FileHeader header)
					{
						EngineInterface.TriggerMPSave(filename + ".msv");
					}, delegate
					{
					});
				}
			}
			if (!Director.instance.MultiplayerGame)
			{
				if (IsActionPressed(Enums.KeyFunctions.IncreaseEngineSpeed))
				{
					Director.instance.IncreaseFrameRate();
					if (MainViewModel.Instance.Show_HUD_Options)
					{
						MainViewModel.Instance.HUDOptions.RefreshGameSpeed();
					}
				}
				if (IsActionPressed(Enums.KeyFunctions.DecreaseEngineSpeed))
				{
					Director.instance.DecreaseFrameRate();
					if (MainViewModel.Instance.Show_HUD_Options)
					{
						MainViewModel.Instance.HUDOptions.RefreshGameSpeed();
					}
				}
				if (IsActionPressed(Enums.KeyFunctions.Pause) && GameData.Instance.lastGameState != null)
				{
					if (GameData.Instance.lastGameState.game_paused > 0)
					{
						EngineInterface.GameAction(Enums.GameActionCommand.Game_Paused, 0, 0);
						OnScreenText.Instance.addOSTEntry(Enums.eOnScreenText.OST_GAME_PAUSED, 0);
					}
					else
					{
						EngineInterface.GameAction(Enums.GameActionCommand.Game_Paused, 1, 1);
						OnScreenText.Instance.addOSTEntry(Enums.eOnScreenText.OST_GAME_PAUSED, 1);
					}
				}
				if (ConfigSettings.Settings_CheatKeysEnabled)
				{
					if (IsActionPressed(Enums.KeyFunctions.Cheat_freestuff) && GameData.Instance.lastGameState != null)
					{
						if (GameData.Instance.lastGameState.free_buildingCheat == 0)
						{
							GameData.Instance.lastGameState.free_buildingCheat = 1;
							EngineInterface.GameAction(Enums.GameActionCommand.SH1Cheats, 1, 0);
							ConfigSettings.AchievementsDisabled = true;
						}
						else
						{
							GameData.Instance.lastGameState.free_buildingCheat = 0;
							EngineInterface.GameAction(Enums.GameActionCommand.SH1Cheats, 0, 0);
						}
					}
					if (IsActionPressed(Enums.KeyFunctions.Cheat_gold))
					{
						EngineInterface.GameAction(Enums.GameActionCommand.SH1Cheats, 3, 0);
						EngineInterface.GameAction(Enums.GameActionCommand.SH1Cheats, 2, 0);
						ConfigSettings.AchievementsDisabled = true;
					}
				}
			}
			if (Director.instance.MultiplayerGame)
			{
				if (IsActionPressed(Enums.KeyFunctions.ShowPings))
				{
					if (!OnScreenText.Instance.isOSTActive(Enums.eOnScreenText.OST_PINGS))
					{
						OnScreenText.Instance.addOSTEntry(Enums.eOnScreenText.OST_PINGS, 1);
					}
					else
					{
						OnScreenText.Instance.removeOSTEntry(Enums.eOnScreenText.OST_PINGS);
					}
				}
				int num = -1;
				if (IsActionPressed(Enums.KeyFunctions.Insult1))
				{
					num = 1;
				}
				if (IsActionPressed(Enums.KeyFunctions.Insult2))
				{
					num = 2;
				}
				if (IsActionPressed(Enums.KeyFunctions.Insult3))
				{
					num = 3;
				}
				if (IsActionPressed(Enums.KeyFunctions.Insult4))
				{
					num = 4;
				}
				if (IsActionPressed(Enums.KeyFunctions.Insult5))
				{
					num = 5;
				}
				if (IsActionPressed(Enums.KeyFunctions.Insult6))
				{
					num = 6;
				}
				if (IsActionPressed(Enums.KeyFunctions.Insult7))
				{
					num = 7;
				}
				if (IsActionPressed(Enums.KeyFunctions.Insult8))
				{
					num = 8;
				}
				if (IsActionPressed(Enums.KeyFunctions.Insult9))
				{
					num = 9;
				}
				if (IsActionPressed(Enums.KeyFunctions.Insult10))
				{
					num = 10;
				}
				if (IsActionPressed(Enums.KeyFunctions.Insult11))
				{
					num = 11;
				}
				if (IsActionPressed(Enums.KeyFunctions.Insult12))
				{
					num = 12;
				}
				if (num >= 0)
				{
					int playerID = GameData.Instance.playerID;
					int[] players = new int[9];
					int[] teams = new int[9];
					EngineInterface.GetMultiplayerChatInfo(ref players, ref teams);
					List<int> list = new List<int>();
					list.Clear();
					for (int l = 1; l < 9; l++)
					{
						if (players[l] > 0)
						{
							list.Add(players[l]);
						}
					}
					Platform_Multiplayer.Instance.SendIngameChatInsult(list, num);
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Platform_Multiplayer.Instance.getPlayerName(playerID), playerID, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_INSULTS, num));
					SFXManager.instance.playInsult(num);
				}
			}
			if (IsActionPressed(Enums.KeyFunctions.ToggleUI))
			{
				MainControls.instance.toggleUIVisibility();
			}
			if (IsActionPressed(Enums.KeyFunctions.ToggleFrameRate))
			{
				EditorDirector.instance.toggleOSTFrameRate();
			}
			if (IsActionPressed(Enums.KeyFunctions.RadarZoomIn))
			{
				GameMap.instance.changeRadarMapSize(-64);
			}
			if (IsActionPressed(Enums.KeyFunctions.RadarZoomOut))
			{
				GameMap.instance.changeRadarMapSize(64);
			}
			if (!MainViewModel.Instance.IsMapEditorMode)
			{
				if (IsActionPressed(Enums.KeyFunctions.ToggleGoods) && !MainViewModel.Instance.Show_HUD_Goods_Button_Disabled)
				{
					MainViewModel.Instance.ButtonExtendedFeaturesFunction("Goods");
				}
				if (IsActionPressed(Enums.KeyFunctions.ToggleObjectives) && MainViewModel.Instance.Show_HUD_Extras_Button_Objectves)
				{
					MainViewModel.Instance.ButtonExtendedFeaturesFunction("Objectives");
				}
				if (!Director.instance.MultiplayerGame && IsActionPressed(Enums.KeyFunctions.QuickSave))
				{
					DateTime now = DateTime.Now;
					if (now > lastQuickSaveTime)
					{
						lastQuickSaveTime = now.AddSeconds(20.0);
						string path = "QuickSave " + now.Year + "-" + now.Month.ToString("D2") + "-" + now.Day.ToString("D2") + " " + now.ToLongTimeString().Replace(':', '-') + ".sav";
						string path2 = System.IO.Path.Combine(ConfigSettings.GetSavesPath(), path);
						EditorDirector.instance.SaveSaveGameOrMap(path2, "");
					}
				}
			}
			if (!MainViewModel.Instance.IsMapEditorMode && GameData.Instance.game_type == 2 && GameData.Instance.mapType == Enums.GameModes.BUILD && IsActionPressed(Enums.KeyFunctions.FreeBuildEvents))
			{
				HUD_FreebuildMenu.ToggleMenu();
			}
			if (IsActionPressed(Enums.KeyFunctions.OpenChat))
			{
				MainViewModel.Instance.HUDMPChatPanel.ToggleMultiplayerChat();
			}
			if (MainViewModel.Instance.IsMapEditorMode)
			{
				if (IsActionPressed(Enums.KeyFunctions.Special) && GameData.Instance.scenarioOverview != null)
				{
					if (MainViewModel.Instance.ShowingScenario)
					{
						if (GameData.Instance.scenarioOverview.special_start > 0)
						{
							GameData.Instance.scenarioOverview.special_start = 0;
						}
						else
						{
							GameData.Instance.scenarioOverview.special_start = 1;
						}
						EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Special, 0, GameData.Instance.scenarioOverview.special_start);
					}
					else
					{
						MainViewModel.Instance.Show_HUD_ScenarioSpecial = !MainViewModel.Instance.Show_HUD_ScenarioSpecial;
					}
				}
				if (IsActionPressed(Enums.KeyFunctions.EditorHoldTime))
				{
					EngineInterface.GameAction(Enums.GameActionCommand.HoldTime, 0, 0);
				}
				if (IsActionPressed(Enums.KeyFunctions.EditorRespawnLord))
				{
					EngineInterface.GameAction(Enums.GameActionCommand.RespawnLord, 0, 0);
				}
				if (IsActionPressed(Enums.KeyFunctions.EditorWipeAnimals))
				{
					EngineInterface.GameAction(Enums.GameActionCommand.WipeAnimals, 0, 0);
				}
			}
		}
		if (!IsActionPressed(Enums.KeyFunctions.OptionsMenu) || !(ignoreNextEscape < DateTime.UtcNow))
		{
			return;
		}
		if (Director.instance.SimRunning)
		{
			if (GameData.scenario.inGameoverSituation)
			{
				return;
			}
			if (MainViewModel.Instance.Show_HUD_FreebuildMenu)
			{
				HUD_FreebuildMenu.ToggleMenu();
			}
			else if (MainViewModel.Instance.Show_HUD_ControlGroups)
			{
				HUD_ControlGroups.ToggleMenu();
			}
			else if (MainViewModel.Instance.Show_HUD_Confirmation)
			{
				MainViewModel.Instance.HUDConfirmationPopup.ConfirmationClicked(2);
			}
			else if (MainViewModel.Instance.Show_HUD_LoadSaveRequester || MainViewModel.Instance.Show_HUD_LoadSaveRequesterMP)
			{
				MainViewModel.Instance.HUDLoadSaveRequester.CloseRequester();
			}
			else if (MainViewModel.Instance.MPChatVisible)
			{
				MainViewModel.Instance.MPChatVisible = false;
			}
			else if (MainViewModel.Instance.Show_HUD_Briefing)
			{
				MainViewModel.Instance.ButtonBriefingResume(0);
			}
			else if (MainViewModel.Instance.Show_HUD_Help)
			{
				MainViewModel.Instance.HUDHelp.Close();
			}
			else if (MainViewModel.Instance.Show_HUD_Options)
			{
				if (MainViewModel.Instance.OptionsHotKeyPanelVis == Visibility.Visible)
				{
					MainViewModel.Instance.HUDOptions.ButtonClicked(-101);
				}
				else
				{
					MainViewModel.Instance.HUDOptions.ButtonClicked(-1);
				}
			}
			else if (MainViewModel.Instance.ShowingScenario)
			{
				if (MainViewModel.Instance.Show_HUD_IngameMenu)
				{
					MainViewModel.Instance.HUDmain.InGameOptions(null, null);
				}
				else
				{
					MainViewModel.Instance.HUDScenario.StartExitAnim();
				}
			}
			else if (MainControls.instance.CurrentAction == 5 || MainControls.instance.CurrentAction == 3 || MainControls.instance.CurrentAction == 6 || MainControls.instance.CurrentAction == 7)
			{
				MainControls.instance.CurrentAction = 0;
				EditorDirector.instance.EscapeCloseUIPanel();
			}
			else if (GameData.Instance.lastGameState != null && (GameData.Instance.lastGameState.app_mode == 16 || (GameData.Instance.lastGameState.app_mode == 14 && (GameData.Instance.lastGameState.app_sub_mode == 61 || GameData.Instance.lastGameState.app_sub_mode == 62))))
			{
				EditorDirector.instance.EscapeCloseUIPanel();
			}
			else if (MainViewModel.Instance.HUD_Markers_Vis)
			{
				MainViewModel.Instance.HUD_Markers_Vis = false;
			}
			else if (MainViewModel.Instance.Show_HUD_WorkshopUploader)
			{
				if (HUD_WorkshopUploader.CanClose)
				{
					MainViewModel.Instance.Show_HUD_WorkshopUploader = false;
					MainViewModel.Instance.Show_HUD_IngameMenu = true;
				}
			}
			else
			{
				MainViewModel.Instance.HUDmain.InGameOptions(null, null);
			}
		}
		else if (MainViewModel.Instance.Show_HUD_Confirmation)
		{
			MainViewModel.Instance.HUDConfirmationPopup.ConfirmationClicked(2);
		}
		else if (MainViewModel.Instance.Show_HUD_Options)
		{
			if (MainViewModel.Instance.OptionsHotKeyPanelVis == Visibility.Visible)
			{
				MainViewModel.Instance.HUDOptions.ButtonClicked(-101);
			}
			else
			{
				MainViewModel.Instance.HUDOptions.ButtonClicked(-1);
			}
		}
		else if (MainViewModel.Instance.Show_HUD_LoadSaveRequester || MainViewModel.Instance.Show_HUD_LoadSaveRequesterMP)
		{
			MainViewModel.Instance.HUDLoadSaveRequester.CloseRequester();
		}
		else if (MainViewModel.Instance.Show_MapEditor)
		{
			MainViewModel.Instance.FrontEndMenu.ButtonClicked("BackMain");
		}
		else if (MainViewModel.Instance.Show_HUD_Help)
		{
			MainViewModel.Instance.HUDHelp.Close();
		}
		else if (MainViewModel.Instance.Show_Story)
		{
			MainViewModel.Instance.JumpToStoryEnd();
		}
		else if (MainViewModel.Instance.Show_StandaloneSetup)
		{
			MainViewModel.Instance.FRONTStandaloneMission.ButtonClicked("Back");
		}
		else if (MainViewModel.Instance.Show_MultiplayerSetup)
		{
			MainViewModel.Instance.FRONTMultiplayer.ButtonClicked("Back");
		}
		else if (MainViewModel.Instance.Show_CampaignMenu)
		{
			MainViewModel.Instance.FrontEndMenu.ButtonClicked("Combat");
		}
		else if (MainViewModel.Instance.Show_EcoCampaignMenu || MainViewModel.Instance.Show_ExtraEcoCampaignMenu)
		{
			MainViewModel.Instance.FrontEndMenu.ButtonClicked("Eco");
		}
		else if (MainViewModel.Instance.Show_Credits)
		{
			MainViewModel.Instance.FrontEndMenu.ButtonClicked("BackMain");
		}
		else if (MainViewModel.Instance.Show_Extra1CampaignMenu || MainViewModel.Instance.Show_Extra2CampaignMenu || MainViewModel.Instance.Show_Extra3CampaignMenu || MainViewModel.Instance.Show_Extra4CampaignMenu)
		{
			MainViewModel.Instance.FrontEndMenu.ButtonClicked("Jewel");
		}
		else if (MainViewModel.Instance.Show_TrailCampaignMenu || MainViewModel.Instance.Show_Trail2CampaignMenu)
		{
			MainViewModel.Instance.FrontEndMenu.ButtonClicked("Trails");
		}
		else if (MainViewModel.Instance.Show_Frontend_Jewel)
		{
			MainViewModel.Instance.FrontEndMenu.ButtonClicked("Combat");
		}
		else if (MainViewModel.Instance.Show_Frontend_Trails)
		{
			MainViewModel.Instance.FrontEndMenu.ButtonClicked("Combat");
		}
		else if (MainViewModel.Instance.Show_MultiplayerSetup)
		{
			MainViewModel.Instance.FRONTMultiplayer.ButtonClicked("Back");
		}
		else if (MainViewModel.Instance.Show_IntroSequence)
		{
			if (!MainViewModel.Instance.EnterYourNameVis)
			{
				MainViewModel.Instance.Intro_Sequence.ButtonClicked(fromClick: false);
			}
		}
		else if (MainViewModel.Instance.Show_Frontend_Combat || MainViewModel.Instance.Show_Frontend_Eco)
		{
			MainViewModel.Instance.FrontEndMenu.ButtonClicked("BackMain");
		}
	}

	public void ignoreEscape()
	{
		ignoreNextEscape = DateTime.UtcNow.AddMilliseconds(200.0);
	}

	public bool isShiftDown()
	{
		if (keys[leftShiftMap] == 1 || keys[leftShiftMap] == 2)
		{
			return true;
		}
		if (keys[rightShiftMap] == 1 || keys[rightShiftMap] == 2)
		{
			return true;
		}
		return false;
	}

	public bool isCtrlDown()
	{
		if (keys[leftCtrlMap] == 1 || keys[leftCtrlMap] == 2)
		{
			return true;
		}
		if (keys[rightCtrlMap] == 1 || keys[rightCtrlMap] == 2)
		{
			return true;
		}
		return false;
	}

	public bool isAltDown()
	{
		if (keys[altMap] == 1 || keys[altMap] == 2)
		{
			return true;
		}
		if (keys[altGrMap] == 1 || keys[altGrMap] == 2)
		{
			return true;
		}
		return false;
	}

	public bool IsKeyPressed(KeyCode code, bool ignoreModifiers = false)
	{
		int num = (int)(code & (KeyCode)65535);
		if (num >= 0 && num < keyCodeMap.Length)
		{
			int num2 = keyCodeMap[num];
			if (num2 >= 0 && keys[num2] == 1)
			{
				if (!ignoreModifiers)
				{
					bool num3 = (code & (KeyCode)65536) > KeyCode.None;
					bool flag = (code & (KeyCode)131072) > KeyCode.None;
					bool flag2 = (code & (KeyCode)262144) > KeyCode.None;
					if (num3 != isShiftDown())
					{
						return false;
					}
					if (flag != isCtrlDown())
					{
						return false;
					}
					if (flag2 != isAltDown())
					{
						return false;
					}
				}
				return true;
			}
		}
		return false;
	}

	public bool IsKeyHeldDown(KeyCode code, bool ignoreModifiers = false)
	{
		int num = (int)(code & (KeyCode)65535);
		if (num >= 0 && num < keyCodeMap.Length)
		{
			int num2 = keyCodeMap[num];
			if (num2 >= 0 && (keys[num2] == 1 || keys[num2] == 2))
			{
				if (!ignoreModifiers)
				{
					bool num3 = (code & (KeyCode)65536) > KeyCode.None;
					bool flag = (code & (KeyCode)131072) > KeyCode.None;
					bool flag2 = (code & (KeyCode)262144) > KeyCode.None;
					if (num3 != isShiftDown())
					{
						return false;
					}
					if (flag != isCtrlDown())
					{
						return false;
					}
					if (flag2 != isAltDown())
					{
						return false;
					}
				}
				return true;
			}
		}
		return false;
	}

	public bool IsKeyReleased(KeyCode code)
	{
		int num = (int)(code & (KeyCode)65535);
		if (num >= 0 && num < keyCodeMap.Length)
		{
			int num2 = keyCodeMap[num];
			if (num2 >= 0 && keys[num2] == 3)
			{
				bool num3 = (code & (KeyCode)65536) > KeyCode.None;
				bool flag = (code & (KeyCode)131072) > KeyCode.None;
				bool flag2 = (code & (KeyCode)262144) > KeyCode.None;
				if (num3 != isShiftDown())
				{
					return false;
				}
				if (flag != isCtrlDown())
				{
					return false;
				}
				if (flag2 != isAltDown())
				{
					return false;
				}
				return true;
			}
		}
		return false;
	}

	public void LoadFromString(string settingsString)
	{
		string[] array = settingsString.Split("||KEYS||\n");
		if (array.Length != 3)
		{
			return;
		}
		string[] array2 = array[1].Split("\n");
		foreach (string text in array2)
		{
			try
			{
				string[] array3 = text.Split(":");
				if (array3.Length <= 1 || array3[1].Length <= 0)
				{
					continue;
				}
				Enums.KeyFunctions keyFunctions = (Enums.KeyFunctions)Enum.Parse(typeof(Enums.KeyFunctions), array3[0]);
				string[] array4 = array3[1].Split(",");
				int num = (int)(KeyCode)Enum.Parse(typeof(KeyCode), array4[0]);
				int num2 = -1;
				for (int j = 1; j < array4.Length; j++)
				{
					switch (array4[j].ToLowerInvariant())
					{
					case "shift":
						num |= 0x10000;
						break;
					case "ctrl":
						num |= 0x20000;
						break;
					case "alt":
						num |= 0x40000;
						break;
					case "mp":
						num |= 0x80000;
						break;
					}
				}
				if (array3.Length > 2 && array3[2].Length > 0)
				{
					string[] array5 = array3[2].Split(",");
					num2 = (int)(KeyCode)Enum.Parse(typeof(KeyCode), array5[0]);
					for (int k = 1; k < array5.Length; k++)
					{
						switch (array4[k].ToLowerInvariant())
						{
						case "shift":
							num2 |= 0x10000;
							break;
						case "ctrl":
							num2 |= 0x20000;
							break;
						case "alt":
							num2 |= 0x40000;
							break;
						}
					}
				}
				if (num >= 0)
				{
					for (int l = 0; l < 97; l++)
					{
						if (functionMap[l, 0] == num)
						{
							functionMap[l, 0] = -1;
						}
						if (functionMap[l, 1] == num)
						{
							functionMap[l, 1] = -1;
						}
					}
				}
				if (num2 >= 0)
				{
					for (int m = 0; m < 97; m++)
					{
						if (functionMap[m, 0] == num2)
						{
							functionMap[m, 0] = -1;
						}
						if (functionMap[m, 1] == num2)
						{
							functionMap[m, 1] = -1;
						}
					}
				}
				functionMap[(int)keyFunctions, 0] = num;
				functionMap[(int)keyFunctions, 1] = num2;
			}
			catch (Exception)
			{
			}
		}
		Dictionary<int, bool> dictionary = new Dictionary<int, bool>();
		for (int n = 0; n < 97; n++)
		{
			if (functionMap[n, 0] >= 0)
			{
				if (dictionary.ContainsKey(functionMap[n, 0]))
				{
					functionMap[n, 0] = -1;
				}
				else
				{
					dictionary[functionMap[n, 0]] = true;
				}
			}
			if (functionMap[n, 1] >= 0)
			{
				if (dictionary.ContainsKey(functionMap[n, 1]))
				{
					functionMap[n, 1] = -1;
				}
				else
				{
					dictionary[functionMap[n, 1]] = true;
				}
			}
		}
		if (functionMap[16, 0] == -1 && !dictionary[114])
		{
			functionMap[16, 0] = 114;
		}
		if (functionMap[17, 0] == -1 && !dictionary[116])
		{
			functionMap[16, 0] = 116;
		}
		if (functionMap[18, 0] == -1 && !dictionary[121])
		{
			functionMap[16, 0] = 121;
		}
	}

	public string SaveToString()
	{
		string text = "||KEYS||\n";
		for (int i = 0; i < 97; i++)
		{
			if (functionMap[i, 0] == -1 && functionMap[i, 1] == -1)
			{
				continue;
			}
			Enums.KeyFunctions keyFunctions = (Enums.KeyFunctions)i;
			string text2 = keyFunctions.ToString();
			text = text + text2 + ":";
			if (functionMap[i, 0] != -1)
			{
				int num = functionMap[i, 0];
				int num2 = num & 0xFFFF;
				bool flag = (num & 0x10000) > 0;
				bool flag2 = (num & 0x20000) > 0;
				bool flag3 = (num & 0x40000) > 0;
				bool num3 = (num & 0x80000) > 0;
				KeyCode keyCode = (KeyCode)num2;
				string text3 = keyCode.ToString();
				text += text3;
				if (flag)
				{
					text += ",shift";
				}
				if (flag2)
				{
					text += ",ctrl";
				}
				if (flag3)
				{
					text += ",alt";
				}
				if (num3)
				{
					text += ",mp";
				}
				text += ":";
			}
			if (functionMap[i, 1] != -1)
			{
				int num4 = functionMap[i, 1];
				int num5 = num4 & 0xFFFF;
				bool flag4 = (num4 & 0x10000) > 0;
				bool flag5 = (num4 & 0x20000) > 0;
				bool num6 = (num4 & 0x40000) > 0;
				KeyCode keyCode = (KeyCode)num5;
				string text4 = keyCode.ToString();
				text += text4;
				if (flag4)
				{
					text += ",shift";
				}
				if (flag5)
				{
					text += ",ctrl";
				}
				if (num6)
				{
					text += ",alt";
				}
				text += ":";
			}
			text += "\n";
		}
		return text + "||KEYS||\n";
	}

	private bool doesActionKeyFunctionExist(Enums.KeyFunctions function)
	{
		if (functionMap[(int)function, 0] >= 0)
		{
			return true;
		}
		if (functionMap[(int)function, 1] >= 0)
		{
			return true;
		}
		return false;
	}

	public bool IsActionPressed(Enums.KeyFunctions function)
	{
		bool ignoreModifiers = false;
		int num = functionMap[(int)function, 0];
		if (num >= 0 && IsKeyPressed((KeyCode)num, ignoreModifiers))
		{
			return true;
		}
		int num2 = functionMap[(int)function, 1];
		if (num2 >= 0 && IsKeyPressed((KeyCode)num2, ignoreModifiers))
		{
			return true;
		}
		return false;
	}

	public bool IsActionHeldDown(Enums.KeyFunctions function, bool ignoreModifiers = false)
	{
		int num = functionMap[(int)function, 0];
		if (num >= 0 && IsKeyHeldDown((KeyCode)num, ignoreModifiers))
		{
			return true;
		}
		int num2 = functionMap[(int)function, 1];
		if (num2 >= 0 && IsKeyHeldDown((KeyCode)num2, ignoreModifiers))
		{
			return true;
		}
		return false;
	}

	public bool IsActionRelease(Enums.KeyFunctions function)
	{
		int num = functionMap[(int)function, 0];
		if (num >= 0 && IsKeyReleased((KeyCode)num))
		{
			return true;
		}
		int num2 = functionMap[(int)function, 1];
		if (num2 >= 0 && IsKeyReleased((KeyCode)num2))
		{
			return true;
		}
		return false;
	}

	public float HorizontalAxis()
	{
		float radarHeldX = RadarHeldX;
		RadarHeldX = 0f;
		if (!Director.instance.SimRunning && CameraControls2D.instance.isMapLocked())
		{
			return 0f;
		}
		if (MainViewModel.Instance.Show_HUD_LoadSaveRequester)
		{
			return 0f;
		}
		bool flag = IsActionHeldDown(Enums.KeyFunctions.Left, ignoreModifiers: true);
		bool flag2 = IsActionHeldDown(Enums.KeyFunctions.Right, ignoreModifiers: true);
		float num = ConfigSettings.GetScrollSpeed();
		if (isShiftDown())
		{
			num *= 2f;
		}
		if (flag2 && flag)
		{
			return 0f;
		}
		if (flag)
		{
			return -1f * num;
		}
		if (flag2)
		{
			return 1f * num;
		}
		if (ConfigSettings.Settings_PushMapScrolling)
		{
			if (Input.mousePosition.x <= 0f)
			{
				return -1f * num;
			}
			if (Input.mousePosition.x >= (float)(Screen.width - 1))
			{
				return 1f * num;
			}
		}
		return radarHeldX;
	}

	public float VerticalAxis()
	{
		float radarHeldY = RadarHeldY;
		RadarHeldY = 0f;
		if (!Director.instance.SimRunning && CameraControls2D.instance.isMapLocked())
		{
			return 0f;
		}
		if (MainViewModel.Instance.Show_HUD_LoadSaveRequester)
		{
			return 0f;
		}
		bool flag = IsActionHeldDown(Enums.KeyFunctions.Up, ignoreModifiers: true);
		bool flag2 = IsActionHeldDown(Enums.KeyFunctions.Down, ignoreModifiers: true);
		float num = ConfigSettings.GetScrollSpeed();
		if (isShiftDown())
		{
			num *= 2f;
		}
		if (flag && flag2)
		{
			return 0f;
		}
		if (flag)
		{
			return 1f * num;
		}
		if (flag2)
		{
			return -1f * num;
		}
		if (ConfigSettings.Settings_PushMapScrolling)
		{
			if (Input.mousePosition.y <= 0f)
			{
				return -1f * num;
			}
			if (Input.mousePosition.y >= (float)(Screen.height - 1))
			{
				return 1f * num;
			}
		}
		return radarHeldY;
	}

	public KeyCode GetKeyCode(Enums.KeyFunctions function, int keyID)
	{
		if (functionMap[(int)function, keyID] <= 0)
		{
			return KeyCode.None;
		}
		return (KeyCode)functionMap[(int)function, keyID];
	}

	public Enums.KeyFunctions GetHotKeyFunction()
	{
		if (hotKeyCurrentKeyPressed > 0)
		{
			for (int i = 0; i < 97; i++)
			{
				if (functionMap[i, 0] == hotKeyCurrentKeyPressed || functionMap[i, 1] == hotKeyCurrentKeyPressed)
				{
					return (Enums.KeyFunctions)i;
				}
			}
		}
		return Enums.KeyFunctions.NumActions;
	}

	public void SetNewKey(Enums.KeyFunctions func, int newKey, int column)
	{
		if (newKey > 0)
		{
			for (int i = 0; i < 97; i++)
			{
				if (functionMap[i, 0] == newKey)
				{
					functionMap[i, 0] = -1;
				}
				if (functionMap[i, 1] == newKey)
				{
					functionMap[i, 1] = -1;
				}
			}
		}
		functionMap[(int)func, column] = newKey;
	}
}
