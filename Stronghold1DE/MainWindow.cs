using Noesis;

namespace Stronghold1DE;

public class MainWindow : UserControl
{
	public MainWindow()
	{
		base.DataContext = MainViewModel.Instance;
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		GUI.LoadComponent(this, "Assets/GUI/XAML/MainWindow.xaml");
	}
}
