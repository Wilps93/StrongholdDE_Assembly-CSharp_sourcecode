using System;

namespace Vuplex.WebView;

public class DownloadChangedEventArgs : EventArgs
{
	public readonly string ContentType;

	public readonly string FilePath;

	public readonly string Id;

	public readonly float Progress;

	public readonly ProgressChangeType Type;

	public readonly string Url;

	public DownloadChangedEventArgs(string contentType, string filePath, string id, float progress, ProgressChangeType type, string url)
	{
		ContentType = contentType;
		FilePath = filePath;
		Id = id;
		Progress = progress;
		Type = type;
		Url = url;
	}
}
