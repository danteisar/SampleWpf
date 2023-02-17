using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace MVVM;

[AttributeUsage(AttributeTargets.Parameter)]
public sealed class CallerMemberNameAttribute : Attribute
{

}

[AttributeUsage(
    AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
    AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event |
    AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.GenericParameter)]
public sealed class NotNullAttribute : Attribute
{
}

[AttributeUsage(
    AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
    AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event |
    AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.GenericParameter)]
public sealed class CanBeNullAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
{
    public NotifyPropertyChangedInvocatorAttribute()
    {
    }

    public NotifyPropertyChangedInvocatorAttribute([NotNull] string parameterName) => ParameterName = parameterName;

    [CanBeNull] public string ParameterName { get; private set; }
}

public class BindableBase : INotifyPropertyChanged
{
    /// <summary>
    ///     Multicast event for property change notifications.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    ///     Checks if a property already matches a desired value.  Sets the property and
    ///     notifies listeners only when necessary.
    /// </summary>
    /// <typeparam name="T">Type of the property.</typeparam>
    /// <param name="storage">Reference to a property with both getter and setter.</param>
    /// <param name="value">Desired value for the property.</param>
    /// <param name="propertyName">
    ///     Name of the property used to notify listeners.  This
    ///     value is optional and can be provided automatically when invoked from compilers that
    ///     support CallerMemberName.
    /// </param>
    /// <returns>
    ///     True if the value was changed, false if the existing value matched the
    ///     desired value.
    /// </returns>
    public bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(storage, value)) return false;
        storage = value;
        RaisePropertyChanged(propertyName);
        return true;
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}