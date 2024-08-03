using System;
using Vuplex.WebView.Internal;

namespace Vuplex.WebView;

public class FocusedInputFieldChangedEventArgs : EventArgs
{
	public readonly FocusedInputFieldType Type;

	public FocusedInputFieldChangedEventArgs(FocusedInputFieldType type)
	{
		Type = type;
	}

	public static FocusedInputFieldType ParseType(string typeString)
	{
		if (!(typeString == "TEXT"))
		{
			if (typeString == "NONE")
			{
				return FocusedInputFieldType.None;
			}
			WebViewLogger.LogWarning("Unrecognized FocusedInputFieldType string: " + typeString);
			return FocusedInputFieldType.None;
		}
		return FocusedInputFieldType.Text;
	}
}
