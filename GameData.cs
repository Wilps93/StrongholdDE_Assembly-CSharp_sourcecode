using System;
using System.Collections.Generic;
using Stronghold1DE;
using UnityEngine;

public class GameData
{
	public class Scenarios
	{
		public List<ScenarioEvent> events = new List<ScenarioEvent>();

		public bool autoLose;

		public int gameOverState;

		public bool inGameoverSituation;

		private ScenarioEvent endMapEvent;

		public void reset()
		{
			if (!inGameoverSituation)
			{
				events.Clear();
				autoLose = false;
				gameOverState = 0;
				endMapEvent = null;
			}
		}

		public List<ScenarioEvent> getEvents()
		{
			return events;
		}

		public void addEvent(int _eventID, int _valid, int _complete, int _eventType, int _targetAmount, int _currentAmount)
		{
			if (!inGameoverSituation)
			{
				ScenarioEvent item = new ScenarioEvent
				{
					eventID = _eventID,
					valid = _valid,
					complete = _complete,
					eventType = _eventType,
					targetAmount = _targetAmount,
					currentAmount = _currentAmount
				};
				events.Add(item);
			}
		}

		public void updateEvent(int _eventID, int _valid, int _complete, int _eventType, int _targetAmount, int _currentAmount)
		{
			foreach (ScenarioEvent @event in events)
			{
				if (@event.eventID == _eventID)
				{
					@event.targetAmount = _targetAmount;
					break;
				}
			}
		}

		public void setAutoLose()
		{
			autoLose = true;
		}

		public void setGameOverState(int state, int screen)
		{
			if (!inGameoverSituation)
			{
				gameOverState = state;
				if (gameOverState > 0)
				{
					inGameoverSituation = true;
					MainViewModel.Instance.HUDIngameMenu.Hide();
					Director.instance.initGameOver(state, screen);
				}
			}
		}

		public void ManageGameOver(int state, int screen)
		{
			if (!Director.instance.SimRunning)
			{
				return;
			}
			MyAudioManager.Instance.StopAllGameSounds(leaveMusicPlaying: true);
			Director.instance.stopSimThread();
			HUD_MissionOver.FutureVoiceLine = "";
			System.Random random = new System.Random();
			if (state == 1)
			{
				if (random.Next(3) < 2)
				{
					SFXManager.instance.playSpeech(1, "general_victory1.wav", 1f);
				}
				else
				{
					SFXManager.instance.playSpeech(1, "general_victory2.wav", 1f);
				}
				SFXManager.instance.PlayWinTune();
				if (!instance.multiplayerMap)
				{
					if (instance.game_type == 0)
					{
						if (instance.mission_level == 5)
						{
							AchievementsCommon.Instance.CompleteAchievement(Enums.Achievements.Complete_Campaign_Act_1);
						}
						if (instance.mission_level == 11)
						{
							AchievementsCommon.Instance.CompleteAchievement(Enums.Achievements.Complete_Campaign_Act_2);
						}
						if (instance.mission_level == 15)
						{
							AchievementsCommon.Instance.CompleteAchievement(Enums.Achievements.Complete_Campaign_Act_3);
						}
						if (instance.mission_level == 21)
						{
							AchievementsCommon.Instance.CompleteAchievement(Enums.Achievements.Complete_Campaign_Act_4);
						}
						else
						{
							if (instance.mission_level == ConfigSettings.Settings_Progress_Campaign && !ConfigSettings.TempMissionUnlock)
							{
								ConfigSettings.Settings_Progress_Campaign++;
							}
							FrontendMenus.CurrentSelectedMission = instance.mission_level + 1;
						}
					}
					else if (instance.game_type == 5)
					{
						if (instance.mission_level == 37)
						{
							HUD_MissionOver.setFutureVoiceLine("Eco_Camp_Comp.wav");
							AchievementsCommon.Instance.CompleteAchievement(Enums.Achievements.Complete_Campaign_Eco);
						}
						else
						{
							if (instance.mission_level - 32 == ConfigSettings.Settings_Progress_EcoCampaign && !ConfigSettings.TempMissionUnlock)
							{
								ConfigSettings.Settings_Progress_EcoCampaign++;
							}
							FrontendMenus.CurrentSelectedEcoMission = instance.mission_level - 32 + 1;
						}
					}
					else if (instance.game_type == 7)
					{
						if (instance.mission_level == 47)
						{
							AchievementsCommon.Instance.CompleteAchievement(Enums.Achievements.Complete_Extra_Act_1);
						}
						else
						{
							if (instance.mission_level - 40 == ConfigSettings.Settings_Progress_Extra1Campaign && !ConfigSettings.TempMissionUnlock)
							{
								ConfigSettings.Settings_Progress_Extra1Campaign++;
							}
							FrontendMenus.CurrentSelectedExtra1Mission = instance.mission_level - 40 + 1 + 10;
						}
					}
					else if (instance.game_type == 8)
					{
						if (instance.mission_level == 57)
						{
							AchievementsCommon.Instance.CompleteAchievement(Enums.Achievements.Complete_Extra_Act_2);
						}
						else
						{
							if (instance.mission_level - 50 == ConfigSettings.Settings_Progress_Extra2Campaign && !ConfigSettings.TempMissionUnlock)
							{
								ConfigSettings.Settings_Progress_Extra2Campaign++;
							}
							FrontendMenus.CurrentSelectedExtra2Mission = instance.mission_level - 50 + 1 + 20;
						}
					}
					else if (instance.game_type == 9)
					{
						if (instance.mission_level == 67)
						{
							AchievementsCommon.Instance.CompleteAchievement(Enums.Achievements.Complete_Extra_Act_3);
						}
						else
						{
							if (instance.mission_level - 60 == ConfigSettings.Settings_Progress_Extra3Campaign && !ConfigSettings.TempMissionUnlock)
							{
								ConfigSettings.Settings_Progress_Extra3Campaign++;
							}
							FrontendMenus.CurrentSelectedExtra3Mission = instance.mission_level - 60 + 1 + 30;
						}
					}
					else if (instance.game_type == 12)
					{
						if (instance.mission_level == 87)
						{
							AchievementsCommon.Instance.CompleteAchievement(Enums.Achievements.Complete_Extra_Eco);
						}
						else
						{
							if (instance.mission_level - 80 == ConfigSettings.Settings_Progress_ExtraEcoCampaign && !ConfigSettings.TempMissionUnlock)
							{
								ConfigSettings.Settings_Progress_ExtraEcoCampaign++;
							}
							FrontendMenus.CurrentSelectedExtraEcoMission = instance.mission_level - 80 + 1;
						}
					}
					else if (instance.game_type == 10)
					{
						if (instance.mission_level == 77)
						{
							AchievementsCommon.Instance.CompleteAchievement(Enums.Achievements.Complete_Extra_Act_4);
						}
						else
						{
							if (instance.mission_level - 70 == ConfigSettings.Settings_Progress_Extra4Campaign && !ConfigSettings.TempMissionUnlock)
							{
								ConfigSettings.Settings_Progress_Extra4Campaign++;
							}
							FrontendMenus.CurrentSelectedExtra4Mission = instance.mission_level - 70 + 1 + 40;
						}
					}
					else if (instance.game_type == 11)
					{
						if (instance.mission_text_id == 10)
						{
							HUD_MissionOver.setFutureVoiceLine("Castle_Trail_Comp.wav");
							AchievementsCommon.Instance.CompleteAchievement(Enums.Achievements.Complete_Historical_Trail);
						}
						else
						{
							if (instance.mission_text_id == ConfigSettings.Settings_Progress_Trail && !ConfigSettings.TempMissionUnlock)
							{
								ConfigSettings.Settings_Progress_Trail++;
							}
							FrontendMenus.CurrentSelectedTrailMission = Instance.mission_text_id + 1;
						}
					}
					else if (instance.game_type == 13)
					{
						if (instance.mission_text_id == 10)
						{
							HUD_MissionOver.setFutureVoiceLine("Castle_Trail_Comp.wav");
							AchievementsCommon.Instance.CompleteAchievement(Enums.Achievements.Complete_Noble_Trail);
						}
						else
						{
							if (instance.mission_text_id == ConfigSettings.Settings_Progress_Trail2 && !ConfigSettings.TempMissionUnlock)
							{
								ConfigSettings.Settings_Progress_Trail2++;
							}
							FrontendMenus.CurrentSelectedTrail2Mission = Instance.mission_text_id + 1;
						}
					}
					else
					{
						switch (Instance.mapType)
						{
						case Enums.GameModes.SIEGE:
							if (Instance.playerID == 1)
							{
								switch (random.Next(3))
								{
								case 0:
									HUD_MissionOver.setFutureVoiceLine("Siege_Def_Victory_1.wav");
									break;
								case 1:
									HUD_MissionOver.setFutureVoiceLine("Siege_Def_Victory_2.wav");
									break;
								case 2:
									HUD_MissionOver.setFutureVoiceLine("Siege_Def_Victory_3.wav");
									break;
								}
								AchievementsCommon.Instance.CompleteAchievement(Enums.Achievements.Complete_Siege_As_Defender);
							}
							else
							{
								switch (random.Next(3))
								{
								case 0:
									HUD_MissionOver.setFutureVoiceLine("Siege_Att_Victory_1.wav");
									break;
								case 1:
									HUD_MissionOver.setFutureVoiceLine("Siege_Att_Victory_2.wav");
									break;
								case 2:
									HUD_MissionOver.setFutureVoiceLine("Siege_Att_Victory_3.wav");
									break;
								}
								AchievementsCommon.Instance.CompleteAchievement(Enums.Achievements.Complete_Siege_As_Attacker);
							}
							break;
						case Enums.GameModes.INVASION:
							switch (random.Next(3))
							{
							case 0:
								HUD_MissionOver.setFutureVoiceLine("Invasion_Victory_1.wav");
								break;
							case 1:
								HUD_MissionOver.setFutureVoiceLine("Invasion_Victory_2.wav");
								break;
							case 2:
								HUD_MissionOver.setFutureVoiceLine("Invasion_Victory_3.wav");
								break;
							}
							if (Instance.difficulty_level == Enums.GameDifficulty.DIFFICULTY_VERYHARD)
							{
								AchievementsCommon.Instance.CompleteAchievement(Enums.Achievements.Complete_An_Invasion_On_Very_Hard);
							}
							break;
						case Enums.GameModes.ECO:
							switch (random.Next(3))
							{
							case 0:
								HUD_MissionOver.setFutureVoiceLine("Eco_Victory_1.wav");
								break;
							case 1:
								HUD_MissionOver.setFutureVoiceLine("Eco_Victory_2.wav");
								break;
							case 2:
								HUD_MissionOver.setFutureVoiceLine("Eco_Victory_3.wav");
								break;
							}
							if (Instance.difficulty_level == Enums.GameDifficulty.DIFFICULTY_VERYHARD)
							{
								AchievementsCommon.Instance.CompleteAchievement(Enums.Achievements.Complete_An_Eco_Mission_On_Very_Hard);
							}
							break;
						}
					}
					ConfigSettings.SaveSettings();
					EngineInterface.ScoreData scoreData = EngineInterface.GetScoreData();
					HUD_MissionOver.ShowVictory((Enums.VictoryScreens)screen, scoreData);
					return;
				}
				EngineInterface.MPScoreData mPScoreData = EngineInterface.GetMPScoreData();
				if (mPScoreData.winners[instance.playerID] > 0)
				{
					switch (random.Next(3))
					{
					case 0:
						HUD_MissionOver.setFutureVoiceLine("MP_Victory_1.wav");
						break;
					case 1:
						HUD_MissionOver.setFutureVoiceLine("MP_Victory_2.wav");
						break;
					case 2:
						HUD_MissionOver.setFutureVoiceLine("MP_Victory_3.wav");
						break;
					}
					AchievementsCommon.Instance.CompleteAchievement(Enums.Achievements.Win_Multiplier_Any);
					int num = 0;
					for (int i = 1; i < 9; i++)
					{
						if (mPScoreData.valid[i] > 0)
						{
							num++;
						}
					}
					if (num == 8)
					{
						AchievementsCommon.Instance.CompleteAchievement(Enums.Achievements.Win_Multiplier_Vs_7);
					}
				}
				HUD_MissionOver.ShowMPVictory((Enums.VictoryScreens)screen, mPScoreData);
				return;
			}
			if (Instance.game_type == 0 && Instance.mission6Prestart)
			{
				MainViewModel.Instance.StartCampaignMission(6);
				return;
			}
			SFXManager.instance.playSpeech(1, "general_warning2.wav", 1f);
			SFXManager.instance.PlayLoseTune();
			if (instance.multiplayerMap)
			{
				switch (random.Next(6))
				{
				case 0:
					HUD_MissionOver.setFutureVoiceLine("MP_Defeat_1.wav");
					break;
				case 1:
					HUD_MissionOver.setFutureVoiceLine("MP_Defeat_2.wav");
					break;
				case 2:
					HUD_MissionOver.setFutureVoiceLine("MP_Defeat_3.wav");
					break;
				case 3:
					HUD_MissionOver.setFutureVoiceLine("MP_Defeat_4.wav");
					break;
				case 4:
					HUD_MissionOver.setFutureVoiceLine("MP_Defeat_5.wav");
					break;
				case 5:
					HUD_MissionOver.setFutureVoiceLine("MP_Defeat_6.wav");
					break;
				}
				EngineInterface.MPScoreData mPScoreData2 = EngineInterface.GetMPScoreData();
				HUD_MissionOver.ShowMPDefeat((Enums.DefeatScreens)screen, mPScoreData2);
				return;
			}
			if (Instance.mapType == Enums.GameModes.SIEGE)
			{
				if (Instance.playerID == 1)
				{
					switch (random.Next(3))
					{
					case 0:
						HUD_MissionOver.setFutureVoiceLine("Siege_Def_Defeat_1.wav");
						break;
					case 1:
						HUD_MissionOver.setFutureVoiceLine("Siege_Def_Defeat_2.wav");
						break;
					case 2:
						HUD_MissionOver.setFutureVoiceLine("Siege_Def_Defeat_3.wav");
						break;
					}
				}
				else
				{
					switch (random.Next(3))
					{
					case 0:
						HUD_MissionOver.setFutureVoiceLine("Siege_Att_Defeat_1.wav");
						break;
					case 1:
						HUD_MissionOver.setFutureVoiceLine("Siege_Att_Defeat_2.wav");
						break;
					case 2:
						HUD_MissionOver.setFutureVoiceLine("Siege_Att_Defeat_3.wav");
						break;
					}
				}
			}
			HUD_MissionOver.ShowDefeat((Enums.DefeatScreens)screen);
		}

		public int getGameOverState()
		{
			return gameOverState;
		}

		public void setEndGameTimer(int _eventID, int _nowDate, int _endDate, int _text)
		{
			if (endMapEvent == null)
			{
				endMapEvent = new ScenarioEvent
				{
					eventID = _eventID,
					valid = _text,
					complete = 0,
					eventType = 0,
					targetAmount = _endDate,
					currentAmount = _nowDate
				};
			}
			else
			{
				endMapEvent.eventID = _eventID;
				endMapEvent.valid = _text;
				endMapEvent.targetAmount = _endDate;
				endMapEvent.currentAmount = _nowDate;
			}
		}

		public string getWinTimer(ref int startDate, ref int nowDate, ref int endDate)
		{
			if (endMapEvent == null)
			{
				return null;
			}
			int valid = endMapEvent.valid;
			if (valid <= 0)
			{
				return null;
			}
			startDate = 0;
			endDate = endMapEvent.targetAmount;
			nowDate = endMapEvent.currentAmount;
			return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_OBJECTIVES, valid);
		}
	}

	public class ScenarioEvent
	{
		public int eventID;

		public int valid;

		public int complete;

		public int eventType;

		public int targetAmount;

		public int currentAmount;
	}

	private static readonly GameData instance;

	private static readonly int[] game_data_txt;

	public static Scenarios scenario;

	public int[] resources = new int[25];

	private int _app_mode;

	private int _app_sub_mode;

	private int _last_app_sub_mode;

	private int _current_buildingchimp_itemID = -1;

	private int _debug_value_1;

	private int _debug_value_2;

	private EngineInterface.PlayState _lastGameState;

	private Enums.GameModes _mapType;

	private bool _siegeThat;

	private bool _multiplayerMap;

	private bool _multiplayerKOTHMap;

	private int _playerID;

	private string _currentFileName = "";

	private string _currentMapName = "";

	private int _game_type;

	private int _mission_level;

	private bool _mission6Prestart;

	private int _mission_text_id;

	private Enums.GameDifficulty _difficulty_level;

	private int numHintsUnlockedForMission;

	public const int MAX_TOTAL_TROOPS_IN_INVASION = 500;

	public static readonly int[] buildingAvailbleOrder;

	public static readonly Enums.eChimps[] scenarioBarracksTroopsAvailableTypes;

	public static readonly int[] startingGoodsLimits;

	public static readonly int[] scn_start_troops_max;

	public static readonly int[] scn_start_siege_equipment_max;

	public static readonly int[] scn_max_invasion_sizes;

	public EngineInterface.ScenarioOverview scenarioOverview;

	public string ansiMissionText;

	public string unicodeMissionText;

	public string utf8MissionText;

	public bool showAlternateMissionTextForBriefing;

	public bool importedStrings;

	public int message_catagory1 = 3;

	public int message_catagory2 = 1;

	public int message_catagory3;

	private int[] message_id = new int[1000];

	private int[] message_types = new int[1000];

	public int num_of_current_type;

	public int[] current_list = new int[1000];

	public static readonly int[] start_event_min;

	public static readonly int[] start_event_max;

	public static readonly int[] start_event_multiplier;

	public static readonly int[] start_event_types;

	public static readonly int[][] start_event_goods;

	public static readonly int[] start_event_goods_text;

	public static readonly int[] lord_killed_list;

	public static readonly int[] scenarioActionsOrder;

	public static readonly int[] scenarioEventsOrder;

	public static readonly Enums.eChimps[] scenarioAttackingForceTypesOrder;

	public static readonly int[] freeBuildEventsOrder;

	private static readonly int[] nhints_eco;

	private static readonly int[] nhints;

	private static readonly int[] nhints_extra1;

	private static readonly int[] nhints_extra2;

	private static readonly int[] nhints_extra3;

	private static readonly int[] nhints_extra4;

	private static readonly int[] nhints_extra5;

	private string cachedTrailBriefing = "";

	public string cachedMissionName = "";

	public static GameData Instance => instance;

	public int app_mode
	{
		get
		{
			return _app_mode;
		}
		set
		{
			_app_mode = value;
		}
	}

	public int app_sub_mode
	{
		get
		{
			return _app_sub_mode;
		}
		set
		{
			_app_sub_mode = value;
		}
	}

	public int last_app_sub_mode
	{
		get
		{
			return _last_app_sub_mode;
		}
		set
		{
			_last_app_sub_mode = value;
		}
	}

	public int current_buildingchimp_itemID
	{
		get
		{
			return _current_buildingchimp_itemID;
		}
		set
		{
			_current_buildingchimp_itemID = value;
		}
	}

	public int debug_value_1
	{
		get
		{
			return _debug_value_1;
		}
		set
		{
			_debug_value_1 = value;
		}
	}

	public int debug_value_2
	{
		get
		{
			return _debug_value_2;
		}
		set
		{
			_debug_value_2 = value;
		}
	}

	public EngineInterface.PlayState lastGameState
	{
		get
		{
			return _lastGameState;
		}
		set
		{
			_lastGameState = value;
		}
	}

	public Enums.GameModes mapType
	{
		get
		{
			return _mapType;
		}
		set
		{
			_mapType = value;
		}
	}

	public bool siegeThat
	{
		get
		{
			return _siegeThat;
		}
		set
		{
			_siegeThat = value;
		}
	}

	public bool multiplayerMap
	{
		get
		{
			return _multiplayerMap;
		}
		set
		{
			_multiplayerMap = value;
		}
	}

	public bool multiplayerKOTHMap
	{
		get
		{
			return _multiplayerKOTHMap;
		}
		set
		{
			_multiplayerKOTHMap = value;
		}
	}

	public int playerID
	{
		get
		{
			return _playerID;
		}
		set
		{
			_playerID = value;
		}
	}

	public string currentFileName
	{
		get
		{
			return _currentFileName;
		}
		set
		{
			_currentFileName = value;
		}
	}

	public string currentMapName
	{
		get
		{
			return _currentMapName;
		}
		set
		{
			_currentMapName = value;
		}
	}

	public int game_type
	{
		get
		{
			return _game_type;
		}
		set
		{
			_game_type = value;
		}
	}

	public int mission_level
	{
		get
		{
			return _mission_level;
		}
		set
		{
			_mission_level = value;
		}
	}

	public bool mission6Prestart
	{
		get
		{
			return _mission6Prestart;
		}
		set
		{
			_mission6Prestart = value;
		}
	}

	public int mission_text_id
	{
		get
		{
			return _mission_text_id;
		}
		set
		{
			_mission_text_id = value;
		}
	}

	public Enums.GameDifficulty difficulty_level
	{
		get
		{
			return _difficulty_level;
		}
		set
		{
			_difficulty_level = value;
		}
	}

	static GameData()
	{
		instance = new GameData();
		game_data_txt = new int[530]
		{
			0, 0, 0, 0, 0, 6, 0, 0, 0, 0,
			6, 0, 0, 0, 0, 3, 0, 0, 0, 0,
			10, 0, 0, 0, 0, 20, 0, 0, 0, 0,
			20, 0, 0, 0, 0, 5, 0, 0, 0, 0,
			15, 0, 0, 0, 0, 0, 15, 0, 0, 0,
			0, 0, 0, 0, 0, 10, 0, 0, 0, 0,
			20, 0, 0, 0, 100, 20, 0, 0, 0, 200,
			10, 0, 0, 0, 100, 20, 0, 0, 0, 100,
			10, 0, 0, 0, 100, 10, 0, 0, 0, 0,
			10, 0, 0, 0, 0, 10, 0, 0, 0, 0,
			20, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			50, 0, 0, 0, 25, 10, 0, 0, 0, 50,
			10, 0, 0, 0, 100, 10, 0, 0, 0, 100,
			10, 0, 0, 0, 0, 0, 0, 0, 0, 30,
			0, 0, 10, 0, 100, 10, 0, 0, 0, 0,
			15, 0, 0, 0, 0, 15, 0, 0, 0, 0,
			5, 0, 0, 0, 0, 10, 0, 0, 0, 0,
			20, 0, 0, 0, 0, 20, 0, 0, 0, 400,
			0, 10, 0, 0, 250, 0, 20, 0, 0, 350,
			0, 40, 0, 0, 500, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 20, 0, 0, 0,
			0, 10, 0, 0, 0, 10, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 10, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			8, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 50, 0, 0, 0, 0, 50,
			0, 0, 0, 0, 50, 0, 0, 0, 0, 30,
			0, 0, 0, 0, 30, 6, 0, 0, 0, 0,
			0, 0, 0, 1, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			10, 0, 0, 0, 0, 0, 10, 0, 0, 0,
			0, 15, 0, 0, 0, 0, 35, 0, 0, 0,
			0, 40, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 150, 0, 0, 0, 0, 150,
			0, 0, 0, 0, 150, 0, 0, 0, 0, 150,
			0, 0, 0, 0, 5, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 50, 0, 0, 0, 0, 50,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 50,
			0, 0, 0, 0, 50, 0, 0, 0, 0, 50,
			0, 0, 0, 0, 50, 0, 0, 0, 0, 50,
			0, 0, 0, 0, 50, 0, 0, 0, 0, 50,
			0, 0, 0, 0, 50, 10, 0, 0, 0, 100,
			0, 0, 0, 0, 30, 0, 0, 0, 0, 30,
			0, 0, 0, 0, 30, 0, 0, 0, 0, 30,
			0, 0, 0, 0, 30, 0, 0, 0, 0, 30
		};
		scenario = new Scenarios();
		buildingAvailbleOrder = new int[64]
		{
			32, 33, 34, 35, 36, 21, 22, 23, 24, 25,
			12, 15, 10, 9, 8, 27, 29, 91, 31, 30,
			16, 17, 204, 279, 11, 66, 67, 139, 47, 134,
			135, 136, 137, 138, 133, 197, 45, 41, 43, 44,
			49, 51, 52, 53, 54, 55, 62, 58, 59, 60,
			61, 46, 48, 56, 57, 64, 42, 39, 65, 101,
			102, 103, 93, 125
		};
		scenarioBarracksTroopsAvailableTypes = new Enums.eChimps[7]
		{
			Enums.eChimps.CHIMP_TYPE_ARCHER,
			Enums.eChimps.CHIMP_TYPE_XBOWMAN,
			Enums.eChimps.CHIMP_TYPE_SPEARMAN,
			Enums.eChimps.CHIMP_TYPE_PIKEMAN,
			Enums.eChimps.CHIMP_TYPE_MACEMAN,
			Enums.eChimps.CHIMP_TYPE_SWORDSMAN,
			Enums.eChimps.CHIMP_TYPE_KNIGHT
		};
		startingGoodsLimits = new int[25]
		{
			0, 0, 5000, 200, 1000, 0, 200, 0, 200, 200,
			1000, 1000, 1000, 1000, 200, 50000, 0, 200, 200, 200,
			200, 200, 200, 200, 200
		};
		scn_start_troops_max = new int[10] { 200, 100, 400, 100, 200, 100, 100, 100, 100, 100 };
		scn_start_siege_equipment_max = new int[6] { 10, 10, 5, 5, 20, 5 };
		scn_max_invasion_sizes = new int[16]
		{
			200, 100, 200, 100, 100, 100, 50, 100, 50, 10,
			10, 10, 10, 10, 50, 10
		};
		start_event_min = new int[40]
		{
			0, 10, 0, 0, 1, 1, 1, 1, 0, 0,
			0, 0, 0, 1, 1, 0, 1, 1, 0, 0,
			0, 0, 1, 1, 1, 0, 1, 1, 1, 1,
			1, 0, 1, 1, 0, 0, 0, 0, 0, 0
		};
		start_event_max = new int[40]
		{
			0, 500, 0, 0, 25000, 10000, 10000, 10000, 0, 0,
			0, 0, 0, 1000, 1000, 0, 100, 10000, 0, 0,
			100, 100, 5, 5, 10, 0, 10, 10, 10, 10,
			10, 0, 1000, 100, 0, 0, 0, 0, 0, 60
		};
		start_event_multiplier = new int[40]
		{
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1
		};
		start_event_types = new int[40]
		{
			0, 0, 2, 2, 0, 1, 1, 1, 0, 0,
			0, 0, 0, 2, 2, 0, 0, 1, 0, 0,
			0, 0, 0, 0, 3, 0, 3, 3, 3, 0,
			3, 0, 1, 1, 0, 0, 0, 0, 0, 0
		};
		start_event_goods = new int[40][]
		{
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[20]
			{
				10, 11, 12, 13, 14, 2, 3, 4, 6, 8,
				9, 17, 18, 19, 20, 21, 22, 23, 24, -1
			},
			new int[20]
			{
				10, 11, 12, 13, 14, 2, 3, 4, 6, 8,
				9, 17, 18, 19, 20, 21, 22, 23, 24, -1
			},
			new int[20]
			{
				10, 11, 12, 13, 14, 2, 3, 4, 6, 8,
				9, 17, 18, 19, 20, 21, 22, 23, 24, -1
			},
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[20]
			{
				10, 11, 12, 13, 14, 2, 3, 4, 6, 8,
				9, 17, 18, 19, 20, 21, 22, 23, 24, -1
			},
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1],
			new int[1]
		};
		start_event_goods_text = new int[25]
		{
			156, 0, 50, 51, 52, 0, 53, 0, 54, 55,
			56, 57, 58, 59, 60, 61, 0, 62, 63, 64,
			65, 66, 67, 68, 69
		};
		lord_killed_list = new int[6] { 156, 9, 10, 11, 12, 213 };
		scenarioActionsOrder = new int[33]
		{
			128, 154, 129, 155, 130, 133, 139, 140, 141, 142,
			143, 144, 147, 145, 131, 132, 146, 148, 149, 150,
			177, 178, 179, 180, 181, 183, 184, 134, 135, 136,
			152, 153, 182
		};
		scenarioEventsOrder = new int[32]
		{
			106, 107, 108, 109, 110, 111, 112, 113, 123, 114,
			201, 115, 116, 117, 118, 119, 120, 121, 190, 191,
			192, 193, 195, 194, 196, 197, 198, 200, 199, 202,
			203, 209
		};
		scenarioAttackingForceTypesOrder = new Enums.eChimps[16]
		{
			Enums.eChimps.CHIMP_TYPE_ARCHER,
			Enums.eChimps.CHIMP_TYPE_XBOWMAN,
			Enums.eChimps.CHIMP_TYPE_SPEARMAN,
			Enums.eChimps.CHIMP_TYPE_PIKEMAN,
			Enums.eChimps.CHIMP_TYPE_MACEMAN,
			Enums.eChimps.CHIMP_TYPE_SWORDSMAN,
			Enums.eChimps.CHIMP_TYPE_KNIGHT,
			Enums.eChimps.CHIMP_TYPE_MONK,
			Enums.eChimps.CHIMP_TYPE_ENGINEER,
			Enums.eChimps.CHIMP_TYPE_LADDERMAN,
			Enums.eChimps.CHIMP_TYPE_TUNNELER,
			Enums.eChimps.CHIMP_TYPE_CATAPULT,
			Enums.eChimps.CHIMP_TYPE_TREBUCHET,
			Enums.eChimps.CHIMP_TYPE_SIEGE_TOWER,
			Enums.eChimps.CHIMP_TYPE_BATTERING_RAM,
			Enums.eChimps.CHIMP_TYPE_PORTABLE_SHIELD
		};
		freeBuildEventsOrder = new int[10] { 133, 139, 143, 144, 145, 146, 148, 149, 150, 179 };
		nhints_eco = new int[5] { 0, 1, 2, 1, 2 };
		nhints = new int[21]
		{
			2, 2, 3, 2, 4, 1, 2, 2, 1, 1,
			1, 1, 2, 4, 1, 2, 1, 1, 3, 1,
			1
		};
		nhints_extra1 = new int[7] { 5, 4, 4, 3, 5, 4, 4 };
		nhints_extra2 = new int[7] { 3, 5, 4, 5, 5, 3, 4 };
		nhints_extra3 = new int[7] { 4, 4, 4, 3, 4, 4, 4 };
		nhints_extra4 = new int[7] { 4, 4, 3, 4, 3, 4, 4 };
		nhints_extra5 = new int[7] { 4, 4, 4, 4, 4, 4, 4 };
	}

	private GameData()
	{
	}

	public static int getStructureCost(int structure, int commodity)
	{
		int num = structure * 5 + commodity;
		if (num >= 0 && num < game_data_txt.Length)
		{
			return game_data_txt[num];
		}
		return 0;
	}

	public static int getStructureMapperCost(int mapper, int commodity)
	{
		return getStructureCost(getStructFromMapper(mapper), commodity);
	}

	public static void getStructureCosts(int structure, ref int wood, ref int stone, ref int iron, ref int pitch, ref int gold)
	{
		if (Instance.lastGameState != null && ((structure == 3 && Instance.lastGameState.freeWoodcutter > 0) || (structure == 19 && Instance.lastGameState.freeGranary > 0)))
		{
			return;
		}
		switch (structure)
		{
		case 127:
		case 128:
		case 129:
		case 130:
			structure = 47;
			break;
		case 131:
		case 132:
			structure = 46;
			break;
		case 133:
		case 134:
			structure = 45;
			break;
		case 118:
		case 119:
		case 120:
			structure = 66;
			break;
		case 121:
		case 122:
			structure = 104;
			break;
		case 115:
			structure = 86;
			break;
		case 116:
			structure = 87;
			break;
		}
		int num = structure * 5;
		if (num >= 0 && num + 4 < game_data_txt.Length)
		{
			wood = game_data_txt[num];
			stone = game_data_txt[num + 1];
			iron = game_data_txt[num + 2];
			pitch = game_data_txt[num + 3];
			gold = game_data_txt[num + 4];
		}
		if (Instance.game_type == 6)
		{
			wood = 0;
			iron = 0;
			pitch = 0;
			switch (structure)
			{
			case 148:
				gold = 12;
				break;
			case 149:
				gold = 8;
				break;
			case 150:
				gold = 20;
				break;
			case 151:
				gold = 20;
				break;
			case 152:
				gold = 20;
				break;
			case 153:
				gold = 40;
				break;
			case 154:
				gold = 40;
				break;
			case 155:
				gold = 1;
				break;
			case 156:
				gold = 30;
				break;
			case 158:
				gold = 10;
				break;
			case 164:
				gold = 20;
				break;
			case 68:
				gold = 2;
				break;
			case 67:
				gold = 5;
				break;
			case 108:
				gold = 1;
				break;
			case 114:
				gold = 5;
				break;
			case 159:
				gold = game_data_txt[404];
				break;
			case 160:
				gold = game_data_txt[409];
				break;
			case 115:
				gold = game_data_txt[434];
				break;
			case 116:
				gold = game_data_txt[439];
				break;
			case 163:
				gold = game_data_txt[424];
				break;
			case 162:
				gold = game_data_txt[414];
				break;
			case 161:
				gold = game_data_txt[419];
				break;
			}
		}
	}

	public static void getStructureMapperCosts(int mapper, ref int wood, ref int stone, ref int iron, ref int pitch, ref int gold)
	{
		getStructureCosts(getStructFromMapper(mapper), ref wood, ref stone, ref iron, ref pitch, ref gold);
	}

	public static int getStructFromMapper(int mapper)
	{
		switch (mapper)
		{
		case 50:
			return 12;
		case 82:
			return 14;
		case 83:
			return 13;
		case 84:
			return 15;
		case 85:
			return 16;
		case 51:
			return 3;
		case 55:
			return 4;
		case 56:
			return 20;
		case 52:
			return 10;
		case 80:
			return 19;
		case 81:
			return 11;
		case 65:
			return 35;
		case 180:
			return 28;
		case 86:
			return 8;
		case 87:
			return 9;
		case 53:
			return 2;
		case 54:
			return 1;
		case 70:
			return 30;
		case 71:
			return 31;
		case 72:
			return 32;
		case 73:
			return 33;
		case 74:
			return 34;
		case 77:
			return 26;
		case 78:
			return 7;
		case 75:
			return 17;
		case 76:
			return 18;
		case 92:
			return 22;
		case 93:
			return 23;
		case 90:
			return 5;
		case 91:
			return 6;
		case 28:
			return 61;
		case 101:
		case 146:
		case 147:
			return 45;
		case 102:
		case 144:
		case 145:
			return 46;
		case 140:
			return 47;
		case 141:
			return 47;
		case 142:
			return 47;
		case 143:
			return 47;
		case 104:
			return 48;
		case 105:
			return 49;
		case 95:
			return 36;
		case 96:
			return 37;
		case 97:
			return 38;
		case 88:
			return 24;
		case 89:
			return 25;
		case 66:
			return 85;
		case 57:
			return 50;
		case 59:
			return 52;
		case 60:
			return 40;
		case 61:
			return 41;
		case 62:
			return 42;
		case 63:
			return 43;
		case 64:
			return 44;
		case 110:
			return 74;
		case 111:
			return 75;
		case 112:
			return 76;
		case 113:
			return 77;
		case 114:
			return 78;
		case 115:
			return 86;
		case 116:
			return 87;
		case 117:
			return 88;
		case 118:
			return 89;
		case 119:
			return 79;
		case 160:
			return 66;
		case 161:
			return 66;
		case 162:
			return 66;
		case 163:
			return 66;
		case 164:
			return 66;
		case 165:
			return 66;
		case 166:
			return 66;
		case 167:
			return 66;
		case 168:
			return 66;
		case 169:
			return 66;
		case 170:
			return 66;
		case 171:
			return 66;
		case 248:
			return 39;
		case 249:
			return 39;
		case 250:
			return 39;
		case 251:
			return 39;
		case 252:
			return 39;
		case 253:
			return 39;
		case 254:
			return 39;
		case 255:
			return 39;
		case 256:
			return 39;
		case 257:
			return 39;
		case 258:
			return 39;
		case 259:
			return 39;
		case 260:
			return 39;
		case 175:
			return 65;
		case 176:
			return 62;
		case 177:
			return 63;
		case 190:
			return 80;
		case 191:
			return 81;
		case 192:
			return 82;
		case 193:
			return 83;
		case 194:
			return 84;
		case 98:
			return 67;
		case 99:
			return 68;
		case 94:
			return 69;
		case 301:
		case 302:
		case 303:
		case 304:
			return 91;
		case 305:
			return 92;
		case 306:
			return 93;
		case 307:
			return 94;
		case 308:
			return 95;
		case 309:
			return 96;
		case 310:
			return 97;
		case 311:
			return 98;
		case 312:
			return 99;
		case 313:
		case 314:
		case 315:
		case 316:
		case 317:
			return 100;
		case 318:
		case 319:
		case 320:
		case 321:
		case 322:
			return 101;
		case 323:
			return 102;
		case 324:
			return 103;
		case 325:
		case 326:
		case 327:
		case 328:
			return 104;
		case 329:
			return 105;
		case 330:
			return 27;
		case 210:
			return 86;
		case 211:
			return 87;
		default:
			return 0;
		}
	}

	public static int getChimpGoldCost(int troopChimpType)
	{
		if (Director.instance.MultiplayerGame && !Instance.lastGameState.MP_TroopsCostGold)
		{
			return troopChimpType switch
			{
				30 => 30, 
				5 => 30, 
				29 => 4, 
				_ => 0, 
			};
		}
		return troopChimpType switch
		{
			30 => 30, 
			5 => 30, 
			29 => 4, 
			22 => 12, 
			23 => 20, 
			24 => 8, 
			25 => 20, 
			26 => 20, 
			27 => 40, 
			28 => 40, 
			_ => 0, 
		};
	}

	public void InitGameInfo(EngineInterface.LoadMapReturnData initData)
	{
		Platform_Multiplayer.MPGameActive = false;
		numHintsUnlockedForMission = 0;
		game_type = initData.game_type;
		siegeThat = initData.siege_that != 0;
		multiplayerMap = initData.multiplayerMap != 0;
		multiplayerKOTHMap = initData.multiplayerKOTHMap != 0;
		switch (initData.siege_or_invasion)
		{
		case 0:
			mapType = Enums.GameModes.SIEGE;
			break;
		case 1:
			mapType = Enums.GameModes.INVASION;
			break;
		case 2:
			mapType = Enums.GameModes.ECO;
			break;
		case 3:
			mapType = Enums.GameModes.BUILD;
			break;
		}
		mission_level = initData.mission_level;
		mission_text_id = initData.textID;
		difficulty_level = (Enums.GameDifficulty)initData.difficulty_level;
		playerID = initData.playerID;
		EditorDirector.instance.SetLocalPlayer(playerID);
		scenario.inGameoverSituation = false;
	}

	public void setGameState(EngineInterface.PlayState gameState)
	{
		if (lastGameState == null && gameState.force_app_mode == 0)
		{
			gameState.force_app_mode = 5;
		}
		lastGameState = gameState;
		resources = gameState.resources;
		AchievementsCommon.Instance.UpdateValue(4, resources[15]);
		AchievementsCommon.Instance.UpdateValue(10, gameState.population);
		debug_value_1 = gameState.debug_value1;
		debug_value_2 = gameState.debug_value2;
		int num = app_mode;
		int num2 = app_sub_mode;
		app_mode = gameState.app_mode;
		app_sub_mode = gameState.app_sub_mode;
		game_type = gameState.game_type;
		if (gameState.completeSelectionBox > 0)
		{
			MainControls.instance.CurrentAction = 8;
			TroopSelector.instance.startSelection(Input.mousePosition, Input.mousePosition);
			TroopSelector.instance.selection_on = true;
			EditorDirector.instance.triggerTroopsSelection();
		}
		bool inTroopUI = false;
		if (app_mode == 14 && (app_sub_mode == 61 || app_sub_mode == 62))
		{
			inTroopUI = true;
			if (num != 14 || (num2 != 61 && num2 != 62))
			{
				MainViewModel.Instance.TroopsSelectedGameAction(fromInitialOpening: true);
			}
			EditorDirector.instance.mapChanged = true;
		}
		else if (app_mode == 16)
		{
			if (num != 16 || num2 != app_sub_mode || (app_sub_mode == 70 && current_buildingchimp_itemID != gameState.in_chimp) || (app_sub_mode != 70 && current_buildingchimp_itemID != gameState.in_structure))
			{
				if (app_sub_mode == 70)
				{
					current_buildingchimp_itemID = gameState.in_chimp;
				}
				else
				{
					current_buildingchimp_itemID = gameState.in_structure;
				}
				MainViewModel.Instance.InBuildingGameAction();
			}
		}
		else if (app_mode == 14 && (num != 14 || num2 == 61 || num2 == 62))
		{
			if (gameState.game_type != 1 && gameState.game_type != 6)
			{
				MainViewModel.Instance.DefaultGameUIGameAction();
				if (MainViewModel.Instance.buildScreenID == 17 && !MainViewModel.Instance.FreezeMainControls)
				{
					MainViewModel.Instance.HUDmain.NewBuildScreenIndustry();
					MainViewModel.Instance.HUDmain.RefTabBuildIndustry.IsChecked = true;
				}
			}
			else if (num != 12)
			{
				MainViewModel.Instance.DefaultMapEditorUIGameAction();
			}
		}
		EditorDirector.instance.updateDLLSelectedTroops(gameState, inTroopUI);
		if (MainViewModel.Instance.IsMapEditorMode)
		{
			gameState.MAPEDITOR_numshieldsToDisplay = getNumShieldsToDisplayInEditor(ref gameState.MAPEDITOR_allowLandscapeEditing);
		}
		OnScreenText.Instance.updateOST(gameState);
	}

	public void SetCameraFromGameState(EngineInterface.PlayState gameState)
	{
		if (gameState == null || gameState.camera_target_x <= 0 || gameState.camera_target_y <= 0)
		{
			return;
		}
		GameMap.instance.mapGameTileToTilemapCoord(gameState.camera_target_x, gameState.camera_target_y, out var tileMapX, out var tileMapY);
		GameMapTile mapTile = GameMap.instance.getMapTile(tileMapX, tileMapY);
		if (mapTile == null)
		{
			return;
		}
		Vector3 spritePosVector = GameMap.instance.getSpritePosVector(tileMapX, tileMapY);
		if (!EngineInterface.FlattenedLandscape && gameState.camera_target_flat == 0)
		{
			float height = mapTile.height;
			spritePosVector.y += height;
			if (gameState.camera_target_z != -123)
			{
				spritePosVector.y += (float)gameState.camera_target_z / 32f;
			}
		}
		CameraControls2D.instance.setCameraPos(spritePosVector.x + 0.5f, spritePosVector.y - 0.5f);
		GameMap.instance.PreCalcScreenCentre();
	}

	private int getNumShieldsToDisplayInEditor(ref bool allowLandscapeEditing)
	{
		allowLandscapeEditing = true;
		if (multiplayerMap)
		{
			return 8;
		}
		switch (mapType)
		{
		case Enums.GameModes.BUILD:
			return 1;
		case Enums.GameModes.ECO:
			return 1;
		case Enums.GameModes.INVASION:
			return 5;
		case Enums.GameModes.SIEGE:
			if (siegeThat)
			{
				allowLandscapeEditing = false;
			}
			return 1;
		default:
			return 1;
		}
	}

	public static int getTroopOfsetForAttackingForce(Enums.eChimps troopType)
	{
		return troopType switch
		{
			Enums.eChimps.CHIMP_TYPE_ARCHER => 0, 
			Enums.eChimps.CHIMP_TYPE_XBOWMAN => 1, 
			Enums.eChimps.CHIMP_TYPE_SPEARMAN => 2, 
			Enums.eChimps.CHIMP_TYPE_PIKEMAN => 3, 
			Enums.eChimps.CHIMP_TYPE_MACEMAN => 4, 
			Enums.eChimps.CHIMP_TYPE_SWORDSMAN => 5, 
			Enums.eChimps.CHIMP_TYPE_KNIGHT => 6, 
			Enums.eChimps.CHIMP_TYPE_MONK => 9, 
			Enums.eChimps.CHIMP_TYPE_ENGINEER => 8, 
			Enums.eChimps.CHIMP_TYPE_LADDERMAN => 7, 
			Enums.eChimps.CHIMP_TYPE_TUNNELER => 105, 
			Enums.eChimps.CHIMP_TYPE_CATAPULT => 100, 
			Enums.eChimps.CHIMP_TYPE_TREBUCHET => 101, 
			Enums.eChimps.CHIMP_TYPE_SIEGE_TOWER => 102, 
			Enums.eChimps.CHIMP_TYPE_BATTERING_RAM => 103, 
			Enums.eChimps.CHIMP_TYPE_PORTABLE_SHIELD => 104, 
			_ => -1, 
		};
	}

	public static int getMaxTroopsForAttackingForce(Enums.eChimps troopType)
	{
		int troopOfsetForAttackingForce = getTroopOfsetForAttackingForce(troopType);
		if (troopOfsetForAttackingForce < 0)
		{
			return 0;
		}
		if (troopOfsetForAttackingForce >= 100)
		{
			if (troopOfsetForAttackingForce < 106)
			{
				return scn_start_siege_equipment_max[troopOfsetForAttackingForce - 100];
			}
		}
		else if (troopOfsetForAttackingForce < 10)
		{
			return scn_start_troops_max[troopOfsetForAttackingForce];
		}
		return 0;
	}

	public static int getTroopOfsetForInvasion(Enums.eChimps troopType)
	{
		return troopType switch
		{
			Enums.eChimps.CHIMP_TYPE_ARCHER => 0, 
			Enums.eChimps.CHIMP_TYPE_XBOWMAN => 1, 
			Enums.eChimps.CHIMP_TYPE_SPEARMAN => 2, 
			Enums.eChimps.CHIMP_TYPE_PIKEMAN => 3, 
			Enums.eChimps.CHIMP_TYPE_MACEMAN => 4, 
			Enums.eChimps.CHIMP_TYPE_SWORDSMAN => 5, 
			Enums.eChimps.CHIMP_TYPE_KNIGHT => 6, 
			Enums.eChimps.CHIMP_TYPE_MONK => 14, 
			Enums.eChimps.CHIMP_TYPE_ENGINEER => 8, 
			Enums.eChimps.CHIMP_TYPE_LADDERMAN => 7, 
			Enums.eChimps.CHIMP_TYPE_TUNNELER => 15, 
			Enums.eChimps.CHIMP_TYPE_CATAPULT => 9, 
			Enums.eChimps.CHIMP_TYPE_TREBUCHET => 10, 
			Enums.eChimps.CHIMP_TYPE_SIEGE_TOWER => 11, 
			Enums.eChimps.CHIMP_TYPE_BATTERING_RAM => 12, 
			Enums.eChimps.CHIMP_TYPE_PORTABLE_SHIELD => 13, 
			_ => -1, 
		};
	}

	public static int getMaxTroopsForInvasion(Enums.eChimps troopType)
	{
		int troopOfsetForInvasion = getTroopOfsetForInvasion(troopType);
		if (troopOfsetForInvasion < 0)
		{
			return 0;
		}
		if (troopOfsetForInvasion < 16)
		{
			return scn_max_invasion_sizes[troopOfsetForInvasion];
		}
		return 0;
	}

	public static int getTroopOfsetForSiegeSetup(Enums.eChimps troopType)
	{
		return troopType switch
		{
			Enums.eChimps.CHIMP_TYPE_ARCHER => 0, 
			Enums.eChimps.CHIMP_TYPE_XBOWMAN => 1, 
			Enums.eChimps.CHIMP_TYPE_SPEARMAN => 2, 
			Enums.eChimps.CHIMP_TYPE_PIKEMAN => 3, 
			Enums.eChimps.CHIMP_TYPE_MACEMAN => 4, 
			Enums.eChimps.CHIMP_TYPE_SWORDSMAN => 5, 
			Enums.eChimps.CHIMP_TYPE_KNIGHT => 6, 
			Enums.eChimps.CHIMP_TYPE_MONK => 9, 
			Enums.eChimps.CHIMP_TYPE_ENGINEER => 8, 
			Enums.eChimps.CHIMP_TYPE_LADDERMAN => 7, 
			Enums.eChimps.CHIMP_TYPE_TUNNELER => 10, 
			_ => -1, 
		};
	}

	public void SetScenarioOverview(EngineInterface.ScenarioOverview data)
	{
		scenarioOverview = data;
	}

	public int getNumEntries()
	{
		if (scenarioOverview != null)
		{
			return scenarioOverview.entries.Count;
		}
		return 0;
	}

	public bool getScenarioEntryOverviewText(int entryID, ref string date, ref string body, ref string repeat, ref int entryType)
	{
		if (entryID < getNumEntries())
		{
			EngineInterface.ScenarioOverviewEntry scenarioOverviewEntry = scenarioOverview.entries[entryID];
			date = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MONTHS, scenarioOverviewEntry.month) + " " + scenarioOverviewEntry.year;
			repeat = "";
			entryType = scenarioOverviewEntry.entryType;
			switch (scenarioOverviewEntry.entryType)
			{
			case 1:
				body = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_INVASION) + " " + scenarioOverviewEntry.data1;
				if (scenarioOverviewEntry.repeatCount > 0)
				{
					if (scenarioOverviewEntry.repeatCount == 1)
					{
						repeat = "(x" + scenarioOverviewEntry.repeatCount + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_MONTH) + ")";
					}
					else
					{
						repeat = "(x" + scenarioOverviewEntry.repeatCount + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_MONTHS) + ")";
					}
				}
				break;
			case 2:
				body = Translate.Instance.getMessageLibraryText(scenarioOverviewEntry.message);
				break;
			case 3:
				if (scenarioOverviewEntry.data1 == 2)
				{
					body = Translate.Instance.getMessageLibraryText(scenarioOverviewEntry.message);
					break;
				}
				body = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, scenarioOverviewEntry.message);
				switch (scenarioOverviewEntry.data1)
				{
				case 33:
					if (scenarioOverviewEntry.data2 == 0)
					{
						body = body + " : " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 14);
					}
					else
					{
						body = body + " : " + scenarioOverviewEntry.action_data_marker;
					}
					break;
				case 4:
				case 11:
				case 17:
				case 18:
				case 20:
				case 30:
					body = body + " : " + scenarioOverviewEntry.data2;
					break;
				case 32:
					body = body + " : " + scenarioOverviewEntry.action_data_marker + " / " + HUD_Scenario.getReinforcementsName(scenarioOverviewEntry.action_data_reinforcement);
					break;
				case 31:
					body = body + " : " + scenarioOverviewEntry.action_data_marker + " / " + HUD_Scenario.getAllegianceTeamName(scenarioOverviewEntry.action_data_reinforcement);
					break;
				}
				if (scenarioOverviewEntry.repeatDuration > 0 && scenarioOverviewEntry.repeatCount != 1)
				{
					repeat = "(";
					if (scenarioOverviewEntry.repeatCount != 10)
					{
						repeat += scenarioOverviewEntry.repeatCount;
					}
					if (scenarioOverviewEntry.repeatDuration == 1)
					{
						repeat = repeat + "x" + scenarioOverviewEntry.repeatDuration + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_MONTH) + ")";
					}
					else
					{
						repeat = repeat + "x" + scenarioOverviewEntry.repeatDuration + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_MONTHS) + ")";
					}
				}
				break;
			}
		}
		return false;
	}

	public void NewMapForEditor()
	{
		ansiMissionText = (unicodeMissionText = (utf8MissionText = ""));
		showAlternateMissionTextForBriefing = false;
	}

	public void SetMissionTextFromHeader(FileHeader header)
	{
		ansiMissionText = header.ansiMissionText;
		unicodeMissionText = header.unicodeMissionText;
		utf8MissionText = header.utf8MissionText;
		showAlternateMissionTextForBriefing = header.showAlternateMissionTextForBriefing;
		if (header.missionTextType != 0)
		{
			mission_text_id = header.missionTextNumber;
		}
		else
		{
			mission_text_id = 0;
		}
	}

	public void importScenarioTextInfo(int[] ids, int[] types)
	{
		importedStrings = true;
		for (int i = 0; i < 1000; i++)
		{
			message_id[i] = ids[i];
			message_types[i] = types[i];
		}
	}

	private void set_message_catagories(int type)
	{
		message_catagory1 = 3;
		message_catagory2 = 2;
		message_catagory3 = 1;
		switch (type)
		{
		case 1:
			message_catagory1 = 0;
			break;
		case 2:
			message_catagory1 = 1;
			break;
		case 3:
			message_catagory1 = 2;
			break;
		case 21:
			message_catagory1 = 3;
			message_catagory2 = 1;
			break;
		case 22:
			message_catagory1 = 3;
			message_catagory2 = 1;
			break;
		case 23:
			message_catagory1 = 3;
			message_catagory2 = 2;
			break;
		case 24:
			message_catagory1 = 3;
			message_catagory2 = 2;
			break;
		case 5:
			message_catagory1 = 4;
			message_catagory2 = 0;
			message_catagory3 = 0;
			break;
		case 6:
			message_catagory1 = 4;
			message_catagory2 = 0;
			message_catagory3 = 1;
			break;
		case 7:
			message_catagory1 = 4;
			message_catagory2 = 0;
			message_catagory3 = 2;
			break;
		case 8:
			message_catagory1 = 4;
			message_catagory2 = 0;
			message_catagory3 = 3;
			break;
		case 9:
			message_catagory1 = 4;
			message_catagory2 = 1;
			message_catagory3 = 0;
			break;
		case 10:
			message_catagory1 = 4;
			message_catagory2 = 1;
			message_catagory3 = 1;
			break;
		case 11:
			message_catagory1 = 4;
			message_catagory2 = 1;
			message_catagory3 = 2;
			break;
		case 12:
			message_catagory1 = 4;
			message_catagory2 = 1;
			message_catagory3 = 3;
			break;
		case 13:
			message_catagory1 = 4;
			message_catagory2 = 2;
			message_catagory3 = 0;
			break;
		case 14:
			message_catagory1 = 4;
			message_catagory2 = 2;
			message_catagory3 = 1;
			break;
		case 15:
			message_catagory1 = 4;
			message_catagory2 = 2;
			message_catagory3 = 2;
			break;
		case 16:
			message_catagory1 = 4;
			message_catagory2 = 2;
			message_catagory3 = 3;
			break;
		case 17:
			message_catagory1 = 4;
			message_catagory2 = 3;
			message_catagory3 = 0;
			break;
		case 18:
			message_catagory1 = 4;
			message_catagory2 = 3;
			message_catagory3 = 1;
			break;
		case 19:
			message_catagory1 = 4;
			message_catagory2 = 3;
			message_catagory3 = 2;
			break;
		case 20:
			message_catagory1 = 4;
			message_catagory2 = 3;
			message_catagory3 = 3;
			break;
		case 25:
			message_catagory1 = 5;
			message_catagory2 = 0;
			message_catagory3 = 0;
			break;
		case 26:
			message_catagory1 = 5;
			message_catagory2 = 1;
			message_catagory3 = 0;
			break;
		case 27:
			message_catagory1 = 5;
			message_catagory2 = 2;
			message_catagory3 = 0;
			break;
		case 28:
			message_catagory1 = 5;
			message_catagory2 = 3;
			message_catagory3 = 0;
			break;
		case 4:
			break;
		}
	}

	public void setMessagesFromMessageID(int messageID)
	{
		if (messageID > 0)
		{
			int message_catagories = message_types[messageID];
			set_message_catagories(message_catagories);
		}
		else
		{
			message_catagory1 = 3;
			message_catagory2 = 1;
			message_catagory3 = 0;
		}
		rescan_current_list();
	}

	public void rescan_current_list()
	{
		int num = 0;
		int num2 = generate_type();
		for (int i = 0; i < 1000; i++)
		{
			if (message_types[i] == num2)
			{
				current_list[num++] = i;
			}
		}
		num_of_current_type = num;
		for (int j = 0; j < num - 1; j++)
		{
			for (int i = 0; i < num - 1 - j; i++)
			{
				int num3 = message_id[current_list[i]];
				int num4 = message_id[current_list[i + 1]];
				if (num3 > num4)
				{
					int num5 = current_list[i];
					current_list[i] = current_list[i + 1];
					current_list[i + 1] = num5;
				}
			}
		}
	}

	private int generate_type()
	{
		if (message_catagory1 < 3)
		{
			return message_catagory1 + 1;
		}
		if (message_catagory1 == 3)
		{
			return message_catagory2 + 21;
		}
		if (message_catagory1 == 5)
		{
			return message_catagory2 + 25;
		}
		return message_catagory2 * 4 + message_catagory3 + 5;
	}

	public static int getAttackingForceTroopTypeFromIndex(int index)
	{
		return index switch
		{
			1 => 23, 
			2 => 24, 
			3 => 25, 
			4 => 26, 
			5 => 27, 
			6 => 28, 
			7 => 29, 
			8 => 30, 
			9 => 37, 
			100 => 39, 
			101 => 40, 
			102 => 58, 
			103 => 59, 
			104 => 60, 
			105 => 5, 
			_ => 22, 
		};
	}

	public static int getInvasionSizeTroopTypeFromIndex(int index)
	{
		return index switch
		{
			1 => 23, 
			2 => 24, 
			3 => 25, 
			4 => 26, 
			5 => 27, 
			6 => 28, 
			7 => 29, 
			8 => 30, 
			14 => 37, 
			9 => 39, 
			10 => 40, 
			12 => 58, 
			11 => 59, 
			13 => 60, 
			15 => 5, 
			_ => 22, 
		};
	}

	public int GetNumHintsForCurrentMission()
	{
		if (game_type == 5)
		{
			return nhints_eco[mission_level - 33];
		}
		if (game_type == 0)
		{
			return nhints[mission_level - 1];
		}
		if (game_type == 7)
		{
			return nhints_extra1[mission_level - 41];
		}
		if (game_type == 8)
		{
			return nhints_extra2[mission_level - 51];
		}
		if (game_type == 9)
		{
			return nhints_extra3[mission_level - 61];
		}
		if (game_type == 10)
		{
			return nhints_extra4[mission_level - 71];
		}
		if (game_type == 12)
		{
			return nhints_extra5[mission_level - 81];
		}
		return 0;
	}

	public int GetHintTextSectionForCurrentMission()
	{
		if (game_type == 5)
		{
			return 191 + (mission_level - 33);
		}
		if (game_type == 0)
		{
			return 102 + (mission_level - 1 << 2);
		}
		if (game_type == 7)
		{
			return 29 + (mission_level - 41) * 6;
		}
		if (game_type == 8)
		{
			return 29 + (mission_level - 51) * 6;
		}
		if (game_type == 9)
		{
			return 29 + (mission_level - 61) * 6;
		}
		if (game_type == 10)
		{
			return 29 + (mission_level - 71) * 6;
		}
		if (game_type == 12)
		{
			return 29 + (mission_level - 81) * 6;
		}
		return -1;
	}

	private void getCachedMissionName(FileHeader header)
	{
		string userMapName = header.standAlone_filename.Replace(".map", "");
		cachedMissionName = Translate.Instance.translateMapNames(userMapName);
	}

	public void getCachedTrailBriefingFromSave(FileHeader header)
	{
		cachedMissionName = "";
		cachedTrailBriefing = "";
		if (header != null)
		{
			getCachedMissionName(header);
			int num = -1;
			switch (header.standAlone_filename.ToLowerInvariant())
			{
			case "dunnottar":
				num = 1;
				break;
			case "warkworth":
				num = 2;
				break;
			case "pembroke":
				num = 3;
				break;
			case "warwick":
				num = 4;
				break;
			case "bodiam":
				num = 5;
				break;
			case "hastings":
				num = 6;
				break;
			case "chateau gaillard":
				num = 7;
				break;
			case "chateau de coucy":
				num = 8;
				break;
			case "marksburg":
				num = 9;
				break;
			case "heuneburg":
				num = 10;
				break;
			case "castillo de coca":
				num = 39;
				break;
			case "heidelberg_nobletrail":
				cachedTrailBriefing = Translate.Instance.getMessageLibraryText(13);
				break;
			case "fougeres":
				num = 41;
				break;
			case "biskupin":
				num = 42;
				break;
			case "malbork":
				num = 43;
				break;
			case "monteriggioni_nobletrail":
				cachedTrailBriefing = Translate.Instance.getMessageLibraryText(128);
				break;
			case "koblenz stolzanfels":
				cachedTrailBriefing = Translate.Instance.getMessageLibraryText(129);
				break;
			case "diósgyőr":
				num = 46;
				break;
			case "fenis":
				num = 47;
				break;
			case "niedzica":
				num = 48;
				break;
			}
			if (num > 0)
			{
				cachedTrailBriefing = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_TRAIL_NAMES, num + 12);
			}
		}
	}

	public string GetMissionBriefing(FileHeader header = null, bool fromBriefing = false)
	{
		string result = "";
		if (header != null)
		{
			cachedMissionName = header.display_filename;
		}
		if (game_type == 2)
		{
			if (header != null && header.builtinMap)
			{
				int num = -1;
				switch (header.fileName.ToLowerInvariant())
				{
				case "dunnottar":
					num = 1;
					break;
				case "warkworth":
					num = 2;
					break;
				case "pembroke":
					num = 3;
					break;
				case "warwick":
					num = 4;
					break;
				case "bodiam":
					num = 5;
					break;
				case "hastings":
					num = 6;
					break;
				case "chateau gaillard":
					num = 7;
					break;
				case "chateau de coucy":
					num = 8;
					break;
				case "marksburg":
					num = 9;
					break;
				case "heuneburg":
					num = 10;
					break;
				case "forest":
					num = 22;
					break;
				case "sleeping volcano":
					num = 52;
					break;
				case "ogrodzieniec":
					num = 54;
					break;
				case "castillo de coca":
					num = 39;
					break;
				case "heidelberg_nobletrail":
					cachedTrailBriefing = Translate.Instance.getMessageLibraryText(13);
					return cachedTrailBriefing;
				case "fougeres":
					num = 41;
					break;
				case "biskupin":
					num = 42;
					break;
				case "malbork":
					num = 43;
					break;
				case "monteriggioni_nobletrail":
					cachedTrailBriefing = Translate.Instance.getMessageLibraryText(128);
					return cachedTrailBriefing;
				case "koblenz stolzanfels":
					cachedTrailBriefing = Translate.Instance.getMessageLibraryText(129);
					return cachedTrailBriefing;
				case "diósgyőr":
					num = 46;
					break;
				case "fenis":
					num = 47;
					break;
				case "niedzica":
					num = 48;
					break;
				}
				if (num > 0)
				{
					cachedTrailBriefing = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_TRAIL_NAMES, num + 12);
					return cachedTrailBriefing;
				}
			}
			if (fromBriefing && cachedTrailBriefing != "")
			{
				return cachedTrailBriefing;
			}
			cachedTrailBriefing = "";
			if (mission_text_id > 0)
			{
				result = Translate.Instance.getMessageLibraryText(mission_text_id);
			}
			else
			{
				if (mission_text_id != -1)
				{
					return utf8MissionText;
				}
				result = Translate.Instance.getMessageLibraryText(0);
			}
		}
		cachedTrailBriefing = "";
		if (game_type == 11)
		{
			cachedMissionName = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_TRAIL_NAMES, mission_text_id);
			return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_TRAIL_NAMES, mission_text_id + 12);
		}
		if (game_type == 13)
		{
			cachedMissionName = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_TRAIL_NAMES, mission_text_id + 40);
			return mission_text_id switch
			{
				7 => Translate.Instance.getMessageLibraryText(129), 
				6 => Translate.Instance.getMessageLibraryText(128), 
				2 => Translate.Instance.getMessageLibraryText(13), 
				_ => Translate.Instance.lookUpText(Enums.eTextSections.TEXT_TRAIL_NAMES, mission_text_id + 40 + 10), 
			};
		}
		if (game_type == 5)
		{
			int index = mission_level - 33 + 1;
			result = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_ECO_MISSION_BRIEFINGS, index);
		}
		else if (game_type == 7)
		{
			int index2 = (mission_level - 41) * 3 + 6;
			result = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, index2);
		}
		else if (game_type == 8)
		{
			int index3 = (mission_level - 51) * 3 + 6;
			result = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, index3);
		}
		else if (game_type == 9)
		{
			int index4 = (mission_level - 61) * 3 + 6;
			result = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, index4);
		}
		else if (game_type == 10)
		{
			int index5 = (mission_level - 71) * 3 + 6;
			result = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, index5);
		}
		else if (game_type == 12)
		{
			result = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_ECOBRIEFINGS, mission_level - 81 + 1);
		}
		else if (game_type == 0)
		{
			int section = 100 + (mission_level - 1 << 2);
			int num2 = 0;
			num2++;
			if (mission_level == 6 && !mission6Prestart)
			{
				num2++;
			}
			result = Translate.Instance.lookUpText((Enums.eTextSections)section, num2);
		}
		else if (game_type == 3)
		{
			if (header != null && header.builtinMap)
			{
				int num3 = -1;
				switch (header.fileName.ToLowerInvariant())
				{
				case "close call":
					num3 = 14;
					break;
				case "the watchtower":
					num3 = 16;
					break;
				case "pits of hell":
					num3 = 18;
					break;
				case "dark forest":
					num3 = 20;
					break;
				case "snake island":
					num3 = 24;
					break;
				case "vile gulch":
					num3 = 26;
					break;
				case "4 rivers":
					num3 = 50;
					break;
				case "forlorn hope":
					num3 = 56;
					break;
				case "three's a crowd":
					num3 = 58;
					break;
				case "downstream":
					num3 = 62;
					break;
				}
				if (num3 > 0)
				{
					cachedTrailBriefing = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_TRAIL_NAMES, num3 + 12);
					return cachedTrailBriefing;
				}
			}
			if (mission_text_id == 0)
			{
				return utf8MissionText;
			}
			result = Translate.Instance.getMessageLibraryText(mission_text_id);
		}
		return result;
	}

	public string GetStrategyText()
	{
		if (game_type == 7 || game_type == 8 || game_type == 9 || game_type == 10 || game_type == 12)
		{
			return "";
		}
		int hintTextSectionForCurrentMission = GetHintTextSectionForCurrentMission();
		if (hintTextSectionForCurrentMission >= 0)
		{
			return Translate.Instance.lookUpText((Enums.eTextSections)hintTextSectionForCurrentMission, 1);
		}
		return "";
	}

	public string GetHintText(int hintLine)
	{
		int hintTextSectionForCurrentMission = GetHintTextSectionForCurrentMission();
		if (hintTextSectionForCurrentMission >= 0 && hintLine < numHintsUnlockedForMission)
		{
			if (game_type == 7)
			{
				return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, hintTextSectionForCurrentMission + hintLine);
			}
			if (game_type == 8)
			{
				return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, hintTextSectionForCurrentMission + hintLine);
			}
			if (game_type == 9)
			{
				return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, hintTextSectionForCurrentMission + hintLine);
			}
			if (game_type == 10)
			{
				return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, hintTextSectionForCurrentMission + hintLine);
			}
			if (game_type == 12)
			{
				return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, hintTextSectionForCurrentMission + hintLine);
			}
			return Translate.Instance.lookUpText((Enums.eTextSections)hintTextSectionForCurrentMission, 2 + hintLine);
		}
		return "";
	}

	public int GetNumHintsUnlocked()
	{
		return numHintsUnlockedForMission;
	}

	public void UnlockHint()
	{
		numHintsUnlockedForMission++;
	}
}
