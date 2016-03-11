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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasicRunner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LaunchWindow : Window
    {
        public LaunchWindow()
        {
            InitializeComponent();
        }

        private void btnChromeOnly_Click(object sender, RoutedEventArgs e)
        {
            new ChromeOnlyWindow { Owner = this }.Show();
        }

        private void Window_DpiChange(object sender, ModernWpf.DpiChangeEventArgs e)
        {
            Debug.WriteLine("DPI changed to " + e.NewDpi);
        }

        private void btnChromeTitle_Click(object sender, RoutedEventArgs e)
        {
            new ChromeWithTitleBarWindow { Owner = this }.Show();
        }
    }
}
