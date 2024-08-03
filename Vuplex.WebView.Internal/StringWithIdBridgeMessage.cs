using System;

namespace Vuplex.WebView.Internal;

[Serializable]
public class StringWithIdBridgeMessage : BridgeMessage
{
	public string id;

	public string value;
}
