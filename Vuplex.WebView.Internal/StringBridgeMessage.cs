using System;
using UnityEngine;

namespace Vuplex.WebView.Internal;

[Serializable]
internal class StringBridgeMessage : BridgeMessage
{
	public string value;

	public static string ParseValue(string serializedMessage)
	{
		return JsonUtility.FromJson<StringBridgeMessage>(serializedMessage).value;
	}
}
