using System;
using UnityEngine;
using Vuplex.WebView.Internal;

namespace Vuplex.WebView;

internal class MockWebPlugin : IWebPlugin
{
	private static MockWebPlugin _instance;

	public ICookieManager CookieManager { get; } = MockCookieManager.Instance;


	public static MockWebPlugin Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new MockWebPlugin();
			}
			return _instance;
		}
	}

	public WebPluginType Type { get; } = WebPluginType.Mock;


	public void ClearAllData()
	{
	}

	public void CreateMaterial(Action<Material> callback)
	{
		Material material = new Material(Resources.Load<Material>("MockViewportMaterial"));
		Texture2D texture2D = new Texture2D(material.mainTexture.width, material.mainTexture.height, TextureFormat.RGBA32, mipChain: true);
		texture2D.SetPixels((material.mainTexture as Texture2D).GetPixels());
		texture2D.Apply();
		material.mainTexture = texture2D;
		Dispatcher.RunOnMainThread(delegate
		{
			callback(material);
		});
	}

	public virtual IWebView CreateWebView()
	{
		return MockWebView.Instantiate();
	}

	public void EnableRemoteDebugging()
	{
	}

	public void SetAutoplayEnabled(bool enabled)
	{
	}

	public void SetCameraAndMicrophoneEnabled(bool enabled)
	{
	}

	public void SetIgnoreCertificateErrors(bool ignore)
	{
	}

	public void SetStorageEnabled(bool enabled)
	{
	}

	public void SetUserAgent(bool mobile)
	{
	}

	public void SetUserAgent(string userAgent)
	{
	}
}
