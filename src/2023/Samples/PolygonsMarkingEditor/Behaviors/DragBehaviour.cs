using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using Microsoft.Xaml.Behaviors;
using PolygonsMarkingEditor.Models;
using PolygonsMarkingEditor.ViewModels;
using ReactiveUI;
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
        AssociatedObject.MouseRightButtonDown += OnRightButtonDown;
        AssociatedObject.MouseRightButtonUp += OnRightButtonUp;
        AssociatedObject.MouseMove += OnMouseMove;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.MouseRightButtonDown -= OnRightButtonDown;
        AssociatedObject.MouseRightButtonUp -= OnRightButtonUp;
        AssociatedObject.MouseMove -= OnMouseMove;
    }

    private void OnRightButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
    {
        var parent = AssociatedObject.FindVisualParent<Canvas>();
        _start = mouseButtonEventArgs.GetPosition(parent);
        AssociatedObject.CaptureMouse();
    }

    private void OnRightButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
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
            Canvas.SetLeft(ellipse, left + diff.X - 10);
            Canvas.SetTop(ellipse, top + diff.Y - 10);
        }
    }

    private void OnMouseMove(object sender, MouseEventArgs mouseEventArgs)
    {

        if (!AssociatedObject.IsMouseCaptured) return;
        var parent = AssociatedObject.FindVisualParent<Canvas>();
        var mousePos = mouseEventArgs.GetPosition(parent);
        var diff = mousePos - _start;

        Canvas.SetLeft(AssociatedObject, _start.X + diff.X - 10);
        Canvas.SetTop(AssociatedObject, _start.Y + diff.Y - 10);
    }
}