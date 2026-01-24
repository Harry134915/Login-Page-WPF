using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Login_Pape.Helpers
{
    public static class FocusHelper
    {
        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached(
                "IsFocused",
                typeof(bool),
                typeof(FocusHelper),
                new UIPropertyMetadata(false, OnIsFocusedPropertyChanged));

        private static void OnIsFocusedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Control control && e.NewValue is bool isFocused && isFocused)
            {
                // 用 Dispatcher 延迟执行，确保 WPF 鼠标事件先处理完
                control.Dispatcher.BeginInvoke((Action)(() =>
                {
                    control.Focus();

                    if (control is TextBox tb)
                    {
                        tb.CaretIndex = tb.Text.Length; // 光标放到末尾
                    }
                    else if (control is PasswordBox pb)
                    {
                        pb.Focus();
                        // 可选：选中已有密码文本（如果想实现全选）
                        pb.SelectAll();
                    }
                }), DispatcherPriority.Input);
            }
        }
    }
}


