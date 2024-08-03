using System;
using System.Windows.Input;

namespace Stronghold1DE;

public class DelegateCommand : ICommand
{
	private Func<object, bool> _canExecute;

	private Action<object> _execute;

	public event EventHandler CanExecuteChanged;

	public DelegateCommand(Action<object> execute)
	{
		if (execute == null)
		{
			throw new ArgumentNullException("execute");
		}
		_execute = execute;
	}

	public DelegateCommand(Func<object, bool> canExecute, Action<object> execute)
	{
		if (canExecute == null)
		{
			throw new ArgumentNullException("canExecute");
		}
		if (execute == null)
		{
			throw new ArgumentNullException("execute");
		}
		_canExecute = canExecute;
		_execute = execute;
	}

	public bool CanExecute(object parameter)
	{
		if (_canExecute != null)
		{
			return _canExecute(parameter);
		}
		return true;
	}

	public void Execute(object parameter)
	{
		_execute(parameter);
	}

	public void RaiseCanExecuteChanged()
	{
		this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}
}
