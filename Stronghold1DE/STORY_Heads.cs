using System;
using Noesis;
using NoesisApp;

namespace Stronghold1DE;

public class STORY_Heads : UserControl
{
	private MediaElement firepitME;

	private MediaElement topHeadME;

	private MediaElement bottomHeadME;

	private MediaElement headSpeechME;

	private TextBlock headText1;

	private TextBlock headText2;

	private TextBlock nameText1;

	private TextBlock nameText2;

	private Storyboard introAnimation;

	private Storyboard introNoFireAnimation;

	private Storyboard outtroAnimation;

	private Storyboard head1InAnimation;

	private Storyboard head1SwapAnimation;

	private Storyboard head1OutAnimation;

	private Storyboard head2InAnimation;

	private Storyboard head2SwapAnimation;

	private Storyboard head2OutAnimation;

	private Storyboard text1InAnimation;

	private Storyboard text1OutAnimation;

	private Storyboard text2InAnimation;

	private Storyboard text2OutAnimation;

	private bool waitTopHead;

	private bool waitBottomHead;

	public STORY_Heads()
	{
		InitializeComponent();
		MainViewModel.Instance.StoryHeads = this;
		firepitME = (MediaElement)FindName("FirepitME");
		MainViewModel.Instance.StoryHeadsFirepitME = firepitME;
		firepitME.MediaEnded += FirepitME_Ended;
		MainViewModel.Instance.StoryFirePitGrid = (Grid)FindName("CenterImageGrid");
		topHeadME = (MediaElement)FindName("TopHeadME");
		MainViewModel.Instance.StoryHeadsTopHead = topHeadME;
		topHeadME.MediaEnded += TopHeadME_Ended;
		MainViewModel.Instance.StoryTopHeadGrid = (Grid)FindName("TopHeadMEGrid");
		bottomHeadME = (MediaElement)FindName("BottomHeadME");
		MainViewModel.Instance.StoryHeadsBottomHead = bottomHeadME;
		bottomHeadME.MediaEnded += BottomHeadME_Ended;
		MainViewModel.Instance.StoryBottomHeadGrid = (Grid)FindName("BottomHeadMEGrid");
		headSpeechME = (MediaElement)FindName("HeadSpeechME");
		MainViewModel.Instance.SpeechHeadsME = headSpeechME;
		headSpeechME.MediaEnded += headsStoryME_Ended;
		headText1 = (TextBlock)FindName("HeadText1");
		MainViewModel.Instance.Head1Text = headText1;
		headText2 = (TextBlock)FindName("HeadText2");
		MainViewModel.Instance.Head2Text = headText2;
		nameText1 = (TextBlock)FindName("NameText1");
		MainViewModel.Instance.Name1Text = nameText1;
		nameText2 = (TextBlock)FindName("NameText2");
		MainViewModel.Instance.Name2Text = nameText2;
		introAnimation = (Storyboard)TryFindResource("Intro");
		MainViewModel.Instance.HeadsFirepitInAnimation = introAnimation;
		introAnimation.Completed += delegate
		{
			MainViewModel.Instance.AdvanceTalkingHeads(1);
		};
		introNoFireAnimation = (Storyboard)TryFindResource("Intro2");
		MainViewModel.Instance.HeadsNoFirepitInAnimation = introNoFireAnimation;
		introNoFireAnimation.Completed += delegate
		{
			MainViewModel.Instance.AdvanceTalkingHeads(1);
		};
		outtroAnimation = (Storyboard)TryFindResource("Outtro");
		MainViewModel.Instance.HeadsFirepitOutAnimation = outtroAnimation;
		outtroAnimation.Completed += delegate
		{
			MainViewModel.Instance.AdvanceTalkingHeads(1);
		};
		head1InAnimation = (Storyboard)TryFindResource("Head1In");
		MainViewModel.Instance.Head1InAnimation = head1InAnimation;
		head1InAnimation.Completed += delegate
		{
			MainViewModel.Instance.AdvanceTalkingHeads(1);
		};
		head1SwapAnimation = (Storyboard)TryFindResource("Head1Swap");
		MainViewModel.Instance.Head1SwapAnimation = head1SwapAnimation;
		head1OutAnimation = (Storyboard)TryFindResource("Head1Out");
		MainViewModel.Instance.Head1OutAnimation = head1OutAnimation;
		head2InAnimation = (Storyboard)TryFindResource("Head2In");
		MainViewModel.Instance.Head2InAnimation = head2InAnimation;
		head2SwapAnimation = (Storyboard)TryFindResource("Head2Swap");
		MainViewModel.Instance.Head2SwapAnimation = head2SwapAnimation;
		head2OutAnimation = (Storyboard)TryFindResource("Head2Out");
		MainViewModel.Instance.Head2OutAnimation = head2OutAnimation;
		text1InAnimation = (Storyboard)TryFindResource("Text1In");
		MainViewModel.Instance.Text1InAnimation = text1InAnimation;
		text1OutAnimation = (Storyboard)TryFindResource("Text1Out");
		MainViewModel.Instance.Text1OutAnimation = text1OutAnimation;
		text2InAnimation = (Storyboard)TryFindResource("Text2In");
		MainViewModel.Instance.Text2InAnimation = text2InAnimation;
		text2OutAnimation = (Storyboard)TryFindResource("Text2Out");
		MainViewModel.Instance.Text2OutAnimation = text2OutAnimation;
	}

	private void InitializeComponent()
	{
		NoesisUnity.LoadComponent(this, "Assets/GUI/XAMLResources/STORY_Heads.xaml");
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

	public void SetVideoWaitTop(bool _waitHead)
	{
		waitTopHead = _waitHead;
		waitBottomHead = false;
	}

	public void SetVideoWaitBottom(bool _waitHead)
	{
		waitBottomHead = _waitHead;
		waitTopHead = false;
	}

	private void BottomHeadME_Ended(object sender, RoutedEventArgs args)
	{
		if (waitBottomHead)
		{
			waitBottomHead = false;
			MainViewModel.Instance.AdvanceTalkingHeads(1);
		}
	}

	private void FirepitME_Ended(object sender, RoutedEventArgs args)
	{
		firepitME.Stop();
		firepitME.Play();
	}

	private void TopHeadME_Ended(object sender, RoutedEventArgs args)
	{
		if (waitTopHead)
		{
			waitTopHead = false;
			MainViewModel.Instance.AdvanceTalkingHeads(1);
		}
	}

	private void headsStoryME_Ended(object sender, RoutedEventArgs args)
	{
		if (!waitTopHead && !waitBottomHead)
		{
			MainViewModel.Instance.AdvanceTalkingHeads(1);
		}
	}
}
