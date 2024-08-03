using System;
using Noesis;
using NoesisApp;

namespace Stronghold1DE;

public class IntroSequence : UserControl
{
	private Image refLogo;

	private Image refPartnerLogo;

	private Image refLogoMain;

	private TextBlock refPresents;

	private MediaElement RefIntroVideo;

	private Storyboard RefFadeInLogos;

	private Storyboard RefSubtitlesStory;

	private TextBlock RefSubtitles;

	private Button RefSubtitlesButton;

	private TextBox RefEnterYourNameTB;

	private int stage;

	private bool binkLoaded;

	public IntroSequence()
	{
		base.DataContext = MainViewModel.Instance;
		MainViewModel.Instance.Intro_Sequence = this;
		InitializeComponent();
		refLogo = (Image)FindName("Logo");
		refPartnerLogo = (Image)FindName("PartnerLogo");
		refLogoMain = (Image)FindName("LogoMain");
		refLogoMain.Source = MainViewModel.Instance.GetImage(Enums.eImages.IMAGE_FRONTEND_LOGO);
		MainViewModel.Instance.EnterYourName = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 48);
		refPresents = (TextBlock)FindName("Presents");
		RefFadeInLogos = (Storyboard)TryFindResource("FadeInLogos");
		if (FatControler.french)
		{
			RefSubtitlesStory = (Storyboard)TryFindResource("SubtitlesStoryFR");
		}
		else if (FatControler.german)
		{
			RefSubtitlesStory = (Storyboard)TryFindResource("SubtitlesStoryDE");
		}
		else if (FatControler.spanish)
		{
			RefSubtitlesStory = (Storyboard)TryFindResource("SubtitlesStoryES");
		}
		else if (FatControler.italian)
		{
			RefSubtitlesStory = (Storyboard)TryFindResource("SubtitlesStoryIT");
		}
		else if (FatControler.polish)
		{
			RefSubtitlesStory = (Storyboard)TryFindResource("SubtitlesStoryPL");
		}
		else if (FatControler.russian)
		{
			RefSubtitlesStory = (Storyboard)TryFindResource("SubtitlesStoryRU");
		}
		else if (FatControler.portuguese)
		{
			RefSubtitlesStory = (Storyboard)TryFindResource("SubtitlesStoryPT");
		}
		else
		{
			RefSubtitlesStory = (Storyboard)TryFindResource("SubtitlesStory");
		}
		RefSubtitles = (TextBlock)FindName("Subtitles");
		RefSubtitlesButton = (Button)FindName("SubtitlesButton");
		RefIntroVideo = (MediaElement)FindName("IntroVideo");
		RefIntroVideo.MediaEnded += IntroVideo_Ended;
		RefIntroVideo.MediaOpened += IntroVideo_Opened;
		RefIntroVideo.Visibility = Visibility.Hidden;
		RefIntroVideo.Source = new Uri("Assets/GUI/Video/intro.webm", UriKind.Relative);
		RefEnterYourNameTB = (TextBox)FindName("EnterYourNameTB");
		RefEnterYourNameTB.IsKeyboardFocusedChanged += TextInputFocus;
		MainViewModel.Instance.EnterYourNameVis = false;
	}

	public void Init()
	{
		MainViewModel.Instance.Show_IntroSequence = true;
	}

	private void StartVideo()
	{
		RefFadeInLogos.Stop();
		refLogo.Visibility = Visibility.Hidden;
		refPresents.Visibility = Visibility.Hidden;
		refPartnerLogo.Visibility = Visibility.Hidden;
		stage++;
		RefIntroVideo.Visibility = Visibility.Visible;
		RefIntroVideo.Volume = ConfigSettings.Settings_SpeechVolume * ConfigSettings.Settings_MasterVolume;
		RefIntroVideo.IsMuted = false;
		RefIntroVideo.Play();
		switch (Translate.Instance.lookUpText(Enums.eTextSections.TEXT_LANGUAGE, 1).ToLower())
		{
		case "zhcn":
		case "zhhk":
		case "jajp":
		case "kokr":
		case "ukua":
		case "cscz":
		case "elgr":
		case "thth":
		case "trtr":
		case "huhu":
			RefSubtitles.Visibility = Visibility.Visible;
			PropEx.SetSprite1(RefSubtitlesButton, MainViewModel.Instance.GameSprites[261]);
			PropEx.SetSprite2(RefSubtitlesButton, MainViewModel.Instance.GameSprites[261]);
			PropEx.SetSprite3(RefSubtitlesButton, MainViewModel.Instance.GameSprites[261]);
			PropEx.SetSprite4(RefSubtitlesButton, MainViewModel.Instance.GameSprites[261]);
			break;
		default:
			RefSubtitles.Visibility = Visibility.Hidden;
			PropEx.SetSprite1(RefSubtitlesButton, MainViewModel.Instance.GameSprites[262]);
			PropEx.SetSprite2(RefSubtitlesButton, MainViewModel.Instance.GameSprites[262]);
			PropEx.SetSprite3(RefSubtitlesButton, MainViewModel.Instance.GameSprites[262]);
			PropEx.SetSprite4(RefSubtitlesButton, MainViewModel.Instance.GameSprites[262]);
			break;
		}
		RefSubtitlesButton.Visibility = Visibility.Visible;
		RefSubtitlesStory.Begin();
	}

	private void EndVideo()
	{
		RefSubtitlesStory.Stop();
		RefIntroVideo.Stop();
		RefIntroVideo.Source = null;
		RefIntroVideo.Close();
		if (ConfigSettings.SettingsFileExisted)
		{
			FatControler.instance.NewScene(Enums.SceneIDS.FrontEnd);
			return;
		}
		MainViewModel.Instance.EnterYourNameVis = true;
		RefEnterYourNameTB.Focus();
		RefEnterYourNameTB.CaretIndex = 50;
	}

	public void ForceStopVideo()
	{
		try
		{
			RefSubtitlesStory.Stop();
		}
		catch (Exception)
		{
		}
		try
		{
			RefIntroVideo.Stop();
		}
		catch (Exception)
		{
		}
		RefIntroVideo.Source = null;
		try
		{
			RefIntroVideo.Close();
		}
		catch (Exception)
		{
		}
	}

	public void ButtonClicked(bool fromClick)
	{
		if (fromClick)
		{
			if (binkLoaded)
			{
				if (stage == 0)
				{
					StartVideo();
				}
				else if (stage == 1)
				{
					EndVideo();
				}
			}
		}
		else
		{
			RefFadeInLogos.Stop();
			EndVideo();
		}
	}

	public void SubtitlesClicked()
	{
		if (RefSubtitles.Visibility == Visibility.Hidden)
		{
			RefSubtitles.Visibility = Visibility.Visible;
			PropEx.SetSprite1(RefSubtitlesButton, MainViewModel.Instance.GameSprites[261]);
			PropEx.SetSprite2(RefSubtitlesButton, MainViewModel.Instance.GameSprites[261]);
			PropEx.SetSprite3(RefSubtitlesButton, MainViewModel.Instance.GameSprites[261]);
			PropEx.SetSprite4(RefSubtitlesButton, MainViewModel.Instance.GameSprites[261]);
		}
		else
		{
			RefSubtitles.Visibility = Visibility.Hidden;
			PropEx.SetSprite1(RefSubtitlesButton, MainViewModel.Instance.GameSprites[262]);
			PropEx.SetSprite2(RefSubtitlesButton, MainViewModel.Instance.GameSprites[262]);
			PropEx.SetSprite3(RefSubtitlesButton, MainViewModel.Instance.GameSprites[262]);
			PropEx.SetSprite4(RefSubtitlesButton, MainViewModel.Instance.GameSprites[262]);
		}
	}

	public void EnterYourNameClicked()
	{
		ConfigSettings.Settings_UserName = RefEnterYourNameTB.Text;
		FatControler.instance.NewScene(Enums.SceneIDS.FrontEnd);
	}

	public void Update()
	{
		if (stage == 0 && refLogo.Opacity < 0.0001f && refPresents.Opacity < 0.0001f)
		{
			StartVideo();
		}
	}

	private void IntroVideo_Opened(object sender, RoutedEventArgs args)
	{
		RefIntroVideo.Pause();
		binkLoaded = true;
	}

	private void IntroVideo_Ended(object sender, RoutedEventArgs args)
	{
		EndVideo();
	}

	private void InitializeComponent()
	{
		GUI.LoadComponent(this, "Assets/GUI/XAML/IntroSequence.xaml");
	}

	protected override bool ConnectEvent(object source, string eventName, string handlerName)
	{
		if (eventName == "MouseEnter" && handlerName == "CommonRedButtonEnter")
		{
			if (source is Button)
			{
				((Button)source).MouseEnter += MainViewModel.Instance.CommonRedButtonEnter;
			}
			else if (source is RadioButton)
			{
				((RadioButton)source).MouseEnter += MainViewModel.Instance.CommonRedButtonEnter;
			}
			return true;
		}
		return false;
	}

	private void TextInputFocus(object sender, DependencyPropertyChangedEventArgs e)
	{
		MainViewModel.Instance.SetNoesisKeyboardState((bool)e.NewValue);
	}
}
