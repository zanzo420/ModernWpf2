using ModernWpf.Native.Api;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ModernWpf.Controls
{
    /// <summary>
    /// An image that only displays the current application's icon in small or large format.
    /// </summary>
    /// <seealso cref="System.Windows.Controls.Image" />
    public sealed class AppIconImage : Image
    {
        static readonly ImageSource __smallIcon;
        static readonly ImageSource __largeIcon;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static AppIconImage()
        {
            StretchProperty.OverrideMetadata(typeof(AppIconImage), new FrameworkPropertyMetadata(Stretch.None));

            string exePath = GetExePath();

            __smallIcon = IconReader.GetFileIcon(exePath, IconReader.IconSize.Small, false);
            __largeIcon = IconReader.GetFileIcon(exePath, IconReader.IconSize.Large, false);
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
            Source = __smallIcon;
        }


        /// <summary>
        /// Gets or sets a value indicating whether to show large icon.
        /// </summary>
        /// <value>
        ///   <c>true</c> to show large icon; otherwise, <c>false</c>.
        /// </value>
        public bool LargeIcon
        {
            get { return (bool)GetValue(LargeIconProperty); }
            set { SetValue(LargeIconProperty, value); }
        }

        /// <summary>
        /// The dependency property for <see cref="LargeIcon"/>,
        /// </summary>
        public static readonly DependencyProperty LargeIconProperty =
            DependencyProperty.Register("LargeIcon", typeof(bool), typeof(AppIconImage), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(LargeIcon_Changed)));

        private static void LargeIcon_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AppIconImage)d).Source = ((bool)e.NewValue) ? __largeIcon : __smallIcon;
        }
    }
}
