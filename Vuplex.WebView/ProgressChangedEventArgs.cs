using System;

namespace Vuplex.WebView;

public class ProgressChangedEventArgs : EventArgs
{
	public readonly float Progress;

	public readonly ProgressChangeType Type;

	public ProgressChangedEventArgs(ProgressChangeType type, float progress)
	{
		Type = type;
		Progress = progress;
	}
}
