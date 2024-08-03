using System;
using System.Threading.Tasks;
using UnityEngine;
using Vuplex.WebView.Internal;

namespace Vuplex.WebView;

public abstract class BaseWebViewPrefab : MonoBehaviour
{
	public bool ClickingEnabled = true;

	[Label("Cursor Icons Enabled (Windows and macOS only)")]
	[Tooltip("(Windows and macOS only) Sets whether the mouse cursor icon is automatically updated based on interaction with the web page. For example, hovering over a link causes the mouse cursor icon to turn into a pointer hand.")]
	public bool CursorIconsEnabled = true;

	[Tooltip("Determines how the prefab handles drag interactions. Note that This property is ignored when running in Native 2D Mode.")]
	public DragMode DragMode;

	[Label("Drag Threshold (px)")]
	[Tooltip("Determines the threshold (in web pixels) for triggering a drag.")]
	public float DragThreshold = 20f;

	public bool HoveringEnabled = true;

	[Label("Initial URL (optional)")]
	[Tooltip("You can set this to the URL that you want to load, or you can leave it blank if you'd rather add a script to load content programmatically with IWebView.LoadUrl() or LoadHtml().")]
	[HideInInspector]
	public string InitialUrl;

	[Tooltip("Determines whether JavaScript console messages are printed to the Unity logs.")]
	public bool LogConsoleMessages;

	[Label("Pixel Density (Windows and macOS only)")]
	[Tooltip("(Windows and macOS only) Sets the webview's pixel density.")]
	public float PixelDensity = 1f;

	[Header("Debugging")]
	[Tooltip("Determines whether remote debugging is enabled.")]
	public bool RemoteDebuggingEnabled;

	public bool ScrollingEnabled = true;

	private float _appliedResolution;

	[SerializeField]
	[HideInInspector]
	private ViewportMaterialView _cachedVideoLayer;

	[SerializeField]
	[HideInInspector]
	private ViewportMaterialView _cachedView;

	private IWebView _cachedWebView;

	private bool _clickIsPending;

	private bool _consoleMessageLoggedHandlerAttached;

	private bool _dragThresholdReached;

	private bool _loggedDragWarning;

	private static bool _loggedHoverWarning;

	protected WebViewOptions _options;

	[SerializeField]
	[HideInInspector]
	private MonoBehaviour _pointerInputDetectorMonoBehaviour;

	private bool _pointerIsDown;

	private Vector2 _pointerDownNormalizedPoint;

	private Vector2 _previousNormalizedDragPoint;

	private Vector2 _previousMovePointerPoint;

	private static bool _remoteDebuggingEnabled;

	protected Vector2 _sizeInUnityUnits;

	private Material _videoMaterial;

	private Material _viewMaterial;

	protected IWebView _webViewForInitialization;

	[SerializeField]
	[HideInInspector]
	private GameObject _webViewGameObject;

	public Material Material
	{
		get
		{
			return _view.Material;
		}
		set
		{
			_view.Material = value;
		}
	}

	public virtual bool Visible
	{
		get
		{
			return _view.Visible;
		}
		set
		{
			_view.Visible = value;
			if (_videoLayerIsEnabled)
			{
				_videoLayer.Visible = value;
			}
		}
	}

	public IWebView WebView
	{
		get
		{
			if (_cachedWebView == null)
			{
				if (_webViewGameObject == null)
				{
					return null;
				}
				_cachedWebView = _webViewGameObject.GetComponent<IWebView>();
			}
			return _cachedWebView;
		}
		private set
		{
			MonoBehaviour monoBehaviour = value as MonoBehaviour;
			if (monoBehaviour == null)
			{
				throw new ArgumentException("The IWebView cannot be set successfully because it's not a MonoBehaviour.");
			}
			_webViewGameObject = monoBehaviour.gameObject;
			_cachedWebView = value;
		}
	}

	private int _heightInPixels => (int)(_sizeInUnityUnits.y * _appliedResolution);

	private IPointerInputDetector _pointerInputDetector
	{
		get
		{
			return _pointerInputDetectorMonoBehaviour as IPointerInputDetector;
		}
		set
		{
			MonoBehaviour monoBehaviour = value as MonoBehaviour;
			if (monoBehaviour == null)
			{
				throw new ArgumentException("The provided IPointerInputDetector can't be successfully set because it's not a MonoBehaviour");
			}
			_pointerInputDetectorMonoBehaviour = monoBehaviour;
		}
	}

	protected ViewportMaterialView _videoLayer
	{
		get
		{
			if (_cachedVideoLayer == null)
			{
				_cachedVideoLayer = _getVideoLayer();
			}
			return _cachedVideoLayer;
		}
	}

	private bool _videoLayerIsEnabled
	{
		get
		{
			if (_videoLayer != null)
			{
				return _videoLayer.gameObject.activeSelf;
			}
			return false;
		}
		set
		{
			if (_videoLayer != null)
			{
				_videoLayer.gameObject.SetActive(value);
			}
		}
	}

	protected ViewportMaterialView _view
	{
		get
		{
			if (_cachedView == null)
			{
				_cachedView = _getView();
			}
			return _cachedView;
		}
	}

	private int _widthInPixels => (int)(_sizeInUnityUnits.x * _appliedResolution);

	[Obsolete("The WebViewPrefab.DragToScrollThreshold property has been removed. Please use DragThreshold instead: https://developer.vuplex.com/webview/WebViewPrefab#DragThreshold", true)]
	public float DragToScrollThreshold { get; set; }

	public virtual event EventHandler<ClickedEventArgs> Clicked;

	public event EventHandler Initialized;

	public virtual event EventHandler<ScrolledEventArgs> Scrolled;

	public void Destroy()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void SetCutoutRect(Rect rect)
	{
		_view.SetCutoutRect(rect);
	}

	public void SetOptionsForInitialization(WebViewOptions options)
	{
		if (WebView != null)
		{
			throw new ArgumentException("SetOptionsForInitialization() was called after the prefab was already initialized. Please call it before initialization instead.");
		}
		_options = options;
	}

	public void SetPointerInputDetector(IPointerInputDetector pointerInputDetector)
	{
		IPointerInputDetector pointerInputDetector2 = _pointerInputDetector;
		_pointerInputDetector = pointerInputDetector;
		if (WebView != null)
		{
			_initPointerInputDetector(WebView, pointerInputDetector2);
		}
	}

	public void SetWebViewForInitialization(IWebView webView)
	{
		if (WebView != null)
		{
			throw new ArgumentException("SetWebViewForInitialization() was called after the prefab was already initialized. Please call it before initialization instead.");
		}
		if (webView != null && !webView.IsInitialized)
		{
			throw new ArgumentException("SetWebViewForInitialization(IWebView) was called with an uninitialized webview, but an initialized webview is required.");
		}
		_webViewForInitialization = webView;
	}

	public Task WaitUntilInitialized()
	{
		TaskCompletionSource<bool> taskSource = new TaskCompletionSource<bool>();
		if (WebView != null)
		{
			taskSource.SetResult(result: true);
		}
		else
		{
			Initialized += delegate
			{
				taskSource.SetResult(result: true);
			};
		}
		return taskSource.Task;
	}

	private void _attachWebViewEventHandlers(IWebView webView)
	{
		if (LogConsoleMessages)
		{
			_consoleMessageLoggedHandlerAttached = true;
			webView.ConsoleMessageLogged += WebView_ConsoleMessageLogged;
		}
		if (webView is IWithChangingTexture withChangingTexture)
		{
			withChangingTexture.TextureChanged += WebView_TextureChanged;
		}
		if (webView is IWithFallbackVideo withFallbackVideo && !_options.disableVideo)
		{
			withFallbackVideo.VideoRectChanged += delegate(object sender, EventArgs<Rect> eventArgs)
			{
				_setVideoRect(eventArgs.Value);
			};
		}
	}

	private Vector2Int _convertNormalizedToPixels(Vector2 normalizedPoint)
	{
		return new Vector2Int((int)(normalizedPoint.x * (float)_widthInPixels), (int)(normalizedPoint.y * (float)_heightInPixels));
	}

	private void _disableHoveringIfNeeded(bool preferNative2DMode)
	{
	}

	private void _enableNativeOnScreenKeyboardIfNeeded(IWebView webView)
	{
		if (webView is IWithNativeOnScreenKeyboard)
		{
			bool nativeOnScreenKeyboardEnabled = _getNativeOnScreenKeyboardEnabled();
			(webView as IWithNativeOnScreenKeyboard).SetNativeOnScreenKeyboardEnabled(nativeOnScreenKeyboardEnabled);
		}
	}

	private void _enableRemoteDebuggingIfNeeded()
	{
		if (RemoteDebuggingEnabled && !_remoteDebuggingEnabled)
		{
			_remoteDebuggingEnabled = true;
			try
			{
				Web.EnableRemoteDebugging();
			}
			catch (Exception ex)
			{
				WebViewLogger.LogError("An exception occurred while enabling remote debugging. On Windows and macOS, this can happen if a prefab with RemoteDebuggingEnabled = true is created after a prior webview has already been initialized. Exception message: " + ex);
			}
		}
	}

	protected abstract float _getResolution();

	protected abstract bool _getNativeOnScreenKeyboardEnabled();

	protected abstract float _getScrollingSensitivity();

	protected abstract ViewportMaterialView _getVideoLayer();

	protected abstract ViewportMaterialView _getView();

	protected async void _initBase(Rect rect, bool preferNative2DMode = false)
	{
		_throwExceptionIfInitialized();
		_sizeInUnityUnits = rect.size;
		_updateResolutionIfNeeded();
		_disableHoveringIfNeeded(preferNative2DMode);
		_enableRemoteDebuggingIfNeeded();
		IWebView webView = await _initWebView(rect, preferNative2DMode);
		_initViews(webView);
		_enableNativeOnScreenKeyboardIfNeeded(webView);
		_attachWebViewEventHandlers(webView);
		if (!_native2DModeEnabled(webView))
		{
			_initPointerInputDetector(webView);
		}
		WebView = webView;
		this.Initialized?.Invoke(this, EventArgs.Empty);
		if (!string.IsNullOrWhiteSpace(InitialUrl))
		{
			if (_webViewForInitialization != null)
			{
				WebViewLogger.LogWarning("Custom InitialUrl value will be ignored because an initialized webview was provided.");
			}
			else
			{
				webView.LoadUrl(InitialUrl.Trim());
			}
		}
	}

	private void _initViews(IWebView webView)
	{
		if (_native2DModeEnabled(webView))
		{
			if (_view != null)
			{
				_view.Visible = false;
				_view.gameObject.SetActive(value: false);
			}
			_videoLayerIsEnabled = false;
			return;
		}
		_viewMaterial = webView.CreateMaterial();
		_view.Material = _viewMaterial;
		if (webView is IWithFallbackVideo { FallbackVideoEnabled: not false } withFallbackVideo)
		{
			_videoMaterial = withFallbackVideo.CreateVideoMaterial();
			_videoLayer.Material = _videoMaterial;
			_setVideoRect(Rect.zero);
		}
		else
		{
			_videoLayerIsEnabled = false;
		}
	}

	private async Task<IWebView> _initWebView(Rect rect, bool preferNative2DMode)
	{
		if (_webViewForInitialization != null)
		{
			return _webViewForInitialization;
		}
		IWebView webView = Web.CreateWebView(_options.preferredPlugins);
		if (preferNative2DMode && webView is IWithNative2DMode)
		{
			IWithNative2DMode native2DWebView = webView as IWithNative2DMode;
			await native2DWebView.InitInNative2DMode(rect);
			native2DWebView.SetVisible(_view.Visible);
			return webView;
		}
		_updatePixelDensityIfNeeded(webView);
		if (webView is IWithFallbackVideo withFallbackVideo && !_options.disableVideo)
		{
			withFallbackVideo.SetFallbackVideoEnabled(enabled: true);
		}
		await webView.Init(_widthInPixels, _heightInPixels);
		if (webView is IWithCursorType withCursorType && CursorIconsEnabled && !VXUtils.XRSettings.enabled)
		{
			withCursorType.CursorTypeChanged += delegate(object sender, EventArgs<string> eventArgs)
			{
				CursorHelper.SetCursorIcon(eventArgs.Value);
			};
		}
		return webView;
	}

	private void _initPointerInputDetector(IWebView webView, IPointerInputDetector previousPointerInputDetector = null)
	{
		if (previousPointerInputDetector != null)
		{
			previousPointerInputDetector.BeganDrag -= InputDetector_BeganDrag;
			previousPointerInputDetector.Dragged -= InputDetector_Dragged;
			previousPointerInputDetector.PointerDown -= InputDetector_PointerDown;
			previousPointerInputDetector.PointerExited -= InputDetector_PointerExited;
			previousPointerInputDetector.PointerMoved -= InputDetector_PointerMoved;
			previousPointerInputDetector.PointerUp -= InputDetector_PointerUp;
			previousPointerInputDetector.Scrolled -= InputDetector_Scrolled;
		}
		if (_pointerInputDetector == null)
		{
			_pointerInputDetector = GetComponentInChildren<IPointerInputDetector>();
		}
		_pointerInputDetector.PointerMovedEnabled = webView is IWithMovablePointer;
		_pointerInputDetector.BeganDrag += InputDetector_BeganDrag;
		_pointerInputDetector.Dragged += InputDetector_Dragged;
		_pointerInputDetector.PointerDown += InputDetector_PointerDown;
		_pointerInputDetector.PointerExited += InputDetector_PointerExited;
		_pointerInputDetector.PointerMoved += InputDetector_PointerMoved;
		_pointerInputDetector.PointerUp += InputDetector_PointerUp;
		_pointerInputDetector.Scrolled += InputDetector_Scrolled;
	}

	private void InputDetector_BeganDrag(object sender, EventArgs<Vector2> eventArgs)
	{
		_dragThresholdReached = false;
		_previousNormalizedDragPoint = _pointerDownNormalizedPoint;
	}

	private void InputDetector_Dragged(object sender, EventArgs<Vector2> eventArgs)
	{
		if (DragMode == DragMode.Disabled || WebView == null)
		{
			return;
		}
		Vector2 value = eventArgs.Value;
		Vector2 previousNormalizedDragPoint = _previousNormalizedDragPoint;
		_previousNormalizedDragPoint = value;
		Vector2Int vector2Int = _convertNormalizedToPixels(_pointerDownNormalizedPoint - value);
		if (!_dragThresholdReached)
		{
			_dragThresholdReached = vector2Int.magnitude > DragThreshold;
		}
		if (DragMode == DragMode.DragWithinPage)
		{
			if (_dragThresholdReached)
			{
				_movePointerIfNeeded(value);
			}
			return;
		}
		Vector2 scrollDelta = previousNormalizedDragPoint - value;
		_scrollIfNeeded(scrollDelta, _pointerDownNormalizedPoint);
		if (_clickIsPending && _dragThresholdReached)
		{
			_clickIsPending = false;
		}
	}

	protected virtual void InputDetector_PointerDown(object sender, PointerEventArgs eventArgs)
	{
		_pointerIsDown = true;
		_pointerDownNormalizedPoint = eventArgs.Point;
		if (!ClickingEnabled || WebView == null)
		{
			return;
		}
		if (DragMode == DragMode.DragWithinPage)
		{
			if (WebView is IWithPointerDownAndUp withPointerDownAndUp)
			{
				withPointerDownAndUp.PointerDown(eventArgs.Point, eventArgs.ToPointerOptions());
				return;
			}
			if (!_loggedDragWarning)
			{
				_loggedDragWarning = true;
				WebViewLogger.LogWarning($"The WebViewPrefab's DragMode is set to DragWithinPage, but the webview implementation for this platform ({WebView.PluginType}) doesn't support the PointerDown and PointerUp methods needed for dragging within a page. For more info, see <em>https://developer.vuplex.com/webview/IWithPointerDownAndUp</em>.");
			}
		}
		_clickIsPending = true;
	}

	private void InputDetector_PointerExited(object sender, EventArgs eventArgs)
	{
		if (HoveringEnabled)
		{
			_movePointerIfNeeded(Vector2.zero);
		}
	}

	private void InputDetector_PointerMoved(object sender, EventArgs<Vector2> eventArgs)
	{
		if (!_pointerIsDown && HoveringEnabled)
		{
			_movePointerIfNeeded(eventArgs.Value);
		}
	}

	protected virtual void InputDetector_PointerUp(object sender, PointerEventArgs eventArgs)
	{
		_pointerIsDown = false;
		if (!ClickingEnabled || WebView == null)
		{
			return;
		}
		IWithPointerDownAndUp withPointerDownAndUp = WebView as IWithPointerDownAndUp;
		if (DragMode == DragMode.DragWithinPage && withPointerDownAndUp != null)
		{
			Vector2 normalizedPoint = ((_convertNormalizedToPixels(_pointerDownNormalizedPoint - eventArgs.Point).magnitude > DragThreshold) ? eventArgs.Point : _pointerDownNormalizedPoint);
			withPointerDownAndUp.PointerUp(normalizedPoint, eventArgs.ToPointerOptions());
		}
		else
		{
			if (!_clickIsPending)
			{
				return;
			}
			_clickIsPending = false;
			if (withPointerDownAndUp == null || _options.clickWithoutStealingFocus)
			{
				WebView.Click(eventArgs.Point, _options.clickWithoutStealingFocus);
			}
			else
			{
				PointerOptions options = eventArgs.ToPointerOptions();
				withPointerDownAndUp.PointerDown(eventArgs.Point, options);
				withPointerDownAndUp.PointerUp(eventArgs.Point, options);
			}
		}
		this.Clicked?.Invoke(this, new ClickedEventArgs(eventArgs.Point));
	}

	private void InputDetector_Scrolled(object sender, ScrolledEventArgs eventArgs)
	{
		float num = _getScrollingSensitivity();
		Vector2 vector = eventArgs.ScrollDelta * num;
		Vector2 scrollDelta = new Vector2(vector.x / _sizeInUnityUnits.x, vector.y / _sizeInUnityUnits.y);
		_scrollIfNeeded(scrollDelta, eventArgs.Point);
	}

	private void _movePointerIfNeeded(Vector2 point)
	{
		if (WebView is IWithMovablePointer withMovablePointer && point != _previousMovePointerPoint)
		{
			_previousMovePointerPoint = point;
			withMovablePointer.MovePointer(point);
		}
	}

	private bool _native2DModeEnabled(IWebView webView)
	{
		if (webView is IWithNative2DMode)
		{
			return (webView as IWithNative2DMode).Native2DModeEnabled;
		}
		return false;
	}

	protected virtual void OnDestroy()
	{
		if (WebView != null && !WebView.IsDisposed)
		{
			WebView.Dispose();
		}
		Destroy();
		if (_viewMaterial != null)
		{
			UnityEngine.Object.Destroy(_viewMaterial.mainTexture);
			UnityEngine.Object.Destroy(_viewMaterial);
		}
		if (_videoMaterial != null)
		{
			UnityEngine.Object.Destroy(_videoMaterial.mainTexture);
			UnityEngine.Object.Destroy(_videoMaterial);
		}
	}

	protected void _resizeWebViewIfNeeded()
	{
		if (WebView != null && WebView.Size != new Vector2(_widthInPixels, _heightInPixels))
		{
			WebView.Resize(_widthInPixels, _heightInPixels);
		}
	}

	private void _scrollIfNeeded(Vector2 scrollDelta, Vector2 point)
	{
		if (ScrollingEnabled && WebView != null && !(scrollDelta == Vector2.zero))
		{
			WebView.Scroll(scrollDelta, point);
			this.Scrolled?.Invoke(this, new ScrolledEventArgs(scrollDelta, point));
		}
	}

	protected abstract void _setVideoLayerPosition(Rect videoRect);

	private void _setVideoRect(Rect videoRect)
	{
		if (!(_videoLayer == null))
		{
			_view.SetCutoutRect(videoRect);
			_setVideoLayerPosition(videoRect);
			float xmin = Math.Max(0f, -1f * videoRect.x / videoRect.width);
			float ymin = Math.Max(0f, -1f * videoRect.y / videoRect.height);
			float xmax = Math.Min(1f, (1f - videoRect.x) / videoRect.width);
			float ymax = Math.Min(1f, (1f - videoRect.y) / videoRect.height);
			Rect rect = Rect.MinMaxRect(xmin, ymin, xmax, ymax);
			if (rect == new Rect(0f, 0f, 1f, 1f))
			{
				rect = Rect.zero;
			}
			_videoLayer.SetCropRect(rect);
		}
	}

	private void _throwExceptionIfInitialized()
	{
		if (WebView != null)
		{
			throw new InvalidOperationException("Init() cannot be called on a WebViewPrefab that has already been initialized.");
		}
	}

	protected virtual void Update()
	{
		_updateResolutionIfNeeded();
		_updatePixelDensityIfNeeded(WebView);
		if (LogConsoleMessages && !_consoleMessageLoggedHandlerAttached && WebView != null)
		{
			_consoleMessageLoggedHandlerAttached = true;
			WebView.ConsoleMessageLogged += WebView_ConsoleMessageLogged;
		}
	}

	private void _updatePixelDensityIfNeeded(IWebView webView)
	{
		if (!(webView is IWithPixelDensity withPixelDensity) || PixelDensity == withPixelDensity.PixelDensity)
		{
			return;
		}
		try
		{
			withPixelDensity.SetPixelDensity(PixelDensity);
		}
		catch (ArgumentException ex)
		{
			WebViewLogger.LogError(ex.ToString());
			PixelDensity = 1f;
		}
	}

	private void _updateResolutionIfNeeded()
	{
		float num = _getResolution();
		if (_appliedResolution != num)
		{
			if (num > 0f)
			{
				_appliedResolution = num;
				_resizeWebViewIfNeeded();
			}
			else
			{
				WebViewLogger.LogWarning("Ignoring invalid Resolution: " + num);
			}
		}
	}

	private void WebView_ConsoleMessageLogged(object sender, ConsoleMessageEventArgs eventArgs)
	{
		if (LogConsoleMessages)
		{
			string text = "[Web Console] " + eventArgs.Message;
			if (eventArgs.Source != null)
			{
				text += $" ({eventArgs.Source}:{eventArgs.Line})";
			}
			switch (eventArgs.Level)
			{
			case ConsoleMessageLevel.Error:
				WebViewLogger.LogError(text, enableFormatting: false);
				break;
			case ConsoleMessageLevel.Warning:
				WebViewLogger.LogWarning(text, enableFormatting: false);
				break;
			default:
				WebViewLogger.Log(text, enableFormatting: false);
				break;
			}
		}
	}

	private void WebView_TextureChanged(object sender, EventArgs<Texture2D> eventArgs)
	{
		Texture texture = _view.Texture;
		if (!(texture is RenderTexture))
		{
			_view.Texture = eventArgs.Value;
			UnityEngine.Object.Destroy(texture);
		}
	}
}
