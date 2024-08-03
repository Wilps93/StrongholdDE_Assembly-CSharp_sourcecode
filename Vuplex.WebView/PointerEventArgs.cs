using System;
using UnityEngine;

namespace Vuplex.WebView;

public class PointerEventArgs : EventArgs
{
	public MouseButton Button;

	public int ClickCount = 1;

	public Vector2 Point;

	public PointerOptions ToPointerOptions()
	{
		return new PointerOptions
		{
			Button = Button,
			ClickCount = ClickCount
		};
	}
}
