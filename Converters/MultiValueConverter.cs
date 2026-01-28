using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Login_Pape.Converters
{
    public class MultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
                return Visibility.Collapsed;

            string text = values[0] as string;
            bool IsFocused = values[1] is bool b && b;

            //如果输入框已有文字--->隐藏提示
            if (!string.IsNullOrEmpty(text))
                return Visibility.Collapsed;

            //中文输入法输入中（鼠标焦点在输入框，但文字未上屏）--->显示提示
            if (IsFocused)
                return Visibility.Visible;

            //其他情况(未输入等)--->显示提示
            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
