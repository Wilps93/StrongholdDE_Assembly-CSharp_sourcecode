using System;
using System.Collections.Generic;
using System.IO;
using Stronghold1DE;
using UnityEngine;

public class SFXManager
{
	private class sh1_sound_effect
	{
		public int first_buffer_no;

		public int max_variants;

		public int variants_loaded;

		public int last_variant_played;
	}

	private class sh1_sound
	{
		public float volume;

		public int position;

		public int requests;

		public float real_volume;

		public string name;

		public AudioClip clip;
	}

	private class VolumeData
	{
		public string name;

		public float volume;
	}

	public static SFXManager instance;

	private readonly string[,] stronghold_main_list = new string[254, 8]
	{
		{ "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\button4 22k", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\chop1 22k", "fx\\chop2 22k", "fx\\chop3 22k", "fx\\chop4 22k", "Null", "Null", "Null", "Null" },
		{ "fx\\sawpull1 22k", "fx\\sawpush1 22k", "fx\\sawpull2 22k", "fx\\sawpush2 22k", "fx\\sawpull3 22k", "fx\\sawpush3 22k", "Null", "Null" },
		{ "fx\\stocks1", "fx\\stocks2", "fx\\stocks5", "fx\\stocks7", "Null", "Null", "Null", "Null" },
		{ "fx\\bowtwang 22k", "fx\\arrowswish1 22k", "fx\\arrowswish2 22k", "fx\\arrowshoot1 22k", "Null", "Null", "Null", "Null" },
		{ "fx\\arrowhit4 22k", "fx\\arrowhit4 22k", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\ilplop_01", "fx\\lilplop_02", "fx\\lilplop_03", "fx\\lilplop_04", "Null", "Null", "Null", "Null" },
		{ "fx\\medplop_01", "fx\\medplop_02", "fx\\medplop_03", "fx\\medplop_04", "Null", "Null", "Null", "Null" },
		{ "fx\\drop_plank1", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\mill", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\inn_01", "fx\\inn_02", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\mason_chip1", "fx\\mason_chip2", "fx\\mason_chip3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\mason_crumble1", "fx\\mason_crumble2", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\puller_lower", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\puller_strain", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\puller_rock", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\puller_impact", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\puller_return", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\armycharge1", "fx\\armycharge2", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\pryer_lever1", "fx\\pryer_lever2", "fx\\pryer_lever3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\drawbridge_lowering", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\drawbridge_lowered", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\drawbridge_raising", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\drawbridge_raised", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\drawbridge_control", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\iron_dump1", "fx\\iron_dump2", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\iron_lildump1", "fx\\iron_lildump2", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\iron_boil1", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\iron_pour1", "fx\\iron_pour2", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\iron_pull1", "fx\\iron_pull2", "fx\\iron_pull7", "fx\\iron_pull4", "fx\\iron_pull5", "fx\\iron_pull6", "fx\\iron_pull3", "Null" },
		{ "fx\\iron_straining1", "fx\\iron_straining2", "fx\\iron_straining3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\stckfood1", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\stckale1", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\stckhops1", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\stckiron2", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\stckpitch2", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\stckstone1", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\stckweap2", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\stckwheat1", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\plank1", "fx\\plank2", "fx\\plank3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\bigtreefall1", "fx\\bigtreefall2", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\liltreefall", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\bs_anvil4", "fx\\bs_anvil2", "fx\\bs_anvil3", "fx\\bs_anvil1", "fx\\bs_anvil5", "Null", "Null", "Null" },
		{ "fx\\bs_bellow1", "fx\\bs_bellow3", "fx\\bs_bellow4", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\bs_cooling2", "fx\\bs_cooling3", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\bs_pour3", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\bs_open4", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\bs_file10", "fx\\bs_file12", "fx\\bs_file13", "fx\\bs_file9", "Null", "Null", "Null", "Null" },
		{ "fx\\bakebig1", "fx\\bakebig4", "fx\\bakebig5", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\bakesmall2", "fx\\bakesmall3", "fx\\bakesmall4", "fx\\bakesmall5", "Null", "Null", "Null", "Null" },
		{ "fx\\mudbub1", "fx\\mudbub2", "fx\\mudbub3", "fx\\mudbub4", "fx\\mudbub5", "fx\\mudbub6", "fx\\mudbub7", "fx\\mudbub8" },
		{ "fx\\pit_waterlap1", "fx\\pit_waterlap2", "fx\\pit_waterlap3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\pit_scoop1", "fx\\pit_scoop2", "fx\\pit_scoop3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\pit_pour5", "fx\\pit_pour6", "fx\\pit_pour7", "fx\\pit_pour8", "Null", "Null", "Null", "Null" },
		{ "fx\\tan_cut4", "fx\\tan_lilcut7", "fx\\tan_cut5", "fx\\tan_lilcut8", "fx\\tan_cut6", "fx\\tan_lilcut9", "Null", "Null" },
		{ "fx\\tan_upbrush1", "fx\\tan_upbrush2", "fx\\tan_upbrush3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\tan_dnbrush1", "fx\\tan_dnbrush2", "fx\\tan_dnbrush3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\regbow_sand_01", "fx\\regbow_sand_02", "fx\\regbow_sand_03", "fx\\regbow_sand_04", "fx\\regbow_sand_05", "fx\\regbow_sand_06", "fx\\regbow_sand_07", "fx\\regbow_sand_08" },
		{ "fx\\ghost3", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\cauldron_01", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\stir1", "fx\\stir2", "fx\\stir3", "fx\\stir4", "fx\\stir5", "fx\\stir6", "Null", "Null" },
		{ "fx\\fireloop1", "fx\\fireloop2", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\arrbounce1", "fx\\arrbounce4", "fx\\arrbounce5", "fx\\arrbounce7", "Null", "Null", "Null", "Null" },
		{ "fx\\swclang1", "fx\\swclang2", "fx\\swclang3", "fx\\swcombi1", "fx\\swcombi4", "fx\\swhit1", "fx\\swhit11", "fx\\swhit15" },
		{ "fx\\swhit3", "fx\\swhit8", "fx\\swhit9", "fx\\swscrape1", "fx\\swclang4", "fx\\swclang5", "fx\\swcombi8", "fx\\swcombi9" },
		{ "fx\\pole_turn1", "fx\\pole_turn2", "fx\\pole_turn3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\pole_grind2", "fx\\pole_grind3", "fx\\pole_grind6", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\moatdig1", "fx\\moatdig2", "fx\\moatdig3", "fx\\moatdig4", "Null", "Null", "Null", "Null" },
		{ "fx\\cbow_01", "fx\\cbow_02", "fx\\cbow_03", "fx\\cbow_04", "fx\\cbow_05", "Null", "Null", "Null" },
		{ "fx\\cbowwind_01", "fx\\cbowwind_02", "fx\\cbowwind_03", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\bearattack_1", "fx\\bearattack_2", "fx\\bearattack_3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\beardies_1", "fx\\beardies_2", "fx\\beardies_3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\cow_slaughter", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\milking_1", "fx\\milking_2", "fx\\milking_3", "fx\\milking_4", "fx\\milking_5", "fx\\milking_6", "fx\\milking_7", "Null" },
		{ "fx\\cowmoo_1", "fx\\cowmoo_2", "fx\\cowmoo_3", "fx\\cowmoo_4", "Null", "Null", "Null", "Null" },
		{ "fx\\milkpour", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\dogdblbark_1", "fx\\dogdblbark_2", "fx\\dogtalk_1", "fx\\dogtalk_4", "fx\\dogsingbark_1", "fx\\dogsingbark_2", "fx\\dogsingbark_3", "fx\\dogtalk_5" },
		{ "fx\\dogdies_1", "fx\\dogdies_2", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\dogpant", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\dogtalk_6", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\broom_1", "fx\\broom_2", "fx\\broom_3", "fx\\broom_4", "fx\\broom_5", "Null", "Null", "Null" },
		{ "fx\\sharpen_sht_1", "fx\\sharpen_med_1", "fx\\sharpen_lng_1", "fx\\sharpen_sht_2", "fx\\sharpen_med_2", "fx\\sharpen_lng_2", "Null", "Null" },
		{ "fx\\deerfall_1", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\huntercut_01", "fx\\huntercut_02", "fx\\huntercut_03", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\trot_sing_01", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\trot_mult_01", "fx\\trot_mult_02", "fx\\trot_mult_03", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\trot_mult_04", "fx\\trot_mult_05", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\whinny_s_02", "fx\\whinny_m_01", "fx\\whinny_s_03", "fx\\whinny_m_02", "Null", "Null", "Null", "Null" },
		{ "fx\\horsedie_01", "fx\\horsedie_02", "fx\\horsedie_03", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\cowhitsdust", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\wabbitdies_1", "fx\\wabbitdies_4", "fx\\wabbitdies_5", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\wolfdies_1", "fx\\wolfdies_2", "fx\\wolfdies_3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\wolfattack_1", "fx\\wolfattack_2", "fx\\wolfattack_3", "fx\\wolfattack_4", "fx\\wolfattack_5", "Null", "Null", "Null" },
		{ "fx\\pantlick_1", "fx\\pantlick_2", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\armourhit_01", "fx\\armourhit_02", "fx\\armourhit_03", "fx\\armourhit_04", "fx\\armourhit_05", "Null", "Null", "Null" },
		{ "fx\\burn9", "fx\\burn10", "fx\\burn3", "fx\\burn4", "fx\\burn5", "fx\\burn6", "fx\\burn7", "fx\\burn8" },
		{ "fx\\pot_flareup_1", "fx\\pot_flareup_2", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\opencldrn_01", "fx\\opencldrn_02", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\burn1", "fx\\burn2", "fx\\burn3", "fx\\burn4", "fx\\burn5", "fx\\burn6", "fx\\burn7", "fx\\burn8" },
		{ "fx\\ignite_oil", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\oildump_1", "fx\\oildump_2", "fx\\oildump_3", "fx\\oildump_4", "Null", "Null", "Null", "Null" },
		{ "fx\\menusl_1", "fx\\menusl_2", "fx\\menusl_3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\siegeroll1", "fx\\siegeroll2", "fx\\siegeroll3", "fx\\siegeroll4", "Null", "Null", "Null", "Null" },
		{ "fx\\ca_load1", "fx\\ca_load2", "fx\\ca_load3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\ca_fire1", "fx\\ca_fire2", "fx\\ca_fire3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\ma_load1", "fx\\ma_load2", "fx\\ma_load3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\ma_fire1", "fx\\ma_fire2", "fx\\ma_fire3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\tr_load1", "fx\\tr_load2", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\tr_fire1", "fx\\tr_fire2", "fx\\tr_fire3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\trebdie_1", "fx\\trebdie_2", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\siegedie_1", "fx\\siegedie_2", "fx\\siegedie_3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\bighit_01", "fx\\bighit_02", "fx\\bighit_03", "fx\\bighit_04", "fx\\bighit_05", "fx\\bighit_06", "Null", "Null" },
		{ "fx\\miss_l_01", "fx\\miss_l_02", "fx\\miss_l_03", "fx\\miss_s_01", "Null", "Null", "Null", "Null" },
		{ "fx\\woodrattle_1", "fx\\woodrattle_2", "fx\\woodrattle_3", "fx\\woodrattle_4", "Null", "Null", "Null", "Null" },
		{ "fx\\clubdth_01", "fx\\clubdth_02", "fx\\clubdth_03", "fx\\clubdth_04", "fx\\clubdth_05", "fx\\clubdth_06", "fx\\clubdth_07", "fx\\clubdth_08" },
		{ "fx\\arrwdth_01", "fx\\arrwdth_02", "fx\\arrwdth_03", "fx\\arrwdth_04", "fx\\arrwdth_05", "fx\\arrwdth_06", "fx\\arrwdth_07", "fx\\arrwdth_08" },
		{ "fx\\speardth_01", "fx\\speardth_02", "fx\\speardth_03", "fx\\speardth_04", "fx\\speardth_05", "fx\\speardth_06", "fx\\speardth_07", "fx\\speardth_08" },
		{ "fx\\swdth_01", "fx\\swdth_02", "fx\\swdth_03", "fx\\swdth_04", "fx\\swdth_05", "fx\\swdth_06", "fx\\swdth_07", "fx\\swdth_08" },
		{ "fx\\hit_01", "fx\\hit_02", "fx\\hit_03", "fx\\hit_04", "fx\\hit_05", "fx\\hit_06", "fx\\hit_07", "fx\\hit_08" },
		{ "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\ignite_pitch", "fx\\ignite_oil", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\metpush7", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\metpush12", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\metpush13", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\metpush15", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\metpush5", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\metpush1", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\metrollover3a", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\metrollover13", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\metrollover15", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\metrollover2", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\metrollover4", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\metrollover12", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\woodpush2", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\woodrollover7", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\begauk", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\chicflap_01", "fx\\chicflap_02", "fx\\chicflap_03", "fx\\chicflap_04", "Null", "Null", "Null", "Null" },
		{ "fx\\clucking1", "fx\\clucking2", "fx\\clucking3", "fx\\clucking4", "Null", "Null", "Null", "Null" },
		{ "fx\\portdrop1", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\portdrop1a", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\portlift1", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\portlift1a", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\maypole_01", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\swish1", "fx\\swish4", "fx\\swish5", "fx\\swish7", "fx\\swish8", "fx\\swish9", "fx\\swish13", "fx\\swish14" },
		{ "fx\\shieldrollover", "fx\\shieldrollover", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\portdrop1b", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\arrowstab_01", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\snort_01", "fx\\snort_02", "fx\\snort_03", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\towersmash_01", "fx\\towersmash_02", "fx\\towersmash_03", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\clubdth_09", "fx\\clubdth_10", "fx\\clubdth_11", "fx\\clubdth_12", "fx\\clubdth_13", "fx\\clubdth_14", "fx\\clubdth_15", "fx\\clubdth_16" },
		{ "fx\\arrwdth_09", "fx\\arrwdth_10", "fx\\arrwdth_11", "fx\\arrwdth_12", "fx\\arrwdth_13", "fx\\arrwdth_14", "fx\\arrwdth_15", "fx\\arrwdth_16" },
		{ "fx\\speardth_09", "fx\\speardth_10", "fx\\speardth_11", "fx\\speardth_12", "fx\\speardth_13", "fx\\speardth_14", "fx\\speardth_15", "fx\\speardth_16" },
		{ "fx\\swdth_09", "fx\\swdth_10", "fx\\swdth_11", "fx\\swdth_12", "fx\\swdth_13", "fx\\swdth_14", "fx\\swdth_15", "fx\\swdth_16" },
		{ "fx\\hit_09", "fx\\hit_10", "fx\\hit_11", "fx\\hit_12", "fx\\hit_13", "fx\\hit_14", "fx\\hit_15", "fx\\hit_16" },
		{ "fx\\hit_17", "fx\\hit_18", "fx\\hit_19", "fx\\hit_20", "fx\\hit_21", "fx\\hit_22", "fx\\hit_23", "fx\\hit_24" },
		{ "fx\\hit_25", "fx\\hit_26", "fx\\hit_27", "fx\\hit_28", "fx\\hit_29", "fx\\hit_30", "fx\\hit_31", "fx\\hit_32" },
		{ "fx\\tunnel1", "fx\\tunnel2", "fx\\tunnel3", "fx\\tunnel4", "fx\\tunnel5", "Null", "Null", "Null" },
		{ "fx\\tunnel6", "fx\\tunnel7", "fx\\tunnel8", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\walldrop_01", "fx\\walldrop_02", "fx\\walldrop_03", "fx\\walldrop_04", "Null", "Null", "Null", "Null" },
		{ "fx\\droplog", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\babycoo_01", "fx\\babycry_01", "fx\\babycoo_02", "fx\\babycry_02", "fx\\babycoo_03", "fx\\babycry_03", "fx\\babyhappy_01", "Null" },
		{ "fx\\metrock_01", "fx\\metrock_02", "fx\\metrock_03", "fx\\metrock_04", "fx\\metrock_05", "fx\\metrock_06", "fx\\metrock_07", "fx\\metrock_08" },
		{ "fx\\woodhit_01", "fx\\woodhit_02", "fx\\woodhit_03", "fx\\woodhit_04", "fx\\woodhit_05", "fx\\woodhit_06", "fx\\woodhit_07", "fx\\woodhit_08" },
		{ "fx\\splatdeath_01", "fx\\splatdeath_02", "fx\\splatdeath_03", "fx\\splatdeath_04", "Null", "Null", "Null", "Null" },
		{ "fx\\cowsplat_01", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\deerrun", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\baliscrank_01", "fx\\baliscrank_02", "fx\\baliscrank_03", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\ballistalaunch_01", "fx\\ballistalaunch_02", "fx\\ballistalaunch_03", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\buildingwreck_01", "fx\\buildingwreck_01", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\shielddie_01", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\flamearrow_01", "fx\\flamearrow_02", "fx\\flamearrow_03", "fx\\flamearrow_04", "Null", "Null", "Null", "Null" },
		{ "fx\\oneswdsmanwalk_01", "fx\\oneswdsmanwalk_02", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\twoswdsmanwalk_01", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\multswdsmanwalk_01", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\rocksplash_01", "fx\\rocksplash_02", "fx\\rocksplash_03", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\swing_01", "fx\\swing_02", "fx\\swing_03", "fx\\swing_04", "Null", "Null", "Null", "Null" },
		{ "fx\\ramhit_01", "fx\\ramhit_02", "fx\\ramhit_03", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\sheathin_01", "fx\\sheathin_02", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\sheathout_01", "fx\\sheathout_02", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\metpush15", "fx\\metpush15", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\girlydie_01", "fx\\girlydie_02", "fx\\girlydie_03", "fx\\girlydie_04", "fx\\girlydie_05", "fx\\girlydie_06", "fx\\girlydie_07", "fx\\girlydie_08" },
		{ "fx\\girlyscream_01", "fx\\girlyscream_02", "fx\\girlyscream_03", "fx\\girlyscream_04", "Null", "Null", "Null", "Null" },
		{ "fx\\arrowbasic_01", "fx\\arrowbasic_02", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\macebasic_01", "fx\\macebasic_02", "fx\\macebasic_03", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\pikebasic_01", "fx\\pikebasic_02", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\spearbasic_01", "fx\\spearbasic_02", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\swordbasic_01", "fx\\swordbasic_02", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\flies_01", "fx\\flies_02", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\harvest_01", "fx\\harvest_02", "fx\\harvest_03", "fx\\harvest_04", "fx\\harvest_05", "fx\\harvest_06", "Null", "Null" },
		{ "fx\\hoe_01", "fx\\hoe_02", "fx\\hoe_03", "fx\\hoe_04", "fx\\hoe_05", "fx\\hoe_06", "fx\\hoe_07", "Null" },
		{ "fx\\lonewolf_1", "fx\\multwolves_4", "fx\\lonewolf_2", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\dogcage", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\oxdeath", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\ladder_01", "fx\\ladder_02", "fx\\ladder_03", "fx\\ladder_04", "fx\\ladder_05", "fx\\ladder_06", "Null", "Null" },
		{ "fx\\ladderbreak_01", "fx\\ladderbreak_02", "fx\\ladderbreak_03", "fx\\ladderbreak_04", "fx\\ladderbreak_05", "fx\\ladderbreak_06", "Null", "Null" },
		{ "fx\\jesterdie_01", "fx\\jesterdie_02", "fx\\jesterdie_03", "fx\\jesterdie_04", "fx\\jesterdie_05", "fx\\jesterdie_06", "fx\\jesterdie_07", "fx\\jesterdie_08" },
		{ "fx\\lorddie_01", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\pigdie_01", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\crowmulti_01", "fx\\crowmulti_02", "fx\\crowsingular_01", "fx\\crowsingular_02", "fx\\crowsingular_03", "Null", "Null", "Null" },
		{ "fx\\gulls_01", "fx\\gulls_02", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\ironrefill", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\flagsmall_01", "fx\\flagsmall_02", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\flaglarge_01", "fx\\flaglarge_02", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\snakedie_01", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\wolfdie_01", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\chapel", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\church_01", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\cathedral_01", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\stretch", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\gallows", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\dungeon1", "fx\\dungeon2", "fx\\dungeon3", "fx\\dungeon4", "Null", "Null", "Null", "Null" },
		{ "fx\\gulldive1", "fx\\gulldive2", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\gullsurface1", "fx\\gullsurface2", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\quickbreath1", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\quickbreath2", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\liftchair1", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\dunkchair1", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\girlgrunt1", "fx\\girlgrunt2", "fx\\girlgrunt3", "fx\\girlgrunt4", "fx\\girlgrunt5", "fx\\girlgrunt6", "fx\\girlgrunt7", "fx\\girlgrunt8" },
		{ "fx\\fireout1", "fx\\fireout2", "fx\\fireout3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\firepop1", "fx\\firepop2", "fx\\firepop3", "fx\\firepop4", "fx\\firepop5", "fx\\firepop6", "fx\\firepop7", "fx\\firepop8" },
		{ "fx\\throwwater1", "fx\\throwwater2", "fx\\throwwater3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\burnwitch1", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\screamwitch2", "fx\\screamwitch3", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\building_placement", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\building_placement_small", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\apothecary_explosion", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\miller_short", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\miller_long", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\pick_apple1", "fx\\pick_apple2", "fx\\pick_apple3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\pick_hops1", "fx\\pick_hops2", "fx\\pick_hops3", "fx\\pick_hops4", "fx\\pick_hops5", "Null", "Null", "Null" },
		{ "fx\\gibbet_01", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\ox_select_01", "fx\\ox_select_02", "fx\\ox_select_03", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\ox_walk_01", "fx\\ox_walk_02", "fx\\ox_walk_03", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\marketplace_01", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\siegetower_dock1", "fx\\siegetower_dock2", "fx\\siegetower_dock3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\xbow_sand_01", "fx\\xbow_sand_02", "fx\\xbow_sand_03", "fx\\xbow_sand_04", "fx\\xbow_sand_05", "fx\\xbow_sand_06", "fx\\xbow_sand_07", "fx\\xbow_sand_08" },
		{ "fx\\xbow_inspect_01", "fx\\xbow_inspect_02", "fx\\xbow_inspect_03", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\xbow_hammerpickup_01", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\xbow_hammer_tap_01", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\xbow_hammer_tap_02", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\xbow_putdown_01", "fx\\xbow_putdown_02", "fx\\xbow_putdown_03", "fx\\xbow_putdown_04", "fx\\xbow_putdown_05", "Null", "Null", "Null" },
		{ "fx\\dungeon_whip_01", "fx\\dungeon_whip_02", "fx\\dungeon_whip_03", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\stocks_click", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\woodwall_placement", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\stonewall_placement", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\woodplatform_placement", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\stonetower_placement", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\woodrollover3", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\stonebuilding_placement", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\battlehorn", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "", "", "", "", "", "", "", "" }
	};

	private readonly string[,] stronghold_ambient_list = new string[11, 8]
	{
		{ "fx\\wind_short1", "fx\\wind_short2", "fx\\wind_short3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\gust1 22k", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\ocean_short1", "fx\\ocean_short2", "fx\\ocean_short3", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\firelp_1", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\waterfalllp_01", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\streamlp_02", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "fx\\birdsloop_01", "fx\\birdsloop_02", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null" },
		{ "", "", "", "", "", "", "", "" }
	};

	private readonly string[] scribeSpeech = new string[238]
	{
		"Food_Double.wav", "Food_Extra.wav", "Food_Falling.wav", "Food_Growing.wav", "Food_Half.wav", "Food_None.wav", "Food_Normal.wav", "Food_Warning1.wav", "Food_Warning2.wav", "Food_Warning3.wav",
		"Food_Warning4.wav", "Food_Warning5.wav", "General_Fear1.wav", "General_Fear10.wav", "General_Fear2.wav", "General_Fear3.wav", "General_Fear4.wav", "General_Fear5.wav", "General_Fear6.wav", "General_Fear7.wav",
		"General_Fear8.wav", "General_Fear9.wav", "General_Loading.wav", "General_Message1.wav", "General_Message10.wav", "General_Message11.wav", "General_Message12.wav", "General_Message13.wav", "General_Message14.wav", "General_Message15.wav",
		"General_Message16.wav", "General_Message17.wav", "General_Message18.wav", "General_Message2.wav", "General_Message3.wav", "General_Message4.wav", "General_Message5.wav", "General_Message6.wav", "General_Message7.wav", "General_Message8.wav",
		"General_Message9.wav", "General_Quitgame.wav", "General_Saving.wav", "General_Startgame.wav", "General_Victory1.wav", "General_Victory2.wav", "General_Victory3.wav", "General_Victory4.wav", "General_Victory5.wav", "General_Victory6.wav",
		"General_Victory7.wav", "General_Victory8.wav", "General_Warning1.wav", "General_Warning10.wav", "General_Warning11.wav", "General_Warning12.wav", "General_Warning13.wav", "General_Warning14.wav", "General_Warning15.wav", "General_Warning16.wav",
		"General_Warning2.wav", "General_Warning3.wav", "General_Warning4.wav", "General_Warning5.wav", "General_Warning6.wav", "General_Warning7.wav", "General_Warning8.wav", "General_Warning9.wav", "Other_Warning1.wav", "Other_Warning10.wav",
		"Other_Warning11.wav", "Other_Warning12.wav", "Other_Warning2.wav", "Other_Warning3.wav", "Other_Warning4.wav", "Other_Warning5.wav", "Other_Warning6.wav", "Other_Warning7.wav", "Other_Warning8.wav", "Other_Warning9.wav",
		"Pig_Attack.wav", "Pig_Defeat.wav", "Placement_Warning1.wav", "Placement_Warning10.wav", "Placement_Warning11.wav", "Placement_Warning12.wav", "Placement_Warning13.wav", "Placement_Warning14.wav", "Placement_Warning15.wav", "Placement_Warning16.wav",
		"Placement_Warning17.wav", "placement_warning18.wav", "placement_warning19.wav", "Placement_Warning2.wav", "placement_warning20.wav", "placement_warning21.wav", "Placement_Warning3.wav", "Placement_Warning4.wav", "Placement_Warning5.wav", "Placement_Warning6.wav",
		"Placement_Warning7.wav", "Placement_Warning8.wav", "Placement_Warning9.wav", "Pop_Emigrate.wav", "Pop_Falling.wav", "Pop_Immigrate.wav", "Pop_Popularity1.wav", "Pop_Popularity2.wav", "Pop_Popularity3.wav", "Pop_Popularity4.wav",
		"Pop_Popularity5.wav", "Pop_Popularity6.wav", "Pop_Popularity7.wav", "Pop_Popularity8.wav", "Pop_Rising.wav", "Pop_Stable.wav", "Random_Events1.wav", "Random_Events10.wav", "Random_Events11.wav", "Random_Events12.wav",
		"Random_Events13.wav", "Random_Events2.wav", "Random_Events3.wav", "Random_Events4.wav", "Random_Events5.wav", "Random_Events6.wav", "Random_Events7.wav", "Random_Events9.wav", "Rat_Attack.wav", "Rat_Defeat.wav",
		"Resource_Need1.wav", "Resource_Need10.wav", "Resource_Need11.wav", "Resource_Need12.wav", "Resource_Need13.wav", "Resource_Need14.wav", "Resource_Need15.wav", "Resource_Need16.wav", "Resource_Need17.wav", "Resource_Need18.wav",
		"Resource_Need19.wav", "Resource_Need2.wav", "Resource_Need20.wav", "Resource_Need21.wav", "Resource_Need22.wav", "Resource_Need23.wav", "Resource_Need24.wav", "Resource_Need25.wav", "Resource_Need26.wav", "Resource_Need27.wav",
		"Resource_Need28.wav", "Resource_Need3.wav", "Resource_Need4.wav", "Resource_Need5.wav", "Resource_Need6.wav", "Resource_Need7.wav", "Resource_Need8.wav", "Resource_Need9.wav", "Snake_Attack.wav", "Snake_Defeat.wav",
		"Space_Warning1.wav", "Space_Warning2.wav", "Space_Warning3.wav", "Space_Warning4.wav", "Space_Warning5.wav", "Space_Warning6.wav", "Space_Warning7.wav", "Space_Warning8.wav", "Taxes_Constant.wav", "Taxes_Decrease1.wav",
		"Taxes_Decrease2.wav", "Taxes_Increase1.wav", "Taxes_Increase2.wav", "Taxes_Rate1.wav", "Taxes_Rate2.wav", "Taxes_Rate3.wav", "Taxes_Rate4.wav", "Taxes_Rate5.wav", "Taxes_Rate6.wav", "Taxes_Rate7.wav",
		"Taxes_Rate8.wav", "Units_Warning1.wav", "Units_Warning2.wav", "Units_Warning3.wav", "Wolf_Attack.wav", "Wolf_Defeat.wav", "General_Message18.wav", "General_Message19.wav", "General_Message20.wav", "General_Message21.wav",
		"General_Message22.wav", "General_Message23.wav", "General_Message24.wav", "General_Message25.wav", "General_Message26.wav", "General_Message27.wav", "General_Message28.wav", "General_Message29.wav", "General_Message30.wav", "Camp_Comp.wav",
		"Eco_Camp_Comp.wav", "DE_Camp1_Comp.wav", "DE_Camp2_Comp.wav", "DE_Camp3_Comp.wav", "DE_Camp4_Comp.wav", "DE_Eco_Camp_Comp.wav", "Castle_Trail_Comp.wav", "MP_Victory_1.wav", "MP_Victory_2.wav", "MP_Victory_3.wav",
		"MP_Defeat_1.wav", "MP_Defeat_2.wav", "MP_Defeat_3.wav", "MP_Defeat_4.wav", "MP_Defeat_5.wav", "MP_Defeat_6.wav", "Siege_Att_Victory_1.wav", "Siege_Att_Victory_2.wav", "Siege_Att_Victory_3.wav", "Siege_Att_Defeat_1.wav",
		"Siege_Att_Defeat_2.wav", "Siege_Att_Defeat_3.wav", "Siege_Def_Victory_1.wav", "Siege_Def_Victory_2.wav", "Siege_Def_Victory_3.wav", "Siege_Def_Defeat_1.wav", "Siege_Def_Defeat_2.wav", "Siege_Def_Defeat_3.wav", "Invasion_Victory_1.wav", "Invasion_Victory_2.wav",
		"Invasion_Victory_3.wav", "Eco_Victory_1.wav", "Eco_Victory_2.wav", "Eco_Victory_3.wav", "Freebuild_Playtime_1.wav", "Freebuild_Playtime_2.wav", "Freebuild_Playtime_3.wav", "Workshop_Publish_1.wav"
	};

	private readonly string[] aiSpeech = new string[80]
	{
		"pg_anger_01.wav", "pg_anger_02.wav", "pg_anger_03.wav", "pg_anger_04.wav", "pg_plead_01.wav", "pg_plead_02.wav", "pg_plead_03.wav", "pg_plead_04.wav", "pg_taunt_01.wav", "pg_taunt_02.wav",
		"pg_taunt_03.wav", "pg_taunt_04.wav", "pg_taunt_05.wav", "pg_taunt_06.wav", "pg_taunt_07.wav", "pg_taunt_08.wav", "pg_vict_01.wav", "pg_vict_02.wav", "pg_vict_03.wav", "pg_vict_04.wav",
		"rt_anger_01.wav", "rt_anger_02.wav", "rt_anger_03.wav", "rt_anger_04.wav", "rt_plead_01.wav", "rt_plead_02.wav", "rt_plead_03.wav", "rt_plead_04.wav", "rt_taunt_01.wav", "rt_taunt_02.wav",
		"rt_taunt_03.wav", "rt_taunt_04.wav", "rt_taunt_05.wav", "rt_taunt_06.wav", "rt_taunt_07.wav", "rt_taunt_08.wav", "rt_vict_01.wav", "rt_vict_02.wav", "rt_vict_03.wav", "rt_vict_04.wav",
		"sn_anger_01.wav", "sn_anger_02.wav", "sn_anger_03.wav", "sn_anger_04.wav", "sn_plead_01.wav", "sn_plead_02.wav", "sn_plead_03.wav", "sn_plead_04.wav", "sn_taunt_01.wav", "sn_taunt_02.wav",
		"sn_taunt_03.wav", "sn_taunt_04.wav", "sn_taunt_05.wav", "sn_taunt_06.wav", "sn_taunt_07.wav", "sn_taunt_08.wav", "sn_vict_01.wav", "sn_vict_02.wav", "sn_vict_03.wav", "sn_vict_04.wav",
		"wf_anger_01.wav", "wf_anger_02.wav", "wf_anger_03.wav", "wf_anger_04.wav", "wf_plead_01.wav", "wf_plead_02.wav", "wf_plead_03.wav", "wf_plead_04.wav", "wf_taunt_01.wav", "wf_taunt_02.wav",
		"wf_taunt_03.wav", "wf_taunt_04.wav", "wf_taunt_05.wav", "wf_taunt_06.wav", "wf_taunt_07.wav", "wf_taunt_08.wav", "wf_vict_01.wav", "wf_vict_02.wav", "wf_vict_03.wav", "wf_vict_04.wav"
	};

	private readonly string[] inMissionSpeech = new string[65]
	{
		"ap_civil_01.wav", "ap_civil_02.wav", "ap_civil_03.wav", "ap_civil_04.wav", "ap_civil_05.wav", "ap_civil_06.wav", "ap_civil_07.wav", "ap_civil_08.wav", "ap_civil_09.wav", "ap_civil_10.wav",
		"ap_civil_11.wav", "ap_civil_12.wav", "ap_civil_13.wav", "ap_civil_14.wav", "ap_civil_15.wav", "ap_civil_16.wav", "ap_civil_17.wav", "ap_civil_18.wav", "ap_civil_19.wav", "ap_civil_20.wav",
		"ap_civil_21.wav", "ap_civil_22.wav", "ap_civil_23.wav", "ap_civil_24.wav", "ap_civil_25.wav", "ap_civil_26.wav", "ap_civil_27.wav", "ap_civil_28.wav", "ap_civil_29.wav", "ap_milit_01.wav",
		"ap_milit_02.wav", "ap_milit_03.wav", "ap_milit_04.wav", "ap_milit_05.wav", "ap_milit_06.wav", "ap_milit_07.wav", "ap_milit_08.wav", "ap_milit_09.wav", "ap_milit_10.wav", "ap_milit_11.wav",
		"ap_milit_12.wav", "ap_milit_13.wav", "ap_milit_14.wav", "ap_milit_15.wav", "ap_milit_16.wav", "ap_milit_17.wav", "ap_milit_18.wav", "ap_milit_19.wav", "ap_milit_20.wav", "Enemy_Attack1.wav",
		"Enemy_Attack10.wav", "Enemy_Attack11.wav", "Enemy_Attack12.wav", "Enemy_Attack13.wav", "Enemy_Attack14.wav", "Enemy_Attack15.wav", "Enemy_Attack16.wav", "Enemy_Attack2.wav", "Enemy_Attack3.wav", "Enemy_Attack4.wav",
		"Enemy_Attack5.wav", "Enemy_Attack6.wav", "Enemy_Attack7.wav", "Enemy_Attack8.wav", "Enemy_Attack9.wav"
	};

	private readonly string[] insultSpeech = new string[20]
	{
		"Insult1.wav", "Insult2.wav", "Insult3.wav", "Insult4.wav", "Insult5.wav", "insult6.wav", "insult7.wav", "insult8.wav", "insult9.wav", "insult10.wav",
		"Insult11.wav", "Insult12.wav", "Insult13.wav", "Insult14.wav", "Insult15.wav", "Insult16.wav", "Insult17.wav", "Insult18.wav", "Insult19.wav", "Insult20.wav"
	};

	private readonly string[] stronghold_names_speech_list = new string[526]
	{
		"Allison", "fx\\speech\\Name1.wav", "Andrea", "fx\\speech\\Name3.wav", "Annabelle", "fx\\speech\\Name5.wav", "Anna", "fx\\speech\\Name4.wav", "Anne Marie", "fx\\speech\\Name7.wav",
		"Anne", "fx\\speech\\Name6.wav", "Beth", "fx\\speech\\Name9.wav", "Betty", "fx\\speech\\Name10.wav", "Bonnie", "fx\\speech\\Name12.wav", "Camille", "fx\\speech\\Name13.wav",
		"Cindy", "fx\\speech\\Name14.wav", "Collette", "fx\\speech\\Name15.wav", "Darlene", "fx\\speech\\Name16.wav", "Dianne", "fx\\speech\\Name17.wav", "Elizabeth", "fx\\speech\\Name19.wav",
		"Ellen", "fx\\speech\\Name20.wav", "Emma", "fx\\speech\\Name21.wav", "Gabriel", "fx\\speech\\Name22.wav", "Heather", "fx\\speech\\Name23.wav", "Heidi", "fx\\speech\\Name24.wav",
		"Helen", "fx\\speech\\Name25.wav", "Jennifer", "fx\\speech\\Name28.wav", "Jessica", "fx\\speech\\Name29.wav", "Julie", "fx\\speech\\Name30.wav", "Kate", "fx\\speech\\Name31.wav",
		"Kathleen", "fx\\speech\\Name32.wav", "Mckanzie", "fx\\speech\\Name33.wav", "Megan", "fx\\speech\\Name34.wav", "Mellissa", "fx\\speech\\Name35.wav", "Nicole", "fx\\speech\\Name36.wav",
		"Patricia", "fx\\speech\\Name38.wav", "Rachael", "fx\\speech\\Name39.wav", "Rhian", "fx\\speech\\Name40.wav", "Sally", "fx\\speech\\Name41.wav", "Sarah", "fx\\speech\\Name42.wav",
		"Susan", "fx\\speech\\Name43.wav", "Tricia", "fx\\speech\\Name44.wav", "Aaron", "fx\\speech\\Name45.wav", "Andrew", "fx\\speech\\Name47.wav", "Andy", "fx\\speech\\Name48.wav",
		"Anthony", "fx\\speech\\Name49.wav", "Bill", "fx\\speech\\Name51.wav", "Brian", "fx\\speech\\Name52.wav", "Bruce", "fx\\speech\\Name53.wav", "Casimir", "fx\\speech\\Name55.wav",
		"Charles", "fx\\speech\\Name56.wav", "Charlie", "fx\\speech\\Name57.wav", "Christoff", "fx\\speech\\Name59.wav", "Christoph", "fx\\speech\\Name60.wav", "Chris", "fx\\speech\\Name58.wav",
		"Claude", "fx\\speech\\Name61.wav", "Cliff", "fx\\speech\\Name62.wav", "Collin", "fx\\speech\\Name63.wav", "Darren", "fx\\speech\\Name64.wav", "Darrin", "fx\\speech\\Name65.wav",
		"Dave", "fx\\speech\\Name66.wav", "David", "fx\\speech\\Name67.wav", "Denby", "fx\\speech\\Name68.wav", "Dennis", "fx\\speech\\Name69.wav", "Doug", "fx\\speech\\Name71.wav",
		"Earl", "fx\\speech\\Name72.wav", "Emmanuel", "fx\\speech\\Name73.wav", "Eric", "fx\\speech\\Name74.wav", "Family", "fx\\speech\\Name75.wav", "FireFly", "fx\\speech\\Name76.wav",
		"Friendly", "fx\\speech\\Name77.wav", "Geoff", "fx\\speech\\Name78.wav", "Gerry", "fx\\speech\\Name79.wav", "Grady", "fx\\speech\\Name80.wav", "Grant", "fx\\speech\\Name81.wav",
		"Greg", "fx\\speech\\Name82.wav", "Harry", "fx\\speech\\Name83.wav", "Heiko", "fx\\speech\\Name84.wav", "Jack", "fx\\speech\\Name86.wav", "James", "fx\\speech\\Name87.wav",
		"Jamie", "fx\\speech\\Name88.wav", "Jason", "fx\\speech\\Name89.wav", "Jeff", "fx\\speech\\Name90.wav", "Jimmy", "fx\\speech\\Name91.wav", "Joanna", "fx\\speech\\Name92.wav",
		"John", "fx\\speech\\Name93.wav", "Joost", "fx\\speech\\Name95.wav", "Jorge", "fx\\speech\\Name96.wav", "Josh", "fx\\speech\\Name97.wav", "Julian", "fx\\speech\\Name98.wav",
		"Keith", "fx\\speech\\Name99.wav", "Kelly", "fx\\speech\\Name100.wav", "Kevin", "fx\\speech\\Name102.wav", "Louie", "fx\\speech\\Name106.wav", "Luke", "fx\\speech\\Name107.wav",
		"Marc", "fx\\speech\\Name108.wav", "Markus", "fx\\speech\\Name110.wav", "Mark", "fx\\speech\\Name109.wav", "Matthias", "fx\\speech\\Name112.wav", "Matt", "fx\\speech\\Name111.wav",
		"Maurizio", "fx\\speech\\Name113.wav", "Michael", "fx\\speech\\Name114.wav", "Mike", "fx\\speech\\Name115.wav", "Nathan", "fx\\speech\\Name116.wav", "Neal", "fx\\speech\\Name117.wav",
		"Neil", "fx\\speech\\Name118.wav", "Nick", "fx\\speech\\Name119.wav", "of the flies", "fx\\speech\\Name120.wav", "Paolo", "fx\\speech\\Name121.wav", "Patrick", "fx\\speech\\Name122.wav",
		"Paul", "fx\\speech\\Name123.wav", "Peter", "fx\\speech\\Name125.wav", "Pete", "fx\\speech\\Name124.wav", "Phil", "fx\\speech\\Name126.wav", "Richard", "fx\\speech\\Name128.wav",
		"Robb", "fx\\speech\\Name130.wav", "Robert", "fx\\speech\\Name131.wav", "Robin", "fx\\speech\\Name132.wav", "Roland", "fx\\speech\\Name133.wav", "Sajjad", "fx\\speech\\Name134.wav",
		"Scott", "fx\\speech\\Name135.wav", "Sean", "fx\\speech\\Name136.wav", "Seth", "fx\\speech\\Name137.wav", "Simon", "fx\\speech\\Name138.wav", "Smitty", "fx\\speech\\Name139.wav",
		"Stephane", "fx\\speech\\Name140.wav", "Steven", "fx\\speech\\Name142.wav", "Steve", "fx\\speech\\Name141.wav", "Stuart", "fx\\speech\\Name144.wav", "Terry", "fx\\speech\\Name145.wav",
		"Thierry", "fx\\speech\\Name146.wav", "Thomas", "fx\\speech\\Name147.wav", "Wayne", "fx\\speech\\Name150.wav", "Youenn", "fx\\speech\\Name151.wav", "Megadeath", "fx\\speech\\Name152.wav",
		"Megalord", "fx\\speech\\Name153.wav", "Super Noodle", "fx\\speech\\Name154.wav", "Wibble", "fx\\speech\\Name198.wav", "Merepatra", "fx\\speech\\Name199.wav", "Hayden", "fx\\speech\\Name201.wav",
		"Adelin", "fx\\speech\\Name202.wav", "Alessio", "fx\\speech\\Name203.wav", "Andreas", "fx\\speech\\Name204.wav", "Cristian", "fx\\speech\\Name205.wav", "Esplendido", "fx\\speech\\Name206.wav",
		"LoFiHeart", "fx\\speech\\Name207.wav", "CaptSkubba", "fx\\speech\\Name209.wav", "Debbie", "fx\\speech\\Name210.wav", "neph", "fx\\speech\\Name211.wav", "Laurie", "fx\\speech\\Name212.wav",
		"Leo", "fx\\speech\\Name213.wav", "Mateusz", "fx\\speech\\Name214.wav", "Palanion", "fx\\speech\\Name215.wav", "Meredith", "fx\\speech\\Name216.wav", "sudouken", "fx\\speech\\Name217.wav",
		"Natasha", "fx\\speech\\Name218.wav", "Caroline", "fx\\speech\\Name219.wav", "Benzie", "fx\\speech\\Name220.wav", "FireFlyNick", "fx\\speech\\Name221.wav", "Nikolay", "fx\\speech\\Name222.wav",
		"Lordy McLordface", "fx\\speech\\Name223.wav", "Sam", "fx\\speech\\Name224.wav", "Sophie", "fx\\speech\\Name225.wav", "Gruber", "fx\\speech\\Name226.wav", "Stephen", "fx\\speech\\Name227.wav",
		"logarhythm", "fx\\speech\\Name228.wav", "GamerZakh", "fx\\speech\\Name229.wav", "Zade", "fx\\speech\\Name230.wav", "Lionheartx10", "fx\\speech\\Name231.wav", "Sergiu", "fx\\speech\\Name232.wav",
		"Raptor", "fx\\speech\\Name233.wav", "Pixelated Apollo", "fx\\speech\\Name234.wav", "Udwin", "fx\\speech\\Name235.wav", "Lutel", "fx\\speech\\Name236.wav", "RIMPAC", "fx\\speech\\Name237.wav",
		"RTS Kurga", "fx\\speech\\Name238.wav", "Jefflenious", "fx\\speech\\Name239.wav", "Nookrium", "fx\\speech\\Name240.wav", "RobDiesALot", "fx\\speech\\Name241.wav", "El Escoces gamer", "fx\\speech\\Name242.wav",
		"Koinsky", "fx\\speech\\Name243.wav", "hugothester", "fx\\speech\\Name244.wav", "DrProof", "fx\\speech\\Name245.wav", "HandOfBlood", "fx\\speech\\Name246.wav", "Dryante Zan", "fx\\speech\\Name247.wav",
		"Beasty", "fx\\speech\\Name248.wav", "LoafCat", "fx\\speech\\Name249.wav", "Lorrdy", "fx\\speech\\Name250.wav", "Lurker", "fx\\speech\\Name251.wav", "Kure", "fx\\speech\\Name252.wav",
		"Jack", "fx\\speech\\Name253.wav", "Sandrobandito", "fx\\speech\\Name255.wav", "Gregg", "fx\\speech\\Name256.wav", "Jay", "fx\\speech\\Name257.wav", "Christopher", "fx\\speech\\Name258.wav",
		"Charlotte", "fx\\speech\\Name259.wav", "Graeme", "fx\\speech\\Name260.wav", "Nigel", "fx\\speech\\Name261.wav", "Abby", "fx\\speech\\Name262.wav", "Hazel", "fx\\speech\\Name263.wav",
		"Robbie", "fx\\speech\\Name264.wav", "Daniel", "fx\\speech\\Name265.wav", "Ardiana", "fx\\speech\\Name266.wav", "Amulya", "fx\\speech\\Name267.wav", "Clara", "fx\\speech\\Name268.wav",
		"Vieko", "fx\\speech\\Name270.wav", "Pavandeep", "fx\\speech\\Name271.wav", "Juan", "fx\\speech\\Name272.wav", "Eli", "fx\\speech\\Name274.wav", "Adoné", "fx\\speech\\Name275.wav",
		"JT", "fx\\speech\\Name276.wav", "Bridie", "fx\\speech\\Name277.wav", "Dilip", "fx\\speech\\Name278.wav", "Viraj", "fx\\speech\\Name279.wav", "Reese", "fx\\speech\\Name280.wav",
		"Danis", "fx\\speech\\Name281.wav", "Barnaby", "fx\\speech\\Name282.wav", "Jess", "fx\\speech\\Name283.wav", "Kavan", "fx\\speech\\Name284.wav", "Oliver", "fx\\speech\\Name287.wav",
		"Amanda", "fx\\speech\\Name288.wav", "Jared", "fx\\speech\\Name289.wav", "George", "fx\\speech\\Name290.wav", "Evan", "fx\\speech\\Name291.wav", "Lincoln", "fx\\speech\\Name292.wav",
		"Tena", "fx\\speech\\Name293.wav", "Shona", "fx\\speech\\Name294.wav", "Zachary", "fx\\speech\\Name295.wav", "Marcus", "fx\\speech\\Name296.wav", "Rodrigo", "fx\\speech\\Name297.wav",
		"Shiva", "fx\\speech\\Name298.wav", "Frederic", "fx\\speech\\Name299.wav", "Pawel", "fx\\speech\\Name300.wav", "Josip", "fx\\speech\\Name301.wav", "Joshua", "fx\\speech\\Name302.wav",
		"Lilac", "fx\\speech\\Name303.wav", "Douglas", "fx\\speech\\Name304.wav", "Lynette", "fx\\speech\\Name306.wav", "Christer", "fx\\speech\\Name307.wav", "Janet", "fx\\speech\\Name308.wav",
		"Daz", "fx\\speech\\Name208.wav", "Leo", "fx\\speech\\Name305.wav", "Fee", "fx\\speech\\Name285.wav", "Tom", "fx\\speech\\Name148.wav", "Tim", "fx\\speech\\Name149.wav",
		"Stu", "fx\\speech\\Name143.wav", "Rob", "fx\\speech\\Name129.wav", "Ray", "fx\\speech\\Name127.wav", "Lou", "fx\\speech\\Name105.wav", "Lee", "fx\\speech\\Name104.wav",
		"Jon", "fx\\speech\\Name94.wav", "Kit", "fx\\speech\\Name103.wav", "Ken", "fx\\speech\\Name101.wav", "Ian", "fx\\speech\\Name85.wav", "Don", "fx\\speech\\Name70.wav",
		"Cas", "fx\\speech\\Name54.wav", "Ben", "fx\\speech\\Name50.wav", "Bev", "fx\\speech\\Name11.wav", "Ava", "fx\\speech\\Name8.wav", "Amy", "fx\\speech\\Name2.wav",
		"Dot", "fx\\speech\\Name18.wav", "Ivy", "fx\\speech\\Name26.wav", "Jen", "fx\\speech\\Name27.wav", "Pat", "fx\\speech\\Name37.wav", "Al", "fx\\speech\\Name46.wav",
		"Jo", "fx\\speech\\Name286.wav", "JR", "fx\\speech\\Name269.wav", "JM", "fx\\speech\\Name273.wav"
	};

	private readonly string[] nameSpeech = new string[265]
	{
		"name1.wav", "name10.wav", "name100.wav", "name101.wav", "name102.wav", "name103.wav", "name104.wav", "name105.wav", "name106.wav", "name107.wav",
		"name108.wav", "name109.wav", "name11.wav", "name110.wav", "name111.wav", "name112.wav", "name113.wav", "name114.wav", "name115.wav", "name116.wav",
		"name117.wav", "name118.wav", "name119.wav", "name12.wav", "name120.wav", "name121.wav", "name122.wav", "name123.wav", "name124.wav", "name125.wav",
		"name126.wav", "name127.wav", "name128.wav", "name129.wav", "name13.wav", "name130.wav", "name131.wav", "name132.wav", "name133.wav", "name134.wav",
		"name135.wav", "name136.wav", "name137.wav", "name138.wav", "name139.wav", "name14.wav", "name140.wav", "name141.wav", "name142.wav", "name143.wav",
		"name144.wav", "name145.wav", "name146.wav", "name147.wav", "name148.wav", "name149.wav", "name15.wav", "name150.wav", "name151.wav", "name152.wav",
		"name153.wav", "name154.wav", "name16.wav", "name17.wav", "name18.wav", "name19.wav", "name2.wav", "name20.wav", "name21.wav", "name22.wav",
		"name23.wav", "name24.wav", "name25.wav", "name26.wav", "name27.wav", "name28.wav", "name29.wav", "name3.wav", "name30.wav", "name31.wav",
		"name32.wav", "name33.wav", "name34.wav", "name35.wav", "name36.wav", "name37.wav", "name38.wav", "name39.wav", "name4.wav", "name40.wav",
		"name41.wav", "name42.wav", "name43.wav", "name44.wav", "name45.wav", "name46.wav", "name47.wav", "name48.wav", "name49.wav", "name5.wav",
		"name50.wav", "name51.wav", "name52.wav", "name53.wav", "name54.wav", "name55.wav", "name56.wav", "name57.wav", "name58.wav", "name59.wav",
		"name6.wav", "name60.wav", "name61.wav", "name62.wav", "name63.wav", "name64.wav", "name65.wav", "name66.wav", "name67.wav", "name68.wav",
		"name69.wav", "name7.wav", "name70.wav", "name71.wav", "name72.wav", "name73.wav", "name74.wav", "name75.wav", "name76.wav", "name77.wav",
		"name78.wav", "name79.wav", "name8.wav", "name80.wav", "name81.wav", "name82.wav", "name83.wav", "name84.wav", "name85.wav", "name86.wav",
		"name87.wav", "name88.wav", "name89.wav", "name9.wav", "name90.wav", "name91.wav", "name92.wav", "name93.wav", "name94.wav", "name95.wav",
		"name96.wav", "name97.wav", "name98.wav", "name99.wav", "name198.wav", "name199.wav", "name200.wav", "name201.wav", "name202.wav", "name203.wav",
		"name204.wav", "name205.wav", "name206.wav", "name207.wav", "name208.wav", "name209.wav", "name210.wav", "name211.wav", "name212.wav", "name213.wav",
		"name214.wav", "name215.wav", "name216.wav", "name217.wav", "name218.wav", "name219.wav", "name220.wav", "name221.wav", "name222.wav", "name223.wav",
		"name224.wav", "name225.wav", "name226.wav", "name227.wav", "name228.wav", "name229.wav", "name230.wav", "name231.wav", "name232.wav", "name233.wav",
		"name234.wav", "name235.wav", "name236.wav", "name237.wav", "name238.wav", "name239.wav", "name240.wav", "name241.wav", "name242.wav", "name243.wav",
		"name244.wav", "name245.wav", "name246.wav", "name247.wav", "name248.wav", "name249.wav", "name250.wav", "name251.wav", "name252.wav", "name253.wav",
		"name254.wav", "name255.wav", "name256.wav", "name257.wav", "name258.wav", "name259.wav", "name260.wav", "name261.wav", "name262.wav", "name263.wav",
		"name264.wav", "name265.wav", "name266.wav", "name267.wav", "name268.wav", "name269.wav", "name270.wav", "name271.wav", "name272.wav", "name273.wav",
		"name274.wav", "name275.wav", "name276.wav", "name277.wav", "name278.wav", "name279.wav", "name280.wav", "name281.wav", "name282.wav", "name283.wav",
		"name284.wav", "name285.wav", "name286.wav", "name287.wav", "name288.wav", "name289.wav", "name290.wav", "name291.wav", "name292.wav", "name293.wav",
		"name294.wav", "name295.wav", "name296.wav", "name297.wav", "name298.wav", "name299.wav", "name300.wav", "name301.wav", "name302.wav", "name303.wav",
		"name304.wav", "name305.wav", "name306.wav", "name307.wav", "name308.wav"
	};

	private readonly string[] peasantSpeech = new string[240]
	{
		"Peasant_Female1.wav", "Peasant_Female10.wav", "Peasant_Female100.wav", "Peasant_Female101.wav", "Peasant_Female102.wav", "Peasant_Female103.wav", "Peasant_Female104.wav", "Peasant_Female105.wav", "Peasant_Female106.wav", "Peasant_Female107.wav",
		"Peasant_Female108.wav", "Peasant_Female109.wav", "Peasant_Female11.wav", "Peasant_Female110.wav", "Peasant_Female111.wav", "Peasant_Female112.wav", "Peasant_Female113.wav", "Peasant_Female114.wav", "Peasant_Female115.wav", "Peasant_Female116.wav",
		"Peasant_Female117.wav", "Peasant_Female118.wav", "Peasant_Female119.wav", "Peasant_Female12.wav", "Peasant_Female120.wav", "Peasant_Female13.wav", "Peasant_Female14.wav", "Peasant_Female15.wav", "Peasant_Female16.wav", "Peasant_Female17.wav",
		"Peasant_Female18.wav", "Peasant_Female19.wav", "Peasant_Female2.wav", "Peasant_Female20.wav", "Peasant_Female21.wav", "Peasant_Female22.wav", "Peasant_Female23.wav", "Peasant_Female24.wav", "Peasant_Female25.wav", "Peasant_Female26.wav",
		"Peasant_Female27.wav", "Peasant_Female28.wav", "Peasant_Female29.wav", "Peasant_Female3.wav", "Peasant_Female30.wav", "Peasant_Female31.wav", "Peasant_Female32.wav", "Peasant_Female33.wav", "Peasant_Female34.wav", "Peasant_Female35.wav",
		"Peasant_Female36.wav", "Peasant_Female37.wav", "Peasant_Female38.wav", "Peasant_Female39.wav", "Peasant_Female4.wav", "Peasant_Female40.wav", "Peasant_Female41.wav", "Peasant_Female42.wav", "Peasant_Female43.wav", "Peasant_Female44.wav",
		"Peasant_Female45.wav", "Peasant_Female46.wav", "Peasant_Female47.wav", "Peasant_Female48.wav", "Peasant_Female49.wav", "Peasant_Female5.wav", "Peasant_Female50.wav", "Peasant_Female51.wav", "Peasant_Female52.wav", "Peasant_Female53.wav",
		"Peasant_Female54.wav", "Peasant_Female55.wav", "Peasant_Female56.wav", "Peasant_Female57.wav", "Peasant_Female58.wav", "Peasant_Female59.wav", "Peasant_Female6.wav", "Peasant_Female60.wav", "Peasant_Female61.wav", "Peasant_Female62.wav",
		"Peasant_Female63.wav", "Peasant_Female64.wav", "Peasant_Female65.wav", "Peasant_Female66.wav", "Peasant_Female67.wav", "Peasant_Female68.wav", "Peasant_Female69.wav", "Peasant_Female7.wav", "Peasant_Female70.wav", "Peasant_Female71.wav",
		"Peasant_Female72.wav", "Peasant_Female73.wav", "Peasant_Female74.wav", "Peasant_Female75.wav", "Peasant_Female76.wav", "Peasant_Female77.wav", "Peasant_Female78.wav", "Peasant_Female79.wav", "Peasant_Female8.wav", "Peasant_Female80.wav",
		"Peasant_Female81.wav", "Peasant_Female82.wav", "Peasant_Female83.wav", "Peasant_Female84.wav", "Peasant_Female85.wav", "Peasant_Female86.wav", "Peasant_Female87.wav", "Peasant_Female88.wav", "Peasant_Female89.wav", "Peasant_Female9.wav",
		"Peasant_Female90.wav", "Peasant_Female91.wav", "Peasant_Female92.wav", "Peasant_Female93.wav", "Peasant_Female94.wav", "Peasant_Female95.wav", "Peasant_Female96.wav", "Peasant_Female97.wav", "Peasant_Female98.wav", "Peasant_Female99.wav",
		"Peasant_Male1.wav", "Peasant_Male10.wav", "Peasant_Male100.wav", "Peasant_Male101.wav", "Peasant_Male102.wav", "Peasant_Male103.wav", "Peasant_Male104.wav", "Peasant_Male105.wav", "Peasant_Male106.wav", "Peasant_Male107.wav",
		"Peasant_Male108.wav", "Peasant_Male109.wav", "Peasant_Male11.wav", "Peasant_Male110.wav", "Peasant_Male111.wav", "Peasant_Male112.wav", "Peasant_Male113.wav", "Peasant_Male114.wav", "Peasant_Male115.wav", "Peasant_Male116.wav",
		"Peasant_Male117.wav", "Peasant_Male118.wav", "Peasant_Male119.wav", "Peasant_Male12.wav", "Peasant_Male120.wav", "Peasant_Male13.wav", "Peasant_Male14.wav", "Peasant_Male15.wav", "Peasant_Male16.wav", "Peasant_Male17.wav",
		"Peasant_Male18.wav", "Peasant_Male19.wav", "Peasant_Male2.wav", "Peasant_Male20.wav", "Peasant_Male21.wav", "Peasant_Male22.wav", "Peasant_Male23.wav", "Peasant_Male24.wav", "Peasant_Male25.wav", "Peasant_Male26.wav",
		"Peasant_Male27.wav", "Peasant_Male28.wav", "Peasant_Male29.wav", "Peasant_Male3.wav", "Peasant_Male30.wav", "Peasant_Male31.wav", "Peasant_Male32.wav", "Peasant_Male33.wav", "Peasant_Male34.wav", "Peasant_Male35.wav",
		"Peasant_Male36.wav", "Peasant_Male37.wav", "Peasant_Male38.wav", "Peasant_Male39.wav", "Peasant_Male4.wav", "Peasant_Male40.wav", "Peasant_Male41.wav", "Peasant_Male42.wav", "Peasant_Male43.wav", "Peasant_Male44.wav",
		"Peasant_Male45.wav", "Peasant_Male46.wav", "Peasant_Male47.wav", "Peasant_Male48.wav", "Peasant_Male49.wav", "Peasant_Male5.wav", "Peasant_Male50.wav", "Peasant_Male51.wav", "Peasant_Male52.wav", "Peasant_Male53.wav",
		"Peasant_Male54.wav", "Peasant_Male55.wav", "Peasant_Male56.wav", "Peasant_Male57.wav", "Peasant_Male58.wav", "Peasant_Male59.wav", "Peasant_Male6.wav", "Peasant_Male60.wav", "Peasant_Male61.wav", "Peasant_Male62.wav",
		"Peasant_Male63.wav", "Peasant_Male64.wav", "Peasant_Male65.wav", "Peasant_Male66.wav", "Peasant_Male67.wav", "Peasant_Male68.wav", "Peasant_Male69.wav", "Peasant_Male7.wav", "Peasant_Male70.wav", "Peasant_Male71.wav",
		"Peasant_Male72.wav", "Peasant_Male73.wav", "Peasant_Male74.wav", "Peasant_Male75.wav", "Peasant_Male76.wav", "Peasant_Male77.wav", "Peasant_Male78.wav", "Peasant_Male79.wav", "Peasant_Male8.wav", "Peasant_Male80.wav",
		"Peasant_Male81.wav", "Peasant_Male82.wav", "Peasant_Male83.wav", "Peasant_Male84.wav", "Peasant_Male85.wav", "Peasant_Male86.wav", "Peasant_Male87.wav", "Peasant_Male88.wav", "Peasant_Male89.wav", "Peasant_Male9.wav",
		"Peasant_Male90.wav", "Peasant_Male91.wav", "Peasant_Male92.wav", "Peasant_Male93.wav", "Peasant_Male94.wav", "Peasant_Male95.wav", "Peasant_Male96.wav", "Peasant_Male97.wav", "Peasant_Male98.wav", "Peasant_Male99.wav"
	};

	private readonly string[] troopsSpeech = new string[291]
	{
		"Arch_ATK_EQP1.wav", "Arch_ATKA1.wav", "Arch_ATKH1.wav", "Arch_ATKM1.wav", "Arch_ATKM2.wav", "Arch_ATKM3.wav", "Arch_ATKM4.wav", "Arch_ATKNT.wav", "Arch_Disband1.wav", "Arch_Light_Pitch1.wav",
		"Arch_m1.wav", "Arch_m2.wav", "Arch_m3.wav", "Arch_m4.wav", "Arch_m5.wav", "Arch_Moat1.wav", "Arch_Moat2.wav", "Arch_Moat3.wav", "Arch_s1.wav", "Arch_s2.wav",
		"Arch_s3.wav", "Arch_s4.wav", "Arch_s5.wav", "Arch_s6.wav", "Cross_ATKA1.wav", "Cross_ATKH1.wav", "Cross_ATKM1.wav", "Cross_ATKM2.wav", "Cross_ATKM3.wav", "Cross_ATKM4.wav",
		"Cross_ATKNT.wav", "Cross_Disband1.wav", "Cross_m1.wav", "Cross_m2.wav", "Cross_m3.wav", "Cross_m4.wav", "Cross_m5.wav", "Cross_Moat1.wav", "Cross_Moat2.wav", "Cross_Moat3.wav",
		"Cross_s1.wav", "Cross_s2.wav", "Cross_s3.wav", "Cross_s4.wav", "Cross_s5.wav", "Cross_s6.wav", "Engineer_ATKS1.wav", "Engineer_ATKS2.wav", "Engineer_ATKS3.wav", "Engineer_ATKS4.wav",
		"Engineer_ATKW1.wav", "Engineer_Balis1.wav", "Engineer_Build1.wav", "Engineer_catplt1.wav", "Engineer_Disband1.wav", "Engineer_Equip1.wav", "Engineer_Equip2.wav", "Engineer_Equip3.wav", "Engineer_Equip4.wav", "Engineer_Exit1.wav",
		"Engineer_Launchcow1.wav", "Engineer_Launchcow2.wav", "Engineer_Launchcow3.wav", "Engineer_Launchcow4.wav", "Engineer_Launchcow5.wav", "Engineer_Launchcow6.wav", "Engineer_M1.wav", "Engineer_M2.wav", "Engineer_M3.wav", "Engineer_M4.wav",
		"Engineer_M5.wav", "Engineer_Manequip1.wav", "Engineer_Mang1.wav", "Engineer_Manoil1.wav", "Engineer_Mansmelter1.wav", "Engineer_MCatplt.wav", "Engineer_Moat1.wav", "Engineer_Moat2.wav", "Engineer_Moat3.wav", "Engineer_Mram.wav",
		"Engineer_Mshield.wav", "Engineer_Mtower.wav", "Engineer_Pouroil1.wav", "Engineer_Pouroil2.wav", "Engineer_Pouroil3.wav", "Engineer_Pouroil4.wav", "Engineer_Pouroil5.wav", "Engineer_Pouroil6.wav", "Engineer_Pouroil7.wav", "Engineer_Pouroil8.wav",
		"Engineer_Pouroil9.wav", "Engineer_Ram1.wav", "Engineer_s1.wav", "Engineer_s2.wav", "Engineer_s3.wav", "Engineer_s4.wav", "Engineer_s5.wav", "Engineer_s6.wav", "Engineer_Sbalis.wav", "Engineer_Scatplt.wav",
		"Engineer_Sman.wav", "Engineer_Sram.wav", "Engineer_Sshield.wav", "Engineer_Stower.wav", "Engineer_STreb.wav", "Engineer_Treb1.wav", "Knight_ATKW1.wav", "Knight_ATKW2.wav", "Knight_ATKW3.wav", "Knight_ATKW4.wav",
		"Knight_Disband1.wav", "Knight_m1.wav", "Knight_m2.wav", "Knight_m3.wav", "Knight_m4.wav", "Knight_m5.wav", "Knight_Moat1.wav", "Knight_Moat2.wav", "Knight_Moat3.wav", "Knight_s1.wav",
		"Knight_s2.wav", "Knight_s3.wav", "Knight_s4.wav", "Knight_s5.wav", "Knight_s6.wav", "Ladder_ATKS1.wav", "Ladder_ATKS2.wav", "Ladder_ATKS3.wav", "Ladder_ATKS4.wav", "Ladder_Disband1.wav",
		"Ladder_m1.wav", "Ladder_m2.wav", "Ladder_m3.wav", "Ladder_m4.wav", "Ladder_m5.wav", "Ladder_Moat1.wav", "Ladder_Moat2.wav", "Ladder_Moat3.wav", "Ladder_Placeladder1.wav", "Ladder_Placeladder2.wav",
		"Ladder_Placeladder3.wav", "Ladder_s1.wav", "Ladder_s2.wav", "Ladder_s3.wav", "Ladder_s4.wav", "Ladder_s5.wav", "Ladder_s6.wav", "Mace_ATKS1.wav", "Mace_ATKS2.wav", "Mace_ATKS3.wav",
		"Mace_ATKS4.wav", "Mace_ATKW1.wav", "Mace_ATKW2.wav", "Mace_ATKW3.wav", "Mace_ATKW4.wav", "Mace_Disband1.wav", "Mace_m1.wav", "Mace_m2.wav", "Mace_m3.wav", "Mace_m4.wav",
		"Mace_m5.wav", "Mace_Moat1.wav", "Mace_Moat2.wav", "Mace_Moat3.wav", "Mace_s1.wav", "Mace_s2.wav", "Mace_s3.wav", "Mace_s4.wav", "Mace_s5.wav", "Mace_s6.wav",
		"Monk_ATKS1.wav", "Monk_ATKS2.wav", "Monk_ATKS3.wav", "Monk_ATKS4.wav", "Monk_ATKW1.wav", "Monk_ATKW2.wav", "Monk_ATKW3.wav", "Monk_ATKW4.wav", "Monk_Disband1.wav", "Monk_m1.wav",
		"Monk_m2.wav", "Monk_m3.wav", "Monk_m4.wav", "Monk_m5.wav", "Monk_Moat1.wav", "Monk_Moat2.wav", "Monk_Moat3.wav", "Monk_s1.wav", "Monk_s2.wav", "Monk_s3.wav",
		"Monk_s4.wav", "Monk_s5.wav", "Monk_s6.wav", "Pike_ATKS1.wav", "Pike_ATKS2.wav", "Pike_ATKS3.wav", "Pike_ATKS4.wav", "Pike_ATKW1.wav", "Pike_ATKW2.wav", "Pike_ATKW3.wav",
		"Pike_ATKW4.wav", "Pike_Disband1.wav", "Pike_Ladder1.wav", "Pike_Ladder2.wav", "Pike_Ladder3.wav", "Pike_M1.wav", "Pike_M2.wav", "Pike_M3.wav", "Pike_M4.wav", "Pike_M5.wav",
		"Pike_Moat1.wav", "Pike_Moat2.wav", "Pike_Moat3.wav", "Pike_S1.wav", "Pike_S2.wav", "Pike_S3.wav", "Pike_S4.wav", "Pike_S5.wav", "Pike_S6.wav", "Spear_ATKS1.wav",
		"Spear_ATKS2.wav", "Spear_ATKS3.wav", "Spear_ATKS4.wav", "Spear_ATKW1.wav", "Spear_ATKW2.wav", "Spear_ATKW3.wav", "Spear_ATKW4.wav", "Spear_Disband1.wav", "Spear_Ladder1.wav", "Spear_Ladder2.wav",
		"Spear_Ladder3.wav", "Spear_m1.wav", "Spear_m2.wav", "Spear_m3.wav", "Spear_m4.wav", "Spear_m5.wav", "Spear_Moat1.wav", "Spear_Moat2.wav", "Spear_Moat3.wav", "Spear_Moat4.wav",
		"Spear_Moat5.wav", "Spear_s1.wav", "Spear_s2.wav", "Spear_s3.wav", "Spear_s4.wav", "Spear_s5.wav", "Spear_s6.wav", "Sword_ATKW1.wav", "Sword_ATKW2.wav", "Sword_ATKW3.wav",
		"Sword_ATKW4.wav", "Sword_Disband1.wav", "Sword_m1.wav", "Sword_m2.wav", "Sword_m3.wav", "Sword_m4.wav", "Sword_m5.wav", "Sword_Moat1.wav", "Sword_Moat2.wav", "Sword_Moat3.wav",
		"Sword_s1.wav", "Sword_s2.wav", "Sword_s3.wav", "Sword_s4.wav", "Sword_s5.wav", "Sword_s6.wav", "Tunnel_ATKS1.wav", "Tunnel_ATKS2.wav", "Tunnel_ATKS3.wav", "Tunnel_ATKS4.wav",
		"Tunnel_ATKW1.wav", "Tunnel_ATKW2.wav", "Tunnel_ATKW3.wav", "Tunnel_ATKW4.wav", "Tunnel_Digtunnel1.wav", "Tunnel_Digtunnel2.wav", "Tunnel_Disband1.wav", "Tunnel_m1.wav", "Tunnel_m2.wav", "Tunnel_m3.wav",
		"Tunnel_m4.wav", "Tunnel_m5.wav", "Tunnel_Moat1.wav", "Tunnel_Moat2.wav", "Tunnel_Moat3.wav", "Tunnel_s1.wav", "Tunnel_s2.wav", "Tunnel_s3.wav", "Tunnel_s4.wav", "Tunnel_s5.wav",
		"Tunnel_s6.wav"
	};

	private readonly string[] tutorialSpeech = new string[63]
	{
		"Tutorial_1.wav", "Tutorial_10a.wav", "Tutorial_10b.wav", "Tutorial_11.wav", "Tutorial_12a.wav", "Tutorial_12b.wav", "Tutorial_13.wav", "Tutorial_14a.wav", "Tutorial_14b.wav", "Tutorial_15.wav",
		"Tutorial_16.wav", "Tutorial_17a.wav", "Tutorial_17a_alt.wav", "Tutorial_17b.wav", "Tutorial_18.wav", "Tutorial_19.wav", "Tutorial_2.wav", "Tutorial_2a.wav", "Tutorial_20.wav", "Tutorial_21.wav",
		"Tutorial_22.wav", "Tutorial_23.wav", "Tutorial_24.wav", "Tutorial_25.wav", "Tutorial_26.wav", "Tutorial_27.wav", "Tutorial_28.wav", "Tutorial_28a.wav", "Tutorial_29.wav", "Tutorial_29a.wav",
		"Tutorial_30.wav", "Tutorial_31.wav", "Tutorial_32.wav", "Tutorial_32a.wav", "Tutorial_33.wav", "Tutorial_34.wav", "Tutorial_35.wav", "Tutorial_36.wav", "Tutorial_37.wav", "Tutorial_38.wav",
		"Tutorial_39.wav", "Tutorial_3a.wav", "Tutorial_3b.wav", "Tutorial_3c.wav", "Tutorial_40.wav", "Tutorial_41.wav", "Tutorial_42.wav", "Tutorial_43.wav", "Tutorial_44.wav", "Tutorial_45.wav",
		"Tutorial_46.wav", "Tutorial_4a.wav", "Tutorial_4b.wav", "Tutorial_4c.wav", "Tutorial_5.wav", "Tutorial_6a.wav", "Tutorial_6b.wav", "Tutorial_7.wav", "Tutorial_8a.wav", "Tutorial_8b.wav",
		"Tutorial_9a.wav", "Tutorial_9b.wav", "Tutorial_9c.wav"
	};

	private readonly string[] extraSpeech = new string[100]
	{
		"DE_DLC0_0.wav", "DE_DLC0_1.wav", "DE_DLC0_2.wav", "DE_DLC0_3.wav", "DE_DLC0_4.wav", "DE_DLC0_5.wav", "DE_DLC0_6.wav", "DE_DLC0_7.wav", "DE_DLC0_8.wav", "DE_DLC0_9.wav",
		"DE_DLC1_M1_0.wav", "DE_DLC1_M2_0.wav", "DE_DLC1_M2_1.wav", "DE_DLC1_M3_0.wav", "DE_DLC1_M3_1.wav", "DE_DLC1_M4_0.wav", "DE_DLC1_M4_1.wav", "DE_DLC1_M4_2.wav", "DE_DLC1_M4_3.wav", "DE_DLC1_M4_4.wav",
		"DE_DLC1_M4_5.wav", "DE_DLC1_M5_0.wav", "DE_DLC1_M5_1.wav", "DE_DLC1_M5_2.wav", "DE_DLC1_M5_3.wav", "DE_DLC1_M5_4.wav", "DE_DLC1_M5_5.wav", "DE_DLC1_M5_6.wav", "DE_DLC1_M5_7.wav", "DE_DLC1_M5_8.wav",
		"DE_DLC1_M6_0.wav", "DE_DLC1_M7_0.wav", "DE_DLC2_M1_0.wav", "DE_DLC2_M1_1.wav", "DE_DLC2_M2_0.wav", "DE_DLC2_M2_1.wav", "DE_DLC2_M2_2.wav", "DE_DLC2_M2_3.wav", "DE_DLC2_M2_4.wav", "DE_DLC2_M2_5.wav",
		"DE_DLC2_M2_6.wav", "DE_DLC2_M2_7.wav", "DE_DLC2_M2_8.wav", "DE_DLC2_M3_0.wav", "DE_DLC2_M3_1.wav", "DE_DLC2_M3_2.wav", "DE_DLC2_M4_0.wav", "DE_DLC2_M4_1.wav", "DE_DLC2_M4_2.wav", "DE_DLC2_M4_3.wav",
		"DE_DLC2_M4_4.wav", "DE_DLC2_M5_0.wav", "DE_DLC2_M5_1.wav", "DE_DLC2_M6_0.wav", "DE_DLC2_M6_1.wav", "DE_DLC2_M7_0.wav", "DE_DLC2_M7_1.wav", "DE_DLC3_M1_0.wav", "DE_DLC3_M1_1.wav", "DE_DLC3_M1_2.wav",
		"DE_DLC3_M2_0.wav", "DE_DLC3_M2_1.wav", "DE_DLC3_M2_2.wav", "DE_DLC3_M3_0.wav", "DE_DLC3_M3_1.wav", "DE_DLC3_M3_2.wav", "DE_DLC3_M3_3.wav", "DE_DLC3_M3_4.wav", "DE_DLC3_M4_0.wav", "DE_DLC3_M4_1.wav",
		"DE_DLC3_M4_2.wav", "DE_DLC3_M4_3.wav", "DE_DLC3_M4_4.wav", "DE_DLC3_M4_5.wav", "DE_DLC3_M5_0.wav", "DE_DLC3_M5_1.wav", "DE_DLC3_M6_0.wav", "DE_DLC3_M6_1.wav", "DE_DLC3_M6_2.wav", "DE_DLC3_M7_0.wav",
		"DE_DLC4_M1_0.wav", "DE_DLC4_M1_1.wav", "DE_DLC4_M2_0.wav", "DE_DLC4_M2_1.wav", "DE_DLC4_M2_2.wav", "DE_DLC4_M3_0.wav", "DE_DLC4_M3_1.wav", "DE_DLC4_M3_2.wav", "DE_DLC4_M3_3.wav", "DE_DLC4_M3_4.wav",
		"DE_DLC4_M4_0.wav", "DE_DLC4_M4_1.wav", "DE_DLC4_M4_2.wav", "DE_DLC4_M4_3.wav", "DE_DLC4_M5_0.wav", "DE_DLC4_M6_0.wav", "DE_DLC4_M6_1.wav", "DE_DLC4_M7_0.wav", "DE_DLC4_M7_1.wav", "DE_DLC4_M7_2.wav"
	};

	private readonly string[] music_filenames = new string[47]
	{
		"", "fx\\music\\demopiece 22k.raw", "fx\\music\\strhld2_22k.raw", "fx\\music\\chantloop1.raw", "fx\\music\\mandloop1.raw", "fx\\music\\mandloop2.raw", "fx\\music\\monks1.raw", "fx\\music\\stainedglass1.raw", "fx\\music\\stainedglass3.raw", "fx\\music\\mattsjig.raw",
		"fx\\music\\the maidenA.raw", "fx\\music\\the maidenB.raw", "fx\\music\\journeys.raw", "fx\\music\\exploration.raw", "fx\\music\\underanoldtree.raw", "fx\\music\\castlejam.raw", "fx\\music\\appytimes.raw", "fx\\music\\darktime.raw", "fx\\music\\paranoia.raw", "fx\\music\\sadtimesa.raw",
		"fx\\music\\sadtimesb.raw", "fx\\music\\german_easter1.raw", "fx\\music\\suspense1a.raw", "fx\\music\\suspense1b.raw", "fx\\music\\suspense2a.raw", "fx\\music\\suspense2b.raw", "fx\\music\\suspense2c.raw", "fx\\music\\drumloop1c.raw", "fx\\music\\percloop1.raw", "fx\\music\\honor_02.raw",
		"fx\\music\\honor_03.raw", "fx\\music\\honor_04.raw", "fx\\music\\honor_05.raw", "fx\\music\\glory_01.raw", "fx\\music\\glory_02.raw", "fx\\music\\glory_03.raw", "fx\\music\\glory_04.raw", "fx\\music\\glory_05.raw", "fx\\music\\glory_06.raw", "fx\\music\\percloop1.raw",
		"fx\\music\\drumloop1a.raw", "fx\\music\\drumloop1b.raw", "fx\\music\\bigwin1.raw", "fx\\music\\bigwin2.raw", "fx\\music\\bigwin3.raw", "fx\\music\\bigloss1.raw", "fx\\music\\bigloss2.raw"
	};

	private Dictionary<string, string> speechFolders = new Dictionary<string, string>();

	private List<sh1_sound_effect> fx_list = new List<sh1_sound_effect>();

	private List<sh1_sound_effect> ambient_list = new List<sh1_sound_effect>();

	private List<sh1_sound> play_list = new List<sh1_sound>();

	private Dictionary<string, VolumeData> volumeData = new Dictionary<string, VolumeData>();

	private DateTime lastUISoundsTime = DateTime.MinValue;

	private int UISoundDecreaser;

	private DateTime nextAllowableMoo = DateTime.MinValue;

	private DateTime freeBuildVoicelinesStart = DateTime.MinValue;

	private int freeBuildVoicelinesStage;

	private static int last_win_tune;

	private static int last_lose_tune;

	private readonly string[] buildingBinks = new string[17]
	{
		"st03_woodcutters_hut.webm", "st07_hunters_hut.webm", "st12_fletchers_workshop.webm", "st13_blacksmiths_workshop.webm", "st14_poleturners_workshop.webm", "st15_armourers_workshop.webm", "st16_tanners_workshop.webm", "st17_bakers_workshop.webm", "st22_inn.webm", "st26_tradepost.webm",
		"st30_wheatfarm.webm", "st32_applefarm.webm", "st33_cattlefarm.webm", "st34_mill.webm", "st36_church1.webm", "st37_church2.webm", "st38_church3.webm"
	};

	private readonly string[] characterBinks = new string[37]
	{
		"pg_anger1.webm", "pg_plead1.webm", "pg_plead2.webm", "pg_taunt1.webm", "pg_taunt2.webm", "pg_vict1.webm", "pg_vict2.webm", "pg_vict3.webm", "rt_anger1.webm", "rt_plead1.webm",
		"rt_plead2.webm", "rt_plead3.webm", "rt_taunt1.webm", "rt_taunt2.webm", "rt_vict1.webm", "rt_vict2.webm", "sn_anger1.webm", "sn_plead1.webm", "sn_plead2.webm", "sn_taunt1.webm",
		"sn_taunt2.webm", "sn_vict1.webm", "sn_vict2.webm", "wf_anger1.webm", "wf_plead1.webm", "wf_plead2.webm", "wf_taunt1.webm", "wf_taunt2.webm", "wf_vict1.webm", "wf_vict2.webm",
		"sgt_angry_1.webm", "sgt_confident_1.webm", "sgt_nervous_1.webm", "sgt_neutral_1.webm", "sgt_taunt_1.webm", "sgt_confident_2.webm", "sgt_neutral_2.webm"
	};

	private readonly string[] eventBinks = new string[20]
	{
		"action_apples_die.webm", "action_archers.webm", "action_bandits.webm", "action_fair.webm", "action_fire.webm", "action_hops_die.webm", "action_jester.webm", "action_mad_cows.webm", "action_marriage.webm", "action_plague.webm",
		"action_rabbits.webm", "action_steal_bread.webm", "action_trees_die.webm", "action_wheat_die.webm", "action_wolves.webm", "message_default.webm", "pig_attack.webm", "rat_attack.webm", "snake_attack.webm", "wolf_attack.webm"
	};

	private readonly string[] storyBinks = new string[29]
	{
		"ap_civil1.webm", "ap_civil10.webm", "ap_civil11.webm", "ap_civil12.webm", "ap_civil13.webm", "ap_civil14.webm", "ap_civil15.webm", "ap_civil16.webm", "ap_civil2.webm", "ap_civil3.webm",
		"ap_civil4.webm", "ap_civil5.webm", "ap_civil6.webm", "ap_civil7.webm", "ap_civil8.webm", "ap_civil9.webm", "ap_milit1.webm", "ap_milit10.webm", "ap_milit11.webm", "ap_milit12.webm",
		"ap_milit13.webm", "ap_milit2.webm", "ap_milit3.webm", "ap_milit4.webm", "ap_milit5.webm", "ap_milit6.webm", "ap_milit7.webm", "ap_milit8.webm", "ap_milit9.webm"
	};

	private Dictionary<string, string> binkFolders = new Dictionary<string, string>();

	private bool binkStarted;

	private bool binkLooping;

	public float binkVolume = 1f;

	public int requestBinkPlayState;

	public Uri requestBinkPlaybackURI;

	public bool binkIsPlaying;

	public bool binkWaitForSpeech;

	public static void InitSoundFX()
	{
		if (instance == null)
		{
			instance = new SFXManager();
			instance.init();
		}
	}

	public void init()
	{
		string[] array = (Resources.Load("fx/volume") as TextAsset).text.Replace("\r", "").Split('\n');
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Length <= 5)
			{
				continue;
			}
			string text = array[i];
			if (!text.StartsWith("---"))
			{
				continue;
			}
			int num = 0;
			string[] array2 = text.Split('=');
			if (array2.Length > 1)
			{
				num = int.Parse(array2[1].Replace("-", "").Replace(" ", ""), Director.defaultCulture);
			}
			i++;
			while (i < array.Length)
			{
				text = array[i];
				if (text.StartsWith("---"))
				{
					i--;
					break;
				}
				if (array[i].Length < 5)
				{
					i++;
					continue;
				}
				string[] array3 = array[i].Split('"');
				int num2 = 0;
				if (array3[0].Length == 0)
				{
					num2++;
				}
				string text2 = array3[num2];
				int num3 = int.Parse(array3[1 + num2].Replace("-", "").Replace(" ", "").Replace("\t", ""), Director.defaultCulture);
				VolumeData volumeData = new VolumeData();
				volumeData.name = text2;
				volumeData.volume = (float)(num3 + num) / 127f;
				if (volumeData.volume < 0f)
				{
					volumeData.volume = 0f;
				}
				else if (volumeData.volume > 1f)
				{
					volumeData.volume = 1f;
				}
				this.volumeData[text2.Replace(".wav", "").ToLowerInvariant()] = volumeData;
				i++;
			}
		}
		for (int j = 0; stronghold_main_list[j, 0].Length != 0; j++)
		{
			sh1_sound_effect sh1_sound_effect = new sh1_sound_effect();
			fx_list.Add(sh1_sound_effect);
			sh1_sound_effect.first_buffer_no = -1;
			sh1_sound_effect.max_variants = 0;
			sh1_sound_effect.variants_loaded = 0;
			sh1_sound_effect.last_variant_played = 0;
			for (int k = 0; k < 8 && !(stronghold_main_list[j, k].ToLowerInvariant() == "null"); k++)
			{
				sh1_sound_effect.max_variants++;
			}
			for (int l = 0; l < 8 && !(stronghold_main_list[j, l].ToLowerInvariant() == "null"); l++)
			{
				AudioClip clip = Resources.Load<AudioClip>(stronghold_main_list[j, l]);
				float num4 = 1f;
				string text3 = stronghold_main_list[j, l].ToLowerInvariant();
				if (this.volumeData.ContainsKey(text3))
				{
					num4 = this.volumeData[text3].volume;
				}
				else
				{
					Debug.Log("Missing SFX Volume : " + text3);
					num4 = 0f;
				}
				int count = play_list.Count;
				sh1_sound sh1_sound = new sh1_sound();
				sh1_sound.volume = 1f;
				sh1_sound.position = 64;
				sh1_sound.clip = clip;
				sh1_sound.volume = (sh1_sound.real_volume = num4);
				play_list.Add(sh1_sound);
				sh1_sound_effect.variants_loaded++;
				if (sh1_sound_effect.first_buffer_no == -1)
				{
					sh1_sound_effect.first_buffer_no = count;
				}
			}
		}
		for (int j = 0; stronghold_ambient_list[j, 0].Length != 0; j++)
		{
			sh1_sound_effect sh1_sound_effect2 = new sh1_sound_effect();
			ambient_list.Add(sh1_sound_effect2);
			sh1_sound_effect2.first_buffer_no = -1;
			sh1_sound_effect2.max_variants = 0;
			sh1_sound_effect2.variants_loaded = 0;
			sh1_sound_effect2.last_variant_played = 0;
			for (int m = 0; m < 8 && !(stronghold_ambient_list[j, m].ToLowerInvariant() == "null"); m++)
			{
				sh1_sound_effect2.max_variants++;
			}
			for (int n = 0; n < 8 && !(stronghold_ambient_list[j, n].ToLowerInvariant() == "null"); n++)
			{
				AudioClip clip2 = Resources.Load<AudioClip>(stronghold_ambient_list[j, n]);
				float real_volume = 1f;
				string text4 = stronghold_ambient_list[j, n].ToLowerInvariant();
				if (this.volumeData.ContainsKey(text4))
				{
					real_volume = this.volumeData[text4].volume;
				}
				else
				{
					Debug.Log("Missing Ambient Volume : " + text4);
				}
				int count2 = play_list.Count;
				sh1_sound sh1_sound2 = new sh1_sound();
				sh1_sound2.volume = 1f;
				sh1_sound2.position = 64;
				sh1_sound2.clip = clip2;
				sh1_sound2.volume = (sh1_sound2.real_volume = real_volume);
				play_list.Add(sh1_sound2);
				sh1_sound_effect2.variants_loaded++;
				if (sh1_sound_effect2.first_buffer_no == -1)
				{
					sh1_sound_effect2.first_buffer_no = count2;
				}
			}
		}
		string[] array4 = scribeSpeech;
		foreach (string text5 in array4)
		{
			speechFolders[text5.ToLowerInvariant()] = "scribe";
		}
		array4 = aiSpeech;
		foreach (string text6 in array4)
		{
			speechFolders[text6.ToLowerInvariant()] = "ai";
		}
		array4 = inMissionSpeech;
		foreach (string text7 in array4)
		{
			speechFolders[text7.ToLowerInvariant()] = "inmission";
		}
		array4 = insultSpeech;
		foreach (string text8 in array4)
		{
			speechFolders[text8.ToLowerInvariant()] = "insults";
		}
		array4 = nameSpeech;
		foreach (string text9 in array4)
		{
			speechFolders[text9.ToLowerInvariant()] = "names";
		}
		array4 = peasantSpeech;
		foreach (string text10 in array4)
		{
			speechFolders[text10.ToLowerInvariant()] = "peasants";
		}
		array4 = troopsSpeech;
		foreach (string text11 in array4)
		{
			speechFolders[text11.ToLowerInvariant()] = "troops";
		}
		array4 = tutorialSpeech;
		foreach (string text12 in array4)
		{
			speechFolders[text12.ToLowerInvariant()] = "tutorial";
		}
		array4 = extraSpeech;
		foreach (string text13 in array4)
		{
			speechFolders[text13.ToLowerInvariant()] = "extra";
		}
		initBinkFolders();
	}

	public void playUISound(int soundID, float setVolume = 1f)
	{
		if (ConfigSettings.Settings_PlayUISFX && Application.isFocused)
		{
			playSound(soundID, setVolume);
		}
	}

	public void playSound(int soundID, float volumeOfset = 1f, float pan = 0f)
	{
		if (throttleSFX(soundID) || soundID < 0 || soundID >= fx_list.Count)
		{
			return;
		}
		sh1_sound_effect sh1_sound_effect = fx_list[soundID];
		if (sh1_sound_effect.first_buffer_no >= 0)
		{
			sh1_sound_effect.last_variant_played++;
			if (sh1_sound_effect.last_variant_played >= sh1_sound_effect.max_variants)
			{
				sh1_sound_effect.last_variant_played = 0;
			}
			int index = sh1_sound_effect.first_buffer_no + sh1_sound_effect.last_variant_played;
			MyAudioManager.Instance.playSFX(play_list[index].clip, play_list[index].volume * volumeOfset, pan);
		}
	}

	private bool throttleSFX(int soundID)
	{
		if (soundID == 76)
		{
			if (!(DateTime.UtcNow > nextAllowableMoo))
			{
				return true;
			}
			nextAllowableMoo = DateTime.UtcNow.AddSeconds(UnityEngine.Random.Range(3, 10));
		}
		return false;
	}

	public void playAmbient(int channel, int soundID, float volumeOfset, bool loop)
	{
		if (soundID < 0 || soundID >= ambient_list.Count)
		{
			return;
		}
		sh1_sound_effect sh1_sound_effect = ambient_list[soundID];
		if (sh1_sound_effect.first_buffer_no >= 0)
		{
			sh1_sound_effect.last_variant_played++;
			if (sh1_sound_effect.last_variant_played >= sh1_sound_effect.max_variants)
			{
				sh1_sound_effect.last_variant_played = 0;
			}
			int index = sh1_sound_effect.first_buffer_no + sh1_sound_effect.last_variant_played;
			MyAudioManager.Instance.playAmbient(channel, play_list[index].clip, play_list[index].volume, volumeOfset, loop);
		}
	}

	public void playSpeech(int channel, string fullpath, float volume)
	{
		fullpath = manageTutorialSpeechAndMissingSpeech(fullpath);
		if (fullpath == "")
		{
			return;
		}
		string text = fullpath.ToLowerInvariant();
		string[] array = text.Split('\\');
		if (array.Length == 0)
		{
			return;
		}
		text = array[^1];
		if (speechFolders.ContainsKey(text))
		{
			bool unitsSpeech = false;
			string text2 = speechFolders[text];
			if (text2 == "troops")
			{
				unitsSpeech = true;
			}
			MyAudioManager.Instance.PlaySpeech(channel, text2, text, force: true, unitsSpeech);
		}
	}

	private string manageTutorialSpeechAndMissingSpeech(string fullpath)
	{
		string text = fullpath.ToLowerInvariant();
		if (text.Contains("tutorial"))
		{
			if (text.EndsWith("tutorial_2.wav"))
			{
				if (!ConfigSettings.Settings_PushMapScrolling)
				{
					return text.Replace("tutorial_2.wav", "tutorial_2a.wav");
				}
			}
			else if (text.EndsWith("tutorial_29.wav"))
			{
				if (!ConfigSettings.Settings_SH1MouseWheel)
				{
					return text.Replace("tutorial_29.wav", "tutorial_29a.wav");
				}
			}
			else
			{
				if (text.EndsWith("tutorial_31a.wav"))
				{
					return text.Replace("tutorial_31a.wav", "tutorial_31.wav");
				}
				if (text.EndsWith("tutorial_31b.wav"))
				{
					return "";
				}
			}
		}
		else
		{
			if (text.Contains("de_dlc4_m5_1"))
			{
				return text.Replace("de_dlc4_m5_1", "de_dlc0_0");
			}
			if (text.Contains("de_dlc4_m5_2"))
			{
				return text.Replace("de_dlc4_m5_2", "de_dlc0_0");
			}
			if (text.Contains("de_dlc4_m5_3"))
			{
				return text.Replace("de_dlc4_m5_3", "de_dlc0_0");
			}
		}
		return fullpath;
	}

	public void playMusic(string fullpath, float gameVolume, bool loop, bool followon, bool fadePrevious = false)
	{
		string text = fullpath.ToLowerInvariant();
		string[] array = text.Split('\\');
		if (array.Length != 0)
		{
			text = array[^1];
			float soundVolume = 1f;
			string text2 = fullpath.ToLowerInvariant();
			if (volumeData.ContainsKey(text2))
			{
				soundVolume = volumeData[text2].volume;
			}
			else
			{
				Debug.Log("Missing Music Volume : " + text2);
			}
			MyAudioManager.Instance.PlayMusic(text, gameVolume, soundVolume, loop, followon, fadePrevious);
		}
	}

	public void playMusic(int ID, bool fadePrevious = false)
	{
		if (ID > 0 && ID < music_filenames.Length && music_filenames[ID].Length > 0)
		{
			string fullpath = music_filenames[ID];
			playMusic(fullpath, 1f, loop: true, followon: false, fadePrevious);
		}
	}

	public void playIntroSpeech(string playerName)
	{
		bool flag = false;
		int month = DateTime.Now.Month;
		int day = DateTime.Now.Day;
		if (month == 12 && day == 14)
		{
			flag = true;
			playSpeech(1, "General_message18.wav", 100f);
		}
		else if (month == 12 && day == 25)
		{
			flag = true;
			playSpeech(1, "General_message17.wav", 100f);
		}
		else if (supportsAdditionalVoicelines())
		{
			if (month == 10 && day == 19)
			{
				flag = true;
				playSpeech(1, "General_message30.wav", 100f);
			}
			else if (month == 9 && day == 25)
			{
				flag = true;
				playSpeech(1, "General_message19.wav", 100f);
			}
			else if (month == 4 && day == 18)
			{
				flag = true;
				playSpeech(1, "General_message20.wav", 100f);
			}
			else if (month == 10 && day == 13)
			{
				flag = true;
				playSpeech(1, "General_message21.wav", 100f);
			}
			else if (month == 10 && day == 25)
			{
				flag = true;
				playSpeech(1, "General_message22.wav", 100f);
			}
			else if (month == 9 && day == 22)
			{
				flag = true;
				playSpeech(1, "General_message23.wav", 100f);
			}
			else if (month == 3 && day == 9)
			{
				flag = true;
				playSpeech(1, "General_message24.wav", 100f);
			}
			else if (month == 11 && day == 4)
			{
				flag = true;
				playSpeech(1, "General_message25.wav", 100f);
			}
			else if (month == 1 && day == 1)
			{
				flag = true;
				playSpeech(1, "General_message26.wav", 100f);
			}
			else if (month == 6 && day == 21)
			{
				flag = true;
				playSpeech(1, "General_message27.wav", 100f);
			}
			else if (month == 12 && day == 21)
			{
				flag = true;
				playSpeech(1, "General_message28.wav", 100f);
			}
			else if (month == 11 && day == GetThanksgivingDate().Day)
			{
				flag = true;
				playSpeech(1, "General_message29.wav", 100f);
			}
		}
		if (ConfigSettings.Settings_CustomIntros && !flag)
		{
			string text = playerName.ToLowerInvariant();
			for (int i = 0; i < stronghold_names_speech_list.Length / 2; i++)
			{
				if (!text.Contains(stronghold_names_speech_list[i * 2].ToLowerInvariant()))
				{
					continue;
				}
				int num = text.IndexOf(stronghold_names_speech_list[i * 2].ToLowerInvariant());
				if (num > 0)
				{
					if (!text.EndsWith(stronghold_names_speech_list[i * 2].ToLowerInvariant()) || text[num - 1] != ' ')
					{
						continue;
					}
				}
				else if (text.Length != stronghold_names_speech_list[i * 2].Length && text[stronghold_names_speech_list[i * 2].Length] != ' ')
				{
					continue;
				}
				playSpeech(1, stronghold_names_speech_list[i * 2 + 1], 100f);
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			playSpeech(1, "general_startgame.wav", 100f);
		}
	}

	private DateTime GetThanksgivingDate()
	{
		DateTime result = DateTime.Now;
		for (int i = 22; i <= 30; i++)
		{
			result = new DateTime(DateTime.Now.Year, 11, i, 0, 0, 0);
			if (result.DayOfWeek == DayOfWeek.Thursday)
			{
				break;
			}
		}
		return result;
	}

	public bool supportsAdditionalVoicelines()
	{
		switch (FatControler.locale)
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
		case "enus":
			return true;
		default:
			return false;
		}
	}

	public void playAdditionalSpeech(string speech)
	{
		if (supportsAdditionalVoicelines())
		{
			playSpeech(1, speech, 1f);
		}
	}

	public void resetFreebuildMessages()
	{
		freeBuildVoicelinesStart = DateTime.MinValue;
	}

	public void startFreebuildMessages()
	{
		if (supportsAdditionalVoicelines())
		{
			freeBuildVoicelinesStart = DateTime.UtcNow;
			freeBuildVoicelinesStage = 0;
		}
		else
		{
			freeBuildVoicelinesStart = DateTime.MinValue;
		}
	}

	public void Update()
	{
		if (!(freeBuildVoicelinesStart != DateTime.MinValue))
		{
			return;
		}
		TimeSpan timeSpan = DateTime.UtcNow - freeBuildVoicelinesStart;
		switch (freeBuildVoicelinesStage)
		{
		case 0:
			if (timeSpan.TotalMinutes > 60.0)
			{
				playAdditionalSpeech("Freebuild_Playtime_1.wav");
				freeBuildVoicelinesStage = 1;
			}
			break;
		case 1:
			if (timeSpan.TotalMinutes > 120.0)
			{
				playAdditionalSpeech("Freebuild_Playtime_2.wav");
				freeBuildVoicelinesStage = 2;
			}
			break;
		case 2:
			if (timeSpan.TotalMinutes > 300.0)
			{
				playAdditionalSpeech("Freebuild_Playtime_3.wav");
				freeBuildVoicelinesStart = DateTime.MinValue;
			}
			break;
		}
	}

	public void playInsult(int insult)
	{
		insult--;
		if (insult >= 0 && insult < 20)
		{
			playSpeech(1, insultSpeech[insult], 100f);
		}
	}

	public void PlayWinTune()
	{
		if (last_win_tune == 0)
		{
			playMusic("fx\\music\\bigwin1.raw", 1f, loop: false, followon: false);
		}
		else if (last_win_tune == 1)
		{
			playMusic("fx\\music\\bigwin2.raw", 1f, loop: false, followon: false);
		}
		else
		{
			playMusic("fx\\music\\bigwin3.raw", 1f, loop: false, followon: false);
		}
		last_win_tune++;
		if (last_win_tune > 2)
		{
			last_win_tune = 0;
		}
	}

	public void PlayLoseTune()
	{
		if (last_lose_tune == 0)
		{
			playMusic("fx\\music\\bigloss1.raw", 1f, loop: false, followon: false);
		}
		else
		{
			playMusic("fx\\music\\bigloss2.raw", 1f, loop: false, followon: false);
		}
		last_lose_tune++;
		if (last_lose_tune > 1)
		{
			last_lose_tune = 0;
		}
	}

	private void initBinkFolders()
	{
		string[] array = buildingBinks;
		foreach (string text in array)
		{
			binkFolders[text.ToLowerInvariant()] = "Buildings";
		}
		array = characterBinks;
		foreach (string text2 in array)
		{
			binkFolders[text2.ToLowerInvariant()] = "Characters";
		}
		array = eventBinks;
		foreach (string text3 in array)
		{
			binkFolders[text3.ToLowerInvariant()] = "Events";
		}
		array = storyBinks;
		foreach (string text4 in array)
		{
			binkFolders[text4.ToLowerInvariant()] = "Story";
		}
	}

	public void playBink(string binkName, bool loop, bool waitForSpeech)
	{
		bool flag = binkIsPlaying;
		float num = getBinkVolume(binkName, processVolume: false);
		binkStarted = true;
		binkLooping = loop;
		binkVolume = num;
		binkName = binkName.Replace(".bik", ".webm");
		if (waitForSpeech && MyAudioManager.Instance.isSpeechPlaying(1))
		{
			binkWaitForSpeech = true;
		}
		else
		{
			binkWaitForSpeech = false;
		}
		string text = "";
		if (binkFolders.ContainsKey(binkName.ToLowerInvariant()))
		{
			text = binkFolders[binkName];
		}
		string uriString = Path.Combine("Assets", "GUI", "Video", text, binkName);
		requestBinkPlaybackURI = new Uri(uriString, UriKind.Relative);
		if (loop)
		{
			requestBinkPlayState = 2;
		}
		else
		{
			requestBinkPlayState = 1;
		}
		if (flag)
		{
			requestBinkPlayState = -requestBinkPlayState;
		}
	}

	public void stopBink()
	{
		if (binkStarted)
		{
			MainViewModel.Instance.HUDRoot.RadarME_Ended();
			requestBinkPlayState = 0;
			binkStarted = false;
			binkIsPlaying = true;
			FatControler.instance.binkPlayWait = false;
		}
	}

	public bool isBinkPlaying()
	{
		if (binkStarted)
		{
			if (binkLooping)
			{
				return true;
			}
			return binkIsPlaying;
		}
		return false;
	}

	public float getBinkVolume(string path, bool processVolume = true)
	{
		float num = 1f;
		switch (Path.GetFileNameWithoutExtension(Path.GetFileName(path)).ToLower())
		{
		case "wf_vict1":
			num = 0.27559054f;
			break;
		case "wf_vict2":
			num = 0.35433072f;
			break;
		case "wf_taunt1":
			num = 0.23622048f;
			break;
		case "wf_taunt2":
			num = 0.27559054f;
			break;
		case "wf_plead1":
			num = 0.27559054f;
			break;
		case "wf_anger1":
			num = 0.23622048f;
			break;
		case "ap_milit6":
			num = 0.27559054f;
			break;
		case "ap_milit7":
			num = 0.27559054f;
			break;
		case "ap_milit4":
			num = 0.31496063f;
			break;
		case "ap_milit5":
			num = 0.31496063f;
			break;
		case "ap_milit9":
			num = 0.31496063f;
			break;
		case "ap_milit12":
			num = 0.31496063f;
			break;
		case "rt_taunt1":
			num = 0.31496063f;
			break;
		case "rt_vict1":
			num = 0.31496063f;
			break;
		case "ap_milit1":
			num = 0.31496063f;
			break;
		case "pig_vict1":
			num = 0.31496063f;
			break;
		case "pig_vict2":
			num = 0.31496063f;
			break;
		case "pig_vict3":
			num = 0.31496063f;
			break;
		case "rt_plead3":
			num = 0.5905512f;
			break;
		case "rt_plead2":
			num = 0.31496063f;
			break;
		case "rt_anger1":
			num = 0.31496063f;
			break;
		case "rt_vict2":
			num = 0.31496063f;
			break;
		case "rt_plead1":
			num = 0.31496063f;
			break;
		case "rt_taunt2":
			num = 0.31496063f;
			break;
		case "well_not_everybody":
			num = 0.62992126f;
			break;
		case "st17_bakers_workshop":
			num = 0.62992126f;
			break;
		case "st16_tanners_workshop":
			num = 0.511811f;
			break;
		case "st15_armourers_workshop":
			num = 0.39370078f;
			break;
		case "st14_poleturners_workshop":
			num = 0.43307087f;
			break;
		case "st12_fletchers_workshop":
			num = 1f;
			break;
		case "st07_hunters_hut":
			num = 0.62992126f;
			break;
		case "st03_woodcutters_hut":
			num = 0.62992126f;
			break;
		case "action_wolves":
			num = 0.62992126f;
			break;
		case "action_steal_bread":
			num = 0.62992126f;
			break;
		case "action_rabbits":
			num = 0.62992126f;
			break;
		case "action_plague":
			num = 0.62992126f;
			break;
		case "action_mad_cows":
			num = 0.62992126f;
			break;
		case "action_fire":
			num = 0.62992126f;
			break;
		case "st13_blacksmiths_workshop":
			num = 0.43307087f;
			break;
		case "action_apples_die":
			num = 0.62992126f;
			break;
		case "intro":
			num = 1f;
			break;
		}
		if (processVolume)
		{
			return ConfigSettings.Settings_SFXVolume * ConfigSettings.Settings_MasterVolume * num;
		}
		return num;
	}
}
