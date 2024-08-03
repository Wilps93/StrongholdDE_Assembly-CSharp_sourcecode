using System.Threading.Tasks;
using UnityEngine;

namespace Vuplex.WebView;

public interface IWithNative2DMode
{
	bool Native2DModeEnabled { get; }

	Rect Rect { get; }

	bool Visible { get; }

	void BringToFront();

	Task InitInNative2DMode(Rect rect);

	void SetNativeZoomEnabled(bool enabled);

	void SetRect(Rect rect);

	void SetVisible(bool visible);
}
