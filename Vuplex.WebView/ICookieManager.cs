using System.Threading.Tasks;

namespace Vuplex.WebView;

public interface ICookieManager
{
	Task<bool> DeleteCookies(string url, string cookieName = null);

	Task<Cookie[]> GetCookies(string url, string cookieName = null);

	Task<bool> SetCookie(Cookie cookie);
}
