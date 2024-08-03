using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Noesis;
using Stronghold1DE;
using UnityEngine;

public class Director : MonoBehaviour
{
	public static Director instance = null;

	public static CultureInfo defaultCulture = CultureInfo.CreateSpecificCulture("en-US");

	[HideInInspector]
	public static bool gameStarted = false;

	[HideInInspector]
	public static int gameOver = 0;

	public Texture2D swordCursor;

	public Texture2D handCursor;

	public Texture2D deleteCursor;

	public Texture2D deleteNotCursor;

	public Texture2D swordCursorX;

	public Texture2D handCursorX;

	public Texture2D deleteCursorX;

	public Texture2D deleteNotCursorX;

	public Texture2D waitCursor;

	private CursorMode cursorMode;

	private Vector2 hotspot = new Vector2(7f, 1f);

	private int currentLevel;

	private int lastLevel;

	private int bootstrap;

	private long updateStartTime;

	private int simTickCount;

	private bool simThreadRunning;

	private bool engineRunning;

	private Thread simThread;

	private int FramesToProcess;

	private int FrameToSkipMP;

	private bool InEngine;

	private Stopwatch debugStopwatch;

	private int tickTimingCount;

	private long totalMS;

	private double engineFrameTime = 0.025;

	private int lowFPSCompensationLevel = 1;

	private int lowFPSCount;

	private bool wasPaused;

	private bool paused;

	private int numTiles;

	private Action postUpdateCallback;

	private DateTime lastNoesisGUICheck = DateTime.MinValue;

	private int currentCursorType = -1;

	private bool multiplayerGame;

	public int EngineFrameRate => (int)(1.0 / engineFrameTime);

	public float EngineFrameTime => (float)engineFrameTime;

	public bool Paused => paused;

	public int NumTiles => numTiles;

	public bool SimRunning => engineRunning;

	public bool MultiplayerGame => multiplayerGame;

	private void Awake()
	{
		instance = this;
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		MemoryBuffers.init();
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		engineRunning = false;
		InEngine = false;
		simThread = new Thread(runSimTickTest);
		simThread.Name = "Stronghold1Engine";
		simThread.Start();
		debugStopwatch = Stopwatch.StartNew();
		Time.fixedDeltaTime = (float)engineFrameTime;
		EndMultiplayer();
		LoadImages();
	}

	public void IncreaseFrameRate()
	{
		int num = 90;
		double num2 = Math.Round(1.0 / engineFrameTime);
		if (num2 < (double)num)
		{
			num2 += 5.0;
			if (num2 > (double)num)
			{
				num2 = num;
			}
			SetEngineFrameRate(num2);
			OnScreenText.Instance.addOSTEntry(Enums.eOnScreenText.OST_GAME_SPEED, (int)num2);
			if (!MultiplayerGame)
			{
				ConfigSettings.Settings_GameSpeed = (int)num2;
				ConfigSettings.SaveSettings();
			}
		}
	}

	public void DecreaseFrameRate()
	{
		double num = Math.Round(1.0 / engineFrameTime);
		if (num > 10.0)
		{
			num -= 5.0;
			if (num < 10.0)
			{
				num = 10.0;
			}
			SetEngineFrameRate(num);
			OnScreenText.Instance.addOSTEntry(Enums.eOnScreenText.OST_GAME_SPEED, (int)num);
			if (!MultiplayerGame)
			{
				ConfigSettings.Settings_GameSpeed = (int)num;
				ConfigSettings.SaveSettings();
			}
		}
	}

	public void SetEngineFrameRate(double fps)
	{
		if (fps < 10.0 || fps > 90.0)
		{
			fps = 40.0;
		}
		if (fps <= 10.0)
		{
			lowFPSCompensationLevel = 4;
		}
		else if (fps <= 15.0)
		{
			lowFPSCompensationLevel = 3;
		}
		else if (fps <= 20.0)
		{
			lowFPSCompensationLevel = 2;
		}
		else
		{
			lowFPSCompensationLevel = 1;
		}
		engineFrameTime = 1.0 / fps;
		Time.fixedDeltaTime = (float)engineFrameTime / (float)lowFPSCompensationLevel;
	}

	public void ResetFrameRate()
	{
		Time.fixedDeltaTime = (float)engineFrameTime / (float)lowFPSCompensationLevel;
	}

	private void FixedUpdate()
	{
		UpdateMultiplayer(fromUpdate: false);
		if (engineRunning && !paused)
		{
			FramesToProcess++;
		}
	}

	public void forceEarlyEngine()
	{
		if (engineRunning && FramesToProcess == 0)
		{
			FramesToProcess++;
		}
	}

	public void MPSkipFrame(int numToSkip)
	{
		if (multiplayerGame)
		{
			FrameToSkipMP += numToSkip;
		}
	}

	public void startSimThread()
	{
		simTickCount = 0;
		FramesToProcess = 0;
		FrameToSkipMP = 0;
		engineRunning = true;
		paused = false;
	}

	public void stopSimThread()
	{
		if (engineRunning)
		{
			GC.Collect();
			engineRunning = false;
			FramesToProcess = 0;
			FrameToSkipMP = 0;
			EndMultiplayer();
		}
	}

	public void SetPausedState(bool state)
	{
		if (!state || !multiplayerGame)
		{
			paused = state;
			if (paused)
			{
				EditorDirector.instance.GamePaused();
			}
		}
	}

	public void TogglePausedState()
	{
		SetPausedState(!paused);
	}

	public bool SafeToSave(bool wait = false)
	{
		if (engineRunning)
		{
			if (FramesToProcess <= 0 && !InEngine)
			{
				wasPaused = paused;
				paused = true;
				return true;
			}
			if (wait)
			{
				int num = 100;
				while (num > 0)
				{
					num--;
					Thread.Sleep(10);
					if (FramesToProcess <= 0 && !InEngine)
					{
						wasPaused = paused;
						paused = true;
						return true;
					}
				}
			}
		}
		return false;
	}

	public void FinishedSaving()
	{
		paused = wasPaused;
	}

	public int getSimTickCount()
	{
		return simTickCount;
	}

	public int getAverageCalcTime()
	{
		if (tickTimingCount > 0)
		{
			return (int)(totalMS / tickTimingCount);
		}
		return 0;
	}

	private void runSimTickTest()
	{
		DateTime dateTime = DateTime.UtcNow.AddSeconds(2.0);
		while (DateTime.UtcNow < dateTime)
		{
			int num = 0;
			for (int i = 0; i < 1000000; i++)
			{
				num++;
			}
		}
		simThreadRunning = true;
		while (simThreadRunning)
		{
			if (FramesToProcess > 0)
			{
				InEngine = true;
				FramesToProcess--;
				simTickCount++;
				long elapsedMilliseconds = debugStopwatch.ElapsedMilliseconds;
				bool mpFrameSkip = false;
				if (MultiplayerGame)
				{
					mpFrameSkip = FrameToSkipMP > 0;
				}
				else if (lowFPSCompensationLevel > 1)
				{
					lowFPSCount++;
					if (lowFPSCount >= lowFPSCompensationLevel)
					{
						lowFPSCount = 0;
					}
					else
					{
						mpFrameSkip = true;
					}
				}
				numTiles = EngineInterface.run(mpFrameSkip);
				if (FrameToSkipMP > 0)
				{
					FrameToSkipMP--;
				}
				long elapsedMilliseconds2 = debugStopwatch.ElapsedMilliseconds;
				tickTimingCount++;
				totalMS += elapsedMilliseconds2 - elapsedMilliseconds;
				InEngine = false;
			}
			else
			{
				Thread.Sleep(1);
			}
		}
	}

	private void OnDestroy()
	{
		if (simThreadRunning)
		{
			simThreadRunning = false;
			simThread.Join();
		}
	}

	private void Start()
	{
	}

	public void SetPostUpdateCallback(Action callback)
	{
		postUpdateCallback = callback;
	}

	private void Update()
	{
		monitorCursors();
		UpdateMultiplayer(fromUpdate: true);
		if (SimRunning)
		{
			GameMap.instance.PreCalcScreenCentre();
			TilemapManager.instance.startTileRefresh();
		}
		bool flag = false;
		while (!paused)
		{
			MemoryBuffers.MemBuffer nextBufferToRender = MemoryBuffers.instance.getNextBufferToRender();
			if (nextBufferToRender == null)
			{
				break;
			}
			if (SimRunning)
			{
				GameMap.instance.processTestMap(nextBufferToRender.memory, nextBufferToRender.numTiles, nextBufferToRender.gameState, nextBufferToRender.radarMap);
				Platform_Multiplayer.Instance.SendChores(nextBufferToRender.MPChores);
			}
			MemoryBuffers.instance.returnBuffer(nextBufferToRender);
			FatControler.instance.NoesisGUIUpdateChecksInGame();
			lastNoesisGUICheck = DateTime.UtcNow;
			flag = true;
			if (postUpdateCallback != null)
			{
				break;
			}
		}
		if (flag && postUpdateCallback != null)
		{
			postUpdateCallback();
			postUpdateCallback = null;
		}
		if (SimRunning)
		{
			GameMap.instance.ApplyRadarMap();
			if (!flag)
			{
				GameMap.instance.experimentalRowManager();
			}
			TilemapManager.instance.endTileRefresh(noGameTick: true);
			if ((DateTime.UtcNow - lastNoesisGUICheck).TotalMilliseconds > 100.0)
			{
				FatControler.instance.NoesisGUIUpdateChecksInGame();
				if (GameData.Instance.lastGameState != null)
				{
					OnScreenText.Instance.updateOST(GameData.Instance.lastGameState, allowExpire: false);
				}
				lastNoesisGUICheck = DateTime.UtcNow;
			}
		}
		FatControler.instance.NoesisGUIUpdateChecksComplete();
	}

	public void monitorCursors()
	{
		if (FatControler.currentScene != Enums.SceneIDS.ActualMainGame)
		{
			setCursor(0);
		}
		else if (MainControls.instance != null && (MainControls.instance.CurrentAction == 6 || (MainControls.instance.CurrentAction == 3 && MainControls.instance.CurrentSubAction == 349)))
		{
			setCursor(2);
		}
		else if (MainViewModel.Instance.IsMapEditorMode)
		{
			setCursor(1);
		}
		else
		{
			setCursor(0);
		}
	}

	public void resetCursor()
	{
		setCursor(currentCursorType, force: true);
	}

	public void setCursor(int cursorType, bool force = false)
	{
		if (!(cursorType != currentCursorType || force))
		{
			return;
		}
		if (ConfigSettings.Settings_CursorStyle == 0)
		{
			currentCursorType = cursorType;
			switch (cursorType)
			{
			case 0:
				UnityEngine.Cursor.SetCursor(swordCursor, new Vector2(2f, 1f), CursorMode.Auto);
				break;
			case 1:
				UnityEngine.Cursor.SetCursor(handCursor, new Vector2(1f, 1f), CursorMode.Auto);
				break;
			case 2:
				UnityEngine.Cursor.SetCursor(deleteCursor, new Vector2(10f, 10f), CursorMode.Auto);
				break;
			case 3:
				UnityEngine.Cursor.SetCursor(deleteNotCursor, new Vector2(10f, 10f), CursorMode.Auto);
				break;
			case 4:
				UnityEngine.Cursor.SetCursor(swordCursor, new Vector2(16f, 16f), CursorMode.Auto);
				break;
			}
		}
		else if (ConfigSettings.Settings_CursorStyle == 2)
		{
			currentCursorType = cursorType;
			switch (cursorType)
			{
			case 0:
				UnityEngine.Cursor.SetCursor(swordCursorX, new Vector2(2f, 1f), CursorMode.ForceSoftware);
				break;
			case 1:
				UnityEngine.Cursor.SetCursor(handCursorX, new Vector2(1f, 1f), CursorMode.ForceSoftware);
				break;
			case 2:
				UnityEngine.Cursor.SetCursor(deleteCursorX, new Vector2(10f, 10f), CursorMode.ForceSoftware);
				break;
			case 3:
				UnityEngine.Cursor.SetCursor(deleteNotCursorX, new Vector2(10f, 10f), CursorMode.ForceSoftware);
				break;
			case 4:
				UnityEngine.Cursor.SetCursor(swordCursorX, new Vector2(16f, 16f), CursorMode.ForceSoftware);
				break;
			}
		}
		else if (ConfigSettings.Settings_CursorStyle == 1)
		{
			UnityEngine.Cursor.SetCursor(null, Vector2.zero, cursorMode);
		}
	}

	public void StartMultiplayerGame()
	{
		multiplayerGame = true;
	}

	public void EndMultiplayer()
	{
		multiplayerGame = false;
	}

	public void UpdateMultiplayer(bool fromUpdate)
	{
		if ((!fromUpdate || !SimRunning) && (fromUpdate || SimRunning) && multiplayerGame)
		{
			Platform_Multiplayer.Instance.ReceiveGameMessages();
			if (Platform_Multiplayer.Instance.IsHost)
			{
				Platform_Multiplayer.Instance.monitorHostGameStart();
			}
			Platform_Multiplayer.Instance.monitorForLostPlayers();
		}
	}

	public void initGameOver(int state, int screen)
	{
		StartCoroutine(ManageGameOver(state, screen));
	}

	private IEnumerator ManageGameOver(int state, int screen)
	{
		yield return new WaitForSeconds(3f);
		if (SFXManager.instance.isBinkPlaying())
		{
			bool finished = false;
			for (int i = 0; i < 15; i++)
			{
				if (SFXManager.instance.isBinkPlaying())
				{
					yield return new WaitForSeconds(1f);
					continue;
				}
				finished = true;
				break;
			}
			if (!finished)
			{
				MainViewModel.Instance.HUDRoot.RadarME_Ended();
				for (int i = 0; i < 15; i++)
				{
					if (MyAudioManager.Instance.isSpeechPlaying(1))
					{
						yield return new WaitForSeconds(1f);
						continue;
					}
					finished = true;
					break;
				}
				if (!finished)
				{
					MyAudioManager.Instance.StopAllGameSounds(leaveMusicPlaying: true);
				}
			}
			yield return new WaitForSeconds(1f);
		}
		GameData.scenario.ManageGameOver(state, screen);
	}

	public void ReGetUIEdge()
	{
		StartCoroutine(doReGetUIEdge());
	}

	private IEnumerator doReGetUIEdge()
	{
		yield return new WaitForSeconds(0.5f);
		FatControler.instance.SHLowerUIPoint = 0f;
		MainViewModel.Instance.IngameUI.findUIlowerPoint();
	}

	public void DelayCentreKeep()
	{
		StartCoroutine(doDelayCentreKeep());
	}

	private IEnumerator doDelayCentreKeep()
	{
		yield return 0;
		yield return 0;
		yield return 0;
		EngineInterface.GameAction(Enums.KeyFunctions.HomeKeep);
	}

	public void DelayShowDisconnect()
	{
		StartCoroutine(doDelayShowDisconnect());
	}

	private IEnumerator doDelayShowDisconnect()
	{
		yield return new WaitForSeconds(9f);
		MainViewModel.Instance.Show_MP_LoadingWarning = true;
		yield return new WaitForSeconds(2f);
		MainViewModel.Instance.Show_MP_LoadingButton = true;
	}

	public void DelayHideConnectionScreen()
	{
		StartCoroutine(doDelayHideConnectionScreen());
	}

	private IEnumerator doDelayHideConnectionScreen()
	{
		yield return new WaitForSeconds(1f);
		MainViewModel.Instance.Show_MP_LoadingBlack = false;
	}

	public void EndAnimStoryAdvanceDelayed()
	{
		StartCoroutine(DoEndAnimStoryAdvanceDelayed());
	}

	private IEnumerator DoEndAnimStoryAdvanceDelayed()
	{
		yield return new WaitForSeconds(0.5f);
		MainViewModel.Instance.EndAnimStoryAdvance("Narrative");
	}

	public void ExitAppFromMP()
	{
		StartCoroutine(DoExitAppFromMP());
	}

	private IEnumerator DoExitAppFromMP()
	{
		Platform_Multiplayer.Instance.LeaveGame();
		yield return new WaitForSeconds(1f);
		Platform_Multiplayer.Instance.exitMP();
		yield return new WaitForSeconds(1f);
		FatControler.instance.ExitApp();
	}

	public void LoadImages()
	{
		StartCoroutine(DoLoadImages());
	}

	private IEnumerator DoLoadImages()
	{
		bool halfSize = false;
		if (Screen.width < 2000 && Screen.height < 1201)
		{
			halfSize = true;
		}
		for (int imageID = 0; imageID < 51; imageID++)
		{
			if (MainViewModel.imageFileNames[imageID].Length > 0 && MainViewModel.GameImages[imageID] == null)
			{
				while (MainViewModel.GameImageData[imageID] == null)
				{
					yield return null;
				}
				Texture2D texture2D = new Texture2D(2, 2, UnityEngine.TextureFormat.RGBA32, mipChain: false, linear: true);
				texture2D.LoadImage(MainViewModel.GameImageData[imageID]);
				if (halfSize)
				{
					GPUTextureScaler.Scale(texture2D, texture2D.width / 2, texture2D.height / 2);
				}
				if (MainViewModel.imageProcessingNeeded[imageID])
				{
					UnityEngine.Color[] pixels = texture2D.GetPixels();
					for (int i = 0; i < pixels.Length; i++)
					{
						float a = pixels[i].a;
						if (a != 0f)
						{
							if (a != 1f)
							{
								pixels[i].r *= pixels[i].a;
								pixels[i].g *= pixels[i].a;
								pixels[i].b *= pixels[i].a;
							}
						}
						else
						{
							pixels[i].r = (pixels[i].g = (pixels[i].b = 0f));
						}
					}
					texture2D.SetPixels(pixels);
					texture2D.Apply();
				}
				if (IsPowerOfTwo(texture2D.width) && IsPowerOfTwo(texture2D.height))
				{
					texture2D.Compress(highQuality: true);
				}
				TextureSource image = new TextureSource(texture2D);
				UnityEngine.Object.DestroyImmediate(texture2D);
				MainViewModel.GameImages[imageID] = new AnImage
				{
					_image = image
				};
				MainViewModel.GameImageData[imageID] = null;
			}
			if (imageID == 29 || imageID == 23 || imageID == 17)
			{
				yield return null;
			}
		}
		GC.Collect();
		yield return null;
	}

	private bool IsPowerOfTwo(int x)
	{
		return (x & (x - 1)) == 0;
	}
}
