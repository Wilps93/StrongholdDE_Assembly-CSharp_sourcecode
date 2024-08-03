using System;
using UnityEngine;

namespace Vuplex.WebView;

public class ScrolledEventArgs : EventArgs
{
	public readonly Vector2 ScrollDelta;

	public readonly Vector2 Point;

	public ScrolledEventArgs(Vector2 scrollDelta, Vector2 point)
	{
		ScrollDelta = scrollDelta;
		Point = point;
	}
}
