using ModernWpf;
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
    /// Interaction logic for ChromeWithTitleBarWindow.xaml
    /// </summary>
    public partial class ChromeWithTitleBarWindow : Window
    {
        public ChromeWithTitleBarWindow()
        {
            InitializeComponent();
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

        private void leftBtn_Click(object sender, RoutedEventArgs e)
        {
            if (leftBtn.IsChecked.GetValueOrDefault()) { leftContent.Visibility = Visibility; }
            else { leftContent.Visibility = Visibility.Collapsed; }
        }
    }
}
