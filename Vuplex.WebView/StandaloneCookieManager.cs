using System.Threading.Tasks;

namespace Vuplex.WebView;

public class StandaloneCookieManager : ICookieManager
{
	private static StandaloneCookieManager _instance;

	public static StandaloneCookieManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new StandaloneCookieManager();
			}
			return _instance;
		}
	}

	public Task<bool> DeleteCookies(string url, string cookieName = null)
	{
		return StandaloneWebView.DeleteCookies(url, cookieName);
	}

	public Task<Cookie[]> GetCookies(string url, string cookieName = null)
	{
		return StandaloneWebView.GetCookies(url, cookieName);
	}

	public Task<bool> SetCookie(Cookie cookie)
	{
		return StandaloneWebView.SetCookie(cookie);
	}
}
