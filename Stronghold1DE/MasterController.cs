using Noesis;

namespace Stronghold1DE;

public class MasterController : UserControl
{
	public MasterController()
	{
		base.DataContext = MainViewModel.INIT();
		InitializeComponent();
		MainViewModel.Instance.GlobalUIRoot = this;
	}

	private void InitializeComponent()
	{
		GUI.LoadComponent(this, "Assets/GUI/XAML/MasterController.xaml");
	}
}
