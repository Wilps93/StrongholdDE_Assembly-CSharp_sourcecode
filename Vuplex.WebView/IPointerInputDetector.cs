using System;
using UnityEngine;

namespace Vuplex.WebView;

public interface IPointerInputDetector
{
	bool PointerMovedEnabled { get; set; }

	event EventHandler<EventArgs<Vector2>> BeganDrag;

	event EventHandler<EventArgs<Vector2>> Dragged;

	event EventHandler<PointerEventArgs> PointerDown;

	event EventHandler PointerExited;

	event EventHandler<EventArgs<Vector2>> PointerMoved;

	event EventHandler<PointerEventArgs> PointerUp;

	event EventHandler<ScrolledEventArgs> Scrolled;
}
