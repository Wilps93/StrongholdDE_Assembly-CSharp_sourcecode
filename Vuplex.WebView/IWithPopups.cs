using System;

namespace Vuplex.WebView;

public interface IWithPopups
{
	event EventHandler<PopupRequestedEventArgs> PopupRequested;

	void SetPopupMode(PopupMode popupMode);
}
