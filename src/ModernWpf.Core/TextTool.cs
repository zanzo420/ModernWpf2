using ModernWpf.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ModernWpf
{
    /// <summary>
    /// Contains attached property for making <see cref="TextBlock"/> look nice.
    /// </summary>
    public static class TextTool
    {
        static double _threshold = 14;

        /// <summary>
        /// Gets or sets the font size threshold for <see cref="AutoCrispProperty"/> option.
        /// </summary>
        /// <value>
        /// The font size threshold.
        /// </value>
        public static double FontSizeThreshold { get { return _threshold; } set { _threshold = value; } }

        /// <summary>
        /// Gets the AutoCrispProperty value.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool GetAutoCrisp(DependencyObject obj)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }
            return (bool)obj.GetValue(AutoCrispProperty);
        }
        /// <summary>
        /// Gets the AutoCrispProperty value.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">if set to <c>true</c> then auto crisp is turned on.</param>
        public static void SetAutoCrisp(DependencyObject obj, bool value)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }
            obj.SetValue(AutoCrispProperty, value);
        }

        /// <summary>
        /// Dependency property that automatically sets the TextOptions.TextFormattingMode when set on
        /// a <see cref="TextBlock"/>. 
        /// </summary>
        public static readonly DependencyProperty AutoCrispProperty =
            DependencyProperty.RegisterAttached("AutoCrisp", typeof(bool), typeof(TextTool), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(AutoCrisp_Changed)));

        private static void AutoCrisp_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // automatically sets TextOptions.TextFormattingMode based on current DPI and font size.
            var tb = d as TextBlock;
            if (tb != null)
            {
                var oldVal = (bool)e.OldValue;
                var newVal = (bool)e.NewValue;
                if (oldVal != newVal)
                {
                    if ((bool)e.NewValue)
                    {
                        //Debug.WriteLine("Hooking up AutoCrisp for text {0}", (object)tb.Text);

                        var bind = new MultiBinding();
                        bind.Converter = TextFormattingModeConverter.Instance;
                        // actual binding used by converter
                        bind.Bindings.Add(new Binding { Source = tb });
                        // hack to listen to dpi change
                        bind.Bindings.Add(new Binding { Source = tb, Path = new PropertyPath(DpiTool.WindowDpiProperty) });
                        // hack to listen to font size change
                        bind.Bindings.Add(new Binding(TextBlock.FontSizeProperty.Name) { Source = tb });
                        BindingOperations.SetBinding(tb, TextOptions.TextFormattingModeProperty, bind);
                    }
                    else
                    {
                        BindingOperations.ClearBinding(tb, TextOptions.TextFormattingModeProperty);
                    }
                }
            }
        }

    }
}
