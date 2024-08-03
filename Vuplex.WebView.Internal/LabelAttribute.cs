using UnityEngine;

namespace Vuplex.WebView.Internal;

public class LabelAttribute : PropertyAttribute
{
	public string Label { get; private set; }

	public LabelAttribute(string label)
	{
		Label = label;
	}
}
