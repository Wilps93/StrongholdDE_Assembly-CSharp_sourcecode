using System;
using Noesis;
using NoesisApp;

namespace Stronghold1DE;

public class STORY_Video : UserControl
{
	private MediaElement videoME;

	private MediaElement speechME;

	private TextBlock videoText;

	private Storyboard introAnimation;

	private Storyboard outtroAnimation;

	private Storyboard textInAnimation;

	private Storyboard textOutAnimation;

	public STORY_Video()
	{
		InitializeComponent();
		MainViewModel.Instance.StoryVideo = this;
		videoME = (MediaElement)FindName("VideoME");
		MainViewModel.Instance.StoryVideoME = videoME;
		videoME.MediaEnded += VideoME_Ended;
		speechME = (MediaElement)FindName("VideoSpeechME");
		MainViewModel.Instance.SpeechVideoME = speechME;
		videoText = (TextBlock)FindName("VideoText");
		MainViewModel.Instance.VideoText = videoText;
		introAnimation = (Storyboard)TryFindResource("Intro");
		MainViewModel.Instance.StoryVideoInAnimation = introAnimation;
		introAnimation.Completed += delegate
		{
			MainViewModel.Instance.AdvanceVideo();
		};
		outtroAnimation = (Storyboard)TryFindResource("Outtro");
		MainViewModel.Instance.StoryVideoOutAnimation = outtroAnimation;
		outtroAnimation.Completed += delegate
		{
			MainViewModel.Instance.AdvanceVideo();
		};
		textInAnimation = (Storyboard)TryFindResource("TextIn");
		MainViewModel.Instance.VideoTextInAnimation = textInAnimation;
		textOutAnimation = (Storyboard)TryFindResource("TextOut");
		MainViewModel.Instance.VideoTextOutAnimation = textOutAnimation;
	}

	private void InitializeComponent()
	{
		NoesisUnity.LoadComponent(this, "Assets/GUI/XAMLResources/STORY_Video.xaml");
	}

	private void VideoME_Ended(object sender, RoutedEventArgs args)
	{
		MainViewModel.Instance.AdvanceVideo();
	}

	public void Stop1(MediaElement thisME)
	{
		thisME.Stop();
	}

	public void Play1(MediaElement thisME)
	{
		thisME.Play();
	}

	public void Rewind1(MediaElement thisME)
	{
		thisME.Position = new TimeSpan(0L);
	}
}
