using UnityEngine;
using UnityEngine.EventSystems;
using Vuplex.WebView.Internal;

namespace Vuplex.WebView;

[HelpURL("https://developer.vuplex.com/webview/IPointerInputDetector")]
public class CanvasPointerInputDetector : DefaultPointerInputDetector
{
	private RectTransform _cachedRectTransform;

	private CachingGetter<Canvas> _canvasGetter;

	protected override Vector2 _convertToNormalizedPoint(PointerEventData pointerEventData)
	{
		if (_canvasGetter == null)
		{
			_canvasGetter = new CachingGetter<Canvas>(base.GetComponentInParent<Canvas>, 1, this);
		}
		Canvas value = _canvasGetter.GetValue();
		Camera cam = ((value == null || value.renderMode == RenderMode.ScreenSpaceOverlay) ? null : value.worldCamera);
		Vector2 screenPoint = pointerEventData.position;
		Vector3 vector = Display.RelativeMouseAt(new Vector3(screenPoint.x, screenPoint.y));
		if (vector != Vector3.zero)
		{
			screenPoint = new Vector2(vector.x, vector.y);
		}
		RectTransformUtility.ScreenPointToLocalPointInRectangle(_getRectTransform(), screenPoint, cam, out var localPoint);
		return _convertToNormalizedPoint(localPoint);
	}

	protected override Vector2 _convertToNormalizedPoint(Vector3 worldPosition)
	{
		Vector3 vector = _getRectTransform().InverseTransformPoint(worldPosition);
		return _convertToNormalizedPoint(vector);
	}

	private Vector2 _convertToNormalizedPoint(Vector2 localPoint)
	{
		Vector2 result = Rect.PointToNormalized(_getRectTransform().rect, localPoint);
		result.y = 1f - result.y;
		return result;
	}

	private RectTransform _getRectTransform()
	{
		if (_cachedRectTransform == null)
		{
			_cachedRectTransform = GetComponent<RectTransform>();
		}
		return _cachedRectTransform;
	}

	protected override bool _positionIsZero(PointerEventData eventData)
	{
		return eventData.position == Vector2.zero;
	}
}
