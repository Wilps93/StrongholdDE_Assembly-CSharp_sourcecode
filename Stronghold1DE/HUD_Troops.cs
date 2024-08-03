using Noesis;

namespace Stronghold1DE;

public class HUD_Troops : UserControl
{
	private Button RefUnitDisband;

	private Button RefUnitBuildCat;

	private Button RefUnitStop;

	private Button RefUnitBuildTreb;

	public Button RefUnitPatrol;

	public Button RefUnitPatrolActive;

	public bool PatrolShouldBeVisible;

	private Button RefUnitBuildTower;

	private Grid RefUnitAttackHere;

	private Button RefUnitTunnelHere;

	private Button RefUnitPourOil;

	private Button RefUnitBuildRam;

	private Button RefUnitBuild;

	private Grid RefUnitReload;

	private Button RefUnitbuildMantlet;

	private Grid RefUnitFireCow;

	private Button RefUnitBack;

	private Button RefSelArchers;

	private Button RefSelSpearmen;

	private Button RefSelMacemen;

	private Button RefSelXBowmen;

	private Button RefSelPikemen;

	private Button RefSelSwordsmen;

	private Button RefSelKnights;

	private Button RefSelEngineers;

	private Button RefSelMonks;

	private Button RefSelLaddermen;

	private Button RefSelTunnelers;

	private Button RefSelCatapults;

	private Button RefSelTrebuchets;

	private Button RefSelRams;

	private Button RefSelSiegeTowers;

	private Button RefSelMantlets;

	private Button RefSelMangonels;

	private Button RefSelBalistae;

	private TextBlock RefSelTroopNo1;

	private TextBlock RefSelTroopNo2;

	private TextBlock RefSelTroopNo3;

	private TextBlock RefSelTroopNo4;

	private TextBlock RefSelTroopNo5;

	private TextBlock RefSelTroopNo6;

	private TextBlock RefSelTroopNo7;

	private TextBlock RefSelTroopNo8;

	public TextBlock RefTroopsPanelRollover;

	public TextBlock RefTroopsPanelRollover2;

	private Button RefButtonTroopPanelPage1;

	private Button RefButtonTroopPanelPage2;

	private int[] SelectedChimpArray = new int[70];

	private int NoSelectedChimpTypes;

	private int currentPage;

	private TranslateTransform[] SelTroopPositions = new TranslateTransform[8]
	{
		new TranslateTransform(-181f, 0f),
		new TranslateTransform(-129f, 0f),
		new TranslateTransform(-77f, 0f),
		new TranslateTransform(-25f, 0f),
		new TranslateTransform(27f, 0f),
		new TranslateTransform(79f, 0f),
		new TranslateTransform(131f, 0f),
		new TranslateTransform(183f, 0f)
	};

	public HUD_Troops()
	{
		InitializeComponent();
		MainViewModel.Instance.HUDTroopPanel = this;
		RefUnitDisband = (Button)FindName("UnitDisband");
		RefUnitBuildCat = (Button)FindName("UnitBuildCat");
		RefUnitStop = (Button)FindName("UnitStop");
		RefUnitBuildTreb = (Button)FindName("UnitBuildTreb");
		RefUnitPatrol = (Button)FindName("UnitPatrol");
		RefUnitPatrolActive = (Button)FindName("UnitPatrolActive");
		RefUnitBuildTower = (Button)FindName("UnitBuildTower");
		RefUnitAttackHere = (Grid)FindName("UnitAttackHere");
		RefUnitTunnelHere = (Button)FindName("UnitTunnelHere");
		RefUnitPourOil = (Button)FindName("UnitPourOil");
		RefUnitBuildRam = (Button)FindName("UnitBuildRam");
		RefUnitBuild = (Button)FindName("UnitBuild");
		RefUnitReload = (Grid)FindName("UnitReload");
		RefUnitbuildMantlet = (Button)FindName("UnitbuildMantlet");
		RefUnitFireCow = (Grid)FindName("UnitFireCow");
		RefUnitBack = (Button)FindName("UnitBack");
		RefSelArchers = (Button)FindName("ArchersSelected");
		RefSelSpearmen = (Button)FindName("SpearmenSelected");
		RefSelMacemen = (Button)FindName("MacemenSelected");
		RefSelXBowmen = (Button)FindName("XBowmenSelected");
		RefSelPikemen = (Button)FindName("PikemenSelected");
		RefSelSwordsmen = (Button)FindName("SwordsmenSelected");
		RefSelKnights = (Button)FindName("KnightsSelected");
		RefSelEngineers = (Button)FindName("EngineersSelected");
		RefSelMonks = (Button)FindName("MonksSelected");
		RefSelLaddermen = (Button)FindName("LaddermenSelected");
		RefSelTunnelers = (Button)FindName("TunnelersSelected");
		RefSelCatapults = (Button)FindName("CatapultsSelected");
		RefSelTrebuchets = (Button)FindName("TrebuchetsSelected");
		RefSelRams = (Button)FindName("RamsSelected");
		RefSelSiegeTowers = (Button)FindName("SiegeTowersSelected");
		RefSelMantlets = (Button)FindName("MantletsSelected");
		RefSelMangonels = (Button)FindName("MangonelsSelected");
		RefSelBalistae = (Button)FindName("BalistaeSelected");
		RefButtonTroopPanelPage1 = (Button)FindName("ButtonTroopPanelPage1");
		RefButtonTroopPanelPage2 = (Button)FindName("ButtonTroopPanelPage2");
		RefSelTroopNo1 = (TextBlock)FindName("SelectedTroopCount1");
		RefSelTroopNo2 = (TextBlock)FindName("SelectedTroopCount2");
		RefSelTroopNo3 = (TextBlock)FindName("SelectedTroopCount3");
		RefSelTroopNo4 = (TextBlock)FindName("SelectedTroopCount4");
		RefSelTroopNo5 = (TextBlock)FindName("SelectedTroopCount5");
		RefSelTroopNo6 = (TextBlock)FindName("SelectedTroopCount6");
		RefSelTroopNo7 = (TextBlock)FindName("SelectedTroopCount7");
		RefSelTroopNo8 = (TextBlock)FindName("SelectedTroopCount8");
		RefTroopsPanelRollover = (TextBlock)FindName("TroopsPanelRollover");
		RefTroopsPanelRollover2 = (TextBlock)FindName("TroopsPanelRollover2");
		SelectedChimpArray[22] = 3;
		SelectedChimpArray[28] = 4;
		SelectedChimpArray[26] = 1;
		SelectedChimpArray[61] = 2;
		SelectedChimpArray[59] = 5;
		SelectedChimpArray[41] = 6;
		SelectedChimpArray[60] = 7;
		SelectedChimpArray[29] = 9;
		SelectedChimpArray[30] = 12;
		SelectedChimpArray[37] = 22;
		SelectedChimpArray[39] = 34;
		SelectedChimpArray[40] = 56;
	}

	private void InitializeComponent()
	{
		GUI.LoadComponent(this, "Assets/GUI/XAMLResources/HUD_Troops.xaml");
	}

	protected override bool ConnectEvent(object source, string eventName, string handlerName)
	{
		return false;
	}

	public void SelectedTroops(bool fromOpen = false)
	{
		if (fromOpen)
		{
			currentPage = 0;
		}
		SetupSelectedTroops();
		SetuptroopActionsUI(fromOpen);
	}

	private void CountSelectedChimpTypes()
	{
		SelectedChimpArray = EditorDirector.instance.getSelectedChimpTypes();
		NoSelectedChimpTypes = 0;
		for (int i = 0; i < 70; i++)
		{
			if (i != 55 && SelectedChimpArray[i] > 0)
			{
				NoSelectedChimpTypes++;
			}
		}
	}

	public void RemoveSelectedChimpTypes(Enums.eChimps type, int mode)
	{
		for (Enums.eChimps eChimps = Enums.eChimps.CHIMP_TYPE_NULL; eChimps < Enums.eChimps.CHIMP_NUM_TYPES; eChimps++)
		{
			switch (mode)
			{
			case 0:
				if (eChimps != type)
				{
					SelectedChimpArray[(int)eChimps] = 0;
				}
				break;
			case 1:
				if (eChimps == type)
				{
					SelectedChimpArray[(int)eChimps] = 0;
				}
				break;
			}
		}
	}

	public void TogglePages(string command)
	{
		if (command == "0" && currentPage != 0)
		{
			currentPage = 0;
			SetupSelectedTroops();
		}
		if (command == "1" && currentPage != 1)
		{
			currentPage = 1;
			SetupSelectedTroops();
		}
	}

	public void SetupSelectedTroops()
	{
		CountSelectedChimpTypes();
		HideAllSelectedTroops();
		HideAllSelectedTroopsNumbers();
		int num = 1;
		int num2 = -1;
		int num3 = 0;
		for (int i = 0; i < 70; i++)
		{
			if (SelectedChimpArray[i] > 0 && i != 55)
			{
				num3++;
				if (num3 == 9)
				{
					num2 = i;
					num = 2;
				}
			}
		}
		if (currentPage == 1 && num < 2)
		{
			currentPage = 0;
		}
		int num4 = 0;
		if (currentPage == 1)
		{
			num4 = num2;
		}
		if (num > 1)
		{
			if (currentPage == 0)
			{
				PropEx.SetButtonVisibility(RefButtonTroopPanelPage1, Visibility.Visible);
				PropEx.SetButtonVisibility(RefButtonTroopPanelPage2, Visibility.Hidden);
			}
			else if (currentPage == 1)
			{
				PropEx.SetButtonVisibility(RefButtonTroopPanelPage1, Visibility.Hidden);
				PropEx.SetButtonVisibility(RefButtonTroopPanelPage2, Visibility.Visible);
			}
		}
		else
		{
			PropEx.SetButtonVisibility(RefButtonTroopPanelPage1, Visibility.Hidden);
			PropEx.SetButtonVisibility(RefButtonTroopPanelPage2, Visibility.Hidden);
		}
		int num5 = 0;
		for (int j = num4; j < 70; j++)
		{
			if (SelectedChimpArray[j] > 0 && j != 55)
			{
				SelTroopPositions[num5].Y = SetSelectedTroopVisible(j);
				SetSelectedTroopPosition(j, num5);
				ShowSelectedTroopsNumber(num5, SelectedChimpArray[j]);
				if (++num5 >= 8)
				{
					break;
				}
			}
		}
	}

	public void SetSelectedTroopPosition(int type, int slot)
	{
		switch (type)
		{
		case 22:
			RefSelArchers.RenderTransform = SelTroopPositions[slot];
			break;
		case 24:
			RefSelSpearmen.RenderTransform = SelTroopPositions[slot];
			break;
		case 26:
			RefSelMacemen.RenderTransform = SelTroopPositions[slot];
			break;
		case 23:
			RefSelXBowmen.RenderTransform = SelTroopPositions[slot];
			break;
		case 25:
			RefSelPikemen.RenderTransform = SelTroopPositions[slot];
			break;
		case 27:
			RefSelSwordsmen.RenderTransform = SelTroopPositions[slot];
			break;
		case 28:
			RefSelKnights.RenderTransform = SelTroopPositions[slot];
			break;
		case 30:
			RefSelEngineers.RenderTransform = SelTroopPositions[slot];
			break;
		case 37:
			RefSelMonks.RenderTransform = SelTroopPositions[slot];
			break;
		case 29:
			RefSelLaddermen.RenderTransform = SelTroopPositions[slot];
			break;
		case 5:
			RefSelTunnelers.RenderTransform = SelTroopPositions[slot];
			break;
		case 39:
			RefSelCatapults.RenderTransform = SelTroopPositions[slot];
			break;
		case 40:
			RefSelTrebuchets.RenderTransform = SelTroopPositions[slot];
			break;
		case 59:
			RefSelRams.RenderTransform = SelTroopPositions[slot];
			break;
		case 58:
			RefSelSiegeTowers.RenderTransform = SelTroopPositions[slot];
			break;
		case 60:
			RefSelMantlets.RenderTransform = SelTroopPositions[slot];
			break;
		case 41:
			RefSelMangonels.RenderTransform = SelTroopPositions[slot];
			break;
		case 61:
			RefSelBalistae.RenderTransform = SelTroopPositions[slot];
			break;
		}
	}

	public int SetSelectedTroopVisible(int type)
	{
		switch (type)
		{
		case 22:
			RefSelArchers.Visibility = Visibility.Visible;
			break;
		case 24:
			RefSelSpearmen.Visibility = Visibility.Visible;
			break;
		case 26:
			RefSelMacemen.Visibility = Visibility.Visible;
			break;
		case 23:
			RefSelXBowmen.Visibility = Visibility.Visible;
			break;
		case 25:
			RefSelPikemen.Visibility = Visibility.Visible;
			break;
		case 27:
			RefSelSwordsmen.Visibility = Visibility.Visible;
			break;
		case 28:
			RefSelKnights.Visibility = Visibility.Visible;
			break;
		case 30:
			RefSelEngineers.Visibility = Visibility.Visible;
			break;
		case 37:
			RefSelMonks.Visibility = Visibility.Visible;
			break;
		case 29:
			RefSelLaddermen.Visibility = Visibility.Visible;
			break;
		case 5:
			RefSelTunnelers.Visibility = Visibility.Visible;
			break;
		case 39:
			RefSelCatapults.Visibility = Visibility.Visible;
			break;
		case 40:
			RefSelTrebuchets.Visibility = Visibility.Visible;
			break;
		case 59:
			RefSelRams.Visibility = Visibility.Visible;
			break;
		case 58:
			RefSelSiegeTowers.Visibility = Visibility.Visible;
			break;
		case 60:
			RefSelMantlets.Visibility = Visibility.Visible;
			break;
		case 41:
			RefSelMangonels.Visibility = Visibility.Visible;
			break;
		case 61:
			RefSelBalistae.Visibility = Visibility.Visible;
			break;
		}
		return -13;
	}

	public void HideAllSelectedTroops()
	{
		RefSelArchers.Visibility = Visibility.Hidden;
		RefSelSpearmen.Visibility = Visibility.Hidden;
		RefSelMacemen.Visibility = Visibility.Hidden;
		RefSelXBowmen.Visibility = Visibility.Hidden;
		RefSelPikemen.Visibility = Visibility.Hidden;
		RefSelSwordsmen.Visibility = Visibility.Hidden;
		RefSelKnights.Visibility = Visibility.Hidden;
		RefSelEngineers.Visibility = Visibility.Hidden;
		RefSelMonks.Visibility = Visibility.Hidden;
		RefSelLaddermen.Visibility = Visibility.Hidden;
		RefSelTunnelers.Visibility = Visibility.Hidden;
		RefSelCatapults.Visibility = Visibility.Hidden;
		RefSelTrebuchets.Visibility = Visibility.Hidden;
		RefSelRams.Visibility = Visibility.Hidden;
		RefSelSiegeTowers.Visibility = Visibility.Hidden;
		RefSelMantlets.Visibility = Visibility.Hidden;
		RefSelMangonels.Visibility = Visibility.Hidden;
		RefSelBalistae.Visibility = Visibility.Hidden;
	}

	public void ShowSelectedTroopsNumber(int slot, int value)
	{
		switch (slot)
		{
		case 0:
			RefSelTroopNo1.Visibility = Visibility.Visible;
			RefSelTroopNo1.Text = value.ToString();
			break;
		case 1:
			RefSelTroopNo2.Visibility = Visibility.Visible;
			RefSelTroopNo2.Text = value.ToString();
			break;
		case 2:
			RefSelTroopNo3.Visibility = Visibility.Visible;
			RefSelTroopNo3.Text = value.ToString();
			break;
		case 3:
			RefSelTroopNo4.Visibility = Visibility.Visible;
			RefSelTroopNo4.Text = value.ToString();
			break;
		case 4:
			RefSelTroopNo5.Visibility = Visibility.Visible;
			RefSelTroopNo5.Text = value.ToString();
			break;
		case 5:
			RefSelTroopNo6.Visibility = Visibility.Visible;
			RefSelTroopNo6.Text = value.ToString();
			break;
		case 6:
			RefSelTroopNo7.Visibility = Visibility.Visible;
			RefSelTroopNo7.Text = value.ToString();
			break;
		case 7:
			RefSelTroopNo8.Visibility = Visibility.Visible;
			RefSelTroopNo8.Text = value.ToString();
			break;
		}
	}

	public void HideAllSelectedTroopsNumbers()
	{
		RefSelTroopNo1.Visibility = Visibility.Hidden;
		RefSelTroopNo2.Visibility = Visibility.Hidden;
		RefSelTroopNo3.Visibility = Visibility.Hidden;
		RefSelTroopNo4.Visibility = Visibility.Hidden;
		RefSelTroopNo5.Visibility = Visibility.Hidden;
		RefSelTroopNo6.Visibility = Visibility.Hidden;
		RefSelTroopNo7.Visibility = Visibility.Hidden;
		RefSelTroopNo8.Visibility = Visibility.Hidden;
	}

	public void SetuptroopActionsUI(bool fromInitialOpening = false)
	{
		if (GameData.Instance.lastGameState != null && GameData.Instance.lastGameState.troops_show_disband > 0)
		{
			RefUnitDisband.Visibility = Visibility.Visible;
		}
		else
		{
			RefUnitDisband.Visibility = Visibility.Hidden;
		}
		RefUnitStop.Visibility = Visibility.Visible;
		RefUnitPatrol.Visibility = Visibility.Visible;
		RefUnitPatrolActive.Visibility = Visibility.Hidden;
		PatrolShouldBeVisible = true;
		RefUnitReload = (Grid)FindName("UnitReload");
		RefUnitFireCow = (Grid)FindName("UnitFireCow");
		if (MainViewModel.Instance.MEMode == 0)
		{
			RefUnitAttackHere.Visibility = Visibility.Visible;
			RefUnitTunnelHere.Visibility = Visibility.Hidden;
			RefUnitPourOil.Visibility = Visibility.Hidden;
			RefUnitBuild.Visibility = Visibility.Hidden;
			RefUnitFireCow.Visibility = Visibility.Hidden;
			RefUnitReload.Visibility = Visibility.Hidden;
			ShownEngineerbuildUI(Visibility.Hidden);
			return;
		}
		ShownAttackHereOrder();
		ShownBuildSiegeKitOrder();
		ShowAmmoOrders();
		if (fromInitialOpening)
		{
			ShownEngineerbuildUI(Visibility.Hidden);
			return;
		}
		if (RefUnitBack.Visibility == Visibility.Visible)
		{
			RefUnitDisband.Visibility = Visibility.Hidden;
			RefUnitStop.Visibility = Visibility.Hidden;
			RefUnitPatrol.Visibility = Visibility.Hidden;
			RefUnitPatrolActive.Visibility = Visibility.Hidden;
			PatrolShouldBeVisible = false;
			RefUnitAttackHere.Visibility = Visibility.Hidden;
			RefUnitTunnelHere.Visibility = Visibility.Hidden;
			RefUnitPourOil.Visibility = Visibility.Hidden;
			RefUnitBuild.Visibility = Visibility.Hidden;
			RefUnitFireCow.Visibility = Visibility.Hidden;
			RefUnitReload.Visibility = Visibility.Hidden;
		}
		ShownEngineerbuildUI(RefUnitBack.Visibility);
	}

	public void SelectedEngiBuild(bool state)
	{
		if (state)
		{
			RefUnitDisband.Visibility = Visibility.Hidden;
			RefUnitStop.Visibility = Visibility.Hidden;
			RefUnitPatrol.Visibility = Visibility.Hidden;
			RefUnitPatrolActive.Visibility = Visibility.Hidden;
			PatrolShouldBeVisible = false;
			RefUnitAttackHere.Visibility = Visibility.Hidden;
			RefUnitTunnelHere.Visibility = Visibility.Hidden;
			RefUnitPourOil.Visibility = Visibility.Hidden;
			RefUnitBuild.Visibility = Visibility.Hidden;
			RefUnitFireCow.Visibility = Visibility.Hidden;
			RefUnitReload.Visibility = Visibility.Hidden;
			ShownEngineerbuildUI(Visibility.Visible);
		}
		else
		{
			SetuptroopActionsUI(fromInitialOpening: true);
		}
	}

	private void ShownAttackHereOrder()
	{
		if (GameData.Instance.lastGameState != null && GameData.Instance.lastGameState.troops_show_attack_here_and_type > 0)
		{
			if (GameData.Instance.lastGameState.troops_show_attack_here_and_type == 1)
			{
				RefUnitAttackHere.Visibility = Visibility.Hidden;
				RefUnitPourOil.Visibility = Visibility.Visible;
				RefUnitTunnelHere.Visibility = Visibility.Hidden;
			}
			else if (GameData.Instance.lastGameState.troops_show_attack_here_and_type == 2)
			{
				RefUnitAttackHere.Visibility = Visibility.Hidden;
				RefUnitPourOil.Visibility = Visibility.Hidden;
				RefUnitTunnelHere.Visibility = Visibility.Visible;
			}
			else
			{
				RefUnitAttackHere.Visibility = Visibility.Visible;
				RefUnitPourOil.Visibility = Visibility.Hidden;
				RefUnitTunnelHere.Visibility = Visibility.Hidden;
			}
		}
		else
		{
			RefUnitAttackHere.Visibility = Visibility.Hidden;
			RefUnitPourOil.Visibility = Visibility.Hidden;
			RefUnitTunnelHere.Visibility = Visibility.Hidden;
		}
	}

	private void ShownBuildSiegeKitOrder()
	{
		if (GameData.Instance.lastGameState != null && GameData.Instance.lastGameState.troops_show_build_menu > 0)
		{
			RefUnitBuild.Visibility = Visibility.Visible;
		}
		else
		{
			RefUnitBuild.Visibility = Visibility.Hidden;
		}
	}

	public void ShowAmmoOrders()
	{
		if (GameData.Instance.lastGameState != null && GameData.Instance.lastGameState.troops_show_launch_cow_and_num_cows > 0)
		{
			RefUnitFireCow.Visibility = Visibility.Visible;
			MainViewModel.Instance.CowAmmoLeft = GameData.Instance.lastGameState.troops_show_launch_cow_and_num_cows.ToString();
		}
		else
		{
			RefUnitFireCow.Visibility = Visibility.Hidden;
		}
		if (GameData.Instance.lastGameState.troops_show_attack_here_and_type == 3)
		{
			MainViewModel.Instance.AmmoLeft = GameData.Instance.lastGameState.troops_show_attack_here_number_rocks.ToString();
		}
		else
		{
			MainViewModel.Instance.AmmoLeft = "";
		}
		if (GameData.Instance.lastGameState != null && GameData.Instance.lastGameState.troops_show_get_ammo > 0)
		{
			RefUnitReload.Visibility = Visibility.Visible;
		}
		else
		{
			RefUnitReload.Visibility = Visibility.Hidden;
		}
	}

	private void ShownEngineerbuildUI(Visibility thisVisibility)
	{
		if (GameData.Instance.lastGameState != null)
		{
			if (GameData.Instance.lastGameState.troops_show_make_catapult > 0)
			{
				RefUnitBuildCat.Visibility = thisVisibility;
			}
			else
			{
				RefUnitBuildCat.Visibility = Visibility.Hidden;
			}
			if (GameData.Instance.lastGameState.troops_show_make_trebuchet > 0)
			{
				RefUnitBuildTreb.Visibility = thisVisibility;
			}
			else
			{
				RefUnitBuildTreb.Visibility = Visibility.Hidden;
			}
			if (GameData.Instance.lastGameState.troops_show_make_siege_tower > 0)
			{
				RefUnitBuildTower.Visibility = thisVisibility;
			}
			else
			{
				RefUnitBuildTower.Visibility = Visibility.Hidden;
			}
			if (GameData.Instance.lastGameState.troops_show_battering_ram > 0)
			{
				RefUnitBuildRam.Visibility = thisVisibility;
			}
			else
			{
				RefUnitBuildRam.Visibility = Visibility.Hidden;
			}
			if (GameData.Instance.lastGameState.troops_show_portable_shield > 0)
			{
				RefUnitbuildMantlet.Visibility = thisVisibility;
			}
			else
			{
				RefUnitbuildMantlet.Visibility = Visibility.Hidden;
			}
		}
		else
		{
			RefUnitBuildCat.Visibility = Visibility.Hidden;
			RefUnitBuildTreb.Visibility = Visibility.Hidden;
			RefUnitBuildTower.Visibility = Visibility.Hidden;
			RefUnitBuildRam.Visibility = Visibility.Hidden;
			RefUnitbuildMantlet.Visibility = Visibility.Hidden;
		}
		RefUnitBack.Visibility = thisVisibility;
	}
}
