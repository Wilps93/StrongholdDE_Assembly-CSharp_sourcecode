using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading;
using Noesis;
using NoesisApp;
using UnityEngine;

namespace Stronghold1DE;

public class MainViewModel : INotifyPropertyChanged
{
	private KeyTime _frontEnd_Row1_Step1 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 200));

	private KeyTime _frontEnd_Row1_Step2 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 220));

	private KeyTime _frontEnd_Row2_Step1 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 100));

	private KeyTime _frontEnd_Row2_Step2 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 300));

	private KeyTime _frontEnd_Row2_Step3 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 320));

	private KeyTime _frontEnd_Row3_Step1 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 200));

	private KeyTime _frontEnd_Row3_Step2 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 400));

	private KeyTime _frontEnd_Row3_Step3 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 420));

	private KeyTime _frontEnd_Row4_Step1 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 300));

	private KeyTime _frontEnd_Row4_Step2 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 500));

	private KeyTime _frontEnd_Row4_Step3 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 520));

	private KeyTime _frontEnd_Row5_Step1 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 400));

	private KeyTime _frontEnd_Row5_Step2 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 600));

	private KeyTime _frontEnd_Row5_Step3 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 620));

	private KeyTime _frontEnd_Row6_Step1 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 500));

	private KeyTime _frontEnd_Row6_Step2 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 700));

	private KeyTime _frontEnd_Row6_Step3 = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 720));

	private Visibility _mapEditorSetup160 = Visibility.Visible;

	private Visibility _mapEditorSetup200 = Visibility.Hidden;

	private Visibility _mapEditorSetup300 = Visibility.Hidden;

	private Visibility _mapEditorSetup400 = Visibility.Hidden;

	private Visibility _mapEditorSetupSiege = Visibility.Visible;

	private Visibility _mapEditorSetupInvasion = Visibility.Hidden;

	private Visibility _mapEditorSetupEco = Visibility.Hidden;

	private Visibility _mapEditorSetupFree = Visibility.Hidden;

	private Visibility _mapEditorSetupMulti = Visibility.Hidden;

	private Visibility _mapEditorSetupSiegeThat = Visibility.Hidden;

	private string _mapEditorTypeHelp = "";

	private static bool _show_MPJoiningLobby = false;

	private static bool _show_CreatingMPHost = false;

	private static bool _show_MPGameCreation = false;

	private static bool _show_MPIsHost = false;

	private static bool _show_MPIsNotHost = true;

	private static bool _show_MPFileList = false;

	private static bool _show_MPRadar = false;

	private static bool _MP_FileListEnabled = false;

	private static string _MP_PublicPrivateText = "";

	private static string _MP_LobbyChatWindow = "";

	private static bool _show_MPSettings = false;

	private static bool _show_MPColours = false;

	private static bool _show_MPSharing = false;

	private static string _MP_Settings_TechLevel = "";

	private static string _MP_Settings_StartingGoods = "";

	private static string _MP_Settings_StartingTroops = "";

	private static string _MP_Settings_Gold = "";

	private static bool _MP_Settings_Toggle_BulkVis = false;

	private static bool _MP_Settings_Toggle_FoodVis = false;

	private static bool _MP_Settings_Toggle_WeaponsVis = false;

	private static string _MP_Settings_Koth = "";

	private static string _MP_Settings_Peacetime = "";

	private static string _MP_Settings_Wall = "";

	private static string _MP_Settings_Alliances = "";

	private static string _MP_Settings_Troops = "";

	private static string _MP_Settings_Autotrading = "";

	private static string _MP_Settings_Autosave = "";

	private static string _MP_Settings_GameSpeed = "";

	private static bool _show_MPKothMap = false;

	private static bool _show_MPPeacetime = false;

	private static string _MP_Settings_Button = "";

	private static bool _show_MPRetrieveMapPanel = false;

	private static bool _show_MPRetrieveMapButton = false;

	private static bool _show_MPRetrievingMapMessage = false;

	private static string _MP_RetrieveMapName = "";

	private static bool _show_MP_LoadingBlack = false;

	private static bool _show_MP_LoadingButton = false;

	private static bool _Show_MP_LoadingWarning = false;

	private string _multiplayerFilter = "";

	private string _multiplayerShareCode = "";

	private string _multiplayerEnterShareCode = "";

	private Visibility _multiplayerFilterLabelVis = Visibility.Hidden;

	private Visibility _multiplayerFilterButtonVis = Visibility.Hidden;

	private string _MPCreateMaxPlayers = "8";

	private string _MPSettingHeight = "520";

	private ImageSource _radarStandaloneImage;

	private string _standaloneMissionText = "";

	private string _standaloneTitle = "";

	private string _standalonePrev = "";

	private string _standaloneNext = "";

	private string _standalonePrev2 = "";

	private string _standaloneNext2 = "";

	private string _standaloneDifficultyText = "";

	private string _standaloneAttackDefendText = "";

	private string _standaloneSiegeText = "";

	private int _standaloneSiegeMax = 1;

	private int _standaloneSiegeFreq = 1;

	private string _standaloneSiegePoints = "0";

	private Visibility _siegeThatHelpButtonVis = Visibility.Hidden;

	private Visibility _siegeThatHelpVis = Visibility.Hidden;

	private Visibility _freeBuildOptionsVis = Visibility.Hidden;

	private string _standaloneFreebuild_Gold_Text = "";

	private string _standaloneFreebuild_Food_Text = "";

	private string _standaloneFreebuild_Resources_Text = "";

	private string _standaloneFreebuild_Weapons_Text = "";

	private string _standaloneFreebuild_RandomEvents_Text = "";

	private string _standaloneFreebuild_Peacetime_Text = "";

	private string _standaloneFreebuild_InvasionsDifficulty_Text = "";

	private string _standaloneFreebuild_InvasionsDifficulty_Label = "";

	private string _standaloneFreebuild_Invasions_Text = "";

	private string _standaloneFilter = "";

	private Visibility _standaloneFilterLabelVis = Visibility.Hidden;

	private Visibility _standaloneFilterButtonVis = Visibility.Hidden;

	private static string _confirmationPanelHeight = "170";

	private static string _confirmationPanelWidth = "450";

	private static string _confirmationPanelWidth2 = "420";

	private static string _confirmationPanelHeightView = "206";

	private static string _confirmationPanelWidthView = "486";

	private static string _confirmationMessage = "";

	private Visibility _MO_SP_Score = Visibility.Visible;

	private Visibility _MO_Months = Visibility.Visible;

	private Visibility _MO_Troops = Visibility.Visible;

	private Visibility _MO_SiegeThat = Visibility.Visible;

	private Visibility _MO_DefeatVis = Visibility.Visible;

	private Visibility _MO_MP_Score = Visibility.Visible;

	private Visibility _MO_MP_Victory = Visibility.Visible;

	private Visibility _MO_MP_Defeat = Visibility.Visible;

	private string _MO_LevelPoints = "0";

	private string _MO_MonthText = "X";

	private string _MO_MonthPoints = "0";

	private string _MO_TroopsText = "X";

	private string _MO_TroopsPoints = "0";

	private string _MO_ScorePoints = "0";

	private string _MO_LastScorePoints = "0";

	private string _MO_SiegeThatName = "Siege That Name";

	private string _MO_ST_Attackers = "0";

	private string _MO_ST_Defenders = "0";

	private string _MO_ST_Castle = "0";

	private ImageSource _MO_MP_PlayersShields0;

	private ImageSource _MO_MP_PlayersShields1;

	private ImageSource _MO_MP_PlayersShields2;

	private ImageSource _MO_MP_PlayersShields3;

	private ImageSource _MO_MP_PlayersShields4;

	private ImageSource _MO_MP_PlayersShields5;

	private ImageSource _MO_MP_PlayersShields6;

	private ImageSource _MO_MP_PlayersShields7;

	private ImageSource _MO_MP_PlayersFear0;

	private ImageSource _MO_MP_PlayersFear1;

	private ImageSource _MO_MP_PlayersFear2;

	private ImageSource _MO_MP_PlayersFear3;

	private ImageSource _MO_MP_PlayersFear4;

	private ImageSource _MO_MP_PlayersFear5;

	private ImageSource _MO_MP_PlayersFear6;

	private ImageSource _MO_MP_PlayersFear7;

	private Thickness _rightclickMargin;

	private string _workshopUploadText = "";

	private static bool EnumTranslationsSetup = false;

	public static bool viewModelLoaded = false;

	private static MainViewModel instance = null;

	public MasterController GlobalUIRoot;

	public IngameUIScreens IngameUI;

	public IntroSequence Intro_Sequence;

	public FrontendMenus FrontEndMenu;

	public FRONT_StandaloneMission FRONTStandaloneMission;

	public FRONT_Multiplayer FRONTMultiplayer;

	public FRONT_EditorSetup FRONTEditorSetup;

	public MainHUD HUDRoot;

	public HUD_Main HUDmain;

	public HUD_Troops HUDTroopPanel;

	public HUD_Buildings HUDBuildingPanel;

	public HUD_Briefing HUDBriefingPanel;

	public HUD_Scenario HUDScenario;

	public HUD_LoadSaveRequester HUDLoadSaveRequester;

	public HUD_WorkshopUploader HUDWorkshopUploader;

	public HUD_Scenario_Popup HUDScenarioPopup;

	public HUD_IngameMenu HUDIngameMenu;

	public HUD_ConfirmationPopup HUDConfirmationPopup;

	public HUD_Tutorial HUDTutorial;

	public HUD_MPInviteWarning HUDMPInviteWarning;

	public HUD_MPConnectionIssue HUDMPConnectionIssue;

	public HUD_MPChatMessages HUDMPChatMessages;

	public HUD_MPChatPanel HUDMPChatPanel;

	public HUD_ScenarioSpecial HUDScenarioSpecial;

	public HUD_FreebuildMenu HUDFreebuildMenu;

	public HUD_ControlGroups HUDControlGroups;

	public HUD_Options HUDOptions;

	public HUD_MissionOver HUDMissionOver;

	public HUD_Help HUDHelp;

	public HUD_RightClick HUDRightClick;

	public HUD_Markers HUDMarkers;

	public HUD_Objectives HUDObjectives;

	public HUD_Goods HUDGoods;

	public FRONT_Credits FRONTCredits;

	public STORY_Map STORYMap;

	private double _panelX;

	private double _panelY;

	private static string _mousePosText = "";

	public int buildScreenID;

	public Enums.SceneIDS CurrentScreenNo;

	public int AppMode;

	public int SubMode;

	public bool WasInGranary;

	public bool FreezeMainControls;

	public bool RadarLoaded;

	public bool MainUILoaded;

	public bool ShowingScenario;

	public int MEScreenID = 1;

	public bool IsMapEditorMode = true;

	private int _MEMode = 1;

	public int MEBrushSize;

	public int MERulerMode = 2;

	public bool MEDeleteMode;

	public int MEFakeLocalPlayer = 1;

	public int BriefingMode = 1;

	public bool BriefingFromStory;

	public Enums.ScenarioViews ScenarioEditorMode = Enums.ScenarioViews.Main;

	public int ScenarioStartingGoodsType = 15;

	public int ScenarioAttackingForceType;

	public int ScenarioInvasionSizeType;

	public const string Default_RollOverText_Margin_Normal = "0,0,-20,130";

	public const string Default_RollOverText_Margin_Editor = "0,0,-20,154";

	public const string Default_RollOverText_Margin_Building = "0,0,-20,156";

	private static string _highlightedModeTitle = "Stronghold Definitive Edition";

	private static string _rollOverText = "Roll Over";

	private static string _rollOverText_AmountReq1 = "1";

	private static string _rollOverText_AmountGot1 = "(0)";

	private static string _rollOverText_AmountReq2 = "1";

	private static string _rollOverText_AmountGot2 = "(0)";

	private static ImageSource _rollOverText_GoodsImage1 = null;

	private static ImageSource _rollOverText_GoodsImage2 = null;

	private static bool _rolloverBuilding_TooltipVis = false;

	private static bool _rolloverBuilding_TooltipProducesVis = false;

	private static bool _rolloverBuilding_TooltipConsumesVis = false;

	private static ImageSource _rolloverBuilding_ProducesImage = null;

	private static ImageSource _rolloverBuilding_ProducesImage2 = null;

	private static ImageSource _rolloverBuilding_ConsumesImage = null;

	private static ImageSource _rolloverBuilding_ConsumesImage2 = null;

	private static string _rolloverBuilding_TooltipBody = "";

	private static string _rolloverBuilding_TooltipTitle = "";

	private static string _rollOverText_Margin = "382,0,0,130";

	private static string _debugText = "Debug text";

	private static string _debugText2 = "Debug text2";

	private static string _bookPopularityText = "";

	private static bool _popularityDecreasingVis = false;

	private static bool _popularityIncreasingVis = false;

	private static bool _bookGoldLarge = true;

	private static bool _bookGoldSmall = false;

	private static int _bookPopularityFontSize = 26;

	private static int _bookGoldLargeFontSize = 16;

	private static int _bookGoldSmallFontSize = 14;

	private static int _bookPopulationFontSize = 16;

	private static SolidColorBrush _bookPopularityColour = new SolidColorBrush(Noesis.Color.FromArgb(byte.MaxValue, 0, 102, 0));

	private static string _bookGoldText = "";

	private static string _bookPopulationText = "";

	private static SolidColorBrush _bookPopulationColour = new SolidColorBrush(Noesis.Color.FromArgb(byte.MaxValue, 0, 102, 0));

	private static ImageSource _highlightedModeImage = null;

	private static ImageSource _sketchImage = null;

	private static ImageSource _groomImage = null;

	private static ImageSource _brideImage = null;

	private float _sketchWidth;

	private float _sketchHeight;

	private float _buttonWidth = 100f;

	private Visibility _churchPanelRingsVis = Visibility.Hidden;

	private bool _show_IntroSequence;

	private bool _show_FrontMenus;

	private bool _show_CampaignMenu;

	private bool _show_EcoCampaignMenu;

	private bool _show_Extra1CampaignMenu;

	private bool _show_Extra2CampaignMenu;

	private bool _show_Extra3CampaignMenu;

	private bool _show_Extra4CampaignMenu;

	private bool _show_TrailCampaignMenu;

	private bool _show_Trail2CampaignMenu;

	private bool _show_ExtraEcoCampaignMenu;

	private bool _show_StandaloneSetup;

	private bool _show_MultiplayerSetup;

	private bool _show_MapScreenStoryMode;

	private bool _show_MapScreenMenuMode;

	private bool _show_Credits;

	private bool _show_Frontend = true;

	private bool _show_Frontend_MainMenu;

	private bool _show_Frontend_Combat = true;

	private bool _show_Frontend_Jewel;

	private bool _show_Frontend_Trails;

	private bool _show_Frontend_Eco;

	private bool _show_Frontend_Demo;

	private bool _show_Frontend_Wish;

	private bool _show_Frontend_Logo = true;

	private bool _show_Frontend_Feedback;

	private bool _show_Frontend_WishBack;

	private bool _show_Frontend_Controls_Selection;

	private bool _show_FrontEndCombat_Main_Help;

	private bool _show_FrontEndCombat_Jewel_Help;

	private bool _show_FrontEndEco_Main_Help;

	private bool _show_FrontEndEco_DLC_Help;

	private bool _show_FrontEndCombat_DLC_3_Help;

	private bool _show_FrontEndCombat_DLC_4_Help;

	private bool _show_Story;

	private bool _show_InGame;

	private bool _show_InGameUI;

	private bool _show_BlackOut;

	private bool _show_MapEditor;

	private bool _hide_Loading_Screen;

	private bool _show_HUD_Troops;

	private bool _show_HUD_Building;

	private bool _show_HUD_Book;

	private bool _show_HUD_Main = true;

	private bool _show_HUD_Briefing;

	private bool _show_HUD_Scenario;

	private bool _show_HUD_Scenario_Button;

	private bool _show_HUD_ScenarioSpecial;

	private bool _show_HUD_IngameMenu;

	private bool _show_HUD_Confirmation;

	private bool _show_HUD_LoadSaveRequester;

	private bool _show_HUD_ConfirmationMP;

	private bool _show_HUD_LoadSaveRequesterMP;

	private bool _show_HUD_WorkshopUploader;

	private bool _show_HUD_Tutorial;

	private bool _show_HUD_MPInviteWarning;

	private bool _show_HUD_MPConnectionIssue;

	private bool _show_HUD_MPChatMessages;

	private bool _show_ActionPoint;

	private bool _show_KeepEnclosed;

	private bool _compass_Vis;

	private string _compass_Margin = "0,2,2,0";

	private bool _show_HUD_FreebuildMenu;

	private bool _show_HUD_ControlGroups;

	private bool _show_HUD_Options;

	private bool _show_HUD_MissionOver;

	private bool _show_HUD_Help;

	private bool _show_HUD_RightClick;

	private bool _show_HUD_Objectives;

	private bool _show_HUD_Extras;

	private bool _show_HUD_Extras_Button_Objectves;

	private bool _show_HUD_Extras_Button_Freebuild;

	private bool _show_HUD_Goods;

	private bool _show_HUD_Goods_Normal;

	private bool _show_HUD_Goods_Trade;

	private bool _show_HUD_Goods_Button_Disabled;

	private Visibility _DEBUG_VISIBLE = Visibility.Hidden;

	private bool _guardStanceActive = true;

	private bool _defensiveStanceActive;

	private bool _aggressiveStanceActive;

	private static ImageSource _tradeGoodsImage = null;

	private static ImageSource _tradePrevGoodsImage = null;

	private static ImageSource _tradeNextGoodsImage = null;

	private static ImageSource _tradeGoldImage = null;

	private static string _tradeAutoSell = "";

	private static string _tradeAutoBuy = "";

	private static ImageSource _radarMapImage = null;

	private static string _radarMargin = "0,0,103,8";

	private static string _radarPlusMargin = "0,0,94,135";

	private static string _radarMinusMargin = "0,0,94,125";

	private static ImageSource _scribeHeadImage = null;

	private static ImageSource _missionOverImage = null;

	private static string _missionBriefingText = "";

	private static string _missionStrategyText = "";

	private static string _missionHint1Text = "";

	private static string _missionHint2Text = "";

	private static string _missionHint3Text = "";

	private static string _missionHint4Text = "";

	private static string _missionHint5Text = "";

	private static string _missionHint6Text = "";

	private static string _briefingDifficultyText = "Difficulty";

	private static string _briefingRolloverText = "";

	private static string _briefingTextMargin = "0,240,0,0";

	private static string _briefingStrategyTextMargin = "0,100,0,-340";

	private static string _briefingTimerMargin = "100,0,0,188";

	private static string _briefingStrategyS = "S";

	private static string _briefingStrategytrategy = "trategy";

	private static string _briefingHintsH = "H";

	private static string _briefingHintsints = "ints";

	private static string _briefingObjectivesO = "O";

	private static string _briefingObjectivesbjectives = "bjectives";

	private static string _briefingMissionTitle = "";

	private static string _objectiveTimerText = "Timer text";

	private static int _objectiveTimerWidth = 100;

	private static string _ingameMessageLoadButtonText = "Load";

	private static string _ingameMessageSaveButtonText = "Save";

	private static string _ingameMessageRestartButtonText = "Restart";

	private static string _ingameMessageQuitButtonText = "Quit";

	private static int _briefingViewboxWidth = 0;

	private static int _briefingViewboxHeight = 0;

	private static int _frontEndRequesterWidth = 1000;

	private static int _frontEndRequesterHeight = 600;

	private static int _frontEndOptionsWidth = 800;

	private static int _frontEndOptionsHeight = 600;

	private static int _frontEndHelpWidth = 820;

	private static int _frontEndHelpHeight = 500;

	private static int _frontEndConfirmationWidth = 820;

	private static int _frontEndConfirmationHeight = 500;

	private static bool FrontendLogoVis = false;

	private static bool _enterYourNameVis = false;

	private static string _enterYourName = "";

	private static bool _extra2Visible = false;

	private static bool _extra3Visible = false;

	private static bool _extra4Visible = false;

	private static bool _extra3NOTVisible = false;

	private static bool _extra4NOTVisible = false;

	private static double _demoFrontEndButtonFontSize = 44.0;

	private static double _FrontEndHomeFiresButtonFontSize = 44.0;

	private static double _frontEndButtonLineHeight = 30.0;

	private static Visibility _showTutorialHelpText = Visibility.Hidden;

	private static string _frontEndButtonMargin = "82,0,0,34";

	private static string _frontEndHomeFiresButtonMargin = "82,0,0,34";

	private static string _frontEndButtonDLCMargin = "82,0,0,34";

	private static string _frontEndWishlistButtonMargin = "0,630,0,0";

	private static string _frontEndVideoMargin = "0,0";

	private static ImageSource _briefingHelpImage = null;

	private static ImageSource _mainHelpImage = null;

	private string _tutorialHeading = "";

	private string _tutorialBody = "";

	private string _tutorialButton1Text = "";

	private string _tutorialButton2Text = "";

	private bool _tutorialButton1Visible;

	private bool _tutorialButton2Visible;

	private static string _loadSaveFileName = "";

	private static string _loadSaveFilter = "";

	private static string _buttonLoadSaveActionText = "";

	private static string _loadSave_FolderText = "";

	private static ImageSource _radarRequesterImage = null;

	private static bool _show_RequesterRadar = false;

	private static bool _saveSiegeThatLockVisible = false;

	private static bool _saveHideQuicksaveVisible = false;

	private static Visibility _loadSaveFilterLabelVis = Visibility.Visible;

	private static Visibility _loadSaveFilterButtonVis = Visibility.Visible;

	private static int _loadSaveDepthSorting = 0;

	private static Visibility _mapLowerEdgeMaskVisible = Visibility.Hidden;

	private static string _mapLowerEdgeMaskHeight = "129";

	public static ImageSource _mapLowerEdgeMaskImage = null;

	private static string _UIScaleValueWidth = "1920";

	private static string _UIScaleValueHeight = "1080";

	public static int iUIScaleValueWidth = 1920;

	public static int iUIScaleValueHeight = 1920;

	private static string _scenarioEditorButtonText = "Scenario Editor";

	private static string _scenarioStartingMonthText = "XXX";

	private static string _scenarioStartingYearText = "1066";

	private static string _scenarioAdjustStartingMonthText = "XXX";

	private static string _scenarioAdjustStartingYearText = "1066";

	private static string _scenarioStartingPopText = "XXX";

	private static string _scenarioStartingSpecialGoldText = "XXX";

	private static string _scenarioStartingGoldText = "XXX";

	private static string _scenarioStartingPitchText = "XXX";

	private static string _scenarioStartingSpecialRations = "XXX";

	private static string _scenarioStartingSpecialTax = "XXX";

	private static Visibility _scenarioStartingSpecialText = Visibility.Collapsed;

	private static string _scenarioAdjustedStartingGoodsText = "123 XXX";

	private static string _scenarioAdjustedAttackingForcesText = "123 XXX";

	private static int _scenarioAdjustedStartingGoodsMax = 50000;

	private static int _scenarioAdjustedStartingGoodsTickFreq = 1000;

	private static int _scenarioAdjustedStartingAttackingForceMax = 100;

	private static int _scenarioAdjustedStartingAttackingForceFreq = 10;

	private static int _scenarioInvasionSizeMax = 100;

	private static int _scenarioInvasionSizeFreq = 10;

	private static string _scenarioInvasionSizeText = "123 XXX";

	private static string _scenarioEventActionTitle = "title";

	private static string _scenarioEventActionMessage = "message";

	private static string _scenarioTradeTextSize = "16";

	private static string _scenarioTradeTextHeight = "24";

	private static string _scenarioEditMessageText = "";

	private static Visibility _scenarioMessageAltTextIVisibility = Visibility.Visible;

	private static string _scenarioAltANSIMessage = "";

	private static string _scenarioAltUNICODEMessage = "";

	private static bool _scenarioBuildingTogglesVis = false;

	private static Visibility _scenarioNormalUIVisibility = Visibility.Visible;

	private static Visibility _scenarioSiegeUIVisibility = Visibility.Collapsed;

	private static Visibility _scenarioNewEventMessageVisible = Visibility.Visible;

	private static Visibility _scenarioNewInvasionVisible = Visibility.Visible;

	private static Visibility _scenarioCommonBackVisible = Visibility.Hidden;

	private static Visibility _scenarioCommonOKVisible = Visibility.Hidden;

	private static Visibility _scenarioCommonDeleteVisible = Visibility.Hidden;

	private static bool _scenarioCommonEditTeamsVisible = true;

	private static string _scenarioMessageMonthText = "XXX";

	private static string _scenarioMessageYearText = "1234";

	private static string _scenarioInvasionMonthText = "XXX";

	private static string _scenarioInvasionYearText = "1234";

	private static string _scenarioInvasionTotalTroopsText = "123";

	private static string _scenarioInvasionRepeatText = "123";

	private static string _scenarioEventMonthText = "XXX";

	private static string _scenarioEventYearText = "1234";

	private static string _actionRepeatMonthsText = "1234";

	private static string _actionRepeatText = "1234";

	private static string _actionValueText = "1234";

	private static int _actionValueMax = 10;

	private static int _actionValueMin = 1;

	private static int _actionValueFreq = 1;

	private static string _actionValueNameText = "";

	private static string _actionValue2Text = "1234";

	private static int _actionValue2Max = 10;

	private static int _actionValue2Min = 1;

	private static int _actionValue2Freq = 1;

	private static string _actionValue2NameText = "";

	private static Visibility _scenarioEventActionMessageVisible = Visibility.Hidden;

	private static Visibility _scenarioEventActionValueVisible = Visibility.Hidden;

	private static Visibility _scenarioEventActionValue2Visible = Visibility.Hidden;

	private static Visibility _scenarioEventActionRepeatVisible = Visibility.Hidden;

	private static string _scenarioEventConditionTitle = "1234";

	private static string _conditionValueText = "1234";

	private static string _conditionValueNameText = "";

	private static int _conditionValueMax = 10;

	private static int _conditionValueMin = 1;

	private static int _conditionValueFreq = 1;

	private static string _scenarionEventConditionToggleText = "1234";

	private static Visibility _scenarionEventConditionToggleVisible = Visibility.Hidden;

	private static Visibility _scenarionEventConditionToggleColourVisible = Visibility.Hidden;

	private static Visibility _scenarioEventConditionValueVisible = Visibility.Hidden;

	private static Visibility _scenarionEventConditionOnOffVisible = Visibility.Hidden;

	private static string _scenarionEventConditionOnOffText = "1234";

	private static string _scenarionEventConditionAndOrText = "1234";

	private static SolidColorBrush _scenarionEventConditionToggleColour = new SolidColorBrush(Noesis.Color.FromArgb(byte.MaxValue, 0, 102, 0));

	public string _buttonMapSettingsText = "";

	public string _scenarioPopup_GameTimeText = "";

	public bool _butBordScenEdit160HL;

	public bool _butBordScenEdit200HL;

	public bool _butBordScenEdit300HL;

	public bool _butBordScenEdit400HL;

	public string _buttonScenarioEditSPMP = "";

	public bool _buttonScenarioEditSinglePlayerVis;

	public bool _buttonScenarioEditMultiPlayerVis;

	public bool _butBordScenEditSPSiege;

	public bool _butBordScenEditSPInvasion;

	public bool _butBordScenEditSPEco;

	public bool _butBordScenEditSPFreeBuild;

	public bool _butBordScenEditMultiPlayerKoth;

	private int[] diffSHStoredGoods = new int[25];

	private bool[] diffSHAutoTradeGoods = new bool[25];

	private long[] diffSHGoodsPrices = new long[25];

	private int[] diffSHTroopCounts = new int[18];

	private static string _buildingTitle = "Title";

	private const string BuildingTitle_Margin_Default = "20,24,0,0";

	private static string _buildingTitle_Margin = "20,24,0,0";

	private static double _buildingTitleFontSize = 32.0;

	private static string _buildingLine1Text = "Line1";

	private static string _buildingLine2Text = "Line2";

	private static string _buildingLine3Text = "Line3 is a longer line of text that may well indeed wrap around on the panel";

	private static int _granaryFoodBarWidth = 0;

	private static string _inGranaryUnitsOfFoodText = "sfsf ";

	private static string _inGranaryMonthsOfFoodText = "3 months supply";

	private static string _inGranaryTypesOfFoodText = "2 Food types eaten";

	private static string _inGranaryTypesPopFoodText = "0";

	private static string _inGranaryRationsPopText = "0";

	private static string _inGranaryRationLevelText = "";

	private static string _inInnBarrelsOfAleText = "23";

	private static string _inInnFlagonsOfAleText = "Flagons";

	private static string _inInnWorkingInnsText = "Working";

	private static string _inInnPopularityText = "0";

	private static string _inInnCoverageText = "Coverage";

	private static string _inInnNextLevelText = "Next Level";

	private static string _inKeepIncomeText = "income";

	private static string _inKeepPopulationText = "42";

	private static string _inKeepTaxRateText = "tax rate";

	private static string _inKeepTaxPopText = "12";

	private static int _inKeepSliderPos = 0;

	private static string _buildingHPText = "1600/1600";

	private static int _buildingRepairHPWidth = 30;

	private static string _troopCostText = "0";

	private static string _troopNameCostText = "0";

	private static string _troopNameCostText2 = "";

	private static string _troopShiftCostText = "";

	private static string _troopCtrlCostText = "";

	private static string _availablePeasantText = "";

	private static string _workshopProducingText = "prod";

	private static string _workshopProducingNextText = "prodN";

	private static string _barracksHorsesAvailableText = "";

	private static string _weddingGossipText = "";

	private static string _groomNameText = "";

	private static string _brideNameText = "";

	private static string _tradeGoodsAmountText = "";

	private static string _buyText = "";

	private static string _sellText = "";

	private static string _buySellFontSize = "22";

	private static string _buyPriceText = "230";

	private static string _sellPriceText = "911";

	private static string _tradeErrorText = "TradeErrorMessage";

	private static string _playerNameText = "PlayerName";

	private static string _playerMottoText = "PlayerMotto";

	private static string _popReportFoodText = "0";

	private static string _popReportTaxText = "1";

	private static string _popReportCrowdingText = "-2";

	private static string _popReportFearFactorText = "0";

	private static string _popReportReligionText = "0";

	private static string _popReportAleText = "0";

	private static string _popReportEventsText = "0";

	private static string _popReportTotalText = "0";

	private static string _popReportFairsText = "0";

	private static string _popReportMarriageText = "0";

	private static string _popReportJesterText = "0";

	private static string _popReportPlagueText = "0";

	private static string _popReportWolvesText = "0";

	private static string _popReportBanditsText = "0";

	private static string _popReportFireText = "0";

	private static string _ffReportBadBuildingsText = "0";

	private static string _ffReportGoodBuildingsText = "0";

	private static string _ffReportFearFactorText = "0";

	private static string _ffReportNextLevelText = "Next Level";

	private static string _ffReportNextLevelAmountText = "0";

	private static string _ffReportCommentaryText = "";

	private static string _ffReportEfficiencyAmountText = "0";

	private static string _graphReportLeftScaleNo2Text = "0";

	private static string _graphReportLeftScaleNo1Text = "0";

	private static string _graphReportBottomScaleNo1Text = "0";

	private static string _graphReportBottomScaleNo2Text = "0";

	private static string _graphReportBottomScaleNo3Text = "0";

	private static string _graphReportBottomScaleNo4Text = "0";

	private static string _graphReportBottomScaleNo5Text = "0";

	private static string _graphReportBottomScaleNo6Text = "0";

	private static string _graphReportPathDataString = "M0,0 L200,50, 250,0, 300,100";

	private static string _armyReportFFBoostText = "FF Boost";

	private static string _armyReportTotalTroopsText = "tot troops";

	private static string _relReportTotalPriestsText = "0";

	private static string _relReportBlessedPeopleText = "0";

	private static string _relReportPopEffectText = "-1";

	private static double _relReportPopEffectTextLabelWidth = 0.0;

	private static double _WGT_RelReportLabelWidth = 0.0;

	private static string _relReportNextLevelText = "0";

	private static string _relReportNextLevel2Text = "0";

	private static string _relReportTypeDemandedText = "Demand";

	private static string _relReportDemandEffectText = "-1";

	private static double _chimpNameTextFontSize = 32.0;

	private static string _chimpNameText = "Name";

	private static string _chimpTypeText = "Type";

	private static string _chimpWorkText = "Work";

	private static string _chimpCommentText = "Comment asfsdf asfsdf asf asdfasf  asdfsdf asdfsd vv xx xx  asdas";

	private static string _troopsPanelRollover = "Rollover";

	private static string _troopsPanelRollover_AmountReq1 = "";

	private static string _troopsPanelRollover_AmountGot1 = "";

	private static ImageSource _troopsPanelRollover_GoodsImage1 = null;

	private static string _ammoLeft = "";

	private static string _cowAmmoLeft = "";

	private static bool _gotMarketVis = false;

	public bool CanShowWorkers;

	private bool _show_BarracksBows1 = true;

	private bool _show_BarracksBows2;

	private bool _show_BarracksBows3;

	private double _show_BarracksBowsOpaque = 1.0;

	private bool _show_BarracksSpears1 = true;

	private bool _show_BarracksSpears2;

	private bool _show_BarracksSpears3;

	private double _show_BarracksSpearsOpaque = 1.0;

	private bool _show_BarracksMaces1 = true;

	private bool _show_BarracksMaces2;

	private bool _show_BarracksMaces3;

	private double _show_BarracksMacesOpaque = 1.0;

	private bool _show_BarracksXBows1 = true;

	private bool _show_BarracksXBows2;

	private bool _show_BarracksXBows3;

	private double _show_BarracksXBowsOpaque = 1.0;

	private bool _show_BarracksLeatherArmour1 = true;

	private bool _show_BarracksLeatherArmour2;

	private bool _show_BarracksLeatherArmour3;

	private double _show_BarracksLeatherArmourOpaque = 1.0;

	private bool _show_BarracksPikes1 = true;

	private bool _show_BarracksPikes2;

	private bool _show_BarracksPikes3;

	private double _show_BarracksPikesOpaque = 1.0;

	private bool _show_BarracksSwords1 = true;

	private bool _show_BarracksSwords2;

	private bool _show_BarracksSwords3;

	private double _show_BarracksSwordsOpaque = 1.0;

	private bool _show_BarracksArmour1 = true;

	private bool _show_BarracksArmour2;

	private bool _show_BarracksArmour3;

	private double _show_BarracksArmourOpaque = 1.0;

	private bool _show_BarracksHorses1 = true;

	private bool _show_BarracksHorses2;

	private bool _show_BarracksHorses3;

	private double _show_BarracksHorsesOpaque = 1.0;

	private bool _show_BarracksArcher;

	private bool _show_BarracksSpearman;

	private bool _show_BarracksMaceman;

	private bool _show_BarracksXbowman;

	private bool _show_BarracksPikeman;

	private bool _show_BarracksSwordsman;

	private bool _show_BarracksKnight;

	private int mainUIMode;

	private static bool _preLoadBlankVis = true;

	private int[] foodNotEdible = new int[4];

	private bool _HUD_Markers_Vis;

	public int lastTroopsAmountToMake;

	public int lastTroopsAmountToMakex5;

	public int lastTroopsAmountToMakeMax;

	public string lastTroopBuildOver = "";

	public Enums.eChimps lastTroopBuildChimp = Enums.eChimps.CHIMP_NUM_TYPES;

	private int xThreadStruct;

	private int xThreadSiegeStruct;

	private string xThreadString = "";

	private bool xThreadClearTroopRollover;

	private int previousTradePanel = -1;

	private bool preBriefingWasPaused;

	private bool ShowAllHudVis = true;

	private EngineInterface.ScenarioOverview scenarioOverview;

	private int[] lowStartingGoodsPreset = new int[9] { 40, 0, 0, 0, 10, 10, 0, 0, 50 };

	private int[] medStartingGoodsPreset = new int[9] { 120, 20, 5, 5, 50, 50, 20, 20, 200 };

	private int[] highStartingGoodsPreset = new int[9] { 500, 100, 25, 25, 200, 200, 50, 50, 1000 };

	private int[] GoodsCurve200 = new int[50]
	{
		0, 1, 2, 3, 4, 5, 6, 7, 8, 10,
		12, 14, 16, 18, 20, 22, 24, 26, 28, 30,
		32, 34, 36, 38, 40, 42, 44, 46, 48, 50,
		55, 60, 65, 70, 75, 80, 85, 90, 95, 100,
		110, 120, 130, 140, 150, 160, 170, 180, 190, 200
	};

	private int[] GoodsCurve1000 = new int[50]
	{
		0, 1, 2, 3, 4, 5, 6, 8, 10, 12,
		14, 16, 18, 20, 22, 24, 26, 28, 30, 35,
		40, 45, 50, 60, 70, 80, 100, 120, 140, 160,
		180, 200, 220, 240, 260, 280, 300, 350, 400, 450,
		500, 550, 600, 650, 700, 750, 800, 850, 900, 1000
	};

	private int[] GoodsCurve5000 = new int[50]
	{
		0, 1, 2, 4, 6, 8, 10, 12, 14, 16,
		18, 20, 25, 30, 35, 40, 50, 60, 70, 80,
		90, 100, 120, 140, 160, 180, 200, 250, 300, 350,
		400, 450, 500, 550, 600, 700, 800, 900, 1000, 1200,
		1400, 1600, 1800, 2000, 2500, 3000, 3500, 4000, 4500, 5000
	};

	private string _MPConnectionIssueText = "";

	private string _MPConnectionIssueButtonText = "";

	private bool _MPConectionIssueButtonVisible;

	private int _MPChat_Size = 135;

	public string _MPChatMessageText = "";

	public bool _MPChatVisible;

	public bool UIVisible = true;

	private Texture2D radarTexture2D;

	private bool _freebuildSliderVis;

	private int _freebuildSizeMax = 1;

	private int _freebuildSizeFreq = 1;

	private string _freebuildSizeText = "1";

	private int _freebuildInvasionSizeMax = 1;

	private int _freebuildInvasionSizeFreq = 1;

	private string _freebuildInvasionSizeText = "1";

	private Visibility _optionsApplyVisible = Visibility.Hidden;

	private Visibility _optionsScaleApplyVisible = Visibility.Hidden;

	private Visibility _optionsPushEnabled = Visibility.Hidden;

	private Visibility _optionsPushDisabled = Visibility.Hidden;

	private Visibility _optionsSH1RTS = Visibility.Hidden;

	private Visibility _optionsDERTS = Visibility.Hidden;

	private Visibility _optionsWheelSH1 = Visibility.Hidden;

	private Visibility _optionsWheelZoom = Visibility.Hidden;

	private Visibility _optionsHotKeyPanelVis = Visibility.Hidden;

	private Visibility _optionsHotKeySelectVis = Visibility.Hidden;

	private Visibility _optionsHotKeyChangeVis = Visibility.Hidden;

	private Visibility _optionsCenteringSH1 = Visibility.Hidden;

	private Visibility _optionsCenteringModern = Visibility.Hidden;

	private string _masterVolumeValue = "1";

	private string _musicVolumeValue = "1";

	private string _sfxVolumeValue = "1";

	private string _speechVolumeValue = "1";

	private string _unitSpeechVolumeValue = "1";

	private string _scrollSpeedValue = "";

	private string _optionsHotKeyTitle = "Function";

	private string _optionsHotKey1 = "1";

	private string _optionsHotKey2 = "2";

	private string _gameSpeedValue = "40";

	private Visibility _optionsGameSpeedVis = Visibility.Hidden;

	private string _optionsHotKeyCurrentKey = "2";

	private string _optionsHotKeyNewKey = "2";

	private string _optionsHotKeyWarning = "2";

	private string _optionsFreeBuildingsText = "";

	private Visibility _optionsAchievementsDisabledVis = Visibility.Hidden;

	private Visibility _optionsInGameCheatsVis = Visibility.Hidden;

	private Visibility _optionsPlayerColourVis = Visibility.Hidden;

	private int startedMission = -1;

	private bool startedMissionPre6;

	private Uri[] SpeechPaths;

	public MediaElement SpeechNarrativeME;

	public MediaElement SpeechCharacterME;

	public MediaElement SpeechHeadsME;

	public MediaElement SpeechVideoME;

	public static List<AnImage> GameImages = new List<AnImage>();

	public List<AVideo> GameVideos = new List<AVideo>();

	public List<ImageSource> GameSprites = new List<ImageSource>();

	public static List<byte[]> GameImageData = new List<byte[]>();

	public int[,] GameSpriteDims;

	private int _spriteWidth1;

	private int _spriteHeight1;

	private int _spriteWidth2;

	private int _spriteHeight2;

	private int _spriteWidth3;

	private int _spriteHeight3;

	private int _spriteWidth4;

	private int _spriteHeight4;

	public static bool[] imageProcessingNeeded = new bool[52]
	{
		false, true, false, false, false, false, false, false, false, false,
		true, true, true, true, true, true, true, true, true, true,
		true, true, true, true, true, true, true, true, true, true,
		true, true, true, true, true, true, true, true, true, false,
		false, false, false, true, true, true, true, true, true, true,
		true, true
	};

	public static string[] imageFileNames = new string[51]
	{
		"", "Assets/GUI/Images/StrongholdDE_Logo.png", "", "", "Assets/GUI/Images/RatBig.png", "Assets/GUI/Images/SnakeBig.png", "Assets/GUI/Images/PigBig.png", "Assets/GUI/Images/WolfBig.png", "Assets/GUI/Images/CampaignMap.png", "Assets/GUI/Images/CampaignMap01.png",
		"Assets/GUI/Images/CampaignMap02.png", "Assets/GUI/Images/CampaignMap03.png", "Assets/GUI/Images/CampaignMap04.png", "Assets/GUI/Images/CampaignMap05.png", "Assets/GUI/Images/CampaignMap06.png", "Assets/GUI/Images/CampaignMap07.png", "Assets/GUI/Images/CampaignMap08.png", "Assets/GUI/Images/CampaignMap09.png", "Assets/GUI/Images/CampaignMap10.png", "Assets/GUI/Images/CampaignMap11.png",
		"Assets/GUI/Images/CampaignMap12.png", "Assets/GUI/Images/CampaignMap13.png", "Assets/GUI/Images/CampaignMap14.png", "Assets/GUI/Images/CampaignMap15.png", "Assets/GUI/Images/CampaignMap16.png", "Assets/GUI/Images/CampaignMap17.png", "Assets/GUI/Images/CampaignMap18.png", "Assets/GUI/Images/CampaignMap19.png", "Assets/GUI/Images/CampaignMap20.png", "Assets/GUI/Images/StrongholdSH_Logo.png",
		"Assets/GUI/Images/credits1.png", "Assets/GUI/Images/credits2.png", "Assets/GUI/Images/credits3.png", "Assets/GUI/Images/credits4.png", "Assets/GUI/Images/credits5.png", "Assets/GUI/Images/credits6.png", "Assets/GUI/Images/credits7.png", "Assets/GUI/Images/credits8.png", "", "",
		"", "", "", "", "", "", "", "", "", "",
		""
	};

	private int StoryChapter = 1;

	private int StoryPhase = 1;

	private int StoryStage = -1;

	private bool StoryActive;

	public int MapStage;

	private string StoryTypeRunning = "";

	private bool firepitOn = true;

	private int TalkingHeadsStyle;

	private int TalkingHeadsStage;

	private int TalkingHeadsCentreStyle;

	private int StoryVideoStage;

	private int VideoSpeech;

	public Uri[] VideoPaths;

	private Uri[] CurrentVideos;

	private bool[] CurrentVideoWait;

	private int[] CurrentSpeech;

	private string[] CurrentText;

	private string[] NameText;

	public STORY_Heads StoryHeads;

	public STORY_Video StoryVideo;

	public Storyboard StoryTitleIntroAnimation;

	public Storyboard StoryNarrativeAnimation;

	public Storyboard StoryCharacterAnimation;

	public Storyboard StoryMapAnimation;

	public Storyboard StoryFlagMoveAnimation;

	public Storyboard HeadsFirepitInAnimation;

	public Storyboard HeadsNoFirepitInAnimation;

	public Storyboard HeadsFirepitOutAnimation;

	public Storyboard Head1InAnimation;

	public Storyboard Head1SwapAnimation;

	public Storyboard Head1OutAnimation;

	public Storyboard Head2InAnimation;

	public Storyboard Head2SwapAnimation;

	public Storyboard Head2OutAnimation;

	public Storyboard Text1InAnimation;

	public Storyboard Text1OutAnimation;

	public Storyboard Text2InAnimation;

	public Storyboard Text2OutAnimation;

	public Storyboard StoryVideoInAnimation;

	public Storyboard StoryVideoOutAnimation;

	public Storyboard VideoTextInAnimation;

	public Storyboard VideoTextOutAnimation;

	public Noesis.Grid StoryTopHeadGrid;

	public Noesis.Grid StoryBottomHeadGrid;

	public Noesis.Grid StoryFirePitGrid;

	public MediaElement StoryHeadsFirepitME;

	public MediaElement StoryHeadsTopHead;

	public MediaElement StoryHeadsBottomHead;

	public MediaElement StoryVideoME;

	public TextBlock Head1Text;

	public TextBlock Head2Text;

	public TextBlock Name1Text;

	public TextBlock Name2Text;

	public TextBlock VideoText;

	public MediaElement RadarME;

	private bool _show_StoryTitle;

	private bool _show_StoryNarrative;

	private bool _show_StoryHeads;

	private bool _show_StoryCharacter;

	private bool _show_StoryMap;

	private bool _show_StoryVideo;

	private bool _show_Story_DEBUG;

	private static string _storyTitle = "Title text";

	private static string _storyNarration = "Narration text";

	private static string _storyCharacter = "Character text";

	private bool _showRat;

	private bool _showSnake;

	private bool _showPig;

	private bool _showWolf;

	private float _opacityRat = 1f;

	private float _opacitySnake = 1f;

	private float _opacityPig = 1f;

	private float _opacityWolf = 1f;

	private static ImageSource _storyImage;

	private static ImageSource _storyImage1;

	private static ImageSource _storyImage2;

	private static ImageSource _storyImage3;

	private static ImageSource _storyImage4;

	private static ImageSource _storyImage5;

	private static ImageSource _storyImage6;

	private static ImageSource _storyImage7;

	private static ImageSource _storyImage8;

	private static ImageSource _storyImage9;

	private static ImageSource _storyImage10;

	private static ImageSource _storyImage11;

	private static ImageSource _storyImage12;

	private static ImageSource _storyImage13;

	private static ImageSource _storyImage14;

	private static ImageSource _storyImage15;

	private static ImageSource _storyImage16;

	private static ImageSource _storyImage17;

	private static ImageSource _storyImage18;

	private static ImageSource _storyImage19;

	private static ImageSource _storyImage20;

	private static Noesis.Color _countyColor1 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color _countyColor2 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color _countyColor3 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color _countyColor4 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color _countyColor5 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color _countyColor6 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color _countyColor7 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color _countyColor8 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color _countyColor9 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color _countyColor10 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color _countyColor11 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color _countyColor12 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color _countyColor13 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color _countyColor14 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color _countyColor15 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color _countyColor16 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color _countyColor17 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color _countyColor18 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color _countyColor19 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color _countyColor20 = Noesis.Color.FromArgb(0, 0, 0, 0);

	private int _flagXPos = -346;

	private int _flagYPos = 270;

	private int _flagXPos2 = -346;

	private int _flagYPos2 = 270;

	private static string _fadeCounty = "";

	private static Visibility _storyHeadsBorderVisible = Visibility.Hidden;

	private static Visibility _storyFirepitVisible = Visibility.Hidden;

	private bool _show_NarrativeBackground_Default;

	private bool _show_NarrativeBackground_Extra1A;

	private bool _show_NarrativeBackground_Extra1B;

	private bool _show_NarrativeBackground_Extra2A;

	private bool _show_NarrativeBackground_Extra2B;

	private bool _show_NarrativeBackground_Extra3A;

	private bool _show_NarrativeBackground_Extra3B;

	private bool _show_NarrativeBackground_Extra4A;

	private bool _show_NarrativeBackground_Extra4B;

	private bool _show_NarrativeBackground_Extra5A;

	private bool _show_NarrativeBackground_Extra5B;

	private bool _OST_Date_Vis;

	private string _OST_Date_Text = "";

	private bool _OST_Game_Paused_Vis;

	private bool _OST_Game_Paused_Vis_Set;

	private bool _OST_Game_Paused_Vis_Allowed = true;

	private string _OST_Game_Paused_Text = "";

	private bool _OST_Keep_Message_Vis;

	private string _OST_Keep_Message_Text = "";

	private bool _OST_Feedback_1_Vis;

	private string _OST_Feedback_1_Text = "";

	private bool _OST_Message_Bar_Vis;

	private string _OST_Message_Bar_Text = "";

	private string _OST_Message_Bar_Margin = "0,0,100,160";

	private bool _OST_Framerate_Vis;

	private string _OST_Framerate_Text = "";

	private string _OST_Framerate_Margin = "0,7,240,0";

	private bool _OST_GameSpeed_Vis;

	private string _OST_GameSpeed_Text = "";

	private string _OST_GameSpeed_Margin = "0,45,240,-6";

	private SolidColorBrush _OST_GameSpeed_Colour = new SolidColorBrush(Noesis.Color.FromArgb(byte.MaxValue, 239, 243, 102));

	public SolidColorBrush _OST_PingLow_Colour = new SolidColorBrush(Noesis.Color.FromArgb(byte.MaxValue, 0, byte.MaxValue, 0));

	public SolidColorBrush _OST_PingMid_Colour = new SolidColorBrush(Noesis.Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0));

	public SolidColorBrush _OST_PingHigh_Colour = new SolidColorBrush(Noesis.Color.FromArgb(byte.MaxValue, byte.MaxValue, 0, 0));

	private bool _OST_Starting_goods_Vis;

	private bool _OST_Time_Until_Vis;

	private string _OST_Time_Until_Text = "";

	private int _OST_Time_Until_Width;

	private bool _OST_LeftStack_Vis;

	private bool _OST_PeopleLeft_Vis;

	private string _OST_PeopleLeft_Text = "";

	private string _OST_StructsLeft_Text = "";

	private string _OST_TreesLeft_Text = "";

	private string _OST_RocksLeft_Text = "";

	private string _OST_TribesLeft_Text = "";

	private bool _OST_WhoOwns_Vis;

	private string _OST_WhoOwns_Text = "";

	private bool _OST_KOTH_Vis;

	private bool _OST_Ping_Vis;

	private bool _OST_MissionFinished_Vis;

	private string _OST_MissionFinished_Text = "";

	private bool _OST_Cart_Vis;

	private KeyTime _cartDuration = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 720));

	private int _cartDistance = 1920;

	public KeyTime FrontEnd_Row1_Step1
	{
		get
		{
			return _frontEnd_Row1_Step1;
		}
		set
		{
			_frontEnd_Row1_Step1 = value;
			NotifyPropertyChanged("FrontEnd_Row1_Step1");
		}
	}

	public KeyTime FrontEnd_Row1_Step2
	{
		get
		{
			return _frontEnd_Row1_Step2;
		}
		set
		{
			_frontEnd_Row1_Step2 = value;
			NotifyPropertyChanged("FrontEnd_Row1_Step2");
		}
	}

	public KeyTime FrontEnd_Row2_Step1
	{
		get
		{
			return _frontEnd_Row2_Step1;
		}
		set
		{
			_frontEnd_Row2_Step1 = value;
			NotifyPropertyChanged("FrontEnd_Row2_Step1");
		}
	}

	public KeyTime FrontEnd_Row2_Step2
	{
		get
		{
			return _frontEnd_Row2_Step2;
		}
		set
		{
			_frontEnd_Row2_Step2 = value;
			NotifyPropertyChanged("FrontEnd_Row2_Step2");
		}
	}

	public KeyTime FrontEnd_Row2_Step3
	{
		get
		{
			return _frontEnd_Row2_Step3;
		}
		set
		{
			_frontEnd_Row2_Step3 = value;
			NotifyPropertyChanged("FrontEnd_Row2_Step3");
		}
	}

	public KeyTime FrontEnd_Row3_Step1
	{
		get
		{
			return _frontEnd_Row3_Step1;
		}
		set
		{
			_frontEnd_Row3_Step1 = value;
			NotifyPropertyChanged("FrontEnd_Row3_Step1");
		}
	}

	public KeyTime FrontEnd_Row3_Step2
	{
		get
		{
			return _frontEnd_Row3_Step2;
		}
		set
		{
			_frontEnd_Row3_Step2 = value;
			NotifyPropertyChanged("FrontEnd_Row3_Step2");
		}
	}

	public KeyTime FrontEnd_Row3_Step3
	{
		get
		{
			return _frontEnd_Row3_Step3;
		}
		set
		{
			_frontEnd_Row3_Step3 = value;
			NotifyPropertyChanged("FrontEnd_Row3_Step3");
		}
	}

	public KeyTime FrontEnd_Row4_Step1
	{
		get
		{
			return _frontEnd_Row4_Step1;
		}
		set
		{
			_frontEnd_Row4_Step1 = value;
			NotifyPropertyChanged("FrontEnd_Row4_Step1");
		}
	}

	public KeyTime FrontEnd_Row4_Step2
	{
		get
		{
			return _frontEnd_Row4_Step2;
		}
		set
		{
			_frontEnd_Row4_Step2 = value;
			NotifyPropertyChanged("FrontEnd_Row4_Step2");
		}
	}

	public KeyTime FrontEnd_Row4_Step3
	{
		get
		{
			return _frontEnd_Row4_Step3;
		}
		set
		{
			_frontEnd_Row4_Step3 = value;
			NotifyPropertyChanged("FrontEnd_Row4_Step3");
		}
	}

	public KeyTime FrontEnd_Row5_Step1
	{
		get
		{
			return _frontEnd_Row5_Step1;
		}
		set
		{
			_frontEnd_Row5_Step1 = value;
			NotifyPropertyChanged("FrontEnd_Row5_Step1");
		}
	}

	public KeyTime FrontEnd_Row5_Step2
	{
		get
		{
			return _frontEnd_Row5_Step2;
		}
		set
		{
			_frontEnd_Row5_Step2 = value;
			NotifyPropertyChanged("FrontEnd_Row5_Step2");
		}
	}

	public KeyTime FrontEnd_Row5_Step3
	{
		get
		{
			return _frontEnd_Row5_Step3;
		}
		set
		{
			_frontEnd_Row5_Step3 = value;
			NotifyPropertyChanged("FrontEnd_Row5_Step3");
		}
	}

	public KeyTime FrontEnd_Row6_Step1
	{
		get
		{
			return _frontEnd_Row6_Step1;
		}
		set
		{
			_frontEnd_Row6_Step1 = value;
			NotifyPropertyChanged("FrontEnd_Row6_Step1");
		}
	}

	public KeyTime FrontEnd_Row6_Step2
	{
		get
		{
			return _frontEnd_Row6_Step2;
		}
		set
		{
			_frontEnd_Row6_Step2 = value;
			NotifyPropertyChanged("FrontEnd_Row6_Step2");
		}
	}

	public KeyTime FrontEnd_Row6_Step3
	{
		get
		{
			return _frontEnd_Row6_Step3;
		}
		set
		{
			_frontEnd_Row6_Step3 = value;
			NotifyPropertyChanged("FrontEnd_Row6_Step3");
		}
	}

	public Visibility MapEditorSetup160
	{
		get
		{
			return _mapEditorSetup160;
		}
		set
		{
			if (_mapEditorSetup160 != value)
			{
				_mapEditorSetup160 = value;
				NotifyPropertyChanged("MapEditorSetup160");
			}
		}
	}

	public Visibility MapEditorSetup200
	{
		get
		{
			return _mapEditorSetup200;
		}
		set
		{
			if (_mapEditorSetup200 != value)
			{
				_mapEditorSetup200 = value;
				NotifyPropertyChanged("MapEditorSetup200");
			}
		}
	}

	public Visibility MapEditorSetup300
	{
		get
		{
			return _mapEditorSetup300;
		}
		set
		{
			if (_mapEditorSetup300 != value)
			{
				_mapEditorSetup300 = value;
				NotifyPropertyChanged("MapEditorSetup300");
			}
		}
	}

	public Visibility MapEditorSetup400
	{
		get
		{
			return _mapEditorSetup400;
		}
		set
		{
			if (_mapEditorSetup400 != value)
			{
				_mapEditorSetup400 = value;
				NotifyPropertyChanged("MapEditorSetup400");
			}
		}
	}

	public Visibility MapEditorSetupSiege
	{
		get
		{
			return _mapEditorSetupSiege;
		}
		set
		{
			if (_mapEditorSetupSiege != value)
			{
				_mapEditorSetupSiege = value;
				NotifyPropertyChanged("MapEditorSetupSiege");
			}
		}
	}

	public Visibility MapEditorSetupInvasion
	{
		get
		{
			return _mapEditorSetupInvasion;
		}
		set
		{
			if (_mapEditorSetupInvasion != value)
			{
				_mapEditorSetupInvasion = value;
				NotifyPropertyChanged("MapEditorSetupInvasion");
			}
		}
	}

	public Visibility MapEditorSetupEco
	{
		get
		{
			return _mapEditorSetupEco;
		}
		set
		{
			if (_mapEditorSetupEco != value)
			{
				_mapEditorSetupEco = value;
				NotifyPropertyChanged("MapEditorSetupEco");
			}
		}
	}

	public Visibility MapEditorSetupFree
	{
		get
		{
			return _mapEditorSetupFree;
		}
		set
		{
			if (_mapEditorSetupFree != value)
			{
				_mapEditorSetupFree = value;
				NotifyPropertyChanged("MapEditorSetupFree");
			}
		}
	}

	public Visibility MapEditorSetupMulti
	{
		get
		{
			return _mapEditorSetupMulti;
		}
		set
		{
			if (_mapEditorSetupMulti != value)
			{
				_mapEditorSetupMulti = value;
				NotifyPropertyChanged("MapEditorSetupMulti");
			}
		}
	}

	public Visibility MapEditorSetupSiegeThat
	{
		get
		{
			return _mapEditorSetupSiegeThat;
		}
		set
		{
			if (_mapEditorSetupSiegeThat != value)
			{
				_mapEditorSetupSiegeThat = value;
				NotifyPropertyChanged("MapEditorSetupSiegeThat");
			}
		}
	}

	public string MapEditorTypeHelp
	{
		get
		{
			return _mapEditorTypeHelp;
		}
		set
		{
			if (_mapEditorTypeHelp != value)
			{
				_mapEditorTypeHelp = value;
				NotifyPropertyChanged("MapEditorTypeHelp");
			}
		}
	}

	public bool Show_MPJoiningLobby
	{
		get
		{
			return _show_MPJoiningLobby;
		}
		set
		{
			if (_show_MPJoiningLobby != value)
			{
				_show_MPJoiningLobby = value;
				NotifyPropertyChanged("Show_MPJoiningLobby");
			}
		}
	}

	public bool Show_CreatingMPHost
	{
		get
		{
			return _show_CreatingMPHost;
		}
		set
		{
			if (_show_CreatingMPHost != value)
			{
				_show_CreatingMPHost = value;
				NotifyPropertyChanged("Show_CreatingMPHost");
			}
		}
	}

	public bool Show_MPGameCreation
	{
		get
		{
			return _show_MPGameCreation;
		}
		set
		{
			if (_show_MPGameCreation != value)
			{
				_show_MPGameCreation = value;
				NotifyPropertyChanged("Show_MPGameCreation");
			}
		}
	}

	public bool Show_MPIsHost
	{
		get
		{
			return _show_MPIsHost;
		}
		set
		{
			if (_show_MPIsHost != value)
			{
				_show_MPIsHost = value;
				NotifyPropertyChanged("Show_MPIsHost");
				Show_MPIsNotHost = !value;
			}
		}
	}

	public bool Show_MPIsNotHost
	{
		get
		{
			return _show_MPIsNotHost;
		}
		set
		{
			if (_show_MPIsNotHost != value)
			{
				_show_MPIsNotHost = value;
				NotifyPropertyChanged("Show_MPIsNotHost");
			}
		}
	}

	public bool Show_MPFileList
	{
		get
		{
			return _show_MPFileList;
		}
		set
		{
			if (_show_MPFileList != value)
			{
				_show_MPFileList = value;
				NotifyPropertyChanged("Show_MPFileList");
			}
		}
	}

	public bool Show_MPRadar
	{
		get
		{
			return _show_MPRadar;
		}
		set
		{
			if (_show_MPRadar != value)
			{
				_show_MPRadar = value;
				NotifyPropertyChanged("Show_MPRadar");
			}
		}
	}

	public bool MP_FileListEnabled
	{
		get
		{
			return _MP_FileListEnabled;
		}
		set
		{
			if (_MP_FileListEnabled != value)
			{
				_MP_FileListEnabled = value;
				NotifyPropertyChanged("MP_FileListEnabled");
			}
		}
	}

	public string MP_PublicPrivateText
	{
		get
		{
			return _MP_PublicPrivateText;
		}
		set
		{
			if (_MP_PublicPrivateText != value)
			{
				_MP_PublicPrivateText = value;
				NotifyPropertyChanged("MP_PublicPrivateText");
			}
		}
	}

	public string MP_LobbyChatWindow
	{
		get
		{
			return _MP_LobbyChatWindow;
		}
		set
		{
			if (_MP_LobbyChatWindow != value)
			{
				_MP_LobbyChatWindow = value;
				NotifyPropertyChanged("MP_LobbyChatWindow");
			}
		}
	}

	public bool Show_MPSettings
	{
		get
		{
			return _show_MPSettings;
		}
		set
		{
			if (_show_MPSettings != value)
			{
				_show_MPSettings = value;
				NotifyPropertyChanged("Show_MPSettings");
			}
		}
	}

	public bool Show_MPColours
	{
		get
		{
			return _show_MPColours;
		}
		set
		{
			if (_show_MPColours != value)
			{
				_show_MPColours = value;
				NotifyPropertyChanged("Show_MPColours");
			}
		}
	}

	public bool Show_MPSharing
	{
		get
		{
			return _show_MPSharing;
		}
		set
		{
			if (_show_MPSharing != value)
			{
				_show_MPSharing = value;
				NotifyPropertyChanged("Show_MPSharing");
			}
		}
	}

	public string MP_Settings_TechLevel
	{
		get
		{
			return _MP_Settings_TechLevel;
		}
		set
		{
			if (_MP_Settings_TechLevel != value)
			{
				_MP_Settings_TechLevel = value;
				NotifyPropertyChanged("MP_Settings_TechLevel");
			}
		}
	}

	public string MP_Settings_StartingGoods
	{
		get
		{
			return _MP_Settings_StartingGoods;
		}
		set
		{
			if (_MP_Settings_StartingGoods != value)
			{
				_MP_Settings_StartingGoods = value;
				NotifyPropertyChanged("MP_Settings_StartingGoods");
			}
		}
	}

	public string MP_Settings_StartingTroops
	{
		get
		{
			return _MP_Settings_StartingTroops;
		}
		set
		{
			if (_MP_Settings_StartingTroops != value)
			{
				_MP_Settings_StartingTroops = value;
				NotifyPropertyChanged("MP_Settings_StartingTroops");
			}
		}
	}

	public string MP_Settings_Gold
	{
		get
		{
			return _MP_Settings_Gold;
		}
		set
		{
			if (_MP_Settings_Gold != value)
			{
				_MP_Settings_Gold = value;
				NotifyPropertyChanged("MP_Settings_Gold");
			}
		}
	}

	public bool MP_Settings_Toggle_BulkVis
	{
		get
		{
			return _MP_Settings_Toggle_BulkVis;
		}
		set
		{
			if (_MP_Settings_Toggle_BulkVis != value)
			{
				_MP_Settings_Toggle_BulkVis = value;
				NotifyPropertyChanged("MP_Settings_Toggle_BulkVis");
			}
		}
	}

	public bool MP_Settings_Toggle_FoodVis
	{
		get
		{
			return _MP_Settings_Toggle_FoodVis;
		}
		set
		{
			if (_MP_Settings_Toggle_FoodVis != value)
			{
				_MP_Settings_Toggle_FoodVis = value;
				NotifyPropertyChanged("MP_Settings_Toggle_FoodVis");
			}
		}
	}

	public bool MP_Settings_Toggle_WeaponsVis
	{
		get
		{
			return _MP_Settings_Toggle_WeaponsVis;
		}
		set
		{
			if (_MP_Settings_Toggle_WeaponsVis != value)
			{
				_MP_Settings_Toggle_WeaponsVis = value;
				NotifyPropertyChanged("MP_Settings_Toggle_WeaponsVis");
			}
		}
	}

	public string MP_Settings_Koth
	{
		get
		{
			return _MP_Settings_Koth;
		}
		set
		{
			if (_MP_Settings_Koth != value)
			{
				_MP_Settings_Koth = value;
				NotifyPropertyChanged("MP_Settings_Koth");
			}
		}
	}

	public string MP_Settings_Peacetime
	{
		get
		{
			return _MP_Settings_Peacetime;
		}
		set
		{
			if (_MP_Settings_Peacetime != value)
			{
				_MP_Settings_Peacetime = value;
				NotifyPropertyChanged("MP_Settings_Peacetime");
			}
		}
	}

	public string MP_Settings_Wall
	{
		get
		{
			return _MP_Settings_Wall;
		}
		set
		{
			if (_MP_Settings_Wall != value)
			{
				_MP_Settings_Wall = value;
				NotifyPropertyChanged("MP_Settings_Wall");
			}
		}
	}

	public string MP_Settings_Alliances
	{
		get
		{
			return _MP_Settings_Alliances;
		}
		set
		{
			if (_MP_Settings_Alliances != value)
			{
				_MP_Settings_Alliances = value;
				NotifyPropertyChanged("MP_Settings_Alliances");
			}
		}
	}

	public string MP_Settings_Troops
	{
		get
		{
			return _MP_Settings_Troops;
		}
		set
		{
			if (_MP_Settings_Troops != value)
			{
				_MP_Settings_Troops = value;
				NotifyPropertyChanged("MP_Settings_Troops");
			}
		}
	}

	public string MP_Settings_Autotrading
	{
		get
		{
			return _MP_Settings_Autotrading;
		}
		set
		{
			if (_MP_Settings_Autotrading != value)
			{
				_MP_Settings_Autotrading = value;
				NotifyPropertyChanged("MP_Settings_Autotrading");
			}
		}
	}

	public string MP_Settings_Autosave
	{
		get
		{
			return _MP_Settings_Autosave;
		}
		set
		{
			if (_MP_Settings_Autosave != value)
			{
				_MP_Settings_Autosave = value;
				NotifyPropertyChanged("MP_Settings_Autosave");
			}
		}
	}

	public string MP_Settings_GameSpeed
	{
		get
		{
			return _MP_Settings_GameSpeed;
		}
		set
		{
			if (_MP_Settings_GameSpeed != value)
			{
				_MP_Settings_GameSpeed = value;
				NotifyPropertyChanged("MP_Settings_GameSpeed");
			}
		}
	}

	public bool Show_MPKothMap
	{
		get
		{
			return _show_MPKothMap;
		}
		set
		{
			if (_show_MPKothMap != value)
			{
				_show_MPKothMap = value;
				NotifyPropertyChanged("Show_MPKothMap");
			}
		}
	}

	public bool Show_MPPeacetime
	{
		get
		{
			return _show_MPPeacetime;
		}
		set
		{
			if (_show_MPPeacetime != value)
			{
				_show_MPPeacetime = value;
				NotifyPropertyChanged("Show_MPPeacetime");
			}
		}
	}

	public string MP_Settings_Button
	{
		get
		{
			return _MP_Settings_Button;
		}
		set
		{
			if (_MP_Settings_Button != value)
			{
				_MP_Settings_Button = value;
				NotifyPropertyChanged("MP_Settings_Button");
			}
		}
	}

	public bool Show_MPRetrieveMapPanel
	{
		get
		{
			return _show_MPRetrieveMapPanel;
		}
		set
		{
			if (_show_MPRetrieveMapPanel != value)
			{
				_show_MPRetrieveMapPanel = value;
				NotifyPropertyChanged("Show_MPRetrieveMapPanel");
			}
		}
	}

	public bool Show_MPRetrieveMapButton
	{
		get
		{
			return _show_MPRetrieveMapButton;
		}
		set
		{
			if (_show_MPRetrieveMapButton != value)
			{
				_show_MPRetrieveMapButton = value;
				NotifyPropertyChanged("Show_MPRetrieveMapButton");
			}
		}
	}

	public bool Show_MPRetrievingMapMessage
	{
		get
		{
			return _show_MPRetrievingMapMessage;
		}
		set
		{
			if (_show_MPRetrievingMapMessage != value)
			{
				_show_MPRetrievingMapMessage = value;
				NotifyPropertyChanged("Show_MPRetrievingMapMessage");
			}
		}
	}

	public string MP_RetrieveMapName
	{
		get
		{
			return _MP_RetrieveMapName;
		}
		set
		{
			if (_MP_RetrieveMapName != value)
			{
				_MP_RetrieveMapName = value;
				NotifyPropertyChanged("MP_RetrieveMapName");
			}
		}
	}

	public bool Show_MP_LoadingBlack
	{
		get
		{
			return _show_MP_LoadingBlack;
		}
		set
		{
			if (_show_MP_LoadingBlack != value)
			{
				_show_MP_LoadingBlack = value;
				NotifyPropertyChanged("Show_MP_LoadingBlack");
				if (value)
				{
					Show_MP_LoadingButton = false;
					Show_MP_LoadingWarning = false;
					Director.instance.DelayShowDisconnect();
				}
			}
		}
	}

	public bool Show_MP_LoadingButton
	{
		get
		{
			return _show_MP_LoadingButton;
		}
		set
		{
			if (_show_MP_LoadingButton != value)
			{
				_show_MP_LoadingButton = value;
				NotifyPropertyChanged("Show_MP_LoadingButton");
			}
		}
	}

	public bool Show_MP_LoadingWarning
	{
		get
		{
			return _Show_MP_LoadingWarning;
		}
		set
		{
			if (_Show_MP_LoadingWarning != value)
			{
				_Show_MP_LoadingWarning = value;
				NotifyPropertyChanged("Show_MP_LoadingWarning");
			}
		}
	}

	public string MultiplayerFilter
	{
		get
		{
			return _multiplayerFilter;
		}
		set
		{
			if (_multiplayerFilter != value)
			{
				_multiplayerFilter = value;
				NotifyPropertyChanged("MultiplayerFilter");
			}
		}
	}

	public string MultiplayerShareCode
	{
		get
		{
			return _multiplayerShareCode;
		}
		set
		{
			if (_multiplayerShareCode != value)
			{
				_multiplayerShareCode = value;
				NotifyPropertyChanged("MultiplayerShareCode");
			}
		}
	}

	public string MultiplayerEnterShareCode
	{
		get
		{
			return _multiplayerEnterShareCode;
		}
		set
		{
			if (_multiplayerEnterShareCode != value)
			{
				_multiplayerEnterShareCode = value;
				NotifyPropertyChanged("MultiplayerEnterShareCode");
			}
		}
	}

	public string MPCreateMaxPlayers
	{
		get
		{
			return _MPCreateMaxPlayers;
		}
		set
		{
			if (_MPCreateMaxPlayers != value)
			{
				_MPCreateMaxPlayers = value;
				NotifyPropertyChanged("MPCreateMaxPlayers");
			}
		}
	}

	public string MPSettingHeight
	{
		get
		{
			return _MPSettingHeight;
		}
		set
		{
			if (_MPSettingHeight != value)
			{
				_MPSettingHeight = value;
				NotifyPropertyChanged("MPSettingHeight");
			}
		}
	}

	public Visibility MultiplayerFilterLabelVis
	{
		get
		{
			return _multiplayerFilterLabelVis;
		}
		set
		{
			if (_multiplayerFilterLabelVis != value)
			{
				_multiplayerFilterLabelVis = value;
				NotifyPropertyChanged("MultiplayerFilterLabelVis");
			}
		}
	}

	public Visibility MultiplayerFilterButtonVis
	{
		get
		{
			return _multiplayerFilterButtonVis;
		}
		set
		{
			if (_multiplayerFilterButtonVis != value)
			{
				_multiplayerFilterButtonVis = value;
				NotifyPropertyChanged("MultiplayerFilterButtonVis");
			}
		}
	}

	public ObservableCollection<string> SiegeAttackForces { get; private set; }

	public ImageSource RadarStandaloneImage
	{
		get
		{
			return _radarStandaloneImage;
		}
		set
		{
			_radarStandaloneImage = value;
			NotifyPropertyChanged("RadarStandaloneImage");
		}
	}

	public string StandaloneMissionText
	{
		get
		{
			return _standaloneMissionText;
		}
		set
		{
			_standaloneMissionText = value;
			NotifyPropertyChanged("StandaloneMissionText");
		}
	}

	public string StandaloneTitle
	{
		get
		{
			return _standaloneTitle;
		}
		set
		{
			_standaloneTitle = value;
			NotifyPropertyChanged("StandaloneTitle");
		}
	}

	public string StandalonePrev
	{
		get
		{
			return _standalonePrev;
		}
		set
		{
			_standalonePrev = value;
			NotifyPropertyChanged("StandalonePrev");
		}
	}

	public string StandaloneNext
	{
		get
		{
			return _standaloneNext;
		}
		set
		{
			_standaloneNext = value;
			NotifyPropertyChanged("StandaloneNext");
		}
	}

	public string StandalonePrev2
	{
		get
		{
			return _standalonePrev2;
		}
		set
		{
			_standalonePrev2 = value;
			NotifyPropertyChanged("StandalonePrev2");
		}
	}

	public string StandaloneNext2
	{
		get
		{
			return _standaloneNext2;
		}
		set
		{
			_standaloneNext2 = value;
			NotifyPropertyChanged("StandaloneNext2");
		}
	}

	public string StandaloneDifficultyText
	{
		get
		{
			return _standaloneAttackDefendText;
		}
		set
		{
			_standaloneAttackDefendText = value;
			NotifyPropertyChanged("StandaloneDifficultyText");
		}
	}

	public string StandaloneAttackDefendText
	{
		get
		{
			return _standaloneDifficultyText;
		}
		set
		{
			_standaloneDifficultyText = value;
			NotifyPropertyChanged("StandaloneAttackDefendText");
		}
	}

	public string StandaloneSiegeText
	{
		get
		{
			return _standaloneSiegeText;
		}
		set
		{
			if (_standaloneSiegeText != value)
			{
				_standaloneSiegeText = value;
				NotifyPropertyChanged("StandaloneSiegeText");
			}
		}
	}

	public int StandaloneSiegeMax
	{
		get
		{
			return _standaloneSiegeMax;
		}
		set
		{
			if (_standaloneSiegeMax != value)
			{
				_standaloneSiegeMax = value;
				NotifyPropertyChanged("StandaloneSiegeMax");
			}
		}
	}

	public int StandaloneSiegeFreq
	{
		get
		{
			return _standaloneSiegeFreq;
		}
		set
		{
			if (_standaloneSiegeFreq != value)
			{
				_standaloneSiegeFreq = value;
				NotifyPropertyChanged("StandaloneSiegeFreq");
			}
		}
	}

	public string StandaloneSiegePoints
	{
		get
		{
			return _standaloneSiegePoints;
		}
		set
		{
			if (_standaloneSiegePoints != value)
			{
				_standaloneSiegePoints = value;
				NotifyPropertyChanged("StandaloneSiegePoints");
			}
		}
	}

	public Visibility SiegeThatHelpButtonVis
	{
		get
		{
			return _siegeThatHelpButtonVis;
		}
		set
		{
			if (_siegeThatHelpButtonVis != value)
			{
				_siegeThatHelpButtonVis = value;
				NotifyPropertyChanged("SiegeThatHelpButtonVis");
			}
		}
	}

	public Visibility SiegeThatHelpVis
	{
		get
		{
			return _siegeThatHelpVis;
		}
		set
		{
			if (_siegeThatHelpVis != value)
			{
				_siegeThatHelpVis = value;
				NotifyPropertyChanged("SiegeThatHelpVis");
			}
		}
	}

	public Visibility FreeBuildOptionsVis
	{
		get
		{
			return _freeBuildOptionsVis;
		}
		set
		{
			if (_freeBuildOptionsVis != value)
			{
				_freeBuildOptionsVis = value;
				NotifyPropertyChanged("FreeBuildOptionsVis");
			}
		}
	}

	public string StandaloneFreebuild_Gold_Text
	{
		get
		{
			return _standaloneFreebuild_Gold_Text;
		}
		set
		{
			if (_standaloneFreebuild_Gold_Text != value)
			{
				_standaloneFreebuild_Gold_Text = value;
				NotifyPropertyChanged("StandaloneFreebuild_Gold_Text");
			}
		}
	}

	public string StandaloneFreebuild_Food_Text
	{
		get
		{
			return _standaloneFreebuild_Food_Text;
		}
		set
		{
			if (_standaloneFreebuild_Food_Text != value)
			{
				_standaloneFreebuild_Food_Text = value;
				NotifyPropertyChanged("StandaloneFreebuild_Food_Text");
			}
		}
	}

	public string StandaloneFreebuild_Resources_Text
	{
		get
		{
			return _standaloneFreebuild_Resources_Text;
		}
		set
		{
			if (_standaloneFreebuild_Resources_Text != value)
			{
				_standaloneFreebuild_Resources_Text = value;
				NotifyPropertyChanged("StandaloneFreebuild_Resources_Text");
			}
		}
	}

	public string StandaloneFreebuild_Weapons_Text
	{
		get
		{
			return _standaloneFreebuild_Weapons_Text;
		}
		set
		{
			if (_standaloneFreebuild_Weapons_Text != value)
			{
				_standaloneFreebuild_Weapons_Text = value;
				NotifyPropertyChanged("StandaloneFreebuild_Weapons_Text");
			}
		}
	}

	public string StandaloneFreebuild_RandomEvents_Text
	{
		get
		{
			return _standaloneFreebuild_RandomEvents_Text;
		}
		set
		{
			if (_standaloneFreebuild_RandomEvents_Text != value)
			{
				_standaloneFreebuild_RandomEvents_Text = value;
				NotifyPropertyChanged("StandaloneFreebuild_RandomEvents_Text");
			}
		}
	}

	public string StandaloneFreebuild_Invasions_Text
	{
		get
		{
			return _standaloneFreebuild_Invasions_Text;
		}
		set
		{
			if (_standaloneFreebuild_Invasions_Text != value)
			{
				_standaloneFreebuild_Invasions_Text = value;
				NotifyPropertyChanged("StandaloneFreebuild_Invasions_Text");
			}
		}
	}

	public string StandaloneFreebuild_InvasionsDifficulty_Text
	{
		get
		{
			return _standaloneFreebuild_InvasionsDifficulty_Text;
		}
		set
		{
			if (_standaloneFreebuild_InvasionsDifficulty_Text != value)
			{
				_standaloneFreebuild_InvasionsDifficulty_Text = value;
				NotifyPropertyChanged("StandaloneFreebuild_InvasionsDifficulty_Text");
			}
		}
	}

	public string StandaloneFreebuild_InvasionsDifficulty_Label
	{
		get
		{
			return _standaloneFreebuild_InvasionsDifficulty_Label;
		}
		set
		{
			if (_standaloneFreebuild_InvasionsDifficulty_Label != value)
			{
				_standaloneFreebuild_InvasionsDifficulty_Label = value;
				NotifyPropertyChanged("StandaloneFreebuild_InvasionsDifficulty_Label");
			}
		}
	}

	public string StandaloneFreebuild_Peacetime_Text
	{
		get
		{
			return _standaloneFreebuild_Peacetime_Text;
		}
		set
		{
			if (_standaloneFreebuild_Peacetime_Text != value)
			{
				_standaloneFreebuild_Peacetime_Text = value;
				NotifyPropertyChanged("StandaloneFreebuild_Peacetime_Text");
			}
		}
	}

	public string StandaloneFilter
	{
		get
		{
			return _standaloneFilter;
		}
		set
		{
			if (_standaloneFilter != value)
			{
				_standaloneFilter = value;
				NotifyPropertyChanged("StandaloneFilter");
			}
		}
	}

	public Visibility StandaloneFilterLabelVis
	{
		get
		{
			return _standaloneFilterLabelVis;
		}
		set
		{
			if (_standaloneFilterLabelVis != value)
			{
				_standaloneFilterLabelVis = value;
				NotifyPropertyChanged("StandaloneFilterLabelVis");
			}
		}
	}

	public Visibility StandaloneFilterButtonVis
	{
		get
		{
			return _standaloneFilterButtonVis;
		}
		set
		{
			if (_standaloneFilterButtonVis != value)
			{
				_standaloneFilterButtonVis = value;
				NotifyPropertyChanged("StandaloneFilterButtonVis");
			}
		}
	}

	public string ConfirmationPanelHeight
	{
		get
		{
			return _confirmationPanelHeight;
		}
		set
		{
			if (_confirmationPanelHeight != value)
			{
				_confirmationPanelHeight = value;
				NotifyPropertyChanged("ConfirmationPanelHeight");
			}
		}
	}

	public string ConfirmationPanelWidth
	{
		get
		{
			return _confirmationPanelWidth;
		}
		set
		{
			if (_confirmationPanelWidth != value)
			{
				_confirmationPanelWidth = value;
				NotifyPropertyChanged("ConfirmationPanelWidth");
			}
		}
	}

	public string ConfirmationPanelWidth2
	{
		get
		{
			return _confirmationPanelWidth2;
		}
		set
		{
			if (_confirmationPanelWidth2 != value)
			{
				_confirmationPanelWidth2 = value;
				NotifyPropertyChanged("ConfirmationPanelWidth2");
			}
		}
	}

	public string ConfirmationPanelHeightView
	{
		get
		{
			return _confirmationPanelHeightView;
		}
		set
		{
			if (_confirmationPanelHeightView != value)
			{
				_confirmationPanelHeightView = value;
				NotifyPropertyChanged("ConfirmationPanelHeightView");
			}
		}
	}

	public string ConfirmationPanelWidthView
	{
		get
		{
			return _confirmationPanelWidthView;
		}
		set
		{
			if (_confirmationPanelWidthView != value)
			{
				_confirmationPanelWidthView = value;
				NotifyPropertyChanged("ConfirmationPanelWidthView");
			}
		}
	}

	public string ConfirmationMessage
	{
		get
		{
			return _confirmationMessage;
		}
		set
		{
			if (_confirmationMessage != value)
			{
				_confirmationMessage = value;
				NotifyPropertyChanged("ConfirmationMessage");
			}
		}
	}

	public ObservableCollection<string> MO_OtherPointsText { get; private set; }

	public ObservableCollection<string> MO_OtherPoints { get; private set; }

	public ObservableCollection<Visibility> MO_OtherPointsVisible { get; private set; }

	public ObservableCollection<Visibility> MO_MP_PlayersVisible { get; private set; }

	public ObservableCollection<string> MO_MP_Players1Values { get; private set; }

	public ObservableCollection<string> MO_MP_Players2Values { get; private set; }

	public ObservableCollection<string> MO_MP_Players3Values { get; private set; }

	public ObservableCollection<string> MO_MP_Players4Values { get; private set; }

	public ObservableCollection<string> MO_MP_Players5Values { get; private set; }

	public ObservableCollection<string> MO_MP_Players6Values { get; private set; }

	public ObservableCollection<string> MO_MP_Players7Values { get; private set; }

	public ObservableCollection<string> MO_MP_Players8Values { get; private set; }

	public ObservableCollection<Visibility> MO_MP_Players1Icons { get; private set; }

	public ObservableCollection<Visibility> MO_MP_Players2Icons { get; private set; }

	public ObservableCollection<Visibility> MO_MP_Players3Icons { get; private set; }

	public ObservableCollection<Visibility> MO_MP_Players4Icons { get; private set; }

	public ObservableCollection<Visibility> MO_MP_Players5Icons { get; private set; }

	public ObservableCollection<Visibility> MO_MP_Players6Icons { get; private set; }

	public ObservableCollection<Visibility> MO_MP_Players7Icons { get; private set; }

	public ObservableCollection<Visibility> MO_MP_Players8Icons { get; private set; }

	public Visibility MO_SP_Score
	{
		get
		{
			return _MO_SP_Score;
		}
		set
		{
			if (_MO_SP_Score != value)
			{
				_MO_SP_Score = value;
				NotifyPropertyChanged("MO_SP_Score");
			}
		}
	}

	public string MO_LevelPoints
	{
		get
		{
			return _MO_LevelPoints;
		}
		set
		{
			if (_MO_LevelPoints != value)
			{
				_MO_LevelPoints = value;
				NotifyPropertyChanged("MO_LevelPoints");
			}
		}
	}

	public Visibility MO_Months
	{
		get
		{
			return _MO_Months;
		}
		set
		{
			if (_MO_Months != value)
			{
				_MO_Months = value;
				NotifyPropertyChanged("MO_Months");
			}
		}
	}

	public string MO_MonthText
	{
		get
		{
			return _MO_MonthText;
		}
		set
		{
			if (_MO_MonthText != value)
			{
				_MO_MonthText = value;
				NotifyPropertyChanged("MO_MonthText");
			}
		}
	}

	public string MO_MonthPoints
	{
		get
		{
			return _MO_MonthPoints;
		}
		set
		{
			if (_MO_MonthPoints != value)
			{
				_MO_MonthPoints = value;
				NotifyPropertyChanged("MO_MonthPoints");
			}
		}
	}

	public Visibility MO_Troops
	{
		get
		{
			return _MO_Troops;
		}
		set
		{
			if (_MO_Troops != value)
			{
				_MO_Troops = value;
				NotifyPropertyChanged("MO_Troops");
			}
		}
	}

	public string MO_TroopsText
	{
		get
		{
			return _MO_TroopsText;
		}
		set
		{
			if (_MO_TroopsText != value)
			{
				_MO_TroopsText = value;
				NotifyPropertyChanged("MO_TroopsText");
			}
		}
	}

	public string MO_TroopsPoints
	{
		get
		{
			return _MO_TroopsPoints;
		}
		set
		{
			if (_MO_TroopsPoints != value)
			{
				_MO_TroopsPoints = value;
				NotifyPropertyChanged("MO_TroopsPoints");
			}
		}
	}

	public string MO_ScorePoints
	{
		get
		{
			return _MO_ScorePoints;
		}
		set
		{
			if (_MO_ScorePoints != value)
			{
				_MO_ScorePoints = value;
				NotifyPropertyChanged("MO_ScorePoints");
			}
		}
	}

	public string MO_LastScorePoints
	{
		get
		{
			return _MO_LastScorePoints;
		}
		set
		{
			if (_MO_LastScorePoints != value)
			{
				_MO_LastScorePoints = value;
				NotifyPropertyChanged("MO_LastScorePoints");
			}
		}
	}

	public Visibility MO_SiegeThat
	{
		get
		{
			return _MO_SiegeThat;
		}
		set
		{
			if (_MO_SiegeThat != value)
			{
				_MO_SiegeThat = value;
				NotifyPropertyChanged("MO_SiegeThat");
			}
		}
	}

	public string MO_SiegeThatName
	{
		get
		{
			return _MO_SiegeThatName;
		}
		set
		{
			if (_MO_SiegeThatName != value)
			{
				_MO_SiegeThatName = value;
				NotifyPropertyChanged("MO_SiegeThatName");
			}
		}
	}

	public string MO_ST_Attackers
	{
		get
		{
			return _MO_ST_Attackers;
		}
		set
		{
			if (_MO_ST_Attackers != value)
			{
				_MO_ST_Attackers = value;
				NotifyPropertyChanged("MO_ST_Attackers");
			}
		}
	}

	public string MO_ST_Defenders
	{
		get
		{
			return _MO_ST_Defenders;
		}
		set
		{
			if (_MO_ST_Defenders != value)
			{
				_MO_ST_Defenders = value;
				NotifyPropertyChanged("MO_ST_Defenders");
			}
		}
	}

	public string MO_ST_Castle
	{
		get
		{
			return _MO_ST_Castle;
		}
		set
		{
			if (_MO_ST_Castle != value)
			{
				_MO_ST_Castle = value;
				NotifyPropertyChanged("MO_ST_Castle");
			}
		}
	}

	public Visibility MO_DefeatVis
	{
		get
		{
			return _MO_DefeatVis;
		}
		set
		{
			if (_MO_DefeatVis != value)
			{
				_MO_DefeatVis = value;
				NotifyPropertyChanged("MO_DefeatVis");
			}
		}
	}

	public Visibility MO_MP_Score
	{
		get
		{
			return _MO_MP_Score;
		}
		set
		{
			if (_MO_MP_Score != value)
			{
				_MO_MP_Score = value;
				NotifyPropertyChanged("MO_MP_Score");
			}
		}
	}

	public Visibility MO_MP_Defeat
	{
		get
		{
			return _MO_MP_Defeat;
		}
		set
		{
			if (_MO_MP_Defeat != value)
			{
				_MO_MP_Defeat = value;
				NotifyPropertyChanged("MO_MP_Defeat");
			}
		}
	}

	public Visibility MO_MP_Victory
	{
		get
		{
			return _MO_MP_Victory;
		}
		set
		{
			if (_MO_MP_Victory != value)
			{
				_MO_MP_Victory = value;
				NotifyPropertyChanged("MO_MP_Victory");
			}
		}
	}

	public ImageSource MO_MP_PlayersShields0
	{
		get
		{
			return _MO_MP_PlayersShields0;
		}
		set
		{
			if (_MO_MP_PlayersShields0 != value)
			{
				_MO_MP_PlayersShields0 = value;
				NotifyPropertyChanged("MO_MP_PlayersShields0");
			}
		}
	}

	public ImageSource MO_MP_PlayersShields1
	{
		get
		{
			return _MO_MP_PlayersShields1;
		}
		set
		{
			if (_MO_MP_PlayersShields1 != value)
			{
				_MO_MP_PlayersShields1 = value;
				NotifyPropertyChanged("MO_MP_PlayersShields1");
			}
		}
	}

	public ImageSource MO_MP_PlayersShields2
	{
		get
		{
			return _MO_MP_PlayersShields2;
		}
		set
		{
			if (_MO_MP_PlayersShields2 != value)
			{
				_MO_MP_PlayersShields2 = value;
				NotifyPropertyChanged("MO_MP_PlayersShields2");
			}
		}
	}

	public ImageSource MO_MP_PlayersShields3
	{
		get
		{
			return _MO_MP_PlayersShields3;
		}
		set
		{
			if (_MO_MP_PlayersShields3 != value)
			{
				_MO_MP_PlayersShields3 = value;
				NotifyPropertyChanged("MO_MP_PlayersShields3");
			}
		}
	}

	public ImageSource MO_MP_PlayersShields4
	{
		get
		{
			return _MO_MP_PlayersShields4;
		}
		set
		{
			if (_MO_MP_PlayersShields4 != value)
			{
				_MO_MP_PlayersShields4 = value;
				NotifyPropertyChanged("MO_MP_PlayersShields4");
			}
		}
	}

	public ImageSource MO_MP_PlayersShields5
	{
		get
		{
			return _MO_MP_PlayersShields5;
		}
		set
		{
			if (_MO_MP_PlayersShields5 != value)
			{
				_MO_MP_PlayersShields5 = value;
				NotifyPropertyChanged("MO_MP_PlayersShields5");
			}
		}
	}

	public ImageSource MO_MP_PlayersShields6
	{
		get
		{
			return _MO_MP_PlayersShields6;
		}
		set
		{
			if (_MO_MP_PlayersShields6 != value)
			{
				_MO_MP_PlayersShields6 = value;
				NotifyPropertyChanged("MO_MP_PlayersShields6");
			}
		}
	}

	public ImageSource MO_MP_PlayersShields7
	{
		get
		{
			return _MO_MP_PlayersShields7;
		}
		set
		{
			if (_MO_MP_PlayersShields7 != value)
			{
				_MO_MP_PlayersShields7 = value;
				NotifyPropertyChanged("MO_MP_PlayersShields7");
			}
		}
	}

	public ImageSource MO_MP_PlayersFear0
	{
		get
		{
			return _MO_MP_PlayersFear0;
		}
		set
		{
			if (_MO_MP_PlayersFear0 != value)
			{
				_MO_MP_PlayersFear0 = value;
				NotifyPropertyChanged("MO_MP_PlayersFear0");
			}
		}
	}

	public ImageSource MO_MP_PlayersFear1
	{
		get
		{
			return _MO_MP_PlayersFear1;
		}
		set
		{
			if (_MO_MP_PlayersFear1 != value)
			{
				_MO_MP_PlayersFear1 = value;
				NotifyPropertyChanged("MO_MP_PlayersFear1");
			}
		}
	}

	public ImageSource MO_MP_PlayersFear2
	{
		get
		{
			return _MO_MP_PlayersFear2;
		}
		set
		{
			if (_MO_MP_PlayersFear2 != value)
			{
				_MO_MP_PlayersFear2 = value;
				NotifyPropertyChanged("MO_MP_PlayersFear2");
			}
		}
	}

	public ImageSource MO_MP_PlayersFear3
	{
		get
		{
			return _MO_MP_PlayersFear3;
		}
		set
		{
			if (_MO_MP_PlayersFear3 != value)
			{
				_MO_MP_PlayersFear3 = value;
				NotifyPropertyChanged("MO_MP_PlayersFear3");
			}
		}
	}

	public ImageSource MO_MP_PlayersFear4
	{
		get
		{
			return _MO_MP_PlayersFear4;
		}
		set
		{
			if (_MO_MP_PlayersFear4 != value)
			{
				_MO_MP_PlayersFear4 = value;
				NotifyPropertyChanged("MO_MP_PlayersFear4");
			}
		}
	}

	public ImageSource MO_MP_PlayersFear5
	{
		get
		{
			return _MO_MP_PlayersFear5;
		}
		set
		{
			if (_MO_MP_PlayersFear5 != value)
			{
				_MO_MP_PlayersFear5 = value;
				NotifyPropertyChanged("MO_MP_PlayersFear5");
			}
		}
	}

	public ImageSource MO_MP_PlayersFear6
	{
		get
		{
			return _MO_MP_PlayersFear6;
		}
		set
		{
			if (_MO_MP_PlayersFear6 != value)
			{
				_MO_MP_PlayersFear6 = value;
				NotifyPropertyChanged("MO_MP_PlayersFear6");
			}
		}
	}

	public ImageSource MO_MP_PlayersFear7
	{
		get
		{
			return _MO_MP_PlayersFear7;
		}
		set
		{
			if (_MO_MP_PlayersFear7 != value)
			{
				_MO_MP_PlayersFear7 = value;
				NotifyPropertyChanged("MO_MP_PlayersFear7");
			}
		}
	}

	public Thickness RightclickMargin
	{
		get
		{
			return _rightclickMargin;
		}
		set
		{
			if (_rightclickMargin != value)
			{
				_rightclickMargin = value;
				NotifyPropertyChanged("RightclickMargin");
			}
		}
	}

	public string WorkshopUploadText
	{
		get
		{
			return _workshopUploadText;
		}
		set
		{
			if (_workshopUploadText != value)
			{
				_workshopUploadText = value;
				NotifyPropertyChanged("WorkshopUploadText");
			}
		}
	}

	public static Dictionary<string, int> eGoodsLookUp { get; set; }

	public static Dictionary<string, int> eChimpLookUp { get; set; }

	public static Dictionary<string, int> eStructLookUp { get; set; }

	public static Dictionary<string, int> eMapperLookUp { get; set; }

	public static Dictionary<string, int> eStructToMapperLookUp { get; set; }

	public static Dictionary<int, int> eMapperHelpLookup { get; set; }

	public static MainViewModel Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new MainViewModel();
			}
			return instance;
		}
	}

	public string MousePosText
	{
		get
		{
			return _mousePosText;
		}
		set
		{
			_mousePosText = value;
			NotifyPropertyChanged("MousePosText");
		}
	}

	public double PanelX
	{
		get
		{
			return _panelX;
		}
		set
		{
			if (!value.Equals(_panelX))
			{
				_panelX = value;
				NotifyPropertyChanged("PanelX");
			}
		}
	}

	public double PanelY
	{
		get
		{
			return _panelY;
		}
		set
		{
			if (!value.Equals(_panelY))
			{
				_panelY = value;
				NotifyPropertyChanged("PanelY");
			}
		}
	}

	public int MEMode
	{
		get
		{
			return _MEMode;
		}
		set
		{
			if (_MEMode != value)
			{
				_MEMode = value;
				if (IsMapEditorMode && EditorDirector.instance != null)
				{
					EditorDirector.instance.changeEditorMode();
				}
			}
		}
	}

	public Visibility DEBUG_VISIBLE => _DEBUG_VISIBLE;

	public string EnterYourName
	{
		get
		{
			return _enterYourName;
		}
		set
		{
			if (_enterYourName != value)
			{
				_enterYourName = value;
				NotifyPropertyChanged("EnterYourName");
			}
		}
	}

	public bool EnterYourNameVis
	{
		get
		{
			return _enterYourNameVis;
		}
		set
		{
			if (_enterYourNameVis != value)
			{
				_enterYourNameVis = value;
				NotifyPropertyChanged("EnterYourNameVis");
			}
		}
	}

	public string FrontEndButtonMargin
	{
		get
		{
			return _frontEndButtonMargin;
		}
		set
		{
			if (_frontEndButtonMargin != value)
			{
				_frontEndButtonMargin = value;
				NotifyPropertyChanged("FrontEndButtonMargin");
			}
		}
	}

	public string FrontEndHomeFiresButtonMargin
	{
		get
		{
			return _frontEndHomeFiresButtonMargin;
		}
		set
		{
			if (_frontEndHomeFiresButtonMargin != value)
			{
				_frontEndHomeFiresButtonMargin = value;
				NotifyPropertyChanged("FrontEndHomeFiresButtonMargin");
			}
		}
	}

	public string FrontEndButtonDLCMargin
	{
		get
		{
			return _frontEndButtonDLCMargin;
		}
		set
		{
			if (_frontEndButtonDLCMargin != value)
			{
				_frontEndButtonDLCMargin = value;
				NotifyPropertyChanged("FrontEndButtonDLCMargin");
			}
		}
	}

	public string FrontEndWishlistButtonMargin
	{
		get
		{
			return _frontEndWishlistButtonMargin;
		}
		set
		{
			if (_frontEndWishlistButtonMargin != value)
			{
				_frontEndWishlistButtonMargin = value;
				NotifyPropertyChanged("FrontEndWishlistButtonMargin");
			}
		}
	}

	public string FrontEndVideoMargin
	{
		get
		{
			return _frontEndVideoMargin;
		}
		set
		{
			if (_frontEndVideoMargin != value)
			{
				_frontEndVideoMargin = value;
				NotifyPropertyChanged("FrontEndVideoMargin");
			}
		}
	}

	public string HighlightedModeTitle
	{
		get
		{
			return _highlightedModeTitle;
		}
		set
		{
			if (_highlightedModeTitle != value)
			{
				_highlightedModeTitle = value;
				NotifyPropertyChanged("HighlightedModeTitle");
			}
		}
	}

	public string RollOverText
	{
		get
		{
			return _rollOverText;
		}
		set
		{
			if (_rollOverText != value)
			{
				_rollOverText = value;
				NotifyPropertyChanged("RollOverText");
			}
		}
	}

	public string RollOverText_AmountReq1
	{
		get
		{
			return _rollOverText_AmountReq1;
		}
		set
		{
			if (_rollOverText_AmountReq1 != value)
			{
				_rollOverText_AmountReq1 = value;
				NotifyPropertyChanged("RollOverText_AmountReq1");
			}
		}
	}

	public string RollOverText_AmountGot1
	{
		get
		{
			return _rollOverText_AmountGot1;
		}
		set
		{
			if (_rollOverText_AmountGot1 != value)
			{
				_rollOverText_AmountGot1 = value;
				NotifyPropertyChanged("RollOverText_AmountGot1");
			}
		}
	}

	public string RollOverText_AmountReq2
	{
		get
		{
			return _rollOverText_AmountReq2;
		}
		set
		{
			if (_rollOverText_AmountReq2 != value)
			{
				_rollOverText_AmountReq2 = value;
				NotifyPropertyChanged("RollOverText_AmountReq2");
			}
		}
	}

	public string RollOverText_AmountGot2
	{
		get
		{
			return _rollOverText_AmountGot2;
		}
		set
		{
			if (_rollOverText_AmountGot2 != value)
			{
				_rollOverText_AmountGot2 = value;
				NotifyPropertyChanged("RollOverText_AmountGot2");
			}
		}
	}

	public bool RolloverBuilding_TooltipVis
	{
		get
		{
			return _rolloverBuilding_TooltipVis;
		}
		set
		{
			if (_rolloverBuilding_TooltipVis != value)
			{
				_rolloverBuilding_TooltipVis = value;
				NotifyPropertyChanged("RolloverBuilding_TooltipVis");
				RolloverBuilding_TooltipVisNot = true;
			}
		}
	}

	public bool RolloverBuilding_TooltipVisNot
	{
		get
		{
			return !_rolloverBuilding_TooltipVis;
		}
		set
		{
			NotifyPropertyChanged("RolloverBuilding_TooltipVisNot");
		}
	}

	public bool RolloverBuilding_TooltipConsumesVis
	{
		get
		{
			return _rolloverBuilding_TooltipConsumesVis;
		}
		set
		{
			if (_rolloverBuilding_TooltipConsumesVis != value)
			{
				_rolloverBuilding_TooltipConsumesVis = value;
				NotifyPropertyChanged("RolloverBuilding_TooltipConsumesVis");
			}
		}
	}

	public bool RolloverBuilding_TooltipProducesVis
	{
		get
		{
			return _rolloverBuilding_TooltipProducesVis;
		}
		set
		{
			if (_rolloverBuilding_TooltipProducesVis != value)
			{
				_rolloverBuilding_TooltipProducesVis = value;
				NotifyPropertyChanged("RolloverBuilding_TooltipProducesVis");
			}
		}
	}

	public ImageSource RolloverBuilding_ConsumesImage
	{
		get
		{
			return _rolloverBuilding_ConsumesImage;
		}
		set
		{
			if (_rolloverBuilding_ConsumesImage != value)
			{
				_rolloverBuilding_ConsumesImage = value;
				NotifyPropertyChanged("RolloverBuilding_ConsumesImage");
			}
		}
	}

	public ImageSource RolloverBuilding_ConsumesImage2
	{
		get
		{
			return _rolloverBuilding_ConsumesImage2;
		}
		set
		{
			if (_rolloverBuilding_ConsumesImage2 != value)
			{
				_rolloverBuilding_ConsumesImage2 = value;
				NotifyPropertyChanged("RolloverBuilding_ConsumesImage2");
			}
		}
	}

	public ImageSource RolloverBuilding_ProducesImage
	{
		get
		{
			return _rolloverBuilding_ProducesImage;
		}
		set
		{
			if (_rolloverBuilding_ProducesImage != value)
			{
				_rolloverBuilding_ProducesImage = value;
				NotifyPropertyChanged("RolloverBuilding_ProducesImage");
			}
		}
	}

	public ImageSource RolloverBuilding_ProducesImage2
	{
		get
		{
			return _rolloverBuilding_ProducesImage2;
		}
		set
		{
			if (_rolloverBuilding_ProducesImage2 != value)
			{
				_rolloverBuilding_ProducesImage2 = value;
				NotifyPropertyChanged("RolloverBuilding_ProducesImage2");
			}
		}
	}

	public string RolloverBuilding_TooltipTitle
	{
		get
		{
			return _rolloverBuilding_TooltipTitle;
		}
		set
		{
			if (_rolloverBuilding_TooltipTitle != value)
			{
				_rolloverBuilding_TooltipTitle = value;
				NotifyPropertyChanged("RolloverBuilding_TooltipTitle");
			}
		}
	}

	public string RolloverBuilding_TooltipBody
	{
		get
		{
			return _rolloverBuilding_TooltipBody;
		}
		set
		{
			if (_rolloverBuilding_TooltipBody != value)
			{
				_rolloverBuilding_TooltipBody = value;
				NotifyPropertyChanged("RolloverBuilding_TooltipBody");
			}
		}
	}

	public ImageSource RollOverText_GoodsImage1
	{
		get
		{
			return _rollOverText_GoodsImage1;
		}
		set
		{
			if (_rollOverText_GoodsImage1 != value)
			{
				_rollOverText_GoodsImage1 = value;
				NotifyPropertyChanged("RollOverText_GoodsImage1");
			}
		}
	}

	public ImageSource RollOverText_GoodsImage2
	{
		get
		{
			return _rollOverText_GoodsImage2;
		}
		set
		{
			if (_rollOverText_GoodsImage2 != value)
			{
				_rollOverText_GoodsImage2 = value;
				NotifyPropertyChanged("RollOverText_GoodsImage2");
			}
		}
	}

	public string RollOverText_Margin
	{
		get
		{
			return _rollOverText_Margin;
		}
		set
		{
			if (_rollOverText_Margin != value)
			{
				_rollOverText_Margin = value;
				NotifyPropertyChanged("RollOverText_Margin");
			}
		}
	}

	public string DebugText
	{
		get
		{
			return _debugText;
		}
		set
		{
			if (_debugText != value)
			{
				_debugText = value;
				NotifyPropertyChanged("DebugText");
			}
		}
	}

	public string DebugText2
	{
		get
		{
			return _debugText2;
		}
		set
		{
			if (_debugText2 != value)
			{
				_debugText2 = value;
				NotifyPropertyChanged("DebugText2");
			}
		}
	}

	public string BookPopularityText
	{
		get
		{
			return _bookPopularityText;
		}
		set
		{
			if (_bookPopularityText != value)
			{
				_bookPopularityText = value;
				NotifyPropertyChanged("BookPopularityText");
			}
		}
	}

	public bool PopularityIncreasingVis
	{
		get
		{
			return _popularityIncreasingVis;
		}
		set
		{
			if (_popularityIncreasingVis != value)
			{
				_popularityIncreasingVis = value;
				NotifyPropertyChanged("PopularityIncreasingVis");
			}
		}
	}

	public bool PopularityDecreasingVis
	{
		get
		{
			return _popularityDecreasingVis;
		}
		set
		{
			if (_popularityDecreasingVis != value)
			{
				_popularityDecreasingVis = value;
				NotifyPropertyChanged("PopularityDecreasingVis");
			}
		}
	}

	public int BookPopularityFontSize
	{
		get
		{
			return _bookPopularityFontSize;
		}
		set
		{
			if (_bookPopularityFontSize != value)
			{
				_bookPopularityFontSize = value;
				NotifyPropertyChanged("BookPopularityFontSize");
			}
		}
	}

	public int BookGoldLargeFontSize
	{
		get
		{
			return _bookGoldLargeFontSize;
		}
		set
		{
			if (_bookGoldLargeFontSize != value)
			{
				_bookGoldLargeFontSize = value;
				NotifyPropertyChanged("BookGoldLargeFontSize");
			}
		}
	}

	public int BookGoldSmallFontSize
	{
		get
		{
			return _bookGoldSmallFontSize;
		}
		set
		{
			if (_bookGoldSmallFontSize != value)
			{
				_bookGoldSmallFontSize = value;
				NotifyPropertyChanged("BookGoldSmallFontSize");
			}
		}
	}

	public int BookPopulationFontSize
	{
		get
		{
			return _bookPopulationFontSize;
		}
		set
		{
			if (_bookPopulationFontSize != value)
			{
				_bookPopulationFontSize = value;
				NotifyPropertyChanged("BookPopulationFontSize");
			}
		}
	}

	public bool BookGoldSmall
	{
		get
		{
			return _bookGoldSmall;
		}
		set
		{
			if (_bookGoldSmall != value)
			{
				_bookGoldSmall = value;
				NotifyPropertyChanged("BookGoldSmall");
			}
		}
	}

	public bool BookGoldLarge
	{
		get
		{
			return _bookGoldLarge;
		}
		set
		{
			if (_bookGoldLarge != value)
			{
				_bookGoldLarge = value;
				NotifyPropertyChanged("BookGoldLarge");
			}
		}
	}

	public SolidColorBrush BookPopularityColour
	{
		get
		{
			return _bookPopularityColour;
		}
		set
		{
			if (_bookPopularityColour != value)
			{
				_bookPopularityColour = value;
				NotifyPropertyChanged("BookPopularityColour");
			}
		}
	}

	public string BookGoldText
	{
		get
		{
			return _bookGoldText;
		}
		set
		{
			if (_bookGoldText != value)
			{
				_bookGoldText = value;
				NotifyPropertyChanged("BookGoldText");
			}
		}
	}

	public string BookPopulationText
	{
		get
		{
			return _bookPopulationText;
		}
		set
		{
			if (_bookPopulationText != value)
			{
				_bookPopulationText = value;
				NotifyPropertyChanged("BookPopulationText");
			}
		}
	}

	public SolidColorBrush BookPopulationColour
	{
		get
		{
			return _bookPopulationColour;
		}
		set
		{
			if (_bookPopulationColour != value)
			{
				_bookPopulationColour = value;
				NotifyPropertyChanged("BookPopulationColour");
			}
		}
	}

	public ImageSource HighlightedModeImage
	{
		get
		{
			return _highlightedModeImage;
		}
		set
		{
			_highlightedModeImage = value;
			NotifyPropertyChanged("HighlightedModeImage");
		}
	}

	public ImageSource SketchImage
	{
		get
		{
			return _sketchImage;
		}
		set
		{
			_sketchImage = value;
			NotifyPropertyChanged("SketchImage");
		}
	}

	public ImageSource GroomImage
	{
		get
		{
			return _groomImage;
		}
		set
		{
			_groomImage = value;
			NotifyPropertyChanged("GroomImage");
		}
	}

	public ImageSource BrideImage
	{
		get
		{
			return _brideImage;
		}
		set
		{
			_brideImage = value;
			NotifyPropertyChanged("BrideImage");
		}
	}

	public float SketchWidth
	{
		get
		{
			return _sketchWidth;
		}
		set
		{
			if (_sketchWidth != value)
			{
				_sketchWidth = value;
				NotifyPropertyChanged("SketchWidth");
			}
		}
	}

	public float SketchHeight
	{
		get
		{
			return _sketchHeight;
		}
		set
		{
			if (_sketchHeight != value)
			{
				_sketchHeight = value;
				NotifyPropertyChanged("SketchHeight");
			}
		}
	}

	public float ButtonWidth
	{
		get
		{
			return _buttonWidth;
		}
		set
		{
			if (_buttonWidth != value)
			{
				_buttonWidth = value;
				NotifyPropertyChanged("ButtonWidth");
			}
		}
	}

	public Visibility ChurchPanelRingsVis
	{
		get
		{
			return _churchPanelRingsVis;
		}
		set
		{
			if (_churchPanelRingsVis != value)
			{
				_churchPanelRingsVis = value;
				NotifyPropertyChanged("ChurchPanelRingsVis");
			}
		}
	}

	public bool Show_IntroSequence
	{
		get
		{
			return _show_IntroSequence;
		}
		set
		{
			_show_IntroSequence = value;
			NotifyPropertyChanged("Show_IntroSequence");
		}
	}

	public bool Show_FrontMenus
	{
		get
		{
			return _show_FrontMenus;
		}
		set
		{
			_show_FrontMenus = value;
			NotifyPropertyChanged("Show_FrontMenus");
			Show_HUD_FrontEndVideo = true;
		}
	}

	public bool Show_Frontend
	{
		get
		{
			return _show_Frontend;
		}
		set
		{
			if (_show_Frontend != value)
			{
				_show_Frontend = value;
				NotifyPropertyChanged("Show_Frontend");
				FrontEndMenu.PlayBackgroundVideo(value);
			}
		}
	}

	public bool Show_Frontend_MainMenu
	{
		get
		{
			return _show_Frontend_MainMenu;
		}
		set
		{
			_show_Frontend_MainMenu = value;
			NotifyPropertyChanged("Show_Frontend_MainMenu");
		}
	}

	public bool Show_Frontend_Combat
	{
		get
		{
			return _show_Frontend_Combat;
		}
		set
		{
			_show_Frontend_Combat = value;
			NotifyPropertyChanged("Show_Frontend_Combat");
		}
	}

	public bool Show_FrontEndCombat_Main_Help
	{
		get
		{
			return _show_FrontEndCombat_Main_Help;
		}
		set
		{
			_show_FrontEndCombat_Main_Help = value;
			NotifyPropertyChanged("Show_FrontEndCombat_Main_Help");
		}
	}

	public bool Show_FrontEndCombat_Jewel_Help
	{
		get
		{
			return _show_FrontEndCombat_Jewel_Help;
		}
		set
		{
			_show_FrontEndCombat_Jewel_Help = value;
			NotifyPropertyChanged("Show_FrontEndCombat_Jewel_Help");
		}
	}

	public bool Show_FrontEndEco_Main_Help
	{
		get
		{
			return _show_FrontEndEco_Main_Help;
		}
		set
		{
			_show_FrontEndEco_Main_Help = value;
			NotifyPropertyChanged("Show_FrontEndEco_Main_Help");
		}
	}

	public bool Show_FrontEndEco_DLC_Help
	{
		get
		{
			return _show_FrontEndEco_DLC_Help;
		}
		set
		{
			_show_FrontEndEco_DLC_Help = value;
			NotifyPropertyChanged("Show_FrontEndEco_DLC_Help");
		}
	}

	public bool Show_FrontEndCombat_DLC_3_Help
	{
		get
		{
			return _show_FrontEndCombat_DLC_3_Help;
		}
		set
		{
			_show_FrontEndCombat_DLC_3_Help = value;
			NotifyPropertyChanged("Show_FrontEndCombat_DLC_3_Help");
		}
	}

	public bool Show_FrontEndCombat_DLC_4_Help
	{
		get
		{
			return _show_FrontEndCombat_DLC_4_Help;
		}
		set
		{
			_show_FrontEndCombat_DLC_4_Help = value;
			NotifyPropertyChanged("Show_FrontEndCombat_DLC_4_Help");
		}
	}

	public bool Show_Frontend_Jewel
	{
		get
		{
			return _show_Frontend_Jewel;
		}
		set
		{
			_show_Frontend_Jewel = value;
			NotifyPropertyChanged("Show_Frontend_Jewel");
		}
	}

	public bool Show_Frontend_Trails
	{
		get
		{
			return _show_Frontend_Trails;
		}
		set
		{
			_show_Frontend_Trails = value;
			NotifyPropertyChanged("Show_Frontend_Trails");
		}
	}

	public bool Show_Frontend_Eco
	{
		get
		{
			return _show_Frontend_Eco;
		}
		set
		{
			_show_Frontend_Eco = value;
			NotifyPropertyChanged("Show_Frontend_Eco");
		}
	}

	public bool Show_CampaignMenu
	{
		get
		{
			return _show_CampaignMenu;
		}
		set
		{
			_show_CampaignMenu = value;
			NotifyPropertyChanged("Show_CampaignMenu");
			Show_HUD_FrontEndVideo = true;
		}
	}

	public bool Show_EcoCampaignMenu
	{
		get
		{
			return _show_EcoCampaignMenu;
		}
		set
		{
			_show_EcoCampaignMenu = value;
			NotifyPropertyChanged("Show_EcoCampaignMenu");
			Show_HUD_FrontEndVideo = true;
		}
	}

	public bool Show_Extra1CampaignMenu
	{
		get
		{
			return _show_Extra1CampaignMenu;
		}
		set
		{
			_show_Extra1CampaignMenu = value;
			NotifyPropertyChanged("Show_Extra1CampaignMenu");
			Show_HUD_FrontEndVideo = true;
		}
	}

	public bool Show_Extra2CampaignMenu
	{
		get
		{
			return _show_Extra2CampaignMenu;
		}
		set
		{
			_show_Extra2CampaignMenu = value;
			NotifyPropertyChanged("Show_Extra2CampaignMenu");
			Show_HUD_FrontEndVideo = true;
		}
	}

	public bool Show_Extra3CampaignMenu
	{
		get
		{
			return _show_Extra3CampaignMenu;
		}
		set
		{
			_show_Extra3CampaignMenu = value;
			NotifyPropertyChanged("Show_Extra3CampaignMenu");
			Show_HUD_FrontEndVideo = true;
		}
	}

	public bool Show_Extra4CampaignMenu
	{
		get
		{
			return _show_Extra4CampaignMenu;
		}
		set
		{
			_show_Extra4CampaignMenu = value;
			NotifyPropertyChanged("Show_Extra4CampaignMenu");
			Show_HUD_FrontEndVideo = true;
		}
	}

	public bool Show_TrailCampaignMenu
	{
		get
		{
			return _show_TrailCampaignMenu;
		}
		set
		{
			_show_TrailCampaignMenu = value;
			NotifyPropertyChanged("Show_TrailCampaignMenu");
			Show_HUD_FrontEndVideo = true;
		}
	}

	public bool Show_Trail2CampaignMenu
	{
		get
		{
			return _show_Trail2CampaignMenu;
		}
		set
		{
			_show_Trail2CampaignMenu = value;
			NotifyPropertyChanged("Show_Trail2CampaignMenu");
			Show_HUD_FrontEndVideo = true;
		}
	}

	public bool Show_ExtraEcoCampaignMenu
	{
		get
		{
			return _show_ExtraEcoCampaignMenu;
		}
		set
		{
			_show_ExtraEcoCampaignMenu = value;
			NotifyPropertyChanged("Show_ExtraEcoCampaignMenu");
			Show_HUD_FrontEndVideo = true;
		}
	}

	public bool Show_StandaloneSetup
	{
		get
		{
			return _show_StandaloneSetup;
		}
		set
		{
			_show_StandaloneSetup = value;
			NotifyPropertyChanged("Show_StandaloneSetup");
			Show_HUD_FrontEndVideo = true;
		}
	}

	public bool Show_MultiplayerSetup
	{
		get
		{
			return _show_MultiplayerSetup;
		}
		set
		{
			_show_MultiplayerSetup = value;
			NotifyPropertyChanged("Show_MultiplayerSetup");
			Show_HUD_FrontEndVideo = true;
		}
	}

	public bool Show_Credits
	{
		get
		{
			return _show_Credits;
		}
		set
		{
			_show_Credits = value;
			NotifyPropertyChanged("Show_Credits");
			Show_HUD_FrontEndVideo = true;
			if (value)
			{
				FRONTCredits.init();
			}
		}
	}

	public bool Show_MapEditor
	{
		get
		{
			return _show_MapEditor;
		}
		set
		{
			_show_MapEditor = value;
			NotifyPropertyChanged("Show_MapEditor");
			Show_HUD_FrontEndVideo = true;
		}
	}

	public bool Show_Frontend_Logo
	{
		get
		{
			return _show_Frontend_Logo;
		}
		set
		{
			_show_Frontend_Logo = value;
			NotifyPropertyChanged("Show_Frontend_Logo");
		}
	}

	public bool Show_Frontend_Feedback
	{
		get
		{
			return _show_Frontend_Feedback;
		}
		set
		{
			_show_Frontend_Feedback = value;
			NotifyPropertyChanged("Show_Frontend_Feedback");
		}
	}

	public bool Show_Frontend_WishBack
	{
		get
		{
			return _show_Frontend_WishBack;
		}
		set
		{
			_show_Frontend_WishBack = value;
			NotifyPropertyChanged("Show_Frontend_WishBack");
		}
	}

	public bool Show_Frontend_Controls_Selection
	{
		get
		{
			return _show_Frontend_Controls_Selection;
		}
		set
		{
			_show_Frontend_Controls_Selection = value;
			NotifyPropertyChanged("Show_Frontend_Controls_Selection");
		}
	}

	public bool Show_Frontend_Demo
	{
		get
		{
			return _show_Frontend_Demo;
		}
		set
		{
			_show_Frontend_Demo = value;
			NotifyPropertyChanged("Show_Frontend_Demo");
		}
	}

	public bool Show_Frontend_Wish
	{
		get
		{
			return _show_Frontend_Wish;
		}
		set
		{
			_show_Frontend_Wish = value;
			NotifyPropertyChanged("Show_Frontend_Wish");
		}
	}

	public bool Show_MapScreenStoryMode
	{
		get
		{
			return _show_MapScreenStoryMode;
		}
		set
		{
			_show_MapScreenStoryMode = value;
			NotifyPropertyChanged("Show_MapScreenStoryMode");
		}
	}

	public bool Show_MapScreenMenuMode
	{
		get
		{
			return _show_MapScreenMenuMode;
		}
		set
		{
			_show_MapScreenMenuMode = value;
			NotifyPropertyChanged("Show_MapScreenMenuMode");
		}
	}

	public bool Show_Story
	{
		get
		{
			return _show_Story;
		}
		set
		{
			_show_Story = value;
			NotifyPropertyChanged("Show_Story");
		}
	}

	public bool Show_InGame
	{
		get
		{
			return _show_InGame;
		}
		set
		{
			_show_InGame = value;
			NotifyPropertyChanged("Show_InGame");
		}
	}

	public bool Show_InGameUI
	{
		get
		{
			return _show_InGameUI;
		}
		set
		{
			_show_InGameUI = value;
			OST_Game_Paused_Vis = _OST_Game_Paused_Vis_Set && _OST_Game_Paused_Vis_Allowed && _show_InGameUI;
			NotifyPropertyChanged("Show_InGameUI");
			NotifyPropertyChanged("Show_HUD_Extras");
		}
	}

	public bool Show_BlackOut
	{
		get
		{
			return _show_BlackOut;
		}
		set
		{
			_show_BlackOut = value;
			NotifyPropertyChanged("Show_BlackOut");
		}
	}

	public bool Hide_Loading_Screen
	{
		get
		{
			return _hide_Loading_Screen;
		}
		set
		{
			_hide_Loading_Screen = value;
			NotifyPropertyChanged("Hide_Loading_Screen");
		}
	}

	public bool Show_HUD_Main
	{
		get
		{
			return _show_HUD_Main;
		}
		set
		{
			_show_HUD_Main = value;
			NotifyPropertyChanged("Show_HUD_Main");
		}
	}

	public bool Show_HUD_Troops
	{
		get
		{
			return _show_HUD_Troops;
		}
		set
		{
			_show_HUD_Troops = value;
			NotifyPropertyChanged("Show_HUD_Troops");
			if (!value)
			{
				Show_HUD_ControlGroups = false;
			}
		}
	}

	public bool Show_HUD_Building
	{
		get
		{
			return _show_HUD_Building;
		}
		set
		{
			_show_HUD_Building = value;
			NotifyPropertyChanged("Show_HUD_Building");
		}
	}

	public bool Show_HUD_MissionOver
	{
		get
		{
			return _show_HUD_MissionOver;
		}
		set
		{
			if (_show_HUD_MissionOver != value)
			{
				_show_HUD_MissionOver = value;
				NotifyPropertyChanged("Show_HUD_MissionOver");
				if (!value)
				{
					HUDMissionOver.PlayBackgroundVideo(state: false);
				}
			}
		}
	}

	public bool Show_HUD_Help
	{
		get
		{
			return _show_HUD_Help;
		}
		set
		{
			_show_HUD_Help = value;
			NotifyPropertyChanged("Show_HUD_Help");
			Show_HUD_FrontEndBlackout = true;
			Show_HUD_Tutorial = _show_HUD_Tutorial;
			if (value)
			{
				OST_Game_Paused_Vis_Allowed = false;
			}
			else
			{
				OST_Game_Paused_Vis_Allowed = true;
			}
		}
	}

	public bool Show_HUD_Briefing
	{
		get
		{
			return _show_HUD_Briefing;
		}
		set
		{
			_show_HUD_Briefing = value;
			NotifyPropertyChanged("Show_HUD_Briefing");
		}
	}

	public int BriefingViewboxWidth
	{
		get
		{
			return _briefingViewboxWidth;
		}
		set
		{
			if (_briefingViewboxWidth != value)
			{
				_briefingViewboxWidth = value;
				NotifyPropertyChanged("BriefingViewboxWidth");
			}
		}
	}

	public int BriefingViewboxHeight
	{
		get
		{
			return _briefingViewboxHeight;
		}
		set
		{
			if (_briefingViewboxHeight != value)
			{
				_briefingViewboxHeight = value;
				NotifyPropertyChanged("BriefingViewboxHeight");
			}
		}
	}

	public int FrontEndRequesterWidth
	{
		get
		{
			return _frontEndRequesterWidth;
		}
		set
		{
			if (_frontEndRequesterWidth != value)
			{
				_frontEndRequesterWidth = value;
				NotifyPropertyChanged("FrontEndRequesterWidth");
			}
		}
	}

	public int FrontEndRequesterHeight
	{
		get
		{
			return _frontEndRequesterHeight;
		}
		set
		{
			if (_frontEndRequesterHeight != value)
			{
				_frontEndRequesterHeight = value;
				NotifyPropertyChanged("FrontEndRequesterHeight");
			}
		}
	}

	public int FrontEndOptionsWidth
	{
		get
		{
			return _frontEndOptionsWidth;
		}
		set
		{
			if (_frontEndOptionsWidth != value)
			{
				_frontEndOptionsWidth = value;
				NotifyPropertyChanged("FrontEndOptionsWidth");
			}
		}
	}

	public int FrontEndOptionsHeight
	{
		get
		{
			return _frontEndOptionsHeight;
		}
		set
		{
			if (_frontEndOptionsHeight != value)
			{
				_frontEndOptionsHeight = value;
				NotifyPropertyChanged("FrontEndOptionsHeight");
			}
		}
	}

	public int FrontEndHelpWidth
	{
		get
		{
			return _frontEndHelpWidth;
		}
		set
		{
			if (_frontEndHelpWidth != value)
			{
				_frontEndHelpWidth = value;
				NotifyPropertyChanged("FrontEndHelpWidth");
			}
		}
	}

	public int FrontEndHelpHeight
	{
		get
		{
			return _frontEndHelpHeight;
		}
		set
		{
			if (_frontEndHelpHeight != value)
			{
				_frontEndHelpHeight = value;
				NotifyPropertyChanged("FrontEndHelpHeight");
			}
		}
	}

	public int FrontEndConfirmationWidth
	{
		get
		{
			return _frontEndConfirmationWidth;
		}
		set
		{
			if (_frontEndConfirmationWidth != value)
			{
				_frontEndConfirmationWidth = value;
				NotifyPropertyChanged("FrontEndConfirmationWidth");
			}
		}
	}

	public int FrontEndConfirmationHeight
	{
		get
		{
			return _frontEndConfirmationHeight;
		}
		set
		{
			if (_frontEndConfirmationHeight != value)
			{
				_frontEndConfirmationHeight = value;
				NotifyPropertyChanged("FrontEndConfirmationHeight");
			}
		}
	}

	public bool Extra2Visible
	{
		get
		{
			return _extra2Visible;
		}
		set
		{
			if (_extra2Visible != value)
			{
				_extra2Visible = value;
				NotifyPropertyChanged("Extra2Visible");
			}
		}
	}

	public bool Extra3Visible
	{
		get
		{
			return _extra3Visible;
		}
		set
		{
			if (_extra3Visible != value)
			{
				_extra3Visible = value;
				NotifyPropertyChanged("Extra3Visible");
			}
		}
	}

	public bool Extra3NOTVisible
	{
		get
		{
			return _extra3NOTVisible;
		}
		set
		{
			if (_extra3NOTVisible != value)
			{
				_extra3NOTVisible = value;
				NotifyPropertyChanged("Extra3NOTVisible");
			}
		}
	}

	public bool Extra4Visible
	{
		get
		{
			return _extra4Visible;
		}
		set
		{
			if (_extra4Visible != value)
			{
				_extra4Visible = value;
				NotifyPropertyChanged("Extra4Visible");
			}
		}
	}

	public bool Extra4NOTVisible
	{
		get
		{
			return _extra4NOTVisible;
		}
		set
		{
			if (_extra4NOTVisible != value)
			{
				_extra4NOTVisible = value;
				NotifyPropertyChanged("Extra4NOTVisible");
			}
		}
	}

	public double DemoFrontEndButtonFontSize
	{
		get
		{
			return _demoFrontEndButtonFontSize;
		}
		set
		{
			if (_demoFrontEndButtonFontSize != value)
			{
				_demoFrontEndButtonFontSize = value;
				NotifyPropertyChanged("DemoFrontEndButtonFontSize");
			}
		}
	}

	public double FrontEndHomeFiresButtonFontSize
	{
		get
		{
			return _FrontEndHomeFiresButtonFontSize;
		}
		set
		{
			if (_FrontEndHomeFiresButtonFontSize != value)
			{
				_FrontEndHomeFiresButtonFontSize = value;
				NotifyPropertyChanged("FrontEndHomeFiresButtonFontSize");
			}
		}
	}

	public double FrontEndButtonLineHeight
	{
		get
		{
			return _frontEndButtonLineHeight;
		}
		set
		{
			if (_frontEndButtonLineHeight != value)
			{
				_frontEndButtonLineHeight = value;
				NotifyPropertyChanged("FrontEndButtonLineHeight");
			}
		}
	}

	public Visibility ShowTutorialHelpText
	{
		get
		{
			return _showTutorialHelpText;
		}
		set
		{
			if (_showTutorialHelpText != value)
			{
				_showTutorialHelpText = value;
				NotifyPropertyChanged("ShowTutorialHelpText");
			}
		}
	}

	public bool Show_HUD_RightClick
	{
		get
		{
			return _show_HUD_RightClick;
		}
		set
		{
			if (_show_HUD_RightClick != value)
			{
				_show_HUD_RightClick = value;
				NotifyPropertyChanged("Show_HUD_RightClick");
			}
		}
	}

	public bool Show_HUD_Objectives
	{
		get
		{
			return _show_HUD_Objectives;
		}
		set
		{
			if (_show_HUD_Objectives != value)
			{
				_show_HUD_Objectives = value;
				NotifyPropertyChanged("Show_HUD_Objectives");
			}
		}
	}

	public bool Show_HUD_Goods
	{
		get
		{
			return _show_HUD_Goods;
		}
		set
		{
			if (_show_HUD_Goods != value)
			{
				_show_HUD_Goods = value;
				NotifyPropertyChanged("Show_HUD_Goods");
			}
		}
	}

	public bool Show_HUD_Goods_Normal
	{
		get
		{
			return _show_HUD_Goods_Normal;
		}
		set
		{
			if (_show_HUD_Goods_Normal != value)
			{
				_show_HUD_Goods_Normal = value;
				NotifyPropertyChanged("Show_HUD_Goods_Normal");
			}
		}
	}

	public bool Show_HUD_Goods_Trade
	{
		get
		{
			return _show_HUD_Goods_Trade;
		}
		set
		{
			if (_show_HUD_Goods_Trade != value)
			{
				_show_HUD_Goods_Trade = value;
				NotifyPropertyChanged("Show_HUD_Goods_Trade");
			}
		}
	}

	public bool Show_HUD_Extras
	{
		get
		{
			if (_show_HUD_Extras)
			{
				return _show_InGameUI;
			}
			return false;
		}
		set
		{
			if (_show_HUD_Extras != value)
			{
				_show_HUD_Extras = value;
				NotifyPropertyChanged("Show_HUD_Extras");
			}
		}
	}

	public bool Show_HUD_Extras_Button_Objectves
	{
		get
		{
			return _show_HUD_Extras_Button_Objectves;
		}
		set
		{
			if (_show_HUD_Extras_Button_Objectves != value)
			{
				_show_HUD_Extras_Button_Objectves = value;
				NotifyPropertyChanged("Show_HUD_Extras_Button_Objectves");
			}
		}
	}

	public bool Show_HUD_Extras_Button_Freebuild
	{
		get
		{
			return _show_HUD_Extras_Button_Freebuild;
		}
		set
		{
			if (_show_HUD_Extras_Button_Freebuild != value)
			{
				_show_HUD_Extras_Button_Freebuild = value;
				NotifyPropertyChanged("Show_HUD_Extras_Button_Freebuild");
			}
		}
	}

	public bool Show_HUD_Goods_Button_Disabled
	{
		get
		{
			return _show_HUD_Goods_Button_Disabled;
		}
		set
		{
			if (_show_HUD_Goods_Button_Disabled != value)
			{
				_show_HUD_Goods_Button_Disabled = value;
				NotifyPropertyChanged("Show_HUD_Goods_Button_Disabled");
			}
		}
	}

	public bool Show_HUD_IngameMenu
	{
		get
		{
			return _show_HUD_IngameMenu;
		}
		set
		{
			_show_HUD_IngameMenu = value;
			NotifyPropertyChanged("Show_HUD_IngameMenu");
		}
	}

	public bool Show_HUD_Confirmation
	{
		get
		{
			return _show_HUD_Confirmation;
		}
		set
		{
			_show_HUD_Confirmation = value;
			NotifyPropertyChanged("Show_HUD_Confirmation");
			Show_HUD_FrontEndBlackout = true;
		}
	}

	public bool Show_HUD_LoadSaveRequester
	{
		get
		{
			return _show_HUD_LoadSaveRequester;
		}
		set
		{
			_show_HUD_LoadSaveRequester = value;
			NotifyPropertyChanged("Show_HUD_LoadSaveRequester");
			Show_HUD_FrontEndBlackout = true;
		}
	}

	public bool Show_HUD_LoadSaveRequesterMP
	{
		get
		{
			return _show_HUD_LoadSaveRequesterMP;
		}
		set
		{
			_show_HUD_LoadSaveRequesterMP = value;
			NotifyPropertyChanged("Show_HUD_LoadSaveRequesterMP");
		}
	}

	public bool Show_HUD_ConfirmationMP
	{
		get
		{
			return _show_HUD_ConfirmationMP;
		}
		set
		{
			_show_HUD_ConfirmationMP = value;
			NotifyPropertyChanged("Show_HUD_ConfirmationMP");
		}
	}

	public bool Show_HUD_WorkshopUploader
	{
		get
		{
			return _show_HUD_WorkshopUploader;
		}
		set
		{
			_show_HUD_WorkshopUploader = value;
			NotifyPropertyChanged("Show_HUD_WorkshopUploader");
		}
	}

	public bool Show_ActionPoint
	{
		get
		{
			return _show_ActionPoint;
		}
		set
		{
			if (_show_ActionPoint != value)
			{
				_show_ActionPoint = value;
				NotifyPropertyChanged("Show_ActionPoint");
			}
		}
	}

	public bool Show_KeepEnclosed
	{
		get
		{
			return _show_KeepEnclosed;
		}
		set
		{
			if (_show_KeepEnclosed != value)
			{
				_show_KeepEnclosed = value;
				NotifyPropertyChanged("Show_KeepEnclosed");
			}
		}
	}

	public bool Compass_Vis
	{
		get
		{
			return _compass_Vis;
		}
		set
		{
			if (_compass_Vis != value)
			{
				_compass_Vis = value;
				NotifyPropertyChanged("Compass_Vis");
			}
		}
	}

	public string Compass_Margin
	{
		get
		{
			return _compass_Margin;
		}
		set
		{
			if (_compass_Margin != value)
			{
				_compass_Margin = value;
				NotifyPropertyChanged("Compass_Margin");
			}
		}
	}

	public bool Show_HUD_Scenario
	{
		get
		{
			return _show_HUD_Scenario;
		}
		set
		{
			_show_HUD_Scenario = value;
			NotifyPropertyChanged("Show_HUD_Scenario");
		}
	}

	public bool Show_HUD_Scenario_Button
	{
		get
		{
			return _show_HUD_Scenario_Button;
		}
		set
		{
			_show_HUD_Scenario_Button = value;
			NotifyPropertyChanged("Show_HUD_Scenario_Button");
		}
	}

	public bool Show_HUD_ScenarioSpecial
	{
		get
		{
			return _show_HUD_ScenarioSpecial;
		}
		set
		{
			_show_HUD_ScenarioSpecial = value;
			NotifyPropertyChanged("Show_HUD_ScenarioSpecial");
		}
	}

	public bool Show_HUD_FreebuildMenu
	{
		get
		{
			return _show_HUD_FreebuildMenu;
		}
		set
		{
			_show_HUD_FreebuildMenu = value;
			NotifyPropertyChanged("Show_HUD_FreebuildMenu");
		}
	}

	public bool Show_HUD_ControlGroups
	{
		get
		{
			return _show_HUD_ControlGroups;
		}
		set
		{
			_show_HUD_ControlGroups = value;
			NotifyPropertyChanged("Show_HUD_ControlGroups");
		}
	}

	public bool Show_HUD_Options
	{
		get
		{
			return _show_HUD_Options;
		}
		set
		{
			if (_show_HUD_Options != value)
			{
				_show_HUD_Options = value;
				if (!value)
				{
					HUDOptions.Save();
				}
				NotifyPropertyChanged("Show_HUD_Options");
				Show_HUD_FrontEndBlackout = true;
			}
		}
	}

	public bool Show_HUD_FrontEndBlackout
	{
		get
		{
			return _show_HUD_Options | _show_HUD_LoadSaveRequester | _show_HUD_Help | _show_HUD_Confirmation;
		}
		set
		{
			NotifyPropertyChanged("Show_HUD_FrontEndBlackout");
		}
	}

	public bool Show_HUD_FrontEndVideo
	{
		get
		{
			if (_show_FrontMenus)
			{
				if (!_show_CampaignMenu && !_show_EcoCampaignMenu && !_show_Extra1CampaignMenu && !_show_Extra2CampaignMenu && !_show_Extra3CampaignMenu && !_show_Extra4CampaignMenu && !_show_TrailCampaignMenu && !_show_Trail2CampaignMenu && !_show_ExtraEcoCampaignMenu && !_show_StandaloneSetup && !_show_MapEditor)
				{
					return !_show_MultiplayerSetup;
				}
				return false;
			}
			return false;
		}
		set
		{
			NotifyPropertyChanged("Show_HUD_FrontEndVideo");
		}
	}

	public bool Show_HUD_Book
	{
		get
		{
			return _show_HUD_Book;
		}
		set
		{
			_show_HUD_Book = value;
			NotifyPropertyChanged("Show_HUD_Book");
		}
	}

	public bool Show_HUD_Tutorial
	{
		get
		{
			if (_show_HUD_Tutorial)
			{
				return !_show_HUD_Help;
			}
			return false;
		}
		set
		{
			_show_HUD_Tutorial = value;
			NotifyPropertyChanged("Show_HUD_Tutorial");
		}
	}

	public string TutorialHeading
	{
		get
		{
			return _tutorialHeading;
		}
		set
		{
			if (_tutorialHeading != value)
			{
				_tutorialHeading = value;
				NotifyPropertyChanged("TutorialHeading");
			}
		}
	}

	public string TutorialBody
	{
		get
		{
			return _tutorialBody;
		}
		set
		{
			if (_tutorialBody != value)
			{
				_tutorialBody = value;
				NotifyPropertyChanged("TutorialBody");
			}
		}
	}

	public string TutorialButton1Text
	{
		get
		{
			return _tutorialButton1Text;
		}
		set
		{
			if (_tutorialButton1Text != value)
			{
				_tutorialButton1Text = value;
				NotifyPropertyChanged("TutorialButton1Text");
			}
		}
	}

	public string TutorialButton2Text
	{
		get
		{
			return _tutorialButton2Text;
		}
		set
		{
			if (_tutorialButton2Text != value)
			{
				_tutorialButton2Text = value;
				NotifyPropertyChanged("TutorialButton2Text");
			}
		}
	}

	public bool TutorialButton1Visible
	{
		get
		{
			return _tutorialButton1Visible;
		}
		set
		{
			if (_tutorialButton1Visible != value)
			{
				_tutorialButton1Visible = value;
				NotifyPropertyChanged("TutorialButton1Visible");
			}
		}
	}

	public bool TutorialButton2Visible
	{
		get
		{
			return _tutorialButton2Visible;
		}
		set
		{
			if (_tutorialButton2Visible != value)
			{
				_tutorialButton2Visible = value;
				NotifyPropertyChanged("TutorialButton2Visible");
			}
		}
	}

	public bool GuardStanceActive
	{
		get
		{
			return _guardStanceActive;
		}
		set
		{
			if (_guardStanceActive != value)
			{
				_guardStanceActive = value;
				NotifyPropertyChanged("GuardStanceActive");
			}
		}
	}

	public bool DefensiveStanceActive
	{
		get
		{
			return _defensiveStanceActive;
		}
		set
		{
			if (_defensiveStanceActive != value)
			{
				_defensiveStanceActive = value;
				NotifyPropertyChanged("DefensiveStanceActive");
			}
		}
	}

	public bool AggressiveStanceActive
	{
		get
		{
			return _aggressiveStanceActive;
		}
		set
		{
			if (_aggressiveStanceActive != value)
			{
				_aggressiveStanceActive = value;
				NotifyPropertyChanged("AggressiveStanceActive");
			}
		}
	}

	public ImageSource TradePrevGoodsImage
	{
		get
		{
			return _tradePrevGoodsImage;
		}
		set
		{
			_tradePrevGoodsImage = value;
			NotifyPropertyChanged("TradePrevGoodsImage");
		}
	}

	public ImageSource TradeNextGoodsImage
	{
		get
		{
			return _tradeNextGoodsImage;
		}
		set
		{
			_tradeNextGoodsImage = value;
			NotifyPropertyChanged("TradeNextGoodsImage");
		}
	}

	public ImageSource TradeGoodsImage
	{
		get
		{
			return _tradeGoodsImage;
		}
		set
		{
			_tradeGoodsImage = value;
			NotifyPropertyChanged("TradeGoodsImage");
		}
	}

	public ImageSource TradeGoldImage
	{
		get
		{
			return _tradeGoldImage;
		}
		set
		{
			_tradeGoldImage = value;
			NotifyPropertyChanged("TradeGoldImage");
		}
	}

	public string TradeAutoSell
	{
		get
		{
			return _tradeAutoSell;
		}
		set
		{
			if (_tradeAutoSell != value)
			{
				_tradeAutoSell = value;
				NotifyPropertyChanged("TradeAutoSell");
			}
		}
	}

	public string TradeAutoBuy
	{
		get
		{
			return _tradeAutoBuy;
		}
		set
		{
			if (_tradeAutoBuy != value)
			{
				_tradeAutoBuy = value;
				NotifyPropertyChanged("TradeAutoBuy");
			}
		}
	}

	public ImageSource RadarMapImage
	{
		get
		{
			return _radarMapImage;
		}
		set
		{
			_radarMapImage = value;
			NotifyPropertyChanged("RadarMapImage");
		}
	}

	public string RadarMargin
	{
		get
		{
			return _radarMargin;
		}
		set
		{
			_radarMargin = value;
			NotifyPropertyChanged("RadarMargin");
		}
	}

	public string RadarPlusMargin
	{
		get
		{
			return _radarPlusMargin;
		}
		set
		{
			_radarPlusMargin = value;
			NotifyPropertyChanged("RadarPlusMargin");
		}
	}

	public string RadarMinusMargin
	{
		get
		{
			return _radarMinusMargin;
		}
		set
		{
			_radarMinusMargin = value;
			NotifyPropertyChanged("RadarMinusMargin");
		}
	}

	public ImageSource ScribeHeadImage
	{
		get
		{
			return _scribeHeadImage;
		}
		set
		{
			if (_scribeHeadImage != value)
			{
				_scribeHeadImage = value;
				NotifyPropertyChanged("ScribeHeadImage");
			}
		}
	}

	public ImageSource MissionOverImage
	{
		get
		{
			return _missionOverImage;
		}
		set
		{
			_missionOverImage = value;
			NotifyPropertyChanged("MissionOverImage");
		}
	}

	public string MissionBriefingText
	{
		get
		{
			return _missionBriefingText;
		}
		set
		{
			_missionBriefingText = value;
			NotifyPropertyChanged("MissionBriefingText");
		}
	}

	public string MissionStrategyText
	{
		get
		{
			return _missionStrategyText;
		}
		set
		{
			_missionStrategyText = value;
			NotifyPropertyChanged("MissionStrategyText");
		}
	}

	public string MissionHint1Text
	{
		get
		{
			return _missionHint1Text;
		}
		set
		{
			_missionHint1Text = value;
			NotifyPropertyChanged("MissionHint1Text");
		}
	}

	public string MissionHint2Text
	{
		get
		{
			return _missionHint2Text;
		}
		set
		{
			_missionHint2Text = value;
			NotifyPropertyChanged("MissionHint2Text");
		}
	}

	public string MissionHint3Text
	{
		get
		{
			return _missionHint3Text;
		}
		set
		{
			_missionHint3Text = value;
			NotifyPropertyChanged("MissionHint3Text");
		}
	}

	public string MissionHint4Text
	{
		get
		{
			return _missionHint4Text;
		}
		set
		{
			_missionHint4Text = value;
			NotifyPropertyChanged("MissionHint4Text");
		}
	}

	public string MissionHint5Text
	{
		get
		{
			return _missionHint5Text;
		}
		set
		{
			_missionHint5Text = value;
			NotifyPropertyChanged("MissionHint5Text");
		}
	}

	public string MissionHint6Text
	{
		get
		{
			return _missionHint6Text;
		}
		set
		{
			_missionHint6Text = value;
			NotifyPropertyChanged("MissionHint6Text");
		}
	}

	public string BriefingDifficultyText
	{
		get
		{
			return _briefingDifficultyText;
		}
		set
		{
			_briefingDifficultyText = value;
			NotifyPropertyChanged("BriefingDifficultyText");
		}
	}

	public string BriefingRolloverText
	{
		get
		{
			return _briefingRolloverText;
		}
		set
		{
			_briefingRolloverText = value;
			NotifyPropertyChanged("BriefingRolloverText");
		}
	}

	public string BriefingTextMargin
	{
		get
		{
			return _briefingTextMargin;
		}
		set
		{
			_briefingTextMargin = value;
			NotifyPropertyChanged("BriefingTextMargin");
		}
	}

	public string BriefingStrategyTextMargin
	{
		get
		{
			return _briefingStrategyTextMargin;
		}
		set
		{
			_briefingStrategyTextMargin = value;
			NotifyPropertyChanged("BriefingStrategyTextMargin");
		}
	}

	public string BriefingTimerMargin
	{
		get
		{
			return _briefingTimerMargin;
		}
		set
		{
			_briefingTimerMargin = value;
			NotifyPropertyChanged("BriefingTimerMargin");
		}
	}

	public string BriefingStrategyS
	{
		get
		{
			return _briefingStrategyS;
		}
		set
		{
			_briefingStrategyS = value;
			NotifyPropertyChanged("BriefingStrategyS");
		}
	}

	public string BriefingStrategytrategy
	{
		get
		{
			return _briefingStrategytrategy;
		}
		set
		{
			_briefingStrategytrategy = value;
			NotifyPropertyChanged("BriefingStrategytrategy");
		}
	}

	public string BriefingHintsH
	{
		get
		{
			return _briefingHintsH;
		}
		set
		{
			_briefingHintsH = value;
			NotifyPropertyChanged("BriefingHintsH");
		}
	}

	public string BriefingHintsints
	{
		get
		{
			return _briefingHintsints;
		}
		set
		{
			_briefingHintsints = value;
			NotifyPropertyChanged("BriefingHintsints");
		}
	}

	public string BriefingObjectivesO
	{
		get
		{
			return _briefingObjectivesO;
		}
		set
		{
			_briefingObjectivesO = value;
			NotifyPropertyChanged("BriefingObjectivesO");
		}
	}

	public string BriefingObjectivesbjectives
	{
		get
		{
			return _briefingObjectivesbjectives;
		}
		set
		{
			_briefingObjectivesbjectives = value;
			NotifyPropertyChanged("BriefingObjectivesbjectives");
		}
	}

	public string BriefingMissionTitle
	{
		get
		{
			return _briefingMissionTitle;
		}
		set
		{
			_briefingMissionTitle = value;
			NotifyPropertyChanged("BriefingMissionTitle");
		}
	}

	public ImageSource BriefingHelpImage
	{
		get
		{
			return _briefingHelpImage;
		}
		set
		{
			_briefingHelpImage = value;
			NotifyPropertyChanged("BriefingHelpImage");
		}
	}

	public ImageSource MainHelpImage
	{
		get
		{
			return _mainHelpImage;
		}
		set
		{
			_mainHelpImage = value;
			NotifyPropertyChanged("MainHelpImage");
		}
	}

	public string ObjectiveTimerText
	{
		get
		{
			return _objectiveTimerText;
		}
		set
		{
			_objectiveTimerText = value;
			NotifyPropertyChanged("ObjectiveTimerText");
		}
	}

	public int ObjectiveTimerWidth
	{
		get
		{
			return _objectiveTimerWidth;
		}
		set
		{
			_objectiveTimerWidth = value;
			NotifyPropertyChanged("ObjectiveTimerWidth");
		}
	}

	public string IngameMessageLoadButtonText
	{
		get
		{
			return _ingameMessageLoadButtonText;
		}
		set
		{
			_ingameMessageLoadButtonText = value;
			NotifyPropertyChanged("IngameMessageLoadButtonText");
		}
	}

	public string IngameMessageSaveButtonText
	{
		get
		{
			return _ingameMessageSaveButtonText;
		}
		set
		{
			_ingameMessageSaveButtonText = value;
			NotifyPropertyChanged("IngameMessageSaveButtonText");
		}
	}

	public string IngameMessageRestartButtonText
	{
		get
		{
			return _ingameMessageRestartButtonText;
		}
		set
		{
			_ingameMessageRestartButtonText = value;
			NotifyPropertyChanged("IngameMessageRestartButtonText");
		}
	}

	public string IngameMessageQuitButtonText
	{
		get
		{
			return _ingameMessageQuitButtonText;
		}
		set
		{
			_ingameMessageQuitButtonText = value;
			NotifyPropertyChanged("IngameMessageQuitButtonText");
		}
	}

	public string LoadSaveFileName
	{
		get
		{
			return _loadSaveFileName;
		}
		set
		{
			_loadSaveFileName = value;
			NotifyPropertyChanged("LoadSaveFileName");
		}
	}

	public string LoadSaveFilter
	{
		get
		{
			return _loadSaveFilter;
		}
		set
		{
			_loadSaveFilter = value;
			NotifyPropertyChanged("LoadSaveFilter");
		}
	}

	public Visibility LoadSaveFilterLabelVis
	{
		get
		{
			return _loadSaveFilterLabelVis;
		}
		set
		{
			if (_loadSaveFilterLabelVis != value)
			{
				_loadSaveFilterLabelVis = value;
				NotifyPropertyChanged("LoadSaveFilterLabelVis");
			}
		}
	}

	public Visibility LoadSaveFilterButtonVis
	{
		get
		{
			return _loadSaveFilterButtonVis;
		}
		set
		{
			if (_loadSaveFilterButtonVis != value)
			{
				_loadSaveFilterButtonVis = value;
				NotifyPropertyChanged("LoadSaveFilterButtonVis");
			}
		}
	}

	public string ButtonLoadSaveActionText
	{
		get
		{
			return _buttonLoadSaveActionText;
		}
		set
		{
			_buttonLoadSaveActionText = value;
			NotifyPropertyChanged("ButtonLoadSaveActionText");
		}
	}

	public string LoadSave_FolderText
	{
		get
		{
			return _loadSave_FolderText;
		}
		set
		{
			_loadSave_FolderText = value;
			NotifyPropertyChanged("LoadSave_FolderText");
		}
	}

	public ImageSource RadarRequesterImage
	{
		get
		{
			return _radarRequesterImage;
		}
		set
		{
			_radarRequesterImage = value;
			NotifyPropertyChanged("RadarRequesterImage");
		}
	}

	public bool Show_RequesterRadar
	{
		get
		{
			return _show_RequesterRadar;
		}
		set
		{
			if (_show_RequesterRadar != value)
			{
				_show_RequesterRadar = value;
				NotifyPropertyChanged("Show_RequesterRadar");
			}
		}
	}

	public bool SaveSiegeThatLockVisible
	{
		get
		{
			return _saveSiegeThatLockVisible;
		}
		set
		{
			if (_saveSiegeThatLockVisible != value)
			{
				_saveSiegeThatLockVisible = value;
				NotifyPropertyChanged("SaveSiegeThatLockVisible");
			}
		}
	}

	public bool SaveHideQuicksaveVisible
	{
		get
		{
			return _saveHideQuicksaveVisible;
		}
		set
		{
			if (_saveHideQuicksaveVisible != value)
			{
				_saveHideQuicksaveVisible = value;
				NotifyPropertyChanged("SaveHideQuicksaveVisible");
			}
		}
	}

	public int LoadSaveDepthSorting
	{
		get
		{
			return _loadSaveDepthSorting;
		}
		set
		{
			if (_loadSaveDepthSorting != value)
			{
				_loadSaveDepthSorting = value;
				NotifyPropertyChanged("LoadSaveDepthSorting");
			}
		}
	}

	public string MapLowerEdgeMaskHeight
	{
		get
		{
			return _mapLowerEdgeMaskHeight;
		}
		set
		{
			if (_mapLowerEdgeMaskHeight != value)
			{
				_mapLowerEdgeMaskHeight = value;
				NotifyPropertyChanged("MapLowerEdgeMaskHeight");
			}
		}
	}

	public Visibility MapLowerEdgeMaskVisible
	{
		get
		{
			return _mapLowerEdgeMaskVisible;
		}
		set
		{
			if (_mapLowerEdgeMaskVisible != value)
			{
				_mapLowerEdgeMaskVisible = value;
				NotifyPropertyChanged("MapLowerEdgeMaskVisible");
			}
		}
	}

	public ImageSource MapLowerEdgeMaskImage
	{
		get
		{
			return _mapLowerEdgeMaskImage;
		}
		set
		{
			_mapLowerEdgeMaskImage = value;
			NotifyPropertyChanged("MapLowerEdgeMaskImage");
		}
	}

	public string UIScaleValueWidth
	{
		get
		{
			return _UIScaleValueWidth;
		}
		set
		{
			if (_UIScaleValueWidth != value)
			{
				_UIScaleValueWidth = value;
				NotifyPropertyChanged("UIScaleValueWidth");
			}
		}
	}

	public string UIScaleValueHeight
	{
		get
		{
			return _UIScaleValueHeight;
		}
		set
		{
			if (_UIScaleValueHeight != value)
			{
				_UIScaleValueHeight = value;
				NotifyPropertyChanged("UIScaleValueHeight");
			}
		}
	}

	public string ScenarioEditorButtonText
	{
		get
		{
			return _scenarioEditorButtonText;
		}
		set
		{
			if (_scenarioEditorButtonText != value)
			{
				_scenarioEditorButtonText = value;
				NotifyPropertyChanged("ScenarioEditorButtonText");
			}
		}
	}

	public string ScenarioStartingYearText
	{
		get
		{
			return _scenarioStartingYearText;
		}
		set
		{
			if (_scenarioStartingYearText != value)
			{
				_scenarioStartingYearText = value;
				NotifyPropertyChanged("ScenarioStartingYearText");
			}
		}
	}

	public string ScenarioAdjustStartingYearText
	{
		get
		{
			return _scenarioAdjustStartingYearText;
		}
		set
		{
			if (_scenarioAdjustStartingYearText != value)
			{
				_scenarioAdjustStartingYearText = value;
				NotifyPropertyChanged("ScenarioAdjustStartingYearText");
			}
		}
	}

	public string ScenarioStartingMonthText
	{
		get
		{
			return _scenarioStartingMonthText;
		}
		set
		{
			if (_scenarioStartingMonthText != value)
			{
				_scenarioStartingMonthText = value;
				NotifyPropertyChanged("ScenarioStartingMonthText");
			}
		}
	}

	public string ScenarioAdjustStartingMonthText
	{
		get
		{
			return _scenarioAdjustStartingMonthText;
		}
		set
		{
			if (_scenarioAdjustStartingMonthText != value)
			{
				_scenarioAdjustStartingMonthText = value;
				NotifyPropertyChanged("ScenarioAdjustStartingMonthText");
			}
		}
	}

	public string ScenarioEditMessageText
	{
		get
		{
			return _scenarioEditMessageText;
		}
		set
		{
			if (_scenarioEditMessageText != value)
			{
				_scenarioEditMessageText = value;
				NotifyPropertyChanged("ScenarioEditMessageText");
			}
		}
	}

	public string ScenarioAltANSIMessage
	{
		get
		{
			return _scenarioAltANSIMessage;
		}
		set
		{
			if (_scenarioAltANSIMessage != value)
			{
				_scenarioAltANSIMessage = value;
				NotifyPropertyChanged("ScenarioAltANSIMessage");
			}
		}
	}

	public string ScenarioAltUNICODEMessage
	{
		get
		{
			return _scenarioAltUNICODEMessage;
		}
		set
		{
			if (_scenarioAltUNICODEMessage != value)
			{
				_scenarioAltUNICODEMessage = value;
				NotifyPropertyChanged("ScenarioAltUNICODEMessage");
			}
		}
	}

	public Visibility ScenarioMessageAltTextIVisibility
	{
		get
		{
			return _scenarioMessageAltTextIVisibility;
		}
		set
		{
			_scenarioMessageAltTextIVisibility = value;
			NotifyPropertyChanged("ScenarioMessageAltTextIVisibility");
		}
	}

	public bool ScenarioMessageAltTextIVisibilityBool
	{
		get
		{
			return ScenarioMessageAltTextIVisibility == Visibility.Visible;
		}
		set
		{
			if (value)
			{
				ScenarioMessageAltTextIVisibility = Visibility.Visible;
			}
			else
			{
				ScenarioMessageAltTextIVisibility = Visibility.Hidden;
			}
		}
	}

	public string ScenarioStartingPopText
	{
		get
		{
			return _scenarioStartingPopText;
		}
		set
		{
			if (_scenarioStartingPopText != value)
			{
				_scenarioStartingPopText = value;
				NotifyPropertyChanged("ScenarioStartingPopText");
			}
		}
	}

	public Visibility ScenarioStartingSpecial
	{
		get
		{
			return _scenarioStartingSpecialText;
		}
		set
		{
			_scenarioStartingSpecialText = value;
			NotifyPropertyChanged("ScenarioStartingSpecial");
		}
	}

	public Visibility ScenarioNormalUIVisibility
	{
		get
		{
			return _scenarioNormalUIVisibility;
		}
		set
		{
			_scenarioNormalUIVisibility = value;
			NotifyPropertyChanged("ScenarioNormalUIVisibility");
		}
	}

	public Visibility ScenarioSiegeUIVisibility
	{
		get
		{
			return _scenarioSiegeUIVisibility;
		}
		set
		{
			_scenarioSiegeUIVisibility = value;
			NotifyPropertyChanged("ScenarioSiegeUIVisibility");
		}
	}

	public string ScenarioStartingSpecialGoldText
	{
		get
		{
			return _scenarioStartingSpecialGoldText;
		}
		set
		{
			if (_scenarioStartingSpecialGoldText != value)
			{
				_scenarioStartingSpecialGoldText = value;
				NotifyPropertyChanged("ScenarioStartingSpecialGoldText");
			}
		}
	}

	public string ScenarioStartingSpecialRationsText
	{
		get
		{
			return _scenarioStartingSpecialRations;
		}
		set
		{
			if (_scenarioStartingSpecialRations != value)
			{
				_scenarioStartingSpecialRations = value;
				NotifyPropertyChanged("ScenarioStartingSpecialRationsText");
			}
		}
	}

	public string ScenarioStartingSpecialTaxText
	{
		get
		{
			return _scenarioStartingSpecialTax;
		}
		set
		{
			if (_scenarioStartingSpecialTax != value)
			{
				_scenarioStartingSpecialTax = value;
				NotifyPropertyChanged("ScenarioStartingSpecialTaxText");
			}
		}
	}

	public string ScenarioStartingGoldText
	{
		get
		{
			return _scenarioStartingGoldText;
		}
		set
		{
			if (_scenarioStartingGoldText != value)
			{
				_scenarioStartingGoldText = value;
				NotifyPropertyChanged("ScenarioStartingGoldText");
			}
		}
	}

	public string ScenarioStartingPitchText
	{
		get
		{
			return _scenarioStartingPitchText;
		}
		set
		{
			if (_scenarioStartingPitchText != value)
			{
				_scenarioStartingPitchText = value;
				NotifyPropertyChanged("ScenarioStartingPitchText");
			}
		}
	}

	public string ScenarioAdjustedStartingGoodsText
	{
		get
		{
			return _scenarioAdjustedStartingGoodsText;
		}
		set
		{
			if (_scenarioAdjustedStartingGoodsText != value)
			{
				_scenarioAdjustedStartingGoodsText = value;
				NotifyPropertyChanged("ScenarioAdjustedStartingGoodsText");
			}
		}
	}

	public int ScenarioAdjustedStartingGoodsMax
	{
		get
		{
			return _scenarioAdjustedStartingGoodsMax;
		}
		set
		{
			if (_scenarioAdjustedStartingGoodsMax != value)
			{
				_scenarioAdjustedStartingGoodsMax = value;
				NotifyPropertyChanged("ScenarioAdjustedStartingGoodsMax");
			}
		}
	}

	public int ScenarioAdjustedStartingGoodsTickFreq
	{
		get
		{
			return _scenarioAdjustedStartingGoodsTickFreq;
		}
		set
		{
			if (_scenarioAdjustedStartingGoodsTickFreq != value)
			{
				_scenarioAdjustedStartingGoodsTickFreq = value;
				NotifyPropertyChanged("ScenarioAdjustedStartingGoodsTickFreq");
			}
		}
	}

	public string ScenarioAdjustedAttackingForcesText
	{
		get
		{
			return _scenarioAdjustedAttackingForcesText;
		}
		set
		{
			if (_scenarioAdjustedAttackingForcesText != value)
			{
				_scenarioAdjustedAttackingForcesText = value;
				NotifyPropertyChanged("ScenarioAdjustedAttackingForcesText");
			}
		}
	}

	public int ScenarioAdjustedStartingAttackingForceMax
	{
		get
		{
			return _scenarioAdjustedStartingAttackingForceMax;
		}
		set
		{
			if (_scenarioAdjustedStartingAttackingForceMax != value)
			{
				_scenarioAdjustedStartingAttackingForceMax = value;
				NotifyPropertyChanged("ScenarioAdjustedStartingAttackingForceMax");
			}
		}
	}

	public int ScenarioAdjustedStartingAttackingForceFreq
	{
		get
		{
			return _scenarioAdjustedStartingAttackingForceFreq;
		}
		set
		{
			if (_scenarioAdjustedStartingAttackingForceFreq != value)
			{
				_scenarioAdjustedStartingAttackingForceFreq = value;
				NotifyPropertyChanged("ScenarioAdjustedStartingAttackingForceFreq");
			}
		}
	}

	public Visibility ScenarioNewInvasionVisible
	{
		get
		{
			return _scenarioNewInvasionVisible;
		}
		set
		{
			_scenarioNewInvasionVisible = value;
			NotifyPropertyChanged("ScenarioNewInvasionVisible");
		}
	}

	public bool ScenarioNewInvasionVisibleBool
	{
		get
		{
			return ScenarioNewInvasionVisible == Visibility.Visible;
		}
		set
		{
			if (value)
			{
				ScenarioNewInvasionVisible = Visibility.Visible;
			}
			else
			{
				ScenarioNewInvasionVisible = Visibility.Hidden;
			}
		}
	}

	public Visibility ScenarioNewEventMessageVisible
	{
		get
		{
			return _scenarioNewEventMessageVisible;
		}
		set
		{
			_scenarioNewEventMessageVisible = value;
			NotifyPropertyChanged("ScenarioNewEventMessageVisible");
		}
	}

	public bool ScenarioNewEventMessageVisibleBool
	{
		get
		{
			return ScenarioNewEventMessageVisible == Visibility.Visible;
		}
		set
		{
			if (value)
			{
				ScenarioNewEventMessageVisible = Visibility.Visible;
			}
			else
			{
				ScenarioNewEventMessageVisible = Visibility.Hidden;
			}
		}
	}

	public Visibility ScenarioCommonBackVisible
	{
		get
		{
			return _scenarioCommonBackVisible;
		}
		set
		{
			_scenarioCommonBackVisible = value;
			NotifyPropertyChanged("ScenarioCommonBackVisible");
		}
	}

	public bool ScenarioCommonBackVisibleBool
	{
		get
		{
			return ScenarioCommonBackVisible == Visibility.Visible;
		}
		set
		{
			if (value)
			{
				ScenarioCommonBackVisible = Visibility.Visible;
			}
			else
			{
				ScenarioCommonBackVisible = Visibility.Hidden;
			}
		}
	}

	public Visibility ScenarioCommonOKVisible
	{
		get
		{
			return _scenarioCommonOKVisible;
		}
		set
		{
			_scenarioCommonOKVisible = value;
			NotifyPropertyChanged("ScenarioCommonOKVisible");
		}
	}

	public bool ScenarioCommonOKVisibleBool
	{
		get
		{
			return ScenarioCommonOKVisible == Visibility.Visible;
		}
		set
		{
			if (value)
			{
				ScenarioCommonOKVisible = Visibility.Visible;
			}
			else
			{
				ScenarioCommonOKVisible = Visibility.Hidden;
			}
		}
	}

	public Visibility ScenarioCommonDeleteVisible
	{
		get
		{
			return _scenarioCommonDeleteVisible;
		}
		set
		{
			_scenarioCommonDeleteVisible = value;
			NotifyPropertyChanged("ScenarioCommonDeleteVisible");
		}
	}

	public bool ScenarioCommonDeleteVisibleBool
	{
		get
		{
			return ScenarioCommonDeleteVisible == Visibility.Visible;
		}
		set
		{
			if (value)
			{
				ScenarioCommonDeleteVisible = Visibility.Visible;
			}
			else
			{
				ScenarioCommonDeleteVisible = Visibility.Hidden;
			}
		}
	}

	public bool ScenarioCommonEditTeamsVisible
	{
		get
		{
			return _scenarioCommonEditTeamsVisible;
		}
		set
		{
			_scenarioCommonEditTeamsVisible = value;
			NotifyPropertyChanged("ScenarioCommonEditTeamsVisible");
		}
	}

	public bool ScenarioBuildingTogglesVis
	{
		get
		{
			return _scenarioBuildingTogglesVis;
		}
		set
		{
			_scenarioBuildingTogglesVis = value;
			NotifyPropertyChanged("ScenarioBuildingTogglesVis");
		}
	}

	public string ScenarioMessageYearText
	{
		get
		{
			return _scenarioMessageYearText;
		}
		set
		{
			if (_scenarioMessageYearText != value)
			{
				_scenarioMessageYearText = value;
				NotifyPropertyChanged("ScenarioMessageYearText");
			}
		}
	}

	public string ScenarioMessageMonthText
	{
		get
		{
			return _scenarioMessageMonthText;
		}
		set
		{
			if (_scenarioMessageMonthText != value)
			{
				_scenarioMessageMonthText = value;
				NotifyPropertyChanged("ScenarioMessageMonthText");
			}
		}
	}

	public string ScenarioInvasionYearText
	{
		get
		{
			return _scenarioInvasionYearText;
		}
		set
		{
			if (_scenarioInvasionYearText != value)
			{
				_scenarioInvasionYearText = value;
				NotifyPropertyChanged("ScenarioInvasionYearText");
			}
		}
	}

	public string ScenarioInvasionMonthText
	{
		get
		{
			return _scenarioInvasionMonthText;
		}
		set
		{
			if (_scenarioInvasionMonthText != value)
			{
				_scenarioInvasionMonthText = value;
				NotifyPropertyChanged("ScenarioInvasionMonthText");
			}
		}
	}

	public string ScenarioInvasionTotalTroopsText
	{
		get
		{
			return _scenarioInvasionTotalTroopsText;
		}
		set
		{
			if (_scenarioInvasionTotalTroopsText != value)
			{
				_scenarioInvasionTotalTroopsText = value;
				NotifyPropertyChanged("ScenarioInvasionTotalTroopsText");
			}
		}
	}

	public string ScenarioInvasionRepeatText
	{
		get
		{
			return _scenarioInvasionRepeatText;
		}
		set
		{
			if (_scenarioInvasionRepeatText != value)
			{
				_scenarioInvasionRepeatText = value;
				NotifyPropertyChanged("ScenarioInvasionRepeatText");
			}
		}
	}

	public int ScenarioInvasionSizeMax
	{
		get
		{
			return _scenarioInvasionSizeMax;
		}
		set
		{
			if (_scenarioInvasionSizeMax != value)
			{
				_scenarioInvasionSizeMax = value;
				NotifyPropertyChanged("ScenarioInvasionSizeMax");
			}
		}
	}

	public int ScenarioInvasionSizeFreq
	{
		get
		{
			return _scenarioInvasionSizeFreq;
		}
		set
		{
			if (_scenarioInvasionSizeFreq != value)
			{
				_scenarioInvasionSizeFreq = value;
				NotifyPropertyChanged("ScenarioInvasionSizeFreq");
			}
		}
	}

	public string ScenarioInvasionSizeText
	{
		get
		{
			return _scenarioInvasionSizeText;
		}
		set
		{
			if (_scenarioInvasionSizeText != value)
			{
				_scenarioInvasionSizeText = value;
				NotifyPropertyChanged("ScenarioInvasionSizeText");
			}
		}
	}

	public string ScenarioEventYearText
	{
		get
		{
			return _scenarioEventYearText;
		}
		set
		{
			if (_scenarioEventYearText != value)
			{
				_scenarioEventYearText = value;
				NotifyPropertyChanged("ScenarioEventYearText");
			}
		}
	}

	public string ScenarioEventMonthText
	{
		get
		{
			return _scenarioEventMonthText;
		}
		set
		{
			if (_scenarioEventMonthText != value)
			{
				_scenarioEventMonthText = value;
				NotifyPropertyChanged("ScenarioEventMonthText");
			}
		}
	}

	public string ScenarioEventActionTitle
	{
		get
		{
			return _scenarioEventActionTitle;
		}
		set
		{
			if (_scenarioEventActionTitle != value)
			{
				_scenarioEventActionTitle = value;
				NotifyPropertyChanged("ScenarioEventActionTitle");
			}
		}
	}

	public string ScenarioEventActionMessage
	{
		get
		{
			return _scenarioEventActionMessage;
		}
		set
		{
			if (_scenarioEventActionMessage != value)
			{
				_scenarioEventActionMessage = value;
				NotifyPropertyChanged("ScenarioEventActionMessage");
			}
		}
	}

	public string ScenarioTradeTextSize
	{
		get
		{
			return _scenarioTradeTextSize;
		}
		set
		{
			if (_scenarioTradeTextSize != value)
			{
				_scenarioTradeTextSize = value;
				NotifyPropertyChanged("ScenarioTradeTextSize");
			}
		}
	}

	public string ScenarioTradeTextHeight
	{
		get
		{
			return _scenarioTradeTextHeight;
		}
		set
		{
			if (_scenarioTradeTextHeight != value)
			{
				_scenarioTradeTextHeight = value;
				NotifyPropertyChanged("ScenarioTradeTextHeight");
			}
		}
	}

	public string ScenarioEventConditionTitle
	{
		get
		{
			return _scenarioEventConditionTitle;
		}
		set
		{
			if (_scenarioEventConditionTitle != value)
			{
				_scenarioEventConditionTitle = value;
				NotifyPropertyChanged("ScenarioEventConditionTitle");
			}
		}
	}

	public Visibility ScenarioEventActionMessageVisible
	{
		get
		{
			return _scenarioEventActionMessageVisible;
		}
		set
		{
			_scenarioEventActionMessageVisible = value;
			NotifyPropertyChanged("ScenarioEventActionMessageVisible");
		}
	}

	public bool ScenarioEventActionMessageVisibleBool
	{
		get
		{
			return ScenarioEventActionMessageVisible == Visibility.Visible;
		}
		set
		{
			if (value)
			{
				ScenarioEventActionMessageVisible = Visibility.Visible;
			}
			else
			{
				ScenarioEventActionMessageVisible = Visibility.Hidden;
			}
		}
	}

	public Visibility ScenarioEventActionRepeatVisible
	{
		get
		{
			return _scenarioEventActionRepeatVisible;
		}
		set
		{
			_scenarioEventActionRepeatVisible = value;
			NotifyPropertyChanged("ScenarioEventActionRepeatVisible");
		}
	}

	public bool ScenarioEventActionRepeatVisibleBool
	{
		get
		{
			return ScenarioEventActionRepeatVisible == Visibility.Visible;
		}
		set
		{
			if (value)
			{
				ScenarioEventActionRepeatVisible = Visibility.Visible;
			}
			else
			{
				ScenarioEventActionRepeatVisible = Visibility.Hidden;
			}
		}
	}

	public Visibility ScenarioEventActionValueVisible
	{
		get
		{
			return _scenarioEventActionValueVisible;
		}
		set
		{
			_scenarioEventActionValueVisible = value;
			NotifyPropertyChanged("ScenarioEventActionValueVisible");
		}
	}

	public bool ScenarioEventActionValueVisibleBool
	{
		get
		{
			return ScenarioEventActionValueVisible == Visibility.Visible;
		}
		set
		{
			if (value)
			{
				ScenarioEventActionValueVisible = Visibility.Visible;
			}
			else
			{
				ScenarioEventActionValueVisible = Visibility.Hidden;
			}
		}
	}

	public Visibility ScenarioEventActionValue2Visible
	{
		get
		{
			return _scenarioEventActionValue2Visible;
		}
		set
		{
			_scenarioEventActionValue2Visible = value;
			NotifyPropertyChanged("ScenarioEventActionValue2Visible");
		}
	}

	public bool ScenarioEventActionValue2VisibleBool
	{
		get
		{
			return ScenarioEventActionValue2Visible == Visibility.Visible;
		}
		set
		{
			if (value)
			{
				ScenarioEventActionValue2Visible = Visibility.Visible;
			}
			else
			{
				ScenarioEventActionValue2Visible = Visibility.Hidden;
			}
		}
	}

	public string ActionRepeatMonthsText
	{
		get
		{
			return _actionRepeatMonthsText;
		}
		set
		{
			if (_actionRepeatMonthsText != value)
			{
				_actionRepeatMonthsText = value;
				NotifyPropertyChanged("ActionRepeatMonthsText");
			}
		}
	}

	public string ActionRepeatText
	{
		get
		{
			return _actionRepeatText;
		}
		set
		{
			if (_actionRepeatText != value)
			{
				_actionRepeatText = value;
				NotifyPropertyChanged("ActionRepeatText");
			}
		}
	}

	public string ActionValueText
	{
		get
		{
			return _actionValueText;
		}
		set
		{
			if (_actionValueText != value)
			{
				_actionValueText = value;
				NotifyPropertyChanged("ActionValueText");
			}
		}
	}

	public int ActionValueMin
	{
		get
		{
			return _actionValueMin;
		}
		set
		{
			if (_actionValueMin != value)
			{
				_actionValueMin = value;
				NotifyPropertyChanged("ActionValueMin");
			}
		}
	}

	public int ActionValueMax
	{
		get
		{
			return _actionValueMax;
		}
		set
		{
			if (_actionValueMax != value)
			{
				_actionValueMax = value;
				NotifyPropertyChanged("ActionValueMax");
			}
		}
	}

	public int ActionValueFreq
	{
		get
		{
			return _actionValueFreq;
		}
		set
		{
			if (_actionValueFreq != value)
			{
				_actionValueFreq = value;
				NotifyPropertyChanged("ActionValueFreq");
			}
		}
	}

	public string ActionValueNameText
	{
		get
		{
			return _actionValueNameText;
		}
		set
		{
			if (_actionValueNameText != value)
			{
				_actionValueNameText = value;
				NotifyPropertyChanged("ActionValueNameText");
			}
		}
	}

	public string ActionValue2Text
	{
		get
		{
			return _actionValue2Text;
		}
		set
		{
			if (_actionValue2Text != value)
			{
				_actionValue2Text = value;
				NotifyPropertyChanged("ActionValue2Text");
			}
		}
	}

	public int ActionValue2Max
	{
		get
		{
			return _actionValue2Max;
		}
		set
		{
			if (_actionValue2Max != value)
			{
				_actionValue2Max = value;
				NotifyPropertyChanged("ActionValue2Max");
			}
		}
	}

	public int ActionValue2Min
	{
		get
		{
			return _actionValue2Min;
		}
		set
		{
			if (_actionValue2Min != value)
			{
				_actionValue2Min = value;
				NotifyPropertyChanged("ActionValue2Min");
			}
		}
	}

	public int ActionValue2Freq
	{
		get
		{
			return _actionValue2Freq;
		}
		set
		{
			if (_actionValue2Freq != value)
			{
				_actionValue2Freq = value;
				NotifyPropertyChanged("ActionValue2Freq");
			}
		}
	}

	public string ActionValue2NameText
	{
		get
		{
			return _actionValue2NameText;
		}
		set
		{
			if (_actionValue2NameText != value)
			{
				_actionValue2NameText = value;
				NotifyPropertyChanged("ActionValue2NameText");
			}
		}
	}

	public Visibility ScenarionEventConditionToggleVisible
	{
		get
		{
			return _scenarionEventConditionToggleVisible;
		}
		set
		{
			_scenarionEventConditionToggleVisible = value;
			NotifyPropertyChanged("ScenarionEventConditionToggleVisible");
		}
	}

	public bool ScenarionEventConditionToggleVisibleBool
	{
		get
		{
			return ScenarionEventConditionToggleVisible == Visibility.Visible;
		}
		set
		{
			if (value)
			{
				ScenarionEventConditionToggleVisible = Visibility.Visible;
			}
			else
			{
				ScenarionEventConditionToggleVisible = Visibility.Hidden;
			}
		}
	}

	public Visibility ScenarionEventConditionToggleColourVisible
	{
		get
		{
			return _scenarionEventConditionToggleColourVisible;
		}
		set
		{
			_scenarionEventConditionToggleColourVisible = value;
			NotifyPropertyChanged("ScenarionEventConditionToggleColourVisible");
		}
	}

	public bool ScenarionEventConditionToggleColourVisibleBool
	{
		get
		{
			return ScenarionEventConditionToggleColourVisible == Visibility.Visible;
		}
		set
		{
			if (value)
			{
				ScenarionEventConditionToggleColourVisible = Visibility.Visible;
			}
			else
			{
				ScenarionEventConditionToggleColourVisible = Visibility.Hidden;
			}
		}
	}

	public SolidColorBrush ScenarionEventConditionToggleColour
	{
		get
		{
			return _scenarionEventConditionToggleColour;
		}
		set
		{
			if (_scenarionEventConditionToggleColour != value)
			{
				_scenarionEventConditionToggleColour = value;
				NotifyPropertyChanged("ScenarionEventConditionToggleColour");
			}
		}
	}

	public Visibility ScenarioEventConditionValueVisible
	{
		get
		{
			return _scenarioEventConditionValueVisible;
		}
		set
		{
			_scenarioEventConditionValueVisible = value;
			NotifyPropertyChanged("ScenarioEventConditionValueVisible");
		}
	}

	public bool ScenarioEventConditionValueVisibleBool
	{
		get
		{
			return ScenarioEventConditionValueVisible == Visibility.Visible;
		}
		set
		{
			if (value)
			{
				ScenarioEventConditionValueVisible = Visibility.Visible;
			}
			else
			{
				ScenarioEventConditionValueVisible = Visibility.Hidden;
			}
		}
	}

	public Visibility ScenarionEventConditionOnOffVisible
	{
		get
		{
			return _scenarionEventConditionOnOffVisible;
		}
		set
		{
			_scenarionEventConditionOnOffVisible = value;
			NotifyPropertyChanged("ScenarionEventConditionOnOffVisible");
		}
	}

	public bool ScenarionEventConditionOnOffVisibleBool
	{
		get
		{
			return ScenarionEventConditionOnOffVisible == Visibility.Visible;
		}
		set
		{
			if (value)
			{
				ScenarionEventConditionOnOffVisible = Visibility.Visible;
			}
			else
			{
				ScenarionEventConditionOnOffVisible = Visibility.Hidden;
			}
		}
	}

	public string ScenarionEventConditionToggleText
	{
		get
		{
			return _scenarionEventConditionToggleText;
		}
		set
		{
			if (_scenarionEventConditionToggleText != value)
			{
				_scenarionEventConditionToggleText = value;
				NotifyPropertyChanged("ScenarionEventConditionToggleText");
			}
		}
	}

	public string ScenarionEventConditionOnOffText
	{
		get
		{
			return _scenarionEventConditionOnOffText;
		}
		set
		{
			if (_scenarionEventConditionOnOffText != value)
			{
				_scenarionEventConditionOnOffText = value;
				NotifyPropertyChanged("ScenarionEventConditionOnOffText");
			}
		}
	}

	public string ScenarionEventConditionAndOrText
	{
		get
		{
			return _scenarionEventConditionAndOrText;
		}
		set
		{
			if (_scenarionEventConditionAndOrText != value)
			{
				_scenarionEventConditionAndOrText = value;
				NotifyPropertyChanged("ScenarionEventConditionAndOrText");
			}
		}
	}

	public string ConditionValueText
	{
		get
		{
			return _conditionValueText;
		}
		set
		{
			if (_conditionValueText != value)
			{
				_conditionValueText = value;
				NotifyPropertyChanged("ConditionValueText");
			}
		}
	}

	public string ConditionValueNameText
	{
		get
		{
			return _conditionValueNameText;
		}
		set
		{
			if (_conditionValueNameText != value)
			{
				_conditionValueNameText = value;
				NotifyPropertyChanged("ConditionValueNameText");
			}
		}
	}

	public int ConditionValueMax
	{
		get
		{
			return _conditionValueMax;
		}
		set
		{
			if (_conditionValueMax != value)
			{
				_conditionValueMax = value;
				NotifyPropertyChanged("ConditionValueMax");
			}
		}
	}

	public int ConditionValueMin
	{
		get
		{
			return _conditionValueMin;
		}
		set
		{
			if (_conditionValueMin != value)
			{
				_conditionValueMin = value;
				NotifyPropertyChanged("ConditionValueMin");
			}
		}
	}

	public int ConditionValueFreq
	{
		get
		{
			return _conditionValueFreq;
		}
		set
		{
			if (_conditionValueFreq != value)
			{
				_conditionValueFreq = value;
				NotifyPropertyChanged("ConditionValueFreq");
			}
		}
	}

	public string ButtonMapSettingsText
	{
		get
		{
			return _buttonMapSettingsText;
		}
		set
		{
			if (_buttonMapSettingsText != value)
			{
				_buttonMapSettingsText = value;
				NotifyPropertyChanged("ButtonMapSettingsText");
			}
		}
	}

	public string ScenarioPopup_GameTimeText
	{
		get
		{
			return _scenarioPopup_GameTimeText;
		}
		set
		{
			if (_scenarioPopup_GameTimeText != value)
			{
				_scenarioPopup_GameTimeText = value;
				NotifyPropertyChanged("ScenarioPopup_GameTimeText");
			}
		}
	}

	public bool ButBordScenEdit160HL
	{
		get
		{
			return _butBordScenEdit160HL;
		}
		set
		{
			_butBordScenEdit160HL = value;
			NotifyPropertyChanged("ButBordScenEdit160HL");
		}
	}

	public bool ButBordScenEdit200HL
	{
		get
		{
			return _butBordScenEdit200HL;
		}
		set
		{
			_butBordScenEdit200HL = value;
			NotifyPropertyChanged("ButBordScenEdit200HL");
		}
	}

	public bool ButBordScenEdit300HL
	{
		get
		{
			return _butBordScenEdit300HL;
		}
		set
		{
			_butBordScenEdit300HL = value;
			NotifyPropertyChanged("ButBordScenEdit300HL");
		}
	}

	public bool ButBordScenEdit400HL
	{
		get
		{
			return _butBordScenEdit400HL;
		}
		set
		{
			_butBordScenEdit400HL = value;
			NotifyPropertyChanged("ButBordScenEdit400HL");
		}
	}

	public string ButtonScenarioEditSPMP
	{
		get
		{
			return _buttonScenarioEditSPMP;
		}
		set
		{
			if (_buttonScenarioEditSPMP != value)
			{
				_buttonScenarioEditSPMP = value;
				NotifyPropertyChanged("ButtonScenarioEditSPMP");
			}
		}
	}

	public bool ButtonScenarioEditSinglePlayerVis
	{
		get
		{
			return _buttonScenarioEditSinglePlayerVis;
		}
		set
		{
			_buttonScenarioEditSinglePlayerVis = value;
			NotifyPropertyChanged("ButtonScenarioEditSinglePlayerVis");
		}
	}

	public bool ButtonScenarioEditMultiPlayerVis
	{
		get
		{
			return _buttonScenarioEditMultiPlayerVis;
		}
		set
		{
			_buttonScenarioEditMultiPlayerVis = value;
			NotifyPropertyChanged("ButtonScenarioEditMultiPlayerVis");
		}
	}

	public bool ButBordScenEditSPSiege
	{
		get
		{
			return _butBordScenEditSPSiege;
		}
		set
		{
			_butBordScenEditSPSiege = value;
			NotifyPropertyChanged("ButBordScenEditSPSiege");
		}
	}

	public bool ButBordScenEditSPInvasion
	{
		get
		{
			return _butBordScenEditSPInvasion;
		}
		set
		{
			_butBordScenEditSPInvasion = value;
			NotifyPropertyChanged("ButBordScenEditSPInvasion");
		}
	}

	public bool ButBordScenEditSPEco
	{
		get
		{
			return _butBordScenEditSPEco;
		}
		set
		{
			_butBordScenEditSPEco = value;
			NotifyPropertyChanged("ButBordScenEditSPEco");
		}
	}

	public bool ButBordScenEditSPFreeBuild
	{
		get
		{
			return _butBordScenEditSPFreeBuild;
		}
		set
		{
			_butBordScenEditSPFreeBuild = value;
			NotifyPropertyChanged("ButBordScenEditSPFreeBuild");
		}
	}

	public bool ButBordScenEditMultiPlayerKoth
	{
		get
		{
			return _butBordScenEditMultiPlayerKoth;
		}
		set
		{
			_butBordScenEditMultiPlayerKoth = value;
			NotifyPropertyChanged("ButBordScenEditMultiPlayerKoth");
		}
	}

	public ObservableCollection<int> StartingGoods { get; private set; }

	public ObservableCollection<string> TradingGoods { get; private set; }

	public ObservableCollection<bool> TradingGoodsBool { get; private set; }

	public ObservableCollection<int> AllStoredGoods { get; private set; }

	public ObservableCollection<bool> AllStoredGoodsVisible { get; private set; }

	public ObservableCollection<bool> AllAutoTradingGoodsVisible { get; private set; }

	public ObservableCollection<int> AllTroops { get; private set; }

	public ObservableCollection<string> GoodsPrices { get; private set; }

	public ObservableCollection<int> AttackingForces { get; private set; }

	public ObservableCollection<int> AttackingForcesSiege { get; private set; }

	public ObservableCollection<bool> MessageButtonHighlight { get; private set; }

	public ObservableCollection<bool> InvasionButtonHighlight { get; private set; }

	public ObservableCollection<bool> InvasionSpawnMarkersHighlight { get; private set; }

	public ObservableCollection<string> InvasionSize { get; private set; }

	public ObservableCollection<string> EventTextList { get; private set; }

	public ObservableCollection<string> ScenarioEditTeams { get; private set; }

	public ObservableCollection<bool> MarkerSelected { get; private set; }

	public ObservableCollection<bool> DisplayHudGoodsBool { get; private set; }

	public ObservableCollection<string> FeedInGoodsAmountList { get; private set; }

	public ObservableCollection<bool> FeedInGoodsVisible { get; private set; }

	public ObservableCollection<string> OST_KOTH_Name { get; private set; }

	public ObservableCollection<string> OST_KOTH_Value { get; private set; }

	public ObservableCollection<bool> OST_KOTH_Visible { get; private set; }

	public ObservableCollection<SolidColorBrush> OST_KOTH_Color { get; private set; }

	public ObservableCollection<string> OST_Ping_Name { get; private set; }

	public ObservableCollection<string> OST_Ping_Value { get; private set; }

	public ObservableCollection<bool> OST_Ping_Visible { get; private set; }

	public ObservableCollection<SolidColorBrush> OST_Ping_Color { get; private set; }

	public ObservableCollection<SolidColorBrush> OST_Ping_Value_Color { get; private set; }

	public ObservableCollection<SolidColorBrush> MPChat_Colours { get; private set; }

	public ObservableCollection<string> MPChat_Names { get; private set; }

	public ObservableCollection<string> MPChat_Text { get; private set; }

	public ObservableCollection<bool> MPChat_Rows { get; private set; }

	public ObservableCollection<Visibility> FreebuildEventBorders { get; private set; }

	public ObservableCollection<string> FreebuildInvasionSize { get; private set; }

	public ObservableCollection<Visibility> OptionsSectionsBorders { get; private set; }

	public ObservableCollection<Visibility> CampaignMenuButtonBorders { get; private set; }

	public ObservableCollection<Visibility> EcoCampaignMenuButtonBorders { get; private set; }

	public ObservableCollection<Visibility> ExtraCampaignMenuButtonBorders { get; private set; }

	public ObservableCollection<Visibility> TrailCampaignMenuButtonBorders { get; private set; }

	public ObservableCollection<Visibility> CampaignMenuButtonsVisible { get; private set; }

	public string BuildingTitle
	{
		get
		{
			return _buildingTitle;
		}
		set
		{
			if (_buildingTitle != value)
			{
				_buildingTitle = value;
				NotifyPropertyChanged("BuildingTitle");
			}
		}
	}

	public string BuildingTitle_Margin
	{
		get
		{
			return _buildingTitle_Margin;
		}
		set
		{
			if (_buildingTitle_Margin != value)
			{
				_buildingTitle_Margin = value;
				NotifyPropertyChanged("BuildingTitle_Margin");
			}
		}
	}

	public double BuildingTitleFontSize
	{
		get
		{
			return _buildingTitleFontSize;
		}
		set
		{
			if (_buildingTitleFontSize != value)
			{
				_buildingTitleFontSize = value;
				NotifyPropertyChanged("BuildingTitleFontSize");
			}
		}
	}

	public string BuildingLine1Text
	{
		get
		{
			return _buildingLine1Text;
		}
		set
		{
			if (_buildingLine1Text != value)
			{
				_buildingLine1Text = value;
				NotifyPropertyChanged("BuildingLine1Text");
			}
		}
	}

	public string BuildingLine2Text
	{
		get
		{
			return _buildingLine2Text;
		}
		set
		{
			if (_buildingLine2Text != value)
			{
				_buildingLine2Text = value;
				NotifyPropertyChanged("BuildingLine2Text");
			}
		}
	}

	public string BuildingLine3Text
	{
		get
		{
			return _buildingLine3Text;
		}
		set
		{
			if (_buildingLine3Text != value)
			{
				_buildingLine3Text = value;
				NotifyPropertyChanged("BuildingLine3Text");
			}
		}
	}

	public int GranaryFoodBarWidth
	{
		get
		{
			return _granaryFoodBarWidth;
		}
		set
		{
			if (_granaryFoodBarWidth != value)
			{
				_granaryFoodBarWidth = value;
				NotifyPropertyChanged("GranaryFoodBarWidth");
			}
		}
	}

	public string InGranaryUnitsOfFoodText
	{
		get
		{
			return _inGranaryUnitsOfFoodText;
		}
		set
		{
			if (_inGranaryUnitsOfFoodText != value)
			{
				_inGranaryUnitsOfFoodText = value;
				NotifyPropertyChanged("InGranaryUnitsOfFoodText");
			}
		}
	}

	public string InGranaryMonthsOfFoodText
	{
		get
		{
			return _inGranaryMonthsOfFoodText;
		}
		set
		{
			if (_inGranaryMonthsOfFoodText != value)
			{
				_inGranaryMonthsOfFoodText = value;
				NotifyPropertyChanged("InGranaryMonthsOfFoodText");
			}
		}
	}

	public string InGranaryTypesPopFoodText
	{
		get
		{
			return _inGranaryTypesPopFoodText;
		}
		set
		{
			if (value == "")
			{
				_inGranaryTypesPopFoodText = value;
				HUDBuildingPanel.RefWGTFoodTypePop.SetPopHead(0, visible: false);
			}
			else
			{
				int num = int.Parse(value, Director.defaultCulture);
				if (num > 0)
				{
					value = "+" + value;
				}
				_inGranaryTypesPopFoodText = value;
				HUDBuildingPanel.RefWGTFoodTypePop.SetPopHead(num, visible: true);
			}
			NotifyPropertyChanged("InGranaryTypesPopFoodText");
		}
	}

	public string InGranaryTypesOfFoodText
	{
		get
		{
			return _inGranaryTypesOfFoodText;
		}
		set
		{
			if (_inGranaryTypesOfFoodText != value)
			{
				_inGranaryTypesOfFoodText = value;
				NotifyPropertyChanged("InGranaryTypesOfFoodText");
			}
		}
	}

	public string InGranaryRationsPopText
	{
		get
		{
			return _inGranaryRationsPopText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_inGranaryRationsPopText = value;
			HUDBuildingPanel.RefWGTRationsPop.SetPopHead(num, visible: true);
			NotifyPropertyChanged("InGranaryRationsPopText");
		}
	}

	public string InGranaryRationLevelText
	{
		get
		{
			return _inGranaryRationLevelText;
		}
		set
		{
			if (_inGranaryRationLevelText != value)
			{
				_inGranaryRationLevelText = value;
				NotifyPropertyChanged("InGranaryRationLevelText");
			}
		}
	}

	public bool GotMarketVis
	{
		get
		{
			return _gotMarketVis;
		}
		set
		{
			if (_gotMarketVis != value)
			{
				_gotMarketVis = value;
				NotifyPropertyChanged("GotMarketVis");
			}
		}
	}

	public string InInnFlagonsOfAleText
	{
		get
		{
			return _inInnFlagonsOfAleText;
		}
		set
		{
			if (_inInnFlagonsOfAleText != value)
			{
				_inInnFlagonsOfAleText = value;
				NotifyPropertyChanged("InInnFlagonsOfAleText");
			}
		}
	}

	public string InInnBarrelsOfAleText
	{
		get
		{
			return _inInnBarrelsOfAleText;
		}
		set
		{
			if (_inInnBarrelsOfAleText != value)
			{
				_inInnBarrelsOfAleText = value;
				NotifyPropertyChanged("InInnBarrelsOfAleText");
			}
		}
	}

	public string InInnWorkingInnsText
	{
		get
		{
			return _inInnWorkingInnsText;
		}
		set
		{
			if (_inInnWorkingInnsText != value)
			{
				_inInnWorkingInnsText = value;
				NotifyPropertyChanged("InInnWorkingInnsText");
			}
		}
	}

	public string InInnPopularityText
	{
		get
		{
			return _inInnPopularityText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_inInnPopularityText = value;
			HUDBuildingPanel.RefWGTInnPop.SetPopHead(num, visible: true);
			NotifyPropertyChanged("InInnPopularityText");
		}
	}

	public string InInnCoverageText
	{
		get
		{
			return _inInnCoverageText;
		}
		set
		{
			if (_inInnCoverageText != value)
			{
				_inInnCoverageText = value;
				NotifyPropertyChanged("InInnCoverageText");
			}
		}
	}

	public string InInnNextLevelText
	{
		get
		{
			return _inInnNextLevelText;
		}
		set
		{
			if (_inInnNextLevelText != value)
			{
				_inInnNextLevelText = value;
				NotifyPropertyChanged("InInnNextLevelText");
			}
		}
	}

	public string InKeepIncomeText
	{
		get
		{
			return _inKeepIncomeText;
		}
		set
		{
			if (_inKeepIncomeText != value)
			{
				_inKeepIncomeText = value;
				NotifyPropertyChanged("InKeepIncomeText");
			}
		}
	}

	public string InKeepPopulationText
	{
		get
		{
			return _inKeepPopulationText;
		}
		set
		{
			if (_inKeepPopulationText != value)
			{
				_inKeepPopulationText = value;
				NotifyPropertyChanged("InKeepPopulationText");
			}
		}
	}

	public string InKeepTaxRateText
	{
		get
		{
			return _inKeepTaxRateText;
		}
		set
		{
			if (_inKeepTaxRateText != value)
			{
				_inKeepTaxRateText = value;
				NotifyPropertyChanged("InKeepTaxRateText");
			}
		}
	}

	public string InKeepTaxPopText
	{
		get
		{
			return _inKeepTaxPopText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_inKeepTaxPopText = value;
			HUDBuildingPanel.RefWGTTaxPop.SetPopHead(num, visible: true);
			NotifyPropertyChanged("InKeepTaxPopText");
		}
	}

	public int InKeepSliderPos
	{
		get
		{
			return _inKeepSliderPos;
		}
		set
		{
			if (_inKeepSliderPos != value)
			{
				_inKeepSliderPos = value;
				NotifyPropertyChanged("InKeepSliderPos");
			}
		}
	}

	public string BuildingHPText
	{
		get
		{
			return _buildingHPText;
		}
		set
		{
			if (_buildingHPText != value)
			{
				_buildingHPText = value;
				NotifyPropertyChanged("BuildingHPText");
			}
		}
	}

	public int BuildingRepairHPWidth
	{
		get
		{
			return _buildingRepairHPWidth;
		}
		set
		{
			if (_buildingRepairHPWidth != value)
			{
				_buildingRepairHPWidth = value;
				NotifyPropertyChanged("BuildingRepairHPWidth");
			}
		}
	}

	public string TroopCostText
	{
		get
		{
			return _troopCostText;
		}
		set
		{
			if (_troopCostText != value)
			{
				_troopCostText = value;
				NotifyPropertyChanged("TroopCostText");
			}
		}
	}

	public string TroopNameCostText
	{
		get
		{
			return _troopNameCostText;
		}
		set
		{
			if (_troopNameCostText != value)
			{
				_troopNameCostText = value;
				NotifyPropertyChanged("TroopNameCostText");
			}
		}
	}

	public string TroopNameCostText2
	{
		get
		{
			return _troopNameCostText2;
		}
		set
		{
			if (_troopNameCostText2 != value)
			{
				_troopNameCostText2 = value;
				NotifyPropertyChanged("TroopNameCostText2");
			}
		}
	}

	public string TroopShiftCostText
	{
		get
		{
			return _troopShiftCostText;
		}
		set
		{
			if (_troopShiftCostText != value)
			{
				_troopShiftCostText = value;
				NotifyPropertyChanged("TroopShiftCostText");
			}
		}
	}

	public string TroopCtrlCostText
	{
		get
		{
			return _troopCtrlCostText;
		}
		set
		{
			if (_troopCtrlCostText != value)
			{
				_troopCtrlCostText = value;
				NotifyPropertyChanged("TroopCtrlCostText");
			}
		}
	}

	public string AvailablePeasantText
	{
		get
		{
			return _availablePeasantText;
		}
		set
		{
			if (_availablePeasantText != value)
			{
				_availablePeasantText = value;
				NotifyPropertyChanged("AvailablePeasantText");
			}
		}
	}

	public string WorkshopProducingText
	{
		get
		{
			return _workshopProducingText;
		}
		set
		{
			if (_workshopProducingText != value)
			{
				_workshopProducingText = value;
				NotifyPropertyChanged("WorkshopProducingText");
			}
		}
	}

	public string WorkshopProducingNextText
	{
		get
		{
			return _workshopProducingNextText;
		}
		set
		{
			if (_workshopProducingNextText != value)
			{
				_workshopProducingNextText = value;
				NotifyPropertyChanged("WorkshopProducingNextText");
			}
		}
	}

	public string BarracksHorsesAvailableText
	{
		get
		{
			return _barracksHorsesAvailableText;
		}
		set
		{
			if (_barracksHorsesAvailableText != value)
			{
				_barracksHorsesAvailableText = value;
				NotifyPropertyChanged("BarracksHorsesAvailableText");
			}
		}
	}

	public string WeddingGossipText
	{
		get
		{
			return _weddingGossipText;
		}
		set
		{
			if (_weddingGossipText != value)
			{
				_weddingGossipText = value;
				NotifyPropertyChanged("WeddingGossipText");
			}
		}
	}

	public string GroomNameText
	{
		get
		{
			return _groomNameText;
		}
		set
		{
			if (_groomNameText != value)
			{
				_groomNameText = value;
				NotifyPropertyChanged("GroomNameText");
			}
		}
	}

	public string BrideNameText
	{
		get
		{
			return _brideNameText;
		}
		set
		{
			if (_brideNameText != value)
			{
				_brideNameText = value;
				NotifyPropertyChanged("BrideNameText");
			}
		}
	}

	public string TradeGoodsAmountText
	{
		get
		{
			return _tradeGoodsAmountText;
		}
		set
		{
			if (_tradeGoodsAmountText != value)
			{
				_tradeGoodsAmountText = value;
				NotifyPropertyChanged("TradeGoodsAmountText");
			}
		}
	}

	public string BuyText
	{
		get
		{
			return _buyText;
		}
		set
		{
			if (_buyText != value)
			{
				_buyText = value;
				NotifyPropertyChanged("BuyText");
			}
		}
	}

	public string SellText
	{
		get
		{
			return _sellText;
		}
		set
		{
			if (_sellText != value)
			{
				_sellText = value;
				NotifyPropertyChanged("SellText");
			}
		}
	}

	public string BuySellFontSize
	{
		get
		{
			return _buySellFontSize;
		}
		set
		{
			if (_buySellFontSize != value)
			{
				_buySellFontSize = value;
				NotifyPropertyChanged("BuySellFontSize");
			}
		}
	}

	public string BuyPriceText
	{
		get
		{
			return _buyPriceText;
		}
		set
		{
			if (_buyPriceText != value)
			{
				_buyPriceText = value;
				NotifyPropertyChanged("BuyPriceText");
			}
		}
	}

	public string SellPriceText
	{
		get
		{
			return _sellPriceText;
		}
		set
		{
			if (_sellPriceText != value)
			{
				_sellPriceText = value;
				NotifyPropertyChanged("SellPriceText");
			}
		}
	}

	public string TradeErrorText
	{
		get
		{
			return _tradeErrorText;
		}
		set
		{
			if (_tradeErrorText != value)
			{
				_tradeErrorText = value;
				NotifyPropertyChanged("TradeErrorText");
			}
		}
	}

	public string PlayerNameText
	{
		get
		{
			return _playerNameText;
		}
		set
		{
			if (_playerNameText != value)
			{
				_playerNameText = value;
				NotifyPropertyChanged("PlayerNameText");
			}
		}
	}

	public string PlayerMottoText
	{
		get
		{
			return _playerMottoText;
		}
		set
		{
			if (_playerMottoText != value)
			{
				_playerMottoText = value;
				NotifyPropertyChanged("PlayerMottoText");
			}
		}
	}

	public string PopReportFoodText
	{
		get
		{
			return _popReportFoodText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_popReportFoodText = value;
			HUDBuildingPanel.RefWGTPopReportFoodPop.SetPopHead(num, visible: true);
			NotifyPropertyChanged("PopReportFoodText");
		}
	}

	public string PopReportTaxText
	{
		get
		{
			return _popReportTaxText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_popReportTaxText = value;
			HUDBuildingPanel.RefWGTPopReportTaxPop.SetPopHead(num, visible: true);
			NotifyPropertyChanged("PopReportTaxText");
		}
	}

	public string PopReportCrowdingText
	{
		get
		{
			return _popReportCrowdingText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_popReportCrowdingText = value;
			HUDBuildingPanel.RefWGTPopReportCrowdingPop.SetPopHead(num, visible: true);
			NotifyPropertyChanged("PopReportCrowdingText");
		}
	}

	public string PopReportFearFactorText
	{
		get
		{
			return _popReportFearFactorText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_popReportFearFactorText = value;
			HUDBuildingPanel.RefWGTPopReportFearFactorPop.SetPopHead(num, visible: true);
			NotifyPropertyChanged("PopReportFearFactorText");
		}
	}

	public string PopReportReligionText
	{
		get
		{
			return _popReportReligionText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_popReportReligionText = value;
			HUDBuildingPanel.RefWGTPopReportReligionPop.SetPopHead(num, visible: true);
			NotifyPropertyChanged("PopReportReligionText");
		}
	}

	public string PopReportAleText
	{
		get
		{
			return _popReportAleText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_popReportAleText = value;
			HUDBuildingPanel.RefWGTPopReportAlePop.SetPopHead(num, visible: true);
			NotifyPropertyChanged("PopReportAleText");
		}
	}

	public string PopReportEventsText
	{
		get
		{
			return _popReportEventsText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_popReportEventsText = value;
			HUDBuildingPanel.RefWGTPopReportEventsPop.SetPopHead(num, visible: true);
			NotifyPropertyChanged("PopReportEventsText");
		}
	}

	public string PopReportTotalText
	{
		get
		{
			return _popReportTotalText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_popReportTotalText = value;
			HUDBuildingPanel.RefWGTPopReportTotalPop.SetPopHead(num, visible: true);
			HUDBuildingPanel.RefWGTPopReportTotal2Pop.SetPopHead(num, visible: true);
			NotifyPropertyChanged("PopReportTotalText");
		}
	}

	public string PopReportFairsText
	{
		get
		{
			return _popReportFairsText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_popReportFairsText = value;
			HUDBuildingPanel.RefWGTPopReportFairsPop.SetPopHead(num, visible: true);
			NotifyPropertyChanged("PopReportFairsText");
		}
	}

	public string PopReportMarriageText
	{
		get
		{
			return _popReportMarriageText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_popReportMarriageText = value;
			HUDBuildingPanel.RefWGTPopReportMarriagePop.SetPopHead(num, visible: true);
			NotifyPropertyChanged("PopReportMarriageText");
		}
	}

	public string PopReportJesterText
	{
		get
		{
			return _popReportJesterText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_popReportJesterText = value;
			HUDBuildingPanel.RefWGTPopReportJesterPop.SetPopHead(num, visible: true);
			NotifyPropertyChanged("PopReportJesterText");
		}
	}

	public string PopReportPlagueText
	{
		get
		{
			return _popReportPlagueText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_popReportPlagueText = value;
			HUDBuildingPanel.RefWGTPopReportPlaguePop.SetPopHead(num, visible: true);
			NotifyPropertyChanged("PopReportPlagueText");
		}
	}

	public string PopReportWolvesText
	{
		get
		{
			return _popReportWolvesText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_popReportWolvesText = value;
			HUDBuildingPanel.RefWGTPopReportWolvesPop.SetPopHead(num, visible: true);
			NotifyPropertyChanged("PopReportWolvesText");
		}
	}

	public string PopReportBanditsText
	{
		get
		{
			return _popReportBanditsText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_popReportBanditsText = value;
			HUDBuildingPanel.RefWGTPopReportBanditsPop.SetPopHead(num, visible: true);
			NotifyPropertyChanged("PopReportBanditsText");
		}
	}

	public string PopReportFireText
	{
		get
		{
			return _popReportFireText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_popReportFireText = value;
			HUDBuildingPanel.RefWGTPopReportFirePop.SetPopHead(num, visible: true);
			NotifyPropertyChanged("PopReportFireText");
		}
	}

	public string FFReportBadBuildingsText
	{
		get
		{
			return _ffReportBadBuildingsText;
		}
		set
		{
			if (_ffReportBadBuildingsText != value)
			{
				_ffReportBadBuildingsText = value;
				NotifyPropertyChanged("FFReportBadBuildingsText");
			}
		}
	}

	public string FFReportGoodBuildingsText
	{
		get
		{
			return _ffReportGoodBuildingsText;
		}
		set
		{
			if (_ffReportGoodBuildingsText != value)
			{
				_ffReportGoodBuildingsText = value;
				NotifyPropertyChanged("FFReportGoodBuildingsText");
			}
		}
	}

	public string FFReportFearFactorText
	{
		get
		{
			return _ffReportFearFactorText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_ffReportFearFactorText = value;
			HUDBuildingPanel.RefWGTFFReportFearFactorPop.SetPopHead(num, visible: true);
			NotifyPropertyChanged("FFReportFearFactorText");
		}
	}

	public string FFReportNextLevelAmountText
	{
		get
		{
			return _ffReportNextLevelAmountText;
		}
		set
		{
			if (_ffReportNextLevelAmountText != value)
			{
				_ffReportNextLevelAmountText = value;
				NotifyPropertyChanged("FFReportNextLevelAmountText");
			}
		}
	}

	public string FFReportNextLevelText
	{
		get
		{
			return _ffReportNextLevelText;
		}
		set
		{
			if (_ffReportNextLevelText != value)
			{
				_ffReportNextLevelText = value;
				NotifyPropertyChanged("FFReportNextLevelText");
			}
		}
	}

	public string FFReportCommentaryText
	{
		get
		{
			return _ffReportCommentaryText;
		}
		set
		{
			if (_ffReportCommentaryText != value)
			{
				_ffReportCommentaryText = value;
				NotifyPropertyChanged("FFReportCommentaryText");
			}
		}
	}

	public string FFReportEfficiencyAmountText
	{
		get
		{
			return _ffReportEfficiencyAmountText;
		}
		set
		{
			if (_ffReportEfficiencyAmountText != value)
			{
				_ffReportEfficiencyAmountText = value;
				NotifyPropertyChanged("FFReportEfficiencyAmountText");
			}
		}
	}

	public string GraphReportLeftScaleNo2Text
	{
		get
		{
			return _graphReportLeftScaleNo2Text;
		}
		set
		{
			if (_graphReportLeftScaleNo2Text != value)
			{
				_graphReportLeftScaleNo2Text = value;
				NotifyPropertyChanged("GraphReportLeftScaleNo2Text");
			}
		}
	}

	public string GraphReportLeftScaleNo1Text
	{
		get
		{
			return _graphReportLeftScaleNo1Text;
		}
		set
		{
			if (_graphReportLeftScaleNo1Text != value)
			{
				_graphReportLeftScaleNo1Text = value;
				NotifyPropertyChanged("GraphReportLeftScaleNo1Text");
			}
		}
	}

	public string GraphReportBottomScaleNo1Text
	{
		get
		{
			return _graphReportBottomScaleNo1Text;
		}
		set
		{
			if (_graphReportBottomScaleNo1Text != value)
			{
				_graphReportBottomScaleNo1Text = value;
				NotifyPropertyChanged("GraphReportBottomScaleNo1Text");
			}
		}
	}

	public string GraphReportBottomScaleNo2Text
	{
		get
		{
			return _graphReportBottomScaleNo2Text;
		}
		set
		{
			if (_graphReportBottomScaleNo2Text != value)
			{
				_graphReportBottomScaleNo2Text = value;
				NotifyPropertyChanged("GraphReportBottomScaleNo2Text");
			}
		}
	}

	public string GraphReportBottomScaleNo3Text
	{
		get
		{
			return _graphReportBottomScaleNo3Text;
		}
		set
		{
			if (_graphReportBottomScaleNo3Text != value)
			{
				_graphReportBottomScaleNo3Text = value;
				NotifyPropertyChanged("GraphReportBottomScaleNo3Text");
			}
		}
	}

	public string GraphReportBottomScaleNo4Text
	{
		get
		{
			return _graphReportBottomScaleNo4Text;
		}
		set
		{
			if (_graphReportBottomScaleNo4Text != value)
			{
				_graphReportBottomScaleNo4Text = value;
				NotifyPropertyChanged("GraphReportBottomScaleNo4Text");
			}
		}
	}

	public string GraphReportBottomScaleNo5Text
	{
		get
		{
			return _graphReportBottomScaleNo5Text;
		}
		set
		{
			if (_graphReportBottomScaleNo5Text != value)
			{
				_graphReportBottomScaleNo5Text = value;
				NotifyPropertyChanged("GraphReportBottomScaleNo5Text");
			}
		}
	}

	public string GraphReportBottomScaleNo6Text
	{
		get
		{
			return _graphReportBottomScaleNo6Text;
		}
		set
		{
			if (_graphReportBottomScaleNo6Text != value)
			{
				_graphReportBottomScaleNo6Text = value;
				NotifyPropertyChanged("GraphReportBottomScaleNo6Text");
			}
		}
	}

	public string GraphReportPathDataString
	{
		get
		{
			return _graphReportPathDataString;
		}
		set
		{
			if (_graphReportPathDataString != value)
			{
				_graphReportPathDataString = value;
				NotifyPropertyChanged("GraphReportPathDataString");
			}
		}
	}

	public string ArmyReportFFBoostText
	{
		get
		{
			return _armyReportFFBoostText;
		}
		set
		{
			if (_armyReportFFBoostText != value)
			{
				_armyReportFFBoostText = value;
				NotifyPropertyChanged("ArmyReportFFBoostText");
			}
		}
	}

	public string ArmyReportTotalTroopsText
	{
		get
		{
			return _armyReportTotalTroopsText;
		}
		set
		{
			if (_armyReportTotalTroopsText != value)
			{
				_armyReportTotalTroopsText = value;
				NotifyPropertyChanged("ArmyReportTotalTroopsText");
			}
		}
	}

	public string RelReportTotalPriestsText
	{
		get
		{
			return _relReportTotalPriestsText;
		}
		set
		{
			if (_relReportTotalPriestsText != value)
			{
				_relReportTotalPriestsText = value;
				NotifyPropertyChanged("RelReportTotalPriestsText");
			}
		}
	}

	public string RelReportBlessedPeopleText
	{
		get
		{
			return _relReportBlessedPeopleText;
		}
		set
		{
			if (_relReportBlessedPeopleText != value)
			{
				_relReportBlessedPeopleText = value;
				NotifyPropertyChanged("RelReportBlessedPeopleText");
			}
		}
	}

	public string RelReportPopEffectText
	{
		get
		{
			return _relReportPopEffectText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_relReportPopEffectText = value;
			HUDBuildingPanel.RefWGTRelReport.SetPopHead(num, visible: true);
			NotifyPropertyChanged("RelReportPopEffectText");
		}
	}

	public double RelReportPopEffectTextLabelWidth
	{
		get
		{
			return _relReportPopEffectTextLabelWidth;
		}
		set
		{
			if (_relReportPopEffectTextLabelWidth != value)
			{
				_relReportPopEffectTextLabelWidth = value;
				NotifyPropertyChanged("RelReportPopEffectTextLabelWidth");
			}
		}
	}

	public double WGT_RelReportLabelWidth
	{
		get
		{
			return _WGT_RelReportLabelWidth;
		}
		set
		{
			if (_WGT_RelReportLabelWidth != value)
			{
				_WGT_RelReportLabelWidth = value;
				NotifyPropertyChanged("WGT_RelReportLabelWidth");
			}
		}
	}

	public string RelReportNextLevelText
	{
		get
		{
			return _relReportNextLevelText;
		}
		set
		{
			if (_relReportNextLevelText != value)
			{
				_relReportNextLevelText = value;
				NotifyPropertyChanged("RelReportNextLevelText");
			}
		}
	}

	public string RelReportNextLevel2Text
	{
		get
		{
			return _relReportNextLevel2Text;
		}
		set
		{
			if (_relReportNextLevel2Text != value)
			{
				_relReportNextLevel2Text = value;
				NotifyPropertyChanged("RelReportNextLevel2Text");
			}
		}
	}

	public string RelReportTypeDemandedText
	{
		get
		{
			return _relReportTypeDemandedText;
		}
		set
		{
			if (_relReportTypeDemandedText != value)
			{
				_relReportTypeDemandedText = value;
				NotifyPropertyChanged("RelReportTypeDemandedText");
			}
		}
	}

	public string RelReportDemandEffectText
	{
		get
		{
			return _relReportDemandEffectText;
		}
		set
		{
			int num = int.Parse(value, Director.defaultCulture);
			if (num > 0)
			{
				value = "+" + value;
			}
			_relReportDemandEffectText = value;
			HUDBuildingPanel.RefWGTRelReport2.SetPopHead(num, visible: true);
			NotifyPropertyChanged("RelReportDemandEffectText");
		}
	}

	public string TroopsPanelRollover
	{
		get
		{
			return _troopsPanelRollover;
		}
		set
		{
			if (_troopsPanelRollover != value)
			{
				_troopsPanelRollover = value;
				NotifyPropertyChanged("TroopsPanelRollover");
			}
		}
	}

	public string TroopsPanelRollover_AmountReq1
	{
		get
		{
			return _troopsPanelRollover_AmountReq1;
		}
		set
		{
			if (_troopsPanelRollover_AmountReq1 != value)
			{
				_troopsPanelRollover_AmountReq1 = value;
				NotifyPropertyChanged("TroopsPanelRollover_AmountReq1");
			}
		}
	}

	public string TroopsPanelRollover_AmountGot1
	{
		get
		{
			return _troopsPanelRollover_AmountGot1;
		}
		set
		{
			if (_troopsPanelRollover_AmountGot1 != value)
			{
				_troopsPanelRollover_AmountGot1 = value;
				NotifyPropertyChanged("TroopsPanelRollover_AmountGot1");
			}
		}
	}

	public ImageSource TroopsPanelRollover_GoodsImage1
	{
		get
		{
			return _troopsPanelRollover_GoodsImage1;
		}
		set
		{
			if (_troopsPanelRollover_GoodsImage1 != value)
			{
				_troopsPanelRollover_GoodsImage1 = value;
				NotifyPropertyChanged("TroopsPanelRollover_GoodsImage1");
			}
		}
	}

	public string AmmoLeft
	{
		get
		{
			return _ammoLeft;
		}
		set
		{
			if (_ammoLeft != value)
			{
				_ammoLeft = value;
				NotifyPropertyChanged("AmmoLeft");
			}
		}
	}

	public string CowAmmoLeft
	{
		get
		{
			return _cowAmmoLeft;
		}
		set
		{
			if (_cowAmmoLeft != value)
			{
				_cowAmmoLeft = value;
				NotifyPropertyChanged("CowAmmoLeft");
			}
		}
	}

	public double ChimpNameTextFontSize
	{
		get
		{
			return _chimpNameTextFontSize;
		}
		set
		{
			if (_chimpNameTextFontSize != value)
			{
				_chimpNameTextFontSize = value;
				NotifyPropertyChanged("ChimpNameTextFontSize");
			}
		}
	}

	public string ChimpNameText
	{
		get
		{
			return _chimpNameText;
		}
		set
		{
			if (_chimpNameText != value)
			{
				_chimpNameText = value;
				NotifyPropertyChanged("ChimpNameText");
			}
		}
	}

	public string ChimpTypeText
	{
		get
		{
			return _chimpTypeText;
		}
		set
		{
			if (_chimpTypeText != value)
			{
				_chimpTypeText = value;
				NotifyPropertyChanged("ChimpTypeText");
			}
		}
	}

	public string ChimpWorkText
	{
		get
		{
			return _chimpWorkText;
		}
		set
		{
			if (_chimpWorkText != value)
			{
				_chimpWorkText = value;
				NotifyPropertyChanged("ChimpWorkText");
			}
		}
	}

	public string ChimpCommentText
	{
		get
		{
			return _chimpCommentText;
		}
		set
		{
			if (_chimpCommentText != value)
			{
				_chimpCommentText = value;
				NotifyPropertyChanged("ChimpCommentText");
			}
		}
	}

	public bool Show_BarracksBows1
	{
		get
		{
			return _show_BarracksBows1;
		}
		set
		{
			if (_show_BarracksBows1 != value)
			{
				_show_BarracksBows1 = value;
				NotifyPropertyChanged("Show_BarracksBows1");
			}
		}
	}

	public bool Show_BarracksBows2
	{
		get
		{
			return _show_BarracksBows2;
		}
		set
		{
			if (_show_BarracksBows2 != value)
			{
				_show_BarracksBows2 = value;
				NotifyPropertyChanged("Show_BarracksBows2");
			}
		}
	}

	public bool Show_BarracksBows3
	{
		get
		{
			return _show_BarracksBows3;
		}
		set
		{
			if (_show_BarracksBows3 != value)
			{
				_show_BarracksBows3 = value;
				NotifyPropertyChanged("Show_BarracksBows3");
			}
		}
	}

	public double Show_BarracksBowsOpaque
	{
		get
		{
			return _show_BarracksBowsOpaque;
		}
		set
		{
			if (_show_BarracksBowsOpaque != value)
			{
				_show_BarracksBowsOpaque = value;
				NotifyPropertyChanged("Show_BarracksBowsOpaque");
			}
		}
	}

	public bool Show_BarracksSpears1
	{
		get
		{
			return _show_BarracksSpears1;
		}
		set
		{
			if (_show_BarracksSpears1 != value)
			{
				_show_BarracksSpears1 = value;
				NotifyPropertyChanged("Show_BarracksSpears1");
			}
		}
	}

	public bool Show_BarracksSpears2
	{
		get
		{
			return _show_BarracksSpears2;
		}
		set
		{
			if (_show_BarracksSpears2 != value)
			{
				_show_BarracksSpears2 = value;
				NotifyPropertyChanged("Show_BarracksSpears2");
			}
		}
	}

	public bool Show_BarracksSpears3
	{
		get
		{
			return _show_BarracksSpears3;
		}
		set
		{
			if (_show_BarracksSpears3 != value)
			{
				_show_BarracksSpears3 = value;
				NotifyPropertyChanged("Show_BarracksSpears3");
			}
		}
	}

	public double Show_BarracksSpearsOpaque
	{
		get
		{
			return _show_BarracksSpearsOpaque;
		}
		set
		{
			if (_show_BarracksSpearsOpaque != value)
			{
				_show_BarracksSpearsOpaque = value;
				NotifyPropertyChanged("Show_BarracksSpearsOpaque");
			}
		}
	}

	public bool Show_BarracksMaces1
	{
		get
		{
			return _show_BarracksMaces1;
		}
		set
		{
			if (_show_BarracksMaces1 != value)
			{
				_show_BarracksMaces1 = value;
				NotifyPropertyChanged("Show_BarracksMaces1");
			}
		}
	}

	public bool Show_BarracksMaces2
	{
		get
		{
			return _show_BarracksMaces2;
		}
		set
		{
			if (_show_BarracksMaces2 != value)
			{
				_show_BarracksMaces2 = value;
				NotifyPropertyChanged("Show_BarracksMaces2");
			}
		}
	}

	public bool Show_BarracksMaces3
	{
		get
		{
			return _show_BarracksMaces3;
		}
		set
		{
			if (_show_BarracksMaces3 != value)
			{
				_show_BarracksMaces3 = value;
				NotifyPropertyChanged("Show_BarracksMaces3");
			}
		}
	}

	public double Show_BarracksMacesOpaque
	{
		get
		{
			return _show_BarracksMacesOpaque;
		}
		set
		{
			if (_show_BarracksMacesOpaque != value)
			{
				_show_BarracksMacesOpaque = value;
				NotifyPropertyChanged("Show_BarracksMacesOpaque");
			}
		}
	}

	public bool Show_BarracksXBows1
	{
		get
		{
			return _show_BarracksXBows1;
		}
		set
		{
			if (_show_BarracksXBows1 != value)
			{
				_show_BarracksXBows1 = value;
				NotifyPropertyChanged("Show_BarracksXBows1");
			}
		}
	}

	public bool Show_BarracksXBows2
	{
		get
		{
			return _show_BarracksXBows2;
		}
		set
		{
			if (_show_BarracksXBows2 != value)
			{
				_show_BarracksXBows2 = value;
				NotifyPropertyChanged("Show_BarracksXBows2");
			}
		}
	}

	public bool Show_BarracksXBows3
	{
		get
		{
			return _show_BarracksXBows3;
		}
		set
		{
			if (_show_BarracksXBows3 != value)
			{
				_show_BarracksXBows3 = value;
				NotifyPropertyChanged("Show_BarracksXBows3");
			}
		}
	}

	public double Show_BarracksXBowsOpaque
	{
		get
		{
			return _show_BarracksXBowsOpaque;
		}
		set
		{
			if (_show_BarracksXBowsOpaque != value)
			{
				_show_BarracksXBowsOpaque = value;
				NotifyPropertyChanged("Show_BarracksXBowsOpaque");
			}
		}
	}

	public bool Show_BarracksLeatherArmour1
	{
		get
		{
			return _show_BarracksLeatherArmour1;
		}
		set
		{
			if (_show_BarracksLeatherArmour1 != value)
			{
				_show_BarracksLeatherArmour1 = value;
				NotifyPropertyChanged("Show_BarracksLeatherArmour1");
			}
		}
	}

	public bool Show_BarracksLeatherArmour2
	{
		get
		{
			return _show_BarracksLeatherArmour2;
		}
		set
		{
			if (_show_BarracksLeatherArmour2 != value)
			{
				_show_BarracksLeatherArmour2 = value;
				NotifyPropertyChanged("Show_BarracksLeatherArmour2");
			}
		}
	}

	public bool Show_BarracksLeatherArmour3
	{
		get
		{
			return _show_BarracksLeatherArmour3;
		}
		set
		{
			if (_show_BarracksLeatherArmour3 != value)
			{
				_show_BarracksLeatherArmour3 = value;
				NotifyPropertyChanged("Show_BarracksLeatherArmour3");
			}
		}
	}

	public double Show_BarracksLeatherArmourOpaque
	{
		get
		{
			return _show_BarracksLeatherArmourOpaque;
		}
		set
		{
			if (_show_BarracksLeatherArmourOpaque != value)
			{
				_show_BarracksLeatherArmourOpaque = value;
				NotifyPropertyChanged("Show_BarracksLeatherArmourOpaque");
			}
		}
	}

	public bool Show_BarracksPikes1
	{
		get
		{
			return _show_BarracksPikes1;
		}
		set
		{
			if (_show_BarracksPikes1 != value)
			{
				_show_BarracksPikes1 = value;
				NotifyPropertyChanged("Show_BarracksPikes1");
			}
		}
	}

	public bool Show_BarracksPikes2
	{
		get
		{
			return _show_BarracksPikes2;
		}
		set
		{
			if (_show_BarracksPikes2 != value)
			{
				_show_BarracksPikes2 = value;
				NotifyPropertyChanged("Show_BarracksPikes2");
			}
		}
	}

	public bool Show_BarracksPikes3
	{
		get
		{
			return _show_BarracksPikes3;
		}
		set
		{
			if (_show_BarracksPikes3 != value)
			{
				_show_BarracksPikes3 = value;
				NotifyPropertyChanged("Show_BarracksPikes3");
			}
		}
	}

	public double Show_BarracksPikesOpaque
	{
		get
		{
			return _show_BarracksPikesOpaque;
		}
		set
		{
			if (_show_BarracksPikesOpaque != value)
			{
				_show_BarracksPikesOpaque = value;
				NotifyPropertyChanged("Show_BarracksPikesOpaque");
			}
		}
	}

	public bool Show_BarracksSwords1
	{
		get
		{
			return _show_BarracksSwords1;
		}
		set
		{
			if (_show_BarracksSwords1 != value)
			{
				_show_BarracksSwords1 = value;
				NotifyPropertyChanged("Show_BarracksSwords1");
			}
		}
	}

	public bool Show_BarracksSwords2
	{
		get
		{
			return _show_BarracksSwords2;
		}
		set
		{
			if (_show_BarracksSwords2 != value)
			{
				_show_BarracksSwords2 = value;
				NotifyPropertyChanged("Show_BarracksSwords2");
			}
		}
	}

	public bool Show_BarracksSwords3
	{
		get
		{
			return _show_BarracksSwords3;
		}
		set
		{
			if (_show_BarracksSwords3 != value)
			{
				_show_BarracksSwords3 = value;
				NotifyPropertyChanged("Show_BarracksSwords3");
			}
		}
	}

	public double Show_BarracksSwordsOpaque
	{
		get
		{
			return _show_BarracksSwordsOpaque;
		}
		set
		{
			if (_show_BarracksSwordsOpaque != value)
			{
				_show_BarracksSwordsOpaque = value;
				NotifyPropertyChanged("Show_BarracksSwordsOpaque");
			}
		}
	}

	public bool Show_BarracksArmour1
	{
		get
		{
			return _show_BarracksArmour1;
		}
		set
		{
			if (_show_BarracksArmour1 != value)
			{
				_show_BarracksArmour1 = value;
				NotifyPropertyChanged("Show_BarracksArmour1");
			}
		}
	}

	public bool Show_BarracksArmour2
	{
		get
		{
			return _show_BarracksArmour2;
		}
		set
		{
			if (_show_BarracksArmour2 != value)
			{
				_show_BarracksArmour2 = value;
				NotifyPropertyChanged("Show_BarracksArmour2");
			}
		}
	}

	public bool Show_BarracksArmour3
	{
		get
		{
			return _show_BarracksArmour3;
		}
		set
		{
			if (_show_BarracksArmour3 != value)
			{
				_show_BarracksArmour3 = value;
				NotifyPropertyChanged("Show_BarracksArmour3");
			}
		}
	}

	public double Show_BarracksArmourOpaque
	{
		get
		{
			return _show_BarracksArmourOpaque;
		}
		set
		{
			if (_show_BarracksArmourOpaque != value)
			{
				_show_BarracksArmourOpaque = value;
				NotifyPropertyChanged("Show_BarracksArmourOpaque");
			}
		}
	}

	public bool Show_BarracksHorses1
	{
		get
		{
			return _show_BarracksHorses1;
		}
		set
		{
			if (_show_BarracksHorses1 != value)
			{
				_show_BarracksHorses1 = value;
				NotifyPropertyChanged("Show_BarracksHorses1");
			}
		}
	}

	public bool Show_BarracksHorses2
	{
		get
		{
			return _show_BarracksHorses2;
		}
		set
		{
			if (_show_BarracksHorses2 != value)
			{
				_show_BarracksHorses2 = value;
				NotifyPropertyChanged("Show_BarracksHorses2");
			}
		}
	}

	public bool Show_BarracksHorses3
	{
		get
		{
			return _show_BarracksHorses3;
		}
		set
		{
			if (_show_BarracksHorses3 != value)
			{
				_show_BarracksHorses3 = value;
				NotifyPropertyChanged("Show_BarracksHorses3");
			}
		}
	}

	public double Show_BarracksHorsesOpaque
	{
		get
		{
			return _show_BarracksHorsesOpaque;
		}
		set
		{
			if (_show_BarracksHorsesOpaque != value)
			{
				_show_BarracksHorsesOpaque = value;
				NotifyPropertyChanged("Show_BarracksHorsesOpaque");
			}
		}
	}

	public bool Show_BarracksArcher
	{
		get
		{
			return _show_BarracksArcher;
		}
		set
		{
			if (_show_BarracksArcher != value)
			{
				_show_BarracksArcher = value;
				NotifyPropertyChanged("Show_BarracksArcher");
			}
		}
	}

	public bool Show_BarracksSpearman
	{
		get
		{
			return _show_BarracksSpearman;
		}
		set
		{
			if (_show_BarracksSpearman != value)
			{
				_show_BarracksSpearman = value;
				NotifyPropertyChanged("Show_BarracksSpearman");
			}
		}
	}

	public bool Show_BarracksMaceman
	{
		get
		{
			return _show_BarracksMaceman;
		}
		set
		{
			if (_show_BarracksMaceman != value)
			{
				_show_BarracksMaceman = value;
				NotifyPropertyChanged("Show_BarracksMaceman");
			}
		}
	}

	public bool Show_BarracksXbowman
	{
		get
		{
			return _show_BarracksXbowman;
		}
		set
		{
			if (_show_BarracksXbowman != value)
			{
				_show_BarracksXbowman = value;
				NotifyPropertyChanged("Show_BarracksXbowman");
			}
		}
	}

	public bool Show_BarracksPikeman
	{
		get
		{
			return _show_BarracksPikeman;
		}
		set
		{
			if (_show_BarracksPikeman != value)
			{
				_show_BarracksPikeman = value;
				NotifyPropertyChanged("Show_BarracksPikeman");
			}
		}
	}

	public bool Show_BarracksSwordsman
	{
		get
		{
			return _show_BarracksSwordsman;
		}
		set
		{
			if (_show_BarracksSwordsman != value)
			{
				_show_BarracksSwordsman = value;
				NotifyPropertyChanged("Show_BarracksSwordsman");
			}
		}
	}

	public bool Show_BarracksKnight
	{
		get
		{
			return _show_BarracksKnight;
		}
		set
		{
			if (_show_BarracksKnight != value)
			{
				_show_BarracksKnight = value;
				NotifyPropertyChanged("Show_BarracksKnight");
			}
		}
	}

	public DelegateCommand ButtonFrontEndEnterCommand { get; private set; }

	public DelegateCommand ButtonNullCommand { get; private set; }

	public DelegateCommand ButtonExitCommand { get; private set; }

	public DelegateCommand ButtonNewSceneCommand { get; private set; }

	public DelegateCommand ButtonDebugCommand { get; private set; }

	public DelegateCommand ButtonBuildCommand { get; private set; }

	public DelegateCommand ButtonMECommand { get; private set; }

	public DelegateCommand ButtonToggleScenrioEditorCommand { get; private set; }

	public DelegateCommand IntroSkipCommand { get; private set; }

	public DelegateCommand SubtitlesCommand { get; private set; }

	public DelegateCommand EnterYourNameCommand { get; private set; }

	public DelegateCommand ButtonMainMenuCommand { get; private set; }

	public DelegateCommand CampaignMenuCommand { get; private set; }

	public DelegateCommand EcoCampaignMenuCommand { get; private set; }

	public DelegateCommand ExtraCampaignMenuCommand { get; private set; }

	public DelegateCommand ExtraEcoCampaignMenuCommand { get; private set; }

	public DelegateCommand TrailCampaignMenuCommand { get; private set; }

	public DelegateCommand StandaloneMenuCommand { get; private set; }

	public DelegateCommand MapEditorSetupCommand { get; private set; }

	public DelegateCommand MultiplayerMenuCommand { get; private set; }

	public DelegateCommand LeftClickSelectedTroopCommand { get; private set; }

	public DelegateCommand RightClickSelectedTroopCommand { get; private set; }

	public DelegateCommand ButtonChangeStanceCommand { get; private set; }

	public DelegateCommand ButtonUnitDisbandCommand { get; private set; }

	public DelegateCommand ButtonUnitBuildCatCommand { get; private set; }

	public DelegateCommand ButtonUnitStopCommand { get; private set; }

	public DelegateCommand ButtonUnitBuildTrebCommand { get; private set; }

	public DelegateCommand ButtonUnitPatrolCommand { get; private set; }

	public DelegateCommand ButtonUnitBuildTwrCommand { get; private set; }

	public DelegateCommand ButtonUnitAttackHereCommand { get; private set; }

	public DelegateCommand ButtonUnitBuildTunnelCommand { get; private set; }

	public DelegateCommand ButtonUnitPourOilCommand { get; private set; }

	public DelegateCommand ButtonUnitBuildRamCommand { get; private set; }

	public DelegateCommand ButtonUnitBuildSiegeKitCommand { get; private set; }

	public DelegateCommand ButtonUnitBuildMantletCommand { get; private set; }

	public DelegateCommand ButtonUnitRechargeRockCommand { get; private set; }

	public DelegateCommand ButtonUnitLaunchCowCommand { get; private set; }

	public DelegateCommand ButtonUnitbackCommand { get; private set; }

	public DelegateCommand ButtonTroopPanelMouseEnterCommand { get; private set; }

	public DelegateCommand ButtonTroopPanelMouseLeaveCommand { get; private set; }

	public DelegateCommand ButtonTroopPanelToggleCGCommand { get; private set; }

	public DelegateCommand ButtonControlGroupCommand { get; private set; }

	public DelegateCommand ButtonTroopPanelPageCommand { get; private set; }

	public DelegateCommand ButtonBuildingHelpCommand { get; private set; }

	public DelegateCommand ButtonBuildingZZZCommand { get; private set; }

	public DelegateCommand ButtonCreateTroopCommand { get; private set; }

	public DelegateCommand ButtonTroopCreateEnterCommand { get; private set; }

	public DelegateCommand ButtonTroopCreateLeaveCommand { get; private set; }

	public DelegateCommand ButtonSetRationsCommand { get; private set; }

	public DelegateCommand ButtonGranarySwitchModeCommand { get; private set; }

	public DelegateCommand ButtonSetEdibleCommand { get; private set; }

	public DelegateCommand ButtonAdjustTaxCommand { get; private set; }

	public DelegateCommand ButtonGateControlsCommand { get; private set; }

	public DelegateCommand ButtonDrawbridgeControlsCommand { get; private set; }

	public DelegateCommand ButtonChangeWorkshopOutputCommand { get; private set; }

	public DelegateCommand ButtonReleaseDogsCommand { get; private set; }

	public DelegateCommand ButtonNewTradeTypeCommand { get; private set; }

	public DelegateCommand ButtonNewTradeGoodsTypeCommand { get; private set; }

	public DelegateCommand ButtonCycleTradeGoodsTypeCommand { get; private set; }

	public DelegateCommand ButtonBuySellCommand { get; private set; }

	public DelegateCommand ButtonRepairCommand { get; private set; }

	public DelegateCommand ButtonRepairMouseEnterCommand { get; private set; }

	public DelegateCommand ButtonRepairMouseLeaveCommand { get; private set; }

	public DelegateCommand ButtonMakeWeaponMouseEnterCommand { get; private set; }

	public DelegateCommand ButtonMakeWeaponMouseLeaveCommand { get; private set; }

	public DelegateCommand ButtonScribeMouseEnterCommand { get; private set; }

	public DelegateCommand ButtonScribeMouseLeaveCommand { get; private set; }

	public DelegateCommand ButtonReportsCommand { get; private set; }

	public DelegateCommand ButtonReturnToReportsCommand { get; private set; }

	public DelegateCommand ButtonReportViewEventsCommand { get; private set; }

	public DelegateCommand ButtonToggleArmyReportCommand { get; private set; }

	public DelegateCommand ButtonActionPointCommand { get; private set; }

	public DelegateCommand ButtonRadarZoomCommand { get; private set; }

	public DelegateCommand ButtonGoToBriefingCommand { get; private set; }

	public DelegateCommand ButtonBriefingQuitCommand { get; private set; }

	public DelegateCommand ButtonBriefingResumeCommand { get; private set; }

	public DelegateCommand ButtonBriefingModeCommand { get; private set; }

	public DelegateCommand ButtonBriefingDifficultyCommand { get; private set; }

	public DelegateCommand ButtonBriefingHintCommand { get; private set; }

	public DelegateCommand ButtonBriefingBackCommand { get; private set; }

	public DelegateCommand ButtonBriefingRolloverCommand { get; private set; }

	public DelegateCommand ButtonMainHelpCommand { get; private set; }

	public DelegateCommand ButtonExtendedFeaturesCommand { get; private set; }

	public DelegateCommand ButtonScenrioStartMonthCommand { get; private set; }

	public DelegateCommand ButtonScenarioAltTextCommand { get; private set; }

	public DelegateCommand ButtonScenrioStartRationsCommand { get; private set; }

	public DelegateCommand ButtonScenrioStartTaxCommand { get; private set; }

	public DelegateCommand ButtonScenrioViewCommand { get; private set; }

	public DelegateCommand ButtonScenrioAdjustDateCommand { get; private set; }

	public DelegateCommand ButtonSelectStartingGoodCommand { get; private set; }

	public DelegateCommand ButtonSelectStartingTraderCommand { get; private set; }

	public DelegateCommand ButtonScenarioBuildingAvailToggleCommand { get; private set; }

	public DelegateCommand ButtonSelectAttackingForcesCommand { get; private set; }

	public DelegateCommand ButtonScenarioEditSettingsCommand { get; private set; }

	public DelegateCommand ButtonWorkshopCommand { get; private set; }

	public DelegateCommand RightClickSizeCommand { get; private set; }

	public DelegateCommand RightClickRulerCommand { get; private set; }

	public DelegateCommand ButtonScenrioMessageMonthCommand { get; private set; }

	public DelegateCommand ButtonScenrioMessageGroup { get; private set; }

	public DelegateCommand ButtonScenrioInvasionMonthCommand { get; private set; }

	public DelegateCommand ButtonScenrioInvasionGroup { get; private set; }

	public DelegateCommand ButtonScenrioInvasionMarkerID { get; private set; }

	public DelegateCommand ButtonSelectInvasionSizeCommand { get; private set; }

	public DelegateCommand ButtonScenrioEventMonthCommand { get; private set; }

	public DelegateCommand ButtonScenarionEventConditionToggleCommand { get; private set; }

	public DelegateCommand ButtonScenarionEventConditionOnOffCommand { get; private set; }

	public DelegateCommand ButtonScenarionEventConditionAndOrCommand { get; private set; }

	public DelegateCommand ButtonScenarioSpecialCommand { get; private set; }

	public DelegateCommand PreLoadCommand { get; private set; }

	public DelegateCommand ClickStoryAdvanceCommand { get; private set; }

	public DelegateCommand ClickDbgPrePostCommand { get; private set; }

	public DelegateCommand ClickDbgStoryChapterCommand { get; private set; }

	public DelegateCommand ButtonIngameMenuCommand { get; private set; }

	public DelegateCommand ButtonConfirmationCommand { get; private set; }

	public DelegateCommand ButtonLoadSaveCommand { get; private set; }

	public DelegateCommand ButtonMPInviteCommand { get; private set; }

	public DelegateCommand ButtonMPConnectionIssueCommand { get; private set; }

	public DelegateCommand ButtonMPChatCommand { get; private set; }

	public DelegateCommand ButtonTutorialCommand { get; private set; }

	public DelegateCommand ButtonFreebuildCommand { get; private set; }

	public DelegateCommand OptionsCommand { get; private set; }

	public DelegateCommand MO_ClickCommand { get; private set; }

	public bool PreLoadBlankVis
	{
		get
		{
			return _preLoadBlankVis;
		}
		set
		{
			_preLoadBlankVis = value;
			NotifyPropertyChanged("PreLoadBlankVis");
		}
	}

	public bool HUD_Markers_Vis
	{
		get
		{
			return _HUD_Markers_Vis;
		}
		set
		{
			if (_HUD_Markers_Vis != value)
			{
				_HUD_Markers_Vis = value;
				NotifyPropertyChanged("HUD_Markers_Vis");
			}
		}
	}

	public bool Show_HUD_MPInviteWarning
	{
		get
		{
			return _show_HUD_MPInviteWarning;
		}
		set
		{
			_show_HUD_MPInviteWarning = value;
			NotifyPropertyChanged("Show_HUD_MPInviteWarning");
		}
	}

	public bool Show_HUD_MPConnectionIssue
	{
		get
		{
			return _show_HUD_MPConnectionIssue;
		}
		set
		{
			_show_HUD_MPConnectionIssue = value;
			NotifyPropertyChanged("Show_HUD_MPConnectionIssue");
		}
	}

	public string MPConnectionIssueText
	{
		get
		{
			return _MPConnectionIssueText;
		}
		set
		{
			if (_MPConnectionIssueText != value)
			{
				_MPConnectionIssueText = value;
				NotifyPropertyChanged("MPConnectionIssueText");
			}
		}
	}

	public string MPConnectionIssueButtonText
	{
		get
		{
			return _MPConnectionIssueButtonText;
		}
		set
		{
			if (_MPConnectionIssueButtonText != value)
			{
				_MPConnectionIssueButtonText = value;
				NotifyPropertyChanged("MPConnectionIssueButtonText");
			}
		}
	}

	public bool MPConectionIssueButtonVisible
	{
		get
		{
			return _MPConectionIssueButtonVisible;
		}
		set
		{
			if (_MPConectionIssueButtonVisible != value)
			{
				_MPConectionIssueButtonVisible = value;
				NotifyPropertyChanged("MPConectionIssueButtonVisible");
			}
		}
	}

	public bool Show_HUD_MPChatMessages
	{
		get
		{
			return _show_HUD_MPChatMessages;
		}
		set
		{
			_show_HUD_MPChatMessages = value;
			NotifyPropertyChanged("Show_HUD_MPChatMessages");
		}
	}

	public int MPChat_Size
	{
		get
		{
			return _MPChat_Size;
		}
		set
		{
			if (_MPChat_Size != value)
			{
				_MPChat_Size = value;
				NotifyPropertyChanged("MPChat_Size");
			}
		}
	}

	public string MPChatMessageText
	{
		get
		{
			return _MPChatMessageText;
		}
		set
		{
			if (_MPChatMessageText != value)
			{
				_MPChatMessageText = value;
				NotifyPropertyChanged("MPChatMessageText");
			}
		}
	}

	public bool MPChatVisible
	{
		get
		{
			return _MPChatVisible;
		}
		set
		{
			_MPChatVisible = value;
			NotifyPropertyChanged("MPChatVisible");
		}
	}

	public bool FreebuildSliderVis
	{
		get
		{
			return _freebuildSliderVis;
		}
		set
		{
			if (_freebuildSliderVis != value)
			{
				_freebuildSliderVis = value;
				NotifyPropertyChanged("FreebuildSliderVis");
			}
		}
	}

	public string FreebuildSizeText
	{
		get
		{
			return _freebuildSizeText;
		}
		set
		{
			if (_freebuildSizeText != value)
			{
				_freebuildSizeText = value;
				NotifyPropertyChanged("FreebuildSizeText");
			}
		}
	}

	public int FreebuildSizeMax
	{
		get
		{
			return _freebuildSizeMax;
		}
		set
		{
			if (_freebuildSizeMax != value)
			{
				_freebuildSizeMax = value;
				NotifyPropertyChanged("FreebuildSizeMax");
			}
		}
	}

	public int FreebuildSizeFreq
	{
		get
		{
			return _freebuildSizeFreq;
		}
		set
		{
			if (_freebuildSizeFreq != value)
			{
				_freebuildSizeFreq = value;
				NotifyPropertyChanged("FreebuildSizeFreq");
			}
		}
	}

	public int FreebuildInvasionSizeMax
	{
		get
		{
			return _freebuildInvasionSizeMax;
		}
		set
		{
			if (_freebuildInvasionSizeMax != value)
			{
				_freebuildInvasionSizeMax = value;
				NotifyPropertyChanged("FreebuildInvasionSizeMax");
			}
		}
	}

	public int FreebuildInvasionSizeFreq
	{
		get
		{
			return _freebuildInvasionSizeFreq;
		}
		set
		{
			if (_freebuildInvasionSizeFreq != value)
			{
				_freebuildInvasionSizeFreq = value;
				NotifyPropertyChanged("FreebuildInvasionSizeFreq");
			}
		}
	}

	public string FreebuildInvasionSizeText
	{
		get
		{
			return _freebuildInvasionSizeText;
		}
		set
		{
			if (_freebuildInvasionSizeText != value)
			{
				_freebuildInvasionSizeText = value;
				NotifyPropertyChanged("FreebuildInvasionSizeText");
			}
		}
	}

	public Visibility OptionsPushEnabled
	{
		get
		{
			return _optionsPushEnabled;
		}
		set
		{
			if (_optionsPushEnabled != value)
			{
				_optionsPushEnabled = value;
				NotifyPropertyChanged("OptionsPushEnabled");
			}
		}
	}

	public Visibility OptionsPushDisabled
	{
		get
		{
			return _optionsPushDisabled;
		}
		set
		{
			if (_optionsPushDisabled != value)
			{
				_optionsPushDisabled = value;
				NotifyPropertyChanged("OptionsPushDisabled");
			}
		}
	}

	public Visibility OptionsSH1RTS
	{
		get
		{
			return _optionsSH1RTS;
		}
		set
		{
			if (_optionsSH1RTS != value)
			{
				_optionsSH1RTS = value;
				NotifyPropertyChanged("OptionsSH1RTS");
			}
		}
	}

	public Visibility OptionsDERTS
	{
		get
		{
			return _optionsDERTS;
		}
		set
		{
			if (_optionsDERTS != value)
			{
				_optionsDERTS = value;
				NotifyPropertyChanged("OptionsDERTS");
			}
		}
	}

	public Visibility OptionsWheelSH1
	{
		get
		{
			return _optionsWheelSH1;
		}
		set
		{
			if (_optionsWheelSH1 != value)
			{
				_optionsWheelSH1 = value;
				NotifyPropertyChanged("OptionsWheelSH1");
			}
		}
	}

	public Visibility OptionsWheelZoom
	{
		get
		{
			return _optionsWheelZoom;
		}
		set
		{
			if (_optionsWheelZoom != value)
			{
				_optionsWheelZoom = value;
				NotifyPropertyChanged("OptionsWheelZoom");
			}
		}
	}

	public Visibility OptionsCenteringSH1
	{
		get
		{
			return _optionsCenteringSH1;
		}
		set
		{
			if (_optionsCenteringSH1 != value)
			{
				_optionsCenteringSH1 = value;
				NotifyPropertyChanged("OptionsCenteringSH1");
			}
		}
	}

	public Visibility OptionsCenteringModern
	{
		get
		{
			return _optionsCenteringModern;
		}
		set
		{
			if (_optionsCenteringModern != value)
			{
				_optionsCenteringModern = value;
				NotifyPropertyChanged("OptionsCenteringModern");
			}
		}
	}

	public string MasterVolumeValue
	{
		get
		{
			return _masterVolumeValue;
		}
		set
		{
			if (_masterVolumeValue != value)
			{
				_masterVolumeValue = value;
				NotifyPropertyChanged("MasterVolumeValue");
			}
		}
	}

	public string MusicVolumeValue
	{
		get
		{
			return _musicVolumeValue;
		}
		set
		{
			if (_musicVolumeValue != value)
			{
				_musicVolumeValue = value;
				NotifyPropertyChanged("MusicVolumeValue");
			}
		}
	}

	public string SfxVolumeValue
	{
		get
		{
			return _sfxVolumeValue;
		}
		set
		{
			if (_sfxVolumeValue != value)
			{
				_sfxVolumeValue = value;
				NotifyPropertyChanged("SfxVolumeValue");
			}
		}
	}

	public string SpeechVolumeValue
	{
		get
		{
			return _speechVolumeValue;
		}
		set
		{
			if (_speechVolumeValue != value)
			{
				_speechVolumeValue = value;
				NotifyPropertyChanged("SpeechVolumeValue");
			}
		}
	}

	public string UnitSpeechVolumeValue
	{
		get
		{
			return _unitSpeechVolumeValue;
		}
		set
		{
			if (_unitSpeechVolumeValue != value)
			{
				_unitSpeechVolumeValue = value;
				NotifyPropertyChanged("UnitSpeechVolumeValue");
			}
		}
	}

	public string ScrollSpeedValue
	{
		get
		{
			return _scrollSpeedValue;
		}
		set
		{
			if (_scrollSpeedValue != value)
			{
				_scrollSpeedValue = value;
				NotifyPropertyChanged("ScrollSpeedValue");
			}
		}
	}

	public string GameSpeedValue
	{
		get
		{
			return _gameSpeedValue;
		}
		set
		{
			if (_gameSpeedValue != value)
			{
				_gameSpeedValue = value;
				NotifyPropertyChanged("GameSpeedValue");
			}
		}
	}

	public Visibility OptionsGameSpeedVis
	{
		get
		{
			return _optionsGameSpeedVis;
		}
		set
		{
			if (_optionsGameSpeedVis != value)
			{
				_optionsGameSpeedVis = value;
				NotifyPropertyChanged("OptionsGameSpeedVis");
			}
		}
	}

	public Visibility OptionsApplyVisible
	{
		get
		{
			return _optionsApplyVisible;
		}
		set
		{
			if (_optionsApplyVisible != value)
			{
				_optionsApplyVisible = value;
				NotifyPropertyChanged("OptionsApplyVisible");
			}
		}
	}

	public Visibility OptionsScaleApplyVisible
	{
		get
		{
			return _optionsScaleApplyVisible;
		}
		set
		{
			if (_optionsScaleApplyVisible != value)
			{
				_optionsScaleApplyVisible = value;
				NotifyPropertyChanged("OptionsScaleApplyVisible");
			}
		}
	}

	public Visibility OptionsHotKeyPanelVis
	{
		get
		{
			return _optionsHotKeyPanelVis;
		}
		set
		{
			if (_optionsHotKeyPanelVis != value)
			{
				_optionsHotKeyPanelVis = value;
				NotifyPropertyChanged("OptionsHotKeyPanelVis");
			}
		}
	}

	public string OptionsHotKeyTitle
	{
		get
		{
			return _optionsHotKeyTitle;
		}
		set
		{
			if (_optionsHotKeyTitle != value)
			{
				_optionsHotKeyTitle = value;
				NotifyPropertyChanged("OptionsHotKeyTitle");
			}
		}
	}

	public string OptionsHotKey1
	{
		get
		{
			return _optionsHotKey1;
		}
		set
		{
			if (_optionsHotKey1 != value)
			{
				_optionsHotKey1 = value;
				NotifyPropertyChanged("OptionsHotKey1");
			}
		}
	}

	public string OptionsHotKey2
	{
		get
		{
			return _optionsHotKey2;
		}
		set
		{
			if (_optionsHotKey2 != value)
			{
				_optionsHotKey2 = value;
				NotifyPropertyChanged("OptionsHotKey2");
			}
		}
	}

	public Visibility OptionsHotKeySelectVis
	{
		get
		{
			return _optionsHotKeySelectVis;
		}
		set
		{
			if (_optionsHotKeySelectVis != value)
			{
				_optionsHotKeySelectVis = value;
				NotifyPropertyChanged("OptionsHotKeySelectVis");
			}
		}
	}

	public Visibility OptionsHotKeyChangeVis
	{
		get
		{
			return _optionsHotKeyChangeVis;
		}
		set
		{
			if (_optionsHotKeyChangeVis != value)
			{
				_optionsHotKeyChangeVis = value;
				NotifyPropertyChanged("OptionsHotKeyChangeVis");
			}
		}
	}

	public string OptionsHotKeyCurrentKey
	{
		get
		{
			return _optionsHotKeyCurrentKey;
		}
		set
		{
			if (_optionsHotKeyCurrentKey != value)
			{
				_optionsHotKeyCurrentKey = value;
				NotifyPropertyChanged("OptionsHotKeyCurrentKey");
			}
		}
	}

	public string OptionsHotKeyNewKey
	{
		get
		{
			return _optionsHotKeyNewKey;
		}
		set
		{
			if (_optionsHotKeyNewKey != value)
			{
				_optionsHotKeyNewKey = value;
				NotifyPropertyChanged("OptionsHotKeyNewKey");
			}
		}
	}

	public string OptionsHotKeyWarning
	{
		get
		{
			return _optionsHotKeyWarning;
		}
		set
		{
			if (_optionsHotKeyWarning != value)
			{
				_optionsHotKeyWarning = value;
				NotifyPropertyChanged("OptionsHotKeyWarning");
			}
		}
	}

	public string OptionsFreeBuildingsText
	{
		get
		{
			return _optionsFreeBuildingsText;
		}
		set
		{
			if (_optionsFreeBuildingsText != value)
			{
				_optionsFreeBuildingsText = value;
				NotifyPropertyChanged("OptionsFreeBuildingsText");
			}
		}
	}

	public Visibility OptionsAchievementsDisabledVis
	{
		get
		{
			return _optionsAchievementsDisabledVis;
		}
		set
		{
			if (_optionsAchievementsDisabledVis != value)
			{
				_optionsAchievementsDisabledVis = value;
				NotifyPropertyChanged("OptionsAchievementsDisabledVis");
			}
		}
	}

	public Visibility OptionsInGameCheatsVis
	{
		get
		{
			return _optionsInGameCheatsVis;
		}
		set
		{
			if (_optionsInGameCheatsVis != value)
			{
				_optionsInGameCheatsVis = value;
				NotifyPropertyChanged("OptionsInGameCheatsVis");
			}
		}
	}

	public Visibility OptionsPlayerColourVis
	{
		get
		{
			return _optionsPlayerColourVis;
		}
		set
		{
			if (_optionsPlayerColourVis != value)
			{
				_optionsPlayerColourVis = value;
				NotifyPropertyChanged("OptionsPlayerColourVis");
			}
		}
	}

	public int SpriteWidth1
	{
		get
		{
			return _spriteWidth1;
		}
		set
		{
			_spriteWidth1 = value;
			NotifyPropertyChanged("SpriteWidth1");
		}
	}

	public int SpriteHeight1
	{
		get
		{
			return _spriteHeight1;
		}
		set
		{
			_spriteHeight1 = value;
			NotifyPropertyChanged("SpriteHeight1");
		}
	}

	public int SpriteWidth2
	{
		get
		{
			return _spriteWidth2;
		}
		set
		{
			_spriteWidth2 = value;
			NotifyPropertyChanged("SpriteWidth2");
		}
	}

	public int SpriteHeight2
	{
		get
		{
			return _spriteHeight2;
		}
		set
		{
			_spriteHeight2 = value;
			NotifyPropertyChanged("SpriteHeight2");
		}
	}

	public int SpriteWidth3
	{
		get
		{
			return _spriteWidth3;
		}
		set
		{
			_spriteWidth3 = value;
			NotifyPropertyChanged("SpriteWidth3");
		}
	}

	public int SpriteHeight3
	{
		get
		{
			return _spriteHeight3;
		}
		set
		{
			_spriteHeight3 = value;
			NotifyPropertyChanged("SpriteHeight3");
		}
	}

	public int SpriteWidth4
	{
		get
		{
			return _spriteWidth4;
		}
		set
		{
			_spriteWidth4 = value;
			NotifyPropertyChanged("SpriteWidth4");
		}
	}

	public int SpriteHeight4
	{
		get
		{
			return _spriteHeight4;
		}
		set
		{
			_spriteHeight4 = value;
			NotifyPropertyChanged("SpriteHeight4");
		}
	}

	public Visibility StoryHeadsBorderVisible
	{
		get
		{
			return _storyHeadsBorderVisible;
		}
		set
		{
			if (_storyHeadsBorderVisible != Visibility.Hidden)
			{
				_storyHeadsBorderVisible = Visibility.Hidden;
				NotifyPropertyChanged("StoryHeadsBorderVisible");
			}
		}
	}

	public Visibility StoryFirepitVisible
	{
		get
		{
			return _storyFirepitVisible;
		}
		set
		{
			if (_storyFirepitVisible != value)
			{
				_storyFirepitVisible = value;
				NotifyPropertyChanged("StoryFirepitVisible");
			}
		}
	}

	public string StoryTitle
	{
		get
		{
			return _storyTitle;
		}
		set
		{
			_storyTitle = value;
			NotifyPropertyChanged("StoryTitle");
		}
	}

	public string StoryNarration
	{
		get
		{
			return _storyNarration;
		}
		set
		{
			_storyNarration = value;
			NotifyPropertyChanged("StoryNarration");
		}
	}

	public string StoryCharacter
	{
		get
		{
			return _storyCharacter;
		}
		set
		{
			_storyCharacter = value;
			NotifyPropertyChanged("StoryCharacter");
		}
	}

	public bool Show_StoryTitle
	{
		get
		{
			return _show_StoryTitle;
		}
		set
		{
			_show_StoryTitle = value;
			NotifyPropertyChanged("Show_StoryTitle");
		}
	}

	public bool Show_StoryNarrative
	{
		get
		{
			return _show_StoryNarrative;
		}
		set
		{
			_show_StoryNarrative = value;
			NotifyPropertyChanged("Show_StoryNarrative");
		}
	}

	public bool Show_NarrativeBackground_Default
	{
		get
		{
			return _show_NarrativeBackground_Default;
		}
		set
		{
			_show_NarrativeBackground_Default = value;
			NotifyPropertyChanged("Show_NarrativeBackground_Default");
		}
	}

	public bool Show_NarrativeBackground_Extra1A
	{
		get
		{
			return _show_NarrativeBackground_Extra1A;
		}
		set
		{
			_show_NarrativeBackground_Extra1A = value;
			NotifyPropertyChanged("Show_NarrativeBackground_Extra1A");
		}
	}

	public bool Show_NarrativeBackground_Extra1B
	{
		get
		{
			return _show_NarrativeBackground_Extra1B;
		}
		set
		{
			_show_NarrativeBackground_Extra1B = value;
			NotifyPropertyChanged("Show_NarrativeBackground_Extra1B");
		}
	}

	public bool Show_NarrativeBackground_Extra2A
	{
		get
		{
			return _show_NarrativeBackground_Extra2A;
		}
		set
		{
			_show_NarrativeBackground_Extra2A = value;
			NotifyPropertyChanged("Show_NarrativeBackground_Extra2A");
		}
	}

	public bool Show_NarrativeBackground_Extra2B
	{
		get
		{
			return _show_NarrativeBackground_Extra2B;
		}
		set
		{
			_show_NarrativeBackground_Extra2B = value;
			NotifyPropertyChanged("Show_NarrativeBackground_Extra2B");
		}
	}

	public bool Show_NarrativeBackground_Extra3A
	{
		get
		{
			return _show_NarrativeBackground_Extra3A;
		}
		set
		{
			_show_NarrativeBackground_Extra3A = value;
			NotifyPropertyChanged("Show_NarrativeBackground_Extra3A");
		}
	}

	public bool Show_NarrativeBackground_Extra3B
	{
		get
		{
			return _show_NarrativeBackground_Extra3B;
		}
		set
		{
			_show_NarrativeBackground_Extra3B = value;
			NotifyPropertyChanged("Show_NarrativeBackground_Extra3B");
		}
	}

	public bool Show_NarrativeBackground_Extra4A
	{
		get
		{
			return _show_NarrativeBackground_Extra4A;
		}
		set
		{
			_show_NarrativeBackground_Extra4A = value;
			NotifyPropertyChanged("Show_NarrativeBackground_Extra4A");
		}
	}

	public bool Show_NarrativeBackground_Extra4B
	{
		get
		{
			return _show_NarrativeBackground_Extra4B;
		}
		set
		{
			_show_NarrativeBackground_Extra4B = value;
			NotifyPropertyChanged("Show_NarrativeBackground_Extra4B");
		}
	}

	public bool Show_NarrativeBackground_Extra5A
	{
		get
		{
			return _show_NarrativeBackground_Extra5A;
		}
		set
		{
			_show_NarrativeBackground_Extra5A = value;
			NotifyPropertyChanged("Show_NarrativeBackground_Extra5A");
		}
	}

	public bool Show_NarrativeBackground_Extra5B
	{
		get
		{
			return _show_NarrativeBackground_Extra5B;
		}
		set
		{
			_show_NarrativeBackground_Extra5B = value;
			NotifyPropertyChanged("Show_NarrativeBackground_Extra5B");
		}
	}

	public bool Show_StoryHeads
	{
		get
		{
			return _show_StoryHeads;
		}
		set
		{
			_show_StoryHeads = value;
			NotifyPropertyChanged("Show_StoryHeads");
		}
	}

	public bool Show_StoryCharacter
	{
		get
		{
			return _show_StoryCharacter;
		}
		set
		{
			_show_StoryCharacter = value;
			NotifyPropertyChanged("Show_StoryCharacter");
		}
	}

	public bool Show_StoryMap
	{
		get
		{
			return _show_StoryMap;
		}
		set
		{
			_show_StoryMap = value;
			NotifyPropertyChanged("Show_StoryMap");
		}
	}

	public bool Show_StoryVideo
	{
		get
		{
			return _show_StoryVideo;
		}
		set
		{
			_show_StoryVideo = value;
			NotifyPropertyChanged("Show_StoryVideo");
		}
	}

	public bool Show_Story_DEBUG
	{
		get
		{
			return _show_Story_DEBUG;
		}
		set
		{
			_show_Story_DEBUG = value;
			NotifyPropertyChanged("Show_Story_DEBUG");
		}
	}

	public bool ShowRat
	{
		get
		{
			return _showRat;
		}
		set
		{
			_showRat = value;
			NotifyPropertyChanged("ShowRat");
		}
	}

	public bool ShowSnake
	{
		get
		{
			return _showSnake;
		}
		set
		{
			_showSnake = value;
			NotifyPropertyChanged("ShowSnake");
		}
	}

	public bool ShowPig
	{
		get
		{
			return _showPig;
		}
		set
		{
			_showPig = value;
			NotifyPropertyChanged("ShowPig");
		}
	}

	public bool ShowWolf
	{
		get
		{
			return _showWolf;
		}
		set
		{
			_showWolf = value;
			NotifyPropertyChanged("ShowWolf");
		}
	}

	public float OpacityRat
	{
		get
		{
			return _opacityRat;
		}
		set
		{
			_opacityRat = value;
			NotifyPropertyChanged("OpacityRat");
		}
	}

	public float OpacitySnake
	{
		get
		{
			return _opacitySnake;
		}
		set
		{
			_opacitySnake = value;
			NotifyPropertyChanged("OpacitySnake");
		}
	}

	public float OpacityPig
	{
		get
		{
			return _opacityPig;
		}
		set
		{
			_opacityPig = value;
			NotifyPropertyChanged("OpacityPig");
		}
	}

	public float OpacityWolf
	{
		get
		{
			return _opacityWolf;
		}
		set
		{
			_opacityWolf = value;
			NotifyPropertyChanged("OpacityWolf");
		}
	}

	public ImageSource StoryImage
	{
		get
		{
			return _storyImage;
		}
		set
		{
			_storyImage = value;
			NotifyPropertyChanged("StoryImage");
		}
	}

	public ImageSource StoryImage1
	{
		get
		{
			return _storyImage1;
		}
		set
		{
			_storyImage1 = value;
			NotifyPropertyChanged("StoryImage1");
		}
	}

	public ImageSource StoryImage2
	{
		get
		{
			return _storyImage2;
		}
		set
		{
			_storyImage2 = value;
			NotifyPropertyChanged("StoryImage2");
		}
	}

	public ImageSource StoryImage3
	{
		get
		{
			return _storyImage3;
		}
		set
		{
			_storyImage3 = value;
			NotifyPropertyChanged("StoryImage3");
		}
	}

	public ImageSource StoryImage4
	{
		get
		{
			return _storyImage4;
		}
		set
		{
			_storyImage4 = value;
			NotifyPropertyChanged("StoryImage4");
		}
	}

	public ImageSource StoryImage5
	{
		get
		{
			return _storyImage5;
		}
		set
		{
			_storyImage5 = value;
			NotifyPropertyChanged("StoryImage5");
		}
	}

	public ImageSource StoryImage6
	{
		get
		{
			return _storyImage6;
		}
		set
		{
			_storyImage6 = value;
			NotifyPropertyChanged("StoryImage6");
		}
	}

	public ImageSource StoryImage7
	{
		get
		{
			return _storyImage7;
		}
		set
		{
			_storyImage7 = value;
			NotifyPropertyChanged("StoryImage7");
		}
	}

	public ImageSource StoryImage8
	{
		get
		{
			return _storyImage8;
		}
		set
		{
			_storyImage8 = value;
			NotifyPropertyChanged("StoryImage8");
		}
	}

	public ImageSource StoryImage9
	{
		get
		{
			return _storyImage9;
		}
		set
		{
			_storyImage9 = value;
			NotifyPropertyChanged("StoryImage9");
		}
	}

	public ImageSource StoryImage10
	{
		get
		{
			return _storyImage10;
		}
		set
		{
			_storyImage10 = value;
			NotifyPropertyChanged("StoryImage10");
		}
	}

	public ImageSource StoryImage11
	{
		get
		{
			return _storyImage11;
		}
		set
		{
			_storyImage11 = value;
			NotifyPropertyChanged("StoryImage11");
		}
	}

	public ImageSource StoryImage12
	{
		get
		{
			return _storyImage12;
		}
		set
		{
			_storyImage12 = value;
			NotifyPropertyChanged("StoryImage12");
		}
	}

	public ImageSource StoryImage13
	{
		get
		{
			return _storyImage13;
		}
		set
		{
			_storyImage13 = value;
			NotifyPropertyChanged("StoryImage13");
		}
	}

	public ImageSource StoryImage14
	{
		get
		{
			return _storyImage14;
		}
		set
		{
			_storyImage14 = value;
			NotifyPropertyChanged("StoryImage14");
		}
	}

	public ImageSource StoryImage15
	{
		get
		{
			return _storyImage15;
		}
		set
		{
			_storyImage15 = value;
			NotifyPropertyChanged("StoryImage15");
		}
	}

	public ImageSource StoryImage16
	{
		get
		{
			return _storyImage16;
		}
		set
		{
			_storyImage16 = value;
			NotifyPropertyChanged("StoryImage16");
		}
	}

	public ImageSource StoryImage17
	{
		get
		{
			return _storyImage17;
		}
		set
		{
			_storyImage17 = value;
			NotifyPropertyChanged("StoryImage17");
		}
	}

	public ImageSource StoryImage18
	{
		get
		{
			return _storyImage18;
		}
		set
		{
			_storyImage18 = value;
			NotifyPropertyChanged("StoryImage18");
		}
	}

	public ImageSource StoryImage19
	{
		get
		{
			return _storyImage19;
		}
		set
		{
			_storyImage19 = value;
			NotifyPropertyChanged("StoryImage19");
		}
	}

	public ImageSource StoryImage20
	{
		get
		{
			return _storyImage20;
		}
		set
		{
			_storyImage20 = value;
			NotifyPropertyChanged("StoryImage20");
		}
	}

	public int FlagXPos
	{
		get
		{
			return _flagXPos;
		}
		set
		{
			_flagXPos = value;
			NotifyPropertyChanged("FlagXPos");
		}
	}

	public int FlagYPos
	{
		get
		{
			return _flagYPos;
		}
		set
		{
			_flagYPos = value;
			NotifyPropertyChanged("FlagYPos");
		}
	}

	public int FlagXPos2
	{
		get
		{
			return _flagXPos2;
		}
		set
		{
			_flagXPos2 = value;
			NotifyPropertyChanged("FlagXPos2");
		}
	}

	public int FlagYPos2
	{
		get
		{
			return _flagYPos2;
		}
		set
		{
			_flagYPos2 = value;
			NotifyPropertyChanged("FlagYPos2");
		}
	}

	public string FadeCounty
	{
		get
		{
			return _fadeCounty;
		}
		set
		{
			_fadeCounty = value;
			NotifyPropertyChanged("FadeCounty");
		}
	}

	public Noesis.Color CountyColor1
	{
		get
		{
			return _countyColor1;
		}
		set
		{
			_countyColor1 = value;
			NotifyPropertyChanged("CountyColor1");
		}
	}

	public Noesis.Color CountyColor2
	{
		get
		{
			return _countyColor2;
		}
		set
		{
			_countyColor2 = value;
			NotifyPropertyChanged("CountyColor2");
		}
	}

	public Noesis.Color CountyColor3
	{
		get
		{
			return _countyColor3;
		}
		set
		{
			_countyColor3 = value;
			NotifyPropertyChanged("CountyColor3");
		}
	}

	public Noesis.Color CountyColor4
	{
		get
		{
			return _countyColor4;
		}
		set
		{
			_countyColor4 = value;
			NotifyPropertyChanged("CountyColor4");
		}
	}

	public Noesis.Color CountyColor5
	{
		get
		{
			return _countyColor5;
		}
		set
		{
			_countyColor5 = value;
			NotifyPropertyChanged("CountyColor5");
		}
	}

	public Noesis.Color CountyColor6
	{
		get
		{
			return _countyColor6;
		}
		set
		{
			_countyColor6 = value;
			NotifyPropertyChanged("CountyColor6");
		}
	}

	public Noesis.Color CountyColor7
	{
		get
		{
			return _countyColor7;
		}
		set
		{
			_countyColor7 = value;
			NotifyPropertyChanged("CountyColor7");
		}
	}

	public Noesis.Color CountyColor8
	{
		get
		{
			return _countyColor8;
		}
		set
		{
			_countyColor8 = value;
			NotifyPropertyChanged("CountyColor8");
		}
	}

	public Noesis.Color CountyColor9
	{
		get
		{
			return _countyColor9;
		}
		set
		{
			_countyColor9 = value;
			NotifyPropertyChanged("CountyColor9");
		}
	}

	public Noesis.Color CountyColor10
	{
		get
		{
			return _countyColor10;
		}
		set
		{
			_countyColor10 = value;
			NotifyPropertyChanged("CountyColor10");
		}
	}

	public Noesis.Color CountyColor11
	{
		get
		{
			return _countyColor11;
		}
		set
		{
			_countyColor11 = value;
			NotifyPropertyChanged("CountyColor11");
		}
	}

	public Noesis.Color CountyColor12
	{
		get
		{
			return _countyColor12;
		}
		set
		{
			_countyColor12 = value;
			NotifyPropertyChanged("CountyColor12");
		}
	}

	public Noesis.Color CountyColor13
	{
		get
		{
			return _countyColor13;
		}
		set
		{
			_countyColor13 = value;
			NotifyPropertyChanged("CountyColor13");
		}
	}

	public Noesis.Color CountyColor14
	{
		get
		{
			return _countyColor14;
		}
		set
		{
			_countyColor14 = value;
			NotifyPropertyChanged("CountyColor14");
		}
	}

	public Noesis.Color CountyColor15
	{
		get
		{
			return _countyColor15;
		}
		set
		{
			_countyColor15 = value;
			NotifyPropertyChanged("CountyColor15");
		}
	}

	public Noesis.Color CountyColor16
	{
		get
		{
			return _countyColor16;
		}
		set
		{
			_countyColor16 = value;
			NotifyPropertyChanged("CountyColor16");
		}
	}

	public Noesis.Color CountyColor17
	{
		get
		{
			return _countyColor17;
		}
		set
		{
			_countyColor17 = value;
			NotifyPropertyChanged("CountyColor17");
		}
	}

	public Noesis.Color CountyColor18
	{
		get
		{
			return _countyColor18;
		}
		set
		{
			_countyColor18 = value;
			NotifyPropertyChanged("CountyColor18");
		}
	}

	public Noesis.Color CountyColor19
	{
		get
		{
			return _countyColor19;
		}
		set
		{
			_countyColor19 = value;
			NotifyPropertyChanged("CountyColor19");
		}
	}

	public Noesis.Color CountyColor20
	{
		get
		{
			return _countyColor20;
		}
		set
		{
			_countyColor20 = value;
			NotifyPropertyChanged("CountyColor20");
		}
	}

	public bool OST_Date_Vis
	{
		get
		{
			return _OST_Date_Vis;
		}
		set
		{
			if (_OST_Date_Vis != value)
			{
				_OST_Date_Vis = value;
				NotifyPropertyChanged("OST_Date_Vis");
			}
		}
	}

	public string OST_Date_Text
	{
		get
		{
			return _OST_Date_Text;
		}
		set
		{
			if (_OST_Date_Text != value)
			{
				_OST_Date_Text = value;
				NotifyPropertyChanged("OST_Date_Text");
			}
		}
	}

	public bool OST_Game_Paused_Vis_Set
	{
		get
		{
			return _OST_Game_Paused_Vis_Set;
		}
		set
		{
			if (_OST_Game_Paused_Vis_Set != value)
			{
				_OST_Game_Paused_Vis_Set = value;
				OST_Game_Paused_Vis = _OST_Game_Paused_Vis_Set && _OST_Game_Paused_Vis_Allowed && _show_InGameUI;
			}
		}
	}

	public bool OST_Game_Paused_Vis_Allowed
	{
		get
		{
			return _OST_Game_Paused_Vis_Allowed;
		}
		set
		{
			if (_OST_Game_Paused_Vis_Allowed != value)
			{
				_OST_Game_Paused_Vis_Allowed = value;
				OST_Game_Paused_Vis = _OST_Game_Paused_Vis_Set && _OST_Game_Paused_Vis_Allowed && _show_InGameUI;
			}
		}
	}

	public bool OST_Game_Paused_Vis
	{
		get
		{
			return _OST_Game_Paused_Vis;
		}
		set
		{
			if (_OST_Game_Paused_Vis != value)
			{
				_OST_Game_Paused_Vis = value;
				NotifyPropertyChanged("OST_Game_Paused_Vis");
			}
		}
	}

	public string OST_Game_Paused_Text
	{
		get
		{
			return _OST_Game_Paused_Text;
		}
		set
		{
			if (_OST_Game_Paused_Text != value)
			{
				_OST_Game_Paused_Text = value;
				NotifyPropertyChanged("OST_Game_Paused_Text");
			}
		}
	}

	public bool OST_Keep_Message_Vis
	{
		get
		{
			return _OST_Keep_Message_Vis;
		}
		set
		{
			if (_OST_Keep_Message_Vis != value)
			{
				_OST_Keep_Message_Vis = value;
				NotifyPropertyChanged("OST_Keep_Message_Vis");
			}
		}
	}

	public string OST_Keep_Message_Text
	{
		get
		{
			return _OST_Keep_Message_Text;
		}
		set
		{
			if (_OST_Keep_Message_Text != value)
			{
				_OST_Keep_Message_Text = value;
				NotifyPropertyChanged("OST_Keep_Message_Text");
			}
		}
	}

	public bool OST_Feedback_1_Vis
	{
		get
		{
			return _OST_Feedback_1_Vis;
		}
		set
		{
			if (_OST_Feedback_1_Vis != value)
			{
				_OST_Feedback_1_Vis = value;
				NotifyPropertyChanged("OST_Feedback_1_Vis");
			}
		}
	}

	public string OST_Feedback_1_Text
	{
		get
		{
			return _OST_Feedback_1_Text;
		}
		set
		{
			if (_OST_Feedback_1_Text != value)
			{
				_OST_Feedback_1_Text = value;
				NotifyPropertyChanged("OST_Feedback_1_Text");
			}
		}
	}

	public bool OST_Message_Bar_Vis
	{
		get
		{
			return _OST_Message_Bar_Vis;
		}
		set
		{
			if (_OST_Message_Bar_Vis != value)
			{
				_OST_Message_Bar_Vis = value;
				NotifyPropertyChanged("OST_Message_Bar_Vis");
			}
		}
	}

	public string OST_Message_Bar_Text
	{
		get
		{
			return _OST_Message_Bar_Text;
		}
		set
		{
			if (_OST_Message_Bar_Text != value)
			{
				_OST_Message_Bar_Text = value;
				NotifyPropertyChanged("OST_Message_Bar_Text");
			}
		}
	}

	public string OST_Message_Bar_Margin
	{
		get
		{
			return _OST_Message_Bar_Margin;
		}
		set
		{
			if (_OST_Message_Bar_Margin != value)
			{
				_OST_Message_Bar_Margin = value;
				NotifyPropertyChanged("OST_Message_Bar_Margin");
			}
		}
	}

	public bool OST_Framerate_Vis
	{
		get
		{
			return _OST_Framerate_Vis;
		}
		set
		{
			if (_OST_Framerate_Vis != value)
			{
				_OST_Framerate_Vis = value;
				NotifyPropertyChanged("OST_Framerate_Vis");
			}
		}
	}

	public string OST_Framerate_Text
	{
		get
		{
			return _OST_Framerate_Text;
		}
		set
		{
			if (_OST_Framerate_Text != value)
			{
				_OST_Framerate_Text = value;
				NotifyPropertyChanged("OST_Framerate_Text");
			}
		}
	}

	public string OST_Framerate_Margin
	{
		get
		{
			return _OST_Framerate_Margin;
		}
		set
		{
			if (_OST_Framerate_Margin != value)
			{
				_OST_Framerate_Margin = value;
				NotifyPropertyChanged("OST_Framerate_Margin");
			}
		}
	}

	public bool OST_GameSpeed_Vis
	{
		get
		{
			return _OST_GameSpeed_Vis;
		}
		set
		{
			if (_OST_GameSpeed_Vis != value)
			{
				_OST_GameSpeed_Vis = value;
				NotifyPropertyChanged("OST_GameSpeed_Vis");
			}
		}
	}

	public string OST_GameSpeed_Text
	{
		get
		{
			return _OST_GameSpeed_Text;
		}
		set
		{
			if (_OST_GameSpeed_Text != value)
			{
				_OST_GameSpeed_Text = value;
				NotifyPropertyChanged("OST_GameSpeed_Text");
			}
		}
	}

	public string OST_GameSpeed_Margin
	{
		get
		{
			return _OST_GameSpeed_Margin;
		}
		set
		{
			if (_OST_GameSpeed_Margin != value)
			{
				_OST_GameSpeed_Margin = value;
				NotifyPropertyChanged("OST_GameSpeed_Margin");
			}
		}
	}

	public SolidColorBrush OST_GameSpeed_Colour
	{
		get
		{
			return _OST_GameSpeed_Colour;
		}
		set
		{
			if (_OST_GameSpeed_Colour != value)
			{
				_OST_GameSpeed_Colour = value;
				NotifyPropertyChanged("OST_GameSpeed_Colour");
			}
		}
	}

	public bool OST_LeftStack_Vis
	{
		get
		{
			return _OST_LeftStack_Vis;
		}
		set
		{
			if (_OST_LeftStack_Vis != value)
			{
				_OST_LeftStack_Vis = value;
				NotifyPropertyChanged("OST_LeftStack_Vis");
			}
		}
	}

	public bool OST_Starting_goods_Vis
	{
		get
		{
			return _OST_Starting_goods_Vis;
		}
		set
		{
			_OST_Starting_goods_Vis = value;
			OST_LeftStack_Vis = _OST_Starting_goods_Vis | _OST_Time_Until_Vis;
		}
	}

	public bool OST_Time_Until_Vis
	{
		get
		{
			return _OST_Time_Until_Vis;
		}
		set
		{
			if (_OST_Time_Until_Vis != value)
			{
				_OST_Time_Until_Vis = value;
				NotifyPropertyChanged("OST_Time_Until_Vis");
			}
			OST_LeftStack_Vis = _OST_Starting_goods_Vis | _OST_Time_Until_Vis;
		}
	}

	public string OST_Time_Until_Text
	{
		get
		{
			return _OST_Time_Until_Text;
		}
		set
		{
			if (_OST_Time_Until_Text != value)
			{
				_OST_Time_Until_Text = value;
				NotifyPropertyChanged("OST_Time_Until_Text");
			}
		}
	}

	public int OST_Time_Until_Width
	{
		get
		{
			return _OST_Time_Until_Width;
		}
		set
		{
			if (_OST_Time_Until_Width != value)
			{
				_OST_Time_Until_Width = value;
				NotifyPropertyChanged("OST_Time_Until_Width");
			}
		}
	}

	public bool OST_PeopleLeft_Vis
	{
		get
		{
			return _OST_PeopleLeft_Vis;
		}
		set
		{
			if (_OST_PeopleLeft_Vis != value)
			{
				_OST_PeopleLeft_Vis = value;
				NotifyPropertyChanged("OST_PeopleLeft_Vis");
			}
		}
	}

	public string OST_PeopleLeft_Text
	{
		get
		{
			return _OST_PeopleLeft_Text;
		}
		set
		{
			if (_OST_PeopleLeft_Text != value)
			{
				_OST_PeopleLeft_Text = value;
				NotifyPropertyChanged("OST_PeopleLeft_Text");
			}
		}
	}

	public string OST_StructsLeft_Text
	{
		get
		{
			return _OST_StructsLeft_Text;
		}
		set
		{
			if (_OST_StructsLeft_Text != value)
			{
				_OST_StructsLeft_Text = value;
				NotifyPropertyChanged("OST_StructsLeft_Text");
			}
		}
	}

	public string OST_TreesLeft_Text
	{
		get
		{
			return _OST_TreesLeft_Text;
		}
		set
		{
			if (_OST_TreesLeft_Text != value)
			{
				_OST_TreesLeft_Text = value;
				NotifyPropertyChanged("OST_TreesLeft_Text");
			}
		}
	}

	public string OST_RocksLeft_Text
	{
		get
		{
			return _OST_RocksLeft_Text;
		}
		set
		{
			if (_OST_RocksLeft_Text != value)
			{
				_OST_RocksLeft_Text = value;
				NotifyPropertyChanged("OST_RocksLeft_Text");
			}
		}
	}

	public string OST_TribesLeft_Text
	{
		get
		{
			return _OST_TribesLeft_Text;
		}
		set
		{
			if (_OST_TribesLeft_Text != value)
			{
				_OST_TribesLeft_Text = value;
				NotifyPropertyChanged("OST_TribesLeft_Text");
			}
		}
	}

	public bool OST_WhoOwns_Vis
	{
		get
		{
			return _OST_WhoOwns_Vis;
		}
		set
		{
			if (_OST_WhoOwns_Vis != value)
			{
				_OST_WhoOwns_Vis = value;
				NotifyPropertyChanged("OST_WhoOwns_Vis");
			}
		}
	}

	public string OST_WhoOwns_Text
	{
		get
		{
			return _OST_WhoOwns_Text;
		}
		set
		{
			if (_OST_WhoOwns_Text != value)
			{
				_OST_WhoOwns_Text = value;
				NotifyPropertyChanged("OST_WhoOwns_Text");
			}
		}
	}

	public bool OST_KOTH_Vis
	{
		get
		{
			return _OST_KOTH_Vis;
		}
		set
		{
			if (_OST_KOTH_Vis != value)
			{
				_OST_KOTH_Vis = value;
				NotifyPropertyChanged("OST_KOTH_Vis");
			}
		}
	}

	public bool OST_Ping_Vis
	{
		get
		{
			return _OST_Ping_Vis;
		}
		set
		{
			if (_OST_Ping_Vis != value)
			{
				_OST_Ping_Vis = value;
				NotifyPropertyChanged("OST_Ping_Vis");
			}
		}
	}

	public bool OST_MissionFinished_Vis
	{
		get
		{
			return _OST_MissionFinished_Vis;
		}
		set
		{
			if (_OST_MissionFinished_Vis != value)
			{
				_OST_MissionFinished_Vis = value;
				NotifyPropertyChanged("OST_MissionFinished_Vis");
			}
		}
	}

	public string OST_MissionFinished_Text
	{
		get
		{
			return _OST_MissionFinished_Text;
		}
		set
		{
			if (_OST_MissionFinished_Text != value)
			{
				_OST_MissionFinished_Text = value;
				NotifyPropertyChanged("OST_MissionFinished_Text");
			}
		}
	}

	public bool OST_Cart_Vis
	{
		get
		{
			return _OST_Cart_Vis;
		}
		set
		{
			if (_OST_Cart_Vis != value)
			{
				_OST_Cart_Vis = value;
				NotifyPropertyChanged("OST_Cart_Vis");
			}
		}
	}

	public KeyTime CartDuration
	{
		get
		{
			return _cartDuration;
		}
		set
		{
			_cartDuration = value;
			NotifyPropertyChanged("CartDuration");
		}
	}

	public int CartDistance
	{
		get
		{
			return _cartDistance;
		}
		set
		{
			if (_cartDistance != value)
			{
				_cartDistance = value;
				NotifyPropertyChanged("CartDistance");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public void MapEditorSetupFunction(object param)
	{
		FRONTEditorSetup.ButtonClicked((string)param);
	}

	public void MultiplayerMenuCommandFunction(object param)
	{
		FRONTMultiplayer.ButtonClicked((string)param);
	}

	private void setUpObservableCollections_FRONT_StandaloneMission()
	{
		SiegeAttackForces = new ObservableCollection<string>();
		for (int i = 0; i < 11; i++)
		{
			SiegeAttackForces.Add("");
		}
	}

	public void StandaloneMenuCommandFunction(object param)
	{
		FRONTStandaloneMission.ButtonClicked((string)param);
	}

	private void setUpObservableCollections_HUD_MissionOver()
	{
		MO_OtherPointsText = new ObservableCollection<string>();
		MO_OtherPoints = new ObservableCollection<string>();
		MO_OtherPointsVisible = new ObservableCollection<Visibility>();
		MO_MP_PlayersVisible = new ObservableCollection<Visibility>();
		MO_MP_Players1Values = new ObservableCollection<string>();
		MO_MP_Players2Values = new ObservableCollection<string>();
		MO_MP_Players3Values = new ObservableCollection<string>();
		MO_MP_Players4Values = new ObservableCollection<string>();
		MO_MP_Players5Values = new ObservableCollection<string>();
		MO_MP_Players6Values = new ObservableCollection<string>();
		MO_MP_Players7Values = new ObservableCollection<string>();
		MO_MP_Players8Values = new ObservableCollection<string>();
		MO_MP_Players1Icons = new ObservableCollection<Visibility>();
		MO_MP_Players2Icons = new ObservableCollection<Visibility>();
		MO_MP_Players3Icons = new ObservableCollection<Visibility>();
		MO_MP_Players4Icons = new ObservableCollection<Visibility>();
		MO_MP_Players5Icons = new ObservableCollection<Visibility>();
		MO_MP_Players6Icons = new ObservableCollection<Visibility>();
		MO_MP_Players7Icons = new ObservableCollection<Visibility>();
		MO_MP_Players8Icons = new ObservableCollection<Visibility>();
		for (int i = 0; i < 7; i++)
		{
			MO_OtherPointsText.Add("");
			MO_OtherPoints.Add("");
			MO_OtherPointsVisible.Add(Visibility.Hidden);
		}
		for (int j = 0; j < 8; j++)
		{
			MO_MP_PlayersVisible.Add(Visibility.Collapsed);
		}
		for (int k = 0; k < 11; k++)
		{
			MO_MP_Players1Values.Add("");
			MO_MP_Players2Values.Add("");
			MO_MP_Players3Values.Add("");
			MO_MP_Players4Values.Add("");
			MO_MP_Players5Values.Add("");
			MO_MP_Players6Values.Add("");
			MO_MP_Players7Values.Add("");
			MO_MP_Players8Values.Add("");
		}
		for (int l = 0; l < 14; l++)
		{
			MO_MP_Players1Icons.Add(Visibility.Collapsed);
			MO_MP_Players2Icons.Add(Visibility.Collapsed);
			MO_MP_Players3Icons.Add(Visibility.Collapsed);
			MO_MP_Players4Icons.Add(Visibility.Collapsed);
			MO_MP_Players5Icons.Add(Visibility.Collapsed);
			MO_MP_Players6Icons.Add(Visibility.Collapsed);
			MO_MP_Players7Icons.Add(Visibility.Collapsed);
			MO_MP_Players8Icons.Add(Visibility.Collapsed);
		}
	}

	private void MO_ClickFunction(object parameter)
	{
		HUDMissionOver.ButtonClicked();
	}

	public void ButtonWorkshopFunction(object param)
	{
		HUDWorkshopUploader.ButtonClicked((string)param);
	}

	private void Setup_eGoodsDictionary()
	{
		eGoodsLookUp = new Dictionary<string, int>();
		eGoodsLookUp.Add("STORED_NULL", 0);
		eGoodsLookUp.Add("STORED_WOOD_LOGS", 1);
		eGoodsLookUp.Add("STORED_WOOD_PLANKS", 2);
		eGoodsLookUp.Add("STORED_RAW_HOPS", 3);
		eGoodsLookUp.Add("STORED_STONE_BLOCKS", 4);
		eGoodsLookUp.Add("STORED_COW_HIDES", 5);
		eGoodsLookUp.Add("STORED_IRON_INGOTS", 6);
		eGoodsLookUp.Add("STORED_PITCH_RAW", 7);
		eGoodsLookUp.Add("STORED_PITCH_REFINED", 8);
		eGoodsLookUp.Add("STORED_RAW_WHEAT", 9);
		eGoodsLookUp.Add("STORED_FOOD_BREAD", 10);
		eGoodsLookUp.Add("STORED_FOOD_CHEESE", 11);
		eGoodsLookUp.Add("STORED_FOOD_MEAT", 12);
		eGoodsLookUp.Add("STORED_FOOD_FRUIT", 13);
		eGoodsLookUp.Add("STORED_FOOD_ALE", 14);
		eGoodsLookUp.Add("STORED_GOLD", 15);
		eGoodsLookUp.Add("STORED_FLOUR", 16);
		eGoodsLookUp.Add("STORED_BOWS", 17);
		eGoodsLookUp.Add("STORED_CROSSBOWS", 18);
		eGoodsLookUp.Add("STORED_SPEARS", 19);
		eGoodsLookUp.Add("STORED_PIKES", 20);
		eGoodsLookUp.Add("STORED_MACES", 21);
		eGoodsLookUp.Add("STORED_SWORDS", 22);
		eGoodsLookUp.Add("STORED_LEATHER_ARMOUR", 23);
		eGoodsLookUp.Add("STORED_METAL_ARMOUR", 24);
	}

	private void Setup_eChimpDictionary()
	{
		eChimpLookUp = new Dictionary<string, int>();
		eChimpLookUp.Add("STRUCT_NULL", 0);
		eChimpLookUp.Add("CHIMP_TYPE_PEASANT", 1);
		eChimpLookUp.Add("CHIMP_TYPE_BURNING_MAN", 2);
		eChimpLookUp.Add("CHIMP_TYPE_WOODCUTTER", 3);
		eChimpLookUp.Add("CHIMP_TYPE_FLETCHER", 4);
		eChimpLookUp.Add("CHIMP_TYPE_TUNNELER", 5);
		eChimpLookUp.Add("CHIMP_TYPE_HUNTER", 6);
		eChimpLookUp.Add("CHIMP_TYPE_QUARRY_MASON", 7);
		eChimpLookUp.Add("CHIMP_TYPE_QUARRY_GRUNT", 8);
		eChimpLookUp.Add("CHIMP_TYPE_QUARRY_OX", 9);
		eChimpLookUp.Add("CHIMP_TYPE_PITCHMAN", 10);
		eChimpLookUp.Add("CHIMP_TYPE_FARMER_WHEAT", 11);
		eChimpLookUp.Add("CHIMP_TYPE_FARMER_HOPS", 12);
		eChimpLookUp.Add("CHIMP_TYPE_FARMER_APPLE", 13);
		eChimpLookUp.Add("CHIMP_TYPE_FARMER_CATTLE", 14);
		eChimpLookUp.Add("CHIMP_TYPE_MILLER", 15);
		eChimpLookUp.Add("CHIMP_TYPE_BAKER", 16);
		eChimpLookUp.Add("CHIMP_TYPE_BREWER", 17);
		eChimpLookUp.Add("CHIMP_TYPE_POLETURNER", 18);
		eChimpLookUp.Add("CHIMP_TYPE_BLACKSMITH", 19);
		eChimpLookUp.Add("CHIMP_TYPE_ARMOURER", 20);
		eChimpLookUp.Add("CHIMP_TYPE_TANNER", 21);
		eChimpLookUp.Add("CHIMP_TYPE_ARCHER", 22);
		eChimpLookUp.Add("CHIMP_TYPE_XBOWMAN", 23);
		eChimpLookUp.Add("CHIMP_TYPE_SPEARMAN", 24);
		eChimpLookUp.Add("CHIMP_TYPE_PIKEMAN", 25);
		eChimpLookUp.Add("CHIMP_TYPE_MACEMAN", 26);
		eChimpLookUp.Add("CHIMP_TYPE_SWORDSMAN", 27);
		eChimpLookUp.Add("CHIMP_TYPE_KNIGHT", 28);
		eChimpLookUp.Add("CHIMP_TYPE_LADDERMAN", 29);
		eChimpLookUp.Add("CHIMP_TYPE_ENGINEER", 30);
		eChimpLookUp.Add("CHIMP_TYPE_MINER1", 31);
		eChimpLookUp.Add("CHIMP_TYPE_MINER2", 32);
		eChimpLookUp.Add("CHIMP_TYPE_PREIST", 33);
		eChimpLookUp.Add("CHIMP_TYPE_HEALER", 34);
		eChimpLookUp.Add("CHIMP_TYPE_DRUNKARD", 35);
		eChimpLookUp.Add("CHIMP_TYPE_INNKEEPER", 36);
		eChimpLookUp.Add("CHIMP_TYPE_MONK", 37);
		eChimpLookUp.Add("CHIMP_TYPE_ARCHER_debug", 38);
		eChimpLookUp.Add("CHIMP_TYPE_CATAPULT", 39);
		eChimpLookUp.Add("CHIMP_TYPE_TREBUCHET", 40);
		eChimpLookUp.Add("CHIMP_TYPE_MANGONEL", 41);
		eChimpLookUp.Add("CHIMP_TYPE_TRADER", 42);
		eChimpLookUp.Add("CHIMP_TYPE_TRADER_HORSE", 43);
		eChimpLookUp.Add("CHIMP_TYPE_DEER", 44);
		eChimpLookUp.Add("CHIMP_TYPE_WOLF", 45);
		eChimpLookUp.Add("CHIMP_TYPE_RABBIT", 46);
		eChimpLookUp.Add("CHIMP_TYPE_BEAR", 47);
		eChimpLookUp.Add("CHIMP_TYPE_CROW", 48);
		eChimpLookUp.Add("CHIMP_TYPE_SEAGULL", 49);
		eChimpLookUp.Add("CHIMP_SIEGE_TENT", 50);
		eChimpLookUp.Add("CHIMP_TYPE_COW", 51);
		eChimpLookUp.Add("CHIMP_TYPE_DOG", 52);
		eChimpLookUp.Add("CHIMP_TYPE_FIREMAN", 53);
		eChimpLookUp.Add("CHIMP_TYPE_GHOST", 54);
		eChimpLookUp.Add("CHIMP_TYPE_LORD", 55);
		eChimpLookUp.Add("CHIMP_TYPE_LADY", 56);
		eChimpLookUp.Add("CHIMP_TYPE_JESTER", 57);
		eChimpLookUp.Add("CHIMP_TYPE_SIEGE_TOWER", 58);
		eChimpLookUp.Add("CHIMP_TYPE_BATTERING_RAM", 59);
		eChimpLookUp.Add("CHIMP_TYPE_PORTABLE_SHIELD", 60);
		eChimpLookUp.Add("CHIMP_TYPE_BALLISTA", 61);
		eChimpLookUp.Add("CHIMP_TYPE_CHICKEN", 62);
		eChimpLookUp.Add("CHIMP_TYPE_MOTHER", 63);
		eChimpLookUp.Add("CHIMP_TYPE_CHILD", 64);
		eChimpLookUp.Add("CHIMP_TYPE_JUGGLER", 65);
		eChimpLookUp.Add("CHIMP_TYPE_FIREEATER", 66);
		eChimpLookUp.Add("CHIMP_TYPE_WAR_DOG", 67);
		eChimpLookUp.Add("CHIMP_TYPE_BURNING_ANIMAL_BIG", 68);
		eChimpLookUp.Add("CHIMP_TYPE_BURNING_ANIMAL_SMALL", 69);
	}

	private void Setup_eStructDictionary()
	{
		eStructLookUp = new Dictionary<string, int>();
		eStructLookUp.Add("STRUCT_NULL", 0);
		eStructLookUp.Add("STRUCT_HOVEL", 1);
		eStructLookUp.Add("STRUCT_HOUSE", 2);
		eStructLookUp.Add("STRUCT_WOODCUTTERS_HUT", 3);
		eStructLookUp.Add("STRUCT_OXEN_BASE", 4);
		eStructLookUp.Add("STRUCT_IRON_MINE", 5);
		eStructLookUp.Add("STRUCT_PITCH_DIGGER", 6);
		eStructLookUp.Add("STRUCT_HUNTERS_HUT", 7);
		eStructLookUp.Add("STRUCT_BARRACKS_WOOD", 8);
		eStructLookUp.Add("STRUCT_BARRACKS_STONE", 9);
		eStructLookUp.Add("STRUCT_GOODS_YARD", 10);
		eStructLookUp.Add("STRUCT_ARMOURY", 11);
		eStructLookUp.Add("STRUCT_FLETCHERS_WORKSHOP", 12);
		eStructLookUp.Add("STRUCT_BLACKSMITHS_WORKSHOP", 13);
		eStructLookUp.Add("STRUCT_POLETURNERS_WORKSHOP", 14);
		eStructLookUp.Add("STRUCT_ARMOURERS_WORKSHOP", 15);
		eStructLookUp.Add("STRUCT_TANNERS_WORKSHOP", 16);
		eStructLookUp.Add("STRUCT_BAKERS_WORKSHOP", 17);
		eStructLookUp.Add("STRUCT_BREWERS_WORKSHOP", 18);
		eStructLookUp.Add("STRUCT_GRANARY", 19);
		eStructLookUp.Add("STRUCT_QUARRY", 20);
		eStructLookUp.Add("STRUCT_QUARRYPILE", 21);
		eStructLookUp.Add("STRUCT_INN", 22);
		eStructLookUp.Add("STRUCT_HEALER", 23);
		eStructLookUp.Add("STRUCT_ENGINEERS_GUILD", 24);
		eStructLookUp.Add("STRUCT_TUNNELLERS_GUILD", 25);
		eStructLookUp.Add("STRUCT_TRADEPOST", 26);
		eStructLookUp.Add("STRUCT_WELL", 27);
		eStructLookUp.Add("STRUCT_OIL_SMELTER", 28);
		eStructLookUp.Add("STRUCT_SIEGE_TENT", 29);
		eStructLookUp.Add("STRUCT_WHEATFARM", 30);
		eStructLookUp.Add("STRUCT_HOPSFARM", 31);
		eStructLookUp.Add("STRUCT_APPLEFARM", 32);
		eStructLookUp.Add("STRUCT_CATTLEFARM", 33);
		eStructLookUp.Add("STRUCT_MILL", 34);
		eStructLookUp.Add("STRUCT_STABLES", 35);
		eStructLookUp.Add("STRUCT_CHURCH1", 36);
		eStructLookUp.Add("STRUCT_CHURCH2", 37);
		eStructLookUp.Add("STRUCT_CHURCH3", 38);
		eStructLookUp.Add("STRUCT_RUINS", 39);
		eStructLookUp.Add("STRUCT_KEEP_ONE", 40);
		eStructLookUp.Add("STRUCT_KEEP_TWO", 41);
		eStructLookUp.Add("STRUCT_KEEP_THREE", 42);
		eStructLookUp.Add("STRUCT_KEEP_FOUR", 43);
		eStructLookUp.Add("STRUCT_KEEP_FIVE", 44);
		eStructLookUp.Add("STRUCT_GATE_MAIN", 45);
		eStructLookUp.Add("STRUCT_GATE_INNER", 46);
		eStructLookUp.Add("STRUCT_GATE_WOOD", 47);
		eStructLookUp.Add("STRUCT_GATE_POSTERN", 48);
		eStructLookUp.Add("STRUCT_DRAWBRIDGE", 49);
		eStructLookUp.Add("STRUCT_TUNNEL_ENTERANCE", 50);
		eStructLookUp.Add("STRUCT_PARADEGROUND_OIL", 51);
		eStructLookUp.Add("STRUCT_SIGNPOST", 52);
		eStructLookUp.Add("STRUCT_PARADEGROUND_ENG", 53);
		eStructLookUp.Add("STRUCT_UNUSED_1", 54);
		eStructLookUp.Add("STRUCT_CAMPGROUND", 55);
		eStructLookUp.Add("STRUCT_PARADEGROUND_MISS", 56);
		eStructLookUp.Add("STRUCT_PARADEGROUND_LGT", 57);
		eStructLookUp.Add("STRUCT_PARADEGROUND_HVY", 58);
		eStructLookUp.Add("STRUCT_PARADEGROUND_TUN", 59);
		eStructLookUp.Add("STRUCT_GATEHOUSE", 60);
		eStructLookUp.Add("STRUCT_TOWER", 61);
		eStructLookUp.Add("STRUCT_GALLOWS", 62);
		eStructLookUp.Add("STRUCT_STOCKS", 63);
		eStructLookUp.Add("STRUCT_WITCH_HOIST", 64);
		eStructLookUp.Add("STRUCT_MAYPOLE", 65);
		eStructLookUp.Add("STRUCT_GARDEN", 66);
		eStructLookUp.Add("STRUCT_KILLING_PIT", 67);
		eStructLookUp.Add("STRUCT_PITCH_DITCH", 68);
		eStructLookUp.Add("STRUCT_SIEGE_TOWER", 69);
		eStructLookUp.Add("STRUCT_UNUSED_2", 70);
		eStructLookUp.Add("STRUCT_KEEPDOOR_LEFT", 71);
		eStructLookUp.Add("STRUCT_KEEPDOOR_RIGHT", 72);
		eStructLookUp.Add("STRUCT_KEEPDOOR", 73);
		eStructLookUp.Add("STRUCT_TOWER1", 74);
		eStructLookUp.Add("STRUCT_TOWER2", 75);
		eStructLookUp.Add("STRUCT_TOWER3", 76);
		eStructLookUp.Add("STRUCT_TOWER4", 77);
		eStructLookUp.Add("STRUCT_TOWER5", 78);
		eStructLookUp.Add("STRUCT_TOWER5_DESTROYED", 79);
		eStructLookUp.Add("STRUCT_SIEGE_TENT_CATAPULT", 80);
		eStructLookUp.Add("STRUCT_SIEGE_TENT_TREBUCHET", 81);
		eStructLookUp.Add("STRUCT_SIEGE_TENT_SIEGE_TOWER", 82);
		eStructLookUp.Add("STRUCT_SIEGE_TENT_BATTERING_RAM", 83);
		eStructLookUp.Add("STRUCT_SIEGE_TENT_PORTABLE_SHIELD", 84);
		eStructLookUp.Add("STRUCT_TUNNEL_CONSTRUCTION", 85);
		eStructLookUp.Add("STRUCT_TOWER1_DESTROYED", 86);
		eStructLookUp.Add("STRUCT_TOWER2_DESTROYED", 87);
		eStructLookUp.Add("STRUCT_TOWER3_DESTROYED", 88);
		eStructLookUp.Add("STRUCT_TOWER4_DESTROYED", 89);
		eStructLookUp.Add("STRUCT_WAS_WALL", 90);
		eStructLookUp.Add("STRUCT_CESS_PIT", 91);
		eStructLookUp.Add("STRUCT_BURNING_STAKE", 92);
		eStructLookUp.Add("STRUCT_GIBBET", 93);
		eStructLookUp.Add("STRUCT_DUNGEON", 94);
		eStructLookUp.Add("STRUCT_RACK_STRETCHING", 95);
		eStructLookUp.Add("STRUCT_RACK_FLOGGING", 96);
		eStructLookUp.Add("STRUCT_CHOPPING_BLOCK", 97);
		eStructLookUp.Add("STRUCT_DUNKING_STOOL", 98);
		eStructLookUp.Add("STRUCT_DOG_CAGE", 99);
		eStructLookUp.Add("STRUCT_STATUE", 100);
		eStructLookUp.Add("STRUCT_SHRINE", 101);
		eStructLookUp.Add("STRUCT_BEE_HIVE", 102);
		eStructLookUp.Add("STRUCT_DANCING_BEAR", 103);
		eStructLookUp.Add("STRUCT_POND", 104);
		eStructLookUp.Add("STRUCT_BEAR_CAVE", 105);
		eStructLookUp.Add("STRUCT_UNUSED_3", 106);
		eStructLookUp.Add("STRUCT_NEW_DELETE", 107);
		eStructLookUp.Add("STRUCT_NEW_EDITOR_DELETE", 209);
		eStructLookUp.Add("STRUCT_NEW_DIG_MOAT", 108);
		eStructLookUp.Add("STRUCT_NEW_FILL_MOAT", 109);
		eStructLookUp.Add("STRUCT_WOOD_WALL", 110);
		eStructLookUp.Add("STRUCT_STONE_WALL", 111);
		eStructLookUp.Add("STRUCT_CRENAL_WALL", 112);
		eStructLookUp.Add("STRUCT_STAIRS", 113);
		eStructLookUp.Add("STRUCT_BRAZIER", 114);
		eStructLookUp.Add("STRUCT_MANGONEL", 115);
		eStructLookUp.Add("STRUCT_BALLISTA", 116);
		eStructLookUp.Add("STRUCT_HEAD_ON_SPIKE", 117);
		eStructLookUp.Add("STRUCT_GARDEN_SMALL", 118);
		eStructLookUp.Add("STRUCT_GARDEN_MED", 119);
		eStructLookUp.Add("STRUCT_GARDEN_LARGE", 120);
		eStructLookUp.Add("STRUCT_POND_SMALL", 121);
		eStructLookUp.Add("STRUCT_POND_LARGE", 122);
		eStructLookUp.Add("STRUCT_FLAG1", 123);
		eStructLookUp.Add("STRUCT_FLAG2", 124);
		eStructLookUp.Add("STRUCT_FLAG3", 125);
		eStructLookUp.Add("STRUCT_FLAG4", 126);
		eStructLookUp.Add("STRUCT_GATE_WOOD1A", 127);
		eStructLookUp.Add("STRUCT_GATE_WOOD1B", 128);
		eStructLookUp.Add("STRUCT_GATE_WOOD1C", 129);
		eStructLookUp.Add("STRUCT_GATE_WOOD1D", 130);
		eStructLookUp.Add("STRUCT_GATE_STONE1A", 131);
		eStructLookUp.Add("STRUCT_GATE_STONE1B", 132);
		eStructLookUp.Add("STRUCT_GATE_STONE2A", 133);
		eStructLookUp.Add("STRUCT_GATE_STONE2B", 134);
		eStructLookUp.Add("STRUCT_RUINS01", 135);
		eStructLookUp.Add("STRUCT_RUINS02", 136);
		eStructLookUp.Add("STRUCT_RUINS03", 137);
		eStructLookUp.Add("STRUCT_RUINS04", 138);
		eStructLookUp.Add("STRUCT_RUINS05", 139);
		eStructLookUp.Add("STRUCT_RUINS06", 140);
		eStructLookUp.Add("STRUCT_RUINS07", 141);
		eStructLookUp.Add("STRUCT_RUINS08", 142);
		eStructLookUp.Add("STRUCT_RUINS09", 143);
		eStructLookUp.Add("STRUCT_RUINS10", 144);
		eStructLookUp.Add("STRUCT_RUINS11", 145);
		eStructLookUp.Add("STRUCT_RUINS12", 146);
		eStructLookUp.Add("STRUCT_RUINS13", 147);
		eStructLookUp.Add("STRUCT_PEOPLE_ARCHERS", 148);
		eStructLookUp.Add("STRUCT_PEOPLE_SPEARMEN", 149);
		eStructLookUp.Add("STRUCT_PEOPLE_PIKEMEN", 150);
		eStructLookUp.Add("STRUCT_PEOPLE_MACEMEN", 151);
		eStructLookUp.Add("STRUCT_PEOPLE_XBOWMEN", 152);
		eStructLookUp.Add("STRUCT_PEOPLE_SWORDSMEN", 153);
		eStructLookUp.Add("STRUCT_PEOPLE_KNIGHTS", 154);
		eStructLookUp.Add("STRUCT_PEOPLE_LADDERMEN", 155);
		eStructLookUp.Add("STRUCT_PEOPLE_ENGINEERS", 156);
		eStructLookUp.Add("STRUCT_PEOPLE_ENGINEERS_POTS", 157);
		eStructLookUp.Add("STRUCT_PEOPLE_MONKS", 158);
		eStructLookUp.Add("STRUCT_PEOPLE_CATAPULTS", 159);
		eStructLookUp.Add("STRUCT_PEOPLE_TREBUCHETS", 160);
		eStructLookUp.Add("STRUCT_PEOPLE_BATTERING_RAMS", 161);
		eStructLookUp.Add("STRUCT_PEOPLE_SIEGE_TOWERS", 162);
		eStructLookUp.Add("STRUCT_PEOPLE_PORTABLE_SHIELDS", 163);
		eStructLookUp.Add("STRUCT_PEOPLE_TUNNELERS", 164);
		eStructLookUp.Add("STRUCT_MARKER_POINT1", 170);
		eStructLookUp.Add("STRUCT_MARKER_POINT2", 171);
		eStructLookUp.Add("STRUCT_MARKER_POINT3", 172);
		eStructLookUp.Add("STRUCT_MARKER_POINT4", 173);
		eStructLookUp.Add("STRUCT_MARKER_POINT5", 174);
		eStructLookUp.Add("STRUCT_MARKER_POINT6", 175);
		eStructLookUp.Add("STRUCT_MARKER_POINT7", 176);
		eStructLookUp.Add("STRUCT_MARKER_POINT8", 177);
		eStructLookUp.Add("STRUCT_MARKER_POINT9", 178);
		eStructLookUp.Add("STRUCT_MARKER_POINT10", 179);
	}

	private void Setup_eStructToMapperDictionary()
	{
		eStructToMapperLookUp = new Dictionary<string, int>();
		eStructToMapperLookUp.Add("STRUCT_NULL", 0);
		eStructToMapperLookUp.Add("STRUCT_HOVEL", 54);
		eStructToMapperLookUp.Add("STRUCT_HOUSE", 53);
		eStructToMapperLookUp.Add("STRUCT_WOODCUTTERS_HUT", 51);
		eStructToMapperLookUp.Add("STRUCT_OXEN_BASE", 55);
		eStructToMapperLookUp.Add("STRUCT_IRON_MINE", 90);
		eStructToMapperLookUp.Add("STRUCT_PITCH_DIGGER", 91);
		eStructToMapperLookUp.Add("STRUCT_HUNTERS_HUT", 78);
		eStructToMapperLookUp.Add("STRUCT_BARRACKS_WOOD", 86);
		eStructToMapperLookUp.Add("STRUCT_BARRACKS_STONE", 87);
		eStructToMapperLookUp.Add("STRUCT_GOODS_YARD", 52);
		eStructToMapperLookUp.Add("STRUCT_ARMOURY", 81);
		eStructToMapperLookUp.Add("STRUCT_FLETCHERS_WORKSHOP", 50);
		eStructToMapperLookUp.Add("STRUCT_BLACKSMITHS_WORKSHOP", 83);
		eStructToMapperLookUp.Add("STRUCT_POLETURNERS_WORKSHOP", 82);
		eStructToMapperLookUp.Add("STRUCT_ARMOURERS_WORKSHOP", 84);
		eStructToMapperLookUp.Add("STRUCT_TANNERS_WORKSHOP", 85);
		eStructToMapperLookUp.Add("STRUCT_BAKERS_WORKSHOP", 75);
		eStructToMapperLookUp.Add("STRUCT_BREWERS_WORKSHOP", 76);
		eStructToMapperLookUp.Add("STRUCT_GRANARY", 80);
		eStructToMapperLookUp.Add("STRUCT_QUARRY", 56);
		eStructToMapperLookUp.Add("STRUCT_QUARRYPILE", 0);
		eStructToMapperLookUp.Add("STRUCT_INN", 92);
		eStructToMapperLookUp.Add("STRUCT_HEALER", 93);
		eStructToMapperLookUp.Add("STRUCT_ENGINEERS_GUILD", 88);
		eStructToMapperLookUp.Add("STRUCT_TUNNELLERS_GUILD", 89);
		eStructToMapperLookUp.Add("STRUCT_TRADEPOST", 77);
		eStructToMapperLookUp.Add("STRUCT_WELL", 330);
		eStructToMapperLookUp.Add("STRUCT_OIL_SMELTER", 180);
		eStructToMapperLookUp.Add("STRUCT_SIEGE_TENT", 0);
		eStructToMapperLookUp.Add("STRUCT_WHEATFARM", 70);
		eStructToMapperLookUp.Add("STRUCT_HOPSFARM", 71);
		eStructToMapperLookUp.Add("STRUCT_APPLEFARM", 72);
		eStructToMapperLookUp.Add("STRUCT_CATTLEFARM", 73);
		eStructToMapperLookUp.Add("STRUCT_MILL", 74);
		eStructToMapperLookUp.Add("STRUCT_STABLES", 65);
		eStructToMapperLookUp.Add("STRUCT_CHURCH1", 95);
		eStructToMapperLookUp.Add("STRUCT_CHURCH2", 96);
		eStructToMapperLookUp.Add("STRUCT_CHURCH3", 97);
		eStructToMapperLookUp.Add("STRUCT_RUINS", 248);
		eStructToMapperLookUp.Add("STRUCT_KEEP_ONE", 60);
		eStructToMapperLookUp.Add("STRUCT_KEEP_TWO", 61);
		eStructToMapperLookUp.Add("STRUCT_KEEP_THREE", 62);
		eStructToMapperLookUp.Add("STRUCT_KEEP_FOUR", 63);
		eStructToMapperLookUp.Add("STRUCT_KEEP_FIVE", 64);
		eStructToMapperLookUp.Add("STRUCT_GATE_MAIN", 101);
		eStructToMapperLookUp.Add("STRUCT_GATE_INNER", 102);
		eStructToMapperLookUp.Add("STRUCT_GATE_WOOD", 103);
		eStructToMapperLookUp.Add("STRUCT_GATE_POSTERN", 104);
		eStructToMapperLookUp.Add("STRUCT_DRAWBRIDGE", 105);
		eStructToMapperLookUp.Add("STRUCT_TUNNEL_ENTERANCE", 66);
		eStructToMapperLookUp.Add("STRUCT_PARADEGROUND_OIL", 0);
		eStructToMapperLookUp.Add("STRUCT_SIGNPOST", 59);
		eStructToMapperLookUp.Add("STRUCT_PARADEGROUND_ENG", 0);
		eStructToMapperLookUp.Add("STRUCT_UNUSED_1", 67);
		eStructToMapperLookUp.Add("STRUCT_CAMPGROUND", 58);
		eStructToMapperLookUp.Add("STRUCT_PARADEGROUND_MISS", 0);
		eStructToMapperLookUp.Add("STRUCT_PARADEGROUND_LGT", 0);
		eStructToMapperLookUp.Add("STRUCT_PARADEGROUND_HVY", 311);
		eStructToMapperLookUp.Add("STRUCT_PARADEGROUND_TUN", 311);
		eStructToMapperLookUp.Add("STRUCT_GATEHOUSE", 100);
		eStructToMapperLookUp.Add("STRUCT_TOWER", 28);
		eStructToMapperLookUp.Add("STRUCT_GALLOWS", 176);
		eStructToMapperLookUp.Add("STRUCT_STOCKS", 177);
		eStructToMapperLookUp.Add("STRUCT_WITCH_HOIST", 311);
		eStructToMapperLookUp.Add("STRUCT_MAYPOLE", 175);
		eStructToMapperLookUp.Add("STRUCT_GARDEN", 160);
		eStructToMapperLookUp.Add("STRUCT_KILLING_PIT", 98);
		eStructToMapperLookUp.Add("STRUCT_PITCH_DITCH", 99);
		eStructToMapperLookUp.Add("STRUCT_SIEGE_TOWER", 192);
		eStructToMapperLookUp.Add("STRUCT_UNUSED_2", 68);
		eStructToMapperLookUp.Add("STRUCT_KEEPDOOR_LEFT", 0);
		eStructToMapperLookUp.Add("STRUCT_KEEPDOOR_RIGHT", 0);
		eStructToMapperLookUp.Add("STRUCT_KEEPDOOR", 0);
		eStructToMapperLookUp.Add("STRUCT_TOWER1", 110);
		eStructToMapperLookUp.Add("STRUCT_TOWER2", 111);
		eStructToMapperLookUp.Add("STRUCT_TOWER3", 112);
		eStructToMapperLookUp.Add("STRUCT_TOWER4", 113);
		eStructToMapperLookUp.Add("STRUCT_TOWER5", 114);
		eStructToMapperLookUp.Add("STRUCT_TOWER5_DESTROYED", 119);
		eStructToMapperLookUp.Add("STRUCT_SIEGE_TENT_CATAPULT", 0);
		eStructToMapperLookUp.Add("STRUCT_SIEGE_TENT_TREBUCHET", 0);
		eStructToMapperLookUp.Add("STRUCT_SIEGE_TENT_SIEGE_TOWER", 0);
		eStructToMapperLookUp.Add("STRUCT_SIEGE_TENT_BATTERING_RAM", 0);
		eStructToMapperLookUp.Add("STRUCT_SIEGE_TENT_PORTABLE_SHIELD", 0);
		eStructToMapperLookUp.Add("STRUCT_TUNNEL_CONSTRUCTION", 66);
		eStructToMapperLookUp.Add("STRUCT_TOWER1_DESTROYED", 115);
		eStructToMapperLookUp.Add("STRUCT_TOWER2_DESTROYED", 116);
		eStructToMapperLookUp.Add("STRUCT_TOWER3_DESTROYED", 117);
		eStructToMapperLookUp.Add("STRUCT_TOWER4_DESTROYED", 118);
		eStructToMapperLookUp.Add("STRUCT_WAS_WALL", 0);
		eStructToMapperLookUp.Add("STRUCT_CESS_PIT", 301);
		eStructToMapperLookUp.Add("STRUCT_BURNING_STAKE", 305);
		eStructToMapperLookUp.Add("STRUCT_GIBBET", 306);
		eStructToMapperLookUp.Add("STRUCT_DUNGEON", 307);
		eStructToMapperLookUp.Add("STRUCT_RACK_STRETCHING", 308);
		eStructToMapperLookUp.Add("STRUCT_RACK_FLOGGING", 309);
		eStructToMapperLookUp.Add("STRUCT_CHOPPING_BLOCK", 310);
		eStructToMapperLookUp.Add("STRUCT_DUNKING_STOOL", 311);
		eStructToMapperLookUp.Add("STRUCT_DOG_CAGE", 312);
		eStructToMapperLookUp.Add("STRUCT_STATUE", 313);
		eStructToMapperLookUp.Add("STRUCT_SHRINE", 318);
		eStructToMapperLookUp.Add("STRUCT_BEE_HIVE", 323);
		eStructToMapperLookUp.Add("STRUCT_DANCING_BEAR", 324);
		eStructToMapperLookUp.Add("STRUCT_POND", 325);
		eStructToMapperLookUp.Add("STRUCT_BEAR_CAVE", 329);
		eStructToMapperLookUp.Add("STRUCT_UNUSED_3", 69);
		eStructToMapperLookUp.Add("STRUCT_NEW_DELETE", 39);
		eStructToMapperLookUp.Add("STRUCT_NEW_EDITOR_DELETE", 349);
		eStructToMapperLookUp.Add("STRUCT_NEW_DIG_MOAT", 106);
		eStructToMapperLookUp.Add("STRUCT_NEW_FILL_MOAT", 107);
		eStructToMapperLookUp.Add("STRUCT_WOOD_WALL", 46);
		eStructToMapperLookUp.Add("STRUCT_STONE_WALL", 25);
		eStructToMapperLookUp.Add("STRUCT_CRENAL_WALL", 26);
		eStructToMapperLookUp.Add("STRUCT_STAIRS", 27);
		eStructToMapperLookUp.Add("STRUCT_BRAZIER", 148);
		eStructToMapperLookUp.Add("STRUCT_MANGONEL", 210);
		eStructToMapperLookUp.Add("STRUCT_BALLISTA", 211);
		eStructToMapperLookUp.Add("STRUCT_HEAD_ON_SPIKE", 129);
		eStructToMapperLookUp.Add("STRUCT_GARDEN_SMALL", 160);
		eStructToMapperLookUp.Add("STRUCT_GARDEN_MED", 166);
		eStructToMapperLookUp.Add("STRUCT_GARDEN_LARGE", 169);
		eStructToMapperLookUp.Add("STRUCT_POND_SMALL", 325);
		eStructToMapperLookUp.Add("STRUCT_POND_LARGE", 327);
		eStructToMapperLookUp.Add("STRUCT_FLAG1", 120);
		eStructToMapperLookUp.Add("STRUCT_FLAG2", 121);
		eStructToMapperLookUp.Add("STRUCT_FLAG3", 122);
		eStructToMapperLookUp.Add("STRUCT_FLAG4", 123);
		eStructToMapperLookUp.Add("STRUCT_GATE_WOOD1A", 140);
		eStructToMapperLookUp.Add("STRUCT_GATE_WOOD1B", 141);
		eStructToMapperLookUp.Add("STRUCT_GATE_WOOD1C", 142);
		eStructToMapperLookUp.Add("STRUCT_GATE_WOOD1D", 143);
		eStructToMapperLookUp.Add("STRUCT_GATE_STONE1A", 144);
		eStructToMapperLookUp.Add("STRUCT_GATE_STONE1B", 145);
		eStructToMapperLookUp.Add("STRUCT_GATE_STONE2A", 146);
		eStructToMapperLookUp.Add("STRUCT_GATE_STONE2B", 147);
		eStructToMapperLookUp.Add("STRUCT_RUINS01", 248);
		eStructToMapperLookUp.Add("STRUCT_RUINS02", 249);
		eStructToMapperLookUp.Add("STRUCT_RUINS03", 250);
		eStructToMapperLookUp.Add("STRUCT_RUINS04", 251);
		eStructToMapperLookUp.Add("STRUCT_RUINS05", 252);
		eStructToMapperLookUp.Add("STRUCT_RUINS06", 253);
		eStructToMapperLookUp.Add("STRUCT_RUINS07", 254);
		eStructToMapperLookUp.Add("STRUCT_RUINS08", 255);
		eStructToMapperLookUp.Add("STRUCT_RUINS09", 256);
		eStructToMapperLookUp.Add("STRUCT_RUINS10", 257);
		eStructToMapperLookUp.Add("STRUCT_RUINS11", 258);
		eStructToMapperLookUp.Add("STRUCT_RUINS12", 259);
		eStructToMapperLookUp.Add("STRUCT_RUINS13", 260);
		eStructToMapperLookUp.Add("STRUCT_PEOPLE_ARCHERS", 270);
		eStructToMapperLookUp.Add("STRUCT_PEOPLE_SPEARMEN", 271);
		eStructToMapperLookUp.Add("STRUCT_PEOPLE_PIKEMEN", 272);
		eStructToMapperLookUp.Add("STRUCT_PEOPLE_MACEMEN", 273);
		eStructToMapperLookUp.Add("STRUCT_PEOPLE_XBOWMEN", 274);
		eStructToMapperLookUp.Add("STRUCT_PEOPLE_SWORDSMEN", 275);
		eStructToMapperLookUp.Add("STRUCT_PEOPLE_KNIGHTS", 276);
		eStructToMapperLookUp.Add("STRUCT_PEOPLE_LADDERMEN", 277);
		eStructToMapperLookUp.Add("STRUCT_PEOPLE_ENGINEERS", 278);
		eStructToMapperLookUp.Add("STRUCT_PEOPLE_ENGINEERS_POTS", 279);
		eStructToMapperLookUp.Add("STRUCT_PEOPLE_MONKS", 280);
		eStructToMapperLookUp.Add("STRUCT_PEOPLE_CATAPULTS", 281);
		eStructToMapperLookUp.Add("STRUCT_PEOPLE_TREBUCHETS", 282);
		eStructToMapperLookUp.Add("STRUCT_PEOPLE_BATTERING_RAMS", 283);
		eStructToMapperLookUp.Add("STRUCT_PEOPLE_SIEGE_TOWERS", 284);
		eStructToMapperLookUp.Add("STRUCT_PEOPLE_PORTABLE_SHIELDS", 285);
		eStructToMapperLookUp.Add("STRUCT_PEOPLE_TUNNELERS", 286);
		eStructToMapperLookUp.Add("STRUCT_MARKER_POINT1", 350);
		eStructToMapperLookUp.Add("STRUCT_MARKER_POINT2", 351);
		eStructToMapperLookUp.Add("STRUCT_MARKER_POINT3", 352);
		eStructToMapperLookUp.Add("STRUCT_MARKER_POINT4", 353);
		eStructToMapperLookUp.Add("STRUCT_MARKER_POINT5", 354);
		eStructToMapperLookUp.Add("STRUCT_MARKER_POINT6", 355);
		eStructToMapperLookUp.Add("STRUCT_MARKER_POINT7", 356);
		eStructToMapperLookUp.Add("STRUCT_MARKER_POINT8", 357);
		eStructToMapperLookUp.Add("STRUCT_MARKER_POINT9", 358);
		eStructToMapperLookUp.Add("STRUCT_MARKER_POINT10", 359);
		eStructToMapperLookUp.Add("STRUCT_SUB_MENU_TOWERS", 340);
		eStructToMapperLookUp.Add("STRUCT_SUB_MENU_MILITARY", 341);
		eStructToMapperLookUp.Add("STRUCT_SUB_MENU_GATEHOUSES", 342);
		eStructToMapperLookUp.Add("STRUCT_SUB_MENU_KEEPS", 343);
		eStructToMapperLookUp.Add("STRUCT_SUB_MENU_GATEHOUSES_WOOD", 344);
		eStructToMapperLookUp.Add("STRUCT_SUB_MENU_GATEHOUSES_STONESMALL", 345);
		eStructToMapperLookUp.Add("STRUCT_SUB_MENU_GATEHOUSES_STONELARGE", 346);
		eStructToMapperLookUp.Add("STRUCT_SUB_MENU_GOOD", 347);
		eStructToMapperLookUp.Add("STRUCT_SUB_MENU_BAD", 348);
		eStructToMapperLookUp.Add("STRUCT_MENU_RETURN_TOWERS", 360);
		eStructToMapperLookUp.Add("STRUCT_MENU_RETURN_GATEHOUSES", 361);
		eStructToMapperLookUp.Add("STRUCT_MENU_RETURN_MILITARY", 362);
		eStructToMapperLookUp.Add("STRUCT_MENU_RETURN_KEEPS", 363);
		eStructToMapperLookUp.Add("STRUCT_MENU_RETURN_GOOD", 364);
		eStructToMapperLookUp.Add("STRUCT_MENU_RETURN_BAD", 365);
	}

	private void Setup_eMapperDictionary()
	{
		eMapperLookUp = new Dictionary<string, int>();
		eMapperLookUp.Add("MAPPER_NULL", 0);
		eMapperLookUp.Add("MAPPER_AREA", 1);
		eMapperLookUp.Add("MAPPER_RAISE", 2);
		eMapperLookUp.Add("MAPPER_LOWER", 3);
		eMapperLookUp.Add("MAPPER_SEA", 4);
		eMapperLookUp.Add("MAPPER_LAND", 5);
		eMapperLookUp.Add("MAPPER_FOREST", 6);
		eMapperLookUp.Add("MAPPER_SCRUB", 7);
		eMapperLookUp.Add("MAPPER_BEACH", 8);
		eMapperLookUp.Add("MAPPER_SHALLOWS", 9);
		eMapperLookUp.Add("MAPPER_ROCKY", 10);
		eMapperLookUp.Add("MAPPER_STONES", 11);
		eMapperLookUp.Add("MAPPER_BOULDERS", 12);
		eMapperLookUp.Add("MAPPER_PEBBLES", 13);
		eMapperLookUp.Add("MAPPER_RIVER", 14);
		eMapperLookUp.Add("MAPPER_FORD", 15);
		eMapperLookUp.Add("MAPPER_IRON", 16);
		eMapperLookUp.Add("MAPPER_MARSH", 17);
		eMapperLookUp.Add("MAPPER_DIRT", 18);
		eMapperLookUp.Add("MAPPER_GRASS", 19);
		eMapperLookUp.Add("MAPPER_BIGROCKS", 20);
		eMapperLookUp.Add("MAPPER_MIN", 21);
		eMapperLookUp.Add("MAPPER_MAX", 22);
		eMapperLookUp.Add("MAPPER_EQUALISE", 23);
		eMapperLookUp.Add("MAPPER_PLATEAU", 24);
		eMapperLookUp.Add("MAPPER_WALL", 25);
		eMapperLookUp.Add("MAPPER_CRENAL", 26);
		eMapperLookUp.Add("MAPPER_STAIR", 27);
		eMapperLookUp.Add("MAPPER_TOWER", 28);
		eMapperLookUp.Add("MAPPER_UP", 29);
		eMapperLookUp.Add("MAPPER_DOWN", 20);
		eMapperLookUp.Add("MAPPER_EXIT", 31);
		eMapperLookUp.Add("MAPPER_TOMAIN", 32);
		eMapperLookUp.Add("MAPPER_TOTEST", 33);
		eMapperLookUp.Add("MAPPER_PATROL", 34);
		eMapperLookUp.Add("MAPPER_PATH_END", 35);
		eMapperLookUp.Add("MAPPER_MOUNTAIN", 36);
		eMapperLookUp.Add("MAPPER_HILL", 37);
		eMapperLookUp.Add("MAPPER_AFFECT_TYPE", 38);
		eMapperLookUp.Add("MAPPER_DELETE", 39);
		eMapperLookUp.Add("MAPPER_DELETE_EDITOR", 349);
		eMapperLookUp.Add("MAPPER_CHESTNUT", 40);
		eMapperLookUp.Add("MAPPER_OAK", 41);
		eMapperLookUp.Add("MAPPER_PINE", 42);
		eMapperLookUp.Add("MAPPER_BIRCH", 43);
		eMapperLookUp.Add("MAPPER_UNDUGMOAT", 44);
		eMapperLookUp.Add("MAPPER_DUGMOAT", 45);
		eMapperLookUp.Add("MAPPER_WOODWALL", 46);
		eMapperLookUp.Add("MAPPER_PLAIN1", 47);
		eMapperLookUp.Add("MAPPER_PLAIN2", 48);
		eMapperLookUp.Add("MAPPER_OIL", 49);
		eMapperLookUp.Add("MAPPER_FLETCHER", 50);
		eMapperLookUp.Add("MAPPER_WOODSMAN", 51);
		eMapperLookUp.Add("MAPPER_STORES", 52);
		eMapperLookUp.Add("MAPPER_HOUSE", 53);
		eMapperLookUp.Add("MAPPER_HOVEL", 54);
		eMapperLookUp.Add("MAPPER_OXENBASE", 55);
		eMapperLookUp.Add("MAPPER_QUARRY", 56);
		eMapperLookUp.Add("MAPPER_TUNNEL", 57);
		eMapperLookUp.Add("MAPPER_CAMP_FIRE", 58);
		eMapperLookUp.Add("MAPPER_SIGNPOST", 59);
		eMapperLookUp.Add("MAPPER_KEEP1", 60);
		eMapperLookUp.Add("MAPPER_KEEP2", 61);
		eMapperLookUp.Add("MAPPER_KEEP3", 62);
		eMapperLookUp.Add("MAPPER_KEEP4", 63);
		eMapperLookUp.Add("MAPPER_KEEP5", 64);
		eMapperLookUp.Add("MAPPER_STABLES", 65);
		eMapperLookUp.Add("MAPPER_TUNNEL_CONSTRUCTION", 66);
		eMapperLookUp.Add("MAPPER_UNUSED_1", 67);
		eMapperLookUp.Add("MAPPER_UNUSED_2", 68);
		eMapperLookUp.Add("MAPPER_UNUSED_3", 69);
		eMapperLookUp.Add("MAPPER_WHEATFARM", 70);
		eMapperLookUp.Add("MAPPER_HOPSFARM", 71);
		eMapperLookUp.Add("MAPPER_APPLEFARM", 72);
		eMapperLookUp.Add("MAPPER_CATTLEFARM", 73);
		eMapperLookUp.Add("MAPPER_MILL", 74);
		eMapperLookUp.Add("MAPPER_BAKER", 75);
		eMapperLookUp.Add("MAPPER_BREWER", 76);
		eMapperLookUp.Add("MAPPER_TRADEPOST", 77);
		eMapperLookUp.Add("MAPPER_HUNTER", 78);
		eMapperLookUp.Add("MAPPER_UNUSED_4", 79);
		eMapperLookUp.Add("MAPPER_GRANARY", 80);
		eMapperLookUp.Add("MAPPER_ARMOURY", 81);
		eMapperLookUp.Add("MAPPER_POLETURNER", 82);
		eMapperLookUp.Add("MAPPER_BLACKSMITH", 83);
		eMapperLookUp.Add("MAPPER_ARMOURER", 84);
		eMapperLookUp.Add("MAPPER_TANNER", 85);
		eMapperLookUp.Add("MAPPER_BARRACKS_WOOD", 86);
		eMapperLookUp.Add("MAPPER_BARRACKS_STONE", 87);
		eMapperLookUp.Add("MAPPER_ENGINEERS_GUILD", 88);
		eMapperLookUp.Add("MAPPER_TUNNELERS_GUILD", 89);
		eMapperLookUp.Add("MAPPER_IRON_MINE", 90);
		eMapperLookUp.Add("MAPPER_PITCH_WORKINGS", 91);
		eMapperLookUp.Add("MAPPER_INN", 92);
		eMapperLookUp.Add("MAPPER_HEALER", 93);
		eMapperLookUp.Add("MAPPER_SIEGE_TOWER_BASE", 94);
		eMapperLookUp.Add("MAPPER_CHURCH1", 95);
		eMapperLookUp.Add("MAPPER_CHURCH2", 96);
		eMapperLookUp.Add("MAPPER_CHURCH3", 97);
		eMapperLookUp.Add("MAPPER_KILLING_PIT", 98);
		eMapperLookUp.Add("MAPPER_PITCH_DITCH", 99);
		eMapperLookUp.Add("MAPPER_GATEHOUSE", 100);
		eMapperLookUp.Add("MAPPER_GATE_MAIN", 101);
		eMapperLookUp.Add("MAPPER_GATE_INNER", 102);
		eMapperLookUp.Add("MAPPER_GATE_WOOD", 103);
		eMapperLookUp.Add("MAPPER_GATE_POSTERN", 104);
		eMapperLookUp.Add("MAPPER_DRAWBRIDGE", 105);
		eMapperLookUp.Add("MAPPER_MOAT", 106);
		eMapperLookUp.Add("MAPPER_ANTIMOAT", 107);
		eMapperLookUp.Add("MAPPER_UNUSED_5", 108);
		eMapperLookUp.Add("MAPPER_UNUSED_6", 109);
		eMapperLookUp.Add("MAPPER_TOWER1", 110);
		eMapperLookUp.Add("MAPPER_TOWER2", 111);
		eMapperLookUp.Add("MAPPER_TOWER3", 112);
		eMapperLookUp.Add("MAPPER_TOWER4", 113);
		eMapperLookUp.Add("MAPPER_TOWER5", 114);
		eMapperLookUp.Add("MAPPER_TOWER1_DESTROYED", 115);
		eMapperLookUp.Add("MAPPER_TOWER2_DESTROYED", 116);
		eMapperLookUp.Add("MAPPER_TOWER3_DESTROYED", 117);
		eMapperLookUp.Add("MAPPER_TOWER4_DESTROYED", 118);
		eMapperLookUp.Add("MAPPER_TOWER5_DESTROYED", 119);
		eMapperLookUp.Add("MAPPER_FLAG_TYPE0", 120);
		eMapperLookUp.Add("MAPPER_FLAG_TYPE1", 121);
		eMapperLookUp.Add("MAPPER_FLAG_TYPE2", 122);
		eMapperLookUp.Add("MAPPER_FLAG_TYPE3", 123);
		eMapperLookUp.Add("MAPPER_FLAG_TYPE4", 124);
		eMapperLookUp.Add("MAPPER_FLAG_TYPE5", 125);
		eMapperLookUp.Add("MAPPER_FLAG_TYPE6", 126);
		eMapperLookUp.Add("MAPPER_FLAG_TYPE7", 127);
		eMapperLookUp.Add("MAPPER_FLAG_TYPE8", 128);
		eMapperLookUp.Add("MAPPER_HEADS", 129);
		eMapperLookUp.Add("MAPPER_SHRUB1A", 130);
		eMapperLookUp.Add("MAPPER_SHRUB1B", 131);
		eMapperLookUp.Add("MAPPER_SHRUB1C", 132);
		eMapperLookUp.Add("MAPPER_SHRUB1D", 133);
		eMapperLookUp.Add("MAPPER_SHRUB1E", 134);
		eMapperLookUp.Add("MAPPER_SHRUB2A", 135);
		eMapperLookUp.Add("MAPPER_SHRUB2B", 136);
		eMapperLookUp.Add("MAPPER_SHRUB2C", 137);
		eMapperLookUp.Add("MAPPER_SHRUB2D", 138);
		eMapperLookUp.Add("MAPPER_SHRUB2E", 139);
		eMapperLookUp.Add("MAPPER_GATE_WOOD1A", 140);
		eMapperLookUp.Add("MAPPER_GATE_WOOD1B", 141);
		eMapperLookUp.Add("MAPPER_GATE_WOOD1C", 142);
		eMapperLookUp.Add("MAPPER_GATE_WOOD1D", 143);
		eMapperLookUp.Add("MAPPER_GATE_STONE1A", 144);
		eMapperLookUp.Add("MAPPER_GATE_STONE1B", 145);
		eMapperLookUp.Add("MAPPER_GATE_STONE2A", 146);
		eMapperLookUp.Add("MAPPER_GATE_STONE2B", 147);
		eMapperLookUp.Add("MAPPER_BRAZIER", 148);
		eMapperLookUp.Add("MAPPER_UNUSED_7", 149);
		eMapperLookUp.Add("MAPPER_FOAM", 150);
		eMapperLookUp.Add("MAPPER_RIPPLE", 151);
		eMapperLookUp.Add("MAPPER_TO_MAP_EDIT", 152);
		eMapperLookUp.Add("MAPPER_UNUSED_8", 153);
		eMapperLookUp.Add("MAPPER_UNUSED_9", 154);
		eMapperLookUp.Add("MAPPER_UNUSED_10", 155);
		eMapperLookUp.Add("MAPPER_UNUSED_11", 156);
		eMapperLookUp.Add("MAPPER_UNUSED_12", 157);
		eMapperLookUp.Add("MAPPER_UNUSED_13", 158);
		eMapperLookUp.Add("MAPPER_UNUSED_14", 159);
		eMapperLookUp.Add("MAPPER_GARDEN1", 160);
		eMapperLookUp.Add("MAPPER_GARDEN2", 161);
		eMapperLookUp.Add("MAPPER_GARDEN3", 162);
		eMapperLookUp.Add("MAPPER_GARDEN4", 163);
		eMapperLookUp.Add("MAPPER_GARDEN5", 164);
		eMapperLookUp.Add("MAPPER_GARDEN6", 165);
		eMapperLookUp.Add("MAPPER_GARDEN7", 166);
		eMapperLookUp.Add("MAPPER_GARDEN8", 167);
		eMapperLookUp.Add("MAPPER_GARDEN9", 168);
		eMapperLookUp.Add("MAPPER_GARDEN10", 169);
		eMapperLookUp.Add("MAPPER_GARDEN11", 170);
		eMapperLookUp.Add("MAPPER_GARDEN12", 171);
		eMapperLookUp.Add("MAPPER_UNUSED_15", 172);
		eMapperLookUp.Add("MAPPER_UNUSED_16", 173);
		eMapperLookUp.Add("MAPPER_UNUSED_17", 174);
		eMapperLookUp.Add("MAPPER_MAYPOLE", 175);
		eMapperLookUp.Add("MAPPER_GALLOWS", 176);
		eMapperLookUp.Add("MAPPER_STOCKS", 177);
		eMapperLookUp.Add("MAPPER_UNUSED_18", 178);
		eMapperLookUp.Add("MAPPER_UNUSED_19", 179);
		eMapperLookUp.Add("MAPPER_OIL_SMELTER", 180);
		eMapperLookUp.Add("MAPPER_UNUSED_20", 181);
		eMapperLookUp.Add("MAPPER_UNUSED_21", 182);
		eMapperLookUp.Add("MAPPER_UNUSED_22", 183);
		eMapperLookUp.Add("MAPPER_UNUSED_23", 184);
		eMapperLookUp.Add("MAPPER_UNUSED_24", 185);
		eMapperLookUp.Add("MAPPER_UNUSED_25", 186);
		eMapperLookUp.Add("MAPPER_UNUSED_26", 187);
		eMapperLookUp.Add("MAPPER_UNUSED_27", 188);
		eMapperLookUp.Add("MAPPER_UNUSED_28", 189);
		eMapperLookUp.Add("MAPPER_CATAPULT", 190);
		eMapperLookUp.Add("MAPPER_TREBUCHET", 191);
		eMapperLookUp.Add("MAPPER_SIEGE_TOWER", 192);
		eMapperLookUp.Add("MAPPER_BATTERING_RAM", 193);
		eMapperLookUp.Add("MAPPER_PORTABLE_SHIELD", 194);
		eMapperLookUp.Add("MAPPER_UNUSED_29", 195);
		eMapperLookUp.Add("MAPPER_UNUSED_30", 196);
		eMapperLookUp.Add("MAPPER_UNUSED_31", 197);
		eMapperLookUp.Add("MAPPER_UNUSED_32", 198);
		eMapperLookUp.Add("MAPPER_UNUSED_33", 199);
		eMapperLookUp.Add("MAPPER_BACK", 200);
		eMapperLookUp.Add("MAPPER_CHECK_BOX", 201);
		eMapperLookUp.Add("MAPPER_TEST", 202);
		eMapperLookUp.Add("MAPPER_REBUILD", 203);
		eMapperLookUp.Add("MAPPER_SNAP_TO", 204);
		eMapperLookUp.Add("MAPPER_BIGROCK1", 205);
		eMapperLookUp.Add("MAPPER_BIGROCK2", 206);
		eMapperLookUp.Add("MAPPER_BIGROCK3", 207);
		eMapperLookUp.Add("MAPPER_BIGROCK4", 208);
		eMapperLookUp.Add("MAPPER_BIGROCK5", 209);
		eMapperLookUp.Add("MAPPER_MANGONEL", 210);
		eMapperLookUp.Add("MAPPER_BALLISTA", 211);
		eMapperLookUp.Add("MAPPER_UNUSED_34", 212);
		eMapperLookUp.Add("MAPPER_UNUSED_35", 213);
		eMapperLookUp.Add("MAPPER_UNUSED_36", 214);
		eMapperLookUp.Add("MAPPER_UNUSED_37", 215);
		eMapperLookUp.Add("MAPPER_UNUSED_38", 216);
		eMapperLookUp.Add("MAPPER_UNUSED_39", 217);
		eMapperLookUp.Add("MAPPER_UNUSED_40", 218);
		eMapperLookUp.Add("MAPPER_UNUSED_41", 219);
		eMapperLookUp.Add("MAPPER_DEER", 220);
		eMapperLookUp.Add("MAPPER_WOLF", 221);
		eMapperLookUp.Add("MAPPER_RABBIT", 222);
		eMapperLookUp.Add("MAPPER_BEAR", 223);
		eMapperLookUp.Add("MAPPER_CROW", 224);
		eMapperLookUp.Add("MAPPER_SEAGULL", 225);
		eMapperLookUp.Add("MAPPER_UNUSED_42", 226);
		eMapperLookUp.Add("MAPPER_UNUSED_44", 227);
		eMapperLookUp.Add("MAPPER_UNUSED_45", 228);
		eMapperLookUp.Add("MAPPER_UNUSED_46", 229);
		eMapperLookUp.Add("MAPPER_MAP_SIZE", 230);
		eMapperLookUp.Add("MAPPER_SUB_MODE_HEIGHT", 231);
		eMapperLookUp.Add("MAPPER_SUB_MODE_TYPE", 232);
		eMapperLookUp.Add("MAPPER_SUB_MODE_OBJ", 234);
		eMapperLookUp.Add("MAPPER_SUB_MODE_ANIMAL", 235);
		eMapperLookUp.Add("MAPPER_SUB_MODE_WATER", 236);
		eMapperLookUp.Add("MAPPER_SUB_MODE_FEATURE", 237);
		eMapperLookUp.Add("MAPPER_ESTUARY", 238);
		eMapperLookUp.Add("MAPPER_SUB_MODE_FEATURE_MP", 239);
		eMapperLookUp.Add("MAPPER_REPORT1", 240);
		eMapperLookUp.Add("MAPPER_REPORT2", 241);
		eMapperLookUp.Add("MAPPER_REPORT3", 242);
		eMapperLookUp.Add("MAPPER_REPORT4", 243);
		eMapperLookUp.Add("MAPPER_REPORT5", 244);
		eMapperLookUp.Add("MAPPER_REPORT6", 245);
		eMapperLookUp.Add("MAPPER_REPORT7", 246);
		eMapperLookUp.Add("MAPPER_REPORT8", 247);
		eMapperLookUp.Add("MAPPER_MP_KEEP1", 240);
		eMapperLookUp.Add("MAPPER_MP_KEEP2", 241);
		eMapperLookUp.Add("MAPPER_MP_KEEP3", 242);
		eMapperLookUp.Add("MAPPER_MP_KEEP4", 243);
		eMapperLookUp.Add("MAPPER_MP_KEEP5", 244);
		eMapperLookUp.Add("MAPPER_MP_KEEP6", 245);
		eMapperLookUp.Add("MAPPER_MP_KEEP7", 246);
		eMapperLookUp.Add("MAPPER_MP_KEEP8", 247);
		eMapperLookUp.Add("MAPPER_RUINS1", 248);
		eMapperLookUp.Add("MAPPER_RUINS2", 249);
		eMapperLookUp.Add("MAPPER_RUINS3", 250);
		eMapperLookUp.Add("MAPPER_RUINS4", 251);
		eMapperLookUp.Add("MAPPER_RUINS5", 252);
		eMapperLookUp.Add("MAPPER_RUINS6", 253);
		eMapperLookUp.Add("MAPPER_RUINS7", 254);
		eMapperLookUp.Add("MAPPER_RUINS8", 255);
		eMapperLookUp.Add("MAPPER_RUINS9", 256);
		eMapperLookUp.Add("MAPPER_RUINS10", 257);
		eMapperLookUp.Add("MAPPER_RUINS11", 258);
		eMapperLookUp.Add("MAPPER_RUINS12", 259);
		eMapperLookUp.Add("MAPPER_RUINS13", 260);
		eMapperLookUp.Add("MAPPER_UNUSED_48", 261);
		eMapperLookUp.Add("MAPPER_UNUSED_49", 262);
		eMapperLookUp.Add("MAPPER_UNUSED_50", 263);
		eMapperLookUp.Add("MAPPER_UNUSED_51", 264);
		eMapperLookUp.Add("MAPPER_UNUSED_52", 265);
		eMapperLookUp.Add("MAPPER_UNUSED_53", 266);
		eMapperLookUp.Add("MAPPER_UNUSED_54", 267);
		eMapperLookUp.Add("MAPPER_UNUSED_55", 268);
		eMapperLookUp.Add("MAPPER_UNUSED_56", 269);
		eMapperLookUp.Add("MAPPER_PEOPLE_ARCHERS", 270);
		eMapperLookUp.Add("MAPPER_PEOPLE_SPEARMEN", 271);
		eMapperLookUp.Add("MAPPER_PEOPLE_PIKEMEN", 272);
		eMapperLookUp.Add("MAPPER_PEOPLE_MACEMEN", 273);
		eMapperLookUp.Add("MAPPER_PEOPLE_XBOWMEN", 274);
		eMapperLookUp.Add("MAPPER_PEOPLE_SWORDSMEN", 275);
		eMapperLookUp.Add("MAPPER_PEOPLE_KNIGHTS", 276);
		eMapperLookUp.Add("MAPPER_PEOPLE_LADDERMEN", 277);
		eMapperLookUp.Add("MAPPER_PEOPLE_ENGINEERS", 278);
		eMapperLookUp.Add("MAPPER_PEOPLE_ENGINEERS_POTS", 279);
		eMapperLookUp.Add("MAPPER_PEOPLE_MONKS", 280);
		eMapperLookUp.Add("MAPPER_PEOPLE_CATAPULTS", 281);
		eMapperLookUp.Add("MAPPER_PEOPLE_TREBUCHETS", 282);
		eMapperLookUp.Add("MAPPER_PEOPLE_BATTERING_RAMS", 283);
		eMapperLookUp.Add("MAPPER_PEOPLE_SIEGE_TOWERS", 284);
		eMapperLookUp.Add("MAPPER_PEOPLE_PORTABLE_SHIELDS", 285);
		eMapperLookUp.Add("MAPPER_PEOPLE_TUNNELERS", 286);
		eMapperLookUp.Add("MAPPER_STANCE_STAND", 287);
		eMapperLookUp.Add("MAPPER_STANCE_DEFENSIVE", 288);
		eMapperLookUp.Add("MAPPER_STANCE_AGGRESSIVE", 289);
		eMapperLookUp.Add("MAPPER_TROOP_STOP", 290);
		eMapperLookUp.Add("MAPPER_ENGINEER_BUILD", 291);
		eMapperLookUp.Add("MAPPER_BUILD_BACK", 292);
		eMapperLookUp.Add("MAPPER_BUY_AMMO", 293);
		eMapperLookUp.Add("MAPPER_UNUSED_57", 294);
		eMapperLookUp.Add("MAPPER_UNUSED_58", 295);
		eMapperLookUp.Add("MAPPER_UNUSED_59", 296);
		eMapperLookUp.Add("MAPPER_UNUSED_60", 297);
		eMapperLookUp.Add("MAPPER_UNUSED_61", 298);
		eMapperLookUp.Add("MAPPER_UNUSED_62", 299);
		eMapperLookUp.Add("MAPPER_UNUSED_63", 300);
		eMapperLookUp.Add("MAPPER_CESS_PIT1", 301);
		eMapperLookUp.Add("MAPPER_CESS_PIT2", 302);
		eMapperLookUp.Add("MAPPER_CESS_PIT3", 303);
		eMapperLookUp.Add("MAPPER_CESS_PIT4", 304);
		eMapperLookUp.Add("MAPPER_BURNING_STAKE", 305);
		eMapperLookUp.Add("MAPPER_GIBBET", 306);
		eMapperLookUp.Add("MAPPER_DUNGEON", 307);
		eMapperLookUp.Add("MAPPER_RACK_STRETCHING", 308);
		eMapperLookUp.Add("MAPPER_RACK_FLOGGING", 309);
		eMapperLookUp.Add("MAPPER_CHOPPING_BLOCK", 310);
		eMapperLookUp.Add("MAPPER_DUNKING_STOOL", 311);
		eMapperLookUp.Add("MAPPER_DOG_CAGE", 312);
		eMapperLookUp.Add("MAPPER_STATUE1", 313);
		eMapperLookUp.Add("MAPPER_STATUE2", 314);
		eMapperLookUp.Add("MAPPER_STATUE3", 315);
		eMapperLookUp.Add("MAPPER_STATUE4", 316);
		eMapperLookUp.Add("MAPPER_STATUE5", 317);
		eMapperLookUp.Add("MAPPER_SHRINE1", 318);
		eMapperLookUp.Add("MAPPER_SHRINE2", 319);
		eMapperLookUp.Add("MAPPER_SHRINE3", 320);
		eMapperLookUp.Add("MAPPER_SHRINE4", 321);
		eMapperLookUp.Add("MAPPER_SHRINE5", 322);
		eMapperLookUp.Add("MAPPER_BEE_HIVE", 323);
		eMapperLookUp.Add("MAPPER_DANCING_BEAR", 324);
		eMapperLookUp.Add("MAPPER_POND1", 325);
		eMapperLookUp.Add("MAPPER_POND2", 326);
		eMapperLookUp.Add("MAPPER_POND3", 327);
		eMapperLookUp.Add("MAPPER_POND4", 328);
		eMapperLookUp.Add("MAPPER_BEAR_CAVE", 329);
		eMapperLookUp.Add("MAPPER_WELL", 330);
		eMapperLookUp.Add("MAPPER_AREA_BACK", 331);
		eMapperLookUp.Add("MAPPER_MARKER_POINT1", 350);
		eMapperLookUp.Add("MAPPER_MARKER_POINT2", 351);
		eMapperLookUp.Add("MAPPER_MARKER_POINT3", 352);
		eMapperLookUp.Add("MAPPER_MARKER_POINT4", 353);
		eMapperLookUp.Add("MAPPER_MARKER_POINT5", 354);
		eMapperLookUp.Add("MAPPER_MARKER_POINT6", 355);
		eMapperLookUp.Add("MAPPER_MARKER_POINT7", 356);
		eMapperLookUp.Add("MAPPER_MARKER_POINT8", 357);
		eMapperLookUp.Add("MAPPER_MARKER_POINT9", 358);
		eMapperLookUp.Add("MAPPER_MARKER_POINT10", 359);
		eMapperLookUp.Add("MAPPER_PLACE_ASSEMBLY_POINT1", 332);
		eMapperLookUp.Add("MAPPER_PLACE_ASSEMBLY_POINT2", 333);
		eMapperLookUp.Add("MAPPER_PLACE_ASSEMBLY_POINT3", 334);
		eMapperLookUp.Add("MAPPER_PLACE_ASSEMBLY_POINT4", 335);
		eMapperLookUp.Add("MAPPER_PLACE_ASSEMBLY_POINT5", 336);
		eMapperLookUp.Add("MAPPER_PLACE_ASSEMBLY_POINT6", 337);
		eMapperLookUp.Add("MAPPER_PLACE_ASSEMBLY_POINT7", 338);
		eMapperLookUp.Add("MAPPER_PLACE_ASSEMBLY_POINTE1", 367);
		eMapperLookUp.Add("MAPPER_PLACE_ASSEMBLY_POINTE2", 368);
		eMapperLookUp.Add("MAPPER_PLACE_ASSEMBLY_POINTT1", 369);
	}

	private void SetupMapperHelpLookup()
	{
		eMapperHelpLookup = new Dictionary<int, int>();
		eMapperHelpLookup.Add(81, 8);
		eMapperHelpLookup.Add(87, 9);
		eMapperHelpLookup.Add(86, 10);
		eMapperHelpLookup.Add(65, 11);
		eMapperHelpLookup.Add(148, 16);
		eMapperHelpLookup.Add(98, 17);
		eMapperHelpLookup.Add(110, 21);
		eMapperHelpLookup.Add(111, 22);
		eMapperHelpLookup.Add(112, 23);
		eMapperHelpLookup.Add(113, 24);
		eMapperHelpLookup.Add(114, 25);
		eMapperHelpLookup.Add(104, 27);
		eMapperHelpLookup.Add(103, 28);
		eMapperHelpLookup.Add(102, 29);
		eMapperHelpLookup.Add(105, 30);
		eMapperHelpLookup.Add(106, 31);
		eMapperHelpLookup.Add(60, 32);
		eMapperHelpLookup.Add(61, 33);
		eMapperHelpLookup.Add(62, 34);
		eMapperHelpLookup.Add(63, 35);
		eMapperHelpLookup.Add(64, 36);
		eMapperHelpLookup.Add(54, 39);
		eMapperHelpLookup.Add(53, 40);
		eMapperHelpLookup.Add(56, 41);
		eMapperHelpLookup.Add(51, 42);
		eMapperHelpLookup.Add(90, 43);
		eMapperHelpLookup.Add(91, 44);
		eMapperHelpLookup.Add(52, 45);
		eMapperHelpLookup.Add(80, 46);
		eMapperHelpLookup.Add(330, 47);
		eMapperHelpLookup.Add(74, 48);
		eMapperHelpLookup.Add(77, 49);
		eMapperHelpLookup.Add(55, 50);
		eMapperHelpLookup.Add(83, 51);
		eMapperHelpLookup.Add(84, 52);
		eMapperHelpLookup.Add(85, 53);
		eMapperHelpLookup.Add(50, 54);
		eMapperHelpLookup.Add(82, 55);
		eMapperHelpLookup.Add(75, 56);
		eMapperHelpLookup.Add(76, 57);
		eMapperHelpLookup.Add(70, 58);
		eMapperHelpLookup.Add(72, 59);
		eMapperHelpLookup.Add(71, 60);
		eMapperHelpLookup.Add(73, 61);
		eMapperHelpLookup.Add(78, 62);
		eMapperHelpLookup.Add(92, 64);
		eMapperHelpLookup.Add(93, 65);
		eMapperHelpLookup.Add(88, 66);
		eMapperHelpLookup.Add(89, 67);
		eMapperHelpLookup.Add(176, 71);
		eMapperHelpLookup.Add(175, 72);
		eMapperHelpLookup.Add(177, 75);
		eMapperHelpLookup.Add(101, 91);
		eMapperHelpLookup.Add(107, 92);
		eMapperHelpLookup.Add(120, 95);
		eMapperHelpLookup.Add(122, 96);
		eMapperHelpLookup.Add(123, 97);
		eMapperHelpLookup.Add(121, 100);
		eMapperHelpLookup.Add(95, 101);
		eMapperHelpLookup.Add(96, 102);
		eMapperHelpLookup.Add(97, 103);
		eMapperHelpLookup.Add(160, 108);
		eMapperHelpLookup.Add(161, 108);
		eMapperHelpLookup.Add(162, 108);
		eMapperHelpLookup.Add(163, 108);
		eMapperHelpLookup.Add(164, 108);
		eMapperHelpLookup.Add(165, 104);
		eMapperHelpLookup.Add(166, 108);
		eMapperHelpLookup.Add(167, 108);
		eMapperHelpLookup.Add(168, 108);
		eMapperHelpLookup.Add(169, 106);
		eMapperHelpLookup.Add(170, 106);
		eMapperHelpLookup.Add(171, 106);
		eMapperHelpLookup.Add(210, 133);
		eMapperHelpLookup.Add(191, 135);
		eMapperHelpLookup.Add(192, 136);
		eMapperHelpLookup.Add(193, 137);
		eMapperHelpLookup.Add(194, 138);
		eMapperHelpLookup.Add(180, 139);
		eMapperHelpLookup.Add(211, 197);
		eMapperHelpLookup.Add(99, 204);
		eMapperHelpLookup.Add(270, 244);
		eMapperHelpLookup.Add(271, 245);
		eMapperHelpLookup.Add(272, 246);
		eMapperHelpLookup.Add(273, 247);
		eMapperHelpLookup.Add(274, 248);
		eMapperHelpLookup.Add(275, 249);
		eMapperHelpLookup.Add(276, 250);
		eMapperHelpLookup.Add(277, 251);
		eMapperHelpLookup.Add(278, 252);
		eMapperHelpLookup.Add(279, 253);
		eMapperHelpLookup.Add(280, 254);
		eMapperHelpLookup.Add(190, 134);
		eMapperHelpLookup.Add(286, 255);
		eMapperHelpLookup.Add(301, 271);
		eMapperHelpLookup.Add(305, 272);
		eMapperHelpLookup.Add(306, 273);
		eMapperHelpLookup.Add(307, 274);
		eMapperHelpLookup.Add(308, 275);
		eMapperHelpLookup.Add(309, 276);
		eMapperHelpLookup.Add(310, 277);
		eMapperHelpLookup.Add(311, 278);
		eMapperHelpLookup.Add(312, 279);
		eMapperHelpLookup.Add(313, 280);
		eMapperHelpLookup.Add(318, 281);
		eMapperHelpLookup.Add(323, 282);
		eMapperHelpLookup.Add(324, 283);
		eMapperHelpLookup.Add(325, 284);
		eMapperHelpLookup.Add(329, 285);
		eMapperHelpLookup.Add(314, 280);
		eMapperHelpLookup.Add(315, 280);
		eMapperHelpLookup.Add(316, 280);
		eMapperHelpLookup.Add(317, 280);
		eMapperHelpLookup.Add(319, 281);
		eMapperHelpLookup.Add(320, 281);
		eMapperHelpLookup.Add(321, 281);
		eMapperHelpLookup.Add(322, 281);
		eMapperHelpLookup.Add(302, 271);
		eMapperHelpLookup.Add(303, 271);
		eMapperHelpLookup.Add(304, 271);
		eMapperHelpLookup.Add(326, 284);
		eMapperHelpLookup.Add(327, 300);
		eMapperHelpLookup.Add(328, 300);
	}

	public void SetupEnumTranslation()
	{
		if (!EnumTranslationsSetup)
		{
			Setup_eGoodsDictionary();
			Setup_eChimpDictionary();
			Setup_eStructDictionary();
			Setup_eMapperDictionary();
			Setup_eStructToMapperDictionary();
			SetupMapperHelpLookup();
			EnumTranslationsSetup = true;
		}
	}

	public Enums.Goods getGoodsEnum(string key)
	{
		SetupEnumTranslation();
		if (key == null || key == "0")
		{
			return Enums.Goods.STORED_NULL;
		}
		if (eGoodsLookUp.TryGetValue(key, out var value))
		{
			return (Enums.Goods)value;
		}
		return Enums.Goods.STORED_NULL;
	}

	public Enums.eChimps getChimpEnum(string key)
	{
		SetupEnumTranslation();
		if (key == null || key == "0")
		{
			return Enums.eChimps.CHIMP_TYPE_NULL;
		}
		if (eChimpLookUp.TryGetValue(key, out var value))
		{
			return (Enums.eChimps)value;
		}
		return Enums.eChimps.CHIMP_TYPE_NULL;
	}

	public int getStructEnum(string key)
	{
		SetupEnumTranslation();
		if (key == null || key == "0")
		{
			return 0;
		}
		if (eStructLookUp.TryGetValue(key, out var value))
		{
			return value;
		}
		return 0;
	}

	public int getStructEnumExtra(string key)
	{
		return key switch
		{
			"STRUCT_SUB_MENU_GATEHOUSES_WOOD" => 47, 
			"STRUCT_SUB_MENU_GATEHOUSES_STONESMALL" => 46, 
			"STRUCT_SUB_MENU_GATEHOUSES_STONELARGE" => 45, 
			_ => 0, 
		};
	}

	public Enums.eMappers getMapperEnum(string key)
	{
		SetupEnumTranslation();
		if (key == null || key == "0")
		{
			return Enums.eMappers.MAPPER_NULL;
		}
		if (eMapperLookUp.TryGetValue(key, out var value))
		{
			return (Enums.eMappers)value;
		}
		return Enums.eMappers.MAPPER_NULL;
	}

	public Enums.eMappers getStructToMapperEnum(string key)
	{
		SetupEnumTranslation();
		if (key == null || key == "0")
		{
			return Enums.eMappers.MAPPER_NULL;
		}
		if (eStructToMapperLookUp.TryGetValue(key, out var value))
		{
			return (Enums.eMappers)value;
		}
		return Enums.eMappers.MAPPER_NULL;
	}

	public int getMapperToHelp(int mapper)
	{
		SetupEnumTranslation();
		if (mapper == 0)
		{
			return 0;
		}
		if (eMapperHelpLookup.TryGetValue(mapper, out var value))
		{
			return value;
		}
		return 0;
	}

	public Enums.eUISprites goodsSpriteEnumFromGoodsEnum(Enums.Goods thisGood)
	{
		return thisGood switch
		{
			Enums.Goods.STORED_WOOD_PLANKS => Enums.eUISprites.SPRITE_GOODS_LARGE_WOOD, 
			Enums.Goods.STORED_STONE_BLOCKS => Enums.eUISprites.SPRITE_GOODS_LARGE_STONE, 
			Enums.Goods.STORED_IRON_INGOTS => Enums.eUISprites.SPRITE_GOODS_LARGE_IRON, 
			Enums.Goods.STORED_PITCH_RAW => Enums.eUISprites.SPRITE_GOODS_LARGE_PITCH, 
			Enums.Goods.STORED_PITCH_REFINED => Enums.eUISprites.SPRITE_GOODS_LARGE_PITCH, 
			Enums.Goods.STORED_RAW_HOPS => Enums.eUISprites.SPRITE_GOODS_LARGE_HOPS, 
			Enums.Goods.STORED_RAW_WHEAT => Enums.eUISprites.SPRITE_GOODS_LARGE_WHEAT, 
			Enums.Goods.STORED_FOOD_BREAD => Enums.eUISprites.SPRITE_GOODS_LARGE_BREAD, 
			Enums.Goods.STORED_FOOD_CHEESE => Enums.eUISprites.SPRITE_GOODS_LARGE_CHEESE, 
			Enums.Goods.STORED_FOOD_MEAT => Enums.eUISprites.SPRITE_GOODS_LARGE_MEAT, 
			Enums.Goods.STORED_FOOD_FRUIT => Enums.eUISprites.SPRITE_GOODS_LARGE_APPLES, 
			Enums.Goods.STORED_FOOD_ALE => Enums.eUISprites.SPRITE_GOODS_LARGE_ALE, 
			Enums.Goods.STORED_GOLD => Enums.eUISprites.SPRITE_GOODS_LARGE_GOLD, 
			Enums.Goods.STORED_FLOUR => Enums.eUISprites.SPRITE_GOODS_LARGE_FLOUR, 
			Enums.Goods.STORED_BOWS => Enums.eUISprites.SPRITE_GOODS_LARGE_BOWS, 
			Enums.Goods.STORED_CROSSBOWS => Enums.eUISprites.SPRITE_GOODS_LARGE_XBOWS, 
			Enums.Goods.STORED_SPEARS => Enums.eUISprites.SPRITE_GOODS_LARGE_SPEARS, 
			Enums.Goods.STORED_PIKES => Enums.eUISprites.SPRITE_GOODS_LARGE_PIKES, 
			Enums.Goods.STORED_MACES => Enums.eUISprites.SPRITE_GOODS_LARGE_MACES, 
			Enums.Goods.STORED_SWORDS => Enums.eUISprites.SPRITE_GOODS_LARGE_SWORDS, 
			Enums.Goods.STORED_LEATHER_ARMOUR => Enums.eUISprites.SPRITE_GOODS_LARGE_LEATHER_ARMOUR, 
			Enums.Goods.STORED_METAL_ARMOUR => Enums.eUISprites.SPRITE_GOODS_LARGE_ARMOUR, 
			_ => Enums.eUISprites.SPRITE_GOODS_LARGE_WOOD, 
		};
	}

	public static MainViewModel INIT()
	{
		instance = new MainViewModel();
		return instance;
	}

	public void ScaleIngameUI(float scaleFactor)
	{
		if (scaleFactor < 0f)
		{
			scaleFactor = 0f;
		}
		else if (scaleFactor > 1f)
		{
			scaleFactor = 1f;
		}
		float num = Screen.width;
		float num2 = Screen.height;
		if (num <= 1366f || num2 <= 768f)
		{
			scaleFactor = 0f;
		}
		if (scaleFactor == 0f)
		{
			iUIScaleValueWidth = (int)num;
			iUIScaleValueHeight = (int)num2;
		}
		else
		{
			float num3 = 1360f;
			float num4 = 768f;
			float a = num / num3;
			float b = num2 / num4;
			float num5 = Mathf.Min(a, b);
			if (scaleFactor < 1f)
			{
				num5 = (num5 - 1f) * scaleFactor + 1f;
			}
			iUIScaleValueWidth = (int)(num / num5);
			iUIScaleValueHeight = (int)(num2 / num5);
		}
		UIScaleValueWidth = iUIScaleValueWidth.ToString();
		UIScaleValueHeight = iUIScaleValueHeight.ToString();
		if (FatControler.instance != null)
		{
			FatControler.instance.SHLowerUIPoint = 0f;
		}
		if (Show_InGame)
		{
			Director.instance.ReGetUIEdge();
		}
	}

	public void SetStartingSpecial(bool visible)
	{
		if (visible && _scenarioStartingSpecialText == Visibility.Collapsed)
		{
			ScenarioStartingSpecial = Visibility.Visible;
		}
		else if (!visible && _scenarioStartingSpecialText == Visibility.Visible)
		{
			ScenarioStartingSpecial = Visibility.Collapsed;
		}
	}

	public void SetMapTypeVisibility(bool SiegeMode)
	{
		if (SiegeMode && _scenarioSiegeUIVisibility == Visibility.Collapsed)
		{
			ScenarioNormalUIVisibility = Visibility.Collapsed;
			ScenarioSiegeUIVisibility = Visibility.Visible;
		}
		else if (!SiegeMode && _scenarioSiegeUIVisibility == Visibility.Visible)
		{
			ScenarioNormalUIVisibility = Visibility.Visible;
			ScenarioSiegeUIVisibility = Visibility.Collapsed;
		}
	}

	private void setUpObservableCollections()
	{
		setUpObservableCollections_HUD_MissionOver();
		setUpObservableCollections_FRONT_StandaloneMission();
		StartingGoods = new ObservableCollection<int>();
		TradingGoods = new ObservableCollection<string>();
		TradingGoodsBool = new ObservableCollection<bool>();
		DisplayHudGoodsBool = new ObservableCollection<bool>();
		AllStoredGoods = new ObservableCollection<int>();
		AllStoredGoodsVisible = new ObservableCollection<bool>();
		AllAutoTradingGoodsVisible = new ObservableCollection<bool>();
		AllTroops = new ObservableCollection<int>();
		GoodsPrices = new ObservableCollection<string>();
		AttackingForces = new ObservableCollection<int>();
		AttackingForcesSiege = new ObservableCollection<int>();
		MessageButtonHighlight = new ObservableCollection<bool>();
		InvasionButtonHighlight = new ObservableCollection<bool>();
		InvasionSpawnMarkersHighlight = new ObservableCollection<bool>();
		InvasionSize = new ObservableCollection<string>();
		EventTextList = new ObservableCollection<string>();
		ScenarioEditTeams = new ObservableCollection<string>();
		FeedInGoodsAmountList = new ObservableCollection<string>();
		FeedInGoodsVisible = new ObservableCollection<bool>();
		OST_KOTH_Name = new ObservableCollection<string>();
		OST_KOTH_Value = new ObservableCollection<string>();
		OST_KOTH_Visible = new ObservableCollection<bool>();
		OST_KOTH_Color = new ObservableCollection<SolidColorBrush>();
		OST_Ping_Name = new ObservableCollection<string>();
		OST_Ping_Value = new ObservableCollection<string>();
		OST_Ping_Visible = new ObservableCollection<bool>();
		OST_Ping_Color = new ObservableCollection<SolidColorBrush>();
		OST_Ping_Value_Color = new ObservableCollection<SolidColorBrush>();
		MPChat_Colours = new ObservableCollection<SolidColorBrush>();
		MPChat_Names = new ObservableCollection<string>();
		MPChat_Text = new ObservableCollection<string>();
		MPChat_Rows = new ObservableCollection<bool>();
		FreebuildEventBorders = new ObservableCollection<Visibility>();
		FreebuildInvasionSize = new ObservableCollection<string>();
		OptionsSectionsBorders = new ObservableCollection<Visibility>();
		CampaignMenuButtonBorders = new ObservableCollection<Visibility>();
		EcoCampaignMenuButtonBorders = new ObservableCollection<Visibility>();
		ExtraCampaignMenuButtonBorders = new ObservableCollection<Visibility>();
		TrailCampaignMenuButtonBorders = new ObservableCollection<Visibility>();
		CampaignMenuButtonsVisible = new ObservableCollection<Visibility>();
		MarkerSelected = new ObservableCollection<bool>();
		for (int i = 0; i < 25; i++)
		{
			StartingGoods.Add(-1);
			TradingGoods.Add("");
			TradingGoodsBool.Add(item: false);
			DisplayHudGoodsBool.Add(item: true);
			AllStoredGoods.Add(0);
			AllStoredGoodsVisible.Add(item: false);
			AllAutoTradingGoodsVisible.Add(item: false);
			GoodsPrices.Add("");
			FeedInGoodsAmountList.Add("Test");
			FeedInGoodsVisible.Add(item: true);
		}
		for (int j = 0; j < 19; j++)
		{
			AllTroops.Add(0);
		}
		for (int k = 0; k < 10; k++)
		{
			AttackingForces.Add(-1);
		}
		for (int l = 0; l < 6; l++)
		{
			AttackingForcesSiege.Add(-1);
		}
		for (int m = 0; m < 20; m++)
		{
			MessageButtonHighlight.Add(item: false);
		}
		for (int n = 0; n < 5; n++)
		{
			InvasionButtonHighlight.Add(item: false);
		}
		for (int num = 0; num < 16; num++)
		{
			InvasionSize.Add("");
			FreebuildInvasionSize.Add("");
		}
		for (int num2 = 0; num2 < 19; num2++)
		{
			EventTextList.Add("");
		}
		for (int num3 = 0; num3 < 11; num3++)
		{
			InvasionSpawnMarkersHighlight.Add(item: false);
		}
		for (int num4 = 0; num4 < 9; num4++)
		{
			ScenarioEditTeams.Add("1");
		}
		SolidColorBrush item = new SolidColorBrush(Noesis.Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue));
		for (int num5 = 0; num5 < 8; num5++)
		{
			OST_KOTH_Name.Add("");
			OST_KOTH_Value.Add("");
			OST_KOTH_Visible.Add(item: false);
			OST_KOTH_Color.Add(item);
			OST_Ping_Name.Add("");
			OST_Ping_Value.Add("");
			OST_Ping_Visible.Add(item: false);
			OST_Ping_Color.Add(item);
			OST_Ping_Value_Color.Add(item);
		}
		for (int num6 = 0; num6 < 5; num6++)
		{
			MPChat_Names.Add("");
			MPChat_Text.Add("");
			MPChat_Rows.Add(item: false);
			MPChat_Colours.Add(item);
		}
		for (int num7 = 0; num7 < 14; num7++)
		{
			FreebuildEventBorders.Add(Visibility.Hidden);
		}
		for (int num8 = 0; num8 < 5; num8++)
		{
			OptionsSectionsBorders.Add(Visibility.Hidden);
		}
		for (int num9 = 0; num9 <= 21; num9++)
		{
			CampaignMenuButtonBorders.Add(Visibility.Hidden);
			CampaignMenuButtonsVisible.Add(Visibility.Hidden);
		}
		for (int num10 = 0; num10 <= 5; num10++)
		{
			EcoCampaignMenuButtonBorders.Add(Visibility.Hidden);
		}
		for (int num11 = 0; num11 <= 7; num11++)
		{
			ExtraCampaignMenuButtonBorders.Add(Visibility.Hidden);
		}
		for (int num12 = 0; num12 <= 11; num12++)
		{
			TrailCampaignMenuButtonBorders.Add(Visibility.Hidden);
		}
		for (int num13 = 0; num13 < 10; num13++)
		{
			MarkerSelected.Add(item: false);
		}
		SHGoodsTestData();
	}

	public void SHGoodsTestData()
	{
		StartingGoods[21] = 9;
		StartingGoods[22] = 88;
		StartingGoods[23] = 7;
		StartingGoods[24] = 6666;
		AllStoredGoods[17] = 15;
		AllStoredGoods[23] = 33;
		AllStoredGoods[2] = 9;
		AllStoredGoods[7] = 88;
		AllStoredGoods[14] = 555;
		AllStoredGoods[12] = 1;
		AllStoredGoods[11] = 24;
		AllStoredGoods[10] = 333;
		AllStoredGoods[13] = 4560;
		AllTroops[1] = 12;
		AllTroops[7] = 2;
		GoodsPrices[19] = "12/24";
		GoodsPrices[17] = "12/24";
		GoodsPrices[21] = "12/24";
		GoodsPrices[18] = "12/24";
		GoodsPrices[20] = "12/24";
		GoodsPrices[22] = "12/24";
		GoodsPrices[24] = "12/24";
		GoodsPrices[23] = "12/24";
		GoodsPrices[2] = "12/24";
		GoodsPrices[4] = "12/24";
		GoodsPrices[6] = "12/24";
		GoodsPrices[8] = "12/24";
		GoodsPrices[3] = "12/24";
		GoodsPrices[14] = "12/24";
		GoodsPrices[16] = "12/24";
		GoodsPrices[9] = "12/24";
		GoodsPrices[12] = "12/24";
		GoodsPrices[11] = "2/4";
		GoodsPrices[10] = "8/4";
		GoodsPrices[13] = "12/24";
	}

	public void resetDiffData()
	{
		for (int i = 0; i < 25; i++)
		{
			diffSHStoredGoods[i] = -1;
			diffSHGoodsPrices[i] = -1L;
			diffSHAutoTradeGoods[i] = false;
			AllAutoTradingGoodsVisible[i] = false;
		}
		for (int j = 0; j < 18; j++)
		{
			diffSHTroopCounts[j] = -1;
		}
	}

	public void UpdateSHGoodsData()
	{
		for (int i = 0; i < 25; i++)
		{
			if (GameData.Instance.lastGameState.resources[i] != diffSHStoredGoods[i])
			{
				diffSHStoredGoods[i] = GameData.Instance.lastGameState.resources[i];
				AllStoredGoods[i] = GameData.Instance.lastGameState.resources[i];
			}
			long num = (GameData.Instance.lastGameState.trade_sell_costs[i] / 5) | (GameData.Instance.lastGameState.trade_buy_costs[i] / 5) | (GameData.Instance.lastGameState.trade_buy_amounts[i] << 16);
			if (num != diffSHGoodsPrices[i])
			{
				diffSHGoodsPrices[i] = num;
				if (GameData.Instance.lastGameState.trade_buy_amounts[i] >= 0)
				{
					GoodsPrices[i] = GameData.Instance.lastGameState.trade_sell_costs[i] / 5 + "/" + GameData.Instance.lastGameState.trade_buy_costs[i] / 5;
				}
				else
				{
					GoodsPrices[i] = "";
				}
				AllStoredGoodsVisible[i] = GameData.Instance.lastGameState.trade_buy_amounts[i] >= 0;
			}
			bool flag = GameData.Instance.lastGameState.trade_buy_amounts[i] >= 0 && GameData.Instance.lastGameState.autotrade_onoff[i] > 0;
			if (diffSHAutoTradeGoods[i] != flag)
			{
				AllAutoTradingGoodsVisible[i] = flag;
				diffSHAutoTradeGoods[i] = flag;
			}
		}
	}

	public void UpdateSHTroopsData()
	{
		for (int i = 1; i < 19; i++)
		{
			if (GameData.Instance.lastGameState.troop_counts[i - 1] != diffSHTroopCounts[i - 1])
			{
				diffSHTroopCounts[i - 1] = GameData.Instance.lastGameState.troop_counts[i - 1];
				AllTroops[i] = GameData.Instance.lastGameState.troop_counts[i - 1];
			}
		}
	}

	public MainViewModel()
	{
		ButtonFrontEndEnterCommand = new DelegateCommand(ButtonFrontEndEnter);
		IntroSkipCommand = new DelegateCommand(IntroSkipFunction);
		SubtitlesCommand = new DelegateCommand(SubtitlesFunction);
		EnterYourNameCommand = new DelegateCommand(EnterYourNameFunction);
		ButtonMainMenuCommand = new DelegateCommand(ButtonMainMenuFunction);
		CampaignMenuCommand = new DelegateCommand(CampaignMenuCommandFunction);
		EcoCampaignMenuCommand = new DelegateCommand(EcoCampaignMenuCommandFunction);
		ExtraCampaignMenuCommand = new DelegateCommand(ExtraCampaignMenuCommandFunction);
		ExtraEcoCampaignMenuCommand = new DelegateCommand(ExtraEcoCampaignMenuCommandFunction);
		TrailCampaignMenuCommand = new DelegateCommand(TrailCampaignMenuCommandFunction);
		StandaloneMenuCommand = new DelegateCommand(StandaloneMenuCommandFunction);
		MultiplayerMenuCommand = new DelegateCommand(MultiplayerMenuCommandFunction);
		MapEditorSetupCommand = new DelegateCommand(MapEditorSetupFunction);
		ButtonNullCommand = new DelegateCommand(ButtonNull);
		ButtonExitCommand = new DelegateCommand(ButtonExit);
		ButtonNewSceneCommand = new DelegateCommand(ButtonNewScene);
		ButtonDebugCommand = new DelegateCommand(ButtonDebug);
		ButtonBuildCommand = new DelegateCommand(ButtonBuild);
		ButtonMECommand = new DelegateCommand(ButtonME);
		ButtonToggleScenrioEditorCommand = new DelegateCommand(ButtonToggleScenrioEditor);
		LeftClickSelectedTroopCommand = new DelegateCommand(TroopsLeftClickCommand);
		RightClickSelectedTroopCommand = new DelegateCommand(TroopsRightClickCommand);
		ButtonChangeStanceCommand = new DelegateCommand(ButtonChangeStance);
		ButtonUnitDisbandCommand = new DelegateCommand(ButtonUnitDisband);
		ButtonUnitBuildCatCommand = new DelegateCommand(ButtonUnitBuildCat);
		ButtonUnitStopCommand = new DelegateCommand(ButtonUnitStop);
		ButtonUnitBuildTrebCommand = new DelegateCommand(ButtonUnitBuildTreb);
		ButtonUnitPatrolCommand = new DelegateCommand(ButtonUnitPatrol);
		ButtonUnitBuildTwrCommand = new DelegateCommand(ButtonUnitBuildTwr);
		ButtonUnitAttackHereCommand = new DelegateCommand(ButtonUnitAttackHere);
		ButtonUnitBuildTunnelCommand = new DelegateCommand(ButtonUnitBuildTunnel);
		ButtonUnitPourOilCommand = new DelegateCommand(ButtonUnitPourOil);
		ButtonUnitBuildRamCommand = new DelegateCommand(ButtonUnitBuildRam);
		ButtonUnitBuildSiegeKitCommand = new DelegateCommand(ButtonUnitBuildSiegeKit);
		ButtonUnitBuildMantletCommand = new DelegateCommand(ButtonUnitBuildMantlet);
		ButtonUnitRechargeRockCommand = new DelegateCommand(ButtonUnitRechargeRock);
		ButtonUnitLaunchCowCommand = new DelegateCommand(ButtonUnitLaunchCow);
		ButtonUnitbackCommand = new DelegateCommand(ButtonUnitback);
		ButtonTroopPanelMouseEnterCommand = new DelegateCommand(ButtonTroopPanelMouseEnter);
		ButtonTroopPanelMouseLeaveCommand = new DelegateCommand(ButtonTroopPanelMouseLeave);
		ButtonTroopPanelToggleCGCommand = new DelegateCommand(ButtonTroopPanelToggleCG);
		ButtonControlGroupCommand = new DelegateCommand(ButtonControlGroupClick);
		ButtonTroopPanelPageCommand = new DelegateCommand(ButtonTroopPanelPageClick);
		ButtonBuildingHelpCommand = new DelegateCommand(ButtonGoToHelpScreen);
		ButtonBuildingZZZCommand = new DelegateCommand(ButtonToggleZZZMode);
		ButtonCreateTroopCommand = new DelegateCommand(ButtonCreateTroop);
		ButtonTroopCreateEnterCommand = new DelegateCommand(ButtonEnterCreateTroop);
		ButtonTroopCreateLeaveCommand = new DelegateCommand(ButtonLeaveCreateTroop);
		ButtonSetRationsCommand = new DelegateCommand(ButtonChangeRations);
		ButtonGranarySwitchModeCommand = new DelegateCommand(ButtonGranarySwitchMode);
		ButtonSetEdibleCommand = new DelegateCommand(ButtonChangeEdibleState);
		ButtonAdjustTaxCommand = new DelegateCommand(ButtonAdjustTax);
		ButtonGateControlsCommand = new DelegateCommand(ButtonGateControls);
		ButtonDrawbridgeControlsCommand = new DelegateCommand(ButtonDrawbridgeControls);
		ButtonChangeWorkshopOutputCommand = new DelegateCommand(ButtonChangeWorkshopOutput);
		ButtonReleaseDogsCommand = new DelegateCommand(ButtonReleaseDogs);
		ButtonNewTradeTypeCommand = new DelegateCommand(ButtonNewTradeType);
		ButtonNewTradeGoodsTypeCommand = new DelegateCommand(ButtonNewTradeGoodsType);
		ButtonCycleTradeGoodsTypeCommand = new DelegateCommand(ButtonCycleTradeGoodsType);
		ButtonBuySellCommand = new DelegateCommand(ButtonBuySell);
		ButtonRepairCommand = new DelegateCommand(ButtonRepairFunction);
		ButtonRepairMouseEnterCommand = new DelegateCommand(ButtonRepairMouseEnterFunction);
		ButtonRepairMouseLeaveCommand = new DelegateCommand(ButtonRepairMouseLeaveFunction);
		ButtonScribeMouseEnterCommand = new DelegateCommand(ButtonScribeMouseEnterFunction);
		ButtonScribeMouseLeaveCommand = new DelegateCommand(ButtonScribeMouseLeaveFunction);
		ButtonMakeWeaponMouseEnterCommand = new DelegateCommand(ButtonMakeWeaponMouseEnterFunction);
		ButtonMakeWeaponMouseLeaveCommand = new DelegateCommand(ButtonMakeWeaponMouseLeaveFunction);
		ButtonReportsCommand = new DelegateCommand(ButtonReports);
		ButtonReturnToReportsCommand = new DelegateCommand(ButtonReturnToReports);
		ButtonReportViewEventsCommand = new DelegateCommand(ButtonReportsViewEvents);
		ButtonToggleArmyReportCommand = new DelegateCommand(ButtonToggleArmyReport);
		ButtonActionPointCommand = new DelegateCommand(ButtonActionPoint);
		ButtonRadarZoomCommand = new DelegateCommand(ButtonRadarZoomFunction);
		ButtonGoToBriefingCommand = new DelegateCommand(ButtonGotoBriefing);
		ButtonBriefingQuitCommand = new DelegateCommand(ButtonBriefingQuit);
		ButtonBriefingResumeCommand = new DelegateCommand(ButtonBriefingResume);
		ButtonBriefingModeCommand = new DelegateCommand(ButtonBriefingMode);
		ButtonBriefingDifficultyCommand = new DelegateCommand(ButtonBriefingDifficulty);
		ButtonBriefingHintCommand = new DelegateCommand(ButtonBriefingHint);
		ButtonBriefingBackCommand = new DelegateCommand(ButtonBriefingBack);
		ButtonBriefingRolloverCommand = new DelegateCommand(ButtonBriefingRollover);
		ButtonMainHelpCommand = new DelegateCommand(ButtonMainHelp);
		ButtonExtendedFeaturesCommand = new DelegateCommand(ButtonExtendedFeaturesFunction);
		ButtonScenrioStartMonthCommand = new DelegateCommand(ButtonScenarioStartMonth);
		ButtonScenarioAltTextCommand = new DelegateCommand(ButtonScenarioAltTextFunc);
		ButtonScenrioStartRationsCommand = new DelegateCommand(ButtonScenarioStartRations);
		ButtonScenrioStartTaxCommand = new DelegateCommand(ButtonScenarioStartTax);
		ButtonScenrioViewCommand = new DelegateCommand(ButtonScenarioViewBtn);
		ButtonScenrioAdjustDateCommand = new DelegateCommand(ButtonScenarioAdjustDateBtn);
		ButtonSelectStartingGoodCommand = new DelegateCommand(ButtonScenarioStartingGoodSelect);
		ButtonSelectStartingTraderCommand = new DelegateCommand(ButtonScenarioStartingTraderToggle);
		ButtonScenarioBuildingAvailToggleCommand = new DelegateCommand(ButtonScenarioBuildingAvailToggle);
		ButtonSelectAttackingForcesCommand = new DelegateCommand(ButtonScenarioAttackingForcesSelect);
		ButtonScenarioEditSettingsCommand = new DelegateCommand(ButtonScenarioEditSettings);
		ButtonWorkshopCommand = new DelegateCommand(ButtonWorkshopFunction);
		RightClickSizeCommand = new DelegateCommand(RightClickSizeClick);
		RightClickRulerCommand = new DelegateCommand(RightClickRulerClick);
		ButtonScenrioMessageMonthCommand = new DelegateCommand(ButtonScenarioMessageMonth);
		ButtonScenrioMessageGroup = new DelegateCommand(ButtonScenrioMessageGroupFunc);
		ButtonScenrioInvasionMonthCommand = new DelegateCommand(ButtonScenarioInvasionMonth);
		ButtonScenrioInvasionGroup = new DelegateCommand(ButtonScenrioInvasionGroupFunc);
		ButtonScenrioInvasionMarkerID = new DelegateCommand(ButtonScenrioInvasionMarkerIDFunc);
		ButtonSelectInvasionSizeCommand = new DelegateCommand(ButtonSelectInvasionSize);
		ButtonScenrioEventMonthCommand = new DelegateCommand(ButtonScenarioEventMonth);
		ButtonScenarionEventConditionToggleCommand = new DelegateCommand(ButtonScenarionEventConditionToggle);
		ButtonScenarionEventConditionOnOffCommand = new DelegateCommand(ButtonScenarionEventConditionOnOff);
		ButtonScenarionEventConditionAndOrCommand = new DelegateCommand(ButtonScenarionEventConditionAndOr);
		ButtonScenarioSpecialCommand = new DelegateCommand(ButtonScenarioSpecialFunction);
		PreLoadCommand = new DelegateCommand(PreLoadClick);
		ClickStoryAdvanceCommand = new DelegateCommand(ClickStoryAdvance);
		ClickDbgPrePostCommand = new DelegateCommand(ClickDbgPrePost);
		ClickDbgStoryChapterCommand = new DelegateCommand(ClickDbgStoryChapter);
		ButtonIngameMenuCommand = new DelegateCommand(ButtonIngameMenuFunction);
		ButtonConfirmationCommand = new DelegateCommand(ButtonConfirmationFunction);
		ButtonLoadSaveCommand = new DelegateCommand(ButtonLoadSaveFunction);
		ButtonTutorialCommand = new DelegateCommand(ButtonTutorialFunction);
		ButtonFreebuildCommand = new DelegateCommand(ButtonFreebuildFunction);
		OptionsCommand = new DelegateCommand(OptionsFunction);
		ButtonMPInviteCommand = new DelegateCommand(ButtonMPInviteFunction);
		ButtonMPConnectionIssueCommand = new DelegateCommand(ButtonMPConectionIssueFunction);
		ButtonMPChatCommand = new DelegateCommand(ButtonMPChatFunction);
		MO_ClickCommand = new DelegateCommand(MO_ClickFunction);
		setupSprites();
		SetupEnumTranslation();
		InitialiseStory();
		setUpObservableCollections();
		StoryMap.Instance.SetupStoryMap();
		ScaleIngameUI(ConfigSettings.Settings_UIScale);
		viewModelLoaded = true;
	}

	public void NotifyPropertyChanged(string info)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
	}

	public int GetMainUIMode()
	{
		return mainUIMode;
	}

	public void ResolutionUpdated()
	{
		GoToScreen(CurrentScreenNo);
	}

	public void PreLoadClick(object param)
	{
	}

	public void setUpInbuilding(int overridePanel, int overrideType)
	{
		int num = overridePanel;
		int num2 = overrideType;
		if (GameData.Instance.lastGameState == null)
		{
			return;
		}
		if (num <= 0)
		{
			num = GameData.Instance.app_sub_mode;
		}
		switch (num)
		{
		case 70:
			if (num2 <= 0)
			{
				num2 = GameData.Instance.lastGameState.in_chimp_type;
			}
			break;
		case 11:
			num2 = 10;
			break;
		default:
			if (num2 <= 0)
			{
				num2 = GameData.Instance.lastGameState.in_structure_type;
			}
			break;
		}
		HUD_Markers_Vis = false;
		HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefBarracksPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefHUDBuildingFullClickMask.Visibility = Visibility.Visible;
		HUDBuildingPanel.RefWorkerPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefKeepPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefGranaryPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefArmouryPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefStockpilePanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefInnPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefFletchersPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefPoleturnersPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefBlacksmithsPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefChurchPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefShowGatePanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefShowDrawbridgePanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefShowEngineersGuildPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefShowTunellersGuildPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefShowDogsPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefReportsPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefReportsFoodPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefReportsPopularityPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefReportsPopEventsPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefReportsFearFactorPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefReportsPopulationPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefReportsArmy1Panel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefReportsArmy2Panel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefReportsStoresPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefReportsWeaponsPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefReportsReligionPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefChimpPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefTradepostPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefTradingFoodPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefTradingResourcesPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefTradingWeaponsPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefTradingPricesPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefTradingTradePanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefShowWorkersPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefShowInfoPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefShowRepairPanel.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefHelpButton.Visibility = Visibility.Hidden;
		if (HUDBuildingPanel.RefTradePost_Trade_Auto.Visibility == Visibility.Visible)
		{
			EngineInterface.GameAction(Enums.GameActionCommand.Autotrade_Apply, 0, 0);
			EngineInterface.GameAction(Enums.GameActionCommand.Autotrade_Pause, 0, 0);
			HUDBuildingPanel.RefTradePost_Trade_Auto.Visibility = Visibility.Hidden;
		}
		switch (num)
		{
		case 1:
			HUDBuildingPanel.RefBarracksPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefHUDBuildingFullClickMask.Visibility = Visibility.Hidden;
			ButtonLeaveCreateTroop("");
			break;
		case 2:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefKeepPanel.Visibility = Visibility.Visible;
			break;
		case 4:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefGranaryPanel.Visibility = Visibility.Visible;
			InGranarySetRationHand(2, 1);
			break;
		case 12:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefArmouryPanel.Visibility = Visibility.Visible;
			break;
		case 11:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefStockpilePanel.Visibility = Visibility.Visible;
			break;
		case 3:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefInnPanel.Visibility = Visibility.Visible;
			break;
		case 13:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefFletchersPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.OpenFletchers();
			break;
		case 15:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefPoleturnersPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.OpenPoleturners();
			break;
		case 14:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefBlacksmithsPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.OpenBlacksmiths();
			break;
		case 35:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefChurchPanel.Visibility = Visibility.Visible;
			GroomImage = GameSprites[151];
			BrideImage = GameSprites[155];
			break;
		case 36:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefShowGatePanel.Visibility = Visibility.Visible;
			break;
		case 37:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefShowDrawbridgePanel.Visibility = Visibility.Visible;
			break;
		case 88:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefShowDogsPanel.Visibility = Visibility.Visible;
			break;
		case 23:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefShowEngineersGuildPanel.Visibility = Visibility.Visible;
			ButtonLeaveCreateTroop("");
			break;
		case 24:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefShowTunellersGuildPanel.Visibility = Visibility.Visible;
			ButtonLeaveCreateTroop("");
			break;
		case 71:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefReportsPanel.Visibility = Visibility.Visible;
			break;
		case 75:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefReportsFoodPanel.Visibility = Visibility.Visible;
			InFoodReportShowEaten(1);
			break;
		case 72:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefReportsPopularityPanel.Visibility = Visibility.Visible;
			break;
		case 69:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefReportsPopEventsPanel.Visibility = Visibility.Visible;
			break;
		case 73:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefReportsFearFactorPanel.Visibility = Visibility.Visible;
			break;
		case 74:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefReportsPopulationPanel.Visibility = Visibility.Visible;
			break;
		case 76:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefReportsArmy1Panel.Visibility = Visibility.Visible;
			break;
		case 68:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefReportsArmy2Panel.Visibility = Visibility.Visible;
			break;
		case 77:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefReportsStoresPanel.Visibility = Visibility.Visible;
			break;
		case 78:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefReportsWeaponsPanel.Visibility = Visibility.Visible;
			break;
		case 79:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefReportsReligionPanel.Visibility = Visibility.Visible;
			break;
		case 25:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefTradepostPanel.Visibility = Visibility.Visible;
			break;
		case 54:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefTradingFoodPanel.Visibility = Visibility.Visible;
			break;
		case 55:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefTradingResourcesPanel.Visibility = Visibility.Visible;
			break;
		case 56:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefTradingWeaponsPanel.Visibility = Visibility.Visible;
			break;
		case 53:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefTradingPricesPanel.Visibility = Visibility.Visible;
			break;
		case 57:
			if (!Director.instance.MultiplayerGame || GameData.Instance.lastGameState.MP_AllowAutoTrading)
			{
				HUDBuildingPanel.RefTrade_GoTo_Auto.Visibility = Visibility.Visible;
				HUDBuildingPanel.UpdateAutoTradeSelectButton();
			}
			else
			{
				HUDBuildingPanel.RefTrade_GoTo_Auto.Visibility = Visibility.Hidden;
			}
			HUDBuildingPanel.RefTradePost_Trade_Normal.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefTradePost_Trade_Auto.Visibility = Visibility.Hidden;
			TradeErrorText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 130);
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefTradingTradePanel.Visibility = Visibility.Visible;
			TradeGoldImage = GameSprites[14];
			SetSpriteWidth2(14, 100);
			TradeGoodsImage = GameSprites[3];
			SetSpriteWidth1(3, 100);
			TradePrevGoodsImage = GameSprites[13];
			SetSpriteWidth3(13, 50);
			TradeNextGoodsImage = GameSprites[8];
			SetSpriteWidth4(13, 50);
			break;
		case 70:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			HUDBuildingPanel.RefChimpPanel.Visibility = Visibility.Visible;
			break;
		default:
			HUDBuildingPanel.RefBuildingPanel.Visibility = Visibility.Visible;
			break;
		}
		int buildingSketchImage = HUDBuildingPanel.GetBuildingSketchImage(num2, num);
		int num3 = 123;
		if (buildingSketchImage > 0)
		{
			SketchImage = GameSprites[buildingSketchImage];
			SketchWidth = num3;
			SketchHeight = 109f;
		}
		else if (num == 71)
		{
			SketchImage = GameSprites[175];
			SketchWidth = 247f;
			SketchHeight = 141f;
		}
		else
		{
			SketchImage = null;
		}
		BuildingTitle = Translate.Instance.lookUpText(HUDBuildingPanel.GetBuildingTitle(num2, num));
		switch (num)
		{
		case 72:
			if (FatControler.locale == "ruru")
			{
				BuildingTitle_Margin = "17,17,0,0";
			}
			else
			{
				BuildingTitle_Margin = "20,24,0,0";
			}
			break;
		case 74:
			if (FatControler.locale == "thth")
			{
				BuildingTitle_Margin = "17,17,0,0";
			}
			else
			{
				BuildingTitle_Margin = "20,24,0,0";
			}
			break;
		default:
			BuildingTitle_Margin = "20,24,0,0";
			break;
		}
		BuildingLine3Text = Translate.Instance.lookUpText(HUDBuildingPanel.GetBuildingInfo(num2, num));
		if (HUDBuildingPanel.GetBuildingSleepable(num2, num))
		{
			HUDBuildingPanel.RefWorkerPanel.Visibility = Visibility.Visible;
		}
		else
		{
			HUDBuildingPanel.RefWorkerPanel.Visibility = Visibility.Hidden;
		}
		CanShowWorkers = HUDBuildingPanel.GetBuildingShowWorkers(num2, num);
		if (CanShowWorkers)
		{
			HUDBuildingPanel.RefShowWorkersPanel.Visibility = Visibility.Visible;
		}
		else
		{
			HUDBuildingPanel.RefShowWorkersPanel.Visibility = Visibility.Hidden;
		}
		if (HUDBuildingPanel.GetBuildingShowInfo(num2, num))
		{
			HUDBuildingPanel.RefShowInfoPanel.Visibility = Visibility.Visible;
		}
		else
		{
			HUDBuildingPanel.RefShowInfoPanel.Visibility = Visibility.Hidden;
		}
		if (HUDBuildingPanel.GetBuildingShowRepair(num2, num))
		{
			HUDBuildingPanel.RefShowRepairPanel.Visibility = Visibility.Visible;
		}
		else
		{
			HUDBuildingPanel.RefShowRepairPanel.Visibility = Visibility.Hidden;
		}
		if (HUDBuildingPanel.GetBuildingShowHelp(num2, num))
		{
			HUDBuildingPanel.RefHelpButton.Visibility = Visibility.Visible;
		}
		else
		{
			HUDBuildingPanel.RefHelpButton.Visibility = Visibility.Hidden;
		}
		HUDBuildingPanel.UpdateButtonState();
	}

	public void InGranarySetRationHand(int level, int gameOverRide)
	{
		if (gameOverRide == 1)
		{
			level = GameData.Instance.lastGameState.rationing;
		}
		HUDBuildingPanel.RefRationHandNone.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefRationHandHalf.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefRationHandFull.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefRationHandExtra.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefRationHandDouble.Visibility = Visibility.Hidden;
		switch (level)
		{
		case 0:
			HUDBuildingPanel.RefRationHandNone.Visibility = Visibility.Visible;
			break;
		case 1:
			HUDBuildingPanel.RefRationHandHalf.Visibility = Visibility.Visible;
			break;
		case 2:
			HUDBuildingPanel.RefRationHandFull.Visibility = Visibility.Visible;
			break;
		case 4:
			HUDBuildingPanel.RefRationHandExtra.Visibility = Visibility.Visible;
			break;
		case 3:
			HUDBuildingPanel.RefRationHandDouble.Visibility = Visibility.Visible;
			break;
		}
	}

	public void InFoodReportShowEaten(int gameOverRide)
	{
		if (gameOverRide == 1)
		{
			for (int i = 0; i < 4; i++)
			{
				foodNotEdible[i] = GameData.Instance.lastGameState.food_types_not_eatable[i];
			}
		}
		HUDBuildingPanel.RefStopMeatConsumption.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefStopCheeseConsumption.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefStopBreadConsumption.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefStopApplesConsumption.Visibility = Visibility.Hidden;
		if (foodNotEdible[2] == 1)
		{
			HUDBuildingPanel.RefStopMeatConsumption.Visibility = Visibility.Visible;
		}
		if (foodNotEdible[1] == 1)
		{
			HUDBuildingPanel.RefStopCheeseConsumption.Visibility = Visibility.Visible;
		}
		if (foodNotEdible[0] == 1)
		{
			HUDBuildingPanel.RefStopBreadConsumption.Visibility = Visibility.Visible;
		}
		if (foodNotEdible[3] == 1)
		{
			HUDBuildingPanel.RefStopApplesConsumption.Visibility = Visibility.Visible;
		}
	}

	public void buildControlsFreeze(bool Mode)
	{
		if (Mode)
		{
			FreezeMainControls = true;
			HUDmain.RefTabBuildCastle.IsEnabled = false;
			HUDmain.RefTabBuildIndustry.IsEnabled = false;
			HUDmain.RefTabBuildFarms.IsEnabled = false;
			HUDmain.RefTabBuildTown.IsEnabled = false;
			HUDmain.RefTabBuildWeapons.IsEnabled = false;
			HUDmain.RefTabBuildFood.IsEnabled = false;
		}
		else
		{
			FreezeMainControls = false;
			HUDmain.RefTabBuildCastle.IsEnabled = true;
			HUDmain.RefTabBuildIndustry.IsEnabled = true;
			HUDmain.RefTabBuildFarms.IsEnabled = true;
			HUDmain.RefTabBuildTown.IsEnabled = true;
			HUDmain.RefTabBuildWeapons.IsEnabled = true;
			HUDmain.RefTabBuildFood.IsEnabled = true;
		}
	}

	private void ButtonNull(object parameter)
	{
	}

	private void ButtonExit(object parameter)
	{
		ExitApplication();
	}

	public void ExitApplication()
	{
		FatControler.instance.ExitApp();
	}

	private void ButtonNewScene(object parameter)
	{
		int screenNo = Convert.ToInt32(parameter as string);
		InitNewScene((Enums.SceneIDS)screenNo);
	}

	public void InitNewScene(Enums.SceneIDS screenNo)
	{
		if (screenNo == Enums.SceneIDS.Options)
		{
			HUD_Options.OpenOptions(fromIngameMenu: false);
			return;
		}
		if (FatControler.currentScene == Enums.SceneIDS.ActualMainGame && screenNo != Enums.SceneIDS.MainGame && HUD_MPInviteWarning.PendingMPInvite)
		{
			if (Director.instance.SimRunning)
			{
				EditorDirector.instance.stopGameSim();
			}
			return;
		}
		Show_HUD_Scenario = false;
		Show_HUD_ScenarioSpecial = false;
		Show_HUD_IngameMenu = false;
		Show_HUD_Confirmation = false;
		Show_HUD_LoadSaveRequester = false;
		Show_HUD_LoadSaveRequesterMP = false;
		Show_HUD_Tutorial = false;
		Show_HUD_MPInviteWarning = false;
		Show_HUD_WorkshopUploader = false;
		FrontendMenus.ClearUIPanels(frontEndState: false);
		HUD_MPInviteWarning.PendingMPInvite = false;
		Show_HUD_MPConnectionIssue = false;
		SetObjectivePopupState(visible: false);
		SetGoodsPopupState(visible: false);
		Show_HUD_Extras = false;
		ScenarioEditorButtonText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_SCENARIO_EDITOR);
		HUDMPChatMessages.ClearMPChat();
		switch (screenNo)
		{
		case Enums.SceneIDS.MainGame:
			IsMapEditorMode = false;
			screenNo = Enums.SceneIDS.ActualMainGame;
			Show_HUD_Book = true;
			ScribeHeadImage = GameSprites[24];
			HUDRoot.RefRadarMapGrid.Visibility = Visibility.Visible;
			HUDRoot.RefReportsControlGrid.Visibility = Visibility.Visible;
			OST_Cart_Vis = false;
			GoToScreen(screenNo);
			break;
		case Enums.SceneIDS.MapEditor:
			IsMapEditorMode = true;
			MEMode = 1;
			screenNo = Enums.SceneIDS.ActualMainGame;
			Show_HUD_Book = false;
			GoToScreen(screenNo);
			break;
		case (Enums.SceneIDS)96:
			GoToScreen(screenNo);
			break;
		case Enums.SceneIDS.Tutorial:
			IsMapEditorMode = false;
			screenNo = Enums.SceneIDS.ActualMainGame;
			GoToScreen(screenNo);
			HUDTutorial.StartTutorial();
			break;
		case Enums.SceneIDS.ActualMainGame:
			GoToScreen(screenNo);
			break;
		case Enums.SceneIDS.Story:
			GoToScreen(screenNo);
			break;
		default:
			GoToScreen(screenNo);
			break;
		}
	}

	public void GoToScreen(Enums.SceneIDS screenNo)
	{
		CurrentScreenNo = screenNo;
		FatControler.instance.NewScene(screenNo);
	}

	public void IntroSkipFunction(object param)
	{
		Intro_Sequence.ButtonClicked(fromClick: true);
	}

	public void SubtitlesFunction(object param)
	{
		Intro_Sequence.SubtitlesClicked();
	}

	public void EnterYourNameFunction(object param)
	{
		Intro_Sequence.EnterYourNameClicked();
	}

	public void ButtonMainMenuFunction(object param)
	{
		FrontEndMenu.ButtonClicked((string)param);
	}

	public void CampaignMenuCommandFunction(object param)
	{
		int value = int.Parse((string)param, Director.defaultCulture);
		FrontEndMenu.ButtonCampaignClicked(value);
	}

	public void EcoCampaignMenuCommandFunction(object param)
	{
		int value = int.Parse((string)param, Director.defaultCulture);
		FrontEndMenu.ButtonEcoCampaignClicked(value);
	}

	public void ExtraCampaignMenuCommandFunction(object param)
	{
		int value = int.Parse((string)param, Director.defaultCulture);
		FrontEndMenu.ButtonExtraCampaignClicked(value);
	}

	public void ExtraEcoCampaignMenuCommandFunction(object param)
	{
		int value = int.Parse((string)param, Director.defaultCulture);
		FrontEndMenu.ButtonExtraEcoCampaignClicked(value);
	}

	public void TrailCampaignMenuCommandFunction(object param)
	{
		int value = int.Parse((string)param, Director.defaultCulture);
		FrontEndMenu.ButtonTrailCampaignClicked(value);
	}

	private void ButtonFrontEndEnter(object parameter)
	{
	}

	private void ButtonDebug(object parameter)
	{
	}

	private void ButtonBuild(object parameter)
	{
		Enums.eMappers structToMapperEnum = getStructToMapperEnum(parameter as string);
		if (structToMapperEnum == Enums.eMappers.MAPPER_DELETE)
		{
			EditorDirector.instance.placeBuildingInteraction(structToMapperEnum);
			return;
		}
		EditorDirector.instance.placeBuildingInteraction(structToMapperEnum);
		TroopCostText = "0";
	}

	public bool CanPlaceMapper(object parameter)
	{
		string text = parameter as string;
		if (text == "STRUCT_MENU_RETURN_KEEPS")
		{
			if (IsMapEditorMode)
			{
				return true;
			}
			return false;
		}
		switch (text)
		{
		case "STRUCT_MENU_RETURN_TOWERS":
		case "STRUCT_MENU_RETURN_GATEHOUSES":
		case "STRUCT_MENU_RETURN_MILITARY":
		case "STRUCT_MENU_RETURN_GOOD":
		case "STRUCT_MENU_RETURN_BAD":
			return true;
		case "STRUCT_SUB_MENU_GATEHOUSES_WOOD":
			if (GameData.Instance.game_type != 0 && GameData.Instance.game_type != 5)
			{
				text = "STRUCT_GATE_WOOD1D";
			}
			break;
		case "STRUCT_SUB_MENU_GATEHOUSES_STONESMALL":
			if (GameData.Instance.game_type != 0 && GameData.Instance.game_type != 5)
			{
				text = "STRUCT_GATE_STONE1A";
			}
			break;
		case "STRUCT_SUB_MENU_GATEHOUSES_STONELARGE":
			if (GameData.Instance.game_type != 0 && GameData.Instance.game_type != 5)
			{
				text = "STRUCT_GATE_STONE2A";
			}
			break;
		}
		Enums.eMappers structToMapperEnum = getStructToMapperEnum(text);
		if (structToMapperEnum == Enums.eMappers.MAPPER_NULL)
		{
			return true;
		}
		if (GameData.Instance.game_type == 0 && GameData.Instance.mission6Prestart)
		{
			return false;
		}
		return EngineInterface.IsMapperAvailable((int)structToMapperEnum);
	}

	private void ButtonME(object parameter)
	{
		if ((string)parameter == "-1")
		{
			if (GameData.Instance.mapType == Enums.GameModes.BUILD)
			{
				ButtonME("MAPPER_SIGNPOST");
				return;
			}
			HUD_Markers_Vis = !HUD_Markers_Vis;
			if (HUD_Markers_Vis)
			{
				for (int i = 0; i < 10; i++)
				{
					MarkerSelected[i] = false;
				}
				HUDMarkers.RefMarkerInvisible.Visibility = Visibility.Hidden;
				HUDMarkers.RefMarkerVisible.Visibility = Visibility.Hidden;
				HUDMarkers.RefMarkerDisappearing.Visibility = Visibility.Hidden;
			}
		}
		else
		{
			Enums.eMappers mapperEnum = getMapperEnum(parameter as string);
			switch (mapperEnum)
			{
			case Enums.eMappers.MAPPER_BACK:
				HUDmain.InGameOptions(parameter, null);
				break;
			case Enums.eMappers.MAPPER_REPORT1:
			case Enums.eMappers.MAPPER_REPORT2:
			case Enums.eMappers.MAPPER_REPORT3:
			case Enums.eMappers.MAPPER_REPORT4:
			case Enums.eMappers.MAPPER_REPORT5:
			case Enums.eMappers.MAPPER_REPORT6:
			case Enums.eMappers.MAPPER_REPORT7:
			case Enums.eMappers.MAPPER_REPORT8:
				MEFakeLocalPlayer = (int)(1 + mapperEnum - 240);
				EditorDirector.instance.SetEditorPlayerID(MEFakeLocalPlayer);
				break;
			default:
				EditorDirector.instance.mapEditorInteraction(mapperEnum);
				break;
			}
		}
	}

	private void ButtonToggleScenrioEditor(object parameter)
	{
		if (HUDScenario.IsEnabled)
		{
			if (ShowingScenario)
			{
				HUDScenario.StartExitAnim();
				Instance.Compass_Vis = ConfigSettings.Settings_Compass;
				return;
			}
			Show_HUD_Confirmation = false;
			HUDIngameMenu.Close();
			Instance.Compass_Vis = false;
			HUDScenario.initScenarioControls();
			HUDScenario.StartEntryAnim();
		}
	}

	public void MENewBrushSize()
	{
		EditorDirector.instance.SetMEBrushSize(MEBrushSize);
	}

	private void ButtonCreateTroop(object parameter)
	{
		Enums.eChimps chimpEnum = getChimpEnum(parameter as string);
		bool flag = true;
		switch (chimpEnum)
		{
		case Enums.eChimps.CHIMP_TYPE_ARCHER:
			if (GameData.Instance.lastGameState.troop_types_available[0] <= 0 && HUDBuildingPanel.RefRecruitArcherButton.IsEnabled)
			{
				flag = false;
			}
			break;
		case Enums.eChimps.CHIMP_TYPE_SPEARMAN:
			if (GameData.Instance.lastGameState.troop_types_available[2] <= 0 && HUDBuildingPanel.RefRecruitSpearmanButton.IsEnabled)
			{
				flag = false;
			}
			break;
		case Enums.eChimps.CHIMP_TYPE_MACEMAN:
			if (GameData.Instance.lastGameState.troop_types_available[4] <= 0 && HUDBuildingPanel.RefRecruitMacemanButton.IsEnabled)
			{
				flag = false;
			}
			break;
		case Enums.eChimps.CHIMP_TYPE_SWORDSMAN:
			if (GameData.Instance.lastGameState.troop_types_available[5] <= 0 && HUDBuildingPanel.RefRecruitSwordsmanButton.IsEnabled)
			{
				flag = false;
			}
			break;
		case Enums.eChimps.CHIMP_TYPE_XBOWMAN:
			if (GameData.Instance.lastGameState.troop_types_available[1] <= 0 && HUDBuildingPanel.RefRecruitXBowmanButton.IsEnabled)
			{
				flag = false;
			}
			break;
		case Enums.eChimps.CHIMP_TYPE_PIKEMAN:
			if (GameData.Instance.lastGameState.troop_types_available[3] <= 0 && HUDBuildingPanel.RefRecruitPikemanButton.IsEnabled)
			{
				flag = false;
			}
			break;
		case Enums.eChimps.CHIMP_TYPE_KNIGHT:
			if (GameData.Instance.lastGameState.troop_types_available[6] <= 0 && HUDBuildingPanel.RefRecruitKnightButton.IsEnabled)
			{
				flag = false;
			}
			break;
		case Enums.eChimps.CHIMP_TYPE_LADDERMAN:
			if (!HUDBuildingPanel.RefRecruitLaddermanButton.IsEnabled)
			{
				flag = false;
			}
			break;
		case Enums.eChimps.CHIMP_TYPE_ENGINEER:
			if (!HUDBuildingPanel.RefRecruitEngineerButton.IsEnabled)
			{
				flag = false;
			}
			break;
		default:
		{
			Enums.eMappers mapperEnum = getMapperEnum(parameter as string);
			if ((uint)(mapperEnum - 332) <= 6u || (uint)(mapperEnum - 367) <= 2u)
			{
				flag = false;
				EditorDirector.instance.placeBuildingInteraction(mapperEnum);
			}
			break;
		}
		}
		if (flag)
		{
			int structureID = 1;
			if (KeyManager.instance.isShiftDown())
			{
				structureID = 5;
			}
			else if (KeyManager.instance.isCtrlDown())
			{
				structureID = 1000;
			}
			EngineInterface.GameAction(Enums.GameActionCommand.MakeTroop, structureID, (int)chimpEnum);
		}
	}

	private void setCreateTroopText()
	{
		TroopNameCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, (int)lastTroopBuildChimp);
		TroopCostText = " " + GameData.getChimpGoldCost((int)lastTroopBuildChimp) * lastTroopsAmountToMake + "  ";
		if (lastTroopsAmountToMake > 1)
		{
			TroopNameCostText = TroopNameCostText + " x" + lastTroopsAmountToMake;
		}
		TroopNameCostText2 = "[" + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 78).ToUpper() + "] x" + lastTroopsAmountToMakex5 + "   [" + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 79).ToUpper() + "] x" + lastTroopsAmountToMakeMax;
	}

	public void ButtonEnterCreateTroop(object parameter)
	{
		string text = (lastTroopBuildOver = parameter as string);
		lastTroopBuildChimp = Enums.eChimps.CHIMP_NUM_TYPES;
		Visibility visibility = Visibility.Visible;
		Visibility visibility2 = Visibility.Hidden;
		Visibility visibility3 = Visibility.Visible;
		Visibility visibility4 = Visibility.Hidden;
		Visibility visibility5 = Visibility.Visible;
		Visibility visibility6 = Visibility.Hidden;
		switch (text)
		{
		case "Archer":
			if (GameData.Instance.lastGameState.troop_types_available[0] > 0)
			{
				lastTroopBuildChimp = Enums.eChimps.CHIMP_TYPE_ARCHER;
				Show_BarracksBows2 = true;
				setCreateTroopText();
				if (!Director.instance.MultiplayerGame || GameData.Instance.lastGameState.MP_TroopsCostGold)
				{
					HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Visible;
				}
			}
			break;
		case "Spearman":
			if (GameData.Instance.lastGameState.troop_types_available[2] > 0)
			{
				lastTroopBuildChimp = Enums.eChimps.CHIMP_TYPE_SPEARMAN;
				Show_BarracksSpears2 = true;
				setCreateTroopText();
				if (!Director.instance.MultiplayerGame || GameData.Instance.lastGameState.MP_TroopsCostGold)
				{
					HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Visible;
				}
			}
			break;
		case "Maceman":
			if (GameData.Instance.lastGameState.troop_types_available[4] > 0)
			{
				lastTroopBuildChimp = Enums.eChimps.CHIMP_TYPE_MACEMAN;
				Show_BarracksMaces2 = true;
				Show_BarracksLeatherArmour2 = true;
				setCreateTroopText();
				if (!Director.instance.MultiplayerGame || GameData.Instance.lastGameState.MP_TroopsCostGold)
				{
					HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Visible;
				}
			}
			break;
		case "XBowman":
			if (GameData.Instance.lastGameState.troop_types_available[1] > 0)
			{
				lastTroopBuildChimp = Enums.eChimps.CHIMP_TYPE_XBOWMAN;
				Show_BarracksXBows2 = true;
				Show_BarracksLeatherArmour2 = true;
				setCreateTroopText();
				if (!Director.instance.MultiplayerGame || GameData.Instance.lastGameState.MP_TroopsCostGold)
				{
					HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Visible;
				}
			}
			break;
		case "Pikeman":
			if (GameData.Instance.lastGameState.troop_types_available[3] > 0)
			{
				lastTroopBuildChimp = Enums.eChimps.CHIMP_TYPE_PIKEMAN;
				Show_BarracksPikes2 = true;
				Show_BarracksArmour2 = true;
				setCreateTroopText();
				if (!Director.instance.MultiplayerGame || GameData.Instance.lastGameState.MP_TroopsCostGold)
				{
					HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Visible;
				}
			}
			break;
		case "Swordsman":
			if (GameData.Instance.lastGameState.troop_types_available[5] > 0)
			{
				lastTroopBuildChimp = Enums.eChimps.CHIMP_TYPE_SWORDSMAN;
				Show_BarracksSwords2 = true;
				Show_BarracksArmour2 = true;
				setCreateTroopText();
				if (!Director.instance.MultiplayerGame || GameData.Instance.lastGameState.MP_TroopsCostGold)
				{
					HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Visible;
				}
			}
			break;
		case "Knight":
			if (GameData.Instance.lastGameState.troop_types_available[6] > 0)
			{
				lastTroopBuildChimp = Enums.eChimps.CHIMP_TYPE_KNIGHT;
				Show_BarracksSwords2 = true;
				Show_BarracksArmour2 = true;
				Show_BarracksHorses2 = true;
				setCreateTroopText();
				if (!Director.instance.MultiplayerGame || GameData.Instance.lastGameState.MP_TroopsCostGold)
				{
					HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Visible;
				}
			}
			break;
		case "Engineer":
			lastTroopBuildChimp = Enums.eChimps.CHIMP_TYPE_ENGINEER;
			TroopNameCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 30);
			TroopCostText = " " + (GameData.getChimpGoldCost(30) * lastTroopsAmountToMake).ToString().ToString() + "  ";
			if (lastTroopsAmountToMake > 1)
			{
				TroopNameCostText = TroopNameCostText + " x" + lastTroopsAmountToMake + "  ";
			}
			TroopNameCostText += "  ";
			HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Visible;
			TroopShiftCostText = "[" + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 78).ToUpper() + "] x" + lastTroopsAmountToMakex5;
			TroopCtrlCostText = "[" + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 79).ToUpper() + "] x" + lastTroopsAmountToMakeMax;
			break;
		case "Ladderman":
			lastTroopBuildChimp = Enums.eChimps.CHIMP_TYPE_LADDERMAN;
			TroopNameCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 29);
			TroopCostText = " " + (GameData.getChimpGoldCost(29) * lastTroopsAmountToMake).ToString().ToString() + "  ";
			if (lastTroopsAmountToMake > 1)
			{
				TroopNameCostText = TroopNameCostText + " x" + lastTroopsAmountToMake + "  ";
			}
			TroopNameCostText += "  ";
			HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Visible;
			TroopShiftCostText = "[" + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 78).ToUpper() + "] x" + lastTroopsAmountToMakex5;
			TroopCtrlCostText = "[" + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 79).ToUpper() + "] x" + lastTroopsAmountToMakeMax;
			break;
		case "Tunneller":
			lastTroopBuildChimp = Enums.eChimps.CHIMP_TYPE_TUNNELER;
			TroopNameCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, 5);
			TroopCostText = " " + (GameData.getChimpGoldCost(5) * lastTroopsAmountToMake).ToString().ToString() + "  ";
			if (lastTroopsAmountToMake > 1)
			{
				TroopNameCostText = TroopNameCostText + " x" + lastTroopsAmountToMake;
			}
			TroopNameCostText += "  ";
			HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Visible;
			TroopShiftCostText = "[" + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 78).ToUpper() + "] x" + lastTroopsAmountToMakex5;
			TroopCtrlCostText = "[" + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_HOT_KEYS, 79).ToUpper() + "] x" + lastTroopsAmountToMakeMax;
			break;
		case "Bow":
			HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Hidden;
			TroopNameCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, 17);
			break;
		case "Spear":
			HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Hidden;
			TroopNameCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, 19);
			break;
		case "Mace":
			HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Hidden;
			TroopNameCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, 21);
			break;
		case "XBow":
			HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Hidden;
			TroopNameCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, 18);
			break;
		case "Pike":
			HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Hidden;
			TroopNameCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, 20);
			break;
		case "Sword":
			HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Hidden;
			TroopNameCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, 22);
			break;
		case "Leather":
			HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Hidden;
			TroopNameCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, 23);
			break;
		case "Armour":
			HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Hidden;
			TroopNameCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, 24);
			break;
		case "Horse":
			HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Hidden;
			TroopNameCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, 224);
			break;
		case "Assembly1":
			HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Hidden;
			TroopCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 86) + " ";
			visibility = Visibility.Hidden;
			visibility2 = Visibility.Visible;
			break;
		case "Assembly2":
			HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Hidden;
			TroopCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 87) + " ";
			visibility = Visibility.Hidden;
			visibility2 = Visibility.Visible;
			break;
		case "Assembly3":
			HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Hidden;
			TroopCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 88) + " ";
			visibility = Visibility.Hidden;
			visibility2 = Visibility.Visible;
			break;
		case "Assembly4":
			HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Hidden;
			TroopCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 89) + " ";
			visibility = Visibility.Hidden;
			visibility2 = Visibility.Visible;
			break;
		case "Assembly5":
			HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Hidden;
			TroopCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 90) + " ";
			visibility = Visibility.Hidden;
			visibility2 = Visibility.Visible;
			break;
		case "Assembly6":
			HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Hidden;
			TroopCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 91) + " ";
			visibility = Visibility.Hidden;
			visibility2 = Visibility.Visible;
			break;
		case "Assembly7":
			HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Hidden;
			TroopCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 92) + "  ";
			visibility = Visibility.Hidden;
			visibility2 = Visibility.Visible;
			break;
		case "AssemblyE1":
			TroopNameCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 93) + "  ";
			visibility3 = Visibility.Hidden;
			visibility4 = Visibility.Visible;
			break;
		case "AssemblyE2":
			TroopNameCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 94) + "  ";
			visibility3 = Visibility.Hidden;
			visibility4 = Visibility.Visible;
			break;
		case "AssemblyT1":
			TroopNameCostText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 95) + "  ";
			visibility5 = Visibility.Hidden;
			visibility6 = Visibility.Visible;
			break;
		}
		HUDBuildingPanel.RefTroopHelpText.Visibility = visibility2;
		HUDBuildingPanel.RefTroopNameText.Visibility = visibility;
		HUDBuildingPanel.RefEngineersCostsText.Visibility = visibility3;
		HUDBuildingPanel.RefEngineersHelpText.Visibility = visibility4;
		HUDBuildingPanel.RefTunellersCostsText.Visibility = visibility5;
		HUDBuildingPanel.RefTunellersHelpText.Visibility = visibility6;
	}

	private void ButtonLeaveCreateTroop(object parameter)
	{
		lastTroopBuildOver = "";
		lastTroopBuildChimp = Enums.eChimps.CHIMP_NUM_TYPES;
		Show_BarracksBows2 = false;
		Show_BarracksSpears2 = false;
		Show_BarracksMaces2 = false;
		Show_BarracksXBows2 = false;
		Show_BarracksPikes2 = false;
		Show_BarracksSwords2 = false;
		Show_BarracksLeatherArmour2 = false;
		Show_BarracksArmour2 = false;
		Show_BarracksHorses2 = false;
		TroopCostText = "0";
		TroopShiftCostText = "";
		TroopCtrlCostText = "";
		HUDBuildingPanel.RefTroopNameText.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefTroopHelpText.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefTroopCostsText.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefEngineersCostsText.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefEngineersHelpText.Visibility = Visibility.Hidden;
		HUDBuildingPanel.RefTunellersCostsText.Visibility = Visibility.Hidden;
	}

	private void ButtonGoToHelpScreen(object parameter)
	{
		HUD_Help.OpenHelpForCurrentBuildingOrChimmp();
	}

	private void ButtonToggleZZZMode(object parameter)
	{
		EngineInterface.GameAction(Enums.GameActionCommand.ToggleSleep, GameData.Instance.lastGameState.in_structure, 0);
	}

	private void ButtonChangeRations(object parameter)
	{
		int num = Convert.ToInt32(parameter as string);
		EngineInterface.GameAction(Enums.GameActionCommand.SetRationing, GameData.Instance.lastGameState.in_structure, num);
		InGranarySetRationHand(num, 0);
	}

	private void ButtonGranarySwitchMode(object parameter)
	{
		if (GameData.Instance.app_sub_mode == 75)
		{
			if (WasInGranary)
			{
				EditorDirector.instance.directSetAppSubMode(4);
				setUpInbuilding(4, 19);
			}
			else
			{
				EditorDirector.instance.directSetAppSubMode(71);
				setUpInbuilding(71, 0);
			}
			WasInGranary = false;
		}
		else
		{
			WasInGranary = true;
			EditorDirector.instance.directSetAppSubMode(75);
			setUpInbuilding(0, 0);
		}
	}

	private void ButtonChangeEdibleState(object parameter)
	{
		int num = Convert.ToInt32(parameter as string);
		EngineInterface.GameAction(Enums.GameActionCommand.SetFoodEaten, GameData.Instance.lastGameState.in_structure, num);
		if (foodNotEdible[num] == 0)
		{
			foodNotEdible[num] = 1;
		}
		else
		{
			foodNotEdible[num] = 0;
		}
		InFoodReportShowEaten(0);
	}

	private void ButtonAdjustTax(object parameter)
	{
		int num = Convert.ToInt32(parameter as string);
		int tax_rate = GameData.Instance.lastGameState.tax_rate;
		tax_rate = num switch
		{
			-2 => tax_rate - 1, 
			-1 => tax_rate + 1, 
			_ => num, 
		};
		if (tax_rate < 0)
		{
			tax_rate = 0;
		}
		if (tax_rate >= 10)
		{
			tax_rate = 9;
		}
		EngineInterface.GameAction(Enums.GameActionCommand.SetTaxRate, GameData.Instance.lastGameState.in_structure, tax_rate);
	}

	private void ButtonGateControls(object parameter)
	{
		int num = ((Convert.ToInt32(parameter as string) != 1) ? 11 : 10);
		EngineInterface.GameAction(Enums.GameActionCommand.GateHouseState, GameData.Instance.lastGameState.in_structure, num);
		GameData.Instance.lastGameState.gatehouse_state = num;
		HUDBuildingPanel.UpdateButtonState();
	}

	private void ButtonDrawbridgeControls(object parameter)
	{
		int num = ((Convert.ToInt32(parameter as string) != 2) ? 10 : 11);
		EngineInterface.GameAction(Enums.GameActionCommand.DrawbridgeState, GameData.Instance.lastGameState.in_structure, num);
		GameData.Instance.lastGameState.gatehouse_state = num;
		HUDBuildingPanel.UpdateButtonState();
	}

	private void ButtonRepairFunction(object parameter)
	{
		EngineInterface.GameAction(Enums.GameActionCommand.RepairBuilding, GameData.Instance.lastGameState.in_structure, 0);
	}

	private void ButtonReleaseDogs(object parameter)
	{
		EngineInterface.GameAction(Enums.GameActionCommand.OpenDogCage, GameData.Instance.lastGameState.in_structure, 0);
		HUDBuildingPanel.UpdateButtonState();
	}

	private void ButtonRepairMouseEnterFunction(object parameter)
	{
		HUDmain.SetRolloverOtherString(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_REPAIR), 1);
		CommonRedButtonEnter(null, null);
	}

	private void ButtonRepairMouseLeaveFunction(object parameter)
	{
		HUDmain.SetRolloverOtherString("");
	}

	private void ButtonMakeWeaponMouseEnterFunction(object parameter)
	{
		string text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 4) + " ";
		switch ((string)parameter)
		{
		case "ProducingBows":
			text += Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 7);
			break;
		case "ProducingXBows":
			text += Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 8);
			break;
		case "ProducingSpears":
			text += Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 9);
			break;
		case "ProducingPikes":
			text += Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 10);
			break;
		case "ProducingSwords":
			text += Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 5);
			break;
		case "ProducingMaces":
			text += Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_BLACKSMITHS_WORKSHOP, 6);
			break;
		case "GotoToTradeRaw":
			text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_TRADEPOST, 3);
			break;
		case "GotoToTradeFood":
			text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_TRADEPOST, 2);
			break;
		case "GotoToTradeWeapons":
			text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_IN_TRADEPOST, 4);
			break;
		}
		HUDmain.SetRolloverOtherString(text, 1);
	}

	private void ButtonMakeWeaponMouseLeaveFunction(object parameter)
	{
		HUDmain.SetRolloverOtherString("");
	}

	private void ButtonChangeWorkshopOutput(object parameter)
	{
		Enums.Goods goodsEnum = getGoodsEnum(parameter as string);
		EngineInterface.GameAction(Enums.GameActionCommand.SetNextWeaponMade, GameData.Instance.lastGameState.in_structure, (int)goodsEnum);
	}

	private void ButtonChangeStance(object parameter)
	{
		int num = Convert.ToInt32(parameter as string);
		int state = 0;
		switch (num)
		{
		case 1:
			state = 287;
			break;
		case 2:
			state = 288;
			break;
		case 3:
			state = 289;
			break;
		}
		EngineInterface.GameAction(Enums.GameActionCommand.Troops_ChangeStance, -1, state);
	}

	private void ButtonUnitDisband(object parameter)
	{
		EngineInterface.GameAction(Enums.GameActionCommand.Troops_Disband, 0, 0);
	}

	private void ButtonUnitBuildCat(object parameter)
	{
		EngineInterface.GameAction(Enums.GameActionCommand.EngBuild_Catapult, 0, 0);
	}

	private void ButtonUnitStop(object parameter)
	{
		EngineInterface.GameAction(Enums.GameActionCommand.Troops_Stop, 0, 0);
	}

	private void ButtonUnitBuildTreb(object parameter)
	{
		EngineInterface.GameAction(Enums.GameActionCommand.EngBuild_Trebuchet, 0, 0);
	}

	private void ButtonUnitPatrol(object parameter)
	{
		EngineInterface.GameAction(Enums.GameActionCommand.Troops_Patrol, 0, 0);
		if (GameData.Instance.lastGameState.troops_patrol_mode == 0)
		{
			GameData.Instance.lastGameState.troops_patrol_mode = 1;
		}
		else
		{
			GameData.Instance.lastGameState.troops_patrol_mode = 0;
		}
	}

	private void ButtonUnitBuildTwr(object parameter)
	{
		EngineInterface.GameAction(Enums.GameActionCommand.EngBuild_SiegeTower, 0, 0);
	}

	private void ButtonUnitAttackHere(object parameter)
	{
		EngineInterface.GameAction(Enums.GameActionCommand.Troops_AttackHere, 0, 0);
	}

	private void ButtonUnitBuildTunnel(object parameter)
	{
		EngineInterface.GameAction(Enums.GameActionCommand.Troops_AttackHere, 0, 0);
	}

	private void ButtonUnitPourOil(object parameter)
	{
		EngineInterface.GameAction(Enums.GameActionCommand.Troops_AttackHere, 0, 0);
	}

	private void ButtonUnitBuildRam(object parameter)
	{
		EngineInterface.GameAction(Enums.GameActionCommand.EngBuild_BatteringRam, 0, 0);
	}

	private void ButtonUnitBuildSiegeKit(object parameter)
	{
		HUDTroopPanel.SelectedEngiBuild(state: true);
	}

	private void ButtonUnitBuildMantlet(object parameter)
	{
		EngineInterface.GameAction(Enums.GameActionCommand.EngBuild_Shield, 0, 0);
	}

	private void ButtonUnitRechargeRock(object parameter)
	{
		EngineInterface.GameAction(Enums.GameActionCommand.AmmoRecharge, 0, 0);
	}

	private void ButtonUnitLaunchCow(object parameter)
	{
		EngineInterface.GameAction(Enums.GameActionCommand.Troops_Cow, 0, 0);
	}

	private void ButtonUnitback(object parameter)
	{
		HUDTroopPanel.SelectedEngiBuild(state: false);
	}

	public void ShowSiegeEnginePlacementRollover(int structure)
	{
		switch (structure)
		{
		case 80:
			ButtonTroopPanelMouseEnter("UnitBuildCat");
			return;
		case 82:
			ButtonTroopPanelMouseEnter("UnitBuildTower");
			return;
		case 81:
			ButtonTroopPanelMouseEnter("UnitBuildTreb");
			return;
		case 83:
			ButtonTroopPanelMouseEnter("UnitBuildRam");
			return;
		case 84:
			ButtonTroopPanelMouseEnter("UnitbuildMantlet");
			return;
		}
		if (xThreadClearTroopRollover)
		{
			ButtonTroopPanelMouseLeave("");
			xThreadClearTroopRollover = false;
		}
	}

	public void CrossThreadSiegeEngineRollover(int structure)
	{
		if (structure == 0 && xThreadSiegeStruct > 0)
		{
			xThreadClearTroopRollover = true;
		}
		xThreadSiegeStruct = structure;
		xThreadStruct = 0;
		xThreadString = "";
	}

	public void CrossThreadRollover(int structure, string structString)
	{
		if (xThreadSiegeStruct > 0)
		{
			xThreadClearTroopRollover = true;
		}
		xThreadSiegeStruct = 0;
		xThreadStruct = structure;
		xThreadString = structString;
	}

	public void CrossThreadRolloverUpdate()
	{
		if (Show_HUD_Troops)
		{
			ShowSiegeEnginePlacementRollover(xThreadSiegeStruct);
		}
		else
		{
			HUDmain.SetRolloverSelected(xThreadStruct, xThreadString);
		}
	}

	private void ButtonTroopPanelToggleCG(object parameter)
	{
		HUD_ControlGroups.ToggleMenu();
	}

	private void ButtonControlGroupClick(object parameter)
	{
		HUDControlGroups.ButtonClicked(parameter as string);
	}

	private void ButtonTroopPanelPageClick(object parameter)
	{
		HUDTroopPanel.TogglePages(parameter as string);
	}

	private void ButtonTroopPanelMouseEnter(object parameter)
	{
		bool flag = false;
		switch ((string)parameter)
		{
		case "UnitBuildCat":
		case "UnitBuildTreb":
		case "UnitBuildTower":
		case "UnitBuildRam":
		case "UnitbuildMantlet":
		{
			int wood = 0;
			int stone = 0;
			int iron = 0;
			int pitch = 0;
			int gold = 0;
			TroopsPanelRollover_GoodsImage1 = GameSprites[72];
			TroopsPanelRollover_AmountGot1 = "(" + GameData.Instance.lastGameState.resources[15] + ")";
			switch ((string)parameter)
			{
			case "UnitBuildCat":
				GameData.getStructureCosts(80, ref wood, ref stone, ref iron, ref pitch, ref gold);
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.TEXT_SCN_ACTION7);
				break;
			case "UnitBuildTreb":
				GameData.getStructureCosts(81, ref wood, ref stone, ref iron, ref pitch, ref gold);
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.TEXT_SCN_ACTION8);
				break;
			case "UnitBuildTower":
				GameData.getStructureCosts(82, ref wood, ref stone, ref iron, ref pitch, ref gold);
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.TEXT_SCN_ACTION9);
				break;
			case "UnitBuildRam":
				GameData.getStructureCosts(83, ref wood, ref stone, ref iron, ref pitch, ref gold);
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.TEXT_SCN_ACTION10);
				break;
			case "UnitbuildMantlet":
				GameData.getStructureCosts(84, ref wood, ref stone, ref iron, ref pitch, ref gold);
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.TEXT_SCN_ACTION11);
				break;
			}
			TroopsPanelRollover_AmountReq1 = "   " + gold;
			break;
		}
		default:
			TroopsPanelRollover_AmountGot1 = "";
			TroopsPanelRollover_AmountReq1 = "";
			TroopsPanelRollover_GoodsImage1 = null;
			switch ((string)parameter)
			{
			case "GuardStanceButton":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_STANCE_STAND);
				break;
			case "DefensiveStanceButton":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_STANCE_DEFENSIVE);
				break;
			case "AggressiveStanceButton":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_STANCE_AGGRESSIVE);
				break;
			case "UnitDisband":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.TEXT_SCN_EVENT_CONDITION30);
				break;
			case "UnitStop":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_STOP);
				break;
			case "UnitPatrol":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.TEXT_SCN_EVENT_CONDITION29);
				TroopsPanelRollover_AmountGot1 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 123);
				flag = true;
				break;
			case "UnitAttackHere":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.TEXT_SCN_EVENT_CONDITION32);
				break;
			case "UnitTunnelHere":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.TEXT_SCN_EVENT_CONDITION31);
				break;
			case "UnitPourOil":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.TEXT_SCN_EVENT_CONDITION28);
				break;
			case "UnitBuild":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_BUILD);
				break;
			case "UnitFireCow":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.TEXT_SCN_EVENT_CONDITION33);
				break;
			case "UnitBack":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_BUILD_BACK);
				break;
			case "ArchersSelected":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_SELECT_ARCHERS);
				break;
			case "SpearmenSelected":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_SELECT_SPEARMEN);
				break;
			case "MacemenSelected":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_SELECT_MACEMEN);
				break;
			case "XBowmenSelected":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_SELECT_XBOWMEN);
				break;
			case "PikemenSelected":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_SELECT_PIKEMEN);
				break;
			case "SwordsmenSelected":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_SELECT_SWORDSMEN);
				break;
			case "KnightsSelected":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_SELECT_KNIGHTS);
				break;
			case "EngineersSelected":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_SELECT_ENGINEERS);
				break;
			case "MonksSelected":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_SELECT_MONKS);
				break;
			case "LaddermenSelected":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_SELECT_LADDERMEN);
				break;
			case "UnitReload":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_AMMO);
				break;
			case "TunnelersSelected":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_SELECT_TUNNELERS);
				break;
			case "CatapultsSelected":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_SELECT_CATAPULTS);
				break;
			case "TrebuchetsSelected":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_SELECT_TREBUCHETS);
				break;
			case "RamsSelected":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_SELECT_BATTERINGRAMS);
				break;
			case "SiegeTowersSelected":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_SELECT_SIEGETOWERS);
				break;
			case "MantletsSelected":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_SELECT_PORTABLESHIELDS);
				break;
			case "MangonelsSelected":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_SELECT_MANGONELS);
				break;
			case "BalistaeSelected":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_SELECT_BALLISTAS);
				break;
			case "ToggleControlGroups":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 149);
				break;
			case "CG1_Select":
			case "CG2_Select":
			case "CG3_Select":
			case "CG4_Select":
			case "CG5_Select":
			case "CG6_Select":
			case "CG7_Select":
			case "CG8_Select":
			case "CG9_Select":
			case "CG0_Select":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 153);
				break;
			case "CG1_Create":
			case "CG2_Create":
			case "CG3_Create":
			case "CG4_Create":
			case "CG5_Create":
			case "CG6_Create":
			case "CG7_Create":
			case "CG8_Create":
			case "CG9_Create":
			case "CG0_Create":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 150);
				break;
			case "CG1_Add":
			case "CG2_Add":
			case "CG3_Add":
			case "CG4_Add":
			case "CG5_Add":
			case "CG6_Add":
			case "CG7_Add":
			case "CG8_Add":
			case "CG9_Add":
			case "CG0_Add":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 151);
				break;
			case "CG1_Delete":
			case "CG2_Delete":
			case "CG3_Delete":
			case "CG4_Delete":
			case "CG5_Delete":
			case "CG6_Delete":
			case "CG7_Delete":
			case "CG8_Delete":
			case "CG9_Delete":
			case "CG0_Delete":
				TroopsPanelRollover = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 152);
				break;
			}
			break;
		}
		if (flag)
		{
			HUDTroopPanel.RefTroopsPanelRollover.Visibility = Visibility.Hidden;
			HUDTroopPanel.RefTroopsPanelRollover2.Visibility = Visibility.Visible;
		}
		else
		{
			HUDTroopPanel.RefTroopsPanelRollover.Visibility = Visibility.Visible;
			HUDTroopPanel.RefTroopsPanelRollover2.Visibility = Visibility.Hidden;
		}
	}

	private void ButtonTroopPanelMouseLeave(object parameter)
	{
		HUDTroopPanel.RefTroopsPanelRollover.Visibility = Visibility.Hidden;
		HUDTroopPanel.RefTroopsPanelRollover2.Visibility = Visibility.Hidden;
	}

	public void ButtonNewTradeType(object parameter)
	{
		int num = Convert.ToInt32(parameter as string);
		if (num == 0)
		{
			if (HUDBuildingPanel.RefTradePost_Trade_Auto.Visibility == Visibility.Visible)
			{
				EngineInterface.GameAction(Enums.GameActionCommand.Autotrade_Apply, 0, 0);
				EngineInterface.GameAction(Enums.GameActionCommand.Autotrade_Pause, 0, 0);
				HUDBuildingPanel.UpdateAutoTradeSelectButton(lastState: true);
				HUDBuildingPanel.RefTradePost_Trade_Auto.Visibility = Visibility.Hidden;
				HUDBuildingPanel.RefTradePost_Trade_Normal.Visibility = Visibility.Visible;
				return;
			}
			if (GameData.Instance.app_sub_mode == 54 || GameData.Instance.app_sub_mode == 55 || GameData.Instance.app_sub_mode == 56 || GameData.Instance.app_sub_mode == 53 || previousTradePanel == -10)
			{
				previousTradePanel = 0;
				EditorDirector.instance.directSetAppSubMode(25);
				setUpInbuilding(25, 26);
				return;
			}
			num = previousTradePanel;
		}
		switch (num)
		{
		case 1:
			previousTradePanel = 1;
			EditorDirector.instance.directSetAppSubMode(54);
			setUpInbuilding(54, 26);
			break;
		case 2:
			previousTradePanel = 2;
			EditorDirector.instance.directSetAppSubMode(55);
			setUpInbuilding(55, 26);
			break;
		case 3:
			previousTradePanel = 3;
			EditorDirector.instance.directSetAppSubMode(56);
			setUpInbuilding(56, 26);
			break;
		case 4:
			previousTradePanel = 4;
			EditorDirector.instance.directSetAppSubMode(53);
			setUpInbuilding(53, 26);
			break;
		}
	}

	private void ButtonNewTradeGoodsType(object parameter)
	{
		int state = Convert.ToInt32(parameter as string);
		EditorDirector.instance.directSetAppSubMode(57);
		setUpInbuilding(57, 26);
		EngineInterface.GameAction(Enums.GameActionCommand.SetCurrentTradedGood, GameData.Instance.lastGameState.in_structure, state);
	}

	private void ButtonCycleTradeGoodsType(object parameter)
	{
		if (Convert.ToInt32(parameter as string) == 0)
		{
			EngineInterface.GameAction(Enums.GameActionCommand.SetCurrentTradedGood, GameData.Instance.lastGameState.in_structure, GameData.Instance.lastGameState.trading_prev_goods);
			if (HUDBuildingPanel.RefTradePost_Trade_Auto.Visibility == Visibility.Visible)
			{
				EngineInterface.GameAction(Enums.GameActionCommand.Autotrade_Apply, 0, 0);
				HUDBuildingPanel.initAutoTrade(GameData.Instance.lastGameState.trading_prev_goods);
			}
		}
		else
		{
			EngineInterface.GameAction(Enums.GameActionCommand.SetCurrentTradedGood, GameData.Instance.lastGameState.in_structure, GameData.Instance.lastGameState.trading_next_goods);
			if (HUDBuildingPanel.RefTradePost_Trade_Auto.Visibility == Visibility.Visible)
			{
				EngineInterface.GameAction(Enums.GameActionCommand.Autotrade_Apply, 0, 0);
				HUDBuildingPanel.initAutoTrade(GameData.Instance.lastGameState.trading_next_goods);
			}
		}
	}

	private void ButtonBuySell(object parameter)
	{
		int num = Convert.ToInt32(parameter as string);
		switch (num)
		{
		case 100:
			HUDBuildingPanel.RefTradePost_Trade_Normal.Visibility = Visibility.Hidden;
			HUDBuildingPanel.RefTradePost_Trade_Auto.Visibility = Visibility.Visible;
			HUDBuildingPanel.initAutoTrade();
			EngineInterface.GameAction(Enums.GameActionCommand.Autotrade_Pause, 1, 1);
			return;
		case 101:
			HUDBuildingPanel.toggleAutoTrade();
			return;
		}
		int num2 = 0;
		int structureID = 0;
		if (KeyManager.instance.isShiftDown())
		{
			structureID = 1;
		}
		switch (num)
		{
		case 1:
			num2 = EngineInterface.GameAction(Enums.GameActionCommand.BuyGoods, structureID, GameData.Instance.lastGameState.trading_current_goods);
			break;
		case 2:
			num2 = EngineInterface.GameAction(Enums.GameActionCommand.SellGoods, structureID, GameData.Instance.lastGameState.trading_current_goods);
			break;
		}
		if (num2 > 0)
		{
			if (num2 == 9)
			{
				TradeErrorText = Translate.Instance.lookUpText("TEXT_IN_TRADEPOST_010");
			}
			if (num2 == 10)
			{
				TradeErrorText = Translate.Instance.lookUpText("TEXT_IN_TRADEPOST_011");
			}
			if (num2 == 13)
			{
				TradeErrorText = Translate.Instance.lookUpText("TEXT_IN_TRADEPOST_014");
			}
			if (num2 == 14)
			{
				TradeErrorText = Translate.Instance.lookUpText("TEXT_IN_TRADEPOST_015");
			}
			if (num2 == 15)
			{
				TradeErrorText = Translate.Instance.lookUpText("TEXT_IN_TRADEPOST_016");
			}
			if (num2 == 16)
			{
				TradeErrorText = Translate.Instance.lookUpText("TEXT_IN_TRADEPOST_017");
			}
			if (num2 == 17)
			{
				TradeErrorText = Translate.Instance.lookUpText("TEXT_IN_TRADEPOST_018");
			}
			if (num2 == 18)
			{
				TradeErrorText = Translate.Instance.lookUpText("TEXT_IN_TRADEPOST_019");
			}
			HUDBuildingPanel.RefTradeErrorAnination.Begin();
		}
		else
		{
			TradeErrorText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 130);
		}
	}

	private void ButtonReports(object parameter)
	{
		int num = Convert.ToInt32(parameter as string);
		WasInGranary = false;
		if (FreezeMainControls)
		{
			if (GameData.Instance.app_mode == 16 && (GameData.Instance.app_sub_mode == 76 || GameData.Instance.app_sub_mode == 68))
			{
				EditorDirector.instance.directSetAppMode(14, 0);
				return;
			}
			EngineInterface.GameAction(Enums.GameActionCommand.CloseTroopsPanel, 0, 0);
			num = 9;
			PropEx.SetButtonVisibility(HUDBuildingPanel.RefButtonArmyReportBack, Visibility.Hidden);
			PropEx.SetButtonVisibility(HUDBuildingPanel.RefButtonArmyReportBack2, Visibility.Hidden);
		}
		else
		{
			PropEx.SetButtonVisibility(HUDBuildingPanel.RefButtonArmyReportBack, Visibility.Visible);
			PropEx.SetButtonVisibility(HUDBuildingPanel.RefButtonArmyReportBack2, Visibility.Visible);
			EngineInterface.GameAction(Enums.GameActionCommand.CloseTroopsPanel, 0, 0);
		}
		switch (num)
		{
		case 0:
			EditorDirector.instance.directSetAppMode(16, 71);
			break;
		case 1:
			EditorDirector.instance.directSetAppSubMode(72);
			break;
		case 2:
			EditorDirector.instance.directSetAppSubMode(73);
			break;
		case 3:
			EditorDirector.instance.directSetAppSubMode(74);
			break;
		case 4:
			EditorDirector.instance.directSetAppSubMode(75);
			break;
		case 5:
			EditorDirector.instance.directSetAppSubMode(76);
			break;
		case 6:
			EditorDirector.instance.directSetAppSubMode(77);
			break;
		case 7:
			EditorDirector.instance.directSetAppSubMode(78);
			break;
		case 8:
			EditorDirector.instance.directSetAppSubMode(79);
			break;
		case 9:
			EditorDirector.instance.directSetAppMode(16, 76);
			break;
		}
		if (num == 0)
		{
			InBuildingGameAction();
		}
		switch (num)
		{
		case 0:
			setUpInbuilding(71, 190);
			break;
		case 1:
			setUpInbuilding(0, 190);
			break;
		case 2:
			setUpInbuilding(0, 190);
			break;
		case 3:
			setUpInbuilding(0, 190);
			break;
		case 4:
			setUpInbuilding(0, 190);
			break;
		case 5:
			setUpInbuilding(0, 190);
			break;
		case 6:
			setUpInbuilding(0, 190);
			break;
		case 7:
			setUpInbuilding(0, 190);
			break;
		case 8:
			setUpInbuilding(0, 190);
			break;
		case 9:
			setUpInbuilding(76, 190);
			break;
		}
	}

	private void ButtonScribeMouseEnterFunction(object parameter)
	{
		HUDmain.SetRolloverOtherString(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.TEXT_SCN_HELP), 1);
	}

	private void ButtonScribeMouseLeaveFunction(object parameter)
	{
		HUDmain.SetRolloverOtherString("");
	}

	private void ButtonReturnToReports(object parameter)
	{
		EditorDirector.instance.directSetAppSubMode(71);
		setUpInbuilding(71, 0);
	}

	private void ButtonReportsViewEvents(object parameter)
	{
		if (Convert.ToInt32(parameter as string) == 0)
		{
			EditorDirector.instance.directSetAppSubMode(69);
			setUpInbuilding(69, 0);
		}
		else
		{
			EditorDirector.instance.directSetAppSubMode(72);
			setUpInbuilding(72, 0);
		}
	}

	private void ButtonToggleArmyReport(object parameter)
	{
		if (Convert.ToInt32(parameter as string) == 0)
		{
			EditorDirector.instance.directSetAppSubMode(76);
			setUpInbuilding(76, 190);
		}
		else
		{
			EditorDirector.instance.directSetAppSubMode(68);
			setUpInbuilding(68, 190);
		}
	}

	private void ButtonActionPoint(object parameter)
	{
		if (GameData.Instance.lastGameState != null && GameData.Instance.lastGameState.action_point_count > 0)
		{
			EngineInterface.GameAction(Enums.GameActionCommand.ActionPointClicked, 0, 0);
			GameData.Instance.lastGameState.action_point_count = 0;
			Show_ActionPoint = false;
		}
	}

	private void ButtonRadarZoomFunction(object parameter)
	{
		if ((string)parameter == "1")
		{
			GameMap.instance.changeRadarMapSize(-64);
		}
		else
		{
			GameMap.instance.changeRadarMapSize(64);
		}
	}

	private void ButtonGotoBriefing(object parameter)
	{
		if (IsMapEditorMode || GameData.Instance.game_type == 4)
		{
			return;
		}
		ResizeBriefingScreen();
		BriefingFromStory = (string)parameter == "FromStory";
		Show_HUD_Main = false;
		Show_HUD_Troops = false;
		Show_HUD_Building = false;
		Show_HUD_Briefing = true;
		Show_HUD_MissionOver = false;
		Show_HUD_Book = false;
		IsMapEditorMode = false;
		BriefingMode = 1;
		MyAudioManager.Instance.PauseSpeech(state: true);
		MyAudioManager.Instance.StopSFX();
		BriefingMissionTitle = "";
		if (!Director.instance.MultiplayerGame)
		{
			preBriefingWasPaused = Director.instance.Paused;
			if (!preBriefingWasPaused)
			{
				Director.instance.SetPausedState(state: true);
			}
		}
		FatControler.instance.BriefingUIUpdate();
		HUDRoot.RefRadarMapGrid.Visibility = Visibility.Hidden;
		HUDRoot.RefReportsControlGrid.Visibility = Visibility.Hidden;
		HUDBriefingPanel.RefObjectivesSubPanel.Visibility = Visibility.Visible;
		HUDBriefingPanel.RefHintsSubPanel.Visibility = Visibility.Hidden;
		HUDBriefingPanel.RefHelpSubPanel.Visibility = Visibility.Hidden;
		if (!ConfigSettings.Settings_UseSteamOverlayForHelp)
		{
			HUDBriefingPanel.OpenHelp();
		}
		HUDBriefingPanel.RefBriefingStrategySection.Visibility = Visibility.Visible;
		if (GameData.Instance.game_type == 0 || GameData.Instance.game_type == 5)
		{
			if (GameData.Instance.game_type == 0)
			{
				BriefingMissionTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MISSION_NAMES, GameData.Instance.mission_level);
			}
			else
			{
				BriefingMissionTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MISSION_NAMES, GameData.Instance.mission_level - 11);
			}
			if (BriefingFromStory)
			{
				switch (GameData.Instance.mission_level)
				{
				case 1:
					HUDBriefingPanel.PlayStorySpeech("M1_Brief1.webm");
					break;
				case 2:
					HUDBriefingPanel.PlayStorySpeech("M2_Brief1.webm");
					break;
				case 3:
					HUDBriefingPanel.PlayStorySpeech("M3_Brief1.webm");
					break;
				case 4:
					HUDBriefingPanel.PlayStorySpeech("M4_Brief1.webm");
					break;
				case 5:
					HUDBriefingPanel.PlayStorySpeech("M5_Brief1.webm");
					break;
				case 6:
					if (GameData.Instance.mission6Prestart)
					{
						HUDBriefingPanel.PlayStorySpeech("M6_Brief1.webm");
					}
					else
					{
						HUDBriefingPanel.PlayStorySpeech("M6_Brief2.webm");
					}
					break;
				case 7:
					HUDBriefingPanel.PlayStorySpeech("M7_Brief1.webm");
					break;
				case 8:
					HUDBriefingPanel.PlayStorySpeech("M8_Brief1.webm");
					break;
				case 9:
					HUDBriefingPanel.PlayStorySpeech("M9_Brief1.webm");
					break;
				case 10:
					HUDBriefingPanel.PlayStorySpeech("M10_Brief1.webm");
					break;
				case 11:
					HUDBriefingPanel.PlayStorySpeech("M11_Brief1.webm");
					break;
				case 12:
					HUDBriefingPanel.PlayStorySpeech("M12_Brief1.webm");
					break;
				case 13:
					HUDBriefingPanel.PlayStorySpeech("M13_Brief1.webm");
					break;
				case 14:
					HUDBriefingPanel.PlayStorySpeech("M14_Brief1.webm");
					break;
				case 15:
					HUDBriefingPanel.PlayStorySpeech("M15_Brief1.webm");
					break;
				case 16:
					HUDBriefingPanel.PlayStorySpeech("M16_Brief1.webm");
					break;
				case 17:
					HUDBriefingPanel.PlayStorySpeech("M17_Brief1.webm");
					break;
				case 18:
					HUDBriefingPanel.PlayStorySpeech("M18_Brief1.webm");
					break;
				case 19:
					HUDBriefingPanel.PlayStorySpeech("M19_Brief1.webm");
					break;
				case 20:
					HUDBriefingPanel.PlayStorySpeech("M20_Brief1.webm");
					break;
				case 21:
					HUDBriefingPanel.PlayStorySpeech("M21_Brief1.webm");
					break;
				case 33:
					HUDBriefingPanel.PlayStorySpeech("M33_Brief1.webm");
					break;
				case 34:
					HUDBriefingPanel.PlayStorySpeech("M34_Brief1.webm");
					break;
				case 35:
					HUDBriefingPanel.PlayStorySpeech("M35_Brief1.webm");
					break;
				case 36:
					HUDBriefingPanel.PlayStorySpeech("M36_Brief1.webm");
					break;
				case 37:
					HUDBriefingPanel.PlayStorySpeech("M37_Brief1.webm");
					break;
				}
			}
			HUDBriefingPanel.RefBriefingObjectivesButton.Visibility = Visibility.Visible;
			if (GameData.Instance.mission_level == 6 && GameData.Instance.mission6Prestart)
			{
				HUDBriefingPanel.RefBriefingHintsButton.Visibility = Visibility.Hidden;
				HUDBriefingPanel.RefBriefingTutorialButton.Visibility = Visibility.Hidden;
			}
			else
			{
				HUDBriefingPanel.RefBriefingHintsButton.Visibility = Visibility.Visible;
				HUDBriefingPanel.RefBriefingTutorialButton.Visibility = Visibility.Visible;
			}
		}
		else if (GameData.Instance.game_type == 7 || GameData.Instance.game_type == 8 || GameData.Instance.game_type == 9 || GameData.Instance.game_type == 10 || GameData.Instance.game_type == 12)
		{
			HUDBriefingPanel.RefBriefingObjectivesButton.Visibility = Visibility.Visible;
			HUDBriefingPanel.RefBriefingHintsButton.Visibility = Visibility.Visible;
			HUDBriefingPanel.RefBriefingTutorialButton.Visibility = Visibility.Visible;
			HUDBriefingPanel.RefBriefingStrategySection.Visibility = Visibility.Hidden;
			switch (GameData.Instance.game_type)
			{
			case 7:
				BriefingMissionTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, 70 + GameData.Instance.mission_level - 41);
				break;
			case 8:
				BriefingMissionTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, 70 + GameData.Instance.mission_level - 51);
				break;
			case 9:
				BriefingMissionTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, 70 + GameData.Instance.mission_level - 61);
				break;
			case 10:
				BriefingMissionTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, 70 + GameData.Instance.mission_level - 71);
				break;
			case 12:
				BriefingMissionTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 70 + GameData.Instance.mission_level - 81);
				break;
			case 11:
				break;
			}
		}
		else
		{
			HUDBriefingPanel.RefBriefingObjectivesButton.Visibility = Visibility.Hidden;
			HUDBriefingPanel.RefBriefingHintsButton.Visibility = Visibility.Hidden;
			HUDBriefingPanel.RefBriefingTutorialButton.Visibility = Visibility.Hidden;
			BriefingMissionTitle = GameData.Instance.cachedMissionName;
		}
	}

	public void ResizeBriefingScreen()
	{
		int num = iUIScaleValueWidth;
		int num2 = iUIScaleValueHeight;
		int num3 = 1365;
		int num4 = 768;
		int num5 = 1365;
		int num6 = 768;
		float a = (float)num / (float)num3;
		float b = (float)num2 / (float)num4;
		float num7 = Mathf.Min(a, b);
		BriefingViewboxWidth = (int)((float)num5 * num7);
		BriefingViewboxHeight = (int)((float)num6 * num7);
		HUD_Briefing.ViewportScale = num7;
	}

	private void ButtonBriefingQuit(object parameter)
	{
		HUDBriefingPanel.StopStorySpeech();
		HUDBriefingPanel.CloseHelp();
		if (!BriefingFromStory)
		{
			SFXManager.instance.playSpeech(1, "General_Quitgame.wav", 1f);
			HUD_ConfirmationPopup.ShowConfirmation(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 7), delegate
			{
				InitNewScene(Enums.SceneIDS.FrontEnd);
			}, delegate
			{
				_ = BriefingMode;
				_ = 3;
			});
			return;
		}
		InitNewScene(Enums.SceneIDS.FrontEnd);
		if (GameData.Instance.game_type == 0)
		{
			FrontEndMenu.ButtonClicked("MainCampaign");
			return;
		}
		if (GameData.Instance.game_type == 5)
		{
			FrontEndMenu.ButtonClicked("EcoCampaign");
			return;
		}
		if (GameData.Instance.game_type == 7)
		{
			FrontEndMenu.ButtonClicked("DLC1");
			return;
		}
		if (GameData.Instance.game_type == 8)
		{
			FrontEndMenu.ButtonClicked("DLC2");
			return;
		}
		if (GameData.Instance.game_type == 9)
		{
			FrontEndMenu.ButtonClicked("DLC3");
			return;
		}
		if (GameData.Instance.game_type == 10)
		{
			FrontEndMenu.ButtonClicked("DLC4");
			return;
		}
		if (GameData.Instance.game_type == 12)
		{
			FrontEndMenu.ButtonClicked("DLCECO");
			return;
		}
		if (GameData.Instance.game_type == 11)
		{
			FrontEndMenu.ButtonClicked("Trail");
			return;
		}
		if (GameData.Instance.game_type == 13)
		{
			FrontEndMenu.ButtonClicked("Trail2");
			return;
		}
		FrontendMenus.ClearUIPanels();
		if (GameData.Instance.game_type != 2)
		{
			return;
		}
		switch (GameData.Instance.mapType)
		{
		case Enums.GameModes.SIEGE:
			if (GameData.Instance.siegeThat)
			{
				FRONT_StandaloneMission.Open(Enums.StartUpUIPanels.SiegeThat);
			}
			else
			{
				FRONT_StandaloneMission.Open(Enums.StartUpUIPanels.Siege);
			}
			break;
		case Enums.GameModes.INVASION:
			FRONT_StandaloneMission.Open(Enums.StartUpUIPanels.Invasion);
			break;
		case Enums.GameModes.ECO:
			FRONT_StandaloneMission.Open(Enums.StartUpUIPanels.EcoMission);
			break;
		case Enums.GameModes.BUILD:
			FRONT_StandaloneMission.Open(Enums.StartUpUIPanels.FreeBuild);
			break;
		}
	}

	public void ButtonBriefingResume(object parameter)
	{
		HUDBriefingPanel.StopStorySpeech();
		if (OST_Starting_goods_Vis && BriefingFromStory)
		{
			OnScreenText.Instance.startCart();
		}
		if (BriefingFromStory && GameData.Instance.game_type == 0 && GameData.Instance.difficulty_level != Enums.GameDifficulty.DIFFICULTY_NORMAL && GameData.Instance.difficulty_level != Enums.GameDifficulty.DIFFICULTY_NA)
		{
			EditorDirector.instance.stopGameSim();
			EditorDirector.instance.LoadCampaignMap(startedMission, (int)GameData.Instance.difficulty_level, startedMissionPre6);
		}
		if (BriefingFromStory && GameData.Instance.game_type == 5 && GameData.Instance.difficulty_level != Enums.GameDifficulty.DIFFICULTY_NORMAL && GameData.Instance.difficulty_level != Enums.GameDifficulty.DIFFICULTY_NA)
		{
			EditorDirector.instance.stopGameSim();
			EditorDirector.instance.LoadEcoCampaignMap(startedMission, (int)GameData.Instance.difficulty_level);
		}
		if (BriefingFromStory && (GameData.Instance.game_type == 7 || GameData.Instance.game_type == 8 || GameData.Instance.game_type == 9 || GameData.Instance.game_type == 10) && GameData.Instance.difficulty_level != Enums.GameDifficulty.DIFFICULTY_NORMAL && GameData.Instance.difficulty_level != Enums.GameDifficulty.DIFFICULTY_NA)
		{
			EditorDirector.instance.stopGameSim();
			int dlc = (startedMission - 30) / 10;
			int mission = startedMission % 10;
			EditorDirector.instance.LoadExtraCampaignMap(dlc, mission, (int)GameData.Instance.difficulty_level);
		}
		if (BriefingFromStory && GameData.Instance.game_type == 12 && GameData.Instance.difficulty_level != Enums.GameDifficulty.DIFFICULTY_NORMAL && GameData.Instance.difficulty_level != Enums.GameDifficulty.DIFFICULTY_NA)
		{
			EditorDirector.instance.stopGameSim();
			int mission2 = startedMission % 10;
			EditorDirector.instance.LoadExtraEcoCampaignMap(mission2, (int)GameData.Instance.difficulty_level);
		}
		if (!BriefingFromStory && GameData.Instance.lastGameState != null && GameData.Instance.lastGameState.app_sub_mode != 49 && GameData.Instance.lastGameState.app_sub_mode != 13)
		{
			EditorDirector.instance.directSetAppMode(14, 0);
		}
		EngineInterface.GameAction(Enums.GameActionCommand.HideObjectiveProgress, 0, 0);
		Show_HUD_Main = true;
		RollOverText_Margin = "0,0,-20,130";
		Show_HUD_Troops = false;
		Show_HUD_Building = false;
		Show_HUD_Briefing = false;
		Show_HUD_MissionOver = false;
		Show_HUD_Book = true;
		IsMapEditorMode = false;
		MyAudioManager.Instance.PauseSpeech(state: false);
		if (!preBriefingWasPaused && !Director.instance.MultiplayerGame)
		{
			Director.instance.SetPausedState(state: false);
		}
		HUDRoot.RefRadarMapGrid.Visibility = Visibility.Visible;
		HUDRoot.RefReportsControlGrid.Visibility = Visibility.Visible;
		HUDBriefingPanel.CloseHelp();
		BriefingFromStory = false;
	}

	private void ButtonBriefingMode(object parameter)
	{
		int num = Convert.ToInt32(parameter as string);
		if (num == 3 && ConfigSettings.Settings_UseSteamOverlayForHelp)
		{
			HUDBriefingPanel.OpenHelpInOverlay();
			return;
		}
		int briefingMode = BriefingMode;
		BriefingMode = num;
		FatControler.instance.BriefingUIUpdate();
		HUDBriefingPanel.RefObjectivesSubPanel.Visibility = Visibility.Hidden;
		HUDBriefingPanel.RefHintsSubPanel.Visibility = Visibility.Hidden;
		HUDBriefingPanel.RefHelpSubPanel.Visibility = Visibility.Hidden;
		if (BriefingMode == 1)
		{
			HUDBriefingPanel.RefObjectivesSubPanel.Visibility = Visibility.Visible;
		}
		else if (BriefingMode == 2)
		{
			HUDBriefingPanel.RefHintsSubPanel.Visibility = Visibility.Visible;
		}
		else if (BriefingMode == 3)
		{
			HUDBriefingPanel.RefHelpSubPanel.Visibility = Visibility.Visible;
		}
		if (briefingMode != num)
		{
			_ = BriefingMode;
			_ = 3;
		}
	}

	private void ButtonBriefingDifficulty(object parameter)
	{
		Enums.GameDifficulty gameDifficulty = GameData.Instance.difficulty_level;
		switch (gameDifficulty)
		{
		case Enums.GameDifficulty.DIFFICULTY_EASY:
			gameDifficulty = Enums.GameDifficulty.DIFFICULTY_NORMAL;
			break;
		case Enums.GameDifficulty.DIFFICULTY_NA:
		case Enums.GameDifficulty.DIFFICULTY_NORMAL:
			gameDifficulty = Enums.GameDifficulty.DIFFICULTY_HARD;
			break;
		case Enums.GameDifficulty.DIFFICULTY_HARD:
			gameDifficulty = Enums.GameDifficulty.DIFFICULTY_VERYHARD;
			break;
		case Enums.GameDifficulty.DIFFICULTY_VERYHARD:
			gameDifficulty = Enums.GameDifficulty.DIFFICULTY_EASY;
			break;
		}
		EngineInterface.setEcoCampaignDifficulty(gameDifficulty);
		GameData.Instance.difficulty_level = gameDifficulty;
	}

	private void ButtonBriefingHint(object parameter)
	{
		if (Convert.ToInt32(parameter as string) == GameData.Instance.GetNumHintsUnlocked() + 1)
		{
			GameData.Instance.UnlockHint();
		}
	}

	private void ButtonBriefingBack(object parameter)
	{
		HUDBriefingPanel.goBack();
	}

	private void ButtonBriefingRollover(object parameter)
	{
		switch (Convert.ToInt32(parameter as string))
		{
		case 999:
			BriefingRolloverText = "";
			break;
		case 1000:
			BriefingRolloverText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 7);
			CommonRedButtonEnter(null, null);
			break;
		case 1001:
			BriefingRolloverText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_BRIEF_OBJECTIVES);
			CommonRedButtonEnter(null, null);
			break;
		case 1002:
			BriefingRolloverText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_BRIEF_HINTS);
			CommonRedButtonEnter(null, null);
			break;
		case 1003:
			BriefingRolloverText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_BRIEF_TUTORIAL);
			CommonRedButtonEnter(null, null);
			break;
		case 1004:
			if (BriefingFromStory)
			{
				BriefingRolloverText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, Enums.eTextValues.BHELP_TEXT_BRIEF_STARTGAME);
			}
			else
			{
				BriefingRolloverText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 10);
			}
			CommonRedButtonEnter(null, null);
			break;
		}
	}

	private void ButtonMainHelp(object parameter)
	{
		switch (Convert.ToInt32(parameter as string))
		{
		case -1:
			HUDHelp.goBack();
			break;
		case -2:
			HUDHelp.goHome();
			break;
		default:
			HUDHelp.Close();
			break;
		}
	}

	public void ButtonExtendedFeaturesFunction(object parameter)
	{
		switch ((string)parameter)
		{
		case "Objectives":
			SetGoodsPopupState(visible: false);
			SetObjectivePopupState(!Show_HUD_Objectives);
			SetHUDGoodsVisible(allVis: true);
			return;
		case "Goods":
			SetObjectivePopupState(visible: false);
			SetGoodsPopupState(!Show_HUD_Goods);
			SetHUDGoodsVisible(ShowAllHudVis);
			return;
		case "Freebuild":
			HUD_FreebuildMenu.ToggleMenu();
			return;
		case "Trading":
			SetHUDGoodsVisible(!ShowAllHudVis);
			return;
		}
		try
		{
			int state = int.Parse((string)parameter);
			previousTradePanel = -10;
			EngineInterface.GameAction(Enums.GameActionCommand.SelectBuildingType, 26, state);
		}
		catch (Exception)
		{
		}
	}

	private void SetHUDGoodsVisible(bool allVis)
	{
		ShowAllHudVis = allVis;
		if (allVis)
		{
			Show_HUD_Goods_Normal = true;
			Show_HUD_Goods_Trade = false;
			for (int i = 0; i < 25; i++)
			{
				DisplayHudGoodsBool[i] = true;
			}
		}
		else
		{
			Show_HUD_Goods_Normal = false;
			Show_HUD_Goods_Trade = true;
			for (int j = 0; j < 25; j++)
			{
				DisplayHudGoodsBool[j] = AllStoredGoodsVisible[j];
			}
		}
	}

	public void SetObjectivePopupState(bool visible)
	{
		Show_HUD_Objectives = visible;
		if (!visible)
		{
			IngameUI.refHUD_Objectives.Margin = new Thickness(-400f, 0f, 0f, 0f);
			return;
		}
		HUDObjectives.RefRoot.Height = 0f;
		IngameUI.refHUD_Objectives.Margin = new Thickness(36f, 0f, 0f, 0f);
	}

	public void SetGoodsPopupState(bool visible)
	{
		Show_HUD_Goods = visible;
		if (!visible)
		{
			IngameUI.refHUD_Goods.Margin = new Thickness(-400f, 0f, 0f, 0f);
		}
		else
		{
			IngameUI.refHUD_Goods.Margin = new Thickness(36f, 0f, 0f, 0f);
		}
	}

	public void InitObjectiveGoodsPanelDelayed()
	{
		GameMap.instance.SetPostUpdateFunction(delegate
		{
			InitObjectiveGoodsPanel();
		});
	}

	public void InitObjectiveGoodsPanel()
	{
		Show_HUD_Extras = true;
		bool show_HUD_Extras_Button_Objectves = false;
		List<GameData.ScenarioEvent> events = GameData.scenario.getEvents();
		int startDate = 0;
		int nowDate = 0;
		int endDate = 0;
		if (GameData.scenario.getWinTimer(ref startDate, ref nowDate, ref endDate) != null)
		{
			show_HUD_Extras_Button_Objectves = true;
		}
		else if (events != null)
		{
			foreach (GameData.ScenarioEvent item in events)
			{
				if (item != null)
				{
					show_HUD_Extras_Button_Objectves = true;
					break;
				}
			}
		}
		Show_HUD_Extras_Button_Objectves = show_HUD_Extras_Button_Objectves;
		if (GameData.Instance.game_type == 2 && GameData.Instance.mapType == Enums.GameModes.BUILD)
		{
			Show_HUD_Extras_Button_Freebuild = true;
		}
		else
		{
			Show_HUD_Extras_Button_Freebuild = false;
		}
	}

	private void ButtonScenarioStartMonth(object parameter)
	{
		if (++GameData.Instance.scenarioOverview.startMonth >= 12)
		{
			GameData.Instance.scenarioOverview.startMonth = 0;
		}
		EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Starting_Month, 0, GameData.Instance.scenarioOverview.startMonth);
	}

	private void ButtonScenarioAltTextFunc(object parameter)
	{
		if ((string)parameter == "1")
		{
			HUDScenario.ApplyAltMissionText(ansi: true);
		}
		else
		{
			HUDScenario.ApplyAltMissionText(ansi: false);
		}
	}

	private void ButtonScenarioStartRations(object parameter)
	{
		if (++GameData.Instance.scenarioOverview.special_start_rationing >= 5)
		{
			GameData.Instance.scenarioOverview.special_start_rationing = 0;
		}
		EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Starting_Special_Rations, 0, GameData.Instance.scenarioOverview.special_start_rationing);
	}

	private void ButtonScenarioStartTax(object parameter)
	{
		if (++GameData.Instance.scenarioOverview.special_start_tax_rate >= 10)
		{
			GameData.Instance.scenarioOverview.special_start_tax_rate = 0;
		}
		EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Starting_Special_Tax, 0, GameData.Instance.scenarioOverview.special_start_tax_rate);
	}

	private void ButtonScenarioAdjustDateBtn(object parameter)
	{
		switch (Convert.ToInt32(parameter as string))
		{
		case 0:
		{
			EngineInterface.ScenarioOverview scenarioOverview = GameData.Instance.scenarioOverview;
			int num = int.Parse(ScenarioStartingYearText, Director.defaultCulture);
			int num2 = int.Parse(ScenarioAdjustStartingYearText, Director.defaultCulture);
			int startMonth = scenarioOverview.startMonth;
			int newStartingMonthValue = HUDScenario.newStartingMonthValue;
			if (num != num2 || startMonth != newStartingMonthValue)
			{
				int num3 = (num2 - num) * 12 + (newStartingMonthValue - startMonth);
				for (int i = 0; i < scenarioOverview.entries.Count; i++)
				{
					int year = scenarioOverview.entries[i].year;
					int month = scenarioOverview.entries[i].month;
					int num4 = year * 12 + month + num3;
					scenarioOverview.entries[i].year = num4 / 12;
					scenarioOverview.entries[i].month = num4 % 12;
					EngineInterface.UpdateScenarioActionDate(i, scenarioOverview.entries[i].year, scenarioOverview.entries[i].month);
				}
				ScenarioStartingYearText = num2.ToString();
				GameData.Instance.scenarioOverview.startMonth = newStartingMonthValue;
				EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Starting_Month, 0, GameData.Instance.scenarioOverview.startMonth);
				HUDScenario.changeScenarioView(Enums.ScenarioViews.Main);
			}
			break;
		}
		case 1:
			if (++HUDScenario.newStartingMonthValue >= 12)
			{
				HUDScenario.newStartingMonthValue = 0;
			}
			ScenarioAdjustStartingMonthText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MONTHS, HUDScenario.newStartingMonthValue);
			break;
		}
	}

	private void ButtonScenarioViewBtn(object parameter)
	{
		ButtonScenarioView(parameter);
	}

	public void ButtonScenarioView(object parameter, bool fromButton = true)
	{
		int mode = Convert.ToInt32(parameter as string);
		if (mode == -1)
		{
			mode = 1;
			if (ScenarioEditorMode == Enums.ScenarioViews.EventsConditions || ScenarioEditorMode == Enums.ScenarioViews.EventsActions)
			{
				mode = 7;
				fromButton = false;
				HUDScenario.PopulateEvent();
			}
			else
			{
				HUDScenario.ActionOKButton(ref mode);
			}
		}
		if (mode == -2)
		{
			HUDScenario.ActionDeleteButton();
			mode = 1;
		}
		switch (mode)
		{
		case -3:
			HUDScenario.EventActionMessageOpen();
			fromButton = false;
			HUDScenario.changeScenarioView(Enums.ScenarioViews.Messages, fromButton);
			break;
		case 1:
			HUDScenario.changeScenarioView(Enums.ScenarioViews.Main);
			break;
		case 2:
			HUDScenario.changeScenarioView(Enums.ScenarioViews.StartingGoods);
			break;
		case 3:
			HUDScenario.changeScenarioView(Enums.ScenarioViews.TradedGoods);
			break;
		case 4:
			HUDScenario.changeScenarioView(Enums.ScenarioViews.BuildingAvailibilty);
			break;
		case 5:
			HUDScenario.changeScenarioView(Enums.ScenarioViews.Invasions, fromButton);
			break;
		case 6:
			HUDScenario.changeScenarioView(Enums.ScenarioViews.Messages, fromButton);
			break;
		case 7:
			HUDScenario.changeScenarioView(Enums.ScenarioViews.Events, fromButton);
			break;
		case 8:
			HUDScenario.changeScenarioView(Enums.ScenarioViews.AttackingForce);
			break;
		case 9:
			HUDScenario.changeScenarioView(Enums.ScenarioViews.EventsConditions);
			break;
		case 10:
			HUDScenario.changeScenarioView(Enums.ScenarioViews.EventsActions);
			break;
		case 11:
			HUDScenario.changeScenarioView(Enums.ScenarioViews.EditMessage);
			break;
		case 12:
			HUDScenario.changeScenarioView(Enums.ScenarioViews.EditTeams);
			break;
		case 13:
			HUDScenario.changeScenarioView(Enums.ScenarioViews.AdjustDates);
			break;
		}
	}

	public void ButtonScenarioStartingGoodSelect(object parameter)
	{
		int mode = Convert.ToInt32(parameter as string);
		if (mode >= 0)
		{
			ScenarioStartingGoodsType = mode;
			HUDScenario.RefStartingGoods.Value = initSliderScenarioStartingGoods();
			return;
		}
		HUD_ConfirmationPopup.ShowConfirmation(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 70), delegate
		{
			for (int i = 0; i < 25; i++)
			{
				GameData.Instance.scenarioOverview.scenario_start_goods[i] = 0;
				EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_StartingGoods, i, 0);
			}
			int[] array;
			if (mode == -1)
			{
				array = lowStartingGoodsPreset;
			}
			else if (mode == -2)
			{
				array = medStartingGoodsPreset;
			}
			else
			{
				if (mode != -3)
				{
					return;
				}
				array = highStartingGoodsPreset;
			}
			int num = 2;
			GameData.Instance.scenarioOverview.scenario_start_goods[num] = array[0];
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_StartingGoods, num, GameData.Instance.scenarioOverview.scenario_start_goods[num]);
			num = 4;
			GameData.Instance.scenarioOverview.scenario_start_goods[num] = array[1];
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_StartingGoods, num, GameData.Instance.scenarioOverview.scenario_start_goods[num]);
			num = 6;
			GameData.Instance.scenarioOverview.scenario_start_goods[num] = array[2];
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_StartingGoods, num, GameData.Instance.scenarioOverview.scenario_start_goods[num]);
			num = 8;
			GameData.Instance.scenarioOverview.scenario_start_goods[num] = array[3];
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_StartingGoods, num, GameData.Instance.scenarioOverview.scenario_start_goods[num]);
			num = 13;
			GameData.Instance.scenarioOverview.scenario_start_goods[num] = array[4];
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_StartingGoods, num, GameData.Instance.scenarioOverview.scenario_start_goods[num]);
			num = 12;
			GameData.Instance.scenarioOverview.scenario_start_goods[num] = array[5];
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_StartingGoods, num, GameData.Instance.scenarioOverview.scenario_start_goods[num]);
			num = 11;
			GameData.Instance.scenarioOverview.scenario_start_goods[num] = array[6];
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_StartingGoods, num, GameData.Instance.scenarioOverview.scenario_start_goods[num]);
			num = 10;
			GameData.Instance.scenarioOverview.scenario_start_goods[num] = array[7];
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_StartingGoods, num, GameData.Instance.scenarioOverview.scenario_start_goods[num]);
			num = 15;
			GameData.Instance.scenarioOverview.scenario_start_goods[num] = array[8];
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_StartingGoods, num, GameData.Instance.scenarioOverview.scenario_start_goods[num]);
			HUDScenario.RefStartingGoods.Value = initSliderScenarioStartingGoods();
			for (int j = 0; j <= 24; j++)
			{
				StartingGoods[j] = GameData.Instance.scenarioOverview.scenario_start_goods[j];
			}
		}, delegate
		{
		});
	}

	public void ButtonScenarioAttackingForcesSelect(object parameter)
	{
		int attackingForceTroopTypeFromIndex = GameData.getAttackingForceTroopTypeFromIndex(ScenarioAttackingForceType = Convert.ToInt32(parameter as string));
		int num = initSliderScenarioAttackingForce();
		ScenarioAdjustedStartingAttackingForceMax = GameData.getMaxTroopsForAttackingForce((Enums.eChimps)attackingForceTroopTypeFromIndex);
		int num2 = ScenarioAdjustedStartingAttackingForceMax / 10;
		if (num2 == 0)
		{
			num2 = 1;
		}
		ScenarioAdjustedStartingAttackingForceFreq = num2;
		HUDScenario.RefAttackingForce.Value = num;
	}

	public void ButtonScenarioEditSettings(object parameter)
	{
		int num = Convert.ToInt32(parameter as string);
		switch (num)
		{
		case -10:
			EngineInterface.GameAction(Enums.GameActionCommand.HoldTime, 0, 0);
			break;
		case -11:
			HUD_IngameMenu.SaveMapEditor(GameData.Instance.mapType == Enums.GameModes.SIEGE && GameData.Instance.siegeThat, fromIngameMenu: false);
			break;
		case -12:
		{
			string title = (EditorDirector.instance.mapChanged ? Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 43) : Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 84));
			SFXManager.instance.playSpeech(1, "General_Quitgame.wav", 1f);
			HUD_ConfirmationPopup.ShowConfirmation(title, delegate
			{
				Instance.InitNewScene(Enums.SceneIDS.FrontEnd);
			}, delegate
			{
			});
			break;
		}
		case -1:
			HUDScenarioPopup.EditSettingsClicked();
			break;
		case -2:
			if (GameData.Instance.multiplayerMap)
			{
				if (EngineInterface.EditorChangeMap_Mode(changeToMP: false) == 0)
				{
					GameData.Instance.multiplayerMap = false;
					HUDScenarioPopup.Refresh();
					SetMapTypeVisibility(GameData.Instance.mapType == Enums.GameModes.SIEGE);
					Show_HUD_Scenario = true;
					Instance.HUDmain.SetEditorModeButtonVisibilityForSiegeThatMode(visible: true);
				}
			}
			else if (EngineInterface.EditorChangeMap_Mode(changeToMP: true) != 0)
			{
				GameData.Instance.multiplayerMap = true;
				HUDScenarioPopup.Refresh();
				SetMapTypeVisibility(GameData.Instance.mapType == Enums.GameModes.SIEGE);
				Show_HUD_Scenario = false;
				Instance.HUDmain.SetEditorModeButtonVisibilityForSiegeThatMode(visible: true);
			}
			break;
		case 1:
			if (!GameData.Instance.multiplayerMap && GameData.Instance.mapType != Enums.GameModes.SIEGE)
			{
				int num3 = EngineInterface.EditorChangeMap_GameType(Enums.GameModes.SIEGE);
				if (num3 >= 0)
				{
					GameData.Instance.mapType = (Enums.GameModes)num3;
					HUDScenarioPopup.Refresh();
					SetMapTypeVisibility(GameData.Instance.mapType == Enums.GameModes.SIEGE);
					Instance.HUDmain.SetEditorModeButtonVisibilityForSiegeThatMode(visible: true);
				}
			}
			break;
		case 2:
			if (!GameData.Instance.multiplayerMap && GameData.Instance.mapType != Enums.GameModes.INVASION)
			{
				int num4 = EngineInterface.EditorChangeMap_GameType(Enums.GameModes.INVASION);
				if (num4 >= 0)
				{
					GameData.Instance.mapType = (Enums.GameModes)num4;
					HUDScenarioPopup.Refresh();
					SetMapTypeVisibility(GameData.Instance.mapType == Enums.GameModes.SIEGE);
					Instance.HUDmain.SetEditorModeButtonVisibilityForSiegeThatMode(visible: true);
				}
			}
			break;
		case 3:
			if (!GameData.Instance.multiplayerMap && GameData.Instance.mapType != Enums.GameModes.ECO)
			{
				int num2 = EngineInterface.EditorChangeMap_GameType(Enums.GameModes.ECO);
				if (num2 >= 0)
				{
					GameData.Instance.mapType = (Enums.GameModes)num2;
					HUDScenarioPopup.Refresh();
					SetMapTypeVisibility(GameData.Instance.mapType == Enums.GameModes.SIEGE);
					Instance.HUDmain.SetEditorModeButtonVisibilityForSiegeThatMode(visible: true);
				}
			}
			break;
		case 4:
			if (!GameData.Instance.multiplayerMap && GameData.Instance.mapType != 0)
			{
				int num5 = EngineInterface.EditorChangeMap_GameType(Enums.GameModes.BUILD);
				if (num5 >= 0)
				{
					GameData.Instance.mapType = (Enums.GameModes)num5;
					HUDScenarioPopup.Refresh();
					SetMapTypeVisibility(GameData.Instance.mapType == Enums.GameModes.SIEGE);
					Instance.HUDmain.SetEditorModeButtonVisibilityForSiegeThatMode(visible: true);
				}
			}
			break;
		case 10:
			if (GameData.Instance.multiplayerMap)
			{
				if (EngineInterface.EditorChangeMap_KotH(!GameData.Instance.multiplayerKOTHMap) != 0)
				{
					GameData.Instance.multiplayerKOTHMap = true;
				}
				else
				{
					GameData.Instance.multiplayerKOTHMap = false;
				}
				HUDScenarioPopup.Refresh();
				SetMapTypeVisibility(GameData.Instance.mapType == Enums.GameModes.SIEGE);
			}
			break;
		case 160:
		case 200:
		case 300:
		case 400:
			if (GameMap.tilemapSize != num)
			{
				EditorDirector.instance.changeMapSize(num);
				HUDScenarioPopup.Refresh();
			}
			break;
		}
	}

	public void RightClickSizeClick(object param)
	{
		HUDmain.CycleMEDrawingSizeSmaller();
	}

	public void RightClickRulerClick(object param)
	{
		HUDmain.CycleMERulerBack();
	}

	private void ButtonScenarioStartingTraderToggle(object parameter)
	{
		int num = Convert.ToInt32(parameter as string);
		if (num >= 1000)
		{
			int num2 = 0;
			if (num == 1000)
			{
				num2 = 1;
			}
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[2] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 2, num2);
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[4] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 4, num2);
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[8] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 8, num2);
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[6] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 6, num2);
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[3] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 3, num2);
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[9] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 9, num2);
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[10] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 10, num2);
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[11] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 11, num2);
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[12] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 12, num2);
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[13] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 13, num2);
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[14] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 14, num2);
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[16] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 16, num2);
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[17] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 17, num2);
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[18] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 18, num2);
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[19] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 19, num2);
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[20] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 20, num2);
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[21] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 21, num2);
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[22] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 22, num2);
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[23] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 23, num2);
			GameData.Instance.scenarioOverview.scenario_trader_goods_available[24] = num2;
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, 24, num2);
		}
		else
		{
			if (GameData.Instance.scenarioOverview.scenario_trader_goods_available[num] > 0)
			{
				GameData.Instance.scenarioOverview.scenario_trader_goods_available[num] = 0;
			}
			else
			{
				GameData.Instance.scenarioOverview.scenario_trader_goods_available[num] = 1;
			}
			EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Trading, num, GameData.Instance.scenarioOverview.scenario_trader_goods_available[num]);
		}
	}

	private void ButtonScenarioBuildingAvailToggle(object parameter)
	{
		HUDScenario.ButtonScenarioBuildingAvailToggle(parameter);
	}

	public void SliderScenarioStartPop(int newStartingPop)
	{
		GameData.Instance.scenarioOverview.scenario_start_popularity = newStartingPop;
		EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Starting_Popularity, 0, GameData.Instance.scenarioOverview.scenario_start_popularity);
	}

	public void SliderScenarioStartSpecialGold(int newStartingGold)
	{
		GameData.Instance.scenarioOverview.special_start_gold = newStartingGold;
		EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_Starting_Special_Gold, 0, GameData.Instance.scenarioOverview.special_start_gold);
	}

	public void SliderScenarioStartGold(int newStartingGold)
	{
		GameData.Instance.scenarioOverview.scenario_start_goods[15] = newStartingGold;
		EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_StartingGoods, 15, GameData.Instance.scenarioOverview.scenario_start_goods[15]);
	}

	public void SliderScenarioStartPitch(int newStartingPitch)
	{
		GameData.Instance.scenarioOverview.scenario_start_goods[8] = newStartingPitch;
		EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_StartingGoods, 8, GameData.Instance.scenarioOverview.scenario_start_goods[8]);
	}

	public void SliderScenarioStartingGoods(int newStartingAmount)
	{
		int num = newStartingAmount;
		if (num >= 50)
		{
			num = 49;
		}
		if (ScenarioAdjustedStartingGoodsMax == 200)
		{
			GameData.Instance.scenarioOverview.scenario_start_goods[ScenarioStartingGoodsType] = GoodsCurve200[num];
		}
		else if (ScenarioAdjustedStartingGoodsMax == 1000)
		{
			GameData.Instance.scenarioOverview.scenario_start_goods[ScenarioStartingGoodsType] = GoodsCurve1000[num];
		}
		else if (ScenarioAdjustedStartingGoodsMax == 5000)
		{
			GameData.Instance.scenarioOverview.scenario_start_goods[ScenarioStartingGoodsType] = GoodsCurve5000[num];
		}
		else if (ScenarioAdjustedStartingGoodsMax == 50000)
		{
			GameData.Instance.scenarioOverview.scenario_start_goods[ScenarioStartingGoodsType] = GoodsCurve5000[num] * 10;
		}
		EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_StartingGoods, ScenarioStartingGoodsType, GameData.Instance.scenarioOverview.scenario_start_goods[ScenarioStartingGoodsType]);
	}

	public int initSliderScenarioStartingGoods()
	{
		int num = GameData.Instance.scenarioOverview.scenario_start_goods[ScenarioStartingGoodsType];
		ScenarioAdjustedStartingGoodsMax = GameData.startingGoodsLimits[ScenarioStartingGoodsType];
		int[] array = GoodsCurve200;
		if (ScenarioAdjustedStartingGoodsMax == 200)
		{
			array = GoodsCurve200;
		}
		else if (ScenarioAdjustedStartingGoodsMax == 1000)
		{
			array = GoodsCurve1000;
		}
		else if (ScenarioAdjustedStartingGoodsMax == 5000)
		{
			array = GoodsCurve5000;
		}
		else if (ScenarioAdjustedStartingGoodsMax == 50000)
		{
			array = GoodsCurve5000;
			num /= 10;
		}
		for (int i = 0; i < 50; i++)
		{
			if (num <= array[i])
			{
				return i;
			}
		}
		return 50;
	}

	public void SliderScenarioAttackingForces(int newStartingAmount)
	{
		if (ScenarioAttackingForceType < 100)
		{
			GameData.Instance.scenarioOverview.scenario_start_troops[ScenarioAttackingForceType] = newStartingAmount;
		}
		else
		{
			GameData.Instance.scenarioOverview.scenario_start_siege_equipment[ScenarioAttackingForceType - 100] = newStartingAmount;
		}
		EngineInterface.GameAction(Enums.GameActionCommand.Scenario_Set_AttackingForce, ScenarioAttackingForceType, newStartingAmount);
	}

	public int initSliderScenarioAttackingForce()
	{
		if (ScenarioAttackingForceType < 100)
		{
			return GameData.Instance.scenarioOverview.scenario_start_troops[ScenarioAttackingForceType];
		}
		return GameData.Instance.scenarioOverview.scenario_start_siege_equipment[ScenarioAttackingForceType - 100];
	}

	private void ButtonScenarioMessageMonth(object parameter)
	{
		HUDScenario.MessageMonthBtn();
	}

	private void ButtonScenrioMessageGroupFunc(object parameter)
	{
		switch (Convert.ToInt32(parameter as string))
		{
		case 1:
			if (GameData.Instance.message_catagory1 == 3 && (GameData.Instance.message_catagory2 == 0 || GameData.Instance.message_catagory2 == 3))
			{
				GameData.Instance.message_catagory2 = 1;
			}
			GameData.Instance.message_catagory1 = 4;
			GameData.Instance.message_catagory2 = 0;
			break;
		case 2:
			if (GameData.Instance.message_catagory1 == 3 && (GameData.Instance.message_catagory2 == 0 || GameData.Instance.message_catagory2 == 3))
			{
				GameData.Instance.message_catagory2 = 1;
			}
			GameData.Instance.message_catagory1 = 4;
			GameData.Instance.message_catagory2 = 1;
			break;
		case 3:
			if (GameData.Instance.message_catagory1 == 3 && (GameData.Instance.message_catagory2 == 0 || GameData.Instance.message_catagory2 == 3))
			{
				GameData.Instance.message_catagory2 = 1;
			}
			GameData.Instance.message_catagory1 = 4;
			GameData.Instance.message_catagory2 = 2;
			break;
		case 4:
			if (GameData.Instance.message_catagory1 == 3 && (GameData.Instance.message_catagory2 == 0 || GameData.Instance.message_catagory2 == 3))
			{
				GameData.Instance.message_catagory2 = 1;
			}
			GameData.Instance.message_catagory1 = 4;
			GameData.Instance.message_catagory2 = 3;
			break;
		case 5:
			GameData.Instance.message_catagory1 = 3;
			if (GameData.Instance.message_catagory2 > 2)
			{
				GameData.Instance.message_catagory2 = 2;
			}
			break;
		case 6:
			GameData.Instance.message_catagory1 = 5;
			if (GameData.Instance.message_catagory2 > 3)
			{
				GameData.Instance.message_catagory2 = 3;
			}
			break;
		case 10:
			GameData.Instance.message_catagory3 = 0;
			break;
		case 11:
			GameData.Instance.message_catagory3 = 1;
			break;
		case 12:
			GameData.Instance.message_catagory3 = 2;
			break;
		case 13:
			GameData.Instance.message_catagory3 = 3;
			break;
		case 14:
			GameData.Instance.message_catagory2 = 1;
			break;
		case 15:
			GameData.Instance.message_catagory2 = 2;
			break;
		case 16:
			GameData.Instance.message_catagory2 = 0;
			break;
		case 17:
			GameData.Instance.message_catagory2 = 1;
			break;
		case 18:
			GameData.Instance.message_catagory2 = 2;
			break;
		case 19:
			GameData.Instance.message_catagory2 = 3;
			break;
		}
		GameData.Instance.rescan_current_list();
		HUDScenario.DrawMessageList();
	}

	private void ButtonScenarioInvasionMonth(object parameter)
	{
		HUDScenario.InvasionMonthBtn();
	}

	private void ButtonScenrioInvasionGroupFunc(object parameter)
	{
		int invasionFrom = Convert.ToInt32(parameter as string);
		HUDScenario.SetInvasionFrom(invasionFrom);
	}

	private void ButtonScenrioInvasionMarkerIDFunc(object parameter)
	{
		int invasionMarkerID = Convert.ToInt32(parameter as string);
		HUDScenario.SetInvasionMarkerID(invasionMarkerID);
	}

	public void ButtonSelectInvasionSize(object parameter)
	{
		int index = (ScenarioInvasionSizeType = Convert.ToInt32(parameter as string));
		int invasionSizeTroopTypeFromIndex = GameData.getInvasionSizeTroopTypeFromIndex(index);
		int invasionSize = HUDScenario.GetInvasionSize(index);
		ScenarioInvasionSizeMax = GameData.getMaxTroopsForInvasion((Enums.eChimps)invasionSizeTroopTypeFromIndex);
		int num = ScenarioInvasionSizeMax / 10;
		if (num == 0)
		{
			num = 1;
		}
		ScenarioInvasionSizeFreq = num;
		HUDScenario.RefInvasionSize.Value = invasionSize;
	}

	private void ButtonScenarioEventMonth(object parameter)
	{
		HUDScenario.EventMonthBtn();
	}

	private void ButtonScenarionEventConditionToggle(object parameter)
	{
		HUDScenario.EventConditionToggle();
	}

	private void ButtonScenarionEventConditionOnOff(object parameter)
	{
		HUDScenario.EventConditionOnOff();
	}

	private void ButtonScenarionEventConditionAndOr(object parameter)
	{
		HUDScenario.EventAndOr();
	}

	private void ButtonScenarioSpecialFunction(object parameter)
	{
		switch (parameter as string)
		{
		case "1":
			EngineInterface.GameAction(Enums.GameActionCommand.Fix_Tribes, 0, 0);
			break;
		case "2":
			EngineInterface.GameAction(Enums.GameActionCommand.Set_AI_Patrolling, 1, 1);
			break;
		case "3":
			EngineInterface.GameAction(Enums.GameActionCommand.Set_AI_Patrolling, 0, 0);
			break;
		case "4":
			EngineInterface.GameAction(Enums.GameActionCommand.AI_Target_Special, 0, 0);
			break;
		case "-1":
			Show_HUD_ScenarioSpecial = false;
			break;
		}
	}

	private void ButtonIngameMenuFunction(object parameter)
	{
		int function = Convert.ToInt32(parameter as string);
		HUDIngameMenu.ButtonIngameMenuFunction(function);
	}

	private void ButtonConfirmationFunction(object parameter)
	{
		int mode = Convert.ToInt32(parameter as string);
		HUDConfirmationPopup.ConfirmationClicked(mode);
	}

	private void ButtonLoadSaveFunction(object parameter)
	{
		int function = Convert.ToInt32(parameter as string);
		HUDLoadSaveRequester.ButtonClicked(function);
	}

	private void ButtonTutorialFunction(object parameter)
	{
		int button = Convert.ToInt32(parameter as string);
		HUDTutorial.ButtonClicked(button);
	}

	private void ButtonMPInviteFunction(object parameter)
	{
		HUDMPInviteWarning.ButtonClicked();
	}

	private void ButtonMPConectionIssueFunction(object parameter)
	{
		HUDMPConnectionIssue.ButtonClicked();
	}

	private void ButtonMPChatFunction(object parameter)
	{
		HUDMPChatPanel.ButtonClicked((string)parameter);
	}

	public void TroopsSelectedGameAction(bool fromInitialOpening)
	{
		ButtonTroopPanelMouseLeave("");
		RollOverText_Margin = "0,0,-90,124";
		Show_HUD_Main = false;
		Show_HUD_Troops = true;
		if (fromInitialOpening)
		{
			Show_HUD_ControlGroups = false;
		}
		Show_HUD_Building = false;
		Show_HUD_Briefing = false;
		Show_HUD_MissionOver = false;
		Show_HUD_Book = true;
		HUDTroopPanel.SelectedTroops(fromInitialOpening);
	}

	public void DefaultGameUIGameAction(bool force = false)
	{
		if (force)
		{
			UIVisible = true;
		}
		Show_HUD_Main = true;
		Show_HUD_Troops = false;
		Show_HUD_Building = false;
		Show_HUD_Briefing = false;
		Show_HUD_MissionOver = false;
		Show_HUD_Book = true;
		RollOverText_Margin = "0,0,-20,130";
	}

	public void DefaultMapEditorUIGameAction()
	{
		Show_HUD_Main = true;
		Show_HUD_Troops = false;
		Show_HUD_Building = false;
		Show_HUD_Briefing = false;
		Show_HUD_MissionOver = false;
		Show_HUD_Book = false;
		RollOverText_Margin = "0,0,-20,130";
	}

	public void InBuildingGameAction()
	{
		Show_HUD_Main = false;
		Show_HUD_Troops = false;
		Show_HUD_Building = true;
		Show_HUD_Briefing = false;
		Show_HUD_MissionOver = false;
		Show_HUD_Book = true;
		RollOverText_Margin = "0,0,-20,156";
		setUpInbuilding(0, 0);
	}

	public void SetVisibleState(bool state)
	{
		if (!state)
		{
			Show_InGameUI = false;
			UIVisible = false;
			Compass_Vis = false;
		}
		else
		{
			UIVisible = true;
			Show_InGameUI = true;
			Compass_Vis = ConfigSettings.Settings_Compass;
		}
	}

	public void CommonRedButtonEnter(object sender, MouseEventArgs e)
	{
		SFXManager.instance.playUISound(134);
	}

	public void TroopsRightClickCommand(object parameter)
	{
		Enums.eChimps chimpEnum = getChimpEnum(parameter as string);
		HUDTroopPanel.RemoveSelectedChimpTypes(chimpEnum, 1);
		EditorDirector.instance.removeSelectedChimpTypes(chimpEnum, 1);
		HUDTroopPanel.SelectedTroops();
	}

	public void TroopsLeftClickCommand(object parameter)
	{
		Enums.eChimps chimpEnum = getChimpEnum(parameter as string);
		HUDTroopPanel.RemoveSelectedChimpTypes(chimpEnum, 0);
		EditorDirector.instance.removeSelectedChimpTypes(chimpEnum, 0);
		HUDTroopPanel.SelectedTroops();
	}

	public TextureSource LoadImageFile(string filePath)
	{
		try
		{
			byte[] data = File.ReadAllBytes(filePath);
			Texture2D texture2D = new Texture2D(2, 2, UnityEngine.TextureFormat.RGBA32, mipChain: false, linear: true);
			texture2D.LoadImage(data);
			UnityEngine.Color[] pixels = texture2D.GetPixels();
			for (int i = 0; i < pixels.Length; i++)
			{
				pixels[i].r *= pixels[i].a;
				pixels[i].g *= pixels[i].a;
				pixels[i].b *= pixels[i].a;
			}
			texture2D.SetPixels(pixels);
			texture2D.Apply();
			TextureSource result = new TextureSource(texture2D);
			UnityEngine.Object.DestroyImmediate(texture2D);
			return result;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public void TestRadarMap()
	{
		radarTexture2D = GameMap.instance.getRadarTexture();
		TextureSource radarMapImage = new TextureSource(radarTexture2D);
		RadarMapImage = radarMapImage;
	}

	private void ButtonFreebuildFunction(object parameter)
	{
		int param = Convert.ToInt32(parameter as string);
		HUDFreebuildMenu.ButtonClicked(param);
	}

	private void OptionsFunction(object parameter)
	{
		int param = Convert.ToInt32(parameter as string);
		HUDOptions.ButtonClicked(param);
	}

	public void StartCampaignMission(int StoryChapter, bool preStartMission6 = false)
	{
		Show_BlackOut = true;
		InitNewScene(Enums.SceneIDS.MainGame);
		Instance.HUDIngameMenu.restartMapInfo = null;
		startedMission = StoryChapter;
		startedMissionPre6 = preStartMission6;
		EditorDirector.instance.LoadCampaignMap(StoryChapter, 1, preStartMission6);
		EngineInterface.GameAction(Enums.GameActionCommand.HideObjectiveProgress, 1, 1);
		Director.instance.SetPostUpdateCallback(delegate
		{
			FatControler.instance.BriefingUIUpdate();
			ButtonGotoBriefing("FromStory");
			InitObjectiveGoodsPanel();
			Show_BlackOut = false;
		});
	}

	public void StartEcoCampaignMission(int EcoMission)
	{
		Show_BlackOut = true;
		InitNewScene(Enums.SceneIDS.MainGame);
		Instance.HUDIngameMenu.restartMapInfo = null;
		startedMission = EcoMission + 32;
		EditorDirector.instance.LoadEcoCampaignMap(EcoMission + 32);
		EngineInterface.GameAction(Enums.GameActionCommand.HideObjectiveProgress, 1, 1);
		Director.instance.SetPostUpdateCallback(delegate
		{
			FatControler.instance.BriefingUIUpdate();
			ButtonGotoBriefing("FromStory");
			InitObjectiveGoodsPanel();
			Show_BlackOut = false;
		});
	}

	public void StartExtraCampaignMissionFromStory(int missionID)
	{
		missionID -= 30;
		StartExtraCampaignMission(missionID / 10, missionID % 10);
	}

	public void StartExtraCampaignMission(int extra, int mission)
	{
		Show_BlackOut = true;
		InitNewScene(Enums.SceneIDS.MainGame);
		Instance.HUDIngameMenu.restartMapInfo = null;
		startedMission = EditorDirector.instance.LoadExtraCampaignMap(extra, mission);
		EngineInterface.GameAction(Enums.GameActionCommand.HideObjectiveProgress, 1, 1);
		Director.instance.SetPostUpdateCallback(delegate
		{
			FatControler.instance.BriefingUIUpdate();
			ButtonGotoBriefing("FromStory");
			InitObjectiveGoodsPanel();
			Show_BlackOut = false;
		});
	}

	public void StartExtraEcoCampaignMissionFromStory(int missionID)
	{
		StartExtraEcoCampaignMission(missionID % 10);
	}

	public void StartExtraEcoCampaignMission(int mission)
	{
		Show_BlackOut = true;
		InitNewScene(Enums.SceneIDS.MainGame);
		Instance.HUDIngameMenu.restartMapInfo = null;
		startedMission = EditorDirector.instance.LoadExtraEcoCampaignMap(mission);
		EngineInterface.GameAction(Enums.GameActionCommand.HideObjectiveProgress, 1, 1);
		Director.instance.SetPostUpdateCallback(delegate
		{
			FatControler.instance.BriefingUIUpdate();
			ButtonGotoBriefing("FromStory");
			InitObjectiveGoodsPanel();
			Show_BlackOut = false;
		});
	}

	public void PreStartMapMission()
	{
		Show_BlackOut = true;
		InitNewScene(Enums.SceneIDS.MainGame);
	}

	public void PostStartMapMission()
	{
		Director.instance.SetPostUpdateCallback(delegate
		{
			FatControler.instance.BriefingUIUpdate();
			ButtonGotoBriefing("FromStory");
			InitObjectiveGoodsPanel();
			Show_BlackOut = false;
		});
	}

	public void SetLocalisedLayout(string locale)
	{
		if (locale == "jaJP")
		{
			ChimpNameTextFontSize = 22.0;
		}
	}

	public void SetNoesisKeyboardState(bool state)
	{
		FatControler.instance.SetNoesisKeyboardState(state);
	}

	public static DependencyObject GetScrollViewer(DependencyObject o)
	{
		if (o is ScrollViewer)
		{
			return o;
		}
		for (int i = 0; i < VisualTreeHelper.GetChildrenCount(o); i++)
		{
			DependencyObject scrollViewer = GetScrollViewer(VisualTreeHelper.GetChild(o, i));
			if (!(scrollViewer == null))
			{
				return scrollViewer;
			}
		}
		return null;
	}

	public static DependencyObject GetListBox(DependencyObject o)
	{
		if (o is ListBox)
		{
			return o;
		}
		for (int i = 0; i < VisualTreeHelper.GetChildrenCount(o); i++)
		{
			DependencyObject listBox = GetListBox(VisualTreeHelper.GetChild(o, i));
			if (!(listBox == null))
			{
				return listBox;
			}
		}
		return null;
	}

	public void SetupSpeech()
	{
		SpeechPaths = new Uri[201];
		SetupSpeechStoryPaths();
	}

	public void PlaySpeechDLC(string setting, int thisSpeech, bool muted = false)
	{
		PlaySpeech(setting, thisSpeech, muted);
	}

	public void PlaySpeech(string setting, int thisSpeech, bool muted = false)
	{
		STORY_Narrative.TriggerEndFromScroller = false;
		switch (setting)
		{
		case "Narrative":
			SpeechNarrativeME.Source = SpeechPaths[thisSpeech];
			SpeechNarrativeME.Play();
			if (!muted)
			{
				SpeechNarrativeME.Volume = ConfigSettings.Settings_MasterVolume * ConfigSettings.Settings_SpeechVolume;
			}
			else
			{
				SpeechNarrativeME.Volume = 0f;
			}
			break;
		case "Heads":
			SpeechHeadsME.Source = SpeechPaths[thisSpeech];
			SpeechHeadsME.Play();
			if (!muted)
			{
				SpeechHeadsME.Volume = ConfigSettings.Settings_MasterVolume * ConfigSettings.Settings_SpeechVolume;
			}
			else
			{
				SpeechHeadsME.Volume = 0f;
			}
			break;
		case "Character":
			SpeechCharacterME.Source = SpeechPaths[thisSpeech];
			SpeechCharacterME.Play();
			if (!muted)
			{
				SpeechCharacterME.Volume = ConfigSettings.Settings_MasterVolume * ConfigSettings.Settings_SpeechVolume;
			}
			else
			{
				SpeechCharacterME.Volume = 0f;
			}
			break;
		case "Video":
			SpeechVideoME.Source = SpeechPaths[thisSpeech];
			SpeechVideoME.Play();
			if (!muted)
			{
				SpeechVideoME.Volume = ConfigSettings.Settings_MasterVolume * ConfigSettings.Settings_SpeechVolume;
			}
			else
			{
				SpeechVideoME.Volume = 0f;
			}
			break;
		}
	}

	public bool StopSpeech()
	{
		if (SpeechNarrativeME == null)
		{
			return false;
		}
		if (SpeechHeadsME == null)
		{
			return false;
		}
		if (SpeechCharacterME == null)
		{
			return false;
		}
		if (SpeechVideoME == null)
		{
			return false;
		}
		SpeechNarrativeME.Close();
		SpeechHeadsME.Close();
		SpeechCharacterME.Close();
		SpeechVideoME.Close();
		SpeechNarrativeME.Source = null;
		SpeechHeadsME.Source = null;
		SpeechCharacterME.Source = null;
		SpeechVideoME.Source = null;
		return true;
	}

	public void CleanupMedia()
	{
		StopSpeech();
		cleanUpHeads();
		if (StoryTitleIntroAnimation != null)
		{
			StoryTitleIntroAnimation.Stop();
		}
		if (StoryNarrativeAnimation != null)
		{
			StoryNarrativeAnimation.Stop();
		}
		if (StoryMapAnimation != null)
		{
			StoryMapAnimation.Stop();
		}
		if (StoryFlagMoveAnimation != null)
		{
			StoryFlagMoveAnimation.Stop();
		}
		if (StoryHeadsFirepitME != null)
		{
			StoryHeadsFirepitME.Stop();
			StoryHeadsFirepitME.Source = null;
			StoryHeadsFirepitME.Close();
		}
		if (StoryHeadsTopHead != null)
		{
			StoryHeadsTopHead.Stop();
			StoryHeadsTopHead.Source = null;
			StoryHeadsTopHead.Close();
		}
		if (StoryHeadsBottomHead != null)
		{
			StoryHeadsBottomHead.Stop();
			StoryHeadsBottomHead.Source = null;
			StoryHeadsBottomHead.Close();
		}
		if (StoryVideoME != null)
		{
			StoryVideoME.Stop();
			StoryVideoME.Source = null;
			StoryVideoME.Close();
		}
		Show_NarrativeBackground_Default = false;
		Show_NarrativeBackground_Extra1A = false;
		Show_NarrativeBackground_Extra1B = false;
		Show_NarrativeBackground_Extra2A = false;
		Show_NarrativeBackground_Extra2B = false;
		Show_NarrativeBackground_Extra3A = false;
		Show_NarrativeBackground_Extra3B = false;
		Show_NarrativeBackground_Extra4A = false;
		Show_NarrativeBackground_Extra4B = false;
		for (int i = 0; i < 6; i++)
		{
			CurrentVideoWait[i] = false;
		}
		StoryHeadsBorderVisible = Visibility.Hidden;
		StoryFirepitVisible = Visibility.Visible;
	}

	private void SetupSpeechStoryPaths()
	{
		string text = "";
		SpeechPaths[1] = new Uri(text + "Assets/GUI/Speech/Story/Act1_1.webm", UriKind.Relative);
		SpeechPaths[2] = new Uri(text + "Assets/GUI/Speech/Story/Act2_1.webm", UriKind.Relative);
		SpeechPaths[3] = new Uri(text + "Assets/GUI/Speech/Story/Act2_2.webm", UriKind.Relative);
		SpeechPaths[4] = new Uri(text + "Assets/GUI/Speech/Story/Act2_3.webm", UriKind.Relative);
		SpeechPaths[5] = new Uri(text + "Assets/GUI/Speech/Story/Act2_4.webm", UriKind.Relative);
		SpeechPaths[6] = new Uri(text + "Assets/GUI/Speech/Story/Act2_5.webm", UriKind.Relative);
		SpeechPaths[7] = new Uri(text + "Assets/GUI/Speech/Story/Act2_6.webm", UriKind.Relative);
		SpeechPaths[8] = new Uri(text + "Assets/GUI/Speech/Story/Act2_7.webm", UriKind.Relative);
		SpeechPaths[9] = new Uri(text + "Assets/GUI/Speech/Story/Act3_1.webm", UriKind.Relative);
		SpeechPaths[10] = new Uri(text + "Assets/GUI/Speech/Story/Act3_2.webm", UriKind.Relative);
		SpeechPaths[11] = new Uri(text + "Assets/GUI/Speech/Story/Act3_3.webm", UriKind.Relative);
		SpeechPaths[12] = new Uri(text + "Assets/GUI/Speech/Story/Act4_1.webm", UriKind.Relative);
		SpeechPaths[13] = new Uri(text + "Assets/GUI/Speech/Story/Act4_2.webm", UriKind.Relative);
		SpeechPaths[14] = new Uri(text + "Assets/GUI/Speech/Story/M10_Brief1.webm", UriKind.Relative);
		SpeechPaths[15] = new Uri(text + "Assets/GUI/Speech/Story/M10_Main1.webm", UriKind.Relative);
		SpeechPaths[16] = new Uri(text + "Assets/GUI/Speech/Story/M10_Post1.webm", UriKind.Relative);
		SpeechPaths[17] = new Uri(text + "Assets/GUI/Speech/Story/M11_Brief1.webm", UriKind.Relative);
		SpeechPaths[18] = new Uri(text + "Assets/GUI/Speech/Story/M11_Main1.webm", UriKind.Relative);
		SpeechPaths[19] = new Uri(text + "Assets/GUI/Speech/Story/M11_Main2.webm", UriKind.Relative);
		SpeechPaths[20] = new Uri(text + "Assets/GUI/Speech/Story/M11_Main3.webm", UriKind.Relative);
		SpeechPaths[21] = new Uri(text + "Assets/GUI/Speech/Story/M11_Main4.webm", UriKind.Relative);
		SpeechPaths[22] = new Uri(text + "Assets/GUI/Speech/Story/M11_Main5.webm", UriKind.Relative);
		SpeechPaths[23] = new Uri(text + "Assets/GUI/Speech/Story/M11_Post1.webm", UriKind.Relative);
		SpeechPaths[24] = new Uri(text + "Assets/GUI/Speech/Story/M12_Brief1.webm", UriKind.Relative);
		SpeechPaths[25] = new Uri(text + "Assets/GUI/Speech/Story/m12_main1.webm", UriKind.Relative);
		SpeechPaths[26] = new Uri(text + "Assets/GUI/Speech/Story/m12_main2.webm", UriKind.Relative);
		SpeechPaths[27] = new Uri(text + "Assets/GUI/Speech/Story/M12_Post1.webm", UriKind.Relative);
		SpeechPaths[28] = new Uri(text + "Assets/GUI/Speech/Story/M12_Post2.webm", UriKind.Relative);
		SpeechPaths[29] = new Uri(text + "Assets/GUI/Speech/Story/M13_Brief1.webm", UriKind.Relative);
		SpeechPaths[30] = new Uri(text + "Assets/GUI/Speech/Story/M13_Main1.webm", UriKind.Relative);
		SpeechPaths[31] = new Uri(text + "Assets/GUI/Speech/Story/M13_Main2.webm", UriKind.Relative);
		SpeechPaths[32] = new Uri(text + "Assets/GUI/Speech/Story/M13_Main3.webm", UriKind.Relative);
		SpeechPaths[33] = new Uri(text + "Assets/GUI/Speech/Story/M13_Post1.webm", UriKind.Relative);
		SpeechPaths[34] = new Uri(text + "Assets/GUI/Speech/Story/M14_Brief1.webm", UriKind.Relative);
		SpeechPaths[35] = new Uri(text + "Assets/GUI/Speech/Story/M14_Main1.webm", UriKind.Relative);
		SpeechPaths[36] = new Uri(text + "Assets/GUI/Speech/Story/M14_Main2.webm", UriKind.Relative);
		SpeechPaths[37] = new Uri(text + "Assets/GUI/Speech/Story/M14_Main3.webm", UriKind.Relative);
		SpeechPaths[38] = new Uri(text + "Assets/GUI/Speech/Story/M14_Main4.webm", UriKind.Relative);
		SpeechPaths[39] = new Uri(text + "Assets/GUI/Speech/Story/M14_Post1.webm", UriKind.Relative);
		SpeechPaths[40] = new Uri(text + "Assets/GUI/Speech/Story/M15_Brief1.webm", UriKind.Relative);
		SpeechPaths[41] = new Uri(text + "Assets/GUI/Speech/Story/M15_Main1.webm", UriKind.Relative);
		SpeechPaths[42] = new Uri(text + "Assets/GUI/Speech/Story/M15_Post1.webm", UriKind.Relative);
		SpeechPaths[43] = new Uri(text + "Assets/GUI/Speech/Story/M15_Post2.webm", UriKind.Relative);
		SpeechPaths[44] = new Uri(text + "Assets/GUI/Speech/Story/M16_Brief1.webm", UriKind.Relative);
		SpeechPaths[45] = new Uri(text + "Assets/GUI/Speech/Story/M16_Main1.webm", UriKind.Relative);
		SpeechPaths[46] = new Uri(text + "Assets/GUI/Speech/Story/M16_Main2.webm", UriKind.Relative);
		SpeechPaths[47] = new Uri(text + "Assets/GUI/Speech/Story/M16_Post1.webm", UriKind.Relative);
		SpeechPaths[48] = new Uri(text + "Assets/GUI/Speech/Story/M17_Brief1.webm", UriKind.Relative);
		SpeechPaths[49] = new Uri(text + "Assets/GUI/Speech/Story/M17_Main1.webm", UriKind.Relative);
		SpeechPaths[50] = new Uri(text + "Assets/GUI/Speech/Story/M17_Post1.webm", UriKind.Relative);
		SpeechPaths[51] = new Uri(text + "Assets/GUI/Speech/Story/M18_Brief1.webm", UriKind.Relative);
		SpeechPaths[52] = new Uri(text + "Assets/GUI/Speech/Story/M18_Main1.webm", UriKind.Relative);
		SpeechPaths[53] = new Uri(text + "Assets/GUI/Speech/Story/M18_Post1.webm", UriKind.Relative);
		SpeechPaths[54] = new Uri(text + "Assets/GUI/Speech/Story/M19_Brief1.webm", UriKind.Relative);
		SpeechPaths[55] = new Uri(text + "Assets/GUI/Speech/Story/M19_Main1.webm", UriKind.Relative);
		SpeechPaths[56] = new Uri(text + "Assets/GUI/Speech/Story/M19_Main2.webm", UriKind.Relative);
		SpeechPaths[57] = new Uri(text + "Assets/GUI/Speech/Story/M19_Main3.webm", UriKind.Relative);
		SpeechPaths[58] = new Uri(text + "Assets/GUI/Speech/Story/M19_Post1.webm", UriKind.Relative);
		SpeechPaths[59] = new Uri(text + "Assets/GUI/Speech/Story/M1_Brief1.webm", UriKind.Relative);
		SpeechPaths[60] = new Uri(text + "Assets/GUI/Speech/Story/M1_Main1.webm", UriKind.Relative);
		SpeechPaths[61] = new Uri(text + "Assets/GUI/Speech/Story/M1_Main2.webm", UriKind.Relative);
		SpeechPaths[62] = new Uri(text + "Assets/GUI/Speech/Story/M1_Main3.webm", UriKind.Relative);
		SpeechPaths[63] = new Uri(text + "Assets/GUI/Speech/Story/M1_Main4.webm", UriKind.Relative);
		SpeechPaths[64] = new Uri(text + "Assets/GUI/Speech/Story/M1_Main5.webm", UriKind.Relative);
		SpeechPaths[65] = new Uri(text + "Assets/GUI/Speech/Story/M1_Main6.webm", UriKind.Relative);
		SpeechPaths[66] = new Uri(text + "Assets/GUI/Speech/Story/M1_Post1.webm", UriKind.Relative);
		SpeechPaths[67] = new Uri(text + "Assets/GUI/Speech/Story/M1_Post2.webm", UriKind.Relative);
		SpeechPaths[68] = new Uri(text + "Assets/GUI/Speech/Story/M1_Post3.webm", UriKind.Relative);
		SpeechPaths[69] = new Uri(text + "Assets/GUI/Speech/Story/M1_Post4.webm", UriKind.Relative);
		SpeechPaths[70] = new Uri(text + "Assets/GUI/Speech/Story/M1_Post5.webm", UriKind.Relative);
		SpeechPaths[71] = new Uri(text + "Assets/GUI/Speech/Story/M20_Brief1.webm", UriKind.Relative);
		SpeechPaths[72] = new Uri(text + "Assets/GUI/Speech/Story/M20_Main1.webm", UriKind.Relative);
		SpeechPaths[73] = new Uri(text + "Assets/GUI/Speech/Story/M20_Main2.webm", UriKind.Relative);
		SpeechPaths[74] = new Uri(text + "Assets/GUI/Speech/Story/M20_Main3.webm", UriKind.Relative);
		SpeechPaths[75] = new Uri(text + "Assets/GUI/Speech/Story/M20_Main4.webm", UriKind.Relative);
		SpeechPaths[76] = new Uri(text + "Assets/GUI/Speech/Story/M20_Post1.webm", UriKind.Relative);
		SpeechPaths[77] = new Uri(text + "Assets/GUI/Speech/Story/M21_Brief1.webm", UriKind.Relative);
		SpeechPaths[78] = new Uri(text + "Assets/GUI/Speech/Story/M21_Main1.webm", UriKind.Relative);
		SpeechPaths[79] = new Uri(text + "Assets/GUI/Speech/Story/M21_Main2.webm", UriKind.Relative);
		SpeechPaths[80] = new Uri(text + "Assets/GUI/Speech/Story/M21_Post1.webm", UriKind.Relative);
		SpeechPaths[81] = new Uri(text + "Assets/GUI/Speech/Story/M21_Post2.webm", UriKind.Relative);
		SpeechPaths[82] = new Uri(text + "Assets/GUI/Speech/Story/M21_Post3.webm", UriKind.Relative);
		SpeechPaths[83] = new Uri(text + "Assets/GUI/Speech/Story/M21_Post4.webm", UriKind.Relative);
		SpeechPaths[84] = new Uri(text + "Assets/GUI/Speech/Story/M2_Brief1.webm", UriKind.Relative);
		SpeechPaths[85] = new Uri(text + "Assets/GUI/Speech/Story/M2_Main1.webm", UriKind.Relative);
		SpeechPaths[86] = new Uri(text + "Assets/GUI/Speech/Story/M2_Main3.webm", UriKind.Relative);
		SpeechPaths[87] = new Uri(text + "Assets/GUI/Speech/Story/M2_Main4.webm", UriKind.Relative);
		SpeechPaths[88] = new Uri(text + "Assets/GUI/Speech/Story/M2_Main5.webm", UriKind.Relative);
		SpeechPaths[89] = new Uri(text + "Assets/GUI/Speech/Story/M2_Post1.webm", UriKind.Relative);
		SpeechPaths[90] = new Uri(text + "Assets/GUI/Speech/Story/M2_Post2.webm", UriKind.Relative);
		SpeechPaths[91] = new Uri(text + "Assets/GUI/Speech/Story/M2_Post3.webm", UriKind.Relative);
		SpeechPaths[92] = new Uri(text + "Assets/GUI/Speech/Story/M33_Brief1.webm", UriKind.Relative);
		SpeechPaths[93] = new Uri(text + "Assets/GUI/Speech/Story/M34_Brief1.webm", UriKind.Relative);
		SpeechPaths[94] = new Uri(text + "Assets/GUI/Speech/Story/M35_Brief1.webm", UriKind.Relative);
		SpeechPaths[95] = new Uri(text + "Assets/GUI/Speech/Story/M36_Brief1.webm", UriKind.Relative);
		SpeechPaths[96] = new Uri(text + "Assets/GUI/Speech/Story/M37_Brief1.webm", UriKind.Relative);
		SpeechPaths[97] = new Uri(text + "Assets/GUI/Speech/Story/M38_Brief1.webm", UriKind.Relative);
		SpeechPaths[98] = new Uri(text + "Assets/GUI/Speech/Story/M39_Brief1.webm", UriKind.Relative);
		SpeechPaths[99] = new Uri(text + "Assets/GUI/Speech/Story/M3_Brief1.webm", UriKind.Relative);
		SpeechPaths[100] = new Uri(text + "Assets/GUI/Speech/Story/M3_Main1.webm", UriKind.Relative);
		SpeechPaths[101] = new Uri(text + "Assets/GUI/Speech/Story/M3_Main2.webm", UriKind.Relative);
		SpeechPaths[102] = new Uri(text + "Assets/GUI/Speech/Story/M3_Main3.webm", UriKind.Relative);
		SpeechPaths[103] = new Uri(text + "Assets/GUI/Speech/Story/M3_Main4.webm", UriKind.Relative);
		SpeechPaths[104] = new Uri(text + "Assets/GUI/Speech/Story/M3_Main5.webm", UriKind.Relative);
		SpeechPaths[105] = new Uri(text + "Assets/GUI/Speech/Story/M3_Post1.webm", UriKind.Relative);
		SpeechPaths[106] = new Uri(text + "Assets/GUI/Speech/Story/M4_Brief1.webm", UriKind.Relative);
		SpeechPaths[107] = new Uri(text + "Assets/GUI/Speech/Story/M4_Main1.webm", UriKind.Relative);
		SpeechPaths[108] = new Uri(text + "Assets/GUI/Speech/Story/M4_Main2.webm", UriKind.Relative);
		SpeechPaths[109] = new Uri(text + "Assets/GUI/Speech/Story/M4_Main3.webm", UriKind.Relative);
		SpeechPaths[110] = new Uri(text + "Assets/GUI/Speech/Story/M4_Post1.webm", UriKind.Relative);
		SpeechPaths[111] = new Uri(text + "Assets/GUI/Speech/Story/M5_Brief1.webm", UriKind.Relative);
		SpeechPaths[112] = new Uri(text + "Assets/GUI/Speech/Story/M5_Main1.webm", UriKind.Relative);
		SpeechPaths[113] = new Uri(text + "Assets/GUI/Speech/Story/M5_Main2.webm", UriKind.Relative);
		SpeechPaths[114] = new Uri(text + "Assets/GUI/Speech/Story/M5_Main3.webm", UriKind.Relative);
		SpeechPaths[115] = new Uri(text + "Assets/GUI/Speech/Story/M5_Main4.webm", UriKind.Relative);
		SpeechPaths[116] = new Uri(text + "Assets/GUI/Speech/Story/M5_Main5.webm", UriKind.Relative);
		SpeechPaths[117] = new Uri(text + "Assets/GUI/Speech/Story/M5_Main6.webm", UriKind.Relative);
		SpeechPaths[118] = new Uri(text + "Assets/GUI/Speech/Story/M5_Main7.webm", UriKind.Relative);
		SpeechPaths[119] = new Uri(text + "Assets/GUI/Speech/Story/M5_Main8.webm", UriKind.Relative);
		SpeechPaths[120] = new Uri(text + "Assets/GUI/Speech/Story/M5_Post1.webm", UriKind.Relative);
		SpeechPaths[121] = new Uri(text + "Assets/GUI/Speech/Story/M6_Brief1.webm", UriKind.Relative);
		SpeechPaths[122] = new Uri(text + "Assets/GUI/Speech/Story/M6_Brief2.webm", UriKind.Relative);
		SpeechPaths[123] = new Uri(text + "Assets/GUI/Speech/Story/M6_Main1.webm", UriKind.Relative);
		SpeechPaths[124] = new Uri(text + "Assets/GUI/Speech/Story/M6_Main2.webm", UriKind.Relative);
		SpeechPaths[125] = new Uri(text + "Assets/GUI/Speech/Story/M6_Main3.webm", UriKind.Relative);
		SpeechPaths[126] = new Uri(text + "Assets/GUI/Speech/Story/M6_Main4.webm", UriKind.Relative);
		SpeechPaths[127] = new Uri(text + "Assets/GUI/Speech/Story/M6_Main5.webm", UriKind.Relative);
		SpeechPaths[128] = new Uri(text + "Assets/GUI/Speech/Story/M6_Post1.webm", UriKind.Relative);
		SpeechPaths[129] = new Uri(text + "Assets/GUI/Speech/Story/M6_Post2.webm", UriKind.Relative);
		SpeechPaths[130] = new Uri(text + "Assets/GUI/Speech/Story/M6_Post3.webm", UriKind.Relative);
		SpeechPaths[131] = new Uri(text + "Assets/GUI/Speech/Story/M6_Post4.webm", UriKind.Relative);
		SpeechPaths[132] = new Uri(text + "Assets/GUI/Speech/Story/M7_Brief1.webm", UriKind.Relative);
		SpeechPaths[133] = new Uri(text + "Assets/GUI/Speech/Story/M7_Main1.webm", UriKind.Relative);
		SpeechPaths[134] = new Uri(text + "Assets/GUI/Speech/Story/M7_Main2.webm", UriKind.Relative);
		SpeechPaths[135] = new Uri(text + "Assets/GUI/Speech/Story/M7_Main3.webm", UriKind.Relative);
		SpeechPaths[136] = new Uri(text + "Assets/GUI/Speech/Story/M7_Main4.webm", UriKind.Relative);
		SpeechPaths[137] = new Uri(text + "Assets/GUI/Speech/Story/M7_Main5.webm", UriKind.Relative);
		SpeechPaths[138] = new Uri(text + "Assets/GUI/Speech/Story/M7_Main6.webm", UriKind.Relative);
		SpeechPaths[139] = new Uri(text + "Assets/GUI/Speech/Story/M7_Main7.webm", UriKind.Relative);
		SpeechPaths[140] = new Uri(text + "Assets/GUI/Speech/Story/M7_Post1.webm", UriKind.Relative);
		SpeechPaths[141] = new Uri(text + "Assets/GUI/Speech/Story/M8_Brief1.webm", UriKind.Relative);
		SpeechPaths[142] = new Uri(text + "Assets/GUI/Speech/Story/M8_Main1.webm", UriKind.Relative);
		SpeechPaths[143] = new Uri(text + "Assets/GUI/Speech/Story/M8_Main2.webm", UriKind.Relative);
		SpeechPaths[144] = new Uri(text + "Assets/GUI/Speech/Story/M8_Main3.webm", UriKind.Relative);
		SpeechPaths[145] = new Uri(text + "Assets/GUI/Speech/Story/M8_Post1.webm", UriKind.Relative);
		SpeechPaths[146] = new Uri(text + "Assets/GUI/Speech/Story/M9_Brief1.webm", UriKind.Relative);
		SpeechPaths[147] = new Uri(text + "Assets/GUI/Speech/Story/M9_Main1.webm", UriKind.Relative);
		SpeechPaths[148] = new Uri(text + "Assets/GUI/Speech/Story/M9_Main2.webm", UriKind.Relative);
		SpeechPaths[149] = new Uri(text + "Assets/GUI/Speech/Story/M9_Main3.webm", UriKind.Relative);
		SpeechPaths[150] = new Uri(text + "Assets/GUI/Speech/Story/M9_Main4.webm", UriKind.Relative);
		SpeechPaths[151] = new Uri(text + "Assets/GUI/Speech/Story/M9_Main5.webm", UriKind.Relative);
		SpeechPaths[152] = new Uri(text + "Assets/GUI/Speech/Story/M9_Main6.webm", UriKind.Relative);
		SpeechPaths[153] = new Uri(text + "Assets/GUI/Speech/Story/M9_Post1.webm", UriKind.Relative);
		SpeechPaths[154] = new Uri(text + "Assets/GUI/Speech/Extra/DLC1/DE_DLC1_PRE.webm", UriKind.Relative);
		SpeechPaths[155] = new Uri(text + "Assets/GUI/Speech/Extra/DLC1/DE_DLC1_M1.webm", UriKind.Relative);
		SpeechPaths[156] = new Uri(text + "Assets/GUI/Speech/Extra/DLC1/DE_DLC1_M2.webm", UriKind.Relative);
		SpeechPaths[157] = new Uri(text + "Assets/GUI/Speech/Extra/DLC1/DE_DLC1_M3.webm", UriKind.Relative);
		SpeechPaths[158] = new Uri(text + "Assets/GUI/Speech/Extra/DLC1/DE_DLC1_M4.webm", UriKind.Relative);
		SpeechPaths[159] = new Uri(text + "Assets/GUI/Speech/Extra/DLC1/DE_DLC1_M5.webm", UriKind.Relative);
		SpeechPaths[160] = new Uri(text + "Assets/GUI/Speech/Extra/DLC1/DE_DLC1_M6.webm", UriKind.Relative);
		SpeechPaths[161] = new Uri(text + "Assets/GUI/Speech/Extra/DLC1/DE_DLC1_M7.webm", UriKind.Relative);
		SpeechPaths[162] = new Uri(text + "Assets/GUI/Speech/Extra/DLC1/DE_DLC1_POST.webm", UriKind.Relative);
		SpeechPaths[163] = new Uri(text + "Assets/GUI/Speech/Extra/DLC2/DE_DLC2_PRE.webm", UriKind.Relative);
		SpeechPaths[164] = new Uri(text + "Assets/GUI/Speech/Extra/DLC2/DE_DLC2_M1.webm", UriKind.Relative);
		SpeechPaths[165] = new Uri(text + "Assets/GUI/Speech/Extra/DLC2/DE_DLC2_M2.webm", UriKind.Relative);
		SpeechPaths[166] = new Uri(text + "Assets/GUI/Speech/Extra/DLC2/DE_DLC2_M3.webm", UriKind.Relative);
		SpeechPaths[167] = new Uri(text + "Assets/GUI/Speech/Extra/DLC2/DE_DLC2_M4.webm", UriKind.Relative);
		SpeechPaths[168] = new Uri(text + "Assets/GUI/Speech/Extra/DLC2/DE_DLC2_M5.webm", UriKind.Relative);
		SpeechPaths[169] = new Uri(text + "Assets/GUI/Speech/Extra/DLC2/DE_DLC2_M6.webm", UriKind.Relative);
		SpeechPaths[170] = new Uri(text + "Assets/GUI/Speech/Extra/DLC2/DE_DLC2_M7.webm", UriKind.Relative);
		SpeechPaths[171] = new Uri(text + "Assets/GUI/Speech/Extra/DLC2/DE_DLC2_POST.webm", UriKind.Relative);
		SpeechPaths[172] = new Uri(text + "Assets/GUI/Speech/Extra/DLC3/DE_DLC3_PRE.webm", UriKind.Relative);
		SpeechPaths[173] = new Uri(text + "Assets/GUI/Speech/Extra/DLC3/DE_DLC3_M1.webm", UriKind.Relative);
		SpeechPaths[174] = new Uri(text + "Assets/GUI/Speech/Extra/DLC3/DE_DLC3_M2.webm", UriKind.Relative);
		SpeechPaths[175] = new Uri(text + "Assets/GUI/Speech/Extra/DLC3/DE_DLC3_M3.webm", UriKind.Relative);
		SpeechPaths[176] = new Uri(text + "Assets/GUI/Speech/Extra/DLC3/DE_DLC3_M4.webm", UriKind.Relative);
		SpeechPaths[177] = new Uri(text + "Assets/GUI/Speech/Extra/DLC3/DE_DLC3_M5.webm", UriKind.Relative);
		SpeechPaths[178] = new Uri(text + "Assets/GUI/Speech/Extra/DLC3/DE_DLC3_M6.webm", UriKind.Relative);
		SpeechPaths[179] = new Uri(text + "Assets/GUI/Speech/Extra/DLC3/DE_DLC3_M7.webm", UriKind.Relative);
		SpeechPaths[180] = new Uri(text + "Assets/GUI/Speech/Extra/DLC3/DE_DLC3_POST.webm", UriKind.Relative);
		SpeechPaths[181] = new Uri(text + "Assets/GUI/Speech/Extra/DLC4/DE_DLC4_PRE.webm", UriKind.Relative);
		SpeechPaths[182] = new Uri(text + "Assets/GUI/Speech/Extra/DLC4/DE_DLC4_M1.webm", UriKind.Relative);
		SpeechPaths[183] = new Uri(text + "Assets/GUI/Speech/Extra/DLC4/DE_DLC4_M2.webm", UriKind.Relative);
		SpeechPaths[184] = new Uri(text + "Assets/GUI/Speech/Extra/DLC4/DE_DLC4_M3.webm", UriKind.Relative);
		SpeechPaths[185] = new Uri(text + "Assets/GUI/Speech/Extra/DLC4/DE_DLC4_M4.webm", UriKind.Relative);
		SpeechPaths[187] = new Uri(text + "Assets/GUI/Speech/Extra/DLC4/DE_DLC4_M5.webm", UriKind.Relative);
		SpeechPaths[188] = new Uri(text + "Assets/GUI/Speech/Extra/DLC4/DE_DLC4_M6.webm", UriKind.Relative);
		SpeechPaths[189] = new Uri(text + "Assets/GUI/Speech/Extra/DLC4/DE_DLC4_M7.webm", UriKind.Relative);
		SpeechPaths[190] = new Uri(text + "Assets/GUI/Speech/Extra/DLC4/DE_DLC4_POST.webm", UriKind.Relative);
		SpeechPaths[191] = new Uri(text + "Assets/GUI/Speech/Extra/DLC5/DE_Eco_Prologue.webm", UriKind.Relative);
		SpeechPaths[192] = new Uri(text + "Assets/GUI/Speech/Extra/DLC5/DE_Eco_M1_Brief.webm", UriKind.Relative);
		SpeechPaths[193] = new Uri(text + "Assets/GUI/Speech/Extra/DLC5/DE_Eco_M2_Brief.webm", UriKind.Relative);
		SpeechPaths[194] = new Uri(text + "Assets/GUI/Speech/Extra/DLC5/DE_Eco_M3_Brief.webm", UriKind.Relative);
		SpeechPaths[195] = new Uri(text + "Assets/GUI/Speech/Extra/DLC5/DE_Eco_M4_Brief.webm", UriKind.Relative);
		SpeechPaths[197] = new Uri(text + "Assets/GUI/Speech/Extra/DLC5/DE_Eco_M5_Brief.webm", UriKind.Relative);
		SpeechPaths[198] = new Uri(text + "Assets/GUI/Speech/Extra/DLC5/DE_Eco_M6_Brief.webm", UriKind.Relative);
		SpeechPaths[199] = new Uri(text + "Assets/GUI/Speech/Extra/DLC5/DE_Eco_M7_Brief.webm", UriKind.Relative);
		SpeechPaths[200] = new Uri(text + "Assets/GUI/Speech/Extra/DLC5/DE_Eco_Epilogue.webm", UriKind.Relative);
	}

	public static void setupImages()
	{
		for (int i = 0; i < 51; i++)
		{
			GameImages.Add(null);
			GameImageData.Add(null);
		}
		Thread thread = new Thread(runImageLoading);
		thread.Name = "Stronghold1DEImageLoading";
		thread.Start();
	}

	private static void runImageLoading()
	{
		int num = 0;
		string[] array = imageFileNames;
		foreach (string text in array)
		{
			if (text != null && text.Length > 0)
			{
				GameImageData[num] = File.ReadAllBytes(text);
			}
			num++;
		}
	}

	public ImageSource GetImage(Enums.eImages imageID)
	{
		if ((int)imageID >= GameImages.Count)
		{
			return null;
		}
		if (GameImages[(int)imageID] == null)
		{
			if (imageFileNames[(int)imageID] != "")
			{
				GameImages[(int)imageID] = new AnImage
				{
					_image = LoadImageFile(imageFileNames[(int)imageID])
				};
				return GameImages[(int)imageID].Image;
			}
			return null;
		}
		return GameImages[(int)imageID].Image;
	}

	public void setupSprites()
	{
		ResourceDictionary applicationResources = Noesis.GUI.GetApplicationResources();
		ImageSource item = (ImageSource)applicationResources["UISprites F007"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites F008"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites F009"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A001"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A002"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A003"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A004"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A005"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A006"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A007"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A008"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A009"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A010"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A011"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A012"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A013"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A014"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A015"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A016"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A017"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A018"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A019"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A020"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites A021"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C001"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C002"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C003"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C004"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C005"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C006"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C007"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C008"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C009"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C010"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C011"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C012"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C013"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C014"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C015"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C016"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C017"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C018"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C019"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C020"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C021"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C022"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C023"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C024"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C025"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C026"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C027"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C028"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C029"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C030"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C031"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C032"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C033"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C034"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C035"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C036"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C037"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C038"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C039"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C040"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C041"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C042"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C043"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe C044"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites E001"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites E005"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites E007"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites E009"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites E023"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats_new-260"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats_new-261"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats_new-262"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats_new-263"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats_new-264"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats_new-265"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats_new-266"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats_new-267"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats_new-268"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats_new-269"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons N013"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons N014"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons N015"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons N016"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-HUD 001"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["ff"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["steam"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["user"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats 036"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats 037"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats 038"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats 041"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats 040"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats 039"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats 046"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats 048"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats 049"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats 127"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["floats 128"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["koth"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["notready_norm"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["notready_over"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["tick_norm"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["tick_over"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H007"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H010"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H013"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H016"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H019"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H022"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H025"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H028"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 001"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 002"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 003"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 004"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 005"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 006"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 007"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 008"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 009"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 010"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 011"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 012"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 013"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 014"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 015"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 016"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 017"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 018"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 019"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 020"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 021"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 022"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 023"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 024"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 025"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 026"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 027"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 028"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 029"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 030"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 031"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 032"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 033"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 034"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 035"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Pie 036"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 000"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 001"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 002"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 003"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 004"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 005"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 006"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 007"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 008"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 009"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 010"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 011"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 012"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 013"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 014"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 015"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 016"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 017"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 018"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 019"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 020"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 021"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 022"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["MarriagePartners 023"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["ReportsShield"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["house_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["woodcutter_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["ox_tether_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["iron_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["pitch_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["hunter_hut_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["fletcher_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["bsmith_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["pole_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["armourer_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["tanner_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["bakery_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["brewer_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["quarry_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["inn_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["healers_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["well_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["oil_smelter_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["wheat_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["hop_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["fruit_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["dairy_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["mill_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["stables_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["church_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["keep_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["campfire_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["tower_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["gallows_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["stocks_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["maypole_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["gardens_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["killing_pits_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["cess_pit_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["stake_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["gibbet_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["dungeon_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["stretching_rack_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["chopping_block_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["ducking_stool_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["dog_cage_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["statue_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["dancing_bear_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["ponds_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["army_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["baker_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["blacksmith_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["chicken_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["child_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["crossbowman_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["drunk_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["farmer_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["fearfactor_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["firewatch_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["food_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["ghost_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["heads_on_spikes_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["healer_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["hunter_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["innkeeper_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["iron_miner_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["jester_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["mother_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["null_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["pitchworker_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["poleturner_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["popularity_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["population_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["priest_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["religion_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["siege_engineer_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["stockpile_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["stone quarry"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["stonemason_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["trader_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["tunnelor_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["weapons_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["wedding_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons J007"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["lady_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["blacksmith_char_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["tanner_char_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["woodcutter_char_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["brewer_char_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["fletcher_char_sketch"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["cutscene_chat_green"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["cutscene_chat_red"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Cursor_over"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Cursor_selected"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Cursor_up"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Sword_over"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Sword_selected"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Sword_up"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons L007"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons L008"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons L009"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons L010"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons L022"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons L023"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons L024"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons L025"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buildings O001"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buildings O003"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buildings O005"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buildings O007"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buildings O009"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buildings O011"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buildings O013"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buildings O015"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buildings O017"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buildings O019"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buildings O021"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buildings O023"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buildings O025"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buildings O027"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buildings O029"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buildings O031"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buildings O033"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buildings M005"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buildings M003"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B001"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B002"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B003"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B004"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B005"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B006"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B007"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B008"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B009"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B010"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B011"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B012"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B013"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B014"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B015"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B016"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B017"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B018"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B019"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B020"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B021"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B022"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B023"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B024"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B025"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B026"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B027"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B028"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B029"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B030"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B031"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B032"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B033"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B034"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B035"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B036"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B037"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B038"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B039"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B040"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B041"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B042"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B043"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Scribe B044"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Scribe_B_over"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Scribe_B_selected"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Scribe_B_up"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Scribe_C_over"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Scribe_C_selected"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Scribe_C_up"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites B002"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites B003"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites B004"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites B005"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites B006"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UISprites B007"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H008"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H011"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H014"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H017"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H020"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H023"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H026"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H029"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H009"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H012"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H015"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H018"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H021"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H024"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H027"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["UI-Buttons H030"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["Slider_Thumb"];
		GameSprites.Add(item);
		item = (ImageSource)applicationResources["notcompleted"];
		GameSprites.Add(item);
		GameSpriteDims = new int[366, 2]
		{
			{ 20, 23 },
			{ 20, 23 },
			{ 19, 23 },
			{ 81, 67 },
			{ 66, 58 },
			{ 77, 71 },
			{ 66, 58 },
			{ 61, 76 },
			{ 33, 80 },
			{ 80, 62 },
			{ 62, 51 },
			{ 64, 48 },
			{ 74, 56 },
			{ 49, 64 },
			{ 86, 45 },
			{ 59, 71 },
			{ 79, 70 },
			{ 79, 56 },
			{ 72, 75 },
			{ 73, 74 },
			{ 78, 77 },
			{ 76, 76 },
			{ 44, 78 },
			{ 75, 73 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 28, 24 },
			{ 23, 22 },
			{ 28, 24 },
			{ 20, 28 },
			{ 27, 11 },
			{ 40, 82 },
			{ 40, 82 },
			{ 40, 82 },
			{ 40, 82 },
			{ 40, 82 },
			{ 40, 82 },
			{ 40, 82 },
			{ 40, 82 },
			{ 40, 82 },
			{ 40, 82 },
			{ 57, 57 },
			{ 57, 57 },
			{ 57, 57 },
			{ 57, 57 },
			{ 1752, 206 },
			{ 60, 60 },
			{ 60, 60 },
			{ 60, 60 },
			{ 124, 90 },
			{ 124, 90 },
			{ 142, 116 },
			{ 142, 116 },
			{ 142, 116 },
			{ 142, 116 },
			{ 152, 150 },
			{ 152, 150 },
			{ 152, 150 },
			{ 120, 90 },
			{ 120, 90 },
			{ 32, 22 },
			{ 90, 116 },
			{ 90, 116 },
			{ 90, 116 },
			{ 90, 116 },
			{ 64, 66 },
			{ 64, 66 },
			{ 64, 66 },
			{ 64, 66 },
			{ 64, 66 },
			{ 64, 66 },
			{ 64, 66 },
			{ 64, 66 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 56, 56 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 100, 100 },
			{ 502, 282 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 68, 68 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 256, 256 },
			{ 256, 256 },
			{ 80, 80 },
			{ 80, 80 },
			{ 80, 80 },
			{ 80, 80 },
			{ 80, 80 },
			{ 80, 80 },
			{ 70, 70 },
			{ 70, 70 },
			{ 70, 70 },
			{ 70, 70 },
			{ 70, 70 },
			{ 70, 70 },
			{ 70, 70 },
			{ 70, 70 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 110, 110 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 1, 1 },
			{ 40, 40 },
			{ 40, 40 }
		};
	}

	public void SetSpriteWidth1(int thisSprite, int scale)
	{
		SpriteWidth1 = GameSpriteDims[thisSprite, 0] * scale / 100;
		SpriteHeight1 = GameSpriteDims[thisSprite, 1] * scale / 100;
	}

	public void SetSpriteWidth2(int thisSprite, int scale)
	{
		SpriteWidth2 = GameSpriteDims[thisSprite, 0] * scale / 100;
		SpriteHeight2 = GameSpriteDims[thisSprite, 1] * scale / 100;
	}

	public void SetSpriteWidth3(int thisSprite, int scale)
	{
		SpriteWidth3 = GameSpriteDims[thisSprite, 0] * scale / 100;
		SpriteHeight3 = GameSpriteDims[thisSprite, 1] * scale / 100;
	}

	public void SetSpriteWidth4(int thisSprite, int scale)
	{
		SpriteWidth4 = GameSpriteDims[thisSprite, 0] * scale / 100;
		SpriteHeight4 = GameSpriteDims[thisSprite, 1] * scale / 100;
	}

	public void SetupVideoPaths()
	{
		VideoPaths = new Uri[97];
		CurrentVideos = new Uri[6];
		CurrentVideoWait = new bool[6];
		CurrentSpeech = new int[6];
		CurrentText = new string[6];
		NameText = new string[2];
		string text = "";
		VideoPaths[1] = new Uri(text + "Assets/GUI/Video/fireplace_01.webm", UriKind.Relative);
		VideoPaths[2] = new Uri(text + "Assets/GUI/Video/intro.webm", UriKind.Relative);
		VideoPaths[3] = new Uri(text + "Assets/GUI/Video/victory_win_game.webm", UriKind.Relative);
		VideoPaths[4] = new Uri(text + "Assets/GUI/Video/well_not_everybody.webm", UriKind.Relative);
		VideoPaths[5] = new Uri(text + "Assets/GUI/Video/Story/ap_civil1_story.webm", UriKind.Relative);
		VideoPaths[7] = new Uri(text + "Assets/GUI/Video/Story/ap_civil3_story.webm", UriKind.Relative);
		VideoPaths[8] = new Uri(text + "Assets/GUI/Video/Story/ap_civil4_story.webm", UriKind.Relative);
		VideoPaths[9] = new Uri(text + "Assets/GUI/Video/Story/ap_civil5_story.webm", UriKind.Relative);
		VideoPaths[10] = new Uri(text + "Assets/GUI/Video/Story/ap_civil6_story.webm", UriKind.Relative);
		VideoPaths[12] = new Uri(text + "Assets/GUI/Video/Story/ap_civil8_story.webm", UriKind.Relative);
		VideoPaths[17] = new Uri(text + "Assets/GUI/Video/Story/ap_civil13_story.webm", UriKind.Relative);
		VideoPaths[18] = new Uri(text + "Assets/GUI/Video/Story/ap_civil14_story.webm", UriKind.Relative);
		VideoPaths[19] = new Uri(text + "Assets/GUI/Video/Story/ap_civil15_story.webm", UriKind.Relative);
		VideoPaths[20] = new Uri(text + "Assets/GUI/Video/Story/ap_civil16_story.webm", UriKind.Relative);
		VideoPaths[34] = new Uri(text + "Assets/GUI/Video/Events/action_apples_die.webm", UriKind.Relative);
		VideoPaths[35] = new Uri(text + "Assets/GUI/Video/Events/action_archers.webm", UriKind.Relative);
		VideoPaths[36] = new Uri(text + "Assets/GUI/Video/Events/action_bandits.webm", UriKind.Relative);
		VideoPaths[41] = new Uri(text + "Assets/GUI/Video/Events/action_mad_cows.webm", UriKind.Relative);
		VideoPaths[37] = new Uri(text + "Assets/GUI/Video/Events/action_fair.webm", UriKind.Relative);
		VideoPaths[38] = new Uri(text + "Assets/GUI/Video/Events/action_fire.webm", UriKind.Relative);
		VideoPaths[39] = new Uri(text + "Assets/GUI/Video/Events/action_hops_die.webm", UriKind.Relative);
		VideoPaths[40] = new Uri(text + "Assets/GUI/Video/Events/action_jester.webm", UriKind.Relative);
		VideoPaths[42] = new Uri(text + "Assets/GUI/Video/Events/action_marriage.webm", UriKind.Relative);
		VideoPaths[43] = new Uri(text + "Assets/GUI/Video/Events/action_plague.webm", UriKind.Relative);
		VideoPaths[44] = new Uri(text + "Assets/GUI/Video/Events/action_rabbits.webm", UriKind.Relative);
		VideoPaths[45] = new Uri(text + "Assets/GUI/Video/Events/action_steal_bread.webm", UriKind.Relative);
		VideoPaths[46] = new Uri(text + "Assets/GUI/Video/Events/action_trees_die.webm", UriKind.Relative);
		VideoPaths[47] = new Uri(text + "Assets/GUI/Video/Events/action_wheat_die.webm", UriKind.Relative);
		VideoPaths[48] = new Uri(text + "Assets/GUI/Video/Events/action_wolves.webm", UriKind.Relative);
		VideoPaths[49] = new Uri(text + "Assets/GUI/Video/Characters/pg_anger1_story.webm", UriKind.Relative);
		VideoPaths[52] = new Uri(text + "Assets/GUI/Video/Characters/pg_taunt1_story.webm", UriKind.Relative);
		VideoPaths[53] = new Uri(text + "Assets/GUI/Video/Characters/pg_taunt2_story.webm", UriKind.Relative);
		VideoPaths[54] = new Uri(text + "Assets/GUI/Video/Characters/pg_vict1_story.webm", UriKind.Relative);
		VideoPaths[55] = new Uri(text + "Assets/GUI/Video/Characters/pg_vict2_story.webm", UriKind.Relative);
		VideoPaths[56] = new Uri(text + "Assets/GUI/Video/Characters/pg_vict3_story.webm", UriKind.Relative);
		VideoPaths[57] = new Uri(text + "Assets/GUI/Video/Characters/rt_anger1_story.webm", UriKind.Relative);
		VideoPaths[58] = new Uri(text + "Assets/GUI/Video/Characters/rt_plead1_story.webm", UriKind.Relative);
		VideoPaths[59] = new Uri(text + "Assets/GUI/Video/Characters/rt_plead2_story.webm", UriKind.Relative);
		VideoPaths[60] = new Uri(text + "Assets/GUI/Video/Characters/rt_plead3_story.webm", UriKind.Relative);
		VideoPaths[64] = new Uri(text + "Assets/GUI/Video/Characters/rt_vict2_story.webm", UriKind.Relative);
		VideoPaths[66] = new Uri(text + "Assets/GUI/Video/Characters/sn_plead1_story.webm", UriKind.Relative);
		VideoPaths[67] = new Uri(text + "Assets/GUI/Video/Characters/sn_plead2_story.webm", UriKind.Relative);
		VideoPaths[68] = new Uri(text + "Assets/GUI/Video/Characters/sn_taunt1_story.webm", UriKind.Relative);
		VideoPaths[70] = new Uri(text + "Assets/GUI/Video/Characters/sn_vict1_story.webm", UriKind.Relative);
		VideoPaths[71] = new Uri(text + "Assets/GUI/Video/Characters/sn_vict2_story.webm", UriKind.Relative);
		VideoPaths[72] = new Uri(text + "Assets/GUI/Video/Characters/wf_anger1_story.webm", UriKind.Relative);
		VideoPaths[75] = new Uri(text + "Assets/GUI/Video/Characters/wf_taunt1_story.webm", UriKind.Relative);
		VideoPaths[76] = new Uri(text + "Assets/GUI/Video/Characters/wf_taunt2_story.webm", UriKind.Relative);
		VideoPaths[77] = new Uri(text + "Assets/GUI/Video/Characters/wf_vict1_story.webm", UriKind.Relative);
		VideoPaths[78] = new Uri(text + "Assets/GUI/Video/Characters/wf_vict2_story.webm", UriKind.Relative);
		VideoPaths[79] = new Uri(text + "Assets/GUI/Video/Buildings/st03_woodcutters_hut.webm", UriKind.Relative);
		VideoPaths[80] = new Uri(text + "Assets/GUI/Video/Buildings/st07_hunters_hut.webm", UriKind.Relative);
		VideoPaths[81] = new Uri(text + "Assets/GUI/Video/Buildings/st12_fletchers_workshop.webm", UriKind.Relative);
		VideoPaths[82] = new Uri(text + "Assets/GUI/Video/Buildings/st13_blacksmiths_workshop.webm", UriKind.Relative);
		VideoPaths[83] = new Uri(text + "Assets/GUI/Video/Buildings/st14_poleturners_workshop.webm", UriKind.Relative);
		VideoPaths[84] = new Uri(text + "Assets/GUI/Video/Buildings/st15_armourers_workshop.webm", UriKind.Relative);
		VideoPaths[85] = new Uri(text + "Assets/GUI/Video/Buildings/st16_tanners_workshop.webm", UriKind.Relative);
		VideoPaths[86] = new Uri(text + "Assets/GUI/Video/Buildings/st17_bakers_workshop.webm", UriKind.Relative);
		VideoPaths[87] = new Uri(text + "Assets/GUI/Video/Buildings/st22_inn.webm", UriKind.Relative);
		VideoPaths[88] = new Uri(text + "Assets/GUI/Video/Buildings/st26_tradepost.webm", UriKind.Relative);
		VideoPaths[89] = new Uri(text + "Assets/GUI/Video/Buildings/st30_wheatfarm.webm", UriKind.Relative);
		VideoPaths[90] = new Uri(text + "Assets/GUI/Video/Buildings/st32_applefarm.webm", UriKind.Relative);
		VideoPaths[91] = new Uri(text + "Assets/GUI/Video/Buildings/st33_cattlefarm.webm", UriKind.Relative);
		VideoPaths[92] = new Uri(text + "Assets/GUI/Video/Buildings/st34_mill.webm", UriKind.Relative);
		VideoPaths[93] = new Uri(text + "Assets/GUI/Video/Buildings/st36_church1.webm", UriKind.Relative);
		VideoPaths[94] = new Uri(text + "Assets/GUI/Video/Buildings/st37_church2.webm", UriKind.Relative);
		VideoPaths[95] = new Uri(text + "Assets/GUI/Video/Buildings/st38_church3.webm", UriKind.Relative);
		VideoPaths[96] = new Uri(text + "Assets/GUI/Video/StoryCastle.webm", UriKind.Relative);
	}

	private void PlayMedia(MediaElement thisClip)
	{
		StoryHeads.Play1(thisClip);
	}

	private void StopMedia(MediaElement thisClip)
	{
		StoryHeads.Stop1(thisClip);
	}

	private void RewindMedia(MediaElement thisClip)
	{
		StoryHeads.Rewind1(thisClip);
	}

	private void ClickDbgPrePost(object parameter)
	{
		if (StoryPhase == 1)
		{
			StoryPhase = 2;
		}
		else if (StoryPhase == 2)
		{
			StoryPhase = 1;
		}
		InitStoryScreenDebugInfo();
	}

	private void ClickDbgStoryChapter(object parameter)
	{
		int storyChapter = Convert.ToInt32(parameter as string);
		StoryChapter = storyChapter;
		InitStoryScreenDebugInfo();
	}

	public void ClickStoryAdvance(object parameter)
	{
		StoryAdvance();
	}

	public void EndAnimStoryAdvance(string typeRunning)
	{
		if (typeRunning == StoryTypeRunning)
		{
			StoryAdvance();
		}
	}

	public void JumpToStoryEnd()
	{
		MyAudioManager.Instance.setMusicFadedState(faded: false);
		StoryStage = 100;
		StoryAdvance();
	}

	public void StoryAdvance()
	{
		CleanupMedia();
		if (StoryActive)
		{
			firepitOn = true;
			StoryStage++;
			if (StoryChapter < 40)
			{
				Show_NarrativeBackground_Default = true;
			}
			else if (StoryChapter < 50)
			{
				Show_NarrativeBackground_Extra1A = true;
			}
			else if (StoryChapter < 60)
			{
				Show_NarrativeBackground_Extra2A = true;
			}
			else if (StoryChapter < 70)
			{
				Show_NarrativeBackground_Extra3A = true;
			}
			else if (StoryChapter < 80)
			{
				Show_NarrativeBackground_Extra4A = true;
			}
			else if (StoryChapter < 90)
			{
				Show_NarrativeBackground_Extra5A = true;
			}
			switch (StoryChapter)
			{
			case 1:
				AdvanceChapter1();
				break;
			case 2:
				AdvanceChapter2();
				break;
			case 3:
				AdvanceChapter3();
				break;
			case 4:
				AdvanceChapter4();
				break;
			case 5:
				AdvanceChapter5();
				break;
			case 6:
				AdvanceChapter6();
				break;
			case 7:
				AdvanceChapter7();
				break;
			case 8:
				AdvanceChapter8();
				break;
			case 9:
				AdvanceChapter9();
				break;
			case 10:
				AdvanceChapter10();
				break;
			case 11:
				AdvanceChapter11();
				break;
			case 12:
				AdvanceChapter12();
				break;
			case 13:
				AdvanceChapter13();
				break;
			case 14:
				AdvanceChapter14();
				break;
			case 15:
				AdvanceChapter15();
				break;
			case 16:
				AdvanceChapter16();
				break;
			case 17:
				AdvanceChapter17();
				break;
			case 18:
				AdvanceChapter18();
				break;
			case 19:
				AdvanceChapter19();
				break;
			case 20:
				AdvanceChapter20();
				break;
			case 21:
				AdvanceChapter21();
				break;
			case 41:
				AdvanceChapter41();
				break;
			case 42:
				AdvanceChapter42();
				break;
			case 43:
				AdvanceChapter43();
				break;
			case 44:
				AdvanceChapter44();
				break;
			case 45:
				AdvanceChapter45();
				break;
			case 46:
				AdvanceChapter46();
				break;
			case 47:
				AdvanceChapter47();
				break;
			case 51:
				AdvanceChapter51();
				break;
			case 52:
				AdvanceChapter52();
				break;
			case 53:
				AdvanceChapter53();
				break;
			case 54:
				AdvanceChapter54();
				break;
			case 55:
				AdvanceChapter55();
				break;
			case 56:
				AdvanceChapter56();
				break;
			case 57:
				AdvanceChapter57();
				break;
			case 61:
				AdvanceChapter61();
				break;
			case 62:
				AdvanceChapter62();
				break;
			case 63:
				AdvanceChapter63();
				break;
			case 64:
				AdvanceChapter64();
				break;
			case 65:
				AdvanceChapter65();
				break;
			case 66:
				AdvanceChapter66();
				break;
			case 67:
				AdvanceChapter67();
				break;
			case 71:
				AdvanceChapter71();
				break;
			case 72:
				AdvanceChapter72();
				break;
			case 73:
				AdvanceChapter73();
				break;
			case 74:
				AdvanceChapter74();
				break;
			case 75:
				AdvanceChapter75();
				break;
			case 76:
				AdvanceChapter76();
				break;
			case 77:
				AdvanceChapter77();
				break;
			case 81:
				AdvanceChapter81();
				break;
			case 82:
				AdvanceChapter82();
				break;
			case 83:
				AdvanceChapter83();
				break;
			case 84:
				AdvanceChapter84();
				break;
			case 85:
				AdvanceChapter85();
				break;
			case 86:
				AdvanceChapter86();
				break;
			case 87:
				AdvanceChapter87();
				break;
			case 22:
			case 23:
			case 24:
			case 25:
			case 26:
			case 27:
			case 28:
			case 29:
			case 30:
			case 31:
			case 32:
			case 33:
			case 34:
			case 35:
			case 36:
			case 37:
			case 38:
			case 39:
			case 40:
			case 48:
			case 49:
			case 50:
			case 58:
			case 59:
			case 60:
			case 68:
			case 69:
			case 70:
			case 78:
			case 79:
			case 80:
				break;
			}
		}
	}

	public void InitialiseStory()
	{
		SetupSpeech();
		SetupVideoPaths();
		InitStoryScreenDebugInfo();
		StoryStage = -1;
	}

	public void InitStoryScreenDebugInfo()
	{
		if (StoryPhase == 1)
		{
			DebugText = "Mission " + StoryChapter + " - Pre";
		}
		else if (StoryPhase == 2)
		{
			DebugText = "Mission " + StoryChapter + " - Post";
		}
		else
		{
			DebugText = "Not set up.";
		}
	}

	public void StartStory(int chapter, int phase)
	{
		StoryActive = true;
		StoryChapter = chapter;
		StoryPhase = phase;
		StoryStage = -1;
	}

	private void AdvanceChapter1()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_003");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_004");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 1);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_006");
				StoryTitleIntroAnimation.Begin();
			}
			else if (StoryStage == 3)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter1TalkingHeadsPre1();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 4)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_016");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 64);
			}
			else if (StoryStage == 5)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter1TalkingHeadsPre2();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 6)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(0, "County0");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartCampaignMission(StoryChapter);
			}
		}
		else
		{
			if (StoryPhase != 2)
			{
				return;
			}
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_021");
				if (StoryNarrativeAnimation != null)
				{
					StoryNarrativeAnimation.Begin();
				}
				PlaySpeech("Narrative", 66);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter1TalkingHeadsPost();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(1, "County1");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter2()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION2_STORY_003");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION2_STORY_004");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 85);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter2TalkingHeadsPre();
				AdvanceTalkingHeads(0);
			}
			else
			{
				StartCampaignMission(StoryChapter);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter2TalkingHeadsPost();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(2, "County0");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter3()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION3_STORY_002");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter3TalkingHeadsPre();
				AdvanceTalkingHeads(0);
			}
			else
			{
				StartCampaignMission(StoryChapter);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION3_STORY_015");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 105);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(3, "County2");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter4()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION4_STORY_002");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION4_STORY_003");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 107);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter4TalkingHeadsPre1();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 3)
			{
				StoryTypeRunning = "Character";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = true;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryCharacter = Translate.Instance.lookUpText("TEXT_MISSION4_STORY_007");
				StoryImage = GetImage(Enums.eImages.IMAGE_RATBIG);
				StoryCharacterAnimation.Begin();
				PlaySpeech("Character", 109);
			}
			else
			{
				StartCampaignMission(StoryChapter);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION4_STORY_009");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 110);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(4, "County3");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter5()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION5_STORY_003");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION5_STORY_004");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 112);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter5TalkingHeadsPre();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 3)
			{
				StoryTypeRunning = "Character";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = true;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryCharacter = Translate.Instance.lookUpText("TEXT_MISSION5_STORY_018");
				StoryImage = GetImage(Enums.eImages.IMAGE_SNAKEBIG);
				StoryCharacterAnimation.Begin();
				PlaySpeech("Character", 119);
			}
			else
			{
				StartCampaignMission(StoryChapter);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION5_STORY_020");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 120);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(5, "County4");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter6()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION6_STORY_002");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter6TalkingHeadsPre1();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION6_STORY_010");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 5);
			}
			else if (StoryStage == 3)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter6TalkingHeadsPre2();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 4)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION6_STORY_016");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 8);
			}
			else if (StoryStage == 5)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION6_STORY_018");
				StoryTitleIntroAnimation.Begin();
			}
			else if (StoryStage == 6)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter6TalkingHeadsPre3();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 7)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION6_STORY_026");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 126);
			}
			else if (StoryStage == 8)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter6TalkingHeadsPre4();
				AdvanceTalkingHeads(0);
			}
			else
			{
				StartCampaignMission(StoryChapter, preStartMission6: true);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter6TalkingHeadsPost();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(6, "County5");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter7()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION7_STORY_002");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				MyAudioManager.Instance.FadeOutMusic();
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter7TalkingHeadsPre1();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 2)
			{
				SFXManager.instance.playMusic(5);
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION7_STORY_006");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 134);
			}
			else if (StoryStage == 3)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter7TalkingHeadsPre2();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 4)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter7TalkingHeadsPre3();
				AdvanceTalkingHeads(0);
			}
			else
			{
				StartCampaignMission(StoryChapter);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter7TalkingHeadsPost();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(7, "County6");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter8()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION8_STORY_003");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION8_STORY_004");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 142);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter8TalkingHeadsPre1();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 3)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION8_STORY_008");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 144);
			}
			else
			{
				StartCampaignMission(StoryChapter);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION8_STORY_010");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 145);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(8, "County7");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter9()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION9_STORY_002");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter9TalkingHeadsPre1();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter9TalkingHeadsPre2();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 3)
			{
				StoryTypeRunning = "Character";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = true;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryCharacter = Translate.Instance.lookUpText("TEXT_MISSION9_STORY_014");
				StoryImage = GetImage(Enums.eImages.IMAGE_PIGBIG);
				StoryCharacterAnimation.Begin();
				PlaySpeech("Character", 152);
			}
			else
			{
				StartCampaignMission(StoryChapter);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION9_STORY_016");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 153);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(9, "County8");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter10()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION10_STORY_002");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter10TalkingHeadsPre1();
				AdvanceTalkingHeads(0);
			}
			else
			{
				StartCampaignMission(StoryChapter);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION10_STORY_006");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 16);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(10, "County9");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter11()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION11_STORY_002");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION11_STORY_003");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 18);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter11TalkingHeadsPre1();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 3)
			{
				StoryTypeRunning = "Character";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = true;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryCharacter = Translate.Instance.lookUpText("TEXT_MISSION11_STORY_011");
				StoryImage = GetImage(Enums.eImages.IMAGE_WOLFBIG);
				StoryCharacterAnimation.Begin();
				PlaySpeech("Character", 22);
			}
			else
			{
				StartCampaignMission(StoryChapter);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION11_STORY_013");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 23);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(11, "County10");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter12()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION12_STORY_002");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION12_STORY_003");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 9);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter12TalkingHeadsPre1();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 3)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION12_STORY_009");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 4)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter12TalkingHeadsPre2();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 5)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION12_STORY_013");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 26);
			}
			else
			{
				StartCampaignMission(StoryChapter);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION12_STORY_015");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 27);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter12TalkingHeadsPost();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(12, "County11");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter13()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION13_STORY_001");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter13TalkingHeadsPre1();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter13TalkingHeadsPre2();
				AdvanceTalkingHeads(0);
			}
			else
			{
				StartCampaignMission(StoryChapter);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION13_STORY_009");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 33);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(13, "County12");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter14()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION14_STORY_002");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION14_STORY_003");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 35);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter14TalkingHeadsPre1();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 3)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter14TalkingHeadsPre2();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 4)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION14_STORY_009");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 38);
			}
			else
			{
				StartCampaignMission(StoryChapter);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION14_STORY_011");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 39);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(14, "County13");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter15()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION15_STORY_001");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION15_STORY_002");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 41);
			}
			else
			{
				StartCampaignMission(StoryChapter);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter15TalkingHeadsPost();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION15_STORY_007");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 43);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(15, "County13");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter16()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION16_STORY_001");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION16_STORY_002");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 12);
				SFXManager.instance.playMusic(6, fadePrevious: true);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter16TalkingHeadsPre1();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 3)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION16_STORY_005");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
				SFXManager.instance.playMusic(4, fadePrevious: true);
			}
			else if (StoryStage == 4)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter16TalkingHeadsPre2();
				AdvanceTalkingHeads(0);
			}
			else
			{
				StartCampaignMission(StoryChapter);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION16_STORY_011");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 47);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(16, "County6");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter17()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION17_STORY_001");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter17TalkingHeadsPre1();
				AdvanceTalkingHeads(0);
			}
			else
			{
				StartCampaignMission(StoryChapter);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION17_STORY_005");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 50);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(17, "County15");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter18()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION18_STORY_001");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter18TalkingHeadsPre1();
				AdvanceTalkingHeads(0);
			}
			else
			{
				StartCampaignMission(StoryChapter);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION18_STORY_005");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 53);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(18, "County14");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter19()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION19_STORY_001");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION19_STORY_002");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 55);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter19TalkingHeadsPre1();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 3)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter19TalkingHeadsPre2();
				AdvanceTalkingHeads(0);
			}
			else
			{
				StartCampaignMission(StoryChapter);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter19TalkingHeadsPost();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(19, "County16");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter20()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION20_STORY_001");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION20_STORY_002");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 72);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter20TalkingHeadsPre1();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 3)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION20_STORY_006");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 74);
			}
			else if (StoryStage == 4)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter20TalkingHeadsPre2();
				AdvanceTalkingHeads(0);
			}
			else
			{
				StartCampaignMission(StoryChapter);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION20_STORY_010");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 76);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(20, "County17");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else
			{
				StoryActive = false;
				StartStory(StoryChapter + 1, 1);
				StoryAdvance();
			}
		}
	}

	private void AdvanceChapter21()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText("TEXT_MISSION21_STORY_001");
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter21TalkingHeadsPre1();
				AdvanceTalkingHeads(0);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Heads";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = true;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				SetUpChapter21TalkingHeadsPre2();
				AdvanceTalkingHeads(0);
			}
			else
			{
				StartCampaignMission(StoryChapter);
				StoryActive = false;
			}
		}
		else if (StoryPhase == 2)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION21_STORY_007");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 80);
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Video";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = true;
				VideoText.Text = "";
				SetupVideo(3, 0);
				AdvanceVideo();
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Map";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = true;
				Show_StoryVideo = false;
				setupMap(21, "County18");
				StoryMapAnimation.Begin();
				StoryFlagMoveAnimation.Begin();
			}
			else if (StoryStage == 3)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText("TEXT_MISSION21_STORY_011");
				StoryNarrativeAnimation.Begin();
				PlaySpeech("Narrative", 82);
			}
			else if (StoryStage == 4)
			{
				StoryTypeRunning = "Video";
				Show_StoryTitle = false;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = true;
				VideoText.Text = Translate.Instance.lookUpText("TEXT_MISSION21_STORY_013");
				SetupVideo(4, 83);
				AdvanceVideo();
			}
			else
			{
				SFXManager.instance.playAdditionalSpeech("Camp_Comp.wav");
				GoToScreen(Enums.SceneIDS.FrontEnd);
				FrontEndMenu.ButtonClicked("Credits");
				StoryActive = false;
			}
		}
	}

	private void AdvanceChapter41()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, 2);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, 3);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 154);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, 5);
				StoryTitleIntroAnimation.Begin();
			}
			else if (StoryStage == 3)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, 6);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 155);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter42()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, 8);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, 9);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 156);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter43()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, 11);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, 12);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 157);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter44()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, 14);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, 15);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 158);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter45()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, 17);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, 18);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 159);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter46()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, 20);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, 21);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 160);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter47()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, 23);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, 24);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 161);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else
		{
			if (StoryPhase != 2)
			{
				return;
			}
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, 26);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_NarrativeBackground_Extra1A = false;
				Show_NarrativeBackground_Extra1B = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_1, 27);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 162);
			}
			else
			{
				SFXManager.instance.playAdditionalSpeech("DE_Camp1_Comp.wav");
				GoToScreen(Enums.SceneIDS.FrontEnd);
				StoryActive = false;
			}
		}
	}

	private void AdvanceChapter51()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, 2);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, 3);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 163);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, 5);
				StoryTitleIntroAnimation.Begin();
			}
			else if (StoryStage == 3)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, 6);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 164);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter52()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, 8);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, 9);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 165);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter53()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, 11);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, 12);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 166);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter54()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, 14);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, 15);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 167);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter55()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, 17);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, 18);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 168);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter56()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, 20);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, 21);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 169);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter57()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, 23);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, 24);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 170);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else
		{
			if (StoryPhase != 2)
			{
				return;
			}
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, 26);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_NarrativeBackground_Extra2A = false;
				Show_NarrativeBackground_Extra2B = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_2, 27);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 171);
			}
			else
			{
				SFXManager.instance.playAdditionalSpeech("DE_Camp2_Comp.wav");
				GoToScreen(Enums.SceneIDS.FrontEnd);
				StoryActive = false;
			}
		}
	}

	private void AdvanceChapter61()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, 2);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, 3);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 172);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, 5);
				StoryTitleIntroAnimation.Begin();
			}
			else if (StoryStage == 3)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, 6);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 173);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter62()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, 8);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, 9);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 174);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter63()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, 11);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, 12);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 175);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter64()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, 14);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, 15);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 176);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter65()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, 17);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, 18);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 177);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter66()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, 20);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, 21);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 178);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter67()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, 23);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, 24);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 179);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else
		{
			if (StoryPhase != 2)
			{
				return;
			}
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, 26);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_NarrativeBackground_Extra3A = false;
				Show_NarrativeBackground_Extra3B = true;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_3, 27);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 180);
			}
			else
			{
				SFXManager.instance.playAdditionalSpeech("DE_Camp3_Comp.wav");
				GoToScreen(Enums.SceneIDS.FrontEnd);
				StoryActive = false;
			}
		}
	}

	private void AdvanceChapter71()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, 2);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, 3);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 181);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, 5);
				StoryTitleIntroAnimation.Begin();
			}
			else if (StoryStage == 3)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, 6);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 182);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter72()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, 8);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, 9);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 183);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter73()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, 11);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, 12);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 184);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter74()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, 14);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, 15);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 185);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter75()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, 17);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, 18);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 187);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter76()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, 20);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, 21);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 188);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter77()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, 23);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, 24);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 189);
			}
			else
			{
				StoryActive = false;
				StartExtraCampaignMissionFromStory(StoryChapter);
			}
		}
		else
		{
			if (StoryPhase != 2)
			{
				return;
			}
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, 26);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_NarrativeBackground_Extra4A = false;
				Show_NarrativeBackground_Extra4B = true;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_4, 27);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 190);
			}
			else
			{
				SFXManager.instance.playAdditionalSpeech("DE_Camp4_Comp.wav");
				GoToScreen(Enums.SceneIDS.FrontEnd);
				StoryActive = false;
			}
		}
	}

	private void AdvanceChapter81()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 2);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 3);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 191, !FatControler.english);
			}
			else if (StoryStage == 2)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 5);
				StoryTitleIntroAnimation.Begin();
			}
			else if (StoryStage == 3)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 6);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 192, !FatControler.english);
			}
			else
			{
				StoryActive = false;
				StartExtraEcoCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter82()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 8);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 9);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 193, !FatControler.english);
			}
			else
			{
				StoryActive = false;
				StartExtraEcoCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter83()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 11);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 12);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 194, !FatControler.english);
			}
			else
			{
				StoryActive = false;
				StartExtraEcoCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter84()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 14);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 15);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 195, !FatControler.english);
			}
			else
			{
				StoryActive = false;
				StartExtraEcoCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter85()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 17);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 18);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 197, !FatControler.english);
			}
			else
			{
				StoryActive = false;
				StartExtraEcoCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter86()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 20);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 21);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 198, !FatControler.english);
			}
			else
			{
				StoryActive = false;
				StartExtraEcoCampaignMissionFromStory(StoryChapter);
			}
		}
		else if (StoryPhase == 2)
		{
			StoryActive = false;
			StartStory(StoryChapter + 1, 1);
			StoryAdvance();
		}
	}

	private void AdvanceChapter87()
	{
		if (StoryPhase == 1)
		{
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 23);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 24);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 199, !FatControler.english);
			}
			else
			{
				StoryActive = false;
				StartExtraEcoCampaignMissionFromStory(StoryChapter);
			}
		}
		else
		{
			if (StoryPhase != 2)
			{
				return;
			}
			if (StoryStage == 0)
			{
				StoryTypeRunning = "Title";
				Show_StoryTitle = true;
				Show_StoryNarrative = false;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 26);
				if (StoryTitleIntroAnimation != null)
				{
					StoryTitleIntroAnimation.Begin();
				}
			}
			else if (StoryStage == 1)
			{
				StoryTypeRunning = "Narrative";
				Show_StoryTitle = false;
				Show_NarrativeBackground_Extra5A = false;
				Show_NarrativeBackground_Extra5B = true;
				Show_StoryNarrative = true;
				Show_StoryHeads = false;
				Show_StoryCharacter = false;
				Show_StoryMap = false;
				Show_StoryVideo = false;
				StoryNarration = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 27);
				StoryNarrativeAnimation.Begin();
				PlaySpeechDLC("Narrative", 200, !FatControler.english);
			}
			else
			{
				SFXManager.instance.playAdditionalSpeech("DE_Eco_Camp_Comp.wav");
				GoToScreen(Enums.SceneIDS.FrontEnd);
				StoryActive = false;
			}
		}
	}

	public void SetupVideo(int thisVideo, int thisSpeech)
	{
		StoryVideoME.Source = VideoPaths[thisVideo];
		StoryVideoME.Volume = ConfigSettings.Settings_SpeechVolume * ConfigSettings.Settings_MasterVolume;
		StoryVideoStage = 0;
		VideoSpeech = thisSpeech;
	}

	public void AdvanceVideo()
	{
		if (StoryVideoStage == 0)
		{
			StoryVideoInAnimation.Begin();
			VideoTextInAnimation.Begin();
			if (VideoSpeech > 0)
			{
				PlaySpeech("Video", VideoSpeech);
			}
			StoryVideoStage = 1;
		}
		else if (StoryVideoStage == 1)
		{
			PlayMedia(StoryVideoME);
			StoryVideoStage = 2;
		}
		else if (StoryVideoStage == 2)
		{
			StoryVideoOutAnimation.Begin();
			VideoTextOutAnimation.Begin();
			StoryVideoStage = 3;
		}
		else
		{
			StoryAdvance();
		}
	}

	public void AdvanceTalkingHeads(int advance)
	{
		if (StoryTypeRunning == "Heads")
		{
			TalkingHeadsStage += advance;
			if (TalkingHeadsStyle == 1)
			{
				AdvanceTalkingHeads1();
			}
			else if (TalkingHeadsStyle == 2)
			{
				AdvanceTalkingHeads2();
			}
			else if (TalkingHeadsStyle == 3)
			{
				AdvanceTalkingHeads3();
			}
			else if (TalkingHeadsStyle == 4)
			{
				AdvanceTalkingHeads4();
			}
			else if (TalkingHeadsStyle == 5)
			{
				AdvanceTalkingHeads5();
			}
			else if (TalkingHeadsStyle == 6)
			{
				AdvanceTalkingHeads6();
			}
		}
		else
		{
			TalkingHeadsStage = 999;
		}
	}

	public void AdvanceTalkingHeads1()
	{
		if (TalkingHeadsStage == 0)
		{
			if (TalkingHeadsCentreStyle == 0)
			{
				StoryHeadsFirepitME.Source = VideoPaths[1];
			}
			else
			{
				StoryHeadsFirepitME.Source = VideoPaths[96];
			}
			if (StoryHeadsFirepitME.IsLoaded)
			{
				PlayMedia(StoryHeadsFirepitME);
			}
			StoryHeadsTopHead.Source = CurrentVideos[0];
			StoryHeads.SetVideoWaitTop(CurrentVideoWait[0]);
			StoryHeadsTopHead.Volume = getBinkVolume(StoryHeadsTopHead.Source.ToString());
			StoryTopHeadGrid.Opacity = 0f;
			StoryBottomHeadGrid.Opacity = 0f;
			StoryFirePitGrid.Opacity = 0f;
			if (firepitOn)
			{
				HeadsFirepitInAnimation.Begin();
			}
			else
			{
				HeadsNoFirepitInAnimation.Begin();
			}
		}
		else if (TalkingHeadsStage == 1)
		{
			PlayMedia(StoryHeadsFirepitME);
			RewindMedia(StoryHeadsTopHead);
			StopMedia(StoryHeadsTopHead);
			Head1InAnimation.Begin();
		}
		else if (TalkingHeadsStage == 2)
		{
			Head1Text.Text = CurrentText[0];
			Name1Text.Text = NameText[0];
			Text1InAnimation.Begin();
			PlayMedia(StoryHeadsTopHead);
			PlaySpeech("Heads", CurrentSpeech[0]);
		}
		else if (TalkingHeadsStage == 3)
		{
			HeadsFirepitOutAnimation.Begin();
			Text1OutAnimation.Begin();
			Head1OutAnimation.Begin();
			Name1Text.Text = "";
		}
		else
		{
			StoryAdvance();
		}
	}

	public void AdvanceTalkingHeads2()
	{
		if (TalkingHeadsStage == 0)
		{
			if (TalkingHeadsCentreStyle == 0)
			{
				StoryHeadsFirepitME.Source = VideoPaths[1];
			}
			else
			{
				StoryHeadsFirepitME.Source = VideoPaths[96];
			}
			StoryFirePitGrid.Opacity = 0f;
			if (StoryHeadsFirepitME.IsLoaded)
			{
				PlayMedia(StoryHeadsFirepitME);
			}
			HeadsFirepitInAnimation.Begin();
			StoryHeadsBottomHead.Source = CurrentVideos[0];
			StoryHeadsBottomHead.Volume = getBinkVolume(StoryHeadsBottomHead.Source.ToString());
			StoryHeadsTopHead.Source = CurrentVideos[1];
			StoryHeadsTopHead.Volume = getBinkVolume(StoryHeadsTopHead.Source.ToString());
		}
		else if (TalkingHeadsStage == 1)
		{
			RewindMedia(StoryHeadsTopHead);
			StopMedia(StoryHeadsTopHead);
			RewindMedia(StoryHeadsBottomHead);
			StopMedia(StoryHeadsBottomHead);
			PlayMedia(StoryHeadsFirepitME);
			StoryTopHeadGrid.Opacity = 0f;
			StoryBottomHeadGrid.Opacity = 0f;
			Head1InAnimation.Begin();
			Head2InAnimation.Begin();
		}
		else if (TalkingHeadsStage == 2)
		{
			Head2Text.Text = CurrentText[0];
			Name2Text.Text = NameText[1];
			Text2InAnimation.Begin();
			PlayMedia(StoryHeadsBottomHead);
			StoryHeads.SetVideoWaitBottom(CurrentVideoWait[0]);
			PlaySpeech("Heads", CurrentSpeech[0]);
		}
		else if (TalkingHeadsStage == 3)
		{
			Text2OutAnimation.Begin();
			Head1Text.Text = CurrentText[1];
			Name1Text.Text = NameText[0];
			Text1InAnimation.Begin();
			PlayMedia(StoryHeadsTopHead);
			StoryHeads.SetVideoWaitTop(CurrentVideoWait[1]);
			PlaySpeech("Heads", CurrentSpeech[1]);
		}
		else if (TalkingHeadsStage == 5)
		{
			Text1OutAnimation.Begin();
			HeadsFirepitOutAnimation.Begin();
			Head1OutAnimation.Begin();
			Head2OutAnimation.Begin();
			Name1Text.Text = "";
			Name2Text.Text = "";
		}
		else
		{
			StoryAdvance();
		}
	}

	public void AdvanceTalkingHeads3()
	{
		if (TalkingHeadsStage == 0)
		{
			if (TalkingHeadsCentreStyle == 0)
			{
				StoryHeadsFirepitME.Source = VideoPaths[1];
			}
			else
			{
				StoryHeadsFirepitME.Source = VideoPaths[96];
			}
			StoryFirePitGrid.Opacity = 0f;
			if (StoryHeadsFirepitME.IsLoaded)
			{
				PlayMedia(StoryHeadsFirepitME);
			}
			HeadsFirepitInAnimation.Begin();
			StoryHeadsBottomHead.Source = CurrentVideos[0];
			StoryHeadsBottomHead.Volume = getBinkVolume(StoryHeadsBottomHead.Source.ToString());
			StoryHeadsTopHead.Source = CurrentVideos[1];
			StoryHeadsTopHead.Volume = getBinkVolume(StoryHeadsTopHead.Source.ToString());
		}
		else if (TalkingHeadsStage == 1)
		{
			RewindMedia(StoryHeadsTopHead);
			StopMedia(StoryHeadsTopHead);
			RewindMedia(StoryHeadsBottomHead);
			StopMedia(StoryHeadsBottomHead);
			PlayMedia(StoryHeadsFirepitME);
			StoryTopHeadGrid.Opacity = 0f;
			StoryBottomHeadGrid.Opacity = 0f;
			Head1InAnimation.Begin();
			Head2InAnimation.Begin();
		}
		else if (TalkingHeadsStage == 2)
		{
			Head2Text.Text = CurrentText[0];
			Name2Text.Text = NameText[1];
			Text2InAnimation.Begin();
			StoryHeads.SetVideoWaitBottom(CurrentVideoWait[0]);
			PlayMedia(StoryHeadsBottomHead);
			PlaySpeech("Heads", CurrentSpeech[0]);
		}
		else if (TalkingHeadsStage == 3)
		{
			Text2OutAnimation.Begin();
			Head1Text.Text = CurrentText[1];
			Name1Text.Text = NameText[0];
			Text1InAnimation.Begin();
			StoryHeads.SetVideoWaitTop(CurrentVideoWait[1]);
			PlayMedia(StoryHeadsTopHead);
			PlaySpeech("Heads", CurrentSpeech[1]);
		}
		else if (TalkingHeadsStage == 4)
		{
			Text1OutAnimation.Begin();
			Head2Text.Text = CurrentText[2];
			Text2InAnimation.Begin();
			StoryHeadsBottomHead.Source = CurrentVideos[2];
			StoryHeadsBottomHead.Volume = getBinkVolume(StoryHeadsBottomHead.Source.ToString());
			Head2SwapAnimation.Begin();
			StoryHeads.SetVideoWaitBottom(CurrentVideoWait[2]);
			PlayMedia(StoryHeadsBottomHead);
			PlaySpeech("Heads", CurrentSpeech[2]);
		}
		else if (TalkingHeadsStage == 5)
		{
			Text2OutAnimation.Begin();
			HeadsFirepitOutAnimation.Begin();
			Head1OutAnimation.Begin();
			Head2OutAnimation.Begin();
			Name1Text.Text = "";
			Name2Text.Text = "";
		}
		else
		{
			StoryAdvance();
		}
	}

	public void AdvanceTalkingHeads4()
	{
		if (TalkingHeadsStage == 0)
		{
			if (TalkingHeadsCentreStyle == 0)
			{
				StoryHeadsFirepitME.Source = VideoPaths[1];
			}
			else
			{
				StoryHeadsFirepitME.Source = VideoPaths[96];
			}
			StoryFirePitGrid.Opacity = 0f;
			if (StoryHeadsFirepitME.IsLoaded)
			{
				PlayMedia(StoryHeadsFirepitME);
			}
			HeadsFirepitInAnimation.Begin();
			StoryHeadsBottomHead.Source = CurrentVideos[0];
			StoryHeadsBottomHead.Volume = getBinkVolume(StoryHeadsBottomHead.Source.ToString());
			StoryHeadsTopHead.Source = CurrentVideos[1];
			StoryHeadsTopHead.Volume = getBinkVolume(StoryHeadsTopHead.Source.ToString());
		}
		else if (TalkingHeadsStage == 1)
		{
			RewindMedia(StoryHeadsTopHead);
			StopMedia(StoryHeadsTopHead);
			RewindMedia(StoryHeadsBottomHead);
			StopMedia(StoryHeadsBottomHead);
			PlayMedia(StoryHeadsFirepitME);
			StoryTopHeadGrid.Opacity = 0f;
			StoryBottomHeadGrid.Opacity = 0f;
			Head1InAnimation.Begin();
			Head2InAnimation.Begin();
		}
		else if (TalkingHeadsStage == 2)
		{
			Head2Text.Text = CurrentText[0];
			Name2Text.Text = NameText[1];
			Text2InAnimation.Begin();
			StoryHeads.SetVideoWaitBottom(CurrentVideoWait[0]);
			PlayMedia(StoryHeadsBottomHead);
			PlaySpeech("Heads", CurrentSpeech[0]);
		}
		else if (TalkingHeadsStage == 3)
		{
			Text2OutAnimation.Begin();
			Head1Text.Text = CurrentText[1];
			Name1Text.Text = NameText[0];
			Text1InAnimation.Begin();
			StoryHeads.SetVideoWaitTop(CurrentVideoWait[1]);
			PlayMedia(StoryHeadsTopHead);
			PlaySpeech("Heads", CurrentSpeech[1]);
		}
		else if (TalkingHeadsStage == 4)
		{
			Text1OutAnimation.Begin();
			Head2Text.Text = CurrentText[2];
			Text2InAnimation.Begin();
			StoryHeadsBottomHead.Source = CurrentVideos[2];
			StoryHeadsBottomHead.Volume = getBinkVolume(StoryHeadsBottomHead.Source.ToString());
			Head2SwapAnimation.Begin();
			StoryHeads.SetVideoWaitBottom(CurrentVideoWait[2]);
			PlayMedia(StoryHeadsBottomHead);
			PlaySpeech("Heads", CurrentSpeech[2]);
		}
		else if (TalkingHeadsStage == 5)
		{
			Text2OutAnimation.Begin();
			Head1Text.Text = CurrentText[3];
			Text1InAnimation.Begin();
			StoryHeadsTopHead.Source = CurrentVideos[3];
			StoryHeadsTopHead.Volume = getBinkVolume(StoryHeadsTopHead.Source.ToString());
			Head1SwapAnimation.Begin();
			StoryHeads.SetVideoWaitTop(CurrentVideoWait[3]);
			PlayMedia(StoryHeadsTopHead);
			PlaySpeech("Heads", CurrentSpeech[3]);
		}
		else if (TalkingHeadsStage == 6)
		{
			HeadsFirepitOutAnimation.Begin();
			Text1OutAnimation.Begin();
			Head1OutAnimation.Begin();
			Head2OutAnimation.Begin();
			Name1Text.Text = "";
			Name2Text.Text = "";
		}
		else
		{
			StoryAdvance();
		}
	}

	public void AdvanceTalkingHeads5()
	{
		if (TalkingHeadsStage == 0)
		{
			if (TalkingHeadsCentreStyle == 0)
			{
				StoryHeadsFirepitME.Source = VideoPaths[1];
			}
			else
			{
				StoryHeadsFirepitME.Source = VideoPaths[96];
			}
			StoryFirePitGrid.Opacity = 0f;
			if (StoryHeadsFirepitME.IsLoaded)
			{
				PlayMedia(StoryHeadsFirepitME);
			}
			HeadsFirepitInAnimation.Begin();
			StoryHeadsBottomHead.Source = CurrentVideos[0];
			StoryHeadsBottomHead.Volume = getBinkVolume(StoryHeadsBottomHead.Source.ToString());
			StoryHeadsTopHead.Source = CurrentVideos[1];
			StoryHeadsTopHead.Volume = getBinkVolume(StoryHeadsTopHead.Source.ToString());
		}
		else if (TalkingHeadsStage == 1)
		{
			RewindMedia(StoryHeadsTopHead);
			StopMedia(StoryHeadsTopHead);
			RewindMedia(StoryHeadsBottomHead);
			StopMedia(StoryHeadsBottomHead);
			PlayMedia(StoryHeadsFirepitME);
			StoryTopHeadGrid.Opacity = 0f;
			StoryBottomHeadGrid.Opacity = 0f;
			Head1InAnimation.Begin();
			Head2InAnimation.Begin();
		}
		else if (TalkingHeadsStage == 2)
		{
			Head2Text.Text = CurrentText[0];
			Name2Text.Text = NameText[1];
			Text2InAnimation.Begin();
			StoryHeads.SetVideoWaitBottom(CurrentVideoWait[0]);
			PlayMedia(StoryHeadsBottomHead);
			PlaySpeech("Heads", CurrentSpeech[0]);
		}
		else if (TalkingHeadsStage == 3)
		{
			Text2OutAnimation.Begin();
			Head1Text.Text = CurrentText[1];
			Name1Text.Text = NameText[0];
			Text1InAnimation.Begin();
			StoryHeads.SetVideoWaitTop(CurrentVideoWait[1]);
			PlayMedia(StoryHeadsTopHead);
			PlaySpeech("Heads", CurrentSpeech[1]);
		}
		else if (TalkingHeadsStage == 4)
		{
			Text1OutAnimation.Begin();
			Head2Text.Text = CurrentText[2];
			Text2InAnimation.Begin();
			StoryHeadsBottomHead.Source = CurrentVideos[2];
			StoryHeadsBottomHead.Volume = getBinkVolume(StoryHeadsBottomHead.Source.ToString());
			Head2SwapAnimation.Begin();
			StoryHeads.SetVideoWaitBottom(CurrentVideoWait[2]);
			PlayMedia(StoryHeadsBottomHead);
			PlaySpeech("Heads", CurrentSpeech[2]);
		}
		else if (TalkingHeadsStage == 5)
		{
			Text2OutAnimation.Begin();
			Head1Text.Text = CurrentText[3];
			Text1InAnimation.Begin();
			StoryHeadsTopHead.Source = CurrentVideos[3];
			StoryHeadsTopHead.Volume = getBinkVolume(StoryHeadsTopHead.Source.ToString());
			Head1SwapAnimation.Begin();
			StoryHeads.SetVideoWaitTop(CurrentVideoWait[3]);
			PlayMedia(StoryHeadsTopHead);
			PlaySpeech("Heads", CurrentSpeech[3]);
		}
		else if (TalkingHeadsStage == 6)
		{
			Text1OutAnimation.Begin();
			Head2Text.Text = CurrentText[4];
			Text2InAnimation.Begin();
			StoryHeadsBottomHead.Source = CurrentVideos[4];
			StoryHeadsBottomHead.Volume = getBinkVolume(StoryHeadsBottomHead.Source.ToString());
			Head2SwapAnimation.Begin();
			StoryHeads.SetVideoWaitBottom(CurrentVideoWait[4]);
			PlayMedia(StoryHeadsBottomHead);
			PlaySpeech("Heads", CurrentSpeech[4]);
		}
		else if (TalkingHeadsStage == 7)
		{
			HeadsFirepitOutAnimation.Begin();
			Text2OutAnimation.Begin();
			Head1OutAnimation.Begin();
			Head2OutAnimation.Begin();
			Name1Text.Text = "";
			Name2Text.Text = "";
		}
		else
		{
			StoryAdvance();
		}
	}

	public void AdvanceTalkingHeads6()
	{
		if (TalkingHeadsStage == 0)
		{
			if (TalkingHeadsCentreStyle == 0)
			{
				StoryHeadsFirepitME.Source = VideoPaths[1];
			}
			else
			{
				StoryHeadsFirepitME.Source = VideoPaths[96];
			}
			StoryFirePitGrid.Opacity = 0f;
			if (StoryHeadsFirepitME.IsLoaded)
			{
				PlayMedia(StoryHeadsFirepitME);
			}
			HeadsFirepitInAnimation.Begin();
			StoryHeadsBottomHead.Source = CurrentVideos[0];
			StoryHeadsBottomHead.Volume = getBinkVolume(StoryHeadsBottomHead.Source.ToString());
			StoryHeadsTopHead.Source = CurrentVideos[1];
			StoryHeadsTopHead.Volume = getBinkVolume(StoryHeadsTopHead.Source.ToString());
		}
		else if (TalkingHeadsStage == 1)
		{
			RewindMedia(StoryHeadsTopHead);
			StopMedia(StoryHeadsTopHead);
			RewindMedia(StoryHeadsBottomHead);
			StopMedia(StoryHeadsBottomHead);
			PlayMedia(StoryHeadsFirepitME);
			StoryTopHeadGrid.Opacity = 0f;
			StoryBottomHeadGrid.Opacity = 0f;
			Head1InAnimation.Begin();
			Head2InAnimation.Begin();
		}
		else if (TalkingHeadsStage == 2)
		{
			Head2Text.Text = CurrentText[0];
			Name2Text.Text = NameText[1];
			Text2InAnimation.Begin();
			StoryHeads.SetVideoWaitBottom(CurrentVideoWait[0]);
			PlayMedia(StoryHeadsBottomHead);
			PlaySpeech("Heads", CurrentSpeech[0]);
		}
		else if (TalkingHeadsStage == 3)
		{
			Text2OutAnimation.Begin();
			Head1Text.Text = CurrentText[1];
			Name1Text.Text = NameText[0];
			Text1InAnimation.Begin();
			StoryHeads.SetVideoWaitTop(CurrentVideoWait[1]);
			PlayMedia(StoryHeadsTopHead);
			PlaySpeech("Heads", CurrentSpeech[1]);
		}
		else if (TalkingHeadsStage == 4)
		{
			Text1OutAnimation.Begin();
			Head2Text.Text = CurrentText[2];
			Text2InAnimation.Begin();
			StoryHeadsBottomHead.Source = CurrentVideos[2];
			StoryHeadsBottomHead.Volume = getBinkVolume(StoryHeadsBottomHead.Source.ToString());
			Head2SwapAnimation.Begin();
			StoryHeads.SetVideoWaitBottom(CurrentVideoWait[2]);
			PlayMedia(StoryHeadsBottomHead);
			PlaySpeech("Heads", CurrentSpeech[2]);
		}
		else if (TalkingHeadsStage == 5)
		{
			Text2OutAnimation.Begin();
			Head1Text.Text = CurrentText[3];
			Text1InAnimation.Begin();
			StoryHeadsTopHead.Source = CurrentVideos[3];
			StoryHeadsTopHead.Volume = getBinkVolume(StoryHeadsTopHead.Source.ToString());
			Head1SwapAnimation.Begin();
			StoryHeads.SetVideoWaitTop(CurrentVideoWait[3]);
			PlayMedia(StoryHeadsTopHead);
			PlaySpeech("Heads", CurrentSpeech[3]);
		}
		else if (TalkingHeadsStage == 6)
		{
			Text1OutAnimation.Begin();
			Head2Text.Text = CurrentText[4];
			Text2InAnimation.Begin();
			StoryHeadsBottomHead.Source = CurrentVideos[4];
			StoryHeadsBottomHead.Volume = getBinkVolume(StoryHeadsBottomHead.Source.ToString());
			Head2SwapAnimation.Begin();
			StoryHeads.SetVideoWaitBottom(CurrentVideoWait[4]);
			PlayMedia(StoryHeadsBottomHead);
			PlaySpeech("Heads", CurrentSpeech[4]);
		}
		else if (TalkingHeadsStage == 7)
		{
			Text2OutAnimation.Begin();
			Head1Text.Text = CurrentText[5];
			Text1InAnimation.Begin();
			StoryHeadsTopHead.Source = CurrentVideos[5];
			StoryHeadsTopHead.Volume = getBinkVolume(StoryHeadsTopHead.Source.ToString());
			Head1SwapAnimation.Begin();
			StoryHeads.SetVideoWaitTop(CurrentVideoWait[5]);
			PlayMedia(StoryHeadsTopHead);
			PlaySpeech("Heads", CurrentSpeech[5]);
		}
		else if (TalkingHeadsStage == 7)
		{
			HeadsFirepitOutAnimation.Begin();
			Text1OutAnimation.Begin();
			Head1OutAnimation.Begin();
			Head2OutAnimation.Begin();
			Name1Text.Text = "";
			Name2Text.Text = "";
		}
		else
		{
			StoryAdvance();
		}
	}

	private void cleanUpHeads()
	{
		if (StopSpeech())
		{
			StopMedia(StoryHeadsTopHead);
			StopMedia(StoryHeadsBottomHead);
			Head1InAnimation.Stop();
			Head2InAnimation.Stop();
			HeadsFirepitInAnimation.Stop();
			HeadsFirepitInAnimation.Remove();
			StoryTopHeadGrid.Opacity = 0f;
			StoryBottomHeadGrid.Opacity = 0f;
			StoryHeadsTopHead.Source = null;
			StoryHeadsTopHead.Volume = getBinkVolume(StoryHeadsTopHead.Source.ToString());
			StoryHeadsBottomHead.Source = null;
			Head1Text.Text = "";
			Head2Text.Text = "";
			Name1Text.Text = "";
			Name2Text.Text = "";
		}
	}

	private void SetUpChapter1TalkingHeadsPre1()
	{
		CurrentVideos[0] = VideoPaths[8];
		CurrentVideos[1] = VideoPaths[12];
		CurrentVideos[2] = VideoPaths[5];
		CurrentVideos[3] = VideoPaths[9];
		CurrentSpeech[0] = 60;
		CurrentSpeech[1] = 61;
		CurrentSpeech[2] = 62;
		CurrentSpeech[3] = 63;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_008");
		CurrentText[1] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_010");
		CurrentText[2] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_012");
		CurrentText[3] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_014");
		CurrentText[4] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_016");
		CurrentText[5] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_018");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_009");
		NameText[1] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_007");
		TalkingHeadsStyle = 4;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private void SetUpChapter1TalkingHeadsPre2()
	{
		CurrentVideos[0] = VideoPaths[12];
		CurrentSpeech[0] = 65;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_018");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_009");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private void SetUpChapter1TalkingHeadsPost()
	{
		CurrentVideos[0] = VideoPaths[8];
		CurrentVideos[1] = VideoPaths[9];
		CurrentVideos[2] = VideoPaths[7];
		CurrentVideos[3] = VideoPaths[10];
		CurrentSpeech[0] = 67;
		CurrentSpeech[1] = 68;
		CurrentSpeech[2] = 69;
		CurrentSpeech[3] = 70;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_023");
		CurrentText[1] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_025");
		CurrentText[2] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_027");
		CurrentText[3] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_029");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_009");
		NameText[1] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_007");
		TalkingHeadsStyle = 4;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private void SetUpChapter2TalkingHeadsPre()
	{
		CurrentVideos[0] = VideoPaths[12];
		CurrentVideos[1] = VideoPaths[7];
		CurrentVideos[2] = VideoPaths[9];
		CurrentSpeech[0] = 86;
		CurrentSpeech[1] = 87;
		CurrentSpeech[2] = 88;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION2_STORY_006");
		CurrentText[1] = Translate.Instance.lookUpText("TEXT_MISSION2_STORY_008");
		CurrentText[2] = Translate.Instance.lookUpText("TEXT_MISSION2_STORY_010");
		NameText[1] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_009");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_007");
		TalkingHeadsStyle = 3;
		TalkingHeadsStage = 0;
	}

	private void SetUpChapter2TalkingHeadsPost()
	{
		CurrentVideos[0] = VideoPaths[12];
		CurrentVideos[1] = VideoPaths[8];
		CurrentVideos[2] = VideoPaths[9];
		CurrentSpeech[0] = 89;
		CurrentSpeech[1] = 90;
		CurrentSpeech[2] = 91;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION2_STORY_013");
		CurrentText[1] = Translate.Instance.lookUpText("TEXT_MISSION2_STORY_015");
		CurrentText[2] = Translate.Instance.lookUpText("TEXT_MISSION2_STORY_017");
		NameText[1] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_009");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_007");
		TalkingHeadsStyle = 3;
		TalkingHeadsStage = 0;
	}

	private void SetUpChapter3TalkingHeadsPre()
	{
		CurrentVideos[0] = VideoPaths[8];
		CurrentVideos[1] = VideoPaths[12];
		CurrentVideos[2] = VideoPaths[7];
		CurrentVideos[3] = VideoPaths[10];
		CurrentVideos[4] = VideoPaths[8];
		CurrentSpeech[0] = 100;
		CurrentSpeech[1] = 101;
		CurrentSpeech[2] = 102;
		CurrentSpeech[3] = 103;
		CurrentSpeech[4] = 104;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION3_STORY_004");
		CurrentText[1] = Translate.Instance.lookUpText("TEXT_MISSION3_STORY_006");
		CurrentText[2] = Translate.Instance.lookUpText("TEXT_MISSION3_STORY_008");
		CurrentText[3] = Translate.Instance.lookUpText("TEXT_MISSION3_STORY_010");
		CurrentText[4] = Translate.Instance.lookUpText("TEXT_MISSION3_STORY_012");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_009");
		NameText[1] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_007");
		TalkingHeadsStyle = 5;
		TalkingHeadsStage = 0;
	}

	private void SetUpChapter4TalkingHeadsPre1()
	{
		CurrentVideos[0] = VideoPaths[8];
		CurrentSpeech[0] = 108;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION4_STORY_005");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_007");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private void SetUpChapter5TalkingHeadsPre()
	{
		StoryHeadsBorderVisible = Visibility.Visible;
		StoryFirepitVisible = Visibility.Visible;
		CurrentVideos[0] = VideoPaths[64];
		CurrentVideos[1] = VideoPaths[68];
		CurrentVideos[2] = VideoPaths[58];
		CurrentVideos[3] = VideoPaths[70];
		CurrentVideos[4] = VideoPaths[57];
		CurrentVideos[5] = VideoPaths[71];
		CurrentSpeech[0] = 113;
		CurrentSpeech[1] = 114;
		CurrentSpeech[2] = 115;
		CurrentSpeech[3] = 116;
		CurrentSpeech[4] = 117;
		CurrentSpeech[5] = 118;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION5_STORY_006");
		CurrentText[1] = Translate.Instance.lookUpText("TEXT_MISSION5_STORY_008");
		CurrentText[2] = Translate.Instance.lookUpText("TEXT_MISSION5_STORY_010");
		CurrentText[3] = Translate.Instance.lookUpText("TEXT_MISSION5_STORY_012");
		CurrentText[4] = Translate.Instance.lookUpText("TEXT_MISSION5_STORY_014");
		CurrentText[5] = Translate.Instance.lookUpText("TEXT_MISSION5_STORY_016");
		NameText[0] = Translate.Instance.lookUpText("TEXT_PREVIEW_005");
		NameText[1] = Translate.Instance.lookUpText("TEXT_PREVIEW_006");
		TalkingHeadsStyle = 6;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 1;
	}

	private void SetUpChapter6TalkingHeadsPre1()
	{
		CurrentVideos[0] = VideoPaths[9];
		CurrentVideos[1] = VideoPaths[8];
		CurrentVideos[2] = VideoPaths[12];
		CurrentSpeech[0] = 2;
		CurrentSpeech[1] = 3;
		CurrentSpeech[2] = 4;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION6_STORY_004");
		CurrentText[1] = Translate.Instance.lookUpText("TEXT_MISSION6_STORY_006");
		CurrentText[2] = Translate.Instance.lookUpText("TEXT_MISSION6_STORY_008");
		NameText[1] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_009");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_007");
		TalkingHeadsStyle = 3;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private void SetUpChapter6TalkingHeadsPre2()
	{
		StoryHeadsBorderVisible = Visibility.Visible;
		StoryFirepitVisible = Visibility.Visible;
		CurrentVideos[0] = VideoPaths[58];
		CurrentVideos[1] = VideoPaths[70];
		CurrentSpeech[0] = 6;
		CurrentSpeech[1] = 7;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION6_STORY_012");
		CurrentText[1] = Translate.Instance.lookUpText("TEXT_MISSION6_STORY_014");
		NameText[0] = Translate.Instance.lookUpText("TEXT_PREVIEW_005");
		NameText[1] = Translate.Instance.lookUpText("TEXT_PREVIEW_006");
		TalkingHeadsStyle = 2;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 1;
	}

	private void SetUpChapter6TalkingHeadsPre3()
	{
		CurrentVideos[0] = VideoPaths[12];
		CurrentVideos[1] = VideoPaths[5];
		CurrentVideos[2] = VideoPaths[9];
		CurrentSpeech[0] = 123;
		CurrentSpeech[1] = 124;
		CurrentSpeech[2] = 125;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION6_STORY_020");
		CurrentText[1] = Translate.Instance.lookUpText("TEXT_MISSION6_STORY_022");
		CurrentText[2] = Translate.Instance.lookUpText("TEXT_MISSION6_STORY_024");
		NameText[1] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_009");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_007");
		TalkingHeadsStyle = 3;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private void SetUpChapter6TalkingHeadsPre4()
	{
		CurrentVideos[0] = VideoPaths[8];
		CurrentSpeech[0] = 127;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION6_STORY_028");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_007");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private void SetUpChapter6TalkingHeadsPost()
	{
		StoryHeadsBorderVisible = Visibility.Visible;
		StoryFirepitVisible = Visibility.Visible;
		CurrentVideos[0] = VideoPaths[57];
		CurrentVideos[1] = VideoPaths[67];
		CurrentVideos[2] = VideoPaths[59];
		CurrentVideos[3] = VideoPaths[71];
		CurrentSpeech[0] = 128;
		CurrentSpeech[1] = 129;
		CurrentSpeech[2] = 130;
		CurrentSpeech[3] = 131;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION6_STORY_031");
		CurrentText[1] = Translate.Instance.lookUpText("TEXT_MISSION6_STORY_033");
		CurrentText[2] = Translate.Instance.lookUpText("TEXT_MISSION6_STORY_035");
		CurrentText[3] = Translate.Instance.lookUpText("TEXT_MISSION6_STORY_037");
		NameText[0] = Translate.Instance.lookUpText("TEXT_PREVIEW_005");
		NameText[1] = Translate.Instance.lookUpText("TEXT_PREVIEW_006");
		TalkingHeadsStyle = 4;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 1;
	}

	private void SetUpChapter7TalkingHeadsPre1()
	{
		CurrentVideos[0] = VideoPaths[17];
		CurrentSpeech[0] = 133;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION7_STORY_004");
		NameText[0] = Translate.Instance.lookUpText("TEXT_CHIMP_NAMES_058");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private void SetUpChapter7TalkingHeadsPre2()
	{
		StoryHeadsBorderVisible = Visibility.Visible;
		StoryFirepitVisible = Visibility.Visible;
		CurrentVideos[0] = VideoPaths[75];
		CurrentVideos[1] = VideoPaths[58];
		CurrentVideos[2] = VideoPaths[72];
		CurrentVideos[3] = VideoPaths[60];
		CurrentVideoWait[3] = true;
		CurrentSpeech[0] = 135;
		CurrentSpeech[1] = 136;
		CurrentSpeech[2] = 137;
		CurrentSpeech[3] = 138;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION7_STORY_008");
		CurrentText[1] = Translate.Instance.lookUpText("TEXT_MISSION7_STORY_010");
		CurrentText[2] = Translate.Instance.lookUpText("TEXT_MISSION7_STORY_012");
		CurrentText[3] = Translate.Instance.lookUpText("TEXT_MISSION7_STORY_014");
		NameText[1] = Translate.Instance.lookUpText("TEXT_PREVIEW_003");
		NameText[0] = Translate.Instance.lookUpText("TEXT_PREVIEW_006");
		TalkingHeadsStyle = 4;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 1;
	}

	private void SetUpChapter7TalkingHeadsPre3()
	{
		CurrentVideos[0] = VideoPaths[5];
		CurrentSpeech[0] = 139;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION7_STORY_016");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_007");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private void SetUpChapter7TalkingHeadsPost()
	{
		StoryHeadsBorderVisible = Visibility.Visible;
		StoryFirepitVisible = Visibility.Visible;
		CurrentVideos[0] = VideoPaths[66];
		CurrentSpeech[0] = 140;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION7_STORY_019");
		NameText[0] = Translate.Instance.lookUpText("TEXT_PREVIEW_005");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 1;
	}

	private void SetUpChapter8TalkingHeadsPre1()
	{
		CurrentVideos[0] = VideoPaths[7];
		CurrentSpeech[0] = 143;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION8_STORY_006");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_007");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private void SetUpChapter9TalkingHeadsPre1()
	{
		CurrentVideos[0] = VideoPaths[5];
		CurrentSpeech[0] = 147;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION9_STORY_004");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_007");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private void SetUpChapter9TalkingHeadsPre2()
	{
		StoryHeadsBorderVisible = Visibility.Visible;
		StoryFirepitVisible = Visibility.Visible;
		CurrentVideos[0] = VideoPaths[75];
		CurrentVideos[1] = VideoPaths[52];
		CurrentVideos[2] = VideoPaths[72];
		CurrentVideos[3] = VideoPaths[53];
		CurrentSpeech[0] = 148;
		CurrentSpeech[1] = 149;
		CurrentSpeech[2] = 150;
		CurrentSpeech[3] = 151;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION9_STORY_006");
		CurrentText[1] = Translate.Instance.lookUpText("TEXT_MISSION9_STORY_008");
		CurrentText[2] = Translate.Instance.lookUpText("TEXT_MISSION9_STORY_010");
		CurrentText[3] = Translate.Instance.lookUpText("TEXT_MISSION9_STORY_012");
		NameText[1] = Translate.Instance.lookUpText("TEXT_PREVIEW_003");
		NameText[0] = Translate.Instance.lookUpText("TEXT_PREVIEW_004");
		TalkingHeadsStyle = 4;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 1;
	}

	private void SetUpChapter10TalkingHeadsPre1()
	{
		CurrentVideos[0] = VideoPaths[8];
		CurrentSpeech[0] = 15;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION10_STORY_004");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_007");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private void SetUpChapter11TalkingHeadsPre1()
	{
		StoryHeadsBorderVisible = Visibility.Visible;
		StoryFirepitVisible = Visibility.Visible;
		CurrentVideos[0] = VideoPaths[77];
		CurrentVideos[1] = VideoPaths[56];
		CurrentVideos[2] = VideoPaths[76];
		CurrentSpeech[0] = 19;
		CurrentSpeech[1] = 20;
		CurrentSpeech[2] = 21;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION11_STORY_005");
		CurrentText[1] = Translate.Instance.lookUpText("TEXT_MISSION11_STORY_007");
		CurrentText[2] = Translate.Instance.lookUpText("TEXT_MISSION11_STORY_009");
		NameText[1] = Translate.Instance.lookUpText("TEXT_PREVIEW_003");
		NameText[0] = Translate.Instance.lookUpText("TEXT_PREVIEW_004");
		TalkingHeadsStyle = 3;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 1;
	}

	private void SetUpChapter12TalkingHeadsPre1()
	{
		StoryHeadsBorderVisible = Visibility.Visible;
		StoryFirepitVisible = Visibility.Visible;
		CurrentVideos[0] = VideoPaths[53];
		CurrentVideos[1] = VideoPaths[76];
		CurrentSpeech[0] = 10;
		CurrentSpeech[1] = 11;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION12_STORY_005");
		CurrentText[1] = Translate.Instance.lookUpText("TEXT_MISSION12_STORY_007");
		NameText[1] = Translate.Instance.lookUpText("TEXT_PREVIEW_004");
		NameText[0] = Translate.Instance.lookUpText("TEXT_PREVIEW_003");
		TalkingHeadsStyle = 2;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 1;
	}

	private void SetUpChapter12TalkingHeadsPre2()
	{
		CurrentVideos[0] = VideoPaths[5];
		CurrentSpeech[0] = 25;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION12_STORY_011");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_007");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private void SetUpChapter12TalkingHeadsPost()
	{
		StoryHeadsBorderVisible = Visibility.Visible;
		StoryFirepitVisible = Visibility.Visible;
		CurrentVideos[0] = VideoPaths[66];
		CurrentSpeech[0] = 28;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION12_STORY_017");
		NameText[0] = Translate.Instance.lookUpText("TEXT_PREVIEW_005");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 1;
	}

	private void SetUpChapter13TalkingHeadsPre1()
	{
		StoryHeadsBorderVisible = Visibility.Visible;
		StoryFirepitVisible = Visibility.Visible;
		CurrentVideos[0] = VideoPaths[67];
		CurrentVideos[1] = VideoPaths[78];
		CurrentSpeech[0] = 30;
		CurrentSpeech[1] = 31;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION13_STORY_003");
		CurrentText[1] = Translate.Instance.lookUpText("TEXT_MISSION13_STORY_005");
		NameText[1] = Translate.Instance.lookUpText("TEXT_PREVIEW_005");
		NameText[0] = Translate.Instance.lookUpText("TEXT_PREVIEW_003");
		TalkingHeadsStyle = 2;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 1;
	}

	private void SetUpChapter13TalkingHeadsPre2()
	{
		StoryHeadsBorderVisible = Visibility.Visible;
		StoryFirepitVisible = Visibility.Visible;
		CurrentVideos[0] = VideoPaths[54];
		CurrentSpeech[0] = 32;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION13_STORY_007");
		NameText[0] = Translate.Instance.lookUpText("TEXT_PREVIEW_004");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 1;
	}

	private void SetUpChapter14TalkingHeadsPre1()
	{
		StoryHeadsBorderVisible = Visibility.Visible;
		StoryFirepitVisible = Visibility.Visible;
		CurrentVideos[0] = VideoPaths[55];
		CurrentSpeech[0] = 36;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION14_STORY_005");
		NameText[0] = Translate.Instance.lookUpText("TEXT_PREVIEW_004");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 1;
	}

	private void SetUpChapter14TalkingHeadsPre2()
	{
		CurrentVideos[0] = VideoPaths[7];
		CurrentSpeech[0] = 37;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION14_STORY_007");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_007");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private void SetUpChapter15TalkingHeadsPost()
	{
		StoryHeadsBorderVisible = Visibility.Visible;
		StoryFirepitVisible = Visibility.Visible;
		CurrentVideos[0] = VideoPaths[49];
		CurrentSpeech[0] = 42;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION15_STORY_005");
		NameText[0] = Translate.Instance.lookUpText("TEXT_PREVIEW_004");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 1;
	}

	private void SetUpChapter16TalkingHeadsPre1()
	{
		firepitOn = true;
		CurrentVideos[0] = VideoPaths[18];
		CurrentSpeech[0] = 13;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION16_STORY_004");
		NameText[0] = Translate.Instance.lookUpText("TEXT_CHIMP_NAMES_038");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private void SetUpChapter16TalkingHeadsPre2()
	{
		StoryHeadsBorderVisible = Visibility.Visible;
		StoryFirepitVisible = Visibility.Visible;
		CurrentVideos[0] = VideoPaths[75];
		CurrentVideos[1] = VideoPaths[52];
		CurrentSpeech[0] = 45;
		CurrentSpeech[1] = 46;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION16_STORY_007");
		CurrentText[1] = Translate.Instance.lookUpText("TEXT_MISSION16_STORY_009");
		NameText[1] = Translate.Instance.lookUpText("TEXT_PREVIEW_003");
		NameText[0] = Translate.Instance.lookUpText("TEXT_PREVIEW_004");
		TalkingHeadsStyle = 2;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 1;
	}

	private void SetUpChapter17TalkingHeadsPre1()
	{
		firepitOn = true;
		CurrentVideos[0] = VideoPaths[19];
		CurrentSpeech[0] = 49;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION17_STORY_003");
		NameText[0] = Translate.Instance.lookUpText("TEXT_CHIMP_NAMES_038");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private void SetUpChapter18TalkingHeadsPre1()
	{
		firepitOn = true;
		CurrentVideos[0] = VideoPaths[8];
		CurrentSpeech[0] = 52;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION18_STORY_003");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_007");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private void SetUpChapter19TalkingHeadsPre1()
	{
		firepitOn = true;
		StoryHeadsBorderVisible = Visibility.Visible;
		StoryFirepitVisible = Visibility.Visible;
		CurrentVideos[0] = VideoPaths[72];
		CurrentSpeech[0] = 56;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION19_STORY_004");
		NameText[0] = Translate.Instance.lookUpText("TEXT_PREVIEW_003");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 1;
	}

	private void SetUpChapter19TalkingHeadsPre2()
	{
		firepitOn = true;
		CurrentVideos[0] = VideoPaths[8];
		CurrentSpeech[0] = 57;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION19_STORY_006");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_007");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private void SetUpChapter19TalkingHeadsPost()
	{
		firepitOn = true;
		StoryHeadsBorderVisible = Visibility.Visible;
		StoryFirepitVisible = Visibility.Visible;
		CurrentVideos[0] = VideoPaths[75];
		CurrentSpeech[0] = 58;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION19_STORY_009");
		NameText[0] = Translate.Instance.lookUpText("TEXT_PREVIEW_003");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 1;
	}

	private void SetUpChapter20TalkingHeadsPre1()
	{
		firepitOn = true;
		StoryHeadsBorderVisible = Visibility.Visible;
		StoryFirepitVisible = Visibility.Visible;
		CurrentVideos[0] = VideoPaths[77];
		CurrentSpeech[0] = 73;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION20_STORY_004");
		NameText[0] = Translate.Instance.lookUpText("TEXT_PREVIEW_003");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 1;
	}

	private void SetUpChapter20TalkingHeadsPre2()
	{
		firepitOn = true;
		CurrentVideos[0] = VideoPaths[5];
		CurrentSpeech[0] = 75;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION20_STORY_008");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION1_STORY_007");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private void SetUpChapter21TalkingHeadsPre1()
	{
		firepitOn = true;
		StoryHeadsBorderVisible = Visibility.Visible;
		StoryFirepitVisible = Visibility.Visible;
		CurrentVideos[0] = VideoPaths[78];
		CurrentSpeech[0] = 78;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION21_STORY_003");
		NameText[0] = Translate.Instance.lookUpText("TEXT_PREVIEW_003");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 1;
	}

	private void SetUpChapter21TalkingHeadsPre2()
	{
		firepitOn = true;
		CurrentVideos[0] = VideoPaths[20];
		CurrentSpeech[0] = 79;
		CurrentText[0] = Translate.Instance.lookUpText("TEXT_MISSION21_STORY_005");
		NameText[0] = Translate.Instance.lookUpText("TEXT_MISSION21_STORY_004");
		TalkingHeadsStyle = 1;
		TalkingHeadsStage = 0;
		TalkingHeadsCentreStyle = 0;
	}

	private float getBinkVolume(string url)
	{
		return SFXManager.instance.getBinkVolume(url);
	}

	public void setupMap(int mapStage, string CountyTofade)
	{
		MapStage = mapStage;
		FadeCounty = CountyTofade;
		StoryImage = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP);
		StoryImage1 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP1);
		StoryImage2 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP2);
		StoryImage3 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP3);
		StoryImage4 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP4);
		StoryImage5 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP5);
		StoryImage6 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP6);
		StoryImage7 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP7);
		StoryImage8 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP8);
		StoryImage9 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP9);
		StoryImage10 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP10);
		StoryImage11 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP11);
		StoryImage12 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP12);
		StoryImage13 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP13);
		StoryImage14 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP14);
		StoryImage15 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP15);
		StoryImage16 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP16);
		StoryImage17 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP17);
		StoryImage18 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP18);
		StoryImage19 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP19);
		StoryImage20 = GetImage(Enums.eImages.IMAGE_CAMPAIGNMAP20);
		StoryMap.Instance.setupMapChapter();
		StoryMap.Instance.lookUpFlagStartPos(Instance.MapStage);
		StoryMap.Instance.lookUpFlagEndPos();
		ShowRat = true;
		ShowSnake = true;
		ShowPig = true;
		ShowWolf = true;
		if (mapStage >= 9)
		{
			ShowRat = false;
		}
		if (mapStage >= 13)
		{
			ShowSnake = false;
		}
		if (mapStage >= 18)
		{
			ShowPig = false;
		}
		if (mapStage >= 21)
		{
			ShowWolf = false;
		}
	}
}
