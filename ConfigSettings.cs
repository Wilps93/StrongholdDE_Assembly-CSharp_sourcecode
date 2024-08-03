using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigSettings
{
	private class Scores
	{
		public string mapName;

		public int bestscore;

		public int difficulty = 1000;
	}

	public const int SCREEN_SIZE_MINIMUM_WIDTH = 1280;

	public const int SCREEN_SIZE_MINIMUM_HEIGHT = 768;

	public static bool AchievementsDisabled = false;

	public static bool TempMissionUnlock = false;

	private static bool settingsDirty = false;

	private static bool _settingsFileExisted = false;

	private static string settings_UserName = "Lord Stronghold";

	private static bool settings_PushMapScrolling = true;

	private static bool settings_SH1MouseWheel = false;

	private static bool settings_SH1RTSControls = true;

	private static bool settings_SH1CentreControls = true;

	private static bool settings_ShowBuildingTooltips = true;

	public const float Default_MusicVolume = 1f;

	private static float settings_MusicVolume = 1f;

	public const float Default_SpeechVolume = 1f;

	private static float settings_SpeechVolume = 1f;

	public const float Default_UnitSpeechVolume = 1f;

	private static float settings_UnitSpeechVolume = 1f;

	public const float Default_SFXVolume = 1f;

	private static float settings_SFXVolume = 1f;

	public const float Default_MasterVolume = 0.8f;

	private static float settings_MasterVolume = 0.8f;

	private static bool settings_PlayUISFX = true;

	private static bool settings_ReduceMusicVolumeForSpeech = true;

	private static bool settings_UseSteamOverlayForHelp = false;

	private static bool settings_CheatKeysEnabled = false;

	private static bool settings_ShowPings = false;

	private static bool settings_ExtraZoom = true;

	public const int Default_ScrollSpeed = 5;

	private static int settings_ScrollSpeed = 5;

	private static bool settings_Compass = false;

	private static bool settings_RadarDefaultZoomedOut = false;

	private static bool settings_CustomIntros = true;

	public const int Default_GameSpeed = 40;

	private static int settings_GameSpeed = 40;

	private static bool settings_LockCursor = false;

	private static bool settings_Vsync = false;

	private static bool settings_SkipIntro = false;

	private static int settings_Scribe = 2;

	private static int settings_CursorStyle = 0;

	private static int settings_PlayerColour = 0;

	private static int settings_LastWindowWidth = -1;

	private static int settings_LastWindowHeight = -1;

	private static int settings_LastFullscreenWidth = -1;

	private static int settings_LastFullscreenHeight = -1;

	private static int settings_LastFullscreenRefresh = -1;

	private static int settings_LastFullscreenType = -1;

	private static float settings_UIScale = 1f;

	private static int settings_Progress_Campaign = 1;

	private static int settings_Progress_EcoCampaign = 1;

	private static int settings_Progress_Extra1Campaign = 1;

	private static int settings_Progress_Extra2Campaign = 1;

	private static int settings_Progress_Extra3Campaign = 1;

	private static int settings_Progress_ExtraEcoCampaign = 1;

	private static int settings_Progress_Extra4Campaign = 1;

	private static int settings_Progress_Trail = 1;

	private static int settings_Progress_Trail2 = 1;

	private static bool muteSounds = false;

	private static Dictionary<string, Scores> scores = new Dictionary<string, Scores>();

	public static bool SettingsFileExisted => _settingsFileExisted;

	public static string Settings_UserName
	{
		get
		{
			return settings_UserName;
		}
		set
		{
			if (settings_UserName != value)
			{
				settingsDirty = true;
				settings_UserName = value;
			}
		}
	}

	public static bool Settings_PushMapScrolling
	{
		get
		{
			return settings_PushMapScrolling;
		}
		set
		{
			if (settings_PushMapScrolling != value)
			{
				settingsDirty = true;
				settings_PushMapScrolling = value;
			}
		}
	}

	public static bool Settings_SH1MouseWheel
	{
		get
		{
			return settings_SH1MouseWheel;
		}
		set
		{
			if (settings_SH1MouseWheel != value)
			{
				settingsDirty = true;
				settings_SH1MouseWheel = value;
			}
		}
	}

	public static bool Settings_SH1RTSControls
	{
		get
		{
			return settings_SH1RTSControls;
		}
		set
		{
			if (settings_SH1RTSControls != value)
			{
				settingsDirty = true;
				settings_SH1RTSControls = value;
			}
		}
	}

	public static bool Settings_SH1CentreControls
	{
		get
		{
			return settings_SH1CentreControls;
		}
		set
		{
			if (settings_SH1CentreControls != value)
			{
				settingsDirty = true;
				settings_SH1CentreControls = value;
			}
		}
	}

	public static bool Settings_ShowBuildingTooltips
	{
		get
		{
			return settings_ShowBuildingTooltips;
		}
		set
		{
			if (settings_ShowBuildingTooltips != value)
			{
				settingsDirty = true;
				settings_ShowBuildingTooltips = value;
			}
		}
	}

	public static float Settings_MusicVolume
	{
		get
		{
			return settings_MusicVolume;
		}
		set
		{
			if (settings_MusicVolume != value)
			{
				settingsDirty = true;
				settings_MusicVolume = value;
			}
		}
	}

	public static float Settings_SpeechVolume
	{
		get
		{
			return settings_SpeechVolume;
		}
		set
		{
			if (settings_SpeechVolume != value)
			{
				settingsDirty = true;
				settings_SpeechVolume = value;
			}
		}
	}

	public static float Settings_UnitSpeechVolume
	{
		get
		{
			return settings_UnitSpeechVolume;
		}
		set
		{
			if (settings_UnitSpeechVolume != value)
			{
				settingsDirty = true;
				settings_UnitSpeechVolume = value;
			}
		}
	}

	public static float Settings_SFXVolume
	{
		get
		{
			return settings_SFXVolume;
		}
		set
		{
			if (settings_SFXVolume != value)
			{
				settingsDirty = true;
				settings_SFXVolume = value;
			}
		}
	}

	public static float Settings_MasterVolume
	{
		get
		{
			if (!muteSounds)
			{
				return settings_MasterVolume;
			}
			return 0f;
		}
		set
		{
			if (settings_MasterVolume != value)
			{
				settingsDirty = true;
				settings_MasterVolume = value;
			}
		}
	}

	public static bool Settings_PlayUISFX
	{
		get
		{
			return settings_PlayUISFX;
		}
		set
		{
			if (settings_PlayUISFX != value)
			{
				settingsDirty = true;
				settings_PlayUISFX = value;
			}
		}
	}

	public static bool Settings_ReduceMusicVolumeForSpeech
	{
		get
		{
			return settings_ReduceMusicVolumeForSpeech;
		}
		set
		{
			if (settings_ReduceMusicVolumeForSpeech != value)
			{
				settingsDirty = true;
				settings_ReduceMusicVolumeForSpeech = value;
			}
		}
	}

	public static bool Settings_UseSteamOverlayForHelp
	{
		get
		{
			return settings_UseSteamOverlayForHelp;
		}
		set
		{
			if (settings_UseSteamOverlayForHelp != value)
			{
				settingsDirty = true;
				settings_UseSteamOverlayForHelp = value;
			}
		}
	}

	public static bool Settings_CheatKeysEnabled
	{
		get
		{
			return settings_CheatKeysEnabled;
		}
		set
		{
			if (settings_CheatKeysEnabled != value)
			{
				settingsDirty = true;
				settings_CheatKeysEnabled = value;
			}
		}
	}

	public static bool Settings_ShowPings
	{
		get
		{
			return settings_ShowPings;
		}
		set
		{
			if (settings_ShowPings != value)
			{
				settingsDirty = true;
				settings_ShowPings = value;
			}
		}
	}

	public static bool Settings_ExtraZoom
	{
		get
		{
			return settings_ExtraZoom;
		}
		set
		{
			if (settings_ExtraZoom != value)
			{
				settingsDirty = true;
				settings_ExtraZoom = value;
			}
		}
	}

	public static int Settings_ScrollSpeed
	{
		get
		{
			return settings_ScrollSpeed;
		}
		set
		{
			if (settings_ScrollSpeed != value)
			{
				settingsDirty = true;
				settings_ScrollSpeed = value;
			}
		}
	}

	public static bool Settings_Compass
	{
		get
		{
			return settings_Compass;
		}
		set
		{
			if (settings_Compass != value)
			{
				settingsDirty = true;
				settings_Compass = value;
			}
		}
	}

	public static bool Settings_RadarDefaultZoomedOut
	{
		get
		{
			return settings_RadarDefaultZoomedOut;
		}
		set
		{
			if (settings_RadarDefaultZoomedOut != value)
			{
				settingsDirty = true;
				settings_RadarDefaultZoomedOut = value;
			}
		}
	}

	public static bool Settings_CustomIntros
	{
		get
		{
			return settings_CustomIntros;
		}
		set
		{
			if (settings_CustomIntros != value)
			{
				settingsDirty = true;
				settings_CustomIntros = value;
			}
		}
	}

	public static int Settings_GameSpeed
	{
		get
		{
			return settings_GameSpeed;
		}
		set
		{
			if (settings_GameSpeed != value)
			{
				settingsDirty = true;
				settings_GameSpeed = value;
			}
		}
	}

	public static bool Settings_LockCursor
	{
		get
		{
			return settings_LockCursor;
		}
		set
		{
			if (settings_LockCursor != value)
			{
				settingsDirty = true;
				settings_LockCursor = value;
				if (value)
				{
					Cursor.lockState = CursorLockMode.Confined;
				}
				else
				{
					Cursor.lockState = CursorLockMode.None;
				}
			}
		}
	}

	public static bool Settings_Vsync
	{
		get
		{
			return settings_Vsync;
		}
		set
		{
			if (settings_Vsync != value)
			{
				settingsDirty = true;
				settings_Vsync = value;
			}
		}
	}

	public static bool Settings_SkipIntro => settings_SkipIntro;

	public static int Settings_Scribe
	{
		get
		{
			return settings_Scribe;
		}
		set
		{
			if (settings_Scribe != value)
			{
				settingsDirty = true;
				settings_Scribe = value;
			}
		}
	}

	public static int Settings_CursorStyle
	{
		get
		{
			return settings_CursorStyle;
		}
		set
		{
			if (settings_CursorStyle != value)
			{
				settingsDirty = true;
				settings_CursorStyle = value;
			}
		}
	}

	public static int Settings_PlayerColour
	{
		get
		{
			return settings_PlayerColour;
		}
		set
		{
			if (settings_PlayerColour != value)
			{
				settingsDirty = true;
				settings_PlayerColour = value;
			}
		}
	}

	public static int Settings_LastWindowWidth
	{
		get
		{
			return settings_LastWindowWidth;
		}
		set
		{
			if (settings_LastWindowWidth != value)
			{
				settingsDirty = true;
				settings_LastWindowWidth = value;
			}
		}
	}

	public static int Settings_LastWindowHeight
	{
		get
		{
			return settings_LastWindowHeight;
		}
		set
		{
			if (settings_LastWindowHeight != value)
			{
				settingsDirty = true;
				settings_LastWindowHeight = value;
			}
		}
	}

	public static int Settings_LastFullscreenWidth
	{
		get
		{
			return settings_LastFullscreenWidth;
		}
		set
		{
			if (settings_LastFullscreenWidth != value)
			{
				settingsDirty = true;
				settings_LastFullscreenWidth = value;
			}
		}
	}

	public static int Settings_LastFullscreenHeight
	{
		get
		{
			return settings_LastFullscreenHeight;
		}
		set
		{
			if (settings_LastFullscreenHeight != value)
			{
				settingsDirty = true;
				settings_LastFullscreenHeight = value;
			}
		}
	}

	public static int Settings_LastFullscreenRefresh
	{
		get
		{
			return settings_LastFullscreenRefresh;
		}
		set
		{
			if (settings_LastFullscreenRefresh != value)
			{
				settingsDirty = true;
				settings_LastFullscreenRefresh = value;
			}
		}
	}

	public static int Settings_LastFullscreenType
	{
		get
		{
			return settings_LastFullscreenType;
		}
		set
		{
			if (settings_LastFullscreenType != value)
			{
				settingsDirty = true;
				settings_LastFullscreenType = value;
			}
		}
	}

	public static float Settings_UIScale
	{
		get
		{
			return settings_UIScale;
		}
		set
		{
			if (settings_UIScale != value)
			{
				settingsDirty = true;
				settings_UIScale = value;
			}
		}
	}

	public static int Settings_Progress_Campaign
	{
		get
		{
			if (TempMissionUnlock)
			{
				return 21;
			}
			return settings_Progress_Campaign;
		}
		set
		{
			if (settings_Progress_Campaign != value)
			{
				settingsDirty = true;
				settings_Progress_Campaign = value;
			}
		}
	}

	public static int Settings_Progress_EcoCampaign
	{
		get
		{
			if (TempMissionUnlock)
			{
				return 5;
			}
			return settings_Progress_EcoCampaign;
		}
		set
		{
			if (settings_Progress_EcoCampaign != value)
			{
				settingsDirty = true;
				settings_Progress_EcoCampaign = value;
			}
		}
	}

	public static int Settings_Progress_Extra1Campaign
	{
		get
		{
			if (TempMissionUnlock)
			{
				return 7;
			}
			return settings_Progress_Extra1Campaign;
		}
		set
		{
			if (settings_Progress_Extra1Campaign != value)
			{
				settingsDirty = true;
				settings_Progress_Extra1Campaign = value;
			}
		}
	}

	public static int Settings_Progress_Extra2Campaign
	{
		get
		{
			if (TempMissionUnlock)
			{
				return 7;
			}
			return settings_Progress_Extra2Campaign;
		}
		set
		{
			if (settings_Progress_Extra2Campaign != value)
			{
				settingsDirty = true;
				settings_Progress_Extra2Campaign = value;
			}
		}
	}

	public static int Settings_Progress_Extra3Campaign
	{
		get
		{
			if (TempMissionUnlock)
			{
				return 7;
			}
			return settings_Progress_Extra3Campaign;
		}
		set
		{
			if (settings_Progress_Extra3Campaign != value)
			{
				settingsDirty = true;
				settings_Progress_Extra3Campaign = value;
			}
		}
	}

	public static int Settings_Progress_ExtraEcoCampaign
	{
		get
		{
			if (TempMissionUnlock)
			{
				return 7;
			}
			return settings_Progress_ExtraEcoCampaign;
		}
		set
		{
			if (settings_Progress_ExtraEcoCampaign != value)
			{
				settingsDirty = true;
				settings_Progress_ExtraEcoCampaign = value;
			}
		}
	}

	public static int Settings_Progress_Extra4Campaign
	{
		get
		{
			if (TempMissionUnlock)
			{
				return 7;
			}
			return settings_Progress_Extra4Campaign;
		}
		set
		{
			if (settings_Progress_Extra4Campaign != value)
			{
				settingsDirty = true;
				settings_Progress_Extra4Campaign = value;
			}
		}
	}

	public static int Settings_Progress_Trail
	{
		get
		{
			if (TempMissionUnlock)
			{
				return 10;
			}
			return settings_Progress_Trail;
		}
		set
		{
			if (settings_Progress_Trail != value)
			{
				settingsDirty = true;
				settings_Progress_Trail = value;
			}
		}
	}

	public static int Settings_Progress_Trail2
	{
		get
		{
			if (TempMissionUnlock)
			{
				return 10;
			}
			return settings_Progress_Trail2;
		}
		set
		{
			if (settings_Progress_Trail2 != value)
			{
				settingsDirty = true;
				settings_Progress_Trail2 = value;
			}
		}
	}

	public static void SetDirty()
	{
		settingsDirty = true;
	}

	public static float GetScrollSpeed()
	{
		return (float)Settings_ScrollSpeed * 0.1f + 0.5f;
	}

	public static void toggleMuteSounds()
	{
		muteSounds = !muteSounds;
		MyAudioManager.Instance.updateSFXVolumeFromSettings();
		MyAudioManager.Instance.updateSpeechVolumeFromSettings();
		MyAudioManager.Instance.updateMusicVolumeFromSettings();
	}

	public static string GetSettingsFileName(bool createPaths = false)
	{
		string persistentDataPath = Application.persistentDataPath;
		if (createPaths)
		{
			if (!Directory.Exists(persistentDataPath))
			{
				Directory.CreateDirectory(persistentDataPath);
			}
			if (!Directory.Exists(persistentDataPath + "\\Saves"))
			{
				Directory.CreateDirectory(persistentDataPath + "\\Saves");
			}
			if (!Directory.Exists(persistentDataPath + "\\Maps"))
			{
				Directory.CreateDirectory(persistentDataPath + "\\Maps");
			}
		}
		return persistentDataPath + "/settings.cfg";
	}

	public static string GetSavesPath()
	{
		return Application.persistentDataPath + "\\Saves";
	}

	public static string GetUserMapsPath()
	{
		return Application.persistentDataPath + "\\Maps";
	}

	public static string GetMpAutoSavePath()
	{
		return Application.persistentDataPath + "\\Saves\\autosave.msv";
	}

	public static void LoadSettings()
	{
		string settingsFileName = GetSettingsFileName(createPaths: true);
		try
		{
			string settingsString = File.ReadAllText(settingsFileName);
			loadSettingsFromString(settingsString);
			KeyManager.instance.LoadFromString(settingsString);
			_settingsFileExisted = true;
		}
		catch (Exception)
		{
		}
		try
		{
			LoadScores();
		}
		catch (Exception)
		{
		}
	}

	public static void SaveSettings(bool onlyWhenAlreadyExists = false)
	{
		string settingsFileName = GetSettingsFileName();
		bool flag = File.Exists(settingsFileName);
		if (!settingsDirty && flag)
		{
			return;
		}
		settingsDirty = false;
		if (onlyWhenAlreadyExists && !flag)
		{
			return;
		}
		string text = "";
		text += createSettingString();
		text += KeyManager.instance.SaveToString();
		try
		{
			File.WriteAllText(settingsFileName, text);
		}
		catch (Exception)
		{
		}
	}

	private static void loadSettingsFromString(string settingsString)
	{
		string[] array = settingsString.Split("||SETTINGS||\n");
		if (array.Length == 3)
		{
			settings_ExtraZoom = false;
			string[] array2 = array[1].Split("\n");
			foreach (string text in array2)
			{
				try
				{
					string[] array3 = text.Split(":");
					if (array3.Length <= 1)
					{
						continue;
					}
					switch (array3[0].ToLowerInvariant())
					{
					case "name":
						settings_UserName = array3[1];
						if (settings_UserName.Length > 39)
						{
							settings_UserName = settings_UserName.Substring(0, 39);
						}
						break;
					case "pushmapscrolling":
						settings_PushMapScrolling = bool.Parse(array3[1]);
						break;
					case "scrollspeed":
						settings_ScrollSpeed = Math.Clamp(int.Parse(array3[1], Director.defaultCulture), 0, 15);
						break;
					case "gamespeed":
						settings_GameSpeed = Math.Clamp(int.Parse(array3[1], Director.defaultCulture) / 5 * 5, 10, 90);
						break;
					case "sh1mousewheel":
						settings_SH1MouseWheel = bool.Parse(array3[1]);
						break;
					case "sh1rtscontrols":
						settings_SH1RTSControls = bool.Parse(array3[1]);
						break;
					case "sh1centrecontrols":
						settings_SH1CentreControls = bool.Parse(array3[1]);
						break;
					case "showbuildingtooltips":
						settings_ShowBuildingTooltips = bool.Parse(array3[1]);
						break;
					case "playuisfx":
						settings_PlayUISFX = bool.Parse(array3[1]);
						break;
					case "reducemusicvolumeforspeech":
						settings_ReduceMusicVolumeForSpeech = bool.Parse(array3[1]);
						break;
					case "usesteamoverlayforhelp":
						settings_UseSteamOverlayForHelp = bool.Parse(array3[1]);
						break;
					case "cheatkeysenabled":
						settings_CheatKeysEnabled = bool.Parse(array3[1]);
						break;
					case "showpings":
						settings_ShowPings = bool.Parse(array3[1]);
						break;
					case "extrazoom":
						settings_ExtraZoom = bool.Parse(array3[1]);
						break;
					case "compass":
						settings_Compass = bool.Parse(array3[1]);
						break;
					case "radardefaultzoomedout":
						settings_RadarDefaultZoomedOut = bool.Parse(array3[1]);
						break;
					case "customintros":
						settings_CustomIntros = bool.Parse(array3[1]);
						break;
					case "mastervolume":
						settings_MasterVolume = Mathf.Clamp(float.Parse(array3[1], Director.defaultCulture), 0f, 1f);
						break;
					case "sfxvolume":
						settings_SFXVolume = Mathf.Clamp(float.Parse(array3[1], Director.defaultCulture), 0f, 1f);
						break;
					case "speechvolume":
						settings_SpeechVolume = Mathf.Clamp(float.Parse(array3[1], Director.defaultCulture), 0f, 1f);
						break;
					case "unitspeechvolume":
						settings_UnitSpeechVolume = Mathf.Clamp(float.Parse(array3[1], Director.defaultCulture), 0f, 1f);
						break;
					case "musicvolume":
						settings_MusicVolume = Mathf.Clamp(float.Parse(array3[1], Director.defaultCulture), 0f, 1f);
						break;
					case "lockcursor":
						settings_LockCursor = bool.Parse(array3[1]);
						break;
					case "cursorstyle":
						settings_CursorStyle = int.Parse(array3[1], Director.defaultCulture);
						break;
					case "playercolour":
						settings_PlayerColour = int.Parse(array3[1], Director.defaultCulture);
						break;
					case "winwidth":
						settings_LastWindowWidth = int.Parse(array3[1], Director.defaultCulture);
						break;
					case "winheight":
						settings_LastWindowHeight = int.Parse(array3[1], Director.defaultCulture);
						break;
					case "fullwidth":
						settings_LastFullscreenWidth = int.Parse(array3[1], Director.defaultCulture);
						break;
					case "fullheight":
						settings_LastFullscreenHeight = int.Parse(array3[1], Director.defaultCulture);
						break;
					case "fullrefreshrate":
						settings_LastFullscreenRefresh = int.Parse(array3[1], Director.defaultCulture);
						break;
					case "fullscreentype":
						settings_LastFullscreenType = int.Parse(array3[1], Director.defaultCulture);
						break;
					case "vsync":
						settings_Vsync = bool.Parse(array3[1]);
						break;
					case "scribe":
						settings_Scribe = int.Parse(array3[1], Director.defaultCulture);
						if (settings_Scribe == 1)
						{
							settings_Scribe = 2;
						}
						break;
					case "skipintro":
						settings_SkipIntro = bool.Parse(array3[1]);
						break;
					case "uiscale":
						settings_UIScale = Mathf.Clamp(float.Parse(array3[1], Director.defaultCulture), 0f, 1f);
						break;
					case "campaign":
						settings_Progress_Campaign = int.Parse(array3[1], Director.defaultCulture);
						break;
					case "campaigneco":
						settings_Progress_EcoCampaign = int.Parse(array3[1], Director.defaultCulture);
						break;
					case "campaignextra1":
						Settings_Progress_Extra1Campaign = int.Parse(array3[1], Director.defaultCulture);
						break;
					case "campaignextra2":
						Settings_Progress_Extra2Campaign = int.Parse(array3[1], Director.defaultCulture);
						break;
					case "campaignextra3":
						Settings_Progress_Extra3Campaign = int.Parse(array3[1], Director.defaultCulture);
						break;
					case "campaignextraeco":
						Settings_Progress_ExtraEcoCampaign = int.Parse(array3[1], Director.defaultCulture);
						break;
					case "campaignextra4":
						Settings_Progress_Extra4Campaign = int.Parse(array3[1], Director.defaultCulture);
						break;
					case "campaigntrail2":
						settings_Progress_Trail2 = int.Parse(array3[1], Director.defaultCulture);
						break;
					case "campaigntrail":
						settings_Progress_Trail = int.Parse(array3[1], Director.defaultCulture);
						break;
					}
				}
				catch (Exception)
				{
				}
			}
		}
		if (settings_Progress_Campaign > 21)
		{
			settings_Progress_Campaign = 21;
		}
		if (settings_Progress_EcoCampaign > 5)
		{
			settings_Progress_EcoCampaign = 5;
		}
		if (settings_Progress_Trail > 10)
		{
			settings_Progress_Trail = 10;
		}
		if (Settings_Progress_Extra1Campaign > 7)
		{
			Settings_Progress_Extra1Campaign = 7;
		}
		if (Settings_Progress_Extra2Campaign > 7)
		{
			Settings_Progress_Extra2Campaign = 7;
		}
		if (Settings_Progress_Extra3Campaign > 7)
		{
			Settings_Progress_Extra3Campaign = 7;
		}
		if (Settings_Progress_ExtraEcoCampaign > 7)
		{
			Settings_Progress_ExtraEcoCampaign = 7;
		}
		if (Settings_Progress_Extra4Campaign > 7)
		{
			Settings_Progress_Extra4Campaign = 7;
		}
		if (settings_Progress_Trail2 > 10)
		{
			settings_Progress_Trail2 = 10;
		}
	}

	private static string createSettingString()
	{
		return string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat("||SETTINGS||\n" + "Name:" + settings_UserName + "\n", "PushMapScrolling:", settings_PushMapScrolling.ToString(), "\n"), "ScrollSpeed:", settings_ScrollSpeed.ToString(), "\n"), "GameSpeed:", settings_GameSpeed.ToString(), "\n"), "SH1MouseWheel:", settings_SH1MouseWheel.ToString(), "\n"), "SH1RTSControls:", settings_SH1RTSControls.ToString(), "\n"), "SH1CentreControls:", settings_SH1CentreControls.ToString(), "\n"), "ShowBuildingTooltips:", settings_ShowBuildingTooltips.ToString(), "\n"), "Compass:", settings_Compass.ToString(), "\n"), "RadarDefaultZoomedOut:", settings_RadarDefaultZoomedOut.ToString(), "\n"), "CustomIntros:", settings_CustomIntros.ToString(), "\n"), "MasterVolume:", settings_MasterVolume.ToString(Director.defaultCulture), "\n"), "SFXVolume:", settings_SFXVolume.ToString(Director.defaultCulture), "\n"), "SpeechVolume:", settings_SpeechVolume.ToString(Director.defaultCulture), "\n"), "UnitSpeechVolume:", settings_UnitSpeechVolume.ToString(Director.defaultCulture), "\n"), "MusicVolume:", settings_MusicVolume.ToString(Director.defaultCulture), "\n"), "PlayUISFX:", settings_PlayUISFX.ToString(), "\n"), "ReduceMusicVolumeForSpeech:", settings_ReduceMusicVolumeForSpeech.ToString(), "\n"), "UseSteamOverlayForHelp:", settings_UseSteamOverlayForHelp.ToString(), "\n"), "CheatKeysEnabled:", settings_CheatKeysEnabled.ToString(), "\n"), "ShowPings:", settings_ShowPings.ToString(), "\n"), "ExtraZoom:", settings_ExtraZoom.ToString(), "\n"), "LockCursor:", settings_LockCursor.ToString(), "\n"), "CursorStyle:", settings_CursorStyle.ToString(), "\n"), "PlayerColour:", settings_PlayerColour.ToString(), "\n"), "WinWidth:", settings_LastWindowWidth.ToString(), "\n"), "WinHeight:", settings_LastWindowHeight.ToString(), "\n"), "FullWidth:", settings_LastFullscreenWidth.ToString(), "\n"), "FullHeight:", settings_LastFullscreenHeight.ToString(), "\n"), "FullRefreshRate:", settings_LastFullscreenRefresh.ToString(), "\n"), "FullscreenType:", settings_LastFullscreenType.ToString(), "\n"), "VSync:", settings_Vsync.ToString(), "\n"), "Scribe:", settings_Scribe.ToString(), "\n"), "SkipIntro:", settings_SkipIntro.ToString(), "\n"), "UIScale:", settings_UIScale.ToString(Director.defaultCulture), "\n"), "Campaign:", settings_Progress_Campaign.ToString(), "\n"), "CampaignEco:", settings_Progress_EcoCampaign.ToString(), "\n"), "CampaignExtra1:", Settings_Progress_Extra1Campaign.ToString(), "\n"), "CampaignExtra2:", Settings_Progress_Extra2Campaign.ToString(), "\n"), "CampaignExtra3:", Settings_Progress_Extra3Campaign.ToString(), "\n"), "CampaignExtraEco:", Settings_Progress_ExtraEcoCampaign.ToString(), "\n"), "CampaignExtra4:", Settings_Progress_Extra4Campaign.ToString(), "\n"), "CampaignTrail2:", settings_Progress_Trail2.ToString(), "\n"), "CampaignTrail:", settings_Progress_Trail.ToString(), "\n"), "||SETTINGS||\n");
	}

	private static string GetScoresFileName()
	{
		return Application.persistentDataPath + "/scores.cfg";
	}

	public static void LoadScores()
	{
		string scoresFileName = GetScoresFileName();
		try
		{
			string[] array = File.ReadAllText(scoresFileName).Split("\n");
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(':');
				if (array2.Length != 2 && array2.Length != 3)
				{
					continue;
				}
				try
				{
					int bestscore = int.Parse(array2[1], Director.defaultCulture);
					Scores scores = new Scores
					{
						mapName = array2[0],
						bestscore = bestscore
					};
					int difficulty = 1000;
					if (array2.Length == 3)
					{
						try
						{
							difficulty = int.Parse(array2[2], Director.defaultCulture);
						}
						catch (Exception)
						{
						}
					}
					scores.difficulty = difficulty;
					ConfigSettings.scores[array2[0].ToLower()] = scores;
				}
				catch (Exception)
				{
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public static void SaveScores()
	{
		if (scores.Count <= 0)
		{
			return;
		}
		string scoresFileName = GetScoresFileName();
		string text = "";
		foreach (KeyValuePair<string, Scores> score in scores)
		{
			text = text + score.Value.mapName + ":" + score.Value.bestscore + ":" + score.Value.difficulty + "\n";
		}
		try
		{
			File.WriteAllText(scoresFileName, text);
		}
		catch (Exception)
		{
		}
	}

	public static bool MapCompleted(string mapname)
	{
		if (scores.TryGetValue(mapname.ToLower(), out var _))
		{
			return true;
		}
		return false;
	}

	public static bool MapCompleted(string mapname, ref int difficulty)
	{
		if (scores.TryGetValue(mapname.ToLower(), out var value))
		{
			difficulty = value.difficulty;
			return true;
		}
		return false;
	}

	public static int ManageScores(string mapname, int newScore, int newDifficulty = 1000)
	{
		if (scores.TryGetValue(mapname.ToLower(), out var value))
		{
			bool flag = false;
			int bestscore = value.bestscore;
			if (newScore > value.bestscore)
			{
				value.bestscore = newScore;
				flag = true;
			}
			if (newDifficulty != 1000 && (value.difficulty == 1000 || newDifficulty > value.difficulty))
			{
				value.difficulty = newDifficulty;
				flag = true;
			}
			if (flag)
			{
				SaveScores();
			}
			return bestscore;
		}
		Scores value2 = new Scores
		{
			mapName = mapname,
			bestscore = newScore,
			difficulty = newDifficulty
		};
		scores[mapname.ToLower()] = value2;
		SaveScores();
		return 0;
	}

	public static string GetUserWorkshopPath()
	{
		string text = Application.persistentDataPath + "\\UserWorkshopMaps";
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		return text;
	}

	public static string GetWorkshopUploadRootPath()
	{
		string text = Application.persistentDataPath + "\\WorkshopTemp";
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		return text;
	}

	public static string GetWorkshopUploadContentPath()
	{
		string text = GetWorkshopUploadRootPath() + "\\Content";
		if (Directory.Exists(text))
		{
			Directory.Delete(text, recursive: true);
		}
		Directory.CreateDirectory(text);
		return text;
	}
}
