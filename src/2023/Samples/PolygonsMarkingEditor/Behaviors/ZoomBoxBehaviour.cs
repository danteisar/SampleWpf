using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;
using PolygonsMarkingEditor.Component;

namespace PolygonsMarkingEditor.Behaviors
{
    internal class ZoomBoxBehaviour : Behavior<Canvas>
    {
        public static readonly DependencyProperty ZoomBoxProperty = DependencyProperty.Register(
            nameof(ZoomBox), typeof(ZoomBox), typeof(ZoomBoxBehaviour), new PropertyMetadata(default(ZoomBox)));

        public ZoomBox ZoomBox
        {
            get => (ZoomBox)GetValue(ZoomBoxProperty);
            set => SetValue(ZoomBoxProperty, value);
        }
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ZoomBox.DesignerCanvas = AssociatedObject;
        }
    }
}
