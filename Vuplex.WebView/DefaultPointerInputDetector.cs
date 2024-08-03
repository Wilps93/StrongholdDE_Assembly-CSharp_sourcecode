using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Vuplex.WebView;

[HelpURL("https://developer.vuplex.com/webview/IPointerInputDetector")]
public class DefaultPointerInputDetector : MonoBehaviour, IPointerInputDetector, IBeginDragHandler, IEventSystemHandler, IDragHandler, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IScrollHandler
{
	private bool _isHovering;

	public bool PointerMovedEnabled { get; set; }

	public event EventHandler<EventArgs<Vector2>> BeganDrag;

	public event EventHandler<EventArgs<Vector2>> Dragged;

	public event EventHandler<PointerEventArgs> PointerDown;

	public event EventHandler PointerExited;

	public event EventHandler<EventArgs<Vector2>> PointerMoved;

	public event EventHandler<PointerEventArgs> PointerUp;

	public event EventHandler<ScrolledEventArgs> Scrolled;

	public void OnBeginDrag(PointerEventData eventData)
	{
		_raiseBeganDragEvent(_convertToEventArgs(eventData));
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!_positionIsZero(eventData))
		{
			_raiseDraggedEvent(_convertToEventArgs(eventData));
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
	}

	public virtual void OnPointerDown(PointerEventData eventData)
	{
		_raisePointerDownEvent(_convertToPointerEventArgs(eventData));
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		_isHovering = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_isHovering = false;
		_raisePointerExitedEvent(EventArgs.Empty);
	}

	public virtual void OnPointerUp(PointerEventData eventData)
	{
		_raisePointerUpEvent(_convertToPointerEventArgs(eventData));
	}

	public void OnScroll(PointerEventData eventData)
	{
		Vector2 scrollDelta = new Vector2(0f - eventData.scrollDelta.x, 0f - eventData.scrollDelta.y);
		_raiseScrolledEvent(new ScrolledEventArgs(scrollDelta, _convertToNormalizedPoint(eventData)));
	}

	private EventArgs<Vector2> _convertToEventArgs(Vector3 worldPosition)
	{
		return new EventArgs<Vector2>(_convertToNormalizedPoint(worldPosition));
	}

	private EventArgs<Vector2> _convertToEventArgs(PointerEventData pointerEventData)
	{
		return new EventArgs<Vector2>(_convertToNormalizedPoint(pointerEventData));
	}

	protected virtual Vector2 _convertToNormalizedPoint(PointerEventData pointerEventData)
	{
		return _convertToNormalizedPoint(pointerEventData.pointerCurrentRaycast.worldPosition);
	}

	protected virtual Vector2 _convertToNormalizedPoint(Vector3 worldPosition)
	{
		Vector3 vector = base.transform.parent.InverseTransformPoint(worldPosition);
		return new Vector2(1f - vector.x, -1f * vector.y);
	}

	private PointerEventArgs _convertToPointerEventArgs(PointerEventData eventData)
	{
		return new PointerEventArgs
		{
			Point = _convertToNormalizedPoint(eventData),
			Button = (MouseButton)eventData.button,
			ClickCount = Math.Max(eventData.clickCount, 1)
		};
	}

	private PointerEventData _getLastPointerEventData()
	{
		PointerInputModule pointerInputModule = EventSystem.current?.currentInputModule as PointerInputModule;
		if (pointerInputModule == null)
		{
			return null;
		}
		object[] array = new object[3] { -1, null, false };
		pointerInputModule.GetType().InvokeMember("GetPointerData", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, pointerInputModule, array);
		return array[1] as PointerEventData;
	}

	protected virtual bool _positionIsZero(PointerEventData eventData)
	{
		return eventData.pointerCurrentRaycast.worldPosition == Vector3.zero;
	}

	protected void _raiseBeganDragEvent(EventArgs<Vector2> eventArgs)
	{
		this.BeganDrag?.Invoke(this, eventArgs);
	}

	protected void _raiseDraggedEvent(EventArgs<Vector2> eventArgs)
	{
		this.Dragged?.Invoke(this, eventArgs);
	}

	protected void _raisePointerDownEvent(PointerEventArgs eventArgs)
	{
		this.PointerDown?.Invoke(this, eventArgs);
	}

	protected void _raisePointerExitedEvent(EventArgs eventArgs)
	{
		this.PointerExited?.Invoke(this, eventArgs);
	}

	private void _raisePointerMovedIfNeeded()
	{
		if (!PointerMovedEnabled || !_isHovering)
		{
			return;
		}
		PointerEventData pointerEventData = _getLastPointerEventData();
		if (pointerEventData != null)
		{
			Vector2 val = _convertToNormalizedPoint(pointerEventData);
			if (val.x >= 0f && val.y >= 0f)
			{
				_raisePointerMovedEvent(new EventArgs<Vector2>(val));
			}
		}
	}

	protected void _raisePointerMovedEvent(EventArgs<Vector2> eventArgs)
	{
		this.PointerMoved?.Invoke(this, eventArgs);
	}

	protected void _raisePointerUpEvent(PointerEventArgs eventArgs)
	{
		this.PointerUp?.Invoke(this, eventArgs);
	}

	protected void _raiseScrolledEvent(ScrolledEventArgs eventArgs)
	{
		this.Scrolled?.Invoke(this, eventArgs);
	}

	private void Update()
	{
		_raisePointerMovedIfNeeded();
	}
}
