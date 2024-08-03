using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Vuplex.WebView.Internal;

namespace Vuplex.WebView;

internal class MockWebView : MonoBehaviour, IWebView
{
	private TaskCompletionSource<bool> _pageLoadFinishedTaskSource;

	public bool IsDisposed { get; private set; }

	public bool IsInitialized { get; private set; }

	public List<string> PageLoadScripts { get; } = new List<string>();


	public WebPluginType PluginType { get; } = WebPluginType.Mock;


	public Vector2Int Size { get; private set; }

	public Texture2D Texture { get; private set; }

	public string Title { get; private set; } = "";


	public string Url { get; private set; } = "";


	[Obsolete("IWebView.Resolution has been removed in v4. Please use WebViewPrefab.Resolution or CanvasWebViewPrefab.Resolution instead. For more details, please see this article: https://support.vuplex.com/articles/v4-changes#resolution", true)]
	public float Resolution { get; }

	[Obsolete("IWebView.SizeInPixels is now deprecated. Please use IWebView.Size instead: https://developer.vuplex.com/webview/IWebView#Size")]
	public Vector2 SizeInPixels => Size;

	[Obsolete("IWebView.VideoTexture has been removed. Please use IWithFallbackVideo.VideoTexture instead: https://developer.vuplex.com/webview/IWithFallbackVideo#VideoTexture", true)]
	public Texture2D VideoTexture { get; }

	public event EventHandler CloseRequested;

	public event EventHandler<ConsoleMessageEventArgs> ConsoleMessageLogged;

	public event EventHandler<FocusedInputFieldChangedEventArgs> FocusedInputFieldChanged;

	public event EventHandler<ProgressChangedEventArgs> LoadProgressChanged;

	public event EventHandler<EventArgs<string>> MessageEmitted;

	public event EventHandler PageLoadFailed;

	public event EventHandler<EventArgs<string>> TitleChanged;

	public event EventHandler<UrlChangedEventArgs> UrlChanged;

	[Obsolete("IWebView.VideoRectChanged has been removed. Please use IWithFallbackVideo.VideoRectChanged instead: https://developer.vuplex.com/webview/IWithFallbackVideo#VideoRectChanged", true)]
	public event EventHandler<EventArgs<Rect>> VideoRectChanged;

	public Task<bool> CanGoBack()
	{
		_log("CanGoBack()");
		return Task.FromResult(result: false);
	}

	public Task<bool> CanGoForward()
	{
		_log("CanGoForward()");
		return Task.FromResult(result: false);
	}

	public Task<byte[]> CaptureScreenshot()
	{
		_log("CaptureScreenshot()");
		return Task.FromResult(new byte[0]);
	}

	public void Click(int xInPixels, int yInPixels, bool preventStealingFocus = false)
	{
		if (xInPixels < 0 || xInPixels > Size.x || yInPixels < 0 || yInPixels > Size.y)
		{
			throw new ArgumentException($"The point provided ({xInPixels}px, {yInPixels}px) is not within the bounds of the webview (width: {Size.x}px, height: {Size.y}px).");
		}
		_log($"Click({xInPixels}, {yInPixels}, {preventStealingFocus})");
	}

	public void Click(Vector2 point, bool preventStealingFocus = false)
	{
		_assertValidNormalizedPoint(point);
		_log(string.Format("Click({0}, {1})", point.ToString("n4"), preventStealingFocus));
	}

	public void Copy()
	{
		_log("Copy()");
	}

	public Material CreateMaterial()
	{
		Material material = new Material(Resources.Load<Material>("MockViewportMaterial"));
		Texture2D texture2D = new Texture2D(material.mainTexture.width, material.mainTexture.height, TextureFormat.RGBA32, mipChain: true);
		texture2D.SetPixels((material.mainTexture as Texture2D).GetPixels());
		texture2D.Apply();
		material.mainTexture = texture2D;
		return material;
	}

	public void Cut()
	{
		_log("Cut()");
	}

	public static Task<bool> DeleteCookies(string url, string cookieName = null)
	{
		if (url == null)
		{
			throw new ArgumentException("The url cannot be null.");
		}
		_log("DeleteCookies(\"" + url + "\", \"" + cookieName + "\")");
		return Task.FromResult(result: true);
	}

	public void Dispose()
	{
		IsDisposed = true;
		_log("Dispose()");
		if (this != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public Task<string> ExecuteJavaScript(string javaScript)
	{
		TaskCompletionSource<string> taskCompletionSource = new TaskCompletionSource<string>();
		ExecuteJavaScript(javaScript, taskCompletionSource.SetResult);
		return taskCompletionSource.Task;
	}

	public void ExecuteJavaScript(string javaScript, Action<string> callback)
	{
		_log("ExecuteJavaScript(\"" + _truncateIfNeeded(javaScript) + "\")");
		callback("");
	}

	public static Task<Cookie[]> GetCookies(string url, string cookieName = null)
	{
		if (url == null)
		{
			throw new ArgumentException("The url cannot be null.");
		}
		_log("GetCookies(\"" + url + "\", \"" + cookieName + "\")");
		new TaskCompletionSource<Cookie[]>();
		return Task.FromResult(new Cookie[0]);
	}

	public Task<byte[]> GetRawTextureData()
	{
		_log("GetRawTextureData()");
		return Task.FromResult(new byte[0]);
	}

	public void GoBack()
	{
		_log("GoBack()");
	}

	public void GoForward()
	{
		_log("GoForward()");
	}

	public Task Init(int width, int height)
	{
		Texture = new Texture2D(width, height, TextureFormat.RGBA32, mipChain: false);
		Size = new Vector2Int(width, height);
		IsInitialized = true;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		_log("Init() width: " + width.ToString("n4") + ", height: " + height.ToString("n4"));
		return Task.FromResult(result: true);
	}

	public static MockWebView Instantiate()
	{
		return new GameObject("MockWebView").AddComponent<MockWebView>();
	}

	public virtual void LoadHtml(string html)
	{
		string text2 = (Url = _truncateIfNeeded(html));
		_log("LoadHtml(\"" + text2 + "...\")");
		_handlePageLoad(text2);
	}

	public virtual void LoadUrl(string url)
	{
		LoadUrl(url, null);
	}

	public virtual void LoadUrl(string url, Dictionary<string, string> additionalHttpHeaders)
	{
		Url = url;
		_log("LoadUrl(\"" + url + "\")");
		_handlePageLoad(url);
	}

	public Vector2Int NormalizedToPoint(Vector2 normalizedPoint)
	{
		return new Vector2Int((int)(normalizedPoint.x * (float)Size.x), (int)(normalizedPoint.y * (float)Size.y));
	}

	public void Paste()
	{
		_log("Paste()");
	}

	public Vector2 PointToNormalized(int xInPixels, int yInPixels)
	{
		return new Vector2((float)xInPixels / (float)Size.x, (float)yInPixels / (float)Size.y);
	}

	public void PostMessage(string data)
	{
		_log("PostMessage(\"" + data + "\")");
	}

	public void Reload()
	{
		_log("Reload()");
	}

	public void Resize(int width, int height)
	{
		Size = new Vector2Int(width, height);
		_log("Resize(" + width.ToString("n4") + ", " + height.ToString("n4") + ")");
	}

	public void Scroll(int scrollDeltaX, int scrollDeltaY)
	{
		_log($"Scroll({scrollDeltaX}, {scrollDeltaY})");
	}

	public void Scroll(Vector2 delta)
	{
		_log("Scroll(" + delta.ToString("n4") + ")");
	}

	public void Scroll(Vector2 delta, Vector2 point)
	{
		_assertValidNormalizedPoint(point);
		_log("Scroll(" + delta.ToString("n4") + ", " + point.ToString("n4") + ")");
	}

	public void SelectAll()
	{
		_log("SelectAll()");
	}

	public void SendKey(string input)
	{
		_log("SendKey(\"" + input + "\")");
	}

	public static Task<bool> SetCookie(Cookie cookie)
	{
		if (cookie == null)
		{
			throw new ArgumentException("Cookie cannot be null.");
		}
		if (!cookie.IsValid)
		{
			throw new ArgumentException("Cannot set invalid cookie: " + cookie);
		}
		_log($"SetCookie({cookie}");
		return Task.FromResult(result: true);
	}

	public void SetFocused(bool focused)
	{
		_log($"SetFocused({focused})");
	}

	public void SetRenderingEnabled(bool enabled)
	{
		_log($"SetRenderingEnabled({enabled})");
	}

	public void StopLoad()
	{
		_log("StopLoad()");
	}

	public Task WaitForNextPageLoadToFinish()
	{
		if (_pageLoadFinishedTaskSource == null)
		{
			_pageLoadFinishedTaskSource = new TaskCompletionSource<bool>();
		}
		return _pageLoadFinishedTaskSource.Task;
	}

	public void ZoomIn()
	{
		_log("ZoomIn()");
	}

	public void ZoomOut()
	{
		_log("ZoomOut()");
	}

	private void _assertValidNormalizedPoint(Vector2 normalizedPoint)
	{
		if (!(normalizedPoint.x >= 0f) || !(normalizedPoint.x <= 1f) || !(normalizedPoint.y >= 0f) || !(normalizedPoint.y <= 1f))
		{
			throw new ArgumentException("The normalized point provided is invalid. The x and y values of normalized points must be in the range of [0, 1], but the value provided was " + normalizedPoint.ToString("n4") + ". For more info, please see https://support.vuplex.com/articles/normalized-points");
		}
	}

	private void _handlePageLoad(string url)
	{
		this.UrlChanged?.Invoke(this, new UrlChangedEventArgs(url, UrlActionType.Load));
		this.LoadProgressChanged?.Invoke(this, new ProgressChangedEventArgs(ProgressChangeType.Started, 0f));
		this.LoadProgressChanged?.Invoke(this, new ProgressChangedEventArgs(ProgressChangeType.Finished, 1f));
		_pageLoadFinishedTaskSource?.SetResult(result: true);
		_pageLoadFinishedTaskSource = null;
	}

	private static void _log(string message)
	{
		WebViewLogger.Log("[MockWebView] " + message);
	}

	private string _truncateIfNeeded(string str)
	{
		int num = 25;
		if (str.Length <= num)
		{
			return str;
		}
		return str.Substring(0, num) + "...";
	}

	[Obsolete("IWebView.Blur() has been removed. Please use SetFocused(false) instead: https://developer.vuplex.com/webview/IWebView#SetFocused", true)]
	public void Blur()
	{
	}

	[Obsolete("The callback-based CanGoBack(Action) version of this method has been removed. Please switch to the Task-based CanGoBack() version instead. If you prefer using a callback instead of awaiting the Task, you can still use a callback like this: CanGoBack().ContinueWith(result => {...})", true)]
	public void CanGoBack(Action<bool> callback)
	{
	}

	[Obsolete("The callback-based CanGoForward(Action) version of this method has been removed. Please switch to the Task-based CanGoForward() version instead. If you prefer using a callback instead of awaiting the Task, you can still use a callback like this: CanGoForward().ContinueWith(result => {...})", true)]
	public void CanGoForward(Action<bool> callback)
	{
	}

	[Obsolete("The callback-based CaptureScreenshot(Action) version of this method has been removed. Please switch to the Task-based CaptureScreenshot() version instead. If you prefer using a callback instead of awaiting the Task, you can still use a callback like this: CaptureScreenshot().ContinueWith(result => {...})", true)]
	public void CaptureScreenshot(Action<byte[]> callback)
	{
	}

	[Obsolete("DisableViewUpdates() has been removed. Please use SetRenderingEnabled(false) instead: https://developer.vuplex.com/webview/IWebView#SetRenderingEnabled", true)]
	public void DisableViewUpdates()
	{
	}

	[Obsolete("EnableViewUpdates() has been removed. Please use SetRenderingEnabled(true) instead: https://developer.vuplex.com/webview/IWebView#SetRenderingEnabled", true)]
	public void EnableViewUpdates()
	{
	}

	[Obsolete("IWebView.Focus() has been removed. Please use SetFocused(false) instead: https://developer.vuplex.com/webview/IWebView#SetFocused", true)]
	public void Focus()
	{
	}

	[Obsolete("The callback-based GetRawTextureData(Action) version of this method has been removed. Please switch to the Task-based GetRawTextureData() version instead. If you prefer using a callback instead of awaiting the Task, you can still use a callback like this: GetRawTextureData().ContinueWith(result => {...})", true)]
	public void GetRawTextureData(Action<byte[]> callback)
	{
	}

	[Obsolete("IWebView.HandleKeyboardInput() has been renamed to IWebView.SendKey(). Please switch to SendKey().")]
	public void HandleKeyboardInput(string key)
	{
		SendKey(key);
	}

	[Obsolete("IWebView.Init(Texture2D, float, float) has been removed in v4. Please switch to IWebView.Init(int, int) and await the Task it returns. For more details, please see this article: https://support.vuplex.com/articles/v4-changes#init", true)]
	public void Init(Texture2D texture, float width, float height)
	{
	}

	[Obsolete("IWebView.Init(Texture2D, float, float, Texture2D) has been removed in v4. Please switch to IWebView.Init(int, int) and await the Task it returns. For more details, please see this article: https://support.vuplex.com/articles/v4-changes#init", true)]
	public void Init(Texture2D texture, float width, float height, Texture2D videoTexture)
	{
	}

	[Obsolete("IWebView.SetResolution() has been removed in v4. Please set the WebViewPrefab.Resolution or CanvasWebViewPrefab.Resolution property instead. For more details, please see this article: https://support.vuplex.com/articles/v4-changes#resolution", true)]
	public void SetResolution(float pixelsPerUnityUnit)
	{
	}
}
