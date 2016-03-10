using ModernWpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BasicRunner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DpiTool.EnableHighDpiSupport();
            Debug.WriteLine("Per-monitor DPI Awareness=" + DpiTool.IsPerMonitorAware);
            base.OnStartup(e);
        }
    }
}
