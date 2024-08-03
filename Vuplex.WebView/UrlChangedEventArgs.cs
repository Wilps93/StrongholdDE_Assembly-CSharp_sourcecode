using System;

namespace Vuplex.WebView;

public class UrlChangedEventArgs : EventArgs
{
	public string Url;

	public string Type;

	[Obsolete("UrlChangedEventArgs.Title has been removed. Please use IWebView.Title or IWebView.TitleChanged instead: https://developer.vuplex.com/webview/IWebView#Title", true)]
	public string Title;

	public UrlChangedEventArgs(string url, string type)
	{
		Url = url;
		Type = type;
	}
}
