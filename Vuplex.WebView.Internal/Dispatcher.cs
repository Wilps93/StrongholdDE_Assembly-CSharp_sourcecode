using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Vuplex.WebView.Internal;

public class Dispatcher : MonoBehaviour
{
	private static Dispatcher _instance;

	private static volatile bool _queued = false;

	private static List<Action> _backlog = new List<Action>(8);

	private static List<Action> _actions = new List<Action>(8);

	public static void RunAsync(Action action)
	{
		ThreadPool.QueueUserWorkItem(delegate
		{
			action();
		});
	}

	public static void RunAsync(Action<object> action, object state)
	{
		ThreadPool.QueueUserWorkItem(delegate(object o)
		{
			action(o);
		}, state);
	}

	public static void RunOnMainThread(Action action)
	{
		lock (_backlog)
		{
			_backlog.Add(action);
			_queued = true;
		}
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Initialize()
	{
		if (_instance == null)
		{
			_instance = new GameObject("Dispatcher").AddComponent<Dispatcher>();
			UnityEngine.Object.DontDestroyOnLoad(_instance.gameObject);
		}
	}

	private void Update()
	{
		if (!_queued)
		{
			return;
		}
		lock (_backlog)
		{
			List<Action> actions = _actions;
			_actions = _backlog;
			_backlog = actions;
			_queued = false;
		}
		foreach (Action action in _actions)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				WebViewLogger.LogError("An exception occurred while dispatching an action on the main thread: " + ex);
			}
		}
		_actions.Clear();
	}
}
