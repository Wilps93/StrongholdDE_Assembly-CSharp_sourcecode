using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Steamworks;
using Stronghold1DE;
using UnityEngine;

public class Platform_Multiplayer
{
	public class MPData
	{
		public short packetType;

		public int dataLength = -1;

		public byte[] data;

		public int dataOffset;

		public byte[] ToBytes()
		{
			int num = dataLength;
			if (dataLength < 0)
			{
				num = data.Length;
			}
			byte[] array = new byte[6 + num];
			if (num > 0)
			{
				Array.Copy(data, dataOffset, array, 6, num);
			}
			byte[] bytes = BitConverter.GetBytes(packetType);
			byte[] bytes2 = BitConverter.GetBytes(num);
			array[0] = bytes[0];
			array[1] = bytes[1];
			array[2] = bytes2[0];
			array[3] = bytes2[1];
			array[4] = bytes2[2];
			array[5] = bytes2[3];
			return array;
		}

		public static MPData FromBytes(byte[] source)
		{
			MPData mPData = new MPData();
			mPData.packetType = BitConverter.ToInt16(source, 0);
			mPData.dataLength = BitConverter.ToInt32(source, 2);
			mPData.data = new byte[mPData.dataLength];
			if (mPData.dataLength > 0)
			{
				Array.Copy(source, 6, mPData.data, 0, mPData.dataLength);
			}
			return mPData;
		}
	}

	public class MPLobby
	{
		public CSteamID id;

		public int numLobbyMembers;

		public string gameName;

		public string mapName;

		public string mapFileName;

		public string maxPlayers;

		public string gameType;

		public string settings;

		public bool isHost;

		public string crc;

		public string startGame;

		private Dictionary<ulong, int> teams = new Dictionary<ulong, int>();

		public List<ulong> hostMemberOrder = new List<ulong>();

		public List<MPLobbyMember> members = new List<MPLobbyMember>();

		public int iMaxPlayers => EditorDirector.getIntFromString(maxPlayers);

		public ulong identifier => (ulong)id;

		public string setTeams
		{
			get
			{
				bool flag = true;
				string text = "";
				foreach (KeyValuePair<ulong, int> team in teams)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						text += ",";
					}
					text = text + team.Key + "," + team.Value;
				}
				return text;
			}
			set
			{
				if (value.Length <= 0)
				{
					return;
				}
				string[] array = value.Split(",");
				teams.Clear();
				List<ulong> list = new List<ulong>();
				if (array.Length >= 2)
				{
					for (int i = 0; i < array.Length; i += 2)
					{
						ulong num = EditorDirector.getuLongFromString(array[i]);
						teams[num] = EditorDirector.getIntFromString(array[i + 1]);
						list.Add(num);
					}
				}
				if (isHost || list.Count != members.Count)
				{
					return;
				}
				bool flag = true;
				for (int j = 0; j < members.Count; j++)
				{
					if (list[j] != members[j].id.m_SteamID)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return;
				}
				List<MPLobbyMember> list2 = new List<MPLobbyMember>();
				for (int k = 0; k < list.Count; k++)
				{
					ulong num2 = list[k];
					foreach (MPLobbyMember member in members)
					{
						if (num2 == member.id.m_SteamID)
						{
							list2.Add(member);
							break;
						}
					}
				}
				members.Clear();
				foreach (MPLobbyMember item in list2)
				{
					members.Add(item);
				}
			}
		}

		public int getTeam(MPLobbyMember member)
		{
			if (teams.TryGetValue(member.id.m_SteamID, out var value))
			{
				return value;
			}
			return -1;
		}

		public void setTeam(MPLobbyMember member, int newTeam)
		{
			teams[member.id.m_SteamID] = newTeam;
		}

		public int getFreeTeam()
		{
			Dictionary<int, ulong> dictionary = new Dictionary<int, ulong>();
			foreach (KeyValuePair<ulong, int> team in teams)
			{
				dictionary[team.Value] = team.Key;
			}
			for (int i = 1; i <= 8; i++)
			{
				if (!dictionary.ContainsKey(i))
				{
					return i;
				}
			}
			return 1;
		}

		public void validateTeams()
		{
			Dictionary<ulong, int> dictionary = new Dictionary<ulong, int>();
			bool flag = false;
			foreach (MPLobbyMember member in members)
			{
				if (teams.ContainsKey(member.id.m_SteamID))
				{
					dictionary[member.id.m_SteamID] = teams[member.id.m_SteamID];
				}
				else
				{
					flag = true;
				}
			}
			if (!flag && dictionary.Count != teams.Count)
			{
				flag = true;
			}
			if (flag)
			{
				teams = dictionary;
			}
		}

		public bool getEnoughTeams()
		{
			int num = -1;
			foreach (MPLobbyMember member in members)
			{
				if (teams.TryGetValue(member.id.m_SteamID, out var value))
				{
					if (num < 0)
					{
						num = value;
					}
					else if (num != value)
					{
						return true;
					}
				}
			}
			return false;
		}

		public string getHostMemberOrder()
		{
			bool flag = true;
			string text = "";
			foreach (MPLobbyMember member in members)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					text += ",";
				}
				text = text + member.id.m_SteamID + "," + member.colourID;
			}
			return text;
		}

		public void setHostMemberOrder(string value)
		{
			string[] array = value.Split(",");
			hostMemberOrder.Clear();
			for (int i = 0; i < array.Length; i += 2)
			{
				ulong num = EditorDirector.getuLongFromString(array[i]);
				hostMemberOrder.Add(num);
				int intFromString = EditorDirector.getIntFromString(array[i + 1]);
				foreach (MPLobbyMember member in members)
				{
					if (member.id.m_SteamID == num)
					{
						member.colourID = intFromString;
						break;
					}
				}
			}
		}
	}

	public class MPLobbyMember
	{
		public CSteamID id;

		public string name;

		public bool ready;

		public int mapStatus;

		public string mapRequested = "";

		public int colourID = -1;

		public DateTime pingSent = DateTime.MinValue;

		public int lastPingDuration = -1;

		public Dictionary<string, bool> mapsSent = new Dictionary<string, bool>();

		public bool IsSelf()
		{
			return id == SteamUser.GetSteamID();
		}
	}

	private class MapSendQueueItem
	{
		public byte[] data;

		public SteamNetworkingIdentity NetID;

		public DateTime sendTime;
	}

	public class MPGameMember
	{
		public MPLobbyMember lobbyData;

		public SteamNetworkingIdentity SNI;

		public ulong steamID;

		public int playerID;

		public bool isHost;

		public bool isSelf;

		public bool acknowledged;

		public string playerName;

		public bool muted;

		public int colourID;

		public DateTime lastTimePacketRecieved = DateTime.MaxValue;

		public int errorCount;

		public bool kicked;

		public bool _pendingKick;

		public DateTime pendingKickTime = DateTime.MinValue;

		public int kickCounter;

		public DateTime[] kickVoteTime = new DateTime[9];

		public bool pendingKick
		{
			get
			{
				if (_pendingKick && pendingKickTime > DateTime.UtcNow.AddSeconds(-45.0))
				{
					return true;
				}
				_pendingKick = false;
				return false;
			}
			set
			{
				_pendingKick = value;
				pendingKickTime = DateTime.UtcNow;
			}
		}

		public MPGameMember()
		{
			for (int i = 0; i < 9; i++)
			{
				kickVoteTime[i] = DateTime.MinValue;
			}
		}

		public bool DoVoteKick(int voterID, int otherActivePlayers)
		{
			DateTime utcNow = DateTime.UtcNow;
			DateTime dateTime = utcNow.AddMinutes(-2.0);
			kickVoteTime[voterID] = utcNow;
			int num = 0;
			for (int i = 0; i < 9; i++)
			{
				if (kickVoteTime[i] > dateTime)
				{
					num++;
				}
			}
			if (num > (otherActivePlayers + 1) / 2)
			{
				return true;
			}
			return false;
		}
	}

	private static readonly Platform_Multiplayer instance;

	public bool PendingMPLobby;

	public bool PendingMPLobby_DelayedMPEnter;

	public int lastLobbyMode;

	public string ShareCodeString = "ABCD";

	public bool IsHost;

	private bool seedReceived;

	private int randSeedReceived;

	private bool mapLoaded;

	public bool resyncingOrSaving;

	private DateTime resyncingOrSavingResumeTime = DateTime.MinValue;

	private bool monitoringForGameStart;

	private DateTime monitoringForGameStartTime = DateTime.MinValue;

	private int localPlayerID;

	private EngineInterface.LoadMapReturnData lastRetData;

	private bool newGameNotLoad = true;

	public List<MPGameMember> gameMembers;

	private int[] loadPlayerRemapping = new int[9];

	private DateTime connectionCheckTime = DateTime.MinValue;

	private DateTime connectionPauseStartTime = DateTime.MinValue;

	private bool connectionPauseEngineState;

	private bool connectionPauseReasonLostConnection = true;

	public const bool HAS_MULTIPLAYER = true;

	public const string MP_VERSION = "7";

	private uint numLobbies;

	private bool lobbyListValid;

	private List<MPLobby> lobbies = new List<MPLobby>();

	private Action LobbyListPopulatedDelegate;

	private Action LobbyCreatedDelegate;

	private Action LobbyJoinedDelegate;

	private Action<string, string, int> LobbyChatDelegate;

	private DateTime lastLobbyDataRefresh = DateTime.MinValue;

	private DateTime kickMemberTime = DateTime.MinValue;

	private MPLobby activeLobby;

	private ulong inviteLobbyID;

	private Callback<GameLobbyJoinRequested_t> m_JoinLobbyRequested;

	protected Callback<GameRichPresenceJoinRequested_t> m_GameRichPresenceJoinRequested;

	private MPLobby autoJoinLobby;

	private CSteamID lobbyJoiningID;

	protected Callback<LobbyChatMsg_t> IncomingMessage;

	protected Callback<SteamNetworkingMessagesSessionRequest_t> NetworkUserListener;

	private Queue<MapSendQueueItem> mapSendQueue = new Queue<MapSendQueueItem>();

	private DateTime lastMapSendQueueItemTime = DateTime.MinValue;

	private int lastCrc;

	private int incomingSize;

	private int incomingOfset;

	private string lastReceivedFileName = "";

	private Action MapReceivedDelegate;

	private int receivingMode;

	private byte[] receiveBuffer;

	public static Platform_Multiplayer Instance => instance;

	public static bool Initialised => SteamManager.Initialized;

	public static bool MPGameActive { get; set; }

	static Platform_Multiplayer()
	{
		instance = new Platform_Multiplayer();
	}

	private Platform_Multiplayer()
	{
	}

	private void initCommon()
	{
		connectionPauseEngineState = false;
	}

	public void SendSaveCRCs()
	{
		foreach (FileHeader mPSafe in MapFileManager.Instance.GetMPSaves(sortByName: true, sortAscend: true))
		{
			mPSafe.retrieveCRCChecks = 0;
			SendSaveCRC(mPSafe.fileName, mPSafe.xPlaySaveChecksum ^ mPSafe.xPlaySaveTime);
		}
	}

	public void SendGamePacketToAll(byte[] gameData, int len, int offset = 0)
	{
		MPData mPData = new MPData();
		mPData.data = gameData;
		mPData.dataLength = len;
		mPData.dataOffset = offset;
		mPData.packetType = 1;
		SendPacketToAll(mPData);
	}

	public void SendEmptyPacketTypeToAll(Enums.MPFlags packetType)
	{
		MPData mPData = new MPData();
		mPData.data = new byte[0];
		mPData.dataLength = 0;
		mPData.packetType = (short)packetType;
		SendPacketToAll(mPData);
	}

	public void SendPacketToAll(MPData data, bool instantMessage = false)
	{
		byte[] dataToSend = data.ToBytes();
		foreach (MPGameMember gameMember in gameMembers)
		{
			if (!gameMember.isSelf)
			{
				SendGameData(gameMember, dataToSend, instantMessage);
			}
		}
	}

	public void SendGamePacketToPlayerID(int playerID, byte[] gameData, int len, int offset = 0)
	{
		MPData mPData = new MPData();
		mPData.data = gameData;
		mPData.dataLength = len;
		mPData.dataOffset = offset;
		mPData.packetType = 1;
		SendPacketToPlayerID(playerID, mPData);
	}

	public void SendPacketToPlayerID(int playerID, MPData data)
	{
		byte[] dataToSend = data.ToBytes();
		foreach (MPGameMember gameMember in gameMembers)
		{
			if (!gameMember.isSelf && gameMember.playerID == playerID)
			{
				SendGameData(gameMember, dataToSend);
			}
		}
	}

	public void SendPacketToPlayers(List<int> recipients, MPData data)
	{
		byte[] dataToSend = data.ToBytes();
		foreach (MPGameMember gameMember in gameMembers)
		{
			if (!gameMember.isSelf && recipients.Contains(gameMember.playerID))
			{
				SendGameData(gameMember, dataToSend);
			}
		}
	}

	private bool remapMPGameMembers()
	{
		MPGameMember[] array = new MPGameMember[9];
		foreach (MPGameMember gameMember in gameMembers)
		{
			array[gameMember.playerID] = gameMember;
		}
		int num = localPlayerID;
		for (int i = 1; i < 9; i++)
		{
			if (i != loadPlayerRemapping[i])
			{
				if (i == num)
				{
					GameData.Instance.playerID = (localPlayerID = loadPlayerRemapping[i]);
				}
				MPGameMember mPGameMember = array[i];
				if (mPGameMember != null)
				{
					mPGameMember.playerID = loadPlayerRemapping[i];
				}
			}
		}
		if (localPlayerID < 0 || localPlayerID > 8)
		{
			return false;
		}
		EngineInterface.RemapPlayers(loadPlayerRemapping, localPlayerID);
		SpriteMapping.BuildMultiPlayerColourMapping(loadPlayerRemapping);
		EditorDirector.instance.SetLocalPlayer(localPlayerID);
		return true;
	}

	public void monitorHostGameStart()
	{
		if (!monitoringForGameStart)
		{
			return;
		}
		if ((DateTime.UtcNow - monitoringForGameStartTime).TotalSeconds > 60.0)
		{
			Debug.Log("Game initialization failed");
			monitoringForGameStart = false;
			LeaveLobby();
			return;
		}
		foreach (MPGameMember gameMember in gameMembers)
		{
			if (!gameMember.acknowledged && !gameMember.isHost)
			{
				return;
			}
		}
		monitoringForGameStart = false;
		LeaveLobby();
		if (!newGameNotLoad)
		{
			MPData mPData = new MPData();
			mPData.packetType = 4;
			mPData.dataLength = 36;
			mPData.data = new byte[mPData.dataLength * 4];
			for (int i = 0; i < 9; i++)
			{
				byte[] bytes = BitConverter.GetBytes(loadPlayerRemapping[i]);
				for (int j = 0; j < 4; j++)
				{
					mPData.data[i * 4 + j] = bytes[j];
				}
			}
			if (!remapMPGameMembers())
			{
				FatControler.instance.NewScene(Enums.SceneIDS.FrontEnd);
				return;
			}
			SendPacketToAll(mPData);
		}
		else
		{
			SendEmptyPacketTypeToAll(Enums.MPFlags.StartGamePacket);
		}
		Director.instance.startSimThread();
		EngineInterface.GameAction(Enums.KeyFunctions.HomeKeep);
		if (ConfigSettings.Settings_ShowPings)
		{
			OnScreenText.Instance.addOSTEntry(Enums.eOnScreenText.OST_PINGS, 1);
		}
		EngineInterface.StartMultiplayerGameSynced();
		Director.instance.DelayHideConnectionScreen();
		MPGameActive = true;
	}

	public void PauseEngine(bool state)
	{
		if (connectionPauseEngineState != state)
		{
			connectionPauseEngineState = state;
			EngineInterface.ConnectionPauseEngine(state);
		}
	}

	public void monitorForLostPlayers()
	{
		if (GameData.Instance.lastGameState != null)
		{
			for (int i = 0; i < 8; i++)
			{
				if (GameData.Instance.lastGameState.mpkick[i] <= 0)
				{
					continue;
				}
				int num = i + 1;
				foreach (MPGameMember gameMember in gameMembers)
				{
					if (gameMember.playerID == num && !gameMember.kicked)
					{
						kickPlayerFromGame(gameMember);
					}
				}
			}
		}
		if (connectionPauseEngineState)
		{
			if (connectionPauseReasonLostConnection)
			{
				if (!(DateTime.UtcNow > connectionCheckTime))
				{
					return;
				}
				connectionCheckTime = DateTime.UtcNow.AddSeconds(1.0);
				if (MonitorNetworkConnectivity())
				{
					PauseEngine(state: false);
					resyncingOrSavingResumeTime = DateTime.UtcNow.AddSeconds(5.0);
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 45), 0, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 47));
					MainViewModel.Instance.HUDMPConnectionIssue.ShowMultiplayerConnectionError("", kickNotLeave: false, -1);
				}
				else
				{
					if (!((DateTime.UtcNow - connectionPauseStartTime).TotalSeconds > 15.0))
					{
						return;
					}
					foreach (MPGameMember gameMember2 in gameMembers)
					{
						if (gameMember2.playerID != localPlayerID && !gameMember2.kicked)
						{
							gameMember2.kicked = true;
							EngineInterface.KickMPPlayer(gameMember2.playerID, kickImmediate: true);
						}
					}
					PauseEngine(state: false);
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 45), 0, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 48));
					MainViewModel.Instance.HUDMPConnectionIssue.ShowMultiplayerConnectionError("", kickNotLeave: false, -1);
				}
				return;
			}
			if (DateTime.UtcNow > connectionCheckTime)
			{
				connectionCheckTime = DateTime.UtcNow.AddSeconds(2.0);
				if (!MonitorNetworkConnectivity())
				{
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 45), 0, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 44));
					connectionPauseReasonLostConnection = true;
					MainViewModel.Instance.HUDMPConnectionIssue.ShowMultiplayerConnectionError(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 44), kickNotLeave: false, -1);
					return;
				}
			}
			DateTime dateTime = DateTime.UtcNow.AddSeconds(-5.0);
			int num2 = 0;
			foreach (MPGameMember gameMember3 in gameMembers)
			{
				if (!gameMember3.isSelf && !gameMember3.kicked && gameMember3.lastTimePacketRecieved != DateTime.MaxValue && gameMember3.lastTimePacketRecieved < dateTime)
				{
					num2++;
				}
			}
			if (num2 == 0)
			{
				PauseEngine(state: false);
				resyncingOrSavingResumeTime = DateTime.UtcNow.AddSeconds(5.0);
				MainViewModel.Instance.HUDMPConnectionIssue.ShowMultiplayerConnectionError("", kickNotLeave: false, -1);
			}
			else
			{
				if (!((DateTime.UtcNow - connectionPauseStartTime).TotalSeconds > 15.0))
				{
					return;
				}
				foreach (MPGameMember gameMember4 in gameMembers)
				{
					if (!gameMember4.isSelf && !gameMember4.kicked && gameMember4.lastTimePacketRecieved != DateTime.MaxValue && gameMember4.lastTimePacketRecieved < dateTime)
					{
						MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 45), 0, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 49) + " : " + gameMember4.playerName);
						kickPlayerFromGame(gameMember4);
					}
				}
				PauseEngine(state: false);
				resyncingOrSavingResumeTime = DateTime.UtcNow.AddSeconds(5.0);
				MainViewModel.Instance.HUDMPConnectionIssue.ShowMultiplayerConnectionError("", kickNotLeave: false, -1);
			}
		}
		else
		{
			if (gameMembers == null || resyncingOrSaving || (!(resyncingOrSavingResumeTime == DateTime.MinValue) && !(DateTime.UtcNow > resyncingOrSavingResumeTime)))
			{
				return;
			}
			if (DateTime.UtcNow > connectionCheckTime)
			{
				connectionCheckTime = DateTime.UtcNow.AddSeconds(2.0);
				if (!MonitorNetworkConnectivity())
				{
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 45), 0, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 44));
					connectionPauseReasonLostConnection = true;
					connectionPauseStartTime = DateTime.UtcNow;
					PauseEngine(state: true);
					MainViewModel.Instance.HUDMPConnectionIssue.ShowMultiplayerConnectionError(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 44), kickNotLeave: false, -1);
				}
			}
			resyncingOrSavingResumeTime = DateTime.MinValue;
			DateTime dateTime2 = DateTime.UtcNow.AddSeconds(-5.0);
			foreach (MPGameMember gameMember5 in gameMembers)
			{
				if (!gameMember5.isSelf && !gameMember5.kicked && gameMember5.lastTimePacketRecieved != DateTime.MaxValue && gameMember5.lastTimePacketRecieved < dateTime2)
				{
					connectionPauseReasonLostConnection = false;
					connectionPauseStartTime = DateTime.UtcNow;
					PauseEngine(state: true);
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 45), 0, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 46) + " : " + gameMember5.playerName);
					MainViewModel.Instance.HUDMPConnectionIssue.ShowMultiplayerConnectionError(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 46) + " : " + gameMember5.playerName, kickNotLeave: true, gameMember5.playerID);
				}
			}
		}
	}

	public void kickPlayerFromGame(int playerID)
	{
		MPGameMember player = getPlayer(playerID);
		if (player != null)
		{
			kickPlayerFromGame(player);
		}
	}

	private void kickPlayerFromGame(MPGameMember kickMember)
	{
		if (kickMember.pendingKick)
		{
			return;
		}
		kickMember.pendingKick = true;
		int num = countOtherPlayers(kickMember);
		if (num == 0)
		{
			kickMember.kicked = true;
			EngineInterface.KickMPPlayer(kickMember.playerID, kickImmediate: true);
			MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(kickMember.playerName, kickMember.playerID, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MULTIPLAYER_CONNECTION, 21));
			return;
		}
		byte[] dataToSend = new MPData
		{
			packetType = 7,
			dataLength = 4,
			data = BitConverter.GetBytes(kickMember.playerID)
		}.ToBytes();
		foreach (MPGameMember gameMember in gameMembers)
		{
			if (!gameMember.isSelf)
			{
				SendGameData(gameMember, dataToSend);
			}
		}
		if (kickMember.DoVoteKick(localPlayerID, num))
		{
			if (kickMember.isHost)
			{
				promoteNewHost(kickMember);
			}
			kickMember.kickCounter++;
			kickMember.kicked = true;
			EngineInterface.KickMPPlayer(kickMember.playerID, kickImmediate: false);
			MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(kickMember.playerName, kickMember.playerID, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MULTIPLAYER_CONNECTION, 21));
			if (kickMember.playerID == localPlayerID)
			{
				EditorDirector.instance.stopGameSim();
				MainViewModel.Instance.InitNewScene(Enums.SceneIDS.FrontEnd);
			}
		}
	}

	private void promoteNewHost(MPGameMember kickMember)
	{
		if (!kickMember.isHost)
		{
			return;
		}
		foreach (MPGameMember gameMember in gameMembers)
		{
			if (gameMember.playerID != kickMember.playerID && !gameMember.kicked && !gameMember.pendingKick)
			{
				gameMember.isHost = true;
				EngineInterface.PromoteMPHost(gameMember.playerID);
				if (gameMember.isSelf)
				{
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(gameMember.playerName, gameMember.playerID, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MULTIPLAYER_CONNECTION, 60));
				}
				else
				{
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(gameMember.playerName, gameMember.playerID, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MULTIPLAYER_CONNECTION, 22));
				}
				break;
			}
		}
	}

	private int countOtherPlayers(MPGameMember otherMember)
	{
		int num = 0;
		if (gameMembers != null)
		{
			foreach (MPGameMember gameMember in gameMembers)
			{
				if (!gameMember.isSelf && gameMember.playerID != otherMember.playerID && !gameMember.kicked)
				{
					num++;
				}
			}
		}
		return num;
	}

	public string getPlayerName(int playerID, bool activeOnly = false)
	{
		if (gameMembers != null)
		{
			foreach (MPGameMember gameMember in gameMembers)
			{
				if (gameMember.playerID == playerID)
				{
					if (!activeOnly || !gameMember.kicked)
					{
						return gameMember.playerName;
					}
					break;
				}
			}
		}
		return "";
	}

	public MPGameMember getPlayer(int playerID)
	{
		if (gameMembers != null)
		{
			foreach (MPGameMember gameMember in gameMembers)
			{
				if (gameMember.playerID == playerID)
				{
					return gameMember;
				}
			}
		}
		return null;
	}

	public int getPlayerColour(int playerID)
	{
		return getPlayer(playerID)?.colourID ?? 1;
	}

	public int GetNumActivePlayers()
	{
		int num = 0;
		if (gameMembers != null)
		{
			foreach (MPGameMember gameMember in gameMembers)
			{
				if (!gameMember.kicked)
				{
					num++;
				}
			}
		}
		return num;
	}

	public void LeaveGame()
	{
		MPData mPData = new MPData();
		mPData.packetType = 8;
		mPData.dataLength = 0;
		SendPacketToAll(mPData, instantMessage: true);
	}

	public void SendChores(byte[] choreBuffer)
	{
		if (!MPGameActive)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		while (true)
		{
			num2++;
			if (num2 > 10000)
			{
				break;
			}
			int num3 = BitConverter.ToInt32(choreBuffer, num);
			if (num3 < 0)
			{
				break;
			}
			bool flag = true;
			switch (choreBuffer[num + 5])
			{
			case 54:
				resyncingOrSaving = true;
				MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 45), 0, "Resyncing");
				break;
			case 67:
				resyncingOrSaving = false;
				resyncingOrSavingResumeTime = DateTime.UtcNow.AddSeconds(5.0);
				break;
			case 39:
				resyncingOrSaving = true;
				MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(getPlayerName(localPlayerID), localPlayerID, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 32));
				break;
			case 94:
				resyncingOrSaving = false;
				resyncingOrSavingResumeTime = DateTime.UtcNow.AddSeconds(5.0);
				break;
			}
			if (flag)
			{
				int num4 = choreBuffer[num + 4];
				if (num4 == 0)
				{
					SendGamePacketToAll(choreBuffer, num3, num + 4 + 1);
				}
				else
				{
					SendGamePacketToPlayerID(num4, choreBuffer, num3, num + 4 + 1);
				}
			}
			num += num3 + 4 + 1;
		}
	}

	public void SendIngameChat(List<int> recipients, string message)
	{
		MPData mPData = new MPData();
		mPData.packetType = 5;
		mPData.data = Encoding.UTF8.GetBytes(message);
		mPData.dataLength = -1;
		SendPacketToPlayers(recipients, mPData);
	}

	public void SendIngameChatInsult(List<int> recipients, int insult)
	{
		MPData mPData = new MPData();
		mPData.packetType = 6;
		mPData.dataLength = 4;
		mPData.data = BitConverter.GetBytes(insult);
		SendPacketToPlayers(recipients, mPData);
	}

	public void SetChatMute(int playerID, bool muted)
	{
		MPGameMember player = getPlayer(playerID);
		if (player != null)
		{
			player.muted = muted;
		}
	}

	public void Initialise()
	{
		SteamNetworkingUtils.InitRelayNetworkAccess();
	}

	private void init()
	{
		initCommon();
		MPGameActive = false;
		mapLoaded = false;
		seedReceived = false;
		autoJoinLobby = null;
		mapSendQueue.Clear();
		if (gameMembers != null)
		{
			foreach (MPGameMember gameMember in gameMembers)
			{
				SteamNetworkingMessages.CloseSessionWithUser(ref gameMember.SNI);
			}
		}
		gameMembers = null;
		IsHost = false;
		monitoringForGameStart = false;
		resyncingOrSaving = false;
		resyncingOrSavingResumeTime = DateTime.MinValue;
	}

	public void exitMP()
	{
		EndDataConnection();
		LeaveChat();
		numLobbies = 0u;
		if (lobbies != null)
		{
			lobbies.Clear();
		}
		lobbyListValid = false;
		activeLobby = null;
		IsHost = false;
		seedReceived = false;
		randSeedReceived = 0;
		mapLoaded = false;
		resyncingOrSaving = false;
		resyncingOrSavingResumeTime = DateTime.MinValue;
		monitoringForGameStart = false;
		localPlayerID = 0;
		init();
	}

	public void GetLobbies(Action lobbyListCompletedelegate)
	{
		numLobbies = 0u;
		lobbyListValid = false;
		LobbyListPopulatedDelegate = lobbyListCompletedelegate;
		lastLobbyDataRefresh = DateTime.UtcNow;
		SteamMatchmaking.AddRequestLobbyListStringFilter("version", "7", ELobbyComparison.k_ELobbyComparisonEqual);
		SteamMatchmaking.AddRequestLobbyListDistanceFilter(ELobbyDistanceFilter.k_ELobbyDistanceFilterDefault);
		SteamMatchmaking.AddRequestLobbyListResultCountFilter(500);
		SteamAPICall_t hAPICall = SteamMatchmaking.RequestLobbyList();
		CallResult<LobbyMatchList_t>.Create().Set(hAPICall, RequestLobbyListResult);
	}

	private void RequestLobbyListResult(LobbyMatchList_t param, bool bIOFailure)
	{
		numLobbies = param.m_nLobbiesMatching;
		List<MPLobby> list = new List<MPLobby>();
		for (int i = 0; i < numLobbies; i++)
		{
			CSteamID lobbyByIndex = SteamMatchmaking.GetLobbyByIndex(i);
			MPLobby item = new MPLobby
			{
				id = lobbyByIndex,
				numLobbyMembers = SteamMatchmaking.GetNumLobbyMembers(lobbyByIndex),
				gameName = SteamMatchmaking.GetLobbyData(lobbyByIndex, "name"),
				mapName = SteamMatchmaking.GetLobbyData(lobbyByIndex, "map"),
				mapFileName = SteamMatchmaking.GetLobbyData(lobbyByIndex, "mapfile"),
				maxPlayers = SteamMatchmaking.GetLobbyData(lobbyByIndex, "max"),
				gameType = SteamMatchmaking.GetLobbyData(lobbyByIndex, "type"),
				settings = SteamMatchmaking.GetLobbyData(lobbyByIndex, "settings"),
				crc = SteamMatchmaking.GetLobbyData(lobbyByIndex, "crc"),
				setTeams = SteamMatchmaking.GetLobbyData(lobbyByIndex, "teams"),
				isHost = false
			};
			string lobbyData = SteamMatchmaking.GetLobbyData(lobbyByIndex, "time");
			bool flag = true;
			if (lobbyData.Length == 0)
			{
				flag = false;
			}
			else
			{
				try
				{
					long num = long.Parse(lobbyData);
					long ticks = DateTime.UtcNow.AddHours(-1.0).Ticks;
					if (num < ticks)
					{
						flag = false;
					}
				}
				catch (Exception)
				{
					flag = false;
				}
			}
			if (SteamMatchmaking.GetLobbyData(lobbyByIndex, "start").Length > 0)
			{
				flag = false;
			}
			if (SteamMatchmaking.GetLobbyData(lobbyByIndex, "closed") == "0" && flag)
			{
				list.Add(item);
			}
		}
		lobbies = list;
		lobbyListValid = true;
		if (LobbyListPopulatedDelegate != null)
		{
			LobbyListPopulatedDelegate();
		}
	}

	public List<MPLobby> ReadLobbies()
	{
		return lobbies;
	}

	private MPLobby FindLobby(CSteamID id)
	{
		foreach (MPLobby lobby in lobbies)
		{
			if (lobby.id == id)
			{
				return lobby;
			}
		}
		return null;
	}

	public void CreateLobby(string _gameName, string _mapName, string _mapFileName, int _maxPlayers, int _type, int _lobbyMode, string _settings, int _crc, Action lobbyCreatedDelegate, Action<string, string, int> lobbyChatDelegate)
	{
		init();
		LobbyCreatedDelegate = lobbyCreatedDelegate;
		LobbyChatDelegate = lobbyChatDelegate;
		activeLobby = new MPLobby
		{
			numLobbyMembers = 1,
			gameName = _gameName,
			mapName = _mapName,
			mapFileName = _mapFileName,
			maxPlayers = _maxPlayers.ToString(),
			gameType = _type.ToString(),
			settings = _settings,
			crc = _crc.ToString(),
			isHost = true
		};
		lastLobbyDataRefresh = DateTime.MinValue;
		lastLobbyMode = _lobbyMode;
		ELobbyType eLobbyType = ELobbyType.k_ELobbyTypePublic;
		switch (_lobbyMode)
		{
		case 2:
			eLobbyType = ELobbyType.k_ELobbyTypeFriendsOnly;
			break;
		case 4:
			eLobbyType = ELobbyType.k_ELobbyTypePrivate;
			break;
		}
		SteamAPICall_t hAPICall = SteamMatchmaking.CreateLobby(eLobbyType, _maxPlayers);
		CallResult<LobbyCreated_t>.Create().Set(hAPICall, CreateLobbyResult);
	}

	private void CreateLobbyResult(LobbyCreated_t param, bool bIOFailure)
	{
		if (param.m_eResult == EResult.k_EResultOK)
		{
			ulong ulSteamIDLobby = param.m_ulSteamIDLobby;
			CSteamID cSteamID = new CSteamID(ulSteamIDLobby);
			activeLobby.id = cSteamID;
			CreateShareCode(ulSteamIDLobby);
			SteamMatchmaking.SetLobbyData(cSteamID, "settings", activeLobby.settings);
			SteamMatchmaking.SetLobbyData(cSteamID, "name", activeLobby.gameName);
			SteamMatchmaking.SetLobbyData(cSteamID, "map", activeLobby.mapName);
			SteamMatchmaking.SetLobbyData(cSteamID, "mapFile", activeLobby.mapFileName);
			SteamMatchmaking.SetLobbyData(cSteamID, "max", activeLobby.maxPlayers);
			SteamMatchmaking.SetLobbyData(cSteamID, "type", activeLobby.gameType);
			SteamMatchmaking.SetLobbyData(cSteamID, "crc", activeLobby.crc);
			SteamMatchmaking.SetLobbyData(cSteamID, "teams", activeLobby.setTeams);
			SteamMatchmaking.SetLobbyData(cSteamID, "time", DateTime.UtcNow.Ticks.ToString());
			SteamMatchmaking.SetLobbyData(cSteamID, "version", "7");
			SteamMatchmaking.SetLobbyData(cSteamID, "closed", "0");
			SteamMatchmaking.SetLobbyData(cSteamID, "start", "");
			if (lastLobbyMode == 2)
			{
				SteamFriends.ActivateGameOverlayInviteDialog(cSteamID);
			}
			SetPlayerColour(ConfigSettings.Settings_PlayerColour + 1);
			GetActiveLobbyMembers();
			string hostMemberOrder = activeLobby.getHostMemberOrder();
			SteamMatchmaking.SetLobbyData(cSteamID, "hostorder", hostMemberOrder);
			InitChat();
			InitDataConnection();
			EngineInterface.sendPath(Application.streamingAssetsPath, ConfigSettings.GetMpAutoSavePath(), ConfigSettings.GetSavesPath());
			if (LobbyCreatedDelegate != null)
			{
				LobbyCreatedDelegate();
			}
		}
	}

	public void InviteOverlay()
	{
		if (activeLobby != null)
		{
			SteamFriends.ActivateGameOverlayInviteDialog(activeLobby.id);
		}
	}

	public void SetInviteLobbyID(ulong id)
	{
		inviteLobbyID = id;
	}

	public void HandleCommandline()
	{
		if (SteamManager.Initialized)
		{
			if (SteamApps.GetLaunchCommandLine(out var pszCommandLine, 260) > 0)
			{
				string[] array = pszCommandLine.Split(' ');
				if (pszCommandLine.Length == 0)
				{
					array = Environment.GetCommandLineArgs();
				}
				if (array != null && array.Length != 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].ToLowerInvariant() == "+connect_lobby" && i + 1 < array.Length)
						{
							inviteLobbyID = EditorDirector.getuLongFromString(array[i + 1]);
							PendingMPLobby = true;
							break;
						}
					}
				}
			}
			m_JoinLobbyRequested = Callback<GameLobbyJoinRequested_t>.Create(OnJoinLobbyRequested);
		}
		else
		{
			Debug.Log("Steam Not Initialised for command line read");
		}
	}

	private void OnJoinLobbyRequested(GameLobbyJoinRequested_t param)
	{
		inviteLobbyID = param.m_steamIDLobby.m_SteamID;
		ResumeInvite();
	}

	public void ResumeInvite()
	{
		if (FatControler.currentScene == Enums.SceneIDS.FrontEnd)
		{
			PendingMPLobby = true;
			MainViewModel.Instance.FrontEndMenu.ButtonClicked("Multiplayer");
		}
		else if (FatControler.currentScene != Enums.SceneIDS.ActualMainGame)
		{
			PendingMPLobby = true;
			MainViewModel.Instance.Intro_Sequence.ForceStopVideo();
			FatControler.instance.NewScene(Enums.SceneIDS.FrontEnd);
			MainViewModel.Instance.FrontEndMenu.ButtonClicked("Multiplayer");
		}
		else if (!Director.instance.SimRunning)
		{
			PendingMPLobby = true;
			FatControler.instance.NewScene(Enums.SceneIDS.FrontEnd);
			MainViewModel.Instance.FrontEndMenu.ButtonClicked("Multiplayer");
		}
		else
		{
			MainViewModel.Instance.HUDMPInviteWarning.ShowInviteWarning();
		}
	}

	public void AutoJoinPendingLobby(ref MPLobby joiningLobby, Action lobbyJoinedDelegate, Action<string, string, int> lobbyChatDelegate)
	{
		CSteamID cSteamID = new CSteamID(inviteLobbyID);
		MPLobby mPLobby = new MPLobby
		{
			id = cSteamID,
			numLobbyMembers = SteamMatchmaking.GetNumLobbyMembers(cSteamID),
			gameName = SteamMatchmaking.GetLobbyData(cSteamID, "name"),
			mapName = SteamMatchmaking.GetLobbyData(cSteamID, "map"),
			mapFileName = SteamMatchmaking.GetLobbyData(cSteamID, "mapfile"),
			maxPlayers = SteamMatchmaking.GetLobbyData(cSteamID, "max"),
			gameType = SteamMatchmaking.GetLobbyData(cSteamID, "type"),
			settings = SteamMatchmaking.GetLobbyData(cSteamID, "settings"),
			crc = SteamMatchmaking.GetLobbyData(cSteamID, "crc"),
			setTeams = SteamMatchmaking.GetLobbyData(cSteamID, "teams"),
			isHost = false
		};
		lobbies.Add(mPLobby);
		autoJoinLobby = (joiningLobby = mPLobby);
		JoinLobby(mPLobby, lobbyJoinedDelegate, lobbyChatDelegate, keepAutoJoinLobby: true);
	}

	public void JoinLobby(MPLobby lobbyToJoin, Action lobbyJoinedDelegate, Action<string, string, int> lobbyChatDelegate, bool keepAutoJoinLobby = false)
	{
		MPLobby mPLobby = autoJoinLobby;
		init();
		if (keepAutoJoinLobby)
		{
			autoJoinLobby = mPLobby;
		}
		if (lobbyToJoin != null)
		{
			_ = lobbyToJoin.id;
			lobbyJoiningID = lobbyToJoin.id;
			LobbyJoinedDelegate = lobbyJoinedDelegate;
			LobbyChatDelegate = lobbyChatDelegate;
			SteamAPICall_t hAPICall = SteamMatchmaking.JoinLobby(lobbyToJoin.id);
			CallResult<LobbyEnter_t>.Create().Set(hAPICall, JoinLobbyResult);
		}
	}

	private void JoinLobbyResult(LobbyEnter_t param, bool bIOFailure)
	{
		ulong ulSteamIDLobby = param.m_ulSteamIDLobby;
		CSteamID cSteamID = new CSteamID(ulSteamIDLobby);
		if (cSteamID == lobbyJoiningID)
		{
			CreateShareCode(ulSteamIDLobby);
			MPLobby mPLobby = (activeLobby = ((autoJoinLobby == null || !(autoJoinLobby.id == cSteamID)) ? FindLobby(cSteamID) : autoJoinLobby));
			SetPlayerColour(ConfigSettings.Settings_PlayerColour + 1);
			GetActiveLobbyMembers();
			InitChat();
			InitDataConnection();
			EngineInterface.sendPath(Application.streamingAssetsPath, ConfigSettings.GetMpAutoSavePath(), ConfigSettings.GetSavesPath());
			if (mPLobby != null && LobbyJoinedDelegate != null)
			{
				LobbyJoinedDelegate();
			}
		}
	}

	public MPLobby GetActiveLobby()
	{
		return activeLobby;
	}

	public void LeaveLobby(bool startGame = false)
	{
		if (activeLobby != null)
		{
			_ = activeLobby.id;
			if (activeLobby.isHost)
			{
				SteamMatchmaking.SetLobbyData(activeLobby.id, "closed", "1");
			}
			LeaveChat();
			SteamMatchmaking.LeaveLobby(activeLobby.id);
			activeLobby = null;
		}
	}

	public void UpdateHostLobbyInfo(string _gameName, string _mapName, string _mapFileName, int _maxPlayers, int _type, string _settings, int _crc)
	{
		if (activeLobby != null)
		{
			_ = activeLobby.id;
			if (activeLobby.isHost)
			{
				activeLobby.gameName = _gameName;
				activeLobby.mapName = _mapName;
				activeLobby.mapFileName = _mapFileName;
				activeLobby.maxPlayers = _maxPlayers.ToString();
				activeLobby.gameType = _type.ToString();
				activeLobby.settings = _settings;
				activeLobby.crc = _crc.ToString();
				SteamMatchmaking.SetLobbyData(activeLobby.id, "name", activeLobby.gameName);
				SteamMatchmaking.SetLobbyData(activeLobby.id, "map", activeLobby.mapName);
				SteamMatchmaking.SetLobbyData(activeLobby.id, "mapFile", activeLobby.mapFileName);
				SteamMatchmaking.SetLobbyData(activeLobby.id, "max", activeLobby.maxPlayers);
				SteamMatchmaking.SetLobbyData(activeLobby.id, "type", activeLobby.gameType);
				SteamMatchmaking.SetLobbyData(activeLobby.id, "settings", activeLobby.settings);
				SteamMatchmaking.SetLobbyData(activeLobby.id, "crc", activeLobby.crc);
				SteamMatchmaking.SetLobbyData(activeLobby.id, "teams", activeLobby.setTeams);
				string hostMemberOrder = activeLobby.getHostMemberOrder();
				SteamMatchmaking.SetLobbyData(activeLobby.id, "hostorder", hostMemberOrder);
				SteamMatchmaking.SetLobbyMemberLimit(activeLobby.id, _maxPlayers);
			}
		}
	}

	public void HostStartGame()
	{
		if (activeLobby != null)
		{
			_ = activeLobby.id;
			if (activeLobby.isHost)
			{
				IsHost = true;
				SteamMatchmaking.SetLobbyData(activeLobby.id, "start", "GO!");
			}
		}
	}

	public void HostLoadGame(string filename)
	{
		if (activeLobby == null)
		{
			return;
		}
		_ = activeLobby.id;
		if (activeLobby.isHost)
		{
			IsHost = true;
			for (int i = 1; i < 9; i++)
			{
				loadPlayerRemapping[i] = -1;
			}
			SteamMatchmaking.SetLobbyData(activeLobby.id, "start", filename);
		}
	}

	public bool GetActiveLobbyMembers()
	{
		if (activeLobby != null)
		{
			_ = activeLobby.id;
			bool result = false;
			List<MPLobbyMember> list = new List<MPLobbyMember>();
			int numLobbyMembers = SteamMatchmaking.GetNumLobbyMembers(activeLobby.id);
			activeLobby.numLobbyMembers = numLobbyMembers;
			for (int i = 0; i < numLobbyMembers; i++)
			{
				CSteamID lobbyMemberByIndex = SteamMatchmaking.GetLobbyMemberByIndex(activeLobby.id, i);
				string friendPersonaName = SteamFriends.GetFriendPersonaName(lobbyMemberByIndex);
				if (!(friendPersonaName != "") || !(friendPersonaName != "[unknown]"))
				{
					continue;
				}
				string lobbyMemberData = SteamMatchmaking.GetLobbyMemberData(activeLobby.id, lobbyMemberByIndex, "ready");
				string lobbyMemberData2 = SteamMatchmaking.GetLobbyMemberData(activeLobby.id, lobbyMemberByIndex, "map");
				string lobbyMemberData3 = SteamMatchmaking.GetLobbyMemberData(activeLobby.id, lobbyMemberByIndex, "request");
				int intFromString = EditorDirector.getIntFromString(SteamMatchmaking.GetLobbyMemberData(activeLobby.id, lobbyMemberByIndex, "colour"));
				int intFromString2 = EditorDirector.getIntFromString(lobbyMemberData2);
				bool flag = false;
				foreach (MPLobbyMember member in activeLobby.members)
				{
					if (!(member.id == lobbyMemberByIndex))
					{
						continue;
					}
					member.name = friendPersonaName;
					member.ready = lobbyMemberData.Length > 0;
					member.mapStatus = intFromString2;
					member.mapRequested = lobbyMemberData3;
					if (activeLobby.isHost && intFromString >= 0 && intFromString != member.colourID)
					{
						int memberColor = GetMemberColor(intFromString, lobbyMemberByIndex);
						if (memberColor != member.colourID)
						{
							result = true;
						}
						member.colourID = memberColor;
					}
					list.Add(member);
					flag = true;
					break;
				}
				if (flag)
				{
					continue;
				}
				MPLobbyMember mPLobbyMember = new MPLobbyMember
				{
					id = lobbyMemberByIndex,
					name = friendPersonaName,
					ready = (lobbyMemberData.Length > 0),
					mapStatus = intFromString2,
					mapRequested = lobbyMemberData3,
					colourID = intFromString
				};
				if (activeLobby.isHost)
				{
					bool num = mPLobbyMember.id == SteamMatchmaking.GetLobbyOwner(activeLobby.id);
					mPLobbyMember.colourID = GetMemberColor(intFromString, lobbyMemberByIndex);
					activeLobby.setTeam(mPLobbyMember, activeLobby.getFreeTeam());
					if (!num)
					{
						result = true;
					}
				}
				list.Add(mPLobbyMember);
			}
			if (!activeLobby.isHost)
			{
				List<ulong> hostMemberOrder = activeLobby.hostMemberOrder;
				activeLobby.members.Clear();
				foreach (ulong item in hostMemberOrder)
				{
					foreach (MPLobbyMember item2 in list)
					{
						if (item2.id.m_SteamID == item)
						{
							activeLobby.members.Add(item2);
							break;
						}
					}
				}
			}
			else
			{
				activeLobby.members = list;
			}
			if (activeLobby.isHost)
			{
				activeLobby.validateTeams();
				string hostMemberOrder2 = activeLobby.getHostMemberOrder();
				SteamMatchmaking.SetLobbyData(activeLobby.id, "hostorder", hostMemberOrder2);
			}
			return result;
		}
		return false;
	}

	public List<int> GetUsedColours(int ignoredColour)
	{
		List<int> list = new List<int>();
		foreach (MPLobbyMember member in activeLobby.members)
		{
			if (member.colourID > 0 && member.colourID != ignoredColour)
			{
				list.Add(member.colourID);
			}
		}
		return list;
	}

	private int GetMemberColor(int requestedColour, CSteamID memberID)
	{
		bool[] array = new bool[9];
		for (int i = 0; i < 9; i++)
		{
			array[i] = false;
		}
		foreach (MPLobbyMember member in activeLobby.members)
		{
			if (member.id != memberID && member.colourID > 0)
			{
				array[member.colourID] = true;
			}
		}
		if (requestedColour > 0 && requestedColour < 9 && !array[requestedColour])
		{
			return requestedColour;
		}
		for (int j = 1; j < 9; j++)
		{
			if (!array[j])
			{
				return j;
			}
		}
		return 1;
	}

	public void SetPlayerColour(int colourID)
	{
		if (activeLobby == null)
		{
			return;
		}
		SteamMatchmaking.SetLobbyMemberData(activeLobby.id, "colour", colourID.ToString());
		CSteamID steamID = SteamUser.GetSteamID();
		foreach (MPLobbyMember member in activeLobby.members)
		{
			if (member.id == steamID)
			{
				member.colourID = colourID;
				break;
			}
		}
	}

	public void SetMemberReadyState(bool state)
	{
		if (activeLobby == null)
		{
			return;
		}
		if (state)
		{
			SteamMatchmaking.SetLobbyMemberData(activeLobby.id, "ready", "ready");
		}
		else
		{
			SteamMatchmaking.SetLobbyMemberData(activeLobby.id, "ready", "");
		}
		CSteamID steamID = SteamUser.GetSteamID();
		foreach (MPLobbyMember member in activeLobby.members)
		{
			if (member.id == steamID)
			{
				member.ready = state;
				break;
			}
		}
	}

	public void KickMemberFromLobby(MPLobbyMember member)
	{
		if (activeLobby != null && activeLobby.isHost)
		{
			kickMemberTime = DateTime.UtcNow.AddSeconds(2.0);
			SteamMatchmaking.SetLobbyData(activeLobby.id, "kick", member.id.ToString());
		}
	}

	public bool RefreshLobbyList(ref EngineInterface.MultiplayerSetupData MPSetupData, ref bool refreshTeams, ref bool settingsChanged)
	{
		refreshTeams = false;
		settingsChanged = false;
		if (lastLobbyDataRefresh != DateTime.MinValue && (DateTime.UtcNow - lastLobbyDataRefresh).TotalSeconds > 5.0)
		{
			lastLobbyDataRefresh = DateTime.UtcNow;
			List<MPLobby> list = new List<MPLobby>();
			foreach (MPLobby lobby in lobbies)
			{
				if (lobby.isHost || (activeLobby != null && !(lobby.id != activeLobby.id)))
				{
					continue;
				}
				if (SteamMatchmaking.RequestLobbyData(lobby.id))
				{
					lobby.numLobbyMembers = SteamMatchmaking.GetNumLobbyMembers(lobby.id);
					lobby.gameName = SteamMatchmaking.GetLobbyData(lobby.id, "name");
					lobby.mapName = SteamMatchmaking.GetLobbyData(lobby.id, "map");
					lobby.mapFileName = SteamMatchmaking.GetLobbyData(lobby.id, "mapFile");
					lobby.maxPlayers = SteamMatchmaking.GetLobbyData(lobby.id, "max");
					lobby.gameType = SteamMatchmaking.GetLobbyData(lobby.id, "type");
					lobby.settings = SteamMatchmaking.GetLobbyData(lobby.id, "settings");
					lobby.setTeams = SteamMatchmaking.GetLobbyData(lobby.id, "teams");
					lobby.crc = SteamMatchmaking.GetLobbyData(lobby.id, "crc");
					lobby.startGame = SteamMatchmaking.GetLobbyData(lobby.id, "start");
					if (SteamMatchmaking.GetLobbyData(lobby.id, "closed") != "0")
					{
						list.Add(lobby);
					}
				}
				else
				{
					list.Add(lobby);
				}
			}
			foreach (MPLobby item in list)
			{
				lobbies.Remove(item);
			}
		}
		if (activeLobby != null && !activeLobby.isHost)
		{
			activeLobby.numLobbyMembers = SteamMatchmaking.GetNumLobbyMembers(activeLobby.id);
			activeLobby.gameName = SteamMatchmaking.GetLobbyData(activeLobby.id, "name");
			activeLobby.mapName = SteamMatchmaking.GetLobbyData(activeLobby.id, "map");
			activeLobby.mapFileName = SteamMatchmaking.GetLobbyData(activeLobby.id, "mapFile");
			activeLobby.maxPlayers = SteamMatchmaking.GetLobbyData(activeLobby.id, "max");
			activeLobby.gameType = SteamMatchmaking.GetLobbyData(activeLobby.id, "type");
			activeLobby.settings = SteamMatchmaking.GetLobbyData(activeLobby.id, "settings");
			activeLobby.crc = SteamMatchmaking.GetLobbyData(activeLobby.id, "crc");
			activeLobby.setTeams = SteamMatchmaking.GetLobbyData(activeLobby.id, "teams");
			activeLobby.startGame = SteamMatchmaking.GetLobbyData(activeLobby.id, "start");
			if (SteamMatchmaking.GetLobbyData(activeLobby.id, "closed") != "0")
			{
				return true;
			}
			settingsChanged = MPSetupData.FromString(activeLobby.settings);
			string lobbyData = SteamMatchmaking.GetLobbyData(activeLobby.id, "hostorder");
			activeLobby.setHostMemberOrder(lobbyData);
			string lobbyData2 = SteamMatchmaking.GetLobbyData(activeLobby.id, "kick");
			if (lobbyData2.Length > 0 && EditorDirector.getuLongFromString(lobbyData2) == SteamUser.GetSteamID().m_SteamID)
			{
				return true;
			}
			CSteamID lobbyOwner = SteamMatchmaking.GetLobbyOwner(activeLobby.id);
			if (lobbyOwner.m_SteamID == 0L || lobbyOwner == SteamUser.GetSteamID())
			{
				return true;
			}
		}
		else if (activeLobby != null && activeLobby.isHost)
		{
			activeLobby.startGame = SteamMatchmaking.GetLobbyData(activeLobby.id, "start");
		}
		if (activeLobby != null)
		{
			if (activeLobby.isHost && kickMemberTime != DateTime.MinValue && kickMemberTime < DateTime.UtcNow)
			{
				kickMemberTime = DateTime.MinValue;
				SteamMatchmaking.SetLobbyData(activeLobby.id, "kick", "");
			}
			refreshTeams = GetActiveLobbyMembers();
			receiveLobbyMessages();
			ReceiveGameMessages();
		}
		return false;
	}

	private void InitChat()
	{
		if (IncomingMessage == null)
		{
			IncomingMessage = Callback<LobbyChatMsg_t>.Create(HandleIncomingMessage);
		}
	}

	private void LeaveChat()
	{
		if (IncomingMessage != null)
		{
			IncomingMessage.Dispose();
			IncomingMessage = null;
		}
	}

	private void HandleIncomingMessage(LobbyChatMsg_t callback)
	{
		byte[] array = new byte[4096];
		int cubData = 4096;
		int iChatID = (int)callback.m_iChatID;
		CSteamID pSteamIDUser;
		EChatEntryType peChatEntryType;
		int lobbyChatEntry = SteamMatchmaking.GetLobbyChatEntry(new CSteamID(callback.m_ulSteamIDLobby), iChatID, out pSteamIDUser, array, cubData, out peChatEntryType);
		if (lobbyChatEntry > 0)
		{
			byte[] array2 = new byte[lobbyChatEntry];
			Array.Copy(array, array2, lobbyChatEntry);
			string @string = Encoding.UTF8.GetString(array2);
			ChatHandle(@string, pSteamIDUser);
		}
	}

	private void ChatHandle(string message, CSteamID Id)
	{
		int arg = -1;
		foreach (MPLobbyMember member in activeLobby.members)
		{
			if (member.id == Id)
			{
				arg = member.colourID;
				break;
			}
		}
		string friendPersonaName = SteamFriends.GetFriendPersonaName(Id);
		if (LobbyChatDelegate != null)
		{
			LobbyChatDelegate(friendPersonaName, message, arg);
		}
	}

	public void SendLobbyChatMessage(string Message)
	{
		if (activeLobby != null)
		{
			CSteamID id = activeLobby.id;
			byte[] bytes = Encoding.UTF8.GetBytes(Message);
			SteamMatchmaking.SendLobbyChatMsg(id, bytes, bytes.Length);
		}
	}

	public void SetMapStatus(int status)
	{
		if (activeLobby != null && !activeLobby.isHost)
		{
			SteamMatchmaking.SetLobbyMemberData(activeLobby.id, "map", status.ToString());
		}
	}

	private void InitDataConnection()
	{
		EndDataConnection();
		NetworkUserListener = Callback<SteamNetworkingMessagesSessionRequest_t>.Create(HandleGameIncomingConnection);
	}

	private void EndDataConnection()
	{
		if (NetworkUserListener != null)
		{
			NetworkUserListener.Dispose();
			NetworkUserListener = null;
		}
	}

	public bool SendMap(MPLobbyMember member, string mapFileName, string fullPath)
	{
		if (!member.mapsSent.ContainsKey(mapFileName.ToLower()))
		{
			member.mapsSent[mapFileName.ToLower()] = true;
			byte[] mapData = File.ReadAllBytes(fullPath);
			CSteamID id = member.id;
			SendMapInternal(id, mapData, mapFileName);
			return true;
		}
		return false;
	}

	private unsafe void SendMapInternal(CSteamID targetID, byte[] mapData, string mapFileName)
	{
		SteamNetworkingIdentity identityRemote = default(SteamNetworkingIdentity);
		identityRemote.SetSteamID(targetID);
		int nSendFlags = 40;
		byte[] bytes = BitConverter.GetBytes(EngineInterface.crc(mapData));
		fixed (byte* ptr = bytes)
		{
			IntPtr pubData = (IntPtr)ptr;
			EResult eResult = SteamNetworkingMessages.SendMessageToUser(ref identityRemote, pubData, (uint)bytes.Length, nSendFlags, 1);
			if (eResult != EResult.k_EResultOK)
			{
				Debug.Log(eResult);
			}
		}
		byte[] bytes2 = BitConverter.GetBytes(mapData.Length);
		fixed (byte* ptr2 = bytes2)
		{
			IntPtr pubData2 = (IntPtr)ptr2;
			EResult eResult2 = SteamNetworkingMessages.SendMessageToUser(ref identityRemote, pubData2, (uint)bytes2.Length, nSendFlags, 1);
			if (eResult2 != EResult.k_EResultOK)
			{
				Debug.Log(eResult2);
			}
		}
		byte[] bytes3 = Encoding.UTF8.GetBytes(mapFileName);
		fixed (byte* ptr3 = bytes3)
		{
			IntPtr pubData3 = (IntPtr)ptr3;
			EResult eResult3 = SteamNetworkingMessages.SendMessageToUser(ref identityRemote, pubData3, (uint)bytes3.Length, nSendFlags, 1);
			if (eResult3 != EResult.k_EResultOK)
			{
				Debug.Log(eResult3);
			}
		}
		int num;
		for (int i = 0; i < mapData.Length; i += num)
		{
			MapSendQueueItem mapSendQueueItem = new MapSendQueueItem();
			mapSendQueueItem.NetID = identityRemote;
			num = Math.Min(250000, mapData.Length - i);
			mapSendQueueItem.data = new byte[num];
			Array.Copy(mapData, i, mapSendQueueItem.data, 0, num);
			if (lastMapSendQueueItemTime < DateTime.UtcNow)
			{
				mapSendQueueItem.sendTime = DateTime.UtcNow.AddSeconds(2.0);
			}
			else
			{
				mapSendQueueItem.sendTime = lastMapSendQueueItemTime.AddSeconds(2.0);
			}
			lastMapSendQueueItemTime = mapSendQueueItem.sendTime;
			mapSendQueue.Enqueue(mapSendQueueItem);
		}
	}

	public unsafe void ProcessMapSendQueue()
	{
		if (mapSendQueue.Count <= 0)
		{
			return;
		}
		MapSendQueueItem mapSendQueueItem = mapSendQueue.Peek();
		if (!(mapSendQueueItem.sendTime < DateTime.UtcNow))
		{
			return;
		}
		mapSendQueueItem = mapSendQueue.Dequeue();
		int nSendFlags = 40;
		fixed (byte* ptr = mapSendQueueItem.data)
		{
			IntPtr pubData = (IntPtr)ptr;
			EResult eResult = SteamNetworkingMessages.SendMessageToUser(ref mapSendQueueItem.NetID, pubData, (uint)mapSendQueueItem.data.Length, nSendFlags, 1);
			if (eResult != EResult.k_EResultOK)
			{
				Debug.Log(eResult);
			}
		}
	}

	private unsafe void SendSaveCRC(string mapName, int crcAndGameTime)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(mapName);
		byte[] array = new byte[bytes.Length + 4];
		byte[] bytes2 = BitConverter.GetBytes(crcAndGameTime);
		Array.Copy(bytes, 0, array, 4, bytes.Length);
		Array.Copy(bytes2, 0, array, 0, 4);
		int nSendFlags = 40;
		foreach (MPLobbyMember member in activeLobby.members)
		{
			if (!(member.id != SteamUser.GetSteamID()))
			{
				continue;
			}
			SteamNetworkingIdentity identityRemote = default(SteamNetworkingIdentity);
			identityRemote.SetSteamID(member.id);
			fixed (byte* ptr = array)
			{
				IntPtr pubData = (IntPtr)ptr;
				EResult eResult = SteamNetworkingMessages.SendMessageToUser(ref identityRemote, pubData, (uint)array.Length, nSendFlags, 21);
				if (eResult != EResult.k_EResultOK)
				{
					Debug.Log(eResult);
				}
			}
		}
	}

	public unsafe void HostSendLobbyPings()
	{
		byte[] array = new byte[0];
		int nSendFlags = 40;
		foreach (MPLobbyMember member in activeLobby.members)
		{
			if (!(member.id != SteamUser.GetSteamID()) || !(member.pingSent == DateTime.MinValue))
			{
				continue;
			}
			member.pingSent = DateTime.UtcNow;
			SteamNetworkingIdentity identityRemote = default(SteamNetworkingIdentity);
			identityRemote.SetSteamID(member.id);
			fixed (byte* ptr = array)
			{
				IntPtr pubData = (IntPtr)ptr;
				SteamNetworkingMessages.SendMessageToUser(ref identityRemote, pubData, (uint)array.Length, nSendFlags, 31);
			}
		}
	}

	private unsafe void receiveLobbyMessages()
	{
		IntPtr[] array = new IntPtr[16];
		int num = SteamNetworkingMessages.ReceiveMessagesOnChannel(1, array, array.Length);
		for (int i = 0; i < num; i++)
		{
			SteamNetworkingMessage_t steamNetworkingMessage_t = Marshal.PtrToStructure<SteamNetworkingMessage_t>(array[i]);
			byte[] array2 = new byte[steamNetworkingMessage_t.m_cbSize];
			Marshal.Copy(steamNetworkingMessage_t.m_pData, array2, 0, array2.Length);
			if (receivingMode == 0)
			{
				if (array2.Length == 4)
				{
					lastCrc = BitConverter.ToInt32(array2);
					receivingMode = 1;
				}
			}
			else if (receivingMode == 1)
			{
				if (array2.Length == 4)
				{
					incomingSize = BitConverter.ToInt32(array2);
					incomingOfset = 0;
					receivingMode = 2;
					receiveBuffer = new byte[incomingSize];
				}
			}
			else if (receivingMode == 2)
			{
				if (array2.Length < 1000)
				{
					lastReceivedFileName = Encoding.UTF8.GetString(array2);
					receivingMode = 3;
				}
			}
			else
			{
				if (receivingMode != 3)
				{
					continue;
				}
				int num2 = array2.Length;
				if (incomingOfset + num2 > incomingSize)
				{
					receivingMode = 0;
					return;
				}
				Array.Copy(array2, 0, receiveBuffer, incomingOfset, num2);
				incomingOfset += num2;
				if (incomingOfset != incomingSize)
				{
					continue;
				}
				if (lastCrc != 0 && lastReceivedFileName.Length > 0)
				{
					File.WriteAllBytes(Path.Combine(ConfigSettings.GetUserMapsPath(), lastReceivedFileName + ".map"), receiveBuffer);
					if (MapReceivedDelegate != null)
					{
						MapReceivedDelegate();
					}
				}
				receivingMode = 0;
			}
		}
		if (!activeLobby.isHost)
		{
			int nSendFlags = 40;
			num = SteamNetworkingMessages.ReceiveMessagesOnChannel(21, array, array.Length);
			for (int j = 0; j < num; j++)
			{
				SteamNetworkingMessage_t steamNetworkingMessage_t2 = Marshal.PtrToStructure<SteamNetworkingMessage_t>(array[j]);
				byte[] array3 = new byte[steamNetworkingMessage_t2.m_cbSize];
				Marshal.Copy(steamNetworkingMessage_t2.m_pData, array3, 0, array3.Length);
				int num3 = BitConverter.ToInt32(array3, 0);
				string @string = Encoding.UTF8.GetString(array3, 4, array3.Length - 4);
				FileHeader headerFromMpSaveFileName = MapFileManager.Instance.GetHeaderFromMpSaveFileName(@string);
				if (headerFromMpSaveFileName == null || (headerFromMpSaveFileName.xPlaySaveChecksum ^ headerFromMpSaveFileName.xPlaySaveTime) != num3)
				{
					continue;
				}
				SteamNetworkingIdentity identityRemote = steamNetworkingMessage_t2.m_identityPeer;
				fixed (byte* ptr = array3)
				{
					IntPtr pubData = (IntPtr)ptr;
					EResult eResult = SteamNetworkingMessages.SendMessageToUser(ref identityRemote, pubData, (uint)array3.Length, nSendFlags, 21);
					if (eResult != EResult.k_EResultOK)
					{
						Debug.Log(eResult);
					}
				}
			}
			num = SteamNetworkingMessages.ReceiveMessagesOnChannel(31, array, array.Length);
			for (int k = 0; k < num; k++)
			{
				byte[] array4 = new byte[0];
				SteamNetworkingIdentity identityRemote2 = Marshal.PtrToStructure<SteamNetworkingMessage_t>(array[k]).m_identityPeer;
				fixed (byte* ptr2 = array4)
				{
					IntPtr pubData2 = (IntPtr)ptr2;
					EResult eResult2 = SteamNetworkingMessages.SendMessageToUser(ref identityRemote2, pubData2, (uint)array4.Length, nSendFlags, 32);
					if (eResult2 != EResult.k_EResultOK)
					{
						Debug.Log(eResult2);
					}
				}
			}
			return;
		}
		num = SteamNetworkingMessages.ReceiveMessagesOnChannel(21, array, array.Length);
		for (int l = 0; l < num; l++)
		{
			SteamNetworkingMessage_t steamNetworkingMessage_t3 = Marshal.PtrToStructure<SteamNetworkingMessage_t>(array[l]);
			byte[] array5 = new byte[steamNetworkingMessage_t3.m_cbSize];
			Marshal.Copy(steamNetworkingMessage_t3.m_pData, array5, 0, array5.Length);
			BitConverter.ToInt32(array5, 0);
			string string2 = Encoding.UTF8.GetString(array5, 4, array5.Length - 4);
			FileHeader headerFromMpSaveFileName2 = MapFileManager.Instance.GetHeaderFromMpSaveFileName(string2);
			if (headerFromMpSaveFileName2 != null)
			{
				headerFromMpSaveFileName2.retrieveCRCChecks++;
			}
		}
		num = SteamNetworkingMessages.ReceiveMessagesOnChannel(32, array, array.Length);
		for (int m = 0; m < num; m++)
		{
			_ = new byte[0];
			CSteamID steamID = Marshal.PtrToStructure<SteamNetworkingMessage_t>(array[m]).m_identityPeer.GetSteamID();
			foreach (MPLobbyMember member in activeLobby.members)
			{
				if (member.id == steamID)
				{
					DateTime utcNow = DateTime.UtcNow;
					member.lastPingDuration = (int)(utcNow - member.pingSent).TotalMilliseconds;
					member.pingSent = DateTime.MinValue;
				}
			}
		}
	}

	public void RequestMap(string mapFileName, Action mapReceived)
	{
		if (activeLobby != null && !activeLobby.isHost)
		{
			MapReceivedDelegate = mapReceived;
			lastCrc = 0;
			lastReceivedFileName = "";
			SteamMatchmaking.SetLobbyMemberData(activeLobby.id, "request", mapFileName);
		}
	}

	public void StartGame(EngineInterface.MultiplayerSetupData MPSetupData, FileHeader map)
	{
		if (activeLobby == null || map == null)
		{
			return;
		}
		MainViewModel.Instance.InitNewScene(Enums.SceneIDS.MainGame);
		MainViewModel.Instance.Show_MP_LoadingBlack = true;
		newGameNotLoad = true;
		Director.instance.SetEngineFrameRate(MPSetupData.starting_gamespeed);
		EngineInterface.sendPath(Application.streamingAssetsPath, ConfigSettings.GetMpAutoSavePath(), ConfigSettings.GetSavesPath());
		EngineInterface.initMultiplayerGame();
		connectionPauseEngineState = false;
		EngineInterface.setMultiplayerStartingData(MPSetupData);
		gameMembers = new List<MPGameMember>();
		int num = 1;
		foreach (MPLobbyMember member in activeLobby.members)
		{
			MPGameMember mPGameMember = new MPGameMember();
			mPGameMember.lobbyData = member;
			mPGameMember.playerName = member.name;
			mPGameMember.SNI = default(SteamNetworkingIdentity);
			mPGameMember.SNI.SetSteamID(member.id);
			mPGameMember.steamID = member.id.m_SteamID;
			mPGameMember.playerID = num;
			mPGameMember.colourID = member.colourID;
			mPGameMember.isSelf = member.id == SteamUser.GetSteamID();
			mPGameMember.isHost = member.id == SteamMatchmaking.GetLobbyOwner(activeLobby.id);
			gameMembers.Add(mPGameMember);
			if (mPGameMember.isSelf)
			{
				localPlayerID = num;
			}
			EngineInterface.RegisterMPPlayer(num, member.name, activeLobby.getTeam(member), mPGameMember.isSelf);
			num++;
		}
		EngineInterface.LoadMapReturnData retData = EngineInterface.loadMultiplayerMap(map.filePath);
		IsHost = activeLobby.isHost;
		if (activeLobby.isHost)
		{
			int value = EngineInterface.StartMultiplayerGame(fromSave: false);
			MPData mPData = new MPData();
			mPData.packetType = 2;
			mPData.dataLength = 4;
			mPData.data = BitConverter.GetBytes(value);
			SendPacketToAll(mPData);
			EditorDirector.instance.postLoading(retData, startGameThread: false);
			EditorDirector.instance.SetLocalPlayer(localPlayerID);
			SpriteMapping.BuildMultiPlayerColourMapping();
			MainViewModel.Instance.InitObjectiveGoodsPanel();
			monitoringForGameStart = true;
			monitoringForGameStartTime = DateTime.UtcNow;
		}
		else if (seedReceived)
		{
			SendEmptyPacketTypeToAll(Enums.MPFlags.InitialAcknowledgePacket);
			EngineInterface.SetMPRandSeed(randSeedReceived);
			EditorDirector.instance.postLoading(retData, startGameThread: false);
			MainViewModel.Instance.InitObjectiveGoodsPanel();
			EditorDirector.instance.SetLocalPlayer(localPlayerID);
			SpriteMapping.BuildMultiPlayerColourMapping();
		}
		else
		{
			lastRetData = retData;
		}
		mapLoaded = true;
	}

	public void StartSave(EngineInterface.MultiplayerSetupData MPSetupData, FileHeader map)
	{
		if (activeLobby == null || map == null)
		{
			return;
		}
		MainViewModel.Instance.InitNewScene(Enums.SceneIDS.MainGame);
		MainViewModel.Instance.Show_MP_LoadingBlack = true;
		newGameNotLoad = false;
		Director.instance.SetEngineFrameRate(MPSetupData.starting_gamespeed);
		EngineInterface.sendPath(Application.streamingAssetsPath, ConfigSettings.GetMpAutoSavePath(), ConfigSettings.GetSavesPath());
		EngineInterface.initMultiplayerGame();
		connectionPauseEngineState = false;
		gameMembers = new List<MPGameMember>();
		int num = 1;
		int playerID = -1;
		if (!IsHost)
		{
			for (int i = 1; i < 9; i++)
			{
				loadPlayerRemapping[i] = -1;
			}
		}
		foreach (MPLobbyMember member in activeLobby.members)
		{
			MPGameMember mPGameMember = new MPGameMember();
			mPGameMember.lobbyData = member;
			mPGameMember.playerName = member.name;
			mPGameMember.SNI = default(SteamNetworkingIdentity);
			mPGameMember.SNI.SetSteamID(member.id);
			mPGameMember.steamID = member.id.m_SteamID;
			mPGameMember.playerID = num;
			mPGameMember.colourID = member.colourID;
			mPGameMember.isSelf = member.id == SteamUser.GetSteamID();
			mPGameMember.isHost = member.id == SteamMatchmaking.GetLobbyOwner(activeLobby.id);
			if (mPGameMember.isHost)
			{
				playerID = num;
			}
			gameMembers.Add(mPGameMember);
			if (mPGameMember.isSelf)
			{
				localPlayerID = num;
			}
			EngineInterface.RegisterMPPlayer(num, member.name, activeLobby.getTeam(member), mPGameMember.isSelf);
			num++;
		}
		EngineInterface.LoadMapReturnData retData = EngineInterface.loadMultiplayerMap(map.filePath, multiplayerSave: true);
		int playerID2 = retData.playerID;
		if (!activeLobby.isHost)
		{
			MPData mPData = new MPData();
			mPData.packetType = 9;
			mPData.dataLength = 8;
			byte[] bytes = BitConverter.GetBytes(playerID2);
			byte[] bytes2 = BitConverter.GetBytes(localPlayerID);
			mPData.data = new byte[8];
			for (int j = 0; j < 4; j++)
			{
				mPData.data[j] = bytes[j];
				mPData.data[j + 4] = bytes2[j];
			}
			SendPacketToPlayerID(playerID, mPData);
		}
		else
		{
			loadPlayerRemapping[localPlayerID] = playerID2;
		}
		IsHost = activeLobby.isHost;
		if (activeLobby.isHost)
		{
			int value = EngineInterface.StartMultiplayerGame(fromSave: true);
			MPData mPData2 = new MPData();
			mPData2.packetType = 2;
			mPData2.dataLength = 4;
			mPData2.data = BitConverter.GetBytes(value);
			SendPacketToAll(mPData2);
			EditorDirector.instance.postLoading(retData, startGameThread: false);
			EditorDirector.instance.SetLocalPlayer(localPlayerID);
			SpriteMapping.BuildMultiPlayerColourMapping();
			MainViewModel.Instance.InitObjectiveGoodsPanel();
			monitoringForGameStart = true;
			monitoringForGameStartTime = DateTime.UtcNow;
		}
		else if (seedReceived)
		{
			SendEmptyPacketTypeToAll(Enums.MPFlags.InitialAcknowledgePacket);
			EngineInterface.SetMPRandSeed(randSeedReceived);
			EditorDirector.instance.postLoading(retData, startGameThread: false);
			EditorDirector.instance.SetLocalPlayer(localPlayerID);
			SpriteMapping.BuildMultiPlayerColourMapping();
			MainViewModel.Instance.InitObjectiveGoodsPanel();
		}
		else
		{
			lastRetData = retData;
		}
		mapLoaded = true;
	}

	private MPGameMember findMember(SteamNetworkingIdentity SNI)
	{
		if (gameMembers != null)
		{
			foreach (MPGameMember gameMember in gameMembers)
			{
				if (SNI.GetSteamID() == gameMember.SNI.GetSteamID())
				{
					return gameMember;
				}
			}
		}
		return null;
	}

	private void HandleGameIncomingConnection(SteamNetworkingMessagesSessionRequest_t callback)
	{
		SteamNetworkingIdentity identityRemote = callback.m_identityRemote;
		if (gameMembers != null)
		{
			foreach (MPGameMember gameMember in gameMembers)
			{
				if (identityRemote.GetSteamID() == gameMember.lobbyData.id)
				{
					SteamNetworkingMessages.AcceptSessionWithUser(ref identityRemote);
					return;
				}
			}
			Debug.Log("Ignored Incoming Connection");
		}
		else if (activeLobby != null)
		{
			if (identityRemote.GetSteamID() == SteamMatchmaking.GetLobbyOwner(activeLobby.id))
			{
				SteamNetworkingMessages.AcceptSessionWithUser(ref identityRemote);
			}
			else
			{
				Debug.Log("Rejected Incoming Connection while in lobby");
			}
		}
		else
		{
			Debug.Log("Rejected Incoming Connection, unknown");
		}
	}

	private unsafe void SendGameData(MPGameMember target, byte[] dataToSend, bool instantMessage = false)
	{
		if (target.kicked)
		{
			return;
		}
		SteamNetworkingIdentity identityRemote = default(SteamNetworkingIdentity);
		identityRemote.SetSteamID(new CSteamID(target.steamID));
		int num = 40;
		if (instantMessage)
		{
			num |= 4;
		}
		fixed (byte* ptr = dataToSend)
		{
			IntPtr pubData = (IntPtr)ptr;
			if (SteamNetworkingMessages.SendMessageToUser(ref identityRemote, pubData, (uint)dataToSend.Length, num, 2) != EResult.k_EResultOK)
			{
				if (!target.pendingKick)
				{
					target.errorCount++;
				}
			}
			else
			{
				target.errorCount = 0;
			}
		}
	}

	public void ReceiveGameMessages()
	{
		IntPtr[] array = new IntPtr[16];
		int num = SteamNetworkingMessages.ReceiveMessagesOnChannel(2, array, array.Length);
		for (int i = 0; i < num; i++)
		{
			SteamNetworkingMessage_t steamNetworkingMessage_t = Marshal.PtrToStructure<SteamNetworkingMessage_t>(array[i]);
			if (steamNetworkingMessage_t.m_cbSize > 2000000)
			{
				continue;
			}
			byte[] array2 = new byte[steamNetworkingMessage_t.m_cbSize];
			Marshal.Copy(steamNetworkingMessage_t.m_pData, array2, 0, array2.Length);
			MPData mPData = MPData.FromBytes(array2);
			MPGameMember mPGameMember = findMember(steamNetworkingMessage_t.m_identityPeer);
			if (mPGameMember == null)
			{
				if (mPData.packetType == 2)
				{
					int num2 = BitConverter.ToInt32(mPData.data);
					randSeedReceived = num2;
					seedReceived = true;
				}
				else if (mPData.packetType == 9 && IsHost && mPData.dataLength == 8)
				{
					int num3 = BitConverter.ToInt32(mPData.data, 0);
					int num4 = BitConverter.ToInt32(mPData.data, 4);
					loadPlayerRemapping[num4] = num3;
				}
				continue;
			}
			if (mPGameMember.kicked)
			{
				if (IsHost)
				{
					new MPData
					{
						packetType = 7,
						dataLength = 4,
						data = BitConverter.GetBytes(mPGameMember.playerID)
					};
					byte[] dataToSend = mPData.ToBytes();
					SendGameData(mPGameMember, dataToSend);
				}
				continue;
			}
			switch (mPData.packetType)
			{
			case 2:
			{
				int mPRandSeed = BitConverter.ToInt32(mPData.data);
				if (mapLoaded)
				{
					EngineInterface.SetMPRandSeed(mPRandSeed);
					EditorDirector.instance.postLoading(lastRetData, startGameThread: false);
					EditorDirector.instance.SetLocalPlayer(localPlayerID);
					SpriteMapping.BuildMultiPlayerColourMapping();
					MainViewModel.Instance.InitObjectiveGoodsPanel();
					SendEmptyPacketTypeToAll(Enums.MPFlags.InitialAcknowledgePacket);
				}
				else
				{
					randSeedReceived = mPRandSeed;
					seedReceived = true;
				}
				break;
			}
			case 3:
				if (IsHost)
				{
					mPGameMember.acknowledged = true;
				}
				break;
			case 9:
				if (IsHost && mPData.dataLength == 8)
				{
					int num7 = BitConverter.ToInt32(mPData.data, 0);
					int num8 = BitConverter.ToInt32(mPData.data, 4);
					loadPlayerRemapping[num8] = num7;
				}
				break;
			case 4:
				LeaveLobby();
				if (mPData.dataLength == 36)
				{
					for (int j = 0; j < 9; j++)
					{
						int num5 = BitConverter.ToInt32(mPData.data, j * 4);
						loadPlayerRemapping[j] = num5;
					}
					if (!remapMPGameMembers())
					{
						FatControler.instance.NewScene(Enums.SceneIDS.FrontEnd);
						return;
					}
				}
				Director.instance.startSimThread();
				EngineInterface.GameAction(Enums.KeyFunctions.HomeKeep);
				if (ConfigSettings.Settings_ShowPings)
				{
					OnScreenText.Instance.addOSTEntry(Enums.eOnScreenText.OST_PINGS, 1);
				}
				EngineInterface.StartMultiplayerGameSynced();
				Director.instance.DelayHideConnectionScreen();
				MPGameActive = true;
				break;
			case 1:
				EngineInterface.ReceiveChore(mPGameMember.playerID, mPData.data, mPData.dataLength);
				if (mPData.dataLength > 0)
				{
					switch (mPData.data[0])
					{
					case 0:
						mPGameMember.lastTimePacketRecieved = DateTime.UtcNow;
						break;
					case 54:
						resyncingOrSaving = true;
						MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 45), 0, "Resyncing");
						break;
					case 67:
						resyncingOrSaving = false;
						resyncingOrSavingResumeTime = DateTime.MinValue;
						break;
					case 39:
						resyncingOrSaving = true;
						MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(getPlayerName(mPGameMember.playerID), mPGameMember.playerID, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 32));
						break;
					case 94:
						resyncingOrSaving = false;
						resyncingOrSavingResumeTime = DateTime.UtcNow.AddSeconds(5.0);
						break;
					case 89:
						EditorDirector.instance.MPGameKilled();
						break;
					}
				}
				break;
			case 5:
				if (!mPGameMember.muted)
				{
					string @string = Encoding.UTF8.GetString(mPData.data);
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(getPlayerName(mPGameMember.playerID), mPGameMember.playerID, @string);
				}
				break;
			case 6:
				if (!mPGameMember.muted)
				{
					int num6 = BitConverter.ToInt32(mPData.data);
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(getPlayerName(mPGameMember.playerID), mPGameMember.playerID, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_INSULTS, num6), 10);
					SFXManager.instance.playInsult(num6);
				}
				break;
			case 7:
			{
				int playerID = BitConverter.ToInt32(mPData.data);
				MPGameMember player = getPlayer(playerID);
				if (player == null || player.kicked)
				{
					break;
				}
				if (player.isSelf)
				{
					int otherActivePlayers = countOtherPlayers(player);
					if (player.DoVoteKick(mPGameMember.playerID, otherActivePlayers))
					{
						EditorDirector.instance.stopGameSim();
						MainViewModel.Instance.InitNewScene(Enums.SceneIDS.FrontEnd);
					}
					break;
				}
				player.kickCounter++;
				int otherActivePlayers2 = countOtherPlayers(player);
				if (player.DoVoteKick(mPGameMember.playerID, otherActivePlayers2))
				{
					if (player.isHost)
					{
						promoteNewHost(player);
					}
					player.kicked = true;
					EngineInterface.KickMPPlayer(playerID, kickImmediate: false);
					MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(player.playerName, player.playerID, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MULTIPLAYER_CONNECTION, 21));
				}
				break;
			}
			case 8:
				if (mPGameMember.isHost)
				{
					promoteNewHost(mPGameMember);
				}
				mPGameMember.kicked = true;
				EngineInterface.KickMPPlayer(mPGameMember.playerID, kickImmediate: false);
				MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(mPGameMember.playerName, mPGameMember.playerID, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MULTIPLAYER_CONNECTION, 21));
				break;
			}
		}
	}

	public bool MonitorNetworkConnectivity()
	{
		return SteamUser.BLoggedOn();
	}

	private void CreateShareCode(ulong lobbyID)
	{
		string text = Convert.ToBase64String(BitConverter.GetBytes(lobbyID));
		int num = 0;
		string text2 = text;
		foreach (char c in text2)
		{
			num += c;
		}
		int num2 = num % 26 + 65;
		ShareCodeString = text + (char)num2;
	}

	public ulong DecodeShareCode(string code)
	{
		ulong result = 0uL;
		if (code.Length > 2)
		{
			int num = code[code.Length - 1] - 65;
			code = code.Substring(0, code.Length - 1);
			int num2 = 0;
			string text = code;
			foreach (char c in text)
			{
				num2 += c;
			}
			if (num2 % 26 == num)
			{
				try
				{
					byte[] array = Convert.FromBase64String(code);
					if (array.Length == 8)
					{
						result = BitConverter.ToUInt64(array, 0);
					}
				}
				catch (Exception)
				{
				}
			}
		}
		return result;
	}
}
