using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;
using PolygonsMarkingEditor.Adorners;
using PolygonsMarkingEditor.ViewModels;

namespace PolygonsMarkingEditor.Behaviors;

internal class RubberbandBehaviour : Behavior<Canvas>
{
    private Point? _startPoint;

    protected override void OnAttached()
    {
        AssociatedObject.MouseMove += AssociatedObjectOnMouseMove;
        AssociatedObject.MouseLeftButtonDown += OnMouseDown;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.MouseMove -= AssociatedObjectOnMouseMove;
        AssociatedObject.MouseLeftButtonDown -= OnMouseDown;
    }

    private void AssociatedObjectOnMouseMove(object sender, MouseEventArgs e)
    {
        if (_startPoint != null && e.LeftButton != MouseButtonState.Pressed)
        {
            _startPoint = null;
        }

        if (!_startPoint.HasValue) return;

        var layer = AdornerLayer.GetAdornerLayer(AssociatedObject);

        layer?.Add(new RubberbandAdorner(AssociatedObject, _startPoint));
    }

    private void OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton != MouseButtonState.Pressed || !Equals(e.Source, AssociatedObject)) return;

        _startPoint = e.GetPosition(AssociatedObject);

        var vm = AssociatedObject.DataContext as MarkupEditorViewModel;
        if (!(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
        {
            vm?.ClearSelectedItemsCommand.Execute(null);
        }
        e.Handled = true;
    }

    
}