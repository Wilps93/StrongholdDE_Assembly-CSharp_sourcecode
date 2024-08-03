using System;

namespace Vuplex.WebView;

public class EventArgs<T> : EventArgs
{
	public T Value { get; private set; }

	public EventArgs(T val)
	{
		Value = val;
	}
}
