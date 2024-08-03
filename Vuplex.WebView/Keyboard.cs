using System;
using UnityEngine;
using UnityEngine.Serialization;
using Vuplex.WebView.Internal;

namespace Vuplex.WebView;

public class Keyboard : BaseKeyboard
{
	[Label("Resolution (px / Unity unit)")]
	[Tooltip("You can change this to make web content appear larger or smaller.")]
	[FormerlySerializedAs("InitialResolution")]
	public float Resolution = 1300f;

	private const float DEFAULT_KEYBOARD_WIDTH = 0.5f;

	private const float DEFAULT_KEYBOARD_HEIGHT = 0.125f;

	public WebViewPrefab WebViewPrefab => (WebViewPrefab)_webViewPrefab;

	[Obsolete("Keyboard.InitialResolution is now deprecated. Please use Keyboard.Resolution instead.")]
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

	public static Keyboard Instantiate()
	{
		return Instantiate(0.5f, 0.125f);
	}

	public static Keyboard Instantiate(float width, float height)
	{
		Keyboard component = UnityEngine.Object.Instantiate((GameObject)Resources.Load("Keyboard")).GetComponent<Keyboard>();
		component.transform.localScale = new Vector3(width, height, 1f);
		return component;
	}

	private void _initKeyboard()
	{
		Vector3 localScale = base.transform.localScale;
		base.transform.localScale = Vector3.one;
		WebViewPrefab webViewPrefab = (WebViewPrefab)(_webViewPrefab = WebViewPrefab.Instantiate(localScale.x, localScale.y, BaseKeyboard._webViewOptions));
		webViewPrefab.Resolution = Resolution;
		webViewPrefab.NativeOnScreenKeyboardEnabled = true;
		_webViewPrefab.transform.SetParent(base.transform, worldPositionStays: false);
		BaseKeyboard._setLayerRecursively(_webViewPrefab.gameObject, base.gameObject.layer);
		_webViewPrefab.transform.localPosition = Vector3.zero;
		_webViewPrefab.transform.localEulerAngles = Vector3.zero;
		_init();
		Transform transform = base.transform.Find("Placeholder");
		if (transform != null)
		{
			transform.gameObject.SetActive(value: false);
		}
	}

	private void Start()
	{
		_initKeyboard();
	}

	[Obsolete("Keyboard.Init() has been removed. The Keyboard script now initializes itself automatically, so Init() no longer needs to be called.", true)]
	public void Init(float width, float height)
	{
	}
}
