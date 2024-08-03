using Noesis;
using NoesisApp;
using UnityEngine;

namespace Stronghold1DE;

public class IngameUIScreens : UserControl
{
	public Button RefToggleScenarioButton;

	private Rectangle RefGuideRect;

	public Noesis.Grid RefHUD_ObjectivesPanel;

	public Noesis.Grid refHUD_Objectives;

	public Noesis.Grid refHUD_Goods;

	public Noesis.Grid refCompass;

	public Image refCompassImg;

	public MediaElement refMissionOverVideo;

	public Image refMPLogo;

	public IngameUIScreens()
	{
		base.DataContext = MainViewModel.Instance;
		InitializeComponent();
		MainViewModel.Instance.IngameUI = this;
		RefToggleScenarioButton = (Button)FindName("ButtonToggleScenerioEditor");
		RefGuideRect = (Rectangle)FindName("GuideRect");
		RefHUD_ObjectivesPanel = (Noesis.Grid)FindName("HUD_ObjectivesPanel");
		refHUD_Objectives = (Noesis.Grid)FindName("HUD_Objectives");
		refHUD_Goods = (Noesis.Grid)FindName("HUD_Goods");
		refCompass = (Noesis.Grid)FindName("Compass");
		refCompassImg = (Image)FindName("CompassImg");
		refMissionOverVideo = (MediaElement)FindName("MissionOverVideo");
		refMPLogo = (Image)FindName("MPLogo");
		refMPLogo.Source = MainViewModel.Instance.GetImage(Enums.eImages.IMAGE_FRONTEND_LOGO);
	}

	private void InitializeComponent()
	{
		Noesis.GUI.LoadComponent(this, "Assets/GUI/XAML/IngameUIScreens.xaml");
	}

	public void findUIlowerPoint()
	{
		if (!(RefGuideRect == null) && FatControler.instance.SHLowerUIPoint == 0f && RefGuideRect.View != null)
		{
			Point point = RefGuideRect.PointToScreen(new Point(-2f, -2f));
			FatControler.instance.SHLowerUIPoint = (float)Screen.height - point.Y;
		}
	}

	public void setRotationImage(Enums.Dircs rotation)
	{
		switch (rotation)
		{
		case Enums.Dircs.North:
			refCompassImg.Source = MainViewModel.Instance.GameSprites[93];
			break;
		case Enums.Dircs.East:
			refCompassImg.Source = MainViewModel.Instance.GameSprites[96];
			break;
		case Enums.Dircs.South:
			refCompassImg.Source = MainViewModel.Instance.GameSprites[95];
			break;
		case Enums.Dircs.West:
			refCompassImg.Source = MainViewModel.Instance.GameSprites[94];
			break;
		case Enums.Dircs.NE:
		case Enums.Dircs.SE:
		case Enums.Dircs.SW:
			break;
		}
	}
}
