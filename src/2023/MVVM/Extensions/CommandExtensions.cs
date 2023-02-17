using System.Windows.Input;
using MVVM.Commands;

namespace MVVM.Extensions;

public static class CommandExtensions
{
    public static void RaiseCanExecuteChanged(this ICommand command) => ((RelayCommand)command).RaiseCanExecuteChanged();
}