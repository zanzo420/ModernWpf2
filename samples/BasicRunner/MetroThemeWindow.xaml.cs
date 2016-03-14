using BasicRunner.VM;
using ModernWpf;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BasicRunner
{
    /// <summary>
    /// Interaction logic for MetroThemeWindow.xaml
    /// </summary>
    public partial class MetroThemeWindow : Window
    {
        public MetroThemeWindow()
        {
            InitializeComponent();
            Style = (Style)FindResource("ModernWindow");
        }

        private void Window_DpiChange(object sender, ModernWpf.DpiChangeEventArgs e)
        {
            Debug.WriteLine("DPI changed to " + e.NewDpi);
        }

        private void btnRtl_Checked(object sender, RoutedEventArgs e)
        {
            this.FlowDirection = FlowDirection.RightToLeft;
        }

        private void btnRtl_Unchecked(object sender, RoutedEventArgs e)
        {
            this.FlowDirection = FlowDirection.LeftToRight;
        }

        private void btnMsgBox_Click(object sender, RoutedEventArgs e)
        {
            var box = flyOuter.IsChecked.Value ? (ContentControl)this : innerFlyoutBox;

            ModernMessageBox.Show(box,
        "Warning Message.\n\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Cras faucibus venenatis felis a luctus. Cras cursus est sed interdum consectetur. Fusce vestibulum cursus interdum. Praesent ultricies egestas dolor at elementum. Quisque et pellentesque magna, ac mattis purus. Ut pretium laoreet ullamcorper. Morbi venenatis accumsan varius. Interdum et malesuada fames ac ante ipsum primis in faucibus. Vivamus sit amet laoreet leo. Vestibulum sodales tempus libero vitae tincidunt. Nulla facilisi. Donec posuere sapien ut interdum condimentum. Vivamus nec velit suscipit, dignissim odio a, ullamcorper arcu. Proin ac tellus enim. Quisque in cursus dolor. Curabitur adipiscing vitae sem in ornare.\n\n" +
        "Duis in lacus volutpat, laoreet felis eget, tristique mauris. Maecenas dictum porta purus, id fringilla diam suscipit eu. Sed vitae vulputate erat. Praesent sit amet volutpat urna. Aenean id eros tincidunt, tempor nisl ut, malesuada augue. Nullam ullamcorper, sem sed consequat placerat, velit lacus porttitor velit, et suscipit ipsum nisi id lorem. Vivamus eleifend congue erat, ut rhoncus magna lacinia et.\n\n" +
        "Maecenas in sapien vitae ligula interdum vestibulum ac ut odio. Praesent id posuere ligula. In a neque magna. Cras vestibulum fringilla urna, nec aliquam ante. Proin consectetur a enim eget varius. Aliquam vitae nulla mattis, imperdiet sapien eu, hendrerit nulla. In vel lorem mauris. Vestibulum rutrum, lorem suscipit sagittis euismod, justo nulla pharetra augue, cursus semper ante ante mollis ipsum. Phasellus volutpat augue eget consequat pretium. Vivamus sed ante vel purus hendrerit sollicitudin. Aliquam dignissim leo eu enim interdum auctor. Nam augue tortor, scelerisque facilisis turpis at, venenatis imperdiet dui. Nulla facilisi.", "Caption", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.No);
            ModernMessageBox.Show(box, "Question Message", "Caption", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            ModernMessageBox.Show(box, "Information Message", "Caption", MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.Cancel);
            ModernMessageBox.Show(box, "Error Message", "Caption", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }

        private void radioAccent_Checked(object sender, RoutedEventArgs e)
        {
            var accent = (Accent)((RadioButton)sender).DataContext;

            Theme.ApplyTheme(Theme.CurrentTheme.GetValueOrDefault(), accent, this.Resources);
        }
    }
}
