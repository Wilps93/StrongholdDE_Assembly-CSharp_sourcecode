using System.Collections.Generic;
using Noesis;

namespace Stronghold1DE;

public class HUD_MPChatPanel : UserControl
{
	private int multiplayerChatNumInTeam;

	private int multiplayerChatNumTotal;

	private int[] multiplayerChatPlayers = new int[9];

	private int[] multiplayerChatTeams = new int[9];

	private bool[] multiplayerChatSelectedPlayers = new bool[9];

	private List<int> multiplayerIngameChatRecipients = new List<int>();

	private int[] multiplayerMapping = new int[8];

	private Button[] RefMPChatPlayers = new Button[8];

	private Button RefMPChatTeam;

	private Button RefMPChatSend;

	private TextBox RefMPChatMessageTextBox;

	private TextBlock RefMPChatInsultText;

	public HUD_MPChatPanel()
	{
		InitializeComponent();
		MainViewModel.Instance.HUDMPChatPanel = this;
		RefMPChatPlayers[0] = (Button)FindName("MPChatPlayer1");
		RefMPChatPlayers[1] = (Button)FindName("MPChatPlayer2");
		RefMPChatPlayers[2] = (Button)FindName("MPChatPlayer3");
		RefMPChatPlayers[3] = (Button)FindName("MPChatPlayer4");
		RefMPChatPlayers[4] = (Button)FindName("MPChatPlayer5");
		RefMPChatPlayers[5] = (Button)FindName("MPChatPlayer6");
		RefMPChatPlayers[6] = (Button)FindName("MPChatPlayer7");
		RefMPChatPlayers[7] = (Button)FindName("MPChatPlayer8");
		RefMPChatTeam = (Button)FindName("MPChatTeam");
		RefMPChatMessageTextBox = (TextBox)FindName("MPChatMessageTextBox");
		RefMPChatMessageTextBox.IsKeyboardFocusedChanged += TextInputFocus;
		RefMPChatMessageTextBox.TextChanged += EditMessageTextChanged;
		RefMPChatMessageTextBox.Loaded += TextBoxLoaded;
		RefMPChatMessageTextBox.PreviewTextInput += DetectEnterTextBox;
		RefMPChatSend = (Button)FindName("MPChatSend");
		RefMPChatInsultText = (TextBlock)FindName("MPChatInsultText");
	}

	private void TextInputFocus(object sender, DependencyPropertyChangedEventArgs e)
	{
		MainViewModel.Instance.SetNoesisKeyboardState((bool)e.NewValue);
	}

	private void TextBoxLoaded(object sender, RoutedEventArgs e)
	{
		RefMPChatMessageTextBox.Focus();
	}

	private void EditMessageTextChanged(object sender, RoutedEventArgs e)
	{
		UpdateButtons();
	}

	private void DetectEnterTextBox(object sender, TextCompositionEventArgs e)
	{
		if (e.Text == "\n")
		{
			e.Handled = true;
			base.Keyboard.ClearFocus();
			if (RefMPChatMessageTextBox.Text.Length > 0)
			{
				ButtonClicked("SEND");
			}
		}
	}

	private void UpdateButtons()
	{
		RefMPChatSend.IsEnabled = RefMPChatMessageTextBox.Text.Length > 0 && multiplayerIngameChatRecipients.Count > 0;
		for (int i = 0; i < 8; i++)
		{
			int num = multiplayerMapping[i];
			if (num > 0)
			{
				if (multiplayerChatSelectedPlayers[num])
				{
					PropEx.SetBorderVisibility(RefMPChatPlayers[i], Visibility.Visible);
				}
				else
				{
					PropEx.SetBorderVisibility(RefMPChatPlayers[i], Visibility.Hidden);
				}
			}
		}
	}

	public void ToggleMultiplayerChat()
	{
		if (MainViewModel.Instance.MPChatVisible || !Director.instance.SimRunning)
		{
			MainViewModel.Instance.MPChatVisible = false;
			return;
		}
		EngineInterface.GetMultiplayerChatInfo(ref multiplayerChatPlayers, ref multiplayerChatTeams);
		RefMPChatMessageTextBox.Text = "";
		RefMPChatSend.IsEnabled = false;
		multiplayerChatNumInTeam = 1;
		multiplayerChatNumTotal = 0;
		int playerID = GameData.Instance.playerID;
		int num = multiplayerChatTeams[playerID];
		for (int i = 0; i < 8; i++)
		{
			multiplayerMapping[i] = -1;
		}
		for (int j = 1; j < 9; j++)
		{
			multiplayerChatSelectedPlayers[j] = true;
			if (multiplayerChatPlayers[j] > 0)
			{
				multiplayerMapping[multiplayerChatNumTotal] = j;
				multiplayerChatNumTotal++;
			}
			if (multiplayerChatTeams[j] == num)
			{
				multiplayerChatNumInTeam++;
			}
		}
		RefMPChatInsultText.Visibility = Visibility.Hidden;
		RefMPChatMessageTextBox.Visibility = Visibility.Visible;
		UpdateIngameChatRecipients();
		if (multiplayerChatNumTotal >= 2)
		{
			MainViewModel.Instance.MPChatVisible = true;
			MainViewModel.Instance.HUDMPChatMessages.OpenChatPanel();
			for (int k = 0; k < 8; k++)
			{
				int num2 = multiplayerMapping[k];
				if (num2 >= 0)
				{
					RefMPChatPlayers[k].Visibility = Visibility.Visible;
					PropEx.SetTextCentre(RefMPChatPlayers[k], Platform_Multiplayer.Instance.getPlayerName(num2));
					SolidColorBrush value = new SolidColorBrush(HUD_MPChatMessages.MPTeamColours[HUD_MPChatMessages.MP_orig_remap_colour_order[num2]]);
					PropEx.SetButtonTextColour(RefMPChatPlayers[k], value);
				}
				else
				{
					RefMPChatPlayers[k].Visibility = Visibility.Hidden;
				}
			}
			if (multiplayerChatNumInTeam >= 2)
			{
				RefMPChatTeam.Visibility = Visibility.Visible;
			}
			else
			{
				RefMPChatTeam.Visibility = Visibility.Hidden;
			}
			UpdateButtons();
		}
		RefMPChatMessageTextBox.Focus();
	}

	private void UpdateIngameChatRecipients()
	{
		multiplayerIngameChatRecipients.Clear();
		for (int i = 1; i < 9; i++)
		{
			if (multiplayerChatSelectedPlayers[i] && multiplayerChatPlayers[i] > 0)
			{
				multiplayerIngameChatRecipients.Add(multiplayerChatPlayers[i]);
			}
		}
	}

	public void ButtonClicked(string param)
	{
		int playerID = GameData.Instance.playerID;
		switch (param)
		{
		case "P1":
		case "P2":
		case "P3":
		case "P4":
		case "P5":
		case "P6":
		case "P7":
		case "P8":
		{
			int num3 = -1;
			switch (param)
			{
			case "P1":
				num3 = multiplayerMapping[0];
				break;
			case "P2":
				num3 = multiplayerMapping[1];
				break;
			case "P3":
				num3 = multiplayerMapping[2];
				break;
			case "P4":
				num3 = multiplayerMapping[3];
				break;
			case "P5":
				num3 = multiplayerMapping[4];
				break;
			case "P6":
				num3 = multiplayerMapping[5];
				break;
			case "P7":
				num3 = multiplayerMapping[6];
				break;
			case "P8":
				num3 = multiplayerMapping[7];
				break;
			}
			multiplayerChatSelectedPlayers[num3] = !multiplayerChatSelectedPlayers[num3];
			UpdateIngameChatRecipients();
			break;
		}
		case "I1":
		case "I2":
		case "I3":
		case "I4":
		case "I5":
		case "I6":
		case "I7":
		case "I8":
		case "I9":
		case "I10":
		case "I11":
		case "I12":
		case "I13":
		case "I14":
		case "I15":
		case "I16":
		case "I17":
		case "I18":
		case "I19":
		case "I20":
			if (multiplayerIngameChatRecipients.Count > 0)
			{
				int num2 = 0;
				switch (param)
				{
				case "I1":
					num2 = 1;
					break;
				case "I2":
					num2 = 2;
					break;
				case "I3":
					num2 = 3;
					break;
				case "I4":
					num2 = 4;
					break;
				case "I5":
					num2 = 5;
					break;
				case "I6":
					num2 = 6;
					break;
				case "I7":
					num2 = 7;
					break;
				case "I8":
					num2 = 8;
					break;
				case "I9":
					num2 = 9;
					break;
				case "I10":
					num2 = 10;
					break;
				case "I11":
					num2 = 11;
					break;
				case "I12":
					num2 = 12;
					break;
				case "I13":
					num2 = 13;
					break;
				case "I14":
					num2 = 14;
					break;
				case "I15":
					num2 = 15;
					break;
				case "I16":
					num2 = 16;
					break;
				case "I17":
					num2 = 17;
					break;
				case "I18":
					num2 = 18;
					break;
				case "I19":
					num2 = 19;
					break;
				case "I20":
					num2 = 20;
					break;
				}
				Platform_Multiplayer.Instance.SendIngameChatInsult(multiplayerIngameChatRecipients, num2);
				MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Platform_Multiplayer.Instance.getPlayerName(playerID), playerID, Translate.Instance.lookUpText(Enums.eTextSections.TEXT_INSULTS, num2));
				SFXManager.instance.playInsult(num2);
			}
			break;
		case "ALL":
		{
			for (int j = 1; j < 9; j++)
			{
				multiplayerChatSelectedPlayers[j] = true;
			}
			UpdateIngameChatRecipients();
			break;
		}
		case "TEAM":
		{
			int num = multiplayerChatTeams[playerID];
			for (int i = 1; i < 9; i++)
			{
				multiplayerChatSelectedPlayers[i] = multiplayerChatTeams[i] == num;
			}
			UpdateIngameChatRecipients();
			break;
		}
		case "SEND":
			Platform_Multiplayer.Instance.SendIngameChat(multiplayerIngameChatRecipients, RefMPChatMessageTextBox.Text);
			MainViewModel.Instance.HUDMPChatMessages.recieveIngameChat(Platform_Multiplayer.Instance.getPlayerName(playerID), playerID, RefMPChatMessageTextBox.Text);
			RefMPChatMessageTextBox.Text = "";
			break;
		case "EXIT":
			MainViewModel.Instance.MPChatVisible = false;
			break;
		}
		UpdateButtons();
	}

	private void MouseEnterInsultHandler(object sender, MouseEventArgs e)
	{
		if (e.Source is Button)
		{
			string text = (string)((Button)e.Source).Tag;
			RefMPChatMessageTextBox.Visibility = Visibility.Hidden;
			RefMPChatInsultText.Text = text;
			RefMPChatInsultText.Visibility = Visibility.Visible;
			RefMPChatSend.IsEnabled = false;
			MainViewModel.Instance.CommonRedButtonEnter(null, null);
		}
	}

	private void MouseLeaveInsultHandler(object sender, MouseEventArgs e)
	{
		RefMPChatMessageTextBox.Visibility = Visibility.Visible;
		RefMPChatInsultText.Visibility = Visibility.Hidden;
		UpdateButtons();
	}

	private void InitializeComponent()
	{
		GUI.LoadComponent(this, "Assets/GUI/XAMLResources/HUD_MPChatPanel.xaml");
	}

	protected override bool ConnectEvent(object source, string eventName, string handlerName)
	{
		if (eventName == "MouseEnter" && handlerName == "MouseEnterInsultHandler")
		{
			if (source is Button)
			{
				((Button)source).MouseEnter += MouseEnterInsultHandler;
			}
			return true;
		}
		if (eventName == "MouseLeave" && handlerName == "MouseLeaveInsultHandler")
		{
			if (source is Button)
			{
				((Button)source).MouseLeave += MouseLeaveInsultHandler;
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
}
