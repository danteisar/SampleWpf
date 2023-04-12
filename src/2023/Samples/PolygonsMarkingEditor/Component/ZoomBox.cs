using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace PolygonsMarkingEditor.Component;

/// <summary>
/// 
/// </summary>
[TemplatePart(Name = "PART_ZoomThumb", Type = typeof(Thumb))]
[TemplatePart(Name = "PART_ZoomCanvas", Type = typeof(Canvas))]
[TemplatePart(Name = "PART_ZoomSlider", Type = typeof(Slider))]
public class ZoomBox : Control
{
    #region fields

    private Thumb _zoomThumb;
    private Canvas _zoomCanvas;
    private Slider _zoomSlider;
    private ScaleTransform _scaleTransform;

    #endregion

    #region ctor

    static ZoomBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoomBox), new FrameworkPropertyMetadata(typeof(ZoomBox)));
    }

    /// <summary>
    /// 
    /// </summary>
    public ZoomBox()
    {
        Unloaded += ZoomBox_Unloaded;
        Loaded += ZoomBox_Loaded;
    }

    #endregion

    #region DPs

    #region ScrollViewer DependencyProperty
    /// <summary>
    /// 
    /// </summary>
    public ScrollViewer ScrollViewer
    {
        get => (ScrollViewer)GetValue(ScrollViewerProperty);
        set => SetValue(ScrollViewerProperty, value);
    }

    /// <summary>
    /// 
    /// </summary>
    public static readonly DependencyProperty ScrollViewerProperty =
        DependencyProperty.Register(nameof(ScrollViewer), typeof(ScrollViewer), typeof(ZoomBox));
    #endregion

    #region DesignerCanvas DependencyProperty


    /// <summary>
    /// 
    /// </summary>
    public static readonly DependencyProperty DesignerCanvasProperty =
        DependencyProperty.Register(nameof(DesignerCanvas), typeof(Canvas), typeof(ZoomBox),
            new FrameworkPropertyMetadata(null, OnDesignerCanvasChanged));


    /// <summary>
    /// 
    /// </summary>
    public Canvas DesignerCanvas
    {
        get => (Canvas)GetValue(DesignerCanvasProperty);
        set => SetValue(DesignerCanvasProperty, value);
    }


    private static void OnDesignerCanvasChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var target = (ZoomBox)d;
        var oldDesignerCanvas = (Canvas)e.OldValue;
        var newDesignerCanvas = target.DesignerCanvas;
        target.OnDesignerCanvasChanged(oldDesignerCanvas, newDesignerCanvas);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="oldDesignerCanvas"></param>
    /// <param name="newDesignerCanvas"></param>
    protected virtual void OnDesignerCanvasChanged(Canvas oldDesignerCanvas, Canvas newDesignerCanvas)
    {
        if (oldDesignerCanvas != null)
        {
            oldDesignerCanvas.LayoutUpdated -= DesignerCanvas_LayoutUpdated;
            //oldDesignerCanvas.MouseWheel -= DesignerCanvas_MouseWheel;
        }
        if (newDesignerCanvas != null)
        {
            newDesignerCanvas.LayoutUpdated += DesignerCanvas_LayoutUpdated;
            //newDesignerCanvas.MouseWheel += DesignerCanvas_MouseWheel;
            newDesignerCanvas.LayoutTransform = _scaleTransform;
        }
    }

    #endregion

    #endregion

    #region methods

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="Exception"></exception>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (ScrollViewer == null) return;

        _zoomThumb = Template.FindName("PART_ZoomThumb", this) as Thumb;
        if (_zoomThumb == null) throw new Exception("PART_ZoomThumb template is missing!");

        _zoomCanvas = Template.FindName("PART_ZoomCanvas", this) as Canvas;
        if (_zoomCanvas == null) throw new Exception("PART_ZoomCanvas template is missing!");

        _zoomSlider = Template.FindName("PART_ZoomSlider", this) as Slider;
        if (_zoomSlider == null)
            throw new Exception("PART_ZoomSlider template is missing!");

        _scaleTransform = new ScaleTransform();
    }

    private void ZoomBox_Loaded(object sender, RoutedEventArgs e)
    {
        MouseWheel += OnMouseWheel;
        if (_zoomThumb != null) _zoomThumb.DragDelta += Thumb_DragDelta;
        if (_zoomSlider != null) _zoomSlider.ValueChanged += ZoomSlider_ValueChanged;
    }

    private void ZoomBox_Unloaded(object sender, RoutedEventArgs e)
    {
        MouseWheel -= OnMouseWheel;
        if (_zoomThumb != null) _zoomThumb.DragDelta -= Thumb_DragDelta;
        if (_zoomSlider != null) _zoomSlider.ValueChanged -= ZoomSlider_ValueChanged;
    }

    private void OnMouseWheel(object sender, MouseWheelEventArgs e)
    {
        var wheel = e;
        var dir = wheel.Delta > 0 ? 1 : -1;
        double value = Math.Min(Math.Abs(wheel.Delta), 10) * dir;
        _zoomSlider.Value += value;
    }

    private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        var scale = e.NewValue / e.OldValue;
        var halfViewportHeight = ScrollViewer.ViewportHeight / 2;
        var newVerticalOffset = (ScrollViewer.VerticalOffset + halfViewportHeight) * scale - halfViewportHeight;
        var halfViewportWidth = ScrollViewer.ViewportWidth / 2;
        var newHorizontalOffset = (ScrollViewer.HorizontalOffset + halfViewportWidth) * scale - halfViewportWidth;
        _scaleTransform.ScaleX *= scale;
        _scaleTransform.ScaleY *= scale;
        ScrollViewer.ScrollToHorizontalOffset(newHorizontalOffset);
        ScrollViewer.ScrollToVerticalOffset(newVerticalOffset);
    }

    private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
    {
        InvalidateScale(out var scale, out _, out _);
        ScrollViewer.ScrollToHorizontalOffset(ScrollViewer.HorizontalOffset + e.HorizontalChange / scale);
        ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset + e.VerticalChange / scale);
    }

    private void DesignerCanvas_LayoutUpdated(object sender, EventArgs e)
    {
        if (_zoomCanvas == null || _scaleTransform == null) return;

        InvalidateScale(out var scale, out var xOffset, out var yOffset);
        _zoomThumb.Width = ScrollViewer.ViewportWidth * scale;
        _zoomThumb.Height = ScrollViewer.ViewportHeight * scale;
        Canvas.SetLeft(_zoomThumb, xOffset + ScrollViewer.HorizontalOffset * scale);
        Canvas.SetTop(_zoomThumb, yOffset + ScrollViewer.VerticalOffset * scale);
    }

    private void InvalidateScale(out double scale, out double xOffset, out double yOffset)
    {
        var w = DesignerCanvas.ActualWidth * _scaleTransform.ScaleX;
        var h = DesignerCanvas.ActualHeight * _scaleTransform.ScaleY;
        var x = _zoomCanvas.ActualWidth;
        var y = _zoomCanvas.ActualHeight;
        var scaleX = x / w;
        var scaleY = y / h;
        scale = scaleX < scaleY ? scaleX : scaleY;
        xOffset = (x - scale * w) / 2;
        yOffset = (y - scale * h) / 2;
    }

    #endregion
}