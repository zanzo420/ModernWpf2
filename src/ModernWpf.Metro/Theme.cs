using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ModernWpf.Metro
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

        #region predefined accents

        /// <summary>
        /// Gets the predefined accent with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static Accent GetPredefinedAccent(string name)
        {
            return PredefinedAccents.Where(a => string.Equals(a.Name, name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }

        static readonly Accent[] _accents = new Accent[]{
            new Accent(Accent.Red, (Color)ColorConverter.ConvertFromString("#CD3333")),
            new Accent(Accent.Orange, Colors.Chocolate),
            //new Accent(Accent.GOLD,(Color)ColorConverter.ConvertFromString("#CDAD00")),
            new Accent(Accent.Gold,Colors.Goldenrod),
            new Accent(Accent.Olive,(Color)ColorConverter.ConvertFromString("#6B8E23")),
            new Accent(Accent.Teal,(Color)ColorConverter.ConvertFromString("#00959D")),
            new Accent(Accent.Green, Colors.ForestGreen),
            new Accent(Accent.LightBlue, Colors.DodgerBlue),
            new Accent(Accent.DarkBlue,(Color)ColorConverter.ConvertFromString("#007ACC")),
            new Accent(Accent.LightPurple, Colors.MediumOrchid),
            new Accent(Accent.DarkPurple, Colors.BlueViolet),
        };

        /// <summary>
        /// Gets the predefined accents colors.
        /// </summary>
        public static IEnumerable<Accent> PredefinedAccents { get { return _accents; } }

        #endregion

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
                if (_curAccent == null) { _curAccent = GetPredefinedAccent(Accent.LightBlue); }
                return _curAccent;
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
        /// Applies the theme with the name of the accent color theme.
        /// </summary>
        /// <param name="theme">The theme.</param>
        /// <param name="predefinedAccentName">Name of the predefined accent. These can be found in the <see cref="Accent" /> class.</param>
        /// <param name="dictionary">The dictionary to apply. If <code>null</code> then the application's resource dictionary is used.</param>
        public static void ApplyTheme(ThemeColor theme, string predefinedAccentName, ResourceDictionary dictionary = null)
        {
            var accent = GetPredefinedAccent(predefinedAccentName);
            if (accent != null)
            {
                ApplyTheme(theme, accent, dictionary);
            }
        }

        /// <summary>
        /// Applies the theme with the accent color theme.
        /// </summary>
        /// <param name="theme">The theme.</param>
        /// <param name="accent">The accent.</param>
        /// <param name="dictionary">The dictionary to apply. If <code>null</code> then the application's resource dictionary is used.</param>
        /// <exception cref="System.ArgumentNullException">accent</exception>
        public static void ApplyTheme(ThemeColor theme, Accent accent, ResourceDictionary dictionary = null)
        {
            if (accent == null) { throw new ArgumentNullException("accent"); }

            _curAccent = accent;
            CurrentTheme = theme;

            if (dictionary == null) { dictionary = Application.Current.Resources; }

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
