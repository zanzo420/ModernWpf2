using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ModernWpf
{
    /// <summary>
    /// Contains various attached properties for <see cref="ContentPresenter"/> when using the modern theme.
    /// </summary>
    public static class ContentUI
    {

        /// <summary>
        /// Gets the show recognize access key flag.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool GetRecognizeAccessKey(DependencyObject obj)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }
            return (bool)obj.GetValue(RecognizeAccessKeyProperty);
        }

        /// <summary>
        /// Sets the recognize access key flag.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">the flag value.</param>
        public static void SetRecognizeAccessKey(DependencyObject obj, bool value)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }
            obj.SetValue(RecognizeAccessKeyProperty, value);
        }

        /// <summary>
        /// The DP flag to set RecognizeAccessKey in ContentPresenter.
        /// </summary>
        public static readonly DependencyProperty RecognizeAccessKeyProperty =
            DependencyProperty.RegisterAttached("RecognizeAccessKey", typeof(bool), typeof(ContentUI), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));


        
    }
}
