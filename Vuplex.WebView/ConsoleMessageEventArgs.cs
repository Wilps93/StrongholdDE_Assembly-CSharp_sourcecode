using System;

namespace Vuplex.WebView;

public class ConsoleMessageEventArgs : EventArgs
{
	public readonly ConsoleMessageLevel Level;

	public readonly string Message;

	public readonly string Source;

	public readonly int Line;

	public ConsoleMessageEventArgs(ConsoleMessageLevel level, string message, string source, int line)
	{
		Level = level;
		Message = message;
		Source = source;
		Line = line;
	}
}
