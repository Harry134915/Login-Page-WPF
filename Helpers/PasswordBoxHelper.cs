using System.Windows;
using System.Windows.Controls;

namespace Login_Pape.Helpers
{
    public static class PasswordBoxHelper
    {
        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached(
                "BoundPassword",
                typeof(string),
                typeof(PasswordBoxHelper),
                new FrameworkPropertyMetadata(string.Empty, OnBoundPasswordChanged));

        public static string GetBoundPassword(DependencyObject d)
            => (string)d.GetValue(BoundPasswordProperty);

        public static void SetBoundPassword(DependencyObject d, string value)
            => d.SetValue(BoundPasswordProperty, value);

        private static readonly DependencyProperty IsUpdatingProperty =
            DependencyProperty.RegisterAttached(
                "IsUpdating",
                typeof(bool),
                typeof(PasswordBoxHelper));

        private static void OnBoundPasswordChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox pb)
            {
                pb.PasswordChanged -= PasswordChanged;

                if (!(bool)pb.GetValue(IsUpdatingProperty))
                {
                    pb.Password = e.NewValue?.ToString() ?? string.Empty;
                }

                pb.PasswordChanged += PasswordChanged;
            }
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pb = (PasswordBox)sender;
            pb.SetValue(IsUpdatingProperty, true);
            SetBoundPassword(pb, pb.Password);
            pb.SetValue(IsUpdatingProperty, false);
        }
    }
}

