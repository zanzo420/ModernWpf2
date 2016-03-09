using ModernWpf.Controls;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ModernWpf.Converters
{
    /// <summary>
    /// Simple converter to select border color.
    /// </summary>
    class BorderBrushConverter : IMultiValueConverter
    {
        BorderWindow _border;
        public BorderBrushConverter(BorderWindow border)
        {
            _border = border;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) { throw new ArgumentNullException("values"); }
            if (values.Length != 2) { throw new ArgumentException("Values must have 2 brush objects."); }
            return _border.IsContentActive ? values[0] : values[1];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

}
