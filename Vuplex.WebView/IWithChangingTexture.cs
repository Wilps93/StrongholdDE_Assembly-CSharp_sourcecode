using System;
using UnityEngine;

namespace Vuplex.WebView;

public interface IWithChangingTexture
{
	event EventHandler<EventArgs<Texture2D>> TextureChanged;
}
