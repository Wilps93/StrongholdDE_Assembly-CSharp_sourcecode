using Noesis;

namespace Stronghold1DE;

public class HUD_Objectives : UserControl
{
	public Grid RefRoot;

	public Grid RefObjectiveTimer;

	public WGT_Objective[] RefWGTObjectives = new WGT_Objective[7];

	public HUD_Objectives()
	{
		InitializeComponent();
		MainViewModel.Instance.HUDObjectives = this;
		RefRoot = (Grid)FindName("LayoutRoot");
		RefObjectiveTimer = (Grid)FindName("ObjectiveTimer");
		RefWGTObjectives[0] = (WGT_Objective)FindName("WGT_Objective1");
		RefWGTObjectives[1] = (WGT_Objective)FindName("WGT_Objective2");
		RefWGTObjectives[2] = (WGT_Objective)FindName("WGT_Objective3");
		RefWGTObjectives[3] = (WGT_Objective)FindName("WGT_Objective4");
		RefWGTObjectives[4] = (WGT_Objective)FindName("WGT_Objective5");
		RefWGTObjectives[5] = (WGT_Objective)FindName("WGT_Objective6");
		RefWGTObjectives[6] = (WGT_Objective)FindName("WGT_Objective7");
	}

	public void SetSizeFromRows(int numRows)
	{
		RefRoot.Height = 200 - (7 - numRows) * 25;
	}

	private void InitializeComponent()
	{
		GUI.LoadComponent(this, "Assets/GUI/XAMLResources/HUD_Objectives.xaml");
	}
}
