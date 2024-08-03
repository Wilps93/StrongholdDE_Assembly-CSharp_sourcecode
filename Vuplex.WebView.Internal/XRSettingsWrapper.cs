using UnityEngine;
using UnityEngine.XR;

namespace Vuplex.WebView.Internal;

public class XRSettingsWrapper
{
	private static XRSettingsWrapper _instance;

	public static XRSettingsWrapper Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new XRSettingsWrapper();
			}
			return _instance;
		}
	}

	public bool enabled => XRSettings.enabled;

	public RenderTextureDescriptor eyeTextureDesc => XRSettings.eyeTextureDesc;

	public string loadedDeviceName => XRSettings.loadedDeviceName;

	public string[] supportedDevices => XRSettings.supportedDevices;
}
