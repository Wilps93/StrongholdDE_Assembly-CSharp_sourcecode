using System;
using UnityEngine;

namespace Vuplex.WebView.Internal;

public interface IWebPlugin
{
	ICookieManager CookieManager { get; }

	WebPluginType Type { get; }

	void ClearAllData();

	void CreateMaterial(Action<Material> callback);

	IWebView CreateWebView();

	void EnableRemoteDebugging();

	void SetAutoplayEnabled(bool enabled);

	void SetCameraAndMicrophoneEnabled(bool enabled);

	void SetIgnoreCertificateErrors(bool ignore);

	void SetStorageEnabled(bool enabled);

	void SetUserAgent(bool mobile);

	void SetUserAgent(string userAgent);
}
