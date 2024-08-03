using UnityEngine;
using Vuplex.WebView.Internal;

namespace Vuplex.WebView;

internal class WindowsWebPlugin : StandaloneWebPlugin, IWebPlugin
{
	private static WindowsWebPlugin _instance;

	public static WindowsWebPlugin Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GameObject("WindowsWebPlugin").AddComponent<WindowsWebPlugin>();
				Object.DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}

	public WebPluginType Type { get; } = WebPluginType.Windows;


	public virtual IWebView CreateWebView()
	{
		return WindowsWebView.Instantiate();
	}

	private void OnValidate()
	{
		WindowsWebView.ValidateGraphicsApi();
	}
}
