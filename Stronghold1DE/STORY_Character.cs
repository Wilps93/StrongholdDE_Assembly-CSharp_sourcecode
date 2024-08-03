using Noesis;
using NoesisApp;

namespace Stronghold1DE;

public class STORY_Character : UserControl
{
	private Storyboard characterAnimation;

	private MediaElement characterSpeechME;

	public STORY_Character()
	{
		InitializeComponent();
		characterSpeechME = (MediaElement)FindName("CharacterSpeechME");
		MainViewModel.Instance.SpeechCharacterME = characterSpeechME;
		characterSpeechME.MediaEnded += characterStoryME_Ended;
		characterAnimation = (Storyboard)TryFindResource("Character");
		MainViewModel.Instance.StoryCharacterAnimation = characterAnimation;
	}

	private void characterStoryME_Ended(object sender, RoutedEventArgs args)
	{
		MainViewModel.Instance.EndAnimStoryAdvance("Character");
	}

	private void InitializeComponent()
	{
		NoesisUnity.LoadComponent(this, "Assets/GUI/XAMLResources/STORY_Character.xaml");
	}
}
