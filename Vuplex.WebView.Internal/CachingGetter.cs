using System;
using System.Collections;
using UnityEngine;

namespace Vuplex.WebView.Internal;

internal class CachingGetter<TResult>
{
	private Func<TResult> _getterFunction;

	private bool _valueNeedsToBeUpdated = true;

	private TResult _cachedValue;

	private WaitForSeconds _waitForSeconds;

	public CachingGetter(Func<TResult> getterFunction, int cacheInvalidationPeriodSeconds, MonoBehaviour monoBehaviourForCoroutine)
	{
		_getterFunction = getterFunction;
		_waitForSeconds = new WaitForSeconds(cacheInvalidationPeriodSeconds);
		monoBehaviourForCoroutine.StartCoroutine(_invalidateCachePeriodically());
	}

	public TResult GetValue()
	{
		if (_valueNeedsToBeUpdated)
		{
			_cachedValue = _getterFunction();
			_valueNeedsToBeUpdated = false;
		}
		return _cachedValue;
	}

	private IEnumerator _invalidateCachePeriodically()
	{
		while (true)
		{
			yield return _waitForSeconds;
			_valueNeedsToBeUpdated = true;
		}
	}
}
