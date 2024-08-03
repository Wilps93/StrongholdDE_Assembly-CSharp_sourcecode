using Noesis;
using NoesisApp;

namespace Stronghold1DE;

public class STORY_Narrative : UserControl
{
	private Storyboard narrationAnimation;

	private MediaElement narractiveSpeechME;

	public static bool TriggerEndFromScroller;

	public STORY_Narrative()
	{
		InitializeComponent();
		narractiveSpeechME = (MediaElement)FindName("NarrativeSpeechME");
		MainViewModel.Instance.SpeechNarrativeME = narractiveSpeechME;
		narractiveSpeechME.MediaEnded += narrativeStoryME_Ended;
		narrationAnimation = (Storyboard)TryFindResource("Narration");
		MainViewModel.Instance.StoryNarrativeAnimation = narrationAnimation;
		narrationAnimation.Completed += narrativeStoryScrollME_Ended;
	}

	private void narrativeStoryME_Ended(object sender, RoutedEventArgs args)
	{
		Director.instance.EndAnimStoryAdvanceDelayed();
	}

	private void narrativeStoryScrollME_Ended(object sender, EventArgs args)
	{
		if (TriggerEndFromScroller)
		{
			TriggerEndFromScroller = false;
			Director.instance.EndAnimStoryAdvanceDelayed();
		}
	}

	private void InitializeComponent()
	{
		NoesisUnity.LoadComponent(this, "Assets/GUI/XAMLResources/STORY_Narrative.xaml");
	}
}
