using System;
using Stronghold1DE;

public class FileHeader
{
	public DateTime created;

	public DateTime written;

	public int crc;

	public int length;

	public int headerID;

	public int radarMapCompressedSize;

	public int missionTextType;

	public int missionTextNumber;

	public string ansiMissionText;

	public string unicodeMissionText;

	public string utf8MissionText;

	public bool showAlternateMissionTextForBriefing;

	public int xPlaySaveTime;

	public int xPlaySaveChecksum;

	public int xPlayAutoSave;

	public int mapType;

	public int[] mapKeeps;

	public int maxPlayers;

	public int scnMissionType;

	public int scnMissionSiegeOrInvasion;

	public int missionLockType;

	public string standAlone_filename = "";

	public string display_filename;

	public string fileName;

	public string filePath;

	public string sortFileName;

	public int isKingOfTheHill;

	public int isSiegeThat;

	public bool missionMap;

	public int mission_level;

	public bool userMap;

	public bool workshopMap;

	public bool builtinMap;

	public int trail;

	public int trailID;

	public int achFood;

	public int achWood;

	public int achWeapons;

	public static bool AllowLockedEditing;

	public int retrieveCRCChecks;

	public bool rowVisible;

	public bool isMapEditable()
	{
		if (AllowLockedEditing)
		{
			return true;
		}
		if (!missionMap)
		{
			return missionLockType == 0;
		}
		return false;
	}

	public string getDateString()
	{
		return written.ToShortDateString() + " " + written.ToShortTimeString();
	}

	public string getGameTypeString()
	{
		if (mission_level >= 0)
		{
			if (mission_level <= 21)
			{
				return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 13) + " " + mission_level;
			}
			if (mission_level < 40)
			{
				return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 14) + " " + (mission_level - 32);
			}
			if (mission_level > 80 && mission_level < 90)
			{
				return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_EXTRA_5, 0) + " " + (mission_level - 80);
			}
			int num = mission_level / 10 - 3;
			int num2 = mission_level % 10;
			return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 30) + " " + num + "-" + num2;
		}
		if (trail == 1)
		{
			return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_TRAIL_NAMES, 0) + " " + trailID;
		}
		if (trail == 2)
		{
			return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_TRAIL_NAMES, 40) + " " + trailID;
		}
		if (mapType == 1)
		{
			return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 9);
		}
		switch (scnMissionSiegeOrInvasion)
		{
		case 3:
			return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 4);
		case 2:
			return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 5);
		case 1:
			return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 6);
		case 0:
			if (GameData.Instance.siegeThat)
			{
				return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 8);
			}
			return Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 7);
		default:
			return "";
		}
	}

	public FileHeader()
	{
		mapKeeps = new int[5];
		headerID = 0;
	}
}
