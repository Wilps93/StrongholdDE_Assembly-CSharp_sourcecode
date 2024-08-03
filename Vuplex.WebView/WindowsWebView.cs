using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;
using Vuplex.WebView.Internal;

namespace Vuplex.WebView;

public class WindowsWebView : StandaloneWebView, IWebView
{
	private readonly WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();

	public WebPluginType PluginType { get; } = WebPluginType.Windows;


	public override void Dispose()
	{
		WebView_removePointer(_nativeWebViewPtr);
		base.Dispose();
	}

	public static WindowsWebView Instantiate()
	{
		return new GameObject().AddComponent<WindowsWebView>();
	}

	public static bool ValidateGraphicsApi()
	{
		bool num = SystemInfo.graphicsDeviceType == GraphicsDeviceType.Direct3D11;
		if (!num)
		{
			WebViewLogger.LogError("Unsupported graphics API: 3D WebView for Windows requires Direct3D11. Please go to Player Settings and set \"Graphics APIs for Windows\" to Direct3D11.");
		}
		return num;
	}

	protected override StandaloneWebView _instantiate()
	{
		return Instantiate();
	}

	private void OnEnable()
	{
		StartCoroutine(_renderPluginOncePerFrame());
	}

	private IEnumerator _renderPluginOncePerFrame()
	{
		while (true)
		{
			if (Application.isBatchMode)
			{
				yield return null;
			}
			else
			{
				yield return _waitForEndOfFrame;
			}
			if (_nativeWebViewPtr != IntPtr.Zero && !base.IsDisposed)
			{
				int eventID = WebView_depositPointer(_nativeWebViewPtr);
				GL.IssuePluginEvent(WebView_getRenderFunction(), eventID);
			}
		}
	}

	[DllImport("VuplexWebViewWindows")]
	private static extern int WebView_depositPointer(IntPtr pointer);

	[DllImport("VuplexWebViewWindows")]
	private static extern IntPtr WebView_getRenderFunction();

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_removePointer(IntPtr pointer);
}
