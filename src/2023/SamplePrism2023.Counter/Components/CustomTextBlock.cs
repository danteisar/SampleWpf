using System;
using System.Windows;
using System.Windows.Controls;
using static SamplePrism2023.Counter.Resources.Extensions;

namespace SamplePrism2023.Counter.Components;

[TemplatePart(Name = PartTextBlock, Type = typeof(TextBox))]
public class CustomTextBlock : Control
{
    private const string PartTextBlock = "PART_TextBox";
    
    static CustomTextBlock()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomTextBlock), new FrameworkPropertyMetadata(typeof(CustomTextBlock)));
    }

    #region TextBox

    public TextBox TextBox { get; set; }

    public static void UpdateTextBlock(TextBox textBlock, int counter)
    {
        textBlock.Background = counter < 0
            ? NumberBrushes[NumberValue.LessZero]
            : counter == 0
                ? NumberBrushes[NumberValue.Zero]
                : NumberBrushes[NumberValue.UpperZero];

        textBlock.Text = counter.ToString();
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        TextBox = Template.FindName(PartTextBlock, this) as TextBox;

        if (TextBox is null)
            throw new NullReferenceException($"{PartTextBlock} is missing!");

        UpdateTextBlock(TextBox, Counter);
    }

    #endregion

    #region Value DependencyProperty

    public int Counter
    {
        get => (int)GetValue(CounterProperty);
        set => SetValue(CounterProperty, value);
    }

    public static readonly DependencyProperty CounterProperty 
    = DependencyProperty.Register(nameof(Counter), typeof(int), typeof(CustomTextBlock),
        new PropertyMetadata(0, PropertyChangedCallback));

    private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
       if (d is not CustomTextBlock ctb || ctb.TextBox is null) return;
       if (e.NewValue is not int counter) return;

       UpdateTextBlock(ctb.TextBox, counter);
    }

    #endregion
}