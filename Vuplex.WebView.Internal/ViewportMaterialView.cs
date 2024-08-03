using UnityEngine;

namespace Vuplex.WebView.Internal;

public class ViewportMaterialView : MonoBehaviour
{
	public virtual Material Material
	{
		get
		{
			return GetComponent<Renderer>().sharedMaterial;
		}
		set
		{
			GetComponent<Renderer>().sharedMaterial = value;
		}
	}

	public virtual Texture Texture
	{
		get
		{
			return GetComponent<Renderer>().sharedMaterial.mainTexture;
		}
		set
		{
			GetComponent<Renderer>().sharedMaterial.mainTexture = value;
		}
	}

	public virtual bool Visible
	{
		get
		{
			return GetComponent<Renderer>().enabled;
		}
		set
		{
			GetComponent<Renderer>().enabled = value;
		}
	}

	public virtual void SetCropRect(Rect rect)
	{
		GetComponent<Renderer>().sharedMaterial.SetVector("_CropRect", _rectToVector(rect));
	}

	public virtual void SetCutoutRect(Rect rect)
	{
		Vector4 value = _rectToVector(rect);
		if (rect != new Rect(0f, 0f, 1f, 1f))
		{
			float num = rect.width * 0.01f;
			float num2 = rect.height * 0.01f;
			value = new Vector4(value.x + num, value.y + num2, value.z - 2f * num, value.w - 2f * num2);
		}
		GetComponent<Renderer>().sharedMaterial.SetVector("_VideoCutoutRect", value);
	}

	private Vector4 _rectToVector(Rect rect)
	{
		return new Vector4(rect.x, rect.y, rect.width, rect.height);
	}
}
