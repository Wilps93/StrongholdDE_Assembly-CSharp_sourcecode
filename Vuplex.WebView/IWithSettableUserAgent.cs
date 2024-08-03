namespace Vuplex.WebView;

public interface IWithSettableUserAgent
{
	void SetUserAgent(bool mobile);

	void SetUserAgent(string userAgent);
}
