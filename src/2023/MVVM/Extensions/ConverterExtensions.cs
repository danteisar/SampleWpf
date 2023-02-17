using System.Windows;

namespace MVVM.Extensions;

/// <summary>
///     Extensions
/// </summary>
public static class ConvertersExtensions
{
    /// <summary>
    ///     Visibility inversion
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static Visibility Invert(this Visibility s) => s == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
}