using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SampleWpf2025.Components;

public partial class Mark : UserControl
{
    public Mark()
    {
        InitializeComponent();
    }

    public Brush Color
    {
        get { return (Brush)GetValue(ColorProperty); }
        set { SetValue(ColorProperty, value); }
    }

    public static readonly DependencyProperty ColorProperty =
        DependencyProperty.Register("Color", typeof(Brush), typeof(Mark), new PropertyMetadata(new SolidColorBrush(Colors.Black)));



    public Visibility MarkVisible
    {
        get { return (Visibility)GetValue(MarkVisibleProperty); }
        set { SetValue(MarkVisibleProperty, value); }
    }

    
    public static readonly DependencyProperty MarkVisibleProperty =
        DependencyProperty.Register("MarkVisible", typeof(Visibility), typeof(Mark), new PropertyMetadata(Visibility.Visible));


    public Orientation Orientation
    {
        get { return (Orientation)GetValue(OrientationProperty); }
        set { SetValue(OrientationProperty, value); }
    }

    public static readonly DependencyProperty OrientationProperty =
        DependencyProperty.Register("Orientation", typeof(Orientation), typeof(Mark), new PropertyMetadata(Orientation.Horizontal));


}
