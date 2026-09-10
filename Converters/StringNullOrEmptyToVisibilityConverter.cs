using System;
using System.Collections.Generic;
using System.Text;

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NeptunoWPF.Converters;

public class StringNullOrEmptyToVisibilityConverter : IValueConverter
{
    public object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
    {
        return string.IsNullOrWhiteSpace(value?.ToString())
            ? Visibility.Collapsed
            : Visibility.Visible;
    }

    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}