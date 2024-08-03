using System;
using UnityEngine;

namespace Vuplex.WebView;

public interface IWithFallbackVideo
{
	bool FallbackVideoEnabled { get; }

	Texture2D VideoTexture { get; }

	event EventHandler<EventArgs<Rect>> VideoRectChanged;

	Material CreateVideoMaterial();

	void SetFallbackVideoEnabled(bool enabled);
}
