using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ModernWpf
{
    /// <summary>
    /// Specifies an accent color and its derived variations.
    /// </summary>
    public sealed class Accent
    {
        #region static stuff


        #region pre-defined accents

        /// <summary>
        /// Gets the pre-defined red accent.
        /// </summary>
        public static Accent Red { get; } = new Accent("Red", (Color)ColorConverter.ConvertFromString("#CD3333"));
        /// <summary>
        /// Gets the pre-defined orange accent.
        /// </summary>
        public static Accent Orange { get; } = new Accent("Orange", Colors.Chocolate);
        /// <summary>
        /// Gets the pre-defined gold accent.
        /// </summary>
        public static Accent Gold { get; } = new Accent("Gold", Colors.Goldenrod); // #CDAD00
        /// <summary>
        /// Gets the pre-defined olive accent.
        /// </summary>
        public static Accent Olive { get; } = new Accent("Olive", (Color)ColorConverter.ConvertFromString("#6B8E23"));
        /// <summary>
        /// Gets the pre-defined teal accent.
        /// </summary>
        public static Accent Teal { get; } = new Accent("Teal", (Color)ColorConverter.ConvertFromString("#00959D"));
        /// <summary>
        /// Gets the pre-defined green accent.
        /// </summary>
        public static Accent Green { get; } = new Accent("Green", Colors.ForestGreen);
        /// <summary>
        /// Gets the pre-defined light blue accent.
        /// </summary>
        public static Accent LightBlue { get; } = new Accent("Light Blue", Colors.DodgerBlue);
        /// <summary>
        /// Gets the pre-defined dark blue accent.
        /// </summary>
        public static Accent DarkBlue { get; } = new Accent("Dark Blue", (Color)ColorConverter.ConvertFromString("#007ACC"));
        /// <summary>
        /// Gets the pre-defined light purple accent.
        /// </summary>
        public static Accent LightPurple { get; } = new Accent("Light Purple", Colors.MediumOrchid);
        /// <summary>
        /// Gets the pre-defined dark purple accent.
        /// </summary>
        public static Accent DarkPurple { get; } = new Accent("Dark Purple", Colors.BlueViolet);

        /// <summary>
        /// Gets all the predefined accents in an array.
        /// This returns a new array when called.
        /// </summary>
        /// <returns></returns>
        public static Accent[] GetPredefinedAccents()
        {
            return new Accent[]
            {
                Red, Orange, Gold, Olive, Teal, Green, LightBlue, DarkBlue, LightPurple, DarkPurple
            };
        }

        #endregion


        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Accent" /> class.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="accentColor">Color of the accent.</param>
        public Accent(string displayName, Color accentColor)
        {
            Name = displayName;
            Color = accentColor;

            // instead of alpha modify in intensity

            var hsl = (HSLColor)accentColor;
            var lumiStep = (hsl.Luminosity - 0.1) / 5;
            Debug.WriteLine("{0}\t{1} at {2:n2}", displayName, hsl, lumiStep);
            //var satStep = 0d;
            //if (hsl.Saturation > 0.3)
            //{
            //    satStep = (hsl.Saturation - 0.3) / 5;
            //}

            MainBrush = GetBrush(0xff, accentColor);
            var origLumi = hsl.Luminosity;

            hsl.Luminosity -= lumiStep;
            //hsl.Saturation -= satStep;
            DarkBrush1 = GetBrush(0xff, hsl);

            hsl.Luminosity -= lumiStep;
            //hsl.Saturation -= satStep;
            DarkBrush2 = GetBrush(0xff, hsl);

            hsl.Luminosity -= lumiStep;
            //hsl.Saturation -= satStep;
            DarkBrush3 = GetBrush(0xff, hsl);

            hsl.Luminosity -= lumiStep;
            //hsl.Saturation -= satStep;
            DarkBrush4 = GetBrush(0xff, hsl);

            hsl.Luminosity = origLumi;

            hsl.Luminosity += lumiStep;
            //hsl.Saturation -= satStep;
            LightBrush1 = GetBrush(0xff, hsl);

            hsl.Luminosity += lumiStep;
            //hsl.Saturation -= satStep;
            LightBrush2 = GetBrush(0xff, hsl);

            hsl.Luminosity += lumiStep;
            //hsl.Saturation -= satStep;
            LightBrush3 = GetBrush(0xff, hsl);

            hsl.Luminosity += lumiStep;
            //hsl.Saturation -= satStep;
            LightBrush4 = GetBrush(0xff, hsl);

            // opacity scale
            AlphaBrush1 = GetBrush(0xe5, accentColor);
            AlphaBrush2 = GetBrush(0xcc, accentColor);
            AlphaBrush3 = GetBrush(0xb2, accentColor);
            AlphaBrush4 = GetBrush(0x99, accentColor);
            AlphaBrush5 = GetBrush(0x7f, accentColor);
            AlphaBrush6 = GetBrush(0x66, accentColor);
            AlphaBrush7 = GetBrush(0x4c, accentColor);
            AlphaBrush8 = GetBrush(0x33, accentColor);
            AlphaBrush9 = GetBrush(0x19, accentColor);
        }

        static SolidColorBrush GetBrush(byte alpha, Color color)
        {
            var brush = new SolidColorBrush(Color.FromArgb(alpha, color.R, color.G, color.B));
            brush.Freeze();
            return brush;
        }

        /// <summary>
        /// Gets the accent name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the base color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public Color Color { get; private set; }

        /// <summary>
        /// Gets the main accent brush.
        /// </summary>
        /// <value>
        /// The main brush.
        /// </value>
        public Brush MainBrush { get; private set; }

        /// <summary>
        /// Gets the brush darker than <see cref="MainBrush" />.
        /// </summary>
        /// <value>
        /// The dark brush.
        /// </value>
        public Brush DarkBrush1 { get; private set; }
        /// <summary>
        /// Gets the brush darker than <see cref="DarkBrush1" />.
        /// </summary>
        /// <value>
        /// The dark brush2.
        /// </value>
        public Brush DarkBrush2 { get; private set; }
        /// <summary>
        /// Gets the brush darker than <see cref="DarkBrush2" />.
        /// </summary>
        /// <value>
        /// The dark brush3.
        /// </value>
        public Brush DarkBrush3 { get; private set; }
        /// <summary>
        /// Gets the brush darker than <see cref="DarkBrush3" />.
        /// </summary>
        /// <value>
        /// The dark brush4.
        /// </value>
        public Brush DarkBrush4 { get; private set; }

        /// <summary>
        /// Gets the brush lighter than <see cref="MainBrush" />.
        /// </summary>
        /// <value>
        /// The light brush.
        /// </value>
        public Brush LightBrush1 { get; private set; }
        /// <summary>
        /// Gets the brush lighter than <see cref="LightBrush1" />.
        /// </summary>
        /// <value>
        /// The light brush2.
        /// </value>
        public Brush LightBrush2 { get; private set; }
        /// <summary>
        /// Gets the brush lighter than <see cref="LightBrush2" />.
        /// </summary>
        /// <value>
        /// The light brush3.
        /// </value>
        public Brush LightBrush3 { get; private set; }
        /// <summary>
        /// Gets the brush lighter than <see cref="LightBrush3" />.
        /// </summary>
        /// <value>
        /// The light brush4.
        /// </value>
        public Brush LightBrush4 { get; private set; }

        /// <summary>
        /// Gets the accent brush that's 10% transparent.
        /// </summary>
        /// <value>
        /// The alpha brush.
        /// </value>
        public Brush AlphaBrush1 { get; private set; }
        /// <summary>
        /// Gets the accent brush that's 20% transparent.
        /// </summary>
        /// <value>
        /// The alpha brush2.
        /// </value>
        public Brush AlphaBrush2 { get; private set; }
        /// <summary>
        /// Gets the accent brush that's 30% transparent.
        /// </summary>
        /// <value>
        /// The alpha brush3.
        /// </value>
        public Brush AlphaBrush3 { get; private set; }
        /// <summary>
        /// Gets the accent brush that's 40% transparent.
        /// </summary>
        /// <value>
        /// The alpha brush4.
        /// </value>
        public Brush AlphaBrush4 { get; private set; }
        /// <summary>
        /// Gets the accent brush that's 50% transparent.
        /// </summary>
        /// <value>
        /// The alpha brush5.
        /// </value>
        public Brush AlphaBrush5 { get; private set; }
        /// <summary>
        /// Gets the accent brush that's 60% transparent.
        /// </summary>
        /// <value>
        /// The alpha brush6.
        /// </value>
        public Brush AlphaBrush6 { get; private set; }
        /// <summary>
        /// Gets the accent brush that's 70% transparent.
        /// </summary>
        /// <value>
        /// The alpha brush7.
        /// </value>
        public Brush AlphaBrush7 { get; private set; }
        /// <summary>
        /// Gets the accent brush that's 80% transparent.
        /// </summary>
        /// <value>
        /// The alpha brush8.
        /// </value>
        public Brush AlphaBrush8 { get; private set; }
        /// <summary>
        /// Gets the accent brush that's 90% transparent.
        /// </summary>
        /// <value>
        /// The alpha brush9.
        /// </value>
        public Brush AlphaBrush9 { get; private set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
