using System;
using UnityEngine;
using Vuplex.WebView.Internal;

namespace Vuplex.WebView;

[Serializable]
public class Cookie
{
	public string Name;

	public string Value;

	public string Domain;

	public string Path = "/";

	public int ExpirationDate;

	public bool HttpOnly;

	public bool Secure;

	public bool IsValid
	{
		get
		{
			bool result = true;
			if (Name == null)
			{
				WebViewLogger.LogWarning("Invalid value for Cookie.Name: " + Name);
				result = false;
			}
			if (Value == null)
			{
				WebViewLogger.LogWarning("Invalid value for Cookie.Value: " + Value);
				result = false;
			}
			if (Domain == null || !Domain.Contains(".") || Domain.Contains("/"))
			{
				WebViewLogger.LogWarning("Invalid value for Cookie.Domain: " + Domain);
				result = false;
			}
			if (Path == null)
			{
				WebViewLogger.LogWarning("Invalid value for Cookie.Path: " + Path);
				result = false;
			}
			return result;
		}
	}

	public static Cookie[] ArrayFromJson(string serializedCookies)
	{
		if (serializedCookies == "null")
		{
			return new Cookie[0];
		}
		return JsonUtility.FromJson<JsonArrayWrapper<Cookie>>(serializedCookies).Items ?? new Cookie[0];
	}

	public static Cookie FromJson(string serializedCookie)
	{
		if (serializedCookie == "null")
		{
			return null;
		}
		return JsonUtility.FromJson<Cookie>(serializedCookie);
	}

	public string ToJson()
	{
		return JsonUtility.ToJson(this);
	}

	public override string ToString()
	{
		return ToJson();
	}
}
