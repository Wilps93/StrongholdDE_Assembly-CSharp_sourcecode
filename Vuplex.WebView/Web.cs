using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Vuplex.WebView;

public static class Web
{
	private static WebPluginFactory _pluginFactory = new WebPluginFactory();

	private const string CreateMaterialMessage = "Web.CreateMaterial() is now deprecated in v4. Please use IWebView.CreateMaterial() instead: https://developer.vuplex.com/webview/IWebView#CreateMaterial";

	private const string CreateTextureMessage = "Web.CreateTexture() has been removed in v4 because IWebView instances now automatically create their own textures. For more details, please see this article: https://support.vuplex.com/articles/v4-changes#init";

	public static ICookieManager CookieManager => _pluginFactory.GetPlugin().CookieManager;

	public static WebPluginType DefaultPluginType => _pluginFactory.GetPlugin().Type;

	public static void ClearAllData()
	{
		_pluginFactory.GetPlugin().ClearAllData();
	}

	public static IWebView CreateWebView()
	{
		return _pluginFactory.GetPlugin().CreateWebView();
	}

	public static IWebView CreateWebView(WebPluginType[] preferredPlugins)
	{
		return _pluginFactory.GetPlugin(preferredPlugins).CreateWebView();
	}

	public static void EnableRemoteDebugging()
	{
		_pluginFactory.GetPlugin().EnableRemoteDebugging();
	}

	public static void SetAutoplayEnabled(bool enabled)
	{
		_pluginFactory.GetPlugin().SetAutoplayEnabled(enabled);
	}

	public static void SetCameraAndMicrophoneEnabled(bool enabled)
	{
		_pluginFactory.GetPlugin().SetCameraAndMicrophoneEnabled(enabled);
	}

	public static void SetIgnoreCertificateErrors(bool ignore)
	{
		_pluginFactory.GetPlugin().SetIgnoreCertificateErrors(ignore);
	}

	public static void SetStorageEnabled(bool enabled)
	{
		_pluginFactory.GetPlugin().SetStorageEnabled(enabled);
	}

	public static void SetUserAgent(bool mobile)
	{
		_pluginFactory.GetPlugin().SetUserAgent(mobile);
	}

	public static void SetUserAgent(string userAgent)
	{
		_pluginFactory.GetPlugin().SetUserAgent(userAgent);
	}

	internal static void SetPluginFactory(WebPluginFactory pluginFactory)
	{
		_pluginFactory = pluginFactory;
	}

	[Obsolete("Web.CreateMaterial() is now deprecated in v4. Please use IWebView.CreateMaterial() instead: https://developer.vuplex.com/webview/IWebView#CreateMaterial")]
	public static Task<Material> CreateMaterial()
	{
		TaskCompletionSource<Material> taskCompletionSource = new TaskCompletionSource<Material>();
		_pluginFactory.GetPlugin().CreateMaterial(taskCompletionSource.SetResult);
		return taskCompletionSource.Task;
	}

	[Obsolete("Web.CreateMaterial() is now deprecated in v4. Please use IWebView.CreateMaterial() instead: https://developer.vuplex.com/webview/IWebView#CreateMaterial")]
	public static void CreateMaterial(Action<Material> callback)
	{
		_pluginFactory.GetPlugin().CreateMaterial(callback);
	}

	[Obsolete("Web.CreateTexture() has been removed in v4 because IWebView instances now automatically create their own textures. For more details, please see this article: https://support.vuplex.com/articles/v4-changes#init", true)]
	public static Task<Texture2D> CreateTexture(int width, int height)
	{
		return null;
	}

	[Obsolete("Web.CreateTexture() has been removed in v4 because IWebView instances now automatically create their own textures. For more details, please see this article: https://support.vuplex.com/articles/v4-changes#init", true)]
	public static void CreateTexture(float width, float height, Action<Texture2D> callback)
	{
	}

	[Obsolete("Web.CreateVideoMaterial() has been removed. Please use IWithFallbackVideo.CreateVideoMaterial() instead: https://developer.vuplex.com/webview/IWithFallbackVideo#CreateVideoMaterial", true)]
	public static void CreateVideoMaterial(Action<Material> callback)
	{
	}

	[Obsolete("Web.SetTouchScreenKeyboardEnabled() has been removed. Please use the NativeOnScreenKeyboardEnabled property of WebViewPrefab / CanvasWebViewPrefab or the IWithNativeOnScreenKeyboard interface instead: https://developer.vuplex.com/webview/WebViewPrefab#NativeOnScreenKeyboardEnabled", true)]
	public static void SetTouchScreenKeyboardEnabled(bool enabled)
	{
	}
}
