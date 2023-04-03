using System.Security.Cryptography.Xml;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;

namespace PolygonsMarking.Editor.Behaviors;

internal class Drag2Behaviour : Behavior<UIElement>
{
    public static readonly DependencyProperty CanDragProperty = DependencyProperty.Register(
        nameof(CanDrag), typeof(bool), typeof(Drag2Behaviour), new PropertyMetadata(default(bool), PropertyChangedCallback));

    private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        
    }

    public bool CanDrag
    {
        get => (bool)GetValue(CanDragProperty);
        set => SetValue(CanDragProperty, value);
    }

}

public class DragBehavior
{
   
    private Point _start;

    public static DragBehavior Instance { get; set; }

    public static bool GetDrag(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsDragProperty);
    }

    public static void SetDrag(DependencyObject obj, bool value)
    {
        obj.SetValue(IsDragProperty, value);
    }

    public static readonly DependencyProperty IsDragProperty =
      DependencyProperty.RegisterAttached("Drag",
      typeof(bool), typeof(DragBehavior),
      new PropertyMetadata(false, OnDragChanged));

    private static void OnDragChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        // ignoring error checking
        var element = (UIElement)sender;
        var isDrag = (bool)(e.NewValue);
        Instance = new DragBehavior();
        if (isDrag)
        {
            element.MouseRightButtonDown += Instance.ElementOnMouseLeftButtonDown;
            element.MouseRightButtonUp += ElementOnMouseLeftButtonUp;
            element.MouseMove += Instance.ElementOnMouseMove;
        }
        else
        {
            element.MouseRightButtonDown -= Instance.ElementOnMouseLeftButtonDown;
            element.MouseRightButtonUp -= ElementOnMouseLeftButtonUp;
            element.MouseMove -= Instance.ElementOnMouseMove;
        }
    }

    private static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
    {
        while (true)
        {
            var parentObject = VisualTreeHelper.GetParent(child);

            switch (parentObject)
            {
                case null:
                    return null;
                case T parent:
                    return parent;
                default:
                    child = parentObject;
                    continue;
            }
        }
    }

    private void ElementOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
    {
        var s = (UIElement)sender;
        var parent = FindVisualParent<Canvas>(s);
        _start = mouseButtonEventArgs.GetPosition(parent);
        s.CaptureMouse();
    }

    private static void ElementOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
    {
        ((UIElement)sender).ReleaseMouseCapture();
    }

    private void ElementOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
    {
        var s = (UIElement)sender;
        if (!s.IsMouseCaptured) return;
        var parent = FindVisualParent<Canvas>(s);
        var mousePos = mouseEventArgs.GetPosition(parent);
        var diff = (mousePos - _start);
        Canvas.SetLeft(s, _start.X + diff.X - 10);
        Canvas.SetTop(s, _start.Y + diff.Y - 10);
    }
}