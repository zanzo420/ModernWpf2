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
    /// Interaction logic for ChromeOnlyWindow.xaml
    /// </summary>
    public partial class ChromeOnlyWindow : Window
    {
        public ChromeOnlyWindow()
        {
            InitializeComponent();
        }

        private void Window_DpiChange(object sender, ModernWpf.DpiChangeEventArgs e)
        {
            Debug.WriteLine("DPI changed to " + e.NewDpi);
        }
    }
}
