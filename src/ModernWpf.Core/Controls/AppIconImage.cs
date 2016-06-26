using ModernWpf.Native.Api;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System;
using System.Windows.Data;

namespace ModernWpf.Controls
{
    /// <summary>
    /// An image that only displays the current application's icon in small or large format.
    /// </summary>
    /// <seealso cref="System.Windows.Controls.Image" />
    public sealed class AppIconImage : Image
    {
        static readonly ImageSource __appSmallIcon;
        //static readonly ImageSource __appLargeIcon;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static AppIconImage()
        {
            StretchProperty.OverrideMetadata(typeof(AppIconImage), new FrameworkPropertyMetadata(Stretch.None));
            SourceProperty.OverrideMetadata(typeof(AppIconImage), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(SourceChanged), new CoerceValueCallback(SourceCoerceCallback)));

            string exePath = GetExePath();

            __appSmallIcon = IconReader.GetFileIcon(exePath, IconReader.IconSize.Small, false);
            //__appLargeIcon = IconReader.GetFileIcon(exePath, IconReader.IconSize.Large, false);
        }

        private static void SourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private static object SourceCoerceCallback(DependencyObject d, object value)
        {
            if (value == null)
            {
                var win = Window.GetWindow(d);
                if (win == null || win.Icon == null)
                {
                    //BindingOperations.ClearBinding(d, Image.SourceProperty);
                    //aii.Source = aii.LargeIcon ? __appLargeIcon : __appSmallIcon;
                    //return aii.LargeIcon ? __appLargeIcon : __appSmallIcon;
                    return __appSmallIcon;
                }
                else
                {
                    return win.Icon;
                }
            }
            return value;
        }

        private static string GetExePath()
        {
            var ass = Assembly.GetEntryAssembly();
            if (ass == null)
            {
                using (var proc = Process.GetCurrentProcess())
                {
                    return proc.MainModule.FileName;
                }
            }
            else
            {
                return ass.Location;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppIconImage"/> class.
        /// </summary>
        public AppIconImage()
        {
            SetBinding(Image.SourceProperty, new Binding("Icon")
            {
                RelativeSource = new RelativeSource
                {
                    Mode = RelativeSourceMode.FindAncestor,
                    AncestorType = typeof(Window), 
                },
                Mode = BindingMode.OneWay
            });
        }

        //private void UpdateIconSize()
        //{
        //    //if (UseWindowIcon)
        //    //{
        //    //    var win = Window.GetWindow(this);
        //    //    SetBinding(Image.SourceProperty, new Binding("Icon")
        //    //    {
        //    //        Source = Window.GetWindow(this),
        //    //    });
        //    //}
        //    //else
        //    //{
        //    //    BindingOperations.ClearBinding(this, Image.SourceProperty);
        //    //    Source = LargeIcon ? __appLargeIcon : __appSmallIcon;
        //    //}
        //}




        ///// <summary>
        ///// Gets or sets a value indicating whether to show large icon.
        ///// </summary>
        ///// <value>
        /////   <c>true</c> to show large icon; otherwise, <c>false</c>.
        ///// </value>
        //public bool LargeIcon
        //{
        //    get { return (bool)GetValue(LargeIconProperty); }
        //    set { SetValue(LargeIconProperty, value); }
        //}

        ///// <summary>
        ///// The dependency property for <see cref="LargeIcon"/>,
        ///// </summary>
        //public static readonly DependencyProperty LargeIconProperty =
        //    DependencyProperty.Register("LargeIcon", typeof(bool), typeof(AppIconImage), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(LargeIcon_Changed)));

        //private static void LargeIcon_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    ((AppIconImage)d).UpdateIconSize();
        //}
    }
}
