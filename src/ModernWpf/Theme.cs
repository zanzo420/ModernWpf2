using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ModernWpf
{
    /// <summary>
    /// Allows changing current theme and accent colors.
    /// </summary>
    public static class Theme
    {
        static readonly ResourceDictionary LIGHT_THEME = GetResource("/ModernWPF;component/themes/ColorLight.xaml");
        static readonly ResourceDictionary DARK_THEME = GetResource("/ModernWPF;component/themes/ColorDark.xaml");

        internal static ResourceDictionary GetResource(string url)
        {
            var style = new ResourceDictionary();
            style.Source = new Uri(url, UriKind.Relative);
            return style;
        }

        static Accent _curAccent;
        /// <summary>
        /// Gets the current accent.
        /// </summary>
        /// <value>
        /// The current accent.
        /// </value>
        public static Accent CurrentAccent
        {
            get
            {
                return _curAccent ?? (_curAccent = Accent.LightBlue);
            }
        }


        /// <summary>
        /// Gets the current theme.
        /// </summary>
        /// <value>
        /// The current theme.
        /// </value>
        public static ThemeColor? CurrentTheme { get; private set; }


        /// <summary>
        /// Applies the theme with the accent color to the global application dictionary.
        /// </summary>
        /// <param name="theme">The theme.</param>
        /// <param name="accent">The accent.</param>
        /// <exception cref="System.ArgumentNullException">accent</exception>
        public static void ApplyTheme(ThemeColor theme, Accent accent)
        {
            ApplyTheme(theme, accent, null);
        }

        /// <summary>
        /// Applies the theme with the accent color to the specified dictionary.
        /// </summary>
        /// <param name="theme">The theme.</param>
        /// <param name="accent">The accent.</param>
        /// <param name="dictionary">The dictionary to apply. If <code>null</code> then the application's resource dictionary is used if possible.</param>
        /// <exception cref="System.ArgumentNullException">accent</exception>
        /// <exception cref="System.NotSupportedException">No application resource dictionary found.</exception>
        public static void ApplyTheme(ThemeColor theme, Accent accent, ResourceDictionary dictionary)
        {
            if (accent == null) { throw new ArgumentNullException("accent"); }
            if (dictionary == null)
            {
                if (Application.Current == null)
                {
                    throw new NotSupportedException("No application resource dictionary found.");
                }
                dictionary = Application.Current.Resources;
            }

            _curAccent = accent;
            CurrentTheme = theme;


            ApplyResources(theme == ThemeColor.Light ? LIGHT_THEME : DARK_THEME, dictionary);
            dictionary["ModernAccent"] = accent.MainBrush;

            dictionary["ModernAccentDark1"] = accent.DarkBrush1;
            dictionary["ModernAccentDark2"] = accent.DarkBrush2;
            dictionary["ModernAccentDark3"] = accent.DarkBrush3;
            dictionary["ModernAccentDark4"] = accent.DarkBrush4;

            dictionary["ModernAccentLight1"] = accent.LightBrush1;
            dictionary["ModernAccentLight2"] = accent.LightBrush2;
            dictionary["ModernAccentLight3"] = accent.LightBrush3;
            dictionary["ModernAccentLight4"] = accent.LightBrush4;

            dictionary["ModernAccentAlpha1"] = accent.AlphaBrush1;
            dictionary["ModernAccentAlpha2"] = accent.AlphaBrush2;
            dictionary["ModernAccentAlpha3"] = accent.AlphaBrush3;
            dictionary["ModernAccentAlpha4"] = accent.AlphaBrush4;
            dictionary["ModernAccentAlpha5"] = accent.AlphaBrush5;
            dictionary["ModernAccentAlpha6"] = accent.AlphaBrush6;
            dictionary["ModernAccentAlpha7"] = accent.AlphaBrush7;
            dictionary["ModernAccentAlpha8"] = accent.AlphaBrush8;
            dictionary["ModernAccentAlpha9"] = accent.AlphaBrush9;
        }

        private static void ApplyResources(ResourceDictionary source, ResourceDictionary target)
        {
            foreach (var k in source.Keys)
            {
                target[k] = source[k];
            }
        }
    }

    /// <summary>
    /// Indicates the main color style.
    /// </summary>
    public enum ThemeColor
    {
        /// <summary>
        /// Theme has light background.
        /// </summary>
        Light,
        /// <summary>
        /// Theme has dark background.
        /// </summary>
        Dark
    }
}
