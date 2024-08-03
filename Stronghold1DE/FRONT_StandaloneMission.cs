using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Noesis;
using UnityEngine;

namespace Stronghold1DE;

public class FRONT_StandaloneMission : UserControl
{
	private ListView RefFileLists;

	private CheckBox RefIncludeUser;

	private CheckBox RefIncludeBuiltin;

	private CheckBox RefIncludeWorkshop;

	private Button RefStandaloneDifficulyLevel;

	private Button RefStandaloneAttackDefend;

	private Button RefStandaloneAdvanced;

	private Button RefStandalonePlayButton;

	private Button RefStandaloneFreebuildAdvanced;

	private RadioButton RefStartingStandaloneArcher;

	private Noesis.Grid RefTroopSetup;

	private Slider RefSiegeTroopsSlider;

	private Noesis.Grid RefCarouselNext;

	private Noesis.Grid RefCarouselPrev;

	private TextBox RefSA_SearchFilter;

	private CheckBox RefShowCompleted;

	private int sortByColumn;

	private bool sortByAscending = true;

	private bool includeUser = true;

	private bool includeBuiltIn = true;

	private bool includeWorkshop = true;

	private bool siegeAttackMode;

	private bool siegeAdvanced;

	private bool freebuildAdvanced;

	private int freebuild_GoldLevel;

	private int freebuild_FoodLevel;

	private int freebuild_ResourcesLevel;

	private int freebuild_WeaponsLevel;

	private int freebuild_RandomEvents;

	private int freebuild_Invasions;

	private int freebuild_InvasionDifficulty;

	private int freebuild_Peacetime = 4;

	private Enums.GameDifficulty difficulty = Enums.GameDifficulty.DIFFICULTY_NORMAL;

	private FileHeader selectedHeader;

	private int[] troop_points_data = new int[11]
	{
		7, 12, 4, 10, 10, 30, 30, 1, 10, 8,
		10
	};

	private int[] troop_points_percents = new int[4] { 167, 100, 75, 50 };

	private int[][] adjusted_start_troops_levels;

	private int[] troop_points_levels = new int[4];

	private int[] adjusted_start_troops = new int[11];

	private int troop_points;

	private int currentTroopID;

	private bool ShowCompletedFlag = true;

	private Enums.eChimps[] siegeTroopsOrder = new Enums.eChimps[11]
	{
		Enums.eChimps.CHIMP_TYPE_ARCHER,
		Enums.eChimps.CHIMP_TYPE_XBOWMAN,
		Enums.eChimps.CHIMP_TYPE_SPEARMAN,
		Enums.eChimps.CHIMP_TYPE_PIKEMAN,
		Enums.eChimps.CHIMP_TYPE_MACEMAN,
		Enums.eChimps.CHIMP_TYPE_SWORDSMAN,
		Enums.eChimps.CHIMP_TYPE_KNIGHT,
		Enums.eChimps.CHIMP_TYPE_LADDERMAN,
		Enums.eChimps.CHIMP_TYPE_ENGINEER,
		Enums.eChimps.CHIMP_TYPE_MONK,
		Enums.eChimps.CHIMP_TYPE_TUNNELER
	};

	private bool panelActive;

	private Enums.StartUpUIPanels missionType;

	private ObservableCollection<FileRow> rows = new ObservableCollection<FileRow>();

	private List<FileHeader> headerlist;

	private DateTime lastScrollTest = DateTime.MinValue;

	public FRONT_StandaloneMission()
	{
		InitializeComponent();
		MainViewModel.Instance.FRONTStandaloneMission = this;
		RefFileLists = (ListView)FindName("MapList");
		RefIncludeUser = (CheckBox)FindName("IncludeUser");
		RefIncludeUser.Checked += Include_ValueChanged;
		RefIncludeUser.Unchecked += Include_ValueChanged;
		RefIncludeBuiltin = (CheckBox)FindName("IncludeBuiltin");
		RefIncludeBuiltin.Checked += Include_ValueChanged;
		RefIncludeBuiltin.Unchecked += Include_ValueChanged;
		RefIncludeWorkshop = (CheckBox)FindName("IncludeWorkshop");
		RefIncludeWorkshop.Checked += Include_ValueChanged;
		RefIncludeWorkshop.Unchecked += Include_ValueChanged;
		RefShowCompleted = (CheckBox)FindName("ShowCompleted");
		RefShowCompleted.Checked += Completed_ValueChanged;
		RefShowCompleted.Unchecked += Completed_ValueChanged;
		RefStandaloneDifficulyLevel = (Button)FindName("StandaloneDifficulyLevel");
		RefStandaloneAttackDefend = (Button)FindName("StandaloneAttackDefend");
		RefStandaloneAdvanced = (Button)FindName("StandaloneAdvanced");
		RefStandaloneFreebuildAdvanced = (Button)FindName("StandaloneFreebuildAdvanced");
		RefStandalonePlayButton = (Button)FindName("StandalonePlayButton");
		RefTroopSetup = (Noesis.Grid)FindName("TroopSetup");
		RefStartingStandaloneArcher = (RadioButton)FindName("StartingStandaloneArcher");
		RefSiegeTroopsSlider = (Slider)FindName("SiegeTroopsSlider");
		RefSiegeTroopsSlider.ValueChanged += SiegeTroopsSlider_ValueChanged;
		RefSA_SearchFilter = (TextBox)FindName("SA_SearchFilter");
		RefSA_SearchFilter.IsKeyboardFocusedChanged += TextInputFocus;
		RefSA_SearchFilter.TextChanged += FilterTextChangedHandler;
		RefSA_SearchFilter.PreviewKeyDown += TextBoxCheckForEscape;
		RefSA_SearchFilter.PreviewTextInput += TextBoxEnterCheck;
		RefCarouselNext = (Noesis.Grid)FindName("CarouselNext");
		RefCarouselPrev = (Noesis.Grid)FindName("CarouselPrev");
		GridView obj = (GridView)RefFileLists.View;
		GridViewColumnHeader obj2 = (GridViewColumnHeader)obj.Columns[1].Header;
		obj2.Content = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 27);
		obj2.Click += FileListHeaderClickedHandler;
		GridViewColumnHeader obj3 = (GridViewColumnHeader)obj.Columns[2].Header;
		obj3.Content = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 28);
		obj3.Click += FileListHeaderClickedHandler;
		GridViewColumnHeader obj4 = (GridViewColumnHeader)obj.Columns[0].Header;
		obj4.Content = "";
		obj4.Click += FileListHeaderClickedHandler;
		RefFileLists.SelectionChanged += delegate
		{
			if (RefFileLists.SelectedItem != null)
			{
				FileHeader fileHeader = ((FileRow)RefFileLists.SelectedItem).fileHeader;
				if (fileHeader != null)
				{
					updateRadarTexture(fileHeader);
					selectedHeader = fileHeader;
					GameData.Instance.SetMissionTextFromHeader(fileHeader);
					MainViewModel.Instance.StandaloneMissionText = GameData.Instance.GetMissionBriefing(fileHeader);
					RefStandalonePlayButton.IsEnabled = true;
					reset_troop_points();
					if (missionType == Enums.StartUpUIPanels.SiegeThat)
					{
						MainViewModel.Instance.SiegeThatHelpVis = Visibility.Hidden;
						UpdateButtons();
						for (int i = 0; i < 11; i++)
						{
							adjusted_start_troops[i] = 0;
						}
						troop_points = 2000;
						siegeAttackMode = false;
						difficulty = Enums.GameDifficulty.DIFFICULTY_NORMAL;
						adjust_troops_difficulty_levels(1);
						set_adjust_troop_difficulty_level((int)difficulty);
						InitSiegeTroopsPanel();
					}
					else if (missionType == Enums.StartUpUIPanels.Siege)
					{
						UpdateButtons();
						int[] scenarioTroopsInfo = EngineInterface.GetScenarioTroopsInfo(selectedHeader.filePath);
						for (int j = 0; j < 11; j++)
						{
							adjusted_start_troops[j] = scenarioTroopsInfo[j] * 7 / 10;
						}
						troop_points = 0;
						adjust_troops_difficulty_levels(1);
						set_adjust_troop_difficulty_level((int)difficulty);
						InitSiegeTroopsPanel();
					}
					else if (missionType == Enums.StartUpUIPanels.FreeBuild)
					{
						UpdateButtons();
					}
				}
			}
		};
	}

	public static void Open(Enums.StartUpUIPanels mode)
	{
		MainViewModel.Instance.FRONTStandaloneMission.doOpen(mode, fromNew: true);
	}

	public void doOpen(Enums.StartUpUIPanels mode, bool fromNew = false)
	{
		panelActive = false;
		if (fromNew)
		{
			MainViewModel.Instance.HUDIngameMenu.restartMapInfo = null;
			sortByColumn = 0;
			sortByAscending = true;
			includeUser = true;
			includeBuiltIn = true;
			includeWorkshop = true;
			RefIncludeBuiltin.IsChecked = true;
			RefIncludeUser.IsChecked = true;
			RefIncludeWorkshop.IsChecked = true;
			siegeAdvanced = false;
			siegeAttackMode = false;
			freebuildAdvanced = false;
			freebuild_GoldLevel = 0;
			freebuild_FoodLevel = 0;
			freebuild_ResourcesLevel = 0;
			freebuild_WeaponsLevel = 0;
			freebuild_RandomEvents = 0;
			freebuild_Invasions = 0;
			freebuild_InvasionDifficulty = 0;
			freebuild_Peacetime = 4;
			ShowCompletedFlag = false;
			RefShowCompleted.IsChecked = false;
			MainViewModel.Instance.StandaloneFilter = "";
			MainViewModel.Instance.StandaloneFilterLabelVis = Visibility.Visible;
			MainViewModel.Instance.StandaloneFilterButtonVis = Visibility.Hidden;
		}
		missionType = mode;
		selectedHeader = null;
		RefStandalonePlayButton.IsEnabled = false;
		MainViewModel.Instance.RadarStandaloneImage = null;
		MainViewModel.Instance.StandaloneMissionText = "";
		MainViewModel.Instance.Show_StandaloneSetup = true;
		RefCarouselNext.Opacity = 0.6f;
		RefCarouselPrev.Opacity = 0.6f;
		difficulty = Enums.GameDifficulty.DIFFICULTY_NORMAL;
		initTroopPoints();
		InitSiegeTroopsPanel();
		UpdateButtons();
		GameData.Instance.game_type = 2;
		MainViewModel.Instance.SiegeThatHelpVis = Visibility.Hidden;
		MainViewModel.Instance.FreeBuildOptionsVis = Visibility.Hidden;
		switch (missionType)
		{
		case Enums.StartUpUIPanels.Siege:
			MainViewModel.Instance.StandalonePrev2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 5);
			MainViewModel.Instance.StandalonePrev = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, 264);
			MainViewModel.Instance.StandaloneTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, 68);
			MainViewModel.Instance.StandaloneNext = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 107);
			MainViewModel.Instance.StandaloneNext2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, 76);
			RefStandaloneDifficulyLevel.Visibility = Visibility.Visible;
			RefStandaloneAttackDefend.Visibility = Visibility.Visible;
			MainViewModel.Instance.SiegeThatHelpButtonVis = Visibility.Hidden;
			break;
		case Enums.StartUpUIPanels.SiegeThat:
			MainViewModel.Instance.StandalonePrev2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, 264);
			MainViewModel.Instance.StandalonePrev = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, 68);
			MainViewModel.Instance.StandaloneTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 107);
			MainViewModel.Instance.StandaloneNext = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, 76);
			MainViewModel.Instance.StandaloneNext2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 5);
			RefStandaloneDifficulyLevel.Visibility = Visibility.Hidden;
			RefStandaloneAttackDefend.Visibility = Visibility.Hidden;
			siegeAdvanced = true;
			siegeAttackMode = true;
			UpdateButtons();
			MainViewModel.Instance.SiegeThatHelpButtonVis = Visibility.Visible;
			break;
		case Enums.StartUpUIPanels.Invasion:
			MainViewModel.Instance.StandalonePrev2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, 68);
			MainViewModel.Instance.StandalonePrev = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 107);
			MainViewModel.Instance.StandaloneTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, 76);
			MainViewModel.Instance.StandaloneNext = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 5);
			MainViewModel.Instance.StandaloneNext2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, 264);
			RefStandaloneDifficulyLevel.Visibility = Visibility.Visible;
			RefStandaloneAttackDefend.Visibility = Visibility.Hidden;
			MainViewModel.Instance.SiegeThatHelpButtonVis = Visibility.Hidden;
			break;
		case Enums.StartUpUIPanels.EcoMission:
			MainViewModel.Instance.StandalonePrev2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 107);
			MainViewModel.Instance.StandalonePrev = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, 76);
			MainViewModel.Instance.StandaloneTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 5);
			MainViewModel.Instance.StandaloneNext = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, 264);
			MainViewModel.Instance.StandaloneNext2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, 68);
			RefStandaloneDifficulyLevel.Visibility = Visibility.Visible;
			RefStandaloneAttackDefend.Visibility = Visibility.Hidden;
			MainViewModel.Instance.SiegeThatHelpButtonVis = Visibility.Hidden;
			break;
		case Enums.StartUpUIPanels.FreeBuild:
			MainViewModel.Instance.StandalonePrev2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, 76);
			MainViewModel.Instance.StandalonePrev = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 5);
			MainViewModel.Instance.StandaloneTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_BUBBLE_HELP_TEXT, 264);
			MainViewModel.Instance.StandaloneNext = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, 68);
			MainViewModel.Instance.StandaloneNext2 = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 107);
			RefStandaloneDifficulyLevel.Visibility = Visibility.Hidden;
			RefStandaloneAttackDefend.Visibility = Visibility.Hidden;
			MainViewModel.Instance.SiegeThatHelpButtonVis = Visibility.Hidden;
			break;
		}
		populateList();
		panelActive = true;
	}

	private void FileListHeaderClickedHandler(object sender, RoutedEventArgs e)
	{
		switch (((GridViewColumnHeader)e.Source).Tag as string)
		{
		case "Name":
			if (sortByColumn == 0)
			{
				sortByAscending = !sortByAscending;
				break;
			}
			sortByColumn = 0;
			sortByAscending = true;
			break;
		case "Date":
			if (sortByColumn == 1)
			{
				sortByAscending = !sortByAscending;
				break;
			}
			sortByColumn = 1;
			sortByAscending = false;
			break;
		}
		populateList();
	}

	private void populateList()
	{
		includeBuiltIn = RefIncludeBuiltin.IsChecked.Value;
		includeUser = RefIncludeUser.IsChecked.Value;
		includeWorkshop = RefIncludeWorkshop.IsChecked.Value;
		headerlist = null;
		bool sortByName = sortByColumn == 0;
		switch (missionType)
		{
		case Enums.StartUpUIPanels.Siege:
			headerlist = MapFileManager.Instance.GetSiegeMaps(sortByName, sortByAscending, includeBuiltIn, includeUser, includeWorkshop);
			break;
		case Enums.StartUpUIPanels.SiegeThat:
			headerlist = MapFileManager.Instance.GetSiegeMaps(sortByName, sortByAscending, includeBuiltIn, includeUser, includeWorkshop, siegeThat: true);
			break;
		case Enums.StartUpUIPanels.Invasion:
			headerlist = MapFileManager.Instance.GetInvasionMaps(sortByName, sortByAscending, includeBuiltIn, includeUser, includeWorkshop);
			break;
		case Enums.StartUpUIPanels.EcoMission:
			headerlist = MapFileManager.Instance.GetEcoMaps(sortByName, sortByAscending, includeBuiltIn, includeUser, includeWorkshop);
			break;
		case Enums.StartUpUIPanels.FreeBuild:
			headerlist = MapFileManager.Instance.GetFreebuildMaps(sortByName, sortByAscending, includeBuiltIn, includeUser, includeWorkshop);
			break;
		}
		if (headerlist == null)
		{
			return;
		}
		string text = RefSA_SearchFilter.Text;
		string value = text.ToLowerInvariant();
		rows.Clear();
		foreach (FileHeader item in headerlist)
		{
			if (text.Length > 0 && !item.display_filename.Contains(text) && !item.display_filename.ToLowerInvariant().Contains(value))
			{
				continue;
			}
			FileRow fileRow = new FileRow();
			fileRow.Text1 = item.display_filename;
			fileRow.Text2 = item.getDateString();
			fileRow.Text3 = "";
			if (!ShowCompletedFlag)
			{
				if (item.builtinMap)
				{
					fileRow.TypeImage = MainViewModel.Instance.GameSprites[88];
				}
				else if (item.workshopMap)
				{
					fileRow.TypeImage = MainViewModel.Instance.GameSprites[89];
				}
				else if (item.userMap)
				{
					fileRow.TypeImage = MainViewModel.Instance.GameSprites[90];
				}
			}
			else if (ConfigSettings.MapCompleted(item.fileName))
			{
				fileRow.TypeImage = MainViewModel.Instance.GameSprites[368];
			}
			else
			{
				fileRow.TypeImage = MainViewModel.Instance.GameSprites[369];
			}
			fileRow.fileHeader = item;
			rows.Add(fileRow);
		}
		RefFileLists.ItemsSource = rows;
	}

	private void updateRadarTexture(FileHeader header)
	{
		if (header != null)
		{
			byte[] radarFromFile = MapFileManager.Instance.GetRadarFromFile(header.filePath);
			if (radarFromFile != null)
			{
				TextureSource radarStandaloneImage = new TextureSource(MapFileManager.Instance.GetRadarPreview(radarFromFile));
				MainViewModel.Instance.RadarStandaloneImage = radarStandaloneImage;
			}
		}
	}

	private void Include_ValueChanged(object sender, RoutedEventArgs e)
	{
		if (panelActive)
		{
			populateList();
		}
	}

	private void Completed_ValueChanged(object sender, RoutedEventArgs e)
	{
		if (panelActive)
		{
			ShowCompletedFlag = RefShowCompleted.IsChecked.Value;
			populateList();
		}
	}

	public void ButtonClicked(string param)
	{
		switch (param)
		{
		case "Back":
			switch (missionType)
			{
			case Enums.StartUpUIPanels.Siege:
			case Enums.StartUpUIPanels.Invasion:
			case Enums.StartUpUIPanels.SiegeThat:
				MainViewModel.Instance.FrontEndMenu.ButtonClicked("Combat");
				break;
			case Enums.StartUpUIPanels.EcoMission:
			case Enums.StartUpUIPanels.FreeBuild:
				MainViewModel.Instance.FrontEndMenu.ButtonClicked("Eco");
				break;
			}
			break;
		case "Play":
		{
			EngineInterface.sendPath(Application.streamingAssetsPath, ConfigSettings.GetMpAutoSavePath(), ConfigSettings.GetSavesPath());
			MainViewModel.Instance.PreStartMapMission();
			HUD_IngameMenu.RestartMapInfo restartMapInfo = new HUD_IngameMenu.RestartMapInfo();
			MainViewModel.Instance.HUDIngameMenu.restartMapInfo = restartMapInfo;
			restartMapInfo.missionType = missionType;
			restartMapInfo.difficulty = difficulty;
			restartMapInfo.siegeAttackMode = siegeAttackMode;
			restartMapInfo.siegeAdvanced = siegeAdvanced;
			restartMapInfo.selectedHeader = selectedHeader;
			for (int i = 0; i < 11; i++)
			{
				restartMapInfo.adjusted_start_troops[i] = adjusted_start_troops[i];
			}
			restartMapInfo.advancedFreebuild = freebuildAdvanced;
			restartMapInfo.freebuild_GoldLevel = freebuild_GoldLevel;
			restartMapInfo.freebuild_FoodLevel = freebuild_FoodLevel;
			restartMapInfo.freebuild_ResourcesLevel = freebuild_ResourcesLevel;
			restartMapInfo.freebuild_WeaponsLevel = freebuild_WeaponsLevel;
			restartMapInfo.freebuild_RandomEvents = freebuild_RandomEvents;
			restartMapInfo.freebuild_Invasions = freebuild_Invasions;
			restartMapInfo.freebuild_InvasionDifficulty = freebuild_InvasionDifficulty;
			restartMapInfo.freebuild_Peacetime = freebuild_Peacetime;
			StartMap(restartMapInfo);
			break;
		}
		case "Next":
			switch (missionType)
			{
			case Enums.StartUpUIPanels.Siege:
				doOpen(Enums.StartUpUIPanels.SiegeThat);
				break;
			case Enums.StartUpUIPanels.SiegeThat:
				doOpen(Enums.StartUpUIPanels.Invasion);
				break;
			case Enums.StartUpUIPanels.Invasion:
				doOpen(Enums.StartUpUIPanels.EcoMission);
				break;
			case Enums.StartUpUIPanels.EcoMission:
				doOpen(Enums.StartUpUIPanels.FreeBuild);
				break;
			case Enums.StartUpUIPanels.FreeBuild:
				doOpen(Enums.StartUpUIPanels.Siege);
				break;
			}
			break;
		case "Next2":
			switch (missionType)
			{
			case Enums.StartUpUIPanels.Siege:
				doOpen(Enums.StartUpUIPanels.Invasion);
				break;
			case Enums.StartUpUIPanels.SiegeThat:
				doOpen(Enums.StartUpUIPanels.EcoMission);
				break;
			case Enums.StartUpUIPanels.Invasion:
				doOpen(Enums.StartUpUIPanels.FreeBuild);
				break;
			case Enums.StartUpUIPanels.EcoMission:
				doOpen(Enums.StartUpUIPanels.Siege);
				break;
			case Enums.StartUpUIPanels.FreeBuild:
				doOpen(Enums.StartUpUIPanels.SiegeThat);
				break;
			}
			break;
		case "Prev":
			switch (missionType)
			{
			case Enums.StartUpUIPanels.Siege:
				doOpen(Enums.StartUpUIPanels.FreeBuild);
				break;
			case Enums.StartUpUIPanels.SiegeThat:
				doOpen(Enums.StartUpUIPanels.Siege);
				break;
			case Enums.StartUpUIPanels.Invasion:
				doOpen(Enums.StartUpUIPanels.SiegeThat);
				break;
			case Enums.StartUpUIPanels.EcoMission:
				doOpen(Enums.StartUpUIPanels.Invasion);
				break;
			case Enums.StartUpUIPanels.FreeBuild:
				doOpen(Enums.StartUpUIPanels.EcoMission);
				break;
			}
			break;
		case "Prev2":
			switch (missionType)
			{
			case Enums.StartUpUIPanels.Siege:
				doOpen(Enums.StartUpUIPanels.EcoMission);
				break;
			case Enums.StartUpUIPanels.SiegeThat:
				doOpen(Enums.StartUpUIPanels.FreeBuild);
				break;
			case Enums.StartUpUIPanels.Invasion:
				doOpen(Enums.StartUpUIPanels.Siege);
				break;
			case Enums.StartUpUIPanels.EcoMission:
				doOpen(Enums.StartUpUIPanels.SiegeThat);
				break;
			case Enums.StartUpUIPanels.FreeBuild:
				doOpen(Enums.StartUpUIPanels.Invasion);
				break;
			}
			break;
		case "Difficulty":
			if (difficulty == Enums.GameDifficulty.DIFFICULTY_VERYHARD)
			{
				difficulty = Enums.GameDifficulty.DIFFICULTY_EASY;
			}
			else
			{
				difficulty++;
			}
			UpdateButtons();
			if (missionType == Enums.StartUpUIPanels.Siege)
			{
				set_adjust_troop_difficulty_level((int)difficulty);
				UpdateSiegeTroopValues();
				UpdateCurrentSiegeTroop();
			}
			break;
		case "AttackDefend":
			siegeAttackMode = !siegeAttackMode;
			UpdateButtons();
			break;
		case "Advanced":
			siegeAdvanced = !siegeAdvanced;
			UpdateButtons();
			break;
		case "FreebuildAdvanced":
			freebuildAdvanced = !freebuildAdvanced;
			if (freebuildAdvanced)
			{
				MainViewModel.Instance.FreeBuildOptionsVis = Visibility.Visible;
				UpdateFreebuildValues();
			}
			else
			{
				MainViewModel.Instance.FreeBuildOptionsVis = Visibility.Hidden;
			}
			break;
		case "0":
		case "1":
		case "2":
		case "3":
		case "4":
		case "5":
		case "6":
		case "7":
		case "8":
		case "9":
		case "10":
		{
			int num = Convert.ToInt32(param);
			currentTroopID = num;
			UpdateCurrentSiegeTroop();
			MainViewModel.Instance.StandaloneSiegePoints = troop_points.ToString();
			break;
		}
		case "SiegeThatHelp":
			if (MainViewModel.Instance.SiegeThatHelpVis == Visibility.Hidden)
			{
				MainViewModel.Instance.SiegeThatHelpVis = Visibility.Visible;
			}
			else
			{
				MainViewModel.Instance.SiegeThatHelpVis = Visibility.Hidden;
			}
			UpdateButtons();
			break;
		case "MouseEnter_Next":
			RefCarouselNext.Opacity = 0.9f;
			break;
		case "MouseLeave_Next":
			RefCarouselNext.Opacity = 0.6f;
			break;
		case "MouseEnter_Prev":
			RefCarouselPrev.Opacity = 0.9f;
			break;
		case "MouseLeave_Prev":
			RefCarouselPrev.Opacity = 0.6f;
			break;
		case "Freebuild_Gold":
			freebuild_GoldLevel++;
			if (freebuild_GoldLevel >= 6)
			{
				freebuild_GoldLevel = 0;
			}
			UpdateFreebuildValues();
			break;
		case "Freebuild_Food":
			freebuild_FoodLevel++;
			if (freebuild_FoodLevel >= 6)
			{
				freebuild_FoodLevel = 0;
			}
			UpdateFreebuildValues();
			break;
		case "Freebuild_Resources":
			freebuild_ResourcesLevel++;
			if (freebuild_ResourcesLevel >= 6)
			{
				freebuild_ResourcesLevel = 0;
			}
			UpdateFreebuildValues();
			break;
		case "Freebuild_Weapons":
			freebuild_WeaponsLevel++;
			if (freebuild_WeaponsLevel >= 6)
			{
				freebuild_WeaponsLevel = 0;
			}
			UpdateFreebuildValues();
			break;
		case "Freebuild_RandomEvents":
			freebuild_RandomEvents++;
			if (freebuild_RandomEvents >= 9)
			{
				freebuild_RandomEvents = 0;
			}
			UpdateFreebuildValues();
			break;
		case "Freebuild_Invasions":
			freebuild_Invasions++;
			if (freebuild_Invasions >= 9)
			{
				freebuild_Invasions = 0;
			}
			UpdateFreebuildValues();
			break;
		case "Freebuild_InvasionsDifficulty":
			freebuild_InvasionDifficulty++;
			if (freebuild_InvasionDifficulty >= 9)
			{
				freebuild_InvasionDifficulty = 0;
			}
			UpdateFreebuildValues();
			break;
		case "Freebuild_Peacetime":
			freebuild_Peacetime++;
			if (freebuild_Peacetime >= 7)
			{
				freebuild_Peacetime = 0;
			}
			UpdateFreebuildValues();
			break;
		case "ClearFilter":
			RefSA_SearchFilter.Text = "";
			MainViewModel.Instance.StandaloneFilterLabelVis = Visibility.Visible;
			MainViewModel.Instance.StandaloneFilterButtonVis = Visibility.Hidden;
			break;
		}
	}

	private void UpdateButtons()
	{
		switch (difficulty)
		{
		case Enums.GameDifficulty.DIFFICULTY_EASY:
			MainViewModel.Instance.StandaloneDifficultyText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAINOPTIONS, 19);
			break;
		case Enums.GameDifficulty.DIFFICULTY_NORMAL:
			MainViewModel.Instance.StandaloneDifficultyText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAINOPTIONS, 20);
			break;
		case Enums.GameDifficulty.DIFFICULTY_HARD:
			MainViewModel.Instance.StandaloneDifficultyText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAINOPTIONS, 21);
			break;
		case Enums.GameDifficulty.DIFFICULTY_VERYHARD:
			MainViewModel.Instance.StandaloneDifficultyText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAINOPTIONS, 22);
			break;
		}
		if (selectedHeader == null)
		{
			RefTroopSetup.Visibility = Visibility.Hidden;
			RefStandaloneAdvanced.Visibility = Visibility.Hidden;
			RefStandaloneFreebuildAdvanced.Visibility = Visibility.Hidden;
			if (siegeAttackMode)
			{
				MainViewModel.Instance.StandaloneAttackDefendText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_ENGINEER);
			}
			else
			{
				MainViewModel.Instance.StandaloneAttackDefendText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_LADDERMAN);
			}
		}
		else if (missionType == Enums.StartUpUIPanels.SiegeThat)
		{
			if (MainViewModel.Instance.SiegeThatHelpVis != Visibility.Visible)
			{
				RefTroopSetup.Visibility = Visibility.Visible;
			}
			else
			{
				RefTroopSetup.Visibility = Visibility.Hidden;
			}
			RefStandaloneAdvanced.Visibility = Visibility.Hidden;
			RefStandaloneFreebuildAdvanced.Visibility = Visibility.Hidden;
		}
		else if (missionType == Enums.StartUpUIPanels.FreeBuild)
		{
			MainViewModel.Instance.StandaloneAttackDefendText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_ENGINEER);
			RefStandaloneAdvanced.Visibility = Visibility.Hidden;
			RefTroopSetup.Visibility = Visibility.Hidden;
			RefStandaloneFreebuildAdvanced.Visibility = Visibility.Visible;
		}
		else if (!siegeAttackMode || missionType != Enums.StartUpUIPanels.Siege)
		{
			MainViewModel.Instance.StandaloneAttackDefendText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_ENGINEER);
			RefStandaloneAdvanced.Visibility = Visibility.Hidden;
			RefStandaloneFreebuildAdvanced.Visibility = Visibility.Hidden;
			RefTroopSetup.Visibility = Visibility.Hidden;
		}
		else
		{
			MainViewModel.Instance.StandaloneAttackDefendText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_LADDERMAN);
			RefStandaloneAdvanced.Visibility = Visibility.Visible;
			RefStandaloneFreebuildAdvanced.Visibility = Visibility.Hidden;
			if (siegeAdvanced)
			{
				RefTroopSetup.Visibility = Visibility.Visible;
			}
			else
			{
				RefTroopSetup.Visibility = Visibility.Hidden;
			}
		}
	}

	private void InitSiegeTroopsPanel()
	{
		currentTroopID = 0;
		RefStartingStandaloneArcher.IsChecked = true;
		UpdateSiegeTroopValues();
		UpdateCurrentSiegeTroop();
		MainViewModel.Instance.StandaloneSiegePoints = troop_points.ToString();
	}

	private void UpdateSiegeTroopValues()
	{
		for (int i = 0; i < 11; i++)
		{
			int num = adjusted_start_troops[i];
			MainViewModel.Instance.SiegeAttackForces[i] = num.ToString();
		}
	}

	private void UpdateCurrentSiegeTroop()
	{
		int num = adjusted_start_troops[currentTroopID];
		int num2 = troop_points / troop_points_data[currentTroopID] + adjusted_start_troops[currentTroopID];
		MainViewModel.Instance.StandaloneSiegeMax = num2;
		if (num2 < 20)
		{
			MainViewModel.Instance.StandaloneSiegeFreq = 1;
		}
		else
		{
			MainViewModel.Instance.StandaloneSiegeFreq = num2 / 10;
		}
		SetCurrentSiegeTroopValue(num);
		RefSiegeTroopsSlider.Value = num;
	}

	private void initTroopPoints()
	{
		if (adjusted_start_troops_levels == null)
		{
			adjusted_start_troops_levels = new int[4][];
			for (int i = 0; i < 4; i++)
			{
				adjusted_start_troops_levels[i] = new int[11];
			}
		}
	}

	private void reset_troop_points()
	{
		for (int i = 0; i < 4; i++)
		{
			troop_points_levels[i] = 0;
			for (int j = 0; j < 11; j++)
			{
				adjusted_start_troops_levels[i][j] = 0;
			}
		}
	}

	private void adjust_troops_difficulty_levels(int current_level)
	{
		int num = troop_points_levels[1];
		int[] array = new int[4];
		for (int i = 0; i < 11; i++)
		{
			num += troop_points_data[i] * adjusted_start_troops_levels[1][i];
		}
		for (int j = 0; j < 4; j++)
		{
			array[j] = num * troop_points_percents[j] / 100;
		}
		for (int j = 0; j < 4; j++)
		{
			troop_points_levels[j] = troop_points;
			for (int i = 0; i < 11; i++)
			{
				adjusted_start_troops_levels[j][i] = adjusted_start_troops[i];
			}
		}
		if (current_level != 1)
		{
			int num2 = 0;
			for (int i = 0; i < 11; i++)
			{
				adjusted_start_troops_levels[1][i] = adjusted_start_troops_levels[current_level][i] * 100 / troop_points_percents[current_level];
				num2 += troop_points_data[i] * adjusted_start_troops_levels[1][i];
			}
			troop_points_levels[1] = num - num2;
			if (troop_points_levels[1] < 0)
			{
				troop_points_levels[1] = 0;
			}
		}
		for (int j = 0; j < 4; j++)
		{
			if (j != current_level && j != 1)
			{
				int num2 = 0;
				for (int i = 0; i < 11; i++)
				{
					adjusted_start_troops_levels[j][i] = adjusted_start_troops_levels[1][i] * troop_points_percents[j] / 100;
					num2 += troop_points_data[i] * adjusted_start_troops_levels[j][i];
				}
				troop_points_levels[j] = array[j] - num2;
				if (troop_points_levels[j] < 0)
				{
					troop_points_levels[j] = 0;
				}
			}
		}
	}

	private void set_adjust_troop_difficulty_level(int level)
	{
		for (int i = 0; i < 11; i++)
		{
			adjusted_start_troops[i] = adjusted_start_troops_levels[level][i];
		}
		troop_points = troop_points_levels[level];
	}

	private void SiegeTroopsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		int num = currentTroopID;
		int num2 = adjusted_start_troops[currentTroopID];
		int num3 = troop_points;
		int num4 = (int)RefSiegeTroopsSlider.Value;
		adjusted_start_troops[currentTroopID] = num4;
		MainViewModel.Instance.SiegeAttackForces[currentTroopID] = num4.ToString();
		SetCurrentSiegeTroopValue(num4);
		troop_points = num3 - (adjusted_start_troops[num] - num2) * troop_points_data[num];
		MainViewModel.Instance.StandaloneSiegePoints = troop_points.ToString();
	}

	private void SetCurrentSiegeTroopValue(int value)
	{
		string text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_CHIMP_NAMES, (int)siegeTroopsOrder[currentTroopID]);
		MainViewModel.Instance.StandaloneSiegeText = text + " " + value + " (" + MainViewModel.Instance.StandaloneSiegeMax + ")";
	}

	public void Update()
	{
		if (RefFileLists.SelectedItem == null && RefFileLists.Items.Count > 0)
		{
			RefFileLists.SelectedItem = RefFileLists.Items[0];
		}
		if (!((DateTime.UtcNow - lastScrollTest).TotalMilliseconds > 150.0))
		{
			return;
		}
		if (KeyManager.instance.CursorUpHeld)
		{
			lastScrollTest = DateTime.UtcNow;
			ScrollViewer scrollViewer = MainViewModel.GetScrollViewer(RefFileLists) as ScrollViewer;
			if (!(scrollViewer != null))
			{
				return;
			}
			if (RefFileLists.SelectedItem == null)
			{
				scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - 30f);
				return;
			}
			if (RefFileLists.SelectedIndex > 0)
			{
				RefFileLists.SelectedIndex--;
			}
			RefFileLists.ScrollIntoView(RefFileLists.SelectedItem);
		}
		else
		{
			if (!KeyManager.instance.CursorDownHeld)
			{
				return;
			}
			lastScrollTest = DateTime.UtcNow;
			ScrollViewer scrollViewer2 = MainViewModel.GetScrollViewer(RefFileLists) as ScrollViewer;
			if (!(scrollViewer2 != null))
			{
				return;
			}
			if (RefFileLists.SelectedItem == null)
			{
				scrollViewer2.ScrollToVerticalOffset(scrollViewer2.VerticalOffset + 30f);
				return;
			}
			if (RefFileLists.SelectedIndex < RefFileLists.Items.Count - 1)
			{
				RefFileLists.SelectedIndex++;
			}
			RefFileLists.ScrollIntoView(RefFileLists.SelectedItem);
		}
	}

	private void UpdateFreebuildValues()
	{
		MainViewModel.Instance.StandaloneFreebuild_Gold_Text = getGoodsLevelText(freebuild_GoldLevel);
		MainViewModel.Instance.StandaloneFreebuild_Food_Text = getGoodsLevelText(freebuild_FoodLevel);
		MainViewModel.Instance.StandaloneFreebuild_Resources_Text = getGoodsLevelText(freebuild_ResourcesLevel);
		MainViewModel.Instance.StandaloneFreebuild_Weapons_Text = getGoodsLevelText(freebuild_WeaponsLevel);
		if (freebuild_RandomEvents == 0)
		{
			MainViewModel.Instance.StandaloneFreebuild_RandomEvents_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 101);
		}
		else if (freebuild_RandomEvents == 1)
		{
			MainViewModel.Instance.StandaloneFreebuild_RandomEvents_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 100);
		}
		else if (freebuild_RandomEvents == 2)
		{
			MainViewModel.Instance.StandaloneFreebuild_RandomEvents_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 119);
		}
		else if (freebuild_RandomEvents == 3)
		{
			MainViewModel.Instance.StandaloneFreebuild_RandomEvents_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 120);
		}
		else if (freebuild_RandomEvents == 8)
		{
			MainViewModel.Instance.StandaloneFreebuild_RandomEvents_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 121);
		}
		else
		{
			MainViewModel.Instance.StandaloneFreebuild_RandomEvents_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 102 + (freebuild_RandomEvents - 4));
		}
		if (freebuild_Invasions == 0)
		{
			MainViewModel.Instance.StandaloneFreebuild_Invasions_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 101);
		}
		else if (freebuild_Invasions == 1)
		{
			MainViewModel.Instance.StandaloneFreebuild_Invasions_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 100);
		}
		else if (freebuild_Invasions == 2)
		{
			MainViewModel.Instance.StandaloneFreebuild_Invasions_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 119);
		}
		else if (freebuild_Invasions == 3)
		{
			MainViewModel.Instance.StandaloneFreebuild_Invasions_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 120);
		}
		else if (freebuild_Invasions == 8)
		{
			MainViewModel.Instance.StandaloneFreebuild_Invasions_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 121);
		}
		else
		{
			MainViewModel.Instance.StandaloneFreebuild_Invasions_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 102 + (freebuild_Invasions - 4));
		}
		if (freebuild_InvasionDifficulty < 6)
		{
			MainViewModel.Instance.StandaloneFreebuild_InvasionsDifficulty_Label = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 98);
			MainViewModel.Instance.StandaloneFreebuild_InvasionsDifficulty_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 112 + freebuild_InvasionDifficulty);
		}
		else
		{
			MainViewModel.Instance.StandaloneFreebuild_InvasionsDifficulty_Label = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 185);
			MainViewModel.Instance.StandaloneFreebuild_InvasionsDifficulty_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 186 + freebuild_InvasionDifficulty - 6);
		}
		if (freebuild_Peacetime == 0)
		{
			MainViewModel.Instance.StandaloneFreebuild_Peacetime_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 101);
		}
		else if (freebuild_Peacetime == 5)
		{
			MainViewModel.Instance.StandaloneFreebuild_Peacetime_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 121);
		}
		else if (freebuild_Peacetime == 6)
		{
			MainViewModel.Instance.StandaloneFreebuild_Peacetime_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 128);
		}
		else
		{
			MainViewModel.Instance.StandaloneFreebuild_Peacetime_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 102 + (freebuild_Peacetime - 1));
		}
	}

	private string getGoodsLevelText(int level)
	{
		return level switch
		{
			0 => Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 107 + level), 
			1 => Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 118), 
			_ => Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 107 + level - 1), 
		};
	}

	public static void StartMap(HUD_IngameMenu.RestartMapInfo restartInfo)
	{
		EngineInterface.sendPath(Application.streamingAssetsPath, ConfigSettings.GetMpAutoSavePath(), ConfigSettings.GetSavesPath());
		EngineInterface.LoadMapReturnData retData = default(EngineInterface.LoadMapReturnData);
		retData.errorCode = -1;
		bool flag = false;
		switch (restartInfo.missionType)
		{
		case Enums.StartUpUIPanels.Invasion:
			retData = EngineInterface.loadInvasionMap(restartInfo.selectedHeader.filePath, restartInfo.difficulty);
			break;
		case Enums.StartUpUIPanels.Siege:
		{
			int playerID = 1;
			if (restartInfo.siegeAttackMode)
			{
				playerID = 2;
			}
			else
			{
				flag = true;
			}
			retData = ((!restartInfo.siegeAdvanced || !restartInfo.siegeAttackMode) ? EngineInterface.loadSiegeMap(restartInfo.selectedHeader.filePath, restartInfo.difficulty, playerID, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, advancedMode: false, restartInfo.trailType, restartInfo.trailID) : EngineInterface.loadSiegeMap(restartInfo.selectedHeader.filePath, restartInfo.difficulty, playerID, restartInfo.adjusted_start_troops[0], restartInfo.adjusted_start_troops[1], restartInfo.adjusted_start_troops[2], restartInfo.adjusted_start_troops[3], restartInfo.adjusted_start_troops[4], restartInfo.adjusted_start_troops[5], restartInfo.adjusted_start_troops[6], restartInfo.adjusted_start_troops[7], restartInfo.adjusted_start_troops[8], restartInfo.adjusted_start_troops[9], restartInfo.adjusted_start_troops[10], advancedMode: true));
			break;
		}
		case Enums.StartUpUIPanels.SiegeThat:
			retData = EngineInterface.loadSiegeMap(restartInfo.selectedHeader.filePath, restartInfo.difficulty, 2, restartInfo.adjusted_start_troops[0], restartInfo.adjusted_start_troops[1], restartInfo.adjusted_start_troops[2], restartInfo.adjusted_start_troops[3], restartInfo.adjusted_start_troops[4], restartInfo.adjusted_start_troops[5], restartInfo.adjusted_start_troops[6], restartInfo.adjusted_start_troops[7], restartInfo.adjusted_start_troops[8], restartInfo.adjusted_start_troops[9], restartInfo.adjusted_start_troops[10], advancedMode: true);
			break;
		case Enums.StartUpUIPanels.EcoMission:
			retData = EngineInterface.loadCustomEcoMap(restartInfo.selectedHeader.filePath, restartInfo.difficulty);
			break;
		case Enums.StartUpUIPanels.FreeBuild:
			retData = EngineInterface.loadJustBuildMap(restartInfo.selectedHeader.filePath, restartInfo.advancedFreebuild, restartInfo.freebuild_GoldLevel, restartInfo.freebuild_FoodLevel, restartInfo.freebuild_ResourcesLevel, restartInfo.freebuild_WeaponsLevel, restartInfo.freebuild_RandomEvents, restartInfo.freebuild_Invasions, restartInfo.freebuild_InvasionDifficulty, restartInfo.freebuild_Peacetime);
			break;
		}
		if (retData.errorCode == 1)
		{
			EngineInterface.GameAction(Enums.GameActionCommand.HideObjectiveProgress, 1, 1);
			GameData.Instance.SetMissionTextFromHeader(restartInfo.selectedHeader);
			EngineInterface.SetUTF8MapName(restartInfo.selectedHeader.display_filename);
			EditorDirector.instance.postLoading(retData);
			AchievementsCommon.Instance.ResetOnMissionStart();
			MainViewModel.Instance.PostStartMapMission();
			if (flag && GameData.Instance.playerID == 1)
			{
				Director.instance.DelayCentreKeep();
			}
		}
	}

	private void InitializeComponent()
	{
		NoesisUnity.LoadComponent(this, "Assets/GUI/XAMLResources/FRONT_StandaloneMission.xaml");
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

	private void TextInputFocus(object sender, DependencyPropertyChangedEventArgs e)
	{
		MainViewModel.Instance.SetNoesisKeyboardState((bool)e.NewValue);
		if ((bool)e.NewValue)
		{
			MainViewModel.Instance.StandaloneFilterLabelVis = Visibility.Hidden;
		}
		else if (RefSA_SearchFilter.Text.Length == 0)
		{
			MainViewModel.Instance.StandaloneFilterLabelVis = Visibility.Visible;
		}
	}

	private void FilterTextChangedHandler(object sender, RoutedEventArgs e)
	{
		if (panelActive)
		{
			populateList();
			if (RefSA_SearchFilter.Text.Length == 0)
			{
				MainViewModel.Instance.StandaloneFilterButtonVis = Visibility.Hidden;
			}
			else
			{
				MainViewModel.Instance.StandaloneFilterButtonVis = Visibility.Visible;
			}
		}
	}

	private void TextBoxCheckForEscape(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Escape)
		{
			base.Keyboard.ClearFocus();
			KeyManager.instance.ignoreEscape();
		}
	}

	private void TextBoxEnterCheck(object sender, TextCompositionEventArgs e)
	{
		if (e.Text == "\n")
		{
			e.Handled = true;
			base.Keyboard.ClearFocus();
		}
	}
}
