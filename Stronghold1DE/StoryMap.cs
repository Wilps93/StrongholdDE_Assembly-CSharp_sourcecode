using System.IO;
using Noesis;
using UnityEngine;

namespace Stronghold1DE;

public class StoryMap
{
	private static StoryMap _instance = null;

	private readonly string StoryMapFile = "Assets/GUI/Images/CampaignMapHotspots.png";

	private const int MaxCounties = 20;

	private const int MapWidth = 2300;

	private const int MapHeight = 1700;

	private const int MapSize = 3910000;

	private int[,] FlagOfsets = new int[21, 2];

	private byte[] CountyMap = new byte[3910000];

	private static Noesis.Color NeutralColor = Noesis.Color.FromArgb(0, 0, 0, 0);

	private static Noesis.Color PlayerColor = Noesis.Color.FromArgb(byte.MaxValue, 0, 136, byte.MaxValue);

	private static Noesis.Color RatColor = Noesis.Color.FromArgb(byte.MaxValue, byte.MaxValue, 136, 0);

	private static Noesis.Color SnakeColor = Noesis.Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);

	private static Noesis.Color PigColor = Noesis.Color.FromArgb(byte.MaxValue, byte.MaxValue, 68, 68);

	private static Noesis.Color WolfColor = Noesis.Color.FromArgb(byte.MaxValue, 68, 68, 68);

	private Noesis.Color thisColour;

	private int[,] hotspots = new int[9, 2]
	{
		{ 0, 0 },
		{ 1, 0 },
		{ -1, 0 },
		{ 0, 1 },
		{ 0, -1 },
		{ 2, 0 },
		{ -2, 0 },
		{ 0, 2 },
		{ 0, -2 }
	};

	private byte[,] mapInfo = new byte[22, 22]
	{
		{
			1, 1, 1, 1, 4, 4, 1, 4, 4, 5,
			5, 5, 5, 3, 3, 3, 6, 6, 6, 3,
			1, 6
		},
		{
			1, 2, 1, 1, 4, 4, 1, 4, 4, 5,
			5, 5, 5, 3, 3, 3, 6, 6, 6, 3,
			1, 6
		},
		{
			2, 2, 1, 1, 4, 4, 1, 4, 4, 5,
			5, 5, 5, 3, 3, 3, 6, 6, 6, 3,
			1, 6
		},
		{
			3, 2, 2, 1, 4, 4, 1, 4, 4, 5,
			5, 5, 5, 3, 3, 3, 6, 6, 6, 3,
			1, 6
		},
		{
			4, 2, 2, 2, 4, 4, 1, 4, 4, 5,
			5, 5, 5, 3, 3, 3, 6, 6, 6, 3,
			1, 6
		},
		{
			5, 2, 2, 2, 2, 4, 1, 4, 4, 5,
			5, 5, 5, 3, 3, 3, 6, 6, 6, 3,
			1, 6
		},
		{
			6, 2, 2, 2, 2, 2, 1, 4, 4, 5,
			5, 5, 5, 3, 3, 3, 6, 6, 6, 3,
			1, 6
		},
		{
			7, 2, 2, 2, 2, 2, 2, 4, 4, 5,
			5, 5, 5, 3, 3, 3, 6, 6, 6, 3,
			1, 6
		},
		{
			8, 2, 2, 2, 2, 2, 2, 5, 4, 5,
			5, 5, 5, 3, 3, 3, 6, 6, 6, 3,
			3, 6
		},
		{
			9, 2, 2, 2, 2, 2, 2, 2, 2, 5,
			5, 5, 5, 3, 3, 3, 6, 6, 6, 3,
			3, 6
		},
		{
			10, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			5, 5, 5, 3, 3, 3, 6, 6, 6, 3,
			3, 6
		},
		{
			11, 2, 2, 2, 2, 2, 2, 2, 2, 3,
			2, 5, 5, 3, 3, 3, 6, 6, 6, 3,
			3, 6
		},
		{
			12, 2, 2, 2, 2, 2, 2, 2, 2, 3,
			2, 2, 5, 3, 3, 3, 6, 6, 6, 3,
			3, 6
		},
		{
			13, 2, 2, 2, 2, 3, 2, 2, 2, 3,
			2, 2, 2, 3, 3, 3, 6, 6, 6, 3,
			3, 6
		},
		{
			14, 2, 2, 2, 2, 3, 3, 2, 2, 3,
			2, 2, 6, 2, 3, 3, 6, 6, 6, 3,
			3, 6
		},
		{
			15, 2, 2, 2, 2, 3, 3, 2, 2, 3,
			2, 2, 6, 6, 3, 3, 6, 6, 6, 3,
			3, 6
		},
		{
			15, 2, 2, 2, 2, 3, 2, 2, 2, 3,
			2, 2, 6, 6, 3, 3, 6, 6, 6, 3,
			3, 6
		},
		{
			14, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 6, 6, 3, 2, 6, 6, 6, 3,
			3, 6
		},
		{
			16, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 6, 6, 2, 2, 6, 6, 6, 2,
			2, 6
		},
		{
			17, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 6, 6, 2,
			2, 6
		},
		{
			18, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 6, 2,
			2, 2
		},
		{
			18, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2
		}
	};

	private bool mapLoaded;

	public static StoryMap Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new StoryMap();
			}
			return _instance;
		}
	}

	public void SetupStoryMap()
	{
		LoadMapFile(StoryMapFile);
	}

	public void setupMapChapter()
	{
		for (int i = 1; i <= 20; i++)
		{
			switch (GetMapOwner(i))
			{
			case 1:
				thisColour = NeutralColor;
				break;
			case 2:
				thisColour = PlayerColor;
				break;
			case 4:
				thisColour = RatColor;
				break;
			case 5:
				thisColour = SnakeColor;
				break;
			case 3:
				thisColour = PigColor;
				break;
			case 6:
				thisColour = WolfColor;
				break;
			}
			switch (i)
			{
			case 1:
				MainViewModel.Instance.CountyColor1 = thisColour;
				break;
			case 2:
				MainViewModel.Instance.CountyColor2 = thisColour;
				break;
			case 3:
				MainViewModel.Instance.CountyColor3 = thisColour;
				break;
			case 4:
				MainViewModel.Instance.CountyColor4 = thisColour;
				break;
			case 5:
				MainViewModel.Instance.CountyColor5 = thisColour;
				break;
			case 6:
				MainViewModel.Instance.CountyColor6 = thisColour;
				break;
			case 7:
				MainViewModel.Instance.CountyColor7 = thisColour;
				break;
			case 8:
				MainViewModel.Instance.CountyColor8 = thisColour;
				break;
			case 9:
				MainViewModel.Instance.CountyColor9 = thisColour;
				break;
			case 10:
				MainViewModel.Instance.CountyColor10 = thisColour;
				break;
			case 11:
				MainViewModel.Instance.CountyColor11 = thisColour;
				break;
			case 12:
				MainViewModel.Instance.CountyColor12 = thisColour;
				break;
			case 13:
				MainViewModel.Instance.CountyColor13 = thisColour;
				break;
			case 14:
				MainViewModel.Instance.CountyColor14 = thisColour;
				break;
			case 15:
				MainViewModel.Instance.CountyColor15 = thisColour;
				break;
			case 16:
				MainViewModel.Instance.CountyColor16 = thisColour;
				break;
			case 17:
				MainViewModel.Instance.CountyColor17 = thisColour;
				break;
			case 18:
				MainViewModel.Instance.CountyColor18 = thisColour;
				break;
			case 19:
				MainViewModel.Instance.CountyColor19 = thisColour;
				break;
			case 20:
				MainViewModel.Instance.CountyColor20 = thisColour;
				break;
			}
		}
	}

	public int lookUpCounty(int x, int y)
	{
		x = (int)((float)x * 2f);
		y = (int)((float)y * 2f);
		int num = 0;
		if (x < 2)
		{
			return 0;
		}
		if (x >= 2298)
		{
			return 0;
		}
		if (y < 2)
		{
			return 0;
		}
		if (y >= 1698)
		{
			return 0;
		}
		for (int i = 0; i < 9; i++)
		{
			num = CountyMap[(1699 - y + hotspots[i, 1]) * 2300 + x + hotspots[i, 0]];
			if (num > 0)
			{
				return num;
			}
		}
		return 0;
	}

	public void lookUpFlagStartPos(int MapStage)
	{
		int num = 0;
		int num2 = -475;
		int num3 = ((MapStage != 0) ? mapInfo[MapStage - 1, 0] : mapInfo[MapStage, 0]);
		if (num3 > 0 && num3 <= 20)
		{
			num += FlagOfsets[num3, 0];
			num2 += 1700 - FlagOfsets[num3, 1];
		}
		MainViewModel.Instance.FlagXPos = (int)((float)num / 2f);
		MainViewModel.Instance.FlagYPos = (int)((float)num2 / 2f);
	}

	public void lookUpFlagEndPos()
	{
		int num = 0;
		int num2 = -475;
		int num3 = mapInfo[MainViewModel.Instance.MapStage, 0];
		if (num3 > 0 && num3 <= 20)
		{
			num += FlagOfsets[num3, 0];
			num2 += 1700 - FlagOfsets[num3, 1];
		}
		MainViewModel.Instance.FlagXPos2 = (int)((float)num / 2f);
		MainViewModel.Instance.FlagYPos2 = (int)((float)num2 / 2f);
	}

	public int GetMapOwner(int county)
	{
		if (county <= 0)
		{
			return 0;
		}
		return mapInfo[MainViewModel.Instance.MapStage, county];
	}

	public bool LoadMapFile(string filePath)
	{
		if (mapLoaded)
		{
			return true;
		}
		mapLoaded = true;
		byte b = 0;
		Texture2D texture2D = null;
		UnityEngine.Color color = new Color32(byte.MaxValue, 0, byte.MaxValue, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(200, 200, 200, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(190, 190, 190, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(180, 180, 180, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(170, 170, 170, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(160, 160, 160, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(150, 150, 150, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(140, 140, 140, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(130, 130, 130, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(120, 120, 120, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(110, 110, 110, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(100, 100, 100, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(90, 90, 90, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(80, 80, 80, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(70, 70, 70, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(60, 60, 60, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(50, 50, 50, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(40, 40, 40, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(30, 30, 30, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(20, 20, 20, byte.MaxValue);
		_ = (UnityEngine.Color)new Color32(10, 10, 10, byte.MaxValue);
		if (File.Exists(filePath))
		{
			byte[] data = File.ReadAllBytes(filePath);
			texture2D = new Texture2D(2, 2, UnityEngine.TextureFormat.RGBA32, mipChain: false, linear: true);
			texture2D.LoadImage(data);
			UnityEngine.Color[] pixels = texture2D.GetPixels();
			int width = texture2D.width;
			for (int i = 0; i < 1700; i++)
			{
				for (int j = 0; j < 2300; j++)
				{
					UnityEngine.Color color2 = pixels[j + i * width];
					if (color2.a == 0f || color2 == color)
					{
						CountyMap[j + i * 2300] = 0;
						continue;
					}
					switch (((int)(color2.g * 256f) + 5) / 10 * 10)
					{
					case 200:
						CountyMap[j + i * 2300] = 1;
						continue;
					case 190:
						CountyMap[j + i * 2300] = 2;
						continue;
					case 180:
						CountyMap[j + i * 2300] = 3;
						continue;
					case 170:
						CountyMap[j + i * 2300] = 4;
						continue;
					case 160:
						CountyMap[j + i * 2300] = 5;
						continue;
					case 150:
						CountyMap[j + i * 2300] = 6;
						continue;
					case 140:
						CountyMap[j + i * 2300] = 7;
						continue;
					case 130:
						CountyMap[j + i * 2300] = 8;
						continue;
					case 120:
						CountyMap[j + i * 2300] = 9;
						continue;
					case 110:
						CountyMap[j + i * 2300] = 10;
						continue;
					case 100:
						CountyMap[j + i * 2300] = 11;
						continue;
					case 90:
						CountyMap[j + i * 2300] = 12;
						continue;
					case 80:
						CountyMap[j + i * 2300] = 13;
						continue;
					case 70:
						CountyMap[j + i * 2300] = 14;
						continue;
					case 60:
						CountyMap[j + i * 2300] = 15;
						continue;
					case 50:
						CountyMap[j + i * 2300] = 16;
						continue;
					case 40:
						CountyMap[j + i * 2300] = 17;
						continue;
					case 30:
						CountyMap[j + i * 2300] = 18;
						continue;
					case 20:
						CountyMap[j + i * 2300] = 19;
						continue;
					case 10:
						CountyMap[j + i * 2300] = 20;
						continue;
					}
					if ((double)color2.r > 0.5 && (double)color2.g < 0.1)
					{
						b = CountyMap[j - 1 + i * 2300];
						CountyMap[j + i * 2300] = b;
						FlagOfsets[b, 0] = j;
						FlagOfsets[b, 1] = i;
					}
				}
			}
			Object.DestroyImmediate(texture2D);
		}
		return true;
	}
}
