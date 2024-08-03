using System;
using Noesis;

namespace Stronghold1DE;

public class HUD_ConfirmationPopup : UserControl
{
	private WGT_Heading RefHeading;

	private Grid RefYesNo;

	private Grid RefOK;

	private static HUD_ConfirmationPopup instance1 = null;

	private static HUD_ConfirmationPopup instance2 = null;

	public static int ConfirmationWidth = 450;

	public static int ConfirmationHeight = 170;

	private Action yesAction;

	private Action noAction;

	public HUD_ConfirmationPopup()
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
		RefHeading = (WGT_Heading)FindName("ConfirmationHeader");
		RefYesNo = (Grid)FindName("YesNo");
		RefOK = (Grid)FindName("OK");
		if (FatControler.hungarian)
		{
			RefHeading.RefHeadingTextBlock.FontSize = 30f;
		}
	}

	private void InitializeComponent()
	{
		NoesisUnity.LoadComponent(this, "Assets/GUI/XAMLResources/HUD_ConfirmationPopup.xaml");
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

	public static void ShowConfirmation(string title, Action _yesAction, Action _noAction)
	{
		MainViewModel.Instance.Show_HUD_Confirmation = true;
		if (instance1.IsVisible)
		{
			MainViewModel.Instance.HUDConfirmationPopup = instance1;
		}
		else if (instance2.IsVisible)
		{
			MainViewModel.Instance.HUDConfirmationPopup = instance2;
		}
		MainViewModel.Instance.ConfirmationPanelHeight = "170";
		MainViewModel.Instance.ConfirmationPanelWidth = "450";
		MainViewModel.Instance.ConfirmationPanelWidth2 = "420";
		MainViewModel.Instance.ConfirmationMessage = "";
		ConfirmationHeight = 170;
		ConfirmationWidth = 450;
		MainViewModel.Instance.ConfirmationPanelWidthView = "486";
		MainViewModel.Instance.ConfirmationPanelHeightView = "206";
		MainViewModel.Instance.HUDConfirmationPopup.RefYesNo.Visibility = Visibility.Visible;
		MainViewModel.Instance.HUDConfirmationPopup.RefOK.Visibility = Visibility.Hidden;
		MainViewModel.Instance.HUDConfirmationPopup.yesAction = _yesAction;
		MainViewModel.Instance.HUDConfirmationPopup.noAction = _noAction;
		MainViewModel.Instance.HUDConfirmationPopup.RefHeading.HeadingText = title;
		MainViewModel.Instance.FrontEndMenu.UpdateFrontMenuPopupScale();
	}

	public static void ShowOK(string title, Action _yesAction)
	{
		MainViewModel.Instance.Show_HUD_Confirmation = true;
		if (instance1.IsVisible)
		{
			MainViewModel.Instance.HUDConfirmationPopup = instance1;
		}
		else if (instance2.IsVisible)
		{
			MainViewModel.Instance.HUDConfirmationPopup = instance2;
		}
		MainViewModel.Instance.ConfirmationPanelHeight = "170";
		MainViewModel.Instance.ConfirmationPanelWidth = "450";
		MainViewModel.Instance.ConfirmationPanelWidth2 = "420";
		MainViewModel.Instance.ConfirmationMessage = "";
		ConfirmationWidth = 450;
		ConfirmationHeight = 170;
		MainViewModel.Instance.ConfirmationPanelWidthView = "486";
		MainViewModel.Instance.ConfirmationPanelHeightView = "206";
		MainViewModel.Instance.HUDConfirmationPopup.RefYesNo.Visibility = Visibility.Hidden;
		MainViewModel.Instance.HUDConfirmationPopup.RefOK.Visibility = Visibility.Visible;
		MainViewModel.Instance.HUDConfirmationPopup.yesAction = _yesAction;
		MainViewModel.Instance.HUDConfirmationPopup.RefHeading.HeadingText = title;
		MainViewModel.Instance.FrontEndMenu.UpdateFrontMenuPopupScale();
	}

	public static void ShowConfirmationMessage(string title, Action _yesAction, Action _noAction, string message)
	{
		MainViewModel.Instance.Show_HUD_Confirmation = true;
		if (instance1.IsVisible)
		{
			MainViewModel.Instance.HUDConfirmationPopup = instance1;
		}
		else if (instance2.IsVisible)
		{
			MainViewModel.Instance.HUDConfirmationPopup = instance2;
		}
		MainViewModel.Instance.ConfirmationPanelHeight = "270";
		MainViewModel.Instance.ConfirmationPanelWidth = "650";
		MainViewModel.Instance.ConfirmationPanelWidth2 = "620";
		MainViewModel.Instance.ConfirmationMessage = message;
		ConfirmationHeight = 270;
		ConfirmationWidth = 650;
		MainViewModel.Instance.ConfirmationPanelWidthView = "702";
		MainViewModel.Instance.ConfirmationPanelHeightView = "327";
		MainViewModel.Instance.HUDConfirmationPopup.RefYesNo.Visibility = Visibility.Visible;
		MainViewModel.Instance.HUDConfirmationPopup.RefOK.Visibility = Visibility.Hidden;
		MainViewModel.Instance.HUDConfirmationPopup.yesAction = _yesAction;
		MainViewModel.Instance.HUDConfirmationPopup.noAction = _noAction;
		MainViewModel.Instance.HUDConfirmationPopup.RefHeading.HeadingText = title;
		MainViewModel.Instance.FrontEndMenu.UpdateFrontMenuPopupScale();
	}

	public void ConfirmationClicked(int mode)
	{
		MainViewModel.Instance.Show_HUD_Confirmation = false;
		switch (mode)
		{
		case -1:
		case 1:
			if (yesAction != null)
			{
				yesAction();
			}
			break;
		case 2:
			if (noAction != null)
			{
				noAction();
			}
			break;
		}
	}
}
