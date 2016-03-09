using ModernWpf.Internal;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace ModernWpf
{
    // yes this is the same idea as the WindowChrome class in framework 4.5 but greatly improved for modern style.

    /// <summary>
    /// Attached property class for making a <see cref="Window"/> borderless.
    /// </summary>
    public class Chrome : Freezable
    {
        #region hit test attached dp

        /// <summary>
        /// Attached property to mark an UI element's hit-test type.
        /// </summary>
        public static readonly DependencyProperty HitTestTypeProperty =
            DependencyProperty.RegisterAttached("HitTestType", typeof(ChromeHitTest), typeof(Chrome),
            new FrameworkPropertyMetadata(ChromeHitTest.Client, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Gets the HitTestType property for an UI element.
        /// </summary>
        /// <param name="element">The UI element.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">element</exception>
        public static ChromeHitTest GetHitTestType(DependencyObject element)
        {
            if (element == null) { throw new ArgumentNullException("element"); }

            return (ChromeHitTest)element.GetValue(HitTestTypeProperty);
        }

        /// <summary>
        /// Sets the HitTestType property for an UI element.
        /// </summary>
        /// <param name="element">The UI element.</param>
        /// <param name="type">The hit-test type.</param>
        /// <exception cref="System.ArgumentNullException">element</exception>
        public static void SetHitTestType(DependencyObject element, ChromeHitTest type)
        {
            if (element == null) { throw new ArgumentNullException("element"); }

            element.SetValue(HitTestTypeProperty, type);
        }

        #endregion

        #region chrome attached dp

        /// <summary>
        /// Gets the chrome.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">window</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Chrome GetChrome(Window window)
        {
            if (window == null) { throw new ArgumentNullException("window"); }
            return (Chrome)window.GetValue(Chrome.ChromeProperty);
        }

        /// <summary>
        /// Sets the chrome.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="chrome">The chrome.</param>
        /// <exception cref="System.ArgumentNullException">window</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetChrome(Window window, Chrome chrome)
        {
            if (window == null) { throw new ArgumentNullException("window"); }
            window.SetValue(Chrome.ChromeProperty, chrome);
        }


        /// <summary>
        /// The modern chrome attached property.
        /// </summary>
        public static readonly DependencyProperty ChromeProperty =
            DependencyProperty.RegisterAttached("Chrome", typeof(Chrome), typeof(Chrome), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, ChromeChanged));

        private static void ChromeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d)) { return; }

            Window window = d as Window;
            if (window != null)
            {
                // don't care about old chrome since it has no state
                Chrome newChrome = e.NewValue as Chrome;


                if (newChrome == null)
                {
                    BorderManager.SetManager(window, null);
                }
                else if (e.NewValue != e.OldValue)
                {
                    var worker = BorderManager.GetManager(window);
                    if (worker == null)
                    {
                        worker = new BorderManager();
                        BorderManager.SetManager(window, worker);
                    }
                    else
                    {
                        worker.UpdateChrome(newChrome);
                    }
                }
            }
        }

        #endregion

        #region chrome border dp

        /// <summary>
        /// The dependency property for <see cref="ResizeBorderThickness"/>.
        /// </summary>
        public static readonly DependencyProperty ResizeBorderThicknessProperty =
            DependencyProperty.Register("ResizeBorderThickness", typeof(Thickness), typeof(Chrome), new PropertyMetadata(new Thickness(8)));

        /// <summary>
        /// Gets the resize border thickness.
        /// </summary>
        /// <value>
        /// The resize border thickness.
        /// </value>
        public Thickness ResizeBorderThickness
        {
            get { return (Thickness)GetValue(ResizeBorderThicknessProperty); }
        }


        /// <summary>
        /// The dependency property for <see cref="ActiveBorderBrush"/>.
        /// </summary>
        public static readonly DependencyProperty ActiveBorderBrushProperty =
            DependencyProperty.Register("ActiveBorderBrush", typeof(Brush), typeof(Chrome), new PropertyMetadata(Brushes.DimGray));

        /// <summary>
        /// Gets or sets the active border brush.
        /// </summary>
        /// <value>
        /// The active border brush.
        /// </value>
        [Category("Appearance")]
        public Brush ActiveBorderBrush
        {
            get { return (Brush)GetValue(ActiveBorderBrushProperty); }
            set { SetValue(ActiveBorderBrushProperty, value); }
        }


        /// <summary>
        /// The dependency property for <see cref="InactiveBorderBrush"/>.
        /// </summary>
        public static readonly DependencyProperty InactiveBorderBrushProperty =
            DependencyProperty.Register("InactiveBorderBrush", typeof(Brush), typeof(Chrome), new PropertyMetadata(Brushes.LightGray));

        /// <summary>
        /// Gets or sets the inactive border brush.
        /// </summary>
        /// <value>
        /// The inactive border brush.
        /// </value>
        [Category("Appearance")]
        public Brush InactiveBorderBrush
        {
            get { return (Brush)GetValue(InactiveBorderBrushProperty); }
            set { SetValue(InactiveBorderBrushProperty, value); }
        }

        #endregion
        
        /// <summary>
        /// When implemented in a derived class, creates a new instance of the <see cref="T:System.Windows.Freezable" /> derived class.
        /// </summary>
        /// <returns>
        /// The new instance.
        /// </returns>
        protected override Freezable CreateInstanceCore()
        {
            return new Chrome();
        }
    }
}
