using System;

namespace Vuplex.WebView;

public interface IWithDownloads
{
	event EventHandler<DownloadChangedEventArgs> DownloadProgressChanged;

	void SetDownloadsEnabled(bool enabled);
}
