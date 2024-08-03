using System.ComponentModel;

namespace Stronghold1DE;

public class HotKeyRow : INotifyPropertyChanged
{
	private string _text1;

	private string _text2;

	private string _dataValue;

	private int _iDataValue;

	private HUD_Options parent;

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

	public string DataValue
	{
		get
		{
			return _dataValue;
		}
		set
		{
			_dataValue = value;
			NotifyPropertyChanged("DataValue");
		}
	}

	public int iDataValue
	{
		get
		{
			return _iDataValue;
		}
		set
		{
			_iDataValue = value;
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void NotifyPropertyChanged(string propertyName = "")
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	public HotKeyRow(HUD_Options _parent)
	{
		parent = _parent;
	}
}
