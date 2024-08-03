using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace Vuplex.WebView.Internal;

public static class VXUtils
{
	public static XRSettingsWrapper XRSettings => XRSettingsWrapper.Instance;

	public static Material CreateDefaultMaterial()
	{
		return new Material(Resources.Load<Material>("DefaultViewportMaterial"));
	}

	public static Texture2D CreateDefaultTexture(int width, int height)
	{
		ThrowExceptionIfAbnormallyLarge(width, height);
		Texture2D texture2D = new Texture2D(width, height, TextureFormat.RGBA32, mipChain: false, linear: false);
		return Texture2D.CreateExternalTexture(width, height, TextureFormat.RGBA32, mipChain: false, linear: false, texture2D.GetNativeTexturePtr());
	}

	public static string GetGraphicsApiErrorMessage(GraphicsDeviceType activeGraphicsApi, GraphicsDeviceType[] acceptableGraphicsApis)
	{
		if (Array.IndexOf(acceptableGraphicsApis, activeGraphicsApi) != -1)
		{
			return null;
		}
		IEnumerable<string> source = from api in acceptableGraphicsApis.ToList()
			select api.ToString();
		string text = string.Join(" or ", source.ToArray());
		return $"Unsupported graphics API: Vuplex 3D WebView requires {text} for this platform, but the selected graphics API is {activeGraphicsApi}. Please go to Player Settings and set \"Graphics APIs\" to {text}.";
	}

	public static void LogNative2DModeWarning(string methodName)
	{
		WebViewLogger.LogWarning(methodName + "() was called but will be ignored because it is not supported in Native 2D Mode.");
	}

	public static void ThrowExceptionIfAbnormallyLarge(int width, int height)
	{
		if ((float)width * (float)height > 19400000f)
		{
			throw new ArgumentException($"The application specified an abnormally large webview size ({width}px x {height}px), and webviews of this size are normally only created by mistake. A WebViewPrefab's default resolution is 1300px per Unity unit, so it's likely that you specified a large physical size by mistake or need to adjust the resolution. For more information, please see WebViewPrefab.Resolution: https://developer.vuplex.com/webview/WebViewPrefab#Resolution ." + " If this large webview size is intentional, you can disable this exception by adding the scripting symbol VUPLEX_ALLOW_LARGE_WEBVIEWS to player settings. However, please note that if the webview size is larger than the graphics system can handle, the app may crash.");
		}
	}
}
