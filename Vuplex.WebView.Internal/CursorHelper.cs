using System.Collections.Generic;
using UnityEngine;

namespace Vuplex.WebView.Internal;

public static class CursorHelper
{
	private class CursorInfo
	{
		public bool Centered;

		public Texture2D Texture;

		public CursorInfo(bool centered = false)
		{
			Centered = centered;
		}
	}

	private static Dictionary<string, CursorInfo> _supportedCursors = new Dictionary<string, CursorInfo>
	{
		["pointer"] = new CursorInfo(),
		["text"] = new CursorInfo(centered: true)
	};

	public static void SetCursorIcon(string cursorType)
	{
		_supportedCursors.TryGetValue(cursorType, out var value);
		if (value == null)
		{
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			return;
		}
		if (value.Texture == null)
		{
			value.Texture = Resources.Load<Texture2D>(cursorType);
		}
		Vector2 hotspot = (value.Centered ? new Vector2(16f, 16f) : Vector2.zero);
		Cursor.SetCursor(value.Texture, hotspot, CursorMode.Auto);
	}
}
