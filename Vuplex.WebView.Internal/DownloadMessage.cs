using System;
using UnityEngine;

namespace Vuplex.WebView.Internal;

[Serializable]
public class DownloadMessage
{
	public string ContentType;

	public string FilePath;

	public string Id;

	public float Progress;

	public int Type;

	public string Url;

	public static DownloadMessage FromJson(string json)
	{
		return JsonUtility.FromJson<DownloadMessage>(json);
	}

	public DownloadChangedEventArgs ToEventArgs()
	{
		return new DownloadChangedEventArgs(ContentType, FilePath, Id, Progress, (ProgressChangeType)Type, Url);
	}
}
