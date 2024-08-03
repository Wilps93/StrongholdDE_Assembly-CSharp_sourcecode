using System;
using Noesis;
using UnityEngine;

namespace Stronghold1DE;

public class STORY_Map : UserControl
{
	private Point MousePosition;

	private Storyboard mapAnimation;

	private Storyboard flagMoveAnimation;

	private Noesis.Grid mapGrid;

	private Rectangle RefMapRect;

	private static string rolloverMessage = "";

	private static DateTime rolloverTime = DateTime.MinValue;

	private static bool rolloverShow = false;

	private static int rolloverMissionID = -1;

	private static Button rolloverButton = null;

	private UIElement SelectedObject { get; set; }

	public STORY_Map()
	{
		InitializeComponent();
		MainViewModel.Instance.STORYMap = this;
		mapAnimation = (Storyboard)TryFindResource("MapAnim");
		MainViewModel.Instance.StoryMapAnimation = mapAnimation;
		flagMoveAnimation = (Storyboard)TryFindResource("FlagMove");
		MainViewModel.Instance.StoryFlagMoveAnimation = flagMoveAnimation;
		mapGrid = (Noesis.Grid)FindName("mapgrid");
		mapGrid.MouseMove += MapImage_MouseMove;
		mapGrid.MouseLeftButtonDown += MapImage_MouseLeftClick;
		RefMapRect = (Rectangle)FindName("MapRect");
	}

	public void MapImage_MouseMove(object sender, MouseEventArgs e)
	{
		SelectedObject = e.Source as UIElement;
		if (SelectedObject.GetType() == typeof(Rectangle) || SelectedObject.GetType() == typeof(Image))
		{
			MousePosition = e.GetPosition(SelectedObject);
			MainViewModel.Instance.PanelX = MousePosition.X;
			MainViewModel.Instance.PanelY = MousePosition.Y;
		}
		int county = StoryMap.Instance.lookUpCounty((int)MainViewModel.Instance.PanelX, (int)MainViewModel.Instance.PanelY);
		int mapOwner = StoryMap.Instance.GetMapOwner(county);
		switch (mapOwner)
		{
		case 1:
			MainViewModel.Instance.MousePosText = Translate.Instance.lookUpText("TEXT_MAP_SCREEN_002");
			break;
		case 2:
			MainViewModel.Instance.MousePosText = Translate.Instance.lookUpText("TEXT_MAP_SCREEN_003");
			break;
		case 3:
			MainViewModel.Instance.MousePosText = Translate.Instance.lookUpText("TEXT_MAP_SCREEN_004");
			break;
		case 4:
			MainViewModel.Instance.MousePosText = Translate.Instance.lookUpText("TEXT_MAP_SCREEN_005");
			break;
		case 5:
			MainViewModel.Instance.MousePosText = Translate.Instance.lookUpText("TEXT_MAP_SCREEN_006");
			break;
		case 6:
			MainViewModel.Instance.MousePosText = Translate.Instance.lookUpText("TEXT_MAP_SCREEN_007");
			break;
		default:
			MainViewModel.Instance.MousePosText = "";
			break;
		}
		MainViewModel.Instance.OpacityRat = 0.5f;
		if (mapOwner == 4)
		{
			MainViewModel.Instance.OpacityRat = 1f;
		}
		MainViewModel.Instance.OpacitySnake = 0.5f;
		if (mapOwner == 5)
		{
			MainViewModel.Instance.OpacitySnake = 1f;
		}
		MainViewModel.Instance.OpacityPig = 0.5f;
		if (mapOwner == 3)
		{
			MainViewModel.Instance.OpacityPig = 1f;
		}
		MainViewModel.Instance.OpacityWolf = 0.5f;
		if (mapOwner == 6)
		{
			MainViewModel.Instance.OpacityWolf = 1f;
		}
	}

	public void MapImage_MouseLeftClick(object sender, MouseEventArgs e)
	{
		MainViewModel.Instance.ClickStoryAdvance(sender);
	}

	private void OnMouseLeftButtonDown(object sender, MouseEventArgs e)
	{
		MainViewModel.Instance.ClickStoryAdvance(sender);
	}

	public void findMapTopPoint()
	{
		if (!(RefMapRect == null))
		{
			Point sHMapStartPoint = RefMapRect.PointToScreen(new Point(-2f, -2f));
			sHMapStartPoint.Y = (float)Screen.height - sHMapStartPoint.Y;
			FatControler.instance.SHMapStartPoint = sHMapStartPoint;
		}
	}

	private void InitializeComponent()
	{
		NoesisUnity.LoadComponent(this, "Assets/GUI/XAMLResources/STORY_Map.xaml");
	}

	protected override bool ConnectEvent(object source, string eventName, string handlerName)
	{
		if (eventName == "MouseEnter" && handlerName == "CampaignMenuCommandEnter")
		{
			if (source is Button)
			{
				((Button)source).MouseEnter += CampaignMenuCommandEnter;
			}
			return true;
		}
		if (eventName == "MouseLeave" && handlerName == "CampaignMenuCommandLeave")
		{
			if (source is Button)
			{
				((Button)source).MouseLeave += CampaignMenuCommandLeave;
			}
			return true;
		}
		return false;
	}

	private void CampaignMenuCommandEnter(object sender, MouseEventArgs e)
	{
		if (!(e.Source is Button))
		{
			return;
		}
		string text = (string)((Button)e.Source).CommandParameter;
		switch (text)
		{
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
		case "11":
		case "12":
		case "13":
		case "14":
		case "15":
		case "16":
		case "17":
		case "18":
		case "19":
		case "20":
		case "21":
		{
			int num = int.Parse(text, Director.defaultCulture);
			int difficulty = 0;
			if (ConfigSettings.MapCompleted("mission" + num, ref difficulty))
			{
				rolloverMessage = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 189);
				if (difficulty >= 0 && difficulty <= 3 && num > 3)
				{
					rolloverMessage += " : ";
					rolloverMessage += Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAINOPTIONS, 19 + difficulty);
				}
				rolloverShow = false;
				rolloverMissionID = num;
				rolloverTime = DateTime.UtcNow.AddSeconds(1.0);
				rolloverButton = (Button)e.Source;
			}
			break;
		}
		}
	}

	private void CampaignMenuCommandLeave(object sender, MouseEventArgs e)
	{
		if (e.Source is Button)
		{
			string text = (string)((Button)e.Source).CommandParameter;
			switch (text)
			{
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
			case "11":
			case "12":
			case "13":
			case "14":
			case "15":
			case "16":
			case "17":
			case "18":
			case "19":
			case "20":
			case "21":
				PropEx.SetTextCentre((Button)e.Source, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MISSION_NAMES, int.Parse(text, Director.defaultCulture)));
				rolloverMessage = "";
				break;
			}
		}
	}

	public void Update()
	{
		if (rolloverMessage.Length > 0 && DateTime.UtcNow > rolloverTime)
		{
			rolloverTime = DateTime.UtcNow.AddSeconds(1.0);
			rolloverShow = !rolloverShow;
			if (rolloverShow)
			{
				PropEx.SetTextCentre(rolloverButton, rolloverMessage);
			}
			else
			{
				PropEx.SetTextCentre(rolloverButton, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MISSION_NAMES, rolloverMissionID));
			}
		}
	}
}
