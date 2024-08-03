using System;
using Noesis;
using Stronghold1DE;
using UnityEngine;

public class OnScreenText
{
	public class OST
	{
		public int ostID;

		public bool active;

		public bool activeThisFrame;

		public bool wasTurnedOnOrChanged;

		public bool wasTurnedOff;

		public DateTime timedEnd = DateTime.MinValue;

		public int data1;

		public int data2;

		public int month => data1;

		public int year => data2;

		public int gameSpeed => data1;

		public int message => data1;

		public int curValue => data1;

		public int maxValue => data2;

		public int peopleLeft => data1;
	}

	private static readonly OnScreenText instance;

	public readonly int[] MP_orig_remap_colour_order = new int[9] { 0, 1, 3, 4, 2, 6, 5, 7, 8 };

	public readonly UnityEngine.Color[] MPTeamColours = new UnityEngine.Color[9]
	{
		new UnityEngine.Color(1f, 1f, 1f),
		new UnityEngine.Color(14f / 51f, 14f / 51f, 40f / 51f),
		new UnityEngine.Color(0.76862746f, 0.007843138f, 0.007843138f),
		new UnityEngine.Color(40f / 51f, 0.38039216f, 2f / 85f),
		new UnityEngine.Color(66f / 85f, 0.7647059f, 0f),
		new UnityEngine.Color(48f / 85f, 0f, 48f / 85f),
		new UnityEngine.Color(0.5019608f, 0.5019608f, 0.5019608f),
		new UnityEngine.Color(3f / 85f, 0.75686276f, 0.7490196f),
		new UnityEngine.Color(0.007843138f, 40f / 51f, 0.007843138f)
	};

	public bool inPeaceTime;

	private OST[] OSTs = new OST[32];

	private DateTime startingGoodsExpiry = DateTime.MinValue;

	private DateTime startingGoodsStaticTime = DateTime.MinValue;

	private int[] lastStartingGoodsValues = new int[26];

	private bool startingGoodsPlayedStockpileLine;

	private bool startingGoodsPlayedGranaryLine;

	private bool startingGoodsPlayedArmouryLine;

	private DateTime nextCartFrame = DateTime.MinValue;

	private int cartFrame;

	public static OnScreenText Instance => instance;

	static OnScreenText()
	{
		instance = new OnScreenText();
	}

	private OnScreenText()
	{
	}

	public void initOST()
	{
		for (int i = 0; i < OSTs.Length; i++)
		{
			OST oST = new OST();
			oST.ostID = i;
			oST.activeThisFrame = false;
			oST.wasTurnedOnOrChanged = false;
			oST.wasTurnedOff = false;
			oST.active = false;
			oST.timedEnd = DateTime.MinValue;
			OSTs[i] = oST;
		}
		for (int j = 0; j < 25; j++)
		{
			lastStartingGoodsValues[j] = -1;
		}
		startingGoodsStaticTime = DateTime.MinValue;
		startingGoodsPlayedStockpileLine = false;
		startingGoodsPlayedGranaryLine = false;
		startingGoodsPlayedArmouryLine = false;
		MainViewModel.Instance.OST_Date_Vis = false;
		MainViewModel.Instance.OST_Game_Paused_Vis_Set = false;
		MainViewModel.Instance.OST_Keep_Message_Vis = false;
		MainViewModel.Instance.OST_Feedback_1_Vis = false;
		MainViewModel.Instance.OST_Message_Bar_Vis = false;
		MainViewModel.Instance.OST_Framerate_Vis = false;
		MainViewModel.Instance.OST_GameSpeed_Vis = false;
		MainViewModel.Instance.OST_Starting_goods_Vis = false;
		MainViewModel.Instance.OST_Time_Until_Vis = false;
		MainViewModel.Instance.OST_PeopleLeft_Vis = false;
		MainViewModel.Instance.OST_WhoOwns_Vis = false;
		MainViewModel.Instance.OST_Ping_Vis = false;
		MainViewModel.Instance.OST_KOTH_Vis = false;
		MainViewModel.Instance.OST_MissionFinished_Vis = false;
		for (int k = 0; k < 25; k++)
		{
			MainViewModel.Instance.FeedInGoodsVisible[k] = false;
		}
		inPeaceTime = false;
	}

	public void addOSTEntry(Enums.eOnScreenText ostID, int data1, int data2 = 2)
	{
		if ((ostID == Enums.eOnScreenText.OST_STARTING_GOODS && GameData.Instance.game_type == 0 && GameData.Instance.mission_level == 6 && GameData.Instance.mission6Prestart) || (ostID == Enums.eOnScreenText.OST_KEEP_MESSAGE && GameData.Instance.game_type == 4) || ostID < Enums.eOnScreenText.OST_CHAT || (int)ostID >= OSTs.Length)
		{
			return;
		}
		OST oST = OSTs[(int)ostID];
		if (!oST.active)
		{
			oST.wasTurnedOnOrChanged = true;
		}
		oST.activeThisFrame = true;
		switch (ostID)
		{
		case Enums.eOnScreenText.OST_GAME_SPEED:
			if (oST.data1 != data1)
			{
				oST.wasTurnedOnOrChanged = true;
			}
			oST.data1 = data1;
			oST.timedEnd = DateTime.UtcNow.AddSeconds(5.0);
			break;
		case Enums.eOnScreenText.OST_GAME_PAUSED:
			if (data1 == 1)
			{
				oST.timedEnd = DateTime.UtcNow.AddYears(5);
				oST.active = (oST.activeThisFrame = true);
			}
			else
			{
				oST.timedEnd = DateTime.UtcNow.AddSeconds(-1.0);
			}
			break;
		case Enums.eOnScreenText.OST_KEEP_MESSAGE:
			oST.timedEnd = DateTime.UtcNow.AddYears(5);
			if (oST.data1 != data1)
			{
				oST.wasTurnedOnOrChanged = true;
			}
			oST.data1 = data1;
			oST.data2 = data2;
			break;
		case Enums.eOnScreenText.OST_STARTING_GOODS:
		case Enums.eOnScreenText.OST_PEOPLE_LEFT:
			oST.data1 = data1;
			oST.data2 = data2;
			break;
		case Enums.eOnScreenText.OST_FEEDBACK_1:
		case Enums.eOnScreenText.OST_FEEDBACK_2:
			oST.data1 = data1;
			oST.data2 = data2;
			oST.timedEnd = DateTime.UtcNow.AddSeconds(5.0);
			break;
		case Enums.eOnScreenText.OST_MISSION_FINISHED:
			oST.data1 = data1;
			oST.timedEnd = DateTime.UtcNow.AddYears(5);
			break;
		case Enums.eOnScreenText.OST_WIN_TIMER:
		case Enums.eOnScreenText.OST_TIMETODEFEAT:
			oST.data1 = data1;
			oST.data2 = data2;
			oST.timedEnd = DateTime.UtcNow.AddYears(5);
			break;
		case Enums.eOnScreenText.OST_PEACETIMER:
			oST.data1 = data1;
			oST.data2 = data2;
			oST.timedEnd = DateTime.UtcNow.AddSeconds(5.0);
			break;
		case Enums.eOnScreenText.OST_FRAMERATE:
			oST.timedEnd = DateTime.UtcNow.AddYears(5);
			break;
		case Enums.eOnScreenText.OST_MESSAGE_BAR:
		{
			oST.data1 = data1;
			oST.data2 = data2;
			int num = 10;
			if (data1 == -1 && data2 == 293)
			{
				num = 13;
			}
			oST.timedEnd = DateTime.UtcNow.AddSeconds(num);
			break;
		}
		case Enums.eOnScreenText.OST_PINGS:
		case Enums.eOnScreenText.OST_KING_OF_THE_HILL:
			oST.timedEnd = DateTime.UtcNow.AddYears(5);
			break;
		case Enums.eOnScreenText.OST_WHO_OWNS:
			oST.data1 = data1;
			oST.timedEnd = DateTime.UtcNow.AddYears(5);
			break;
		case (Enums.eOnScreenText)7:
		case (Enums.eOnScreenText)8:
		case (Enums.eOnScreenText)9:
		case (Enums.eOnScreenText)10:
		case Enums.eOnScreenText.OST_POPULARITY:
		case (Enums.eOnScreenText)13:
		case (Enums.eOnScreenText)14:
		case (Enums.eOnScreenText)15:
		case Enums.eOnScreenText.OST_MP_GAME_OVER:
		case (Enums.eOnScreenText)18:
		case Enums.eOnScreenText.OST_SPLIT_MESSAGE:
		case Enums.eOnScreenText.OST_PING_ERROR:
			break;
		}
	}

	public void removeOSTEntry(Enums.eOnScreenText ostID)
	{
		if (ostID >= Enums.eOnScreenText.OST_CHAT && (int)ostID < OSTs.Length)
		{
			OST oST = OSTs[(int)ostID];
			switch (ostID)
			{
			case Enums.eOnScreenText.OST_FRAMERATE:
			case Enums.eOnScreenText.OST_KEEP_MESSAGE:
			case Enums.eOnScreenText.OST_WHO_OWNS:
			case Enums.eOnScreenText.OST_PINGS:
			case Enums.eOnScreenText.OST_KING_OF_THE_HILL:
			case Enums.eOnScreenText.OST_WIN_TIMER:
			case Enums.eOnScreenText.OST_TIMETODEFEAT:
			case Enums.eOnScreenText.OST_PEACETIMER:
				oST.timedEnd = DateTime.UtcNow.AddSeconds(-1.0);
				break;
			}
		}
	}

	public void Update()
	{
		if (MainViewModel.Instance.OST_Cart_Vis && DateTime.UtcNow > nextCartFrame)
		{
			nextCartFrame = DateTime.UtcNow.AddMilliseconds(200.0);
			switch (cartFrame)
			{
			case 0:
				OST_StartingGoods.refCart.Source = MainViewModel.Instance.GameSprites[346];
				break;
			case 1:
				OST_StartingGoods.refCart.Source = MainViewModel.Instance.GameSprites[347];
				break;
			case 2:
				OST_StartingGoods.refCart.Source = MainViewModel.Instance.GameSprites[348];
				break;
			case 3:
				OST_StartingGoods.refCart.Source = MainViewModel.Instance.GameSprites[349];
				break;
			case 4:
				OST_StartingGoods.refCart.Source = MainViewModel.Instance.GameSprites[350];
				break;
			case 5:
				OST_StartingGoods.refCart.Source = MainViewModel.Instance.GameSprites[351];
				break;
			}
			cartFrame++;
			if (cartFrame >= 6)
			{
				cartFrame = 0;
			}
		}
	}

	public void updateOST(EngineInterface.PlayState gameState, bool allowExpire = true)
	{
		if (!MainViewModel.Instance.IsMapEditorMode)
		{
			OST oST = OSTs[1];
			oST.data1 = gameState.month;
			oST.data2 = gameState.year;
			if (!oST.active)
			{
				oST.wasTurnedOnOrChanged = true;
				MainViewModel.Instance.OST_Date_Vis = true;
			}
			oST.activeThisFrame = true;
		}
		if (allowExpire)
		{
			OST[] oSTs = OSTs;
			foreach (OST oST2 in oSTs)
			{
				if (oST2.timedEnd != DateTime.MinValue)
				{
					if (oST2.timedEnd < DateTime.UtcNow)
					{
						oST2.timedEnd = DateTime.MinValue;
						oST2.activeThisFrame = false;
					}
					else
					{
						oST2.activeThisFrame = true;
					}
				}
				if (oST2.active && !oST2.activeThisFrame)
				{
					oST2.wasTurnedOff = true;
				}
				oST2.active = oST2.activeThisFrame;
				oST2.activeThisFrame = false;
			}
		}
		bool wasTurnedOff = false;
		bool wasTurnedOnOrChanged = false;
		bool handleStatChange = true;
		OST oST3 = getOST(Enums.eOnScreenText.OST_DATE, ref wasTurnedOff, ref wasTurnedOnOrChanged, handleStatChange);
		if (oST3 != null)
		{
			MainViewModel.Instance.OST_Date_Text = " " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MONTHS, oST3.month) + " " + oST3.year + " ";
		}
		else if (wasTurnedOff)
		{
			MainViewModel.Instance.OST_Date_Vis = false;
		}
		oST3 = getOST(Enums.eOnScreenText.OST_GAME_PAUSED, ref wasTurnedOff, ref wasTurnedOnOrChanged, handleStatChange);
		if (oST3 != null)
		{
			if (wasTurnedOnOrChanged)
			{
				MainViewModel.Instance.OST_Game_Paused_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_FEEDBACK, 22);
				MainViewModel.Instance.OST_Game_Paused_Vis_Set = true;
			}
		}
		else if (wasTurnedOff)
		{
			MainViewModel.Instance.OST_Game_Paused_Vis_Set = false;
		}
		bool flag = false;
		oST3 = getOST(Enums.eOnScreenText.OST_KEEP_MESSAGE, ref wasTurnedOff, ref wasTurnedOnOrChanged, handleStatChange);
		if (oST3 != null)
		{
			flag = true;
			if (wasTurnedOnOrChanged)
			{
				if (oST3.message == 1)
				{
					SFXManager.instance.playSpeech(1, "other_warning1.wav", 1f);
				}
				else
				{
					SFXManager.instance.playSpeech(1, "other_warning2.wav", 1f);
				}
				MainViewModel.Instance.OST_Keep_Message_Text = "  " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_FEEDBACK, 10 + oST3.message - 1) + "  ";
				MainViewModel.Instance.OST_Keep_Message_Vis = true;
			}
		}
		else if (wasTurnedOff)
		{
			MainViewModel.Instance.OST_Keep_Message_Vis = false;
		}
		if (MainViewModel.Instance.IsMapEditorMode && GameData.Instance.mapType == Enums.GameModes.BUILD && GameData.Instance.lastGameState != null)
		{
			if (GameData.Instance.lastGameState.gotSignpost > 0 || GameData.Instance.multiplayerMap)
			{
				MainViewModel.Instance.OST_Keep_Message_Vis = false;
			}
			else
			{
				MainViewModel.Instance.OST_Keep_Message_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 86);
				MainViewModel.Instance.OST_Keep_Message_Vis = true;
			}
		}
		oST3 = getOST(Enums.eOnScreenText.OST_FEEDBACK_1, ref wasTurnedOff, ref wasTurnedOnOrChanged, handleStatChange);
		if (oST3 != null)
		{
			if (wasTurnedOnOrChanged)
			{
				MainViewModel.Instance.OST_Feedback_1_Text = "  " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_FEEDBACK, oST3.message) + "  ";
				MainViewModel.Instance.OST_Feedback_1_Vis = true;
			}
		}
		else if (wasTurnedOff)
		{
			MainViewModel.Instance.OST_Feedback_1_Vis = false;
		}
		oST3 = getOST(Enums.eOnScreenText.OST_MESSAGE_BAR, ref wasTurnedOff, ref wasTurnedOnOrChanged, handleStatChange);
		if (oST3 != null)
		{
			if (MainViewModel.Instance.Show_HUD_Briefing)
			{
				oST3.timedEnd = DateTime.UtcNow.AddSeconds(10.0);
			}
			if (wasTurnedOnOrChanged)
			{
				MainViewModel.Instance.OST_Message_Bar_Vis = true;
				if (oST3.data1 > 0)
				{
					MainViewModel.Instance.OST_Message_Bar_Text = Translate.Instance.lookUpText((Enums.eTextSections)oST3.data1, oST3.data2);
				}
				else
				{
					MainViewModel.Instance.OST_Message_Bar_Text = Translate.Instance.getMessageLibraryText(oST3.data2);
				}
			}
			if (GameData.Instance.lastGameState.app_mode == 16 && GameData.Instance.lastGameState.app_sub_mode == 1)
			{
				MainViewModel.Instance.OST_Message_Bar_Margin = "0,0,70,210";
			}
			else if (!flag)
			{
				MainViewModel.Instance.OST_Message_Bar_Margin = "0,0,70,160";
			}
			else
			{
				MainViewModel.Instance.OST_Message_Bar_Margin = "0,0,70,200";
			}
		}
		else if (wasTurnedOff)
		{
			MainViewModel.Instance.OST_Message_Bar_Vis = false;
		}
		oST3 = getOST(Enums.eOnScreenText.OST_FRAMERATE, ref wasTurnedOff, ref wasTurnedOnOrChanged, handleStatChange);
		if (oST3 != null)
		{
			MainViewModel.Instance.OST_Framerate_Vis = true;
			MainViewModel.Instance.OST_Framerate_Text = EditorDirector.instance.CurrentFPS + " fps";
			if (MainViewModel.Instance.Show_HUD_Scenario_Button)
			{
				MainViewModel.Instance.OST_Framerate_Margin = "0,7,240,0";
			}
			else if (ConfigSettings.Settings_Compass)
			{
				MainViewModel.Instance.OST_Framerate_Margin = "0,7,90,0";
			}
			else
			{
				MainViewModel.Instance.OST_Framerate_Margin = "0,7,10,0";
			}
		}
		else if (wasTurnedOff)
		{
			MainViewModel.Instance.OST_Framerate_Vis = false;
		}
		oST3 = getOST(Enums.eOnScreenText.OST_GAME_SPEED, ref wasTurnedOff, ref wasTurnedOnOrChanged, handleStatChange);
		if (oST3 != null)
		{
			if (wasTurnedOnOrChanged)
			{
				MainViewModel.Instance.OST_GameSpeed_Vis = true;
				MainViewModel.Instance.OST_GameSpeed_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_FEEDBACK, 23) + " " + oST3.gameSpeed;
			}
			if (oST3.gameSpeed == 40)
			{
				MainViewModel.Instance.OST_GameSpeed_Colour = new SolidColorBrush(Noesis.Color.FromArgb(byte.MaxValue, 218, 165, 32));
			}
			else
			{
				MainViewModel.Instance.OST_GameSpeed_Colour = new SolidColorBrush(Noesis.Color.FromArgb(byte.MaxValue, 239, 243, 198));
			}
			if (MainViewModel.Instance.Show_HUD_Scenario_Button)
			{
				MainViewModel.Instance.OST_GameSpeed_Margin = "0,45,240,-6";
			}
			else if (ConfigSettings.Settings_Compass)
			{
				MainViewModel.Instance.OST_GameSpeed_Margin = "0,45,90,-6";
			}
			else
			{
				MainViewModel.Instance.OST_GameSpeed_Margin = "0,45,10,-6";
			}
		}
		else if (wasTurnedOff)
		{
			MainViewModel.Instance.OST_GameSpeed_Vis = false;
		}
		oST3 = getOST(Enums.eOnScreenText.OST_STARTING_GOODS, ref wasTurnedOff, ref wasTurnedOnOrChanged, handleStatChange);
		if (oST3 != null)
		{
			if (wasTurnedOnOrChanged)
			{
				if (!MainViewModel.Instance.OST_Starting_goods_Vis)
				{
					startCart();
				}
				MainViewModel.Instance.OST_Starting_goods_Vis = true;
				string text = "  " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_STARTUP, oST3.message) + "  ";
				if (MainViewModel.Instance.FeedInGoodsAmountList[0] != text)
				{
					MainViewModel.Instance.FeedInGoodsAmountList[0] = text;
				}
				MainViewModel.Instance.FeedInGoodsVisible[0] = true;
				startingGoodsExpiry = DateTime.UtcNow.AddSeconds(20.0);
				startingGoodsStaticTime = DateTime.UtcNow.AddSeconds(10.0);
				for (int j = 1; j < 25; j++)
				{
					MainViewModel.Instance.FeedInGoodsVisible[j] = GameData.Instance.lastGameState.keep_storage[j] > 0;
				}
			}
			if (oST3.message == 0 && startingGoodsExpiry < DateTime.UtcNow)
			{
				startingGoodsExpiry = DateTime.MinValue;
				MainViewModel.Instance.FeedInGoodsVisible[0] = false;
			}
			if (oST3.message == 0)
			{
				for (int k = 1; k < 25; k++)
				{
					if (lastStartingGoodsValues[k] != GameData.Instance.lastGameState.keep_storage[k])
					{
						startingGoodsStaticTime = DateTime.UtcNow.AddSeconds(10.0);
					}
					lastStartingGoodsValues[k] = GameData.Instance.lastGameState.keep_storage[k];
				}
				if ((GameData.Instance.lastGameState.app_mode == 14 && (GameData.Instance.lastGameState.app_sub_mode == 49 || GameData.Instance.lastGameState.app_sub_mode == 13)) || Director.instance.Paused)
				{
					startingGoodsStaticTime = DateTime.UtcNow.AddSeconds(10.0);
				}
				if (startingGoodsStaticTime < DateTime.UtcNow)
				{
					MainViewModel.Instance.FeedInGoodsVisible[0] = true;
					string text2 = "";
					bool flag2 = false;
					bool flag3 = false;
					bool flag4 = false;
					if (lastStartingGoodsValues[1] > 0 || lastStartingGoodsValues[2] > 0 || lastStartingGoodsValues[3] > 0 || lastStartingGoodsValues[4] > 0 || lastStartingGoodsValues[5] > 0 || lastStartingGoodsValues[6] > 0 || lastStartingGoodsValues[7] > 0 || lastStartingGoodsValues[8] > 0 || lastStartingGoodsValues[9] > 0 || lastStartingGoodsValues[16] > 0)
					{
						flag2 = true;
					}
					if (lastStartingGoodsValues[1] > 0 || lastStartingGoodsValues[2] > 0 || lastStartingGoodsValues[3] > 0 || lastStartingGoodsValues[4] > 0 || lastStartingGoodsValues[5] > 0)
					{
						flag3 = true;
					}
					if (lastStartingGoodsValues[17] > 0 || lastStartingGoodsValues[18] > 0 || lastStartingGoodsValues[19] > 0 || lastStartingGoodsValues[20] > 0 || lastStartingGoodsValues[21] > 0 || lastStartingGoodsValues[22] > 0 || lastStartingGoodsValues[23] > 0 || lastStartingGoodsValues[24] > 0)
					{
						flag4 = true;
					}
					if (flag2)
					{
						text2 = "  " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 132) + "  ";
						if (!startingGoodsPlayedStockpileLine)
						{
							SFXManager.instance.playSpeech(1, "Placement_Warning11.wav", 1f);
						}
						startingGoodsPlayedStockpileLine = true;
					}
					else if (flag3)
					{
						text2 = "  " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 133) + "  ";
						if (!startingGoodsPlayedGranaryLine)
						{
							SFXManager.instance.playSpeech(1, "Placement_Warning10.wav", 1f);
						}
						startingGoodsPlayedGranaryLine = true;
					}
					else if (flag4)
					{
						text2 = "  " + Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 134) + "  ";
						if (!startingGoodsPlayedArmouryLine)
						{
							SFXManager.instance.playSpeech(1, "Placement_Warning12.wav", 1f);
						}
						startingGoodsPlayedArmouryLine = true;
					}
					if (!flag2)
					{
						startingGoodsPlayedStockpileLine = false;
					}
					if (!flag3)
					{
						startingGoodsPlayedGranaryLine = false;
					}
					if (!flag4)
					{
						startingGoodsPlayedArmouryLine = false;
					}
					if (MainViewModel.Instance.FeedInGoodsAmountList[0] != text2)
					{
						MainViewModel.Instance.FeedInGoodsAmountList[0] = text2;
					}
				}
				else
				{
					startingGoodsPlayedStockpileLine = false;
					startingGoodsPlayedGranaryLine = false;
					startingGoodsPlayedArmouryLine = false;
				}
			}
			for (int l = 1; l < 25; l++)
			{
				if (GameData.Instance.lastGameState.keep_storage[l] > 0)
				{
					string text3 = "  " + GameData.Instance.lastGameState.keep_storage[l];
					if (MainViewModel.Instance.FeedInGoodsAmountList[l] != text3)
					{
						MainViewModel.Instance.FeedInGoodsAmountList[l] = text3;
					}
					if (!MainViewModel.Instance.FeedInGoodsVisible[l])
					{
						MainViewModel.Instance.FeedInGoodsVisible[l] = true;
					}
				}
				else if (MainViewModel.Instance.FeedInGoodsVisible[l])
				{
					MainViewModel.Instance.FeedInGoodsVisible[l] = false;
				}
			}
		}
		else if (wasTurnedOff)
		{
			for (int m = 0; m < 25; m++)
			{
				MainViewModel.Instance.FeedInGoodsVisible[m] = false;
			}
			MainViewModel.Instance.OST_Starting_goods_Vis = false;
		}
		bool flag5 = false;
		oST3 = getOST(Enums.eOnScreenText.OST_TIMETODEFEAT, ref wasTurnedOff, ref wasTurnedOnOrChanged, handleStatChange);
		if (oST3 != null)
		{
			flag5 = true;
			if (wasTurnedOnOrChanged)
			{
				MainViewModel.Instance.OST_Time_Until_Vis = true;
				MainViewModel.Instance.OST_Time_Until_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_OBJECTIVES, 17);
			}
			if (oST3.maxValue > 0)
			{
				MainViewModel.Instance.OST_Time_Until_Width = Math.Min((oST3.maxValue - oST3.curValue) * 158 / oST3.maxValue, 158);
			}
			else
			{
				MainViewModel.Instance.OST_Time_Until_Width = 0;
			}
		}
		else if (wasTurnedOff)
		{
			MainViewModel.Instance.OST_Time_Until_Vis = false;
		}
		if (!flag5)
		{
			oST3 = getOST(Enums.eOnScreenText.OST_WIN_TIMER, ref wasTurnedOff, ref wasTurnedOnOrChanged, handleStatChange);
			if (oST3 != null)
			{
				flag5 = true;
				if (wasTurnedOnOrChanged)
				{
					MainViewModel.Instance.OST_Time_Until_Vis = true;
					MainViewModel.Instance.OST_Time_Until_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_OBJECTIVES, 25);
				}
				if (oST3.maxValue > 0)
				{
					MainViewModel.Instance.OST_Time_Until_Width = Math.Min((oST3.maxValue - oST3.curValue) * 158 / oST3.maxValue, 158);
				}
				else
				{
					MainViewModel.Instance.OST_Time_Until_Width = 0;
				}
			}
			else if (wasTurnedOff)
			{
				MainViewModel.Instance.OST_Time_Until_Vis = false;
			}
		}
		inPeaceTime = false;
		if (!flag5)
		{
			oST3 = getOST(Enums.eOnScreenText.OST_PEACETIMER, ref wasTurnedOff, ref wasTurnedOnOrChanged, handleStatChange);
			if (oST3 != null)
			{
				inPeaceTime = true;
				if (wasTurnedOnOrChanged)
				{
					MainViewModel.Instance.OST_Time_Until_Vis = true;
					MainViewModel.Instance.OST_Time_Until_Text = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT2, 99);
				}
				if (oST3.maxValue > 0)
				{
					MainViewModel.Instance.OST_Time_Until_Width = Math.Min((oST3.maxValue - oST3.curValue) * 158 / oST3.maxValue, 158);
				}
				else
				{
					MainViewModel.Instance.OST_Time_Until_Width = 0;
				}
			}
			else if (wasTurnedOff)
			{
				MainViewModel.Instance.OST_Time_Until_Vis = false;
			}
		}
		oST3 = getOST(Enums.eOnScreenText.OST_PEOPLE_LEFT, ref wasTurnedOff, ref wasTurnedOnOrChanged, handleStatChange);
		if (oST3 != null)
		{
			if (wasTurnedOnOrChanged)
			{
				MainViewModel.Instance.OST_PeopleLeft_Vis = true;
			}
			MainViewModel.Instance.OST_PeopleLeft_Text = GameData.Instance.lastGameState.chimps_count + "/" + GameData.Instance.lastGameState.chimps_limit;
			MainViewModel.Instance.OST_StructsLeft_Text = GameData.Instance.lastGameState.structs_count + "/" + GameData.Instance.lastGameState.structs_limit;
			MainViewModel.Instance.OST_TreesLeft_Text = GameData.Instance.lastGameState.orgs_count + "/" + GameData.Instance.lastGameState.orgs_limit;
			MainViewModel.Instance.OST_RocksLeft_Text = GameData.Instance.lastGameState.minerals_count + "/" + GameData.Instance.lastGameState.minerals_limit;
			MainViewModel.Instance.OST_TribesLeft_Text = GameData.Instance.lastGameState.tribes_count + "/" + GameData.Instance.lastGameState.tribes_limit;
		}
		else if (wasTurnedOff)
		{
			MainViewModel.Instance.OST_PeopleLeft_Vis = false;
		}
		oST3 = getOST(Enums.eOnScreenText.OST_WHO_OWNS, ref wasTurnedOff, ref wasTurnedOnOrChanged, handleStatChange);
		if (oST3 != null)
		{
			int data = oST3.data1;
			string text4 = "  " + Platform_Multiplayer.Instance.getPlayerName(data) + "  ";
			if (text4.Length > 0)
			{
				MainViewModel.Instance.OST_WhoOwns_Vis = true;
				MainViewModel.Instance.OST_WhoOwns_Text = text4;
			}
			else
			{
				MainViewModel.Instance.OST_WhoOwns_Vis = false;
			}
		}
		else if (wasTurnedOff)
		{
			MainViewModel.Instance.OST_WhoOwns_Vis = false;
		}
		oST3 = getOST(Enums.eOnScreenText.OST_KING_OF_THE_HILL, ref wasTurnedOff, ref wasTurnedOnOrChanged, handleStatChange);
		if (oST3 != null)
		{
			if (wasTurnedOnOrChanged)
			{
				MainViewModel.Instance.OST_KOTH_Vis = true;
				for (int n = 1; n < 9; n++)
				{
					MainViewModel.Instance.OST_KOTH_Visible[n - 1] = false;
				}
				for (int num = 1; num < 9; num++)
				{
					string playerName = Platform_Multiplayer.Instance.getPlayerName(num, activeOnly: true);
					if (playerName.Length > 0)
					{
						MainViewModel.Instance.OST_KOTH_Visible[num - 1] = true;
						MainViewModel.Instance.OST_KOTH_Name[num - 1] = playerName;
						UnityEngine.Color color = MPTeamColours[MP_orig_remap_colour_order[num]];
						SolidColorBrush value = new SolidColorBrush(Noesis.Color.FromRgb((byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f)));
						MainViewModel.Instance.OST_KOTH_Color[num - 1] = value;
					}
				}
			}
			for (int num2 = 1; num2 < 9; num2++)
			{
				MainViewModel.Instance.OST_KOTH_Value[num2 - 1] = GameData.Instance.lastGameState.koth_scores[num2 - 1].ToString();
			}
		}
		else if (wasTurnedOff)
		{
			MainViewModel.Instance.OST_KOTH_Vis = false;
		}
		oST3 = getOST(Enums.eOnScreenText.OST_PINGS, ref wasTurnedOff, ref wasTurnedOnOrChanged, handleStatChange);
		if (oST3 != null)
		{
			if (wasTurnedOnOrChanged)
			{
				MainViewModel.Instance.OST_Ping_Vis = true;
				for (int num3 = 1; num3 < 9; num3++)
				{
					MainViewModel.Instance.OST_Ping_Visible[num3 - 1] = false;
				}
				for (int num4 = 1; num4 < 9; num4++)
				{
					string playerName2 = Platform_Multiplayer.Instance.getPlayerName(num4, activeOnly: true);
					if (playerName2.Length > 0 && num4 != GameData.Instance.playerID)
					{
						MainViewModel.Instance.OST_Ping_Visible[num4 - 1] = true;
						MainViewModel.Instance.OST_Ping_Name[num4 - 1] = playerName2;
						UnityEngine.Color color2 = MPTeamColours[SpriteMapping.remapColours[SpriteMapping.RemapMPLoadedColour(num4)]];
						SolidColorBrush value2 = new SolidColorBrush(Noesis.Color.FromRgb((byte)(color2.r * 255f), (byte)(color2.g * 255f), (byte)(color2.b * 255f)));
						MainViewModel.Instance.OST_Ping_Color[num4 - 1] = value2;
					}
				}
			}
			for (int num5 = 1; num5 < 9; num5++)
			{
				MainViewModel.Instance.OST_Ping_Value[num5 - 1] = GameData.Instance.lastGameState.pingtimes[num5 - 1] + "ms";
				if (GameData.Instance.lastGameState.pingtimes[num5 - 1] < 50)
				{
					MainViewModel.Instance.OST_Ping_Value_Color[num5 - 1] = MainViewModel.Instance._OST_PingLow_Colour;
				}
				else if (GameData.Instance.lastGameState.pingtimes[num5 - 1] < 120)
				{
					MainViewModel.Instance.OST_Ping_Value_Color[num5 - 1] = MainViewModel.Instance._OST_PingMid_Colour;
				}
				else
				{
					MainViewModel.Instance.OST_Ping_Value_Color[num5 - 1] = MainViewModel.Instance._OST_PingHigh_Colour;
				}
			}
		}
		else if (wasTurnedOff)
		{
			MainViewModel.Instance.OST_Ping_Vis = false;
		}
		oST3 = getOST(Enums.eOnScreenText.OST_MISSION_FINISHED, ref wasTurnedOff, ref wasTurnedOnOrChanged, handleStatChange);
		if (oST3 != null)
		{
			if (wasTurnedOnOrChanged)
			{
				MainViewModel.Instance.OST_MissionFinished_Vis = true;
				MainViewModel.Instance.SetObjectivePopupState(visible: false);
				MainViewModel.Instance.SetGoodsPopupState(visible: false);
				MainViewModel.Instance.Show_HUD_Extras = false;
				string oST_MissionFinished_Text = ((oST3.message != 1) ? Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAINOPTIONS, Enums.eTextValues.TEXT_SCN_NEW_MESSAGE) : Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAINOPTIONS, Enums.eTextValues.TEXT_SCN_MOOD4));
				MainViewModel.Instance.OST_MissionFinished_Text = oST_MissionFinished_Text;
			}
		}
		else if (wasTurnedOff)
		{
			MainViewModel.Instance.OST_MissionFinished_Vis = false;
		}
	}

	public OST getOST(Enums.eOnScreenText ostID, ref bool wasTurnedOff, ref bool wasTurnedOnOrChanged, bool handleStatChange = false)
	{
		wasTurnedOnOrChanged = (wasTurnedOff = false);
		if (isOSTActive(ostID))
		{
			wasTurnedOff = OSTs[(int)ostID].wasTurnedOff;
			wasTurnedOnOrChanged = OSTs[(int)ostID].wasTurnedOnOrChanged;
			if (handleStatChange)
			{
				OSTs[(int)ostID].wasTurnedOff = false;
				OSTs[(int)ostID].wasTurnedOnOrChanged = false;
			}
			return OSTs[(int)ostID];
		}
		if (ostID >= Enums.eOnScreenText.OST_CHAT && (int)ostID < OSTs.Length)
		{
			wasTurnedOff = OSTs[(int)ostID].wasTurnedOff;
			if (handleStatChange)
			{
				OSTs[(int)ostID].wasTurnedOff = false;
			}
		}
		return null;
	}

	public bool isOSTActive(Enums.eOnScreenText ostID)
	{
		if (ostID >= Enums.eOnScreenText.OST_CHAT && (int)ostID < OSTs.Length)
		{
			return OSTs[(int)ostID].active;
		}
		return false;
	}

	public void startCart()
	{
		MainViewModel.Instance.CartDistance = MainViewModel.iUIScaleValueWidth;
		MainViewModel.Instance.CartDuration = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 8, 0));
		OST_StartingGoods.refCartStory.Stop();
		OST_StartingGoods.refCartStory.Begin();
		MainViewModel.Instance.OST_Cart_Vis = true;
	}
}
