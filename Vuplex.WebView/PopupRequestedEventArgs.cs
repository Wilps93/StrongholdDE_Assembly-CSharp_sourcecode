using System;

namespace Vuplex.WebView;

public class PopupRequestedEventArgs : EventArgs
{
	public readonly string Url;

	public readonly IWebView WebView;

	public PopupRequestedEventArgs(string url, IWebView webView)
	{
		Url = url;
		WebView = webView;
	}
}
