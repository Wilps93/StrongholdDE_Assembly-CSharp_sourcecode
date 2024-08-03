using System;
using UnityEngine;
using UnityEngine.Serialization;
using Vuplex.WebView.Internal;

namespace Vuplex.WebView;

[HelpURL("https://developer.vuplex.com/webview/CanvasWebViewPrefab")]
public class CanvasWebViewPrefab : BaseWebViewPrefab
{
	[Label("Native 2D Mode (Android, iOS, WebGL, and UWP only)")]
	[Tooltip("Native 2D Mode positions a native 2D webview in front of the Unity game view instead of rendering web content as a texture in the Unity scene. Native 2D Mode provides better performance on iOS and UWP, because the default mode of rendering web content to a texture is slower. \n\nImportant notes:\n• Native 2D Mode is only supported for Android (non-Gecko), iOS, WebGL, and UWP. For the other 3D WebView packages, the default render mode is used instead.\n• Native 2D Mode requires that the canvas's render mode be set to \"Screen Space - Overlay\".")]
	[HideInInspector]
	[Header("Platform-specific")]
	public bool Native2DModeEnabled;

	[Label("Native On-Screen Keyboard (Android and iOS only)")]
	[Tooltip("Determines whether the operating system's native on-screen keyboard is automatically shown when a text input in the webview is focused. The native on-screen keyboard is only supported for the following packages:\n• 3D WebView for Android (non-Gecko)\n• 3D WebView for iOS")]
	public bool NativeOnScreenKeyboardEnabled = true;

	[Label("Resolution (px / Unity unit)")]
	[Tooltip("You can change this to make web content appear larger or smaller. Note that This property is ignored when running in Native 2D Mode.")]
	[HideInInspector]
	[FormerlySerializedAs("InitialResolution")]
	public float Resolution = 1f;

	[HideInInspector]
	[Tooltip("Determines the scroll sensitivity. Note that This property is ignored when running in Native 2D Mode.")]
	public float ScrollingSensitivity = 15f;

	private RectTransform _cachedRectTransform;

	private CachingGetter<Canvas> _canvasGetter;

	private static Resolution _originalScreenResolution;

	private bool _setCustomPointerInputDetector;

	public override bool Visible
	{
		get
		{
			return _getNative2DWebViewIfActive()?.Visible ?? base.Visible;
		}
		set
		{
			IWithNative2DMode withNative2DMode = _getNative2DWebViewIfActive();
			if (withNative2DMode != null)
			{
				withNative2DMode.SetVisible(value);
			}
			else
			{
				base.Visible = value;
			}
		}
	}

	private Canvas _canvas
	{
		get
		{
			if (_canvasGetter == null)
			{
				_canvasGetter = new CachingGetter<Canvas>(base.GetComponentInParent<Canvas>, 1, this);
			}
			return _canvasGetter.GetValue();
		}
	}

	private bool _native2DModeActive
	{
		get
		{
			if (base.WebView is IWithNative2DMode withNative2DMode)
			{
				return withNative2DMode.Native2DModeEnabled;
			}
			return false;
		}
	}

	private RectTransform _rectTransform
	{
		get
		{
			if (_cachedRectTransform == null)
			{
				_cachedRectTransform = GetComponent<RectTransform>();
			}
			return _cachedRectTransform;
		}
	}

	[Obsolete("CanvasWebViewPrefab.InitialResolution is now deprecated. Please use CanvasWebViewPrefab.Resolution instead.")]
	public float InitialResolution
	{
		get
		{
			return Resolution;
		}
		set
		{
			Resolution = value;
		}
	}

	public override event EventHandler<ClickedEventArgs> Clicked
	{
		add
		{
			if (_native2DModeActive)
			{
				_logNative2DModeWarning("The CanvasWebViewPrefab.Clicked event is not supported in Native 2D Mode.");
			}
			base.Clicked += value;
		}
		remove
		{
			base.Clicked -= value;
		}
	}

	public override event EventHandler<ScrolledEventArgs> Scrolled
	{
		add
		{
			if (_native2DModeActive)
			{
				_logNative2DModeWarning("The CanvasWebViewPrefab.Scrolled event is not supported in Native 2D Mode.");
			}
			base.Scrolled += value;
		}
		remove
		{
			base.Scrolled -= value;
		}
	}

	public static CanvasWebViewPrefab Instantiate()
	{
		return Instantiate(default(WebViewOptions));
	}

	public static CanvasWebViewPrefab Instantiate(WebViewOptions options)
	{
		CanvasWebViewPrefab component = UnityEngine.Object.Instantiate((GameObject)Resources.Load("CanvasWebViewPrefab")).GetComponent<CanvasWebViewPrefab>();
		component._options = options;
		return component;
	}

	public static CanvasWebViewPrefab Instantiate(IWebView webView)
	{
		CanvasWebViewPrefab component = UnityEngine.Object.Instantiate((GameObject)Resources.Load("CanvasWebViewPrefab")).GetComponent<CanvasWebViewPrefab>();
		component.SetWebViewForInitialization(webView);
		return component;
	}

	private bool _canNative2DModeBeEnabled(bool logWarnings = false)
	{
		Canvas canvas = _canvas;
		if ((object)canvas != null && canvas.renderMode == RenderMode.WorldSpace)
		{
			if (logWarnings)
			{
				_logNative2DModeWarning("CanvasWebViewPrefab.Native2DModeEnabled is enabled but the canvas's render mode is set to World Space, so Native 2D Mode will not be enabled. In order to use Native 2D Mode, please switch the canvas's render mode to \"Screen Space - Overlay\" or \"Screen Space - Camera\".");
			}
			return false;
		}
		if (VXUtils.XRSettings.enabled)
		{
			if (logWarnings)
			{
				_logNative2DModeWarning("CanvasWebViewPrefab.Native2DModeEnabled is enabled but XR is enabled, so Native 2D Mode will not be enabled.");
			}
			return false;
		}
		return true;
	}

	protected override float _getResolution()
	{
		if (Resolution > 0f)
		{
			return Resolution;
		}
		WebViewLogger.LogError("Invalid value set for CanvasWebViewPrefab.Resolution: " + Resolution);
		return 1f;
	}

	private IWithNative2DMode _getNative2DWebViewIfActive()
	{
		if (base.WebView is IWithNative2DMode { Native2DModeEnabled: not false } withNative2DMode)
		{
			return withNative2DMode;
		}
		return null;
	}

	protected override bool _getNativeOnScreenKeyboardEnabled()
	{
		return NativeOnScreenKeyboardEnabled;
	}

	protected override float _getScrollingSensitivity()
	{
		return ScrollingSensitivity;
	}

	private Rect _getScreenSpaceRect()
	{
		Canvas canvas = _canvas;
		if (canvas == null)
		{
			WebViewLogger.LogError("Unable to determine the screen space rect for Native 2D Mode because the CanvasWebViewPrefab is not placed in a Canvas. Please place the CanvasWebViewPrefab as the child of a Unity UI Canvas.");
			return Rect.zero;
		}
		Vector3[] array = new Vector3[4];
		_rectTransform.GetWorldCorners(array);
		Vector3 position = array[1];
		Vector3 position2 = array[3];
		if (canvas.renderMode != 0)
		{
			Camera worldCamera = canvas.worldCamera;
			if (worldCamera == null)
			{
				WebViewLogger.LogError("Unable to determine the screen space rect for Native 2D Mode because the Canvas's render camera is not set. Please set the Canvas's \"Render Camera\" setting or change its render mode to \"Screen Space - Overlay\".");
			}
			else
			{
				position = worldCamera.WorldToScreenPoint(position);
				position2 = worldCamera.WorldToScreenPoint(position2);
			}
		}
		float num = position.x;
		float num2 = (float)Screen.height - position.y;
		float num3 = position2.x - position.x;
		float num4 = position.y - position2.y;
		float num5 = _getScreenSpaceScaleFactor();
		if (num5 != 1f)
		{
			num *= num5;
			num2 *= num5;
			num3 *= num5;
			num4 *= num5;
		}
		return new Rect(num, num2, num3, num4);
	}

	private float _getScreenSpaceScaleFactor()
	{
		if (!_resolutionsAreEqual(Screen.currentResolution, _originalScreenResolution))
		{
			return (float)_originalScreenResolution.width / (float)Screen.currentResolution.width;
		}
		return 1f;
	}

	protected override ViewportMaterialView _getVideoLayer()
	{
		return base.transform.Find("VideoLayer").GetComponent<ViewportMaterialView>();
	}

	protected override ViewportMaterialView _getView()
	{
		return base.transform.Find("CanvasWebViewPrefabView").GetComponent<ViewportMaterialView>();
	}

	private void _initCanvasPrefab()
	{
		base.Initialized += _logNative2DRecommendationIfNeeded;
		bool flag = Native2DModeEnabled && _canNative2DModeBeEnabled(logWarnings: true);
		Rect rect = (flag ? _getScreenSpaceRect() : _rectTransform.rect);
		if (!_logErrorIfSizeIsInvalid(rect.size))
		{
			_initBase(rect, flag);
		}
	}

	private bool _logErrorIfSizeIsInvalid(Vector2 size)
	{
		if (!(size.x > 0f) || !(size.y > 0f))
		{
			WebViewLogger.LogError("CanvasWebViewPrefab dimensions are invalid! Width: " + size.x.ToString("f4") + ", Height: " + size.y.ToString("f4") + ". To correct this, please adjust the CanvasWebViewPrefab's RectTransform to make it so that its width and height are both greater than zero. https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/class-RectTransform.html");
			return true;
		}
		return false;
	}

	private void _logNative2DModeWarning(string message)
	{
		WebViewLogger.LogWarning(message + " For more info, please see this article: <em>https://support.vuplex.com/articles/native-2d-mode</em>");
	}

	private void _logNative2DRecommendationIfNeeded(object sender, EventArgs eventArgs)
	{
		IWithNative2DMode withNative2DMode = base.WebView as IWithNative2DMode;
		if (_canNative2DModeBeEnabled() && withNative2DMode != null && !withNative2DMode.Native2DModeEnabled)
		{
			WebViewLogger.LogTip("This platform supports Native 2D Mode, so consider enabling CanvasWebViewPrefab.Native2DModeEnabled for best results. For more info, see https://support.vuplex.com/articles/native-2d-mode .");
		}
	}

	private void OnDisable()
	{
		_getNative2DWebViewIfActive()?.SetVisible(visible: false);
	}

	private void OnEnable()
	{
		_getNative2DWebViewIfActive()?.SetVisible(visible: true);
	}

	private bool _resolutionsAreEqual(Resolution res1, Resolution res2)
	{
		if (res1.width == res2.width && res1.height == res2.height)
		{
			return true;
		}
		if (res1.width == res2.height && res1.height == res2.width)
		{
			return true;
		}
		return false;
	}

	protected override void _setVideoLayerPosition(Rect videoRect)
	{
		RectTransform obj = base._videoLayer.transform as RectTransform;
		obj.anchoredPosition = Vector2.Scale(Vector2.Scale(videoRect.position, _rectTransform.rect.size), new Vector2(1f, -1f));
		obj.sizeDelta = Vector2.Scale(videoRect.size, _rectTransform.rect.size);
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void _saveOriginalScreenResolution()
	{
		_originalScreenResolution = Screen.currentResolution;
	}

	private void Start()
	{
		_initCanvasPrefab();
	}

	protected override void Update()
	{
		base.Update();
		if (base.WebView == null)
		{
			return;
		}
		_sizeInUnityUnits = _rectTransform.rect.size;
		if (_logErrorIfSizeIsInvalid(_sizeInUnityUnits))
		{
			return;
		}
		IWithNative2DMode withNative2DMode = _getNative2DWebViewIfActive();
		if (withNative2DMode != null)
		{
			Rect rect = _getScreenSpaceRect();
			if (withNative2DMode.Rect != rect)
			{
				withNative2DMode.SetRect(rect);
			}
		}
		else
		{
			_resizeWebViewIfNeeded();
		}
	}

	[Obsolete("CanvasWebViewPrefab.Init() has been removed. The CanvasWebViewPrefab script now initializes itself automatically, so Init() no longer needs to be called.", true)]
	public void Init()
	{
	}

	[Obsolete("CanvasWebViewPrefab.Init() has been removed. The CanvasWebViewPrefab script now initializes itself automatically, so Init() no longer needs to be called.", true)]
	public void Init(WebViewOptions options)
	{
	}

	[Obsolete("CanvasWebViewPrefab.Init() has been removed. The CanvasWebViewPrefab script now initializes itself automatically, so Init() no longer needs to be called. Please use CanvasWebViewPrefab.SetWebViewForInitialization(IWebView) instead.", true)]
	public void Init(IWebView webView)
	{
	}
}
