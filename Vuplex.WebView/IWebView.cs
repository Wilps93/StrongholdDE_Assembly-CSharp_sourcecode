using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Vuplex.WebView;

public interface IWebView
{
	bool IsDisposed { get; }

	bool IsInitialized { get; }

	List<string> PageLoadScripts { get; }

	WebPluginType PluginType { get; }

	Vector2Int Size { get; }

	Texture2D Texture { get; }

	string Title { get; }

	string Url { get; }

	[Obsolete("IWebView.Resolution has been removed in v4. Please use WebViewPrefab.Resolution or CanvasWebViewPrefab.Resolution instead. For more details, please see this article: https://support.vuplex.com/articles/v4-changes#resolution", true)]
	float Resolution { get; }

	[Obsolete("IWebView.SizeInPixels is now deprecated. Please use IWebView.Size instead: https://developer.vuplex.com/webview/IWebView#Size")]
	Vector2 SizeInPixels { get; }

	[Obsolete("IWebView.VideoTexture has been removed. Please use IWithFallbackVideo.VideoTexture instead: https://developer.vuplex.com/webview/IWithFallbackVideo#VideoTexture", true)]
	Texture2D VideoTexture { get; }

	event EventHandler CloseRequested;

	event EventHandler<ConsoleMessageEventArgs> ConsoleMessageLogged;

	event EventHandler<FocusedInputFieldChangedEventArgs> FocusedInputFieldChanged;

	event EventHandler<ProgressChangedEventArgs> LoadProgressChanged;

	event EventHandler<EventArgs<string>> MessageEmitted;

	event EventHandler PageLoadFailed;

	event EventHandler<EventArgs<string>> TitleChanged;

	event EventHandler<UrlChangedEventArgs> UrlChanged;

	[Obsolete("IWebView.VideoRectChanged has been removed. Please use IWithFallbackVideo.VideoRectChanged instead: https://developer.vuplex.com/webview/IWithFallbackVideo#VideoRectChanged", true)]
	event EventHandler<EventArgs<Rect>> VideoRectChanged;

	Task<bool> CanGoBack();

	Task<bool> CanGoForward();

	Task<byte[]> CaptureScreenshot();

	void Click(int xInPixels, int yInPixels, bool preventStealingFocus = false);

	void Click(Vector2 normalizedPoint, bool preventStealingFocus = false);

	void Copy();

	Material CreateMaterial();

	void Cut();

	void Dispose();

	Task<string> ExecuteJavaScript(string javaScript);

	void ExecuteJavaScript(string javaScript, Action<string> callback);

	Task<byte[]> GetRawTextureData();

	void GoBack();

	void GoForward();

	Task Init(int width, int height);

	void LoadHtml(string html);

	void LoadUrl(string url);

	void LoadUrl(string url, Dictionary<string, string> additionalHttpHeaders);

	Vector2Int NormalizedToPoint(Vector2 normalizedPoint);

	void Paste();

	Vector2 PointToNormalized(int xInPixels, int yInPixels);

	void PostMessage(string data);

	void Reload();

	void Resize(int width, int height);

	void Scroll(int scrollDeltaXInPixels, int scrollDeltaYInPixels);

	void Scroll(Vector2 normalizedScrollDelta);

	void Scroll(Vector2 normalizedScrollDelta, Vector2 normalizedPoint);

	void SelectAll();

	void SendKey(string key);

	void SetFocused(bool focused);

	void SetRenderingEnabled(bool enabled);

	void StopLoad();

	Task WaitForNextPageLoadToFinish();

	void ZoomIn();

	void ZoomOut();

	[Obsolete("IWebView.Blur() has been removed. Please use SetFocused(false) instead: https://developer.vuplex.com/webview/IWebView#SetFocused", true)]
	void Blur();

	[Obsolete("The callback-based CanGoBack(Action) version of this method has been removed. Please switch to the Task-based CanGoBack() version instead. If you prefer using a callback instead of awaiting the Task, you can still use a callback like this: CanGoBack().ContinueWith(result => {...})", true)]
	void CanGoBack(Action<bool> callback);

	[Obsolete("The callback-based CanGoForward(Action) version of this method has been removed. Please switch to the Task-based CanGoForward() version instead. If you prefer using a callback instead of awaiting the Task, you can still use a callback like this: CanGoForward().ContinueWith(result => {...})", true)]
	void CanGoForward(Action<bool> callback);

	[Obsolete("The callback-based CaptureScreenshot(Action) version of this method has been removed. Please switch to the Task-based CaptureScreenshot() version instead. If you prefer using a callback instead of awaiting the Task, you can still use a callback like this: CaptureScreenshot().ContinueWith(result => {...})", true)]
	void CaptureScreenshot(Action<byte[]> callback);

	[Obsolete("DisableViewUpdates() has been removed. Please use SetRenderingEnabled(false) instead: https://developer.vuplex.com/webview/IWebView#SetRenderingEnabled", true)]
	void DisableViewUpdates();

	[Obsolete("EnableViewUpdates() has been removed. Please use SetRenderingEnabled(true) instead: https://developer.vuplex.com/webview/IWebView#SetRenderingEnabled", true)]
	void EnableViewUpdates();

	[Obsolete("IWebView.Focus() has been removed. Please use SetFocused(false) instead: https://developer.vuplex.com/webview/IWebView#SetFocused", true)]
	void Focus();

	[Obsolete("The callback-based GetRawTextureData(Action) version of this method has been removed. Please switch to the Task-based GetRawTextureData() version instead. If you prefer using a callback instead of awaiting the Task, you can still use a callback like this: GetRawTextureData().ContinueWith(result => {...})", true)]
	void GetRawTextureData(Action<byte[]> callback);

	[Obsolete("IWebView.HandleKeyboardInput() has been renamed to IWebView.SendKey(). Please switch to SendKey().")]
	void HandleKeyboardInput(string key);

	[Obsolete("IWebView.Init(Texture2D, float, float) has been removed in v4. Please switch to IWebView.Init(int, int) and await the Task it returns. For more details, please see this article: https://support.vuplex.com/articles/v4-changes#init", true)]
	void Init(Texture2D texture, float width, float height);

	[Obsolete("IWebView.Init(Texture2D, float, float, Texture2D) has been removed in v4. Please switch to IWebView.Init(int, int) and await the Task it returns. For more details, please see this article: https://support.vuplex.com/articles/v4-changes#init", true)]
	void Init(Texture2D texture, float width, float height, Texture2D videoTexture);

	[Obsolete("IWebView.SetResolution() has been removed in v4. Please set the WebViewPrefab.Resolution or CanvasWebViewPrefab.Resolution property instead. For more details, please see this article: https://support.vuplex.com/articles/v4-changes#resolution", true)]
	void SetResolution(float pixelsPerUnityUnit);
}
