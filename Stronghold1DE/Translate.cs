using System;
using System.Collections.Generic;
using System.IO;
using Noesis;

namespace Stronghold1DE;

public class Translate
{
	public static readonly DependencyProperty InstanceProperty = DependencyProperty.Register("Instance", typeof(Translate), typeof(Translate), new PropertyMetadata(Instance));

	private readonly string StrongholdTextFileNew = "Assets/Text/Stronghold.txt";

	private readonly string StrongholdTextMLBFileNew = "Assets/Text/stronghold_message_library.txt";

	private readonly string StrongholdTextFileExtra1 = "Assets/Text/Extra1.txt";

	private readonly string StrongholdTextFileExtra2 = "Assets/Text/Extra2.txt";

	private readonly string StrongholdTextFileExtra3 = "Assets/Text/Extra3.txt";

	private readonly string StrongholdTextFileExtra4 = "Assets/Text/Extra4.txt";

	private readonly string StrongholdTextFileExtra5 = "Assets/Text/Extra5.txt";

	public const string locale_english = "enus";

	public const string locale_german = "dede";

	public const string locale_french = "frfr";

	public const string locale_spanish = "eses";

	public const string locale_italian = "itit";

	public const string locale_polish = "plpl";

	public const string locale_russian = "ruru";

	public const string locale_portuguese = "ptbr";

	public const string locale_japanese = "jajp";

	public const string locale_korean = "kokr";

	public const string locale_simplified_chinese = "zhcn";

	public const string locale_traditional_chinese = "zhhk";

	public const string locale_czech = "cscz";

	public const string locale_turkish = "trtr";

	public const string locale_hungarian = "huhu";

	public const string locale_thai = "thth";

	public const string locale_ukrainian = "ukua";

	public const string locale_greek = "elgr";

	private const int maxTextSections = 250;

	private const int MAX_ML_MESSAGES = 1000;

	private string[] ML_message_text = new string[1000];

	public bool ExtraText_1_Loaded;

	public bool ExtraText_2_Loaded;

	public bool ExtraText_3_Loaded;

	public bool ExtraText_4_Loaded;

	public bool ExtraText_5_Loaded;

	private string[] SectionNames = new string[250]
	{
		"TEXT_COPYRIGHT", "TEXT_MONTHS", "TEXT_GOODS", "TEXT_POPULARITY_EFFECTS", "TEXT_STARTUP", "TEXT_MAINOPTIONS", "TEXT_LANGUAGE", "TEXT_FREE1_", "TEXT_BUBBLE_HELP_TEXT", "TEXT_BUBBLE_HELP_DATA",
		"TEXT_FEEDBACK", "TEXT_MAPEDIT", "TEXT_DEMOSCORE", "TEXT_FREE2_", "TEXT_REPORTS", "TEXT_FREE3_", "TEXT_FREE4_", "TEXT_FREE5_", "TEXT_IN_GENERAL_BUILDINGS", "TEXT_IN_KEEP",
		"TEXT_IN_INN", "TEXT_FREE6_", "TEXT_IN_BARRACKS", "TEXT_IN_GRANARY", "TEXT_IN_HOUSE", "TEXT_IN_WOODCUTTERS_HUT", "TEXT_IN_OXEN_BASE", "TEXT_IN_IRON_MINE", "TEXT_IN_PITCH_DIGGER", "TEXT_IN_HUNTERS_HUT",
		"TEXT_IN_GOODS_YARD", "TEXT_IN_ARMOURY", "TEXT_IN_FLETCHERS_WORKSHOP", "TEXT_IN_BLACKSMITHS_WORKSHOP", "TEXT_IN_POLETURNERS_WORKSHOP", "TEXT_IN_ARMOURERS_WORKSHOP", "TEXT_IN_TANNERS_WORKSHOP", "TEXT_IN_BAKERS_WORKSHOP", "TEXT_IN_BREWERS_WORKSHOP", "TEXT_IN_QUARRY",
		"TEXT_IN_QUARRYPILE", "TEXT_IN_HEALERS", "TEXT_IN_ENGINEERS_GUILD", "TEXT_IN_TUNNELLERS_GUILD", "TEXT_IN_TRADEPOST", "TEXT_IN_WELL", "TEXT_IN_OIL_SMELTER", "TEXT_IN_SIEGE_TENT", "TEXT_IN_WHEATFARM", "TEXT_IN_HOPSFARM",
		"TEXT_IN_APPLEFARM", "TEXT_IN_CATTLEFARM", "TEXT_IN_MILL", "TEXT_IN_STABLES", "TEXT_IN_CHURCH", "TEXT_IN_GATEHOUSE", "TEXT_IN_DRAWBRIDGE", "TEXT_IN_POSTERN_GATE", "TEXT_IN_TUNNEL_ENTERANCE", "TEXT_IN_CAMP_FIRE",
		"TEXT_IN_SIGNPOST", "TEXT_IN_KILLING_PIT", "TEXT_IN_CATAPULT", "TEXT_IN_TREBUCHET", "TEXT_IN_MANGONEL", "TEXT_IN_TOWER", "TEXT_IN_GALLOWS", "TEXT_IN_STOCKS", "TEXT_IN_WITCH_HOIST", "TEXT_IN_MAYPOLE",
		"TEXT_FREE7_", "TEXT_IN_TRAINING_GROUND", "TEXT_IN_GARDEN", "TEXT_FREE8_", "TEXT_GAME_OPTIONS", "TEXT_HELP", "TEXT_MULTIPLAYER_CONNECTION", "TEXT_PANEL_FEEDBACK", "TEXT_STRUCTURE_WAS", "TEXT_XPLAY_WAITING_ROOM",
		"TEXT_MISSION_BUTTONS", "TEXT_OBJECTIVES", "TEXT_REPORT_BUTTONS", "TEXT_PLAYER_DESC", "TEXT_PEASANT_NAMES", "TEXT_PEASANT_SURNAMES", "TEXT_UNIT_ACTIONS", "TEXT_MARRIAGE", "TEXT_CHIMP_NAMES", "TEXT_CHIMP_COMMENT",
		"TEXT_FREE9_", "TEXT_FREE10_", "TEXT_NEWMAP_TYPES_HELP", "TEXT_INSULTS", "TEXT_PREVIEW", "TEXT_TUTORIAL", "TEXT_TUTORIAL_BUTTONS", "TEXT_MAP_SCREEN", "TEXT_FREE11_", "TEXT_MISSION1_STORY",
		"TEXT_MISSION1_BRIEFING", "TEXT_MISSION1_OBJECTIVES", "TEXT_MISSION1_HINTS", "TEXT_MISSION2_STORY", "TEXT_MISSION2_BRIEFING", "TEXT_MISSION2_OBJECTIVES", "TEXT_MISSION2_HINTS", "TEXT_MISSION3_STORY", "TEXT_MISSION3_BRIEFING", "TEXT_MISSION3_OBJECTIVES",
		"TEXT_MISSION3_HINTS", "TEXT_MISSION4_STORY", "TEXT_MISSION4_BRIEFING", "TEXT_MISSION4_OBJECTIVES", "TEXT_MISSION4_HINTS", "TEXT_MISSION5_STORY", "TEXT_MISSION5_BRIEFING", "TEXT_MISSION5_OBJECTIVES", "TEXT_MISSION5_HINTS", "TEXT_MISSION6_STORY",
		"TEXT_MISSION6_BRIEFING", "TEXT_MISSION6_OBJECTIVE", "TEXT_MISSION6_HINTS", "TEXT_MISSION7_STORY", "TEXT_MISSION7_BRIEFING", "TEXT_MISSION7_OBJECTIVES", "TEXT_MISSION7_HINTS", "TEXT_MISSION8_STORY", "TEXT_MISSION8_BRIEFING", "TEXT_MISSION8_OBJECTIVES",
		"TEXT_MISSION8_HINTS", "TEXT_MISSION9_STORY", "TEXT_MISSION9_BRIEFING", "TEXT_MISSION9_OBJECTIVES", "TEXT_MISSION9_HINTS", "TEXT_MISSION10_STORY", "TEXT_MISSION10_BRIEFING", "TEXT_MISSION10_OBJECTIVES", "TEXT_MISSION10_HINTS", "TEXT_MISSION11_STORY",
		"TEXT_MISSION11_BRIEFING", "TEXT_MISSION11_OBJECTIVES", "TEXT_MISSION11_HINTS", "TEXT_MISSION12_STORY", "TEXT_MISSION12_BRIEFING", "TEXT_MISSION12_OBJECTIVES", "TEXT_MISSION12_HINTS", "TEXT_MISSION13_STORY", "TEXT_MISSION13_BRIEFING", "TEXT_MISSION13_OBJECTIVES",
		"TEXT_MISSION13_HINTS", "TEXT_MISSION14_STORY", "TEXT_MISSION14_BRIEFING", "TEXT_MISSION14_OBJECTIVES", "TEXT_MISSION14_HINTS", "TEXT_MISSION15_STORY", "TEXT_MISSION15_BRIEFING", "TEXT_MISSION15_OBJECTIVES", "TEXT_MISSION15_HINTS", "TEXT_MISSION16_STORY",
		"TEXT_MISSION16_BRIEFING", "TEXT_MISSION16_OBJECTIVES", "TEXT_MISSION16_HINTS", "TEXT_MISSION17_STORY", "TEXT_MISSION17_BRIEFING", "TEXT_MISSION17_OBJECTIVES", "TEXT_MISSION17_HINTS", "TEXT_MISSION18_STORY", "TEXT_MISSION18_BRIEFING", "TEXT_MISSION18_OBJECTIVES",
		"TEXT_MISSION18_HINTS", "TEXT_MISSION19_STORY", "TEXT_MISSION19_BRIEFING", "TEXT_MISSION19_OBJECTIVES", "TEXT_MISSION19_HINTS", "TEXT_MISSION20_STORY", "TEXT_MISSION20_BRIEFING", "TEXT_MISSION20_OBJECTIVES", "TEXT_MISSION20_HINTS", "TEXT_MISSION21_STORY",
		"TEXT_MISSION21_BRIEFING", "TEXT_MISSION21_OBJECTIVES", "TEXT_MISSION21_HINTS", "TEXT_FREE12_", "TEXT_MISSION28_STORY", "TEXT_MISSION29_STORY", "TEXT_FREE13_", "TEXT_FREE14_", "TEXT_FREE15_", "TEXT_DEMO_BRIEFINGS",
		"TEXT_HINTS", "TEXT_ECO1_HINTS", "TEXT_ECO2_HINTS", "TEXT_ECO3_HINTS", "TEXT_ECO4_HINTS", "TEXT_ECO5_HINTS", "TEXT_ECO_MISSION_BRIEFINGS", "TEXT_MISSION_NAMES", "TEXT_PREATTACK", "TEXT_SCENARIO",
		"TEXT_TRADER_NAMES", "TEXT_ACTION", "TEXT_IN_CESS_PIT", "TEXT_IN_BURNING_STAKE", "TEXT_IN_GIBBET", "TEXT_IN_DUNGEON", "TEXT_IN_STRETCHING_RACK", "TEXT_IN_FLOGGING_RACK", "TEXT_IN_CHOPPING_BLOCK", "TEXT_IN_DUNKING_STOOL",
		"TEXT_IN_DOG_CAGE", "TEXT_IN_STATUE", "TEXT_IN_SHRINE", "TEXT_IN_BEEHIVE", "TEXT_IN_DANCING_BEAR", "TEXT_IN_POND", "TEXT_IN_BEAR_CAVE", "TEXT_FREE16_", "TEXT_FREE17_", "TEXT_MAP_NAMES",
		"TEXT_FREE18_", "TEXT_FREE19_", "TEXT_FREE20_", "TEXT_FREE21_", "TEXT_FREE22_", "TEXT_FREE23_", "TEXT_FREE24_", "TEXT_FREE25_", "TEXT_FREE26_", "TEXT_MP_VERSION_CONTROL",
		"TEXT_NEW_TEXT", "TEXT_HOT_KEYS", "TEXT_NEW_DEMO", "TEXT_NEW_TEXT2", "TEXT_TRAIL_NAMES", "TEXT_BUILDING_DESCRIPTIONS", "TEXT_CREDITS", "TEXT_SUBTITLES", "TEXT_ECOBRIEFINGS", "TEXT_FREE36_",
		"TEXT_FREE37_", "TEXT_FREE38_", "TEXT_FREE39_", "TEXT_FREE40_", "TEXT_FREE41_", "TEXT_FREE42_", "TEXT_FREE43_", "TEXT_FREE44_", "TEXT_FREE45_", "TEXT_FREE46_"
	};

	private Dictionary<string, int> builtInMapNameTranslations;

	public static Translate Instance { get; } = new Translate();


	public Dictionary<string, string> GameTexts { get; set; }

	public Translate()
	{
		SetupGameText();
		LoadTextFileNew(StrongholdTextFileNew);
		ExtraText_1_Loaded = LoadExtraText(StrongholdTextFileExtra1, "TEXT_EXTRA_1");
		ExtraText_2_Loaded = LoadExtraText(StrongholdTextFileExtra2, "TEXT_EXTRA_2");
		ExtraText_3_Loaded = LoadExtraText(StrongholdTextFileExtra3, "TEXT_EXTRA_3");
		ExtraText_4_Loaded = LoadExtraText(StrongholdTextFileExtra4, "TEXT_EXTRA_4");
		ExtraText_5_Loaded = LoadExtraText(StrongholdTextFileExtra5, "TEXT_EXTRA_5");
		LoadTextMLBFFileNew(StrongholdTextMLBFileNew);
	}

	private void SetupGameText()
	{
		GameTexts = new Dictionary<string, string>();
	}

	public string lookUpText(string index)
	{
		if (index == "")
		{
			return index;
		}
		return GameTexts[index];
	}

	public string lookUpText(string sectionString, int index)
	{
		index++;
		string key = sectionString + "_" + index.ToString("D3");
		return GameTexts[key];
	}

	public string lookUpText(Enums.eTextSections section, Enums.eTextValues index)
	{
		return lookUpText(section, (int)index);
	}

	public string lookUpText(Enums.eTextSections section, int index)
	{
		index++;
		string key = section.ToString() + "_" + index.ToString("D3");
		return GameTexts[key];
	}

	private void CONVERT_FILENAME(string mapName, int textID)
	{
		builtInMapNameTranslations[mapName.ToLowerInvariant()] = textID;
	}

	public string translateMapNames(string userMapName)
	{
		if (builtInMapNameTranslations == null)
		{
			builtInMapNameTranslations = new Dictionary<string, int>();
			CONVERT_FILENAME("8 Player Mayhem", 1);
			CONVERT_FILENAME("8 Player - no walls", 2);
			CONVERT_FILENAME("A request from the King", 21);
			CONVERT_FILENAME("Blessing the Peasants", 24);
			CONVERT_FILENAME("Butterfly Island", 8);
			CONVERT_FILENAME("Castell Y Bere", 26);
			CONVERT_FILENAME("Camelot", 35);
			CONVERT_FILENAME("Calanais", 43);
			CONVERT_FILENAME("Defending the homeland", 20);
			CONVERT_FILENAME("Desperate Measures", 44);
			CONVERT_FILENAME("Edinburgh", 36);
			CONVERT_FILENAME("Fork in the River", 11);
			CONVERT_FILENAME("Fernhaven", 13);
			CONVERT_FILENAME("Grasslands", 12);
			CONVERT_FILENAME("Gluecksburg", 27);
			CONVERT_FILENAME("Guadamur", 28);
			CONVERT_FILENAME("Heidelberg", 29);
			CONVERT_FILENAME("Harlech", 37);
			CONVERT_FILENAME("Haut-Koenigsbourg", 38);
			CONVERT_FILENAME("Javier", 31);
			CONVERT_FILENAME("King of the Hill", 9);
			CONVERT_FILENAME("Koblenz Stolzanfels", 39);
			CONVERT_FILENAME("Leeds", 30);
			CONVERT_FILENAME("Need an Ally", 3);
			CONVERT_FILENAME("Mountain Fortress", 4);
			CONVERT_FILENAME("Monteriggioni", 32);
			CONVERT_FILENAME("Mordred's Fortress", 40);
			CONVERT_FILENAME("Stranglehold", 45);
			CONVERT_FILENAME("The Fat Pig (oink, oink)", 17);
			CONVERT_FILENAME("The four castles", 6);
			CONVERT_FILENAME("The Forest", 23);
			CONVERT_FILENAME("The Emerging City", 25);
			CONVERT_FILENAME("There be witches!", 48);
			CONVERT_FILENAME("The tactician", 5);
			CONVERT_FILENAME("The Tyrant", 22);
			CONVERT_FILENAME("The Island Castle", 10);
			CONVERT_FILENAME("The Rat's Revenge", 15);
			CONVERT_FILENAME("Tower of London", 42);
			CONVERT_FILENAME("The Vipers Nest", 16);
			CONVERT_FILENAME("The weak, the bad, the fat and the slippery", 19);
			CONVERT_FILENAME("The Wolves", 47);
			CONVERT_FILENAME("Tintagel", 41);
			CONVERT_FILENAME("The Gauntlet", 46);
			CONVERT_FILENAME("Wall to Wall", 7);
			CONVERT_FILENAME("Waterfall Valley", 14);
			CONVERT_FILENAME("Who's afraid of the big bad wolf", 18);
			CONVERT_FILENAME("Windsor 1070", 33);
			CONVERT_FILENAME("Wartburg", 34);
			CONVERT_FILENAME("User Created Map - 2 player Map", 49);
			CONVERT_FILENAME("User Created Map - Asgard", 50);
			CONVERT_FILENAME("User Created Map - Asgard II - Wolf Hunt", 51);
			CONVERT_FILENAME("User Created Map - Bridge Across the River", 52);
			CONVERT_FILENAME("User Created Map - Bruckberg", 53);
			CONVERT_FILENAME("User Created Map - Castillo de Loarre Siege", 54);
			CONVERT_FILENAME("User Created Map - Castle Falkenstein", 55);
			CONVERT_FILENAME("User Created Map - Castle Kreuzberg", 56);
			CONVERT_FILENAME("User Created Map - Castle on fire", 57);
			CONVERT_FILENAME("User Created Map - Coast Castle", 58);
			CONVERT_FILENAME("User Created Map - Crater Castle", 59);
			CONVERT_FILENAME("User Created Map - Crusader", 60);
			CONVERT_FILENAME("User Created Map - Drei Gleichen", 61);
			CONVERT_FILENAME("User Created Map - Forest Hill", 62);
			CONVERT_FILENAME("User Created Map - Fougeres Castle Inv", 63);
			CONVERT_FILENAME("User Created Map - Fougeres Castle Siege", 64);
			CONVERT_FILENAME("User Created Map - Hochstrohgau Castle", 65);
			CONVERT_FILENAME("User Created Map - Hohensalzburg", 66);
			CONVERT_FILENAME("User Created Map - Hohenzollern Siege", 67);
			CONVERT_FILENAME("User Created Map - Island Warfare", 68);
			CONVERT_FILENAME("User Created Map - Pasture", 69);
			CONVERT_FILENAME("User Created Map - Peninsular Building", 70);
			CONVERT_FILENAME("User Created Map - Revenge", 71);
			CONVERT_FILENAME("User Created Map - Rumania", 72);
			CONVERT_FILENAME("User Created Map - The Dam", 73);
			CONVERT_FILENAME("User Created Map - Two Valleys", 74);
			CONVERT_FILENAME("User Created Map - Wooden Toys", 75);
			CONVERT_FILENAME("dunnottar", 1001);
			CONVERT_FILENAME("warkworth", 1002);
			CONVERT_FILENAME("pembroke", 1003);
			CONVERT_FILENAME("warwick", 1004);
			CONVERT_FILENAME("bodiam", 1005);
			CONVERT_FILENAME("hastings", 1006);
			CONVERT_FILENAME("chateau gaillard", 1007);
			CONVERT_FILENAME("chateau de coucy", 1008);
			CONVERT_FILENAME("marksburg", 1009);
			CONVERT_FILENAME("heuneburg", 1010);
			CONVERT_FILENAME("close call", 1025);
			CONVERT_FILENAME("the watchtower", 1027);
			CONVERT_FILENAME("pits of hell", 1029);
			CONVERT_FILENAME("dark forest", 1031);
			CONVERT_FILENAME("forest", 1033);
			CONVERT_FILENAME("snake island", 1035);
			CONVERT_FILENAME("vile gulch", 1037);
			CONVERT_FILENAME("castillo de coca", 1041);
			CONVERT_FILENAME("heidelberg_nobletrail", 11042);
			CONVERT_FILENAME("fougeres", 1043);
			CONVERT_FILENAME("biskupin", 1044);
			CONVERT_FILENAME("malbork", 1045);
			CONVERT_FILENAME("monteriggioni_nobletrail", 11046);
			CONVERT_FILENAME("koblenz stolzanfels", 1047);
			CONVERT_FILENAME("diósgyőr", 1048);
			CONVERT_FILENAME("fenis", 1049);
			CONVERT_FILENAME("niedzica", 1050);
			CONVERT_FILENAME("4 rivers", 1061);
			CONVERT_FILENAME("sleeping volcano", 1063);
			CONVERT_FILENAME("ogrodzieniec", 1065);
			CONVERT_FILENAME("forlorn hope", 1067);
			CONVERT_FILENAME("three's a crowd", 1069);
			CONVERT_FILENAME("downstream", 1073);
		}
		if (builtInMapNameTranslations.TryGetValue(userMapName.ToLowerInvariant(), out var value))
		{
			if (value >= 10000)
			{
				return lookUpText(Enums.eTextSections.TEXT_TRAIL_NAMES, value - 11000) + " (" + lookUpText(Enums.eTextSections.TEXT_TRAIL_NAMES, 40) + ")";
			}
			if (value >= 1000)
			{
				return lookUpText(Enums.eTextSections.TEXT_TRAIL_NAMES, value - 1000);
			}
			return lookUpText(Enums.eTextSections.TEXT_MAP_NAMES, value);
		}
		return userMapName;
	}

	public string getMessageLibraryText(int index)
	{
		if (index >= 0 && index < 1000)
		{
			return ML_message_text[index];
		}
		return "";
	}

	public bool LoadTextFileNew(string filePath)
	{
		try
		{
			string[] array = File.ReadAllLines(filePath);
			int num = 0;
			int i = 0;
			int num2 = 0;
			string text = "";
			for (; i < array.Length; i++)
			{
				if (array[i].ToLowerInvariant() == ">>textstart")
				{
					i++;
					break;
				}
			}
			while (i < array.Length)
			{
				if (array[i] == "")
				{
					i++;
					continue;
				}
				if (array[i].Length >= 3 && array[i][0] == '>' && array[i][1] == '>')
				{
					num++;
					if (num > 250)
					{
						break;
					}
					i++;
					num2 = 0;
					text = SectionNames[num - 1];
					continue;
				}
				string text2 = array[i];
				text2 = text2.Replace("\\n", "\n");
				string text3 = text + "_" + (num2 + 1).ToString("D3");
				string value = text2;
				if (text3 == "TEXT_HOT_KEYS_113")
				{
					text3 = "TEXT_HOT_KEYS_116";
				}
				GameTexts.Add(text3, value);
				switch (text3.ToLower())
				{
				case "text_new_text_024":
				{
					string key13 = "TEXT_SCENARIO_195";
					GameTexts[key13] = text2;
					key13 = "TEXT_SCENARIO_197";
					GameTexts[key13] = text2;
					key13 = "TEXT_SCENARIO_198";
					GameTexts[key13] = text2;
					key13 = "TEXT_SCENARIO_199";
					GameTexts[key13] = text2;
					key13 = "TEXT_SCENARIO_201";
					GameTexts[key13] = text2;
					break;
				}
				case "text_new_text_026":
				{
					string key12 = "TEXT_SCENARIO_181";
					GameTexts[key12] = text2;
					break;
				}
				case "text_new_text_027":
				{
					string key11 = "TEXT_SCENARIO_182";
					GameTexts[key11] = text2;
					break;
				}
				case "text_new_text2_010":
				{
					string key10 = "TEXT_SCENARIO_183";
					GameTexts[key10] = text2;
					break;
				}
				case "text_new_text2_046":
				{
					string key9 = "TEXT_SCENARIO_184";
					GameTexts[key9] = text2;
					break;
				}
				case "text_new_text2_047":
				{
					string key8 = "TEXT_SCENARIO_185";
					GameTexts[key8] = text2;
					break;
				}
				case "text_new_text_090":
				{
					string key7 = "TEXT_SCENARIO_196";
					GameTexts[key7] = text2;
					break;
				}
				case "text_new_text2_007":
				{
					string key6 = "TEXT_SCENARIO_200";
					GameTexts[key6] = text2;
					break;
				}
				case "text_new_text2_148":
				{
					string key5 = "TEXT_SCENARIO_203";
					GameTexts[key5] = text2;
					break;
				}
				case "text_new_text2_149":
				{
					string key4 = "TEXT_SCENARIO_204";
					GameTexts[key4] = text2;
					break;
				}
				case "text_bubble_help_text_288":
				{
					string key3 = "TEXT_HOT_KEYS_113";
					GameTexts[key3] = text2;
					break;
				}
				case "text_bubble_help_text_289":
				{
					string key2 = "TEXT_HOT_KEYS_114";
					GameTexts[key2] = text2;
					break;
				}
				case "text_bubble_help_text_290":
				{
					string key = "TEXT_HOT_KEYS_115";
					GameTexts[key] = text2;
					break;
				}
				}
				num2++;
				i++;
			}
			GameTexts["TEXT_SCENARIO_202"] = GameTexts["TEXT_SCENARIO_115"];
			MainViewModel.Instance.SetLocalisedLayout(GameTexts["TEXT_LANGUAGE_002"]);
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	private bool LoadExtraText(string filePath, string tag)
	{
		try
		{
			string[] array = File.ReadAllLines(filePath);
			int num = 0;
			while (num < array.Length)
			{
				string key = tag + "_" + (num + 1).ToString("D3");
				string text = array[num];
				text = text.Replace("\\n", "\n");
				GameTexts.Add(key, text);
				num++;
				if (num == 70)
				{
					break;
				}
			}
			num = 70;
			string key2 = tag + "_" + (num + 1).ToString("D3");
			string value = StripMission(array[5]);
			GameTexts.Add(key2, value);
			num++;
			key2 = tag + "_" + (num + 1).ToString("D3");
			value = StripMission(array[8]);
			GameTexts.Add(key2, value);
			num++;
			key2 = tag + "_" + (num + 1).ToString("D3");
			value = StripMission(array[11]);
			GameTexts.Add(key2, value);
			num++;
			key2 = tag + "_" + (num + 1).ToString("D3");
			value = StripMission(array[14]);
			GameTexts.Add(key2, value);
			num++;
			key2 = tag + "_" + (num + 1).ToString("D3");
			value = StripMission(array[17]);
			GameTexts.Add(key2, value);
			num++;
			key2 = tag + "_" + (num + 1).ToString("D3");
			value = StripMission(array[20]);
			GameTexts.Add(key2, value);
			num++;
			key2 = tag + "_" + (num + 1).ToString("D3");
			value = StripMission(array[23]);
			GameTexts.Add(key2, value);
			num++;
			return true;
		}
		catch (Exception)
		{
			string value2 = " ";
			for (int i = 0; i < 80; i++)
			{
				string key3 = tag + "_" + (i + 1).ToString("D3");
				GameTexts.Add(key3, value2);
			}
			return false;
		}
	}

	private string StripMission(string str)
	{
		int num = str.IndexOf(':');
		if (num > 0)
		{
			string text = str.Substring(num + 1).TrimStart();
			if (text.Length > 2 && (FatControler.french || FatControler.spanish || FatControler.italian))
			{
				return $"{char.ToUpper(text[0])}{text[1..]}";
			}
			return text;
		}
		num = str.IndexOf('：');
		if (num > 0)
		{
			return str.Substring(num + 1).TrimStart();
		}
		return str;
	}

	public bool LoadTextMLBFFileNew(string filePath)
	{
		try
		{
			string[] array = File.ReadAllLines(filePath);
			int num = -1;
			int num2 = 0;
			while (num2 < array.Length)
			{
				if (array[num2] == "")
				{
					num2++;
				}
				else if (array[num2].Length >= 3 && array[num2][0] == '>' && array[num2][1] == '>')
				{
					num++;
					num2++;
					string text = array[num2];
					text = text.Replace("\\n", "\n");
					ML_message_text[num] = text;
					num2++;
				}
				else
				{
					num2++;
				}
			}
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}
}
