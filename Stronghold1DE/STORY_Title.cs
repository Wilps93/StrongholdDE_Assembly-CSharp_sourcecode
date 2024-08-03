using Noesis;

namespace Stronghold1DE;

public class STORY_Title : UserControl
{
	private Storyboard introAnimation;

	public STORY_Title()
	{
		InitializeComponent();
		introAnimation = (Storyboard)TryFindResource("Intro");
		MainViewModel.Instance.StoryTitleIntroAnimation = introAnimation;
		introAnimation.Completed += delegate
		{
			MainViewModel.Instance.EndAnimStoryAdvance("Title");
		};
	}

	private void InitializeComponent()
	{
		NoesisUnity.LoadComponent(this, "Assets/GUI/XAMLResources/STORY_Title.xaml");
	}
}
