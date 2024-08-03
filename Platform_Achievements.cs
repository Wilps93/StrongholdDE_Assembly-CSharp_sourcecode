using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class Platform_Achievements
{
	private static readonly Platform_Achievements instance;

	private const int VersionNumber = 5;

	private int STAT_Units;

	private int STAT_Bears;

	public static Platform_Achievements Instance => instance;

	static Platform_Achievements()
	{
		instance = new Platform_Achievements();
	}

	private Platform_Achievements()
	{
	}

	public Dictionary<Enums.Achievements, int> LoadAchievements()
	{
		Dictionary<Enums.Achievements, int> dictionary = new Dictionary<Enums.Achievements, int>();
		if (SteamManager.Initialized)
		{
			SteamUserStats.RequestCurrentStats();
			SteamUserStats.GetStat("stat_Units_Killed", out int pData);
			SteamUserStats.GetStat("stat_Bears_Killed", out int pData2);
			dictionary[Enums.Achievements.Complete_Tutorial] = getAchievementValue("ACHIEVEMENT_Tut_Complete");
			dictionary[Enums.Achievements.Complete_Campaign_Act_1] = getAchievementValue("ACHIEVEMENT_Campaign_Act_1");
			dictionary[Enums.Achievements.Complete_Campaign_Act_2] = getAchievementValue("ACHIEVEMENT_Campaign_Act_2");
			dictionary[Enums.Achievements.Complete_Campaign_Act_3] = getAchievementValue("ACHIEVEMENT_Campaign_Act_3");
			dictionary[Enums.Achievements.Complete_Campaign_Act_4] = getAchievementValue("ACHIEVEMENT_Campaign_Act_4");
			dictionary[Enums.Achievements.Complete_Campaign_Eco] = getAchievementValue("ACHIEVEMENT_Eco_Campaign");
			dictionary[Enums.Achievements.Complete_Extra_Act_1] = getAchievementValue("ACHIEVEMENT_Extra_Act_1");
			dictionary[Enums.Achievements.Complete_Extra_Act_2] = getAchievementValue("ACHIEVEMENT_Extra_Act_2");
			dictionary[Enums.Achievements.Complete_Extra_Act_3] = getAchievementValue("ACHIEVEMENT_Extra_Act_3");
			dictionary[Enums.Achievements.Complete_Extra_Eco] = getAchievementValue("ACHIEVEMENT_Home_Fires");
			dictionary[Enums.Achievements.Complete_Extra_Act_4] = getAchievementValue("ACHIEVEMENT_Extra_Act_4");
			dictionary[Enums.Achievements.Complete_Noble_Trail] = getAchievementValue("ACHIEVEMENT_Noble_Trail");
			dictionary[Enums.Achievements.Win_Multiplier_Any] = getAchievementValue("ACHIEVEMENT_Win_Multiplayer");
			dictionary[Enums.Achievements.Win_Multiplier_Vs_7] = getAchievementValue("ACHIEVEMENT_Win_Multiplayer_v7");
			dictionary[Enums.Achievements.Complete_Siege_As_Defender] = getAchievementValue("ACHIEVEMENT_Complete_Siege_Defender");
			dictionary[Enums.Achievements.Complete_Siege_As_Attacker] = getAchievementValue("ACHIEVEMENT_Complete_Siege_Attacker");
			dictionary[Enums.Achievements.Complete_An_Invasion_On_Very_Hard] = getAchievementValue("ACHIEVEMENT_Complete_Invasion_VeryHard");
			dictionary[Enums.Achievements.Complete_An_Eco_Mission_On_Very_Hard] = getAchievementValue("ACHIEVEMENT_Complete_Economics_VeryHard");
			dictionary[Enums.Achievements.Map_Uploaded_To_Workshop] = getAchievementValue("ACHIEVEMENT_Upload_Map");
			dictionary[Enums.Achievements.Complete_Historical_Trail] = getAchievementValue("ACHIEVEMENT_Historical_Trail");
			dictionary[Enums.Achievements.Kill_The_Jester] = getAchievementValue("ACHIEVEMENT_Kill_Jester");
			dictionary[Enums.Achievements.Store_1000_Food] = getAchievementValue("ACHIEVEMENT_Store_Food_1000");
			dictionary[Enums.Achievements.Store_10000_Wood] = getAchievementValue("ACHIEVEMENT_Store_Wood_10000");
			dictionary[Enums.Achievements.Store_1000_Weapons] = getAchievementValue("ACHIEVEMENT_Store_Weapons_1000");
			dictionary[Enums.Achievements.Amass_10000_Gold] = getAchievementValue("ACHIEVEMENT_Amass_Gold_10000");
			dictionary[Enums.Achievements.Population_300] = getAchievementValue("ACHIEVEMENT_Population_300");
			dictionary[Enums.Achievements.Kill_100_Bears] = getAchievementValue("ACHIEVEMENT_Kill_100_Bears", pData2);
			dictionary[Enums.Achievements.Kill_Units_10k] = getAchievementValue("ACHIEVEMENT_Kill_10k_Units", pData);
			dictionary[Enums.Achievements.Kill_Units_100k] = getAchievementValue("ACHIEVEMENT_Kill_100k_Units", pData);
			dictionary[Enums.Achievements.Kill_Units_1M] = getAchievementValue("ACHIEVEMENT_Kill_1M_Units", pData);
			STAT_Units = pData;
			STAT_Bears = pData2;
		}
		return dictionary;
	}

	private int getAchievementValue(string name)
	{
		if (SteamUserStats.GetAchievement(name, out var pbAchieved))
		{
			if (pbAchieved)
			{
				return -1;
			}
		}
		else
		{
			Debug.Log("Unknown Achievement on Steam : " + name);
		}
		return 0;
	}

	private int getAchievementValue(string name, int statValue)
	{
		if (SteamUserStats.GetAchievement(name, out var pbAchieved))
		{
			if (pbAchieved)
			{
				return -1;
			}
			return statValue;
		}
		Debug.Log("Unknown Achievement on Steam : " + name);
		return 0;
	}

	public void addStat(Enums.AchievementStat stat, int value)
	{
		if (SteamManager.Initialized)
		{
			switch (stat)
			{
			case Enums.AchievementStat.UnitsKilled:
				STAT_Units += value;
				SteamUserStats.SetStat("stat_Units_Killed", STAT_Units);
				SteamUserStats.StoreStats();
				break;
			case Enums.AchievementStat.BearsKilled:
				STAT_Bears += value;
				SteamUserStats.SetStat("stat_Bears_Killed", STAT_Bears);
				SteamUserStats.StoreStats();
				break;
			}
		}
	}

	public void SetAchievementComplete(Enums.Achievements achType)
	{
		if (SteamManager.Initialized)
		{
			switch (achType)
			{
			case Enums.Achievements.Complete_Tutorial:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Tut_Complete");
				break;
			case Enums.Achievements.Complete_Campaign_Act_1:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Campaign_Act_1");
				break;
			case Enums.Achievements.Complete_Campaign_Act_2:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Campaign_Act_2");
				break;
			case Enums.Achievements.Complete_Campaign_Act_3:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Campaign_Act_3");
				break;
			case Enums.Achievements.Complete_Campaign_Act_4:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Campaign_Act_4");
				break;
			case Enums.Achievements.Complete_Campaign_Eco:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Eco_Campaign");
				break;
			case Enums.Achievements.Complete_Extra_Act_1:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Extra_Act_1");
				break;
			case Enums.Achievements.Complete_Extra_Act_2:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Extra_Act_2");
				break;
			case Enums.Achievements.Complete_Extra_Act_3:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Extra_Act_3");
				break;
			case Enums.Achievements.Complete_Extra_Eco:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Home_Fires");
				break;
			case Enums.Achievements.Complete_Extra_Act_4:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Extra_Act_4");
				break;
			case Enums.Achievements.Complete_Noble_Trail:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Noble_Trail");
				break;
			case Enums.Achievements.Win_Multiplier_Any:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Win_Multiplayer");
				break;
			case Enums.Achievements.Win_Multiplier_Vs_7:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Win_Multiplayer_v7");
				break;
			case Enums.Achievements.Kill_Units_10k:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Kill_10k_Units");
				break;
			case Enums.Achievements.Kill_Units_100k:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Kill_100k_Units");
				break;
			case Enums.Achievements.Kill_Units_1M:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Kill_1M_Units");
				break;
			case Enums.Achievements.Complete_Siege_As_Defender:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Complete_Siege_Defender");
				break;
			case Enums.Achievements.Complete_Siege_As_Attacker:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Complete_Siege_Attacker");
				break;
			case Enums.Achievements.Complete_An_Invasion_On_Very_Hard:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Complete_Invasion_VeryHard");
				break;
			case Enums.Achievements.Complete_An_Eco_Mission_On_Very_Hard:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Complete_Economics_VeryHard");
				break;
			case Enums.Achievements.Map_Uploaded_To_Workshop:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Upload_Map");
				break;
			case Enums.Achievements.Complete_Historical_Trail:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Historical_Trail");
				break;
			case Enums.Achievements.Kill_The_Jester:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Kill_Jester");
				break;
			case Enums.Achievements.Kill_100_Bears:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Kill_100_Bears");
				break;
			case Enums.Achievements.Store_1000_Food:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Store_Food_1000");
				break;
			case Enums.Achievements.Store_1000_Weapons:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Store_Weapons_1000");
				break;
			case Enums.Achievements.Store_10000_Wood:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Store_Wood_10000");
				break;
			case Enums.Achievements.Amass_10000_Gold:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Amass_Gold_10000");
				break;
			case Enums.Achievements.Population_300:
				SteamUserStats.SetAchievement("ACHIEVEMENT_Population_300");
				break;
			}
			SteamUserStats.StoreStats();
		}
	}
}
