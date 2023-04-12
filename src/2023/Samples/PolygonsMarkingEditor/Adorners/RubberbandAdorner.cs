using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using PolygonsMarkingEditor.ViewModels;

using static PolygonsMarkingEditor.Tools.Extensions;

namespace PolygonsMarkingEditor.Adorners;

/// <inheritdoc cref="Adorner"/>
/// <summary>
/// Прямоугольная рамка, внутри которой все элементы типа <see cref="MarkupEditorViewModel" /> выбираются в список.
/// </summary>
public class RubberbandAdorner : Adorner
{
    #region ctor

    /// <inheritdoc />
    /// <summary>
    /// Прямоугольная рамка, внутри которой все элементы типа <see cref="T:DT.DataMiner.FlowChart.ViewModelBase.ItemViewModelBase" /> выбираются в список.
    /// </summary>
    public RubberbandAdorner(Canvas designerCanvas, Point? dragStartPoint)
        : base(designerCanvas)
    {
        _designerCanvas = designerCanvas;
        _startPoint = dragStartPoint;
        var paletteHelper = new PaletteHelper();

        var theme = paletteHelper.GetTheme();

        _rubberbandPen = new Pen(new SolidColorBrush(Color.FromArgb(0x90, theme.SecondaryMid.Color.R, theme.SecondaryMid.Color.G, theme.SecondaryMid.Color.B)), 1) { DashStyle = new DashStyle(new double[] { 3 }, 2) };
        _backBrush = new SolidColorBrush(Color.FromArgb(0x90, theme.SecondaryLight.Color.R, theme.SecondaryLight.Color.G, theme.SecondaryLight.Color.B));
    }

    #endregion

    #region _fields

    private readonly Canvas _designerCanvas;
    private readonly Point? _startPoint;
    private Point? _endPoint;
    private readonly Pen _rubberbandPen;
    private readonly Brush _backBrush;

    #endregion

    #region Methods

    /// <inheritdoc />
    /// <summary>
    /// Изменение размеров при перемещении курсора мыши по канвасу, мышь при этом будет захвачена
    /// </summary>
    /// <param name="e"></param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            if (!IsMouseCaptured) CaptureMouse();
            var p = e.GetPosition(this);
            if (_endPoint != p)
            {
                _endPoint = p;
                InvalidateVisual();
            }
        }
        else
        {
            if (IsMouseCaptured) ReleaseMouseCapture();
        }
        e.Handled = true;
    }

    /// <inheritdoc />
    /// <summary>
    /// При опускании мыши, убрать слой адорнера
    /// </summary>
    /// <param name="e"></param>
    protected override void OnMouseUp(MouseButtonEventArgs e)
    {
        if (IsMouseCaptured) ReleaseMouseCapture();
        UpdateSelection();
        var adornerLayer = AdornerLayer.GetAdornerLayer(_designerCanvas);
        adornerLayer?.Remove(this);
        e.Handled = true;
    }

    //private static int dd = 0;
    /// <inheritdoc />
    /// <summary>
    /// Отрисовка прямоугольника
    /// </summary>
    /// <param name="dc"></param>
    protected override void OnRender(DrawingContext dc)
    {
        base.OnRender(dc);

        dc.DrawRectangle(Brushes.Transparent, null, new Rect(RenderSize));

        if (_startPoint.HasValue && _endPoint.HasValue)
            dc.DrawRectangle(_backBrush, _rubberbandPen, new Rect(_startPoint.Value, _endPoint.Value));
    }

    /// <summary>
    /// Выборка элементов внутри прямоугольной рамки
    /// </summary>
    private void UpdateSelection()
    {
        if (_startPoint == null || _endPoint == null) return;
        if (_designerCanvas.DataContext is not MarkupEditorViewModel vm) return;
        var rubberBand = new Rect(_startPoint.Value, _endPoint.Value);
        var itemsControl = _designerCanvas.FindVisualParent<ItemsControl>();

        foreach (var polygon in vm.Polygons)
        {
            foreach (var itemPoint in polygon.Points)
            {
                var container = itemsControl.ItemContainerGenerator.ContainerFromItem(itemPoint);
                var itemRect = VisualTreeHelper.GetDescendantBounds((Visual)container);
                var itemBounds = ((Visual)container).TransformToAncestor(_designerCanvas)
                    .TransformBounds(itemRect);

                if (rubberBand.Contains(itemBounds))
                {
                    itemPoint.IsSelected = true;
                }
                else
                {
                    if (!(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                    {
                        itemPoint.IsSelected = false;
                    }
                }
            }
        }
    }

    #endregion
}
