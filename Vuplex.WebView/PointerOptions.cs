namespace Vuplex.WebView;

public class PointerOptions
{
	public MouseButton Button;

	public int ClickCount = 1;

	public override string ToString()
	{
		return $"Button = {Button}, ClickCount = {ClickCount}";
	}
}
