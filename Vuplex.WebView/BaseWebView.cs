using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using Vuplex.WebView.Internal;

namespace Vuplex.WebView;

public abstract class BaseWebView : MonoBehaviour
{
	protected enum InitState
	{
		Uninitialized,
		InProgress,
		Initialized
	}

	private EventHandler<ConsoleMessageEventArgs> _consoleMessageLogged;

	protected IntPtr _currentNativeTexture;

	protected const string _dllName = "VuplexWebViewWindows";

	private EventHandler<FocusedInputFieldChangedEventArgs> _focusedInputFieldChanged;

	protected InitState _initState;

	protected TaskCompletionSource<bool> _initTaskSource;

	private Material _materialForBlitting;

	protected Vector2Int _native2DPosition;

	protected IntPtr _nativeWebViewPtr;

	private TaskCompletionSource<bool> _pageLoadFinishedTaskSource;

	private List<Action<bool>> _pendingCanGoBackCallbacks = new List<Action<bool>>();

	private List<Action<bool>> _pendingCanGoForwardCallbacks = new List<Action<bool>>();

	protected Dictionary<string, Action<string>> _pendingJavaScriptResultCallbacks = new Dictionary<string, Action<string>>();

	protected bool _renderingEnabled = true;

	private static readonly Regex _streamingAssetsUrlRegex = new Regex("^streaming-assets:(//)?(.*)$", RegexOptions.IgnoreCase);

	public bool IsDisposed { get; protected set; }

	public bool IsInitialized => _initState == InitState.Initialized;

	public List<string> PageLoadScripts { get; } = new List<string>();


	public Vector2Int Size { get; private set; }

	public Texture2D Texture { get; protected set; }

	public string Title { get; private set; } = "";


	public string Url { get; private set; } = "";


	protected Rect _rect
	{
		get
		{
			return new Rect(_native2DPosition, Size);
		}
		set
		{
			Size = new Vector2Int((int)value.width, (int)value.height);
			_native2DPosition = new Vector2Int((int)value.x, (int)value.y);
		}
	}

	[Obsolete("IWebView.Resolution has been removed in v4. Please use WebViewPrefab.Resolution or CanvasWebViewPrefab.Resolution instead. For more details, please see this article: https://support.vuplex.com/articles/v4-changes#resolution", true)]
	public float Resolution { get; }

	[Obsolete("IWebView.SizeInPixels is now deprecated. Please use IWebView.Size instead: https://developer.vuplex.com/webview/IWebView#Size")]
	public Vector2 SizeInPixels => Size;

	[Obsolete("IWebView.VideoTexture has been removed. Please use IWithFallbackVideo.VideoTexture instead: https://developer.vuplex.com/webview/IWithFallbackVideo#VideoTexture", true)]
	public Texture2D VideoTexture { get; }

	public event EventHandler CloseRequested;

	public event EventHandler<ConsoleMessageEventArgs> ConsoleMessageLogged
	{
		add
		{
			_consoleMessageLogged = (EventHandler<ConsoleMessageEventArgs>)Delegate.Combine(_consoleMessageLogged, value);
			if (_consoleMessageLogged != null && _consoleMessageLogged.GetInvocationList().Length == 1)
			{
				_setConsoleMessageEventsEnabled(enabled: true);
			}
		}
		remove
		{
			_consoleMessageLogged = (EventHandler<ConsoleMessageEventArgs>)Delegate.Remove(_consoleMessageLogged, value);
			if (_consoleMessageLogged == null)
			{
				_setConsoleMessageEventsEnabled(enabled: false);
			}
		}
	}

	public event EventHandler<FocusedInputFieldChangedEventArgs> FocusedInputFieldChanged
	{
		add
		{
			_focusedInputFieldChanged = (EventHandler<FocusedInputFieldChangedEventArgs>)Delegate.Combine(_focusedInputFieldChanged, value);
			if (_focusedInputFieldChanged != null && _focusedInputFieldChanged.GetInvocationList().Length == 1)
			{
				_setFocusedInputFieldEventsEnabled(enabled: true);
			}
		}
		remove
		{
			_focusedInputFieldChanged = (EventHandler<FocusedInputFieldChangedEventArgs>)Delegate.Remove(_focusedInputFieldChanged, value);
			if (_focusedInputFieldChanged == null)
			{
				_setFocusedInputFieldEventsEnabled(enabled: false);
			}
		}
	}

	public event EventHandler<ProgressChangedEventArgs> LoadProgressChanged;

	public event EventHandler<EventArgs<string>> MessageEmitted;

	public event EventHandler PageLoadFailed;

	public event EventHandler<EventArgs<string>> TitleChanged;

	public event EventHandler<UrlChangedEventArgs> UrlChanged;

	[Obsolete("IWebView.VideoRectChanged has been removed. Please use IWithFallbackVideo.VideoRectChanged instead: https://developer.vuplex.com/webview/IWithFallbackVideo#VideoRectChanged", true)]
	public event EventHandler<EventArgs<Rect>> VideoRectChanged;

	public virtual Task<bool> CanGoBack()
	{
		_assertValidState();
		TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
		_pendingCanGoBackCallbacks.Add(taskCompletionSource.SetResult);
		WebView_canGoBack(_nativeWebViewPtr);
		return taskCompletionSource.Task;
	}

	public virtual Task<bool> CanGoForward()
	{
		_assertValidState();
		TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
		_pendingCanGoForwardCallbacks.Add(taskCompletionSource.SetResult);
		WebView_canGoForward(_nativeWebViewPtr);
		return taskCompletionSource.Task;
	}

	public virtual Task<byte[]> CaptureScreenshot()
	{
		Texture2D texture2D = _getReadableTexture();
		byte[] result = texture2D.EncodeToPNG();
		UnityEngine.Object.Destroy(texture2D);
		return Task.FromResult(result);
	}

	public virtual void Click(int xInPixels, int yInPixels, bool preventStealingFocus = false)
	{
		_assertValidState();
		_assertPointIsWithinBounds(xInPixels, yInPixels);
		WebView_click(_nativeWebViewPtr, xInPixels, yInPixels);
	}

	public void Click(Vector2 normalizedPoint, bool preventStealingFocus = false)
	{
		_assertValidState();
		Vector2Int vector2Int = _convertNormalizedToPixels(normalizedPoint);
		Click(vector2Int.x, vector2Int.y, preventStealingFocus);
	}

	public virtual async void Copy()
	{
		_assertValidState();
		GUIUtility.systemCopyBuffer = await _getSelectedText();
	}

	public virtual Material CreateMaterial()
	{
		Material material = VXUtils.CreateDefaultMaterial();
		material.mainTexture = Texture;
		return material;
	}

	public virtual async void Cut()
	{
		_assertValidState();
		GUIUtility.systemCopyBuffer = await _getSelectedText();
		SendKey("Backspace");
	}

	public virtual void Dispose()
	{
		_assertValidState();
		IsDisposed = true;
		WebView_destroy(_nativeWebViewPtr);
		_nativeWebViewPtr = IntPtr.Zero;
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

	public virtual void ExecuteJavaScript(string javaScript, Action<string> callback)
	{
		_assertValidState();
		string text = null;
		if (callback != null)
		{
			text = Guid.NewGuid().ToString();
			_pendingJavaScriptResultCallbacks[text] = callback;
		}
		WebView_executeJavaScript(_nativeWebViewPtr, javaScript, text);
	}

	public virtual Task<byte[]> GetRawTextureData()
	{
		Texture2D texture2D = _getReadableTexture();
		byte[] rawTextureData = texture2D.GetRawTextureData();
		UnityEngine.Object.Destroy(texture2D);
		return Task.FromResult(rawTextureData);
	}

	public virtual void GoBack()
	{
		_assertValidState();
		WebView_goBack(_nativeWebViewPtr);
	}

	public virtual void GoForward()
	{
		_assertValidState();
		WebView_goForward(_nativeWebViewPtr);
	}

	public virtual void LoadHtml(string html)
	{
		_assertValidState();
		WebView_loadHtml(_nativeWebViewPtr, html);
	}

	public virtual void LoadUrl(string url)
	{
		_assertValidState();
		WebView_loadUrl(_nativeWebViewPtr, _transformUrlIfNeeded(url));
	}

	public virtual void LoadUrl(string url, Dictionary<string, string> additionalHttpHeaders)
	{
		_assertValidState();
		if (additionalHttpHeaders == null)
		{
			LoadUrl(url);
			return;
		}
		string[] value = additionalHttpHeaders.Keys.Select((string key) => key + ": " + additionalHttpHeaders[key]).ToArray();
		string newlineDelimitedHttpHeaders = string.Join("\n", value);
		WebView_loadUrlWithHeaders(_nativeWebViewPtr, url, newlineDelimitedHttpHeaders);
	}

	public Vector2Int NormalizedToPoint(Vector2 normalizedPoint)
	{
		return new Vector2Int((int)(normalizedPoint.x * (float)Size.x), (int)(normalizedPoint.y * (float)Size.y));
	}

	public virtual void Paste()
	{
		_assertValidState();
		string systemCopyBuffer = GUIUtility.systemCopyBuffer;
		foreach (char c in systemCopyBuffer)
		{
			SendKey(char.ToString(c));
		}
	}

	public Vector2 PointToNormalized(int xInPixels, int yInPixels)
	{
		return new Vector2((float)xInPixels / (float)Size.x, (float)yInPixels / (float)Size.y);
	}

	public virtual void PostMessage(string message)
	{
		string text = message.Replace("\\", "\\\\").Replace("'", "\\\\'").Replace("\n", "\\\\n");
		ExecuteJavaScript("vuplex._emit('message', { data: '" + text + "' })", null);
	}

	public virtual void Reload()
	{
		_assertValidState();
		WebView_reload(_nativeWebViewPtr);
	}

	public virtual void Resize(int width, int height)
	{
		if (width != Size.x || height != Size.y)
		{
			_assertValidState();
			_assertValidSize(width, height);
			VXUtils.ThrowExceptionIfAbnormallyLarge(width, height);
			Size = new Vector2Int(width, height);
			_resize();
		}
	}

	public virtual void Scroll(int scrollDeltaXInPixels, int scrollDeltaYInPixels)
	{
		_assertValidState();
		WebView_scroll(_nativeWebViewPtr, scrollDeltaXInPixels, scrollDeltaYInPixels);
	}

	public void Scroll(Vector2 normalizedScrollDelta)
	{
		_assertValidState();
		Vector2Int vector2Int = _convertNormalizedToPixels(normalizedScrollDelta, assertBetweenZeroAndOne: false);
		Scroll(vector2Int.x, vector2Int.y);
	}

	public virtual void Scroll(Vector2 normalizedScrollDelta, Vector2 normalizedPoint)
	{
		_assertValidState();
		Vector2Int vector2Int = _convertNormalizedToPixels(normalizedScrollDelta, assertBetweenZeroAndOne: false);
		Vector2Int vector2Int2 = _convertNormalizedToPixels(normalizedPoint);
		WebView_scrollAtPoint(_nativeWebViewPtr, vector2Int.x, vector2Int.y, vector2Int2.x, vector2Int2.y);
	}

	public virtual void SelectAll()
	{
		_assertValidState();
		ExecuteJavaScript("(function() {\n                    var element = document.activeElement || document.body;\n                    while (!(element === document.body || element.getAttribute('contenteditable') === 'true')) {\n                        if (typeof element.select === 'function') {\n                            element.select();\n                            return;\n                        }\n                        element = element.parentElement;\n                    }\n                    var range = document.createRange();\n                    range.selectNodeContents(element);\n                    var selection = window.getSelection();\n                    selection.removeAllRanges();\n                    selection.addRange(range);\n                })();", null);
	}

	public virtual void SendKey(string key)
	{
		_assertValidState();
		WebView_sendKey(_nativeWebViewPtr, key);
	}

	public static void SetCameraAndMicrophoneEnabled(bool enabled)
	{
		WebView_setCameraAndMicrophoneEnabled(enabled);
	}

	public virtual void SetFocused(bool focused)
	{
		_assertValidState();
		WebView_setFocused(_nativeWebViewPtr, focused);
	}

	public virtual void SetRenderingEnabled(bool enabled)
	{
		_assertValidState();
		WebView_setRenderingEnabled(_nativeWebViewPtr, enabled);
		_renderingEnabled = enabled;
		if (enabled && _currentNativeTexture != IntPtr.Zero)
		{
			Texture.UpdateExternalTexture(_currentNativeTexture);
		}
	}

	public virtual void StopLoad()
	{
		_assertValidState();
		WebView_stopLoad(_nativeWebViewPtr);
	}

	public Task WaitForNextPageLoadToFinish()
	{
		if (_pageLoadFinishedTaskSource == null)
		{
			_pageLoadFinishedTaskSource = new TaskCompletionSource<bool>();
		}
		return _pageLoadFinishedTaskSource.Task;
	}

	public virtual void ZoomIn()
	{
		_assertValidState();
		WebView_zoomIn(_nativeWebViewPtr);
	}

	public virtual void ZoomOut()
	{
		_assertValidState();
		WebView_zoomOut(_nativeWebViewPtr);
	}

	protected void _assertPointIsWithinBounds(int xInPixels, int yInPixels)
	{
		if (xInPixels < 0 || xInPixels > Size.x || yInPixels < 0 || yInPixels > Size.y)
		{
			throw new ArgumentException($"The point provided ({xInPixels}px, {yInPixels}px) is not within the bounds of the webview (width: {Size.x}px, height: {Size.y}px).");
		}
	}

	protected void _assertSingletonEventHandlerUnset(object handler, string eventName)
	{
		if (handler != null)
		{
			throw new InvalidOperationException(eventName + " supports only one event handler. Please remove the existing handler before adding a new one.");
		}
	}

	private void _assertValidSize(int width, int height)
	{
		if (width <= 0 || height <= 0)
		{
			throw new ArgumentException($"Invalid size: ({width}, {height}). The width and height must both be greater than 0.");
		}
	}

	protected void _assertValidState()
	{
		if (!IsInitialized)
		{
			throw new InvalidOperationException("Methods cannot be called on an uninitialized webview. Prior to calling the webview's methods, please initialize it first by calling IWebView.Init() and awaiting the Task it returns.");
		}
		if (IsDisposed)
		{
			throw new InvalidOperationException("Methods cannot be called on a disposed webview.");
		}
	}

	protected Vector2Int _convertNormalizedToPixels(Vector2 normalizedPoint, bool assertBetweenZeroAndOne = true)
	{
		if (assertBetweenZeroAndOne && (!(normalizedPoint.x >= 0f) || !(normalizedPoint.x <= 1f) || !(normalizedPoint.y >= 0f) || !(normalizedPoint.y <= 1f)))
		{
			throw new ArgumentException("The normalized point provided is invalid. The x and y values of normalized points must be in the range of [0, 1], but the value provided was " + normalizedPoint.ToString("n4") + ". For more info, please see https://support.vuplex.com/articles/normalized-points");
		}
		return new Vector2Int((int)(normalizedPoint.x * (float)Size.x), (int)(normalizedPoint.y * (float)Size.y));
	}

	protected virtual Task<Texture2D> _createTexture(int width, int height)
	{
		VXUtils.ThrowExceptionIfAbnormallyLarge(width, height);
		Texture2D texture2D = new Texture2D(width, height, TextureFormat.RGBA32, mipChain: false, linear: false);
		texture2D = Texture2D.CreateExternalTexture(width, height, TextureFormat.RGBA32, mipChain: false, linear: false, texture2D.GetNativeTexturePtr());
		return Task.FromResult(texture2D);
	}

	protected virtual void _destroyNativeTexture(IntPtr nativeTexture)
	{
		WebView_destroyTexture(nativeTexture, SystemInfo.graphicsDeviceType.ToString());
	}

	private Texture2D _getReadableTexture()
	{
		RenderTexture temporary = RenderTexture.GetTemporary(Size.x, Size.y, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = temporary;
		GL.Clear(clearDepth: true, clearColor: true, Color.clear);
		if (_materialForBlitting == null)
		{
			_materialForBlitting = VXUtils.CreateDefaultMaterial();
		}
		Graphics.Blit(Texture, temporary, _materialForBlitting);
		Texture2D texture2D = new Texture2D(Size.x, Size.y);
		texture2D.ReadPixels(new Rect(0f, 0f, temporary.width, temporary.height), 0, 0);
		texture2D.Apply();
		RenderTexture.active = active;
		RenderTexture.ReleaseTemporary(temporary);
		return texture2D;
	}

	private Task<string> _getSelectedText()
	{
		return ExecuteJavaScript("var element = document.activeElement;\n                if (element instanceof HTMLInputElement || element instanceof HTMLTextAreaElement) {\n                    element.value.substring(element.selectionStart, element.selectionEnd);\n                } else {\n                    window.getSelection().toString();\n                }");
	}

	private void HandleCanGoBackResult(string message)
	{
		bool obj = bool.Parse(message);
		List<Action<bool>> list = new List<Action<bool>>(_pendingCanGoBackCallbacks);
		_pendingCanGoBackCallbacks.Clear();
		foreach (Action<bool> item in list)
		{
			try
			{
				item(obj);
			}
			catch (Exception ex)
			{
				WebViewLogger.LogError("An exception occurred while calling the callback for CanGoBack: " + ex);
			}
		}
	}

	private void HandleCanGoForwardResult(string message)
	{
		bool obj = bool.Parse(message);
		List<Action<bool>> list = new List<Action<bool>>(_pendingCanGoForwardCallbacks);
		_pendingCanGoForwardCallbacks.Clear();
		foreach (Action<bool> item in list)
		{
			try
			{
				item(obj);
			}
			catch (Exception ex)
			{
				WebViewLogger.LogError("An exception occurred while calling the callForward for CanGoForward: " + ex);
			}
		}
	}

	private void HandleCloseRequested(string message)
	{
		this.CloseRequested?.Invoke(this, EventArgs.Empty);
	}

	private void HandleInitFinished(string unusedParam)
	{
		_initState = InitState.Initialized;
		_initTaskSource.SetResult(result: true);
		_initTaskSource = null;
	}

	private void HandleJavaScriptResult(string message)
	{
		string[] array = message.Split(new char[1] { ',' }, 2);
		string resultCallbackId = array[0];
		string result = array[1];
		_handleJavaScriptResult(resultCallbackId, result);
	}

	private void _handleJavaScriptResult(string resultCallbackId, string result)
	{
		Action<string> action = _pendingJavaScriptResultCallbacks[resultCallbackId];
		_pendingJavaScriptResultCallbacks.Remove(resultCallbackId);
		action(result);
	}

	private void HandleLoadFailed(string unusedParam)
	{
		this.PageLoadFailed?.Invoke(this, EventArgs.Empty);
		OnLoadProgressChanged(new ProgressChangedEventArgs(ProgressChangeType.Failed, 1f));
		_pageLoadFinishedTaskSource?.SetException(new PageLoadFailedException("The current web page failed to load."));
		_pageLoadFinishedTaskSource = null;
		if (this.PageLoadFailed == null && this.LoadProgressChanged == null)
		{
			WebViewLogger.LogWarning("A web page failed to load. This can happen if the URL loaded is invalid or if the device has no network connection. To detect and handle page load failures like this, applications can use the IWebView.LoadProgressChanged event or the IWebView.PageLoadFailed event.");
		}
	}

	private void HandleLoadFinished(string unusedParam)
	{
		OnLoadProgressChanged(new ProgressChangedEventArgs(ProgressChangeType.Finished, 1f));
		_pageLoadFinishedTaskSource?.SetResult(result: true);
		_pageLoadFinishedTaskSource = null;
		foreach (string pageLoadScript in PageLoadScripts)
		{
			ExecuteJavaScript(pageLoadScript, null);
		}
	}

	private void HandleLoadStarted(string unusedParam)
	{
		OnLoadProgressChanged(new ProgressChangedEventArgs(ProgressChangeType.Started, 0f));
	}

	private void HandleLoadProgressUpdate(string progressString)
	{
		float progress = float.Parse(progressString, CultureInfo.InvariantCulture);
		OnLoadProgressChanged(new ProgressChangedEventArgs(ProgressChangeType.Updated, progress));
	}

	protected virtual void HandleMessageEmitted(string serializedMessage)
	{
		switch (serializedMessage.Contains("vuplex.webview") ? BridgeMessage.ParseType(serializedMessage) : null)
		{
		case "vuplex.webview.consoleMessageLogged":
		{
			ConsoleBridgeMessage consoleBridgeMessage = JsonUtility.FromJson<ConsoleBridgeMessage>(serializedMessage);
			_consoleMessageLogged?.Invoke(this, consoleBridgeMessage.ToEventArgs());
			break;
		}
		case "vuplex.webview.focusedInputFieldChanged":
		{
			FocusedInputFieldType type = FocusedInputFieldChangedEventArgs.ParseType(StringBridgeMessage.ParseValue(serializedMessage));
			_focusedInputFieldChanged?.Invoke(this, new FocusedInputFieldChangedEventArgs(type));
			break;
		}
		case "vuplex.webview.javaScriptResult":
		{
			StringWithIdBridgeMessage stringWithIdBridgeMessage = JsonUtility.FromJson<StringWithIdBridgeMessage>(serializedMessage);
			_handleJavaScriptResult(stringWithIdBridgeMessage.id, stringWithIdBridgeMessage.value);
			break;
		}
		case "vuplex.webview.titleChanged":
			Title = StringBridgeMessage.ParseValue(serializedMessage);
			this.TitleChanged?.Invoke(this, new EventArgs<string>(Title));
			break;
		case "vuplex.webview.urlChanged":
		{
			UrlAction urlAction = JsonUtility.FromJson<UrlChangedMessage>(serializedMessage).urlAction;
			if (!(Url == urlAction.Url))
			{
				Url = urlAction.Url;
				this.UrlChanged?.Invoke(this, new UrlChangedEventArgs(urlAction.Url, urlAction.Type));
			}
			break;
		}
		default:
			this.MessageEmitted?.Invoke(this, new EventArgs<string>(serializedMessage));
			break;
		}
	}

	protected virtual void HandleTextureChanged(string textureString)
	{
		IntPtr intPtr = new IntPtr((long)ulong.Parse(textureString));
		if (!(intPtr == _currentNativeTexture))
		{
			IntPtr currentNativeTexture = _currentNativeTexture;
			_currentNativeTexture = intPtr;
			if (_renderingEnabled)
			{
				Texture.UpdateExternalTexture(intPtr);
			}
			if (currentNativeTexture != IntPtr.Zero)
			{
				_destroyNativeTexture(currentNativeTexture);
			}
		}
	}

	protected async Task _initBase(int width, int height, bool createTexture = true, bool asyncInit = false)
	{
		if (_initState != 0)
		{
			throw new InvalidOperationException((_initState == InitState.Initialized) ? "Init() cannot be called on a webview that has already been initialized." : "Init() cannot be called on a webview that is already in the process of initialization.");
		}
		_assertValidSize(width, height);
		base.gameObject.name = "WebView-" + Guid.NewGuid().ToString();
		Size = new Vector2Int(width, height);
		VXUtils.ThrowExceptionIfAbnormallyLarge(width, height);
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		if (createTexture)
		{
			Texture = await _createTexture(width, height);
		}
		if (asyncInit)
		{
			_initState = InitState.InProgress;
			_initTaskSource = new TaskCompletionSource<bool>();
		}
		else
		{
			_initState = InitState.Initialized;
		}
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void _logDeprecationErrorIfNeeded()
	{
	}

	protected virtual void OnLoadProgressChanged(ProgressChangedEventArgs eventArgs)
	{
		this.LoadProgressChanged?.Invoke(this, eventArgs);
	}

	protected ConsoleMessageLevel _parseConsoleMessageLevel(string levelString)
	{
		switch (levelString)
		{
		case "DEBUG":
			return ConsoleMessageLevel.Debug;
		case "ERROR":
			return ConsoleMessageLevel.Error;
		case "LOG":
			return ConsoleMessageLevel.Log;
		case "WARNING":
			return ConsoleMessageLevel.Warning;
		default:
			WebViewLogger.LogWarning("Unrecognized console message level: " + levelString);
			return ConsoleMessageLevel.Log;
		}
	}

	protected virtual void _resize()
	{
		WebView_resize(_nativeWebViewPtr, Size.x, Size.y);
	}

	protected virtual void _setConsoleMessageEventsEnabled(bool enabled)
	{
		_assertValidState();
		WebView_setConsoleMessageEventsEnabled(_nativeWebViewPtr, enabled);
	}

	protected virtual void _setFocusedInputFieldEventsEnabled(bool enabled)
	{
		_assertValidState();
		WebView_setFocusedInputFieldEventsEnabled(_nativeWebViewPtr, enabled);
	}

	protected string _transformUrlIfNeeded(string originalUrl)
	{
		if (originalUrl == null)
		{
			throw new ArgumentException("URL cannot be null.");
		}
		if (!originalUrl.Contains(":"))
		{
			if (!originalUrl.Contains("."))
			{
				throw new ArgumentException("Invalid URL: " + originalUrl);
			}
			string text = "https://" + originalUrl;
			WebViewLogger.LogWarning("The provided URL is missing a protocol (e.g. http://, https://), so it will default to https://. Original URL: " + originalUrl + ", Updated URL: " + text);
			return text;
		}
		Match match = _streamingAssetsUrlRegex.Match(originalUrl);
		if (match.Success)
		{
			string value = match.Groups[2].Captures[0].Value;
			return (Application.streamingAssetsPath.Contains("://") ? "" : "file://") + Path.Combine(Application.streamingAssetsPath, value).Replace(" ", "%20");
		}
		return originalUrl;
	}

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_canGoBack(IntPtr webViewPtr);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_canGoForward(IntPtr webViewPtr);

	[DllImport("VuplexWebViewWindows")]
	protected static extern void WebView_click(IntPtr webViewPtr, int x, int y);

	[DllImport("VuplexWebViewWindows")]
	protected static extern void WebView_destroyTexture(IntPtr texture, string graphicsApi);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_destroy(IntPtr webViewPtr);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_executeJavaScript(IntPtr webViewPtr, string javaScript, string resultCallbackId);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_goBack(IntPtr webViewPtr);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_goForward(IntPtr webViewPtr);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_sendKey(IntPtr webViewPtr, string input);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_loadHtml(IntPtr webViewPtr, string html);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_loadUrl(IntPtr webViewPtr, string url);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_loadUrlWithHeaders(IntPtr webViewPtr, string url, string newlineDelimitedHttpHeaders);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_reload(IntPtr webViewPtr);

	[DllImport("VuplexWebViewWindows")]
	protected static extern void WebView_resize(IntPtr webViewPtr, int width, int height);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_scroll(IntPtr webViewPtr, int deltaX, int deltaY);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_scrollAtPoint(IntPtr webViewPtr, int deltaX, int deltaY, int pointerX, int pointerY);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_setCameraAndMicrophoneEnabled(bool enabled);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_setConsoleMessageEventsEnabled(IntPtr webViewPtr, bool enabled);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_setFocused(IntPtr webViewPtr, bool focused);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_setFocusedInputFieldEventsEnabled(IntPtr webViewPtr, bool enabled);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_setRenderingEnabled(IntPtr webViewPtr, bool enabled);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_stopLoad(IntPtr webViewPtr);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_zoomIn(IntPtr webViewPtr);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_zoomOut(IntPtr webViewPtr);

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
