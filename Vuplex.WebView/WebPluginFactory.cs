using UnityEngine;
using Vuplex.WebView.Internal;

namespace Vuplex.WebView;

public class WebPluginFactory
{
	protected static IWebPlugin _androidPlugin;

	protected static IWebPlugin _androidGeckoPlugin;

	protected static IWebPlugin _iosPlugin;

	protected static IWebPlugin _macPlugin;

	protected static IWebPlugin _mockPlugin = MockWebPlugin.Instance;

	private bool _mockWarningLogged;

	private const string MORE_INFO_TEXT = " For more info, please visit https://developer.vuplex.com.";

	protected static IWebPlugin _uwpPlugin;

	protected static IWebPlugin _webGLPlugin;

	protected static IWebPlugin _windowsPlugin;

	public virtual IWebPlugin GetPlugin()
	{
		return GetPlugin(null);
	}

	public virtual IWebPlugin GetPlugin(WebPluginType[] preferredPlugins)
	{
		if (_windowsPlugin != null)
		{
			return _windowsPlugin;
		}
		throw new WebViewUnavailableException("The 3D WebView for Windows plugin is not currently installed. For more info, please visit https://developer.vuplex.com.");
	}

	public static void RegisterAndroidPlugin(IWebPlugin plugin)
	{
		_androidPlugin = plugin;
	}

	public static void RegisterAndroidGeckoPlugin(IWebPlugin plugin)
	{
		_androidGeckoPlugin = plugin;
	}

	public static void RegisterIOSPlugin(IWebPlugin plugin)
	{
		_iosPlugin = plugin;
	}

	public static void RegisterMacPlugin(IWebPlugin plugin)
	{
		_macPlugin = plugin;
	}

	public static void RegisterMockPlugin(IWebPlugin plugin)
	{
		_mockPlugin = plugin;
	}

	public static void RegisterUwpPlugin(IWebPlugin plugin)
	{
		_uwpPlugin = plugin;
	}

	public static void RegisterWebGLPlugin(IWebPlugin plugin)
	{
		_webGLPlugin = plugin;
	}

	public static void RegisterWindowsPlugin(IWebPlugin plugin)
	{
		_windowsPlugin = plugin;
	}

	private void _logMockWarningOnce(string reason)
	{
		if (!_mockWarningLogged)
		{
			_mockWarningLogged = true;
			WebViewLogger.LogWarning(reason + ", so the mock webview will be used" + (Application.isEditor ? " while running in the editor" : "") + ". For more info, please see <em>https://support.vuplex.com/articles/mock-webview</em>.");
		}
	}
}
