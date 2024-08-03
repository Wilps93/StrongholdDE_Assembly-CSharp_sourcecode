using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Noesis;
using Steamworks;
using UnityEngine;

namespace Stronghold1DE;

public class FRONT_Multiplayer : UserControl
{
	private class LobbyChatEntry
	{
		public string name;

		public string message;

		public int colourID;

		public DateTime received;
	}

	private class PlayerRow
	{
		public Noesis.Grid RefRow;

		public Image RefReadyState;

		public Image RefColour;

		public TextBlock RefName;

		public Image RefHost;

		public TextBlock RefTeamNumber;

		public TextBlock RefPing;

		public Button RefTeam;

		public Button RefKick;

		public void Clear()
		{
			RefRow.Visibility = Visibility.Hidden;
		}

		public void Update(FRONT_Multiplayer parent, Platform_Multiplayer.MPLobbyMember member, int row)
		{
			if (member == null)
			{
				Clear();
				return;
			}
			SetVisibility(RefRow, Visibility.Visible);
			if (row == 0)
			{
				SetVisibility(RefHost, Visibility.Visible);
			}
			if (parent.currentLobby.isHost && member.IsSelf())
			{
				SetButtonVisibility(RefKick, Visibility.Hidden);
			}
			else if (parent.currentLobby.isHost)
			{
				SetButtonVisibility(RefKick, Visibility.Visible);
			}
			else
			{
				SetButtonVisibility(RefKick, Visibility.Hidden);
			}
			if (parent.currentLobby.isHost)
			{
				SetButtonVisibility(RefTeam, Visibility.Visible);
				if (!member.IsSelf())
				{
					if (member.lastPingDuration > 0)
					{
						RefPing.Text = member.lastPingDuration / 2 + "ms";
					}
					else
					{
						RefPing.Text = "";
					}
				}
			}
			else
			{
				SetButtonVisibility(RefTeam, Visibility.Hidden);
				if (RefPing != null)
				{
					RefPing.Text = "";
				}
			}
			if (RefName.Text != member.name)
			{
				RefName.Text = member.name;
			}
			if (member.ready)
			{
				ImageSource imageSource = MainViewModel.Instance.GameSprites[105];
				if (RefReadyState.Source != imageSource)
				{
					RefReadyState.Source = imageSource;
				}
			}
			else
			{
				ImageSource imageSource2 = MainViewModel.Instance.GameSprites[103];
				if (RefReadyState.Source != imageSource2)
				{
					RefReadyState.Source = imageSource2;
				}
			}
			ImageSource colourShield = GetColourShield(member.colourID);
			if (RefColour.Source != colourShield)
			{
				RefColour.Source = colourShield;
			}
			string text = parent.currentLobby.getTeam(member).ToString();
			if (RefTeamNumber.Text != text)
			{
				RefTeamNumber.Text = text;
			}
		}
	}

	private bool panelLoaded;

	private ListView RefLobbyLists;

	private Slider RefLobbyMaxPlayersSlider;

	private Slider RefSetupMaxPlayersSlider;

	private Button RefJoinButton;

	private TextBox RefTextBoxGameName;

	private Button RefMultiplayerPlayButton;

	private Button RefReadyButton;

	private Button RefLoadButton;

	private TextBox RefMP_ChatInput;

	private TextBlock RefMP_ChatDisplay;

	private ScrollViewer RefMP_ChatScrollView;

	private Slider RefMP_Settings_Gold_Slider;

	private Slider RefMP_Settings_Koth_Slider;

	private Slider RefMP_Settings_Peacetime_Slider;

	private Slider RefMP_Settings_GameSpeed_Slider;

	private Button RefColourSelectButton;

	private Button RefColShield1;

	private Button RefColShield2;

	private Button RefColShield3;

	private Button RefColShield4;

	private Button RefColShield5;

	private Button RefColShield6;

	private Button RefColShield7;

	private Button RefColShield8;

	private TextBox RefMP_SearchFilter;

	private TextBox RefMP_EnterShareCodeText;

	private Button RefShareJoinButton;

	private Storyboard pulseAnimation;

	private List<Platform_Multiplayer.MPLobby> lobbies = new List<Platform_Multiplayer.MPLobby>();

	private EngineInterface.MultiplayerSetupData MPsetupData;

	private EngineInterface.MultiplayerSetupData MPTEMPsetupData;

	private Platform_Multiplayer.MPLobby selectedLobby;

	private Platform_Multiplayer.MPLobby currentLobby;

	private FileHeader selectedMPHeader;

	private int numConnectedPlayers = 1;

	private int sortByColumn;

	private bool sortByAscending = true;

	private bool includeUser = true;

	private bool includeBuiltIn = true;

	private bool includeWorkshop = true;

	private bool MPLocalReady;

	private bool readyAnimPlaying;

	private int MPTotalPlayers;

	private string MPLastMapName = "";

	private bool MPMapChecked;

	private bool MPMapValid;

	private bool MPGameLoading;

	private bool regetMapListNextTime;

	private bool pendingMPHost;

	private DateTime delayedSendDataToLobby = DateTime.MinValue;

	private DateTime nextHostSendPings = DateTime.MinValue;

	private string MPHostLobbyname = "";

	private DateTime multiplayerMapRequestTime = DateTime.MinValue;

	private DateTime lastAutoRefreshTime = DateTime.MinValue;

	private int MPLobbyMode;

	private ulong LatestSharedCode;

	private bool ShowSharingCode;

	private int PlayerCap = 8;

	private PlayerRow[] playerRows = new PlayerRow[8];

	public static readonly int[] MP_orig_remap_colour_order = new int[9] { 0, 1, 3, 4, 2, 6, 5, 7, 8 };

	private List<LobbyChatEntry> lobbyChat = new List<LobbyChatEntry>();

	private string chatMessage = "";

	private ListView RefFileLists;

	private CheckBox RefIncludeUser;

	private CheckBox RefIncludeBuiltin;

	private CheckBox RefIncludeWorkshop;

	private FileHeader selectedHeader;

	private bool panelActive;

	private ObservableCollection<FileRow> fileRows = new ObservableCollection<FileRow>();

	private ObservableCollection<FileRow> lobbyRows = new ObservableCollection<FileRow>();

	private List<FileHeader> headerlist;

	private DateTime lastScrollTest = DateTime.MinValue;

	private DateTime startGameTime = DateTime.MinValue;

	private int[,] start_few_troop_level = new int[5, 10]
	{
		{ 2, 0, 2, 0, 0, 0, 0, 0, 0, 0 },
		{ 2, 0, 2, 0, 2, 0, 0, 0, 0, 0 },
		{ 2, 0, 2, 2, 2, 0, 0, 0, 1, 0 },
		{ 2, 2, 0, 2, 0, 2, 0, 0, 2, 2 },
		{ 2, 2, 0, 2, 0, 2, 1, 0, 2, 2 }
	};

	private int[,] start_some_troop_level = new int[5, 10]
	{
		{ 3, 0, 3, 0, 0, 0, 0, 0, 0, 0 },
		{ 4, 0, 4, 0, 4, 0, 0, 0, 1, 0 },
		{ 4, 0, 4, 0, 2, 0, 0, 0, 2, 0 },
		{ 8, 4, 0, 6, 0, 4, 0, 0, 3, 4 },
		{ 8, 4, 0, 6, 0, 4, 4, 0, 3, 4 }
	};

	private int[,] start_many_troop_level = new int[5, 10]
	{
		{ 6, 0, 6, 0, 0, 0, 0, 0, 1, 0 },
		{ 8, 0, 8, 0, 8, 0, 0, 0, 2, 0 },
		{ 8, 0, 8, 0, 4, 0, 0, 0, 3, 0 },
		{ 10, 6, 0, 8, 0, 6, 0, 0, 5, 6 },
		{ 10, 6, 0, 8, 0, 6, 6, 0, 5, 6 }
	};

	private int[,] start_low_goods_level = new int[5, 20]
	{
		{
			48, 0, 0, 0, 0, 0, 0, 0, 25, 25,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0
		},
		{
			48, 0, 0, 5, 0, 0, 0, 5, 20, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0
		},
		{
			48, 0, 24, 12, 0, 0, 0, 20, 20, 10,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0
		},
		{
			48, 0, 48, 12, 0, 0, 20, 10, 10, 10,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0
		},
		{
			48, 0, 48, 12, 0, 0, 20, 10, 10, 10,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0
		}
	};

	private int[,] start_med_goods_level = new int[5, 20]
	{
		{
			96, 0, 0, 0, 0, 0, 0, 0, 80, 0,
			0, 2, 0, 2, 0, 0, 0, 0, 0, 0
		},
		{
			96, 0, 0, 12, 0, 0, 0, 15, 35, 0,
			0, 4, 0, 4, 0, 0, 0, 4, 0, 0
		},
		{
			96, 0, 48, 24, 8, 0, 0, 15, 15, 50,
			0, 6, 2, 6, 0, 2, 0, 4, 0, 0
		},
		{
			96, 0, 96, 24, 8, 16, 35, 15, 15, 15,
			8, 6, 4, 6, 4, 4, 4, 8, 8, 0
		},
		{
			96, 0, 96, 24, 8, 16, 35, 15, 15, 15,
			8, 6, 4, 6, 4, 4, 4, 8, 8, 0
		}
	};

	private int[,] start_high_goods_level = new int[5, 20]
	{
		{
			120, 0, 0, 0, 0, 0, 0, 0, 50, 50,
			0, 4, 0, 4, 0, 0, 0, 0, 0, 0
		},
		{
			120, 0, 0, 24, 0, 0, 0, 30, 50, 0,
			0, 8, 0, 8, 0, 0, 0, 8, 0, 0
		},
		{
			120, 0, 96, 48, 16, 0, 0, 25, 25, 50,
			0, 8, 4, 8, 0, 4, 0, 8, 0, 0
		},
		{
			120, 0, 192, 48, 16, 32, 50, 30, 30, 30,
			16, 8, 4, 8, 6, 6, 4, 10, 10, 0
		},
		{
			120, 0, 192, 48, 16, 32, 40, 20, 20, 20,
			16, 8, 4, 4, 6, 6, 3, 10, 9, 0
		}
	};

	private static void SetVisibility(UIElement element, Visibility state)
	{
		if (element.Visibility != state)
		{
			element.Visibility = state;
		}
	}

	private static void SetButtonVisibility(UIElement element, Visibility state)
	{
		if (PropEx.GetButtonVisibility(element) != state)
		{
			PropEx.SetButtonVisibility(element, state);
		}
	}

	public static ImageSource GetColourShield(int colourID, int state = 0)
	{
		if (colourID < 0 || colourID >= MP_orig_remap_colour_order.Length)
		{
			return null;
		}
		colourID = MP_orig_remap_colour_order[colourID];
		switch (state)
		{
		case 0:
			switch (colourID)
			{
			case 1:
				return MainViewModel.Instance.GameSprites[107];
			case 2:
				return MainViewModel.Instance.GameSprites[110];
			case 3:
				return MainViewModel.Instance.GameSprites[108];
			case 4:
				return MainViewModel.Instance.GameSprites[109];
			case 5:
				return MainViewModel.Instance.GameSprites[112];
			case 6:
				return MainViewModel.Instance.GameSprites[111];
			case 7:
				return MainViewModel.Instance.GameSprites[113];
			case 8:
				return MainViewModel.Instance.GameSprites[114];
			}
			break;
		case 1:
			switch (colourID)
			{
			case 1:
				return MainViewModel.Instance.GameSprites[352];
			case 2:
				return MainViewModel.Instance.GameSprites[355];
			case 3:
				return MainViewModel.Instance.GameSprites[353];
			case 4:
				return MainViewModel.Instance.GameSprites[354];
			case 5:
				return MainViewModel.Instance.GameSprites[357];
			case 6:
				return MainViewModel.Instance.GameSprites[356];
			case 7:
				return MainViewModel.Instance.GameSprites[358];
			case 8:
				return MainViewModel.Instance.GameSprites[359];
			}
			break;
		case 2:
			switch (colourID)
			{
			case 1:
				return MainViewModel.Instance.GameSprites[360];
			case 2:
				return MainViewModel.Instance.GameSprites[363];
			case 3:
				return MainViewModel.Instance.GameSprites[361];
			case 4:
				return MainViewModel.Instance.GameSprites[362];
			case 5:
				return MainViewModel.Instance.GameSprites[365];
			case 6:
				return MainViewModel.Instance.GameSprites[364];
			case 7:
				return MainViewModel.Instance.GameSprites[366];
			case 8:
				return MainViewModel.Instance.GameSprites[367];
			}
			break;
		}
		return null;
	}

	public FRONT_Multiplayer()
	{
		InitializeComponent();
		MainViewModel.Instance.FRONTMultiplayer = this;
		pulseAnimation = (Storyboard)TryFindResource("ReadyButtonAnim");
		RefFileLists = (ListView)FindName("MapList");
		RefLobbyLists = (ListView)FindName("LobbyList");
		RefLobbyMaxPlayersSlider = (Slider)FindName("LobbyMaxPlayersSlider");
		RefLobbyMaxPlayersSlider.ValueChanged += LobbyMaxPlayersSlider_ValueChanged;
		RefSetupMaxPlayersSlider = (Slider)FindName("SetupMaxPlayersSlider");
		RefSetupMaxPlayersSlider.ValueChanged += SetupMaxPlayersSlider_ValueChanged;
		RefJoinButton = (Button)FindName("JoinButton");
		RefMultiplayerPlayButton = (Button)FindName("MultiplayerPlayButton");
		RefReadyButton = (Button)FindName("ReadyButton");
		RefLoadButton = (Button)FindName("LoadButton");
		RefColourSelectButton = (Button)FindName("ColourSelectButton");
		RefColShield1 = (Button)FindName("ColShield1");
		RefColShield2 = (Button)FindName("ColShield2");
		RefColShield3 = (Button)FindName("ColShield3");
		RefColShield4 = (Button)FindName("ColShield4");
		RefColShield5 = (Button)FindName("ColShield5");
		RefColShield6 = (Button)FindName("ColShield6");
		RefColShield7 = (Button)FindName("ColShield7");
		RefColShield8 = (Button)FindName("ColShield8");
		RefTextBoxGameName = (TextBox)FindName("TextBoxGameName");
		RefTextBoxGameName.IsKeyboardFocusedChanged += TextInputFocus;
		RefMP_ChatInput = (TextBox)FindName("MP_ChatInput");
		RefMP_ChatInput.IsKeyboardFocusedChanged += TextInputFocus;
		RefMP_ChatInput.PreviewKeyUp += DetectChatEnter;
		RefMP_ChatDisplay = (TextBlock)FindName("MP_ChatDisplay");
		RefMP_ChatScrollView = (ScrollViewer)FindName("MP_ChatScrollView");
		RefMP_SearchFilter = (TextBox)FindName("MP_SearchFilter");
		RefMP_SearchFilter.IsKeyboardFocusedChanged += FilterTextInputFocus;
		RefMP_SearchFilter.TextChanged += FilterTextChangedHandler;
		RefMP_SearchFilter.PreviewKeyDown += TextBoxCheckForEscape;
		RefMP_SearchFilter.PreviewTextInput += TextBoxEnterCheck;
		RefMP_EnterShareCodeText = (TextBox)FindName("MP_EnterShareCodeText");
		RefMP_EnterShareCodeText.IsKeyboardFocusedChanged += TextInputFocus;
		RefMP_EnterShareCodeText.TextChanged += EnterShareTextChangedHandler;
		RefShareJoinButton = (Button)FindName("ShareJoinButton");
		RefMP_Settings_Gold_Slider = (Slider)FindName("MP_Settings_Gold_Slider");
		RefMP_Settings_Gold_Slider.ValueChanged += MP_Settings_Gold_Slider_ValueChanged;
		RefMP_Settings_Koth_Slider = (Slider)FindName("MP_Settings_Koth_Slider");
		RefMP_Settings_Koth_Slider.ValueChanged += MP_Settings_Koth_Slider_ValueChanged;
		RefMP_Settings_Peacetime_Slider = (Slider)FindName("MP_Settings_Peacetime_Slider");
		RefMP_Settings_Peacetime_Slider.ValueChanged += MP_Settings_Peacetime_Slider_ValueChanged;
		RefMP_Settings_GameSpeed_Slider = (Slider)FindName("MP_Settings_GameSpeed_Slider");
		RefMP_Settings_GameSpeed_Slider.ValueChanged += MP_Settings_GameSpeed_Slider_ValueChanged;
		for (int i = 0; i < 8; i++)
		{
			playerRows[i] = new PlayerRow();
		}
		playerRows[0].RefRow = (Noesis.Grid)FindName("Player1_Row");
		playerRows[0].RefReadyState = (Image)FindName("Player1_ReadyState");
		playerRows[0].RefColour = (Image)FindName("Player1_Colour");
		playerRows[0].RefName = (TextBlock)FindName("Player1_Name");
		playerRows[0].RefHost = (Image)FindName("Player1_Host");
		playerRows[0].RefTeamNumber = (TextBlock)FindName("Player1_TeamNumber");
		playerRows[0].RefTeam = (Button)FindName("Player1_Team");
		playerRows[0].RefKick = (Button)FindName("Player1_Kick");
		playerRows[1].RefRow = (Noesis.Grid)FindName("Player2_Row");
		playerRows[1].RefReadyState = (Image)FindName("Player2_ReadyState");
		playerRows[1].RefColour = (Image)FindName("Player2_Colour");
		playerRows[1].RefName = (TextBlock)FindName("Player2_Name");
		playerRows[1].RefTeamNumber = (TextBlock)FindName("Player2_TeamNumber");
		playerRows[1].RefPing = (TextBlock)FindName("Player2_Ping");
		playerRows[1].RefTeam = (Button)FindName("Player2_Team");
		playerRows[1].RefKick = (Button)FindName("Player2_Kick");
		playerRows[2].RefRow = (Noesis.Grid)FindName("Player3_Row");
		playerRows[2].RefReadyState = (Image)FindName("Player3_ReadyState");
		playerRows[2].RefColour = (Image)FindName("Player3_Colour");
		playerRows[2].RefName = (TextBlock)FindName("Player3_Name");
		playerRows[2].RefTeamNumber = (TextBlock)FindName("Player3_TeamNumber");
		playerRows[2].RefPing = (TextBlock)FindName("Player3_Ping");
		playerRows[2].RefTeam = (Button)FindName("Player3_Team");
		playerRows[2].RefKick = (Button)FindName("Player3_Kick");
		playerRows[3].RefRow = (Noesis.Grid)FindName("Player4_Row");
		playerRows[3].RefReadyState = (Image)FindName("Player4_ReadyState");
		playerRows[3].RefColour = (Image)FindName("Player4_Colour");
		playerRows[3].RefName = (TextBlock)FindName("Player4_Name");
		playerRows[3].RefTeamNumber = (TextBlock)FindName("Player4_TeamNumber");
		playerRows[3].RefPing = (TextBlock)FindName("Player4_Ping");
		playerRows[3].RefTeam = (Button)FindName("Player4_Team");
		playerRows[3].RefKick = (Button)FindName("Player4_Kick");
		playerRows[4].RefRow = (Noesis.Grid)FindName("Player5_Row");
		playerRows[4].RefReadyState = (Image)FindName("Player5_ReadyState");
		playerRows[4].RefColour = (Image)FindName("Player5_Colour");
		playerRows[4].RefName = (TextBlock)FindName("Player5_Name");
		playerRows[4].RefTeamNumber = (TextBlock)FindName("Player5_TeamNumber");
		playerRows[4].RefPing = (TextBlock)FindName("Player5_Ping");
		playerRows[4].RefTeam = (Button)FindName("Player5_Team");
		playerRows[4].RefKick = (Button)FindName("Player5_Kick");
		playerRows[5].RefRow = (Noesis.Grid)FindName("Player6_Row");
		playerRows[5].RefReadyState = (Image)FindName("Player6_ReadyState");
		playerRows[5].RefColour = (Image)FindName("Player6_Colour");
		playerRows[5].RefName = (TextBlock)FindName("Player6_Name");
		playerRows[5].RefTeamNumber = (TextBlock)FindName("Player6_TeamNumber");
		playerRows[5].RefPing = (TextBlock)FindName("Player6_Ping");
		playerRows[5].RefTeam = (Button)FindName("Player6_Team");
		playerRows[5].RefKick = (Button)FindName("Player6_Kick");
		playerRows[6].RefRow = (Noesis.Grid)FindName("Player7_Row");
		playerRows[6].RefReadyState = (Image)FindName("Player7_ReadyState");
		playerRows[6].RefColour = (Image)FindName("Player7_Colour");
		playerRows[6].RefName = (TextBlock)FindName("Player7_Name");
		playerRows[6].RefTeamNumber = (TextBlock)FindName("Player7_TeamNumber");
		playerRows[6].RefPing = (TextBlock)FindName("Player7_Ping");
		playerRows[6].RefTeam = (Button)FindName("Player7_Team");
		playerRows[6].RefKick = (Button)FindName("Player7_Kick");
		playerRows[7].RefRow = (Noesis.Grid)FindName("Player8_Row");
		playerRows[7].RefReadyState = (Image)FindName("Player8_ReadyState");
		playerRows[7].RefColour = (Image)FindName("Player8_Colour");
		playerRows[7].RefName = (TextBlock)FindName("Player8_Name");
		playerRows[7].RefTeamNumber = (TextBlock)FindName("Player8_TeamNumber");
		playerRows[7].RefPing = (TextBlock)FindName("Player8_Ping");
		playerRows[7].RefTeam = (Button)FindName("Player8_Team");
		playerRows[7].RefKick = (Button)FindName("Player8_Kick");
		RefIncludeUser = (CheckBox)FindName("IncludeUser");
		RefIncludeUser.Checked += Include_ValueChanged;
		RefIncludeUser.Unchecked += Include_ValueChanged;
		RefIncludeBuiltin = (CheckBox)FindName("IncludeBuiltin");
		RefIncludeBuiltin.Checked += Include_ValueChanged;
		RefIncludeBuiltin.Unchecked += Include_ValueChanged;
		RefIncludeWorkshop = (CheckBox)FindName("IncludeWorkshop");
		RefIncludeWorkshop.Checked += Include_ValueChanged;
		RefIncludeWorkshop.Unchecked += Include_ValueChanged;
		GridView obj = (GridView)RefFileLists.View;
		GridViewColumnHeader obj2 = (GridViewColumnHeader)obj.Columns[2].Header;
		obj2.Content = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 27);
		obj2.Click += FileListHeaderClickedHandler;
		GridViewColumnHeader obj3 = (GridViewColumnHeader)obj.Columns[3].Header;
		obj3.Content = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 28);
		obj3.Click += FileListHeaderClickedHandler;
		GridViewColumnHeader obj4 = (GridViewColumnHeader)obj.Columns[0].Header;
		obj4.Content = "";
		obj4.Click += FileListHeaderClickedHandler;
		GridViewColumnHeader obj5 = (GridViewColumnHeader)obj.Columns[1].Header;
		obj5.Content = "#";
		obj5.Click += FileListHeaderClickedHandler;
		RefFileLists.SelectionChanged += delegate
		{
			if (RefFileLists.SelectedItem != null && currentLobby != null && currentLobby.isHost)
			{
				FileHeader fileHeader = ((FileRow)RefFileLists.SelectedItem).fileHeader;
				if (fileHeader != null)
				{
					selectedMPHeader = fileHeader;
					UpdateHostInfo();
					updateRadarTexture(fileHeader);
					GameData.Instance.SetMissionTextFromHeader(fileHeader);
					MainViewModel.Instance.StandaloneMissionText = GameData.Instance.GetMissionBriefing(fileHeader);
					MPLocalReady = false;
					Platform_Multiplayer.Instance.SetMemberReadyState(state: false);
					MainViewModel.Instance.Show_MPKothMap = fileHeader.isKingOfTheHill > 0;
					MainViewModel.Instance.Show_MPPeacetime = fileHeader.isKingOfTheHill == 0;
				}
			}
		};
		GridView obj6 = (GridView)RefLobbyLists.View;
		GridViewColumnHeader obj7 = (GridViewColumnHeader)obj6.Columns[0].Header;
		obj7.Content = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 48);
		obj7.Click += LobbyListHeaderClickedHandler;
		GridViewColumnHeader obj8 = (GridViewColumnHeader)obj6.Columns[1].Header;
		obj8.Content = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 49);
		obj8.Click += LobbyListHeaderClickedHandler;
		GridViewColumnHeader obj9 = (GridViewColumnHeader)obj6.Columns[2].Header;
		obj9.Content = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 50);
		obj9.Click += LobbyListHeaderClickedHandler;
		RefLobbyLists.SelectionChanged += delegate
		{
			if (RefLobbyLists.SelectedItem != null)
			{
				selectedLobby = ((FileRow)RefLobbyLists.SelectedItem).lobby;
				RefJoinButton.IsEnabled = true;
			}
		};
		RefLobbyLists.MouseDoubleClick += delegate
		{
			if (RefLobbyLists.SelectedItem != null)
			{
				selectedLobby = ((FileRow)RefLobbyLists.SelectedItem).lobby;
				RefJoinButton.IsEnabled = true;
				ButtonClicked("Join");
			}
		};
		panelLoaded = true;
	}

	public static void Open()
	{
		MainViewModel.Instance.FRONTMultiplayer.doOpen(fromNew: true);
	}

	public void doOpen(bool fromNew = false)
	{
		try
		{
			panelActive = false;
			if (fromNew)
			{
				pulseAnimation.Stop();
				readyAnimPlaying = false;
				sortByColumn = 0;
				sortByAscending = true;
				includeUser = true;
				includeBuiltIn = true;
				includeWorkshop = true;
				RefIncludeBuiltin.IsChecked = true;
				RefIncludeUser.IsChecked = true;
				RefIncludeWorkshop.IsChecked = true;
				selectedHeader = null;
				selectedMPHeader = null;
				selectedLobby = null;
				currentLobby = null;
				lobbies.Clear();
				for (int i = 0; i < 8; i++)
				{
					playerRows[i].Clear();
				}
				populateLobbyList();
				lastAutoRefreshTime = DateTime.UtcNow;
				MPsetupData = EngineInterface.initMultiplayerGame();
				updateLevelsFromTechLevel();
				Platform_Multiplayer.Instance.Initialise();
				Platform_Multiplayer.Instance.GetLobbies(delegate
				{
					lobbies = Platform_Multiplayer.Instance.ReadLobbies();
					populateLobbyList();
				});
				numConnectedPlayers = 2;
				headerlist = MapFileManager.Instance.GetMultiplayerMaps(sortByColumn, sortByAscending, numConnectedPlayers, includeBuiltIn, includeUser, includeWorkshop);
				MainViewModel.Instance.MultiplayerFilter = "";
				MainViewModel.Instance.MultiplayerFilterLabelVis = Visibility.Visible;
				MainViewModel.Instance.MultiplayerFilterButtonVis = Visibility.Hidden;
				MainViewModel.Instance.MultiplayerEnterShareCode = "";
				RefShareJoinButton.IsEnabled = false;
				LatestSharedCode = 0uL;
				pendingMPHost = false;
				MPMapChecked = false;
				MPMapValid = false;
				MPGameLoading = false;
				regetMapListNextTime = false;
				MPLocalReady = false;
				MPLobbyMode = 0;
				MPLastMapName = "";
				multiplayerMapRequestTime = DateTime.MinValue;
				MainViewModel.Instance.MP_LobbyChatWindow = "";
				ShowSharingCode = false;
				if (Platform_Multiplayer.Instance.PendingMPLobby)
				{
					Platform_Multiplayer.MPLobby joiningLobby = null;
					Platform_Multiplayer.Instance.AutoJoinPendingLobby(ref joiningLobby, delegate
					{
						currentLobby = joiningLobby;
						ShowSetupScreen();
					}, delegate(string name, string message, int colourID)
					{
						receivedLobbyChat(name, message, colourID);
					});
					Platform_Multiplayer.Instance.PendingMPLobby = false;
				}
				ShowLobbyScreen();
				RefJoinButton.IsEnabled = false;
			}
			selectedHeader = null;
			MainViewModel.Instance.RadarStandaloneImage = null;
			MainViewModel.Instance.StandaloneMissionText = "";
			MainViewModel.Instance.Show_MultiplayerSetup = true;
			UpdateButtons();
			GameData.Instance.game_type = 3;
			populateLobbyList();
			populateMapList();
			panelActive = true;
		}
		catch (Exception)
		{
			MainViewModel.Instance.FrontEndMenu.ButtonClicked("BackMain");
		}
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
		populateMapList();
	}

	private void populateMapList(FileHeader selectedHeader = null)
	{
		includeBuiltIn = RefIncludeBuiltin.IsChecked.Value;
		includeUser = RefIncludeUser.IsChecked.Value;
		includeWorkshop = RefIncludeWorkshop.IsChecked.Value;
		_ = sortByColumn;
		headerlist = MapFileManager.Instance.GetMultiplayerMaps(sortByColumn, sortByAscending, numConnectedPlayers, includeBuiltIn, includeUser, includeWorkshop);
		FileRow fileRow = null;
		fileRows.Clear();
		if (headerlist != null)
		{
			string text = RefMP_SearchFilter.Text;
			string value = text.ToLowerInvariant();
			foreach (FileHeader item in headerlist)
			{
				if (text.Length <= 0 || item.display_filename.Contains(text) || item.display_filename.ToLowerInvariant().Contains(value))
				{
					FileRow fileRow2 = new FileRow();
					fileRow2.Text2 = item.getDateString();
					fileRow2.Text3 = item.maxPlayers.ToString();
					if (item.builtinMap)
					{
						fileRow2.TypeImage = MainViewModel.Instance.GameSprites[88];
					}
					else if (item.workshopMap)
					{
						fileRow2.TypeImage = MainViewModel.Instance.GameSprites[89];
					}
					else if (item.userMap)
					{
						fileRow2.TypeImage = MainViewModel.Instance.GameSprites[90];
					}
					string text2 = "";
					if (item.isKingOfTheHill > 0)
					{
						fileRow2.KothImage = MainViewModel.Instance.GameSprites[102];
						text2 = " ";
					}
					else
					{
						fileRow2.KothImage = null;
					}
					fileRow2.Text1 = text2 + item.display_filename;
					fileRow2.fileHeader = item;
					fileRows.Add(fileRow2);
					if (item == selectedHeader)
					{
						fileRow = fileRow2;
					}
				}
			}
		}
		RefFileLists.ItemsSource = fileRows;
		if (fileRow != null)
		{
			RefFileLists.SelectedItem = fileRow;
		}
	}

	private void populateLobbyList()
	{
		ulong num = 0uL;
		FileRow fileRow = null;
		if (selectedLobby != null)
		{
			num = selectedLobby.identifier;
		}
		lobbyRows.Clear();
		for (int i = 0; i < lobbies.Count; i++)
		{
			Platform_Multiplayer.MPLobby mPLobby = lobbies[i];
			FileRow fileRow2 = new FileRow();
			fileRow2.Text1 = mPLobby.gameName;
			fileRow2.Text3 = mPLobby.numLobbyMembers + "/" + mPLobby.maxPlayers;
			fileRow2.lobby = mPLobby;
			string text = "";
			if (EditorDirector.getIntFromString(mPLobby.gameType) > 0)
			{
				fileRow2.KothImage = MainViewModel.Instance.GameSprites[102];
				text = " ";
			}
			else
			{
				fileRow2.KothImage = null;
				text = "";
			}
			fileRow2.Text2 = text + mPLobby.mapName;
			lobbyRows.Add(fileRow2);
			if (mPLobby.identifier == num)
			{
				fileRow = fileRow2;
			}
		}
		RefLobbyLists.ItemsSource = lobbyRows;
		if (fileRow != null)
		{
			RefLobbyLists.SelectedItem = fileRow;
		}
		else
		{
			RefJoinButton.IsEnabled = false;
		}
	}

	private void LobbyListHeaderClickedHandler(object sender, RoutedEventArgs e)
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
		populateLobbyList();
	}

	private void updateRadarTexture(FileHeader header)
	{
		if (header != null)
		{
			MainViewModel.Instance.Show_MPRadar = true;
			byte[] radarFromFile = MapFileManager.Instance.GetRadarFromFile(header.filePath);
			if (radarFromFile != null)
			{
				TextureSource radarStandaloneImage = new TextureSource(MapFileManager.Instance.GetRadarPreview(radarFromFile));
				MainViewModel.Instance.RadarStandaloneImage = radarStandaloneImage;
			}
		}
		else
		{
			MainViewModel.Instance.Show_MPRadar = false;
		}
	}

	private void Include_ValueChanged(object sender, RoutedEventArgs e)
	{
		if (panelActive)
		{
			populateMapList();
		}
	}

	public void ButtonClicked(string param)
	{
		switch (param)
		{
		case "Back":
			if (MainViewModel.Instance.Show_MPSharing)
			{
				MainViewModel.Instance.Show_MPSharing = false;
			}
			else if (MainViewModel.Instance.Show_MPColours)
			{
				MainViewModel.Instance.Show_MPColours = false;
			}
			else if (MainViewModel.Instance.Show_MPSettings)
			{
				MainViewModel.Instance.Show_MPSettings = false;
			}
			else if (MainViewModel.Instance.Show_MPJoiningLobby)
			{
				LeaveLobby();
				MainViewModel.Instance.FrontEndMenu.ButtonClicked("BackMain");
			}
			else
			{
				LeaveLobby();
				ShowLobbyScreen();
			}
			break;
		case "Host":
			MainViewModel.Instance.Show_CreatingMPHost = true;
			RefLobbyMaxPlayersSlider.Value = 8f;
			RefSetupMaxPlayersSlider.Value = 8f;
			MainViewModel.Instance.MPCreateMaxPlayers = "8";
			PlayerCap = 8;
			MPHostLobbyname = "";
			updateHostLobbyButton();
			if (SteamManager.Initialized)
			{
				string personaName = SteamFriends.GetPersonaName();
				MPHostLobbyname = personaName + " Game";
				RefTextBoxGameName.Text = MPHostLobbyname;
			}
			break;
		case "Join":
			Platform_Multiplayer.Instance.JoinLobby(selectedLobby, delegate
			{
				currentLobby = selectedLobby;
				ShowSetupScreen();
				headerlist = MapFileManager.Instance.GetMultiplayerMaps(sortByColumn, sortByAscending, numConnectedPlayers, includeBuiltIn, includeUser, includeWorkshop);
			}, delegate(string name, string message, int colourID)
			{
				receivedLobbyChat(name, message, colourID);
			});
			break;
		case "ShareJoin":
			if (LatestSharedCode != 0)
			{
				Platform_Multiplayer.Instance.SetInviteLobbyID(LatestSharedCode);
				Platform_Multiplayer.MPLobby joiningLobby = null;
				Platform_Multiplayer.Instance.AutoJoinPendingLobby(ref joiningLobby, delegate
				{
					currentLobby = joiningLobby;
					ShowSetupScreen();
				}, delegate(string name, string message, int colourID)
				{
					receivedLobbyChat(name, message, colourID);
				});
				Platform_Multiplayer.Instance.PendingMPLobby = false;
				LatestSharedCode = 0uL;
				RefMP_EnterShareCodeText.Text = "";
			}
			break;
		case "Refresh":
			lastAutoRefreshTime = DateTime.MinValue;
			break;
		case "TogglePublic":
			if (MPLobbyMode == 0)
			{
				MPLobbyMode = 2;
			}
			else if (MPLobbyMode == 2)
			{
				MPLobbyMode = 4;
			}
			else
			{
				MPLobbyMode = 0;
			}
			updateHostLobbyButton();
			break;
		case "DoHost":
			if (RefTextBoxGameName.Text.Length > 0)
			{
				pendingMPHost = true;
				MainViewModel.Instance.Show_MPJoiningLobby = false;
				MainViewModel.Instance.Show_CreatingMPHost = false;
			}
			break;
		case "CancelHost":
			MainViewModel.Instance.Show_CreatingMPHost = false;
			break;
		case "Invite":
			Platform_Multiplayer.Instance.InviteOverlay();
			break;
		case "Ready":
			if (MPMapValid || currentLobby.isHost)
			{
				MPLocalReady = !MPLocalReady;
			}
			else
			{
				MPLocalReady = false;
			}
			Platform_Multiplayer.Instance.SetMemberReadyState(MPLocalReady);
			break;
		case "Play":
			Platform_Multiplayer.Instance.HostStartGame();
			startGameTime = DateTime.UtcNow.AddMilliseconds(500.0);
			break;
		case "Load":
			Platform_Multiplayer.Instance.SendSaveCRCs();
			HUD_LoadSaveRequester.OpenLoadSaveRequester(Enums.RequesterTypes.LoadMultiplayerGame, delegate(string filename, FileHeader header)
			{
				Platform_Multiplayer.Instance.HostLoadGame(header.fileName);
				startGameTime = DateTime.UtcNow.AddMilliseconds(500.0);
			}, delegate
			{
			}, MPTotalPlayers - 1);
			break;
		case "Kick_1":
		case "Kick_2":
		case "Kick_3":
		case "Kick_4":
		case "Kick_5":
		case "Kick_6":
		case "Kick_7":
		case "Kick_8":
		{
			int num = param[param.Length - 1] - 49;
			List<Platform_Multiplayer.MPLobbyMember> members = currentLobby.members;
			if (currentLobby.isHost && members.Count > num && !members[num].IsSelf())
			{
				Platform_Multiplayer.Instance.KickMemberFromLobby(members[num]);
			}
			break;
		}
		case "Team_1":
		case "Team_2":
		case "Team_3":
		case "Team_4":
		case "Team_5":
		case "Team_6":
		case "Team_7":
		case "Team_8":
		{
			int index = param[param.Length - 1] - 49;
			List<Platform_Multiplayer.MPLobbyMember> members2 = currentLobby.members;
			if (currentLobby.isHost)
			{
				int team = currentLobby.getTeam(members2[index]);
				team++;
				int count = currentLobby.members.Count;
				if (team > count)
				{
					team = 1;
				}
				currentLobby.setTeam(members2[index], team);
				UpdateHostInfo();
			}
			break;
		}
		case "SendChat":
			if (RefMP_ChatInput.Text.Length > 0)
			{
				Platform_Multiplayer.Instance.SendLobbyChatMessage(RefMP_ChatInput.Text);
				RefMP_ChatInput.Text = "";
			}
			break;
		case "Setup":
		{
			MainViewModel.Instance.MP_Settings_Button = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 58);
			string str2 = MPsetupData.ToString();
			MPTEMPsetupData = new EngineInterface.MultiplayerSetupData();
			MPTEMPsetupData.FromString(str2);
			MainViewModel.Instance.MP_Settings_TechLevel = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, 79 + MPTEMPsetupData.starting_keep_type / 2);
			MainViewModel.Instance.MP_Settings_StartingGoods = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, 17 + MPTEMPsetupData.starting_goods_level);
			MainViewModel.Instance.MP_Settings_StartingTroops = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, 23 + MPTEMPsetupData.starting_troops_level);
			MainViewModel.Instance.MP_Settings_Gold = MPTEMPsetupData.starting_gold.ToString();
			RefMP_Settings_Gold_Slider.Value = MPTEMPsetupData.starting_gold / 50;
			MainViewModel.Instance.MP_Settings_Toggle_BulkVis = MPTEMPsetupData.trading[3] == 0;
			MainViewModel.Instance.MP_Settings_Toggle_FoodVis = MPTEMPsetupData.trading[1] == 0;
			MainViewModel.Instance.MP_Settings_Toggle_WeaponsVis = MPTEMPsetupData.trading[0] == 0;
			RefMP_Settings_Koth_Slider.Value = MPTEMPsetupData.king_of_the_hill_points / 1000;
			MainViewModel.Instance.MP_Settings_Koth = MPTEMPsetupData.king_of_the_hill_points.ToString();
			RefMP_Settings_GameSpeed_Slider.Value = MPTEMPsetupData.starting_gamespeed / 5;
			MainViewModel.Instance.MP_Settings_GameSpeed = MPTEMPsetupData.starting_gamespeed.ToString();
			RefMP_Settings_Peacetime_Slider.Value = MPTEMPsetupData.peacetime;
			if (FatControler.ukrainian)
			{
				MainViewModel.Instance.MP_Settings_Peacetime = MPTEMPsetupData.peacetime + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 168);
			}
			else
			{
				MainViewModel.Instance.MP_Settings_Peacetime = MPTEMPsetupData.peacetime + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 168);
			}
			if (MPTEMPsetupData.no_knockdown_walls > 0)
			{
				MainViewModel.Instance.MP_Settings_Wall = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_ON);
			}
			else
			{
				MainViewModel.Instance.MP_Settings_Wall = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_OFF);
			}
			if (MPTEMPsetupData.no_ingame_alliances > 0)
			{
				MainViewModel.Instance.MP_Settings_Alliances = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_EVENT_CONDITIONS);
			}
			else
			{
				MainViewModel.Instance.MP_Settings_Alliances = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_EDIT_ACTIONS);
			}
			if (MPTEMPsetupData.allow_troop_gold > 0)
			{
				MainViewModel.Instance.MP_Settings_Troops = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_AVAILABLE);
			}
			else
			{
				MainViewModel.Instance.MP_Settings_Troops = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_WEEKSATMARKET);
			}
			if (MPTEMPsetupData.allow_autotrading > 0)
			{
				MainViewModel.Instance.MP_Settings_Autotrading = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_ON);
			}
			else
			{
				MainViewModel.Instance.MP_Settings_Autotrading = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_OFF);
			}
			switch (MPTEMPsetupData.autosave)
			{
			case 0:
				MainViewModel.Instance.MP_Settings_Autosave = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_MACEMEN);
				break;
			case 5:
				MainViewModel.Instance.MP_Settings_Autosave = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_SWORDSMEN);
				break;
			case 10:
				MainViewModel.Instance.MP_Settings_Autosave = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_KNIGHTS);
				break;
			case 20:
				MainViewModel.Instance.MP_Settings_Autosave = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_LADDERMEN);
				break;
			}
			RefSetupMaxPlayersSlider.Value = PlayerCap;
			if (currentLobby.isHost)
			{
				MainViewModel.Instance.MPSettingHeight = "520";
			}
			else
			{
				MainViewModel.Instance.MPSettingHeight = "490";
			}
			MainViewModel.Instance.Show_MPSettings = true;
			break;
		}
		case "Settings_TechLevel":
			MPTEMPsetupData.starting_keep_type += 2;
			if (MPTEMPsetupData.starting_keep_type > 5)
			{
				MPTEMPsetupData.starting_keep_type = 0;
			}
			switch (MPTEMPsetupData.starting_keep_type)
			{
			case 0:
				MPTEMPsetupData.starting_goods_level = 1;
				MPTEMPsetupData.starting_troops_level = 0;
				break;
			case 2:
				MPTEMPsetupData.starting_goods_level = 2;
				MPTEMPsetupData.starting_troops_level = 1;
				break;
			case 4:
				MPTEMPsetupData.starting_goods_level = 3;
				MPTEMPsetupData.starting_troops_level = 3;
				break;
			}
			MainViewModel.Instance.MP_Settings_TechLevel = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, 79 + MPTEMPsetupData.starting_keep_type / 2);
			MainViewModel.Instance.MP_Settings_StartingGoods = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, 17 + MPTEMPsetupData.starting_goods_level);
			MainViewModel.Instance.MP_Settings_StartingTroops = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, 23 + MPTEMPsetupData.starting_troops_level);
			break;
		case "Settings_StartingGoods":
			MPTEMPsetupData.starting_goods_level++;
			if (MPTEMPsetupData.starting_goods_level > 3)
			{
				MPTEMPsetupData.starting_goods_level = 1;
			}
			MainViewModel.Instance.MP_Settings_StartingGoods = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, 17 + MPTEMPsetupData.starting_goods_level);
			break;
		case "Settings_StartingTroops":
			MPTEMPsetupData.starting_troops_level++;
			if (MPTEMPsetupData.starting_troops_level > 3)
			{
				MPTEMPsetupData.starting_troops_level = 0;
			}
			MainViewModel.Instance.MP_Settings_StartingTroops = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, 23 + MPTEMPsetupData.starting_troops_level);
			break;
		case "MP_Settings_Toggle_Bulk":
			MPTEMPsetupData.trading[3]++;
			if (MPTEMPsetupData.trading[3] > 1)
			{
				MPTEMPsetupData.trading[3] = 0;
			}
			MainViewModel.Instance.MP_Settings_Toggle_BulkVis = MPTEMPsetupData.trading[3] == 0;
			break;
		case "MP_Settings_Toggle_Food":
			MPTEMPsetupData.trading[1]++;
			if (MPTEMPsetupData.trading[1] > 1)
			{
				MPTEMPsetupData.trading[1] = 0;
			}
			MainViewModel.Instance.MP_Settings_Toggle_FoodVis = MPTEMPsetupData.trading[1] == 0;
			break;
		case "MP_Settings_Toggle_Weapons":
			MPTEMPsetupData.trading[0]++;
			if (MPTEMPsetupData.trading[0] > 1)
			{
				MPTEMPsetupData.trading[0] = 0;
			}
			MainViewModel.Instance.MP_Settings_Toggle_WeaponsVis = MPTEMPsetupData.trading[0] == 0;
			break;
		case "Settings_Walls":
			if (MPTEMPsetupData.no_knockdown_walls == 0)
			{
				MPTEMPsetupData.no_knockdown_walls = 1;
			}
			else
			{
				MPTEMPsetupData.no_knockdown_walls = 0;
			}
			if (MPTEMPsetupData.no_knockdown_walls > 0)
			{
				MainViewModel.Instance.MP_Settings_Wall = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_ON);
			}
			else
			{
				MainViewModel.Instance.MP_Settings_Wall = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_OFF);
			}
			break;
		case "Settings_Alliances":
			if (MPTEMPsetupData.no_ingame_alliances == 0)
			{
				MPTEMPsetupData.no_ingame_alliances = 1;
			}
			else
			{
				MPTEMPsetupData.no_ingame_alliances = 0;
			}
			if (MPTEMPsetupData.no_ingame_alliances > 0)
			{
				MainViewModel.Instance.MP_Settings_Alliances = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_EVENT_CONDITIONS);
			}
			else
			{
				MainViewModel.Instance.MP_Settings_Alliances = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_EDIT_ACTIONS);
			}
			break;
		case "Settings_Troops":
			if (MPTEMPsetupData.allow_troop_gold == 0)
			{
				MPTEMPsetupData.allow_troop_gold = 1;
			}
			else
			{
				MPTEMPsetupData.allow_troop_gold = 0;
			}
			if (MPTEMPsetupData.allow_troop_gold > 0)
			{
				MainViewModel.Instance.MP_Settings_Troops = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_AVAILABLE);
			}
			else
			{
				MainViewModel.Instance.MP_Settings_Troops = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_WEEKSATMARKET);
			}
			break;
		case "Settings_Autotrading":
			if (MPTEMPsetupData.allow_autotrading == 0)
			{
				MPTEMPsetupData.allow_autotrading = 1;
			}
			else
			{
				MPTEMPsetupData.allow_autotrading = 0;
			}
			if (MPTEMPsetupData.allow_autotrading > 0)
			{
				MainViewModel.Instance.MP_Settings_Autotrading = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_ON);
			}
			else
			{
				MainViewModel.Instance.MP_Settings_Autotrading = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_OFF);
			}
			break;
		case "Settings_Autosave":
			if (MPTEMPsetupData.autosave == 0)
			{
				MPTEMPsetupData.autosave = 5;
			}
			else if (MPTEMPsetupData.autosave == 5)
			{
				MPTEMPsetupData.autosave = 10;
			}
			else if (MPTEMPsetupData.autosave == 10)
			{
				MPTEMPsetupData.autosave = 20;
			}
			else if (MPTEMPsetupData.autosave == 20)
			{
				MPTEMPsetupData.autosave = 0;
			}
			switch (MPTEMPsetupData.autosave)
			{
			case 0:
				MainViewModel.Instance.MP_Settings_Autosave = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_MACEMEN);
				break;
			case 5:
				MainViewModel.Instance.MP_Settings_Autosave = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_SWORDSMEN);
				break;
			case 10:
				MainViewModel.Instance.MP_Settings_Autosave = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_KNIGHTS);
				break;
			case 20:
				MainViewModel.Instance.MP_Settings_Autosave = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_XPLAY_WAITING_ROOM, Enums.eTextValues.TEXT_SCN_LADDERMEN);
				break;
			}
			break;
		case "ApplySettings":
		{
			MainViewModel.Instance.Show_MPSettings = false;
			MPTEMPsetupData.starting_gold = (int)RefMP_Settings_Gold_Slider.Value * 50;
			MPTEMPsetupData.king_of_the_hill_points = (int)RefMP_Settings_Koth_Slider.Value * 1000;
			MPTEMPsetupData.peacetime = (int)RefMP_Settings_Peacetime_Slider.Value;
			if (!MainViewModel.Instance.Show_MPPeacetime)
			{
				MPTEMPsetupData.peacetime = 0;
			}
			string str = MPTEMPsetupData.ToString();
			MPsetupData.FromString(str);
			PlayerCap = (int)RefSetupMaxPlayersSlider.Value;
			updateLevelsFromTechLevel();
			UpdateHostInfo(delayed: true);
			break;
		}
		case "CancelSettings":
			MainViewModel.Instance.Show_MPSettings = false;
			break;
		case "RetrieveMap":
			multiplayerMapRequestTime = DateTime.UtcNow.AddSeconds(60.0);
			Platform_Multiplayer.Instance.RequestMap(currentLobby.mapFileName, delegate
			{
				multiplayerMapRequestTime = DateTime.UtcNow.AddSeconds(1.0);
			});
			break;
		case "CancelConnecting":
			LeaveLobby();
			MainViewModel.Instance.InitNewScene(Enums.SceneIDS.FrontEnd);
			break;
		case "ClearFilter":
			RefMP_SearchFilter.Text = "";
			MainViewModel.Instance.MultiplayerFilterLabelVis = Visibility.Visible;
			MainViewModel.Instance.MultiplayerFilterButtonVis = Visibility.Hidden;
			break;
		case "ShowSharing":
			MainViewModel.Instance.Show_MPSharing = true;
			break;
		case "CopySharing":
			GUIUtility.systemCopyBuffer = Platform_Multiplayer.Instance.ShareCodeString;
			break;
		case "DisplaySharing":
			ShowSharingCode = true;
			break;
		case "ColourPicker":
			ShowColourPicker();
			break;
		case "Col1":
			SetShieldColour(1);
			break;
		case "Col2":
			SetShieldColour(2);
			break;
		case "Col3":
			SetShieldColour(3);
			break;
		case "Col4":
			SetShieldColour(4);
			break;
		case "Col5":
			SetShieldColour(5);
			break;
		case "Col6":
			SetShieldColour(6);
			break;
		case "Col7":
			SetShieldColour(7);
			break;
		case "Col8":
			SetShieldColour(8);
			break;
		}
	}

	private void UpdateButtons()
	{
	}

	public void Update()
	{
		if (MPGameLoading)
		{
			return;
		}
		if (MPLocalReady)
		{
			Platform_Multiplayer.Instance.ReceiveGameMessages();
		}
		if (!MainViewModel.Instance.Show_CreatingMPHost)
		{
			bool refreshTeams = false;
			bool settingsChanged = false;
			if (Platform_Multiplayer.Instance.RefreshLobbyList(ref MPsetupData, ref refreshTeams, ref settingsChanged))
			{
				LeaveLobby();
				ShowLobbyScreen();
			}
			else if (refreshTeams)
			{
				UpdateHostInfo();
			}
			if (settingsChanged && MPLocalReady)
			{
				MPLocalReady = false;
				MainViewModel.Instance.MP_Settings_Button = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 59);
				Platform_Multiplayer.Instance.SetMemberReadyState(MPLocalReady);
			}
			if (currentLobby != null && !currentLobby.isHost)
			{
				EngineInterface.setMultiplayerStartingData(MPsetupData);
			}
			if (currentLobby != null && currentLobby.startGame != null && currentLobby.startGame.Length > 0)
			{
				if (!currentLobby.isHost || startGameTime < DateTime.UtcNow)
				{
					if (currentLobby.startGame == "GO!")
					{
						Platform_Multiplayer.Instance.StartGame(MPsetupData, selectedMPHeader);
						MPGameLoading = true;
						MainViewModel.Instance.Show_MPJoiningLobby = false;
						MainViewModel.Instance.Show_MPGameCreation = false;
					}
					else
					{
						FileHeader headerFromMpSaveFileName = MapFileManager.Instance.GetHeaderFromMpSaveFileName(currentLobby.startGame);
						if (headerFromMpSaveFileName != null)
						{
							Platform_Multiplayer.Instance.StartSave(MPsetupData, headerFromMpSaveFileName);
							MPGameLoading = true;
							MainViewModel.Instance.Show_MPJoiningLobby = false;
							MainViewModel.Instance.Show_MPGameCreation = false;
						}
						else
						{
							Debug.LogError("Missing Save file : " + currentLobby.startGame);
						}
					}
				}
				Director.instance.StartMultiplayerGame();
			}
			if (currentLobby != null && currentLobby.isHost && DateTime.UtcNow > nextHostSendPings)
			{
				nextHostSendPings = DateTime.UtcNow.AddSeconds(2.0);
				Platform_Multiplayer.Instance.HostSendLobbyPings();
			}
		}
		if (delayedSendDataToLobby != DateTime.MinValue && DateTime.UtcNow > delayedSendDataToLobby)
		{
			UpdateHostInfo();
		}
		if (MainViewModel.Instance.Show_MPJoiningLobby && (DateTime.UtcNow - lastAutoRefreshTime).TotalSeconds > 30.0)
		{
			lastAutoRefreshTime = DateTime.UtcNow;
			Platform_Multiplayer.Instance.GetLobbies(delegate
			{
				lobbies = Platform_Multiplayer.Instance.ReadLobbies();
				populateLobbyList();
			});
		}
		if (pendingMPHost)
		{
			pendingMPHost = false;
			if (headerlist != null && headerlist.Count > 0)
			{
				if (selectedMPHeader == null)
				{
					headerlist = MapFileManager.Instance.GetMultiplayerMaps(sortByColumn, sortByAscending, numConnectedPlayers, includeBuiltIn, includeUser, includeWorkshop);
					selectedMPHeader = headerlist[0];
					populateMapList(selectedMPHeader);
					updateRadarTexture(selectedMPHeader);
					GameData.Instance.SetMissionTextFromHeader(selectedMPHeader);
					MainViewModel.Instance.StandaloneMissionText = GameData.Instance.GetMissionBriefing();
					MainViewModel.Instance.Show_MPKothMap = selectedMPHeader.isKingOfTheHill > 0;
					MainViewModel.Instance.Show_MPPeacetime = selectedMPHeader.isKingOfTheHill == 0;
				}
				Platform_Multiplayer.Instance.CreateLobby(RefTextBoxGameName.Text, selectedMPHeader.display_filename, selectedMPHeader.fileName, selectedMPHeader.maxPlayers, (selectedMPHeader.isKingOfTheHill > 0) ? 1 : 0, MPLobbyMode, MPsetupData.ToString(), selectedMPHeader.crc, delegate
				{
					MPHostLobbyname = RefTextBoxGameName.Text;
					currentLobby = Platform_Multiplayer.Instance.GetActiveLobby();
					ShowSetupScreen();
				}, delegate(string name, string message, int colourID)
				{
					receivedLobbyChat(name, message, colourID);
				});
			}
		}
		if (currentLobby != null)
		{
			List<Platform_Multiplayer.MPLobbyMember> members = currentLobby.members;
			MPTotalPlayers = members.Count;
			for (int i = 0; i < members.Count; i++)
			{
				Platform_Multiplayer.MPLobbyMember mPLobbyMember = members[i];
				playerRows[i].Update(this, mPLobbyMember, i);
				if (mPLobbyMember.IsSelf())
				{
					UpdateColourShields(GetPlayerColour());
				}
				if (currentLobby.isHost && mPLobbyMember != null && mPLobbyMember.mapRequested != null && mPLobbyMember.mapRequested.Length > 0 && mPLobbyMember.mapRequested.ToLower() == currentLobby.mapFileName.ToLower() && Platform_Multiplayer.Instance.SendMap(mPLobbyMember, currentLobby.mapFileName, selectedMPHeader.filePath))
				{
					addSystemLobbyChat(mPLobbyMember.name, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 139) + " ->");
				}
			}
			Platform_Multiplayer.Instance.ProcessMapSendQueue();
			for (int j = members.Count; j < 8; j++)
			{
				playerRows[j].Update(this, null, j);
			}
			if (MPLocalReady)
			{
				if (readyAnimPlaying)
				{
					readyAnimPlaying = false;
					pulseAnimation.Stop();
				}
				ImageSource imageSource = MainViewModel.Instance.GameSprites[105];
				if ((ImageSource)PropEx.GetSprite1(RefReadyButton) != imageSource)
				{
					PropEx.SetSprite1(RefReadyButton, imageSource);
					imageSource = MainViewModel.Instance.GameSprites[106];
					PropEx.SetSprite2(RefReadyButton, imageSource);
					PropEx.SetSprite3(RefReadyButton, imageSource);
				}
			}
			else
			{
				if (!readyAnimPlaying)
				{
					readyAnimPlaying = true;
					pulseAnimation.Begin();
				}
				ImageSource imageSource2 = MainViewModel.Instance.GameSprites[103];
				if ((ImageSource)PropEx.GetSprite1(RefReadyButton) != imageSource2)
				{
					PropEx.SetSprite1(RefReadyButton, imageSource2);
					imageSource2 = MainViewModel.Instance.GameSprites[104];
					PropEx.SetSprite2(RefReadyButton, imageSource2);
					PropEx.SetSprite3(RefReadyButton, imageSource2);
				}
			}
			if (!currentLobby.isHost)
			{
				(MainViewModel.GetListBox(RefFileLists) as ListBox).IsHitTestVisible = false;
				if (multiplayerMapRequestTime != DateTime.MinValue && multiplayerMapRequestTime < DateTime.UtcNow)
				{
					multiplayerMapRequestTime = DateTime.MinValue;
					MPMapChecked = false;
				}
				if (!MPMapChecked || MPLastMapName != currentLobby.mapFileName)
				{
					MPLocalReady = false;
					Platform_Multiplayer.Instance.SetMemberReadyState(state: false);
					MPMapChecked = true;
					MPLastMapName = currentLobby.mapFileName;
					int intFromString = EditorDirector.getIntFromString(currentLobby.crc);
					FileHeader headerFromFileNameMP = MapFileManager.Instance.GetHeaderFromFileNameMP(MPLastMapName, intFromString);
					if (headerFromFileNameMP == null)
					{
						Platform_Multiplayer.Instance.SetMapStatus(1);
						MPMapValid = false;
						regetMapListNextTime = true;
						selectedMPHeader = null;
						MainViewModel.Instance.MP_RetrieveMapName = MPLastMapName;
						MainViewModel.Instance.Show_MPFileList = false;
						MainViewModel.Instance.Show_MPRadar = false;
						MainViewModel.Instance.Show_MPRetrieveMapPanel = true;
					}
					else if (headerFromFileNameMP.crc != intFromString)
					{
						Platform_Multiplayer.Instance.SetMapStatus(2);
						MPMapValid = false;
						regetMapListNextTime = true;
						selectedMPHeader = null;
						MainViewModel.Instance.MP_RetrieveMapName = MPLastMapName;
						MainViewModel.Instance.Show_MPFileList = false;
						MainViewModel.Instance.Show_MPRadar = false;
						MainViewModel.Instance.Show_MPRetrieveMapPanel = true;
					}
					else
					{
						Platform_Multiplayer.Instance.SetMapStatus(0);
						MPMapValid = true;
						MainViewModel.Instance.Show_MPFileList = true;
						MainViewModel.Instance.Show_MPRetrieveMapPanel = false;
						if (regetMapListNextTime)
						{
							headerlist = MapFileManager.Instance.GetMultiplayerMaps(sortByColumn, sortByAscending, numConnectedPlayers, includeBuiltIn, includeUser, includeWorkshop);
							populateMapList();
						}
						regetMapListNextTime = false;
						selectedMPHeader = headerFromFileNameMP;
						foreach (FileRow item in RefFileLists.ItemsSource)
						{
							if (item.fileHeader == selectedMPHeader)
							{
								RefFileLists.SelectedItem = item;
								RefFileLists.ScrollIntoView(RefFileLists.SelectedItem);
								break;
							}
						}
						updateRadarTexture(selectedMPHeader);
						GameData.Instance.SetMissionTextFromHeader(selectedMPHeader);
						MainViewModel.Instance.StandaloneMissionText = GameData.Instance.GetMissionBriefing();
						MainViewModel.Instance.Show_MPKothMap = selectedMPHeader.isKingOfTheHill > 0;
						MainViewModel.Instance.Show_MPPeacetime = selectedMPHeader.isKingOfTheHill == 0;
					}
				}
			}
			else
			{
				(MainViewModel.GetListBox(RefFileLists) as ListBox).IsHitTestVisible = true;
				MPMapValid = true;
			}
			if (ShowSharingCode)
			{
				MainViewModel.Instance.MultiplayerShareCode = Platform_Multiplayer.Instance.ShareCodeString;
			}
			else
			{
				MainViewModel.Instance.MultiplayerShareCode = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 179);
			}
			if (MainViewModel.Instance.Show_MPRetrieveMapPanel)
			{
				if (multiplayerMapRequestTime == DateTime.MinValue)
				{
					MainViewModel.Instance.Show_MPRetrieveMapButton = true;
					MainViewModel.Instance.Show_MPRetrievingMapMessage = false;
				}
				else
				{
					MainViewModel.Instance.Show_MPRetrieveMapButton = false;
					MainViewModel.Instance.Show_MPRetrievingMapMessage = true;
				}
			}
			bool flag = currentLobby.numLobbyMembers > 1 && currentLobby.numLobbyMembers <= currentLobby.iMaxPlayers && MPLocalReady;
			if (flag)
			{
				foreach (Platform_Multiplayer.MPLobbyMember member in currentLobby.members)
				{
					if (!member.ready)
					{
						flag = false;
						break;
					}
				}
				if (flag && !currentLobby.getEnoughTeams())
				{
					flag = false;
				}
			}
			RefMultiplayerPlayButton.IsEnabled = flag;
			RefLoadButton.IsEnabled = flag;
		}
		if (!((DateTime.UtcNow - lastScrollTest).TotalMilliseconds > 150.0) || (!MainViewModel.Instance.Show_MPJoiningLobby && (currentLobby == null || !currentLobby.isHost)))
		{
			return;
		}
		if (KeyManager.instance.CursorUpHeld)
		{
			lastScrollTest = DateTime.UtcNow;
			ListView listView = ((!MainViewModel.Instance.Show_MPJoiningLobby) ? RefFileLists : RefLobbyLists);
			ScrollViewer scrollViewer = MainViewModel.GetScrollViewer(listView) as ScrollViewer;
			if (!(scrollViewer != null))
			{
				return;
			}
			if (listView.SelectedItem == null)
			{
				scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - 30f);
				return;
			}
			if (listView.SelectedIndex > 0)
			{
				listView.SelectedIndex--;
			}
			listView.ScrollIntoView(listView.SelectedItem);
		}
		else
		{
			if (!KeyManager.instance.CursorDownHeld)
			{
				return;
			}
			lastScrollTest = DateTime.UtcNow;
			ListView listView2 = ((!MainViewModel.Instance.Show_MPJoiningLobby) ? RefFileLists : RefLobbyLists);
			ScrollViewer scrollViewer2 = MainViewModel.GetScrollViewer(listView2) as ScrollViewer;
			if (!(scrollViewer2 != null))
			{
				return;
			}
			if (listView2.SelectedItem == null)
			{
				scrollViewer2.ScrollToVerticalOffset(scrollViewer2.VerticalOffset + 30f);
				return;
			}
			if (listView2.SelectedIndex < RefFileLists.Items.Count - 1)
			{
				listView2.SelectedIndex++;
			}
			listView2.ScrollIntoView(listView2.SelectedItem);
		}
	}

	private void ShowLobbyScreen()
	{
		MainViewModel.Instance.Show_MPJoiningLobby = true;
		MainViewModel.Instance.Show_MPGameCreation = false;
		MainViewModel.Instance.Show_MPSettings = false;
		RefMultiplayerPlayButton.Visibility = Visibility.Hidden;
		RefLoadButton.Visibility = Visibility.Hidden;
		MainViewModel.Instance.StandaloneTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 51);
		lobbyChat.Clear();
		RefMP_ChatDisplay.Inlines.Clear();
	}

	private void ShowSetupScreen()
	{
		if (currentLobby != null)
		{
			MainViewModel.Instance.Show_MPIsHost = currentLobby.isHost;
			MainViewModel.Instance.Show_MPFileList = true;
			MainViewModel.Instance.Show_MPJoiningLobby = false;
			MainViewModel.Instance.Show_MPGameCreation = true;
			MainViewModel.Instance.Show_MPSettings = false;
			MainViewModel.Instance.Show_MPRetrieveMapPanel = false;
			RefMP_ChatInput.Text = "";
			MainViewModel.Instance.MP_LobbyChatWindow = "";
			MainViewModel.Instance.MP_Settings_Button = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 58);
			includeUser = true;
			includeBuiltIn = true;
			includeWorkshop = true;
			RefIncludeBuiltin.IsChecked = true;
			RefIncludeUser.IsChecked = true;
			RefIncludeWorkshop.IsChecked = true;
			if (currentLobby.isHost)
			{
				RefMultiplayerPlayButton.Visibility = Visibility.Visible;
				RefMultiplayerPlayButton.IsEnabled = false;
				RefLoadButton.Visibility = Visibility.Visible;
				RefLoadButton.IsEnabled = false;
			}
			MainViewModel.Instance.StandaloneTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 56);
		}
	}

	private void InitializeComponent()
	{
		NoesisUnity.LoadComponent(this, "Assets/GUI/XAMLResources/FRONT_Multiplayer.xaml");
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

	private void updateHostLobbyButton()
	{
		if (MPLobbyMode == 0)
		{
			MainViewModel.Instance.MP_PublicPrivateText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 54);
		}
		else if (MPLobbyMode == 4)
		{
			MainViewModel.Instance.MP_PublicPrivateText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 177);
		}
		else
		{
			MainViewModel.Instance.MP_PublicPrivateText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 55);
		}
	}

	private void receivedLobbyChat(string _name, string _message, int _colourID)
	{
		if (_message.Length > 300)
		{
			_message = _message.Substring(0, 300);
		}
		_message = _message.Replace("\n", "");
		LobbyChatEntry item = new LobbyChatEntry
		{
			name = _name,
			message = _message,
			colourID = _colourID,
			received = DateTime.UtcNow
		};
		lobbyChat.Add(item);
		if (lobbyChat.Count > 100)
		{
			lobbyChat.RemoveAt(0);
		}
		refreshLobbyChat();
	}

	private void refreshLobbyChat()
	{
		RefMP_ChatDisplay.Inlines.Clear();
		foreach (LobbyChatEntry item5 in lobbyChat)
		{
			if (item5.colourID >= 0)
			{
				ImageSource colourShield = GetColourShield(item5.colourID);
				InlineUIContainer item = new InlineUIContainer
				{
					Child = new Image
					{
						Source = colourShield,
						Width = 14f,
						Height = 14f
					}
				};
				RefMP_ChatDisplay.Inlines.Add(item);
				InlineUIContainer item2 = new InlineUIContainer
				{
					Child = new TextBlock
					{
						Text = " " + item5.name,
						Width = 600f,
						FontSize = 14f,
						Height = 14f
					}
				};
				RefMP_ChatDisplay.Inlines.Add(item2);
				RefMP_ChatDisplay.Inlines.Add(new LineBreak());
				InlineUIContainer item3 = new InlineUIContainer
				{
					Child = new TextBlock
					{
						Text = item5.message,
						TextWrapping = TextWrapping.WrapWithOverflow,
						Margin = new Thickness(40f, 0f, 5f, 0f),
						FontSize = 12f
					}
				};
				RefMP_ChatDisplay.Inlines.Add(item3);
				RefMP_ChatDisplay.Inlines.Add(new Run(Environment.NewLine));
			}
			else
			{
				InlineUIContainer item4 = new InlineUIContainer
				{
					Child = new TextBlock
					{
						Text = item5.message + " " + item5.name,
						Width = 600f,
						FontSize = 14f,
						Height = 16f
					}
				};
				RefMP_ChatDisplay.Inlines.Add(item4);
				RefMP_ChatDisplay.Inlines.Add(new LineBreak());
			}
		}
		RefMP_ChatScrollView.ScrollToBottom();
	}

	private void addSystemLobbyChat(string _name, string _message)
	{
		LobbyChatEntry item = new LobbyChatEntry
		{
			name = _name,
			message = _message,
			colourID = -1,
			received = DateTime.UtcNow
		};
		lobbyChat.Add(item);
		if (lobbyChat.Count > 100)
		{
			lobbyChat.RemoveAt(0);
		}
		refreshLobbyChat();
	}

	private void updateLevelsFromTechLevel()
	{
		switch (MPsetupData.starting_goods_level)
		{
		case 1:
		{
			for (int i = 0; i < 20; i++)
			{
				MPsetupData.starting_goods[i] = start_low_goods_level[MPsetupData.starting_keep_type, i];
			}
			break;
		}
		case 2:
		{
			for (int i = 0; i < 20; i++)
			{
				MPsetupData.starting_goods[i] = start_med_goods_level[MPsetupData.starting_keep_type, i];
			}
			break;
		}
		case 3:
		{
			for (int i = 0; i < 20; i++)
			{
				MPsetupData.starting_goods[i] = start_high_goods_level[MPsetupData.starting_keep_type, i];
			}
			break;
		}
		}
		switch (MPsetupData.starting_troops_level)
		{
		case 0:
		{
			for (int i = 0; i < 10; i++)
			{
				MPsetupData.starting_troops[i] = 0;
			}
			break;
		}
		case 1:
		{
			for (int i = 0; i < 10; i++)
			{
				MPsetupData.starting_troops[i] = start_few_troop_level[MPsetupData.starting_keep_type, i];
			}
			break;
		}
		case 2:
		{
			for (int i = 0; i < 10; i++)
			{
				MPsetupData.starting_troops[i] = start_some_troop_level[MPsetupData.starting_keep_type, i];
			}
			break;
		}
		case 3:
		{
			for (int i = 0; i < 10; i++)
			{
				MPsetupData.starting_troops[i] = start_many_troop_level[MPsetupData.starting_keep_type, i];
			}
			break;
		}
		}
	}

	private void MP_Settings_Gold_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		if (panelLoaded && MPTEMPsetupData != null)
		{
			int starting_gold = (int)RefMP_Settings_Gold_Slider.Value * 50;
			MainViewModel.Instance.MP_Settings_Gold = starting_gold.ToString();
			MPTEMPsetupData.starting_gold = starting_gold;
		}
	}

	private void MP_Settings_Koth_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		if (panelLoaded && MPTEMPsetupData != null)
		{
			int king_of_the_hill_points = (int)RefMP_Settings_Koth_Slider.Value * 1000;
			MainViewModel.Instance.MP_Settings_Koth = king_of_the_hill_points.ToString();
			MPTEMPsetupData.king_of_the_hill_points = king_of_the_hill_points;
		}
	}

	private void MP_Settings_Peacetime_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		if (panelLoaded && MPTEMPsetupData != null)
		{
			int peacetime = (int)RefMP_Settings_Peacetime_Slider.Value;
			if (FatControler.ukrainian)
			{
				MainViewModel.Instance.MP_Settings_Peacetime = peacetime + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 168);
			}
			else
			{
				MainViewModel.Instance.MP_Settings_Peacetime = peacetime + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 168);
			}
			MPTEMPsetupData.peacetime = peacetime;
		}
	}

	private void MP_Settings_GameSpeed_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		if (panelLoaded && MPTEMPsetupData != null)
		{
			int starting_gamespeed = (int)RefMP_Settings_GameSpeed_Slider.Value * 5;
			MainViewModel.Instance.MP_Settings_GameSpeed = starting_gamespeed.ToString();
			MPTEMPsetupData.starting_gamespeed = starting_gamespeed;
		}
	}

	private void UpdateHostInfo(bool delayed = false)
	{
		EngineInterface.setMultiplayerStartingData(MPsetupData);
		if (delayed)
		{
			delayedSendDataToLobby = DateTime.UtcNow.AddSeconds(1.5);
		}
		else if (selectedMPHeader != null)
		{
			delayedSendDataToLobby = DateTime.MinValue;
			int num = selectedMPHeader.maxPlayers;
			if (num > PlayerCap)
			{
				num = PlayerCap;
			}
			Platform_Multiplayer.Instance.UpdateHostLobbyInfo(MPHostLobbyname, selectedMPHeader.display_filename, selectedMPHeader.fileName, num, (selectedMPHeader.isKingOfTheHill > 0) ? 1 : 0, MPsetupData.ToString(), selectedMPHeader.crc);
		}
	}

	private void ShowColourPicker()
	{
		if (currentLobby != null)
		{
			int playerColour = GetPlayerColour();
			if (playerColour > 0)
			{
				MainViewModel.Instance.Show_MPColours = true;
				UpdateColourShields(playerColour);
			}
		}
	}

	private int GetPlayerColour()
	{
		int result = -1;
		foreach (Platform_Multiplayer.MPLobbyMember member in currentLobby.members)
		{
			if (member.IsSelf())
			{
				result = member.colourID;
				break;
			}
		}
		return result;
	}

	private void SetShieldColour(int colourID)
	{
		Platform_Multiplayer.Instance.SetPlayerColour(colourID);
		UpdateColourShields(colourID);
	}

	private void UpdateColourShields(int colourID)
	{
		PropEx.SetSprite1(RefColourSelectButton, GetColourShield(colourID));
		PropEx.SetSprite2(RefColourSelectButton, GetColourShield(colourID, 1));
		PropEx.SetSprite3(RefColourSelectButton, GetColourShield(colourID, 1));
		PropEx.SetSprite4(RefColourSelectButton, GetColourShield(colourID));
		List<int> usedColours = Platform_Multiplayer.Instance.GetUsedColours(colourID);
		bool flag = !usedColours.Contains(1);
		RefColShield1.IsEnabled = flag;
		RefColShield1.Opacity = (flag ? 1f : 0.5f);
		bool flag2 = !usedColours.Contains(2);
		RefColShield2.IsEnabled = flag2;
		RefColShield2.Opacity = (flag2 ? 1f : 0.5f);
		bool flag3 = !usedColours.Contains(3);
		RefColShield3.IsEnabled = flag3;
		RefColShield3.Opacity = (flag3 ? 1f : 0.5f);
		bool flag4 = !usedColours.Contains(4);
		RefColShield4.IsEnabled = flag4;
		RefColShield4.Opacity = (flag4 ? 1f : 0.5f);
		bool flag5 = !usedColours.Contains(5);
		RefColShield5.IsEnabled = flag5;
		RefColShield5.Opacity = (flag5 ? 1f : 0.5f);
		bool flag6 = !usedColours.Contains(6);
		RefColShield6.IsEnabled = flag6;
		RefColShield6.Opacity = (flag6 ? 1f : 0.5f);
		bool flag7 = !usedColours.Contains(7);
		RefColShield7.IsEnabled = flag7;
		RefColShield7.Opacity = (flag7 ? 1f : 0.5f);
		bool flag8 = !usedColours.Contains(8);
		RefColShield8.IsEnabled = flag8;
		RefColShield8.Opacity = (flag8 ? 1f : 0.5f);
		if (colourID == 1)
		{
			PropEx.SetSprite1(RefColShield1, GetColourShield(1, 2));
			PropEx.SetSprite2(RefColShield1, GetColourShield(1, 2));
			PropEx.SetSprite3(RefColShield1, GetColourShield(1, 2));
			PropEx.SetSprite4(RefColShield1, GetColourShield(1, 2));
		}
		else
		{
			PropEx.SetSprite1(RefColShield1, GetColourShield(1));
			PropEx.SetSprite2(RefColShield1, GetColourShield(1, 1));
			PropEx.SetSprite3(RefColShield1, GetColourShield(1, 1));
			PropEx.SetSprite4(RefColShield1, GetColourShield(1));
		}
		if (colourID == 2)
		{
			PropEx.SetSprite1(RefColShield2, GetColourShield(2, 2));
			PropEx.SetSprite2(RefColShield2, GetColourShield(2, 2));
			PropEx.SetSprite3(RefColShield2, GetColourShield(2, 2));
			PropEx.SetSprite4(RefColShield2, GetColourShield(2, 2));
		}
		else
		{
			PropEx.SetSprite1(RefColShield2, GetColourShield(2));
			PropEx.SetSprite2(RefColShield2, GetColourShield(2, 1));
			PropEx.SetSprite3(RefColShield2, GetColourShield(2, 1));
			PropEx.SetSprite4(RefColShield2, GetColourShield(2));
		}
		if (colourID == 3)
		{
			PropEx.SetSprite1(RefColShield3, GetColourShield(3, 2));
			PropEx.SetSprite2(RefColShield3, GetColourShield(3, 2));
			PropEx.SetSprite3(RefColShield3, GetColourShield(3, 2));
			PropEx.SetSprite4(RefColShield3, GetColourShield(3, 2));
		}
		else
		{
			PropEx.SetSprite1(RefColShield3, GetColourShield(3));
			PropEx.SetSprite2(RefColShield3, GetColourShield(3, 1));
			PropEx.SetSprite3(RefColShield3, GetColourShield(3, 1));
			PropEx.SetSprite4(RefColShield3, GetColourShield(3));
		}
		if (colourID == 4)
		{
			PropEx.SetSprite1(RefColShield4, GetColourShield(4, 2));
			PropEx.SetSprite2(RefColShield4, GetColourShield(4, 2));
			PropEx.SetSprite3(RefColShield4, GetColourShield(4, 2));
			PropEx.SetSprite4(RefColShield4, GetColourShield(4, 2));
		}
		else
		{
			PropEx.SetSprite1(RefColShield4, GetColourShield(4));
			PropEx.SetSprite2(RefColShield4, GetColourShield(4, 1));
			PropEx.SetSprite3(RefColShield4, GetColourShield(4, 1));
			PropEx.SetSprite4(RefColShield4, GetColourShield(4));
		}
		if (colourID == 5)
		{
			PropEx.SetSprite1(RefColShield5, GetColourShield(5, 2));
			PropEx.SetSprite2(RefColShield5, GetColourShield(5, 2));
			PropEx.SetSprite3(RefColShield5, GetColourShield(5, 2));
			PropEx.SetSprite4(RefColShield5, GetColourShield(5, 2));
		}
		else
		{
			PropEx.SetSprite1(RefColShield5, GetColourShield(5));
			PropEx.SetSprite2(RefColShield5, GetColourShield(5, 1));
			PropEx.SetSprite3(RefColShield5, GetColourShield(5, 1));
			PropEx.SetSprite4(RefColShield5, GetColourShield(5));
		}
		if (colourID == 6)
		{
			PropEx.SetSprite1(RefColShield6, GetColourShield(6, 2));
			PropEx.SetSprite2(RefColShield6, GetColourShield(6, 2));
			PropEx.SetSprite3(RefColShield6, GetColourShield(6, 2));
			PropEx.SetSprite4(RefColShield6, GetColourShield(6, 2));
		}
		else
		{
			PropEx.SetSprite1(RefColShield6, GetColourShield(6));
			PropEx.SetSprite2(RefColShield6, GetColourShield(6, 1));
			PropEx.SetSprite3(RefColShield6, GetColourShield(6, 1));
			PropEx.SetSprite4(RefColShield6, GetColourShield(6));
		}
		if (colourID == 7)
		{
			PropEx.SetSprite1(RefColShield7, GetColourShield(7, 2));
			PropEx.SetSprite2(RefColShield7, GetColourShield(7, 2));
			PropEx.SetSprite3(RefColShield7, GetColourShield(7, 2));
			PropEx.SetSprite4(RefColShield7, GetColourShield(7, 2));
		}
		else
		{
			PropEx.SetSprite1(RefColShield7, GetColourShield(7));
			PropEx.SetSprite2(RefColShield7, GetColourShield(7, 1));
			PropEx.SetSprite3(RefColShield7, GetColourShield(7, 1));
			PropEx.SetSprite4(RefColShield7, GetColourShield(7));
		}
		if (colourID == 8)
		{
			PropEx.SetSprite1(RefColShield8, GetColourShield(8, 2));
			PropEx.SetSprite2(RefColShield8, GetColourShield(8, 2));
			PropEx.SetSprite3(RefColShield8, GetColourShield(8, 2));
			PropEx.SetSprite4(RefColShield8, GetColourShield(8, 2));
		}
		else
		{
			PropEx.SetSprite1(RefColShield8, GetColourShield(8));
			PropEx.SetSprite2(RefColShield8, GetColourShield(8, 1));
			PropEx.SetSprite3(RefColShield8, GetColourShield(8, 1));
			PropEx.SetSprite4(RefColShield8, GetColourShield(8));
		}
	}

	private void LeaveLobby(bool doLeaveOnSteam = true)
	{
		Platform_Multiplayer.Instance.SetMemberReadyState(state: false);
		MainViewModel.Instance.StandaloneTitle = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 51);
		MainViewModel.Instance.Show_MPJoiningLobby = true;
		pendingMPHost = false;
		MainViewModel.Instance.Show_CreatingMPHost = false;
		currentLobby = null;
		MPHostLobbyname = "";
		MPMapChecked = false;
		MPMapValid = false;
		MPLocalReady = false;
		selectedLobby = null;
		ShowSharingCode = false;
		selectedMPHeader = null;
		delayedSendDataToLobby = DateTime.MinValue;
		lobbyChat.Clear();
		RefMP_ChatDisplay.Inlines.Clear();
		chatMessage = "";
		multiplayerMapRequestTime = DateTime.MinValue;
		if (doLeaveOnSteam)
		{
			Platform_Multiplayer.Instance.LeaveLobby();
		}
		lastAutoRefreshTime = DateTime.UtcNow;
		Platform_Multiplayer.Instance.GetLobbies(delegate
		{
			lobbies = Platform_Multiplayer.Instance.ReadLobbies();
			populateLobbyList();
			lastAutoRefreshTime = DateTime.UtcNow.AddSeconds(-28.0);
		});
	}

	private void TextInputFocus(object sender, DependencyPropertyChangedEventArgs e)
	{
		MainViewModel.Instance.SetNoesisKeyboardState((bool)e.NewValue);
	}

	private void FilterTextInputFocus(object sender, DependencyPropertyChangedEventArgs e)
	{
		MainViewModel.Instance.SetNoesisKeyboardState((bool)e.NewValue);
		if ((bool)e.NewValue)
		{
			MainViewModel.Instance.MultiplayerFilterLabelVis = Visibility.Hidden;
		}
		else if (RefMP_SearchFilter.Text.Length == 0)
		{
			MainViewModel.Instance.MultiplayerFilterLabelVis = Visibility.Visible;
		}
	}

	private void FilterTextChangedHandler(object sender, RoutedEventArgs e)
	{
		if (panelActive)
		{
			populateMapList();
			if (RefMP_SearchFilter.Text.Length == 0)
			{
				MainViewModel.Instance.MultiplayerFilterButtonVis = Visibility.Hidden;
			}
			else
			{
				MainViewModel.Instance.MultiplayerFilterButtonVis = Visibility.Visible;
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

	private void DetectChatEnter(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Return)
		{
			ButtonClicked("SendChat");
		}
	}

	private void EnterShareTextChangedHandler(object sender, RoutedEventArgs e)
	{
		if (!panelActive)
		{
			return;
		}
		if (RefMP_EnterShareCodeText.Text.Length < 3)
		{
			RefShareJoinButton.IsEnabled = false;
			return;
		}
		ulong num = Platform_Multiplayer.Instance.DecodeShareCode(RefMP_EnterShareCodeText.Text);
		if (num != 0)
		{
			LatestSharedCode = num;
			RefShareJoinButton.IsEnabled = true;
		}
		else
		{
			RefShareJoinButton.IsEnabled = false;
		}
	}

	private void LobbyMaxPlayersSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		if (panelActive)
		{
			int num = (int)RefLobbyMaxPlayersSlider.Value;
			MainViewModel.Instance.MPCreateMaxPlayers = num.ToString();
			PlayerCap = num;
			RefSetupMaxPlayersSlider.Value = num;
		}
	}

	private void SetupMaxPlayersSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
	{
		if (panelActive)
		{
			int num = (int)RefSetupMaxPlayersSlider.Value;
			MainViewModel.Instance.MPCreateMaxPlayers = num.ToString();
		}
	}
}
