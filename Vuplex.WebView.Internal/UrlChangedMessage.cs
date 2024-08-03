using System;

namespace Vuplex.WebView.Internal;

[Serializable]
internal class UrlChangedMessage : BridgeMessage
{
	public UrlAction urlAction;
}
