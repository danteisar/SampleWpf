using System.Windows;
using System.Windows.Controls;

namespace SampleWpf2025.Components;

public partial class Digit : UserControl
{
    public Digit()
    {
        InitializeComponent();
    }

    public int Score
    {   
        get { return (int)GetValue(ScoreProperty); }
        set { SetValue(ScoreProperty, value); }
    }

    public static readonly DependencyProperty ScoreProperty =
        DependencyProperty.Register("Score", typeof(int), typeof(Digit), new PropertyMetadata(0));

}

