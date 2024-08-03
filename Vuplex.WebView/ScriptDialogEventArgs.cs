using System;

namespace Vuplex.WebView;

public class ScriptDialogEventArgs : EventArgs
{
	public readonly string Message;

	public readonly Action Continue;

	public ScriptDialogEventArgs(string message, Action continueCallback)
	{
		Message = message;
		Continue = continueCallback;
	}
}
public class ScriptDialogEventArgs<T> : EventArgs
{
	public readonly string Message;

	public readonly Action<T> Continue;

	public ScriptDialogEventArgs(string message, Action<T> continueCallback)
	{
		Message = message;
		Continue = continueCallback;
	}
}
