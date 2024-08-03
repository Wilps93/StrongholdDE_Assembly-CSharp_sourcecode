using System;

namespace Vuplex.WebView;

public class PageLoadFailedException : Exception
{
	public PageLoadFailedException(string message)
		: base(message)
	{
	}
}
