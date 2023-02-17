using System;
using System.Windows.Input;

namespace MVVM.Commands;

/// <summary>
///     Base implement of <see cref="ICommand"/>
/// <para>
///     
/// </para>
/// </summary>
public class DumbCommand : ICommand
{
    #region ctor

    public DumbCommand(Action<object> action, Predicate<object> canExecute = null)
    {
        ExecuteDelegate = action;
        CanExecuteDelegate = canExecute;
    }

    #endregion

    #region Props

    public Predicate<object> CanExecuteDelegate { get; set; }
    public Action<object> ExecuteDelegate { get; set; }

    #endregion

    #region ICommand methods

    public bool CanExecute(object parameter) => CanExecuteDelegate == null || CanExecuteDelegate(parameter);


    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public void Execute(object parameter) => ExecuteDelegate?.Invoke(parameter);

    #endregion
}