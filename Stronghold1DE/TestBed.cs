using Noesis;

namespace Stronghold1DE;

public class TestBed : UserControl
{
	public TestBed()
	{
		base.DataContext = MainViewModel.Instance;
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		GUI.LoadComponent(this, "Assets/GUI/XAML/TestBed.xaml");
	}
}
