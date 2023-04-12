using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using Microsoft.Xaml.Behaviors;
using PolygonsMarkingEditor.Models;
using PolygonsMarkingEditor.Tools;
using static PolygonsMarkingEditor.Tools.Extensions;

namespace PolygonsMarkingEditor.Behaviors;

internal class DragBehaviour : Behavior<UIElement>
{
    private Point _start;

    public static readonly DependencyProperty CanDragProperty = DependencyProperty.Register(
        nameof(CanDrag), typeof(bool), typeof(DragBehaviour), new PropertyMetadata(default(bool)));

    public bool CanDrag
    {
        get => (bool)GetValue(CanDragProperty);
        set => SetValue(CanDragProperty, value);
    }

    protected override void OnAttached()
    {
        if (!CanDrag) return;
        AssociatedObject.MouseLeftButtonDown += OnButtonDown;
        AssociatedObject.MouseLeftButtonUp += OnButtonUp;
        AssociatedObject.MouseMove += OnMouseMove;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.MouseLeftButtonDown -= OnButtonDown;
        AssociatedObject.MouseLeftButtonUp -= OnButtonUp;
        AssociatedObject.MouseMove -= OnMouseMove;
    }

    private void OnButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
    {
        var parent = AssociatedObject.FindVisualParent<Canvas>();
        _start = mouseButtonEventArgs.GetPosition(parent);
        AssociatedObject.CaptureMouse();
    }

    private void OnButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
    {
        AssociatedObject.ReleaseMouseCapture();
        
        var parent = AssociatedObject.FindVisualParent<Canvas>();
        var mousePos = mouseButtonEventArgs.GetPosition(parent);
        var diff = mousePos - _start;

        foreach (var ellipse in parent.FindVisualChildren<Ellipse>())
        {
            if (ellipse.DataContext is not Vertex { IsSelected: true } || ellipse == AssociatedObject) continue;
            var left = Canvas.GetLeft(ellipse);
            var top = Canvas.GetTop(ellipse);
            Canvas.SetLeft(ellipse, left + diff.X - ElementSize);
            Canvas.SetTop(ellipse, top + diff.Y - ElementSize);
        }
    }

    private void OnMouseMove(object sender, MouseEventArgs mouseEventArgs)
    {

        if (!AssociatedObject.IsMouseCaptured) return;
        var parent = AssociatedObject.FindVisualParent<Canvas>();
        var mousePos = mouseEventArgs.GetPosition(parent);
        var diff = mousePos - _start;

        Canvas.SetLeft(AssociatedObject, _start.X + diff.X - ElementSize);
        Canvas.SetTop(AssociatedObject, _start.Y + diff.Y - ElementSize);
    }
}