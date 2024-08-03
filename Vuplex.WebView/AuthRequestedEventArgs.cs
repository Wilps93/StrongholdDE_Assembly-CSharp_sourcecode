using System;

namespace Vuplex.WebView;

public class AuthRequestedEventArgs : EventArgs
{
	public readonly string Host;

	private Action _cancelCallback;

	private Action<string, string> _continueCallback;

	public AuthRequestedEventArgs(string host, Action<string, string> continueCallback, Action cancelCallback)
	{
		Host = host;
		_continueCallback = continueCallback;
		_cancelCallback = cancelCallback;
	}

	public void Cancel()
	{
		_cancelCallback();
	}

	public void Continue(string username, string password)
	{
		_continueCallback(username, password);
	}
}
