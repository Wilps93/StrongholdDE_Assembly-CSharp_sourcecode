using System;
using UnityEngine;

namespace Vuplex.WebView;

public class ClickedEventArgs : EventArgs
{
	public readonly Vector2 Point;

	public ClickedEventArgs(Vector2 point)
	{
		Point = point;
	}
}
