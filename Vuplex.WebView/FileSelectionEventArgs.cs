using System;

namespace Vuplex.WebView;

public class FileSelectionEventArgs : EventArgs
{
	public readonly string[] AcceptFilters;

	public readonly bool MultipleAllowed;

	public readonly Action<string[]> Continue;

	public readonly Action Cancel;

	public FileSelectionEventArgs(string[] acceptFilters, bool multipleAllowed, Action<string[]> continueCallback, Action cancelCallback)
	{
		AcceptFilters = acceptFilters;
		MultipleAllowed = multipleAllowed;
		Continue = continueCallback;
		Cancel = cancelCallback;
	}
}
