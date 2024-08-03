using System;
using System.IO;
using Noesis;
using Steamworks;
using UnityEngine;
using Vuplex.WebView;

namespace Stronghold1DE;

public class HUD_Help : UserControl
{
	public static int from = 0;

	private static HUD_Help instance1 = null;

	private static HUD_Help instance2 = null;

	public Image RefMainHelpTexture;

	public Button RefButtonBack;

	public Button RefButtonHome;

	private IWebView webView;

	private Material webMaterial;

	private bool webBrowserOpen;

	public bool webBrowserLoaded;

	public static bool browserThumbHeld = false;

	public static bool mouseIsUpStroke = false;

	public static bool mouseIsDownStroke = false;

	private static bool wasPaused = false;

	private string openingPageURL = "";

	private bool canGoBackValue;

	private static bool[] buildingHelpChecked = new bool[120];

	private static bool[] buildingHelpExists = new bool[120];

	private static string[] in_building_help = new string[106]
	{
		null, "st02_house.html", "st02_house.html", "st03_woodcutters_hut.html", "st04_oxen_base.html", "st05_iron_mine.html", "st06_pitch_digger.html", "st07_hunters_hut.html", "st08_barracks.html", "st08_barracks.html",
		"st10_goods_yard.html", "st11_armoury.html", "st12_fletchers_workshop.html", "st13_blacksmiths_workshop.html", "st14_poleturners_workshop.html", "st15_armourers_workshop.html", "st16_tanners_workshop.html", "st17_bakers_workshop.html", "st18_brewers_workshop.html", "st19_granary.html",
		"st20_quarry.html", "st21_quarrypile.html", "st22_inn.html", "st23_healer.html", "st24_engineers_guild.html", "st25_tunnellers_guild.html", "st26_tradepost.html", null, "st28_oil_smelter.html", null,
		"st30_wheatfarm.html", "st31_hopsfarm.html", "st32_applefarm.html", "st33_cattlefarm.html", "st34_mill.html", "st35_stables.html", "st36_church.html", "st36_church.html", "st36_church.html", null,
		"st40_keep.html", "st40_keep.html", "st40_keep.html", "st40_keep.html", "st40_keep.html", "st60_gatehouse.html", "st60_gatehouse.html", "st47_gate_wood.html", "st48_gate_postern.html", "st49_drawbridge.html",
		"st50_tunnel_entrance.html", "st51_paradeground_oil.html", "st52_signpost.html", "st53_paradeground_eng.html", null, "st55_campground.html", "st56_paradeground_miss.html", "st57_paradeground_lgt.html", "st58_paradeground_hvy.html", "st59_paradeground_tun.html",
		"st60_gatehouse.html", "st61_tower.html", "st62_bad_things.html", "st62_bad_things.html", null, "st65_good_things.html", "st65_good_things.html", "st67_killing_pit.html", "st68_pitch_ditch.html", null,
		null, "st71_keepdoor_left.html", "st72_keepdoor_right.html", "st73_keepdoor.html", "st74_tower1.html", "st75_tower2.html", "st76_tower3.html", "st77_tower4.html", "st78_tower5.html", null,
		"st80_catapult.html", "st81_trebuchet.html", "st82_siege_tower.html", "st83_battering_ram.html", "st84_portable_shield.html", "st85_tunnel_construction.html", null, null, null, null,
		null, "st62_bad_things.html", "st62_bad_things.html", "st62_bad_things.html", "st62_bad_things.html", "st62_bad_things.html", "st62_bad_things.html", "st62_bad_things.html", "st62_bad_things.html", "st99_dog_cage.html",
		"st65_good_things.html", "st65_good_things.html", "st102_bee_hive.html", "st65_good_things.html", "st65_good_things.html", "st105_bear_cave.html"
	};

	public HUD_Help()
	{
		InitializeComponent();
		if (instance1 == null)
		{
			instance1 = this;
		}
		else if (instance2 == null)
		{
			instance2 = this;
		}
		RefMainHelpTexture = (Image)FindName("MainHelpTexture");
		RefButtonBack = (Button)FindName("ButtonBack");
		RefButtonHome = (Button)FindName("ButtonHome");
	}

	public static void OpenHelp(bool fromMenu, string url = "")
	{
		if (ConfigSettings.Settings_UseSteamOverlayForHelp)
		{
			url = url.Replace('/', '\\');
			SteamFriends.ActivateGameOverlayToWebPage(url);
			return;
		}
		MainViewModel.Instance.Show_HUD_Help = true;
		if (instance1.IsVisible)
		{
			MainViewModel.Instance.HUDHelp = instance1;
		}
		else if (instance2.IsVisible)
		{
			MainViewModel.Instance.HUDHelp = instance2;
		}
		if (fromMenu)
		{
			from = 0;
		}
		else
		{
			from = 1;
		}
		MainViewModel.Instance.HUDHelp.Init(url);
		if (fromMenu)
		{
			wasPaused = Director.instance.Paused;
			if (!wasPaused)
			{
				Director.instance.SetPausedState(state: true);
			}
		}
	}

	private async void Init(string url)
	{
		try
		{
			openingPageURL = url;
			RefButtonBack.Visibility = Visibility.Hidden;
			RefButtonHome.Visibility = Visibility.Hidden;
			webBrowserOpen = true;
			webView = Web.CreateWebView();
			int width = (int)(RefMainHelpTexture.Width * 2f);
			int height = (int)(RefMainHelpTexture.Height * 2f);
			await webView.Init(width, height);
			webView.LoadUrl(url);
			mouseIsUpStroke = false;
			mouseIsDownStroke = false;
			webBrowserLoaded = true;
			browserThumbHeld = false;
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
	}

	public void Update()
	{
		if (!webBrowserLoaded)
		{
			return;
		}
		bool flag = FatControler.MouseIsDownStroke;
		bool flag2 = FatControler.MouseIsUpStroke;
		TextureSource mainHelpImage = new TextureSource(webView.Texture);
		MainViewModel.Instance.MainHelpImage = mainHelpImage;
		Point briefingHelpMousePoint = FatControler.instance.BriefingHelpMousePoint;
		if ((briefingHelpMousePoint.X >= 0f && briefingHelpMousePoint.X < RefMainHelpTexture.Width && briefingHelpMousePoint.Y >= 0f && briefingHelpMousePoint.Y < RefMainHelpTexture.Height) || browserThumbHeld)
		{
			Vector2 normalizedPoint = new Vector2(briefingHelpMousePoint.X / RefMainHelpTexture.Width, 1f - briefingHelpMousePoint.Y / RefMainHelpTexture.Height);
			if (normalizedPoint.x < 0f)
			{
				normalizedPoint.x = 0f;
			}
			if (normalizedPoint.y < 0f)
			{
				normalizedPoint.y = 0f;
			}
			if (normalizedPoint.x > 1f)
			{
				normalizedPoint.x = 1f;
			}
			if (normalizedPoint.y > 1f)
			{
				normalizedPoint.y = 1f;
			}
			if (webView is IWithPointerDownAndUp withPointerDownAndUp && !webView.IsDisposed && webView.IsInitialized)
			{
				if (flag)
				{
					browserThumbHeld = true;
					withPointerDownAndUp.PointerDown(normalizedPoint);
				}
				if (flag2)
				{
					browserThumbHeld = false;
					withPointerDownAndUp.PointerUp(normalizedPoint);
				}
				(webView as IWithMovablePointer).MovePointer(normalizedPoint);
			}
		}
		mouseIsUpStroke = false;
		mouseIsDownStroke = false;
		if (canGoBack())
		{
			Button refButtonHome = RefButtonHome;
			Visibility visibility2 = (RefButtonBack.Visibility = Visibility.Visible);
			refButtonHome.Visibility = visibility2;
		}
		else
		{
			Button refButtonHome2 = RefButtonHome;
			Visibility visibility2 = (RefButtonBack.Visibility = Visibility.Hidden);
			refButtonHome2.Visibility = visibility2;
		}
	}

	public void MouseWheelScrolled(float delta)
	{
		Point briefingHelpMousePoint = FatControler.instance.BriefingHelpMousePoint;
		if (briefingHelpMousePoint.X >= 0f && briefingHelpMousePoint.X < RefMainHelpTexture.Width && briefingHelpMousePoint.Y >= 0f && briefingHelpMousePoint.Y < RefMainHelpTexture.Height)
		{
			if (delta > 0f)
			{
				webView.Scroll(0, -60);
			}
			else
			{
				webView.Scroll(0, 60);
			}
		}
	}

	public bool canGoBack()
	{
		canGoBackInternal();
		return canGoBackValue;
	}

	private async void canGoBackInternal()
	{
		if (webBrowserLoaded && webView != null && !webView.IsDisposed && webView.IsInitialized)
		{
			canGoBackValue = await webView.CanGoBack();
		}
		else
		{
			canGoBackValue = false;
		}
	}

	public void goBack()
	{
		if (webBrowserLoaded && webView != null && !webView.IsDisposed && webView.IsInitialized)
		{
			webView.GoBack();
		}
	}

	public void goHome()
	{
		if (webBrowserLoaded && webView != null && !webView.IsDisposed && webView.IsInitialized)
		{
			webView.LoadUrl(openingPageURL);
		}
	}

	private void InitializeComponent()
	{
		NoesisUnity.LoadComponent(this, "Assets/GUI/XAMLResources/HUD_Help.xaml");
	}

	public void Close()
	{
		try
		{
			if (webBrowserOpen)
			{
				webView.LoadUrl("about:blank");
				webView.Dispose();
				webView = null;
				webBrowserOpen = false;
				webBrowserLoaded = false;
			}
		}
		catch (Exception)
		{
		}
		MainViewModel.Instance.MainHelpImage = null;
		mouseIsUpStroke = false;
		mouseIsDownStroke = false;
		browserThumbHeld = false;
		MainViewModel.Instance.Show_HUD_Help = false;
		if (from == 0)
		{
			if (!wasPaused)
			{
				Director.instance.SetPausedState(state: false);
			}
			MainViewModel.Instance.HUDmain.InGameOptions(null, null);
		}
	}

	public static void OpenHelpForCurrentBuildingOrChimmp()
	{
		if (GameData.Instance.lastGameState.app_mode == 16 && doesBuildingHelpExist(GameData.Instance.lastGameState.in_structure_type))
		{
			OpenHelp(fromMenu: false, getBuildingHelpURL(GameData.Instance.lastGameState.in_structure_type));
		}
	}

	public static string getBuildingHelpURL(int buildingType)
	{
		if (doesBuildingHelpExist(buildingType))
		{
			return "file://" + Application.dataPath + "/StreamingAssets/Help/" + in_building_help[buildingType];
		}
		return null;
	}

	public static bool doesBuildingHelpExist(int buildingType)
	{
		if (buildingType < 0 || buildingType > 120)
		{
			return false;
		}
		if (buildingHelpChecked[buildingType])
		{
			return buildingHelpExists[buildingType];
		}
		buildingHelpChecked[buildingType] = true;
		if (in_building_help[buildingType] == null)
		{
			buildingHelpExists[buildingType] = false;
		}
		string path = Application.dataPath + "/StreamingAssets/Help/" + in_building_help[buildingType];
		buildingHelpExists[buildingType] = File.Exists(path);
		return buildingHelpExists[buildingType];
	}
}
