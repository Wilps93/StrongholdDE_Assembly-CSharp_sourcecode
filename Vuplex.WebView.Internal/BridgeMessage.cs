using System;
using UnityEngine;

namespace Vuplex.WebView.Internal;

[Serializable]
public class BridgeMessage
{
	public string type;

	public static string ParseType(string serializedMessage)
	{
		try
		{
			return JsonUtility.FromJson<BridgeMessage>(serializedMessage).type;
		}
		catch (Exception)
		{
			return null;
		}
	}
}
