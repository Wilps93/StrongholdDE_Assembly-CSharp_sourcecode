using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Stronghold1DE;
using UnityEngine;

public class MapFileManager
{
	private static readonly MapFileManager instance;

	private Thread fileThread;

	private string UserMapsPath;

	private string UserWorkshopPath;

	private string SavesPath;

	private List<string> _fileList = new List<string>();

	private bool mapListBuilt;

	private Dictionary<string, FileHeader> UserInvasion = new Dictionary<string, FileHeader>();

	private Dictionary<string, FileHeader> WorkshopInvasion = new Dictionary<string, FileHeader>();

	private Dictionary<string, FileHeader> BuiltInInvasion = new Dictionary<string, FileHeader>();

	private Dictionary<string, FileHeader> UserSiege = new Dictionary<string, FileHeader>();

	private Dictionary<string, FileHeader> WorkshopSiege = new Dictionary<string, FileHeader>();

	private Dictionary<string, FileHeader> BuiltInSiege = new Dictionary<string, FileHeader>();

	private Dictionary<string, FileHeader> UserEcoMissions = new Dictionary<string, FileHeader>();

	private Dictionary<string, FileHeader> WorkshopEcoMissions = new Dictionary<string, FileHeader>();

	private Dictionary<string, FileHeader> BuiltInEcoMissions = new Dictionary<string, FileHeader>();

	private Dictionary<string, FileHeader> UserFreeBuilds = new Dictionary<string, FileHeader>();

	private Dictionary<string, FileHeader> WorkshopFreeBuilds = new Dictionary<string, FileHeader>();

	private Dictionary<string, FileHeader> BuiltInFreeBuilds = new Dictionary<string, FileHeader>();

	private Dictionary<string, FileHeader> UserMP = new Dictionary<string, FileHeader>();

	private Dictionary<string, FileHeader> WorkshopMP = new Dictionary<string, FileHeader>();

	private Dictionary<string, FileHeader> BuiltInMP = new Dictionary<string, FileHeader>();

	private Dictionary<string, FileHeader> SaveFiles = new Dictionary<string, FileHeader>();

	private Dictionary<string, FileHeader> SaveMPFiles = new Dictionary<string, FileHeader>();

	private Dictionary<string, FileHeader> SiegeThatTempFiles = new Dictionary<string, FileHeader>();

	private Dictionary<string, FileHeader> UserWorkshopUploads = new Dictionary<string, FileHeader>();

	private FileSystemWatcher userMapsWatcher;

	private FileSystemWatcher userSavesWatcher;

	private FileSystemWatcher userWorkshopUploadsWatcher;

	private Texture2D radarPreviewTexture;

	public const int RADARPREVIEW_TEXTURE_SIZE = 200;

	public static MapFileManager Instance => instance;

	static MapFileManager()
	{
		instance = new MapFileManager();
	}

	private MapFileManager()
	{
	}

	public void BuildFileList()
	{
		UserMapsPath = ConfigSettings.GetUserMapsPath();
		UserWorkshopPath = ConfigSettings.GetUserWorkshopPath();
		SavesPath = ConfigSettings.GetSavesPath();
		fileThread = new Thread(runMapLoading);
		fileThread.Name = "StrongholdMapLoading";
		fileThread.Start();
	}

	private void runMapLoading()
	{
		BuildFileList("map");
		CreateWatchers();
		mapListBuilt = true;
	}

	public void BuildFileList(string fileType)
	{
		_fileList.Clear();
		string userMapsPath = UserMapsPath;
		if (!Directory.Exists(userMapsPath))
		{
			Directory.CreateDirectory(userMapsPath);
		}
		string[] files = Directory.GetFiles(userMapsPath, "*." + fileType);
		foreach (string text in files)
		{
			_fileList.Add(text.ToLower());
			UpdateUserMap(text.ToLower());
		}
		files = Directory.GetFiles(userMapsPath, "*.tmp");
		foreach (string text2 in files)
		{
			_fileList.Add(text2.ToLower());
			UpdateUserSiegeThatTemp(text2.ToLower());
		}
		userMapsPath = Path.Combine(Application.streamingAssetsPath, "Maps");
		files = Directory.GetFiles(userMapsPath, "*." + fileType);
		foreach (string text3 in files)
		{
			_fileList.Add(text3.ToLower());
			UpdateBuiltinMap(text3.ToLower());
		}
		try
		{
			foreach (string listOfSubscribedItemsPath in Platform_Workshop.Instance.GetListOfSubscribedItemsPaths())
			{
				files = Directory.GetFiles(listOfSubscribedItemsPath, "*." + fileType);
				foreach (string text4 in files)
				{
					_fileList.Add(text4.ToLower());
					UpdateWorkshopMap(text4.ToLower());
				}
			}
		}
		catch (Exception)
		{
		}
		userMapsPath = UserWorkshopPath;
		files = Directory.GetFiles(userMapsPath, "*.map");
		foreach (string text5 in files)
		{
			_fileList.Add(text5.ToLower());
			UpdateUserWorkshopUploads(text5.ToLower());
		}
		userMapsPath = SavesPath;
		if (!Directory.Exists(userMapsPath))
		{
			Directory.CreateDirectory(userMapsPath);
		}
		files = Directory.GetFiles(userMapsPath, "*.sav");
		foreach (string text6 in files)
		{
			_fileList.Add(text6.ToLower());
			UpdateUserSave(text6.ToLower());
		}
		files = Directory.GetFiles(userMapsPath, "*.msv");
		foreach (string text7 in files)
		{
			_fileList.Add(text7.ToLower());
			UpdateUserMPSave(text7.ToLower());
		}
	}

	private void UpdateUserMap(string file)
	{
		file = file.Replace('/', '\\');
		FileHeader fileInfoFromFileName = GetFileInfoFromFileName(file, 0);
		if (fileInfoFromFileName == null || fileInfoFromFileName.missionMap)
		{
			return;
		}
		if (fileInfoFromFileName.maxPlayers >= 2 && fileInfoFromFileName.mapType == 1)
		{
			UserMP[file] = fileInfoFromFileName;
			return;
		}
		switch (fileInfoFromFileName.scnMissionSiegeOrInvasion)
		{
		case 0:
			UserSiege[file] = fileInfoFromFileName;
			break;
		case 1:
			UserInvasion[file] = fileInfoFromFileName;
			break;
		case 2:
			UserEcoMissions[file] = fileInfoFromFileName;
			break;
		case 3:
			UserFreeBuilds[file] = fileInfoFromFileName;
			break;
		}
	}

	private void RemoveUserMap(string file)
	{
		file = file.Replace('/', '\\');
		if (UserSiege.ContainsKey(file))
		{
			UserSiege.Remove(file);
		}
		if (UserInvasion.ContainsKey(file))
		{
			UserInvasion.Remove(file);
		}
		if (UserEcoMissions.ContainsKey(file))
		{
			UserEcoMissions.Remove(file);
		}
		if (UserFreeBuilds.ContainsKey(file))
		{
			UserFreeBuilds.Remove(file);
		}
		if (UserMP.ContainsKey(file))
		{
			UserMP.Remove(file);
		}
	}

	private void UpdateWorkshopMap(string file)
	{
		file = file.Replace('/', '\\');
		FileHeader fileInfoFromFileName = GetFileInfoFromFileName(file, 2);
		if (fileInfoFromFileName == null || fileInfoFromFileName.missionMap)
		{
			return;
		}
		if (fileInfoFromFileName.maxPlayers >= 2 && fileInfoFromFileName.mapType == 1)
		{
			WorkshopMP[file] = fileInfoFromFileName;
			return;
		}
		switch (fileInfoFromFileName.scnMissionSiegeOrInvasion)
		{
		case 0:
			WorkshopSiege[file] = fileInfoFromFileName;
			break;
		case 1:
			WorkshopInvasion[file] = fileInfoFromFileName;
			break;
		case 2:
			WorkshopEcoMissions[file] = fileInfoFromFileName;
			break;
		case 3:
			WorkshopFreeBuilds[file] = fileInfoFromFileName;
			break;
		}
	}

	private void RemoveWorkshopMap(string file)
	{
		file = file.Replace('/', '\\');
		if (WorkshopSiege.ContainsKey(file))
		{
			WorkshopSiege.Remove(file);
		}
		if (WorkshopInvasion.ContainsKey(file))
		{
			WorkshopInvasion.Remove(file);
		}
		if (WorkshopEcoMissions.ContainsKey(file))
		{
			WorkshopEcoMissions.Remove(file);
		}
		if (WorkshopFreeBuilds.ContainsKey(file))
		{
			WorkshopFreeBuilds.Remove(file);
		}
		if (WorkshopMP.ContainsKey(file))
		{
			WorkshopMP.Remove(file);
		}
	}

	private void UpdateBuiltinMap(string file)
	{
		file = file.Replace('/', '\\');
		FileHeader fileInfoFromFileName = GetFileInfoFromFileName(file, 1);
		if (fileInfoFromFileName == null || fileInfoFromFileName.missionMap)
		{
			return;
		}
		if (fileInfoFromFileName.maxPlayers >= 2 && fileInfoFromFileName.mapType == 1)
		{
			BuiltInMP[file] = fileInfoFromFileName;
			return;
		}
		switch (fileInfoFromFileName.scnMissionSiegeOrInvasion)
		{
		case 0:
			BuiltInSiege[file] = fileInfoFromFileName;
			break;
		case 1:
			BuiltInInvasion[file] = fileInfoFromFileName;
			break;
		case 2:
			BuiltInEcoMissions[file] = fileInfoFromFileName;
			break;
		case 3:
			BuiltInFreeBuilds[file] = fileInfoFromFileName;
			break;
		}
	}

	private void UpdateUserSiegeThatTemp(string file)
	{
		file = file.Replace('/', '\\');
		FileHeader fileInfoFromFileName = GetFileInfoFromFileName(file, 0);
		if (fileInfoFromFileName != null)
		{
			SiegeThatTempFiles[file] = fileInfoFromFileName;
		}
	}

	private void RemoveUserSiegeThatTemp(string file)
	{
		file = file.Replace('/', '\\');
		if (SiegeThatTempFiles.ContainsKey(file))
		{
			SiegeThatTempFiles.Remove(file);
		}
	}

	private void UpdateUserWorkshopUploads(string file)
	{
		file = file.Replace('/', '\\');
		FileHeader fileInfoFromFileName = GetFileInfoFromFileName(file, 0);
		if (fileInfoFromFileName == null)
		{
			return;
		}
		string path = file.Replace(".map", ".data");
		try
		{
			string[] array = File.ReadAllLines(path);
			if (array != null && array.Length >= 2)
			{
				UserWorkshopUploads[file] = fileInfoFromFileName;
			}
		}
		catch (Exception)
		{
		}
	}

	private void RemoveUserWorkshopUploads(string file)
	{
		file = file.Replace('/', '\\');
		if (UserWorkshopUploads.ContainsKey(file))
		{
			UserWorkshopUploads.Remove(file);
		}
	}

	private void UpdateUserSave(string file)
	{
		file = file.Replace('/', '\\');
		FileHeader fileInfoFromFileName = GetFileInfoFromFileName(file, 0);
		if (fileInfoFromFileName != null)
		{
			SaveFiles[file] = fileInfoFromFileName;
		}
	}

	private void UpdateUserMPSave(string file)
	{
		file = file.Replace('/', '\\');
		FileHeader fileInfoFromFileName = GetFileInfoFromFileName(file, 0);
		if (fileInfoFromFileName != null)
		{
			SaveMPFiles[file] = fileInfoFromFileName;
		}
	}

	private void RemoveUserSave(string file)
	{
		file = file.Replace('/', '\\');
		if (SaveFiles.ContainsKey(file))
		{
			SaveFiles.Remove(file);
		}
	}

	private void RemoveUserMPSave(string file)
	{
		file = file.Replace('/', '\\');
		if (SaveMPFiles.ContainsKey(file))
		{
			SaveMPFiles.Remove(file);
		}
	}

	private void CreateWatchers()
	{
		string userMapsPath = UserMapsPath;
		userMapsWatcher = new FileSystemWatcher(userMapsPath);
		userMapsWatcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
		userMapsWatcher.Changed += OnChangedMap;
		userMapsWatcher.Created += OnCreatedMap;
		userMapsWatcher.Deleted += OnDeletedMap;
		userMapsWatcher.Renamed += OnRenamedMap;
		userMapsWatcher.Filter = "*";
		userMapsWatcher.IncludeSubdirectories = false;
		userMapsWatcher.EnableRaisingEvents = true;
		userMapsPath = SavesPath;
		userSavesWatcher = new FileSystemWatcher(userMapsPath);
		userSavesWatcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
		userSavesWatcher.Changed += OnChangedSave;
		userSavesWatcher.Created += OnCreatedSave;
		userSavesWatcher.Deleted += OnDeletedSave;
		userSavesWatcher.Renamed += OnRenamedSave;
		userSavesWatcher.Filter = "*";
		userSavesWatcher.IncludeSubdirectories = false;
		userSavesWatcher.EnableRaisingEvents = true;
		userMapsPath = UserWorkshopPath;
		userWorkshopUploadsWatcher = new FileSystemWatcher(userMapsPath);
		userWorkshopUploadsWatcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
		userWorkshopUploadsWatcher.Changed += OnChangedWS;
		userWorkshopUploadsWatcher.Created += OnCreatedWS;
		userWorkshopUploadsWatcher.Deleted += OnDeletedWS;
		userWorkshopUploadsWatcher.Renamed += OnRenamedWS;
		userWorkshopUploadsWatcher.Filter = "*";
		userWorkshopUploadsWatcher.IncludeSubdirectories = false;
		userWorkshopUploadsWatcher.EnableRaisingEvents = true;
	}

	private void OnChangedMap(object sender, FileSystemEventArgs e)
	{
		if (e.ChangeType == WatcherChangeTypes.Changed)
		{
			if (e.FullPath.ToLower().EndsWith(".map"))
			{
				UpdateUserMap(e.FullPath.ToLower());
			}
			else if (e.FullPath.ToLower().EndsWith(".tmp"))
			{
				UpdateUserSiegeThatTemp(e.FullPath.ToLower());
			}
		}
	}

	private void OnCreatedMap(object sender, FileSystemEventArgs e)
	{
		if (e.FullPath.ToLower().EndsWith(".map"))
		{
			UpdateUserMap(e.FullPath.ToLower());
		}
		else if (e.FullPath.ToLower().EndsWith(".tmp"))
		{
			UpdateUserSiegeThatTemp(e.FullPath.ToLower());
		}
	}

	private void OnDeletedMap(object sender, FileSystemEventArgs e)
	{
		if (e.FullPath.ToLower().EndsWith(".map"))
		{
			RemoveUserMap(e.FullPath.ToLower());
		}
		else if (e.FullPath.ToLower().EndsWith(".tmp"))
		{
			RemoveUserSiegeThatTemp(e.FullPath.ToLower());
		}
	}

	private void OnRenamedMap(object sender, RenamedEventArgs e)
	{
		if (e.OldFullPath.ToLower().EndsWith(".map"))
		{
			RemoveUserMap(e.OldFullPath.ToLower());
		}
		else if (e.OldFullPath.ToLower().EndsWith(".tmp"))
		{
			RemoveUserSiegeThatTemp(e.OldFullPath.ToLower());
		}
		if (e.FullPath.ToLower().EndsWith(".map"))
		{
			UpdateUserMap(e.FullPath.ToLower());
		}
		else if (e.FullPath.ToLower().EndsWith(".tmp"))
		{
			UpdateUserSiegeThatTemp(e.FullPath.ToLower());
		}
	}

	private void OnChangedSave(object sender, FileSystemEventArgs e)
	{
		if (e.ChangeType == WatcherChangeTypes.Changed)
		{
			if (e.FullPath.ToLower().EndsWith(".sav"))
			{
				UpdateUserSave(e.FullPath.ToLower());
			}
			else if (e.FullPath.ToLower().EndsWith(".msv"))
			{
				UpdateUserMPSave(e.FullPath.ToLower());
			}
		}
	}

	private void OnCreatedSave(object sender, FileSystemEventArgs e)
	{
		if (e.FullPath.ToLower().EndsWith(".sav"))
		{
			UpdateUserSave(e.FullPath.ToLower());
		}
		else if (e.FullPath.ToLower().EndsWith(".msv"))
		{
			UpdateUserMPSave(e.FullPath.ToLower());
		}
	}

	private void OnDeletedSave(object sender, FileSystemEventArgs e)
	{
		if (e.FullPath.ToLower().EndsWith(".sav"))
		{
			RemoveUserSave(e.FullPath.ToLower());
		}
		else if (e.FullPath.ToLower().EndsWith(".msv"))
		{
			RemoveUserSave(e.FullPath.ToLower());
		}
	}

	private void OnRenamedSave(object sender, RenamedEventArgs e)
	{
		if (e.OldFullPath.ToLower().EndsWith(".sav"))
		{
			RemoveUserSave(e.OldFullPath.ToLower());
		}
		else if (e.OldFullPath.ToLower().EndsWith(".msv"))
		{
			RemoveUserMPSave(e.OldFullPath.ToLower());
		}
		if (e.FullPath.ToLower().EndsWith(".sav"))
		{
			UpdateUserSave(e.FullPath.ToLower());
		}
		else if (e.FullPath.ToLower().EndsWith(".msv"))
		{
			UpdateUserSave(e.FullPath.ToLower());
		}
	}

	private void OnChangedWS(object sender, FileSystemEventArgs e)
	{
		if (e.ChangeType == WatcherChangeTypes.Changed && e.FullPath.ToLower().EndsWith(".map"))
		{
			UpdateUserWorkshopUploads(e.FullPath.ToLower());
		}
	}

	private void OnCreatedWS(object sender, FileSystemEventArgs e)
	{
		if (e.FullPath.ToLower().EndsWith(".map"))
		{
			UpdateUserWorkshopUploads(e.FullPath.ToLower());
		}
	}

	private void OnDeletedWS(object sender, FileSystemEventArgs e)
	{
		if (e.FullPath.ToLower().EndsWith(".map"))
		{
			RemoveUserWorkshopUploads(e.FullPath.ToLower());
		}
	}

	private void OnRenamedWS(object sender, RenamedEventArgs e)
	{
		if (e.OldFullPath.ToLower().EndsWith(".map"))
		{
			RemoveUserWorkshopUploads(e.OldFullPath.ToLower());
		}
		if (e.FullPath.ToLower().EndsWith(".map"))
		{
			UpdateUserWorkshopUploads(e.FullPath.ToLower());
		}
	}

	private List<FileHeader> SortList(List<FileHeader> list, bool sortByName, bool sortAscend)
	{
		if (sortByName)
		{
			if (sortAscend)
			{
				list.Sort((FileHeader x, FileHeader y) => x.sortFileName.CompareTo(y.sortFileName));
			}
			else
			{
				list.Sort((FileHeader x, FileHeader y) => y.sortFileName.CompareTo(x.sortFileName));
			}
		}
		else if (sortAscend)
		{
			list.Sort((FileHeader x, FileHeader y) => x.written.CompareTo(y.written));
		}
		else
		{
			list.Sort((FileHeader x, FileHeader y) => y.written.CompareTo(x.written));
		}
		return list;
	}

	private List<FileHeader> SortList(List<FileHeader> list, int sortMode, bool sortAscend)
	{
		switch (sortMode)
		{
		case 0:
			if (sortAscend)
			{
				list.Sort((FileHeader x, FileHeader y) => x.sortFileName.CompareTo(y.sortFileName));
			}
			else
			{
				list.Sort((FileHeader x, FileHeader y) => y.sortFileName.CompareTo(x.sortFileName));
			}
			break;
		case 1:
			if (sortAscend)
			{
				list.Sort((FileHeader x, FileHeader y) => x.written.CompareTo(y.written));
			}
			else
			{
				list.Sort((FileHeader x, FileHeader y) => y.written.CompareTo(x.written));
			}
			break;
		case 2:
			if (sortAscend)
			{
				list.Sort((FileHeader x, FileHeader y) => x.maxPlayers.CompareTo(y.maxPlayers));
			}
			else
			{
				list.Sort((FileHeader x, FileHeader y) => y.maxPlayers.CompareTo(x.maxPlayers));
			}
			break;
		}
		return list;
	}

	public List<FileHeader> GetUserSiegeThatTempMaps(bool sortByName, bool sortAscend)
	{
		List<FileHeader> list = new List<FileHeader>();
		foreach (KeyValuePair<string, FileHeader> siegeThatTempFile in SiegeThatTempFiles)
		{
			list.Add(siegeThatTempFile.Value);
		}
		return SortList(list, sortByName, sortAscend);
	}

	public List<FileHeader> GetUserWorkshopUploads(bool sortByName, bool sortAscend)
	{
		List<FileHeader> list = new List<FileHeader>();
		foreach (KeyValuePair<string, FileHeader> userWorkshopUpload in UserWorkshopUploads)
		{
			list.Add(userWorkshopUpload.Value);
		}
		return SortList(list, sortByName, sortAscend);
	}

	public List<FileHeader> GetMultiplayerMaps(int sortMode, bool sortAscend, int numberOfPlayersMin, bool includeBuiltIn, bool includeUser, bool includeWorkshop)
	{
		List<FileHeader> list = new List<FileHeader>();
		if (includeUser)
		{
			foreach (KeyValuePair<string, FileHeader> item in UserMP)
			{
				if (item.Value.maxPlayers >= numberOfPlayersMin)
				{
					list.Add(item.Value);
				}
			}
		}
		if (includeBuiltIn)
		{
			foreach (KeyValuePair<string, FileHeader> item2 in BuiltInMP)
			{
				if (item2.Value.maxPlayers >= numberOfPlayersMin)
				{
					list.Add(item2.Value);
				}
			}
		}
		if (includeWorkshop)
		{
			foreach (KeyValuePair<string, FileHeader> item3 in WorkshopMP)
			{
				if (item3.Value.maxPlayers >= numberOfPlayersMin)
				{
					list.Add(item3.Value);
				}
			}
		}
		return SortList(list, sortMode, sortAscend);
	}

	public List<FileHeader> GetSiegeMaps(bool sortByName, bool sortAscend, bool includeBuiltIn, bool includeUser, bool includeWorkshop, bool siegeThat = false)
	{
		List<FileHeader> list = new List<FileHeader>();
		if (includeUser)
		{
			foreach (KeyValuePair<string, FileHeader> item in UserSiege)
			{
				if (item.Value.isSiegeThat > 0 == siegeThat)
				{
					list.Add(item.Value);
				}
			}
		}
		if (includeBuiltIn)
		{
			foreach (KeyValuePair<string, FileHeader> item2 in BuiltInSiege)
			{
				if (item2.Value.isSiegeThat > 0 == siegeThat)
				{
					list.Add(item2.Value);
				}
			}
		}
		if (includeWorkshop)
		{
			foreach (KeyValuePair<string, FileHeader> item3 in WorkshopSiege)
			{
				if (item3.Value.isSiegeThat > 0 == siegeThat)
				{
					list.Add(item3.Value);
				}
			}
		}
		return SortList(list, sortByName, sortAscend);
	}

	public List<FileHeader> GetInvasionMaps(bool sortByName, bool sortAscend, bool includeBuiltIn, bool includeUser, bool includeWorkshop)
	{
		List<FileHeader> list = new List<FileHeader>();
		if (includeUser)
		{
			foreach (KeyValuePair<string, FileHeader> item in UserInvasion)
			{
				list.Add(item.Value);
			}
		}
		if (includeBuiltIn)
		{
			foreach (KeyValuePair<string, FileHeader> item2 in BuiltInInvasion)
			{
				list.Add(item2.Value);
			}
		}
		if (includeWorkshop)
		{
			foreach (KeyValuePair<string, FileHeader> item3 in WorkshopInvasion)
			{
				list.Add(item3.Value);
			}
		}
		return SortList(list, sortByName, sortAscend);
	}

	public List<FileHeader> GetEcoMaps(bool sortByName, bool sortAscend, bool includeBuiltIn, bool includeUser, bool includeWorkshop)
	{
		List<FileHeader> list = new List<FileHeader>();
		if (includeUser)
		{
			foreach (KeyValuePair<string, FileHeader> userEcoMission in UserEcoMissions)
			{
				list.Add(userEcoMission.Value);
			}
		}
		if (includeBuiltIn)
		{
			foreach (KeyValuePair<string, FileHeader> builtInEcoMission in BuiltInEcoMissions)
			{
				list.Add(builtInEcoMission.Value);
			}
		}
		if (includeWorkshop)
		{
			foreach (KeyValuePair<string, FileHeader> workshopEcoMission in WorkshopEcoMissions)
			{
				list.Add(workshopEcoMission.Value);
			}
		}
		return SortList(list, sortByName, sortAscend);
	}

	public List<FileHeader> GetFreebuildMaps(bool sortByName, bool sortAscend, bool includeBuiltIn, bool includeUser, bool includeWorkshop)
	{
		List<FileHeader> list = new List<FileHeader>();
		if (includeUser)
		{
			foreach (KeyValuePair<string, FileHeader> userFreeBuild in UserFreeBuilds)
			{
				list.Add(userFreeBuild.Value);
			}
		}
		if (includeBuiltIn)
		{
			foreach (KeyValuePair<string, FileHeader> builtInFreeBuild in BuiltInFreeBuilds)
			{
				list.Add(builtInFreeBuild.Value);
			}
		}
		if (includeWorkshop)
		{
			foreach (KeyValuePair<string, FileHeader> workshopFreeBuild in WorkshopFreeBuilds)
			{
				list.Add(workshopFreeBuild.Value);
			}
		}
		return SortList(list, sortByName, sortAscend);
	}

	public List<FileHeader> GetSaves(bool sortByName, bool sortAscend, bool excludeQuicksaves = false)
	{
		List<FileHeader> list = new List<FileHeader>();
		foreach (KeyValuePair<string, FileHeader> saveFile in SaveFiles)
		{
			if (!excludeQuicksaves || !saveFile.Value.display_filename.ToLowerInvariant().StartsWith("quicksave 20"))
			{
				list.Add(saveFile.Value);
			}
		}
		return SortList(list, sortByName, sortAscend);
	}

	public List<FileHeader> GetMPSaves(bool sortByName, bool sortAscend)
	{
		List<FileHeader> list = new List<FileHeader>();
		foreach (KeyValuePair<string, FileHeader> saveMPFile in SaveMPFiles)
		{
			list.Add(saveMPFile.Value);
		}
		return SortList(list, sortByName, sortAscend);
	}

	public List<FileHeader> GetMapEditableMaps(bool sortByName, bool sortAscend)
	{
		List<FileHeader> list = new List<FileHeader>();
		foreach (KeyValuePair<string, FileHeader> userFreeBuild in UserFreeBuilds)
		{
			if (userFreeBuild.Value.isMapEditable())
			{
				list.Add(userFreeBuild.Value);
			}
		}
		foreach (KeyValuePair<string, FileHeader> userEcoMission in UserEcoMissions)
		{
			if (userEcoMission.Value.isMapEditable())
			{
				list.Add(userEcoMission.Value);
			}
		}
		foreach (KeyValuePair<string, FileHeader> item in UserSiege)
		{
			if (item.Value.isMapEditable() && item.Value.isSiegeThat == 0)
			{
				list.Add(item.Value);
			}
		}
		foreach (KeyValuePair<string, FileHeader> item2 in UserInvasion)
		{
			if (item2.Value.isMapEditable())
			{
				list.Add(item2.Value);
			}
		}
		foreach (KeyValuePair<string, FileHeader> item3 in UserMP)
		{
			if (item3.Value.isMapEditable())
			{
				list.Add(item3.Value);
			}
		}
		return SortList(list, sortByName, sortAscend);
	}

	public List<FileHeader> GetAllUserMapsForfilenameCheck()
	{
		List<FileHeader> list = new List<FileHeader>();
		foreach (KeyValuePair<string, FileHeader> userFreeBuild in UserFreeBuilds)
		{
			list.Add(userFreeBuild.Value);
		}
		foreach (KeyValuePair<string, FileHeader> userEcoMission in UserEcoMissions)
		{
			list.Add(userEcoMission.Value);
		}
		foreach (KeyValuePair<string, FileHeader> item in UserSiege)
		{
			list.Add(item.Value);
		}
		foreach (KeyValuePair<string, FileHeader> item2 in UserInvasion)
		{
			list.Add(item2.Value);
		}
		foreach (KeyValuePair<string, FileHeader> item3 in UserMP)
		{
			list.Add(item3.Value);
		}
		return list;
	}

	public FileHeader GetHeaderFromFileNameMP(string fileName)
	{
		FileHeader fileHeader = FindFileFromList(fileName, BuiltInMP);
		if (fileHeader != null)
		{
			return fileHeader;
		}
		fileHeader = FindFileFromList(fileName, WorkshopMP);
		if (fileHeader != null)
		{
			return fileHeader;
		}
		fileHeader = FindFileFromList(fileName, UserMP);
		if (fileHeader != null)
		{
			return fileHeader;
		}
		return null;
	}

	public FileHeader GetHeaderFromFileNameMP(string fileName, int crc)
	{
		FileHeader fileHeader = FindFileFromList(fileName, BuiltInMP);
		if (fileHeader != null && fileHeader.crc == crc)
		{
			return fileHeader;
		}
		fileHeader = FindFileFromList(fileName, WorkshopMP);
		if (fileHeader != null && fileHeader.crc == crc)
		{
			return fileHeader;
		}
		fileHeader = FindFileFromList(fileName, UserMP);
		if (fileHeader != null && fileHeader.crc == crc)
		{
			return fileHeader;
		}
		return null;
	}

	public FileHeader GetHeaderFromMpSaveFileName(string fileName)
	{
		FileHeader fileHeader = FindFileFromList(fileName, SaveMPFiles);
		if (fileHeader != null)
		{
			return fileHeader;
		}
		return null;
	}

	private FileHeader FindFileFromList(string fileName, Dictionary<string, FileHeader> files)
	{
		string text = fileName.ToLower();
		foreach (KeyValuePair<string, FileHeader> file in files)
		{
			if (file.Value.fileName.ToLower() == text)
			{
				return file.Value;
			}
		}
		return null;
	}

	public FileHeader GetFileInfoFromFileName(string filePath, int folderType)
	{
		int num = 0;
		try
		{
			FileHeader fileHeader = new FileHeader();
			switch (folderType)
			{
			case 0:
				fileHeader.userMap = true;
				break;
			case 1:
				fileHeader.builtinMap = true;
				break;
			case 2:
				fileHeader.workshopMap = true;
				break;
			}
			DateTime creationTime = File.GetCreationTime(filePath);
			fileHeader.created = creationTime;
			DateTime lastWriteTime = File.GetLastWriteTime(filePath);
			fileHeader.written = lastWriteTime;
			fileHeader.showAlternateMissionTextForBriefing = false;
			fileHeader.achFood = 0;
			fileHeader.achWood = 0;
			fileHeader.achWeapons = 0;
			using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
			if (fileStream.Length < 10000 || fileStream.Length > 5000000)
			{
				return null;
			}
			byte[] array = new byte[fileStream.Length];
			int num2 = (fileHeader.length = (int)fileStream.Length);
			int num3 = 0;
			while (num2 > 0)
			{
				int num4 = fileStream.Read(array, num3, num2);
				if (num4 == 0)
				{
					break;
				}
				num3 += num4;
				num2 -= num4;
			}
			fileHeader.crc = EngineInterface.crc(array);
			fileHeader.headerID = BitConverter.ToInt32(array, num);
			num += 4;
			if (fileHeader.headerID >= 0)
			{
				return null;
			}
			fileHeader.radarMapCompressedSize = BitConverter.ToInt32(array, num);
			if (fileHeader.radarMapCompressedSize > 0)
			{
				num += 4;
				int num5 = BitConverter.ToInt32(array, num + 4);
				bool flag = false;
				if (num5 + 12 != fileHeader.radarMapCompressedSize)
				{
					flag = true;
				}
				num += fileHeader.radarMapCompressedSize;
				int num6 = num;
				int num7 = BitConverter.ToInt32(array, num);
				num += 4;
				if (num7 > 0)
				{
					fileHeader.missionTextType = BitConverter.ToInt32(array, num);
					num += 4;
					fileHeader.missionTextNumber = BitConverter.ToInt32(array, num);
					num += 4;
					fileHeader.utf8MissionText = "";
					if (fileHeader.missionTextType == 0 && fileHeader.missionTextNumber == 1234567 && flag)
					{
						int num8 = BitConverter.ToInt32(array, num6 - 4);
						fileHeader.utf8MissionText = Encoding.UTF8.GetString(array, num6 - 4 - num8, num8);
					}
					byte[] array2 = new byte[num7 - 8];
					Buffer.BlockCopy(array, num, array2, 0, num7 - 8);
					byte[] array3 = EngineInterface.unpack(array2);
					if (array3 != null && array3.Length != 0)
					{
						byte[] bytes = removeTrailingZerosAnsi(array3);
						try
						{
							fileHeader.ansiMissionText = Encoding.ASCII.GetString(bytes);
						}
						catch (Exception)
						{
							fileHeader.ansiMissionText = "";
						}
						byte[] bytes2 = removeTrailingZerosUnicode(array3);
						try
						{
							fileHeader.unicodeMissionText = Encoding.Unicode.GetString(bytes2);
						}
						catch (Exception)
						{
							fileHeader.unicodeMissionText = "";
						}
						if (fileHeader.utf8MissionText.Length == 0)
						{
							if (fileHeader.ansiMissionText.Length > 0 && fileHeader.unicodeMissionText.Length > 0)
							{
								fileHeader.showAlternateMissionTextForBriefing = true;
							}
							if (fileHeader.ansiMissionText.Length > 0)
							{
								fileHeader.utf8MissionText = fileHeader.ansiMissionText;
							}
							else if (fileHeader.unicodeMissionText.Length > 0)
							{
								fileHeader.utf8MissionText = fileHeader.unicodeMissionText;
							}
						}
					}
					num += num7 - 8;
					num7 = BitConverter.ToInt32(array, num);
					num += 4;
					if (num7 > 0)
					{
						fileHeader.xPlaySaveTime = BitConverter.ToInt32(array, num);
						num += 4;
						fileHeader.xPlaySaveChecksum = BitConverter.ToInt32(array, num);
						num += 4;
						num7 = BitConverter.ToInt32(array, num);
						if (num7 > 0)
						{
							num += 4;
							fileHeader.mapType = BitConverter.ToInt32(array, num);
							if (fileHeader.mapType <= -200)
							{
								fileHeader.trailID = -200 - fileHeader.mapType;
								fileHeader.mapType = 0;
								fileHeader.trail = 2;
							}
							else if (fileHeader.mapType <= -100)
							{
								fileHeader.trailID = -100 - fileHeader.mapType;
								fileHeader.mapType = 0;
								fileHeader.trail = 1;
							}
							num += 4;
							fileHeader.mapKeeps[0] = BitConverter.ToInt32(array, num);
							num += 4;
							fileHeader.mapKeeps[1] = BitConverter.ToInt32(array, num);
							num += 4;
							fileHeader.mapKeeps[2] = BitConverter.ToInt32(array, num);
							num += 4;
							fileHeader.mapKeeps[3] = BitConverter.ToInt32(array, num);
							num += 4;
							fileHeader.mapKeeps[4] = BitConverter.ToInt32(array, num);
							num += 4;
							fileHeader.maxPlayers = BitConverter.ToInt32(array, num);
							num += 4;
							num7 = BitConverter.ToInt32(array, num);
							num += 4;
							if (num7 > 0)
							{
								fileHeader.scnMissionType = BitConverter.ToInt32(array, num);
								num += 4;
								if (num7 > 4)
								{
									fileHeader.scnMissionSiegeOrInvasion = BitConverter.ToInt32(array, num);
									num += 4;
									if (num7 > 8)
									{
										fileHeader.missionLockType = BitConverter.ToInt32(array, num);
										num += 4;
										if (num7 > 12)
										{
											int num9 = num7 - 12;
											if (num9 > 80)
											{
												num9 = 80;
											}
											for (int i = 0; i < num9; i++)
											{
												if (array[num + i] == 0)
												{
													num9 = i;
													break;
												}
											}
											if (num9 > 0)
											{
												byte[] array4 = new byte[num9];
												Buffer.BlockCopy(array, num, array4, 0, num9);
												fileHeader.standAlone_filename = Encoding.ASCII.GetString(array4);
											}
											if (num7 > 200)
											{
												fileHeader.achFood = BitConverter.ToInt16(array, num + 200 - 12 - 6);
												fileHeader.achWeapons = BitConverter.ToInt16(array, num + 200 - 12 - 4);
												fileHeader.achWood = BitConverter.ToInt16(array, num + 200 - 12 - 2);
												int num10 = BitConverter.ToInt32(array, num + 200 - 12);
												if (num10 > 0)
												{
													byte[] array5 = new byte[num10];
													Buffer.BlockCopy(array, num + 204 - 12, array5, 0, num10);
													fileHeader.standAlone_filename = Encoding.UTF8.GetString(array5);
												}
											}
											num += num7 - 12;
										}
									}
								}
								num7 = BitConverter.ToInt32(array, num);
								num += 4;
								if (num7 > 0)
								{
									fileHeader.isKingOfTheHill = BitConverter.ToInt32(array, num);
									if (fileHeader.isKingOfTheHill > 0)
									{
										fileHeader.maxPlayers--;
									}
									num += 4;
									if (num7 > 4)
									{
										fileHeader.isSiegeThat = BitConverter.ToInt32(array, num);
										num += 4;
										if (num7 > 8)
										{
											fileHeader.xPlayAutoSave = BitConverter.ToInt32(array, num);
											num += 4;
										}
									}
								}
							}
						}
					}
					string text = (fileHeader.fileName = Path.GetFileNameWithoutExtension(filePath));
					fileHeader.filePath = filePath;
					if (fileHeader.builtinMap)
					{
						fileHeader.display_filename = Translate.Instance.translateMapNames(text);
					}
					else
					{
						fileHeader.display_filename = text;
					}
					if (fileHeader.standAlone_filename == "")
					{
						fileHeader.standAlone_filename = text;
					}
					fileHeader.sortFileName = fileHeader.display_filename.ToLowerInvariant();
					fileHeader.missionMap = false;
					fileHeader.missionMap = fileHeader.missionLockType == 2;
					if (!fileHeader.missionMap && text.ToLowerInvariant().StartsWith("mission"))
					{
						if (text.Length == 8)
						{
							if (char.IsDigit(text[7]))
							{
								fileHeader.missionMap = true;
							}
						}
						else if (text.Length == 9 && char.IsDigit(text[7]) && char.IsDigit(text[8]))
						{
							fileHeader.missionMap = true;
						}
					}
					if (filePath.ToLower().EndsWith(".sav"))
					{
						fileHeader.mission_level = EngineInterface.GetCampaignLevel(filePath);
					}
					else
					{
						fileHeader.mission_level = -1;
					}
					return fileHeader;
				}
				return null;
			}
			return null;
		}
		catch (Exception)
		{
		}
		return null;
	}

	private byte[] removeTrailingZerosAnsi(byte[] data)
	{
		int num = data.Length;
		for (int i = 0; i < data.Length; i++)
		{
			if (data[i] == 0)
			{
				num = i;
				break;
			}
		}
		byte[] array = new byte[num];
		for (int j = 0; j < num; j++)
		{
			array[j] = data[j];
		}
		return array;
	}

	private byte[] removeTrailingZerosUnicode(byte[] data)
	{
		int num = data.Length;
		for (int i = 0; i < data.Length; i += 2)
		{
			if (data[i] == 0 && i + 1 < data.Length && data[i + 1] == 0)
			{
				num = i;
				break;
			}
		}
		byte[] array = new byte[num];
		for (int j = 0; j < num; j++)
		{
			array[j] = data[j];
		}
		return array;
	}

	private void GetFileRadarFromListEntry(int thisEntry)
	{
		string path = _fileList[thisEntry];
		GetRadarFromFile(path);
	}

	public byte[] GetRadarFromFile(string path)
	{
		int num = 0;
		try
		{
			using FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
			byte[] array = new byte[fileStream.Length];
			int num2 = (int)fileStream.Length;
			int num3 = 0;
			while (num2 > 0)
			{
				int num4 = fileStream.Read(array, num3, num2);
				if (num4 == 0)
				{
					break;
				}
				num3 += num4;
				num2 -= num4;
			}
			int num5 = BitConverter.ToInt32(array, num);
			num += 4;
			if (num5 >= 0)
			{
				return null;
			}
			int num6 = BitConverter.ToInt32(array, num);
			num += 4;
			if (num6 <= 0)
			{
				return null;
			}
			byte[] array2 = new byte[num6];
			Array.Copy(array, num, array2, 0, num6);
			return EngineInterface.unpackSavedRadar(array2);
		}
		catch (Exception)
		{
		}
		return null;
	}

	public Texture2D GetRadarPreview(byte[] radarMapPreview)
	{
		if (radarPreviewTexture == null)
		{
			radarPreviewTexture = new Texture2D(200, 200, TextureFormat.BGRA32, mipChain: false);
			radarPreviewTexture.filterMode = FilterMode.Point;
		}
		radarPreviewTexture.SetPixelData(radarMapPreview, 0);
		radarPreviewTexture.Apply();
		return radarPreviewTexture;
	}

	public void clearMPCRCChecks()
	{
		foreach (FileHeader mPSafe in GetMPSaves(sortByName: true, sortAscend: true))
		{
			mPSafe.retrieveCRCChecks = 0;
		}
	}
}
