using System;

namespace Vuplex.WebView.Internal;

[Serializable]
public class JsonArrayWrapper<T>
{
	public T[] Items;

	public JsonArrayWrapper()
	{
	}

	public JsonArrayWrapper(T[] items)
	{
		Items = items;
	}
}
