using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Vuplex.WebView.Internal;

namespace Vuplex.WebView;

public class CanvasKeyboard : BaseKeyboard
{
	[Label("Resolution (px / Unity unit)")]
	[Tooltip("You can change this to make web content appear larger or smaller.")]
	[FormerlySerializedAs("InitialResolution")]
	public float Resolution = 1f;

	public CanvasWebViewPrefab WebViewPrefab => (CanvasWebViewPrefab)_webViewPrefab;

	[Obsolete("CanvasKeyboard.InitialResolution is now deprecated. Please use CanvasKeyboard.Resolution instead.")]
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

	public static CanvasKeyboard Instantiate()
	{
		return UnityEngine.Object.Instantiate((GameObject)Resources.Load("CanvasKeyboard")).GetComponent<CanvasKeyboard>();
	}

	private void _initCanvasKeyboard()
	{
		CanvasWebViewPrefab canvasWebViewPrefab = (CanvasWebViewPrefab)(_webViewPrefab = CanvasWebViewPrefab.Instantiate(BaseKeyboard._webViewOptions));
		_webViewPrefab.transform.SetParent(base.transform, worldPositionStays: false);
		BaseKeyboard._setLayerRecursively(_webViewPrefab.gameObject, base.gameObject.layer);
		RectTransform obj = _webViewPrefab.transform as RectTransform;
		obj.anchoredPosition3D = Vector3.zero;
		obj.offsetMin = Vector2.zero;
		obj.offsetMax = Vector2.zero;
		_webViewPrefab.transform.localScale = Vector3.one;
		canvasWebViewPrefab.Resolution = Resolution;
		_init();
		Image component = GetComponent<Image>();
		if (component != null)
		{
			component.enabled = false;
		}
	}

	private void Start()
	{
		_initCanvasKeyboard();
	}
}
