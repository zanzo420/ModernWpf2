using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interop;

namespace ModernWpf
{
    /// <summary>
    /// Wrapper on a WPF <see cref="Window"/> to work in winform land.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.IWin32Window" />
    class Wpf32Window : System.Windows.Forms.IWin32Window
    {
        public IntPtr Handle { get; private set; }

        public Wpf32Window(Window window)
        {
            Handle = new WindowInteropHelper(window).Handle;
        }
    }
}
