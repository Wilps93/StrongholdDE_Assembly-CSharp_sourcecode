using UnityEngine;

namespace Vuplex.WebView.Internal;

public static class WebViewLogger
{
	private const string PREFIX = "[3D WebView] ";

	private const string EM_OPENING_REPLACEMENT = "";

	private const string EM_CLOSING_REPLACEMENT = "";

	public static void Log(string message, bool enableFormatting = true)
	{
		if (enableFormatting)
		{
			Debug.Log(_format(message));
		}
		else
		{
			Debug.Log("[3D WebView] " + message);
		}
	}

	public static void LogError(string message, bool enableFormatting = true)
	{
		if (enableFormatting)
		{
			Debug.LogError(_format(message));
		}
		else
		{
			Debug.LogError("[3D WebView] " + message);
		}
	}

	public static void LogErrorWithoutFormatting(string message)
	{
		Debug.LogError("[3D WebView] " + message);
	}

	public static void LogTip(string message)
	{
		Log("Tip: " + message);
	}

	public static void LogWarning(string message, bool enableFormatting = true)
	{
		if (enableFormatting)
		{
			Debug.LogWarning(_format(message));
		}
		else
		{
			Debug.LogWarning("[3D WebView] " + message);
		}
	}

	public static void LogWarningWithoutFormatting(string message)
	{
		Debug.LogWarning("[3D WebView] " + message);
	}

	private static string _format(string originalMessage)
	{
		string text = originalMessage;
		if (text.Contains("<em>"))
		{
			text = text.Replace("<em>", "").Replace("</em>", "");
		}
		return "[3D WebView] " + text;
	}
}
