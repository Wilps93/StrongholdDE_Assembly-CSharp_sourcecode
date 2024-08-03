using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Noesis;
using UnityEngine;

namespace Stronghold1DE;

public class HUD_LoadSaveRequester : UserControl
{
	private WGT_Heading RefHeading;

	private Noesis.Grid RefViewRoot;

	private ListView RefFileLists;

	private TextBox RefFileName;

	private TextBox RefSearchFilter;

	private Noesis.Grid RefRadarGrid;

	private Button RefActionButton;

	private CheckBox RefLockMapCheck;

	private CheckBox RefHideQuicksaveCheck;

	private Button RefButtonOpenFolder;

	private bool loadRequester;

	private bool saveNotMapRequester;

	private FileHeader selectedHeader;

	private string rememberedSaveName = "";

	private string rememberedMapName = "";

	private static HUD_LoadSaveRequester instance1;

	private static HUD_LoadSaveRequester instance2;

	private static HUD_LoadSaveRequester instance3;

	public static bool siegeThatCreateLockedMap;

	private static int MPCrcCount;

	private bool panelActive;

	private DateTime lastScrollTest = DateTime.MinValue;

	private ObservableCollection<FileRow> rows = new ObservableCollection<FileRow>();

	private Action<string, FileHeader> OKAction;

	private Action CancelAction;

	private Enums.RequesterTypes requesterType;

	private int sortByColumn;

	private bool sortByAscending = true;

	private List<FileHeader> headerlist;

	public HUD_LoadSaveRequester()
	{
		InitializeComponent();
		if (instance1 == null)
		{
			instance1 = this;
		}
		else if (instance2 == null)
		{
			instance2 = this;
		}
		else if (instance3 == null)
		{
			instance3 = this;
		}
		RefHeading = (WGT_Heading)FindName("RequesterHeader");
		RefViewRoot = (Noesis.Grid)FindName("LayoutRoot");
		RefFileLists = (ListView)FindName("FileList");
		RefActionButton = (Button)FindName("ButtonAction");
		RefButtonOpenFolder = (Button)FindName("ButtonOpenFolder");
		RefFileName = (TextBox)FindName("FileName");
		RefFileName.IsKeyboardFocusedChanged += TextInputFocus;
		RefFileName.TextChanged += TextChangedHandler;
		RefFileName.Loaded += TextBoxLoaded;
		RefFileName.PreviewTextInput += FileNameValidationTextBox;
		RefFileName.PreviewKeyDown += TextBoxCheckForEscape;
		RefSearchFilter = (TextBox)FindName("SearchFilter");
		RefSearchFilter.IsKeyboardFocusedChanged += FilterTextInputFocus;
		RefSearchFilter.TextChanged += FilterTextChangedHandler;
		RefSearchFilter.PreviewKeyDown += TextBoxCheckForEscape;
		RefSearchFilter.PreviewTextInput += TextBoxEnterCheck;
		RefLockMapCheck = (CheckBox)FindName("LockMapCheck");
		RefHideQuicksaveCheck = (CheckBox)FindName("HideQuicksaveCheck");
		RefHideQuicksaveCheck.Checked += QuickSave_ValueChanged;
		RefHideQuicksaveCheck.Unchecked += QuickSave_ValueChanged;
		RefRadarGrid = (Noesis.Grid)FindName("RadarRequesterGrid");
		RefFileLists.MouseDoubleClick += delegate
		{
			if (RefFileLists.SelectedItem != null && loadRequester)
			{
				FileHeader fileHeader3 = ((FileRow)RefFileLists.SelectedItem).fileHeader;
				if (fileHeader3 != null)
				{
					updateRadarTexture(fileHeader3);
					selectedHeader = fileHeader3;
					RefActionButton.IsEnabled = true;
					ButtonClicked(1, fromDoubleClick: true);
				}
			}
		};
		GridView obj = (GridView)RefFileLists.View;
		GridViewColumnHeader obj2 = (GridViewColumnHeader)obj.Columns[0].Header;
		obj2.Content = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 27);
		obj2.Click += FileListHeaderClickedHandler;
		GridViewColumnHeader obj3 = (GridViewColumnHeader)obj.Columns[1].Header;
		obj3.Content = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 28);
		obj3.Click += FileListHeaderClickedHandler;
		GridViewColumnHeader obj4 = (GridViewColumnHeader)obj.Columns[2].Header;
		obj4.Content = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_SCENARIO, Enums.eTextValues.TEXT_SCN_TYPE);
		obj4.Click += FileListHeaderClickedHandler;
		RefFileLists.SelectionChanged += delegate
		{
			if (RefFileLists.SelectedItem != null)
			{
				if (loadRequester)
				{
					FileHeader fileHeader = ((FileRow)RefFileLists.SelectedItem).fileHeader;
					if (fileHeader != null)
					{
						updateRadarTexture(fileHeader);
						selectedHeader = fileHeader;
						RefActionButton.IsEnabled = true;
					}
				}
				else
				{
					FileHeader fileHeader2 = ((FileRow)RefFileLists.SelectedItem).fileHeader;
					if (fileHeader2 != null)
					{
						MainViewModel.Instance.LoadSaveFileName = System.IO.Path.GetFileNameWithoutExtension(fileHeader2.filePath);
					}
				}
			}
		};
		if (FatControler.portuguese || FatControler.czech)
		{
			PropEx.SetGlowButtonFontSize(RefButtonOpenFolder, 14);
			PropEx.SetGlowButtonTextHeight(RefButtonOpenFolder, 20);
		}
		if (FatControler.german)
		{
			RefHideQuicksaveCheck.Margin = new Thickness(25f, 440f, 67f, 0f);
		}
	}

	public void Update()
	{
		if (!((DateTime.UtcNow - lastScrollTest).TotalMilliseconds > 150.0))
		{
			return;
		}
		if (requesterType == Enums.RequesterTypes.LoadMultiplayerGame)
		{
			UpdateMPRows();
		}
		if (KeyManager.instance.CursorUpHeld)
		{
			lastScrollTest = DateTime.UtcNow;
			ScrollViewer scrollViewer = MainViewModel.GetScrollViewer(RefFileLists) as ScrollViewer;
			if (!(scrollViewer != null))
			{
				return;
			}
			if (RefFileLists.SelectedItem == null)
			{
				scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - 30f);
				return;
			}
			if (RefFileLists.SelectedIndex > 0)
			{
				RefFileLists.SelectedIndex--;
			}
			RefFileLists.ScrollIntoView(RefFileLists.SelectedItem);
		}
		else
		{
			if (!KeyManager.instance.CursorDownHeld)
			{
				return;
			}
			lastScrollTest = DateTime.UtcNow;
			ScrollViewer scrollViewer2 = MainViewModel.GetScrollViewer(RefFileLists) as ScrollViewer;
			if (!(scrollViewer2 != null))
			{
				return;
			}
			if (RefFileLists.SelectedItem == null)
			{
				scrollViewer2.ScrollToVerticalOffset(scrollViewer2.VerticalOffset + 30f);
				return;
			}
			if (RefFileLists.SelectedIndex < RefFileLists.Items.Count - 1)
			{
				RefFileLists.SelectedIndex++;
			}
			RefFileLists.ScrollIntoView(RefFileLists.SelectedItem);
		}
	}

	private void InitializeComponent()
	{
		NoesisUnity.LoadComponent(this, "Assets/GUI/XAMLResources/HUD_LoadSaveRequester.xaml");
	}

	protected override bool ConnectEvent(object source, string eventName, string handlerName)
	{
		if (eventName == "MouseEnter" && handlerName == "CommonRedButtonEnter")
		{
			if (source is Button)
			{
				((Button)source).MouseEnter += MainViewModel.Instance.CommonRedButtonEnter;
			}
			else if (source is RadioButton)
			{
				((RadioButton)source).MouseEnter += MainViewModel.Instance.CommonRedButtonEnter;
			}
			return true;
		}
		return false;
	}

	public static void OpenLoadSaveRequester(Enums.RequesterTypes reqType, Action<string, FileHeader> _OKAction, Action _cancelAction, int MPCrcCount = -1)
	{
		if (reqType != Enums.RequesterTypes.LoadMultiplayerGame)
		{
			MainViewModel.Instance.Show_HUD_LoadSaveRequester = true;
		}
		else
		{
			MainViewModel.Instance.Show_HUD_LoadSaveRequesterMP = true;
		}
		if (instance1.IsVisible)
		{
			MainViewModel.Instance.HUDLoadSaveRequester = instance1;
		}
		else if (instance2.IsVisible)
		{
			MainViewModel.Instance.HUDLoadSaveRequester = instance2;
		}
		else if (instance3.IsVisible)
		{
			MainViewModel.Instance.HUDLoadSaveRequester = instance3;
		}
		MainViewModel.Instance.HUDLoadSaveRequester._OpenLoadSaveRequester(reqType, _OKAction, _cancelAction, MPCrcCount);
	}

	private void _OpenLoadSaveRequester(Enums.RequesterTypes reqType, Action<string, FileHeader> _OKAction, Action _cancelAction, int _MPCrcCount = -1)
	{
		MPCrcCount = _MPCrcCount;
		panelActive = false;
		requesterType = reqType;
		OKAction = _OKAction;
		CancelAction = _cancelAction;
		bool saveHideQuicksaveVisible = false;
		MainViewModel.Instance.LoadSaveFilter = "";
		MainViewModel.Instance.LoadSaveFilterLabelVis = Visibility.Visible;
		MainViewModel.Instance.LoadSaveFilterButtonVis = Visibility.Hidden;
		sortByColumn = 1;
		sortByAscending = false;
		RefRadarGrid.Visibility = Visibility.Hidden;
		MainViewModel.Instance.SaveSiegeThatLockVisible = false;
		siegeThatCreateLockedMap = false;
		MainViewModel.Instance.LoadSaveDepthSorting = 0;
		switch (reqType)
		{
		case Enums.RequesterTypes.LoadSinglePlayerGame:
		{
			loadRequester = true;
			saveNotMapRequester = true;
			saveHideQuicksaveVisible = true;
			MainViewModel instance10 = MainViewModel.Instance;
			string buttonLoadSaveActionText = (RefHeading.HeadingText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAINOPTIONS, Enums.eTextValues.TEXT_SCN_HELP));
			instance10.ButtonLoadSaveActionText = buttonLoadSaveActionText;
			break;
		}
		case Enums.RequesterTypes.LoadMultiplayerGame:
		{
			loadRequester = true;
			saveNotMapRequester = true;
			MainViewModel instance9 = MainViewModel.Instance;
			string buttonLoadSaveActionText = (RefHeading.HeadingText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 38));
			instance9.ButtonLoadSaveActionText = buttonLoadSaveActionText;
			break;
		}
		case Enums.RequesterTypes.LoadEditorMap:
		{
			loadRequester = true;
			saveNotMapRequester = false;
			MainViewModel instance8 = MainViewModel.Instance;
			string buttonLoadSaveActionText = (RefHeading.HeadingText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAPEDIT, 2));
			instance8.ButtonLoadSaveActionText = buttonLoadSaveActionText;
			break;
		}
		case Enums.RequesterTypes.LoadSiegeThatTempMap:
		{
			loadRequester = true;
			saveNotMapRequester = false;
			MainViewModel instance7 = MainViewModel.Instance;
			string buttonLoadSaveActionText = (RefHeading.HeadingText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAPEDIT, 2));
			instance7.ButtonLoadSaveActionText = buttonLoadSaveActionText;
			break;
		}
		case Enums.RequesterTypes.LoadUserWorkshopMap:
		{
			loadRequester = true;
			saveNotMapRequester = false;
			MainViewModel instance6 = MainViewModel.Instance;
			string buttonLoadSaveActionText = (RefHeading.HeadingText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAPEDIT, 2));
			instance6.ButtonLoadSaveActionText = buttonLoadSaveActionText;
			break;
		}
		case Enums.RequesterTypes.SaveSinglePlayerGame:
		{
			loadRequester = false;
			saveNotMapRequester = true;
			saveHideQuicksaveVisible = true;
			MainViewModel instance5 = MainViewModel.Instance;
			string buttonLoadSaveActionText = (RefHeading.HeadingText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 3));
			instance5.ButtonLoadSaveActionText = buttonLoadSaveActionText;
			break;
		}
		case Enums.RequesterTypes.SaveMultiplayerGame:
		{
			loadRequester = false;
			saveNotMapRequester = true;
			MainViewModel instance4 = MainViewModel.Instance;
			string buttonLoadSaveActionText = (RefHeading.HeadingText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 3));
			instance4.ButtonLoadSaveActionText = buttonLoadSaveActionText;
			break;
		}
		case Enums.RequesterTypes.SaveEditorMap:
		{
			loadRequester = false;
			saveNotMapRequester = false;
			MainViewModel instance3 = MainViewModel.Instance;
			string buttonLoadSaveActionText = (RefHeading.HeadingText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAPEDIT, 3));
			instance3.ButtonLoadSaveActionText = buttonLoadSaveActionText;
			MainViewModel.Instance.LoadSaveDepthSorting = 4;
			break;
		}
		case Enums.RequesterTypes.SaveSiegeThatLockedMap:
		{
			loadRequester = false;
			saveNotMapRequester = false;
			MainViewModel instance2 = MainViewModel.Instance;
			string buttonLoadSaveActionText = (RefHeading.HeadingText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAPEDIT, 36));
			instance2.ButtonLoadSaveActionText = buttonLoadSaveActionText;
			MainViewModel.Instance.LoadSaveDepthSorting = 4;
			break;
		}
		case Enums.RequesterTypes.SaveSiegeThatTempMap:
		{
			loadRequester = false;
			saveNotMapRequester = false;
			MainViewModel instance = MainViewModel.Instance;
			string buttonLoadSaveActionText = (RefHeading.HeadingText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAPEDIT, 35));
			instance.ButtonLoadSaveActionText = buttonLoadSaveActionText;
			MainViewModel.Instance.SaveSiegeThatLockVisible = true;
			RefLockMapCheck.IsChecked = false;
			MainViewModel.Instance.LoadSaveDepthSorting = 4;
			break;
		}
		}
		if (loadRequester)
		{
			RefFileName.Visibility = Visibility.Hidden;
			MainViewModel.Instance.Show_RequesterRadar = true;
		}
		else
		{
			RefFileName.Visibility = Visibility.Visible;
			updateSaveRadarTexture();
			MainViewModel.Instance.Show_RequesterRadar = true;
			if (saveNotMapRequester)
			{
				MainViewModel.Instance.LoadSaveFileName = rememberedSaveName;
			}
			else
			{
				MainViewModel.Instance.LoadSaveFileName = rememberedMapName;
			}
			RefFileName.Focus();
		}
		if (saveNotMapRequester)
		{
			MainViewModel.Instance.LoadSave_FolderText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 163);
		}
		else
		{
			MainViewModel.Instance.LoadSave_FolderText = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 164);
		}
		MainViewModel.Instance.SaveHideQuicksaveVisible = saveHideQuicksaveVisible;
		RefHideQuicksaveCheck.IsChecked = false;
		if (MainViewModel.Instance.LoadSaveFileName.Length > 0)
		{
			RefActionButton.IsEnabled = true;
		}
		else
		{
			RefActionButton.IsEnabled = false;
		}
		selectedHeader = null;
		populateList();
		if (RefFileLists.Items.Count > 0)
		{
			RefFileLists.ScrollIntoView(RefFileLists.Items[0]);
			if (loadRequester)
			{
				RefFileLists.SelectedItem = RefFileLists.Items[0];
			}
		}
		panelActive = true;
	}

	public void ButtonClicked(int function, bool fromDoubleClick = false)
	{
		switch (function)
		{
		case 1:
			MainViewModel.Instance.Show_HUD_LoadSaveRequester = false;
			MainViewModel.Instance.Show_HUD_LoadSaveRequesterMP = false;
			siegeThatCreateLockedMap = RefLockMapCheck.IsChecked.Value;
			if (!loadRequester && headerlist != null)
			{
				bool flag = false;
				string text = MainViewModel.Instance.LoadSaveFileName.ToLower();
				foreach (FileHeader item in headerlist)
				{
					if (text == item.display_filename.ToLower())
					{
						flag = true;
						break;
					}
				}
				if (!flag && requesterType == Enums.RequesterTypes.SaveSiegeThatTempMap && siegeThatCreateLockedMap)
				{
					foreach (FileHeader item2 in MapFileManager.Instance.GetAllUserMapsForfilenameCheck())
					{
						if (text == item2.display_filename.ToLower())
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					SFXManager.instance.playSpeech(1, "General_Message11.wav", 1f);
					HUD_ConfirmationPopup.ShowConfirmation(Translate.Instance.lookUpText(Enums.eTextSections.TEXT_GAME_OPTIONS, 30), delegate
					{
						SFXManager.instance.playSpeech(1, "General_Saving.wav", 1f);
						RunOKAction();
					}, delegate
					{
						MainViewModel.Instance.Show_HUD_LoadSaveRequester = true;
					});
					break;
				}
			}
			if (loadRequester)
			{
				SFXManager.instance.playSpeech(1, "General_Loading.wav", 1f);
			}
			else
			{
				SFXManager.instance.playSpeech(1, "General_Saving.wav", 1f);
			}
			RunOKAction();
			if (fromDoubleClick)
			{
				EditorDirector.instance.IgnoreNextMouseDown();
			}
			break;
		case 2:
			MainViewModel.Instance.Show_HUD_LoadSaveRequester = false;
			MainViewModel.Instance.Show_HUD_LoadSaveRequesterMP = false;
			CloseRequester();
			break;
		case 3:
			if (saveNotMapRequester)
			{
				try
				{
					string savesPath = ConfigSettings.GetSavesPath();
					Application.OpenURL("file://" + savesPath);
					break;
				}
				catch (Exception)
				{
					break;
				}
			}
			try
			{
				string userMapsPath = ConfigSettings.GetUserMapsPath();
				Application.OpenURL("file://" + userMapsPath);
				break;
			}
			catch (Exception)
			{
				break;
			}
		case 4:
			MainViewModel.Instance.LoadSaveFilter = "";
			MainViewModel.Instance.LoadSaveFilterLabelVis = Visibility.Visible;
			MainViewModel.Instance.LoadSaveFilterButtonVis = Visibility.Hidden;
			break;
		}
	}

	private void RunOKAction()
	{
		if (OKAction != null)
		{
			OKAction(MainViewModel.Instance.LoadSaveFileName, selectedHeader);
		}
		if (loadRequester)
		{
			if (selectedHeader != null)
			{
				if (saveNotMapRequester)
				{
					rememberedSaveName = selectedHeader.display_filename;
				}
				else
				{
					rememberedMapName = selectedHeader.display_filename;
				}
				GameData.Instance.currentFileName = selectedHeader.display_filename;
			}
		}
		else
		{
			if (saveNotMapRequester)
			{
				rememberedSaveName = MainViewModel.Instance.LoadSaveFileName;
			}
			else
			{
				rememberedMapName = MainViewModel.Instance.LoadSaveFileName;
			}
			GameData.Instance.currentFileName = MainViewModel.Instance.LoadSaveFileName;
		}
	}

	public void CloseRequester()
	{
		MainViewModel.Instance.Show_HUD_LoadSaveRequester = false;
		MainViewModel.Instance.Show_HUD_LoadSaveRequesterMP = false;
		if (CancelAction != null)
		{
			CancelAction();
		}
	}

	private void FileListHeaderClickedHandler(object sender, RoutedEventArgs e)
	{
		switch (((GridViewColumnHeader)e.Source).Tag as string)
		{
		case "Name":
			if (sortByColumn == 0)
			{
				sortByAscending = !sortByAscending;
				break;
			}
			sortByColumn = 0;
			sortByAscending = true;
			break;
		case "Date":
			if (sortByColumn == 1)
			{
				sortByAscending = !sortByAscending;
				break;
			}
			sortByColumn = 1;
			sortByAscending = false;
			break;
		}
		populateList();
	}

	private void populateList()
	{
		headerlist = null;
		bool flag = false;
		switch (requesterType)
		{
		case Enums.RequesterTypes.LoadSinglePlayerGame:
		case Enums.RequesterTypes.SaveSinglePlayerGame:
			switch (sortByColumn)
			{
			case 0:
			case 2:
				headerlist = MapFileManager.Instance.GetSaves(sortByName: true, sortByAscending, RefHideQuicksaveCheck.IsChecked.Value);
				break;
			case 1:
				headerlist = MapFileManager.Instance.GetSaves(sortByName: false, sortByAscending, RefHideQuicksaveCheck.IsChecked.Value);
				break;
			}
			break;
		case Enums.RequesterTypes.LoadMultiplayerGame:
		case Enums.RequesterTypes.SaveMultiplayerGame:
			switch (sortByColumn)
			{
			case 0:
			case 2:
				headerlist = MapFileManager.Instance.GetMPSaves(sortByName: true, sortByAscending);
				break;
			case 1:
				headerlist = MapFileManager.Instance.GetMPSaves(sortByName: false, sortByAscending);
				break;
			}
			flag = true;
			break;
		case Enums.RequesterTypes.LoadEditorMap:
		case Enums.RequesterTypes.SaveEditorMap:
		case Enums.RequesterTypes.SaveSiegeThatLockedMap:
			switch (sortByColumn)
			{
			case 0:
			case 2:
				headerlist = MapFileManager.Instance.GetMapEditableMaps(sortByName: true, sortByAscending);
				break;
			case 1:
				headerlist = MapFileManager.Instance.GetMapEditableMaps(sortByName: false, sortByAscending);
				break;
			}
			break;
		case Enums.RequesterTypes.LoadUserWorkshopMap:
			switch (sortByColumn)
			{
			case 0:
			case 2:
				headerlist = MapFileManager.Instance.GetUserWorkshopUploads(sortByName: true, sortByAscending);
				break;
			case 1:
				headerlist = MapFileManager.Instance.GetUserWorkshopUploads(sortByName: false, sortByAscending);
				break;
			}
			break;
		case Enums.RequesterTypes.LoadSiegeThatTempMap:
		case Enums.RequesterTypes.SaveSiegeThatTempMap:
			switch (sortByColumn)
			{
			case 0:
			case 2:
				headerlist = MapFileManager.Instance.GetUserSiegeThatTempMaps(sortByName: true, sortByAscending);
				break;
			case 1:
				headerlist = MapFileManager.Instance.GetUserSiegeThatTempMaps(sortByName: false, sortByAscending);
				break;
			}
			break;
		}
		if (headerlist == null)
		{
			return;
		}
		string text = RefSearchFilter.Text;
		string value = text.ToLowerInvariant();
		rows.Clear();
		foreach (FileHeader item in headerlist)
		{
			if (requesterType == Enums.RequesterTypes.LoadMultiplayerGame && item.retrieveCRCChecks != MPCrcCount)
			{
				item.rowVisible = false;
			}
			else if (text.Length <= 0 || item.display_filename.Contains(text) || item.display_filename.ToLowerInvariant().Contains(value))
			{
				FileRow fileRow = new FileRow();
				fileRow.Text1 = item.display_filename;
				fileRow.Text2 = item.getDateString();
				if (flag)
				{
					fileRow.Text3 = "";
				}
				else
				{
					fileRow.Text3 = item.getGameTypeString();
				}
				fileRow.fileHeader = item;
				rows.Add(fileRow);
			}
		}
		RefFileLists.ItemsSource = rows;
	}

	private void UpdateMPRows()
	{
		bool flag = false;
		foreach (FileHeader item in headerlist)
		{
			if (!item.rowVisible && item.retrieveCRCChecks == MPCrcCount)
			{
				FileRow fileRow = new FileRow();
				fileRow.Text1 = item.display_filename;
				fileRow.Text2 = item.getDateString();
				fileRow.Text3 = "";
				fileRow.fileHeader = item;
				rows.Add(fileRow);
				item.rowVisible = true;
			}
		}
		if (flag)
		{
			RefFileLists.ItemsSource = rows;
		}
	}

	private void updateRadarTexture(FileHeader header)
	{
		if (header != null)
		{
			byte[] radarFromFile = MapFileManager.Instance.GetRadarFromFile(header.filePath);
			if (radarFromFile != null)
			{
				TextureSource radarRequesterImage = new TextureSource(MapFileManager.Instance.GetRadarPreview(radarFromFile));
				MainViewModel.Instance.RadarRequesterImage = radarRequesterImage;
				RefRadarGrid.Visibility = Visibility.Visible;
			}
			else
			{
				RefRadarGrid.Visibility = Visibility.Hidden;
			}
		}
		else
		{
			RefRadarGrid.Visibility = Visibility.Hidden;
		}
	}

	private void updateSaveRadarTexture()
	{
		byte[] saveRadar = EngineInterface.getSaveRadar();
		if (saveRadar != null)
		{
			TextureSource radarRequesterImage = new TextureSource(MapFileManager.Instance.GetRadarPreview(saveRadar));
			MainViewModel.Instance.RadarRequesterImage = radarRequesterImage;
			RefRadarGrid.Visibility = Visibility.Visible;
		}
		else
		{
			RefRadarGrid.Visibility = Visibility.Hidden;
		}
	}

	private void TextInputFocus(object sender, DependencyPropertyChangedEventArgs e)
	{
		MainViewModel.Instance.SetNoesisKeyboardState((bool)e.NewValue);
	}

	private void FilterTextInputFocus(object sender, DependencyPropertyChangedEventArgs e)
	{
		MainViewModel.Instance.SetNoesisKeyboardState((bool)e.NewValue);
		if ((bool)e.NewValue)
		{
			MainViewModel.Instance.LoadSaveFilterLabelVis = Visibility.Hidden;
		}
		else if (RefSearchFilter.Text.Length == 0)
		{
			MainViewModel.Instance.LoadSaveFilterLabelVis = Visibility.Visible;
		}
	}

	private void TextBoxLoaded(object sender, RoutedEventArgs e)
	{
		if (!loadRequester)
		{
			RefFileName.Focus();
		}
	}

	private void TextChangedHandler(object sender, RoutedEventArgs e)
	{
		if (!loadRequester)
		{
			if (RefFileName.Text.Length > 0)
			{
				RefActionButton.IsEnabled = true;
			}
			else
			{
				RefActionButton.IsEnabled = false;
			}
		}
	}

	private void FilterTextChangedHandler(object sender, RoutedEventArgs e)
	{
		if (panelActive)
		{
			populateList();
			if (RefSearchFilter.Text.Length == 0)
			{
				MainViewModel.Instance.LoadSaveFilterButtonVis = Visibility.Hidden;
			}
			else
			{
				MainViewModel.Instance.LoadSaveFilterButtonVis = Visibility.Visible;
			}
		}
	}

	private void FileNameValidationTextBox(object sender, TextCompositionEventArgs e)
	{
		if (e.Text == "\n")
		{
			e.Handled = true;
			base.Keyboard.ClearFocus();
			return;
		}
		char[] invalidFileNameChars = System.IO.Path.GetInvalidFileNameChars();
		string text = e.Text;
		foreach (char value in text)
		{
			if (invalidFileNameChars.Contains(value))
			{
				e.Handled = true;
			}
		}
	}

	private void TextBoxCheckForEscape(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Escape)
		{
			base.Keyboard.ClearFocus();
			KeyManager.instance.ignoreEscape();
		}
	}

	private void TextBoxEnterCheck(object sender, TextCompositionEventArgs e)
	{
		if (e.Text == "\n")
		{
			e.Handled = true;
			base.Keyboard.ClearFocus();
		}
	}

	private void QuickSave_ValueChanged(object sender, RoutedEventArgs e)
	{
		if (panelActive)
		{
			populateList();
		}
	}
}
