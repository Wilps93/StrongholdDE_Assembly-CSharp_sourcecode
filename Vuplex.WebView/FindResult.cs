namespace Vuplex.WebView;

public struct FindResult
{
	public int CurrentMatchIndex;

	public int MatchCount;

	public override string ToString()
	{
		return $"MatchCount: {MatchCount}, CurrentMatchIndex: {CurrentMatchIndex}";
	}
}
