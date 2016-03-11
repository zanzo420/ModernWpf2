using ModernWpf.Native.Api;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ModernWpf.Controls
{
    public sealed class AppIconImage : Image
    {
        static readonly ImageSource __smallIcon;
        static readonly ImageSource __largeIcon;

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

        public AppIconImage()
        {
            Source = __smallIcon;
        }


        public bool LargeIcon
        {
            get { return (bool)GetValue(LargeIconProperty); }
            set { SetValue(LargeIconProperty, value); }
        }

        public static readonly DependencyProperty LargeIconProperty =
            DependencyProperty.Register("LargeIcon", typeof(bool), typeof(AppIconImage), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(LargeIcon_Changed)));

        private static void LargeIcon_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AppIconImage)d).Source = ((bool)e.NewValue) ? __largeIcon : __smallIcon;
        }
    }
}
