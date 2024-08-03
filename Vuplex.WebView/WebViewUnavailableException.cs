using System;

namespace Vuplex.WebView;

public class WebViewUnavailableException : Exception
{
	public WebViewUnavailableException(string message)
		: base(message)
	{
	}
}
