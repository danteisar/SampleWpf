using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace PolygonsMarking.Editor.Behaviors;

internal class MouseBehaviour : Behavior<UIElement>
{
    public static readonly DependencyProperty MouseXProperty = DependencyProperty.Register(
        nameof(MouseX), typeof(double), typeof(MouseBehaviour), new PropertyMetadata(default(double)));

    public double MouseX
    {
        get => (double)GetValue(MouseXProperty);
        set => SetValue(MouseXProperty, value);
    }

    public static readonly DependencyProperty MouseYProperty = DependencyProperty.Register(
        nameof(MouseY), typeof(double), typeof(MouseBehaviour), new PropertyMetadata(default(double)));

    public double MouseY
    {
        get => (double)GetValue(MouseYProperty);
        set => SetValue(MouseYProperty, value);
    }

    protected override void OnAttached()
    {
       AssociatedObject.MouseMove += AssociatedObjectOnMouseMove;
    }

    private void AssociatedObjectOnMouseMove(object sender, MouseEventArgs e)
    {
        var pos = e.GetPosition(AssociatedObject);
        MouseX = pos.X;
        MouseY = pos.Y;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.MouseMove -= AssociatedObjectOnMouseMove;
    }
}