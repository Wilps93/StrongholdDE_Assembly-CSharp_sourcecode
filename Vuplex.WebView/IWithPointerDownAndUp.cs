using UnityEngine;

namespace Vuplex.WebView;

public interface IWithPointerDownAndUp
{
	void PointerDown(Vector2 normalizedPoint);

	void PointerDown(Vector2 normalizedPoint, PointerOptions options);

	void PointerUp(Vector2 normalizedPoint);

	void PointerUp(Vector2 normalizedPoint, PointerOptions options);
}
