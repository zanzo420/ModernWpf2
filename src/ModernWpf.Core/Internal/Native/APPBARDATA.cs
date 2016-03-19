using ModernWpf.Native;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ModernWpf.Native
{
    /// <summary>
    /// Contains information about a system appbar message.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    struct APPBARDATA
    {
        /// <summary>
        /// The size of the structure, in bytes.
        /// </summary>
        public uint cbSize;
        /// <summary>
        /// The handle to the appbar window.
        /// </summary>
        public IntPtr hWnd;
        /// <summary>
        /// An application-defined message identifier. The application uses the specified identifier for notification messages that it sends to the appbar identified by the hWnd member. This member is used when sending the ABM_NEW message.
        /// </summary>
        public uint uCallbackMessage;
        /// <summary>
        /// A value that specifies an edge of the screen.
        /// </summary>
        public AppBarEdge uEdge;
        /// <summary>
        /// A RECT structure whose use varies depending on the message.
        /// </summary>
        public RECT rc;
        /// <summary>
        /// A message-dependent value.
        /// </summary>
        public IntPtr lParam;
    }

}
