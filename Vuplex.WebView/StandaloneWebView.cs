using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AOT;
using UnityEngine;
using Vuplex.WebView.Internal;

namespace Vuplex.WebView;

public abstract class StandaloneWebView : BaseWebView, IWithCursorType, IWithDownloads, IWithFileSelection, IWithKeyDownAndUp, IWithMovablePointer, IWithMutableAudio, IWithPixelDensity, IWithPointerDownAndUp, IWithPopups, IWithTouch
{
	private EventHandler<AuthRequestedEventArgs> _authRequestedHandler;

	private static string _cachePathOverride;

	private EventHandler<FileSelectionEventArgs> _fileSelectionHandler;

	private static Dictionary<string, Action<Cookie[]>> _pendingGetCookiesResultCallbacks = new Dictionary<string, Action<Cookie[]>>();

	private static Dictionary<string, Action<bool>> _pendingModifyCookiesResultCallbacks = new Dictionary<string, Action<bool>>();

	private static TaskCompletionSource<bool> _terminationTaskSource;

	public float PixelDensity { get; private set; } = 1f;


	public event EventHandler<AuthRequestedEventArgs> AuthRequested
	{
		add
		{
			_assertSingletonEventHandlerUnset(_authRequestedHandler, "AuthRequested");
			_authRequestedHandler = value;
			WebView_setAuthEnabled(_nativeWebViewPtr, enabled: true);
		}
		remove
		{
			if (_authRequestedHandler == value)
			{
				_authRequestedHandler = null;
				WebView_setAuthEnabled(_nativeWebViewPtr, enabled: false);
			}
		}
	}

	public event EventHandler<EventArgs<string>> CursorTypeChanged
	{
		add
		{
			_cursorTypeChanged += value;
			WebView_setCursorTypeEventsEnabled(_nativeWebViewPtr, enabled: true);
		}
		remove
		{
			_cursorTypeChanged -= value;
			if (this._cursorTypeChanged == value)
			{
				_authRequestedHandler = null;
				WebView_setCursorTypeEventsEnabled(_nativeWebViewPtr, enabled: false);
			}
		}
	}

	public event EventHandler<DownloadChangedEventArgs> DownloadProgressChanged;

	public event EventHandler<FileSelectionEventArgs> FileSelectionRequested
	{
		add
		{
			_assertSingletonEventHandlerUnset(_fileSelectionHandler, "FileSelectionRequested");
			_fileSelectionHandler = value;
			WebView_setFileSelectionEnabled(_nativeWebViewPtr, enabled: true);
		}
		remove
		{
			if (_fileSelectionHandler == value)
			{
				_fileSelectionHandler = null;
				WebView_setFileSelectionEnabled(_nativeWebViewPtr, enabled: false);
			}
		}
	}

	public event EventHandler<PopupRequestedEventArgs> PopupRequested;

	private event EventHandler<EventArgs<string>> _cursorTypeChanged;

	public override Task<bool> CanGoBack()
	{
		return base.CanGoBack();
	}

	public override Task<bool> CanGoForward()
	{
		return base.CanGoForward();
	}

	public override Task<byte[]> CaptureScreenshot()
	{
		return base.CaptureScreenshot();
	}

	public static void ClearAllData()
	{
		if (WebView_browserProcessIsRunning())
		{
			_throwAlreadyInitializedException("ClearAllData");
		}
		string path = _getCachePath();
		if (Directory.Exists(path))
		{
			Directory.Delete(path, recursive: true);
		}
	}

	public override void Copy()
	{
		_assertValidState();
		WebView_copy(_nativeWebViewPtr);
	}

	public override void Cut()
	{
		_assertValidState();
		WebView_cut(_nativeWebViewPtr);
	}

	public static Task<bool> DeleteAllCookies()
	{
		return _deleteCookies();
	}

	public static Task<bool> DeleteCookies(string url, string cookieName = null)
	{
		if (url == null)
		{
			throw new ArgumentException("The url cannot be null.");
		}
		return _deleteCookies(url, cookieName);
	}

	public void SendTouchEvent(TouchEvent touchEvent)
	{
		_assertValidState();
		Vector2Int vector2Int = _convertNormalizedToPixels(touchEvent.Point);
		WebView_sendTouchEvent(_nativeWebViewPtr, touchEvent.TouchID, (int)touchEvent.Type, vector2Int.x, vector2Int.y, touchEvent.RadiusX, touchEvent.RadiusY, touchEvent.RotationAngle, touchEvent.Pressure);
	}

	public static void EnableRemoteDebugging(int portNumber)
	{
		if (1024 > portNumber || portNumber > 65535)
		{
			throw new ArgumentException($"The given port number ({portNumber}) is not in the range from 1024 to 65535.");
		}
		if (!WebView_enableRemoteDebugging(portNumber))
		{
			_throwAlreadyInitializedException("EnableRemoteDebugging");
		}
	}

	public override void ExecuteJavaScript(string javaScript, Action<string> callback)
	{
		base.ExecuteJavaScript(javaScript, callback);
	}

	public static Task<Cookie[]> GetCookies(string url, string cookieName = null)
	{
		if (url == null)
		{
			throw new ArgumentException("The url cannot be null.");
		}
		TaskCompletionSource<Cookie[]> taskCompletionSource = new TaskCompletionSource<Cookie[]>();
		string text = Guid.NewGuid().ToString();
		_pendingGetCookiesResultCallbacks[text] = taskCompletionSource.SetResult;
		WebView_getCookies(url, cookieName, text);
		return taskCompletionSource.Task;
	}

	public override Task<byte[]> GetRawTextureData()
	{
		return base.GetRawTextureData();
	}

	public static void GloballySetUserAgent(bool mobile)
	{
		if (!WebView_globallySetUserAgentToMobile(mobile))
		{
			_throwAlreadyInitializedException("SetUserAgent");
		}
	}

	public static void GloballySetUserAgent(string userAgent)
	{
		if (!WebView_globallySetUserAgent(userAgent))
		{
			_throwAlreadyInitializedException("SetUserAgent");
		}
	}

	public override void GoBack()
	{
		base.GoBack();
	}

	public override void GoForward()
	{
		base.GoForward();
	}

	public async Task Init(int width, int height)
	{
		await _initBase(width, height, createTexture: true, asyncInit: true);
		_nativeWebViewPtr = WebView_new(base.gameObject.name, width, height, PixelDensity, null);
		if (_nativeWebViewPtr == IntPtr.Zero)
		{
			throw new WebViewUnavailableException("Failed to instantiate a new webview. This could indicate that you're using an expired trial version of 3D WebView.");
		}
		await _initTaskSource.Task;
	}

	public void KeyDown(string key, KeyModifier modifiers)
	{
		_assertValidState();
		WebView_keyDown(_nativeWebViewPtr, key, (int)modifiers);
	}

	public void KeyUp(string key, KeyModifier modifiers)
	{
		_assertValidState();
		WebView_keyUp(_nativeWebViewPtr, key, (int)modifiers);
	}

	public override void LoadHtml(string html)
	{
		base.LoadHtml(html);
	}

	public override void LoadUrl(string url)
	{
		base.LoadUrl(url);
	}

	public override void LoadUrl(string url, Dictionary<string, string> additionalHttpHeaders)
	{
		if (additionalHttpHeaders != null)
		{
			foreach (string key in additionalHttpHeaders.Keys)
			{
				if (key.Equals("Accept-Language", StringComparison.InvariantCultureIgnoreCase))
				{
					WebViewLogger.LogError("On Windows and macOS, the Accept-Language request header cannot be set with LoadUrl(url, headers). For more info, please see this article: <em>https://support.vuplex.com/articles/how-to-change-accept-language-header</em>");
				}
			}
		}
		base.LoadUrl(url, additionalHttpHeaders);
	}

	public void MovePointer(Vector2 normalizedPoint)
	{
		_assertValidState();
		Vector2Int vector2Int = _convertNormalizedToPixels(normalizedPoint);
		WebView_movePointer(_nativeWebViewPtr, vector2Int.x, vector2Int.y);
	}

	public override void Paste()
	{
		_assertValidState();
		WebView_paste(_nativeWebViewPtr);
	}

	public void PointerDown(Vector2 point)
	{
		_pointerDown(point, MouseButton.Left, 1);
	}

	public void PointerDown(Vector2 point, PointerOptions options)
	{
		if (options == null)
		{
			options = new PointerOptions();
		}
		_pointerDown(point, options.Button, options.ClickCount);
	}

	public void PointerUp(Vector2 point)
	{
		_pointerUp(point, MouseButton.Left, 1);
	}

	public void PointerUp(Vector2 point, PointerOptions options)
	{
		if (options == null)
		{
			options = new PointerOptions();
		}
		_pointerUp(point, options.Button, options.ClickCount);
	}

	public override void SelectAll()
	{
		_assertValidState();
		WebView_selectAll(_nativeWebViewPtr);
	}

	public void SetAudioMuted(bool muted)
	{
		_assertValidState();
		WebView_setAudioMuted(_nativeWebViewPtr, muted);
	}

	public static void SetAutoplayEnabled(bool enabled)
	{
		if (!WebView_setAutoplayEnabled(enabled))
		{
			_throwAlreadyInitializedException("SetAutoplayEnabled");
		}
	}

	public static void SetCachePath(string absoluteFilePath)
	{
		_cachePathOverride = absoluteFilePath;
		_setCachePath(absoluteFilePath, "SetCachePath");
	}

	public new static void SetCameraAndMicrophoneEnabled(bool enabled)
	{
		if (!WebView_setCameraAndMicrophoneEnabled(enabled))
		{
			_throwAlreadyInitializedException("SetCameraAndMicrophoneEnabled");
		}
	}

	public static void SetChromiumLogLevel(ChromiumLogLevel level)
	{
		if (!WebView_setChromiumLogLevel((int)level))
		{
			_throwAlreadyInitializedException("SetChromiumLogLevel");
		}
	}

	public static void SetCommandLineArguments(string args)
	{
		if (!WebView_setCommandLineArguments(args))
		{
			_throwAlreadyInitializedException("SetCommandLineArguments");
		}
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
		TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
		string text = Guid.NewGuid().ToString();
		_pendingModifyCookiesResultCallbacks[text] = taskCompletionSource.SetResult;
		WebView_setCookie(cookie.ToJson(), text);
		return taskCompletionSource.Task;
	}

	public void SetDownloadsEnabled(bool enabled)
	{
		_assertValidState();
		string downloadsDirectoryPath = (enabled ? Path.Combine(Application.temporaryCachePath, Path.Combine("Vuplex.WebView", "downloads")) : "");
		WebView_setDownloadsEnabled(_nativeWebViewPtr, downloadsDirectoryPath);
	}

	public static void SetIgnoreCertificateErrors(bool ignore)
	{
		if (!WebView_setIgnoreCertificateErrors(ignore))
		{
			_throwAlreadyInitializedException("SetIgnoreCertificateErrors");
		}
	}

	public void SetNativeFileDialogEnabled(bool enabled)
	{
		_assertValidState();
		WebView_setNativeFileDialogEnabled(_nativeWebViewPtr, enabled);
	}

	public void SetNativeScriptDialogEnabled(bool enabled)
	{
		_assertValidState();
		WebView_setNativeScriptDialogEnabled(_nativeWebViewPtr, enabled);
	}

	public void SetPixelDensity(float pixelDensity)
	{
		if (pixelDensity <= 0f || pixelDensity > 10f)
		{
			throw new ArgumentException($"Invalid pixel density: {pixelDensity}. The pixel density must be between 0 and 10 (exclusive).");
		}
		PixelDensity = pixelDensity;
		if (base.IsInitialized)
		{
			_resize();
		}
	}

	public void SetPopupMode(PopupMode popupMode)
	{
		_assertValidState();
		WebView_setPopupMode(_nativeWebViewPtr, (int)popupMode);
	}

	public static void SetScreenSharingEnabled(bool enabled)
	{
		if (!WebView_setScreenSharingEnabled(enabled))
		{
			_throwAlreadyInitializedException("SetScreenSharingEnabled");
		}
	}

	public static void SetStorageEnabled(bool enabled)
	{
		_setCachePath(enabled ? _getCachePath() : "", "SetStorageEnabled");
	}

	public static void SetTargetFrameRate(uint targetFrameRate)
	{
		if (!WebView_setTargetFrameRate(targetFrameRate))
		{
			_throwAlreadyInitializedException("SetTargetFrameRate");
		}
	}

	public void SetZoomLevel(float zoomLevel)
	{
		_assertValidState();
		WebView_setZoomLevel(_nativeWebViewPtr, zoomLevel);
	}

	public static Task TerminateBrowserProcess()
	{
		if (_terminationTaskSource != null)
		{
			return _terminationTaskSource.Task;
		}
		if (!WebView_terminateBrowserProcess())
		{
			return Task.FromResult(result: true);
		}
		_terminationTaskSource = new TaskCompletionSource<bool>();
		return _terminationTaskSource.Task;
	}

	private static Task<bool> _deleteCookies(string url = null, string cookieName = null)
	{
		TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
		string text = Guid.NewGuid().ToString();
		_pendingModifyCookiesResultCallbacks[text] = taskCompletionSource.SetResult;
		WebView_deleteCookies(url, cookieName, text);
		return taskCompletionSource.Task;
	}

	protected static string _getCachePath()
	{
		return _cachePathOverride ?? Path.Combine(Application.persistentDataPath, "Vuplex.WebView", "chromium-cache");
	}

	private void HandleAuthRequested(string host)
	{
		if (_authRequestedHandler == null)
		{
			WebViewLogger.LogWarning("The native webview sent an auth request, but no event handler is attached to AuthRequested.");
			WebView_cancelAuth(_nativeWebViewPtr);
			return;
		}
		AuthRequestedEventArgs e = new AuthRequestedEventArgs(host, delegate(string username, string password)
		{
			WebView_continueAuth(_nativeWebViewPtr, username, password);
		}, delegate
		{
			WebView_cancelAuth(_nativeWebViewPtr);
		});
		_authRequestedHandler?.Invoke(this, e);
	}

	private void HandleCursorTypeChanged(string type)
	{
		this._cursorTypeChanged?.Invoke(this, new EventArgs<string>(type));
	}

	private void HandleDownloadProgressChanged(string serializedMessage)
	{
		this.DownloadProgressChanged?.Invoke(this, DownloadMessage.FromJson(serializedMessage).ToEventArgs());
	}

	private void HandleFileSelectionRequested(string serializedMessage)
	{
		FileSelectionMessage fileSelectionMessage = FileSelectionMessage.FromJson(serializedMessage);
		Action<string[]> continueCallback = delegate(string[] filePaths)
		{
			string serializedFilePaths = JsonUtility.ToJson(new JsonArrayWrapper<string>(filePaths));
			WebView_continueFileSelection(_nativeWebViewPtr, serializedFilePaths);
		};
		Action cancelCallback = delegate
		{
			WebView_cancelFileSelection(_nativeWebViewPtr);
		};
		FileSelectionEventArgs e = new FileSelectionEventArgs(fileSelectionMessage.AcceptFilters, fileSelectionMessage.MultipleAllowed, continueCallback, cancelCallback);
		_fileSelectionHandler(this, e);
	}

	[AOT.MonoPInvokeCallback(typeof(Action<string, string>))]
	private static void _handleGetCookiesResult(string resultCallbackId, string serializedCookies)
	{
		Action<Cookie[]> action = _pendingGetCookiesResultCallbacks[resultCallbackId];
		_pendingGetCookiesResultCallbacks.Remove(resultCallbackId);
		Cookie[] obj = Cookie.ArrayFromJson(serializedCookies);
		action(obj);
	}

	private void HandlePopup(string message)
	{
		if (this.PopupRequested == null)
		{
			return;
		}
		string[] array = message.Split(new char[1] { ',' }, 2);
		string url = array[0];
		string popupBrowserId = array[1];
		if (popupBrowserId.Length == 0)
		{
			this.PopupRequested?.Invoke(this, new PopupRequestedEventArgs(url, null));
			return;
		}
		StandaloneWebView popupWebView = _instantiate();
		Dispatcher.RunOnMainThread(async delegate
		{
			await popupWebView._initPopup(base.Size.x, base.Size.y, PixelDensity, popupBrowserId);
			this.PopupRequested?.Invoke(this, new PopupRequestedEventArgs(url, popupWebView as IWebView));
		});
	}

	[AOT.MonoPInvokeCallback(typeof(Action<string, bool>))]
	private static void _handleModifyCookiesResult(string resultCallbackId, bool success)
	{
		Action<bool> action = _pendingModifyCookiesResultCallbacks[resultCallbackId];
		_pendingModifyCookiesResultCallbacks.Remove(resultCallbackId);
		action?.Invoke(success);
	}

	[AOT.MonoPInvokeCallback(typeof(Action))]
	private static void _handleTerminationFinished()
	{
		if (_terminationTaskSource != null)
		{
			_terminationTaskSource.SetResult(result: true);
			_terminationTaskSource = null;
		}
	}

	private async Task _initPopup(int width, int height, float pixelDensity, string popupId)
	{
		await _initBase(width, height, createTexture: true, asyncInit: true);
		PixelDensity = pixelDensity;
		_nativeWebViewPtr = WebView_new(base.gameObject.name, width, height, PixelDensity, popupId);
		await _initTaskSource.Task;
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void _initializePlugin()
	{
		WebView_initializePlugin(Marshal.GetFunctionPointerForDelegate<Action>(_handleTerminationFinished), Marshal.GetFunctionPointerForDelegate<Action<string>>(_logInfo), Marshal.GetFunctionPointerForDelegate<Action<string>>(_logWarning), Marshal.GetFunctionPointerForDelegate<Action<string>>(_logError), Marshal.GetFunctionPointerForDelegate<Action<string, string, string>>(_unitySendMessage), Marshal.GetFunctionPointerForDelegate<Action<string, string>>(_handleGetCookiesResult), Marshal.GetFunctionPointerForDelegate<Action<string, bool>>(_handleModifyCookiesResult));
		_setCachePath(_getCachePath(), "_initializePlugin");
	}

	protected abstract StandaloneWebView _instantiate();

	[AOT.MonoPInvokeCallback(typeof(Action<string>))]
	private static void _logInfo(string message)
	{
		WebViewLogger.Log(message, enableFormatting: false);
	}

	[AOT.MonoPInvokeCallback(typeof(Action<string>))]
	private static void _logWarning(string message)
	{
		WebViewLogger.LogWarning(message, enableFormatting: false);
	}

	[AOT.MonoPInvokeCallback(typeof(Action<string>))]
	private static void _logError(string message)
	{
		WebViewLogger.LogError(message, enableFormatting: false);
	}

	protected override void _resize()
	{
		WebView_resizeWithPixelDensity(_nativeWebViewPtr, base.Size.x, base.Size.y, PixelDensity);
	}

	private static void _throwAlreadyInitializedException(string methodName)
	{
		throw new InvalidOperationException("Unable to execute " + methodName + "() because Chromium is already running. On Windows and macOS, " + methodName + "() can only be called before Chromium is started. The easiest way to resolve this issue is by calling " + methodName + "() earlier in the application, like by calling it from Awake() instead of from Start(). Alternatively, you can manually terminate Chromium prior calling the method by using StandaloneWebView.TerminateBrowserProcess(): https://developer.vuplex.com/webview/StandaloneWebView#TerminateBrowserProcess");
	}

	private void _pointerDown(Vector2 normalizedPoint, MouseButton mouseButton, int clickCount)
	{
		_assertValidState();
		Vector2Int vector2Int = _convertNormalizedToPixels(normalizedPoint);
		WebView_pointerDown(_nativeWebViewPtr, vector2Int.x, vector2Int.y, (int)mouseButton, clickCount);
	}

	private void _pointerUp(Vector2 normalizedPoint, MouseButton mouseButton, int clickCount)
	{
		_assertValidState();
		Vector2Int vector2Int = _convertNormalizedToPixels(normalizedPoint);
		WebView_pointerUp(_nativeWebViewPtr, vector2Int.x, vector2Int.y, (int)mouseButton, clickCount);
	}

	private static void _setCachePath(string path, string methodName)
	{
		if (!WebView_setCachePath(path ?? ""))
		{
			_throwAlreadyInitializedException(methodName);
		}
	}

	[AOT.MonoPInvokeCallback(typeof(Action<string, string, string>))]
	private static void _unitySendMessage(string gameObjectName, string methodName, string message)
	{
		Dispatcher.RunOnMainThread(delegate
		{
			GameObject gameObject = GameObject.Find(gameObjectName);
			if (gameObject == null)
			{
				WebViewLogger.LogWarning("Unable to deliver a message from the native plugin to a webview GameObject because there is no longer a GameObject named '" + gameObjectName + "'. This can sometimes happen directly after destroying a webview. In that case, it is benign and this message can be ignored.");
			}
			else
			{
				gameObject.SendMessage(methodName, message);
			}
		});
	}

	[DllImport("VuplexWebViewWindows")]
	private static extern bool WebView_browserProcessIsRunning();

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_cancelAuth(IntPtr webViewPtr);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_cancelFileSelection(IntPtr webViewPtr);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_continueAuth(IntPtr webViewPtr, string username, string password);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_continueFileSelection(IntPtr webViewPtr, string serializedFilePaths);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_copy(IntPtr webViewPtr);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_cut(IntPtr webViewPtr);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_deleteCookies(string url, string cookieName, string resultCallbackId);

	[DllImport("VuplexWebViewWindows")]
	private static extern bool WebView_enableRemoteDebugging(int portNumber);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_getCookies(string url, string cookieName, string resultCallbackId);

	[DllImport("VuplexWebViewWindows")]
	private static extern bool WebView_globallySetUserAgentToMobile(bool mobile);

	[DllImport("VuplexWebViewWindows")]
	private static extern bool WebView_globallySetUserAgent(string userAgent);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_initializePlugin(IntPtr terminationFinishedCallback, IntPtr logInfoFunction, IntPtr logWarningFunction, IntPtr logErrorFunction, IntPtr unitySendMessageFunction, IntPtr getCookiesCallback, IntPtr modifyCookiesCallback);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_keyDown(IntPtr webViewPtr, string key, int modifiers);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_keyUp(IntPtr webViewPtr, string key, int modifiers);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_movePointer(IntPtr webViewPtr, int x, int y);

	[DllImport("VuplexWebViewWindows")]
	private static extern IntPtr WebView_new(string gameObjectName, int width, int height, float pixelDensity, string popupBrowserId);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_paste(IntPtr webViewPtr);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_pointerDown(IntPtr webViewPtr, int x, int y, int mouseButton, int clickCount);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_pointerUp(IntPtr webViewPtr, int x, int y, int mouseButton, int clickCount);

	[DllImport("VuplexWebViewWindows")]
	protected static extern void WebView_resizeWithPixelDensity(IntPtr webViewPtr, int width, int height, float pixelDensity);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_selectAll(IntPtr webViewPtr);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_sendTouchEvent(IntPtr webViewPtr, int touchID, int type, float pointX, float pointY, float radiusX, float radiusY, float rotationAngle, float pressure);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_setAudioMuted(IntPtr webViewPtr, bool muted);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_setAuthEnabled(IntPtr webViewPtr, bool enabled);

	[DllImport("VuplexWebViewWindows")]
	private static extern bool WebView_setAutoplayEnabled(bool enabled);

	[DllImport("VuplexWebViewWindows")]
	private static extern bool WebView_setCachePath(string cachePath);

	[DllImport("VuplexWebViewWindows")]
	private static extern bool WebView_setCameraAndMicrophoneEnabled(bool enabled);

	[DllImport("VuplexWebViewWindows")]
	private static extern bool WebView_setChromiumLogLevel(int level);

	[DllImport("VuplexWebViewWindows")]
	private static extern bool WebView_setCommandLineArguments(string args);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_setCookie(string serializedCookie, string resultCallbackId);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_setCursorTypeEventsEnabled(IntPtr webViewPtr, bool enabled);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_setDownloadsEnabled(IntPtr webViewPtr, string downloadsDirectoryPath);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_setFileSelectionEnabled(IntPtr webViewPtr, bool enabled);

	[DllImport("VuplexWebViewWindows")]
	private static extern bool WebView_setIgnoreCertificateErrors(bool ignore);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_setNativeFileDialogEnabled(IntPtr webViewPtr, bool enabled);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_setNativeScriptDialogEnabled(IntPtr webViewPtr, bool enabled);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_setPopupMode(IntPtr webViewPtr, int popupMode);

	[DllImport("VuplexWebViewWindows")]
	private static extern bool WebView_setScreenSharingEnabled(bool enabled);

	[DllImport("VuplexWebViewWindows")]
	private static extern bool WebView_setTargetFrameRate(uint targetFrameRate);

	[DllImport("VuplexWebViewWindows")]
	private static extern void WebView_setZoomLevel(IntPtr webViewPtr, float zoomLevel);

	[DllImport("VuplexWebViewWindows")]
	private static extern bool WebView_terminateBrowserProcess();

	[Obsolete("StandaloneWebView.DispatchTouchEvent() has been renamed to SendTouchEvent(). Please switch to using IWithTouch.SendTouchEvent(): https://developer.vuplex.com/webview/IWithTouch")]
	public void DispatchTouchEvent(TouchEvent touchEvent)
	{
		SendTouchEvent(touchEvent);
	}

	[Obsolete("StandaloneWebView.GetCookie() is now deprecated. Please switch to using Web.CookieManager.GetCookies(): https://developer.vuplex.com/webview/CookieManager#GetCookies")]
	public static async void GetCookie(string url, string cookieName, Action<Cookie> callback)
	{
		callback(await GetCookie(url, cookieName));
	}

	[Obsolete("StandaloneWebView.GetCookie() is now deprecated. Please switch to Web.CookieManager.GetCookies(): https://developer.vuplex.com/webview/CookieManager#GetCookies")]
	public static async Task<Cookie> GetCookie(string url, string cookieName)
	{
		Cookie[] array = await GetCookies(url, cookieName);
		return (array.Length != 0) ? array[0] : null;
	}

	[Obsolete("StandaloneWebView.GetCookies(url, callback) is now deprecated. Please switch to Web.CookieManager.GetCookies(): https://developer.vuplex.com/webview/CookieManager#GetCookies")]
	public static async void GetCookies(string url, Action<Cookie[]> callback)
	{
		callback(await GetCookies(url));
	}

	[Obsolete("StandaloneWebView.SetAudioAndVideoCaptureEnabled() is now deprecated. Please switch to Web.SetCameraAndMicrophoneEnabled(): https://developer.vuplex.com/webview/Web#SetCameraAndMicrophoneEnabled")]
	public static void SetAudioAndVideoCaptureEnabled(bool enabled)
	{
		SetCameraAndMicrophoneEnabled(enabled);
	}

	[Obsolete("StandaloneWebView.SetCookie(cookie, callback) is now deprecated. Please switch to Web.CookieManager.SetCookie(): https://developer.vuplex.com/webview/CookieManager#SetCookie")]
	public static async void SetCookie(Cookie cookie, Action<bool> callback)
	{
		callback(await SetCookie(cookie));
	}

	[Obsolete("StandaloneWebView.TerminatePlugin() has been replaced with StandaloneWebView.TerminateBrowserProcess(). Please switch to TerminateBrowserProcess().")]
	public static void TerminatePlugin()
	{
		TerminateBrowserProcess();
	}
}
