using System;
using Noesis;

namespace Stronghold1DE;

public class FRONT_Trail : UserControl
{
	private static string rolloverMessage = "";

	private static string rolloverMissionName = "";

	private static DateTime rolloverTime = DateTime.MinValue;

	private static bool rolloverShow = false;

	private static Button rolloverButton = null;

	public FRONT_Trail()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		NoesisUnity.LoadComponent(this, "Assets/GUI/XAMLResources/FRONT_Trail.xaml");
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
		{
			int num = int.Parse(text, Director.defaultCulture);
			int difficulty = 0;
			string mapname = "";
			switch (num)
			{
			case 1:
				mapname = "dunnottar";
				break;
			case 2:
				mapname = "warkworth";
				break;
			case 3:
				mapname = "pembroke";
				break;
			case 4:
				mapname = "warwick";
				break;
			case 5:
				mapname = "bodiam";
				break;
			case 6:
				mapname = "hastings";
				break;
			case 7:
				mapname = "chateau gaillard";
				break;
			case 8:
				mapname = "chateau de coucy";
				break;
			case 9:
				mapname = "marksburg";
				break;
			case 10:
				mapname = "heuneburg";
				break;
			}
			if (ConfigSettings.MapCompleted(mapname, ref difficulty))
			{
				rolloverMessage = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 189);
				rolloverShow = false;
				rolloverTime = DateTime.UtcNow.AddSeconds(1.0);
				rolloverButton = (Button)e.Source;
				rolloverMissionName = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_TRAIL_NAMES, num);
			}
			break;
		}
		}
	}

	private void CampaignMenuCommandLeave(object sender, MouseEventArgs e)
	{
		if (!(e.Source is Button))
		{
			return;
		}
		switch ((string)((Button)e.Source).CommandParameter)
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
			if (rolloverMessage.Length > 0)
			{
				PropEx.SetTextCentre((Button)e.Source, rolloverMissionName);
				rolloverMessage = "";
			}
			break;
		}
	}

	public static void Update()
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
				PropEx.SetTextCentre(rolloverButton, rolloverMissionName);
			}
		}
	}
}
