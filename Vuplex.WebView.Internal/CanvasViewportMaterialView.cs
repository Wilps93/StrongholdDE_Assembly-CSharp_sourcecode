using UnityEngine;
using UnityEngine.UI;

namespace Vuplex.WebView.Internal;

public class CanvasViewportMaterialView : ViewportMaterialView
{
	public override Material Material
	{
		get
		{
			return GetComponent<RawImage>().material;
		}
		set
		{
			GetComponent<RawImage>().material = value;
			if (value.mainTexture != null)
			{
				GetComponent<RawImage>().texture = value.mainTexture;
			}
		}
	}

	public override Texture Texture
	{
		get
		{
			return GetComponent<RawImage>().material.mainTexture;
		}
		set
		{
			GetComponent<RawImage>().material.mainTexture = value;
			GetComponent<RawImage>().texture = value;
		}
	}

	public override bool Visible
	{
		get
		{
			return GetComponent<RawImage>().enabled;
		}
		set
		{
			GetComponent<RawImage>().enabled = value;
		}
	}

	public override void SetCropRect(Rect rect)
	{
		_setShaderProperty("_CropRect", _rectToVector(rect));
	}

	public override void SetCutoutRect(Rect rect)
	{
		Vector4 value = _rectToVector(rect);
		if (rect != new Rect(0f, 0f, 1f, 1f))
		{
			float num = rect.width * 0.01f;
			float num2 = rect.height * 0.01f;
			value = new Vector4(value.x + num, value.y + num2, value.z - 2f * num, value.w - 2f * num2);
		}
		_setShaderProperty("_VideoCutoutRect", value);
	}

	private Vector4 _rectToVector(Rect rect)
	{
		return new Vector4(rect.x, rect.y, rect.width, rect.height);
	}

	private void _setShaderProperty(string propertyName, Vector4 value)
	{
		GetComponent<RawImage>().materialForRendering.SetVector(propertyName, value);
	}
}
