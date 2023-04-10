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
    private Point? _rubberbandSelectionStartPoint;

    protected override void OnAttached()
    {
        AssociatedObject.MouseMove += AssociatedObjectOnMouseMove;
        AssociatedObject.MouseRightButtonDown += OnMouseDown;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.MouseMove -= AssociatedObjectOnMouseMove;
        AssociatedObject.MouseRightButtonDown -= OnMouseDown;
    }

    private void AssociatedObjectOnMouseMove(object sender, MouseEventArgs e)
    {
        if (_rubberbandSelectionStartPoint != null && e.RightButton != MouseButtonState.Pressed)
        {
            _rubberbandSelectionStartPoint = null;
        }

        if (!_rubberbandSelectionStartPoint.HasValue) return;

        var adornerLayer = AdornerLayer.GetAdornerLayer(AssociatedObject);
        if (adornerLayer == null) return;

        var adorner = new RubberbandAdorner(AssociatedObject, _rubberbandSelectionStartPoint);
        adornerLayer.Add(adorner);
    }

    private void OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.RightButton != MouseButtonState.Pressed || !Equals(e.Source, AssociatedObject)) return;

        _rubberbandSelectionStartPoint = e.GetPosition(AssociatedObject);

        var vm = AssociatedObject.DataContext as MarkupEditorViewModel;
        if (!(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
        {
            vm?.ClearSelectedItemsCommand.Execute(null);
        }
        e.Handled = true;
    }

    
}