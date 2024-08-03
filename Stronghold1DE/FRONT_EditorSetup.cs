using System;
using System.IO;
using Noesis;

namespace Stronghold1DE;

public class FRONT_EditorSetup : UserControl
{
	private int mapSize = 160;

	private int mode;

	private Button refSize160;

	private Button refSize200;

	private Button refSize300;

	private Button refSize400;

	private TextBlock refMapEditorTypeHelpTB;

	public FRONT_EditorSetup()
	{
		InitializeComponent();
		MainViewModel.Instance.FRONTEditorSetup = this;
		refSize160 = (Button)FindName("Size160");
		refSize200 = (Button)FindName("Size200");
		refSize300 = (Button)FindName("Size300");
		refSize400 = (Button)FindName("Size400");
		refMapEditorTypeHelpTB = (TextBlock)FindName("MapEditorTypeHelpTB");
		if (FatControler.thai)
		{
			refMapEditorTypeHelpTB.FontSize = 14f;
		}
	}

	public static void Open()
	{
		MainViewModel.Instance.Show_MapEditor = true;
		Platform_Workshop.Instance.clearMetaData();
		MainViewModel.Instance.FRONTEditorSetup.doOpen();
	}

	private void doOpen()
	{
		mapSize = 160;
		mode = 0;
		UpdateButtons();
	}

	public void ButtonClicked(string param)
	{
		switch (param)
		{
		case "Back":
			MainViewModel.Instance.FrontEndMenu.ButtonClicked("BackMain");
			break;
		case "Start":
			MainViewModel.Instance.InitNewScene(Enums.SceneIDS.MapEditor);
			switch (mode)
			{
			case 0:
				EditorDirector.instance.createNewMap(mapSize, Enums.GameModes.SIEGE, siege_that: false);
				break;
			case 1:
				EditorDirector.instance.createNewMap(mapSize, Enums.GameModes.INVASION, siege_that: false);
				break;
			case 2:
				EditorDirector.instance.createNewMap(mapSize, Enums.GameModes.ECO, siege_that: false);
				break;
			case 3:
				EditorDirector.instance.createNewMap(mapSize, Enums.GameModes.BUILD, siege_that: false);
				break;
			case 4:
				EditorDirector.instance.createNewMap(mapSize, Enums.GameModes.BUILD, siege_that: false, multiPlayerMap: true);
				break;
			case 5:
				EditorDirector.instance.createNewMap(160, Enums.GameModes.SIEGE, siege_that: true);
				break;
			}
			break;
		case "160":
			if (mode != 5)
			{
				mapSize = 160;
				UpdateButtons();
			}
			break;
		case "200":
			if (mode != 5)
			{
				mapSize = 200;
				UpdateButtons();
			}
			break;
		case "300":
			if (mode != 5)
			{
				mapSize = 300;
				UpdateButtons();
			}
			break;
		case "400":
			if (mode != 5)
			{
				mapSize = 400;
				UpdateButtons();
			}
			break;
		case "Siege":
			mode = 0;
			UpdateButtons();
			break;
		case "Invasion":
			mode = 1;
			UpdateButtons();
			break;
		case "Economic":
			mode = 2;
			UpdateButtons();
			break;
		case "Freebuild":
			mode = 3;
			UpdateButtons();
			break;
		case "Multiplayer":
			mode = 4;
			UpdateButtons();
			break;
		case "SiegeThat":
			mode = 5;
			mapSize = 160;
			UpdateButtons();
			break;
		case "Load":
			MainViewModel.Instance.FrontEndMenu.UpdateFrontMenuPopupScale();
			HUD_LoadSaveRequester.OpenLoadSaveRequester(Enums.RequesterTypes.LoadEditorMap, delegate(string filename, FileHeader header)
			{
				if (header.isMapEditable())
				{
					GameData.Instance.SetMissionTextFromHeader(header);
					MainViewModel.Instance.InitNewScene(Enums.SceneIDS.MapEditor);
					EditorDirector.instance.loadMapIntoEditor(header.filePath, header.standAlone_filename);
				}
			}, delegate
			{
			});
			break;
		case "LoadWorkshop":
			MainViewModel.Instance.FrontEndMenu.UpdateFrontMenuPopupScale();
			HUD_LoadSaveRequester.OpenLoadSaveRequester(Enums.RequesterTypes.LoadUserWorkshopMap, delegate(string filename, FileHeader header)
			{
				if (header.isMapEditable())
				{
					GameData.Instance.SetMissionTextFromHeader(header);
					MainViewModel.Instance.InitNewScene(Enums.SceneIDS.MapEditor);
					EditorDirector.instance.loadMapIntoEditor(header.filePath, header.standAlone_filename);
					string path = header.filePath.Replace(".map", ".data");
					try
					{
						string[] array = File.ReadAllLines(path);
						if (array != null && array.Length >= 2)
						{
							bool balanced = false;
							ulong publishID = ulong.Parse(array[0], Director.defaultCulture);
							if (array[1][0] == '-')
							{
								balanced = true;
								array[1] = array[1].Substring(1);
							}
							int difficulty = int.Parse(array[1], Director.defaultCulture);
							string text = "";
							for (int i = 2; i < array.Length; i++)
							{
								if (i > 2 && (i != array.Length || array[i].Length > 0))
								{
									text += "\n";
								}
								text += array[i];
							}
							Platform_Workshop.Instance.importMetaData(publishID, header.standAlone_filename, difficulty, text, balanced);
						}
					}
					catch (Exception)
					{
					}
				}
			}, delegate
			{
			});
			break;
		case "LoadSiegeThat":
			HUD_LoadSaveRequester.OpenLoadSaveRequester(Enums.RequesterTypes.LoadSiegeThatTempMap, delegate(string filename, FileHeader header)
			{
				GameData.Instance.SetMissionTextFromHeader(header);
				MainViewModel.Instance.InitNewScene(Enums.SceneIDS.MapEditor);
				EditorDirector.instance.loadMapIntoEditor(header.filePath, header.standAlone_filename);
			}, delegate
			{
			});
			break;
		}
	}

	private void UpdateButtons()
	{
		switch (mapSize)
		{
		case 160:
			MainViewModel.Instance.MapEditorSetup160 = Visibility.Visible;
			MainViewModel.Instance.MapEditorSetup200 = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetup300 = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetup400 = Visibility.Hidden;
			break;
		case 200:
			MainViewModel.Instance.MapEditorSetup160 = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetup200 = Visibility.Visible;
			MainViewModel.Instance.MapEditorSetup300 = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetup400 = Visibility.Hidden;
			break;
		case 300:
			MainViewModel.Instance.MapEditorSetup160 = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetup200 = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetup300 = Visibility.Visible;
			MainViewModel.Instance.MapEditorSetup400 = Visibility.Hidden;
			break;
		case 400:
			MainViewModel.Instance.MapEditorSetup160 = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetup200 = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetup300 = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetup400 = Visibility.Visible;
			break;
		}
		switch (mode)
		{
		case 0:
			MainViewModel.Instance.MapEditorSetupSiege = Visibility.Visible;
			MainViewModel.Instance.MapEditorSetupInvasion = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupEco = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupFree = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupMulti = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupSiegeThat = Visibility.Hidden;
			refSize160.IsEnabled = true;
			refSize200.IsEnabled = true;
			refSize300.IsEnabled = true;
			refSize400.IsEnabled = true;
			break;
		case 1:
			MainViewModel.Instance.MapEditorSetupSiege = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupInvasion = Visibility.Visible;
			MainViewModel.Instance.MapEditorSetupEco = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupFree = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupMulti = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupSiegeThat = Visibility.Hidden;
			refSize160.IsEnabled = true;
			refSize200.IsEnabled = true;
			refSize300.IsEnabled = true;
			refSize400.IsEnabled = true;
			break;
		case 2:
			MainViewModel.Instance.MapEditorSetupSiege = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupInvasion = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupEco = Visibility.Visible;
			MainViewModel.Instance.MapEditorSetupFree = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupMulti = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupSiegeThat = Visibility.Hidden;
			refSize160.IsEnabled = true;
			refSize200.IsEnabled = true;
			refSize300.IsEnabled = true;
			refSize400.IsEnabled = true;
			break;
		case 3:
			MainViewModel.Instance.MapEditorSetupSiege = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupInvasion = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupEco = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupFree = Visibility.Visible;
			MainViewModel.Instance.MapEditorSetupMulti = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupSiegeThat = Visibility.Hidden;
			refSize160.IsEnabled = true;
			refSize200.IsEnabled = true;
			refSize300.IsEnabled = true;
			refSize400.IsEnabled = true;
			break;
		case 4:
			MainViewModel.Instance.MapEditorSetupSiege = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupInvasion = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupEco = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupFree = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupMulti = Visibility.Visible;
			MainViewModel.Instance.MapEditorSetupSiegeThat = Visibility.Hidden;
			refSize160.IsEnabled = true;
			refSize200.IsEnabled = true;
			refSize300.IsEnabled = true;
			refSize400.IsEnabled = true;
			break;
		case 5:
			MainViewModel.Instance.MapEditorSetupSiege = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupInvasion = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupEco = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupFree = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupMulti = Visibility.Hidden;
			MainViewModel.Instance.MapEditorSetupSiegeThat = Visibility.Visible;
			refSize160.IsEnabled = true;
			refSize200.IsEnabled = false;
			refSize300.IsEnabled = false;
			refSize400.IsEnabled = false;
			break;
		}
	}

	private void InitializeComponent()
	{
		GUI.LoadComponent(this, "Assets/GUI/XAMLResources/FRONT_EditorSetup.xaml");
	}

	protected override bool ConnectEvent(object source, string eventName, string handlerName)
	{
		if (eventName == "MouseEnter" && handlerName == "MouseEnterTypeHandler")
		{
			if (source is Button)
			{
				((Button)source).MouseEnter += MouseEnterTypeHandler;
			}
			return true;
		}
		if (eventName == "MouseLeave" && handlerName == "MouseLeaveTypeHandler")
		{
			if (source is Button)
			{
				((Button)source).MouseLeave += MouseLeaveTypeHandler;
			}
			return true;
		}
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

	private void MouseEnterTypeHandler(object sender, MouseEventArgs e)
	{
		if (e.Source is Button && ((Button)e.Source).CommandParameter != null && ((Button)e.Source).CommandParameter is string)
		{
			switch ((string)((Button)e.Source).CommandParameter)
			{
			case "Siege":
				MainViewModel.Instance.MapEditorTypeHelp = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEWMAP_TYPES_HELP, 1);
				MainViewModel.Instance.CommonRedButtonEnter(null, null);
				break;
			case "Invasion":
				MainViewModel.Instance.MapEditorTypeHelp = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEWMAP_TYPES_HELP, 3);
				MainViewModel.Instance.CommonRedButtonEnter(null, null);
				break;
			case "Economic":
				MainViewModel.Instance.MapEditorTypeHelp = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEWMAP_TYPES_HELP, 5);
				MainViewModel.Instance.CommonRedButtonEnter(null, null);
				break;
			case "Freebuild":
				MainViewModel.Instance.MapEditorTypeHelp = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEWMAP_TYPES_HELP, 7);
				MainViewModel.Instance.CommonRedButtonEnter(null, null);
				break;
			case "Multiplayer":
				MainViewModel.Instance.MapEditorTypeHelp = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_NEW_TEXT, 109);
				MainViewModel.Instance.CommonRedButtonEnter(null, null);
				break;
			case "SiegeThat":
				MainViewModel.Instance.MapEditorTypeHelp = Translate.Instance.lookUpText(Enums.eTextSections.TEXT_MAPEDIT, 37);
				MainViewModel.Instance.CommonRedButtonEnter(null, null);
				break;
			}
		}
	}

	private void MouseLeaveTypeHandler(object sender, MouseEventArgs e)
	{
		MainViewModel.Instance.MapEditorTypeHelp = "";
	}
}
