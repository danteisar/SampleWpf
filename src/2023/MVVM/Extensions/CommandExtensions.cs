using System.Windows.Input;
using MVVM.Commands;

namespace MVVM.Extensions;

public static class CommandExtensions
{
    /// <summary>
    /// Triggers the CanExecuteChanged event and a property changed event on the CanExecute property.
    /// </summary>
    /// <param name="command">Must be <see cref="RelayCommand"/></param>
    public static void RaiseCanExecuteChanged(this ICommand command) => ((RelayCommand)command).RaiseCanExecuteChanged();
}