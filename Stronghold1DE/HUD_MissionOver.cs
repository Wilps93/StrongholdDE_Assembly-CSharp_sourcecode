using System;
using System.Collections.ObjectModel;
using Noesis;
using NoesisApp;
using UnityEngine;

namespace Stronghold1DE;

public class HUD_MissionOver : UserControl
{
	public MediaElement refMissionOverVideo;

	private bool initialised;

	private bool Victory;

	private string[] victory_videos = new string[10] { "victory_banquet.webm", "victory_economic.webm", "victory_military_big.webm", "victory_military_small.webm", "victory_pig.webm", "victory_rat.webm", "victory_resources.webm", "victory_snake.webm", "victory_stockpile.webm", "victory_jewel_big.webm" };

	private string[] defeat_videos = new string[8] { "defeat_baddies.webm", "defeat_general.webm", "defeat_pig.webm", "defeat_rat.webm", "defeat_ruins.webm", "defeat_snake.webm", "defeat_stocks.webm", "defeat_wolf.webm" };

	private int numActivePlayers;

	private int this_player;

	private int id;

	private int temp;

	private int total;

	private int[] ranking = new int[9];

	private int width;

	private int pie;

	private int star_x;

	private int[][] individual_ranking;

	private int[] points = new int[9];

	private int[] enemy_killed = new int[9];

	private int[] yours_killed = new int[9];

	private int[] stars = new int[9];

	private int num_players;

	private const int MAX_HUMANS = 9;

	private const int RANK_ENEMY = 0;

	private const int RANK_BUILDINGS = 1;

	private const int RANK_FOOD = 2;

	private const int RANK_GOLD = 3;

	private const int RANK_POP = 4;

	private const int RANK_WOOD = 5;

	private const int RANK_STONE = 6;

	private const int RANK_IRON = 7;

	private const int RANK_PITCH = 8;

	private const int RANK_KOTH = 9;

	public static string FutureVoiceLine = "";

	private static DateTime FutureVoiceLineStart = DateTime.MinValue;

	public HUD_MissionOver()
	{
		InitializeComponent();
		MainViewModel.Instance.HUDMissionOver = this;
	}

	public void init()
	{
		if (!initialised)
		{
			refMissionOverVideo = MainViewModel.Instance.IngameUI.refMissionOverVideo;
			refMissionOverVideo.MediaEnded += FrontendBackVideo_Ended;
			initialised = true;
		}
	}

	public static void ShowVictory(Enums.VictoryScreens screen, EngineInterface.ScoreData scoreData)
	{
		MainViewModel.Instance.HUDMissionOver.LoadVictoryScreen(screen, scoreData);
		MainViewModel.Instance.Show_HUD_MissionOver = true;
	}

	public static void ShowDefeat(Enums.DefeatScreens screen)
	{
		MainViewModel.Instance.HUDMissionOver.LoadDefeatScreen(screen);
		MainViewModel.Instance.Show_HUD_MissionOver = true;
	}

	public static void ShowMPVictory(Enums.VictoryScreens screen, EngineInterface.MPScoreData scoreData)
	{
		MainViewModel.Instance.HUDMissionOver.LoadMPVictoryScreen(screen, scoreData);
		MainViewModel.Instance.Show_HUD_MissionOver = true;
	}

	public static void ShowMPDefeat(Enums.DefeatScreens screen, EngineInterface.MPScoreData scoreData)
	{
		MainViewModel.Instance.HUDMissionOver.LoadMPDefeatScreen(screen, scoreData);
		MainViewModel.Instance.Show_HUD_MissionOver = true;
	}

	private void LoadVictoryScreen(Enums.VictoryScreens screen, EngineInterface.ScoreData lastScores)
	{
		LoadVideoBackground("Victory/" + victory_videos[(int)screen]);
		MainViewModel.Instance.MO_SP_Score = Visibility.Visible;
		MainViewModel.Instance.MO_DefeatVis = Visibility.Hidden;
		MainViewModel.Instance.MO_MP_Score = Visibility.Hidden;
		Victory = true;
		int num = 0;
		if (GameData.Instance.currentMapName.Length > 0)
		{
			num = ConfigSettings.ManageScores(GameData.Instance.currentMapName, lastScores.score, lastScores.difficulty_level);
		}
		MainViewModel.Instance.MO_LevelPoints = lastScores.levelPoints.ToString();
		MainViewModel.Instance.MO_ScorePoints = lastScores.score.ToString();
		MainViewModel.Instance.MO_LastScorePoints = num.ToString();
		if (lastScores.score_months > 0)
		{
			MainViewModel.Instance.MO_MonthText = lastScores.score_months + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_DEMOSCORE, 3);
			MainViewModel.Instance.MO_MonthPoints = lastScores.score_months_points.ToString();
			MainViewModel.Instance.MO_Months = Visibility.Visible;
		}
		else
		{
			MainViewModel.Instance.MO_Months = Visibility.Collapsed;
		}
		if (lastScores.score_troops > 0)
		{
			if (!FatControler.turkish)
			{
				MainViewModel.Instance.MO_TroopsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_DEMOSCORE, 13) + " " + lastScores.troops_percent_lost + "% =";
			}
			else
			{
				MainViewModel.Instance.MO_TroopsText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_DEMOSCORE, 13) + " %" + lastScores.troops_percent_lost + " =";
			}
			MainViewModel.Instance.MO_TroopsPoints = lastScores.score_troops.ToString();
			MainViewModel.Instance.MO_Troops = Visibility.Visible;
		}
		else
		{
			MainViewModel.Instance.MO_Troops = Visibility.Collapsed;
		}
		for (int i = lastScores.items_count; i < 7; i++)
		{
			MainViewModel.Instance.MO_OtherPointsVisible[i] = Visibility.Collapsed;
		}
		for (int j = 0; j < lastScores.items_count; j++)
		{
			if (lastScores.items_extra_type[j] == -1)
			{
				MainViewModel.Instance.MO_OtherPointsText[j] = lastScores.items_extra[j] + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_DEMOSCORE, 12);
			}
			else
			{
				MainViewModel.Instance.MO_OtherPointsText[j] = lastScores.items_extra[j] + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GOODS, lastScores.items_extra_type[j]) + " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_DEMOSCORE, 11);
			}
			MainViewModel.Instance.MO_OtherPoints[j] = lastScores.items_extra_points[j].ToString();
			MainViewModel.Instance.MO_OtherPointsVisible[j] = Visibility.Visible;
		}
		if (GameData.Instance.siegeThat)
		{
			MainViewModel.Instance.MO_SiegeThat = Visibility.Visible;
			MainViewModel.Instance.MO_SiegeThatName = GameData.Instance.currentMapName;
			MainViewModel.Instance.MO_ST_Attackers = lastScores.siege_attackers_score.ToString();
			MainViewModel.Instance.MO_ST_Defenders = lastScores.siege_defenders_score.ToString();
			MainViewModel.Instance.MO_ST_Castle = lastScores.siege_that_score.ToString();
		}
		else
		{
			MainViewModel.Instance.MO_SiegeThat = Visibility.Collapsed;
		}
	}

	private void LoadDefeatScreen(Enums.DefeatScreens screen)
	{
		LoadVideoBackground("Defeat/" + defeat_videos[(int)screen]);
		MainViewModel.Instance.MO_SP_Score = Visibility.Hidden;
		MainViewModel.Instance.MO_DefeatVis = Visibility.Visible;
		MainViewModel.Instance.MO_MP_Score = Visibility.Hidden;
		Victory = false;
	}

	private void LoadMPVictoryScreen(Enums.VictoryScreens screen, EngineInterface.MPScoreData scoreData)
	{
		LoadVideoBackground("Victory/" + victory_videos[(int)screen]);
		ShowMPScore(scoreData);
		MainViewModel.Instance.MO_SP_Score = Visibility.Hidden;
		MainViewModel.Instance.MO_DefeatVis = Visibility.Hidden;
		MainViewModel.Instance.MO_MP_Score = Visibility.Visible;
		MainViewModel.Instance.MO_MP_Victory = Visibility.Visible;
		MainViewModel.Instance.MO_MP_Defeat = Visibility.Collapsed;
		Victory = true;
	}

	private void LoadMPDefeatScreen(Enums.DefeatScreens screen, EngineInterface.MPScoreData scoreData)
	{
		LoadVideoBackground("Defeat/" + defeat_videos[(int)screen]);
		ShowMPScore(scoreData);
		MainViewModel.Instance.MO_SP_Score = Visibility.Hidden;
		MainViewModel.Instance.MO_DefeatVis = Visibility.Hidden;
		MainViewModel.Instance.MO_MP_Score = Visibility.Visible;
		MainViewModel.Instance.MO_MP_Victory = Visibility.Collapsed;
		MainViewModel.Instance.MO_MP_Defeat = Visibility.Visible;
		Victory = false;
	}

	private void ShowMPScore(EngineInterface.MPScoreData lastMPScores)
	{
		prepMPScores(lastMPScores);
		for (int i = 0; i < numActivePlayers; i++)
		{
			MainViewModel.Instance.MO_MP_PlayersVisible[i] = Visibility.Visible;
		}
		for (int j = numActivePlayers; j < 8; j++)
		{
			MainViewModel.Instance.MO_MP_PlayersVisible[j] = Visibility.Collapsed;
		}
		int num = 0;
		for (int k = 1; k < 9; k++)
		{
			int num2 = ranking[k];
			if (lastMPScores.valid[num2] > 0)
			{
				ObservableCollection<string> playerValuesRow = GetPlayerValuesRow(num);
				playerValuesRow[0] = Platform_Multiplayer.Instance.getPlayerName(num2);
				playerValuesRow[1] = enemy_killed[num2].ToString();
				playerValuesRow[2] = yours_killed[num2].ToString();
				playerValuesRow[3] = lastMPScores.enemy_buildings_destroyed[num2].ToString();
				playerValuesRow[4] = lastMPScores.gold_acquired[num2].ToString();
				playerValuesRow[5] = lastMPScores.food_produced[num2].ToString();
				playerValuesRow[6] = lastMPScores.wood_produced[num2].ToString();
				playerValuesRow[7] = lastMPScores.stone_produced[num2].ToString();
				playerValuesRow[8] = lastMPScores.iron_produced[num2].ToString();
				playerValuesRow[9] = lastMPScores.pitch_produced[num2].ToString();
				playerValuesRow[10] = lastMPScores.max_population[num2].ToString();
				ObservableCollection<Visibility> playerIconsRow = GetPlayerIconsRow(num);
				for (int l = 0; l < 14; l++)
				{
					playerIconsRow[l] = Visibility.Collapsed;
				}
				int num3 = lastMPScores.lords_killed[num2];
				int num4 = 0;
				if (lastMPScores.winners[num2] > 0)
				{
					num4++;
				}
				num4 += stars[num2];
				for (int m = 0; m < num4 && m < 7; m++)
				{
					playerIconsRow[m] = Visibility.Visible;
				}
				for (int n = 0; n < num3 && n < 7; n++)
				{
					playerIconsRow[n + 7] = Visibility.Visible;
				}
				SetPlayerShieldsImageSource(num, FRONT_Multiplayer.GetColourShield(Platform_Multiplayer.Instance.getPlayerColour(num2)));
				int fearPie = lastMPScores.fearfactor[num2] * 6 + -lastMPScores.minfearfactor[num2];
				SetPlayerFearImageSource(num, GetPlayerFear(fearPie));
				num++;
			}
		}
	}

	private ObservableCollection<string> GetPlayerValuesRow(int rowID)
	{
		return rowID switch
		{
			0 => MainViewModel.Instance.MO_MP_Players1Values, 
			1 => MainViewModel.Instance.MO_MP_Players2Values, 
			2 => MainViewModel.Instance.MO_MP_Players3Values, 
			3 => MainViewModel.Instance.MO_MP_Players4Values, 
			4 => MainViewModel.Instance.MO_MP_Players5Values, 
			5 => MainViewModel.Instance.MO_MP_Players6Values, 
			6 => MainViewModel.Instance.MO_MP_Players7Values, 
			7 => MainViewModel.Instance.MO_MP_Players8Values, 
			_ => null, 
		};
	}

	private ObservableCollection<Visibility> GetPlayerIconsRow(int rowID)
	{
		return rowID switch
		{
			0 => MainViewModel.Instance.MO_MP_Players1Icons, 
			1 => MainViewModel.Instance.MO_MP_Players2Icons, 
			2 => MainViewModel.Instance.MO_MP_Players3Icons, 
			3 => MainViewModel.Instance.MO_MP_Players4Icons, 
			4 => MainViewModel.Instance.MO_MP_Players5Icons, 
			5 => MainViewModel.Instance.MO_MP_Players6Icons, 
			6 => MainViewModel.Instance.MO_MP_Players7Icons, 
			7 => MainViewModel.Instance.MO_MP_Players8Icons, 
			_ => null, 
		};
	}

	private void SetPlayerShieldsImageSource(int rowID, ImageSource image)
	{
		switch (rowID)
		{
		case 0:
			MainViewModel.Instance.MO_MP_PlayersShields0 = image;
			break;
		case 1:
			MainViewModel.Instance.MO_MP_PlayersShields1 = image;
			break;
		case 2:
			MainViewModel.Instance.MO_MP_PlayersShields2 = image;
			break;
		case 3:
			MainViewModel.Instance.MO_MP_PlayersShields3 = image;
			break;
		case 4:
			MainViewModel.Instance.MO_MP_PlayersShields4 = image;
			break;
		case 5:
			MainViewModel.Instance.MO_MP_PlayersShields5 = image;
			break;
		case 6:
			MainViewModel.Instance.MO_MP_PlayersShields6 = image;
			break;
		case 7:
			MainViewModel.Instance.MO_MP_PlayersShields7 = image;
			break;
		}
	}

	private void SetPlayerFearImageSource(int rowID, ImageSource image)
	{
		switch (rowID)
		{
		case 0:
			MainViewModel.Instance.MO_MP_PlayersFear0 = image;
			break;
		case 1:
			MainViewModel.Instance.MO_MP_PlayersFear1 = image;
			break;
		case 2:
			MainViewModel.Instance.MO_MP_PlayersFear2 = image;
			break;
		case 3:
			MainViewModel.Instance.MO_MP_PlayersFear3 = image;
			break;
		case 4:
			MainViewModel.Instance.MO_MP_PlayersFear4 = image;
			break;
		case 5:
			MainViewModel.Instance.MO_MP_PlayersFear5 = image;
			break;
		case 6:
			MainViewModel.Instance.MO_MP_PlayersFear6 = image;
			break;
		case 7:
			MainViewModel.Instance.MO_MP_PlayersFear7 = image;
			break;
		}
	}

	private ImageSource GetPlayerFear(int fearPie)
	{
		return MainViewModel.Instance.GameSprites[115 + fearPie];
	}

	private void prepMPScores(EngineInterface.MPScoreData mp_stats)
	{
		numActivePlayers = 0;
		if (individual_ranking == null)
		{
			individual_ranking = new int[10][];
			for (int i = 0; i < 10; i++)
			{
				individual_ranking[i] = new int[9];
			}
		}
		for (this_player = 1; this_player < 9; this_player++)
		{
			total = 0;
			points[this_player] = 0;
			for (int i = 1; i < 9; i++)
			{
				if (i != this_player)
				{
					total += mp_stats.who_killed_who[this_player][i];
				}
			}
			enemy_killed[this_player] = total;
			total = 0;
			for (int i = 0; i < 9; i++)
			{
				if (i != this_player)
				{
					total += mp_stats.who_killed_who[i][this_player];
				}
			}
			yours_killed[this_player] = total;
		}
		for (int i = 1; i < 9; i++)
		{
			ranking[i] = i;
			for (int j = 0; j < 10; j++)
			{
				individual_ranking[j][i] = i;
			}
			stars[i] = 0;
		}
		for (int i = 1; i < 9; i++)
		{
			for (int j = 1; j < 9 - i; j++)
			{
				if (mp_stats.time_deceased[ranking[j]] < mp_stats.time_deceased[ranking[j + 1]])
				{
					temp = ranking[j];
					ranking[j] = ranking[j + 1];
					ranking[j + 1] = temp;
				}
				else if (mp_stats.time_deceased[ranking[j]] == mp_stats.time_deceased[ranking[j + 1]] && mp_stats.winners[ranking[j]] < mp_stats.winners[ranking[j + 1]])
				{
					temp = ranking[j];
					ranking[j] = ranking[j + 1];
					ranking[j + 1] = temp;
				}
				if (enemy_killed[individual_ranking[0][j]] < enemy_killed[individual_ranking[0][j + 1]])
				{
					temp = individual_ranking[0][j];
					individual_ranking[0][j] = individual_ranking[0][j + 1];
					individual_ranking[0][j + 1] = temp;
				}
				if (mp_stats.enemy_buildings_destroyed[individual_ranking[1][j]] < mp_stats.enemy_buildings_destroyed[individual_ranking[1][j + 1]])
				{
					temp = individual_ranking[1][j];
					individual_ranking[1][j] = individual_ranking[1][j + 1];
					individual_ranking[1][j + 1] = temp;
				}
				if (mp_stats.food_produced[individual_ranking[2][j]] < mp_stats.food_produced[individual_ranking[2][j + 1]])
				{
					temp = individual_ranking[2][j];
					individual_ranking[2][j] = individual_ranking[2][j + 1];
					individual_ranking[2][j + 1] = temp;
				}
				if (mp_stats.gold_acquired[individual_ranking[3][j]] < mp_stats.gold_acquired[individual_ranking[3][j + 1]])
				{
					temp = individual_ranking[3][j];
					individual_ranking[3][j] = individual_ranking[3][j + 1];
					individual_ranking[3][j + 1] = temp;
				}
				if (mp_stats.max_population[individual_ranking[4][j]] < mp_stats.max_population[individual_ranking[4][j + 1]])
				{
					temp = individual_ranking[4][j];
					individual_ranking[4][j] = individual_ranking[4][j + 1];
					individual_ranking[4][j + 1] = temp;
				}
				if (mp_stats.wood_produced[individual_ranking[5][j]] < mp_stats.wood_produced[individual_ranking[5][j + 1]])
				{
					temp = individual_ranking[5][j];
					individual_ranking[5][j] = individual_ranking[5][j + 1];
					individual_ranking[5][j + 1] = temp;
				}
				if (mp_stats.stone_produced[individual_ranking[6][j]] < mp_stats.stone_produced[individual_ranking[6][j + 1]])
				{
					temp = individual_ranking[6][j];
					individual_ranking[6][j] = individual_ranking[6][j + 1];
					individual_ranking[6][j + 1] = temp;
				}
				if (mp_stats.iron_produced[individual_ranking[7][j]] < mp_stats.iron_produced[individual_ranking[7][j + 1]])
				{
					temp = individual_ranking[7][j];
					individual_ranking[7][j] = individual_ranking[7][j + 1];
					individual_ranking[7][j + 1] = temp;
				}
				if (mp_stats.pitch_produced[individual_ranking[8][j]] < mp_stats.pitch_produced[individual_ranking[8][j + 1]])
				{
					temp = individual_ranking[8][j];
					individual_ranking[8][j] = individual_ranking[8][j + 1];
					individual_ranking[8][j + 1] = temp;
				}
				if (mp_stats.king_of_the_hill_points[individual_ranking[9][j]] > mp_stats.king_of_the_hill_points[individual_ranking[9][j + 1]])
				{
					temp = individual_ranking[9][j];
					individual_ranking[9][j] = individual_ranking[9][j + 1];
					individual_ranking[9][j + 1] = temp;
				}
			}
		}
		if (enemy_killed[individual_ranking[0][1]] > 10 && enemy_killed[individual_ranking[0][1]] > enemy_killed[individual_ranking[0][2]] * 2)
		{
			points[individual_ranking[0][1]] += 3;
		}
		if (mp_stats.enemy_buildings_destroyed[individual_ranking[1][1]] > 2 && mp_stats.enemy_buildings_destroyed[individual_ranking[1][1]] > mp_stats.enemy_buildings_destroyed[individual_ranking[1][2]] * 2)
		{
			points[individual_ranking[1][1]] += 2;
		}
		if (mp_stats.food_produced[individual_ranking[2][1]] > 50 && mp_stats.food_produced[individual_ranking[2][1]] > mp_stats.food_produced[individual_ranking[2][2]] * 2)
		{
			points[individual_ranking[2][1]] += 2;
		}
		if (mp_stats.gold_acquired[individual_ranking[3][1]] > 100 && mp_stats.gold_acquired[individual_ranking[3][1]] > mp_stats.gold_acquired[individual_ranking[3][2]] * 2)
		{
			points[individual_ranking[3][1]] += 3;
		}
		if (mp_stats.max_population[individual_ranking[4][1]] > 24 && mp_stats.max_population[individual_ranking[4][1]] > mp_stats.max_population[individual_ranking[4][2]] * 2)
		{
			points[individual_ranking[0][1]]++;
		}
		if (mp_stats.wood_produced[individual_ranking[5][1]] > 100 && mp_stats.wood_produced[individual_ranking[5][1]] > mp_stats.wood_produced[individual_ranking[5][2]] * 2)
		{
			points[individual_ranking[5][1]]++;
		}
		if (mp_stats.stone_produced[individual_ranking[6][1]] > 50 && mp_stats.stone_produced[individual_ranking[6][1]] > mp_stats.stone_produced[individual_ranking[6][2]] * 2)
		{
			points[individual_ranking[6][1]]++;
		}
		if (mp_stats.iron_produced[individual_ranking[7][1]] > 10 && mp_stats.iron_produced[individual_ranking[7][1]] > mp_stats.iron_produced[individual_ranking[7][2]] * 2)
		{
			points[individual_ranking[7][1]]++;
		}
		if (mp_stats.pitch_produced[individual_ranking[8][1]] > 20 && mp_stats.pitch_produced[individual_ranking[8][1]] > mp_stats.pitch_produced[individual_ranking[8][2]] * 2)
		{
			points[individual_ranking[8][1]]++;
		}
		num_players = 0;
		for (id = 1; id < 9; id++)
		{
			if (mp_stats.valid[1] > 0)
			{
				num_players++;
			}
		}
		if (GameData.Instance.multiplayerKOTHMap)
		{
			if (mp_stats.king_of_the_hill_points[individual_ranking[9][1]] == 0)
			{
				int num = mp_stats.king_of_the_hill_points[individual_ranking[9][2]];
				if (num != 0 && mp_stats.king_of_the_hill_points_start != 0)
				{
					stars[individual_ranking[9][1]] = Math.Min(5, 5 * num / mp_stats.king_of_the_hill_points_start);
				}
			}
		}
		else
		{
			for (this_player = 1; this_player < 9; this_player++)
			{
				stars[this_player] = points[this_player] / 3;
				if (stars[this_player] > 5)
				{
					stars[this_player] = 5;
				}
				if (num_players < 4 && stars[this_player] == 5)
				{
					stars[this_player] = 4;
				}
			}
		}
		for (int k = 1; k < 9; k++)
		{
			int num2 = ranking[k];
			if (mp_stats.valid[num2] > 0)
			{
				numActivePlayers++;
			}
		}
	}

	public void LoadVideoBackground(string fileName)
	{
		init();
		string text = "Assets/GUI/Video/" + fileName;
		if (Screen.width <= 1920 && Screen.height <= 1080)
		{
			text = text.Replace(".webm", "_low.webm");
		}
		text += "*";
		Uri source = new Uri(text, UriKind.Relative);
		refMissionOverVideo.Source = source;
		PlayBackgroundVideo(state: true);
	}

	public static void setFutureVoiceLine(string wav)
	{
		FutureVoiceLine = wav;
		FutureVoiceLineStart = DateTime.UtcNow.AddSeconds(2.0);
	}

	public void Update()
	{
		if (FutureVoiceLine.Length <= 0)
		{
			return;
		}
		if (!MyAudioManager.Instance.isSpeechPlaying(1))
		{
			if (DateTime.UtcNow > FutureVoiceLineStart)
			{
				SFXManager.instance.playSpeech(1, FutureVoiceLine, 1f);
				FutureVoiceLine = "";
			}
		}
		else
		{
			FutureVoiceLineStart = DateTime.UtcNow.AddSeconds(1.0);
		}
	}

	public void ButtonClicked()
	{
		MainViewModel.Instance.Show_HUD_MissionOver = false;
		if (GameData.Instance.game_type == 0)
		{
			if (Victory)
			{
				MainViewModel.Instance.Show_Story_DEBUG = false;
				MainViewModel.Instance.InitNewScene(Enums.SceneIDS.Story);
				MainViewModel.Instance.StartStory(GameData.Instance.mission_level, 2);
				MainViewModel.Instance.StoryAdvance();
			}
			else
			{
				MainViewModel.Instance.StartCampaignMission(GameData.Instance.mission_level);
			}
		}
		else if (GameData.Instance.game_type == 5)
		{
			if (Victory)
			{
				if (GameData.Instance.mission_level < 37)
				{
					MainViewModel.Instance.StartEcoCampaignMission(GameData.Instance.mission_level - 32 + 1);
				}
				else
				{
					MainViewModel.Instance.InitNewScene(Enums.SceneIDS.FrontEnd);
				}
			}
			else
			{
				MainViewModel.Instance.StartEcoCampaignMission(GameData.Instance.mission_level - 32);
			}
		}
		else if (GameData.Instance.game_type == 7 || GameData.Instance.game_type == 8 || GameData.Instance.game_type == 9 || GameData.Instance.game_type == 10)
		{
			if (Victory)
			{
				MainViewModel.Instance.Show_Story_DEBUG = false;
				MainViewModel.Instance.InitNewScene(Enums.SceneIDS.Story);
				MainViewModel.Instance.StartStory(GameData.Instance.mission_level, 2);
				MainViewModel.Instance.StoryAdvance();
			}
			else
			{
				MainViewModel.Instance.StartExtraCampaignMission((GameData.Instance.mission_level - 30) / 10, GameData.Instance.mission_level % 10);
			}
		}
		else if (GameData.Instance.game_type == 12)
		{
			if (Victory)
			{
				MainViewModel.Instance.Show_Story_DEBUG = false;
				MainViewModel.Instance.InitNewScene(Enums.SceneIDS.Story);
				MainViewModel.Instance.StartStory(GameData.Instance.mission_level, 2);
				MainViewModel.Instance.StoryAdvance();
			}
			else
			{
				MainViewModel.Instance.StartExtraEcoCampaignMission(GameData.Instance.mission_level % 10);
			}
		}
		else if (GameData.Instance.game_type == 11)
		{
			if (Victory)
			{
				MainViewModel.Instance.InitNewScene(Enums.SceneIDS.FrontEnd);
				if (GameData.Instance.mission_text_id < 10)
				{
					FrontendMenus.CurrentSelectedTrailMission = GameData.Instance.mission_text_id + 1;
				}
				else
				{
					FrontendMenus.CurrentSelectedTrailMission = GameData.Instance.mission_text_id;
				}
				MainViewModel.Instance.FrontEndMenu.ButtonClicked("Trail");
			}
			else
			{
				MainViewModel.Instance.InitNewScene(Enums.SceneIDS.FrontEnd);
				FrontendMenus.CurrentSelectedTrailMission = GameData.Instance.mission_text_id;
				MainViewModel.Instance.FrontEndMenu.ButtonClicked("Trail");
			}
		}
		else if (GameData.Instance.game_type == 13)
		{
			if (Victory)
			{
				MainViewModel.Instance.InitNewScene(Enums.SceneIDS.FrontEnd);
				if (GameData.Instance.mission_text_id < 10)
				{
					FrontendMenus.CurrentSelectedTrail2Mission = GameData.Instance.mission_text_id + 1;
				}
				else
				{
					FrontendMenus.CurrentSelectedTrail2Mission = GameData.Instance.mission_text_id;
				}
				MainViewModel.Instance.FrontEndMenu.ButtonClicked("Trail2");
			}
			else
			{
				MainViewModel.Instance.InitNewScene(Enums.SceneIDS.FrontEnd);
				FrontendMenus.CurrentSelectedTrail2Mission = GameData.Instance.mission_text_id;
				MainViewModel.Instance.FrontEndMenu.ButtonClicked("Trail2");
			}
		}
		else
		{
			MainViewModel.Instance.InitNewScene(Enums.SceneIDS.FrontEnd);
		}
	}

	public void PlayBackgroundVideo(bool state)
	{
		if (state)
		{
			FrontendMenus.UpdateVideoScale();
			refMissionOverVideo.Play();
		}
		else
		{
			refMissionOverVideo.Stop();
			refMissionOverVideo.Source = null;
			refMissionOverVideo.Close();
		}
	}

	private void FrontendBackVideo_Ended(object sender, RoutedEventArgs args)
	{
	}

	private void InitializeComponent()
	{
		Noesis.GUI.LoadComponent(this, "Assets/GUI/XAMLResources/HUD_MissionOver.xaml");
	}
}
