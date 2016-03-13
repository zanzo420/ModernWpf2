using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ModernWpf.Internal
{
    static class DefaultColors
    {
        static Brush MakeBrush(string htmlColor)
        {
            var brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(htmlColor));
            brush.Freeze();
            return brush;
        }

        public static Brush TitleInactiveBackground { get; } = MakeBrush("#69A5D3");
        public static Brush TitleInactiveForeground { get; } = MakeBrush("#eeeeee");

        public static Brush TitleActiveBackground { get; } = MakeBrush("#0078D7");
        public static Brush TitleActiveForeground { get; } = MakeBrush("#f0f0f0");
    }
}
