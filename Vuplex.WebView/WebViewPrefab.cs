using System;
using UnityEngine;
using UnityEngine.Serialization;
using Vuplex.WebView.Internal;

namespace Vuplex.WebView;

[HelpURL("https://developer.vuplex.com/webview/WebViewPrefab")]
public class WebViewPrefab : BaseWebViewPrefab
{
	[Label("Native On-Screen Keyboard (Android and iOS only)")]
	[Header("Platform-specific")]
	[Tooltip("Determines whether the operating system's native on-screen keyboard is automatically shown when a text input in the webview is focused. The native on-screen keyboard is only supported for the following packages:\n• 3D WebView for Android (non-Gecko)\n• 3D WebView for iOS")]
	public bool NativeOnScreenKeyboardEnabled;

	[Label("Resolution (px / Unity unit)")]
	[Tooltip("You can change this to make web content appear larger or smaller.")]
	[HideInInspector]
	[FormerlySerializedAs("InitialResolution")]
	public float Resolution = 1300f;

	[HideInInspector]
	public float ScrollingSensitivity = 0.005f;

	private Vector2 _sizeForInitialization = Vector2.zero;

	[SerializeField]
	[HideInInspector]
	private Transform _videoRectPositioner;

	[SerializeField]
	[HideInInspector]
	protected Transform _viewResizer;

	[Obsolete("The static WebViewPrefab.ScrollSensitivity property has been removed. Please use the ScrollingSensitivity instance property instead: https://developer.vuplex.com/webview/WebViewPrefab#ScrollingSensitivity", true)]
	public static float ScrollSensitivity;

	public Collider Collider => base._view.GetComponent<Collider>();

	[Obsolete("WebViewPrefab.InitialResolution is now deprecated. Please use WebViewPrefab.Resolution instead.")]
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

	public Vector2 WorldToNormalized(Vector3 worldPoint)
	{
		Vector3 vector = _viewResizer.transform.InverseTransformPoint(worldPoint);
		return new Vector2(1f - vector.x, -1f * vector.y);
	}

	public static WebViewPrefab Instantiate(float width, float height)
	{
		return Instantiate(width, height, default(WebViewOptions));
	}

	public static WebViewPrefab Instantiate(float width, float height, WebViewOptions options)
	{
		WebViewPrefab component = UnityEngine.Object.Instantiate((GameObject)Resources.Load("WebViewPrefab")).GetComponent<WebViewPrefab>();
		component._sizeForInitialization = new Vector2(width, height);
		component._options = options;
		return component;
	}

	public static WebViewPrefab Instantiate(IWebView webView)
	{
		WebViewPrefab component = UnityEngine.Object.Instantiate((GameObject)Resources.Load("WebViewPrefab")).GetComponent<WebViewPrefab>();
		component.SetWebViewForInitialization(webView);
		return component;
	}

	public void Resize(float width, float height)
	{
		if (width <= 0f || height <= 0f)
		{
			throw new ArgumentException("Invalid dimensions: (" + width.ToString("n4") + ", " + height.ToString("n4") + ")");
		}
		_sizeInUnityUnits = new Vector2(width, height);
		_resizeWebViewIfNeeded();
		_setViewSize(width, height);
	}

	protected override float _getResolution()
	{
		if (Resolution > 0f)
		{
			return Resolution;
		}
		WebViewLogger.LogError("Invalid value set for WebViewPrefab.Resolution: " + Resolution);
		return 1300f;
	}

	protected override float _getScrollingSensitivity()
	{
		return ScrollingSensitivity;
	}

	protected override bool _getNativeOnScreenKeyboardEnabled()
	{
		return NativeOnScreenKeyboardEnabled;
	}

	protected override ViewportMaterialView _getVideoLayer()
	{
		if (_videoRectPositioner == null)
		{
			return null;
		}
		return _videoRectPositioner.GetComponentInChildren<ViewportMaterialView>();
	}

	protected override ViewportMaterialView _getView()
	{
		return base.transform.Find("WebViewPrefabResizer/WebViewPrefabView").GetComponent<ViewportMaterialView>();
	}

	private void _initWebViewPrefab()
	{
		if (_sizeForInitialization == Vector2.zero)
		{
			if (_webViewForInitialization != null)
			{
				_sizeForInitialization = (Vector2)_webViewForInitialization.Size / Resolution;
			}
			else
			{
				_sizeForInitialization = base.transform.localScale;
				_resetLocalScale();
			}
		}
		_viewResizer = base.transform.GetChild(0);
		_videoRectPositioner = _viewResizer.Find("VideoRectPositioner");
		_setViewSize(_sizeForInitialization.x, _sizeForInitialization.y);
		_initBase(new Rect(Vector2.zero, _sizeForInitialization));
	}

	private void _resetLocalScale()
	{
		Vector3 localScale = base.transform.localScale;
		_ = base.transform.localPosition;
		base.transform.localScale = new Vector3(1f, 1f, localScale.z);
		float x = 0.5f * localScale.x;
		base.transform.localPosition = base.transform.localPosition + Quaternion.Euler(base.transform.localEulerAngles) * new Vector3(x, 0f, 0f);
	}

	protected override void _setVideoLayerPosition(Rect videoRect)
	{
		_videoRectPositioner.localPosition = new Vector3(1f - (videoRect.x + videoRect.width), -1f * videoRect.y, _videoRectPositioner.localPosition.z);
		_videoRectPositioner.localScale = new Vector3(videoRect.width, videoRect.height, _videoRectPositioner.localScale.z);
	}

	private void _setViewSize(float width, float height)
	{
		float z = _viewResizer.localScale.z;
		_viewResizer.localScale = new Vector3(width, height, z);
		Vector3 localPosition = _viewResizer.localPosition;
		localPosition.x = width * -0.5f;
		_viewResizer.localPosition = localPosition;
	}

	private void Start()
	{
		_initWebViewPrefab();
	}

	[Obsolete("WebViewPrefab.ConvertToScreenPoint() has been renamed to WebViewPrefab.WorldToNormalized(). Please switch to WorldToNormalized().")]
	public Vector2 ConvertToScreenPoint(Vector3 worldPoint)
	{
		return WorldToNormalized(worldPoint);
	}

	[Obsolete("WebViewPrefab.Init() has been removed. The WebViewPrefab script now initializes itself automatically, so Init() no longer needs to be called.", true)]
	public void Init()
	{
	}

	[Obsolete("WebViewPrefab.Init() has been removed. The WebViewPrefab script now initializes itself automatically, so Init() no longer needs to be called.", true)]
	public void Init(float width, float height)
	{
	}

	[Obsolete("WebViewPrefab.Init() has been removed. The WebViewPrefab script now initializes itself automatically, so Init() no longer needs to be called.", true)]
	public void Init(float width, float height, WebViewOptions options)
	{
	}

	[Obsolete("WebViewPrefab.Init() has been removed. The WebViewPrefab script now initializes itself automatically, so Init() no longer needs to be called. Please use WebViewPrefab.SetWebViewForInitialization(IWebView) instead.", true)]
	public void Init(IWebView webView)
	{
	}
}
