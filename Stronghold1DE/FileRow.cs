using System.ComponentModel;
using Noesis;

namespace Stronghold1DE;

public class FileRow : INotifyPropertyChanged
{
	private string _text1;

	private string _text2;

	private string _text3;

	private string _text4 = "";

	private ImageSource _typeImage;

	private ImageSource _kothImage;

	public FileHeader fileHeader;

	public Platform_Multiplayer.MPLobby lobby;

	public string Text1
	{
		get
		{
			return _text1;
		}
		set
		{
			_text1 = value;
			NotifyPropertyChanged("Text1");
		}
	}

	public string Text2
	{
		get
		{
			return _text2;
		}
		set
		{
			_text2 = value;
			NotifyPropertyChanged("Text2");
		}
	}

	public string Text3
	{
		get
		{
			return _text3;
		}
		set
		{
			_text3 = value;
			NotifyPropertyChanged("Text3");
		}
	}

	public string Text4
	{
		get
		{
			return _text4;
		}
		set
		{
			_text4 = value;
			NotifyPropertyChanged("Text4");
		}
	}

	public ImageSource TypeImage
	{
		get
		{
			return _typeImage;
		}
		set
		{
			_typeImage = value;
			NotifyPropertyChanged("TypeImage");
		}
	}

	public ImageSource KothImage
	{
		get
		{
			return _kothImage;
		}
		set
		{
			_kothImage = value;
			NotifyPropertyChanged("KothImage");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void NotifyPropertyChanged(string propertyName = "")
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
