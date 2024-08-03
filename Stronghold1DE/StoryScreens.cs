using Noesis;

namespace Stronghold1DE;

public class StoryScreens : UserControl
{
	public StoryScreens()
	{
		base.DataContext = MainViewModel.Instance;
		InitializeComponent();
		MainViewModel.Instance.StoryAdvance();
	}

	private void InitializeComponent()
	{
		GUI.LoadComponent(this, "Assets/GUI/XAML/StoryScreens.xaml");
	}
}
