using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Stronghold1DE;
using UnityNativeTool;

public class EngineInterface
{
	public struct LoadMapReturnData
	{
		public int errorCode;

		public int mapSize;

		public int mapRotation;

		public int mapRotationCentreX;

		public int mapRotationCentreY;

		public int siege_or_invasion;

		public int siege_that;

		public int multiplayerMap;

		public int multiplayerKOTHMap;

		public int game_type;

		public int mission_level;

		public int textID;

		public int difficulty_level;

		public int playerID;
	}

	public struct MultiplayerSetupTransferData
	{
		public int trading0;

		public int trading1;

		public int trading2;

		public int trading3;

		public int starting_gold;

		public int starting_popularity;

		public int starting_gamespeed;

		public int starting_goods_level;

		public int starting_troops_level;

		public int starting_keep_type;

		public int king_of_the_hill_points;

		public int win_condition;

		public int allow_troop_gold;

		public int allow_autotrading;

		public int no_knockdown_walls;

		public int no_ingame_alliances;

		public int autosave;

		public int peacetime;

		public int starting_goods0;

		public int starting_goods1;

		public int starting_goods2;

		public int starting_goods3;

		public int starting_goods4;

		public int starting_goods5;

		public int starting_goods6;

		public int starting_goods7;

		public int starting_goods8;

		public int starting_goods9;

		public int starting_goods10;

		public int starting_goods11;

		public int starting_goods12;

		public int starting_goods13;

		public int starting_goods14;

		public int starting_goods15;

		public int starting_goods16;

		public int starting_goods17;

		public int starting_goods18;

		public int starting_goods19;

		public int starting_troops0;

		public int starting_troops1;

		public int starting_troops2;

		public int starting_troops3;

		public int starting_troops4;

		public int starting_troops5;

		public int starting_troops6;

		public int starting_troops7;

		public int starting_troops8;

		public int starting_troops9;
	}

	public class MultiplayerSetupData
	{
		public int[] trading = new int[4];

		public int starting_gold;

		public int starting_popularity;

		public int starting_gamespeed;

		public int starting_goods_level;

		public int starting_troops_level;

		public int starting_keep_type;

		public int king_of_the_hill_points;

		public int win_condition;

		public int allow_troop_gold;

		public int allow_autotrading;

		public int no_knockdown_walls;

		public int no_ingame_alliances;

		public int autosave;

		public int peacetime;

		public int[] starting_goods = new int[20];

		public int[] starting_troops = new int[10];

		public bool FromString(string str)
		{
			bool result = ToString() != str;
			string[] array = str.Split(",");
			int num = 0;
			starting_gold = EditorDirector.getIntFromString(array[num++]);
			starting_popularity = EditorDirector.getIntFromString(array[num++]);
			starting_gamespeed = EditorDirector.getIntFromString(array[num++]);
			starting_goods_level = EditorDirector.getIntFromString(array[num++]);
			starting_troops_level = EditorDirector.getIntFromString(array[num++]);
			starting_keep_type = EditorDirector.getIntFromString(array[num++]);
			king_of_the_hill_points = EditorDirector.getIntFromString(array[num++]);
			win_condition = EditorDirector.getIntFromString(array[num++]);
			allow_troop_gold = EditorDirector.getIntFromString(array[num++]);
			allow_autotrading = EditorDirector.getIntFromString(array[num++]);
			no_knockdown_walls = EditorDirector.getIntFromString(array[num++]);
			no_ingame_alliances = EditorDirector.getIntFromString(array[num++]);
			autosave = EditorDirector.getIntFromString(array[num++]);
			peacetime = EditorDirector.getIntFromString(array[num++]);
			for (int i = 0; i < 4; i++)
			{
				trading[i] = EditorDirector.getIntFromString(array[num++]);
			}
			for (int j = 0; j < 20; j++)
			{
				starting_goods[j] = EditorDirector.getIntFromString(array[num++]);
			}
			for (int k = 0; k < 10; k++)
			{
				starting_troops[k] = EditorDirector.getIntFromString(array[num++]);
			}
			return result;
		}

		public override string ToString()
		{
			string text = starting_gold + "," + starting_popularity + "," + starting_gamespeed + "," + starting_goods_level + "," + starting_troops_level + "," + starting_keep_type + "," + king_of_the_hill_points + "," + win_condition + "," + allow_troop_gold + "," + allow_autotrading + "," + no_knockdown_walls + "," + no_ingame_alliances + "," + autosave + "," + peacetime + ",";
			for (int i = 0; i < 4; i++)
			{
				text = text + trading[i] + ",";
			}
			for (int j = 0; j < 20; j++)
			{
				text = text + starting_goods[j] + ",";
			}
			for (int k = 0; k < 10; k++)
			{
				text = text + starting_troops[k] + ",";
			}
			return text;
		}
	}

	[StructLayout(LayoutKind.Explicit)]
	private struct ScenarioOverviewReturnData
	{
		[FieldOffset(0)]
		public int startMonth;

		[FieldOffset(4)]
		public int startYear;

		[FieldOffset(8)]
		public int numEntries;

		[FieldOffset(12)]
		public unsafe fixed int month[200];

		[FieldOffset(812)]
		public unsafe fixed int year[200];

		[FieldOffset(1612)]
		public unsafe fixed int entryType[200];

		[FieldOffset(2412)]
		public unsafe fixed int data1[200];

		[FieldOffset(3212)]
		public unsafe fixed int message[200];

		[FieldOffset(4012)]
		public unsafe fixed int repeatDuration[200];

		[FieldOffset(4812)]
		public unsafe fixed int repeatCount[200];

		[FieldOffset(5612)]
		public unsafe fixed int scenario_start_goods[25];

		[FieldOffset(5712)]
		public unsafe fixed int scenario_trader_goods_available[25];

		[FieldOffset(5812)]
		public unsafe fixed int scenario_start_troops[10];

		[FieldOffset(5852)]
		public unsafe fixed int scenario_start_siege_equipment[6];

		[FieldOffset(5876)]
		public unsafe fixed int scenario_buildings_available[100];

		[FieldOffset(6276)]
		public unsafe fixed int sa_troop_availability[7];

		[FieldOffset(6304)]
		public int scenario_start_popularity;

		[FieldOffset(6308)]
		public int scenario_buildings_count;

		[FieldOffset(6312)]
		public int sa_fletcher;

		[FieldOffset(6316)]
		public int sa_blacksmith;

		[FieldOffset(6320)]
		public int sa_poleturner;

		[FieldOffset(6324)]
		public int special_start_gold;

		[FieldOffset(6328)]
		public int special_start;

		[FieldOffset(6332)]
		public int special_start_rationing;

		[FieldOffset(6336)]
		public int special_start_tax_rate;

		[FieldOffset(6340)]
		public unsafe fixed int data2[200];

		[FieldOffset(7140)]
		public int fast_goods_feedin;
	}

	public class ScenarioOverviewEntry
	{
		public int month;

		public int year;

		public int entryType;

		public int data1;

		public int data2;

		public int message;

		public int repeatDuration;

		public int repeatCount;

		public int action_data_marker => data2 & 0xFFFF;

		public int action_data_reinforcement => data2 >> 16;
	}

	public class ScenarioOverview
	{
		public int startMonth;

		public int startYear;

		public List<ScenarioOverviewEntry> entries = new List<ScenarioOverviewEntry>();

		public int[] scenario_start_goods = new int[25];

		public int[] scenario_trader_goods_available = new int[25];

		public int[] scenario_start_troops = new int[10];

		public int[] scenario_start_siege_equipment = new int[6];

		public int[] scenario_buildings_available = new int[100];

		public int[] sa_troop_availability = new int[7];

		public int scenario_start_popularity;

		public int scenario_buildings_count;

		public int sa_fletcher;

		public int sa_blacksmith;

		public int sa_poleturner;

		public int special_start_gold;

		public int special_start;

		public int special_start_rationing;

		public int special_start_tax_rate;

		public int fast_goods_feedin;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct evF
	{
		[FieldOffset(0)]
		public short value;

		[FieldOffset(2)]
		public byte type;

		[FieldOffset(3)]
		public byte onoff;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct tl_eventF
	{
		[FieldOffset(0)]
		public int month;

		[FieldOffset(4)]
		public int year;

		[FieldOffset(8)]
		public int tl_type;

		[FieldOffset(12)]
		public short done;

		[FieldOffset(14)]
		public short pre_done;

		[FieldOffset(16)]
		public int action_data;

		[FieldOffset(20)]
		public int action;

		[FieldOffset(24)]
		public short and_or;

		[FieldOffset(26)]
		public byte repeat;

		[FieldOffset(27)]
		public byte repeat_count;

		[FieldOffset(28)]
		public evF event_value1;

		[FieldOffset(32)]
		public evF event_value2;

		[FieldOffset(36)]
		public evF event_value3;

		[FieldOffset(40)]
		public evF event_value4;

		[FieldOffset(44)]
		public evF event_value5;

		[FieldOffset(48)]
		public evF event_value6;

		[FieldOffset(52)]
		public evF event_value7;

		[FieldOffset(56)]
		public evF event_value8;

		[FieldOffset(60)]
		public evF event_value9;

		[FieldOffset(64)]
		public evF event_value10;

		[FieldOffset(68)]
		public evF event_value11;

		[FieldOffset(72)]
		public evF event_value12;

		[FieldOffset(76)]
		public evF event_value13;

		[FieldOffset(80)]
		public evF event_value14;

		[FieldOffset(84)]
		public evF event_value15;

		[FieldOffset(88)]
		public evF event_value16;

		[FieldOffset(92)]
		public evF event_value17;

		[FieldOffset(96)]
		public evF event_value18;

		[FieldOffset(100)]
		public evF event_value19;

		[FieldOffset(104)]
		public evF event_value20;

		[FieldOffset(108)]
		public evF event_value21;

		[FieldOffset(112)]
		public evF event_value22;

		[FieldOffset(116)]
		public evF event_value23;

		[FieldOffset(120)]
		public evF event_value24;

		[FieldOffset(124)]
		public evF event_value25;

		[FieldOffset(128)]
		public evF event_value26;

		[FieldOffset(132)]
		public evF event_value27;

		[FieldOffset(136)]
		public evF event_value28;

		[FieldOffset(140)]
		public evF event_value29;

		[FieldOffset(144)]
		public evF event_value30;

		[FieldOffset(148)]
		public evF event_value31;

		[FieldOffset(152)]
		public evF event_value32;

		[FieldOffset(156)]
		public evF event_value33;

		[FieldOffset(160)]
		public evF event_value34;

		[FieldOffset(164)]
		public evF event_value35;

		[FieldOffset(168)]
		public evF event_value36;

		[FieldOffset(172)]
		public evF event_value37;

		[FieldOffset(176)]
		public evF event_value38;

		[FieldOffset(180)]
		public evF event_value39;

		[FieldOffset(184)]
		public evF event_value40;
	}

	public class ev
	{
		public short value;

		public byte type;

		public byte onoff;
	}

	public class tl_event
	{
		public int month;

		public int year;

		public int tl_type;

		public short done;

		public short pre_done;

		public int action_data;

		public int action;

		public short and_or;

		public byte repeat;

		public byte repeat_count;

		public ev[] event_value = new ev[40];

		public int action_data_marker
		{
			get
			{
				return action_data & 0xFFFF;
			}
			set
			{
				action_data = value | (int)(action_data & 0xFFFF0000u);
			}
		}

		public int action_data_reinforcement
		{
			get
			{
				return action_data >> 16;
			}
			set
			{
				action_data = (value << 16) | (action_data & 0xFFFF);
			}
		}
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct tl_messageF
	{
		[FieldOffset(0)]
		public int month;

		[FieldOffset(4)]
		public int year;

		[FieldOffset(8)]
		public int tl_type;

		[FieldOffset(12)]
		public short done;

		[FieldOffset(14)]
		public short pre_done;

		[FieldOffset(16)]
		public int message_id;

		[FieldOffset(20)]
		public int action;
	}

	public class tl_message
	{
		public int month;

		public int year;

		public int tl_type;

		public short done;

		public short pre_done;

		public int message_id;

		public int action;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct tl_invasionF
	{
		[FieldOffset(0)]
		public int month;

		[FieldOffset(4)]
		public int year;

		[FieldOffset(8)]
		public int tl_type;

		[FieldOffset(12)]
		public short done;

		[FieldOffset(14)]
		public short pre_done;

		[FieldOffset(16)]
		public int total;

		[FieldOffset(20)]
		public unsafe fixed int _size[17];

		[FieldOffset(88)]
		public int invasion_point;

		[FieldOffset(92)]
		public int start_year;

		[FieldOffset(96)]
		public int repeat;

		[FieldOffset(100)]
		public int from;

		[FieldOffset(104)]
		public int markerID;
	}

	public class tl_invasion
	{
		public int month;

		public int year;

		public int tl_type;

		public short done;

		public short pre_done;

		public int total;

		public int[] _size = new int[17];

		public int invasion_point;

		public int start_year;

		public int repeat;

		public int from;

		public int markerID;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct PlayStateReturnData
	{
		[FieldOffset(0)]
		public unsafe fixed int resources[25];

		[FieldOffset(100)]
		public int numSelectedChimps;

		[FieldOffset(104)]
		public unsafe fixed int selectedChimps[1];

		[FieldOffset(108)]
		public int popularity;

		[FieldOffset(112)]
		public int population;

		[FieldOffset(116)]
		public int gold;

		[FieldOffset(120)]
		public int housing_cap;

		[FieldOffset(124)]
		public int upcoming_total_popularity;

		[FieldOffset(128)]
		public int rationing_popularity;

		[FieldOffset(132)]
		public int foodsEaten_popularity;

		[FieldOffset(136)]
		public int food_popularity;

		[FieldOffset(140)]
		public int tax_popularity;

		[FieldOffset(144)]
		public int overcrowding_popularity;

		[FieldOffset(148)]
		public int fearFactor_popularity;

		[FieldOffset(152)]
		public int religion_popularity;

		[FieldOffset(156)]
		public int fairs_popularity;

		[FieldOffset(160)]
		public int plague_popularity;

		[FieldOffset(164)]
		public int wolves_popularity;

		[FieldOffset(168)]
		public int bandits_popularity;

		[FieldOffset(172)]
		public int fire_popularity;

		[FieldOffset(176)]
		public int marriage_popularity;

		[FieldOffset(180)]
		public int jester_popularity;

		[FieldOffset(184)]
		public int good_things;

		[FieldOffset(188)]
		public int bad_things;

		[FieldOffset(192)]
		public int fear_factor;

		[FieldOffset(196)]
		public int fear_factor_next_level;

		[FieldOffset(200)]
		public int efficiency;

		[FieldOffset(204)]
		public unsafe fixed short population_graph[300];

		[FieldOffset(804)]
		public unsafe fixed short food_types_not_eatable[4];

		[FieldOffset(812)]
		public unsafe fixed short troop_counts[18];

		[FieldOffset(848)]
		public short num_priests;

		[FieldOffset(850)]
		public short blessed_percent;

		[FieldOffset(856)]
		public short blessed_next_level_at;

		[FieldOffset(852)]
		public int tax_rate;

		[FieldOffset(858)]
		public short tax_amount;

		[FieldOffset(860)]
		public short peasants_available_for_troops;

		[FieldOffset(862)]
		public unsafe fixed byte make_troop_state[10];

		[FieldOffset(872)]
		public int rationing;

		[FieldOffset(876)]
		public int food_clock;

		[FieldOffset(880)]
		public int total_food;

		[FieldOffset(884)]
		public int months_of_food;

		[FieldOffset(888)]
		public int food_types_eaten;

		[FieldOffset(892)]
		public int food_types_available;

		[FieldOffset(896)]
		public int app_mode;

		[FieldOffset(900)]
		public int app_sub_mode;

		[FieldOffset(904)]
		public int debug_value1;

		[FieldOffset(908)]
		public int debug_value2;

		[FieldOffset(912)]
		public int in_structure;

		[FieldOffset(916)]
		public int in_structure_type;

		[FieldOffset(920)]
		public int completeSelectionBox;

		[FieldOffset(924)]
		public int in_chimp;

		[FieldOffset(928)]
		public int in_chimp_type;

		[FieldOffset(932)]
		public short inchimp_name1;

		[FieldOffset(934)]
		public short inchimp_name2;

		[FieldOffset(936)]
		public short dog_cage_state;

		[FieldOffset(938)]
		public short inchimp_n_text;

		[FieldOffset(940)]
		public int in_chimp_goods;

		[FieldOffset(944)]
		public int gatehouse_state;

		[FieldOffset(948)]
		public short repairs_allowed;

		[FieldOffset(950)]
		public short can_do_repairs;

		[FieldOffset(952)]
		public short building_hps_for_repair;

		[FieldOffset(954)]
		public short building_maxhps_for_repair;

		[FieldOffset(956)]
		public short sleep_allowed;

		[FieldOffset(958)]
		public short building_type_sleeping;

		[FieldOffset(960)]
		public short have_building_stats;

		[FieldOffset(962)]
		public short workers_have;

		[FieldOffset(964)]
		public short job_vacancies;

		[FieldOffset(966)]
		public short workers_needed;

		[FieldOffset(968)]
		public short got_keep_access;

		[FieldOffset(970)]
		public short turned_off;

		[FieldOffset(972)]
		public short working;

		[FieldOffset(974)]
		public short mill_message;

		[FieldOffset(976)]
		public int pints_of_ale;

		[FieldOffset(980)]
		public short barrels_of_ale;

		[FieldOffset(982)]
		public short working_inns;

		[FieldOffset(984)]
		public short total_inns;

		[FieldOffset(986)]
		public short inn_coverage_percent;

		[FieldOffset(988)]
		public short inn_coverage_popularity;

		[FieldOffset(990)]
		public short inn_coverage_next;

		[FieldOffset(992)]
		public byte troops_show_disband;

		[FieldOffset(993)]
		public byte troops_show_build_menu;

		[FieldOffset(994)]
		public byte troops_show_make_catapult;

		[FieldOffset(995)]
		public byte troops_show_make_trebuchet;

		[FieldOffset(996)]
		public byte troops_show_make_siege_tower;

		[FieldOffset(997)]
		public byte troops_show_battering_ram;

		[FieldOffset(998)]
		public byte troops_show_portable_shield;

		[FieldOffset(999)]
		public byte troops_show_get_ammo;

		[FieldOffset(1000)]
		public byte troops_show_launch_cow_and_num_cows;

		[FieldOffset(1001)]
		public byte troops_show_attack_here_and_type;

		[FieldOffset(1002)]
		public byte troops_show_attack_here_number_rocks;

		[FieldOffset(1003)]
		public byte troops_show_stance;

		[FieldOffset(1004)]
		public byte troops_show_patrol;

		[FieldOffset(1005)]
		public byte troops_patrol_mode;

		[FieldOffset(1006)]
		public byte weapon_being_made_now;

		[FieldOffset(1007)]
		public byte game_type;

		[FieldOffset(1008)]
		public byte can_make_xbows;

		[FieldOffset(1009)]
		public byte can_make_sword;

		[FieldOffset(1010)]
		public byte can_make_pike;

		[FieldOffset(1011)]
		public byte weapon_being_made_next;

		[FieldOffset(1012)]
		public byte production_no_resources;

		[FieldOffset(1013)]
		public byte playerdesc_message;

		[FieldOffset(1014)]
		public byte playerdesc_message2;

		[FieldOffset(1015)]
		public unsafe fixed byte weapon_types_available[9];

		[FieldOffset(1024)]
		public unsafe fixed short trade_buy_costs[25];

		[FieldOffset(1074)]
		public unsafe fixed short trade_sell_costs[25];

		[FieldOffset(1124)]
		public unsafe fixed short trade_buy_amounts[25];

		[FieldOffset(1174)]
		public unsafe fixed short trade_sell_amounts[25];

		[FieldOffset(1224)]
		public short marry_status;

		[FieldOffset(1226)]
		public short marry_male_type;

		[FieldOffset(1228)]
		public short marry_female_type;

		[FieldOffset(1230)]
		public short marry_text;

		[FieldOffset(1232)]
		public short marry_m_name1;

		[FieldOffset(1234)]
		public short marry_m_name2;

		[FieldOffset(1236)]
		public short marry_f_name1;

		[FieldOffset(1238)]
		public short marry_f_name2;

		[FieldOffset(1240)]
		public short blessed_popularity;

		[FieldOffset(1242)]
		public byte church_adjustment;

		[FieldOffset(1243)]
		public byte church_missing;

		[FieldOffset(1244)]
		public short scribe_frame;

		[FieldOffset(1246)]
		public short total_horses_available;

		[FieldOffset(1248)]
		public int action_point_count;

		[FieldOffset(1252)]
		public unsafe fixed short action_points_x[20];

		[FieldOffset(1292)]
		public unsafe fixed short action_points_y[20];

		[FieldOffset(1332)]
		public short camera_target_x;

		[FieldOffset(1334)]
		public short camera_target_y;

		[FieldOffset(1336)]
		public short camera_target_z;

		[FieldOffset(1338)]
		public short rotateHappened;

		[FieldOffset(1340)]
		public unsafe fixed short trade_sell_costs_fixed[25];

		[FieldOffset(1390)]
		public short trading_current_goods;

		[FieldOffset(1392)]
		public short trading_next_goods;

		[FieldOffset(1394)]
		public short trading_prev_goods;

		[FieldOffset(1396)]
		public short force_app_mode;

		[FieldOffset(1398)]
		public short month;

		[FieldOffset(1400)]
		public short year;

		[FieldOffset(1402)]
		public short pop_months;

		[FieldOffset(1404)]
		public unsafe fixed int keep_storage[25];

		[FieldOffset(1504)]
		public unsafe fixed byte speechFileName[128];

		[FieldOffset(1632)]
		public unsafe fixed byte musicFileName[128];

		[FieldOffset(1760)]
		public short chimp_comments;

		[FieldOffset(1762)]
		public short camera_target_flat;

		[FieldOffset(1764)]
		public unsafe fixed byte binkFileName[128];

		[FieldOffset(1892)]
		public short siege_that_saveable;

		[FieldOffset(1894)]
		public short inbuilding_help_id;

		[FieldOffset(1896)]
		public short MP_Ahead_By;

		[FieldOffset(1898)]
		public short MP_Behind_By;

		[FieldOffset(1900)]
		public short SkipFrame;

		[FieldOffset(1902)]
		public short undoAvailable;

		[FieldOffset(1904)]
		public unsafe fixed int koth_scores[8];

		[FieldOffset(1936)]
		public unsafe fixed short pingtimes[8];

		[FieldOffset(1952)]
		public short chimps_count;

		[FieldOffset(1954)]
		public short chimps_limit;

		[FieldOffset(1956)]
		public short structs_count;

		[FieldOffset(1958)]
		public short structs_limit;

		[FieldOffset(1960)]
		public short orgs_count;

		[FieldOffset(1962)]
		public short orgs_limit;

		[FieldOffset(1964)]
		public short minerals_count;

		[FieldOffset(1966)]
		public short minerals_limit;

		[FieldOffset(1968)]
		public short tribes_count;

		[FieldOffset(1970)]
		public short tribes_limit;

		[FieldOffset(1972)]
		public unsafe fixed byte starting_teams[9];

		[FieldOffset(1981)]
		public byte freeWoodcutter;

		[FieldOffset(1982)]
		public byte freeGranary;

		[FieldOffset(1983)]
		public byte gotSignpost;

		[FieldOffset(1984)]
		public int repair_wood_needed;

		[FieldOffset(1988)]
		public int repair_stone_needed;

		[FieldOffset(1992)]
		public short panel_text_group;

		[FieldOffset(1994)]
		public short panel_text_text;

		[FieldOffset(1996)]
		public unsafe fixed int markers_start_points[40];

		[FieldOffset(2156)]
		public unsafe fixed byte troop_types_available[8];

		[FieldOffset(2164)]
		public byte free_buildingCheat;

		[FieldOffset(2165)]
		public byte editor_time_paused;

		[FieldOffset(2166)]
		public short bld_tiles_built;

		[FieldOffset(2168)]
		public byte game_paused;

		[FieldOffset(2169)]
		public byte numMPChatEntries;

		[FieldOffset(2170)]
		public short ai_clock;

		[FieldOffset(2172)]
		public unsafe fixed short chat_store_data[40];

		[FieldOffset(2252)]
		public unsafe fixed short autotrade_sell_amount[26];

		[FieldOffset(2304)]
		public unsafe fixed short autotrade_buy_amount[26];

		[FieldOffset(2356)]
		public unsafe fixed byte autotrade_onoff[28];

		[FieldOffset(2384)]
		public unsafe fixed byte control_groups_match[10];

		[FieldOffset(2394)]
		public unsafe fixed short control_groups_total[10];

		[FieldOffset(2414)]
		public unsafe fixed byte control_groups_type[40];

		[FieldOffset(2454)]
		public unsafe fixed short control_groups_count[40];

		[FieldOffset(2534)]
		public byte lordOnlySelected;

		[FieldOffset(2535)]
		public byte gotMarket;

		[FieldOffset(2536)]
		public unsafe fixed byte mpkick[8];

		[FieldOffset(2544)]
		public byte keep_enclosed;

		[FieldOffset(2545)]
		public byte free2;

		[FieldOffset(2546)]
		public byte free3;

		[FieldOffset(2547)]
		public byte free4;
	}

	public class PlayState
	{
		public int[] resources = new int[25];

		public int[] keep_storage = new int[25];

		public int numSelectedChimps;

		public int[] selectedChimps = new int[3000];

		public int popularity;

		public int population;

		public int gold;

		public int housing_cap;

		public int upcoming_total_popularity;

		public int rationing_popularity;

		public int foodsEaten_popularity;

		public int food_popularity;

		public int tax_popularity;

		public int overcrowding_popularity;

		public int fearFactor_popularity;

		public int religion_popularity;

		public int fairs_popularity;

		public int plague_popularity;

		public int wolves_popularity;

		public int bandits_popularity;

		public int fire_popularity;

		public int marriage_popularity;

		public int jester_popularity;

		public int good_things;

		public int bad_things;

		public int fear_factor;

		public int fear_factor_next_level;

		public int efficiency;

		public short[] population_graph = new short[300];

		public short[] food_types_not_eatable = new short[4];

		public short[] troop_counts = new short[18];

		public short num_priests;

		public short blessed_percent;

		public short blessed_next_level_at;

		public int tax_rate;

		public short tax_amount;

		public short peasants_available_for_troops;

		public byte[] make_troop_state = new byte[8];

		public int rationing;

		public int food_clock;

		public int total_food;

		public int months_of_food;

		public int food_types_eaten;

		public int food_types_available;

		public int app_mode;

		public int app_sub_mode;

		public int debug_value1;

		public int debug_value2;

		public int in_structure;

		public int in_structure_type;

		public int completeSelectionBox;

		public int in_chimp;

		public int in_chimp_type;

		public short inchimp_name1;

		public short inchimp_name2;

		public short dog_cage_state;

		public short inchimp_n_text;

		public int in_chimp_goods;

		public int gatehouse_state;

		public short repairs_allowed;

		public short can_do_repairs;

		public short building_hps_for_repair;

		public short building_maxhps_for_repair;

		public short sleep_allowed;

		public short building_type_sleeping;

		public short have_building_stats;

		public short workers_have;

		public short job_vacancies;

		public short workers_needed;

		public short got_keep_access;

		public short turned_off;

		public short working;

		public short mill_message;

		public int pints_of_ale;

		public short barrels_of_ale;

		public short working_inns;

		public short total_inns;

		public short inn_coverage_percent;

		public short inn_coverage_popularity;

		public short inn_coverage_next;

		public byte troops_show_disband;

		public byte troops_show_build_menu;

		public byte troops_show_make_catapult;

		public byte troops_show_make_trebuchet;

		public byte troops_show_make_siege_tower;

		public byte troops_show_battering_ram;

		public byte troops_show_portable_shield;

		public byte troops_show_get_ammo;

		public byte troops_show_launch_cow_and_num_cows;

		public byte troops_show_attack_here_and_type;

		public byte troops_show_attack_here_number_rocks;

		public byte troops_show_stance;

		public byte troops_show_patrol;

		public byte troops_patrol_mode;

		public byte weapon_being_made_now;

		public byte game_type;

		public byte can_make_xbows;

		public byte can_make_sword;

		public byte can_make_pike;

		public byte weapon_being_made_next;

		public byte production_no_resources;

		public byte playerdesc_message;

		public byte playerdesc_message2;

		public byte[] weapon_types_available;

		public short[] trade_buy_costs;

		public short[] trade_sell_costs;

		public short[] trade_buy_amounts;

		public short[] trade_sell_amounts;

		public short marry_status;

		public short marry_male_type;

		public short marry_female_type;

		public short marry_text;

		public short marry_m_name1;

		public short marry_m_name2;

		public short marry_f_name1;

		public short marry_f_name2;

		public short blessed_popularity;

		public sbyte church_adjustment;

		public byte church_missing;

		public short scribe_frame;

		public short total_horses_available;

		public int action_point_count;

		public short[] action_points_x;

		public short[] action_points_y;

		public short camera_target_x;

		public short camera_target_y;

		public short camera_target_z;

		public short rotateHappened;

		public short[] trade_sell_costs_fixed;

		public short trading_current_goods;

		public short trading_next_goods;

		public short trading_prev_goods;

		public short force_app_mode;

		public short month;

		public short year;

		public short pop_months;

		public short chimp_comments;

		public short camera_target_flat;

		public short siege_that_saveable;

		public short inbuilding_help_id;

		public short MP_Ahead_By;

		public short MP_Behind_By;

		public short SkipFrame;

		public short undoAvailable;

		public int[] koth_scores;

		public short[] pingtimes;

		public short chimps_count;

		public short chimps_limit;

		public short structs_count;

		public short structs_limit;

		public short orgs_count;

		public short orgs_limit;

		public short minerals_count;

		public short minerals_limit;

		public short tribes_count;

		public short tribes_limit;

		public byte[] starting_teams;

		public byte freeWoodcutter;

		public byte freeGranary;

		public byte gotSignpost;

		public int repair_wood_needed;

		public int repair_stone_needed;

		public short panel_text_group;

		public short panel_text_text;

		public int[,] markers_start_points;

		public byte[] troop_types_available;

		public byte free_buildingCheat;

		public byte editor_time_paused;

		public short bld_tiles_built;

		public byte game_paused;

		public byte numMPChatEntries;

		public short ai_clock;

		public short[,] chat_store_data;

		public short[] autotrade_sell_amount;

		public short[] autotrade_buy_amount;

		public byte[] autotrade_onoff;

		public byte[] control_groups_match;

		public short[] control_groups_total;

		public byte[] control_groups_type;

		public short[] control_groups_count;

		public byte lordOnlySelected;

		public byte gotMarket;

		public byte[] mpkick;

		public byte keep_enclosed;

		public byte free2;

		public byte free3;

		public byte free4;

		public string speechFileName;

		public string musicFileName;

		public string binkFileName;

		public int MAPEDITOR_numshieldsToDisplay;

		public bool MAPEDITOR_allowLandscapeEditing;

		public bool MP_TroopsCostGold => (gotSignpost & 1) > 0;

		public bool MP_AllowAutoTrading => (gotSignpost & 2) > 0;
	}

	public struct ScoreReturnData
	{
		public int score_weapons;

		public int score_weapons_points;

		public int score;

		public int levelPoints;

		public int score_months;

		public int score_months_points;

		public int items_count;

		public int items_extra1;

		public int items_extra2;

		public int items_extra3;

		public int items_extra4;

		public int items_extra5;

		public int items_extra6;

		public int items_extra7;

		public int items_extra_points1;

		public int items_extra_points2;

		public int items_extra_points3;

		public int items_extra_points4;

		public int items_extra_points5;

		public int items_extra_points6;

		public int items_extra_points7;

		public int items_extra_type1;

		public int items_extra_type2;

		public int items_extra_type3;

		public int items_extra_type4;

		public int items_extra_type5;

		public int items_extra_type6;

		public int items_extra_type7;

		public int score_troops;

		public int troops_percent_lost;

		public int siege_that_score;

		public int siege_defenders_score;

		public int siege_attackers_score;

		public int difficulty_level;
	}

	public class ScoreData
	{
		public int score_weapons;

		public int score_weapons_points;

		public int score;

		public int levelPoints;

		public int score_months;

		public int score_months_points;

		public int items_count;

		public int[] items_extra;

		public int[] items_extra_points;

		public int[] items_extra_type;

		public int score_troops;

		public int troops_percent_lost;

		public int siege_that_score;

		public int siege_defenders_score;

		public int siege_attackers_score;

		public int difficulty_level;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct multiplayer_stats_export
	{
		[FieldOffset(0)]
		public unsafe fixed int valid[9];

		[FieldOffset(36)]
		public unsafe fixed int gold_acquired[9];

		[FieldOffset(72)]
		public unsafe fixed int max_population[9];

		[FieldOffset(108)]
		public unsafe fixed int fearfactor[9];

		[FieldOffset(144)]
		public unsafe fixed int time_deceased[9];

		[FieldOffset(180)]
		public unsafe fixed int who_killed_who[81];

		[FieldOffset(504)]
		public unsafe fixed int enemy_buildings_destroyed[9];

		[FieldOffset(540)]
		public unsafe fixed int food_produced[9];

		[FieldOffset(576)]
		public unsafe fixed int iron_produced[9];

		[FieldOffset(612)]
		public unsafe fixed int stone_produced[9];

		[FieldOffset(648)]
		public unsafe fixed int wood_produced[9];

		[FieldOffset(684)]
		public unsafe fixed int pitch_produced[9];

		[FieldOffset(720)]
		public unsafe fixed int minfearfactor[9];

		[FieldOffset(756)]
		public unsafe fixed int lords_killed[9];

		[FieldOffset(792)]
		public unsafe fixed int king_of_the_hill_points[9];

		[FieldOffset(828)]
		public unsafe fixed int winners[9];

		[FieldOffset(864)]
		public int king_of_the_hill_points_start;
	}

	public class MPScoreData
	{
		public int[] valid;

		public int[] gold_acquired;

		public int[] max_population;

		public int[] fearfactor;

		public int[] time_deceased;

		public int[][] who_killed_who;

		public int[] enemy_buildings_destroyed;

		public int[] food_produced;

		public int[] iron_produced;

		public int[] stone_produced;

		public int[] wood_produced;

		public int[] pitch_produced;

		public int[] minfearfactor;

		public int[] lords_killed;

		public int[] king_of_the_hill_points;

		public int[] winners;

		public int king_of_the_hill_points_start;
	}

	public struct LogicDebugInfo
	{
		public int gfx_layer;

		public int gfx_layer_file;

		public int gfx_layer_id;

		public int alpha_gfx_layer;

		public int construction_gfx_layer;

		public int pillar_gfx_layer;

		public int pillar_gfx_layer_file;

		public int pillar_gfx_layer_id;

		public int wall_gfx_layer;

		public int wall_gfx_layer_file;

		public int wall_gfx_layer_id;

		public int floating_layer;

		public int random_layer;

		public int logic_layer;

		public int logic2_layer;

		public int changed_layer;

		public int organism_layer;

		public int structure_layer;

		public int structure_was_layer;

		public int chimp_layer;

		public int fly_layer;

		public int height_layer;

		public int default_height_layer;

		public int wall_owner_layer;

		public int luminesence_layer;

		public int show_hi_layer;

		public int misc_display_layer;

		public int damage_layer;

		public int macro_layer;

		public int path_connection_layer;

		public int path_linkage_layer;

		public int occupancy_layer;

		public int certain_path_layer;

		public int walk_layer;

		public int ai_zone_layer;

		public int ai_info_layer;

		public int ai_danger_layer;

		public int ai_proximity_layer;

		public int delay_layer;

		public int mapOfset;
	}

	private static object threadLock = new object();

	private const int CHIMPS_LIMIT_FAKE = 1;

	private const int CHIMPS_LIMIT = 3000;

	public static bool flattenedLandscape = false;

	public static bool FlattenedLandscape => flattenedLandscape;

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern void DLL_PreInitMap_Editor(int mapSize, int mapType, bool siegeThat, bool multiplayerMap, byte* retData);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern void DLL_PreInitMap_Campaign(int difficulty);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern void DLL_PreInitMap_EcoCampaign();

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern void DLL_EcoCampaign_ChangeDifficulty(int difficulty);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_EcoCampaign_ChangeDifficulty_briefing(int difficulty, int* retData);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern void DLL_PreInitMap_SiegeThat(int difficulty, int playerID, int troop0, int troop1, int troop2, int troop3, int troop4, int troop5, int troop6, int troop7, int troop8, int troop9, int troop10, bool advancedMode);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern void DLL_PreInitMap_Invasion(int difficulty);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern void DLL_PreInitMap_EcoMap(int difficulty);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern void DLL_PreInitMap_JustBuild(bool advancedFreebuild, int freebuild_GoldLevel, int freebuild_FoodLevel, int freebuild_ResourcesLevel, int freebuild_WeaponsLevel, int freebuild_RandomEvents, int freebuild_Invasions, int freebuild_InvasionDifficulty, int freebuild_Peacetime);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern void DLL_PreInitMap_Multiplayer(byte* retData);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern void DLL_PreInitTutorial();

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern void DLL_ApplyMultiplayerSetupData(byte* retData);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern void DLL_GetMultiplayerSetupData(byte* retData);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern void DLL_RegisterMultiplayerUser(int playerID, byte* name, int nameLength, int team, bool localPlayer);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern int DLL_StartMultiplayerGame(bool fromSave);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern int DLL_StartMultiplayerGameSynced();

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern void DLL_SetMPRandSeed(int seed);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_LoadSaveGame(byte* data, int length, byte* retData, bool loadingEditorMap);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_ReceiveChore(int playerID, byte* data, int length);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern void DLL_GetMultiplayerChatInfo(int* players, int* teams);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern void DLL_KickMPPlayer(int kickPlayerID, bool immediate);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern void DLL_PromoteMPHost(int hostID);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_TriggerMPSave(byte* data, int length);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_TriggerMPLoad(byte* data, int length);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_RemapPlayers(int* newMappings, int newLocalPlayer);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_SetMPRadarColours(int* newMappings);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern int DLL_ConnectionPause(bool pause);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_SaveSaveGame(byte* data, int length, int screenCentreX, int screenCentreY, int realScreenCentreX, int realScreenCentreY, bool lockMap, bool tempLockOnly, bool mapSave);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_LoadMapToPlay(int campaignMapID, byte* fileName, int length, byte* retData, bool mission6PreStart, byte* mapName, int maplength, bool multiplayerSave, int trailType, int trailID);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_GetScenarioTroopsInfo(byte* data, int length, int* retData);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_CampaignLevel(byte* path, int length);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern void DLL_GetColourMapping(int* retData, int remappedPlayer);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_RunTick(short* data, byte* radarMap, bool flattenedLandscape, int mouseOverX, int mouseOverY, bool shiftPressed, bool ctrlPressed, bool altPressed, byte* retData, bool paused, bool ambientSoundChannel1, bool ambientSoundChannel2, bool speechSoundChannel1, bool speechSoundChannel2, bool musicPlaying, bool musicAboutToLoop, bool binkPlaying, int screenCentrePosX, int screenCentrePosY, int screenTilesWide, int screenTilesHigh, int radarMapWidth, int radarMapHeight, int radarZoom, int screenZoom, bool SH1RtsControls, int screenCentreTileX, int screenCentreTileY, byte* choreBuffer, int* selectedChimpsBuffer, bool mpFrameSkip, int buildingOverDepth, int troopOverdepth);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_SetPath(byte* data, int length, byte* autoData, int autoLength, byte* saveFolderData, int saveFolderLength);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_Unpack(byte* source, byte* dest, int destBufferLength);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_Pack(byte* source, byte* dest, int sourceLength);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_GetunpackSize(byte* source);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_UnpackRadarToARGB(byte* source, byte* dest);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_CRC(byte* source, int size);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_GetSaveRadar(byte* dest);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern int DLL_SetMapRotation(int rotation, int centreX, int centreY);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern int DLL_StartMapAction(int action);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern int DLL_MapAction(int action, int map_x, int map_y, int brushSize, int playerID, bool inGameNotMapEditor, bool constructingOnly, int mouseState);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern int DLL_GameAction(int action, int structureID, int value);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_TroopSelection(int mouseState, bool rightDown, bool rightUp, int count, int* selectedChimps, bool selection_on, bool selection_established, int underCursorCount, int* underCursorChimps, int mousePosX, int mousePosY, bool overTopHalf, int onScreenCount, int* onScreenChimps);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_TroopSelectionChanged(int count, int* selectedChimps);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern int DLL_GetMapperSize(int action);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern int DLL_IsMapperAvailable(int mapper);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern int DLL_GetMapperCoord(int mapper);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern void DLL_SetAchValues(int food, int wood, int weapons);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern void DLL_SetEditorPlayer(int playerID);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern void DLL_SetUTF8MissionText(byte* text, int length);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern void DLL_SetUTF8MapName(byte* text, int length, byte* text2, int length2);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern void DLL_GetScenarioOverview(byte* retData);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern int DLL_CreateScenarioAction(int action);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern bool DLL_GetScenarioEvent(int eventID, byte* retData);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern bool DLL_GetScenarioMessage(int eventID, byte* retData);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern bool DLL_GetScenarioInvasion(int eventID, byte* retData);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern bool DLL_ApplyScenarioEvent(int eventID, byte* retData);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern bool DLL_ApplyScenarioMessage(int eventID, byte* retData);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern bool DLL_ApplyScenarioInvasion(int eventID, byte* retData);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern void DLL_DeleteScenarioAction(int eventID);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern void DLL_UpdateScenarioActionDate(int entryID, int year, int month);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern int DLL_SetMapEditorParam(int SPMPMode, int gameType, int koth, int mapSize);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern void DLL_GetScenarioMessageList(int* messageIDs, int* messageTypes);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern void DLL_SetAppMode(int app_mode, int app_sub_mode);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern void DLL_TutorialAction(int ID, int value);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern void DLL_GetScoreData(byte* retData);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern void DLL_GetMPScoreData(byte* retData);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public static extern int DLL_SetDebugMode(int action);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_GetLayerDebug(int x, int y, byte* retData);

	[DllImport("StrongholdDE")]
	[MockNativeDeclaration]
	public unsafe static extern int DLL_GetLayerData(int layedID, byte* retData);

	public unsafe static void sendPath(string gameDataPath, string multiplayerAutoSaveName, string saveFolder)
	{
		byte[] bytes = Encoding.Unicode.GetBytes(gameDataPath);
		byte[] bytes2 = Encoding.Unicode.GetBytes(multiplayerAutoSaveName);
		byte[] bytes3 = Encoding.Unicode.GetBytes(saveFolder);
		fixed (byte* data = bytes)
		{
			fixed (byte* autoData = bytes2)
			{
				fixed (byte* saveFolderData = bytes3)
				{
					DLL_SetPath(data, bytes.Length, autoData, bytes2.Length, saveFolderData, bytes3.Length);
				}
			}
		}
	}

	public unsafe static void TriggerMPLoad(string filename)
	{
		byte[] bytes = Encoding.Unicode.GetBytes(filename);
		fixed (byte* data = bytes)
		{
			DLL_TriggerMPLoad(data, bytes.Length);
		}
	}

	public unsafe static void TriggerMPSave(string savename)
	{
		lock (threadLock)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(savename);
			fixed (byte* data = bytes)
			{
				DLL_TriggerMPSave(data, bytes.Length);
			}
		}
	}

	public static MultiplayerSetupData getMPSetup(MultiplayerSetupTransferData source)
	{
		MultiplayerSetupData obj = new MultiplayerSetupData
		{
			starting_gold = source.starting_gold,
			starting_popularity = source.starting_popularity,
			starting_gamespeed = source.starting_gamespeed,
			starting_goods_level = source.starting_goods_level,
			starting_troops_level = source.starting_troops_level,
			starting_keep_type = source.starting_keep_type,
			king_of_the_hill_points = source.king_of_the_hill_points,
			win_condition = source.win_condition,
			allow_troop_gold = source.allow_troop_gold,
			allow_autotrading = source.allow_autotrading,
			no_knockdown_walls = source.no_knockdown_walls,
			no_ingame_alliances = source.no_ingame_alliances,
			autosave = source.autosave,
			peacetime = source.peacetime
		};
		obj.trading[0] = source.trading0;
		obj.trading[1] = source.trading1;
		obj.trading[2] = source.trading2;
		obj.trading[3] = source.trading3;
		obj.starting_goods[0] = source.starting_goods0;
		obj.starting_goods[1] = source.starting_goods1;
		obj.starting_goods[2] = source.starting_goods2;
		obj.starting_goods[3] = source.starting_goods3;
		obj.starting_goods[4] = source.starting_goods4;
		obj.starting_goods[5] = source.starting_goods5;
		obj.starting_goods[6] = source.starting_goods6;
		obj.starting_goods[7] = source.starting_goods7;
		obj.starting_goods[8] = source.starting_goods8;
		obj.starting_goods[9] = source.starting_goods9;
		obj.starting_goods[10] = source.starting_goods10;
		obj.starting_goods[11] = source.starting_goods11;
		obj.starting_goods[12] = source.starting_goods12;
		obj.starting_goods[13] = source.starting_goods13;
		obj.starting_goods[14] = source.starting_goods14;
		obj.starting_goods[15] = source.starting_goods15;
		obj.starting_goods[16] = source.starting_goods16;
		obj.starting_goods[17] = source.starting_goods17;
		obj.starting_goods[18] = source.starting_goods18;
		obj.starting_goods[19] = source.starting_goods19;
		obj.starting_troops[0] = source.starting_troops0;
		obj.starting_troops[1] = source.starting_troops1;
		obj.starting_troops[2] = source.starting_troops2;
		obj.starting_troops[3] = source.starting_troops3;
		obj.starting_troops[4] = source.starting_troops4;
		obj.starting_troops[5] = source.starting_troops5;
		obj.starting_troops[6] = source.starting_troops6;
		obj.starting_troops[7] = source.starting_troops7;
		obj.starting_troops[8] = source.starting_troops8;
		obj.starting_troops[9] = source.starting_troops9;
		return obj;
	}

	public static MultiplayerSetupTransferData setMPSetup(MultiplayerSetupData source)
	{
		MultiplayerSetupTransferData result = default(MultiplayerSetupTransferData);
		result.starting_gold = source.starting_gold;
		result.starting_popularity = source.starting_popularity;
		result.starting_gamespeed = source.starting_gamespeed;
		result.starting_goods_level = source.starting_goods_level;
		result.starting_troops_level = source.starting_troops_level;
		result.starting_keep_type = source.starting_keep_type;
		result.king_of_the_hill_points = source.king_of_the_hill_points;
		result.win_condition = source.win_condition;
		result.allow_troop_gold = source.allow_troop_gold;
		result.allow_autotrading = source.allow_autotrading;
		result.no_knockdown_walls = source.no_knockdown_walls;
		result.no_ingame_alliances = source.no_ingame_alliances;
		result.autosave = source.autosave;
		result.peacetime = source.peacetime;
		result.trading0 = source.trading[0];
		result.trading1 = source.trading[1];
		result.trading2 = source.trading[2];
		result.trading3 = source.trading[3];
		result.starting_goods0 = source.starting_goods[0];
		result.starting_goods1 = source.starting_goods[1];
		result.starting_goods2 = source.starting_goods[2];
		result.starting_goods3 = source.starting_goods[3];
		result.starting_goods4 = source.starting_goods[4];
		result.starting_goods5 = source.starting_goods[5];
		result.starting_goods6 = source.starting_goods[6];
		result.starting_goods7 = source.starting_goods[7];
		result.starting_goods8 = source.starting_goods[8];
		result.starting_goods9 = source.starting_goods[9];
		result.starting_goods10 = source.starting_goods[10];
		result.starting_goods11 = source.starting_goods[11];
		result.starting_goods12 = source.starting_goods[12];
		result.starting_goods13 = source.starting_goods[13];
		result.starting_goods14 = source.starting_goods[14];
		result.starting_goods15 = source.starting_goods[15];
		result.starting_goods16 = source.starting_goods[16];
		result.starting_goods17 = source.starting_goods[17];
		result.starting_goods18 = source.starting_goods[18];
		result.starting_goods19 = source.starting_goods[19];
		result.starting_troops0 = source.starting_troops[0];
		result.starting_troops1 = source.starting_troops[1];
		result.starting_troops2 = source.starting_troops[2];
		result.starting_troops3 = source.starting_troops[3];
		result.starting_troops4 = source.starting_troops[4];
		result.starting_troops5 = source.starting_troops[5];
		result.starting_troops6 = source.starting_troops[6];
		result.starting_troops7 = source.starting_troops[7];
		result.starting_troops8 = source.starting_troops[8];
		result.starting_troops9 = source.starting_troops[9];
		return result;
	}

	private unsafe static ScenarioOverview convertScenarioOverview(ScenarioOverviewReturnData source)
	{
		ScenarioOverview scenarioOverview = new ScenarioOverview();
		scenarioOverview.startMonth = source.startMonth;
		scenarioOverview.startYear = source.startYear;
		for (int i = 0; i < source.numEntries; i++)
		{
			ScenarioOverviewEntry item = new ScenarioOverviewEntry
			{
				month = source.month[i],
				year = source.year[i],
				entryType = source.entryType[i],
				data1 = source.data1[i],
				data2 = source.data2[i],
				message = source.message[i],
				repeatDuration = source.repeatDuration[i],
				repeatCount = source.repeatCount[i]
			};
			scenarioOverview.entries.Add(item);
		}
		for (int j = 0; j < 25; j++)
		{
			scenarioOverview.scenario_start_goods[j] = source.scenario_start_goods[j];
			scenarioOverview.scenario_trader_goods_available[j] = source.scenario_trader_goods_available[j];
		}
		for (int k = 0; k < 10; k++)
		{
			scenarioOverview.scenario_start_troops[k] = source.scenario_start_troops[k];
		}
		for (int l = 0; l < 100; l++)
		{
			scenarioOverview.scenario_buildings_available[l] = source.scenario_buildings_available[l];
		}
		for (int m = 0; m < 6; m++)
		{
			scenarioOverview.scenario_start_siege_equipment[m] = source.scenario_start_siege_equipment[m];
		}
		for (int n = 0; n < 7; n++)
		{
			scenarioOverview.sa_troop_availability[n] = source.sa_troop_availability[n];
		}
		scenarioOverview.sa_fletcher = source.sa_fletcher;
		scenarioOverview.sa_blacksmith = source.sa_blacksmith;
		scenarioOverview.sa_poleturner = source.sa_poleturner;
		scenarioOverview.special_start_gold = source.special_start_gold;
		scenarioOverview.special_start = source.special_start;
		scenarioOverview.special_start_rationing = source.special_start_rationing;
		scenarioOverview.special_start_tax_rate = source.special_start_tax_rate;
		scenarioOverview.fast_goods_feedin = source.fast_goods_feedin;
		scenarioOverview.scenario_start_popularity = source.scenario_start_popularity;
		scenarioOverview.scenario_buildings_count = source.scenario_buildings_count;
		return scenarioOverview;
	}

	private static ev convertTL_event(evF source)
	{
		return new ev
		{
			onoff = source.onoff,
			type = source.type,
			value = source.value
		};
	}

	private static tl_event convertTL_event(tl_eventF source)
	{
		tl_event obj = new tl_event
		{
			month = source.month,
			year = source.year,
			tl_type = source.tl_type,
			done = source.done,
			pre_done = source.pre_done,
			action_data = source.action_data,
			action = source.action,
			and_or = source.and_or,
			repeat = source.repeat,
			repeat_count = source.repeat_count
		};
		int num = 0;
		obj.event_value[num++] = convertTL_event(source.event_value1);
		obj.event_value[num++] = convertTL_event(source.event_value2);
		obj.event_value[num++] = convertTL_event(source.event_value3);
		obj.event_value[num++] = convertTL_event(source.event_value4);
		obj.event_value[num++] = convertTL_event(source.event_value5);
		obj.event_value[num++] = convertTL_event(source.event_value6);
		obj.event_value[num++] = convertTL_event(source.event_value7);
		obj.event_value[num++] = convertTL_event(source.event_value8);
		obj.event_value[num++] = convertTL_event(source.event_value9);
		obj.event_value[num++] = convertTL_event(source.event_value10);
		obj.event_value[num++] = convertTL_event(source.event_value11);
		obj.event_value[num++] = convertTL_event(source.event_value12);
		obj.event_value[num++] = convertTL_event(source.event_value13);
		obj.event_value[num++] = convertTL_event(source.event_value14);
		obj.event_value[num++] = convertTL_event(source.event_value15);
		obj.event_value[num++] = convertTL_event(source.event_value16);
		obj.event_value[num++] = convertTL_event(source.event_value17);
		obj.event_value[num++] = convertTL_event(source.event_value18);
		obj.event_value[num++] = convertTL_event(source.event_value19);
		obj.event_value[num++] = convertTL_event(source.event_value20);
		obj.event_value[num++] = convertTL_event(source.event_value21);
		obj.event_value[num++] = convertTL_event(source.event_value22);
		obj.event_value[num++] = convertTL_event(source.event_value23);
		obj.event_value[num++] = convertTL_event(source.event_value24);
		obj.event_value[num++] = convertTL_event(source.event_value25);
		obj.event_value[num++] = convertTL_event(source.event_value26);
		obj.event_value[num++] = convertTL_event(source.event_value27);
		obj.event_value[num++] = convertTL_event(source.event_value28);
		obj.event_value[num++] = convertTL_event(source.event_value29);
		obj.event_value[num++] = convertTL_event(source.event_value30);
		obj.event_value[num++] = convertTL_event(source.event_value31);
		obj.event_value[num++] = convertTL_event(source.event_value32);
		obj.event_value[num++] = convertTL_event(source.event_value33);
		obj.event_value[num++] = convertTL_event(source.event_value34);
		obj.event_value[num++] = convertTL_event(source.event_value35);
		obj.event_value[num++] = convertTL_event(source.event_value36);
		obj.event_value[num++] = convertTL_event(source.event_value37);
		obj.event_value[num++] = convertTL_event(source.event_value38);
		obj.event_value[num++] = convertTL_event(source.event_value39);
		obj.event_value[num++] = convertTL_event(source.event_value40);
		return obj;
	}

	private static evF convertTL_event(ev source)
	{
		evF result = default(evF);
		result.onoff = source.onoff;
		result.type = source.type;
		result.value = source.value;
		return result;
	}

	private static tl_eventF convertTL_event(tl_event source)
	{
		tl_eventF result = default(tl_eventF);
		result.month = source.month;
		result.year = source.year;
		result.tl_type = source.tl_type;
		result.done = source.done;
		result.pre_done = source.pre_done;
		result.action_data = source.action_data;
		result.action = source.action;
		result.and_or = source.and_or;
		result.repeat = source.repeat;
		result.repeat_count = source.repeat_count;
		int num = 0;
		result.event_value1 = convertTL_event(source.event_value[num++]);
		result.event_value2 = convertTL_event(source.event_value[num++]);
		result.event_value3 = convertTL_event(source.event_value[num++]);
		result.event_value4 = convertTL_event(source.event_value[num++]);
		result.event_value5 = convertTL_event(source.event_value[num++]);
		result.event_value6 = convertTL_event(source.event_value[num++]);
		result.event_value7 = convertTL_event(source.event_value[num++]);
		result.event_value8 = convertTL_event(source.event_value[num++]);
		result.event_value9 = convertTL_event(source.event_value[num++]);
		result.event_value10 = convertTL_event(source.event_value[num++]);
		result.event_value11 = convertTL_event(source.event_value[num++]);
		result.event_value12 = convertTL_event(source.event_value[num++]);
		result.event_value13 = convertTL_event(source.event_value[num++]);
		result.event_value14 = convertTL_event(source.event_value[num++]);
		result.event_value15 = convertTL_event(source.event_value[num++]);
		result.event_value16 = convertTL_event(source.event_value[num++]);
		result.event_value17 = convertTL_event(source.event_value[num++]);
		result.event_value18 = convertTL_event(source.event_value[num++]);
		result.event_value19 = convertTL_event(source.event_value[num++]);
		result.event_value20 = convertTL_event(source.event_value[num++]);
		result.event_value21 = convertTL_event(source.event_value[num++]);
		result.event_value22 = convertTL_event(source.event_value[num++]);
		result.event_value23 = convertTL_event(source.event_value[num++]);
		result.event_value24 = convertTL_event(source.event_value[num++]);
		result.event_value25 = convertTL_event(source.event_value[num++]);
		result.event_value26 = convertTL_event(source.event_value[num++]);
		result.event_value27 = convertTL_event(source.event_value[num++]);
		result.event_value28 = convertTL_event(source.event_value[num++]);
		result.event_value29 = convertTL_event(source.event_value[num++]);
		result.event_value30 = convertTL_event(source.event_value[num++]);
		result.event_value31 = convertTL_event(source.event_value[num++]);
		result.event_value32 = convertTL_event(source.event_value[num++]);
		result.event_value33 = convertTL_event(source.event_value[num++]);
		result.event_value34 = convertTL_event(source.event_value[num++]);
		result.event_value35 = convertTL_event(source.event_value[num++]);
		result.event_value36 = convertTL_event(source.event_value[num++]);
		result.event_value37 = convertTL_event(source.event_value[num++]);
		result.event_value38 = convertTL_event(source.event_value[num++]);
		result.event_value39 = convertTL_event(source.event_value[num++]);
		result.event_value40 = convertTL_event(source.event_value[num++]);
		return result;
	}

	private static tl_message convertTL_message(tl_messageF source)
	{
		return new tl_message
		{
			month = source.month,
			year = source.year,
			tl_type = source.tl_type,
			done = source.done,
			pre_done = source.pre_done,
			message_id = source.message_id,
			action = source.action
		};
	}

	private static tl_messageF convertTL_message(tl_message source)
	{
		tl_messageF result = default(tl_messageF);
		result.month = source.month;
		result.year = source.year;
		result.tl_type = source.tl_type;
		result.done = source.done;
		result.pre_done = source.pre_done;
		result.message_id = source.message_id;
		result.action = source.action;
		return result;
	}

	private unsafe static tl_invasion convertTL_invasion(tl_invasionF source)
	{
		tl_invasion tl_invasion = new tl_invasion();
		tl_invasion.month = source.month;
		tl_invasion.year = source.year;
		tl_invasion.tl_type = source.tl_type;
		tl_invasion.done = source.done;
		tl_invasion.pre_done = source.pre_done;
		tl_invasion.total = source.total;
		for (int i = 0; i < 17; i++)
		{
			tl_invasion._size[i] = source._size[i];
		}
		tl_invasion.invasion_point = source.invasion_point;
		tl_invasion.start_year = source.start_year;
		tl_invasion.repeat = source.repeat;
		tl_invasion.from = source.from;
		tl_invasion.markerID = source.markerID;
		return tl_invasion;
	}

	private unsafe static tl_invasionF convertTL_invasion(tl_invasion source)
	{
		tl_invasionF result = default(tl_invasionF);
		result.month = source.month;
		result.year = source.year;
		result.tl_type = source.tl_type;
		result.done = source.done;
		result.pre_done = source.pre_done;
		result.total = source.total;
		for (int i = 0; i < 17; i++)
		{
			result._size[i] = source._size[i];
		}
		result.invasion_point = source.invasion_point;
		result.start_year = source.start_year;
		result.repeat = source.repeat;
		result.from = source.from;
		result.markerID = source.markerID;
		return result;
	}

	public unsafe static PlayState CopyPlayStateStruct(PlayStateReturnData source, int[] selectedChimps)
	{
		PlayState playState = new PlayState();
		for (int i = 0; i < 25; i++)
		{
			playState.resources[i] = source.resources[i];
			playState.keep_storage[i] = source.keep_storage[i];
		}
		playState.numSelectedChimps = source.numSelectedChimps;
		for (int j = 0; j < playState.numSelectedChimps; j++)
		{
			playState.selectedChimps[j] = selectedChimps[j];
		}
		playState.popularity = source.popularity;
		playState.population = source.population;
		playState.gold = source.gold;
		playState.housing_cap = source.housing_cap;
		playState.upcoming_total_popularity = source.upcoming_total_popularity;
		playState.rationing_popularity = source.rationing_popularity;
		playState.foodsEaten_popularity = source.foodsEaten_popularity;
		playState.food_popularity = source.food_popularity;
		playState.tax_popularity = source.tax_popularity;
		playState.overcrowding_popularity = source.overcrowding_popularity;
		playState.fearFactor_popularity = source.fearFactor_popularity;
		playState.religion_popularity = source.religion_popularity;
		playState.fairs_popularity = source.fairs_popularity;
		playState.plague_popularity = source.plague_popularity;
		playState.wolves_popularity = source.wolves_popularity;
		playState.bandits_popularity = source.bandits_popularity;
		playState.fire_popularity = source.fire_popularity;
		playState.marriage_popularity = source.marriage_popularity;
		playState.jester_popularity = source.jester_popularity;
		playState.good_things = source.good_things;
		playState.bad_things = source.bad_things;
		playState.fear_factor = source.fear_factor;
		playState.fear_factor_next_level = source.fear_factor_next_level;
		playState.efficiency = source.efficiency;
		for (int k = 0; k < 300; k++)
		{
			playState.population_graph[k] = source.population_graph[k];
		}
		for (int l = 0; l < 4; l++)
		{
			playState.food_types_not_eatable[l] = source.food_types_not_eatable[l];
		}
		for (int m = 0; m < 18; m++)
		{
			playState.troop_counts[m] = source.troop_counts[m];
		}
		playState.num_priests = source.num_priests;
		playState.blessed_percent = source.blessed_percent;
		playState.blessed_next_level_at = source.blessed_next_level_at;
		playState.tax_rate = source.tax_rate;
		playState.tax_amount = source.tax_amount;
		playState.peasants_available_for_troops = source.peasants_available_for_troops;
		for (int n = 0; n < 8; n++)
		{
			playState.make_troop_state[n] = source.make_troop_state[n];
		}
		playState.rationing = source.rationing;
		playState.food_clock = source.food_clock;
		playState.total_food = source.total_food;
		playState.months_of_food = source.months_of_food;
		playState.food_types_eaten = source.food_types_eaten;
		playState.food_types_available = source.food_types_available;
		playState.app_mode = source.app_mode;
		playState.app_sub_mode = source.app_sub_mode;
		playState.debug_value1 = source.debug_value1;
		playState.debug_value2 = source.debug_value2;
		playState.in_structure = source.in_structure;
		playState.in_structure_type = source.in_structure_type;
		playState.completeSelectionBox = source.completeSelectionBox;
		playState.in_chimp = source.in_chimp;
		playState.in_chimp_type = source.in_chimp_type;
		playState.inchimp_name1 = source.inchimp_name1;
		playState.inchimp_name2 = source.inchimp_name2;
		playState.dog_cage_state = source.dog_cage_state;
		playState.inchimp_n_text = source.inchimp_n_text;
		playState.in_chimp_goods = source.in_chimp_goods;
		playState.gatehouse_state = source.gatehouse_state;
		playState.repairs_allowed = source.repairs_allowed;
		playState.can_do_repairs = source.can_do_repairs;
		playState.building_hps_for_repair = source.building_hps_for_repair;
		playState.building_maxhps_for_repair = source.building_maxhps_for_repair;
		playState.sleep_allowed = source.sleep_allowed;
		playState.building_type_sleeping = source.building_type_sleeping;
		playState.have_building_stats = source.have_building_stats;
		playState.workers_have = source.workers_have;
		playState.job_vacancies = source.job_vacancies;
		playState.workers_needed = source.workers_needed;
		playState.got_keep_access = source.got_keep_access;
		playState.turned_off = source.turned_off;
		playState.working = source.working;
		playState.mill_message = source.mill_message;
		playState.pints_of_ale = source.pints_of_ale;
		playState.barrels_of_ale = source.barrels_of_ale;
		playState.working_inns = source.working_inns;
		playState.total_inns = source.total_inns;
		playState.inn_coverage_percent = source.inn_coverage_percent;
		playState.inn_coverage_popularity = source.inn_coverage_popularity;
		playState.inn_coverage_next = source.inn_coverage_next;
		playState.troops_show_disband = source.troops_show_disband;
		playState.troops_show_build_menu = source.troops_show_build_menu;
		playState.troops_show_make_catapult = source.troops_show_make_catapult;
		playState.troops_show_make_trebuchet = source.troops_show_make_trebuchet;
		playState.troops_show_make_siege_tower = source.troops_show_make_siege_tower;
		playState.troops_show_battering_ram = source.troops_show_battering_ram;
		playState.troops_show_portable_shield = source.troops_show_portable_shield;
		playState.troops_show_get_ammo = source.troops_show_get_ammo;
		playState.troops_show_launch_cow_and_num_cows = source.troops_show_launch_cow_and_num_cows;
		playState.troops_show_attack_here_and_type = source.troops_show_attack_here_and_type;
		playState.troops_show_attack_here_number_rocks = source.troops_show_attack_here_number_rocks;
		playState.troops_show_stance = source.troops_show_stance;
		playState.troops_show_patrol = source.troops_show_patrol;
		playState.troops_patrol_mode = source.troops_patrol_mode;
		playState.weapon_being_made_now = source.weapon_being_made_now;
		playState.game_type = source.game_type;
		playState.can_make_xbows = source.can_make_xbows;
		playState.can_make_sword = source.can_make_sword;
		playState.can_make_pike = source.can_make_pike;
		playState.weapon_being_made_next = source.weapon_being_made_next;
		playState.production_no_resources = source.production_no_resources;
		playState.playerdesc_message = source.playerdesc_message;
		playState.playerdesc_message2 = source.playerdesc_message2;
		playState.weapon_types_available = new byte[9];
		for (int num = 0; num < 9; num++)
		{
			playState.weapon_types_available[num] = source.weapon_types_available[num];
		}
		playState.troop_types_available = new byte[8];
		for (int num2 = 0; num2 < 8; num2++)
		{
			playState.troop_types_available[num2] = source.troop_types_available[num2];
		}
		playState.trade_buy_costs = new short[25];
		playState.trade_sell_costs = new short[25];
		playState.trade_buy_amounts = new short[25];
		playState.trade_sell_amounts = new short[25];
		playState.trade_sell_costs_fixed = new short[25];
		for (int num3 = 0; num3 < 25; num3++)
		{
			playState.trade_buy_costs[num3] = source.trade_buy_costs[num3];
			playState.trade_sell_costs[num3] = source.trade_sell_costs[num3];
			playState.trade_buy_amounts[num3] = source.trade_buy_amounts[num3];
			playState.trade_sell_amounts[num3] = source.trade_sell_amounts[num3];
			playState.trade_sell_costs_fixed[num3] = source.trade_sell_costs_fixed[num3];
		}
		playState.trading_current_goods = source.trading_current_goods;
		playState.trading_next_goods = source.trading_next_goods;
		playState.trading_prev_goods = source.trading_prev_goods;
		playState.marry_status = source.marry_status;
		playState.marry_male_type = source.marry_male_type;
		playState.marry_female_type = source.marry_female_type;
		playState.marry_text = source.marry_text;
		playState.marry_m_name1 = source.marry_m_name1;
		playState.marry_m_name2 = source.marry_m_name2;
		playState.marry_f_name1 = source.marry_f_name1;
		playState.marry_f_name2 = source.marry_f_name2;
		playState.blessed_popularity = source.blessed_popularity;
		playState.church_adjustment = (sbyte)source.church_adjustment;
		playState.church_missing = source.church_missing;
		playState.scribe_frame = source.scribe_frame;
		playState.total_horses_available = source.total_horses_available;
		playState.action_point_count = source.action_point_count;
		playState.action_points_x = new short[playState.action_point_count];
		playState.action_points_y = new short[playState.action_point_count];
		for (int num4 = 0; num4 < playState.action_point_count; num4++)
		{
			playState.action_points_x[num4] = source.action_points_x[num4];
			playState.action_points_y[num4] = source.action_points_y[num4];
		}
		playState.camera_target_x = source.camera_target_x;
		playState.camera_target_y = source.camera_target_y;
		playState.camera_target_z = source.camera_target_z;
		playState.rotateHappened = source.rotateHappened;
		playState.force_app_mode = source.force_app_mode;
		playState.month = source.month;
		playState.year = source.year;
		playState.pop_months = source.pop_months;
		playState.chimp_comments = source.chimp_comments;
		playState.camera_target_flat = source.camera_target_flat;
		playState.siege_that_saveable = source.siege_that_saveable;
		playState.inbuilding_help_id = source.inbuilding_help_id;
		playState.MP_Ahead_By = source.MP_Ahead_By;
		playState.MP_Behind_By = source.MP_Behind_By;
		playState.SkipFrame = source.SkipFrame;
		playState.undoAvailable = source.undoAvailable;
		playState.chimps_count = source.chimps_count;
		playState.chimps_limit = source.chimps_limit;
		playState.structs_count = source.structs_count;
		playState.structs_limit = source.structs_limit;
		playState.orgs_count = source.orgs_count;
		playState.orgs_limit = source.orgs_limit;
		playState.minerals_count = source.minerals_count;
		playState.minerals_limit = source.minerals_limit;
		playState.tribes_count = source.tribes_count;
		playState.tribes_limit = source.tribes_limit;
		playState.freeWoodcutter = source.freeWoodcutter;
		playState.freeGranary = source.freeGranary;
		playState.gotSignpost = source.gotSignpost;
		playState.repair_wood_needed = source.repair_wood_needed;
		playState.repair_stone_needed = source.repair_stone_needed;
		playState.panel_text_group = source.panel_text_group;
		playState.panel_text_text = source.panel_text_text;
		playState.free_buildingCheat = source.free_buildingCheat;
		playState.editor_time_paused = source.editor_time_paused;
		playState.bld_tiles_built = source.bld_tiles_built;
		playState.game_paused = source.game_paused;
		playState.numMPChatEntries = source.numMPChatEntries;
		playState.ai_clock = source.ai_clock;
		playState.lordOnlySelected = source.lordOnlySelected;
		playState.gotMarket = source.gotMarket;
		playState.keep_enclosed = source.keep_enclosed;
		playState.free2 = source.free2;
		playState.free3 = source.free3;
		playState.free4 = source.free4;
		playState.starting_teams = new byte[9];
		for (int num5 = 0; num5 < 9; num5++)
		{
			playState.starting_teams[num5] = source.starting_teams[num5];
		}
		playState.koth_scores = new int[8];
		playState.pingtimes = new short[8];
		playState.mpkick = new byte[8];
		for (int num6 = 0; num6 < 8; num6++)
		{
			playState.koth_scores[num6] = source.koth_scores[num6];
			playState.pingtimes[num6] = source.pingtimes[num6];
			playState.mpkick[num6] = source.mpkick[num6];
		}
		playState.chat_store_data = new short[10, 4];
		for (int num7 = 0; num7 < 40; num7++)
		{
			playState.chat_store_data[num7 % 10, num7 / 10] = source.chat_store_data[num7];
		}
		playState.autotrade_sell_amount = new short[25];
		playState.autotrade_buy_amount = new short[25];
		playState.autotrade_onoff = new byte[25];
		for (int num8 = 0; num8 < 25; num8++)
		{
			playState.autotrade_sell_amount[num8] = source.autotrade_sell_amount[num8];
			playState.autotrade_buy_amount[num8] = source.autotrade_buy_amount[num8];
			playState.autotrade_onoff[num8] = source.autotrade_onoff[num8];
		}
		if (source.control_groups_total[0] < 0)
		{
			playState.control_groups_total = new short[1];
			playState.control_groups_total[0] = -1;
		}
		else
		{
			playState.control_groups_match = new byte[10];
			playState.control_groups_total = new short[40];
			playState.control_groups_type = new byte[40];
			playState.control_groups_count = new short[40];
			for (int num9 = 0; num9 < 10; num9++)
			{
				playState.control_groups_match[num9] = source.control_groups_match[num9];
				playState.control_groups_total[num9] = source.control_groups_total[num9];
			}
			for (int num10 = 0; num10 < 40; num10++)
			{
				playState.control_groups_type[num10] = source.control_groups_type[num10];
				playState.control_groups_count[num10] = source.control_groups_count[num10];
			}
		}
		playState.markers_start_points = new int[10, 4];
		for (int num11 = 0; num11 < 40; num11++)
		{
			playState.markers_start_points[num11 % 10, num11 / 10] = source.markers_start_points[num11];
		}
		if (source.speechFileName[0] != 0)
		{
			int num12 = 0;
			for (int num13 = 0; num13 < 128; num13++)
			{
				if (source.speechFileName[num13] == 0)
				{
					num12 = num13;
					break;
				}
			}
			byte[] array = new byte[num12];
			for (int num14 = 0; num14 < num12; num14++)
			{
				array[num14] = source.speechFileName[num14];
			}
			playState.speechFileName = Encoding.ASCII.GetString(array);
		}
		if (source.musicFileName[0] != 0)
		{
			int num15 = 0;
			for (int num16 = 0; num16 < 128; num16++)
			{
				if (source.musicFileName[num16] == 0)
				{
					num15 = num16;
					break;
				}
			}
			byte[] array2 = new byte[num15];
			for (int num17 = 0; num17 < num15; num17++)
			{
				array2[num17] = source.musicFileName[num17];
			}
			playState.musicFileName = Encoding.ASCII.GetString(array2);
		}
		if (source.binkFileName[0] != 0)
		{
			int num18 = 0;
			for (int num19 = 0; num19 < 128; num19++)
			{
				if (source.binkFileName[num19] == 0)
				{
					num18 = num19;
					break;
				}
			}
			byte[] array3 = new byte[num18];
			for (int num20 = 0; num20 < num18; num20++)
			{
				array3[num20] = source.binkFileName[num20];
			}
			playState.binkFileName = Encoding.ASCII.GetString(array3);
		}
		return playState;
	}

	public static ScoreData convertScoreData(ScoreReturnData source)
	{
		ScoreData obj = new ScoreData
		{
			score_weapons = source.score_weapons,
			score_weapons_points = source.score_weapons_points,
			score = source.score,
			levelPoints = source.levelPoints,
			score_months = source.score_months,
			score_months_points = source.score_months_points,
			items_count = source.items_count,
			score_troops = source.score_troops,
			troops_percent_lost = source.troops_percent_lost,
			siege_that_score = source.siege_that_score,
			siege_defenders_score = source.siege_defenders_score,
			siege_attackers_score = source.siege_attackers_score,
			difficulty_level = source.difficulty_level,
			items_extra = new int[7]
		};
		obj.items_extra[0] = source.items_extra1;
		obj.items_extra[1] = source.items_extra2;
		obj.items_extra[2] = source.items_extra3;
		obj.items_extra[3] = source.items_extra4;
		obj.items_extra[4] = source.items_extra5;
		obj.items_extra[5] = source.items_extra6;
		obj.items_extra[6] = source.items_extra7;
		obj.items_extra_points = new int[7];
		obj.items_extra_points[0] = source.items_extra_points1;
		obj.items_extra_points[1] = source.items_extra_points2;
		obj.items_extra_points[2] = source.items_extra_points3;
		obj.items_extra_points[3] = source.items_extra_points4;
		obj.items_extra_points[4] = source.items_extra_points5;
		obj.items_extra_points[5] = source.items_extra_points6;
		obj.items_extra_points[6] = source.items_extra_points7;
		obj.items_extra_type = new int[7];
		obj.items_extra_type[0] = source.items_extra_type1;
		obj.items_extra_type[1] = source.items_extra_type2;
		obj.items_extra_type[2] = source.items_extra_type3;
		obj.items_extra_type[3] = source.items_extra_type4;
		obj.items_extra_type[4] = source.items_extra_type5;
		obj.items_extra_type[5] = source.items_extra_type6;
		obj.items_extra_type[6] = source.items_extra_type7;
		return obj;
	}

	public unsafe static MPScoreData convertMPStats(multiplayer_stats_export source)
	{
		MPScoreData mPScoreData = new MPScoreData();
		mPScoreData.valid = new int[9];
		mPScoreData.gold_acquired = new int[9];
		mPScoreData.max_population = new int[9];
		mPScoreData.fearfactor = new int[9];
		mPScoreData.time_deceased = new int[9];
		mPScoreData.who_killed_who = new int[9][];
		for (int i = 0; i < 9; i++)
		{
			mPScoreData.who_killed_who[i] = new int[9];
		}
		mPScoreData.enemy_buildings_destroyed = new int[9];
		mPScoreData.food_produced = new int[9];
		mPScoreData.iron_produced = new int[9];
		mPScoreData.stone_produced = new int[9];
		mPScoreData.wood_produced = new int[9];
		mPScoreData.pitch_produced = new int[9];
		mPScoreData.minfearfactor = new int[9];
		mPScoreData.lords_killed = new int[9];
		mPScoreData.king_of_the_hill_points = new int[9];
		mPScoreData.winners = new int[9];
		for (int j = 0; j < 9; j++)
		{
			mPScoreData.valid[j] = source.valid[j];
			mPScoreData.gold_acquired[j] = source.gold_acquired[j];
			mPScoreData.max_population[j] = source.max_population[j];
			mPScoreData.fearfactor[j] = source.fearfactor[j];
			mPScoreData.time_deceased[j] = source.time_deceased[j];
			mPScoreData.enemy_buildings_destroyed[j] = source.enemy_buildings_destroyed[j];
			mPScoreData.food_produced[j] = source.food_produced[j];
			mPScoreData.iron_produced[j] = source.iron_produced[j];
			mPScoreData.stone_produced[j] = source.stone_produced[j];
			mPScoreData.wood_produced[j] = source.wood_produced[j];
			mPScoreData.pitch_produced[j] = source.pitch_produced[j];
			mPScoreData.minfearfactor[j] = source.minfearfactor[j];
			mPScoreData.lords_killed[j] = source.lords_killed[j];
			mPScoreData.winners[j] = source.winners[j];
			for (int k = 0; k < 9; k++)
			{
				mPScoreData.who_killed_who[j][k] = source.who_killed_who[j * 9 + k];
			}
		}
		mPScoreData.king_of_the_hill_points_start = source.king_of_the_hill_points_start;
		return mPScoreData;
	}

	public static LoadMapReturnData loadTutorial()
	{
		lock (threadLock)
		{
			DLL_PreInitTutorial();
		}
		return loadMap(-1);
	}

	public static LoadMapReturnData loadCampaignMap(int campaignMapID, int difficulty = 1, bool mission6Prestart = false)
	{
		lock (threadLock)
		{
			DLL_PreInitMap_Campaign(difficulty);
		}
		return loadMap(campaignMapID, "", mission6Prestart);
	}

	public static LoadMapReturnData loadEcoCampaignMap(int campaignMapID, int difficulty = 1)
	{
		lock (threadLock)
		{
			DLL_PreInitMap_EcoCampaign();
			DLL_EcoCampaign_ChangeDifficulty(difficulty);
		}
		return loadMap(campaignMapID);
	}

	public static LoadMapReturnData loadExtraCampaignMap(int campaignMapID, int difficulty = 1)
	{
		lock (threadLock)
		{
			DLL_PreInitMap_EcoCampaign();
			DLL_EcoCampaign_ChangeDifficulty(difficulty);
		}
		return loadMap(campaignMapID);
	}

	public unsafe static void setEcoCampaignDifficulty(Enums.GameDifficulty difficulty)
	{
		int[] array = new int[160];
		int num = 0;
		lock (threadLock)
		{
			fixed (int* retData = array)
			{
				num = DLL_EcoCampaign_ChangeDifficulty_briefing((int)difficulty, retData);
			}
		}
		for (int i = 0; i < num; i++)
		{
			GameData.scenario.updateEvent(array[i * 4], 1, 0, array[i * 4 + 1], array[i * 4 + 2], array[i * 4 + 3]);
		}
	}

	public static LoadMapReturnData loadSiegeMap(string mapName, Enums.GameDifficulty difficulty, int playerID, int troop0, int troop1, int troop2, int troop3, int troop4, int troop5, int troop6, int troop7, int troop8, int troop9, int troop10, bool advancedMode, int trailType = 0, int trailID = 0)
	{
		lock (threadLock)
		{
			DLL_PreInitMap_SiegeThat((int)difficulty, playerID, troop0, troop1, troop2, troop3, troop4, troop5, troop6, troop7, troop8, troop9, troop10, advancedMode);
		}
		return loadMap(-1, mapName, mission6PreStart: false, multiplayerSave: false, trailType, trailID);
	}

	public static LoadMapReturnData loadInvasionMap(string mapName, Enums.GameDifficulty difficulty)
	{
		lock (threadLock)
		{
			DLL_PreInitMap_Invasion((int)difficulty);
		}
		return loadMap(-1, mapName);
	}

	public static LoadMapReturnData loadCustomEcoMap(string mapName, Enums.GameDifficulty difficulty)
	{
		lock (threadLock)
		{
			DLL_PreInitMap_EcoMap((int)difficulty);
		}
		return loadMap(-1, mapName);
	}

	public static LoadMapReturnData loadJustBuildMap(string mapName, bool advancedFreebuild, int freebuild_GoldLevel, int freebuild_FoodLevel, int freebuild_ResourcesLevel, int freebuild_WeaponsLevel, int freebuild_RandomEvents, int freebuild_Invasions, int freebuild_InvasionDifficulty, int freebuild_Peacetime)
	{
		lock (threadLock)
		{
			DLL_PreInitMap_JustBuild(advancedFreebuild, freebuild_GoldLevel, freebuild_FoodLevel, freebuild_ResourcesLevel, freebuild_WeaponsLevel, freebuild_RandomEvents, freebuild_Invasions, freebuild_InvasionDifficulty, freebuild_Peacetime);
		}
		return loadMap(-1, mapName);
	}

	public unsafe static LoadMapReturnData newMapEditor(int size, int gameType, bool siegeThat, bool multiplayerMap = false)
	{
		GameData.Instance.mission6Prestart = false;
		lock (threadLock)
		{
			byte[] array = new byte[Marshal.SizeOf(typeof(LoadMapReturnData))];
			fixed (byte* retData = array)
			{
				DLL_PreInitMap_Editor(size, gameType, siegeThat, multiplayerMap, retData);
			}
			int[] array2 = new int[9];
			fixed (int* retData2 = array2)
			{
				DLL_GetColourMapping(retData2, -1);
			}
			GameMap.instance.setColourMapping(array2);
			LoadMapReturnData result = Deserialize<LoadMapReturnData>(array);
			result.mapRotation++;
			return result;
		}
	}

	public unsafe static MultiplayerSetupData initMultiplayerGame()
	{
		byte[] array = new byte[Marshal.SizeOf(typeof(MultiplayerSetupTransferData))];
		lock (threadLock)
		{
			fixed (byte* retData = array)
			{
				DLL_PreInitMap_Multiplayer(retData);
			}
		}
		return getMPSetup(Deserialize<MultiplayerSetupTransferData>(array));
	}

	public unsafe static void setMultiplayerStartingData(MultiplayerSetupData setupData)
	{
		byte[] array = Serialize(setMPSetup(setupData));
		lock (threadLock)
		{
			fixed (byte* retData = array)
			{
				DLL_ApplyMultiplayerSetupData(retData);
			}
		}
	}

	public static LoadMapReturnData loadMultiplayerMap(string mapName, bool multiplayerSave = false)
	{
		return loadMap(-1, mapName, mission6PreStart: false, multiplayerSave);
	}

	public unsafe static MultiplayerSetupData getMultiplayerStartingData()
	{
		byte[] array = new byte[Marshal.SizeOf(typeof(MultiplayerSetupTransferData))];
		lock (threadLock)
		{
			fixed (byte* retData = array)
			{
				DLL_GetMultiplayerSetupData(retData);
			}
		}
		return getMPSetup(Deserialize<MultiplayerSetupTransferData>(array));
	}

	public unsafe static void RegisterMPPlayer(int playerID, string name, int team, bool localPlayer)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(name);
		lock (threadLock)
		{
			fixed (byte* name2 = bytes)
			{
				DLL_RegisterMultiplayerUser(playerID, name2, bytes.Length, team, localPlayer);
			}
		}
	}

	public static int StartMultiplayerGame(bool fromSave)
	{
		lock (threadLock)
		{
			return DLL_StartMultiplayerGame(fromSave);
		}
	}

	public static int StartMultiplayerGameSynced()
	{
		lock (threadLock)
		{
			return DLL_StartMultiplayerGameSynced();
		}
	}

	public unsafe static void RemapPlayers(int[] newMappings, int newLocalPlayer)
	{
		lock (threadLock)
		{
			fixed (int* newMappings2 = newMappings)
			{
				DLL_RemapPlayers(newMappings2, newLocalPlayer);
			}
		}
	}

	public unsafe static void SetMPRadarColours(int[] newMappings)
	{
		lock (threadLock)
		{
			fixed (int* newMappings2 = newMappings)
			{
				DLL_SetMPRadarColours(newMappings2);
			}
		}
	}

	public static void ConnectionPauseEngine(bool state)
	{
		lock (threadLock)
		{
			DLL_ConnectionPause(state);
		}
	}

	public static void SetMPRandSeed(int seed)
	{
		lock (threadLock)
		{
			DLL_SetMPRandSeed(seed);
		}
	}

	public unsafe static void ReceiveChore(int playerID, byte[] data, int dataLength)
	{
		lock (threadLock)
		{
			fixed (byte* data2 = data)
			{
				DLL_ReceiveChore(playerID, data2, dataLength);
			}
		}
	}

	public unsafe static void GetMultiplayerChatInfo(ref int[] players, ref int[] teams)
	{
		lock (threadLock)
		{
			fixed (int* players2 = players)
			{
				fixed (int* teams2 = teams)
				{
					DLL_GetMultiplayerChatInfo(players2, teams2);
				}
			}
		}
	}

	public static void KickMPPlayer(int playerID, bool kickImmediate)
	{
		lock (threadLock)
		{
			DLL_KickMPPlayer(playerID, kickImmediate);
		}
	}

	public static void PromoteMPHost(int playerID)
	{
		lock (threadLock)
		{
			DLL_PromoteMPHost(playerID);
		}
	}

	public static void SetAchData(int food, int wood, int weapons)
	{
		lock (threadLock)
		{
			DLL_SetAchValues(food, wood, weapons);
		}
	}

	public unsafe static LoadMapReturnData LoadSaveFile(string path)
	{
		GameData.Instance.mission6Prestart = false;
		lock (threadLock)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(path);
			byte[] array = new byte[Marshal.SizeOf(typeof(LoadMapReturnData))];
			int num;
			fixed (byte* data = bytes)
			{
				fixed (byte* retData = array)
				{
					num = DLL_LoadSaveGame(data, bytes.Length, retData, loadingEditorMap: false);
				}
			}
			if (num > 0)
			{
				int[] array2 = new int[9];
				fixed (int* retData2 = array2)
				{
					DLL_GetColourMapping(retData2, ConfigSettings.Settings_PlayerColour + 1);
				}
				GameMap.instance.setColourMapping(array2);
				LoadMapReturnData result = Deserialize<LoadMapReturnData>(array);
				result.mapRotation++;
				return result;
			}
			return default(LoadMapReturnData);
		}
	}

	public unsafe static LoadMapReturnData LoadMapFile(string path, bool editorMode)
	{
		lock (threadLock)
		{
			fixed (byte* retData = new byte[Marshal.SizeOf(typeof(LoadMapReturnData))])
			{
				DLL_PreInitMap_Editor(160, 0, siegeThat: false, multiplayerMap: false, retData);
			}
			byte[] bytes = Encoding.Unicode.GetBytes(path);
			byte[] array = new byte[Marshal.SizeOf(typeof(LoadMapReturnData))];
			int num;
			fixed (byte* data = bytes)
			{
				fixed (byte* retData2 = array)
				{
					num = DLL_LoadSaveGame(data, bytes.Length, retData2, editorMode);
				}
			}
			if (num > 0)
			{
				int[] array2 = new int[9];
				fixed (int* retData3 = array2)
				{
					DLL_GetColourMapping(retData3, ConfigSettings.Settings_PlayerColour + 1);
				}
				GameMap.instance.setColourMapping(array2);
				LoadMapReturnData result = Deserialize<LoadMapReturnData>(array);
				result.mapRotation++;
				return result;
			}
			return default(LoadMapReturnData);
		}
	}

	public unsafe static bool SaveSaveGame(string path, int screenCentreX, int screenCentreY, int realScreenCentreX, int realScreenCentreY, bool lockMap = false, bool tempLockOnly = false, bool mapSave = false)
	{
		int num = 0;
		if (Director.instance.SafeToSave(wait: true))
		{
			lock (threadLock)
			{
				byte[] bytes = Encoding.Unicode.GetBytes(path);
				fixed (byte* data = bytes)
				{
					num = DLL_SaveSaveGame(data, bytes.Length, screenCentreX, screenCentreY, realScreenCentreX, realScreenCentreY, lockMap, tempLockOnly, mapSave);
				}
			}
			Director.instance.FinishedSaving();
		}
		return num > 0;
	}

	private unsafe static LoadMapReturnData loadMap(int campaignMapID, string fileName = "", bool mission6PreStart = false, bool multiplayerSave = false, int trailType = 0, int trailID = 0)
	{
		GameData.Instance.mission6Prestart = mission6PreStart;
		lock (threadLock)
		{
			flattenedLandscape = false;
			string fileName2 = Path.GetFileName(fileName);
			byte[] bytes = Encoding.Unicode.GetBytes(fileName2);
			byte[] bytes2 = Encoding.Unicode.GetBytes(fileName);
			byte[] array = new byte[Marshal.SizeOf(typeof(LoadMapReturnData))];
			fixed (byte* fileName3 = bytes2)
			{
				fixed (byte* retData = array)
				{
					fixed (byte* mapName = bytes)
					{
						DLL_LoadMapToPlay(campaignMapID, fileName3, bytes2.Length, retData, mission6PreStart, mapName, bytes.Length, multiplayerSave, trailType, trailID);
					}
				}
			}
			int[] array2 = new int[9];
			fixed (int* retData2 = array2)
			{
				DLL_GetColourMapping(retData2, ConfigSettings.Settings_PlayerColour + 1);
			}
			GameMap.instance.setColourMapping(array2);
			LoadMapReturnData result = Deserialize<LoadMapReturnData>(array);
			result.mapRotation++;
			EditorDirector.instance.clearMouseStateForEngine();
			return result;
		}
	}

	public unsafe static int[] GetScenarioTroopsInfo(string mapName)
	{
		lock (threadLock)
		{
			flattenedLandscape = false;
			int[] array = new int[11];
			byte[] bytes = Encoding.Unicode.GetBytes(mapName);
			fixed (int* retData = array)
			{
				fixed (byte* data = bytes)
				{
					DLL_GetScenarioTroopsInfo(data, bytes.Length, retData);
				}
			}
			return array;
		}
	}

	public unsafe static int GetCampaignLevel(string mapName)
	{
		lock (threadLock)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(mapName);
			fixed (byte* path = bytes)
			{
				return DLL_CampaignLevel(path, bytes.Length);
			}
		}
	}

	public static void toggleFlattenedLandscapeMode()
	{
		flattenedLandscape = !flattenedLandscape;
		if (GameData.Instance.game_type == 4)
		{
			if (!flattenedLandscape)
			{
				TutorialAction(5);
			}
			else
			{
				TutorialAction(4);
			}
		}
		EditorDirector.instance.FlattenedLandscape();
	}

	public static void setFlattenedLandscapeMode(bool state)
	{
		flattenedLandscape = state;
	}

	public unsafe static int run(bool mpFrameSkip = false)
	{
		MemoryBuffers.MemBuffer freeBuffer = MemoryBuffers.instance.getFreeBuffer(writing: true);
		if (freeBuffer != null)
		{
			lock (threadLock)
			{
				int mouseOverX = -1;
				int mouseOverY = -1;
				EditorDirector.instance.preDLLCallActions(ref mouseOverX, ref mouseOverY);
				byte[] array = new byte[Marshal.SizeOf(typeof(PlayStateReturnData))];
				int[] array2 = new int[3000];
				int num = 0;
				fixed (short* data = freeBuffer.memory)
				{
					fixed (byte* radarMap = freeBuffer.radarMap)
					{
						fixed (byte* retData = array)
						{
							fixed (byte* choreBuffer = freeBuffer.MPChores)
							{
								fixed (int* selectedChimpsBuffer = array2)
								{
									num = DLL_RunTick(data, radarMap, flattenedLandscape, mouseOverX, mouseOverY, EditorDirector.instance.shiftPressed, EditorDirector.instance.ctrlPressed, EditorDirector.instance.altPressed, retData, Director.instance.Paused, MyAudioManager.Instance.isAmbientPlaying(1), MyAudioManager.Instance.isAmbientPlaying(2), MyAudioManager.Instance.isSpeechPlaying(1), MyAudioManager.Instance.isSpeechPlaying(2), MyAudioManager.Instance.isMusicPlaying(), MyAudioManager.Instance.isMusicAboutToLoop(), SFXManager.instance.isBinkPlaying(), GameMap.instance.ScreenCentreTileScreenSpaceX, GameMap.instance.ScreenCentreTileScreenSpaceY, GameMap.instance.ScreenTilesWide, GameMap.instance.ScreenTilesHigh, GameMap.instance.RadarMapWidth, GameMap.instance.RadarMapHeight, GameMap.instance.RadarZoom, GameMap.instance.ScreenZoom, ConfigSettings.Settings_SH1RTSControls, GameMap.instance.ScreenCentreTileX, GameMap.instance.ScreenCentreTileY, choreBuffer, selectedChimpsBuffer, mpFrameSkip, MainControls.instance.mouseTileClickDepth - 49, EditorDirector.instance.lastTroopOverDepth);
								}
							}
						}
					}
				}
				PlayStateReturnData source = Deserialize<PlayStateReturnData>(array);
				freeBuffer.gameState = CopyPlayStateStruct(source, array2);
				if (freeBuffer.gameState.SkipFrame > 0)
				{
					Director.instance.MPSkipFrame(freeBuffer.gameState.SkipFrame);
				}
				freeBuffer.numTiles = num;
				MemoryBuffers.instance.returnBuffer(freeBuffer);
				return num;
			}
		}
		return 0;
	}

	public unsafe static byte[] unpack(byte[] source)
	{
		if (source.Length <= 4)
		{
			return null;
		}
		lock (threadLock)
		{
			int num = 0;
			fixed (byte* source2 = source)
			{
				num = DLL_GetunpackSize(source2);
				if (num == 0 || num >= 10000000)
				{
					return null;
				}
				byte[] array = new byte[num];
				fixed (byte* dest = array)
				{
					DLL_Unpack(source2, dest, num);
					return array;
				}
			}
		}
	}

	public unsafe static byte[] pack(byte[] source)
	{
		lock (threadLock)
		{
			byte[] array = new byte[source.Length + 1000];
			int num = 0;
			fixed (byte* source2 = source)
			{
				fixed (byte* dest = array)
				{
					num = DLL_Pack(source2, dest, source.Length);
				}
			}
			if (num > 0)
			{
				byte[] array2 = new byte[num];
				Array.Copy(array, array2, num);
				return array2;
			}
			return null;
		}
	}

	public unsafe static byte[] unpackSavedRadar(byte[] source)
	{
		lock (threadLock)
		{
			byte[] array = new byte[160000];
			fixed (byte* source2 = source)
			{
				fixed (byte* dest = array)
				{
					if (DLL_UnpackRadarToARGB(source2, dest) > 0)
					{
						return array;
					}
					return null;
				}
			}
		}
	}

	public unsafe static byte[] getSaveRadar()
	{
		lock (threadLock)
		{
			byte[] array = new byte[160000];
			fixed (byte* dest = array)
			{
				if (DLL_GetSaveRadar(dest) > 0)
				{
					return array;
				}
				return null;
			}
		}
	}

	public unsafe static int crc(byte[] source)
	{
		lock (threadLock)
		{
			fixed (byte* source2 = source)
			{
				return DLL_CRC(source2, source.Length);
			}
		}
	}

	public static void SetMapRotation(Enums.Dircs rotation, int centreX, int centreY)
	{
		lock (threadLock)
		{
			DLL_SetMapRotation((int)(rotation - 1), centreX, centreY);
		}
	}

	public static int StartMapperItem(int item)
	{
		lock (threadLock)
		{
			return DLL_StartMapAction(item);
		}
	}

	public static void PlaceMapperItem(int item, int x, int y, int size, int player, bool inGameNotEditor, bool constructingOnly, int mouseState)
	{
		lock (threadLock)
		{
			int num = DLL_MapAction(item, x, y, size, player, inGameNotEditor, constructingOnly, mouseState);
			if (num >= 0)
			{
				if (MainViewModel.Instance.MEMode == 0 && Enum.IsDefined(typeof(Enums.eMappers), num))
				{
					EditorDirector.instance.mapEditorInteraction((Enums.eMappers)num);
				}
			}
			else if (num == -50)
			{
				EditorDirector.instance.CancelPlacement();
			}
		}
	}

	public static int GameAction(Enums.GameActionCommand command, int structureID, int state)
	{
		lock (threadLock)
		{
			int num = DLL_GameAction((int)command, structureID, state);
			if (num > 0)
			{
				switch (command)
				{
				case Enums.GameActionCommand.MakeTroop:
				case Enums.GameActionCommand.BuyGoods:
				case Enums.GameActionCommand.SellGoods:
					return num;
				case Enums.GameActionCommand.RotateBuilding:
					MainControls.instance.CurrentSubAction = num;
					break;
				default:
					if (Enum.IsDefined(typeof(Enums.eMappers), num))
					{
						EditorDirector.instance.placeBuilding((Enums.eMappers)num);
					}
					break;
				}
			}
			return 0;
		}
	}

	public static void GameAction(Enums.KeyFunctions command, int value1 = -1, int value2 = 0)
	{
		lock (threadLock)
		{
			DLL_GameAction((int)command, value1, value2);
		}
	}

	public static void SetAutoTrade(int goods, bool on, int buyLevel, int sellLevel)
	{
		lock (threadLock)
		{
			DLL_GameAction(1052, goods, buyLevel);
			DLL_GameAction(1053, goods, sellLevel);
			if (on)
			{
				DLL_GameAction(1051, goods, 1);
			}
			else
			{
				DLL_GameAction(1051, goods, 0);
			}
		}
	}

	public unsafe static void TroopSelection(int mouseState, bool rightDown, bool rightUp, int[] selectedChimps, bool selection_on, bool selection_established, int[] underCursorChimps, int mousePosX, int mousePosY, bool overTopHalf, int[] onScreenChimps)
	{
		if (onScreenChimps == null)
		{
			onScreenChimps = new int[0];
		}
		if (underCursorChimps == null)
		{
			underCursorChimps = new int[0];
		}
		lock (threadLock)
		{
			if (selectedChimps != null)
			{
				fixed (int* selectedChimps2 = selectedChimps)
				{
					fixed (int* underCursorChimps2 = underCursorChimps)
					{
						fixed (int* onScreenChimps2 = onScreenChimps)
						{
							DLL_TroopSelection(mouseState, rightDown, rightUp, selectedChimps.Length, selectedChimps2, selection_on, selection_established, underCursorChimps.Length, underCursorChimps2, mousePosX, mousePosY, overTopHalf, onScreenChimps.Length, onScreenChimps2);
						}
					}
				}
				return;
			}
			fixed (int* selectedChimps3 = new int[0])
			{
				fixed (int* underCursorChimps3 = underCursorChimps)
				{
					fixed (int* onScreenChimps3 = onScreenChimps)
					{
						DLL_TroopSelection(mouseState, rightDown, rightUp, 0, selectedChimps3, selection_on, selection_established, underCursorChimps.Length, underCursorChimps3, mousePosX, mousePosY, overTopHalf, onScreenChimps.Length, onScreenChimps3);
					}
				}
			}
		}
	}

	public unsafe static void TroopSelectionChanged(int[] selectedChimps)
	{
		lock (threadLock)
		{
			if (selectedChimps != null)
			{
				fixed (int* selectedChimps2 = selectedChimps)
				{
					DLL_TroopSelectionChanged(selectedChimps.Length, selectedChimps2);
				}
			}
			else
			{
				fixed (int* selectedChimps3 = new int[0])
				{
					DLL_TroopSelectionChanged(0, selectedChimps3);
				}
			}
		}
	}

	public static void DeleteBuilding(int x, int y, int player = -1, bool inGameNotEditor = true, int mouseState = 0)
	{
		lock (threadLock)
		{
			DLL_MapAction(39, x, y, 0, player, inGameNotEditor, constructingOnly: false, mouseState);
		}
	}

	public static bool IsMapperAvailable(int mapper)
	{
		lock (threadLock)
		{
			return DLL_IsMapperAvailable(mapper) > 0;
		}
	}

	public static bool GetMapperCoords(int mapper, ref int x, ref int y)
	{
		lock (threadLock)
		{
			int num = DLL_GetMapperCoord(mapper);
			if (num == 1431655765)
			{
				return false;
			}
			x = (num >> 16) & 0xFFFF;
			y = num & 0xFFFF;
			return true;
		}
	}

	public static void SetEditorPlayer(int playerID)
	{
		lock (threadLock)
		{
			DLL_SetEditorPlayer(playerID);
		}
	}

	public unsafe static void SetUTF8MissionText(string missionText)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(missionText);
		lock (threadLock)
		{
			fixed (byte* text = bytes)
			{
				DLL_SetUTF8MissionText(text, bytes.Length);
			}
		}
	}

	public unsafe static void SetUTF8MapName(string mapName)
	{
		GameData.Instance.currentMapName = mapName;
		byte[] array = Encoding.ASCII.GetBytes(mapName);
		if (array.Length > 78)
		{
			byte[] array2 = new byte[78];
			for (int i = 0; i < 78; i++)
			{
				array2[i] = array[i];
			}
			array = array2;
		}
		byte[] bytes = Encoding.UTF8.GetBytes(mapName);
		lock (threadLock)
		{
			fixed (byte* text = bytes)
			{
				fixed (byte* text2 = array)
				{
					DLL_SetUTF8MapName(text, bytes.Length, text2, array.Length);
				}
			}
		}
	}

	public static tl_event CreateNewScenarioEvent(ref int eventid)
	{
		eventid = DLL_CreateScenarioAction(3);
		if (eventid >= 0)
		{
			return GetScenarioEvent(eventid);
		}
		return null;
	}

	public static tl_invasion CreateNewScenarioInvasion(ref int eventid)
	{
		eventid = DLL_CreateScenarioAction(1);
		if (eventid >= 0)
		{
			return GetScenarioInvasion(eventid);
		}
		return null;
	}

	public static tl_message CreateNewScenarioMessage(ref int eventid)
	{
		eventid = DLL_CreateScenarioAction(2);
		if (eventid >= 0)
		{
			return GetScenarioMessage(eventid);
		}
		return null;
	}

	public unsafe static tl_event GetScenarioEvent(int eventID)
	{
		byte[] array = new byte[Marshal.SizeOf(typeof(tl_eventF))];
		lock (threadLock)
		{
			fixed (byte* retData = array)
			{
				DLL_GetScenarioEvent(eventID, retData);
			}
		}
		return convertTL_event(Deserialize<tl_eventF>(array));
	}

	public unsafe static tl_message GetScenarioMessage(int eventID)
	{
		byte[] array = new byte[Marshal.SizeOf(typeof(tl_messageF))];
		lock (threadLock)
		{
			fixed (byte* retData = array)
			{
				DLL_GetScenarioEvent(eventID, retData);
			}
		}
		return convertTL_message(Deserialize<tl_messageF>(array));
	}

	public unsafe static tl_invasion GetScenarioInvasion(int eventID)
	{
		byte[] array = new byte[Marshal.SizeOf(typeof(tl_invasionF))];
		lock (threadLock)
		{
			fixed (byte* retData = array)
			{
				DLL_GetScenarioInvasion(eventID, retData);
			}
		}
		return convertTL_invasion(Deserialize<tl_invasionF>(array));
	}

	public unsafe static ScenarioOverview GetScenarioOverview()
	{
		byte[] array = new byte[Marshal.SizeOf(typeof(ScenarioOverviewReturnData))];
		lock (threadLock)
		{
			fixed (byte* retData = array)
			{
				DLL_GetScenarioOverview(retData);
			}
			if (!GameData.Instance.importedStrings)
			{
				int[] array2 = new int[1000];
				int[] array3 = new int[1000];
				fixed (int* messageIDs = array2)
				{
					fixed (int* messageTypes = array3)
					{
						DLL_GetScenarioMessageList(messageIDs, messageTypes);
					}
				}
				GameData.Instance.importScenarioTextInfo(array2, array3);
			}
		}
		return convertScenarioOverview(Deserialize<ScenarioOverviewReturnData>(array));
	}

	public unsafe static void ApplyScenarioEvent(int eventID, tl_event evnt)
	{
		byte[] array = Serialize(convertTL_event(evnt));
		lock (threadLock)
		{
			fixed (byte* retData = array)
			{
				DLL_ApplyScenarioEvent(eventID, retData);
			}
		}
	}

	public unsafe static void ApplyScenarioMessage(int eventID, tl_message msg)
	{
		byte[] array = Serialize(convertTL_message(msg));
		lock (threadLock)
		{
			fixed (byte* retData = array)
			{
				DLL_ApplyScenarioMessage(eventID, retData);
			}
		}
	}

	public unsafe static void ApplyScenarioInvasion(int eventID, tl_invasion inv)
	{
		byte[] array = Serialize(convertTL_invasion(inv));
		lock (threadLock)
		{
			fixed (byte* retData = array)
			{
				DLL_ApplyScenarioInvasion(eventID, retData);
			}
		}
	}

	public static void DeleteScenarioEntry(int entryID)
	{
		lock (threadLock)
		{
			DLL_DeleteScenarioAction(entryID);
		}
	}

	public static void UpdateScenarioActionDate(int entryID, int year, int month)
	{
		lock (threadLock)
		{
			DLL_UpdateScenarioActionDate(entryID, year, month);
		}
	}

	public static int EditorChangeMap_Mode(bool changeToMP)
	{
		if (changeToMP)
		{
			return DLL_SetMapEditorParam(1, -1, -1, -1);
		}
		return DLL_SetMapEditorParam(0, -1, -1, -1);
	}

	public static int EditorChangeMap_GameType(Enums.GameModes mapType)
	{
		return DLL_SetMapEditorParam(-1, (int)mapType, -1, -1);
	}

	public static int EditorChangeMap_KotH(bool koth)
	{
		if (koth)
		{
			return DLL_SetMapEditorParam(-1, -1, 1, -1);
		}
		return DLL_SetMapEditorParam(-1, -1, 0, -1);
	}

	public static int EditorChangeMap_MapSize(int mapSize)
	{
		return DLL_SetMapEditorParam(-1, -1, -1, mapSize);
	}

	public static void SetAppMode(int app_mode, int app_sub_mode)
	{
		lock (threadLock)
		{
			DLL_SetAppMode(app_mode, app_sub_mode);
		}
	}

	public static void TutorialAction(int ID, int value = -1)
	{
		lock (threadLock)
		{
			DLL_TutorialAction(ID, value);
		}
	}

	public unsafe static ScoreData GetScoreData()
	{
		byte[] array = new byte[Marshal.SizeOf(typeof(ScoreReturnData))];
		lock (threadLock)
		{
			fixed (byte* retData = array)
			{
				DLL_GetScoreData(retData);
			}
		}
		return convertScoreData(Deserialize<ScoreReturnData>(array));
	}

	public unsafe static MPScoreData GetMPScoreData()
	{
		byte[] array = new byte[Marshal.SizeOf(typeof(multiplayer_stats_export))];
		lock (threadLock)
		{
			fixed (byte* retData = array)
			{
				DLL_GetMPScoreData(retData);
			}
		}
		return convertMPStats(Deserialize<multiplayer_stats_export>(array));
	}

	public unsafe static LogicDebugInfo GetLayerDebug(int x, int y)
	{
		lock (threadLock)
		{
			byte[] array = new byte[Marshal.SizeOf(typeof(LogicDebugInfo))];
			fixed (byte* retData = array)
			{
				DLL_GetLayerDebug(x, y, retData);
			}
			return Deserialize<LogicDebugInfo>(array);
		}
	}

	public unsafe static byte[] GetLayerData(int layerID)
	{
		lock (threadLock)
		{
			byte[] array = new byte[160000];
			fixed (byte* retData = array)
			{
				DLL_GetLayerData(layerID, retData);
			}
			return array;
		}
	}

	public static void SetDebugMode(int action)
	{
		lock (threadLock)
		{
			DLL_SetDebugMode(action);
		}
	}

	public static byte[] Serialize<T>(T s) where T : struct
	{
		int num = Marshal.SizeOf(typeof(T));
		byte[] array = new byte[num];
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.StructureToPtr(s, intPtr, fDeleteOld: true);
		Marshal.Copy(intPtr, array, 0, num);
		Marshal.FreeHGlobal(intPtr);
		return array;
	}

	public static T Deserialize<T>(byte[] array) where T : struct
	{
		int num = Marshal.SizeOf(typeof(T));
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.Copy(array, 0, intPtr, num);
		T result = (T)Marshal.PtrToStructure(intPtr, typeof(T));
		Marshal.FreeHGlobal(intPtr);
		return result;
	}

	public static T DeserializeStr<T>(byte[] array) where T : class
	{
		int num = Marshal.SizeOf(typeof(T));
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.Copy(array, 0, intPtr, num);
		T result = (T)Marshal.PtrToStructure(intPtr, typeof(T));
		Marshal.FreeHGlobal(intPtr);
		return result;
	}
}
