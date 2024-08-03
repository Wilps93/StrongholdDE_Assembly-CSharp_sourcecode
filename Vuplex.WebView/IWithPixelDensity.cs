namespace Vuplex.WebView;

public interface IWithPixelDensity
{
	float PixelDensity { get; }

	void SetPixelDensity(float pixelDensity);
}
