using System.Threading.Tasks;

namespace Vuplex.WebView;

internal class MockCookieManager : ICookieManager
{
	private static MockCookieManager _instance;

	public static MockCookieManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new MockCookieManager();
			}
			return _instance;
		}
	}

	public Task<bool> DeleteCookies(string url, string cookieName = null)
	{
		return MockWebView.DeleteCookies(url, cookieName);
	}

	public Task<Cookie[]> GetCookies(string url, string cookieName = null)
	{
		return MockWebView.GetCookies(url, cookieName);
	}

	public Task<bool> SetCookie(Cookie cookie)
	{
		return MockWebView.SetCookie(cookie);
	}
}
