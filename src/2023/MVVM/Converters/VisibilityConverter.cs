using System.Windows;

namespace MVVM.Converters;

/// <summary>
///     Boolean to Visibility converter
/// </summary>
public class VisibilityConverter : BooleanConverters<Visibility, VisibilityConverter>
{
    public VisibilityConverter() : base(Visibility.Visible, Visibility.Collapsed)
    {

    }
}

/// <summary>
///     Boolean to Reverse Visibility converter
/// </summary>
public class VisibilityReverseConverter : BooleanConverters<Visibility, VisibilityReverseConverter>
{
    public VisibilityReverseConverter() : base(Visibility.Collapsed, Visibility.Visible)
    {

    }
}