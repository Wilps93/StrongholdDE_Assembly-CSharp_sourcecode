using System;

namespace Vuplex.WebView;

public interface IWithCursorType
{
	event EventHandler<EventArgs<string>> CursorTypeChanged;
}
