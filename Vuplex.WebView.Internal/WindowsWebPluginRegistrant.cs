using UnityEngine;

namespace Vuplex.WebView.Internal;

internal class WindowsWebPluginRegistrant
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void _registerPlugin()
	{
		WebPluginFactory.RegisterWindowsPlugin(WindowsWebPlugin.Instance);
	}
}
