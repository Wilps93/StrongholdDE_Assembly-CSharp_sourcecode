using System;
using UnityEngine;

namespace Vuplex.WebView.Internal;

[Serializable]
public class FileSelectionMessage
{
	public string[] AcceptFilters;

	public bool MultipleAllowed;

	public static FileSelectionMessage FromJson(string json)
	{
		return JsonUtility.FromJson<FileSelectionMessage>(json);
	}
}
