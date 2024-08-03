using System;

namespace Vuplex.WebView;

public interface IWithFileSelection
{
	event EventHandler<FileSelectionEventArgs> FileSelectionRequested;
}
