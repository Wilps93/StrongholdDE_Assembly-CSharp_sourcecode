using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class MyAudioManager : MonoBehaviour
{
	private static MyAudioManager instance;

	private const int NUM_SFX_CHANNELS = 20;

	private AudioSource[] sfxSource = new AudioSource[20];

	private AudioSource ambientSource1;

	private float ambientSound1_SoundVolume = 1f;

	private float ambientSound1_GameVolume = 1f;

	private bool ambientSound1_IsPlaying;

	private AudioSource ambientSource2;

	private float ambientSound2_SoundVolume = 1f;

	private float ambientSound2_GameVolume = 1f;

	private bool ambientSound2_IsPlaying;

	private int nextSFXChannel;

	private AudioSource speechSource1;

	private AudioClip speechClip1;

	private int speechMode1;

	private bool speech1Units;

	private AudioSource speechSource2;

	private AudioClip speechClip2;

	private int speechMode2;

	private bool speech2Units;

	private bool speechPaused;

	private AudioSource musicSource;

	private AudioClip musicClip;

	private int musicMode;

	private float nextMusic_SoundVolume = 1f;

	private bool nextMusicLoop;

	private AudioClip nextMusicClip;

	private float music_GameVolume = 1f;

	private float music_SoundVolume = 1f;

	private bool music_faded_for_speech;

	private float music_faded_for_speech_volume = 0.3f;

	private bool musicAboutToLoop;

	private DateTime musicAboutToLoopTime = DateTime.MinValue;

	private bool fadingOutMusic;

	private string fadeOutName = "";

	private DateTime fadeOutStart;

	private float fadeOutSoundVolume;

	private float fadeOutGameVolume;

	private bool fadeOutLoop;

	private bool music_faded_for_speech_forced;

	public static MyAudioManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = UnityEngine.Object.FindObjectOfType<MyAudioManager>();
			}
			return instance;
		}
		set
		{
			instance = value;
		}
	}

	private void Awake()
	{
		instance = this;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		for (int i = 0; i < 20; i++)
		{
			sfxSource[i] = base.gameObject.AddComponent<AudioSource>();
		}
		ambientSource1 = base.gameObject.AddComponent<AudioSource>();
		ambientSource2 = base.gameObject.AddComponent<AudioSource>();
		speechSource1 = base.gameObject.AddComponent<AudioSource>();
		speechSource2 = base.gameObject.AddComponent<AudioSource>();
		musicSource = base.gameObject.AddComponent<AudioSource>();
	}

	private void Start()
	{
	}

	private void OnDestroy()
	{
		StopAllGameSounds();
	}

	private void Update()
	{
		if (Application.isFocused)
		{
			ambientSound1_IsPlaying = getAmbientSource(1).isPlaying;
			ambientSound2_IsPlaying = getAmbientSource(2).isPlaying;
			MonitorLoadedSounds();
		}
	}

	public void playSFX(AudioClip sound, float volume, float pan = 0f)
	{
		AudioSource freeSFXChannel = getFreeSFXChannel();
		if (freeSFXChannel != null)
		{
			freeSFXChannel.panStereo = pan;
			freeSFXChannel.PlayOneShot(sound, volume * ConfigSettings.Settings_SFXVolume * ConfigSettings.Settings_MasterVolume);
		}
	}

	private AudioSource getFreeSFXChannel()
	{
		int num = 20;
		do
		{
			int num2 = nextSFXChannel;
			nextSFXChannel++;
			if (nextSFXChannel == 20)
			{
				nextSFXChannel = 0;
			}
			if (!sfxSource[num2].isPlaying)
			{
				return sfxSource[num2];
			}
		}
		while (num-- >= 0);
		return null;
	}

	public void updateSFXVolumeFromSettings()
	{
		if (isAmbientPlaying(1))
		{
			ambientSource1.volume = getAmbientVolume(1) * ConfigSettings.Settings_SFXVolume * ConfigSettings.Settings_MasterVolume;
		}
		if (isAmbientPlaying(2))
		{
			ambientSource2.volume = getAmbientVolume(2) * ConfigSettings.Settings_SFXVolume * ConfigSettings.Settings_MasterVolume;
		}
	}

	public void playAmbient(int channelID, AudioClip sound, float soundVolume, float gameVolume, bool loop)
	{
		AudioSource ambientSource = getAmbientSource(channelID);
		switch (channelID)
		{
		case 1:
			ambientSound1_SoundVolume = soundVolume;
			ambientSound1_GameVolume = gameVolume;
			break;
		case 2:
			ambientSound2_SoundVolume = soundVolume;
			ambientSound2_GameVolume = gameVolume;
			break;
		}
		float ambientVolume = getAmbientVolume(channelID);
		ambientSource.loop = loop;
		if (loop)
		{
			ambientSource.volume = ambientVolume * ConfigSettings.Settings_SFXVolume * ConfigSettings.Settings_MasterVolume;
			ambientSource.clip = sound;
			ambientSource.Play();
		}
		else
		{
			ambientSource.volume = ambientVolume * ConfigSettings.Settings_SFXVolume * ConfigSettings.Settings_MasterVolume;
			ambientSource.PlayOneShot(sound);
		}
	}

	public void setAmbientVolume(int channelID, float gameVolume)
	{
		AudioSource ambientSource = getAmbientSource(channelID);
		switch (channelID)
		{
		case 1:
			ambientSound1_GameVolume = gameVolume;
			break;
		case 2:
			ambientSound2_GameVolume = gameVolume;
			break;
		}
		float ambientVolume = getAmbientVolume(channelID);
		ambientSource.volume = ambientVolume * ConfigSettings.Settings_SFXVolume * ConfigSettings.Settings_MasterVolume;
	}

	public void stopAmbient(int channelID)
	{
		getAmbientSource(channelID).Stop();
	}

	public bool isAmbientPlaying(int channelID)
	{
		return channelID switch
		{
			1 => ambientSound1_IsPlaying, 
			2 => ambientSound2_IsPlaying, 
			_ => false, 
		};
	}

	private AudioSource getAmbientSource(int channelID)
	{
		return channelID switch
		{
			1 => ambientSource1, 
			2 => ambientSource2, 
			_ => ambientSource1, 
		};
	}

	private float getAmbientVolume(int channelID)
	{
		return channelID switch
		{
			1 => ambientSound1_GameVolume * ambientSound1_SoundVolume, 
			2 => ambientSound2_GameVolume * ambientSound2_SoundVolume, 
			_ => 1f, 
		};
	}

	public void PlaySpeech(int channel, string folder, string soundName, bool force = false, bool unitsSpeech = false)
	{
		switch (channel)
		{
		case 1:
			if (force && speechMode1 != 0)
			{
				speechSource1.Stop();
				if (speechClip1 != null)
				{
					speechClip1.UnloadAudioData();
					speechClip1 = null;
				}
				speechMode1 = 0;
			}
			if (speechMode1 == 0)
			{
				speechMode1 = 1;
				speech1Units = unitsSpeech;
				LoadClip(channel, folder, soundName, unitsSpeech);
			}
			break;
		case 2:
			if (force && speechMode2 != 0)
			{
				speechSource2.Stop();
				if (speechClip2 != null)
				{
					speechClip2.UnloadAudioData();
					speechClip2 = null;
				}
				speechMode2 = 0;
			}
			if (speechMode2 == 0)
			{
				speechMode2 = 1;
				speech2Units = unitsSpeech;
				LoadClip(channel, folder, soundName, unitsSpeech);
			}
			break;
		}
	}

	private async void LoadClip(int channel, string folder, string soundName, bool unitsSpeech)
	{
		string path = Path.Combine(Application.dataPath, "Assets", "GUI", "Speech", folder, soundName);
		switch (channel)
		{
		case 1:
		{
			speechClip1 = await LoadClip(path);
			float volume2 = ConfigSettings.Settings_SpeechVolume * ConfigSettings.Settings_MasterVolume;
			if (unitsSpeech)
			{
				volume2 = ConfigSettings.Settings_UnitSpeechVolume * ConfigSettings.Settings_MasterVolume;
			}
			speechSource1.volume = volume2;
			speechSource1.PlayOneShot(speechClip1);
			speechMode1 = 2;
			if (speechPaused)
			{
				speechSource1.Pause();
			}
			else
			{
				setMusicFadedState(faded: true);
			}
			break;
		}
		case 2:
		{
			speechClip2 = await LoadClip(path);
			float volume = ConfigSettings.Settings_SpeechVolume * ConfigSettings.Settings_MasterVolume;
			if (unitsSpeech)
			{
				volume = ConfigSettings.Settings_UnitSpeechVolume * ConfigSettings.Settings_MasterVolume;
			}
			speechSource2.volume = volume;
			speechSource2.PlayOneShot(speechClip2);
			speechMode2 = 2;
			if (speechPaused)
			{
				speechSource2.Pause();
			}
			else
			{
				setMusicFadedState(faded: true);
			}
			break;
		}
		}
	}

	public void PauseSpeech(bool state)
	{
		speechPaused = state;
		if (state)
		{
			if (speechMode1 == 2)
			{
				speechSource1.Pause();
			}
			if (speechMode2 == 2)
			{
				speechSource2.Pause();
			}
			return;
		}
		if (speechMode1 == 2)
		{
			speechSource1.UnPause();
			setMusicFadedState(faded: true);
		}
		if (speechMode2 == 2)
		{
			speechSource2.UnPause();
			setMusicFadedState(faded: true);
		}
	}

	private async Task<AudioClip> LoadClip(string path)
	{
		AudioClip clip = null;
		using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV))
		{
			uwr.SendWebRequest();
			try
			{
				while (!uwr.isDone)
				{
					await Task.Delay(5);
				}
				if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
				{
					Debug.Log(uwr.error + " " + path);
				}
				else
				{
					clip = DownloadHandlerAudioClip.GetContent(uwr);
				}
			}
			catch (Exception ex)
			{
				Debug.Log(ex.Message + ", " + ex.StackTrace);
			}
		}
		return clip;
	}

	private void MonitorLoadedSounds()
	{
		if (!speechPaused)
		{
			if (speechMode1 == 2 && !speechSource1.isPlaying && Application.isFocused)
			{
				if (speechClip1 != null)
				{
					speechClip1.UnloadAudioData();
					speechClip1 = null;
				}
				speechMode1 = 0;
			}
			if (speechMode2 == 2 && !speechSource2.isPlaying && Application.isFocused)
			{
				if (speechClip2 != null)
				{
					speechClip2.UnloadAudioData();
					speechClip2 = null;
				}
				speechMode2 = 0;
			}
			if (!music_faded_for_speech_forced)
			{
				setMusicFadedState((speechSource1.isPlaying || speechSource2.isPlaying) && ConfigSettings.Settings_ReduceMusicVolumeForSpeech);
			}
		}
		if (musicMode != 2)
		{
			return;
		}
		if (fadingOutMusic)
		{
			TimeSpan timeSpan = DateTime.UtcNow - fadeOutStart;
			float num = 1f;
			if (timeSpan.TotalSeconds > (double)num)
			{
				fadingOutMusic = false;
				if (musicSource.isPlaying)
				{
					musicSource.Stop();
				}
				if (musicClip != null)
				{
					musicClip.UnloadAudioData();
					musicClip = null;
				}
				musicMode = 0;
				if (fadeOutName.Length > 0)
				{
					PlayMusic(fadeOutName, fadeOutGameVolume, fadeOutSoundVolume, fadeOutLoop);
				}
			}
			else
			{
				float num2 = (num - (float)timeSpan.TotalSeconds) / num;
				musicSource.volume = music_GameVolume * music_SoundVolume * ConfigSettings.Settings_MusicVolume * ConfigSettings.Settings_MasterVolume * getFadedMusicVolume() * num2;
			}
			return;
		}
		if (musicSource.time > musicSource.clip.length - 1f && !musicAboutToLoop && musicAboutToLoopTime < DateTime.UtcNow)
		{
			musicAboutToLoop = true;
			musicAboutToLoopTime = DateTime.UtcNow.AddSeconds(4.0);
		}
		if (!musicSource.isPlaying && Application.isFocused)
		{
			if (musicClip != null)
			{
				musicClip.UnloadAudioData();
				musicClip = null;
			}
			if (nextMusicClip != null)
			{
				musicSource.loop = nextMusicLoop;
				musicSource.clip = nextMusicClip;
				musicClip = nextMusicClip;
				nextMusicClip = null;
				musicSource.volume = nextMusic_SoundVolume * music_GameVolume * ConfigSettings.Settings_MusicVolume * ConfigSettings.Settings_MasterVolume * getFadedMusicVolume();
				musicSource.Play();
				musicAboutToLoop = false;
				musicAboutToLoopTime = DateTime.MinValue;
				musicMode = 2;
			}
			else
			{
				musicMode = 0;
			}
		}
	}

	public bool isSpeechPlaying(int channelID)
	{
		return channelID switch
		{
			1 => speechMode1 != 0, 
			2 => speechMode2 != 0, 
			_ => false, 
		};
	}

	public void updateSpeechVolumeFromSettings()
	{
		if (isSpeechPlaying(1))
		{
			if (speech1Units)
			{
				speechSource1.volume = ConfigSettings.Settings_UnitSpeechVolume * ConfigSettings.Settings_MasterVolume;
			}
			else
			{
				speechSource1.volume = ConfigSettings.Settings_SpeechVolume * ConfigSettings.Settings_MasterVolume;
			}
		}
		if (isSpeechPlaying(2))
		{
			if (speech2Units)
			{
				speechSource2.volume = ConfigSettings.Settings_UnitSpeechVolume * ConfigSettings.Settings_MasterVolume;
			}
			else
			{
				speechSource2.volume = ConfigSettings.Settings_SpeechVolume * ConfigSettings.Settings_MasterVolume;
			}
		}
	}

	public void FadeOutMusic()
	{
		fadingOutMusic = true;
		fadeOutName = "";
		fadeOutStart = DateTime.UtcNow;
		fadeOutSoundVolume = 0f;
		fadeOutGameVolume = 0f;
		fadeOutLoop = false;
	}

	public void PlayMusic(string soundName, float gameVolume, float soundVolume, bool loop = true, bool followon = false, bool fadeOut = false)
	{
		soundName = soundName.Replace(".raw", ".wav");
		if (musicMode != 0)
		{
			if (!followon)
			{
				if (fadeOut)
				{
					fadingOutMusic = true;
					fadeOutName = soundName;
					fadeOutStart = DateTime.UtcNow;
					fadeOutGameVolume = gameVolume;
					fadeOutSoundVolume = soundVolume;
					fadeOutLoop = loop;
					return;
				}
				musicSource.Stop();
				if (musicClip != null)
				{
					musicClip.UnloadAudioData();
					musicClip = null;
				}
				musicMode = 0;
			}
			else
			{
				musicSource.loop = false;
			}
		}
		else if (followon)
		{
			followon = false;
		}
		if (musicMode == 0 || followon)
		{
			if (!followon)
			{
				musicMode = 1;
			}
			LoadMusicClip(soundName, gameVolume, soundVolume, loop, followon);
		}
	}

	private async void LoadMusicClip(string soundName, float gameVolume, float soundVolume, bool loop, bool followon)
	{
		string path = Path.Combine(Application.streamingAssetsPath, "Music", soundName);
		if (!followon)
		{
			musicClip = await LoadClip(path);
			music_GameVolume = gameVolume;
			music_SoundVolume = soundVolume;
			musicSource.loop = loop;
			musicSource.volume = gameVolume * soundVolume * ConfigSettings.Settings_MusicVolume * ConfigSettings.Settings_MasterVolume * getFadedMusicVolume();
			musicSource.clip = musicClip;
			musicSource.Play();
			musicAboutToLoop = false;
			musicAboutToLoopTime = DateTime.MinValue;
			musicMode = 2;
		}
		else
		{
			nextMusicLoop = loop;
			nextMusic_SoundVolume = soundVolume;
			nextMusicClip = await LoadClip(path);
		}
	}

	public bool isMusicPlaying()
	{
		return musicMode != 0;
	}

	public bool isMusicAboutToLoop()
	{
		if (musicAboutToLoop)
		{
			musicAboutToLoop = false;
			return true;
		}
		return false;
	}

	public void setMusicVolume(float gameVolume)
	{
		music_GameVolume = gameVolume;
		musicSource.volume = music_GameVolume * music_SoundVolume * ConfigSettings.Settings_MusicVolume * ConfigSettings.Settings_MasterVolume * getFadedMusicVolume();
	}

	private float getFadedMusicVolume()
	{
		if (music_faded_for_speech)
		{
			return music_faded_for_speech_volume;
		}
		return 1f;
	}

	public void setMusicFadedState(bool faded, float fadedVolume = 0.3f, bool force = false)
	{
		if (music_faded_for_speech != faded)
		{
			music_faded_for_speech_forced = force;
			if (faded && ConfigSettings.Settings_SpeechVolume == 0f)
			{
				faded = false;
			}
			music_faded_for_speech = faded;
			music_faded_for_speech_volume = fadedVolume;
			updateMusicVolumeFromSettings();
		}
	}

	public void updateMusicVolumeFromSettings()
	{
		if (isMusicPlaying())
		{
			setMusicVolume(music_GameVolume);
		}
	}

	public void stopMusic()
	{
		if (musicMode == 2)
		{
			if (nextMusicClip != null)
			{
				nextMusicClip.UnloadAudioData();
				nextMusicClip = null;
			}
			if (musicSource.isPlaying)
			{
				musicSource.Stop();
			}
			if (musicClip != null)
			{
				musicClip.UnloadAudioData();
				musicClip = null;
			}
			musicMode = 0;
		}
	}

	public void StopSFX()
	{
		AudioSource[] array = sfxSource;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Stop();
		}
	}

	public void StopAllGameSounds(bool leaveMusicPlaying = false)
	{
		ambientSource1.Stop();
		ambientSource2.Stop();
		StopSFX();
		if (speechMode1 >= 1)
		{
			try
			{
				if (speechSource1.isPlaying)
				{
					speechSource1.Stop();
				}
				if (speechClip1 != null)
				{
					speechClip1.UnloadAudioData();
					speechClip1 = null;
				}
			}
			catch (Exception)
			{
			}
			speechMode1 = 0;
		}
		if (speechMode2 >= 1)
		{
			try
			{
				if (speechSource2.isPlaying)
				{
					speechSource2.Stop();
				}
				if (speechClip2 != null)
				{
					speechClip2.UnloadAudioData();
					speechClip2 = null;
				}
			}
			catch (Exception)
			{
			}
			speechMode2 = 0;
		}
		music_faded_for_speech = false;
		if (!leaveMusicPlaying)
		{
			stopMusic();
		}
		speechPaused = false;
	}
}
