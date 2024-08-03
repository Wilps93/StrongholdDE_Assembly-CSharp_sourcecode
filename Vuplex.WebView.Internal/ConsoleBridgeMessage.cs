using System;

namespace Vuplex.WebView.Internal;

[Serializable]
internal class ConsoleBridgeMessage : BridgeMessage
{
	public string message;

	public string level;

	public string source;

	public int line;

	public ConsoleMessageEventArgs ToEventArgs()
	{
		return new ConsoleMessageEventArgs(_parseMessageLevel(level), message, source, line);
	}

	private ConsoleMessageLevel _parseMessageLevel(string levelString)
	{
		switch (levelString)
		{
		case "LOG":
			return ConsoleMessageLevel.Log;
		case "DEBUG":
			return ConsoleMessageLevel.Debug;
		case "WARNING":
			return ConsoleMessageLevel.Warning;
		case "ERROR":
			return ConsoleMessageLevel.Error;
		default:
			WebViewLogger.LogError("Unrecognized ConsoleMessageLevel string: " + levelString);
			return ConsoleMessageLevel.Log;
		}
	}
}
