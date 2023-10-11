using Microsoft.Xaml.Behaviors;
using System.Windows;
using System;
using System.Windows.Controls.Primitives;
namespace Controls.Behavior
{
    public class PopupPositionBehavior : Behavior<Popup>
    {
        Window window = new Window();
        protected override void OnAttached()
        {
            base.OnAttached();

            Window window = Window.GetWindow(AssociatedObject);

            if (window != null)
            {
                window.LocationChanged += OnLocationChanged;

                //window.LocationChanged += (s, e) =>
                //{
                //    double ofsett = AssociatedObject.HorizontalOffset;

                //    AssociatedObject.HorizontalOffset = ofsett + 1;
                //    AssociatedObject.HorizontalOffset = ofsett;
                //};
                window.Closing += (s, e) =>
                {
                    if (window == Application.Current.MainWindow)
                        Application.Current.Shutdown();
                    window = null;
                };
            }
        }

        private void OnLocationChanged(object sender, EventArgs e)
        {
            double ofsett = AssociatedObject.HorizontalOffset;

            AssociatedObject.HorizontalOffset = ofsett + 1;
            AssociatedObject.HorizontalOffset = ofsett;
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            window.LocationChanged -= OnLocationChanged;
        }
    }
}
