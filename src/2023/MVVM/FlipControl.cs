using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace MVVM;


[TemplatePart(Name = "PART_FrontContent", Type = typeof(Border)),
 TemplatePart(Name = "PART_BackContent", Type = typeof(Border)),
 TemplateVisualState(Name = "Normal", GroupName = "ViewStates"),
 TemplateVisualState(Name = "Flipped", GroupName = "ViewStates")]
public class FlipControl : Control
{
    static FlipControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(FlipControl), new FrameworkPropertyMetadata(typeof(FlipControl)));
    }


    #region FrontContent DependencyProperty

    public object FrontContent
    {
        get => (object)GetValue(FrontContentProperty);
        set => SetValue(FrontContentProperty, value);
    }

    public static readonly DependencyProperty FrontContentProperty
        = DependencyProperty.Register(nameof(FrontContent), typeof(object), typeof(FlipControl), null);


    #endregion

    #region BackContent DependencyProperty

    public object BackContent
    {
        get => (object)GetValue(BackContentProperty);
        set => SetValue(BackContentProperty, value);
    }

    public static readonly DependencyProperty BackContentProperty
        = DependencyProperty.Register(nameof(BackContent), typeof(object), typeof(FlipControl), null);


    #endregion

    #region IsFlipped DependencyProperty

    public bool IsFlipped
    {
        get => (bool)GetValue(IsFlippedProperty);
        set => SetValue(IsFlippedProperty, value);
    }

    public static readonly DependencyProperty IsFlippedProperty
        = DependencyProperty.Register(nameof(IsFlipped), typeof(bool), typeof(FlipControl), null);


    #endregion

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        //var f1 = GetTemplateChild("PART_FrontContentPresenter") as ContentPresenter ?? throw new NullReferenceException();
        //f1.MouseEnter += OnMouseEnter;

        //var b1 = GetTemplateChild("PART_BackContentPresenter") as ContentPresenter ?? throw new NullReferenceException();
        //b1.MouseLeave += OnMouseLeave;

        if (GetTemplateChild("PART_FlipButton") is ToggleButton flipButton)
        {
            flipButton.Click += flipButton_Click;
            flipButton.Opacity = 0;
            flipButton.MouseEnter += (s, a) => flipButton.Opacity = 1;
            flipButton.MouseLeave += (s, a) => flipButton.Opacity = 0;
        }

        //if (GetTemplateChild("FlipButtonAlternate") is ToggleButton flipButtonAlternate)
        //    flipButtonAlternate.Click += flipButton_Click;

        ChangeVisualState(false);
    }

    private void flipButton_Click(object sender, RoutedEventArgs e)
    {
        IsFlipped = !IsFlipped;
        ChangeVisualState(true);
    }

    private void OnMouseEnter(object sender, MouseEventArgs e)
    {
        IsFlipped = true;
        ChangeVisualState(true);
    }


    private void OnMouseLeave(object sender, MouseEventArgs e)
    {
        IsFlipped = false;
        ChangeVisualState(true);
    }

    private void ChangeVisualState(bool useTransitions)
    {
        VisualStateManager.GoToState(this, !IsFlipped ? "Normal" : "Flipped", useTransitions);

        // Disable flipped side to prevent tabbing to invisible buttons.            
        if (FrontContent is UIElement front)
        {
            front.Visibility = IsFlipped ? Visibility.Hidden : Visibility.Visible;
        }

        if (BackContent is not UIElement back) return;

        back.Visibility = IsFlipped ? Visibility.Visible : Visibility.Hidden;
    }
}