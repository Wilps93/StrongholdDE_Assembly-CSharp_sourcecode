using System;
using Noesis;

namespace Stronghold1DE;

public class FRONT_EcoCampaign : UserControl
{
	private static string rolloverMessage = "";

	private static string rolloverMissionName = "";

	private static DateTime rolloverTime = DateTime.MinValue;

	private static bool rolloverShow = false;

	private static Button rolloverButton = null;

	public FRONT_EcoCampaign()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		NoesisUnity.LoadComponent(this, "Assets/GUI/XAMLResources/FRONT_EcoCampaign.xaml");
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
		{
			int num = int.Parse(text, Director.defaultCulture);
			int num2 = num + 32;
			int difficulty = 0;
			if (ConfigSettings.MapCompleted("mission" + num2, ref difficulty))
			{
				rolloverMessage = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 189);
				if (difficulty >= 0 && difficulty <= 3)
				{
					rolloverMessage += " : ";
					rolloverMessage += Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAINOPTIONS, 19 + difficulty);
				}
				rolloverShow = false;
				rolloverTime = DateTime.UtcNow.AddSeconds(1.0);
				rolloverButton = (Button)e.Source;
				rolloverMissionName = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MISSION_NAMES, num + 21);
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
