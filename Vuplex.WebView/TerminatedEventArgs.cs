using System;

namespace Vuplex.WebView;

public class TerminatedEventArgs : EventArgs
{
	public readonly TerminationType Type;

	public TerminatedEventArgs(TerminationType type)
	{
		Type = type;
	}
}
