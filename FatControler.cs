using System;
using System.Collections.Generic;
using System.IO;
using CodeStage.AdvancedFPSCounter;
using Noesis;
using Stronghold1DE;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FatControler : MonoBehaviour
{
	public static FatControler instance = null;

	private Camera m_MainCamera;

	private NoesisView NGview;

	public bool overUI;

	public bool overBuildingMenu;

	public bool mouseIsDown;

	public static bool mouseIsUpStroke = false;

	public static bool mouseIsDownStroke = false;

	public string lastUIHit = "None";

	public static int firstScene = -1;

	public static Enums.SceneIDS currentScene = (Enums.SceneIDS)0;

	public float SHLowerUIPoint;

	public int SHRadarRectSize;

	public float SHRadarScalar = 1f;

	private DateTime lastRadarMapMove = DateTime.MinValue;

	private DateTime binkPlayDelay = DateTime.MinValue;

	public Point SHMapStartPoint = new Point(0f, 0f);

	private Point NGMousePoint = new Point(0f, 0f);

	private Point LastNGMousePoint = new Point(0f, 0f);

	public Point BriefingHelpMousePoint = new Point(0f, 0f);

	private int setupFileList = 3;

	public bool binkPlayWait;

	private bool binkPaused;

	public int lastPopularity = 100;

	public static string locale;

	public static bool english = false;

	public static bool german = false;

	public static bool french = false;

	public static bool spanish = false;

	public static bool italian = false;

	public static bool polish = false;

	public static bool russian = false;

	public static bool portuguese = false;

	public static bool japanese = false;

	public static bool korean = false;

	public static bool simplified_chinese = false;

	public static bool traditional_chinese = false;

	public static bool czech = false;

	public static bool turkish = false;

	public static bool hungarian = false;

	public static bool thai = false;

	public static bool ukrainian = false;

	public static bool greek = false;

	private static SolidColorBrush BookColour_Green = new SolidColorBrush(Noesis.Color.FromArgb(byte.MaxValue, 0, 102, 0));

	private static SolidColorBrush BookColour_Red = new SolidColorBrush(Noesis.Color.FromArgb(byte.MaxValue, byte.MaxValue, 0, 0));

	private bool radarScrollTrigged;

	private bool temp_intro_speech_played;

	private DateTime DelayedSwitchToScene2Time = DateTime.MinValue;

	private int lastScreenWidth = -1;

	private int lastScreenHeight = -1;

	private bool screenModeSet;

	private FullScreenMode lastFullscreenMode = FullScreenMode.Windowed;

	private DateTime saveWindowSizeChange = DateTime.MinValue;

	private bool firstScreenChange = true;

	private bool noesisHasKeyboard = true;

	public static bool MouseIsUpStroke
	{
		get
		{
			return mouseIsUpStroke;
		}
		set
		{
			mouseIsUpStroke = value;
			if (value)
			{
				HUD_Briefing.mouseIsUpStroke = true;
				HUD_Help.mouseIsUpStroke = true;
			}
		}
	}

	public static bool MouseIsDownStroke
	{
		get
		{
			return mouseIsDownStroke;
		}
		set
		{
			mouseIsDownStroke = value;
			if (value)
			{
				HUD_Briefing.mouseIsDownStroke = true;
				HUD_Help.mouseIsDownStroke = true;
			}
		}
	}

	public bool NoesisHasKeyboard => noesisHasKeyboard;

	public string GetLocale(string filePath)
	{
		try
		{
			string[] array = File.ReadAllLines(filePath);
			if (array.Length != 0)
			{
				return array[0];
			}
		}
		catch (Exception)
		{
		}
		return "";
	}

	private void Awake()
	{
		locale = GetLocale("Assets/Text/Stronghold.txt").ToLowerInvariant();
		NoesisFont defaultFont;
		switch (locale)
		{
		case "zhcn":
			defaultFont = (NoesisFont)Resources.Load("NotoSansSC-Regular");
			break;
		case "zhhk":
			defaultFont = (NoesisFont)Resources.Load("NotoSansTC-Regular");
			break;
		case "jajp":
			defaultFont = (NoesisFont)Resources.Load("NotoSansJP-Regular");
			break;
		case "ruru":
		case "ukua":
			defaultFont = (NoesisFont)Resources.Load("NotoSerif-Regular");
			break;
		case "kokr":
			defaultFont = (NoesisFont)Resources.Load("NotoSansKR-Regular");
			break;
		case "thth":
			defaultFont = (NoesisFont)Resources.Load("NotoSansThaiLooped-Regular");
			break;
		default:
			defaultFont = (NoesisFont)Resources.Load("JunicodeTwoBeta-Medium");
			break;
		}
		switch (locale)
		{
		case "enus":
			english = true;
			break;
		case "dede":
			german = true;
			break;
		case "frfr":
			french = true;
			break;
		case "eses":
			spanish = true;
			break;
		case "itit":
			italian = true;
			break;
		case "plpl":
			polish = true;
			break;
		case "ruru":
			russian = true;
			break;
		case "ptbr":
			portuguese = true;
			break;
		case "jajp":
			japanese = true;
			break;
		case "kokr":
			korean = true;
			break;
		case "zhcn":
			simplified_chinese = true;
			break;
		case "zhhk":
			traditional_chinese = true;
			break;
		case "cscz":
			czech = true;
			break;
		case "trtr":
			turkish = true;
			break;
		case "huhu":
			hungarian = true;
			break;
		case "thth":
			thai = true;
			break;
		case "ukua":
			ukrainian = true;
			break;
		case "elgr":
			greek = true;
			break;
		}
		NoesisSettings.Get().defaultFont = defaultFont;
		Application.runInBackground = true;
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		MainViewModel.setupImages();
	}

	private void Start()
	{
		ConfigSettings.LoadSettings();
		SFXManager.InitSoundFX();
		AchievementsCommon.Instance.Init();
		MinimumWindowSize.Set(1280, 768);
		EngineInterface.sendPath(Application.streamingAssetsPath, ConfigSettings.GetMpAutoSavePath(), ConfigSettings.GetSavesPath());
		Director.instance.SetEngineFrameRate(ConfigSettings.Settings_GameSpeed);
		SceneManager.LoadSceneAsync("SampleScene");
	}

	private void Update()
	{
		if (setupFileList > 0)
		{
			if (MainViewModel.viewModelLoaded)
			{
				setupFileList--;
			}
			if (setupFileList == 2)
			{
				MapFileManager.Instance.BuildFileList();
			}
			if (setupFileList == 0)
			{
				Director.instance.setCursor(0, force: true);
				if (Platform_Multiplayer.Instance.PendingMPLobby && Platform_Multiplayer.Instance.PendingMPLobby_DelayedMPEnter)
				{
					Platform_Multiplayer.Instance.PendingMPLobby_DelayedMPEnter = false;
					MainViewModel.Instance.FrontEndMenu.ButtonClicked("Multiplayer");
				}
			}
		}
		MonitorScreenResolutions();
		mouseIsDown = Input.GetMouseButton(0);
		if (GameData.Instance.lastGameState != null && GameData.Instance.lastGameState.app_mode == 14 && MainViewModel.Instance.Show_HUD_Briefing)
		{
			MainViewModel.Instance.HUDBriefingPanel.Update();
		}
		if (MainViewModel.viewModelLoaded)
		{
			if (MainViewModel.Instance.Show_HUD_Help)
			{
				MainViewModel.Instance.HUDHelp.Update();
			}
			if (MainViewModel.Instance.Show_HUD_RightClick)
			{
				MainViewModel.Instance.HUDRightClick.Update();
			}
			if (MainViewModel.Instance.Show_HUD_ControlGroups)
			{
				MainViewModel.Instance.HUDControlGroups.Update();
			}
			if (MainViewModel.Instance.Show_HUD_MissionOver)
			{
				MainViewModel.Instance.HUDMissionOver.Update();
			}
			if (MainViewModel.Instance.Show_HUD_LoadSaveRequester || MainViewModel.Instance.Show_HUD_LoadSaveRequesterMP)
			{
				MainViewModel.Instance.HUDLoadSaveRequester.Update();
			}
			if (MainViewModel.Instance.Show_StandaloneSetup)
			{
				MainViewModel.Instance.FRONTStandaloneMission.Update();
			}
			if (MainViewModel.Instance.Show_MultiplayerSetup)
			{
				MainViewModel.Instance.FRONTMultiplayer.Update();
			}
			if (MainViewModel.Instance.Show_HUD_Scenario)
			{
				MainViewModel.Instance.HUDScenarioPopup.Update();
			}
			if (MainViewModel.Instance.Show_HUD_FreebuildMenu)
			{
				MainViewModel.Instance.HUDFreebuildMenu.Update();
			}
			if (MainViewModel.Instance.Show_Credits)
			{
				MainViewModel.Instance.FRONTCredits.Update();
			}
			if (MainViewModel.Instance.Show_IntroSequence)
			{
				MainViewModel.Instance.Intro_Sequence.Update();
			}
			if (MainViewModel.Instance.Show_CampaignMenu)
			{
				MainViewModel.Instance.STORYMap.Update();
			}
			if (MainViewModel.Instance.Show_Extra1CampaignMenu)
			{
				FRONT_Extra1Campaign.Update();
			}
			if (MainViewModel.Instance.Show_Extra2CampaignMenu)
			{
				FRONT_Extra2Campaign.Update();
			}
			if (MainViewModel.Instance.Show_Extra3CampaignMenu)
			{
				FRONT_Extra3Campaign.Update();
			}
			if (MainViewModel.Instance.Show_Extra4CampaignMenu)
			{
				FRONT_Extra4Campaign.Update();
			}
			if (MainViewModel.Instance.Show_ExtraEcoCampaignMenu)
			{
				FRONT_ExtraEcoCampaign.Update();
			}
			if (MainViewModel.Instance.Show_EcoCampaignMenu)
			{
				FRONT_EcoCampaign.Update();
			}
			if (MainViewModel.Instance.Show_TrailCampaignMenu)
			{
				FRONT_Trail.Update();
			}
			if (MainViewModel.Instance.Show_Trail2CampaignMenu)
			{
				FRONT_Trail2.Update();
			}
			MainViewModel.Instance.CrossThreadRolloverUpdate();
			OnScreenText.Instance.Update();
			if (Director.instance.SimRunning)
			{
				SFXManager.instance.Update();
			}
		}
		RadarScrollMap();
	}

	public void NoesisGUIUpdateChecksComplete()
	{
		if (MainViewModel.viewModelLoaded && MainViewModel.Instance.Show_HUD_Options && MainViewModel.Instance.HUDOptions != null)
		{
			MainViewModel.Instance.HUDOptions.Update();
		}
	}

	public void NoesisGUIUpdateChecksInGame()
	{
		int num = 0;
		bool isEnabled = true;
		string line = "";
		string line2 = "";
		double buildingTitleFontSize = 32.0;
		EngineInterface.ScenarioOverview scenarioOverview = null;
		MainViewModel.Instance.IngameUI.findUIlowerPoint();
		if (GameData.Instance.lastGameState == null)
		{
			MouseIsUpStroke = false;
			MouseIsDownStroke = false;
			return;
		}
		MainViewModel.Instance.UpdateSHGoodsData();
		MainViewModel.Instance.UpdateSHTroopsData();
		MainViewModel.Instance.AvailablePeasantText = GameData.Instance.lastGameState.peasants_available_for_troops.ToString();
		MainViewModel.Instance.BarracksHorsesAvailableText = GameData.Instance.lastGameState.total_horses_available.ToString();
		int popularity = GameData.Instance.lastGameState.popularity;
		MainViewModel.Instance.BookPopularityText = popularity.ToString();
		MainViewModel.Instance.BookGoldText = GameData.Instance.lastGameState.gold.ToString();
		int num2 = GameData.Instance.lastGameState.population;
		if (GameData.Instance.lastGameState.housing_cap == 0)
		{
			num2 = 0;
		}
		MainViewModel.Instance.BookPopulationText = num2 + "/" + GameData.Instance.lastGameState.housing_cap;
		if (GameData.Instance.lastGameState.overcrowding_popularity == 0)
		{
			MainViewModel.Instance.BookPopulationColour = BookColour_Green;
		}
		else
		{
			MainViewModel.Instance.BookPopulationColour = BookColour_Red;
		}
		if (popularity >= 50)
		{
			MainViewModel.Instance.BookPopularityColour = BookColour_Green;
			MainViewModel.Instance.HUDRoot.setPulsing(0);
		}
		else
		{
			MainViewModel.Instance.BookPopularityColour = BookColour_Red;
			if (popularity <= 20)
			{
				MainViewModel.Instance.HUDRoot.setPulsing(1000);
			}
			else if (popularity <= 30 && lastPopularity > 30)
			{
				MainViewModel.Instance.HUDRoot.setPulsing(3);
			}
			else if (popularity <= 40 && lastPopularity > 40)
			{
				MainViewModel.Instance.HUDRoot.setPulsing(3);
			}
			else if (popularity <= 49 && lastPopularity > 49)
			{
				MainViewModel.Instance.HUDRoot.setPulsing(3);
			}
			else
			{
				MainViewModel.Instance.HUDRoot.setPulsing(-1);
			}
		}
		lastPopularity = popularity;
		if (GameData.Instance.lastGameState.upcoming_total_popularity > 0)
		{
			MainViewModel.Instance.PopularityIncreasingVis = true;
			MainViewModel.Instance.PopularityDecreasingVis = false;
		}
		else if (GameData.Instance.lastGameState.upcoming_total_popularity < 0)
		{
			MainViewModel.Instance.PopularityIncreasingVis = false;
			MainViewModel.Instance.PopularityDecreasingVis = true;
		}
		else
		{
			MainViewModel.Instance.PopularityIncreasingVis = false;
			MainViewModel.Instance.PopularityDecreasingVis = false;
		}
		if (GameData.Instance.lastGameState.gold > 9999)
		{
			MainViewModel.Instance.BookGoldLarge = false;
			MainViewModel.Instance.BookGoldSmall = true;
		}
		else
		{
			MainViewModel.Instance.BookGoldLarge = true;
			MainViewModel.Instance.BookGoldSmall = false;
		}
		MainViewModel.Instance.GotMarketVis = (GameData.Instance.lastGameState.gotMarket & 1) > 0;
		MainViewModel.Instance.Show_HUD_Goods_Button_Disabled = (GameData.Instance.lastGameState.gotMarket & 2) == 0;
		MainViewModel.Instance.HUDmain.SetEnginePanelText(GameData.Instance.lastGameState.panel_text_group, GameData.Instance.lastGameState.panel_text_text);
		bool flag = false;
		if (GameData.Instance.lastGameState.undoAvailable > 0)
		{
			flag = ((GameData.Instance.lastGameState.app_mode != 14 || GameData.Instance.lastGameState.app_sub_mode != 49) ? true : false);
		}
		if (MainViewModel.Instance.HUDmain.RefGameUndoButton.IsEnabled != flag)
		{
			MainViewModel.Instance.HUDmain.RefGameUndoButton.IsEnabled = flag;
		}
		if (MainViewModel.Instance.HUDmain != null)
		{
			MainViewModel.Instance.HUDmain.UpdateRollover();
		}
		if (GameData.Instance.lastGameState.peasants_available_for_troops <= 0)
		{
			isEnabled = false;
		}
		if (addChimpActions(GameData.Instance.lastGameState, ref line, ref line2))
		{
			MainViewModel.Instance.BuildingLine1Text = line;
			MainViewModel.Instance.BuildingLine2Text = line2;
		}
		if (GameData.Instance.lastGameState.building_maxhps_for_repair > 0)
		{
			MainViewModel.Instance.BuildingHPText = GameData.Instance.lastGameState.building_hps_for_repair + "/" + GameData.Instance.lastGameState.building_maxhps_for_repair;
			MainViewModel.Instance.BuildingRepairHPWidth = 60 * GameData.Instance.lastGameState.building_hps_for_repair / GameData.Instance.lastGameState.building_maxhps_for_repair;
			MainViewModel.Instance.HUDBuildingPanel.RefButtonRepair.IsEnabled = GameData.Instance.lastGameState.building_hps_for_repair != GameData.Instance.lastGameState.building_maxhps_for_repair;
		}
		if (ConfigSettings.Settings_Scribe == 0)
		{
			if (!MainViewModel.Instance.IsMapEditorMode)
			{
				MainViewModel.Instance.ScribeHeadImage = MainViewModel.Instance.GameSprites[296 + GameData.Instance.lastGameState.scribe_frame - 1];
			}
			else
			{
				MainViewModel.Instance.ScribeHeadImage = MainViewModel.Instance.GameSprites[296];
			}
		}
		else if (ConfigSettings.Settings_Scribe == 2)
		{
			if (!MainViewModel.Instance.IsMapEditorMode)
			{
				MainViewModel.Instance.ScribeHeadImage = MainViewModel.Instance.GameSprites[24 + GameData.Instance.lastGameState.scribe_frame - 1];
			}
			else
			{
				MainViewModel.Instance.ScribeHeadImage = MainViewModel.Instance.GameSprites[24];
			}
		}
		if (MainViewModel.Instance.Show_HUD_Objectives && !MainViewModel.Instance.Show_HUD_Briefing && GameData.scenario != null)
		{
			List<GameData.ScenarioEvent> events = GameData.scenario.getEvents();
			int startDate = 0;
			int nowDate = 0;
			int endDate = 0;
			int num3 = 0;
			string winTimer = GameData.scenario.getWinTimer(ref startDate, ref nowDate, ref endDate);
			if (winTimer == null)
			{
				MainViewModel.Instance.HUDObjectives.RefObjectiveTimer.Visibility = Visibility.Collapsed;
			}
			else
			{
				MainViewModel.Instance.ObjectiveTimerText = winTimer;
				MainViewModel.Instance.HUDObjectives.RefObjectiveTimer.Visibility = Visibility.Visible;
				long num4 = nowDate - startDate;
				long num5 = endDate - startDate;
				if (num4 > num5)
				{
					num4 = num5;
				}
				if (num5 > 0)
				{
					MainViewModel.Instance.ObjectiveTimerWidth = (int)(200 * num4 / num5);
				}
				num3 += 2;
			}
			num3 += UpdateObjectiveRows(events, MainViewModel.Instance.HUDObjectives.RefWGTObjectives);
			MainViewModel.Instance.HUDObjectives.SetSizeFromRows(num3);
		}
		if (MainViewModel.Instance.ScenarioEditorMode > Enums.ScenarioViews.Blank && GameData.Instance.scenarioOverview != null)
		{
			scenarioOverview = GameData.Instance.scenarioOverview;
			MainViewModel.Instance.ScenarioStartingMonthText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MONTHS, scenarioOverview.startMonth);
			MainViewModel.Instance.ScenarioStartingPopText = scenarioOverview.scenario_start_popularity.ToString();
			int num6 = int.Parse(MainViewModel.Instance.ScenarioStartingYearText, Director.defaultCulture);
			if (num6 != scenarioOverview.startYear)
			{
				scenarioOverview.startYear = num6;
				EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Starting_Year, 0, scenarioOverview.startYear);
			}
			MainViewModel.Instance.SetStartingSpecial(scenarioOverview.special_start > 0);
			MainViewModel.Instance.ScenarioStartingSpecialGoldText = scenarioOverview.special_start_gold.ToString();
			string scenarioStartingSpecialRationsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GRANARY, 3);
			switch (scenarioOverview.special_start_rationing)
			{
			case 1:
				scenarioStartingSpecialRationsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GRANARY, 4);
				break;
			case 2:
				scenarioStartingSpecialRationsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GRANARY, 5);
				break;
			case 3:
				scenarioStartingSpecialRationsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GRANARY, 6);
				break;
			case 4:
				scenarioStartingSpecialRationsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GRANARY, 12);
				break;
			}
			MainViewModel.Instance.ScenarioStartingSpecialRationsText = scenarioStartingSpecialRationsText;
			MainViewModel.Instance.ScenarioStartingSpecialTaxText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_KEEP, scenarioOverview.special_start_tax_rate + 7);
			MainViewModel.Instance.ScenarioStartingGoldText = scenarioOverview.scenario_start_goods[15].ToString();
			MainViewModel.Instance.ScenarioStartingPitchText = scenarioOverview.scenario_start_goods[8].ToString();
			ScenarioEditorUpdateNewEventButtons();
			if (MainViewModel.Instance.ScenarioEditorMode != Enums.ScenarioViews.Main)
			{
				if (MainViewModel.Instance.ScenarioEditorMode == Enums.ScenarioViews.StartingGoods)
				{
					MainViewModel.Instance.ScenarioAdjustedStartingGoodsText = GameData.Instance.scenarioOverview.scenario_start_goods[MainViewModel.Instance.ScenarioStartingGoodsType] + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, MainViewModel.Instance.ScenarioStartingGoodsType);
					for (int i = 0; i <= 24; i++)
					{
						MainViewModel.Instance.StartingGoods[i] = GameData.Instance.scenarioOverview.scenario_start_goods[i];
					}
				}
				else if (MainViewModel.Instance.ScenarioEditorMode == Enums.ScenarioViews.AttackingForce)
				{
					if (MainViewModel.Instance.ScenarioAttackingForceType < 100)
					{
						int attackingForceTroopTypeFromIndex = GameData.getAttackingForceTroopTypeFromIndex(MainViewModel.Instance.ScenarioAttackingForceType);
						GameData.getMaxTroopsForAttackingForce((Enums.eChimps)attackingForceTroopTypeFromIndex);
						MainViewModel.Instance.ScenarioAdjustedAttackingForcesText = GameData.Instance.scenarioOverview.scenario_start_troops[MainViewModel.Instance.ScenarioAttackingForceType] + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, attackingForceTroopTypeFromIndex);
					}
					else
					{
						int num7 = MainViewModel.Instance.ScenarioAttackingForceType - 100;
						int attackingForceTroopTypeFromIndex2 = GameData.getAttackingForceTroopTypeFromIndex(MainViewModel.Instance.ScenarioAttackingForceType);
						GameData.getMaxTroopsForAttackingForce((Enums.eChimps)attackingForceTroopTypeFromIndex2);
						MainViewModel.Instance.ScenarioAdjustedAttackingForcesText = GameData.Instance.scenarioOverview.scenario_start_siege_equipment[num7] + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, attackingForceTroopTypeFromIndex2);
					}
					for (int j = 0; j < 10; j++)
					{
						MainViewModel.Instance.AttackingForces[j] = GameData.Instance.scenarioOverview.scenario_start_troops[j];
					}
					for (int k = 0; k < 6; k++)
					{
						MainViewModel.Instance.AttackingForcesSiege[k] = GameData.Instance.scenarioOverview.scenario_start_siege_equipment[k];
					}
				}
				else if (MainViewModel.Instance.ScenarioEditorMode == Enums.ScenarioViews.TradedGoods)
				{
					string text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_ON);
					string text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_OFF);
					for (int l = 0; l <= 24; l++)
					{
						if (GameData.Instance.scenarioOverview.scenario_trader_goods_available[l] > 0)
						{
							if (MainViewModel.Instance.TradingGoods[l] != text)
							{
								MainViewModel.Instance.TradingGoods[l] = text;
								MainViewModel.Instance.TradingGoodsBool[l] = true;
							}
						}
						else if (MainViewModel.Instance.TradingGoods[l] != text2)
						{
							MainViewModel.Instance.TradingGoods[l] = text2;
							MainViewModel.Instance.TradingGoodsBool[l] = false;
						}
					}
				}
				else if (MainViewModel.Instance.ScenarioEditorMode == Enums.ScenarioViews.Messages)
				{
					MainViewModel.Instance.HUDScenario.PopulateMessage(fromUpdate: true);
				}
				else if (MainViewModel.Instance.ScenarioEditorMode == Enums.ScenarioViews.Invasions)
				{
					MainViewModel.Instance.HUDScenario.PopulateInvasion(fromUpdate: true);
				}
				else if (MainViewModel.Instance.ScenarioEditorMode == Enums.ScenarioViews.Events)
				{
					MainViewModel.Instance.HUDScenario.PopulateEvent(fromUpdate: true);
				}
				else if (MainViewModel.Instance.ScenarioEditorMode == Enums.ScenarioViews.EventsActions)
				{
					MainViewModel.Instance.HUDScenario.PopulateEventActions(fromUpdate: true);
				}
				else if (MainViewModel.Instance.ScenarioEditorMode == Enums.ScenarioViews.EventsConditions)
				{
					MainViewModel.Instance.HUDScenario.PopulateEventConditions(fromUpdate: true);
				}
			}
		}
		if (GameData.Instance.lastGameState.app_mode == 14 && MainViewModel.Instance.Show_HUD_Briefing)
		{
			BriefingUIUpdate();
		}
		if (!MainViewModel.Instance.Show_HUD_Briefing)
		{
			if (SFXManager.instance.requestBinkPlayState < 0 && SFXManager.instance.binkIsPlaying)
			{
				int requestBinkPlayState = -SFXManager.instance.requestBinkPlayState;
				MainViewModel.Instance.HUDRoot.RadarME_Ended();
				MainViewModel.Instance.HUDRoot.RefRadarME.Volume = ConfigSettings.Settings_SFXVolume * ConfigSettings.Settings_MasterVolume * SFXManager.instance.binkVolume;
				MainViewModel.Instance.HUDRoot.RefRadarME.Source = SFXManager.instance.requestBinkPlaybackURI;
				binkPlayWait = true;
				binkPlayDelay = DateTime.UtcNow;
				SFXManager.instance.requestBinkPlayState = requestBinkPlayState;
			}
			else if (SFXManager.instance.requestBinkPlayState > 0 && !SFXManager.instance.binkIsPlaying)
			{
				if (!binkPlayWait)
				{
					if (MainViewModel.Instance.HUDRoot.RefRadarME.Source == SFXManager.instance.requestBinkPlaybackURI)
					{
						MainViewModel.Instance.HUDRoot.RefRadarME.Volume = ConfigSettings.Settings_SFXVolume * ConfigSettings.Settings_MasterVolume * SFXManager.instance.binkVolume;
						MainViewModel.Instance.HUDRoot.RefRadarME.Play();
						MainViewModel.Instance.HUDRoot.RefRadarME.Opacity = 1f;
						SFXManager.instance.binkIsPlaying = true;
						binkPaused = false;
					}
					else
					{
						MainViewModel.Instance.HUDRoot.RefRadarME.Volume = ConfigSettings.Settings_SFXVolume * ConfigSettings.Settings_MasterVolume * SFXManager.instance.binkVolume;
						MainViewModel.Instance.HUDRoot.RefRadarME.Source = SFXManager.instance.requestBinkPlaybackURI;
						binkPlayWait = true;
						binkPlayDelay = DateTime.UtcNow;
					}
				}
				else if ((DateTime.UtcNow - binkPlayDelay).TotalMilliseconds > 200.0)
				{
					binkPlayWait = false;
					MainViewModel.Instance.HUDRoot.RefRadarME.Play();
					MainViewModel.Instance.HUDRoot.RefRadarME.Volume = ConfigSettings.Settings_SFXVolume * ConfigSettings.Settings_MasterVolume * SFXManager.instance.binkVolume;
					MainViewModel.Instance.HUDRoot.RefRadarME.Opacity = 1f;
					SFXManager.instance.binkIsPlaying = true;
					binkPaused = false;
				}
			}
			else if (SFXManager.instance.requestBinkPlayState == 0 && SFXManager.instance.binkIsPlaying)
			{
				MainViewModel.Instance.HUDRoot.RadarME_Ended();
			}
			else if (SFXManager.instance.requestBinkPlayState == 3 && SFXManager.instance.binkIsPlaying && !MyAudioManager.Instance.isSpeechPlaying(1))
			{
				MainViewModel.Instance.HUDRoot.RadarME_Ended();
			}
		}
		if (SFXManager.instance.binkIsPlaying)
		{
			if (MainViewModel.Instance.Show_HUD_Briefing)
			{
				if (!binkPaused)
				{
					MainViewModel.Instance.HUDRoot.RefRadarME.Pause();
					binkPaused = true;
				}
			}
			else if (binkPaused)
			{
				MainViewModel.Instance.HUDRoot.RefRadarME.Play();
				binkPaused = false;
			}
		}
		bool show_ActionPoint = false;
		bool show_KeepEnclosed = false;
		if (MainViewModel.Instance.UIVisible && !MainViewModel.Instance.IsMapEditorMode && Director.instance.SimRunning)
		{
			if (GameData.Instance.lastGameState.action_point_count > 0)
			{
				show_ActionPoint = true;
			}
			show_KeepEnclosed = GameData.Instance.lastGameState.keep_enclosed > 0;
		}
		MainViewModel.Instance.Show_ActionPoint = show_ActionPoint;
		MainViewModel.Instance.Show_KeepEnclosed = show_KeepEnclosed;
		if (GameData.Instance.lastGameState.app_mode == 14 && MainViewModel.Instance.HUDmain != null)
		{
			Enums.ForcedAppModes force_app_mode = (Enums.ForcedAppModes)GameData.Instance.lastGameState.force_app_mode;
			GameData.Instance.lastGameState.force_app_mode = 0;
			if ((GameData.Instance.game_type == 2 || GameData.Instance.game_type == 11 || GameData.Instance.game_type == 13) && GameData.Instance.mapType == Enums.GameModes.SIEGE && GameData.Instance.playerID == 2)
			{
				if ((GameData.Instance.app_sub_mode != 20 && GameData.Instance.app_sub_mode != 48 && GameData.Instance.app_sub_mode != 61) || force_app_mode == Enums.ForcedAppModes.refresh_current || !MainViewModel.Instance.FreezeMainControls)
				{
					MainViewModel.Instance.buildControlsFreeze(Mode: true);
					MainViewModel.Instance.HUDmain.NewBuildScreenBlank();
				}
			}
			else if (((GameData.Instance.game_type == 0 || GameData.Instance.game_type == 7 || GameData.Instance.game_type == 8 || GameData.Instance.game_type == 9 || GameData.Instance.game_type == 10) && GameData.Instance.app_sub_mode == 48) || force_app_mode == Enums.ForcedAppModes.blank)
			{
				MainViewModel.Instance.buildControlsFreeze(Mode: true);
				MainViewModel.Instance.HUDmain.NewBuildScreenBlank();
			}
			else
			{
				switch (force_app_mode)
				{
				case Enums.ForcedAppModes.keeps:
					MainViewModel.Instance.buildControlsFreeze(Mode: true);
					MainViewModel.Instance.HUDmain.NewBuildScreenKeeps();
					break;
				case Enums.ForcedAppModes.granary:
					MainViewModel.Instance.buildControlsFreeze(Mode: true);
					MainViewModel.Instance.HUDmain.NewBuildScreenFood(updateAppMode: false);
					break;
				case Enums.ForcedAppModes.refresh_current:
					MainViewModel.Instance.buildControlsFreeze(Mode: false);
					if (GameData.Instance.game_type == 4 || GameData.Instance.game_type == 1)
					{
						MainViewModel.Instance.HUDmain.NewBuildScreenCastle(force: true);
						MainViewModel.Instance.HUDmain.RefTabBuildCastle.IsChecked = true;
					}
					else if (GameData.Instance.game_type == 6)
					{
						MainViewModel.Instance.HUDmain.NewBuildScreenKeeps();
						MainViewModel.Instance.HUDmain.RefTabBuildCastle.IsChecked = true;
					}
					else
					{
						MainViewModel.Instance.HUDmain.NewBuildScreenIndustry(force: true);
						MainViewModel.Instance.HUDmain.RefTabBuildIndustry.IsChecked = true;
					}
					break;
				case Enums.ForcedAppModes.castle:
					MainViewModel.Instance.buildControlsFreeze(Mode: false);
					if (GameData.Instance.lastGameState.app_sub_mode != 61 && GameData.Instance.lastGameState.app_sub_mode != 62)
					{
						if (GameData.Instance.game_type == 4 || GameData.Instance.game_type == 6)
						{
							MainViewModel.Instance.HUDmain.NewBuildScreenCastle();
							MainViewModel.Instance.HUDmain.RefTabBuildCastle.IsChecked = true;
						}
						else
						{
							MainViewModel.Instance.HUDmain.NewBuildScreenIndustry();
							MainViewModel.Instance.HUDmain.RefTabBuildIndustry.IsChecked = true;
						}
					}
					break;
				}
			}
		}
		if (GameData.Instance.lastGameState.app_mode != 16 && GameData.Instance.lastGameState.app_sub_mode != 57 && MainViewModel.Instance.HUDBuildingPanel.RefTradePost_Trade_Auto.Visibility == Visibility.Visible)
		{
			EngineInterface.GameAction(Enums.GameActionCommand.Autotrade_Apply, 0, 0);
			EngineInterface.GameAction(Enums.GameActionCommand.Autotrade_Pause, 0, 0);
			MainViewModel.Instance.HUDBuildingPanel.RefTradePost_Trade_Normal.Visibility = Visibility.Visible;
			MainViewModel.Instance.HUDBuildingPanel.RefTradePost_Trade_Auto.Visibility = Visibility.Hidden;
		}
		if (GameData.Instance.lastGameState.app_mode == 14 && (GameData.Instance.lastGameState.app_sub_mode == 61 || GameData.Instance.lastGameState.app_sub_mode == 62))
		{
			switch (GameData.Instance.lastGameState.troops_show_stance)
			{
			case 0:
				MainViewModel.Instance.GuardStanceActive = true;
				MainViewModel.Instance.DefensiveStanceActive = false;
				MainViewModel.Instance.AggressiveStanceActive = false;
				break;
			case 1:
				MainViewModel.Instance.DefensiveStanceActive = true;
				MainViewModel.Instance.GuardStanceActive = false;
				MainViewModel.Instance.AggressiveStanceActive = false;
				break;
			case 2:
				MainViewModel.Instance.AggressiveStanceActive = true;
				MainViewModel.Instance.GuardStanceActive = false;
				MainViewModel.Instance.DefensiveStanceActive = false;
				break;
			}
			MainViewModel.Instance.HUDTroopPanel.ShowAmmoOrders();
			if (MainViewModel.Instance.HUDTroopPanel.PatrolShouldBeVisible)
			{
				if (GameData.Instance.lastGameState.troops_patrol_mode != 0)
				{
					MainViewModel.Instance.HUDTroopPanel.RefUnitPatrol.Visibility = Visibility.Hidden;
					MainViewModel.Instance.HUDTroopPanel.RefUnitPatrolActive.Visibility = Visibility.Visible;
				}
				else
				{
					MainViewModel.Instance.HUDTroopPanel.RefUnitPatrol.Visibility = Visibility.Visible;
					MainViewModel.Instance.HUDTroopPanel.RefUnitPatrolActive.Visibility = Visibility.Hidden;
				}
			}
		}
		if (GameData.Instance.lastGameState.app_mode == 16)
		{
			if (GameData.Instance.lastGameState.app_sub_mode == 1 || GameData.Instance.lastGameState.app_sub_mode == 23 || GameData.Instance.lastGameState.app_sub_mode == 24)
			{
				int num8 = GameData.Instance.lastGameState.resources[15];
				int num9 = 1;
				if (MainViewModel.Instance.lastTroopBuildOver.Length > 0 && MainViewModel.Instance.lastTroopBuildChimp != Enums.eChimps.CHIMP_NUM_TYPES)
				{
					num9 = 1000;
					if (num9 > 1)
					{
						if (num9 > GameData.Instance.lastGameState.peasants_available_for_troops)
						{
							num9 = GameData.Instance.lastGameState.peasants_available_for_troops;
						}
						int chimpGoldCost = GameData.getChimpGoldCost((int)MainViewModel.Instance.lastTroopBuildChimp);
						if (chimpGoldCost > 0)
						{
							int num10 = num8 / chimpGoldCost;
							if (num10 < num9)
							{
								num9 = num10;
							}
						}
						if (num9 > 1)
						{
							int num11 = 10000;
							int num12 = 10000;
							int num13 = 10000;
							switch (MainViewModel.Instance.lastTroopBuildChimp)
							{
							case Enums.eChimps.CHIMP_TYPE_ARCHER:
								num11 = GameData.Instance.lastGameState.resources[17];
								break;
							case Enums.eChimps.CHIMP_TYPE_SPEARMAN:
								num11 = GameData.Instance.lastGameState.resources[19];
								break;
							case Enums.eChimps.CHIMP_TYPE_MACEMAN:
								num11 = GameData.Instance.lastGameState.resources[21];
								num12 = GameData.Instance.lastGameState.resources[23];
								break;
							case Enums.eChimps.CHIMP_TYPE_XBOWMAN:
								num11 = GameData.Instance.lastGameState.resources[18];
								num12 = GameData.Instance.lastGameState.resources[23];
								break;
							case Enums.eChimps.CHIMP_TYPE_PIKEMAN:
								num11 = GameData.Instance.lastGameState.resources[20];
								break;
							case Enums.eChimps.CHIMP_TYPE_SWORDSMAN:
								num11 = GameData.Instance.lastGameState.resources[22];
								num12 = GameData.Instance.lastGameState.resources[24];
								break;
							case Enums.eChimps.CHIMP_TYPE_KNIGHT:
								num11 = GameData.Instance.lastGameState.resources[22];
								num12 = GameData.Instance.lastGameState.resources[24];
								num13 = GameData.Instance.lastGameState.total_horses_available;
								break;
							}
							if (num11 < num9)
							{
								num9 = num11;
							}
							if (num12 < num9)
							{
								num9 = num12;
							}
							if (num13 < num9)
							{
								num9 = num13;
							}
						}
					}
					MainViewModel.Instance.lastTroopsAmountToMakeMax = num9;
					MainViewModel.Instance.lastTroopsAmountToMakex5 = Math.Min(num9, 5);
					if (KeyManager.instance.isShiftDown())
					{
						num9 = Math.Min(num9, 5);
					}
					else if (!KeyManager.instance.isCtrlDown())
					{
						num9 = 1;
					}
					MainViewModel.Instance.lastTroopsAmountToMake = num9;
					MainViewModel.Instance.ButtonEnterCreateTroop(MainViewModel.Instance.lastTroopBuildOver);
					if (num9 <= 0)
					{
						num9 = 1;
					}
				}
				MainViewModel.Instance.HUDBuildingPanel.RefRecruitArcherButton.IsEnabled = isEnabled;
				MainViewModel.Instance.HUDBuildingPanel.RefRecruitSpearmanButton.IsEnabled = isEnabled;
				MainViewModel.Instance.HUDBuildingPanel.RefRecruitMacemanButton.IsEnabled = isEnabled;
				MainViewModel.Instance.HUDBuildingPanel.RefRecruitXBowmanButton.IsEnabled = isEnabled;
				MainViewModel.Instance.HUDBuildingPanel.RefRecruitPikemanButton.IsEnabled = isEnabled;
				MainViewModel.Instance.HUDBuildingPanel.RefRecruitSwordsmanButton.IsEnabled = isEnabled;
				MainViewModel.Instance.HUDBuildingPanel.RefRecruitKnightButton.IsEnabled = isEnabled;
				MainViewModel.Instance.HUDBuildingPanel.RefRecruitEngineerButton.IsEnabled = isEnabled;
				MainViewModel.Instance.HUDBuildingPanel.RefRecruitLaddermanButton.IsEnabled = isEnabled;
				MainViewModel.Instance.HUDBuildingPanel.RefRecruitTunellerButton.IsEnabled = isEnabled;
				if (GameData.getChimpGoldCost(22) * num9 > num8)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitArcherButton.IsEnabled = false;
				}
				if (GameData.getChimpGoldCost(24) * num9 > num8)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitSpearmanButton.IsEnabled = false;
				}
				if (GameData.getChimpGoldCost(26) * num9 > num8)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitMacemanButton.IsEnabled = false;
				}
				if (GameData.getChimpGoldCost(23) * num9 > num8)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitXBowmanButton.IsEnabled = false;
				}
				if (GameData.getChimpGoldCost(25) * num9 > num8)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitPikemanButton.IsEnabled = false;
				}
				if (GameData.getChimpGoldCost(27) * num9 > num8)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitSwordsmanButton.IsEnabled = false;
				}
				if (GameData.getChimpGoldCost(28) * num9 > num8)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitKnightButton.IsEnabled = false;
				}
				if (GameData.getChimpGoldCost(30) * num9 > num8)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitEngineerButton.IsEnabled = false;
				}
				if (GameData.getChimpGoldCost(29) * num9 > num8)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitLaddermanButton.IsEnabled = false;
				}
				if (GameData.getChimpGoldCost(5) * num9 > num8)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitTunellerButton.IsEnabled = false;
				}
				if (GameData.getChimpGoldCost(30) * num9 <= num8 || GameData.getChimpGoldCost(29) * num9 <= num8)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitEngineerButton.Visibility = Visibility.Visible;
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitLaddermanButton.Visibility = Visibility.Visible;
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitEngineerButtonX.Visibility = Visibility.Visible;
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitLaddermanButtonX.Visibility = Visibility.Visible;
					MainViewModel.Instance.HUDBuildingPanel.RefEngineersGuildNoGoldMessage.Visibility = Visibility.Hidden;
				}
				else
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitEngineerButton.Visibility = Visibility.Hidden;
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitLaddermanButton.Visibility = Visibility.Hidden;
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitEngineerButtonX.Visibility = Visibility.Hidden;
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitLaddermanButtonX.Visibility = Visibility.Hidden;
					MainViewModel.Instance.HUDBuildingPanel.RefEngineersGuildNoGoldMessage.Visibility = Visibility.Visible;
				}
				if (GameData.getChimpGoldCost(5) * num9 <= num8)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitTunellerButton.Visibility = Visibility.Visible;
					MainViewModel.Instance.HUDBuildingPanel.RefTunnllersGuildNoGoldMessage.Visibility = Visibility.Hidden;
				}
				else
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitTunellerButton.Visibility = Visibility.Hidden;
					MainViewModel.Instance.HUDBuildingPanel.RefTunnllersGuildNoGoldMessage.Visibility = Visibility.Visible;
				}
				if (GameData.Instance.lastGameState.resources[17] < num9)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitArcherButton.IsEnabled = false;
				}
				if (GameData.Instance.lastGameState.resources[19] < num9)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitSpearmanButton.IsEnabled = false;
				}
				if (GameData.Instance.lastGameState.resources[21] < num9)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitMacemanButton.IsEnabled = false;
				}
				if (GameData.Instance.lastGameState.resources[18] < num9)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitXBowmanButton.IsEnabled = false;
				}
				if (GameData.Instance.lastGameState.resources[20] < num9)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitPikemanButton.IsEnabled = false;
				}
				if (GameData.Instance.lastGameState.resources[22] < num9)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitSwordsmanButton.IsEnabled = false;
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitKnightButton.IsEnabled = false;
				}
				if (GameData.Instance.lastGameState.resources[24] < num9)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitSwordsmanButton.IsEnabled = false;
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitKnightButton.IsEnabled = false;
				}
				if (GameData.Instance.lastGameState.resources[23] < num9)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitMacemanButton.IsEnabled = false;
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitXBowmanButton.IsEnabled = false;
				}
				if (GameData.Instance.lastGameState.total_horses_available < num9)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefRecruitKnightButton.IsEnabled = false;
				}
				MainViewModel.Instance.Show_BarracksArcher = GameData.Instance.lastGameState.troop_types_available[0] > 0;
				MainViewModel.Instance.Show_BarracksXbowman = GameData.Instance.lastGameState.troop_types_available[1] > 0;
				MainViewModel.Instance.Show_BarracksSpearman = GameData.Instance.lastGameState.troop_types_available[2] > 0;
				MainViewModel.Instance.Show_BarracksPikeman = GameData.Instance.lastGameState.troop_types_available[3] > 0;
				MainViewModel.Instance.Show_BarracksMaceman = GameData.Instance.lastGameState.troop_types_available[4] > 0;
				MainViewModel.Instance.Show_BarracksSwordsman = GameData.Instance.lastGameState.troop_types_available[5] > 0;
				MainViewModel.Instance.Show_BarracksKnight = GameData.Instance.lastGameState.troop_types_available[6] > 0;
				MainViewModel.Instance.Show_BarracksBows1 = GameData.Instance.lastGameState.weapon_types_available[0] > 0;
				MainViewModel.Instance.Show_BarracksXBows1 = GameData.Instance.lastGameState.weapon_types_available[1] > 0;
				MainViewModel.Instance.Show_BarracksSpears1 = GameData.Instance.lastGameState.weapon_types_available[2] > 0;
				MainViewModel.Instance.Show_BarracksPikes1 = GameData.Instance.lastGameState.weapon_types_available[3] > 0;
				MainViewModel.Instance.Show_BarracksMaces1 = GameData.Instance.lastGameState.weapon_types_available[4] > 0;
				MainViewModel.Instance.Show_BarracksSwords1 = GameData.Instance.lastGameState.weapon_types_available[5] > 0;
				MainViewModel.Instance.Show_BarracksLeatherArmour1 = GameData.Instance.lastGameState.weapon_types_available[6] > 0;
				MainViewModel.Instance.Show_BarracksArmour1 = GameData.Instance.lastGameState.weapon_types_available[7] > 0;
				MainViewModel.Instance.Show_BarracksHorses1 = GameData.Instance.lastGameState.weapon_types_available[8] > 0;
				MainViewModel.Instance.Show_BarracksBows3 = GameData.Instance.lastGameState.weapon_types_available[0] > 0 && GameData.Instance.lastGameState.resources[17] > 0;
				MainViewModel.Instance.Show_BarracksXBows3 = GameData.Instance.lastGameState.weapon_types_available[1] > 0 && GameData.Instance.lastGameState.resources[18] > 0;
				MainViewModel.Instance.Show_BarracksSpears3 = GameData.Instance.lastGameState.weapon_types_available[2] > 0 && GameData.Instance.lastGameState.resources[19] > 0;
				MainViewModel.Instance.Show_BarracksPikes3 = GameData.Instance.lastGameState.weapon_types_available[3] > 0 && GameData.Instance.lastGameState.resources[20] > 0;
				MainViewModel.Instance.Show_BarracksMaces3 = GameData.Instance.lastGameState.weapon_types_available[4] > 0 && GameData.Instance.lastGameState.resources[21] > 0;
				MainViewModel.Instance.Show_BarracksSwords3 = GameData.Instance.lastGameState.weapon_types_available[5] > 0 && GameData.Instance.lastGameState.resources[22] > 0;
				MainViewModel.Instance.Show_BarracksLeatherArmour3 = GameData.Instance.lastGameState.weapon_types_available[6] > 0 && GameData.Instance.lastGameState.resources[23] > 0;
				MainViewModel.Instance.Show_BarracksArmour3 = GameData.Instance.lastGameState.weapon_types_available[7] > 0 && GameData.Instance.lastGameState.resources[24] > 0;
				MainViewModel.Instance.Show_BarracksHorses3 = GameData.Instance.lastGameState.weapon_types_available[8] > 0 && GameData.Instance.lastGameState.total_horses_available > 0;
				MainViewModel.Instance.Show_BarracksBowsOpaque = (MainViewModel.Instance.Show_BarracksBows3 ? 1f : 0.5f);
				MainViewModel.Instance.Show_BarracksXBowsOpaque = (MainViewModel.Instance.Show_BarracksXBows3 ? 1f : 0.5f);
				MainViewModel.Instance.Show_BarracksSpearsOpaque = (MainViewModel.Instance.Show_BarracksSpears3 ? 1f : 0.5f);
				MainViewModel.Instance.Show_BarracksPikesOpaque = (MainViewModel.Instance.Show_BarracksPikes3 ? 1f : 0.5f);
				MainViewModel.Instance.Show_BarracksMacesOpaque = (MainViewModel.Instance.Show_BarracksMaces3 ? 1f : 0.5f);
				MainViewModel.Instance.Show_BarracksSwordsOpaque = (MainViewModel.Instance.Show_BarracksSwords3 ? 1f : 0.5f);
				MainViewModel.Instance.Show_BarracksLeatherArmourOpaque = (MainViewModel.Instance.Show_BarracksLeatherArmour3 ? 1f : 0.5f);
				MainViewModel.Instance.Show_BarracksArmourOpaque = (MainViewModel.Instance.Show_BarracksArmour3 ? 1f : 0.5f);
				MainViewModel.Instance.Show_BarracksHorsesOpaque = (MainViewModel.Instance.Show_BarracksHorses3 ? 1f : 0.5f);
			}
			if (GameData.Instance.lastGameState.app_sub_mode == 4)
			{
				MainViewModel.Instance.GranaryFoodBarWidth = 160 * GameData.Instance.lastGameState.food_clock / 12000;
				num = ((GameData.Instance.lastGameState.total_food != 1) ? 1 : 2);
				MainViewModel.Instance.InGranaryUnitsOfFoodText = GameData.Instance.lastGameState.total_food + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GRANARY, num);
				if (GameData.Instance.lastGameState.months_of_food <= 0)
				{
					num = 11;
					MainViewModel.Instance.InGranaryMonthsOfFoodText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GRANARY, num);
				}
				else
				{
					num = ((GameData.Instance.lastGameState.months_of_food != 1) ? 9 : 10);
					MainViewModel.Instance.InGranaryMonthsOfFoodText = GameData.Instance.lastGameState.months_of_food + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GRANARY, num);
				}
				if (GameData.Instance.lastGameState.food_types_eaten > 1)
				{
					MainViewModel.Instance.InGranaryTypesPopFoodText = (GameData.Instance.lastGameState.foodsEaten_popularity / 25).ToString();
					num = ((GameData.Instance.lastGameState.food_types_eaten != 1) ? 7 : 8);
					MainViewModel.Instance.InGranaryTypesOfFoodText = GameData.Instance.lastGameState.food_types_eaten + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GRANARY, num);
				}
				else
				{
					MainViewModel.Instance.InGranaryTypesPopFoodText = "";
					MainViewModel.Instance.InGranaryTypesOfFoodText = "";
				}
				MainViewModel.Instance.InGranaryRationsPopText = (GameData.Instance.lastGameState.rationing_popularity / 25).ToString();
				switch (GameData.Instance.lastGameState.rationing)
				{
				case 0:
					MainViewModel.Instance.InGranaryRationLevelText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GRANARY, 3);
					break;
				case 1:
					MainViewModel.Instance.InGranaryRationLevelText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GRANARY, 4);
					break;
				case 2:
					MainViewModel.Instance.InGranaryRationLevelText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GRANARY, 5);
					break;
				case 3:
					MainViewModel.Instance.InGranaryRationLevelText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GRANARY, 6);
					break;
				case 4:
					MainViewModel.Instance.InGranaryRationLevelText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GRANARY, 12);
					break;
				default:
					MainViewModel.Instance.InGranaryRationLevelText = "";
					break;
				}
			}
			else if (GameData.Instance.lastGameState.app_sub_mode == 3)
			{
				MainViewModel.Instance.InInnBarrelsOfAleText = GameData.Instance.lastGameState.barrels_of_ale.ToString();
				MainViewModel.Instance.InInnFlagonsOfAleText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_INN, 3) + " " + GameData.Instance.lastGameState.pints_of_ale;
				MainViewModel.Instance.InInnWorkingInnsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_INN, 7) + " " + GameData.Instance.lastGameState.working_inns + " (" + GameData.Instance.lastGameState.total_inns + ")";
				MainViewModel.Instance.InInnPopularityText = (GameData.Instance.lastGameState.inn_coverage_popularity / 25).ToString();
				if (!turkish)
				{
					MainViewModel.Instance.InInnCoverageText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_INN, 5) + " " + GameData.Instance.lastGameState.inn_coverage_percent + "%";
				}
				else
				{
					MainViewModel.Instance.InInnCoverageText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_INN, 5) + " %" + GameData.Instance.lastGameState.inn_coverage_percent;
				}
				if (GameData.Instance.lastGameState.inn_coverage_next > 0)
				{
					if (!turkish)
					{
						MainViewModel.Instance.InInnNextLevelText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_INN, 6) + " " + GameData.Instance.lastGameState.inn_coverage_next + "%";
					}
					else
					{
						MainViewModel.Instance.InInnNextLevelText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_INN, 6) + " %" + GameData.Instance.lastGameState.inn_coverage_next;
					}
				}
				else
				{
					MainViewModel.Instance.InInnNextLevelText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_INN, 8);
				}
			}
			else if (GameData.Instance.lastGameState.app_sub_mode == 2)
			{
				MainViewModel.Instance.InKeepPopulationText = GameData.Instance.lastGameState.population.ToString();
				MainViewModel.Instance.InKeepIncomeText = GameData.Instance.lastGameState.tax_amount.ToString();
				MainViewModel.Instance.InKeepTaxRateText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_KEEP, GameData.Instance.lastGameState.tax_rate + 7);
				MainViewModel.Instance.InKeepTaxPopText = (GameData.Instance.lastGameState.tax_popularity / 25).ToString();
				MainViewModel.Instance.InKeepSliderPos = GameData.Instance.lastGameState.tax_rate * 25;
			}
			else if (GameData.Instance.lastGameState.app_sub_mode == 13)
			{
				string workshopProducingText;
				if (GameData.Instance.lastGameState.production_no_resources > 0)
				{
					workshopProducingText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 11);
				}
				else
				{
					workshopProducingText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 3);
					workshopProducingText = ((GameData.Instance.lastGameState.weapon_being_made_now != 17) ? (workshopProducingText + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 8)) : (workshopProducingText + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 7)));
				}
				string text3 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 4);
				text3 = ((GameData.Instance.lastGameState.weapon_being_made_next != 17) ? (text3 + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 8)) : (text3 + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 7)));
				MainViewModel.Instance.WorkshopProducingText = workshopProducingText;
				MainViewModel.Instance.WorkshopProducingNextText = text3;
			}
			else if (GameData.Instance.lastGameState.app_sub_mode == 15)
			{
				string workshopProducingText2;
				if (GameData.Instance.lastGameState.production_no_resources > 0)
				{
					workshopProducingText2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 11);
				}
				else
				{
					workshopProducingText2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 3);
					workshopProducingText2 = ((GameData.Instance.lastGameState.weapon_being_made_now != 19) ? (workshopProducingText2 + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 10)) : (workshopProducingText2 + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 9)));
				}
				string text4 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 4);
				text4 = ((GameData.Instance.lastGameState.weapon_being_made_next != 19) ? (text4 + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 10)) : (text4 + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 9)));
				MainViewModel.Instance.WorkshopProducingText = workshopProducingText2;
				MainViewModel.Instance.WorkshopProducingNextText = text4;
			}
			else if (GameData.Instance.lastGameState.app_sub_mode == 14)
			{
				string workshopProducingText3;
				if (GameData.Instance.lastGameState.production_no_resources > 0)
				{
					workshopProducingText3 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 12);
				}
				else
				{
					workshopProducingText3 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 3);
					workshopProducingText3 = ((GameData.Instance.lastGameState.weapon_being_made_now != 21) ? (workshopProducingText3 + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 5)) : (workshopProducingText3 + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 6)));
				}
				string text5 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 4);
				text5 = ((GameData.Instance.lastGameState.weapon_being_made_next != 21) ? (text5 + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 5)) : (text5 + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 6)));
				MainViewModel.Instance.WorkshopProducingText = workshopProducingText3;
				MainViewModel.Instance.WorkshopProducingNextText = text5;
			}
			else if (GameData.Instance.lastGameState.app_sub_mode == 35)
			{
				MainViewModel.Instance.GroomNameText = "";
				MainViewModel.Instance.BrideNameText = "";
				MainViewModel.Instance.GroomImage = null;
				MainViewModel.Instance.BrideImage = null;
				if (GameData.Instance.lastGameState.marry_text == 34 && GameData.Instance.lastGameState.marry_status <= 0)
				{
					MainViewModel.Instance.ChurchPanelRingsVis = Visibility.Hidden;
					if (GameData.Instance.lastGameState.workers_have == 0)
					{
						MainViewModel.Instance.WeddingGossipText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 47);
					}
					else
					{
						MainViewModel.Instance.WeddingGossipText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MARRIAGE, GameData.Instance.lastGameState.marry_text);
					}
				}
				else
				{
					MainViewModel.Instance.ChurchPanelRingsVis = Visibility.Hidden;
					MainViewModel.Instance.WeddingGossipText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MARRIAGE, GameData.Instance.lastGameState.marry_text);
				}
				if (GameData.Instance.lastGameState.marry_status > 0)
				{
					MainViewModel.Instance.ChurchPanelRingsVis = Visibility.Visible;
					MainViewModel.Instance.GroomImage = MainViewModel.Instance.GameSprites[MainViewModel.Instance.HUDBuildingPanel.GetPartnerImage(GameData.Instance.lastGameState.marry_male_type)];
					MainViewModel.Instance.BrideImage = MainViewModel.Instance.GameSprites[MainViewModel.Instance.HUDBuildingPanel.GetPartnerImage(GameData.Instance.lastGameState.marry_female_type)];
					if (GameData.Instance.lastGameState.marry_m_name1 > 0)
					{
						string text6 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_PEASANT_NAMES, GameData.Instance.lastGameState.marry_m_name1);
						if (GameData.Instance.lastGameState.marry_m_name2 > 0)
						{
							text6 = text6 + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_PEASANT_SURNAMES, GameData.Instance.lastGameState.marry_m_name2);
						}
						MainViewModel.Instance.GroomNameText = text6;
					}
					if (GameData.Instance.lastGameState.marry_f_name1 > 0)
					{
						string text7 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_PEASANT_NAMES, GameData.Instance.lastGameState.marry_f_name1);
						if (GameData.Instance.lastGameState.marry_f_name2 > 0)
						{
							text7 = text7 + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_PEASANT_SURNAMES, GameData.Instance.lastGameState.marry_f_name2);
						}
						MainViewModel.Instance.BrideNameText = text7;
					}
				}
			}
			else if (GameData.Instance.lastGameState.app_sub_mode == 58 || GameData.Instance.lastGameState.app_sub_mode == 61 || GameData.Instance.lastGameState.app_sub_mode == 60 || GameData.Instance.lastGameState.app_sub_mode == 62 || GameData.Instance.lastGameState.app_sub_mode == 59)
			{
				int num14 = 0;
				if (GameData.Instance.lastGameState.app_sub_mode == 58)
				{
					num14 = 320;
				}
				if (GameData.Instance.lastGameState.app_sub_mode == 61)
				{
					num14 = 640;
				}
				if (GameData.Instance.lastGameState.app_sub_mode == 60)
				{
					num14 = 1280;
				}
				if (GameData.Instance.lastGameState.app_sub_mode == 62)
				{
					num14 = 120;
				}
				if (GameData.Instance.lastGameState.app_sub_mode == 59)
				{
					num14 = 640;
				}
				if (num14 > 0)
				{
					string text8 = (GameData.Instance.lastGameState.ai_clock * 100 / num14).ToString();
					MainViewModel.Instance.BuildingLine3Text = text8 + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_SIEGE_TENT, 6);
				}
			}
			else if (GameData.Instance.lastGameState.app_sub_mode == 34)
			{
				int ai_clock = GameData.Instance.lastGameState.ai_clock;
				int dog_cage_state = GameData.Instance.lastGameState.dog_cage_state;
				string text9 = ((ai_clock == 1) ? (ai_clock + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_STABLES, 3) + "\n") : (ai_clock + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_STABLES, 1) + "\n"));
				text9 = ((dog_cage_state == 1) ? (text9 + dog_cage_state + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_STABLES, 4)) : (text9 + dog_cage_state + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_STABLES, 2)));
				MainViewModel.Instance.BuildingLine3Text = text9;
			}
			else if (GameData.Instance.lastGameState.app_sub_mode == 56 || GameData.Instance.lastGameState.app_sub_mode == 54)
			{
				buildingTitleFontSize = ((!(locale == "frfr")) ? 32.0 : 26.0);
			}
			else if (GameData.Instance.lastGameState.app_sub_mode == 55)
			{
				buildingTitleFontSize = ((!(locale == "frfr")) ? 32.0 : 24.0);
			}
			else if (GameData.Instance.lastGameState.app_sub_mode == 57)
			{
				int trading_current_goods = GameData.Instance.lastGameState.trading_current_goods;
				Enums.eUISprites eUISprites = MainViewModel.Instance.goodsSpriteEnumFromGoodsEnum((Enums.Goods)GameData.Instance.lastGameState.trading_current_goods);
				Enums.eUISprites eUISprites2 = MainViewModel.Instance.goodsSpriteEnumFromGoodsEnum((Enums.Goods)GameData.Instance.lastGameState.trading_prev_goods);
				Enums.eUISprites eUISprites3 = MainViewModel.Instance.goodsSpriteEnumFromGoodsEnum((Enums.Goods)GameData.Instance.lastGameState.trading_next_goods);
				int num15 = GameData.Instance.lastGameState.trade_buy_amounts[trading_current_goods];
				int num16 = GameData.Instance.lastGameState.trade_sell_amounts[trading_current_goods];
				int num17 = GameData.Instance.lastGameState.trade_buy_costs[trading_current_goods];
				int num18 = GameData.Instance.lastGameState.trade_sell_costs[trading_current_goods];
				MainViewModel.Instance.TradeGoodsImage = MainViewModel.Instance.GameSprites[(int)eUISprites];
				MainViewModel.Instance.SetSpriteWidth1((int)eUISprites, 100);
				MainViewModel.Instance.TradePrevGoodsImage = MainViewModel.Instance.GameSprites[(int)eUISprites2];
				MainViewModel.Instance.SetSpriteWidth3((int)eUISprites2, 50);
				MainViewModel.Instance.TradeNextGoodsImage = MainViewModel.Instance.GameSprites[(int)eUISprites3];
				MainViewModel.Instance.SetSpriteWidth4((int)eUISprites3, 50);
				MainViewModel.Instance.TradeGoodsAmountText = GameData.Instance.lastGameState.resources[trading_current_goods].ToString();
				MainViewModel.Instance.BuyText = Translate.Instance.lookUpText("TEXT_IN_TRADEPOST_006") + "  " + num15;
				if (!german)
				{
					MainViewModel.Instance.SellText = Translate.Instance.lookUpText("TEXT_IN_TRADEPOST_007") + "  " + num16;
				}
				else
				{
					MainViewModel.Instance.SellText = Translate.Instance.lookUpText("TEXT_IN_TRADEPOST_007") + " " + num16;
				}
				MainViewModel.Instance.BuyPriceText = num17.ToString();
				MainViewModel.Instance.SellPriceText = num18.ToString();
				MainViewModel.Instance.HUDBuildingPanel.RefTradeBuyButton.IsEnabled = true;
				MainViewModel.Instance.HUDBuildingPanel.RefTradeSellButton.IsEnabled = true;
				if (num15 <= 0)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefTradeBuyButton.IsEnabled = false;
				}
				if (num16 <= 0)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefTradeSellButton.IsEnabled = false;
				}
			}
			else if (GameData.Instance.lastGameState.app_sub_mode == 71)
			{
				MainViewModel.Instance.PlayerNameText = ConfigSettings.Settings_UserName;
				string text10 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_PLAYER_DESC, GameData.Instance.lastGameState.playerdesc_message);
				text10 = text10 + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_PLAYER_DESC, 40);
				text10 = text10 + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_PLAYER_DESC, GameData.Instance.lastGameState.playerdesc_message2);
				MainViewModel.Instance.PlayerMottoText = text10;
			}
			else if (GameData.Instance.lastGameState.app_sub_mode == 72 || GameData.Instance.lastGameState.app_sub_mode == 69)
			{
				MainViewModel.Instance.PopReportFoodText = (GameData.Instance.lastGameState.food_popularity / 25).ToString();
				MainViewModel.Instance.PopReportTaxText = (GameData.Instance.lastGameState.tax_popularity / 25).ToString();
				MainViewModel.Instance.PopReportCrowdingText = (GameData.Instance.lastGameState.overcrowding_popularity / 25).ToString();
				MainViewModel.Instance.PopReportFearFactorText = (GameData.Instance.lastGameState.fearFactor_popularity / 25).ToString();
				MainViewModel.Instance.PopReportReligionText = (GameData.Instance.lastGameState.religion_popularity / 25).ToString();
				MainViewModel.Instance.PopReportAleText = (GameData.Instance.lastGameState.inn_coverage_popularity / 25).ToString();
				MainViewModel.Instance.PopReportTotalText = (GameData.Instance.lastGameState.upcoming_total_popularity / 25).ToString();
				int num19 = GameData.Instance.lastGameState.fairs_popularity + GameData.Instance.lastGameState.marriage_popularity + GameData.Instance.lastGameState.jester_popularity + GameData.Instance.lastGameState.plague_popularity + GameData.Instance.lastGameState.wolves_popularity + GameData.Instance.lastGameState.bandits_popularity + GameData.Instance.lastGameState.fire_popularity;
				MainViewModel.Instance.PopReportEventsText = (num19 / 25).ToString();
				MainViewModel.Instance.PopReportFairsText = (GameData.Instance.lastGameState.fairs_popularity / 25).ToString();
				MainViewModel.Instance.PopReportMarriageText = (GameData.Instance.lastGameState.marriage_popularity / 25).ToString();
				MainViewModel.Instance.PopReportJesterText = (GameData.Instance.lastGameState.jester_popularity / 25).ToString();
				MainViewModel.Instance.PopReportPlagueText = (GameData.Instance.lastGameState.plague_popularity / 25).ToString();
				MainViewModel.Instance.PopReportWolvesText = (GameData.Instance.lastGameState.wolves_popularity / 25).ToString();
				MainViewModel.Instance.PopReportBanditsText = (GameData.Instance.lastGameState.bandits_popularity / 25).ToString();
				MainViewModel.Instance.PopReportFireText = (GameData.Instance.lastGameState.fire_popularity / 25).ToString();
				MainViewModel.Instance.HUDBuildingPanel.RefShowEventsButton.Visibility = Visibility.Visible;
				if (GameData.Instance.lastGameState.app_sub_mode == 72 && num19 == 0)
				{
					MainViewModel.Instance.HUDBuildingPanel.RefShowEventsButton.Visibility = Visibility.Hidden;
				}
			}
			else if (GameData.Instance.lastGameState.app_sub_mode == 73)
			{
				int good_things = GameData.Instance.lastGameState.good_things;
				int bad_things = GameData.Instance.lastGameState.bad_things;
				MainViewModel.Instance.FFReportGoodBuildingsText = good_things.ToString();
				MainViewModel.Instance.FFReportBadBuildingsText = bad_things.ToString();
				MainViewModel.Instance.FFReportFearFactorText = (GameData.Instance.lastGameState.fearFactor_popularity / 25).ToString();
				MainViewModel.Instance.FFReportNextLevelText = "";
				MainViewModel.Instance.FFReportNextLevelAmountText = "";
				if (GameData.Instance.lastGameState.fear_factor >= 5)
				{
					MainViewModel.Instance.FFReportNextLevelText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_REPORT_BUTTONS, 25);
				}
				else if (GameData.Instance.lastGameState.fear_factor <= -5)
				{
					MainViewModel.Instance.FFReportNextLevelText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_REPORT_BUTTONS, 24);
				}
				else if (good_things > bad_things)
				{
					MainViewModel.Instance.FFReportNextLevelText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_REPORT_BUTTONS, 27);
					MainViewModel.Instance.FFReportNextLevelAmountText = GameData.Instance.lastGameState.fear_factor_next_level.ToString();
				}
				else if (bad_things > good_things)
				{
					MainViewModel.Instance.FFReportNextLevelText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_REPORT_BUTTONS, 26);
					MainViewModel.Instance.FFReportNextLevelAmountText = GameData.Instance.lastGameState.fear_factor_next_level.ToString();
				}
				int index = 0;
				if (GameData.Instance.lastGameState.fear_factor == 0)
				{
					index = 10;
				}
				else if (GameData.Instance.lastGameState.fear_factor > 0)
				{
					index = 11;
				}
				else if (GameData.Instance.lastGameState.fear_factor < 0)
				{
					index = 12;
				}
				MainViewModel.Instance.FFReportCommentaryText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_REPORT_BUTTONS, index);
				MainViewModel.Instance.FFReportEfficiencyAmountText = GameData.Instance.lastGameState.efficiency.ToString();
			}
			else if (GameData.Instance.lastGameState.app_sub_mode == 74)
			{
				int num20 = 0;
				int num21 = 1;
				int num22 = 1;
				int num23 = GameData.Instance.lastGameState.pop_months;
				int num24 = (GameData.Instance.lastGameState.pop_months - 300) / 12;
				if (num24 < 0)
				{
					num24 = 0;
				}
				string text11 = locale;
				buildingTitleFontSize = ((text11 == "plpl") ? 28.0 : ((!(text11 == "thth")) ? 32.0 : 26.0));
				if (num23 > 300)
				{
					num23 = 300;
				}
				for (int m = 0; m < num23; m++)
				{
					if (GameData.Instance.lastGameState.population_graph[m] > num20)
					{
						num20 = GameData.Instance.lastGameState.population_graph[m];
					}
				}
				if (num20 <= 8)
				{
					MainViewModel.Instance.GraphReportLeftScaleNo2Text = "8";
					MainViewModel.Instance.GraphReportLeftScaleNo1Text = "4";
					num21 = 12;
				}
				else if (num20 <= 16)
				{
					MainViewModel.Instance.GraphReportLeftScaleNo2Text = "16";
					MainViewModel.Instance.GraphReportLeftScaleNo1Text = "8";
					num21 = 6;
				}
				else if (num20 <= 24)
				{
					MainViewModel.Instance.GraphReportLeftScaleNo2Text = "24";
					MainViewModel.Instance.GraphReportLeftScaleNo1Text = "12";
					num21 = 4;
				}
				else if (num20 <= 32)
				{
					MainViewModel.Instance.GraphReportLeftScaleNo2Text = "32";
					MainViewModel.Instance.GraphReportLeftScaleNo1Text = "16";
					num21 = 3;
				}
				else if (num20 <= 50)
				{
					MainViewModel.Instance.GraphReportLeftScaleNo2Text = "50";
					MainViewModel.Instance.GraphReportLeftScaleNo1Text = "25";
					num21 = 2;
				}
				else if (num20 <= 100)
				{
					MainViewModel.Instance.GraphReportLeftScaleNo2Text = "100";
					MainViewModel.Instance.GraphReportLeftScaleNo1Text = "50";
					num21 = 1;
				}
				else if (num20 <= 200)
				{
					MainViewModel.Instance.GraphReportLeftScaleNo2Text = "200";
					MainViewModel.Instance.GraphReportLeftScaleNo1Text = "100";
					num22 = 2;
				}
				else if (num20 <= 300)
				{
					MainViewModel.Instance.GraphReportLeftScaleNo2Text = "300";
					MainViewModel.Instance.GraphReportLeftScaleNo1Text = "150";
					num22 = 3;
				}
				else if (num20 <= 400)
				{
					MainViewModel.Instance.GraphReportLeftScaleNo2Text = "400";
					MainViewModel.Instance.GraphReportLeftScaleNo1Text = "200";
					num22 = 4;
				}
				else if (num20 <= 500)
				{
					MainViewModel.Instance.GraphReportLeftScaleNo2Text = "500";
					MainViewModel.Instance.GraphReportLeftScaleNo1Text = "250";
					num22 = 5;
				}
				else if (num20 <= 600)
				{
					MainViewModel.Instance.GraphReportLeftScaleNo2Text = "600";
					MainViewModel.Instance.GraphReportLeftScaleNo1Text = "300";
					num22 = 6;
				}
				else if (num20 <= 800)
				{
					MainViewModel.Instance.GraphReportLeftScaleNo2Text = "800";
					MainViewModel.Instance.GraphReportLeftScaleNo1Text = "400";
					num22 = 8;
				}
				else if (num20 <= 1000)
				{
					MainViewModel.Instance.GraphReportLeftScaleNo2Text = "1000";
					MainViewModel.Instance.GraphReportLeftScaleNo1Text = "500";
					num22 = 10;
				}
				else
				{
					MainViewModel.Instance.GraphReportLeftScaleNo2Text = "2000";
					MainViewModel.Instance.GraphReportLeftScaleNo1Text = "1000";
					num22 = 20;
				}
				MainViewModel mainViewModel = MainViewModel.Instance;
				int num25 = num24;
				mainViewModel.GraphReportBottomScaleNo1Text = num25.ToString();
				MainViewModel.Instance.GraphReportBottomScaleNo2Text = (num24 + 5).ToString();
				MainViewModel.Instance.GraphReportBottomScaleNo3Text = (num24 + 10).ToString();
				MainViewModel.Instance.GraphReportBottomScaleNo4Text = (num24 + 15).ToString();
				MainViewModel.Instance.GraphReportBottomScaleNo5Text = (num24 + 20).ToString();
				MainViewModel.Instance.GraphReportBottomScaleNo6Text = (num24 + 25).ToString();
				string text12 = "";
				for (int n = 0; n < num23; n++)
				{
					if (n == 0)
					{
						text12 += "M";
					}
					int num26 = 100 - GameData.Instance.lastGameState.population_graph[n] * num21 / num22;
					string text13 = n + 1 + "," + num26;
					text12 += text13;
					if (n < num23 - 1)
					{
						text12 += ",";
					}
				}
				MainViewModel.Instance.GraphReportPathDataString = text12;
			}
			else if (GameData.Instance.lastGameState.app_sub_mode == 76 || GameData.Instance.lastGameState.app_sub_mode == 68)
			{
				int num27 = 0;
				for (int num28 = 0; num28 < 18; num28++)
				{
					num27 += GameData.Instance.lastGameState.troop_counts[num28];
				}
				MainViewModel.Instance.ArmyReportTotalTroopsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_REPORT_BUTTONS, 16) + " " + num27;
				if (GameData.Instance.lastGameState.fear_factor > 0)
				{
					if (!turkish)
					{
						MainViewModel.Instance.ArmyReportFFBoostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_REPORT_BUTTONS, 31) + " " + GameData.Instance.lastGameState.fear_factor * 5 + "%";
					}
					else
					{
						MainViewModel.Instance.ArmyReportFFBoostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_REPORT_BUTTONS, 31) + " %" + GameData.Instance.lastGameState.fear_factor * 5;
					}
				}
				else if (GameData.Instance.lastGameState.fear_factor < 0)
				{
					if (!turkish)
					{
						MainViewModel.Instance.ArmyReportFFBoostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_REPORT_BUTTONS, 31) + " " + GameData.Instance.lastGameState.fear_factor * 5 + "%";
					}
					else
					{
						MainViewModel.Instance.ArmyReportFFBoostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_REPORT_BUTTONS, 31) + " %" + GameData.Instance.lastGameState.fear_factor * 5;
					}
				}
				else
				{
					MainViewModel.Instance.ArmyReportFFBoostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_REPORT_BUTTONS, 30);
				}
			}
			else if (GameData.Instance.lastGameState.app_sub_mode == 79)
			{
				MainViewModel.Instance.RelReportTotalPriestsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_REPORT_BUTTONS, 22) + " : " + GameData.Instance.lastGameState.num_priests;
				MainViewModel.Instance.RelReportBlessedPeopleText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_REPORT_BUTTONS, 17) + " " + GameData.Instance.lastGameState.blessed_percent;
				MainViewModel.Instance.RelReportPopEffectText = (GameData.Instance.lastGameState.blessed_popularity / 25).ToString();
				MainViewModel.Instance.RelReportPopEffectTextLabelWidth = MainViewModel.Instance.HUDBuildingPanel.RefRelReportPopEffectLabel.RenderSize.Width;
				if (GameData.Instance.lastGameState.blessed_next_level_at != 0)
				{
					if (!turkish)
					{
						MainViewModel.Instance.RelReportNextLevelText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_REPORT_BUTTONS, 28) + " " + GameData.Instance.lastGameState.blessed_next_level_at + "%";
					}
					else
					{
						MainViewModel.Instance.RelReportNextLevelText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_REPORT_BUTTONS, 28) + " %" + GameData.Instance.lastGameState.blessed_next_level_at;
					}
				}
				else
				{
					MainViewModel.Instance.RelReportNextLevelText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_REPORT_BUTTONS, 29);
				}
				MainViewModel.Instance.HUDBuildingPanel.RefWGTRelReport2.Visibility = Visibility.Hidden;
				if (GameData.Instance.lastGameState.church_adjustment != 0)
				{
					if (((uint)GameData.Instance.lastGameState.church_missing & (true ? 1u : 0u)) != 0)
					{
						MainViewModel.Instance.RelReportTypeDemandedText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_REPORT_BUTTONS, 20);
						MainViewModel.Instance.RelReportDemandEffectText = (GameData.Instance.lastGameState.church_adjustment / 25).ToString();
						MainViewModel.Instance.HUDBuildingPanel.RefWGTRelReport2.Visibility = Visibility.Visible;
						MainViewModel.Instance.WGT_RelReportLabelWidth = MainViewModel.Instance.HUDBuildingPanel.RefWGT_RelReportLabel.RenderSize.Width;
					}
					else if ((GameData.Instance.lastGameState.church_missing & 2u) != 0)
					{
						MainViewModel.Instance.RelReportTypeDemandedText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_REPORT_BUTTONS, 21);
						MainViewModel.Instance.RelReportDemandEffectText = (GameData.Instance.lastGameState.church_adjustment / 25).ToString();
						MainViewModel.Instance.HUDBuildingPanel.RefWGTRelReport2.Visibility = Visibility.Visible;
						MainViewModel.Instance.WGT_RelReportLabelWidth = MainViewModel.Instance.HUDBuildingPanel.RefWGT_RelReportLabel.RenderSize.Width;
					}
					else
					{
						MainViewModel.Instance.RelReportTypeDemandedText = "";
					}
				}
				else
				{
					MainViewModel.Instance.RelReportTypeDemandedText = "";
				}
			}
			else if (GameData.Instance.lastGameState.app_sub_mode == 70)
			{
				int in_chimp_type = GameData.Instance.lastGameState.in_chimp_type;
				MainViewModel.Instance.ChimpTypeText = "";
				MainViewModel.Instance.ChimpCommentText = "";
				if (in_chimp_type == 54)
				{
					MainViewModel.Instance.ChimpNameText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, GameData.Instance.lastGameState.in_chimp_type);
				}
				else
				{
					MainViewModel.Instance.ChimpNameText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_PEASANT_NAMES, GameData.Instance.lastGameState.inchimp_name1);
					if (GameData.Instance.lastGameState.inchimp_name2 > 0)
					{
						MainViewModel mainViewModel2 = MainViewModel.Instance;
						mainViewModel2.ChimpNameText = mainViewModel2.ChimpNameText + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_PEASANT_SURNAMES, GameData.Instance.lastGameState.inchimp_name2);
					}
					MainViewModel.Instance.ChimpTypeText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, in_chimp_type);
					if (GameData.Instance.lastGameState.chimp_comments > 0)
					{
						MainViewModel.Instance.ChimpCommentText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_COMMENT, GameData.Instance.lastGameState.chimp_comments);
					}
				}
				MainViewModel.Instance.ChimpWorkText = getChimpActionText(GameData.Instance.lastGameState);
			}
		}
		if (Director.instance.MultiplayerGame && GameData.Instance.lastGameState.numMPChatEntries > 0)
		{
			for (int num29 = 0; num29 < GameData.Instance.lastGameState.numMPChatEntries; num29++)
			{
				switch (GameData.Instance.lastGameState.chat_store_data[num29, 0])
				{
				case 1:
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Platform_Multiplayer.Instance.getPlayerName(GameData.Instance.lastGameState.chat_store_data[num29, 1]), GameData.Instance.lastGameState.chat_store_data[num29, 1], Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MULTIPLAYER_CONNECTION, 41) + " " + GameData.Instance.lastGameState.chat_store_data[num29, 3] + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MULTIPLAYER_CONNECTION, 42));
					break;
				case 2:
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Platform_Multiplayer.Instance.getPlayerName(GameData.Instance.lastGameState.chat_store_data[num29, 1]), GameData.Instance.lastGameState.chat_store_data[num29, 1], Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MULTIPLAYER_CONNECTION, 37));
					break;
				case 3:
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Platform_Multiplayer.Instance.getPlayerName(GameData.Instance.lastGameState.chat_store_data[num29, 1]), GameData.Instance.lastGameState.chat_store_data[num29, 1], Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MULTIPLAYER_CONNECTION, 38) + " " + Platform_Multiplayer.Instance.getPlayerName(GameData.Instance.lastGameState.chat_store_data[num29, 2]));
					break;
				case 4:
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Platform_Multiplayer.Instance.getPlayerName(GameData.Instance.lastGameState.chat_store_data[num29, 1]), GameData.Instance.lastGameState.chat_store_data[num29, 1], Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MULTIPLAYER_CONNECTION, Enums.eTextValues.TEXT_SCN_MESSAGE));
					break;
				case 5:
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Platform_Multiplayer.Instance.getPlayerName(GameData.Instance.lastGameState.chat_store_data[num29, 1]), GameData.Instance.lastGameState.chat_store_data[num29, 1], Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MULTIPLAYER_CONNECTION, Enums.eTextValues.TEXT_SCN_EVENT));
					break;
				case 6:
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Platform_Multiplayer.Instance.getPlayerName(GameData.Instance.lastGameState.chat_store_data[num29, 1]), GameData.Instance.lastGameState.chat_store_data[num29, 1], Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MULTIPLAYER_CONNECTION, Enums.eTextValues.TEXT_SCN_CHEESE) + " " + Platform_Multiplayer.Instance.getPlayerName(GameData.Instance.lastGameState.chat_store_data[num29, 2]));
					break;
				case 7:
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Platform_Multiplayer.Instance.getPlayerName(GameData.Instance.lastGameState.chat_store_data[num29, 1]), GameData.Instance.lastGameState.chat_store_data[num29, 1], Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_ANY_OF_THESE));
					break;
				case 8:
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Platform_Multiplayer.Instance.getPlayerName(GameData.Instance.lastGameState.chat_store_data[num29, 1]), GameData.Instance.lastGameState.chat_store_data[num29, 1], Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MULTIPLAYER_CONNECTION, 40) + " " + Platform_Multiplayer.Instance.getPlayerName(GameData.Instance.lastGameState.chat_store_data[num29, 2]));
					break;
				case 9:
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Platform_Multiplayer.Instance.getPlayerName(GameData.Instance.lastGameState.chat_store_data[num29, 1]), GameData.Instance.lastGameState.chat_store_data[num29, 1], Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MULTIPLAYER_CONNECTION, 39));
					break;
				}
			}
		}
		if (MainViewModel.Instance.Show_HUD_Tutorial)
		{
			MainViewModel.Instance.HUDmain.monitorTutorialArrows();
		}
		if (MainViewModel.Instance.Show_HUD_MPInviteWarning)
		{
			MainViewModel.Instance.HUDMPInviteWarning.Update();
		}
		if (MainViewModel.Instance.Show_HUD_MPChatMessages)
		{
			MainViewModel.Instance.HUDMPChatMessages.Update();
		}
		if (GameData.Instance.game_type == 6)
		{
			OnScreenText.Instance.addOSTEntry(Enums.eOnScreenText.OST_STARTING_GOODS, 1, 0);
		}
		MouseIsUpStroke = false;
		MouseIsDownStroke = false;
		MainViewModel.Instance.BuildingTitleFontSize = buildingTitleFontSize;
	}

	public void BriefingUIUpdate()
	{
		string text = "";
		int startDate = 0;
		int nowDate = 0;
		int endDate = 0;
		if (MainViewModel.Instance.BriefingMode == 1)
		{
			List<GameData.ScenarioEvent> events = GameData.scenario.getEvents();
			text = GameData.scenario.getWinTimer(ref startDate, ref nowDate, ref endDate);
			if (text == null)
			{
				MainViewModel.Instance.HUDBriefingPanel.RefObjectiveTimer.Visibility = Visibility.Hidden;
			}
			else
			{
				MainViewModel.Instance.ObjectiveTimerText = text;
				MainViewModel.Instance.HUDBriefingPanel.RefObjectiveTimer.Visibility = Visibility.Visible;
				int num = nowDate - startDate;
				int num2 = endDate - startDate;
				if (num > num2)
				{
					num = num2;
				}
				if (num2 > 0)
				{
					MainViewModel.Instance.ObjectiveTimerWidth = 200 * num / num2;
				}
			}
			int num3 = events.Count;
			if (num3 > 9)
			{
				num3 = ((num3 <= 18) ? 9 : 10);
			}
			int num4 = 20 + num3 * 25;
			MainViewModel.Instance.BriefingTextMargin = "0," + num4 + ",0,0";
			MainViewModel.Instance.MissionBriefingText = GameData.Instance.GetMissionBriefing(null, fromBriefing: true);
			num4 -= 130;
			MainViewModel.Instance.BriefingTimerMargin = "100,0,0," + num4;
			if (GameData.Instance.game_type == 0 && GameData.Instance.mission6Prestart && events.Count == 0)
			{
				GameData.ScenarioEvent scenarioEvent = new GameData.ScenarioEvent();
				scenarioEvent.eventID = 11;
				scenarioEvent.valid = 1;
				events.Add(scenarioEvent);
			}
			UpdateObjectiveRows(events, MainViewModel.Instance.HUDBriefingPanel.RefWGTObjectives);
			if (((GameData.Instance.game_type == 0 && GameData.Instance.mission_level > 3) || GameData.Instance.game_type == 5 || GameData.Instance.game_type == 7 || GameData.Instance.game_type == 8 || GameData.Instance.game_type == 9 || GameData.Instance.game_type == 10 || GameData.Instance.game_type == 12 || GameData.Instance.game_type == 2) && !GameData.Instance.siegeThat && (GameData.Instance.game_type != 2 || GameData.Instance.mapType != 0))
			{
				MainViewModel.Instance.HUDBriefingPanel.RefBriefingDifficultyButton.IsEnabled = MainViewModel.Instance.BriefingFromStory && (GameData.Instance.game_type == 0 || GameData.Instance.game_type == 5 || GameData.Instance.game_type == 7 || GameData.Instance.game_type == 8 || GameData.Instance.game_type == 9 || GameData.Instance.game_type == 10 || GameData.Instance.game_type == 12);
				MainViewModel.Instance.HUDBriefingPanel.RefBriefingDifficultyButton.Visibility = Visibility.Visible;
				if (GameData.Instance.difficulty_level == Enums.GameDifficulty.DIFFICULTY_EASY)
				{
					MainViewModel.Instance.BriefingDifficultyText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAINOPTIONS, 19);
				}
				else if (GameData.Instance.difficulty_level == Enums.GameDifficulty.DIFFICULTY_NORMAL || GameData.Instance.difficulty_level == Enums.GameDifficulty.DIFFICULTY_NA)
				{
					MainViewModel.Instance.BriefingDifficultyText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAINOPTIONS, 20);
				}
				else if (GameData.Instance.difficulty_level == Enums.GameDifficulty.DIFFICULTY_HARD)
				{
					MainViewModel.Instance.BriefingDifficultyText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAINOPTIONS, 21);
				}
				else if (GameData.Instance.difficulty_level == Enums.GameDifficulty.DIFFICULTY_VERYHARD)
				{
					MainViewModel.Instance.BriefingDifficultyText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAINOPTIONS, 22);
				}
			}
			else
			{
				MainViewModel.Instance.HUDBriefingPanel.RefBriefingDifficultyButton.Visibility = Visibility.Hidden;
			}
		}
		else if (MainViewModel.Instance.BriefingMode == 2)
		{
			MainViewModel.Instance.HUDBriefingPanel.RefBriefingDifficultyButton.Visibility = Visibility.Hidden;
			MainViewModel.Instance.MissionStrategyText = GameData.Instance.GetStrategyText();
			for (int i = 0; i < 5; i++)
			{
				MainViewModel.Instance.HUDBriefingPanel.RefBriefingHintButtons[i].Visibility = Visibility.Hidden;
				MainViewModel.Instance.HUDBriefingPanel.RefBriefingHintTexts[i].Visibility = Visibility.Hidden;
			}
			int num5 = GameData.Instance.GetNumHintsForCurrentMission();
			if (num5 == 0)
			{
				MainViewModel.Instance.HUDBriefingPanel.RefHintsTitleStamp.Visibility = Visibility.Hidden;
				MainViewModel.Instance.BriefingHintsH = "";
				MainViewModel.Instance.BriefingHintsints = "";
				return;
			}
			MainViewModel.Instance.HUDBriefingPanel.RefHintsTitleStamp.Visibility = Visibility.Visible;
			string text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HINTS, 2);
			MainViewModel.Instance.BriefingHintsH = text2.Substring(0, 1);
			MainViewModel.Instance.BriefingHintsints = text2.Substring(1, text2.Length - 1);
			if (num5 > 5)
			{
				num5 = 5;
			}
			for (int j = 0; j < num5; j++)
			{
				text = GameData.Instance.GetHintText(j);
				if (text == "")
				{
					MainViewModel.Instance.HUDBriefingPanel.RefBriefingHintButtons[j].Visibility = Visibility.Visible;
					MainViewModel.Instance.HUDBriefingPanel.RefBriefingHintTexts[j].Visibility = Visibility.Visible;
					MainViewModel.Instance.HUDBriefingPanel.RefBriefingHintTexts[j].Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HINTS, 4);
					break;
				}
				MainViewModel.Instance.HUDBriefingPanel.RefBriefingHintButtons[j].Visibility = Visibility.Visible;
				MainViewModel.Instance.HUDBriefingPanel.RefBriefingHintTexts[j].Visibility = Visibility.Visible;
				MainViewModel.Instance.HUDBriefingPanel.RefBriefingHintTexts[j].Text = text;
			}
		}
		else if (MainViewModel.Instance.BriefingMode == 3)
		{
			MainViewModel.Instance.HUDBriefingPanel.RefBriefingDifficultyButton.Visibility = Visibility.Hidden;
			if (MainViewModel.Instance.HUDBriefingPanel.canGoBack())
			{
				MainViewModel.Instance.HUDBriefingPanel.RefBriefingHelpBackButton.Visibility = Visibility.Visible;
			}
			else
			{
				MainViewModel.Instance.HUDBriefingPanel.RefBriefingHelpBackButton.Visibility = Visibility.Hidden;
			}
		}
	}

	public int UpdateObjectiveRows(List<GameData.ScenarioEvent> eventsList, WGT_Objective[] RefWGTObjectives)
	{
		int num = RefWGTObjectives.Length;
		int num2 = 0;
		foreach (GameData.ScenarioEvent events in eventsList)
		{
			bool complete = false;
			string text = "";
			bool flag = true;
			if (events == null)
			{
				continue;
			}
			int num3 = events.eventID;
			if (num3 >= 20)
			{
				num3++;
			}
			if (num3 == 5)
			{
				num3 = 7;
			}
			if (num3 == 6)
			{
				num3 = 7;
			}
			if (num3 == 17)
			{
				num3 = 7;
			}
			if (num3 == 15 || num3 == 32)
			{
				num3 = 8;
			}
			string text2;
			if (num3 == 25 || num3 == 27 || num3 == 28 || num3 == 29 || num3 == 31)
			{
				int index = 132 + events.eventType;
				if (events.eventType == 31)
				{
					index = 166;
				}
				text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, index);
				flag = false;
			}
			else if (num3 == 26)
			{
				text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 89);
				flag = false;
			}
			else if (num3 == 33)
			{
				text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 147);
			}
			else if (num3 == 34)
			{
				text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 148);
			}
			else if (num3 == 30)
			{
				text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 6);
				flag = false;
			}
			else if (num3 == 3)
			{
				text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_OBJECTIVES, num3);
				if (events.eventType == 5)
				{
					text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_KILL_ALL_ENEMY_LORDS);
				}
				else if (events.eventType > 0)
				{
					text2 = text2 + " - " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, GameData.lord_killed_list[events.eventType]);
				}
				flag = false;
			}
			else if (num3 == 8 && GameData.Instance.game_type == 10 && GameData.Instance.mission_level == 75)
			{
				text2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 31);
				flag = false;
			}
			else
			{
				bool flag2 = false;
				if (GameData.Instance.game_type != 0)
				{
					switch (num3)
					{
					case 11:
						num3 = 117;
						flag2 = true;
						break;
					case 12:
						num3 = 118;
						flag2 = true;
						break;
					case 13:
						num3 = 119;
						flag2 = true;
						break;
					case 14:
						num3 = 120;
						flag2 = true;
						break;
					}
				}
				text2 = (flag2 ? Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, num3) : Translate.Instance.lookUpText(Enums.eTextSections.TEXT_OBJECTIVES, num3));
			}
			switch (events.eventID)
			{
			case 5:
			case 6:
			case 7:
			case 17:
				if (events.eventType > 0)
				{
					text = text + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, events.eventType) + " ";
					flag = true;
				}
				break;
			case 1:
			case 4:
			case 20:
			case 21:
			case 22:
			case 23:
			case 32:
			case 33:
				flag = true;
				break;
			default:
				flag = false;
				break;
			}
			if (events.complete > 0)
			{
				complete = true;
			}
			if (flag)
			{
				text = ((events.currentAmount != -9999 && !MainViewModel.Instance.BriefingFromStory) ? (text + events.targetAmount + " (" + events.currentAmount + ")") : (text + events.targetAmount));
			}
			RefWGTObjectives[num2].SetObjective(isActive: true, text2, text, complete);
			num2++;
			if (num2 >= num)
			{
				break;
			}
		}
		for (int i = num2; i < num; i++)
		{
			RefWGTObjectives[i].SetObjective(isActive: false, "", "", complete: false);
		}
		return num2;
	}

	private void RadarScrollMap()
	{
		if (!mouseIsDown)
		{
			radarScrollTrigged = false;
		}
		if (GameData.Instance.lastGameState == null || currentScene != Enums.SceneIDS.ActualMainGame || !MainViewModel.Instance.RadarLoaded || !MainViewModel.Instance.MainUILoaded || (MouseIsDownStroke && (NGMousePoint.X < 0f || NGMousePoint.X >= (float)SHRadarRectSize || NGMousePoint.Y < 0f || NGMousePoint.Y >= (float)SHRadarRectSize)) || !mouseIsDown || MainControls.instance.CurrentAction == 8 || MainControls.instance.CurrentAction == 9 || MainControls.instance.CurrentAction == 5)
		{
			return;
		}
		if (MouseIsDownStroke)
		{
			if (MainViewModel.Instance.HUDRoot.RefRadarME.Opacity != 0f)
			{
				MainViewModel.Instance.HUDRoot.RefRadarME.Opacity = 0f;
				MouseIsDownStroke = false;
				radarScrollTrigged = false;
			}
			else
			{
				radarScrollTrigged = true;
				LastNGMousePoint = NGMousePoint;
				EngineInterface.GameAction(Enums.GameActionCommand.RadarClicked, (int)(NGMousePoint.X * SHRadarScalar), (int)(NGMousePoint.Y * SHRadarScalar));
			}
		}
		else
		{
			if (!radarScrollTrigged)
			{
				return;
			}
			float num = NGMousePoint.X - LastNGMousePoint.X;
			float num2 = LastNGMousePoint.Y - NGMousePoint.Y;
			if (num == 0f && num2 != 0f)
			{
				if (num2 > 0f)
				{
					KeyManager.instance.RadarHeldY = 1f;
				}
				else
				{
					KeyManager.instance.RadarHeldY = -1f;
				}
				return;
			}
			if (num2 == 0f && num != 0f)
			{
				if (num > 0f)
				{
					KeyManager.instance.RadarHeldX = 1f;
				}
				else
				{
					KeyManager.instance.RadarHeldX = -1f;
				}
				return;
			}
			float num3 = Math.Abs(num);
			float num4 = Math.Abs(num2);
			if (num3 > num4)
			{
				KeyManager.instance.RadarHeldX = num / num3;
				KeyManager.instance.RadarHeldY = num2 / num3;
			}
			else
			{
				KeyManager.instance.RadarHeldX = num / num4;
				KeyManager.instance.RadarHeldY = num2 / num4;
			}
		}
	}

	public void InitScenarioEditorValues()
	{
		EngineInterface.ScenarioOverview scenarioOverview = GameData.Instance.scenarioOverview;
		MainViewModel.Instance.ScenarioStartingMonthText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MONTHS, scenarioOverview.startMonth);
		MainViewModel.Instance.ScenarioStartingPopText = scenarioOverview.scenario_start_popularity.ToString();
		MainViewModel.Instance.ScenarioStartingYearText = scenarioOverview.startYear.ToString();
		MainViewModel.Instance.SetStartingSpecial(scenarioOverview.special_start > 0);
		MainViewModel.Instance.ScenarioStartingSpecialGoldText = scenarioOverview.special_start_gold.ToString();
		string scenarioStartingSpecialRationsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GRANARY, 3);
		switch (scenarioOverview.special_start_rationing)
		{
		case 1:
			scenarioStartingSpecialRationsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GRANARY, 4);
			break;
		case 2:
			scenarioStartingSpecialRationsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GRANARY, 5);
			break;
		case 3:
			scenarioStartingSpecialRationsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GRANARY, 6);
			break;
		case 4:
			scenarioStartingSpecialRationsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GRANARY, 12);
			break;
		}
		MainViewModel.Instance.ScenarioStartingSpecialRationsText = scenarioStartingSpecialRationsText;
		MainViewModel.Instance.ScenarioStartingSpecialTaxText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_KEEP, scenarioOverview.special_start_tax_rate + 7);
		MainViewModel.Instance.ScenarioStartingGoldText = scenarioOverview.scenario_start_goods[15].ToString();
		MainViewModel.Instance.ScenarioStartingPitchText = scenarioOverview.scenario_start_goods[8].ToString();
		MainViewModel.Instance.SetMapTypeVisibility(GameData.Instance.mapType == Enums.GameModes.SIEGE);
		ScenarioEditorUpdateNewEventButtons();
	}

	private void ScenarioEditorUpdateNewEventButtons()
	{
		bool flag = false;
		bool flag2 = false;
		if (GameData.Instance.mapType != 0 && MainViewModel.Instance.ScenarioEditorMode == Enums.ScenarioViews.Main)
		{
			flag = ((GameData.Instance.mapType != Enums.GameModes.SIEGE && GameData.Instance.mapType != Enums.GameModes.ECO) ? true : false);
			flag2 = true;
		}
		if (MainViewModel.Instance.ScenarioNewEventMessageVisibleBool != flag2)
		{
			MainViewModel.Instance.ScenarioNewEventMessageVisibleBool = flag2;
		}
		if (MainViewModel.Instance.ScenarioNewInvasionVisibleBool != flag)
		{
			MainViewModel.Instance.ScenarioNewInvasionVisibleBool = flag;
		}
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = false;
		bool flag6 = false;
		switch (MainViewModel.Instance.ScenarioEditorMode)
		{
		case Enums.ScenarioViews.Main:
			flag6 = true;
			break;
		case Enums.ScenarioViews.Messages:
			flag4 = true;
			if (!MainViewModel.Instance.HUDScenario.isEventPanelOpen())
			{
				flag5 = true;
			}
			break;
		case Enums.ScenarioViews.Invasions:
		case Enums.ScenarioViews.Events:
			flag4 = true;
			flag5 = true;
			break;
		case Enums.ScenarioViews.EventsConditions:
		case Enums.ScenarioViews.EventsActions:
			flag4 = true;
			break;
		default:
			flag3 = true;
			break;
		}
		if (MainViewModel.Instance.ScenarioCommonBackVisibleBool != flag3)
		{
			MainViewModel.Instance.ScenarioCommonBackVisibleBool = flag3;
		}
		if (MainViewModel.Instance.ScenarioCommonOKVisibleBool != flag4)
		{
			MainViewModel.Instance.ScenarioCommonOKVisibleBool = flag4;
		}
		if (MainViewModel.Instance.ScenarioCommonDeleteVisibleBool != flag5)
		{
			MainViewModel.Instance.ScenarioCommonDeleteVisibleBool = flag5;
		}
		if (MainViewModel.Instance.ScenarioCommonEditTeamsVisible != flag6)
		{
			MainViewModel.Instance.ScenarioCommonEditTeamsVisible = flag6;
		}
	}

	public bool addChimpActions(EngineInterface.PlayState state, ref string line1, ref string line2)
	{
		line1 = "";
		line2 = "";
		if (state == null)
		{
			return false;
		}
		if (state.building_type_sleeping != 0)
		{
			line2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 131);
			MainViewModel.Instance.HUDBuildingPanel.RefBuildingZZZButtonOff.Visibility = Visibility.Visible;
			MainViewModel.Instance.HUDBuildingPanel.RefBuildingZZZButtonOn.Visibility = Visibility.Hidden;
			return true;
		}
		MainViewModel.Instance.HUDBuildingPanel.RefBuildingZZZButtonOff.Visibility = Visibility.Hidden;
		MainViewModel.Instance.HUDBuildingPanel.RefBuildingZZZButtonOn.Visibility = Visibility.Visible;
		if (state.have_building_stats <= 0)
		{
			return false;
		}
		if (state.got_keep_access == 0)
		{
			line2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GENERAL_BUILDINGS, 9);
		}
		else if (state.turned_off > 0)
		{
			line2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GENERAL_BUILDINGS, 7);
		}
		else if (state.job_vacancies > 0)
		{
			if (state.workers_have == 0)
			{
				line2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GENERAL_BUILDINGS, 3);
			}
			else
			{
				line2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GENERAL_BUILDINGS, 4 + state.job_vacancies);
			}
		}
		else if (state.working > 0)
		{
			switch (state.in_structure_type)
			{
			case 34:
				if (state.mill_message < 100)
				{
					line1 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_MILL, state.mill_message + 3);
				}
				break;
			case 20:
				line1 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GENERAL_BUILDINGS, 2);
				break;
			case 12:
			case 13:
			case 14:
				line1 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GENERAL_BUILDINGS, 10);
				line2 = getChimpActionText(state);
				break;
			case 5:
				line1 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GENERAL_BUILDINGS, 10);
				line2 = getChimpActionText(state);
				break;
			default:
				line1 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GENERAL_BUILDINGS, 10);
				line2 = getChimpActionText(state);
				break;
			}
		}
		else
		{
			line1 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_GENERAL_BUILDINGS, 4);
		}
		return true;
	}

	public string getChimpActionText(EngineInterface.PlayState state)
	{
		int inchimp_n_text = state.inchimp_n_text;
		int in_chimp_goods = state.in_chimp_goods;
		if (inchimp_n_text > 0)
		{
			if (inchimp_n_text == 10 && in_chimp_goods > 0)
			{
				return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_UNIT_ACTIONS, 101) + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, in_chimp_goods) + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_UNIT_ACTIONS, 102);
			}
			if (inchimp_n_text == 6 && in_chimp_goods > 0)
			{
				return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_UNIT_ACTIONS, 100) + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, in_chimp_goods);
			}
			if (inchimp_n_text == 9 && in_chimp_goods > 0)
			{
				return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_UNIT_ACTIONS, 103) + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, in_chimp_goods);
			}
			return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_UNIT_ACTIONS, inchimp_n_text);
		}
		return "";
	}

	public bool overNoesisGUI()
	{
		overUI = false;
		overBuildingMenu = false;
		if (!MainViewModel.Instance.MainUILoaded)
		{
			return false;
		}
		if (m_MainCamera == null)
		{
			m_MainCamera = Camera.main.transform.GetComponent<Camera>();
		}
		NGview = m_MainCamera.GetComponent<NoesisView>();
		Visual obj = (Visual)VisualTreeHelper.GetRoot(NGview.Content);
		if (MainViewModel.Instance.HUDRoot.RefRadarMapImage != null && MainViewModel.Instance.RadarLoaded)
		{
			NGMousePoint = Mouse.GetPosition(MainViewModel.Instance.HUDRoot.RefRadarMapImage);
		}
		if (MainViewModel.Instance.Show_HUD_Briefing && MainViewModel.Instance.HUDBriefingPanel.webBrowserLoaded)
		{
			BriefingHelpMousePoint = Mouse.GetPosition(MainViewModel.Instance.HUDBriefingPanel.RefBriefingHelpTexture);
		}
		if (MainViewModel.Instance.Show_HUD_Help && MainViewModel.Instance.HUDHelp.webBrowserLoaded)
		{
			BriefingHelpMousePoint = Mouse.GetPosition(MainViewModel.Instance.HUDHelp.RefMainHelpTexture);
		}
		Vector3 mousePosition = Input.mousePosition;
		Point point = obj.PointFromScreen(new Point(mousePosition.x, (float)Screen.height - mousePosition.y));
		HitTestResult hitTestResult = VisualTreeHelper.HitTest(obj, point);
		Visual visual = (Visual)hitTestResult.VisualHit;
		UIElement uIElement = (UIElement)hitTestResult.VisualHit;
		if ((object)uIElement == null)
		{
			lastUIHit = "";
			return false;
		}
		if (visual != null)
		{
			lastUIHit = visual.GetType().ToString();
		}
		UIElement uIElement2 = uIElement;
		bool flag = true;
		while (uIElement2 is FrameworkElement)
		{
			if (((FrameworkElement)uIElement2).Name == "AlignmentGrid" || ((FrameworkElement)uIElement2).Name == "InBuildingLayoutRoot")
			{
				overBuildingMenu = true;
			}
			if (((FrameworkElement)uIElement2).Tag != null && ((FrameworkElement)uIElement2).Tag is string)
			{
				if ((string)((FrameworkElement)uIElement2).Tag == "Ignore")
				{
					return false;
				}
				if (flag && (string)((FrameworkElement)uIElement2).Tag == "IgnoreSelf")
				{
					return false;
				}
			}
			flag = false;
			if (((FrameworkElement)uIElement2).Parent != null)
			{
				uIElement2 = ((FrameworkElement)uIElement2).Parent;
				continue;
			}
			DependencyObject parent = VisualTreeHelper.GetParent(uIElement2);
			if (parent == null || !(parent is FrameworkElement))
			{
				break;
			}
			uIElement2 = (UIElement)parent;
		}
		overUI = true;
		return true;
	}

	public void NewScene(Enums.SceneIDS sceneNo)
	{
		if (currentScene == Enums.SceneIDS.ActualMainGame && sceneNo != Enums.SceneIDS.ActualMainGame)
		{
			if (Director.instance.SimRunning)
			{
				EditorDirector.instance.stopGameSim(leavingScene: true);
			}
			TilemapManager.instance.ClearTilemap();
			GameMap.instance.clearSprites();
		}
		MainViewModel.Instance.Show_InGame = false;
		MainViewModel.Instance.Show_Story = false;
		MainViewModel.Instance.Show_Frontend = false;
		if (sceneNo != Enums.SceneIDS.Intro)
		{
			MainViewModel.Instance.Show_IntroSequence = false;
		}
		MainViewModel.Instance.Show_ActionPoint = false;
		switch (sceneNo)
		{
		case Enums.SceneIDS.ActualMainGame:
			OnScreenText.Instance.initOST();
			MainViewModel.Instance.Show_InGame = true;
			MainViewModel.Instance.Show_MP_LoadingBlack = false;
			MainViewModel.Instance.Compass_Vis = ConfigSettings.Settings_Compass;
			TilemapManager.instance.ClearTilemap();
			GameMap.instance.clearSprites();
			MainViewModel.Instance.MapLowerEdgeMaskImage = MainViewModel.Instance.GameSprites[87];
			MainViewModel.Instance.HUDmain.SetupModeDependantUI();
			MainViewModel.Instance.HUDmain.RefGameUndoButton.IsEnabled = false;
			MainViewModel.Instance.HUDmain.SetRolloverSelected(0, "");
			MainViewModel.Instance.HUDmain.SetEnginePanelText(0, 0, force: true);
			MainViewModel.Instance.HUDmain.UpdateRollover();
			if (MainViewModel.Instance.HUDScenario != null)
			{
				MainViewModel.Instance.HUDScenario.IsEnabled = true;
				MainViewModel.Instance.HUDScenarioPopup.IsEnabled = true;
			}
			MainControls.instance.forceUIState(state: true);
			MainViewModel.Instance.RadarMargin = "0,0,103,8";
			MainViewModel.Instance.RadarPlusMargin = "0,0,94,135";
			MainViewModel.Instance.RadarMinusMargin = "0,0,94,125";
			if (MainViewModel.Instance.IsMapEditorMode)
			{
				MainViewModel.Instance.DefaultMapEditorUIGameAction();
				MainViewModel.Instance.HUDmain.RefGameInfoButton.IsEnabled = false;
				MainViewModel.Instance.HUDmain.RefButtonBuildModeBuildings.IsChecked = true;
				MainViewModel.Instance.HUDmain.RefButtonMEHeightControls.IsChecked = true;
				MainViewModel.Instance.HUDmain.SetupNewMEScreen(1, ignoreSetupCall: true);
				MainViewModel.Instance.HUDmain.SetEditorModeButtonVisibilityForSiegeThatMode(visible: true);
				MainViewModel.Instance.Compass_Margin = "0,50,10,0";
			}
			else
			{
				MainViewModel.Instance.DefaultGameUIGameAction();
				MainViewModel.Instance.HUDmain.ResetTutorialArrows();
				MainViewModel.Instance.HUDmain.RefGameInfoButton.IsEnabled = true;
				MainViewModel.Instance.Compass_Margin = "0,2,2,0";
			}
			MainControls.instance.setUIState(state: true);
			break;
		case Enums.SceneIDS.Story:
		{
			setInfoDisplayVisible(visible: false);
			ResizeStoryWindow();
			MainViewModel.Instance.Show_MapScreenMenuMode = false;
			MainViewModel.Instance.Show_MapScreenStoryMode = true;
			MainViewModel.Instance.Show_Story = true;
			Enums.eMusicIDS iD = Enums.eMusicIDS.MUSIC_TUNE_NARR1;
			if (GameData.Instance.game_type == 0)
			{
				int currentSelectedMission = FrontendMenus.CurrentSelectedMission;
				switch (currentSelectedMission)
				{
				case 17:
					iD = Enums.eMusicIDS.MUSIC_TUNE_MONK;
					break;
				case 20:
				case 21:
					iD = Enums.eMusicIDS.MUSIC_TUNE_CHOIR;
					break;
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
				case 10:
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 18:
				case 19:
					if (currentSelectedMission >> 1 << 1 != currentSelectedMission)
					{
						iD = Enums.eMusicIDS.MUSIC_TUNE_NARR2;
					}
					break;
				}
			}
			else if (new System.Random().Next(2) == 0)
			{
				iD = Enums.eMusicIDS.MUSIC_TUNE_NARR2;
			}
			SFXManager.instance.playMusic((int)iD);
			break;
		}
		case Enums.SceneIDS.Intro:
			MainViewModel.Instance.Intro_Sequence.Init();
			setInfoDisplayVisible(visible: false);
			break;
		case Enums.SceneIDS.FrontEnd:
			setInfoDisplayVisible(visible: false);
			FrontendMenus.OpenFrontEndMenus();
			if (!temp_intro_speech_played)
			{
				temp_intro_speech_played = true;
				SFXManager.instance.playIntroSpeech(ConfigSettings.Settings_UserName);
			}
			SFXManager.instance.playMusic(3);
			break;
		}
		currentScene = sceneNo;
	}

	private void ResizeStoryWindow()
	{
		int width = Screen.width;
		int height = Screen.height;
		int num = 1365;
		int num2 = 768;
		int num3 = 1920;
		int num4 = 1080;
		float a = (float)width / (float)num;
		float b = (float)height / (float)num2;
		float num5 = Mathf.Min(a, b);
		MainViewModel.Instance.BriefingViewboxWidth = (int)((float)num3 * num5);
		MainViewModel.Instance.BriefingViewboxHeight = (int)((float)num4 * num5);
	}

	public void DelayedSwitchToScene2()
	{
		DelayedSwitchToScene2Time = DateTime.UtcNow.AddSeconds(0.5);
	}

	private void OnApplicationQuit()
	{
		MinimumWindowSize.Reset();
	}

	public void ExitApp()
	{
		Application.Quit();
	}

	public void setInfoDisplayVisible(bool visible)
	{
		if (AFPSCounter.Instance != null)
		{
			AFPSCounter.Instance.deviceInfoCounter.Enabled = visible;
		}
	}

	public void MonitorScreenResolutions()
	{
		if (saveWindowSizeChange != DateTime.MinValue && saveWindowSizeChange < DateTime.UtcNow)
		{
			if (firstScreenChange)
			{
				firstScreenChange = false;
			}
			else
			{
				ConfigSettings.SaveSettings(onlyWhenAlreadyExists: true);
			}
			saveWindowSizeChange = DateTime.MinValue;
		}
		if (lastFullscreenMode != Screen.fullScreenMode || !screenModeSet)
		{
			if ((lastFullscreenMode == FullScreenMode.FullScreenWindow || lastFullscreenMode == FullScreenMode.ExclusiveFullScreen) && Screen.fullScreenMode == FullScreenMode.Windowed && ConfigSettings.Settings_LastWindowWidth > 0)
			{
				int num = ConfigSettings.Settings_LastWindowWidth;
				int num2 = ConfigSettings.Settings_LastWindowHeight;
				if (num < 1280)
				{
					num = 1280;
				}
				else if (num > Screen.mainWindowDisplayInfo.width)
				{
					num = Screen.mainWindowDisplayInfo.width;
				}
				if (num2 < 768)
				{
					num2 = 768;
				}
				else if (num2 > Screen.mainWindowDisplayInfo.height)
				{
					num2 = Screen.mainWindowDisplayInfo.height;
				}
				Screen.SetResolution(num, num2, fullscreen: false, 0);
				if (MainViewModel.viewModelLoaded)
				{
					MainViewModel.Instance.ScaleIngameUI(ConfigSettings.Settings_UIScale);
				}
			}
			if (Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen || Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
			{
				bool flag = false;
				if ((Screen.fullScreenMode == FullScreenMode.FullScreenWindow && ConfigSettings.Settings_LastFullscreenType == 0) || (Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen && ConfigSettings.Settings_LastFullscreenType == 1))
				{
					flag = true;
				}
				if (ConfigSettings.Settings_LastFullscreenWidth > -1 && (Screen.width != ConfigSettings.Settings_LastFullscreenWidth || Screen.height != ConfigSettings.Settings_LastFullscreenHeight || flag))
				{
					Screen.SetResolution(fullscreenMode: (ConfigSettings.Settings_LastFullscreenType != 0) ? FullScreenMode.FullScreenWindow : FullScreenMode.ExclusiveFullScreen, width: ConfigSettings.Settings_LastFullscreenWidth, height: ConfigSettings.Settings_LastFullscreenHeight, preferredRefreshRate: ConfigSettings.Settings_LastFullscreenRefresh);
				}
			}
			if (!screenModeSet)
			{
				if (ConfigSettings.Settings_LockCursor)
				{
					UnityEngine.Cursor.lockState = CursorLockMode.Confined;
				}
				else
				{
					UnityEngine.Cursor.lockState = CursorLockMode.None;
				}
			}
			screenModeSet = true;
			lastFullscreenMode = Screen.fullScreenMode;
			if (ConfigSettings.Settings_LastFullscreenRefresh > 0)
			{
				HUD_Options.SetVSync(ConfigSettings.Settings_Vsync);
			}
			else
			{
				Application.targetFrameRate = 300;
				QualitySettings.vSyncCount = 0;
			}
		}
		if (lastScreenWidth == Screen.width && lastScreenHeight == Screen.height)
		{
			return;
		}
		if (PerfectPixelWithZoom.instance != null && !PerfectPixelWithZoom.instance.CanUserExtraZoom())
		{
			PerfectPixelWithZoom.instance.limitZoomOnResChange();
		}
		if (lastScreenWidth >= 0)
		{
			if (MainViewModel.Instance.Show_HUD_Briefing)
			{
				MainViewModel.Instance.ResizeBriefingScreen();
			}
			if (MainViewModel.Instance.Show_Story)
			{
				ResizeStoryWindow();
			}
			if (MainViewModel.viewModelLoaded)
			{
				MainViewModel.Instance.ScaleIngameUI(ConfigSettings.Settings_UIScale);
			}
			if (MainViewModel.Instance.Show_Frontend)
			{
				MainViewModel.Instance.FrontEndMenu.UpdateFrontMenuPopupScale();
			}
			else if (MainViewModel.viewModelLoaded)
			{
				FrontendMenus.UpdateVideoScale();
			}
		}
		else
		{
			lastFullscreenMode = Screen.fullScreenMode;
		}
		lastScreenWidth = Screen.width;
		lastScreenHeight = Screen.height;
		if (Screen.fullScreenMode == FullScreenMode.Windowed)
		{
			ConfigSettings.Settings_LastWindowWidth = lastScreenWidth;
			ConfigSettings.Settings_LastWindowHeight = lastScreenHeight;
			saveWindowSizeChange = DateTime.UtcNow.AddSeconds(1.0);
		}
		else
		{
			if (Screen.fullScreenMode != FullScreenMode.FullScreenWindow && Screen.fullScreenMode != 0)
			{
				return;
			}
			ConfigSettings.Settings_LastFullscreenWidth = lastScreenWidth;
			ConfigSettings.Settings_LastFullscreenHeight = lastScreenHeight;
			ConfigSettings.Settings_LastFullscreenRefresh = Screen.currentResolution.refreshRate;
			if (ConfigSettings.Settings_LastFullscreenType == -1)
			{
				if (Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen)
				{
					ConfigSettings.Settings_LastFullscreenType = 0;
				}
				else
				{
					ConfigSettings.Settings_LastFullscreenType = 1;
				}
			}
			saveWindowSizeChange = DateTime.UtcNow.AddSeconds(1.0);
		}
	}

	public void SetNoesisKeyboardState(bool state)
	{
		noesisHasKeyboard = state;
		if (m_MainCamera == null)
		{
			m_MainCamera = Camera.main.transform.GetComponent<Camera>();
		}
		NGview = m_MainCamera.GetComponent<NoesisView>();
		NGview.EnableKeyboard = state;
	}
}
