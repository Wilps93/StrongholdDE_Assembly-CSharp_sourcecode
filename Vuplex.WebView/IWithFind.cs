using System.Threading.Tasks;

namespace Vuplex.WebView;

public interface IWithFind
{
	void ClearFindMatches();

	Task<FindResult> Find(string text, bool forward);
}
