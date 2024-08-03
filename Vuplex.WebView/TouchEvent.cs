using UnityEngine;

namespace Vuplex.WebView;

public struct TouchEvent
{
	public int TouchID;

	public TouchEventType Type;

	public Vector2 Point;

	public float RadiusX;

	public float RadiusY;

	public float RotationAngle;

	public float Pressure;
}
