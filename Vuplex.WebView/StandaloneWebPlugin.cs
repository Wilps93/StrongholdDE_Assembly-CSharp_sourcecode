using System;
using UnityEngine;
using Vuplex.WebView.Internal;

namespace Vuplex.WebView;

public class StandaloneWebPlugin : MonoBehaviour
{
	public ICookieManager CookieManager { get; } = StandaloneCookieManager.Instance;


	public void ClearAllData()
	{
		StandaloneWebView.ClearAllData();
	}

	public void CreateMaterial(Action<Material> callback)
	{
		callback(VXUtils.CreateDefaultMaterial());
	}

	public void EnableRemoteDebugging()
	{
		string text = ((Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) ? "Windows" : "macOS");
		StandaloneWebView.EnableRemoteDebugging(8080);
		WebViewLogger.Log("Enabling remote debugging for " + text + " on port 8080. Please visit http://localhost:8080 using a Chromium-based browser. For more info, see <em>https://support.vuplex.com/articles/how-to-debug-web-content#standalone</em>.");
	}

	public void SetAutoplayEnabled(bool enabled)
	{
		StandaloneWebView.SetAutoplayEnabled(enabled);
	}

	public void SetCameraAndMicrophoneEnabled(bool enabled)
	{
		StandaloneWebView.SetCameraAndMicrophoneEnabled(enabled);
	}

	public void SetIgnoreCertificateErrors(bool ignore)
	{
		StandaloneWebView.SetIgnoreCertificateErrors(ignore);
	}

	public void SetStorageEnabled(bool enabled)
	{
		StandaloneWebView.SetStorageEnabled(enabled);
	}

	public void SetUserAgent(bool mobile)
	{
		StandaloneWebView.GloballySetUserAgent(mobile);
	}

	public void SetUserAgent(string userAgent)
	{
		StandaloneWebView.GloballySetUserAgent(userAgent);
	}

	private void Start()
	{
		Application.quitting += delegate
		{
			StandaloneWebView.TerminateBrowserProcess();
		};
	}
}
